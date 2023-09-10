$(document).ready(function(){

    $("#submit-form").on("click", function(){
        $(this).attr("disabled" , true);
        $(this).children(".indicator-label").addClass("d-none");
        $(this).children(".indicator-progress").addClass("d-block");
    })

});