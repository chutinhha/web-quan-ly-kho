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
namespace QLCV.PageSystem
{
    public partial class Authority_Group : System.Web.UI.Page
    {
        private static DataTable dtMenu, dtDepartment, dtMenuGroup;
        private static bool bProcessing = false;
       private cls_Group_Menus ObjGroupMenu = new cls_Group_Menus();
        protected void Page_Load(object sender, EventArgs e)
        {
           // this.uscMsgBox.MsgBoxAnswered += MessageAnswered;
            // ---- KIEM TRA LOGIN -----
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/login.aspx");
            }
            // ---- END KT LOGIN -------

            if (!IsPostBack)
            {
                LoadDataToFunction();
                LoadDataToDepartment();
                LoadDataGroupUser();
            
            }

        }

        #region "U_Function"
        private void LoadDataGroupUser()
        {
            cls_Groups objGroup = new cls_Groups();
            DataTable _dt = objGroup.GetData();
            gridGroup.DataSource = _dt;
            gridGroup.DataBind();
            if (gridGroup.Items.Count > 0)
            {
                gridGroup.Items[0].Selected = true;
                CheckAuThority(Convert.ToInt32(gridGroup.SelectedValue));
                trFunction.ExpandAllNodes();
                trFunction.Nodes[0].Selected = true;
                CheckDepartment(gridGroup.SelectedValue.ToString(), trFunction.SelectedValue.ToString());
            }
        }
        private void LoadDataToFunction()
        {
              if (trFunction.Nodes.Count > 0)
                trFunction.Nodes.Clear();
            cls_Menus objMenu = new cls_Menus();
            dtMenu = objMenu.GetMenu();
             
            ShowDSTreeview(trFunction,dtMenu,0,"MenuId","MenuName","MenuParentId","Toàn bộ chức năng");
            trFunction.ExpandAllNodes();
        }
        private void LoadDataToDepartment()
        {

            if (trDepartment.Nodes.Count > 0)
                trDepartment.Nodes.Clear();
            cls_DepartmentInformation objDe = new cls_DepartmentInformation();
            dtDepartment = objDe.GetList();
            ShowDSTreeview(trDepartment, dtDepartment, 0, "Id", "Name", "Belong_DepartmentId","Toàn bộ phòng ban");
            trDepartment.ExpandAllNodes();
        }
        private void ShowDSTreeview(RadTreeView Tree,DataTable _dtTree,int IDRoot, string FeildValue, string FeildText,string FeildParent,string TextRoot)
        {
            RadTreeNode Node = new RadTreeNode(TextRoot, IDRoot.ToString());
            Tree.Nodes.Add(Node);
            AddNodeToTree(Node, _dtTree, FeildValue, FeildText, FeildParent);

        }
        //private void ShowDSTreeview_DFS(RadTreeView Tree,DataTable _dtTree, DataRow DrTra, string FeildValue, string FeildText,string FeildParent)
        //{
        //    try
        //    {
        //        RadTreeNode ItemTRa = new RadTreeNode();

        //        ItemTRa.Text = DrTra[FeildText].ToString();
        //        ItemTRa.Value = DrTra[FeildValue].ToString();

        //        ItemTRa.ImageUrl = "../images/drop-add.gif";
        //        //  ItemTRa.NavigateUrl = "javascript:void(0)";
        //        Tree.Nodes.Add(ItemTRa);

        //        DataTable dtQue = _dtTree.Clone();
        //        dtQue.ImportRow(DrTra);

        //        RadTreeNode NodeTra = default(RadTreeNode);

