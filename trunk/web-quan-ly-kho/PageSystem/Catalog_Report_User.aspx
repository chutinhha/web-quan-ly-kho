<%@ Page Language="C#"  MasterPageFile="~/master/default.master" AutoEventWireup="true" CodeBehind="Catalog_Report_User.aspx.cs" Inherits="QLCV.PageSystem.Catalog_Report_User" %>
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
                <td style="width: 521px; text-align: left; font-size: small;">
                    <table style="width: 100%">
                        <tr>
                            <td style="font-size: small; width: 58px">
                                <b>Nhóm: </b>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cboGroup" Runat="server" AutoPostBack="True" 
                                    onselectedindexchanged="cboGroup_SelectedIndexChanged" Skin="Vista" 
                                    Width="100%">
                                    <CollapseAnimation Duration="200" Type="OutQuint" />
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 7px">
                    &nbsp;</td>
                <td style="width: 522px; text-align: center; font-size: medium;">
                    <b>Danh sách báo cáo</b></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 521px">
                    <telerik:RadGrid ID="gridUser" runat="server"  
                        AutoGenerateColumns="False" GridLines="None" Height="460px" Skin="Office2007" 
                        Width="100%" onselectedindexchanged="gridGroup_SelectedIndexChanged">
                        <MasterTableView DataKeyNames="ID">
                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px" />
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px" />
                            </ExpandCollapseColumn>
                            <Columns>
                                
                                <telerik:GridBoundColumn DataField="FullName" HeaderText="Họ tên" 
                                    UniqueName="FullName">
                                    <HeaderStyle Width="50%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Department" HeaderText="Phòng ban" 
                                    UniqueName="Department">
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
                <td style="width: 7px">
                    &nbsp;</td>
                <td style="width: 522px">
                    <telerik:RadGrid ID="gridReport" runat="server" AllowMultiRowSelection="True" 
                        AutoGenerateColumns="False" GridLines="None" Height="460px" Skin="Office2007" 
                        Width="100%">
                        <MasterTableView DataKeyNames="ID_Report">
                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px" />
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px" />
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridClientSelectColumn UniqueName="colCheck">
                                    <HeaderStyle Width="10px" />
                                </telerik:GridClientSelectColumn>
                                <telerik:GridBoundColumn DataField="Report_Name" HeaderText="Tên báo cáo" 
                                    UniqueName="Report_Name">
                                    <HeaderStyle Width="60%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Note" HeaderText="Mô tả" 
                                    UniqueName="Note">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <ClientSettings>
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                        <FilterMenu EnableTheming="True">
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </FilterMenu>
                    </telerik:RadGrid>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 521px">
                    &nbsp;</td>
                <td style="width: 7px">
                    &nbsp;</td>
                <td style="width: 522px">
                    <asp:Button ID="btnUpdate" runat="server" Height="37px" 
                        onclick="btnUpdate_Click" Text="Cập nhật" Width="141px" />
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
   
        </ContentTemplate>
    </asp:UpdatePanel>
   <uc1:uscMsgBox ID="uscMsgBox" runat="server"  />
</asp:Content>
