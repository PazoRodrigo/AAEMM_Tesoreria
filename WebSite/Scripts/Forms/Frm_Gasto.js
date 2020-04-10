var _ObjGasto;
var _ObjComprobante;
var _ListaG;
var _ListaC;

$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Gastos');
        $("#SpanBtnComprobante").text('Nuevo Comprobante');
        $("#SpanBtnGasto").text('Nuevo Gasto');
        $("#SpanBtnBuscar").text('Buscar');
        $("#SpanBtnGuardarComprobante").text('Guardar Comprobante');
        $("#SpanBtnCerrarGasto").text('Cerrar Gasto');
        $("#SpanBtnImprimirGasto").text('Imprimir Gasto');
        Inicio();
    } catch (e) {
        alertAlerta(e);
    }
});
async function Inicio() {
    _ObjGasto = new Gasto();
    _ObjComprobante = new Comprobante();
    await Gasto.Refresh();
    _ListaG = await Gasto.TraerGastosAbiertos();
    await LlenarGrillaGasto();
    if (parseInt(_ListaG.lenght) === 1) {
        _ObjComprobante.IdGasto = ListaGastos[0];
        await LlenarGasto();
    }
    await LimpiarComprobante();
    MostrarSolapaGasto();
}
async function LlenarGrillaGasto() {
    Gasto.ArmarGrilla(_ListaG, 'GrillaGastosRegistrados', 'EventoSeleccionarGasto', 'EventoEliminarGasto', 'height:300px; overflow-y: scroll');
}
async function LlenarGrillaComprobante() {
    Comprobante.ArmarGrilla(_ListaC, 'GrillaComprobantesRegistrados', 'EventoSeleccionarComprobante', 'EventoEliminarComprobante', 'height:300px; overflow-y: scroll');
}
async function LlenarCboOriginario() {
    let lista = await OriginarioGasto.TraerTodos();
    OriginarioGasto.ArmarCombo(lista, 'CboOriginarioGasto', 'SelectorOriginarioGasto', 'EventoSeleccionOriginarioGasto', 'Seleccione', '');
}
async function LlenarCboProveedor() {
    let lista = await Proveedor.TraerTodos();
    Proveedor.ArmarCombo(lista, 'CboProveedor', 'SelectorProveedor', 'EventoSeleccionProveedor', 'Seleccione', '');
}
async function LlenarCboCentroCosto() {
    let lista = await CentroCosto.TraerTodos();
    CentroCosto.ArmarCombo(lista, 'CboCentroCosto', 'SelectorCentroCosto', 'EventoSeleccionCentroCosto', 'Seleccione', '');
}
async function LlenarCboCuenta() {
    let lista = await CuentaContable.TraerTodos();
    CuentaContable.ArmarCombo(lista, 'CboCuenta', 'SelectorCuentaContable', 'EventoSeleccionCuentaContable', 'Seleccione', '');
}
async function LlenarCboTipoPago() {
    let lista = await TipoPago.TraerTodos();
    TipoPago.ArmarCombo(lista, 'CboTipoPago', 'SelectorTipoPago', 'EventoSeleccionTipoPago', 'Seleccione', '');
}
function MostrarSolapaGasto() {
    $(".btnGastoOn").css("display", "block");
    $(".btnGastoOff").css("display", "none");
    $(".btnComprobanteOn").css("display", "none");
    $(".btnComprobanteOff").css("display", "block");
    $("#GrillaGastosRegistrados").css("display", "block");
    $("#GrillaComprobantesRegistrados").css("display", "none");
}
function MostrarSolapaComprobante() {
    $(".btnGastoOn").css("display", "none");
    $(".btnGastoOff").css("display", "block");
    $(".btnComprobanteOn").css("display", "block");
    $(".btnComprobanteOff").css("display", "none");
    $("#GrillaGastosRegistrados").css("display", "none");
    $("#GrillaComprobantesRegistrados").css("display", "block");
}
$('body').on('click', '.btnGastoOff', async function (e) {
    try {
        await LlenarGrillaGasto();
        await LimpiarComprobante();
        MostrarSolapaGasto();
    } catch (e) {
        alertAlerta(e);
    }
});
// Gasto
function LimpiarGasto() {
    $(".DatoFormulario").val('');
}
async function NuevoGasto() {
    _ObjGasto = new Gasto;
    await _ObjGasto.Alta();
    _ListaG = await Gasto.TraerGastosAbiertos();
    await LlenarGrillaGasto();
    await LlenarGasto();
    await NuevoComprobante();
}
async function LlenarGasto() {
    LimpiarGasto();
    if (_ObjGasto !== null) {
        $("#SpanNroGasto").text(_ObjGasto.IdEntidad);
        $("#SpanGastoImporte").text(_ObjGasto.Importe.toFixed(2));
        $("#SpanGastoComprobantes").text(_ObjGasto.CantidadComprobantes);
        $("#SpanGastoEstado").text(_ObjGasto.Estado);
        _ListaC = await _ObjGasto.ListaComprobantes();
        if (_ListaC.length > 0) {
            await LlenarGrillaComprobante();
            MostrarSolapaComprobante();
        }
    }
}
$('body').on('click', '#LinkBtnNuevoGasto', async function (e) {
    try {
        _ListaG = await Gasto.TraerGastosAbiertos();
        if (_ListaG.length > 0) {
            PopUpConfirmarConCancelar('info', null, 'Existe un Gasto Abierto, Desea realmente abrir otro?', '', 'EventoNuevoGasto', 'Nuevo Gasto', 'Cancelar');
        } else {
            await NuevoGasto();
        }
    } catch (e) {
        alertAlerta(e);
    }
});
document.addEventListener('EventoNuevoGasto', async function (e) {
    try {
        await NuevoGasto();
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('EventoSeleccionarGasto', async function (e) {
    try {
        let objSeleccionado = e.detail;
        _ObjGasto = objSeleccionado;
        LlenarGasto();
        NuevoComprobante();
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('EventoEliminarGasto', async function (e) {
    try {
        let objSeleccionado = e.detail;
        _ObjGasto = objSeleccionado;
        _ListaG = await Gasto.TraerGastosAbiertos();
        if (_ListaG.length > 0) {
            PopUpConfirmarConCancelar('warning', null, 'Desea realmente eliminar el Gasto?', 'Será realizada una eliminacón lógica', 'EventoConfirmarEliminarGasto', 'Eliminar Gasto', 'Cancelar', 'red');
        }
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('EventoConfirmarEliminarGasto', async function (e) {
    try {
        await _ObjGasto.Baja();
        _ListaG = await Gasto.TraerGastosAbiertos();
        await LlenarGrillaGasto();
    } catch (e) {
        alertAlerta(e);
    }
}, false);

// Comprobante
async function LimpiarComprobante() {
    $(".DatoFormularioComprobante").val('');
    await LlenarCboOriginario();
    await LlenarCboProveedor();
    await LlenarCboCuenta();
    await LlenarCboCentroCosto();
    await LlenarCboTipoPago();
    await LlenarGrillaGasto();
}
function NuevoComprobante() {
    LimpiarComprobante();
    _ObjComprobante = new Comprobante;
    _ObjComprobante.IdGasto = _ObjGasto.IdEntidad;
}
async function LlenarComprobante() {
    await LimpiarComprobante();
    $("#SelectorOriginarioGasto").text((await _ObjComprobante.ObjOriginarioGasto()).Nombre);
    $("#SelectorProveedor").text((await _ObjComprobante.ObjProveedor()).Nombre);
    $("#SelectorCentroCosto").text((await _ObjComprobante.ObjCentroCosto()).Nombre);
    $("#SelectorCuentaContable").text((await _ObjComprobante.ObjCuentaContable()).Nombre);
    $("#SelectorTipoPago").text((await _ObjComprobante.ObjTipoPago()).Nombre);
    $("#TxtNroComprobante").val(_ObjComprobante.NroComprobante);
    $("#TxtFechaPago").val(LongToDateString(_ObjComprobante.FechaPago));
    $("#TxtFechaGasto").val(LongToDateString(_ObjComprobante.FechaGasto));
    $("#TxtImporte").val(_ObjComprobante.Importe.toFixed(2));


}
$('body').on('click', '#LinkBtnNuevoComprobante', async function (e) {
    try {
        _ObjComprobante = new Comprobante;
        await LimpiarComprobante();
    } catch (e) {
        alertAlerta(e);
    }
});
document.addEventListener('EventoSeleccionarComprobante', async function (e) {
    try {
        let objSeleccionado = e.detail;
        _ObjComprobante = objSeleccionado;
        await LlenarComprobante();
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('EventoEliminarComprobante', async function (e) {
    try {
        let objSeleccionado = e.detail;
        _ObjComprobante = objSeleccionado;
        if (_ListaG.length > 0) {
            PopUpConfirmarConCancelar('warning', null, 'Desea realmente eliminar el Comprobante?', 'Será realizada una eliminacón lógica', 'EventoConfirmarEliminarComprobante', 'Eliminar Comprobante', 'Cancelar', 'red');
        }
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('EventoConfirmarEliminarComprobante', async function (e) {
    try {
        await _ObjComprobante.Baja();
        _ObjGasto = await Gasto.TraerUno(_ObjGasto.IdEntidad);
        await LlenarGasto();
    } catch (e) {
        alertAlerta(e);
    }
}, false);
$('body').on('click', '#LinkBtnGuardarComprobante', async function (e) {
    try {
        if (_ObjComprobante === undefined) {
            throw 'Debe ingresar los Datos del comprobante';
        }
        _ObjComprobante.NroComprobante = $("#TxtNroComprobante").val();
        _ObjComprobante.Importe = $("#TxtImporte").val();
        _ObjComprobante.IdGasto = _ObjGasto.IdEntidad;
        if (_ObjComprobante.IdEntidad === 0) {
            await _ObjComprobante.Alta();
        } else {
            await _ObjComprobante.Modifica();
        }
        await LimpiarComprobante();
        _ObjGasto = await Gasto.TraerUno(_ObjGasto.IdEntidad);
        await LlenarGasto();
        alertOk('El Comprobante se ha guardado correctamente');
    } catch (e) {
        alertAlerta(e);
    }
});



document.addEventListener('EventoSeleccionOriginarioGasto', async function (e) {
    try {
        let objSeleccionado = e.detail;
        if (_ObjComprobante === undefined) {
            _ObjComprobante = new Comprobante;
        }
        _ObjComprobante.IdOriginario = objSeleccionado.IdEntidad;
        $("#SelectorOriginarioGasto").text(objSeleccionado.Nombre);
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('EventoSeleccionProveedor', async function (e) {
    try {
        let objSeleccionado = e.detail;
        if (_ObjComprobante === undefined) {
            _ObjComprobante = new Comprobante;
        }
        console.log(objSeleccionado);
        _ObjComprobante.IdProveedor = objSeleccionado.IdEntidad;
        $("#SelectorProveedor").text(objSeleccionado.Nombre);
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('EventoSeleccionCentroCosto', async function (e) {
    try {
        let objSeleccionado = e.detail;
        if (_ObjComprobante === undefined) {
            _ObjComprobante = new Comprobante;
        }
        _ObjComprobante.IdCentroCosto = objSeleccionado.IdEntidad;
        $("#SelectorCentroCosto").text(objSeleccionado.Nombre);
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('EventoSeleccionCuentaContable', async function (e) {
    try {
        let objSeleccionado = e.detail;
        if (_ObjComprobante === undefined) {
            _ObjComprobante = new Comprobante;
        }
        _ObjComprobante.IdCuenta = objSeleccionado.IdEntidad;
        $("#SelectorCuentaContable").text(objSeleccionado.Nombre);
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('EventoSeleccionTipoPago', async function (e) {
    try {
        let objSeleccionado = e.detail;
        if (_ObjComprobante === undefined) {
            _ObjComprobante = new Comprobante;
        }
        _ObjComprobante.IdTipoPago = objSeleccionado.IdEntidad;
        $("#SelectorTipoPago").text(objSeleccionado.Nombre);
    } catch (e) {
        alertAlerta(e);
    }
}, false);

$(function () {
    $("#TxtFechaGasto").datepicker();
    $("#TxtFechaGasto").on("change", async function () {
        let selected = $(this).val();
        let seleccionLNG = selected.substr(6, 4) + '' + selected.substr(3, 2) + '' + selected.substr(0, 2);
        try {
            if (seleccionLNG > FechaHoyLng()) {
                $("#TxtFechaGasto").val(fechaHoy);
                throw 'La Fecha del Gasto no puede ser mayor a hoy';
            }
            if (_ObjComprobante.FechaPago > 0) {
                if (seleccionLNG > _ObjComprobante.FechaPago) {
                    throw 'La Fecha del Gasto no puede ser mayor a la fecha del Pago.';
                }
            }
            _ObjComprobante.FechaGasto = parseInt(seleccionLNG);
        } catch (e) {
            _ObjComprobante.FechaGasto = parseInt(FechaHoyLng());
            alertAlerta(e);
        }
    });
    $("#TxtFechaPago").datepicker();
    $("#TxtFechaPago").on("change", async function () {
        let selected = $(this).val();
        let seleccionLNG = selected.substr(6, 4) + '' + selected.substr(3, 2) + '' + selected.substr(0, 2);
        try {
            if (seleccionLNG > FechaHoyLng()) {
                $("#TxtFechaPago").val(fechaHoy);
                throw 'La Fecha del Pago no puede ser mayor a hoy.';
            }
            if (_ObjComprobante.FechaGasto > 0) {
                if (_ObjComprobante.FechaGasto > seleccionLNG) {
                    throw 'La Fecha del Pago no puede ser menor a la fecha del Gasto.';
                }
            }
            _ObjComprobante.FechaPago = parseInt(seleccionLNG);
        } catch (e) {
            _ObjComprobante.FechaPago = parseInt(FechaHoyLng());
            alertAlerta(e);
        }
    });
});


