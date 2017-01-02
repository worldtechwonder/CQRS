using MfxBi.Cqrs.CommandStack.Aggregates;

namespace MfxBi.Models
{
    public class BookingDetailsViewModel
    {
        public BookingDetailsViewModel()
        {
            Booking = new Booking();
        }

        public Booking Booking { get; set; }
    }
}