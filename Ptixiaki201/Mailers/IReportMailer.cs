using Mvc.Mailer;

namespace Ptixiaki201.Mailers
{ 
    public interface IReportMailer
    {
			MvcMailMessage ReportProduced();
			MvcMailMessage ReportSent();
			MvcMailMessage ReportLoading();
	}
}