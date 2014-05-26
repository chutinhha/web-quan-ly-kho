<%@ Page Language="C#" MasterPageFile="~/master/default.master" AutoEventWireup="true" CodeBehind="Catalog_Report.aspx.cs" Inherits="QLCV.PageSystem.Catalog_Report" %>


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
    <table style="width: 100%;height:100% " cellpadding="0" cellspacing="0" border="0">
                    <tr valign="top" style ="width: 100%;">
                        <td align="center" valign = "top">
                            <table border="0" cellpadding="0" cellspacing="0" style="width: 99%;">
                                <tr>
                                    <td style="width: 100%; height: 15px" valign="top" align="center">
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                            <tr>
                                                <td align="center" valign="top">
                                                    <font color="#3b78ae" face="Verdana" size="4">
                                                        <b style="width: 100%; text-align: center;">Danh mục Báo cáo</b>
                                                        </font>
                                                </td>
                                                <td align="center" valign="top">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="top">                                    
                                                    &nbsp;&nbsp;&nbsp; 
                                                </td>
                                                <td align="center" valign="top">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="top">
                                                    <table class="style4" style="width: 100%">
                                                        <tr>
                                                            <td style="width: 84px">
                                                                <asp:Label ID="px" runat="server" Height="19px" 
                                                                    style="text-align: left; font-size: small; font-weight: 700" Text="Module: " 
                                                                    Width="22px"></asp:Label>
                                                                &nbsp;&nbsp;&nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="cboModule" runat="server" DataTextField="ModuleName" 
                                                                    DataValueField="ID" Height="22px" Width="253px" AutoPostBack="True" 
                                                                    onselectedindexchanged="cboModule_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="center" valign="top">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                            <tr>
                                                <td align="center" valign="top">
                                                    <!--grid-->
                                                  <asp:GridView ID="m_grid" DataKeyNames="ID_Report" runat="server" 
                                CellPadding="4" ForeColor="#333333"
                                AutoGenerateColumns="False" Width="100%" HorizontalAlign="Center"  
                                OnRowDeleting="m_grid_RowDeleting" 
                                OnRowEditing="m_grid_RowEditing"  CssClass="celltext"
                                OnRowCancelingEdit="m_grid_RowCancelingEdit"
                                OnRowUpdating="m_grid_RowUpdating" 
                                ShowFooter="True" 
                                OnRowCommand="m_grid_RowCommand" Height="25px">
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                             <HeaderStyle   CssClass="textTitleGrid"/>
                            <EditRowStyle BackColor="#999999" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                          <asp:TemplateField HeaderText="Tên báo cáo">
                                            <ItemTemplate>
                                              <%#Eval("Report_Name")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                               <asp:TextBox ID="txtUName" Width = "100%" Enabled=true Text='<%#Bind("Report_Name")%>'  runat="server"/>
                                              <asp:RequiredFieldValidator ID="RequiredName" runat="server" ControlToValidate="txtUName" Display="Dynamic" ErrorMessage="Nhập tên báo cáo" SetFocusOnError="True"></asp:RequiredFieldValidator>
    				
                                            </EditItemTemplate>
      						                <FooterTemplate>
	    						                <asp:TextBox id="txtIName"  Width = "100%" Enabled=true runat="server"></asp:TextBox>
	    						                 <asp:RequiredFieldValidator ID="RedName" runat="server" ControlToValidate="txtIName" Display="Dynamic" ErrorMessage="Nhập tên báo cao" SetFocusOnError="True"></asp:RequiredFieldValidator>
    						                </FooterTemplate>
                                              <FooterStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                              <HeaderStyle Width="25%" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                          </asp:TemplateField>

                                        
                                          <asp:TemplateField HeaderText="Đường dẫn">
                                            <ItemTemplate>
                                              <%#Eval("File_Path")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                              <asp:TextBox ID="txtUFilePath" Width = "100%" Text='<%#Bind("File_Path")%>' runat="server"  />
                                             <asp:RequiredFieldValidator ID="RequiredDetail" runat="server" ControlToValidate="txtUFilePath" Display="Dynamic" ErrorMessage="Nhập đường dẫn" SetFocusOnError="True"></asp:RequiredFieldValidator>
    					
                                                                  </EditItemTemplate>
      						                <FooterTemplate>
	    						                <asp:TextBox id="txtIFilePath" Width = "100%" runat="server" ></asp:TextBox><br />
	    						                   <asp:RequiredFieldValidator ID="ReDetail" runat="server" ControlToValidate="txtIFilePath" Display="Dynamic" ErrorMessage="Nhập đường dẫn" SetFocusOnError="True"></asp:RequiredFieldValidator>
    						           
	    						                   </FooterTemplate>
                                              <FooterStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                              <HeaderStyle Width="20%" />
                                             <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                          </asp:TemplateField>
                                          
                                          <asp:TemplateField HeaderText="Tên SP">
                                            <ItemTemplate>
                                              <%#Eval("SPName")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                              <asp:TextBox ID="txtUSP" Width = "100%" Text='<%#Bind("SPName")%>' runat="server"  />
                                             <asp:RequiredFieldValidator ID="RequiredSP" runat="server" ControlToValidate="txtUSP" Display="Dynamic" ErrorMessage="Nhập tên SP" SetFocusOnError="True"></asp:RequiredFieldValidator>
    					
                                                                  </EditItemTemplate>
      						                <FooterTemplate>
	    						                <asp:TextBox id="txtISP" Width = "100%" runat="server" ></asp:TextBox><br />
	    						                   <asp:RequiredFieldValidator ID="ReSP" runat="server" ControlToValidate="txtISP" Display="Dynamic" ErrorMessage="Nhập tên SP" SetFocusOnError="True"></asp:RequiredFieldValidator>
    						           
	    						                   </FooterTemplate>
                                              <FooterStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                              <HeaderStyle Width="30%" />
                                             <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                          </asp:TemplateField>
                                          
                                          <asp:TemplateField HeaderText="Mô tả">
                                            <ItemTemplate>
                                              <%#Eval("Note")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                              <asp:TextBox ID="txtUDetail" Width = "100%" Text='<%#Bind("Note")%>'  runat="server"  />
                                            
                                                </EditItemTemplate>
      						                <FooterTemplate>
	    						                <asp:TextBox id="txtIDetail" Width = "100%" runat="server" ></asp:TextBox><br />
	    						                  						           
	    						                   </FooterTemplate>
                                              <FooterStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                              <HeaderStyle Width="20%" />
                                             <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                          </asp:TemplateField>
                                        <asp:TemplateField>
                                            <FooterStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <HeaderStyle Width="5%"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:LinkButton id="cmdEdit" runat="server" CommandName="Edit" CausesValidation="false">sửa</asp:LinkButton>
                                                &nbsp;
                                                <asp:LinkButton id="cmdDelete" runat="server" CommandName="Delete" CausesValidation="false">xoá</asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton id="cmdInsert" runat="server" CommandName="Insert">Thêm mới</asp:LinkButton>
                                            </FooterTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton id="cmdUpdate" runat="server" CommandName="Update" CausesValidation="false">cập 
                                                nhật</asp:LinkButton>&nbsp;
                                                <asp:LinkButton id="cmdCancel" runat="server" CommandName="Cancel" CausesValidation="false">thoát</asp:LinkButton>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <!--button-->
                                    <td valign ="bottom" style ="width:100%; height:28px">
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                            <tr>
                                                <td align="left" style="width: 100%; height: 25px;" valign="top">
                                                    &nbsp;</tr>
                                            <tr>
                                                <td>
                                                    <asp:Literal ID="ltlAnnouncement" runat="server"></asp:Literal>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table> 
         </ContentTemplate>
    </asp:UpdatePanel>
                           	</td>
		</tr>
		</table>
</asp:Content>

