using System.Linq;
using System.Threading;
using Memento.Persistence;
using MfxBi.Cqrs.Api;
using MfxBi.Cqrs.CommandStack.Aggregates;
using MfxBi.Cqrs.CommandStack.Events;

namespace MfxBi.Cqrs.CommandStack.Services
{
    public class MoverService : DomainService
    {
        public MoverService(IEventStore eventStore, IRepository repository) : base(eventStore, repository)
        {
        }

        public bool CanMoveBooking(Booking bookingToMove, int shift)
        {
            // THIS IS NOT A WAY TO HANDLE CONCURRENCY (manual locking system that tentatively allocates a resource
            // for at most a given amount of time and then releases it).
            // THIS JUST helps making sure (for the purposes of the demo) that there's no clash as SHIFT is 
            // randomly generated.
            var newDate = bookingToMove.ToDateTime().AddDays(shift);

            // Check to see if there's any active booking on that slot
            var createdEvents = EventStore.Find<NewBookingCreatedEvent>(e =>
                e.ToDateTime() == newDate).ToList();
            foreach (var e in createdEvents)
            {
                var bookingId = e.BookingId;
                var booking = Repository.GetById<Booking>(bookingId);
                if (booking.Active && booking.ToDateTime() == newDate)
                    return false;
            }

            return true;
        }
    }
}