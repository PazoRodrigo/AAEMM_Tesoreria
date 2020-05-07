$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Administración');
    } catch (e) {
        alertAlerta(e);
    }
});