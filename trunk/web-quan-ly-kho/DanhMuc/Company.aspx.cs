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

namespace QLCV.DanhMuc
{
    public partial class Company : System.Web.UI.Page
    {
        public int cAct = 0;
        string empAuthorityCode = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // ---- KIEM TRA LOGIN -----
                if (Session["UserId"] == null)
                {
                    Response.Redirect("~/login.aspx");
                }
                // --- END KT LOGIN ----

                bindAuthority();
                if (empAuthorityCode.Length > 0)
                {
                    btnDelete.Attributes.Add("onclick", "return confirm('Bạn thực sự muốn xóa thông tin Công Ty này?')");
                    bindMenuLeft();
                    bindDetail();
                }
                else
                {
                    Response.Redirect("~/ErrorPermission.aspx");
                }
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
                                  "  <td class=\"mainlevel-top\" nowrap=\"nowrap\"><a href=\"../N_Su/List.aspx\">Nhân sự</a></td>" +
                                  "  <td class=\"rtab_off\"><img src=\"../images/1x1.gif\" height=\"26\" width=\"7\"></td>" +
                                  "  <td id=\"tspacer\"><img src=\"../images/1x1.gif\" height=\"1\" width=\"1\"></td>");
                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["AuthorityCode"].ToString() == ConfigurationSettings.AppSettings["PermissionAuthority"])
                    {
                        sb.Append("<td class=\"ltab_off\"><img src=\"../images/1x1.gif\" height=\"27\" width=\"7\"></td>" +
                                  "  <td class=\"mainlevel-top\" nowrap=\"nowrap\"><a href=\"../N_Su/Permission.aspx\">Phân quyền</a></td>" +
                                  "  <td class=\"rtab_off\"><img src=\"../images/1x1.gif\" height=\"26\" width=\"7\"></td>" +
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
                        sb.Append("<td class=\"ltab_on\"><img src=\"../images/1x1.gif\" height=\"26\" width=\"7\"></td>" +
                                  "  <td class=\"mainlevel-top-selected\" nowrap=\"nowrap\"><a href=\"\">Quản trị</a></td>" +
                                  "  <td class=\"rtab_on\"><img src=\"../images/1x1.gif\" height=\"26\" width=\"7\"></td>" +
                                  "  <td id=\"tspacer\"><img src=\"../images/1x1.gif\" height=\"1\" width=\"1\"></td>");
                        empAuthorityCode = dt.Rows[i]["AuthorityCode"].ToString();
                    }

                    if ((dt.Rows[i]["AuthorityCode"].ToString() == ConfigurationSettings.AppSettings["Emp_FullAuthority"]) &&
                        (isExist == false))
                    {
                        isExist = true;
                        sb.Append("<td class=\"ltab_off\"><img src=\"../images/1x1.gif\" height=\"27\" width=\"7\"></td>" +
                                  "  <td class=\"mainlevel-top\" nowrap=\"nowrap\"><a href=\"../N_Su/Attendance_Tracking.aspx\">Chấm công</a></td>" +
                                  "  <td class=\"rtab_off\"><img src=\"../images/1x1.gif\" height=\"26\" width=\"7\"></td>" +
                                  "  <td id=\"tspacer\"><img src=\"../images/1x1.gif\" height=\"1\" width=\"1\"></td>");
                    }

                    if ((dt.Rows[i]["AuthorityCode"].ToString() == ConfigurationSettings.AppSettings["Emp_PartAuthority"]) &&
                        (isExist == false))
                    {
                        isExist = true;
                        sb.Append("<td class=\"ltab_off\"><img src=\"../images/1x1.gif\" height=\"27\" width=\"7\"></td>" +
                                  "  <td class=\"mainlevel-top\" nowrap=\"nowrap\"><a href=\"../N_Su/Attendance_Tracking.aspx\">Chấm công</a></td>" +
                                  "  <td class=\"rtab_off\"><img src=\"../images/1x1.gif\" height=\"26\" width=\"7\"></td>" +
                                  "  <td id=\"tspacer\"><img src=\"../images/1x1.gif\" height=\"1\" width=\"1\"></td>");
                    }
                }
                sb.Append("<td class=\"ltab_off\"><img src=\"../images/1x1.gif\" height=\"27\" width=\"7\"></td>" +
                                 "  <td class=\"mainlevel-top\" nowrap=\"nowrap\"><a href=\"../N_Su/PageReport.aspx\">Báo cáo</a></td>" +
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

        private void bindMenuLeft()
        {
            try
            {
                if (Request["C_action"] == null)
                    cAct = 1;
                else
                    cAct = Convert.ToInt32(Request["C_action"]);

                DataTable dt = new cls_Catalog_Company().Select((short)1, Convert.ToInt32(Session["UserId"]));

                if (cAct == 1)
                {
                    if(dt.Rows.Count<=0)
                        Response.Redirect("Company.aspx?C_action=2");
                        
                }
                if (dt.Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    string cId, cName;

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cId = dt.Rows[i]["Id"].ToString();
                        cName = dt.Rows[i]["ComName"].ToString();

                        sb.Append("<div class=\"MMT_top-level\">" +
                                  "  <a id=\"Hyperlink1\" class=\"NormalBold\" href=\"Company.aspx?C_action=1&C_Id=" + cId + "\">" +
                                  "      <img src=\"../images/MRoom/breadcrumb_separator_arrow_full.png\" align=\"absmiddle\" border=\"0\" alt=\"\" hspace=\"2\" /> " + cName + "</a>" +
                                  "</div>");

                        if (i == 0) ViewState["cIdDefault"] = cId;
                    }

                    ltl_Menu.Text = sb.ToString();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void bindDetail()
        {
            try
            {
                if (Request["C_action"] == null)
                    cAct = 1;
                else
                    cAct = Convert.ToInt32(Request["C_action"]);

                DataTable dt = new cls_Catalog_Company().Select((short)1, Convert.ToInt32(Session["UserId"]));
                DataRow dRow = dt.NewRow();
                dRow["Id"] = "0";
                dRow["ComName"] = "Không";
                dt.Rows.InsertAt(dRow, 0);
                ddlBelongCompany.DataSource = dt;
                ddlBelongCompany.DataBind();

                if (cAct == 1)
                {
                    short cId;
                    if (Request["C_Id"] == null)
                        cId = Convert.ToInt16(ViewState["cIdDefault"]);
                    else
                        cId = Convert.ToInt16(Request["C_Id"]);

                    cls_Catalog_Company cCompany = new cls_Catalog_Company();
                    cCompany.Id = cId;
                    DataTable dtDetail = cCompany.SelectById();

                    txtComCode.Text = dtDetail.Rows[0]["ComCode"].ToString();
                    txtComName.Text = dtDetail.Rows[0]["ComName"].ToString();
                    txtEstablishDay.Text = dtDetail.Rows[0]["EstablishDate"].ToString();
                    ddlBelongCompany.SelectedValue = dtDetail.Rows[0]["Belong_ComId"].ToString();
                }
                else
                {
                    lblTitle.Text = "Thêm mới công ty";
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DateTime esbDate;
            if (checkValidData(out esbDate))
            {
                if (Request["C_action"] == null)
                    cAct = 1;
                else
                    cAct = Convert.ToInt32(Request["C_action"]);

                short cId;
                if (cAct == 1)
                {
                    if (Request["C_Id"] == null)
                        cId = Convert.ToInt16(ViewState["cIdDefault"]);
                    else
                        cId = Convert.ToInt16(Request["C_Id"]);
                }
                else cId = (short)0;

                try
                {
                    cls_Catalog_Company cCompany = new cls_Catalog_Company();
                    cCompany.Id = cId;
                    cCompany.Belong_ComId = Convert.ToInt16(ddlBelongCompany.SelectedValue);
                    cCompany.ComCode = txtComCode.Text;
                    cCompany.ComName = txtComName.Text;
                    cCompany.CreatedTime = DateTime.Now;
                    cCompany.EstablishDate = esbDate;

                    if (cAct == 2)
                    {
                        int resultInsert = cCompany.Insert();
                        if (resultInsert > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                                   "MyScript", "alert('Thêm mới công ty thành công.');", true);

                            Response.Redirect("Company.aspx");
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                                   "MyScript", "alert('Có lỗi khi thêm mới công ty. Vui lòng thử lại !!');", true);
                        }
                    }
                    else
                    {
                        int resultUpdate = cCompany.Update();

                        if (resultUpdate > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                                   "MyScript", "alert('Cập nhật thông tin công ty thành công.');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                                   "MyScript", "alert('Có lỗi khi cập nhật TT công ty. Vui lòng thử lại !!');", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Có lỗi khi cập nhật thông tin: " + ex.ToString();
                }
            }

        }

        private bool checkValidData(out DateTime esbDate)
        {
            esbDate = DateTime.Now;
            if (txtComName.Text.Length <= 0)
            {
                txtComName.Focus();
                lblMessage.Text = "Bạn phải nhập Tên Công Ty trước.";
                return false;
            }

            if (txtComCode.Text.Length <= 0)
            {
                txtComCode.Focus();
                lblMessage.Text = "Bạn phải nhập Mã Công Ty trước.";
                return false;
            }

            if (txtEstablishDay.Text.Length <= 0)
            {
                txtEstablishDay.Focus();
                lblMessage.Text = "Bạn phải nhập Ngày thành lập trước.";
                return false;
            }
            else
            {
                try
                {
                    //check valid date
                    string temp = txtEstablishDay.Text;
                    int day, month, year;
                    day = Convert.ToInt32(temp.Substring(0, temp.IndexOf('/')));
                    temp = temp.Substring(temp.IndexOf('/') + 1);
                    month = Convert.ToInt32(temp.Substring(0, temp.IndexOf('/')));
                    year = Convert.ToInt32(temp.Substring(temp.IndexOf('/') + 1));
                
                    esbDate = new DateTime(year, month, day);
                }
                catch (Exception ex)
                {
                    txtEstablishDay.Focus();
                    lblMessage.Text = "Bạn phải nhập Ngày thành lập sai định dạng \"dd/mm/yyy\". Vui lòng nhập lại.";
                    return false;
                }
            }
            return true;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                short cId;
                if (Request["C_Id"] == null)
                    cId = Convert.ToInt16(ViewState["cIdDefault"]);
                else
                    cId = Convert.ToInt16(Request["C_Id"]);

                cls_Catalog_Company cCompany = new cls_Catalog_Company();
                cCompany.Id = cId;

                int resultDelete = cCompany.Delete();

                if (resultDelete > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                        "MyScript", "alert('Xóa TT công ty thành công.');", true);

                    Response.Redirect("Company.aspx");
                }
                else
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                        "MyScript", "alert('Có lỗi khi xóa TT công ty. Vui lòng thử lại!!');", true);
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Có lỗi khi xóa thông tin: " + ex.ToString();
            }
        }
    }
}
