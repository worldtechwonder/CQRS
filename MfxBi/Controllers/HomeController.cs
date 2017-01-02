using System;
using System.Web.Mvc;
using MfxBi.Application;

namespace MfxBi.Controllers
{
    public class HomeController : Controller
    {
        private readonly HomeService _service = new HomeService();

        public ActionResult Index(DateTime? day)
        {
            var date = DateTime.Today;
            if (day.HasValue)
                date = day.Value.Date;

            var model = _service.GetHomeViewModel(date);
            return View(model);
        }
    }
}