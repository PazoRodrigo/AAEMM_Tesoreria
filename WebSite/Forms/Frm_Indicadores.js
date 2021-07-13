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
    $("#LblPagoUltimoMes").text(ObjIndicadores.PagoUltimoMes);
    $("#LblEmpresasSinDeuda").text(ObjIndicadores.EmpresasSinDeuda);
    $("#LblEmpresasDeuda1Mes").text(ObjIndicadores.EmpresasDeuda1Mes);
    $("#LblEmpresasDeuda3Meses").text(ObjIndicadores.EmpresasDeuda3Meses);
    $("#LblEmpresasDeuda6Meses").text(ObjIndicadores.EmpresasDeuda6Meses);
    $("#LblEmpresasDeudaMayor6Meses").text(ObjIndicadores.EmpresasDeudaMayor6Meses);
    $("#LblEmpresasSinPagosUltimos12Meses").text(ObjIndicadores.EmpresasSinPagosUltimos12Meses);
    $("#LblEmpresasPagosIntercalados").text(ObjIndicadores.EmpresasPagosIntercalados);
    $("#LblEmpresasInactivas").text(ObjIndicadores.EmpresasInactivas);

    //$("#LblEmpresasSinDeudaSinBoleta").text(ObjIndicadores.EmpresasSinDeudaSinBoleta);
    //$("#LblEmpresasSinDeudaConBoleta").text(ObjIndicadores.EmpresasSinDeudaConBoleta);
    //$("#LblEmpresasDeuda1Mes").text(ObjIndicadores.EmpresasDeuda1Mes);
    //$("#LblEmpresasDeuda3Meses").text(ObjIndicadores.EmpresasDeuda3Meses);
    //$("#LblEmpresasDeuda6Meses").text(ObjIndicadores.EmpresasDeuda6Meses);
    //$("#LblEmpresasDeudaMayor6Meses").text(ObjIndicadores.EmpresasDeudaMayor6Meses);
    //$("#LblEmpresasPagosIntercalados").text(ObjIndicadores.EmpresasPagosIntercalados);
    //$("#LblEmpresasInactivas").text(ObjIndicadores.EmpresasInactivas);

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
$('body').on('click', '#Indicadores_Empresas_DeudaMayor6', async function (e) {
    try {
        spinner();
        let ListaEmpresas = await Empresa.TraerTodosDeudaMayor6();
        await Empresa.ArmarGrillaImpresion('ContainerPrincipal', ListaEmpresas, 'max-height: 300px; overflow-y: scroll;');
        spinnerClose();
    } catch (e) {
        spinnerClose();
        alertAlerta(e);
    }
});
$('body').on('click', '#Indicadores_Empresas_SinPagosUltimos12', async function (e) {
    try {
        spinner();
        let ListaEmpresas = await Empresa.TraerTodosSinPagoUltimos12();
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
    control += '                            <div class="col-md-12">';
    control += '                                <div style="max-height: 400px; overflow-y: scroll;width:100%;">';
    control += '                                    <table class="table table-bordered">';
    control += '                                        <tr><th style="text-align: center;">Fecha <br> Acreditación</th><th style="text-align: center;">CUIT</th><th>Razón Social</th><th style="text-align: center;width: 150px;">Importe</th><th style="text-align: center;">Origen</th></tr>';
    let i = 0;
    let totalDia = 0;
    let total = 0;
    let dia;
    while (i <= lista.length - 1) {
        dia = lista[i].FechaAcreditacion;
        totalDia = 0;
        while (i <= lista.length - 1 && dia == lista[i].FechaAcreditacion) {
            totalDia += lista[i].Importe;
            total += lista[i].Importe;
            let color = '';
            let CUIT = lista[i].CUIT;
            let RazonSocial = lista[i].RazonSocial;
            if (lista[i].CUIT == 0) {
                color = 'color: red;';
                CUIT = '';
                RazonSocial = 'A DETERMINAR';
            }
            control += String.format("<tr><td>{0}</td><td style='" + color + "'>{1}</td><td style='" + color + "'>{2}</td><td style='text-align: right;" + color + "'>{3}</td><td style='text-align: center;'>{4}</td></tr>", Date_LongToString(lista[i].FechaAcreditacion), CUIT, RazonSocial, MonedaDecimales2(lista[i].Importe), lista[i].Origen);

            i++;
        }
        control += '<tr><td colspan=3 style="text-align:right"><span>Total Parcial  ' + Date_LongToString(dia) + ': </td><th colspan=2>' + MonedaDecimales2(totalDia) + '</span></th></tr>';
    }
    control += '<tr><th colspan=5 style="text-align:right">' + MonedaDecimales2(total) + '</span></th></tr>';
    control += '                            </table>';
    control += '                        </div>';
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
