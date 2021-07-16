
class AsientoImpresion {
    constructor() {
        this.IdAsiento = 0;
        this.Fecha = '';
        this.ImporteTotal = 0;
        this.IdCuenta = 0;
        this.Cuenta = '';
        this.DH = 0;
        this.ImporteLinea = 0;
    }
    static async TraerTodosXCuentaImpresion(IdCuenta) {
        let data = {
            'IdCuenta': IdCuenta
        };
        let lista = await ejecutarAsync(urlWsAsiento + "/TraerTodosXCuentaImpresion", data);
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadAsientoImpresion(value));
            });
        }
        return result;
    }
    static async ArmarImpresionAsientosOculta(div, Lista) {
        let str = '';
        $("#" + div + "").html('');
        if (Lista?.length > 0) {
            str += '<table>'
            let iAsiento = 0;
            let IdAsiento = Lista[0].IdAsiento;
            str += '';
            // Version 2
            while (iAsiento <= Lista.length - 1) {
                let item = Lista[iAsiento];
                if (IdAsiento != item.IdAsiento) {
                    IdAsiento = item.IdAsiento;
                    // Cabecera
                    str += '<tr class="bg-info"><th>Asiento</th><td>' + Right('0000000' + item.IdAsiento, 7) + '</td>';
                    str += '<tr class="bg-info"><th>Fecha</th><td>' + Date_LongToString(item.Fecha) + '</td>';
                    str += '<tr class="bg-info"><th>Importe Asiento</th><td>' + separadorMiles(item.ImporteTotal) + '</td>';
                    str += '<tr><th>Id Cta</th><th>Cuenta</th><th colspan="2">Importe Linea</th></tr>';
                }
                let DebeHaber = 'D';
                if (item.DH == 1) {
                    str += '<tr><td>' + item.IdCuenta + '</td><td>' + item.Cuenta + '</td><td>' + separadorMiles(item.ImporteLinea) + '</td><td></td></tr>';

                } else {
                    str += '<tr><td>' + item.IdCuenta + '</td><td>' + item.Cuenta + '</td><td></td><td>' + separadorMiles(item.ImporteLinea) + '</td></tr>';
                }
                iAsiento++;
            }
            // Version 1
            //while (iAsiento <= Lista.length - 1) {
            //    let item = Lista[iAsiento];
            //    if (IdAsiento != item.IdAsiento) {
            //        IdAsiento = item.IdAsiento;
            //        // Cabecera
            //        str += '<tr class="bg-info"><th>Asiento</th><th colspan="2">Fecha</th><th>Importe Asiento</th></tr>';
            //        str += '<tr><td>' + Right('0000000' + item.IdAsiento, 7) + '</td><td colspan="2">' + item.Fecha + '</td><td>' + separadorMiles(item.ImporteTotal) + '</td></tr>';
            //        str += '<tr><th>Id Cta</th><th>DH</th><th>Cuenta</th><th>Importe Linea</th></tr>';

            //    }
            //    let DebeHaber = 'D';
            //    if (item.DH == 1) {
            //        DebeHaber = 'H';
            //     }
            //    str += '<tr><td>' + item.IdCuenta + '</td><td>' + DebeHaber + '</td><td>' + item.Cuenta + '</td><td>' + separadorMiles(item.ImporteLinea)  + '</td></tr>';
            //    iAsiento++;
            //}
            str += '</table>'

        }
        return $("#" + div + "").html(str);
    }


}
class Asiento extends DBE {
    constructor() {
        super();
        this.IdEntidad = 0;
        this.Fecha = '';
        this.Importe = 0;

        this.ListaLineas;
    }

