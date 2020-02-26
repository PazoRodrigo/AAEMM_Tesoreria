var _ObjChequera;

$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Chequeras');
        $("#SpanTituloGrillaDimensional").text('Buscador de cheques');
        //$("#SpanTituloDimensional").text('Centros de Costo');
        //$("#SpanBtnImprimir").text('Imprimir Centros de Costo');
        //$("#SpanBtnNuevo").text('Nuevo');
        //$("#SpanBtnGuardar").text('Guardar');
        //Inicio();
    } catch (e) {
        alertAlerta(e);
    }
});
async function Inicio() {
    Nuevo_CentroCosto();
    await LlenarGrilla_CentroCosto();
}