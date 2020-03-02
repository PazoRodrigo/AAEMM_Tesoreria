var _EstadoBusca = 0;
var _EstadoModifica = 0;

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
    _EstadoBusca = 0;
    _EstadoModifica = 0;
    await ComboBusca();
    await ComboModifica();
    _EstadoBusca = 1;
    let objSeleccionado = await EstadoCheque.TraerUno(1, 1);
    $("#SelectornBuscaEstadoCheque").text(objSeleccionado.Estado);
    Nueva_Chequera();
    await LlenarGrilla();
}
async function ComboBusca() {
    let lista = await EstadoCheque.TraerTodos_Propios();
    await EstadoCheque.ArmarCombo(lista, 'CboBusca', 'SelectornBuscaEstadoCheque', 'EventoBuscaEstadoCheque', 'Seleccione Estado Cheque', 'CboB');
}
async function ComboModifica() {
    let lista = await EstadoCheque.TraerTodos_Propios();
    await EstadoCheque.ArmarCombo(lista, 'CboModifica', 'SelectornModificaEstadoCheque', 'EventoModificaEstadoCheque', 'Seleccione Estado Cheque', 'CboM');
}
async function Nueva_Chequera() {
    $("TxtCrearDesde").val();
    $("TxtCrearHasta").val();
}
function LimpiarGrilla() {
    $("#GrillaRegistrados").html('');
}
async function LlenarGrilla() {
    LimpiarGrilla();
    if (_EstadoBusca !== undefined) {
        let Lista = await ChequePropio.TraerTodosXEstado(_EstadoBusca, $("#TxtBuscadorDesde").val(), $("#TxtBuscadorHasta").val());
        await ChequePropio.ArmarGrillaChequera(Lista, 'GrillaRegistrados', 'height:250px; overflow-y: scroll');
    }
}

document.addEventListener('EventoBuscaEstadoCheque', async function (e) {
    try {
        let objSeleccionado = e.detail;
        _EstadoBusca = objSeleccionado.IdEntidad;
        $("#SelectornBuscaEstadoCheque").text(objSeleccionado.Estado);
        spinner();
        await LlenarGrilla();
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
}, false);
document.addEventListener('EventoModificaEstadoCheque', async function (e) {
    try {
        let objSeleccionado = e.detail;
        _EstadoModifica = objSeleccionado.idEntidad;
        $("#SelectornModificaEstadoCheque").text(objSeleccionado.Estado);
    } catch (e) {
        alertAlerta(e);
    }
}, false);
$('body').on('click', '#LinkCrearChequera', async function (e) {
    try {
        spinner();
        await ChequePropio.AltaChequera($("#TxtCrearDesde").val(), $("#TxtCrearHasta").val());
        alertOk('La chequera se ha guardado con éxito');
        await LlenarGrilla();
        Nueva_Chequera();
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});

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