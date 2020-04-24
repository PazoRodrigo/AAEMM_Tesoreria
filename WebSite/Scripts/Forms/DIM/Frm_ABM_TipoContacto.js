var _ObjTipoContacto;

$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Tipos de Contacto');
        $("#SpanTituloGrillaDimensional").text('Tipos de Contacto Registrados');
        $("#SpanTituloDimensional").text('Tipo de Contacto');
        $("#SpanBtnImprimir").text('Imprimir Tipos de Contacto');
        $("#SpanBtnNuevo").text('Nuevo');
        $("#SpanBtnGuardar").text('Guardar');
        Inicio();
    } catch (e) {
        alertAlerta(e);
    }
});
async function Inicio() {
    Nuevo_TipoContacto();
    await LlenarGrilla_TipoContacto();
}
function Limpiar_TipoContacto() {
    $(".DatoFormulario").val('');
}
function Nuevo_TipoContacto() {
    Limpiar_TipoContacto();
    _ObjTipoContacto = new TipoContacto;
    $("#TxtNombre").focus();
}
function Llenar_TipoContacto(Obj_TipoContacto) {
    Nuevo_TipoContacto();
    _ObjTipoContacto.IdEntidad = Obj_TipoContacto.IdEntidad;
    _ObjTipoContacto.Nombre = Obj_TipoContacto.Nombre;
    _ObjTipoContacto.Observaciones = Obj_TipoContacto.Observaciones;
    $("#TxtNombre").val(_ObjTipoContacto.Nombre);
    $("#TxtObservaciones").val(_ObjTipoContacto.Observaciones);

}
async function LlenarGrilla_TipoContacto() {
    let lista = await TipoContacto.TraerTodos();
    TipoContacto.ArmarGrilla(lista, 'GrillaRegistrados', 'SeleccionarTipoContacto', 'EliminarTipoContacto', 'height:350px; overflow-y: scroll');
}

$('body').on('click', '#LinkBtnGuardar', async function (e) {
    try {
        spinner();
        _ObjTipoContacto.Nombre = $("#TxtNombre").val();
        _ObjTipoContacto.Observaciones = $("#TxtObservaciones").val();
        let Mensaje = '';
        if (_ObjTipoContacto.IdEntidad === 0) {
            // Es Alta
            await _ObjTipoContacto.Alta();
            Mensaje = 'agregado';
        } else {
            // Modifica
            await _ObjTipoContacto.Modifica();
            Mensaje = 'modificado';
        }
        await LlenarGrilla_TipoContacto();
        alertOk('El Tipo de Contacto se ha ' + Mensaje + ' correctamente.');
        Nuevo_TipoContacto();
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#LinkBtnNuevo', async function (e) {
    try {
        Nuevo_TipoContacto();
    } catch (e) {
        alertAlerta(e);
    }
});
document.addEventListener('SeleccionarTipoContacto', async function (e) {
    try {
        let objSeleccionado = e.detail;
        Llenar_TipoContacto(objSeleccionado);
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('EliminarTipoContacto', async function (e) {
    try {
        let objSeleccionado = e.detail;
        console.log(objSeleccionado);
        if (objSeleccionado.IdEstado === 1) {
            throw 'El Tipo de Contacto ya se encuentra eliminado.';
        }
        console.log(objSeleccionado);
        _ObjTipoContacto.IdEntidad = objSeleccionado.IdEntidad;
        _ObjTipoContacto.Nombre = objSeleccionado.Nombre;
        _ObjTipoContacto.Observaciones = objSeleccionado.Observaciones;

        PopUpConfirmarConCancelar('warning', _ObjTipoContacto, 'Eliminar', 'Confirma que desea eliminar (Dar de baja) ' + objSeleccionado.Nombre + '?', 'ConfirmacionEventoEliminar', 'Eliminar', 'Cancelar', 'red');
    } catch (e) {
        spinnerClose();
        alertAlerta('<b>El Tipo de Contacto no se ha eliminado</b> </br></br> ' + e);
    }
}, false);
document.addEventListener('ConfirmacionEventoEliminar', async function (e) {
    try {
        spinner();
        let objSeleccionado = e.detail;
        await objSeleccionado.Baja();
        await Inicio();
        spinnerClose();
        alertOk('El Tipo de Contacto se ha eliminado correctamente.');
    } catch (e) {
        spinnerClose();
        alertAlerta('El Tipo de Contacto no se ha eliminado. \n\n ' + e);
    }
}, false);