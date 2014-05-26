<%@ Page Language="C#"  MasterPageFile="~/master/default.master" AutoEventWireup="true" CodeBehind="Catalog_Parameter_Report.aspx.cs" Inherits="QLCV.PageSystem.Catalog_Parameter_Report" %>
<%@ Register src="../controls/uscMsgBox.ascx" tagname="uscMsgBox" tagprefix="uc1" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_contentBody" Runat="Server">
 <script src="../js/PageTab.js" type="text/javascript"></script>

   <script src="../../js/HC/p_main.js" type="text/javascript"></script> 
  <link type="text/css" href="../CSS/HC/main.css" rel="stylesheet" />
<link type="text/css" href="../CSS/HC/p_main.css" rel="stylesheet" />
      <table cellSpacing="0" cellPadding="0" width="100%" border="0">          
	<tr vAlign="top">

		<td id="maincontrol_LeftPane" width ="180" vAlign="top" height="100%" class="LeftPanel_BG" style="DISPLAY: block" >
			<Table width="180" cellpadding="0" cellspacing="0">
				<tr class="ModuleHead_Admin">
					<td class="ModuleHead" style=" width:100%"><img src="../images/NhanSu/hrm.gif" align="absMiddle" border="0" hspace="3">QUẢN TRỊ HỆ THỐNG
						</td>

				</tr>
		</Table>
		<Div id="TSK_Left-Menu">
	<%
 
  for (int i = 0; i < _dtFunction.Rows.Count; i++)
  {
      if (_dtFunction.Rows[i]["Parent"].ToString() == "0")
      {
      %>
      <DIV class="TSK_top-level"><IMG hspace="2" src="<%=_dtFunction.Rows[i]["Image_Url"].ToString() %>" align="absMiddle" border="0">
       <%=_dtFunction.Rows[i]["Function_Name"].ToString()%>
							</DIV>
      <%
        }
      else
      {
          %>
           	<div class="TSK_second-level">
            <a style='font-weight: normal;' href='<%=_dtFunction.Rows[i]["Function_Url"].ToString() %>'><img src='<%=_dtFunction.Rows[i]["Image_Url"].ToString() %>' border='0' align='absmiddle'> <%= _dtFunction.Rows[i]["Function_Name"].ToString() %></a>
            </Div>
          <%
        }
  }       

 %>
	</DIV>

		</TD>
		<td id="maincontrol_SeperatePane" class="small-separator-pane" title="Nhấn vào đây để ẩn, hiện thanh công cụ - Ctrl+R" width="6" onclick="toggleLeftPane('maincontrol_LeftPane', 'maincontrol_SeperatePane');"><img id="maincontrol_imgToggle" title="Nhấn vào đây để ẩn, hiện thanh công cụ - Ctrl+R" vspace="250" onmouseclick="toggleLeftPane('maincontrol_LeftPane', 'maincontrol_SeperatePane');" onmouseover="this.src='../images/QLCV/menu_ungrippy.gif';" onmouseout="this.src='../images/QLCV/menu_ungrippy.gif';" src="../images/QLCV/menu_ungrippy.gif" alt="" style="border-width:0px;width:6px;" /></td>

		<td id="SpacePane" width="5"><IMG src="../images/QLCV/1x1.gif" width="5" border="0"></td>
		<td id="maincontrol_RightPane" width="100%">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode ="Conditional"    >
        <ContentTemplate>
        <table width ="100%">
        <tr>
        <td align ="center">
         <asp:Label ID="Label1" runat="server" ForeColor="#003399" 
                                            style="font-weight: 700; font-size: 13pt" 
                Text="Danh mục tham số báo cáo"></asp:Label>
                                     
        </td>
        </tr>
        </table>
            <table style="width: 100%">
                <tr>
                    <td style="width: 50%">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 44px; font-weight: 700;">
                                    Module:</td>
                                <td style="width: 75px">
                                    <asp:DropDownList ID="cboModule" runat="server" AutoPostBack="True" 
                                        DataTextField="ModuleName" DataValueField="ID" Height="23px" 
                                        onselectedindexchanged="cboModule_SelectedIndexChanged" Width="154px">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 61px">
                                    <b>Báo cáo</b>:</td>
                                <td>
                                    <telerik:RadComboBox ID="cboReport" Runat="server" Height="100px" 
                                        Skin="Vista" Width="291px"  EnableVirtualScrolling="True"
                                        onselectedindexchanged="cboReport_SelectedIndexChanged" 
                                        AutoPostBack="True">
                                        <CollapseAnimation Duration="200" Type="OutQuint" />
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 13px">
                        &nbsp;</td>
                    <td style="width: 58%">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="font-size: medium; font-weight: bold">Chi tiết</span><br />
                        <asp:Label ID="Label2" runat="server" BorderColor="#6600FF" BorderStyle="Solid" 
                            BorderWidth="1px" Height="0px" Width="100%"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 38%; height: 405px;">
                        <telerik:RadGrid ID="grParameter" runat="server" AutoGenerateColumns="False" 
                            GridLines="None" Height="406px" Skin="Office2007" Width="100%" 
                            onselectedindexchanged="grParameter_SelectedIndexChanged">
                            <MasterTableView DataKeyNames="Parameter_Code">
                                <RowIndicatorColumn>
                                    <HeaderStyle Width="20px" />
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn>
                                    <HeaderStyle Width="20px" />
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="Parameter_Code" HeaderText="Mã tham số" 
                                        UniqueName="Parameter_Code">
                                        <HeaderStyle Width="40%" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Parameter_Name" HeaderText="Tên tham số" 
                                        UniqueName="Parameter_Name">
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
                    <td style="width: 13px; height: 405px;">
                        </td>
                    <td style="width: 58%; height: 405px;">
                        <table style="width: 100%; height: 410px;">
                            <tr>
                                <td style="height: 31px; width: 108px">
                                    Mã tham số:</td>
                                <td style="height: 31px" class="style3">
                                    <asp:TextBox ID="txtParaCode" runat="server" Width="60%"></asp:TextBox>
                                    </td>
                            </tr>
                            <tr>
                                <td style="height: 36px; width: 108px">
                                    Tên tham số:</td>
                                <td style="height: 36px" class="style2">
                                    <asp:TextBox ID="txtParaName" runat="server" Width="90%"></asp:TextBox>
                                    </td>
                            </tr>
                            <tr>
                                <td style="height: 34px; width: 108px">
                                    Kiểu dữ liệu:</td>
                                <td class="style3">
                                    <telerik:RadComboBox ID="cboDataType" Runat="server" Height="100px" Skin="Vista" 
                                        Width="60%" EnableVirtualScrolling="True">
                                        <CollapseAnimation Duration="200" Type="OutQuint" />
                                    </telerik:RadComboBox>
                                    </td>
                            </tr>
                            <tr>
                                <td style="width: 108px; height: 39px;">
                                    Loại điều khiển:</td>
                                <td class="style3" style="height: 39px">
                                    <telerik:RadComboBox ID="cboDataControl" Runat="server" Height="100px" 
                                        Skin="Vista" Width="60%">
                                        <CollapseAnimation Duration="200" Type="OutQuint" />
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 108px; height: 13px;">
                                </td>
                                <td class="style2" style="height: 13px">
                                    <asp:CheckBox ID="chkPara" runat="server" AutoPostBack="True" 
                                        oncheckedchanged="chkPara_CheckedChanged" Text="Là Stored Procedures" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 108px; height: 115px;">
                                    <asp:Label ID="lblName" runat="server" Text="Câu truy vấn:"></asp:Label>
                                </td>
                                <td style="height: 115px">
                                    <asp:TextBox ID="txtSQL" runat="server" Height="90%" TextMode="MultiLine" 
                                        Width="90%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 31px;">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 106px">
                                                Chiều rộng:</td>
                                            <td style="width: 127px">
                                                <telerik:RadNumericTextBox ID="txtwidth" Runat="server" Height="23px" 
                                                    Skin="Office2007">
                                                    <NumberFormat DecimalDigits="0" />
                                                </telerik:RadNumericTextBox>
                                            </td>
                                            <td style="width: 7px">
                                                &nbsp;</td>
                                            <td style="width: 69px">
                                                Chiều cao:</td>
                                            <td style="width: 126px">
                                                <telerik:RadNumericTextBox ID="txtHeight" Runat="server" Height="23px" 
                                                    Skin="Office2007">
                                                    <NumberFormat DecimalDigits="0" />
                                                </telerik:RadNumericTextBox>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 108px; height: 26px;">
                                    Thứ tự:</td>
                                <td class="style3" style="height: 26px">
                                    <telerik:RadNumericTextBox ID="txtOrder" Runat="server" Height="23px" 
                                        Skin="Office2007">
                                        <NumberFormat DecimalDigits="0" />
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 108px; height: 15px;">
                                    </td>
                                <td class="style2" style="height: 15px">
                                    </td>
                            </tr>
                            <tr>
                                <td style="width: 108px; height: 25px;">
                                    &nbsp;</td>
                                <td class="style2" style="height: 25px">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 109px">
                                                <asp:Button ID="btnAdd" runat="server" Height="31px" onclick="btnAdd_Click" 
                                                    TabIndex="1" Text="Thêm" Width="103px" />
                                            </td>
                                            <td style="width: 119px">
                                                <asp:Button ID="btnEdit" runat="server" Height="31px" onclick="btnEdit_Click" 
                                                    Text="Sửa" Width="111px" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnDelete" runat="server" Height="31px" 
                                                    onclick="btnDelete_Click" Text="Xóa" Width="111px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 40%">
                        &nbsp;</td>
                    <td style="width: 13px">
                        &nbsp;</td>
                    <td style="width: 58%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 40%">
                        &nbsp;</td>
                    <td style="width: 13px">
                        &nbsp;</td>
                    <td style="width: 58%">
                        &nbsp;</td>
                </tr>
            </table>
         

        </ContentTemplate>
    </asp:UpdatePanel>
                             	</td>
		</tr>
		</table>
   </asp:Content>  