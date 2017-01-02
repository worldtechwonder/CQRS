using System.Web.Mvc;
using MfxBi.Application;
using MfxBi.Models;

namespace MfxBi.Controllers
{
    public class BookingController : Controller
    {
        private readonly BookingService _service = new BookingService(MvcApplication.Bus);

        [HttpPost]
        public ActionResult Add(MakeReservationViewModel input, string connectionId)
        {
            _service.MakeReservation(input, connectionId);
            return RedirectToAction("index", "home");
        }

        [HttpPost]
        public ActionResult Move(ChangeReservationViewModel input, string connectionId)
        {
            // connectionId only if using SignalR (not here)
            _service.ChangeReservation(input, connectionId);
            return RedirectToAction("index", "home");
        }

        [HttpPost]
        public ActionResult Cancel(string bid)
        {
            _service.CancelReservation(bid);
            return Content("OK");
        }
    }
}