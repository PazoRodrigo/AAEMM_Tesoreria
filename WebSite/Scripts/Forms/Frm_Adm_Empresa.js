var _ObjEmpresa;

$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Empresas');
        //$("#SpanTituloGrillaDimensional").text('Centros de Costo Registrados');
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
}
function Limpiar_Empresa() {
    $(".DatoFormulario").val('');
}
function Nuevo_Empresa() {
    Limpiar_Empresa();
    _ObjEmpresa = new Empresa;
}
