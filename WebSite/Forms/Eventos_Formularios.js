async function ValidarPermisosXPerfil() {

    let Usuario = JSON.parse(sessionStorage.getItem("User"));
    console.log(Usuario);
    if (Usuario == undefined || Usuario == null) {
        throw 'Error de Acceso';
    }
    if (Usuario.ListaPerfiles?.length == 0) {
        throw 'Error de Acceso. Perfiles';
    }
    let iPerfil = 0;
    console.log(Usuario.ListaPerfiles?.length);
    while (iPerfil <= Usuario.ListaPerfiles?.length - 1) {
        let ObjPerfil = Usuario.ListaPerfiles[iPerfil];
        console.log(ObjPerfil.ListaPermisos?.length);
        if (ObjPerfil.ListaPermisos?.length > 0) {
            let iPermiso = 0;
            while (iPermiso <= ObjPerfil.ListaPermisos.length - 1) {
                let ObjPermiso = ObjPerfil.ListaPermisos[iPermiso];
                console.log(ObjPermiso);
                switch (ObjPermiso.Codigo) {
                    case 'FORM0_RECAUDACIONNETA':
                        $("#FORM0_RECAUDACIONNETA").css('display', 'block');
                        break;
                    case 'FORM0_RECAUDACIONBRUTA':
                        $("#FORM0_RECAUDACIONBRUTA").css('display', 'block');
                        break;
                    default:
                }
                //switch (ObjPermiso.Codigo) {
                //    case 'FORM0_RECAUDACIONNETA':
                //        $("#FORM0_RECAUDACIONNETA").css('display', 'block');
                //        break;
                //    case 'FORM0_RECAUDACIONBRUTA':
                //        $("#FORM0_RECAUDACIONBRUTA").css('display', 'block');
                //        break;
                //    default:
                //        break;

                iPermiso++;
            }
        }
        iPerfil++;
    }
}
