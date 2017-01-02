using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MfxBi.Cqrs.CommandStack.Aggregates;

namespace MfxBi.Cqrs.QueryStack.ReadModel
{
    public class BookingSummary : Dto
    {
        public Guid BookingId { get; set; }

        [Index]
        [MaxLength(200)]
        public string DisplayName { get; set; }
        public DateTime Day { get; set; }
        public int StartHour { get; set; }
        public int StartMins { get; set; }
        public int NumberOfSlots { get; set; }
        public BookingReason Reason { get; set; }
    }
}