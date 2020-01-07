$(document).ready(function () {
    try {
        $("#NombreFormulario").text('Centros de Costos');
        $("#SpanTituloGrillaDimensional").text('Centros de Costos Registrados');
        $("#SpanTituloDimensional").text('Centros de Costos');
        $("#BtnImprimir").val('Imprimir Centros de Costos');
    } catch (e) {
        alertAlerta(e);
    }
})