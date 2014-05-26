using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;
using CrystalDecisions.Reporting;
using CrystalDecisions.ReportAppServer;
using CrystalDecisions.Enterprise;
namespace QLCV.BaoCaoThongKe
{
    public partial class ViewReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //lấy dữ liệu từ cookie
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();// "server='NgoQuangLua-PC'; uid=sa;pwd=sa; Database='QL'; trusted_connection=true;";
            System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);

            HttpCookie Cookie = Request.Cookies["BaoCaoID"];
            HttpCookie Cookie1 = Request.Cookies["TuNgay"];
            HttpCookie Cookie2 = Request.Cookies["DenNgay"];
            if (Cookie["Data"] == "1")//báo cáo kế hoạch
            {
                IFormatProvider theCultureInfo = new System.Globalization.CultureInfo("en-GB", true);
                DateTime TuNgay = DateTime.ParseExact(Cookie1["Data"], "dd/MM/yyyy", theCultureInfo);
                DateTime DenNgay = DateTime.ParseExact(Cookie2["Data"], "dd/MM/yyyy", theCultureInfo);

                rptKeHoachTrienKhai oReport = new rptKeHoachTrienKhai();
                dsReport oDs=new dsReport();
                SqlCommand ocm=new SqlCommand();
                ocm.Connection=sqlConnection;
                ocm.CommandText = "select b.TenDoiTac,a.So_Luong SoLuong,(c.DiaChi +'  '+ c.Tel) ThongTinLienHe,a.NgayLap ThoiGianDuKien,SYSDATETIME() ThoiGianTrienKhai,SYSDATETIME() ThoiGianHoanThanh,c.TenDayDu PhuTrachTrienKhai,a.TinhTrang,'' GhiChu from KeHoachNhapXuat a,DoiTac b,NhanVien c where a.DoiTac_Id=b.DoiTac_Id and a.NhanVien_Lap_Id=c.NhanVien_Id and a.NgayLap>='"+ TuNgay +"' and a.NgayLap<='"+ DenNgay +"'";
                SqlDataAdapter oAdap=new SqlDataAdapter(ocm);
                oAdap.Fill(oDs.KeHoachNhapXuat);
                oReport.SetDataSource(oDs);
                rptviewer.ReportSource = oReport;
            }
            else if (Cookie["Data"] == "2")//Nhật ký cài đặt phần mềm
            {
                IFormatProvider theCultureInfo = new System.Globalization.CultureInfo("en-GB", true);
                DateTime TuNgay = DateTime.ParseExact(Cookie1["Data"], "dd/MM/yyyy", theCultureInfo);
                DateTime DenNgay = DateTime.ParseExact(Cookie2["Data"], "dd/MM/yyyy", theCultureInfo);

                rptNhatKyCaiDatPM oReport = new rptNhatKyCaiDatPM();
                dsReport oDs = new dsReport();
                SqlCommand ocm = new SqlCommand();
                ocm.Connection = sqlConnection;
                ocm.CommandText = "select a.NgayLap,a.NguoiPheDuyet,a.NguoiLap,b.TenDoiTac,a.So_Luong,d.Ma_Serial,d.Ma_MAC,d.Ma_DLK,d.Dien_Giai,c.PhieuXuat_Id as gr from KeHoachNhapXuat a,DoiTac b,PhieuXuat c,ThongTinThietBi d where a.DoiTac_Id=b.DoiTac_Id and a.KeHoachNhapXuat_Id=c.KeHoachNhapXuat_Id and c.PhieuXuat_Id=d.PhieuXuat_Id";
                SqlDataAdapter oAdap = new SqlDataAdapter(ocm);
                oAdap.Fill(oDs.NhatKyCaiDatPM);
                oReport.SetDataSource(oDs);
                rptviewer.ReportSource = oReport;
            }
          
        }
    }
}
