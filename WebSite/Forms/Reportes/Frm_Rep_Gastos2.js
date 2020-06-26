$(document).ready(async function() {
    try {
        await LlenarCombosComprobante();
        $("#RBuscaGasto").prop('checked', true);
        SeleccionGasto();
    } catch (e) {
        alertAlerta(e);
    }
});

$('body').on('click', '#RBuscaGasto', async function(e) {
    try {
        $("#Reporte").css('display', 'none');
        SeleccionGasto();
    } catch (e) {
        alertAlerta(e);
    }
});
$('body').on('click', '#RBuscaComprobante', async function(e) {
    try {
        $("#Reporte").css('display', 'none');
        SeleccionComprobante();
    } catch (e) {
        alertAlerta(e);
    }
});

function SeleccionGasto() {
    $("#BuscaGastoDesde").val('');
    $("#BuscaGastoHasta").val('');
    $("#BuscaGastoEstadoA").prop('checked', false);
    $("#BuscaGastoEstadoC").prop('checked', false);
    $("#divBuscadorGasto").css("display", "block");
    $("#divBuscadorComprobante").css("display", "none");
}

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

$('body').on('click', '#BtnBuscar', async function(e) {
    try {
        $("#Reporte").css('display', 'none');
        LimpiarGrillas();
        let Lista = [];
        spinner();
        if ($("#RBuscaGasto").prop("checked") == true) {
            strTipoBusqueda = 'G';
            Busqueda = await ArmarBusqueda(strTipoBusqueda);
            strBusqueda = await ArmarBusquedaSQL(Busqueda, strTipoBusqueda);
            Lista = await Gasto.TraerTodosXBusqueda(Busqueda)
            if (Lista.length == 0) {
                throw 'No existen Gastos para la búsqueda informada.';
            }
            await Gasto.ArmarGrillaCabecera('GrillaCabecera');
            await Gasto.ArmarGrillaDetalle('GrillaDetalle', Lista, 'EventoSeleccionarComprobante', 'max-height: 380px; overflow-y: scroll;');
            $("#divCantRegistrosImprimir").css('display', 'block');
        } else {
            if ($("#RBuscaComprobante").prop("checked") == true) {
                strTipoBusqueda = 'C';
                Busqueda = await ArmarBusqueda(strTipoBusqueda);
                strBusqueda = await ArmarBusquedaSQL(Busqueda, strTipoBusqueda);
                Lista = await Comprobante.TraerTodosXBusqueda(Busqueda)
                if (Lista.length == 0) {
                    throw 'No existen Comprobantes para la búsqueda informada.';
                }
                await Comprobante.ArmarGrillaCabecera('GrillaCabecera');
                await Comprobante.ArmarGrillaDetalle('GrillaDetalle', Lista, 'EventoSeleccionarComprobante', 'max-height: 380px; overflow-y: scroll;');
                $("#divCantRegistrosImprimir").css('display', 'block');
            }
        }
        spinnerClose();
        $("#Grilla").css("display", "block");

    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});

async function ArmarBusqueda(Tipo) {
    let Buscador;
    let Desde = '';
    let Hasta = '';
    let ChecksEstado;
    let EstadosSeleccionados = '';
    switch (Tipo) {
        case 'G':
            Desde = $("#BuscaGastoDesde").val();
            Hasta = $("#BuscaGastoHasta").val();
            ChecksEstado = $('[name ="CheckEstadoGasto"]');
            EstadosSeleccionados = '';
            for (let chk of ChecksEstado) {
                if (chk.checked === true) {
                    EstadosSeleccionados += chk.value;
                }
            }
            Buscador = new StrBusquedaGasto;
            Buscador.Desde = dateStringToLong(Desde);
            Buscador.Hasta = dateStringToLong(Hasta);
            Buscador.Estados = EstadosSeleccionados;
            break;
        case 'C':
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
            break;
        default:

            break;
    }
    return Buscador;
}
async function ArmarBusquedaSQL(Busqueda, Tipo) {
    let strSQL = '';
    let primero = true;
    switch (Tipo) {
        case 'G':
            // strSQL = ';WITH CTE (IdGasto, CantidadComprobantes, FechaGasto,Importe) AS(SELECT IdGasto,';
            // strSQL += ' COUNT(1) CantComprobantes, MAX(FechaGasto) FechaGasto, SUM(Importe) Importe FROM ADM.Comprobante LEFT JOIN [ADM].[Gasto] g ON g.Id = IdGasto';
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
                strSQL += ' g.IdEstado IN (\'' + Busqueda.Estados[0] + '\'';
                for (let i = 1; i < Busqueda.Estados.length; i++) {
                    strSQL += ', \'' + Busqueda.Estados[i] + '\'';
                }
                strSQL += ')';
            }
            // strSQL += ' GROUP BY IdGasto)';
            // strSQL += ' SELECT [Id], CTE.CantidadComprobantes, CTE.FechaGasto, CTE.Importe, [Observaciones], [IdEstado], [IdUsuarioAlta], [IdUsuarioBaja], [IdUsuarioModifica], [FechaAlta], [FechaModifica], [FechaBaja], [IdMotivoBaja]';
            // strSQL += ' FROM[ADM].[Gasto] g';
            // strSQL += ' INNER JOIN CTE ON CTE.idGasto = g.Id';
            // strSQL += ' ORDER BY CTE.FechaGasto desc';
            break;
        case 'C':
            // strSQL = 'SELECT Id, IdGasto, IdOriginario, IdProveedor, IdCentroCosto, IdTipoPago, IdCuenta,';
            // strSQL += ' FechaGasto, FechaPago, NroComprobante, Importe, Observaciones,';
            // strSQL += ' IdUsuarioAlta, IdUsuarioBaja, IdUsuarioModifica, FechaAlta, FechaModifica, FechaBaja, IdMotivoBaja';
            // strSQL += ' FROM ADM.Comprobante';
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
            break;
        default:
            break;

    }
    if (strSQL.length == 0) {
        strSQL = '  ';
    }
    return strSQL;
}