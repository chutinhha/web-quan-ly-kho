<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_tab" Codebehind="tab.ascx.cs" %>
<%--<script type="text/javascript">
<!--
function __keyPress(event, href) {
    var keyCode;
    if (typeof(event.keyCode) != "undefined") {
        keyCode = event.keyCode;
    }
    else {
        keyCode = event.which;
    }

    if (keyCode == 13) {
        window.location = href;
    }
}
//-->
</script>--%>

<table cellpadding="0" cellspacing="0" border="0" width="100%">
    <tr class="homePageHeader" valign="top">
        <td>
            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                <tr valign="top">
                    <td valign="bottom" style="height: 23px;">
                        <table id="TabTable" cellspacing="0" cellpadding="0" border="0">
                            <tr>
                                <asp:Literal ID="ltlMenu" runat="server"></asp:Literal>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>