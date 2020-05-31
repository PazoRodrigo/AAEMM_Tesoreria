var _ListaIngresos;
var _ObjIngreso;

$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Ingresos');
        $("#divCantRegistrosImprimir").css('display', 'none');
        // $("#SpanBtnNuevo").text('Nuevo Ingreso');
        // $("#SpanBtnBuscar").text('Buscar Ingreso');
        // $("#SpanBtnEliminar").text('Eliminar');
        // $("#SpanBtnGuardar").text('Guardar');
        // $("#SpanBtnImprimir").text('Imprimir Ingreso');
        Inicio();
    } catch (e) {
        alertAlerta(e);
    }
});

async function Inicio() {
    LimpiarFormulario();
}

function LimpiarFormulario() {
    LimpiarBuscador();
    LimpiarGrilla();
    LimpiarIngreso();
}

function LimpiarBuscador() {
    $("#BuscaDesdeAcred").val('');
    $("#BuscaHastaAcred").val('');
    let ChecksEstado = $('[name ="CheckEstado"]');
    for (let chk of ChecksEstado) {
        chk.checked = false;
    }
    $("#BuscaRazonSocal").val('');
    $("#BuscaCUIT").val('');
    let CheckTipo = $('[name ="CheckTipo"]');
    for (let chk of CheckTipo) {
        chk.checked = false;
    }
    $("#BuscaImporte").val('');
    $("#BuscaNroRecibo").val('');
    $("#BuscaNroCheque").val('');
}

function LimpiarGrilla() {
    LimpiarIngreso();
    $("#Grilla").css("display", "none");
}

function LimpiarIngreso() {
    $("#EntidadCUIT").val('');
    $("#EntidadRazonSocial").val('');
    $("#EntidadAcred").val('');
    $("#EntidadPeriodo").val('');
    $("#EntidadImporte").val('');
    $("#EntidadOrigen").val('');
    $("#EntidadNroCheque").val('');
    $("#EntidadEstado").val('');
}

$('body').on('click', '#BtnBuscador', async function (e) {
    try {
        $("#Seleccionado").css("display", "none");
        $("#divCantRegistrosImprimir").css('display', 'none');
        spinner();
        LimpiarGrilla();
        let Busqueda = await ArmarBusqueda();
        _ListaIngresos = await Ingreso.TraerTodosXBusqueda(Busqueda);
        await LlenarGrilla();
        if (_ListaIngresos.length > 0) {
            let TextoCantidadRegistros = "Imprimir " + _ListaIngresos.length + " registro";
            if (_ListaIngresos.length > 1) {
                TextoCantidadRegistros += "s";
            }
            $("#LblCantidadRegistrosGrilla").text(TextoCantidadRegistros);
            $("#divCantRegistrosImprimir").css('display', 'block');
        }
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});

async function ArmarBusqueda() {
    let Desde = $("#BuscaDesdeAcred").val();
    let Hasta = $("#BuscaHastaAcred").val();
    let ChecksEstado = $('[name ="CheckEstado"]');
    let EstadosSeleccionados = '';
    for (let chk of ChecksEstado) {
        if (chk.checked === true) {
            EstadosSeleccionados += chk.value;
        }
    }
    let CUIT = $("#BuscaCUIT").val();
    let RazonSocial = $("#BuscaRazonSocial").val();
    let ChecksTipo = $('[name ="CheckTipo"]');
    let TiposSeleccionados = '';
    for (let chk of ChecksTipo) {
        if (chk.checked === true) {
            TiposSeleccionados += chk.value;
        }
    }

    let Importe = $("#BuscaImporte").val();
    let NroRecibo = $("#BuscaNroRecibo").val();
    let NroCheque = $("#BuscaNroCheque").val();
    let Buscador = new StrBusquedaIngreso;
    Buscador.Desde = dateStringToLong(Desde);
    Buscador.Hasta = dateStringToLong(Hasta);
    Buscador.Estados = EstadosSeleccionados;
    Buscador.Tipos = TiposSeleccionados;
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

async function LlenarGrilla() {
    $("#Grilla").css("display", "none");
    console.log(_ListaIngresos.length);
    if (_ListaIngresos.length > 0) {
        await Ingreso.ArmarGrillaCabecera('GrillaCabecera');
        await Ingreso.ArmarGrillaDetalle('GrillaDetalle', _ListaIngresos, 'EventoSeleccionarIngreso', 'max-height: 350px; overflow-y: scroll;');
        $("#Grilla").css("display", "block");
    } else {
        throw ("No existen Ingresos para Mostrar con esos parámetros");
    }
}

async function LlenarIngreso() {
    LimpiarIngreso();
    if (_ObjIngreso == undefined) {
        throw ('No existe Ingreso seleccionado');
    }
    $("#EntidadCUIT").val(_ObjIngreso.CUIT);
    $("#EntidadRazonSocial").val(_ObjIngreso.RazonSocial);
    $("#EntidadAcred").val(_ObjIngreso.FechaAcreditacion);
    $("#EntidadPeriodo").val(_ObjIngreso.Periodo);
    $("#EntidadImporte").val(_ObjIngreso.Importe);
    $("#EntidadOrigen").val(await _ObjIngreso.OrigenLargo());
    $("#EntidadNroCheque").val(_ObjIngreso.NroCheque);
    $("#EntidadEstado").val(await _ObjIngreso.Estado());

}

document.addEventListener('EventoSeleccionarIngreso', async function (e) {
    try {
        let objSeleccionado = e.detail;
        let listaTempIngresos = [];
        listaTempIngresos.push(objSeleccionado);
        await Ingreso.ArmarGrillaDetalle('GrillaDetalle', listaTempIngresos, 'EventoSeleccionarIngreso', '');
        _ObjIngreso = objSeleccionado;
        await LlenarIngreso();
        $("#Seleccionado").css("display", "block");
    } catch (e) {
        alertAlerta(e);
    }
}, false);