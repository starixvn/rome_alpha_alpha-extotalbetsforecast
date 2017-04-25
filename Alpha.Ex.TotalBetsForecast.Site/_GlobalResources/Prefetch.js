var __ihttp = 0; var __chttp = 0; var __arrs = null; var __date1 = null; var __date2 = null; var __num = 8;
var __loc1 = '../../';
var __loc2 = '../';
function isIE() { return /msie/i.test(navigator.userAgent) && !/opera/i.test(navigator.userAgent); }
var __ie = isIE();
function __http_caching(result, date) {
    if (__ihttp == __chttp || __num < 0) return;
    if (__ie && date + '' == '') __num--;
    else if (__ie) __num = 8;
    else {
        __date2 = Date.parse(date);
        if (__date2 - __date1 < 0) __num--;
        else __num = 8;
    }
    jx.request(__loc1 + __arrs[__ihttp++], __http_caching, 'get');
}

function __http_preload(result, date) { __date1 = Date.parse(date); eval('__arrs = ' + result.responseText); __chttp = __arrs.length; jx.request(__loc1 + __arrs[__ihttp++], __http_caching, 'get'); }
function __http_start() { jx.request(__loc2 + '_GlobalResources/Prefetch.ashx', __http_preload, 'get'); }
setTimeout(__http_start, 2000);
