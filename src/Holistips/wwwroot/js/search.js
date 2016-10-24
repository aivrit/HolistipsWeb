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