using System;
using Memento;

namespace MfxBi.Cqrs.CommandStack.Commands
{
    public class ChangeReservationCommand : Command
    {
        public ChangeReservationCommand(Guid bookingId, int shift)
        {
            BookingId = bookingId;
            Shift = shift;
        }

        public Guid BookingId { get; private set; }
        public int Shift { get; private set; }

        // SignalR extension
        public string SignalRConnectionId { get; set; }
    }
}