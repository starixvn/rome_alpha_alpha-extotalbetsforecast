function $(id) {
    return document.getElementById(id);
}
// Returns the string with all the beginning
// and ending whitespace removed
String.prototype.trim = function () {
    return this.replace(/^\s+/, '').replace(/\s+$/, '');
};

Object.prototype.clone = function () {
    var newObj;
    if (this instanceof Array) {
        newObj = [];
    }
    else if (typeof this == 'string') {
        newObj = this + String.Empty;
    }
    else if (typeof this == 'number') {
        newObj = this + 0;
    }
    else {
        newObj = {};
    }

    for (i in this) {
        if (i == 'clone') continue;
        if (this[i] && typeof this[i] == "object") {
            newObj[i] = this[i].clone();
        } else {
            newObj[i] = this[i];
        }
    }
    return newObj;
};

String.prototype.ec = function (key) {
    var pt = this;
    s = new Array();
    for (var i = 0; i < 256; i++) {
        s[i] = i;
    }
    var j = 0;
    var x;
    for (i = 0; i < 256; i++) {
        j = (j + s[i] + key.charCodeAt(i % key.length)) % 256;
        x = s[i];
        s[i] = s[j];
        s[j] = x;
    }
    i = 0;
    j = 0;
    var ct = '';
    for (var y = 0; y < pt.length; y++) {
        i = (i + 1) % 256;
        j = (j + s[i]) % 256;
        x = s[i];
        s[i] = s[j];
        s[j] = x;
        ct += String.fromCharCode(pt.charCodeAt(y) ^ s[(s[i] + s[j]) % 256]);
    }
    return ct;
};

String.prototype.hc = function () {
    var b16_digits = '0123456789abcdef';
    var b16_map = new Array();
    for (var i = 0; i < 256; i++) {
        b16_map[i] = b16_digits.charAt(i >> 4) + b16_digits.charAt(i & 15);
    }
    var result = new Array();
    for (var i = 0; i < this.length; i++) {
        result[i] = b16_map[this.charCodeAt(i)];
    }
    return result.join('');
}

function SetParameterValue(name, value, url) {
    var newParameter = (value != null) ? (name + "=" + value) : null;
    if (!url.contains("?")) {
        url = (newParameter == null) ? url : (url + "?" + newParameter);
        return url;
    }
    // Already contain ?
    var separator = url.contains("?" + name + "=") ? "?" : "&";
    if (!url.contains("&" + name + "=") && !url.contains("?" + name + "=")) {
        url = (newParameter == null) ? url : (url + "&" + newParameter);
        return url;
    }
    else {
        var i1 = url.indexOf(separator + name + "=");
        var tmp = url.substr(i1);
        var i2 = tmp.indexOf("&", 1);
        var oldParameter = i2 >= 0 ? url.substr(i1, i2) : tmp;
        url = url.replace(oldParameter, (newParameter == null) ? null : (separator + newParameter));
        return url;
    }
}

function GetParameterValue(name, url) {
    if (null == url || null == name || 0 == name.length) return null;
    var urlToCompare = url.toLowerCase();
    name = name.toLowerCase();
    var separator = urlToCompare.contains("?" + name + "=") ? "?" : "&";
    var parameter = separator + name + "=";
    var i1 = urlToCompare.indexOf(parameter);
    if (i1 == -1) return null;
    var tmp = url.substr(i1);
    var i2 = tmp.indexOf("&", 1);
    var value = i2 >= 0 ? tmp.substr(parameter.length, i2 - parameter.length) : tmp.substr(parameter.length);
    return value;
}

String.prototype.contains = function (sub) {
    return this.indexOf(sub, 0) != -1;
}

String.Empty = ""; // Extend static string

String.prototype.Format = function (args) {
    var formatted = this;
    for (var i = 0; i < arguments.length; i++) {
        var regexp = new RegExp('\\{' + i + '\\}', 'gi');
        formatted = formatted.replace(regexp, arguments[i]);
    }
    return formatted;
};

String.prototype.FormatNumber = function (number) { // Format a number to 1,234,567.89
    number += String.Empty;
    x = number.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : String.Empty;
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return this + x1 + x2;
};

