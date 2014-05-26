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
using QLCV.code.common;

namespace QLCV.Permission
{
    public partial class Admin_System : System.Web.UI.Page
    {
        public int a_action = 0;
         public DataTable _dtFunction;
        protected void Page_Load(object sender, EventArgs e)
        {
            int e_id = 0;
            if (Request["A_action"] == null)
            {
                a_action = 1;
                lblGroupName.Text = "Danh sách Admin";                
            }
            else
            {
                a_action = Convert.ToInt32(Request["A_action"]);
                
                if (Request["E_Id"] != null) e_id = Convert.ToInt32(Request["E_Id"]);

                if (e_id == 0) lblGroupName.Text = "Thêm mới quyền Admin";
                else lblGroupName.Text = "Cập nhật quyền Admin";
            }     
            
            if (!IsPostBack)
            {
                // ---- KIEM TRA LOGIN -----
                if (Session["UserId"] == null)
                {
                    Response.Redirect("~/login.aspx");
                }
                // --- END DT LOGIN -----

                if (checkAdminHTAuthority())
                {
                    if (a_action == 1)
                        bindData();
                    else
                    {
                        bindRepeater(e_id);
                        chkAdminFull.Attributes.Add("onclick", "viewModuleDetails()");
                    }
                }
            }
            _dtFunction = (new clsSystem()).Get_System_Menu(Session["UserId"].ToString());
        }

        private bool checkAdminHTAuthority()
        {
            cls_Module_Authority_Admin maAdmin = new cls_Module_Authority_Admin();
            maAdmin.EmployeeId = Convert.ToInt32(Session["UserId"]);
            int result = maAdmin.SelectAuthority();

            if (result > 0) return true;
            else return false;
        }

        #region List
        private void bindData()
        {
            try
            {
                //LAY DANH SACH CONG TY
                DataTable dt = new cls_Catalog_Company().Select((short)1, Convert.ToInt32(Session["UserId"]));
                ddlCompany.DataSource = dt;
                ddlCompany.DataBind();

                bindListAdmin();
            }
            catch (Exception ex)
            {
            }
        }

        private void bindListAdmin()
        {
            try
            {
                //BIND CAC MODULE
                DataTable dt = new cls_Modules().Select();

                if (dt.Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" <table class=\"basic-table\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">" +
                              "  <tbody><tr>" +
                              "      <td>" +
                              "          <table class=\"form-table\" border=\"0\" cellpadding=\"4\"><tbody>" +
                              "              <tr class=\"gray\">" +
                              "                  <td colspan=\"" + (dt.Rows.Count + 1).ToString() + "\">PHÂN QUYỀN NGƯỜI DÙNG</td>" +
                              "                  <td align=\"right\" colspan=\"2\"><a href=\"Admin_System.aspx?A_action=2\">+Bổ sung</a></td>" +
                              "              </tr>" +
                              "              <tr bgcolor=\"whitesmoke\">" +
                              "                  <td class=\"Normal\" width=\"180px\"> <u>Người đã phân quyền </u></td>" +
                              "                  <td class=\"Normalred\"><b>Admin HT</b></td>");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("                  <td class=\"NormalBold\" align=\"center\">Module " + dt.Rows[i]["ModuleName"].ToString() + "</td>");
                    }
                    sb.Append("                  <td class=\"normal\">Cập nhật</td>"+
                              "              </tr>");

                    DataTable dtEmp = new cls_Module_Authority_Admin().SelectEmpByCom(Convert.ToInt16(ddlCompany.SelectedValue));
                    if (dtEmp.Rows.Count > 0)
                    {
                        DataTable dtAModule = new cls_Module_Authority_Admin().SelectByCompany(Convert.ToInt16(ddlCompany.SelectedValue));

                        for (int i = 0; i < dtEmp.Rows.Count; i++)
                        {
                            DataRow[] _Row = dtAModule.Select("EmployeeId=" + dtEmp.Rows[i]["EmployeeId"].ToString(), "ModuleCode asc");

                            sb.Append("<tr>"+
                                      "     <td class=\"NormalBold\"><img src=\"../images/MRoom/user.gif\" /> "+_Row[0]["EmployName"].ToString()+"</td>");

                            if (_Row[0]["AdminType"].ToString() == "1")
                                sb.Append("     <td class=\"normal\" align=\"center\"><img src=\"../images/Update.gif\" align=\"absmiddle\"></td>");
                            else
                                sb.Append("     <td class=\"normal\" align=\"center\">&nbsp;</td>");

                            for (int j = 0; j < _Row.Count(); j++)
                            {
                                if (_Row[j]["IsUse"].ToString() == "1")
                                    sb.Append("     <Td class=\"normal\" align=\"center\"><img src=\"../images/Update.gif\" align=\"absmiddle\"> </td>");
                                else
                                    sb.Append("     <Td class=\"normal\" align=\"center\">&nbsp;</td>");
                            }

                            

                            sb.Append("     <td class=\"normal\" align=\"center\"><a href=\"Admin_System.aspx?A_action=2&E_Id=" + dtEmp.Rows[i]["EmployeeId"].ToString() + "\">Sửa</a></td>" +
                                      "</tr>");
                        }
                    }


                    sb.Append("              </tbody>" +
                              "          </table>" +
                              "      </td>" +
                              "  </tr>" +
                            "</tbody></table>");

                    ltlListAdmin.Text = sb.ToString();
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindListAdmin();
        }
        #endregion List

