$(document).ready(function () {
    $('#myTable').DataTable();
 
});

$('input[type=checkbox]').on('click', function () {
    var isChecked = $(this).is(':checked');
    
    $(this).parent().parent().find('#yourText').attr('readonly', !isChecked);
    $(this).parent().parent().find('#yourText').val('0');
   

  

}); 


$('input[type=checkbox]').on('click', function () {
    var isChecked = $(this).is(':checked');
    if (isChecked) {
        $(this).parent().parent().find('#yourText').css("backgroundColor", "white");
    }
    else {

        $(this).parent().parent().find('#yourText').css("backgroundColor", "lightgray");
    }


});
function disableButton(btn) {
    document.getElementById(btn.id).disabled = true;
    alert("Button has been disabled.");
}