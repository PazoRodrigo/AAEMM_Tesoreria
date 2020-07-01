var _Lista_Comprobante;

class StrBusquedaComprobante {
    constructor() {
        this.Desde = 0;
        this.Hasta = 0;
        this.Estados = '';
        this.IdGasto = 0;
        this.IdOriginarioGasto = 0;
        this.IdProveedor = 0;
        this.IdCentroCosto = 0;
        this.IdTipoPago = 0;
        this.IdCuenta = 0;
        this.Importe = 0;
        this.NroComprobante = 0;
    }
}
class Comprobante extends DBE {
    constructor() {
        super();
        this.IdEntidad = 0;
        this.IdGasto = 0;
        this.IdOriginario = 0;
        this.IdProveedor = 0;
        this.IdCentroCosto = 0;
        this.IdCuenta = 0;
        this.IdTipoPago = 0;
        this.FechaGasto = 0;
        this.FechaPago = 0;
        this.NroComprobante = 0;
        this.Importe = 0;
        this.CantidadComprobantes = 0;
        this.Observaciones = '';
        this.IdEstado = 0;
        this.Estado = '';

        this._OriginarioGasto;
        this._Proveedor;
        this._CentroCosto;
    }

    // Lazy
    async ObjOriginarioGasto() {
        try {
            if (this._OriginarioGasto === undefined) {
                this._ObjOriginarioGasto = await OriginarioGasto.TraerUno(this.IdOriginario);
            }
            return this._ObjOriginarioGasto;
        } catch (e) {
            return new OriginarioGasto;
        }
    }
    async ObjProveedor() {
        try {
            if (this._Proveedor === undefined) {
                this._ObjProveedor = await Proveedor.TraerUno(this.IdProveedor);
            }
            return this._ObjProveedor;
        } catch (e) {
            return new Proveedor;
        }
    }
    async ObjCentroCosto() {
        try {
            if (this._CentroCosto === undefined) {
                this._ObjCentroCosto = await CentroCosto.TraerUno(this.IdCentroCosto);
            }
            return this._ObjCentroCosto;
        } catch (e) {
            return new CentroCosto;
        }
    }
    async ObjCuentaContable() {
        try {
            if (this._CuentaContable === undefined) {
                this._ObjCuentaContable = await CuentaContable.TraerUno(this.IdCuenta);
            }
            return this._ObjCuentaContable;
        } catch (e) {
            return new CuentaContable;
        }
    }
    async ObjTipoPago() {
        try {
            if (this._TipoPago === undefined) {
                if (this.IdTipoPago > 0) {
                    this._ObjTipoPago = await TipoPago.TraerUno(this.IdTipoPago);
                } else {
                    this._ObjTipoPago = new TipoPago;
                }
            }
            return this._ObjTipoPago;
        } catch (e) {
            return new TipoPago;
        }
    }

    StrPagado() {
        let Result = 'No';
        if (this.FechaPago > 0) {
            Result = 'Si';
        }
        return Result;

    }

