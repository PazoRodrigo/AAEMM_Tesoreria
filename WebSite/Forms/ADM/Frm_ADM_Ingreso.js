var _ListaIngresos;
var _ObjIngreso;

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
    $("#EntidadImporte").prop('disabled', true);
}

$('body').on('click', '#BtnBuscador', async function (e) {
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
        _ObjIngreso.Periodo = $("#EntidadPeriodo").val();
        _ObjIngreso.NroCheque = $("#EntidadNroCheque").val();
        _ObjIngreso.Importe = $("#EntidadImporte").val();
        await _ObjIngreso.Modifica();
        $("#Grilla").css("display", "none");
        alertOk('El Ingreso ha sido modificado correctamente.');
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#BtnExplotar', async function (e) {
    try {
        alertInfo('En Desarrollo')
        //if (_ObjIngreso.CodigoEntidad == 0) {
        //    throw 'Debe informar el CUIT del Ingreso para luego separarlo';
        //}
        //PopUpConfirmarConCancelar('info', null, 'Desea realmente explotar el Ingreso?', '', 'EventoConfirmarExplotarIngreso', 'Explotar Ingreso', 'Cancelar', 'green');
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
let _ListaIngresosExplotado = [];
document.addEventListener('EventoConfirmarExplotarIngreso', async function (e) {
    try {
        $("#EntidadCUIT").prop('disabled', true);
        $("#EntidadPeriodo").prop('disabled', true);
        $("#EntidadNroCheque").prop('disabled', true);
        $("#ContenidoSeleccionado").css("display", "none");
        $("#LblImporteRestante").text(separadorMiles((await Explotar_Resto()).toFixed(2)));
        _ListaIngresosExplotado = [];
        await Explotar_NuevoIngreso();
        await Ingreso.ArmarGrillaIngresoSeparado('GrillaIngresoSeparado', _ListaIngresosExplotado, '', 'EventoAgregarExplotacion', _ObjIngreso);
        $("#DivExplotacionIngreso").css('display', 'block')
    } catch (e) {
        alertAlerta(e);
    }
}, false);
async function Explotar_NuevoIngreso() {
    let NuevoIngreso = new Ingreso;
    if (_ListaIngresosExplotado.length == 0) {
        NuevoIngreso.IdEntidad = 1;
    } else {
        alertAlerta(_ListaIngresosExplotado.length - 1);
        alertAlerta(_ListaIngresosExplotado[_ListaIngresosExplotado.length - 1].IdEntidad + 1);
        NuevoIngreso.IdEntidad = _ListaIngresosExplotado[_ListaIngresosExplotado.length - 1].IdEntidad + 1;
    }
    NuevoIngreso.Importe = await Explotar_Resto();
    _ListaIngresosExplotado.push(NuevoIngreso);

}
async function Explotar_Resto() {
    let Result = _ObjIngreso.Importe;
    if (_ListaIngresosExplotado.length > 0) {
        for (let ItemIngreso of _ListaIngresosExplotado) {
            Result -= ItemIngreso.Importe
        }
    }
    return Result;
}
document.addEventListener('EventoAgregarExplotacion', async function (e) {
    try {
        let objSeleccionado = e.detail;
        console.log(objSeleccionado);
        buscado = $.grep(_ListaIngresosExplotado, function (entidad, index) {
            return entidad.IdEntidad === objSeleccionado.IdEntidad;
        });
        buscado[0].Importe = objSeleccionado.Importe;
        console.log(buscado[0]);
        $("#LblImporteRestante").text(separadorMiles((await Explotar_Resto()).toFixed(2)));
        await Explotar_NuevoIngreso();
        await Ingreso.ArmarGrillaIngresoSeparado('GrillaIngresoSeparado', _ListaIngresosExplotado, '', 'EventoAgregarExplotacion', _ObjIngreso);
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
        await LlenarIngreso();
        $("#divCantRegistrosBusqueda").css("display", "none");
        $("#ContenedorSeleccionado").css("display", "block");
    } catch (e) {
        alertAlerta(e);
    }
}, false);