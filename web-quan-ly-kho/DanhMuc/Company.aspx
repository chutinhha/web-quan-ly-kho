<%@ Page Language="C#" MasterPageFile="~/master/default.master" AutoEventWireup="true" CodeBehind="Company.aspx.cs" Inherits="QLCV.DanhMuc.Company" %>
<asp:Content ID="Content1" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <link href="../CSS/Modules/MeetingRoom/main.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Modules/MeetingRoom/cms.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Modules/MeetingRoom/qlvt.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Modules/MeetingRoom/calendar.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="../CSS/HC/p_main.css" rel="stylesheet" />


<script src="../js/EnterEvent.js" type="text/javascript"></script>
<script src="../js/calendar_us.js" type="text/javascript"></script>
<script src="../../js/HC/p_main.js" type="text/javascript"></script> 

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>        
        <table width="100%" valign="top" style="height:400px">
            <asp:Literal ID="ltlAuthorityList" runat="server"></asp:Literal>
            <tr>
                <td id="maincontrol_LeftPane" width ="150" vAlign="top" height="100%" class="LeftPanel_BG" style="DISPLAY: block" >
			        <Table width="150" cellpadding="0" cellspacing="0">
				        <tr class="ModuleHead_Admin">
					        <td class="ModuleHead" style=" width:100%"><img src="../images/NhanSu/catalog-icon.png" align="absMiddle" border="0" hspace="3">DANH MỤC
						    </td>
				        </tr>
		            </Table>
		            <Div id="TSK_Left-Menu">
		                <div class="TSK_second-level">
                            <a style='font-weight: normal;' href=''><img src="../images/SelectedRow.gif" border='0' align='absmiddle'> <font color="red"><u><b>Công ty</b></u></font></a>
                        </Div>
                        <div class="TSK_second-level">
                            <a style='font-weight: normal;' href='Department.aspx'><img src="../images/SelectedRow.gif" border='0' align='absmiddle'> <b>Phòng ban</b></a>
                        </Div>
                        <div class="TSK_second-level">
                            <a style='font-weight: normal;' href='Position.aspx'><img src="../images/SelectedRow.gif" border='0' align='absmiddle'> <b>Vị trí CV</b></a>
                        </Div>
                        <div class="TSK_second-level">
                            <a style='font-weight: normal;' href='Labour.aspx'><img src="../images/SelectedRow.gif" border='0' align='absmiddle'> <b>Tính công</b></a>
                        </Div>
                        <div class="TSK_second-level">
                            <a style='font-weight: normal;' href='Common.aspx'><img src="../images/SelectedRow.gif" border='0' align='absmiddle'> <b>Quốc gia</b></a>
                        </Div>
                        <div class="TSK_second-level">
                            <a style='font-weight: normal;' href='Education.aspx'><img src="../images/SelectedRow.gif" border='0' align='absmiddle'> <b>Học vấn</b></a>
                        </Div>
                        <div class="TSK_second-level">
                            <a style='font-weight: normal;' href='Others.aspx'><img src="../images/SelectedRow.gif" border='0' align='absmiddle'> <b>Danh mục khác</b></a>
                        </Div>
		            </Div>
		        </td>
		        <td id="maincontrol_SeperatePane" class="small-separator-pane" title="Nhấn vào đây để ẩn, hiện danh sách Danh Mục" width="1" onclick="toggleLeftPane('maincontrol_LeftPane', 'maincontrol_SeperatePane');"><img id="maincontrol_imgToggle" title="Nhấn vào đây để ẩn, hiện thanh công cụ - Ctrl+R" vspace="250" onmouseclick="toggleLeftPane('maincontrol_LeftPane', 'maincontrol_SeperatePane');" onmouseover="this.src='../images/QLCV/menu_ungrippy.gif';" onmouseout="this.src='../images/QLCV/menu_ungrippy.gif';" src="../images/QLCV/menu_ungrippy.gif" alt="" style="border-width:0px;width:6px;" /></td>
		        <td id="maincontrol_RightPane" width="100%" valign="top" style="height:100%">
		            <table width="100%" style="height:100%">
		                <tr>
		                    <td width="17%" valign="top">
                                <table cellpadding="0" cellspacing="0" width="100%">
				                    <tbody>
				                        <tr class="ModuleHead_BG">
					                        <td class="ModuleHead">
					                            <img src="../images/Company-icon.png" align="absMiddle" border="0">&nbsp;CÔNG TY</td>
				                        </tr>
				                        <tr>
					                        <td nowrap="nowrap">
					                        </td>

				                        </tr>
			                        </tbody>
			                    </table>
			                    <asp:Literal runat="server" id="ltl_Menu"></asp:Literal>            				
		                        <div class="MMT_top-level"><a id="estoreadmin_Linkbutton1" class="NormalBold" href="Company.aspx?C_action=2"><img src="../images/building_add.png" alt="" align="absmiddle" border="0" hspace="2" />&nbsp;Thêm mới</a></div>
                            </td>
                            <td background="../images/bg_vline_dot.gif" height="100%" width="1"><img src="../images/1x1.gif" width="1"></td>
		                    <td valign="top" width="5">&nbsp;</td>
		                    <td height="100%" valign="top" width="83%">
                                 <table class="basic-table" cellpadding="0" cellspacing="1" width="600">
		                                <tbody><tr bgcolor="whitesmoke">
			                                <td class="T1" colspan="3" height="40" nowrap="nowrap" width="100%">
                                                <asp:Label ID="lblTitle" runat="server" Text="Cập nhật công ty"></asp:Label><hr class="hr1">
			                                </td>
		                                </tr>
		                                <tr>
		                                    <td colspan="3">
                                                <asp:Label ID="lblMessage" runat="server" Text="" class="NormalRed"></asp:Label>
                                            </td>
		                                </tr>
		                                <tr bgcolor="whitesmoke">
                                             <td class="NormalBold">Tên Công ty:
			                                </td>
			                                <td class="Normal" colspan="2">
                                                <asp:TextBox ID="txtComName" BackColor="White" runat="server" Width="200px"></asp:TextBox>
			                                </td>
			                            </tr>
			                            <tr bgcolor="whitesmoke">
                                             <td class="NormalBold">Mã công ty<asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label> :
			                                </td>
			                                <td class="Normal" colspan="2">
                                                <asp:TextBox ID="txtComCode" BackColor="White" runat="server" Width="200px"></asp:TextBox>
			                                </td>
			                            </tr>
			                            <tr bgcolor="whitesmoke">
                                             <td class="NormalBold">Công ty trực thuộc:
			                                </td>
			                                <td class="Normal" colspan="2">
                                                <asp:DropDownList ID="ddlBelongCompany" runat="server"
                                                    DataTextField="ComName" DataValueField="Id" Width="200px">
                                                </asp:DropDownList>
			                                </td>
			                            </tr>
			                            <tr bgcolor="whitesmoke">
                                            <td class="NormalBold">Ngày thành lập:
                                            </td>
                                            <td class="normal" colspan="2">
                                                <asp:TextBox ID="txtEstablishDay" runat="server" Width="170px"></asp:TextBox>&nbsp;
				                                <script language="JavaScript" type="text/javascript">

				                                    // whole calendar template can be redefined per individual calendar
				                                    var A_CALTPL = {
				                                        'months': ['Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4', 'Tháng 5', 'Tháng 6', 'Tháng 7', 'Tháng 8', 'Tháng 9', 'Tháng 10', 'Tháng 11', 'Tháng 12'],
				                                        'weekdays': ['Chủ Nhật', 'Thứ Hai', 'Thứ Ba', 'Thứ Tư', 'Thứ Năm', 'Thứ Sáu', 'Thứ Bảy'],
				                                        'yearscroll': true,
				                                        'weekstart': 0,
				                                        'centyear': 70,
				                                        'imgpath': '../images/img_Calendar/'
				                                    }

				                                    new tcal({
				                                        // if referenced by ID then form name is not required
				                                        'controlname': 'ctl00_m_contentBody_txtEstablishDay'
				                                    }, A_CALTPL);
	                                            </script>
	                                            <img src="../images/img_Calendar/cal.gif" id="tcalico_0" onclick="A_TCALS['0'].f_toggle()" class="tcalIcon" alt="Mở Lịch" />
	                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
		                                    <td height="30" colspan="3" bgcolor="whitesmoke">&nbsp;&nbsp;
                                                <asp:Button ID="btnSave" runat="server" Text="Ghi"
                                                    onclick="btnSave_Click" class="FormButton"/>
			                                &nbsp;
                                                <asp:Button ID="btnDelete" runat="server" Text="Xóa" 
                                                    onclick="btnDelete_Click" class="FormButton"/>
                                            &nbsp;
                                                <asp:Button ID="btnBack1" runat="server" Text="Quay lại" class="FormButton"
                                                    OnClientClick="JavaScript: window.history.back(1); return false;"/>
			                                </td>
			                            </tr>
			                        </tbody>
			                    </table>            			        
		                    </td>
		                </tr>
		            </table>
                
		        </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>


</asp:Content>