    async StrFecha() {
        let Result = '';
        if (this.Fecha > 0) {
            Result = LongToDateString(this.Fecha);
        }
        return Result;
    }
    async Alta(ListaLineas) {
        try {
            let ObjU = JSON.parse(sessionStorage.getItem("User"));
            this.IdUsuarioAlta = ObjU.IdEntidad;
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsAsiento + "/Alta", data);
            if (id !== undefined)
                this.IdEntidad = id;
            let i = 0;
            while (i <= ListaLineas?.length - 1) {
                let Linea = new AsientoLinea;
                Linea.IdUsuarioAlta = this.IdUsuarioAlta;
                Linea.IdAsiento = this.IdEntidad;
                Linea.IdCuentaContable = ListaLineas[i].IdCuentaContable;
                Linea.TipoDH = ListaLineas[i].TipoDH;
                Linea.Importe = ListaLineas[i].Importe;
                await Linea.Alta();
                i++;
            }
        } catch (e) {
            throw e;
        }
    }
    static async TraerTodosXCuenta(IdCuenta) {
        let data = {
            'IdCuenta': IdCuenta
        };
        let lista = await ejecutarAsync(urlWsAsiento + "/TraerTodosXCuenta", data);
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadAsiento(value));
            });
        }
        return result;
    }


    static async ArmarGrilla(lista, div) {
        $("#" + div + "").html('');
        let str = "";
        if (lista?.length > 0) {
            for (let objAsiento of lista) {
                str += '<div class="row bg-primary text-light">';
                str += '	<div class="col-7 pl-3">';
                str += '		<span>' + await objAsiento.StrFecha() + '</span>';
                str += '	</div>';
                str += '	<div class="col-4 text-right">';
                str += '		<span>' + separadorMiles(objAsiento.Importe.toFixed(2)) + '</span>';
                str += '	</div>';
                str += '</div>';
                str += '<div class="row mb-2">';
                str += '	<div class="col-12">';
                for (let Objlinea of objAsiento.ListaLineas) {
                    str += '		<div class="row">';
                    let ObjCuentaContable = await CuentaContable.TraerUno(Objlinea.IdCuentaContable);
                    if (Objlinea.TipoDH == 0) {
                        str += '			<div class="col-9">';
                        str += '				<span class="text-light">' + ObjCuentaContable.Nombre + '</span>';
                        str += '			</div>';
                    } else {
                        str += '			<div class="col-1">';
                        str += '				<span class="text-light">A</span>';
                        str += '			</div>';
                        str += '			<div class="col-8">';
                        str += '				<span class="text-light">' + ObjCuentaContable.Nombre + '</span>';
                        str += '			</div>';
                    }
                    str += '			<div class="col-3">';
                    str += '				<span class="text-light">' + separadorMiles(Objlinea.Importe.toFixed(2)) + '</span>';
                    str += '			</div>';
                    str += '		</div>';
                }
                str += '	</div>';
                str += '</div>';
            }
        }
        return $("#" + div + "").html(str);
    }

}
function LlenarEntidadAsiento(entidad) {
    let Res = new Asiento;
    Res.IdUsuarioAlta = entidad.IdUsuarioAlta;
    Res.IdUsuarioBaja = entidad.IdUsuarioBaja;
    Res.IdUsuarioModifica = entidad.IdUsuarioModifica;
    Res.FechaAlta = entidad.FechaAlta;
    Res.FechaBaja = entidad.FechaBaja;
    Res.IdMotivoBaja = entidad.IdMotivoBaja;
    Res.IdEntidad = entidad.IdEntidad;
    Res.Fecha = entidad.Fecha;
    Res.Importe = entidad.Importe;
    Res.ListaLineas = entidad.ListaLineas;
    return Res;
}
function LlenarEntidadAsientoImpresion(entidad) {
    let Res = new AsientoImpresion;
    Res.IdAsiento = entidad.IdAsiento;
    Res.Fecha = entidad.Fecha;
    Res.ImporteTotal = entidad.ImporteTotal;
    Res.IdCuenta = entidad.IdCuenta;
    Res.Cuenta = entidad.Cuenta;
    Res.DH = entidad.DH;
    Res.ImporteLinea = entidad.ImporteLinea;
    return Res;
}
