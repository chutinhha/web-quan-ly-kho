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
    public partial class ChiTietPhieuNhapXuat : System.Web.UI.Page
    {
        private int EditIndex = -1;
     
        protected DataTable dsGroup = null;
        protected DataTable dsPhieuNhapXuat = null;
        private static bool I_Option;                              // QUYEN INSERT
        private static bool U_Option;                              // QUYEN UPDATE
        private static bool D_Option;
        public DataTable _dtFunction;
        
        static bool bsua = false;
        static string sKey = "";
        static string sLoaiPhieu = "";
        static string sPhieu_Id = "";
        static string sDienGiai = "";
        static string sCode = "";
        static string sNhanVienPheDuyet = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            // ---- KIEM TRA LOGIN -----
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/login.aspx");
            }
            // ---- END KT LOGIN -------
            HttpCookie Cookie = Request.Cookies["So_Phieu"];
            sKey = Cookie["Data"];

            HttpCookie Cookie1 = Request.Cookies["LoaiPhieu"];
            sLoaiPhieu = Cookie1["Data"];

            HttpCookie Cookie2 = Request.Cookies["Phieu_Id"];
            sPhieu_Id = Cookie2["Data"];

            bindData(-1);
            if (!this.IsPostBack)
            {

                getAuthorization();
                sKey = Guid.NewGuid().ToString();
                
            }
            _dtFunction = new DataTable();
            _dtFunction = (new QLCV.code.common.clsKhoMain()).Get_List_Menu(Session["UserId"].ToString());
          
        }
        public System.Data.SqlClient.SqlDataReader GetSourceNoiNhan()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();// "server='NgoQuangLua-PC'; uid=sa;pwd=sa; Database='QL'; trusted_connection=true;";
            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);

            string queryString = "select 1 NoiNhan_Id,N'Nhân viên'  TenNoiNhan union select 2 NoiNhan_Id,N'Đối tác'  TenNoiNhan union select 3 NoiNhan_Id,N'Nhà cung cấp'  TenNoiNhan";
            System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(queryString, sqlConnection);

            sqlConnection.Open();
            System.Data.SqlClient.SqlDataReader dataReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            return dataReader;
        }


        public System.Data.SqlClient.SqlDataReader GetSourceKieuNhapXuat()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();// "server='NgoQuangLua-PC'; uid=sa;pwd=sa; Database='QL'; trusted_connection=true;";
            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);

            string queryString = "select 1 id_loai,N'Nhập'  Ten_Loai union select 2 id_loai,N'Xuất' Ten_Loai";
            System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(queryString, sqlConnection);

            sqlConnection.Open();
            System.Data.SqlClient.SqlDataReader dataReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            return dataReader;
        }


        public System.Data.SqlClient.SqlDataReader GetSourceKho()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();// "server='NgoQuangLua-PC'; uid=sa;pwd=sa; Database='QL'; trusted_connection=true;";
            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);

            string queryString = "SELECT * FROM Kho";
            System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(queryString, sqlConnection);

            sqlConnection.Open();
            System.Data.SqlClient.SqlDataReader dataReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            return dataReader;
        }
        public System.Data.SqlClient.SqlDataReader GetSourceDonVi()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();// "server='NgoQuangLua-PC'; uid=sa;pwd=sa; Database='QL'; trusted_connection=true;";
            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);

            string queryString = "SELECT * FROM DonVi";
            System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(queryString, sqlConnection);

            sqlConnection.Open();
            System.Data.SqlClient.SqlDataReader dataReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            return dataReader;
        }
        public System.Data.SqlClient.SqlDataReader GetSourceNhanVien()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();// "server='NgoQuangLua-PC'; uid=sa;pwd=sa; Database='QL'; trusted_connection=true;";
            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);

            string queryString = "SELECT * FROM NhanVien";
            System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(queryString, sqlConnection);

            sqlConnection.Open();
            System.Data.SqlClient.SqlDataReader dataReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            return dataReader;
        }
        public System.Data.SqlClient.SqlDataReader GetSourceThietBi()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();// "server='NgoQuangLua-PC'; uid=sa;pwd=sa; Database='QL'; trusted_connection=true;";
            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);

            string queryString = "SELECT * FROM ThietBi";
            System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(queryString, sqlConnection);

            sqlConnection.Open();
            System.Data.SqlClient.SqlDataReader dataReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            return dataReader;
        }
        public System.Data.SqlClient.SqlDataReader GetSourceThanhPham()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();// "server='NgoQuangLua-PC'; uid=sa;pwd=sa; Database='QL'; trusted_connection=true;";
            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);

            string queryString = "SELECT * FROM ThanhPham";
            System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(queryString, sqlConnection);

            sqlConnection.Open();
            System.Data.SqlClient.SqlDataReader dataReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            return dataReader;
        }
        public System.Data.SqlClient.SqlDataReader GetSourceDoiTac()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();// "server='NgoQuangLua-PC'; uid=sa;pwd=sa; Database='QL'; trusted_connection=true;";
            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);

            string queryString = "SELECT * FROM DoiTac";
            System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(queryString, sqlConnection);

            sqlConnection.Open();
            System.Data.SqlClient.SqlDataReader dataReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            return dataReader;
        }
        public System.Data.SqlClient.SqlDataReader GetSourceNhaCungCap()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();// "server='NgoQuangLua-PC'; uid=sa;pwd=sa; Database='QL'; trusted_connection=true;";
            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);

            string queryString = "SELECT * FROM NhaCungCap";
            System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(queryString, sqlConnection);

            sqlConnection.Open();
            System.Data.SqlClient.SqlDataReader dataReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            return dataReader;
        }
        protected void btnBoQua_Click(object sender, System.EventArgs e)
        {
            Init();
            bindData(-1);

        }
        protected void btnGhi_Click(object sender, System.EventArgs e)
        {
            Response.Write("<script language='javascript'>self.close();</script>");
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
                dsPhieuNhapXuat = new DataTable();
                clsPhieuNhap objPhieuNhap = new clsPhieuNhap();
                clsPhieuXuat objPhieuXuat = new clsPhieuXuat();

                if (sLoaiPhieu == "0")
                {
                    objPhieuNhap.So_Phieu = sKey;
                    dsGroup = objPhieuNhap.Select_ChiTiet();
                    objPhieuNhap.PhieuNhap_Id = sPhieu_Id;
                    dsPhieuNhapXuat = objPhieuNhap.Select();
                    if (dsPhieuNhapXuat.Rows.Count > 0)
                    {
                        if (Convert.ToInt16(dsPhieuNhapXuat.Rows[0]["LoaiNhapXuat_Id"]) == 1)
                        {
                            txtLoaiNhapXuat.Text = "Phiếu nhập";
                        }
                        else
                        {
                            txtLoaiNhapXuat.Text = "Phiếu xuất";
                        }
                        txtSoPhieu.Text = dsPhieuNhapXuat.Rows[0]["so_phieu"].ToString();
                        txtLyDoNhap.Text = dsPhieuNhapXuat.Rows[0]["LyDoNhap"].ToString();
                        txtDienGiai.Text = dsPhieuNhapXuat.Rows[0]["Dien_Giai"].ToString();
                        dtpNgayNhapXuat.SelectedDate = Convert.ToDateTime(dsPhieuNhapXuat.Rows[0]["Ngay_Nhap"]);
                        if (dsPhieuNhapXuat.Rows[0]["DuAn_DanhRieng_Id"] == DBNull.Value)
                        {
                            txtDuAnDanhRieng.Text = "";
                        }
                        else
                        {
                            txtDuAnDanhRieng.Text = dsPhieuNhapXuat.Rows[0]["DuAn_DanhRieng_Id"].ToString();
                        }
                        txtNhanVienBan.Text = dsPhieuNhapXuat.Rows[0]["TenDayDu"].ToString();
                        txtNhanVienDoiTac.Text = dsPhieuNhapXuat.Rows[0]["TenDoiTac"].ToString();
                    }
                }
                else
                {
                    objPhieuXuat.So_Phieu = sKey;
                    dsGroup = objPhieuXuat.Select_ChiTiet();
                    objPhieuXuat.PhieuXuat_Id = sPhieu_Id;
                    dsPhieuNhapXuat = objPhieuXuat.Select();
                    if (dsPhieuNhapXuat.Rows.Count > 0)
                    {
                        if (Convert.ToInt16(dsPhieuNhapXuat.Rows[0]["LoaiNhapXuat_Id"]) == 1)
                        {
                            txtLoaiNhapXuat.Text = "Phiếu nhập";
                        }
                        else
                        {
                            txtLoaiNhapXuat.Text = "Phiếu xuất";
                        }
                        txtSoPhieu.Text = dsPhieuNhapXuat.Rows[0]["so_phieu"].ToString();
                        txtLyDoNhap.Text = dsPhieuNhapXuat.Rows[0]["LyDoXuat"].ToString();
                        txtDienGiai.Text = dsPhieuNhapXuat.Rows[0]["Dien_Giai"].ToString();
                        dtpNgayNhapXuat.SelectedDate = Convert.ToDateTime(dsPhieuNhapXuat.Rows[0]["Ngay_Xuat"]);
                        if (dsPhieuNhapXuat.Rows[0]["DuAn_Id"] == DBNull.Value)
                        {
                            txtDuAnDanhRieng.Text = "";
                        }
                        else
                        {
                            txtDuAnDanhRieng.Text = dsPhieuNhapXuat.Rows[0]["DuAn_Id"].ToString();
                        }
                        txtNhanVienBan.Text = dsPhieuNhapXuat.Rows[0]["TenDayDu"].ToString();
                        txtNhanVienDoiTac.Text = dsPhieuNhapXuat.Rows[0]["TenDoiTac"].ToString();
                    }
                }
                objPhieuNhap.So_Phieu = "0";

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
                    clsKeHoachNhapXuat objKeHoachNhapXuat = new clsKeHoachNhapXuat();
                    objKeHoachNhapXuat.KeHoachNhapXuatChiTiet_Id = m_grid.DataKeys[e.RowIndex].Value.ToString();
                    int status_Delete = objKeHoachNhapXuat.DeleteChiTiet();
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

        protected void m_grid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int id = e.RowIndex;
                m_grid.EditIndex = id;
                GridViewRow row = m_grid.Rows[id];
                string ThietBi_id = "";
                string ThanhPham_id = "";
                string ThietBi = ((TextBox)row.FindControl("txtUTenThietBi")).Text;
                string ThanhPham = ((TextBox)row.FindControl("txtUTenThanhPham")).Text;
                Int32 SoLuong = Convert.ToInt32(((TextBox)row.FindControl("txtUSoLuong")).Text);
                DataTable tblTemp = new DataTable();
                clsThanhPham oThanhPham = new clsThanhPham();
                clsThietBi oThietBi = new clsThietBi();
                
                if (ThietBi != "")
                {
                    tblTemp = oThietBi.Select();
                    int i;
                    for (i = 0; i < tblTemp.Rows.Count; i++)
                    {
                        if (tblTemp.Rows[i]["TenThietBi"].ToString().Trim() == ThietBi.Trim())
                        {
                            ThietBi_id = tblTemp.Rows[i]["ThietBi_Id"].ToString();
                        }
                    }
                }
                if (ThanhPham != "")
                {
                    tblTemp = oThanhPham.Select();
                    int i;
                    for (i = 0; i < tblTemp.Rows.Count; i++)
                    {
                        if (tblTemp.Rows[i]["TenThanhPham"].ToString().Trim() == ThanhPham.Trim())
                        {
                            ThanhPham_id = tblTemp.Rows[i]["ThanhPham_Id"].ToString();
                        }
                    }
                }
                if (m_grid.DataKeys[id].Value.ToString() != "")
                {
                    clsKeHoachNhapXuat objKeHoachNhapXuat = new clsKeHoachNhapXuat();

                    objKeHoachNhapXuat.KeHoachNhapXuat_Id = sKey;
                    objKeHoachNhapXuat.ThietBi_Id = ThietBi_id;
                    objKeHoachNhapXuat.ThanhPham_Id = ThanhPham_id;
                    objKeHoachNhapXuat.So_Luong = SoLuong;
                    int _result = objKeHoachNhapXuat.UpdateChiTiet();
                    if (_result == 1)
                    {
                        ltlAnnouncement.Text = "Sửa kế hoạch nhập xuất chi tiết '" + m_grid.DataKeys[e.RowIndex].Value.ToString() + "' thành công!";
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

        protected void m_grid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string commandName = e.CommandName;
                GridViewRow row = m_grid.FooterRow;
                if (commandName == "Insert")
                {
                    clsKeHoachNhapXuat objKeHoachNhapXuat = new clsKeHoachNhapXuat();

                    string ThietBi_id = ((DropDownList)row.FindControl("txtITenThietBi")).Text;
                    string ThanhPham_id = ((DropDownList)row.FindControl("txtITenThanhPham")).Text;
                    int LoaiNhap_id = Convert.ToInt32(((DropDownList)row.FindControl("txtITenLoaiNhap")).Text);
                    Int32 SoLuong = Convert.ToInt32(((TextBox)row.FindControl("txtISoLuong")).Text);

                    objKeHoachNhapXuat.KeHoachNhapXuat_Id = sKey;
                    if (LoaiNhap_id == 1) //nhập thành phẩm
                    {
                        //objKeHoachNhapXuat.ThietBi_Id = DBNull.Value;
                        objKeHoachNhapXuat.ThanhPham_Id = ThanhPham_id;
                    }
                    else if (LoaiNhap_id == 2) //nhập thiết bị
                    {
                        objKeHoachNhapXuat.ThietBi_Id = ThietBi_id;
                        //objKeHoachNhapXuat.ThanhPham_Id = ThanhPham_id;
                    }
                    objKeHoachNhapXuat.So_Luong = SoLuong;


                    int _result = objKeHoachNhapXuat.InsertChiTiet();
                    if (_result >= 1)
                    {
                        ltlAnnouncement.Text = "Thêm mới kho '" + ThietBi_id + "' thành công!";
                    }
                    if (_result == -1)
                    {
                        ltlAnnouncement.Text = "Kho '" + ThietBi_id + "' đã tồn tại, bạn nên kiểm tra lại!";
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
    
    }
}
