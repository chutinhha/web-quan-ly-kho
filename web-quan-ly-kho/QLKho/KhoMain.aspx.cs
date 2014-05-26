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

namespace QLCV.QLKho
{
    public partial class KhoMain : System.Web.UI.Page
    {
        public int a_action = 0;
         public DataTable _dtFunction;
        protected void Page_Load(object sender, EventArgs e)
        {
            int e_id = 0;
            if (Request["A_action"] == null)
            {
                a_action = 1;
                lblGroupName.Text = "Quản lý kho";                
            }
            else
            {
                a_action = Convert.ToInt32(Request["A_action"]);
                
                if (Request["E_Id"] != null) e_id = Convert.ToInt32(Request["E_Id"]);

                if (e_id == 0) lblGroupName.Text = "Thêm mới quyền Admin";
                else lblGroupName.Text = "Cập nhật quyền Admin";
            }     
            
            if (!IsPostBack)
            {
                // ---- KIEM TRA LOGIN -----
                if (Session["UserId"] == null)
                {
                    Response.Redirect("~/login.aspx");
                }
                // --- END DT LOGIN -----

               
            }
            
            _dtFunction = (new clsKhoMain()).Get_List_Menu(Session["UserId"].ToString());
        }

       

    }
}
