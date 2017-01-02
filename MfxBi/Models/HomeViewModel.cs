using System;
using System.Collections.Generic;
using MfxBi.Cqrs.QueryStack.ReadModel;
using MfxBi.Cqrs.QueryStack.ReadModel.Model;

namespace MfxBi.Models
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            Items = new List<ScheduleItem>();
            DayOfSchedule = DateTime.Today;
            Room = new Room();
        }

        public IList<ScheduleItem> Items { get; set; }
        public Room Room { get; set; }
        public DateTime DayOfSchedule { get; set; }
    }
}