$(document).ready(function () {
    try {
        $("#NombreFormulario").text('Convenios');
        $("#SpanTituloGrillaDimensional").text('Convenios Registrados');
        $("#SpanTituloDimensional").text('Convenio');
        $("#BtnImprimir").val('Imprimir Convenios');
    } catch (e) {
        alertAlerta(e);
    }
})