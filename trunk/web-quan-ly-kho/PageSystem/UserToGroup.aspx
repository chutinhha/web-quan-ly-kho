<%@ Page Language="C#"  MasterPageFile="~/master/default.master" AutoEventWireup="true" CodeBehind="UserToGroup.aspx.cs" Inherits="QLCV.PageSystem.UserToGroup" %>
<%@ Register src="../controls/uscMsgBox.ascx" tagname="uscMsgBox" tagprefix="uc1" %>


<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content  ID="Content1" ContentPlaceHolderID="m_contentBody" Runat="Server" >
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
   <table width="100%">
   <tr>
    <td align="center" valign="top" style="height: 27px">
     <font color="#3b78ae" face="Verdana" size="4">
      <b style="width: 100%;"><span style="font-family: Tahoma; font-size: medium">Gán 
        người dùng cho nhóm</span></b>
                                                        
    </font>
  </td>
   </tr>
   <tr>
   <td align="left" valign="top">
                   <asp:Label ID="Label1" runat="server" Height="22px"      
                Text="Công ty:" Font-Bold="True" Font-Names="Arial" Font-Size= "Small"></asp:Label>
             &nbsp;
                 <asp:DropDownList ID="cboCompany" runat="server" Height="22px" Width="335px" 
                DataTextField ="ComName" DataValueField="ID" AutoPostBack ="true" 
              onselectedindexchanged="cboCompany_SelectedIndexChanged">
                </asp:DropDownList>
                                                </td>
   </tr>
   </table>
    <table style="width: 100%">
        <tr>
            <td style="width: 360px; text-align: center; font-size: small;">
                <b>Phòng ban&nbsp;
                <asp:CheckBox ID="chkNhanVien" runat="server" style="font-size: 9pt" 
                    Text="Toàn bộ nhân viên cấp dưới" />
                </b></td>
            <td style="width: 6px; font-size: small;">
                <b></b></td>
            <td style="width: 431px; text-align: center; font-size: small;">
                <b>Nhân viên</b></td>
            <td style="width: 6px; font-size: small;">
                <b></b></td>
            <td style="text-align: center; font-size: small;">
                <b>Nhóm người dùng</b></td>
        </tr>
        <tr>
            <td style="width: 360px">
                <asp:Panel ID="Panel1" runat="server" BorderColor="#9900FF" BorderStyle="Solid" 
                    BorderWidth="1px" Height="476px" Width="385px">
                    <telerik:RadTreeView ID="trDepartment" Runat="server" Height="100%" 
                        Width="100%" onnodeclick="trDepartment_NodeClick" Skin="Office2007" 
                        Font-Names="Tahoma" Font-Size="9pt">
                        <CollapseAnimation Duration="100" Type="OutQuint" />
                        <ExpandAnimation Duration="100" />
                    </telerik:RadTreeView>
                </asp:Panel>
            </td>
            <td style="width: 6px">
                &nbsp;</td>
            <td style="width: 431px">
                      <telerik:RadGrid ID="gridEmploy" runat="server" GridLines="None" Height="477px" 
                          Skin="Office2007" Width="100%" AutoGenerateColumns="False" 
                          onselectedindexchanged="gridEmploy_SelectedIndexChanged">
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
                                  <telerik:GridBoundColumn DataField="PositionName" HeaderText="Vị trí" 
                                      UniqueName="PositionName">
                                  </telerik:GridBoundColumn>
                              </Columns>
                          </MasterTableView>
                          <HeaderStyle Height="5px" />
                          <ClientSettings EnablePostBackOnRowClick="True">
                              <Selecting AllowRowSelect="True" />
                              <Scrolling AllowScroll="True" />
                          </ClientSettings>
                          <FilterMenu EnableTheming="True">
                              <CollapseAnimation Duration="200" Type="OutQuint" />
                          </FilterMenu>
                      </telerik:RadGrid>
                  </td>
            <td style="width: 6px">
                &nbsp;</td>
            <td>
                      <telerik:RadGrid ID="gridGroup" runat="server" GridLines="None" Height="477px" 
                          Skin="Office2007" Width="100%" AutoGenerateColumns="False"  
                          AllowMultiRowSelection="True" 
                         >
                          <MasterTableView DataKeyNames="GroupID">
                              <RowIndicatorColumn>
                                  <HeaderStyle Width="20px" />
                              </RowIndicatorColumn>
                              <ExpandCollapseColumn>
                                  <HeaderStyle Width="20px" />
                              </ExpandCollapseColumn>
                              <Columns>
                                  <telerik:GridClientSelectColumn  UniqueName="colCheck" > 
                                      <HeaderStyle Width="10px" />
                                  </telerik:GridClientSelectColumn>
                                  <telerik:GridBoundColumn DataField="GroupName" HeaderText="Tên nhóm" 
                                      UniqueName="GroupName">
                                      <HeaderStyle Width="60%" />
                                  </telerik:GridBoundColumn>
                                  <telerik:GridBoundColumn DataField="Detail" HeaderText="Mô tả" 
                                      UniqueName="Detail">
                                  </telerik:GridBoundColumn>
                              </Columns>
                          </MasterTableView>
                          <ClientSettings>
                              <Selecting AllowRowSelect="True" />
                              <Scrolling AllowScroll="True" />
                          </ClientSettings>
                          <FilterMenu EnableTheming="True">
                              <CollapseAnimation Duration="200" Type="OutQuint" />
                          </FilterMenu>
                      </telerik:RadGrid>
                  </td>
        </tr>
        <tr>
            <td style="width: 360px">
                &nbsp;</td>
            <td style="width: 6px">
                &nbsp;</td>
            <td style="width: 431px">
                <asp:Button ID="btnUpdate" runat="server" Height="36px" Text="Cập nhật" 
                    Width="139px" onclick="btnUpdate_Click" />
            </td>
            <td style="width: 6px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
       </ContentTemplate>
    </asp:UpdatePanel>
       </td>
	  </tr>
	</table>
</asp:Content>
