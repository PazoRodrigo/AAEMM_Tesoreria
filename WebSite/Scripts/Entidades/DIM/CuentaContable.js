var _Lista_CuentaContable;

class CuentaContable extends DBE {
    constructor() {
        super();
        this.IdEntidad = 0;
        this.Nombre = '';
        this.Observaciones = '';
        this.IdTipoCuenta = 0;
    }

    // ABM
    async Alta() {
        await this.ValidarCamposCuentaContable();
        this.Nombre = this.Nombre.toUpperCase();
        this.Observaciones = this.Observaciones.toUpperCase();
        try {
            let ObjU = JSON.parse(sessionStorage.getItem("User"));
            this.IdUsuarioAlta = ObjU.IdEntidad;
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsCuentaContable + "/Alta", data);
            if (id !== undefined)
                this.IdEntidad = id;
            _Lista_CuentaContable.push(this);
            return;
        } catch (e) {
            throw e;
        }
    }
    async Modifica() {
        await this.ValidarCamposCuentaContable();
        this.Nombre = this.Nombre.toUpperCase();
        this.Observaciones = this.Observaciones.toUpperCase();
        try {
            let ObjU = JSON.parse(sessionStorage.getItem("User"));
            this.IdUsuarioModifica = ObjU.IdEntidad;
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsCuentaContable + "/Modifica", data);
            if (id !== undefined)
                this.IdEntidad = id;
            let buscados = $.grep(_Lista_CuentaContable, function (entidad, index) {
                return entidad.IdEntidad !== id;
            });
            _Lista_CuentaContable = buscados;
            this.IdEstado = 0;
            _Lista_CuentaContable.push(this);
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
            let id = await ejecutarAsync(urlWsCuentaContable + "/Baja", data);
            if (id !== undefined)
                this.IdEntidad = id;
            let buscados = $.grep(_Lista_CuentaContable, function (entidad, index) {
                return entidad.IdEntidad !== id;
            });
            _Lista_CuentaContable = buscados;
            this.IdEstado = 1;
            _Lista_CuentaContable.push(this);
            return;
        } catch (e) {
            throw e;
        }
    }

    async ValidarCamposCuentaContable() {
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
        if (_Lista_CuentaContable === undefined) {
            _Lista_CuentaContable = await CuentaContable.TraerTodas();
        }
        return _Lista_CuentaContable;
    }

    // Traer
    static async TraerUno(IdEntidad) {
        _Lista_CuentaContable = await CuentaContable.TraerTodos();
        let buscado = $.grep(_Lista_CuentaContable, function (entidad, index) {
            return entidad.IdEntidad === IdEntidad;
        });
        return buscado[0];
    }
    static async TraerTodosGasto() {
        let buscado = $.grep(await CuentaContable.Todos(), function (entidad, index) {
            return entidad.IdTipoCuenta == 2;
        });
        return buscado;
    }
    static async TraerTodosResultado() {
        let buscado = $.grep(await CuentaContable.Todos(), function (entidad, index) {
            return entidad.IdTipoCuenta == 3;
        });
        return buscado;
    }
    static async TraerTodosAnticipo() {
        let buscado = $.grep(await CuentaContable.Todos(), function (entidad, index) {
            return entidad.IdTipoCuenta == 4;
        });
        return buscado;
    }
    static async TraerTodosPrestamo() {
        let buscado = $.grep(await CuentaContable.Todos(), function (entidad, index) {
            return entidad.IdTipoCuenta == 5;
        });
        return buscado;
    }
    static async TraerTodosPrincipal() {
        let buscado = $.grep(await CuentaContable.Todos(), function (entidad, index) {
            return entidad.IdTipoCuenta == 1;
        });
        return buscado;
    }
    static async TraerTodos() {
        return await CuentaContable.Todos();
    }
    static async TraerTodosActivos() {
        _Lista_CuentaContable = await CuentaContable.TraerTodos();
        let buscado = $.grep(_Lista_CuentaContable, function (entidad, index) {
            return entidad.IdEstado === 0;
        });
        return buscado;
    }
    static async TraerTodas() {
        let lista = await ejecutarAsync(urlWsCuentaContable + "/TraerTodos");
        _Lista_CuentaContable = [];
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadCuentaContable(value));
            });
        }
        _Lista_CuentaContable = result;
        return _Lista_CuentaContable;
    }
    // Otros
    static async Refresh() {
        _Lista_CuentaContable = await CuentaContable.TraerTodas();
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
                let aItem = '<a href="#" class="mibtn-seleccionCuentaContable" data-Evento="' + eventoSeleccion + '" data-Id="' + item.IdEntidad + '">' + item.Nombre + '</a>';
                let aEliminar = '<a href="#" class="mibtn-EliminarCuentaContable" data-Evento="' + eventoEliminar + '" data-Id="' + item.IdEntidad + '"><span class="icon-bin"></span></a>';
                str += String.format('<li><div class="LinkListaGrilla ' + estiloItem + '">{0}</div><div class="LinkListaGrilla LinkListaGrillaElimina">{1}</div></li>', aItem, aEliminar);
            }
            str += '</ul>';
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
    static async ArmarCombo(lista, div, selector, evento, ventana, estilo) {
        lista.sort(SortXNombre);
        let Cbo = '';
        Cbo += '<select id="_' + div + '" data-Evento="' + evento + '" class="' + estilo + '">';
        Cbo += '    <option value="0" id="' + selector + '">' + ventana + '</option>';
        $(lista).each(function () {
            Cbo += '<option class="mibtn-seleccionCuentaContable" value="' + this.IdEntidad + '" data-Id="' + this.IdEntidad + '" data-Evento="' + evento + '">' + this.Nombre + '</option>';
        });
        Cbo += '</select>';
        return $('#' + div + '').html(Cbo);
    }
}

function LlenarEntidadCuentaContable(entidad) {
    let Res = new CuentaContable;
    Res.IdUsuarioAlta = entidad.IdUsuarioAlta;
    Res.IdUsuarioBaja = entidad.IdUsuarioBaja;
    Res.IdUsuarioModifica = entidad.IdUsuarioModifica;
    Res.FechaAlta = entidad.FechaAlta;
    Res.FechaBaja = entidad.FechaBaja;
    Res.IdMotivoBaja = entidad.IdMotivoBaja;
    Res.IdEntidad = entidad.IdEntidad;
    Res.IdTipoCuenta = entidad.IdTipoCuenta;
    Res.Nombre = entidad.Nombre;
    Res.Observaciones = entidad.Observaciones;
    Res.IdEstado = entidad.IdEstado;
    return Res;
}
$('body').on('click', ".mibtn-seleccionCuentaContable", async function () {
    try {
        $this = $(this);
        let buscado = $.grep(_Lista_CuentaContable, function (entidad, index) {
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
$('body').on('click', ".mibtn-EliminarCuentaContable", async function () {
    try {
        $this = $(this);
        let buscado = $.grep(_Lista_CuentaContable, function (entidad, index) {
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
async function SeleccionCuenta() {
    try {
        let elemento = document.getElementById("_CboCuenta");
        let buscado = $.grep(_Lista_CuentaContable, function (entidad, index) {
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