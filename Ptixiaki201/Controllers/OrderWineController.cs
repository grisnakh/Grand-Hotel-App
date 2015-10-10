using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ptixiaki201.Mailers;

namespace Ptixiaki201.Controllers
{
    public class OrderWineController : Controller
    {
        //private Iusermailer _usermailer = new usermailer();
        //public Iusermailer UserMailer
        //{
        //    get { return _usermailer; }
        //    set { _usermailer = value; }
        //}
        //public ActionResult SendWelcomeMessage()
        //{
        //    UserMailer.orderFood().SendAsync();
        //    return View();
        //}

        private Icommentmailer _usermailer = new commentmailer();
        public Icommentmailer CommentMailer
        {
            get { return _usermailer; }
            set { _usermailer = value; }            
        }
        public ActionResult SendWineOrder()
        {
            CommentMailer.commentposted().SendAsync();
            return View();
        }

        public ActionResult Index()
        {
            using (var db = new MyDataContainer())
            {
                return View(db.OrderWineTab.ToList());
            }

        }
        public ActionResult Order(int id = 0)
        {
            using (var db = new MyDataContainer())
            {
                OrderWineTab orderwinetab = db.OrderWineTab.Find(id);
                if (orderwinetab == null)
                {
                    return HttpNotFound();
                }
                return View(orderwinetab);
            }
        }

        [HttpPost]
        public ActionResult Order(FormCollection form)
        {
            using (var db = new MyDataContainer())
            {
                var quanti = Convert.ToInt32(form["Quantity"]);
                var pric = Convert.ToDouble(form["overallPrice"]);
                var num = form["Quantity"];
                var newInt = 0;

                var isValid = Int32.TryParse(num, out newInt);
                if (!isValid)
                {
                    ModelState.AddModelError("Quantity", "Number is only accepted for order");
                }

                DateTime time = DateTime.Today;
                DateTime date = time.Date;

                var myobj = new Order
                {
                    Quantity = Convert.ToInt32(form["Quantity"]),
                    Title = (form["Title"]),
                    Category = (form["Category"]),
                    OverallPrice = Convert.ToDouble((pric * quanti)),
                    DateTime = date,
                    Status = "wineorder",
                    Room = Convert.ToInt32(Session["Room"]),
                    Ordered = "Unordered"
                };

                db.Order.Add(myobj);            

                db.SaveChanges();

                return RedirectToAction("Index");
            }

        }
        public ActionResult CheckOut()
        {
            using (var db = new MyDataContainer())
            {
                 var room = Convert.ToInt32(Session["Room"]);;
                var x = db.Order.Where(pro=>pro.Status=="wineorder" && pro.Room == room ).ToList();

                return View(x);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(FormCollection form)
        {
            using (var db = new MyDataContainer())
            {
                var rowtoupdate = db.Order.Where(pro => pro.Ordered == "Unordered").ToList();

                foreach (var item in rowtoupdate)
                {
                    item.Ordered = form["Ordered"];
                }
                db.SaveChanges();

                return RedirectToAction("SendWineOrder");
            }
        }

        [HttpPost]
        public ActionResult DeleteOrder() 
        {
            using (var db = new MyDataContainer())
            {
                var room = Convert.ToInt32(Session["Room"]);
                var rowstodelete = db.Order.Where(pro => pro.Room == room && pro.Status == "wineorder").ToList();
                foreach (var item in rowstodelete)
                {
                    db.Order.Remove(item);
                }
                db.SaveChanges();

                return RedirectToAction("index");
            }
        }

        public ActionResult Comments(int id = 0)
        {
            using (var db = new MyDataContainer())
            {
                return View(db.Order.Find(id));
            }
        }

        [HttpPost]
        public ActionResult Comments(FormCollection form)
        {
            using (var db = new MyDataContainer())
            {
                var id = Convert.ToInt32(form["id"]);

                var rowtoupdate = db.Order.Where(pro => pro.Id == id).ToList();

                foreach (var item in rowtoupdate)
                {
                    item.Comments = form["Comments"];
                }
                db.SaveChanges();

                return RedirectToAction("Checkout");
            }
        }
        //[HttpPost]
        //public ActionResult Comments(FormCollection form)
        //{
        //    using (var db = new MyDataContainer())
        //    {
        //        var id = Convert.ToInt32(form["id"]);

        //        var rowtoupdate = db.Order.Where(pro => pro.Id == id).ToList();
        //        foreach (var item in rowtoupdate)
        //        {
        //            item.Comments = form["Comments"];
        //        }
        //        db.SaveChanges();

        //        return RedirectToAction("CheckOut");
        //    }
        //}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var db = new MyDataContainer())
            {
                Order order = db.Order.Find(id);
                db.Order.Remove(order);
                db.SaveChanges();
                return RedirectToAction("CheckOut");
            }
        }

    }
}
