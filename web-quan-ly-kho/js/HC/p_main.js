/**
*
*  Javascript trim, ltrim, rtrim
*  http://www.webtoolkit.info/
*
*
**/

function trim(str, chars) {
    return ltrim(rtrim(str, chars), chars);
}

function ltrim(str, chars) {
    chars = chars || "\\s";
    return str.replace(new RegExp("^[" + chars + "]+", "g"), "");
}

function rtrim(str, chars) {
    chars = chars || "\\s";
    return str.replace(new RegExp("[" + chars + "]+$", "g"), "");
}

function newWindow(file,window) {
	msgWindow=open(file,window,'resizable=yes,width=520,height=600,scrollbars=yes,top=80,left=400');
}
function getObj(id,d)
{
	var i,x;
	if(!d)d=document;
	if(!(x=d[id])&&d.all)x=d.all[id];
	for(i=0;!x&&i<d.forms.length;i++)x=d.forms[i][id];
	for(i=0;!x&&d.layers&&i<d.layers.length;i++)x=getObj(id,d.layers[i].document);
	if(!x&&document.getElementById)x=document.getElementById(id);
	return x;
};

/*  
Script made by Martial Boissonneault © 2001-2003 http://getElementById.com/
This script may be used and changed freely as long as this msg is intact
Visit http://getElementById.com/ for more free scripts and tutorials.
*/
function SwitchMenu(obj){
	var el = getObj(obj);
	var ar = getObj('cont').getElementsByTagName('span');
	if(el.style.display == "none"){
		/*
		for (var i=0; i<ar.length; i++){
			ar[i].style.display = "none";
		}
		*/
		for (var i=0; i<ar.length; i++){
			if (ar[i].className=="pSecondMenu") //DynamicDrive.com change
			ar[i].style.display = "none";
		}
		
		el.style.display = "block";
	}else{
		el.style.display = "none";
	}
};

function ChangeClass(menu, newClass) {
	tempClassName = document.getElementById(menu).className; 
	if (document.getElementById) { 
	 	document.getElementById(menu).className = newClass;
	} 
};

function ResetClass(menu) {
	document.getElementById(menu).className = tempClassName;
};

//document.onselectstart = new Function("return false");

function addOption(oList, oOption) {
	oList.options[oList.options.length] = oOption;
}

function changeSelectedValueDynaList(oList1, oList2) {
	oList2.selectedIndex = oList1.selectedIndex;re;
}

function populateDynaList(oList, nIndex, aArray){
	//oList[oList.length]= new Option("Please Select");
	for (var i = 0; i < aArray.length; i= i + 3){
		if (aArray[i] == nIndex){
			oList.options[oList.options.length] = new Option(aArray[i + 1], aArray[i + 2]);
		}
		//oList.size=oList.length //You need to comment out this line of the function if you use this mod
	}
	//A quick mod here, I changed the ==0 to ==1 so that the length takes into account the Please select option
	//from above.
	//if (oList.options.length == 1){
	//	oList.options[oList.options.length] = new Option(": No SubCategories Available :");
	//}
	//oList.selectedIndex = 0;
};
function clearDynaList(oList){
	for (var i = oList.options.length; i >= 0; i--){
	oList.options[i] = null;
	}
	oList.selectedIndex = -1;
};

function setDynaListLevel2s(oList1, oList2, arrLevel2s) {
	clearDynaList(oList2);
	var oOption = new Option('---Lựa chọn---', '');
	addOption(oList2, oOption);
	if (oList1.selectedIndex == -1){
		oList1.selectedIndex = 0;
	}
	populateDynaList(oList2, oList1[oList1.selectedIndex].value, arrLevel2s);
};

function setDynaListLevel3s(oList2, oList3, arrLevel3s) {
	if (oList2.selectedIndex == -1){
		oList2.selectedIndex = 0;
	}
	populateDynaList(oList3, oList2[oList2.selectedIndex].value, arrLevel3s);
};

function setDynaListLevel4s(oList3, oList4, arrLevel4s) {
	if (oList3.selectedIndex == -1){
		oList3.selectedIndex = 0;
	}
	populateDynaList(oList4, oList3[oList3.selectedIndex].value, arrLevel4s);
};

function confirmDelete() {
	return confirm("Bạn chắc chắn xoá chứ?");	
};
	
function changePage(idList, idPageNumber, idButton) {
	document.getElementById(idPageNumber).value = document.getElementById(idList).options[document.getElementById(idList).selectedIndex].value;
	__doPostBack(idButton,'');
};
function sortPage(idButton) {
	__doPostBack(idButton,'');
};

