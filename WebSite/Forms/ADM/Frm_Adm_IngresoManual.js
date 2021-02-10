var _ListaIngresosManuales;
var _ObjPago;
var _ListaPeriodos = [];
var _ListaPagos = [];
var _ObjRecibo;

$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Ingresos Manuales');
        $("#divCantRegistrosBusqueda").css('display', 'none');
        Inicio();
    } catch (e) {
        alertAlerta(e);
    }
});
async function Inicio() {
    await LimpiarFormulario();
}
async function LimpiarFormulario() {
    LimpiarBuscador();
    LimpiarGrilla();
    LimpiarRecibo();
}
function LimpiarBuscador() {
    $("#BuscaDesdeAcred").val('');
    $("#BuscaHastaAcred").val('');
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
    LimpiarRecibo();
    $("#Grilla").css("display", "none");
}
function LimpiarRecibo() {
    $("#EntidadCUIT").val('');
    $("#EntidadCodigoEntidad").val('');
    $("#EntidadRazonSocial").val('');
    $("#EntidadFecha").val('');
    $("#EntidadNroRecibo").val('');
    $("#EntidadEstado").val('');
    $("#EntidadImporteTotal").val('');
    $("#EntidadImporteEfectivo").val('');
    $("#EntidadObservaciones").val('');
    _ObjRecibo = new Recibo;
    InicializarPagos();
    InicializarPeriodos();
    LimpiarPeriodo();
}
// Buscador
$('body').on('click', '#BtnBuscador', async function (e) {
    await RealizarBusqueda();
});
async function RealizarBusqueda() {
    let Result = 0;
    try {
        $("#ContenedorSeleccionadoManual").css("display", "none");
        $("#divCantRegistrosBusqueda").css('display', 'none');
        spinner();
        LimpiarGrilla();
        let Busqueda = await ArmarBusqueda();
        _ListaIngresosManuales = await Recibo.TraerTodosXBusqueda(Busqueda);
        await LlenarGrilla();
        if (_ListaIngresosManuales.length > 0) {
            for (let item of _ListaIngresosManuales) {
                Result += item.ImporteTotal;
            }
            $("#LblCantidadRegistrosGrilla").text(_ListaIngresosManuales.length);
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
    let Desde = $("#BuscaDesdeAcred").val();
    let Hasta = $("#BuscaHastaAcred").val();
    let CUIT = $("#BuscaCUIT").val();
    let RazonSocial = $("#BuscaRazonSocial").val();
    let Importe = $("#BuscaImporte").val();
    let NroRecibo = $("#BuscaNroRecibo").val();
    let NroCheque = $("#BuscaNroCheque").val();
    let Buscador = new StrBusquedaRecibo;
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
async function MostrarRecibo() {
    $("#divCantRegistrosBusqueda").css("display", "none");
    $("#Grilla").css("display", "none");
    $("#EntidadCUIT").val(_ObjRecibo.CUIT);
    let TempEmpresa = await Empresa.TraerUnaXCUIT(_ObjRecibo.CUIT);
    $("#EntidadCodigoEntidad").val(await TempEmpresa.StrCodigo(6));
    $("#EntidadRazonSocial").val(TempEmpresa.RazonSocial);
    $("#EntidadFecha").val(_ObjRecibo.Fecha);
    $("#EntidadNroRecibo").val(Right('0000000000' + _ObjRecibo.NroReciboFin, 10));
    $("#EntidadImporteTotal").val(separadorMiles(_ObjRecibo.ImporteTotal.toFixed(2)));
    $("#EntidadImporteEfectivo").val(separadorMiles(_ObjRecibo.ImporteEfectivo.toFixed(2)));
    $("#EntidadObservaciones").val(_ObjRecibo.Observaciones);
    _ListaPagos = await _ObjRecibo.ListaPagos();
    _ListaPeriodos = await _ObjRecibo.ListaPeriodos();
    await MostrarPagos();
    await MostrarPeriodos();
    $("#ContenedorSeleccionadoManual").css("display", "block");
    $("#BtnGuardarRecibo").css("display", "none");
}
async function LlenarGrilla() {
    $("#Grilla").css("display", "none");
    if (_ListaIngresosManuales.length > 0) {
        await Recibo.ArmarGrillaCabecera('GrillaCabecera');
        await Recibo.ArmarGrillaDetalle('GrillaDetalle', _ListaIngresosManuales, 'EventoSeleccionarIngresoManual', 'height: 350px; overflow-y: scroll;');
        $("#Grilla").css("display", "block");
    } else {
        throw "No existen Ingresos para mostrar con esos parámetros";
    }
}
$('body').on('click', '#BtnNuevoRecibo', async function (e) {
    try {
        _ObjRecibo = new Recibo;
        $("#divCantRegistrosBusqueda").css("display", "none");
        $("#Grilla").css("display", "none");
        _ListaIngresosManuales = [];
        await InicializarPagos();
        await InicializarPeriodos();
        $("#ContenedorSeleccionadoManual").css("display", "block");
        $("#EntidadCUIT").focus();
    } catch (error) {
        alertAlerta(error);
    }
});
$('body').on('keydown', '#EntidadCUIT', async function (e) {
    try {
        let Texto = $("#EntidadCUIT").val();
        _ObjRecibo.CUIT = 0;
        $("#EntidadCodigoEntidad").val('');
        $("#EntidadRazonSocial").val('');
        if (e.keyCode === 13) {
            if (Texto.length != 11) {
                throw 'Debe ingresar 11 dígitos para buscar por CUIT';
            }
            spinner();
            let TempEmpresa = await Empresa.TraerUnaXCUIT(Texto);
            $("#EntidadCodigoEntidad").val(await TempEmpresa.StrCodigo(6));
            _ObjRecibo.CodigoEntidad = TempEmpresa.Codigo;
            _ObjRecibo.CUIT = TempEmpresa.CUIT;
            $("#EntidadRazonSocial").val(TempEmpresa.RazonSocial);
            spinnerClose();
        }
    } catch (error) {
        _ObjRecibo.CUIT = 0;
        _ObjRecibo.CodigoEntidad = 0;
        $("#EntidadCodigoEntidad").val('');
        $("#EntidadRazonSocial").val('');
        spinnerClose();
        alertAlerta(error);
    }
});
$('body').on('click', '#BtnGuardarRecibo', async function (e) {
    try {
        spinner();
        await ValidarCampos();
        let TempNroRecibo = Right('0000000000' + $("#EntidadNroRecibo").val(), 10);
        _ObjRecibo.NroReciboInicio = Left(TempNroRecibo, 4);
        _ObjRecibo.NroReciboFin = Right(TempNroRecibo, 6);
        let Fecha = $("#EntidadFecha").val();
        _ObjRecibo.Fecha = dateStringToLong($("#EntidadFecha").val());
        _ObjRecibo.ImporteTotal = parseFloat($("#EntidadImporteTotal").val());
        _ObjRecibo.ImporteEfectivo = parseFloat($("#EntidadImporteEfectivo").val());
        _ObjRecibo.Observaciones = $("#EntidadObservaciones").val();
        await _ObjRecibo.Alta(_ListaPagos, _ListaPeriodos);
        await LimpiarFormulario();
        spinnerClose();
        alertOk('El recibo se ha guardado correctamente');
    } catch (error) {
        spinnerClose();
        alertAlerta(error);
    }
});
async function ValidarCampos() {
    if (_ObjRecibo.CUIT == 0) {
        throw 'Debe ingresar la Entidad';
    }
    let FechaRecibo = $("#EntidadFecha").val();
    if (FechaRecibo.length == 0) {
        throw 'Debe ingresar la Fecha del Recibo';
    }
    let NroRecibo = $("#EntidadNroRecibo").val();
    if (NroRecibo.length == 0) {
        throw 'Debe ingresar el Número del Recibo';
    } else {
        if (parseInt(NroRecibo) <= 0) {
            throw 'El Número del Recibo debe ser mayor a 0';
        }
    }
    let ImporteTotal = $("#EntidadImporteTotal").val();
    if (ImporteTotal.length == 0) {
        throw 'Debe ingresar el Importe del Recibo';
    } else {
        if (parseFloat(ImporteTotal) <= 0) {
            throw 'El Importe del Recibo debe ser mayor a 0';
        }
    }
    if (_ListaPeriodos?.length == 0) {
        throw 'Debe ingresar al menos un Período';
    }
    let ImporteEfectivo = $("#EntidadImporteEfectivo").val();
    if (ImporteEfectivo.length == 0) {
        $("#EntidadImporteEfectivo").val(0);
    }
    let ImportePagos = parseFloat($("#EntidadImporteEfectivo").val());
    let iPag = 0
    while (iPag <= _ListaPagos.length - 1) {
        ImportePagos += parseFloat(_ListaPagos[iPag].Importe);
        iPag++;
    }
    let ImportePeriodos = 0;
    let iPer = 0
    while (iPer <= _ListaPeriodos.length - 1) {
        ImportePeriodos += parseFloat(_ListaPeriodos[iPer].Importe);
        iPer++;
    }
    alert('ImportePagos' + parseFloat(ImportePagos))
    if (parseFloat(ImportePagos) !== parseFloat(ImporteTotal) || parseFloat(ImportePeriodos) !== parseFloat(ImporteTotal)) {
        let Mensaje = '<b><u>Validar Importes</u></b><br><br>';
        Mensaje += '<div style="text-align: left;">';
        Mensaje += '- <i>Importe Recibo $ ' + separadorMiles(parseFloat(ImporteTotal).toFixed(2)) + '</i><br>';
        Mensaje += '- Importe Periodos $ ' + separadorMiles(parseFloat(ImportePeriodos).toFixed(2)) + '<br>';
        Mensaje += '- Importe Pagos $ ' + separadorMiles(parseFloat(ImportePagos).toFixed(2)) + '<br>';
        Mensaje += '</div>';
        throw Mensaje;
    }
}
// Cheques / Transferencias
$('body').on('click', '#BtnChequesTransferencias', async function (e) {
    try {
        let NroRecibo = $("#EntidadNroRecibo").val();
        if (NroRecibo.length == 0) {
            throw 'Debe ingresar el Nro. del Recibo para agregar Cheques y Transferencias.';
        } else {
            if (NroRecibo == 0) {
                throw 'Debe ingresar el Nro. del Recibo para agregar Cheques y Transferencias.';
            }
        }
        await ArmarPopUpChequeTransferencia(_ListaPagos.length);
    } catch (error) {
        alertAlerta(error);
    }
});
async function InicializarPagos() {
    _ListaPagos = [];
    await Recibo.ArmarGrillaPagos('GrillaPagos', _ListaPagos, '', '');
}
function LimpiarPopUp() {
    $("#PopReciboChequeNro").val('');
    $("#PopReciboChequeImporte").val('');
    $("#PopReciboChequeVencimiento").val('');
    $("#_DivCboBanco").val(0)
    $("#PopReciboTransferenciaNro").val('');
    $("#PopReciboTransferenciaImporte").val('');

}
async function ArmarPopUpChequeTransferencia(Cantidad) {
    if (parseInt($("#Modal-PopUpChequesTransferencias").length) === 0) {
        let control = '';
        control += '<div id="Modal-PopUpChequesTransferencias" class="modal" tabindex="-1" role="dialog" >';
        control += '    <div class="modal-dialog modal-lg">';
        control += '        <div class="modal-content">';
        control += '            <div class="modal-header HeaderPopUp bg-success text-light">';
        control += '                <div class="col-9">';
        control += '                    <h3 class="modal-title">Cheques y Transferencias</h3>';
        control += '                </div>';
        control += '                <div class="col-3 text-right">';
        control += '                    <h3 class="modal-title"><span id="LblPopCantidadPagos"></span></h3>';
        control += '                </div>';
        control += '            </div>';
        control += '            <div class="modal-body">';
        control += '                <div class="row mb-1">';
        control += '                    <div class="col-md-2 text-right pr-2">Cheque: </div>';
        control += '                    <div class="col-md-3"><div id="DivCboBanco"></div></div>';
        control += '                    <div class="col-md-3"><input type="text" id="PopReciboChequeImporte" class="form-control text-right" onkeypress="return jsSoloNumeros(event)" placeholder="Importe"></div>';
        control += '                    <div class="col-md-4"></div>';
        control += '                </div>';
        control += '                <div class="row mb-2">';
        control += '                    <div class="col-md-2 text-right pr-2"></div>';
        control += '                    <div class="col-md-3"><input type="text" id="PopReciboChequeNro" class="form-control text-center" placeholder="10 dígitos" onkeypress="return jsSoloNumerosSinPuntos(event)" maxlength="10" ></div>';
        control += '                    <div class="col-md-3"><input type="text" id="PopReciboChequeVencimiento" class="form-control datepicker text-center" onkeypress="return jsSoloNumeros(event)" placeholder="Venc. (ddMMaaaa)" maxlength="8"></div>';
        control += '                    <div class="col-md-4"><a href="#" id="BtnAgregarCheque" class="btn btn-md btn-block btn-success"> Agregar Cheque</a></div>';
        control += '                </div>';
        // control += '                <div class="row">';
        // control += '                    <div class="col-md-2 text-right pr-2">Transferencia: </div>';
        // control += '                    <div class="col-md-3"><input type="text" id="PopReciboTransferenciaNro" class="form-control text-center" placeholder="10 dígitos" onkeypress="return jsSoloNumerosSinPuntos(event)" maxlength="10" ></div>';
        // control += '                    <div class="col-md-3"><input type="text" id="PopReciboTransferenciaImporte" class="form-control text-right" onkeypress="return jsSoloNumeros(event)" placeholder="Importe"></div>';
        // control += '                    <div class="col-md-4"><a href="#" id="BtnAgregarTransferencia" class="btn btn-md btn-block btn-success"> Agregar Transferencia</a></div>';
        // control += '                </div>';
        control += '            </div>';
        control += '            <div class="modal-footer">';
        control += '                <button type="button" class="btn btn-success" data-dismiss="modal">Cerrar</button>';
        control += '            </div>';
        control += '        </div>';
        control += '    </div>';
        control += '</div>';
        $("body").append(control);
    }
    await Banco.ArmarCombo(await Banco.Todos(), "DivCboBanco", 'IdDivCboBanco', 'EventoBancoSeleccionado', 'Banco ...', 'form-control');
    $('#Modal-PopUpChequesTransferencias').modal({ show: true });
    $("#LblPopCantidadPagos").text(Cantidad);
}
async function MostrarPagos() {
    $("#GrillaPagos").html('');
    if (_ListaPagos?.length > 0) {
        await Recibo.ArmarGrillaPagos('GrillaPagos', _ListaPagos, 'height: 150px; overflow-y: scroll;', 'EventoEliminarPago');
    }
}
$('body').on('click', '#BtnAgregarCheque', async function (e) {
    try {
        spinner();
        let Numero = Right('0000000000' + parseInt($("#PopReciboChequeNro").val()), 10);
        let Importe = $("#PopReciboChequeImporte").val();
        let IdBanco = $("#_DivCboBanco").val();
        let Vencimiento = $("#PopReciboChequeVencimiento").val();
        Vencimiento = Right(Vencimiento, 4) + Right(Left(Vencimiento, 4), 2) + Left(Vencimiento, 2)
        let Tipo = 1; //  Cheque
        await ValidarNuevoPago(parseInt(Tipo), Importe, Numero, IdBanco, Vencimiento);
        await AgregarPago(parseInt(Tipo), parseFloat(Importe), Numero, IdBanco, Vencimiento);
        $("#LblPopCantidadPagos").text(_ListaPagos.length);
        LimpiarPopUp();
        await MostrarPagos();
        spinnerClose()
    } catch (error) {
        spinnerClose()
        alertAlerta(error);
    }
});
$('body').on('click', '#BtnAgregarTransferencia', async function (e) {
    try {
        spinner();
        let Numero = $("#PopReciboTransferenciaNro").val();
        let Importe = $("#PopReciboTransferenciaImporte").val();
        let IdBanco = 0;
        let Vencimiento = 0;
        let Tipo = 2; //  Transferencia
        await ValidarNuevoPago(parseInt(Tipo), Importe, Numero, IdBanco, Vencimiento);
        await AgregarPago(parseInt(Tipo), parseFloat(Importe), Numero, IdBanco, Vencimiento);
        $("#LblPopCantidadPagos").text(_ListaPagos.length);
        LimpiarPopUp();
        await MostrarPagos();
        spinnerClose()
    } catch (error) {
        spinnerClose()
        alertAlerta(error);
    }
});
document.addEventListener('EventoSeleccionarIngresoManual', async function (e) {
    try {
        let objSeleccionado = e.detail;
        let buscado = $.grep(_ListaIngresosManuales, function (entidad, index) {
            return entidad.IdEntidad == objSeleccionado.IdEntidad;
        });
        _ObjRecibo = buscado[0];
        await MostrarRecibo();

    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('EventoEliminarPago', async function (e) {
    try {
        let objSeleccionado = e.detail;
        if (_ListaPagos?.length > 0) {
            let buscado = $.grep(_ListaPagos, function (entidad, index) {
                return entidad.IdEntidad != objSeleccionado.IdEntidad;
            });
            _ListaPagos = buscado;
        }
        await MostrarPagos();
    } catch (e) {
        alertAlerta(e);
    }
}, false);
async function ValidarNuevoPago(IdTipoPagoManual, Importe, Numero, IdBanco, Vencimiento) {
    let error = '';
    switch (IdTipoPagoManual) {
        case 0:
            error += 'Seleccione el tipo de Pago <br>';
            break;
        case 1: // Cheque
            if (IdBanco == 0) {
                error += 'Debe seleccionar el Banco. <br>';
            }
            console.log(Vencimiento.length);
            if (Vencimiento.length != 8) {
                error += 'Debe ingresar la Fecha de Vencimiento (8 dígitos). <br>';
            } else {
                if (Vencimiento == 0) {
                    error += 'Debe ingresar la Fecha de Vencimiento (8 dígitos). <br>';
                } else {
                    let hoy = FechaHoyLng();
                    if (Vencimiento < hoy) {
                        error += 'Verifique la Fecha del Vencimiento (ddMMaaaa). <br>';
                    }
                }
            }
            if (Numero.length != 10) {
                error += 'El Número debe tener 10 dígitos. <br>';
            }
            if (error.length > 0) {
                error = '<b>Cheque</b><br><br><div class="text-left">' + error;
            }
            break;
        case 2: // Transferencia
            if (Numero.length != 10) {
                error += 'El Número de la Transferencia debe tener 10 dígitos. <br>';
            }
            break;
        default:
            break;
    }
    if (Importe.length == 0) {
        error += 'Informe el Importe <br>';
    } else {
        if (Importe == 0) {
            error += 'El Importe debe ser mayor a 0<br>';
        }
    }
    if (error.length > 0) {
        throw error + '</div>';
    }
}
async function AgregarPago(IdTipoPagoManual, Importe, Numero, IdBanco, Vencimiento) {
    let Id = 1;
    if (_ListaPagos?.length > 0) {
        let i = 0;
        while (i <= _ListaPagos.length - 1) {
            if (_ListaPagos[i].IdEntidad >= Id) {
                Id = _ListaPagos[i].IdEntidad + 1
            }
            i++;
        }
    }
    _ObjPago = { IdEntidad: Id, IdTipoPagoManual: IdTipoPagoManual, Importe: Importe, Numero: Numero, IdBanco: IdBanco, Vencimiento: Vencimiento }
    _ListaPagos.push(_ObjPago);
}

// Periodo
async function InicializarPeriodos() {
    _ListaPeriodos = [];
    await Recibo.ArmarGrillaPeriodos('GrillaPeriodos', _ListaPeriodos, '', '');

}
async function MostrarPeriodos() {
    $("#GrillaPeriodos").html('');
    if (_ListaPeriodos?.length > 0) {
        await Recibo.ArmarGrillaPeriodos('GrillaPeriodos', _ListaPeriodos, 'height: 150px; overflow-y: scroll;', 'EventoEliminarPeriodo');
    }
}
$('body').on('click', '#BtnAgregarPeriodo', async function (e) {
    try {
        spinner();
        let Importe = $("#PeriodoImporte").val();
        let Periodo = $("#PeriodoPeriodo").val();
        await ValidarNuevoPeriodo(Importe, Periodo);
        await AgregarPeriodo(parseFloat(Importe), Periodo);
        LimpiarPeriodo();
        await MostrarPeriodos();
        spinnerClose()
        $("#PeriodoPeriodo").focus();
    } catch (error) {
        spinnerClose()
        alertAlerta(error);
    }
});
document.addEventListener('EventoEliminarPeriodo', async function (e) {
    try {
        let objSeleccionado = e.detail;
        if (_ListaPeriodos?.length > 0) {
            let buscado = $.grep(_ListaPeriodos, function (entidad, index) {
                return entidad.IdEntidad != objSeleccionado.IdEntidad;
            });
            _ListaPeriodos = buscado;
        }
        await MostrarPeriodos();
    } catch (e) {
        alertAlerta(e);
    }
}, false);
async function ValidarNuevoPeriodo(Importe, Periodo) {
    let error = '';
    if (Importe.length == 0) {
        error += 'Informe el Importe <br>';
    } else {
        if (Importe == 0) {
            error += 'El Importe debe ser mayor a 0<br>';
        }
    }
    if (Periodo.length == 0) {
        error += 'Informe el Periodo <br>';
    } else {
        if (Periodo.length != 6) {
            error += 'El Periodo debe tener 6 dígitos (2 del mes y 4 del año) <br>';
        } else {
            let Mes = Left(Periodo, 2);
            if (Mes < 0 || Mes > 12) {
                error += 'El Mes debe ser entre 01 y 12<br>';
            }
            let Anio = Right(Periodo, 4);
            if (Anio > new Date().getFullYear()) {
                error += 'El Año NO debe ser mayor al Actual<br>';

            }
        }
    }
    if (error.length > 0) {
        throw error;
    }
}
async function AgregarPeriodo(Importe, Periodo) {
    let Id = 1;
    if (_ListaPeriodos?.length > 0) {
        let i = 0;
        while (i <= _ListaPeriodos.length - 1) {
            if (_ListaPeriodos[i].IdEntidad >= Id) {
                Id = _ListaPeriodos[i].IdEntidad + 1
            }
            i++;
        }
    }
    _ObjPeriodo = { IdEntidad: Id, Importe: Importe, Periodo: Periodo }
    _ListaPeriodos.push(_ObjPeriodo);
}
function LimpiarPeriodo() {
    _ObjPago = undefined;
    $("#PeriodoImporte").val('');
    $("#PeriodoPeriodo").val('');
}