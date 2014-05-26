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

namespace QLCV.DanhMuc
{
    public partial class Nhap_Xuat : System.Web.UI.Page
    {
        private int EditIndex = -1;
     
        protected DataTable dsGroup = null;
        protected DataTable dsChiTiet = null;
        protected DataTable dsTongHop = null;

        protected DataTable dsKeHoachNhapXuat = null;
        private static bool I_Option;                              // QUYEN INSERT
        private static bool U_Option;                              // QUYEN UPDATE
        private static bool D_Option;
        public DataTable _dtFunction;
        
        static bool bsua = false;
        static string sKey = "";
        static string sPhieuNhap_Id = "";
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

                getAuthorization();
                //
                cboKeHoachNhapXuat.DataSource = GetSourceKeHoach();
                cboKeHoachNhapXuat.DataTextField = "Code";
                cboKeHoachNhapXuat.DataValueField = "KeHoachNhapXuat_Id";
                cboKeHoachNhapXuat.DataBind();
                //
                sPhieuNhap_Id = Guid.NewGuid().ToString();
            }
            _dtFunction = new DataTable();
            _dtFunction = (new QLCV.code.common.clsKhoMain()).Get_List_Menu(Session["UserId"].ToString());
            BindData();
            //Lấy số phiếu
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();
            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            string sTemp = "";
            if (cboKeHoachNhapXuat.SelectedValue.ToString().Length > 0)
            {
                sTemp = cboKeHoachNhapXuat.SelectedValue;
            }
            else
            {
                sTemp = Guid.NewGuid().ToString();
            }
            cmd.CommandText = "select b.DonVi_Id,b.Kho_Id from Kho b where b.Kho_Id=(select a.Kho_Id from KeHoachNhapXuat a where a.KeHoachNhapXuat_Id='"+ sTemp +"')";
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
            oPhieuNhap.LoaiPhieu = 1;
            oPhieuNhap.Ngay_Nhap = DateTime.Today;
            //txtSoPhieu.Text=oPhieuNhap.LaySoPhieu();
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
      
