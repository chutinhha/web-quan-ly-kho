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
using Telerik.Web.UI;
using Nhansu_Lib.DB_Class;
using System.Text;
using QLCV.code.common;

namespace QLCV.N_Su
{
    public partial class Calculate_Salary : System.Web.UI.Page
    {
        string empAuthorityCode = "";
        private DataTable dtDepart = null;
        public  string _sUrlReport="";
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    bindAuthority();

        //    if (!IsPostBack)
        //    {
        //        // --- KIEM TRA LOGIN ----
        //        if (Session["UserId"] == null)
        //        {
        //            Response.Redirect("~/login.aspx");
        //        }
        //        // --- END KT LOGIN ----

        //        if (empAuthorityCode.Length > 0)
        //        {
        //            addTab();
        //            bindCompanyList();

        //            rcbDepartment_Bind();
        //            //_sUrlReport = " ../PageSystem/ReportView.aspx?CompanyId=" + ddlCompany.SelectedValue + "&YEAR=" + ddlYear.SelectedValue +
        //            //                                                     "&S_Type=" + ddlSType.SelectedValue + "&MONTH=" + ddlMonth.SelectedValue + "&M_Code=Emp";
                  
        //        }
        //        else
        //        {
        //            Response.Redirect("~/ErrorPermission.aspx");
        //        }
        //    }
        //}

        //#region bindAuthority
        //private void bindAuthority()
        //{
        //    try
        //    {
        //        bool isExist = false;
        //        string moduleCode = ConfigurationSettings.AppSettings["Emp_Module"];
        //        int userId = Convert.ToInt32(Session["UserId"]);
        //        cls_Module_Authority_Employee aEmp = new cls_Module_Authority_Employee();
        //        aEmp.EmployeeId = userId;

        //        DataTable dt = aEmp.SelectAuthority(moduleCode);
        //        StringBuilder sb = new StringBuilder();
        //        sb.Append("<tr>" +
        //                  "  <td background=\"../images/NhanSu/MenuTab_Bg.gif\" height=\"40\" valign=\"bottom\" colspan=\"5\">" +
        //                  "  <link href=\"../CSS/Modules/Nhan_Su/TabMenu.css\" rel=\"stylesheet\" type=\"text/css\" />" +
        //                  "  <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">" +
        //                  "  <tbody><tr><td background=\"../images/NhanSu/tbar_lcap_on.gif\" width=\"5\"><img src=\"../images/1x1.gif\" border=\"0\" height=\"1\" width=\"5\"></td>" +
        //                  "  <td align=\"right\" background=\"../images/NhanSu/tbar_bg_on.gif\" nowrap=\"nowrap\" valign=\"middle\" width=\"190\"><img src=\"../images/1x1.gif\" border=\"0\" height=\"1\" width=\"188\"></td>" +
        //                  "  <td background=\"../images/NhanSu/tbar_bg_on.gif\" valign=\"middle\">" +
        //                  "  <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\"><tbody><tr>" +
        //                  "  <td id=\"tspacer\"><img src=\"../images/1x1.gif\" height=\"1\" width=\"1\"></td>");

        //        sb.Append("<td class=\"ltab_off\"><img src=\"../images/1x1.gif\" height=\"27\" width=\"7\"></td>" +
        //                          "  <td class=\"mainlevel-top\" nowrap=\"nowrap\"><a href=\"List.aspx\">Nhân sự</a></td>" +
        //                          "  <td class=\"rtab_off\"><img src=\"../images/1x1.gif\" height=\"26\" width=\"7\"></td>" +
        //                          "  <td id=\"tspacer\"><img src=\"../images/1x1.gif\" height=\"1\" width=\"1\"></td>");

        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            if (dt.Rows[i]["AuthorityCode"].ToString() == ConfigurationSettings.AppSettings["PermissionAuthority"])
        //            {
        //                sb.Append("<td class=\"ltab_off\"><img src=\"../images/1x1.gif\" height=\"27\" width=\"7\"></td>" +
        //                          "  <td class=\"mainlevel-top\" nowrap=\"nowrap\"><a href=\"Permission.aspx\">Phân quyền</a></td>" +
        //                          "  <td class=\"rtab_off\"><img src=\"../images/1x1.gif\" height=\"26\" width=\"7\"></td>" +
        //                          "  <td id=\"tspacer\"><img src=\"../images/1x1.gif\" height=\"1\" width=\"1\"></td>");
        //            }

        //            if (dt.Rows[i]["AuthorityCode"].ToString() == ConfigurationSettings.AppSettings["SalaryAuthority"])
        //            {
        //                empAuthorityCode = dt.Rows[i]["AuthorityCode"].ToString();
        //                sb.Append("<td class=\"ltab_on\"><img src=\"../images/1x1.gif\" height=\"26\" width=\"7\"></td>" +
        //                          "  <td class=\"mainlevel-top-selected\" nowrap=\"nowrap\"><a href=\"\">Tiền lương</a></td>" +
        //                          "  <td class=\"rtab_on\"><img src=\"../images/1x1.gif\" height=\"26\" width=\"7\"></td>" +
        //                          "  <td id=\"tspacer\"><img src=\"../images/1x1.gif\" height=\"1\" width=\"1\"></td>");
        //            }

        //            if (dt.Rows[i]["AuthorityCode"].ToString() == ConfigurationSettings.AppSettings["ManageAuthority"])
        //            {
        //                sb.Append("<td class=\"ltab_off\"><img src=\"../images/1x1.gif\" height=\"27\" width=\"7\"></td>" +
        //                          "  <td class=\"mainlevel-top\" nowrap=\"nowrap\"><a href=\"../DanhMuc/Company.aspx\">Quản Trị</a></td>" +
        //                          "  <td class=\"rtab_off\"><img src=\"../images/1x1.gif\" height=\"26\" width=\"7\"></td>" +
        //                          "  <td id=\"tspacer\"><img src=\"../images/1x1.gif\" height=\"1\" width=\"1\"></td>");
        //            }

