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
using Telerik.Web.UI;
using Nhansu_Lib.DB_Class;
namespace QLCV.PageSystem
{
    public partial class UserToGroup : System.Web.UI.Page
    {
      private  cls_Group_Employees objGr= new cls_Group_Employees();
      public DataTable _dtFunction;
        protected void Page_Load(object sender, EventArgs e)
        {
            // ---- KIEM TRA LOGIN -----
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/login.aspx");
            }
            // ---- END KT LOGIN -------

            if (!IsPostBack)
            {
                Fill_Combo_Company();
                LoadDataGroupUser();
                LoadDataToDepartment();
            }
            _dtFunction = (new QLCV.code.common.clsSystem()).Get_System_Menu(Session["UserId"].ToString());
        }
        #region "U_Function"
        private void Fill_Combo_Company()
        {
            DataTable _dt = (new cls_Catalog_Company()).Select_Permit(Session["UserId"].ToString());
            cboCompany.DataSource = _dt;
            cboCompany.DataBind();
        }
        private void LoadDataGroupUser()
        {
            cls_Groups objGroup = new cls_Groups();
            DataTable _dt = objGroup.GetData( cboCompany.SelectedValue);
            gridGroup.DataSource = _dt;
            gridGroup.DataBind();         
        }
        private void LoadDataToDepartment()
        {

            if (trDepartment.Nodes.Count > 0)
                trDepartment.Nodes.Clear();
            cls_DepartmentInformation objDe = new cls_DepartmentInformation();
           
            DataTable _dtDepartment = objDe.GetList( cboCompany.SelectedValue);
            ShowDSTreeview(trDepartment, _dtDepartment, 0, "Id", "Name", "Belong_DepartmentId", "Toàn bộ phòng ban");
            trDepartment.ExpandAllNodes();

        }
        private void ShowDSTreeview(RadTreeView Tree, DataTable _dtTree, int IDRoot, string FeildValue, string FeildText, string FeildParent, string TextRoot)
        {
            RadTreeNode Node = new RadTreeNode(TextRoot, IDRoot.ToString());
            Tree.Nodes.Add(Node);
            AddNodeToTree(Node, _dtTree, FeildValue, FeildText, FeildParent);

        }
        private void AddNodeToTree(RadTreeNode parentNode, DataTable _dtTree, string FeildValue, string FeildText, string FeildParent)
        {
            try
            {
                DataRow[] _Row = null;
                int k = 0;
                RadTreeNode currentNode = default(RadTreeNode);

                _Row = _dtTree.Select(FeildParent + " = " + parentNode.Value.ToString(), FeildValue + " ASC");

                if (_Row.Length > 0)
                {
                    for (k = 0; k <= _Row.Length - 1; k++)
                    {
                        //If _Row(k)("Ten_Hien_Thi").ToString <> "Phân quyền người dùng" Then
                        RadTreeNode Node = new RadTreeNode(_Row[k][FeildText].ToString(), _Row[k][FeildValue].ToString());
                        parentNode.Nodes.Add(Node);
                        currentNode = Node;

                        currentNode.Checked = false;
                        AddNodeToTree(currentNode, _dtTree, FeildValue, FeildText, FeildParent);
                        //End If
                    }
                }
                //nd _Row(k)("Ten_Hien_Thi").ToString <> "Danh mục tuyến thu" And _Row(k)("Ten_Hien_Thi").ToString <> "Kiểm tra và chốt số liệu" And _Row(k)("Ten_Hien_Thi").ToString <> "Danh mục cụm địa chỉ"
            }
            catch (Exception ex)
            {

            }
        }
        private void LoadEmployeeFollowDepart(int DepartID,short EmAll)
        {

            DataTable _dtEm = objGr.GetEmployeeFollowDepart(DepartID, EmAll);
            gridEmploy.DataSource = _dtEm;
            gridEmploy.DataBind();

        }
        private void CheckEmployFollowGroup(string GroupID)
        {
            DataTable _dt = objGr.GetListEmployeeFollowGroup(GroupID);
            for (int i = 0; i < gridEmploy.Items.Count; i++)
            {
                bool OK = false;
                for (int k = 0; k < _dt.Rows.Count; k++)
                    if (gridEmploy.Items[i].GetDataKeyValue("ID").ToString() == _dt.Rows[k]["EmployeeId"].ToString())
                    {
                        gridEmploy.Items[i].Selected = true;
                        OK = true;
                        break;
                    }
                if (OK == false)
                    gridEmploy.Items[i].Selected = false;
            }
        }
        private void CheckGroupFollowEmploy(string EmployID)
        {
            DataTable _dt = objGr.GetListGroupFollowEmployee(EmployID);

            for (int i = 0; i < gridGroup.Items.Count; i++)
            {
                bool OK = false;
                for (int k = 0; k < _dt.Rows.Count; k++)
                    if (gridGroup.Items[i].GetDataKeyValue("GroupID").ToString() == _dt.Rows[k]["GroupID"].ToString())
                    {
                        gridGroup.Items[i].Selected = true;
                        OK = true;
                        break;
                    }
                if(OK == false)
                    gridGroup.Items[i].Selected = false;
            }
        }
        private DataTable GetAllGroup()
        {
            DataTable _dt = new DataTable();
            _dt.Columns.Add("GroupID", typeof(int));
            if (gridGroup.SelectedItems.Count > 0)
            {
                for (int i = 0; i < gridGroup.SelectedItems.Count; i++)
                {
                    DataRow dr = _dt.NewRow();
                    dr["GroupID"] = ((GridDataItem)gridGroup.SelectedItems[i]).GetDataKeyValue("GroupID");
                    _dt.Rows.Add(dr);
                }
                return _dt;
            }
            else
                return null;
        }

        #endregion

        protected void trDepartment_NodeClick(object sender, RadTreeNodeEventArgs e)
        {
            if (trDepartment.SelectedNodes.Count == 0) return;
            LoadEmployeeFollowDepart(Convert.ToInt32(trDepartment.SelectedValue),Convert.ToInt16(chkNhanVien.Checked.GetHashCode()));

        }

        protected void gridEmploy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gridEmploy.SelectedValue == null) return;
            CheckGroupFollowEmploy(gridEmploy.SelectedValue.ToString());
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (gridEmploy.SelectedValue == null)
            {
            
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                "MyScript", "alert('Bạn phải chọn nhân viên!');", true);
                return;
            }
            if (objGr.Delete(Convert.ToInt32(gridEmploy.SelectedValue)) < 0)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                "MyScript", "alert('Có lỗi khi xóa và cập nhật dữ liệu!');", true);
          
                return;
            }
            DataTable _dt = GetAllGroup();
            if (_dt != null)
            {
                if (objGr.UpdateAllRow(gridEmploy.SelectedValue.ToString(), _dt) < 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
              "MyScript", "alert('Có lỗi khi cập nhật dữ liệu!');", true);
                    return;
                }
            }
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
        "MyScript", "alert('Cập nhật dữ liệu thành công!');", true);
         

        }
        protected void cboCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDataGroupUser();
            LoadDataToDepartment();
        }
        //protected void gridGroup_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (gridGroup.SelectedValue == null) return;
        //    CheckEmployFollowGroup(gridGroup.SelectedValue.ToString());
        //}
    }
}
