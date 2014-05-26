<%@ Page Language="C#"  MasterPageFile="~/master/default.master" AutoEventWireup="true" CodeBehind="Authority_Group.aspx.cs" Inherits="QLCV.PageSystem.Authority_Group" %>
<%@ Register src="../controls/uscMsgBox.ascx" tagname="uscMsgBox" tagprefix="uc1" %>


<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content  ID="Content1" ContentPlaceHolderID="m_contentBody" Runat="Server" >
  <script type="text/javascript">
// Jefferson Scher -- Oct. 1, 2007 -- Fx2 + IE6 on WinXPSP2
  
    function OnClientBlur(sender,args)
    {
        sender.set_closeDropDownOnBlur(true);
    }
function jfspress(e) { 
 // Check event for Enter key only
 var evt = (e) ? e : window.event;
 var key = (evt.keyCode) ? evt.keyCode : evt.which;
 if (key!=13) return true;

 // Check event for target in desired form/nodeName/type
 var target = (evt.target) ? evt.target : evt.srcElement;
 if (!target.form) return true;
 var nod = "input|select|textarea|";
 
if (nod.indexOf(target.nodeName.toLowerCase())<0) return true;
// var inpTypes = "text|file|checkbox|radio|select-one|select";
// // alert(target.getAttribute("type").toLowerCase());
// if (target.getAttribute("type")) {
//  if (inpTypes.indexOf(target.getAttribute("type").toLowerCase())<0) return true;
// } else { // for textarea controls
//  if (target.className != "enterExits") return true;
// }


 // Find and focus next form control
 //  Find target in the form's elements collection
 var els = target.form.elements;
 for (var i=0; i<els.length; i++){
  if (els[i]==target) {
   if (i==els.length-1) {
    return true; // last element, do default
   } else {
    var eldex = i;
    break;
   }
  }
 }
 //alert(els[eldex].tabIndex);
 //  If control has manually set tabIndex, try to find next higher
 if (target.tabIndex!=0) {
  var minTI = 999;
  var minTIel = -1;
  for (var k=0; k<els.length; k++){
   if (k!=eldex && els[k].tabIndex>=target.tabIndex && els[k].tabIndex<minTI && 
       els[k].disabled==false && els[k].type!="hidden") {
    minTI = els[k].tabIndex;
    minTIel = k;
   }
  }
  
  if (minTIel>-1) { // go to next by numbered tabIndex
   els[minTIel].focus();
   return false;
  }
 }

  
 //  Try natural tab order, forward only
 for (var j=eldex+1; j<els.length; j++){
  if (els[j].tabIndex==0 && els[j].disabled==false && els[j].type!="hidden") { // go to next in natural order
   els[j].focus();
   return false;   
  }
 }

 // TEMPORARY CATCH ALL FOR TESTING ONLY
 //alert("Not handled; eldex="+eldex);
 return false;
} // end function jfspress()

function setup() {
 // add onkeydown event handlers to <form>s
 var forms = document.getElementsByTagName("FORM");
 for (var i=0; i<forms.length; i++) {
  forms[i].onkeypress=jfspress;
 }
} // end function setup()