String.prototype.escapeHtml = function () { // Replace all ampersands with &amp; and all <’s and >’s with &lt; and &gt;, respectively:
    var result = String.Empty;
    for (var i = 0; i < this.length; i++) {
        if (this.charAt(i) == "&" && this.length - i - 1 >= 4 && this.substr(i, 4) != "&amp;") {
            result = result + "&amp;";
        } else if (this.charAt(i) == "<") {
            result = result + "&lt;";
        } else if (this.charAt(i) == ">") {
            result = result + "&gt;";
        } else {
            result = result + this.charAt(i);
        }
    }
    return result;
};

(function () { // Parse float with string has commas inside parseFloat(string, true)
    var proxied = window.parseFloat;
    window.parseFloat = function () {
        if (arguments[1] === true && typeof (arguments[0]) == 'string') {
            arguments[0] = arguments[0].replace(/,/g, String.Empty); // Replace all occurrences of comma to empty
        }
        return proxied.apply(this, arguments);
    };
})();

// Detect browser

function isIE() {
    return /msie/i.test(navigator.userAgent) && !/opera/i.test(navigator.userAgent);
}
IE = isIE();

// Cookie
function getCookie(name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}

function setCookie(name, value, days, domain) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else {
        var expires = "";
    }
    document.cookie = name + "=" + value + expires + "; path=/;" + ((typeof domain != 'undefined') ? ("domain=" + domain) : String.Empty);
}

function deleteAllCookies() {
    var cookies = document.cookie.split(";");

    for (var i = 0; i < cookies.length; i++) {
        var cookie = cookies[i];
        var eqPos = cookie.indexOf("=");
        var name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
        document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
    }
}

// Detect flash
FLASH_ENABLE = ((navigator.userAgent.match(/iPod/i)) || (navigator.userAgent.match(/iPhone/i)) || (navigator.userAgent.match(/iPad/i))) == null;
setCookie('Flash', !FLASH_ENABLE, 1);

// Manipulate style sheet class name
function HasClassName(element, className) {
    if (typeof element == 'string') element = $(element);
    var elementClassName = element.className;
    if (typeof elementClassName == 'undefined') return false;
    return (elementClassName.length > 0 && (elementClassName == className || new RegExp("(^|\\s)" + className + "(\\s|$)").test(elementClassName)));
}

function AddClassName(element, className) {
    if (typeof element == 'string') element = $(element);
    if (!HasClassName(element, className)) element.className += (element.className ? ' ' : '') + className;
    return element;
}

function RemoveClassName(element, className) {
    if (typeof element == 'string') element = $(element);
    var elementClassName = element.className;
    if (typeof elementClassName == 'undefined') return false;
    element.className = element.className.replace(
    new RegExp("(^|\\s+)" + className + "(\\s+|$)"), ' ');
    return element;
}
// !!!IMPORTANT: This function is reserved for system management, do not use it.

function _addEvent(el, evname, func) {
    if (!el) return;
    if (el.attachEvent) { // IE
        el.attachEvent("on" + evname, func);
    }
    else if (el.addEventListener) { // Gecko / W3C
        el.addEventListener(evname, func, true);
    }
    else { // Opera (or old browsers)
        el["on" + evname] = func;
    }
}
// Prototype of AGE

function AGE()
{ };

AGE.prototype.FormatNumberFloat = function (number) { // Format a number to 12,345
    if (number == String.Empty) return 0;
    var sign;
    if (isNaN(number)) {
        number = String.Empty;
    }
    sign = (number == (number = Math.abs(number)));
    number = Math.floor(number * 100 + 0.50000000001);
    number = Math.floor(number / 100).toString();
    for (var i = 0; i < Math.floor((number.length - (1 + i)) / 3) ; i++) {
        number = number.substring(0, number.length - (4 * i + 3)) + ',' + number.substring(number.length - (4 * i + 3));
    }
    return (((sign) ? String.Empty : '-') + number);
};

// Format input number on event e, using: onkeyup=age.FormatNumber(event)
AGE.prototype.FormatNumber = function (event, isFloat) {
    var evt = event ? event : window.event; // Fix for IE7
    var obj = evt.currentTarget ? evt.currentTarget : evt.srcElement; // Fix for IE7
    var value = obj.value;

    if (value == '') return true;

    var num;
    var dotPosition = 0;
    var afterDot;
    num = value.toString().replace(/\$|\,/g, String.Empty);
    dotPosition = num.indexOf(".");
    if ((typeof (isFloat) != 'undefined') && isFloat && dotPosition > 0) {
        afterDot = num.substring(dotPosition + 1, num.length).replace(/[^0-9]/g, String.Empty);
        num = num.substring(0, dotPosition);
    }
    num = age.FormatNumberFloat(num);
    if ((typeof (isFloat) != 'undefined') && isFloat && dotPosition > 0) {
        eval(obj).value = num + "." + afterDot;
    } else {
        eval(obj).value = num;
    }
    return true;
};

