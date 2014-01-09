using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seranet.Api.Plugins.Project
{
    public class Project
    {

        public Project() {
            this.Members = new List<string>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Rep { get; set; }
        public List<string> Members { get; set; }
    }
}
