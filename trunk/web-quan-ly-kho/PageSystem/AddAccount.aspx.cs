using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Nhansu_Lib.DB_Class;
namespace QLCV.PageSystem
{
    public partial class AddAccount : System.Web.UI.Page
    {
        private cls_UserAccount _ObjAcc = new cls_UserAccount();
        private cls_Groups objGroup = new cls_Groups();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillCombo();
                optEmployee.Checked = true;
                LoadCombo_Employee();
                LoadCombo_Nhom();
                string UserID = Request["UserID"] == null ? "" :Request["UserID"].ToString();
                if (UserID.Length > 0)
                    ObjToPage(UserID);

            }
            
        }
        private void FillCombo()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(short));
            dt.Columns.Add("Name");
            dt.Rows.Add(dt.NewRow());
            dt.Rows[dt.Rows.Count - 1]["ID"] = 1;
            dt.Rows[dt.Rows.Count - 1]["Name"] = "Hoạt động";
            dt.Rows.Add(dt.NewRow());
            dt.Rows[dt.Rows.Count - 1]["ID"] = 2;
            dt.Rows[dt.Rows.Count - 1]["Name"] = "Tạm khóa";
            dt.Rows.Add(dt.NewRow());
            dt.Rows[dt.Rows.Count - 1]["ID"] = 3;
            dt.Rows[dt.Rows.Count - 1]["Name"] = "Khóa vĩnh viễn";
            cboState.DataSource = dt;
            cboState.DataBind();
        }
        private void LoadCombo_Employee()
        {
            DataTable _dt = _ObjAcc.Select_Employee(optEmployee.Checked);
            cboEmployee.DataSource = _dt;
            cboEmployee.DataBind();
        
        }
        private void LoadCombo_Nhom()
        {
            DataTable _dt = objGroup.SelectByCompanyId();
            cboNhom.DataSource = _dt;
            cboNhom.DataBind();

        }
       private bool ValidForm()
           {
               if (txtUserName.Text.Trim().Equals(""))
               {
                   ScriptManager.RegisterStartupScript(Page, Page.GetType(),
                        "MyScript", "alert('Bạn phải nhập tên đăng nhập');", true);
                   txtUserName.Focus();
                   return false;
               }
               string UserID = Request["UserID"] == null ? "" : Request["UserID"].ToString();
               if (UserID.Length<=0 && !_ObjAcc.CheckExistUser(txtUserName.Text.Trim()))
               {
                   ScriptManager.RegisterStartupScript(Page, Page.GetType(),
                        "MyScript", "alert('Tên đăng nhập đã tồn tại');", true);
                   txtUserName.Focus();
                   return false;
               }
               if (txtPass1.Text.Trim().Equals(""))
               {
                   ScriptManager.RegisterStartupScript(Page, Page.GetType(),
                        "MyScript", "alert('Bạn phải nhập mật khẩu');", true);
                   txtPass1.Focus();
                   return false;
               }
               if (!txtPass1.Text.Equals(txtPass2.Text))
               {
                   ScriptManager.RegisterStartupScript(Page, Page.GetType(),
                        "MyScript", "alert('Hai mật khẩu không giống nhau');", true);
                   txtPass2.Focus();
                   return false;
               }
             
               return true;

           }
        private void PageToObj()
        {
            _ObjAcc.User_Id = Request["UserID"] == null ? "" : Request["UserID"].ToString();
            _ObjAcc.Username = txtUserName.Text.Trim();
            _ObjAcc.Password = txtPass1.Text;
            if (optEmployee.Checked)
                _ObjAcc.NhanVien_Id = cboEmployee.SelectedValue;
            else
                _ObjAcc.DoiTac_NhanVien_Id = cboEmployee.SelectedValue;
            _ObjAcc.IsEnabled = Convert.ToByte(cboState.SelectedValue);
            _ObjAcc.Group_Id = Convert.ToInt32(cboNhom.SelectedValue);
            
        }
        private void ObjToPage( string sUserID )
        {
            _ObjAcc.Get_Record(sUserID);
            txtUserName.Text = _ObjAcc.Username;
            txtPass1.Text = _ObjAcc.Password;
            txtPass2.Text = _ObjAcc.Password;
            if (_ObjAcc.NhanVien_Id.Length > 0)
            {
                optEmployee.Checked = true;
                cboEmployee.SelectedValue = _ObjAcc.NhanVien_Id;
            }
            else if (_ObjAcc.DoiTac_NhanVien_Id.Length > 0)
            {
                optPartner.Checked = true;
                cboEmployee.SelectedValue = _ObjAcc.DoiTac_NhanVien_Id;
            }
            
           
            cboState.SelectedValue = _ObjAcc.IsEnabled.ToString();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidForm())
                    return;
                PageToObj();
                if (_ObjAcc.User_Id.Length <= 0)
                {
                    if (_ObjAcc.Insert()> 0)
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind();", true);
                    }
                }
                else
                {
                    if (_ObjAcc.Update() > 0)
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CloseAndRebind();", true);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void optEmployee_CheckedChanged(object sender, EventArgs e)
        {
            LoadCombo_Employee();
        }

        protected void optPartner_CheckedChanged(object sender, EventArgs e)
        {
            LoadCombo_Employee();

        }
    }
}
