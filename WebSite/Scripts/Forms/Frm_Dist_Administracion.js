$(document).ready(function () {
    try {
        alert(1111);
        $("#SpanNombreFormulario").text('Administración');
    } catch (e) {
        alertAlerta(e);
    }
});