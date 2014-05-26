<%@ Page Language="C#" MasterPageFile="~/master/default.master" AutoEventWireup="true" CodeBehind="XacNhanXuatKho.aspx.cs" Inherits="QLCV.DanhMuc.XacNhanXuatKho" %>

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
					<td class="ModuleHead" style=" width:100%"><img src="../images/NhanSu/hrm.gif" align="absMiddle" border="0" hspace="3">XÁC NHẬN XUẤT KHO
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
                                                        <b style="width: 100%;">Xác nhận xuất kho</b>
                                                        
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
                                         <td colspan=4>
                                         <table border="0" style="border-width: 0px; border-style: solid; width: 100%;">
                                         <tr>
                                         <td colspan=2 style="background-color: #009900; font-size:14; " align="left" 
                                                 bgcolor="#339933">Lựa chọn phiếu xuất</td>
                                        <td colspan=2 style="background-color: #009900; " align="right" bgcolor="#339933" >
                                        <asp:Button ID="btnLocPhieu" runat="server" Text="Lọc phiếu" OnClick="btnLocPhieu_Click" Width=120px Font-Bold="true"  ForeColor="Teal" />
                                        
                                        </td>
                                         </tr>
                                         </table>
                                         </td>
                                        </tr>
                                
                               
                                <tr align=left>
                                <td>
                                <table border="0" style="border-width: 1px; border-color:#000000;border-style: solid; width: 100%;">
                                <tr>
                                <td style="width:15%;" >Tình trạng hoàn thiện</td>
                                <td  align=left style="width:20%;">
                                <asp:DropDownList ID="cboKeHoachNhapXuat"  DataTextField="Code" DataValueField="KeHoachNhapXuat_id" runat="server" Height="20px" 
                                                Width="230px">
                                            </asp:DropDownList>
                                            </td>
                                <td style="width:12%;">
                                            Số phiếu:
                                </td>  
                                <td><asp:TextBox ID="txtSoPhieu" runat="server" ></asp:TextBox> </td>                         
                                </tr>
                                <tr>
                                         <td >Từ ngày</td>
                                        <td style="width: 3px">
                                        <telerik:RadDatePicker ID="dtpTuNgay" runat="server" 
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
                                                                             
                                            <td >Đến ngày</td>
                                            <td>
                                         <telerik:RadDatePicker ID="dtpDenNgay" runat="server" 
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
                                            
                                        </tr>
                                        
                                <tr>
                                        <td >Người xuất</td>
                                       <td style="width: 3px"  >
                                             <asp:TextBox ID="txtNguoiLap" runat="server" Width=100%></asp:TextBox> 
                                       </td>
                                       
                                      
                                        </tr>
                                       
                                     
                                       <tr>
                                       <td>Loại đối tượng giao hàng</td>
                                        <td  align=left style="width:20%;">
                                <asp:DropDownList ID="cboLoaiDoiTuongGiaoHang"  AutoPostBack=true DataTextField="Code" DataValueField="LoaiDoiTuongGiaoHang_id" runat="server" Height="20px"  OnSelectedIndexChanged="cboLoaiDoiTuongGiaoHang_SelectedIndexChanged"
                                                Width="230px">
                                            </asp:DropDownList>
                                            </td>
                                            
                                            <td>Đối tượng giao hàng</td>
                                        <td  align=left >
                                <asp:DropDownList ID="cboDoiTuongGiaoHang"  DataTextField="Code" DataValueField="DoiTuongGiaoHang_id" runat="server" Height="20px" 
                                                Width="230px">
                                            </asp:DropDownList>
                                            </td>
                                       </tr>
                                       
                               
                                </table>
                                </td>
                                </tr>
                            
                                 <tr>
                                         <td colspan=4>
                                         <table border="0" style="border-width: 0px; border-color:#000000;border-style: solid; width: 100%;">
                                         <tr>
                                         <td colspan=2 style="background-color: #009900; font-size:14; " align="left">Lựa chọn phiếu xuất</td>
                                        <td colspan=2 style="background-color: #009900; " align="right" >
                                        <asp:Button ID="Button2" runat="server" Text="Lọc phiếu" OnClick="btnLocPhieu_Click" Width=120px Font-Bold="true"  ForeColor="Teal" />
                                        
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
                                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                                         
                                        
                                       <tr>
                                         <td colspan=4>
                                         <table border="0" style="border-width: 0px; border-color:#000000;border-style: solid; width: 100%;">
                                         <tr>
                                        <td colspan=4 style="background-color: #009900; font-size:14" >
                                        Danh sách phiếu
                                        </td>
                                         </tr>
                                         </table>
                                         </td>
                                        </tr>
                                        
                                       
                                        <tr>
                                       <td align="center" valign="top" colspan=4>
                                       <!--grid nhap chi tiet-->
                            <table border="1" cellpadding="0" cellspacing="0" style="width: 100%;">
                                         <tr>
                                                <td align="center" valign="top" colspan=6>
                                                
                                                <!--grid-->
                                                <asp:GridView ID="GridView1" runat="server"  Caption=""
                                                     AllowPaging="True" AllowSorting="True" AutoGenerateEditButton="false" AutoGenerateColumns="false"
                                                      BackColor="Snow" BorderColor="DimGray"  BorderStyle="Double"  BorderWidth="2"                                                        
                                                      DataKeyNames="PhieuXuat_Id" PageSize="8" Width=100% OnRowCommand = "GridView1_RowCommand">
                                                      
                                                      <Columns>
                                                        <asp:BoundField HeaderText="Số phiếu" DataField="So_Phieu" /> 
                                                        <asp:BoundField HeaderText="Ngày xuất" DataField="Ngay_Nhap" />  
                                                        <asp:BoundField HeaderText="Lý do xuất" DataField="LyDoXuat" />  
                                                        <asp:BoundField HeaderText="Diễn giải" DataField="Dien_Giai" />  
                                                       <asp:ButtonField HeaderText="Sửa chi tiết" CommandName = "SuaChiTiet"  Text="     " ButtonType = "Button"  ItemStyle-HorizontalAlign=Center />
                                                      </Columns>
                                                    </asp:GridView>
                                                  
                                                </td>
                                            </tr>
                                         </table>
                                    <!--hết grid detail-->
                                       
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
                                      

                                     