var _ObjTipoGasto;

$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Tipos de Gasto');
        $("#SpanTituloGrillaDimensional").text('Tipos de Gasto Registrados');
        $("#SpanTituloDimensional").text('Tipo de Gasto');
        $("#SpanBtnImprimir").text('Imprimir Tipos de Gasto');
        $("#SpanBtnNuevo").text('Nuevo');
        $("#SpanBtnGuardar").text('Guardar');
        Inicio();
    } catch (e) {
        alertAlerta(e);
    }
});
async function Inicio() {
    Nuevo_TipoGasto();
    await LlenarGrilla_TipoGasto();
}
function Limpiar_TipoGasto() {
    $(".DatoFormulario").val('');
}
function Nuevo_TipoGasto() {
    Limpiar_TipoGasto();
    _ObjTipoGasto = new TipoGasto;
    $("#TxtNombre").focus();
}
function Llenar_TipoGasto(Obj_TipoGasto) {
    Nuevo_TipoGasto();
    _ObjTipoGasto.IdEntidad = Obj_TipoGasto.IdEntidad;
    _ObjTipoGasto.Nombre = Obj_TipoGasto.Nombre;
    _ObjTipoGasto.Observaciones = Obj_TipoGasto.Observaciones;
    $("#TxtNombre").val(_ObjTipoGasto.Nombre);
    $("#TxtObservaciones").val(_ObjTipoGasto.Observaciones);

}
async function LlenarGrilla_TipoGasto() {
    let lista = await TipoGasto.TraerTodos();
    TipoGasto.ArmarGrilla(lista, 'GrillaRegistrados', 'SeleccionarTipoGasto', 'EliminarTipoGasto', '');
}

$('body').on('click', '#LinkBtnGuardar', async function (e) {
    try {
        spinner();
        _ObjTipoGasto.Nombre = $("#TxtNombre").val();
        _ObjTipoGasto.Observaciones = $("#TxtObservaciones").val();
        let Mensaje = '';
        if (_ObjTipoGasto.IdEntidad === 0) {
            // Es Alta
            await _ObjTipoGasto.Alta();
            Mensaje = 'agregado';
        } else {
            // Modifica
            await _ObjTipoGasto.Modifica();
            Mensaje = 'modificado';
        }
        await LlenarGrilla_TipoGasto();
        alertOk('El Tipo de Gasto se ha ' + Mensaje + ' correctamente.');
        Nuevo_TipoGasto();
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#LinkBtnNuevo', async function (e) {
    try {
        Nuevo_TipoGasto();
    } catch (e) {
        alertAlerta(e);
    }
});
document.addEventListener('SeleccionarTipoGasto', async function (e) {
    try {
        let objSeleccionado = e.detail;
        Llenar_TipoGasto(objSeleccionado);
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('EliminarTipoGasto', async function (e) {
    try {
        let objSeleccionado = e.detail;
        console.log(objSeleccionado);
        if (objSeleccionado.IdEstado === 1) {
            throw 'El Tipo de Gasto ya se encuentra eliminado.';
        }
        console.log(objSeleccionado);
        _ObjTipoGasto.IdEntidad = objSeleccionado.IdEntidad;
        _ObjTipoGasto.Nombre = objSeleccionado.Nombre;
        _ObjTipoGasto.Observaciones = objSeleccionado.Observaciones;

        PopUpConfirmarConCancelar('warning', _ObjTipoGasto, 'Eliminar', 'Confirma que desea eliminar (Dar de baja) ' + objSeleccionado.Nombre + '?', 'ConfirmacionEventoEliminar', 'Eliminar', 'Cancelar', 'red');
    } catch (e) {
        spinnerClose();
        alertAlerta('<b>El Tipo de Gasto no se ha eliminado</b> </br></br> ' + e);
    }
}, false);
document.addEventListener('ConfirmacionEventoEliminar', async function (e) {
    try {
        spinner();
        let objSeleccionado = e.detail;
        await objSeleccionado.Baja();
        await Inicio();
        spinnerClose();
        alertOk('El Tipo de Gasto se ha eliminado correctamente.');
    } catch (e) {
        spinnerClose();
        alertAlerta('El Tipo de Gasto no se ha eliminado. \n\n ' + e);
    }
}, false);