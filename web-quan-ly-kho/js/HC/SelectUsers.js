
function updateOpener1(id)
{
    var temp = document.getElementById('hddChildCountSessionP'+id.toString());    
    opener.updateSelectDepartment(temp.value);
    self.close();
    return false;
}

function updateOpener(isClose) {
    var users = '';
     var userID = '';
	var lastIndex = 0;
  var frm = document.MainForm;
  
     for (var i = 0; i< frm.elements.length; i++) {
         if ((frm.elements[i].name.indexOf('chkUserId') > -1)) {
			if ((frm.elements[i].checked == true) && (frm.elements[i].value != '')) {
				users = users + frm.elements[i].value + ';';
				
			}
        }
    }
	lastIndex = users.lastIndexOf(';');
	if (users.length - 1 == lastIndex)
	{
		users = users.substring(0,lastIndex);
}
  
	if (document.MainForm.hddOption == null) {
			  opener.updateSelectUsers(users);
	} else {

		switch(document.MainForm.hddOption.value) {
			case '1':
				opener.updateSelectUsers1(users);
				break    
			case '2':
				opener.updateSelectUsers2(users);
				break
			case '3':
				opener.updateSelectUsers3(users);
				break    
			case '4':
				opener.updateSelectUsers4(users);
				break    
			case '5':
				opener.updateSelectUsers5(users);
				break
			case '6':
				opener.updateSelectUsers3(users);
				break   
			default:
			  opener.updateSelectUsers(users);
			
		}
	}
  if (isClose == true){ self.close();}
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

	for(i=0;i< frm.length;i++)
	{                         
		e=frm.elements[i]; 
		if(e.type=='checkbox' && e.name.indexOf(itemName) != -1) {
			e.checked = chkState;
		}
	}
};

function UnCheckAll(itemName) {
	var frm = document.MainForm;
	for(i=0;i< frm.length;i++)
	{                         
		e=frm.elements[i]; 
		if(e.type=='checkbox' && e.name.indexOf(itemName) != -1) {
			e.checked = false;
		}
	}
}

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
	var childCount =  GetChildCount(hddChildCount);
	
	if (document.getElementById(eTarget).style.display=='')
		{
    		document.getElementById(eTarget).style.display='none';
    		changeSymbol('img' + eTarget, 'none')
    		if (childCount > 0) flipFlopSubGroups(eTarget, childCount, 'none')
		}
	else
		{
    		document.getElementById(eTarget).style.display='';
    		changeSymbol('img' + eTarget, '')
    		if (childCount > 0) flipFlopSubGroups(eTarget, childCount, '')
		}
}

function flipFlopAll(eTarget,eIma) {
    var hddChildCount = document.getElementById(eTarget);
    //alert(hddChildCount);
    
    changeSymbolAll('img' + eIma);
      
      var eTarget1 = document.getElementById('img' + eIma);
        
    var tables = document.getElementsByTagName('table');
//    alert(tables.length);
    for (i = 0; i < tables.length; i++) {
         
     //  alert(tables[i].id);
        if (tables[i].id.indexOf(eTarget) != -1 && tables[i].id != eTarget) {
//            alert(tables[i].id);
//            alert(tables[i].style.display);
            if (eTarget1.src.indexOf('plus.gif') != -1) {
//
                tables[i].style.display = 'none';
            }
            else {
                tables[i].style.display = '';

            }
        }
    }
   
}
function flipFlopSubGroups(parentTarget, count, display) {
	var eTarget = '' 
	for (var i = 1; i <= count; i++) {
		eTarget = parentTarget + ':' +  i;
		document.getElementById(eTarget).style.display = display;
		changeSymbol('img' + eTarget, display )
	}
}

function changeSymbol(eTarget, display) {

    var plusImg = "../images/QLCV/plus.gif";
    var minusImg = "../images/QLCV/minus.gif";
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
function changeSymbolAll(eTarget) {

    var plusImg = "../images/QLCV/plus.gif";
    var minusImg = "../images/QLCV/minus.gif";
   
    var eTarget1 = document.getElementById(eTarget);

    if (eTarget1 != undefined) {
//        alert(eTarget1.src);
//        alert(eTarget1.src.indexOf("plus"));
        if (eTarget1.src.indexOf('plus.gif') != -1)
            eTarget1.src = minusImg;
        else
            eTarget1.src = plusImg;

    }
}
