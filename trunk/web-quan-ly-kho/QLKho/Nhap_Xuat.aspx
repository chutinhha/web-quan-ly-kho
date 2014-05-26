<%@ Page Language="C#" MasterPageFile="~/master/default.master" AutoEventWireup="true" CodeBehind="Nhap_Xuat.aspx.cs" Inherits="QLCV.DanhMuc.Nhap_Xuat" %>

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
					<td class="ModuleHead" style=" width:100%"><img src="../images/NhanSu/hrm.gif" align="absMiddle" border="0" hspace="3">NHẬP KHO
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
                                                        <b style="width: 100%;">Nhập kho</b>
                                                        
                                                        </font>
                                                </td>
                                            </tr>
                                           
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
                                            <td align=center>
                                            <asp:Label ID="lblThongBao" runat="server" Height="22px"      
                                                         Font-Bold="True" Font-Names="Arial" ForeColor="Red" Font-Size= "Small"></asp:Label>
                                            </td>
                                            </tr>
                                 <tr>
                                         
                                         <table border="0" style="border-width: 0px; border-color:#000000;border-style: solid; width: 100%;">
                                         <tr>
                                        <td colspan=4 style="background-color: #009900; " align="right" >
                                        <asp:Button ID="Button1" runat="server" Text="Tạo phiếu nhập" OnClick="btnTaoPhieuNhap_Click" Width=120px Font-Bold="true"  ForeColor="Teal" />
                                        
                                        </td>
                                         </tr>
                                         </table>
                                         
                                        </tr>
                                
                                <tr>
                                <td>
                                TIÊU CHÍ TÌM KIẾM
                                </td>
                                </tr>
                                <tr align=left>
                                <td>
                                <table border="0" style="border-width: 1px; border-color:#000000;border-style: solid; width: 100%;">
                                <tr>
                                <td align=right style="width:9%;">Tên người lập</td>
                                <td style="width:20%;"><asp:TextBox ID="txtTenNguoiLap" runat="server" Width="230px"></asp:TextBox> </td>
                                <td align=right>Tên người phê duyệt</td>
                                <td><asp:TextBox ID="txtTenNguoiPheDuyet" runat="server" Width="230px"></asp:TextBox> </td>
                                </tr>
                                <tr>
                                <td style="width:5%;" align=right>Kho</td>
                                        <td style="width:20%;">
                                        <asp:DropDownList ID="cboKho"   DataTextField="TenKho" DataValueField="Kho_Id"  runat="server" Height="20px " 
                                                Width="230px" >
                                            </asp:DropDownList> 
                                        </td>
                                        <td style="width:11%;" align=right>Từ ngày</td>
                                        <td>
                                        <telerik:RadDatePicker ID="dtpTuNgay" runat="server"
                                                 Skin="Web20" Font-Names="Times New Roman" 
                                                Font-Size="11pt" Culture="Vietnamese (Vietnam)" Width="100px">
                                                <dateinput  labelcssclass="radLabelCss_Web20" skin="Web20">
                                                </dateinput>
                                                <Calendar ID="Calendar2" runat="server" Skin="Web20">
                                                </Calendar>
                                                <datepopupbutton cssclass="radPopupImage_Vista" 
                                                    HoverImageUrl="~/images/Calendar-icon.png" 
                                                    ImageUrl="~/images/Calendar-icon.png" />
                                            </telerik:RadDatePicker>
                                            Đến
                                        <telerik:RadDatePicker ID="dtpDenNgay" runat="server"
                                                Skin="Web20" Font-Names="Times New Roman" 
                                                Font-Size="11pt" Culture="Vietnamese (Vietnam)" Width="100px">
                                                <dateinput  labelcssclass="radLabelCss_Web20" skin="Web20">
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
                                
                                            <td colspan=3 align=center>
                                            <asp:Button ID="btnTimKiem" runat="server" Text="Tìm kiếm" OnClick="btnTim_Click" Width=80px Font-Bold="true"  ForeColor="Teal" />
                                </td>                           
                                </tr>
                                
                                </table>
                                </td>
                                </tr>
                                
                                <tr>
                                <td><br /></td>
                                </tr>
                                
                                <tr>
                                <td>
                                THÔNG TIN PHIẾU
                                </td>
                                </tr>
                                
                                <tr align=left>
                                <td>
                                <table border="0" style="border-width: 1px; border-color:#000000;border-style: solid; width: 100%;">
                                                              
                                <tr>
                                        <td width="100px">Loại phiếu</td>
                                       <td style="width: 3px"  >
                                             <asp:TextBox ID="txtNhapXuat" runat="server" Enabled=false></asp:TextBox> 
                                       </td>
                                       
                                       <td width="100px">Kho</td>
                                       <td>
                                       <asp:TextBox ID="txtKho" runat="server"  Enabled=false></asp:TextBox> 
                                        </tr>
                                        <tr>
                                         <td >Ngày lập</td>
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
                                                                             
                                            <td >Người lập</td>
                                            <td>
                                         <asp:TextBox ID="txtNguoiLap" runat="server"  Enabled=false></asp:TextBox> 
                                            </td>
                                            
                                        </tr>
                                     
                                       <tr>
                                       <td >Lý do nhập</td>
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
                                       <td >Loại đối tượng</td>
                                        <td style="width: 3px">
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
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                         
                                        
                                        <tr>
                                        <td colspan=6>THÔNG TIN CHI TIẾT</td>
                                        </tr>
                                        
                                       
                                        <tr>
                                       <td align="center" valign="top" colspan=6>
                                       <!--grid nhap chi tiet-->
                                <asp:GridView ID="m_grid1" DataKeyNames="KeHoachNhapXuatChiTiet_id" runat="server" 
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
                                       
                                        </table>
                                    </td>
                                </tr>
                                
                                
                                <tr>
                                         <td colspan=6>
                                         <table border="0" style="border-width: 0px; border-color:#000000;border-style: solid; width: 100%;">
                                         <tr>
                                        <td colspan=4 style="background-color: #009900; " align="right" >
                                        <asp:Button ID="btnTaoPhieuNhap" runat="server" Text="Tạo phiếu nhập" OnClick="btnTaoPhieuNhap_Click" Width=120px Font-Bold="true"  ForeColor="Teal" /> 
                                        </td>
                                         </tr>
                                         </table>
                                         </td>
                                        </tr>
                                
                                
                                 <tr>
                                        <td colspan=6>DANH SÁCH KẾ HOẠCH NHẬP</td>
                                        </tr>
                                        <tr>
                                        <td colspan=6>
                                        <table border="1" cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <tr>
                                        <td colspan=6>
                                         <tr>
                                                <td align="center" valign="top" colspan=6>
                                                <!--grid-->
                                                <asp:GridView ID="grKeHoachNhapXuat" runat="server"  Caption=""
                                                     AutoGenerateColumns="false"
                                                      BackColor="Snow" BorderColor="DimGray"                                                          
                                                      DataKeyNames="KeHoachNhapXuat_Id" PageSize="8" Width=100% OnRowCommand = "grKeHoachNhapXuat_RowCommand">
                                                      
                                                      <Columns>
                                                       
                                                                                                                           
                                                        <asp:BoundField HeaderText="Code" DataField="Code" />  
                                                        <asp:BoundField HeaderText="Tên kho" DataField="TenKho" /> 
                                                        <asp:BoundField HeaderText="Ngày lập" DataField="NgayLap" />  
                                                        <asp:BoundField HeaderText="Ngày phê duyệt" DataField="NgayPheDuyet" />  
                                                        <asp:BoundField HeaderText="Người lập" DataField="TenDayDu" />  
                                                        <asp:BoundField HeaderText="Người phê duyệt" DataField="TenDayDu1" />  
                                                        <asp:BoundField HeaderText="Tình trạng" DataField="TinhTrang" />  
                                                        <asp:BoundField HeaderText="Lý do nhập" DataField="LyDoNhapXuat" />  
                                                        <asp:BoundField HeaderText="Diễn giải" DataField="Dien_Giai" />  
                                                        <asp:BoundField HeaderText="Nơi nhận" DataField="NoiNhan" />
                                                       <asp:ButtonField CommandName = "Sửa"  Text="Chọn" ButtonType = "Button"/>
                                                       
                                                       
                                                      </Columns>
                                                    </asp:GridView>
                                                  
                                                </td>
                                            </tr>
                                        
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
                                      

                                     