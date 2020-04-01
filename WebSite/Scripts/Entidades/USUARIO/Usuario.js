
class Usuario extends DBE {
    constructor() {
        super();
        this.IdEntidad = 0;
        this.Nombre = "";
        this.Apellido = "";
        this.UserName = "";
        this.Password = "";
        this.CorreoElectronico = "";
        this.NroInterno = 0;
        this.AccesosSistema = 0;
        this.Observaciones = "";
    }

    async Alta() {
        try {
            let ObjU = JSON.parse(sessionStorage.getItem("User"));
            let data = {
                'IdUsuario': ObjU.IdEntidad,
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsUsuario + "/Alta", data);
            if (id !== undefined)
                this.IdEntidad = id;
            return;
        } catch (e) {
            throw e;
        }
    }
    async Baja() {
        try {
            let ObjU = JSON.parse(sessionStorage.getItem("User"));
            let data = {
                'IdUsuario': ObjU.IdEntidad,
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsUsuario + "/Baja", data);
            if (id !== undefined)
                this.IdEntidad = id;
            return;
        } catch (e) {
            throw e;
        }
    }
    async Modifica() {
        try {
            let ObjU = JSON.parse(sessionStorage.getItem("User"));
            let data = {
                'IdUsuario': ObjU.IdEntidad,
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsUsuario + "/Modifica", data);
            if (id !== undefined)
                this.IdEntidad = id;
            return;
        } catch (e) {
            throw e;
        }
    }
    async ModificaPassword(anterior, nueva) {
        try {
            let ObjU = JSON.parse(sessionStorage.getItem("User"));
            let data = {
                "IdUsuario": ObjU.IdEntidad,
                "anterior": anterior,
                "nueva": nueva
            };
            await ejecutarAsync(urlWsUsuario + "/ModificaPassword", data);
        } catch (e) {
            throw e;
        }
    }
    static async AccederAlSistema(user, pass) {
        let data = {
            "User": user,
            'Pass': pass
        };
        let lista = await ejecutarAsync(urlWsUsuario + "/AccederAlSistema", data);
        let result = [];
        if (parseInt(lista.length) === 1) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadUsuario(value));
            });
        } 
        return result[0];
    }
    static async EnviarPassword(Identificador) {
        let data = {
            'Identificador': Identificador
        };
        await ejecutarAsync(urlWsUsuario + "/EnviarPassword", data);
    }
}
function LlenarEntidadUsuario(entidad) {
    let res = new Usuario;
    res.IdEntidad = entidad.IdEntidad;
    res.Nombre = entidad.Nombre;
    res.Apellido = entidad.Apellido;
    res.UserName = entidad.UserName;
    res.CorreoElectronico = entidad.CorreoElectronico;
    res.NroInterno = entidad.NroInterno;
    res.AccesosSistema = entidad.AccesosSistema;
    res.Observaciones = entidad.Observaciones;
    return res;
}
