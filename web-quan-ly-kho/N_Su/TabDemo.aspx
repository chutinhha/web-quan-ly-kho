<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TabDemo.aspx.cs" Inherits="QLCV.N_Su.TabDemo" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" TagPrefix="act" Namespace="AjaxControlToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div>
        <AjaxControls:TabContainer runat="server" ID=""></AjaxControls:TabContainer>
            <act:TabContainer ID="tc1" runat="server" ActiveTabIndex="0" 
                OnClientActiveTabChanged="loadTabPanel" 
                onactivetabchanged="tc1_ActiveTabChanged">
                <act:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                    <ContentTemplate>
                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                        <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
                    </ContentTemplate>
                </act:TabPanel>
                <act:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="TabButton1" runat="server" OnClick="TabButton_Click" Style="display: none;" />
                                <asp:Panel ID="TabContent1" runat="server" Visible="False">
                                    <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </act:TabPanel>
                <act:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel2">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="TabButton2" runat="server" Style="display: none;" OnClick="TabButton_Click" />
                                <asp:Panel ID="TabContent2" runat="server" Visible="False">
                                    &nbsp;<asp:CheckBox ID="CheckBox1" runat="server" />
                                    <asp:CheckBox ID="CheckBox2" runat="server" />
                                    <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                                    </asp:CheckBoxList>
                                    <asp:RadioButton ID="RadioButton1" runat="server" />
                                    <asp:RadioButton ID="RadioButton2" runat="server" />
                                    <asp:DropDownList ID="DropDownList1" runat="server">
                                    </asp:DropDownList></asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdateProgress ID="uprog" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                            <ProgressTemplate>
                                Loading...</ProgressTemplate>
                        </asp:UpdateProgress>
                    </ContentTemplate>
                    <HeaderTemplate>
                        TabPanel3
                    </HeaderTemplate>
                </act:TabPanel>
            </act:TabContainer>
        </div>
        <hr />
    </form>
</body>
</html>
