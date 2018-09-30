
function SelectRow(objCheckBox,strID)
    {
    
    
    
    var hdnSelectedIDs = GetHiddenField();
    
        var strValue = hdnSelectedIDs.value;
      
        var row = objCheckBox.parentNode.parentNode;
      
        if(objCheckBox.checked == true)
        {
            
            strValue = strValue + strID + ",";
            row.className  = "gridrowselectedcheck";
        }
        else
        {
            var arIDs = strValue.split(",");
            strValue = "";
            var i = 0;
            for(i=0; i<arIDs.length-1;i++)
            {
                if(arIDs[i] != strID)
                {
                    strValue = strValue + arIDs[i] + ",";
                }
            }
            row.className  = "gridrow";
        }
        //alert(strValue);
        hdnSelectedIDs.value = strValue;
    }
    
    function SelectAll(objCheckBox)
    {
    var hdnSelectedIDs = GetHiddenField();
    var strValue= "";
    hdnSelectedIDs.value = ""
    
    
     var arAllControls = document.getElementsByTagName("input");
     for (var i = 0; i < arAllControls.length; i++) {
        if (arAllControls[i].type == "checkbox") {
            if(arAllControls[i].id.indexOf("chkSelectRow_")>0)
            {
                var row = arAllControls[i].parentNode.parentNode;
                if(objCheckBox.checked == true){
                    arAllControls[i].checked = true;
                    strValue = strValue +  arAllControls[i].id.split("chkSelectRow_")[1].split("_")[0] + ",";
                    row.className  = "gridrowselectedcheck";
                }
                else{
                    arAllControls[i].checked = false;
                    row.className  = "gridrow";
                }
            }
        }
     }
     //alert(strValue);
    hdnSelectedIDs.value = strValue;
    }
    
    
    function GetHiddenField()
    {
    var arAllControls = document.getElementsByTagName("input");
     for (var i = 0; i < arAllControls.length; i++) {
        if (arAllControls[i].type == "hidden") {
            if(arAllControls[i].id.indexOf("hdnSelectedIDs")>0)
            {
                return arAllControls[i];
                i = arAllControls.length;
            }
        }
     }
    }