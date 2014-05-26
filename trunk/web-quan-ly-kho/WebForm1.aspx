<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="QLCV.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link href="../images/css.css" rel="stylesheet" type="text/css" />
    <link href="../images/backend.css" rel="stylesheet" type="text/css" />
    <link href="../images/style_repeater.css" rel="stylesheet" type="text/css" />
    <link href="../images/paper.css" rel="stylesheet" type="text/css" />
    <link href="../images/cms.css" rel="stylesheet" type="text/css"/>
    <link href="../bdc_style.css" rel="stylesheet" type="text/css" />
    <link href="../images/style_grid.css" rel="stylesheet" type="text/css" />
     <link type="text/css" href="../CSS/menu.css" rel="stylesheet" />
 <%--   <link href="../CSS/transmenu.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../CSS/superfish.css" media="screen"/> --%>
 
    <script type="text/javascript" src="<%=UrlJs %>/calendar1.js"></script> 
    <script type="text/javascript" src="<%=UrlJs %>/popcalendar.js"></script>
    <script type="text/javascript" src="<%=UrlJs %>/dragdrop.js"></script>
    <script type="text/javascript" src="<%=UrlJs %>/validate.js"></script>
    
  <%--  <script type="text/javascript" src="<%=UrlJs %>/transmenu.js"></script>
    <script type="text/javascript" src="<%=UrlJs %>/utils.js"></script>
  
		<script type="text/javascript" src="<%=UrlJs %>/jquery-1.2.6.min.js"></script>
		<script type="text/javascript" src="<%=UrlJs %>/hoverIntent.js"></script>
		<script type="text/javascript" src="<%=UrlJs %>/superfish.js"></script>--%>
    <%--<script type="text/javascript" src="<%=UrlJs %>/report.js"></script>--%>
   
    <script type="text/javascript" src="<%=UrlJs %>/jquery.js"></script>
    <script type="text/javascript" src="<%=UrlJs %>/menu.js"></script>
    <script type="text/javascript">

		// initialise plugins
//		jQuery(function(){
//			jQuery('ul.sf-menu').superfish();
//		});

		</script>
    <style type="text/css">
        .style1
        {
            width: 506px;
        }
        .style2
        {
            width: 143px;
        }
        .style3
        {
            width: 317px;
        }
    </style>
</head>
<body>
<style type="text/css">

div#menu {
    top:40px;
    left:40px;
    width:350px;
}
</style>
   <div id="menu"> 
   <ul class="menu">
   <li><a  href='#' class='parent'><span> he thong</span>
</a>
<div>
<ul><li><a  href='#' class='parent'><span> cau hinh</span></a><div><ul><li><a href=''>
<span>bao cao </span></a></li></ul></div> </li>
</ul>
</div> 
</li>
<li><a href='#'class='parent'><span>
 danh muc</span></a><div><ul><li>
 <a href=''><span>tinh thanh </span></a></li></ul> </div> </li>
 </ul></div>

 
    </form>
</body>
</html>
