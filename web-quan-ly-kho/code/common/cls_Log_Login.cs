using System;
using System.Data;
using System.Data.SqlClient;
namespace QLCV.code.common
{
	[Serializable()]
	public class cls_Log_Login
	{
		public DBHelper _DAO;
		private SqlParameter[] _Parameter = null;
		#region Business Properties and Methods

		private long _ID;
		private int _EmployeeID;
		private DateTime _Date_Login;
		private DateTime _Date_Logout;
		private string _IPLogin;
		private string _Client_Name;
		private string _Note;
		public long ID
		{
			get
			{
				 return _ID ;
			}
			set
			{
				_ID = value;
			}
		}
		public int EmployeeID
		{
			get
			{
				 return _EmployeeID ;
			}
			set
			{
				_EmployeeID = value;
			}
		}
		public DateTime Date_Login
		{
			get
			{
				 return _Date_Login ;
			}
			set
			{
				_Date_Login = value;
			}
		}
		public DateTime Date_Logout
		{
			get
			{
				 return _Date_Logout ;
			}
			set
			{
				_Date_Logout = value;
			}
		}
		public string IPLogin
		{
			get
			{
				 return _IPLogin ;
			}
			set
			{
				_IPLogin = value;
			}
		}
		public string Client_Name
		{
			get
			{
				 return _Client_Name ;
			}
			set
			{
				_Client_Name = value;
			}
		}
		public string Note
		{
			get
			{
				 return _Note ;
			}
			set
			{
				_Note = value;
			}
		}
		#endregion //Business Properties and Methods
		#region initialize
		public cls_Log_Login()
		{
			_DAO = new DBHelper(System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString());
		}

		#endregion //initialize
		#region Data Access
        public DataTable Get_Log(string sFromDate, string sToDate, int DertpartmentID, string sEmployeeName)
        {
            _Parameter = null;
            try
            {

                CreateParameter("@DateFrom", sFromDate);
                CreateParameter("@DateTo", sToDate);
                CreateParameter("@DepartmentID", DertpartmentID);
                CreateParameter("@EmployeeName", sEmployeeName);

                return _DAO.getDataTableSP("SP_Login_Get_View_Log", _Parameter);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable Get_Detail_Log(long IDLog, string sModule)
        {
            _Parameter = null;
            try
            {

                CreateParameter("@IDLog", IDLog);
                CreateParameter("@Module", sModule);


                return _DAO.getDataTableSP("SP_Loin_Get_Detail_Operator", _Parameter);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
		public int Insert()
		{
			_Parameter = null;
			try
			{
 				
				CreateParameter("@EmployeeID",_EmployeeID);
				CreateParameter("@Date_Login",_Date_Login);
				CreateParameter("@IPLogin",_IPLogin);
				CreateParameter("@Client_Name",_Client_Name);
				CreateParameter("@Note",_Note);
				return _DAO.ExecuteSP("SP_Log_Login_Insert", _Parameter);
			}
			catch (Exception ex)
			{
				return -1;
			}
		}
 		public int Update()
		{
			_Parameter = null;
			try
			{
 				CreateParameter("@ID",_ID);
				CreateParameter("@EmployeeID",_EmployeeID);
				CreateParameter("@Date_Logout",_Date_Logout);
				return _DAO.ExecuteSP("SP_Log_Login_Update", _Parameter);
			}
			catch (Exception ex)
			{
				return -1;
			}
		}
 		public int Delete()
		{
			_Parameter = null;
			try
			{
 				CreateParameter("@ID",_ID);
				return _DAO.ExecuteSP("SP_Log_Login_Delete", _Parameter);
			}
			catch (Exception ex)
			{
				return -1;
			}
		}
		private void CreateParameter(string sParaName, object sParaValue)
		{
			if (_Parameter == null)
				Array.Resize(ref _Parameter, 1);
			else
				Array.Resize(ref _Parameter, _Parameter.Length + 1);
			_Parameter[_Parameter.Length - 1] = new SqlParameter(sParaName, sParaValue);
		}
		#endregion //Data Access
	}
}