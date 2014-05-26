using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
 using Nhansu_Lib.DB_Class;
using Yogesh.ExcelXml;
namespace QLCV.code.common
{
    public class clsCommonFunction
    {
        private DataTable _dtCode, _dtSarary;
        public void Split_FullName(string sFullName, ref string sLastName, ref string sFirstName)
        {
            try
            {
                sLastName = ""; sFirstName = "";
                int index = 0;
                string sName = "";
                sFullName = sFullName.Trim();
                while (sFullName.IndexOf("  ") >= 0)
                {
                    sFullName = sFullName.Replace("  ", " ");
                }
                for (int i = 0; i < sFullName.Length; i++)
                {
                    if (sFullName[i] == ' ')
                        index = i;
                    if (i == 0)
                        sName += sFullName[i].ToString().ToUpper();
                    else if ((i - 1) == index && index > 0)
                        sName += sFullName[i].ToString().ToUpper();
                    else
                        sName += sFullName[i].ToString();

                }
                if (index == 0)
                { sLastName = sName; sFirstName = ""; }
                else
                {
                    sLastName = sName.Substring(0, index).Trim();

                    sFirstName = sName.Substring(index + 1, sName.Length - index - 1).Trim();
                }

            }
            catch (Exception ex)
            {

            }
        }
        public  string FormatNumber(string StrNumber, byte NumDot, string NumSpace, string NumComma)
        {
            string[] StrArr = null;
            if (StrNumber.IndexOf(",") > 0)
            {
                StrArr = StrNumber.Split(',');
            }
            else if (StrNumber.IndexOf(".") > 0)
            {
                StrArr = StrNumber.Split('.');
            }
            else
            {
                StrArr = StrNumber.Split(' ');
            }
            int i = 0;
            int k = 0;
            string StrFormatChar = "";
            k = 0;
            if (StrArr.Length > 0)
            {
                for (i = StrArr[0].Length - 1; i >= 0; i += -1)
                {
                    k += 1;
                    StrFormatChar += StrArr[0][i];
                    if (k < StrArr[0].Length && k % NumDot == 0)
                    {
                        StrFormatChar += NumSpace;
                    }
                }
            }
            //'
            string StrFormat = "";
            for (i = StrFormatChar.Length - 1; i >= 0; i += -1)
            {
                StrFormat += StrFormatChar[i];
            }
            if (StrArr.Length > 1)
            {
                StrFormat += NumComma + StrArr[1];
            }
            return StrFormat;
        }
        /// <summary>
        /// ham doc so
        /// </summary>
        /// <param name="Number1"></param>
        /// <param name="sNgoaiTe"></param>
        /// <returns></returns>
        public  string NumberToText(double Number1, string sNgoaiTe)
        {
            double number = Number1;
            string[] pre = {
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
	    };
            string[] unit = {
		" ",
		" ngàn",
		" triệu",
		" tỉ"
	};

            string Result = "";
            long x = 0;
            long y = 0;
            long i = 0;

            if (Number1 == 0)
            {
                return "";
            }

            while (number > 0)
            {
                string tmpResult = "";
                x = (long)(number % 1000);
                number = number / 1000;
                y = x / 100;
                if (y > 0)
                {
                    tmpResult = pre[y] + " trăm";
                }
                y = x % 100;
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
                        if ((y % 10) == 5)
                        {
                            tmpResult = tmpResult + " mười lăm";
                        }
                        else if ((y % 10) == 0)
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
                        if ((y % 10) == 1)
                        {
                            tmpResult = tmpResult + pre[y / 10] + " mươi mốt";
                        }
                        else if ((y % 10) == 5)
                        {
                            tmpResult = tmpResult + pre[y / 10] + " mươi lăm";
                        }
                        else
                        {
                            tmpResult = tmpResult + pre[y / 10] + " mươi" + pre[y % 10];
                        }
                    }
                }
                if (tmpResult.Length > 0)
                {
                    Result = tmpResult + unit[i] + Result;
                }
                i += 1;

            }
            switch (sNgoaiTe)
            {
                case "VNĐ":
                    Result = Result.Trim() + " đồng";
                    break;
                case "USD":
                    Result = Result.Trim() + " đô la Mỹ";
                    break;
            }

            while (Result.IndexOf("  ") > -1)
            {
                Result = Result.Replace("  ", " ");
            }

            ToUpperFirstLetter(ref Result);
            return Result.Trim();
        }

        public  void ToUpperFirstLetter(ref string sSource)
        {
            string str1 = null;
            string str2 = null;

            if (sSource == null)
            {
                return;
            }
            sSource = sSource.Trim();
            if (string.IsNullOrEmpty(sSource))
                return;
            while (sSource.IndexOf("  ") > -1)
            {
                sSource = sSource.Replace("  ", " ");
            }

            str1 = sSource.Substring(0, 1).ToUpper();
            str2 = sSource.Substring(1, sSource.Length - 1);
            sSource = str1 + str2;
        }
       
    }
}