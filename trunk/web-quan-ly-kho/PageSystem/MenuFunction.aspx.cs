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
using Telerik.Web.UI;
using QLCV.code.common;
namespace QLCV.PageSystem
{
    public partial class MenuFunction : System.Web.UI.Page
    {
        private static DataTable dtMenu;
        private static int IDMenu;
        private  cls_Menus objMenu = new cls_Menus();
        protected void Page_Load(object sender, EventArgs e)
        {
            // ---- KIEM TRA LOGIN -----
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/login.aspx");
            }
            // ---- END KT LOGIN -------

            InjectLoadEvent();
            this.uscMsgBox.MsgBoxAnswered += MessageAnswered;
         
            if (IsPostBack) return;
            LoadData();
            ControlForm(0);
          
        }
        private void LoadData()
        {
            if (trMenu.Nodes.Count > 0)
                trMenu.Nodes.Clear();
            dtMenu = objMenu.GetMenu();
            ShowDSTreeview(0);
            trMenu.ExpandAll();
            if (trMenu.Nodes.Count > 0)
            {
                trMenu.Focus();
                trMenu.Nodes[0].Selected = true;
                GetInforItem(trMenu.SelectedNode.Value.ToString());
            }
            LoadToCombo();
        }
        private bool CheckValid()
        {
          
            if (txtFunctionName.Text.ToString() == "")
            {

                uscMsgBox.AddMessage("Bạn phải nhập tên menu!", YaBu.MessageBox.uscMsgBox.enmMessageType.Error, "txtOrder");
           
                return false;
            }
            return true;

        }
        private void PageToObject()
        {
            objMenu.MenuName = txtFunctionName.Text;
            objMenu.LinkRun = txtUrl.Text;
            try
            {
                objMenu.MenuOrder = Convert.ToInt16(txtOrder.Text);
            }
                catch
            {
                objMenu.MenuOrder = 0;
                }
            try
            {
                objMenu.MenuParentId = Convert.ToInt32(cboParent.SelectedValue);
            }
            catch
            {
                objMenu.MenuParentId = 1;
            }
            objMenu.CreatedTime = DateTime.Now;
            objMenu.MenuId = IDMenu;
        }
        private void LoadToCombo()
        {
            DataTable _dt = objMenu.GetMenuParent();
            DataRow dr = _dt.NewRow();
            dr["MenuId"] = 0;
            dr["MenuName"] = "  ";
            _dt.Rows.InsertAt(dr, 0);
            cboParent.DataTextField = "MenuName";
            cboParent.DataValueField = "MenuId";
            cboParent.DataSource = _dt;
            cboParent.DataBind();
        }
        private void ControlForm(int Cate)
        {
            if (Cate == 0)
            {
                trMenu.Enabled = true;
                txtFunctionName.ReadOnly = true;
                txtUrl.ReadOnly = true;
                txtOrder.ReadOnly = true;
                btnAdd.Text = "Thêm";
                btnEdit.Text = "Sửa";
                btnDelete.Visible = true;

            }
            else
            {
                trMenu.Enabled = false;
                txtFunctionName.ReadOnly = false;
                txtUrl.ReadOnly = false;
                txtOrder.ReadOnly = false;
                btnAdd.Text = "Hủy";
                btnEdit.Text = "Ghi";
                btnEdit.CommandName = Cate.ToString();
                btnDelete.Visible = false;
                if (Cate == 2)
                {
                    txtFunctionName.Text = "";
                    txtOrder.Text = "";
                    txtUrl.Text = "";
                    cboParent.SelectedIndex = 0;
                  
                }
                txtFunctionName.Focus();
            }
                
        }
        private void InjectLoadEvent()
        {
            string script =
              "function LoadEvent()" +
              "{{" +
              " try" +
              " {{" +
              "   var elem = document.getElementById('{0}_SelectedNode');" +
              "   if(elem != null )" +
              "   {{" +
              "     var node = document.getElementById(elem.value);" +
              "     if(node != null)" +
              "     {{" +
              "       node.scrollIntoView(true);" +
              "       {1}.scrollLeft = 0;" +
              "     }}" +
              "   }}" +
              " }}" +
              " catch(oException)" +
              " {{}}" +
              "}}";

            Page.ClientScript.RegisterClientScriptBlock(this.GetType(),
              "LoadEvent",
              String.Format(script, trMenu.ClientID, Panel1.ClientID),
              true);
        }
        private DataTable LayDSTKCon(int IDTra)
        {
            DataTable dt = dtMenu.Clone();
            DataRow[] _Row = dtMenu.Select("MenuParentId = " + IDTra.ToString());
            if (_Row.Length > 0)
            {
                for (int i = 0; i <= _Row.Length - 1; i++)
                {
                    dt.ImportRow(_Row[i]);
                }
            }
            return dt;
        }

