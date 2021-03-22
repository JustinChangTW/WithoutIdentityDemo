using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace WithoutIdentityDemo.Extensions
{
    public static class SessionExtension
    {
        const string KEY= "SESSION";
        public static void Set<T>(this ISession session,T Data)
        {
            session.SetString(KEY, JsonSerializer.Serialize(Data));
        }

        public static T Get<T>(this ISession session)where T:new ()
        {
            var data = session.GetString(KEY);
            return data!=null?JsonSerializer.Deserialize<T>(data):new T();
        }
    }
}
