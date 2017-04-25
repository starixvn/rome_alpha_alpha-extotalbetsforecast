/*
* Created 20100602@Lee - Paging javascript prototype
* Url: ?pageSize=x&pageIndex=y
* Revision ?@? - ... 
* 
*/
function isIE() { return /msie/i.test(navigator.userAgent) && !/opera/i.test(navigator.userAgent); }

var PAGE_SIZE_P = "pageSize";
var PAGE_INDEX_P = "pageIndex";

Paging = function(id) {
    this.id = id;

    var el = document.getElementById(this.id);

    this.pagesize = parseInt(el.getAttribute('pagesize'));
    this.currentindex = parseInt(el.getAttribute('currentindex'));
    this.rowcount = parseInt(el.getAttribute('rowcount'));
    this.pagecount = parseInt(el.getAttribute('pagecount'));
    
    Paging_addEvent('btnFirst' + id, 'mouseover', Paging_ElementOver);
    Paging_addEvent('btnFirst' + id, 'mouseout', Paging_ElementOut);
    Paging_SetDisableStyle('btnFirst' + id);

    Paging_addEvent('btnPrev' + id, 'mouseover', Paging_ElementOver);
    Paging_addEvent('btnPrev' + id, 'mouseout', Paging_ElementOut);
    Paging_SetDisableStyle('btnPrev' + id);

    Paging_addEvent('btnNext' + id, 'mouseover', Paging_ElementOver);
    Paging_addEvent('btnNext' + id, 'mouseout', Paging_ElementOut);
    Paging_SetDisableStyle('btnNext' + id);

    Paging_addEvent('btnLast' + id, 'mouseover', Paging_ElementOver);
    Paging_addEvent('btnLast' + id, 'mouseout', Paging_ElementOut);
    Paging_SetDisableStyle('btnLast' + id);

    var sel = document.getElementById('sel' + id);
    if (sel) {
        var minPageSize = parseInt(sel.options[0].value);
        if (this.rowcount < minPageSize) {
            el.style.display = 'none';
        }
    }
}

Paging.prototype.id = null;
Paging.prototype.pagesize = 0;
Paging.prototype.currentindex = 0;
Paging.prototype.rowcount = 0;
Paging.prototype.pagecount = 0;

Paging.prototype.Move = function(event, distance)
{
    if (!this.ValidateStatusBeforeCallEvent(event)) {
        return;
    }

    var newIndex = this.currentindex + distance;
    if (newIndex <= 0) newIndex = 1;
    else if (newIndex > this.pagecount) newIndex = this.pagecount;

    this.Go(newIndex);
}

Paging.prototype.Go = function(index)
{
    if (this.rowcount <= 0) return;
    
    if(!index){ // Get from textbox
        index = document.getElementById('txt' + this.id).value;
    }

    if(index <= 0) index = 1;
    
    if(index > 0 && index <= this.pagecount) {
        this.currentindex = index;
        location = this.UpdateUrl(location.href);
    }
}

Paging.prototype.First = function(event)
{
    if (!this.ValidateStatusBeforeCallEvent(event)) {
        return;
    }

    this.Go(1);
}

Paging.prototype.Last = function (event)
{
    if (!this.ValidateStatusBeforeCallEvent(event)) {
        return;
    }

    this.Go(this.pagecount);
}

Paging.prototype.ValidateStatusBeforeCallEvent = function(event){
    if (event !== undefined) {
        var isDisabled = event.getAttribute('disabled');
        if (isDisabled === '' ||
            isDisabled === 'disabled') {
            return false;
        }
    }

    return true;
}

Paging.prototype.SetPageSize = function(size)
{
    if(size <= 0 || this.rowcount <= 0) return;
    this.pagesize = size;
    this.currentindex = 1;
    this.pagecount = Math.floor(this.rowcount / this.pagesize) + (this.rowcount % this.pagesize == 0 ? 0 : 1);
    
    var el = document.getElementById(this.id);

    el.setAttribute('pagesize', this.pagesize);
    el.setAttribute('currentindex', this.currentindex);
    el.setAttribute('pagecount', this.pagecount);
    
    this.Go(this.currentindex);
}

Paging.prototype.UpdateUrl = function(url)
{
    if(url) {
        url = SetParameterValue(PAGE_INDEX_P, this.currentindex, url);
        url = SetParameterValue(PAGE_SIZE_P, this.pagesize, url);
        
        url = this.RemoveBookmarksInUrl(url);
        return url;
    }
}

Paging.prototype.RemoveBookmarksInUrl = function(url) {
    if (typeof url != 'string') return null;
    var i = url.indexOf('#');
    if (i == -1) return url;
    return url.substr(0, i);
}

Paging.prototype.DoEnter = function(evt, action) {
    if (isIE()) {
        if (window.event.keyCode == 13) {
            eval(action);
            Paging_stopEvent(evt);
        }
    }
    else {
        if (evt.keyCode == 13) {
            eval(action);
            Paging_stopEvent(evt);
        }
    }


}

function Paging_ElementOver(evt)
{
    var obj = evt.target || evt.srcElement;
    AddClassName(obj, 'pagingElementOver');// Require Core.js
}

function Paging_ElementOut(evt)
{
     var obj = evt.target || evt.srcElement;
    RemoveClassName(obj, 'pagingElementOver');// Require Core.js
}

function Paging_SetDisableStyle(id)
{
    var el = document.getElementById(id);    
    if(el.disabled)
    {
        AddClassName(el, 'pagingDisable');     
    }
    else
    {
        RemoveClassName(el, 'pagingDisable');
    }
}

// Library functions

function HasClassName(element, className) {
    if (typeof element == 'string') element = $(element);
    var elementClassName = element.className;
    if (typeof elementClassName == 'undefined') return false;
    return (elementClassName.length > 0 && (elementClassName == className ||
  new RegExp("(^|\\s)" + className + "(\\s|$)").test(elementClassName)));
}

function AddClassName(element, className) {
    if (typeof element == 'string') element = $(element);
    if (!HasClassName(element, className))
        element.className += (element.className ? ' ' : '') + className;
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

function Paging_addEvent(el, evname, func) {
    if(typeof(el) == 'string') el = document.getElementById(el);
	if (el.attachEvent) { // IE
		el.attachEvent("on" + evname, func);
	} else if (el.addEventListener) { // Gecko / W3C
		el.addEventListener(evname, func, true);
	} else { // Opera (or old browsers)
		el["on" + evname] = func;
	}
}

function Paging_stopEvent(ev) {
    if (isIE()) {
        window.event.cancelBubble = true;
        window.event.returnValue = false;
    } else {
        ev.preventDefault();
        ev.stopPropagation();
    }
    return false;
};


String.prototype.contains = function(sub)
{
    return this.indexOf(sub, 0) != -1;
}

function SetParameterValue(name, value,  url)
{
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

function GetParameterValue(name, url)
{
    if (url == null) return null;

    var separator = url.contains("?" + name + "=") ? "?" : "&";

    var parameter = separator + name + "=";

    var i1 = url.indexOf(parameter);
    if (i1 == -1) return null;

    var tmp = url.substr(i1);
    var i2 = tmp.indexOf("&", 1);

    var value = i2 >= 0 ? tmp.substr(parameter.Length, i2 - parameter.Length) : tmp.substr(parameter.Length);

    return value;
}