        private void ShowDSTreeview(int IDMenu)
        {
            DataTable dtBCTra = this.LayDSTKCon(IDMenu);
            for (int t = 0; t <= dtBCTra.Rows.Count - 1; t++)
            {
                ShowDSTreeview_DFS(dtBCTra.Rows[t]);
            }
        }
        private void ShowDSTreeview_DFS(DataRow DrTra)
        {
            try
            {
                TreeNode ItemTRa = new TreeNode();
                ItemTRa.Text = DrTra["MenuId"].ToString() + " - " + DrTra["MenuName"].ToString();
                ItemTRa.Value = DrTra["MenuId"].ToString();

                ItemTRa.ImageUrl = "../images/drop-add.gif";
              //  ItemTRa.NavigateUrl = "javascript:void(0)";
                this.trMenu.Nodes.Add(ItemTRa);
              
                DataTable dtQue = dtMenu.Clone();
                dtQue.ImportRow(DrTra);

                TreeNode NodeTra = default(TreeNode);

                while (dtQue.Rows.Count > 0)
                {
                    NodeTra = SearchNode(dtQue.Rows[0]["MenuId"].ToString());
                    DataTable dtBC = this.LayDSTKCon(Convert.ToInt32(dtQue.Rows[0]["MenuId"]));
                    int k = 0;
                    if (dtBC.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dtBC.Rows.Count - 1; i++)
                        {
                            TreeNode Item = new TreeNode();
                            Item.Text = dtBC.Rows[i]["MenuId"].ToString() + " - " + dtBC.Rows[i]["MenuName"].ToString();
                            Item.Value = dtBC.Rows[i]["MenuId"].ToString();
                           // Item.SelectedImageIndex = 1;
                         //   Item.NavigateUrl = "javascript:void(0)";
                           // Item.SelectAction = TreeNodeSelectAction.None;
                            Item.ImageUrl = "../images/drop-yes.gif";
                            if ((NodeTra != null))
                            {
                                NodeTra.ChildNodes.Add(Item);
                            }
                            DataRow dr = dtQue.NewRow();
                            dr.ItemArray = dtBC.Rows[i].ItemArray;
                            if (dtQue.Rows[0]["MenuId"].ToString().Trim() != dtBC.Rows[i]["MenuId"].ToString().Trim())
                            {
                                dtQue.Rows.Add(dr);
                            }
                        }

                    }
                    dtQue.Rows[0].Delete();
                    dtQue.AcceptChanges();

                }
            }
            catch (Exception ex)
            {
                //Interaction.MsgBox("coLoi");
            }
        }

        private TreeNode SearchNode(string IDTK)
        {
            TreeNode ReNode = default(TreeNode);
            for (int j = 0; j <= this.trMenu.Nodes.Count - 1; j++)
            {
                ReNode = SearchNodeDQ(this.trMenu.Nodes[j], IDTK);
                if ((ReNode != null))
                    break; // TODO: might not be correct. Was : Exit For
            }
            return ReNode;
        }
        private TreeNode SearchNodeDQ(TreeNode NodeTra, string IDTK)
        {
            if (NodeTra == null)
            {
                return null;
            }
            else
            {
                if (NodeTra.Value.ToString() == IDTK)
                    return NodeTra;
                if (NodeTra.ChildNodes.Count == 0)
                    return null;
                for (int i = 0; i <= NodeTra.ChildNodes.Count - 1; i++)
                {
                    if (NodeTra.ChildNodes[i].Value != IDTK)
                    {
                        SearchNodeDQ(NodeTra.ChildNodes[i], IDTK);
                    }
                    else
                    {
                        return NodeTra.ChildNodes[i];
                    }
                }
                return null;
            }
        }

       
        private void GetInforItem(string MenuID)
        {
            if (MenuID == "") return;
            DataRow[] _Row = dtMenu.Select("MenuId = " + MenuID);
            if (_Row.Length > 0)
            {
                this.txtFunctionName.Text = _Row[0]["MenuName"].ToString();
                txtUrl.Text = _Row[0]["LinkRun"].ToString();
                txtOrder.Text = _Row[0]["MenuOrder"].ToString();
                for (int i = 0; i < cboParent.Items.Count; i++)
                    if (cboParent.Items[i].Value == _Row[0]["MenuParentId"].ToString())
                    {
                        cboParent.SelectedIndex =i;
                        // cboParent.DataBind();
                        break;
                    }
                IDMenu = Convert.ToInt32(trMenu.SelectedValue);
              txtFunctionName.Focus();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "Thêm")
                ControlForm(2);
            else
            {
                ControlForm(0);
                trMenu_SelectedNodeChanged(sender, e);
            }
        }

        protected void trMenu_SelectedNodeChanged(object sender, EventArgs e)
        {
            if (trMenu.SelectedValue == null) return;
           GetInforItem(trMenu.SelectedValue);
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
            if (btnEdit.Text == "Sửa")
            {
                ControlForm(1);
            }
            else
            {
                if (CheckValid() == false) return;
                PageToObject();
                if (btnEdit.CommandName == "2")
                {
                    if (objMenu.Insert() > 0)
                    {
                        uscMsgBox.AddMessage("Thêm mới thành công menu!", YaBu.MessageBox.uscMsgBox.enmMessageType.Info);
                        ControlForm(0);
                        LoadData();
                    }
                    else
                    {
                        uscMsgBox.AddMessage("Có lỗi khi thêm mới Menu!", YaBu.MessageBox.uscMsgBox.enmMessageType.Error);

                    }
                }
                else
                {
                   
                    if (objMenu.Update() > 0)
                    {
                        uscMsgBox.AddMessage("Sửa thành công menu!", YaBu.MessageBox.uscMsgBox.enmMessageType.Info);
                        ControlForm(0);
                        LoadData();
                    }
                    else
                    {
                        uscMsgBox.AddMessage("Có lỗi khi sửa mới Menu!", YaBu.MessageBox.uscMsgBox.enmMessageType.Error);

                    }
                }
               }
             }
                catch(Exception ex)
            {
                uscMsgBox.AddMessage("Có lỗi khi thêm, sửa menu!" + ex.Message, YaBu.MessageBox.uscMsgBox.enmMessageType.Error);
                }

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (trMenu.SelectedNode == null) return;
                if (trMenu.SelectedNode.ChildNodes.Count > 0)
                {
                    uscMsgBox.AddMessage("Bạn phải xóa chức năng con trước!", YaBu.MessageBox.uscMsgBox.enmMessageType.Info);
                    return;
                }
               
                uscMsgBox.AddMessage("Bạn có muốn xóa chức năng:" + txtFunctionName.Text + " không?", YaBu.MessageBox.uscMsgBox.enmMessageType.Attention, true, true, "Thông báo");
            }
            catch (Exception ex)
            {
                uscMsgBox.AddMessage("Có lỗi tại chức năng xóa dữ liêu: " + ex.Message, YaBu.MessageBox.uscMsgBox.enmMessageType.Error);
            }
        }

        public  void MessageAnswered(object sender, YaBu.MessageBox.uscMsgBox.MsgBoxEventArgs e)
        {
            if (e.Answer == YaBu.MessageBox.uscMsgBox.enmAnswer.OK)
            {
                objMenu.MenuId = Convert.ToInt32(trMenu.SelectedValue);
                if (objMenu.Delete() > 0)
                {
                    LoadData();
                }
                else
                {
                    uscMsgBox.AddMessage("Có lỗi khi xóa dữ liệu", YaBu.MessageBox.uscMsgBox.enmMessageType.Error);
   
                }
            }
           
        }
      
    }
}