        private void bindRepeater(int empId)
        {
            try
            {
                //LAY DANH SACH CONG TY
                DataTable dt = new cls_Catalog_Company().Select((short)1, Convert.ToInt32(Session["UserId"]));
                ddlCompanySelect.DataSource = dt;
                ddlCompanySelect.DataBind();
                ViewState["DT_COMPANY"] = dt;

                bindCboEmployee(empId);

                cls_Module_Authority_Admin aAdmin = new cls_Module_Authority_Admin();
                aAdmin.EmployeeId = empId;

                DataTable dtEAuthority = aAdmin.SelectByEId();
                List<cls_Module_Authority_Admin>  list = getList(dtEAuthority);
                ViewState["DT_EAUTHORITY"] = dtEAuthority;

                rptData.DataSource = list;
                rptData.DataBind();

                if (list[0].AdminType == 1) chkAdminFull.Checked = true;
                setVisibleRepeater();
            }
            catch (Exception ex)
            {
            }
        }

        private void setVisibleRepeater()
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopup", "viewModuleDetails();", true);
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

        protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemIndex != -1 && e.Item.ItemType != ListItemType.Separator)
                {
                    var chkSelectM = (CheckBox)e.Item.FindControl("chkSelectM");
                    var lbModuleName = (Label)e.Item.FindControl("lbModuleName");
                    var lblModuleId = (Label)e.Item.FindControl("lblModuleId");
                    var lblEMId = (Label)e.Item.FindControl("lblEMId");
                    var txtComName = (TextBox)e.Item.FindControl("txtComName");
                    var hplSelectCompany = (HyperLink)e.Item.FindControl("hplSelectCompany");

                    var item = (cls_Module_Authority_Admin)e.Item.DataItem;
                    int index = e.Item.ItemIndex;
                    if (item != null)
                    {
                        chkSelectM.Checked = (item.IsUse == 1) ? true : false;
                        lbModuleName.Text = item.ModuleName;
                        lblModuleId.Text = item.ModuleId.ToString();
                        txtComName.Text = getCompanyTextStr(item.Company);
                        lblEMId.Text = item.Id.ToString();

                        hplSelectCompany.Attributes.Add("onclick", "javascript:newWindow('SelectCompany.aspx?Index=" + (index + 1).ToString() + "','window2')");
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private string getCompanyTextStr(string comIdStr)
        {
            string result="";
            try
            {
                DataTable dtComId = ConvertToDataTable(comIdStr, ';', "CompanyId", 1);
                DataTable dt = (DataTable)ViewState["DT_COMPANY"];

                for (int i = 0; i < dtComId.Rows.Count; i++)
                {
                    DataRow[] _Row = dt.Select("Id=" + dtComId.Rows[i]["CompanyId"].ToString());
                    if (_Row.Count() > 0)
                        result += _Row[0]["ComName"].ToString()+",";
                }

                if (result.Length > 0)
                    result = result.Substring(0, result.Length - 1);
            }
            catch (Exception ex)
            {
            }
            return result;
        }

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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtCompany = (DataTable)ViewState["DT_COMPANY"];
                DataTable dtEModule = new DataTable();
                dtEModule.Columns.Add("Id", typeof(int));
                dtEModule.Columns.Add("EmployeeId");
                dtEModule.Columns.Add("ModuleId");
                dtEModule.Columns.Add("CompanyId");
                dtEModule.Columns.Add("AdminType");
                dtEModule.Columns.Add("IsUse");
                dtEModule.Columns.Add("CreatedTime");

                int e_id;
                e_id = Convert.ToInt32(cboEmployee.SelectedValue);

                if (e_id == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                                "MyScript", "alert('Bạn phải chọn Nhân viên cần phân quyền trước !');", true);
                    cboEmployee.Focus();
                    return;
                }

                DataTable dtEAuthority = (DataTable)ViewState["DT_EAUTHORITY"];

                for (int i = 0; i < rptData.Items.Count; i++)
                {
                    RepeaterItem item = rptData.Items[i];

                    var chkSelectM = (CheckBox)item.FindControl("chkSelectM");
                    var lbModuleName = (Label)item.FindControl("lbModuleName");
                    var lblModuleId = (Label)item.FindControl("lblModuleId");
                    var txtComName = (TextBox)item.FindControl("txtComName");
                    var lblEMId = (Label)item.FindControl("lblEMId");

                    if(chkAdminFull.Checked ==false)
                        if ((chkSelectM.Checked) && (txtComName.Text.Length <= 0))
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                                "MyScript", "alert('Bạn phải chọn công ty cho Module \""+lbModuleName.Text+"\" !');", true);
                            txtComName.Focus();
                            return;
                        }

