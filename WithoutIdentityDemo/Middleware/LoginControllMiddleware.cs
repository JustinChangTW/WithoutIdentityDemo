using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WithoutIdentityDemo.Extensions;

namespace WithoutIdentityDemo.Middleware
{
    public class LoginControllMiddleware
    {
        private readonly RequestDelegate _next;

        public LoginControllMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            if (endpoint != null) { 
                var metadata = endpoint.Metadata;

                //1. 取得路徑資訊
                ControllerActionDescriptor controllerAction = (ControllerActionDescriptor)metadata.SingleOrDefault(x => x.ToString() == "Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor");
                if (controllerAction.ControllerName != "Account")
                {
                    var value = context.Request.Cookies.SingleOrDefault(x => x.Key == "auth_token").Value;
                    var decode = value.ToDecode();

                    // 2. 如果是空的或解碼失敗則導回登入頁面
                    if (String.IsNullOrEmpty(decode))
                    {
                        context.Response.Redirect("/Account/Index", false);
                        return;
                    }
     
                }
            }
            await _next(context);

        }
    }
}
