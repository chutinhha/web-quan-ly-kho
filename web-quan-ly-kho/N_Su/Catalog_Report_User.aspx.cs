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
namespace QLCV.N_Su
{
    public partial class Catalog_Report_User : System.Web.UI.Page
    {
        private cls_Report_User objReport = new cls_Report_User();
        string empAuthorityCode = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            // ---- KIEM TRA LOGIN -----
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/login.aspx");
            }
            // ---- END KT LOGIN -------

            if (!IsPostBack)
            {
                bindAuthority();
                if (empAuthorityCode.Length > 0)
                {
                    LoadGroupUser();
                    LoadListReport();
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
        private void LoadGroupUser()
        {
            DataTable _dt = new cls_Groups().SelectFollowCompanyByUserId(Convert.ToInt32(Session["UserId"]));
            cboGroup.DataSource = _dt;
            cboGroup.DataValueField = "GroupID";
            cboGroup.DataTextField = "GroupName";
            cboGroup.DataBind();
            if (cboGroup.Items.Count > 0)
            {
                cboGroup.Items[0].Selected = true;
                LoadListUser(cboGroup.SelectedValue);
            }
        }
        private void LoadListUser(string GroupID)
        {
            cls_Group_Employees objGroup = new cls_Group_Employees();
            DataTable _dt = objGroup.GetUserList(Convert.ToInt32(GroupID));
            gridUser.DataSource = _dt;
            gridUser.DataBind();
            if (gridUser.Items.Count > 0)
            {
                gridUser.Items[0].Selected = true;
                GetReportFollowGroup(Convert.ToInt32(cboGroup.SelectedValue), Convert.ToInt32(gridUser.SelectedValue));
            }
        }
        private void LoadListReport()
        {
            DataTable _dt = new cls_Catalog_Report().SelectByModuleCode(ConfigurationSettings.AppSettings["Emp_Module"]);
            gridReport.DataSource = _dt;
            gridReport.DataBind();
        }
        private void GetReportFollowGroup(int GroupID,int UserID)
        {
            DataTable _dt = objReport.GetReportFollowUser(GroupID, UserID);

            for (int i = 0; i < gridReport.Items.Count; i++)
            {
                bool OK = false;
                for (int k = 0; k < _dt.Rows.Count; k++)
                    if (gridReport.Items[i].GetDataKeyValue("ID_Report").ToString() == _dt.Rows[k]["ID_Report"].ToString())
                    {
                        gridReport.Items[i].Selected = true;
                        OK = true;
                        break;
                    }
                if (OK == false)
                    gridReport.Items[i].Selected = false;
            }
        }
        private DataTable GetAllReport()
        {
            DataTable _dt = new DataTable();
            _dt.Columns.Add("ID_Report", typeof(int));
            if (gridReport.SelectedItems.Count > 0)
            {
                for (int i = 0; i < gridReport.SelectedItems.Count; i++)
                {
                    DataRow dr = _dt.NewRow();
                    dr["ID_Report"] = ((GridDataItem)gridReport.SelectedItems[i]).GetDataKeyValue("ID_Report");
                    _dt.Rows.Add(dr);
                }
                return _dt;
            }
            else
                return null;
        }
        #endregion

        protected void gridGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gridUser.SelectedValue == null) return;
            try
            {
                GetReportFollowGroup(Convert.ToInt32(cboGroup.SelectedValue),Convert.ToInt32(gridUser.SelectedValue));
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (gridUser.SelectedValue == null)
            {
                uscMsgBox.AddMessage("Bạn phải chọn nhóm người dùng", YaBu.MessageBox.uscMsgBox.enmMessageType.Info);
                return;
            }
            if (objReport.Delete(Convert.ToInt32(gridUser.SelectedValue)) < 0)
            {
                uscMsgBox.AddMessage("Có lỗi khi xóa và cập nhật dữ liệu!", YaBu.MessageBox.uscMsgBox.enmMessageType.Error);
                return;
            }
            DataTable _dt = GetAllReport();
            if (_dt != null)
            {
                if (objReport.UpdateAllRow(cboGroup.SelectedValue,gridUser.SelectedValue.ToString(), _dt) < 0)
                {
                    uscMsgBox.AddMessage("Có lỗi khi cập nhật dữ liệu!", YaBu.MessageBox.uscMsgBox.enmMessageType.Info);
                    return;
                }
            }
            uscMsgBox.AddMessage("Cập nhật dữ liệu thành công!", YaBu.MessageBox.uscMsgBox.enmMessageType.Info);
        }

        protected void cboGroup_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cboGroup.SelectedValue == null) return;
            LoadListUser(cboGroup.SelectedValue);
        }

    }
}
