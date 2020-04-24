var _Lista_TipoPago;

class TipoPago extends DBE {
    constructor() {
        super();
        this.IdEntidad = 0;
        this.Nombre = '';
        this.Observaciones = '';
    }

    // ABM
    async Alta() {
        await this.ValidarCamposTipoPago();
        this.Nombre = this.Nombre.toUpperCase();
        this.Observaciones = this.Observaciones.toUpperCase();
        try {
            let ObjU = JSON.parse(sessionStorage.getItem("User"));
            this.IdUsuarioAlta = ObjU.IdEntidad;
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsTipoPago + "/Alta", data);
            if (id !== undefined)
                this.IdEntidad = id;
            _Lista_TipoPago.push(this);
            return;
        } catch (e) {
            throw e;
        }
    }
    async Modifica() {
        await this.ValidarCamposTipoPago();
        this.Nombre = this.Nombre.toUpperCase();
        this.Observaciones = this.Observaciones.toUpperCase();
        try {
            let ObjU = JSON.parse(sessionStorage.getItem("User"));
            this.IdUsuarioModifica = ObjU.IdEntidad;
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsTipoPago + "/Modifica", data);
            if (id !== undefined)
                this.IdEntidad = id;
            let buscados = $.grep(_Lista_TipoPago, function (entidad, index) {
                return entidad.IdEntidad !== id;
            });
            _Lista_TipoPago = buscados;
            this.IdEstado = 0;
            _Lista_TipoPago.push(this);
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
            let id = await ejecutarAsync(urlWsTipoPago + "/Baja", data);
            if (id !== undefined)
                this.IdEntidad = id;
            let buscados = $.grep(_Lista_TipoPago, function (entidad, index) {
                return entidad.IdEntidad !== id;
            });
            _Lista_TipoPago = buscados;
            this.IdEstado = 1;
            _Lista_TipoPago.push(this);
            return;
        } catch (e) {
            throw e;
        }
    }

    async ValidarCamposTipoPago() {
        let sError = '';
        if (this.Nombre.length === 0) {
            sError += 'Debe ingresar el Nombre';
        }
        if (sError !== '') {
            throw '<b> Error de grabación </b> <br/><br/>' + sError;
        }
    }

    // Todos
    static async Todos() {
        if (_Lista_TipoPago === undefined) {
            _Lista_TipoPago = await TipoPago.TraerTodas();
        }
        return _Lista_TipoPago;
    }

    // Traer
    static async TraerUno(IdEntidad) {
        _Lista_TipoPago = await TipoPago.TraerTodos();
        let buscado = $.grep(_Lista_TipoPago, function (entidad, index) {
            return entidad.IdEntidad === IdEntidad;
        });
        let Encontrado = buscado[0];
        return Encontrado;
    }
    static async TraerTodos() {
        return await TipoPago.Todos();
    }
    static async TraerTodosActivos() {
        _Lista_TipoPago = await TipoPago.TraerTodos();
        let buscado = $.grep(_Lista_TipoPago, function (entidad, index) {
            return entidad.IdEstado === 0;
        });
        return buscado;
    }
    static async TraerTodas() {
        let lista = await ejecutarAsync(urlWsTipoPago + "/TraerTodos");
        _Lista_TipoPago = [];
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadTipoPago(value));
            });
        }
        _Lista_TipoPago = result;
        return _Lista_TipoPago;
    }
    // Otros
    static async Refresh() {
        _Lista_TipoPago = await TipoPago.TraerTodas();
    }
    // Herramientas
    static async ArmarGrilla(lista, div, eventoSeleccion, eventoEliminar, estilo) {
        $('#' + div + '').html('');
        let str = '';
        lista.sort(SortXNombre);
        if (lista.length > 0) {
            str += '<div style="' + estilo + '">';
            str += '    <ul class="ListaGrilla">';
            let estiloItem = '';
            for (let item of lista) {
                estiloItem = 'LinkListaGrillaObjeto';
                if (item.IdEstado === 1) {
                    estiloItem = 'LinkListaGrillaObjetoEliminado';
                }
                let aItem = '<a href="#" class="mibtn-seleccionTipoPago" data-Evento="' + eventoSeleccion + '" data-Id="' + item.IdEntidad + '">' + item.Nombre + '</a>';
                let aEliminar = '<a href="#" class="mibtn-EliminarTipoPago" data-Evento="' + eventoEliminar + '" data-Id="' + item.IdEntidad + '"><span class="icon-bin"></span></a>';
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
    //static async ArmarCombo(lista, div, selector, evento, ventana, Cbo) {
    //    let cbo = "";
    //    cbo += '<div id="' + Cbo + '" class="dropdown">';
    //    cbo += '    <button id="' + selector + '" class="btn btn-primary dropdown-toggle btn-md btn-block" type="button" data-toggle="dropdown">' + ventana;
    //    cbo += '        <span class="caret"></span>';
    //    cbo += '    </button>';
    //    cbo += '<ul class="dropdown-menu">';
    //    $(lista).each(function () {
    //        cbo += '<li><a href="#" class="mibtn-seleccionTipoPago" data-Id="' + this.IdEntidad + '" data-Nombre="' + this.Nombre + '" data-Evento="' + evento + '" > ' + this.Nombre + '</a></li>';
    //    });
    //    cbo += '</ul>';
    //    cbo += '</div>';
    //    return $('#' + div + '').html(cbo);
    //}
    static async ArmarCombo(lista, div, selector, evento, ventana, estilo) {
        lista.sort(SortXNombre);
        let Cbo = '';
        Cbo += '<select id="_' + div + '" onchange="SeleccionTipoPago()"  data-Evento="' + evento + '" name="myselect" class="' + estilo + '">';
        Cbo += '    <option value="0" id="' + selector + '">' + ventana + '</option>';
        $(lista).each(function () {
            Cbo += '<option class="mibtn-seleccionTipoPago" value="' + this.IdEntidad + '" data-Id="' + this.IdEntidad + '" data-Evento="' + evento + '">' + this.Nombre + '</option>';
        });
        Cbo += '</select>';
        return $('#' + div + '').html(Cbo);
    }
}
function LlenarEntidadTipoPago(entidad) {
    let Res = new TipoPago;
    Res.IdUsuarioAlta = entidad.IdUsuarioAlta;
    Res.IdUsuarioBaja = entidad.IdUsuarioBaja;
    Res.IdUsuarioModifica = entidad.IdUsuarioModifica;
    Res.FechaAlta = entidad.FechaAlta;
    Res.FechaBaja = entidad.FechaBaja;
    Res.IdMotivoBaja = entidad.IdMotivoBaja;
    Res.IdEntidad = entidad.IdEntidad;
    Res.Nombre = entidad.Nombre;
    Res.Observaciones = entidad.Observaciones;
    Res.IdEstado = entidad.IdEstado;
    return Res;
}
$('body').on('click', ".mibtn-seleccionTipoPago", async function () {
    try {
        $this = $(this);
        let buscado = $.grep(_Lista_TipoPago, function (entidad, index) {
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
$('body').on('click', ".mibtn-EliminarTipoPago", async function () {
    try {
        $this = $(this);
        let buscado = $.grep(_Lista_TipoPago, function (entidad, index) {
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
async function SeleccionTipoPago() {
    try {
        let elemento = document.getElementById("_CboTipoPago");
        let buscado = $.grep(_Lista_TipoPago, function (entidad, index) {
            return entidad.IdEntidad == elemento.options[elemento.selectedIndex].value;
        });
        let Seleccionado = buscado[0];
        let evento = elemento.getAttribute('data-Evento');
        let event = new CustomEvent(evento, { detail: Seleccionado });
        document.dispatchEvent(event);
    } catch (e) {
        alertAlerta(e);
    }
}