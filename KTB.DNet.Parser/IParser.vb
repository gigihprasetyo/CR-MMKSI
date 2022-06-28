#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Collections
#End Region

Namespace KTB.DNet.Parser

    Public Interface IParser
        Function ParseWithTransaction(ByVal fileName As String, ByVal user As String) As Object
        Function ParseNoTransaction(ByVal fileName As String, ByVal user As String) As Object
        Function ParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        Function ParseWithTransactionWS(ByVal fileName As String, ByVal StrValue As String) As Object
        Function ParseWithTransactionWS(ByVal fileName As String, ByVal StrValue As String, ByRef msg As String) As Object
    End Interface

    Public Interface IExcelParser
        Function ParseExcelNoTransaction(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object
        Function ParseExcelNoTransactionWithValidDate(ByVal fileName As String, ByVal sheetName As String, ByVal user As String, ByVal ValidFrom As DateTime, ByVal ValidTo As DateTime) As Object
        Function ParseExcelDataSet(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As DataSet
    End Interface

End Namespace