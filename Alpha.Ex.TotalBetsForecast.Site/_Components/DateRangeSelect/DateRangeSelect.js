function DisableButton() {
    var dSubmit = $('dSubmit');
    var dToday = $('dToday');
    var dYesterday = $('dYesterday');
    var dSelLeague = $('dSelLeague');
    var btResultType = $('btResultType');

    dSubmit.disabled = true;
    if (!HasClassName(dSubmit, 'hidden')) dSubmit.className = 'btn2';

    if (dToday) {
        dToday.disabled = true;
        if (!HasClassName(dToday, 'hidden')) dToday.className = 'btn2';
    }

    if (dYesterday) {
        dYesterday.disabled = true;
        if (!HasClassName(dYesterday, 'hidden')) dYesterday.className = 'btn2';
    }

    if (dSelLeague) {
        dSelLeague.disabled = true;
        if (!HasClassName(dSelLeague, 'hidden')) dSelLeague.className = 'btn2';
    }

    if (btResultType) {
        btResultType.disabled = true;
        btResultType.className = 'btn2';
    }
    var loading = $("loading");
    if (loading) loading.className = "loading";
}

function EnableButton() {
    var dSubmit = $('dSubmit');
    var dToday = $('dToday');
    var dYesterday = $('dYesterday');
    var dSelLeague = $('dSelLeague');
    var btResultType = $('btResultType');

    dSubmit.disabled = false;
    if (!HasClassName(dSubmit, 'hidden')) dSubmit.className = 'btn';

    if (dToday) {
        dToday.disabled = false;
        if (!HasClassName(dToday, 'hidden')) dToday.className = 'btn';
    }

    if (dYesterday) {
        dYesterday.disabled = false;
        if (!HasClassName(dYesterday, 'hidden')) dYesterday.className = 'btn';
    }

    if (dSelLeague) {
        dSelLeague.disabled = false;
        if (!HasClassName(dSelLeague, 'hidden')) dSelLeague.className = 'btn';
    }

    if (btResultType) {
        btResultType.disabled = false;
        btResultType.className = 'btn';
    }
    var loading = $("loading");
    if (loading) loading.className = "";
}

function ChangeMonth() {
    var value = $("ddlMonth").value;
    var dateFromObj = ClearOptions("ddlDateFrom");
    var dateToObj = ClearOptions("ddlDateTo");
    var selectedMonth = new Date(value);
    var year = selectedMonth.getFullYear();
    var month = selectedMonth.getMonth();
    month = month + 1;
    if (month > 12) {
        year = year + 1;
        month = month - 12;
    }
    var date = new Date(year, month, 0);
    var currentDate = new Date(max_server_date);
    if (date > currentDate) date = currentDate;

    var totalDate = date.getDate();
    for (var i = 0; i < totalDate; i++) {
        AddOption(dateFromObj, (i + 1).toString(), (i + 1).toString());
        AddOption(dateToObj, (i + 1).toString(), (i + 1).toString());
    }

    dateFromObj.selectedIndex = 0;
    dateToObj.selectedIndex = totalDate - 1;

    ChangeDateFrom();
    ChangeDateTo();
}
function ChangeDateFrom() {
    var value = $("ddlDateFrom").value;
    var dateFromInput = document.getElementById('fdate');
    var selectedMonth = new Date($('lblSelectedMonth').title);

    selectedMonth.setDate(value);
    var month = selectedMonth.getMonth() + 1;
    var year = selectedMonth.getFullYear();
    var day = selectedMonth.getDate();
    dateFromInput.value = month + "/" + day + "/" + year;
}

function ChangeDateTo() {
    var value = $("ddlDateTo").value;
    var dateToInput = document.getElementById('tdate');
    var selectedMonth = new Date($('lblSelectedMonth').title);

    selectedMonth.setDate(value);
    var month = selectedMonth.getMonth() + 1;
    var year = selectedMonth.getFullYear();
    var day = selectedMonth.getDate();
    dateToInput.value = month + "/" + day + "/" + year;
}

function ClearOptions(id) {
    var selectObj = document.getElementById(id);
    var selectParentNode = selectObj.parentNode;
    var newSelectObj = selectObj.cloneNode(false); // Make a shallow copy
    selectParentNode.replaceChild(newSelectObj, selectObj);
    return newSelectObj;
}
function AddOption(selectBox, text, value) {
    var option = document.createElement("OPTION");
    option.text = text;
    option.value = value;
    selectBox.options.add(option);
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
    if (url == null) return null;

    var separator = url.contains("?" + name + "=") ? "?" : "&";

    var parameter = separator + name + "=";

    var i1 = url.indexOf(parameter);
    if (i1 == -1) return null;

    var tmp = url.substr(i1);
    var i2 = tmp.indexOf("&", 1);

    var value = i2 >= 0 ? tmp.substr(parameter.length, i2 - parameter.length) : tmp.substr(parameter.length);

    return value;
}

