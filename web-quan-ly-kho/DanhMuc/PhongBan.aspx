<%@ Page Language="C#" MasterPageFile="~/master/default.master" AutoEventWireup="true" CodeBehind="PhongBan.aspx.cs" Inherits="QLCV.DanhMuc.PhongBan" %>

<script runat="server">
    public System.Data.SqlClient.SqlDataReader GetSourcePhongBan()
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();// "server='NgoQuangLua-PC'; uid=sa;pwd=sa; Database='QL'; trusted_connection=true;";
        System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);

        string queryString = "select a.DonVi_Id PhongBan_Id,upper(a.TenDonVi) tenphongban from DonVi a union select a.PhongBan_Id,a.tenphongban from phongban a";
        System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(queryString, sqlConnection);

        sqlConnection.Open();
        System.Data.SqlClient.SqlDataReader dataReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

        return dataReader;
    }

    public System.Data.SqlClient.SqlDataReader GetSourceDonVi()
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();// "server='NgoQuangLua-PC'; uid=sa;pwd=sa; Database='QL'; trusted_connection=true;";
        System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);

        string queryString = "SELECT * FROM DonVi";
        System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(queryString, sqlConnection);

        sqlConnection.Open();
        System.Data.SqlClient.SqlDataReader dataReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

        return dataReader;
    }

    public System.Data.SqlClient.SqlDataReader GetSourceNhanVien()
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();// "server='NgoQuangLua-PC'; uid=sa;pwd=sa; Database='QL'; trusted_connection=true;";
        System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);

        string queryString = "SELECT * FROM NhanVien";
        System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(queryString, sqlConnection);

        sqlConnection.Open();
        System.Data.SqlClient.SqlDataReader dataReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

        return dataReader;
    }

  

</script>
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
					<td class="ModuleHead" style=" width:100%"><img src="../images/NhanSu/hrm.gif" align="absMiddle" border="0" hspace="3">DANH MỤC PHÒNG BAN
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
                                                        <b style="width: 100%;">Danh mục phòng ban</b>
                                                        
                                                        </font>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" valign="top">                                    
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
                                            <tr>
                                                <td align="left" valign="top">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;" VALIGN="top" >
                                            <tr>
                                            <td style="width:20%;valign:top;" VALIGN="top" >
                                            
                                            
                                            <table border="0" style="width: 100%;height:400px;valign:top;border-width: 1px; border-color:#000000;border-style: solid;" VALIGN="top" >
                                            <tr style="width:100%;valign:top;"VALIGN="top" >
                                            <td style="width:100%;valign:top;"VALIGN="top" >
                                             <asp:TreeView ID="trPhongBan" runat="server" OnSelectedNodeChanged="trPhongBan_NodeChanged" Width=100%>
                                                <DataBindings>
                                                    <asp:TreeNodeBinding DataMember="System.Data.DataRowView" 
                                                        TextField="Text" ValueField="ID" />
                                                </DataBindings>
                                            </asp:TreeView>
                                            
                                            </td>
                                            </tr>
                                            </table>
                                            
                                            
                                            </td>
                                                <td align="left" valign="top" style="width:100%">
                                                    <!--grid-->
                                                  <asp:GridView ID="m_grid" DataKeyNames="PhongBan_Id" runat="server" 
                                CellPadding="4" ForeColor="#333333"
                                AutoGenerateColumns="False" Width="98%" HorizontalAlign="Center"  
                                OnRowDeleting="m_grid_RowDeleting" 
                                OnRowEditing="m_grid_RowEditing"  CssClass="celltext"
                                OnRowCancelingEdit="m_grid_RowCancelingEdit"
                                OnRowUpdating="m_grid_RowUpdating" 
                                ShowFooter="True" 
                                OnRowCommand="m_grid_RowCommand">
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                             <HeaderStyle   CssClass="textTitleGrid"/>
                            <EditRowStyle BackColor="#999999" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                          
                                          
                                        <asp:TemplateField HeaderText="Phòng ban cha">
                                            <ItemTemplate>
                                              <%#Eval("TenPhongBanCha")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                              <asp:DropDownList ID="txtUPhongBanCha" DataSource='<%# GetSourcePhongBan() %>' DataTextField="TenPhongBan" DataValueField="PhongBan_Id" MaxLength="100" runat="server"  /></asp:DropDownList>
    					
                                                                  </EditItemTemplate>
      						                <FooterTemplate>
	    						                <asp:DropDownList id="txtIPhongBanCha" runat="server" DataSource='<%# GetSourcePhongBan() %>' DataTextField="TenPhongBan" DataValueField="PhongBan_Id" MaxLength="100"></asp:DropDownList><br />
    						           
	    						                   </FooterTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          
                                                                                    
                                          
                                          <asp:TemplateField HeaderText="Tên phòng ban">
                                            <ItemTemplate>
                                              <%#Eval("TenPhongBan")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                               <asp:TextBox ID="txtUTenPhongBan" Enabled=true Text='<%#Bind("TenPhongBan")%>' MaxLength="100" runat="server"/>
    				
                                            </EditItemTemplate>
      						                <FooterTemplate>
	    						                <asp:TextBox id="txtITenPhongBan" Enabled=true runat="server" MaxLength="100"></asp:TextBox>
    						                </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>

                                        
                                          <asp:TemplateField HeaderText="Địa chỉ">
                                            <ItemTemplate>
                                              <%#Eval("DiaChi")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                              <asp:TextBox ID="txtUDiaChi" Text='<%#Bind("DiaChi")%>' MaxLength="100" runat="server"  />
    					
                                                                  </EditItemTemplate>
      						                <FooterTemplate>
	    						                <asp:TextBox id="txtIDiaChi" runat="server" MaxLength="100"></asp:TextBox><br />
    						           
	    						                   </FooterTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          
                                          <asp:TemplateField HeaderText="Nhân viên phụ trách">
                                            <ItemTemplate>
                                              <%#Eval("TenDayDu")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                              <asp:DropDownList ID="txtUTenDayDu" DataSource='<%# GetSourceNhanVien() %>' DataTextField="TenDayDu" DataValueField="NhanVien_Id" MaxLength="100" runat="server"  /></asp:DropDownList>
    					
                                                                  </EditItemTemplate>
      						                <FooterTemplate>
	    						                <asp:DropDownList id="txtITenDayDu" runat="server" DataSource='<%# GetSourceNhanVien() %>' DataTextField="TenDayDu" DataValueField="NhanVien_Id" MaxLength="100"></asp:DropDownList><br />
    						           
	    						                   </FooterTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          
                                          <asp:TemplateField HeaderText="Chức danh">
                                            <ItemTemplate>
                                              <%#Eval("ChucDanhPhuTrach")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                              <asp:TextBox ID="txtUChucDanhPhuTrach" Text='<%#Bind("ChucDanhPhuTrach")%>' MaxLength="100" runat="server"  />
    					
                                                                  </EditItemTemplate>
      						                <FooterTemplate>
	    						                <asp:TextBox id="txtIChucDanhPhuTrach" runat="server" MaxLength="100"></asp:TextBox><br />
    						           
	    						                   </FooterTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          
                                       
                                          
                                        <asp:TemplateField>
                                            <HeaderStyle Width="110px"></HeaderStyle>
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

