
function doViewBetList(matchId, betType) {
    var url = _betListsUrl + 'BetListByBetTypeAndMatch/Index?matchId=' + matchId + '&betType=' + betType;
    window.location.href = url;
}

function doViewBetListRacing(matchId, betType) {
    var url = _betListsUrl + 'BetListByBetTypeAndMatch/Index?matchId=' + matchId + '&betType=' + betType;
    window.location.href = url;
}

function doViewBetListGame(matchId, betType, islive) {
    var url = _betListsUrl + 'BetListByBetTypeAndMatch/Index?matchId=' + matchId + '&betType=' + betType + '&islive=' + islive;
    window.location.href = url;
}

function doViewBetListNewGames(matchId, betType, islive, betteam) {
    var url = _betListsUrl + 'BetListByBetTypeAndMatch/Index?matchId=' + matchId + '&betType=' + betType + '&islive=' + islive + '&betteam=' + betteam;
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
