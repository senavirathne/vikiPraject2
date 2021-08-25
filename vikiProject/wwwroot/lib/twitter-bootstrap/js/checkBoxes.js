function cbChange(obj) {
    var cbs = document.getElementsByClassName("cb");
   
    for (var i = 0; i < cbs.length; i++) {
        cbs[i].checked = false;
        cbs[i].nextElementSibling.nextElementSibling.nextElementSibling.type = "hidden";
        
    }
    obj.checked = true;
    var x =obj.nextElementSibling.nextElementSibling.nextElementSibling;
    x.value = "Generate";
    x.type = "submit";
    
}