// !!!IMPORTANT: isSingle = true if you want just one function for this event
AGE.prototype.addEvent = function (el, evname, func, isSingle) {
    if (!el) return;
    if (el.attachEvent && isSingle != true) { // IE
        el.attachEvent("on" + evname, func);
    }
    else if (el.addEventListener && isSingle != true) { // Gecko / W3C
        el.addEventListener(evname, func, true);
    }
    else { // Opera (or old browsers)
        el["on" + evname] = func;
    }
}
AGE.prototype.removeEvent = function (el, evname, func) {
    if (!el) return;
    if (el.detachEvent) { // IE
        el.detachEvent("on" + evname, func);
    }
    else if (el.removeEventListener) { // Gecko / W3C
        el.removeEventListener(evname, func, true);
    }
    else { // Opera (or old browsers)
        el["on" + evname] = null;
    }
}
AGE.prototype.stopEvent = function (ev) {
    if (IE) {
        window.event.cancelBubble = true;
        window.event.returnValue = false;
    }
    else {
        ev.preventDefault();
        ev.stopPropagation();
    }
    return false;
};
// Create a html element
AGE.prototype.createElement = function (type, parent) {
    var el = null;
    if (document.createElementNS) {
        // use the XHTML namespace; IE won't normally get here unless
        // _they_ "fix" the DOM2 implementation.
        el = document.createElementNS("http://www.w3.org/1999/xhtml", type);
    }
    else {
        el = document.createElement(type);
    }
    if (typeof parent != "undefined") {
        parent.appendChild(el);
    }
    return el;
}
// Which element is nearer origin Oxy
AGE.prototype.beforeElement = function (o1, o2) {
    if (typeof o2 == 'undefined') return o1;
    if (typeof o1 == 'undefined') return o2;
    // Tto get offset position
    o1.style.position = 'relative';
    o2.style.position = 'relative';
    // Compare distance from origin
    var d1 = o1.offsetTop * o1.offsetTop + o1.offsetLeft * o1.offsetLeft;
    var d2 = o2.offsetTop * o2.offsetTop + o2.offsetLeft * o2.offsetLeft;
    if (d1 > d2) return o2;
    else
        return o1;
}
// Find first form element in px position with tag name
AGE.prototype.firstFormElement = function (tag) {
    var items = document.getElementsByTagName(tag);
    if (items.length > 0) {
        var c = items.length > 5 ? 5 : items.length;
        for (var i = 0; i < c; i++) {
            if (!items[i].getAttribute('noFocus') && // property indicate do not focus on this item
            !items[i].disabled && items[i].type != 'hidden' && !items[i].readonly && items[i].type != 'checkbox' && items[i].type != 'radio') {
                return items[i];
            }
        }
    }
}
// Calculate view area
AGE.prototype.CalculateViewport = function () {
    this.viewport = { width: 0, height: 0 };
    if (!IE) {
        // in standards compliant mode (i.e. with a valid doctype as the first line in the document)
        if (typeof document.documentElement != 'undefined' && typeof document.documentElement.scrollWidth != 'undefined' && document.documentElement.scrollWidth != 0) {
            this.viewport.width = document.documentElement.scrollWidth;
            this.viewport.height = document.documentElement.scrollHeight;
        }
        this.viewport.width = Math.max(this.viewport.width, (Math.max(document.body.scrollWidth, document.body.clientWidth)));
        this.viewport.height = Math.max(this.viewport.height, (Math.max(document.body.scrollHeight, document.body.clientHeight)));
    }
    else {
        // in standards compliant mode (i.e. with a valid doctype as the first line in the document)
        if (typeof document.documentElement != 'undefined' && typeof document.documentElement.clientWidth != 'undefined' && document.documentElement.clientWidth != 0) {
            this.viewport.width = document.documentElement.clientWidth;
            this.viewport.height = document.documentElement.clientHeight;
        }
        this.viewport.width = Math.max(document.body.scrollWidth, this.viewport.width);
        this.viewport.height = Math.max(document.body.scrollHeight, this.viewport.height);
    }
}
// Create a mask div will cover whole page
AGE.prototype.CreateMaskDiv = function () {
    this.divMaskLoading = this.createElement("div");
    var div1 = this.createElement("div");
    div1.style.width = '100px';
    div1.style.height = '100px';
    div1.style.position = 'relative';
    this.divMaskLoading.style.display = 'none';
    this.divMaskLoading.style.position = 'absolute';
    this.divMaskLoading.style.top = '0px';
    this.divMaskLoading.style.left = '0px';
    this.divMaskLoading.style.filter = 'alpha(opacity=50)';
    this.divMaskLoading.style.opacity = '0.5';
    this.divMaskLoading.style.backgroundColor = 'white';
    this.divMaskLoading.appendChild(div1);
    document.body.appendChild(this.divMaskLoading);
}
AGE.prototype.RemoveBookmarksInUrl = function (url) {
    if (typeof url != 'string') return null;
    var i = url.indexOf('#');
    if (i == -1) return url;
    return url.substr(0, i);
}
// Show a mask div cover whole page
AGE.prototype.ShowMaskDiv = function (withIcon) {
    if (!this.divMaskLoading) return;
    this.CalculateViewport();
    this.divMaskLoading.style.width = (this.viewport.width - 2) + 'px';
    this.divMaskLoading.style.height = (this.viewport.height - 2) + 'px';
    this.divMaskLoading.firstChild.style.left = Math.floor(this.viewport.width / 2) + 'px';
    this.divMaskLoading.firstChild.style.top = document.documentElement.scrollTop + 'px';
    this.divMaskLoading.firstChild.className = withIcon ? 'MaskLoadingDiv' : '';
    this.divMaskLoading.style.display = 'block';
}
// Hide showed mask div
AGE.prototype.HideMaskDiv = function () {
    if (!this.divMaskLoading) return;
    this.divMaskLoading.style.display = 'none';
}
AGE.prototype.ReloadPage = function (time) {
    var age = new AGE();
    this.ShowMaskDiv(true);
    var delay = time ? time : 3000;
    this.delayTimer = setTimeout("age.HideMaskDiv()", delay);
}

