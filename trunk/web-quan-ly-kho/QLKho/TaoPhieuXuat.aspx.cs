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
    public partial class TaoPhieuXuat : System.Web.UI.Page
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
        static string sPhieuNhap_Id = "";
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
            HttpCookie Cookie = Request.Cookies["KeHoach_ID"];
            sKey = Cookie["Data"];
           
            if (!this.IsPostBack)
            {

                getAuthorization();
                bindData(-1);

                cboMaLoaiNhap.DataSource = GetSourceLoaiNhapXuat();
                cboMaLoaiNhap.DataTextField = "TenLoaiNhapXuat";
                cboMaLoaiNhap.DataValueField = "AliasName";
                cboMaLoaiNhap.DataBind();
                //
                sPhieuNhap_Id = Guid.NewGuid().ToString();
            }
            _dtFunction = new DataTable();
            _dtFunction = (new QLCV.code.common.clsKhoMain()).Get_List_Menu(Session["UserId"].ToString());

            //
            cboNhanVienNhapXuat.DataSource = GetSourceNhanVien();
            cboNhanVienNhapXuat.DataTextField = "TenDayDu";
            cboNhanVienNhapXuat.DataValueField = "NhanVien_id";
            cboNhanVienNhapXuat.DataBind();
            //
            cboNhanVienDoiTac.DataSource = GetSourceNhanVienDoiTac();
            cboNhanVienDoiTac.DataTextField = "TenNhanVien";
            cboNhanVienDoiTac.DataValueField = "DoiTac_NhanVien_id";
            cboNhanVienDoiTac.DataBind();
            //
            cboDuAn.DataSource = GetSourceDuAn();
            cboDuAn.DataTextField = "TenDuAn";
            cboDuAn.DataValueField = "DuAn_id";
            cboDuAn.DataBind();
            BindData();
            //
            dtpNgayNhapXuat.SelectedDate = DateTime.Today;

            //Lấy số phiếu

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();
            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            string sTemp = "";
            if (sKey.Length > 0)
            {
                sTemp = sKey;
            }
            else
            {
                sTemp = Guid.NewGuid().ToString();
            }
            cmd.CommandText = "select b.DonVi_Id,b.Kho_Id from Kho b where b.Kho_Id=(select a.Kho_Id from KeHoachNhapXuat a where a.KeHoachNhapXuat_Id='" + sTemp + "')";
            cmd.Connection = sqlConnection;
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            sqlConnection.Open();
            cmd.ExecuteNonQuery();

            clsPhieuNhap oPhieuNhap = new clsPhieuNhap();
            if (ds.Tables[0].Rows.Count > 0)
            {
                oPhieuNhap.DonVi_Id = ds.Tables[0].Rows[0]["DonVi_Id"].ToString();
                oPhieuNhap.Kho_Id = ds.Tables[0].Rows[0]["Kho_Id"].ToString();
            }
            else
            {
                oPhieuNhap.DonVi_Id = "a0f3f798-b9bc-4537-b97c-997b638f5450";
            }
            oPhieuNhap.LoaiPhieu = 2;
            oPhieuNhap.Ngay_Nhap = DateTime.Today;
            txtSoPhieu.Text = oPhieuNhap.LaySoPhieu();
        }
        public System.Data.SqlClient.SqlDataReader GetSourceDuAn()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();// "server='NgoQuangLua-PC'; uid=sa;pwd=sa; Database='QL'; trusted_connection=true;";
            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);

            string queryString = "select NEWID() duan_id,N'' tenduan union select duan_id,tenduan from DuAn";
            System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(queryString, sqlConnection);

            sqlConnection.Open();
            System.Data.SqlClient.SqlDataReader dataReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            return dataReader;
        }
        public System.Data.SqlClient.SqlDataReader GetSourceLoaiNhapXuat()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();// "server='NgoQuangLua-PC'; uid=sa;pwd=sa; Database='QL'; trusted_connection=true;";
            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);

            string queryString = "SELECT * FROM LoaiNhapXuat";
            System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(queryString, sqlConnection);

            sqlConnection.Open();
            System.Data.SqlClient.SqlDataReader dataReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            return dataReader;
        }
        public System.Data.SqlClient.SqlDataReader GetSourceNhanVienDoiTac()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();// "server='NgoQuangLua-PC'; uid=sa;pwd=sa; Database='QL'; trusted_connection=true;";
            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);

            string queryString = "SELECT * FROM DoiTac_NhanVien";
            System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(queryString, sqlConnection);

            sqlConnection.Open();
            System.Data.SqlClient.SqlDataReader dataReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            return dataReader;
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
            if (!bsua) //thêm mới
            {

                clsPhieuXuat oPhieuXuat = new clsPhieuXuat();
                byte[] productImage = FileUpload1.FileBytes;
                oPhieuXuat.NhanVienLogon_Id = "83e36d21-8607-4b6f-8dcc-4be0c0fd4ba5";
                oPhieuXuat.KeHoachNhapXuat_Id = sKey;
                oPhieuXuat.PhieuXuat_Id = sPhieuNhap_Id;
                oPhieuXuat.Ngay_Xuat = Convert.ToDateTime(dtpNgayNhapXuat.SelectedDate);
                oPhieuXuat.MaLoaiXuat_Id = cboMaLoaiNhap.SelectedValue;
                oPhieuXuat.LyDoXuat = txtLyDoNhap.Text;
                oPhieuXuat.Dien_Giai = txtDienGiai.Text;
                if (cboDuAn.SelectedIndex == 0)
                {
                    oPhieuXuat.DuAn_DanhRieng_Id = "";
                }
                else
                {
                    oPhieuXuat.DuAn_DanhRieng_Id = cboDuAn.SelectedValue;
                }
                oPhieuXuat.NhanVien_Xuat_Id = cboNhanVienNhapXuat.SelectedValue;
                oPhieuXuat.Ten_NhanVien_DoiTac = cboNhanVienDoiTac.Text;
                oPhieuXuat.NhanVien_DoiTac_Id = cboNhanVienDoiTac.SelectedValue;
                oPhieuXuat.ChungTu = productImage;
                oPhieuXuat.KieuFile = ".jpg";
                oPhieuXuat.So_Phieu = txtSoPhieu.Text;
                oPhieuXuat.Insert();
                lblThongBao.Text = "Tạo phiếu xuất thành công, mời bạn nhập chi tiết";
            }
            else //sửa
            {
                clsNhanVien oNhanVien = new clsNhanVien();
                //oNhanVien.NhanVien_Id = sKey;
                //oNhanVien.PhongBan_Id = cboPhongBan.SelectedValue.ToString();
                //oNhanVien.Ten = txtTen.Text;
                //oNhanVien.Ho = txtHo.Text;
                //oNhanVien.TenDayDu = txtTenDayDu.Text;
                //oNhanVien.DiaChi = txtDiaChi.Text;
                //oNhanVien.Tel = txtTel.Text;
                //oNhanVien.SubTel = txtSubTel.Text;
                //oNhanVien.Mobile = txtMobile.Text;
                //oNhanVien.HomePhone = "";
                //oNhanVien.Main_EMail = txtEmail.Text;
                //oNhanVien.Other_Email = txtOtherEmail.Text;
                //oNhanVien.ChucDanh = txtChucDanh.Text;
                //oNhanVien.GioiTinh = (byte)(cboGioiTinh.SelectedValue == "1" ? 1 : 0);
                //oNhanVien.Update();
                //sKey = "";
                bsua = false;
            }
            Init();
            BindData();
        }

        protected void btnThoat_Click(object sender, System.EventArgs e)
        {

            Response.Write("<script language='javascript'>window.opener.location.reload();window.close();</script>");
            
        }

        public void Init()
        {
           
            sKey = Guid.NewGuid().ToString();
            sCode = "";
            sDienGiai = "";
        }

        public void BindData()
        {

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();
            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            cmd.CommandText = "select a.PhieuXuat_Id,b.Code,c.TenKho,a.So_Phieu,a.Ngay_Xuat,'' LyDoXuat,'' DuAn_DanhRieng_id,d.TenDayDu,e.TenDoiTac,f.TenNhanVien,'' TenNCC,0 Tong_Tien,h.TenLoaiNhapXuat,'' Dien_Giai from PhieuXuat a left join DoiTac e on a.DoiTac_Id=e.DoiTac_Id left join DoiTac_NhanVien f on a.NhanVien_DoiTac_Id=f.DoiTac_NhanVien_Id ,KeHoachNhapXuat b, Kho c,NhanVien d,  LoaiNhapXuat h where a.KeHoachNhapXuat_Id=b.KeHoachNhapXuat_Id and a.Kho_Id=c.Kho_Id  and a.NhanVien_Xuat_Id=d.NhanVien_Id  and a.LoaiNhapXuat_Id=h.LoaiNhapXuat_Id and a.Ngay_Xuat='"+ DateTime.Today.ToString("yyyyMMdd") +"'";
            cmd.Connection = sqlConnection;
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            sqlConnection.Open();
            cmd.ExecuteNonQuery();

            GridView1.DataSource = ds;
            GridView1.DataBind();
            sqlConnection.Close();

        }

        private void bindData(int index)
        {
            try
            {
                clsPhieuXuatChiTiet objPhieuXuatChiTiet = new clsPhieuXuatChiTiet();
                clsKeHoachNhapXuat objKeHoachNhapXuatChiTiet = new clsKeHoachNhapXuat();

                objPhieuXuatChiTiet.KeHoachNhapXuat_Id = sKey;
                dsGroup = objPhieuXuatChiTiet.Select();
                objKeHoachNhapXuatChiTiet.KeHoachNhapXuat_Id = sKey;
                dsTongHop = objKeHoachNhapXuatChiTiet.Select_onerow();

                objPhieuXuatChiTiet.PhieuXuatChiTiet_Id = "0";
                
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

        protected void cboNhapXuat_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindData();
            //Init();
            //bindData(-1);
        }

        protected void m_grid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            EditIndex = e.NewEditIndex;
            bindData(EditIndex);
        }

        protected void GridView1_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {

           
        }

        protected void m_grid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            bindData(-1);
        }

        protected void m_grid_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void m_grid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int id = e.RowIndex;
                m_grid.EditIndex = id;
                GridViewRow row = m_grid.Rows[id];

                string NhanVien_Logon_id = "3d2df69a-4d7c-4d78-a4a3-075a169a109d";
                string TenHangMuc = ((TextBox)row.FindControl("txtUHangMuc")).Text;
                string sSoLuong = ((TextBox)row.FindControl("txtUSoLuong")).Text;
                string sDonGia = ((TextBox)row.FindControl("txtUDonGia")).Text;

                double SoLuong = Convert.ToDouble(sSoLuong==""? "0": sSoLuong);
                double DonGia = Convert.ToDouble(sDonGia==""? "0": sDonGia);
                double TongTien = SoLuong * DonGia;
                double VAT = TongTien * 0.01;
                //double VAT = Convert.ToDouble(((TextBox)row.FindControl("txtIVAT")).Text);
                string sPhiCacLoai = ((TextBox)row.FindControl("txtUPhiCacLoai")).Text;
                string sGiamGia = ((TextBox)row.FindControl("txtUGiamGia")).Text;
                double PhiCacLoai = Convert.ToDouble(sPhiCacLoai==""? "0": sPhiCacLoai);
                double GiamGia = Convert.ToDouble(sGiamGia==""? "0": sGiamGia);
                double ThanhTien = TongTien + VAT + PhiCacLoai - GiamGia;
                //-------------
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();
                System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                cmd.CommandText = "select * from thanhpham where tenthanhpham=N'" + TenHangMuc + "'";
                cmd.Connection = sqlConnection;
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                string ThanhPham_id = "";
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ThanhPham_id = ds.Tables[0].Rows[0]["ThanhPham_Id"].ToString();
                }
                else
                {
                    ThanhPham_id = "";
                }
                ds = new DataSet();
                cmd.CommandText = "select * from thietbi where tenthietbi=N'" + TenHangMuc + "'";
                cmd.Connection = sqlConnection;
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                string ThietBi_id = "";
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ThietBi_id = ds.Tables[0].Rows[0]["ThietBi_Id"].ToString();
                }
                else
                {
                    ThietBi_id = "";
                }

                if (m_grid.DataKeys[id].Value.ToString() != "")
                {
                    clsPhieuXuat objPhieuXuat = new clsPhieuXuat();
                    objPhieuXuat.NhanVienLogon_Id = NhanVien_Logon_id;
                    objPhieuXuat.PhieuXuat_Id = sPhieuNhap_Id;
                    objPhieuXuat.ThietBi_Id = ThietBi_id;
                    objPhieuXuat.SoLuong = SoLuong;
                    objPhieuXuat.DonGia = DonGia;
                    objPhieuXuat.VAT = VAT;
                    objPhieuXuat.GiamGia = GiamGia;
                    objPhieuXuat.PhiCacLoai = PhiCacLoai;
                    string _result = objPhieuXuat.InsertChiTiet();

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
                    string NhanVien_Logon_id = "3d2df69a-4d7c-4d78-a4a3-075a169a109d";
                    string ThietBi_id = ((DropDownList)row.FindControl("txtITenThietBi")).Text;
                    string ThanhPham_id = ((DropDownList)row.FindControl("txtITenThanhPham")).Text;
                    double SoLuong = Convert.ToDouble(((TextBox)row.FindControl("txtISoLuong")).Text);
                    double DonGia = Convert.ToDouble(((TextBox)row.FindControl("txtIDonGia")).Text);
                    double VAT = Convert.ToDouble(((TextBox)row.FindControl("txtIVAT")).Text);
                    double PhiCacLoai = Convert.ToDouble(((TextBox)row.FindControl("txtIPhiCacLoai")).Text);
                    double GiamGia = Convert.ToDouble(((TextBox)row.FindControl("txtIGiamGia")).Text);

                    clsPhieuNhap objPhieuNhap = new clsPhieuNhap();
                    objPhieuNhap.NhanVienLogon_Id = NhanVien_Logon_id;
                    objPhieuNhap.PhieuNhap_Id = sPhieuNhap_Id;
                    objPhieuNhap.ThanhPham_Id = ThanhPham_id;
                    objPhieuNhap.ThietBi_Id = ThietBi_id;
                    objPhieuNhap.SoLuong = SoLuong;
                    objPhieuNhap.DonGia = DonGia;
                    objPhieuNhap.VAT = VAT;
                    objPhieuNhap.GiamGia = GiamGia;
                    objPhieuNhap.PhiCacLoai = PhiCacLoai;

                    string _result = objPhieuNhap.InsertChiTiet();
                    bindData(-1);
                }
            }
            catch (Exception ex)
            {
                ltlAnnouncement.Text = "Khong them moi duoc du lieu. Vui long lien he voi quan tri<br>" + ex.Message;
            }

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

     
    }
}
