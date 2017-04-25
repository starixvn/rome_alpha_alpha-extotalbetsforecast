AGEWnd = function (docContext) {
    this.is_ie = (/MSIE (\d+\.\d+);/.test(navigator.userAgent));
    this.is_opera = (/Opera[\/\s](\d+\.\d+)/.test(navigator.userAgent));
    this.dragging = false;
    this.createdElement = false;
    this.displayBtnClose = true;

    var scripts = document.getElementsByTagName("script");

    this.js = "AGEWnd.js";
    this.css = "AGEWnd.css";
    this.blank = "blank.html";

    if (scripts.length > 0) {
        for (var i = 0; i < scripts.length; i++) {
            var idx = scripts[i].src.indexOf(this.js);
            if (idx != -1) {
                this.baseURL = scripts[i].src.substr(0, idx);
                break;
            }
        }
    }

    if (typeof this.blankURL != "undefined") {
        this.cssURL = this.baseURL + this.css;
        this.jsURL = this.baseURL + this.js;
        this.blankURL = this.baseURL + this.blank;
    }

    if (docContext != undefined) {
        this.docContext = docContext;
    }
}

AGEWnd.prototype.open = function (url, titleText, left, top, width, height, windowCss, titleCss, closeBtnCss, iframeAttributes) {
    if (typeof left == 'undefined' || typeof top == 'undefined' || typeof width == 'undefined' || typeof height == 'undefined' || typeof url == 'undefined') {
        alert('ageWnd.open(url, titleText, left, top, width, height, windowCss, titleCss, closeBtnCss)\r\n\r\n\   - Invalid parameters  ');
        return false;
    }

    AGEWnd_preWidth = 0;
    AGEWnd_preHeight = 0;

    if (AGEWnd_autoKeepPosition || this.autoKeepPosition) {
        if (typeof AGEWnd_preLeft != 'undefined') left = AGEWnd_preLeft;
        if (typeof AGEWnd_preTop != 'undefined') top = AGEWnd_preTop;
    }

    var self = this;

    if (!this.createdElement) {
        this.mask = this.createElement("div");
        this.mask.className = "AGEWndMask";
        this.mask.style.position = "absolute";
        this.mask.style.display = "none";
        this.mask.innerHTML = "<input class=\"AGEWndTitleMask\" readonly=\"true\" type=\"text\" value=\"\" />";

        this.element = this.createElement("div");
        this.element.className = (windowCss ? (windowCss) : "AGEWnd");
        this.element.style.position = "absolute";
        this.element.style.display = "none";
        this.element.style.zIndex = 1000;

        var table = this.createElement("table", this.element);
        table.cellPadding = 0; table.cellSpacing = 0; table.className = "AGEWndTable"; table.border = 0;
        var thead = this.createElement("thead", table);
        var tr = this.createElement("tr", thead);
        tr.oncontextmenu = function () { return false; };
        tr.className = (titleCss ? (titleCss) : "AGEWndTitle");
        tr.style.height = "39px"; tr.style.overflow = "hidden"; tr.style.zIndex = 1000;

        var td1 = this.createElement("td", tr);
        td1.className = "AGEWndTitleText"; td1.style.cursor = "move";
        this.addEvent(td1, "mousedown", function (event) {

            self.dragStart.call(self, event);
        });

        td1.className = (titleCss ? (titleCss) : "AGEWndTitle");
        this.maskTitle = td1;
        var div1 = this.createElement("div", td1); div1.style.height = "39px"; div1.style.display = "block"; div1.style.zIndex = 1000;
        div1.style.lineHeight = "39px"; div1.style.color = "#333"; div1.style.textAlign = "left";
        div1.className = "AGEWndTitleDiv";
        div1.id = "AGEWndTitleDiv";
        this.title = div1;

        var td2 = this.createElement("td", tr); td2.className = "AGEWndTitleButton";
        td2.onclick = function () {
            self.close();
        };
        td2.oncontextmenu = function () { return false; };

        if (this.displayBtnClose) {
            td2.innerHTML = "<div id=\"AGEWndCloseButton\" oncontextmenu=\"return false\" title=\"Close\" class=\"" + (closeBtnCss ? closeBtnCss : "AGEWndCloseButton") + "\" style=\"float:right\"></div>";
        }
        else {
            td2.onclick = function () { return false; }
        }

        td2.vAlign = "center"; td2.align = "right";
        td2.className = (titleCss ? (titleCss) : "AGEWndTitle");
        var tbody = this.createElement("tbody", table);

        tr = this.createElement("tr", tbody);
        td = this.createElement("td", tr); td.colSpan = 2;
        td.align = "left"; td.vAlign = "top"; td.className = "AGEWndNoPadding";
        this.content = td;

        this.iframe = this.createIframe();
        this.content.appendChild(this.iframe);

        var docContext = this.getDocContext();

        docContext.body.appendChild(this.mask);
        docContext.body.appendChild(this.element);

        this.createdElement = true;
    }

    this.setRect(left, top, width, height);

    this.url = url;
    if (typeof this.blankURL != "undefined") {
        this.iframe.src = this.blankURL;
    }
    this.iframe.src = url;
    this.loaded = false;

    this.title.innerHTML = titleText;
    this.element.style.display = 'block';

    // Listen event
    this.addEvent(this.iframe, "load", function () {
        var iframe_doc = self.is_ie ? self.iframe.contentWindow.document : self.iframe.contentDocument;

        if (AGEWnd._W.title.innerHTML == '' && iframe_doc.title != 'Home') {
            AGEWnd._W.title.innerHTML = iframe_doc.title;
        }

        self.addEvent(iframe_doc, "keydown", self.keydown);
    });

    this.addEvent(window, "resize", this.calculateViewport());

    // add public reference for active window
    AGEWnd._W = this;

    //// cached data for iframe
    if (iframeAttributes) {
        this.iframeAttributes = iframeAttributes;
        this.iframeAttributes.src = url;
    }
}

