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

namespace QLCV.Permission
{
    public partial class SelectCompany : System.Web.UI.Page
    {
        public DataTable _dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int iUser = Session["UserId"] == null ? 0 : Convert.ToInt32(Session["UserId"].ToString());
                if (Request["A_Id"] == null)
                {                    
                    _dt = new cls_Catalog_Company().Select((short)1, iUser);

                    StringBuilder sb = new StringBuilder();

                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        sb.Append("<tr class='normalbold' style='background:#DDDDDD' height='23'>" +
                                  "      <td><input type='checkbox' id='chkPortalId_" + (i + 1).ToString() + "' name='chkPortalId_" + (i + 1).ToString() + "' value='" + _dt.Rows[i]["Id"].ToString() + ":" + _dt.Rows[i]["ComName"].ToString() + "'></td>" +
                                  "      <td><img src='../images/NhanSu/icon_portal.gif' border='0' align='absmiddle' hspace='2'>" + _dt.Rows[i]["ComName"].ToString() + "</td>" +
                                  "</tr>");
                    }
                    ltlListCompany.Text = sb.ToString();
                }
                else
                {
                    cls_Module_Authority_Employee aEmp = new cls_Module_Authority_Employee();
                    aEmp.EmployeeId = iUser;
                    aEmp.AuthorityId = Convert.ToInt16(Request["A_Id"]);

                    _dt = aEmp.GetCompanyByAuthorityId();
                    StringBuilder sb = new StringBuilder();

                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        sb.Append("<tr class='normalbold' style='background:#DDDDDD' height='23'>" +
                                  "      <td><input type='checkbox' id='chkPortalId_" + (i + 1).ToString() + "' name='chkPortalId_" + (i + 1).ToString() + "' value='" + _dt.Rows[i]["Id"].ToString() + ":" + _dt.Rows[i]["ComName"].ToString() + "'></td>" +
                                  "      <td><img src='../images/NhanSu/icon_portal.gif' border='0' align='absmiddle' hspace='2'>" + _dt.Rows[i]["ComName"].ToString() + "</td>" +
                                  "</tr>");
                    }
                    ltlListCompany.Text = sb.ToString();
                }
            }
        }
    }
}
