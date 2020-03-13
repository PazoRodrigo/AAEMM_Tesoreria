var _ListaLocalidades;

class Localidad {
    constructor() {
        this.IdEntidad = 0;
        this.Nombre = '';
        this.CP = 0;
        this.IdSeccional = 0;
        this.IdProvincia = 0;

        this._ObjSeccional;
        this._ObjProvincia;
    }

    async ObjProvincia() {
        try {
            if (this._ObjProvincia === undefined) {
                this._ObjProvincia = await Provincia.TraerUna(this.IdProvincia);
            }
            return this._ObjProvincia;
        } catch (e) {
            return new Provincia();
        }
    }
    static async TraerUna(IdEntidad) {
        let data = {
            "IdLocalidad": IdEntidad
        };
        let entidad = 'localidades';
        let metodo = 'TraerUna';
        let url = ApiURL + '/' + entidad + '/' + metodo;
        let datos = await TraerAPI(url, data);
        let result = [];
        $.each(datos, function (key, value) {
            result.push(llenarEntidadLocalidad(value));
        });
        return result[0];
    }
    static async TraerTodasXCP(CodigoPostal) {
        let data = {
            "CP": CodigoPostal
        };
        let entidad = 'localidades';
        let metodo = 'TraerTodasXCP';
        let url = ApiURL + '/' + entidad + '/' + metodo;
        let lista = await TraerAPI(url, data);
        _ListaLocalidades = [];
        let result = [];
        $.each(lista, function (key, value) {
            result.push(llenarEntidadLocalidad(value));
        });
        _ListaLocalidades = result;
        return _ListaLocalidades;
    }
    static ArmarUCBuscarLocalidad(CP_Buscado) {
        $("#grilla").html('');
        if (parseInt($("#Modal-PopUpLocalidad").length) === 0) {
            let control = '';
            control += '<div id="Modal-PopUpLocalidad" class="modal" tabindex="-1" role="dialog" >';
            control += '    <div class="modal-dialog modal-lg">';
            control += '        <div class="modal-content">';
            control += '            <div class="modal-header HeaderPopUp">';
            control += '                <h2 class="modal-title">Buscar Localidad.  CP: <span id="spanCPBuscador" class="spanEncabezado"></span></h2>';
            control += '            </div>';
            control += '            <div class="modal-body">';
            control += '                <div class="col-md-2"></div>';
            control += '                <div class="col-md-8"><div id="DivBuscadorLocalidad"></div>';
            control += '                <div class="col-md-2"></div>';
            control += '            </div>';
            control += '            <div class="modal-footer">';
            control += '                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>';
            control += '            </div>';
            control += '        </div>';
            control += '    </div>';
            control += '</div>';
            $("body").append(control);
        }
        $("#spanCPBuscador").text(CP_Buscado);
        $('#Modal-PopUpLocalidad').modal({ show: true });
    }
    static async ArmarCombo(lista, div, selector, evento, TipoDomicilio) {
        _ListaLocalidades = lista;
        let cbo = "";
        cbo += '<div id="CboLocalidad" class="dropdown">';
        cbo += '<button id="' + selector + '" class="btn btn-primary dropdown-toggle btn-md btn-block" type="button" data-toggle="dropdown">';
        cbo += 'Localidad <span class="caret"></span>';
        cbo += '</button>';
        cbo += '<ul class="dropdown-menu">';
        $(lista).each(function () {
            cbo += '<li><a href="#" class="mibtn-seleccionLocalidad" data-Id="' + this.IdEntidad + '" data-Nombre="' + this.Nombre + '" data-Evento="' + evento + '" data-TipoDomicilio="' + TipoDomicilio + '" > ' + this.Nombre + '</a></li>';
        });
        cbo += '</ul>';
        cbo += '</div>';
        return $('#' + div + '').html(cbo);
    }
}
function llenarEntidadLocalidad(entidad) {
    console.log(entidad);
    let obj = new Localidad;
    obj.IdEntidad = entidad.IdEntidad;
    obj.Nombre = entidad.Descripcion;
    obj.CP = entidad.CP;
    obj.IdSeccional = entidad.IdSeccional;
    obj.IdProvincia = entidad.IdProvincia;
    return obj;
}
$('body').on('click', ".mibtn-seleccionLocalidad", async function () {
    try {
        spinner();
        $this = $(this);
        let buscado = $.grep(_ListaLocalidades, function (entidad, index) {
            return entidad.IdEntidad === parseInt($this.attr("data-Id"));
        });
        $("#Modal-PopUpLocalidad").modal("hide");
        let Seleccionado = buscado[0];
        let evento = $this.attr("data-Evento");
        Seleccionado.TipoDomicilio = $this.attr("data-TipoDomicilio");
        let event = new CustomEvent(evento, { detail: Seleccionado });
        document.dispatchEvent(event);
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
