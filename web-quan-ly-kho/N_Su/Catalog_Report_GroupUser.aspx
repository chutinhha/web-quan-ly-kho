<%@ Page Language="C#"  MasterPageFile="~/master/default.master" AutoEventWireup="true" CodeBehind="Catalog_Report_GroupUser.aspx.cs" Inherits="QLCV.N_Su.Catalog_Report_GroupUser" %>
<%@ Register src="../controls/uscMsgBox.ascx" tagname="uscMsgBox" tagprefix="uc1" %>


<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content  ID="Content1" ContentPlaceHolderID="m_contentBody" Runat="Server" >
<link href="../CSS/Modules/Message/prms.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Modules/MeetingRoom/main.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Modules/Nhan_Su/hrm.css" rel="stylesheet" type="text/css" />
    
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
   <table width="100%">
    <tbody>
        <asp:Literal ID="ltlAuthorityList" runat="server"></asp:Literal>
        
		<tr>
		    <td>
    		    <table cellpadding="0" cellspacing="0" height="100%" width="100%">
	                <tbody><tr>
		                <td bgcolor="whitesmoke" nowrap="nowrap" valign="top" width="200px">
			                <table cellpadding="0" cellspacing="0" width="100%">
			                <tbody><tr>
				                <td class="ModuleHead_Admin"><img src="../images/NhanSu/hrm.gif" align="absMiddle" border="0" hspace="3">PHÂN QUYỀN</td>

			                </tr>
			                </tbody></table>
			                <div id="HRM_Left-Menu">
			                    <div class="HRM_top-level">QUYỀN CHỨC NĂNG</div>
                                <div class="HRM_second-level"><a id="hrmcontrol_lnkModuleSetting" href="Permission.aspx"><img src="../images/NhanSu/User-Group-icon.png" align="absMiddle" border="0" hspace="2"><b>Danh 
                                    sách phân quyền</b></a></div>
				                <div class="HRM_second-level"><a id="A2" href="Permission.aspx?P_action=2"><img src="../images/NhanSu/group-add-icon.png" align="absMiddle" border="0" hspace="2"><b>Phân quyền Nhóm</b></a></div>
				                <div class="HRM_second-level"><a id="A1" href="Permission.aspx?P_action=3"><img src="../images/NhanSu/user-add-icon.png" align="absMiddle" border="0" hspace="2"><b>Phân quyền Người dùng</b></a></div>
				                <div class="HRM_top-level">QUYỀN BÁO CÁO</div>
				                <div class="HRM_second-level"><a id="A3" href="Catalog_Report_GroupUser.aspx"><img src="../images/NhanSu/distributor-report.png" align="absMiddle" border="0" hspace="2"><b>Nhóm người dùng</b></a></div>
				                <div class="HRM_second-level"><a id="A4" href="Catalog_Report_User.aspx"><img src="../images/NhanSu/report-user-icon.png" align="absMiddle" border="0" hspace="2"><b>Người dùng</b></a></div>
                                                         <div class="HRM_top-level">QUẢN LÝ ĐĂNG NHẬP</div>
                                <div class="HRM_second-level"><a id="A5" href="View_Log_Login.aspx"><img src="../images/NhanSu/distributor-report.png" align="absMiddle" border="0" hspace="2"><b>Xem log thao tác</b></a></div>
	
			                </div>
		                </td>

		                <td background="/images/bg_vline_dot.gif" width="1"><img src="/images/1x1.gif" width="1"></td>
		                <td width="3"><img src="/images/1x1.gif" width="3"></td>
		                <td height="100%" valign="top">
   
                            <table style="width: 100%">
            <tr>
                <td style="width: 473px; text-align: center; font-size: medium;">
                    <b>Danh sách nhóm người dùng</b></td>
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
                <td style="width: 473px">
                    <telerik:RadGrid ID="gridGroup" runat="server"  
                        AutoGenerateColumns="False" GridLines="None" Height="460px" Skin="Office2007" 
                        Width="100%" onselectedindexchanged="gridGroup_SelectedIndexChanged">
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
                            <Scrolling AllowScroll="true" SaveScrollPosition="true" />
                        </ClientSettings>
                   <%--     <FilterMenu EnableTheming="True">
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </FilterMenu>--%>
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
                            <Scrolling AllowScroll="true" SaveScrollPosition="true" />
                        </ClientSettings>
                       <%-- <FilterMenu EnableTheming="True">
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </FilterMenu>--%>
                    </telerik:RadGrid>
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 473px">
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
                        </td>
                    </tr>
                    </tbody>
                </table>
            </td>
        </tr>
	</tbody>
    </table>
   
        </ContentTemplate>
    </asp:UpdatePanel>
   <uc1:uscMsgBox ID="uscMsgBox" runat="server"  />
</asp:Content>
