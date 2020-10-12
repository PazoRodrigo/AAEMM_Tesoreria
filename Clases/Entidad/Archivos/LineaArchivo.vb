Option Explicit On
Option Strict On

Imports System.IO

Namespace Entidad
    Public MustInherit Class LineaArchivo
        Inherits LUM.DBE
#Region " Atributos / Propiedades "
        Public Property IdTipoArchivo() As Enumeradores.TipoOrigen = Nothing
        Public Property NombreArchivo() As String = ""
        Protected ContadorLinea As Integer = 0
#End Region
        Public MustOverride Function ArmarListaLineas(idUsuario As Integer, nombreArchivo As String, archivo As StreamReader) As List(Of LineaArchivo)
        Public MustOverride Sub IngresarListaLineas(ListaLineas As List(Of LineaArchivo))
        Protected MustOverride Sub ValidacionesPrimeraLinea(Linea As LineaArchivo)
    End Class ' LineaArchivo
End Namespace ' Entidad

