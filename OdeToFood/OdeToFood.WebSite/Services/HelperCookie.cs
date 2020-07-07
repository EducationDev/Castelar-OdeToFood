using System;
using System.Web;

namespace OdeToFood.WebSite.Services
{
    public static class HelperCookie
    {
        public static void StoreInCookie(string cookieName, string keyName, string value, DateTime? expirationDate)
        {
            HttpCookie cookie = HttpContext.Current.Response.Cookies[cookieName] ?? HttpContext.Current.Request.Cookies[cookieName];
            if (cookie == null)
                cookie = new HttpCookie(cookieName);

            if (!string.IsNullOrEmpty(keyName))
                cookie.Values.Set(keyName, value);
            else
                cookie.Value = value;
            if (expirationDate.HasValue)
                cookie.Expires = expirationDate.Value;

            HttpContext.Current.Response.Cookies.Set(cookie);
        }

        public static string GetFromCookie(string cookieName, string keyName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null)
            {
                string val = (!String.IsNullOrEmpty(keyName)) ? cookie[keyName] : cookie.Value;
                if (!String.IsNullOrEmpty(val)) 
                    return Uri.UnescapeDataString(val);
            }
            return null;
        }

        // Elimina un valor único de una cookie o la cookie completa (keyName is null)       
        public static void RemoveCookie(string cookieName, string keyName)
        {
            if (string.IsNullOrEmpty(keyName))
            {
                if (HttpContext.Current.Request.Cookies[cookieName] != null)
                {
                    HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
                    cookie.Expires = DateTime.UtcNow.AddYears(-1);                    
                    HttpContext.Current.Response.Cookies.Add(cookie);
                    HttpContext.Current.Request.Cookies.Remove(cookieName);
                }
            }
            else
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
                cookie.Values.Remove(keyName);                
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
        public static bool CookieExist(string cookieName, string keyName)
        {
            HttpCookieCollection cookies = HttpContext.Current.Request.Cookies;
            return (String.IsNullOrEmpty(keyName))
                ? cookies[cookieName] != null
                : cookies[cookieName] != null && cookies[cookieName][keyName] != null;
        }
    }
}