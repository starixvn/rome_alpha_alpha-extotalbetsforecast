var noClickSelect = true;

function DisableButton() {
    var dSubmit = $('dSubmit');
    var dSelMatch = $('dSelMatch');

    dSubmit.disabled = true;
    if (!HasClassName(dSubmit, 'hidden')) dSubmit.className = 'btn2';

    if (dSelMatch) {
        dSelMatch.disabled = true;
        if (!HasClassName(dSelMatch, 'hidden')) dSelMatch.className = 'btn2';
    }

    var loading = $("loading");
    if (loading) loading.className = "loading";
}

function EnableButton() {
    var dSubmit = $('dSubmit');
    var dSelMatch = $('dSelMatch');

    dSubmit.disabled = false;
    if (!HasClassName(dSubmit, 'hidden')) dSubmit.className = 'btn';

    if (dSelMatch) {
        dSelMatch.disabled = false;
        if (!HasClassName(dSelMatch, 'hidden')) dSelMatch.className = 'btn';
    }

    var loading = $("loading");
    if (loading) loading.className = "";
}

function SelectMatch() {    
    DisableButton();
    if ($('BoxMatchWL').style.display == '') {
        hiddenBoxMatch();
        EnableButton();
        return;
    }
    noClickSelect = false;
    var LeagueID = $('ddlLeague') ? $('ddlLeague').value : '0';
    var NewFilterMatch = LeagueID + $('isChanged').value;
    if (_page.FilterMatch != null && _page.FilterMatch != NewFilterMatch) {
        _page.FilterMatch = NewFilterMatch;
        http = jx.request(
        '../_Components/MatchByLeague/MatchByLeague.aspx', onComplete, 'POST'
           , 'LeagueID=' + LeagueID
        );
    }
    else {
        EnableButton();
        $('dSelMatch').value = _page.hideMatch;
        $('BoxMatchWL').style.display = '';
    }

    function onComplete(rs) {
        if (typeof rs == 'object') {
            $('dSelMatch').value = _page.hideMatch;
            $('tblMsgMatch').style.display = "none";
            $('BoxMatchWL').style.display = '';
            $('ContentMatchWL').innerHTML = rs.responseText;
            setMatchIds();
            GetMatchSportFull();
            EnableButton();
        }
    }
}

function isAllMatch(curMatch) {
    if ($(curMatch).checked) {
        var isCheckAll = true;
        var ids = document.getElementsByTagName("input");
        for (var i = 0; i < ids.length; i++) {
            if (ids[i].type == "checkbox" && ids[i].id.substring(0, 3) == "chL") {
                if (ids[i].id == 'chLAll') continue;
                if (!$(ids[i].id).checked) {
                    isCheckAll = false;
                    break;
                }
            }
        }
        if ($('chLAll') != null) $('chLAll').checked = isCheckAll;
    }
    else {
        if ($('chLAll') != null) $('chLAll').checked = false;
    }

    setMatchIds();
}

function setMatchIds() {
    var MatchIds = '';
    var ids = document.getElementsByTagName("input");
    for (var i = 0; i < ids.length; i++) {
        if (ids[i].type == "checkbox" && ids[i].id.substring(0, 3) == "chL") {
            if (ids[i].checked && ids[i].id != 'chLAll') {
                MatchIds += (MatchIds == '' ? ids[i].id.substring(3, ids[i].id.length) : ',' + ids[i].id.substring(3, ids[i].id.length));
            }
        }
    }
    $('MatchIds').value = (MatchIds==''?'-1':MatchIds);
}

function CheckSelAllMatch() {
    var bolMatch = $('chLAll').checked;
    var ids = document.getElementsByTagName("input");
    for (var i = 0; i < ids.length; i++) {
        if (ids[i].type == "checkbox" && ids[i].id.substring(0, 3) == "chL") {
            ids[i].checked = bolMatch;
        }
    }
    setMatchIds();
}

function GetMatchSportFull() {
    var ids = document.getElementsByTagName("input");
    $('MatchFull').value = '';
    for (var i = 0; i < ids.length; i++) {
        if (ids[i].type == "checkbox" && ids[i].id.substring(0, 3) == "chL") {
            if (ids[i].checked && (ids[i].id != 'chL0') && (ids[i].id != 'chLAll')) {
                $('MatchFull').value += ids[i].id.substring(3, ids[i].id.length) + ',';
            }
        }
    }
    if ($('MatchFull').value.substring($('MatchFull').value.length - 1, $('MatchFull').value.length) == ',')
        $('MatchFull').value = $('MatchFull').value.substring(0, $('MatchFull').value.length-1);
}

function setVisibleBoxMatch() {
    if (noClickSelect) {
        hiddenBoxMatch();
    }
    noClickSelect = true;
}