var _IdCentroCostoBuscadorIngreso;

class Ingreso extends DBE {
    constructor() {
        super();
        this.IdEntidad = 0;
        this.IdEstado = '';
        this.IdCentroCosto = 0;
        this.CodigoEntidad = 0;
        this.CUIT = 0;
        this.Periodo = 0;
        this.NroCheche = 0;
        this.Importe = 0;
        this.IdOrigen = 0;
        this.NroRecibo = 0;
        this.NombreArchivo = '';
        this.FechaPago = 0;
        this.FechaAcreditacion = 0;
        this.FechaPagoFacil = 0;

        this._ObjIngreso;
        this._ObjCentroCosto;
        this._ObjOrigen;
    }

    async ObjIngreso() {
        try {
            if (this._ObjIngreso === undefined) {
                this._ObjIngreso = Ingreso.TraerUnoXCUIT(this.CUIT);
            }
            return this._ObjIngreso;
        } catch (e) {
            return new Ingreso;
        }
    }
    async ObjCentroCosto() {
        try {
            if (this._ObjCentroCosto === undefined) {
                this._ObjCentroCosto = CentroCosto.TraerUno(this.IdCentroCosto);
            }
            return this._ObjCentroCosto;
        } catch (e) {
            return new CentroCosto;
        }
    }
    async Origen() {
        let result = '';
        switch (this.IdOrigen) {
            case 1:
                result = 'BNA';
                break;
            case 2:
                result = 'Pago Fácil';
                break;
            case 10:
                result = 'Transferencia';
                break;
            case 11:
                result = 'Transferencia';
                break;
            default:
        }
        return result;
    }
    async Estado() {
        switch (this.IdEstado) {

            case 'A':
                result = 'Acreditado';
                break;
            case 'L':
                result = 'Pendiente Acreditado';
                break;
            case 'P':
                result = 'Pendiente';
                break;
            case 'R':
                result = 'Rechazado';
                break;
            default:
        }
    }

 
    static async armarUC() {
        $("#grilla").html("");
        let control = "";
        if (parseInt($("#Modal-PopUpIngreso").length) === 0) {
            control += '<div id="Modal-PopUpIngreso" class="modal" tabindex="-1" role="dialog" >';
            control += '    <div class="modal-dialog modal-lg">';
            control += '        <div class="modal-content">';
            control += '            <div class="modal-header">';
            control += '                <h2 class="modal-title">Buscador de Ingresos</h2>';
            control += '            </div>';
            control += '            <div class="row">';
            control += '                <div class="modal-body">';
            control += '                    <div class="container col-md-12">';
            control += '                        <div class="row">';
            control += '                            <div class="col-md-2">';
            control += '                                <h6> CUIT </h6>';
            control += '                            </div>';
            control += '                            <div class="col-md-5">';
            control += '                                <input class="form-control input-sm TxtBuscadores" maxlength="11" style="width:160px" id="txtBuscaCUIT" type="text" placeholder="CUIT (11 números)" onkeypress="return jsSoloNumeros(event);" autocomplete="off"/>';
            control += '                           </div>';
            control += '                        </div>';
            control += '                        <div class="row mt-1">';
            control += '                            <div class="col-md-2">';
            control += '                                <h6> C. de C.</h6>';
            control += '                            </div>';
            control += '                            <div class="col-md-8">';
            control += '                                <div id="CboBuscadorCentroCosto"></div>';
            control += '                            </div>';
            control += '                        </div>';
            control += '                        <div class="row mt-1">';
            control += '                            <div class="col-md-2">';
            control += '                                <h6> Tipo </h6>';
            control += '                            </div>';
            control += '                            <div class="col-md-10">';
            control += '                                <input type="radio" name="seleccionFecha" class="mibtn-seleccionFecha" value="1"/>Acreditación';
            control += '                                <input type="radio" name="seleccionFecha" class="mibtn-seleccionFecha" value="2"/>Pago';
            control += '                            </div>';
            control += '                        </div>';
            control += '                        <div class="row mt-1">';
            control += '                            <div class="col-md-2">';
            control += '                                <h6> Fechas</h6>';
            control += '                            </div>';
            control += '                            <div class="col-md-4">';
            control += '                                <input id="TxtFechaDesde" class="form-control input-sm TxtBuscadores datepicker" type="text" placeholder="Desde" autocomplete="off">';
            control += '                            </div>';
            control += '                            <div class="col-md-4">';
            control += '                                <input id="TxtFechaHasta" class="form-control input-sm TxtBuscadores datepicker" type="text" placeholder="Hasta" autocomplete="off">';
            control += '                            </div>';
            control += '                        </div>';
            control += '                        <div class="row mt-2">';
            control += '                            <div class="col-md-9"></div>';
            control += '                            <div class="col-md-3">';
            control += '                                <div class="Boton BtnBuscar">';
            control += '                                    <a id="LinkBtnBuscarIngreso" href="#"><span>Buscar Ingreso</span></a>';
            control += '                                </div>';
            control += '                            </div>';
            control += '                        </div>';
            control += '                        <div class="row mt-2">';
            control += '                            <div class="col-md-12">';
            control += '                                <div id="grillaBuscadorIngresos" style="height: 180px;overflow-y: scroll;"></div>';
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
        _IdCentroCostoBuscadorIngreso = 0;
        $("#Modal-PopUpIngreso").modal('show');
        $("#txtBuscaCUIT").focus();
    }
}
function LimpiarBuscador() {
    $(".TxtBuscadores").val('');
    $("#grillaBuscadorIngresos").html('');
}