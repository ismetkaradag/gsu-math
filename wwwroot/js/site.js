
function is_active_toggle_blog(id){
    var as = "/panel/toggle_is_active/"+id;
    $.post(as,function(data,status){
        if(data == true){
            document.getElementById(id).parentElement.parentElement.classList.toggle("bg-success")
            document.getElementById(id).parentElement.parentElement.classList.toggle("bg-light")
        }
    })
}