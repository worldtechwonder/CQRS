using System;
using Memento;

namespace MfxBi.Cqrs.CommandStack.Commands
{
    public class CancelReservationCommand : Command
    {
        public CancelReservationCommand(string bookingId)
        {
            BookingId = Guid.Parse(bookingId);
        }

        public Guid BookingId { get; private set; }

        // SignalR extension
        public string SignalRConnectionId { get; set; }
    }
}