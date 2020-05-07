$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Reportes');
    } catch (e) {
        alertAlerta(e);
    }
})