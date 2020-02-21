var _ObjCuentaContable;

$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Cuentas Contables');
        $("#SpanTituloGrillaDimensional").text('Cuentas Contables Registrados');
        $("#SpanTituloDimensional").text('Cuenta Contable');
        $("#SpanBtnImprimir").text('Imprimir Cuentas Contables');
        $("#SpanBtnNuevo").text('Nuevo');
        $("#SpanBtnGuardar").text('Guardar');
        Inicio();
    } catch (e) {
        alertAlerta(e);
    }
});
async function Inicio() {
    Nuevo_CuentaContable();
    await LlenarGrilla_CuentaContable();
}
function Limpiar_CuentaContable() {
    $(".DatoFormulario").val('');
}
function Nuevo_CuentaContable() {
    Limpiar_CuentaContable();
    _ObjCuentaContable = new CuentaContable;
    $("#TxtNombre").focus();
}
function Llenar_CuentaContable(Obj_CuentaContable) {
    Nuevo_CuentaContable();
    _ObjCuentaContable.IdEntidad = Obj_CuentaContable.IdEntidad;
    _ObjCuentaContable.Nombre = Obj_CuentaContable.Nombre;
    _ObjCuentaContable.Observaciones = Obj_CuentaContable.Observaciones;
    $("#TxtNombre").val(_ObjCuentaContable.Nombre);
    $("#TxtObservaciones").val(_ObjCuentaContable.Observaciones);

}
async function LlenarGrilla_CuentaContable() {
    let lista = await CuentaContable.TraerTodos();
    CuentaContable.ArmarGrilla(lista, 'GrillaRegistrados', 'SeleccionarCuentaContable', 'EliminarCuentaContable', '');
}

$('body').on('click', '#LinkBtnGuardar', async function (e) {
    try {
        spinner();
        _ObjCuentaContable.Nombre = $("#TxtNombre").val();
        _ObjCuentaContable.Observaciones = $("#TxtObservaciones").val();
        let Mensaje = '';
        if (_ObjCuentaContable.IdEntidad === 0) {
            // Es Alta
            await _ObjCuentaContable.Alta();
            Mensaje = 'agregado';
        } else {
            // Modifica
            await _ObjCuentaContable.Modifica();
            Mensaje = 'modificado';
        }
        await LlenarGrilla_CuentaContable();
        alertOk('La Cuenta Contable se ha ' + Mensaje + ' correctamente.');
        Nuevo_CuentaContable();
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#LinkBtnNuevo', async function (e) {
    try {
        Nuevo_CuentaContable();
    } catch (e) {
        alertAlerta(e);
    }
});
document.addEventListener('SeleccionarCuentaContable', async function (e) {
    try {
        let objSeleccionado = e.detail;
        Llenar_CuentaContable(objSeleccionado);
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('EliminarCuentaContable', async function (e) {
    try {
        let objSeleccionado = e.detail;
        console.log(objSeleccionado);
        if (objSeleccionado.IdEstado === 1) {
            throw 'La Cuenta Contable ya se encuentra eliminado.';
        }
        console.log(objSeleccionado);
        _ObjCuentaContable.IdEntidad = objSeleccionado.IdEntidad;
        _ObjCuentaContable.Nombre = objSeleccionado.Nombre;
        _ObjCuentaContable.Observaciones = objSeleccionado.Observaciones;

        PopUpConfirmarConCancelar('warning', _ObjCuentaContable, 'Eliminar', 'Confirma que desea eliminar (Dar de baja) ' + objSeleccionado.Nombre + '?', 'ConfirmacionEventoEliminar', 'Eliminar', 'Cancelar', 'red');
    } catch (e) {
        spinnerClose();
        alertAlerta('<b>La Cuenta Contable no se ha eliminado</b> </br></br> ' + e);
    }
}, false);
document.addEventListener('ConfirmacionEventoEliminar', async function (e) {
    try {
        spinner();
        let objSeleccionado = e.detail;
        await objSeleccionado.Baja();
        await Inicio();
        spinnerClose();
        alertOk('La Cuenta Contable se ha eliminado correctamente.');
    } catch (e) {
        spinnerClose();
        alertAlerta('La Cuenta Contable no se ha eliminado. \n\n ' + e);
    }
}, false);