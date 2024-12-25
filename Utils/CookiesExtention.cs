﻿using System.Text.Json;

namespace BackEnd_Camping.Utils
{
    public static class CookiesExtention
    {
        public static void Append<T>(this IResponseCookies cookies, string key, T value, CookieOptions opt)
        {
            cookies.Append(key, JsonSerializer.Serialize(value), opt);
        }

        public static T? Get<T>(this IRequestCookieCollection cookies, string key)
        {
            var value = cookies[key];
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
