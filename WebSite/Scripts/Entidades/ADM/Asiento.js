
class Asiento extends DBE {
    constructor() {
        super();
        this.IdEntidad = 0;
        this.Fecha = '';
        this.Importe = '';

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
                Linea.IdCuentaCorriente = ListaLineas[i].IdCuentaCorriente;
                Linea.TipoDH = ListaLineas[i].TipoDH;
                Linea.Importe = ListaLineas[i].Importe;
                await Linea.Alta();
                i++;
            }
        } catch (e) {
            throw e;
        }
    }
}
