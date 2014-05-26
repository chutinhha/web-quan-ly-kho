<%@ Page Language="C#" MasterPageFile="~/master/default.master" AutoEventWireup="true" CodeBehind="NhanVien.aspx.cs" Inherits="QLCV.DanhMuc.NhanVien" %>

<script runat="server">
   

     

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
					<td class="ModuleHead" style=" width:100%"><img src="../images/NhanSu/hrm.gif" align="absMiddle" border="0" hspace="3">DANH MỤC NHÂN VIÊN
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
                                                        <b style="width: 100%;">Quản lý nhân viên</b>
                                                        
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
                                        <td>Phòng ban</td>
                                        <td><asp:DropDownList ID="cboPhongBan" DataSource='<%# GetSourcePhongBan() %>'  DataTextField="TenPhongBan" DataValueField="PhongBan_id" runat="server" Height="20px" 
                                                Width="230px">
                                            </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                        <td>Tên</td>
                                        <td><asp:TextBox ID="txtTen" runat="server" Width="231px"></asp:TextBox></td>
                                        <td>Họ</td>
                                        <td><asp:TextBox ID="txtHo" runat="server" Width="231px"></asp:TextBox></td>
                                        <td>Tên đầy đủ</td>
                                        <td><asp:TextBox ID="txtTenDayDu" runat="server" Width="231px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                        <td>Giới tính</td>
                                         <td><asp:DropDownList ID="cboGioiTinh" runat="server" Height="20px" 
                                                Width="100px">
                                                <asp:ListItem Value="1">Nam</asp:ListItem>
                                                <asp:ListItem Value="0">Nữ</asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td>Địa chỉ</td>
                                         <td colspan=3><asp:TextBox ID="txtDiaChi" runat="server" Width=90%></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                        <td>Tel</td>
                                        <td><asp:TextBox ID="txtTel" runat="server" Width="231px"></asp:TextBox></td>
                                        <td>SubTel</td>
                                        <td><asp:TextBox ID="txtSubTel" runat="server" Width="231px"></asp:TextBox></td>
                                        <td>Mobile</td>
                                        <td><asp:TextBox ID="txtMobile" runat="server" Width="231px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                        <td>Email</td>
                                        <td><asp:TextBox ID="txtEmail" runat="server" Width="231px"></asp:TextBox></td>
                                        <td>Other email</td>
                                        <td><asp:TextBox ID="txtOtherEmail" runat="server" Width="231px"></asp:TextBox></td>
                                        <td>Chức danh</td>
                                        <td><asp:TextBox ID="txtChucDanh" runat="server" Width="231px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                        <td colspan=6>
                                        <asp:Button ID="btnGhi" runat="server" Text="Ghi" OnClick="btnGhi_Click" Width=100px Font-Bold="true"  ForeColor="Teal" />
                                        </td>
                                        </tr>
                                            <tr>
                                                <td align="center" valign="top" colspan=6>
                                                <!--grid-->
                                                <asp:GridView ID="GridView1" runat="server"  Caption="Danh sách nhân viên"
                                                     AllowPaging="True" AllowSorting="True" AutoGenerateEditButton="false" AutoGenerateColumns="false"
                                                      BackColor="Snow" BorderColor="DimGray"  BorderStyle="Double"  BorderWidth="2"                                                        
                                                      DataKeyNames="NhanVien_Id" PageSize="8" Width=100% OnRowCommand = "GridView1_RowCommand">
                                                      
                                                      <Columns>
                                                       
                                                                                                                           
                                                        <asp:BoundField HeaderText="Tên đầy đủ" DataField="TenDayDu" />  
                                                        <asp:BoundField HeaderText="Tên phòng" DataField="TenPhongBan" /> 
                                                        <asp:BoundField HeaderText="Tên" DataField="Ten" />  
                                                        <asp:BoundField HeaderText="Ho" DataField="Ho" />  
                                                        <asp:BoundField HeaderText="Tên đầy đủ" DataField="TenDayDu" />  
                                                        <asp:BoundField HeaderText="Địa chỉ" DataField="DiaChi" />  
                                                        <asp:BoundField HeaderText="Tel 1" DataField="Tel" />  
                                                        <asp:BoundField HeaderText="Tel 2" DataField="SubTel" />  
                                                        <asp:BoundField HeaderText="Mobile" DataField="Mobile" />  
                                                        <asp:BoundField HeaderText="HomePhone" DataField="HomePhone" />  
                                                        <asp:BoundField HeaderText="Email 1" DataField="Main_Email" />  
                                                        <asp:BoundField HeaderText="Email 2" DataField="Other_Email" />  
                                                        <asp:BoundField HeaderText="Chức danh" DataField="ChucDanh" />
                                                        <asp:BoundField HeaderText="Giới tính" DataField="GioiTinh" />
                                                       <asp:ButtonField CommandName = "Sửa"  Text="Edit" ButtonType = "Button"/>
                                                       <asp:ButtonField CommandName = "Xóa"  Text="Delete" ButtonType = "Button"/>
                                                       
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