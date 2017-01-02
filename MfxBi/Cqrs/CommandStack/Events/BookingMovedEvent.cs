using System;
using Memento;

namespace MfxBi.Cqrs.CommandStack.Events
{
    public class BookingMovedEvent : DomainEvent
    {
        public BookingMovedEvent(Guid id, int shift)
        {
            BookingId = id;
            Shift = shift;
        }

        public Guid BookingId { get; private set; }
        public int Shift { get; private set; }
    }
}