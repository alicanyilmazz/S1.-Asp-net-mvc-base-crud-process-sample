using alican_yılmaz_come_back.Models;
using alican_yılmaz_come_back.Models.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace alican_yılmaz_come_back.Controllers
{
    public class AdresController : Controller
    {
        // GET: Adres
        [HttpGet]
        public ActionResult Yeni()
        {
            DatabaseContext db = new DatabaseContext();

            //Linq kullanarak yapımı yapımı

            List<SelectListItem> kisiler_List =
                (from kisi in db.kisiler.ToList()
                 select new SelectListItem()
                 {
                     Text = kisi.ad + " " + kisi.soyad,
                     Value = kisi.ID.ToString()

                 }).ToList();


            //Linq kullanmadan yapımı

            //List<kisiler> kisiler = db.kisiler.ToList();

            //List<SelectListItem> kisiler_List = new List<SelectListItem>();
            //foreach (kisiler kisi in kisiler)
            //{
            //    SelectListItem item = new SelectListItem();
            //    item.Text=kisi.ad +" "+kisi.soyad; //ekranda gözüken    
            //    item.Value = kisi.ID.ToString(); //arkada tutulan  
            //    kisiler_List.Add(item);
            //}



            TempData["kisiler"] = kisiler_List;
            ViewBag.kisiler = kisiler_List;


            return View();
        }


        [HttpPost]
        public ActionResult Yeni(adresler adres)
        {
            DatabaseContext db = new DatabaseContext();
            kisiler kisi = db.kisiler.Where(x => x.ID == adres.kisi.ID).FirstOrDefault();
            //FirstOrDefault(); ile buldugu ilk kayıdı getirir.Bulamazsa kayıdı "null" döner.
            //ToArray() ve ToList() ile de tüm bulduklarını array yada list olarak döner.Tabi sol tarafında List ve array olması gerektiğini unutma!
            if (kisi != null)
            {
                adres.kisi = kisi; //gelen adres'e ait kisiye database den buldugumuz kisi 'yi veriyoruz.

                db.adresler.Add(adres);
                int sonuc = db.SaveChanges();

                if (sonuc > 0)
                {
                    ViewBag.Result = "Adres kaydedilmistir.";
                    ViewBag.Status = "success";
                }

                else
                {
                    ViewBag.Result = "Adres kaydedilememiştir.";
                    ViewBag.Status = "danger";
                }
            }

            ViewBag.kisiler = TempData["kisiler"];
            return View();
        }

        [HttpGet]
        public ActionResult edit(int? adres_id)
        {
            adresler adres = null;

            if (adres_id != null)
            {
                DatabaseContext db = new DatabaseContext();

                List<SelectListItem> kisiler_List =
                    (from kisi in db.kisiler.ToList()
                     select new SelectListItem()
                     {
                         Text = kisi.ad + " " + kisi.soyad,
                         Value = kisi.ID.ToString()

                     }).ToList();


                TempData["kisiler"] = kisiler_List;
                ViewBag.kisiler = kisiler_List;

                 adres = db.adresler.Where(x => x.ID == adres_id).FirstOrDefault();


            }
            return View(adres);


        }


            
        [HttpPost]
        public ActionResult edit(adresler model,int? adres_id)  //hidden field kullanırsan int? adres_id parametresine gerek kalmaz.
        {
            DatabaseContext db = new DatabaseContext();
            kisiler kisi = db.kisiler.Where(x => x.ID == model.kisi.ID).FirstOrDefault();
            adresler adres = db.adresler.Where(x => x.ID == adres_id).FirstOrDefault();
            if (kisi != null)
            {
                adres.kisi = kisi;    //gelen adres'e ait kisiye database den buldugumuz kisi 'yi veriyoruz.
                adres.adres_tanim = model.adres_tanim;
                
                int sonuc = db.SaveChanges();

                if (sonuc > 0)
                {
                    ViewBag.Result = "Adres güncellenmiştir.";
                    ViewBag.Status = "success";
                }
                else
                {
                    ViewBag.Result = "Adres güncellenememiştir.";
                    ViewBag.Status = "danger";
                }
            }

            ViewBag.kisiler = TempData["kisiler"];

            return View();
        }

        [HttpGet]
        public ActionResult delete(int? adres_id )
        {
            adresler adres = null;

            if(adres_id!=null)
            {
                DatabaseContext db = new DatabaseContext();
                adres = db.adresler.Where(x => x.ID == adres_id).FirstOrDefault();
            }

            return View(adres);
        }


        [HttpPost, ActionName("delete")]
        public ActionResult delete_post(int? adres_id)
        {


            if (adres_id != null)
            {
                DatabaseContext db = new DatabaseContext();
         
               adresler adres = db.adresler.Where(x => x.ID == adres_id).FirstOrDefault();
                db.adresler.Remove(adres);

                db.SaveChanges();
            }

            return RedirectToAction("homepage", "Home");
        }


    }
}


// TempData direkt sayfaya gidemez!ViewBag ve ViewData sayfalarda kullanılabilir.
// TempData bir atımlık daha veri tasıyabilmek için actionlar arasında  