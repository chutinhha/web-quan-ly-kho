using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//using Zooz.Libs;

using QLCV.code.common;
public partial class controls_footer : System.Web.UI.UserControl
{
    public string UrlImages = Constant.IMG_PATH;
    protected void Page_Load(object sender, EventArgs e)
    {
        //ltlFooter0.Text = "Phần mềm General Internal Report Information System phiên bản 2.1, 2008. Bản quyền Công ty Đầu tư và phát triển Công nghệ thông tin InteCom.";
        //ltlFooter1.Text = "Địa chỉ: Tầng 2, Chung cư 46, Ngõ 230 Lạc Trung, quận Hai Bà Trưng, Hà Nội.";
        //ltlFooter2.Text = "Email: software@vtc.vn   Điện thoại: +844-6367727 ex: 165   Fax: +844-6367728";
    }
}
