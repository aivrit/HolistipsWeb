function hashtag() {
    
    if (document.getElementById("SearchQuery").value[0] === '#')
    {
        document.getElementById("SearchQuery").style.color = 'blue';
    }
    else
    {
        document.getElementById("SearchQuery").style.color = 'black';
    }
   
}


    function stats() {
        if ($("#SearchStats").is(':checked') && $("#SearchStats").val() === 'on') {
            document.getElementById("SearchStatsGo").disabled = false;
        }
        else
        {
            document.getElementById("SearchStatsGo").disabled = true;
        }
}
