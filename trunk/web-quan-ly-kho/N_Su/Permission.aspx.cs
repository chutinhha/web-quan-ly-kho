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

namespace QLCV.N_Su
{
    public partial class Permission : System.Web.UI.Page
    {
        public int p_action = 0;
        string empAuthorityCode = "";
        private bool isChange;
        protected void Page_Load(object sender, EventArgs e)
        {
            int e_id = 0;
            int g_id = 0;
            if (Request["P_action"] == null)
            {
                p_action = 1;
                lblGroupName.Text = "Danh sách đã phân quyền";
            }
            else
            {
                p_action = Convert.ToInt32(Request["P_action"]);

                if (p_action == 3)
                {
                    if (Request["E_Id"] != null) e_id = Convert.ToInt32(Request["E_Id"]);

                    if (e_id == 0) lblGroupName.Text = "Thêm mới quyền Người dùng";
                    else lblGroupName.Text = "Cập nhật quyền Người dùng";
                }

                if (p_action == 1) lblGroupName.Text = "Danh sách đã phân quyền";
                if (p_action == 2)
                {
                    if (Request["G_Id"] != null) g_id = Convert.ToInt32(Request["G_Id"]);

                    if (g_id == 0) lblGroupName.Text = "Thêm mới quyền Nhóm người dùng";
                    else lblGroupName.Text = "Cập nhật quyền Nhóm người dùng";
                }
            }
            bindAuthority();

            if (!IsPostBack)
            {
                // ---- KIEM TRA LOGIN -----
                if (Session["UserId"] == null)
                {
                    Response.Redirect("~/login.aspx");
                }
                // --- END KT LOGIN ----  

                if (empAuthorityCode.Length > 0)
                {
                    if (p_action == 1)
                        bindData();

                    if (p_action == 3)
                        bindRepeaterE(e_id);

                    if (p_action == 2)
                        bindRepeaterG(g_id);
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

        #region P_Action =1
        private void bindData()
        {
            try
            {
                //LAY DANH SACH CONG TY
                cls_Module_Authority_Employee aEmp = new cls_Module_Authority_Employee();
                aEmp.EmployeeId = Convert.ToInt32(Session["UserId"]);

                DataTable dt = aEmp.GetCompanyByAuthorityCode(ConfigurationSettings.AppSettings["PermissionAuthority"]);
                ddlCompany.DataSource = dt;
                ddlCompany.DataBind();

                bindPermissionList();
            }
            catch (Exception ex)
            {
            }
        }

        private void bindPermissionList()
        {
            try
            {
                // --- BIND CAC NHOM DA DUOC PHAN QUYEN ---
                // --- LAY DANH SACH CAC QUYEN CUA NGUOI DUNG HIEN TAI DUOC PHAN ---

                cls_Module_Authority_Employee aEmp = new cls_Module_Authority_Employee();
                aEmp.EmployeeId = Convert.ToInt32(Session["UserId"]);
                DataTable dtAuthority = aEmp.GetByModuleCode(ConfigurationSettings.AppSettings["Emp_Module"]);

                StringBuilder sb = new StringBuilder();
                sb.Append(" <table class=\"basic-table\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">" +
                             "  <tbody><tr>" +
                             "      <td>" +
                             "          <table class=\"form-table\" border=\"0\" cellpadding=\"4\"><tbody>" +
                             "              <tr class=\"gray\">" +
                             "                  <td colspan=\"" + dtAuthority.Rows.Count.ToString() + "\">PHÂN QUYỀN NHÓM NGƯỜI DÙNG</td>" +
                             "                  <td align=\"right\" colspan=\"2\"><a href=\"Permission.aspx?P_action=2\">+Bổ sung</a></td>" +
                             "              </tr>" +
                             "              <tr bgcolor=\"whitesmoke\">" +
                             "                  <td class=\"Normal\" width=\"180px\"> <u>Nhóm đã phân quyền </u></td>");
                for (int i = 0; i < dtAuthority.Rows.Count; i++)
                {
                    sb.Append("                  <td class=\"NormalBold\" align=\"center\">Quyền " + dtAuthority.Rows[i]["AuthorityName"].ToString() + "</td>");
                }
                sb.Append("                  <td class=\"normal\">Cập nhật</td>" +
                          "              </tr>");

                cls_Module_Authority_Group aGroup = new cls_Module_Authority_Group();
                aGroup.CompanyId = Convert.ToInt16(ddlCompany.SelectedValue);
                DataTable dtGroup = aGroup.SelectGroupByCompany(ConfigurationSettings.AppSettings["Emp_Module"]);

                if (dtGroup.Rows.Count > 0)
                {
                    DataTable dtGAuthority = aGroup.SelectByCompany(ConfigurationSettings.AppSettings["Emp_Module"], Convert.ToInt32(Session["UserId"]));

                    for (int i = 0; i < dtGroup.Rows.Count; i++)
                    {
                        DataRow[] _Row = dtGAuthority.Select("GroupId=" + dtGroup.Rows[i]["GroupId"].ToString(), "AuthorityCode asc");

                        sb.Append("<tr>" +
                                  "     <td class=\"NormalBold\"><img src=\"../images/Message/user-group.png\" /> " + _Row[0]["GroupName"].ToString() + "</td>");

                        for (int j = 0; j < _Row.Count(); j++)
                        {
                            if (_Row[j]["IsUse"].ToString() == "1")
                                sb.Append("     <Td class=\"normal\" align=\"center\"><img src=\"../images/Update.gif\" align=\"absmiddle\"> </td>");
                            else
                                sb.Append("     <Td class=\"normal\" align=\"center\">&nbsp;</td>");
                        }

                        sb.Append("     <td class=\"normal\" align=\"center\"><a href=\"Permission.aspx?P_action=2&G_Id=" + dtGroup.Rows[i]["GroupId"].ToString() + "\">Sửa</a></td>" +
                                  "</tr>");
                    }
                }
                else
                {
                    sb.Append("<tr><td class=\"NormalBold\" align=\"center\" colspan=\"" + (dtAuthority.Rows.Count + 2).ToString() + "\"><i>Hiện chưa có nhóm nào được phân quyền.</i></td></tr>");
                }
                sb.Append("              </tbody>" +
                          "          </table>" +
                          "      </td>" +
                          "  </tr>" +
                        "</tbody></table>");
                ltlListGroup.Text = sb.ToString();

                // --- BIND CAC NGUOI DUNG DA DUOC PHAN QUYEN ---
                sb = new StringBuilder();
                sb.Append(" <table class=\"basic-table\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">" +
                             "  <tbody><tr>" +
                             "      <td>" +
                             "          <table class=\"form-table\" border=\"0\" cellpadding=\"4\"><tbody>" +
                             "              <tr class=\"gray\">" +
                             "                  <td colspan=\"" + dtAuthority.Rows.Count.ToString() + "\">PHÂN QUYỀN NGƯỜI DÙNG</td>" +
                             "                  <td align=\"right\" colspan=\"2\"><a href=\"Permission.aspx?P_action=3\">+Bổ sung</a></td>" +
                             "              </tr>" +
                             "              <tr bgcolor=\"whitesmoke\">" +
                             "                  <td class=\"Normal\" width=\"180px\"><u>Người dùng đã phân quyền </u></td>");
                for (int i = 0; i < dtAuthority.Rows.Count; i++)
                {
                    sb.Append("                  <td class=\"NormalBold\" align=\"center\">Quyền " + dtAuthority.Rows[i]["AuthorityName"].ToString() + "</td>");
                }
                sb.Append("                  <td class=\"normal\">Cập nhật</td>" +
                          "              </tr>");

                aEmp = new cls_Module_Authority_Employee();
                aEmp.EmployeeId = Convert.ToInt32(Session["UserId"]);
                aEmp.CompanyId = Convert.ToInt16(ddlCompany.SelectedValue);

                DataTable dtEmp = aEmp.SelectEmpByCompany(ConfigurationSettings.AppSettings["Emp_Module"]);

                if (dtEmp.Rows.Count > 0)
                {
                    DataTable dtEAuthority = aEmp.SelectByCompany(ConfigurationSettings.AppSettings["Emp_Module"]);

                    for (int i = 0; i < dtEmp.Rows.Count; i++)
                    {
                        DataRow[] _Row = dtEAuthority.Select("EmployeeId=" + dtEmp.Rows[i]["EmployeeId"].ToString(), "AuthorityCode asc");

                        sb.Append("<tr>" +
                                  "     <td class=\"NormalBold\"><img src=\"../images/MRoom/user.gif\" /> " + _Row[0]["EmployName"].ToString() + "</td>");

                        for (int j = 0; j < _Row.Count(); j++)
                        {
                            if (_Row[j]["IsUse"].ToString() == "1")
                                sb.Append("     <Td class=\"normal\" align=\"center\"><img src=\"../images/Update.gif\" align=\"absmiddle\"> </td>");
                            else
                                sb.Append("     <Td class=\"normal\" align=\"center\">&nbsp;</td>");
                        }

                        sb.Append("     <td class=\"normal\" align=\"center\"><a href=\"Permission.aspx?P_action=3&E_Id=" + dtEmp.Rows[i]["EmployeeId"].ToString() + "\">Sửa</a></td>" +
                                  "</tr>");
                    }
                }
                else
                {
                    sb.Append("<tr><td class=\"NormalBold\" align=\"center\" colspan=\"" + (dtAuthority.Rows.Count + 2).ToString() + "\"><i>Hiện chưa có người dùng nào được phân quyền.</i></td></tr>");
                }
                sb.Append("              </tbody>" +
                          "          </table>" +
                          "      </td>" +
                          "  </tr>" +
                        "</tbody></table>");
                ltlListEmployee.Text = sb.ToString();


            }
            catch (Exception ex)
            {
            }
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindPermissionList();
        }
        #endregion P_Action =1

        #region Common
        private DataTable ConvertToDataTable(string str, char separateCharacter, string colName, int typeColumn)
        {
            DataTable tmpTable = new DataTable();

            try
            {
                tmpTable.Columns.Add(colName);

                if (str.Length > 0)
                {
                    string[] tmpList = str.Split(separateCharacter);
                    int index = 0;
                    DataRow dtRow;
                    while (index < tmpList.Count())
                    {
                        DataRow[] _row;
                        if (typeColumn == 1)           //kieu int
                            _row = tmpTable.Select(colName + "=" + tmpList[index].ToString().Trim());
                        else                            //kieu string
                            _row = tmpTable.Select(colName + "='" + tmpList[index].ToString().Trim() + "'");

                        if (_row.Count() <= 0)
                        {
                            dtRow = tmpTable.NewRow();
                            dtRow[colName] = tmpList[index].ToString().Trim();
                            tmpTable.Rows.Add(dtRow);
                        }
                        index++;
                    }
                }

                return tmpTable;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion Common

        #region P_Action=3

        private void bindRepeaterE(int empId)
        {
            try
            {
                //LAY DANH SACH CONG TY
                cls_Module_Authority_Employee aEmp = new cls_Module_Authority_Employee();
                aEmp.EmployeeId = Convert.ToInt32(Session["UserId"]);

                DataTable dt = aEmp.GetCompanyByAuthorityCode(ConfigurationSettings.AppSettings["PermissionAuthority"]);
                ddlCompanySelect.DataSource = dt;
                ddlCompanySelect.DataBind();
                ViewState["DT_COMPANY"] = dt;
                ViewState["DT_DEPARTMENT"] = new cls_DepartmentInformation().SelectList(0);

                bindCboEmployee(empId);

                short moduleId, isAdmin;
                dt = aEmp.SelectByEId(ConfigurationSettings.AppSettings["Emp_Module"], ConfigurationSettings.AppSettings["PermissionAuthority"],
                                        empId, out moduleId, out isAdmin);

                if (isAdmin > 0)
                    btnESave.Enabled = isChange = false;
                else
                    btnESave.Enabled = isChange = true;

                List<cls_Module_Authority_Employee> list = getList(dt, moduleId);
                ViewState["DT_EAUTHORITY"] = dt;

                rptData.DataSource = list;
                rptData.DataBind();
            }
            catch (Exception ex)
            {
            }
        }

        private void bindCboEmployee(int empId)
        {
            try
            {
                cls_PersonalInformaiton pInfor = new cls_PersonalInformaiton();
                pInfor.RootId = empId;
                DataTable dtEmployee = pInfor.SelectForPermission(Convert.ToInt16(ddlCompanySelect.SelectedValue));

                cboEmployee.DataSource = dtEmployee;
                cboEmployee.DataBind();

                if (empId > 0)
                {
                    DataRow[] _row = dtEmployee.Select("Id=" + empId);

                    if (_row.Count() > 0)
                    {
                        ddlCompanySelect.SelectedValue = _row[0]["CompanyId"].ToString();
                        cboEmployee.DataSource = dtEmployee;
                        cboEmployee.DataBind();
                        cboEmployee.SelectedValue = empId.ToString();

                        ddlCompanySelect.Enabled = false;
                        cboEmployee.Enabled = false;
                        return;
                    }
                }

                DataRow dtRow = dtEmployee.NewRow();
                dtRow["EmployeeName"] = "-- Lựa chọn --";
                dtRow["Email"] = "";
                dtRow["EmployeeCode"] = "None";
                dtRow["Id"] = "0";
                dtEmployee.Rows.InsertAt(dtRow, 0);

                cboEmployee.DataSource = dtEmployee;
                cboEmployee.DataBind();
                cboEmployee.SelectedValue = "0";
            }
            catch (Exception ex)
            {
            }
        }

        private List<cls_Module_Authority_Employee> getList(DataTable dt, short moduleId)
        {
            try
            {
                List<cls_Module_Authority_Employee> list = new List<cls_Module_Authority_Employee>();

                cls_Module_Authority_Employee aEmp = new cls_Module_Authority_Employee();
                aEmp.EmployeeId = Convert.ToInt32(Session["UserId"]);
                DataTable dtAuthority = aEmp.GetByModuleCode(ConfigurationSettings.AppSettings["Emp_Module"]);

                for (int i = 0; i < dtAuthority.Rows.Count; i++)
                {
                    aEmp = new cls_Module_Authority_Employee();
                    aEmp.Company = "";
                    aEmp.Department = "";

                    DataRow[] _Row = dt.Select("AuthorityId=" + dtAuthority.Rows[i]["Id"].ToString());
                    for (int j = 0; j < _Row.Count(); j++)
                    {
                        aEmp.Company += _Row[j]["CompanyId"].ToString() + ";";
                        aEmp.Department += _Row[j]["DepartmentId"].ToString() + ";";
                    }
                    aEmp.Company = aEmp.Company.Substring(0, aEmp.Company.Length - 1);
                    aEmp.Department = aEmp.Department.Substring(0, aEmp.Department.Length - 1);
                    if (aEmp.Company == "0") aEmp.Company = "";
                    if (aEmp.Department == "0") aEmp.Department = "";

                    aEmp.AuthorityId = Convert.ToInt16(_Row[0]["AuthorityId"]);
                    aEmp.AuthorityName = _Row[0]["AuthorityName"].ToString();
                    aEmp.ModuleId = moduleId;
                    aEmp.IsUse = Convert.ToInt16(_Row[0]["IsUse"]);
                    aEmp.UseDepartment = Convert.ToInt16(_Row[0]["UseDepartment"]);

                    list.Add(aEmp);
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
            {
                var chkSelectA = (CheckBox)e.Item.FindControl("chkSelectA");
                var lblAuthorityId = (Label)e.Item.FindControl("lblAuthorityId");
                var lblModuleId = (Label)e.Item.FindControl("lblModuleId");
                var lbAuthorityName = (Label)e.Item.FindControl("lbAuthorityName");

                var txtComName = (TextBox)e.Item.FindControl("txtComName");
                var ibSelectCompany = (ImageButton)e.Item.FindControl("ibSelectCompany");
                var txtDepartName = (TextBox)e.Item.FindControl("txtDepartName");
                var ibSelectDepartment = (ImageButton)e.Item.FindControl("ibSelectDepartment");

                var item = (cls_Module_Authority_Employee)e.Item.DataItem;
                int index = e.Item.ItemIndex;
                if (item != null)
                {
                    lblAuthorityId.Text = item.AuthorityId.ToString();
                    lbAuthorityName.Text = item.AuthorityName;
                    lblModuleId.Text = item.ModuleId.ToString();
                    chkSelectA.Checked = (item.IsUse == 1) ? true : false;
                    txtComName.Text = getCompanyTextStr(item.Company);
                    ibSelectCompany.Attributes.Add("onclick", "javascript:newWindow('../Permission/SelectCompany.aspx?Index=" + (index + 1).ToString() + "&A_Id=" + lblAuthorityId.Text + "','window2')");
                    if (item.UseDepartment == 0)
                    {
                        txtDepartName.Visible = false;
                        ibSelectDepartment.Visible = false;
                    }
                    else
                    {
                        txtDepartName.Text = getDepartTextStr(item.Department);
                        ibSelectDepartment.Attributes.Add("onclick", "javascript:newWindow('../Permission/SelectDepartment.aspx?Index=" + (index + 1).ToString() + "&A_Id=" + lblAuthorityId.Text + "','window2')");
                    }
                    txtComName.Enabled = txtDepartName.Enabled = ibSelectCompany.Enabled = ibSelectDepartment.Enabled = isChange;
                }
            }
        }

        private string getDepartTextStr(string comIdStr)
        {
            string result = "";
            try
            {
                DataTable dtDepartmentId = ConvertToDataTable(comIdStr, ';', "Id", 1);
                DataTable dt = (DataTable)ViewState["DT_DEPARTMENT"];

                for (int i = 0; i < dtDepartmentId.Rows.Count; i++)
                {
                    DataRow[] _Row = dt.Select("Id=" + dtDepartmentId.Rows[i]["Id"].ToString());
                    if (_Row.Count() > 0)
                    {
                        if (result.IndexOf(_Row[0]["DepartName"].ToString() + ",") < 0)
                            result += _Row[0]["DepartName"].ToString() + ",";
                    }
                }

                if (result.Length > 0)
                    result = result.Substring(0, result.Length - 1);
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        private string getCompanyTextStr(string departIdStr)
        {
            string result = "";
            try
            {
                DataTable dtComId = ConvertToDataTable(departIdStr, ';', "CompanyId", 1);
                DataTable dt = (DataTable)ViewState["DT_COMPANY"];

                for (int i = 0; i < dtComId.Rows.Count; i++)
                {
                    DataRow[] _Row = dt.Select("Id=" + dtComId.Rows[i]["CompanyId"].ToString());
                    if (_Row.Count() > 0)
                    {
                        if (result.IndexOf(_Row[0]["ComName"].ToString() + ",") < 0)
                            result += _Row[0]["ComName"].ToString() + ",";
                    }
                }

                if (result.Length > 0)
                    result = result.Substring(0, result.Length - 1);
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        protected void ddlCompanySelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindCboEmployee(0);
        }

        protected void cboEmployee_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            int empId = Convert.ToInt32(cboEmployee.SelectedValue);

            if (empId > 0)
            {
                short moduleId, isAdmin;
                cls_Module_Authority_Employee aEmp = new cls_Module_Authority_Employee();
                aEmp.EmployeeId = Convert.ToInt32(Session["UserId"]);

                DataTable dt = aEmp.SelectByEId(ConfigurationSettings.AppSettings["Emp_Module"], ConfigurationSettings.AppSettings["PermissionAuthority"],
                                                    empId, out moduleId, out isAdmin);

                if (isAdmin > 0)
                    btnESave.Enabled = isChange = false;
                else
                    btnESave.Enabled = isChange = true;

                List<cls_Module_Authority_Employee> list = getList(dt, moduleId);
                ViewState["DT_EAUTHORITY"] = dt;

                rptData.DataSource = list;
                rptData.DataBind();
            }
        }

        protected void btnESave_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtCompany = (DataTable)ViewState["DT_COMPANY"];
                DataTable dtDepartment = (DataTable)ViewState["DT_DEPARTMENT"];
                DataTable dtOld = (DataTable)ViewState["DT_EAUTHORITY"];

                DataTable dtEAuthority = new DataTable();
                dtEAuthority.Columns.Add("Id", typeof(int));
                dtEAuthority.Columns.Add("ModuleId");
                dtEAuthority.Columns.Add("AuthorityId");
                dtEAuthority.Columns.Add("EmployeeId");
                dtEAuthority.Columns.Add("CompanyId");
                dtEAuthority.Columns.Add("DepartmentId");
                dtEAuthority.Columns.Add("CreatedTime");

                int e_id;
                short mId = 0;
                e_id = Convert.ToInt32(cboEmployee.SelectedValue);

                if (e_id == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                                    "MyScript", "alert('Bạn phải chọn Nhân viên cần phân quyền trước !');", true);
                    cboEmployee.Focus();
                    return;
                }

                for (int i = 0; i < rptData.Items.Count; i++)
                {
                    RepeaterItem item = rptData.Items[i];

                    var chkSelectA = (CheckBox)item.FindControl("chkSelectA");
                    var lblAuthorityId = (Label)item.FindControl("lblAuthorityId");
                    var lblModuleId = (Label)item.FindControl("lblModuleId");
                    mId = Convert.ToInt16(lblModuleId.Text);
                    var lbAuthorityName = (Label)item.FindControl("lbAuthorityName");

                    var txtComName = (TextBox)item.FindControl("txtComName");
                    var txtDepartName = (TextBox)item.FindControl("txtDepartName");

                    if (chkSelectA.Checked == true)
                    {
                        if (txtComName.Text.Length <= 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                                    "MyScript", "alert('Bạn phải chọn Công ty cho Quyền \"" + lbAuthorityName.Text + "\" !');", true);
                            txtComName.Focus();
                            return;
                        }
                        if (txtDepartName.Visible == true && txtDepartName.Text.Length <= 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                                    "MyScript", "alert('Bạn phải chọn Phòng/Ban cho Quyền \"" + lbAuthorityName.Text + "\" !');", true);
                            txtDepartName.Focus();
                            return;
                        }

                        if (txtDepartName.Visible == true)
                        {
                            DataTable dtDepartmentId = getDepartmentIdList(txtDepartName.Text, dtDepartment);

                            for (int j = 0; j < dtDepartmentId.Rows.Count; j++)
                            {
                                DataRow dRow = dtEAuthority.NewRow();
                                DataRow[] _Row = dtOld.Select("AuthorityId=" + lblAuthorityId.Text + " AND DepartmentId=" + dtDepartmentId.Rows[j]["Id"].ToString());

                                if (_Row.Count() > 0)
                                    dRow["Id"] = _Row[0]["Id"];
                                else
                                    dRow["Id"] = "0";

                                dRow["ModuleId"] = lblModuleId.Text;
                                dRow["AuthorityId"] = lblAuthorityId.Text;
                                dRow["EmployeeId"] = e_id;
                                dRow["CompanyId"] = dtDepartmentId.Rows[j]["CompanyId"];
                                dRow["DepartmentId"] = dtDepartmentId.Rows[j]["Id"];
                                dRow["CreatedTime"] = DateTime.Now;

                                dtEAuthority.Rows.Add(dRow);
                            }
                        }
                        else
                        {
                            DataTable dtCompanyId = getCompanyIdList(txtComName.Text, dtCompany);

                            for (int j = 0; j < dtCompanyId.Rows.Count; j++)
                            {
                                DataRow dRow = dtEAuthority.NewRow();
                                DataRow[] _Row = dtOld.Select("AuthorityId=" + lblAuthorityId.Text + " AND CompanyId=" + dtCompanyId.Rows[j]["Id"].ToString());

                                if (_Row.Count() > 0)
                                    dRow["Id"] = _Row[0]["Id"];
                                else
                                    dRow["Id"] = "0";

                                dRow["ModuleId"] = lblModuleId.Text;
                                dRow["AuthorityId"] = lblAuthorityId.Text;
                                dRow["EmployeeId"] = e_id;
                                dRow["CompanyId"] = dtCompanyId.Rows[j]["Id"];
                                dRow["DepartmentId"] = 0;
                                dRow["CreatedTime"] = DateTime.Now;

                                dtEAuthority.Rows.Add(dRow);
                            }
                        }
                    }
                }
                int resultUpdate = new cls_Module_Authority_Employee().ExecuteUpdateByPId(dtEAuthority, e_id, mId);

                if (resultUpdate > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                        "MyScript", "alert('Cập nhật phân quyền thao tác cho Nhân sự thành công!');", true);
                    Response.Redirect("Permission.aspx");
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                        "MyScript", "alert('Có lỗi trong quá trình cập nhật, vui lòng thử lại !!!');", true);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private DataTable getCompanyIdList(string comTextStr, DataTable dt)
        {
            try
            {
                DataTable dtComId = new DataTable();
                dtComId.Columns.Add("Id");

                DataTable dtComText = ConvertToDataTable(comTextStr, ',', "CompanyName", 2);

                for (int i = 0; i < dtComText.Rows.Count; i++)
                {
                    DataRow[] _Row = dt.Select("ComName='" + dtComText.Rows[i]["CompanyName"].ToString() + "'");
                    if (_Row.Count() > 0)
                    {
                        DataRow dRow = dtComId.NewRow();
                        dRow["Id"] = _Row[0]["Id"];
                        dtComId.Rows.Add(dRow);
                    }
                }

                return dtComId;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private DataTable getDepartmentIdList(string departTextStr, DataTable dt)
        {
            try
            {
                DataTable dtDepartId = new DataTable();
                dtDepartId.Columns.Add("Id");
                dtDepartId.Columns.Add("CompanyId");

                DataTable dtComText = ConvertToDataTable(departTextStr, ',', "DepartmentName", 2);

                for (int i = 0; i < dtComText.Rows.Count; i++)
                {
                    DataRow[] _Row = dt.Select("DepartName='" + dtComText.Rows[i]["DepartmentName"].ToString() + "'");
                    if (_Row.Count() > 0)
                    {
                        DataRow dRow = dtDepartId.NewRow();
                        dRow["Id"] = _Row[0]["Id"];
                        dRow["CompanyId"] = _Row[0]["CompanyId"];
                        dtDepartId.Rows.Add(dRow);
                    }
                }

                return dtDepartId;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion P_Action=3

        #region P_Action=2

        private void bindRepeaterG(int gId)
        {
            try
            {
                //LAY DANH SACH CONG TY
                cls_Module_Authority_Employee aEmp = new cls_Module_Authority_Employee();
                aEmp.EmployeeId = Convert.ToInt32(Session["UserId"]);

                DataTable dt = aEmp.GetCompanyByAuthorityCode(ConfigurationSettings.AppSettings["PermissionAuthority"]);
                ddlCompanySelect1.DataSource = dt;
                ddlCompanySelect1.DataBind();
                ViewState["DT_COMPANY"] = dt;
                ViewState["DT_DEPARTMENT"] = new cls_DepartmentInformation().SelectList(0);

                bindGroupList(gId);

                short moduleId;
                isChange = true;
                dt = new cls_Module_Authority_Group().SelectByGId(Convert.ToInt32(Session["UserId"]), ConfigurationSettings.AppSettings["Emp_Module"],
                                                            ConfigurationSettings.AppSettings["PermissionAuthority"], gId, out moduleId);

                List<cls_Module_Authority_Employee> list = getList(dt, moduleId);
                ViewState["DT_GAUTHORITY"] = dt;

                rptData1.DataSource = list;
                rptData1.DataBind();
            }
            catch (Exception ex)
            {
            }
        }

        private void bindGroupList(int gId)
        {
            try
            {
                cls_Groups group = new cls_Groups();
                group.CompanyId = Convert.ToInt16(ddlCompanySelect1.SelectedValue);
                DataTable dt = group.SelectByCompanyId();

                if (gId > 0)
                {
                    DataRow[] _row = dt.Select("GroupId=" + gId);

                    if (_row.Count() > 0)
                    {
                        ddlCompanySelect1.SelectedValue = _row[0]["CompanyId"].ToString();
                        ddlGroup.DataSource = dt;
                        ddlGroup.DataBind();
                        ddlGroup.SelectedValue = gId.ToString();

                        ddlCompanySelect1.Enabled = false;
                        ddlGroup.Enabled = false;
                        return;
                    }
                }

                DataRow dtRow = dt.NewRow();
                dtRow["GroupName"] = "-- Lựa chọn --";
                dtRow["GroupId"] = "0";
                dt.Rows.InsertAt(dtRow, 0);

                ddlGroup.DataSource = dt;
                ddlGroup.DataBind();
                ddlGroup.SelectedValue = "0";
            }
            catch (Exception ex)
            {
            }
        }

        protected void ddlCompanySelect1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindGroupList(0);
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            int gId = Convert.ToInt32(ddlGroup.SelectedValue);

            if (gId > 0)
            {
                short moduleId;
                isChange = true;
                DataTable dt = new cls_Module_Authority_Group().SelectByGId(Convert.ToInt32(Session["UserId"]), ConfigurationSettings.AppSettings["Emp_Module"],
                                                            ConfigurationSettings.AppSettings["PermissionAuthority"], gId, out moduleId);

                List<cls_Module_Authority_Employee> list = getList(dt, moduleId);
                ViewState["DT_GAUTHORITY"] = dt;

                rptData1.DataSource = list;
                rptData1.DataBind();
            }
        }

        protected void btnGSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtCompany = (DataTable)ViewState["DT_COMPANY"];
                DataTable dtDepartment = (DataTable)ViewState["DT_DEPARTMENT"];
                DataTable dtOld = (DataTable)ViewState["DT_GAUTHORITY"];

                DataTable dtGAuthority = new DataTable();
                dtGAuthority.Columns.Add("Id", typeof(int));
                dtGAuthority.Columns.Add("ModuleId");
                dtGAuthority.Columns.Add("AuthorityId");
                dtGAuthority.Columns.Add("GroupId");
                dtGAuthority.Columns.Add("CompanyId");
                dtGAuthority.Columns.Add("DepartmentId");
                dtGAuthority.Columns.Add("CreatedTime");

                int g_id;
                short mId = 0;
                g_id = Convert.ToInt32(ddlGroup.SelectedValue);

                if (g_id == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                                    "MyScript", "alert('Bạn phải chọn Nhóm người dùng cần phân quyền trước !');", true);
                    ddlGroup.Focus();
                    return;
                }

                for (int i = 0; i < rptData1.Items.Count; i++)
                {
                    RepeaterItem item = rptData1.Items[i];

                    var chkSelectA = (CheckBox)item.FindControl("chkSelectA");
                    var lblAuthorityId = (Label)item.FindControl("lblAuthorityId");
                    var lblModuleId = (Label)item.FindControl("lblModuleId");
                    mId = Convert.ToInt16(lblModuleId.Text);
                    var lbAuthorityName = (Label)item.FindControl("lbAuthorityName");

                    var txtComName = (TextBox)item.FindControl("txtComName");
                    var txtDepartName = (TextBox)item.FindControl("txtDepartName");

                    if (chkSelectA.Checked == true)
                    {
                        if (txtComName.Text.Length <= 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                                    "MyScript", "alert('Bạn phải chọn Công ty cho Quyền \"" + lbAuthorityName.Text + "\" !');", true);
                            txtComName.Focus();
                            return;
                        }
                        if (txtDepartName.Visible == true && txtDepartName.Text.Length <= 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                                    "MyScript", "alert('Bạn phải chọn Phòng/Ban cho Quyền \"" + lbAuthorityName.Text + "\" !');", true);
                            txtDepartName.Focus();
                            return;
                        }

                        if (txtDepartName.Visible == true)
                        {
                            DataTable dtDepartmentId = getDepartmentIdList(txtDepartName.Text, dtDepartment);

                            for (int j = 0; j < dtDepartmentId.Rows.Count; j++)
                            {
                                DataRow dRow = dtGAuthority.NewRow();
                                DataRow[] _Row = dtOld.Select("AuthorityId=" + lblAuthorityId.Text + " AND DepartmentId=" + dtDepartmentId.Rows[j]["Id"].ToString());

                                if (_Row.Count() > 0)
                                    dRow["Id"] = _Row[0]["Id"];
                                else
                                    dRow["Id"] = "0";

                                dRow["ModuleId"] = lblModuleId.Text;
                                dRow["AuthorityId"] = lblAuthorityId.Text;
                                dRow["GroupId"] = g_id;
                                dRow["CompanyId"] = dtDepartmentId.Rows[j]["CompanyId"];
                                dRow["DepartmentId"] = dtDepartmentId.Rows[j]["Id"];
                                dRow["CreatedTime"] = DateTime.Now;

                                dtGAuthority.Rows.Add(dRow);
                            }
                        }
                        else
                        {
                            DataTable dtCompanyId = getCompanyIdList(txtComName.Text, dtCompany);

                            for (int j = 0; j < dtCompanyId.Rows.Count; j++)
                            {
                                DataRow dRow = dtGAuthority.NewRow();
                                DataRow[] _Row = dtOld.Select("AuthorityId=" + lblAuthorityId.Text + " AND CompanyId=" + dtCompanyId.Rows[j]["Id"].ToString());

                                if (_Row.Count() > 0)
                                    dRow["Id"] = _Row[0]["Id"];
                                else
                                    dRow["Id"] = "0";

                                dRow["ModuleId"] = lblModuleId.Text;
                                dRow["AuthorityId"] = lblAuthorityId.Text;
                                dRow["GroupId"] = g_id;
                                dRow["CompanyId"] = dtCompanyId.Rows[j]["Id"];
                                dRow["DepartmentId"] = 0;
                                dRow["CreatedTime"] = DateTime.Now;

                                dtGAuthority.Rows.Add(dRow);
                            }
                        }
                    }
                }
                int resultUpdate = new cls_Module_Authority_Group().ExecuteUpdateById(dtGAuthority, g_id, mId);

                if (resultUpdate > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                        "MyScript", "alert('Cập nhật phân quyền thao tác cho Nhóm người dùng thành công!');", true);
                    Response.Redirect("Permission.aspx");
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                        "MyScript", "alert('Có lỗi trong quá trình cập nhật, vui lòng thử lại !!!');", true);
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion P_Action=2
    }
}
