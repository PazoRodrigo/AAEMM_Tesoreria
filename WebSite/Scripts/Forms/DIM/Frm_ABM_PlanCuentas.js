$(document).ready(function () {
    try {
        $("#NombreFormulario").text('Plan de Cuentas');
        $("#SpanTituloGrillaDimensional").text('Cuentas Registradas');
        $("#SpanTituloDimensional").text('Cuenta');
        $("#BtnImprimir").val('Imprimir Cuentas');
    } catch (e) {
        alertAlerta(e);
    }
})