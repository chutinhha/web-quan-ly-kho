<%@ Page Language="C#" MasterPageFile="~/master/default.master" AutoEventWireup="true" CodeBehind="AccList.aspx.cs" Inherits="QLCV.PageSystem.AccList" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxGridView" tagprefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.2, Version=11.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="m_contentBody" Runat="Server">
<script src="../js/PageTab.js" type="text/javascript"></script>
  <script type="text/javascript">
        function newWindow12(file, window) {
            msgWindow = open(file, window, 'resizable=yes,width=800,height=400,scrollbars=yes,top=80,left=400');
            if (msgWindow.opener == null) msgWindow.opener = self;
        }
          function refreshGrid1() {
             CallbackPanel.PerformCallback();
           }
            function OnGridDoubleClick(index) {

            gridAccount.GetRowValues(index, 'User_Id', OnGetRowValues);
        }
        function OnGetRowValues(values) {
           
            msgWindow = open("AddAccount.aspx?UserID=" + values, window, 'resizable=no,width=800,height=400,scrollbars=yes,top=80,left=200');
            if (msgWindow.opener == null) msgWindow.opener = self;
        
        }
    </script>
   <script src="../../js/HC/p_main.js" type="text/javascript"></script> 
 
<link type="text/css" href="../CSS/HC/p_main.css" rel="stylesheet" />
<link href="../CSS/Stylesheet.css" rel="stylesheet" type="text/css" />
<link href="../CSS/Modules/MeetingRoom/main.css" rel="stylesheet" type="text/css" />
<link href="../CSS/Modules/Nhan_Su/hrm.css" rel="stylesheet" type="text/css" />

<script src="../js/EnterEvent.js" type="text/javascript"></script>

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
            <div id="hrmcontrol__ctl0_panelGroupInfo" style="height:400px">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
          
				    <table class="style4">
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                              <asp:Panel runat="server" ID="pnSalary" ScrollBars="None">
                           <dx:ASPxRoundPanel ID="ASPxRoundPanel2" runat="server" HeaderText="Danh sách tài khoản"
            Width="100%">
          
            <PanelCollection>
           
                <dx:PanelContent ID="PanelContent2" runat="server">
                    <dx:ASPxCallbackPanel runat="server" ID="ASPxCallbackPanel1" Height="300px" ClientInstanceName="CallbackPanel">
                        <ClientSideEvents EndCallback="OnEndCallback"></ClientSideEvents>
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent3" runat="server">
                                <dx:ASPxGridView ID="gridAccount" runat="server" Width="100%" 
                                    AutoGenerateColumns="False" KeyFieldName="User_Id"  ClientInstanceName  ="gridAccount">
                                     <ClientSideEvents    RowDblClick="function(s, e) {
	OnGridDoubleClick(e.visibleIndex);
}"  ColumnSorting="function(s, e) {
     e.cancel = true;
}"/>
      <SettingsBehavior ColumnResizeMode="NextColumn" AllowSelectByRowClick="True" />
             <SettingsPager Mode="ShowAllRecords">
             </SettingsPager>
        <Settings ShowHorizontalScrollBar="True" VerticalScrollableHeight="270" 
                 ShowVerticalScrollBar="True" />
                                    <Columns>
                                        <dx:GridViewDataTextColumn Caption="Tên đăng nhập" FieldName="Username" 
                                            VisibleIndex="0" Width="10%">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Họ tên" FieldName="FullName" 
                                            VisibleIndex="1" Width="15%">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Địa chỉ" FieldName="Address" 
                                            VisibleIndex="2" Width="40%">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Email" FieldName="Email" VisibleIndex="3" 
                                            Width="13%">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Điện thoại" FieldName="Mobile" 
                                            VisibleIndex="4" Width="12%">
                                        </dx:GridViewDataTextColumn>
                                         <dx:GridViewDataTextColumn Caption="Trạng thái" FieldName="State" 
                                            VisibleIndex="4" Width="5%">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Loại" FieldName="Category" VisibleIndex="5" 
                                            Width="5%">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                </dx:ASPxGridView>
                                </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxCallbackPanel>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxRoundPanel>
          </asp:Panel>
           <table width="100%">
            <tr>
            <td>
                <input id="btnAdd" style="width: 73px; height: 36px" type="button"  onclick = "newWindow12('AddAccount.aspx','window2');"
                    value="Thêm mới" /></td>
            </tr>
            </table>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
          
				</ContentTemplate>
            </asp:UpdatePanel>
           
            </div>
        </td>
    </tr>
    
</table>

</asp:Content>