<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectCompany.aspx.cs" Inherits="QLCV.Permission.SelectCompany" %>

<HTML>
	<HEAD runat="server">
		<title id="Title">Chọn công ty</title>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
        <link href="../CSS/Modules/MeetingRoom/main.css" rel="stylesheet" type="text/css" />

        <script src="../js/NhanSu/SelectPortals.js" type="text/javascript"></script>
	</HEAD>	
	<BODY leftMargin="0" topMargin="0">
		<form name="MainForm" method="post" action="FindPortals.aspx?option=0&amp;PortalIDs=" id="MainForm">
<input type="hidden" name="__VIEWSTATE" value="dDwxMTczNTE5OTM3O3Q8O2w8aTwxPjs+O2w8dDw7bDxpPDE+Oz47bDx0PHA8bDxUZXh0Oz47bDxcPFRBQkxFIFdJRFRIPSc1MjAnIGFsaWduPSdjZW50ZXInIEJPUkRFUj0nMCcgQ0VMTFNQQUNJTkc9JzEnIENFTExQQURESU5HPScwJ1w+XDx0ciBjbGFzcz0nbm9ybWFsYm9sZCcgc3R5bGU9J2JhY2tncm91bmQ6I0NGQ0ZDRicgaGVpZ2h0PScyMydcPlw8dGRcPlw8SU5QVVQgaWQ9J2Noa0FsbFBvcnRhbHMnIHR5cGU9J2NoZWNrYm94JyBvbmNsaWNrPSJDaGVja0FsbCgnY2hrQWxsUG9ydGFscycsICdjaGtQb3J0YWxJZF8nKVw7IiBuYW1lPSdjaGtBbGxQb3J0YWxzJyB2YWx1ZT0nJyBcPlw8L3RkXD5cPHRkIHdpZHRoPScxMDAlJ1w+VMOqbiDEkcahbiB24buLXDwvdGRcPlw8L3RyXD5cPHRyIGNsYXNzPSdub3JtYWxib2xkJyBzdHlsZT0nYmFja2dyb3VuZDojREREREREJyBoZWlnaHQ9JzIzJ1w+XDx0ZFw+XDxpbnB1dCB0eXBlPSdjaGVja2JveCcgaWQ9J2Noa1BvcnRhbElkXzEnIG5hbWU9J2Noa1BvcnRhbElkXzEnIHZhbHVlPScxOkPDlE5HIFRZJ1w+XDwvdGRcPlw8dGRcPlw8aW1nIHNyYz0nL2ltYWdlcy9pY29uX3BvcnRhbC5naWYnIGJvcmRlcj0nMCcgYWxpZ249J2Fic21pZGRsZScgaHNwYWNlPScyJ1w+Q8OUTkcgVFlcPC90ZFw+XDwvdHJcPlw8dHIgY2xhc3M9J25vcm1hbGJvbGQnIHN0eWxlPSdiYWNrZ3JvdW5kOiNEREREREQnIGhlaWdodD0nMjMnXD5cPHRkXD5cPGlucHV0IHR5cGU9J2NoZWNrYm94JyBpZD0nY2hrUG9ydGFsSWRfMicgbmFtZT0nY2hrUG9ydGFsSWRfMicgdmFsdWU9JzI6Q0hJIE5Iw4FOSCdcPlw8L3RkXD5cPHRkXD5cPGltZyBzcmM9Jy9pbWFnZXMvaWNvbl9wb3J0YWwuZ2lmJyBib3JkZXI9JzAnIGFsaWduPSdhYnNtaWRkbGUnIGhzcGFjZT0nMidcPkNISSBOSMOBTkhcPC90ZFw+XDwvdHJcPlw8L1RBQkxFXD47Pj47Oz47Pj47Pj47PuOG6Ci4kG8TP1KtZ4ys9ml0LF1D" />

			<TABLE class="form-table" width="100%" border="0">
				<TR valign="top">

					<TD class="SOF_T1">Chọn công ty</TD>
				</TR>
			</TABLE>
			<TABLE WIDTH='520' align='center' BORDER='0' CELLSPACING='1' CELLPADDING='0'>
			    <tr class='normalbold' style='background:#CFCFCF' height='23'>
			        <td><INPUT id='chkAllPortals' type='checkbox' onclick="CheckAll('chkAllPortals', 'chkPortalId_');" name='chkAllPortals' value='' ></td>
			        <td width='100%'>Tên đơn vị</td>
			    </tr>
                <asp:Literal ID="ltlListCompany" runat="server"></asp:Literal>
			</TABLE>
			<TABLE class="form-table" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TR>
					<TD align="center" height="40">

						<INPUT class="formbutton" onclick="javascript:self.close();" type="button" value="  Đóng  "
							name="btnClose"><INPUT class="formbutton" onclick="javascript:finishUpdate();" type="button" value="Cập nhật và đóng"
							name="btnFinish">
					</TD>
				</TR>
			</TABLE>
			
			<input type='hidden' id='hddOption' value='<%= Request["Index"] == null ? 0 : Convert.ToInt32(Request["Index"].ToString()) %>'/>
		</form>
	</BODY>
</HTML>
