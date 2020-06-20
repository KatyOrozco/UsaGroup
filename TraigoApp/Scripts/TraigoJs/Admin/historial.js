$(document).ready(function () {
    //boton editar live
    jQuery(document.body).on('click', '.editarCotizacion', function (event) {
        var idCotizacion = $(this).attr('idCotizacion');
        $("#btnCotizacionGuardar").attr("idquotation", idCotizacion);
        $('#modalEditar').modal('show');
    });

    jQuery(document.body).on('click', '#btnCalculo', function (event) {
        $("#cajaTexto").text("");
        var nombre = $("input:checked:first").attr("nombreMuneco");
        var pesoGlobal = 0;
        var fechaVuelta = $("input:checked:first").attr("fechaLlegada");
        var precioGlobal = 0;
        var comTraigo = 0;
        var comViajero = 0;
        $("input:checked").each(function () {
            pesoGlobal += parseFloat($(this).attr("valuePeso"));
            precioGlobal += parseFloat($(this).attr("precio"));
            comTraigo += parseFloat($(this).attr("comTraigo"));
            comViajero += parseFloat($(this).attr("comViajero"));
        });

        var sumaTotal = parseFloat(comViajero + comTraigo);
        generarModalCompradorHistorial(nombre, fechaVuelta, pesoGlobal, precioGlobal, comTraigo, comViajero, sumaTotal);
    });

    function generarModalCompradorHistorial(nombre, fecha, peso, precio, comTraigo, comViajero, sumaTotal) {
        peso = parseFloat(peso).toFixed(2);
        comTraigo = parseFloat(comTraigo).toFixed(2);
        precio = parseFloat(precio).toFixed(2);
        comTraigo = parseFloat(comTraigo).toFixed(2);
        comViajero = parseFloat(comViajero).toFixed(2);
        sumaTotal = parseFloat(sumaTotal).toFixed(2);
        var nombreSolo = nombre.replace(/ .*/, '');
        var texto = "El valor total por la traída  tu paquete es: ";
        texto += "TOTAL: $" + sumaTotal + " USD. \n\n";

        $('#cajaTexto').text(texto);
        $('#modalTextoGrupo').modal('show');
    }

    $('.logout').click(function () {
        Cookies.remove('adminTraigo');
        location.reload();
    });

    $("#btnCrearComanda").click(function () {
        var email = Cookies.get('ua');
        var paqueteRequest = {
            Email: email
        }
        $.ajax({
            type: 'POST',
            url: '/api/command/newcommand',
            dataType: 'json',
            data: JSON.stringify(paqueteRequest),
            contentType: 'application/json; charset=utf-8',
            async: true,
            success: function (data) {
                alert('Paquete Creado correctamente.');

            },
            error: function (xhr) {
                alert('Problemas al crear el paquete.');
            }
        });

    });
});

