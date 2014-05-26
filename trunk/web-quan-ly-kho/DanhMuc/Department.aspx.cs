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

namespace QLCV.DanhMuc
{
    public partial class Department : System.Web.UI.Page
    {
        string empAuthorityCode = "";
        private DataTable dtDepart = null;
        public int dAction;

        protected void Page_Load(object sender, EventArgs e)
        {
            btnDelete.Attributes.Add("onclick", "return confirm('Bạn thật sự muốn xóa thông tin Phòng/Ban này?')");

            dAction = (Request["D_Action"] != null) ? Convert.ToInt32(Request["D_Action"]) : 0;

            if (!this.IsPostBack)
            {
                //Session["UserId"] = 937;
                // ---- KIEM TRA LOGIN -----
                if (Session["UserId"] == null)
                {
                    Response.Redirect("~/login.aspx");
                }
                // --- END DT LOGIN -----

                bindAuthority();
                if (empAuthorityCode.Length > 0)
                {
                    ViewState["countDRoles"] = 0;
                    ViewState["countHName"] = 0;                                 //VIEW

                    bindCompanyList();

                    if (dAction ==0)
                    {
                        bindListDepartment();
                    }
                    else
                    {
                        if (dAction == 2)
                        {
                            Label13.Text = "Thêm mới";
                            btnDelete.Visible = false;
                        }
                        else
                        {
                            ddlBelongCompany.Enabled = false;
                            Label13.Text = "Cập nhật thông tin";
                        }
                        bindData();
                    }                    
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

        private void bindCompanyList()
        {
            try
            {
                cls_Module_Authority_Employee aEmp = new cls_Module_Authority_Employee();
                aEmp.EmployeeId = Convert.ToInt32(Session["UserId"]);

                DataTable dtCom = aEmp.GetWorkingCompany(ConfigurationSettings.AppSettings["Emp_Module"], empAuthorityCode);
                ddlCompany.DataSource = dtCom;
                ddlCompany.DataBind();

                DataRow drow = dtCom.NewRow();
                drow["Id"] = 0;
                drow["ComName"] = "-- Lựa chọn --";
                dtCom.Rows.InsertAt(drow, 0);

                ddlBelongCompany.DataSource = dtCom;
                ddlBelongCompany.DataBind();

                if (Request["C_Id"] != null)
                {
                    ddlCompany.SelectedValue = Request["C_Id"].ToString();
                    ddlBelongCompany.SelectedValue = Request["C_Id"].ToString();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void bindListDepartment()
        {
            try
            {
                cls_DepartmentInformation dInfor = new cls_DepartmentInformation();
                dInfor.CompanyId = Convert.ToInt16(ddlCompany.SelectedValue);

                DataTable dtDInfo = dInfor.SelectDTS();
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < dtDInfo.Rows.Count; i++)
                {
                    sb.Append("<tr>"+
			                  "  <td nowrap=\"nowrap\" align=\"center\">"+
					          "          "+(i+1).ToString()+
                              "      </td><td nowrap=\"nowrap\" align=\"Left\">" + dtDInfo.Rows[i]["ExtendStr"].ToString() +
                              "          <a class=\"NormalBold\" href=\"Department.aspx?D_Id=" + dtDInfo.Rows[i]["Id"].ToString() + "&D_Action=1\">" +
                              "              <acronym title=''>" + dtDInfo.Rows[i]["DepartNameNature"].ToString() +
						      "              </acronym>"+
					          "          </a>"+
                              "      </td><td valign=\"Top\">" + dtDInfo.Rows[i]["Established_Number"].ToString() +
                              "      </td><td align=\"Center\" valign=\"Top\">" + dtDInfo.Rows[i]["Established_Date"].ToString() +
                              "      </td><td valign=\"Top\">" + dtDInfo.Rows[i]["Established_Signer"].ToString() +
                              "      </td><td valign=\"Top\">" + dtDInfo.Rows[i]["LeaderMember"].ToString() +
				              "      </td>"+
		                    "</tr>");
                }

                ltl_ListDepartment.Text = sb.ToString();
            }
            catch (Exception ex)
            {
            }
        }

        /*
        private void bindTreeView()
        {
            trDepartment.Nodes.Clear();
            // ---- BIND DU LIEU VAO LISTVIEW --
            cls_DepartmentInformation dInfor = new cls_DepartmentInformation();
            dInfor.CompanyId = Convert.ToInt16(ddlCompany.SelectedValue);
            DataTable dtDepart = dInfor.SelectByComId();
            ViewState["countDepartment"] = dtDepart.Rows.Count.ToString();

            DataRow[] _Row = dtDepart.Select("DepartParentId=0");
            if (_Row.Count() > 0)
            {
                for (int i = 0; i < _Row.Count(); i++)
                {
                    TreeNode node = new TreeNode();
                    node.Text = _Row[i]["DepartName"].ToString();
                    node.Value = _Row[i]["Id"].ToString();

                    trDepartment.Nodes.Add(node);
                    addNode(_Row[i]["Id"].ToString(), node, dtDepart);
                }
            }
        }

        private void addNode(string parentId, TreeNode parentNode, DataTable dtDepart)
        {
            try
            {
                DataRow[] _Row = dtDepart.Select("DepartParentId=" + parentId);
                if (_Row.Count() > 0)
                {
                    for (int i = 0; i < _Row.Count(); i++)
                    {
                        TreeNode childNode = new TreeNode();
                        childNode.Text = _Row[i]["DepartName"].ToString();
                        childNode.Value = _Row[i]["Id"].ToString();

                        parentNode.ChildNodes.Add(childNode);
                        addNode(_Row[i]["Id"].ToString(), childNode, dtDepart);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
         * */

        private void bindData()
        {
            ckbActive.Checked = true;
           
            if (dAction != 2)
            {
                try
                {
                    //LAY THONG TIN PHONG BAN
                    cls_DepartmentInformation cDepart = new cls_DepartmentInformation();
                    cDepart.Id = Convert.ToInt32(Request["D_Id"]);
                    cDepart = cDepart.Select();

                    if (cDepart != null)
                    {
                        txtDepartName.Text = cDepart.Name;
                        rtxtOrder.Text = cDepart.Order.ToString();
                        ddlBelongCompany.SelectedValue = cDepart.CompanyId.ToString();
                        if(cDepart.Established_Date.CompareTo(new DateTime(1900, 1, 1)) > 0)
                            dateEstablish.SelectedDate = cDepart.Established_Date;

                        txtEstaNumber.Text = cDepart.Established_Number;
                        txtSigner.Text = cDepart.Established_Signer;
                        txtSeatPosition.Text = cDepart.SeatPosition;
                        ckbActive.Checked = (cDepart.IsActive == 1) ? true : false;
                        ckbCalSalary.Checked = (cDepart.CalculateSalary == 1) ? true : false;

                        if (cDepart.CompanyId > 0)
                        {
                            rcbDepartment_Bind();
                            bindRadCombo();
                        }

                        RadTreeView rTree = (RadTreeView)rcbDepartment.Items[0].FindControl("rtrDepartment");
                        RadTreeNode node = rTree.FindNodeByValue(cDepart.Belong_DepartmentId.ToString());
                        node.Selected = true;
                        rcbDepartment.Items[0].Text = node.Text;

                        cboLeader.SelectedValue = cDepart.LeaderId.ToString();
                        cboAdministrator.SelectedValue = cDepart.AdministratorId.ToString();                            
                    }
                    bind_M_Grid_Data(-1);
                    bind_Ls_HonourName(-1);
                }
                catch (Exception ex)
                {
                }
            }
            else                                    //insert them department moi
            {
                bind_M_Grid_Data(-1);
                bind_Ls_HonourName(-1);

                if (ViewState["countHName"].ToString() == "0")
                    lvHonourName.Items[0].Visible = false;
                if (ViewState["countDRoles"].ToString() == "0")
                    m_grid.Rows[0].Visible = false;

                if ((ViewState["countDRoles"].ToString() == "0") && (ViewState["countHName"].ToString() == "0"))
                {
                    //VIEW DU LIEU LEN
                    txtDepartName.Text = "";
                    rtxtOrder.Text = "";
                    txtEstaNumber.Text = "";
                    txtSigner.Text = "";
                }
                rcbDepartment_Bind();
                bindRadCombo();

                RadTreeView rTree = (RadTreeView)rcbDepartment.Items[0].FindControl("rtrDepartment");
                rTree.Nodes[0].Selected = true;
                rcbDepartment.Items[0].Text = rTree.Nodes[0].Text;
                cboAdministrator.SelectedValue = cboLeader.SelectedValue = "0";
            }
        }

        /*

        private void setVisibleComponent(int countTreeView)
        {
            if (countTreeView <= 0)
            {
                txtDepartName.Text = txtEstaNumber.Text = txtSigner.Text = rtxtOrder.Text = rcbDepartment.Items[0].Text = "";
                ckbActive.Checked = ckbCalSalary.Checked = ddlBelongCompany.Enabled = false;
                dateEstablish.SelectedDate = null;
                ddlBelongCompany.SelectedValue = "0";
                cboLeader.DataSource = cboAdministrator.DataSource = null;
                cboAdministrator.DataBind();
                cboLeader.DataBind();

                trDepartment.Visible = cboLeader.Enabled = cboAdministrator.Enabled = rcbDepartment.Enabled = false;
                btnDelete.Enabled = btnUpdate.Enabled = ckbActive.Enabled = ckbCalSalary.Enabled = false;
                txtDepartName.Enabled = txtEstaNumber.Enabled = rtxtOrder.Enabled = txtSigner.Enabled = false;
                lbNoneDepartment.Text = "Công ty chưa có phòng ban. Lựa chọn 'Thêm mới' để thêm Phòng.";
                lbNoneDepartment.Visible = true;

                m_grid.DataSource = lvHonourName.DataSource = null;
                m_grid.DataBind();
                lvHonourName.DataBind();
            }
            else
            {
                trDepartment.Nodes[0].Selected = true;
                ViewState["DeparmentId"] = trDepartment.SelectedNode.Value.ToString();
                trDepartment.Visible = cboLeader.Enabled = cboAdministrator.Enabled = rcbDepartment.Enabled = true;
                btnDelete.Enabled = btnUpdate.Enabled = ckbActive.Enabled = ckbCalSalary.Enabled = ddlBelongCompany.Enabled = true;
                txtDepartName.Enabled = txtEstaNumber.Enabled = rtxtOrder.Enabled = txtSigner.Enabled = true;
                lbNoneDepartment.Visible = false;
            }
        }
         */

        private void bindRadCombo()
        {
            try
            {
                DataTable dtEmployee = new cls_PersonalInformaiton().SelectByCompanyId(Convert.ToInt16(ddlBelongCompany.SelectedValue));
                DataRow dtRow = dtEmployee.NewRow();
                dtRow["EmployeeName"] = "None";
                dtRow["Birthday"] = "01/01/1970";
                dtRow["EmployeeCode"] = "None";
                dtRow["Id"] = "0";
                dtEmployee.Rows.InsertAt(dtRow, 0);

                cboLeader.DataSource = dtEmployee;
                cboAdministrator.DataSource = dtEmployee;
                cboAdministrator.DataBind();
                cboLeader.DataBind();
            }
            catch (Exception ex)
            {
            }
        }

        #region working with gridview
        private void bind_M_Grid_Data(int index)
        {
            if (dAction==1)
            {
                try
                {
                    //LAY DANH SACH NHIEM VU CUA PHONG BAN
                    cls_Department_Roles dRoles = new cls_Department_Roles();
                    dRoles.DepartmentId = Convert.ToInt32(ViewState["DeparmentId"]);
                    DataTable dtRoles = dRoles.Select();
                    m_grid.EditIndex = index;

                    if ((dtRoles == null) || (dtRoles.Rows.Count <= 0))
                    {
                        DataRow dRow = dtRoles.NewRow();
                        dRow["Id"] = "0";
                        dRow["Order"] = "0";
                        dRow["Note"] = "";
                        dRow["Describle"] = "";
                        dtRoles.Rows.Add(dRow);

                        m_grid.DataSource = dtRoles;
                        m_grid.DataBind();
                        m_grid.Rows[0].Visible = false;
                    }
                    else
                    {
                        m_grid.DataSource = dtRoles;
                        m_grid.DataBind();
                    }

                    if (m_grid.Rows.Count > 0)
                    {
                        string confirm = "return confirm('Bạn thật sự muốn xóa row này?')";

                        for (int i = 0; i < m_grid.Rows.Count; i++)
                        {
                            GridViewRow row = m_grid.Rows[i];

                            ImageButton imgB = (ImageButton)row.FindControl("iBDelete");
                            if (imgB != null)
                            {
                                imgB.Attributes.Add("onclick", confirm);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                DataTable dtDRoles = (DataTable)ViewState["dtDRoles"];
                if (dtDRoles == null)
                {
                    dtDRoles = new DataTable();
                    dtDRoles.Columns.Add("Id", typeof(int));
                    dtDRoles.Columns.Add("Order", typeof(int));
                    dtDRoles.Columns.Add("Note", typeof(string));
                    dtDRoles.Columns.Add("Describle", typeof(string));
                }

                if (dtDRoles.Rows.Count <= 0)
                {
                    DataRow dRow = dtDRoles.NewRow();
                    dRow["Id"] = "0";
                    dRow["Order"] = "0";
                    dRow["Note"] = "";
                    dRow["Describle"] = "";
                    dtDRoles.Rows.Add(dRow);

                    m_grid.DataSource = dtDRoles;
                    m_grid.DataBind();
                    m_grid.Rows[0].Visible = false;
                }
                else
                {
                    m_grid.EditIndex = index;
                    m_grid.DataSource = dtDRoles;
                    m_grid.DataBind();
                }

                ViewState["dtDRoles"] = dtDRoles;
            }
            
        }

        protected void m_grid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            bind_M_Grid_Data(e.NewEditIndex);
        }

        protected void m_grid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            bind_M_Grid_Data(-1);
        }

        protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int id = e.RowIndex;
                m_grid.EditIndex = id;

                int delId = (int)m_grid.DataKeys[e.RowIndex].Value;
                if (delId != 0)
                {
                    if (dAction==1)
                    {
                        cls_Department_Roles dRoles = new cls_Department_Roles();
                        dRoles.Id = delId;

                        int status_Delete = dRoles.Delete();
                    }
                    else
                    {
                        DataTable dtDRoles = (DataTable)ViewState["dtDRoles"];
                        dtDRoles.Rows[id].Delete();
                        dtDRoles.AcceptChanges();
                        ViewState["dtDRoles"] = dtDRoles;
                    }
                }
            }
            catch (Exception ex)
            {
                if (dAction==1)
                    ltlAnnouncement.Text = "Lỗi trong quá trình xóa nhiệm vụ Phòng Ban: " + ex.ToString();
            }
            bind_M_Grid_Data(-1);
        }

        protected void m_grid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int id = e.RowIndex;
                m_grid.EditIndex = id;
                GridViewRow row = m_grid.Rows[id];

                string rDes = ((TextBox)row.FindControl("txtUDescrible")).Text;
                string rNote = ((TextBox)row.FindControl("txtUNote")).Text;
                string orderStr = ((TextBox)row.FindControl("txtUOrder")).Text;

                if (!checkMgridData(rDes, orderStr))
                    return;

                int updateId = Int32.Parse(m_grid.DataKeys[id].Value.ToString());
                if (updateId > 0)
                {

                    if (dAction==1)
                    {
                        cls_Department_Roles dRoles = new cls_Department_Roles();

                        dRoles.Id = updateId;
                        dRoles.DepartmentId = Convert.ToInt32(ViewState["DeparmentId"]);
                        dRoles.Note = rNote;
                        dRoles.Describle = rDes;
                        dRoles.Order = int.Parse(orderStr);
                        dRoles.CreatedTime = DateTime.Now;

                        int _result = dRoles.Update();
                    }
                    else
                    {
                        DataTable dtDRoles = (DataTable)ViewState["dtDRoles"];

                        dtDRoles.Rows[id]["Describle"] = rDes;
                        dtDRoles.Rows[id]["Order"] = orderStr;
                        dtDRoles.Rows[id]["Note"] = rNote;
                        ViewState["dtDRoles"] = dtDRoles;
                    }
                }
            }
            catch (Exception ex)
            {
                if (dAction==1)
                    ltlAnnouncement.Text = "Không cập nhật được dữ liệu. Vui lòng liên hệ quản trị: <br>" + ex.Message;
            }

            bind_M_Grid_Data(-1);
        }

        protected void m_grid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string commandName = e.CommandName;
                GridViewRow row = m_grid.FooterRow;
                if (commandName == "Insert")
                {
                    string rDes = ((TextBox)row.FindControl("txtIDescrible")).Text;
                    string rNote = ((TextBox)row.FindControl("txtINote")).Text;
                    string orderStr = ((TextBox)row.FindControl("txtIOrder")).Text;
                    int order = -1;

                    if (!checkMgridData(rDes, orderStr))
                        return;

                    if (dAction==1)
                    {
                        cls_Department_Roles dRoles = new cls_Department_Roles();

                        dRoles.DepartmentId = Convert.ToInt32(ViewState["DeparmentId"]);
                        dRoles.Note = rNote;
                        dRoles.Describle = rDes;
                        dRoles.CreatedTime = DateTime.Now;

                        int _result = dRoles.Insert();
                    }
                    else
                    {
                        ViewState["countDRoles"] = (Convert.ToInt32(ViewState["countDRoles"]) + 1).ToString();
                        DataTable dtDRoles = (DataTable)ViewState["dtDRoles"];

                        if (Convert.ToInt32(ViewState["countDRoles"]) == 1)
                            dtDRoles.Rows[0].Delete();
                        DataRow rowDT = dtDRoles.NewRow();
                        rowDT["Id"] = ViewState["countDRoles"];
                        rowDT["Describle"] = rDes;
                        rowDT["Order"] = orderStr;
                        rowDT["Note"] = rNote;

                        dtDRoles.Rows.Add(rowDT);
                        ViewState["dtDRoles"] = dtDRoles;
                    }
                    bind_M_Grid_Data(-1);
                }
            }
            catch (Exception ex)
            {
                if (dAction==1)
                    ltlAnnouncement.Text = "Khong them moi duoc du lieu. Vui long lien he voi quan tri<br>" + ex.Message;
            }
        }

        private bool checkMgridData(string describle, string orderStr)
        {
            if (describle.Length <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                              "MyScript", "alert('Bạn phải nhập Tên Nhiệm Vụ Trước.');", true);
                return false;
            }

            try
            {
                int order = Convert.ToInt32(orderStr);
                if (order <= 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                              "MyScript", "alert('Thứ Tự của nhiệm vụ phải là Số Nguyên Lớn Hơn 0.');", true);
                    return false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                              "MyScript", "alert('Thứ Tự của nhiệm vụ phải là Số Nguyên Lớn Hơn 0.');", true);
                return false;
            }
            return true;
        }

