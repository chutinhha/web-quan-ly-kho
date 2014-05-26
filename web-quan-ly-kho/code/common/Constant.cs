using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace QLCV.code.common
{
    public sealed class Constant
    {
        public static string DOC_PATH = ConfigurationManager.AppSettings["DOC_PATH"];
        public static string Incoming_Foder = ConfigurationManager.AppSettings["Incoming_Foder"];
        public static string Outcoming_Foder = ConfigurationManager.AppSettings["Outcoming_Foder"];
        public static string Work_Foder = ConfigurationManager.AppSettings["Work_Foder"];
        public static string Work_Foder_Read = ConfigurationManager.AppSettings["Work_Foder_Read"];
        public static string CRM_Foder = ConfigurationManager.AppSettings["CRM_Foder"];
        public static string DOC_URL = ConfigurationManager.AppSettings["DOC_URL"];
        public static string ROOT_PATH = ConfigurationManager.AppSettings["ROOT_PATH"];
    
        public static string IMG_PATH = ROOT_PATH + "images";
        public static string JS_PATH = ROOT_PATH + "js";
        public static string PRJ_ROOT = ConfigurationManager.AppSettings["PRJ_ROOT"];
        public static string COOKIESID = ConfigurationManager.AppSettings["COOKIESID"];
        public static string WEBSITE_TITLE = ConfigurationManager.AppSettings["WEBSITE_TITLE"];
        public static string _CONNECTION_STRING = ConfigurationManager.ConnectionStrings["SQLConn"].ConnectionString;

        public static string LINK_HEALTHFILE = ConfigurationManager.AppSettings["LINK_HEALTHFILE"];
        public static string DIR_HEALTHFILE = ConfigurationManager.AppSettings["DIR_HEALTHFILE"];
        public static string LINK_EDUCATIONFILE = ConfigurationManager.AppSettings["LINK_EDUCATIONFILE"];
        public static string DIR_EDUCATIONFILE = ConfigurationManager.AppSettings["DIR_EDUCATIONFILE"];
        public static string LINK_WORKFILE = ConfigurationManager.AppSettings["LINK_WORKFILE"];
        public static string DIR_WORKFILE = ConfigurationManager.AppSettings["DIR_WORKFILE"];
        public static string LINK_AWARDFILE = ConfigurationManager.AppSettings["LINK_AWARDFILE"];
        public static string DIR_AWARDFILE = ConfigurationManager.AppSettings["DIR_AWARDFILE"];
        public static string LINK_EXPANDFILE = ConfigurationManager.AppSettings["LINK_EXPANDFILE"];
        public static string DIR_EXPANDFILE = ConfigurationManager.AppSettings["DIR_EXPANDFILE"];

        // SESSION
        public static string SESSION_USERID = "UserId";
        public static string SESSION_FULLNAME = "FullName";
        public static string CONST_KEY_ENCRYPTION = "1f2b0a04614dbec42bb4dc68279343ad";
        public static string EVENT_EMAIL_TEMP_PATH = "~/EventCalendar/EventEmailTemplate.xml";

        //public static string _CONNECTION_STRING = getConnStr("CONNECTION_STRING");
        public static string getConnStr(string Name)
        {
            RijndaelEnhanced rijndaelKey = new RijndaelEnhanced("CDR", "@1B2c3D4e5F6g7H8");
            return rijndaelKey.Decrypt("J0/HDHjRC+z1MzutidYOtcOXlINZ4zcLb4F9wvHuWqos5rjdo1FrhYp8ltTX3EpH");
            //ConfigurationManager.ConnectionStrings[Name].ConnectionString
        }
        public static string GetMess(string mess)
        {
            string tmp;
            tmp = "<script type=\"text/javascript\">";
            tmp += "alert('" + mess + "');";
            tmp += "</script>";
            return tmp;


        }
    }
    public static class Alert
    {

        /// <summary>
        /// Shows a client-side JavaScript alert in the browser.
        /// </summary>
        /// <param name="message">The message to appear in the alert.</param>
        public static void Show(string message)
{
   // Cleans the message to allow single quotation marks
   string cleanMessage = message.Replace("'", "\'");
   string script = "<script type=\"text/javascript\">alert('" + cleanMessage + "');</script>";

   // Gets the executing web page
   Page page = HttpContext.Current.CurrentHandler as Page;

   // Checks if the handler is a Page and that the script isn't allready on the Page
   if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
   {
     page.ClientScript.RegisterClientScriptBlock(typeof(Alert), "alert", script);
   }
}
    } 
}
