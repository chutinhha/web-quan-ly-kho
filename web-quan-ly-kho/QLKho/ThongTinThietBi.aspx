
<%@ Page Language="C#" MasterPageFile="~/master/default.master" AutoEventWireup="true" CodeBehind="ThongTinThietBi.aspx.cs" Inherits="QLCV.QLKho.ThongTinThietBi" %>



<script runat="server">
    public System.Data.SqlClient.SqlDataReader GetSourceThietBi()
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();// "server='NgoQuangLua-PC'; uid=sa;pwd=sa; Database='QL'; trusted_connection=true;";
        System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);

        string queryString = "SELECT * FROM ThietBi";
        System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(queryString, sqlConnection);

        sqlConnection.Open();
        System.Data.SqlClient.SqlDataReader dataReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

        return dataReader;
    }

</script>
<asp:Content ID="Content1" ContentPlaceHolderID="m_contentBody" Runat="Server">

    Mã phiếu nhập:<asp:TextBox ID="txtPhieuNhapID" Enabled=true Text='' MaxLength="200" runat="server" ReadOnly="true"/>
    
    <asp:GridView ID="m_grid" DataKeyNames="ThongTinThietBi_id" runat="server" 
                                CellPadding="4" ForeColor="#333333"
                                AutoGenerateColumns="False" Width="100%" HorizontalAlign="Center"  
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
                                        <asp:TemplateField HeaderText="Thiết bị">
                                            <ItemTemplate>
                                              <%#Eval("TenThietBi")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                              <asp:DropDownList ID="txtUTenThietBi" runat="server" DataSource='<%# GetSourceThietBi() %>' DataTextField="TenThietBi" DataValueField="ThietBi_Id" MaxLength="100" runat="server"  /></asp:DropDownList>
                                             <asp:RequiredFieldValidator ID="RetelquiredDetail" runat="server" ControlToValidate="txtUNhanVienPhuTrach" Display="Dynamic" ErrorMessage="Nhập thiết bị" SetFocusOnError="True"></asp:RequiredFieldValidator>
    					
                                            </EditItemTemplate>
      						                <FooterTemplate>
	    						                <asp:DropDownList id="txtITenThietBi" runat="server" DataSource='<%# GetSourceThietBi() %>' DataTextField="TenThietBi" DataValueField="ThietBi_Id" MaxLength="100"></asp:DropDownList><br />
	    						                   <asp:RequiredFieldValidator ID="RetelDetail" runat="server" ControlToValidate="txtITenThietBi" Display="Dynamic" ErrorMessage="Nhập thiết bị" SetFocusOnError="True"></asp:RequiredFieldValidator>
    						           
	    						                   </FooterTemplate>
	    						           <ItemStyle HorizontalAlign="Left" />
                                           </asp:TemplateField>
                                          
	    						             <asp:TemplateField HeaderText="Serial">
                                            <ItemTemplate>
                                              <%#Eval("Ma_Serial")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                               <asp:TextBox ID="txtUMa_Serial" Enabled=true Text='<%#Bind("Ma_Serial")%>' MaxLength="100" runat="server"/>
                                              <asp:RequiredFieldValidator ID="RequiredName" runat="server" ControlToValidate="txtUMa_Serial" Display="Dynamic" ErrorMessage="Nhập Mã Serial" SetFocusOnError="True"></asp:RequiredFieldValidator>
    				
                                            </EditItemTemplate>
      						                <FooterTemplate>
	    						                <asp:TextBox id="txtIMa_Serial" Enabled=true runat="server" MaxLength="100"></asp:TextBox>
	    						                 <asp:RequiredFieldValidator ID="RedName" runat="server" ControlToValidate="txtIMa_Serial" Display="Dynamic" ErrorMessage="Nhập Mã Serial" SetFocusOnError="True"></asp:RequiredFieldValidator>
    						                </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
	    						                          
                                           
                                            <asp:TemplateField HeaderText="MAC">
                                            <ItemTemplate>
                                              <%#Eval("Ma_MAC")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                               <asp:TextBox ID="txtUMa_MAC" Enabled=true Text='<%#Bind("Ma_MAC")%>' MaxLength="100" runat="server"/>
                                              <asp:RequiredFieldValidator ID="RequiredName1" runat="server" ControlToValidate="txtUMa_MAC" Display="Dynamic" ErrorMessage="Nhập Mã MAC" SetFocusOnError="True"></asp:RequiredFieldValidator>
    				
                                            </EditItemTemplate>
      						                <FooterTemplate>
	    						                <asp:TextBox id="txtIMa_MAC" Enabled=true runat="server" MaxLength="100"></asp:TextBox>
	    						                 <asp:RequiredFieldValidator ID="RedName1" runat="server" ControlToValidate="txtIMa_MAC" Display="Dynamic" ErrorMessage="Nhập Mã MAC" SetFocusOnError="True"></asp:RequiredFieldValidator>
    						                </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          
                                            <asp:TemplateField HeaderText="SLC">
                                            <ItemTemplate>
                                              <%#Eval("Ma_SLC")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                               <asp:TextBox ID="txtUMa_SLC" Enabled=true Text='<%#Bind("Ma_SLC")%>' MaxLength="100" runat="server"/>
                                              <asp:RequiredFieldValidator ID="RequiredName2" runat="server" ControlToValidate="txtUMa_SLC" Display="Dynamic" ErrorMessage="Nhập Mã SLC" SetFocusOnError="True"></asp:RequiredFieldValidator>
    				
                                            </EditItemTemplate>
      						                <FooterTemplate>
	    						                <asp:TextBox id="txtIMa_SLC" Enabled=true runat="server" MaxLength="100"></asp:TextBox>
	    						                 <asp:RequiredFieldValidator ID="RedName2" runat="server" ControlToValidate="txtIMa_SLC" Display="Dynamic" ErrorMessage="Nhập Mã SLC" SetFocusOnError="True"></asp:RequiredFieldValidator>
    						                </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          
                                          
                                          <asp:TemplateField HeaderText="DLK">
                                            <ItemTemplate>
                                              <%#Eval("Ma_DLK")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                               <asp:TextBox ID="txtUMa_DLK" Enabled=true Text='<%#Bind("Ma_DLK")%>' MaxLength="100" runat="server"/>
                                              <asp:RequiredFieldValidator ID="RequiredName3" runat="server" ControlToValidate="txtUMa_DLK" Display="Dynamic" ErrorMessage="Nhập Mã DLK" SetFocusOnError="True"></asp:RequiredFieldValidator>
    				
                                            </EditItemTemplate>
      						                <FooterTemplate>
	    						                <asp:TextBox id="txtIMa_DLK" Enabled=true runat="server" MaxLength="100"></asp:TextBox>
	    						                 <asp:RequiredFieldValidator ID="RedName3" runat="server" ControlToValidate="txtIMa_DLK" Display="Dynamic" ErrorMessage="Nhập Mã DLK" SetFocusOnError="True"></asp:RequiredFieldValidator>
    						                </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          
                                          <asp:TemplateField HeaderText="Diễn giải">
                                            <ItemTemplate>
                                              <%#Eval("Dien_Giai")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                               <asp:TextBox ID="txtUDien_Giai" Enabled=true Text='<%#Bind("Dien_Giai")%>' MaxLength="100" runat="server"/>
                                              <asp:RequiredFieldValidator ID="RequiredName4" runat="server" ControlToValidate="txtUDien_Giai" Display="Dynamic" ErrorMessage="Nhập diễn giải" SetFocusOnError="True"></asp:RequiredFieldValidator>
    				
                                            </EditItemTemplate>
      						                <FooterTemplate>
	    						                <asp:TextBox id="txtIDien_Giai" Enabled=true runat="server" MaxLength="100"></asp:TextBox>
	    						                 <asp:RequiredFieldValidator ID="RedName4" runat="server" ControlToValidate="txtIDien_Giai" Display="Dynamic" ErrorMessage="Nhập diễn giải" SetFocusOnError="True"></asp:RequiredFieldValidator>
    						                </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          
                                          <asp:TemplateField HeaderText="Trạng thái">
                                            <ItemTemplate>
                                              <%#Eval("Trang_Thai")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                               <asp:TextBox ID="txtUTrang_Thai" Enabled=true Text='<%#Bind("Trang_Thai")%>' MaxLength="100" runat="server"/>
                                              <asp:RequiredFieldValidator ID="RequiredName5" runat="server" ControlToValidate="txtUTrang_Thai" Display="Dynamic" ErrorMessage="Nhập trạng thái" SetFocusOnError="True"></asp:RequiredFieldValidator>
    				
                                            </EditItemTemplate>
      						                <FooterTemplate>
	    						                <asp:TextBox id="txtITrang_Thai" Enabled=true runat="server" MaxLength="100"></asp:TextBox>
	    						                 <asp:RequiredFieldValidator ID="RedName5" runat="server" ControlToValidate="txtITrang_Thai" Display="Dynamic" ErrorMessage="Nhập trạng thái" SetFocusOnError="True"></asp:RequiredFieldValidator>
    						                </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          
                                           <asp:TemplateField HeaderText="Tình trạng">
                                            <ItemTemplate>
                                              <%#Eval("Tinh_Trang")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                               <asp:TextBox ID="txtUTinh_Trang" Enabled=true Text='<%#Bind("Tinh_Trang")%>' MaxLength="100" runat="server"/>
                                              <asp:RequiredFieldValidator ID="RequiredName6" runat="server" ControlToValidate="txtUTinh_Trang" Display="Dynamic" ErrorMessage="Nhập tình trạng" SetFocusOnError="True"></asp:RequiredFieldValidator>
    				
                                            </EditItemTemplate>
      						                <FooterTemplate>
	    						                <asp:TextBox id="txtITinh_Trang" Enabled=true runat="server" MaxLength="100"></asp:TextBox>
	    						                 <asp:RequiredFieldValidator ID="RedName6" runat="server" ControlToValidate="txtITinh_Trang" Display="Dynamic" ErrorMessage="Nhập tình trạng" SetFocusOnError="True"></asp:RequiredFieldValidator>
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
</asp:Content>
