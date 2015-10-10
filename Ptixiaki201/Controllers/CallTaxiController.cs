using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ptixiaki201.Models;
using Ptixiaki201.Mailers;

namespace Ptixiaki201.Controllers
{
    public class CallTaxiController : Controller
    {
        //
        // GET: /CallTaxi/
        private IReportMailer _reportmailer = new ReportMailer();
        private Icommentmailer _usermailer = new commentmailer();
        public IReportMailer ReportMailer
        {
            get { return _reportmailer; }
            set { _reportmailer = value; }
        }
        public Icommentmailer CommentMailer
        {
            get { return _usermailer; }
            set { _usermailer = value; }
        }

        public ActionResult EmailTaxiNOW()
        {
            CommentMailer.liked().SendAsync();
            return View();
        }
        public ActionResult EmailTaxiLater()
        {
            ReportMailer.ReportProduced().SendAsync();
            return View();
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CallTaxiNow()
        {

            return PartialView();
        }

        [HttpPost]
        public ActionResult CallTaxiNow(FormCollection form)
        {
            var numofcars = Convert.ToInt32(form["numberofcars"]);
            var time = Convert.ToDateTime(DateTime.Now.ToShortTimeString());            

            var room = Convert.ToInt32(Session["Room"]);

            using (var db = new MyDataContainer())
            {
                var myobj = new CallTAXI
                {
                    NumOfCars = numofcars,
                    Time = time,
                    Room = room
                };
                db.CallTAXI.Add(myobj);
                db.SaveChanges();

                return RedirectToAction("EmailTaxiNOW");
            }

        }
        [HttpPost]
        public ActionResult CallTaxiLater(FormCollection form)
        {
            var numofcars = Convert.ToInt32(form["numberofcars"]);
            var time = Convert.ToDateTime(form["time"]);
            var room = Convert.ToInt32(Session["Room"]);

            //DateTime newInt;
            //var tim = form["time"];
            //var isValid = DateTime.TryParse(tim, out newInt);
            //if (!isValid)
            //{
            //    ModelState.AddModelError(tim, "Wrong time input!");
            //}
            //else
            //{
                using (var db = new MyDataContainer())
                {
                    var myobj = new CallTAXI
                    {
                        NumOfCars = numofcars,
                        Time = time,
                        Room = room
                    };
                    db.CallTAXI.Add(myobj);
                    db.SaveChanges();

                    return RedirectToAction("EmailTaxiLater");
                }
            
        }

        public ActionResult CallTaxiLater()
        {
            return PartialView();
        }

    }
}
