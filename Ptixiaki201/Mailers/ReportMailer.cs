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
    public class ReportMailer : MailerBase, IReportMailer 	
	{
		public ReportMailer()
		{
			MasterName="_Layout";
		}
		
		public virtual MvcMailMessage ReportProduced()
		{
            MyDataContainer db = new MyDataContainer();

            var test = db.CallTAXI.OrderByDescending(id => id.Id).Take(1).ToList();
            ViewData.Model = test;
			return Populate(x =>
			{
				x.Subject = "ReportProduced";
				x.ViewName = "ReportProduced";
				x.To.Add("some-email@example.com");
			});
		}
 
		public virtual MvcMailMessage ReportSent()
		{
			//ViewBag.Data = someObject;
			return Populate(x =>
			{
				x.Subject = "ReportSent";
				x.ViewName = "ReportSent";
				x.To.Add("some-email@example.com");
			});
		}
 
		public virtual MvcMailMessage ReportLoading()
		{
			//ViewBag.Data = someObject;
			return Populate(x =>
			{
				x.Subject = "ReportLoading";
				x.ViewName = "ReportLoading";
				x.To.Add("some-email@example.com");
			});
		}
 	}
}