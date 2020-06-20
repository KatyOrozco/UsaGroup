$(document).ready(function () {
    var html = "";
    $(".btnpaquete").click(function (event) {
        event.preventDefault();
        var id = $(this).attr('ViajeId');
        html += "<tr><td colspan='11'><div class='row'><div class='col-lg-12'><table class='table table-bordered table-hover'><thead><tr><th>Id</th><th>Cliente</th><th>Producto</th><th>Tracking</th><th>Comisión</th><th>Comisión TC</th><th>Peso</th><th>Estado</th></tr></thead><tbody>";
        var comisionTotal = 0;
        var pesoTotal = 0;
        var comisionTCTotal = 0;
        var tripRequest = {
            ViajeId: id
        }

        $.ajax({
            type: 'POST',
            url: '/api/quotation/PackagesByTrip',
            dataType: 'json',
            data: JSON.stringify(tripRequest),
            contentType: 'application/json; charset=utf-8',
            async: false,
            success: function (data) {
                //hago un forecach
                $.each(data, function (index, element) {
                    armarTablaPaquetes(element.NombreCliente, element.NombreItem, element.TrackingNumber, element.ComisionViajero, element.ComisionTraigo, element.Peso, element.Estado, element.Id);
                    comisionTotal += element.ComisionViajero;
                    pesoTotal += element.Peso;
                    comisionTCTotal += element.ComisionTraigo;
                });
            },
            error: function (xhr) {
                alert('Problemas al traer los paquetes.');
            }
        });

        html += "<td colspan='4'>Total:</td><td title='Comisión Viajero'>" + parseFloat(comisionTotal).toFixed(2) + "</td><td title='Comisión Traigo'>" + parseFloat(comisionTCTotal).toFixed(2) + "</td><td title='Peso'>" + parseFloat(pesoTotal).toFixed(2) +"</td><td></td>";
        html += "</tbody ></table ></div ></div ></td ></tr > ";
        $(this).closest("tr").after(html);
        html = "";
    });

    function armarTablaPaquetes(nombreCliente, nombreItem, trackingNumber, comisionViajero, comisionTraigo, peso, estado, id) {
        html += "<tr><td>" + id + "</td>";
        html += "<td>" + nombreCliente + "</td>";
        html += "<td>" + nombreItem + "</td>";
        html += "<td>" + trackingNumber + "</td>";
        html += "<td title='Comisión Viajero'>" + comisionViajero + "</td>";
        html += "<td title='Comisión Traigo'>" + comisionTraigo + "</td>";
        html += "<td title='Peso'>" + peso + "</td>";
        html += "<td title='Estado'>" + estado + "</td>";
        html += "</td></tr>";
    }
});
