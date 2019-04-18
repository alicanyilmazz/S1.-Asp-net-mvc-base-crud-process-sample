using alican_yılmaz_come_back.Models;
using alican_yılmaz_come_back.Models.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace alican_yılmaz_come_back.Controllers
{
    public class KisiController : Controller
    {
        // GET: Kisi
        [HttpGet]
        public ActionResult yeni(bool cntrlr_cnt)
        {
           
            TempData["cntrl"] = cntrlr_cnt;
            return View();
        }

        [HttpPost]
        public ActionResult yeni(kisiler kisi)
        {   
          if(TempData["cntrl"]!=null)
            {
                DatabaseContext db = new DatabaseContext();
                db.kisiler.Add(kisi);

                ViewBag.Button_Status = true;
                int sonuc = db.SaveChanges();
                if (sonuc > 0)
                {
                    ViewBag.Result = "kisi kaydedilmistir.";
                    ViewBag.Status = "success";
                }

                else
                {
                    ViewBag.Result = "kisi kaydedilememiştir.";
                    ViewBag.Status = "danger";
                }

                return View();

            }
            return RedirectToAction("yeni", "Kisi",new { cntrlr_cnt=true });

        }

        [HttpGet]
        public ActionResult edit(int? kisi_id/*,string kisi_ad*/)
        {
            kisiler kisi = null;

            if (kisi_id!=null)
            {
                DatabaseContext db = new DatabaseContext();
                kisi = db.kisiler.Where(x => x.ID == kisi_id).FirstOrDefault();
            }

            return View(kisi);
        }

        [HttpPost]
        public ActionResult edit(kisiler model_ks, int? kisi_id)
        {
            DatabaseContext db = new DatabaseContext();
            kisiler kisi = db.kisiler.Where(x => x.ID == kisi_id).FirstOrDefault();

            if(kisi!=null)
            {
                kisi.ad = model_ks.ad;
                kisi.soyad = model_ks.soyad;
                kisi.yas = model_ks.yas;

                int sonuc = db.SaveChanges();
                if (sonuc > 0)
                {
                    ViewBag.Result = "kisi güncellenmiştir.";
                    ViewBag.Status = "success";
                }

                else
                {
                    ViewBag.Result = "kisi güncellenememiştir.";
                    ViewBag.Status = "danger";
                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult delete(int? kisi_id)
        {
            kisiler kisi = null;

            if(kisi_id!=null)
            {
                DatabaseContext db = new DatabaseContext();
                kisi = db.kisiler.Where(x => x.ID == kisi_id).FirstOrDefault();
            }

            return View(kisi);
        }

        [HttpPost,ActionName("delete")]
        public ActionResult delete_post(int? kisi_id)
        {
            

            if (kisi_id != null)
            {
                DatabaseContext db = new DatabaseContext();
                kisiler kisi=db.kisiler.Where(x => x.ID == kisi_id).FirstOrDefault();
                List<adresler> adres = db.adresler.Where(x => x.kisi.ID == kisi_id).ToList();
                db.kisiler.Remove(kisi);
                db.adresler.RemoveRange(adres);
               
                db.SaveChanges();
            }

            return RedirectToAction("homepage", "Home");
        }
    }
}