using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace QLCV.code.common
{
	public static class SessionExtension
	{
		public static void CheckSession(this HttpSessionState state)
		{
			if (state[Constant.SESSION_USERID] != null)
			{
				string userId = state[Constant.SESSION_USERID].ToString();
				if (userId == null)
				{
					DoLogin();
				}
			}
			else DoLogin();
		}

		public static int GetUserId(this HttpSessionState state)
		{
			return state[Constant.SESSION_USERID] != null ? Convert.ToInt32(state[Constant.SESSION_USERID]) : 0;
		}
		public static string GetFullName(this HttpSessionState state)
		{
			return state[Constant.SESSION_FULLNAME] != null ? state[Constant.SESSION_FULLNAME].ToString() : String.Empty;
		}

		private static void DoLogin()
		{
			HttpContext.Current.Response.Redirect("~/login.aspx");
		}
	}
}