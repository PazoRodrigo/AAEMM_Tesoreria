var _Lista_Empresa;
var _IdCentroCosto;

class Empresa extends DBE {
    constructor() {
        super();
        this.IdEntidad = 0;
        this.RazonSocial = '';
        this.CUIT = 0;
        this.CorreoElectronico = '';
        this.IdEstado = 0;

        this.Domicilio;
    }

    // ABM
    async Alta() {
        await this.ValidarCamposEmpresa();
        this.Nombre = this.Nombre.toUpperCase();
        this.Observaciones = this.Observaciones.toUpperCase();
        try {
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsEmpresa + "/Alta", data);
            if (id !== undefined)
                this.IdEntidad = id;
            _Lista_Empresa.push(this);
            return;
        } catch (e) {
            throw e;
        }
    }
    async Modifica() {
        await this.ValidarCamposEmpresa();
        this.Nombre = this.Nombre.toUpperCase();
        this.Observaciones = this.Observaciones.toUpperCase();
        try {
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsEmpresa + "/Modifica", data);
            if (id !== undefined)
                this.IdEntidad = id;
            let buscados = $.grep(_Lista_Empresa, function (entidad, index) {
                return entidad.IdEntidad !== id;
            });
            _Lista_Empresa = buscados;
            this.IdEstado = 0;
            _Lista_Empresa.push(this);
            return;
        } catch (e) {
            throw e;
        }
    }
    async Baja() {
        try {
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsEmpresa + "/Baja", data);
            if (id !== undefined)
                this.IdEntidad = id;
            let buscados = $.grep(_Lista_Empresa, function (entidad, index) {
                return entidad.IdEntidad !== id;
            });
            _Lista_Empresa = buscados;
            this.IdEstado = 1;
            _Lista_Empresa.push(this);
            return;
        } catch (e) {
            throw e;
        }
    }

    async ValidarCamposEmpresa() {
        let sError = '';
        if (this.Nombre.length === 0) {
            sError += 'Debe ingresar el Nombre';
        }
        if (sError !== '') {
            throw '<b> Error de grabación </b> <br/><br/>' + sError;
        }
    }

    // Todos
    static async Todos() {
        console.log('_Lista_Empresa');
        if (_Lista_Empresa === undefined) {
            _Lista_Empresa = await Empresa.TraerTodas();
        }
        return _Lista_Empresa;
    }

    // Traer
    static async TraerUno(IdEntidad) {
        _Lista_Empresa = await Empresa.TraerTodos();
        let buscado = $.grep(_Lista_Empresa, function (entidad, index) {
            return entidad.IdEntidad === IdEntidad;
        });
        let Encontrado = buscado[0];
        return Encontrado;
    }
    static async TraerTodos() {
        return await Empresa.Todos();
    }
    static async TraerTodosActivos() {
        _Lista_Empresa = await Empresa.TraerTodos();
        let buscado = $.grep(_Lista_Empresa, function (entidad, index) {
            return entidad.IdEstado === 0;
        });
        return buscado;
    }
    static async TraerTodas() {
        let lista = await ejecutarAsync(urlWsEmpresa + "/TraerTodos");
        _Lista_Empresa = [];
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadEmpresa(value));
            });
        }
        _Lista_Empresa = result;
        return _Lista_Empresa;
    }
    static async TraerTodasXCUIT(CUIT) {
        let data = {
            'CUIT': CUIT
        };
        let lista = await ejecutarAsync(urlWsEmpresa + "/TraerTodosXCUIT", data);
        _Lista_Empresa = [];
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadEmpresa(value));
            });
        }
        _Lista_Empresa = result;
        return _Lista_Empresa;
    }
    static async TraerTodasXRazonSocial(RazonSocial) {
        let data = {
            'RazonSocial': RazonSocial
        };
        let lista = await ejecutarAsync(urlWsEmpresa + "/TraerTodosXRazonSocial", data);
        _Lista_Empresa = [];
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadEmpresa(value));
            });
        }
        _Lista_Empresa = result;
        return _Lista_Empresa;
    }
    static async TraerTodasXCentroCosto(IdCentroCosto) {
        let data = {
            'IdCentroCosto': IdCentroCosto
        };
        let lista = await ejecutarAsync(urlWsEmpresa + "/TraerTodosXCentroCosto", data);
        _Lista_Empresa = [];
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadEmpresa(value));
            });
        }
        _Lista_Empresa = result;
        return _Lista_Empresa;
    }
    // Otros
    static async Refresh() {
        _Lista_Empresa = await Empresa.TraerTodas();
    }
    // Herramientas
    static async ArmarGrillaSinEliminar(lista, div, eventoSeleccion, estilo) {
        $('#' + div + '').html('');
        let str = '';
        if (lista.length > 0) {
            str += '<div style="' + estilo + '">';
            str += '    <ul class="ListaGrilla">';
            let estiloItem = '';
            for (let item of lista) {
                estiloItem = 'LinkListaGrillaObjeto';
                if (item.IdEstado === 1) {
                    estiloItem = 'LinkListaGrillaObjetoEliminado';
                }
                let aItem = '<a href="#" class="mibtn-seleccionEmpresa" data-Evento="' + eventoSeleccion + '" data-Id="' + item.IdEntidad + '">' + item.RazonSocial + '</a>';
                str += String.format('<li><div class="LinkListaGrilla ' + estiloItem + '">{0}</div></li>', aItem);
            }
            str += '    </ul>';
            str += '</div>';
        }
        return $('#' + div + '').html(str);
    }
    static async ArmarGrilla(lista, div, eventoSeleccion, eventoEliminar, estilo) {
        $('#' + div + '').html('');
        let str = '';
        console.log(lista);
        if (lista.length > 0) {
            str += '<div style="' + estilo + '">';
            str += '    <ul class="ListaGrilla">';
            let estiloItem = '';
            for (let item of lista) {
                estiloItem = 'LinkListaGrillaObjeto';
                if (item.IdEstado === 1) {
                    estiloItem = 'LinkListaGrillaObjetoEliminado';
                }
                let aItem = '<a href="#" class="mibtn-seleccionEmpresa" data-Evento="' + eventoSeleccion + '" data-Id="' + item.IdEntidad + '">' + item.RazonSocial + '</a>';
                let aEliminar = '<a href="#" class="mibtn-EliminarEmpresa" data-Evento="' + eventoEliminar + '" data-Id="' + item.IdEntidad + '"><span class="icon-bin"></span></a>';
                str += String.format('<li><div class="LinkListaGrilla ' + estiloItem + '">{0}</div><div class="LinkListaGrilla LinkListaGrillaElimina">{1}</div></li>', aItem, aEliminar);
            }
            str += '    </ul>';
            str += '</div>';
        }
        return $('#' + div + '').html(str);
    }
    static async ArmarRadios(lista, div, evento, estilo) {
        $('#' + div + '').html('');
        let str = '';
        await Area.Refresh();
        if (lista.length > 0) {
            str += '<div style="' + estilo + '">';
            str += '    <table class="table table-bordered" style="width: 70%;">';
            str += '        <thead>';
            str += '            <tr>';
            str += '                <th colspan="2" style="text-align: center;">Areas</th>';
            str += '            </tr>';
            str += '        </thead>';
            str += '        <tbody>';
            for (let item of lista) {
                let radioSeleccion = '<input type="radio" class="mibtn-seleccionArea"  name="rblArea" data-Evento="' + evento + '" data-Id="' + item.IdEntidad + '" value="' + item.IdEntidad + '">';
                str += String.format('<tr><td align="center" valign="middle" style="width: 5%;">{0}</td><td align="left">{1}</td></tr>', radioSeleccion, item.Nombre);
            }
            str += '        </tbody>';
            str += '    </table>';
            str += '</div>';
        }
        return $('#' + div + '').html(str);
    }
    static async ArmarCheckBoxs(lista, div, evento, estilo) {
        $('#' + div + '').html('');
        let str = '';
        str += '<div style="' + estilo + '">';
        await Area.Refresh();
        if (lista.length > 0) {
            for (let item of lista) {
                str += '<div class="col-lg-4"><input type="checkbox" class="micbx-Area" name="CkbList_Areas" value="' + item.IdEntidad + '"    id="chk_' + item.IdEntidad + '" /><label for="chk_' + item.IdEntidad + '"> ' + item.Nombre + '</label></div>';
            }
        }
        str += '</div>';
        return $('#' + div + '').html(str);
    }
    static async armarUC() {
        $("#grilla").html("");
        let control = "";
        if ($("#Modal-PopUpEmpresa").length == 0) {
            control += '<div id="Modal-PopUpEmpresa" class="modal" tabindex="-1" role="dialog" >';
            control += '    <div class="modal-dialog modal-lg">';
            control += '        <div class="modal-content">';
            control += '            <div class="modal-header">';
            control += '                <h2 class="modal-title">Buscador de Empresas</h2>';
            control += '            </div>';
            control += '            <div class="row">';
            control += '                <div class="modal-body">';
            control += '                    <div class="container col-md-12">';
            control += '                        <div class="row">';
            control += '                            <div class="col-md-2">';
            control += '                                <h6> CUIT </h6>';
            control += '                            </div>';
            control += '                            <div class="col-md-5">';
            control += '                                <input class="form-control input-sm" maxlength="11" style="width:160px" id="txtBuscaCUIT" type="text" placeholder="CUIT (11 números)" onkeypress="return jsSoloNumeros(event);" maxlength="10" autocomplete="off"/>';
            control += '                           </div>';
            control += '                        </div>';
            control += '                        <div class="row mt-1">';
            control += '                            <div class="col-md-2">';
            control += '                                <h6> Razón Social </h6>';
            control += '                            </div>';
            control += '                            <div class="col-md-9">';
            control += '                                <input class="form-control input-sm txtAfi" id="txtRazonSocial" type="text" placeholder="Razón Social" autocomplete="off"/>';
            control += '                            </div>';
            control += '                        </div>';
            control += '                        <div class="row mt-1">';
            control += '                            <div class="col-md-2">';
            control += '                                <h6> C. de C.</h6>';
            control += '                            </div>';
            control += '                            <div class="col-md-8">';
            control += '                                <div id="CboBuscadorCentroCosto"></div>';
            control += '                            </div>';
            control += '                        </div>';
            control += '                        <div class="row mt-2">';
            control += '                            <div class="col-md-9"></div>';
            control += '                            <div class="col-md-3">';
            control += '                                <div class="Boton BtnBuscar">';
            control += '                                    <a id="LinkBtnBuscar" href="#"><span>Buscar Empresa</span></a>';
            control += '                                </div>';
            control += '                            </div>';
            control += '                        </div>';
            control += '                        <div class="row mt-2">';
            control += '                            <div class="col-md-12">';
            control += '                                <div id="grillaBuscador" style="height: 180px;overflow-y: scroll;"></div>';
            control += '                            </div>';
            control += '                        </div>';
            control += '                    </div>';
            control += '                </div>';
            control += '            </div>';
            control += '            <div class="modal-footer">';
            control += '                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>';
            control += '            </div>';
            control += '        </div>';
            control += '    </div>';
            control += '</div>';
            $("body").append(control);
        }
        let lista = await CentroCosto.TraerTodosActivos();
        await CentroCosto.ArmarCombo(lista, 'CboBuscadorCentroCosto', 'SelectorBuscadorCentroCosto', 'EventoBuscadorCentroCosto', 'Centro de Costo', 'CboBuscadorCC')
        _IdCentroCosto = 0;
        $("#Modal-PopUpEmpresa").modal('show');
        $("#txtBuscaCUIT").focus();
    }
}
function LlenarEntidadEmpresa(entidad) {
    let Res = new Empresa;
    Res.IdUsuarioAlta = entidad.IdUsuarioAlta;
    Res.IdUsuarioBaja = entidad.IdUsuarioBaja;
    Res.IdUsuarioModifica = entidad.IdUsuarioModifica;
    Res.FechaAlta = entidad.FechaAlta;
    Res.FechaBaja = entidad.FechaBaja;
    Res.IdMotivoBaja = entidad.IdMotivoBaja;
    Res.IdEntidad = entidad.IdEntidad;
    Res.RazonSocial = entidad.RazonSocial;
    Res.CUIT = entidad.CUIT;
    Res.CorreoElectronico = entidad.CorreoElectronico;
    Res.IdEstado = entidad.IdEstado;
    Res.Domicilio = LlenarEntidadDomicilio(entidad.Domicilio);
    return Res;
}

