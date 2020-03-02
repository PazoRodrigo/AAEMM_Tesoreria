var _ObjChequePropio;
var _EstadoBusca = 0;

$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Cheque Propio');
        $("#SpanTituloGrillaDimensional").text('Buscador de cheques');
        $("#SpanBuscar").text('Buscar');
        $("#SpanAnularCheque").text('Anular Cheque');
        Inicio();
    } catch (e) {
        alertAlerta(e);
    }
});
async function Inicio() {
    await ComboBusca();
    _EstadoBusca = 1;
    await LlenarGrilla();
}
async function ComboBusca() {
    let lista = await EstadoCheque.TraerTodos_Propios();
    await EstadoCheque.ArmarCombo(lista, 'CboBusca', 'SelectornBuscaEstadoCheque', 'EventoBuscaEstadoCheque', 'Seleccione Estado Cheque', 'CboB');
}
function LimpiarGrilla() {
    $("#GrillaRegistrados").html('');
}
async function LlenarGrilla() {
    LimpiarGrilla();
    //if (_EstadoBusca !== undefined) {
    //    let Lista = await ChequePropio.TraerTodosXEstado(_EstadoBusca, $("#TxtBuscadorDesde").val(), $("#TxtBuscadorHasta").val());
    //    await ChequePropio.ArmarGrillaChequera(Lista, 'GrillaRegistrados', 'height:250px; overflow-y: scroll');
    //}
}
$('body').on('click', '#LinkBtnBuscar', async function (e) {
    try {
        spinner();
        await LlenarGrilla();
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});