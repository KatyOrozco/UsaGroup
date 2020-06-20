$(document).ready(function () {
    traerComandas();
    traerComandasVerificadas();
    traerComandasPagadas();

    window.setInterval(function () {
        traerComandas();
    }, 100000);

    function traerComandas() {
        var comandaRequest = {

        }

        $.ajax({
            type: 'POST',
            url: '/api/command/getCommandPanel',
            dataType: 'json',
            data: JSON.stringify(comandaRequest),
            contentType: 'application/json; charset=utf-8',
            async: true,
            success: function (data) {
                $('#tablaComandas').empty();
                $.each(data, function (index, element) {
                    itemComanda(element.IdCommand, element.ValorComanda, element.Email, element.Nombre, element.Celular);
                });
            },
            error: function (xhr) {
                alert('Problemas al traer comandas.');
            }
        });
    }

    function traerComandasVerificadas() {
        var comandaRequest = {

        }

        $.ajax({
            type: 'POST',
            url: '/api/command/getCommandsVerify',
            dataType: 'json',
            data: JSON.stringify(comandaRequest),
            contentType: 'application/json; charset=utf-8',
            async: true,
            success: function (data) {
                $('#comandasVerificadas').empty();
                $.each(data, function (index, element) {
                    itemComandaVerificada(element.IdCommand, element.ValorComanda, element.Email, element.Nombre, element.Celular);
                });
            },
            error: function (xhr) {
                alert('Problemas al traer comandas.');
            }
        });
    }

    function traerComandasPagadas() {
        var comandaRequest = {

        }

        $.ajax({
            type: 'POST',
            url: '/api/command/getCommandsPaid',
            dataType: 'json',
            data: JSON.stringify(comandaRequest),
            contentType: 'application/json; charset=utf-8',
            async: true,
            success: function (data) {
                $('#comandasPagadas').empty();
                $.each(data, function (index, element) {
                    itemComandaPagada(element.IdCommand, element.ValorComanda, element.Email, element.Nombre, element.Celular);
                });
            },
            error: function (xhr) {
                alert('Problemas al traer comandas.');
            }
        });
    }

    function itemComanda(id, valor, email, nombre, celular) {
        var cotizacion = "<tr>";
        cotizacion += "<td>" + id + "</td>";
        cotizacion += "<td>" + nombre + "</td>";
        cotizacion += "<td>" + celular + "</td>";
        cotizacion += "<td>" + email + "</td>";
        cotizacion += "<td>" + valor + "</td>";
        cotizacion += "<td><a class='btn btn-primary' href='/admin/admincomanda?idcomanda=" + id + "' target='_blank'>Ver paquete</button ></td>";
        cotizacion += "</tr>";
        $('#tablaEnviadas').prepend(cotizacion);
    }

    function itemComandaVerificada(id, valor, email, nombre, celular) {
        var cotizacion = "<tr>";
        cotizacion += "<td>" + id + "</td>";
        cotizacion += "<td>" + nombre + "</td>";
        cotizacion += "<td>" + celular + "</td>";
        cotizacion += "<td>" + email + "</td>";
        cotizacion += "<td>" + valor + "</td>";
        cotizacion += "<td><a class='btn btn-primary' href='/admin/admincomanda?idcomanda=" + id + "' target='_blank'>Ver comanda</button ></td>";
        cotizacion += "</tr>";
        $('#comandasVerificadas').prepend(cotizacion);
    }

    function itemComandaPagada(id, valor, email, nombre, celular) {
        var cotizacion = "<tr>";
        cotizacion += "<td>" + id + "</td>";
        cotizacion += "<td>" + nombre + "</td>";
        cotizacion += "<td>" + celular + "</td>";
        cotizacion += "<td>" + email + "</td>";
        cotizacion += "<td>" + valor + "</td>";
        cotizacion += "<td><a class='btn btn-primary' href='/admin/admincomanda?idcomanda=" + id + "' target='_blank'>Ver paquete</button ></td>";
        cotizacion += "</tr>";
        $('#comandasPagadas').prepend(cotizacion);
    }
});