using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seranet.Api.Plugins.Contacts
{
    public class Contact
    {
        public Contact()
        {
            this.ContactList = new List<ContactItem>();
        }

        public string Organization { get; set; }
        public List<ContactItem> ContactList { get; set; }
    }

    public class ContactItem
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Skype { get; set; }
    }
}