function SearchByDate() {
    var form = $("frmmain");
    if (form) {
        form.method = "get";
        form.submit();
    }
}
function SearchOneDay(value) {
    var form = $("frmmain");

    if (form) {
        var fdate = document.getElementById('fdate');
        var tdate = document.getElementById('tdate');
        if (fdate) fdate.value = value;
        if (tdate) tdate.value = value;
        form.method = "get";
        form.submit();
    }
}

//Add control monthly and handle the events on here
//Hide form Nick Name
function HideForm(seconds) {
    timeoutID = setTimeout("if($('monthPickerDropDown')) $('monthPickerDropDown').style.display ='none'", seconds); //Set delay
}

function HideFormV2(seconds, id) {
    timeoutID = setTimeout("if($('" + id + "')) $('" + id + "').style.display ='none'", seconds); //Set delay
}

function StopTimer() {
    if (typeof timeoutID != 'undefined') window.clearTimeout(timeoutID);
}
//Display or Hide div list month when click on here
function ShowMonth() {
    if ($('monthPickerDropDown').style.display == "none" || $('monthPickerDropDown').style.display == "") {
        $('monthPickerDropDown').style.display = "block";
        $('monthPickerDropDown').onmouseover = function () { StopTimer(); }
        $('monthPickerDropDown').onmouseout = function () { HideForm(1000); }
        $('btnPrevMonth').onmouseout = function () { HideForm(1000); }
        $('btnNextMonth').onmouseout = function () { HideForm(1000); }
    } else {
        $('monthPickerDropDown').style.display = "none";
    }
    StopTimer();
}

function ShowMonthV2(id, btnPre, btnNext) {
    if ($(id).style.display == "none" || $(id).style.display == "") {
        if (id.indexOf('2') > 0) {
            $('monthPickerDropDown').style.display = "none";
            $(id).style.display = "block";
        }
        else {
            $('monthPickerDropDown2').style.display = "none";
            $(id).style.display = "block";
        }

        $(id).onmouseover = function () { StopTimer(); }
        $(id).onmouseout = function () { HideFormV2(1000, id); }
        $(btnPre).onmouseout = function () { HideFormV2(1000, id); }
        $(btnNext).onmouseout = function () { HideFormV2(1000, id); }
    } else {
        $('monthPickerDropDown2').style.display = "none";
        $('monthPickerDropDown').style.display = "none";
    }
    StopTimer();
}
//Event Expand list month when click  on year.
function SelectedYear(year, calendarPos) {
    var listYear = $('listYear').value.split('-');
    var part_num = 0;
    while (part_num < listYear.length) {
        if (listYear[part_num] == year) {
            ExpandMonth(listYear[part_num], null, calendarPos);
        }
        else {
            ExpandMonth(listYear[part_num], "none", calendarPos);
        }
        part_num += 1;
    }
}

//Hide or Show month when click on year
function ExpandMonth(year, displayNone, calendarPos) {
    var tmp = calendarPos == null ? '' : calendarPos;
    var LI = "LI" + tmp + "_";
    var ID = "ID" + tmp + "_";

    if (displayNone != null) {
        $(ID + year).className = "";
        for (i = 1; i < 13; i++) {
            if ($(LI + year + "_" + i) != null) {
                $(LI + year + "_" + i).style.display = "none";
            }
        }
    }
    else {
        if ($(ID + year).className == "")
            $(ID + year).className = "selected";
        else
            $(ID + year).className = "";

        for (i = 1; i < 13; i++) {
            if ($(LI + year + "_" + i) != null && $(LI + year + "_" + i).style.display == "none") {
                $(LI + year + "_" + i).style.display = "block";
            }
            else if ($(LI + year + "_" + i) != null) {
                $(LI + year + "_" + i).style.display = "none";
            }
        }
    }
}

function SetSelectedMonth(value, isClick) {
    $('lblSelectedMonth').title = value;
    var dateFromObj = ClearOptions("ddlDateFrom");
    var dateToObj = ClearOptions("ddlDateTo");
    var selectedMonth = new Date(value);
    var year = selectedMonth.getFullYear();
    var month = selectedMonth.getMonth();
    month = month + 1;
    if (month > 12) {
        year = year + 1;
        month = month - 12;
    }
    var date = new Date(year, month, 0);
    var currentDate = new Date(max_server_date);
    if (date > currentDate) date = currentDate;

    var totalDate = date.getDate();
    for (var i = 0; i < totalDate; i++) {
        AddOption(dateFromObj, (i + 1).toString(), (i + 1).toString());
        AddOption(dateToObj, (i + 1).toString(), (i + 1).toString());
    }

    dateFromObj.selectedIndex = 0;
    dateToObj.selectedIndex = totalDate - 1;

    ChangeDateFrom();
    ChangeDateTo();
    HideDivMonthAndSelectedValue(value, $(value).title, year, isClick);
    SetClassNameSelected(month, year);
    CheckBoundary(value);
}

