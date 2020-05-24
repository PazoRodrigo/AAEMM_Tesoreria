var _ListaIngresos;
var _ObjIngreso;

$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Ingresos');
        $("#SpanBtnNuevo").text('Nuevo Ingreso');
        $("#SpanBtnBuscar").text('Buscar Ingreso');
        $("#SpanBtnEliminar").text('Eliminar');
        $("#SpanBtnGuardar").text('Guardar');
        $("#SpanBtnImprimir").text('Imprimir Ingreso');
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
    LimpiarImgreso();
}
function LimpiarBuscador() {
    $("#BuscaDesdeAcred").val('');
    $("#BuscaHastaAcred").val('');
    let ChecksEstado = $('[name ="CheckEstado"]');
    for (let chk of ChecksEstado) {
        chk.checked = false;
    }
    $("#BuscaEmpresa").val('');
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
    LimpiarImgreso();
    $("#Grilla").css("display", "none");
}
function LimpiarImgreso() {
}
$('body').on('click', '#BtnBuscador', async function (e) {
    try {

        //spinner();
        LimpiarGrilla();
        //let Busqueda = await ArmarBusqueda();
        //console.log(Busqueda);
        //_ListaIngresos = await Ingreso.TraerTodosXBusqueda(Busqueda);
        _ListaIngresos = await Ingreso.TraerTodos();
       
        //$("#Buscador").css("display", "none");
        //$("#Grilla").css("display", "block");
        await LlenarGrilla();
        //spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});

async function ArmarBusqueda() {
    let Desde = $("#BuscaDesdeAcred").val();
    let Hasta = $("#BuscaHastaAcred").val();
    let ChecksEstado = $('[name ="CheckEstado"]');
    let EstadosSeleccionados = [];
    for (let chk of ChecksEstado) {
        if (chk.checked === true) {
            EstadosSeleccionados.push(chk.value);
        }
    }
    let CUIT = $("#BuscaCUIT").val();
    let ChecksTipo = $('[name ="CheckTipo"]');
    let TiposSeleccionados = [];
    for (let chk of ChecksTipo) {
        if (chk.checked === true) {
            TiposSeleccionados.push(chk.value);
        }
    }
    let Importe = $("#BuscaImporte").val();
    let Recibo = $("#BuscaNroRecibo").val();
    let Cheque = $("#BuscaNroCheque").val();
    let Buscador = [];
    Buscador.Desde = dateStringToLong(Desde);
    console.log(Buscador.Desde);
    //Buscador.Hasta = Hasta;
    //Buscador.EstadosSeleccionados = EstadosSeleccionados;
    //Buscador.CUIT = CUIT;
    //Buscador.TiposSeleccionados = TiposSeleccionados;
    //Buscador.Importe = Importe;
    //Buscador.Recibo = Recibo;
    //Buscador.Cheque = Cheque;
    return Buscador;
}
async function LlenarGrilla() {
    $("#Grilla").css("display", "none");
    console.log(_ListaIngresos.length);
    if (_ListaIngresos.length > 0) {
        await Ingreso.ArmarGrillaCabecera('GrillaCabecera');
        await Ingreso.ArmarGrillaDetalle('GrillaDetalle', _ListaIngresos, 'EventoSeleccionarIngreso', 'max-height: 350px; overflow-y: scroll;');
        $("#Grilla").css("display", "block");
    }
}
