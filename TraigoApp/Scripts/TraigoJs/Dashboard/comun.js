$(document).ready(function () {
    $(".loguot").click(function () {
        Cookies.remove('ua');
        Cookies.remove('command');
        window.location.replace("/login");
    });
});