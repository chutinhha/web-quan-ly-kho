using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for POP3Auth
/// </summary>
/// 
namespace QLCV.code.common
{
    public class POP3Auth : System.Net.Sockets.TcpClient
    {
        public POP3Auth()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public string CheckAuth(string username, string password)
        {
            try
            {
                string message;
                string response;
                Connect("pop.mail.vtc.vn", 110);
                response = Response();
                if (response.Substring(0, 3) != "+OK")
                {
                    return response;
                }
                message = "USER " + username + "\r\n";
                Write(message);
                response = Response();

                if (response.Substring(0, 3) != "+OK")
                {
                    return response;
                }
                message = "PASS " + password + "\r\n";
                Write(message);
                response = Response();

                if (response.Substring(0, 3) != "+OK")
                {
                    return response;
                }
                message = "QUIT\r\n";
                Write(message);
                return "OK";
            }
            catch
            {
                return "CheckAuth Error";
            }
        }
        private string Response()
        {
            System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            byte[] serverbuff = new Byte[1024];
            NetworkStream stream = GetStream();
            int count = 0;
            while (true)
            {
                byte[] buff = new Byte[2];
                int bytes = stream.Read(buff, 0, 1);
                if (bytes == 1)
                {
                    serverbuff[count] = buff[0];
                    count++;
                    if (buff[0] == '\n')
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            string retval = enc.GetString(serverbuff, 0, count);
            return retval;
        }
        private void Write(string message)
        {
            System.Text.ASCIIEncoding en = new System.Text.ASCIIEncoding();
            byte[] WriteBuffer = new byte[1024];
            WriteBuffer = en.GetBytes(message);
            NetworkStream stream = GetStream();
            stream.Write(WriteBuffer, 0, WriteBuffer.Length);
        }
    }
}
