function ConfirmMessage(strModule, strAction) {
    if (GetHiddenField().value == '') {
        alert("Please select atleast one " + strModule + " from list.");
        return false;
    }
    else {
        return confirm("Are you sure? You want to " + strAction + " " + strModule + ".");
    }

}

function ChangeMessage(strModule, strAction) {
    if (GetHiddenField().value == '') {
        alert("Please select atleast one " + strModule + " from list.");
        return false;
    }
    else {
        return true;
    }
}

function wopen(url, name, w, h) {
    // Fudge factors for window decoration space.
    // In my tests these work well on all platforms & browsers.
    w += 32;
    h += 96;
    wleft = (screen.width - w) / 2;
    wtop = (screen.height - h) / 2;
    var win = window.open(url, name,
                        'width=' + w + ', height=' + h + ', ' + 'left=' + wleft + ', top=' + wtop + ', ' + 'location=no, menubar=no, ' + 'status=no, toolbar=no, scrollbars=yes, resizable=no');
    // Just in case width and height are ignored
    win.resizeTo(w, h);
    // Just in case left and top are ignored
    win.moveTo(wleft, wtop);
    win.focus();
    return false;
}

function GetHiddenField() {
    var arAllControls = document.getElementsByTagName("input");
    for (var i = 0; i < arAllControls.length; i++) {
        if (arAllControls[i].type == "hidden") {
            if (arAllControls[i].id.indexOf("hdnSelectedIDs") > 0) {
                return arAllControls[i];
                i = arAllControls.length;
            }
        }
    }
}






function setUpCK(ControlId) {

    try {
        var instance = CKEDITOR.instances[ControlId];

        if (instance) {
            CKEDITOR.remove(ControlId);
        }
        CKEDITOR.config.toolbar = 'AdminPanel';

        CKEDITOR.replace(ControlId, { filebrowserImageUploadUrl: 'UploadHandler.ashx', htmlEncodeOutput: true });
    }
    catch (e) {
    }
}
function setUpCK_Multiple(ControlId) {
    var arAllControls = document.getElementsByTagName("textarea");
    for (var i = 0; i < arAllControls.length; i++) {
        if (arAllControls[i].id.indexOf(ControlId) > 0) {
            //alert(arAllControls[i].id);
            var instance = CKEDITOR.instances[arAllControls[i].id];
            if (instance) {
                CKEDITOR.remove(arAllControls[i].id);
            }

            CKEDITOR.config.toolbar = 'AdminPanel';

            CKEDITOR.replace(arAllControls[i].id, { filebrowserImageUploadUrl: 'UploadHandler.ashx', htmlEncodeOutput: true });
        }
    }
}

