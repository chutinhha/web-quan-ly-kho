<%@ Page Language="C#" MasterPageFile="~/master/default.master" AutoEventWireup="true" CodeBehind="Position_Detail.aspx.cs" Inherits="QLCV.DanhMuc.Position_Detail" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="m_contentBody" Runat="Server">

<script type="text/javascript" language="javascript">
function ViewData(Id, Option)
    {
    alert(Id);
        var response;
        Position_Detail.ViewData(Id,Option,GetData_CallBack);   
    }
    
    function GetData_CallBack(response)
    {
        var response=response.value;
        
        if(response=="Empty")
        {
            alert("No Record Found !!!");
        }
        else if(response=='Error')
        {
            alert("An Error occured in accessing the DataBase !!!");
        }
    }

</script>

<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"/>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate>

     <table width="100%" cellpadding="0" cellspacing="0" border="0">
        <tr valign="top">
            <td align="center" style="width: 249px">
                <fieldset>
                    <legend class="textcms"><strong>Danh sách VTCV</strong></legend>
                    
                    
                            <telerik:RadGrid ID="RadGrid1" AutoGenerateColumns="False" runat="server" 
                                onselectedindexchanged="RadGrid1_SelectedIndexChanged" 
                        GridLines="Horizontal" Skin="WebBlue" ForeColor="Black" 
                        HorizontalAlign="Center">
                                <MasterTableView DataKeyNames="Id" BackColor="White" ForeColor="Black" >
<RowIndicatorColumn>
<HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>

<ExpandCollapseColumn>
<HeaderStyle Width="20px"></HeaderStyle>
</ExpandCollapseColumn>
                                 <Columns>
                                    <telerik:GridBoundColumn UniqueName="Description" HeaderText="PositionName" DataField="PositionName" >
                                        <HeaderStyle />
                                        <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    </Columns>
                                </MasterTableView>
                                <SelectedItemStyle BackColor="White" BorderStyle="Double" BorderWidth="2px" />
                                <ClientSettings EnablePostBackOnRowClick="True">
                                    <Selecting AllowRowSelect="True" />
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                </ClientSettings>
                                <FilterMenu EnableTheming="false">
                                    <CollapseAnimation Duration="200" Type="OutQuint" />
                                </FilterMenu>
                            </telerik:RadGrid>                             
                </fieldset>
                
            </td>
            <td>
            <p></p>
                <center><asp:Label runat="server" ID="lbTitle" Font-Bold="True" Font-Size="11pt" 
                        Font-Strikeout="False">Thông tin chi tiết Vị Trí Công Việc</asp:Label></center>
                <p></p>
                <p></p>

                <table ID="Table1" width ="100%">
                    <tr>
                        <td style="width: 95%" align="left">
                            <table align="right">
                                <tr><td >
                                        <asp:Button ID="btnInsert" runat="server" Text="Thêm" 
                                        onclick="btnInsert_Click" Width="70px" Height="30px"/>
                                    &nbsp;&nbsp;
                                        <asp:Button ID="btnUpdate" runat="server" Text="Sửa" Width="70px" 
                                        Height="30px" onclick="btnUpdate_Click"/>
                                    &nbsp;&nbsp;
                                        <asp:Button ID="btnDelete" runat="server" Text="Xóa" Width="70px" 
                                        Height="30px" onclick="btnDelete_Click"/>
                                </td></tr>
                            </table>
                            
                            <p></p>
                            <p></p>
                        </td> 
                        <td style="width:30px"></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="11pt"> Thông tin cơ bản</asp:Label></td>
                    </tr>
                    <tr>
                        <td  colspan="2">
                            <table ID="Table2"  width="100%">
                                <tr>
                                    <td style="width: 15%">
                                        <asp:Label ID="lbPositionName" runat="server" Text="Tên VTCV: " 
                                            Font-Bold="True"></asp:Label>
                                    </td>
                                    <td style="width: 34%" colspan="2">
                                        <asp:TextBox ID="txtPositionName" runat="server" Width="99%" Height="25px" 
                                            BackColor="White"></asp:TextBox>
                                    </td>
                                    <td style="width: 17%" align="center">
                                        <table width="100%">
                                            <tr>
                                                <td style="width: 60%"><asp:Label ID="lblSalaryLevel" runat="server" Text="Bậc lương: " 
                                            Font-Bold="True"></asp:Label></td>
                                                <td style="width: 40%"><asp:TextBox ID="txtSalaryLevel" runat="server" Width="100%" 
                                                        BackColor="White"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                        
                                    </td>
                                    <td style="width: 17%" align="center">
                                        <asp:Label ID="lbBelongName" runat="server" Text="VTCV trực thuộc: " 
                                            Font-Bold="True"></asp:Label>
                                    </td>
                                    <td style="width: 17%">
                                        <telerik:RadComboBox ID="dlBelongName" runat="server" Width="100%"
                                                DataTextField="PositionName"
                                                DataValueField="Id" Skin="Sunset">
<CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                                        </telerik:RadComboBox>
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%">
                                        <asp:Label ID="lbEduMajor" runat="server" Text="Chuyên ngành đào tạo: " 
                                            Font-Bold="True"></asp:Label>
                                    </td>
                                    <td style="width: 17%">
                                        <telerik:RadComboBox ID="dlEduMajor" runat="server" Width="100%"
                                            DataSource="<%#cboEduMajor%>"
                                                DataTextField="MajorName"
                                                DataValueField="Id" Skin="Sunset">
<CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td style="width: 17%" align="center">
                                        <asp:Label ID="lbEduLevel" runat="server" Text="Trình độ Đào Tạo: " 
                                            Font-Bold="True"></asp:Label>
                                    </td>
                                    <td style="width: 17%">
                                        <telerik:RadComboBox ID="dlEduLevel" runat="server" Width="100%"
                                               DataSource="<%#cboEduLevel%>"
                                                DataTextField="LevelName"
                                                DataValueField="Id" Skin="Sunset" >
<CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td style="width: 17%" align="center">
                                        <asp:Label ID="lbEngLevel" runat="server" Text="Trình độ Tiếng Anh: " 
                                            Font-Bold="True"></asp:Label>
                                    </td>
                                    <td style="width: 17%">
                                        <telerik:RadComboBox ID="dlEngLevel" Runat="server" Width="100%" 
                                            Skin="Sunset">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%">
                                        <asp:Label ID="lbITLevel" runat="server" Text="Trình độ Tin học: " 
                                            Font-Bold="True"></asp:Label>
                                    </td>
                                    <td  style="width: 17%">
                                        <asp:TextBox ID="txtITLevel" runat="server" Width="100%" Height="25px" 
                                            BackColor="White"></asp:TextBox>
                                    </td>
                                    <td style="width: 17%" align="center">
                                        <asp:Label ID="lbMLevel" runat="server" Text="Trình độ quản lý: " 
                                            Font-Bold="True"></asp:Label>
                                    </td>
                                    <td style="width: 17%">
                                        <asp:TextBox ID="txtMLevel" runat="server" Width="100%" Height="25px" 
                                            BackColor="White"></asp:TextBox>
                                    </td>
                                    <td style="width: 17%" align="center">
                                        <asp:Label ID="lbPLevel" runat="server" Text="Trình độ chính trị: " 
                                            Font-Bold="True"></asp:Label>
                                    </td>
                                    <td style="width: 17%">
                                        <asp:TextBox ID="txtPLevel" runat="server" Width="100%" Height="25px" 
                                            BackColor="White"></asp:TextBox>
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td style="width: 15%">
                                        <asp:Label ID="lbDescrible" runat="server" Text="Mô tả công việc: " 
                                            Font-Bold="True"></asp:Label>
                                    </td>
                                    <td colspan="2" style="width: 34%">
                                        <asp:TextBox TextMode="MultiLine" Rows="4" runat="server"  ID="txtDescrible" 
                                            Width="100%" BackColor="White"></asp:TextBox>
                                    </td>
                                    <td style="width: 9%" align="right">
                                        <asp:Label ID="lbSkills" runat="server" Text="Các kỹ năng:    " Font-Bold="True"></asp:Label>
                                    </td>
                                    <td colspan="2" style="width: 34%">
                                        <asp:TextBox TextMode="MultiLine" Rows="4" runat="server"  ID="txtSkills" 
                                            Width="100%" BackColor="White"></asp:TextBox>
                                    </td>
                                </tr>
                                 
                                
                                <tr>
                                    
                                    <td style="width: 15%">
                                        <asp:Label ID="lbOther" runat="server" Text="Yêu cầu khác: " Font-Bold="True"></asp:Label>
                                    </td>
                                    <td colspan="2" style="width: 34%">
                                        <asp:TextBox TextMode="MultiLine" Rows="4" runat="server"  ID="txtOther" 
                                            Width="100%" BackColor="White"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <p></p>
                <p></p>
                <table ID="Table3"  width="100%">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="11pt"> Nhiệm vụ của Vị Trí Công Việc</asp:Label></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <table ID="Table4" border="0" cellpadding="0" cellspacing="0" style="width: 90%;">
                            <tr>
                                <td align="center" valign="top">
                                
                                    
                                    <!--grid-->

                                  <asp:GridView ID="m_grid" DataKeyNames="Id" runat="server" 
                                        CellPadding="4" ForeColor="#333333" ShowHeader="true"
                                        AutoGenerateColumns="False" Width="100%" HorizontalAlign="Center"  
                                        OnRowDeleting="m_grid_RowDeleting" OnRowEditing="m_grid_RowEditing"
                                        OnRowCancelingEdit="m_grid_RowCancelingEdit" OnRowUpdating="m_grid_RowUpdating" 
                                        ShowFooter="true" OnRowCommand="m_grid_RowCommand" >
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Tên Công Việc">
                                            <ItemTemplate>
                                                <%#Eval("WorkName")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtUWorkName" Text='<%#Bind("WorkName")%>' Width="300" runat="server"/>
                                                <asp:RequiredFieldValidator ID="RreqName" runat="server" ControlToValidate="txtUWorkName" Display="Dynamic" ErrorMessage="Nhập tên công việc" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox id="txtIWorkName" runat="server" Width="300"></asp:TextBox><br />
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Mức độ ưu tiên CV">
                                            <ItemTemplate>
                                                <%#Eval("Prior_Level")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtUPrior_Level" Text='<%#Bind("Prior_Level")%>' MaxLength="16" runat="server"  />
                                                <asp:RequiredFieldValidator ID="RequiredCode" runat="server" ControlToValidate="txtUPrior_Level" Display="Dynamic" ErrorMessage="Nhập Mức độ ưu tiên CV" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox id="txtIPrior_Level" runat="server" MaxLength="16"></asp:TextBox><br />
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
                        </td>
                    </tr>
                    <tr>
                    <td valign ="bottom" style ="width:100%; height:28px">
                        <table ID="Table5"  border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                            <tr>
                                <td>
                                <p></p>
                                    <asp:Literal ID="ltlAnnouncement" runat="server"></asp:Literal>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 95%" align="left">
                            <p></p>
                            <p></p>
                            <table align="right">
                                <tr><td >
                                        <asp:Button ID="btnInsert1" runat="server" Text="Thêm" 
                                        onclick="btnInsert_Click" Width="70px" Height="30px"/>
                                    &nbsp;&nbsp;
                                        <asp:Button ID="btnUpdate1" runat="server" Text="Sửa" Width="70px" 
                                        Height="30px" onclick="btnUpdate_Click"/>
                                    &nbsp;&nbsp;
                                        <asp:Button ID="btnDelete1" runat="server" Text="Xóa" Width="70px" 
                                        Height="30px" onclick="btnDelete_Click"/>
                                </td></tr>
                            </table>
                        </td> 
                        <td style="width:30px"></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    
    

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    <AjaxSettings>
        <telerik:AjaxSetting AjaxControlID="RadGrid1">
            <UpdatedControls>
                <telerik:AjaxUpdatedControl ControlID="txtPositionName"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="txtSalaryLevel"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="txtDescrible"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="dlBelongName"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="dlEduLevel"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="dlEduMajor"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="dlEngLevel"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="txtITLevel"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="txtMLevel"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="txtPLevel"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="txtSkills"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="txtOther"></telerik:AjaxUpdatedControl>
                <telerik:AjaxUpdatedControl ControlID="m_grid"></telerik:AjaxUpdatedControl>
            </UpdatedControls>
        </telerik:AjaxSetting>
    </AjaxSettings>
</telerik:RadAjaxManager>

</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>