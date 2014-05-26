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
    public partial class KeHoachNhapXuat : System.Web.UI.Page
    {
        private int EditIndex = -1;
     
        protected DataTable dsGroup = null;

        private static bool I_Option;                              // QUYEN INSERT
        private static bool U_Option;                              // QUYEN UPDATE
        private static bool D_Option;
        public DataTable _dtFunction;
        
        static int bsua = 0;
        static string sKey = "";
        static string sKeHoachNhapXuat_Id = "";
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
                bindData(-1);
             
                //
                sKeHoachNhapXuat_Id = Guid.NewGuid().ToString();
                //
                cboNhapXuat.DataSource = GetSourceKieuNhapXuat();
                cboNhapXuat.DataTextField = "Ten_Loai";
                cboNhapXuat.DataValueField = "id_loai";
                cboNhapXuat.DataBind();
                //
                cboNoiNhan.DataSource = GetSourceNoiNhan();
                cboNoiNhan.DataTextField = "TenNoiNhan";
                cboNoiNhan.DataValueField = "NoiNhan_Id";
                cboNoiNhan.DataBind();
                //
                cboGiaTriNoiNhan.DataSource = GetSourceNhanVien();
                cboGiaTriNoiNhan.DataTextField = "TenDayDu";
                cboGiaTriNoiNhan.DataValueField = "NhanVien_id";
                cboGiaTriNoiNhan.DataBind();
                

                //
                dtpNgayLap.SelectedDate = DateTime.Today;
            }
            _dtFunction = new DataTable();
            _dtFunction = (new QLCV.code.common.clsKhoMain()).Get_List_Menu(Session["UserId"].ToString());
            //
            cboKho.DataSource = GetSourceKho();
            cboKho.DataTextField = "tenKho";
            cboKho.DataValueField = "Kho_id";
            cboKho.DataBind();
            //
            cboNguoiLap.DataSource = GetSourceNhanVien();
            cboNguoiLap.DataTextField = "TenDayDu";
            cboNguoiLap.DataValueField = "NhanVien_id";
            cboNguoiLap.DataBind();
            
            BindData("DE_NGHI");
            
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


        public System.Data.SqlClient.SqlDataReader GetSourceTrangThai()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();// "server='NgoQuangLua-PC'; uid=sa;pwd=sa; Database='QL'; trusted_connection=true;";
            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);

            string queryString = "SELECT * FROM TRANG_THAI";
            System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(queryString, sqlConnection);

            sqlConnection.Open();
            System.Data.SqlClient.SqlDataReader dataReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            return dataReader;
        }

        public System.Data.SqlClient.SqlDataReader GetSourceTrangThaiTimKiem()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();// "server='NgoQuangLua-PC'; uid=sa;pwd=sa; Database='QL'; trusted_connection=true;";
            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);

            string queryString = "SELECT * FROM TRANG_THAI where id in (2,3)";
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
        protected void btnGhi_Click(object sender, System.EventArgs e)
        {
            if (bsua==0) //thêm mới
            {
                clsKeHoachNhapXuat oKeHoachNhapXuat = new clsKeHoachNhapXuat();
                oKeHoachNhapXuat.KeHoachNhapXuat_Id = sKeHoachNhapXuat_Id;
                oKeHoachNhapXuat.Kho_Id = cboKho.SelectedValue;
                oKeHoachNhapXuat.Code = Guid.NewGuid().ToString();
                oKeHoachNhapXuat.NhanVien_Lap_Id = cboNguoiLap.SelectedValue;
                oKeHoachNhapXuat.LyDoNhap = txtLyDoNhap.Text;
                oKeHoachNhapXuat.Dien_Giai = txtDienGiai.Text;
                oKeHoachNhapXuat.KieuNhapXuat = Convert.ToInt16(cboNhapXuat.SelectedValue);
                oKeHoachNhapXuat.DoiTac_Id = DBNull.Value.ToString();
                oKeHoachNhapXuat.NhaCungCap_Id = DBNull.Value.ToString();
                oKeHoachNhapXuat.NhanVien_Nhan_Id = DBNull.Value.ToString();
                if (cboNoiNhan.SelectedIndex == 0)
                {
                    oKeHoachNhapXuat.NhanVien_Nhan_Id = cboGiaTriNoiNhan.SelectedValue;
                }
                else if (cboNoiNhan.SelectedIndex == 1)
                {
                    oKeHoachNhapXuat.DoiTac_Id = cboGiaTriNoiNhan.SelectedValue;
                }
                else
                {
                    oKeHoachNhapXuat.NhaCungCap_Id = cboGiaTriNoiNhan.SelectedValue;
                }
                oKeHoachNhapXuat.Insert();
                
            }
            else //sửa
            {
                clsKeHoachNhapXuat oKeHoachNhapXuat = new clsKeHoachNhapXuat();
                
                oKeHoachNhapXuat.NhanVien_Lap_Id = cboNguoiLap.SelectedValue;
                oKeHoachNhapXuat.KeHoachNhapXuat_Id = Guid.NewGuid().ToString();
                oKeHoachNhapXuat.Kho_Id = cboKho.SelectedValue;
                oKeHoachNhapXuat.Code = Guid.NewGuid().ToString();
                oKeHoachNhapXuat.LyDoNhap = txtLyDoNhap.Text;
                oKeHoachNhapXuat.Dien_Giai = txtDienGiai.Text;
                oKeHoachNhapXuat.TinhTrang = "DE_NGHI";
                oKeHoachNhapXuat.DoiTac_Id = DBNull.Value.ToString();
                oKeHoachNhapXuat.NhaCungCap_Id = DBNull.Value.ToString();
                oKeHoachNhapXuat.NhanVien_Nhan_Id = DBNull.Value.ToString();
                if (cboNoiNhan.SelectedIndex == 0)
                {
                    oKeHoachNhapXuat.NhanVien_Nhan_Id = cboGiaTriNoiNhan.SelectedValue;
                }
                else if (cboNoiNhan.SelectedIndex == 1)
                {
                    oKeHoachNhapXuat.DoiTac_Id = cboGiaTriNoiNhan.SelectedValue;
                }
                else
                {
                    oKeHoachNhapXuat.NhaCungCap_Id = cboGiaTriNoiNhan.SelectedValue;
                }
                oKeHoachNhapXuat.Update();
                sKey = "";
                bsua = 0;
            }
      
            Init();
            BindData("DE_NGHI");
            bindData(-1);
        }
        protected void btnBoQua_Click(object sender, System.EventArgs e)
        {
            txtLyDoNhap.Enabled = true;
            txtDienGiai.Enabled = true;
            cboKho.Enabled = true;
            dtpNgayLap.Enabled = true;
            cboNguoiLap.Enabled = true;
            btnGhi.Enabled = true;
            Init();
            bindData(-1);
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e) 
        {

            if (e.CommandName.ToString() == "Sửa")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvRow = GridView1.Rows[index];

                if (GridView1.DataKeys[index].Value.ToString() != "")
                {
                    clsKeHoachNhapXuat objKeHoachNhapXuat = new clsKeHoachNhapXuat();
                    objKeHoachNhapXuat.KeHoachNhapXuat_Id = GridView1.DataKeys[index].Value.ToString();
                    sKeHoachNhapXuat_Id = GridView1.DataKeys[index].Value.ToString();
                    sKey = GridView1.DataKeys[index].Value.ToString();
                    DataTable mytable = new DataTable();
                    mytable = objKeHoachNhapXuat.Select_onerow();
                    if (mytable.Rows.Count > 0)
                    {
                        cboKho.SelectedValue = mytable.Rows[0]["Kho_Id"].ToString();
                        cboNguoiLap.SelectedValue = mytable.Rows[0]["NhanVien_Lap_Id"].ToString();
                        txtLyDoNhap.Text = mytable.Rows[0]["LyDoNhapXuat"].ToString();
                        txtDienGiai.Text = mytable.Rows[0]["Dien_Giai"].ToString();
                        //cboDoiTac.SelectedValue = mytable.Rows[0]["DoiTac_Id"].ToString();
                        //cboNhaCungCap.SelectedValue = mytable.Rows[0]["NhaCungCap_Id"].ToString();
                        dtpNgayLap.SelectedDate = Convert.ToDateTime(mytable.Rows[0]["NgayLap"]);
                        //sửa hết
                        if (mytable.Rows[0]["TinhTrang"].ToString() == "DE_NGHI")
                        {
                            bsua = 1;
                        }
                        //chỉ sửa tình trạng
                        else if (mytable.Rows[0]["TinhTrang"].ToString() == "PHE_DUYET" || mytable.Rows[0]["TinhTrang"].ToString() == "KHONG_PHE_DUYET")
                        {
                            bsua = 2;
                            txtLyDoNhap.Enabled = false;
                            txtDienGiai.Enabled = false;
                            cboKho.Enabled = false;
                            dtpNgayLap.Enabled = false;
                            cboNguoiLap.Enabled = false;
                        }
                        else //không sửa
                        {
                            bsua = 3;
                            txtLyDoNhap.Enabled = false;
                            txtDienGiai.Enabled = false;
                            cboKho.Enabled = false;
                            dtpNgayLap.Enabled = false;
                            cboNguoiLap.Enabled = false;
                            btnGhi.Enabled = false;
                        }
                    }
                   
                }

            }
            else if (e.CommandName.ToString() == "Xóa")
            {
                try
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    GridView1.EditIndex = id;


                    Guid delId = new Guid(GridView1.DataKeys[id].Value.ToString());
                    
                    if (GridView1.DataKeys[id].Value.ToString() != "")
                    {
                        clsKeHoachNhapXuat objKeHoachNhapXuat = new clsKeHoachNhapXuat();
                        objKeHoachNhapXuat.KeHoachNhapXuat_Id = GridView1.DataKeys[id].Value.ToString();
                        DataTable mytable = new DataTable();
                        mytable = objKeHoachNhapXuat.Select_onerow();

                        objKeHoachNhapXuat.NhanVien_Lap_Id = mytable.Rows[0]["NhanVien_Lap_Id"].ToString();
                        int status_Delete = objKeHoachNhapXuat.Delete();

                    }
                }
                catch (Exception ex)
                {
                    ltlAnnouncement.Text = "Lỗi trong quá trình xóa nhóm người dùng: " + ex.ToString();
                }
            }
            BindData("DE_NGHI");
            bindData(-1);
        }

        
        public void BindData()
        {

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();
            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            cmd.CommandText = "select a.KeHoachNhapXuat_Id,a.Code,b.TenKho,a.NgayLap,a.NgayPheDuyet,a.NguoiLap,d.TenDayDu,e.TenDayDu as TenDayDu1,a.TinhTrang,a.LyDoNhapXuat,a.Dien_Giai,h.TenDoiTac,i.TenNCC from KeHoachNhapXuat a left join NhanVien d on a.NhanVien_Lap_Id=d.NhanVien_Id left join NhanVien e on a.NhanVien_PheDuyet_Id=e.NhanVien_Id,Kho b,DoiTac h,NhaCungCap i where a.Kho_Id=b.Kho_Id and a.NhanVien_Lap_Id=d.NhanVien_Id  and  a.DoiTac_Id=h.DoiTac_Id and a.NhaCungCap_Id=i.NhaCungCap_Id";
            cmd.Connection = sqlConnection;
            da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            GridView1.DataSource = ds;
            GridView1.DataBind();
            sqlConnection.Close();

        }

        public void BindData(string sTinhTrang)
        {

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();
            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            cmd.CommandText = "select a.KeHoachNhapXuat_Id,a.Code,b.TenKho,convert(nvarchar(10),a.NgayLap,103) NgayLap,convert(nvarchar(10),a.NgayPheDuyet,103) NgayPheDuyet,a.NguoiLap,d.TenDayDu,e.TenDayDu as TenDayDu1,a.TinhTrang,a.LyDoNhapXuat,a.Dien_Giai,h.TenDoiTac,i.TenNCC from KeHoachNhapXuat a left join NhanVien d on a.NhanVien_Lap_Id=d.NhanVien_Id left join NhanVien e on a.NhanVien_PheDuyet_Id=e.NhanVien_Id left join DoiTac h on a.DoiTac_Id=h.DoiTac_Id left join NhaCungCap i on a.NhaCungCap_Id=i.NhaCungCap_Id,Kho b where a.Kho_Id=b.Kho_Id and a.NhanVien_Lap_Id=d.NhanVien_Id  and a.TinhTrang='" + sTinhTrang + "'";
            if (cboNhapXuat.SelectedIndex == 0) //nhập
            {
                cmd.CommandText += " and a.KieuNhapXuat=1";
            }
            else //xuất
            {
                cmd.CommandText += " and a.KieuNhapXuat=2";
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
            txtLyDoNhap.Text = "";
            txtDienGiai.Text = "";
            bsua = 0;
            sKeHoachNhapXuat_Id = Guid.NewGuid().ToString();
            
        }

        protected void cboTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindData("DE_NGHI");
        }

        protected void cboNhapXuat_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData("DE_NGHI");
        }
        protected void cboTenLoaiNhap_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = m_grid.FooterRow;
            string LoaiNhap_id = ((DropDownList)row.FindControl("txtITenLoaiNhap")).Text;
            DropDownList cboThanhPham = (DropDownList)row.FindControl("txtITenThanhPham");
            cboThanhPham.DataSource = GetSourceTenThanhPham(LoaiNhap_id);
            cboThanhPham.DataTextField = "TenHangMuc";
            cboThanhPham.DataValueField = "HangMuc_Id";
            cboThanhPham.DataBind();

        }

        public System.Data.SqlClient.SqlDataReader GetSourceTenThanhPham(string LoaiNhap_Id)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();// "server='NgoQuangLua-PC'; uid=sa;pwd=sa; Database='QL'; trusted_connection=true;";
            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);
            string queryString = "";
            if (LoaiNhap_Id == "1")
            {
                queryString = "SELECT ThanhPham_Id HangMuc_Id,TenThanhPham TenHangMuc FROM ThanhPham";
            }
            else
            {
                queryString = "SELECT ThietBi_Id HangMuc_Id,TenThietBi TenHangMuc FROM ThietBi";
            }
            System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(queryString, sqlConnection);

            sqlConnection.Open();
            System.Data.SqlClient.SqlDataReader dataReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            return dataReader;
        }
        
        protected void cboNoiNhan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboNoiNhan.SelectedIndex == 0)
            {
                cboGiaTriNoiNhan.DataSource = GetSourceNhanVien();
                cboGiaTriNoiNhan.DataTextField = "TenDayDu";
                cboGiaTriNoiNhan.DataValueField = "NhanVien_id";
                cboGiaTriNoiNhan.DataBind();
            }
            else if (cboNoiNhan.SelectedIndex == 1)
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
//===========================
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
                dsGroup = new DataTable();
                clsKeHoachNhapXuat objKeHoachNhapXuatChiTiet = new clsKeHoachNhapXuat();
                objKeHoachNhapXuatChiTiet.KeHoachNhapXuat_Id = sKeHoachNhapXuat_Id;
                dsGroup = objKeHoachNhapXuatChiTiet.SelectChiTiet();
                objKeHoachNhapXuatChiTiet.KeHoachNhapXuatChiTiet_Id = "0";

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

                GridViewRow row10 = m_grid.FooterRow;
                string LoaiNhap_id = ((DropDownList)row10.FindControl("txtITenLoaiNhap")).Text;
                DropDownList cboThanhPham = (DropDownList)row10.FindControl("txtITenThanhPham");
                cboThanhPham.DataSource = GetSourceTenThanhPham(LoaiNhap_id);
                cboThanhPham.DataTextField = "TenHangMuc";
                cboThanhPham.DataValueField = "HangMuc_Id";
                cboThanhPham.DataBind();
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
                clsKeHoachNhapXuat objKeHoachNhapXuat = new clsKeHoachNhapXuat();

                int id = e.RowIndex;
                m_grid.EditIndex = id;
                GridViewRow row = m_grid.Rows[id];

                //sKeHoachNhapXuat_Id = Guid.NewGuid().ToString();
                objKeHoachNhapXuat.KeHoachNhapXuatChiTiet_Id=m_grid.DataKeys[e.RowIndex].Value.ToString();
                DataTable tbltem=new DataTable();
                tbltem=objKeHoachNhapXuat.SelectChiTiet_onerow();
                
                int LoaiNhap_id = Convert.ToInt32(((DropDownList)row.FindControl("txtUTenLoaiNhap")).Text);
                string ThanhPham_id ="";
                Int32 SoLuong = Convert.ToInt32(((TextBox)row.FindControl("txtUSoLuong")).Text);
                string ThietBi_id = "";

                //  short updateId = Int16.Parse(m_grid.DataKeys[id].Value.ToString());
                if (m_grid.DataKeys[id].Value.ToString() != "")
                {
                    if (tbltem.Rows.Count > 0)
                    {
                        if (tbltem.Rows[0]["ThietBi_Id"] == DBNull.Value)
                        {
                            ThanhPham_id = tbltem.Rows[0]["ThanhPham_Id"].ToString();
                            ThietBi_id = "";
                        }
                        else
                        {
                            ThietBi_id = tbltem.Rows[0]["ThietBi_Id"].ToString();
                            ThanhPham_id = "";
                        }

                    }
                    objKeHoachNhapXuat.KeHoachNhapXuat_Id = sKeHoachNhapXuat_Id;
                    objKeHoachNhapXuat.ThanhPham_Id = ThanhPham_id;
                    objKeHoachNhapXuat.ThietBi_Id = ThietBi_id;
                    objKeHoachNhapXuat.So_Luong = SoLuong;
                    int _result = objKeHoachNhapXuat.InsertChiTiet();
                    if (_result == 1)
                    {
                        ltlAnnouncement.Text = "Sửa kế hoạch nhập xuất chi tiết '" + sKeHoachNhapXuat_Id + "' thành công!";
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
                    clsKeHoachNhapXuat objKeHoachNhapXuat = new clsKeHoachNhapXuat();

                    
                    int LoaiNhap_id = Convert.ToInt32(((DropDownList)row.FindControl("txtITenLoaiNhap")).Text);
                    string ThanhPham_id = ((DropDownList)row.FindControl("txtITenThanhPham")).Text;
                    Int32 SoLuong = Convert.ToInt32(((TextBox)row.FindControl("txtISoLuong")).Text);

                    objKeHoachNhapXuat.KeHoachNhapXuat_Id = sKeHoachNhapXuat_Id;
                    if (LoaiNhap_id == 1) //nhập thành phẩm
                    {
                        //objKeHoachNhapXuat.ThietBi_Id = DBNull.Value;
                        
                        objKeHoachNhapXuat.ThanhPham_Id = ThanhPham_id;
                    }
                    else if (LoaiNhap_id == 2) //nhập thiết bị
                    {
                        objKeHoachNhapXuat.ThietBi_Id = ThanhPham_id;
                    }
                    objKeHoachNhapXuat.So_Luong = SoLuong;


                    int _result = objKeHoachNhapXuat.InsertChiTiet();
                    if (_result >= 1)
                    {
                        ltlAnnouncement.Text = "Thêm mới kho '" + ThanhPham_id + "' thành công!";
                    }
                    if (_result == -1)
                    {
                        //ltlAnnouncement.Text = "Kho '" + ThanhPham_id + "' đã tồn tại, bạn nên kiểm tra lại!";
                    }
                    if (_result == 0)
                    {
                        //ltlAnnouncement.Text = "Có lỗi trong quá trình thêm mới!";
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