        //            if ((dt.Rows[i]["AuthorityCode"].ToString() == ConfigurationSettings.AppSettings["Emp_FullAuthority"]) &&
        //                (isExist == false))
        //            {
        //                isExist = true;
        //                sb.Append("<td class=\"ltab_off\"><img src=\"../images/1x1.gif\" height=\"27\" width=\"7\"></td>" +
        //                          "  <td class=\"mainlevel-top\" nowrap=\"nowrap\"><a href=\"Attendance_Tracking.aspx\">Chấm công</a></td>" +
        //                          "  <td class=\"rtab_off\"><img src=\"../images/1x1.gif\" height=\"26\" width=\"7\"></td>" +
        //                          "  <td id=\"tspacer\"><img src=\"../images/1x1.gif\" height=\"1\" width=\"1\"></td>");
        //            }

        //            if ((dt.Rows[i]["AuthorityCode"].ToString() == ConfigurationSettings.AppSettings["Emp_PartAuthority"]) &&
        //                (isExist == false))
        //            {
        //                isExist = true;
        //                sb.Append("<td class=\"ltab_off\"><img src=\"../images/1x1.gif\" height=\"27\" width=\"7\"></td>" +
        //                          "  <td class=\"mainlevel-top\" nowrap=\"nowrap\"><a href=\"Attendance_Tracking.aspx\">Chấm công</a></td>" +
        //                          "  <td class=\"rtab_off\"><img src=\"../images/1x1.gif\" height=\"26\" width=\"7\"></td>" +
        //                          "  <td id=\"tspacer\"><img src=\"../images/1x1.gif\" height=\"1\" width=\"1\"></td>");
        //            }
        //        }
        //        sb.Append("<td class=\"ltab_off\"><img src=\"../images/1x1.gif\" height=\"27\" width=\"7\"></td>" +
        //                          "  <td class=\"mainlevel-top\" nowrap=\"nowrap\"><a href=\"PageReport.aspx\">Báo cáo</a></td>" +
        //                          "  <td class=\"rtab_off\"><img src=\"../images/1x1.gif\" height=\"26\" width=\"7\"></td>" +
        //                          "  <td id=\"tspacer\"><img src=\"../images/1x1.gif\" height=\"1\" width=\"1\"></td>");

        //        sb.Append("         </tr></tbody></table>" +
        //                  "          </td><td align=\"left\" background=\"../images/NhanSu/tbar_bg_on.gif\" valign=\"middle\" width=\"100%\"><img src=\"/images/tabs/rtabs.gif\" border=\"0\" height=\"27\" width=\"1\"></td><td background=\"../images/NhanSu/tbar_rcap_on.gif\" width=\"5\"><img src=\"../images/pixel.gif\" border=\"0\" height=\"1\" width=\"5\"></td>" +
        //                  "          </tr></tbody></table>" +
        //                  "      </td>" +
        //                  "  </tr>");

        //        ltlAuthorityList.Text = sb.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
        //#endregion bindAuthority

        //#region Tabs
        //private void addTab()
        //{

        //    StringBuilder sb = new StringBuilder();
        //    DataTable dt = new cls_TabEmployee().Select(ConfigurationSettings.AppSettings["EmpM_Salary_Part"]);

        //    string link, name, img;
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        link = dt.Rows[i]["NameLink"].ToString();
        //        img = dt.Rows[i]["LinkImage"].ToString();
        //        name = dt.Rows[i]["Name"].ToString();

        //        sb.Append("<div class=\"TSK_second-level\">" +
        //                     "   <a style='font-weight: normal;' href='" + link + "'><img src=\"" + img + "\" border='0' align='absmiddle'> <b>" + name + "</b></a>" +
        //                    "</div>");
        //    }
        //    ltlTabDetail.Text = sb.ToString();
        //    sb = new StringBuilder();
        //    dt = new cls_TabEmployee().Select(ConfigurationSettings.AppSettings["EmpM_Reward_Part"]);

        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        link = dt.Rows[i]["NameLink"].ToString();
        //        img = dt.Rows[i]["LinkImage"].ToString();
        //        name = dt.Rows[i]["Name"].ToString();

        //        sb.Append("<div class=\"TSK_second-level\">" +
        //                     "   <a style='font-weight: normal;' href='" + link + "'><img src=\"" + img + "\" border='0' align='absmiddle'> <b>" + name + "</b></a>" +
        //                    "</div>");
        //    }
        //    ltlTabReward.Text = sb.ToString();
        //}
        //#endregion Tabs

        //private void bindCompanyList()
        //{
        //    try
        //    {
        //        cls_Module_Authority_Employee aEmp = new cls_Module_Authority_Employee();
        //        aEmp.EmployeeId = Convert.ToInt32(Session["UserId"]);

        //        DataTable dtCom = aEmp.GetWorkingCompany(ConfigurationSettings.AppSettings["Emp_Module"], empAuthorityCode);
        //        DataRow dRow = dtCom.NewRow();
        //        dRow["Id"] = "0";
        //        dRow["ComName"] = "-- Lựa chọn --";

        //        dtCom.Rows.InsertAt(dRow, 0);
        //        ddlCompany.DataSource = dtCom;
        //        ddlCompany.DataBind();
        //        if (ddlCompany.Items.Count > 1)
        //            ddlCompany.SelectedIndex = 1;
        //        for (int i = 2006; i <= (DateTime.Now.AddMonths(-1).Year); i++)
        //            ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));

        //        for (int i = 1; i <= (DateTime.Now.Month); i++)
        //            ddlMonth.Items.Add(new ListItem("Tháng " + i.ToString(), i.ToString()));

