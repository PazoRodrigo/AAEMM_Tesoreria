var _Lista_Comprobante;

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
        this.Estado = '';
    }

    // ABM
    async Alta() {
        await this.ValidarCampos();

        this.Observaciones = this.Observaciones.toUpperCase();

        console.log(this);
        try {
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
        //if (this.Fec) {

        //}
    }
    // Todos
    static async Todos() {
        if (_Lista_Comprobante === undefined) {
            _Lista_Comprobante = await Comprobante.TraerTodas();
        }
        return _Lista_Comprobante;
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
    static async Refresh() {
        _Lista_Comprobante = await Comprobante.TraerTodas();
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
                let aItem = '<a href="#" class="mibtn-seleccionComprobante" data-Evento="' + eventoSeleccion + '" data-Id="' + item.IdEntidad + '">' + item.IdEntidad + '  ' + LongToDateString(item.FechaAlta) + ' </a>';
                let aEliminar = '<a href="#" class="mibtn-EliminarComprobante" data-Evento="' + eventoEliminar + '" data-Id="' + item.IdEntidad + '"><span class="icon-bin"></span></a>';
                str += String.format('<li><div class="LinkListaGrilla ' + estiloItem + '">{0}</div><div class="LinkListaGrilla LinkListaGrillaElimina">{1}</div></li>', aItem, aEliminar);
            }
            str += '    </ul>';
            str += '</div>';
        }
        return $('#' + div + '').html(str);
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
    console.log(entidad);
    let Res = new Comprobante;
    Res.IdUsuarioAlta = entidad.IdUsuarioAlta;
    Res.IdUsuarioBaja = entidad.IdUsuarioBaja;
    Res.IdUsuarioModifica = entidad.IdUsuarioModifica;
    Res.FechaAlta = entidad.FechaAlta;
    Res.FechaBaja = entidad.FechaBaja;
    Res.IdMotivoBaja = entidad.IdMotivoBaja;
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
    Res.IdEstado = entidad.IdEstado;
    Res.Estado = entidad.Estado;
    console.log(Res);
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
        let event = new CustomEvent(evento, { detail: Seleccionado });
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
        let event = new CustomEvent(evento, { detail: Seleccionado });
        document.dispatchEvent(event);
    } catch (e) {
        alertAlerta(e);
    }
});