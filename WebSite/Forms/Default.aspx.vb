Option Explicit On
Option Strict On


Imports Clases
Partial Class Forms_Default
    Inherits System.Web.UI.Page

    Private Sub Forms_Default_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                ' Usuario        ************************************************
                'Dim temp As Entidad.Usuario = Entidad.Usuario.AccederAlSistema("USERNAMENUEVO2", "Password2")

                'Dim ObjUsuario As Entidad.Usuario = Entidad.Usuario.TraerUno(3)
                'Dim a1 As String = ObjUsuario.Nombre
                'Dim a2 As String = ObjUsuario.Apellido
                'Dim a3 As String = ObjUsuario.UserName
                'Dim a4 As String = ObjUsuario.Password
                'Dim a5 As String = ObjUsuario.CorreoElectronico
                'Dim a6 As String = CType(ObjUsuario.NroInterno, String)
                'Dim a7 As String = CType(ObjUsuario.AccesosSistema, String)
                'Dim a8 As String = ObjUsuario.Observaciones
                'Dim l As List(Of Entidad.Perfil) = ObjUsuario.ListaPerfiles()
                'Dim a9 As String = ObjUsuario.Observaciones


                'Dim ObjUsuario As New Entidad.Usuario("USUARIO USERNAME 2", "Password 2")
                'Dim ObjUsuario As New Entidad.Usuario("USUARIO USERNAME 2", "Password")
                'Dim ObjUsuario As New Entidad.Usuario("USUARIO USERNAME 1", "Password 1")

                'Dim ObjUsuario As New Entidad.Usuario(3)
                'ObjUsuario.IdUsuarioModifica = 1
                'ObjUsuario.AgregarPerfil(1)
                'ObjUsuario.AgregarPerfil(2)
                'ObjUsuario.AgregarPerfil(2)

                'ObjUsuario.IdMotivoBaja = 1
                'ObjUsuario.QuitarPerfil(1)
                'ObjUsuario.QuitarPerfil(1)

                'ObjUsuario.AgregarPerfil(1)
                'ObjUsuario.AgregarPerfil(3)

                'ObjUsuario.ResetearPassword()
                'Dim ObjUsuario As New Entidad.Usuario
                'ObjUsuario.IdUsuarioAlta = 1
                'ObjUsuario.Nombre = "Usuario Nombre 3"
                'ObjUsuario.Apellido = "Usuario Apellido 3"
                'ObjUsuario.UserName = "UsuarioUserName3"
                'ObjUsuario.Password = "Password 3"
                'ObjUsuario.CorreoElectronico = "c@c.com"
                'ObjUsuario.NroInterno = 3
                'ObjUsuario.Observaciones = "Observaciones Usuario 3"
                'ObjUsuario.Alta()

                'Dim ObjUsuario As New Entidad.Usuario(2)
                'ObjUsuario.IdUsuarioBaja = 1
                'ObjUsuario.IdMotivoBaja = 1
                'ObjUsuario.Baja()

                'Dim ObjUsuario As New Entidad.Usuario(2)
                'ObjUsuario.IdUsuarioModifica = 1
                'ObjUsuario.Nombre = "Usuario Nombre 2 NUEVO"
                'ObjUsuario.Apellido = "Usuario Apellido 2 nuevo"
                'ObjUsuario.UserName = "usernamenuevo2"
                'ObjUsuario.Password = "Password2"
                'ObjUsuario.CorreoElectronico = "bb@bb.com"
                'ObjUsuario.NroInterno = 22
                'ObjUsuario.Modifica()

                'Dim listaUsuario As List(Of Entidad.Usuario) = Entidad.Usuario.TraerTodos
                ' Perfil        ************************************************

                'Dim ObjPerfil As Entidad.Perfil = Entidad.Perfil.TraerUno(2)
                'Dim a1 As String = ObjPerfil.Nombre
                'Dim a2 As String = ObjPerfil.Observaciones
                'Dim l As List(Of Entidad.Permiso) = ObjPerfil.ListaPermisos()
                'Dim a4 As String = ObjPerfil.Observaciones


                'Dim ObjPerfil As New Entidad.Perfil(2)
                'ObjPerfil.IdUsuarioModifica = 1
                'ObjPerfil.AgregarPermiso(1)
                'ObjPerfil.AgregarPermiso(2)
                'ObjPerfil.AgregarPermiso(2)

                'ObjPerfil.IdMotivoBaja = 1
                'ObjPerfil.QuitarPermiso(1)
                'ObjPerfil.QuitarPermiso(1)

                'ObjPerfil.AgregarPermiso(1)
                'ObjPerfil.AgregarPermiso(2)

                'Dim ObjPerfil As New Entidad.Perfil
                'ObjPerfil.IdUsuarioAlta = 1
                'ObjPerfil.Nombre = "Perfil 3"
                'ObjPerfil.Observaciones = "Observaciones Perfil 3"
                'ObjPerfil.Alta()

                'Dim ObjPerfil As New Entidad.Perfil(2)
                'ObjPerfil.IdUsuarioBaja = 1
                'ObjPerfil.IdMotivoBaja = 1
                'ObjPerfil.Baja()

                'Dim ObjPerfil As New Entidad.Perfil(2)
                'ObjPerfil.IdUsuarioModifica = 1
                'ObjPerfil.Nombre = "Perfil Nuevo 2"
                'ObjPerfil.Observaciones = "Observaciones Perfil nuevo2"
                'ObjPerfil.Modifica()

                'Dim listaPerfil As List(Of Entidad.Perfil) = Entidad.Perfil.TraerTodos

                ' Permiso        ************************************************

                'Dim Obj As Entidad.Permiso = Entidad.Permiso.TraerUno(2)
                'Dim a1 As String = Obj.Nombre
                'Dim a2 As String = CType(Obj.IdTipoPermiso, String)
                'Dim a3 As String = Obj.Observaciones
                'Dim oo As Entidad.Formulario = Obj.ObjFormulario
                'Dim a4 As String = Obj.Observaciones

                'Dim ObjPermiso As New Entidad.Permiso
                'ObjPermiso.IdUsuarioAlta = 1
                'ObjPermiso.IdTipoPermiso = Enumeradores.TipoPermiso.Pagina
                'ObjPermiso.Nombre = "Permiso 3"
                'ObjPermiso.Observaciones = "Observaciones Permiso 3"
                'ObjPermiso.Alta()

                'Dim ObjPermiso As New Entidad.Permiso(2)
                'ObjPermiso.IdUsuarioBaja = 1
                'ObjPermiso.IdMotivoBaja = 1
                'ObjPermiso.Baja()

                'Dim ObjPermiso As New Entidad.Permiso(2)
                'ObjPermiso.IdUsuarioModifica = 1
                'ObjPermiso.Nombre = "Permiso nuevo 2"
                'ObjPermiso.Observaciones = "Observaciones Permiso nuevo 2"
                'ObjPermiso.Modifica()

                'Dim listapermiso As List(Of Entidad.Permiso) = Entidad.Permiso.TraerTodos
                ' Formulario      ************************************************

                'Dim Obj As Entidad.Formulario = Entidad.Formulario.TraerUno(2)
                'Dim a1 As String = CType(Obj.IdPermiso, String)
                'Dim a2 As String = Obj.ASPX
                'Dim a3 As String = Obj.Observaciones
                'Dim a4 As String = Obj.Observaciones


                'Dim ObjFormulario As New Entidad.Formulario
                'ObjFormulario.IdUsuarioAlta = 1
                'ObjFormulario.IdPermiso = 2
                'ObjFormulario.ASPX = "Formulario 2"
                'ObjFormulario.Observaciones = "Formulario Permiso 2"
                'ObjFormulario.Alta()

                'Dim ObjFormulario As New Entidad.Formulario(2)
                'ObjFormulario.IdUsuarioBaja = 1
                'ObjFormulario.IdMotivoBaja = 1
                'ObjFormulario.Baja()

                'Dim ObjFormulario As New Entidad.Formulario(2)
                'ObjFormulario.IdUsuarioModifica = 1
                'ObjFormulario.IdPermiso = 1
                'ObjFormulario.ASPX = "FormularioNuevo2"
                'ObjFormulario.Observaciones = "Formulario Permiso nuevo 2"
                'ObjFormulario.Modifica()

                'Dim listaformulario As List(Of Entidad.Formulario) = Entidad.Formulario.TraerTodos

                'Dim lista As Entidad.Empresa = Entidad.Empresa.TraerUno(5)
                'Dim lista0 As List(Of Entidad.Empresa) = Entidad.Empresa.TraerTodos
                'Dim lista As List(Of Entidad.Gasto) = Entidad.Gasto.TraerTodos()
                'Dim lista1 As List(Of Entidad.Comprobante) = Entidad.Comprobante.TraerTodosXGasto(1)
                'Dim lista1 As List(Of Entidad.Empresa) = Entidad.Empresa.TraerTodosXCUIT(0)
                'Dim lista2 As List(Of Entidad.Empresa) = Entidad.Empresa.TraerTodosXRazonSocial("NAVEGAC")
                'Dim lista3 As List(Of Entidad.Empresa) = Entidad.Empresa.TraerTodosXCentroCosto(2)

                'Const storeTraerTodosXCUIT As String = "ADM.p_Empresa_TraerTodosXCUIT"
                'Const storeTraerTodosXRazonSocial As String = "ADM.p_Empresa_TraerTodosXRazonSocial"
                'Const storeTraerTodosXCentroCosto As String = "ADM.p_Empresa_TraerTodosXCentroCosto"
            Catch ex As Exception
                MsgBox(ex.Message.ToString, MsgBoxStyle.DefaultButton1)
            End Try
        End If
    End Sub
End Class