window.onload = setup; // add onkeypress to each form
</script>
    
     
    <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode ="Conditional"    >
   <ContentTemplate>
   
          <table style="width: 100%">
              <tr>
                  <td style="width: 408px; height: 22px; text-align: center; font-size: small;">
                      <b>Danh sách nhóm người dùng</b></td>
                  <td style="width: 6px; height: 22px">
                  </td>
                  <td style="height: 22px; text-align: center; width: 403px; font-size: small;">
                      <b>Chức năng</b></td>
                  <td style="width: 9px; height: 22px">
                  </td>
                  <td style="height: 22px; text-align: center; font-size: small;">
                      <b>Quyền</b></td>
              </tr>
              <tr>
                  <td style="width: 408px; height: 464px">
                      <telerik:RadGrid ID="gridGroup" runat="server" GridLines="None" Height="477px" 
                          Skin="Office2007" Width="398px" AutoGenerateColumns="False" 
                          onselectedindexchanged="gridGroup_SelectedIndexChanged">
                          <MasterTableView DataKeyNames="GroupID">
                              <RowIndicatorColumn>
                                  <HeaderStyle Width="20px" />
                              </RowIndicatorColumn>
                              <ExpandCollapseColumn>
                                  <HeaderStyle Width="20px" />
                              </ExpandCollapseColumn>
                              <Columns>
                                  <telerik:GridBoundColumn DataField="GroupName" HeaderText="Tên nhóm" 
                                      UniqueName="GroupName">
                                      <HeaderStyle Width="60%" />
                                  </telerik:GridBoundColumn>
                                  <telerik:GridBoundColumn DataField="Detail" HeaderText="Mô tả" 
                                      UniqueName="Detail">
                                  </telerik:GridBoundColumn>
                              </Columns>
                          </MasterTableView>
                          <ClientSettings EnablePostBackOnRowClick="True">
                              <Selecting AllowRowSelect="True" />
                          </ClientSettings>
                          <FilterMenu EnableTheming="True">
                              <CollapseAnimation Duration="200" Type="OutQuint" />
                          </FilterMenu>
                      </telerik:RadGrid>
                  </td>
                  <td style="width: 6px; height: 464px">
                  </td>
                  <td style="height: 464px; width: 403px">
                      <asp:Panel ID="Panel1" runat="server" Height="480px" Width="395px" 
                          BorderColor="#9900FF" BorderStyle="Solid" BorderWidth="1px">
                          <telerik:RadTreeView ID="trFunction" Runat="server" Height="100%" 
                              Skin="Office2007" Width="100%" CheckBoxes="True" 
                              onnodecheck="trFunction_NodeCheck" onnodeclick="trFunction_NodeClick">
                              <CollapseAnimation Duration="100" Type="OutQuint" />
                              <ExpandAnimation Duration="100" />
                          </telerik:RadTreeView>
                      </asp:Panel>
                  </td>
                  <td style="width: 9px; height: 464px">
                  </td>
                  <td style="height: 464px">
                      <table style="border: 1px inset #9900CC; width: 100%; height: 100%" 
                          border="1">
                          <tr>
                              <td class="style4" style="border: 1px solid #660066; height: 29px">
                                  <table style="width: 100%">
                                      <tr>
                                          <td style="width: 57px">
                                              &nbsp;</td>
                                          <td style="width: 64px">
                                              <asp:CheckBox ID="chkAdd" runat="server" Text="Thêm" />
                                          </td>
                                          <td style="width: 5px">
                                              &nbsp;</td>
                                          <td style="width: 59px">
                                              <asp:CheckBox ID="chkEdit" runat="server" Text="Sửa" />
                                          </td>
                                          <td style="width: 6px">
                                              &nbsp;</td>
                                          <td style="width: 128px">
                                              <asp:CheckBox ID="chkDelete" runat="server" Text="Xóa" />
                                          </td>
                                          <td>
                                              &nbsp;</td>
                                      </tr>
                                  </table>
                              </td>
                          </tr>
                          <tr>
                              <td class="style4" style="height: 7px; text-align: center; font-weight: bold;">
                                  Phòng ban</td>
                          </tr>
                          <tr>
                              <td style="height: 420px">
                                  <asp:Panel ID="Panel2" runat="server" Height="410px" Width="424px">
                                      <telerik:RadTreeView ID="trDepartment" Runat="server" Height="100%" 
                                          Skin="Office2007" Width="100%" CheckBoxes="True" 
                                          onnodecheck="trDepartment_NodeCheck">
                                          <CollapseAnimation Duration="100" Type="OutQuint" />
                                          <ExpandAnimation Duration="100" />
                                      </telerik:RadTreeView>
                                  </asp:Panel>
                              </td>
                          </tr>
                      </table>
                  </td>
              </tr>
              <tr>
                  <td style="width: 408px">
                  
                      <asp:Button ID="btnUpdate" runat="server" Height="33px" 
                          onclick="btnUpdate_Click" Text="Cập nhật" Width="137px" />
                  </td>
                  <td style="width: 6px">
                      &nbsp;</td>
                  <td style="width: 403px">
                      &nbsp;</td>
                  <td style="width: 9px">
                      &nbsp;</td>
                  <td>
                      &nbsp;<asp:Button ID="btnDepartment" runat="server" Height="28px" 
                          onclick="btnDepartment_Click" Text="Cập nhật phòng ban" Width="146px" />
                  </td>
              </tr>
          </table>
     <uc1:uscMsgBox ID="uscMsgBox" runat="server"  />
          </ContentTemplate>
    </asp:UpdatePanel>
 
</asp:Content>
