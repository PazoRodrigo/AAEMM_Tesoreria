var _Lista_Empleado;

class Empleado extends DBE {
    constructor() {
        super();
        this.IdEntidad = 0;
        this.Nombre = '';
        this.Observaciones = '';
    }

    // ABM
    async Alta() {
        await this.ValidarCamposEmpleado();
        this.Nombre = this.Nombre.toUpperCase();
        this.Observaciones = this.Observaciones.toUpperCase();
        try {
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsEmpleado + "/Alta", data);
            if (id !== undefined)
                this.IdEntidad = id;
            _Lista_Empleado.push(this);
            return;
        } catch (e) {
            throw e;
        }
    }
    async Modifica() {
        await this.ValidarCamposEmpleado();
        this.Nombre = this.Nombre.toUpperCase();
        this.Observaciones = this.Observaciones.toUpperCase();
        try {
            let data = {
                'entidad': this
            };
            let id = await ejecutarAsync(urlWsEmpleado + "/Modifica", data);
            if (id !== undefined)
                this.IdEntidad = id;
            let buscados = $.grep(_Lista_Empleado, function (entidad, index) {
                return entidad.IdEntidad !== id;
            });
            _Lista_Empleado = buscados;
            this.IdEstado = 0;
            _Lista_Empleado.push(this);
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
            let id = await ejecutarAsync(urlWsEmpleado + "/Baja", data);
            if (id !== undefined)
                this.IdEntidad = id;
            let buscados = $.grep(_Lista_Empleado, function (entidad, index) {
                return entidad.IdEntidad !== id;
            });
            _Lista_Empleado = buscados;
            this.IdEstado = 1;
            _Lista_Empleado.push(this);
            return;
        } catch (e) {
            throw e;
        }
    }

    async ValidarCamposEmpleado() {
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
        console.log('_Lista_Empleado');
        if (_Lista_Empleado === undefined) {
            _Lista_Empleado = await Empleado.TraerTodas();
        }
        return _Lista_Empleado;
    }

    // Traer
    static async TraerUno(IdEntidad) {
        _Lista_Empleado = await Empleado.TraerTodos();
        let buscado = $.grep(_Lista_Empleado, function (entidad, index) {
            return entidad.IdEntidad === IdEntidad;
        });
        let Encontrado = buscado[0];
        return Encontrado;
    }
    static async TraerTodos() {
        return await Empleado.Todos();
    }
    static async TraerTodosActivos() {
        _Lista_Empleado = await Empleado.TraerTodos();
        let buscado = $.grep(_Lista_Empleado, function (entidad, index) {
            return entidad.IdEstado === 0;
        });
        return buscado;
    }
    static async TraerTodas() {
        let lista = await ejecutarAsync(urlWsEmpleado + "/TraerTodos");
        _Lista_Empleado = [];
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadEmpleado(value));
            });
        }
        _Lista_Empleado = result;
        return _Lista_Empleado;
    }
    static async TraerTodasXCUIL(CUIL) {
        let data = {
            'CUIL': CUIL
        };
        let lista = await ejecutarAsync(urlWsEmpleado + "/TraerTodosXCUIL", data);
        _Lista_Empleado = [];
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadEmpleado(value));
            });
        }
        _Lista_Empleado = result;
        return _Lista_Empleado;
    }
    static async TraerTodasXNroDocumento(NroDocumento) {
        let data = {
            'NroDocumento': NroDocumento
        };
        let lista = await ejecutarAsync(urlWsEmpleado + "/TraerTodosXNroDocumento", data);
        console.log(lista);
        alert('1000');
        _Lista_Empleado = [];
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadEmpleado(value));
            });
        }
        _Lista_Empleado = result;
        console.log(_Lista_Empleado);
        alert(11);
        return _Lista_Empleado;
    }
    static async TraerTodasXNombre(Nombre) {
        let data = {
            'Nombre': Nombre
        };
        let lista = await ejecutarAsync(urlWsEmpleado + "/TraerTodosXNombre", data);
        _Lista_Empleado = [];
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadEmpleado(value));
            });
        }
        _Lista_Empleado = result;
        return _Lista_Empleado;
    }
    // Otros
    static async Refresh() {
        _Lista_Empleado = await Empleado.TraerTodas();
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
                let aItem = '<a href="#" class="mibtn-seleccionEmpleado" data-Evento="' + eventoSeleccion + '" data-Id="' + item.IdEntidad + '">' + item.Nombre + '</a>';
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
        lista.sort(SortXNombre);
        if (lista.length > 0) {
            str += '<div style="' + estilo + '">';
            str += '    <ul class="ListaGrilla">';
            let estiloItem = '';
            for (let item of lista) {
                estiloItem = 'LinkListaGrillaObjeto';
                if (item.IdEstado === 1) {
                    estiloItem = 'LinkListaGrillaObjetoEliminado';
                }
                let aItem = '<a href="#" class="mibtn-seleccionEmpleado" data-Evento="' + eventoSeleccion + '" data-Id="' + item.IdEntidad + '">' + item.Nombre + '</a>';
                let aEliminar = '<a href="#" class="mibtn-EliminarEmpleado" data-Evento="' + eventoEliminar + '" data-Id="' + item.IdEntidad + '"><span class="icon-bin"></span></a>';
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
        if (parseInt($("#Modal-PopUpEmpleado").length) === 0) {
            control += '<div id="Modal-PopUpEmpleado" class="modal" tabindex="-1" role="dialog" >';
            control += '    <div class="modal-dialog modal-lg">';
            control += '        <div class="modal-content">';
            control += '            <div class="modal-header">';
            control += '                <h2 class="modal-title">Buscador de Empleados</h2>';
            control += '            </div>';
            control += '            <div class="row">';
            control += '                <div class="modal-body">';
            control += '                    <div class="container col-md-12">';
            control += '                        <div class="row">';
            control += '                            <div class="col-md-3">';
            control += '                                <h6> CUIL </h6>';
            control += '                            </div>';
            control += '                            <div class="col-md-5">';
            control += '                                <input class="form-control input-sm TxtBuscadores" id="txtBuscaCUIL" style="width:160px"  type="text" placeholder="CUIL (11 números)" onkeypress="return jsSoloNumeros(event);" maxlength="11" autocomplete="off"/>';
            control += '                           </div>';
            control += '                        </div>';
            control += '                        <div class="row mt-1">';
            control += '                            <div class="col-md-3">';
            control += '                                <h6> Nombre / Apellido </h6>';
            control += '                            </div>';
            control += '                            <div class="col-md-8">';
            control += '                                <input class="form-control input-sm TxtBuscadores" id="txtBuscaNombre" type="text" placeholder="Nombre / Apellido" autocomplete="off"/>';
            control += '                            </div>';
            control += '                        </div>';
            control += '                        <div class="row mt-1">';
            control += '                            <div class="col-md-3">';
            control += '                                <h6> DNI </h6>';
            control += '                            </div>';
            control += '                            <div class="col-md-9">';
            control += '                                <input class="form-control input-sm TxtBuscadores" style="width:160px" id="txtBuscaNroDocumento" type="text" placeholder="DNI (7/8 números)" onkeypress="return jsSoloNumeros(event);" maxlength="8" autocomplete="off"/>';
            control += '                            </div>';
            control += '                        </div>';
            control += '                        <div class="row mt-2">';
            control += '                            <div class="col-md-9"></div>';
            control += '                            <div class="col-md-3">';
            control += '                                <div class="Boton BtnBuscar">';
            control += '                                    <a id="LinkBtnBuscarEmpleado" href="#"><span>Buscar Empleado</span></a>';
            control += '                                </div>';
            control += '                            </div>';
            control += '                        </div>';
            control += '                        <div class="row mt-2">';
            control += '                            <div class="col-md-12">';
            control += '                                <div id="grillaBuscadorEmpleado" style="height: 180px;overflow-y: scroll;"></div>';
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
        LimpiarBuscador();
        let lista = await CentroCosto.TraerTodosActivos();
        await CentroCosto.ArmarCombo(lista, 'CboBuscadorCentroCosto', 'SelectorBuscadorCentroCosto', 'EventoBuscadorCentroCosto', 'Centro de Costo', 'CboBuscadorCC');
        _IdCentroCosto = 0;
        $("#Modal-PopUpEmpleado").modal('show');
        $("#txtBuscaCUIL").focus();
    }
}
function LlenarEntidadEmpleado(entidad) {
    let Res = new Empleado;
    Res.IdUsuarioAlta = entidad.IdUsuarioAlta;
    Res.IdUsuarioBaja = entidad.IdUsuarioBaja;
    Res.IdUsuarioModifica = entidad.IdUsuarioModifica;
    Res.FechaAlta = entidad.FechaAlta;
    Res.FechaBaja = entidad.FechaBaja;
    Res.IdMotivoBaja = entidad.IdMotivoBaja;
    Res.IdEntidad = entidad.IdEntidad;
    Res.Nombre = entidad.Nombre;
    Res.Observaciones = entidad.Observaciones;
    Res.IdEstado = entidad.IdEstado;
    return Res;
}
function LimpiarBuscador() {
    $(".TxtBuscadores").val('');
}
$('body').on('click', '#LinkBtnBuscarEmpleado', async function (e) {
    try {
        spinner();
        let buscaCUIL = $("#txtBuscaCUIL").val();
        let buscaNroDocumento = $("#txtBuscaNroDocumento").val();
        let buscaNombre = $("#txtBuscaNombre").val();
        let TipoBuscador = '';
        if (parseInt(buscaCUIL.length) === 11 || parseInt(buscaNombre.length) > 3 || (parseInt(buscaNroDocumento.length) >= 7 && parseInt(buscaNroDocumento.length) <= 8)) {
            if (parseInt(buscaCUIL.length) === 11) {
                TipoBuscador = 'xCUIL';
            } else {
                if (parseInt(buscaNombre.length) > 3) {
                    TipoBuscador = 'xNombre';
                } else {
                    if (parseInt(buscaNroDocumento.length) > 0) {
                        TipoBuscador = 'xNroDocumento';
                    }
                }
            }
            await LlenarGrillaBuscadorEmpleado(TipoBuscador);
        }
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
async function LlenarGrillaBuscadorEmpleado(TipoBuscador) {
    _Lista_Empleado = [];
    switch (TipoBuscador) {
        case 'xCUIL':
            _Lista_Empleado = await Empleado.TraerTodasXCUIL($("#txtBuscaCUIL").val());
            break;
        case 'xNombre':
            _Lista_Empleado = await Empleado.TraerTodasXNombre($("#txtBuscaNombre").val());
            break;
        case 'xNroDocumento':
            _Lista_Empleado = await Empleado.TraerTodasXNroDocumento($("#txtBuscaNroDocumento").val());
            break;
        default:
    }
    await Empleado.ArmarGrillaSinEliminar(_Lista_Empleado, 'grillaBuscadorEmpleado', 'EventoSeleccionarEmpleado', '');
}
$('body').on('click', ".mibtn-seleccionEmpleado", async function () {
    try {
        $this = $(this);
        let buscado = $.grep(_Lista_Empleado, function (entidad, index) {
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
$('body').on('click', ".mibtn-EliminarEmpleado", async function () {
    try {
        $this = $(this);
        let buscado = $.grep(_Lista_Empleado, function (entidad, index) {
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