        //        ddlYear.SelectedValue = DateTime.Now.AddMonths(-1).Year.ToString();
        //        ddlMonth.SelectedValue = DateTime.Now.AddMonths(-1).Month.ToString();
        //        setOnclickButtons();

        //        DataTable dtTypeFormula = new cls_CONFIG_TEXT().Select_ByCode(ConfigurationSettings.AppSettings["EMP_FORMULA_TYPE"]);
        //        ddlSType.DataSource = dtTypeFormula;
        //        ddlSType.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        //private void setOnclickButtons()
        //{
        //    string monthStr = "tháng " + ddlMonth.SelectedValue + " năm " + ddlYear.SelectedValue;
        //    btnUpdate.Attributes.Add("onclick", "return confirm('Bạn chắc chắn nhập ĐÚNG, ĐỦ thông tin và muốn sinh lương " + monthStr + "?')");
        //    btnClose.Attributes.Add("onclick", "return confirm('Bạn chắc chắn muốn CHỐT LƯƠNG " + monthStr + " của công ty?')");
        //    btnReset.Attributes.Add("onclick", "return confirm('Bạn muốn RESET Lương " + monthStr + " của phòng?')");

        // //   Button2.Attributes.Add("onclick", "return confirm('Bạn muốn RESET Lương " + monthStr + " của phòng?')");
            
        //}

        //private void createColumnsGridView()
        //{
        //    try
        //    {
        //        string monthStr = ddlYear.SelectedValue;
        //        monthStr += (ddlMonth.SelectedValue.Length < 2) ? ("0" + ddlMonth.SelectedValue) : ddlMonth.SelectedValue;
        //        cls_Salary_Attributes sAtt = new cls_Salary_Attributes();
        //        sAtt.CompanyId = Convert.ToInt16(ddlCompany.SelectedValue);
        //        DataTable dtCurrent = sAtt.SelectCurrent(monthStr, ddlSType.SelectedValue);
        //        ViewState["SALARY_COLUMNS"] = dtCurrent;

        //        if (dtCurrent.Rows.Count > 0)
        //        {
        //            string code, name;
        //            int width;

        //            for (int i = 0; i < dtCurrent.Rows.Count; i++)
        //            {
        //                code = dtCurrent.Rows[i]["AttributeCode"].ToString().Trim();
        //                name = dtCurrent.Rows[i]["AttributeName"].ToString().Trim();
        //                width = Convert.ToInt32(dtCurrent.Rows[i]["AttributeWidth"].ToString().Trim());


        //                GridBoundColumn boundColumn = new GridBoundColumn();
        //                boundColumn.HeaderText = name;
        //                boundColumn.HeaderStyle.BorderWidth = Unit.Pixel(1);
        //                boundColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;
        //                boundColumn.HeaderStyle.Width = Unit.Pixel(width);
        //                boundColumn.DataField = code;
        //                boundColumn.ItemStyle.Width = Unit.Pixel(width);
        //                boundColumn.ItemStyle.Font.Bold = true;
        //              //  boundColumn.HeaderStyle.Height = Unit.Pixel(40);
        //                if (code != "EmployeeName")
        //                    boundColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Center;

        //                grid_Salary.MasterTableView.Columns.Add(boundColumn);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        //private void bindData()
        //{
        //    try
        //    {
        //        short isCalculate = -1;
        //        short enterCommonValue = 0;
        //        cls_Salary_Employees sEmployee = new cls_Salary_Employees();

        //        //RadTreeView rTree = (RadTreeView)rcbDepartment.Items[0].FindControl("rtrDepartment");
        //        int idD = findDepartmentId(rcbDepartment.Items[0].Text);

        //        if (idD > 0)
        //        {
        //            string monthStr = ddlYear.SelectedValue;
        //            monthStr += (ddlMonth.SelectedValue.Length < 2) ? ("0" + ddlMonth.SelectedValue) : ddlMonth.SelectedValue;
        //            sEmployee.DepartmentId = idD;                                               //Convert.ToInt32(ddlDepartment.SelectedValue);
        //            DataTable dtSalary = sEmployee.SelectByDepartId(monthStr, ddlSType.SelectedValue, out isCalculate, out enterCommonValue);

        //            if (enterCommonValue == 1)
        //            {
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "EnterCommonValue", "newWindow1('Enter_CommonValue.aspx?CompanyId=" + ddlCompany.SelectedValue + "&M_STR=" + monthStr + "&S_Type=" + ddlSType.SelectedValue + "');", true);
        //                lnkWord.Visible = lnkExcel.Visible = btnUpdate.Visible = btnClose.Visible = btnCommonValue.Visible = btnReset.Visible = lnkReport.Visible = false;
        //                Button2.Visible = false;
                        
        //            }
        //            else
        //            {
        //                SumSalary(ref dtSalary);
        //                //
        //                ViewState["SALARY_DETAILS"] = dtSalary;
                       
        //                grid_Salary.DataSource = dtSalary;
        //                grid_Salary.DataBind();
        //                grid_Salary.Items[grid_Salary.Items.Count - 1].ForeColor = System.Drawing.Color.Red;
        //                for (int i = 0; i < grid_Salary.Items.Count; i++)
        //                {
        //                    grid_Salary.Items[i].ToolTip = dtSalary.Rows[i]["EmployeeName"].ToString();
        //                }

        //                if (isCalculate <= 0)
        //                {
        //                    btnUpdate.Visible = btnClose.Visible = btnCommonValue.Visible = btnReset.Visible = true;
        //                    Button2.Visible = true;
        //                    lblState.Text = "Trạng thái: Đã có số liệu tính lương";
        //                }
        //                else { btnUpdate.Visible = btnClose.Visible = btnCommonValue.Visible = btnReset.Visible = false; Button2.Visible = false;
        //                lblState.Text = "Trạng thái: Đã chốt bảng lương";
        //                }

