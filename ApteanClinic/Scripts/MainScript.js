$(document).ready(function () {
    $('#sidenav').hide();
    let sidenavtoggler = 0;
    $("#sidenavButton").click(function () {
        sidenavtoggler = toggleSideNav(sidenavtoggler);
    });
    $('#manageusers').click(function () {
        $('#innerList1').slideToggle(300);
    });
    $('#manageappointments').click(function () {
        $('#innerList2').slideToggle(300);
    });
    $('#viewDisplayer').click(function () {
        if (sidenavtoggler === 1)
            sidenavtoggler = toggleSideNav(sidenavtoggler);
    });
});

function toggleSideNav(sidenavtoggler) {
    let speed = 400;
    if (sidenavtoggler === 0) {
        sidenavtoggler = 1;
        $('#sidenav').show(0).animate({ left: '440px' }, speed);
        $('#helperDiv1').hide(speed);
        $('#helperDiv2').hide(speed);
        $('#helperDiv3').show(speed);
        $('#sidenavButton').html("&#10006").animate({ backgroundColor: '#2A3F54', color: 'white' });
        $('#top-icons').animate({ backgroundColor: "#2A3F54" }, speed);
        $('#logo').animate({ color: "white" }, speed);
    }
    else {
        sidenavtoggler = 0;
        $('#sidenav').animate({ left: '-440px' }, speed).hide(100);
        $('#helperDiv1').show(speed);
        $('#helperDiv2').show(speed);
        $('#helperDiv3').hide(speed);
        $('#sidenavButton').html("&#9776").animate({backgroundColor: '#EAEAEA', color: '#2A3F54'});
        $('#top-icons').animate({ backgroundColor: "#EAEAEA" }, speed);
        $('#logo').animate({ color: '#2A3F54' });
    }
    return sidenavtoggler;
}