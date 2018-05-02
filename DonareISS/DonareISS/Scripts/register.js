$(document).ready(function () {
    //called when key is pressed in textbox
    $("#quantity").keypress(function (e) {
        //if the letter is not digit then display error and don't type anything
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            //display error message
            //$("#errmsg").html("Digits Only").show().fadeOut("slow");
            return false;
        }
    });
    $('#pwd, #confirm_pwd').on('keyup', function () {
        if ($('#pwd').val() == $('#confirm_pwd').val()) {
            $('#message').html('').css('color', 'green');
        } else
            $('#message').html('Parola gresita!').css('color', 'red');
    });
});

