using Mvc.Mailer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ptixiaki201.Models;
using Ptixiaki201.Mailers;



namespace Ptixiaki201.Mailers
{
    public class usermailer : MailerBase, Iusermailer
    {
        public usermailer()
        {
            MasterName = "_Layout";
        }

        public virtual MvcMailMessage orderFood()
        {
            MyDataContainer db = new MyDataContainer();

            var mailMessage = new MvcMailMessage { Subject = "Order" };

            var test = db.Order.Where(pro=>pro.Status=="foodorder").ToList();
            ViewData.Model = test;

            //ViewData = new ViewDataDictionary(model);


            mailMessage.To.Add("leon_christou@hotmail.com");
            PopulateBody(mailMessage, viewName: "Welcome");

            return mailMessage;

        }

        public virtual MvcMailMessage orderWine()
        {
            MyDataContainer db = new MyDataContainer();

            var mailMessage = new MvcMailMessage { Subject = "Order Wine" };

            var test = db.Order.Where(pro=>pro.Status=="wineorder").ToList();
            ViewData.Model = test;

            mailMessage.To.Add("leon_christou@hotmail.com");
            PopulateBody(mailMessage, viewName: "Order Wine");

            return mailMessage;
        
        }

        public virtual MvcMailMessage passwordreset()
        {
            ////ViewBag.Data = someObject;
            MyDataContainer db = new MyDataContainer();

            
            var test = db.Requests.OrderByDescending(id=>id.Id).Take(1).ToList();

            ViewData.Model = test;
            return Populate(x =>
            {
                x.Subject = "passwordreset";
                x.ViewName = "passwordreset";
                x.To.Add("some-email@example.com");
            });
        }
    }
}