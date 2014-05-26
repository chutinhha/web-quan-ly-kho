/* ThuNV: cac ham thu vien ve dialog */
var SystemDialog = new function () {
    this.Init = function () {
        var strDialog = '';
        strDialog += '<div id="pnlDiaglogAlert" title="Thông báo" class="pop" style="display: none;">';
        strDialog += '<div id="pnlDiaglogAlertContent" style="width: 100%;"></div>';
        strDialog += '</div>';

        strDialog += '<div id="pnlDiaglogConfirm" title="Xác nhận" style="display:none;">';
        strDialog += '<span id="pnlDiaglogConfirmContent" style="width:100%"></span>';
        strDialog += '</div>';
        document.write(strDialog);
    }
    this.Alert = function (content, _title, callback, params) {
        var title = _title;
        if (title == '' || title == 'undefined') {
            title = "Thông báo";
        }
        var tblDlg = '';
        tblDlg += content;
        $("#pnlDiaglogAlertContent").html(tblDlg);
        $("#pnlDiaglogAlert").dialog({
            autoOpen: true,
            modal: true,
            overlay: {
                opacity: 0.5,
                background: "black"
            },
            resizable: false,
            title: title,
            buttons: {
                'Đóng': function () {
                    $(this).dialog('close');
                }
            },
            close: function () {
                if (typeof callback == 'function') {
                    if (typeof params != 'undefined') {
                        callback(params);
                    }
                    else {
                        callback();
                    }
                }
                $(this).dialog("destroy");
            }
        });
    }

    this.Confirm = function (title, content, callback, params) {
        $("#pnlDiaglogAlertContent").html(content);
        $("#pnlDiaglogAlert").dialog({
            autoOpen: true,
            modal: true,
            title: title,
            resizable: false,
            buttons: {
                'Bỏ qua': function () {
                    $(this).dialog('close');
                    return;
                },
                'Đồng ý': function () {
                    if (typeof params != 'undefined') {
                        callback(params);
                    }
                    else {
                        callback();
                    }
                    $(this).dialog('close');
                }
            },
            close: function () {
                $(this).dialog("destroy");
            }
        });
    }
}
try { SystemDialog.Init(); }
catch (e) { }
function pnlClose() {
    $("#pnlDiaglogAlert").dialog("close");
}