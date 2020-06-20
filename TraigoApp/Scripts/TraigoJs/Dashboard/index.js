$(document).ready(function () {
    $("#rowCotizacion").hide();
    $("#nuevaComanda").hide();

    var user = Cookies.get('ua');
    var comisionTotal = "";
    var comisionTraigo = "";
    var comisionViajero = "";
    comprobarComanda();


    function comprobarComanda() {
        var usuarioRequest = {
            Email: user
        }

        $.ajax({
            type: 'POST',
            url: '/api/command/getcommand',
            dataType: 'json',
            data: JSON.stringify(usuarioRequest),
            contentType: 'application/json; charset=utf-8',
            async: true,
            success: function (data) {
                if (data.Respuesta === false) {
                    //si hay command
                    Cookies.set("command", data.IdCommand);
                    $("#rowCotizacion").show();
                    traerItems();
                } else {
                    //no hay commands
                    $("#nuevaComanda").show();
                }
            },
            error: function (xhr) {
                alert('Por favor vuelva a cargar la página.');
            }
        });
    }

    function addItem(idCotizacion) {
        var idComanda = Cookies.get('command');

        var usuarioRequest = {
            IdCommand: idComanda,
            IdQuotation: idCotizacion
        }

        $.ajax({
            type: 'POST',
            url: '/api/item/newItemInCommand',
            dataType: 'json',
            data: JSON.stringify(usuarioRequest),
            contentType: 'application/json; charset=utf-8',
            async: true,
            success: function (data) {
                traerItems();
            },
            error: function (xhr) {
                alert('Por favor vuelva a cargar la página.');
            }
        });
    }

    $(document).on("click", ".btnItem", function () {
        var cotizacion = $(this).attr('idcotizacion');
        addItem(cotizacion);
    });

    $(document).on("click", ".btnDelete", function () {
        var item = $(this).attr('iditem');
        deleteItem(item);
    });

    function deleteItem(itemId) {
        var usuarioRequest = {
            IdItem: itemId
        }

        $.ajax({
            type: 'POST',
            url: '/api/item/deteleItem',
            dataType: 'json',
            data: JSON.stringify(usuarioRequest),
            contentType: 'application/json; charset=utf-8',
            async: true,
            success: function (data) {
                traerItems();
            },
            error: function (xhr) {
                alert('Por favor vuelva a cargar la página.');
            }
        });
    }

    $("#nuevaComanda").click(function () {
        //api que crea un nuevo registro en commands

        var usuarioRequest = {
            Email: user
        }

        $.ajax({
            type: 'POST',
            url: '/api/command/newCommand',
            dataType: 'json',
            data: JSON.stringify(usuarioRequest),
            contentType: 'application/json; charset=utf-8',
            async: true,
            success: function (data) {
                if (data.Respuesta === true) {
                    $("#nuevaComanda").hide();
                    $("#rowCotizacion").show();
                    Cookies.set("command", data.IdCommand);

                }
            },
            error: function (xhr) {
                alert('Por favor vuelva a cargar la página.');
            }
        });

        //show campos cotizadar
    });



    $("#historial").click(function () {
        traerCotizaciones(user);
    });

    function traerCotizaciones(email) {
        var usuarioRequest = {
            Email: email
        }

        $.ajax({
            type: 'POST',
            url: '/api/quotation/GetQuotationByUser',
            dataType: 'json',
            data: JSON.stringify(usuarioRequest),
            contentType: 'application/json; charset=utf-8',
            async: true,
            success: function (data) {
                $.each(data, function (index, element) {
                    tablaHistoria(element.URL, element.NombreItem, element.Estado, element.Precio, element.ComisionTotal, element.ComisionViajero, element.ComisionTraigo, element.Peso, element.TextoResultado, element.FechaVuelta, element.Id);
                });
                $('#historicoModal').modal('show');
            },
            error: function (xhr) {
                alert('Problemas al traer las cotizaciones. ');
            }
        });
    }

    function traerItems() {
        var idComanda = Cookies.get('command');

        var usuarioRequest = {
            IdCommand: idComanda
        }

        $.ajax({
            type: 'POST',
            url: '/api/item/GetItemsByCommand',
            dataType: 'json',
            data: JSON.stringify(usuarioRequest),
            contentType: 'application/json; charset=utf-8',
            async: true,
            success: function (data) {
                var comTotal = 0;
                var comTraigo = 0;
                var comViajero = 0;
                var numItems = 0;
                $('#tablaPreview').empty();
                $.each(data, function (index, element) {
                    tablaPrevia(element.URL, element.NombreItem, element.Precio, element.ComisionTotal, element.ComisionViajero, element.ComisionTraigoIva, element.Peso, element.Id);
                    comTotal += parseFloat(element.ComisionTotal);
                    comViajero += parseFloat(element.ComisionViajero);
                    comTraigo += parseFloat(element.ComisionTraigoIva);
                    numItems++;
                });

                $('#contenedorTabla').show();
                if (numItems == 0) {
                    $('#contenedorTabla').hide();
                }

                if (comTraigo < 5.60) {
                    comTraigo = 5.60;
                }

                if (comViajero < 6) {
                    comViajero = 6;
                }

                comTotal = comTraigo + comViajero;
                $("#comTraigo").text(comTraigo.toFixed(2));
                $("#comViajero").text(comViajero.toFixed(2));
                $("#totalComision").text(comTotal.toFixed(2));
                comisionTotal = comTotal;
                comisionTraigo = comTraigo;
                comisionViajero = comViajero;
            },
            error: function (xhr) {
                alert('Problemas al traer los items.');
            }
        });
    }

    $("#cotizacionNueva").click(function () {
        $('#modalCotizacion').modal('show');
    });

    $("#enviarPeticion").click(function () {
        //var valorComanda = $("#totalComision").text();
        var idComanda = Cookies.get('command');
        var usuarioRequest = {
            Estado: 2,
            ValorComanda: comisionTotal,
            IdComanda: idComanda, 
            ValorTraigo: comisionTraigo,
            ValorViajero: comisionViajero
        }

        $.ajax({
            type: 'POST',
            url: '/api/command/updateCommand',
            dataType: 'json',
            data: JSON.stringify(usuarioRequest),
            contentType: 'application/json; charset=utf-8',
            async: true,
            success: function (data) {
                alert("Cotización recibida. En este momento una operadora verificará la información ingresada. En un lapso de 6 horas recibirás un mail con la información detallada para continuar con el proceso de compra.");
                $("#nuevaComanda").show();
                $("#rowCotizacion").hide();
                Cookies.remove('command');
            },
            error: function (xhr) {
                alert('Problemas al enviar la petición. Por favor intente nuevamente.');
            }
        });
    });

    //BotonCrearCotizacion
    $("#ingresarCotizacion").click(function () {
        var url = $('#campoUrl').val();
        var nombreItem = $('#campoNombreitem').val();
        var precio = $('#campoPrecio').val();
        var email = user;
        var categoria = $('#campoCategoria').val();
        var peso = $('#campoPeso').val();
        var largo = $('#campoLargo').val();
        var ancho = $('#campoAncho').val();
        var alto = $('#campoAlto').val();
        var urlok = 'amazon.';
        if (url.includes(urlok)) {
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
                    addItem(data.Id);
                    traerItems();
                },
                error: function (xhr) {
                    alert('Problemas al ingresar la cotización. Intente nuevamente. Código 50');
                }
            });
        } else {
            alert('Por favor ingrese un item de amazon.com');
        }
        
    });

    function tablaPrevia(url, nombre, precio, comisionTotal, comisionViajero, comisionTraigo, pesoItem, id) {
        var cotizacion = "<tr>";
        cotizacion += "<td><a href='" + url + "' target='_blank' class='itemattr'>" + nombre + "</a></td>";
        cotizacion += "<td title='Peso' class='pesoattr'>" + pesoItem + " libras</td>";
        cotizacion += "<td title='Precio' class='precioattr'>" + precio + " USD</td>";
        cotizacion += "<td title='Comisión Traigo sin iva' class='comtraigoattr'>" + comisionTraigo + " USD</td>";
        cotizacion += "<td title='Comisión Viajero' class='comviajeroattr'>" + comisionViajero + " USD</td>";
        cotizacion += "<td title='Comisión Total' class='comviajeroattr' comision='" + comisionTotal+"'>$" + comisionTotal + " USD</td>";
        cotizacion += "<td><button class='btn btn-danger btnDelete' iditem='" + id + "' >Eliminar <i class='fas fa-minus-circle'></i></button ></td>";
        cotizacion += "</tr>";
        $('#tablaPreview').prepend(cotizacion);
    } 

    function tablaHistoria(url, nombre, estado, precio, comisionTotal, comisionViajero, comisionTraigo, pesoItem, textoResultado, fechaVuelta, id) {
        var cotizacion = "<tr idrow='" + id + "'>";
        cotizacion += "<td><a href='" + url + "' target='_blank' class='itemattr'>" + nombre + "</a></td>";
        cotizacion += "<td title='Peso' class='pesoattr'>" + pesoItem + " libras</td>";
        cotizacion += "<td title='Precio' class='precioattr'>" + precio + " USD</td>";
        cotizacion += "<td title='Comisión Traigo sin iva' class='comtraigoattr'>$" + comisionTraigo + " USD</td>";
        cotizacion += "<td title='Comisión Viajero' class='comviajeroattr'>" + comisionViajero + " USD</td>";
        cotizacion += "<td title='Comisión Viajero' class='comviajeroattr'>" + comisionTotal + " USD</td>";
        cotizacion += "<td><button class='btn btnItem' idcotizacion='" + id + "' >Agregar <i class='fas fa-plus-circle'></i></button ></td>";
        cotizacion += "</tr>";
        $('#tablaHistoria').prepend(cotizacion);
    }
});
