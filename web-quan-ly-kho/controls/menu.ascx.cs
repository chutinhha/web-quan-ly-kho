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
using QLCV.code.common;

using QLCV.code.common;

public partial class menu : System.Web.UI.UserControl
{
    private int userId;
    private string userName;
    //private int actionId;

    protected void Page_Load(object sender, EventArgs e)
    {
        //UserValidation m_userValid = new UserValidation();

        //if (Session["userId"] == null)
        //{
        //    Response.Redirect(Constant.PRJ_ROOT + "/Login.aspx");
        //}
        //else
        //{
        //    userName = Session["user"].ToString();

        //    //Neu User nay chua loggin thi cho out
        //    if (!m_userValid.isLoggedIn(userName))
        //    {
        //        Response.Redirect(Constant.PRJ_ROOT + "/Login.aspx");
        //    }
        //    else
        //    {
        //        //Neu Userkhong co quyen voi chuc nang do thi cho logout.
        //        userId = Int32.Parse(Session["userId"].ToString());
        //        string strURI = Request.Url.ToString();
        //        if (strURI.IndexOf("doisoat.aspx") > 0)
        //        {
        //            //TH la doisoat.aspx thi kg can phai check
        //        }
        //        else
        //        {
        //            if (!m_userValid.user_has_priv(userId, strURI))
        //            {
        //                //UserName nay khong co quyen su dung trang nay
        //                Response.Redirect(Constant.PRJ_ROOT + "/pages/Default.aspx");
        //            }
        //        }
        //   }
        //}
    }
}
