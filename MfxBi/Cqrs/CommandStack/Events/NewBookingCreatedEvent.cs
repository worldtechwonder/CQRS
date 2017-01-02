using System;
using Memento;

namespace MfxBi.Cqrs.CommandStack.Events
{
    public class NewBookingCreatedEvent : DomainEvent
    {
        public NewBookingCreatedEvent(Guid id, string name, DateTime when, int hour, int mins, int length)
        {
            BookingId = id;
            FullName = name;
            Hour = hour;
            Mins = mins;
            Length = length;
            When = when;
        }

        public Guid BookingId { get; private set; }
        public string FullName { get; private set; }
        public int Hour { get; private set; }
        public int Mins { get; private set; }
        public int Length { get; private set; }
        public DateTime When { get; private set; }

        public DateTime ToDateTime()
        {
            return When.Date.AddHours(Hour).AddMinutes(Mins);
        }
    }
}