                    DataTable dtCompanyId;
                    
                    if(chkAdminFull.Checked)
                        dtCompanyId = dtCompany;
                    else
                        dtCompanyId = getCompanyIdList(txtComName.Text, dtCompany);

                    for (int j = 0; j < dtCompanyId.Rows.Count; j++)
                    {
                        DataRow dRow = dtEModule.NewRow();
                        DataRow[] _Row = dtEAuthority.Select("ModuleId=" + lblModuleId.Text + " AND CompanyId=" + dtCompanyId.Rows[j]["Id"].ToString());

                        if (_Row.Count() > 0)
                            dRow["Id"] = _Row[0]["Id"];
                        else
                            dRow["Id"] = "0";

                        dRow["EmployeeId"] = e_id;
                        dRow["ModuleId"] = lblModuleId.Text;
                        dRow["CompanyId"] = dtCompanyId.Rows[j]["Id"];
                        dRow["AdminType"] = (chkAdminFull.Checked) ? 1 : 2;

                        dRow["IsUse"] = (short)((chkAdminFull.Checked) ? 1 : 0);
                        if (dRow["IsUse"].ToString() == "0") dRow["IsUse"] = (short)((chkSelectM.Checked) ? 1 : 0);
                        dRow["CreatedTime"] = DateTime.Now;

                        dtEModule.Rows.Add(dRow);
                    }
                }

                int result = new cls_Module_Authority_Admin().ExecuteUpdateByPId(dtEModule, e_id);
                if (result > 0)
                {
                    cls_Module_Authority_Admin aAdmin = new cls_Module_Authority_Admin();
                    aAdmin.EmployeeId = e_id;
                    result = aAdmin.UpdateByEId();

                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                        "MyScript", "alert('Cập nhật quyền thao tác với Module cho Nhân sự thành công!');", true);
                    Response.Redirect("Admin_System.aspx");
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
                    DataRow[] _Row = dt.Select("ComName='" + dtComText.Rows[i]["CompanyName"].ToString()+"'");
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

        private string getCompanyIdStr(string comTextStr, DataTable dt, bool adminOption)
        {
            string result = "";
            try
            {
                if (adminOption == false)
                {
                    DataTable dtComId = ConvertToDataTable(comTextStr, ',', "CompanyName", 2);

                    for (int i = 0; i < dtComId.Rows.Count; i++)
                    {
                        DataRow[] _Row = dt.Select("ComName=" + dtComId.Rows[i]["CompanyName"].ToString());
                        if (_Row.Count() > 0)
                            result += _Row[0]["Id"].ToString() + ";";
                    }
                }
                else
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        result += dt.Rows[i]["Id"].ToString() + ";";
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
                cls_Module_Authority_Admin aAdmin = new cls_Module_Authority_Admin();
                aAdmin.EmployeeId = empId;

                DataTable dtEAuthority = aAdmin.SelectByEId();
                List<cls_Module_Authority_Admin> list = getList(dtEAuthority);
                ViewState["DT_EAUTHORITY"] = dtEAuthority;

                rptData.DataSource = list;
                rptData.DataBind();

                if (list[0].AdminType == 1) chkAdminFull.Checked = true;
                setVisibleRepeater();
            }
        }

        private List<cls_Module_Authority_Admin> getList(DataTable dt)
        {
            try
            {
                List<cls_Module_Authority_Admin> list = new List<cls_Module_Authority_Admin>();
                cls_Module_Authority_Admin mAAmin;

                DataTable dtModule = new cls_Modules().Select();

                for (int i = 0; i < dtModule.Rows.Count; i++)
                {
                    mAAmin = new cls_Module_Authority_Admin();
                    mAAmin.Company = "";

                    DataRow[] _Row = dt.Select("ModuleId=" + dtModule.Rows[i]["Id"].ToString());

                    for (int j = 0; j < _Row.Count(); j++)
                    {
                        mAAmin.Company += _Row[j]["CompanyId"].ToString() + ";";
                    }

                    mAAmin.Company = mAAmin.Company.Substring(0, mAAmin.Company.Length - 1);
                    if (mAAmin.Company == "0") mAAmin.Company = "";
                    
                    mAAmin.AdminType = Convert.ToInt16(_Row[0]["AdminType"]);
                    mAAmin.IsUse = Convert.ToInt16(_Row[0]["IsUse"]);
                    mAAmin.ModuleName = _Row[0]["ModuleName"].ToString();
                    mAAmin.ModuleId = Convert.ToInt16(_Row[0]["ModuleId"]);
                    mAAmin.Id = 0;

                    list.Add(mAAmin);
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
