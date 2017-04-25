var curCountDown = 0;
var ckiName = null;
var countDownTimer = null;

function CountDownRefresh_Tick() {
    document.getElementById('CountDownRefresh').innerHTML = curCountDown;
    curCountDown--;
    if (countDownTimer != null) clearTimeout(countDownTimer);
    countDownTimer=setTimeout(CountDownRefresh_Tick, 1000);

    if (curCountDown < -1) {
        eval(countDownRefreshFunc);
    }
}

function CountDownRefresh() {
    if (countDownTimer != null) clearTimeout(countDownTimer);
    document.getElementById('CountDownRefresh').innerHTML = loadingTxt;

    ajax.Request(window.location.href, {asynchronous: true, parameters: 'full_mode=0'
                            , onComplete: function(data) {
                                $('sContent').innerHTML = data.responseText;
                                RefreshCountDown_Init();
                            }
    });
    
    return true;
}


function CountDownRefresh_SaveOption(idNewOption) {
    $('CountDownMenu').style.display = 'none';
    var txtNewOption = $(idNewOption).innerHTML;
    initCountDown = parseInt(txtNewOption);
    CountDownRefresh_DoCheckMenu();
    setCookie(ckiName, initCountDown, 35);
    CountDownRefresh_SetCountDown(initCountDown); //reset
}


function CountDownRefresh_DoCheckMenu() {
    var menus = $('CountDownMenu').getElementsByTagName("div");
    for (i = 0; i < menus.length; i++) {
        if (parseInt(menus[i].innerHTML) == initCountDown) {
            AddClassName(menus[i].id, "menuitemcheck");
        }
        else
            RemoveClassName(menus[i].id, "menuitemcheck");
    }
}

function CountDownRefresh_Refresh() {
    var pnlDown = $('CountDownMenu');
    if (pnlDown.style.display == '')
        pnlDown.style.display = 'none';
    eval(countDownRefreshFunc);
}

function CountDownRefresh_DoOpenOption(btnDown) {
    var pnlDown = $('CountDownMenu');
    if (pnlDown.style.display == 'none') {
        pnlDown.style.display = '';
    }
    else pnlDown.style.display = 'none';
}

function CountDownRefresh_SetCountDown(newCountDown) {
    initCountDown = newCountDown;
    curCountDown = initCountDown;
}

function FindPosition(oElement) {
    if (typeof (oElement.offsetParent) != 'undefined') {
        for (var posX = 0, posY = 0; oElement; oElement = oElement.offsetParent) {
            posX += oElement.offsetLeft;
            posY += oElement.offsetTop;
        }
        return [posX, posY];
    } else {
        return [oElement.x, oElement.y];
    }
}


// Start up
function RefreshCountDown_Init() {

    curCountDown = initCountDown;
    ckiName = "ibcCountDown" + refreshCountDownId;
    
    //read countdown from cookie
    var countDownFromCookie = getCookie(ckiName);
    if (countDownFromCookie && countDownFromCookie != 0) {
        CountDownRefresh_SetCountDown(countDownFromCookie);
    }
    CountDownRefresh_DoCheckMenu();
    //do count down
    CountDownRefresh_Tick();
}

// utility functions
function RefreshCountDown_AddEvent(el, evname, func) {
    if (el.attachEvent) { // IE
        el.attachEvent("on" + evname, func);
    } else if (el.addEventListener) { // Gecko / W3C
        el.addEventListener(evname, func, true);
    } else { // Opera (or old browsers)
        el["on" + evname] = func;
    }
}

RefreshCountDown_AddEvent(window, "load", RefreshCountDown_Init);