        //                if (dtSalary.Rows.Count <= 0)
        //                {
        //                    btnUpdate.Visible = btnClose.Visible = btnCommonValue.Visible = btnReset.Visible = false; Button2.Visible = false;
        //                    lblState.Text = "Trạng thái: Chưa nhập thông tin tính lương";
        //                }

        //                if (dtSalary.Rows.Count > 0)
        //                {
        //                    lnkWord.Visible = lnkExcel.Visible = lnkReport.Visible = true;
        //                    lnkReport.OnClientClick = "newWindow2('../PageSystem/ReportView.aspx?CompanyId=" + ddlCompany.SelectedValue + "&YEAR=" + ddlYear.SelectedValue +
        //                                                                  "&S_Type=" + ddlSType.SelectedValue + "&MONTH=" + ddlMonth.SelectedValue + "&M_Code=Emp'); return false";
        //                }
        //                else lnkWord.Visible = lnkExcel.Visible = lnkReport.Visible = false;
        //            }
        //        }
        //        else
        //        {
        //            btnUpdate.Visible = btnClose.Visible = btnCommonValue.Visible = btnReset.Visible = lnkWord.Visible = lnkExcel.Visible = lnkReport.Visible = false;
        //            Button2.Visible = false;
        //            grid_Salary.DataSource = null;
        //            grid_Salary.DataBind();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
        //private void SumSalary(ref DataTable dtSa)
        //{
        //    DataRow dr = dtSa.NewRow();
        //    dr["EmployeeName"] = "Tổng:";
        //    DataTable dtColum = (DataTable)ViewState["SALARY_COLUMNS"];
            
        //    for (int i = 0; i < dtColum.Rows.Count; i++)
        //    {
        //        if (dtColum.Rows[i]["IsNumber"].ToString() == "1")
        //        {
        //            Double dSum = 0;
        //            for (int k = 0; k < dtSa.Rows.Count; k++)
        //            {
        //                string sNum = dtSa.Rows[k][dtColum.Rows[i]["AttributeCode"].ToString()].ToString()==""?"0":dtSa.Rows[k][dtColum.Rows[i]["AttributeCode"].ToString()].ToString();
        //                sNum=sNum.Replace(".","");
        //                sNum = sNum.Replace(",", ".");
        //                try
        //                {
        //                    dSum += Convert.ToDouble(sNum);
        //                }
        //                  catch
        //                {}
        //            }
        //            //
        //            dr[dtColum.Rows[i]["AttributeCode"].ToString()] = new clsCommonFunction().FormatNumber(dSum.ToString(), 3, ".", ",");
        //        }

        //    }
        //    dtSa.Rows.Add(dr);
        //}
        //protected void grid_Salary_PageIndexChanged(object source, GridPageChangedEventArgs e)
        //{
        //    grid_Salary.MasterTableView.Columns.Clear();
        //    createColumnsGridView();
        //    grid_Salary.CurrentPageIndex = e.NewPageIndex;
        //    bindData();
        //}

        //protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        //{
        //    if (e.Argument == "Rebind")
        //    {
        //        grid_Salary.MasterTableView.Columns.Clear();
        //        createColumnsGridView();
        //        bindData();
        //    }
        //    else if (e.Argument == "RebindAndNavigate")
        //    {
        //        int currentPage = grid_Salary.MasterTableView.PageCount - 1;

        //        grid_Salary.MasterTableView.Columns.Clear();
        //        createColumnsGridView();
        //        bindData();
        //    }
        //}

        //#region TreeView

        //protected void rcbDepartment_Bind()
        //{
        //    RadTreeView rtrDepartment = (RadTreeView)rcbDepartment.Items[0].FindControl("rtrDepartment");

        //    if (rtrDepartment != null)
        //    {
        //        rtrDepartment.Nodes.Clear();
        //        rcbDepartment.Items[0].Text = "";
        //        bindTreeView(rtrDepartment);

        //        grid_Salary.MasterTableView.Columns.Clear();
        //        grid_Salary.DataSource = null;
        //        grid_Salary.DataBind();
        //        createColumnsGridView();

        //        if (rtrDepartment.Nodes.Count > 0)
        //        {
        //            rtrDepartment.Nodes[0].Selected = true;
        //            rcbDepartment.Items[0].Text = rtrDepartment.Nodes[0].Text;

        //            bindData();
        //        }
        //    }
        //}

        //private void bindTreeView(RadTreeView rtrDepartment)
        //{
        //    // ---- BIND DU LIEU VAO LISTVIEW --
        //    cls_DepartmentInformation dInfor = new cls_DepartmentInformation();
        //    dInfor.CompanyId = Convert.ToInt16(ddlCompany.SelectedValue);

        //    dtDepart = dInfor.SelectForSalary();
        //    ViewState["DT_DEPARTMENT"] = dtDepart;

        //    //int countDepartment = dtDepart.Rows.Count;
        //    //ShowDSTreeview(0, rtrDepartment);
        //    RadTreeNode node = new RadTreeNode();
        //    node.Text = "--- Lựa chọn ---";
        //    node.Value = "0";

        //    rtrDepartment.Nodes.Add(node);
        //    addNodeTreeView("0", node, dtDepart);
        //}

        //private void addNodeTreeView(string parentId, RadTreeNode parentNode, DataTable dtDepart)
        //{
        //    try
        //    {
        //        DataRow[] _Row = dtDepart.Select("DepartParentId=" + parentId);
        //        if (_Row.Count() > 0)
        //        {
        //            for (int i = 0; i < _Row.Count(); i++)
        //            {
        //                RadTreeNode childNode = new RadTreeNode();
        //                childNode.Text = _Row[i]["DepartName"].ToString();
        //                childNode.Value = _Row[i]["Id"].ToString();

