using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ptixiaki201.Controllers
{
    public class StartController : Controller
    {
        //
        // GET: /Start/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Account()
        {
            using (var db = new MyDataContainer())
            {
                var room = Convert.ToInt32(Session["Room"]);
                return View(db.Users.Where(i => i.Room == room).FirstOrDefault());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Account(FormCollection form)
        {

            var newemail = form["newemailcontroller"];
            var newpass = form["newpasswordcontroller"];

            var room = Convert.ToInt32(Session["Room"]);
            using (var db = new MyDataContainer())
            {
                var rowtoupdate = db.Users.Where(pro => pro.Room == room).ToList();

                foreach (var i in rowtoupdate)
                {
                    i.Email = newemail;
                    i.Password = newpass;

                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

    }
}
