var newUser = false;
var nombreUserEnviar = "";
var cedulaUser = "";
var celularUser = "";
var ciudadUser = "";
var ciudadIdUser = 0;
var emailComun = "";

var comisionTotal = "";
var comisionTraigo = "";
var comisionViajero = "";

function editarCotizacion(url, nombreItem, precio, email, categoria, peso, largo, ancho, alto, id, estado, viaje, tracking) {
    var value = false;
    var edicionRequest = {
        URL: url,
        Id: id,
        NombreItem: nombreItem,
        Precio: precio,
        Email: email,
        CategoryId: categoria,
        Peso: peso,
        Largo: largo,
        Ancho: ancho,
        TrackingNumber: tracking,
        EstadoCotizacion: estado,
        ViajeId: viaje,
        Alto: alto
    }

    $.ajax({
        type: 'POST',
        url: '/api/quotation/EditQuotationAll',
        dataType: 'json',
        data: JSON.stringify(edicionRequest),
        contentType: 'application/json; charset=utf-8',
        async: false,
        success: function (data) {
            value = true;
            var especifico = $('tr[idrow="' + id + '"]');
            especifico.addClass('green');
            setTimeout(function () {
                especifico.removeClass('green');
            }, 700)
            especifico.find('.precioattr').text(data.Precio);
            especifico.find('.estadoattr').text(data.Estado);
            especifico.find('.comtraigoattr').text(data.ComisionTraigo);
            especifico.find('.comviajeroattr').text(data.ComisionViajero);
            especifico.find('.vueltaattr').text(data.FechaVuelta);
            especifico.find('.pesoattr').text(data.Peso);
            especifico.find('.itemattr').text(data.NombreItem);
            especifico.find('.itemattr').attr('href', data.URL);

           //values del check
            var especifico2 = $('tr[idrow="' + id + '"] td');
            especifico2.find('.checkIn').attr('precio', data.Precio);
            especifico2.find('.checkIn').attr('valuepeso', data.Peso);
            especifico2.find('.checkIn').attr('fechallegada', data.FechaVuelta);
            especifico2.find('.checkIn').attr('comviajero', data.ComisionViajero);
            especifico2.find('.checkIn').attr('comtraigo', data.ComisionTraigo);
        },
        error: function (xhr) {
            value = false;
        }
    });
    return value;
}

function enviarConsulta(emailUser) {
    
    var usuarioRequest = {
        Email: emailUser
    }
    $.ajax({
        type: 'POST',
        url: '/api/user/GetUserEmail',
        dataType: 'json',
        data: JSON.stringify(usuarioRequest),
        contentType: 'application/json; charset=utf-8',
        async: false,
        success: function (data) {
            if (data.NewUser === true) {
                newUser = true;
            }
            nombreUserEnviar = data.Nombre;
            cedulaUser = data.Cedula;
            celularUser = data.CelularEc;
            ciudadUser = data.Ciudad;
            ciudadIdUser = data.CiudadId;
           

        },
        error: function (xhr) {
            alert('Problemas al traer datos del usuario.');
        }
    });
    return newUser;
}


//guardar edición editarCotizacion
$('#guardarEdicion').click(function () {
    //ajax pasar datos para guardar
    //envio a funcion guardar edicion
    var url = $('#campoUrlEdit').val();
    var nombreItem = $('#campoNombreitemEdit').val();
    var precio = $('#campoPrecioEdit').val();
    var email = emailComun;
    var categoria = $('#campoCategoriaEdit').val();
    var peso = $('#campoPesoEdit').val();

    var viaje = $('#campoViajeEdit').val();
    var tracking = $('#campoTrackingEdit').val();
    var estado = $('#campoEstadoEdit').val();

    var largo = $('#campoLargoEdit').val();
    var ancho = $('#campoAnchoEdit').val();
    var alto = $('#campoAltoEdit').val();
    var id = $(this).attr("idcotiza");

    //envía a actualizar funcion común, si es verdadero hace lo siguiente ->
    if (editarCotizacion(url, nombreItem, precio, email, categoria, peso, largo, ancho, alto, id, estado, viaje, tracking)) {
        var url = window.location.href;
        var cotizacionUrl = "/cotizacion";
        var comandaUrl = "/admincomanda";
        if (url.includes(comandaUrl)) {
            traerItems();
        }
        $('#modalEditarQ').modal('hide');

        
    } else {
        alert('Problemas al editar la cotización. ');
    };
});


function traerItems() {
    var idComanda = getUrlParameter('idcomanda');
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
            $('#tablaPreview').empty();
            $.each(data, function (index, element) {
                tablaPrevia(element.URL, element.NombreItem, element.Precio, element.ComisionTotal, element.ComisionViajero, element.ComisionTraigoIva, element.Peso, element.Id, element.IdCotizacion);
                comTotal += parseFloat(element.ComisionTotal);
                comViajero += parseFloat(element.ComisionViajero);
                comTraigo += parseFloat(element.ComisionTraigoIva);
            });

            if (comTraigo < 5.60) {
                comTraigo = 5.60;
            }

            if (comViajero < 6) {
                comViajero = 6;
            }

            comTotal = comTraigo + comViajero;
            $("#valorTraigo").text(comTraigo.toFixed(2));
            $("#valorViajero").text(comViajero.toFixed(2));
            $("#valorComanda").text(comTotal.toFixed(2));

            comisionTotal = comTotal;
            comisionTraigo = comTraigo;
            comisionViajero = comViajero;
        },
        error: function (xhr) {
            alert('Problemas al traer los items.');
        }
    });
}