        public System.Data.SqlClient.SqlDataReader GetSourceKeHoach()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();// "server='NgoQuangLua-PC'; uid=sa;pwd=sa; Database='QL'; trusted_connection=true;";
            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);

            string queryString = "SELECT * FROM KeHoachNhapXuat where KieuNhapXuat=1 and TinhTrang='PHE_DUYET'";
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

       
        protected void btnTim_Click(object sender, System.EventArgs e)
        {

            bindData(-1);

        }


        protected void btnTaoPhieuNhap_Click(object sender, System.EventArgs e)
        {
            if (cboKeHoachNhapXuat.SelectedValue.ToString() != "")
            {
                lblThongBao.Text = "";
                HttpCookie C = new HttpCookie("KeHoach_ID");
                C["Data"] = cboKeHoachNhapXuat.SelectedValue.ToString();
                Response.Cookies.Add(C);

                string window = "../QLKho/TaoPhieuNhap.aspx";
                string newwin = "window.open('" + window + "');";
                ClientScript.RegisterStartupScript(this.GetType(), "pop", newwin, true);
            }
            else
            {
                lblThongBao.Text = "Bạn cần chọn một kế hoạch để nhập kho!";
            }
        }

      
        public void BindData()
        {

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();
            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            cmd.CommandText = "select a.PhieuNhap_Id,b.Code,c.TenKho,a.So_Phieu,a.Ngay_Nhap,a.LyDoNhap,j.TenDuAn as DuAn_DanhRieng_id,d.TenDayDu,e.TenDoiTac,f.TenNhanVien,g.TenNCC,a.Tong_Tien,h.TenLoaiNhapXuat,a.Dien_Giai from PhieuNhap a left join DoiTac e on a.DoiTac_Id=e.DoiTac_Id left join DoiTac_NhanVien f on a.NhanVien_DoiTac_Id=f.DoiTac_NhanVien_Id left join NhaCungCap g on a.NhaCungCap_Id=g.NhaCungCap_Id left join DuAn j on a.DuAn_DanhRieng_Id=j.DuAn_Id,KeHoachNhapXuat b, Kho c,NhanVien d,  LoaiNhapXuat h where a.KeHoachNhapXuat_Id=b.KeHoachNhapXuat_Id and a.Kho_Id=c.Kho_Id  and a.NhanVien_Mua_Id=d.NhanVien_Id  and a.LoaiNhapXuat_Id=h.LoaiNhapXuat_Id ";
            cmd.Connection = sqlConnection;
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            sqlConnection.Close();

        }

        public void Init()
        {
            //txtLyDoNhap.Text = "";
            //txtDienGiai.Text = "";

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
                dsTongHop = new DataTable();
                clsKeHoachNhapXuat objKeHoachNhapXuatChiTiet = new clsKeHoachNhapXuat();
                clsPhieuNhapChiTiet objPhieuNhapChiTiet= new clsPhieuNhapChiTiet();
                objPhieuNhapChiTiet.KeHoachNhapXuat_Id = cboKeHoachNhapXuat.SelectedValue.ToString();
                dsGroup = objPhieuNhapChiTiet.Select();
                objKeHoachNhapXuatChiTiet.KeHoachNhapXuat_Id = cboKeHoachNhapXuat.SelectedValue.ToString();
                dsTongHop = objKeHoachNhapXuatChiTiet.Select_onerow();
                //
                objKeHoachNhapXuatChiTiet.KeHoachNhapXuat_Id = cboKeHoachNhapXuat.SelectedValue.ToString();
                dsChiTiet = objKeHoachNhapXuatChiTiet.SelectChiTiet();
                m_grid1.EditIndex = index;
                m_grid1.DataSource = dsChiTiet;
                m_grid1.DataBind();

                objPhieuNhapChiTiet.PhieuNhapChiTiet_Id = "0";
                //Hiển thị tổng hợp
                if (dsTongHop.Rows.Count > 0)
                {
                    if (dsTongHop.Rows[0]["KieuNhapXuat"] == "1")
                    {
                        txtNhapXuat.Text = "Phiếu nhập";
                    }
                    else
                    {
                        txtNhapXuat.Text = "Phiếu xuất";
                    }
                    txtKho.Text = dsTongHop.Rows[0]["TenKho"].ToString();
                    txtNguoiLap.Text = dsTongHop.Rows[0]["NguoiLap"].ToString();
                    txtLyDoNhapXuat1.Text = dsTongHop.Rows[0]["LyDoNhapXuat"].ToString();
                    txtDienGiai1.Text = dsTongHop.Rows[0]["Dien_Giai"].ToString();
                    if (dsTongHop.Rows[0]["DoiTac_Id"] != DBNull.Value)
                    {
                        txtNoiNhan.Text = "Đối tác";
                        txtGiaTriNoiNhan.Text = dsTongHop.Rows[0]["TenDoiTac"].ToString();
                    }
                    else if (dsTongHop.Rows[0]["NhaCungCap_Id"] != DBNull.Value)
                    {
                        txtNoiNhan.Text = "Nhà cung cấp";
                        txtGiaTriNoiNhan.Text = dsTongHop.Rows[0]["TenNCC"].ToString();
                    }
                    else if (dsTongHop.Rows[0]["NhanVien_Nhan_Id"] != DBNull.Value)
                    {
                        txtNoiNhan.Text = "Nhân viên";
                        txtGiaTriNoiNhan.Text = dsTongHop.Rows[0]["TenDayDu"].ToString();
                    }
                    dtpNgayLap.SelectedDate = Convert.ToDateTime(dsTongHop.Rows[0]["NgayLap"]);
                }
                bool add = false;
                if (dsGroup.Rows.Count <= 0)
                {
                    dsGroup.Rows.Add(dsGroup.NewRow());
                    add = true;

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
          
        }

        protected void m_grid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
               
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
               
            }
            catch (Exception ex)
            {
                ltlAnnouncement.Text = "Khong them moi duoc du lieu. Vui long lien he voi quan tri<br>" + ex.Message;
            }

        }
       
       
    }
}
