using Mvc.Mailer;

namespace Ptixiaki201.Mailers
{ 
    public interface Iusermailer
    {
			MvcMailMessage orderFood();
			MvcMailMessage passwordreset();            
	}
}