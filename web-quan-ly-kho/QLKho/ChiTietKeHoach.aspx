<%@ Page Language="C#" MasterPageFile="~/master/default_pop.master" AutoEventWireup="true" CodeBehind="ChiTietKeHoach.aspx.cs" Inherits="QLCV.QLKho.ChiTietKeHoach" %>
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
                                <tr>
                                        <td colspan=4>THÔNG TỔNG HỢP</td>
                                        </tr>
                                      <tr align="left">
                                      <td colspan=4 align="left">
                                     <table border="0" style="border-width: 1px; border-color:#000000;border-style: solid; width: 100%;">
                                        <tr>
                                        <td align="right">Loại phiếu</td>
                                       <td style="width: 3px"  >
                                             <asp:TextBox ID="txtNhapXuat" runat="server" Enabled=false></asp:TextBox> 
                                       </td>
                                       
                                       <td align="right">Kho</td>
                                       <td>
                                       <asp:TextBox ID="txtKho" runat="server"  Enabled=false></asp:TextBox> 
                                        </tr>
                                        <tr>
                                         <td align="right">Ngày lập</td>
                                        <td style="width: 3px">
                                        <telerik:RadDatePicker ID="dtpNgayLap" runat="server" Enabled=false
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
                                                                             
                                            <td align="right">Người lập</td>
                                            <td>
                                         <asp:TextBox ID="txtNguoiLap" runat="server"  Enabled=false></asp:TextBox> 
                                            </td>
                                            
                                        </tr>
                                     
                                       <tr>
                                       <td align="right">Lý do nhập/xuất</td>
                                           <td colspan=3>
                                           <asp:TextBox ID="txtLyDoNhap" runat="server" Width="80%" Enabled=false></asp:TextBox>
                                           </td>
                                       </tr>
                                       <tr>
                                       <td align="right">Diễn giải</td>
                                       <td colspan=3>
                                       <asp:TextBox ID="txtDienGiai" runat="server" Width="80%" TextMode="MultiLine" Enabled=false></asp:TextBox>
                                       </td>
                                       
                                       </tr>
                                       <tr>
                                       <td align="right">Nơi nhận</td>
                                        <td style="width: 3px">
                                        <asp:TextBox ID="txtNoiNhan" runat="server"  Enabled=false></asp:TextBox> 
                                            </td>
                                       <td align="right">-->Chọn</td>
                                       <td>
                                       <asp:TextBox ID="txtGiaTriNoiNhan" runat="server"  Enabled=false></asp:TextBox> 
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
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                        
                                        <tr>
                                        <td colspan=6>CHI TIẾT SỐ LƯỢNG PHÊ DUYỆT</td>
                                        </tr>
                                        
                                       
                                        <tr>
                                       <td align="center" valign="top" colspan=6>
                                       <!--grid nhap chi tiet-->
                                <asp:GridView ID="m_grid" DataKeyNames="KeHoachNhapXuatChiTiet_id" runat="server" 
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
                                        <asp:TemplateField HeaderText="Loại hạng mục">
                                            <ItemTemplate>
                                              <%#Eval("TenLoaiNhap")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
    					                      <asp:TextBox ID="txtUTenLoaiNhap" Enabled=false Text='<%#Bind("TenLoaiNhap")%>' MaxLength="100" runat="server"/>  
                                                                  </EditItemTemplate>
      						              
                                             <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          
                                          
                                          <asp:TemplateField HeaderText="Hạng mục">
                                            <ItemTemplate>
                                              <%#Eval("TenThanhPham")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
    					                      <asp:TextBox ID="txtUTenThanhPham" Enabled=false Text='<%#Bind("tenthanhpham")%>' MaxLength="100" runat="server"/>    
                                                                  </EditItemTemplate>
      						                
                                             <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          
                                          <asp:TemplateField HeaderText="Số lượng">
                                            <ItemTemplate>
                                              <%#Eval("So_Luong")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                               <asp:TextBox ID="txtUSoLuong" Enabled=true Text='<%#Bind("So_Luong")%>' MaxLength="100" runat="server"/>
    				
                                            </EditItemTemplate>
      						               
                                            <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          
                                       
                                        </Columns>
                                    </asp:GridView>
                                    <!--hết grid detail-->
                                       
                                       </td>
                                       </tr>
                                        <tr>
                                        <td colspan=5 align=right>
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