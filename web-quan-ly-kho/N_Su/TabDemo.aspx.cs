using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace QLCV.N_Su
{
    public partial class TabDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TabButton_Click(object sender, EventArgs e)
        {
            string containerId = tc1.ActiveTabIndex.ToString();
            Panel panel = (Panel) tc1.ActiveTab.FindControl(containerId);

            if (panel != null)
            {
                panel.Visible = true;
            }
        }

        protected void tc1_ActiveTabChanged(object sender, EventArgs e)
        {

        }
    }
}
