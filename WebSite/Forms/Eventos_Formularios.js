async function ValidarPermisosXPerfil() {

    let Usuario = JSON.parse(sessionStorage.getItem("User"));
    if (Usuario == undefined || Usuario == null) {
        throw 'Error de Acceso';
    }
    if (Usuario.ListaPerfiles?.length == 0) {
        throw 'Error de Acceso. Perfiles';
    }
    let iPerfil = 0;
    while (iPerfil <= Usuario.ListaPerfiles?.length - 1) {
        let ObjPerfil = Usuario.ListaPerfiles[iPerfil];
        if (ObjPerfil.ListaPermisos?.length > 0) {
            let iPermiso = 0;
            while (iPermiso <= ObjPerfil.ListaPermisos.length - 1) {
                let ObjPermiso = ObjPerfil.ListaPermisos[iPermiso];
                console.log(ObjPermiso.Codigo);
                switch (ObjPermiso.Codigo) {
                    case 'FORM0_RECAUDACIONNETA':
                        $("#FORM0_RECAUDACIONNETA").css('display', 'block');
                        break;
                    case 'FORM0_RECAUDACIONBRUTA':
                        $("#FORM0_RECAUDACIONBRUTA").css('display', 'block');
                        break;


                    case 'FORM0_INDICADORES_EMPRESAS':
                        $("#FORM0_INDICADORES_EMPRESAS").css('display', 'block');
                        break;
                    case 'FORM0_INDICADORES_EMPRESAS_SINDEUDA':
                        $("#FORM0_INDICADORES_EMPRESAS_SINDEUDA").css('display', 'block');
                        break;
                    case 'FORM0_INDICADORES_EMPRESAS_DEUDA1':
                        $("#FORM0_INDICADORES_EMPRESAS_DEUDA1").css('display', 'block');
                        break;
                    case 'FORM0_INDICADORES_EMPRESAS_DEUDA3':
                        $("#FORM0_INDICADORES_EMPRESAS_DEUDA3").css('display', 'block');
                        break;
                    case 'FORM0_INDICADORES_EMPRESAS_DEUDA6':
                        $("#FORM0_INDICADORES_EMPRESAS_DEUDA6").css('display', 'block');
                        break;
                    case 'FORM0_INDICADORES_EMPRESAS_DEUDAMAYOR6':
                        $("#FORM0_INDICADORES_EMPRESAS_DEUDAMAYOR6").css('display', 'block');
                        break;
                    case 'FORM0_INDICADORES_EMPRESAS_SINPAGOSULTIMOS12':
                        $("#FORM0_INDICADORES_EMPRESAS_SINPAGOSULTIMOS12").css('display', 'block');
                        break;
                    case 'FORM0_INDICADORES_EMPRESAS_PAGOSINTERCALADOS':
                        $("#FORM0_INDICADORES_EMPRESAS_PAGOSINTERCALADOS").css('display', 'block');
                        break;
                    case 'FORM0_INDICADORES_EMPRESAS_INACTIVAS':
                        $("#FORM0_INDICADORES_EMPRESAS_INACTIVAS").css('display', 'block');
                        break;


                    case 'FORM0_INDICADORES_EMPLEADOS':
                        $("#FORM0_INDICADORES_EMPLEADOS").css('display', 'block');
                        break;


                    case 'FORM0_BTNCONFIGURACION':
                        $("#FORM0_BTNCONFIGURACION").css('display', 'block');
                        break;
                    case 'FORM0_BTNADMINISTRACION':
                        $("#FORM0_BTNADMINISTRACION").css('display', 'block');
                        break;
                    case 'FORM0_BTNREPORTES':
                        $("#FORM0_BTNREPORTES").css('display', 'block');
                        break;
                    case 'FORM0_BTNINGRESOS':
                        $("#FORM0_BTNINGRESOS").css('display', 'block');
                        break;
                    case 'FORM0_BTNGASTOS':
                        $("#FORM0_BTNGASTOS").css('display', 'block');
                        break;
                    case 'FORM0_BTNARCHIVOS':
                        $("#FORM0_BTNARCHIVOS").css('display', 'block');
                        break;

                    case 'FORM0_DIVINGRESOARCHIVOS':
                        $("#FORM0_DIVINGRESOARCHIVOS").css('display', 'block');
                        break;

                    case 'FORM2_BTNEMPRESAS':
                        $("#FORM2_BTNEMPRESAS").css('display', 'block');
                        break;
                    case 'FORM2_BTNEMPLEADOS':
                        $("#FORM2_BTNEMPLEADOS").css('display', 'block');
                        break;
                    case 'FORM2_BTNCHEQUESTERCEROS':
                        $("#FORM2_BTNCHEQUESTERCEROS").css('display', 'block');
                        break;
                    case 'FORM2_BTNCUENTASCORRIENTES':
                        $("#FORM2_BTNCUENTASCORRIENTES").css('display', 'block');
                        break;
                    case 'FORM2_BTNSUELDOS':
                        $("#FORM2_BTNSUELDOS").css('display', 'block');
                        break;

                    case 'FORM3_BTNINGRESOS':
                        $("#FORM3_BTNINGRESOS").css('display', 'block');
                        break;
                    case 'FORM3_BTNINGRESOSMANUALES':
                        $("#FORM3_BTNINGRESOSMANUALES").css('display', 'block');
                        break;
                    default:
                }
                iPermiso++;
            }
        }
        iPerfil++;
    }
}
