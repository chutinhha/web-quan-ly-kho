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
using Telerik.Web.UI;
using System.Text;
using QLCV.code.common;

namespace QLCV.N_Su
{
    public partial class View_Log_Login : System.Web.UI.Page
    {
        private cls_Report_Group objReport = new cls_Report_Group();
        private string empAuthorityCode = "";
        protected void Page_Load(object sender, EventArgs e)
        {
           
            // ---- KIEM TRA LOGIN -----
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/login.aspx");
            }
            // ---- END KT LOGIN -------

            bindAuthority();
           
            if (!IsPostBack)
            {
                if (empAuthorityCode.Length > 0)
                {
                    dtFrom.SelectedDate = DateTime.Now;
                    dtTo.SelectedDate = DateTime.Now;
                    rcbDepartment_Bind();
                }
                else
                    Response.Redirect("~/ErrorPermission.aspx");
            }

        }

        #region bindAuthority
        private void bindAuthority()
        {
            try
            {
                bool isExist = false;
                string moduleCode = ConfigurationSettings.AppSettings["Emp_Module"];
                int userId = Convert.ToInt32(Session["UserId"]);
                cls_Module_Authority_Employee aEmp = new cls_Module_Authority_Employee();
                aEmp.EmployeeId = userId;

                DataTable dt = aEmp.SelectAuthority(moduleCode);
                StringBuilder sb = new StringBuilder();
                sb.Append("<tr>" +
                          "  <td background=\"../images/NhanSu/MenuTab_Bg.gif\" height=\"40\" valign=\"bottom\" colspan=\"3\">" +
                          "  <link href=\"../CSS/Modules/Nhan_Su/TabMenu.css\" rel=\"stylesheet\" type=\"text/css\" />" +
                          "  <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">" +
                          "  <tbody><tr><td background=\"../images/NhanSu/tbar_lcap_on.gif\" width=\"5\"><img src=\"../images/1x1.gif\" border=\"0\" height=\"1\" width=\"5\"></td>" +
                          "  <td align=\"right\" background=\"../images/NhanSu/tbar_bg_on.gif\" nowrap=\"nowrap\" valign=\"middle\" width=\"190\"><img src=\"../images/1x1.gif\" border=\"0\" height=\"1\" width=\"188\"></td>" +
                          "  <td background=\"../images/NhanSu/tbar_bg_on.gif\" valign=\"middle\">" +
                          "  <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tbody><tr>" +
                          "  <td id=\"tspacer\"><img src=\"../images/1x1.gif\" height=\"1\" width=\"1\"></td>");

                sb.Append("<td class=\"ltab_off\"><img src=\"../images/1x1.gif\" height=\"27\" width=\"7\"></td>" +
                                  "  <td class=\"mainlevel-top\" nowrap=\"nowrap\"><a href=\"List.aspx\">Nhân sự</a></td>" +
                                  "  <td class=\"rtab_off\"><img src=\"../images/1x1.gif\" height=\"26\" width=\"7\"></td>" +
                                  "  <td id=\"tspacer\"><img src=\"../images/1x1.gif\" height=\"1\" width=\"1\"></td>");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["AuthorityCode"].ToString() == ConfigurationSettings.AppSettings["PermissionAuthority"])
                    {
                        empAuthorityCode = dt.Rows[i]["AuthorityCode"].ToString();
                        sb.Append("<td class=\"ltab_on\"><img src=\"../images/1x1.gif\" height=\"26\" width=\"7\"></td>" +
                                  "  <td class=\"mainlevel-top-selected\" nowrap=\"nowrap\"><a href=\"\">Phân quyền</a></td>" +
                                  "  <td class=\"rtab_on\"><img src=\"../images/1x1.gif\" height=\"26\" width=\"7\"></td>" +
                                  "  <td id=\"tspacer\"><img src=\"../images/1x1.gif\" height=\"1\" width=\"1\"></td>");

                    }

                    if (dt.Rows[i]["AuthorityCode"].ToString() == ConfigurationSettings.AppSettings["SalaryAuthority"])
                    {
                        sb.Append("<td class=\"ltab_off\"><img src=\"../images/1x1.gif\" height=\"27\" width=\"7\"></td>" +
                                  "  <td class=\"mainlevel-top\" nowrap=\"nowrap\"><a href=\"../N_Su/Calculate_Salary.aspx\">Tiền Lương</a></td>" +
                                  "  <td class=\"rtab_off\"><img src=\"../images/1x1.gif\" height=\"26\" width=\"7\"></td>" +
                                  "  <td id=\"tspacer\"><img src=\"../images/1x1.gif\" height=\"1\" width=\"1\"></td>");
                    }

                    if (dt.Rows[i]["AuthorityCode"].ToString() == ConfigurationSettings.AppSettings["ManageAuthority"])
                    {
                        sb.Append("<td class=\"ltab_off\"><img src=\"../images/1x1.gif\" height=\"27\" width=\"7\"></td>" +
                                  "  <td class=\"mainlevel-top\" nowrap=\"nowrap\"><a href=\"../DanhMuc/Company.aspx\">Quản Trị</a></td>" +
                                  "  <td class=\"rtab_off\"><img src=\"../images/1x1.gif\" height=\"26\" width=\"7\"></td>" +
                                  "  <td id=\"tspacer\"><img src=\"../images/1x1.gif\" height=\"1\" width=\"1\"></td>");
                    }

                    if ((dt.Rows[i]["AuthorityCode"].ToString() == ConfigurationSettings.AppSettings["Emp_FullAuthority"]) &&
                        (isExist == false))
                    {
                        isExist = true;
                        sb.Append("<td class=\"ltab_off\"><img src=\"../images/1x1.gif\" height=\"27\" width=\"7\"></td>" +
                                  "  <td class=\"mainlevel-top\" nowrap=\"nowrap\"><a href=\"Attendance_Tracking.aspx\">Chấm công</a></td>" +
                                  "  <td class=\"rtab_off\"><img src=\"../images/1x1.gif\" height=\"26\" width=\"7\"></td>" +
                                  "  <td id=\"tspacer\"><img src=\"../images/1x1.gif\" height=\"1\" width=\"1\"></td>");
                    }

                    if ((dt.Rows[i]["AuthorityCode"].ToString() == ConfigurationSettings.AppSettings["Emp_PartAuthority"]) &&
                        (isExist == false))
                    {
                        isExist = true;
                        sb.Append("<td class=\"ltab_off\"><img src=\"../images/1x1.gif\" height=\"27\" width=\"7\"></td>" +
                                  "  <td class=\"mainlevel-top\" nowrap=\"nowrap\"><a href=\"Attendance_Tracking.aspx\">Chấm công</a></td>" +
                                  "  <td class=\"rtab_off\"><img src=\"../images/1x1.gif\" height=\"26\" width=\"7\"></td>" +
                                  "  <td id=\"tspacer\"><img src=\"../images/1x1.gif\" height=\"1\" width=\"1\"></td>");
                    }
                }
                sb.Append("<td class=\"ltab_off\"><img src=\"../images/1x1.gif\" height=\"27\" width=\"7\"></td>" +
                                  "  <td class=\"mainlevel-top\" nowrap=\"nowrap\"><a href=\"PageReport.aspx\">Báo cáo</a></td>" +
                                  "  <td class=\"rtab_off\"><img src=\"../images/1x1.gif\" height=\"26\" width=\"7\"></td>" +
                                  "  <td id=\"tspacer\"><img src=\"../images/1x1.gif\" height=\"1\" width=\"1\"></td>");

