function updateGroupToOpener() {
    var GroupNames = '';
    var GroupIds = '';
    var value = '';
    var GroupId = '';
    var GroupName = '';
    var index = 0;

    for (var i = 0; i < MainForm.elements.length; i++) {
        if ((MainForm.elements[i].name.indexOf('chkGroupId') > -1)) {
            if ((MainForm.elements[i].checked == true)) {
                value = MainForm.elements[i].value;
                index = value.indexOf(":");

                GroupId = value.substring(0, index)
                GroupName = value.substring(index + 1, value.length)

                GroupIds = GroupIds + GroupId + ':';
                if (GroupNames == '') {
                    GroupNames = GroupName
                } else {
                    GroupNames = GroupNames + ', ' + GroupName
                }
            }
        }
    }

    var index = document.MainForm.hddOption.value;
    opener.updateSelectDepartment(index, GroupIds, GroupNames);

/*
    if (document.MainForm.hddOption == null) {
        opener.updateSelectGroups(GroupIds, GroupNames);
    } else {
        switch (MainForm.hddOption.value) {
            case '1':
                opener.updateSelectGroups1(GroupIds, GroupNames);
                break
            case '2':
                opener.updateSelectGroups2(GroupIds, GroupNames);
                break
            case '3':
                opener.updateSelectGroups3(GroupIds, GroupNames);
                break
            case '4':
                opener.updateSelectGroups4(GroupIds, GroupNames);
                break
            case '5':
                opener.updateSelectGroups5(GroupIds, GroupNames);
                break
            case '6':
                opener.updateSelectGroups6(GroupIds, GroupNames);
                break
            case '7':
                opener.updateSelectGroups7(GroupIds, GroupNames);
                break
            default:
                opener.updateSelectGroups(GroupIds, GroupNames);
        }
    }*/
    //self.close();
    return false;
}
function closeWindow() {
    opener.focus();
    self.close();
    return false;
}

// Ham nay check cac checkBox User 
// Va kiem tra neu bo check - bo lai group cha no neu co
function CheckAll(pCheckBox, itemName) {
    var frm = document.MainForm;
    var chkState = document.getElementById(pCheckBox).checked;

    for (i = 0; i < frm.length; i++) {
        e = frm.elements[i];
        if (e.type == 'checkbox' && e.name.indexOf(itemName) != -1) {
            e.checked = chkState;
        }
    }

    //if ((parentId != '') && (chkState == false))  { RemoveGroupParent(parentId); }
};
function GetChildCount(ControlId) {
    var count = 0
    hddChildCount = document.getElementById(ControlId);
    if (hddChildCount == undefined) {
        return count;
    } else {
        count = hddChildCount.value;
    }
    return count;
}

function flipFlop(eTarget) {
    var hddChildCount = 'hddChildCount' + eTarget;
    var childCount = GetChildCount(hddChildCount);

    if (document.getElementById(eTarget).style.display == '') {
        document.getElementById(eTarget).style.display = 'none';
        changeSymbol('img' + eTarget, 'none')
        if (childCount > 0) flipFlopSubGroups(eTarget, childCount, 'none')
    }
    else {
        document.getElementById(eTarget).style.display = '';
        changeSymbol('img' + eTarget, '')
        if (childCount > 0) flipFlopSubGroups(eTarget, childCount, '')
    }
}

function flipFlopSubGroups(parentTarget, count, display) {
    var eTarget = ''
    for (var i = 1; i <= count; i++) {
        eTarget = parentTarget + ':' + i;
        document.getElementById(eTarget).style.display = display;
        //changeSymbol('img' + eTarget, display )
    }
}

function changeSymbol(eTarget, display) {
    var plusImg = "/images/plus.gif";
    var minusImg = "/images/minus.gif";
    var eTarget1 = document.getElementById(eTarget);
    if (eTarget1 != undefined) {
        if (display == '') {
            eTarget1.src = minusImg;
        }
        else {
            eTarget1.src = plusImg;
        }
    }
}