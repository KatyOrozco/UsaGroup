$(document).ready(function () {

    $("#signup").click(function () {
        var nombreUser = $("#nombreForm").val();
        var emailUser = $("#emailForm").val();
        var cedulaUser = $("#cedulaForm").val();
        var celularecUser = $("#celularForm").val();
        var ciudadId = $("#ciudadForm").val();
        var password = $("#passwordForm").val();
        if (password != "" && emailUser != "") {
            var usuarioRequest = {
                Nombre: nombreUser,
                Email: emailUser,
                Cedula: cedulaUser,
                CelularEc: celularecUser,
                IdCiudad: ciudadId,
                Clave: password
            }

            $.ajax({
                type: 'POST',
                url: '/api/user/CreateUser',
                dataType: 'json',
                data: JSON.stringify(usuarioRequest),
                contentType: 'application/json; charset=utf-8',
                async: true,
                success: function (data) {
                    if (data.NewUser === false) {
                        alert('Al parecer el usuario ya existe. Intente inciando sesión.');
                    } else {
                        Cookies.set('ua', emailUser);
                        window.location.replace("/dashboard");
                    }
                },
                error: function (xhr) {
                    alert('Problemas al crear el usuario. Por favor intente crear el usuario nuevamente.');
                }
            });
        } else {
            alert("Por favor llene todos los campos.");
        }
      
    });
});