        #endregion working with gridview
        #region working with listview

        private void bind_Ls_HonourName(int index)
        {
            if (dAction==1)
            {
                try
                {
                    //LAY DANH SACH DANH HIEU CUA PHONG BAN
                    cls_Department_HonourableName dHName = new cls_Department_HonourableName();
                    dHName.DepartmentId = Convert.ToInt32(ViewState["DeparmentId"]);
                    DataTable dtDHName = dHName.Select();
                    lvHonourName.EditIndex = index;

                    if ((dtDHName == null) || (dtDHName.Rows.Count <= 0))
                    {
                        DataRow dRow = dtDHName.NewRow();
                        dRow["Id"] = "0";
                        dRow["Content"] = "";
                        dtDHName.Rows.Add(dRow);

                        lvHonourName.DataSource = dtDHName;
                        lvHonourName.DataBind();
                        lvHonourName.Items[0].Visible = false;
                    }
                    else
                    {
                        lvHonourName.DataSource = dtDHName;
                        lvHonourName.DataBind();
                    }

                    if (lvHonourName.Items.Count > 0)
                    {
                        string confirm = "return confirm('Bạn thật sự muốn xóa row này?')";

                        for (int i = 0; i < lvHonourName.Items.Count; i++)
                        {
                            ImageButton imgB = (ImageButton)lvHonourName.Items[i].FindControl("iBLDelete");
                            if (imgB != null)
                            {
                                imgB.Attributes.Add("onclick", confirm);
                            }
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                DataTable dtHName = (DataTable)ViewState["dtHName"];
                if (dtHName == null)
                {
                    dtHName = new DataTable();
                    dtHName.Columns.Add("Id", typeof(int));
                    dtHName.Columns.Add("Content", typeof(string));
                }

                if (dtHName.Rows.Count <= 0)
                {
                    DataRow dRow = dtHName.NewRow();
                    dRow["Id"] = "0";
                    dRow["Content"] = "";
                    dtHName.Rows.Add(dRow);

                    lvHonourName.DataSource = dtHName;
                    lvHonourName.DataBind();
                    lvHonourName.Items[0].Visible = false;
                }
                else
                {
                    lvHonourName.EditIndex = index;
                    lvHonourName.DataSource = dtHName;
                    lvHonourName.DataBind();
                }

                ViewState["dtHName"] = dtHName;
            }
        }

        protected void lvHonourName_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            bind_Ls_HonourName(e.NewEditIndex);
        }

        protected void lvHonourName_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            bind_Ls_HonourName(-1);
        }

        protected void lvHonourName_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {
                int id = e.ItemIndex;
                lvHonourName.EditIndex = id;

                int delId = (int)lvHonourName.DataKeys[e.ItemIndex].Value;
                if (delId != 0)
                {
                    if (dAction==1)
                    {
                        cls_Department_HonourableName dHName = new cls_Department_HonourableName();
                        dHName.Id = delId;

                        int status_Delete = dHName.Delete();
                    }
                    else
                    {
                        DataTable dtHName = (DataTable)ViewState["dtHName"];
                        dtHName.Rows[id].Delete();
                        dtHName.AcceptChanges();
                        ViewState["dtHName"] = dtHName;
                    }
                }
            }
            catch (Exception ex)
            {
                if (dAction==1)
                    ltlAnnouncement.Text = "Lỗi trong quá trình xóa danh hiệu: " + ex.ToString();
            }
            bind_Ls_HonourName(-1);
        }

