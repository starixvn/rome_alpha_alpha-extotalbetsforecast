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
    request: function (url, callback, method, params, async) {
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
                http.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
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
    var callback = 'void(0)',
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
AJAX.prototype.GetJSON = function (url, params, callback, async) {
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
    return jx.request(url, callback1, 'GET', params, async);
}
AJAX.prototype.PostJSON = function (url, params, callback, async) {
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
    return jx.request(url, callback1, 'POST', params, async);
}

ajax = new AJAX();