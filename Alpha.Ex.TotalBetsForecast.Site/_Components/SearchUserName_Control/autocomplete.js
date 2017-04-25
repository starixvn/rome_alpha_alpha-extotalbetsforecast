// http://www.vonloesch.de/node/18
var suggestions = null;

if(typeof parent.cachePage == 'undefined')
{
    parent.cachePage = new Array();
    parent.cacheData = new Array();
    
}

if(findItemInArr(location.href, parent.cachePage) == -1)
{
    parent.cachePage.push(location.href);
    parent.cacheData.push([new Array(), new Array()]);
}


var maxCache = 200;

var cachePageIndex = findItemInArr(location.href, parent.cachePage);

var cacheQuery = parent.cacheData[cachePageIndex][0];
var cacheSuggestion = parent.cacheData[cachePageIndex][1];

var outp;
var oldins;
var posi = -1;
var words = new Array();
var input;
var key;
var tId = 0;
var url = null;
var txtObj = null;
function setVisible(visi){
	var x = $("shadow");
	x.style.position = 'absolute';
	x.style.top =  (findPosY(txtObj)+3)+"px";
	x.style.left = (findPosX(txtObj)+2)+"px";
	x.style.visibility = visi;
}

function initAutoComplete(id, queryUrl){
    txtObj = $(id);
    url = queryUrl;
    txtObj.setAttribute('autocomplete', 'off');
    age.addEvent(txtObj, 'blur', function(){setTimeout('setVisible("hidden")', 500);});
	outp = $("output");
	setVisible("hidden");

	age.addEvent(txtObj, 'keydown', keygetter); //needed for Opera...
	age.addEvent(txtObj, 'keyup', keyHandler);
	
	if(cacheQuery.length == 0)
	{
	    cacheQuery.push('');
	    cacheSuggestion.push([]);
	}	
}

function findPosX(obj)
{
	var curleft = 0;
	if (obj.offsetParent){
		while (obj.offsetParent){
			curleft += obj.offsetLeft;
			obj = obj.offsetParent;
		}
	}
	else if (obj.x)
		curleft += obj.x;
	return curleft;
}

function findPosY(obj)
{
	var curtop = 0;
	if (obj.offsetParent){
		curtop += obj.offsetHeight;
		while (obj.offsetParent){
			curtop += obj.offsetTop;
			obj = obj.offsetParent;
		}
	}
	else if (obj.y){
		curtop += obj.y;
		curtop += obj.height;
	}
	return curtop;
}

var oldValue = ''; var oldIdx = -1;
function getSuggestions()
{
    if(txtObj.value.length < 2) 
    {
        suggestions = [];
        lookAt('');
        return;
    }
    
    if (cacheSuggestion[oldIdx] && oldValue != '' && txtObj.value.indexOf(oldValue) != -1 && oldIdx != -1 && cacheSuggestion[oldIdx].length == 0)
    {
        return;
    }
    
    oldValue = txtObj.value;
    oldIdx = findItemInArr(oldValue.toLowerCase(), cacheQuery);
    
    if(oldIdx != -1 && cacheQuery.length < maxCache)
    {
        suggestions = cacheSuggestion[oldIdx];
        lookAt();
    }
    else
    {
        if(cacheQuery.length >= maxCache) {cacheQuery.slice(1, maxCache-1); cacheSuggestion.slice(1, maxCache-1)};
        jx.request(url,lookAt,'get','query=' + oldValue);
    }
}

