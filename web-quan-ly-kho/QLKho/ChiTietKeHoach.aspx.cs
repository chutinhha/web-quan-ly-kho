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
    public partial class ChiTietKeHoach : System.Web.UI.Page
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
            bindData(-1);
            if (!this.IsPostBack)
            {

            //    Fill_Combo_Company();
                getAuthorization();
                sKey = Guid.NewGuid().ToString();
                
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
        protected void btnBoQua_Click(object sender, System.EventArgs e)
        {
            Init();
            bindData(-1);

        }
       

        protected void btnThoat_Click(object sender, System.EventArgs e)
        {

            Response.Write("<script language='javascript'>window.close();</script>");
            
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
                dsTongHop = new DataTable();

                clsKeHoachNhapXuat objKeHoachNhapXuatChiTiet = new clsKeHoachNhapXuat();
                objKeHoachNhapXuatChiTiet.KeHoachNhapXuat_Id = sKey;
                dsGroup = objKeHoachNhapXuatChiTiet.SelectChiTiet();
                dsTongHop = objKeHoachNhapXuatChiTiet.Select_onerow();
                objKeHoachNhapXuatChiTiet.KeHoachNhapXuatChiTiet_Id = "0";
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
                    //dtpNgayLap.DateInput. = dsTongHop.Rows[0]["NgayLap"].ToString();
                    txtNguoiLap.Text = dsTongHop.Rows[0]["NguoiLap"].ToString();
                    txtLyDoNhap.Text = dsTongHop.Rows[0]["LyDoNhapXuat"].ToString();
                    txtDienGiai.Text = dsTongHop.Rows[0]["Dien_Giai"].ToString();
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
                objKeHoachNhapXuat.KeHoachNhapXuatChiTiet_Id = m_grid.DataKeys[e.RowIndex].Value.ToString();
                DataTable tbltem = new DataTable();
                tbltem = objKeHoachNhapXuat.SelectChiTiet_onerow();

                int LoaiNhap_id = Convert.ToInt32(((DropDownList)row.FindControl("txtUTenLoaiNhap")).Text);
                string ThanhPham_id = "";
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
                    objKeHoachNhapXuat.KeHoachNhapXuat_Id = sKey;
                    objKeHoachNhapXuat.ThanhPham_Id = ThanhPham_id;
                    objKeHoachNhapXuat.ThietBi_Id = ThietBi_id;
                    objKeHoachNhapXuat.So_Luong = SoLuong;
                    int _result = objKeHoachNhapXuat.InsertChiTiet();
                    if (_result == 1)
                    {
                        ltlAnnouncement.Text = "Sửa kế hoạch nhập xuất chi tiết '" + sKey + "' thành công!";
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
