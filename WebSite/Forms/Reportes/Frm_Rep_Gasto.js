$(document).ready(async function () {
    try {
        SeleccionGasto();
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

$('body').on('click', '#BtnBuscar', async function (e) {
    let Result = 0;
    try {
        $("#Reporte").css('display', 'none');
        LimpiarGrillas();
        let Lista = [];
        spinner();
        Busqueda = await ArmarBusqueda(strTipoBusqueda);
        strBusqueda = await ArmarBusquedaSQL(Busqueda);
        Lista = await Gasto.TraerTodosXBusqueda(Busqueda)
        if (Lista.length == 0) {
            throw 'No existen Gastos para la búsqueda informada.';
        }
        await Gasto.ArmarGrillaCabecera('GrillaCabecera');
        await Gasto.ArmarGrillaDetalle('GrillaDetalle', Lista, 'EventoSeleccionarComprobante', 'max-height: 380px; overflow-y: scroll;');
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
    return Buscador;
}
async function ArmarBusquedaSQL(Busqueda) {
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
        strSQL += ' g.IdEstado IN (\'' + Busqueda.Estados[0] + '\'';
        for (let i = 1; i < Busqueda.Estados.length; i++) {
            strSQL += ', \'' + Busqueda.Estados[i] + '\'';
        }
        strSQL += ')';
    }
    if (strSQL.length == 0) {
        strSQL = '  ';
    }
    return strSQL;
}