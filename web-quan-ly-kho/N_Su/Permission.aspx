<%@ Page Language="C#" MasterPageFile="~/master/default.master" AutoEventWireup="true" CodeBehind="Permission.aspx.cs" Inherits="QLCV.N_Su.Permission" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <link href="../CSS/Modules/Message/prms.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Modules/MeetingRoom/main.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Modules/Nhan_Su/hrm.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript">
        function newWindow(file, window) {
            msgWindow = open(file, window, 'resizable=yes,width=520,height=600,scrollbars=yes,top=80,left=400');
            if (msgWindow.opener == null) msgWindow.opener = self;
        }
    </script>

    <table width="100%">
    <tbody>
        <asp:Literal ID="ltlAuthorityList" runat="server"></asp:Literal>
        
		<tr>
		    <td>
    		    <table cellpadding="0" cellspacing="0" height="100%" width="100%">
	                <tbody><tr>
		                <td bgcolor="whitesmoke" nowrap="nowrap" valign="top" width="200px" height="400px">
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
                            <div id="hrmcontrol__ctl0_panelGroupInfo">
                            
                                 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                    
		                            <table class="basic-table" border="0" cellspacing="0" width="100%">
			                            <tbody>
			                                <tr>
				                                <td>
                                                    <asp:Label ID="lblGroupName" runat="server" Text="Danh sách đã phân quyền"  class="T1"></asp:Label>
					                                <hr class="HR"></td>
				                            </tr>
				                        </tbody>
				                    </table>
				                    
				                    <%
                                        if (p_action == 1)
                                        {
                                    %>
                                    <table class="basic-table" width="100%">
				                        <tbody>
				                            <tr>
				                                <td class="normalbold" align="right" width="300px">Công ty:</td>
				                                <td>
                                                    <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="true"
                                                        DataTextField="ComName" DataValueField="Id" Width="300px" 
                                                        onselectedindexchanged="ddlCompany_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
				                            </tr>
		                                </tbody></table>		            	            
                                    <asp:Literal ID="ltlListGroup" runat="server"></asp:Literal><br />
                                    <asp:Literal ID="ltlListEmployee" runat="server"></asp:Literal><br />
                                   
                                    <%}
                                        if (p_action == 2)
                                        {
                                    %>
                                    <table width="700px" cellpadding="4">
                                        <tr>
                                            <td class="normalbold" colspan="2"><u>Lựa chọn Nhóm người dùng: </u>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="normalbold" width="140px">Công ty: </td>
                                            <td class="normal">
                                                <asp:DropDownList ID="ddlCompanySelect1" runat="server" AutoPostBack="true"
                                                    DataTextField="ComName" DataValueField="Id" Width="180px" 
                                                    onselectedindexchanged="ddlCompanySelect1_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="normalbold">
                                                Nhóm người dùng: </td>
                                            <td>
                                                <asp:DropDownList ID="ddlGroup" runat="server" AutoPostBack="true"
                                                    DataTextField="GroupName" DataValueField="GroupId" Width="180px" 
                                                    onselectedindexchanged="ddlGroup_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="800px" cellpadding="4">
                                        <tr>
                                            <td>
                                                <asp:Repeater ID="rptData1" runat="server" OnItemDataBound="rptData_ItemDataBound">
                                                    <HeaderTemplate>
                                                        <table width="850px">
                                                            <tr class="gray">
                                                                <td nowrap="nowrap" width="110px">Tên Quyền</td>
                                                                <td nowrap="nowrap" width="80px" align="center">Phân quyền</td>
                                                                <td nowrap="nowrap" width="290px">Chọn Công ty</td>
                                                                <td nowrap="nowrap">Chọn Phòng/Ban</td>
                                                            </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr id="trItem" runat="server" bgcolor="whitesmoke">
                                                            <td bgcolor="whitesmoke">
                                                                <asp:Label ID="lblAuthorityId" runat="server" Text="" Visible="false"></asp:Label>
                                                                <asp:Label ID="lblModuleId" runat="server" Text="" Visible="false"></asp:Label>
                                                                <asp:Label ID="lbAuthorityName" runat="server" Text="Authority" Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td align="center" bgcolor="whitesmoke">
                                                                <asp:CheckBox ID="chkSelectA" runat="server" />
                                                            </td>
                                                            <td >
                                                                <asp:TextBox ID="txtComName" runat="server" Width="250px"></asp:TextBox>
                                                                <asp:ImageButton ID="ibSelectCompany" runat="server" ImageUrl="../images/MRoom/icon_book.gif" ToolTip="Chọn"/>
                                                            </td> 
                                                            <td >
                                                                <asp:TextBox ID="txtDepartName" runat="server" Width="310px"></asp:TextBox>
                                                                <asp:ImageButton ID="ibSelectDepartment" runat="server" ImageUrl="../images/MRoom/icon_book.gif" ToolTip="Chọn"/>
                                                            </td> 
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater> 
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnGSave" runat="server" Text="Cập nhật" class="FormButton"
                                                    OnClick="btnGSave_Click"/>&nbsp;
                                                <asp:Button ID="btnBack1" runat="server" Text="Quay lại" class="FormButton"
                                                    OnClientClick="JavaScript: window.history.back(1); return false;" />
                                            </td>
                                        </tr>
                                    </table> 
                                    
                                    <script type="text/javascript">
                                        function updateSelectPortals(index, ids, names) {

                                            var eName = 'ctl00_m_contentBody_rptData1_ctl';
                                            if (index.length < 2) eName = eName + '0';
                                            eName = eName + index + '_txtComName';

                                            var txtBox = document.getElementById(eName);

                                            if (txtBox.value == '') {
                                                txtBox.value = names;
                                            } else {
                                                txtBox.value += ', ' + names;
                                            }
                                        }

                                        function updateSelectDepartment(index, ids, names) {

                                            var eName = 'ctl00_m_contentBody_rptData1_ctl';
                                            if (index.length < 2) eName = eName + '0';
                                            eName = eName + index + '_txtDepartName';

                                            var txtBox = document.getElementById(eName);

                                            if (txtBox.value == '') {
                                                txtBox.value = names;
                                            } else {
                                                txtBox.value += ', ' + names;
                                            }
                                        }
                                    </script>
                                    
                                    <%
                                        }
                                        if (p_action == 3)
                                        {
                                    %>
                                    <table width="700px" cellpadding="4">
                                        <tr>
                                            <td class="normalbold" colspan="2"><u>Lựa chọn Người dùng: </u>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="normalbold" width="110px">Công ty: </td>
                                            <td class="normal">
                                                <asp:DropDownList ID="ddlCompanySelect" runat="server" AutoPostBack="true"
                                                    DataTextField="ComName" DataValueField="Id" Width="200px" 
                                                    onselectedindexchanged="ddlCompanySelect_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="normalbold">
                                                Nhân viên: </td>
                                            <td class="normal">
                                                <telerik:RadComboBox ID="cboEmployee" runat="server" DataTextField="EmployeeName" 
                                                    DataValueField="Id" AllowCustomText="True" Width="200px"
                                                    HighlightTemplatedItems="true" DropDownWidth="400px"
                                                    Filter="Contains" Height="150px" MarkFirstMatch="True" 
                                                    onselectedindexchanged="cboEmployee_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <HeaderTemplate>
                                                        <table style="width: 390px" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 160px">Họ Tên</td>
                                                                <td style="width: 60px">Mã NV</td>
                                                                <td style="width: 170px">Email</td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <collapseanimation duration="200" type="OutQuint" />
                                                    <ItemTemplate>
                                                        <table cellpadding="0" cellspacing="0" style="width: 390">
                                                            <tr>
                                                                <td style="width: 160px">
                                                                    <%# DataBinder.Eval(Container.DataItem, "EmployeeName")%></td>
                                                                <td style="width: 60px">
                                                                    <%# DataBinder.Eval(Container.DataItem, "EmployeeCode")%></td>
                                                                <td style="width: 170px">
                                                                    <%# DataBinder.Eval(Container.DataItem, "Email")%></td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="800px" cellpadding="4">
                                        <tr id="tr_Authority_Details">
                                            <td>
                                                <asp:Repeater ID="rptData" runat="server" OnItemDataBound="rptData_ItemDataBound">
                                                    <HeaderTemplate>
                                                        <table width="850px">
                                                            <tr class="gray">
                                                                <td nowrap="nowrap" width="110px">Tên Quyền</td>
                                                                <td nowrap="nowrap" width="80px" align="center">Phân quyền</td>
                                                                <td nowrap="nowrap" width="290px">Chọn Công ty</td>
                                                                <td nowrap="nowrap">Chọn Phòng/Ban</td>
                                                            </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr id="trItem" runat="server" bgcolor="whitesmoke">
                                                            <td bgcolor="whitesmoke">
                                                                <asp:Label ID="lblAuthorityId" runat="server" Text="" Visible="false"></asp:Label>
                                                                <asp:Label ID="lblModuleId" runat="server" Text="" Visible="false"></asp:Label>
                                                                <asp:Label ID="lbAuthorityName" runat="server" Text="Authority" Font-Bold="true"></asp:Label>
                                                            </td>
                                                            <td align="center" bgcolor="whitesmoke">
                                                                <asp:CheckBox ID="chkSelectA" runat="server" />
                                                            </td>
                                                            <td >
                                                                <asp:TextBox ID="txtComName" runat="server" Width="250px"></asp:TextBox>
                                                                <asp:ImageButton ID="ibSelectCompany" runat="server" ImageUrl="../images/MRoom/icon_book.gif" ToolTip="Chọn"/>
                                                            </td> 
                                                            <td >
                                                                <asp:TextBox ID="txtDepartName" runat="server" Width="310px"></asp:TextBox>
                                                                <asp:ImageButton ID="ibSelectDepartment" runat="server" ImageUrl="../images/MRoom/icon_book.gif" ToolTip="Chọn"/>
                                                            </td> 
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater> 
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnESave" runat="server" Text="Cập nhật" class="FormButton"
                                                    OnClick="btnESave_Click"/>&nbsp;
                                                <asp:Button ID="btnBack" runat="server" Text="Quay lại" class="FormButton"
                                                    OnClientClick="JavaScript: window.history.back(1); return false;" />
                                            </td>
                                        </tr>
                                    </table> 
                                    
                                    <script type="text/javascript">
                                        function updateSelectPortals(index, ids, names) {

                                            var eName = 'ctl00_m_contentBody_rptData_ctl';
                                            if (index.length < 2) eName = eName + '0';
                                            eName = eName + index + '_txtComName';

                                            var txtBox = document.getElementById(eName);

                                            if (txtBox.value == '') {
                                                txtBox.value = names;
                                            } else {
                                                txtBox.value += ', ' + names;
                                            }
                                        }

                                        function updateSelectDepartment(index, ids, names) {

                                            var eName = 'ctl00_m_contentBody_rptData_ctl';
                                            if (index.length < 2) eName = eName + '0';
                                            eName = eName + index + '_txtDepartName';

                                            var txtBox = document.getElementById(eName);

                                            if (txtBox.value == '') {
                                                txtBox.value = names;
                                            } else {
                                                txtBox.value += ', ' + names;
                                            }
                                        }
                                    </script>
                                    <%
                                        }  %>
				                    </ContentTemplate>
				                </asp:UpdatePanel>
                            </div>
                        </td>
                    </tr>
                    </tbody>
                </table>
		    </td>
		</tr>
	</tbody>
</table>
 
</asp:Content>