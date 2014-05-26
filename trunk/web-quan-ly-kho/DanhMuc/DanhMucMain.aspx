<%@ Page Language="C#" MasterPageFile="~/master/default.master" AutoEventWireup="true" CodeBehind="DanhMucMain.aspx.cs" Inherits="QLCV.DanhMuc.DanhMucMain" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="m_contentBody" Runat="Server">
<script src="../js/PageTab.js" type="text/javascript"></script>

   <script src="../../js/HC/p_main.js" type="text/javascript"></script> 
 
<link type="text/css" href="../CSS/HC/p_main.css" rel="stylesheet" />
<link href="../CSS/Stylesheet.css" rel="stylesheet" type="text/css" />
<link href="../CSS/Modules/MeetingRoom/main.css" rel="stylesheet" type="text/css" />
<link href="../CSS/Modules/Nhan_Su/hrm.css" rel="stylesheet" type="text/css" />

<script src="../js/EnterEvent.js" type="text/javascript"></script>

    <table cellSpacing="0" cellPadding="0" width="100%" border="0">          
	<tr vAlign="top">

		<td id="maincontrol_LeftPane" width ="180" vAlign="top" height="100%" class="LeftPanel_BG" style="DISPLAY: block" >
			<Table width="180" cellpadding="0" cellspacing="0">
				<tr class="ModuleHead_Admin">
					<td class="ModuleHead" style=" width:100%"><img src="../images/NhanSu/hrm.gif" align="absMiddle" border="0" hspace="3">QUẢN LÝ DANH MỤC
						</td>

				</tr>
		</Table>
		<Div id="TSK_Left-Menu">
	<%
 
  for (int i = 0; i < _dtFunction.Rows.Count; i++)
  {
      if (_dtFunction.Rows[i]["Parent"].ToString() == "0")
      {
      %>
      <DIV class="TSK_top-level"><IMG hspace="2" src="<%=_dtFunction.Rows[i]["Image_Url"].ToString() %>" align="absMiddle" border="0">
       <%=_dtFunction.Rows[i]["Function_Name"].ToString()%>
							</DIV>
      <%
        }
      else
      {
          %>
           	<div class="TSK_second-level">
            <a style='font-weight: normal;' href='<%=_dtFunction.Rows[i]["Function_Url"].ToString() %>'><img src='<%=_dtFunction.Rows[i]["Image_Url"].ToString() %>' border='0' align='absmiddle'> <%= _dtFunction.Rows[i]["Function_Name"].ToString() %></a>
            </Div>
          <%
        }
  }       

 %>
	</DIV>

		</TD>
		<td id="maincontrol_SeperatePane" class="small-separator-pane" title="Nhấn vào đây để ẩn, hiện thanh công cụ - Ctrl+R" width="6" onclick="toggleLeftPane('maincontrol_LeftPane', 'maincontrol_SeperatePane');"><img id="maincontrol_imgToggle" title="Nhấn vào đây để ẩn, hiện thanh công cụ - Ctrl+R" vspace="250" onmouseclick="toggleLeftPane('maincontrol_LeftPane', 'maincontrol_SeperatePane');" onmouseover="this.src='../images/QLCV/menu_ungrippy.gif';" onmouseout="this.src='../images/QLCV/menu_ungrippy.gif';" src="../images/QLCV/menu_ungrippy.gif" alt="" style="border-width:0px;width:6px;" /></td>

		<td id="SpacePane" width="5"><IMG src="../images/QLCV/1x1.gif" width="5" border="0"></td>
		<td id="maincontrol_RightPane" width="100%"> 
            <div id="hrmcontrol__ctl0_panelGroupInfo" style="height:400px">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                
		        <table class="basic-table" border="0" cellspacing="0" width="100%">
			        <tbody>
			            <tr><td></td></tr>
			            <tr>
				            <td>
                                <asp:Label ID="lblGroupName" runat="server" Text="Danh sách Admin"  class="T1"></asp:Label>
					            <hr class="HR"></td>
				        </tr>
				    </tbody>
				</table>
				<%
                    if (a_action == 1)
                    {
                %>
                <table class="basic-table" width="100%">
				    <tbody>
				        <tr>
				           
				            
				        </tr>
		            </tbody></table>		            	            
                <asp:Literal ID="ltlListAdmin" runat="server"></asp:Literal>
                <%
                    }
                    else
                    {
                        
                %>
                    <table width="700px" cellpadding="4">
                       
                    </table>
                                       
                                
                    <script type="text/javascript">
                    function updateSelectPortals(index, ids, names) {
                        var eName = 'ctl00_m_contentBody_rptData_ctl';
                        if(index.length<2) eName=eName+'0';
                        eName=eName+index+'_txtComName';
                        
                        var txtBox = document.getElementById(eName);
                        
                        if (txtBox.value == '') {
                            txtBox.value = names;
                        } else {
                            txtBox.value += ', ' + names;
                        }   	
                    }	
                    
                    function newWindow(file,window) {
                        msgWindow=open(file,window,'resizable=yes,width=520,height=600,scrollbars=yes,top=80,left=400');
                        if (msgWindow.opener == null) msgWindow.opener = self;
                    }
                    
                    function viewModuleDetails() {
                        var trNV, tdCTV;
                        var chkNV = document.getElementById('ctl00_m_contentBody_chkAdminFull');
                        trNV = document.getElementById('tr_Module_Details');
                        
                        if (chkNV.checked) {
                            trNV.style.display='none';
                        } else {
                            trNV.style.display='';
                        }
                    }

                    </script>  
    <%
                    }	     
                %>
                
				</ContentTemplate>
            </asp:UpdatePanel>
            </div>
        </td>
    </tr>
    
</table>

</asp:Content>