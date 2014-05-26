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
  //alert("snsfs");
 // alert( els[minTIel]);
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