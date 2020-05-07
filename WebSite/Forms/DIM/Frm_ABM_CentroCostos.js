var _ObjCentroCosto;

$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Centros de Costo');
        $("#SpanTituloGrillaDimensional").text('Centros de Costo Registrados');
        $("#SpanTituloDimensional").text('Centros de Costo');
        $("#SpanBtnImprimir").text('Imprimir Centros de Costo');
        $("#SpanBtnNuevo").text('Nuevo');
        $("#SpanBtnGuardar").text('Guardar');
        Inicio();
    } catch (e) {
        alertAlerta(e);
    }
});
async function Inicio() {
    Nuevo_CentroCosto();
    await LlenarGrilla_CentroCosto();
}
function Limpiar_CentroCosto() {
    $(".DatoFormulario").val('');
}
function Nuevo_CentroCosto() {
    Limpiar_CentroCosto();
    _ObjCentroCosto = new CentroCosto;
    $("#TxtNombre").focus();
}
function Llenar_CentroCosto(Obj_CentroCosto) {
    Nuevo_CentroCosto();
    _ObjCentroCosto.IdEntidad = Obj_CentroCosto.IdEntidad;
    _ObjCentroCosto.Nombre = Obj_CentroCosto.Nombre;
    _ObjCentroCosto.Observaciones = Obj_CentroCosto.Observaciones;
    $("#TxtNombre").val(_ObjCentroCosto.Nombre);
    $("#TxtObservaciones").val(_ObjCentroCosto.Observaciones);

}
async function LlenarGrilla_CentroCosto() {
    let lista = await CentroCosto.TraerTodos();
    CentroCosto.ArmarGrilla(lista, 'GrillaRegistrados', 'SeleccionarCentroCosto', 'EliminarCentroCosto', 'height:350px; overflow-y: scroll');
}

$('body').on('click', '#LinkBtnGuardar', async function (e) {
    try {
        spinner();
        _ObjCentroCosto.Nombre = $("#TxtNombre").val();
        _ObjCentroCosto.Observaciones = $("#TxtObservaciones").val();
        let Mensaje = '';
        if (_ObjCentroCosto.IdEntidad === 0) {
            // Es Alta
            await _ObjCentroCosto.Alta();
            Mensaje = 'agregado';
        } else {
            // Modifica
            await _ObjCentroCosto.Modifica();
            Mensaje = 'modificado';
        }
        await LlenarGrilla_CentroCosto();
        alertOk('El Centro de Costo se ha ' + Mensaje + ' correctamente.');
        Nuevo_CentroCosto();
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#LinkBtnNuevo', async function (e) {
    try {
        Nuevo_CentroCosto();
    } catch (e) {
        alertAlerta(e);
    }
});
document.addEventListener('SeleccionarCentroCosto', async function (e) {
    try {
        let objSeleccionado = e.detail;
        Llenar_CentroCosto(objSeleccionado);
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('EliminarCentroCosto', async function (e) {
    try {
        let objSeleccionado = e.detail;
        console.log(objSeleccionado);
        if (objSeleccionado.IdEstado === 1) {
            throw 'El Centro de Costo ya se encuentra eliminado.';
        }
        console.log(objSeleccionado);
        _ObjCentroCosto.IdEntidad = objSeleccionado.IdEntidad;
        _ObjCentroCosto.Nombre = objSeleccionado.Nombre;
        _ObjCentroCosto.Observaciones = objSeleccionado.Observaciones;

        PopUpConfirmarConCancelar('warning', _ObjCentroCosto, 'Eliminar', 'Confirma que desea eliminar (Dar de baja) ' + objSeleccionado.Nombre + '?', 'ConfirmacionEventoEliminar', 'Eliminar', 'Cancelar', 'red');
    } catch (e) {
        spinnerClose();
        alertAlerta('<b>El Centro de Costo no se ha eliminado</b> </br></br> ' + e);
    }
}, false);
document.addEventListener('ConfirmacionEventoEliminar', async function (e) {
    try {
        spinner();
        let objSeleccionado = e.detail;
        await objSeleccionado.Baja();
        await Inicio();
        spinnerClose();
        alertOk('El Centro de Costo se ha eliminado correctamente.');
    } catch (e) {
        spinnerClose();
        alertAlerta('El Centro de Costo no se ha eliminado. \n\n ' + e);
    }
}, false);