        //                parentNode.Nodes.Add(childNode);
        //                addNodeTreeView(_Row[i]["Id"].ToString(), childNode, dtDepart);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        //private int findDepartmentId(string name)
        //{
        //    DataTable dt = (DataTable)ViewState["DT_DEPARTMENT"];

        //    DataRow[] _Row = dt.Select("DepartName='" + name + "'");

        //    if (_Row.Count() > 0)
        //    {
        //        return Convert.ToInt32(_Row[0]["Id"]);
        //    }
        //    else
        //        return 0;
        //}

        //protected void rtrDepartment_NodeClick(object sender, RadTreeNodeEventArgs e)
        //{
        //    e.Node.Selected = true;
        //    rcbDepartment.Items[0].Text = e.Node.Text;

        //    grid_Salary.MasterTableView.Columns.Clear();
        //    //grid_Salary.CurrentPageIndex = 0;
        //    createColumnsGridView();
        //    bindData();
        //}

        //#endregion TreeView

        //protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    rcbDepartment_Bind();

        //}

        //#region Print, Export Data to Document

        //protected void lnkWord_Click(object sender, EventArgs e)
        //{
        //    Export_To_Word();
        //}

        //protected void lnkExcel_Click(object sender, EventArgs e)
        //{
        //    Export_To_Excel();
        //}
        //protected void lnkExcelAll_Click(object sender, EventArgs e)
        //{
        //    Export_Salary_XSL();
        //}
        //private void Export_To_Word()
        //{
        //    DataTable dtSColumns = (DataTable)ViewState["SALARY_COLUMNS"];
        //    DataTable dtSDetails = (DataTable)ViewState["SALARY_DETAILS"];

        //    for (int i = 0; i < dtSColumns.Rows.Count; i++)
        //    {
        //        dtSDetails.Columns[i].ColumnName = dtSColumns.Rows[i]["AttributeName"].ToString();
        //    }

        //    dtSDetails.Columns.RemoveAt(dtSDetails.Columns.Count - 1);
        //    dtSDetails.Columns.RemoveAt(dtSDetails.Columns.Count - 1);
        //    dtSDetails.Columns.RemoveAt(dtSDetails.Columns.Count - 1);
        //    dtSDetails.Columns.RemoveAt(dtSDetails.Columns.Count - 1);
        //    dtSDetails.AcceptChanges();

        //    string date = rcbDepartment.Items[0].Text + "_" + ddlMonth.SelectedValue + "_" + ddlYear.SelectedValue;
        //    Response.Clear();
        //    Response.AddHeader("content-disposition", "attachment;filename=DanhSachLuong_" + date + ".doc");
        //    Response.Charset = "";
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    Response.ContentType = "application/vnd.word";
        //    Response.ContentEncoding = System.Text.Encoding.Unicode;
        //    Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
        //    System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        //    System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        //    System.Web.UI.WebControls.GridView dg = new System.Web.UI.WebControls.GridView();

        //    dg.DataSource = dtSDetails;
        //    dg.DataBind();

        //    dg.RenderControl(htmlWrite);
        //    Response.Write(stringWrite.ToString());
        //    Response.End();
        //}
        //private void Export_To_Excel()
        //{
        //    DataTable dtSColumns = (DataTable)ViewState["SALARY_COLUMNS"];
        //    DataTable dtSDetails = (DataTable)ViewState["SALARY_DETAILS"];

        //    for (int i = 0; i < dtSColumns.Rows.Count; i++)
        //    {
        //        dtSDetails.Columns[i].ColumnName = dtSColumns.Rows[i]["AttributeName"].ToString();
        //    }
        //    dtSDetails.Columns.RemoveAt(dtSDetails.Columns.Count - 1);
        //    dtSDetails.Columns.RemoveAt(dtSDetails.Columns.Count - 1);
        //    dtSDetails.Columns.RemoveAt(dtSDetails.Columns.Count - 1);
        //    dtSDetails.Columns.RemoveAt(dtSDetails.Columns.Count - 1);

        //    dtSDetails.AcceptChanges();

        //    string date = rcbDepartment.Items[0].Text + "_" + ddlMonth.SelectedValue + "_" + ddlYear.SelectedValue;

        //    System.Collections.Generic.List<string> lstHeaders = new System.Collections.Generic.List<string>();

        //    var query = from m in dtSColumns.AsEnumerable()
        //                               select m.Field<string>("AttributeName");

        //    foreach (var item in query)
        //    {
        //        lstHeaders.Add(item);
        //    }

        //    //QLCV.code.ExcelExporter exporter = new code.ExcelExporter(date, dtSDetails, lstHeaders);
        //    //exporter.WriteContent(Context);

        //    Response.Clear();
        //    Response.AddHeader("content-disposition", "attachment;filename=DSLuong_" + date + ".xls");
        //    Response.Charset = "";

        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    Response.ContentType = "application/vnd.ms-excel";
        //    Response.ContentEncoding = System.Text.Encoding.Unicode;
        //    Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());

        //    System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        //    System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        //    System.Web.UI.WebControls.GridView dg = new System.Web.UI.WebControls.GridView();

        //    dg.DataSource = dtSDetails;
        //    dg.DataBind();


        //    dg.RenderControl(htmlWrite);
        //    Response.Write(stringWrite.ToString());
        //    Response.End();
        //}
        ////private void Export_Salary_XSL()
        ////{
        ////    try
        ////    {
        ////        string monthStr = ddlYear.SelectedValue;
        ////        monthStr += (ddlMonth.SelectedValue.Length < 2) ? ("0" + ddlMonth.SelectedValue) : ddlMonth.SelectedValue;
        ////        int iCompannyID = Convert.ToInt32(ddlCompany.SelectedValue);
        ////        DataSet dsSalary = (new cls_Salary_Employees()).Export_Salary_XSL(iCompannyID, monthStr, ddlSType.SelectedValue);
        ////        DataTable dtSalary = dsSalary.Tables[0];
        ////        DataTable dtDepart = dsSalary.Tables[1];
        ////        DataTable dtCode = dsSalary.Tables[2];
               
