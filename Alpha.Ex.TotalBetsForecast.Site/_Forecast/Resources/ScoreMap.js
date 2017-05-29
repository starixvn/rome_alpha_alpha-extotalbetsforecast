var isCalling = false;
var lnkRefresh = "lnkRefresh";
var lnkRefreshClick = "lnkRefreshClick";

RegisterStartUp("initScoreMap()");

function initScoreMap() {
    try {
        eCol();

        if ($('page_main') != null && $('page_main') != undefined) {
            $("page_main").onclick = function () {
                setVisibleBoxMatch();
            };
        }

        if ($('RefreshLeagues') != null && $('RefreshLeagues') != undefined) {
            $("RefreshLeagues").onclick = function () {
                RefreshLeague();
            };
        }

        if ($('dSelMatch') != null && $('dSelMatch') != undefined) {
            $("dSelMatch").onclick = function () {
                SelectMatch();
            };
        }

        if ($('closeBoxMatch') != null && $('closeBoxMatch') != undefined) {
            $("closeBoxMatch").onclick = function () {
                CloseBoxMatch();
            };
        }

        if ($('dSubmit') != null && $('dSubmit') != undefined) {
            $("dSubmit").onclick = function () {
                LoadScoreMap();
            };
        }

        if ($('dSubmitInMatch') != null && $('dSubmitInMatch') != undefined) {
            $("dSubmitInMatch").onclick = function () {
                hiddenBoxMatch();
            };
        }

        if ($('chkAllL') != null && $('chkAllL') != undefined) {
            $("chkAllL").onclick = function () {
                CheckAllBetType('L');
            };
        }

        if ($('chkAllD') != null && $('chkAllD') != undefined) {
            $("chkAllD").onclick = function () {
                CheckAllBetType('D');
            };
        }

        $('scoreMapIcon').onclick = function () {
            var width = window.top.window.innerWidth * 0.7;
            var url = age.GetExTotalBetsForecastUrl() + 'ForecastScoreMap/BetTypesInfo';
            top.popupManager.open(url, '', width);
        };
    }
    catch (err) { }
}

function hideBoxMatch() {
    $("BoxMatchWL").style.display = "none";
    $('tblMsgMatch').style.display = "none";
    $('dSelMatch').value = _page.selectMatch;
}

function ShowHideLiveDead(result) {
    $("tblLiveDead").style.display = result;
    $('mainContent').style.display = result;
    if (result == "none")
        $('mainContent').innerHTML = "";
}

function hiddenBoxMatch() {
    if ($('MatchIds').value != '-1') {
        hideBoxMatch();
        ShowHideLiveDead("");
    }
    else {
        $('msgMatchErr').innerHTML = _page.plsChooseMatch;
        $('tblMsgMatch').style.display = "";
    }
}

function CloseBoxMatch() {
    hideBoxMatch();
    if ($('MatchIds').value != '-1') {
        ShowHideLiveDead("");
    }
    else {
        ShowHideLiveDead("none");
    }
    $('dSelMatch').value = _page.selectMatch;
}

function CommonLoadData(MatchId) {
    hiddenBoxMatch();
    LoadingRefresh(MatchId, lnkRefreshClick);
    setBetTypes('L');
    setBetTypes('D');
}

function DisableBtnSubmit() {
    var dSubmit = $('dSubmit');

    dSubmit.disabled = true;
    if (!HasClassName(dSubmit, 'hidden')) dSubmit.className = 'btn2';

    var loading = $("loadingSubmit");
    if (loading) loading.className = "loading";
}

function EnableBtnSubmit() {
    var dSubmit = $('dSubmit');

    dSubmit.disabled = false;
    if (!HasClassName(dSubmit, 'hidden')) dSubmit.className = 'btn';

    var loading = $("loadingSubmit");
    if (loading) loading.className = "";
}

function LoadScoreMap() {
    if (isCalling && typeof (http) != 'undefined') {
        http.abort();
        isCalling = false;
    }
    isCalling = true;

    if ($('MatchIds') && $('MatchIds').value != '-1') {
        CommonLoadData($('MatchIds').value);
        if ($('strValueL').value == '-1' && $('strValueD').value == '-1') {
            $('mainContent').innerHTML = "";
            $('spmsgerr').innerHTML = _page.plsChooseBetType;
            $('diverrmsg').style.display = "";
            LoadingRefresh($('MatchIds').value, lnkRefresh);
        }
        else {
            $('diverrmsg').style.display = "none";
            DisableBtnSubmit();
            var paras = "isRefresh=false" + "&leagueid=0&matchIds=" + $('MatchIds').value + "&bettypes_live=" + $('strValueL').value + "&bettypes_dead=" + $('strValueD').value;
            http = jx.request(
                '../ForecastScoreMap/Results', onComplete, 'GET'
                , paras
            );
        }
    }
    else {
        SelectMatch();
    }

    function onComplete(rs) {
        if (!isCalling) {
            return;
        }

        if (typeof rs == 'object') {
            $('mainContent').innerHTML = rs.responseText;
            LoadingRefresh($('MatchIds').value, lnkRefresh);
            EnableBtnSubmit();
            eCol();
        }

        isCalling = false;
    }
}

function GroupScoreMap(GroupMatchId) {
    if ($(GroupMatchId).style.display != "") {
        $(GroupMatchId).style.display = "";
        $('lnkGroup' + GroupMatchId).className = "lnkGroup";
    }
    else {
        $(GroupMatchId).style.display = "none";
        $('lnkGroup' + GroupMatchId).className = "lnkGroupClick";
    }
}

