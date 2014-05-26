function CheckAll(pCheckBox, itemName)
{
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
function updatePortalToOpener() {
	var PortalNames = '';
	var PortalIds = '';
	var value = '';
	var PortalId = '';
	var PortalName = '';
	var index = 0;
	for (var i = 0; i< MainForm.elements.length; i++) {
		if ((MainForm.elements[i].name.indexOf('chkPortalId') > -1)) {
			if ((MainForm.elements[i].checked == true)) {
				value = MainForm.elements[i].value;
				index = value.indexOf(":");						
				PortalId = value.substring(0, index)
				PortalName = value.substring(index + 1, value.length)
				if (PortalIds == '') {
					PortalIds = PortalId 
				} else {
					PortalIds = PortalIds +  ',' + PortalId 
				}
				if (PortalNames == '') {
					PortalNames = PortalName 
				} else {
					PortalNames = PortalNames +  ', ' + PortalName 
				}
			}
		}
	}
	var index = document.MainForm.hddOption.value;
	opener.updateSelectPortals(index, PortalIds, PortalNames);
	/*
	if (document.MainForm.hddOption == null) {
		opener.updateSelectPortals(PortalIds, PortalNames);
	} else {
		switch(MainForm.hddOption.value) {
			case '1':
				opener.updateSelectPortals1(PortalIds, PortalNames);
				break    
			case '2':
				opener.updateSelectPortals2(PortalIds, PortalNames);
				break
			case '3':
				opener.updateSelectPortals3(PortalIds, PortalNames);
				break    
			case '4':
				opener.updateSelectPortals4(PortalIds, PortalNames);
				break
			case '5':
				opener.updateSelectPortals5(PortalIds, PortalNames);
				break    
			case '6':
				opener.updateSelectPortals6(PortalIds, PortalNames);
				break
			case '7':
				opener.updateSelectPortals7(PortalIds, PortalNames);
				break
			default:
			  opener.updateSelectPortals(PortalIds, PortalNames);
		}
	}	*/		
	//self.close();
	return false;
}
function closeWindow() {
	opener.focus();
	self.close();
	return false;
}					
function finishUpdate() {
	updatePortalToOpener();
	closeWindow();
}