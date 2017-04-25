
function doViewBetList(matchId, betType, type) {
    var url = age.GetBaseUrl() + '_BetList/BetList.aspx?matchId=' + matchId + '&betType=' + betType + '&type=' + type;
    window.location.href = url;
}

function doViewBetListRacing(matchId, betType, type) {
    var url = age.GetBaseUrl() + '_BetList/BetList.aspx?matchId=' + matchId + '&betType=' + betType + '&type=' + type + '&racing=1';
    window.location.href = url;
}

function doViewBetListGame(matchId, betType, type, islive) {
    var url = age.GetBaseUrl() + '_BetList/BetList.aspx?matchId=' + matchId + '&betType=' + betType + '&type=' + type + '&islive=' + islive;
    window.location.href = url;
}

function doViewBetListNewGames(matchId, betType, type, islive, betteam) {
    var url = age.GetBaseUrl() + '_BetList/BetList.aspx?matchId=' + matchId + '&betType=' + betType + '&type=' + type + '&islive=' + islive + '&betteam=' + betteam;
    window.location.href = url;
}

function openForecast(matchId, betType) {
    top.popupManager.openByRelativeUrl('_Forecast/FCAH.aspx?matchId=' + matchId + '&b=' + betType, '', 900);
}

function openForecast1X2(matchId, betType) {
    top.popupManager.openByRelativeUrl('_Forecast/FC1X2.aspx?is_popup=1&matchId=' + matchId + '&betType=' + betType, '', 900);
}