AGEWnd.prototype.onClosing = function () {
    return true;
};

AGEWnd.prototype.close = function (force) {
    if (!force) {
        var bContinue = this.onClosing();
        if (!bContinue) return false;
    }

    var docContext = this.getDocContext();
    if (typeof this.docContext != "undefined") {
        docContext.body.removeChild(this.mask);
        docContext.body.removeChild(this.element);

        return true;
    }

    // un-listen event
    this.removeEvent(this.iframe.document ? this.iframe.document : this.iframe.contentDocument, "keydown", self.keydown);
    this.removeEvent(window, "resize", this.calculateViewport());

    this.url = null;
    this.loaded = false;
    this.element.style.display = 'none';
    this.viewport = null;

    if (typeof this.blankURL != "undefined") {
        if (this.iframe.src != this.blankURL) this.iframe.src = this.blankURL;

        // Reset event
        this.onClosing = function () { return true; };
    }
    this.createdElement = false;
    docContext.body.removeChild(this.mask);
    docContext.body.removeChild(this.element);
    this.createdElement = false;
}

AGEWnd.prototype.setRect = function (left, top, width, height) {
    if (!this.createdElement) return false;

    var docContext = this.getDocContext();

    var scrOfX = this.getScrollLeft(docContext.body || docContext.documentElement);
    var scrOfY = 0;
    if (this.is_ie && typeof (window.pageYOffset) == 'number') {
        scrOfY = window.pageYOffset;
    } else if (docContext.body && (docContext.body.scrollLeft || docContext.body.scrollTop)) {
        scrOfY = docContext.body.scrollTop;
    } else if (docContext.documentElement && (docContext.documentElement.scrollLeft || docContext.documentElement.scrollTop)) {
        scrOfY = docContext.documentElement.scrollTop;
    }

    //scrOfY += 15;

    this.scrOfX = scrOfX;
    this.scrOfY = scrOfY;

    if (left != null && top != null && !this.loaded) {
        left += scrOfX;
        top += scrOfY;
        this.left = left;
        this.top = top;
    }

    this.width = (width < 39) ? 39 : width;

    var maxHeight = docContext.documentElement.clientHeight - 35 - top;
    var minHeight = 39;

    var calculatedHeight = height > maxHeight ? maxHeight : height;
    calculatedHeight = calculatedHeight < minHeight ? minHeight : calculatedHeight;
    this.height = calculatedHeight;

    if (this.height < minHeight) {
        if (minHeight < height) {
            this.height = minHeight;
        } else {
            this.height = height;
        }
    }

    this.calculateViewport();

    if (this.left > this.maxLeft) {
        this.left = this.maxLeft;
    }

    if (left != null && top != null) {
        this.mask.style.left = this.left + "px";
        this.mask.style.top = this.top + "px";
    }

    this.mask.style.width = this.mask.firstChild.style.width = this.width + "px";
    this.mask.style.height = this.mask.firstChild.style.height = this.height + "px";

    this.title.style.width = this.maskTitle.style.width = (this.width - 39) + 'px';
    this.content.style.width = (this.width) + "px";
    this.content.style.height = (this.height - 41) + "px";

    this.element.style.left = this.left + "px";
    this.element.style.top = this.top + "px";
    this.element.style.width = this.width + "px";
    this.element.style.height = this.height + "px";
    if (navigator.userAgent.match(/Android|BlackBerry|iPhone|iPad|iPod|Opera Mini|IEMobile/i)) {
        this.element.style.height = "auto";
    }

    if (width <= 5 || height <= 5)
        this.element.style.visibility = "hidden";
    else
        this.element.style.visibility = "visible";

    return true;
}
AGEWnd.prototype.getScrollLeft = function (element) {
    return element.pageXOffset || element.scrollX || element.scrollLeft || 0;
};

