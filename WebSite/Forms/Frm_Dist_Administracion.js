$(document).ready(async function () {
    try {
        spinner();
        await ValidarPermisosXPerfil();
        spinnerClose();
        $("#SpanNombreFormulario").text('Administración');
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});