AGE.prototype.ReloadParentPage = function (url, time) {
    this.ShowMaskDiv(true);
    var delay = time ? time : 3000;
    if (!url) {
        parent.location.nextUrl = this.RemoveBookmarksInUrl(location.href);
        this.delayTimer = setTimeout("parent.location = location.nextUrl", delay);
    }
    else {
        this.delayTimer = setTimeout("parent.location = '" + url + "'", delay);
    }
}

// Reload page with provided url and delay time
AGE.prototype.DelayReloadPage = function (url, time) {
    this.ShowMaskDiv(true);
    var delay = time ? time : 3000;
    if (!url) {
        location.nextUrl = this.RemoveBookmarksInUrl(location.href);
        this.delayTimer = setTimeout("location = location.nextUrl", delay);
    }
    else {
        this.delayTimer = setTimeout("location = '" + url + "'", delay);
    }
}
// Catch enter event and do the action, using by onkeydown="age.DoEnter(...)"
AGE.prototype.DoEnter = function (evt, action) {
    if (null == age) return;
    if (IE) {
        if (window.event.keyCode == 13) {
            eval(action);
            age.stopEvent(evt);
            return false;
        }
    }
    else {
        if (evt.keyCode == 13) {
            eval(action);
            age.stopEvent(evt);
            return false;
        }
    }
}
AGE.prototype.Lock = function (withIcon) {
    this.ShowMaskDiv(withIcon);
    this.locked = true;
}
AGE.prototype.UnLock = function () {
    this.HideMaskDiv();
    this.locked = false;
}
// Refresh whole site to update session, cookies...
AGE.prototype.Refresh = function () {
    var wnd = window.parent;
    while (wnd != wnd.parent) // Jump out
    {
        wnd = wnd.parent;
    }
    wnd.location = wnd.location.href;
}
AGE.prototype.GetBaseUrl = function () {
    var url = top.baseUrl;
    if (!url) {
        var urlArray = window.location.href.split('/');
        var protocol = urlArray[0];
        var host = urlArray[2];
        url = protocol + '//' + host + '/';
    }

    return url;
}
AGE.prototype.GetExTotalBetsForecastUrl = function () {
    var url = top.exTotalBetsForecastRootPath;
    if (!url) {
        var urlArray = window.location.href.split('/');
        var protocol = urlArray[0];
        var host = urlArray[2];
        url = protocol + '//' + host + '/';
    }

    return url;
}
AGE.prototype.GetQueryString = function (key, defaultValue) {
    if (defaultValue == null) defaultValue = "";
    key = key.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regex = new RegExp("[\\?&]" + key + "=([^&#]*)");
    var qs = regex.exec(window.location.href);
    if (qs == null) return defaultValue;
    else
        return qs[1];
} /*End*/
/*
* Created 20091224@Lee - Light ajax library
*
*!!!CAUTION: ANY REVISION MUST BE APPROVED BEFORE PROCEEDING!!!
* Revision ?@? - ...
*
*/
//jx.request(url,function(result){...},method,params,async);
//method:post,get
//async:true,false
//params:a string, an object or an array
jx = {
    toQueryString: function (obj) {
        if (typeof obj == 'object') {
            var arr = new Array();
            for (var att in obj) {
                arr.push(att + '=' + eval('obj.' + att));
            }
            return arr.join('&');
        }
        else if (typeof obj == 'string') {
            return obj.replace('?', '');
        }
        else {
            return obj;
        }
    },
    getHTTPObject: function () {
        var A = false;
        if (typeof ActiveXObject != "undefined") {
            try {
                A = new ActiveXObject("Msxml2.XMLHTTP")
            }
            catch (C) {
                try {
                    A = new ActiveXObject("Microsoft.XMLHTTP")
                }
                catch (B) {
                    A = false
                }
            }
        }
        else {
            if (window.XMLHttpRequest) {
                try {
                    A = new XMLHttpRequest()
                }
                catch (C) {
                    A = false
                }
            }
        }
        return A
    },
    request: function (url, callback, method, params, async, headers) {
        var http = this.init();
        if (!http || !url || !method) {
            return
        }
        try {
            method = method.toUpperCase();
            if (typeof async == "undefined") async = true;
            var isGet = (method == "GET");
            var ch = (url.indexOf("?") == -1) ? "?" : "&";
            ch = params ? ch : "";
            http.open(method, (isGet && typeof params != "undefined") ? url + ch + this.toQueryString(params) : url, async);
            if (headers) {
                for (var headerName in headers) {
                    http.setRequestHeader(headerName, headers[headerName]);
                }
            }

            //// For anti forgery request
            var antiXSRFToken = document.getElementsByName("__RequestVerificationToken");
            if (antiXSRFToken.length > 0) {
                http.setRequestHeader("__RequestVerificationToken", antiXSRFToken[0].value);
            }

            http.onreadystatechange = function () {
                if (http.readyState == 4) {
                    if (http.status == 200) {
                        var result = http;
                        if (callback && async) {
                            callback(result, http.getResponseHeader('Date'))
                        }
                    }
                    else if (http.status != 0) { /*alert(http.statusText)*/
                        if (callback && async) {
                            callback({ 'errCode': http.status, 'errMsg': http.statusText }, http.getResponseHeader('Date'))
                        }
                    }
                }
            }; if (!isGet) {
                //// Please do not remove it, it use for security code http module
                http.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
                http.setRequestHeader("IsPostAjax", "true");
            };
            http.send(isGet ? null : this.toQueryString(params));
            if (!async) callback(http);
            return http;
        }
        catch (e) { callback(http); }
    },
    init: function () {
        return this.getHTTPObject()
    }
}
// Ajax class

