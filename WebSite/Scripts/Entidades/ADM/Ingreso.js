var _ListaIngresos;

class Ingreso extends DBE {
    constructor() {
        super();
        this.IdEntidad = 0;
        this.IdEstado = '';
        this.IdCentroCosto = 0;
        this.CodigoEntidad = 0;
        this.CUIT = 0;
        this.Periodo = 0;
        this.NroCheche = 0;
        this.Importe = 0;
        this.IdOrigen = 0;
        this.NroRecibo = 0;
        this.NombreArchivo = '';
        this.FechaPago = 0;
        this.FechaAcreditacion = 0;
        this.Observaciones = '';

        this._ObjCentroCosto;
        this._ObjOrigen;
    }

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
    async Estado() {
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

    static async ArmarGrillaCabecera(div) {
        $("#" + div + "").html('');
        let str = "";
        str += '<table class="table table-sm">';
        str += '    <thead>';
        str += '        <tr>';
        str += '            <th style="visibility: hidden;"><img src="../../Imagenes/lupa.png" style="width:30px; height: auto;" alt=""></th>';
        str += '            <th class="text-center" style="width: 80px;">Fecha</th>';
        str += '            <th class="text-center" style="width: 270px;">CUIT / Empresa</th>';
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
                console.log(item);
                str += '        <tr>';
                str += '            <td style=""><a hfre="#" data-Id="' + item.IdEntidad + '"  data-Evento="' + evento + '" onclick="SeleccionIngreso(this);"> <img src="../../Imagenes/lupa.png" alt=""></a></td>';
                str += '            <td class="text-center" style="width: 80px;"><small class="text-light">' + item.FechaAcreditacion + '</small></td>';
                str += '            <td class="text-left" style="width: 270px;"><small class="text-light">' + item.CUIT + '</small></td>';
                str += '            <td class="text-center" style="width: 80px;"><small class="text-light">' + item.Periodo + '</small></td>';
                str += '            <td class="text-right" style="width: 110px;"><small class="text-light">' + separadorMiles(item.Importe) + '</small></td>';
                str += '            <td class="text-center" style="width: 100px;"><small class="text-light">' + item.NombreArchivo + '</small></td>';
                str += '            <td class="text-center" style="width: 40px;"><small class="text-light">' + await item.Origen() + '</small></td>';
                let cheque = '';
                if (item.NroCheche > 0) {
                    cheque = 'Si';
                }
                str += '            <td class="text-center" style="width: 40px;"><small class="text-light">' + cheque + 'si</small></td>';
                str += '            <td class="text-center" style="width: 30px;"><small class="text-light">' + item.IdEstado + '</small></td>';
                str += '        </tr>';
            }
        }
        str += '    </tbody>';
        str += '</table >';
        str += '</div >';
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
    Res.Periodo = entidad.Periodo;
    Res.NroCheche = entidad.NroCheche;
    Res.Importe = entidad.Importe;
    Res.IdOrigen = entidad.IdOrigen;
    Res.NroRecibo = entidad.NroRecibo;
    Res.NombreArchivo = entidad.NombreArchivo;
    Res.FechaPago = entidad.FechaPago;
    Res.FechaAcreditacion = entidad.FechaAcreditacion;
    Res.Observaciones = entidad.Observaciones;
    return Res;
}
function SeleccionIngreso(MiElemento) {
    alert(111);
}
