﻿var _ListaIngresos;

class StrBusquedaIngreso {
    constructor() {
        this.Desde = 0;
        this.Hasta = 0;
        this.CUIT = 0;
        this.RazonSocial = '';
        this.Importe = 0;
        this.NroRecibo = 0;
        this.NroCheque = 0;
        this.Estados = '';
        this.Tipos = '';
    }
}
class Ingreso extends DBE {
    constructor() {
        super();
        this.IdEntidad = 0;
        this.IdEstado = '';
        this.IdCentroCosto = 0;
        this.CodigoEntidad = 0;
        this.CUIT = 0;
        this.Periodo = 0;
        this.NroCheque = 0;
        this.Importe = 0;
        this.IdOrigen = 0;
        this.NroRecibo = 0;
        this.NombreArchivo = '';
        this.FechaPago = 0;
        this.FechaAcreditacion = 0;
        this.Observaciones = '';
        this.RazonSocial = '';

        this._ObjCentroCosto;
        this._ObjOrigen;
        this._ObjEmpresa;
    }

    async StrPeriodo() {
        let Result = '';
        if (this.Periodo > 0) {
            Result = Right(this.Periodo, 2) + '/' + Left(this.Periodo, 4);
        }
        return Result;
    }
    async StrFechaAcreditacion() {
        let Result = '';
        if (this.FechaAcreditacion > 0) {
            Result = LongToDateString(this.FechaAcreditacion);
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
    // Lazy
    async ObjCentroCosto() {
        try {
            if (this._ObjCentroCosto === undefined) {
                this._ObjCentroCosto = CentroCosto.TraerUno(this.IdCentroCosto);
            }
            return this._ObjCentroCosto;
        } catch (e) {
            return new CentroCosto;
        }
    }
    async Origen() {
        let result = '';
        switch (this.IdOrigen) {
            case 1:
                result = 'BN';
                break;
            case 2:
                result = 'PF';
                break;
            case 3:
                result = 'MC';
                break;
            default:
        }
        return result;
    }
    async OrigenLargo() {
        let result = '';
        switch (this.IdOrigen) {
            case 1:
                result = 'Banco Nación';
                break;
            case 2:
                result = 'Pago Fácil';
                break;
            case 3:
                result = 'Mov. Conformados';
                break;
            default:
        }
        return result;
    }
    async Estado() {
        let result = '';
        switch (this.IdEstado) {
            case 'A':
                result = 'Acreditado';
                break;
            case 'L':
                result = 'Pendiente Acreditado';
                break;
            case 'P':
                result = 'Pendiente';
                break;
            case 'R':
                result = 'Rechazado';
                break;
            case 'T':
                result = 'CUIT No Encontrado';
                break;
            default:
        }
        return result;
    }

    // ABM
    async Modifica() {
        await this.ValidarCamposIngreso();
        try {
            let ObjU = JSON.parse(sessionStorage.getItem("User"));
            this.IdUsuarioModifica = ObjU.IdEntidad;
            this.IdEstado = 'A';
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsIngreso + "/Modifica", data);
            if (id !== undefined)
                this.IdEntidad = id;
            let buscados = $.grep(_ListaIngresos, function (entidad, index) {
                return entidad.IdEntidad !== id;
            });
            _ListaIngresos = buscados;
            this.IdEstado = 0;
            _ListaIngresos.push(this);
            return;
        } catch (e) {
            throw e;
        }
    }
    async ValidarCamposIngreso() {
        // Validaciones
        let sError = '';
        if (this.CUIT.length == 0) {
            sError += 'Debe ingresar el CUIT  <br >';
        } else {
            if (this.CUIT.length != 11) {
                sError += 'El CUIT debe tener 11 dígitos  <br >';
            } else {
                if (this.CodigoEntidad == 0) {
                    sError += 'Debe ingresar el CUIT de una entidad correcta  <br >';
                }
            }
        }
        if (this.Periodo.length == 0) {
            sError += 'Debe ingresar el Período "MM/aaaa"  <br >';
        } else {
            if (this.Periodo.length != 7) {
                sError += 'Debe ingresar el Período correctamente "MM/aaaa"  <br >';
            } else {
                if (this.Periodo.slice(2, 3) != '/') {
                    sError += 'Debe ingresar el Período correctamente "MM/aaaa"  <br >';
                } else {
                    let meses = Left(this.Periodo, 2);
                    if (meses < 1 || meses > 11) {
                        sError += 'Verifique el Mes del Período  <br >';
                    }
                    let años = Right(this.Periodo, 4);
                    let fechaActual = FechaHoyLng();
                    if (años > Left(fechaActual, 4)) {
                        sError += 'Verifique el Año del Período <br >';
                    }
                    let peridoIngresado = años.toString() + meses.toString();
                    let peridoActual = Left(fechaActual, 6);
                    if (parseInt(peridoIngresado) > parseInt(peridoActual)) {
                        sError += 'Verifique el Período <br >';
                    }
                }
            }
        }

        if (sError.length > 0) {
            throw sError;
        }
        // Asignaciones
        this.CUIT = parseInt(this.CUIT);
        this.Periodo = parseInt(Right(this.Periodo, 4).toString() + Left(this.Periodo, 2).toString());
        this.Importe = parseFloat(this.Importe.replace(/,/g, ''));
        if (this.NroCheque == '') {
            this.NroCheque = 0;
        }
    }
    // Traer
    static async TraerTodos() {
        let lista = await ejecutarAsync(urlWsIngreso + "/TraerTodos");
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadIngreso(value));
            });
        }
        _ListaIngresos = result;
        return result;
    }
    static async TraerTodosXBusqueda(Busqueda) {
        let data = {
            'Busqueda': Busqueda
        };
        let lista = await ejecutarAsync(urlWsIngreso + "/TraerTodosXBusqueda", data);
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadIngreso(value));
            });
        }
        _ListaIngresos = result;
        return result;
    }
    // Herramientas
    static async ArmarGrillaCabecera(div) {
        $("#" + div + "").html('');
        let str = "";
        str += '<table class="table table-sm">';
        str += '    <thead>';
        str += '        <tr>';
        str += '            <th class="" style="width:49px;"></th>';
        str += '            <th class="text-center" style="width: 80px;">Fecha</th>';
        str += '            <th class="text-center" style="width: 50px;">Cód.</th>';
        str += '            <th class="text-center" style="width: 220px;">CUIT / Empresa</th>';
        str += '            <th class="text-center" style="width: 80px;">Período</th>';
        str += '            <th class="text-center" style="width: 110px;">Importe</th>';
        str += '            <th class="text-center" style="width: 100px;">Archivo</th>';
        str += '            <th class="text-center" style="width: 40px;">Og</th>';
        str += '            <th class="text-center" style="width: 40px;">Ch.</th>';
        str += '            <th class="text-left" style="width: 50px;">Et.</th>';
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
                str += '            <td style="width:45px;"><a hfre="#" id="' + item.IdEntidad + '"  data-Evento="' + evento + '" onclick="SeleccionIngreso(this);"> <img src="../../Imagenes/lupa.png" alt=""></a></td>';
                str += '            <td class="text-center" style="width: 80px;"><small class="text-light">' + await item.StrFechaAcreditacion() + '</small></td>';
                str += '            <td class="text-center" style="width: 50px;"><small class="text-light">' + await item.StrCodigoEntidad(6) + '</small></td>';
                str += '            <td class="text-left" style="width: 220px;"><small class="text-light">' + Left(item.RazonSocial, 25) + '</small></td>';
                str += '            <td class="text-center" style="width: 80px;"><small class="text-light">' + await item.StrPeriodo() + '</small></td>';
                str += '            <td class="text-right pr-1" style="width: 100px;"><small class="text-light">' + separadorMiles(item.Importe.toFixed(2)) + '</small></td>';
                str += '            <td class="text-center" style="width: 100px;"><small class="text-light">' + item.NombreArchivo + '</small></td>';
                str += '            <td class="text-center" style="width: 40px;"><small class="text-light">' + await item.Origen() + '</small></td>';
                let cheque = '';
                if (parseInt(item.NroCheche) > 0) {
                    cheque = 'Si';
                }
                str += '            <td class="text-center" style="width: 40px;"><small class="text-light">' + cheque + '</small></td>';
                str += '            <td class="text-center" style="width: 30px;"><small class="text-light">' + item.IdEstado + '</small></td>';
                str += '        </tr>';
            }
        }
        str += '    </tbody>';
        str += '</table >';
        str += '</div >';
        return $("#" + div + "").html(str);
    }
    static async ArmarGrillaIngresoSeparado(div, lista, estilo, evento) {
        $("#" + div + "").html('');
        let str = "";
        str += '<div style="' + estilo + '">';
        str += '<table class="table table-sm table-striped table-hover">';
        str += '    <thead>';
        str += '        <tr>';
        str += '            <th scope="col">Período</th>';
        str += '            <th scope="col">Importe</th>';
        str += '            <th scope="col"></th>';
        str += '        </tr>';
        str += '    </thead>';
        str += '    <tbody>';
        if (lista.length > 0) {
            for (let item of lista) {
                str += '        <tr>';
                str += '            <td class="col-3 text-center" ><input type="text" id="EntidadPeriodo" class="form-control text-center" placeholder = "MM/aaaa" /></td>';
                str += '            <td class="col-3 text-right pr-1" ><input id="valor_' + item.IdEntidad + '" type="text" id="EntidadImporte" class="form-control text-center" placeholder="Importe" value="' + item.Importe + '" onkeypress="return jsSoloNumeros(event)"/></td>';
                str += '            <td class="col-1 text-center"><a hfre="#" id="' + item.IdEntidad + '" data-Evento="' + evento + '" onclick="AgregarLineaExplotar(this);"><img src="../../Imagenes/plusVerde.png" alt="" /></a></td>';
                str += '        </tr>';
            }
        }
        str += '    </tbody>';
        str += '</table >';
        str += '</div >';
        if (lista.length > 0) {
            str += '<div class="row justify-content-center"><div class="col-10"><div class="btn btn-block btn-success">Guardar Separación de Ingreso</div></div></div>';
        }
        return $("#" + div + "").html(str);
    }
}