    // ABM
    async Alta() {
        await this.ValidarCampos();
        this.Observaciones = this.Observaciones.toUpperCase();
        try {
            let ObjU = JSON.parse(sessionStorage.getItem("User"));
            this.IdUsuarioAlta = ObjU.IdEntidad;
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsComprobante + "/Alta", data);
            if (id !== undefined)
                this.IdEntidad = id;
            _Lista_Comprobante.push(this);
            return;
        } catch (e) {
            throw e;
        }
    }
    async Modifica() {
        await this.ValidarCampos();
        this.Observaciones = this.Observaciones.toUpperCase();
        try {
            let ObjU = JSON.parse(sessionStorage.getItem("User"));
            this.IdUsuarioModifica = ObjU.IdEntidad;
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsComprobante + "/Modifica", data);
            if (id !== undefined)
                this.IdEntidad = id;
            let buscados = $.grep(_Lista_Comprobante, function (entidad, index) {
                return entidad.IdEntidad !== id;
            });
            _Lista_Comprobante = buscados;
            this.IdEstado = 0;
            _Lista_Comprobante.push(this);
            return;
        } catch (e) {
            throw e;
        }
    }
    async Baja() {
        try {
            let ObjU = JSON.parse(sessionStorage.getItem("User"));
            this.IdUsuarioBaja = ObjU.IdEntidad;
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsComprobante + "/Baja", data);
            if (id !== undefined)
                this.IdEntidad = id;
            let buscados = $.grep(_Lista_Comprobante, function (entidad, index) {
                return entidad.IdEntidad !== id;
            });
            _Lista_Comprobante = buscados;
            this.IdEstado = 1;
            _Lista_Comprobante.push(this);
            return;
        } catch (e) {
            throw e;
        }
    }

    async ValidarCampos() {
        let sError = '';
        if (this.IdCuenta === 0) {
            sError += '- Debe informar la Cuenta. <br>';
        }
        if (this.Importe.length == 0) {
            sError += '- Debe informar el importe. <br>';
        } else {
            if (isNaN(this.Importe)) {
                sError += '- El importe debe ser numérico. <br>';
            }
        }
        if (this.IdOriginario === 0) {
            sError += '- Debe informar el Originario. <br>';
        }
        if (this.IdCentroCosto === 0) {
            sError += '- Debe informar el Centro de Costo. <br>';
        }
        if (this.IdProveedor === 0) {
            sError += '- Debe informar el Proveedor. <br>';
        }
        if (this.NroComprobante.length == 0) {
            this.NroComprobante = 0;
        } else {
            if (isNaN(this.NroComprobante)) {
                sError += '- El Nro. Comprobante debe ser numérico. <br>';
            }
        }
        if (sError.length > 0) {
            throw '<b>Debe Completar todos los campos</b><br><br>' + sError;
        }
    }
    // Todos
    static async TodosXGasto(IdGasto) {
        return await Comprobante.TraerTodasXGasto(IdGasto);
    }

    // Traer
    static async TraerUno(IdEntidad) {
        _Lista_Comprobante = await Comprobante.TraerTodos();
        let buscado = $.grep(_Lista_Comprobante, function (entidad, index) {
            return entidad.IdEntidad === IdEntidad;
        });
        let Encontrado = buscado[0];
        return Encontrado;
    }
    static async TraerTodos() {
        return await Comprobante.Todos();
    }
    static async TraerTodosActivos() {
        _Lista_Comprobante = await Comprobante.TraerTodos();
        let buscado = $.grep(_Lista_Comprobante, function (entidad, index) {
            return entidad.IdEstado === 0;
        });
        return buscado;
    }
    static async TraerTodas() {
        let lista = await ejecutarAsync(urlWsComprobante + "/TraerTodos");
        _Lista_Comprobante = [];
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadComprobante(value));
            });
        }
        _Lista_Comprobante = result;
        return _Lista_Comprobante;
    }
    static async TraerTodasXGasto(IdGasto) {
        let data = {
            "IdGasto": IdGasto
        };
        let lista = await ejecutarAsync(urlWsComprobante + "/TraerTodosXGasto", data);
        _Lista_Comprobante = [];
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadComprobante(value));
            });
        }
        _Lista_Comprobante = result;
        return _Lista_Comprobante;
    }
    // Otros
    static async TraerTodosXBusqueda(Busqueda) {
        let data = {
            'Busqueda': Busqueda
        };
        let lista = await ejecutarAsync(urlWsComprobante + "/TraerTodosXBusqueda", data);
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadComprobante(value));
            });
        }
        _ListaIngresos = result;
        return result;
    }
    // Herramientas
    static async ArmarGrilla(lista, div, eventoSeleccion, eventoEliminar, estilo) {
        $('#' + div + '').html('');
        let str = '';
        if (lista.length > 0) {
            str += '<div style="' + estilo + '">';
            str += '    <ul class="ListaGrilla">';
            let estiloItem = '';
            for (let item of lista) {
                estiloItem = 'LinkListaGrillaObjeto';
                if (item.IdEstado === 1) {
                    estiloItem = 'LinkListaGrillaObjetoEliminado';
                }
                let aItem = '<a href="#" class="mibtn-seleccionComprobante" data-Evento="' + eventoSeleccion + '" data-Id="' + item.IdEntidad + '">' + item.IdEntidad + '  ' + LongToDateString(item.FechaGasto) + ' ' + item.Estado + ' - ' + separadorMiles(item.Importe.toFixed(2)) + ' </a>';
                let aEliminar = '<a href="#" class="mibtn-EliminarComprobante" data-Evento="' + eventoEliminar + '" data-Id="' + item.IdEntidad + '"><span class="icon-bin"></span></a>';
                str += String.format('<li><div class="LinkListaGrilla ' + estiloItem + '">{0}</div><div class="LinkListaGrilla LinkListaGrillaElimina">{1}</div></li>', aItem, aEliminar);
            }
            str += '    </ul>';
            str += '</div>';
        }
        return $('#' + div + '').html(str);
    }



    static async ArmarGrillaCabecera(div) {
        $("#" + div + "").html('');
        let str = "";
        str += '<table class="table table-sm">';
        str += '    <thead>';
        str += '        <tr>';
        str += '            <th class="text-center" style="width: 55px;">Id</th>';
        str += '            <th class="text-center" style="width: 110px;">Gasto</th>';
        str += '            <th class="text-center" style="width: 55px;">Pago</th>';
        str += '            <th class="text-center" style="width: 180px;">Tipo Pago</th>';
        str += '            <th class="text-center" style="width: 195px;">Proveedor</th>';
        str += '            <th class="text-center" style="width: 105px;">Importe</th>';
        // str += '            <th class="text-center" style="width: 90px;">Baja</th>';
        str += '        </tr>';
        str += '    </thead>';
        str += '</table >';
        return $("#" + div + "").html(str);
    }

    static async ArmarGrillaDetalle(div, lista, evento, estilo) {
        $("#" + div + "").html('');
        let str = "";
        str += '<div style="' + estilo + '">';
        str += '<table class="table table-sm table-striped table-hover">';
        str += '    <tbody>';
        if (lista.length > 0) {
            for (let item of lista) {
                let ColorClase = 'text-light';
                if (item.FechaBaja > 0) {
                    ColorClase = 'text-danger bg-light';
                }
                str += '        <tr>';
                str += '            <td class="text-center" style="width: 55px;"><div class="small text-light">' + item.IdGasto + '</div></td>';
                str += '            <td class="text-center" style="width: 110px;"><div class="small text-light">' + LongToDateString(item.FechaGasto) + '</div></td>';
                str += '            <td class="text-center" style="width: 75px;"><div class="small text-light">' + item.StrPagado() + '</div></td>';
                str += '            <td class="text-left pl-1" style="width: 180px;"><div class="small text-light">' + Left((await item.ObjTipoPago()).Nombre, 22) + '</div></td>';
                str += '            <td class="text-left pl-1" style="width: 200px;"><div class="small text-light">' + Left((await item.ObjProveedor()).Nombre, 25) + '</div></td>';
                str += '            <td class="text-right pr-2" style="width: 105px;"><div class="' + ColorClase + '">' + separadorMiles(item.Importe.toFixed(2)) + '</div></td>';
                str += '        </tr>';
            }
        }
        str += '    </tbody>';
        str += '</table >';
        str += '</div >';
        return $("#" + div + "").html(str);

    }

    static async ArmarRadios(lista, div, evento, estilo) {
        $('#' + div + '').html('');
        let str = '';
        await Area.Refresh();
        if (lista.length > 0) {
            str += '<div style="' + estilo + '">';
            str += '    <table class="table table-bordered" style="width: 70%;">';
            str += '        <thead>';
            str += '            <tr>';
            str += '                <th colspan="2" style="text-align: center;">Areas</th>';
            str += '            </tr>';
            str += '        </thead>';
            str += '        <tbody>';
            for (let item of lista) {
                let radioSeleccion = '<input type="radio" class="mibtn-seleccionArea"  name="rblArea" data-Evento="' + evento + '" data-Id="' + item.IdEntidad + '" value="' + item.IdEntidad + '">';
                str += String.format('<tr><td align="center" valign="middle" style="width: 5%;">{0}</td><td align="left">{1}</td></tr>', radioSeleccion, item.Nombre);
            }
            str += '        </tbody>';
            str += '    </table>';
            str += '</div>';
        }
        return $('#' + div + '').html(str);
    }
    static async ArmarCheckBoxs(lista, div, evento, estilo) {
        $('#' + div + '').html('');
        let str = '';
        str += '<div style="' + estilo + '">';
        await Area.Refresh();
        if (lista.length > 0) {
            for (let item of lista) {
                str += '<div class="col-lg-4"><input type="checkbox" class="micbx-Area" name="CkbList_Areas" value="' + item.IdEntidad + '"    id="chk_' + item.IdEntidad + '" /><label for="chk_' + item.IdEntidad + '"> ' + item.Nombre + '</label></div>';
            }
        }
        str += '</div>';
        return $('#' + div + '').html(str);
    }
}

