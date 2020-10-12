var _Lista_Empresa;

class StrBusquedaEmpresa {
    constructor() {
        this.CUIT = 0;
        this.RazonSocial = '';
        this.IdCentroCosto = 0;
        this.IncluirAlta = 1;
        this.IncluirBaja = 0;
        this.Incluir0 = 0;
    }
}
class Empresa extends DBE {
    constructor() {
        super();
        this.IdEntidad = 0;
        this.Codigo = 0;
        this.RazonSocial = '';
        this.CUIT = 0;
        this.CorreoElectronico = '';
        this.IdEstado = 0;

        this.ObjDomicilio;
    }

    async StrCodigo(cantCaracteres) {
        let Result = '';
        if (this.Codigo > 0) {
            Result = Right('00000000000' + this.Codigo, cantCaracteres);
        }
        return Result;
    }
    // Lazy
    async ObjDatosCalculados() {
        try {
            let data = {
                'CUIT': CUIT,
                'IdEstablecimiento': IdEstablecimiento
            };
            let lista = await ejecutarAsync(urlWsEmpresa + "/TraerDatosCalculados", data);
            let result = [];
            if (lista.length > 0) {
                $.each(lista, function (key, value) {
                    result.push(LlenarEntidadDatosCalculados(value));
                });
            }
            return result[0];
        } catch (e) {
            return [];
        }
    }
    // ABM
    async Alta() {
        await this.ValidarCamposEmpresa();
        this.RazonSocial = this.RazonSocial.toUpperCase();
        this.CorreoElectronico = this.CorreoElectronico.toUpperCase();
        try {
            let ObjU = JSON.parse(sessionStorage.getItem("User"));
            this.IdUsuarioAlta = ObjU.IdEntidad;
            let data = {
                'entidad': this
            };
            let NuevaEmpresa = await ejecutarAsync(urlWsEmpresa + "/Alta", data);
            if (NuevaEmpresa !== undefined) {
                this.IdEntidad = NuevaEmpresa.IdEntidad;
                this.Codigo = NuevaEmpresa.Codigo;
            }
            return;
        } catch (e) {
            throw e;
        }
    }
    async Modifica() {
        await this.ValidarCamposEmpresa();
        try {
            let ObjU = JSON.parse(sessionStorage.getItem("User"));
            this.IdUsuarioModifica = ObjU.IdEntidad;
            let data = {
                'entidad': this
            };
            let NuevaEmpresa = await ejecutarAsync(urlWsEmpresa + "/Modifica", data);
            if (NuevaEmpresa !== undefined) {
                this.IdEntidad = NuevaEmpresa.IdEntidad;
                this.Codigo = NuevaEmpresa.Codigo;
            }
            return;
        } catch (e) {
            throw e;
        }
    }
    async Baja() {
        try {
            let ObjU = JSON.parse(sessionStorage.getItem("User"));
            this.IdUsuarioBaja = ObjU.IdEntidad;
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

        //    this.IdEntidad = 0;
        //    this.Codigo = 0;
        //    this.RazonSocial = '';
        //    this.CUIT = 0;
        //    this.CorreoElectronico = '';
        //    this.IdEstado = 0;
        let sError = '';
        if (this.RazonSocial.length === 0) {
            sError += 'Debe ingresar la Razón Social<br/>';
        } else {
            if (this.RazonSocial.length <= 2) {
                sError += 'La Razón Social debe tener al menos 3 caracteres<br/>';
            }
        }
        if (this.CUIT.length === 0) {
            sError += 'Debe ingresar el CUIT<br/>';
        } else {
            if (this.CUIT.length != 11) {
                sError += 'El CUIT debe tener 11 dígitos<br/>';
            }
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
        let data = {
            'IdEntidad': IdEntidad
        };
        let lista = await ejecutarAsync(urlWsEmpresa + "/TraerUno", data);
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadEmpresa(value));
            });
        }
        return result[0];
    }
    static async TraerUnaXCUIT(CUIT) {
        let data = {
            'CUIT': CUIT
        };
        let lista = await ejecutarAsync(urlWsEmpresa + "/TraerUnaXCUIT", data);
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadEmpresa(value));
            });
        }
        return result[0];
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
    // static async TraerTodasXCUIT(CUIT) {
    //     let data = {
    //         'CUIT': CUIT
    //     };
    //     let lista = await ejecutarAsync(urlWsEmpresa + "/TraerTodosXCUIT", data);
    //     _Lista_Empresa = [];
    //     let result = [];
    //     if (lista.length > 0) {
    //         $.each(lista, function(key, value) {
    //             result.push(LlenarEntidadEmpresa(value));
    //         });
    //     }
    //     _Lista_Empresa = result;
    //     return _Lista_Empresa;
    // }
    // static async TraerTodasXRazonSocial(RazonSocial) {
    //     let data = {
    //         'RazonSocial': RazonSocial
    //     };
    //     let lista = await ejecutarAsync(urlWsEmpresa + "/TraerTodosXRazonSocial", data);
    //     _Lista_Empresa = [];
    //     let result = [];
    //     if (lista.length > 0) {
    //         $.each(lista, function(key, value) {
    //             result.push(LlenarEntidadEmpresa(value));
    //         });
    //     }
    //     _Lista_Empresa = result;
    //     return _Lista_Empresa;
    // }
    // static async TraerTodasXCentroCosto(IdCentroCosto) {
    //     let data = {
    //         'IdCentroCosto': IdCentroCosto
    //     };
    //     let lista = await ejecutarAsync(urlWsEmpresa + "/TraerTodosXCentroCosto", data);
    //     _Lista_Empresa = [];
    //     let result = [];
    //     if (lista.length > 0) {
    //         $.each(lista, function(key, value) {
    //             result.push(LlenarEntidadEmpresa(value));
    //         });
    //     }
    //     _Lista_Empresa = result;
    //     return _Lista_Empresa;
    // }
    static async TraerTodosXBusqueda(Busqueda) {
        let data = {
            'Busqueda': Busqueda
        };
        let lista = await ejecutarAsync(urlWsEmpresa + "/TraerTodosXBusqueda", data);
        let result = [];
        if (lista.length > 0) {
            $.each(lista, function (key, value) {
                result.push(LlenarEntidadEmpresa(value));
            });
        }
        _ListaEmpresas = result;
        return result;
    }
    // Otros
    static async Refresh() {
        _Lista_Empresa = await Empresa.TraerTodas();
    }
    // Herramientas
    static async ArmarGrillaCabecera(div) {
        $("#" + div + "").html('');
        let str = "";
        str += '<table class="table table-sm">';
        str += '    <thead>';
        str += '        <tr>';
        str += '            <th style="width: 50px;visibility: hidden;"><img src="../../Imagenes/lupa.png" style="width:30px; height: auto;" alt=""></th>';
        str += '            <th class="text-center" style="width: 100px;">CUIT</th>';
        str += '            <th class="text-center" style="width: 50px;">Cód.</th>';
        str += '            <th class="text-center" style="width: 400px;">Razon Social</th>';
        str += '            <th class="text-left pl-4" style="width: 120px;">Fecha Baja</th>';
        str += '        </tr>';
        str += '    </thead>';
        str += '</table >';
        return $("#" + div + "").html(str);
    }
    static async ArmarGrillaDetalle(div, lista, evento, estilo) {
        $("#" + div + "").html('');
        let str = "";
        str += '<div style="' + estilo + '">';
        str += '<table class="table table-sm table-striped table-hover">';
        str += '    <tbody>';
        if (lista.length > 0) {
            for (let item of lista) {
                str += '        <tr>';
                str += '            <td style="width: 50px;" class="text-center"><a hfre="#" id="' + item.IdEntidad + '"  data-Evento="' + evento + '" onclick="SeleccionEmpresa(this);"> <img src="../../Imagenes/lupa.png" alt=""></a></td>';
                str += '            <td class="text-center" style="width: 120px;"><div class="text-light">' + item.CUIT + '</div></td>';
                str += '            <td class="text-center" style="width: 50px;"><div class="text-light">' + await item.StrCodigo(6) + '</div></td>';
                str += '            <td class="text-left pl-3" style="width: 500px;"><div class="text-light">' + item.RazonSocial + '</div></td>';
                let strFechaBaja = '';
                if (item.FechaBaja > 0) {
                    console.log(item.FechaBaja);
                    strFechaBaja = LongToDateString(item.FechaBaja);
                }
                str += '            <td class="text-center" style="width: 120px"><div class="text-danger bg-light">' + strFechaBaja + '</div></td>';
                str += '        </tr>';
            }
        }
        str += '    </tbody>';
        str += '</table >';
        str += '</div >';
        return $("#" + div + "").html(str);

    }
    // static async ArmarGrillaSinEliminar(lista, div, eventoSeleccion, estilo) {
    //     $('#' + div + '').html('');
    //     let str = '';
    //     if (lista.length > 0) {
    //         str += '<div style="' + estilo + '">';
    //         str += '    <ul class="ListaGrilla">';
    //         let estiloItem = '';
    //         for (let item of lista) {
    //             estiloItem = 'LinkListaGrillaObjeto';
    //             if (item.IdEstado === 1) {
    //                 estiloItem = 'LinkListaGrillaObjetoEliminado';
    //             }
    //             let aItem = '<a href="#" class="mibtn-seleccionEmpresa" data-Evento="' + eventoSeleccion + '" data-Id="' + item.IdEntidad + '">' + item.CUIT + ' - ' + item.RazonSocial + '</a>';
    //             str += String.format('<li class="liGrilla"><div class="LinkListaGrilla ' + estiloItem + '">{0}</div></li>', aItem);
    //         }
    //         str += '    </ul>';
    //         str += '</div>';
    //     }
    //     return $('#' + div + '').html(str);
    // }
    // static async ArmarGrilla(lista, div, eventoSeleccion, eventoEliminar, estilo) {
    //     $('#' + div + '').html('');
    //     let str = '';
    //     console.log(lista);
    //     if (lista.length > 0) {
    //         str += '<div style="' + estilo + '">';
    //         str += '    <ul class="ListaGrilla">';
    //         let estiloItem = '';
    //         for (let item of lista) {
    //             estiloItem = 'LinkListaGrillaObjeto';
    //             if (item.IdEstado === 1) {
    //                 estiloItem = 'LinkListaGrillaObjetoEliminado';
    //             }
    //             let aItem = '<a href="#" class="mibtn-seleccionEmpresa" data-Evento="' + eventoSeleccion + '" data-Id="' + item.IdEntidad + '">' + item.CUIT + ' - ' + item.RazonSocial + '</a>';
    //             let aEliminar = '<a href="#" class="mibtn-EliminarEmpresa" data-Evento="' + eventoEliminar + '" data-Id="' + item.IdEntidad + '"><span class="icon-bin"></span></a>';
    //             str += String.format('<li class="liGrilla"><div class="LinkListaGrilla ' + estiloItem + '">{0}</div><div class="LinkListaGrilla LinkListaGrillaElimina">{1}</div></li>', aItem, aEliminar);
    //         }
    //         str += '    </ul>';
    //         str += '</div>';
    //     }
    //     return $('#' + div + '').html(str);
    // }
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
    Res.Codigo = entidad.Codigo;
    Res.RazonSocial = entidad.RazonSocial;
    Res.CUIT = entidad.CUIT;
    Res.CorreoElectronico = entidad.CorreoElectronico;
    Res.IdEstado = entidad.IdEstado;
    Res.ObjDomicilio = entidad.ObjDomicilio;
    return Res;
}
async function SeleccionEmpresa(MiElemento) {
    try {
        let elemento = document.getElementById(MiElemento.id);
        let evento = elemento.getAttribute('data-Evento');
        let buscado = $.grep(_ListaEmpresas, function (entidad, index) {
            return entidad.IdEntidad == MiElemento.id;
        });
        if (buscado[0] != undefined) {
            let event = new CustomEvent(evento, {
                detail: buscado[0]
            });
            document.dispatchEvent(event);
        }
    } catch (e) {
        alertAlerta(e);
    }
}

