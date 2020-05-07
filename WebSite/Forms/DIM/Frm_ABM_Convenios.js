var _ObjConvenio;

$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Convenios');
        $("#SpanTituloGrillaDimensional").text('Convenios Registrados');
        $("#SpanTituloDimensional").text('Convenio');
        $("#SpanBtnImprimir").text('Imprimir Convenios');
        $("#SpanBtnNuevo").text('Nuevo');
        $("#SpanBtnGuardar").text('Guardar');
        Inicio();
    } catch (e) {
        alertAlerta(e);
    }
});
async function Inicio() {
    Nuevo_Convenio();
    await LlenarGrilla_Convenio();
}
function Limpiar_Convenio() {
    $(".DatoFormulario").val('');
}
function Nuevo_Convenio() {
    Limpiar_Convenio();
    _ObjConvenio = new Convenio;
    $("#TxtNombre").focus();
}
function Llenar_Convenio(Obj_Convenio) {
    Nuevo_Convenio();
    _ObjConvenio.IdEntidad = Obj_Convenio.IdEntidad;
    _ObjConvenio.Nombre = Obj_Convenio.Nombre;
    _ObjConvenio.Observaciones = Obj_Convenio.Observaciones;
    _ObjConvenio.PorcEmpresa = Obj_Convenio.PorcEmpresa;
    _ObjConvenio.PorcAfiliado = Obj_Convenio.PorcAfiliado;
    _ObjConvenio.PorcNoAfiliado = Obj_Convenio.PorcNoAfiliado;
    _ObjConvenio.PorcOtro = Obj_Convenio.PorcOtro;
    console.log(_ObjConvenio);
    $("#TxtNombre").val(_ObjConvenio.Nombre);
    $("#TxtObservaciones").val(_ObjConvenio.Observaciones);
    $("#TxtPorcEmpresa").val(_ObjConvenio.PorcEmpresa);
    $("#TxtPorcAfiliado").val(_ObjConvenio.PorcAfiliado);
    $("#TxtPorcNoAfiliado").val(_ObjConvenio.PorcNoAfiliado);
    $("#TxtPorcOtro").val(_ObjConvenio.PorcOtro);
}
async function LlenarGrilla_Convenio() {
    let lista = await Convenio.TraerTodos();
    Convenio.ArmarGrilla(lista, 'GrillaRegistrados', 'SeleccionarConvenio', 'EliminarConvenio', '');
}

$('body').on('click', '#LinkBtnGuardar', async function (e) {
    try {
        spinner();
        _ObjConvenio.Nombre = $("#TxtNombre").val();
        _ObjConvenio.Observaciones = $("#TxtObservaciones").val();
        if ($("#TxtPorcEmpresa").val().length > 0) {
            _ObjConvenio.PorcEmpresa = parseFloat($("#TxtPorcEmpresa").val());
        }
        if ($("#TxtPorcAfiliado").val().length > 0) {
            _ObjConvenio.PorcAfiliado = parseFloat($("#TxtPorcAfiliado").val());
        }
        if ($("#TxtPorcNoAfiliado").val().length > 0) {
            _ObjConvenio.PorcNoAfiliado = parseFloat($("#TxtPorcNoAfiliado").val());
        }
        if ($("#TxtPorcOtro").val().length > 0) {
            _ObjConvenio.PorcOtro = parseFloat($("#TxtPorcOtro").val());
        }
        console.log(_ObjConvenio);
        let Mensaje = '';
        if (_ObjConvenio.IdEntidad === 0) {
            // Es Alta
            await _ObjConvenio.Alta();
            Mensaje = 'agregado';
        } else {
            // Modifica
            await _ObjConvenio.Modifica();
            Mensaje = 'modificado';
        }
        await LlenarGrilla_Convenio();
        alertOk('El Convenio se ha ' + Mensaje + ' correctamente.');
        Nuevo_Convenio();
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#LinkBtnNuevo', async function (e) {
    try {
        Nuevo_Convenio();
    } catch (e) {
        alertAlerta(e);
    }
});
document.addEventListener('SeleccionarConvenio', async function (e) {
    try {
        let objSeleccionado = e.detail;
        Llenar_Convenio(objSeleccionado);
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('EliminarConvenio', async function (e) {
    try {
        let objSeleccionado = e.detail;
        console.log(objSeleccionado);
        if (objSeleccionado.IdEstado === 1) {
            throw 'El Convenio ya se encuentra eliminado.';
        }
        console.log(objSeleccionado);
        _ObjConvenio.IdEntidad = objSeleccionado.IdEntidad;
        _ObjConvenio.Nombre = objSeleccionado.Nombre;
        _ObjConvenio.Observaciones = objSeleccionado.Observaciones;

        PopUpConfirmarConCancelar('warning', _ObjConvenio, 'Eliminar', 'Confirma que desea eliminar (Dar de baja) ' + objSeleccionado.Nombre + '?', 'ConfirmacionEventoEliminar', 'Eliminar', 'Cancelar', 'red');
    } catch (e) {
        spinnerClose();
        alertAlerta('<b>El Convenio no se ha eliminado</b> </br></br> ' + e);
    }
}, false);
document.addEventListener('ConfirmacionEventoEliminar', async function (e) {
    try {
        spinner();
        let objSeleccionado = e.detail;
        await objSeleccionado.Baja();
        await Inicio();
        spinnerClose();
        alertOk('El Convenio se ha eliminado correctamente.');
    } catch (e) {
        spinnerClose();
        alertAlerta('El Convenio no se ha eliminado. \n\n ' + e);
    }
}, false);