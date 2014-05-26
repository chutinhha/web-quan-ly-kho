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
using System.Data.SqlClient;

namespace QLCV.QLKho
{
    public partial class XacNhanXuatKhoChiTiet : System.Web.UI.Page
    {
        private int EditIndex = -1;
     
        protected DataTable dsGroup = null;
        protected DataTable dsTongHop = null;

        private static bool I_Option;                              // QUYEN INSERT
        private static bool U_Option;                              // QUYEN UPDATE
        private static bool D_Option;
        public DataTable _dtFunction;
        
        static bool bsua = false;
        static string sKey = "";
        static string sDienGiai = "";
        static string sCode = "";
        static string sNhanVienPheDuyet = "";
        static string sThongTinThietBi_Id = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            // ---- KIEM TRA LOGIN -----
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/login.aspx");
            }
            // ---- END KT LOGIN -------
            HttpCookie Cookie = Request.Cookies["PhieuNhap_Id"];
            sKey = Cookie["Data"];
            
            
            if (!this.IsPostBack)
            {

                getAuthorization();
                cboThietBi.DataSource = GetSourceThietBi();
                cboThietBi.DataTextField = "TenThietBi";
                cboThietBi.DataValueField = "ThietBi_Id";
                cboThietBi.DataBind();
                //
                cboThietBi_SelectedIndexChanged(sender, e);
                bindData(-1);
            }
            
