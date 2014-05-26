<%@ Control Language="C#" AutoEventWireup="true" Inherits="menu" Codebehind="menu.ascx.cs" %>

<%=QLCV.code.common.clsMenu.genMenu(Int32.Parse(Session["UserId"].ToString()))%>

