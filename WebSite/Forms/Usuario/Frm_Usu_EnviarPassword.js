$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Enviar Contraseña');
        let ctlLogin = document.getElementById("ctlLogin");
        ctlLogin.style.visibility = "visible";
        $("#SpanBtnEnviarPassword").text('Enviar Contraseña');
        $("#TxtAcceso").focus();
        Inicio();
    } catch (e) {
        alertAlerta(e);
    }
});
async function Inicio() {
    LimpiarFormulario();
}
function LimpiarFormulario() {
    $(".DatoFormulario").val('');
}
$('body').on('click', '#LinkBtnEnviarPassword', async function (e) {
    try {
        let textoIngresado = $("#TxtAcceso").val();
        if (parseInt(textoIngresado.length) === 0) {
            throw 'Debe ingresar su UserName o su Correo Electrónico registrado';
        }
        spinner();
        await Usuario.EnviarPassword(textoIngresado);
        spinnerClose();
        PopUpConfirmarSinCancelar('success', 'Un mail le llegará con su clave. Revise el SPAM.', 'Ingrese nuevamente al Sitio.', 'EventoInicio', 'Aceptar');

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