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

    Public Class UploadMSPExPaymentParser
        Inherits AbstractExcelParser

#Region "Private Variables"
        Private MSPExPayment As MSPExPayment
        Private RecallChassisMaster As RecallChassisMaster
        Private UserChassisMaster As ChassisMaster
        Private _MSPExPayment As ArrayList
        Private ContentFileType As String
        Private ErrorMessage As StringBuilder
        Private IsDataValid As Boolean
        Private CompanyCode As String
#End Region

#Region "Protected Methods"

#End Region

        Sub New(Optional ByVal contentFileType As String = "", Optional ByVal companyCode As String = "")
            Me.ContentFileType = contentFileType
            Me.CompanyCode = companyCode
        End Sub

        Public Function GetErrorMessage() As String
            Return Me.ErrorMessage.ToString
        End Function

        Public Function IsAllDataValid() As Boolean
            Return IsDataValid
        End Function

        Protected Overrides Function ParsingExcelNoTransaction(fileName As String, sheetName As String, user As String) As Object
            Me.ErrorMessage = New StringBuilder
            _MSPExPayment = New ArrayList  '-- List of Material Promotion 
            IsDataValid = True
            Try
                AbstractExcelParser.ContentTypeExcel = Me.ContentFileType
                Dim Ds As DataSet = ParseExcelDataSet(fileName, sheetName, "")
                If IsNothing(Ds) Then
                    Me.ErrorMessage.Append("Pastikan nama worksheetnya adalah Sheet1.")
                Else

                    'Validasi Column
                    Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExPayment), "RowStatus", MatchType.Exact, 0))
                    Dim sortCol As SortCollection = New SortCollection
                    sortCol.Add(New Sort(GetType(MSPExPayment), "ID", Sort.SortDirection.ASC))

                    If Ds.Tables(0).Columns.Count < 4 Then
                        Me.ErrorMessage.Append("Jumlah Kolom tidak sesuai. ")
                        Throw New Exception("Jumlah Kolom tidak sesuai. ")
                    End If

                    Dim colNames As String() = New String() {}
                    Dim iCol As Integer = 0
                    Dim dt As DataTable = Ds.Tables(0)
                    ReDim Preserve colNames(4)

                    For Each dc As DataColumn In dt.Columns
                        If iCol < 4 Then
                            colNames(iCol) = dc.ColumnName
                            iCol = iCol + 1
                        End If
                    Next

                    If Me.ErrorMessage.Length > 0 Then
                        IsDataValid = False
                        Return _MSPExPayment
                        Exit Function
                    End If

                    Dim row As DataRow
                    Dim i As Integer = 0
                    For i = 0 To Ds.Tables(0).Rows.Count - 1
                        row = Ds.Tables(0).Rows(i)
                        Try
                            'If Not IsDBNull(row(0)) Then
                            MSPExPayment = New MSPExPayment
                            If IsDBNull(row(0)) Then
                                MSPExPayment.DealerCode = ""
                            Else
                                MSPExPayment.DealerCode = row(0).ToString
                            End If
                            If Not IsDBNull(row(1)) Then
                                If Not IsValidDate(row(1)) Then
                                    MSPExPayment.PlanTransferDate = New Date(1900, 1, 1)
                                Else
                                    MSPExPayment.PlanTransferDate = row(1).ToString
                                End If
                            End If
                            
                            If Not IsDBNull(row(2)) Then
                                MSPExPayment.RegNumber = row(2).ToString
                            End If
                            If Not IsDBNull(row(3)) Then
                                MSPExPayment.ActualTotalAmount = row(3).ToString
                            End If

                            If Not IsDBNull(row(0)) Then
                                If Not IsDBNull(row(0)) Then
                                    If IsNumeric(row(0)) Then
                                        MSPExPayment.Dealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(row(0).ToString)
                                    End If
                                End If
                                
                            End If
                            If IsDBNull(row(0)) AndAlso IsDBNull(row(1)) AndAlso IsDBNull(row(2)) AndAlso IsDBNull(row(3)) Then
                            Else
                                MSPExPayment = ParseMSPExPayment(row)
                                _MSPExPayment.Add(MSPExPayment)
                            End If

                        Catch ex As Exception
                            Me.ErrorMessage.Append(ex.Message)
                        End Try
                    Next
                End If
            Catch ex As Exception
                Me.ErrorMessage.Append(ex.Message)
            End Try
            Return _MSPExPayment
        End Function

        Private Function ParseMSPExPayment(ByVal row As DataRow) As MSPExPayment
            Dim objRCM As MSPExPayment
            Dim ArrUpload As New ArrayList
            Me.ErrorMessage = New StringBuilder
            ErrorMessage.Clear()

            If Not IsDBNull(row(0)) AndAlso Not IsDBNull(row(2)) Then
                Try
                    Dim strDlr As String = CType(row(0), String)
                    Dim strRNo As String = CType(row(2), String)
                    objRCM = New MSPExPaymentFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).RetrieveDC(strDlr, strRNo)

                    If objRCM.ID <> Nothing Then
                        If objRCM.Status = EnumMSPEx.MSPExStatusPayment.Selesai Then
                            If ErrorMessage.ToString = "OK" Then
                                ErrorMessage.Clear()
                            End If
                            If MSPExPayment.ErrorMessage = "" Then
                                MSPExPayment.ErrorMessage = "Status Data Payment sudah selesai"
                            Else
                                MSPExPayment.ErrorMessage = MSPExPayment.ErrorMessage + "; Status Data Payment sudah selesai"
                            End If
                        Else
                            ErrorMessage.Append("OK")
                        End If

                    Else
                        ErrorMessage.Append("Data MSPExPayment belum ada")
                        If MSPExPayment.ErrorMessage = "" Then
                            MSPExPayment.ErrorMessage = "Data MSPExPayment belum ada"
                        Else
                            MSPExPayment.ErrorMessage = MSPExPayment.ErrorMessage + "; Data MSPExtPayment belum ada"
                        End If
                        'If ErrorMessage.ToString = "OK" Then
                        '    ErrorMessage.Clear()
                        'End If
                    End If

                    'check Dealer
                    'Dim ObjDLR As New Dealer
                    'ObjDLR = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(strDlr)
                    'If ObjDLR.ID <= 0 Or IsNothing(ObjDLR) Then
                    '    If ErrorMessage.ToString = "OK" Then
                    '        ErrorMessage.Clear()
                    '    End If
                    '    ErrorMessage.Append("Data Dealer tidak ada")
                    '    If MSPExPayment.ErrorMessage = "" Then
                    '        MSPExPayment.ErrorMessage = "Data Dealer tidak ada"
                    '    Else
                    '        MSPExPayment.ErrorMessage = MSPExPayment.ErrorMessage + "; Data Dealer tidak ada"
                    '    End If
                    'End If
                Catch ex As Exception
                    If ErrorMessage.ToString = "OK" Then
                        ErrorMessage.Clear()
                    End If
                    ErrorMessage.Append("Data MSPExtPayment belum ada")
                End Try
            Else
                If ErrorMessage.ToString = "OK" Then
                    ErrorMessage.Clear()
                End If
                If IsDBNull(row(0)) Then
                    If MSPExPayment.ErrorMessage = "" Then
                        MSPExPayment.ErrorMessage = "Dealer Code tidak boleh kosong"
                    Else
                        MSPExPayment.ErrorMessage = MSPExPayment.ErrorMessage + "; Dealer Code tidak boleh kosong"
                    End If
                End If
                ErrorMessage.Append("Dealer Code belum terdaftar")
            End If

            If Not IsDBNull(row(1)) Then
                If Not IsValidDate(row(1)) Then
                    If MSPExPayment.ErrorMessage = "" Then
                        MSPExPayment.ErrorMessage = "Format Payment Date salah"
                    Else
                        MSPExPayment.ErrorMessage = MSPExPayment.ErrorMessage + "; Format Payment Date salah"
                    End If
                End If
            Else
                If MSPExPayment.ErrorMessage = "" Then
                    MSPExPayment.ErrorMessage = "Payment Date tidak boleh kosong"
                Else
                    MSPExPayment.ErrorMessage = MSPExPayment.ErrorMessage + "; Payment Date tidak boleh kosong"
                End If
            End If

            If IsDBNull(row(2)) Then
                If MSPExPayment.ErrorMessage = "" Then
                    MSPExPayment.ErrorMessage = "Reg Number tidak boleh kosong"
                Else
                    MSPExPayment.ErrorMessage = MSPExPayment.ErrorMessage + "; Reg Number tidak boleh kosong"
                End If
            End If

            If Not IsDBNull(row(3)) Then
                If Not IsNumeric(row(3)) Then
                    If MSPExPayment.ErrorMessage = "" Then
                        MSPExPayment.ErrorMessage = "Total Amount harus numerik dan tidak boleh 0"
                    Else
                        MSPExPayment.ErrorMessage = MSPExPayment.ErrorMessage + "; Total Amount harus numerik dan tidak boleh 0"
                    End If
                End If
            Else
                If MSPExPayment.ErrorMessage = "" Then
                    MSPExPayment.ErrorMessage = "Total Amount harus numerik dan tidak boleh 0 "
                Else
                    MSPExPayment.ErrorMessage = MSPExPayment.ErrorMessage + "; Total Amount harus numerik dan tidak boleh 0"
                End If
            End If

            If ErrorMessage.Length > 0 Then
                If IsDataValid = True Then
                    IsDataValid = False
                End If
            End If

            If IsNothing(MSPExPayment.ErrorMessage) Then
                MSPExPayment.ErrorMessage = "OK"
            End If

            Return MSPExPayment
        End Function

        Private Function IsValidColumn(ByVal colName As String, ByVal mapCols As ArrayList) As Boolean
            Dim isValid As Boolean = False
            For i As Integer = 0 To mapCols.Count - 1
                If InStr(colName, mapCols(i).ColTitle, CompareMethod.Text) > 0 Then
                    isValid = True
                    Exit For
                End If
            Next
            Return isValid
        End Function

        Private Function IsValidDate(ByVal strdate As String) As Boolean
            'Dim strtgl As String = strdate.Substring(2, 2).ToString & "-" & strdate.Substring(0, 2) & "-" & strdate.Substring(4, 4)
            'yang jadi dipakai adalah setting tanggal indonesia
            If Not strdate.Trim = "" AndAlso strdate.Length > 4 Then
                Dim strtgl As String = strdate.Substring(0, 2).ToString & "" & strdate.Substring(2, 2) & "" & strdate.Substring(4, 4)
                If IsDate(strtgl) Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If

        End Function

    End Class

End Namespace


