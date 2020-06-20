$(document).ready(function () {

    //crear dirección
    $("#nuevaDireccion").click(function () {
        var direccion = $("#campoDireccion").val();
        var ciudad = $("#campoCiudad").val();
        var zipcode = $("#campoZipcode").val();
        var nombre = $("#campoNombre").val();
        var telefono = $("#campoTelefono").val();
        var estado = $("#campoEstado").val();

        var usuarioRequest = {
            Email: emailComun,
            Direccion: direccion,
            Ciudad: ciudad,
            Zipcode: zipcode,
            Nombre: nombre,
            Telefono: telefono,
            Estado: estado
        }

        $.ajax({
            type: 'POST',
            url: '/api/usaddress/NewUsAddress',
            dataType: 'json',
            data: JSON.stringify(usuarioRequest),
            contentType: 'application/json; charset=utf-8',
            async: true,
            success: function (data) {
                traerDirecciones();
                $('#modalDireccion').modal('hide');
            },
            error: function (xhr) {
                alert('Problemas al crear la dirección. ');
            }
        });
    });

});