function LlenarEntidadIngreso(entidad) {
    let Res = new Ingreso;
    Res.IdUsuarioAlta = entidad.IdUsuarioAlta;
    Res.IdUsuarioBaja = entidad.IdUsuarioBaja;
    Res.IdUsuarioModifica = entidad.IdUsuarioModifica;
    Res.FechaAlta = entidad.FechaAlta;
    Res.FechaBaja = entidad.FechaBaja;
    Res.IdMotivoBaja = entidad.IdMotivoBaja;
    Res.IdEntidad = entidad.IdEntidad;
    Res.IdEstado = entidad.IdEstado;
    Res.IdCentroCosto = entidad.IdCentroCosto;
    Res.CodigoEntidad = entidad.CodigoEntidad;
    Res.CUIT = entidad.CUIT;
    Res.RazonSocial = entidad.RazonSocial;
    Res.Periodo = entidad.Periodo;
    Res.NroCheque = entidad.NroCheque;
    Res.Importe = entidad.Importe;
    Res.IdOrigen = entidad.IdOrigen;
    Res.NroRecibo = entidad.NroRecibo;
    Res.NombreArchivo = entidad.NombreArchivo;
    Res.FechaPago = entidad.FechaPago;
    Res.FechaAcreditacion = entidad.FechaAcreditacion;
    Res.Observaciones = entidad.Observaciones;
    return Res;
}
async function SeleccionIngreso(MiElemento) {
    try {
        let elemento = document.getElementById(MiElemento.id);
        let evento = elemento.getAttribute('data-Evento');
        let buscado = $.grep(_ListaIngresos, function (entidad, index) {
            return entidad.IdEntidad == MiElemento.id;
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
async function AgregarLineaExplotar(MiElemento) {
    try {
        let elemento = document.getElementById(MiElemento.id);
        let evento = elemento.getAttribute('data-Evento');
        let Res = new Ingreso;
        Res.IdEntidad = elemento.id;
        Res.Importe = $("#valor_" + Res.IdEntidad).val();
        let event = new CustomEvent(evento, {
            detail: Res
        });
        document.dispatchEvent(event);
    } catch (e) {
        alertAlerta(e);
    }
}