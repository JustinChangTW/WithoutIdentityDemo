using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WithoutIdentityDemo.Authorizations
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class SessionAuthorizeAttribute:Attribute
    {
 
    }
}
