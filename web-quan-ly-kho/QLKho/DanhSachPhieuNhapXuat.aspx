<%@ Page Language="C#" MasterPageFile="~/master/default.master" AutoEventWireup="true" CodeBehind="DanhSachPhieuNhapXuat.aspx.cs" Inherits="QLCV.QLKho.DanhSachPhieuNhapXuat" %>
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

		<td id="maincontrol_LeftPane" width ="180" vAlign="top" height="100%" class="LeftPanel_BG" style="DISPLAY: block" >
			<Table width="180" cellpadding="0" cellspacing="0">
				<tr class="ModuleHead_Admin">
					<td class="ModuleHead" style=" width:100%"><img src="../images/NhanSu/hrm.gif" align="absMiddle" border="0" hspace="3">DANH SÁCH NHẬP XUẤT
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
                                                        <b style="width: 100%;">Danh sách phiếu</b>
                                                        
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
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                        <tr>
                                        <td colspan=6>TIÊU CHÍ TÌM KIẾM</td>
                                        </tr>
                                        <tr>
                                        <td colspan=6>
                                        <table border="0" style="border-width: 1px; border-color:#000000;border-style: solid; width: 100%;">
                                        <tr>
                                        <td style="width:12%;">Loại phiếu</td>
                                        <td style="width:15%;">
                                        <asp:DropDownList ID="cboLoaiPhieu"   DataTextField="Ten_Loai" DataValueField="id_loai"  runat="server" Height="20px " 
                                                Width="230px" >
                                            </asp:DropDownList> 
                                        </td>
                                        <td style="width:5%;" align=right>Kho</td>
                                        <td style="width:15%;">
                                        <asp:DropDownList ID="cboKho"   DataTextField="TenKho" DataValueField="Kho_Id"  runat="server" Height="20px " 
                                                Width="230px" >
                                            </asp:DropDownList> 
                                        </td>
                                        <td style="width:10%;" align=right>Ngày nhập/xuất</td>
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
                                        <td>Lý do nhập xuất</td>
                                        <td colspan=5> <asp:TextBox ID="txtLyDoNhap" runat="server" Width="95%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                        <td>Diễn giải</td>
                                        <td colspan=5><asp:TextBox ID="txtDienGiai" runat="server" Width="95%" TextMode="MultiLine"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                       
                                       <td>Nhân viên nhập/xuất</td>
                                       <td>
                                       <asp:DropDownList ID="cboGiaTriNoiNhan"  DataTextField="TenNoiNhan" DataValueField="NoiNhan_id" runat="server" Height="20px" 
                                                Width="230px">
                                            </asp:DropDownList>  
                                            </td>
                                            <td align=left colspan=4>
                                            <asp:Button ID="btnTimKiem" runat="server" Text="Tìm kiếm" OnClick="btnTimKiem_Click" Width=100px Font-Bold="true"  ForeColor="Teal" />
                                            </td>
                                       
                                       </tr>
                                       </table>
                                       </td>
                                       </tr>
                                        <tr><td><br /></td></tr>
                                         <!--Chi tiet ke hoach-->
                                        <tr>
                                        <td colspan=6>KẾT QUẢ TÌM KIẾM</td>
                                        </tr>
                                        <tr>
                                        <td colspan=6>
                                        <table border="1" cellpadding="0" cellspacing="0" style="width: 100%;">
                                           <tr>
                                                <td align="center" valign="top" colspan=6>
                                                
                                                <!--grid-->
                                                <asp:GridView ID="GridView1" runat="server"  Caption=""
                                                     AllowPaging="True" AllowSorting="True" AutoGenerateEditButton="false" AutoGenerateColumns="false"
                                                      BackColor="Snow" BorderColor="DimGray"  BorderStyle="Double"  BorderWidth="2"                                                        
                                                      DataKeyNames="Phieu_Id" PageSize="8" Width=100% OnRowCommand = "GridView1_RowCommand">
                                                      
                                                      <Columns>
                                                       
                                                                                                                           
                                                        <asp:BoundField HeaderText="Số phiếu" DataField="So_Phieu" />  
                                                        <asp:BoundField HeaderText="Ngày nhập/xuất" DataField="NgayNhapXuat" /> 
                                                        <asp:BoundField HeaderText="Người nhập/xuất" DataField="Nguoi" />  
                                                        <asp:BoundField HeaderText="Lý do" DataField="LyDo" />  
                                                        <asp:BoundField HeaderText="Diễn giải" DataField="Dien_Giai" />  
                                                        <asp:BoundField HeaderText="Tổng tiền" DataField="Tong_Tien" />  
                                                        <asp:BoundField HeaderText="Tình trạng" DataField="TinhTrang" />  
                                                       <asp:ButtonField CommandName = "PheDuyet"  Text="Chi Tiết" ButtonType = "Button"/>
                                                        <asp:ButtonField CommandName = "Xoa"  Text="Xóa" ButtonType = "Button"/>
                                                      </Columns>
                                                    </asp:GridView>
                                                  
                                                </td>
                                            </tr>
                                        </table>
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