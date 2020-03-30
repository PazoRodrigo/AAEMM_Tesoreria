var _Lista_Gasto;

class Gasto extends DBE {
    constructor() {
        super();
        this.IdEntidad = 0;
        this.Importe = 0;
        this.CantidadComprobantes = 0;
        this.Estado = '';
        this.Observaciones = '';

        this._ListaComprobantes
    }

    async ListaComprobante() {
        try {
            if (this.ListaComprobante === undefined) {
                this.ListaComprobante = await Comprobante.TraerTodasXGasto(this.IdEntidad);
            }
            return this.ListaComprobante;
        } catch (e) {
            return new Comprobante;
        }       
    }
    // ABM
    async Alta() {
        //try {
        //    var _Lista_Gasto;
        //    _Lista_Gasto = [];
        //    let data = {
        //        'Desde': Desde,
        //        'Hasta': Hasta
        //    };
        //    let id = await ejecutarAsync(urlWsGasto + "/Alta", data);
        //    if (id !== undefined)
        //        this.IdEntidad = id;
        //    _Lista_Gasto.push(this);
        //    Gasto.Refresh();
        //    return;
        //} catch (e) {
        //    throw e;
        //}
    }
    async Modifica() {
        await this.ValidarCamposGasto();
        this.Nombre = this.Nombre.toUpperCase();
        this.Observaciones = this.Observaciones.toUpperCase();
        try {
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsGasto + "/Modifica", data);
            if (id !== undefined)
                this.IdEntidad = id;
            let buscados = $.grep(_Lista_Gasto, function (entidad, index) {
                return entidad.IdEntidad !== id;
            });
            _Lista_Gasto = buscados;
            this.IdEstado = 0;
            _Lista_Gasto.push(this);
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
            let id = await ejecutarAsync(urlWsGasto + "/Baja", data);
            if (id !== undefined)
                this.IdEntidad = id;
            let buscados = $.grep(_Lista_Gasto, function (entidad, index) {
                return entidad.IdEntidad !== id;
            });
            _Lista_Gasto = buscados;
            this.IdEstado = 1;
            _Lista_Gasto.push(this);
            return;
        } catch (e) {
            throw e;
        }
    }

    // Todos
    static async Todos() {
        if (_Lista_Gasto === undefined) {
            _Lista_Gasto = await Gasto.TraerTodas();
        }
        return _Lista_Gasto;
    }

    // Traer
    static async TraerUno(IdEntidad) {
        _Lista_Gasto = await Gasto.TraerTodos();
        let buscado = $.grep(_Lista_Gasto, function (entidad, index) {
            return entidad.IdEntidad === IdEntidad;
        });
        let Encontrado = buscado[0];
        return Encontrado;
    }
    static async TraerTodos() {
        return await Gasto.Todos();
    }
    static async TraerGastoAbierto() {
        let lista = await ejecutarAsync(urlWsGasto + "/TraerGastoAbierto");
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadGasto(value));
            });
        }
        return result[0];
    }
    static async TraerTodosActivos() {
        _Lista_Gasto = await Gasto.TraerTodos();
        let buscado = $.grep(_Lista_Gasto, function (entidad, index) {
            return entidad.IdEstado === 0;
        });
        return buscado;
    }
    static async TraerTodas() {
        let lista = await ejecutarAsync(urlWsGasto + "/TraerTodos");
        _Lista_Gasto = [];
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadGasto(value));
            });
        }
        _Lista_Gasto = result;
        return _Lista_Gasto;
    }
    // Otros
    static async Refresh() {
        _Lista_Gasto = await Gasto.TraerTodas();
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
                let aItem = '<a href="#" class="mibtn-seleccionGasto" data-Evento="' + eventoSeleccion + '" data-Id="' + item.IdEntidad + '">' + item.IdEntidad + '  ' + LongToDateString(item.FechaAlta) + ' </a>';
                let aEliminar = '<a href="#" class="mibtn-EliminarGasto" data-Evento="' + eventoEliminar + '" data-Id="' + item.IdEntidad + '"><span class="icon-bin"></span></a>';
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
function LlenarEntidadGasto(entidad) {
    let Res = new Gasto;
    Res.IdUsuarioAlta = entidad.IdUsuarioAlta;
    Res.IdUsuarioBaja = entidad.IdUsuarioBaja;
    Res.IdUsuarioModifica = entidad.IdUsuarioModifica;
    Res.FechaAlta = entidad.FechaAlta;
    Res.FechaBaja = entidad.FechaBaja;
    Res.IdMotivoBaja = entidad.IdMotivoBaja;
    Res.IdEntidad = entidad.IdEntidad;
    Res.Observaciones = entidad.Observaciones;
    Res.IdEstado = entidad.IdEstado;
    Res.Estado = entidad.Estado;
    return Res;
}
$('body').on('click', ".mibtn-seleccionGasto", async function () {
    try {
        $this = $(this);
        let buscado = $.grep(_Lista_Gasto, function (entidad, index) {
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
$('body').on('click', ".mibtn-EliminarGasto", async function () {
    try {
        $this = $(this);
        let buscado = $.grep(_Lista_Gasto, function (entidad, index) {
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
