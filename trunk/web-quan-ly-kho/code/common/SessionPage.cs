using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Nhansu_Lib;

namespace QLCV.code.common
{
	public class SessionPage : Page
	{
		protected override void OnInit(EventArgs e)
		{
            if (GetUserId() < 1)
            {
                Response.Redirect("~/login.aspx");
                return;
            }
			base.OnInit(e);
		}

        protected int GetUserId()
        {
            return ObjectTypeConverter.ConvertToInt32(Session[Constant.SESSION_USERID]);
        }

        protected string GetUserFullName()
        {
            return Session[Constant.SESSION_FULLNAME].ToString();
        }
	}
}