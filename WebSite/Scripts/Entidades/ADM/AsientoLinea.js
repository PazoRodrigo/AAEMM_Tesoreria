class AsientoLinea extends DBE {
    constructor() {
        super();
        this.IdEntidad = 0;
        this.IdAsiento = 0;
        this.IdCuentaCorriente = 0;
        this.TipoDH = 0;
        this.Importe = 0;

        this._ObjTipoCuentaCorriente;
    }


    async ObjCuentaCorriente() {
        try {
            if (this._ObjTipoCuentaCorriente == undefined) {
                this._ObjTipoCuentaCorriente = await CuentaCorriente.TraerUno(this.IdCuentaCorriente);
            }
            return this._ObjTipoCuentaCorriente;
        } catch (error) {
            return new CuentaCorriente;
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
}