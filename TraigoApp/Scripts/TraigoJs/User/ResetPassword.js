$(document).ready(function () {
    $('#reinicio').click(function () {
        var correo = $("#correorecuperar").val();

        var usuarioRequest = {
            Email: correo
        }

        $.ajax({
            type: 'POST',
            url: '/api/user/resetPassword',
            dataType: 'json',
            data: JSON.stringify(usuarioRequest),
            contentType: 'application/json; charset=utf-8',
            async: true,
            success: function (data) {
                if (data.Ingresa === true) {
                    alert("Correo de recuperación enviado.");
                } else {
                    alert('El usuario no existe. Por favor intente creando una cuenta.');
                }
            },
            error: function (xhr) {
                alert('Usuario o contraseña incorrecto. Código de error 45.');
            }
        });
    });
});