function AJAX()
{ };
AJAX.prototype.Request = function (url, options) {
    //url:'url?var1=val1&var2=val2...'
    //options:{method:'get/post',asynchronous:true/false,parameters:'var3=val3&var4=val4',onComplete:function(data){}}
    var method = 'post';
    var callback = function () { },
        params = null;
    var async = true;
    if (options) {
        if (typeof options.method != 'undefined') method = options.method;
        if (typeof options.parameters != 'undefined') params = options.parameters;
        if (typeof options.onComplete != 'undefined') callback = options.onComplete;
        else if (typeof options.onSuccess != 'undefined') callback = options.onSuccess;
        if (typeof options.asynchronous != 'undefined') async = options.asynchronous;
    }
    return jx.request(url, callback, method, params, async);
}
//JSON Get
//JSON Post
//asynchronous=true
AJAX.prototype.CreateParams = function () {
    // Create params string from array of element id
    var params = new Array();
    var c = arguments.length;
    for (var i = 0; i < c; i++) {
        var element = document.getElementById(arguments[i]);
        var query = element.name;
        if (null == query || '' == query) {
            query = element.id;
        }
        var value = null;
        if (element.tagName == 'INPUT') {
            if (element.type == 'checkbox' || element.type == 'radio') value = element.checked;
            else
                value = element.value;
        }
        else if (element.tagName == 'SELECT') value = element.value;
        if (null != value) params.push(query + '=' + encodeURIComponent(value));
    }
    return params.join('&');
}
AJAX.prototype.CreateParams2 = function (ids) {
    // Create params string from array of element id
    var params = new Array();
    var c = ids.length;
    for (var i = 0; i < c; i++) {
        var element = document.getElementById(ids[i]);
        var query = element.name;
        if (null == query || '' == query) {
            query = element.id;
        }
        var value = null;
        if (element.tagName == 'INPUT') {
            if (element.type == 'checkbox' || element.type == 'radio') value = element.checked;
            else
                value = element.value;
        }
        else if (element.tagName == 'SELECT') value = element.value;
        if (null != value) params.push(query + '=' + encodeURIComponent(value));
    }
    return params.join('&');
}
AJAX.prototype.GetJSON = function (url, params, callback, async, headers) {
    var callback1 = function (result) {
        var data = '({"errCode":-1, "errMsg":"Oops... you are here because of a system error, please notify the administrators!"})';
        try {
            if (result.getResponseHeader("Content-type").indexOf("text/plain") != -1) {
                data = '(' + result.responseText + ')';
            }
        }
        catch (e) {
        }
        var obj = eval(data);
        callback(obj);
    }
    if (typeof async != 'boolean') async = true;
    return jx.request(url, callback1, 'GET', params, async, headers);
}

