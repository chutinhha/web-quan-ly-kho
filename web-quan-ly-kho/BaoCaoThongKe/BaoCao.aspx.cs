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

namespace QLCV.BaoCaoThongKe
{
    public partial class BaoCao : System.Web.UI.Page
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

            if (!this.IsPostBack)
            {

                //Fill_Combo_Company();
                getAuthorization();
                cboLoaiBaoCao.DataSource = GetSourceBaoCao();
                cboLoaiBaoCao.DataTextField = "Ten_BaoCao";
                cboLoaiBaoCao.DataValueField = "Report_Id";
                cboLoaiBaoCao.DataBind();
                //
                IFormatProvider theCultureInfo = new System.Globalization.CultureInfo("en-GB", true);
                DateTime TuNgay = DateTime.ParseExact("01/" + string.Format("{0:00}",DateTime.Today.Month) + "/" + DateTime.Today.Year, "dd/MM/yyyy", theCultureInfo);
                DateTime DenNgay = DateTime.ParseExact(GetDaysInMonth(DateTime.Today.Month,DateTime.Today.Year).ToString()+ "/" + string.Format("{0:00}",DateTime.Today.Month) + "/" + DateTime.Today.Year, "dd/MM/yyyy", theCultureInfo);
                dtpTuNgay.SelectedDate = TuNgay;
                dtpDenNgay.SelectedDate = DenNgay;
                
            }
            _dtFunction = new DataTable();
            _dtFunction = (new QLCV.code.common.clsBCTKMain()).Get_List_Menu(Session["UserId"].ToString());
            
          
          
        }
        public int GetDaysInMonth(int month, int year)
        {
            DateTime date = DateTime.Now;

            if (month == 12) date = new DateTime(year + 1, 1, 1);
            else date = new DateTime(year, month + 1, 1);

            return date.AddDays(-1).Day;
        }
       
        protected void btnKetXuat_Click(object sender, EventArgs e)
        {
            string url = "../BaoCaoThongKe/ViewReport.aspx";
            string newWin = "window.open('" + url + "','_blank','height=700,width=1000,left='+((screen.width - 1000) / 2)+',top='+((screen.height - 700)/2)+',status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no');";
            //loại báo cáo
            HttpCookie C = new HttpCookie("BaoCaoID");
            C["Data"] = cboLoaiBaoCao.SelectedValue.ToString();
            Response.Cookies.Add(C);
            //ngày tháng
            HttpCookie D1 = new HttpCookie("TuNgay");
            D1["Data"] = string.Format("{0:dd/MM/yyyy}",dtpTuNgay.SelectedDate);
            Response.Cookies.Add(D1);

            HttpCookie D2 = new HttpCookie("DenNgay");
            D2["Data"] = string.Format("{0:dd/MM/yyyy}", dtpDenNgay.SelectedDate); 
            Response.Cookies.Add(D2);

            ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
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


        public System.Data.SqlClient.SqlDataReader GetSourceBaoCao()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();// "server='NgoQuangLua-PC'; uid=sa;pwd=sa; Database='QL'; trusted_connection=true;";
            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);

            string queryString = "SELECT * FROM DanhMucBaoCao";
            System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(queryString, sqlConnection);

            sqlConnection.Open();
            System.Data.SqlClient.SqlDataReader dataReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            return dataReader;
        }

     

        //protected void GridView1_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        //{

        //    string currentCommand = e.CommandName;
        //    int currentRowIndex = Int32.Parse(e.CommandArgument.ToString());
        //    string ProductID = GridView1.DataKeys[currentRowIndex].Value.ToString();

        //    if (currentCommand == "NhapChiTiet")
        //    {
        //        string url = "../QLKho/ThongTinThietBi.aspx";
        //        string newWin = "window.open('" + url + "','_blank','height=400,width=1366,left='+((screen.width - 1366) / 2)+',top='+((screen.height - 400)/2)+',status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no');";
        //        HttpCookie C = new HttpCookie("PhieuNhapID");
        //        C["Data"] = "xyz";
        //        Response.Cookies.Add(C);

        //        ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
        //    }
        //}


        

       
    }
}
