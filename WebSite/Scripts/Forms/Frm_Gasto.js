var _ObjGasto;
var _ObjComprobante;


$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Gastos');
        $("#SpanBtnComprobante").text('Nuevo Comprobante');
        $("#SpanBtnGasto").text('Nuevo Gasto');
        $("#SpanBtnBuscar").text('Buscar');
        $("#SpanBtnGuardarComprobante").text('Guardar Comprobante');
        $("#SpanBtnCerrarGasto").text('Cerrar Gasto');
        $("#SpanBtnImprimirGasto").text('Imprimir Gasto');
        Inicio();
    } catch (e) {
        alertAlerta(e);
    }
});
async function Inicio() {
    Nuevo_Gasto();
    await LlenarGrilla_Gasto();
    await MostrarSolapaGasto();
    await LimpiarComprobante();
}

function Limpiar_Gasto() {
    $(".DatoFormulario").val('');
}
function Nuevo_Gasto() {
    Limpiar_Gasto();
    _ObjGasto = new Gasto;
    $("#TxtNombre").focus();
}
function Llenar_Gasto(Obj_Gasto) {
    Nuevo_Gasto();
    _ObjGasto.IdEntidad = Obj_Gasto.IdEntidad;
    _ObjGasto.Nombre = Obj_Gasto.Nombre;
    _ObjGasto.Observaciones = Obj_Gasto.Observaciones;
    $("#TxtNombre").val(_ObjGasto.Nombre);
    $("#TxtObservaciones").val(_ObjGasto.Observaciones);
}
async function LlenarGrilla_Gasto() {
    let lista = await Gasto.TraerTodos();
    Gasto.ArmarGrilla(lista, 'GrillaGastosRegistrados', 'EventoSeleccionarGasto', 'EventoEliminarGasto', 'height:300px; overflow-y: scroll');
}
async function LlenarCboOriginario() {
    let lista = await OriginarioGasto.TraerTodos();
    OriginarioGasto.ArmarCombo(lista, 'CboOriginarioGasto', 'SelectorGasto', 'EventoSeleccionOriginarioGasto', 'Seleccione', '');
}
async function LlenarCboProveedor() {
    let lista = await Proveedor.TraerTodos();
    Proveedor.ArmarCombo(lista, 'CboProveedor', 'SelectorProveedor', 'EventoSeleccionProveedor', 'Seleccione', '');
}
async function LlenarCboCentroCosto() {
    let lista = await CentroCosto.TraerTodos();
    CentroCosto.ArmarCombo(lista, 'CboCentroCosto', 'SelectorCentroCosto', 'EventoSeleccionCentroCosto', 'Seleccione', '');
}
async function LlenarCboCuenta() {
    let lista = await CuentaContable.TraerTodos();
    CuentaContable.ArmarCombo(lista, 'CboCuenta', 'SelectorCuenta', 'EventoSeleccionCuenta', 'Seleccione', '');
}
async function LlenarCboTipoGasto() {
    let lista = await TipoGasto.TraerTodos();
    TipoGasto.ArmarCombo(lista, 'CboTipoGasto', 'SelectorTipoGasto', 'EventoSeleccionTipoGasto', 'Seleccione', '');
}

function MostrarSolapaGasto() {
    $(".btnGastoOn").css("display", "block");
    $(".btnGastoOff").css("display", "none");
    $(".btnComprobanteOn").css("display", "none");
    $(".btnComprobanteOff").css("display", "block");
    $("#GrillaGastosRegistrados").css("display", "block");
    $("#GrillaComprobantesRegistrados").css("display", "none");
}
function MostrarSolapaComprobante() {
    $(".btnGastoOn").css("display", "none");
    $(".btnGastoOff").css("display", "block");
    $(".btnComprobanteOn").css("display", "block");
    $(".btnComprobanteOff").css("display", "none");
    $("#GrillaGastosRegistrados").css("display", "none");
    $("#GrillaComprobantesRegistrados").css("display", "block");
}
$('body').on('click', '.btnGastoOff', async function (e) {
    try {
        MostrarSolapaGasto();
    } catch (e) {
        alertAlerta(e);
    }
});
$('body').on('click', '.btnComprobanteOff', async function (e) {
    try {
        MostrarSolapaComprobante();
    } catch (e) {
        alertAlerta(e);
    }
});

