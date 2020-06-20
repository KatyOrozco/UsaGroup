

$(document).ready(function () {



    //BotonCrearCotizacion
    $("#nuevaCotizacion").click(function () {
        var email = Cookies.get('ua');
        var url = $('#campoUrl').val();
        var nombreItem = $('#campoNombreitem').val();
        var precio = $('#campoPrecio').val();
        var categoria = $('#campoCategoria').val();
        var peso = $('#campoPeso').val();
        var largo = $('#campoLargo').val();
        var ancho = $('#campoAncho').val();
        var alto = $('#campoAlto').val();
        var cotizacionRequest = {
            URL: url,
            NombreItem: nombreItem,
            Precio: precio,
            Email: email,
            CategoryId: categoria,
            Peso: peso,
            Largo: largo,
            Ancho: ancho,
            Alto: alto
        }
        $.ajax({
            type: 'POST',
            url: '/api/quotation/NewQuotation',
            dataType: 'json',
            data: JSON.stringify(cotizacionRequest),
            contentType: 'application/json; charset=utf-8',
            async: true,
            success: function (data) {
                $('#modalCotizacion').modal('hide');
                ingresarCotizacionTabla(data.URL, data.NombreItem, data.Precio, data.ComisionTotal, data.ComisionViajero, data.ComisionTraigo, data.Peso, data.TextoResultado, data.FechaVuelta, data.Id);
            },
            error: function (xhr) {
                alert('Problemas al ingresar la cotización.');
            }
        });
    });
    $("#enviarConsultaCotizacion").click(function () {
        $('#cotizacionRow').hide();
        var emailUser = $("#emailConsulta").val();
        emailComun = emailUser;
        $("#cuadroCliente").empty();
        $("#tablaCotizacion").empty();

        if (enviarConsulta(emailComun)) {
            //armo form para crear user
            $('#cuadroCliente').hide();
            $('#cuadroForm').show('slow');
            $("#emailForm").val($('#emailConsulta').val());
        } else {
            //armo tabla con datos de user
            $('#cuadroCliente').show();
            $('#cuadroForm').hide();
            armarTabla(nombreUserEnviar, emailComun, cedulaUser, celularUser, ciudadUser);

            //funcion traer cotizaciones
            $('#cotizacionRow').show();
            traerCotizaciones(emailComun);
        }
    });


    //btnCalculo
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

        var sumaTotal = parseFloat(comViajero + comTraigo).toFixed(2);
        generarModalComprador(nombre, fechaVuelta, pesoGlobal, precioGlobal, comTraigo, comViajero, sumaTotal);
    });

    function generarModalComprador(nombre, fecha, peso, precio, comTraigo, comViajero, sumaTotal) {
        peso = parseFloat(peso).toFixed(2);
        comTraigo = parseFloat(comTraigo).toFixed(2);
        precio = parseFloat(precio).toFixed(2);
        comTraigo = parseFloat(comTraigo).toFixed(2);
        comViajero = parseFloat(comViajero).toFixed(2);
        sumaTotal = parseFloat(sumaTotal).toFixed(2);
        var nombreSolo1 = nombreUserEnviar.replace(/ .*/, '');
        var texto = "la cotización agrupada del cliente" + nombreSolo1 + "\n\n";
        //texto += "El viajero llega el " + fecha + ". \n";
        texto += "Precio Productos : $" + precio + " USD.\n";
        texto += "Peso: " + peso + " lbs.\n\n";
        texto += "Costos:\n";
        texto += "Servicio USA Group: $" + comTraigo + " USD. \n";
        texto += "Viajero: $" + comViajero + " USD. \n\n";
        texto += "TOTAL: $" + sumaTotal + " USD. \n\n";
        texto += "Saludos.";
        $('#cajaTexto').text(texto);
        $('#modalTextoGrupo').modal('show');
    }




});
