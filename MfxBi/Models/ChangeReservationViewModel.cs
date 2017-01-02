using System;

namespace MfxBi.Models
{
    public class ChangeReservationViewModel
    {
        public ChangeReservationViewModel()
        {
            Shift = 0;
            Day = DateTime.Today;
        }

        public static ChangeReservationViewModel Default()
        {
            var model = new ChangeReservationViewModel();
            return model;
        }

        public Guid BookingId { get; set; }
        public DateTime Day { get; set; }
        public int Shift { get; set; }
    }
}