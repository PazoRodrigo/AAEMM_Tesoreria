var _ListaIngresos;
var _ObjIngreso;
let _ListaIngresosExplotado = [];

$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Ingresos');
        $("#divCantRegistrosBusqueda").css('display', 'none');
        Inicio();
    } catch (e) {
        alertAlerta(e);
    }
});

function Inicio() {
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
    $("#EntidadCodigoEntidad").val('');
    $("#EntidadRazonSocial").val('');
    $("#EntidadAcred").val('');
    $("#EntidadPeriodo").val('');
    $("#EntidadImporte").val('');
    $("#EntidadOrigen").val('');
    $("#EntidadNroCheque").val('');
    $("#EntidadEstado").val('');
    $("#LblExplotado").text('');
    $("#EntidadCodigoEntidad").prop('disabled', true);
    $("#EntidadRazonSocial").prop('disabled', true);
    //$("#EntidadImporte").prop('disabled', true);
}
async function RealizarBusqueda() {
    let Result = 0;
    try {
        $("#ContenedorSeleccionado").css("display", "none");
        $("#divCantRegistrosBusqueda").css('display', 'none');
        spinner();
        LimpiarGrilla();
        let Busqueda = await ArmarBusqueda();
        _ListaIngresos = await Ingreso.TraerTodosXBusqueda(Busqueda);
        await LlenarGrilla();
        if (_ListaIngresos.length > 0) {
            for (let item of _ListaIngresos) {
                Result += item.Importe;
            }
            $("#LblCantidadRegistrosGrilla").text(_ListaIngresos.length);
            $("#divCantRegistrosBusqueda").css('display', 'block');
        }
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    } finally {
        $("#LblValorSeleccion").text(separadorMiles(Result.toFixed(2)));
    }
}
$('body').on('click', '#BtnBuscador', async function (e) {
    await RealizarBusqueda();
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
    if (_ListaIngresos.length > 0) {
        await Ingreso.ArmarGrillaCabecera('GrillaCabecera');
        await Ingreso.ArmarGrillaDetalle('GrillaDetalle', _ListaIngresos, 'EventoSeleccionarIngreso', 'height: 350px; overflow-y: scroll;');
        $("#Grilla").css("display", "block");
    } else {
        throw "No existen Ingresos para mostrar con esos parámetros";
    }
}

async function LlenarIngreso() {
    LimpiarIngreso();
    if (_ObjIngreso == undefined) {
        throw 'No existe Ingreso seleccionado';
    }
    $("#DivExplotacionIngreso").css('display', 'none');
    $("#EntidadCUIT").val('');
    $("#EntidadCodigoEntidad").val('');
    $("#EntidadRazonSocial").val('');
    if (_ObjIngreso.CUIT > 0) {
        $("#EntidadCUIT").val(_ObjIngreso.CUIT);
        $("#EntidadCodigoEntidad").val(await _ObjIngreso.StrCodigoEntidad(6));
        $("#EntidadRazonSocial").val(_ObjIngreso.RazonSocial);
    }
    $("#EntidadAcred").val(await _ObjIngreso.StrFechaAcreditacion());
    $("#EntidadPeriodo").val(await _ObjIngreso.StrPeriodo());
    $("#EntidadImporte").val(separadorMiles(_ObjIngreso.Importe.toFixed(2)));
    $("#EntidadOrigen").val(await _ObjIngreso.OrigenLargo());
    if (_ObjIngreso.NroCheque > 0) {
        $("#EntidadNroCheque").val(_ObjIngreso.NroCheque);
    }
    $("#EntidadEstado").val(await _ObjIngreso.Estado());
    if (_ObjIngreso.IdExplotado > 0) {
        $("#LblExplotado").text('Ingreso proveniente de Ingreso Explotado');
    }
    $("#ContenidoSeleccionado").css("display", "block");
}
$('body').on('keyup', '#EntidadCUIT', async function (e) {
    let Texto = $("#EntidadCUIT").val();
    _ObjIngreso.CodigoEntidad = 0;
    $("#EntidadCodigoEntidad").val('');
    if (Texto.length == 11) {
        let TempEmpresa = await Empresa.TraerUnaXCUIT(Texto);
        $("#EntidadCodigoEntidad").val(await TempEmpresa.StrCodigo(6));
        _ObjIngreso.CodigoEntidad = TempEmpresa.CodigoEntidad;
        $("#EntidadRazonSocial").val(TempEmpresa.RazonSocial);
    }
});

$('body').on('click', '#BtnModificar', async function (e) {
    try {
        spinner();
        _ObjIngreso.CUIT = $("#EntidadCUIT").val();
        _ObjIngreso.CodigoEntidad = $("#EntidadCodigoEntidad").val();
        _ObjIngreso.Periodo = $("#EntidadPeriodo").val();
        _ObjIngreso.NroCheque = $("#EntidadNroCheque").val();
        _ObjIngreso.Importe = $("#EntidadImporte").val();
        await _ObjIngreso.Modifica();
        $("#BuscaCUIT").val(_ObjIngreso.CUIT);
        await RealizarBusqueda();
        spinnerClose();

        alertOk('El Ingreso ha sido modificado correctamente.');
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#BtnExplotar', async function (e) {
    try {
        if (_ObjIngreso.CodigoEntidad == 0) {
            throw 'Debe informar el CUIT del Ingreso para luego separarlo';
        }
        PopUpConfirmarConCancelar('info', null, 'Desea realmente explotar el Ingreso?', '', 'EventoConfirmarExplotarIngreso', 'Explotar Ingreso', 'Cancelar', 'green');
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
document.addEventListener('EventoConfirmarExplotarIngreso', async function (e) {
    try {
        $("#EntidadCUIT").prop('disabled', true);
        $("#EntidadPeriodo").prop('disabled', true);
        $("#EntidadNroCheque").prop('disabled', true);
        $("#ContenidoSeleccionado").css("display", "none");
        $("#LblImporteRestante").text(separadorMiles((await Explotar_Resto()).toFixed(2)));
        _ListaIngresosExplotado = [];
        await Explotar_NuevoIngreso();
        await Ingreso.ArmarGrillaIngresoExplotado('GrillaIngresoSeparado', _ListaIngresosExplotado, '', 'EventoAgregarExplotacion', _ObjIngreso);
        $("#DivExplotacionIngreso").css('display', 'block')
        $("#Periodo_" + _ListaIngresosExplotado.length).focus();
    } catch (e) {
        alertAlerta(e);
    }
}, false);
async function Explotar_NuevoIngreso() {
    let NuevoIngreso = new Ingreso;
    if (_ListaIngresosExplotado.length == 0) {
        NuevoIngreso.IdEntidad = 1;
    } else {
        let NroEntidad = _ListaIngresosExplotado[_ListaIngresosExplotado.length - 1].IdEntidad + 1;
        NuevoIngreso.IdEntidad = parseInt(NroEntidad);
    }
    _ListaIngresosExplotado.push(NuevoIngreso);
}

async function Explotar_Resto() {
    let Result = _ObjIngreso.Importe;
    if (_ListaIngresosExplotado.length > 0) {
        for (let ItemIngreso of _ListaIngresosExplotado) {
            Result -= parseFloat(ItemIngreso.Importe)
        }
    }
    if (Result < 0) {
        throw 'La sumatoria del valor explotado es mayor al importe del Ingreso.';
    }
    return Result;
}
async function Explotar_AgregarIngreso(aValidar) {
    let NuevoIngreso = new Ingreso;
    let sError = '';
    let TempImporte = aValidar.Importe;
    let TempPeriodo = aValidar.Periodo;
    let TotalCaracteres = TempImporte.length;
    let separadorDecimal = parseInt(TotalCaracteres) - 3;
    let paraborrar = TempImporte[separadorDecimal];
    if (paraborrar == ',') {
        if (TempImporte.length > 6) {
            TempImporte = TempImporte.replace('.', '');
        }
    }
    if (parseFloat(TempImporte) <= 0) {
        sError += 'El importe debe ser mayor a 0.00<br>';
    }
    if (TempPeriodo.length == 0) {
        sError += 'Debe completar el período';
    } else {
        if (TempPeriodo.length != 6) {
            sError += 'Debe completar el período correctamente';
        }
    }
    if (parseInt(Left(TempPeriodo, 2)) < 1 || parseInt(Left(TempPeriodo, 2)) > 12) {
        sError += 'Debe completar el período correctamente';
    }
    if (sError.length > 0) {
        throw sError;
    }
    NuevoIngreso.Importe = parseFloat(TempImporte);
    NuevoIngreso.Periodo = TempPeriodo;
    NuevoIngreso.FechaAcreditacion = _ObjIngreso.FechaAcreditacion
    NuevoIngreso.IdEntidad = _ListaIngresosExplotado.length;
    _ListaIngresosExplotado[_ListaIngresosExplotado.length - 1] = NuevoIngreso;
}

document.addEventListener('EventoAgregarExplotacion', async function (e) {
    try {
        let aValidar = e.detail;
        await Explotar_AgregarIngreso(aValidar);
        $("#LblImporteRestante").text(separadorMiles((await Explotar_Resto()).toFixed(2)));
        await Explotar_NuevoIngreso();
        await Ingreso.ArmarGrillaIngresoExplotado('GrillaIngresoSeparado', _ListaIngresosExplotado, '', 'EventoAgregarExplotacion', _ObjIngreso);
        if (_ListaIngresosExplotado.length > 0) {
            await Ingreso.MostrarBotonesExplotado('DivBotonesExplotado');
        }
        $("#Periodo_" + _ListaIngresosExplotado.length).focus();
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('EventoSeleccionarIngreso', async function (e) {
    try {
        let objSeleccionado = e.detail;
        let listaTempIngresos = [];
        listaTempIngresos.push(objSeleccionado);
        await Ingreso.ArmarGrillaDetalle('GrillaDetalle', listaTempIngresos, 'EventoSeleccionarIngreso', '');
        _ObjIngreso = objSeleccionado;
        _ListaIngresosExplotado = [];
        await LlenarIngreso();
        $("#divCantRegistrosBusqueda").css("display", "none");
        $("#ContenedorSeleccionado").css("display", "block");
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('EventoGuardarExplotacion', async function (e) {
    try {
        spinner();
        let ultimo = _ListaIngresosExplotado.length;
        let aValidar = [];
        aValidar.Importe = $("#Importe_" + ultimo).val();;
        aValidar.Periodo = $("#Periodo_" + ultimo).val();;
        await Explotar_AgregarIngreso(aValidar);
        $("#LblImporteRestante").text(separadorMiles((await Explotar_Resto()).toFixed(2)));
        if (_ListaIngresosExplotado.length == 1) {
            throw 'No se puede explotar un Ingreso en tan solo un registro';
        }
        await Explotar_Resto();
        let validaImporte = 0;
        if (_ListaIngresosExplotado.length > 0) {
            for (let ItemIngreso of _ListaIngresosExplotado) {
                validaImporte += parseFloat(ItemIngreso.Importe);
            }
        }
        if (validaImporte != _ObjIngreso.Importe) {
            throw 'La sumatoria del valor explotado es diferente al importe del Ingreso.';
        }

        await _ObjIngreso.ExplotarIngreso(_ListaIngresosExplotado);
        _ListaIngresosExplotado = [];
        $("#BuscaCUIT").val(_ObjIngreso.CUIT);
        await RealizarBusqueda();
        spinnerClose();
        alertOk('El Pago ha sido explotado correctamente');
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
}, false);
document.addEventListener('EventoRehacerExplotacion', async function (e) {
    try {
        _ListaIngresosExplotado = [];
        await Explotar_NuevoIngreso();
        await Ingreso.ArmarGrillaIngresoExplotado('GrillaIngresoSeparado', _ListaIngresosExplotado, '', 'EventoAgregarExplotacion', _ObjIngreso);
        $("#DivExplotacionIngreso").css('display', 'block')
        $("#Periodo_" + _ListaIngresosExplotado.length).focus();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
}, false);