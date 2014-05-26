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
    public partial class Catalog_Report_GroupUser : System.Web.UI.Page
    {
        private cls_Report_Group objReport = new cls_Report_Group();
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
            gridGroup.DataSource = _dt;
            gridGroup.DataBind();  
        }
        private void LoadListReport()
        {
            //cls_Catalog_Report objRe = new cls_Catalog_Report();
            //DataTable _dt = objRe.GetData();
            //gridReport.DataSource = _dt;
            //gridReport.DataBind();
        }
        private void GetReportFollowGroup(string sIDGroup)
        {
            DataTable _dt = objReport.GetReportFollowGroui(sIDGroup);

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
        private DataTable GetAllGroup()
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
            if (gridGroup.SelectedValue == null) return;
            GetReportFollowGroup(gridGroup.SelectedValue.ToString());
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (gridGroup.SelectedValue == null)
            {
                uscMsgBox.AddMessage("Bạn phải chọn nhóm người dùng", YaBu.MessageBox.uscMsgBox.enmMessageType.Info);
                return;
            }
            if (objReport.Delete(Convert.ToInt32(gridGroup.SelectedValue)) < 0)
            {
                uscMsgBox.AddMessage("Có lỗi khi xóa và cập nhật dữ liệu!", YaBu.MessageBox.uscMsgBox.enmMessageType.Info);
                return;
            }
            DataTable _dt = GetAllGroup();
            if (_dt != null)
            {
                if (objReport.UpdateAllRow(gridGroup.SelectedValue.ToString(), _dt) < 0)
                {
                    uscMsgBox.AddMessage("Có lỗi khi cập nhật dữ liệu!", YaBu.MessageBox.uscMsgBox.enmMessageType.Info);
                    return;
                }
            }
            uscMsgBox.AddMessage("Cập nhật dữ liệu thành công!", YaBu.MessageBox.uscMsgBox.enmMessageType.Info);
        }

    }
}
