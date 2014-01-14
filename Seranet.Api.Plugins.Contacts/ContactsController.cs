using Seranet.Api.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Seranet.Api.Plugins.Contacts
{
    public class ContactsController: ApiController
    {
        /*public IEnumerable<Contact> Get()
        {
            return new List<Contact> { new Contact() { Organization="99X", 
                ContactList = new List<ContactItem>{new ContactItem(){Name = "Hasith", Email = "hasith@gmail.com", Phone = "0712223334", Skype = "HasithY"}} }};
        }*/
        //[SeranetAuth]
        public IEnumerable<Contact> Get()
        {
            string path = System.Environment.GetEnvironmentVariable("SERANET.API.PLUGINS.CONTACT.RESOURCEFILE");//("SERANET.API.PLUGINS.CONTACT.RESOURCEFILE");

            ResourcePlanReader reader = new ResourcePlanReader(path);
            return reader.ReadFile();

        }
    }
}
