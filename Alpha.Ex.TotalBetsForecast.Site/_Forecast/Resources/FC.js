function openBetListPopup(matchId, betType) {
    top.popupManager.openMultiple(
        _betListsUrl + "BetListByBetTypeAndMatch/Index?matchid=" + matchId + "&bettype=" + betType,
        "", 1024);
}

function GetFCAH(matchId, score, bettype) {
    ajax.Request("FCAH.aspx", {
        asynchronous: true,
        method: 'post',
        parameters: 'matchid=' + matchId + '&score=' + score + "&b=" + bettype + "&full_mode=0",
        onComplete: function (rs) {
            $('sContent').innerHTML = rs.responseText;
        }
    });
}
function openForecast(matchId, betType) {
    if (betType == 1) {
        location.href = _betListsUrl + 'BetListByBetTypeAndMatch/Index?matchid=' + matchId + '&bettype=5';
    }
    else {
        location.href = _betListsUrl + 'BetListByBetTypeAndMatch/Index?matchid=' + matchId + '&bettype=15';
    }
}
function openForecastPopup(matchId, betType) {
    var url = age.GetExTotalBetsForecastUrl() + '_Forecast/FCAH.aspx?matchid=' + matchId + '&b=' + betType

    top.popupManager.open(url, '', 600, 290);
}

function RefreshFC() {
    if (countDownTimer != null) clearTimeout(countDownTimer);
    document.getElementById('CountDownRefresh').innerHTML = loadingTxt;


    var leagueid = $('ddlLeague') ? $('ddlLeague').value : null;
    // Get href

    var href = window.location.href;
    // Viewing bettype
    var betTypeInput = document.getElementById('bettype_vl');
    if (betTypeInput) {
        var betType = betTypeInput.value;
        var regExp = new RegExp("b=\\d", "g");
        href = href.replace(regExp, "b=" + betType);
    }
    // Viewing score
    var scoreInput = document.getElementById('score_vl');
    if (scoreInput) {
        var score = scoreInput.value;
        href += "&score=" + score;
    }

    var ajaxParameters = leagueid != null ? 'full_mode=0&leagueid=' + leagueid : 'full_mode=0';
    ajax.Request(href, {
        asynchronous: true, parameters: ajaxParameters
                            , onComplete: function (data) {
                                $('sContent').innerHTML = data.responseText;
                                RefreshCountDown_Init();
                            }
    });

    return true;
}

function GetForecastByLeague() {
    var leagueid = null;
    if ($('ddlLeague')) {
        leagueid = $('ddlLeague').value;
    }
    if (leagueid) {
        ajax.Request(location.href, {
            asynchronous: true,
            method: 'post',
            parameters: 'full_mode=0&leagueid=' + leagueid,
            onComplete: function (rs) {
                $('sContent').innerHTML = rs.responseText;
            }
        });
    }
}

function correctCheckValueOnDropdown(dropdownId) {
    var dropdown = $(dropdownId);
    if (!dropdown) {
        return;
    }

    for (var i = 0; i < dropdown.length; i++) {
        var option = dropdown[i];
        var attributeLength = option.attributes.length;
        for (var j = 0; j < attributeLength; j++) {
            var attr = option.attributes[j];
            if (attr.name == "selected") {
                dropdown.selectedIndex = i;
                dropdown.value = option.value;

                break;
            }
        }
    }
}

function initFC() {
    correctCheckValueOnDropdown("ddlLeague");
    var ageWnd = parent.ageWnd;
    if (typeof ageWnd == 'undefined') {
        return;
    }
    var h = 320;
    var w = 600;

    if (ageWnd.loaded) {
        ageWnd.setRect(ageWnd.left, ageWnd.top, w, h);
        //if (navigator.userAgent.match(/Android|BlackBerry|iPhone|iPad|iPod|Opera Mini|IEMobile/i) != null) {
        //    ageWnd.setRect(ageWnd.left, ageWnd.top, w, 320);
        //}
    }

}

RegisterStartUp('initFC()');

function getEditSettingPopUpLeft() {
    var accountInfoWidth = top.isNewSite ? top.$('.sidebar').width() : top.menu.innerWidth;
    return accountInfoWidth + 8;
}