AGEWnd.prototype.calculateViewport = function () {
    var docContext = this.getDocContext();

    this.viewport = [];
    if (!this.is_ie) {
        // in standards compliant mode (i.e. with a valid doctype as the first line in the document)
        if (typeof docContext.documentElement != 'undefined'
         && typeof docContext.documentElement.scrollWidth !=
         'undefined' && docContext.documentElement.scrollWidth != 0) {
            this.viewport.width = docContext.documentElement.scrollWidth;
            this.viewport.height = docContext.documentElement.scrollHeight;
        }

        this.viewport.width = Math.max(this.viewport.width, (Math.max(docContext.body.scrollWidth, docContext.body.clientWidth)));
        this.viewport.height = Math.max(this.viewport.height, (Math.max(docContext.body.scrollHeight, docContext.body.clientHeight)));
    }
    else {
        // in standards compliant mode (i.e. with a valid doctype as the first line in the document)
        if (typeof docContext.documentElement != 'undefined'
         && typeof docContext.documentElement.clientWidth !=
         'undefined' && docContext.documentElement.clientWidth != 0) {
            this.viewport.width = docContext.documentElement.clientWidth;
            this.viewport.height = docContext.documentElement.clientHeight;
        }
            // older versions of IE
        else {
            this.viewport.width = docContext.getElementsByTagName('body')[0].clientWidth;
            this.viewport.height = docContext.getElementsByTagName('body')[0].clientHeight;
        }

        this.viewport.width = Math.max(docContext.body.scrollWidth, this.viewport.width);
        this.viewport.height = Math.max(docContext.body.scrollHeight, this.viewport.height);
    }

    this.maxLeft = this.viewport.width - this.width - 30;
    this.maxTop = this.viewport.height - this.height - 6;
}

AGEWnd.prototype.addEvent = function addEvent(el, evname, func) {
    if (el.attachEvent) { // IE
        el.attachEvent("on" + evname, func);
    } else if (el.addEventListener) { // Gecko / W3C
        el.addEventListener(evname, func, true);
    } else { // Opera (or old browsers)
        el["on" + evname] = func;
    }
}

AGEWnd.prototype.removeEvent = function removeEvent(el, evname, func) {
    if (el.detachEvent) { // IE
        el.detachEvent("on" + evname, func);
    } else if (el.removeEventListener) { // Gecko / W3C
        el.removeEventListener(evname, func, true);
    } else { // Opera (or old browsers)
        el["on" + evname] = null;
    }
}

AGEWnd.prototype.createElement = function createElement(type, parent) {
    var docContext = this.getDocContext();
    // TODO: ???
    var el = null;
    if (docContext.createElementNS) {
        // use the XHTML namespace; IE won't normally get here unless
        // _they_ "fix" the DOM2 implementation.
        el = docContext.createElementNS("http://www.w3.org/1999/xhtml", type);
    } else {
        el = docContext.createElement(type);
    }
    if (typeof parent != "undefined") {
        parent.appendChild(el);
    }
    return el;
}

AGEWnd.prototype.createIframe = function () {
    var docContext = this.getDocContext();
    var iframe = docContext.createElement("iframe"); iframe.setAttribute("border", 0); iframe.className = "AGEWndNoPadding"; iframe.id = "AGEWndIframe"; iframe.name = "AGEWndIframe";
    iframe.setAttribute("frameborder", 0); iframe.style.backgroundColor = "#ffffff";
    iframe.width = "100%"; iframe.height = "100%";
    return iframe;
}

