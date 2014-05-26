using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCV.code.common
{
    public class ParamUti
    {
        public static int GetIntFromForm(string key, int defaultValue)
        {
            try { return int.Parse(HttpContext.Current.Request.Form[key]); }
            catch { return defaultValue; }
        }
        public static int GetIntFromForm(string key)
        {
            return GetIntFromForm(key, 0);
        }
        public static long GetLongFromForm(string key, long defaultValue)
        {
            try { return long.Parse(HttpContext.Current.Request.Form[key]); }
            catch { return defaultValue; }
        }
        public static long GetLongFromForm(string key)
        {
            return GetLongFromForm(key, 0);
        }
        public static bool GetBoolFromForm(string key, bool defaultValue)
        {
            try { return bool.Parse(HttpContext.Current.Request.Form[key]); }
            catch { return defaultValue; }
        }
        public static bool GetBoolFromForm(string key)
        {
            return GetBoolFromForm(key, false);
        }
        public static string GetStringFromForm(string key, string defaultValue)
        {
            try { return HttpContext.Current.Request.Form[key].Trim(); }
            catch { return string.Empty; }
        }
        public static string GetStringFromForm(string key)
        {
            return GetStringFromForm(key, string.Empty);
        }
        public static int GetIntFromQuery(string par)
        {
            return GetIntFromQuery(par, 0);
        }
        public static int GetIntFromQuery(string par, int defvalue)
        {
            try { return int.Parse(HttpContext.Current.Request.QueryString[par]); }
            catch { return defvalue; }
        }
        public static long GetLongFromQuery(string par, long defvalue)
        {
            try { return long.Parse(HttpContext.Current.Request.QueryString[par]); }
            catch { return defvalue; }
        }
        public static long GetLongFromQuery(string par)
        {
            return GetLongFromQuery(par, 0);
        }


        public static bool GetBoolFromQuery(string par, bool defvalue)
        {
            try { return bool.Parse(HttpContext.Current.Request.QueryString[par]); }
            catch { return defvalue; }
        }
        public static bool GetBoolFromQuery(string par)
        {
            return GetBoolFromQuery(par, true);
        }

        public static string GetStringFromQuery(string par)
        {
            return GetStringFromQuery(par, string.Empty);
        }
        public static string GetStringFromQuery(string par, string defvalue)
        {
            try { return HttpContext.Current.Request.QueryString[par].Trim(); }
            catch { return defvalue; }
        }
    }
}