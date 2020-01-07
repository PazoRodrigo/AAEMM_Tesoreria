$(document).ready(function () {
    try {
        $("#NombreFormulario").text('Administración');
    } catch (e) {
        alertAlerta(e);
    }
});