function drawCalendar(currDate, duration) {
    editUrl = urlRoot + 'Booking/Book_Room_Edit.aspx?id=';

	var ltTitle = 'Tháng ' + parseInt(new Date(currDate).getMonth() + 1) + ' - ' + new Date(currDate).getFullYear();
	$("#lblDateTitle").html(ltTitle);
	$("#lblDateTitle1").html(ltTitle);
	if (duration == 1) {
		render1Day(tblContentId, currDate);
		renderSimpleMonth(tblSimpleMonthId, currDate);
	}
	if (duration == 7) {
		render1Week(tblContentId, currDate);
		renderSimpleMonth(tblSimpleMonthId, currDate);
	}
	if (duration == 30) {
		render1Month(tblContentId, currDate);
	}
	if (xhrEventSource != undefined) xhrEventSource.abort();
	var roomId = $("#ctl00_m_contentBody_ddlRoom").val();

	$("#divLoading").show();
	$("#tblCalendarMain").hide();

	xhrEventSource = $.ajax({
		type: 'GET',
		url: urlRoot + "Handlers/GetRoomBookingCalendar.ashx?id=" + roomId + "&dura=" + duration + "&date=" + currDate,
		processData: true,
		dataType: "json",
		success: function (data) {
			if (data != null) {
			    eventSource = data;

			    $("#divLoading").hide();
			    $("#tblCalendarMain").show();

				if (duration == 1) {
					draw1DayEvent();
				} else {
	                drawDayEvent(); 
                }
			}
		}
	});
}

$(document).ready(function () {
	loadData();

	$("#ctl00_m_contentBody_ddlRoom").change(function () {
		cid = $("#ctl00_m_contentBody_ddlRoom").val();
		eventBuildHref();
		loadData();
	});
}); 

function loadData() {
	if (getCurrentAction() != eventLastUrl) {
		parseFilter();
		drawCalendar(currDate, duration);
	}
}