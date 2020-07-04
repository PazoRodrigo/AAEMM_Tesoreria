$(document).ready(async function () {
    try {
        await LlenarCombosComprobante();
        SeleccionComprobante();
    } catch (e) {
        alertAlerta(e);
    }
});

function LimpiarGrillas() {
    $("#GrillaCabecera").html('');
    $("#GrillaDetalle").html('');
    $("#divCantRegistrosImprimir").css('display', 'none');
    $("#Grilla").css('display', 'none');
}

function SeleccionComprobante() {
    $("#BuscaComprobanteDesde").val('');
    $("#BuscaComprobanteHasta").val('');
    $("#_CboBuscaComprobanteOriginarioGasto").val("0");
    $("#_CboBuscaComprobanteProveedor").val("0");
    $("#_CboBuscaComprobanteCentroCosto").val("0");
    $("#_CboBuscaComprobanteTipoPago").val("0");
    $("#_CboBuscaComprobanteCuenta").val("0");
    $("#divBuscadorGasto").css("display", "none");
    $("#divBuscadorComprobante").css("display", "block");
}
async function LlenarCombosComprobante() {
    await LlenarCboBuscaComprobanteGasto();
    await LlenarCboBuscaComprobanteOriginario();
    await LlenarCboBuscaComprobanteProveedor();
    await LlenarCboBuscaComprobanteCentroCosto();
    await LlenarCboBuscaComprobanteTipoPago();
    await LlenarCboBuscaComprobanteCuenta();
}
async function LlenarCboBuscaComprobanteGasto() {
    let lista = await Gasto.TraerTodos();
    Gasto.ArmarCombo(lista, 'CboBuscaComprobanteGasto', 'SelectorGasto', 'EventoSeleccionGasto', 'Seleccione Gasto', '');
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
}
async function LlenarCboBuscaComprobanteCuenta() {
    let lista = await CuentaContable.TraerTodos();
    CuentaContable.ArmarCombo(lista, 'CboBuscaComprobanteCuenta', 'SelectorCuentaContable', 'EventoSeleccionCuentaContable', 'Seleccione Cuenta', '');
}

$('body').on('click', '#BtnBuscar', async function (e) {
    let Result = 0;
    try {
        $("#Reporte").css('display', 'none');
        LimpiarGrillas();
        let Lista = [];
        spinner();
        Busqueda = await ArmarBusqueda(strTipoBusqueda);
        strBusqueda = await ArmarBusquedaSQL(Busqueda, strTipoBusqueda);
        Lista = await Comprobante.TraerTodosXBusqueda(Busqueda)
        if (Lista.length == 0) {
            throw 'No existen Comprobantes para la búsqueda informada.';
        }
        await Comprobante.ArmarGrillaCabecera('GrillaCabecera');
        await Comprobante.ArmarGrillaDetalle('GrillaDetalle', Lista, 'EventoSeleccionarComprobante', 'height: 350px; overflow-y: scroll;');
        if (Lista.length > 0) {
            for (let item of Lista) {
                Result += item.Importe;
            }
        }
        $("#divCantRegistrosImprimir").css('display', 'block');
        spinnerClose();
        $("#Grilla").css("display", "block");
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    } finally {
        $("#LblValorSeleccion").text(separadorMiles(Result.toFixed(2)));
    }
});

