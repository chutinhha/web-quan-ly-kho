<%@ Page Language="C#" MasterPageFile="~/master/default.master" AutoEventWireup="true" CodeBehind="Calculate_Salary.aspx.cs" Inherits="QLCV.N_Su.Calculate_Salary" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register src="../controls/uscMsgBox.ascx" tagname="uscMsgBox" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <link href="../CSS/Modules/Message/prms.css" rel="stylesheet" type="text/css" />
<link href="../CSS/Modules/MeetingRoom/main.css" rel="stylesheet" type="text/css" />
<link type="text/css" href="../CSS/HC/p_main.css" rel="stylesheet" />

<script src="../js/EnterEvent.js" type="text/javascript"></script>
<script src="../js/HC/p_main.js" type="text/javascript"></script>

<telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    <script type="text/javascript">
        function refreshGrid(arg) {
            if (!arg) {
                $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("Rebind");
            }
            else {
                $find("<%= RadAjaxManager1.ClientID %>").ajaxRequest("RebindAndNavigate");
            }
        }
        function RowDblClick(sender, eventArgs) {
            window.radopen("Detail_Salary.aspx?SalaryId=" + eventArgs.getDataKeyValue("SalaryId"), "UserListDialog");
        }

        function reloadPage() {
            window.location.reload();
        }

        function newWindow1(file, window) {
            msgWindow = open(file, window, 'resizable=no,width=800,height=400,scrollbars=yes,top=80,left=400');
            if (msgWindow.opener == null) msgWindow.opener = self;
        }

        function newWindow2(file, window) {
            msgWindow = open(file, window, 'resizable=no,width=1000,height=800,scrollbars=yes,top=20,left=50');
            if (msgWindow.opener == null) msgWindow.opener = self;
        }
    </script>
</telerik:RadCodeBlock>
<telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadAjaxManager1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grid_Salary" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="grid_Salary" EventName="Pageindexchanged">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grid_Salary" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="ddlCompany" EventName="SelectedIndexChanged">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="ddlDepartment" />
                <telerik:AjaxUpdatedControl ControlID="grid_Salary" />
            </UpdatedControls>
        </telerik:AjaxSetting>
        <telerik:AjaxSetting AjaxControlID="ddlDepartment" EventName="SelectedIndexChanged">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="grid_Salary" />
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>

<%--
 <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>--%>

