using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [CompatibleFileResultAttribute]
        public ActionResult File(string name)
        {
            var fileName = Server.MapPath(@"~/App_Data/sample.txt");
            return File(fileName, "application/octet-stream", name);
        }
    }
}