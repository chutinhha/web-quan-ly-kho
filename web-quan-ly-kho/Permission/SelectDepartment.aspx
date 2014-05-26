<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectDepartment.aspx.cs" Inherits="QLCV.Permission.SelectDepartment" %>

<HTML>
	<HEAD runat="server">
		<title id="Title">Chọn phòng ban</title>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<link href="../CSS/Modules/MeetingRoom/main.css" rel="stylesheet" type="text/css" />

        <script src="../js/NhanSu/selectgroups.js" type="text/javascript"></script>
		<script lang="javascript">
			function finishUpdate() {
				updateGroupToOpener();
				closeWindow();
			}
		</script>
	</HEAD>	
	<BODY leftMargin="0" topMargin="0">
		<form name="MainForm" method="post" action="FindGroups.aspx?showview=1&amp;GroupIDs=" id="MainForm">
			<TABLE class="form-table" width="100%" border="0">

				<TR valign="top">
					<TD class="SOF_T1">Chọn phòng ban</TD>
				</TR>
			</TABLE>
            <asp:Literal ID="ltlListDepartment" runat="server"></asp:Literal>
            
			<input type='hidden' id='hddOption' value='<%= Request["Index"] == null ? 0 : Convert.ToInt32(Request["Index"].ToString()) %>'>

			<TABLE class="form-table" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD align="center" height="40">
						<INPUT class="formbutton" onclick="javascript:self.close();" type="button" value="  Đóng  "
							name="btnClose"> <INPUT class="formbutton" onclick="javascript:finishUpdate();" type="button" value="Cập nhật và đóng"
							name="btnFinish">
					</TD>
				</TR>
			</TABLE>
		</form>

	</BODY>
</HTML>

