var _Lista_Gasto;

class StrBusquedaGasto {
    constructor() {
        this.Desde = 0;
        this.Hasta = 0;
        this.Estados = '';
    }
}
class Gasto extends DBE {
    constructor() {
        super();
        this.IdEntidad = 0;
        this.Importe = 0;
        this.FechaGasto = 0;
        this.CantidadComprobantes = 0;
        this.Estado = '';
        this.Observaciones = '';

        this._ListaComprobantes;
    }
    StrPeriodo() {
        let Result = '';
        if (this.FechaGasto > 0) {
            Result = Right(Left(this.FechaGasto, 6), 2) + '/' + Left(this.FechaGasto, 4);
        }
        return Result;
    }
    async ListaComprobantes() {
            try {
                return await Comprobante.TraerTodasXGasto(this.IdEntidad);

            } catch (e) {
                return new Comprobante;

            }
        }
        // ABM
    async Alta() {
        try {
            let ObjU = JSON.parse(sessionStorage.getItem("User"));
            this.IdUsuarioAlta = ObjU.IdEntidad;
            //this.IdUsuarioAlta = 1;
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsGasto + "/Alta", data);
            if (id !== undefined)
                this.IdEntidad = id;
            _Lista_Gasto.push(this);
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
            let id = await ejecutarAsync(urlWsGasto + "/Baja", data);
            if (id !== undefined)
                this.IdEntidad = id;
            let buscados = $.grep(_Lista_Gasto, function(entidad, index) {
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
    async Cerrar() {
        try {
            let ObjU = JSON.parse(sessionStorage.getItem("User"));
            this.IdUsuarioModifica = ObjU.IdEntidad;
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsGasto + "/Cerrar", data);
            if (id !== undefined)
                this.IdEntidad = id;
            let buscados = $.grep(_Lista_Gasto, function(entidad, index) {
                return entidad.IdEntidad !== id;
            });
            _Lista_Gasto = buscados;
            this.IdEstado = 2;
            _Lista_Gasto.push(this);
            return;
        } catch (e) {
            throw e;
        }
    }

    // Todos
    //static async Todos() {
    //    if (_Lista_Gasto === undefined) {
    //        _Lista_Gasto = await Gasto.TraerTodas();
    //    }
    //    return _Lista_Gasto;
    //}

    // Traer
    static async TraerUno(IdEntidad) {
        _Lista_Gasto = await Gasto.TraerTodas();
        let buscado = $.grep(_Lista_Gasto, function(entidad, index) {
            return entidad.IdEntidad === IdEntidad;
        });
        let Encontrado = buscado[0];
        return Encontrado;
    }
    static async TraerTodos() {
        return await Gasto.TraerTodas();
    }
    static async TraerGastosAbiertos() {
        let lista = await ejecutarAsync(urlWsGasto + "/TraerGastosAbiertos");
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function(key, value) {
                result.push(LlenarEntidadGasto(value));
            });
        }
        return result;
    }
    static async TraerTodosActivos() {
        _Lista_Gasto = await Gasto.TraerTodas();
        let buscado = $.grep(_Lista_Gasto, function(entidad, index) {
            return entidad.IdEstado === 0;
        });
        return buscado;
    }
    static async TraerTodas() {
        let lista = await ejecutarAsync(urlWsGasto + "/TraerTodos");
        _Lista_Gasto = [];
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function(key, value) {
                result.push(LlenarEntidadGasto(value));
            });
        }
        _Lista_Gasto = result;
        return _Lista_Gasto;
    }
    static async TraerTodosUltimos5() {
        let lista = await ejecutarAsync(urlWsGasto + "/TraerTodosUltimos5");
        _Lista_Gasto = [];
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function(key, value) {
                result.push(LlenarEntidadGasto(value));
            });
        }
        _Lista_Gasto = result;
        return _Lista_Gasto;
    }
    static async TraerTodosXBusqueda(Busqueda) {
        let data = {
            'Busqueda': Busqueda
        };
        let lista = await ejecutarAsync(urlWsGasto + "/TraerTodosXBusqueda", data);
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function(key, value) {
                result.push(LlenarEntidadGasto(value));
            });
        }
        _ListaIngresos = result;
        return result;
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
                if (item.IdEstado === 11) {
                    estiloItem = 'LinkListaGrillaObjetoEliminado';
                }
                let periodo = Right(Left(item.FechaGasto, 6), 2) + '/' + Left(item.FechaGasto, 4);
                let aItem = '<a href="#" class="mibtn-seleccionGasto" data-Evento="' + eventoSeleccion + '" data-Id="' + item.IdEntidad + '">' + item.IdEntidad + '  ' + periodo + ' - ' + item.Estado + ' - ' + MonedaDecimales2(item.Importe) + ' </a>';
                let aEliminar = '<a href="#" class="mibtn-EliminarGasto" data-Evento="' + eventoEliminar + '" data-Id="' + item.IdEntidad + '"><span class="icon-bin"></span></a>';
                str += String.format('<li><div class="LinkListaGrilla ' + estiloItem + '">{0}</div > <div class="LinkListaGrilla LinkListaGrillaElimina">{1}</div></li > ', aItem, aEliminar);
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
        str += '            <th class="text-center" style="width: 110px;">Período</th>';
        str += '            <th class="text-center" style="width: 85px;">Estado</th>';
        str += '            <th class="text-center" style="width: 120px;">Cant. Comprob.</th>';
        str += '            <th class="text-center" style="width: 135px;">Importe</th>';
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
                str += '        <tr>';
                str += '            <td class="text-center" style="width: 55px;"><div class="small text-light">' + item.IdEntidad + '</div></td>';
                str += '            <td class="text-center" style="width: 110px;"><div class="small text-light">' + item.StrPeriodo() + '</div></td>';
                str += '            <td class="text-center" style="width: 85px;"><div class="small text-light">' + item.Estado + '</div></td>';
                str += '            <td class="text-right pr-2" style="width: 120px;"><div class="small text-light">' + item.CantidadComprobantes + '</div></td>';
                str += '            <td class="text-right pr-2" style="width: 135px;"><div class="small text-light">' + MonedaDecimales2(item.Importe) + '</div></td>';
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
    static async ArmarCombo(lista, div, selector, evento, ventana, estilo) {
        let Cbo = '';
        Cbo += '<select id="_' + div + '" data-Evento="' + evento + '" class="' + estilo + '">';
        Cbo += '    <option value="0" id="' + selector + '">' + ventana + '</option>';
        $(lista).each(function() {
            let periodo = Right(Left(this.FechaGasto, 6), 2) + '/' + Left(this.FechaGasto, 4);
            Cbo += '<option class="mibtn-seleccionGasto" value="' + this.IdEntidad + '" data-Id="' + this.IdEntidad + '" data-Evento="' + evento + '">' + periodo + '</option>';
        });
        Cbo += '</select>';
        return $('#' + div + '').html(Cbo);
    }
}

function LlenarEntidadGasto(entidad) {
    let Res = new Gasto;
    Res.IdEntidad = entidad.IdEntidad;
    Res.Observaciones = entidad.Observaciones;
    Res.Importe = entidad.Importe;
    Res.FechaGasto = entidad.FechaGasto;
    Res.CantidadComprobantes = entidad.CantidadComprobantes;
    Res.IdEstado = entidad.IdEstado;
    Res.Estado = entidad.Estado;
    Res.IdUsuarioAlta = entidad.IdUsuarioAlta;
    Res.IdUsuarioBaja = entidad.IdUsuarioBaja;
    Res.IdUsuarioModifica = entidad.IdUsuarioModifica;
    Res.FechaAlta = entidad.FechaAlta;
    Res.FechaBaja = entidad.FechaBaja;
    Res.IdMotivoBaja = entidad.IdMotivoBaja;
    return Res;
}
$('body').on('click', ".mibtn-seleccionGasto", async function() {
    try {
        $this = $(this);
        let buscado = $.grep(_Lista_Gasto, function(entidad, index) {
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
$('body').on('click', ".mibtn-EliminarGasto", async function() {
    try {
        $this = $(this);
        let buscado = $.grep(_Lista_Gasto, function(entidad, index) {
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