using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MissteenLib;
public partial class Controls_Candidate : System.Web.UI.UserControl
{
    private int numMissPerPage = 35;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Missteen_Profile miss = new Missteen_Profile();
            int rec_count = miss.GetCountMiss();
            int numpage = rec_count / numMissPerPage;
            if (numpage == 0) numpage = 1;
            DateTime time = new DateTime();
            time = DateTime.Now;
            Random x = new Random(time.Millisecond);
            int iGT = x.Next(1, numpage);
            int status = 1;
            DataTable dt = new Missteen_Profile().getListMiss(iGT, numMissPerPage, status, "ORDER BY newid()");
            if (dt.Rows.Count > 0)
            {
                listMiss.DataSource = dt;
                listMiss.DataBind();
            }
        }
       
    }
    
   
}

