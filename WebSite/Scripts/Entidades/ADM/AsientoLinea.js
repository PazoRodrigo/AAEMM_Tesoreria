class AsientoLinea extends DBE {
    constructor() {
        super();
        this.IdEntidad = 0;
        this.IdAsiento = 0;
        this.IdCuentaContable = 0;
        this.TipoDH = 0;
        this.Importe = 0;

        this._ObjTipoCuentaContable;
    }


    async ObjCuentaContable() {
        try {
                console.log(_ObjTipoCuentaContable, this.IdCuentaContable);
            if (this._ObjTipoCuentaContable == undefined) {
                console.log(_ObjTipoCuentaContable, this.IdCuentaContable);
                this._ObjTipoCuentaContable = await CuentaContable.TraerUno(this.IdCuentaContable);
            }
            return this._ObjTipoCuentaContable;
        } catch (error) {
            return new CuentaContable;
        }
    }
    async Alta() {
        try {
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsAsientoLinea + "/Alta", data);
            if (id !== undefined)
                this.IdEntidad = id;
        } catch (e) {
            throw e;
        }
    }

    static async ArmarAsiento(div, Lineas, SumaD, SumaH) {
        $("#DivGrillaLineasAsiento").html('');
        let str = '';
        if (_ListaAsientoLineas.length > 0) {
            let i = 0;
            let borrar = 1;
            while (i <= _ListaAsientoLineas.length - 1) {
                str += '<div class="row border-light border-bottom">';
                let valorLinea = parseFloat(_ListaAsientoLineas[i].Importe);
                let ObjCuentaCorriente = await _ListaAsientoLineas[i].ObjCuentaCorriente();
                let textoLinea = ObjCuentaCorriente.Nombre;

                str += '<a href="#"class="LinkBorrarLinea col-1 bg-danger text-light" data-IdRegistro="' + i + '" ><span class="icon-bin"></span></a>';
                switch (parseInt(_ListaAsientoLineas[i].TipoDH)) {
                    case 0:

                        str += '<div class="col-7 text-light pl-3"><h5>' + textoLinea + '</h5></div>';
                        str += '<div class="col-4 text-right text-light pr-1">' + separadorMiles(valorLinea.toFixed(2)) + '</div>';
                        SumaD += valorLinea;
                        break;
                    case 1:
                        str += '<div class="col-1 text-light text-center"><h5> A </h5></div>';
                        str += '<div class="col-6 text-light pl-4"><h5>' + textoLinea + '</h5></div>';
                        str += '<div class="col-4 text-right text-light pr-1">' + separadorMiles(valorLinea.toFixed(2)) + '</div>';
                        SumaH += valorLinea;
                        break;
                    default:
                }
                i++;
                borrar++;
                str += '</div>';
            }
        }
        await MostrarImportesTotalAsiento(SumaD, SumaH);
        $("#DivGrillaLineasAsiento").html(str);
    }
}