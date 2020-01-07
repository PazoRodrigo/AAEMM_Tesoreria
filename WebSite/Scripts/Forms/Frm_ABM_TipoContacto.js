$(document).ready(function () {
    try {
        $("#NombreFormulario").text('Tipos de Contacto');
        $("#SpanTituloGrillaDimensional").text('Tipos de Contacto Registrados');
        $("#SpanTituloDimensional").text('Tipo de Contacto');
        $("#BtnImprimir").val('Imprimir Tipos de Contacto');
    } catch (e) {
        alertAlerta(e);
    }
})