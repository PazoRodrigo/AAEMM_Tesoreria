var _EstadoBusca;
var _EstadoModifica;

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
    Nueva_Chequera();
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
document.addEventListener('EventoBuscaEstadoCheque', async function (e) {
    try {
        let objSeleccionado = e.detail;
        _EstadoBusca = objSeleccionado.idEntidad;
        $("#SelectornBuscaEstadoCheque").text(objSeleccionado.Estado);
    } catch (e) {
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
        Nueva_Chequera();
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});

$('body').on('click', '#LinkBtnNuevo', async function (e) {
    try {
        Nuevo_CentroCosto();
    } catch (e) {
        alertAlerta(e);
    }
});