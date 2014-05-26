/*  Sondt
*	03/11/2011
*	Calendar helper - convert from events_calendar.js - Mr.Thu
*/
var eventSource;
var editUrl = '';
var duration = 7;
//var todayDate = new Date().getFullYear() + '/' + parseInt(new Date().getMonth() + 1) + '/' + new Date().getDate();
var currDate = new Date().getFullYear() + '/' + parseInt(new Date().getMonth() + 1) + '/' + new Date().getDate();
var userId = currentUserId;
var cid = 0;
var eventLastUrl = "#";
var tblContentId = "tblCalendarMain";
var tblSimpleMonthId = "simpleMonth";
var xhrEventSource;

/* cac ham xu ly thoi gian============================================================*/
function formatDateTime(a, initDate) {
	var e = [[11, 'sáng'], [14, 'trua'], [19, 'chiều']];
	var f = ['Chủ Nhật', 'Thứ Hai', 'Thứ Ba', 'Thứ Tư', 'Thứ Năm', 'Thứ Sáu', 'Thứ Bảy'];
	var g = new Date(); //new Date();
	var j = null;

	if (initDate) {
		j = a;
	}
	else {
		var _match = a.match(/^\/Date\((\d+)([-+]\d\d)(\d\d)\)\/$/);
		if (_match) {
			j = new Date(1 * _match[1] + 3600000 * (_match[2] - 7) + 60000 * _match[3]);
		}
		else {
			j = new Date(a);
		}
	}

	var d = Math.floor(g.getTime() / 1000) - Math.floor(j / 1000);
	var h = j.getHours();
	var m = formatValue(j.getMinutes());
	h = formatValue((h));
	return h + ':' + m;
}

function formatJsonSimple(a, initDate) {
	var e = [[11, 'sáng'], [14, 'trua'], [19, 'chiều']];
	var f = ['Chủ Nhật', 'Thứ Hai', 'Thứ Ba', 'Thứ Tư', 'Thứ Năm', 'Thứ Sáu', 'Thứ Bảy'];
	var g = new Date(); //new Date();
	var j = null;
	if (initDate) {
		j = a;
	}
	else {
		var _match = a.match(/^\/Date\((\d+)([-+]\d\d)(\d\d)\)\/$/);
		if (_match) {
			j = new Date(1 * _match[1] + 3600000 * (_match[2] - 7) + 60000 * _match[3]);
		}
		else {
			j = new Date(a);
		}
	}
	return formatValue(j.getDate()) + '_' + formatValue(j.getMonth() + 1) + '_' + j.getFullYear();
}

function formatValue(value) {
	return value < 10 ? ('0' + value) : value;
}

function getDayInWeek(d, numberDay) {
	var day = new Date(d).getDay(),
      diff = new Date(d).getDate() - day + (day == 0 ? -7 + numberDay : numberDay); // adjust when day is sunday
	return new Date(new Date(d).setDate(diff));
}

function formatDayInWeek(d, numberDay) {
	var f = ['Thứ Hai', 'Thứ Ba', 'Thứ Tư', 'Thứ Năm', 'Thứ Sáu', 'Thứ Bảy', 'Chủ Nhật'];
	var day = new Date(d).getDay(),
      diff = new Date(d).getDate() - day + (day == 0 ? -7 + numberDay : numberDay); // adjust when day is sunday
	return '<span class="PRC_DayName"> ' + f[numberDay - 1] + '</span><br />' + formatValue(new Date(new Date(d).setDate(diff)).getDate()) + '/' + formatValue(new Date(new Date(d).setDate(diff)).getMonth() + 1) + '/' + new Date(new Date(d).setDate(diff)).getFullYear();
}

/* tra ve du lieu dang dd_mm_yyyy */
function formatDaySimple(d, numberDay) {
	var day = new Date(d).getDay(),
      diff = new Date(d).getDate() - day + (day == 0 ? -7 + numberDay : numberDay); // adjust when day is sunday
	return formatValue(new Date(new Date(d).setDate(diff)).getDate()) + '_' + formatValue(new Date(new Date(d).setDate(diff)).getMonth() + 1) + '_' + new Date(new Date(d).setDate(diff)).getFullYear();
}