document.addEventListener('EventoBuscadorCentroCosto', async function (e) {
    try {
        let objSeleccionado = e.detail;
        console.log(objSeleccionado);
        _IdCentroCosto = objSeleccionado.IdEntidad;
        $("#SelectorBuscadorCentroCosto").text(objSeleccionado.Nombre);
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
}, false);
$('body').on('click', '#LinkBtnBuscar', async function (e) {
    try {
        spinner()
        let buscaCUIT = $("#txtBuscaCUIT").val();
        let buscaRazonSocial = $("#txtRazonSocial").val();
        let TipoBuscador = '';
        if (parseInt(buscaCUIT.length) === 11 || parseInt(buscaRazonSocial.length) > 3 || parseInt(_IdCentroCosto) > 0) {
            if (parseInt(buscaCUIT.length) === 11) {
                TipoBuscador = 'xCUIT';
                await LlenarGrillaBuscador(TipoBuscador);
            } else {
                if (parseInt(buscaRazonSocial.length) > 3) {
                    TipoBuscador = 'xRazonSocial';
                    await LlenarGrillaBuscador(TipoBuscador);
                } else {
                    if (parseInt(_IdCentroCosto) > 0) {
                        TipoBuscador = 'xIdCentroCosto';
                        await LlenarGrillaBuscador(TipoBuscador);
                    }
                }
            }
        }
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
async function LlenarGrillaBuscador(TipoBuscador) {
    _Lista_Empresa = [];
    switch (TipoBuscador) {
        case 'xCUIT':
            _Lista_Empresa = await Empresa.TraerTodasXCUIT($("#txtBuscaCUIT").val());
            break;
        case 'xRazonSocial':
            _Lista_Empresa = await Empresa.TraerTodasXRazonSocial($("#txtRazonSocial").val());
            break;
        case 'xIdCentroCosto':
            _Lista_Empresa = await Empresa.TraerTodasXCentroCosto(_IdCentroCosto);
            break;
        default:
    }
    Empresa.ArmarGrillaSinEliminar(_Lista_Empresa, 'grillaBuscador', 'EventoSeleccionarEmpresa', '');
}

$('body').on('click', ".mibtn-seleccionEmpresa", async function () {
    try {
        $this = $(this);
        let buscado = $.grep(_Lista_Empresa, function (entidad, index) {
            return entidad.IdEntidad === parseInt($this.attr("data-Id"));
        });
        let Seleccionado = buscado[0];
        let evento = $this.attr("data-Evento");
        $("#Modal-PopUpEmpresa").modal('hide');
        let event = new CustomEvent(evento, { detail: Seleccionado });
        document.dispatchEvent(event);
    } catch (e) {
        alertAlerta(e);
    }
});
$('body').on('click', ".mibtn-EliminarEmpresa", async function () {
    try {
        $this = $(this);
        let buscado = $.grep(_Lista_Empresa, function (entidad, index) {
            return entidad.IdEntidad === parseInt($this.attr("data-Id"));
        });
        let Seleccionado = buscado[0];
        let evento = $this.attr("data-Evento");
        let event = new CustomEvent(evento, { detail: Seleccionado });
        document.dispatchEvent(event);
    } catch (e) {
        alertAlerta(e);
    }
});

