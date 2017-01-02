using System.Collections.Generic;
using MfxBi.Cqrs.QueryStack.ReadModel;

namespace MfxBi.Models
{
    public class DashboardViewModel
    {
        public DashboardViewModel()
        {
            PendingBookings = new List<BookingSummary>();
        }

        public IList<BookingSummary> PendingBookings { get; set; }
    }
}