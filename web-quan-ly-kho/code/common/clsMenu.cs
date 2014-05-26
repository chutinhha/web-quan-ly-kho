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
using System.Data;
using System.Data.SqlClient;
//using QLCV;
namespace QLCV.code.common
{
    public sealed class clsMenu
    {
        public static DBHelper _DAO = new DBHelper(System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString());
        private static SqlParameter[] _Parameter = null;

        #region initialize
        public clsMenu()
        {
            _DAO = new DBHelper(System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString());
        }

        #endregion //initialize

        public static string genNewMenu(int userId)
        {
            //Co time se chuyen ban ben _Example_1 sang
            try
            {
                string tmp = "";
                string fullName = "";

                //UsersImpl userImpl = new UsersImpl();
                //Users m_user = userImpl.GetUser(userId);
                //fullName = m_user.FullName;
                //ActionsImpl actionsImpl = new ActionsImpl();
                string _ActionId;
                string _Name;
                string _Url;
                string _parentid;
                string _MainMenuName = "menu";

                tmp += "<table border='0' cellpadding='0' cellspacing='0' width='100%' id='table1'>";
                tmp += "\n<tr>";
                tmp += "\n<td background='" + Constant.PRJ_ROOT + "/images/bg.jpg' height='90'>";
                tmp += "\n<table border='0' cellpadding='0' cellspacing='0' width='100%' id='table2' height='90'>";
                tmp += "\n<tr>";
                tmp += "\n<td width='144'>";
                tmp += "\n<img border='0' src='" + Constant.PRJ_ROOT + "/images/logo.jpg' width='144' height='90'></td>";
                tmp += "\n<td>";
                tmp += "\n<table border='0' cellpadding='0' cellspacing='0' width='100%' id='table3' height='44'>";
                tmp += "\n<tr>";
                tmp += "\n<td valign='bottom' class='tenweb'>General Internal Report Information System</td>";
                tmp += "\n</tr>";
                tmp += "\n</table>";
                tmp += "\n</td>";
                tmp += "\n<td valign='top'>";
                tmp += "\n<div align='right'>";
                tmp += "\n<table border='0' cellpadding='0' cellspacing='0' id='table4'>";
                tmp += "\n<tr>";
                tmp += "\n<td background='" + Constant.PRJ_ROOT + "/images/bg_menutop.jpg' width='16'>";
                tmp += "\n<img border='0' src='" + Constant.PRJ_ROOT + "/images/dau.jpg' width='16' height='26'></td>";
                tmp += "\n<td background='" + Constant.PRJ_ROOT + "/images/bg_menutop.jpg' class='linkmenu'>";
                tmp += "\n<table style='border-collapse: collapse;' id='AutoNumber5' width='100%' border='0' bordercolor='#111111' cellpadding='0' cellspacing='0'>";
                tmp += "\n    <tbody>";
                tmp += "\n        <tr>";
                tmp += "\n            <td width='100%'>";
                tmp += "\n                <table style='border-collapse: collapse;' id='AutoNumber7' width='100%' border='0'";
                tmp += "\n                    bordercolor='#111111' cellpadding='4' cellspacing='0'>";
                tmp += "\n                    <tbody>";
                tmp += "\n                        <tr>";
                int i = 0;
                //DataTable dtMain = GetMainMenu(userId);
                //foreach (DataRow dr in dtMain.Rows)
                //{
                //    _ActionId =dr["MenuId"].ToString();
                //    _Name = dr["MenuName"].ToString();
                //    _Url = dr["LinkRun"].ToString();
                //    tmp += "\n <td id='" + _MainMenuName + _ActionId + "' align='center'>";
                //    if (i > 0) tmp += " | ";
                //    tmp += "\n<b><a  href='#' STYLE='text-decoration:none;cursor:hand;' ><font style='font-family:Verdana;font-size:10;font-weight:bold;color=#111111'>" + _Name + "</font></a></b>";
                //    tmp += "\n </td>";
                //    i++;
                //}
                tmp += "\n                        </tr>";
                tmp += "\n                    </tbody>";
                tmp += "\n                </table>";
                tmp += "\n            </td>";
                tmp += "\n        </tr>";
                tmp += "\n    </tbody>";
                tmp += "\n</table>";

                //tmp += "\n<script type=text/javascript language=javascript>";
                //tmp += "\nfunction init() {";
                //tmp += "\nif (TransMenu.isSupported()) {";
                //tmp += "\nTransMenu.initialize();";
                //DataTable _dtChild;
                //DataTable _Queue = new DataTable();
                //string _IDMenu;
                //foreach (DataRow dr in dtMain.Rows)
                //{
                //     _dtChild = GetMainMenu_Child(Convert.ToInt32(dr["MenuId"]), userId);
                //     _Queue = _dtChild.Clone();
                //    DataRow[] _Row = _dtChild.Select("MenuId = " + dr["MenuId"].ToString());
                //    DataRow[] _RowMenu;
                //    _Queue.Rows.Add(_Row[0].ItemArray);
                //    while (_Queue.Rows.Count>0)
                //    {
                //        _IDMenu = _Queue.Rows[0]["MenuId"].ToString();
                //        _Row = _dtChild.Select("MenuParentId = " + _IDMenu);
                //        for (int k = 0; k < _Row.Length; k++)
                //        {
                //            _Queue.Rows.Add(_Row[k].ItemArray);
                //        }
                //        if (_IDMenu != dr["MenuId"].ToString())
                //        {
                //            _RowMenu = _dtChild.Select("MenuId = " + _IDMenu);
                //            _ActionId = _RowMenu[0]["MenuId"].ToString();
                //            tmp += "\n" + _MainMenuName + _ActionId + ".onactivate = function() { ";
                //            tmp += "\ndocument.getElementById('" + _MainMenuName + _ActionId + "').className = 'hover'; ";
                //            tmp += "\n};";
                //            tmp += "\n" + _MainMenuName + _ActionId + ".ondeactivate = function() {";
                //            tmp += "\ndocument.getElementById('" + _MainMenuName + _ActionId + "').className = '';";
                //            tmp += "\n};";
                //        }
                //        _Queue.Rows[0].Delete();
                //        _Queue.AcceptChanges();

                //    }
                //    //foreach (Actions actionChild in actionsImpl.GetChildActionsByUser(userId, action.ActionId))
                //    //{
                //    //    _ActionId = action.ActionId;
                //    //    tmp += "\n" + _MainMenuName + _ActionId.ToString() + ".onactivate = function() { ";
                //    //    tmp += "\ndocument.getElementById('" + _MainMenuName + _ActionId.ToString() + "').className = 'hover'; ";
                //    //    tmp += "\n};";
                //    //    tmp += "\n" + _MainMenuName + _ActionId.ToString() + ".ondeactivate = function() {";
                //    //    tmp += "\ndocument.getElementById('" + _MainMenuName + _ActionId.ToString() + "').className = '';";
                //    //    tmp += "\n};";
                //    //}
                //}
                //tmp += "}";
                //tmp += "}";
                //tmp += "\n</Script>";

                //tmp += "\n<script language='javascript'>";
                //tmp += "\nif (TransMenu.isSupported()) {";
                //tmp += "\n    var ms = new TransMenuSet(TransMenu.direction.down, 1, 1, TransMenu.reference.bottomLeft);";
                //tmp += "\n    var ItemArray;";
                //tmp += "\n    var LinkArray;";

                //int j;
                //foreach (DataRow dr in dtMain.Rows)
                //{
                //    _ActionId = _ActionId = dr["MenuId"].ToString(); ;
                //    tmp += "\n    var " + _MainMenuName + _ActionId + " = ms.addMenu(document.getElementById('" + _MainMenuName + _ActionId + "'));";

                //    _dtChild = GetMainMenu_Child(Convert.ToInt32(dr["MenuId"]), userId);
                //    _Queue = _dtChild.Clone();
                //    DataRow[] _Row = _dtChild.Select("MenuId = " + dr["MenuId"].ToString());
                //    DataRow[] _RowMenu;
                //    _Queue.Rows.Add(_Row[0].ItemArray);
                //    //

                //    while (_Queue.Rows.Count > 0)
                //    {
                //        _IDMenu = _Queue.Rows[0]["MenuId"].ToString();
                //        _Row = _dtChild.Select("MenuParentId = " + _IDMenu);
                //        tmp += "\n    ItemArray = new Array('" + _Row.Length.ToString() + "');";
                //        tmp += "\n    LinkArray = new Array('" + _Row.Length.ToString() + "');";
                //        for (int k = 0; k < _Row.Length; k++)
                //        {
                //            _Queue.Rows.Add(_Row[k].ItemArray);
                //            //
                //           // _parentid = actionChild.ActionId;
                //            _Url = _Row[k]["LinkRun"].ToString().Trim();
                //            _Name = _Row[k]["MenuName"].ToString();

                //            tmp += "\nItemArray['" + k.ToString() + "'] = '" + _Name + "';";
                //            tmp += "\nLinkArray['" + k.ToString() + "'] = '" + Constant.PRJ_ROOT + _Url + "';";
                //            tmp += "\n" + _MainMenuName + _ActionId.ToString() + ".addItem(ItemArray['" + k.ToString() + "'], LinkArray['" + k.ToString() + "']);";

                //        }
                //        //if (_IDMenu != dr["MenuId"].ToString())
                //        //{
                //        //    _RowMenu = _dtChild.Select("MenuId = " + _IDMenu);
                //        //    _ActionId = _RowMenu[0]["MenuId"].ToString();
                //        //    tmp += "\n" + _MainMenuName + _ActionId + ".onactivate = function() { ";
                //        //    tmp += "\ndocument.getElementById('" + _MainMenuName + _ActionId + "').className = 'hover'; ";
                //        //    tmp += "\n};";
                //        //    tmp += "\n" + _MainMenuName + _ActionId + ".ondeactivate = function() {";
                //        //    tmp += "\ndocument.getElementById('" + _MainMenuName + _ActionId + "').className = '';";
                //        //    tmp += "\n};";
                //        //}
                //        _Queue.Rows[0].Delete();
                //        _Queue.AcceptChanges();

                //    }

                //    //ActionsImpl objs = new ActionsImpl();
                //    //IList objsList = objs.GetChildActionsByUser(userId, action.ActionId);
                //    //tmp += "\n    ItemArray = new Array('" + objsList.Count.ToString() + "');";
                //    //tmp += "\n    LinkArray = new Array('" + objsList.Count.ToString() + "');";
                //    //j = 0;
                //    //foreach (Actions actionChild in objsList)
                //    //{
                //    //    _parentid = actionChild.ActionId;
                //    //    _Url = actionChild.Url.ToString().Trim();
                //    //    _Name = actionChild.Name.ToString();

                //    //    tmp += "\nItemArray['" + j.ToString() + "'] = '" + _Name + "';";
                //    //    tmp += "\nLinkArray['" + j.ToString() + "'] = '" + Constant.PRJ_ROOT + _Url + "';";
                //    //    tmp += "\n" + _MainMenuName + _ActionId.ToString() + ".addItem(ItemArray['" + j.ToString() + "'], LinkArray['" + j.ToString() + "']);";
                //    //    j++;
                //    //}
                //}

                ////==================================================================================================
                //tmp += "\n    TransMenu.renderAll();";
                //tmp += "\n }";
                //tmp += "\n            </script>";

                tmp += "\n  </td>";
                tmp += "\n				<td background='" + Constant.PRJ_ROOT + "/images/bg_menutop.jpg' width='10'>&nbsp;</td>";
                tmp += "\n			</tr>";
                tmp += "\n	  </table>";
                tmp += "\n	</div>";
                tmp += "\n</td>";
                tmp += "\n</tr>";
                tmp += "\n</table>";
                tmp += "\n</td>";
                tmp += "\n</tr>";
                tmp += "\n<tr class='tabgreenMidBody' >";
                tmp += "\n<td align='left' style='font-family:Verdana; font-size:10; font-weight:bold'>&nbsp;" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss tt");
                tmp += " | Tài khoản: " + fullName;
                //tmp += " |<a href='" + Constant.PRJ_ROOT + "/Default.aspx'><font style='font-family:Verdana;font-size:10;font-weight:bold;color=#FFFFFF'>Home</font> </a> ";
                tmp += " |<a href='" + Constant.PRJ_ROOT + "/Logout.aspx'><font style='font-family:Verdana;font-size:10;font-weight:bold;color=#FFFFFF'>Thoát</font> </a>";
                tmp += "\n</td>	";
                tmp += "\n</tr>";
                tmp += "\n</table>";

                return tmp;
            }
            catch (Exception ex)
            {
                return "";
                //throw ex;
            }
        }
        public static string genMenu(int userId)
        {
            //Co time se chuyen ban ben _Example_1 sang
            try
            {
                string tmp = "";
                string fullName = "";

                //UsersImpl userImpl = new UsersImpl();
                //Users m_user = userImpl.GetUser(userId);
                //fullName = m_user.FullName;
                //ActionsImpl actionsImpl = new ActionsImpl();
                string _ActionId;
                string _Name;
                string _Url;
                string _parentid;
                string _MainMenuName = "menu";

                tmp += "  <table border='0' cellpadding='0' cellspacing='0' width='100%' ";
                tmp += "id='table1'>";
                tmp += "<tr><td background='/images/bg.jpg' height='30px'><table border='0' cellpadding='0' cellspacing='0' width='100%' id='table2' height='30px'><tr><td class='style3'>";
                tmp += "&nbsp;</td><td class='style2'></td>";
                tmp += "<td class='style1'><table border='0' cellpadding='0' cellspacing='0' width='100%' id='table3' height='20px'><tr><td valign='bottom' align='center' class='tenweb'>General Internal Report Information System</td></tr></table></td><td valign='top'><div align='right'><table border='0' cellpadding='0' cellspacing='0' id='table4'><tr>";
                tmp += " <td background='/images/bg_menutop.jpg'>";
                tmp += "</td>";
                tmp += "<td background='/images/bg_menutop.jpg' class='linkmenu'><table style='border-collapse: collapse;' id='AutoNumber5' width='100%' border='0' bordercolor='#111111' cellpadding='0' cellspacing='0'>  ";
                tmp += " <tbody>        <tr>            <td width='100%'>        ";
                tmp += " </td>        </tr>    </tbody></table>  </td></tr>  </table>";
                tmp += " </div></td></tr></table></td></tr>";
                tmp += "<tr><td> <telerik:RadMenu ID='RadMenu1' Runat='server' BorderColor='#990099' ";
                tmp += " Skin='Office2007'>";
                tmp += "<CollapseAnimation Type='OutQuint' Duration='200'></CollapseAnimation>  <Items> ";

                int i = 0;
                DataTable dtMain = GetMainMenu(userId);

                DataTable _dtChild;
                DataTable _Stack = new DataTable();
                string _IDMenu;
                foreach (DataRow dr in dtMain.Rows)
                {
                    //tmp += "<li class='current'>";
                    //tmp += "<a href='#'> " + dr["MenuName"].ToString() + "</a>";
                    //tmp += "<ul> ";
                    _dtChild = GetMainMenu_Child(Convert.ToInt32(dr["MenuId"]), userId);
                    _Stack = _dtChild.Clone();

                    DataRow[] _Row = _dtChild.Select("(MenuId = " + dr["MenuId"].ToString() + ")");
                    DataRow[] _RowMenu;
                    _Stack.Rows.Add(_Row[0].ItemArray);
                    while (_Stack.Rows.Count > 0)
                    {
                        _IDMenu = _Stack.Rows[_Stack.Rows.Count - 1]["MenuId"].ToString();
                        if (_Stack.Rows[_Stack.Rows.Count - 1]["LinkRun"].ToString() == "co")
                        {

                            tmp += "</Items>";
                            tmp += "</telerik:RadMenuItem>";
                            _Stack.Rows[_Stack.Rows.Count - 1].Delete();
                            _Stack.AcceptChanges();
                        }
                        else
                        {
                            _Row = _dtChild.Select("(MenuParentId = " + _IDMenu + ")", "MenuOrder desc");
                            if (_Row.Length > 0)
                            {
                                tmp += "<telerik:RadMenuItem runat='server' Text = '" + _Stack.Rows[_Stack.Rows.Count - 1]["MenuName"].ToString() + "'";
                                if (_Stack.Rows[_Stack.Rows.Count - 1]["LinkRun"].ToString().Length > 0)
                                    tmp += " NavigateUrl='" + _Stack.Rows[_Stack.Rows.Count - 1]["LinkRun"].ToString() + "?M_Id=" + _Stack.Rows[_Stack.Rows.Count - 1]["MenuId"].ToString() + "'>";
                                else
                                    tmp += " NavigateUrl='#'>";
                                tmp += "<Items>";
                                _Stack.Rows[_Stack.Rows.Count - 1]["LinkRun"] = "co";
                                for (int k = 0; k < _Row.Length; k++)
                                {
                                    _Stack.Rows.Add(_Row[k].ItemArray);
                                }
                            }
                            else
                            {
                                tmp += "<telerik:RadMenuItem runat='server' Text = '" + _Stack.Rows[_Stack.Rows.Count - 1]["MenuName"].ToString() + "'";
                                tmp += " NavigateUrl='" + _Stack.Rows[_Stack.Rows.Count - 1]["LinkRun"].ToString() + "?M_Id=" + _Stack.Rows[_Stack.Rows.Count - 1]["MenuId"].ToString() + "'>";
                                tmp += "</telerik:RadMenuItem>";
                                _Stack.Rows[_Stack.Rows.Count - 1].Delete();
                                _Stack.AcceptChanges();
                            }
                        }

                        //     tmp += "</ul> ";
                        //tmp += "</li>";

                    }
                }
                //foreach (DataRow dr in dtMain.Rows)
                //{
                //    //tmp += "<li class='current'>";
                //    //tmp += "<a href='#'> " + dr["MenuName"].ToString() + "</a>";
                //    //tmp += "<ul> ";
                //    _dtChild = GetMainMenu_Child(Convert.ToInt32(dr["MenuId"]), userId);
                //    _Stack = _dtChild.Clone();

                //    DataRow[] _Row = _dtChild.Select("MenuId = " + dr["MenuId"].ToString());
                //    DataRow[] _RowMenu;
                //    _Stack.Rows.Add(_Row[0].ItemArray);
                //    while (_Stack.Rows.Count > 0)
                //    {
                //        _IDMenu = _Stack.Rows[_Stack.Rows.Count-1]["MenuId"].ToString();
                //        if (_Stack.Rows[_Stack.Rows.Count - 1]["LinkRun"].ToString() == "co")
                //        {
                //            tmp += "</ul> ";
                //            tmp += "</li>";
                //            _Stack.Rows[_Stack.Rows.Count - 1].Delete();
                //            _Stack.AcceptChanges();
                //        }
                //        else
                //        {
                //            _Row = _dtChild.Select("MenuParentId = " + _IDMenu, "MenuOrder desc");
                //            if (_Row.Length > 0)
                //            {
                //                _Stack.Rows[_Stack.Rows.Count - 1]["LinkRun"] = "co";
                //                tmp += "<li class='current'>";
                //                tmp += "<a href='#'> <b>" + _Stack.Rows[_Stack.Rows.Count - 1]["MenuName"].ToString() + "</b></a>";
                //                tmp += "<ul> ";
                //                for (int k = 0; k < _Row.Length; k++)
                //                {
                //                    _Stack.Rows.Add(_Row[k].ItemArray);
                //                }  
                //            }
                //            else
                //            {
                //                tmp += "<li><a href='" + _Stack.Rows[_Stack.Rows.Count - 1]["LinkRun"].ToString() + "'>" + _Stack.Rows[_Stack.Rows.Count - 1]["MenuName"].ToString() + " </a></li>";
                //                _Stack.Rows[_Stack.Rows.Count - 1].Delete();
                //                _Stack.AcceptChanges();
                //            }
                //        }

                //        //     tmp += "</ul> ";
                //        //tmp += "</li>";

                //    }
                //}

                tmp += "  </Items>  </telerik:RadMenu>   </td> </tr>  </table>";



                return tmp;
            }
            catch (Exception ex)
            {
                return "";
                //throw ex;
            }


        }
        public static DataTable GetMainMenu(int UserID)
        {
            var oCommand = new SqlCommand("Menu_GetMenuByID");
            oCommand.CommandType = CommandType.StoredProcedure;
            _Parameter = null;
            CreateParameter("@UserID", UserID);

            DataTable dt = _DAO.getDataTable(oCommand, _Parameter);

            if (dt != null)
                return dt;
            else
                return null;
        }
        public static DataTable GetMainMenu_Module(int UserID)
        {
            var oCommand = new SqlCommand("SP_Menu_GetModule");
            oCommand.CommandType = CommandType.StoredProcedure;
            _Parameter = null;
            CreateParameter("@UserID", UserID);

            DataTable dt = _DAO.getDataTable(oCommand, _Parameter);

            if (dt != null)
                return dt;
            else
                return null;
        }
        public static DataTable GetMainMenu_Child(int IdMenu, int UserID)
        {
            var oCommand = new SqlCommand("Menu_GetChild_By_User");
            oCommand.CommandType = CommandType.StoredProcedure;

            _Parameter = null;
            CreateParameter("@Parent", IdMenu);
            CreateParameter("@UserID", UserID);

            DataTable dt = _DAO.getDataTable(oCommand, _Parameter);
            if (dt != null)
                return dt;
            else
                return null;
        }
        private static void CreateParameter(string sParaName, object sParaValue)
        {
            if (_Parameter == null)
                Array.Resize(ref _Parameter, 1);
            else
                Array.Resize(ref _Parameter, _Parameter.Length + 1);
            _Parameter[_Parameter.Length - 1] = new SqlParameter(sParaName, sParaValue);
        }

        public static DataSet GetInfor_Page_Main(int userId)
        {
            _Parameter = null;
            try
            {

                CreateParameter("@UserID", userId);
                return _DAO.getDataSetSP("SP_Infomation_Page_Main", _Parameter);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static DataSet SelectByFolder(int UserId, int FolderId, int CurrPage, int RecordPerPage)
        {
            _Parameter = null;
            try
            {
                CreateParameter("@UserId", UserId);
                CreateParameter("@FolderId", FolderId);
                CreateParameter("@CurrPage", CurrPage);
                CreateParameter("@RecordPerPage", RecordPerPage);
                return _DAO.getDataSetSP("sp_Message_SelectByFolder_MainPage", _Parameter);
            }
            catch (Exception ex)
            {
                return null;
            }
           
        }
    }
}
