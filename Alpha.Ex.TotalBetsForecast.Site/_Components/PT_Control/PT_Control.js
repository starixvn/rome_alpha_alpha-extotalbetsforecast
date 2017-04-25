function PT_Reduce(id, f)
{
    var combo = $(id);
    if(combo.disabled) return;
    
    var selectedIndex = combo.selectedIndex;
    
    var newSelectedIndex =  selectedIndex + f;
    if(newSelectedIndex >= combo.options.length) newSelectedIndex = combo.options.length - 1;
    
    combo.selectedIndex = newSelectedIndex;
    
    if(combo.onchange) combo.onchange();
}

function PT_Increase(id, f)
{
    var combo = $(id);
    if(combo.disabled) return;
    
    var selectedIndex = combo.selectedIndex;
    
    var newSelectedIndex =  selectedIndex - f;
    if(newSelectedIndex <= 0) newSelectedIndex = 0;
    
    combo.selectedIndex = newSelectedIndex;
    if(combo.onchange) combo.onchange();
}

function PT_RenderCombo(combo, min, max, step, selectedValue) {
    var index = 0;
    var itemIndex = 0;

    if (!combo) return;
    combo.options.length = 0;
    
    var c = Math.round((max*100 - min*100)/(step*100));
        
    if(c <= 0)
    {
        var opt = document.createElement("option");
        opt.text = opt.value = Math.round(min * 100)/100;
        combo.options.add(opt);
        return;
    }

    for (var i = c; i >= 0; i--) {
        var f = Math.round(min * 100 + (i * step * 100)) / 100;
        var opt = document.createElement("option");

        if (selectedValue && parseFloat(selectedValue) == parseFloat(f))
            index = itemIndex;

        opt.text = opt.value = f;
        combo.options.add(opt);
        itemIndex++;
    }

    if (selectedValue) {
        if (document.all)
            combo.options[index].selected = true;
        else
            combo.value = selectedValue;
    }     
}