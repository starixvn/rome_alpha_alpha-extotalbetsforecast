/*
* Created 20111010@Andy - Context Menu for show/hide columns
* Revision:
* ?@? - ...
*
*/

var timerId = null;

function onChangeColumn(colName, chkId, colId, colspan, showLcsFunc) {
    var table = document.getElementById(_page.tableId);
    var currentClass = table.className;
    var display = document.getElementById(chkId).checked;

    table.className = display ? currentClass + ' ' + 'show' + colName : currentClass.replace('show' + colName, '');

    //BEGIN -- Used for Commission By Bet Type report page
    if (colId && colId != '') {
        if (display) {
            document.getElementById(colId).setAttribute('colSpan', colspan);
        }
        else {
            document.getElementById(colId).setAttribute('colSpan', '1');
        }
    }
    //END -- Used for Commission By Bet Type report page

    //BEGIN -- Used for credit/balance page when show LC Win % column
    if (showLcsFunc && $(chkId).checked && chkId == 'chk_LCS') {
        GetWinLimit();
    }

    if (_page.sessionId && _page.sessionId != getCookie('sessionId')) {
        setCookie('sessionId', _page.sessionId, 1);
    }
    //END -- Used for credit/balance page when show LC Win % column

    var cookieValue = readCookie().split(',');
    var cookieValues = '';
    var cols = document.getElementById('Popup').getAttribute('cols').split(',');

    for (var k = 0; k < cols.length; k++) {
        if (cols[k] == colName)
            cookieValues += display ? '1,' : '0,';
        else
            cookieValues += cookieValue[k] + ',';
    }

    cookieValues = cookieValues.substring(0, cookieValues.length - 1);
    writeCookie(cookieValues);
}

function showContextMenu(ev, left, top) {
    document.getElementById('menuPopupAd').style.display = 'none';

    var menu = document.getElementById(_page.parentId);
    menu.style.display = 'block';
    menu.style.position = 'absolute';

    var menu2 = document.getElementById('PopupCtx');
    menu2.style.display = 'block';
    menu2.style.position = 'absolute';

    if (left != null) menu2.style.left = (ev.clientX || ev.x) - left + (window.pageXOffset || document.documentElement.scrollLeft) + 'px';
    else menu2.style.left = (ev.clientX || ev.x) - 3 + (window.pageXOffset || document.documentElement.scrollLeft) + 'px';

    if (top != null) menu2.style.top = (ev.clientY || ev.y) - top + 'px';
    else menu2.style.top = (ev.clientY || ev.y) - 78 + 'px';
}

function readCookie() {
    var cookieValue = getCookie(_page.cookiePrefix);
    var cookieValues = '';

    if (cookieValue) {
        cookieValues = cookieValue;
    }
    else {
        var cols = document.getElementById('Popup').getAttribute('cols').split(',');

        for (var i = 0; i < cols.length; i++) {
            if (_page.noHideTotal && (i != 3)) {
                cookieValues += '0,';
            }
            else {
                cookieValues += '1,';
            }
        }

        cookieValues = cookieValues.substring(0, cookieValues.length - 1);
    }

    return cookieValues;
}

function writeCookie(cookieValues) {
    setCookie(_page.cookiePrefix, cookieValues, 30);
}

function initContextMenu(left, top) {
    if (!$('Popup')) {
        return
    }

    var cols = document.getElementById('Popup').getAttribute('cols');
    var colArray = cols.split(',');
    if (colArray.length == 1) return false;

    initPopupAd();

    var cookieValues = readCookie().split(',');
    var colSpans = _page.colSpans;

    var sessionId = null;

    if (_page.sessionId) {
        sessionId = getCookie('sessionId');
    }

    for (var i = 0; i < colArray.length; i++) {
        var chk = document.getElementById('chk_' + colArray[i]);

        if (chk)
            chk.checked = cookieValues[i] == '1' || i >= cookieValues.length ? 'checked' : '';

        if (chk.id == 'chk_LCS' && sessionId != _page.sessionId) {
            chk.checked = '';
        }

        if (_page.noHideTotal) {
            if ((i == 0 || i == 1 || i == 4) && cookieValues[i] == '0') {
                chk.checked = '';
                document.getElementById(colArray[i]).setAttribute('colSpan', '1');
            }
            else {
                document.getElementById(colArray[i]).setAttribute('colSpan', cookieValues[i] == '0' ? '1' : colSpans[i]);
            }
        }
    }

    document.getElementById(_page.parentId).onmouseover = function () {
        overDiv();
    }

    document.getElementById(_page.parentId).onmouseout = function () {
        outDiv();
    }

    var containers = _page.containerId;

    for (var x = 0; x < containers.length; x++) {
        if (left != null && top != null) {
            if (!isIE())
                document.getElementById(containers[x]).oncontextmenu = function (event) {
                    showContextMenu(event, left, top); return false;
                }
            else
                document.getElementById(containers[x]).oncontextmenu = function () {
                    showContextMenu(event, left, top); return false;
                }
        }
        else {
            if (!isIE())
                document.getElementById(containers[x]).oncontextmenu = function (event) {
                    showContextMenu(event); return false;
                }
            else
                document.getElementById(containers[x]).oncontextmenu = function () {
                    showContextMenu(event); return false;
                }
        }
    }
}

