$(document).ready(function () {
    var idComanda = getUrlParameter('idcomanda');
    traerDatosUser();
    traerItems();

    function traerDatosUser() {

        var comandaRequest = {
            IdComanda: idComanda
        }

        $.ajax({
            type: 'POST',
            url: '/api/command/getCommandById',
            dataType: 'json',
            data: JSON.stringify(comandaRequest),
            contentType: 'application/json; charset=utf-8',
            async: true,
            success: function (data) {
                $('#nombreUser').text(data.Nombre);
                $('#emailUser').text(data.Email);
                $('#celularUser').text(data.Celular);

            },
            error: function (xhr) {
                alert('Problemas al traer comandas.');
            }
        });
    }
});