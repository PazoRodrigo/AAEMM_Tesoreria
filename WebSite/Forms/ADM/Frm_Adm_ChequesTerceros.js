// var _ListaChequesTerceros;
// var _ObjChequeTercero;

$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Cheques Terceros');
        Inicio();
    } catch (e) {
        alertAlerta(e);
    }
});
async function Inicio() {
    await ArmarGrillas(await ChequeTercero.TraerTodosXEstado(0));
}
async function LimpiarGrilla() {
    $("#LblCantidadRegistrosGrilla").text(0);
    $("#LblValorSeleccion").text('');
    $("#divCantRegistrosBusqueda").css('display', 'none');
    $("#ContenedorSeleccionado").css('display', 'none');
    $("#Grilla").css('display', 'none');
}
async function ArmarGrillas(lista) {
    $("#Grilla").css('display', 'none');
    $("#divCantRegistrosBusqueda").css('display', 'none');
    if (lista.length > 0) {
        await ChequeTercero.ArmarGrillaCabecera('GrillaCabecera');
        let valor = 0;
        let i = 0;
        while (i <= lista.length - 1) {
            valor = parseFloat(valor) + parseFloat(lista[i].Importe);
            i++;
        }
        await ChequeTercero.ArmarGrillaDetalle('GrillaDetalle', lista, 'SeleccionChequeTercero', 'height: 350px; overflow-y: scroll;');
        $("#GrilGrillaCabecerala").css('display', 'block');
        $("#Grilla").css('display', 'block');
        $("#LblCantidadRegistrosGrilla").text(lista.length);
        $("#LblValorSeleccion").text(separadorMiles(valor.toFixed(2)));
        $("#divCantRegistrosBusqueda").css('display', 'block');
    }
    $("#ContenedorSeleccionado").css('display', 'none');
}
// Botones Cabecera
$('body').on('click', '#BtnChequesRecibidos', async function (e) {
    try {
        spinner();
        await ArmarGrillas(await ChequeTercero.TraerTodosXEstado(0));
        LimpiarBuscador();
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#BtnChequesDepositados', async function (e) {
    try {
        spinner();
        await ArmarGrillas(await ChequeTercero.TraerTodosXEstado(1));
        LimpiarBuscador();
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#BtnChequesAcreditados', async function (e) {
    try {
        spinner();
        await ArmarGrillas(await ChequeTercero.TraerTodosXEstado(2));
        LimpiarBuscador();
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#BtnChequesRechazados', async function (e) {
    try {
        spinner();
        await ArmarGrillas(await ChequeTercero.TraerTodosXEstado(10));
        LimpiarBuscador();
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
// Botones
$('body').on('click', '#BtnGuardarCheque', async function (e) {
    try {
        spinner();
        let Tmp = $("#IdEntidad").text();
        let ObjGuardar = await ChequeTercero.TraerUno(Tmp);
        ObjGuardar.Numero = $("#EntidadNroCheque").val();
        ObjGuardar.FechaVencimiento = dateStringToLong($("#EntidadVenc").val())
        ObjGuardar.IdBanco = parseInt($("#IdCboBanco").val());
        ObjGuardar.IdEstado = parseInt($('input[name=RadioEstado]:checked').val());
        await ObjGuardar.Modifica();
        spinnerClose();
        alertOk('Cheque guardado con Éxito');
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
// Eventos
document.addEventListener('SeleccionChequeTercero', async function (e) {
    try {
        $("#divCantRegistrosBusqueda").css('display', 'none');
        let objSeleccionado = e.detail;
        let listaTempCheques = [];
        listaTempCheques.push(objSeleccionado);
        $("#GrillaCabecera").css('display', 'none');
        ChequeTercero.ArmarGrillaDetalle('GrillaDetalle', listaTempCheques, 'SeleccionChequeTercero', '');
        await LlenarEntidad(objSeleccionado);
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
}, false);
async function LlenarEntidad(ObjCheque) {
    await LimpiarEntidad();
    let ObjRecibo = await ObjCheque.ObjRecibo();
    $("#IdEntidad").text(ObjCheque.IdEntidad);
    $("#EntidadNroCheque").val(await ObjCheque.StrNumero(10));
    $("#EntidadEstado").val(await ObjCheque.StrEstado());
    $("#EntidadImporte").val(separadorMiles(await ObjCheque.Importe));
    $("#IdCboBanco").val(ObjCheque.IdBanco);
    $("#EntidadVenc").val(await ObjCheque.StrFechaVencimiento());
    $("#EntidadCUIT").val(ObjRecibo.CUIT);
    $("#EntidadNroRecibo").val(await ObjRecibo.StrNumero());
    $("#EntidadCodigoEntidad").val(await ObjRecibo.StrCodigoEntidad(10));
    $("#EntidadRazonSocial").val((await ObjRecibo.ObjEmpresa()).RazonSocial)
    $("input[name=RadioEstado][value=" + ObjCheque.IdEstado + "]").prop('checked', true);
    switch (ObjCheque.IdEstado) {
        case 0:
            //Recibido
            $(".DivEstadoRecibido").css('display', 'block');
            $(".DivEstadoDepositado").css('display', 'block');
            break;
        case 1:
            //Depositado
            $(".DivEstadoDepositado").css('display', 'block');
            $(".DivEstadoAcreditado").css('display', 'block');
            $(".DivEstadoRechazado").css('display', 'block');
            break;
        case 2:
            //Acreditado
            $(".DivEstadoAcreditado").css('display', 'block');
        case 10:
            //Rechazado
            $(".DivEstadoRechazado").css('display', 'block');
        default:
            break;
    }
    $("#ContenedorSeleccionado").css('display', 'block')
}
async function LimpiarEntidad() {
    $(".datoCheque").text('');
    $(".datoCheque").val('');
    await LlenarCboBancos();
};
async function LlenarCboBancos() {
    await Banco.ArmarCombo(await Banco.Todos(), 'CboBanco', 'IdCboBanco', 'Seleccionar Banco...', 'EventoBanco', 'form-control');

}
// Buscador Inicio **************************
function LimpiarBuscador() {
    $("#BuscaDesdeVenc").val('');
    $("#BuscaHastaVenc").val('');
    $("#BuscaRazonSocial").val('');
    $("#BuscaCUIT").val('');
    $("#BuscaImporte").val('');
    $("#BuscaNroRecibo").val('');
    $("#BuscaNroCheque").val('');
}
$('body').on('click', '#BtnBuscador', async function (e) {
    await RealizarBusqueda();
});
async function RealizarBusqueda() {
    let Result = 0;
    try {
        $("#ContenedorSeleccionado").css("display", "none");
        $("#divCantRegistrosBusqueda").css('display', 'none');
        spinner();
        LimpiarGrilla();
        let Busqueda = await ArmarBusqueda();
        let ListaCheques = await ChequeTercero.TraerTodosXBusqueda(Busqueda);
        await ArmarGrillas(ListaCheques);
        if (ListaCheques.length > 0) {
            for (let item of ListaCheques) {
                Result += item.Importe;
            }
            $("#LblCantidadRegistrosGrilla").text(ListaCheques.length);
            $("#divCantRegistrosBusqueda").css('display', 'block');
        }
        spinnerClose();
    } catch (error) {
        spinnerClose();
        alertAlerta(error);
    } finally {
        $("#LblValorSeleccion").text(separadorMiles(Result.toFixed(2)));
    }
}
async function ArmarBusqueda() {
    let Desde = $("#BuscaDesdeVenc").val();
    let Hasta = $("#BuscaHastaVenc").val();
    let RazonSocial = $("#BuscaRazonSocial").val();
    let CUIT = $("#BuscaCUIT").val();
    let Importe = $("#BuscaImporte").val();
    let NroRecibo = $("#BuscaNroRecibo").val();
    let NroCheque = $("#BuscaNroCheque").val();
    let Buscador = new StrBusquedaChequeTercero;
    Buscador.Desde = dateStringToLong(Desde);
    Buscador.Hasta = dateStringToLong(Hasta);
    if (RazonSocial.length != 0) {
        Buscador.RazonSocial = RazonSocial;
    }
    if (CUIT.length != 0) {
        Buscador.CUIT = CUIT;
    }
    if (Importe.length != 0) {
        Buscador.Importe = Importe;
    }
    if (NroRecibo.length != 0) {
        Buscador.NroRecibo = NroRecibo;
    }
    if (NroCheque.length != 0) {
        Buscador.NroCheque = NroCheque;
    }
    return Buscador;
}
// Buscador Fin *************************** **