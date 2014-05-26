<%@ Page Language="C#" MasterPageFile="~/master/default.master" AutoEventWireup="true" CodeBehind="KeHoachNhapXuat.aspx.cs" Inherits="QLCV.QLKho.KeHoachNhapXuat" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<script runat="server">
   
   
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

		<td id="maincontrol_LeftPane" width ="180" vAlign="top" height="100%" class="LeftPanel_BG" style="DISPLAY: block" >
			<Table width="180" cellpadding="0" cellspacing="0">
				<tr class="ModuleHead_Admin">
					<td class="ModuleHead" style=" width:100%"><img src="../images/NhanSu/hrm.gif" align="absMiddle" border="0" hspace="3">LẬP KẾ HOẠCH
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
                                                        <b style="width: 100%;">LẬP KẾ HOẠCH NHẬP XUẤT KHO</b>
                                                        
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
                                    <td align=left>
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                                                                  
                                         <table border="0" style="border-width: 0px; border-color:#009900;border-style: solid; width: 80%;">
                                         <tr>
                                         <td colspan=2 style="background-color: #009900; ">
                                          <asp:Label ID="lblTrangThai" runat="server" Font-Bold="True" ForeColor="Blue"></asp:Label>
                                         </td>
                                        <td colspan=2 style="background-color: #009900; " align="right" >
                                        <asp:Button ID="Button2" runat="server" Text="Bỏ qua" OnClick="btnBoQua_Click" Width=100px Font-Bold="true"  ForeColor="Teal" />
                                        </td>
                                         </tr>
                                         </table>
                                         
                                        </tr>
                                        
                                        <tr>
                                        <td colspan=4>NHẬP THÔNG TIN KẾ HOẠCH HOẠCH</td>
                                        </tr>
                                      <tr align="left">
                                      <td colspan=4 align="left">
                                     <table border="0" style="border-width: 1px; border-color:#000000;border-style: solid; width: 80%;">
                                        <tr>
                                        <td align="right">Loại phiếu</td>
                                       <td style="width: 3px">
                                        <asp:DropDownList ID="cboNhapXuat"  AutoPostBack=true DataTextField="Ten_Loai" DataValueField="id_loai" OnSelectedIndexChanged="cboNhapXuat_SelectedIndexChanged" runat="server" Height="20px " 
                                                Width="230px" >
                                            </asp:DropDownList>  
                                       </td>
                                       
                                       <td align="right">Kho</td>
                                       <td><asp:DropDownList ID="cboKho"  DataTextField="TenKho" DataValueField="Kho_id" runat="server" Height="20px" 
                                                Width="230px">
                                            </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                         <td align="right">Ngày lập</td>
                                        <td style="width: 3px">
                                        <telerik:RadDatePicker ID="dtpNgayLap" runat="server"
                                                AutoPostBack="True" Skin="Web20" Font-Names="Times New Roman" 
                                                Font-Size="11pt" Culture="Vietnamese (Vietnam)" Width="150px">
                                                <dateinput  labelcssclass="radLabelCss_Web20" skin="Web20">
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
                                         <asp:DropDownList ID="cboNguoiLap"  DataTextField="TenDayDu" DataValueField="NhanVien_id" runat="server" Height="20px" 
                                                Width="230px" Enabled=false>
                                            </asp:DropDownList>  
                                            </td>
                                            
                                        </tr>
                                     
                                       <tr>
                                       <td align="right">Lý do nhập/xuất</td>
                                           <td colspan=3>
                                           <asp:TextBox ID="txtLyDoNhap" runat="server" Width="76%"></asp:TextBox>
                                           </td>
                                       </tr>
                                       <tr>
                                       <td align="right">Diễn giải</td>
                                       <td colspan=3>
                                       <asp:TextBox ID="txtDienGiai" runat="server" Width="76%" TextMode="MultiLine"></asp:TextBox>
                                       </td>
                                       
                                       </tr>
                                       <tr>
                                       <td align="right">Nơi nhận</td>
                                        <td style="width: 3px">
                                         <asp:DropDownList ID="cboNoiNhan"  DataTextField="TenNoiNhan" DataValueField="NoiNhan_id" AutoPostBack=true OnSelectedIndexChanged="cboNoiNhan_SelectedIndexChanged" runat="server" Height="20px" 
                                                Width="230px">
                                            </asp:DropDownList>  
                                            </td>
                                       <td align="right">-->Chọn</td>
                                       <td>
                                       <telerik:radcombobox id="cboGiaTriNoiNhan"  runat="server"   markfirstmatch="True" DataTextField="TenNoiNhan" DataValueField="NoiNhan_id" width=80%> 
   
                                        </telerik:radcombobox>
                                            
                                            
                                           
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
                                       <td colspan=4>NHẬP CHI TIẾT KẾ HOẠCH</td>
                                       </tr>
                                       <tr align=left>
                                       <td align="left" valign="top" colspan=4>
                                       <!--grid nhap chi tiet-->
                                                  <asp:GridView ID="m_grid" DataKeyNames="KeHoachNhapXuatChiTiet_id" runat="server" 
                                CellPadding="4" ForeColor="#333333"
                                AutoGenerateColumns="False" Width="80%" HorizontalAlign="left"  
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
                                              <asp:DropDownList ID="txtUTenLoaiNhap" DataSource='<%# GetSourceLoaiNhap() %>' DataTextField="TenLoaiNhap" DataValueField="LoaiNhap_Id" MaxLength="100"  Width="150px" runat="server" Enabled=false /></asp:DropDownList>
    					
                                                                  </EditItemTemplate>
      						                <FooterTemplate>
	    						                <asp:DropDownList id="txtITenLoaiNhap" runat="server" DataSource='<%# GetSourceLoaiNhap() %>' DataTextField="TenLoaiNhap" DataValueField="LoaiNhap_Id" MaxLength="100"  Width="150px" AutoPostBack=true OnSelectedIndexChanged="cboTenLoaiNhap_SelectedIndexChanged"></asp:DropDownList><br />
    						           
	    						                   </FooterTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          
                                          
                                          <asp:TemplateField HeaderText="Hạng mục">
                                            <ItemTemplate>
                                              <%#Eval("TenThanhPham")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                              <asp:TextBox ID="txtUTenThanhPham" Text='<%#Bind("TenThanhPham")%>' MaxLength="100" runat="server" Width="200" Enabled=false/></asp:DropDownList>
    					
                                                                  </EditItemTemplate>
      						                <FooterTemplate>
	    						                <asp:DropDownList id="txtITenThanhPham" runat="server"  MaxLength="100" Width="200"></asp:DropDownList><br />
    						           
	    						                   </FooterTemplate>
                                             <ItemStyle HorizontalAlign="Left" />
                                          </asp:TemplateField>
                                          
                                          <asp:TemplateField HeaderText="Số lượng">
                                            <ItemTemplate>
                                              <%#Eval("So_Luong")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                               <asp:TextBox ID="txtUSoLuong" Enabled=true Text='<%#Bind("So_Luong")%>' MaxLength="100" runat="server"/>
    				
                                            </EditItemTemplate>
      						                <FooterTemplate>
	    						                <asp:TextBox id="txtISoLuong" Enabled=true runat="server" MaxLength="100"></asp:TextBox>
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
                                         <td align="left" >
                                         <table border="0" style="border-width: 0px; border-color:#000000;border-style: solid; width: 80%;">
                                         <tr>
                                        <td colspan=4 style="background-color: #009900; " align="right" >
                                        <asp:Button ID="btnGhi" runat="server" Text="Lưu" OnClick="btnGhi_Click" Width=100px Font-Bold="true"  ForeColor="Teal" />
                                        <asp:Button ID="btnBoQua" runat="server" Text="Bỏ qua" OnClick="btnBoQua_Click" Width=100px Font-Bold="true"  ForeColor="Teal" />
                                      
                                        </td>
                                         </tr>
                                         </table>
                                         </td>
                                        </tr>
                                        
                                        
                                        <tr>
                                        <td colspan=4><br /></td>
                                        </tr>
                                        <tr>
                                        <td colspan=6>DANH SÁCH KẾ HOẠCH NHẬP/XUẤT</td>
                                        </tr>
                                        <tr>
                                        <td colspan=6>
                                        <table border="1" cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <tr>
                                        <td colspan=6>
                                         <tr>
                                                <td align="center" valign="top" colspan=6>
                                                <!--grid-->
                                                <asp:GridView ID="GridView1" runat="server"  Caption=""
                                                     AutoGenerateColumns="false"
                                                      BackColor="Snow" BorderColor="DimGray"                                                          
                                                      DataKeyNames="KeHoachNhapXuat_Id" PageSize="8" Width=100% OnRowCommand = "GridView1_RowCommand">
                                                      
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
                                                       <asp:ButtonField CommandName = "Sửa"  Text="Sửa" ButtonType = "Button"/>
                                                       <asp:ButtonField CommandName = "Xóa"  Text="Xóa" ButtonType = "Button" />
                                                       
                                                      </Columns>
                                                    </asp:GridView>
                                                  
                                                </td>
                                            </tr>
                                        
                                        </td>
                                        </tr>
                                        </table>
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