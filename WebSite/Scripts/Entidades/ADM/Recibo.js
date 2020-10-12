var _ListaRecibos;
var _ListaTipoPago;


class StrBusquedaRecibo {
    constructor() {
        this.Desde = 0;
        this.Hasta = 0;
        this.CUIT = 0;
        this.Importe = 0;
        this.NroRecibo = 0;
        this.NroCheque = 0;
    }
}

class Recibo extends DBE {
    constructor() {
        super();
        this.IdEntidad = 0;
        this.CUIT = 0;
        this.CodigoEntidad = 0;
        this.NroReciboInicio = 0;
        this.NroReciboFin = 0;
        this.Fecha = 0;
        this.ImporteTotal = 0;
        this.ImporteEfectivo = 0;
        this.Observaciones = '';


        //this.ListaPagos = [];
        // this.ListaPeriodos = [];
    }

    async StrFecha() {
        let Result = '';
        if (this.Fecha > 0) {
            Result = LongToDateString(this.Fecha);
        }
        return Result;
    }
    async StrCodigoEntidad(cantCaracteres) {
        let Result = '';
        if (this.CodigoEntidad > 0) {
            Result = Right('00000000000' + this.CodigoEntidad, cantCaracteres);
        }
        return Result;
    }
    async ObjEmpresa() {
        let Result = '';
        if (this.CUIT > 0) {
            Result = await Empresa.TraerUnaXCUIT(this.CUIT);
        }
        return Result;
    }
    async StrNumero() {
        let Result = '';
        if (this.NroReciboFin > 0) {
            let nroInicio = Right('0000' + parseInt(this.NroReciboInicio), 4);
            let nroFin = Right('000000' + parseInt(this.NroReciboFin), 6);
            Result = nroInicio + nroFin;
        }
        return Result;
    }
    // ABM
    async Alta(ListaPagos, ListaPeriodos) {
        await this.ValidarCampos();
        this.Observaciones = this.Observaciones.toUpperCase();
        try {
            let ObjU = JSON.parse(sessionStorage.getItem("User"));
            this.IdUsuarioAlta = ObjU.IdEntidad;
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsRecibo + "/Alta", data);
            if (id !== undefined)
                this.IdEntidad = id;
            await Recibo.AltaPagos(id, ListaPagos);
            await Recibo.AltaPeriodos(id, ListaPeriodos);
            return;
        } catch (e) {
            throw e;
        }
    }
    static async AltaPagos(IdRecibo, Lista) {
        try {
            let ObjU = JSON.parse(sessionStorage.getItem("User"));
            this.IdUsuarioAlta = ObjU.IdEntidad;
            let i = 0;
            while (i <= Lista.length - 1) {
                let ObjLista = Lista[i];
                ObjLista.IdRecibo = IdRecibo;
                let data = {
                    'entidad': ObjLista
                }
                await ejecutarAsync(urlWsRecibo + "/AltaPago", data);
                i++;
            }
            return;
        } catch (e) {
            throw e;
        }
    }
    static async AltaPeriodos(IdRecibo, Lista) {
        try {
            let ObjU = JSON.parse(sessionStorage.getItem("User"));
            this.IdUsuarioAlta = ObjU.IdEntidad;
            let i = 0;
            while (i <= Lista.length - 1) {
                let ObjLista = Lista[i];
                ObjLista.IdRecibo = IdRecibo;
                let data = {
                    'entidad': ObjLista
                }
                await ejecutarAsync(urlWsRecibo + "/AltaPeriodo", data);
                i++;
            }
            return;
        } catch (e) {
            throw e;
        }
    }
    async Baja() {
        try {
            let ObjU = JSON.parse(sessionStorage.getItem("User"));
            this.IdUsuarioBaja = ObjU.IdEntidad;
            this.IdEstado = 'A';
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsRecibo + "/Baja", data);
            if (id !== undefined)
                this.IdEntidad = id;
            let buscados = $.grep(_ListaIngresosManuales, function (entidad, index) {
                return entidad.IdEntidad !== id;
            });
            _ListaRecibos = buscados;
            return;
        } catch (e) {
            throw e;
        }
    }
    async Modifica() {
        await this.ValidarCamposIngreso();
        try {
            let ObjU = JSON.parse(sessionStorage.getItem("User"));
            this.IdUsuarioModifica = ObjU.IdEntidad;
            this.IdEstado = 'A';
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsRecibo + "/Modifica", data);
            if (id !== undefined)
                this.IdEntidad = id;
            let buscados = $.grep(_ListaIngresosManuales, function (entidad, index) {
                return entidad.IdEntidad !== id;
            });
            _ListaIngresos = buscados;
            this.IdEstado = 'A';
            _ListaRecibos.push(this);
            return;
        } catch (e) {
            throw e;
        }
    }
    async ValidarCampos() { }
    // Traer
    static async TraerUno(IdEntidad) {
        let buscado = $.grep(await Recibo.TraerTodos(), function (entidad, index) {
            return entidad.IdEntidad == IdEntidad;
        });
        return buscado[0];
    }
    static async TraerTodos() {
        let lista = await ejecutarAsync(urlWsRecibo + "/TraerTodos");
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadRecibo(value));
            });
        }
        return result;
    }
    static async TraerTodosXBusqueda(Busqueda) {
        let data = {
            'Busqueda': Busqueda
        };
        let lista = await ejecutarAsync(urlWsRecibo + "/TraerTodosXBusqueda", data);
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadRecibo(value));
            });
        }
        return result;
    }
    static async ArmarGrillaPeriodos(div, lista, estilo, eventoEliminar) {
        $("#" + div + "").html('');
        let str = "";
        _ListaPeriodos = lista;
        if (lista.length > 0) {
            str += '<div style="' + estilo + '">';
            str += '<table class="table table-sm table-striped table-hover">';
            str += '    <thead>';
            str += '        <tr>';
            str += '            <th scope="col-3">Periodo</th>';
            str += '            <th scope="col-3 text-center">Importe</th>';
            str += '            <th scope="col-1"></th>';
            str += '        </tr>';
            str += '    </thead>';
            str += '    <tbody>';
            for (let item of lista) {
                str += '        <tr>';
                let StrPeriodo = Left(item.Periodo, 2) + '/' + Right(item.Periodo, 4)
                str += '            <td class="col-3 text-left"><span> ' + StrPeriodo + '</span></td>';
                str += '            <td class="col-3 text-center"><span> ' + separadorMiles(item.Importe.toFixed(2)) + '</span></td>';
                str += '            <td class="col-1 text-center"><a href="#" id="_IdPeriodo_' + item.IdEntidad + '" data-Id="' + item.IdEntidad + '" data-EventoEliminar="' + eventoEliminar + '" onclick="EliminarPeriodo(this);"><span class="icon-bin text-light"></span></a></td>';
                str += '        </tr>';
            }
        }
        str += '    </tbody>';
        str += '</table >';
        str += '</div >';
        return $("#" + div + "").html(str);
    }
    static async ArmarGrillaPagos(div, lista, estilo, eventoEliminar) {
        $("#" + div + "").html('');
        let str = "";
        _ListaPagos = lista;
        if (lista.length > 0) {
            str += '<div style="' + estilo + '">';
            str += '<table class="table table-sm table-striped table-hover">';
            str += '    <thead>';
            str += '        <tr>';
            str += '            <th scope="col-2">Tipo</th>';
            str += '            <th scope="col-3 text-center">Importe</th>';
            str += '            <th scope="col-3 text-center">Numero</th>';
            str += '            <th scope="col-1"></th>';
            str += '        </tr>';
            str += '    </thead>';
            str += '    <tbody>';
            for (let item of lista) {
                str += '        <tr>';
                str += '            <td class="col-2 text-left"><span> ' + Left((await TipoPagoManual.TraerUno(item.IdTipoPagoManual)).Nombre, 2) + '</span></td>';
                str += '            <td class="col-3 text-center"><span> ' + separadorMiles(item.Importe.toFixed(2)) + '</span></td>';
                str += '            <td class="col-3 text-center"><span> ' + item.Numero + '</span></td>';
                str += '            <td class="col-1 text-center"><a href="#" id="_IdPago_' + item.IdEntidad + '" data-Id="' + item.IdEntidad + '" data-EventoEliminar="' + eventoEliminar + '" onclick="EliminarPago(this)"><span class="icon-bin text-light"></span></a></td>';
                str += '        </tr>';
            }
        }
        str += '    </tbody>';
        str += '</table >';
        str += '</div >';
        return $("#" + div + "").html(str);
    }
    // Herramientas
    static async ArmarGrillaCabecera(div) {
        $("#" + div + "").html('');
        let str = "";
        str += '<table class="table table-sm">';
        str += '    <thead>';
        str += '        <tr>';
        str += '            <th class="text-center" style="width: 80px;">Fecha</th>';
        str += '            <th class="text-center" style="width: 50px;">CUIT</th>';
        str += '            <th class="text-center" style="width: 220px;">Empresa</th>';
        str += '            <th class="text-center" style="width: 110px;">Importe Total</th>';
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
                str += '            <td class="text-center" style="width: 80px;"><small class="text-light">' + await item.StrFecha() + '</small></td>';
                str += '            <td class="text-center" style="width: 50px;"><small class="text-light">' + await item.CUIT + '</small></td>';
                let strRazonSocial = (await item.ObjEmpresa()).RazonSocial;
                if (strRazonSocial.length > 0) {
                    strRazonSocial = Left(strRazonSocial, 40);
                }
                str += '            <td class="text-left" style="width: 220px;"><small class="text-light">' + strRazonSocial + '</small></td>';
                str += '            <td class="text-right pr-1" style="width: 100px;"><small class="text-light">' + separadorMiles(item.ImporteTotal.toFixed(2)) + '</small></td>';
                str += '        </tr>';
            }
        }
        str += '    </tbody>';
        str += '</table >';
        str += '</div >';
        return $("#" + div + "").html(str);
    }
}
function LlenarEntidadRecibo(entidad) {
    let Res = new Recibo;
    // DBE
    Res.IdUsuarioAlta = entidad.IdUsuarioAlta;
    Res.IdUsuarioBaja = entidad.IdUsuarioBaja;
    Res.IdUsuarioModifica = entidad.IdUsuarioModifica;
    Res.FechaAlta = entidad.FechaAlta;
    Res.FechaBaja = entidad.FechaBaja;
    Res.IdMotivoBaja = entidad.IdMotivoBaja;
    // Entidad
    Res.IdEntidad = entidad.IdEntidad;
    Res.CUIT = entidad.CUIT;
    Res.CodigoEntidad = entidad.CodigoEntidad;
    Res.NroReciboInicio = entidad.NroReciboInicio;
    Res.NroReciboFin = entidad.NroReciboFin;
    Res.Fecha = entidad.Fecha;
    Res.ImporteTotal = entidad.ImporteTotal;
    Res.ImporteEfectivo = entidad.ImporteEfectivo;
    Res.Observaciones = entidad.Observaciones;
    return Res;
}
async function SeleccionReciboGrilla(MiElemento) {
    // try {
    //     let elemento = document.getElementById(MiElemento.id);
    //     let evento = elemento.getAttribute('data-Evento');
    //     let buscado = $.grep(_ListaIngresos, function (entidad, index) {
    //         return entidad.IdEntidad == MiElemento.id;
    //     });
    //     if (buscado[0] != undefined) {
    //         let event = new CustomEvent(evento, {
    //             detail: buscado[0]
    //         });
    //         document.dispatchEvent(event);
    //     }
    // } catch (e) {
    //     alertAlerta(e);
    // }
}
async function EliminarPago(MiElemento) {
    try {
        let elemento = document.getElementById(MiElemento.id);
        let evento = elemento.getAttribute('data-EventoEliminar');
        let Identificador = elemento.getAttribute('data-Id');
        let buscado = $.grep(_ListaPagos, function (entidad, index) {
            return entidad.IdEntidad == Identificador;
        });
        if (buscado[0] != undefined) {
            let event = new CustomEvent(evento, {
                detail: buscado[0]
            });
            document.dispatchEvent(event);
        }
    } catch (error) {
        alertAlerta(error);
    }
}
async function EliminarPeriodo(MiElemento) {
    try {
        let elemento = document.getElementById(MiElemento.id);
        let evento = elemento.getAttribute('data-EventoEliminar');
        let Identificador = elemento.getAttribute('data-Id');
        let buscado = $.grep(_ListaPeriodos, function (entidad, index) {
            return entidad.IdEntidad == Identificador;
        });
        if (buscado[0] != undefined) {
            let event = new CustomEvent(evento, {
                detail: buscado[0]
            });
            document.dispatchEvent(event);
        }
    } catch (error) {
        alertAlerta(error);
    }
}