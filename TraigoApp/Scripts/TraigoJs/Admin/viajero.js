$(document).ready(function () {
    $(function () {
        $(".fecha").datepicker();
    });
 
    $('#viajesRow').hide();
    $('#datosViajeroRow').hide();
    $('#tablaDirecciones').hide();
    $('#cuadroForm').hide();
    $('#crearDireccion').click(function () {
        $('#modalDireccion').modal('show');
    });

    $("#enviarConsulta").click(function () {
        var emailUser = $("#emailConsulta").val();
        emailComun = emailUser;
        $("#cuadroCliente").empty();
        if (enviarConsulta(emailComun)) {
            //armo form para crear user
            $('#cuadroCliente').hide();
            $('#cuadroForm').show('slow');
            $("#emailForm").val($('#emailConsulta').val());
        } else {
            //armo tabla con datos de user
            $('#cuadroCliente').show();
            $('#cuadroForm').hide();
            $('#datosViajeroRow').show();
            armarTabla(nombreUserEnviar, emailComun, cedulaUser, celularUser, ciudadUser);
            //funcion traer direcciones
            traerDirecciones();
        }
    });

    //crear dirección
    $("#creoDireccion").click(function () {
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

    //btn crear viaje funcion #creoViaje
    $("#creoViaje").click(function () {
        var fechaIda = $("#fechaIda").val();
        var fechaVuelta = $("#fechaVuelta").val();
        var itemDesde = $("#itemDesde").val();
        var itemHasta = $("#itemHasta").val();
        var itemReserva = $("#itemReserva").val();
        var aerolineaId = $("#itemDireccion").val();
        var itemNotas = $("#itemNotas").val();
        var direccionId = $(this).attr("idDirBtnCrear");
        //status 1 = pendiente
        var statusId = 1;

        var viajeRequest = {
            Email: emailComun,
            Vuelta: fechaVuelta,
            Ida: fechaIda,
            ItemDesde: itemDesde,
            ItemHasta: itemHasta,
            NotasImportantes: itemNotas,
            UsAddressId: direccionId,
            CodigoReserva: itemReserva,
            AirlineId: aerolineaId,
            StatusId: statusId
        }

        $.ajax({
            type: 'POST',
            url: '/api/trip/newtrip',
            dataType: 'json',
            data: JSON.stringify(viajeRequest),
            contentType: 'application/json; charset=utf-8',
            async: true,
            success: function (data) {
                $('#modalViaje').modal('hide');
                //llamar a viajes function
                traerViajes();
            },
            error: function (xhr) {
                alert('Problemas al crear el viaje. ');
            }
        });
    });

    //boton viaje viajeDirBtn
    jQuery(document.body).on('click', '.viajeDirBtn', function (event) {
        var iddir = $(this).attr('iddireccion');
        //pasar atributo al modal viaje
        $('#creoViaje').attr("idDirBtnCrear", iddir);
        //abrir modal
        $('#modalViaje').modal('show');
    });

    //function trae direcciones
    function traerDirecciones() {
        $('#tablaDireccion').empty();
        var usuarioRequest = {
            Email: emailComun
        }

        $.ajax({
            type: 'POST',
            url: '/api/usaddress/GetAddressByEmail',
            dataType: 'json',
            data: JSON.stringify(usuarioRequest),
            contentType: 'application/json; charset=utf-8',
            async: true,
            success: function (data) {
                var ArrayContent = data.length;
                if (ArrayContent > 0) {
                    //armo tabla direcciones
                    $('#tablaDirecciones').show();
                    $.each(data, function (index, element) {
                        armarDirecciones(element.Direccion, element.Ciudad, element.EstadoNombre, element.Zipcode, element.Nombre, element.Telefono, element.DireccionId);
                    });
                    //llamar a viajes function
                    traerViajes();
                } else {
                    $('#tablaDirecciones').hide();
                }
            },
            error: function (xhr) {
                alert('Problemas al traer las direcciones. ');
            }
        });
    }

    //function trae viajes en caso de haberlo
    function traerViajes() {
        $('#tablaViaje').empty();
        var viajeRequest = {
            Email: emailComun
        }

        $.ajax({
            type: 'POST',
            url: '/api/trip/GetActiveTripsByEmail',
            dataType: 'json',
            data: JSON.stringify(viajeRequest),
            contentType: 'application/json; charset=utf-8',
            async: true,
            success: function (data) {
                var ArrayContent = data.length;
                if (ArrayContent > 0) {
                    //armo tabla viajes
                    $('#viajesRow').show();
                    $('#tablaViaje').show();
                    $.each(data, function (index, element) {
                        armarViajes(element.Ida, element.Vuelta, element.FechaDesde, element.FechaHasta, element.Estado, element.Reserva, element.Aerolinea, element.NotasImportantes);
                    });

                } else {
                    $('#viajesRow').hide();
                    $('#tablaViaje').hide();
                }
            },
            error: function (xhr) {
                alert('Problemas al traer los viajes. ');
            }
        });
    }

    function armarDirecciones(direccion, ciudad, estado, zipcode, nombre, telefono, idDireccion) {
        var dir = "<tr>";
        dir += "<td>"+ direccion+"</td>";
        dir += "<td>" + ciudad + "</td>";
        dir += "<td>" + estado + "</td>";
        dir += "<td>" + zipcode + "</td>";
        dir += "<td>" + nombre + "</td>";
        dir += "<td>" + telefono + "</td>";
        dir += "<td><button class='btn viajeDirBtn' idDireccion='" + idDireccion + "'>Crear viaje</button >";
        dir += "</tr>";
        $('#tablaDireccion').prepend(dir);
    }

    function armarViajes(ida, vuelta, desde, hasta, estado, reserva, aerolinea, notas) {
        var trip = "<tr>";
        trip += "<td>" + ida + "</td>";
        trip += "<td>" + vuelta + "</td>";
        trip += "<td>" + desde + "</td>";
        trip += "<td>" + hasta + "</td>";
        trip += "<td>" + estado + "</td>";
        trip += "<td>" + reserva + "</td>";
        trip += "<td>" + aerolinea + "</td>";
        trip += "<td>" + notas + "</td>";
        trip += "</tr>";
        $('#tablaViaje').prepend(trip);
    }

});