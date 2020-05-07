var _ObjOriginarioGasto;

$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Originarios de Gasto');
        $("#SpanTituloGrillaDimensional").text('Originarios de Gasto Registrados');
        $("#SpanTituloDimensional").text('Originario de Gasto');
        $("#SpanBtnImprimir").text('Imprimir Originarios de Gasto');
        $("#SpanBtnNuevo").text('Nuevo');
        $("#SpanBtnGuardar").text('Guardar');
        Inicio();
    } catch (e) {
        alertAlerta(e);
    }
});
async function Inicio() {
    Nuevo_OriginarioGasto();
    await LlenarGrilla_OriginarioGasto();
}
function Limpiar_OriginarioGasto() {
    $(".DatoFormulario").val('');
}
function Nuevo_OriginarioGasto() {
    Limpiar_OriginarioGasto();
    _ObjOriginarioGasto = new OriginarioGasto;
    $("#TxtNombre").focus();
}
function Llenar_OriginarioGasto(Obj_OriginarioGasto) {
    Nuevo_OriginarioGasto();
    _ObjOriginarioGasto.IdEntidad = Obj_OriginarioGasto.IdEntidad;
    _ObjOriginarioGasto.Nombre = Obj_OriginarioGasto.Nombre;
    _ObjOriginarioGasto.Observaciones = Obj_OriginarioGasto.Observaciones;
    $("#TxtNombre").val(_ObjOriginarioGasto.Nombre);
    $("#TxtObservaciones").val(_ObjOriginarioGasto.Observaciones);

}
async function LlenarGrilla_OriginarioGasto() {
    let lista = await OriginarioGasto.TraerTodos();
    OriginarioGasto.ArmarGrilla(lista, 'GrillaRegistrados', 'SeleccionarOriginarioGasto', 'EliminarOriginarioGasto', 'height:350px; overflow-y: scroll');
}

$('body').on('click', '#LinkBtnGuardar', async function (e) {
    try {
        spinner();
        _ObjOriginarioGasto.Nombre = $("#TxtNombre").val();
        _ObjOriginarioGasto.Observaciones = $("#TxtObservaciones").val();
        let Mensaje = '';
        if (_ObjOriginarioGasto.IdEntidad === 0) {
            // Es Alta
            await _ObjOriginarioGasto.Alta();
            Mensaje = 'agregado';
        } else {
            // Modifica
            await _ObjOriginarioGasto.Modifica();
            Mensaje = 'modificado';
        }
        await LlenarGrilla_OriginarioGasto();
        alertOk('El Originario del Gasto se ha ' + Mensaje + ' correctamente.');
        Nuevo_OriginarioGasto();
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#LinkBtnNuevo', async function (e) {
    try {
        Nuevo_OriginarioGasto();
    } catch (e) {
        alertAlerta(e);
    }
});
document.addEventListener('SeleccionarOriginarioGasto', async function (e) {
    try {
        let objSeleccionado = e.detail;
        Llenar_OriginarioGasto(objSeleccionado);
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('EliminarOriginarioGasto', async function (e) {
    try {
        let objSeleccionado = e.detail;
        console.log(objSeleccionado);
        if (objSeleccionado.IdEstado === 1) {
            throw 'El Originario del Gasto ya se encuentra eliminado.';
        }
        console.log(objSeleccionado);
        _ObjOriginarioGasto.IdEntidad = objSeleccionado.IdEntidad;
        _ObjOriginarioGasto.Nombre = objSeleccionado.Nombre;
        _ObjOriginarioGasto.Observaciones = objSeleccionado.Observaciones;

        PopUpConfirmarConCancelar('warning', _ObjOriginarioGasto, 'Eliminar', 'Confirma que desea eliminar (Dar de baja) ' + objSeleccionado.Nombre + '?', 'ConfirmacionEventoEliminar', 'Eliminar', 'Cancelar', 'red');
    } catch (e) {
        spinnerClose();
        alertAlerta('<b>El Originario del Gasto no se ha eliminado</b> </br></br> ' + e);
    }
}, false);
document.addEventListener('ConfirmacionEventoEliminar', async function (e) {
    try {
        spinner();
        let objSeleccionado = e.detail;
        await objSeleccionado.Baja();
        await Inicio();
        spinnerClose();
        alertOk('El Originario del Gasto se ha eliminado correctamente.');
    } catch (e) {
        spinnerClose();
        alertAlerta('El Originario del Gasto no se ha eliminado. \n\n ' + e);
    }
}, false);