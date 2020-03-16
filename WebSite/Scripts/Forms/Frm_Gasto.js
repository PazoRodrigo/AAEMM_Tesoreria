$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Gastos');
        $("#SpanBtnComprobante").text('Nuevo Comprobante');
        $("#SpanBtnGasto").text('Nuevo Gasto');
        $("#SpanBtnBuscar").text('Buscar');
        Inicio();
    } catch (e) {
        alertAlerta(e);
    }
});
async function Inicio() {
    Nuevo_Gasto();
    await LlenarGrilla_Gasto();
    await LlenarCboOriginario();
    await LlenarCboProveedor();
}
function Limpiar_Gasto() {
    $(".DatoFormulario").val('');
}
function Nuevo_Gasto() {
    Limpiar_Gasto();
    _ObjGasto = new Gasto;
    $("#TxtNombre").focus();
}
function Llenar_Gasto(Obj_Gasto) {
    Nuevo_Gasto();
    _ObjGasto.IdEntidad = Obj_Gasto.IdEntidad;
    _ObjGasto.Nombre = Obj_Gasto.Nombre;
    _ObjGasto.Observaciones = Obj_Gasto.Observaciones;
    $("#TxtNombre").val(_ObjGasto.Nombre);
    $("#TxtObservaciones").val(_ObjGasto.Observaciones);
}
async function LlenarGrilla_Gasto() {
    let lista = await Gasto.TraerTodos();
    Gasto.ArmarGrilla(lista, 'GrillaGastosRegistrados', 'SeleccionarGasto', 'EliminarGasto', 'height:350px; overflow-y: scroll');
}
async function LlenarCboOriginario() {
    let lista = await OriginarioGasto.TraerTodos();
    console.log(lista);
    OriginarioGasto.ArmarCombo(lista, 'CboOriginarioGasto', 'SeleccionarGasto', 'EventoSeleccionOriginarioGasto', 'Seleccione', '');
}
async function LlenarCboProveedor() {
    let lista = await Gasto.TraerTodos();
    Gasto.ArmarGrilla(lista, 'GrillaGastosRegistrados', 'SeleccionarGasto', 'EliminarGasto', 'height:350px; overflow-y: scroll');
}
