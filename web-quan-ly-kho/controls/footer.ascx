<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_footer" Codebehind="footer.ascx.cs" %>
<table cellspacing="0" cellpadding="0" width="100%" border="0" style="background-color:#3b78ae">
	<tr valign="top">
		<td>
			<table cellspacing="0" cellpadding="0" border="0" width="100%" style="background-color:#ffffff">
				<tr style="height:15px">
					<td valign="top" style="width:24px;background-color:#3b78ae; height: 5px;">
						<img alt="" style="border:0px" src="<%=UrlImages%>/goc1.gif" /></td>
					<td align="left" style="width:100%"></td>
					<td valign="top" align="right" style="width:24px;background-color:#3b78ae">
						<img alt="" style="border:0px" src="<%=UrlImages%>/goc3.gif" /></td>
				</tr>
			</table>
		</td>
	</tr>
    <tr valign="top" >
        <td align="left" class="footer" style="color: #ffffff; padding-top: 20px;padding-bottom: 0px">
		        <asp:Literal ID="ltlFooter0" runat="server"></asp:Literal></td>
    </tr>
    <tr valign="top" >
        <td align="left" class="footer" style="color: #ffffff; padding-top: 0px;padding-bottom: 0px">
		        <asp:Literal ID="ltlFooter1" runat="server"></asp:Literal></td>
    </tr>
    <tr valign="top" >
        <td align="left" class="footer" style="color: #ffffff; padding-top: 0px;padding-bottom: 10px">
		        <asp:Literal ID="ltlFooter2" runat="server"></asp:Literal></td>
    </tr>
</table>
