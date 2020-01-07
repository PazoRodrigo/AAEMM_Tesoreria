$(document).ready(function () {
    try {
        $("#NombreFormulario").text('Reportes');
    } catch (e) {
        alertAlerta(e);
    }
})