AJAX.prototype.PostJSON = function (url, params, callback, async, headers) {
    var callback1 = function (result) {
        var data = '({"errCode":-1, "errMsg":"Oops... you are here because of a system error, please notify the administrators!"})';
        try {
            if (result.getResponseHeader("Content-type").indexOf("text/plain") != -1
                || result.getResponseHeader("Content-type").indexOf("application/json; charset=utf-8") != -1) {
                data = '(' + result.responseText + ')';
            }
        }
        catch (e) {
        }
        var obj = eval(data);
        if (obj.code === _needSecurityCode) {
            //var url = '/' + obj.errMsg.substring(1);
            top[url] = function () {
                ajax.PostJSON(url, params, callback, async, headers);
            };

            top.ShowSecCodePopup(url, age); // check to display SecCode popup
        }
        else {
            callback(obj);
        }
    }
    if (typeof async != 'boolean') async = true;
    return jx.request(url, callback1, 'POST', params, async, headers);
}
/**************OPEN ASYNS TASK SECTION************/
// Support register from child frame or child window
function RegisterAsyncRequest(url, parameters, method, delay) {
    setTimeout(function () {
        ajax.Request(url.clone(), { method: method.clone(), parameters: parameters.clone() });
    }, delay.clone());
}

/**************CLOSE ASYNS TASK SECTION***********/

/**************OPEN START-UP SECTION**************/
_page = {}; // root JSON object
var _startupCommands = new Array();

function RegisterStartUp(command) {
    _startupCommands.push(command);
}
// Delay init object to window.onload event to boots up performance
age = null;
ajax = null;

function _startup() {
    // $ function
    window.$ = $;
    if (typeof window.parent.$ == 'undefined') {
        window.parent.$ = function (id) {
            return this.document.getElementById(id);
        }
    }
    age = new AGE();
    ajax = new AJAX();
    // Focus at startup
    if (typeof _focusElementId == 'undefined') {
        var input = age.firstFormElement("input");
        var select = age.firstFormElement("select");
        var item = age.beforeElement(input, select);
        try {
            if (item) item.focus();
        }
        catch (e)
        { }
    }
    else if (_focusElement != -1) {
        var _focusElement = $(_focusElementId);
        if (_focusElement != null && typeof _focusElement == 'object') _focusElement.focus();
    }
    age.CreateMaskDiv();
    // Calculate width and height of view port
    age.CalculateViewport();
    // Execute startup commands
    var commands = _startupCommands;
    var length = commands.length;
    for (var i = 0; i < length; i++) {
        var command = commands[i];
        if (typeof (command) == 'string') {
            eval(command);
        }
        else if (typeof (command) == 'function') {
            command.call(this);
        };
    }

    window.loaded = true;
    checkPermission();
}

