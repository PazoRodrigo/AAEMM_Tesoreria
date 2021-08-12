var _Lista_Usuarios;
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

        this.ListaPerfiles;
        this.ListaPermisos;
    }

    // Todos
    static async Todos() {
        if (_Lista_Usuarios === undefined) {
            _Lista_Usuarios = await Usuario.TraerTodos();
        }
        return _Lista_Usuarios;
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
    static async UsuarioLogueado() {
        let ObjU = JSON.parse(sessionStorage.getItem("User"));
        return ObjU;
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

    static async TraerTodos() {
        let lista = await ejecutarAsync(urlWsUsuario + "/TraerTodos");
        _Lista_Usuarios = [];
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadUsuario(value));
            });
        }
        _Lista_Usuarios = result;
        return result;
    }
    static async TraerUno(IdUsuario) {
        let buscado = $.grep(await Usuario.Todos(), function (entidad, index) {
            return entidad.IdEntidad == IdUsuario;
        });
        return buscado[0];
    }
    static async TraerTodosXPerfil(IdPerfil) {
        let data = {
            'IdPerfil': IdPerfil
        };
        let lista = await ejecutarAsync(urlWsUsuario + "/TraerTodosXPerfil", data);
        _Lista_Usuarios = [];
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadUsuario(value));
            });
        }
        return result;
    }
    static async ArmarCombo(lista, div, IdSelect, selector, evento, estilo) {
        let Cbo = '';
        Cbo += '<select id="_CboUsuario"  class="' + estilo + '" onchange="SeleccionUsuario(this);" data-Evento="' + evento + '">';
        Cbo += '    <option value="0" >' + selector + '</option>';
        for (let item of lista) {
            Cbo += '<option value="' + item.IdEntidad + '" >' + item.Nombre + '</option>';
        };
        Cbo += '</select>';
        return $('#' + div + '').html(Cbo);
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

    res.ListaPerfiles = entidad.ListaPerfiles;
    return res;
}
async function SeleccionUsuario() {
    try {
        let elemento = document.getElementById("_CboUsuario");
        let buscado = $.grep(_Lista_Usuarios, function (entidad, index) {
            return entidad.IdEntidad == elemento.options[elemento.selectedIndex].value;
        });
        let Seleccionado = buscado[0];
        let evento = elemento.getAttribute('data-Evento');
        let event = new CustomEvent(evento, { detail: Seleccionado });
        document.dispatchEvent(event);
    } catch (e) {
        alertAlerta(e);
    }
}
