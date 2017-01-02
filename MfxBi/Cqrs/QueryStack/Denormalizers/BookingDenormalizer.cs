using System.Linq;
using Memento.Messaging.Postie;
using MfxBi.Cqrs.CommandStack.Events;
using MfxBi.Cqrs.QueryStack.ReadModel;

namespace MfxBi.Cqrs.QueryStack.Denormalizers
{
    public class BookingDenormalizer :
        IHandleMessages<NewBookingCreatedEvent>,
        IHandleMessages<BookingMovedEvent>,
        IHandleMessages<BookingCanceledEvent>
    {
        public void Handle(NewBookingCreatedEvent message)
        {
            var item = new BookingSummary()
            {
                DisplayName = message.FullName,
                BookingId = message.BookingId,
                Day = message.When,
                StartHour = message.Hour,
                StartMins = message.Mins,
                NumberOfSlots = message.Length
            };
            
            using (var context = new MfxbiDatabase())
            {
                context.BookingSummaries.Add(item);
                context.SaveChanges();
            }
        }

        public void Handle(BookingCanceledEvent message)
        {
            using (var context = new MfxbiDatabase())
            {
                var existing = (from b in context.BookingSummaries
                    where b.BookingId == message.BookingId
                    select b).FirstOrDefault();
                if (existing == null)
                    return;

                context.BookingSummaries.Remove(existing);
                context.SaveChanges();
            }
        }

        public void Handle(BookingMovedEvent message)
        {
            using (var context = new MfxbiDatabase())
            {
                var existing = (from b in context.BookingSummaries
                    where b.BookingId == message.BookingId
                    select b).FirstOrDefault();
                if (existing == null)
                    return;

                existing.Day = existing.Day.AddDays(message.Shift);
                context.SaveChanges();
            }
        }
    }
}