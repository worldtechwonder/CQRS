namespace MfxBi.Cqrs.QueryStack.ReadModel
{
    public class ScheduleItem 
    {
        public ScheduleItem(int hour, int mins, BookingSummary booking)
        {
            IsFirstSlotOfBooking = true;
            IsAvailable = true;

            Hour = hour;
            Mins = mins;
            if (booking != null)
            {
                Text = booking.DisplayName;
                IsAvailable = false;
            }
        }
        public BookingSummary CurrentBooking { get; set; }

        public int Hour { get; set; }
        public int Mins { get; set; }

        public string Text { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsFirstSlotOfBooking { get; set; }
    }
}