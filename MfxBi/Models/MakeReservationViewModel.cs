using System;

namespace MfxBi.Models
{
    public class MakeReservationViewModel
    {
        public MakeReservationViewModel()
        {
            Hour = 0;
            Mins = 0;
            NumberOfSlots = 0;
            FullName = String.Empty;
            Day = DateTime.Today;
        }

        public static MakeReservationViewModel Default()
        {
            var model = new MakeReservationViewModel();
            return model;
        }

        public int RoomId { get; set; }
        public DateTime Day { get; set; }
        public int Hour { get; set; }
        public int Mins { get; set; }
        public int NumberOfSlots { get; set; }
        public string FullName { get; set; }
    }
}