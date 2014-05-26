using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Nhansu_Lib.DB_Class;
using System.Text;
using System.Data;

namespace QLCV.Permission
{
    public partial class SelectDepartment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int iUser = Session["UserId"] == null ? 0 : Convert.ToInt32(Session["UserId"].ToString());
                if (Request["A_Id"] != null)
                {
                    cls_Module_Authority_Employee aEmp = new cls_Module_Authority_Employee();
                    aEmp.EmployeeId = iUser;
                    aEmp.AuthorityId = Convert.ToInt16(Request["A_Id"]);

                    DataTable _dt = aEmp.GetCompanyByAuthorityId();
                    DataTable dtDepartment = aEmp.GetDepartmentByAuthorityId();

                    StringBuilder sb = new StringBuilder();

                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        sb.Append("<TABLE WIDTH='520' align='center' BORDER='0' CELLSPACING='1' CELLPADDING='0'>" +
                                    "<tr class='PortalTR'><td colspan='2' height='26'>" +
                                    "   <img src='../images/NhanSu/icon_portal.gif' border='0' align='absmiddle' hspace='2'><b>"+_dt.Rows[i]["ComName"].ToString()+"</b></td></tr><tr><td colspan='2' bgcolor='#CCCCCC'></td></tr>" +
                                    "<tr><td colspan='2' bgcolor='gray'></td></tr>");

                        DataRow[] _row = dtDepartment.Select("CompanyId=" + _dt.Rows[i]["Id"].ToString());
                        if (_row.Count() > 0)
                        {
                            for(int j=0; j< _row.Count(); j++)
                            {
                                sb.Append("<tr bgcolor='#DDDDDD' ><td width='20'><SPAN CLASS='Normal' STYLE='width:20;overflow:hidden;'>" +
                                            "<INPUT  id='chkGroupId' type=\"checkbox\"  name='chkGroupId' value='" + _row[j]["Id"].ToString() + ":" + _row[j]["DepartName"].ToString() + "'></SPAN></td>" +
                                            "<td width='450' style='margin-left: 5' NOWRAP ONCLICK=\"flipFlop('SessionP" + _row[j]["Id"].ToString() + ":G" + _row[j]["Id"].ToString() + "')\">" +
                                            "<img src=\"../images/Treeview/ix_book.gif\" align='absmiddle'><span class='NormalBold'> " + _row[j]["DepartName"].ToString() + "</span>" +
                                            "<input type='hidden' name='hddChildCountSessionP" + _row[j]["Id"].ToString() + ":G" + _row[j]["Id"].ToString() + "'  Id='hddChildCountSessionP" + _row[j]["Id"].ToString() + ":G" + _row[j]["Id"].ToString() + "' value='0'></td></tr>");
                            }
                        }

                        sb.Append("</TABLE>"); 
                    }
                    ltlListDepartment.Text = sb.ToString();
                }
            }
        }
    }
}