// Gasto
$('body').on('click', '#LinkBtnNuevoGasto', async function (e) {
    try {
        //await Gasto.Alta();
        _ObjGasto = new Gasto;

    } catch (e) {
        alertAlerta(e);
    }
});

// Comprobante
$('body').on('click', '#LinkBtnNuevoComprobante', async function (e) {
    try {
        _ObjComprobante = new Comprobante;
        await LimpiarComprobante();
    } catch (e) {
        alertAlerta(e);
    }
});
$('body').on('click', '#LinkBtnGuardarComprobante', async function (e) {
    try {
        if (_ObjComprobante === undefined) {
            throw 'Debe ingresar los Datos del comprobante';
        }
        await _ObjComprobante.Alta();
        await LimpiarComprobante();
        alertOk('El Comprobante se ha guardado correctamente');
    } catch (e) {
        alertAlerta(e);
    }
});
async function LimpiarComprobante() {
    Limpiar_Gasto();
    await LlenarCboOriginario();
    await LlenarCboProveedor();
    await LlenarCboCuenta();
    await LlenarCboCentroCosto();
    await LlenarCboTipoGasto();
    await LlenarGrilla_Gasto();
}


document.addEventListener('EventoSeleccionarGasto', async function (e) {
    try {
        let objSeleccionado = e.detail;
        _ObjGasto = objSeleccionado;
        let Lista = await Comprobante.TraerTodasXGasto(_ObjGasto.IdEntidad);
        MostrarSolapaComprobante();
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('EventoSeleccionOriginarioGasto', async function (e) {
    try {
        let objSeleccionado = e.detail;
        if (_ObjComprobante === undefined) {
            _ObjComprobante = new Comprobante;
        }
        _ObjComprobante.IdOriginarioGasto = objSeleccionado.IdEntidad;
        $("#SelectorGasto").text(objSeleccionado.Nombre);
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('EventoSeleccionProveedor', async function (e) {
    try {
        let objSeleccionado = e.detail;
        if (_ObjComprobante === undefined) {
            _ObjComprobante = new Comprobante;
        }
        console.log(objSeleccionado);
        _ObjComprobante.IdProveedor = objSeleccionado.IdEntidad;
        $("#SelectorProveedor").text(objSeleccionado.Nombre);
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('EventoSeleccionCentroCosto', async function (e) {
    try {
        let objSeleccionado = e.detail;
        if (_ObjComprobante === undefined) {
            _ObjComprobante = new Comprobante;
        }
        _ObjComprobante.IdCentroCosto = objSeleccionado.IdEntidad;
        $("#SelectorCentroCosto").text(objSeleccionado.Nombre);
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('EventoSeleccionCuenta', async function (e) {
    try {
        let objSeleccionado = e.detail;
        if (_ObjComprobante === undefined) {
            _ObjComprobante = new Comprobante;
        }
        _ObjComprobante.IdCuenta = objSeleccionado.IdEntidad;
        $("#SelectorCuenta").text(objSeleccionado.Nombre);
    } catch (e) {
        alertAlerta(e);
    }
}, false);
document.addEventListener('EventoSeleccionTipoGasto', async function (e) {
    try {
        let objSeleccionado = e.detail;
        if (_ObjComprobante === undefined) {
            _ObjComprobante = new Comprobante;
        }
        _ObjComprobante.IdTipoGasto = objSeleccionado.IdEntidad;
        $("#SelectorTipoGasto").text(objSeleccionado.Nombre);
    } catch (e) {
        alertAlerta(e);
    }
}, false);