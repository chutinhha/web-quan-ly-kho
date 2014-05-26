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
    public partial class NhanVien : System.Web.UI.Page
    {
        private int EditIndex = -1;
     
        protected DataTable dsGroup = null;

        private static bool I_Option;                              // QUYEN INSERT
        private static bool U_Option;                              // QUYEN UPDATE
        private static bool D_Option;
        public DataTable _dtFunction;
        
        static bool bsua = false;
        static string sKey = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            // ---- KIEM TRA LOGIN -----
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/login.aspx");
            }
            // ---- END KT LOGIN -------

            //if (!this.IsPostBack)
            //{

            //    Fill_Combo_Company();
            //    getAuthorization();
            //    bindData(-1);
            //}
            _dtFunction = new DataTable();
            _dtFunction = (new QLCV.code.common.clsList()).Get_List_Menu(Session["UserId"].ToString());
            cboPhongBan.DataSource = GetSourcePhongBan();
            cboPhongBan.DataTextField = "tenphongban";
            cboPhongBan.DataValueField = "Phongban_id";
            cboPhongBan.DataBind();
            BindData();
        }

        public System.Data.SqlClient.SqlDataReader GetSourcePhongBan()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();// "server='NgoQuangLua-PC'; uid=sa;pwd=sa; Database='QL'; trusted_connection=true;";
            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);

            string queryString = "SELECT * FROM PhongBan";
            System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(queryString, sqlConnection);

            sqlConnection.Open();
            System.Data.SqlClient.SqlDataReader dataReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            return dataReader;
        }
        protected void btnGhi_Click(object sender, System.EventArgs e)
        {
            if (!bsua) //thêm mới
            {
                clsNhanVien oNhanVien = new clsNhanVien();
                oNhanVien.PhongBan_Id = cboPhongBan.SelectedValue.ToString();
                oNhanVien.Ten = txtTen.Text;
                oNhanVien.Ho = txtHo.Text;
                oNhanVien.TenDayDu = txtTenDayDu.Text;
                oNhanVien.DiaChi = txtDiaChi.Text;
                oNhanVien.Tel = txtTel.Text;
                oNhanVien.SubTel = txtSubTel.Text;
                oNhanVien.Mobile = txtMobile.Text;
                oNhanVien.HomePhone = "";
                oNhanVien.Main_EMail = txtEmail.Text;
                oNhanVien.Other_Email = txtOtherEmail.Text;
                oNhanVien.ChucDanh = txtChucDanh.Text;
                oNhanVien.GioiTinh = (byte)(cboGioiTinh.SelectedValue == "1" ? 1 : 0);
                oNhanVien.Insert();
            }
            else //sửa
            {
                clsNhanVien oNhanVien = new clsNhanVien();
                oNhanVien.NhanVien_Id = sKey;
                oNhanVien.PhongBan_Id = cboPhongBan.SelectedValue.ToString();
                oNhanVien.Ten = txtTen.Text;
                oNhanVien.Ho = txtHo.Text;
                oNhanVien.TenDayDu = txtTenDayDu.Text;
                oNhanVien.DiaChi = txtDiaChi.Text;
                oNhanVien.Tel = txtTel.Text;
                oNhanVien.SubTel = txtSubTel.Text;
                oNhanVien.Mobile = txtMobile.Text;
                oNhanVien.HomePhone = "";
                oNhanVien.Main_EMail = txtEmail.Text;
                oNhanVien.Other_Email = txtOtherEmail.Text;
                oNhanVien.ChucDanh = txtChucDanh.Text;
                oNhanVien.GioiTinh = (byte)(cboGioiTinh.SelectedValue == "1" ? 1 : 0);
                oNhanVien.Update();
                sKey = "";
                bsua = false;
            }
            Init();
            BindData();
        }

      

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e) 
        {
            
            if (e.CommandName.ToString() == "Sửa")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvRow = GridView1.Rows[index];

                if (GridView1.DataKeys[index].Value.ToString() != "")
                {
                    clsNhanVien objNhanVien = new clsNhanVien();
                    objNhanVien.NhanVien_Id = GridView1.DataKeys[index].Value.ToString();
                    sKey=  GridView1.DataKeys[index].Value.ToString();
                    DataTable mytable = new DataTable();
                    mytable = objNhanVien.Select_onerow();
                    if (mytable.Rows.Count > 0)
                    {
                        cboPhongBan.SelectedValue = mytable.Rows[0]["PhongBan_Id"].ToString();
                        txtTen.Text = mytable.Rows[0]["Ten"].ToString();
                        txtHo.Text = mytable.Rows[0]["Ho"].ToString();
                        txtTenDayDu.Text = mytable.Rows[0]["TenDayDu"].ToString();
                        txtDiaChi.Text = mytable.Rows[0]["DiaChi"].ToString();
                        txtTel.Text = mytable.Rows[0]["Tel"].ToString();
                        txtSubTel.Text = mytable.Rows[0]["SubTel"].ToString();
                        txtMobile.Text = mytable.Rows[0]["Mobile"].ToString();
                        txtEmail.Text = mytable.Rows[0]["Main_Email"].ToString();
                        txtOtherEmail.Text = mytable.Rows[0]["Other_Email"].ToString();
                        txtChucDanh.Text = mytable.Rows[0]["ChucDanh"].ToString();
                        if (mytable.Rows[0]["GioiTinh"].ToString() == "1")
                        {
                            cboGioiTinh.SelectedIndex = 0;
                        }

                    }
                    bsua = true;
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
                        clsNhanVien objNhanVien = new clsNhanVien();
                        objNhanVien.NhanVien_Id = GridView1.DataKeys[id].Value.ToString();
                        int status_Delete = objNhanVien.Delete();
                       
                    }
                }
                catch (Exception ex)
                {
                    ltlAnnouncement.Text = "Lỗi trong quá trình xóa nhóm người dùng: " + ex.ToString();
                }
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
            cmd.CommandText = "SELECT a.NhanVien_Id,b.TenPhongBan,a.Ten,a.Ho,a.TenDayDu,a.DiaChi,a.Tel,a.SubTel,a.Mobile,a.HomePhone,a.Main_EMail,a.Other_Email,a.ChucDanh,case a.GioiTinh when '1' then 'Nam' when '0' then N'Nữ' end as GioiTinh	FROM NhanVien a, phongban b where a.phongban_id=b.phongban_id	ORDER BY a.TenDayDu";
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
            txtTen.Text = "";
            txtHo.Text = "";
            txtTenDayDu.Text ="";
            txtDiaChi.Text = "";
            txtTel.Text = "";
            txtSubTel.Text = "";
            txtMobile.Text = "";
            txtEmail.Text = "";
            txtOtherEmail.Text = "";
            txtChucDanh.Text = "";
        }

    }
}