function SetSelectedMonthV2(value, isClick, lblId, dtId) {
    if (value.split('/')[0] == "0") {
        var year = parseInt(value.split('/')[2]) - 1;
        value = value.replace('0/', '12/').replace(value.split('/')[2], year);
    }

    if (value.split('/')[0] == "13") {
        var year = parseInt(value.split('/')[2]) + 1;
        value = value.replace('13/', '1/').replace(value.split('/')[2], year);
    }

    $(lblId).title = value;

    var selectedMonth = new Date(value);
    var year = selectedMonth.getFullYear();
    var month = selectedMonth.getMonth();
    var tmp = lblId.indexOf('2') > 0 ? '_LI2' : ''

    month = month + 1;

    if (month > 12) {
        year = year + 1;
        month = month - 12;
    }
    var date = new Date(year, month, 0);
    var currentDate = new Date(max_server_date);
    if (date > currentDate) date = currentDate;

    var now = new Date(_page.ServerTime);

    if (date.getFullYear() == now.getFullYear() && date.getMonth() == now.getMonth() && lblId.indexOf('2') > 0) {
        value = (now.getMonth() + 1) + '/' + now.getDate() + '/' + now.getFullYear();
    }

    if (lblId.indexOf('2') > 0) {
        $('tdate').value = value;
    }
    else {
        $('fdate').value = value;
    }

    if ($(value + tmp)) {
        HideDivMonthAndSelectedValueV2(value, $(value + tmp).title, year, isClick, lblId, dtId);
    }

    SetClassNameSelected(month, year, lblId.indexOf('2') > 0 ? 2 : null);

    var btnPre = null;
    var btnNext = null;

    if (lblId.indexOf('2') > 0) {
        btnNext = 'btnNextMonth2';
        btnPre = 'btnPrevMonth2';
    }
    else {
        btnNext = 'btnNextMonth';
        btnPre = 'btnPrevMonth';
    }

    CheckBoundaryV2(value, btnPre, btnNext);
}

function HideDivMonthAndSelectedValue(id, month, year, isClick) {
    $('lblSelectedMonth').innerHTML = month + " - " + year;
    if (isClick) $('monthPickerDropDown').style.display = "none";
    $(id).className = "selected";
}

function HideDivMonthAndSelectedValueV2(id, month, year, isClick, lblId, dtId) {
    $(lblId).innerHTML = month + " - " + year;

    if (isClick) {
        $('monthPickerDropDown2').style.display = "none";
        $('monthPickerDropDown').style.display = "none";
    }

    var tmp = lblId.indexOf('2') > 0 ? '_LI2' : '';
    $(id + tmp).className = "selected";
}

function SetClassNameSelected(month, year, calendarPos) {
    var listYear = $('listYear').value.split('-');
    var part_num = 0;
    var tmp = calendarPos == null ? '' : calendarPos;
    var subID = calendarPos == null ? '' : "_LI" + calendarPos;
    var LI = "LI" + tmp + "_";
    var IDS = "ID" + tmp + "_";
    var now = new Date(_page.ServerTime);

    while (part_num < listYear.length) {
        //Expland icon year when have selected month
        if (listYear[part_num] == year) {
            if ($(IDS + year).className == "") {
                $(IDS + year).className = "selected";
            }
            for (var i = 1; i < 13; i++) {
                if ($(LI + year + "_" + i) != null) {
                    var id = i + '/1/' + year + subID;

                    if (tmp != '') {
                        var day = DayOfMonth(year, i);
                        if (year == now.getFullYear() && i == now.getMonth() + 1) {
                            day = now.getDate();
                        }

                        id = i + '/' + day + '/' + year + subID;
                    }

                    if (month == i) {
                        if ($(id)) {
                            $(id).className = "selected";
                        }
                    }
                    else {
                        if ($(id)) {
                            $(id).className = "";
                        }
                    }

                    if ($(LI + year + "_" + i).style.display == "none")
                        $(LI + year + "_" + i).style.display = "";
                }
            }
        }
        else { //Hide icon year when haven't selected month
            $(IDS + listYear[part_num]).className = "";
            for (var i = 1; i < 13; i++) {
                if ($(LI + listYear[part_num] + "_" + i) != null) {
                    var id = i + '/1/' + listYear[part_num] + subID;

                    if (tmp != '') {
                        var now = new Date(_page.ServerTime);
                        var day = DayOfMonth(year, i);

                        if (now.getFullYear() == listYear[part_num] && i == now.getMonth() + 1) {
                            day = now.getDate();
                        }

                        id = i + '/' + day + '/' + listYear[part_num] + subID;
                    }

                    if ($(id)) {
                        $(id).className = "";
                    }

                    $(LI + listYear[part_num] + "_" + i).style.display = "none";
                }
            }
        }
        part_num += 1;
    }
}

