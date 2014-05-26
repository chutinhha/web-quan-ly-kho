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
    public partial class DoiTac : System.Web.UI.Page
    {
        private int EditIndex = -1;
     
        protected DataTable dsGroup = null;

        private static bool I_Option;                              // QUYEN INSERT
        private static bool U_Option;                              // QUYEN UPDATE
        private static bool D_Option;
        public DataTable _dtFunction;

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

                clsDoiTac objDoiTac = new clsDoiTac();
                dsGroup = objDoiTac.Select();
                objDoiTac.DoiTac_Id = "0";
              
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
                    clsDoiTac objDoiTac = new clsDoiTac();
                    objDoiTac.DoiTac_Id= m_grid.DataKeys[e.RowIndex].Value.ToString();
                    int status_Delete = objDoiTac.Delete();
                    if (status_Delete == 1)
                    {
                        ltlAnnouncement.Text = "Xóa thành công phòng ban!";
                    }
                    if (status_Delete <= 0)
                    {
                        ltlAnnouncement.Text = "Không xóa được phòng ban đã chọn.";
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
                string TenDoiTac = ((TextBox)row.FindControl("txtUTenDoiTac")).Text;
                string TenVietTat = ((TextBox)row.FindControl("txtUTenVietTat")).Text;
                string DiaChi = ((TextBox)row.FindControl("txtUDiaChi")).Text;
                string Tel = ((TextBox)row.FindControl("txtUTel")).Text;

                string Mobile = ((TextBox)row.FindControl("txtUMobile")).Text;
                string Fax = ((TextBox)row.FindControl("txtUFax")).Text;
                string Email = ((TextBox)row.FindControl("txtUEmail")).Text;
                string TenGiamDoc = ((TextBox)row.FindControl("txtUTenGiamDoc")).Text;
                string TenGiamDoc_Nhan = ((TextBox)row.FindControl("txtUGiamDoc_Nhan")).Text;
                
                string TenDonVi = ((DropDownList)row.FindControl("txtUDonVi")).Text;
               
              //  short updateId = Int16.Parse(m_grid.DataKeys[id].Value.ToString());
                if (m_grid.DataKeys[id].Value.ToString()!= "")
                {
                    clsDoiTac objDoiTac = new clsDoiTac();

                    objDoiTac.DoiTac_Id = m_grid.DataKeys[id].Value.ToString();
                    objDoiTac.TenDoiTac= TenDoiTac;
                    objDoiTac.TenVietTat = TenVietTat;
                    objDoiTac.DiaChi = DiaChi;
                    objDoiTac.Tel = Tel;
                    objDoiTac.Mobile= Mobile;
                    objDoiTac.Fax= Fax;
                    objDoiTac.Email = Email;
                    objDoiTac.TenGiamDoc = TenGiamDoc;
                    objDoiTac.GiamDoc_Nhan = TenGiamDoc_Nhan;
                    objDoiTac.DonVi_Id = TenDonVi;

                    int _result = objDoiTac.Update();
                    if (_result == 1)
                    {
                        ltlAnnouncement.Text = "Sửa đối tác '" + TenDoiTac + "' thành công!";
                    }
                    if (_result < 0)
                    {
                        ltlAnnouncement.Text = "Có lỗi trong quá trình sửa đối tác!";
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
                    clsDoiTac objDoiTac = new clsDoiTac();

                    string TenDoiTac = ((TextBox)row.FindControl("txtITenDoiTac")).Text;
                    string TenVietTat = ((TextBox)row.FindControl("txtITenVietTat")).Text;
                    string DiaChi = ((TextBox)row.FindControl("txtIDiaChi")).Text;
                    string Tel = ((TextBox)row.FindControl("txtITel")).Text;

                    string Mobile = ((TextBox)row.FindControl("txtIMobile")).Text;
                    string Fax = ((TextBox)row.FindControl("txtIFax")).Text;
                    string Email = ((TextBox)row.FindControl("txtIEmail")).Text;
                    string TenGiamDoc = ((TextBox)row.FindControl("txtITenGiamDoc")).Text;
                    string TenGiamDoc_Nhan = ((TextBox)row.FindControl("txtIGiamDoc_Nhan")).Text;

                    string TenDonVi = ((DropDownList)row.FindControl("txtIDonVi")).Text;

                    objDoiTac.TenDoiTac = TenDoiTac;
                    objDoiTac.TenVietTat = TenVietTat;
                    objDoiTac.DiaChi = DiaChi;
                    objDoiTac.Tel = Tel;
                    objDoiTac.Mobile = Mobile;
                    objDoiTac.Fax = Fax;
                    objDoiTac.Email = Email;
                    objDoiTac.TenGiamDoc = TenGiamDoc;
                    objDoiTac.GiamDoc_Nhan = TenGiamDoc_Nhan;
                    objDoiTac.DonVi_Id = TenDonVi;


                    int _result = objDoiTac.Insert();
                    if (_result >= 1)
                    {
                        ltlAnnouncement.Text = "Thêm mới đối tác '" + TenDoiTac + "' thành công!";
                    }
                    if (_result == -1)
                    {
                        ltlAnnouncement.Text = "Đối tác '" + TenDoiTac + "' đã tồn tại, bạn nên kiểm tra lại!";
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
