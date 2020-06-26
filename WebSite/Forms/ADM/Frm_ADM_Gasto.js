var _ListaIngresos;
var _ObjIngreso;

$(document).ready(async function() {
    try {
        SeleccionGasto();
        await LlenarCombosComprobante();
        $("#divCantRegistrosImprimir").css('display', 'none');
    } catch (e) {
        alertAlerta(e);
    }
});

$('body').on('click', '#RBuscaGasto', async function(e) {
    try {
        SeleccionGasto();

    } catch (e) {
        alertAlerta(e);
    }
});
$('body').on('click', '#RBuscaComprobante', async function(e) {
    try {
        SeleccionComprobante();
    } catch (e) {
        alertAlerta(e);
    }
});

function SeleccionGasto() {
    $("#divBuscadorGasto").css("display", "block");
    $("#divBuscadorComprobante").css("display", "none");
}

function SeleccionComprobante() {
    $("#divBuscadorGasto").css("display", "none");
    $("#divBuscadorComprobante").css("display", "block");
}

async LlenarCombosComprobante() {
    await LlenarCboBuscaComprobanteOriginario();
    await LlenarCboBuscaComprobanteCuenta();
    await LlenarCboBuscaComprobanteProveedor();
    await LlenarCboBuscaComprobanteCentroCosto();
    await LlenarCboBuscaComprobanteTipoPago();
}
async function LlenarCboBuscaComprobanteCuenta() {
    let lista = await CuentaContable.TraerTodos();
    CuentaContable.ArmarCombo(lista, 'CboBuscaComprobanteCuenta', 'SelectorCuentaContable', 'EventoSeleccionCuentaContable', 'Seleccione Cuenta', '');
}
async function LlenarCboBuscaComprobanteOriginario() {
    let lista = await OriginarioGasto.TraerTodos();
    OriginarioGasto.ArmarCombo(lista, 'CboBuscaComprobanteOriginarioGasto', 'SelectorOriginarioGasto', 'EventoSeleccionOriginarioGasto', 'Seleccione Originario Gasto', '');
}
async function LlenarCboBuscaComprobanteProveedor() {
    let lista = await Proveedor.TraerTodos();
    Proveedor.ArmarCombo(lista, 'CboBuscaComprobanteProveedor', 'SelectorProveedor', 'EventoSeleccionProveedor', 'Seleccione Proveedor', '');
}
async function LlenarCboBuscaComprobanteCentroCosto() {
    let lista = await CentroCosto.TraerTodos();
    CentroCosto.ArmarCombo(lista, 'CboBuscaComprobanteCentroCosto', 'SelectorCentroCosto', 'EventoSeleccionCentroCosto', 'Seleccione C. de C.', '');
}
async function LlenarCboBuscaComprobanteTipoPago() {
    let lista = await TipoPago.TraerTodos();
    TipoPago.ArmarCombo(lista, 'CboBuscaComprobanteTipoPago', 'SelectorTipoPago', 'EventoSeleccionTipoPago', 'Seleccione Tipo Pago', '');
}   $("#GrillaGastosRegistrados").css("display", "block");
    $("#GrillaComprobantesRegistrados").css("display", "none");
    $("#GastoDetalle").css("display", "none");

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
    await LlenarGrillaGasto();
    await LlenarGasto();
    await NuevoComprobante();
    MostrarSolapaGasto();
}
async function LlenarGasto() {
    LimpiarGasto();
    if (_ObjGasto !== null) {
        $("#GastoDetalle").css('display', 'block');
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
        await LlenarGrillaGasto();
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
        if (_ListaG.length > 0) {
            PopUpConfirmarConCancelar('warning', null, 'Desea realmente eliminar el Gasto?', 'Será realizada una eliminación lógica<br><i>Los datos no se perderán.</i>', 'EventoConfirmarEliminarGasto', 'Eliminar Gasto', 'Cancelar', 'red');
        }
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('EventoConfirmarEliminarGasto', async function (e) {
    try {
        await _ObjGasto.Baja();
        await LlenarGrillaGasto();
    } catch (e) {
        alertAlerta(e);
    }
}, false);
$('body').on('click', '#LinkBtnCerrarGasto', async function (e) {
    try {
        if (_ObjGasto.IdEntidad > 0) {
            PopUpConfirmarConCancelar('info', null, 'Desea realmente cerrar el gasto?', '<i>El mismo ya no podrá reabrirse.</i>', 'EventoConfirmarCerrarGasto', 'Cerrar Gasto', 'Cancelar');
        }
    } catch (e) {
        alertAlerta(e);
    }
});
document.addEventListener('EventoConfirmarCerrarGasto', async function (e) {
    try {
        await _ObjGasto.Cerrar();
        $("#SpanNroGasto").text('');
        $("#SpanGastoImporte").text('');
        $("#SpanGastoComprobantes").text('');
        $("#SpanGastoEstado").text('');
        $("#GastoDetalle").css("display", "none");
        _ObjGasto = new Gasto;
        alertOk('El Gasto se ha cerrado correctamente.');
    } catch (e) {
        alertAlerta(e);
    } finally {
        await LlenarGrillaGasto();
        MostrarSolapaGasto();
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
    $('#TxtFechaGasto').val(fechaHoy);
}
function NuevoComprobante() {
    if (_ObjGasto.IdEntidad === 0) {
        throw ('Debe Abrir o Seleccionar un Gasto');
    }
    LimpiarComprobante();
    _ObjComprobante = new Comprobante;
    _ObjComprobante.IdGasto = _ObjGasto.IdEntidad;
    _ObjComprobante.FechaGasto = parseInt(FechaHoyLng());
}
async function LlenarComprobante() {
    await LimpiarComprobante();
    document.getElementById("_CboCuenta").value = _ObjComprobante.IdCuenta;
    document.getElementById("_CboOriginarioGasto").value = _ObjComprobante.IdOriginario;
    document.getElementById("_CboCentroCosto").value = _ObjComprobante.IdCentroCosto;
    document.getElementById("_CboProveedor").value = _ObjComprobante.IdProveedor;
    if (_ObjComprobante.IdTipoPago > 0) {
        document.getElementById("_CboTipoPago").value = _ObjComprobante.IdTipoPago;
    }
    $("#TxtNroComprobante").val(_ObjComprobante.NroComprobante);
    $("#TxtObservaciones").val(_ObjComprobante.Observaciones);
    if (_ObjComprobante.FechaPago > 0) {
        $("#TxtFechaPago").val(LongToDateString(_ObjComprobante.FechaPago));
    }
    if (_ObjComprobante.FechaGasto) {
        $("#TxtFechaGasto").val(LongToDateString(_ObjComprobante.FechaGasto));
    }
    $("#TxtImporte").val(_ObjComprobante.Importe.toFixed(2));
}
$('body').on('click', '#LinkBtnNuevoComprobante', async function (e) {
    try {
        await LimpiarComprobante();
        _ObjComprobante = new Comprobante;
        NuevoComprobante();
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
            PopUpConfirmarConCancelar('warning', null, 'Desea realmente eliminar el Comprobante?', 'Será realizada una eliminación lógica<br><i>Los datos no se perderán.</i>', 'EventoConfirmarEliminarComprobante', 'Eliminar Comprobante', 'Cancelar', 'red');
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
        //alert(_ObjComprobante.FechaGasto);
        //if (_ObjComprobante.FechaGasto.lenght == 10) {
        //    let temp = _ObjComprobante.FechaGasto.replace(/_/g, "")

        //}
        //alert(_ObjComprobante.FechaGasto);
        //if (typeof (_ObjComprobante.FechaGasto) == 'string') {
        //    _ObjComprobante.FechaGasto = dateStringToLong(_ObjComprobante.FechaGasto);
        //}
        //alert(_ObjComprobante.FechaGasto);
        _ObjComprobante.NroComprobante = $("#TxtNroComprobante").val();
        _ObjComprobante.Importe = $("#TxtImporte").val();
        _ObjComprobante.Observaciones = $("#TxtObservaciones").val();
        if (_ObjComprobante.IdEntidad === 0) {
            await _ObjComprobante.Alta();
        } else {
            await _ObjComprobante.Modifica();
        }
        await LlenarGrillaGasto
        await NuevoComprobante();
        _ObjGasto = await Gasto.TraerUno(_ObjGasto.IdEntidad);
        await LlenarGasto();
        alertOk('El Comprobante se ha guardado correctamente');
    } catch (e) {
        alertAlerta(e);
    }
});


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


