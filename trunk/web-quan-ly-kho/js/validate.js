// JScript File

function IsDate(strDate, strFormatDate)
{
    //Modified by chungbx 07/03/2008
    //Chi validate "dd/MM/yyyy" va "MM/dd/yyyy"
    var datePat = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
    var matchArray = strDate.match(datePat); // is the format ok?
    var datestatus=true;
    datemsg="";
    if (matchArray == null || matchArray[1]==null) 
    {
        datemsg="----- Please enter date as " + strFormatDate + " " + "\n";
        return false;
    }
    else
    {
        if(matchArray[3]==null || matchArray[5]==null)
        {
            datemsg="----- Please enter date as " + strFormatDate + " " + "\n";
            return false;
        }
    }

    if(strFormatDate=="MM/dd/yyyy" || strFormatDate=="MM-dd-yyyy")
    {
	    month = matchArray[1]; // p@rse date into variables
	    day = matchArray[3];
	    year = matchArray[5];
    }
    else //strFormatDate=="dd/MM/yyyy" || strFormatDate=="dd-MM-yyyy")
    {
	    day = matchArray[1]; // p@rse date into variables
	    month = matchArray[3];
	    year = matchArray[5];
    }

    if (month < 1 || month > 12) 
    { // check month range
        datemsg=datemsg + "----- Month must be between 1 and 12." + "\n";
        datestatus=false;
    }

    if (day < 1 || day > 31) 
    {
        datemsg=datemsg + "----- Day must be between 1 and 31." + "\n";
        datestatus=false;
    }

    if ((month==4 || month==6 || month==9 || month==11) && day==31) 
    {
        datemsg=datemsg + "----- Month " + month + " doesn`t have 31 days!" + "\n";
        datestatus=false;
    }

    if (month == 2) 
    { // check for february 29th
        var isleap = (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0));
        if (day > 29 || (day==29 && !isleap)) 
        {
            datemsg=datemsg + "----- February " + year + " doesn`t have " + day + " days!" + "\n";
            datestatus=false;
        }
    }
    return datestatus; 
}


function CheckSpace(textName)
{
    var theForm = document.forms[0];
    var len = theForm.elements.length;
    for (var j = 0; j < len; j++) 
    { 
        var e = theForm.elements[j];
        if (e.name.indexOf(textName)>=0) 
        {
            if(trimAll(e.value) == '')
            {
                alert('Bạn hãy nhập dữ liệu!');
                e.focus();
                return false;
            }
            else
            {
                return true;
            }
        }
    }
    return true;
}

function trimAll(sString) 
{
    while (sString.substring(0,1) == ' ')
    {
        sString = sString.substring(1, sString.length);
    }
    while (sString.substring(sString.length-1, sString.length) == ' ')
    {
        sString = sString.substring(0,sString.length-1);
    }
    return sString;
}
function CheckNumbers(evt, controlID)
{
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if ((charCode > 31 && (charCode < 48 || charCode > 57)))
    {                
        return false;
    }
    return true;
}
function CheckDate(textName,strFormatDate)
{
    var theForm = document.forms[0];
    var len = theForm.elements.length;
    for (var j = 0; j < len; j++) 
    { 
        var e = theForm.elements[j];
        if (e.name.indexOf(textName)>=0) 
        {
            
            if(trimAll(e.value) != '' && !IsDate(e.value,strFormatDate))
            {
                alert('Sai kiểu ngày tháng ('+ strFormatDate +')!');
                e.focus();
                return false;
            }
            else
            {
                return true;
            }
        }
    }
    return true;
}