            _dtFunction = new DataTable();
            _dtFunction = (new QLCV.code.common.clsKhoMain()).Get_List_Menu(Session["UserId"].ToString());

        }
        public System.Data.SqlClient.SqlDataReader GetSourceThietBi()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();// "server='NgoQuangLua-PC'; uid=sa;pwd=sa; Database='QL'; trusted_connection=true;";
            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);

            string queryString = "select a.ThietBi_Id,b.TenThietBi from PhieuXuatChiTiet a,ThietBi b where a.ThietBi_Id=b.ThietBi_Id and a.PhieuXuat_Id='"+ sKey +"'";
            System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(queryString, sqlConnection);

            sqlConnection.Open();
            System.Data.SqlClient.SqlDataReader dataReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            return dataReader;
        }


        protected void btnThoat_Click(object sender, System.EventArgs e)
        {

            Response.Write("<script language='javascript'>window.close();</script>");
            
        }

        protected void btnXacNhan_Click(object sender, System.EventArgs e)
        {
            clsPhieuXuat oPhieuXuat = new clsPhieuXuat();
            oPhieuXuat.NhanVienLogon_Id = "3d2df69a-4d7c-4d78-a4a3-075a169a109d";
            oPhieuXuat.PhieuXuat_Id = sKey;
            oPhieuXuat.ChungTu = null;
            oPhieuXuat.KieuFile = "";
            oPhieuXuat.XacNhanPhieuXuat();
            lblThongBao.Text = "Xác nhận thành công!";
        }

        public void Init()
        {
           
            sKey = Guid.NewGuid().ToString();
            sCode = "";
            sDienGiai = "";
        }

        private void bindData(int index)
        {
            try
            {
                //LAY DANH SACH CAC QUOC GIA RA
                dsGroup = new DataTable();

                //Hiển thị tổng hợp
                clsPhieuXuat oPhieuXuat = new clsPhieuXuat();
                DataTable tblPhieuXuat = new DataTable();
                oPhieuXuat.PhieuXuat_Id = sKey;
                dsGroup = oPhieuXuat.Select_ThongTinThietBi();
                //
                tblPhieuXuat = oPhieuXuat.Select();
                if (tblPhieuXuat.Rows.Count > 0)
                {
                    txtSoPhieu.Text = tblPhieuXuat.Rows[0]["So_Phieu"].ToString();
                    dtpNgayNhap.SelectedDate = Convert.ToDateTime(tblPhieuXuat.Rows[0]["Ngay_Xuat"]);
                    txtNguoiNhan.Text = tblPhieuXuat.Rows[0]["NguoiXuat"].ToString();
                }

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
                if (add)
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
                //
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();// "server='NgoQuangLua-PC'; uid=sa;pwd=sa; Database='QL'; trusted_connection=true;";
                System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);
                string queryString = "select COUNT(*) sl_danhap from ThongTinThietBi a where a.PhieuXuat_Id='"+ sKey +"' and a.ThietBi_Id='"+ cboThietBi.SelectedValue +"'";
                sqlConnection.Open();
                SqlDataAdapter oAdap = new SqlDataAdapter(queryString, sqlConnection);

                DataTable tblTemp = new DataTable();
                oAdap.Fill(tblTemp);
                if (tblTemp.Rows.Count > 0)
                {
                    txtSoLuongDaCoChiTiet.Text = tblTemp.Rows[0]["sl_danhap"].ToString();
                    txtSoLuongCanNhapChiTiet.Text = (Convert.ToInt16(txtSoLuongNhapKho.Text == "" ? "0" : txtSoLuongNhapKho.Text) - Convert.ToInt16(txtSoLuongDaCoChiTiet.Text == "" ? "0" : txtSoLuongDaCoChiTiet.Text)).ToString();
                }
                else
                {
                    txtSoLuongDaCoChiTiet.Text = "0";
                    txtSoLuongCanNhapChiTiet.Text = (Convert.ToInt16(txtSoLuongNhapKho.Text == "" ? "0" : txtSoLuongNhapKho.Text) - Convert.ToInt16(txtSoLuongDaCoChiTiet.Text == "" ? "0" : txtSoLuongDaCoChiTiet.Text)).ToString();
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
                    clsPhieuNhap oPhieuNhap = new clsPhieuNhap();
                    oPhieuNhap.ThongTinThietBi_Id = m_grid.DataKeys[e.RowIndex].Value.ToString();
                    int status_Delete = oPhieuNhap.Delete_ThongTinThietBi();
                    if (status_Delete == 1)
                    {
                        ltlAnnouncement.Text = "Xóa thành công kế hoạch nhập xuất chi tiết!";
                    }
                    if (status_Delete <= 0)
                    {
                        ltlAnnouncement.Text = "Không xóa được kế hoạch nhập xuất chi tiết.";
                    }
                }
            }
            catch (Exception ex)
            {
                ltlAnnouncement.Text = "Lỗi trong quá trình xóa kế hoạch nhập xuất chi tiết: " + ex.ToString();
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
                    clsPhieuXuat oPhieuXuat = new clsPhieuXuat();
                    oPhieuXuat.NhanVienLogon_Id = "3d2df69a-4d7c-4d78-a4a3-075a169a109d";
                    oPhieuXuat.PhieuXuat_Id = sKey;
                    oPhieuXuat.ThongTinThietBi_Id = sThongTinThietBi_Id;

                    oPhieuXuat.InsertChiTietPhieuXuat();
                    bindData(-1);
                    sThongTinThietBi_Id = "";
                }
                else if (commandName == "TimKiem")
                {
                    string Ma_Serial = ((TextBox)row.FindControl("txtIMa_Serial")).Text;
                    string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();// "server='NgoQuangLua-PC'; uid=sa;pwd=sa; Database='QL'; trusted_connection=true;";
                    System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);
                    string queryString = "select * from ThongTinThietBi a where a.Ma_Serial='" + Ma_Serial + "' and a.trang_thai in (1,7)";
                    sqlConnection.Open();
                    SqlDataAdapter oAdap = new SqlDataAdapter(queryString, sqlConnection);

                    DataTable tblTemp = new DataTable();
                    oAdap.Fill(tblTemp);
                    if (tblTemp.Rows.Count > 0)
                    {
                        ((TextBox)row.FindControl("txtIMA_MAC")).Text = tblTemp.Rows[0]["Ma_MAC"].ToString();
                        ((TextBox)row.FindControl("txtIMA_DLK")).Text = tblTemp.Rows[0]["Ma_DLK"].ToString();
                        ((TextBox)row.FindControl("txtIMA_SLC")).Text = tblTemp.Rows[0]["Ma_SLC"].ToString();
                        sThongTinThietBi_Id = tblTemp.Rows[0]["ThongTinThietBi_Id"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ltlAnnouncement.Text = "Khong them moi duoc du lieu. Vui long lien he voi quan tri<br>" + ex.Message;
            }

        }
        protected void m_grid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int id = e.RowIndex;
                m_grid.EditIndex = id;
                GridViewRow row = m_grid.Rows[id];

                clsPhieuNhap oPhieuNhap = new clsPhieuNhap();
                oPhieuNhap.ThongTinThietBi_Id = m_grid.DataKeys[e.RowIndex].Value.ToString();
                DataTable tbltem = new DataTable();
                tbltem = oPhieuNhap.Select_ThongTinThietBi_OneRow();

                string PhieuNhap = sKey;
                string ThietBiId = cboThietBi.SelectedValue.ToString();
                string Ma_Serial = ((TextBox)row.FindControl("txtIMa_Serial")).Text;
                string Ma_MAC = ((TextBox)row.FindControl("txtIMA_MAC")).Text;
                string Ma_SLC = ((TextBox)row.FindControl("txtIMA_SLC")).Text;
                string Ma_DLK = ((TextBox)row.FindControl("txtIMa_DLK")).Text;
                string DienGiai = "";
                int TinhTrang = 1;

                if (m_grid.DataKeys[id].Value.ToString() != "")
                {

                    oPhieuNhap.PhieuNhap_Id = PhieuNhap;
                    oPhieuNhap.ThietBi_Id = ThietBiId;
                    oPhieuNhap.Ma_Serial = Ma_Serial;
                    oPhieuNhap.Ma_MAC = Ma_MAC;
                    oPhieuNhap.Ma_SLC = Ma_SLC;
                    oPhieuNhap.Ma_DLK = Ma_DLK;
                    oPhieuNhap.Dien_Giai = DienGiai;
                    oPhieuNhap.TinhTrangHang = TinhTrang;
                    string _result = oPhieuNhap.InsertThongTinThietBi();
                    if (_result == "1")
                    {
                        ltlAnnouncement.Text = "Sửa kế hoạch nhập xuất chi tiết '" + sKey + "' thành công!";
                    }
                    
                }
            }
            catch (Exception ex)
            {
                ltlAnnouncement.Text = "Khong cap nhat duoc du lieu. Vui long lien he voi quan tri<br>" + ex.Message;
            }

            bindData(-1);
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

        protected void cboThietBi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboThietBi.SelectedValue.ToString() != "")
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();// "server='NgoQuangLua-PC'; uid=sa;pwd=sa; Database='QL'; trusted_connection=true;";
                System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);
                string queryString = "select * from PhieuXuatChiTiet a where a.ThietBi_Id='" + cboThietBi.SelectedValue + "' and a.PhieuXuat_Id='" + sKey + "'";
                sqlConnection.Open();
                SqlDataAdapter oAdap = new SqlDataAdapter(queryString, sqlConnection);

                DataTable tblTemp = new DataTable();
                oAdap.Fill(tblTemp);
                if (tblTemp.Rows.Count > 0)
                {
                    txtSoLuongNhapKho.Text = tblTemp.Rows[0]["So_Luong"].ToString();
                }
            }
           
        }

        
    
    }
}
