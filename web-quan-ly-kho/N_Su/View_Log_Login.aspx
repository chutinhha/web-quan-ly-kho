<%@ Page Language="C#"  MasterPageFile="~/master/default.master" AutoEventWireup="true" CodeBehind="View_Log_Login.aspx.cs" Inherits="QLCV.N_Su.View_Log_Login" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content  ID="Content1" ContentPlaceHolderID="m_contentBody" Runat="Server" >
    <link href="../CSS/Modules/Message/prms.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Modules/MeetingRoom/main.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Modules/Nhan_Su/hrm.css" rel="stylesheet" type="text/css" />
    


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
   <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode ="Conditional"    >
   <ContentTemplate>
                            <table style="width: 100%">
            <tr>
                <td style="text-align: center; font-size: medium;" colspan="3">
                    <table class="style4">
                        <tr>
                            <td style="width: 74px; height: 26px;">
                                <strong>Từ ngày:</strong></td>
                            <td style="width: 140px; height: 26px">
                                <telerik:RadDatePicker ID="dtFrom" runat="server" 
                                    Culture="Vietnamese (Vietnam)" Font-Names="Tahoma" Font-Size="9pt" 
                                    Height="20px" MinDate="1880-01-01" Skin="Web20" Width="147px">
                                    <DateInput Height="20px" LabelCssClass="radLabelCss_Web20" Skin="Web20">
                                    </DateInput>
                                    <Calendar Skin="Web20">
                                    </Calendar>
                                    <DatePopupButton CssClass="radPopupImage_Web20" 
                                        hoverimageurl="~/images/Calendar-icon.png" 
                                        imageurl="~/images/Calendar-icon.png" />
                                </telerik:RadDatePicker>
                            </td>
                            <td style="width: 75px; height: 26px">
                                <strong>Đến ngày:</strong></td>
                            <td style="height: 26px">
                                <telerik:RadDatePicker ID="dtTo" runat="server" 
                                    Culture="Vietnamese (Vietnam)" Font-Names="Tahoma" Font-Size="9pt" 
                                    Height="20px" MinDate="1880-01-01" Skin="Web20" Width="147px">
                                    <DateInput LabelCssClass="radLabelCss_Web20" Skin="Web20" Height="20px">
                                    </DateInput>
                                    <Calendar Skin="Web20">
                                    </Calendar>
                                    <DatePopupButton CssClass="radPopupImage_Web20" 
                                        hoverimageurl="~/images/Calendar-icon.png" 
                                        imageurl="~/images/Calendar-icon.png" />
                                </telerik:RadDatePicker>
                                &nbsp;
                            </td>
                            <td style="height: 26px">
                                </td>
                            <td style="height: 26px; width: 54px">
                                </td>
                        </tr>
                        <tr>
                            <td style="width: 74px; height: 29px;">
                                <asp:Label ID="Label2" runat="server" Font-Bold="True" 
                                    Font-Names="Times New Roman" Font-Size="11pt" Text="Phòng/Ban: "></asp:Label>
                            </td>
                            <td class="style2" colspan="2" style="height: 29px">
                                 <script type="text/javascript">
                                     function nodeClicking(sender, args) {
                                         var comboBox = $find("<%= rcbDepartment.ClientID %>");

                                         var node = args.get_node()

                                         comboBox.set_text(node.get_text());

                                         comboBox.trackChanges();
                                         comboBox.get_items().getItem(0).set_text(node.get_text());
                                         comboBox.commitChanges();

                                         comboBox.hideDropDown();

                                         // Call comboBox.attachDropDown if:
                                         // 1) The RadComboBox is inside an AJAX panel.
                                         // 2) The RadTreeView has a server-side event handler for the NodeClick event, i.e. it initiates a postback when clicking on a Node.
                                         // Otherwise the AJAX postback becomes a normal postback regardless of the outer AJAX panel.

                                         //comboBox.attachDropDown();
                                     }

                                     function StopPropagation(e) {
                                         if (!e) {
                                             e = window.event;
                                         }

                                         e.cancelBubble = true;
                                     }

                                     function OnClientDropDownOpenedHandler(sender, eventArgs) {
                                         var tree = sender.get_items().getItem(0).findControl("rtrDepartment");
                                         var selectedNode = tree.get_selectedNode();
                                         if (selectedNode) {
                                             selectedNode.scrollIntoView();
                                         }
                                     }
                        </script>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server"  UpdateMode ="Conditional"    >
   <ContentTemplate>
                                <telerik:RadComboBox ID="rcbDepartment" runat="server" 
                                    CollapseAnimation-Type="None" ExpandAnimation-Type="None" 
                                    OnClientDropDownOpened="OnClientDropDownOpenedHandler" 
                                    Style="vertical-align: middle;" TabIndex="1" Width="250px">
                                    <ItemTemplate>
                                        <div ID="div1">
                                            <asp:UpdatePanel ID="upTreeView" runat="server">
                                                <ContentTemplate>
                                                    <telerik:RadTreeView ID="rtrDepartment" runat="server" Height="140px" 
                                                        OnClientNodeClicking="nodeClicking" OnNodeClick="rtrDepartment_NodeClick" 
                                                        Width="100%">
                                                    </telerik:RadTreeView>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="rtrDepartment" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </ItemTemplate>
                                    <Items>
                                        <telerik:RadComboBoxItem Text="" />
                                    </Items>
                                </telerik:RadComboBox>
                                   </ContentTemplate>
    </asp:UpdatePanel>
                                  <script type="text/javascript">
                                      var div1 = document.getElementById("div1");
                                      div1.onclick = StopPropagation;
                        </script>
                            </td>
                            <td style="text-align: left; height: 29px">
                                <asp:Label ID="Label3" runat="server" style="font-weight: 700" 
                                    Text="Nhân viên:" Width="81px"></asp:Label>
