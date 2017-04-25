function onclickUser(username) {
    if ($('txtUserName').value == username) {
        $('txtUserName').className = 'text_normal';
        $('txtUserName').value = '';
    }
}
function onblurUser(username) {
    if ($('txtUserName').value == '') {
        $('txtUserName').className = 'text_italic';
        $('txtUserName').value = username;
    }
}

function onKeyPressUser(submitID,event) {
    if (!event) event = window.event;
    var key = (event.keyCode) ? event.keyCode : event.which;
    if (key == 13) {
        $(submitID).click();
        return false;
    }
}

function InitTagSuggestion() {
    initAutoComplete('txtUserName', '../../../_GlobalResources/Handlers/QueryUserName.ashx?custid=' + _page.custid + '&isdir=1');
}