function FormatNumber(StrNumber)
{
//alert(StrNumber);
StrNumber = String(StrNumber);
var NumDot =3;
var NumSpace =".";
var NumComma =",";
var StrArr=new Array();
 // string[] StrArr = null;
	if (StrNumber.indexOf(",") > 0) {
        StrArr = StrNumber.split(",");
	} else if (StrNumber.indexOf(".") > 0) {
        StrArr = StrNumber.split(".");
	} else {
        StrArr = StrNumber.split(" ");
	}
	

	var i = 0;
	var k = 0;
	var StrFormatChar = "";
	k = 0;
	if (StrArr.length > 0) {
		for (i = StrArr[0].length - 1; i >= 0; i += -1) {
			k += 1;
			StrFormatChar += StrArr[0][i];
			if (k < StrArr[0].length && k % NumDot == 0) {
				StrFormatChar += NumSpace;
			}
		}
	}
	//'
	var StrFormat = "";
	for (i = StrFormatChar.length - 1; i >= 0; i += -1) {
		StrFormat += StrFormatChar[i];
	}
	if (StrArr.length > 1) {
		StrFormat += NumComma + StrArr[1];
	}
	return StrFormat;
}
///Chuyen xau thanh so float
function StrToFloat(StrNumber)
{
StrNumber = String(StrNumber);
StrNumber = StrNumber.replace(/\./g,"");

StrNumber = StrNumber.replace(/\,/g,".");
var fNum = parseFloat(StrNumber);
return fNum; 
}
////
function StrToFloatBegin(StrNumber)
{
StrNumber = String(StrNumber);
StrNumber = StrNumber.replace(/\./g,",");
//alert(StrNumber);
return StrNumber;
}
///chuyen xau thanh so int
function StrToInt(StrNumber)
{
if (StrNumber=="") return 0;
StrNumber = String(StrNumber);
StrNumber = StrNumber.replace(/\./g,"");
//alert(StrNumber);
StrNumber = StrNumber.replace(/\,/g,".");
var fNum =parseFloat(StrNumber).toFixed(0);
return fNum; 
}

function NumberToText(Number1,sNgoaiTe)
{
    var number = Number1;
        var     pre  =  new Array(
		" ",
		" một",
		" hai",
		" ba",
		" bốn",
		" năm",
		" sáu",
		" bảy",
		" tám",
		" chín"
	    );
         var unit = new Array (
		" ",
		" ngàn",
		" triệu",
		" tỉ"
	);

            var Result = "";
            var x = 0;
            var y = 0;
            var i = 0;

            if (Number1 == 0)
            {
                return "";
            }
          
            while (number > 0)
            {
                var tmpResult ="";
                
                x = parseInt(number % 1000);
               
                number = parseInt(number / 1000);
                
                y = parseInt(x / 100);
            
                if (y > 0)
                {
                    tmpResult = pre[y] + " trăm";
                  
                }
               // alert(tmpResult);
                y = parseInt(x % 100);
                if ((y > 0))
                {
                    if ((y < 10))
                    {
                        if ((x > 99))
                        {
                            tmpResult = tmpResult + " linh " + pre[y];
                        }
                        else
                        {
                            tmpResult = pre[y];
                        }
                    }
                    else if ((y < 20))
                    {
                        if (parseInt(y % 10) == 5)
                        {
                            tmpResult = tmpResult + " mười lăm";
                        }
                        else if (parseInt(y % 10) == 0)
                        {
                            tmpResult = tmpResult + " mười";
                        }
                        else
                        {
                            tmpResult = tmpResult + " mười " + pre[y % 10];
                        }
                    }
                    else
                    {
                        if (parseInt(y % 10) == 1)
                        {
                            tmpResult = tmpResult + pre[parseInt(y / 10)] + " mươi mốt";
                        }
                        else if (parseInt(y % 10) == 5)
                        {
                            tmpResult = tmpResult + pre[parseInt(y / 10)] + " mươi lăm";
                        }
                        else
                        {
                            tmpResult = tmpResult + pre[parseInt(y / 10)] + " mươi" + pre[parseInt(y % 10)];
                        }
                    }
                }
                 
                if (tmpResult.length > 0)
                {
                    Result = tmpResult + unit[i] + Result;
                }
                i += 1;

            }
         
            switch (sNgoaiTe)
            {
                case "VNĐ":
                    Result = Trim(Result) + " đồng";
                    break;
                case "USD":
                    Result = Trim(Result) + " đô la Mỹ";
                    break;
            }

            while (Result.indexOf("  ") > -1)
            {
                Result = Result.replace(/\  /g, " ");
            }

            Result = ToUpperFirstLetter( Result);
            
            return Trim(Result);
}
//////

     function ToUpperFirstLetter(sSource)
        {
            var str1 = null;
            var str2 = null;

            if (sSource == null)
            {
                return "";
            }
            sSource = Trim(sSource);
            if (sSource=="")
                return "";
            while (sSource.indexOf("  ") > -1)
            {
             sSource = sSource.replace(/\  /g, " ");
               
            }

            str1 = sSource.substring(0, 1).toUpperCase();
           
            str2 = sSource.substring(1, sSource.length);
             
            sSource = str1 + str2;
            return sSource;
        }
/////////////////
function lTrim(sString)
{
while (sString.substring(0,1) == ' ')
{
sString = sString.substring(1, sString.length);
}
return sString;
}

//Hàm tiếp theo là rTrim() bỏ những khoảng trắng ở đằng cuối chuỗi.

function rTrim(sString)
{
while (sString.substring(sString.length-1, sString.length) == ' ')
{
sString = sString.substring(0,sString.length-1);
}
return sString;
}
/////
function Trim(sString)
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