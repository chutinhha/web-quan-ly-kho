using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nhansu_Lib.DB_Class;
using System.Collections.Generic;
using System.Data;

namespace QLCV.N_Su
{
    public partial class CEmployee_List_Permission : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                short comId;
                if (Request["ComId"] != null)
                    comId = Convert.ToInt16(Request["ComId"]);
                else comId = (short)0;

                string str1, str2;
                str2 = "";
                str1 = "";

                cls_PersonalInformaiton pInfor = new cls_PersonalInformaiton();
                pInfor.RootId = 0;
                DataTable dtEmp = pInfor.SelectForPermission(comId);
                if (dtEmp.Rows.Count > 0)
                {
                    str1 = "var majorarray=new Array(";
                    str2 = "var majornamearray=new Array(";
                    for (int i = 0; i < dtEmp.Rows.Count; i++)
                    {
                        str1 += "'" + dtEmp.Rows[i]["EmployeeName"].ToString() + ", \\\"" + dtEmp.Rows[i]["ExtendInfor"].ToString() + "\\\"',";
                        str2 += "'" + dtEmp.Rows[i]["EmployeeName"].ToString() + "- " + dtEmp.Rows[i]["Email"].ToString() + "',";
                    }
                    str1 = str1.Substring(0, str1.Length - 1);
                    str2 = str2.Substring(0, str2.Length - 1);
                    str1 += ");";
                    str2 += ");";
                }
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();

                Response.ContentType = "text/plain";

                //Write the message
                Response.Write(str1);
                Response.Write("\n");
                Response.Write(str2);
                //End the response causing it to be sent
                Response.End();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
