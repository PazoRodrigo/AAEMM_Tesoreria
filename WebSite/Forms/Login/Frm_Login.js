var _Us;
$(document).ready(function () {
    try {
        $("#NombreFormulario").text('Acceso al Sistema');
        Inicio();
    } catch (e) {
        alertAlerta(e);
    }
});
function Inicio() {
    sessionStorage.removeItem('User');
    LimpiarFormulario();
}
function LimpiarFormulario() {
    $('#txtUser').val('');
    $('#txtPass').val('');
    $('#txtUser').focus();
}
$('body').on('click', '#BtnLogin', async function (e) {
    await AlmacenarUsuario();
});
$('body').on('keyup', '#txtUser', async function (e) {
    if (e.keyCode == 13) {
        await AlmacenarUsuario();
    }
});
$('body').on('keyup', '#txtPass', async function (e) {
    if (e.keyCode == 13) {
        await AlmacenarUsuario();
    }
});
async function AlmacenarUsuario() {
    try {
        let u = $("#txtUser").val();
        let p = $("#txtPass").val();
        if (u.length == 0 || p.length == 0) {
            throw ('Debe ingresar Usuario y Contraseña');
        }
        spinner();
        let usuValido = await Usuario.AccederAlSistema(u, p);
        _Us = usuValido;
        spinnerClose();
        sessionStorage.setItem("User", JSON.stringify(usuValido));
        window.location.href = 'http://localhost:14162/Forms/Frm_Indicadores.aspx';
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
}
