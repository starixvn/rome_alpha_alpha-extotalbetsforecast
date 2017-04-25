function loadUrl() {
    try {
        if (!window.parent.AGEWnd._W.loaded && window.parent.AGEWnd._W.url != null) {
            document.getElementById('p1').style.display = 'none';
            window.parent.AGEWnd._W.iframe.src = window.parent.AGEWnd._W.url;
            window.parent.AGEWnd._W.loaded = true;
        }
        else if (window.parent.AGEWnd._W.url == null || window.parent.AGEWnd._W.loaded) {
            window.parent.AGEWnd._W.close();
        }
    }
    catch (e) {  }
}

window.onload = loadUrl;