$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Gastos');
        $("#SpanBtnComprobante").text('Nuevo Comprobante');
        $("#SpanBtnGasto").text('Nuevo Gasto');
        $("#SpanBtnBuscar").text('Buscar');

    } catch (e) {
        alertAlerta(e);
    }
});