function checkTreeAll(chkParentID, chkChildIDs) {
	var chkParent = document.getElementById(chkParentID);
	for(var i = 0; i < chkChildIDs.length; i++){
		document.getElementById(chkChildIDs[i]).checked = chkParent.checked;	
	}	
	for(var i = 0; i < chkChildIDs.length; i++){
		if (document.getElementById(chkChildIDs[i]).onclick) {
			document.getElementById(chkChildIDs[i]).onclick();
		}		
	}		
}

function checkTreeChanged(chkParentID, chkChildIDs) {
	var chkParent = document.getElementById(chkParentID);
	var isAllChecked = true;
	for(var i = 0; i < chkChildIDs.length; i++){
		if (document.getElementById(chkChildIDs[i]).checked == false) {
			isAllChecked = false;
			break;
		}		
	}
	chkParent.checked = isAllChecked;
}

function checkAll(chkParentID, chkChildIDs) {
	var chkParent = document.getElementById(chkParentID);	
	for(var i = 0; i < chkChildIDs.length; i++){
		//document.getElementById(chkChildIDs[i]).click();
		document.getElementById(chkChildIDs[i]).checked = chkParent.checked;
	}
}

function checkChanged(chkParentID, chkChildIDs) {
	var chkParent = document.getElementById(chkParentID);
	var isAllChecked = true;
	for(var i = 0; i < chkChildIDs.length; i++){
		if (document.getElementById(chkChildIDs[i]).checked == false) {
			isAllChecked = false;
			break;
		}		
	}
	chkParent.checked = isAllChecked;
}

function hasSelected(chkChildIDs) {
	for(var i = 0; i < chkChildIDs.length; i++){
		if (document.getElementById(chkChildIDs[i]).checked == true) {
			return true;
		}		
	}
	return false;	
}

function disable(clientId, uniqueId) {
	document.getElementById(clientId).setAttribute('disabled', 'disabled');
	document.getElementById(clientId).removeAttribute('href');
}
function enable(clientId, uniqueId) {
	document.getElementById(clientId).removeAttribute('disabled');
	document.getElementById(clientId).setAttribute('href', 'javascript:__doPostBack(\'' + uniqueId + '\',\'' + '\')');
}

/**
    Created by: Michael Synovic
    on: 01/12/2003
    
    This is a Javascript implementation of the Java Hashtable object.
    
    Contructor(s):
     Hashtable()
              Creates a new, empty hashtable
    
    Method(s):
     void clear() 
              Clears this hashtable so that it contains no keys. 
     boolean containsKey(String key) 
              Tests if the specified object is a key in this hashtable. 
     boolean containsValue(Object value) 
              Returns true if this Hashtable maps one or more keys to this value. 
     Object get(String key) 
              Returns the value to which the specified key is mapped in this hashtable. 
     boolean isEmpty() 
              Tests if this hashtable maps no keys to values. 
     Array keys() 
              Returns an array of the keys in this hashtable. 
     void put(String key, Object value) 
              Maps the specified key to the specified value in this hashtable. A NullPointerException is thrown is the key or value is null.
     Object remove(String key) 
              Removes the key (and its corresponding value) from this hashtable. Returns the value of the key that was removed
     int size() 
              Returns the number of keys in this hashtable. 
     String toString() 
              Returns a string representation of this Hashtable object in the form of a set of entries, enclosed in braces and separated by the ASCII characters ", " (comma and space). 
     Array values() 
              Returns a array view of the values contained in this Hashtable. 
            
*/
function Hashtable(){
    this.clear = hashtable_clear;
    this.containsKey = hashtable_containsKey;
    this.containsValue = hashtable_containsValue;
    this.get = hashtable_get;
    this.isEmpty = hashtable_isEmpty;
    this.keys = hashtable_keys;
    this.put = hashtable_put;
    this.remove = hashtable_remove;
    this.size = hashtable_size;
    this.toString = hashtable_toString;
    this.values = hashtable_values;
    this.hashtable = new Array();
}

/*=======Private methods for internal use only========*/

function hashtable_clear(){
    this.hashtable = new Array();
}

function hashtable_containsKey(key){
    var exists = false;
    for (var i in this.hashtable) {
        if (i == key && this.hashtable[i] != null) {
            exists = true;
            break;
        }
    }
    return exists;
}

function hashtable_containsValue(value){
    var contains = false;
    if (value != null) {
        for (var i in this.hashtable) {
            if (this.hashtable[i] == value) {
                contains = true;
                break;
            }
        }
    }
    return contains;
}

