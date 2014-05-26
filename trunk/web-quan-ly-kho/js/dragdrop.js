// JScript File

if  (document.getElementById){
    (
        function(){
        //Stop Opera selecting anything whilst dragging.
            if (window.opera){
                document.write("<input type='hidden' id='Q' value=' '>");
            }
            var n = 500;
            var dragok = false;
            var y,x,d,dy,dx;

        function move(e){
            if (!e) e = window.event;
            if (dragok){
                 if(dx + e.clientX - x - 20 < 0)
                 {
                    d.style.left = "20px";
                 }
                 else if(dx + e.clientX - x + d.offsetWidth + 30 > screen.width)
                 {
                    d.style.left = screen.width - d.offsetWidth - 30 +"px";
                 }
                 else
                    d.style.left = dx + e.clientX - x + "px";
                 
                 if(dy + e.clientY - y < 0)
                 {
                    d.style.top  = "0px";
                 }
                 else if(dy + e.clientY - y + d.offsetHeight + 180 > screen.height)
                 {
                    d.style.top  = screen.height - d.offsetHeight - 180 + "px";
                 }
                 else
                    d.style.top  = dy + e.clientY - y + "px";
                 return false;
            }
            
        }

        function down(e){
            if (!e) e = window.event;
            var temp = (typeof e.target != "undefined")?e.target:e.srcElement;
            if (temp.tagName != "HTML"|"BODY" && temp.className != "dragclass"){
                temp = (typeof temp.parentNode != "undefined")?temp.parentNode:temp.parentElement;
            }
            if (temp.className == "dragclass"){
                if (window.opera){
                    document.getElementById("Q").focus();
            }
             dragok = true;
             temp.style.zIndex = n++;
             d = temp;
             dx = parseInt(temp.style.left+0);
             dy = parseInt(temp.style.top+0);
             x = e.clientX;
             y = e.clientY;
             
             document.onmousemove = move;
             
             return false;
             }
        }

        function up(){
            dragok = false;
            document.onmousemove = null;
        }

        document.onmousedown = down;
        document.onmouseup = up;

        }
    )
    ();
}
function EnLarge()
{

    var tdLeft = document.getElementById('tdLeft');
    var tdRight = document.getElementById('tdRightContent');
    var nLeft = tdLeft.offsetWidth;
    if(nLeft < eval(screen.width - 500)){
        tdRight.style.width = eval(tdRight.offsetWidth - 50)+"px";
        tdLeft.style.width = eval(nLeft + 50)+"px";
        
    }
}
function Decrease()
{
    var tdLeft = document.getElementById('tdLeft');
    var tdRight = document.getElementById('tdRightContent');  
    nLeft = tdLeft.offsetWidth;
    if(nLeft > 250){    
        tdLeft.style.width = eval(nLeft - 50)+"px";
        tdRight.style.width = eval(tdRight.offsetWidth + 50)+"px";
    }
}
function removeObj(objId){
    if (document.getElementById(objId)) 
        document.body.removeChild(document.getElementById(objId));
}