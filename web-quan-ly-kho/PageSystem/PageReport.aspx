<%@ Page Language="C#"  MasterPageFile="~/master/default.master" AutoEventWireup="true" CodeBehind="PageReport.aspx.cs" Inherits="QLCV.PageSystem.PageReport" %>
<%@ Register src="../controls/uscMsgBox.ascx" tagname="uscMsgBox" tagprefix="uc1" %>


<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content  ID="Content1" ContentPlaceHolderID="m_contentBody" Runat="Server" >

    <script type="text/javascript">
// Jefferson Scher -- Oct. 1, 2007 -- Fx2 + IE6 on WinXPSP2
  
    function OnClientBlur(sender,args)
    {
        sender.set_closeDropDownOnBlur(true);
    }
function jfspress(e) { 
 // Check event for Enter key only
 var evt = (e) ? e : window.event;
 var key = (evt.keyCode) ? evt.keyCode : evt.which;
 if (key!=13) return true;

 // Check event for target in desired form/nodeName/type
 var target = (evt.target) ? evt.target : evt.srcElement;
 if (!target.form) return true;
 var nod = "input|select|textarea|";
 
if (nod.indexOf(target.nodeName.toLowerCase())<0) return true;
// var inpTypes = "text|file|checkbox|radio|select-one|select";
// // alert(target.getAttribute("type").toLowerCase());
// if (target.getAttribute("type")) {
//  if (inpTypes.indexOf(target.getAttribute("type").toLowerCase())<0) return true;
// } else { // for textarea controls
//  if (target.className != "enterExits") return true;
// }


 // Find and focus next form control
 //  Find target in the form's elements collection
 var els = target.form.elements;
 for (var i=0; i<els.length; i++){
  if (els[i]==target) {
   if (i==els.length-1) {
    return true; // last element, do default
   } else {
    var eldex = i;
    break;
   }
  }
 }
 //alert(els[eldex].tabIndex);
 //  If control has manually set tabIndex, try to find next higher
 if (target.tabIndex!=0) {
  var minTI = 999;
  var minTIel = -1;
  for (var k=0; k<els.length; k++){
   if (k!=eldex && els[k].tabIndex>=target.tabIndex && els[k].tabIndex<minTI && 
       els[k].disabled==false && els[k].type!="hidden") {
    minTI = els[k].tabIndex;
    minTIel = k;
   }
  }
  
  if (minTIel>-1) { // go to next by numbered tabIndex
   els[minTIel].focus();
   return false;
  }
 }

  
 //  Try natural tab order, forward only
 for (var j=eldex+1; j<els.length; j++){
  if (els[j].tabIndex==0 && els[j].disabled==false && els[j].type!="hidden") { // go to next in natural order
   els[j].focus();
   return false;   
  }
 }

 // TEMPORARY CATCH ALL FOR TESTING ONLY
 //alert("Not handled; eldex="+eldex);
 return false;
} // end function jfspress()

function setup() {
 // add onkeydown event handlers to <form>s
 var forms = document.getElementsByTagName("FORM");
 for (var i=0; i<forms.length; i++) {
  forms[i].onkeypress=jfspress;
 }
} // end function setup()

window.onload = setup; // add onkeypress to each form
</script>
   
         <asp:UpdatePanel ID="UpdatePanel1" runat="server"  UpdateMode ="Conditional"    >
   <ContentTemplate>
           <table style="width: 100%">
               <tr>
                   <td style="width: 142px">
                       &nbsp;</td>
                   <td style="width: 143px">
                       &nbsp;</td>
                   <td style="width: 95px">
                       <b>Loại báo cáo:</b></td>
                   <td style="width: 537px">
                       <telerik:RadComboBox ID="cboReport" Runat="server" Skin="Vista" Width="100%" 
                           Height = "100px" AutoPostBack="True" 
                           onselectedindexchanged="cboReport_SelectedIndexChanged">
                           <CollapseAnimation Duration="200" Type="OutQuint" />
                       </telerik:RadComboBox>
                   </td>
                   <td style="width: 109px">
                       &nbsp;</td>
                   <td>
                       &nbsp;</td>
               </tr>
               <tr>
                   <td style="width: 142px">
                       &nbsp;</td>
                   <td style="width: 143px">
                       &nbsp;</td>
                   <td style="width: 95px">
                       &nbsp;</td>
                   <td style="width: 537px">
                       &nbsp;</td>
                   <td style="width: 109px">
                       &nbsp;</td>
                   <td>
                       &nbsp;</td>
               </tr>
               <tr>
                   <td class="style1" style="height: 325px; width: 142px;">
                       &nbsp;</td>
                   <td class="style1" colspan="4" style="height: 325px">
                       <asp:Panel ID="Panel" runat="server" Height="325px" Width="894px" 
                           style="margin-left: 1px">
                           </asp:Panel>
                   </td>
                   <td class="style1" style="height: 325px">
                   </td>
               </tr>
               <tr>
                   <td style="width: 142px">
                       &nbsp;</td>
                   <td style="width: 143px">
                       &nbsp;</td>
                   <td style="width: 95px">
                       &nbsp;</td>
                   <td style="width: 537px">
                       &nbsp;</td>
                   <td style="width: 109px">
                       &nbsp;</td>
                   <td>
                       &nbsp;</td>
               </tr>
               <tr>
                   <td style="width: 142px">
                       &nbsp;</td>
                   <td style="width: 143px">
                       &nbsp;</td>
                   <td>
                       &nbsp;</td>
                   <td>
                       <table style="width: 100%">
                           <tr>
                               <td style="width: 216px">
                                   &nbsp;</td>
                               <td style="width: 141px">
                                   <asp:Button ID="btnLapBaoCao" runat="server" Text="Lập báo cáo" Width="150px" 
                                       onclick="btnLapBaoCao_Click" />
                               </td>
                               <td>
                                   &nbsp;</td>
                           </tr>
                       </table>
                   </td>
                   <td style="width: 109px">
                       &nbsp;</td>
                   <td>
                       &nbsp;</td>
               </tr>
           </table>
            <telerik:RadWindowManager
            id="Singleton"                 
            Behaviors="close, minimize, maximize, Move" 
            runat="server" Height="41px">
                <windows>
                    <telerik:RadWindow ID="RadWindow1" 
                     Height="550px"
                     Width="900px"
                     Runat="server"
                     Skin="Vista"
                    >
                    </telerik:RadWindow>
                </windows>
            </telerik:RadWindowManager>
           </ContentTemplate>
    </asp:UpdatePanel>
   <uc1:uscMsgBox ID="uscMsgBox" runat="server"  />
</asp:Content>
