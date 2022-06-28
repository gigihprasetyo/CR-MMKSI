
#Region ".NET Base Class Namespace Imports"
Imports System.Data.Odbc
Imports System.Security.Principal
Imports System.Text
Imports System
Imports System.Data
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Data.OleDb
Imports System.Globalization
#End Region

#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade

#End Region

Namespace KTB.DNet.Parser
    Public Class UploadSparePartDueDateNotificationParser
        Inherits AbstractExcelParser

        Private SPDueDateNotification As SparePartDueDateNotification
        Private _SPDueDateNotification As ArrayList
        Private ContentFileType As String
        Private ErrorMessage As StringBuilder
        Private IsDataValid As Boolean

        Sub New(Optional ByVal contentFileType As String = "")
            Me.ContentFileType = contentFileType
        End Sub

        Public Function GetErrorMessage() As String
            Return Me.ErrorMessage.ToString
        End Function

        Public Function IsAllDataValid() As Boolean
            Return IsDataValid
        End Function

        Protected Overrides Function ParsingExcelNoTransaction(fileName As String, sheetName As String, user As String) As Object
            Me.ErrorMessage = New StringBuilder
            _SPDueDateNotification = New ArrayList  '-- List of Material Promotion 
            IsDataValid = True
            Try
                AbstractExcelParser.ContentTypeExcel = Me.ContentFileType
                Dim Ds As DataSet = ParseExcelDataSet(fileName, sheetName, "")
                If IsNothing(Ds) Then
                    Me.ErrorMessage.Append("Pastikan nama worksheetnya adalah Sheet1.")
                Else

                    'Validasi Column
                    Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartDueDateNotification), "RowStatus", MatchType.Exact, 0))
                    Dim sortCol As SortCollection = New SortCollection
                    sortCol.Add(New Sort(GetType(SparePartDueDateNotification), "ID", Sort.SortDirection.ASC))

                    If Ds.Tables(0).Columns.Count < 6 Then
                        Me.ErrorMessage.Append("Jumlah Kolom tidak sesuai. ")
                        Throw New Exception("Jumlah Kolom tidak sesuai. ")
                    End If

                    Dim colNames As String() = New String() {}
                    Dim iCol As Integer = 0
                    Dim dt As DataTable = Ds.Tables(0)
                    'ReDim Preserve colNames(dt.Columns.Count - 1)
                    ReDim Preserve colNames(5)

                    For Each dc As DataColumn In dt.Columns
                        If iCol < 6 Then
                            colNames(iCol) = dc.ColumnName
                            iCol = iCol + 1
                        End If
                    Next

                    If Me.ErrorMessage.Length > 0 Then
                        IsDataValid = False
                        Return _SPDueDateNotification
                        Exit Function
                    End If

                    Dim row As DataRow
                    Dim i As Integer = 0
                    For i = 0 To Ds.Tables(0).Rows.Count - 1
                        row = Ds.Tables(0).Rows(i)
                        Try
                            If Not IsDBNull(row(0)) Then
                                SPDueDateNotification = New SparePartDueDateNotification
                                Dim oDealer As Dealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(row(1).ToString)
                                SPDueDateNotification.Dealer = oDealer
                                SPDueDateNotification.NameRecipient = row(2).ToString
                                SPDueDateNotification.EmailDealer = row(3).ToString
                                SPDueDateNotification.PositionRecipient = row(4).ToString
                                SPDueDateNotification.EmailNotificationKind = row(5).ToString
                                _SPDueDateNotification.Add(SPDueDateNotification)
                            End If
                        Catch ex As Exception
                            Me.ErrorMessage.Append(ex.Message)
                        End Try
                    Next
                End If
            Catch ex As Exception
                Me.ErrorMessage.Append(ex.Message)
            End Try
            Return _SPDueDateNotification
        End Function
    End Class
End Namespace
