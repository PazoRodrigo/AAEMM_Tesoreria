var _ObjTipoDomicilio;

$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Tipos de Domicilio');
        $("#SpanTituloGrillaDimensional").text('Tipos de Domicilio Registrados');
        $("#SpanTituloDimensional").text('Tipo de Domicilio');
        $("#SpanBtnImprimir").text('Imprimir Tipos de Domicilio');
        $("#SpanBtnNuevo").text('Nuevo');
        $("#SpanBtnGuardar").text('Guardar');
        Inicio();
    } catch (e) {
        alertAlerta(e);
    }
});
async function Inicio() {
    Nuevo_TipoDomicilio();
    await LlenarGrilla_TipoDomicilio();
}
function Limpiar_TipoDomicilio() {
    $(".DatoFormulario").val('');
}
function Nuevo_TipoDomicilio() {
    Limpiar_TipoDomicilio();
    _ObjTipoDomicilio = new TipoDomicilio;
    $("#TxtNombre").focus();
}
function Llenar_TipoDomicilio(Obj_TipoDomicilio) {
    Nuevo_TipoDomicilio();
    _ObjTipoDomicilio.IdEntidad = Obj_TipoDomicilio.IdEntidad;
    _ObjTipoDomicilio.Nombre = Obj_TipoDomicilio.Nombre;
    _ObjTipoDomicilio.Observaciones = Obj_TipoDomicilio.Observaciones;
    $("#TxtNombre").val(_ObjTipoDomicilio.Nombre);
    $("#TxtObservaciones").val(_ObjTipoDomicilio.Observaciones);

}
async function LlenarGrilla_TipoDomicilio() {
    let lista = await TipoDomicilio.TraerTodos();
    TipoDomicilio.ArmarGrilla(lista, 'GrillaRegistrados', 'SeleccionarTipoDomicilio', 'EliminarTipoDomicilio', '');
}

$('body').on('click', '#LinkBtnGuardar', async function (e) {
    try {
        spinner();
        _ObjTipoDomicilio.Nombre = $("#TxtNombre").val();
        _ObjTipoDomicilio.Observaciones = $("#TxtObservaciones").val();
        let Mensaje = '';
        if (_ObjTipoDomicilio.IdEntidad === 0) {
            // Es Alta
            await _ObjTipoDomicilio.Alta();
            Mensaje = 'agregado';
        } else {
            // Modifica
            await _ObjTipoDomicilio.Modifica();
            Mensaje = 'modificado';
        }
        await LlenarGrilla_TipoDomicilio();
        alertOk('El Tipo de Domicilio se ha ' + Mensaje + ' correctamente.');
        Nuevo_TipoDomicilio();
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#LinkBtnNuevo', async function (e) {
    try {
        Nuevo_TipoDomicilio();
    } catch (e) {
        alertAlerta(e);
    }
});
document.addEventListener('SeleccionarTipoDomicilio', async function (e) {
    try {
        let objSeleccionado = e.detail;
        Llenar_TipoDomicilio(objSeleccionado);
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('EliminarTipoDomicilio', async function (e) {
    try {
        let objSeleccionado = e.detail;
        console.log(objSeleccionado);
        if (objSeleccionado.IdEstado === 1) {
            throw 'El Tipo de Domicilio ya se encuentra eliminado.';
        }
        console.log(objSeleccionado);
        _ObjTipoDomicilio.IdEntidad = objSeleccionado.IdEntidad;
        _ObjTipoDomicilio.Nombre = objSeleccionado.Nombre;
        _ObjTipoDomicilio.Observaciones = objSeleccionado.Observaciones;

        PopUpConfirmarConCancelar('warning', _ObjTipoDomicilio, 'Eliminar', 'Confirma que desea eliminar (Dar de baja) ' + objSeleccionado.Nombre + '?', 'ConfirmacionEventoEliminar', 'Eliminar', 'Cancelar', 'red');
    } catch (e) {
        spinnerClose();
        alertAlerta('<b>El Tipo de Domicilio no se ha eliminado</b> </br></br> ' + e);
    }
}, false);
document.addEventListener('ConfirmacionEventoEliminar', async function (e) {
    try {
        spinner();
        let objSeleccionado = e.detail;
        await objSeleccionado.Baja();
        await Inicio();
        spinnerClose();
        alertOk('El Tipo de Domicilio se ha eliminado correctamente.');
    } catch (e) {
        spinnerClose();
        alertAlerta('El Tipo de Domicilio no se ha eliminado. \n\n ' + e);
    }
}, false);