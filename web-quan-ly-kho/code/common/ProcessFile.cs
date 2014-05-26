using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;

namespace QLCV.code.common
{
    public class ProcessFile
    {

       static public void WriteToFile(string strPath,  byte[] Buffer)
        {
            // Create a file
            FileStream newFile = new FileStream(strPath, FileMode.Create);

            // Write data to the file
            newFile.Write(Buffer, 0, Buffer.Length);

            // Close file
            newFile.Close();
        }
      static public byte[] ReadFileToByte(string sFileName, ref string sName)
       {
           byte[] buff = null;
           try
           {
               if (sFileName != "")
               {
                   FileStream fs = new FileStream(sFileName, FileMode.Open, FileAccess.Read);
                   BinaryReader br = new BinaryReader(fs);
                   long numBytes = new FileInfo(sFileName).Length;
                   sName = new FileInfo(sFileName).Name;
                   buff = br.ReadBytes((int)numBytes);
               }
           }
           catch (Exception ex)
           {
           }
           return buff;
       }
    }
}
