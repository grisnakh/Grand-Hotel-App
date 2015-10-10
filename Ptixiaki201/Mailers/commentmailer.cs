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
    public class commentmailer : MailerBase, Icommentmailer 	
	{
		public commentmailer()
		{
			MasterName="_Layout";
		}
		
		public virtual MvcMailMessage commentposted()
		{
			//ViewBag.Data = someObject;
            MyDataContainer db = new MyDataContainer();
            var test = db.Order.Where(pro => pro.Status == "wineorder").ToList();

            ViewData.Model = test;
			return Populate(x =>
			{
                
				x.Subject = "WineOrder";
				x.ViewName = "commentposted";
				x.To.Add("some-email@example.com");
			});
		}
 
		public virtual MvcMailMessage liked()
		{
			//ViewBag.Data = someObject;
            MyDataContainer db = new MyDataContainer();

            var test = db.CallTAXI.OrderByDescending(id => id.Id).Take(1).ToList();

            ViewData.Model = test;
			return Populate(x =>
			{
				x.Subject = "liked";
				x.ViewName = "liked";
				x.To.Add("some-email@example.com");
			});
		}
 	}
}