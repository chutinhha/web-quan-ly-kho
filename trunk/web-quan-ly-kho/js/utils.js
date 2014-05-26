// JScript File
function openPopup(vLink, vHeight, vWidth, vTarget)
{
    winDef = 'status=no,resizable=yes,scrollbars=yes,toolbar=no,location=no,fullscreen=no,titlebar=yes,height='.concat(vHeight).concat(',').concat('width=').concat(vWidth).concat(',');
    winDef = winDef.concat('top=').concat((screen.height - vHeight)/2).concat(',');
    winDef = winDef.concat('left=').concat((screen.width - vWidth)/2);
    window.open(vLink, vTarget, winDef);
}