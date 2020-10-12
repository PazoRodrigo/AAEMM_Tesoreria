var _Lista_TipoPagosManuales;

class TipoPagoManual extends DBE {
    constructor() {
        super();
        this.IdEntidad = 0;
        this.Nombre = '';
        this.Abreviatura = '';
    }

    // Todos
    static async Todos() {
        if (_Lista_TipoPagosManuales === undefined) {
            _Lista_TipoPagosManuales = await TipoPagoManual.TraerTodas();
        }
        return _Lista_TipoPagosManuales;
    }

    // Traer
    static async TraerUno(IdEntidad) {
        _Lista_TipoPagosManuales = await TipoPagoManual.TraerTodos();
        let buscado = $.grep(_Lista_TipoPagosManuales, function (entidad, index) {
            return entidad.IdEntidad === IdEntidad;
        });
        let Encontrado = buscado[0];
        return Encontrado;
    }
    static async TraerTodos() {
        return await TipoPagoManual.Todos();
    }
    static async TraerTodas() {
        let lista = await ejecutarAsync(urlWsTipoPagoManual + "/TraerTodos");
        _Lista_TipoPagosManuales = [];
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadTipoPagoManual(value));
            });
        }
        return result;
    }
    // Otros
    static async Refresh() {
        _Lista_TipoPagosManuales = await TipoPagoManual.TraerTodas();
    }
    // Herramientas
    static async ArmarCombo(lista, div, selector, evento, ventana, estilo) {
        let Cbo = '';
        Cbo += '<select id="_' + div + '" data-Evento="' + evento + '" class="' + estilo + '">';
        Cbo += '    <option value="0" id="' + selector + '">' + ventana + '</option>';
        $(lista).each(function () {
            Cbo += '<option class="mibtn-seleccionTipoPagoManual" value="' + this.IdEntidad + '" data-Id="' + this.IdEntidad + '" data-Evento="' + evento + '">' + this.Abreviatura + '</option>';
        });
        Cbo += '</select>';
        return $('#' + div + '').html(Cbo);
    }
}

function LlenarEntidadTipoPagoManual(entidad) {
    let Res = new TipoPagoManual;
    Res.IdUsuarioAlta = entidad.IdUsuarioAlta;
    Res.IdUsuarioBaja = entidad.IdUsuarioBaja;
    Res.IdUsuarioModifica = entidad.IdUsuarioModifica;
    Res.FechaAlta = entidad.FechaAlta;
    Res.FechaBaja = entidad.FechaBaja;
    Res.IdMotivoBaja = entidad.IdMotivoBaja;
    Res.IdEntidad = entidad.IdEntidad;
    Res.Nombre = entidad.Nombre;
    Res.Abreviatura = entidad.Abreviatura;
    return Res;
}
$('body').on('click', ".mibtn-seleccionTipoPagoManual", async function () {
    try {
        $this = $(this);
        let buscado = $.grep(_Lista_TipoPagosManuales, function (entidad, index) {
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