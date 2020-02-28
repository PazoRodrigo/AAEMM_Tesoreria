$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Chequeras');
        $("#SpanTituloGrillaDimensional").text('Buscador de cheques');
        //$("#SpanTituloDimensional").text('Centros de Costo');
        //$("#SpanBtnImprimir").text('Imprimir Centros de Costo');
        $("#SpanBuscar").text('Buscar');
        $("#SpanModificar").text('Modificar');
        $("#SpanCrearChequera").text('Crear Chequera');
        Inicio();
    } catch (e) {
        alertAlerta(e);
    }
});
async function Inicio() {
    await ComboBuscador();
    //await ComboModificador();
}
async function ComboBuscador() {
    let lista = await EstadoCheque.TraerTodos_Propios();
    console.log(lista);
    await EstadoCheque.ArmarCombo(lista, 'CboBuscador', 'SelectornBuscadorEstadoCheque', 'SeleccionBuscadorEstadoCheque', 'Seleccione Estado Cheque', 'CboB');
}
async function Nueva_Chequera() {
}