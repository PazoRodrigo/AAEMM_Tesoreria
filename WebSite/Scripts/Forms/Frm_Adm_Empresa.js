var _ObjEmpresa;

$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Empresas');
        $("#SpanTituloDimensional").text('Estado Empresa. ACTIVA');
        $("#SpanBtnImprimirNomina").text('Imprimir Nómina');
        $("#SpanBtnImprimir").text('Imprimir Empresa');
        $("#SpanBtnNuevo").text('Nueva Empresa');
        $("#SpanBtnBuscar").text('Buscar Empresa');
        $("#SpanBtnEliminar").text('Eliminar');
        $("#SpanBtnGuardar").text('Guardar');
        $("#SpanBtnDomicilios").text('Domicilios (1)');
        $("#SpanBtnContactos").text('Contactos (2)');
        $("#SpanBtnTelefonos").text('Teléfonos (1)');
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
async function LlenarEmpresa() {
    $("#TxtRazonSocial").val(_ObjEmpresa.RazonSocial);
    $("#TxtCUIT").val(_ObjEmpresa.CUIT);
    $("#TxtEmail").val(_ObjEmpresa.CorreoElectronico);
    $("#TxtDireccion").val(_ObjEmpresa.Domicilio.Direccion);
    $("#TxtCP").val(_ObjEmpresa.Domicilio.CodigoPostal);
    $("#TxtLocalidad").val(_ObjEmpresa.Domicilio.Localidad.Descripcion);
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
    await Convenio.ArmarCombo(lista, 'CboConvenio', 'SelectorConvenio', 'EventoSeleccionConvenio', 'Convenio', 'CboConvenio');
}

document.addEventListener('EventoSeleccionarEmpresa', async function (e) {
    try {
        let objSeleccionado = e.detail;
        _ObjEmpresa = objSeleccionado;
        await LlenarEmpresa();
    } catch (e) {
        alertAlerta(e);
    }
}, false);
$('body').on('click', '#LinkBtnArmarUC', async function (e) {
    try {
        Inicio();
    } catch (e) {
        alertAlerta(e);
    }
});