        ////        for (int i = 0; i < dtDepart.Rows.Count; i++)
        ////        {
        ////            DataRow dr = dtSalary.NewRow();
        ////            dr["STT"] = "I." + i.ToString();
        ////            dr["EmployeeName"] = dtDepart.Rows[i]["NAME"].ToString().ToUpper();
        ////            DataRow[] _row = dtSalary.Select("DepartmentID=" + dtDepart.Rows[i]["ID"].ToString());
        ////            for (int k = 0; k < dtCode.Rows.Count; k++)
        ////            {
        ////                if (dtCode.Rows[k]["SumTotal"].ToString() == "1")
        ////                {
        ////                    long dTong = 0;
        ////                    for (int t = 0; t < _row.Length; t++)
        ////                    {
        ////                        try
        ////                        {
        ////                            string svalue = _row[t][dtCode.Rows[k]["AttributeCode"].ToString()].ToString() == "" ? "0" : _row[t][dtCode.Rows[k]["AttributeCode"].ToString()].ToString().Replace(".", "");
        ////                            dTong += Convert.ToInt64(svalue);
        ////                        }
        ////                        catch
        ////                        {
        ////                        }
        ////                    }
        ////                    dr[dtCode.Rows[k]["AttributeCode"].ToString()] = new clsCommonFunction().FormatNumber(dTong.ToString(), 3, ".", ",");
        ////                }
        ////            }
        ////            for (int p = 0; p < dtSalary.Rows.Count; p++)
        ////            {
        ////                if (dtSalary.Rows[p]["DepartmentID"].ToString() == dtDepart.Rows[i]["ID"].ToString())
        ////                {
        ////                    dtSalary.Rows.InsertAt(dr, p);
        ////                    dtSalary.AcceptChanges();
        ////                    break;
        ////                }
        ////            }
        ////        }
              
        ////        ////
        ////        for (int i = 0; i < dtSalary.Rows.Count; i++)
        ////        {
        ////            //DataRow _dr = _dt.NewRow();
        ////            for (int k = 0; k < dtCode.Rows.Count; k++)
        ////            {
        ////                if (dtSalary.Rows[i][dtCode.Rows[k]["AttributeCode"].ToString()].ToString().IndexOf(',') >= 0)
        ////                    dtSalary.Rows[i][dtCode.Rows[k]["AttributeCode"].ToString()] = dtSalary.Rows[i][dtCode.Rows[k]["AttributeCode"].ToString()].ToString().Replace(",", ".");
        ////                else
        ////                    dtSalary.Rows[i][dtCode.Rows[k]["AttributeCode"].ToString()] = dtSalary.Rows[i][dtCode.Rows[k]["AttributeCode"].ToString()].ToString().Replace(".", ",");
        ////            }
        ////            //_dt.Rows.Add(_dr);
        ////        }
        ////        //
        ////        DataRow drSum = dtSalary.NewRow();
        ////        drSum["EmployeeName"] = "TỔNG CỘNG";
        ////        for (int k = 0; k < dtCode.Rows.Count; k++)
        ////        {
        ////            double iSum = 0;
        ////            for (int i = 0; i < dtSalary.Rows.Count; i++)
        ////            {
        ////                 try
        ////                {
        ////                    string svalue = dtSalary.Rows[i][dtCode.Rows[k]["AttributeCode"].ToString()].ToString() == "" ? "0" : dtSalary.Rows[i][dtCode.Rows[k]["AttributeCode"].ToString()].ToString().Replace(",", "");
        ////                    iSum += Convert.ToDouble(svalue);
        ////                }
        ////                catch
        ////                {
        ////                }
        ////            }
        ////            drSum[dtCode.Rows[k]["AttributeCode"].ToString()] = new clsCommonFunction().FormatNumber(iSum.ToString(), 3, ",", ".");
        ////        }
        ////        //
        ////        ////////////
        ////        for (int i = 0; i < dtCode.Rows.Count; i++)
        ////        {
        ////            for (int k = 0; k < dtSalary.Columns.Count; k++)
        ////            {
        ////                if (dtSalary.Columns[k].ColumnName.ToUpper() == dtCode.Rows[i]["AttributeCode"].ToString().ToUpper())
        ////                {
        ////                    dtSalary.Columns[k].ColumnName = dtCode.Rows[i]["AttributeName"].ToString();
        ////                    break;
        ////                }
        ////            }
        ////        }
        ////        dtSalary.Rows.Add(drSum);
        ////        dtSalary.Columns.RemoveAt(dtSalary.Columns.Count - 1);
        ////        dtSalary.Columns.RemoveAt(dtSalary.Columns.Count - 1);
        ////        dtSalary.Columns.RemoveAt(dtSalary.Columns.Count - 1);
        ////        dtSalary.Columns.RemoveAt(dtSalary.Columns.Count - 1);
        ////        dtSalary.Columns.RemoveAt(dtSalary.Columns.Count - 1);
        ////        dtSalary.Columns.RemoveAt(1);
        ////        dtSalary.Columns.RemoveAt(1);
        ////        dtSalary.AcceptChanges();
        ////        dtSalary.Columns[1].ColumnName = "Họ và tên";
        ////        //DataTable _dt = new DataTable();
        ////        //for (int k = 0; k < dtSalary.Columns.Count; k++)
        ////        //{
        ////        //    _dt.Columns.Add(dtSalary.Columns[k].ColumnName);
        ////        //    _dt.Columns[dtSalary.Columns[k].ColumnName].DataType = typeof(string);
        ////        //}
              
