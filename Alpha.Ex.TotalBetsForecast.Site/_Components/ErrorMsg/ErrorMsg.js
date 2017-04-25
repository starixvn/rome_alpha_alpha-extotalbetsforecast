
// Prototype
AGEMsg = function () {
    this.spmsgerr = 'spmsgerr';
    this.diverrmsg = 'diverrmsg';
}

AGEMsg.prototype.Show = function (msg, success) {
    if (success) {
        $('spmsgerr').className = 'msgscc';
    }
    else {
        $('spmsgerr').className = 'msgerr';
    }    

    $(this.spmsgerr).innerHTML = msg;

    if (document.getElementById("spmsgerr").childNodes[0].tagName == 'TABLE') {
        document.getElementById("spmsgerr").classList.add("container");
    }

    $(this.diverrmsg).style.display = IE ? 'block' : 'table';
}

AGEMsg.prototype.ShowMsgs = function (msg, success) {
    if (success) {
        $('spmsgerr').className = 'msgscc';
    }
    else {
        $('spmsgerr').className = 'Enrich';
    }
    
    $(this.spmsgerr).innerHTML = msg;

    if (document.getElementById("spmsgerr").childNodes[0].tagName == 'TABLE') {
        document.getElementById("spmsgerr").classList.add("container");
    }

    $(this.diverrmsg).style.display = IE ? 'block' : 'table';
}

AGEMsg.prototype.Hide = function () {
    $(this.diverrmsg).style.display = 'none';
}

AGEMsg.prototype.InsertRow = function (tableId, classname, message) {
    var tr = document.createElement('tr');
    var td = document.createElement('td');

    var div = age.createElement('DIV', td);
    div.innerHTML = message;

    td.className = classname;
    tr.appendChild(td);
    document.getElementById(tableId).appendChild(tr);
}

AGEMsg.prototype.CheckMessageIsSucc = function (ArrCodes) {
    for (var i = 0; i < ArrCodes.length; i++) {
        if (ArrCodes[i] > 0)
            return false;
    }
    return true;
}

AGEMsg.prototype.GenerateMessage = function (ArrCodes, ArrMsgs, ArrUserName, isAdd) {
    var isSucc = true;
    isSucc = isSucc & this.CheckMessageIsSucc(ArrCodes);

    if (isAdd) // Added
    {
        for (var j = 0; j < ArrUserName.length; j++) {
            ageMsg.InsertRow('tblMessage', 'EnrichUserName', ArrUserName[j]);
            for (var i = 0; i < ArrCodes.length; i++) {
                if (ArrCodes[i] < 0) {
                    ageMsg.InsertRow('tblMessage', 'EnrichSuccmsg', ArrMsgs[i]);
                }
                else {
                    ageMsg.InsertRow('tblMessage', 'EnrichErrmsg', ArrMsgs[i]);
                }
            }
        }
    }
    else // Reset table
    {
        var myTable = '<table id="tblMessage" cellpadding="0" cellspacing="0" border="0" style="width:100%;">';
        var myTDTemp = '';
        var myEndTable = ' </table>';
        for (var j = 0; j < ArrUserName.length; j++) {
            myTDTemp += '<tr><td class="EnrichUserName">';
            myTDTemp += ArrUserName[j];
            myTDTemp += "</td></tr>";
            for (var i = 0; i < ArrCodes.length; i++) {
                if (ArrCodes[i] < 0) {
                    myTDTemp += '<tr><td class="EnrichSuccmsg">';
                    myTDTemp += ArrMsgs[i];
                    myTDTemp += "</td></tr>";
                }
                else {
                    myTDTemp += '<tr><td class="EnrichErrmsg">';
                    myTDTemp += ArrMsgs[i];
                    myTDTemp += "</td></tr>";
                }
            }
        }
        myTable += myTDTemp + myEndTable;

        ageMsg.ShowMsgs(myTable, isSucc);
    }
}

AGEMsg.prototype.CheckMessageIsSucc2 = function (ArrCodes) {
    for (var i = 0; i < ArrCodes.length; i++) {
        if (ArrCodes[i] > 0 || ArrCodes[i] == -1)
            return false;
    }
    return true;
}

ageMsg = new AGEMsg();