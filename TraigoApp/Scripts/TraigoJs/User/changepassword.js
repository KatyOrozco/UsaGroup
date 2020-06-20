$(document).ready(function () {
    $('#changepass').click(function () {
        var email = getUrlParameter('user');
        var contra1 = $("#password1").val();
        var contra2 = $("#password2").val();
        if (contra1 == contra2) {
            //enviar al api que cambia la contraseña
            var usuarioRequest = {
                Email: email,
                Clave: contra1
            }

            $.ajax({
                type: 'POST',
                url: '/api/user/changePassword',
                dataType: 'json',
                data: JSON.stringify(usuarioRequest),
                contentType: 'application/json; charset=utf-8',
                async: true,
                success: function (data) {
                    window.location.replace("/login");
                },
                error: function (xhr) {
                    alert('Problemas al cambiar la clave. Error 48.');
                }
            });

        } else {
            alert("Las contraseñas no coinciden.");
        }
    });

    function getUrlParameter(sParam) {
        var sPageURL = window.location.search.substring(1);
        var sURLVariables = sPageURL.split('&');
        for (var i = 0; i < sURLVariables.length; i++) {
            var sParameterName = sURLVariables[i].split('=');
            if (sParameterName[0] == sParam) {
                return sParameterName[1];
            }
        }
    }
});