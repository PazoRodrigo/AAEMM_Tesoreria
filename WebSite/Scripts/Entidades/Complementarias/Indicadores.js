var _ObjIndicadores;

class Indicadores {
    constructor() {
        this.Empresas = 0;
        this.EmpresasSinDeudaSinBoleta = 0;
        this.EmpresasSinDeudaConBoleta = 0;
        this.EmpresasDeuda1Mes = 0;
        this.EmpresasDeuda3Meses = 0;
        this.EmpresasDeuda6Meses = 0;
        this.EmpresasDeudaMayor6Meses = 0;
        this.EmpresasPagosIntercalados = 0;
        this.EmpresasInactivas = 0;

        this.Empleados = 0;
        this.EmpleadosSinDeudaSinBoleta = 0;
        this.EmpleadosSinDeudaConBoleta = 0;
        this.EmpleadosDeuda1Mes = 0;
        this.EmpleadosDeuda3Meses = 0;
        this.EmpleadosDeuda6Meses = 0;
        this.EmpleadosDeudaMayor6Meses = 0;
        this.EmpleadosInactivos = 0;

        this.Recaudacion = 0;
        this.RecaudacionXCobrarSinBoleta = 0;
        this.RecaudacionXCobrarConBoleta = 0;
        this.RecaudacionDeuda1Mes = 0;
        this.RecaudacionDeuda3Meses = 0;
        this.RecaudacionDeuda6Meses = 0;
        this.RecaudacionDeudaMayor6Meses = 0;
        this.RecaudacionInactivos = 0;
        this.RecaudacionFueraTermino = 0;

        this.ChequesRechazados = 0;
    }
    // Todos
    static async Todos() {
        if (_ObjIndicadores === undefined) {
            _ObjIndicadores = await Indicadores.TraerTodas();
        }
        return _ObjIndicadores;
    }
    // Traer
    static async TraerTodas() {
        let obj = await ejecutarAsync(urlWsIndicadores + "/TraerTodos");
        if (obj !== undefined) {
            _ObjIndicadores = LlenarEntidadIndicadores(obj);
        }
        return _ObjIndicadores;
    }
}
function LlenarEntidadIndicadores(entidad) {
    let Res = new Indicadores;
    Res.Empresas = entidad.Empresas;
    Res.EmpresasSinDeudaSinBoleta = entidad.EmpresasSinDeudaSinBoleta;
    Res.EmpresasSinDeudaConBoleta = entidad.EmpresasSinDeudaConBoleta;
    Res.EmpresasDeuda1Mes = entidad.EmpresasDeuda1Mes;
    Res.EmpresasDeuda3Meses = entidad.EmpresasDeuda3Meses;
    Res.EmpresasDeuda6Meses = entidad.EmpresasDeuda6Meses;
    Res.EmpresasDeudaMayor6Meses = entidad.EmpresasDeudaMayor6Meses;
    Res.EmpresasPagosIntercalados = entidad.EmpresasPagosIntercalados;
    Res.EmpresasInactivas = entidad.EmpresasInactivas;

    Res.Empleados = entidad.Empleados;
    Res.EmpleadosSinDeudaSinBoleta = entidad.EmpleadosSinDeudaSinBoleta;
    Res.EmpleadosSinDeudaConBoleta = entidad.EmpleadosSinDeudaConBoleta;
    Res.EmpleadosDeuda1Mes = entidad.EmpleadosDeuda1Mes;
    Res.EmpleadosDeuda3Meses = entidad.EmpleadosDeuda3Meses;
    Res.EmpleadosDeuda6Meses = entidad.EmpleadosDeuda6Meses;
    Res.EmpleadosDeudaMayor6Meses = entidad.EmpleadosDeudaMayor6Meses;
    Res.EmpleadosInactivos = entidad.EmpleadosInactivos;

    Res.Recaudacion = entidad.Recaudacion;
    Res.RecaudacionXCobrarSinBoleta = entidad.RecaudacionXCobrarSinBoleta;
    Res.RecaudacionXCobrarConBoleta = entidad.RecaudacionXCobrarConBoleta;
    Res.RecaudacionDeuda1Mes = entidad.RecaudacionDeuda1Mes;
    Res.RecaudacionDeuda3Meses = entidad.RecaudacionDeuda3Meses;
    Res.RecaudacionDeuda6Meses = entidad.RecaudacionDeuda6Meses;
    Res.RecaudacionDeudaMayor6Meses = entidad.RecaudacionDeudaMayor6Meses;
    Res.RecaudacionInactivos = entidad.RecaudacionInactivos;
    Res.RecaudacionFueraTermino = entidad.RecaudacionFueraTermino;

    Res.ChequesRechazados = entidad.ChequesRechazados;
    return Res;
}
