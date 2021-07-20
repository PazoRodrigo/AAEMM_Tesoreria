$(document).ready(async function () {
    try {
        spinner();
        await ValidarPermisosXPerfil();
        spinnerClose();
        $("#SpanNombreFormulario").text('Ingresos');
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});