$("#solicitarPago").click(function () {
    var idComanda = getUrlParameter('idcomanda');
    var usuarioRequest = {
        Estado: 3,
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
            correoPago();

        },
        error: function (xhr) {
            alert('Problemas al enviar la petición. Por favor intente nuevamente.');
        }
    });
});


function correoPago() {

    var idComanda = getUrlParameter('idcomanda');
    var usuarioRequest = {
        IdComanda: idComanda,
    }

    $.ajax({
        type: 'POST',
        url: '/api/command/solicitarPago',
        dataType: 'json',
        data: JSON.stringify(usuarioRequest),
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            alert("Pago solicitado por correo.");
        },
        error: function (xhr) {
            alert('Problemas al enviar la petición. Por favor intente nuevamente.');
        }
    });
}

function tablaPrevia(url, nombre, precio, comisionTotal, comisionViajero, comisionTraigo, pesoItem, id, idcotizacion) {
    var cotizacion = "<tr>";
    cotizacion += "<td><a href='" + url + "' target='_blank' class='itemattr'>" + nombre + "</a></td>";
    cotizacion += "<td title='Peso' class='pesoattr'>" + pesoItem + " libras</td>";
    cotizacion += "<td title='Precio' class='precioattr'>" + precio + " USD</td>";
    cotizacion += "<td title='Comisión Traigo sin iva' class='comtraigoattr'>" + comisionTraigo + " USD</td>";
    cotizacion += "<td title='Comisión Viajero' class='comviajeroattr'>" + comisionViajero + " USD</td>";
    cotizacion += "<td title='Comisión Total' class='comviajeroattr' comision='" + comisionTotal + "'>$" + comisionTotal + " USD</td>";
    cotizacion += "<td><button class='btn btn-primary btnEditar' iditem='" + id + "' idcotizacion='" + idcotizacion + "'>Editar <i class='fas fa-minus-circle'></i></button ></td>";
    cotizacion += "</tr>";
    $('#tablaPreview').prepend(cotizacion);
} 

//function traer cotizacion
function traerCotiza(idCotizacion) {
    var usuarioRequest = {
        Id: idCotizacion
    }

    $.ajax({
        type: 'POST',
        url: '/api/quotation/GetQuotationById',
        dataType: 'json',
        data: JSON.stringify(usuarioRequest),
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            $('#modalEditarQ').modal('show');
            $('#campoUrlEdit').val(data.URL);
            $('#campoNombreitemEdit').val(data.NombreItem);
            $('#campoPrecioEdit').val(data.Precio);
            $('#campoCategoriaEdit').val(data.IdCategoria);
            $('#campoPesoEdit').val(data.Peso);
            $('#campoLargoEdit').val(data.Largo);
            $('#campoAnchoEdit').val(data.Ancho);
            $('#campoAltoEdit').val(data.Alto);
            $('#campoViajeEdit').val(data.IdViaje);
            $('#campoEstadoEdit').val(data.IdEstado);
            $('#campoTrackingEdit').val(data.TrackingNumber);
        },
        error: function (xhr) {
            alert('Problemas al traer la cotización. ');
        }
    });
}

function armarTabla(nombre, email, cedula, celularec, ciudad) {
    //cuadroCliente
    var tablaDatos = "<table class='table table-bordered table-hover'>";
    tablaDatos += "<thead><tr><th>Campo</th><th>Descripcion &nbsp; <button class='btn btn-default' id='btnEditarUser'><i class='fas fa-pencil-alt'></i></button></th></tr></thead>";
    tablaDatos += "<tbody><tr>";
    tablaDatos += "<td>Nombre:</td>";
    tablaDatos += "<td>" + nombre + "</td>";
    tablaDatos += "</tr>";
    tablaDatos += "<tr>";
    tablaDatos += "<td>Email:</td>";
    tablaDatos += "<td>" + email + "</td>";
    tablaDatos += "</tr>";
    tablaDatos += "<td>Cédula:</td>";
    tablaDatos += "<td>" + cedula + "</td>";
    tablaDatos += "</tr>";
    tablaDatos += "<tr>";
    tablaDatos += "<td>Celular:</td>";
    tablaDatos += "<td>" + celularec + "</td>";
    tablaDatos += "</tr>";
    tablaDatos += "<tr>";
    tablaDatos += "<td>Ciudad:</td>";
    tablaDatos += "<td>" + ciudad + "</td>";
    tablaDatos += "</tr>";
    $('#cuadroCliente').append(tablaDatos);
}

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
            //show tabla cotizacinoes
            //armo tabla cotizaciones
            $.each(data, function (index, element) {
                ingresarCotizacionTabla(element.URL, element.NombreItem, element.Estado, element.Precio, element.ComisionTotal, element.ComisionViajero, element.ComisionTraigo, element.Peso, element.TextoResultado, element.FechaVuelta, element.Id);
            });
        },
        error: function (xhr) {
            alert('Problemas al traer las cotizaciones. ');
        }
    });
}

