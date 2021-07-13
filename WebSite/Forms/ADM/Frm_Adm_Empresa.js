﻿var _ListaEmpresas;
var _ObjEmpresa;

$(document).ready(async function () {
    try {
        $("#SpanNombreFormulario").text('Empresas');
        $("#divCantRegistrosImprimir").css('display', 'none');
        $("#LinkBtnNuevo").css('display', 'none');
        await Inicio();
    } catch (e) {
        alertAlerta(e);
    }
});

async function Inicio() {
    await LimpiarFormulario();
}

async function LimpiarFormulario() {
    await LimpiarBuscador();
    LimpiarGrilla();
    LimpiarEmpresa();
}

async function LimpiarBuscador() {
    $("#switchIncluirAlta").prop('checked', true);
    $("#switchIncluirBaja").prop('checked', false);
    $("#switchIncluirIncluir0").prop('checked', false);
    $("#BuscaRazonSocal").val('');
    $("#BuscaCUIT").val('');
    let lista = await CentroCosto.Todos();
    await CentroCosto.ArmarCombo(lista, 'BuscaCboCentroCosto', 'SelectorCentroCosto', '', 'Centro de Costo', 'CboBuscadorCC');
}

function LimpiarGrilla() {
    $("#Grilla").css("display", "none");
}

async function LimpiarEmpresa() {
    $("#EntidadCUIT").val('');
    $("#EntidadCodigoEntidad").val('');
    $("#EntidadRazonSocial").val('');
    await ArmarComboCentroCosto();
    await ArmarComboConvenio();
}