<table  cellpadding="2"  width = "1250px" >
<asp:Literal ID="ltlAuthorityList" runat="server"></asp:Literal>
<tr>
    <td id="maincontrol_LeftPane"  width="180px" vAlign="top" class="LeftPanel_BG" bgcolor="#E7EBF7" style="DISPLAY: none">
        <Table width="180px" cellpadding="0" cellspacing="0" >
            <tr class="ModuleHead_Admin">
	            <td class="ModuleHead" style=" width:100%"><img src="../images/NhanSu/catalog-icon.png" align="absMiddle" border="0" hspace="3">TIỀN LƯƠNG
		        </td>
            </tr>
        </Table>
        <Div id="TSK_Left-Menu">
            <asp:Literal ID="ltlTabDetail" runat="server"></asp:Literal>
        </Div>
            <Table width="180" cellpadding="0" cellspacing="0">
                                <tr class="ModuleHead_Admin">
	                                <td class="ModuleHead" style=" width:100%"><img src="../images/NhanSu/catalog-icon.png" align="absMiddle" border="0" hspace="3">THƯỞNG
		                            </td>
                                </tr>
                            </Table>
                            <Div id="TSK_Left-Menu1">
                                <asp:Literal ID="ltlTabReward" runat="server"></asp:Literal>
                            </Div>
    </td>
    <td id="maincontrol_SeperatePane" class="small-separator-pane" title="Nhấn vào đây để ẩn, hiện danh sách Menu" width="1" onclick="toggleLeftPane('maincontrol_LeftPane', 'maincontrol_SeperatePane');"><img id="maincontrol_imgToggle" title="Nhấn vào đây để ẩn, hiện thanh công cụ" vspace="250" onmouseclick="toggleLeftPane('maincontrol_LeftPane', 'maincontrol_SeperatePane');" onmouseover="this.src='../images/QLCV/menu_ungrippy.gif';" onmouseout="this.src='../images/QLCV/menu_ungrippy.gif';" src="../images/QLCV/menu_ungrippy.gif" alt="" style="border-width:0px;width:6px;" /></td>
    <td id="maincontrol_RightPane" width="100%" valign="top" style="height:100%" colspan="3">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
		    <tr>
                <td colspan="7">
                    <table width="100%">
                        <tr>
                            <td class="T1" style="width: 9%">Tính Lương</td>
                            <td width="50%">
                                <asp:Label ID="lblState" runat="server"></asp:Label>
                            </td>
                            <td align="right" class="NormalBold" valign="bottom">
                                <small><u>Ghi chú:</u><font color="red"> Click đúp dòng lương để xem/cập nhật TT lương từng người </font></small>
                            &nbsp;&nbsp;</td>
                        </tr>
                    </table>
                    <hr class="HR1" />
                </td>
                
            </tr>
		    <tr style="height:30px">
		        <td width="130px" align="right">
		            <asp:Label ID="Label1" runat="server" Font-Bold="True" 
                            Font-Names="Times New Roman" Font-Size="11pt" Text="Tính lương: "></asp:Label>
		        </td>
		        <td style="width:200px">
		            <asp:DropDownList ID="ddlSType" runat="server" Width="170px"
		                DataTextField="Text" DataValueField="Code">
                    </asp:DropDownList>
		        </td>
                <td width="80px" align="right">
                        <asp:Label ID="Label5" runat="server" Font-Bold="True" 
                            Font-Names="Times New Roman" Font-Size="11pt" Text="Năm: "></asp:Label>
                </td>
                <td width="100px">
                    <asp:DropDownList ID="ddlYear" runat="server" Width="80px"
                        AutoPostBack="true" onselectedindexchanged="ddlYear_SelectedIndexChanged">
                    </asp:DropDownList>   </td>
                <td align="right" style="width:40px" >
                    <asp:Label ID="Label4" runat="server" Font-Bold="True" 
                        Font-Names="Times New Roman" Font-Size="11pt" Text="Tháng: "></asp:Label>
                </td>
                <td style="width:110px">
                    <asp:DropDownList ID="ddlMonth" runat="server" Width="90px"
                        AutoPostBack="true" onselectedindexchanged="ddlMonth_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:UpdatePanel runat="server" ID="upCSalary">
                        <ContentTemplate>
                    
                            <asp:Button ID="btnReset" runat="server" onclick="Button1_Click" Text="Reset Lương phòng" 
                                Font-Bold="True" Font-Names="Tahoma" Font-Size="10pt" Visible="false" Width="140px"/>
                            &nbsp;
                            <asp:Button ID="btnClose" runat="server" Text="Chốt bảng Lương" 
                                Font-Bold="True" Font-Names="Tahoma" Font-Size="10pt" 
                                onclick="btnClose_Click" Visible="false" Width="140px"/>
                            &nbsp;
                            <asp:LinkButton ID="lnkCongThuc" runat="server" onclick="lnkCongThuc_Click" 
                                ForeColor="#9933FF">Công thức tính lương</asp:LinkButton>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnReset"/>
                            <asp:PostBackTrigger ControlID="btnClose"/>
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
		    <tr style="height:30px">
                <td align="right" style="width:130px" ><asp:Label ID="Label3" runat="server" Font-Bold="True" 
                            Font-Names="Times New Roman" Font-Size="11pt" Text="Công Ty: "></asp:Label>
                </td>
                <td style="width:200px">
                    <asp:DropDownList ID="ddlCompany" runat="server" Width="170px"
                        DataTextField="ComName" DataValueField="Id" AutoPostBack="true"
                        onselectedindexchanged="ddlCompany_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td align="right" width="80px">
                        <asp:Label ID="Label2" runat="server" Font-Bold="True" 
                            Font-Names="Times New Roman" Font-Size="11pt" Text="Phòng/Ban: "></asp:Label>
                </td>
                <td width="300px" colspan="3">
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
                        
                        <telerik:RadComboBox ID="rcbDepartment" runat="server" Width="250px"
                            Style="vertical-align: middle;" OnClientDropDownOpened="OnClientDropDownOpenedHandler"
                            ExpandAnimation-Type="None" CollapseAnimation-Type="None" TabIndex="1">
                            <ItemTemplate>
                                <div id="div1">
                                    <asp:UpdatePanel ID="upTreeView" runat="server">
                                        <ContentTemplate>
                                            <telerik:RadTreeView ID="rtrDepartment" runat="server" Width="100%"
                                                OnClientNodeClicking="nodeClicking" Height="140px" OnNodeClick="rtrDepartment_NodeClick">
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
                        
                        <script type="text/javascript">
                            var div1 = document.getElementById("div1");
                            div1.onclick = StopPropagation;
                        </script>
                    </td>
                <td>
                     <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                        <ContentTemplate>
                            <asp:Button ID="btnUpdate" runat="server" Font-Bold="True"
                                Font-Names="Tahoma" Font-Size="10pt" Text="Tính Lương" 
                                Width="140px" onclick="btnUpdate_Click" Visible="false" />
                                    &nbsp;
                            <asp:Button ID="btnCommonValue" runat="server" Font-Bold="True"
                                Font-Names="Tahoma" Font-Size="10pt" Text="Tham số Chung" 
                                Visible="false" onclick="btnCommonValue_Click" Width="140px"/>
                            &nbsp;
                            <asp:Button ID="Button2" runat="server" Text="Reset Lương List"  
                                Font-Bold="True" Font-Names="Tahoma" 
                                Font-Size="10pt" 
                                onclick="Button1_Click1" Width="132px" />
                           
                              
                            &nbsp;
                            <asp:Button ID="btnOffWork" runat="server" onclick="btnOffWork_Click" 
                                style="font-weight: 700" Text="List nghỉ việc" Width="95px" />
                           
                              
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnUpdate"/>
                            <asp:PostBackTrigger ControlID="btnClose"/>
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <table width="1250px">
            <tr>
                <td align="center" style="width:1240px">
                <asp:Panel runat="server" ID="pnSalary" ScrollBars="None">
                    <telerik:RadGrid ID="grid_Salary" runat="server" AutoGenerateColumns="False" 
                        Skin="Office2007" AllowPaging="True" PageSize="200" Height="500px"
                        Width="1150px" onpageindexchanged="grid_Salary_PageIndexChanged" 
                        GridLines="None" HorizontalAlign="Center">
                        <PagerStyle Mode="NumericPages"  />
                        <MasterTableView DataKeyNames="SalaryId" GroupLoadMode="Client"        
                            ClientDataKeyNames="SalaryId" PageSize="200">
                            <RowIndicatorColumn>
                                <HeaderStyle Width="200px" />
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn>
                                <HeaderStyle Width="200px" />
                            </ExpandCollapseColumn>
                            <Columns>
                            </Columns>
                            <NoRecordsTemplate>
                                <b>Phòng Ban chưa có nhân viên, nên không thể tính lương được.</b>
                            </NoRecordsTemplate>
                            <HeaderStyle Height="40px" />
                        </MasterTableView>
                        <ClientSettings>
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"  FrozenColumnsCount="2" />
                            <Selecting AllowRowSelect="true" />
                            <ClientEvents OnRowDblClick="RowDblClick" />
                        </ClientSettings>
                       <%-- <FilterMenu EnableTheming="True" Skin="Office2007">
                            <CollapseAnimation Duration="200" Type="OutQuint" />
                        </FilterMenu>--%>
                    </telerik:RadGrid>
                    
                    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true">
                        <Windows>
                            <telerik:RadWindow runat="server" Title="Nhập Chi Tiết Lương" Height="600px"
                                Width="1000px" Left="100px" ReloadOnShow="true" ShowContentDuringLoad="false"
                                Modal="true" ID="UserListDialog"></telerik:RadWindow>
                        </Windows>
                    </telerik:RadWindowManager>
                </asp:Panel>
                </td>
            </tr>
        </table>
        <table width="1200px">
            <tr>
                <td align="center">
                    <asp:LinkButton 
                        ID="lnkWord" runat="server" onclick="lnkWord_Click" Visible="false"><img src="../images/IcoType/ico_Word.gif"  align="absmiddle" />Xuất ra Word</asp:LinkButton>
            &nbsp;
                    <asp:LinkButton ID="lnkExcel" runat="server" onclick="lnkExcel_Click" Visible="false"><img src="../images/IcoType/ico_Excel.gif" align="absmiddle" />Xuất ra Excel</asp:LinkButton>
                    &nbsp;
                     
                    <asp:LinkButton ID="LinkExcelAll" runat="server" onclick="lnkExcelAll_Click"><img src="../images/IcoType/ico_Excel.gif" align="absmiddle" />Xuất bảng lương</asp:LinkButton>
                    &nbsp;
                  <%--  <a href ="<%=_sUrlReport %>" target="_blank"><img src="../images/IcoType/icon_print.gif" align="absmiddle" />Lập Báo Cáo</a>--%>
                    <asp:LinkButton ID="lnkReport" runat="server" Visible="false" 
                       ><img src="../images/IcoType/spreadsheet-icon.png" align="absmiddle" /> Lập Báo Cáo</asp:LinkButton>
                </td>
            </tr>
	    </table>
    </td>
</tr>


</table>
<%--  </ContentTemplate>
</asp:UpdatePanel>--%>
</asp:Content>