/* lay ra nga`y cuoi cung trong thang */
function lastDaysInMonth(month, year) {
	var m = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
	if (month != 2) return m[month - 1];
	if (year % 4 != 0) return m[1];
	if (year % 100 == 0 && year % 400 != 0) return m[1];
	return m[1] + 1;
}

function getFirstDayInMonth(d) {
	return new Date(new Date(d).getFullYear() + '/' + parseInt(new Date(d).getMonth() + 1) + '/01');
}

function getLastDayInMonth(d) {
	return new Date(new Date(d).getFullYear() + '/' + parseInt(new Date(d).getMonth() + 1) + '/' + lastDaysInMonth(new Date(d).getMonth() + 1, new Date(d).getFullYear()));
}

function getMonth(d) {
	return parseInt(new Date(d).getMonth() + 1)
}

function getDate(d) {
	return new Date(d).getDate();
}

function AddDaysToDate(date, days) {
	date.setDate(date.getDate() + days);
	return date;
}

function AddMonthToDate(date, months) {
	var dm = new Date(date);
	dm.setMonth(dm.getMonth() + months);
	return dm;
}
/* END ============================================================*/

function getCurrentUrl() {
	var url = window.location.href.toString();
	var url = url.replace(/#.*/gi, '');
	return url;
}

function getCurrentAction() {
	var url = window.location.href.toString();
	url = url.replace(/^[^#]+/gi, '').replace('#', '');
	return url;
}

function eventBuildHref() {
	var url = '';
	url += ((duration > 0) ? ('dur' + duration) : ''); // build duration
	url += ((currDate.length > 0) ? (',currDate' + currDate) : ''); // build currDate
	url += ((userId > 0) ? (',uid' + userId) : ''); // build user id
	url += ((cid > 0) ? (',cId' + cid) : ''); // buid id
	url = (url != '' && url.charAt(0) == ',') ? url.substring(1) : url;
	url = ((url != '') ? '#' + url : url);

	if (url.replace('#', '') != eventLastUrl) {
		eventLastUrl = url;
		window.location.href = getCurrentUrl() + url;
	}
}

/* loc cac tham so tren url*/
function parseFilter() {
	var r; var m;
	var url = getCurrentAction();
	var actionArray = url.split(',');
	if (actionArray.length < 1) { return; }
	// duration
	$.each(actionArray, function (i, item) {
		r = new RegExp(/dur\d+/gi);
		m = r.exec(item);
		if (m != null) { duration = parseInt(item.replace('dur', '')); return true; }
	});
	// currDate
	$.each(actionArray, function (i, item) {
		r = new RegExp(/currDate\d+/gi);
		m = r.exec(item);
		if (m != null) { currDate = item.replace('currDate', ''); return true; }
	});
	// userId
	$.each(actionArray, function (i, item) {
		r = new RegExp(/userId\d+/gi);
		m = r.exec(item);
		if (m != null) { userId = parseInt(item.replace('userId', '')); return true; }
	});
	$.each(actionArray, function (i, item) {
		r = new RegExp(/cid\d+/gi);
		m = r.exec(item);
		if (m != null) { cid = parseInt(item.replace('cId', '')); return true; }
	});
	eventLastUrl = url;
}

/* ve danh sach su kien ==============================================================*/
function drawDayEvent() {
    if (eventSource != null) {
        $.each(eventSource, function (i, result) {
            var iconStatus = '';

            if (result.Status == 0) {
                iconStatus = '<img border="0" align="absmiddle" src="/images/icon_book.gif" title="Đang chờ duyệt">';
            } else if (result.Status == 1) {
                iconStatus = '<img border="0" align="absmiddle" src="/images/icon_approved.gif" title="Đã chấp nhận">';
            } else if (result.Status == 2) {
                iconStatus = '<img border="0" align="absmiddle" src="/images/icon_rejected.gif" title="Đã từ chối">';
            }

            //alert(result.BookUserId + ' - ' + userId);
            if (editUrl != '' && result.BookUserId == userId) {
                $("#evxDay" + formatJsonSimple(result.FromTime)).append('<tr><td width="100%" valign="top">' + iconStatus + formatDateTime(result.FromTime) + ' ' + '<a href="' + editUrl + result.Id + '">' + result.BookingContent + '</a>' + '</td></tr>');
            } else {
                $("#evxDay" + formatJsonSimple(result.FromTime)).append('<tr><td width="100%" valign="top">' + iconStatus + formatDateTime(result.FromTime) + ' ' + '<a href="javascript:;">' + result.BookingContent + '</a>' + '</td></tr>');
            }
        });
	}
}

function draw1DayEvent() {
	if (eventSource != null) {
	    $.each(eventSource, function (i, result) {
	        var dtStart = new Date("2011/10/10 " + formatDateTime(result.FromTime));
	        
            var iconStatus = '';

	        if (result.Status == 0) {
	            iconStatus = '<img border="0" align="absmiddle" src="/images/icon_book.gif" title="Đang chờ duyệt">';
	        } else if (result.Status == 1) {
	            iconStatus = '<img border="0" align="absmiddle" src="/images/icon_approved.gif" title="Đã chấp nhận">';
	        } else if (result.Status == 2) {
	            iconStatus = '<img border="0" align="absmiddle" src="/images/icon_rejected.gif" title="Đã từ chối">';
	        }

	        var url = "javascript:;";

	        if (editUrl != '' && result.BookUserId == userId) {
	            url = editUrl + result.Id;
	        } else {
                url = "javascript:;"
	        }
            if (dtStart < new Date("2011/10/10 7:00")) {
                $("#eventH6").append(iconStatus + formatDateTime(result.FromTime) + '<a href="' + url + '">' + ' ' + result.BookingContent + '</a><br />');
	        }
	        else if (dtStart >= new Date("2011/10/10 7:00") && dtStart < new Date("2011/10/10 8:00")) {
	            $("#eventH7").append(iconStatus + formatDateTime(result.FromTime) + '<a href="' + url + '">' + ' ' + result.BookingContent + '</a><br />');
	        }
	        else if (dtStart >= new Date("2011/10/10 8:00") && dtStart < new Date("2011/10/10 9:00")) {
	            $("#eventH8").append(iconStatus + formatDateTime(result.FromTime) + '<a href="' + url + '">' + ' ' + result.BookingContent + '</a><br />');
	        }
	        else if (dtStart >= new Date("2011/10/10 9:00") && dtStart < new Date("2011/10/10 10:00")) {
	            $("#eventH9").append(iconStatus + formatDateTime(result.FromTime) + '<a href="' + url + '">' + ' ' + result.BookingContent + '</a><br />');
	        }
	        else if (dtStart >= new Date("2011/10/10 10:00") && dtStart < new Date("2011/10/10 11:00")) {
	            $("#eventH10").append(iconStatus + formatDateTime(result.FromTime) + '<a href="' + url + '">' + ' ' + result.BookingContent + '</a><br />');
	        }
	        else if (dtStart >= new Date("2011/10/10 11:00") && dtStart < new Date("2011/10/10 12:00")) {
	            $("#eventH11").append(iconStatus + formatDateTime(result.FromTime) + '<a href="' + url + '">' + ' ' + result.BookingContent + '</a><br />');
	        }
	        else if (dtStart >= new Date("2011/10/10 12:00") && dtStart < new Date("2011/10/10 13:00")) {
	            $("#eventH12").append(iconStatus + formatDateTime(result.FromTime) + '<a href="' + url + '">' + ' ' + result.BookingContent + '</a><br />');
	        }
	        else if (dtStart >= new Date("2011/10/10 13:00") && dtStart < new Date("2011/10/10 14:00")) {
	            $("#eventH13").append(iconStatus + formatDateTime(result.FromTime) + '<a href="' + url + '">' + ' ' + result.BookingContent + '</a><br />');
	        }
	        else if (dtStart >= new Date("2011/10/10 14:00") && dtStart < new Date("2011/10/10 15:00")) {
	            $("#eventH14").append(iconStatus + formatDateTime(result.FromTime) + '<a href="' + url + '">' + ' ' + result.BookingContent + '</a><br />');
	        }
	        else if (dtStart >= new Date("2011/10/10 15:00") && dtStart < new Date("2011/10/10 16:00")) {
	            $("#eventH15").append(iconStatus + formatDateTime(result.FromTime) + '<a href="' + url + '">' + ' ' + result.BookingContent + '</a><br />');
	        }
	        else if (dtStart >= new Date("2011/10/10 16:00") && dtStart < new Date("2011/10/10 17:00")) {
	            $("#eventH16").append(iconStatus + formatDateTime(result.FromTime) + '<a href="' + url + '">' + ' ' + result.BookingContent + '</a><br />');
	        }
	        else if (dtStart >= new Date("2011/10/10 17:00")) {
	            $("#eventH17").append(iconStatus + formatDateTime(result.FromTime) + '<a href="' + url + '">' + ' ' + result.BookingContent + '</a><br />');
	        }
	    });
	}
}

function eventNext() {
	if (duration == 1) {
		var dadd = AddDaysToDate(new Date(currDate), 1);
		currDate = dadd.getFullYear() + '/' + parseInt(dadd.getMonth() + 1) + '/' + dadd.getDate();
	}
	if (duration == 7) {
		var dadd = AddDaysToDate(new Date(currDate), 7);
		currDate = dadd.getFullYear() + '/' + parseInt(dadd.getMonth() + 1) + '/' + dadd.getDate();
	}
	if (duration == 30) {
		var dadd = AddMonthToDate(new Date(currDate), 1);
		currDate = dadd.getFullYear() + '/' + parseInt(dadd.getMonth() + 1) + '/' + dadd.getDate();
	}
	eventBuildHref();
	drawCalendar(currDate, duration);
}

function eventBack() {
	if (duration == 1) {
		var dadd = AddDaysToDate(new Date(currDate), -1);
		currDate = dadd.getFullYear() + '/' + parseInt(dadd.getMonth() + 1) + '/' + dadd.getDate();
	}
	if (duration == 7) {
		var dadd = AddDaysToDate(new Date(currDate), -7);
		currDate = dadd.getFullYear() + '/' + parseInt(dadd.getMonth() + 1) + '/' + dadd.getDate();
	}
	if (duration == 30) {
		var dadd = AddMonthToDate(new Date(currDate), -1);
		currDate = dadd.getFullYear() + '/' + parseInt(dadd.getMonth() + 1) + '/' + dadd.getDate();
	}
	eventBuildHref();
	drawCalendar(currDate, duration);
}

function simpleNext() {
	var dadd = AddMonthToDate(new Date(currDate), 1);
	currDate = dadd.getFullYear() + '/' + parseInt(dadd.getMonth() + 1) + '/' + dadd.getDate();
	eventBuildHref();
	drawCalendar(currDate, duration);
}

function simpleBack() {
	var dadd = AddMonthToDate(new Date(currDate), -1);
	currDate = dadd.getFullYear() + '/' + parseInt(dadd.getMonth() + 1) + '/' + dadd.getDate();
	eventBuildHref();
	drawCalendar(currDate, duration);
}

function changeDuration(dr) {
	duration = dr;
	eventBuildHref();
	drawCalendar(currDate, duration);
}

function eventQuickView() {
	currDate = $("#calendarDropListYears option:selected").val() + '/' + $("#calendarDropListMonths option:selected").val() + '/01';
	duration = 30;
	eventBuildHref();
	drawCalendar(currDate, duration);
}

function viewDay(dateView) {
	currDate = dateView;
	duration = 1;
	eventBuildHref();
	drawCalendar(currDate, duration);
}

function render1Day(element, currDate) {
	var tblDay = '<table width="100%" cellspacing="1" cellpadding="0" border="0">';
	tblDay += '		<tr><td></td></tr>';
	tblDay += '      <tr>';
	tblDay += '          <td>';
	tblDay += '              <table width="100%" cellspacing="1" cellpadding="1" border="0" class="PRC_BgTable">';
	tblDay += '                    <tr><td width="100%" class="PRC_TodayHead"><span class="PRC_DayName">&nbsp;&nbsp;' + currDate + ' </span></td></tr>';
	tblDay += '              </table>';
	tblDay += '              <table width="100%" cellspacing="1" cellpadding="1" border="0" class="PRC_BgTable_Day">';
	tblDay += '                  <tr><td align="right" class="PRC_HourHead"> <div class="PRC_Div_HourHead"> ----- </div></td><td width="90%" class="PRC_HourEvent" id="eventH6"></td></tr>';
	tblDay += '              </table>';
	tblDay += '              <table width="100%" cellspacing="1" cellpadding="1" border="0" class="PRC_BgTable_Day">';
	tblDay += '                  <tr><td align="right" class="PRC_HourHead"> <div class="PRC_Div_HourHead">7:00 AM</div></td><td width="90%" class="PRC_HourEvent" id="eventH7"></td></tr>';
	tblDay += '              </table>';
	tblDay += '              <table width="100%" cellspacing="1" cellpadding="1" border="0" class="PRC_BgTable_Day">';
	tblDay += '                  <tr><td align="right" class="PRC_HourHead"> <div class="PRC_Div_HourHead">8:00 AM</div></td><td width="90%" class="PRC_HourEvent" id="eventH8"></td></tr>';
	tblDay += '              </table>';
	tblDay += '              <table width="100%" cellspacing="1" cellpadding="1" border="0" class="PRC_BgTable_Day">';
	tblDay += '                  <tr><td align="right" class="PRC_HourHead"> <div class="PRC_Div_HourHead">9:00 AM</div></td><td width="90%" class="PRC_HourEvent" id="eventH9"></td></tr>';
	tblDay += '              </table>';
	tblDay += '              <table width="100%" cellspacing="1" cellpadding="1" border="0" class="PRC_BgTable_Day">';
	tblDay += '                  <tr><td align="right" class="PRC_HourHead"> <div class="PRC_Div_HourHead">10:00 AM</div></td><td width="90%" class="PRC_HourEvent" id="eventH10"></td></tr>';
	tblDay += '              </table>';
	tblDay += '              <table width="100%" cellspacing="1" cellpadding="1" border="0" class="PRC_BgTable_Day">';
	tblDay += '                  <tr><td align="right" class="PRC_HourHead"> <div class="PRC_Div_HourHead">11:00 AM</div></td><td width="90%" class="PRC_HourEvent" id="eventH11"></td></tr>';
	tblDay += '              </table>';
	tblDay += '              <table width="100%" cellspacing="1" cellpadding="1" border="0" class="PRC_BgTable_Day">';
	tblDay += '                  <tr><td align="right" class="PRC_HourHead"> <div class="PRC_Div_HourHead">12:00 AM</div></td><td width="90%" class="PRC_HourEvent" id="eventH12"></td></tr>';
	tblDay += '              </table>';
	tblDay += '              <table width="100%" cellspacing="1" cellpadding="1" border="0" class="PRC_BgTable_Day">';
	tblDay += '                  <tr><td align="right" class="PRC_HourHead"> <div class="PRC_Div_HourHead">1:00 PM</div></td><td width="90%" class="PRC_HourEvent" id="eventH13"></td></tr>';
	tblDay += '              </table>';
	tblDay += '              <table width="100%" cellspacing="1" cellpadding="1" border="0" class="PRC_BgTable_Day">';
	tblDay += '                  <tr><td align="right" class="PRC_HourHead"> <div class="PRC_Div_HourHead">2:00 PM</div></td><td width="90%" class="PRC_HourEvent" id="eventH14"></td></tr>';
	tblDay += '              </table>';
	tblDay += '              <table width="100%" cellspacing="1" cellpadding="1" border="0" class="PRC_BgTable_Day">';
	tblDay += '                  <tr><td align="right" class="PRC_HourHead"> <div class="PRC_Div_HourHead">3:00 PM</div></td><td width="90%" class="PRC_HourEvent" id="eventH15"></td></tr>';
	tblDay += '              </table>';
	tblDay += '              <table width="100%" cellspacing="1" cellpadding="1" border="0" class="PRC_BgTable_Day">';
	tblDay += '                  <tr><td align="right" class="PRC_HourHead"> <div class="PRC_Div_HourHead">4:00 PM</div></td><td width="90%" class="PRC_HourEvent" id="eventH16"></td></tr>';
	tblDay += '              </table>';
	tblDay += '              <table width="100%" cellspacing="1" cellpadding="1" border="0" class="PRC_BgTable_Day">';
	tblDay += '                  <tr><td align="right" class="PRC_HourHead"> <div class="PRC_Div_HourHead">5:00 PM</div></td><td width="90%" class="PRC_HourEvent" id="eventH17"></td></tr>';
	tblDay += '              </table>';
	tblDay += '      </tr>';
	tblDay += '  </table>';
	$("#" + element).html(tblDay);
}

function render1Week(element, currDate) {
	var tblWeek = '<table width="100%" cellspacing="1" cellpadding="1" border="0" class="PRC_BgTable">';
	for (i = 1; i <= 7; i++) {
	    tblWeek += '    <tr>';
	    if (currDate == getDayInWeek(currDate, i).getFullYear() + '/' + parseInt(getDayInWeek(currDate, i).getMonth() + 1) + '/' + getDayInWeek(currDate, i).getDate()) {
	        tblWeek += '        <td width="20%" valign="middle" height="60" align="center" class="PRC_TodayHead">';
	    } else {
	        tblWeek += '        <td width="20%" valign="middle" height="60" align="center" class="PRC_DayHead">';
	    }
		tblWeek += '            <a href="javascript:;" onclick="viewDay(\'' + getDayInWeek(currDate, i).getFullYear() + '/' + parseInt(getDayInWeek(currDate, i).getMonth() + 1) + '/' + getDayInWeek(currDate, i).getDate() + '\')">     ' + formatDayInWeek(currDate, i) + '</a>';
		tblWeek += '        </td>';
		tblWeek += '        <td width="80%" valign="top" class="PRC_InMonthTable">';
		tblWeek += '            <table width="100%" cellspacing="0" cellpadding="1" border="0" id="evxDay' + formatDaySimple(currDate, i) + '"></table>';
		tblWeek += '        </td>';
		tblWeek += '    </tr>';
	}
	tblWeek += '</table>';
	$("#" + element).html(tblWeek);
}

function render1Month(element, currDate) {
	var tblMonth = '<table width="100%" cellspacing="1" cellpadding="1" border="0">';
	tblMonth += '<tr><td></td></tr>';
	tblMonth += '<tr><td>';
	tblMonth += '<table width="100%" cellspacing="1" cellpadding="1" border="0" class="PRC_BgTable">';
	tblMonth += '<tr><td class="PRC_DayHead">&nbsp;Thứ hai</td><td class="PRC_DayHead">&nbsp;Thứ ba</td><td class="PRC_DayHead">&nbsp;Thứ tư</td><td class="PRC_DayHead">&nbsp;Thứ năm</td><td class="PRC_DayHead">&nbsp;Thứ sáu</td><td class="PRC_DayHead">&nbsp;Thứ bảy</td><td class="PRC_DayHead">&nbsp;Chủ nhật</td></tr>';
	/* lap ve cac ngay trong thang */
	var fDate = getFirstDayInMonth(currDate);
	var lDate = getLastDayInMonth(currDate);
	lDate = getDayInWeek(lDate, 7);
	var crDate = fDate;
	var crMonth = getMonth(currDate);
	while (crDate <= lDate) {
		tblMonth += '<tr>';
		for (i = 1; i <= 7; i++) {
		    if (currDate == getDayInWeek(crDate, i).getFullYear() + '/' + parseInt(getDayInWeek(crDate, i).getMonth() + 1) + '/' + getDayInWeek(crDate, i).getDate()) {
		        tblMonth += '        <td valign="top" class="PRC_TodayTable">';
		    } else {
		        tblMonth += '        <td valign="top" class="PRC_InMonthTable">';
		    }
			tblMonth += '            <table height="100%" width="100%" cellspacing="0" cellpadding="1" border="0">';
			if (crMonth == getMonth(getDayInWeek(crDate, i)))
				tblMonth += '                   <tr><td valign="top" nowrap=""><a href="javascript:;" onclick="viewDay(\'' + getDayInWeek(crDate, i).getFullYear() + '/' + parseInt(getDayInWeek(crDate, i).getMonth() + 1) + '/' + getDayInWeek(crDate, i).getDate() + '\')"> ' + getDayInWeek(crDate, i).getDate() + ' </a></td><td align="right" nowrap=""></td></tr>';
			else tblMonth += '                   <tr><td valign="top" nowrap=""></td><td align="right" nowrap=""></td></tr>';
			tblMonth += '                   <tr><td valign="top" colspan="2">';
			tblMonth += '                       <table height="100%" width="100%" cellspacing="0" cellpadding="1" border="0" id="evxDay' + formatDaySimple(crDate, i) + '"></table>';
			tblMonth += '                   </td></tr>';
			tblMonth += '            </table>';
			tblMonth += '        </td>';
		}
		tblMonth += '</tr>';
		crDate = AddDaysToDate(crDate, 7);
	}
	tblMonth += '</table>';
	tblMonth += '</td></tr>';
	tblMonth += '</table>';
	$("#" + element).html(tblMonth);
}

function renderSimpleMonth(element, currDate) {
	var tblMonth = '';
	tblMonth += '<table width="100%" cellspacing="1" cellpadding="1" border="0" class="PRC_BgTable">';
	tblMonth += '<tr><td class="PRC_DayHead">&nbsp;T2</td><td class="PRC_DayHead">&nbsp;T3</td><td class="PRC_DayHead">&nbsp;T4</td><td class="PRC_DayHead">&nbsp;T5</td><td class="PRC_DayHead">&nbsp;T6</td><td class="PRC_DayHead">&nbsp;T7</td><td class="PRC_DayHead">&nbsp;CN</td></tr>';
	/* lap ve cac ngay trong thang */
	var fDate = getFirstDayInMonth(currDate);
	var lDate = getLastDayInMonth(currDate);
	lDate = getDayInWeek(lDate, 7);
	var crDate = fDate;
	var crMonth = getMonth(currDate);
	while (crDate <= lDate) {
		tblMonth += '<tr>';
		for (i = 1; i <= 7; i++) {
		    if (currDate == getDayInWeek(crDate, i).getFullYear() + '/' + parseInt(getDayInWeek(crDate, i).getMonth() + 1) + '/' + getDayInWeek(crDate, i).getDate()) {
		        tblMonth += '        <td valign="top" align="center" class="PRC_TodayTable">';
		    } else {
		        tblMonth += '        <td valign="top" align="center" class="PRC_InMonthTable">';
		    }
			if (crMonth == getMonth(getDayInWeek(crDate, i)))
				tblMonth += '                   <a class="PRC_DayName_Slim" href="javascript:;" onclick="viewDay(\'' + getDayInWeek(crDate, i).getFullYear() + '/' + parseInt(getDayInWeek(crDate, i).getMonth() + 1) + '/' + getDayInWeek(crDate, i).getDate() + '\')"> ' + getDayInWeek(crDate, i).getDate() + ' </a>';
			else tblMonth += '                   ';
			tblMonth += '        </td>';
		}
		tblMonth += '</tr>';
		crDate = AddDaysToDate(crDate, 7);
	}
	tblMonth += '</table>';
	$("#" + element).html(tblMonth);
}

