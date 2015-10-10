using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ptixiaki201.Models;
using Ptixiaki201.Mailers;

namespace Ptixiaki201.Controllers
{
    public class MakeRequestController : Controller
    {
        private Iusermailer _usermailer = new usermailer();
        public Iusermailer UserMailer
        {
            get { return _usermailer; }
            set { _usermailer = value; }
        }
        //
        // GET: /MakeRequest/

        public ActionResult Index()
        {
            var model = new OrderModel { 
                Room = Session["Room"].ToString(),
                Email = Session["Email"].ToString()           
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FormCollection form)
        {
            

            using (var db = new MyDataContainer())
            {
                var obj = new Requests
                {
                    Date = DateTime.Now.Date,
                    RequestBody = form["RequestArea"],
                    Room = Convert.ToInt32(Session["Room"])
                };
                db.Requests.Add(obj);
                db.SaveChanges();
            }

            return RedirectToAction("MakeRequest");
        }

        public ActionResult MakeRequest()
        {
            UserMailer.passwordreset().SendAsync();
            return View();
        }

    }
}
