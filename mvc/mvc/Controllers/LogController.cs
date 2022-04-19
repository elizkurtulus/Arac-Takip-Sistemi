using Microsoft.AspNetCore.Mvc;
using mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Controllers
{
    public class LogController : Controller
    {
        static int sayac = 0;

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
       [HttpPost]
        public ActionResult Index(string username,string pass)
        {
            DateTime date = new DateTime();
            Kullanici.saat= DateTime.Now;
            string gsaat = Convert.ToString(Kullanici.saat);
            int id;
            var ad = Request.Form["username"];
            var sifre = Request.Form["pass"];
            Baglanti connect = new Baglanti();
            string sorgu = "select kullaniciadi,sifre from kullanici where kullaniciadi='" + ad+ "' and sifre='" + sifre + "'";
            bool giris;
            giris = connect.Sql_ara(sorgu);
            if (giris == true)
            {
                sorgu = "select id from kullanici where kullaniciadi='" + ad + "' and sifre='" + sifre + "'";
                id = connect.Sql_ara1(sorgu);
                Kullanici.kid = id;
                sorgu = "insert into kullanici_zaman(k_id,giris,cıkıs) values('" + id + "','" + gsaat + "','NULL')";
                connect.Sql_sorgu(sorgu);
                ViewBag.Message = "başarılı";

               return Redirect("Home/Index");

            }
            else
            {
                
                sayac += 1;

                if (sayac >= 3)
                {


                    ViewBag.Message = "Kullanici 3 ve 3ten fazla hatali giris yapamaz";

                    return View();
                }
                else
                {
                    return Redirect("/");
                }
                
            }
        }
    }
}
