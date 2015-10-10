using Mvc.Mailer;

namespace Ptixiaki201.Mailers
{ 
    public interface Icommentmailer
    {
			MvcMailMessage commentposted();
			MvcMailMessage liked();
            //MvcMailMessage calltaxiNOW();
	}
}