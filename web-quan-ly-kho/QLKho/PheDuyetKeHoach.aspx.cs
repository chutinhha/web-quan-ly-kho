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
    public partial class PheDuyetKeHoach : System.Web.UI.Page
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

                getAuthorization();
                sKey = Guid.NewGuid().ToString();
                //
                cboLoaiNoiNhan.DataSource = GetSourceNoiNhan();
                cboLoaiNoiNhan.DataTextField = "TenNoiNhan";
                cboLoaiNoiNhan.DataValueField = "NoiNhan_Id";
                cboLoaiNoiNhan.DataBind();
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
                dtpTuNgay.SelectedDate = DateTime.Today;
                dtpDenNgay.SelectedDate = DateTime.Today;
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
        protected void btnTimKiem_Click(object sender, System.EventArgs e)
        {
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
                    clsKeHoachNhapXuat objKeHoachNhapXuat = new clsKeHoachNhapXuat();
                    objKeHoachNhapXuat.KeHoachNhapXuat_Id = GridView1.DataKeys[index].Value.ToString();
                    sKey = GridView1.DataKeys[index].Value.ToString();
                    //hien thi popup                    
                    HttpCookie C = new HttpCookie("KeHoach_ID");
                    C["Data"] = sKey;
                    Response.Cookies.Add(C);

                    string window = "../QLKho/popPheDuyetKeHoach.aspx";
                    string newwin = "window.open('" + window + "');";
                    ClientScript.RegisterStartupScript(this.GetType(), "pop", newwin, true);

                    //txtKeHoachID.Text = sKey;
                    //DataTable mytable = new DataTable();
                    //mytable = objKeHoachNhapXuat.Select_onerow();
                    //if (mytable.Rows.Count > 0)
                    //{

                    //    sDienGiai = mytable.Rows[0]["Dien_Giai"].ToString();
                    //    sCode = mytable.Rows[0]["Code"].ToString();

                    //}
                    //bsua = true;
                    //bindData(-1);
                   
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
            cmd.CommandText = "select a.KeHoachNhapXuat_Id,a.Code,b.TenKho,convert(nvarchar(10),a.NgayLap,103) NgayLap,a.NguoiLap,d.TenDayDu,a.TinhTrang,a.LyDoNhapXuat,a.Dien_Giai,h.TenDoiTac,i.TenNCC from KeHoachNhapXuat a left join NhanVien d on a.NhanVien_Lap_Id=d.NhanVien_Id left join NhanVien e on a.NhanVien_PheDuyet_Id=e.NhanVien_Id left join DoiTac h on a.DoiTac_Id=h.DoiTac_Id left join NhaCungCap i on a.NhaCungCap_Id=i.NhaCungCap_Id,Kho b where a.Kho_Id=b.Kho_Id and a.NhanVien_Lap_Id=d.NhanVien_Id and a.TinhTrang='DE_NGHI'";
            if (cboLoaiPhieu.SelectedIndex == 0) //nhập
            {
                cmd.CommandText += " and a.KieuNhapXuat=1";
            }
            else //xuất
            {
                cmd.CommandText += " and a.KieuNhapXuat=2";
            }
            cmd.CommandText += " and a.Kho_Id='" + cboKho.SelectedValue.ToString() + "'";
            if (txtLyDoNhap.Text.Trim() != "")
            {
                cmd.CommandText += " and a.LyDoNhapXuat like '%" + txtLyDoNhap.Text.Trim() +"%'";
            }
            if (txtDienGiai.Text.Trim() != "")
            {
                cmd.CommandText += " and a.Dien_Giai like '%" + txtDienGiai.Text.Trim() + "%'";
            }
            if (cboLoaiNoiNhan.SelectedIndex == 0)//nhân viên
            {
                cmd.CommandText += " and a.nhanvien_nhan_id='" + cboGiaTriNoiNhan.SelectedValue.ToString() + "'";
            }
            else if (cboLoaiNoiNhan.SelectedIndex == 1)//Đối tác
            {
                cmd.CommandText += " and a.DoiTac_id='" + cboGiaTriNoiNhan.SelectedValue.ToString() + "'";
            }
            else //nhà cung cấp
            {
                cmd.CommandText += " and a.NhaCungCap_id='" + cboGiaTriNoiNhan.SelectedValue.ToString() + "'";
            }
            if (dtpTuNgay.SelectedDate.ToString().Length > 0 && dtpDenNgay.SelectedDate.ToString().Length>0)
            {
                cmd.CommandText += " and convert(nvarchar(8),a.NgayLap,112)>='" + dtpTuNgay.SelectedDate.Value.ToString("yyyyMMdd") + "' and convert(nvarchar(8),a.NgayLap,112)<='" + dtpDenNgay.SelectedDate.Value.ToString("yyyyMMdd") + "'";
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
            //txtTinhTrang.Text = "";
            //txtLyDoNhap.Text = "";
            //txtDienGiai.Text = "";
            //txtSoLuong.Text = "";s
            //txtCode.Text = "";
            //txtKeHoachID.Text = "";
            sKey = Guid.NewGuid().ToString();
            sCode = "";
            sDienGiai = "";
        }


        protected void cboNhapXuat_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void cboLoaiNoiNhan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLoaiNoiNhan.SelectedIndex == 0)
            {
                cboGiaTriNoiNhan.DataSource = GetSourceNhanVien();
                cboGiaTriNoiNhan.DataTextField = "TenDayDu";
                cboGiaTriNoiNhan.DataValueField = "NhanVien_id";
                cboGiaTriNoiNhan.DataBind();
            }
            else if (cboLoaiNoiNhan.SelectedIndex == 1)
            {
                cboGiaTriNoiNhan.DataSource = GetSourceDoiTac();
                cboGiaTriNoiNhan.DataTextField = "TenDoiTac";
                cboGiaTriNoiNhan.DataValueField = "DoiTac_id";
                cboGiaTriNoiNhan.DataBind();
            }
            else
            {
                cboGiaTriNoiNhan.DataSource = GetSourceNhaCungCap();
                cboGiaTriNoiNhan.DataTextField = "TenNCC";
                cboGiaTriNoiNhan.DataValueField = "NhaCungCap_id";
                cboGiaTriNoiNhan.DataBind();
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