function RefreshScoreMap(MatchId) {
    if (isCalling && typeof (http) != 'undefined') {
        http.abort();
        isCalling = false;
    }
    isCalling = true;

    CommonLoadData(MatchId);
    var paras = "isRefresh=true" + "&leagueid=0&matchIds=" + MatchId + "&bettypes_live=" + $('strValueL').value + "&bettypes_dead=" + $('strValueD').value;
    http = jx.request(
        '../ForecastScoreMap/Results', onComplete, 'GET'
        , paras
    );

    function onComplete(rs) {
        if (!isCalling) {
            return;
        }

        if (typeof rs == 'object') {
            $('RefreshContent' + MatchId).innerHTML = rs.responseText;
            LoadingRefresh(MatchId, lnkRefresh);
            eCol();
        }

        isCalling = false;
    }
}

function LoadingRefresh(Matchs, cName) {
    var arrMatchs = Matchs.split(',');
    for (var i = 0; i < arrMatchs.length; i++) {
        if ($('lnkRefresh' + arrMatchs[i]) != null && $('lnkRefresh' + arrMatchs[i]) != undefined) {
            $('lnkRefresh' + arrMatchs[i]).className = cName;
        }
    }
}

function ChangeLeague() {
    $('MatchIds').value = "-1";
    hideBoxMatch();
    ShowHideLiveDead("none");
}

function RefreshLeague() {
    DisableButton();
    var LeagueID = $('ddlLeague') ? $('ddlLeague').value : '0';
    http = jx.request(
    '../_Forecast/RefreshLeague.aspx', onComplete, 'POST'
        , 'LeagueID=' + LeagueID
    );

    function onComplete(rs) {
        if (typeof rs == 'object') {
            $('selLeague').innerHTML = rs.responseText;
            $('isChanged').value = $('isChanged').value != 0 ? 0 : 1;
            $('MatchIds').value = "-1";
            hideBoxMatch();
            EnableButton();
        }
    }
}

/* START: For Box Bet Types */
function isAllBetType(Type, curBetType) {
    var strChkBetType = "bt" + Type;
    var strChkAllBetType = "chkAll" + Type;
    if ($(curBetType).checked) {
        var isCheckAll = true;
        var ids = document.getElementsByTagName("input");
        for (var i = 0; i < ids.length; i++) {
            if (ids[i].type == "checkbox" && ids[i].id.substring(0, 3) == strChkBetType) {
                if (ids[i].id == strChkAllBetType) continue;
                if (!$(ids[i].id).checked) {
                    isCheckAll = false;
                    break;
                }
            }
        }
        if ($(strChkAllBetType) != null) $(strChkAllBetType).checked = isCheckAll;
    }
    else {
        if ($(strChkAllBetType) != null) $(strChkAllBetType).checked = false;
    }
}

function CheckAllBetType(Type) {
    var strChkBetType = "bt" + Type;
    var bolAll = $('chkAll' + Type).checked;
    var ids = document.getElementsByTagName("input");
    for (var i = 0; i < ids.length; i++) {
        if (ids[i].type == "checkbox" && ids[i].id.substring(0, 3) == strChkBetType) {
            ids[i].checked = bolAll;
        }
    }
}

function setBetTypes(Type) {
    var strChkBetType = "bt" + Type;
    var BetTypes = '';
    var ids = document.getElementsByTagName("input");
    for (var i = 0; i < ids.length; i++) {
        if (ids[i].type == "checkbox" && ids[i].id.substring(0, 3) == strChkBetType) {
            if (ids[i].checked && ids[i].id != 'chLAll') {
                BetTypes += (BetTypes == '' ? ids[i].id.substring(3, ids[i].id.length) : ',' + ids[i].id.substring(3, ids[i].id.length));
            }
        }
    }
    $('strValue' + Type).value = (BetTypes == '' ? '-1' : BetTypes);
}
/* END: For Box Bet Types */



function hCol(tblMatchId, n) {
    n = n - 1;
    var tTab = document.getElementById(tblMatchId);
    for (var j_tem = 0; j_tem < 7; j_tem++) {
        if (j_tem == n)
            tTab.rows[1].cells[j_tem].style.backgroundColor = '#d0d3d3';
    }
}
function lCol(tblMatchId, n) {
    n = n - 1;
    var tTab = document.getElementById(tblMatchId);
    for (var j_tem = 0; j_tem < 7; j_tem++) {
        if (j_tem == n)
            tTab.rows[1].cells[j_tem].style.backgroundColor = '';
    }
}
function eCol() {
    if ($('MatchIds').value != -1) {
        var arrMatchIds = $('MatchIds').value.split(',');
        for (k = 0; k < arrMatchIds.length; k++) {
            var tblHaftTime = 'tblht' + arrMatchIds[k];
            var htTab = document.getElementById(tblHaftTime);

            var tblFullTime = 'tblft' + arrMatchIds[k];
            var ftTab = document.getElementById(tblFullTime);

            for (var i_tem = 2; i_tem < htTab.rows.length; i_tem++)
                for (var j_tem = 1; j_tem < htTab.rows[i_tem].cells.length; j_tem++) {
                    htTab.rows[i_tem].cells[j_tem].onmouseover = new Function("hCol('" + tblHaftTime + "'," + j_tem + ")");
                    htTab.rows[i_tem].cells[j_tem].onmouseout = new Function("lCol('" + tblHaftTime + "'," + j_tem + ")");

                    ftTab.rows[i_tem].cells[j_tem].onmouseover = new Function("hCol('" + tblFullTime + "'," + j_tem + ")");
                    ftTab.rows[i_tem].cells[j_tem].onmouseout = new Function("lCol('" + tblFullTime + "'," + j_tem + ")");
                }
        }
    }
}