$(document).ready(function () {
    if ($('#role').html() == 'Patient') {
        $('#dashboardPanel').remove();
        $('#patientView').slideDown(1500);
    }
    else {
        $('#patientView').remove();
    }
    $('#element1').mouseenter(function () {
        $(this).animate({ marginTop: "-20px" });
    });
    $('#element1').mouseleave(function () {
        $(this).animate({ marginTop: "0px" });
    });

    $('#element2').mouseenter(function () {
        $(this).animate({ marginTop: "-20px" });
    });
    $('#element2').mouseleave(function () {
        $(this).animate({ marginTop: "0px" });
    });

    $('#element3').mouseenter(function () {
        $(this).animate({ marginTop: "-20px" });
    });
    $('#element3').mouseleave(function () {
        $(this).animate({ marginTop: "0px" });
    });

    $('#element4').mouseenter(function () {
        $(this).animate({ marginTop: "-20px" });
    });
    $('#element4').mouseleave(function () {
        $(this).animate({ marginTop: "0px" });
    });
    let time = new Date().getHours();
    if (time >= 0 && time < 12)
        $('#display-name').html('Good Morning, ' + $('#display-name').html());
    else if (time >= 12 && time < 17)
        $('#display-name').html('Good Afternoon, ' + $('#display-name').html());
    else
        $('#display-name').html('Good Evening, ' + $('#display-name').html());
    let speed = 1500;
    $('#display-name').delay(500).fadeIn(speed).delay(1000).fadeOut(speed).fadeIn(speed);
    setTimeout(function () { $('#display-name').html('Welcome to Aptean Clinic'); }, 4500);
    for (let i = 1; i <= 4; i++) {
        $('#element' + i).delay(400 * (i - 1)).fadeIn(800);
    }
    $('#role').remove();
});