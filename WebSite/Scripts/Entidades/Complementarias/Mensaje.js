var _Lista_Mensaje;
var _Lista_Mensaje;
class Mensaje extends DBE {
    constructor() {
        super();
        this.IdEntidad = 0;
        this.IdHastaPerfil = 0;
        this.IdHastaUsuario = 0;
        this.TextoMensaje = '';
        this.IdUsuarioLeido = 0;
        this.FechaLeido = 0;
    }

    // ABM
    async AgregarMensaje() {
        await this.ValidarCamposMensaje();
        this.TextoMensaje = this.TextoMensaje.toUpperCase();
        try {
            let ObjU = JSON.parse(sessionStorage.getItem("User"));
            this.IdUsuarioAlta = ObjU.IdEntidad;
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsMensaje + "/AgregarMensaje", data);
            if (id !== undefined) {
                this.IdEntidad = id;
            }
            return;
        } catch (e) {
            throw e;
        }
    }
    async Modifica() {
        try {
            await this.ValidarCamposMensaje();
            this.Nombre = this.Nombre.toUpperCase();
            this.Observaciones = this.Observaciones.toUpperCase();
            let ObjU = JSON.parse(sessionStorage.getItem("User"));
            this.IdUsuarioModifica = ObjU.IdEntidad;
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsMensaje + "/Modifica", data);
            if (id !== undefined)
                this.IdEntidad = id;
            let buscados = $.grep(_Lista_Mensaje, function (entidad, index) {
                return entidad.IdEntidad !== id;
            });
            _Lista_Mensaje = buscados;
            this.IdEstado = 0;
            _Lista_Mensaje.push(this);
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
            let id = await ejecutarAsync(urlWsMensaje + "/Baja", data);
            if (id !== undefined)
                this.IdEntidad = id;
            let buscados = $.grep(_Lista_Mensaje, function (entidad, index) {
                return entidad.IdEntidad !== id;
            });
            _Lista_Mensaje = buscados;
            _Lista_Mensaje.push(this);
            return;
        } catch (e) {
            throw e;
        }
    }
    async Leido() {
        try {
            let ObjU = JSON.parse(sessionStorage.getItem("User"));
            this.IdUsuarioLeido = ObjU.IdEntidad;
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsMensaje + "/Leido", data);
            if (id !== undefined)
                this.IdEntidad = id;
            let buscados = $.grep(_Lista_Mensaje, function (entidad, index) {
                return entidad.IdEntidad !== id;
            });
            _Lista_Mensaje = buscados;
            _Lista_Mensaje.push(this);
            return;
        } catch (e) {
            throw e;
        }
    }
    async ValidarCamposMensaje() {
        let sError = '';
        if (this.TextoMensaje.length === 0) {
            sError += 'Debe ingresar el Mensaje';
        }
        if (sError !== '') {
            throw '<b> Error de grabación </b> <br/><br/>' + sError;
        }
    }

    // Traer
    static async TraerTodosXUsuario() {
        let ObjU = JSON.parse(sessionStorage.getItem("User"));
        let data = {
            'IdUsuario': ObjU.IdEntidad
        };
        let lista = await ejecutarAsync(urlWsMensaje + "/TraerTodosXUsuario", data);
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadMensaje(value));
            });
        }
        _Lista_Mensaje = result;
        return result;
    }
   
    static async Refresh() {
        _Lista_Mensaje = await Mensaje.TraerTodosXUsuario();
        return result;
    }
    // Herramientas
    //static async ArmarGrilla(lista, div, eventoSeleccion, eventoEliminar, estilo) {
    //    $('#' + div + '').html('');
    //    let str = '';
    //    // lista.sort(SortXNombre);
    //    if (lista.length > 0) {
    //        str += '<div style="' + estilo + '">';
    //        str += '    <ul class="ListaGrilla">';
    //        let estiloItem = '';
    //        for (let item of lista) {
    //            estiloItem = 'LinkListaGrillaObjeto';
    //            if (item.IdEstado === 1) {
    //                estiloItem = 'LinkListaGrillaObjetoEliminado';
    //            }
    //            let aItem = '<a href="#" class="mibtn-seleccionMensaje" data-Evento="' + eventoSeleccion + '" data-Id="' + item.IdEntidad + '">' + item.Nombre + '</a>';
    //            let aEliminar = '<a href="#" class="mibtn-EliminarMensaje" data-Evento="' + eventoEliminar + '" data-Id="' + item.IdEntidad + '"><span class="icon-bin"></span></a>';
    //            str += String.format('<li><div class="LinkListaGrilla ' + estiloItem + '">{0}</div><div class="LinkListaGrilla LinkListaGrillaElimina">{1}</div></li>', aItem, aEliminar);
    //        }
    //        str += '    </ul>';
    //        str += '</div>';
    //    }
    //    return $('#' + div + '').html(str);
    //}
    //static async ArmarRadios(lista, div, evento, estilo) {
    //    $('#' + div + '').html('');
    //    let str = '';
    //    await Area.Refresh();
    //    if (lista.length > 0) {
    //        str += '<div style="' + estilo + '">';
    //        str += '    <table class="table table-bordered" style="width: 70%;">';
    //        str += '        <thead>';
    //        str += '            <tr>';
    //        str += '                <th colspan="2" style="text-align: center;">Areas</th>';
    //        str += '            </tr>';
    //        str += '        </thead>';
    //        str += '        <tbody>';
    //        for (let item of lista) {
    //            let radioSeleccion = '<input type="radio" class="mibtn-seleccionArea"  name="rblArea" data-Evento="' + evento + '" data-Id="' + item.IdEntidad + '" value="' + item.IdEntidad + '">';
    //            str += String.format('<tr><td align="center" valign="middle" style="width: 5%;">{0}</td><td align="left">{1}</td></tr>', radioSeleccion, item.Nombre);
    //        }
    //        str += '        </tbody>';
    //        str += '    </table>';
    //        str += '</div>';
    //    }
    //    return $('#' + div + '').html(str);
    //}
    //static async ArmarCheckBoxs(lista, div, evento, estilo) {
    //    $('#' + div + '').html('');
    //    let str = '';
    //    str += '<div style="' + estilo + '">';
    //    await Area.Refresh();
    //    if (lista.length > 0) {
    //        for (let item of lista) {
    //            str += '<div class="col-lg-4"><input type="checkbox" class="micbx-Area" name="CkbList_Areas" value="' + item.IdEntidad + '"    id="chk_' + item.IdEntidad + '" /><label for="chk_' + item.IdEntidad + '"> ' + item.Nombre + '</label></div>';
    //        }
    //    }
    //    str += '</div>';
    //    return $('#' + div + '').html(str);
    //}
    //static async ArmarCombo(lista, div, IdSelect, selector, evento, estilo) {
    //    let Cbo = '';
    //    Cbo += '<select id="' + IdSelect + '"  class="' + estilo + '" onchange="SeleccionEntidaMensajeCombo(this);" data-Evento="' + evento + '">';
    //    Cbo += '    <option value="0" >' + selector + '</option>';
    //    for (let item of lista) {
    //        Cbo += '<option value="' + item.IdEntidad + '" >' + item.Nombre + '</option>';
    //    };
    //    Cbo += '</select>';
    //    return $('#' + div + '').html(Cbo);
    //}
}