function LlenarEntidadComprobante(entidad) {
    let Res = new Comprobante;
    Res.IdEntidad = entidad.IdEntidad;
    Res.IdGasto = entidad.IdGasto;
    Res.IdOriginario = entidad.IdOriginario;
    Res.IdProveedor = entidad.IdProveedor;
    Res.IdCentroCosto = entidad.IdCentroCosto;
    Res.IdCuenta = entidad.IdCuenta;
    Res.IdTipoPago = entidad.IdTipoPago;
    Res.FechaGasto = entidad.FechaGasto;
    Res.FechaPago = entidad.FechaPago;
    Res.NroComprobante = entidad.NroComprobante;
    Res.Importe = entidad.Importe;
    Res.Observaciones = entidad.Observaciones;
    if (entidad.FechaBaja > 0) {
        Res.IdEstado = 1;
    }
    Res.IdUsuarioAlta = entidad.IdUsuarioAlta;
    Res.IdUsuarioBaja = entidad.IdUsuarioBaja;
    Res.IdUsuarioModifica = entidad.IdUsuarioModifica;
    Res.FechaAlta = entidad.FechaAlta;
    Res.FechaBaja = entidad.FechaBaja;
    Res.IdMotivoBaja = entidad.IdMotivoBaja;
    return Res;
}
$('body').on('click', ".mibtn-seleccionComprobante", async function () {
    try {
        $this = $(this);
        let buscado = $.grep(_Lista_Comprobante, function (entidad, index) {
            return entidad.IdEntidad === parseInt($this.attr("data-Id"));
        });
        let Seleccionado = buscado[0];
        let evento = $this.attr("data-Evento");
        let event = new CustomEvent(evento, {
            detail: Seleccionado
        });
        document.dispatchEvent(event);
    } catch (e) {
        alertAlerta(e);
    }
});
$('body').on('click', ".mibtn-EliminarComprobante", async function () {
    try {
        $this = $(this);
        let buscado = $.grep(_Lista_Comprobante, function (entidad, index) {
            return entidad.IdEntidad === parseInt($this.attr("data-Id"));
        });
        let Seleccionado = buscado[0];
        let evento = $this.attr("data-Evento");
        let event = new CustomEvent(evento, {
            detail: Seleccionado
        });
        document.dispatchEvent(event);
    } catch (e) {
        alertAlerta(e);
    }
});