function ingresarCotizacionTabla(url, nombre, estado, precio, comisionTotal, comisionViajero, comisionTraigo, pesoItem, textoResultado, fechaVuelta, id) {
    var cotizacion = "<tr idrow='"+id+"'>";
    cotizacion += "<td><input type='checkbox' class='checkIn' valuePeso='" + pesoItem + "' nombreMuneco='" + nombreUserEnviar + "' fechaLlegada='" + fechaVuelta + "' precio='" + precio + "' comTraigo='" + comisionTraigo + "' comViajero='" + comisionViajero + "' '></td>";
    cotizacion += "<td><a href='" + url + "' target='_blank' class='itemattr'>" + nombre +"</a></td>";
    cotizacion += "<td title='Estado' class='estadoattr'>" + estado + "</td>";
    cotizacion += "<td title='Peso' class='pesoattr'>" + pesoItem + "</td>";
    cotizacion += "<td title='Precio' class='precioattr'>" + precio + "</td>";
    cotizacion += "<td title='Comisión Traigo sin iva' class='comtraigoattr'>" + comisionTraigo + "</td>";
    cotizacion += "<td title='Comisión Viajero' class='comviajeroattr'>" + comisionViajero + "</td>";
    cotizacion += "<td title='Viajero asignado' class='vueltaattr'>" + fechaVuelta + "</td>";
    cotizacion += "<td><button class='btn' data-clipboard-text='" + textoResultado + "'><i class='fas fa-clipboard-list'></i></button >&nbsp;";
    cotizacion += "<button class='btn btnEditar' idcotizacion='" + id + "' ><i class='fas fa-pencil-alt'></i></button ></td>";
    cotizacion += "</tr>";
    $('#tablaCotizacion').prepend(cotizacion);
} 

//btnEditar cotización
jQuery(document.body).on('click', '.btnEditar', function (event) {
    var idCotizacion = $(this).attr("idcotizacion");
    $('#guardarEdicion').attr("idcotiza", idCotizacion);
    //llamo a la función traer cotización
    traerCotiza(idCotizacion);
});

//btnEditarUser
jQuery(document.body).on('click', '#btnEditarUser', function (event) {
    $('#nombreEditForm').val(nombreUserEnviar);
    $('#cedulaEditForm').val(cedulaUser);
    $('#celularecEditForm').val(celularUser);
    $('#ciudadEditForm1').val(ciudadIdUser);
    $('#modalEditarUser').modal('show');
});

$('#btnEditUser').click(function () {
    var nombreNuevo = $('#nombreEditForm').val();
    var cedulaNuevo = $('#cedulaEditForm').val();
    var celularEcNuevo = $('#celularecEditForm').val();
    var ciudadEcNuevo = $('#ciudadEditForm1').val();
    var edicionUserRequest = {

        Email: emailComun,
        Nombre: nombreNuevo,
        Cedula: cedulaNuevo,
        CelularEc: celularEcNuevo,
        IdCiudad: ciudadEcNuevo
    }

    $.ajax({
        type: 'POST',
        url: '/api/user/EditUser',
        dataType: 'json',
        data: JSON.stringify(edicionUserRequest),
        contentType: 'application/json; charset=utf-8',
        async: true,
        success: function (data) {
            //esconder modal
            $('#modalEditarUser').modal('hide');
            //actualizar datos 
            $('#cuadroCliente').empty();
            armarTabla(data.Nombre, emailComun, data.Cedula, data.CelularEc, data.Ciudad);
            alert('Datos guardados correctamente ');

        },
        error: function (xhr) {
            alert('Problemas al editar el usuario. ');
        }
    });
});





function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1);
    var sURLVariables = sPageURL.split('&');
    for (var i = 0; i < sURLVariables.length; i++) {
        var sParameterName = sURLVariables[i].split('=');
        if (sParameterName[0] == sParam) {
            return sParameterName[1];
        }
    }
}

//crearUsuario
$("#crearUsuario").click(function () {
    var nombreUser = $("#nombreForm").val();
    var emailUser = $("#emailForm").val();
    var cedulaUser = $("#cedulaForm").val();
    var celularecUser = $("#celularecForm").val();
    var ciudadId = $("#ciudadForm").val();

    var usuarioRequest = {
        Nombre: nombreUser,
        Email: emailUser,
        Cedula: cedulaUser,
        CelularEc: celularecUser,
        IdCiudad: ciudadId
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
                alert('Al parecer el usuario ya existe.');
            } else {
                $('#cuadroCliente').show();
                $('#cuadroForm').hide();
                $('#cotizacionRow').show();
                armarTabla(data.Nombre, data.Email, data.Cedula, data.CelularEc, data.Ciudad);
                $('#datosViajeroRow').show();
            }
        },
        error: function (xhr) {
            alert('Problemas al crear el usuario. ');
        }
    });
});