using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Seranet.Api.Plugins.Project
{
    public class ProjectsController: ApiController
    {
        //[Authorize(Users= @"SERANET\hasithy")]
        public IEnumerable<Project> Get()
        {
            string path = System.Environment.GetEnvironmentVariable("SERANET.API.PLUGINS.PROJECT.RESOURCEFILE");

            ResourcePlanReader reader = new ResourcePlanReader(path);
            return reader.ReadFile();

        }
    }
}
