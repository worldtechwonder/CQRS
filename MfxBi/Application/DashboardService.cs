using System;
using System.Linq;
using Memento.Messaging.Postie;
using Memento.Persistence;
using MfxBi.Cqrs.CommandStack.Aggregates;
using MfxBi.Cqrs.QueryStack.ReadModel;
using MfxBi.Models;

namespace MfxBi.Application
{
    public class DashboardService
    {
        public IRepository Repository { get; private set; }
        public IBus Bus { get; private set; }

        public DashboardService(IBus bus, IRepository repository)
        {
            Bus = bus;
            Repository = repository;
        }

        public DashboardViewModel GetDashboardViewModel()
        {
            var model = new DashboardViewModel();
            using (var db = new ProjectionManager())
            {
                model.PendingBookings = (from b in db.BookingSummaries select b).ToList();
                return model;
            }
        }

        public BookingDetailsViewModel GetRequestDetails(Guid requestId)
        {
            var booking = Repository.GetById<Booking>(requestId);
            var model = new BookingDetailsViewModel() { Booking = booking };
            return model;
        }

        //public void Approve(Guid requestId)
        //{
        //    var approved = new BankAccountApprovedEvent(requestId, "The Boss");

        //}
    }
}