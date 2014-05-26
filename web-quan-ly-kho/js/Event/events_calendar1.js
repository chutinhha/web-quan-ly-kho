/*  ThuNV created date 24/10/2011
ham xu ly ve calendar va event
*/
var eventSource;
var workSource;
var duration = 7;
var todayDate = new Date().getFullYear() + '/' + parseInt(new Date().getMonth() + 1) + '/' + new Date().getDate();
var currDate = new Date().getFullYear() + '/' + parseInt(new Date().getMonth() + 1) + '/' + new Date().getDate();
var userId = sessionUserId;
var eventLastUrl = "#";
var divLoading = "divLoading";
var tblContentId = "tblCalendarMain";
var tblSimpleMonthId = "simpleMonth";
var xhrEventSource;
var xhrWorkSource;

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
/* END cac ham xu ly thoi gian============================================================*/
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
    url += ((userId > 0) ? (',uid' + userId) : ''); // build os type
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
    eventLastUrl = url;
}
/* draw danh sach su kien ==============================================================*/
function drawDayEvent() {
    if (eventSource != null) {
        $.each(eventSource, function (i, result) {
            $("#evxDay" + formatJsonSimple(result.StartDate)).append('<tr><td width="100%" valign="top"><img id="imgApproved' + result.Id + '" style="display:none" border="0" align="absmiddle" title="Đã chấp nhận" src="' + urlRoot + 'images/img_Calendar/icon_approved.gif"><img id="imgRejected' + result.Id + '" style="display:none" border="0" align="absmiddle" title="Đã từ chối" src="' + urlRoot + 'images/img_Calendar/icon_rejected.gif"><a id="lnkViewId' + result.Id + '" href="javascript:;" onclick="viewEventDetail(' + result.Id + ')">' + formatDateTime(result.StartDate) + ' ' + result.Title + '</a></td></tr>');
            /* cai dat hien thi cac icon trang thai */
            if (result.Status == 2) {
                $("#imgApproved" + result.Id).attr("style", "");
            }
            else if (result.Status == 3) {
                $("#imgRejected" + result.Id).attr("style", "");
            }
            else if (result.Status == 4) {
                $("#lnkViewId" + result.Id).html('<strike>' + $("#lnkViewId" + result.Id).text() + '</strike>');
                $("#lnkViewId" + result.Id).attr("style", "color: red");
                $("#lnkViewId" + result.Id).attr("title", "Đã hủy");
                $("#lnkViewId" + result.Id).attr("alt", "Đã hủy");
            }
        });
    }
}
/* draw lich cong viec */
function drawDayWork() {
    if (workSource != null) {
        $.each(workSource, function (i, result) {
            $("#evxDay" + formatJsonSimple(result.StartDate)).append('<tr><td width="100%" valign="top"><a id="lnkViewWorkId' + result.Id + '" href="' + urlRoot + 'QLHC/Work_Main.aspx?tsk_action=' + result.Status + '&WorkID=' + result.Id + '"> ' + result.Title + '</a></td></tr>');
            /* cai dat hien thi cac icon trang thai */
            if (result.Status == 1) {
                $("#lnkViewWorkId" + result.Id).attr("style", "color: blue");
                $("#lnkViewWorkId" + result.Id).attr("title", "Công việc xử lý chính");
                $("#lnkViewWorkId" + result.Id).attr("alt", "Công việc xử lý chính");
            }
            else if (result.Status == 2) {
                $("#lnkViewWorkId" + result.Id).attr("style", "color: green");
                $("#lnkViewWorkId" + result.Id).attr("title", "Công việc tham gia");
                $("#lnkViewWorkId" + result.Id).attr("alt", "Công việc tham gia");
            }
            else if (result.Status == 3) {
                $("#lnkViewWorkId" + result.Id).html('<i>' + $("#lnkViewWorkId" + result.Id).text() + '</i>');
                $("#lnkViewWorkId" + result.Id).attr("style", "color: grey");
                $("#lnkViewWorkId" + result.Id).attr("title", "Công việc theo dõi");
                $("#lnkViewWorkId" + result.Id).attr("alt", "Công việc theo dõi");
            }
        });
    }
}
function draw1DayEvent() {
    if (eventSource != null) {
        $.each(eventSource, function (i, result) {
            var dtStart = new Date("2011/10/10 " + formatDateTime(result.StartDate));
            if (dtStart < new Date("2011/10/10 7:00")) {
                $("#eventH6").append('<img id="imgApproved' + result.Id + '" style="display:none" border="0" align="absmiddle" title="Đã chấp nhận" src="' + urlRoot + 'images/img_Calendar/icon_approved.gif"><img id="imgRejected' + result.Id + '" style="display:none" border="0" align="absmiddle" title="Đã từ chối" src="' + urlRoot + 'images/img_Calendar/icon_rejected.gif"><a id="lnkViewId' + result.Id + '" href="javascript:;" onclick="viewEventDetail(' + result.Id + ')">' + formatDateTime(result.StartDate) + ' ' + result.Title + '</a><br />');
            }
            else if (dtStart >= new Date("2011/10/10 7:00") && dtStart < new Date("2011/10/10 8:00")) {
                $("#eventH7").append('<img id="imgApproved' + result.Id + '" style="display:none" border="0" align="absmiddle" title="Đã chấp nhận" src="' + urlRoot + 'images/img_Calendar/icon_approved.gif"><img id="imgRejected' + result.Id + '" style="display:none" border="0" align="absmiddle" title="Đã từ chối" src="' + urlRoot + 'images/img_Calendar/icon_rejected.gif"><a id="lnkViewId' + result.Id + '" href="javascript:;" onclick="viewEventDetail(' + result.Id + ')">' + formatDateTime(result.StartDate) + ' ' + result.Title + '</a><br />');
            }
            else if (dtStart >= new Date("2011/10/10 8:00") && dtStart < new Date("2011/10/10 9:00")) {
                $("#eventH8").append('<img id="imgApproved' + result.Id + '" style="display:none" border="0" align="absmiddle" title="Đã chấp nhận" src="' + urlRoot + 'images/img_Calendar/icon_approved.gif"><img id="imgRejected' + result.Id + '" style="display:none" border="0" align="absmiddle" title="Đã từ chối" src="' + urlRoot + 'images/img_Calendar/icon_rejected.gif"><a id="lnkViewId' + result.Id + '" href="javascript:;" onclick="viewEventDetail(' + result.Id + ')">' + formatDateTime(result.StartDate) + ' ' + result.Title + '</a><br />');
            }
            else if (dtStart >= new Date("2011/10/10 9:00") && dtStart < new Date("2011/10/10 10:00")) {
                $("#eventH9").append('<img id="imgApproved' + result.Id + '" style="display:none" border="0" align="absmiddle" title="Đã chấp nhận" src="' + urlRoot + 'images/img_Calendar/icon_approved.gif"><img id="imgRejected' + result.Id + '" style="display:none" border="0" align="absmiddle" title="Đã từ chối" src="' + urlRoot + 'images/img_Calendar/icon_rejected.gif"><a id="lnkViewId' + result.Id + '" href="javascript:;" onclick="viewEventDetail(' + result.Id + ')">' + formatDateTime(result.StartDate) + ' ' + result.Title + '</a><br />');
            }
            else if (dtStart >= new Date("2011/10/10 10:00") && dtStart < new Date("2011/10/10 11:00")) {
                $("#eventH10").append('<img id="imgApproved' + result.Id + '" style="display:none" border="0" align="absmiddle" title="Đã chấp nhận" src="' + urlRoot + 'images/img_Calendar/icon_approved.gif"><img id="imgRejected' + result.Id + '" style="display:none" border="0" align="absmiddle" title="Đã từ chối" src="' + urlRoot + 'images/img_Calendar/icon_rejected.gif"><a id="lnkViewId' + result.Id + '" href="javascript:;" onclick="viewEventDetail(' + result.Id + ')">' + formatDateTime(result.StartDate) + ' ' + result.Title + '</a><br />');
            }
            else if (dtStart >= new Date("2011/10/10 11:00") && dtStart < new Date("2011/10/10 12:00")) {
                $("#eventH11").append('<img id="imgApproved' + result.Id + '" style="display:none" border="0" align="absmiddle" title="Đã chấp nhận" src="' + urlRoot + 'images/img_Calendar/icon_approved.gif"><img id="imgRejected' + result.Id + '" style="display:none" border="0" align="absmiddle" title="Đã từ chối" src="' + urlRoot + 'images/img_Calendar/icon_rejected.gif"><a id="lnkViewId' + result.Id + '" href="javascript:;" onclick="viewEventDetail(' + result.Id + ')">' + formatDateTime(result.StartDate) + ' ' + result.Title + '</a><br />');
            }
            else if (dtStart >= new Date("2011/10/10 12:00") && dtStart < new Date("2011/10/10 13:00")) {
                $("#eventH12").append('<img id="imgApproved' + result.Id + '" style="display:none" border="0" align="absmiddle" title="Đã chấp nhận" src="' + urlRoot + 'images/img_Calendar/icon_approved.gif"><img id="imgRejected' + result.Id + '" style="display:none" border="0" align="absmiddle" title="Đã từ chối" src="' + urlRoot + 'images/img_Calendar/icon_rejected.gif"><a id="lnkViewId' + result.Id + '" href="javascript:;" onclick="viewEventDetail(' + result.Id + ')">' + formatDateTime(result.StartDate) + ' ' + result.Title + '</a><br />');
            }
            else if (dtStart >= new Date("2011/10/10 13:00") && dtStart < new Date("2011/10/10 14:00")) {
                $("#eventH13").append('<img id="imgApproved' + result.Id + '" style="display:none" border="0" align="absmiddle" title="Đã chấp nhận" src="' + urlRoot + 'images/img_Calendar/icon_approved.gif"><img id="imgRejected' + result.Id + '" style="display:none" border="0" align="absmiddle" title="Đã từ chối" src="' + urlRoot + 'images/img_Calendar/icon_rejected.gif"><a id="lnkViewId' + result.Id + '" href="javascript:;" onclick="viewEventDetail(' + result.Id + ')">' + formatDateTime(result.StartDate) + ' ' + result.Title + '</a><br />');
            }
            else if (dtStart >= new Date("2011/10/10 14:00") && dtStart < new Date("2011/10/10 15:00")) {
                $("#eventH14").append('<img id="imgApproved' + result.Id + '" style="display:none" border="0" align="absmiddle" title="Đã chấp nhận" src="' + urlRoot + 'images/img_Calendar/icon_approved.gif"><img id="imgRejected' + result.Id + '" style="display:none" border="0" align="absmiddle" title="Đã từ chối" src="' + urlRoot + 'images/img_Calendar/icon_rejected.gif"><a id="lnkViewId' + result.Id + '" href="javascript:;" onclick="viewEventDetail(' + result.Id + ')">' + formatDateTime(result.StartDate) + ' ' + result.Title + '</a><br />');
            }
            else if (dtStart >= new Date("2011/10/10 15:00") && dtStart < new Date("2011/10/10 16:00")) {
                $("#eventH15").append('<img id="imgApproved' + result.Id + '" style="display:none" border="0" align="absmiddle" title="Đã chấp nhận" src="' + urlRoot + 'images/img_Calendar/icon_approved.gif"><img id="imgRejected' + result.Id + '" style="display:none" border="0" align="absmiddle" title="Đã từ chối" src="' + urlRoot + 'images/img_Calendar/icon_rejected.gif"><a id="lnkViewId' + result.Id + '" href="javascript:;" onclick="viewEventDetail(' + result.Id + ')">' + formatDateTime(result.StartDate) + ' ' + result.Title + '</a><br />');
            }
            else if (dtStart >= new Date("2011/10/10 16:00") && dtStart < new Date("2011/10/10 17:00")) {
                $("#eventH16").append('<img id="imgApproved' + result.Id + '" style="display:none" border="0" align="absmiddle" title="Đã chấp nhận" src="' + urlRoot + 'images/img_Calendar/icon_approved.gif"><img id="imgRejected' + result.Id + '" style="display:none" border="0" align="absmiddle" title="Đã từ chối" src="' + urlRoot + 'images/img_Calendar/icon_rejected.gif"><a id="lnkViewId' + result.Id + '" href="javascript:;" onclick="viewEventDetail(' + result.Id + ')">' + formatDateTime(result.StartDate) + ' ' + result.Title + '</a><br />');
            }
            else if (dtStart >= new Date("2011/10/10 17:00")) {
                $("#eventH17").append('<img id="imgApproved' + result.Id + '" style="display:none" border="0" align="absmiddle" title="Đã chấp nhận" src="' + urlRoot + 'images/img_Calendar/icon_approved.gif"><img id="imgRejected' + result.Id + '" style="display:none" border="0" align="absmiddle" title="Đã từ chối" src="' + urlRoot + 'images/img_Calendar/icon_rejected.gif"><a id="lnkViewId' + result.Id + '" href="javascript:;" onclick="viewEventDetail(' + result.Id + ')">' + formatDateTime(result.StartDate) + ' ' + result.Title + '</a><br />');
            }
            if (result.Status == 2) {
                $("#imgApproved" + result.Id).attr("style", "");
            }
            else if (result.Status == 3) {
                $("#imgRejected" + result.Id).attr("style", "");
            }
            else if (result.Status == 4) {
                $("#lnkViewId" + result.Id).html('<strike>' + $("#lnkViewId" + result.Id).text() + '</strike>');
                $("#lnkViewId" + result.Id).attr("style", "color: red");
                $("#lnkViewId" + result.Id).attr("title", "Đã hủy");
                $("#lnkViewId" + result.Id).attr("alt", "Đã hủy");
            }
        });
    }
}
function draw1DayWork() {
    if (workSource != null) {
        $.each(workSource, function (i, result) {
            var dtStart = new Date("2011/10/10 " + formatDateTime(result.StartDate));
            if (dtStart < new Date("2011/10/10 7:00")) {
                $("#eventH6").append('<a id="lnkViewWorkId' + result.Id + '" href="' + urlRoot + 'QLHC/Work_Main.aspx?tsk_action=' + result.Status + '&WorkID=' + result.Id + '">' + formatDateTime(result.StartDate) + ' ' + result.Title + '</a><br />');
            }
            else if (dtStart >= new Date("2011/10/10 7:00") && dtStart < new Date("2011/10/10 8:00")) {
                $("#eventH7").append('<a id="lnkViewWorkId' + result.Id + '" href="' + urlRoot + 'QLHC/Work_Main.aspx?tsk_action=' + result.Status + '&WorkID=' + result.Id + '">' + formatDateTime(result.StartDate) + ' ' + result.Title + '</a><br />');
            }
            else if (dtStart >= new Date("2011/10/10 8:00") && dtStart < new Date("2011/10/10 9:00")) {
                $("#eventH8").append('<a id="lnkViewWorkId' + result.Id + '" href="' + urlRoot + 'QLHC/Work_Main.aspx?tsk_action=' + result.Status + '&WorkID=' + result.Id + '">' + formatDateTime(result.StartDate) + ' ' + result.Title + '</a><br />');
            }
            else if (dtStart >= new Date("2011/10/10 9:00") && dtStart < new Date("2011/10/10 10:00")) {
                $("#eventH9").append('<a id="lnkViewWorkId' + result.Id + '" href="' + urlRoot + 'QLHC/Work_Main.aspx?tsk_action=' + result.Status + '&WorkID=' + result.Id + '">' + formatDateTime(result.StartDate) + ' ' + result.Title + '</a><br />');
            }
            else if (dtStart >= new Date("2011/10/10 10:00") && dtStart < new Date("2011/10/10 11:00")) {
                $("#eventH10").append('<a id="lnkViewWorkId' + result.Id + '" href="' + urlRoot + 'QLHC/Work_Main.aspx?tsk_action=' + result.Status + '&WorkID=' + result.Id + '">' + formatDateTime(result.StartDate) + ' ' + result.Title + '</a><br />');
            }
            else if (dtStart >= new Date("2011/10/10 11:00") && dtStart < new Date("2011/10/10 12:00")) {
                $("#eventH11").append('<a id="lnkViewWorkId' + result.Id + '" href="' + urlRoot + 'QLHC/Work_Main.aspx?tsk_action=' + result.Status + '&WorkID=' + result.Id + '">' + formatDateTime(result.StartDate) + ' ' + result.Title + '</a><br />');
            }
            else if (dtStart >= new Date("2011/10/10 12:00") && dtStart < new Date("2011/10/10 13:00")) {
                $("#eventH12").append('<a id="lnkViewWorkId' + result.Id + '" href="' + urlRoot + 'QLHC/Work_Main.aspx?tsk_action=' + result.Status + '&WorkID=' + result.Id + '">' + formatDateTime(result.StartDate) + ' ' + result.Title + '</a><br />');
            }
            else if (dtStart >= new Date("2011/10/10 13:00") && dtStart < new Date("2011/10/10 14:00")) {
                $("#eventH13").append('<a id="lnkViewWorkId' + result.Id + '" href="' + urlRoot + 'QLHC/Work_Main.aspx?tsk_action=' + result.Status + '&WorkID=' + result.Id + '">' + formatDateTime(result.StartDate) + ' ' + result.Title + '</a><br />');
            }
            else if (dtStart >= new Date("2011/10/10 14:00") && dtStart < new Date("2011/10/10 15:00")) {
                $("#eventH14").append('<a id="lnkViewWorkId' + result.Id + '" href="' + urlRoot + 'QLHC/Work_Main.aspx?tsk_action=' + result.Status + '&WorkID=' + result.Id + '">' + formatDateTime(result.StartDate) + ' ' + result.Title + '</a><br />');
            }
            else if (dtStart >= new Date("2011/10/10 15:00") && dtStart < new Date("2011/10/10 16:00")) {
                $("#eventH15").append('<a id="lnkViewWorkId' + result.Id + '" href="' + urlRoot + 'QLHC/Work_Main.aspx?tsk_action=' + result.Status + '&WorkID=' + result.Id + '">' + formatDateTime(result.StartDate) + ' ' + result.Title + '</a><br />');
            }
            else if (dtStart >= new Date("2011/10/10 16:00") && dtStart < new Date("2011/10/10 17:00")) {
                $("#eventH16").append('<a id="lnkViewWorkId' + result.Id + '" href="' + urlRoot + 'QLHC/Work_Main.aspx?tsk_action=' + result.Status + '&WorkID=' + result.Id + '">' + formatDateTime(result.StartDate) + ' ' + result.Title + '</a><br />');
            }
            else if (dtStart >= new Date("2011/10/10 17:00")) {
                $("#eventH17").append('<a id="lnkViewWorkId' + result.Id + '" href="' + urlRoot + 'QLHC/Work_Main.aspx?tsk_action=' + result.Status + '&WorkID=' + result.Id + '">' + formatDateTime(result.StartDate) + ' ' + result.Title + '</a><br />');
            }
            if (result.Status == 1) {
                $("#lnkViewWorkId" + result.Id).attr("style", "color: blue");
                $("#lnkViewWorkId" + result.Id).attr("title", "Công việc xử lý chính");
                $("#lnkViewWorkId" + result.Id).attr("alt", "Công việc xử lý chính");
            }
            else if (result.Status == 2) {
                $("#lnkViewWorkId" + result.Id).attr("style", "color: green");
                $("#lnkViewWorkId" + result.Id).attr("title", "Công việc tham gia");
                $("#lnkViewWorkId" + result.Id).attr("alt", "Công việc tham gia");
            }
            else if (result.Status == 3) {
                $("#lnkViewWorkId" + result.Id).html('<i>' + $("#lnkViewWorkId" + result.Id).text() + '</i>');
                $("#lnkViewWorkId" + result.Id).attr("style", "color: grey");
                $("#lnkViewWorkId" + result.Id).attr("title", "Công việc theo dõi");
                $("#lnkViewWorkId" + result.Id).attr("alt", "Công việc theo dõi");
            }
        });
    }
}
function drawCalendar(currDate, duration) {
    var ltTitle = 'Tháng ' + parseInt(new Date(currDate).getMonth() + 1) + ' - ' + new Date(currDate).getFullYear();
    $("#lblDateTitle1").html(ltTitle);
    $("#" + tblContentId).attr("style", "display: none");
    $("#" + divLoading).attr("style", "");
    $("#" + divLoading).html('<div style="position:absolute; top:40%; left:55%; width:300px; height:321px; z-index:1;"><img src="' + urlRoot + 'images/Loading.gif" /></div>');

    renderSimpleMonth(tblSimpleMonthId, currDate);
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
    window.location = "EventsList.aspx#dur1,currDate"+dateView+",uid56";
//    currDate = dateView;
//    duration = 1;
//    eventBuildHref();
//    drawCalendar(currDate, duration);
}
function render1Day(element, currDate) {
    var tblDay = '<table width="100%" cellspacing="0" cellpadding="0" border="0">';
    tblDay += '		<tr><td></td></tr>';
    tblDay += '      <tr>';
    tblDay += '          <td>';
    tblDay += '              <table width="100%" cellspacing="1" cellpadding="1" border="0" class="PRC_BgTable">';
    tblDay += '                    <tr><td width="100%" class="PRC_TodayHead"><span class="PRC_DayName">&nbsp;&nbsp;' + new Date(currDate).getDate() + '/' + parseInt(new Date(currDate).getMonth() + 1) + '/' + new Date(currDate).getFullYear() + ' </span></td></tr>';
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
        if (todayDate == getDayInWeek(currDate, i).getFullYear() + '/' + parseInt(getDayInWeek(currDate, i).getMonth() + 1) + '/' + getDayInWeek(currDate, i).getDate()) {
            tblWeek += '        <td width="20%" valign="middle" height="60" align="center" class="PRC_TodayHead">';
        }
        else { tblWeek += '        <td width="20%" valign="middle" height="60" align="center" class="PRC_DayHead">'; }
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
    var tblMonth = '<table width="100%" cellspacing="0" cellpadding="0" border="0">';
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
            if (todayDate == getDayInWeek(crDate, i).getFullYear() + '/' + parseInt(getDayInWeek(crDate, i).getMonth() + 1) + '/' + getDayInWeek(crDate, i).getDate()) {
                tblMonth += '        <td valign="top" class="PRC_TodayTable">';
            }
            else tblMonth += '        <td valign="top" class="PRC_InMonthTable">';
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
            if (todayDate == getDayInWeek(crDate, i).getFullYear() + '/' + parseInt(getDayInWeek(crDate, i).getMonth() + 1) + '/' + getDayInWeek(crDate, i).getDate()) {
                tblMonth += '        <td valign="top" class="PRC_TodayTable">';
            }
            else tblMonth += '        <td valign="top" align="center" class="PRC_InMonthTable">';
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
$(document).ready(function () {
    if (getCurrentAction() != eventLastUrl) {
        parseFilter();
        drawCalendar(currDate, duration);
    }
});
function updateEventStatus(eventId, status) {
    $("#eventDetail").html("Đang cập nhật dữ liệu...");
    $.ajax({
        type: 'GET',
        url: urlRoot + "Handlers/CalendarEventsHandler.ashx?handler=updatestatus" + "&eventId=" + eventId + "&status=" + status + "&sign=" + sign + "&userId=" + userId,
        processData: true,
        success: function (data) {
            if (data != null) {
                if (data != "-1") {
                    $("#eventDetail").html(data);
                    $("#imgApproved" + eventId).attr("style", "display: none");
                    $("#imgRejected" + eventId).attr("style", "display: none");
                    $("#lnkViewId" + eventId).html($("#lnkViewId" + eventId).text());
                    $("#lnkViewId" + eventId).attr("style", "");
                    $("#lnkViewId" + eventId).attr("title", "");
                    $("#lnkViewId" + eventId).attr("alt", "");
                    if (status == 2) {
                        $("#imgApproved" + eventId).attr("style", "");
                    }
                    else if (status == 3) {
                        $("#imgRejected" + eventId).attr("style", "");
                    }
                    else if (status == 4) {
                        $("#lnkViewId" + eventId).html('<strike>' + $("#lnkViewId" + eventId).text() + '</strike>');
                        $("#lnkViewId" + eventId).attr("style", "color: red");
                        $("#lnkViewId" + eventId).attr("title", "Đã hủy");
                        $("#lnkViewId" + eventId).attr("alt", "Đã hủy");
                    }
                }
                else $("#eventDetail").html("Xin lỗi, quyền cập nhật dữ liệu của bạn không hợp lệ !");
            }
        }
    });
}
function deleteEvent(eventId) {
    if (confirm('Bạn có chắc chắn muốn xóa lịch biểu này?')) {
        $("#eventDetail").html("Đang cập nhật dữ liệu...");
        $.ajax({
            type: 'GET',
            url: urlRoot + "Handlers/CalendarEventsHandler.ashx?handler=deleteEvent" + "&eventId=" + eventId + "&sign=" + sign + "&userId=" + userId,
            processData: true,
            success: function (data) {
                if (data != null) {
                    if (data != "-1") {
                        document.location.href = document.location.href;

                        //                        $("#eventDetail").html(data);
                        //                        $("#imgApproved" + eventId).remove();
                        //                        $("#imgRejected" + eventId).remove();
                        //                        $("#lnkViewId" + eventId).remove();
                    }
                    else $("#eventDetail").html("Xin lỗi, quyền cập nhật dữ liệu của bạn không hợp lệ !");
                }
            }
        });
    }
}
function editEvent(eventId) {
    document.location = urlRoot + "EventCalendar/EditEvent.aspx?id=" + eventId;
}
function viewEventDetail(eventId) {
    var divDetail = '<div id="eventDetail">Đang tải ...</div>';
    SystemDialog.Alert(divDetail, "Chi tiết sự kiện");
    var divDetailContent = '';
    $.ajax({
        type: 'GET',
        url: urlRoot + "Handlers/CalendarEventsHandler.ashx?handler=getdetail" + "&eventId=" + eventId + "&userId=" + userId,
        processData: true,
        dataType: "json",
        cache: false,
        success: function (data) {
            if (data != null) {
                divDetailContent += '<table width="100%" cellspacing="2" cellpadding="2" border="0">';
                divDetailContent += '<tr>';
                divDetailContent += '<td width="30%" class="FormSubject">Tên sự kiện</td>';
                divDetailContent += '<td width="70%" colspan="2">';
                divDetailContent += '	' + data.Title + '</td>';
                divDetailContent += '</tr>';
                divDetailContent += '<tr>';
                divDetailContent += '<td width="30%" class="FormSubject">Thời gian thực hiện</td>';
                divDetailContent += '<td width="70%" colspan="2">';
                divDetailContent += formatDateTime(data.StartDate) + '&nbsp;</td>';
                divDetailContent += '</tr>';
                divDetailContent += '<tr>';
                divDetailContent += '<td width="30%" class="FormSubject">Người tạo</td>';
                divDetailContent += '<td width="70%" colspan="2">';
                divDetailContent += data.OwnerName + '&nbsp;</td>';
                divDetailContent += '</tr>';
                divDetailContent += '<tr>';
                divDetailContent += '<td width="30%" class="FormSubject">Nội dung sự kiện</td>';
                divDetailContent += '<td width="70%" colspan="2">';
                divDetailContent += data.Description + '&nbsp;</td>';
                divDetailContent += '</tr>';
                divDetailContent += '<tr>';
                divDetailContent += '<td width="30%" class="FormSubject">Tài nguyên sử dụng</td>';
                divDetailContent += '<td width="70%" colspan="2">';
                divDetailContent += data.Resources + '&nbsp;</td>';
                divDetailContent += '</tr>';
                divDetailContent += '<tr>';
                divDetailContent += '   <td width="30%" class="FormSubject" valign="top">Thành phần tham gia</td>';
                divDetailContent += '   <td width="70%" colspan="2">';
                divDetailContent += '       <table width="100%" cellspacing="2" cellpadding="2" border="0">';
                $.each(data.Members, function (i, result) {
                    divDetailContent += '       <tr>';
                    divDetailContent += '           <td width="50%" class="FormSubject">' + result.FullName + '</td>';
                    divDetailContent += '           <td width="50%" colspan="2">';
                    if (result.Status == 1) {
                        divDetailContent += 'Chờ duyệt';
                    }
                    else if (result.Status == 2) {
                        divDetailContent += '<img border="0" align="absmiddle" title="Đã chấp nhận" src="' + urlRoot + 'images/img_Calendar/icon_approved.gif"> Chấp nhận';
                    }
                    else if (result.Status == 3) {
                        divDetailContent += '<img border="0" align="absmiddle" title="Đã từ chối" src="' + urlRoot + 'images/img_Calendar/icon_rejected.gif"> Từ chối';
                    }
                    else if (result.Status == 4) {
                        divDetailContent += '<strike><span style="color: red;">Hủy tham gia</span></strike>';
                    }
                    divDetailContent += '       </td></tr>';
                });
                divDetailContent += '       </table>';
                divDetailContent += '   </td>';
                divDetailContent += '</tr>';
                divDetailContent += '<tr>';
                divDetailContent += '<td width="100%" colspan="3" align="center">';
                if (userId == sessionUserId) {
                    divDetailContent += '<button type="button" class="ui-state-default ui-corner-all" onclick="updateEventStatus(' + data.Id + ',3)">Từ chối</button>&nbsp;';
                    divDetailContent += '<button type="button" class="ui-state-default ui-corner-all" onclick="updateEventStatus(' + data.Id + ',2)">Chấp nhận</button>&nbsp;';
                    if (sessionUserId == data.OwnerId) {
                        divDetailContent += '<button type="button" class="ui-state-default ui-corner-all" onclick="editEvent(' + data.Id + ')">Sửa</button>&nbsp;';
                    }
                    divDetailContent += '<button type="button" class="ui-state-default ui-corner-all" onclick="updateEventStatus(' + data.Id + ',4)">Hủy</button>&nbsp;';
                    divDetailContent += '<button type="button" class="ui-state-default ui-corner-all" onclick="deleteEvent(' + data.Id + ')">Xóa</button>&nbsp;';
                }
                divDetailContent += '</td>';
                divDetailContent += '</tr>';

                divDetailContent += '</table>';
                $("#eventDetail").html(divDetailContent);
            }
        }
    });
}
function expandDepartment(departmentId) {
    $("#tblListMember").find("#rowDeparment" + departmentId).attr("style", "");
    $("#collapse" + departmentId).attr("style", "");
    $("#expand" + departmentId).attr("style", "display: none");
}
function collapseDepartment(departmentId) {
    $("#tblListMember").find("#rowDeparment" + departmentId).attr("style", "display: none");
    $("#collapse" + departmentId).attr("style", "display: none");
    $("#expand" + departmentId).attr("style", "");
}
/* chuyen xem lich cua mot thanh vien nao do trong phong */
function setCurrUser(uId, fullName) {
    userId = uId;
    currFullName = fullName;
    eventBuildHref();
    drawCalendar(currDate, duration);
    pnlClose();
}
/* hien thi danh sach cac thanh vien trong phong, cty */
function showListUser() {
    var divDetail = '<div id="lsListMember">Đang tải ...</div>';
    SystemDialog.Alert(divDetail, "Chọn người trong danh sách");
    var divDetailContent = '';
    $.ajax({
        type: 'GET',
        url: urlRoot + "Handlers/CalendarEventsHandler.ashx?handler=getbyposition",
        processData: true,
        dataType: "json",
        success: function(data) {
            if (data != '') {
                var departmentName = data[0].DepartmentName;
                var departmentID = data[0].DepartmentID;
                divDetailContent += '<table id="tblListMember" width="100%" cellspacing="2" cellpadding="2" border="0">';
                divDetailContent += '<tr>';
                divDetailContent += '<td width="10%" class="FormSubject"></td>';
                divDetailContent += '<td width="40%" class="FormSubject">Họ và tên</td>';
                divDetailContent += '<td width="50%" class="FormSubject">Chức danh</td>';
                divDetailContent += '</tr>';
                divDetailContent += '<tr>';
                divDetailContent += '<td width="100%" class="FormSubject" colspan="3"><a id="expand' + data[0].DepartmentID + '" href="javascript:;" onclick="expandDepartment(' + data[0].DepartmentID + ')"><img border="0" align="absmiddle" src="' + urlRoot + 'images/img_Calendar/m_plus.gif" id="imgSessionD"></a><a id="collapse' + data[0].DepartmentID + '" style="display: none" href="javascript:;" onclick="collapseDepartment(' + data[0].DepartmentID + ')"><img border="0" align="absmiddle" src="' + urlRoot + 'images/img_Calendar/m_minus.gif" id="imgSessionD"></a> ' + data[0].DepartmentName + '</td>';
                divDetailContent += '</tr>';
                $.each(data, function(i, result) {
                    if (departmentID != result.DepartmentID) {
                        divDetailContent += '<tr>';
                        divDetailContent += '<td width="100%" class="FormSubject" colspan="3"><a id="expand' + result.DepartmentID + '" href="javascript:;" onclick="expandDepartment(' + result.DepartmentID + ')"><img border="0" align="absmiddle" src="' + urlRoot + 'images/img_Calendar/m_plus.gif" id="imgSessionD"></a><a id="collapse' + result.DepartmentID + '" style="display: none" href="javascript:;" onclick="collapseDepartment(' + result.DepartmentID + ')"><img border="0" align="absmiddle" src="' + urlRoot + 'images/img_Calendar/m_minus.gif" id="imgSessionD"></a> ' + result.DepartmentName + '</td>';
                        divDetailContent += '</tr>';
                        departmentID = result.DepartmentID;
                    }
                    divDetailContent += '<tr style="display: none;" id="rowDeparment' + result.DepartmentID + '">';
                    divDetailContent += '<td width="10%"></td>';
                    divDetailContent += '<td width="40%"><a href="javascript:;" onclick="setCurrUser(' + result.ID + ',\'' + result.FullName + '\')">' + result.FullName + '<span style="display:none">' + result.Email + '</span></td>';
                    divDetailContent += '<td width="50%">' + result.PositionName + '</td>';
                    divDetailContent += '</tr>';
                });
                divDetailContent += '<tr>';
                divDetailContent += '<td width="100%" class="FormSubject" colspan="3">';
                divDetailContent += '<button type="button" onclick="pnlClose()" class="ui-state-default ui-corner-all">Thoát</button>&nbsp;';
                divDetailContent += '</td>';
                divDetailContent += '</tr>';
                divDetailContent += '</table>';
            } else {
                divDetailContent += '<table id="tblListMember" width="100%" cellspacing="2" cellpadding="2" border="0">';
                divDetailContent += '<tr><td>Không có thông tin nhân sự.</td></tr>';
                divDetailContent += '<tr>';
                divDetailContent += '<td class="FormSubject">';
                divDetailContent += '<button type="button" onclick="pnlClose()" class="ui-state-default ui-corner-all">Thoát</button>&nbsp;';
                divDetailContent += '</td>';
                divDetailContent += '</tr>';
                divDetailContent += '</table>';
            }
            $("#lsListMember").html(divDetailContent);
        }
    });
}