function hashtable_get(key){
    return this.hashtable[key];
}

function hashtable_isEmpty(){
    return (parseInt(this.size()) == 0) ? true : false;
}

function hashtable_keys(){
    var keys = new Array();
    for (var i in this.hashtable) {
        if (this.hashtable[i] != null) 
            keys.push(i);
    }
    return keys;
}

function hashtable_put(key, value){
    if (key == null || value == null) {
        throw "NullPointerException {" + key + "},{" + value + "}";
    }else{
        this.hashtable[key] = value;
    }
}

function hashtable_remove(key){
    var rtn = this.hashtable[key];
    this.hashtable[key] = null;
    return rtn;
}

function hashtable_size(){
    var size = 0;
    for (var i in this.hashtable) {
        if (this.hashtable[i] != null) 
            size ++;
    }
    return size;
}

function hashtable_toString(){
    var result = "";
    for (var i in this.hashtable)
    {      
        if (this.hashtable[i] != null) 
            result += "{" + i + "},{" + this.hashtable[i] + "}\n";   
    }
    return result;
}

function hashtable_values(){
    var values = new Array();
    for (var i in this.hashtable) {
        if (this.hashtable[i] != null) 
            values.push(this.hashtable[i]);
    }
    return values;
}

function initStatus(leftpaneid, seperatepaneid) {
	var leftpane = document.getElementById(leftpaneid);
	var seperatepane = document.getElementById(seperatepaneid);
	var img = seperatepane.getElementsByTagName('img')[0];	
	if (getCookie(leftpaneid) == 'none') {		
		leftpane.style.display = 'none';
		seperatepane.setAttribute('title', 'Nhấn vào đây để hiện thanh công cụ - Ctrl+R');
		img.setAttribute('width', '6');
		img.setAttribute('title', 'Nhấn vào đây để hiện thanh công cụ - Ctrl+R');
		seperatepane.className = 'small-separator-pane';
	} else {
		leftpane.style.display = 'block';
		seperatepane.setAttribute('title', 'Nhấn vào đây để ẩn thanh công cụ - Ctrl+R');
		img.setAttribute('width', '6');
		img.setAttribute('title', 'Nhấn vào đây để ẩn thanh công cụ - Ctrl+R');
		seperatepane.className = 'large-separator-pane';
	}		
}
function toggleLeftPane(leftpaneid, seperatepaneid) {
	var leftpane = document.getElementById(leftpaneid);
	var seperatepane = document.getElementById(seperatepaneid);
	var img = seperatepane.getElementsByTagName('img')[0];
	if (leftpane.style.display == 'block') {
		leftpane.style.display = 'none';
		img.setAttribute('width', '6');
		img.setAttribute('title', 'Nhấn vào đây để hiện thanh công cụ - Ctrl+R');
		seperatepane.setAttribute('title', 'Nhấn vào đây để hiện thanh công cụ - Ctrl+R');
		seperatepane.className = 'large-separator-pane';
		setCookie(leftpaneid, 'none', null);
	} else {
		leftpane.style.display = 'block';
		img.setAttribute('width', '6');
		img.setAttribute('title', 'Nhấn vào đây để ẩn thanh công cụ - Ctrl+R');
		seperatepane.setAttribute('title', 'Nhấn vào đây để ẩn thanh công cụ - Ctrl+R');
		seperatepane.className = 'small-separator-pane';
		setCookie(leftpaneid, 'block', null);
	}
}

function toggleWindow(tableid, lnkid) {
	var table = document.getElementById(tableid);
	var lnk = document.getElementById(lnkid);
	var img = lnk.getElementsByTagName('tr')[0].getElementsByTagName('td')[0].getElementsByTagName('img')[0];
	if (table.style.display == 'inherit') {
		table.style.display = 'none';			
		img.setAttribute('src', '../images/QLCV/arrow-down.png');

		img.setAttribute('title', 'Phóng to');
		
	} else {
		table.style.display = 'inherit';
		img.setAttribute('src', '../images/QLCV/arrow-up.png');

		img.setAttribute('title', 'Thu nhỏ');			
	}
}

function initWindow(tableid, lnkid) {
	var table = document.getElementById(tableid);
	var lnk = document.getElementById(lnkid);
	var img = lnk.getElementsByTagName('tr')[0].getElementsByTagName('td')[0].getElementsByTagName('img')[0];
	if (table.style.display == 'block') {
		img.setAttribute('src', '/modules/workflow/images/minimize.gif');
		img.setAttribute('title', 'Thu nhỏ');
		
	} else {
		img.setAttribute('src', '/modules/workflow/images/maximize.gif');
		img.setAttribute('title', 'Phóng to');			
	}
}

