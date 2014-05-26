<%@ Page Language="C#" MasterPageFile="~/master/default.master" AutoEventWireup="true" CodeBehind="Admin_System.aspx.cs" Inherits="QLCV.Permission.Admin_System" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="m_contentBody" Runat="Server">
<script src="../js/PageTab.js" type="text/javascript"></script>

   <script src="../../js/HC/p_main.js" type="text/javascript"></script> 
 
<link type="text/css" href="../CSS/HC/p_main.css" rel="stylesheet" />
<link href="../CSS/Stylesheet.css" rel="stylesheet" type="text/css" />
<link href="../CSS/Modules/MeetingRoom/main.css" rel="stylesheet" type="text/css" />
<link href="../CSS/Modules/Nhan_Su/hrm.css" rel="stylesheet" type="text/css" />

<script src="../js/EnterEvent.js" type="text/javascript"></script>

    <table cellSpacing="0" cellPadding="0" width="100%" border="0">          
	<tr vAlign="top">

		<td id="maincontrol_LeftPane" width ="180" vAlign="top" height="100%" class="LeftPanel_BG" style="DISPLAY: block" >
			<Table width="180" cellpadding="0" cellspacing="0">
				<tr class="ModuleHead_Admin">
					<td class="ModuleHead" style=" width:100%"><img src="../images/NhanSu/hrm.gif" align="absMiddle" border="0" hspace="3">QUẢN TRỊ HỆ THỐNG
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
            <div id="hrmcontrol__ctl0_panelGroupInfo" style="height:400px">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                
		        <table class="basic-table" border="0" cellspacing="0" width="100%">
			        <tbody>
			            <tr><td></td></tr>
			            <tr>
				            <td>
                                <asp:Label ID="lblGroupName" runat="server" Text="Danh sách Admin"  class="T1"></asp:Label>
					            <hr class="HR"></td>
				        </tr>
				    </tbody>
				</table>
				<%
                    if (a_action == 1)
                    {
                %>
                <table class="basic-table" width="100%">
				    <tbody>
				        <tr>
				            <td class="normalbold" align="right" width="300px">Công ty:</td>
				            <td>
                                <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="true"
                                    DataTextField="ComName" DataValueField="Id" Width="250px" 
                                    onselectedindexchanged="ddlCompany_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
				        </tr>
		            </tbody></table>		            	            
                <asp:Literal ID="ltlListAdmin" runat="server"></asp:Literal>
                <%
                    }
                    else
                    {
                        
                %>
                    <table width="700px" cellpadding="4">
                        <tr>
                            <td class="normalbold" width="180px">Quyền Admin hệ thống: 
                            </td>
                            <td><asp:CheckBox ID="chkAdminFull" runat="server" /></td>
                        </tr>
                        <tr>
                            <td class="normalbold">Công ty: </td>
                            <td class="normal">
                                <asp:DropDownList ID="ddlCompanySelect" runat="server" AutoPostBack="true"
                                    DataTextField="ComName" DataValueField="Id" Width="200px" 
                                    onselectedindexchanged="ddlCompanySelect_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="normalbold">
                                Nhân viên: </td>
                            <td class="normal">
                                <telerik:RadComboBox ID="cboEmployee" runat="server" DataTextField="EmployeeName" 
                                    DataValueField="Id" AllowCustomText="True" Width="200px"
                                    HighlightTemplatedItems="true" DropDownWidth="400px"
                                    Filter="Contains" Height="150px" MarkFirstMatch="True" 
                                    onselectedindexchanged="cboEmployee_SelectedIndexChanged"
                                    AutoPostBack="true">
                                    <HeaderTemplate>
                                        <table style="width: 390px" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width: 160px">Họ Tên</td>
                                                <td style="width: 60px">Mã NV</td>
                                                <td style="width: 170px">Email</td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <collapseanimation duration="200" type="OutQuint" />
                                    <ItemTemplate>
                                        <table cellpadding="0" cellspacing="0" style="width: 390">
                                            <tr>
                                                <td style="width: 160px">
                                                    <%# DataBinder.Eval(Container.DataItem, "EmployeeName")%></td>
                                                <td style="width: 60px">
                                                    <%# DataBinder.Eval(Container.DataItem, "EmployeeCode")%></td>
                                                <td style="width: 170px">
                                                    <%# DataBinder.Eval(Container.DataItem, "Email")%></td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                    </table>
                    <table width="700px" cellpadding="4">
                        <tr id="tr_Module_Details">
                            <td>
                                <asp:Repeater ID="rptData" runat="server" OnItemDataBound="rptData_ItemDataBound">
                                    <HeaderTemplate>
                                        <table width="700px">
                                            <tr class="gray">
                                                <td nowrap="nowrap" width="140px">Tên Module</td>
                                                <td nowrap="nowrap" width="100px" align="center">Phân quyền</td>
                                                <td nowrap="nowrap">Chọn công ty</td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr id="trItem" runat="server">
                                            <td bgcolor="whitesmoke">
                                                <asp:Label ID="lblModuleId" runat="server" Text="" Visible="false"></asp:Label>
                                                <asp:Label ID="lblEMId" runat="server" Text="" Visible="false"></asp:Label>
                                                <asp:Label ID="lbModuleName" runat="server" Text="Date" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td align="center" bgcolor="whitesmoke">
                                                <asp:CheckBox ID="chkSelectM" runat="server" />
                                            </td>
                                            <td >
                                                <asp:TextBox ID="txtComName" runat="server" Width="390px"></asp:TextBox>
                                                <asp:HyperLink runat="server" ID="hplSelectCompany" >
                                                    <img src="../images/MRoom/icon_book.gif" align="absMiddle" border="0">Chọn</a>
                                                </asp:HyperLink>
                                            </td> 
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater> 
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnSave" runat="server" Text="Cập nhật" class="FormButton"
                                    OnClick="btnSave_Click"/>&nbsp;
                                <asp:Button ID="btnBack" runat="server" Text="Quay lại" class="FormButton"
                                    OnClientClick="JavaScript: window.history.back(1); return false;" />
                            </td>
                        </tr>
                    </table>                    
                                
                    <script type="text/javascript">
                    function updateSelectPortals(index, ids, names) {
                        var eName = 'ctl00_m_contentBody_rptData_ctl';
                        if(index.length<2) eName=eName+'0';
                        eName=eName+index+'_txtComName';
                        
                        var txtBox = document.getElementById(eName);
                        
                        if (txtBox.value == '') {
                            txtBox.value = names;
                        } else {
                            txtBox.value += ', ' + names;
                        }   	
                    }	
                    
                    function newWindow(file,window) {
                        msgWindow=open(file,window,'resizable=yes,width=520,height=600,scrollbars=yes,top=80,left=400');
                        if (msgWindow.opener == null) msgWindow.opener = self;
                    }
                    
                    function viewModuleDetails() {
                        var trNV, tdCTV;
                        var chkNV = document.getElementById('ctl00_m_contentBody_chkAdminFull');
                        trNV = document.getElementById('tr_Module_Details');
                        
                        if (chkNV.checked) {
                            trNV.style.display='none';
                        } else {
                            trNV.style.display='';
                        }
                    }

                    </script>  
    <%
                    }	     
                %>
                
				</ContentTemplate>
            </asp:UpdatePanel>
            </div>
        </td>
    </tr>
    
</table>

</asp:Content>