        protected void lvHonourName_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                int id = e.ItemIndex;
                lvHonourName.EditIndex = id;
                ListViewItem item = lvHonourName.Items[id];

                string hName = ((TextBox)item.FindControl("txtUHonourName")).Text;
                if (hName.Length <= 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                              "MyScript", "alert('Bạn phải nhập Nội Dung Danh Hiệu trước!!!');", true);
                    return;
                }

                int updateId = Int32.Parse(lvHonourName.DataKeys[id].Value.ToString());
                if (updateId > 0)
                {

                    if (dAction==1)
                    {
                        cls_Department_HonourableName dHName = new cls_Department_HonourableName();

                        dHName.Id = updateId;
                        dHName.DepartmentId = Convert.ToInt32(ViewState["DeparmentId"]);
                        dHName.Content = hName;
                        dHName.CreatedTime = DateTime.Now;

                        int _result = dHName.Update();
                    }
                    else
                    {
                        DataTable dtHName = (DataTable)ViewState["dtHName"];
                        dtHName.Rows[id]["Content"] = hName;
                        ViewState["dtHName"] = dtHName;
                    }
                }
            }
            catch (Exception ex)
            {
                if (dAction==1)
                    ltlAnnouncement.Text = "Khong cap nhat duoc du lieu. Vui long lien he voi quan tri<br>" + ex.Message;
            }

            bind_Ls_HonourName(-1);
        }

        protected void lvHonourName_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            try
            {
                ListViewItem item = lvHonourName.InsertItem;

                string hName = ((TextBox)item.FindControl("txtIHonourName")).Text;
                if (hName.Length <= 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                              "MyScript", "alert('Bạn phải nhập Nội Dung Danh Hiệu trước!!!');", true);
                    return;
                }

                if (dAction==1)
                {
                    cls_Department_HonourableName dHName = new cls_Department_HonourableName();
                    dHName.DepartmentId = Convert.ToInt32(ViewState["DeparmentId"]);
                    dHName.Content = hName;
                    dHName.CreatedTime = DateTime.Now;

                    int _result = dHName.Insert();
                }
                else
                {
                    ViewState["countHName"] = (Convert.ToInt32(ViewState["countHName"]) + 1).ToString();
                    DataTable dtHName = (DataTable)ViewState["dtHName"];

                    if (ViewState["countHName"].ToString() == "1")
                        dtHName.Rows[0].Delete();
                    DataRow rowDT = dtHName.NewRow();
                    rowDT["Id"] = ViewState["countHName"];
                    rowDT["Content"] = hName;

                    dtHName.Rows.Add(rowDT);
                    ViewState["dtHName"] = dtHName;
                }
                bind_Ls_HonourName(-1);
            }
            catch (Exception ex)
            {
                if (dAction==1)
                    ltlAnnouncement.Text = "Khong them moi duoc du lieu. Vui long lien he voi quan tri<br>" + ex.Message;
            }
        }

        #endregion working with listview
        #region Button Click

        private bool checkInputData()
        {
            if (txtDepartName.Text.Length <= 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                              "MyScript", "alert('Bạn phải nhập Tên Phòng/Ban trước.');", true);
                txtDepartName.Focus();
                return false;
            }

            int dOrder = -1;
            try
            {
                dOrder = int.Parse(rtxtOrder.Text);

                if (dOrder <= 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                              "MyScript", "alert('Thứ tự Phòng Ban không hợp lệ, phải lớn hơn 0. Vui lòng kiểm tra lại!!!');", true);
                    rtxtOrder.Focus();
                    
                    return false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                              "MyScript", "alert('Thứ tự Phòng Ban nhập vào: " + rtxtOrder.Text + " không hợp lệ. Vui lòng kiểm tra lại!!!');", true);
                rtxtOrder.Focus();
                
                return false;
            }

            if (ddlCompany.SelectedValue == "0")
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                              "MyScript", "alert('Bạn phải lựa chọn \"Công ty\" trước.');", true);
                ddlCompany.Focus();
                return false;
            }
            
            return true;
        }

        private int findDepartmentId(string name)
        {
            DataTable dt = (DataTable)ViewState["DT_DEPARTMENT"];

            DataRow[] _Row = dt.Select("DepartName='"+name+"'");

            if (_Row.Count() > 0)
            {
                return Convert.ToInt32(_Row[0]["Id"]);
            }
            else
                return 0;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            ltlAnnouncement.Text = "";
            try
            {
                if (checkInputData())
                {
                    cls_DepartmentInformation dInfor = new cls_DepartmentInformation();
                    dInfor.Id = Convert.ToInt32(Request["D_Id"]);
                    dInfor.CreatedTime = DateTime.Now;
                    dInfor.AdministratorId = Convert.ToInt32(cboAdministrator.SelectedValue);
                    dInfor.Belong_DepartmentId = findDepartmentId(rcbDepartment.Items[0].Text);                   //Convert.ToInt32(rTree.SelectedValue);  
                    dInfor.Established_Date = (dateEstablish.SelectedDate == null) ? new DateTime(1900, 1, 1) : (DateTime)dateEstablish.SelectedDate;
                    dInfor.Established_Number = txtEstaNumber.Text;
                    dInfor.Established_Signer = txtSigner.Text;
                    dInfor.IsActive = Convert.ToInt16(ckbActive.Checked);
                    dInfor.CalculateSalary = Convert.ToInt16(ckbCalSalary.Checked);
                    dInfor.LeaderId = Convert.ToInt32(cboLeader.SelectedValue);
                    dInfor.Name = txtDepartName.Text;
                    dInfor.Order = Convert.ToInt32(rtxtOrder.Text);
                    dInfor.CompanyId = Convert.ToInt16(ddlBelongCompany.SelectedValue);
                    dInfor.SeatPosition = txtSeatPosition.Text;

                    int result;
                    if (dAction==1)
                        result = dInfor.Update();
                    else
                    {
                        result = dInfor.Insert();

                        //THEM THONG TIN VE CAC VAI TRO CUA Phong Ban
                        DataTable dtDRoles = (DataTable)ViewState["dtDRoles"];
                        if (dtDRoles.Rows[0]["Id"].ToString() != "0")
                        {
                            int index = 0;
                            cls_Department_Roles dRoles;
                            while (index < dtDRoles.Rows.Count)
                            {
                                DataRow row = dtDRoles.Rows[index];
                                dRoles = new cls_Department_Roles();
                                dRoles.DepartmentId = result;
                                dRoles.Describle = row["Describle"].ToString();
                                dRoles.Note = row["Note"].ToString();
                                dRoles.Order = (int)row["Order"];
                                dRoles.CreatedTime = DateTime.Now;

                                dRoles.Insert();
                                index++;
                            }
                        }

                        //THEM THONG TIN VE CAC DANH HIEU CUA PHONG BAN
                        DataTable dtHName = (DataTable)ViewState["dtHName"];
                        if (dtHName.Rows[0]["Id"].ToString() != "0")
                        {
                            int index1 = 0;
                            cls_Department_HonourableName dHName;

                            while (index1 < dtHName.Rows.Count)
                            {
                                DataRow row = dtHName.Rows[index1];
                                dHName = new cls_Department_HonourableName();
                                dHName.DepartmentId = result;
                                dHName.Content = row["Content"].ToString();
                                dHName.CreatedTime = DateTime.Now;

                                dHName.Insert();
                                index1++;
                            }
                        }

                        ViewState["countDRoles"] = "0"; ViewState["dtDRoles"] = null;
                        ViewState["countHName"] = 0; ViewState["dtHName"] = null;

                        Label13.Text = "Cập nhật thông tin";
                        ddlCompany.SelectedValue = dInfor.CompanyId.ToString();
                        btnDelete.Visible = true;
                    }

                    if (result > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                               "MyScript", "alert('Cập nhật thông tin Phòng Ban " + txtDepartName.Text + " thành công.');", true);
                        Response.Redirect("Department.aspx?C_Id="+ddlBelongCompany.SelectedValue);
                        //bindTreeView();
                    }
                    else if (result == 0)
                         ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                               "MyScript", "alert('Không cập nhật được thông tin Phòng Ban " + txtDepartName.Text + ".');", true);
                    else
                        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                              "MyScript", "alert('Trùng tên Phòng Ban " + txtDepartName.Text + ", không thể cập nhật.');", true);
                }

            }
            catch (Exception ex)
            {
                ltlAnnouncement.Text = "Có lỗi trong quá trình cập nhật thông tin Phòng Ban " + txtDepartName.Text + ": " + ex.ToString();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                cls_DepartmentInformation dInfor = new cls_DepartmentInformation();
                dInfor.Id = Convert.ToInt32(Request["D_Id"]);
                int result = dInfor.Delete();

                ddlCompany.SelectedValue = ddlBelongCompany.SelectedValue;

                if (result > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                                   "MyScript", "alert('Xóa thông tin Phòng Ban thành công.');", true);
                    if (Request.UrlReferrer != null)
                        Response.Redirect(Request.UrlReferrer.ToString());
                    else
                        Response.Redirect("Department.aspx");
                }
                else
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                                   "MyScript", "alert('Không xóa được thông tin Phòng Ban. Vui lòng thử lại !!!');", true);
            }
            catch (Exception ex)
            {
                ltlAnnouncement.Text = "Có lỗi khi xóa thông tin Phòng Ban: " + ex.ToString();
            }
        }

        #endregion Button Click

        /*
        protected void trDepartment_SelectedNodeChanged(object sender, EventArgs e)
        {
            ViewState["DeparmentId"] = Convert.ToInt32(trDepartment.SelectedNode.Value.ToString());
            trDepartment.SelectedNode.Expand();
            
            ltlAnnouncement.Text = "";
            ViewState["D_action"] = "1";
            bindData();
        }*/

        #region TreeView in Combobox

        protected void rcbDepartment_Bind()
        {
            RadTreeView rtrDepartment = (RadTreeView)rcbDepartment.Items[0].FindControl("rtrDepartment");

            if (rtrDepartment != null)
            {
                rtrDepartment.Nodes.Clear();
                bindTreeView(rtrDepartment);
            }
        }

        private void bindTreeView(RadTreeView rtrDepartment)
        {
            // ---- BIND DU LIEU VAO LISTVIEW --
            cls_DepartmentInformation dInfor = new cls_DepartmentInformation();
            dInfor.CompanyId = Convert.ToInt16(ddlBelongCompany.SelectedValue);
            DataTable dtDepart = dInfor.SelectByComId();
            ViewState["DT_DEPARTMENT"] = dtDepart;

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

        protected void rtrDepartment_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            rcbDepartment_Bind();
            e.Node.Selected = true;
            rcbDepartment.Items[0].Text = e.Node.Text;
        }

        #endregion TreeView TreeView in Combobox

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindListDepartment();
        }

        protected void ddlBelongCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            rcbDepartment_Bind();
            bindRadCombo();

            cboAdministrator.SelectedValue = cboLeader.SelectedValue = "0";
            RadTreeView rTree = (RadTreeView)rcbDepartment.Items[0].FindControl("rtrDepartment");
            rTree.Nodes[0].Selected = true;
            rcbDepartment.Items[0].Text = rTree.Nodes[0].Text;
        }

        protected void btnAddDepartment_Click(object sender, EventArgs e)
        {
            Response.Redirect("Department.aspx?D_Action=2&C_Id=" + ddlCompany.SelectedValue);
        }
    }
}
