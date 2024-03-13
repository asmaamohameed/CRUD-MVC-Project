using Demo.DAL.Entites;
using System.Net;
using System.Net.Mail;

namespace Demo.PL.Helper
{
	public static class EmailSettings
	{
		public static void SendEmail(Email email)
		{
			var client = new SmtpClient("stmp.gmail.com", 587);

			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("asmaa.mo104@gmail.com", "axxusjalxqhvgtha");

			client.Send("asmaa.mo104@gmail.com",email.To, email.Title , email.Body);

		}
	}
}

