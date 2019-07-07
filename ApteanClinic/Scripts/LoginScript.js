$(document).ready(function () {
    resizeWindow();
    $(window).resize(function () {
        resizeWindow();
    });
});
function resizeWindow() {
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
    else if (window.innerWidth > 478) {
        $('body').css('zoom', '75%');
        if (window.innerWidth >= 1920)
            $('body').css('zoom', '90%');
    }
}