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
    public partial class DanhSachPhieuNhapXuat : System.Web.UI.Page
    {
        private int EditIndex = -1;
     
        protected DataTable dsGroup = null;

        private static bool I_Option;                              // QUYEN INSERT
        private static bool U_Option;                              // QUYEN UPDATE
        private static bool D_Option;
        public DataTable _dtFunction;
        
        static bool bsua = false;
        static string sKey = "";
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

            if (!this.IsPostBack)
            {

            //    Fill_Combo_Company();
                getAuthorization();
                sKey = Guid.NewGuid().ToString();
               
                //
                cboGiaTriNoiNhan.DataSource = GetSourceNhanVien();
                cboGiaTriNoiNhan.DataTextField = "TenDayDu";
                cboGiaTriNoiNhan.DataValueField = "NhanVien_id";
                cboGiaTriNoiNhan.DataBind();
                //
                cboLoaiPhieu.DataSource = GetSourceKieuNhapXuat();
                cboLoaiPhieu.DataTextField = "Ten_Loai";
                cboLoaiPhieu.DataValueField = "id_loai";
                cboLoaiPhieu.DataBind();
                //
                cboKho.DataSource = GetSourceKho();
                cboKho.DataTextField = "TenKho";
                cboKho.DataValueField = "Kho_Id";
                cboKho.DataBind();
                //
                cboGiaTriNoiNhan.DataSource = GetSourceNhanVien();
                cboGiaTriNoiNhan.DataTextField = "TenDayDu";
                cboGiaTriNoiNhan.DataValueField = "NhanVien_id";
                cboGiaTriNoiNhan.DataBind();
              
            }
            _dtFunction = new DataTable();
            _dtFunction = (new QLCV.code.common.clsKhoMain()).Get_List_Menu(Session["UserId"].ToString());
           
          
            //
            //BindData();
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
        protected void btnTimKiem_Click(object sender, System.EventArgs e)
        {
            Init();
            BindData();

        }
        protected void btnBoQua_Click(object sender, System.EventArgs e)
        {
            BindData();
            Init();

        }
        protected void btnGhi_Click(object sender, System.EventArgs e)
        {
            clsKeHoachNhapXuat oKeHoachNhapXuat = new clsKeHoachNhapXuat();
            oKeHoachNhapXuat.KeHoachNhapXuat_Id = sKey;
            oKeHoachNhapXuat.Code = sCode;
            oKeHoachNhapXuat.NhanVien_PheDuyet_Id = "83e36d21-8607-4b6f-8dcc-4be0c0fd4ba5";
            oKeHoachNhapXuat.Dien_Giai = sDienGiai;
            oKeHoachNhapXuat.PheDuyetKeHoach();
            Init();
            BindData();
        }

      
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e) 
        {

            if (e.CommandName.ToString() == "PheDuyet")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvRow = GridView1.Rows[index];

                if (GridView1.DataKeys[index].Value.ToString() != "")
                {
                    clsPhieuNhap objPhieuNhap = new clsPhieuNhap();
                    objPhieuNhap.So_Phieu= GridView1.DataKeys[index].Value.ToString();
                    sKey = GridView1.DataKeys[index].Value.ToString();
                    //hien thi popup                    
                    HttpCookie C = new HttpCookie("So_Phieu");
                    C["Data"] = gvRow.Cells[0].Text;
                    Response.Cookies.Add(C);

                    HttpCookie C1 = new HttpCookie("LoaiPhieu");
                    C1["Data"] = cboLoaiPhieu.SelectedIndex.ToString();
                    Response.Cookies.Add(C1);

                    HttpCookie C2 = new HttpCookie("Phieu_Id");
                    C2["Data"] = sKey;
                    Response.Cookies.Add(C2);

                    string window = "../QLKho/ChiTietPhieuNhapXuat.aspx";
                    string newwin = "window.open('" + window + "');";
                    ClientScript.RegisterStartupScript(this.GetType(), "pop", newwin, true);
                }

            }
            else if (e.CommandName.ToString() == "KhongPheDuyet")
            {
                //try
                //{
                //    int id = Convert.ToInt32(e.CommandArgument);
                //    GridView1.EditIndex = id;


                //    Guid delId = new Guid(GridView1.DataKeys[id].Value.ToString());
                    
                //    if (GridView1.DataKeys[id].Value.ToString() != "")
                //    {
                //        clsKeHoachNhapXuat objKeHoachNhapXuat = new clsKeHoachNhapXuat();
                //        objKeHoachNhapXuat.KeHoachNhapXuat_Id = GridView1.DataKeys[id].Value.ToString();
                //        DataTable mytable = new DataTable();
                //        mytable = objKeHoachNhapXuat.Select_onerow();

                //        objKeHoachNhapXuat.NhanVien_Lap_Id = mytable.Rows[0]["NhanVien_Lap_Id"].ToString();
                //        int status_Delete = objKeHoachNhapXuat.Delete();

                //    }
                //}
                //catch (Exception ex)
                //{
                //    ltlAnnouncement.Text = "Lỗi trong quá trình xóa nhóm người dùng: " + ex.ToString();
                //}
            }
            BindData();
        }

      
        public void BindData()
        {

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();
            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            
            if (cboLoaiPhieu.SelectedIndex == 0) //nhập
            {
                cmd.CommandText = "select a.phieunhap_id Phieu_Id,a.So_Phieu,convert(nvarchar(10),a.Ngay_Nhap,103) NgayNhapXuat,a.Nguoi_Mua Nguoi,a.LyDoNhap LyDo,a.Dien_Giai,a.Tong_Tien,a.TinhTrang from PhieuNhap a where 1=1";
            }
            else //xuất
            {
                cmd.CommandText = "select a.phieuxuat_id Phieu_Id,a.So_Phieu,convert(nvarchar(10),a.Ngay_Xuat,103) NgayNhapXuat ,a.NguoiXuat Nguoi,a.LyDoXuat LyDo,a.Dien_Giai Dien_Giai,a.TongTienXuat Tong_Tien,a.TinhTrang TinhTrang  from PhieuXuat a where 1=1";
            }
            cmd.CommandText += " and a.Kho_Id='" + cboKho.SelectedValue.ToString() + "'";
            if (cboLoaiPhieu.SelectedIndex == 0) //nhập
            {
                if (txtLyDoNhap.Text.Trim() != "")
                {
                    cmd.CommandText += " and a.LyDoNhap like '%" + txtLyDoNhap.Text.Trim() + "%'";
                }
                if (txtDienGiai.Text.Trim() != "")
                {
                    cmd.CommandText += " and a.Dien_Giai like '%" + txtDienGiai.Text.Trim() + "%'";
                }
                cmd.CommandText += " order by NgayNhapXuat";
            }
            cmd.Connection = sqlConnection;
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            GridView1.DataSource = ds;
            GridView1.DataBind();
            sqlConnection.Close();

        }

        public void Init()
        {
            sKey = Guid.NewGuid().ToString();
            sCode = "";
            sDienGiai = "";
        }

        protected void cboNhapXuat_SelectedIndexChanged(object sender, EventArgs e)
        {
           
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
