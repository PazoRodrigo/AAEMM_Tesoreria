var _ListaEmpleados;
var _ObjEmpleado;

$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Empleados');
        $("#divCantRegistrosImprimir").css('display', 'none');
        Inicio();
    } catch (e) {
        alertAlerta(e);
    }
});

async function Inicio() {
    LimpiarFormulario();
}

async function LimpiarFormulario() {
    LimpiarBuscador();
    LimpiarGrilla();
    LimpiarEmpleado();
}

function LimpiarBuscador() {
    $("#switchIncluirAfiliados").prop('checked', true);
    $("#switchIncluirNoAfiliados").prop('checked', false);
    $("#BuscaNombre").val('');
    $("#BuscaDNI").val('');
    $("#BuscaCUIL").val('');
    $("#BuscaRazonSocal").val('');
    $("#BuscaCUIT").val('');
}

function LimpiarGrilla() {
    $("#Grilla").css("display", "none");
}

function LimpiarEmpleado() {
    $("#EntidadAfiliado").prop('checked', false);
    $("#EntidadNombre").val('');
    $("#EntidadDNI").val('');
    $("#EntidadCUIL").val('');
    $("#EntidadRazonSocial").val('');
    $("#EntidadCUIT").val('');
}

$('body').on('click', '#BtnBuscador', async function (e) {
    try {
        $("#Seleccionado").css("display", "none");
        $("#divCantRegistrosImprimir").css('display', 'none');
        spinner();
        LimpiarGrilla();
        let Busqueda = await ArmarBusqueda();
        _ListaEmpleados = await Empleado.TraerTodosXBusqueda(Busqueda);
        await LlenarGrilla();
        if (_ListaEmpleados.length > 0) {
            let TextoCantidadRegistros = "Imprimir " + _ListaEmpleados.length + " registro";
            if (_ListaEmpleados.length > 1) {
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
    let Nombre = $("#BuscaNombre").val();
    let CUIL = $("#BuscaCUIL").val();
    let DNI = $("#BuscaDNI").val();
    let CUIT = $("#BuscaCUIT").val();
    let RazonSocial = $("#BuscaRazonSocial").val();
    let ChkAfiliado = 0;
    let ChkNoAfiliado = 0;
    if ($("#switchIncluirAfiliado").prop("checked") == true) {
        ChkAfiliado = 1;
    }
    if ($("#switchIncluirNoAfiliado").prop("checked") == true) {
        ChkNoAfiliado = 1;
    }
 
    let Buscador = new StrBusquedaEmpleado;
    Buscador.IncluirAfiliados = ChkAfiliado;
    Buscador.IncluirNoAfiliados = ChkNoAfiliado;
    if (Nombre.length != 0) {
        Buscador.Nombre = Nombre;
    }
    if (CUIL.length != 0) {
        Buscador.CUIL = CUIL;
    }
    if (DNI.length != 0) {
        Buscador.DNI = DNI;
    }
    if (RazonSocial.length != 0) {
        Buscador.RazonSocial = RazonSocial;
    }
    if (CUIT.length != 0) {
        Buscador.CUIT = CUIT;
    }
    return Buscador;
}

async function LlenarGrilla() {
    $("#Grilla").css("display", "none");
    $("#LblCantidadRegistrosGrilla").css("display", "none");
    console.log(_ListaEmpleados.length);
    if (_ListaEmpleados.length > 0) {
        await Empleado.ArmarGrillaCabecera('GrillaCabecera');
        await Empleado.ArmarGrillaDetalle('GrillaDetalle', _ListaEmpleados, 'EventoSeleccionarEmpleado', 'max-height: 350px; overflow-y: scroll;');
        $("#Grilla").css("display", "block");
        let TextoCantidadRegistros = "Cantidad de Registros: " + _ListaEmpleados.length;
        $("#LblCantidadRegistrosGrilla").text(TextoCantidadRegistros);
        $("#LblCantidadRegistrosGrilla").css("display", "block");
    } else {
        throw ("No existen Empleados para mostrar con esos parámetros");
    }
}

async function LlenarEmpleado() {
    LimpiarEmpleado();
    if (_ObjEmpleado == undefined) {
        throw ('No existe Empleado seleccionado');
    }
    $("#EntidadCUIT").val(_ObjEmpleado.Nombre);
    $("#EntidadCUIT").val(_ObjEmpleado.CUIL);
    $("#EntidadCUIT").val(_ObjEmpleado.DNI);
    $("#EntidadCUIT").val(_ObjEmpleado.CUIT);
    $("#EntidadRazonSocial").val(_ObjEmpleado.RazonSocial);
}

document.addEventListener('EventoSeleccionarEmpleado', async function (e) {
    try {
        let objSeleccionado = e.detail;
        let listaTempEmpleados = [];
        listaTempEmpleados.push(objSeleccionado);
        await Emopleado.ArmarGrillaDetalle('GrillaDetalle', listaTempEmpleados, 'EventoSeleccionarEmpleado', '');
        $("#GrillaCabecera").html('');
        _ObjEmpleado = objSeleccionado;
        await LlenarEmpleado();
        $("#Seleccionado").css("display", "block");
    } catch (e) {
        alertAlerta(e);
    }
}, false);

function Nuevo_Empleado() {
    LimpiarEmpleado();
    _ObjEmpleado = new Empleado;
}