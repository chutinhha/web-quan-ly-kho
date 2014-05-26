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
using System.Drawing;
using System.Text;
namespace QLCV.N_Su
{
    public partial class PageReport : System.Web.UI.Page
    {
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

                LoadReport(Convert.ToInt32(Session["UserId"].ToString()));
                   
            }
            if (cboReport.Items.Count <= 0)
            {
                Response.Redirect("~/ErrorPermission.aspx");
            }
            LoadControlToPage(Convert.ToInt32(cboReport.SelectedValue)); 
               
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
                        sb.Append("<td class=\"ltab_off\"><img src=\"../images/1x1.gif\" height=\"27\" width=\"7\"></td>" +
                                  "  <td class=\"mainlevel-top\" nowrap=\"nowrap\"><a href=\"Permission.aspx\">Phân quyền</a></td>" +
                                  "  <td class=\"rtab_off\"><img src=\"../images/1x1.gif\" height=\"26\" width=\"7\"></td>" +
                                  "  <td id=\"tspacer\"><img src=\"../images/1x1.gif\" height=\"1\" width=\"1\"></td>");
                    }

                    if (dt.Rows[i]["AuthorityCode"].ToString() == ConfigurationSettings.AppSettings["SalaryAuthority"])
                    {
                        sb.Append("<td class=\"ltab_off\"><img src=\"../images/1x1.gif\" height=\"27\" width=\"7\"></td>" +
                                  "  <td class=\"mainlevel-top\" nowrap=\"nowrap\"><a href=\"Calculate_Salary.aspx\">Tiền Lương</a></td>" +
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
                sb.Append("<td class=\"ltab_on\"><img src=\"../images/1x1.gif\" height=\"26\" width=\"7\"></td>" +
                                  "  <td class=\"mainlevel-top-selected\" nowrap=\"nowrap\"><a href=\"\">Báo cáo</a></td>" +
                                  "  <td class=\"rtab_on\"><img src=\"../images/1x1.gif\" height=\"26\" width=\"7\"></td>" +
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
        private void LoadReport(int UserID)
        {
            cls_Report_User objUser = new cls_Report_User();
            DataTable _dt = new cls_Report_User().GetFollowUserId_ModuleCode(UserID, ConfigurationSettings.AppSettings["Emp_Module"]);
            cboReport.DataValueField = "ID_Report";
            cboReport.DataTextField = "Report_Name";
            cboReport.DataSource = _dt;
            cboReport.DataBind();
            if (cboReport.Items.Count > 0)
            {
                cboReport.SelectedIndex = 0;
               
            }

        }
        private void LoadControlToPage(int ReportId)
        {
            try
            {
                Panel.Controls.Clear();
                ViewState.Clear();
               
                cls_Report_User objUser = new cls_Report_User();
                DataSet _ds = objUser.GetConfigReport(ReportId);
                DataTable _dtConfig = _ds.Tables[0];
                //if (_dtConfig.Rows.Count == 0) return;
                ViewState["Config"] = _dtConfig;
                 if(_ds.Tables[1].Rows.Count>0)
                   ViewState["ConfigUserID"] = 1;
                 else
                     ViewState["ConfigUserID"] = 0;
                short TabIndex = 1;
                Label LB; 
                string[] ArrRow =null;
                int t = 0;
                int k = 0;
                while (k < _dtConfig.Rows.Count)
                {
                    if (ArrRow == null)
                        Array.Resize(ref ArrRow, 1);
                    else
                        Array.Resize(ref ArrRow, ArrRow.Length + 1);
                    if (k < _dtConfig.Rows.Count - 1 && (Convert.ToInt32(_dtConfig.Rows[k]["Width"]) + Convert.ToInt32(_dtConfig.Rows[k + 1]["Width"]) <= (848 - 848 / 3)))
                    {

                        ArrRow[t] = k.ToString() + ":" + (k + 1).ToString();
                        k++;
                    }
                    else
                    {
                        ArrRow[t] = k.ToString();
                    }
                    t++;
                    k++;
                }
                //Lay max text
                
                string strMax = "";
                for (k = 0; k < ArrRow.Length; k++)
                {
                    if (ArrRow[k] != null && ArrRow[k].Length > 0)
                    {
                        if (ArrRow[k].IndexOf(":") >= 0)
                        {
                            string[] ArStr = ArrRow[k].Split(':');
                            if (_dtConfig.Rows[Convert.ToInt32(ArStr[0])]["Parameter_Name"].ToString().Length > strMax.Length)
                                strMax = _dtConfig.Rows[Convert.ToInt32(ArStr[0])]["Parameter_Name"].ToString();
                        }
                        else
                        {
                            if (_dtConfig.Rows[Convert.ToInt32(ArrRow[k])]["Parameter_Name"].ToString().Length > strMax.Length)
                                strMax = _dtConfig.Rows[Convert.ToInt32(ArrRow[k])]["Parameter_Name"].ToString();
                        }
                    }
                }
                strMax += ": ";
                //              
                for (k = 0; k < ArrRow.Length; k++)
                {
                    if (k > 0)
                    {
                        HtmlAnchor te = new HtmlAnchor();
                        te.InnerHtml = "<br />";
                        Panel.Controls.Add(te);
                        HtmlAnchor te1 = new HtmlAnchor();
                        te1.InnerHtml = "<br />";
                        Panel.Controls.Add(te1);
                    }
                    if (ArrRow[k].Length > 0)
                    {
                        if (ArrRow[k].IndexOf(":") >= 0)
                        {
                            string[] ArStr = ArrRow[k].Split(':');
                            for (int j = 0; j < ArStr.Length; j++)
                            {

                                LB = new Label();
                            
                                if (j > 0)
                                {
                                  HtmlAnchor tex = new HtmlAnchor();
                                  //tex.InnerText =
                                    tex.InnerHtml = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";                                  
                                    Panel.Controls.Add(tex);                            
                                }
                                string str = _dtConfig.Rows[Convert.ToInt32(ArStr[j])]["Parameter_Name"].ToString() + ": ";
                                LB.Text = str;
                                Panel.Controls.Add(LB);
                                if(j==0)
                                if (str.Length < strMax.Length)
                                {
                                    string sadd ="";
                                    for (int y = 0; y <= strMax.Length - str.Length;y++ )
                                        sadd += "&nbsp;";
                                     HtmlAnchor add = new HtmlAnchor();
                                      add.InnerHtml  = sadd;
                                     Panel.Controls.Add(add);
                                }
                               string sCate = _dtConfig.Rows[Convert.ToInt32(ArStr[j])]["Control_Type"].ToString();
                                if (sCate == "TextBox")
                                {
                                    TextBox TB = new TextBox();
                                    TB.ID = _dtConfig.Rows[Convert.ToInt32(ArStr[j])]["Parameter_Code"].ToString();
                                    TB.Height =  Convert.ToInt32(_dtConfig.Rows[Convert.ToInt32(ArStr[j])]["Height"]);
                                    TB.Width = Convert.ToInt32(_dtConfig.Rows[Convert.ToInt32(ArStr[j])]["Width"]);
                                    TB.TabIndex = TabIndex;
                                    Panel.Controls.Add(TB);
                                }
                                if (sCate == "DatePicker")
                                {
                                    RadDatePicker date = new RadDatePicker();
                                    date.Height = Convert.ToInt32(_dtConfig.Rows[Convert.ToInt32(ArStr[j])]["Height"]);
                                    date.Width = Convert.ToInt32(_dtConfig.Rows[Convert.ToInt32(ArStr[j])]["Width"]);
                                    date.Skin = "Vista";

                                    date.DateInput.DateFormat = "dd/MM/yyyy";
                                    date.ID = _dtConfig.Rows[Convert.ToInt32(ArStr[j])]["Parameter_Code"].ToString();
                                   // date.Culture = new  System.Globalization.CultureInfo("Vietnamese");
                                    date.Font.Bold = true; date.Font.Name = "Times New Roman";
                                    date.Font.Size = 12;
                                    date.SelectedDate = DateTime.Now;
                                    date.TabIndex = TabIndex;
                                    Panel.Controls.Add(date);
                                   
                                }
                                if (sCate == "DateTimePicker")
                                {

                                    RadDateTimePicker date = new RadDateTimePicker();
                                    date.Height = Convert.ToInt32(_dtConfig.Rows[Convert.ToInt32(ArStr[j])]["Height"]);
                                    date.Width = Convert.ToInt32(_dtConfig.Rows[Convert.ToInt32(ArStr[j])]["Width"]);
                                    date.Skin = "Vista";
                                    date.DateInput.DateFormat = "dd/MM/yyyy";
                                    date.ID = _dtConfig.Rows[Convert.ToInt32(ArStr[j])]["Parameter_Code"].ToString();
                                   
                                    // date.Culture = new  System.Globalization.CultureInfo("Vietnamese");
                                    date.Font.Bold = true; date.Font.Name = "Times New Roman";
                                    date.Font.Size = 12;
                                    date.SelectedDate = DateTime.Now;
                                    date.TabIndex = TabIndex;
                                    Panel.Controls.Add(date);
                                }
                                
                                if (sCate == "ComboBox")
                                {
                                    DropDownList com = new DropDownList();
                                   // RadComboBox com = new RadComboBox();
                                  //  com.Skin = "Vista";
                                    com.Height = Convert.ToInt32(_dtConfig.Rows[Convert.ToInt32(ArStr[j])]["Height"]);
                                    com.Width = Convert.ToInt32(_dtConfig.Rows[Convert.ToInt32(ArStr[j])]["Width"]);
                                    com.ID = _dtConfig.Rows[Convert.ToInt32(ArStr[j])]["Parameter_Code"].ToString();
                                   // com.EnableViewState = false;
                                 //   com.EnableVirtualScrolling = true;
                                    string SQL = _dtConfig.Rows[Convert.ToInt32(ArStr[j])]["Data_SQL"].ToString();
                                    if (SQL.Length > 0)
                                    {
                                        DataTable _dt;
                                        if (_dtConfig.Rows[Convert.ToInt32(ArStr[j])]["IsSP"] != null && Convert.ToBoolean(_dtConfig.Rows[Convert.ToInt32(ArStr[j])]["IsSP"]) == true)
                                        {
                                            int Index = SQL.IndexOf('@');
                                            if (Index >= 0)
                                                _dt = objUser.GetSP(SQL.Substring(0, Index), SQL.Substring(Index, SQL.Length - Index), Session["UserId"] == null ? "0" : Session["UserId"].ToString());
                                            else
                                                _dt = objUser.GetSP(SQL);
                                        }
                                        else
                                      _dt = objUser.GetSQL(SQL);
                                        com.DataValueField = "ID";
                                        com.DataTextField = "Name";
                                        com.DataSource = _dt;
                                        com.DataBind();
                                    }
                                    com.TabIndex = TabIndex;
                                    Panel.Controls.Add(com);
                                }
                                 TabIndex++;
                            }

                        }
                        else
                        {
                            LB = new Label();
                            string str = _dtConfig.Rows[Convert.ToInt32(ArrRow[k])]["Parameter_Name"].ToString() + ": ";
                            LB.Text = str;
                            Panel.Controls.Add(LB);
                            
                                if (str.Length < strMax.Length)
                                {
                                    string sadd = "";
                                    for (int y = 0; y <= strMax.Length - str.Length; y++)
                                        sadd += "&nbsp;";
                                    HtmlAnchor add = new HtmlAnchor();
                                    add.InnerHtml = sadd;
                                    Panel.Controls.Add(add);
                                }
                            string sCate = _dtConfig.Rows[Convert.ToInt32(ArrRow[k])]["Control_Type"].ToString();
                            if (sCate == "TextBox")
                            {
                                TextBox TB = new TextBox();
                                TB.ID = _dtConfig.Rows[Convert.ToInt32(ArrRow[k])]["Parameter_Code"].ToString();
                                TB.Height = Convert.ToInt32(_dtConfig.Rows[Convert.ToInt32(ArrRow[k])]["Height"]);
                                TB.Width = Convert.ToInt32(_dtConfig.Rows[Convert.ToInt32(ArrRow[k])]["Width"]);
                                TB.TabIndex = TabIndex;
                                Panel.Controls.Add(TB);
                            }
                            if (sCate == "DatePicker")
                            {
                                RadDatePicker date = new RadDatePicker();
                                date.Height = Convert.ToInt32(_dtConfig.Rows[Convert.ToInt32(ArrRow[k])]["Height"]);
                                date.Width = Convert.ToInt32(_dtConfig.Rows[Convert.ToInt32(ArrRow[k])]["Width"]);
                                date.Skin = "Vista";
                               // date.EnableViewState = false;
                                date.DateInput.DateFormat = "dd/MM/yyyy";
                                date.ID = _dtConfig.Rows[Convert.ToInt32(ArrRow[k])]["Parameter_Code"].ToString();
                                // date.Culture = new  System.Globalization.CultureInfo("Vietnamese");
                                date.Font.Bold = true; date.Font.Name = "Times New Roman";
                                date.Font.Size = 12;
                                date.SelectedDate = DateTime.Now;
                                date.TabIndex = TabIndex;
                                Panel.Controls.Add(date);
                            }
                            if (sCate == "DateTimePicker")
                            {
                                RadDateTimePicker date = new RadDateTimePicker();
                                date.Height = Convert.ToInt32(_dtConfig.Rows[Convert.ToInt32(ArrRow[k])]["Height"]);
                                date.Width = Convert.ToInt32(_dtConfig.Rows[Convert.ToInt32(ArrRow[k])]["Width"]);
                                date.Skin = "Vista";
                              //  date.EnableViewState = false;
                                date.DateInput.DateFormat = "dd/MM/yyyy";
                                date.ID = _dtConfig.Rows[Convert.ToInt32(ArrRow[k])]["Parameter_Code"].ToString();
                                // date.Culture = new  System.Globalization.CultureInfo("Vietnamese");
                                date.Font.Bold = true; date.Font.Name = "Times New Roman";
                                date.Font.Size = 12;
                                date.SelectedDate = DateTime.Now;
                                date.TabIndex = TabIndex;
                                Panel.Controls.Add(date);
                            }

                            if (sCate == "ComboBox")
                            {
                                DropDownList com = new DropDownList();
                               // RadComboBox com = new RadComboBox();
                                //com.Skin = "Vista";
                                com.Height = Convert.ToInt32(_dtConfig.Rows[Convert.ToInt32(ArrRow[k])]["Height"]);
                                com.Width = Convert.ToInt32(_dtConfig.Rows[Convert.ToInt32(ArrRow[k])]["Width"]);
                                com.ID = _dtConfig.Rows[Convert.ToInt32(ArrRow[k])]["Parameter_Code"].ToString();
                               // com.EnableViewState = false;
                               // com.EnableVirtualScrolling = true;
                                string SQL = _dtConfig.Rows[Convert.ToInt32(ArrRow[k])]["Data_SQL"].ToString();
                                if (SQL.Length > 0)
                                {
                                    DataTable _dt;
                                    if (_dtConfig.Rows[Convert.ToInt32(ArrRow[k])]["IsSP"] != null && Convert.ToBoolean(_dtConfig.Rows[Convert.ToInt32(ArrRow[k])]["IsSP"]) == true)
                                    {
                                        int Index = SQL.IndexOf('@');
                                        if (Index >= 0)
                                            _dt = objUser.GetSP(SQL.Substring(0, Index), SQL.Substring(Index, SQL.Length - Index), Session["UserId"] == null ? "0" : Session["UserId"].ToString());
                                        else
                                            _dt = objUser.GetSP(SQL);
                                    }
                                    else
                                        _dt = objUser.GetSQL(SQL);
                                    com.DataValueField = "ID";
                                    com.DataTextField = "Name";
                                    com.DataSource = _dt;
                                    com.DataBind();
                                }
                                com.TabIndex = TabIndex;
                                Panel.Controls.Add(com);
                            }
                        
                            TabIndex++;
                        }
                    }
                  }
                //  
            }
                
            catch (Exception ex)
            {
            }
        }
        private void GetParaValue()
        {
            if (ViewState["Config"] == null) return;
            DataTable _dtConfig = (DataTable)ViewState["Config"];
            DataTable _dtPara = new DataTable();
            _dtPara.Columns.Add("ParaName");
            _dtPara.Columns.Add("ParaValue");
            
            for (int i = 0; i < _dtConfig.Rows.Count; i++)
            {
                string sType = _dtConfig.Rows[i]["Control_Type"].ToString();
                string sName = _dtConfig.Rows[i]["Parameter_Code"].ToString();
                DataRow dr = _dtPara.NewRow();
                dr["ParaName"] = _dtConfig.Rows[i]["Parameter_Code"].ToString();
                if (sType == "TextBox")
                {
                    TextBox tb = (TextBox)Panel.FindControl(sName);
                    dr["ParaValue"] = tb.Text;
                }
                if (sType == "DatePicker")
                {
                    RadDatePicker date = (RadDatePicker)Panel.FindControl(sName);
                    dr["ParaValue"] = date.SelectedDate;
                }
                if (sType == "DateTimePicker")
                {
                    RadDateTimePicker date = (RadDateTimePicker)Panel.FindControl(sName);
                    dr["ParaValue"] = date.SelectedDate;
                }
                if (sType == "ComboBox")
                {

                    DropDownList com = (DropDownList)Panel.FindControl(sName);
                    if(_dtConfig.Rows[i]["Data_Type"].ToString()=="int")
                    dr["ParaValue"] = com.SelectedValue;
                    else
                        dr["ParaValue"] = com.SelectedItem.Text;
                }
                _dtPara.Rows.Add(dr);
                
            }
            if (ViewState["ConfigUserID"] != null && ViewState["ConfigUserID"].ToString() == "1")
            {
                DataRow dr = _dtPara.NewRow();
                dr["ParaName"] = "UserID";
                dr["ParaValue"] = Session["UserId"]==null?"": Session["UserId"].ToString();
                _dtPara.Rows.Add(dr);
            }
            Session["ReportPara"] = _dtPara;
        }
        #endregion

        protected void cboReport_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //if (cboReport.SelectedValue == null) return;
            //LoadControlToPage(Convert.ToInt32(cboReport.SelectedValue));
        }

        protected void btnLapBaoCao_Click(object sender, EventArgs e)
        {
            string sUrl = "../PageSystem/ReportView.aspx?ID_Report=" + cboReport.SelectedValue;
            GetParaValue();
            RadWindow1.NavigateUrl = sUrl;
            RadWindow1.Title = cboReport.SelectedItem.Text;
            RadWindow1.VisibleOnPageLoad = true;
         //   Response.Write("<script type='text/javascript'>detailedresults=window.open('" + sUrl + "');</script>");

          //  Response.Redirect(sUrl);
        }
    }
}
