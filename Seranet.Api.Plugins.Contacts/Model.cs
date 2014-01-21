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
            this.ContactAssignment = new List<ContactAssignment>();
        }

        public string Organization { get; set; }
        public List<ContactAssignment> ContactAssignment { get; set; }
    }

    public class ContactAssignment
    {
        public ContactAssignment()
        {
            this.ContactList = new List<ContactItem>();
        }

        public string Assignment { get; set; }
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
