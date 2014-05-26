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
public partial class master_default_tab : System.Web.UI.MasterPage
{
    //public string UrlImages = Constant.IMG_PATH;
    public string UrlJs = Constant.JS_PATH;
    //public string Url__ = Constant.PRJ_ROOT;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["user"] == null)
            Response.Redirect(Constant.PRJ_ROOT + "/Login.aspx");
    }
}
