#Region ".NET Base Class Namespace Imports"
Imports System.Data.Odbc
#End Region

Namespace KTB.DNet.Parser
    Public Class TemplateExcelParser
        Inherits AbstractExcelParser

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function ParsingExcelNoTransaction(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object
            DataCollection = New ArrayList
            Try
                Dim strConn As String = StrConnection & fileName
                Dim objConn = New OdbcConnection(strConn)
                objConn.Open()
                Dim objCmd = New OdbcCommand("SELECT * FROM " & sheetName, objConn)
                objCmd.CommandType = CommandType.Text
                objReader = objCmd.ExecuteReader()
                While objReader.Read()
                    Dim objdata As New Object
                    '_data.NAME = objReader(1)
                    '_data.ID = objReader(0)
                    '_data.KOTA = objReader(2)
                    DataCollection.Add(objdata)
                End While
            Catch ex As Exception
                Dim str As String = ex.Message
                Return Nothing
            End Try
            Return DataCollection
        End Function

#End Region

    End Class

End Namespace