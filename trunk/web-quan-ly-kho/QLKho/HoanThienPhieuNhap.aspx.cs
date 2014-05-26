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
    public partial class HoanThienPhieuNhap : System.Web.UI.Page
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
                sPhieuNhap_Id = Guid.NewGuid().ToString();
                //
                cboLoaiDoiTuongGiaoHang.DataSource = GetSourceNoiNhan();
                cboLoaiDoiTuongGiaoHang.DataTextField = "TenNoiNhan";
                cboLoaiDoiTuongGiaoHang.DataValueField = "NoiNhan_Id";
                cboLoaiDoiTuongGiaoHang.DataBind();
                //
                cboDoiTuongGiaoHang.DataSource = GetSourceNhanVien();
                cboDoiTuongGiaoHang.DataTextField = "TenDayDu";
                cboDoiTuongGiaoHang.DataValueField = "NhanVien_id";
                cboDoiTuongGiaoHang.DataBind();
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

        protected void btnLocPhieu_Click(object sender, System.EventArgs e)
        {
            BindData();
        }

      
        public void BindData()
        {

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();
            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            cmd.CommandText = "select a.PhieuNhap_Id,a.So_Phieu,convert(nvarchar(10),a.Ngay_Nhap,103) Ngay_Nhap,a.LyDoNhap,a.Dien_Giai from PhieuNhap a where 1=1 ";
            if (dtpTuNgay.SelectedDate.ToString().Length > 0 && dtpDenNgay.SelectedDate.ToString().Length > 0)
            {
                cmd.CommandText += " and convert(nvarchar(8),a.Ngay_Nhap,112)>='" + dtpTuNgay.SelectedDate.Value.ToString("yyyyMMdd") + "' and convert(nvarchar(8),a.Ngay_Nhap,112)<='" + dtpDenNgay.SelectedDate.Value.ToString("yyyyMMdd") + "'";
            }
            if (txtSoPhieu.Text != "")
            {
                cmd.CommandText += " and a.So_Phieu='"+ txtSoPhieu.Text +"'";

            }
            if (cboLoaiDoiTuongGiaoHang.SelectedIndex == 0) //Nhân viên
            {
                cmd.CommandText += " and a.NhanVien_Mua_Id='" + cboDoiTuongGiaoHang.SelectedValue.ToString() + "'";
            }
            else if (cboLoaiDoiTuongGiaoHang.SelectedIndex == 1) //Đối tác
            {
                cmd.CommandText += " and a.DoiTac_Id='" + cboDoiTuongGiaoHang.SelectedValue.ToString() + "'";
            }
            else //nhà cung cấp
            {
                cmd.CommandText += " and a.NhaCungCap_Id='" + cboDoiTuongGiaoHang.SelectedValue.ToString() + "'";
            }
            cmd.Connection = sqlConnection;
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
            GridView1.DataSource = ds;
            GridView1.DataBind();
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

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName.ToString() == "SuaChiTiet")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvRow = GridView1.Rows[index];

                if (GridView1.DataKeys[index].Value.ToString() != "")
                {
                    clsKeHoachNhapXuat objKeHoachNhapXuat = new clsKeHoachNhapXuat();
                    objKeHoachNhapXuat.KeHoachNhapXuat_Id = GridView1.DataKeys[index].Value.ToString();
                    sKey = GridView1.DataKeys[index].Value.ToString();
                    //hien thi popup                    
                    HttpCookie C = new HttpCookie("PhieuNhap_Id");
                    C["Data"] = sKey;
                    Response.Cookies.Add(C);

                    string window = "../QLKho/HoanThienPhieuNhapChiTiet.aspx";
                    string newwin = "window.open('" + window + "');";
                    ClientScript.RegisterStartupScript(this.GetType(), "pop", newwin, true);


                }

            }
          
            BindData();
        }


        protected void cboLoaiDoiTuongGiaoHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLoaiDoiTuongGiaoHang.SelectedIndex == 0)
            {
                cboDoiTuongGiaoHang.DataSource = GetSourceNhanVien();
                cboDoiTuongGiaoHang.DataTextField = "TenDayDu";
                cboDoiTuongGiaoHang.DataValueField = "NhanVien_id";
                cboDoiTuongGiaoHang.DataBind();
            }
            else if (cboLoaiDoiTuongGiaoHang.SelectedIndex == 1)
            {
                cboDoiTuongGiaoHang.DataSource = GetSourceDoiTac();
                cboDoiTuongGiaoHang.DataTextField = "TenDoiTac";
                cboDoiTuongGiaoHang.DataValueField = "DoiTac_id";
                cboDoiTuongGiaoHang.DataBind();
            }
            else
            {
                cboDoiTuongGiaoHang.DataSource = GetSourceNhaCungCap();
                cboDoiTuongGiaoHang.DataTextField = "TenNCC";
                cboDoiTuongGiaoHang.DataValueField = "NhaCungCap_id";
                cboDoiTuongGiaoHang.DataBind();
            }
        }
    }
}
