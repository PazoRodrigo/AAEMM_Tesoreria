
class Domicilio {
    constructor() {
        this.Direccion = '';
        this.CodigoPostal = 0;
        this.IdLocalidad = 0;

        this._ObjLocalidad;
    }
    // Todos
    async ObjLocalidad() {
        if (_ObjLocalidad === undefined) {
            _ObjLocalidad = await Localidad.TraerUno(this.IdLocalidad);
        }
        return _ObjLocalidad;
    }
}
function LlenarEntidadDomicilio(entidad) {
    let Res = new Domicilio;
    Res.Direccion = entidad.Direccion;
    Res.CodigoPostal = entidad.CodigoPostal;
    Res.IdLocalidad = entidad.IdLocalidad;
    return Res;
}
