<%@ Page Language="C#" MasterPageFile="~/master/default.master" AutoEventWireup="true" CodeBehind="ErrorPermission.aspx.cs" Inherits="QLCV.ErrorPermission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="m_contentBody" Runat="Server">
<link href="../CSS/Modules/Message/prms.css" rel="stylesheet" type="text/css" />
<link href="../CSS/Modules/MeetingRoom/main.css" rel="stylesheet" type="text/css" />

<table width="100%" cellpadding="4">
    <tbody>
        <tr>
            <td class="T1" height="40"></asp:Label>
                <asp:Label ID="lblENAme" runat="server" Text="Bạn không có quyền truy nhập thông tin này !!!" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td><asp:Button ID="btnBack" runat="server" Text="Quay lại" class="formbutton"
                    OnClientClick="JavaScript: window.history.back(1); return false;"/>&nbsp;</td>
        </tr>
    </tbody>
</table>
</asp:Content>
