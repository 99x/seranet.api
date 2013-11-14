using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seranet.Api.Plugins.Email
{
    public class Message
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string[] To { get; set; }
        public Sender From { get; set; }
    }

    public class Sender {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
    }
}
