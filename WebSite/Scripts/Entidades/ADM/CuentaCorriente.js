var _Lista_CuentaCorriente;

class CuentaCorriente extends DBE {
    constructor() {
        super();
        this.IdEntidad = 0;
        this.Nombre = '';
        this.Saldo = parseFloat(0);
    }
    // Todos
    static async Todos() {
        if (_Lista_CuentaCorriente === undefined) {
            _Lista_CuentaCorriente = await CuentaCorriente.TraerTodas();
        }
        return _Lista_CuentaCorriente;
    }

    // Traer
    static async TraerUno(IdEntidad) {
        _Lista_CuentaCorriente = await CuentaCorriente.TraerTodos();
        let buscado = $.grep(_Lista_CuentaCorriente, function (entidad, index) {
            return entidad.IdEntidad == IdEntidad;
        });
        let Encontrado = buscado[0];
        return Encontrado;
    }
    static async TraerTodos() {
        return await CuentaCorriente.Todos();
    }
    static async TraerTodas() {
        let lista = await ejecutarAsync(urlWsCuentaCorriente + "/TraerTodos");
        _Lista_CuentaCorriente = [];
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadCuentaCorriente(value));
            });
        }
        _Lista_CuentaCorriente = result;
        return _Lista_CuentaCorriente;
    }

    static async TraerTodosSaldos() {
        let lista = await ejecutarAsync(urlWsCuentaCorriente + "/TraerTodosSaldos");
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadCuentaCorriente(value));
            });
        }
        return result;
    }
    // Tools
    static async ArmarCombo(lista, div, IdSelect, selector, evento, estilo) {
        let Cbo = '';
        Cbo += '<select id="' + IdSelect + '"  class="' + estilo + '" onchange="SeleccionCuentaCorriente(this);" data-Evento="' + evento + '">';
        Cbo += '    <option value="0" >' + selector + '</option>';
        for (let item of lista) {
            Cbo += '<option value="' + item.IdEntidad + '" >' + item.Nombre + '</option>';
        };
        Cbo += '</select>';
        return $('#' + div + '').html(Cbo);
    }
}
function LlenarEntidadCuentaCorriente(entidad) {
    let Res = new Banco;
    Res.IdUsuarioAlta = entidad.IdUsuarioAlta;
    Res.IdUsuarioBaja = entidad.IdUsuarioBaja;
    Res.IdUsuarioModifica = entidad.IdUsuarioModifica;
    Res.FechaAlta = entidad.FechaAlta;
    Res.FechaBaja = entidad.FechaBaja;
    Res.IdMotivoBaja = entidad.IdMotivoBaja;
    Res.IdEntidad = entidad.IdEntidad;
    Res.Nombre = entidad.Nombre;
    Res.Saldo = entidad.Saldo;
    return Res;
}
async function SeleccionCuentaCorriente() {
    try {
        let elemento = document.getElementById("IdCboCuentaCorriente");
        let buscado = $.grep(_Lista_CuentaCorriente, function (entidad, index) {
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
