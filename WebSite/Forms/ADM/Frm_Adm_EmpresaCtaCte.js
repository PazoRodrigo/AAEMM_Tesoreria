$(document).ready(async function () {
    try {
        $("#SpanNombreFormulario").text('Cuenta Corriente');
        $("#divCantRegistrosImprimir").css('display', 'none');
        await Inicio();
    } catch (e) {
        alertAlerta(e);
    }
});

async function Inicio() {
    LimpiarFormulario();
    await TipoMovimientoCtaCte.ArmarComboDH('CboDH', 'EventoSeleccionDH');
    await ArmarGrilla();
}

function LimpiarFormulario() {
}

async function ArmarGrilla() {
    let saldo = 1532600;
    $("#TxtSaldo").text(separadorMiles(saldo.toFixed(2)));
}
document.addEventListener(
    "EventoSeleccionDH",
    async function (e) {
        try {
            let objSeleccionado = e.detail;
            let lista = new Array;
            if (objSeleccionado.IdTipo > 0) {
                lista = await TipoMovimientoCtaCte.TraerTodosXTipo(objSeleccionado.IdTipo);
            }
            await TipoMovimientoCtaCte.ArmarCombo(lista, 'CboTipoMovimiento', 'selector', '', 'Seleccionar Tipo Movimiento...', 'form-control')
        } catch (err) {
            alertAlerta(err);
        }
    },
    false
);

$('body').on('click', '#BtnGuardar', async function (e) {
    try {
        spinner();
        let ObjGuardar = new MovimientoCtaCte;
        ObjGuardar.
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