function LlenarEntidadMensaje(entidad) {
    let Res = new Mensaje;
    Res.IdUsuarioAlta = entidad.IdUsuarioAlta;
    Res.IdUsuarioBaja = entidad.IdUsuarioBaja;
    Res.IdUsuarioModifica = entidad.IdUsuarioModifica;
    Res.FechaAlta = entidad.FechaAlta;
    Res.FechaBaja = entidad.FechaBaja;
    Res.IdMotivoBaja = entidad.IdMotivoBaja;
    Res.IdEntidad = entidad.IdEntidad;
    Res.TextoMensaje = entidad.TextoMensaje;
    Res.IdHastaPerfil = entidad.IdHastaPerfil;
    Res.IdHastaUsuario = entidad.IdHastaUsuario;
    Res.IdUsuarioLeido = entidad.IdUsuarioLeido;
    Res.FechaLeido = entidad.FechaLeido;
    return Res;
}
$('body').on('click', ".mibtn-seleccionMensaje", async function () {
    try {
        $this = $(this);
        let buscado = $.grep(_Lista_Mensaje, function (entidad, index) {
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
$('body').on('click', ".mibtn-EliminarMensaje", async function () {
    try {
        $this = $(this);
        let buscado = $.grep(_Lista_Mensaje, function (entidad, index) {
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
async function SeleccionMensaje() {
    try {
        let elemento = document.getElementById("_CboMensaje");
        let buscado = $.grep(_Lista_Mensaje, function (entidad, index) {
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
async function SeleccionEntidadFamiliaCombo(MiElemento) {
    try {
        let elemento = document.getElementById(MiElemento.id);
        let buscado = $.grep(_Lista_Mensaje, function (entidad, index) {
            return entidad.IdEntidad == elemento.options[elemento.selectedIndex].value;
        });
        let Seleccionado = [];
        let evento = elemento.getAttribute('data-Evento');
        if (buscado[0] != undefined) {
            Seleccionado = buscado[0];
        } else {
            Seleccionado.IdEntidad = 0;
        }
        let event = new CustomEvent(evento, { detail: Seleccionado });
        document.dispatchEvent(event);
    } catch (e) {
        alertAlerta(e);
    }
}