&nbsp;<asp:TextBox ID="txtEmployee" runat="server" Width="213px"></asp:TextBox>
                            </td>
                            <td style="width: 54px; height: 29px">
                                <asp:Button ID="btnSearch" runat="server" Height="31px" 
                                    style="font-weight: 700" Text="Lấy dữ liệu" Width="83px" 
                                    onclick="btnSearch_Click" />
                            </td>
                            <td style="width: 54px; height: 29px">
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
                                <tr>
                                    <td style="width: 473px; text-align: center; font-size: medium; height: 15px;">
                                        <b style="font-size: small">Danh sách Login</b></td>
                                    <td style="width: 7px; height: 15px;">
                                        </td>
                                    <td style="width: 522px; text-align: center; font-size: medium; height: 15px;">
                                        <b style="font-size: small">Danh sách thao tác</b></td>
                                </tr>
                                  <asp:UpdatePanel ID="UpdatePanel3" runat="server"  UpdateMode ="Conditional"    >
   <ContentTemplate>
            <tr>
                <td style="width: 473px">
                    <telerik:RadGrid ID="gridLogin" runat="server"  
                        AutoGenerateColumns="False" GridLines="None" Height="500px" Skin="Office2007" 
                        Width="100%" onselectedindexchanged="gridLogin_SelectedIndexChanged">
                        <MasterTableView DataKeyNames="ID_Log">
                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px" />
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px" />
                            </ExpandCollapseColumn>
                            <Columns>
                                
                                <telerik:GridBoundColumn DataField="FullName" HeaderText="Tên nhân viên" 
                                    UniqueName="FullName">
                                    <HeaderStyle Width="60%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Date_Login" HeaderText="TG login" 
                                    UniqueName="Date_Login">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Date_Logout" HeaderText="TG logout" 
                                    UniqueName="Date_Logout">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Client_Name" HeaderText="Từ máy" 
                                    UniqueName="Client_Name">
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
                    <telerik:RadGrid ID="gridOperator" runat="server" AllowMultiRowSelection="True" 
                        AutoGenerateColumns="False" GridLines="None" Height="500px" Skin="Office2007" 
                        Width="100%">
                        <MasterTableView DataKeyNames="ID">
                            <RowIndicatorColumn>
                                <HeaderStyle Width="20px" />
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn>
                                <HeaderStyle Width="20px" />
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridBoundColumn DataField="Form_Operator" HeaderText="Chức năng" 
                                    UniqueName="Form_Operator">
                                    <HeaderStyle Width="60%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Operator" HeaderText="Thao tác" 
                                    UniqueName="Operator">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Date_Operator" HeaderText="Thời gian" 
                                    UniqueName="Date_Operator">
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
            </tr>
                        </ContentTemplate>
    </asp:UpdatePanel>
                          
        </table>
         </ContentTemplate>
    </asp:UpdatePanel>
                        </td>
                    </tr>
                    </tbody>
                </table>
            </td>
        </tr>
	</tbody>
    </table>
   
       
  
</asp:Content>