AGEWnd.prototype.keydown = function (ev) {
    var wnd = this;
    if (!wnd.loaded) return true;

    if (wnd.dragging) {
        wnd.mask.style.display = "none";
        wnd.dragging = false;
        return false;
    }

    if (!ev) {
        ev = window.event;
    }

    if (ev.which == 27 || ev.keyCode == 27) wnd.close();

    return true;
}

AGEWnd.prototype.dragStart = function (ev) {
    var wnd = this;
    var docContext = this.getDocContext();

    var leftMouse = 1;
    if (wnd.is_ie) {
        leftMouse = window.event.button;
    }
    else {
        leftMouse = ev.which;
    }

    if (leftMouse != 1) return false;

    wnd.mask.style.display = "block";
    wnd.mask.style.left = wnd.element.style.left;
    wnd.mask.style.top = wnd.element.style.top;

    if (wnd.dragging) {
        return false;
    }
    wnd.dragging = true;

    var posX;
    var posY;
    if (wnd.is_ie) {
        posY = window.event.clientY + docContext.body.scrollTop;
        posX = window.event.clientX + docContext.body.scrollLeft;
    } else {
        posX = ev.pageX;
        posY = ev.pageY;
    }

    var st = wnd.element.style;
    wnd.xOffs = posX - parseInt(st.left);
    wnd.yOffs = posY - parseInt(st.top);

    this.addEvent(docContext, "mousemove", function (event) {
        wnd.calDragIt.call(wnd, event);
    });
    this.addEvent(docContext, "mouseover", function (event) {
        wnd.stopEvent.call(wnd, event);
    })
    this.addEvent(docContext, "mouseup", function (event) {
        wnd.calDragEnd.call(wnd, event);
    });

    wnd.stopEvent(ev);
};

AGEWnd.prototype.calDragIt = function (ev) {
    var wnd = this;
    if (!wnd.dragging) {
        return false;
    }
    var posX;
    var posY;
    if (wnd.is_ie) {
        posY = window.event.clientY + document.body.scrollTop;
        posX = window.event.clientX + document.body.scrollLeft;
    } else {
        posX = ev.pageX;
        posY = ev.pageY;
    }

    var left = (posX - wnd.xOffs);
    var top = (posY - wnd.yOffs);

    // prevent horizontal move verflow width
    if (wnd.width < wnd.viewport.width - 6) {
        if (left <= 1) left = 1; if (left >= wnd.maxLeft) left = wnd.maxLeft;
        wnd.mask.style.left = left + "px";
    }

    // prevent vertical move overflow height
    if (wnd.height < wnd.viewport.height) {
        if (top <= 1) top = 1; if (top >= wnd.maxTop) top = wnd.maxTop;
        wnd.mask.style.top = top + "px";
    }
};

AGEWnd.prototype.calDragEnd = function () {
    var wnd = this;
    wnd.mask.style.display = "none";

    if (!wnd.dragging) {
        return false;
    }

    wnd.dragging = false;

    wnd.left = AGEWnd_preLeft = parseInt(wnd.mask.style.left);
    wnd.top = AGEWnd_preTop = parseInt(wnd.mask.style.top);

    wnd.element.style.left = wnd.mask.style.left;
    wnd.element.style.top = wnd.mask.style.top;

    this.removeEvent(document, "mousemove", wnd.calDragIt);
    this.removeEvent(document, "mouseover", wnd.stopEvent);
    this.removeEvent(document, "mouseup", wnd.calDragEnd);
};

AGEWnd.prototype.stopEvent = function (ev) {
    var wnd = this;
    if (wnd.is_ie) {
        window.event.cancelBubble = true;
        window.event.returnValue = false;
    } else {
        ev.preventDefault();
        ev.stopPropagation();
    }
    return false;
};

AGEWnd.prototype.getDocContext = function () {
    var docContext = document;
    if (typeof this.docContext != "undefined") {
        docContext = this.docContext;
    }

    return docContext;
}

AGEWnd.prototype.createInstance = function () {
    var curDocContext = this.getDocContext();
    var result = new AGEWnd(curDocContext);
    return result;
}

// static property
AGEWnd_autoKeepPosition = false;

AGEWnd_preLeft = 0;
AGEWnd_preTop = 0;
AGEWnd_preWidth = 0;
AGEWnd_preHeight = 0;

// init object
var ageWnd = new AGEWnd();
//ageWnd.addEvent(window, 'load', function () {
//    ageWnd = new AGEWnd();
//});