                sb.Append("         </tr></tbody></table>" +
                          "          </td><td align=\"left\" background=\"../images/NhanSu/tbar_bg_on.gif\" valign=\"middle\" width=\"100%\"><img src=\"/images/tabs/rtabs.gif\" border=\"0\" height=\"27\" width=\"1\"></td><td background=\"../images/NhanSu/tbar_rcap_on.gif\" width=\"5\"><img src=\"../images/pixel.gif\" border=\"0\" height=\"1\" width=\"5\"></td>" +
                          "          </tr></tbody></table>" +
                          "      </td>" +
                          "  </tr>");

                ltlAuthorityList.Text = sb.ToString();
            }
            catch (Exception ex)
            {
            }
        }
        #endregion bindAuthority

        #region "U_Function"
        private void Search()
        {
            string sDateForm = Convert.ToDateTime(dtFrom.SelectedDate).ToString("yyyyMMdd");
            string sDateTo = Convert.ToDateTime(dtTo.SelectedDate).ToString("yyyyMMdd");
            string sEmp = txtEmployee.Text.Trim();
            int idD = findDepartmentId(rcbDepartment.Items[0].Text);
            DataTable _dtLog = new cls_Log_Login().Get_Log(sDateForm, sDateTo, idD, sEmp);
            if (_dtLog != null)
            {
                gridLogin.DataSource = _dtLog;
                gridLogin.DataBind();

            }
            else
            {
                
            }

        }
        private void Detail_Operator( long IDLog)
        {

            DataTable _dtOperator = new cls_Log_Login().Get_Detail_Log(IDLog, "Employee");
            if (_dtOperator != null)
            {
                gridOperator.DataSource = _dtOperator;
                gridOperator.DataBind();
            }
        }
        #endregion
        #region TreeView

        protected void rcbDepartment_Bind()
        {
            RadTreeView rtrDepartment = (RadTreeView)rcbDepartment.Items[0].FindControl("rtrDepartment");

            if (rtrDepartment != null)
            {
                rtrDepartment.Nodes.Clear();
                rcbDepartment.Items[0].Text = "";
                bindTreeView(rtrDepartment);

              
                if (rtrDepartment.Nodes.Count > 0)
                {
                    rtrDepartment.Nodes[0].Selected = true;
                    rcbDepartment.Items[0].Text = rtrDepartment.Nodes[0].Text;

                  
                }
            }
        }

        private void bindTreeView(RadTreeView rtrDepartment)
        {
            // ---- BIND DU LIEU VAO LISTVIEW --
            cls_DepartmentInformation dInfor = new cls_DepartmentInformation();
            dInfor.CompanyId = 1;

          DataTable  dtDepart = dInfor.SelectForSalary();
            ViewState["DT_DEPARTMENT"] = dtDepart;

            //int countDepartment = dtDepart.Rows.Count;
            //ShowDSTreeview(0, rtrDepartment);
            RadTreeNode node = new RadTreeNode();
            node.Text = "--- Lựa chọn ---";
            node.Value = "0";

            rtrDepartment.Nodes.Add(node);
            addNodeTreeView("0", node, dtDepart);
        }

        private void addNodeTreeView(string parentId, RadTreeNode parentNode, DataTable dtDepart)
        {
            try
            {
                DataRow[] _Row = dtDepart.Select("DepartParentId=" + parentId);
                if (_Row.Count() > 0)
                {
                    for (int i = 0; i < _Row.Count(); i++)
                    {
                        RadTreeNode childNode = new RadTreeNode();
                        childNode.Text = _Row[i]["DepartName"].ToString();
                        childNode.Value = _Row[i]["Id"].ToString();

                        parentNode.Nodes.Add(childNode);
                        addNodeTreeView(_Row[i]["Id"].ToString(), childNode, dtDepart);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private int findDepartmentId(string name)
        {
            DataTable dt = (DataTable)ViewState["DT_DEPARTMENT"];

            DataRow[] _Row = dt.Select("DepartName='" + name + "'");

            if (_Row.Count() > 0)
            {
                return Convert.ToInt32(_Row[0]["Id"]);
            }
            else
                return 0;
        }

        protected void rtrDepartment_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            e.Node.Selected = true;
            rcbDepartment.Items[0].Text = e.Node.Text;

          
        }

        #endregion TreeView
    
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        protected void gridLogin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gridLogin.SelectedValue == null) return;
            try
            {
                Detail_Operator(Convert.ToInt64(gridLogin.SelectedValue));
               
            }
            catch (Exception ex)
            {
            }
        }

    }
}
