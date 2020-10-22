var _ObjTipoMovimientoCtaCte;

$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Tipos de Movimiento Cta. Cte.');
        $("#SpanTituloGrillaDimensional").text('Tipos de Movimiento Cta. Cte. Registrados');
        $("#SpanTituloDimensional").text('Tipo de Movimiento Cta. Cte.');
        $("#SpanBtnImprimir").text('Imprimir Tipos de Movimiento Cta. Cte.');
        $("#SpanBtnNuevo").text('Nuevo');
        $("#SpanBtnGuardar").text('Guardar');
        Inicio();
    } catch (e) {
        alertAlerta(e);
    }
});

async function Inicio() {
    Nuevo_TipoMovimientoCtaCte();
    await TipoMovimientoCtaCte.ArmarComboDH('CboDH');
    await LlenarGrilla_TipoMovimientoCtaCte();
}
function Limpiar_TipoMovimientoCtaCte() {
    $("#_CboDH").val(0);
    $(".DatoFormulario").val('');
}
function Nuevo_TipoMovimientoCtaCte() {
    Limpiar_TipoMovimientoCtaCte();
    _ObjTipoMovimientoCtaCte = new TipoMovimientoCtaCte;
    $("#TxtNombre").focus();
}
function Llenar_TipoMovimientoCtaCte(Obj_TipoMovimientoCtaCte) {
    Nuevo_TipoMovimientoCtaCte();
    _ObjTipoMovimientoCtaCte.IdEntidad = Obj_TipoMovimientoCtaCte.IdEntidad;
    _ObjTipoMovimientoCtaCte.Nombre = Obj_TipoMovimientoCtaCte.Nombre;
    _ObjTipoMovimientoCtaCte.Observaciones = Obj_TipoMovimientoCtaCte.Observaciones;
    $("#TxtNombre").val(_ObjTipoMovimientoCtaCte.Nombre);
    $("#_CboDH").val(Obj_TipoMovimientoCtaCte.IdTipo);
    $("#TxtObservaciones").val(_ObjTipoMovimientoCtaCte.Observaciones);

}
async function LlenarGrilla_TipoMovimientoCtaCte() {
    await TipoMovimientoCtaCte.ArmarGrilla(await TipoMovimientoCtaCte.TraerTodos(), 'GrillaRegistrados', 'SeleccionarTipoMovimientoCtaCte', 'EliminarTipoMovimientoCtaCte', 'height:350px; overflow-y: scroll');
}

$('body').on('click', '#LinkBtnGuardar', async function (e) {
    try {
        spinner();
        _ObjTipoMovimientoCtaCte.Nombre = $("#TxtNombre").val();
        _ObjTipoMovimientoCtaCte.IdTipo = $("#_CboDH").val();
        _ObjTipoMovimientoCtaCte.Observaciones = $("#TxtObservaciones").val();
        let Mensaje = '';
        if (_ObjTipoMovimientoCtaCte.IdEntidad === 0) {
            // Es Alta
            await _ObjTipoMovimientoCtaCte.Alta();
            Mensaje = 'agregado';
        } else {
            // Modifica
            await _ObjTipoMovimientoCtaCte.Modifica();
            Mensaje = 'modificado';
        }
        await LlenarGrilla_TipoMovimientoCtaCte();
        alertOk('El Tipo de Movimiento de Cta. Cte. se ha ' + Mensaje + ' correctamente.');
        Nuevo_TipoMovimientoCtaCte();
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#LinkBtnNuevo', async function (e) {
    try {
        Nuevo_TipoMovimientoCtaCte();
    } catch (e) {
        alertAlerta(e);
    }
});
document.addEventListener('SeleccionarTipoMovimientoCtaCte', async function (e) {
    try {
        let objSeleccionado = e.detail;
        Llenar_TipoMovimientoCtaCte(objSeleccionado);
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('EliminarTipoMovimientoCtaCte', async function (e) {
    try {
        let objSeleccionado = e.detail;
        console.log(objSeleccionado);
        if (objSeleccionado.IdEstado === 1) {
            throw 'El Tipo de Movimiento de Cta. Cte. ya se encuentra eliminado.';
        }
        console.log(objSeleccionado);
        _ObjTipoMovimientoCtaCte.IdEntidad = objSeleccionado.IdEntidad;
        _ObjTipoMovimientoCtaCte.Nombre = objSeleccionado.Nombre;
        _ObjTipoMovimientoCtaCte.Observaciones = objSeleccionado.Observaciones;

        PopUpConfirmarConCancelar('warning', _ObjTipoMovimientoCtaCte, 'Eliminar', 'Confirma que desea eliminar (Dar de baja) ' + objSeleccionado.Nombre + '?', 'ConfirmacionEventoEliminar', 'Eliminar', 'Cancelar', 'red');
    } catch (e) {
        spinnerClose();
        alertAlerta('<b>El Tipo de Movimiento de Cta. Cte. no se ha eliminado</b> </br></br> ' + e);
    }
}, false);
document.addEventListener('ConfirmacionEventoEliminar', async function (e) {
    try {
        spinner();
        let objSeleccionado = e.detail;
        await objSeleccionado.Baja();
        await Inicio();
        spinnerClose();
        alertOk('El Tipo de Movimiento de Cta. Cte. se ha eliminado correctamente.');
    } catch (e) {
        spinnerClose();
        alertAlerta('El Tipo de Movimiento de Cta. Cte. no se ha eliminado. \n\n ' + e);
    }
}, false);