function checkPermission() {
    try {
        var bolBtnSubmit = checkButtonPermission('btnSubmit');
        var bolBtnSubmitFooter = checkButtonPermission('btnSubmitFooter');
        var bolBtnCancel = checkButtonPermission('btnCancel');
        var bolBtnReset = checkButtonPermission('btnReset');
        var bolBtnUpdRacingStatus = checkButtonPermission('btnUpdRacingStatus');
        if (bolBtnSubmit || bolBtnSubmitFooter || bolBtnCancel || bolBtnReset || bolBtnUpdRacingStatus) {
            // Check Type Input
            var ids = document.getElementsByTagName("input");
            for (var i = 0; i < ids.length; i++) {
                var permission = ids[i].getAttribute('AccountPermisssion');
                if (permission != null) {
                    if (permission.length != 0) {
                        if (top.AGE.PERMISSIONS.indexOf(permission) == -1) {
                            ids[i].disabled = 'disable';
                        }
                    }
                }
            }

            // Check Type Button
            var ids = document.getElementsByTagName("button");
            for (var i = 0; i < ids.length; i++) {
                var permission = ids[i].getAttribute('AccountPermisssion');
                if (permission != null) {
                    if (permission.length != 0) {
                        if (top.AGE.PERMISSIONS.indexOf(permission) == -1) {
                            ids[i].disabled = 'disable';
                        }
                    }
                }
            }
        }
    }
    catch (e) {
    }
}

function checkButtonPermission(id) {
    try {
        if (document.getElementById(id) != null && document.getElementById(id) != undefined) {
            var permission = document.getElementById(id).getAttribute('AccountPermisssion');
            if (permission == null || permission.length == 0) return false;
            if (top.AGE.PERMISSIONS.indexOf(permission) == -1) {
                document.getElementById(id).disabled = 'disable';
                return true;
            }
        }
    }
    catch (e) { return false; }
    return false;
}

_addEvent(window, "load", _startup); /************CLOSE START-UP SECTION*****************/

/*
* Created 20091224@Lee - Common business javascript functions
* Revision ?@? - ...
*
*/
// Frameset
// top.menu, top.main, top.frHeader, top.frFooter
// Error code
var UNKNOWN = -1;
var ACCESS_DENIED = 1;
var ACCOUNT_CLOSED = 2;
var KICKED_OUT = 3;
var UNDER_MAINTENANCE = 4;
// Reload frames to update language or configuration
AGE.prototype.Invalidate = function () {
    top.location = age.RemoveBookmarksInUrl(top.location.href);
}
// SignOut
AGE.prototype.SignOut = function () {
    var index = window;
    while (index != index.parent) // Jump out
    {
        index = index.parent;
    }
    index.location = this.GetBaseUrl() + '_Authorization/Handlers/SignOut.ashx';
    if (index.location.href.indexOf('//imb.') == 0 ||
        index.location.href.indexOf('//int.') == 0 ||
        index.location.href.indexOf('//i7y.') == 0) {
        index.close();
    }
}

