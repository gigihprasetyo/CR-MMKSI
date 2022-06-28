
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
Imports KTB.DNet.Utility

#End Region

Namespace KTB.DNet.Parser
    Public Class UploadServiceDueDateNotificationParser
        Inherits AbstractExcelParser

        Private SPDueDateNotification As ServiceDueDateNotification
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
                    Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ServiceDueDateNotification), "RowStatus", MatchType.Exact, 0))
                    Dim sortCol As SortCollection = New SortCollection
                    sortCol.Add(New Sort(GetType(ServiceDueDateNotification), "ID", Sort.SortDirection.ASC))

                    If Ds.Tables(0).Columns.Count < 5 Then
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
                        Dim errMess As StringBuilder = New StringBuilder
                        Try
                            If Not IsDBNull(row(0)) Then
                                If row(0).ToString.Trim.Length = 0 Then
                                    errMess.AppendLine("Kode Dealer tidak boleh kosong")
                                Else
                                    If CType(New SessionHelper().GetSession("Dealer"), Dealer).Title <> CType(EnumDealerTittle.DealerTittle.KTB, String) Then
                                        If row(0).ToString.Trim <> CType(New SessionHelper().GetSession("Dealer"), Dealer).DealerCode Then
                                            errMess.AppendLine("Hanya boleh upload dealer login")
                                        End If
                                    End If
                                End If

                                Dim oDealer As Dealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(row(0).ToString.Trim)
                                If oDealer.ID = 0 Then
                                    errMess.AppendLine("Dealer tidak ditemukan")
                                End If

                                If row(2).ToString.Trim.Length = 0 Then
                                    errMess.AppendLine("Email tidak boleh kosong")
                                End If
                                If Not CommonFunction.ValidateEmail(row(2).ToString.Trim) Then
                                    errMess.AppendLine("Email tidak valid")
                                End If
                                If row(1).ToString.Trim.Length = 0 Then
                                    errMess.AppendLine("Nama Penerima tidak boleh kosong")
                                End If
                                If row(3).ToString.Trim.Length = 0 Then
                                    errMess.AppendLine("Harap isi Posisi Penerima")
                                End If
                                If row(4).ToString.Trim.Length = 0 Then
                                    errMess.AppendLine("Harap isi Jenis Notifikasi")
                                End If

                                SPDueDateNotification = New ServiceDueDateNotification
                                'Dim oDealer As Dealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(row(0).ToString)
                                SPDueDateNotification.Dealer = oDealer
                                SPDueDateNotification.NameRecipient = row(1).ToString
                                SPDueDateNotification.EmailDealer = row(2).ToString
                                SPDueDateNotification.PositionRecipient = row(3).ToString
                                SPDueDateNotification.EmailNotificationKind = row(4).ToString
                                SPDueDateNotification.ErrorMessage = errMess.ToString
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
