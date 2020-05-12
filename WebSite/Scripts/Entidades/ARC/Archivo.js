
class Archivo {
    constructor() {
        this.NombreArchivo = '';
        this.IdTipo = 0;
    }

    static async TraerTodas() {
        let lista = await ejecutarAsync(urlWsCentroCosto + "/TraerTodos");
        _Lista_CentroCosto = [];
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadCentroCosto(value));
            });
        }
        _Lista_CentroCosto = result;
        return _Lista_CentroCosto;
    }

}
function LlenarEntidadArchivo(entidad) {
    let Res = new Archivo;
    Res.IdUsuarioAlta = entidad.IdUsuarioAlta;
    Res.IdUsuarioBaja = entidad.IdUsuarioBaja;
    Res.IdUsuarioModifica = entidad.IdUsuarioModifica;
    Res.FechaAlta = entidad.FechaAlta;
    Res.FechaBaja = entidad.FechaBaja;
    Res.IdMotivoBaja = entidad.IdMotivoBaja;
    Res.IdEntidad = entidad.IdEntidad;
    Res.NombreArchivo = entidad.NombreArchivo;
    Res.IdTipo = entidad.IdTipo;
    return Res;
}