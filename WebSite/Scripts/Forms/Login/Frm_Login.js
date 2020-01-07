$(document).ready(function () {
    try {
        $("#NombreFormulario").text('Acceso al Sistema');
    } catch (e) {
        alertAlerta(e);
    }
});