$(document).ready(function () {
    try {
        let ObjU = JSON.parse(sessionStorage.getItem("User"));
        $("#SpanNombreFormulario").text(ObjU.Apellido + ', ' + ObjU.Nombre);
        $("#SpanBtnGuardar").text('Guardar Usuario');
        $("#SpanBtnModificarPassword").text('Modificar Contraseña');
        $("#SpanBtnGuardarPassword").text('Guardar Contraseña');
        Inicio(ObjU);
    } catch (e) {
        alertAlerta(e);
    }
});
function Inicio(us) {
    $("#TxtNombre").val(us.Nombre);
    $("#TxtApellido").val(us.Apellido);
    $("#TxtUserName").val(us.UserName);
    $("#TxtCorreoElectronico").val(us.CorreoElectronico);
    $("#TxtNroInterno").val(us.NroInterno);
}
$('body').on('click', '#LinkBtnModificarPassword', async function (e) {
    $("#DivPassword").css('display', 'block');
});
$('body').on('click', '#LinkBtnGuardar', async function (e) {
    try {
        spinner();
        let Temp = JSON.parse(sessionStorage.getItem("User"));
        let ObjU = new Usuario;
        ObjU.IdEntidad = Temp.IdEntidad;
        ObjU.Nombre = $('#TxtNombre').val();
        ObjU.Apellido = $('#TxtApellido').val();
        ObjU.UserName = $('#TxtUserName').val();
        ObjU.CorreoElectronico = $('#TxtCorreoElectronico').val();
        ObjU.NroInterno = $('#TxtNroInterno').val();
        await ObjU.Modifica();
        spinnerClose();
        PopUpConfirmarSinCancelar('success', 'Los cambios se han guardado correctamente', 'Ingrese nuevamente al Sitio.', 'EventoInicio', 'Aceptar');
            } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#LinkBtnGuardarPassword', async function (e) {
    try {
        spinner();
        let Anterior = $('#TxtPasswordAnterior').val();
        let Nueva = $('#TxtPasswordNueva').val();
        let Valida = $('#TxtPasswordValida').val();
        if (Nueva !== Valida) {
            $('#TxtPasswordValida').val('');
            $('#TxtPasswordValida').focus();
            throw 'La Nueva Contraseña debe ser igual a la que valida.';
        }
        let Temp = JSON.parse(sessionStorage.getItem("User"));
        let ObjU = new Usuario;
        ObjU.IdEntidad = Temp.IdEntidad;
        ObjU.ModificaPassword(Anterior, Nueva);
        spinnerClose();
        alertOk('La contraseña ha sido modificada correctamente. Recuerde utilizarla al ingresar la próxima vez.');
        $("#DivPassword").css('display', 'none');

    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
document.addEventListener('EventoInicio', async function (e) {
    try {
        window.location.href = 'http://aplicativosaaemm.dyndns.org/SitioTesoreria/Forms/Login/Frm_Login.aspx';
    } catch (e) {
        alertAlerta(e);
    }
}, false);