function CheckBoundary(dateToGo) {
    var maxDate = Date.parse($('maxDate').value);
    var minDate = Date.parse($('minDate').value);

    dateToGo = Date.parse(dateToGo);

    var next = $('btnNextMonth');
    var prev = $('btnPrevMonth');
    if (dateToGo >= maxDate) {
        AddClassName(next, "disabled");
    } else {
        RemoveClassName(next, "disabled");
    }

    if (dateToGo <= minDate) {
        AddClassName(prev, "disabled");
    } else {
        RemoveClassName(prev, "disabled");
    }
};

function CheckBoundaryV2(dateToGo, btnPreId, btnNextId) {
    var maxDate = Date.parse($('maxDateTo').value);
    var minDate = Date.parse($('minDateTo').value);

    if (dateToGo.indexOf('/1/') > 0) {
        maxDate = Date.parse($('maxDate').value);
        minDate = Date.parse($('minDate').value);
    }

    dateToGo = Date.parse(dateToGo);

    var next = $(btnNextId);
    var prev = $(btnPreId);
    if (dateToGo >= maxDate) {
        AddClassName(next, "disabled");
    } else {
        RemoveClassName(next, "disabled");
    }

    if (dateToGo <= minDate) {
        AddClassName(prev, "disabled");
    } else {
        RemoveClassName(prev, "disabled");
    }
};

function PrevMonth() {
    StopTimer();
    if (HasClassName($('btnPrevMonth'), 'disabled')) return;

    var currentDate = new Date($('lblSelectedMonth').title);
    currentDate.setMonth(currentDate.getMonth() - 1);
    currentDate = (currentDate.getMonth() + 1) + "/" + currentDate.getDate() + "/" + currentDate.getFullYear();
    SetSelectedMonth(currentDate, false);
}

function NextMonth() {
    StopTimer();
    if (HasClassName($('btnNextMonth'), 'disabled')) return;

    var currentDate = new Date($('lblSelectedMonth').title);
    currentDate.setMonth(currentDate.getMonth() + 1);
    currentDate = (currentDate.getMonth() + 1) + "/" + currentDate.getDate() + "/" + currentDate.getFullYear();
    SetSelectedMonth(currentDate, false);
}

function PrevMonthV2(id, lblId, dtId) {
    StopTimer();
    if (HasClassName($(id), 'disabled')) return;

    var currentDate = new Date($(lblId).title);

    var day = 1;

    if (lblId.indexOf('2') > 0) {
        day = DayOfMonth(currentDate.getFullYear(), currentDate.getMonth());
        currentDate = currentDate.getMonth() + "/" + day + "/" + currentDate.getFullYear();
    }
    else {
        currentDate.setMonth(currentDate.getMonth() - 1);
        currentDate = (currentDate.getMonth() + 1) + "/" + day + "/" + currentDate.getFullYear();
    }
    SetSelectedMonthV2(currentDate, false, lblId, dtId);
}

function NextMonthV2(id, lblId, dtId) {
    StopTimer();
    if (HasClassName($(id), 'disabled')) return;

    var currentDate = new Date($(lblId).title);

    var day = 1;

    if (lblId.indexOf('2') > 0) {
        day = DayOfMonth(currentDate.getFullYear(), currentDate.getMonth() + 2);

        var now = new Date(_page.ServerTime);

        if (currentDate.getFullYear() == now.getFullYear() && (currentDate.getMonth() + 1) == now.getMonth()) {
            day = now.getDate();
        }

        currentDate = (currentDate.getMonth() + 2) + "/" + day + "/" + currentDate.getFullYear();
    }
    else {
        currentDate.setMonth(currentDate.getMonth() + 1);
        currentDate = (currentDate.getMonth() + 1) + "/" + day + "/" + currentDate.getFullYear();
    }

    SetSelectedMonthV2(currentDate, false, lblId, dtId);
}

function DayOfMonth(year, month) {
    var day30 = new Array(4, 6, 9, 11);

    if (month == 2) {
        return ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0) ? 29 : 28;
    }
    else {
        return day30.indexOf(month) >= 0 ? 30 : 31;
    }
}