$(document).ready(function () {
    $("#login").click(function () {
        var emailUser = $("#emailForm").val();
        var password = $("#passwordForm").val();
        if (password != "" && emailUser != "") {
            var usuarioRequest = {
                Email: emailUser,
                Clave: password
            }

            $.ajax({
                type: 'POST',
                url: '/api/user/Login',
                dataType: 'json',
                data: JSON.stringify(usuarioRequest),
                contentType: 'application/json; charset=utf-8',
                async: true,
                success: function (data) {
                    if (data.Ingresa === true) {
                        Cookies.set('ua', emailUser);
                        window.location.replace("/dashboard");
                    } else {
                    }
                },
                error: function (xhr) {
                    alert('Usuario o contraseña incorrecto. Código de error 35');
                }
            });
        } else {
            alert("Por favor llene todos los campos.");
        }
    });
});