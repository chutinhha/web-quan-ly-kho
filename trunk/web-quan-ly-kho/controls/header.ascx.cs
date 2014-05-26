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
//using Zooz.Libs;

using QLCV.code.common;
public partial class controls_header : System.Web.UI.UserControl
{
    public string UrlImages = Constant.IMG_PATH;
    private const int _itemW = 100;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {

            //string FileName = Server.MapPath(DBCommon.UrlRoot+"xmls/vn.xml");
            //Languages lang = new Languages(FileName);
            //lang.SetPage("HEADER");
            //ltlHeader.Text = lang.GetText("HEADER_TITLE");


            //DataTable dt = lang.GetPage("MENU_ADMIN_HEADER", "priority ASC");

            //if (dt.Rows.Count == 0)
            //    return;

            //string sCurr = Request.Url.AbsoluteUri;
            //string sHtml = string.Empty;
            //int i = 0;
            //foreach (DataRow dr in dt.Rows)
            //{
            //    string caption = dr["caption"].ToString();
            //    string link = DBCommon.UrlRoot + dr["url"].ToString();
              
               

              
            //    if (sCurr.IndexOf(dr["key"].ToString())>0)
            //    {
            //        sHtml += "<td style=\"width: 4px; height: 20px\" class=\"spacerTab\" nowrap>&nbsp;</td>";
            //        sHtml += "<td id='left" + i + "' style=\"width: 4px; height: 20px\" nowrap valign=\"top\" class=\"selTabLeft\">";
            //        sHtml += "<img style=\"border:0px\" src=\"" + UrlImages + "selectedTab_leftCorner.gif\" width=\"4\" height=\"3\" alt=\"\"></td>";
            //        sHtml += "<td style=\"width:" + _itemW + "px; height: 20px;\" align=\"center\" nowrap valign=\"middle\" class=\"selTabCenter\" >" + caption + "</td>";
            //        sHtml += "<td id='right" + i + "' style=\"width: 4px; height: 20px\" nowrap align=\"right\" valign=\"top\" nowrap class=\"selTabRight\">";
            //        sHtml += "<img src=\"" + UrlImages + "selectedTab_rightCorner.gif\" width=\"4\" height=\"3\" alt=\"\" style=\"border:0px\"></td>";
                    
            //    }
            //    else
            //    {
            //        sHtml += "<td style=\"width: 4px; height: 20px\" class=\"spacerTab\" nowrap>&nbsp;</td>";
            //        sHtml += "<td id='left" + i + "' style=\"width: 4px; height: 20px\" nowrap valign=\"top\" class=\"deSTabLeft\">";
            //        sHtml += "<img src=\"" + UrlImages + "unSelectedTab_leftCorner.gif\" width=\"4\" height=\"3\" alt=\"\" style=\"border:0px\" /></td>";
            //        sHtml += "<td style=\"width:" + _itemW + "px; height: 20px;cursor:hand;cursor:pointer;\" align=\"center\" nowrap valign=\"middle\" class=\"deSTabCenter\" onclick='window.location = \"" + link + "\"' onmouseover=\"this.className='hoverTabCenter';document.getElementById('left" + i + "').className='hoverTabLeft';document.getElementById('right" + i + "').className='hoverTabRight';\" onmouseout=\"this.className='deSTabCenter';document.getElementById('left" + i + "').className='deSTabLeft';document.getElementById('right" + i + "').className='deSTabRight';\" onkeypress=\"__keyPress(event, '" + link + "');\">" + caption + "</td>";
            //        sHtml += "<td id='right" + i + "' style=\"width: 4px; height: 20px\" nowrap align=\"right\" valign=\"top\" class=\"deSTabRight\">";
            //        sHtml += "<img src=\"" + UrlImages + "unSelectedTab_rightCorner.gif\" style=\"width: 4px; height: 3px; border: 0px\" alt=\"\" /></td>";
            //    }

            //    i++;
            //}
            //ltlMenu.Text = sHtml;

            //ltlHeader.Text = "Title Form";

        }
    }
}
