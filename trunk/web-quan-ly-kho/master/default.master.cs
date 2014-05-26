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

using Telerik.Web.UI;
//using Zooz.Libs;

using QLCV.code.common;
public partial class master_default : System.Web.UI.MasterPage
{
    //public string UrlImages = Constant.IMG_PATH;
    public string UrlJs = Constant.JS_PATH;
    //public string Url__ = Constant.PRJ_ROOT;
    public DataTable _dtMenu;
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["user"] == null)
        //    Response.Redirect(Constant.PRJ_ROOT + "/Login.aspx");
        int iUserID = Session["UserId"] == null ? 0 : Int32.Parse(Session["UserId"].ToString());
        FillMenu(iUserID);
        lblName.Text = Session["FullName"] == null ? "" : Session["FullName"].ToString();
    }
    private void FillMenu(int userId)
    {
        _dtMenu = clsMenu.GetMainMenu_Module(userId);
        //DataTable dtMain =  clsMenu.GetMainMenu(userId);

        //DataTable _dtChild;
        //DataTable _Stack = new DataTable();
        //string _IDMenu;
        //RadMenu1.Items.Clear();
        //foreach (DataRow dr in dtMain.Rows)
        //{
        //    _dtChild = clsMenu.GetMainMenu_Child(Convert.ToInt32(dr["MenuId"]), userId);
        //    string sUrl = "";
        //    if (dr["LinkRun"].ToString().Length > 0)
        //        sUrl = dr["LinkRun"].ToString() + "?M_Id=" + dr["MenuId"].ToString();
        //    else
        //        sUrl = "#";
        //    RadMenuItem Parent = new RadMenuItem(dr["MenuName"].ToString(), sUrl);
        //    Parent.Value = dr["MenuId"].ToString();
        //    Parent.ForeColor = System.Drawing.Color.White;
        //    Parent.Font.Bold = true;
        //    RadMenu1.Items.Add(Parent);
        //    AddMenu(Parent, _dtChild, "MenuId", "MenuName", "MenuParentId", "LinkRun");

        //}

    }
    public void AddMenu(RadMenuItem parentNode, DataTable _dtTree, string FeildValue, string FeildText, string FeildParent, string FeildUrl)
    {
        try
        {
            DataRow[] _Row = null;
            int k = 0;
            RadMenuItem currentNode = default(RadMenuItem);

            _Row = _dtTree.Select(FeildParent + " = " + parentNode.Value, "MenuOrder desc");

            if (_Row.Length > 0)
            {
                for (k = 0; k <= _Row.Length - 1; k++)
                {
                    //If _Row(k)("Ten_Hien_Thi").ToString <> "Phân quyền người dùng" Then
                    if (_Row[k]["Visible"].ToString() == "1")
                    {
                        string sUrl = "";
                        if (_Row[k][FeildUrl].ToString().Length > 0)
                            sUrl = _Row[k][FeildUrl].ToString() + "?M_Id=" + _Row[k][FeildValue].ToString();
                        else
                            sUrl = "#";
                        RadMenuItem Node = new RadMenuItem(_Row[k][FeildText].ToString(), sUrl);
                        Node.Value = _Row[k][FeildValue].ToString();
                        parentNode.Items.Add(Node);
                        currentNode = Node;

                        AddMenu(currentNode, _dtTree, FeildValue, FeildText, FeildParent, FeildUrl);
                    }
                    //End If
                }
            }
            //nd _Row(k)("Ten_Hien_Thi").ToString <> "Danh mục tuyến thu" And _Row(k)("Ten_Hien_Thi").ToString <> "Kiểm tra và chốt số liệu" And _Row(k)("Ten_Hien_Thi").ToString <> "Danh mục cụm địa chỉ"
        }
        catch (Exception ex)
        {

        }
    }

    protected void LinkLogout_Click(object sender, EventArgs e)
    {
        SaveLog_Client_LogOut();
        System.Web.Security.FormsAuthentication.SignOut();
        Session.Abandon();
        Response.Redirect("~/login.aspx");
    }
    private void SaveLog_Client_LogOut()
    {
        try
        {
            cls_Log_Login objLogIn = new cls_Log_Login();
            objLogIn.EmployeeID = Session["UserId"] == null ? 0 : Convert.ToInt32(Session["UserId"].ToString());
            objLogIn.Date_Logout = DateTime.Now;

            int iResult = objLogIn.Update();
        }
        catch (Exception ex)
        {
        }

    }
}
