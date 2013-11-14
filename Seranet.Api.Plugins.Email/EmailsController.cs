using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Seranet.Api.Plugins.Email
{
    public class EmailsController : ApiController
    {

        /// <summary> 
        /// Send a new mail
        /// </summary> 
        /// <param name="message">Detals of the email to be sent</param> 
        public HttpResponseMessage Post(Message message)
        {
            if (this.ModelState.IsValid)
            {

                SmtpClient client = new SmtpClient("mail.99xtechnology.com");
                client.Credentials = new NetworkCredential(message.From.UserName, message.From.Password);

                MailMessage m = new MailMessage();
                m.From = new MailAddress(message.From.UserName + "@99x.lk", message.From.DisplayName);
                foreach (string recipient in message.To)
                {
                    m.To.Add(new MailAddress(recipient));
                }

                m.Subject = message.Subject;
                m.Body = message.Body;
                m.IsBodyHtml = true;
                client.Send(m);

                // let's send entity created response, ofcourse without password
                message.From.Password = null;
                var response = Request.CreateResponse<Message>(HttpStatusCode.Created, message);
                return response;
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        } 

    }
}
