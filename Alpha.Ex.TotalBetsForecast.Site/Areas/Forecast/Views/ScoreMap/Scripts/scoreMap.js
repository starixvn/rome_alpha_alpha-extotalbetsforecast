(function () {
    'use strict';
    var hiddenMatchId = $('#requestMatchids');
    var hiddenLiveBettype = $('#requestBettypesLive'),
        hiddenDeadBettype = $('#requestBettypesDead');

    var fromBetList = hiddenMatchId.val().length !== 0;
    // DOM.
    var isLeagueChanged = true,
        leagueSelect = null,
        loadingIcon = null,
        loadingSubmit = null;
    // Buttons.
    var dSelMatch = null,
        dSubmit = null;

    // Boxes.
    var matchBox = null,
        liveDeadBallsBox = null,
        mainContent = null;

    function enableButton(button, value) {
        if (value) {
            //button.addClass('btn');
            button.removeAttr('disabled');
        }
        else {
            //button.removeClass('btn');
            button.prop('disabled', 'disabled');
        }
    }

    function showElement(element, value) {
        if (value) {
            element.removeClass('hide');
        }
        else {
            element.addClass('hide');
        }
    }

    function showMatchError(msg) {
        if (msg) {
            $('#msgMatchErr').html(msg);
            $('#tblMsgMatch').removeClass('hide');
        }
        else {
            $('#msgMatchErr').html('');
            $('#tblMsgMatch').addClass('hide');
        }
    }

    function showBettypeError(msg) {
        if (msg) {
            $('#msgBettypeErr').html(msg);
            $('#tblMsgBettype').removeClass('hide');
        }
        else {
            $('#msgBettypeErr').html('');
            $('#tblMsgBettype').addClass('hide');
        }
    }

    function cacheDOMElements() {
        if (!fromBetList) {
            leagueSelect = $('#League');
            loadingIcon = $('#loading');
            matchBox = $('#match-box');
            dSelMatch = $('#dSelMatch');
        }

        loadingSubmit = $('#loadingSubmit');

        // Boxes.
        liveDeadBallsBox = $('#tblLiveDead');
        mainContent = $('#mainContent');

        // Buttons.
        dSubmit = $('#dSubmit');
    }

    function viewScoremapInfo() {
        var width = window.top.window.innerWidth * 0.7;
        var url = rootUrl + 'ForecastScoreMap/BetTypesInfo';

        top.popupManager.openMultiple(url, '', width);
    }

    function leagueChanged() {
        isLeagueChanged = true;
        dSelMatch.text(scoreMapResources.SelectMatch);
        showElement(matchBox, false);
        showElement(liveDeadBallsBox, false);
        mainContent.html('');
    }

    function refreshLeagues() {
        isLeagueChanged = true;
        showElement(matchBox, false);
        $.ajax({
            method: 'GET',
            url: rootUrl + 'ForecastScoreMap/RefreshLeagues',
            dataType: 'json',
            beforeSend: function (jqXHR, settings) {
                loadingIcon.addClass('loading');
                enableButton(dSelMatch, false);
                enableButton(dSubmit, false);
                dSelMatch.text(scoreMapResources.SelectMatch);
            },
            success: function (data, textStatus, jqXHR) {
                var selectItems = [];
                $.each(data, function (key, value) {
                    selectItems.push('<option value="' + value.Id + '">' + value.Name + '</option>');
                });

                leagueSelect.html(selectItems.join(''));
            },
            complete: function (jqXHR, textStatus) {
                loadingIcon.removeClass('loading');
                enableButton(dSelMatch, true);
                enableButton(dSubmit, true);
            }
        });
    }

    function selectMatchesByLeague(leagueId) {
        $.ajax({
            method: 'GET',
            url: rootUrl + 'ForecastScoreMap/MatchesByLeague?leagueId=' + leagueSelect.val(),
            dataType: 'json',
            beforeSend: function (jqXHR, settings) {
                loadingIcon.addClass('loading');
                enableButton(dSelMatch, false);
            },
            success: function (data, textStatus, jqXHR) {
                var matchesCount = data ? data.length : 0;
                if (matchesCount > 0) {
                    var matchArray = ['<ul class="match-list">'];
                    if (matchesCount > 1) {
                        matchArray.push('<li><input type="checkbox" id="chLAll" class="hand"/>\
                            <label for="chLAll" class="hand"><b>' + scoreMapResources.All + '</b></label></li>');
                    }

                    $.each(data, function (key, value) {
                        matchArray.push('<li><input type="checkbox" class="match hand" id="chL' + value.MatchId + '" value="' + value.MatchId + '"/>');
                        matchArray.push('<label class="hand" for="chL' + value.MatchId + '">');
                        matchArray.push(value.HomeName + ' -vs- ' + value.AwayName + '</label></li>');
                    });

                    matchArray.push('</ul>')
                    $('#match-list').html(matchArray.join(''));
                    matchBox.removeClass('hide');

                    selectAllMatches();
                }
            },
            complete: function (jqXHR, textStatus) {
                loadingIcon.removeClass('loading');
                enableButton(dSelMatch, true);
                dSelMatch.text(scoreMapResources.HideMatch);
            }
        });
    }

    function closeMatchBox() {
        dSelMatch.text(scoreMapResources.SelectMatch);
        showElement(matchBox, false);

        if (liveDeadBallsBox.is(':visible')) {
            mainContent.html('');
            showElement(liveDeadBallsBox, false);
        }
    }

    function selectMatch() {
        var selectedMatches = $('.match:checked');
        if (selectedMatches.length === 0) {
            showMatchError(scoreMapResources.MatchEmptyError);
            return;
        }

        showMatchError();
        showElement(matchBox, false);
        showElement(liveDeadBallsBox, true);
        dSelMatch.text(scoreMapResources.SelectMatch);
    }

    function selectAllMatches() {
        var allMatch = $('#chLAll');
        var singleMatch = $('.match');

        allMatch.unbind('click').bind('click', function () {
            var value = $(this).prop('checked');
            singleMatch.prop('checked', value ? 'checked' : '');
        });

        singleMatch.unbind('click').bind('click', function () {
            if (singleMatch.length === $('.match:checked').length) {
                allMatch.prop('checked', 'checked');
            }
            else {
                allMatch.prop('checked', '');
            }
        });
    }

    function selectAllLiveDeadBalls(isLive) {
        var allBettype, bettypeClass;
        if (isLive) {
            allBettype = $('#allBettypeLive');
            bettypeClass = '.liveBettype';
        }
        else {
            allBettype = $('#allBettypeDead');
            bettypeClass = '.deadBettype';
        }

        var bettype = $(bettypeClass);

        allBettype.unbind('click').bind('click', function () {
            var value = $(this).prop('checked');
            bettype.prop('checked', value ? 'checked' : '');
        });

        bettype.unbind('click').bind('click', function () {
            if (bettype.length === $(bettypeClass + ':checked').length) {
                allBettype.prop('checked', 'checked');
            }
            else {
                allBettype.prop('checked', '');
            }
        });
    }

    function getScoreMap() {
        var liveBettypes = [];
        $.each($('.liveBettype:checked'), function (key, value) {
            liveBettypes.push($(value).attr('data-ids'));
        });

        var deadBettypes = [];
        $.each($('.deadBettype:checked'), function (key, value) {
            deadBettypes.push($(value).attr('data-ids'));
        });

        if (liveBettypes.length === 0 && deadBettypes.length === 0) {
            mainContent.html('');
            showBettypeError(scoreMapResources.BettypeEmptyError);
            return;
        }

        showBettypeError();
        var matchIds = hiddenMatchId.val();
        if (!fromBetList) {
            var matches = [];
            $.each($('.match:checked'), function (key, value) {
                matches.push(value.value);
            });

            matchIds = matches.join(',');
        }

        var liveBettypesParam = liveBettypes.length === 0 ? '-1' : liveBettypes.join(',');
        hiddenLiveBettype.val(liveBettypesParam);

        var deadBettypesParam = deadBettypes.length === 0 ? '-1' : deadBettypes.join(',');
        hiddenDeadBettype.val(deadBettypesParam);

        var params = '?isRefresh=false&leagueid=0&matchIds=' + matchIds + '&bettypes_live=' + liveBettypesParam + '&bettypes_dead=' + deadBettypesParam;
        $.ajax({
            method: 'GET',
            url: rootUrl + 'ForecastScoreMap/Results' + params,
            dataType: 'html',
            beforeSend: function (jqXHR, settings) {
                enableButton(dSubmit, false);
                loadingSubmit.addClass('loading');
                $('.lnkRefresh').addClass('lnkRefreshClick');
            },
            success: function (data, textStatus, jqXHR) {
                mainContent.html(data);
                toggleScoreMap(mainContent);
            },
            complete: function (jqXHR, textStatus) {
                enableButton(dSubmit, true);
                loadingSubmit.removeClass('loading');
            }
        });
    }

    function toggleScoreMap(container) {
        container.find('.button_mid').unbind('click').bind('click', function () {
            var matchId = $(this).attr('data-matchid');
            var scoreMapContainer = $('#scoreMap' + matchId);
            var lnkGroupscoreMap = $('#lnkGroupscoreMap' + matchId)
            if (scoreMapContainer.is(':visible')) {
                scoreMapContainer.addClass('hide');
                lnkGroupscoreMap.removeClass('lnkGroup').addClass('lnkGroupClick');
            }
            else {
                scoreMapContainer.removeClass('hide');
                lnkGroupscoreMap.removeClass('lnkGroupClick').addClass('lnkGroup');
            }
        })

        container.find('.lnkRefresh').unbind('click').bind('click', function () {
            var $this = $(this);
            var matchId = $this.attr('data-matchid');

            var params = '?isRefresh=true&leagueid=0&matchIds=' + matchId + '&bettypes_live=' + hiddenLiveBettype.val() + '&bettypes_dead=' + hiddenDeadBettype.val();
            $.ajax({
                method: 'GET',
                url: rootUrl + 'ForecastScoreMap/Results' + params,
                dataType: 'html',
                beforeSend: function (jqXHR, settings) {
                    $this.removeClass('lnkRefresh').addClass('lnkRefreshClick');
                },
                success: function (data, textStatus, jqXHR) {
                    var refreshContent = $('#RefreshContent' + matchId);
                    refreshContent.html(data);
                    toggleScoreMap(refreshContent);
                },
                complete: function (jqXHR, textStatus) { }
            });
        });
    }

    function initialize() {
        $.ajaxSetup({
            error: function (xhr) {
                if (xhr.status === 500 && xhr.responseText) {
                    var jsonData = JSON.parse(xhr.responseText);
                    if (jsonData.UMFrom && jsonData.UMTo) {
                        location.href = top.alpha.urls.mainRootPath + 'PartialUM/Index?umfrom=' + jsonData.UMFrom + '&umto=' + jsonData.UMTo;
                    }
                }
            }
        });

        cacheDOMElements();
        $('.infoIco').click(function () { viewScoremapInfo(); });
        if (fromBetList) {
            var containerName = '#RefreshContent' + hiddenMatchId.val();
            toggleScoreMap($(containerName));
        }
        else {
            $('.icon-close').click(function () { closeMatchBox(); });

            leagueSelect.change(function () { leagueChanged(); });
            dSelMatch.click(function () {
                if (isLeagueChanged) {
                    selectMatchesByLeague(leagueSelect.val());
                    isLeagueChanged = false;
                }
                else if (matchBox.is(':visible')) {
                    selectMatch();
                }
                else {
                    showElement(matchBox, true);
                    dSelMatch.text(scoreMapResources.HideMatch);
                }
            });

            $('#dSubmitInMatch').click(function () { selectMatch(); });
        }

        selectAllLiveDeadBalls(true);
        selectAllLiveDeadBalls(false);

        $('#RefreshLeagues').click(function () { refreshLeagues(); });

        // Buttons event.
        dSubmit.click(function () { getScoreMap(); });
    }

    initialize();
})();