function updateWindowStatus(tableid, hiddenid) {
	var table = document.getElementById(tableid);
	var hidden = document.getElementById(hiddenid);
	if (table.style.display == 'block') {
		hidden.value = '0';
	} else {
		hidden.value = '1';
	}
}
	
function setCookie(c_name,value,expiredays)
{
	var exdate=new Date();
	exdate.setDate(exdate.getDate()+expiredays);
	document.cookie=c_name+ "=" +escape(value)+	((expiredays==null) ? "" : ";expires="+exdate.toGMTString()) + ";path=/";
}	
function getCookie(c_name)
{
	if (document.cookie.length > 0)
	{
		c_start=document.cookie.indexOf(c_name + "=");
		if (c_start!=-1)
		{ 
			c_start=c_start + c_name.length+1; 
			c_end=document.cookie.indexOf(";",c_start);
			if (c_end==-1) c_end=document.cookie.length;
			return unescape(document.cookie.substring(c_start,c_end));
		} 
	}
	return "";
}



String.prototype.isArgument=function()
{
	return /^([a-zA-Z]){1,}=([0-9]){1,}$/.test(this);
}


/*
call this function just work like window.open(url,name,feature);
however, for IE5.0+, it will open a showModelessDialog window;
and For Gecko(Mozilla or Netscape), the child window will stay on top focus untill user close it.
programmed by hedger
hedger@yahoo-inc.com
*/


function dialog(url,name,feature,isModal)
{
 if(url==null){return false;}
 url = url
 if(name==null){name=""}
 if(feature==null){feature=""};
 if(window.showModelessDialog)
 {
  	var WindowFeature = new Object();
	WindowFeature["width"] = 400;
	WindowFeature["height"]  =400;
	WindowFeature["left"]  = "";
	WindowFeature["top"]  =  "";
	WindowFeature["resizable"]  = "";

	if(feature !=null && feature!="")
	{
      feature = ( feature.toLowerCase()).split(",");
	
      for(var i=0;i< feature.length;i++)
		{
          if( feature[i].isArgument())
			{
               var featureName = feature[i].split("=")[0];
			   var featureValue = feature[i].split("=")[1];
			  
			   if(WindowFeature[featureName]!=null){WindowFeature[featureName] = featureValue; }
			}
		}
	}
 
  if(WindowFeature["resizable"]==1 || WindowFeature["resizable"]=="1" || WindowFeature["resizable"].toString().toLowerCase()=="yes"){WindowFeature["resizable"] = "resizable:1;minimize:1;maximize:1;"}
  if(WindowFeature["left"]!=""){WindowFeature["left"] ="dialogLeft:" +  WindowFeature["left"] +"px;";}
  if(WindowFeature["top"]!=""){WindowFeature["top"] ="dialogTop:" +  WindowFeature["Top"] +"px;"; }
  if(window.ModelessDialog ==null){window.ModelessDialog = new Object() ; };
  if(name!="")
  {
   if(window.ModelessDialog[name]!=null && !window.ModelessDialog[name].closed )
   {
     window.ModelessDialog[name].focus();
	 return window.ModelessDialog[name];
   }
  }
	var F = WindowFeature["left"] +WindowFeature["top"] +  "dialogWidth:"+WindowFeature["width"] +" px;dialogHeight:"+WindowFeature["height"]+"px;center:1;help:0;" + WindowFeature["resizable"] +"status:0;unadorned:0;edge: raised; ;border:thick;"
	if(isModal)
	{
		window.showModalDialog(url,self,F);
		return false;
	}
	else
	{
		window.ModelessDialog[name] = window.showModelessDialog(url,self,F);
		return window.ModelessDialog[name];
	}	
 }
 else
 {
   if(document.getBoxObjectFor)
   {
	

	 if(isModal)
	 {		 
		 var Modal = window.open(url,name,"modal=1," + feature);
		 var ModalFocus = function()
		 {
			if(!Modal.closed){Modal.focus();}
			else{Modal =null;window.removeEventListener(ModalFocus,"focus");ModalFocus = null; };					
		 }
		 window.addEventListener( "focus",ModalFocus, false ); 
		 return false;
	 }
	 else
	 {
		return window.open(url,name,"modal=1," + feature);
	 }	 
   }
   else
   { 
     return window.open(url,name,feature);
   }
   //
 }
 return null;
}


 
   
function modal(url,feature)
{
	dialog(url,"",feature,true);
	return false;
}
