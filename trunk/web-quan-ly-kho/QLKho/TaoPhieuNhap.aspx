<%@ Page Language="C#" MasterPageFile="~/master/default_pop.master" AutoEventWireup="true" CodeBehind="TaoPhieuNhap.aspx.cs" Inherits="QLCV.QLKho.TaoPhieuNhap" %>
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
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                
                                
                                
                                        <tr>
                                <td>
                                THÔNG TIN PHIẾU
                                </td>
                                </tr>
                                
                                <tr align=left>
                                <td>
                                <table border="0" style="border-width: 1px; border-color:#000000;border-style: solid; width: 100%;">
                                                              
                                <tr>
                                        <td style="width: 12%" >Kho</td>
                                       <td  style="width: 20%" >
                                              <asp:TextBox ID="txtKho" runat="server"  Enabled=false></asp:TextBox> 
                                       </td>
                                       
                                       <td style="width: 12%" ></td>
                                       <td>
                                      
                                        </tr>
                                        <tr>
                                         <td >Ngày lập</td>
                                        <td style="width: 12%">
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
                                                                             
                                            <td >Người lập</td>
                                            <td>
                                         <asp:TextBox ID="txtNguoiLap" runat="server"  Enabled=false></asp:TextBox> 
                                            </td>
                                            
                                        </tr>
                                     
                                       <tr>
                                       <td >Lý do nhập/xuất</td>
                                           <td colspan=3>
                                           <asp:TextBox ID="txtLyDoNhapXuat1" runat="server" Width="80%" Enabled=false></asp:TextBox>
                                           </td>
                                       </tr>
                                       <tr>
                                       <td >Diễn giải</td>
                                       <td colspan=3>
                                       <asp:TextBox ID="txtDienGiai1" runat="server" Width="80%" TextMode="MultiLine" Enabled=false></asp:TextBox>
                                       </td>
                                       
                                       </tr>
                                       <tr>
                                       <td >Nơi nhận</td>
                                        <td style="width: 12%">
                                        <asp:TextBox ID="txtNoiNhan" runat="server"  Enabled=false></asp:TextBox> 
                                            </td>
                                       <td >-->Chọn</td>
                                       <td>
                                       <asp:TextBox ID="txtGiaTriNoiNhan" runat="server"  Enabled=false></asp:TextBox> 
                                            </td>
                                       </tr>
                               
                                </table>
                                </td>
                                </tr>
                                
                                
                                 <tr>
                                <td><br /></td>
                                </tr>
                                
                                
                                
                                        <tr>
                                        <td colspan=4>
                                        TẠO PHIẾU NHẬP
                                        </td>
                                        </tr>
                                       <td colspan=4>
                                        <table border="0" style="border-width: 1px; border-color:#000000;border-style: solid; width: 100%;">
                                        <tr>
                                        
                                        <td style="width:12%;">Kiểu nhập</td>
                                        <td style="width:20%;">
                                        <asp:DropDownList ID="cboMaLoaiNhap"   DataTextField="TenLoaiNhapXuat" DataValueField="AliasName" runat="server" Height="20px" 
                                                Width="230px">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width:12%;">Số phiếu</td>
                                        <td><asp:TextBox ID="txtSoPhieu" runat="server" Width="200px" Enabled=false></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                        <td>Ngày nhập</td>
                                        <td>
                                                                                      
                                           <telerik:RadDatePicker ID="dtpNgayNhapXuat" runat="server"
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
                                            &nbsp;</td>
                                        </tr>
                                         <tr>
                                        <td>Lý do nhập</td>
                                         <td colspan=3><asp:TextBox ID="txtLyDoNhap" runat="server" Width=80%></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                        <td>Diễn giải</td>
                                        <td colspan=3><asp:TextBox ID="txtDienGiai" runat="server" Width="80%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                        <td>Dự án dành riêng</td>
                                         <td>
                                          <asp:DropDownList ID="cboDuAn"  DataTextField="TenDuAn" DataValueField="DuAn_Id" runat="server" Height="20px" 
                                                Width="230px">
                                            </asp:DropDownList>
                                            </td>
                                        
                                        
                                        
                                        </tr>
                                        <tr>
                                        <td>Nhân viên mua</td>
                                        <td>
                                        <asp:DropDownList ID="cboNhanVienNhapXuat"  DataTextField="TenDayDu" DataValueField="NhanVien_Id" runat="server" Height="20px" 
                                                Width="230px">
                                            </asp:DropDownList>
                                        </td>
                                         <td>Nhân viên đối tác</td>
                                         <td ><asp:TextBox ID="txtNhanVienDoiTac" runat="server" Width="230px"></asp:TextBox></td>
                                         
                                        </tr>
                                        <tr>
                                        <td>Chứng từ:</td><td><asp:FileUpload id="FileUpload1" runat="server" /></td>
                                        </tr>
                                        <tr>
                                        <td colspan=4 align=right>
                                        <asp:Label ID="lblThongBao" runat="server" Height="22px"      
                                                         Font-Bold="True" Font-Names="Arial" ForeColor="Blue" Font-Size= "Small"></asp:Label>
                                        <asp:Button ID="btnGhi" runat="server" Text="Tạo phiếu nhập" OnClick="btnGhi_Click" Width=120px Font-Bold="true"  ForeColor="Teal" />
                                        <asp:Button ID="btnHoanThanh" runat="server" Text="Hoàn thành" OnClick="btnHoanThanh_Click" Width=120px Font-Bold="true"  ForeColor="Teal" />
                                        <asp:Button ID="btnBoQua" runat="server" Text="Bỏ qua" OnClick="btnBoQua_Click" Width=100px Font-Bold="true"  ForeColor="Teal" />
                                        <asp:Button ID="btnHuyKQ" runat="server" Text="Hủy kết quả" OnClick="btnHuyKQ_Click" Width=120px Font-Bold="true"  ForeColor="Teal" />
                                        
                                        </td>
                                        </tr>
                                        </table>
                                       </td>
                                       <tr>
                                       <td colspan=4><br /></td>
                                       </tr>
                                       <tr>
                                       <td colspan=4>
                                       THÊM CHI TIẾT PHIẾU NHẬP
                                       </td>
                                       </tr>
                                       
                                         <tr>
                                                                                                
                                                <td align="center" valign="top" colspan=4>
                                                <!--grid nhap chi tiet-->
                                                  <asp:GridView ID="m_grid" DataKeyNames="KeHoachNhapXuatChiTiet_Id" runat="server" 
                                CellPadding="4" ForeColor="#333333"
                                AutoGenerateColumns="False" Width="100%" HorizontalAlign="Center"  
                                OnRowDeleting="m_grid_RowDeleting" 
                                OnRowEditing="m_grid_RowEditing"  CssClass="celltext"
                                OnRowCancelingEdit="m_grid_RowCancelingEdit"
                                OnRowUpdating="m_grid_RowUpdating" 
                                OnRowCommand="m_grid_RowCommand">
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                             <HeaderStyle   CssClass="textTitleGrid"/>
                            <EditRowStyle BackColor="#999999" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                          <asp:TemplateField HeaderText="Hạng mục">
                                            <ItemTemplate>
                                              <%#Eval("HangMuc")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
    					                     <asp:TextBox ID="txtUHangMuc" Enabled=false Text='<%#Bind("HangMuc")%>' MaxLength="10" runat="server" Width="150"/>
                                                                  </EditItemTemplate>
      						                
                                             <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          
                                          <asp:TemplateField HeaderText="Số lượng">
                                            <ItemTemplate>
                                              <%#Eval("So_Luong")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                               <asp:TextBox ID="txtUSoLuong" Enabled=false Text='<%#Bind("So_Luong")%>' MaxLength="10" runat="server" Width="50"/>
    				
                                            </EditItemTemplate>
      						               
                                            <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>

                                        
                                          <asp:TemplateField HeaderText="Đơn giá">
                                            <ItemTemplate>
                                              <%#Eval("Don_Gia")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                              <asp:TextBox ID="txtUDonGia" Text='<%#Bind("Don_Gia")%>' MaxLength="100" runat="server"  Width="80"/>
    					
                                                                  </EditItemTemplate>
      						              
                                             <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          
                                          <asp:TemplateField HeaderText="VAT">
                                            <ItemTemplate>
                                              <%#Eval("VAT")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                              <asp:TextBox ID="txtUVAT" Enabled=true Text='<%#Bind("VAT")%>' MaxLength="10" runat="server"  Width="100"/>
    					
                                                                  </EditItemTemplate>
      						               
                                             <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          
                                          <asp:TemplateField HeaderText="Phí khác">
                                            <ItemTemplate>
                                              <%#Eval("PhiCacLoai")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                              <asp:TextBox ID="txtUPhiCacLoai" Text='<%#Bind("PhiCacLoai")%>' MaxLength="100" runat="server"  />
    					
                                                                  </EditItemTemplate>
      						               
                                             <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          
                                           <asp:TemplateField HeaderText="Giảm giá">
                                            <ItemTemplate>
                                              <%#Eval("GiamGia")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                              <asp:TextBox ID="txtUGiamGia" Text='<%#Bind("GiamGia")%>' MaxLength="100" runat="server"  />
    					
                                                                  </EditItemTemplate>
      						                
                                             <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          
                                          <asp:TemplateField HeaderText="Thành tiền">
                                            <ItemTemplate>
                                              <%#Eval("Thanh_Tien")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                              <asp:TextBox ID="txtUThanhTien" Enabled=false Text='<%#Bind("Thanh_Tien")%>' MaxLength="100" runat="server"  />
    					
                                                                  </EditItemTemplate>
      						                
                                             <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          
                                         
                                          
                                        <asp:TemplateField>
                                            <HeaderStyle Width="110px"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:LinkButton id="cmdEdit" runat="server" CommandName="Edit" CausesValidation="false">Nhập chi tiết</asp:LinkButton>
                                                &nbsp;
                                            </ItemTemplate>
                                           
                                            <EditItemTemplate>
                                                <asp:LinkButton id="cmdUpdate" runat="server" CommandName="Update" CausesValidation="false">cập 
                                                nhật</asp:LinkButton>&nbsp;
                                                <asp:LinkButton id="cmdCancel" runat="server" CommandName="Cancel" CausesValidation="false">thoát</asp:LinkButton>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <!--hết grid detail-->
                                                <tr>
                                                <td><br /></td>
                                                </tr>
                                                <tr><td style="background-color: #009900; ">DANH SÁCH PHIẾU NHẬP</td></tr>
                                                <!--grid-->
                                                <asp:GridView ID="GridView1" runat="server"  
                                                     AllowPaging="True" AllowSorting="True" AutoGenerateEditButton="false" AutoGenerateColumns="false"
                                                      BackColor="Snow" BorderColor="DimGray"  BorderStyle="Double"  BorderWidth="2"                                                        
                                                      DataKeyNames="PhieuNhap_Id" PageSize="8" Width=100% OnRowCommand = "GridView1_RowCommand">
                                                      
                                                      <Columns>
                                                       
                                                                                                                           
                                                        <asp:BoundField HeaderText="Kế hoạch" DataField="Code" />  
                                                        <asp:BoundField HeaderText="Tên kho" DataField="TenKho" /> 
                                                        <asp:BoundField HeaderText="Số phiếu" DataField="So_Phieu" />  
                                                        <asp:BoundField HeaderText="Ngày nhập" DataField="Ngay_Nhap" />  
                                                        <asp:BoundField HeaderText="Lý do" DataField="LyDoNhap" />  
                                                        <asp:BoundField HeaderText="Dự án" DataField="DuAn_DanhRieng_id" />  
                                                        <asp:BoundField HeaderText="Người mua" DataField="TenDayDu" />  
                                                        <asp:BoundField HeaderText="Đối tác" DataField="TenDoiTac" />  
                                                        <asp:BoundField HeaderText="Nhân viên đối tác" DataField="TenNhanVien" />  
                                                        <asp:BoundField HeaderText="Nhà cung cấp" DataField="TenNCC" />  
                                                        <asp:BoundField HeaderText="Tổng tiền" DataField="Tong_Tien" />  
                                                        <asp:BoundField HeaderText="Loại nhập xuất" DataField="TenLoaiNhapXuat" />  
                                                        <asp:BoundField HeaderText="Diễn giải" DataField="Dien_Giai" />
                                                        
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
                                                    &nbsp;</tr><tr>
                                                <td>
                                                    <asp:Literal ID="ltlAnnouncement" runat="server"></asp:Literal></td></tr></table></td></tr></table></td></tr></table></ContentTemplate>
                                      </td></tr></table></asp:Content>