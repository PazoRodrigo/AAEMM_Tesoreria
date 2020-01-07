$(document).ready(function () {
    try {
        $("#NombreFormulario").text('Proveedores');
        $("#SpanTituloGrillaDimensional").text('Proveedores Registrados');
        $("#SpanTituloDimensional").text('Proveedor');
        $("#BtnImprimir").val('Imprimir Proveedores');
    } catch (e) {
        alertAlerta(e);
    }
})