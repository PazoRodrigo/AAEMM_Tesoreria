var _ObjTipoPago;

$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Tipos de Pago');
        $("#SpanTituloGrillaDimensional").text('Tipos de Pago Registrados');
        $("#SpanTituloDimensional").text('Tipo de Pago');
        $("#SpanBtnImprimir").text('Imprimir Tipos de Pago');
        $("#SpanBtnNuevo").text('Nuevo');
        $("#SpanBtnGuardar").text('Guardar');
        Inicio();
    } catch (e) {
        alertAlerta(e);
    }
});
async function Inicio() {
    Nuevo_TipoPago();
    await LlenarGrilla_TipoPago();
}
function Limpiar_TipoPago() {
    $(".DatoFormulario").val('');
}
function Nuevo_TipoPago() {
    Limpiar_TipoPago();
    _ObjTipoPago = new TipoPago;
    $("#TxtNombre").focus();
}
function Llenar_TipoPago(Obj_TipoPago) {
    Nuevo_TipoPago();
    _ObjTipoPago.IdEntidad = Obj_TipoPago.IdEntidad;
    _ObjTipoPago.Nombre = Obj_TipoPago.Nombre;
    _ObjTipoPago.Observaciones = Obj_TipoPago.Observaciones;
    $("#TxtNombre").val(_ObjTipoPago.Nombre);
    $("#TxtObservaciones").val(_ObjTipoPago.Observaciones);

}
async function LlenarGrilla_TipoPago() {
    let lista = await TipoPago.TraerTodos();
    TipoPago.ArmarGrilla(lista, 'GrillaRegistrados', 'SeleccionarTipoPago', 'EliminarTipoPago', '');
}

$('body').on('click', '#LinkBtnGuardar', async function (e) {
    try {
        spinner();
        _ObjTipoPago.Nombre = $("#TxtNombre").val();
        _ObjTipoPago.Observaciones = $("#TxtObservaciones").val();
        let Mensaje = '';
        if (_ObjTipoPago.IdEntidad === 0) {
            // Es Alta
            await _ObjTipoPago.Alta();
            Mensaje = 'agregado';
        } else {
            // Modifica
            await _ObjTipoPago.Modifica();
            Mensaje = 'modificado';
        }
        await LlenarGrilla_TipoPago();
        alertOk('El Tipo de Pago se ha ' + Mensaje + ' correctamente.');
        Nuevo_TipoPago();
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#LinkBtnNuevo', async function (e) {
    try {
        Nuevo_TipoPago();
    } catch (e) {
        alertAlerta(e);
    }
});
document.addEventListener('SeleccionarTipoPago', async function (e) {
    try {
        let objSeleccionado = e.detail;
        Llenar_TipoPago(objSeleccionado);
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('EliminarTipoPago', async function (e) {
    try {
        let objSeleccionado = e.detail;
        console.log(objSeleccionado);
        if (objSeleccionado.IdEstado === 1) {
            throw 'El Tipo de Pago ya se encuentra eliminado.';
        }
        console.log(objSeleccionado);
        _ObjTipoPago.IdEntidad = objSeleccionado.IdEntidad;
        _ObjTipoPago.Nombre = objSeleccionado.Nombre;
        _ObjTipoPago.Observaciones = objSeleccionado.Observaciones;

        PopUpConfirmarConCancelar('warning', _ObjTipoPago, 'Eliminar', 'Confirma que desea eliminar (Dar de baja) ' + objSeleccionado.Nombre + '?', 'ConfirmacionEventoEliminar', 'Eliminar', 'Cancelar', 'red');
    } catch (e) {
        spinnerClose();
        alertAlerta('<b>El Tipo de Pago no se ha eliminado</b> </br></br> ' + e);
    }
}, false);
document.addEventListener('ConfirmacionEventoEliminar', async function (e) {
    try {
        spinner();
        let objSeleccionado = e.detail;
        await objSeleccionado.Baja();
        await Inicio();
        spinnerClose();
        alertOk('El Tipo de Pago se ha eliminado correctamente.');
    } catch (e) {
        spinnerClose();
        alertAlerta('El Tipo de Pago no se ha eliminado. \n\n ' + e);
    }
}, false);