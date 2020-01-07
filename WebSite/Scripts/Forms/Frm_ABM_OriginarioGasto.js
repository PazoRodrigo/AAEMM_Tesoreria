$(document).ready(function () {
    try {
        $("#NombreFormulario").text('Originario de Gasto');
        $("#SpanTituloGrillaDimensional").text('Originarios de Gasto Registrados');
        $("#SpanTituloDimensional").text('Originario de Gasto');
        $("#BtnImprimir").val('Imprimir Originarios de Gasto');
    } catch (e) {
        alertAlerta(e);
    }
})