        ////        string date = rcbDepartment.Items[0].Text + "_" + ddlMonth.SelectedValue + "_" + ddlYear.SelectedValue;
        ////        Response.Clear();
        ////        Response.AddHeader("content-disposition", "attachment;filename=DSLuong_All_" + date + ".xls");
        ////        Response.Charset = "";

        ////        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        ////        Response.ContentType = "application/vnd.ms-excel";
        ////        Response.ContentEncoding = System.Text.Encoding.Unicode;
        ////        Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());

        ////        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        ////        System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);
        ////        System.Web.UI.WebControls.GridView dg = new System.Web.UI.WebControls.GridView();

        ////        dg.DataSource = dtSalary;
        ////        dg.DataBind();
        ////        dg.Font.Name = "Times New Roman";
        ////        for (int i = 0; i < dg.Rows.Count; i++)
        ////        {

        ////            if (dg.Rows[i].Cells[0].Text.IndexOf('.') >= 0)
        ////            {
        ////                dg.Rows[i].ForeColor = System.Drawing.Color.Black;
        ////                dg.Rows[i].Font.Bold = true;
        ////               // dg.Rows[i].Cells[1].Text = dg.Rows[i].Cells[1].Text.ToUpper();
        ////            }
        ////        }
        ////        dg.Rows[dg.Rows.Count-1].ForeColor = System.Drawing.Color.Black;
        ////        dg.Rows[dg.Rows.Count-1].Font.Bold = true;
        ////        dg.Rows[dg.Rows.Count-1].Cells[1].Text = dg.Rows[dg.Rows.Count-1].Cells[1].Text.ToUpper();
        ////        dg.RenderControl(htmlWrite);
        ////        Response.Write(stringWrite.ToString());
        ////        Response.End();
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////    }
        ////}
        //private void Export_Salary_XSL()
        //{
        //    try
        //    {
        //        string sfile = Server.MapPath("~/BangLuong_" + DateTime.Now.ToString("MMyyyy") + ".xls");
        //        string monthStr = ddlYear.SelectedValue;
        //        monthStr += (ddlMonth.SelectedValue.Length < 2) ? ("0" + ddlMonth.SelectedValue) : ddlMonth.SelectedValue;
        //       int CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        //       string sType = ddlSType.SelectedValue;
        //       if (new clsCommonFunction().Export_To_XLS(sfile, CompanyId, monthStr, sType))
        //       {
        //           Response.Clear();
        //           Response.AddHeader("content-disposition", "attachment;filename=BangLuong_" + DateTime.Now.ToString("MMyyyy") + ".xls");
        //           Response.Charset = "";
        //           Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //           Response.ContentType = "application/vnd.ms-excel";
        //           Response.ContentEncoding = System.Text.Encoding.Unicode;
        //           Response.TransmitFile(sfile);
        //           Response.End();
        //       }
        //    }
        //    catch
        //    {
        //    }
        //}
        //protected void lnkReport_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "CreateReport", "newWindow2('../PageSystem/ReportView.aspx?CompanyId=" + ddlCompany.SelectedValue + "&YEAR=" + ddlYear.SelectedValue +
        //                                                                "&S_Type=" + ddlSType.SelectedValue + "&MONTH=" + ddlMonth.SelectedValue + "&M_Code=Emp');", true);
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
        //#endregion Print, Export Data to Document

        //#region Button Click
        //protected void btnUpdate_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        RadTreeView rTree = (RadTreeView)rcbDepartment.Items[0].FindControl("rtrDepartment");
        //        int idD = Convert.ToInt32(rTree.SelectedNode.Value);

        //        cls_Salary_Employees sEmployee = new cls_Salary_Employees();
        //        sEmployee.DepartmentId = idD;
        //        sEmployee.Salary_Month = ddlYear.SelectedValue;
        //        sEmployee.Salary_Month += (ddlMonth.SelectedValue.Length < 2) ? ("0" + ddlMonth.SelectedValue) : ddlMonth.SelectedValue;

        //        int resultCalculate = sEmployee.CalculateSalary(ddlSType.SelectedValue);

        //        if (resultCalculate > 0)
        //        {
        //            grid_Salary.MasterTableView.Columns.Clear();
        //            createColumnsGridView();
        //            bindData();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        //protected void btnClose_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        cls_Salary_Employees sEmployee = new cls_Salary_Employees();
        //        sEmployee.Salary_Month = ddlYear.SelectedValue;
        //        sEmployee.Salary_Month += (ddlMonth.SelectedValue.Length < 2) ? ("0" + ddlMonth.SelectedValue) : ddlMonth.SelectedValue;

        //        cls_Salary_Attributes sAtt = new cls_Salary_Attributes();
        //        sAtt.CompanyId = Convert.ToInt16(ddlCompany.SelectedValue);

        //        int resultBackup = sAtt.BackUp(sEmployee.Salary_Month, ddlSType.SelectedValue);

        //        if (resultBackup > 0)
        //        {
        //            int resultClose = sEmployee.CloseByMonth(Convert.ToInt16(ddlCompany.SelectedValue), ddlSType.SelectedValue);

        //            if (resultClose > 0)
        //            {
        //                grid_Salary.MasterTableView.Columns.Clear();
        //                createColumnsGridView();
        //                bindData();

