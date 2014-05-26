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
using QLCV.code.common;
namespace QLCV.PageSystem
{
    public partial class Catalog_Parameter_Report : System.Web.UI.Page
    {
        private cls_Catalog_Report_Parameter objPara = new cls_Catalog_Report_Parameter();
        public DataTable _dtFunction;
        protected void Page_Load(object sender, EventArgs e)
        {
            // ---- KIEM TRA LOGIN -----
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/login.aspx");
            }
            // ---- END KT LOGIN -------

            _dtFunction = (new clsSystem()).Get_System_Menu(Session["UserId"].ToString());
 
            if (!IsPostBack)
            {
                Fill_Combo_Module();
                LoadReport();
                LoadDataToCombo();
                ControlForm(0);

                btnDelete.OnClientClick = "javascript:return confirm(' Bạn chắc chắn xoá tham số này chứ ?');";
            }
        }
        #region "U_Function"
        private void Fill_Combo_Module()
        {
            DataTable _dt = (new cls_Modules()).Select();
            cboModule.DataSource = _dt;
            cboModule.DataBind();
        }
        private void ControlForm(int Cate)
        {
            if (Cate == 0)
            {
                grParameter.Enabled = true;
                txtParaCode.ReadOnly = true;
                txtParaName.ReadOnly = true;
                txtSQL.ReadOnly = true;
                txtOrder.ReadOnly = true;
                chkPara.Enabled = false;
                txtHeight.ReadOnly = true;
                txtwidth.ReadOnly = true;
                btnAdd.Text = "Thêm";
                btnEdit.Text = "Sửa";
                btnDelete.Visible = true;
            }
            else
            {
                txtParaName.Focus();
                grParameter.Enabled = false;
                
                txtParaName.ReadOnly = false;
                txtSQL.ReadOnly = false;
                txtOrder.ReadOnly = false;
                chkPara.Enabled = true;
                txtHeight.ReadOnly = false;
                txtwidth.ReadOnly = false;
                btnAdd.Text = "Hủy";
                btnEdit.Text = "Ghi";
                btnEdit.CommandName = Cate.ToString();
                btnDelete.Visible = false;
                if (Cate == 2)
                {
                    txtParaCode.ReadOnly = false;
                    txtParaCode.Text = "";
                    txtParaName.Text = "";
                    txtSQL.Text = "";
                    txtOrder.Text = "0";
                    cboDataType.SelectedIndex = 0;
                    cboDataControl.SelectedIndex = 0;
                    txtParaCode.Focus();
                }
              
            }

        }
        private void LoadReport()
        {
            cls_Catalog_Report objRe = new cls_Catalog_Report();
            DataTable _dt = objRe.GetData( cboModule.SelectedValue);
            cboReport.DataSource = _dt;
            cboReport.DataValueField = "ID_Report";
           cboReport.DataTextField = "Report_Name";
            cboReport.DataBind();
            if (cboReport.Items.Count > 0)
            {
                cboReport.SelectedIndex = 0;
                LoadParameter(cboReport.SelectedValue);
            }
        }
        private void LoadParameter(string ReportID)
        {
            DataTable _dt = objPara.GetParameter(ReportID);
            grParameter.DataSource = _dt;
            grParameter.DataBind();
            if (grParameter.Items.Count > 0)
            {
                grParameter.Items[0].Selected = true;
                GetParaItem(cboReport.SelectedValue, grParameter.SelectedValue.ToString());
            }
            else
            {
                txtParaCode.Text = "";
                txtParaName.Text = "";
                txtSQL.Text = "";
                txtOrder.Text = "0";
                txtwidth.Text = "0";
                txtHeight.Text = "0";
                chkPara.Checked = false;
                cboDataType.SelectedIndex = 0;
                cboDataControl.SelectedIndex = 0;
            }
        }
        private void LoadDataToCombo()
        {
            try
            {
                DataTable _dtType = new DataTable();
                _dtType.Columns.Add("ID",typeof(int));
                _dtType.Columns.Add("Name");
                DataRow dr = _dtType.NewRow();
                dr["ID"] = _dtType.Rows.Count;
                dr["Name"] = "int";
               _dtType.Rows.Add(dr);
               dr = _dtType.NewRow();
               dr["ID"] = _dtType.Rows.Count;
               dr["Name"] = "text";
               _dtType.Rows.Add(dr);
               dr = _dtType.NewRow();
               dr["ID"] = _dtType.Rows.Count;
               dr["Name"] = "Date";
               _dtType.Rows.Add(dr);
               dr = _dtType.NewRow();
               dr["ID"] = _dtType.Rows.Count;
               dr["Name"] = "DateTime";
               _dtType.Rows.Add(dr);
               dr = _dtType.NewRow();
               dr["ID"] = _dtType.Rows.Count;
               dr["Name"] = "long";
               _dtType.Rows.Add(dr);
               cboDataType.DataSource = _dtType;
               cboDataType.DataValueField = "ID";
               cboDataType.DataTextField = "Name";
               cboDataType.DataBind();
               DataTable _dtControl = new DataTable();
               _dtControl.Columns.Add("ID", typeof(int));
               _dtControl.Columns.Add("Name");
               dr = _dtControl.NewRow();
               dr["ID"] = _dtControl.Rows.Count;
               dr["Name"] = "TextBox";
               _dtControl.Rows.Add(dr);
                dr = _dtControl.NewRow();
               dr["ID"] = _dtControl.Rows.Count;
               dr["Name"] = "ComboBox";
               _dtControl.Rows.Add(dr);
               dr = _dtControl.NewRow();
               dr["ID"] = _dtControl.Rows.Count;
               dr["Name"] = "DatePicker";
               _dtControl.Rows.Add(dr);
               dr = _dtControl.NewRow();
               dr["ID"] = _dtControl.Rows.Count;
               dr["Name"] = "DateTimePicker";
               _dtControl.Rows.Add(dr);
               cboDataControl.DataSource = _dtControl;
               cboDataControl.DataValueField = "ID";
               cboDataControl.DataTextField = "Name";
               cboDataControl.DataBind();
            }
            catch (Exception ex)
            {
            }
        }
        private void GetParaItem(string ReportID, string ParaCode)
        {
            if (objPara.GetParameter(ReportID, ParaCode))
            {
                txtParaCode.Text = objPara.Parameter_Code;
                txtParaName.Text = objPara.Parameter_Name;
                txtSQL.Text = objPara.Data_SQL;
                txtOrder.Value = objPara.Order;
                txtHeight.Value = objPara.Height;
                txtwidth.Value = objPara.Width;
                chkPara.Checked = objPara.IsSP;
                for (int i = 0; i < cboDataType.Items.Count; i++)
                    if (cboDataType.Items[i].Text.ToUpper() == objPara.Data_Type.ToUpper())
                    {
                        cboDataType.Items[i].Selected = true;
                        break;
                    }

                for (int i = 0; i < cboDataControl.Items.Count; i++)
                    if (cboDataControl.Items[i].Text.ToUpper() == objPara.Control_Type.ToUpper())
                    {
                        cboDataControl.Items[i].Selected = true;
                        break;
                    }
            }
            else
            {

            }
           
        }
        private bool CheckValid()
        {

            if (txtParaCode.Text.Trim().Equals(""))
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                  "MyScript", "alert('Bạn phải nhập mã tham số!');", true);
                txtParaCode.Focus();
                  return false;
            }

            if (txtParaName.Text.Trim().Equals(""))
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                  "MyScript", "alert('Bạn phải nhập tên tham số!');", true);
                txtParaName.Focus();
                return false;
            }
            return true;

        }
        private void PageToObject()
        {

            objPara.ID_Report = Convert.ToInt32(cboReport.SelectedValue);
            objPara.Parameter_Code = txtParaCode.Text.Trim();
            objPara.Parameter_Name = txtParaName.Text;
            objPara.Data_Type = cboDataType.SelectedItem.Text;
            objPara.Control_Type = cboDataControl.SelectedItem.Text;
            objPara.Data_SQL = txtSQL.Text.Trim();
            objPara.Order = Convert.ToInt16(txtOrder.Value);
            objPara.Height = Convert.ToInt32( txtHeight.Value );
            objPara.Width = Convert.ToInt32( txtwidth.Value);
            objPara.IsSP = chkPara.Checked;
        }
        #endregion

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Thêm")
                ControlForm(2);
            else
            {
                ControlForm(0);
                grParameter_SelectedIndexChanged(sender, e);
            }

        }

        protected void grParameter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grParameter.SelectedValue == null|| cboReport.SelectedValue ==null) return;
            GetParaItem(cboReport.SelectedValue, grParameter.SelectedValue.ToString());
        }

        protected void cboReport_SelectedIndexChanged(object o, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (cboReport.SelectedValue == null) return;
            LoadParameter(cboReport.SelectedValue);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                objPara.ID_Report = Convert.ToInt32(cboReport.SelectedValue);
                objPara.Parameter_Code = grParameter.SelectedValue.ToString();
                if (objPara.Delete() > 0)
                {
                    LoadParameter(cboReport.SelectedValue);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                "MyScript", "alert('Có lỗi khi xóa dữ liệu!');", true);                  
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
            "MyScript", "alert('Có lỗi tại chức năng xóa dữ liêu:" + ex.Message +"');", true);
               
            }
        }
      

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnEdit.Text == "Sửa")
                {
                    ControlForm(1);
                }
                else
                {
                    if (CheckValid() == false) return;
                    PageToObject();
                    if (btnEdit.CommandName == "2")
                    {
                        if ( objPara.Insert() > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                             "MyScript", "alert('Thêm mới thành công tham số báo cáo!');", true);
                                  
                            ControlForm(0);
                            LoadParameter(cboReport.SelectedValue);
                        }
                        else
                           
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                             "MyScript", "alert('Có lỗi khi thêm mới tham số báo cáo!');", true);                    

                        }
                    }
                    else
                    {

                        if (objPara.Update() > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                             "MyScript", "alert('Sửa thành công tham số báo cáo!');", true);
                            
                            ControlForm(0);
                            LoadParameter(cboReport.SelectedValue);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                            "MyScript", "alert('Có lỗi khi sửa mới tham số báo cáo!');", true);
                           

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                           "MyScript", "alert('Có lỗi khi thêm, sửa tham số báo cáo!');", true);
            }
        }

        protected void chkPara_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPara.Checked)
                lblName.Text = "Tên SP:";
            else
                lblName.Text = "Câu truy vấn:";
        }

        protected void cboModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadReport();
        }
    }
}
