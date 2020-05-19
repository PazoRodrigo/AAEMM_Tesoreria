$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Reporte Ingresos por Nombre de Archivo');
    } catch (e) {
        alertAlerta(e);
    }
});