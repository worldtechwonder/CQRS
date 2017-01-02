using Memento.Messaging.Postie;
using MfxBi.Cqrs.CommandStack.Commands;
using MfxBi.Models;

namespace MfxBi.Application
{
    public class BookingService
    {
        public IBus Bus { get; private set; }

        public BookingService(IBus bus)
        {
            Bus = bus;
        }

        public void MakeReservation(MakeReservationViewModel input, string connectionId)
        {
            var command = new MakeReservationCommand(input.RoomId, 
                input.FullName, input.Day, input.Hour, input.Mins, input.NumberOfSlots)
            {
                SignalRConnectionId = connectionId
            };
            Bus.Send(command);
        }

        public void ChangeReservation(ChangeReservationViewModel input, string connectionId)
        {
            var command = new ChangeReservationCommand(input.BookingId, input.Shift)
            {
                SignalRConnectionId = connectionId
            };
            Bus.Send(command);
        }

        public void CancelReservation(string bid)
        {
            var command = new CancelReservationCommand(bid)
            {
                //SignalRConnectionId = connectionId
            };
            Bus.Send(command);
        }
    }
}