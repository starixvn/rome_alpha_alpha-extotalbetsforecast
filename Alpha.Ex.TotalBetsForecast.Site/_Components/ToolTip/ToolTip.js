function ShowBalloonMsg(id, strMsg) {
    var _e1 = $(id);
    _e1.style.postion = "relative";

    var pathTooltip = $('pathTooltip') ? $('pathTooltip').value : '';

    var x = 0;
    var y = 0;
    var obj = _e1;

    if (obj.offsetParent) {
        do {
            x += obj.offsetLeft;
            y += obj.offsetTop;
        }
        while (obj = obj.offsetParent);
    }

    x += _e1.offsetWidth - 155;
    y -= _e1.offsetHeight + 74;
    _e1.ontip = function() {
    Tip('<div class="msgerr">' + strMsg + '</div>', BALLOON, true, ABOVE, true, BALLOONIMGPATH, pathTooltip + "../../../_Components/ToolTip/tip_balloon/", WIDTH, -350, FIX, [x, y]);
    }
    _e1.ontip();
    _e1.focus();
    window.scrollBy(0, -60);
    _e1.onblur = UnTip;
}

function UnShowBalloonMsg() {
    UnTip();
}
