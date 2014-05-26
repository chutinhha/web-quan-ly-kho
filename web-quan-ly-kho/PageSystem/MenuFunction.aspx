<%@ Page Language="C#"  MasterPageFile="~/master/default.master"AutoEventWireup="true" CodeBehind="MenuFunction.aspx.cs" Inherits="QLCV.PageSystem.MenuFunction" %>


<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>


<%@ Register src="../controls/uscMsgBox.ascx" tagname="uscMsgBox" tagprefix="uc1" %>


<asp:Content  ID="Content1" ContentPlaceHolderID="m_contentBody" Runat="Server"   >

    <script type="text/javascript">
// Jefferson Scher -- Oct. 1, 2007 -- Fx2 + IE6 on WinXPSP2

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
   
  
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate>
    <table style=" width: 100%; height: 500px; vertical-align: top; font-style: inherit; font-weight: bold;" 
        align="left">
        <tr>
            <td align="left" style="width: 1%;">
            </td> 
            <td align="left" style="width: 8%;  height : 100%;">
            <asp:Panel ID="Panel1" runat="server" Height="500px"
                 Width="400px" ScrollBars="Both" BorderColor="Blue" 
        BorderStyle="Double">
                <asp:TreeView ID="trMenu" runat="server" 
                    onselectednodechanged="trMenu_SelectedNodeChanged" Width="100%" 
                    Font-Bold="True" Font-Overline="False">
                    <HoverNodeStyle BackColor="#666699" />
                </asp:TreeView>
    </asp:Panel>
  
    
            </td>
         
          <td style="width: 30%; height: 100%;"  >
          
              <table style="width: 100%; height: 492px; font-weight: bold;">
                  <tr>
                      <td style="width: 218px">
                          &nbsp;</td>
                      <td style="width: 752px">
                          &nbsp;</td>
                  </tr>
                  <tr>
                      <td style="height: 34px; width: 218px">
                          Tên chức năng: </td>
                      <td class="style3" style="width: 752px">
                          <asp:TextBox ID="txtFunctionName" runat="server" Width="442px" 
                              BackColor="White"></asp:TextBox>
                      </td>
                  </tr>
                  <tr>
                      <td style="height: 29px; width: 218px">
                          Địa chỉ trang:</td>
                      <td class="style4" style="height: 29px; width: 752px;">
                          <asp:TextBox ID="txtUrl" runat="server" Width="442px" BackColor="White"></asp:TextBox>
                      </td>
                  </tr>
                  <tr>
                      <td style="height: 29px; width: 218px">
                          Thứ tự:</td>
                      <td class="style4" style="height: 29px; width: 752px;">
                          <asp:TextBox ID="txtOrder" runat="server" Width="442px" BackColor="White"></asp:TextBox>
                      </td>
                  </tr>
                  <tr>
                      <td style="height: 29px; width: 218px">
                          Chức năng cha:</td>
                      <td class="style4" style="height: 29px; width: 752px;">
                          <asp:DropDownList ID="cboParent" runat="server" Height="21px" Width="442px">
                          </asp:DropDownList>
                      </td>
                  </tr>
                  <tr>
                      <td style="height: 29px; width: 218px">
                          &nbsp;</td>
                      <td class="style4" style="height: 29px; width: 752px;">
                          &nbsp;</td>
                  </tr>
                  <tr>
                      <td style="height: 29px; width: 218px">
                          &nbsp;</td>
                      <td class="style4" style="height: 29px; width: 752px;">
                          <table style="width: 100%">
                              <tr>
                                  <td style="width: 109px">
                                      <asp:Button ID="btnAdd" runat="server" Height="31px" Text="Thêm" 
                                          Width="103px" onclick="btnAdd_Click" TabIndex="1"/>
                                  </td>
                                  <td style="width: 119px">
                                      <asp:Button ID="btnEdit" runat="server" Height="31px" Text="Sửa" 
                                          Width="111px" onclick="btnEdit_Click" />
                                  </td>
                                  <td>
                                      <asp:Button ID="btnDelete" runat="server" Height="31px" Text="Xóa" 
                                          Width="111px" onclick="btnDelete_Click" />
                                  </td>
                              </tr>
                          </table>
                      </td>
                  </tr>
                  <tr>
                      <td style="height: 250px; width: 218px">
                         
                      </td>
                      <td style="height: 250px; width: 752px;">
                         
                       
                         
                      </td>
                  </tr>
              </table>
          
          </td>
        </tr>  
        </table>
        </ContentTemplate>
 </asp:UpdatePanel>
  <uc1:uscMsgBox ID="uscMsgBox" runat="server" />
  <uc1:uscMsgBox ID="uscMsgBoxConfirm" runat="server" />
</asp:Content>
