<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Đăng nhập hệ thống</title>
    
    <meta name="description" content="VTC NEWS">
    <meta http-equiv="Content-Language" content="en-us">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <style type="text/css">
        .style1
        {
            width: 100%;
            height: 319px;
        }
INPUT
{
    font-size: 11px;
    font-family: verdana;
    border: 1px solid #AFC2CC;
    background-color: #FFFFFF;
}
input, select
{
    vertical-align: middle;
}
body, input, select, textarea
{
    font-family: Verdana,Helvetica,sans-serif;
    font-size: 11px;
}
BODY
{
    padding: 10px;
 
    margin: 0px;
    background: url('img/main_rond.gif') no-repeat fixed;
        }
body
{
    cursor: default;
    background-color: #fff;
    padding: 0;
    padding-bottom: 8px;
    margin: 0;
}
        .style2
        {
            width: 295px;
        }
        .style3
        {
            width: 295px;
            height: 122px;
        }
        .style4
        {
            height: 122px;
        }
    </style>
</head>
<body class="BODY_LOGIN" style="background-image: none; background-color: #1c5b94;text-align: center; vertical-align:middle">
    <form id="form1" runat="server">
    <div>
    
        <table class="style1">
            <tr>
                <td class="style3">
                </td>
                <td class="style4">
                </td>
                <td class="style4">
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;</td>
                <td>
       <table border="0" cellpadding="0" cellspacing="0" width="508" >
                        <tr>
                            <td colspan="3">
                                <img alt="" height="54" src="images/bn1.gif" style="border-right: 0px; border-top: 0px;
                                    border-left: 0px; border-bottom: 0px" width="508" /><br />
                            </td>
                        </tr>
                        <tr>
                            <td style="background-image: url(images/login_07.gif); width: 25px">
                                &nbsp;
                            </td>
                            <td style="width: 455px; background-color: #ffffff" >
                                <table border="0" cellpadding="0" cellspacing="0" style="width: 433px">
                                    <tr>
                                        <td style="width: 81px" valign="top">
                                            &nbsp;
                                        </td>
                                        <td align="right" style="width: 352px">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" valign="top" style="height: 160px">
                                            <img alt="" height="156px" src="images/khoa.gif" style="border-right: 0px; border-top: 0px;
                                                border-left: 0px; border-bottom: 0px" width="68px" />
                                        </td>
                                        <td id="Td1" align="right" style="width: 352px; background-color: #6390b9; height: 160px;">
                                            <div style="color: #003399">

    <asp:Login ID="m_login" runat="server" BorderPadding="4"
        BorderStyle="Solid" BorderWidth="0px" Font-Names="Verdana" Font-Size="14px" 
        LoginButtonText = "Đăng nhập" 
        TitleText = "ĐĂNG NHẬP HỆ THỐNG" 
        TitleTextStyle-Font-Size="12px"
        TitleTextStyle-Font-Names="arial" 
        TitleTextStyle-Font-Bold="true" 
        InstructionText=""
        InstructionTextStyle-ForeColor="red"
        OnAuthenticate="m_login_Authenticate" 
        UserNameLabelText="Email:" 
        PasswordLabelText="Mật khẩu:"
        RememberMeText="Lưu mật khẩu"
        FailureText="Tên đăng nhập hoặc mật khẩu không đúng,<br>Bạn hãy thử lại!"
        Width="352px" Height="160px" VisibleWhenLoggedIn="False" DisplayRememberMe="False" 
        >
        <LoginButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px"
            Font-Names="Verdana" Font-Size="0.8em" ForeColor="#003399" Font-Bold="True"/>
        <TextBoxStyle Font-Size="0.8em" />
        <TitleTextStyle BackColor="Transparent" Font-Bold="True" Font-Size="0.9em" ForeColor="White" Font-Names="arial" />
        <InstructionTextStyle Font-Italic="True" ForeColor="White" Font-Bold="True" />
        <CheckBoxStyle HorizontalAlign="Center" Wrap="True" ForeColor="#003399" Font-Bold="True" />
        <LabelStyle ForeColor="#003399" Font-Bold="True" />
    </asp:Login>
</div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="background-image: url(images/login_09.gif); width: 28px">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 25px">
                                <img alt="" height="24" src="images/login_10.gif" style="border-right: 0px;
                                    border-top: 0px; border-left: 0px; border-bottom: 0px" width="25" /></td>
                            <td style="background-image: url(images/login_11.gif); width: 455px">
                                &nbsp;</td>
                            <td style="width: 28px">
                                <img alt="" height="24" src="images/login_12.gif" style="border-right: 0px;
                                    border-top: 0px; border-left: 0px; border-bottom: 0px" width="28" /></td>
                        </tr>
                    </table>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