function hideDiv() {
    document.getElementById(/*_page.parentId*/'PopupCtx').style.display = 'none';
}

function outDiv() {
    timerId = setTimeout('hideDiv()', 100);
}

function overDiv() {
    clearTimeout(timerId);
    timerId = null;
}

function getElementsByName(name, tag) {
    //Not IE
    if (!isIE()) return document.getElementsByName(name);

    //IE
    var elem = document.getElementsByTagName('td');
    if (tag) elem = document.getElementsByTagName(tag);
    var arr = new Array();

    for (var i = 0; i < elem.length; i++) {
        var att = elem[i].getAttribute("name");
        if (att == name) {
            arr.push(elem[i]);
        }
    }

    return arr;
}

function initCSS() {
    var cols = document.getElementById('Popup').getAttribute('cols').split(',');
    var css = '';

    for (var i = 0; i < cols.length; i++) {
        css += '.' + cols[i] + ' { display: none; } ';
        css += 'table.show' + cols[i] + ' .' + cols[i] + ' { display: table-cell; } ';
    }

    var body = document.getElementsByTagName('body')[0];
    var style = document.createElement('style');

    style.innerHTML = css;
    body.appendChild(style);
}

function initPopupAd(left, top) {
    var value = getCookie(_page.cookiePrefix + 'X');
    var div = document.getElementById('menuPopupAd');
    if (value) {
        if (value < 5) {
            div.style.display = 'block';
            div.style.left = left ? left + 'px' : '200px';
            div.style.top = top ? top + 'px' : document.getElementById(_page.tableId).style.top;
            timerId = setTimeout('autoHidePopupAd()', 10000);
            setCookie(_page.cookiePrefix + 'X', parseInt(value) + 1, 30);
        }
        else {
            div.style.display = 'none';
        }
    }
    else {
        div.style.display = 'block';
        div.style.left = left ? left + 'px' : '200px';
        div.style.top = top ? top + 'px' : document.getElementById(_page.tableId).style.top;
        timerId = setTimeout('autoHidePopupAd()', 10000);
        setCookie(_page.cookiePrefix + 'X', 1, 30);
    }
}

function hidePopupAd() {
    setCookie(_page.cookiePrefix + 'X', 5, 30);
    document.getElementById('menuPopupAd').style.display = 'none';
}

function autoHidePopupAd() {
    clearTimeout(timerId);
    timerId = null;
    document.getElementById('menuPopupAd').style.display = 'none';
}

function GetWinLimit() {
    var url = '../../_MemberInfo/LiveCasino/Handlers/Agent/MemberWinLimit.ashx';

    url = SetParameterValue('custid', _page.CustId, url);
    url = SetParameterValue('username', $('txtUserName').value, url);
    url = SetParameterValue('pagesize', _page.PageSize, url);
    url = SetParameterValue('status', $('statusFilter').value, url);

    jx.request(
    url,
    function (res) {
        try {
            var ret = eval('(' + res.responseText + ')');
            var users = eval(ret.errMsg);

            var table = $('tblMain');

            for (var i = 1; i < table.rows.length; i++) {
                table.rows[i].cells[9].innerHTML = users[i - 1].WinLimit;
            }
        } catch (ex) { }
    },
    'post',
    '',
    true
    );
}