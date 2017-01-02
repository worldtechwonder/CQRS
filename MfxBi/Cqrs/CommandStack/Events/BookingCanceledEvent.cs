using System;
using Memento;

namespace MfxBi.Cqrs.CommandStack.Events
{
    public class BookingCanceledEvent : DomainEvent
    {
        public BookingCanceledEvent(Guid id)
        {
            BookingId = id;
        }

        public Guid BookingId { get; private set; }
    }
}