        //        while (dtQue.Rows.Count > 0)
        //        {
        //            NodeTra = SearchNode(_dtTree, dtQue.Rows[0][FeildValue].ToString(), Tree);
        //            DataTable dtBC = this.LayDSTKCon(_dtTree, Convert.ToInt32(dtQue.Rows[0][FeildValue]), FeildParent);
        //            int k = 0;
        //            if (dtBC.Rows.Count > 0)
        //            {
        //                for (int i = 0; i <= dtBC.Rows.Count - 1; i++)
        //                {
        //                    RadTreeNode Item = new RadTreeNode();
        //                    Item.Text = dtBC.Rows[i][FeildText].ToString();
        //                    Item.Value = dtBC.Rows[i][FeildValue].ToString();
        //                    // Item.SelectedImageIndex = 1;
        //                    //   Item.NavigateUrl = "javascript:void(0)";
        //                    // Item.SelectAction = TreeNodeSelectAction.None;
        //                    Item.ImageUrl = "../images/drop-yes.gif";
        //                    if ((NodeTra != null))
        //                    {
        //                        NodeTra.Nodes.Add(Item);
        //                    }
        //                    DataRow dr = dtQue.NewRow();
        //                    dr.ItemArray = dtBC.Rows[i].ItemArray;
        //                    if (dtQue.Rows[0][FeildValue].ToString().Trim() != dtBC.Rows[i][FeildValue].ToString().Trim())
        //                    {
        //                        dtQue.Rows.Add(dr);
        //                    }
        //                }

        //            }
        //            dtQue.Rows[0].Delete();
        //            dtQue.AcceptChanges();

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //Interaction.MsgBox("coLoi");
        //    }
        //}

        //private RadTreeNode SearchNode( string IDTK, RadTreeView Tree)
        //{
        //    RadTreeNode ReNode = default(RadTreeNode);
        //    for (int j = 0; j <= Tree.Nodes.Count - 1; j++)
        //    {
        //        ReNode = SearchNodeDQ(Tree.Nodes[j], IDTK);
        //        if ((ReNode != null))
        //            break; // TODO: might not be correct. Was : Exit For
        //    }
        //    return ReNode;
        //}
        //private RadTreeNode SearchNodeDQ(RadTreeNode NodeTra, string IDTK)
        //{
        //    if (NodeTra == null)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        if (NodeTra.Value.ToString() == IDTK)
        //            return NodeTra;
        //        if (NodeTra.Nodes.Count == 0)
        //            return null;
        //        for (int i = 0; i <= NodeTra.Nodes.Count - 1; i++)
        //        {
        //            if (NodeTra.Nodes[i].Value != IDTK)
        //            {
        //                SearchNodeDQ(NodeTra.Nodes[i], IDTK);
        //            }
        //            else
        //            {
        //                return NodeTra.Nodes[i];
        //            }
        //        }
        //        return null;
        //    }

