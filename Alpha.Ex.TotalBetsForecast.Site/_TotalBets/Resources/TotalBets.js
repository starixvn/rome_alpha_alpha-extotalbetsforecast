
function doViewBetList(matchId, betType, type) {
    var url = _oldMainUrl + '_BetList/BetList.aspx?matchId=' + matchId + '&betType=' + betType + '&type=' + type;
    window.location.href = url;
}

function doViewBetListRacing(matchId, betType, type) {
    var url = _oldMainUrl + '_BetList/BetList.aspx?matchId=' + matchId + '&betType=' + betType + '&type=' + type + '&racing=1';
    window.location.href = url;
}

function doViewBetListGame(matchId, betType, type, islive) {
    var url = _oldMainUrl + '_BetList/BetList.aspx?matchId=' + matchId + '&betType=' + betType + '&type=' + type + '&islive=' + islive;
    window.location.href = url;
}

function doViewBetListNewGames(matchId, betType, type, islive, betteam) {
    var url = _oldMainUrl + '_BetList/BetList.aspx?matchId=' + matchId + '&betType=' + betType + '&type=' + type + '&islive=' + islive + '&betteam=' + betteam;
    window.location.href = url;
}

function openForecast(matchId, betType) {
    var url = age.GetExTotalBetsForecastUrl() + '_Forecast/FCAH.aspx?matchId=' + matchId + '&b=' + betType

    top.popupManager.open(url, '', 900);
}

function openForecast1X2(matchId, betType) {
    var url = age.GetExTotalBetsForecastUrl() + '_Forecast/FC1X2.aspx?is_popup=1&matchId=' + matchId + '&betType=' + betType

    top.popupManager.open(url, '', 900);
}
