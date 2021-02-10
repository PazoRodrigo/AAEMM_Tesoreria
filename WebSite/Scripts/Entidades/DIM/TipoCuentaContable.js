var _Lista_TipoCuentaContable;

class TipoCuentaContable {
    constructor() {
        this.IdEntidad = 0;
        this.Nombre = '';
    }

    static async Todos() {
        if (_Lista_TipoCuentaContable === undefined) {
            _Lista_TipoCuentaContable = new Array;
            let TipoPrincipal = new TipoCuentaContable;
            TipoPrincipal.IdEntidad = 1;
            TipoPrincipal.Nombre = 'Principal';
            _Lista_TipoCuentaContable.push(TipoPrincipal);
            let TipoGasto = new TipoCuentaContable;
            TipoGasto.IdEntidad = 2;
            TipoGasto.Nombre = 'Gasto';
            _Lista_TipoCuentaContable.push(TipoGasto);
            let TipoResultado = new TipoCuentaContable;
            TipoResultado.IdEntidad = 3;
            TipoResultado.Nombre = 'Resultado';
            _Lista_TipoCuentaContable.push(TipoResultado);
            let TipoAnticipos = new TipoCuentaContable;
            TipoAnticipos.IdEntidad = 4;
            TipoAnticipos.Nombre = 'Anticipos';
            _Lista_TipoCuentaContable.push(TipoAnticipos);
            let TipoPrestamos = new TipoCuentaContable;
            TipoPrestamos.IdEntidad = 5;
            TipoPrestamos.Nombre = 'Prestamos';
            _Lista_TipoCuentaContable.push(TipoPrestamos);
        }
        return _Lista_TipoCuentaContable;
    }
    // Traer
    static async TraerUno(IdEntidad) {
        _Lista_TipoCuentaContable = await TipoCuentaContable.TraerTodos();
        let buscado = $.grep(_Lista_TipoCuentaContable, function (entidad, index) {
            return entidad.IdEntidad === IdEntidad;
        });
        let Encontrado = buscado[0];
        return Encontrado;
    }
    static async TraerTodos() {
        return await TipoCuentaContable.Todos();
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
    static async ArmarCombo(lista, div, IdSelect, selector, evento, estilo) {
        let Cbo = '';
        Cbo += '<select id="' + IdSelect + '"  class="' + estilo + '" onchange="SeleccionTipoCuentaContable(this);" data-Evento="' + evento + '">';
        Cbo += '    <option value="0" >' + selector + '</option>';
        for (let item of lista) {
            Cbo += '<option value="' + item.IdEntidad + '" >' + item.Nombre + '</option>';
        };
        Cbo += '</select>';
        return $('#' + div + '').html(Cbo);
    }
}

function LlenarEntidadTipoCuentaContable(entidad) {
    let Res = new TipoCuentaContable;
    Res.IdUsuarioAlta = entidad.IdUsuarioAlta;
    Res.IdUsuarioBaja = entidad.IdUsuarioBaja;
    Res.IdUsuarioModifica = entidad.IdUsuarioModifica;
    Res.FechaAlta = entidad.FechaAlta;
    Res.FechaBaja = entidad.FechaBaja;
    Res.IdMotivoBaja = entidad.IdMotivoBaja;
    Res.IdEntidad = entidad.IdEntidad;
    Res.Nombre = entidad.Nombre;
    Res.Observaciones = entidad.Observaciones;
    return Res;
}
async function SeleccionTipoCuentaContable() {
    try {
        let elemento = document.getElementById("SelectorTipoCuentaContable");
        console.log(elemento.options[elemento.selectedIndex].value);
        let buscado = $.grep(_Lista_TipoCuentaContable, function (entidad, index) {
            return entidad.IdEntidad == elemento.options[elemento.selectedIndex].value;
        });
        let Seleccionado = buscado[0];
        console.log(Seleccionado);
        let evento = elemento.getAttribute('data-Evento');
        let event = new CustomEvent(evento, { detail: Seleccionado });
        document.dispatchEvent(event);
    } catch (e) {
        alertAlerta(e);
    }
}
//$('body').on('click', ".mibtn-seleccionTipoCuentaContable", async function () {
//    try {
//        $this = $(this);
//        let buscado = $.grep(_Lista_TipoCuentaContable, function (entidad, index) {
//            return entidad.IdEntidad === parseInt($this.attr("data-Id"));
//        });
//        let Seleccionado = buscado[0];
//        let evento = $this.attr("data-Evento");
//        let event = new CustomEvent(evento, { detail: Seleccionado });
//        document.dispatchEvent(event);
//    } catch (e) {
//        alertAlerta(e);
//    }
//});
//$('body').on('click', ".mibtn-EliminarTipoCuentaContable", async function () {
//    try {
//        $this = $(this);
//        let buscado = $.grep(_Lista_TipoCuentaContable, function (entidad, index) {
//            return entidad.IdEntidad === parseInt($this.attr("data-Id"));
//        });
//        let Seleccionado = buscado[0];
//        let evento = $this.attr("data-Evento");
//        let event = new CustomEvent(evento, { detail: Seleccionado });
//        document.dispatchEvent(event);
//    } catch (e) {
//        alertAlerta(e);
//    }
//}
//);

//async function SeleccionEntidadFamiliaCombo(MiElemento) {
//    try {
//        let elemento = document.getElementById(MiElemento.id);
//        let buscado = $.grep(_Lista_TipoCuentaContable, function (entidad, index) {
//            return entidad.IdEntidad == elemento.options[elemento.selectedIndex].value;
//        });
//        let Seleccionado = [];
//        let evento = elemento.getAttribute('data-Evento');
//        if (buscado[0] != undefined) {
//            Seleccionado = buscado[0];
//        } else {
//            Seleccionado.IdEntidad = 0;
//        }
//        let event = new CustomEvent(evento, { detail: Seleccionado });
//        document.dispatchEvent(event);
//    } catch (e) {
//        alertAlerta(e);
//    }
//}