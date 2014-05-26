<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAccount.aspx.cs" Inherits="QLCV.PageSystem.AddAccount" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        .style1
        {
            width: 100%;
            height: 150px;
        }
        .style5
        {
            width: 143px;
            height: 35px;
            font-weight: bold;
        }
        .style6
        {
            height: 35px;
        }
        .style15
        {
            width: 143px;
            height: 30px;
            font-weight: bold;
        }
        .style17
        {
            width: 90px;
            height: 30px;
        }
        .style18
        {
            height: 30px;
        }
        .style19
        {
            width: 143px;
            height: 28px;
        }
        .style20
        {
            height: 28px;
        }
        .style25
        {
            width: 100%;
        }
        .style26
        {
            width: 110px;
        }
        .style27
        {
            width: 110px;
            height: 39px;
        }
        .style28
        {
            height: 39px;
        }
        .style29
        {
            height: 39px;
            width: 97px;
        }
        .style30
        {
            width: 97px;
        }
        .style32
        {
            height: 30px;
            width: 213px;
        }
        .style34
        {
            height: 21px;
            width: 213px;
        }
        .style35
        {
            width: 90px;
            height: 21px;
        }
        .style36
        {
            width: 143px;
            height: 21px;
        }
        .style37
        {
            height: 21px;
        }
        .style38
        {
            width: 143px;
            height: 18px;
            font-weight: bold;
        }
        .style39
        {
            height: 18px;
            width: 213px;
        }
        .style40
        {
            width: 90px;
            height: 18px;
            font-weight: bold;
        }
        .style41
        {
            height: 18px;
        }
    </style>
        <script type="text/javascript">
            function CloseAndRebind() {
                opener.refreshGrid1();
                self.close();
                return false;
               }
            function Close() {

                self.close();
                return false;
             }
        </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
    
        <table >
            <tr>
                <td class="style36">
                    &nbsp;</td>
                <td class="style34">
                    &nbsp;</td>
                <td class="style35">
                    &nbsp;</td>
                <td class="style37">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style36">
                    <b>Tên đăng nhập</b>:</td>
                <td class="style34">
                    <asp:TextBox ID="txtUserName" runat="server" Width="208px"></asp:TextBox>
                </td>
                <td class="style35">
                    </td>
                <td class="style37">
                    </td>
            </tr>
            <tr>
                <td class="style38">
                    Mật khẩu:</td>
                <td class="style39">
                    <asp:TextBox ID="txtPass1" runat="server" 
                        Width="206px" TextMode="Password"></asp:TextBox>
                </td>
                <td class="style40">
                    Nhập lại:</td>
                <td class="style41">
                    <asp:TextBox ID="txtPass2" runat="server"
                        Width="227px" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            </table>
               <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
            <table>
            <tr>
                <td class="style19">
                    <b>Loại người dùng:</b></td>
                <td class="style20" colspan="3">
                    <b>
                    <asp:RadioButton ID="optEmployee" runat="server" Text="Nhân viên"  
                        GroupName ="ab" AutoPostBack="True" 
                        oncheckedchanged="optEmployee_CheckedChanged"/>
                    </b>&nbsp;<b><asp:RadioButton ID="optPartner" runat="server" Text="Đối tác" 
                        GroupName ="ab" AutoPostBack="True" 
                        oncheckedchanged="optPartner_CheckedChanged" />
                    </b>
                </td>
            </tr>
            <tr>
                <td class="style5">
                    Họ và tên:</td>
                <td class="style6" colspan="3">
                    <asp:DropDownList ID="cboEmployee" runat="server" Height="21px" Width="534px" 
                        DataTextField ="Name" DataValueField ="ID">
                    </asp:DropDownList>
                </td>
            </tr>
            </table>
            </ContentTemplate>
            </asp:UpdatePanel>
            <table>
            <tr>
                <td class="style15">
                    Trạng thái:</td>
                <td class="style32">
                    <asp:DropDownList ID="cboState" runat="server" Height="21px" Width="203px" 
                        DataTextField ="Name" DataValueField ="ID">
                    </asp:DropDownList>
                </td>
                <td class="style17">
                    Tên nhóm:</td>
                <td class="style18">
                <asp:DropDownList ID="cboNhom" runat="server" Height="21px" Width="203px" 
                        DataTextField ="GroupName" DataValueField ="GroupId">
                    </asp:DropDownList>
                    </td>
            </tr>
        </table>
    
    </div>
    <table class="style25">
        <tr>
            <td class="style27">
            </td>
            <td class="style29">
                <asp:Button ID="Button1" runat="server" Height="32px" Text="Cập nhật" 
                    Width="88px" onclick="Button1_Click" />
            </td>
            <td class="style28">
            </td>
        </tr>
        <tr>
            <td class="style26">
                &nbsp;</td>
            <td class="style30">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    </form>
</body>
</html>
