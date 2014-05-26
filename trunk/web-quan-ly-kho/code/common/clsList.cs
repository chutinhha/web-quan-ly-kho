using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
namespace QLCV.code.common
{
    public class clsList
    {
        public DBHelper _DAO;
        private SqlParameter[] _Parameter = null;
        public clsList()
		{
			_DAO = new DBHelper(System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString());
		}
        public DataTable Get_List_Menu( string sUserID)
        {
            _Parameter = null;
            try
            {
                CreateParameter("@UserID", sUserID);
                return _DAO.getDataTableSP("SP_List_Module", _Parameter);
            }
            catch (Exception ex)
            {
                return null;
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
    }
}
