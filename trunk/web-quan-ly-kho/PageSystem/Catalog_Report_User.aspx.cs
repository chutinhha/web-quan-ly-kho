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
namespace QLCV.PageSystem
{
    public partial class Catalog_Report_User : System.Web.UI.Page
    {
        private cls_Report_User objReport = new cls_Report_User();
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
                LoadGroupUser();
                LoadListReport();
            }

        }
        #region "U_Function"
        private void LoadGroupUser()
        {
            cls_Groups objGroup = new cls_Groups();
            DataTable _dt = objGroup.GetData();
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
            //cls_Catalog_Report objRe = new cls_Catalog_Report();
            //DataTable _dt = objRe.GetData();
            //gridReport.DataSource = _dt;
            //gridReport.DataBind();
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