async function ArmarBusqueda(Tipo) {
    let Buscador;
    let Desde = '';
    let Hasta = '';
    let ChecksEstado;
    let EstadosSeleccionados = '';
    Desde = $("#BuscaComprobanteDesde").val();
    Hasta = $("#BuscaComprobanteHasta").val();
    ChecksEstado = $('[name ="CheckEstadoComprobante"]');
    EstadosSeleccionados = '';
    for (let chk of ChecksEstado) {
        if (chk.checked === true) {
            EstadosSeleccionados += chk.value;
        }
    }
    Buscador = new StrBusquedaComprobante;
    Buscador.Desde = dateStringToLong(Desde);
    Buscador.Hasta = dateStringToLong(Hasta);
    Buscador.Estados = EstadosSeleccionados;
    if ($("#_CboBuscaComprobanteGasto").val() > 0) {
        Buscador.IdGasto = $("#_CboBuscaComprobanteGasto").val();
    }
    if ($("#_CboBuscaComprobanteOriginarioGasto").val() > 0) {
        Buscador.IdOriginarioGasto = $("#_CboBuscaComprobanteOriginarioGasto").val();
    }
    if ($("#_CboBuscaComprobanteProveedor").val() > 0) {
        Buscador.IdProveedor = $("#_CboBuscaComprobanteProveedor").val();
    }
    if ($("#_CboBuscaComprobanteCentroCosto").val() > 0) {
        Buscador.IdCentroCosto = $("#_CboBuscaComprobanteCentroCosto").val();
    }
    if ($("#_CboBuscaComprobanteTipoPago").val() > 0) {
        Buscador.IdTipoPago = $("#_CboBuscaComprobanteTipoPago").val();
    }
    if ($("#_CboBuscaComprobanteCuenta").val() > 0) {
        Buscador.IdCuenta = $("#_CboBuscaComprobanteCuenta").val();
    }
    let Importe = $("#BuscaComprobanteImporte").val();
    if (Importe.length != 0) {
        Buscador.Importe = Importe;
    }
    let NroComprobante = $("#BuscaComprobanteNroComprobante").val();
    if (NroComprobante.length != 0) {
        Buscador.NroComprobante = NroComprobante;
    }
    return Buscador;
}
async function ArmarBusquedaSQL(Busqueda, Tipo) {
    let strSQL = '';
    let primero = true;
    if (Busqueda.Desde > 0) {
        if (primero == true) {
            strSQL += ' WHERE'
            primero = false;
        } else {
            strSQL += ' AND'
        }
        strSQL += ' FechaGasto >= \'' + Busqueda.Desde + '\'';

    }
    if (Busqueda.Hasta > 0) {
        if (primero == true) {
            strSQL += ' WHERE'
            primero = false;
        } else {
            strSQL += ' AND'
        }
        strSQL += ' FechaGasto <= \'' + Busqueda.Hasta + '\'';
    }
    if (Busqueda.Estados.length > 0) {
        if (primero == true) {
            strSQL += ' WHERE'
            primero = false;
        } else {
            strSQL += ' AND'
        }
        strSQL += ' IdEstado IN (\'' + Busqueda.Estados[0] + '\'';
        for (let i = 1; i < Busqueda.Estados.length; i++) {
            strSQL += ', \'' + Busqueda.Estados[i] + '\'';
        }
        strSQL += ')';
    }
    if (Busqueda.IdGasto > 0) {
        if (primero == true) {
            strSQL += ' WHERE'
            primero = false;
        } else {
            strSQL += ' AND'
        }
        strSQL += ' IdGasto <= \'' + Busqueda.IdGasto + '\'';
    }
    if (Busqueda.IdOriginarioGasto > 0) {
        if (primero == true) {
            strSQL += ' WHERE'
            primero = false;
        } else {
            strSQL += ' AND'
        }
        strSQL += ' IdOriginarioGasto <= \'' + Busqueda.IdOriginarioGasto + '\'';
    }
    if (Busqueda.IdProveedor > 0) {
        if (primero == true) {
            strSQL += ' WHERE'
            primero = false;
        } else {
            strSQL += ' AND'
        }
        strSQL += ' IdProveedor <= \'' + Busqueda.IdProveedor + '\'';
    }
    if (Busqueda.IdCentroCosto > 0) {
        if (primero == true) {
            strSQL += ' WHERE'
            primero = false;
        } else {
            strSQL += ' AND'
        }
        strSQL += ' IdCentroCosto <= \'' + Busqueda.IdCentroCosto + '\'';
    }
    if (Busqueda.IdTipoPago > 0) {
        if (primero == true) {
            strSQL += ' WHERE'
            primero = false;
        } else {
            strSQL += ' AND'
        }
        strSQL += ' IdTipoPago <= \'' + Busqueda.IdTipoPago + '\'';
    }
    if (Busqueda.IdCuenta > 0) {
        if (primero == true) {
            strSQL += ' WHERE'
            primero = false;
        } else {
            strSQL += ' AND'
        }
        strSQL += ' IdCuenta <= \'' + Busqueda.IdCuenta + '\'';
    }
    if (Busqueda.Importe > 0) {
        if (primero == true) {
            strSQL += ' WHERE'
            primero = false;
        } else {
            strSQL += ' AND'
        }
        strSQL += ' Importe <= \'' + Busqueda.Importe + '\'';
    }
    if (strSQL.length == 0) {
        strSQL = '  ';
    }
    return strSQL;
}