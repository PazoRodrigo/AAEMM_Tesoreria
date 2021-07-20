$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Banco');
        $("#SpanTituloGrillaDimensional").text('Bancos Registrados');
        $("#SpanTituloDimensional").text('Banco');
        $("#SpanBtnImprimir").text('Imprimir Bancos');
        $("#SpanBtnNuevo").text('Nuevo');
        $("#SpanBtnGuardar").text('Guardar');
   //     Inicio();
    } catch (e) {
        alertAlerta(e);
    }
});