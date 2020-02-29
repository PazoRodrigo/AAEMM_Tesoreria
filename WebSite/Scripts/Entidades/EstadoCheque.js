var _Lista_EstadoCheque_Propios;
var _Lista_EstadoCheque_Terceros;

class EstadoCheque {
    constructor() {
        this.IdEntidad = 0;
        this.Estado = '';
        this.Observaciones = '';
        this.IdTipoCheque = '';
    }

    // Todos
    static async TodosPropios() {
        if (_Lista_EstadoCheque_Propios === undefined) {
            _Lista_EstadoCheque_Propios = await EstadoCheque.TraerTodas_Propios();
        }
        return _Lista_EstadoCheque_Propios;
    }
    static async TodosTerceros() {
        if (_Lista_EstadoCheque_Terceros === undefined) {
            _Lista_EstadoCheque_Terceros = await EstadoCheque.TraerTodas_Terceros();
        }
        return _Lista_EstadoCheque_Terceros;
    }

    // Traer
    static async TraerUno(IdEntidad, IdTipoCheque) {
        let Encontrado = new EstadoCheque;
        let buscado;
        switch (parseInt(IdTipoCheque)) {
            case 1:
                _Lista_EstadoCheque_Propios = await EstadoCheque.TraerTodos_Propios();
                buscado = $.grep(_Lista_EstadoCheque_Propios, function (entidad, index) {
                    return entidad.IdEntidad === IdEntidad;
                });
                break;
            case 2:
                _Lista_EstadoCheque_Terceros = await EstadoCheque.TraerTodos_Terceros();
                buscado = $.grep(_Lista_EstadoCheque_Terceros, function (entidad, index) {
                    return entidad.IdEntidad === IdEntidad;
                });
                break;
            default:
        }
        Encontrado = buscado[0];
        return Encontrado;
    }
    static async TraerTodos_Propios() {
        return await EstadoCheque.TodosPropios();
    }
    static async TraerTodos_Terceros() {
        return await EstadoCheque.TodosTerceros();
    }
    static async TraerTodas_Propios() {
        let lista = await ejecutarAsync(urlWsEstadoCheque + "/TraerTodos_ChequesPropios");
        _Lista_EstadoCheque_Propios = [];
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadEstadoCheque(value));
            });
        }
        _Lista_EstadoCheque_Propios = result;
        return _Lista_EstadoCheque_Propios;
    }
    static async TraerTodas_Terceros() {
        let lista = await ejecutarAsync(urlWsEstadoCheque + "/TraerTodos_ChequesTerceros");
        _Lista_EstadoCheque_Terceros = [];
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadEstadoCheque(value));
            });
        }
        _Lista_EstadoCheque_Terceros = result;
        return _Lista_EstadoCheque;
    }
    // Otros
    static async Refresh() {
        _Lista_EstadoCheque_Terceros = await EstadoCheque.TraerTodas_Terceros();
        _Lista_EstadoCheque_Propios = await EstadoCheque.TraerTodas_Propios();
    }
    // Herramientas
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
                let radioSeleccion = '<input type="radio" class="mibtn-seleccionArea"  name="rblArea" data-Evento="' + evento + '"  data-Id="' + item.IdEntidad + '" value="' + item.IdEntidad + '">';
                str += String.format('<tr><td align="center" valign="middle" style="width: 5%;">{0}</td><td align="left">{1}</td></tr>', radioSeleccion, item.Nombre);
            }
            str += '        </tbody>';
            str += '    </table>';
            str += '</div>';
        }
        return $('#' + div + '').html(str);
    }
    static async ArmarCombo(lista, div, selector, evento, ventana, Cbo) {
        let cbo = "";
        cbo += '<div id="' + Cbo + '" class="dropdown">';
        cbo += '    <button id="' + selector + '" class="btn btn-primary dropdown-toggle btn-md btn-block" type="button" data-toggle="dropdown">' + ventana;
        cbo += '        <span class="caret"></span>';
        cbo += '    </button>';
        cbo += '<ul class="dropdown-menu">';
        $(lista).each(function () {
            cbo += '<li><a href="#" class="mibtn-seleccionEstadoCheque" data-Id="' + this.IdEntidad + '" data-IdTipoCheque="' + this.IdTipoCheque + '" data-Nombre="' + this.Estado + '" data-Evento="' + evento + '" > ' + this.Estado + '</a></li>';
        });
        cbo += '</ul>';
        cbo += '</div>';
        return $('#' + div + '').html(cbo);
    }
}
function LlenarEntidadEstadoCheque(entidad) {
    let Res = new EstadoCheque;
    Res.IdEntidad = entidad.IdEntidad;
    Res.Estado = entidad.Estado;
    Res.Observaciones = entidad.Observaciones;
    Res.IdTipoCheque = entidad.IdTipoCheque;
    return Res;
}
$('body').on('click', ".mibtn-seleccionEstadoCheque", async function () {
    try {
        $this = $(this);
        let _Lista_EstadoCheque = [];
        let TipoCheque = $this.attr("data-IdTipoCheque");
        switch (parseInt(TipoCheque)) {
            case 1:
                _Lista_EstadoCheque = _Lista_EstadoCheque_Propios;
                break;
            case 2:
                _Lista_EstadoCheque = _Lista_EstadoCheque_Terceros;
                break;
            default:
                throw "Error En tipo de Cheque"
        }
        let buscado = $.grep(_Lista_EstadoCheque, function (entidad, index) {
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