using System.Security.Principal;
using System.DirectoryServices;
﻿using System;

namespace Seranet.Api.Authentication
{
    public class DummyPrincipalProvider : IProvidePrincipal
    {
        public IPrincipal CreatePrincipal(string username, string password)
        {
            bool authenticated = false;

            try
            {
                DirectoryEntry entry = new DirectoryEntry("LDAP://" + "99xtad001.seranet.local", username, password);
                object nativeObject = entry.NativeObject;
                authenticated = true;
            }
            catch (DirectoryServicesCOMException cex)
            {
                //not authenticated; reason why is in cex
            }
            catch (Exception ex)
            {
                //not authenticated due to some other exception [this is optional]
            }

            if (!authenticated)
            {
                return null;
            }

            var identity = new GenericIdentity(username);
            IPrincipal principal = new GenericPrincipal(identity, new[] { "User" });
            return principal;
        }
    }
}