function LlenarEntidadDatosCalculados(entidad) {
    console.log(entidad);
    let Res = [];
    Res.IdEntidad = entidad.IdEntidad;
    Res.RazonSocial = entidad.RazonSocial;
    Res.CUIT = entidad.CUIT;
    Res.SaldoCuentaCorriente = entidad.SaldoCuentaCorriente;
    Res.Empleados = entidad.Empleados;
    Res.Afiliados = entidad.Afiliados;
    Res.NoAfiliados = entidad.NoAfiliados;
    console.log(Res);
    return Res;
}

// function LimpiarBuscador() {
//     $(".TxtBuscadores").val('');
//     $("#grillaBuscadorEmpresa").html('');
// }
// document.addEventListener('EventoBuscadorCentroCosto', async function(e) {
//     try {
//         let objSeleccionado = e.detail;
//         _IdCentroCostoBuscadorEmpresa = objSeleccionado.IdEntidad;
//         $("#SelectorBuscadorCentroCosto").text(objSeleccionado.Nombre);
//     } catch (e) {
//         alertAlerta(e);
//     }
// }, false);
// $('body').on('click', '#LinkBtnBuscarEmpresa', async function(e) {
//     try {
//         let buscaCUIT = $("#txtBuscaCUIT").val();
//         let buscaRazonSocial = $("#txtRazonSocial").val();
//         let TipoBuscador = '';
//         if (parseInt(buscaCUIT.length) === 11 || parseInt(buscaRazonSocial.length) > 3 || parseInt(_IdCentroCostoBuscadorEmpresa) > 0) {
//             if (parseInt(buscaCUIT.length) === 11) {
//                 TipoBuscador = 'xCUIT';
//             } else {
//                 if (parseInt(buscaRazonSocial.length) > 3) {
//                     TipoBuscador = 'xRazonSocial';
//                 } else {
//                     if (parseInt(_IdCentroCostoBuscadorEmpresa) > 0) {
//                         TipoBuscador = 'xIdCentroCosto';
//                     }
//                 }
//             }
//             spinner();
//             await LlenarGrillaBuscadorEmpresa(TipoBuscador);
//             spinnerClose();
//         }
//     } catch (e) {
//         spinnerClose();
//         alertAlerta(e);
//     }
// });
// async function LlenarGrillaBuscadorEmpresa(TipoBuscador) {
//     _Lista_Empresa = [];
//     switch (TipoBuscador) {
//         case 'xCUIT':
//             _Lista_Empresa = await Empresa.TraerTodasXCUIT($("#txtBuscaCUIT").val());
//             break;
//         case 'xRazonSocial':
//             _Lista_Empresa = await Empresa.TraerTodasXRazonSocial($("#txtRazonSocial").val());
//             break;
//         case 'xIdCentroCosto':
//             _Lista_Empresa = await Empresa.TraerTodasXCentroCosto(_IdCentroCostoBuscadorEmpresa);
//             break;
//         default:
//     }
//     Empresa.ArmarGrillaSinEliminar(_Lista_Empresa, 'grillaBuscadorEmpresa', 'EventoSeleccionarEmpresa', '');
// }

// $('body').on('click', ".mibtn-seleccionEmpresa", async function() {
//     try {
//         $this = $(this);
//         let buscado = $.grep(_Lista_Empresa, function(entidad, index) {
//             return entidad.IdEntidad === parseInt($this.attr("data-Id"));
//         });
//         let Seleccionado = buscado[0];
//         let evento = $this.attr("data-Evento");
//         $("#Modal-PopUpEmpresa").modal('hide');
//         let event = new CustomEvent(evento, { detail: Seleccionado });
//         document.dispatchEvent(event);
//     } catch (e) {
//         alertAlerta(e);
//     }
// });
// $('body').on('click', ".mibtn-EliminarEmpresa", async function() {
//     try {
//         $this = $(this);
//         let buscado = $.grep(_Lista_Empresa, function(entidad, index) {
//             return entidad.IdEntidad === parseInt($this.attr("data-Id"));
//         });
//         let Seleccionado = buscado[0];
//         let evento = $this.attr("data-Evento");
//         let event = new CustomEvent(evento, { detail: Seleccionado });
//         document.dispatchEvent(event);
//     } catch (e) {
//         alertAlerta(e);
//     }
// });