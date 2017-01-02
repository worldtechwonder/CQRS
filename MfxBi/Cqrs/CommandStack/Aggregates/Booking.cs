using System;
using Memento.Domain;
using MfxBi.Common;
using MfxBi.Cqrs.CommandStack.Events;

namespace MfxBi.Cqrs.CommandStack.Aggregates
{
    public class Booking : Aggregate,
        IApplyEvent<NewBookingCreatedEvent>,
        IApplyEvent<BookingMovedEvent>,
        IApplyEvent<BookingCanceledEvent> 
    {
        public Guid BookingId { get; set; }
        public DateTime Received { get; set; }
        public string CustomerName { get; set; }
        public DateTime Day { get; set; }
        public int Hour { get; set; }
        public int Mins { get; set; }
        public int NumberOfSlots { get; set; }
        public BookingReason Reason { get; set; }
        public bool Active { get; set; }

        #region DOMAIN EVENTS
        public void ApplyEvent(
            [AggregateId("BookingId")] NewBookingCreatedEvent theEvent)
        {
            BookingId = theEvent.BookingId;
            Day = theEvent.When;
            Received = DateTime.UtcNow;
            Hour = theEvent.Hour;
            Mins = theEvent.Mins;
            NumberOfSlots = theEvent.Length;
            CustomerName = theEvent.FullName;
            Reason = BookingReason.Regular;
            Active = true;
        }

        public void ApplyEvent(
            [AggregateId("BookingId")] BookingMovedEvent theEvent)
        {
            Day = Day.Date.AddDays(theEvent.Shift);

            // 
        }

        public void ApplyEvent(
            [AggregateId("BookingId")] BookingCanceledEvent theEvent)
        {
            Active = false;           
        }
        #endregion


        #region BUSINESS
        public void Cancel()
        {
            var canceled = new BookingCanceledEvent(BookingId);
            RaiseEvent(canceled);
        }

        public void Shift(int shift)
        {
            var moved = new BookingMovedEvent(BookingId, shift);
            RaiseEvent(moved);
        }

        public DateTime ToDateTime()
        {
            return Day.Date.AddHours(Hour).AddMinutes(Mins);
        }
        #endregion


        #region FACTORY
        public static class Factory
        {
            public static Booking NewRequestFrom(string name, DateTime when, int hour, int mins, int length)
            {
                var created = new NewBookingCreatedEvent(Guid.NewGuid(), name.Capitalize(), when, hour, mins, length);

                // Tell the aggregate to log the "received" event
                var booking = new Booking();
                booking.RaiseEvent(created);
                return booking;
            }
        }
        #endregion
    }
}