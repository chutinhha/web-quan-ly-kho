using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Nhansu_Lib.DB_Class;
using System.Text;
using System.Collections.Generic;
using QLCV.code.common;
using System.Threading;

namespace QLCV.PageSystem
{
    public partial class AccList : System.Web.UI.Page
    {
        public int a_action = 0;
         public DataTable _dtFunction;
        protected void Page_Load(object sender, EventArgs e)
       {
           Session["UserId"] = 56;
     
           if (!IsPostBack)
           {
               // ---- KIEM TRA LOGIN -----
               if (Session["UserId"] == null)
               {
                   Response.Redirect("~/login.aspx");
               }
               // --- END DT LOGIN -----
               Load_Data(0);
           }
           _dtFunction = (new clsSystem()).Get_System_Menu(Session["UserId"].ToString());
           if (ASPxCallbackPanel1.IsCallback)
           {
               // Intentionally pauses server-side processing, 
               // to demonstrate the Loading Panel functionality.
               Thread.Sleep(500);

             

               //  createColumnsGridView();
               Load_Data(0);

           }
        }

        private void Load_Data(int icate)
        {
            DataTable _dt = new cls_UserAccount().Select(icate);
            gridAccount.DataSource = _dt;
            gridAccount.DataBind();

        }
    }
}