        //                btnClose.Visible = btnReset.Visible = btnUpdate.Visible = btnCommonValue.Visible = false;
        //                Button2.Visible = false;
        //            }
        //        }
        //        else
        //            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
        //                "MyScript", "alert('Có lỗi khi Chốt lương tháng " + ddlMonth.SelectedValue + "- " + ddlYear.SelectedValue + ", vui lòng KT lại!');", true);
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        //protected void btnCommonValue_Click(object sender, EventArgs e)
        //{
        //    string monthStr = ddlYear.SelectedValue;
        //    monthStr += (ddlMonth.SelectedValue.Length < 2) ? ("0" + ddlMonth.SelectedValue) : ddlMonth.SelectedValue;

        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "EnterCommonValue", "newWindow1('Enter_CommonValue.aspx?CompanyId=" + ddlCompany.SelectedValue + "&M_STR=" + monthStr + "&S_Type=" + ddlSType.SelectedValue + "');", true);
        //}

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        cls_Salary_Employees sEmp = new cls_Salary_Employees();
        //        sEmp.Salary_Month = ddlYear.SelectedValue;
        //        sEmp.Salary_Month += (ddlMonth.SelectedValue.Length < 2) ? ("0" + ddlMonth.SelectedValue) : ddlMonth.SelectedValue;
        //        int idD = findDepartmentId(rcbDepartment.Items[0].Text);
        //        int resultReset = sEmp.Reset_Department(Convert.ToInt16(ddlCompany.SelectedValue), idD, ddlSType.SelectedValue);
        //        if (resultReset > 0)
        //        {
        //            grid_Salary.MasterTableView.Columns.Clear();
        //            RadTreeView rtrDepartment = (RadTreeView)rcbDepartment.Items[0].FindControl("rtrDepartment");

        //            if (rtrDepartment != null)
        //            {
        //                if (rtrDepartment.Nodes.Count > 0)
        //                {
        //                    rtrDepartment.Nodes[0].Selected = true;
        //                    rcbDepartment.Items[0].Text = rtrDepartment.Nodes[0].Text;
        //                    bindData();
        //                }
        //            }
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
        //                "MyScript", "alert('Có lỗi khi Reset Lương của công ty, vui lòng KT lại!');", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
        //#endregion Button Click

        //protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    grid_Salary.MasterTableView.Columns.Clear();
        //    int idD = findDepartmentId(rcbDepartment.Items[0].Text);
        //    if (idD > 0)
        //    {
        //        createColumnsGridView();
        //        bindData();
        //    }
        //    else
        //    {
        //        btnUpdate.Visible = btnClose.Visible = btnCommonValue.Visible = btnReset.Visible = lnkWord.Visible = lnkExcel.Visible = lnkReport.Visible = false;
        //        Button2.Visible = false;
        //    }
        //    setOnclickButtons();
        //}

        //protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string monthSelected = ddlMonth.SelectedValue;
        //    if (ddlYear.SelectedValue == DateTime.Now.AddMonths(-1).Year.ToString())
        //    {
        //        for (int i = 1; i <= (DateTime.Now.AddMonths(-1).Month); i++)
        //            ddlMonth.Items.Add(new ListItem("Tháng " + i.ToString(), i.ToString()));
        //    }
        //    else
        //    {
        //        for (int i = 1; i <= 12; i++)
        //            ddlMonth.Items.Add(new ListItem("Tháng " + i.ToString(), i.ToString()));
        //    }
        //    ddlMonth.SelectedValue = monthSelected;

        //    grid_Salary.MasterTableView.Columns.Clear();
        //    int idD = findDepartmentId(rcbDepartment.Items[0].Text);
        //    if (idD > 0)
        //    {
        //        createColumnsGridView();
        //        bindData();
        //    }
        //    else
        //    {
        //        btnUpdate.Visible = btnClose.Visible = btnCommonValue.Visible = btnReset.Visible = lnkWord.Visible = lnkExcel.Visible = lnkReport.Visible = false;
        //        Button2.Visible = false;
        //    }
        //    setOnclickButtons();
        //}

        //protected void lnkCongThuc_Click(object sender, EventArgs e)
        //{
        //    string monthStr = ddlYear.SelectedValue;
        //    monthStr += (ddlMonth.SelectedValue.Length < 2) ? ("0" + ddlMonth.SelectedValue) : ddlMonth.SelectedValue;

        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "EnterCommonValue", "newWindow1('Formula_Salary_View.aspx?Company=" + ddlCompany.SelectedValue + "&MonthView=" + monthStr + "&ForType=" + ddlSType.SelectedValue + "');", true);
     
        //}

        //protected void btnReset_Depart_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string monthStr = ddlYear.SelectedValue;
        //        monthStr += (ddlMonth.SelectedValue.Length < 2) ? ("0" + ddlMonth.SelectedValue) : ddlMonth.SelectedValue;
        //        int idD = findDepartmentId(rcbDepartment.Items[0].Text);

        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "EnterCommonValue", "newWindow1('Salary_Reset_List.aspx?Company=" + ddlCompany.SelectedValue + "&Depart=" + idD.ToString() + "&MonthView=" + monthStr + "&ForType=" + ddlSType.SelectedValue + "');", true);
     
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //}

        //protected void Button1_Click1(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string monthStr = ddlYear.SelectedValue;
        //        monthStr += (ddlMonth.SelectedValue.Length < 2) ? ("0" + ddlMonth.SelectedValue) : ddlMonth.SelectedValue;
        //        int idD = findDepartmentId(rcbDepartment.Items[0].Text);

        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "EnterCommonValue", "newWindow1('Salary_Reset_List.aspx?Company=" + ddlCompany.SelectedValue + "&Depart=" + idD.ToString() + "&MonthView=" + monthStr + "&ForType=" + ddlSType.SelectedValue + "');", true);

        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        //protected void btnOffWork_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string monthStr = ddlYear.SelectedValue;
        //        monthStr += (ddlMonth.SelectedValue.Length < 2) ? ("0" + ddlMonth.SelectedValue) : ddlMonth.SelectedValue;
        //        int idD = findDepartmentId(rcbDepartment.Items[0].Text);

        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "EnterCommonValue", "newWindow1('Salary_Off_Work_List.aspx?Company=" + ddlCompany.SelectedValue + "&Depart=" + idD.ToString() + "&MonthView=" + monthStr + "&ForType=" + ddlSType.SelectedValue + "');", true);

        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
    }
}
