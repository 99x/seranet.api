using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Seranet.Api.Core
{
    public class SeranetAuthAttribute : AuthorizeAttribute  
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            string path = actionContext.Request.RequestUri.LocalPath;
            string authorizedUsersString = ConfigurationManager.AppSettings[path.ToLower()];
            

            if (authorizedUsersString != null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var userArray = authorizedUsersString.Split(',');
                foreach (var user in userArray)
	            {
                    if (HttpContext.Current.User.Identity.Name.Trim().Equals(user.Trim(), StringComparison.CurrentCultureIgnoreCase)) 
                    {
                        return;
                    }
	            }

            }

            //if user is not found to be authorized, return unauthorized
            HandleUnauthorizedRequest(actionContext);
        }

    }
}
