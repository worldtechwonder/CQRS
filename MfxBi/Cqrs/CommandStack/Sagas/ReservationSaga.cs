using System.Transactions;
using Memento.Messaging.Postie;
using Memento.Persistence;
using MfxBi.Cqrs.CommandStack.Aggregates;
using MfxBi.Cqrs.CommandStack.Commands;
using MfxBi.Cqrs.CommandStack.Services;

namespace MfxBi.Cqrs.CommandStack.Sagas
{
    public class ReservationSaga : Saga,
        IAmStartedBy<MakeReservationCommand>,
        IHandleMessages<ChangeReservationCommand>,
        IHandleMessages<CancelReservationCommand>
    {
        public ReservationSaga(IBus bus, IEventStore eventStore, IRepository repository)
            : base(bus, eventStore, repository)
        {
        }

        public void Handle(MakeReservationCommand message)
        {
            // Invoke domain service to determine longest possible duration
            var numberOfSlots = message.NumberOfSlots;
            if (numberOfSlots == 0)
            {
                var durationService = new MaxDurationService(EventStore, Repository);
                numberOfSlots = durationService.LongestPossible(message.RoomId, message.When, message.Hour, message.Mins);
            }

            var booking = Booking.Factory.NewRequestFrom(
                message.FullName, message.When, message.Hour, message.Mins, numberOfSlots);
            Repository.Save(booking);
            

            // Notify back
            //var hub = new BookingHub();
            //hub.NotifyReceived(message.SignalRConnectionId, booking.BookingId);
        }

       
        public void Handle(CancelReservationCommand message)
        {
            var booking = Repository.GetById<Booking>(message.BookingId);
            booking.Cancel();
            Repository.Save(booking);
        }

        public void Handle(ChangeReservationCommand message)
        {
            // Lock issues: what about implementing a manual locking system
            // so that the aggregate is NOT available for writing since it is selected. And locking
            // is successful only if the aggregate BOOKING is available at the time of locking.

            var booking = Repository.GetById<Booking>(message.BookingId);
            var service = new MoverService(EventStore, Repository);
            using (var tx = new TransactionScope())
            {
                if (!service.CanMoveBooking(booking, message.Shift)) 
                    return;

                booking.Shift(message.Shift);
                Repository.Save(booking);
                tx.Complete();
            }
        }
    }
}