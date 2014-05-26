<%@ Page Language="C#" MasterPageFile="~/master/default_pop.master" AutoEventWireup="true" CodeBehind="HoanThienPhieuNhapChiTiet.aspx.cs" Inherits="QLCV.QLKho.HoanThienPhieuNhapChiTiet" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<script runat="server">
   
     
    public System.Data.SqlClient.SqlDataReader GetSourceThanhPham()
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();// "server='NgoQuangLua-PC'; uid=sa;pwd=sa; Database='QL'; trusted_connection=true;";
        System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);

        string queryString = "SELECT * FROM ThanhPham";
        System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand(queryString, sqlConnection);

        sqlConnection.Open();
        System.Data.SqlClient.SqlDataReader dataReader = sqlCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

        return dataReader;
    }
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

    public System.Data.SqlClient.SqlDataReader GetSourceLoaiNhap()
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLConn"].ToString();// "server='NgoQuangLua-PC'; uid=sa;pwd=sa; Database='QL'; trusted_connection=true;";
        System.Data.SqlClient.SqlConnection sqlConnection = new System.Data.SqlClient.SqlConnection(connectionString);

        string queryString = "select 1 as LoaiNhap_id,N'Nhập thành phẩm' TenLoaiNhap union select 2 as LoaiNhap_id,N'Nhập thiết bị' TenLoaiNhap";
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

		
		<td id="maincontrol_SeperatePane" class="small-separator-pane" title="Nhấn vào đây để ẩn, hiện thanh công cụ - Ctrl+R" width="6" onclick="toggleLeftPane('maincontrol_LeftPane', 'maincontrol_SeperatePane');"><img id="maincontrol_imgToggle" title="Nhấn vào đây để ẩn, hiện thanh công cụ - Ctrl+R" vspace="250" onmouseclick="toggleLeftPane('maincontrol_LeftPane', 'maincontrol_SeperatePane');" onmouseover="this.src='../images/QLCV/menu_ungrippy.gif';" onmouseout="this.src='../images/QLCV/menu_ungrippy.gif';" src="../images/QLCV/menu_ungrippy.gif" alt="" style="border-width:0px;width:6px;" /></td>

		<td id="SpacePane" width="5"><IMG src="../images/QLCV/1x1.gif" width="5" border="0"></td>
		<td id="maincontrol_RightPane" width="100%">  
     
    
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
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="top">
                                                   
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="top">
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                               
                                      <tr align="left">
                                      <td colspan=4 align="left">
                                     <table border="0" style="border-width: 1px; border-color:#000000;border-style: solid; width: 100%;">
                                        <tr>
                                        <td align="right">Số phiếu</td>
                                       <td style="width: 3px"  >
                                             <asp:TextBox ID="txtSoPhieu" runat="server" Enabled=false></asp:TextBox> 
                                       </td>
                                       
                                       <td align="right">Ngày nhập</td>
                                       <td>
                                        <telerik:RadDatePicker ID="dtpNgayNhap" runat="server" Enabled=false
                                                AutoPostBack="True" Skin="Web20" Font-Names="Times New Roman" 
                                                Font-Size="11pt" Culture="Vietnamese (Vietnam)" Width="150px">
                                                <dateinput autopostback="True" labelcssclass="radLabelCss_Web20" skin="Web20">
                                                </dateinput>
                                                <Calendar ID="Calendar2" runat="server" Skin="Web20">
                                                </Calendar>
                                                <datepopupbutton cssclass="radPopupImage_Vista" 
                                                    HoverImageUrl="~/images/Calendar-icon.png" 
                                                    ImageUrl="~/images/Calendar-icon.png" />
                                            </telerik:RadDatePicker>
                                            </td>
                                         <td align="right">Ngày vào kho</td>
                                       <td style="width: 3px"  >
                                       <telerik:RadDatePicker ID="dtpNgayVaoKho" runat="server" Enabled=false
                                                AutoPostBack="True" Skin="Web20" Font-Names="Times New Roman" 
                                                Font-Size="11pt" Culture="Vietnamese (Vietnam)" Width="150px">
                                                <dateinput autopostback="True" labelcssclass="radLabelCss_Web20" skin="Web20">
                                                </dateinput>
                                                <Calendar ID="Calendar2" runat="server" Skin="Web20">
                                                </Calendar>
                                                <datepopupbutton cssclass="radPopupImage_Vista" 
                                                    HoverImageUrl="~/images/Calendar-icon.png" 
                                                    ImageUrl="~/images/Calendar-icon.png" />
                                            </telerik:RadDatePicker>
                                       </td>
                                                                                   
                                        </tr>
                                        <tr>
                                         <td align="right">Người nhận</td>
                                        <td style="width: 3px">
                                          <asp:TextBox ID="txtNguoiNhan" runat="server"  Enabled=false></asp:TextBox>
                                        </td> 
                                                                             
                                            <td align="right">Đối tượng giao hàng</td>
                                            <td>
                                         <asp:TextBox ID="txtDoiTuongGiaoHang" runat="server"  Enabled=false></asp:TextBox> 
                                            </td>
                                            
                                        </tr>
                                     
                                      </table>
                                      </td>
                                      </tr>
                                     
                                      </tr>
                                      <tr>
                                      <td> <br /></td>
                                      </tr>
                                
                                <tr>
                                         <td colspan=4>
                                         <table border="0" style="border-width: 0px; border-style: solid; width: 100%;">
                                         <tr>
                                         <td colspan=2 style="background-color: #009900; font-size:14; " align="left" 
                                                 bgcolor="#339933">CHI TIẾT THIẾT BỊ</td>
                                        <td colspan=2 style="background-color: #009900; " align="right" bgcolor="#339933" >
                                         Thiết bị:<asp:DropDownList ID="cboThietBi"  DataTextField="TenThietBi" DataValueField="ThietBi_Id" OnSelectedIndexChanged="cboThietBi_SelectedIndexChanged" runat="server" Height="20px"  AutoPostBack=true
                                                Width="230px">
                                            </asp:DropDownList>
                                        
                                        </td>
                                         </tr>
                                         </table>
                                         </td>
                                        </tr>
                                        
                                <tr>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                        
                                        <tr>
                                        <td >SL nhập kho</td>
                                        <td style="width: 3px">
                                          <asp:TextBox ID="txtSoLuongNhapKho" runat="server"  Enabled=false></asp:TextBox>
                                        </td> 
                                        <td >SL đã có chi tiết</td>
                                        <td style="width: 3px">
                                          <asp:TextBox ID="txtSoLuongDaCoChiTiet" runat="server"  Enabled=false></asp:TextBox>
                                        </td> 
                                        <td >SL cần nhập chi tiết</td>
                                        <td style="width: 3px">
                                          <asp:TextBox ID="txtSoLuongCanNhapChiTiet" runat="server"  Enabled=false></asp:TextBox>
                                        </td> 
                                        </tr>
                                        
                                       
                                        <tr>
                                       <td align="center" valign="top" colspan=6>
                                       <!--grid nhap chi tiet-->
                                <asp:GridView ID="m_grid" DataKeyNames="ThongTinThietBi_Id" runat="server" 
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
                                          <asp:TemplateField HeaderText="Mã Serial">
                                            <ItemTemplate>
                                              <%#Eval("Ma_Serial")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                               <asp:TextBox ID="txtUMa_Serial" Enabled=true Text='<%#Bind("Ma_Serial")%>' MaxLength="100" runat="server"/>
    				
                                            </EditItemTemplate>
      						                <FooterTemplate>
	    						                <asp:TextBox id="txtIMa_Serial" Enabled=true runat="server" MaxLength="100"></asp:TextBox>
    						                </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>

                                        
                                          <asp:TemplateField HeaderText="Mã MAC">
                                            <ItemTemplate>
                                              <%#Eval("MA_MAC")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                              <asp:TextBox ID="txtUMA_MAC" Text='<%#Bind("MA_MAC")%>' MaxLength="100" runat="server"  />
    					
                                                                  </EditItemTemplate>
      						                <FooterTemplate>
	    						                <asp:TextBox id="txtIMA_MAC" runat="server" MaxLength="100"></asp:TextBox><br />
    						           
	    						                   </FooterTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          
                                          <asp:TemplateField HeaderText="Mã DLK">
                                            <ItemTemplate>
                                              <%#Eval("MA_DLK")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                              <asp:TextBox ID="txtUMA_DLK" Text='<%#Bind("MA_DLK")%>' MaxLength="100" runat="server"  />
    					
                                                                  </EditItemTemplate>
      						                <FooterTemplate>
	    						                <asp:TextBox id="txtIMA_DLK" runat="server" MaxLength="100"></asp:TextBox><br />
    						           
	    						                   </FooterTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          
                                          <asp:TemplateField HeaderText="Mã SLC">
                                            <ItemTemplate>
                                              <%#Eval("MA_SLC")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                              <asp:TextBox ID="txtUMA_SLC" Text='<%#Bind("MA_SLC")%>' MaxLength="100" runat="server"  />
    					
                                                                  </EditItemTemplate>
      						                <FooterTemplate>
	    						                <asp:TextBox id="txtIMA_SLC" runat="server" MaxLength="100"></asp:TextBox><br />
    						           
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
                                    <!--hết grid detail-->
                                       
                                       </td>
                                       </tr>
                                        <tr>
                                        <td colspan=6 align=right>
                                        <asp:Button ID="btnThoat" runat="server" Text="Thoát" OnClick="btnThoat_Click" Width=100px Font-Bold="true"  ForeColor="Teal" />
                                        </td>
                                        </tr>
                                       
                                        <tr>
                                        <td colspan=6> <br /></td>
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
                                                    &nbsp;</tr><tr>
                                                <td>
                                                    <asp:Literal ID="ltlAnnouncement" runat="server"></asp:Literal></td></tr></table></td></tr></table></td></tr></table></ContentTemplate>
                                      </td></tr></table></asp:Content>