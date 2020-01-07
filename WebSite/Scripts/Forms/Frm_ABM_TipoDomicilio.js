$(document).ready(function () {
    try {
        $("#NombreFormulario").text('Tipos de Domicilio');
        $("#SpanTituloGrillaDimensional").text('Tipos de Domicilio Registrados');
        $("#SpanTituloDimensional").text('Tipo de Domicilio');
        $("#BtnImprimir").val('Imprimir Tipos de Domicilio');
    } catch (e) {
        alertAlerta(e);
    }
})