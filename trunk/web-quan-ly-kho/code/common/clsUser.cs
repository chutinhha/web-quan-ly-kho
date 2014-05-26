using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;

namespace QLCV.code.common
{

    public class clsUser
    {
        public DBHelper _DAO;
        private SqlParameter[] _Parameter = null;
        #region Business Properties and Methods
        private string _sEmail;
        private int _UserID;
        private int _Pass;
        private string _sFirstName;
        private string _sLastName;
        public int UserID
        {
            get
            {
                return _UserID;
            }
        }
        public int Id { get; set; }
        public string Email
        {
            get { return _sEmail; }
            set { _sEmail = value; }
        }
        public string FirstName
        {
            get
            {
                return _sFirstName;
            }
            set { _sFirstName = value; }
        }
        public string LastName
        {
            get
            {
                return _sLastName;
            }
            set { _sLastName = value; }
        }
        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }
        #endregion //Business Properties and Methods
        #region initialize
        public clsUser()
        {
            _DAO = new DBHelper(Constant._CONNECTION_STRING);
        }

        #endregion //initialize
        public bool checkPermission(string Email, string password, ref string errormsg)
        {
            try
            {
                if (GetUser_username(Email, password))
                    return true;
                else
                {
                    errormsg = "Tài khoản email hoặc mật khẩu không đúng, bạn kiểm tra lại thông tin đăng nhập!";
                    return false;
                }
                //if (Email.IndexOf('@') < 0)
                //    Email += "@vtc.vn";
                //if (Email.IndexOf("@") < 0)
                //{
                //    errormsg = "Bạn phải nhập đầy đủ tài khoản email!!";
                //    return false;
                //}
                //string userNameMail = Email.Substring(0, Email.IndexOf("@"));

                //if (!userNameMail.StartsWith(ConfigurationSettings.AppSettings["TEST_EMAIL"]))
                //{
                //    POP3Auth Pop3 = new POP3Auth();
                //    bool userIsValid = true; //(Pop3.CheckAuth(userNameMail, password) == "OK");
                //    if (userIsValid == false)
                //    {
                //        errormsg = "Tài khoản email hoặc mật khẩu không đúng, bạn kiểm tra lại thông tin đăng nhập!";
                //        return false;
                //    }
                //}
                //_sEmail = injectionString(Email);

                //if (GetUser(_sEmail) == false)
                //{
                //    errormsg = "Bạn chưa có hồ sơ nhân sự trong hệ thống, bạn hãy liên hệ với quản lý hệ thống!";
                //    return false;
                //}

            }
            catch (Exception ex)
            {
                throw new ApplicationException("checkPermission(): " + ex.Message);
            }
            return true;
        }

        //Xay dung ham loai mot so ky tu ma nguoi dung co the injection
        private string injectionString(string str)
        {
            try
            {
                string tmp;
                tmp = killChar(str).Replace("'", "''");
                return str;
            }
            catch (ArgumentNullException anex)
            {
                return "";
                throw anex;
            }
            catch (ArgumentException aex)
            {
                return "";
                throw aex;
            }
        }
        private string killChar(string strInput)
        {
            try
            {
                string newChars;

                string[] badChars = new string[] { "select", "drop", ";", "--", "insert", "delete", "xp_" };
                newChars = strInput.Trim();

                for (int i = 0; i < badChars.Length; i++)
                {
                    newChars = newChars.Replace(badChars[i], "");
                }

                return newChars;
            }
            catch (ArgumentNullException anex)
            {
                return "";
                throw anex;
            }
            catch (ArgumentException aex)
            {
                return "";
                throw aex;
            }

        }

        public bool GetUser(string Email)
        {
            _Parameter = null;
            try
            {
                CreateParameter("@Email", Email);
                DataTable _dt = _DAO.getDataTableSP("login_GetUserInfo", _Parameter);
                if (_dt.Rows.Count > 0)
                {
                    _UserID = Convert.ToInt32(_dt.Rows[0]["ID"]);
                    _sFirstName = _dt.Rows[0]["FirstName"].ToString();
                    _sLastName = _dt.Rows[0]["LastName"].ToString();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }
        public bool GetUser_username(string sUserName, string sPass)
        {
            _Parameter = null;
            try
            {
                CreateParameter("@UserName", sUserName);
                CreateParameter("@Pass", sPass);
                DataTable _dt = _DAO.getDataTableSP("SP_Login_Check_UserName", _Parameter);
                if (_dt.Rows.Count > 0)
                {
                    _UserID = Convert.ToInt32(_dt.Rows[0]["User_Id"]);
                    _sFirstName = _dt.Rows[0]["FullName"].ToString();
                    _sLastName = "";
                    _sEmail = _dt.Rows[0]["Email"].ToString();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }

        public clsUser GetInfo(string Email)
        {
            SqlCommand oCommand = new SqlCommand("login_GetUserInfo");
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.Parameters.Add(new SqlParameter("@Email", Email));
            List<clsUser> lt = _DAO.GetList<clsUser>(oCommand);
            if (lt != null && lt.Count > 0)
                return lt[0];
            return null;
        }
        public clsUser GetInfo(int UserId)
        {
            SqlCommand oCommand = new SqlCommand("sp2_User_GetInfo");
            oCommand.CommandType = CommandType.StoredProcedure;
            oCommand.Parameters.Add(new SqlParameter("@UserId", UserId));
            List<clsUser> lt = _DAO.GetList<clsUser>(oCommand);
            if (lt != null && lt.Count > 0)
                return lt[0];
            return null;
        }
        private void CreateParameter(string sParaName, object sParaValue)
        {
            if (_Parameter == null)
                Array.Resize(ref _Parameter, 1);
            else
                Array.Resize(ref _Parameter, _Parameter.Length + 1);
            _Parameter[_Parameter.Length - 1] = new SqlParameter(sParaName, sParaValue);
        }
    }
}