        //}
        //private DataTable LayDSTKCon(DataTable _dtTree, int IDTra, string FeildParent)
        //{
        //    DataTable dt = _dtTree.Clone();
        //    DataRow[] _Row = _dtTree.Select(FeildParent + " = " + IDTra.ToString());
        //    if (_Row.Length > 0)
        //    {
        //        for (int i = 0; i <= _Row.Length - 1; i++)
        //        {
        //            dt.ImportRow(_Row[i]);
        //        }
        //    }
        //    return dt;
        //}
        ////
private void DuyetDeCheckLaiChucNang(RadTreeNode currentNode)
{
	try {
		//RadTreeNode objTreeNode = default(RadTreeNode);
		RadTreeNode objTemp = default(RadTreeNode);
		bool checkAll = true;
        
		objTemp = currentNode.Nodes[0];
		if (objTemp == null) {
			return;
			// menu thi hanh
		} else {
			while ((objTemp != null)) {
				DuyetDeCheckLaiChucNang(objTemp);
				objTemp = objTemp.Next;
			}
			currentNode.CollapseChildNodes();
			// xet tat ca cac nut con
			foreach(RadTreeNode objTreeNode in currentNode.Nodes)
            {
				if (!objTreeNode.Checked) {
					checkAll = false;
					break; // TODO: might not be correct. Was : Exit For
				}
			}
			if (checkAll)
				currentNode.Checked = true;
		}
	} catch (Exception ex) {
		
	}
}
private void CheckAuThority(int IDGroup)
{
    try
    {
        //RadTreeNode objTreeNode = default(RadTreeNode);
        cls_Group_Menus objG = new cls_Group_Menus();
      dtMenuGroup  = objG.GetAuthority(IDGroup);
      RadTreeNode objTemp = trFunction.Nodes[0];
      objTemp.Checked = false;
      UnCheckNodeCon(objTemp);
      bool OK = false;
          CheckFunctionTree(objTemp, dtMenuGroup,ref OK);
          if (OK)
              trFunction.Nodes[0].Checked = true;
      
    }
    catch (Exception ex)
    {

    }
}
private void CheckDepartment(string IDGroup,string IDMenu)
{
    try
    {

        DataRow[] _Row = dtMenuGroup.Select("GroupId =" + IDGroup + " and MenuId = " + IDMenu);
        if (_Row.Length > 0)
        {
            string[] sArr = _Row[0]["Authorization"].ToString().Split(',');
            bool bA = false, bB = false, bC = false;
            for (int k = 0; k < sArr.Length; k++)
            {
                if (sArr[k] == "2") bA = true;
                if (sArr[k] == "3") bB = true;
                if (sArr[k] == "4") bC = true;
            }
            chkAdd.Checked = bA; chkEdit.Checked = bB; chkDelete.Checked = bC;
            RadTreeNode objTemp = default(RadTreeNode);
            bool OK = false;
            objTemp =trDepartment.Nodes[0];
            UnCheckNodeCon(objTemp);
                CheckDepartmentTree(objTemp, _Row[0]["Working_Department"].ToString().Split(','),ref OK);
                if (OK)
                    trDepartment.Nodes[0].Checked = true;

            
        }
        else
        {
            chkAdd.Checked = false; chkEdit.Checked = false; chkDelete.Checked = false;
            trDepartment.ExpandAllNodes();
            trDepartment.Nodes[0].Checked = false;
            UnCheckNodeCon(trDepartment.Nodes[0]);
        }
       
    }
    catch (Exception ex)
    {

    }
}
private void UpdateFunctionForGroup(int iIDGroup, RadTreeNode currentNode,DataTable _dtInfo)
{
    RadTreeNode objTemp = default(RadTreeNode);
	int j = 0;
	try {
		if ((currentNode == null))
			return;
        if (currentNode.Nodes.Count > 0)
            objTemp = currentNode.Nodes[0];
        else
            objTemp = null;
		//  currentNode la menu thi hanh
		if ((objTemp == null) & currentNode.Checked) {
			// If currentNode.Checked Then
            ObjGroupMenu.GroupId = iIDGroup;
            ObjGroupMenu.MenuId = Convert.ToInt32(currentNode.Value);
            DataRow[] _Row1 = _dtInfo.Select("GroupId = " + iIDGroup.ToString() + " and MenuId = " + currentNode.Value.ToString());
            if (_Row1.Length > 0)
            {
                ObjGroupMenu.Authorization = _Row1[0]["Authorization"].ToString();
                ObjGroupMenu.Working_Department = _Row1[0]["Working_Department"].ToString();
            }
            else
            {
                ObjGroupMenu.Authorization = "1";
                ObjGroupMenu.Working_Department = "";
            }
            ObjGroupMenu.CreatedTime = DateTime.Now;
       
          //  ObjGroupMenu.Insert();
		} else if (currentNode.Checked) {
            ObjGroupMenu.GroupId = iIDGroup;
            ObjGroupMenu.MenuId = Convert.ToInt32(currentNode.Value);
            DataRow[] _Row2 = _dtInfo.Select("GroupId = " + iIDGroup.ToString() + " and MenuId = " + currentNode.Value.ToString());
            if (_Row2.Length > 0)
            {
                ObjGroupMenu.Authorization = _Row2[0]["Authorization"].ToString();
                ObjGroupMenu.Working_Department = _Row2[0]["Working_Department"].ToString();
            }
            else
            {
                ObjGroupMenu.Authorization = "1";
                ObjGroupMenu.Working_Department = "";
            }

            ObjGroupMenu.CreatedTime = DateTime.Now;
           
         //   ObjGroupMenu.Insert();
			// Else
			while ((objTemp != null)) {
				j += 1;
                if (objTemp.Checked)
                {
                    ObjGroupMenu.GroupId = iIDGroup;
                    ObjGroupMenu.MenuId = Convert.ToInt32(objTemp.Value);
                    DataRow[] _Row3 = _dtInfo.Select("GroupId = " + iIDGroup.ToString() + " and MenuId = " + objTemp.Value.ToString());

                    if (_Row3.Length > 0)
                    {
                        ObjGroupMenu.Authorization = _Row3[0]["Authorization"].ToString();
                        ObjGroupMenu.Working_Department = _Row3[0]["Working_Department"].ToString();
                    }
                    else
                    {
                        ObjGroupMenu.Authorization = "1";
                        ObjGroupMenu.Working_Department = "";
                    }
                    ObjGroupMenu.CreatedTime = DateTime.Now;

                    ObjGroupMenu.Insert();
                }
                UpdateFunctionForGroup(iIDGroup, objTemp, _dtInfo);
				objTemp = objTemp.Next;
			}
		}

	} catch (Exception ex) {
        throw (new Exception(ex.Message)); 
	}

}
private void CheckFunctionTree(RadTreeNode currentNode,DataTable _dt,ref bool OK)
{
    try
    {
        //RadTreeNode objTreeNode = default(RadTreeNode);
        RadTreeNode objTemp =null;
        if (currentNode.Nodes.Count > 0)
            objTemp = currentNode.Nodes[0];
        else
            objTemp = null;
        if (objTemp == null)
        {
            return;
            // menu thi hanh
        }
        else
        {
            while ((objTemp != null))
            {
                CheckFunctionTree(objTemp, _dt,ref OK);
                objTemp = objTemp.Next;
            }
            currentNode.CollapseChildNodes();
            DataRow[] _Row = _dt.Select("MenuId = " + currentNode.Value.ToString());
            if (_Row.Length > 0)
            {
                currentNode.Checked = true;
                OK = true;
            }
            foreach (RadTreeNode objTreeNode in currentNode.Nodes)
            {
                DataRow[] _Row1 = _dt.Select("MenuId = " + objTreeNode.Value.ToString());
                if (_Row1.Length > 0)
                {
                    objTreeNode.Checked = true;
                    OK = true;
                }
                
            }
       
        }
    }
    catch (Exception ex)
    {

    }
}
private void CheckDepartmentTree(RadTreeNode currentNode, string[] sArrDe,ref bool OK)
{
    try
    {
        //RadTreeNode objTreeNode = default(RadTreeNode);
        RadTreeNode objTemp = default(RadTreeNode);
        if (currentNode.Nodes.Count > 0)
            objTemp = currentNode.Nodes[0];
        else
            objTemp = null;
        if (objTemp == null)
        {
            return;
            // menu thi hanh
        }
        else
        {
            while ((objTemp != null))
            {
                CheckDepartmentTree(objTemp, sArrDe,ref OK);
                objTemp = objTemp.Next;
            }
            currentNode.CollapseChildNodes();
          
            foreach (RadTreeNode objTreeNode in currentNode.Nodes)
            {
                for (int k = 0; k < sArrDe.Length; k++)
                    if (sArrDe[k] == objTreeNode.Value.ToString())
                    {
                        objTreeNode.Checked = true;
                        OK = true;
                        break;
                    }
            }
            //if (OK)
            //    currentNode.Checked = true;
        }
    }
    catch (Exception ex)
    {

    }
}
private void CheckNodeCon(RadTreeNode currentNode)
{
    RadTreeNode objTemp = default(RadTreeNode);
    int j = 0;
    try
    {
        if (currentNode.Nodes.Count > 0)
            objTemp = currentNode.Nodes[0];
        else
            objTemp = null;
        while ((objTemp != null))
        {
            objTemp.Checked = true;
            CheckNodeCon(objTemp);
            objTemp = objTemp.Next;
        }
    }
    catch (Exception ex)
    {
      
    }
}
private void UnCheckNodeCon(RadTreeNode currentNode)
{
    RadTreeNode objTemp = default(RadTreeNode);
    int j = 0;
    try
    {
        if (currentNode.Nodes.Count > 0)
            objTemp = currentNode.Nodes[0];
        else
            objTemp = null;
        while ((objTemp != null))
        {
            objTemp.Checked = false;
            UnCheckNodeCon(objTemp);
            objTemp = objTemp.Next;
        }
    }
    catch (Exception ex)
    {
       
    }
}
private void UpdateDepartment(ref string sDepartment, RadTreeNode currentNode)
{
    RadTreeNode objTemp = default(RadTreeNode);
    int j = 0;
    try
    {
        if ((currentNode == null))
            return;
        if (currentNode.Nodes.Count > 0)
            objTemp = currentNode.Nodes[0];
        else objTemp = null;
        //  currentNode la menu thi hanh
        if ((objTemp == null) & currentNode.Checked)
        {
            // If currentNode.Checked Then
            sDepartment += currentNode.Value.ToString() + ",";
        }
        else
        {
            if (currentNode.Checked)
            sDepartment += currentNode.Value.ToString() + ",";
            // Else
            while ((objTemp != null))
            {
                j += 1;
                //if(objTemp.Checked)
                //    sDepartment += objTemp.Value.ToString() + ",";
                UpdateDepartment(ref sDepartment, objTemp);
                objTemp = objTemp.Next;
            }
        }

    }
    catch (Exception ex)
    {

    }

}
public void AddNodeToTree(RadTreeNode parentNode, DataTable _dtTree,string FeildValue, string FeildText, string FeildParent)
{
    try
    {
        DataRow[] _Row = null;
        int k = 0;
        RadTreeNode currentNode = default(RadTreeNode);

        _Row = _dtTree.Select(FeildParent + " = " + parentNode.Value.ToString()  ,FeildValue +  " ASC");

        if (_Row.Length > 0)
        {
            for (k = 0; k <= _Row.Length - 1; k++)
            {
                //If _Row(k)("Ten_Hien_Thi").ToString <> "Phân quyền người dùng" Then
                RadTreeNode Node = new RadTreeNode(_Row[k][FeildText].ToString(), _Row[k][FeildValue].ToString());
                 parentNode.Nodes.Add(Node);
                currentNode = Node;
              
                    currentNode.Checked = false;
                    AddNodeToTree(currentNode, _dtTree,   FeildValue,  FeildText,  FeildParent);
                //End If
            }
        }
        //nd _Row(k)("Ten_Hien_Thi").ToString <> "Danh mục tuyến thu" And _Row(k)("Ten_Hien_Thi").ToString <> "Kiểm tra và chốt số liệu" And _Row(k)("Ten_Hien_Thi").ToString <> "Danh mục cụm địa chỉ"
    }
    catch (Exception ex)
    {
       
    }
}
        #endregion

protected void gridGroup_SelectedIndexChanged(object sender, EventArgs e)
{
    if (gridGroup.SelectedItems.Count == 0) return;
    CheckAuThority(Convert.ToInt32(gridGroup.SelectedValue));
    trFunction.ExpandAllNodes();
    trFunction.Nodes[0].Selected = true;
    CheckDepartment(gridGroup.SelectedValue.ToString(), trFunction.SelectedValue.ToString());
}

protected void trFunction_NodeClick(object sender, RadTreeNodeEventArgs e)
{
    if (trFunction.SelectedNodes.Count == 0) return;
    CheckDepartment(gridGroup.SelectedValue.ToString(),trFunction.SelectedValue.ToString());
    trDepartment.ExpandAllNodes();
}

protected void trFunction_NodeCheck(object sender, RadTreeNodeEventArgs e)
{
    if (bProcessing ) return;
	RadTreeNode currentNode = default(RadTreeNode);
	RadTreeNode parentNode = default(RadTreeNode);
	//RadTreeNode objTreeNode = default(RadTreeNode);
	try {
		currentNode = e.Node;
		parentNode = currentNode.ParentNode;
		bProcessing = true;
		//  đặt không xử lý sự kiện AfterCheck nữa
		if (currentNode.Checked) {
			// check tat ca cac nut con
			foreach (RadTreeNode objTreeNode in currentNode.Nodes) {
				objTreeNode.Checked = true;
				CheckNodeCon(objTreeNode);
			}
			if ((parentNode != null)) {
				bool checkAll = false;
				// xet tat ca cac nut ngang hang voi currentNode
				foreach ( RadTreeNode objTreeNode in parentNode.Nodes) {
					if (objTreeNode.Checked == true) {
						checkAll = true;
						break; // TODO: might not be correct. Was : Exit For
					}
				}
				//While Not currentNode Is Nothing
				//    If currentNode.Checked = False Then
				//        checkAll = False
				//        Exit While
				//    Else
				//        currentNode = currentNode.NextNode
				//    End If
				//End While
				while ((parentNode != null)) {
					parentNode.Checked = checkAll;
					parentNode = parentNode.ParentNode;
				}

			}
		} else {
			//bo check tat ca cac nut con
            foreach (RadTreeNode objTreeNode in currentNode.Nodes)
            {
				objTreeNode.Checked = false;
				UnCheckNodeCon(objTreeNode);
			}
			if ((parentNode != null)) {
				bool checkAll = false;
				while ((currentNode != null)) {
					if (currentNode.Checked == true) {
						checkAll = true;
						break; // TODO: might not be correct. Was : Exit While
					}
					currentNode = currentNode.Next;
				}
				currentNode = e.Node;
				while ((currentNode != null)) {
					if (currentNode.Checked == true) {
						checkAll = true;
						break; // TODO: might not be correct. Was : Exit While
					}
					currentNode = currentNode.Prev;
				}
				//Dim UnCheckAll As Boolean = False
				//For Each objTreeNode In parentNode.Nodes   ' xet tat ca cac nut ngang hang voi currentNode
				//    If objTreeNode.Checked Then
				//        UnCheckAll = True
				//        Exit For
				//    End If
				//Next
				parentNode.Checked = checkAll;
			}
			//If Not (parentNode Is Nothing) Then parentNode.Checked = False
		}
		bProcessing = false;
	} catch (Exception ex) {

        bProcessing = false;
	}
}
protected void btnDepartment_Click(object sender, EventArgs e)
{
    try
    {
        string sAutho = "1";
        if (chkAdd.Checked) sAutho += ",2";
        if (chkEdit.Checked) sAutho += ",3";
        if (chkDelete.Checked) sAutho += ",4";
        string sDepartment = "";

        RadTreeNode objTemp = trDepartment.Nodes[0];
            UpdateDepartment(ref sDepartment,  objTemp);

       
        if (sDepartment.Length > 0) sDepartment = sDepartment.Substring(0, sDepartment.Length - 1);

        bool OK = false;
        for(int k=0;k<dtMenuGroup.Rows.Count;k++)
            if (dtMenuGroup.Rows[k]["GroupId"].ToString() == gridGroup.SelectedValue.ToString() && dtMenuGroup.Rows[k]["MenuId"].ToString() == trFunction.SelectedValue.ToString())
            {
                dtMenuGroup.Rows[k]["Authorization"] = sAutho;
                dtMenuGroup.Rows[k]["Working_Department"] = sDepartment;
                OK = true;
                break;
            }
        if (OK == false)
        {
            DataRow dr = dtMenuGroup.NewRow();
            dr["GroupId"] = gridGroup.SelectedValue.ToString();
            dr["MenuId"] = trFunction.SelectedValue.ToString();
            dr["Authorization"] = sAutho;
            dr["Working_Department"] = sDepartment;
            dtMenuGroup.Rows.Add(dr);
        }

    }
    catch (Exception ex)
    {
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                       "MyScript", "alert('Có lỗi khi cập nhật phòng ban làm việc: " + ex.Message + "');", true);

    }
    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),
                        "MyScript", "alert('Cập nhật phòng ban làm việc thành công!');", true);


}

