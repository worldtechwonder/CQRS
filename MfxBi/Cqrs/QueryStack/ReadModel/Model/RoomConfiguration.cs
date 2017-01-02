using System;

namespace MfxBi.Cqrs.QueryStack.ReadModel.Model
{
    public class RoomConfiguration : Dto
    {
        public RoomConfiguration()
        {
            Enabled = true;
            StartingHour = 8;
            StartingMins = 0;
            EndingHour = 16;
            SlotLengthInMins = 45;
        }

        public int RooomId { get; set; }
        public int StartingHour { get; set; }
        public int StartingMins { get; set; }
        public int EndingHour { get; set; }
        public int SlotLengthInMins { get; set; }
        public bool Enabled { get; set; }

        public DateTime? InEffectSince { get; set; }
        public DateTime? InEffectUntil { get; set; }

        public bool IsInEffect(DateTime? date = null)
        {
            if (!Enabled)
                return false;

            var dateToCheck = DateTime.Today;
            if (date.HasValue)
                dateToCheck = date.Value;

            return dateToCheck >= InEffectSince.GetValueOrDefault() &&
                   dateToCheck <= InEffectUntil.GetValueOrDefault();
        }

        public static RoomConfiguration Default()
        {
            return new RoomConfiguration();
        }
    }
}