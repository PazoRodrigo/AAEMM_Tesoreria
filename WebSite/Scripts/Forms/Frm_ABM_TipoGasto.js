$(document).ready(function () {
    try {
        $("#NombreFormulario").text('Tipos de Gasto');
        $("#SpanTituloGrillaDimensional").text('Tipos de Gasto Registrados');
        $("#SpanTituloDimensional").text('Tipo de Gasto');
        $("#BtnImprimir").val('Imprimir Tipos de Gasto');
    } catch (e) {
        alertAlerta(e);
    }
})