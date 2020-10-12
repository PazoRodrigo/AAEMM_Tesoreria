var _Lista_ChequeTercero;

class StrBusquedaChequeTercero {
    constructor() {
        this.Desde = 0;
        this.Hasta = 0;
        this.RazonSocial = '';
        this.CUIT = 0;
        this.Importe = 0;
        this.NroRecibo = 0;
        this.NroCheque = 0;
    }
}
class ChequeTercero extends Cheque {
    constructor() {
        super();
        this.FechaVencimiento = 0;
        this.FechaDeposito = 0;
        this.IdRecibo = 0;
        this.IdEstado = 0;
    }

    async StrFechaVencimiento() {
        let Result = '';
        if (this.FechaVencimiento > 0) {
            Result = LongToDateString(this.FechaVencimiento);
        }
        return Result;
    }
    async ObjRecibo() {
        let Result = new Recibo;
        if (this.IdEntidad > 0) {
            Result = await Recibo.TraerUno(this.IdRecibo);
        }
        return Result;
    }
    async StrNumero(cantCaracteres) {
        let Result = '';
        if (this.Numero > 0) {
            Result = Right('00000000000' + this.Numero, cantCaracteres);
        }
        return Result;
    }
    async StrEstado() {
        let Result = await EstadoCheque.StrEstadoTercero(this.IdEstado);
        return Result;
    }
    // ABM
    // async Alta() {
    //     await this.ValidarCamposChequeTercero();
    //     this.Numero = Right('0000000000' + this.Numero, 10);
    //     this.Observaciones = this.Observaciones.toUpperCase();
    //     try {
    //         let data = {
    //             'entidad': this
    //         };
    //         let id = await ejecutarAsync(urlWsChequeTercero + "/Alta", data);
    //         if (id !== undefined)
    //             this.IdEntidad = id;
    //         _Lista_ChequeTercero.push(this);
    //         return;
    //     } catch (e) {
    //         throw e;
    //     }
    // }
    async Modifica() {
        await this.ValidarCamposChequeTercero();
        this.Numero = Right('0000000000' + this.Numero, 10);
        this.Observaciones = this.Observaciones.toUpperCase();
        try {
            let ObjU = JSON.parse(sessionStorage.getItem("User"));
            this.IdUsuarioModifica = ObjU.IdEntidad;
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsChequeTercero + "/Modifica", data);
            if (id !== undefined)
                this.IdEntidad = id;
            _Lista_ChequeTercero = $.grep(_Lista_ChequeTercero, function (entidad, index) {
                return entidad.IdEntidad !== id;
            });
            _Lista_ChequeTercero.push(this);
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
            let id = await ejecutarAsync(urlWsChequeTercero + "/Baja", data);
            if (id !== undefined)
                this.IdEntidad = id;
            let buscados = $.grep(_Lista_ChequeTercero, function (entidad, index) {
                return entidad.IdEntidad !== id;
            });
            _Lista_ChequeTercero = buscados;
            this.IdEstado = 1;
            _Lista_ChequeTercero.push(this);
            return;
        } catch (e) {
            throw e;
        }
    }

    async ValidarCamposChequeTercero() {
        let sError = '';
        if (this.Numero.length == 0) {
            sError += 'Debe ingresar el Número del Cheque';
        } else {
            if (parseInt(this.Numero) == 0) {
                sError += 'El Número del Cheque debe ser mayor a 0';
            }
        }
        if (this.FechaVencimiento.length == 0) {
            sError += 'Debe ingresar la Fecha de Vencimiento';
        }
        if (this.IdBanco == 0) {
            sError += 'Debe ingresar el Banco del Cheque';
        }
        if (sError !== '') {
            throw '<b> Error de grabación </b> <br/><br/>' + sError;
        }
    }
    // Todos
    static async Todos() {
        if (_Lista_ChequeTercero === undefined) {
            _Lista_ChequeTercero = await ChequeTercero.TraerTodas();
        }
        return _Lista_ChequeTercero;
    }

    // Traer
    static async TraerUno(IdEntidad) {
        let buscado = $.grep(await ChequeTercero.Todos(), function (entidad, index) {
            return entidad.IdEntidad == IdEntidad;
        });
        let Encontrado = buscado[0];
        return Encontrado;
    }
    static async TraerTodos() {
        return await ChequeTercero.Todos();
    }
    static async TraerTodosXEstado(IdEstado) {
        let buscado = $.grep(await ChequeTercero.Todos(), function (entidad, index) {
            return entidad.IdEstado == IdEstado;
        });
        return buscado;
    }

    static async TraerTodosActivos() {
        _Lista_ChequeTercero = await ChequeTercero.TraerTodos();
        let buscado = $.grep(_Lista_ChequeTercero, function (entidad, index) {
            return entidad.IdEstado == 0;
        });
        return buscado;
    }
    static async TraerTodas() {
        let lista = await ejecutarAsync(urlWsChequeTercero + "/TraerTodos");
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadChequeTercero(value));
            });
        }
        return result;
    }
    static async TraerTodosXBusqueda(Busqueda) {
        let data = {
            'Busqueda': Busqueda
        };
        let lista = await ejecutarAsync(urlWsChequeTercero + "/TraerTodosXBusqueda", data);
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadChequeTercero(value));
            });
        }
        return result;
    }

    // Otros
    static async Refresh() {
        _Lista_ChequeTercero = await ChequeTercero.TraerTodas();
    }
    // Herramientas
    static async ArmarGrillaCabecera(div) {
        $("#" + div + "").html('');
        let str = "";
        str += '<table class="table table-sm">';
        str += '    <thead>';
        str += '        <tr>';
        str += '            <th class="" style="width:49px;"></th>';
        str += '            <th class="text-center" style="width: 80px;">Cheque</th>';
        str += '            <th class="text-center" style="width: 80px;">Vencimiento</th>';
        str += '            <th class="text-center" style="width: 220px;">CUIT</th>';
        str += '            <th class="text-center" style="width: 110px;">Importe</th>';
        str += '            <th class="text-center" style="width: 100px;">Nro. Recibo</th>';
        str += '            <th class="text-center" style="width: 40px;">Estado</th>';
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
                str += '            <td style="width:45px;"><a hfre="#" id="' + item.IdEntidad + '" data-Id="' + item.IdEntidad + '" data-Evento="' + evento + '" onclick="SeleccionChequeTerceroGrilla(this);"> <img src="../../Imagenes/lupa.png" alt=""></a></td>';
                str += '            <td class="text-center" style="width: 80px;"><small class="text-light">' + await item.StrNumero(10) + '</small></td>';
                str += '            <td class="text-center" style="width: 120px;"><small class="text-light">' + await item.StrFechaVencimiento() + '</small></td>';
                let ObjRecibo = await item.ObjRecibo();
                str += '            <td class="text-left" style="width: 220px;"><small class="text-light">' + ObjRecibo.CUIT + '</small></td>';
                str += '            <td class="text-right pr-1" style="width: 100px;"><small class="text-light">' + separadorMiles(item.Importe.toFixed(2)) + '</small></td>';
                str += '            <td class="text-center" style="width: 100px;"><small class="text-light">' + await ObjRecibo.StrNumero() + '</small></td>';
                str += '            <td class="text-center" style="width: 40px;"><small class="text-light">' + await item.StrEstado() + '</small></td>';
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

}
function LlenarEntidadChequeTercero(entidad) {
    let Res = new ChequeTercero;
    Res.IdUsuarioAlta = entidad.IdUsuarioAlta;
    Res.IdUsuarioBaja = entidad.IdUsuarioBaja;
    Res.IdUsuarioModifica = entidad.IdUsuarioModifica;
    Res.FechaAlta = entidad.FechaAlta;
    Res.FechaBaja = entidad.FechaBaja;
    Res.IdMotivoBaja = entidad.IdMotivoBaja;
    // Cheque
    Res.IdEntidad = entidad.IdEntidad;
    Res.IdBanco = entidad.IdBanco;
    Res.Numero = entidad.Numero;
    Res.Importe = entidad.Importe;
    Res.Observaciones = entidad.Observaciones;
    // Entidad
    Res.FechaVencimiento = entidad.FechaVencimiento;
    Res.FechaDeposito = entidad.FechaDeposito;
    Res.IdRecibo = entidad.IdRecibo;
    Res.IdEstado = entidad.IdEstado;
    return Res;
}
async function SeleccionChequeTerceroGrilla(MiElemento) {
    try {
        let elemento = document.getElementById(MiElemento.id);
        let IdBuscado = elemento.getAttribute('data-Id');
        let evento = elemento.getAttribute('data-Evento');
        let buscado = $.grep(_Lista_ChequeTercero, function (entidad, index) {
            return entidad.IdEntidad == IdBuscado;
        });
        if (buscado[0] != undefined) {
            let event = new CustomEvent(evento, {
                detail: buscado[0]
            });
            document.dispatchEvent(event);
        }
    } catch (e) {
        alertAlerta(e);
    }
}
$('body').on('click', ".mibtn-seleccionChequeTercero", async function () {
    try {
        $this = $(this);
        let buscado = $.grep(_Lista_ChequeTercero, function (entidad, index) {
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
$('body').on('click', ".mibtn-EliminarChequeTercero", async function () {
    try {
        $this = $(this);
        let buscado = $.grep(_Lista_ChequeTercero, function (entidad, index) {
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
