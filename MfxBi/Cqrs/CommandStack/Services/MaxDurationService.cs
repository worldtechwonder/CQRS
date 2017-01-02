using System;
using System.Linq;
using Memento.Persistence;
using MfxBi.Cqrs.Api;
using MfxBi.Cqrs.CommandStack.Aggregates;
using MfxBi.Cqrs.CommandStack.Events;
using MfxBi.Cqrs.QueryStack.Services;

namespace MfxBi.Cqrs.CommandStack.Services
{
    public class MaxDurationService : DomainService
    {
        public MaxDurationService(IEventStore eventStore, IRepository repository)
            :base(eventStore, repository)
        {           
        }

        public int LongestPossible(int roomId, DateTime day, int hour, int mins)
        {
            var numberOfSlot = 1;

            var nearest = FindNearestBooking(day, hour, mins);
            var config = RoomHelpers.FindCurrentConfigForRoom(roomId, day);
            var slotLength = config.SlotLengthInMins;

            var nearestDateTime = nearest == null 
                ? day.Date.AddHours(config.EndingHour).AddMinutes(slotLength-5) 
                : nearest.ToDateTime();

            var ts = nearestDateTime - day.AddHours(hour).AddMinutes(mins);
            numberOfSlot = (int) ts.TotalMinutes/slotLength;

            return numberOfSlot;
        }

        private Booking FindNearestBooking(DateTime day, int hourOfDay, int minsOfDay)
        {
            var date = day.Date.AddHours(hourOfDay).AddMinutes(minsOfDay);
            var createdEvents = EventStore.Find<NewBookingCreatedEvent>(e =>
                e.ToDateTime() >= date).ToList();

            // Problem here is that we only know that a booking has been created. 
            // We don't know whether it's been canceled later. If we take only the most recent
            // event it might be one that was later canceled. Hence, let's keep ALL events for now.

            // Map events to actual aggregates, check existence, and take the earliest we find
            // after the specified time slot
            var nearest = createdEvents
                .Select(e => Repository.GetById<Booking>(e.BookingId))
                .Where(actualBooking => actualBooking.Active)
                .OrderBy(b => b.Hour).ThenBy(b => b.Mins)
                .FirstOrDefault();
            return nearest;
        }
    }
}