using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ptixiaki201.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RegisterClient()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterClient(FormCollection form)
        {
            var newInt = 0;
            var roomint = form["ClientRoom"];

            var email = form["ClientEmail"];
            //var room = Convert.ToInt32(form["ClientRoom"]);
            var password = form["ClientPassword"];
            var name = form["ClientName"];

            var isValid = Int32.TryParse(roomint, out newInt);
            if (!isValid)
            {
                ModelState.AddModelError("Room", "Number is only accepted!");
            }
            else
            {

                using (var db = new MyDataContainer())
                {
                    var myobj = new Users
                    {
                        Email = email,
                        Room = Convert.ToInt32(form["ClientRoom"]),
                        Password = password,
                        Name = name,
                        DateofRegistration = Convert.ToDateTime(DateTime.Now.ToShortDateString())
                    };
                    db.Users.Add(myobj);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

            }
        }
        public ActionResult AdminLogin()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminLogin(FormCollection form)
        {
            var username = form["username"];
            var password = form["password"];

            using (var db = new MyDataContainer())
            {
                bool exists = db.Administrator.Where(pro => pro.username == username && pro.password == password).Any();

                if (exists)
                {
                    return RedirectToAction("Index");
                }

                return View("AdminLogin");
            }

        }
        public ActionResult ShowClientList()
        {
            using (var db = new MyDataContainer())
            {
                return View(db.Users.ToList());
            }
        }
        public ActionResult DeleteClient()
        {
            using (var db = new MyDataContainer())
            {
                return View(db.Users.ToList());
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var db = new MyDataContainer())
            {
                var user = db.Users.Find(id);
                
                db.Users.Remove(user);
                db.SaveChanges();
                return RedirectToAction("DeleteClient");
            }
        }
    }
}
