var isIE = false;

function calculateMainContentHeightForIE() {

    var ua = window.navigator.userAgent;
    var msie = ua.indexOf("MSIE ");
    isIE = false;

    if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer, return version number
    {
        isIE = true;
    }

    if (isIE) {
        var body = document.body,
        html = document.documentElement;

        var height = Math.max(body.offsetHeight, html.clientHeight, html.offsetHeight);

        $('menu-panel').style.height = (height - 112) + 'px';
        $('main-panel').style.height = (height - 112) + 'px';
    }
}

calculateMainContentHeightForIE();

if (isIE) {
    window.onresize = function (event) {
        calculateMainContentHeightForIE();
    };
}

function reloadPopUpSetting(timeOut) {
    var iframeSrc = window.top.$('AGEWndIframe') && window.top.$('AGEWndIframe').attributes['src'].value;
    if (timeOut) {
        setTimeout(function () {
            doReloadMainFrame();
        }, timeOut);
    } else {
        doReloadMainFrame();
    }

    function doReloadPopup() {
        if (!hasPopupOpen()) {
            return;
        }

        if (iframeSrc && iframeSrc != '') {
            window.top.frames['AGEWndIframe'].location.reload(true);
        } else {
            top.main.$('target').remove();
            top.main.document.body.appendChild(top.$('target'));
            top.main.$('target').submit();
        }
    }

    function doReloadMainFrame() {
        if (!iframeSrc && top.main.$('target')) {
            if (top.$('target')) {
                top.$('target').remove();
            }

            top.document.body.appendChild(top.main.$('target'));
        }

        var isCustomerList = top.main.$("arrayCustID") && top.main.$("arrayCustID").value.length &&
            top.main.$("arrayUserName") && top.main.$("arrayUserName").value.length;
        if (isCustomerList) {
            var arrayCustId = top.main.$("arrayCustID").value;
            var arrayUserName = top.main.$("arrayUserName").value;
            window.top.main.location.reload(true);
            window.top.$('main').onload = function () {
                top.main.$("arrayCustID").value = arrayCustId;
                top.main.$("arrayUserName").value = arrayUserName;
                doReloadPopup();
                window.top.$('main').onload = null;
            };
        } else {
            window.top.main.location.reload(true);
            window.top.$('main').onload = function () {
                doReloadPopup();
                window.top.$('main').onload = null;
            };
        }

        window.top.clearPopupSubtitle(true);
    }
}

function hasPopupOpen() {
    var popUp = parent.document.getElementsByClassName('AGEWnd');
    var hasPopUp = popUp && popUp.length;

    return hasPopUp;
}

function clearPopupSubtitle(hasSubTitle) {
    var popUps = parent.document.getElementsByClassName('AGEWnd');
    var hasPopUp = popUps && popUps.length;

    if (!hasPopUp) {
        return;
    }

    var iframe = top.$('AGEWndIframe');
    if (!iframe) {
        return;
    }

    var timer = setInterval(function () {
        var iframeDoc = iframe.contentDocument || (iframe.contentWindow ? iframe.contentWindow.document : null);
        if (!iframeDoc) {
            return;
        }

        var isReady = iframeDoc.readyState == 'complete' ||
            iframeDoc.readyState == 'interactive';
        if (!isReady) {
            return;
        }

        clearSubTitleAfterInframeLoad(iframe);
        clearInterval(timer);
    }, 200);

    var onloadFunction = iframe.onload;
    iframe.onload = function () {
        if (onloadFunction && typeof (onloadFunction) == 'function') {
            onloadFunction();
        }

        clearSubTitleAfterInframeLoad(iframe);
        clearInterval(timer);
    };

    function clearSubTitleAfterInframeLoad(iframe) {
        var titles = iframe.contentWindow.document.getElementsByClassName('setting-header-agency');
        var header_main = titles && titles.length ? titles[0] : null;

        header_main = header_main || iframe.contentWindow.document.getElementById('header_main');
        header_main = header_main || iframe.contentWindow.document.getElementById('race-book-positiontaking-header');
        header_main = header_main || iframe.contentWindow.document.getElementById('title_header');

        if (header_main) {
            header_main.style.display = 'none';
        }

        if (hasSubTitle) {
            var subTitles = iframe.contentWindow.document.getElementsByClassName('subTitle');
            if (subTitles && subTitles.length) {
                subTitles[0].style.display = 'none';
            }
        }
    }
}

var popupManager = {
    positionPadding: 5,
    leftPosition: function () {
        // top-left main frame
        var accountInfoWidth = top.isNewSite ? top.$('.sidebar').width() : top.menu.innerWidth;

        return accountInfoWidth + top.popupManager.positionPadding;
    },

    topPosition: function () {
        // top-left main frame
        return 90 + top.popupManager.positionPadding;
    },

    // Choose most suitable screen resolution 1920 X 1080
    defaultHeight: 1000,

    defaultWidth: 480,
    openByRelativeUrl: function (url, title, width, height, windowCss, titleCss, closeBtnCss, left, top, iframeAttributes) {
        window.top.popupManager.open(age.GetBaseUrl() + url, title, width, height, windowCss, titleCss, closeBtnCss, left, top, iframeAttributes);
    },
    openMultipleByRelativeUrl: function (url, title, width, height, windowCss, titleCss, closeBtnCss, left, top, iframeAttributes) {
        window.top.popupManager.openMultiple(age.GetBaseUrl() + url, title, width, height, windowCss, titleCss, closeBtnCss, left, top, iframeAttributes);
    },
    open: function (url, title, width, height, windowCss, titleCss, closeBtnCss, left, top, iframeAttributes) {
        var handler = window.top.ageWnd;
        window.top.popupManager.openWithHanlder(handler, url, title, width, height, windowCss, titleCss, closeBtnCss, left, top, iframeAttributes);
    },
    openMultiple: function (url, title, width, height, windowCss, titleCss, closeBtnCss, left, top, iframeAttributes) {
        var handler = window.top.ageWnd.createInstance();
        window.top.popupManager.openWithHanlder(handler, url, title, width, height, windowCss, titleCss, closeBtnCss, left, top, iframeAttributes);
    },
    openWithHanlder: function (handler, url, title, width, height, windowCss, titleCss, closeBtnCss, left, top, iframeAttributes) {
        left = left || window.top.popupManager.leftPosition();
        top = top || window.top.popupManager.topPosition();

        width = width || window.top.popupManager.defaultWidth;
        height = height || window.top.popupManager.defaultHeight;
        title = title || '';

        handler.open(url, title, left, top, width, height, windowCss, titleCss, closeBtnCss, iframeAttributes);
    },
    close: function () {
        top.ageWnd.close();
    }
};

















