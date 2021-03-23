using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WithoutIdentityDemo.Authorizations;
using WithoutIdentityDemo.Extensions;
using WithoutIdentityDemo.Models;

namespace WithoutIdentityDemo.Middleware
{
    public class SessionAuthorizeMiddleware
    {
        private readonly RequestDelegate _next;
        private string _message;

        public SessionAuthorizeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //1. 取得有所有的Meatdata
            var metaData = context.GetEndpoint().Metadata;

            //2. 是否有使用SessionAuthorizeAttribute
            var hasSessionAuthorizeAttribute = metaData.Any(x => x.ToString().EndsWith(nameof(SessionAuthorizeAttribute)));

            //3. 沒使用不判斷
            if (!hasSessionAuthorizeAttribute)
            {
                await _next(context);
                return;
            }


            //4. 判斷權角色是否是管理者
            var userInfo = context.Session.Get<UserInfo>();
            if (userInfo != null && userInfo.Role == "administrator")
            {
                await _next(context);
                return;
            }

            //5. 如果未登入，則回應JSON
            if (userInfo?.Name == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                _message = "{\"code\":403,\"message\":\"無訪問權限 Forbidden\"}";
                context.Response.ContentType = "application/json";
                await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(_message), 0, Encoding.UTF8.GetBytes(_message).Length);
                return;
            }

            //6. 如果不被授權
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            _message = "{\"code\":401,\"message\":\"需要授權 Forbidden\"}";
            context.Response.ContentType = "application/json";
            await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(_message), 0, Encoding.UTF8.GetBytes(_message).Length);

        }
    }
}
