$(document).ready(function () {
    try {
        $("#NombreFormulario").text('Tipos de Pago');
        $("#SpanTituloGrillaDimensional").text('Tipos de Pago Registrados');
        $("#SpanTituloDimensional").text('Tipo de Pago');
        $("#BtnImprimir").val('Imprimir Tipos de Pago');
    } catch (e) {
        alertAlerta(e);
    }
})