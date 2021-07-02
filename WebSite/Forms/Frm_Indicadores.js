$(document).ready(function () {
    try {
        $("#NombreFormulario").text('Indicadores');
        Inicio();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
})
async function Inicio() {
    spinner();
    let ObjIndicadores = await Indicadores.Todos();
    $("#LblEmpresas").text(ObjIndicadores.Empresas);
    $("#LblEmpresasSinDeudaSinBoleta").text(ObjIndicadores.EmpresasSinDeudaSinBoleta);
    $("#LblEmpresasSinDeudaConBoleta").text(ObjIndicadores.EmpresasSinDeudaConBoleta);
    $("#LblEmpresasDeuda1Mes").text(ObjIndicadores.EmpresasDeuda1Mes);
    $("#LblEmpresasDeuda3Meses").text(ObjIndicadores.EmpresasDeuda3Meses);
    $("#LblEmpresasDeuda6Meses").text(ObjIndicadores.EmpresasDeuda6Meses);
    $("#LblEmpresasDeudaMayor6Meses").text(ObjIndicadores.EmpresasDeudaMayor6Meses);
    $("#LblEmpresasPagosIntercalados").text(ObjIndicadores.EmpresasPagosIntercalados);
    $("#LblEmpresasInactivas").text(ObjIndicadores.EmpresasInactivas);

    $("#LblEmpleados").text(ObjIndicadores.Empleados);
    $("#LblEmpleadosSinDeudaSinBoleta").text(ObjIndicadores.EmpleadosSinDeudaSinBoleta);
    $("#LblEmpleadosSinDeudaConBoleta").text(ObjIndicadores.EmpleadosSinDeudaConBoleta);
    $("#LblEmpleadosDeuda1Mes").text(ObjIndicadores.EmpleadosDeuda1Mes);
    $("#LblEmpleadosDeuda3Meses").text(ObjIndicadores.EmpleadosDeuda3Meses);
    $("#LblEmpleadosDeuda6Meses").text(ObjIndicadores.EmpleadosDeuda6Meses);
    $("#LblEmpleadosDeudaMayor6Meses").text(ObjIndicadores.EmpleadosDeudaMayor6Meses);
    $("#LblEmpleadosInactivos").text(ObjIndicadores.EmpleadosInactivos);

    $("#LblRecaudacionNeta").text(separadorMiles(ObjIndicadores.RecaudacionNeta.toFixed(2)));
    $("#LblRecaudacionBruta").text(separadorMiles(ObjIndicadores.RecaudacionBruta.toFixed(2)));

    //$("#LblRecaudacion").text(separadorMiles(ObjIndicadores.Recaudacion.toFixed(2)));
    //$("#LblRecaudacionXCobrarSinBoleta").text(ObjIndicadores.RecaudacionXCobrarSinBoleta);
    //$("#LblRecaudacionXCobrarConBoleta").text(ObjIndicadores.RecaudacionXCobrarConBoleta);
    //$("#LblRecaudacionDeuda1Mes").text(ObjIndicadores.RecaudacionDeuda1Mes);
    //$("#LblRecaudacionDeuda3Meses").text(ObjIndicadores.RecaudacionDeuda3Meses);
    //$("#LblRecaudacionDeuda6Meses").text(ObjIndicadores.RecaudacionDeuda6Meses);
    //$("#LblRecaudacionDeudaMayor6Meses").text(ObjIndicadores.RecaudacionDeudaMayor6Meses);
    //$("#LblRecaudacionInactivos").text(ObjIndicadores.RecaudacionInactivos);
    //$("#LblRecaudacionFueraTermino").text(ObjIndicadores.RecaudacionFueraTermino);

    $("#LblChequesRechazados").text(ObjIndicadores.ChequesRechazados);
    spinnerClose();
}

$('body').on('click', '#Indicadores_Empresas', async function (e) {
    try {
        spinner();
        let ListaEmpresas = await Empresa.TraerTodosSinBaja();
        await Empresa.ArmarGrillaImpresion('ContainerPrincipal', ListaEmpresas, 'max-height: 300px; overflow-y: scroll;');
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});

$('body').on('click', '#Indicadores_Empresas_SinDeuda', async function (e) {
    try {
        spinner();
        let ListaEmpresas = await Empresa.TraerTodosSinDeuda();
        await Empresa.ArmarGrillaImpresion('ContainerPrincipal', ListaEmpresas, 'max-height: 300px; overflow-y: scroll;');
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});

$('body').on('click', '#Indicadores_Empresas_Deuda1', async function (e) {
    try {
        spinner();
        let ListaEmpresas = await Empresa.TraerTodosDeuda1();
        await Empresa.ArmarGrillaImpresion('ContainerPrincipal', ListaEmpresas, 'max-height: 300px; overflow-y: scroll;');
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#Indicadores_Empresas_Deuda3', async function (e) {
    try {
        spinner();
        let ListaEmpresas = await Empresa.TraerTodosDeuda3();
        await Empresa.ArmarGrillaImpresion('ContainerPrincipal', ListaEmpresas, 'max-height: 300px; overflow-y: scroll;');
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#Indicadores_Empresas_Deuda6', async function (e) {
    try {
        spinner();
        let ListaEmpresas = await Empresa.TraerTodosDeuda6();
        await Empresa.ArmarGrillaImpresion('ContainerPrincipal', ListaEmpresas, 'max-height: 300px; overflow-y: scroll;');
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#Indicadores_Empresas_Limpiar', async function (e) {
    try {
        let ListaEmpresas = [];
        $("#ContainerPrincipal").html("");
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#Indicadores_Empresas_Imprimir', async function (e) {
    try {
        spinner()
        let nombreArchivo = "Reporte Empresas-" + FechaHoyLng() + ".xls";
        $("#ContainerPrincipal").table2excel({ filename: nombreArchivo, sheetName: "Reporte Empresas" });
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#BtnRecadudacionNeta', async function (e) {
    try {
        spinner()
        await ArmarPopRecaudacion(0);
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#BtnRecadudacionBruta', async function (e) {
    try {
        spinner()
        await ArmarPopRecaudacion(0);
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});


async function ArmarPopRecaudacion(Tipo) {
    //    spinner();
    let lista = [];
    let Titulo = '';
    let fecha = new Date();
    //let fecha = new Date('2021-01-01');
    if (Tipo == 0) {
        lista = await IngresoReporte.TraerRecaudacionNeta(Date_PrimerDiaMes_LngToLng(fecha), Date_UltimoDiaMes_LngToLng(fecha));
        Titulo = 'Recaudación Neta';
    } else {
        lista = await IngresoReporte.TraerRecaudacionBruta(dateToLong(strDesde), dateToLong(strHasta));
        Titulo = 'Recaudación Bruta';
    }
    let control = "";
    ($("#Modal-PopUpConsumosAfiliado").remove());
    control += '<div id="Modal-PopUpConsumosAfiliado" class="modal" tabindex="-1" role="dialog" >';
    control += '    <div class="modal-dialog modal-lg">';
    control += '        <div class="modal-content">';
    control += '            <div class="modal-header bg-primary">';
    control += '                <h3 class="modal-title ">' + Titulo + '</h3>';
    control += '            </div>';
    control += '            <div class="row">';
    control += '                <div class="modal-body">';
    control += '                    <div class="container col-md-12">';
    control += '                        <div class="row">';
    control += '                            <div class="col-md-12">';
    control += '                                <h4>' + '' + '</h4>';
    control += '                            </div>';
    control += '                        </div>';
    control += '                    </div>';
    control += '                        <div class="row">';
    control += '                            <div class="col-md-12" style="max-height: 400px; overflow: auto;">';
    control += '                        <table class="table table-bordered">';
    control += '                            <tr><th>Fecha Pago</th><th>Fecha Acreditación</th><th>CUIT</th><th>Razón Social</th><th>Importe</th><th>Origen</th></tr>';
    for (let item of lista) {
        control += String.format("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td></tr>", item.FechaPago, item.FechaAcreditacion, item.CUIT, item.RazonSocial, item.Importe, item.Origen);
    }
    control += '                        </table>';
    control += '                        </div>';
    control += '                    </div>';
    control += '                </div>';
    control += '            </div>';
    control += '            <div class="modal-footer">';
    control += '                <button type="button" class="btn btn-primary" data-dismiss="modal">Cerrar</button>';
    control += '            </div>';
    control += '        </div>';
    control += '    </div>';
    control += '</div>';
    $("body").append(control);
    $("#Modal-PopUpConsumosAfiliado").modal({ show: true, backdrop: 'static', keyboard: false })
    spinnerClose();
}
