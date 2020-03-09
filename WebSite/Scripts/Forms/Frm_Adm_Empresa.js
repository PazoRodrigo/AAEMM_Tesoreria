var _ObjEmpresa;

$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Empresas');
        $("#SpanTituloDimensional").text('Estado Empresa. ACTIVA');
        $("#SpanBtnImprimirNomina").text('Imprimir Nómina');
        $("#SpanBtnImprimir").text('Imprimir Empresa');
        $("#SpanBtnEliminar").text('Eliminar');
        $("#SpanBtnNuevo").text('Nueva Empresa');
        $("#SpanBtnBuscar").text('Buscar Empresa');
        $("#SpanBtnDomicilios").text('Domicilios (1)');
        $("#SpanBtnContactos").text('Contactos (2)');
        $("#SpanBtnTelefonos").text('Teléfonos (1)');
        $("#SpanBtnGuardar").text('Guardar');
        $("#SpanBtnReactivar").text('Reactivando Empresa');
        Inicio();
    } catch (e) {
        alertAlerta(e);
    }
});
async function Inicio() {
    Nuevo_Empresa();
    await Empresa.armarUC();
    await ArmarComboCentroCosto();
    await ArmarComboConvenio();
}
function Limpiar_Empresa() {
    $(".DatoFormulario").val('');
}
function Nuevo_Empresa() {
    Limpiar_Empresa();
    _ObjEmpresa = new Empresa;
}
async function ArmarComboCentroCosto() {
    let lista = await  CentroCosto.Todos();
    await CentroCosto.ArmarCombo(lista, 'CboCentroCosto', 'SelectorCentroCosto', 'EventoSeleccionCentroCosto', 'Centro de Costo', 'CboBuscadorCC');

}
async function ArmarComboConvenio() {
    let lista = await Convenio.Todos();
    //await Convenio.ArmarCombo(lista, 'CboConvenio', 'SelectorConvenio', 'EventoSeleccionConvenio', 'Convenio', 'CboConvenio');
}

document.addEventListener('EventoSeleccionarEmpresa', async function (e) {
    try {
        let objSeleccionado = e.detail;
        _ObjEmpresa = objSeleccionado;
        console.log(_ObjEmpresa);
    } catch (e) {
        alertAlerta(e);
    }
}, false);

