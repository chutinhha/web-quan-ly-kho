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

namespace QLCV.DanhMuc
{
    public partial class LoaiTbi : System.Web.UI.Page
    {
        private int EditIndex = -1;
     
        protected DataTable dsGroup = null;

        private static bool I_Option;                              // QUYEN INSERT
        private static bool U_Option;                              // QUYEN UPDATE
        private static bool D_Option;
        public DataTable _dtFunction;
        public DataTable _dtDonVi;

        protected void Page_Load(object sender, EventArgs e)
        {
            // ---- KIEM TRA LOGIN -----
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/login.aspx");
            }
            // ---- END KT LOGIN -------

            if (!this.IsPostBack)
            {

                Fill_Combo_Company();
                getAuthorization();
                bindData(-1);
            }
            _dtFunction = (new QLCV.code.common.clsList()).Get_List_Menu(Session["UserId"].ToString());
           
            
        }
        private void Fill_Combo_Company()
        {
            DataTable _dt = (new cls_Catalog_Company()).Select_Permit(Session["UserId"].ToString());
            cboCompany.DataSource = _dt;
            cboCompany.DataBind();
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

                clsLoaiThietBi objLoaiThietBi = new clsLoaiThietBi();
                dsGroup = objLoaiThietBi.Select();
                objLoaiThietBi.LoaiThietBi_Id = "0";
              
                m_grid.EditIndex = index;
                m_grid.DataSource = dsGroup;
                bool add = false;
                if (dsGroup.Rows.Count <= 0)
                {
                    dsGroup.Rows.Add(dsGroup.NewRow());
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

                
                Guid delId = new Guid(m_grid.DataKeys[e.RowIndex].Value.ToString());
                if (m_grid.DataKeys[e.RowIndex].Value.ToString() != "")
                {
                    clsLoaiThietBi objLoaiThietBi = new clsLoaiThietBi();
                    objLoaiThietBi.LoaiThietBi_Id = m_grid.DataKeys[e.RowIndex].Value.ToString();
                    int status_Delete = objLoaiThietBi.Delete();
                    if (status_Delete == 1)
                    {
                        ltlAnnouncement.Text = "Xóa thành công loại thiết bị!";
                    }
                    if (status_Delete <= 0)
                    {
                        ltlAnnouncement.Text = "Không xóa được loại thiết bị đã chọn.";
                    }
                }
            }
            catch (Exception ex)
            {
                ltlAnnouncement.Text = "Lỗi trong quá trình xóa nhóm người dùng: " + ex.ToString();
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

               string TenLoaiThietBi = ((TextBox)row.FindControl("txtUTenLoaiThietBi")).Text;
               string AliasName = ((TextBox)row.FindControl("txtUAliasName")).Text;
               byte IsUsing = (byte)(((DropDownList)row.FindControl("txtUIsUsing")).Text=="1"? 1:0);
               string DonVi = ((DropDownList)row.FindControl("txtUDonVi")).Text;
               
             
              //  short updateId = Int16.Parse(m_grid.DataKeys[id].Value.ToString());
                if (m_grid.DataKeys[id].Value.ToString()!= "")
                {
                    clsLoaiThietBi objLoaiThietBi = new clsLoaiThietBi();

                    objLoaiThietBi.LoaiThietBi_Id = m_grid.DataKeys[id].Value.ToString();
                    objLoaiThietBi.TenLoaiThietBi = TenLoaiThietBi;
                    objLoaiThietBi.AliasName = AliasName;
                    objLoaiThietBi.IsUsing = IsUsing;
                    objLoaiThietBi.DonVi_Id = DonVi;

                    int _result = objLoaiThietBi.Update();
                    if (_result == 1)
                    {
                        ltlAnnouncement.Text = "Sửa đơn vị '" + TenLoaiThietBi + "' thành công!";
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
                    clsLoaiThietBi objLoaiThietBi = new  clsLoaiThietBi();

                    string TenLoaiThietBi = ((TextBox)row.FindControl("txtITenLoaiThietBi")).Text;
                    string AliasName = ((TextBox)row.FindControl("txtIAliasName")).Text;
                    byte IsUsing = (byte)((((DropDownList)row.FindControl("txtIIsUsing")).Text=="1")? 1:0);
                    string DonVi = ((DropDownList)row.FindControl("txtIDonVi")).Text;

                    objLoaiThietBi.TenLoaiThietBi = TenLoaiThietBi;
                    objLoaiThietBi.AliasName = AliasName;
                    objLoaiThietBi.IsUsing = IsUsing;
                    objLoaiThietBi.DonVi_Id = DonVi;

                    int _result = objLoaiThietBi.Insert();
                    if (_result >= 1)
                    {
                        ltlAnnouncement.Text = "Thêm mới loai '" + TenLoaiThietBi + "' thành công!";
                    }
                    if (_result == -1)
                    {
                        ltlAnnouncement.Text = "Thiết bị '" + TenLoaiThietBi + "' đã tồn tại, bạn nên kiểm tra lại!";
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

       
        protected void cboCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindData(-1);
        }
    }
}
