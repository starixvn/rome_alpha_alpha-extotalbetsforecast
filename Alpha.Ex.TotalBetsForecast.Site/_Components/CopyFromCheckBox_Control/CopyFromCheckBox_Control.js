// Copy from Win action
//window.enableCopyList = new Array();
//function Getlocation(elm) {
//    var suffix = elm.id.substring(elm.id.indexOf('_') + 1, elm.id.length);
//    for (var i = 0; i < ArrBetType.length; i++) {
//        if (suffix == ArrBetType[i]) return i;
//    }
//}
//function CheckCopy_Onclick(e, Arr) {
//    SetCopy_Control(e.checked, Arr, Getlocation(e));
//}

//function SetCopy_Control(ischeck, Arr, location) {
//    var arr_temp = new Array();
//    if (ischeck) {
//        for (var i = 0; i < Arr.length; i++) {
//            if ($(Arr[i]) && !$(Arr[i]).disabled) {
//                $(Arr[i]).disabled = true;
//                arr_temp[arr_temp.length] = $(Arr[i]);                
//            }
//        }
//        window.enableCopyList[location] = arr_temp;
//    }
//    else {
//        for (var i = 0; i < window.enableCopyList[location].length; i++) {
//            window.enableCopyList[location][i].disabled = false;
//        }
//    }
//}