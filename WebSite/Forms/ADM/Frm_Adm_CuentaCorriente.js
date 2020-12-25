var _ObjAsiento;
var _ListaAsientoLineas;

$(document).ready(function () {
    try {
        $("#SpanNombreFormulario").text('Cuenta Corriente');
        $("#SpanTituloGrillaDimensional").text('Grilla de Asientos');
        $("#SpanTituloAsientos").text('Asientos');
        $("#SpanBtnImprimir").text('Imprimir Cuenta Corriente');
        $("#SpanBtnNuevo").text('Nuevo');
        $("#SpanBtnGuardar").text('Guardar Asiento');
        Inicio();
    } catch (e) {
        alertAlerta(e);
    }
});
async function Inicio() {
    await LlenarSaldosCuentas();
    await LlenarCboCuentasCorrientes();
    LimpiarFormulario();
    //await LlenarGrilla_CuentaCorriente();
}

function LimpiarFormulario() {
    LimpiarAsiento();
}

function LimpiarLineaAsiento() {
    $("#TxtImporte").val('');
    $('input[name="radioDH"]').prop('checked', false);
    $("#IdCboCuentaCorriente").val(0);
}
function LimpiarAsiento() {
    _ListaAsientoLineas = [];
    $("#DivGrillaLineasAsiento").html('');
    LimpiarLineaAsiento();
    $("#TxtFecha").val('');
    $("#LblImporteTotalAsientoD").text('');
    $("#LblImporteTotalAsientoH").text('');
}

async function LlenarCboCuentasCorrientes() {
    await CuentaCorriente.ArmarCombo(await CuentaCorriente.Todos(), 'CboCuentaCorriente', 'IdCboCuentaCorriente', 'Seleccione Cuenta', '', 'form-control');

}
async function LlenarSaldosCuentas() {
    let SaldosCuentaCorriente = await CuentaCorriente.TraerTodosSaldos();
    $("#spanSaldoCAJA").text(separadorMiles(separadorMiles(SaldosCuentaCorriente[0].Saldo.toFixed(2))));
    $("#spanSaldoFONDOFIJO").text(separadorMiles(separadorMiles(SaldosCuentaCorriente[1].Saldo.toFixed(2))));
    $("#spanSaldoPAGADORA").text(separadorMiles(separadorMiles(SaldosCuentaCorriente[2].Saldo.toFixed(2))));
    $("#spanSaldoRECAUDADORA").text(separadorMiles(separadorMiles(SaldosCuentaCorriente[3].Saldo.toFixed(2))));
}

