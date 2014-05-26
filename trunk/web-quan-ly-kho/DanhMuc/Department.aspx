<%@ Page Language="C#" MasterPageFile="~/master/default.master" AutoEventWireup="true" CodeBehind="Department.aspx.cs" Inherits="QLCV.DanhMuc.Department" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<%@ Register src="../controls/uscMsgBox.ascx" tagname="uscMsgBox" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="m_contentBody" Runat="Server">
    <link href="../CSS/Modules/MeetingRoom/main.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Modules/MeetingRoom/cms.css" rel="stylesheet" type="text/css" />
    <link type="text/css" href="../CSS/HC/p_main.css" rel="stylesheet" />
    
<script src="../js/EnterEvent.js" type="text/javascript"></script>
<script src="../../js/HC/p_main.js" type="text/javascript"></script> 
 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table width="100%" valign="top" style="height:500px">
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
                            <a style='font-weight: normal;' href='Company.aspx'><img src="../images/SelectedRow.gif" border='0' align='absmiddle'> <b>Công ty</b></a>
                        </Div>
                        <div class="TSK_second-level">
                            <a style='font-weight: normal;' href=''><img src="../images/SelectedRow.gif" border='0' align='absmiddle'> <font color="red"><u><b>Phòng ban</b></u></font></a>
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
		        <td id="maincontrol_RightPane" width="100%" valign="top">
		        
		        <%
                    if (dAction == 0)
                    {
		        %>
		            <table class="basic-table" width="100%">
		                <tr>
		                    <td class="T1" colspan="2">
		                        <asp:Label ID="lb_TitleDepartment" runat="server" Text="Danh sách Phòng/Ban" Font-Bold="True"></asp:Label><hr class="hr1">
		                    </td>
		                </tr>
		                <tr>
		                    <td class="T2" width="80px"><u>Công ty: </u></td>
		                    <td>
		                        <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="true"
                                    DataTextField="ComName" DataValueField="Id"
                                    Width="300px" onselectedindexchanged="ddlCompany_SelectedIndexChanged">
                                </asp:DropDownList>
		                    </td>
		                </tr>
		            </table>
    
                    <table class="basic-table" cellspacing="1" align="Center" rules="all" Border="0px" border="1" id="hrmcontrol__ctl0_dgDepartments" style="width:98%;">
		                <tr class="dark" style="height:1px;">

			                <td class="NormalBold" style="width:30px;">STT</td>
			                <td>Phòng/ Ban/ Trung Tâm</td>
			                <td style="width:130px;" align="center">QĐ thành lập</td>
			                <td class="NormalBold" nowrap="nowrap" style="width:90px;" align="center">Ngày thành lập</td>
			                <td style="width:150px;" align="center">Người ký</td>
			                <td style="width:150px;" align="center">Trưởng bộ phận</td>
		                </tr>
                        <asp:Literal ID="ltl_ListDepartment" runat="server"></asp:Literal>
	                </table>
	                <TABLE cellSpacing="0" cellPadding="2" width="100%" align="center">
		                <TR>
			                <TD height="30">
                                <asp:Button ID="btnBack" runat="server" Text="Quay về" class="formButton"
                                    OnClientClick="JavaScript: window.history.back(1); return false;"/>&nbsp;
                                <asp:Button ID="btnAddDepartment" runat="server" Text=" Thêm Phòng/Ban " 
                                    class="formButton" onclick="btnAddDepartment_Click"/>
				            </TD>
		                </TR>
	                </TABLE>
	            <%}
                else
                {   
                %>	
    
                    <table width="100%">
                        <tr>
                            <td align="left" valign="top">                                
                                <table width="750px" cellspacing="0" class="basic-table">
                                    <tr>
                                        <td colspan="4"  height="40" nowrap="nowrap">
                                            <table width="100%">
                                                <tr>
                                                    <td class="T1"><asp:Label ID="Label13" runat="server" Text="Cập nhật Thông tin" Font-Bold="True"></asp:Label></td>
                                                    <td align="right">
                                                        <asp:Button ID="Button1" runat="server" Text="Quay lại" class="formButton"
                                                            OnClientClick="JavaScript: window.history.back(1); return false;"/>&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                            
                                            
                                            <hr class="hr1"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:Label runat="server" ID="ltlAnnouncement" CssClass="NormalRed" Font-Bold="true"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="NormalBold" width="220px">
                                            Tên Phòng/Ban<font color="red">*</font>: 
                                        </td>
                                        <td class="NormalBold" width="100px">
                                            Thứ tự<font color="red">*</font>: 
                                        </td>
                                        <td class="NormalBold" style="width: 123px">
                                            Đang hoạt động<font color="red">(!)</font>: 
                                        </td>
                                        <td class="NormalBold" width="160px">
                                            Tính lương: 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Normal">
                                            <asp:TextBox ID="txtDepartName" runat="server" Width="180px">
                                            </asp:TextBox>
                                        </td>
                                        <td class="Normal">
                                            <telerik:RadNumericTextBox ID="rtxtOrder" runat="server" MaxLength="2" 
                                                    NumberFormat-DecimalDigits="0" Width="50px">
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td class="Normal" style="width: 123px">
                                            <asp:CheckBox ID="ckbActive" runat="server"/>
                                        </td>
                                        <td class="Normal">
                                            <asp:CheckBox ID="ckbCalSalary" runat="server"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="NormalBold">
                                            Quyết định thành lập: 
                                        </td>
                                        <td class="NormalBold" colspan="2">
                                            Người ký: 
                                        </td>
                                        <td class="NormalBold">
                                            Ngày ký: 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="Normal">
                                            <asp:TextBox ID="txtEstaNumber" runat="server" Width="180px"></asp:TextBox>
                                        </td>
                                        <td class="Normal" colspan="2">
                                            <asp:TextBox ID="txtSigner" runat="server" Width="180px"></asp:TextBox>
                                        </td>
                                        <td class="Normal">
                                            <telerik:RadDatePicker ID="dateEstablish" runat="server"
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
                                        <td class="NormalBold">
                                            Thuộc công ty<font color="red">*</font>: 
                                        </td>
                                        <td class="NormalBold" colspan="2">
                                            Thuộc Phòng/Ban: 
                                        </td>
                                        <td class ="NormalBold">
                                            Địa điểm làm việc:
                                        </td>
                                    </tr>                                    
                                    <tr>
                                        <td class="Normal" >
                                            <asp:DropDownList ID="ddlBelongCompany" runat="server" AutoPostBack="true"
                                                DataTextField="ComName" DataValueField="Id" Width="180px" 
                                                onselectedindexchanged="ddlBelongCompany_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="Normal" colspan="2">
                                            <script type="text/javascript">

                                                function nodeClicking(sender, args) {
                                                    var comboBox = $find("<%= rcbDepartment.ClientID %>");

                                                    comboBox.hideDropDown();
                                                }

                                                function StopPropagation(e) {
                                                    if (!e) {
                                                        e = window.event;
                                                    }

                                                    e.cancelBubble = true;
                                                }

                                                function OnClientDropDownOpenedHandler(sender, eventArgs) {
                                                    var tree = sender.get_items().getItem(0).findControl("rtrDepartment");
                                                    var selectedNode = tree.get_selectedNode();
                                                    if (selectedNode) {
                                                        selectedNode.scrollIntoView();
                                                    }
                                                }
                                            </script>

                                            <telerik:RadComboBox ID="rcbDepartment" runat="server" 
                                                CollapseAnimation-Type="None" ExpandAnimation-Type="None" 
                                                OnClientDropDownOpened="OnClientDropDownOpenedHandler" 
                                                Style="vertical-align: middle;" Width="180px">
                                                <ItemTemplate>
                                                    <div ID="div1">
                                                        <asp:UpdatePanel ID="upTreeView" runat="server">
                                                            <ContentTemplate>
                                                                <telerik:RadTreeView ID="rtrDepartment" runat="server" Height="140px" 
                                                                    OnClientNodeClicking="nodeClicking" OnNodeClick="rtrDepartment_NodeClick" 
                                                                    Width="100%">
                                                                </telerik:RadTreeView>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="rtrDepartment" EventName="NodeClick" />
                                                                <%--<asp:PostBackTrigger ControlID="rtrDepartment" />--%>
                                                            </Triggers>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </ItemTemplate>
                                                <Items>
                                                    <telerik:RadComboBoxItem Text="" />
                                                </Items>
                                            </telerik:RadComboBox>

                                            <script type="text/javascript">

                                                var div1 = document.getElementById("div1");
                                                div1.onclick = StopPropagation;
                                            </script>
                                        </td>
                                        <td class="Normal">
                                            <asp:TextBox ID="txtSeatPosition" runat="server" Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="NormalBold">
                                            Trưởng phòng: 
                                        </td>
                                        <td class="NormalBold" colspan="2">
                                            Hành chính: 
                                        </td>
                                    </tr> 
                                    <tr>
                                        <td class="Normal">
                                            <telerik:RadComboBox ID="cboLeader" runat="server" DataTextField="EmployeeName" 
                                                DataValueField="Id" AllowCustomText="True" Width="180px"
                                                HighlightTemplatedItems="true" DropDownWidth="360px"
                                                Filter="Contains" Height="250px" MarkFirstMatch="True">
                                                <HeaderTemplate>
                                                    <table style="width: 350px" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 140px">Họ Tên</td>
                                                            <td style="width: 60px">Mã NV</td>
                                                            <td style="width: 150px">Ngày Sinh</td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <collapseanimation duration="200" type="OutQuint" />
                                                <ItemTemplate>
                                                    <table cellpadding="0" cellspacing="0" style="width: 350">
                                                        <tr>
                                                            <td style="width: 140px">
                                                                <%# DataBinder.Eval(Container.DataItem, "EmployeeName")%></td>
                                                            <td style="width: 60px">
                                                                <%# DataBinder.Eval(Container.DataItem, "EmployeeCode")%></td>
                                                            <td style="width: 150px">
                                                                <%# DataBinder.Eval(Container.DataItem, "Birthday")%></td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td class="Normal" colspan="2">
                                            <telerik:RadComboBox ID="cboAdministrator" DataTextField="EmployeeName" 
                                                DataValueField="Id" AllowCustomText="True" runat="server"
                                                HighlightTemplatedItems="true" DropDownWidth="360px" Width="180px"
                                                Filter="Contains" Height="250px" MarkFirstMatch="True">
                                                <collapseanimation duration="200" type="OutQuint" />
                                                 <HeaderTemplate>
                                                    <table style="width: 350px" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td style="width: 140px">Họ Tên</td>
                                                            <td style="width: 60px">Mã NV</td>
                                                            <td style="width: 150px">Ngày Sinh</td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table cellpadding="0" cellspacing="0" style="width: 350">
                                                        <tr>
                                                            <td style="width: 140px">
                                                                <%# DataBinder.Eval(Container.DataItem, "EmployeeName")%></td>
                                                            <td style="width: 60px">
                                                                <%# DataBinder.Eval(Container.DataItem, "EmployeeCode")%></td>
                                                            <td style="width: 150px">
                                                                <%# DataBinder.Eval(Container.DataItem, "Birthday")%></td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="NormalBold" colspan="3">Nhiệm vụ: </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" class="Normal">
                                            <asp:GridView ID="m_grid" DataKeyNames="Id" runat="server"
                                                CellPadding="4" ForeColor="#333333" 
                                                AutoGenerateColumns="False" Width="742px" 
                                                OnRowDeleting="m_grid_RowDeleting" OnRowEditing="m_grid_RowEditing"
                                                OnRowCancelingEdit="m_grid_RowCancelingEdit" OnRowUpdating="m_grid_RowUpdating" 
                                                ShowFooter="True" OnRowCommand="m_grid_RowCommand">
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle Font-Size="10pt" BackColor="#5D7B9D"/>
                                                <EditRowStyle BackColor="#999999" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Nhiệm Vụ">
                                                        <ItemTemplate>
                                                            <%#Eval("Describle")%>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtUDescrible" Text='<%#Bind("Describle")%>'  runat="server"/>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox id="txtIDescrible" runat="server" Width="100%"></asp:TextBox><br />
                                                        </FooterTemplate>
                                                        <FooterStyle Width="270px" />
                                                        <HeaderStyle Width="270px" ForeColor="White"/>
                                                        <ItemStyle HorizontalAlign="Left" Width="270px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Thứ tự">
                                                        <ItemTemplate>
                                                            <%#Eval("Order")%>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtUOrder" Text='<%#Bind("Order")%>' MaxLength="10" runat="server"  />
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox id="txtIOrder" runat="server" MaxLength="10"></asp:TextBox><br />
                                                        </FooterTemplate>
                                                        <FooterStyle Width="60px" />
                                                        <HeaderStyle Width="60px" ForeColor="White"/>
                                                        <ItemStyle HorizontalAlign="Left" Width="60px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Ghi Chú">
                                                        <ItemTemplate>
                                                            <%#Eval("Note")%>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtUNote" Text='<%#Bind("Note")%>' runat="server"  />
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox id="txtINote" runat="server"></asp:TextBox><br />
                                                        </FooterTemplate>
                                                        <FooterStyle Width="170px" />
                                                        <HeaderStyle Width="170px" ForeColor="White"/>
                                                        <ItemStyle HorizontalAlign="Left" Width="170px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <FooterStyle Width="70px" />
                                                        <HeaderStyle Width="70px"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="iBEdit" runat="server" CommandName="Edit" ImageUrl="~/images/Edit.gif" CausesValidation="false"/>
                                                            <asp:ImageButton ID="iBDelete" runat="server" CommandName="Delete" ImageUrl="~/images/Delete.gif" CausesValidation="false"/>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:ImageButton ID="iBInsert" runat="server" CommandName="Insert" ImageUrl="~/images/Insert.gif" CausesValidation="false" />
                                                        </FooterTemplate>
                                                        <EditItemTemplate>
                                                            <asp:ImageButton ID="iBUpdate" runat="server" CommandName="Update" ImageUrl="~/images/Update.gif" CausesValidation="false"/>
                                                            <asp:ImageButton ID="iBCancel" runat="server" CommandName="Cancel" ImageUrl="~/images/Cancel.gif" CausesValidation="false"/>
                                                        </EditItemTemplate>
                                                        <ItemStyle Width="70px" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="NormalBold" colspan="3">Danh hiệu: </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" class="Normal">
                                            <asp:ListView ID="lvHonourName" runat="server" DataKeyNames="Id"
                                                onitemediting="lvHonourName_ItemEditing" 
                                                onitemcanceling="lvHonourName_ItemCanceling"
                                                onitemdeleting="lvHonourName_ItemDeleting"
                                                onitemupdating="lvHonourName_ItemUpdating"
                                                InsertItemPosition="LastItem"
                                                oniteminserting="lvHonourName_ItemInserting">
                                                <LayoutTemplate>
                                                    <table border="0" cellpadding="4" width="742px">
                                                      <tr style="background-color:#c46200">
                                                       <th align="center" width="600px"><asp:Label ID="lbTitleHonourName" runat="server" Font-Size="10pt" Font-Bold="true" ForeColor="White">Nội 
                                                           dung</asp:Label></th>
                                                       <th></th>
                                                      </tr>
                                                      <tr id="itemPlaceholder" runat="server"></tr>
                                                     </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td align="left"><asp:Label runat="server" ID="lbHonourName"><%#Eval("Content")%></asp:Label></td>
                                                        <td>
                                                            <asp:ImageButton ID="iBLEdit" runat="server" CommandName="Edit" ImageUrl="~/images/Edit.gif" CausesValidation="false" CommandArgument='<%#Bind("Id") %>'/>
                                                            <asp:ImageButton ID="iBLDelete" runat="server" CommandName="Delete" ImageUrl="~/images/Delete.gif" CausesValidation="false" CommandArgument='<%#Bind("Id") %>'/>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr style="background-color:#E5E5FE">
                                                        <td align="left"><asp:Label runat="server" ID="lbHonourName"><%#Eval("Content")%></asp:Label></td>
                                                        <td>
                                                            <asp:ImageButton ID="iBLEdit" runat="server" CommandName="Edit" ImageUrl="~/images/Edit.gif" CausesValidation="false" CommandArgument='<%#Bind("Id") %>'/>
                                                            <asp:ImageButton ID="iBLDelete" runat="server" CommandName="Delete" ImageUrl="~/images/Delete.gif" CausesValidation="false" CommandArgument='<%#Bind("Id") %>'/>
                                                        </td>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                                <EditItemTemplate>
                                                    <tr style="background-color:#ffc184">
                                                        <td align="left"><asp:TextBox runat="server" ID="txtUHonourName" Text='<%#Bind("Content")%>' Width="100%"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RhonourName" runat="server" ControlToValidate="txtUHonourName" Display="Dynamic" ErrorMessage="Nhập danh hieu" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </td>
                                                        <td>
                                                            <asp:ImageButton ID="iBLUpdate" runat="server" CommandName="Update" ImageUrl="~/images/Update.gif" CausesValidation="false"/>
                                                            <asp:ImageButton ID="iBLCancel" runat="server" CommandName="Cancel" ImageUrl="~/images/Cancel.gif" CausesValidation="false"/>
                                                        </td>
                                                    </tr>
                                                </EditItemTemplate>
                                                <InsertItemTemplate>
                                                    <tr style="background-color:#ffc184">
                                                        <td align="left"><asp:TextBox runat="server" ID="txtIHonourName" Width="100%"></asp:TextBox></td>
                                                        <td>
                                                            <asp:ImageButton ID="iBLInsert" runat="server" CommandName="Insert" ImageUrl="~/images/Insert.gif" CausesValidation="false" />
                                                        </td>
                                                    </tr>
                                                </InsertItemTemplate>
                                            </asp:ListView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="width: 100%" align="center">
                                            <asp:Button ID="btnUpdate" runat="server" Text="Ghi" Width="60px" 
                                                onclick="btnUpdate_Click" CssClass="FormButton"/>
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btnDelete" runat="server" Text="Xóa" Width="60px" 
                                                onclick="btnDelete_Click" CssClass="FormButton"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" class="NormalBold">
                                            <small><font color="red">(!) </font><u>Ghi chú:</u> nếu Phòng/Ban không còn hoạt động thì <font color="red">BỎ CHỌN "Đang hoạt động" </font>, nếu <font color="red">chọn "Xóa"</font> sẽ xóa vĩnh viễn thông tin Phòng/Ban trong hệ thống.</small>
                                        </td>
                                    </tr>
                                </table>
                            </td>  
                            <td style="width:2%"></td>
                        </tr>
                    </table>
                <%} %>
                </td>
            </tr>
        </table>
      
    </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>