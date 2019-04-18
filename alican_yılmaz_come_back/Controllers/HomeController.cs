using alican_yılmaz_come_back.Models;
using alican_yılmaz_come_back.Models.Managers;
using alican_yılmaz_come_back.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace alican_yılmaz_come_back.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult homepage()
        {
            DatabaseContext db = new DatabaseContext();
            List<kisiler> kisiler_listesi =db.kisiler.ToList(); //select * from kisiler //aynı zamanda database olusumunu tetikler bu select islemi

            HomePageViewModel model = new HomePageViewModel();
            model.kisiler = db.kisiler.ToList(); //database den liste şeklinde kişileri döner
            model.adresler = db.adresler.ToList(); //database den liste şeklinde adresleri döner


            return View(model);
        }

    }
}