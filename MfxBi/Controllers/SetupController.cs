using System.Web.Mvc;
using MfxBi.Cqrs.QueryStack.Services;

namespace MfxBi.Controllers
{
    public class SetupController : Controller
    {
        public ActionResult Room()
        {
            //var command = new AddRoomCommand("MAIN", "Main meeting room");
            //MvcApplication.Bus.Send(command);

            RoomHelpers.Add("MAIN meeting room");
            return RedirectToAction("index", "home");
        }
    }
}