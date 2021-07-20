$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Banco');
        $("#SpanTituloGrillaDimensional").text('Centros de Costo Registrados');
        $("#SpanTituloDimensional").text('Centros de Costo');
        $("#SpanBtnImprimir").text('Imprimir Centros de Costo');
        $("#SpanBtnNuevo").text('Nuevo');
        $("#SpanBtnGuardar").text('Guardar');
   //     Inicio();
    } catch (e) {
        alertAlerta(e);
    }
});