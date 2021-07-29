$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Banco');
        $("#SpanTituloGrillaDimensional").text('Bancos Registrados');
        $("#SpanTituloDimensional").text('Banco');
        $("#SpanBtnImprimir").text('Imprimir Bancos');
        $("#SpanBtnNuevo").text('Nuevo');
        $("#SpanBtnGuardar").text('Guardar');
        Inicio();
    } catch (e) {
        alertAlerta(e);
    }
});

async function Inicio() {
    Nuevo_Banco();
    await LlenarGrilla_Banco();
}
function Limpiar_Banco() {
    $(".DatoFormulario").val('');
}
function Nuevo_Banco() {
    Limpiar_Banco();
    _ObjBanco = new Banco;
    $("#TxtNombre").focus();
}
function Llenar_Banco(Obj_Banco) {
    Nuevo_Banco();
    _ObjBanco.IdEntidad = Obj_Banco.IdEntidad;
    _ObjBanco.Nombre = Obj_Banco.Nombre;
    _ObjBanco.Observaciones = Obj_Banco.Observaciones;
    $("#TxtNombre").val(_ObjBanco.Nombre);
    $("#TxtObservaciones").val(_ObjBanco.Observaciones);

}
async function LlenarGrilla_Banco() {
    let lista = await Banco.TraerTodos();
    Banco.ArmarGrilla(lista, 'GrillaRegistrados', 'SeleccionarBanco', 'EliminarBanco', 'height:350px; overflow-y: scroll');
}

$('body').on('click', '#LinkBtnGuardar', async function (e) {
    try {
        spinner();
        _ObjBanco.Nombre = $("#TxtNombre").val();
        _ObjBanco.Observaciones = $("#TxtObservaciones").val();
        let Mensaje = '';
        if (_ObjBanco.IdEntidad === 0) {
            // Es Alta
            await _ObjBanco.Alta();
            Mensaje = 'agregado';
        } else {
            // Modifica
            await _ObjBanco.Modifica();
            Mensaje = 'modificado';
        }
        await LlenarGrilla_Banco();
        alertOk('El Banco se ha ' + Mensaje + ' correctamente.');
        Nuevo_Banco();
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#LinkBtnNuevo', async function (e) {
    try {
        Nuevo_Banco();
    } catch (e) {
        alertAlerta(e);
    }
});
document.addEventListener('SeleccionarBanco', async function (e) {
    try {
        let objSeleccionado = e.detail;
        Llenar_Banco(objSeleccionado);
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('EliminarBanco', async function (e) {
    try {
        let objSeleccionado = e.detail;
        console.log(objSeleccionado);
        if (objSeleccionado.IdEstado === 1) {
            throw 'El Banco ya se encuentra eliminado.';
        }
        console.log(objSeleccionado);
        _ObjBanco.IdEntidad = objSeleccionado.IdEntidad;
        _ObjBanco.Nombre = objSeleccionado.Nombre;
        _ObjBanco.Observaciones = objSeleccionado.Observaciones;

        PopUpConfirmarConCancelar('warning', _ObjBanco, 'Eliminar', 'Confirma que desea eliminar (Dar de baja) ' + objSeleccionado.Nombre + '?', 'ConfirmacionEventoEliminar', 'Eliminar', 'Cancelar', 'red');
    } catch (e) {
        spinnerClose();
        alertAlerta('<b>El Banco no se ha eliminado</b> </br></br> ' + e);
    }
}, false);
document.addEventListener('ConfirmacionEventoEliminar', async function (e) {
    try {
        spinner();
        let objSeleccionado = e.detail;
        await objSeleccionado.Baja();
        await Inicio();
        spinnerClose();
        alertOk('El Banco se ha eliminado correctamente.');
    } catch (e) {
        spinnerClose();
        alertAlerta('El Banco no se ha eliminado. \n\n ' + e);
    }
}, false);