$('body').on('click', '#BtnBuscador', async function (e) {
    try {
        $("#Seleccionado").css("display", "none");
        $("#divCantRegistrosImprimir").css('display', 'none');
        spinner();
        LimpiarGrilla();
        let Busqueda = await ArmarBusqueda();
        _ListaEmpresas = await Empresa.TraerTodosXBusqueda(Busqueda);
        await LlenarGrilla();
        if (_ListaEmpresas.length > 0) {
            let TextoCantidadRegistros = "Imprimir " + _ListaEmpresas.length + " registro";
            if (_ListaEmpresas.length > 1) {
                TextoCantidadRegistros += "s";
            }
            $("#LblCantidadRegistrosGrilla").text(TextoCantidadRegistros);
            $("#divCantRegistrosImprimir").css('display', 'block');
            $("#LinkBtnNuevo").css('display', 'block');
        }
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#LinkBtnNuevo', async function (e) {
    try {
        $("#Seleccionado").css("display", "block");
        $("#divCantRegistrosImprimir").css('display', 'none');
        $("#LinkBtnNuevo").css('display', 'none');
        $("#GrillaCabecera").html('');
        $("#GrillaDetalle").html('');
        NuevaEmpresa();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});

$('body').on('click', '#BtnCuentaCorriente', async function (e) {
    try {
        sessionStorage.setItem("ObjEmpresaCUIT", _ObjEmpresa.CUIT);
        window.location.href = "../ADM/Frm_Adm_EmpresaCtaCte.aspx";
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#BtnGuardar', async function (e) {
    try {
        spinner();
        _ObjEmpresa.CUIT = $("#EntidadCUIT").val();
        _ObjEmpresa.RazonSocial = $("#EntidadRazonSocial").val();
        _ObjEmpresa.ObjDomicilio = new Domicilio;
        let TextoOK = '';
        if (_ObjEmpresa.IdEntidad == 0) {
            await _ObjEmpresa.Alta();
            let CodEntidad = await _ObjEmpresa.StrCodigo(6);
            $("#EntidadCodigoEntidad").val(CodEntidad);
            TextoOK = 'La Empresa se ha dado de alta correctamente. Su Código de Entidad es ' + CodEntidad;
        } else {
            await _ObjEmpresa.Modifica();
        }
        spinnerClose();
        alertOk(TextoOK);
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#BtnEmpresaReactivar', async function (e) {
    try {
        spinner();
        await _ObjEmpresa.Baja();
        spinnerClose();
        alertOk('La Empresa se ha reactivado correctamente.');
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#BtnEmpresaBaja', async function (e) {
    try {
        spinner();
        await _ObjEmpresa.Baja();
        spinnerClose();
        alertOk('La Empresa se ha dado de baja correctamente.');
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});

async function ArmarBusqueda() {
    let CUIT = $("#BuscaCUIT").val();
    let RazonSocial = $("#BuscaRazonSocial").val();
    let ChkAlta = 0;
    let ChkBaja = 0;
    let ChkCUIT0 = 0;
    if ($("#switchIncluirAlta").prop("checked") == true) {
        ChkAlta = 1;
    }
    if ($("#switchIncluirBaja").prop("checked") == true) {
        ChkBaja = 1;
    }
    // if ($("#switchIncluirCUIT0").prop("checked") == true) {
    //     ChkCUIT0 = 1;
    // }
    let Buscador = new StrBusquedaEmpresa;
    //this.IdCentroCosto = 0;
    Buscador.IncluirAlta = ChkAlta;
    Buscador.IncluirBaja = ChkBaja;
    Buscador.Incluir0 = ChkCUIT0;
    if (RazonSocial.length != 0) {
        Buscador.RazonSocial = RazonSocial;
    }
    if (CUIT.length != 0) {
        Buscador.CUIT = CUIT;
    }
    // if (IdCentroCosto > 0) {
    //     Buscador.IdCentroCosto = IdCentroCosto;
    // }
    return Buscador;
}

async function LlenarGrilla() {
    $("#Grilla").css("display", "none");
    $("#LblCantidadRegistrosGrilla").css("display", "none");
    if (_ListaEmpresas.length > 0) {
        await Empresa.ArmarGrillaCabecera('GrillaCabecera');
        await Empresa.ArmarGrillaDetalle('GrillaDetalle', _ListaEmpresas, 'EventoSeleccionarEmpresa', 'max-height: 350px; overflow-y: scroll;');
        $("#Grilla").css("display", "block");
        let TextoCantidadRegistros = "Cantidad de Registros: " + _ListaEmpresas.length;
        $("#LblCantidadRegistrosGrilla").text(TextoCantidadRegistros);
        $("#LblCantidadRegistrosGrilla").css("display", "block");
    } else {
        $("#LinkBtnNuevo").css('display', 'block');
        throw ("No existen Empresas para mostrar con esos parámetros");
    }
}

async function LlenarEmpresa() {
    await LimpiarEmpresa();
    if (_ObjEmpresa == undefined) {
        throw ('No existe Empresa seleccionada');
    }
    $("#EntidadCUIT").val(_ObjEmpresa.CUIT);
    $("#EntidadCodigoEntidad").val(await _ObjEmpresa.StrCodigo(11));
    $("#EntidadRazonSocial").val(_ObjEmpresa.RazonSocial);
}

document.addEventListener('EventoSeleccionarEmpresa', async function (e) {
    try {
        let objSeleccionado = e.detail;
        let listaTempEmpresas = [];
        listaTempEmpresas.push(objSeleccionado);
        await Empresa.ArmarGrillaDetalle('GrillaDetalle', listaTempEmpresas, 'EventoSeleccionarEmpresa', '');
        $("#GrillaCabecera").html('');
        _ObjEmpresa = objSeleccionado;
        await LlenarEmpresa();
        $("#Seleccionado").css("display", "block");
        $("#LinkBtnNuevo").css('display', 'none');

    } catch (e) {
        alertAlerta(e);
    }
}, false);

function NuevaEmpresa() {
    LimpiarEmpresa();
    _ObjEmpresa = new Empresa;
}

async function ArmarComboCentroCosto() {
    let lista = await CentroCosto.Todos();
    await CentroCosto.ArmarCombo(lista, 'CboCentroCosto', 'SelectorCentroCosto', 'EventoSeleccionCentroCosto', 'Centro de Costo', 'CboBuscadorCC');
}

async function ArmarComboConvenio() {
    let lista = await Convenio.Todos();
    await Convenio.ArmarCombo(lista, 'CboConvenio', 'SelectorConvenio', 'EventoSeleccionConvenio', 'Convenio', 'CboConvenio');
}