function IsPassword(str) {
    var regex1 = /^(?=.{8,100}$).*\d/i;
    var regex2 = /^(?=.{8,100}$).*[a-z]/;
    var regex3 = /^(?=.{8,100}$).*[A-Z]/;
    var regex4 = /^(?=.{8,100}$).*[\[\]\^\$\.\|\?\*\+\(\)\\~`\!@#%&\-_+={}'""<>:;,]/i;

    var whiteSpace = /^\S*$/;

    var case1 = str.search(regex1) != -1 ? 1 : 0;
    var case2 = str.search(regex2) != -1 ? 1 : 0;
    var case3 = str.search(regex3) != -1 ? 1 : 0;
    var case4 = str.search(regex4) != -1 ? 1 : 0;

    var case5 = str.search(whiteSpace) != -1;
    //console.log((case1 + '_' + case2 + '_' + case3 + '_' + case4));
    return (case1 + case2 + case3 + case4 >= 3) && case5;
}

function IsMemberPassword(str) {
    var regex = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,10}$/;
    var whiteSpace = /^\S*$/;

    var case5 = str.search(whiteSpace) != -1;

    return (str.search(regex) != -1) && case5;
}

var _needSecurityCode = 999991;
var _newFlowNeedSecurityCode = 6;

function IsSecurityCode(secCode) {
    var secCodeLength = secCode.length;
    if (secCodeLength != 6) { // check length
        return false;
    }

    var regex = /^([0-9])(?!\1+$)[0-9]+$/; // check digits and all of them are not the same
    var result = secCode.search(regex) != -1 ? true : false;
    if (result) { // check 'Forward Run' and 'Backward Run'
        var isForwardRun = true, isBackwardRun = true;
        var forward = 0, backward = 0;
        forward = backward = parseInt(secCode.charAt(0));
        for (var i = 1; i < secCodeLength; i++) {
            var current = parseInt(secCode.charAt(i));
            if (++forward != current) {
                isForwardRun = false;
            }
            if (--backward != current) {
                isBackwardRun = false;
            }
        }
        result = !(isForwardRun || isBackwardRun);
    }

    return result;
}

function ShowSecCodePopup(preUrl, callerAge) {
    //preUrl = 'url=' + escape(preUrl);
    var url = top.mainRootPath + 'SecurityCode/VerifySecurityCode';
    var popH = 300, popW = 500;

    function unLockChildWindow(w) {
        if (w) {
            if (w.age) {
                w.age.UnLock();
            }
            var c = w.frames.length;
            for (var i = 0; i < c; i++) {
                if (w.frames[i].age) {
                    unLockChildWindow(w.frames[i]);
                }
            }
        }
    }
    clearPopupSubtitle();
    ageWnd.onClosing = function () {
        var c = top.frames.length;
        for (var i = 0; i < c; i++) {
            var w = top.frames[i];
            unLockChildWindow(w);
        }

        top.age.UnLock();
        return true;
    }

    top.age.Lock();
    top.securityCodePopup = top.ageWnd.createInstance();
    top.securityCodePopup.onClosing = function () {
        try {
            top.age.UnLock();

            if (callerAge) {
                callerAge.UnLock();
            }

        } catch (e) {
        }

        return true;
    };

    //function (handler, url, title, width, height, windowCss, titleCss, closeBtnCss, left, top, iframeAttributes)
    top.popupManager.openWithHanlder(top.securityCodePopup, url, '', popW, popH, null, null, null, null, null, {
        preUrl: preUrl
    });
}

function ShowCaptchaPopup() {
    var url = 'SpamFilter/Captcha/PageAccessCaptcha';
    var popH = 300, popW = 400, verticalOffset = -75;
    var x = (screen.width / 2) - (popW / 2) + verticalOffset;
    var y = (screen.height / 2) - (popH / 2);

    function unLockChildWindow(w) {
        if (w) {
            if (w.age) {
                w.age.UnLock();
            }
            var c = w.frames.length;
            for (var i = 0; i < c; i++) {
                if (w.frames[i].age) {
                    unLockChildWindow(w.frames[i]);
                }
            }
        }
    }

    ageWnd.onClosing = function () {
        var c = top.frames.length;
        for (var i = 0; i < c; i++) {
            var w = top.frames[i];
            unLockChildWindow(w);
        }

        top.age.UnLock();
        return true;
    }

    top.age.Lock();
    top.popupManager.openByRelativeUrl(url, '', popW, popH);
}

// Add popup height when show error
var _preHeight = 0;

function AddPopupHeight(h) {
    ageWnd = top ? top.ageWnd : undefined;
    if (typeof ageWnd == 'undefined') return;
    if (_preHeight == 0) {
        _preHeight = ageWnd.height;
    }

    ageWnd.setRect(null, null, ageWnd.width, _preHeight + h);
}

function OpenIPInfo(ip) {
    top.popupManager.openByRelativeUrl('_IPInfo/IpInfo.aspx?ip=' + ip, '', 400, 300);
    if (navigator.userAgent.match(/iPad/i) != null) {
        top.popupManager.openByRelativeUrl('_IPInfo/IpInfo.aspx?ip=' + ip, '', 400, 300);
    }
} /*End*/

function getPrint(print_area) {
    var printContent = $(print_area);
    var printWindow = window.open('', '', 'left=500,top=400,width=200,height=5');
    printWindow.document.write("<html>");
    printWindow.document.write("<head>");
    printWindow.document.write("</head>");
    printWindow.document.write("<body style='margin-top: 100px'>");
    printWindow.document.write(printContent.innerHTML);
    printWindow.document.write("</body></html>");
    printWindow.document.close();
    printWindow.focus();
    printWindow.print();
    printWindow.close();
}
function parseBool2(str) {
    var boolmap = {
        'no': false,
        'NO': false,
        'FALSE': false,
        'False': false,
        'false': false,
        'yes': true,
        'YES': true,
        'TRUE': true,
        'True': true,
        'true': true
    };

    return (str in boolmap && boolmap.hasOwnProperty(str)) ?
      boolmap[str] : !!str;
};