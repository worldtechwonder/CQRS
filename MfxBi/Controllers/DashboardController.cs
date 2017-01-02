using System;
using System.Web.Mvc;
using MfxBi.Application;

namespace MfxBi.Controllers
{
    public class DashboardController : Controller
    {
        private readonly DashboardService _service;  

        public DashboardController()
        {
            _service = new DashboardService(MvcApplication.Bus, MvcApplication.AggregateRepository);
        }

        public ActionResult Index()
        {
            var model = _service.GetDashboardViewModel();
            return View(model);
        }

        public ActionResult More([Bind(Prefix="id")] string requestId)
        {
            var guid = Guid.Parse(requestId);
            var model = _service.GetRequestDetails(guid);
            return View(model);
        }

        //public ActionResult Approve([Bind(Prefix = "id")] string requestId)
        //{
        //    var guid = Guid.Parse(requestId);
        //    _service.Approve(guid);
        //    return RedirectToAction("index");
        //}
    }
}