function lookAt(data){

    if(typeof data == 'object')
    {
        eval('suggestions = ' + data.responseText + '.suggestions');
    }
    
    if(findItemInArr(oldValue.toLowerCase(), cacheQuery) == -1) {
        try {
            cacheQuery.push(oldValue.toLowerCase());
            cacheSuggestion.push(suggestions);

            oldIdx = cacheQuery.length - 1;
        } catch (exception) { }
    }
    
	var ins = txtObj.value;
	if (oldins == ins) return;

	else if (ins.length > 0){
		words = getWord(ins);
		if (words.length > 0){
			clearOutput();
			for (var i = 0; i < words.length; ++i) addWord(words[i]);
			setVisible("visible");
			input = txtObj.value;
		}
		else{
			setVisible("hidden");
			posi = -1;
		}
	}
	else{
		setVisible("hidden");
		posi = -1;
	}
	oldins = ins;
}

function addWord(word){
	var sp = document.createElement("div");	
	sp.className = 'opOut';
	sp.innerHTML = word;
	sp.onmouseover = mouseHandler;
	sp.onmouseout = mouseHandlerOut;
	sp.onclick = mouseClick;
	outp.appendChild(sp);
}

function clearOutput(){
	while (outp.hasChildNodes()){
		noten=outp.firstChild;
		outp.removeChild(noten);
	}
	posi = -1;
}

function getWord(beginning){
    var words = new Array();
    var content;
    if (suggestions) {
        for (var i = 0; i < suggestions.length; ++i)
            words[words.length] = suggestions[i][0] + "<br/><span class='opSpan'>" + suggestions[i][1] + ' ' + suggestions[i][2] + '</span>';
    }
    return words;	
}

function keygetter(event){
	if (!event && window.event) event = window.event;
	if (event) key = event.keyCode;
	else key = event.which;
}
	
function keyHandler(event){

	if ($("shadow").style.visibility == "visible"){
	    if (key == 40){ //Key down
		    if (words.length > 0 && posi < words.length-1){
		        if (posi >= 0) {
		            outp.childNodes[posi].className = 'opOut';
		            outp.childNodes[posi].lastChild.className = 'opSpan'
		        }
		        else input = txtObj.value;
		        outp.childNodes[++posi].className = 'opOver';
		        outp.childNodes[posi].lastChild.className = 'opSpanOver';
			    txtObj.value = outp.childNodes[posi].firstChild.nodeValue;
		    }
	    }
	    else if (key == 38){ //Key up
		    if (words.length > 0 && posi >= 0){
			    if (posi >=1){
			        outp.childNodes[posi].className = 'opOut';
			        outp.childNodes[posi].lastChild.className = 'opSpan'
			        outp.childNodes[--posi].className = 'opOver';			        
				    outp.childNodes[posi].lastChild.className = 'opSpanOver'
				    txtObj.value = outp.childNodes[posi].firstChild.nodeValue;
			    }
			    else {			        
			        outp.childNodes[posi].className = 'opOut';
			        outp.childNodes[posi].lastChild.className = 'opSpan'
				    txtObj.value = input;
				    txtObj.focus();
				    posi--;
			    }
		    }
	    }
	    else if (key == 27){ // Esc
		    txtObj.value = input;
		    setVisible("hidden");
		    posi = -1;
		    oldins = input;
	    }
	    else if (key == 8){ // Backspace
		    posi = -1;
		    oldins=-1;
	    }
	}
	
	if (key != 40 && key != 38 && key != 27)
	{
        if(tId > 0) clearTimeout(tId);
        tId = setTimeout(getSuggestions, 200);
    }
}

var mouseHandler = function() {
    this.className = 'opOver';
    this.lastChild.className = 'opSpanOver'
}

var mouseHandlerOut=function(){
    this.className = 'opOut';
    this.lastChild.className = 'opSpan'
}

var mouseClick=function(){
	txtObj.value = this.firstChild.nodeValue;
	setVisible("hidden");
	posi = -1;
	oldins = this.firstChild.nodeValue;
	txtObj.focus();
}

function findItemInArr(item, array) {
    if (typeof array != 'object') return -1;
    var c = array.length;
    if (typeof c == 'undefined') return -1;
    for (var i = c; i >= 0; i--) {
        if (array[i] == item) return i;
    }
    return -1;
}