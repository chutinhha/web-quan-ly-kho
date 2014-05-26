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
using QLCV.code.common;

namespace QLCV.PageSystem
{
    public partial class Catalog_Report : System.Web.UI.Page
    {
        private int EditIndex = -1;
     
        protected DataTable dsReport = null;

        private static bool I_Option;                              // QUYEN INSERT
        private static bool U_Option;                              // QUYEN UPDATE
        private static bool D_Option;

        public DataTable _dtFunction;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // ---- KIEM TRA LOGIN -----
                if (Session["UserId"] == null)
                {
                    Response.Redirect("~/login.aspx");
                }
                // ---- END KT LOGIN -------
              
                Fill_Combo_Module();
                getAuthorization();
                bindData(-1);
            }
            _dtFunction = (new clsSystem()).Get_System_Menu(Session["UserId"].ToString());
        }
        private void Fill_Combo_Module()
        {
            DataTable _dt = (new cls_Modules()).Select();
            cboModule.DataSource = _dt;
            cboModule.DataBind();
        }
        private void getAuthorization()
        {
            int MenuId = 0;
            int iResult, uResult, dResult;
            
            string idStr = Request.QueryString["M_Id"];
            if (idStr != null)
                MenuId = Convert.ToInt32(idStr);
              I_Option = true;
                U_Option = true;
                D_Option = true;
            if (MenuId > 0)
            {
                cls_Menu_Employees mEmp = new cls_Menu_Employees();
                mEmp.MenuId = MenuId;
                mEmp.EmployeeId = Convert.ToInt32(Session["UserId"].ToString());

                mEmp.CheckAllOptions(out iResult, out uResult, out dResult);

                if (iResult > 0) I_Option = true; else I_Option = false;
                if (uResult > 0) U_Option = true; else U_Option = false;
                if (dResult > 0) D_Option = true; else D_Option = false;
              
            }
            I_Option = true;
            U_Option = true;
            D_Option = true;
        }

        private void bindData(int index)
        {
            try
            {
                //LAY DANH SACH CAC QUOC GIA RA

                cls_Catalog_Report objReport = new cls_Catalog_Report();
                dsReport = objReport.GetData( cboModule.SelectedValue);
                objReport.ID_Report =0;
              
                m_grid.EditIndex = index;
                m_grid.DataSource = dsReport;
                bool add = false;
                if (dsReport.Rows.Count <= 0)
                {
                    dsReport.Rows.Add(dsReport.NewRow());
                    add = true;

                }
              //  
                m_grid.DataBind();
                if(add)
                    m_grid.Rows[0].Visible = false;
                if (m_grid.Rows.Count > 0)
                {
                    string confirm = "return confirm('Bạn thật sự muốn xóa row này?')";

                    for (int i = 0; i < m_grid.Rows.Count; i++)
                    {
                        GridViewRow row = m_grid.Rows[i];
                        LinkButton lbutton1 = (LinkButton)row.FindControl("cmdDelete");
                        if (lbutton1 != null)
                        {
                            lbutton1.Visible = D_Option;
                            lbutton1.Attributes.Add("onclick", confirm);
                            //lbutton1.Enabled = D_Option;
                        }

                        LinkButton lbutton2 = (LinkButton)row.FindControl("cmdEdit");
                        if (lbutton2 != null)
                            lbutton2.Visible = U_Option;
                    }
                }

                //XET QUYETN CHO CHUC NANG INSERT 
                GridViewRow row1 = m_grid.FooterRow;
                if (row1 != null)
                {
                    row1.Visible = I_Option;
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void m_grid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            EditIndex = e.NewEditIndex;
            bindData(EditIndex);
        }

        protected void m_grid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            bindData(-1);
        }

        protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int id = e.RowIndex;
                m_grid.EditIndex = id;

                int delId =Convert.ToInt32(m_grid.DataKeys[e.RowIndex].Value);
                if (delId != 0)
                {
                    cls_Catalog_Report objReport = new cls_Catalog_Report();
                    objReport.ID_Report = delId;

                    int status_Delete = objReport.Delete();
                    if (status_Delete == 1)
                    {
                        ltlAnnouncement.Text = "Xóa thành công báo cáo!";
                    }
                    if (status_Delete <= 0)
                    {
                        ltlAnnouncement.Text = "Không xóa được báo cáo đã chọn.";
                    }
                }
            }
            catch (Exception ex)
            {
                ltlAnnouncement.Text = "Lỗi trong quá trình xóa báo cáo: " + ex.ToString();
            }
            bindData(-1);
        }

        protected void m_grid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int id = e.RowIndex;
                m_grid.EditIndex = id;
                GridViewRow row = m_grid.Rows[id];

                string eName = ((TextBox)row.FindControl("txtUName")).Text;
                string eFilePath = ((TextBox)row.FindControl("txtUFilePath")).Text;
                string eSP = ((TextBox)row.FindControl("txtUSP")).Text;
                string eDetail = ((TextBox)row.FindControl("txtUDetail")).Text;

                short updateId = Int16.Parse(m_grid.DataKeys[id].Value.ToString());
                if (updateId > 0)
                {
                    cls_Catalog_Report objReport = new cls_Catalog_Report();
                    objReport.ID_Report = updateId;
                    objReport.ID_Module = Convert.ToInt32(cboModule.SelectedValue);
                    objReport.Report_Name = eName;
                    objReport.File_Path = eFilePath;
                    objReport.SPName = eSP;
                    objReport.Note = eDetail;
                 

                    int _result = objReport.Update();
                    if (_result == 1)
                    {
                        ltlAnnouncement.Text = "Sửa báo cáo '" + eName + "' thành công!";
                    }
                    if (_result < 0)
                    {
                        ltlAnnouncement.Text = "Có lỗi trong quá trình sửa dữ liệu!";
                    }
                }
            }
            catch (Exception ex)
            {
                ltlAnnouncement.Text = "Khong cap nhat duoc du lieu. Vui long lien he voi quan tri<br>" + ex.Message;
            }

            bindData(-1);
        }

        protected void m_grid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string commandName = e.CommandName;
                GridViewRow row = m_grid.FooterRow;
                if (commandName == "Insert")
                {
                    cls_Catalog_Report objReport = new cls_Catalog_Report();

                    string eName = ((TextBox)row.FindControl("txtIName")).Text;
                    string eFilePath = ((TextBox)row.FindControl("txtIFilePath")).Text;
                    string eSP = ((TextBox)row.FindControl("txtISP")).Text;
                    string eDetail = ((TextBox)row.FindControl("txtIDetail")).Text;
                    objReport.ID_Module = Convert.ToInt32(cboModule.SelectedValue);
                    objReport.Report_Name = eName;
                    objReport.File_Path = eFilePath;
                    objReport.SPName = eSP;
                    objReport.Note = eDetail;
                    int _result = objReport.Insert();
                    if (_result >= 1)
                    {
                        ltlAnnouncement.Text = "Thêm mới báo cáo '" + eName + "' thành công!";
                    }
                    if (_result == -1)
                    {
                        ltlAnnouncement.Text = "báo cáo '" + eName + "' đã tồn tại, bạn nên kiểm tra lại!";
                    }
                    if (_result == 0)
                    {
                        ltlAnnouncement.Text = "Có lỗi trong quá trình thêm mới!";
                    }
                    bindData(-1);
                }
            }
            catch (Exception ex)
            {
                ltlAnnouncement.Text = "Khong them moi duoc du lieu. Vui long lien he voi quan tri<br>" + ex.Message;
            }

        }

        protected void cboModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindData(-1);
        }
    }
}
