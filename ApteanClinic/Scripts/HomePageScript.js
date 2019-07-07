$(document).ready(function () {
    resizeWindow();
    $(window).resize(function () {
        resizeWindow();
    });
    switch ($('#layoutRole').html()) {
        case "Patient":
            {
                for (let i = 0; i < 6; i++)
                    $('#menu').children('li').eq(0).remove();
                $('#menu').children('li').eq(3).remove();
            }
            break;
        case "Doctor":
            {
                for (let i = 0; i < 4; i++)
                    $('#menu').children('li').eq(0).remove();
                $('#menu').children('li').eq(1).remove();
                for (let i = 0; i < 2; i++)
                    $('#menu').children('li').eq(2).remove();
                $('#menu').children('li').eq(3).remove();
            }
            break;
        case "Admin":
            {
                for (let i = 0; i < 4; i++)
                    $('#menu').children('li').eq(6).remove();
            }
            break;
        case "Nurse":
            {
                $('#menu').children('li').eq(1).children('ul').children('li').eq(1).remove();
                $('#menu').children('li').eq(1).children('ul').children('li').eq(1).remove();
                $('#menu').children('li').eq(3).remove();
                $('#menu').children('li').eq(4).remove();
            }
            break;
    }
    $('#layoutRole').remove();
});
function resizeWindow() {
    if (window.innerWidth <= 1280) {
        $('body').css('zoom', '65%');
        if (window.innerWidth <= 478) {
            $('body').css('zoom', '55%');
            if (window.innerWidth <= 353) {
                $('body').css('zoom', '45%');
                if (window.innerWidth <= 288)
                    $('body').css('zoom', '35%');
                if (window.innerWidth <= 225)
                    $('body').css('zoom', '25%');
                if (window.innerWidth <= 161)
                    $('body').css('zoom', '15%');
            }
        }
    }
    else if (window.innerWidth > 478) {
        $('body').css('zoom', '75%');
    }
}