async function AgregarLineaAsiento() {
    await ValidarLineaAsiento();
    let ObjLinea = new AsientoLinea;
    ObjLinea.IdAsiento = _ObjAsiento.IdEntidad;
    ObjLinea.IdCuentaCorriente = $("#IdCboCuentaCorriente").val();
    ObjLinea.TipoDH = $('input[name="radioDH"]:checked').val();
    ObjLinea.Importe = $("#TxtImporte").val();
    _ListaAsientoLineas.push(ObjLinea);
    await LlenarGrillaAsientoLineas();
    LimpiarLineaAsiento();
}
async function ValidarLineaAsiento() {
    let ch = $('input[name="radioDH"]:checked').val();
    if (ch == undefined)
        throw ("Debe seleccionar si es Debe o Haber");
}
async function LlenarGrillaAsientoLineas() {
    let SumaD = parseFloat(0);
    let SumaH = parseFloat(0);
    //if (_ListaAsientoLineas.length > 0) {
    //    await AsientoLinea.ArmarAsiento(div, _ListaAsientoLineas, SumaD, SumaH);
    //    await MostrarImportesTotalAsiento(SumaD, SumaH);
    //}

    $("#DivGrillaLineasAsiento").html('');
    let str = '';
    if (_ListaAsientoLineas.length > 0) {
        let i = 0;
        let borrar = 1;
        while (i <= _ListaAsientoLineas.length - 1) {
            str += '<div class="row border-light border-bottom">';
            let valorLinea = parseFloat(_ListaAsientoLineas[i].Importe);
            let ObjCuentaCorriente = await _ListaAsientoLineas[i].ObjCuentaCorriente();
            let textoLinea = ObjCuentaCorriente.Nombre;
            str += '<a href="#"class="LinkBorrarLinea col-1 bg-danger text-light" data-IdRegistro="' + i + '" ><span class="icon-bin"></span></a>';
            switch (parseInt(_ListaAsientoLineas[i].TipoDH)) {
                case 0:
                    str += '<div class="col-7 text-light pl-3"><h5>' + textoLinea + '</h5></div>';
                    str += '<div class="col-4 text-right text-light pr-1">' + separadorMiles(valorLinea.toFixed(2)) + '</div>';
                    SumaD += valorLinea;
                    break;
                case 1:
                    str += '<div class="col-1 text-light text-center"><h5> A </h5></div>';
                    str += '<div class="col-6 text-light pl-4"><h5>' + textoLinea + '</h5></div>';
                    str += '<div class="col-4 text-right text-light pr-1">' + separadorMiles(valorLinea.toFixed(2)) + '</div>';
                    SumaH += valorLinea;
                    break;
                default:
            }
            i++;
            borrar++;
            str += '</div>';
        }
    }
    await MostrarImportesTotalAsiento(SumaD, SumaH);
    $("#DivGrillaLineasAsiento").html(str);

}
async function MostrarImportesTotalAsiento(ImporteD, ImporteH) {
    let SumaD = parseFloat(0);
    let SumaH = parseFloat(0);
    if (_ListaAsientoLineas.length > 0) {
        let i = 0;
        while (i <= _ListaAsientoLineas.length - 1) {
            let valorLinea = parseFloat(_ListaAsientoLineas[i].Importe);
            switch (parseInt(_ListaAsientoLineas[i].TipoDH)) {
                case 0:
                    SumaD += valorLinea;
                    break;
                case 1:
                    SumaH += valorLinea;
                    break;
                default:
            }
            i++;
        }
    }
    $("#LblImporteTotalAsientoD").text(separadorMiles(ImporteD.toFixed(2)));
    $("#LblImporteTotalAsientoH").text(separadorMiles(ImporteH.toFixed(2)));
}

$('body').on('click', '#BtnAgregarLinea', async function (e) {
    try {
        if (_ObjAsiento == undefined) {
            _ObjAsiento = new Asiento;
            _ListaAsientoLineas = new Array;
        }
        spinner();
        await AgregarLineaAsiento();
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', ".LinkBorrarLinea", async function () {
    try {
        $this = $(this);
        let posicionEliminar = parseInt($this.attr("data-IdRegistro"));
        let LineasNuevas = new Array;
        let ind = 0;
        spinner();
        while (ind <= _ListaAsientoLineas.length - 1) {
            if (ind != posicionEliminar) {
                LineasNuevas.push(_ListaAsientoLineas[ind]);
                console.log(LineasNuevas);
            }
            ind++;
        }
        spinnerClose();
        _ListaAsientoLineas = [];
        _ListaAsientoLineas = LineasNuevas;
        await LlenarGrillaAsientoLineas();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#BtnGuardarAsiento', async function (e) {
    try {
        let Debe = $("#LblImporteTotalAsientoD").text();
        let Haber = $("#LblImporteTotalAsientoH").text();
        if (_ListaAsientoLineas ?.length == 0)
            throw ('El DAsiento debe contener lineas');

        if (Debe ?.length == 0 || Haber ?.length == 0) {
            throw ('El Debe y el Haber deben ser mayores a 0');
        }
        if (parseFloat(Debe) != parseFloat(Haber)) {
            throw ('El Debe y el Haber deben ser iguales');
        }
        let Fecha = dateStringToLong($("#TxtFecha").val());
        if (Fecha.length == 0)
            throw ('Debe ingresar la fecha');

        spinner();
        let _ObjAsiento = new Asiento;
        _ObjAsiento.Importe = parseFloat(Debe);

        _ObjAsiento.Fecha = dateStringToLong($("#TxtFecha").val());
        await _ObjAsiento.Alta(_ListaAsientoLineas);
        await LlenarSaldosCuentas();
        LimpiarAsiento();
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
