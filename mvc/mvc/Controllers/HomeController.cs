using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mvc.Models;
using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace mvc.Controllers
{
    public class HomeController : Controller
    {
        static int[] ids = new int[2];

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet ]  
        public IActionResult Index()
        {
            Baglanti connect = new Baglanti();
            string sorgu = "select aracid from k_arac where kid='" + Kullanici.kid + "'";
            ids = connect.Sql_arac(sorgu);
            Vehicles v = new Vehicles();
            var araclar = v.Nosql_islemleri(ids,0);
            return View(araclar);
        }
        [HttpPost]
        public ActionResult Index(IFormCollection frm)
        {
            string tarih = Request.Form["tarih"];
            string aracno = frm["arac"].ToString();
            Vehicles v = new Vehicles(); 
            var araclar = v.Nosql_islemleri(ids, 3);
            if (tarih.Equals(""))
            {
                if (aracno.Equals("1"))
                {
                    araclar = v.Nosql_islemleri(ids, 1);
                    return View(araclar);
                }
                else if (aracno.Equals("2"))
                {
                    araclar = v.Nosql_islemleri(ids, 2);
                    return View(araclar);
                }
                else if (aracno.Equals("3"))
                {
                    araclar = v.Nosql_islemleri(ids, 3);
                    return View(araclar);
                }
                else
                {
                    return View(araclar);
                }
            }
            else
            {
                string sayi = Request.Form["arac"];
                DateTime itarih = Convert.ToDateTime(Request.Form["tarih"]);
                DateTime starih = Convert.ToDateTime(Request.Form["saat"]);
                if (sayi.Equals("1"))
                {
                    araclar = v.Aralik_alma(ids, 1, itarih, starih);
                    return View(araclar);
                }
                else if (sayi.Equals("2"))
                {
                    araclar = v.Aralik_alma(ids, 2, itarih, starih);
                    return View(araclar);
                }
                else
                {
                    araclar = v.Aralik_alma(ids, 0, itarih, starih);
                    return View(araclar);
                }
            }
            
        }
        
        [HttpPost]
        public ActionResult Cikis(string id)
        {
            DateTime date = new DateTime();
            DateTime saat = DateTime.Now;
            string gsaat = Convert.ToString(saat);
            Baglanti connect = new Baglanti();

            string sorgu = "update kullanici_zaman set cıkıs='"+gsaat+"' where k_id='"+Kullanici.kid+"'";
             connect.Sql_sorgu(sorgu);   
            ViewBag.Message = "başarılı";

            return RedirectToAction("Index","Log");

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
