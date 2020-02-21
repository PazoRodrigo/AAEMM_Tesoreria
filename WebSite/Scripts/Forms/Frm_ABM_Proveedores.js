var _ObjProveedor;

$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Proveedores');
        $("#SpanTituloGrillaDimensional").text('Proveedores Registrados');
        $("#SpanTituloDimensional").text('Proveedor');
        $("#SpanBtnImprimir").text('Imprimir Proveedores');
        $("#SpanBtnNuevo").text('Nuevo');
        $("#SpanBtnGuardar").text('Guardar');
        Inicio();
    } catch (e) {
        alertAlerta(e);
    }
});
async function Inicio() {
    Nuevo_Proveedor();
    await LlenarGrilla_Proveedor();
}
function Limpiar_Proveedor() {
    $(".DatoFormulario").val('');
}
function Nuevo_Proveedor() {
    Limpiar_Proveedor();
    _ObjProveedor = new Proveedor;
    $("#TxtNombre").focus();
}
function Llenar_Proveedor(Obj_Proveedor) {
    Nuevo_Proveedor();
    _ObjProveedor.IdEntidad = Obj_Proveedor.IdEntidad;
    _ObjProveedor.Nombre = Obj_Proveedor.Nombre;
    _ObjProveedor.Observaciones = Obj_Proveedor.Observaciones;
    $("#TxtNombre").val(_ObjProveedor.Nombre);
    $("#TxtObservaciones").val(_ObjProveedor.Observaciones);

}
async function LlenarGrilla_Proveedor() {
    let lista = await Proveedor.TraerTodos();
    Proveedor.ArmarGrilla(lista, 'GrillaRegistrados', 'SeleccionarProveedor', 'EliminarProveedor', '');
}

$('body').on('click', '#LinkBtnGuardar', async function (e) {
    try {
        spinner();
        _ObjProveedor.Nombre = $("#TxtNombre").val();
        _ObjProveedor.Observaciones = $("#TxtObservaciones").val();
        let Mensaje = '';
        if (_ObjProveedor.IdEntidad === 0) {
            // Es Alta
            await _ObjProveedor.Alta();
            Mensaje = 'agregado';
        } else {
            // Modifica
            await _ObjProveedor.Modifica();
            Mensaje = 'modificado';
        }
        await LlenarGrilla_Proveedor();
        alertOk('El Proveedor se ha ' + Mensaje + ' correctamente.');
        Nuevo_Proveedor();
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#LinkBtnNuevo', async function (e) {
    try {
        Nuevo_Proveedor();
    } catch (e) {
        alertAlerta(e);
    }
});
document.addEventListener('SeleccionarProveedor', async function (e) {
    try {
        let objSeleccionado = e.detail;
        Llenar_Proveedor(objSeleccionado);
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('EliminarProveedor', async function (e) {
    try {
        let objSeleccionado = e.detail;
        console.log(objSeleccionado);
        if (objSeleccionado.IdEstado === 1) {
            throw 'El Proveedor ya se encuentra eliminado.';
        }
        console.log(objSeleccionado);
        _ObjProveedor.IdEntidad = objSeleccionado.IdEntidad;
        _ObjProveedor.Nombre = objSeleccionado.Nombre;
        _ObjProveedor.Observaciones = objSeleccionado.Observaciones;

        PopUpConfirmarConCancelar('warning', _ObjProveedor, 'Eliminar', 'Confirma que desea eliminar (Dar de baja) ' + objSeleccionado.Nombre + '?', 'ConfirmacionEventoEliminar', 'Eliminar', 'Cancelar', 'red');
    } catch (e) {
        spinnerClose();
        alertAlerta('<b>El Proveedor no se ha eliminado</b> </br></br> ' + e);
    }
}, false);
document.addEventListener('ConfirmacionEventoEliminar', async function (e) {
    try {
        spinner();
        let objSeleccionado = e.detail;
        await objSeleccionado.Baja();
        await Inicio();
        spinnerClose();
        alertOk('El Proveedor se ha eliminado correctamente.');
    } catch (e) {
        spinnerClose();
        alertAlerta('El Proveedor no se ha eliminado. \n\n ' + e);
    }
}, false);