protected void btnUpdate_Click(object sender, EventArgs e)
{
    if (gridGroup.SelectedItems.Count == 0 || trFunction.CheckedNodes.Count==0) return;
    try
    {
        ObjGroupMenu.Delete(Convert.ToInt32(gridGroup.SelectedValue));
        UpdateFunctionForGroup(Convert.ToInt32(gridGroup.SelectedValue), trFunction.Nodes[0], dtMenuGroup);
        uscMsgBox.AddMessage("Cập nhật quyền thành công!", YaBu.MessageBox.uscMsgBox.enmMessageType.Info);
     }
        catch(Exception ex)
    {
        uscMsgBox.AddMessage("Có lỗi khi cập nhật quyền nhóm!", YaBu.MessageBox.uscMsgBox.enmMessageType.Error);
        }
  }

protected void trDepartment_NodeCheck(object sender, RadTreeNodeEventArgs e)
{

    if (bProcessing) return;
    RadTreeNode currentNode = default(RadTreeNode);
    RadTreeNode parentNode = default(RadTreeNode);
    //RadTreeNode objTreeNode = default(RadTreeNode);
    try
    {
        currentNode = e.Node;
        parentNode = currentNode.ParentNode;
        bProcessing = true;
        //  đặt không xử lý sự kiện AfterCheck nữa
        if (currentNode.Checked)
        {
            // check tat ca cac nut con
            foreach (RadTreeNode objTreeNode in currentNode.Nodes)
            {
                objTreeNode.Checked = true;
                CheckNodeCon(objTreeNode);
            }
            //if ((parentNode != null))
            //{
            //    bool checkAll = false;
            //    // xet tat ca cac nut ngang hang voi currentNode
            //    foreach (RadTreeNode objTreeNode in parentNode.Nodes)
            //    {
            //        if (objTreeNode.Checked == true)
            //        {
            //            checkAll = true;
            //            break; // TODO: might not be correct. Was : Exit For
            //        }
            //    }
            //    //While Not currentNode Is Nothing
            //    //    If currentNode.Checked = False Then
            //    //        checkAll = False
            //    //        Exit While
            //    //    Else
            //    //        currentNode = currentNode.NextNode
            //    //    End If
            //    //End While
            //    while ((parentNode != null))
            //    {
            //        parentNode.Checked = checkAll;
            //        parentNode = parentNode.ParentNode;
            //    }

            //}
        }
        else
        {
            //bo check tat ca cac nut con
            foreach (RadTreeNode objTreeNode in currentNode.Nodes)
            {
                objTreeNode.Checked = false;
                UnCheckNodeCon(objTreeNode);
            }
            //if ((parentNode != null))
            //{
            //    bool checkAll = false;
            //    while ((currentNode != null))
            //    {
            //        if (currentNode.Checked == true)
            //        {
            //            checkAll = true;
            //            break; // TODO: might not be correct. Was : Exit While
            //        }
            //        currentNode = currentNode.Next;
            //    }
            //    currentNode = e.Node;
            //    while ((currentNode != null))
            //    {
            //        if (currentNode.Checked == true)
            //        {
            //            checkAll = true;
            //            break; // TODO: might not be correct. Was : Exit While
            //        }
            //        currentNode = currentNode.Prev;
            //    }
            //    //Dim UnCheckAll As Boolean = False
            //    //For Each objTreeNode In parentNode.Nodes   ' xet tat ca cac nut ngang hang voi currentNode
            //    //    If objTreeNode.Checked Then
            //    //        UnCheckAll = True
            //    //        Exit For
            //    //    End If
            //    //Next
            //    parentNode.Checked = checkAll;
            //}
            //If Not (parentNode Is Nothing) Then parentNode.Checked = False
        }
        bProcessing = false;
    }
    catch (Exception ex)
    {

        bProcessing = false;
    }
}
    }
}
