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
Imports KTB.DNet.BusinessFacade.Sparepart
Imports KTB.DNet.BusinessFacade

#End Region

Namespace KTB.DNet.Parser
    Public Class UploadTOPSparepartPaymentParser
        Inherits AbstractExcelParser


#Region "Private Variables"
        Private TOPSPTransferPayment As TOPSPTransferPayment
        Private _arrTOPSPTransferPayment As ArrayList
        Private ContentFileType As String
        Private ErrorMessage As StringBuilder
        Private IsDataValid As Boolean
        Private CompanyCode As String
#End Region

#Region "Protected Methods"

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

        ''' <summary>
        ''' Parse With No Trans to Obejct
        ''' </summary>
        ''' <param name="fileName">Nama filenya</param>
        ''' <param name="sheetName">Nama Sheet</param>
        ''' <param name="user">user Name</param>
        ''' <returns>Object Data Customer list</returns>
        ''' <remarks>Pake Method Baru</remarks>
        Protected Overrides Function ParsingExcelNoTransaction(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object
            Me.ErrorMessage = New StringBuilder
            _arrTOPSPTransferPayment = New ArrayList  '-- List of Material Promotion 
            IsDataValid = True
            Try
                AbstractExcelParser.ContentTypeExcel = Me.ContentFileType
                Dim Ds As DataSet = ParseExcelDataSet(fileName, sheetName, "")
                If IsNothing(Ds) Then
                    Me.ErrorMessage.Append("Pastikan nama worksheetnya adalah Sheet1.")
                Else

                    'Validasi Column
                    Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TOPSPTransferPayment), "RowStatus", MatchType.Exact, 0))
                    Dim sortCol As SortCollection = New SortCollection
                    sortCol.Add(New Sort(GetType(TOPSPTransferPayment), "ID", Sort.SortDirection.ASC))

                    If Ds.Tables(0).Columns.Count < 8 Then
                        Me.ErrorMessage.Append("Jumlah Kolom tidak sesuai. ")
                        Throw New Exception("Jumlah Kolom tidak sesuai. ")
                    End If

                    Dim colNames As String() = New String() {}
                    Dim iCol As Integer = 0
                    Dim dt As DataTable = Ds.Tables(0)
                    ReDim Preserve colNames(8)

                    For Each dc As DataColumn In dt.Columns
                        If iCol < 8 Then
                            colNames(iCol) = dc.ColumnName
                            iCol = iCol + 1
                        End If
                    Next

                    If Me.ErrorMessage.Length > 0 Then
                        IsDataValid = False
                        Return _arrTOPSPTransferPayment
                        Exit Function
                    End If

                    Dim row As DataRow
                    Dim i As Integer = 0
                    For i = 0 To Ds.Tables(0).Rows.Count - 1
                        row = Ds.Tables(0).Rows(i)
                        Try
                            TOPSPTransferPayment = New TOPSPTransferPayment
                            If IsDBNull(row(0)) Then
                                TOPSPTransferPayment.RegNumber = ""
                            Else
                                TOPSPTransferPayment.RegNumber = row(0).ToString
                            End If

                            If IsDBNull(row(1)) Then
                                TOPSPTransferPayment.KodeDealer = ""
                            Else
                                TOPSPTransferPayment.KodeDealer = row(1).ToString
                            End If

                            'If IsDBNull(row(2)) Then
                            '    TOPSPTransferPayment.NoBilling = ""
                            'Else
                            '    TOPSPTransferPayment.NoBilling = row(2).ToString
                            'End If

                            If Not IsDBNull(row(2)) Then
                                If Not IsNumeric(row(2)) Then
                                    TOPSPTransferPayment.TransferAmount = 0
                                Else
                                    TOPSPTransferPayment.TransferAmount = row(2)
                                End If
                            End If

                            If Not IsDBNull(row(3)) Then
                                If Not IsNumeric(row(3)) Then
                                    TOPSPTransferPayment.TotalKliring = 0
                                Else
                                    TOPSPTransferPayment.TotalKliring = row(3)
                                End If
                            End If

                            If Not IsDBNull(row(4)) Then
                                If Not IsValidDate(row(4)) Then
                                    TOPSPTransferPayment.KliringDate = New Date(1900, 1, 1)
                                Else
                                    TOPSPTransferPayment.KliringDate = row(4).ToString
                                End If
                            End If

                            If IsDBNull(row(5)) Then
                                TOPSPTransferPayment.DocClearing = ""
                            Else
                                TOPSPTransferPayment.DocClearing = row(5).ToString
                            End If

                            If Not IsDBNull(row(6)) Then
                                If Not IsValidDate(row(6)) Then
                                    TOPSPTransferPayment.ActualDate = New Date(1900, 1, 1)
                                Else
                                    TOPSPTransferPayment.ActualDate = row(6).ToString
                                End If
                            End If

                            If IsDBNull(row(7)) Then
                                TOPSPTransferPayment.ReffBank = ""
                            Else
                                TOPSPTransferPayment.ReffBank = row(7).ToString
                            End If

                            If Not IsDBNull(row(1)) Then
                                If IsNumeric(row(1)) Then
                                    TOPSPTransferPayment.Dealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(row(1).ToString)
                                End If
                            End If
                            If IsDBNull(row(0)) AndAlso IsDBNull(row(1)) AndAlso IsDBNull(row(2)) AndAlso IsDBNull(row(3)) AndAlso IsDBNull(row(4)) AndAlso IsDBNull(row(5)) AndAlso IsDBNull(row(6)) Then
                            Else
                                TOPSPTransferPayment = ParseTOPSPTransferPayment(row)
                                _arrTOPSPTransferPayment.Add(TOPSPTransferPayment)
                            End If

                        Catch ex As Exception
                            Me.ErrorMessage.Append(ex.Message)
                        End Try
                    Next
                End If
            Catch ex As Exception
                Me.ErrorMessage.Append(ex.Message)
            End Try
            Return _arrTOPSPTransferPayment
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

        Private Function ParseTOPSPTransferPayment(ByVal row As DataRow) As TOPSPTransferPayment
            Me.ErrorMessage = New StringBuilder
            ErrorMessage.Clear()

            If Not IsDBNull(row(0)) AndAlso Not IsDBNull(row(1)) Then
                Dim data As ArrayList = New ArrayList
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TOPSPTransferPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TOPSPTransferPayment), "RegNumber", MatchType.Exact, row(0)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TOPSPTransferPayment), "Dealer.DealerCode", MatchType.Exact, row(1)))

                data = New TOPSPTransferPaymentFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                If data.Count = 0 Then
                    If ErrorMessage.ToString = "OK" Then
                        ErrorMessage.Clear()
                    End If
                    ErrorMessage.Append("Reg Number belum terdaftar")
                    If TOPSPTransferPayment.ErrorMessage = "" Then
                        TOPSPTransferPayment.ErrorMessage = "Reg Number belum terdaftar"
                    Else
                        TOPSPTransferPayment.ErrorMessage = TOPSPTransferPayment.ErrorMessage + "; Reg Number belum terdaftar"
                    End If
                    'Else
                    '    If CType(data(0), TOPSPTransferPayment).Status = 5 Then
                    '        If TOPSPTransferPayment.ErrorMessage = "" Then
                    '            TOPSPTransferPayment.ErrorMessage = "Status Data Transfer Payment sudah selesai"
                    '        Else
                    '            TOPSPTransferPayment.ErrorMessage = TOPSPTransferPayment.ErrorMessage + "; Status Data Transfer Payment sudah selesai"
                    '        End If
                    '    End If
                End If
            End If

            If IsDBNull(row(0)) Then
                If TOPSPTransferPayment.ErrorMessage = "" Then
                    TOPSPTransferPayment.ErrorMessage = "No Reg tidak boleh kosong"
                Else
                    TOPSPTransferPayment.ErrorMessage = TOPSPTransferPayment.ErrorMessage + "; No Reg tidak boleh kosong"
                End If
            End If

            If IsDBNull(row(1)) Then
                If TOPSPTransferPayment.ErrorMessage = "" Then
                    TOPSPTransferPayment.ErrorMessage = "Dealer tidak boleh kosong"
                Else
                    TOPSPTransferPayment.ErrorMessage = TOPSPTransferPayment.ErrorMessage + "; Dealer tidak boleh kosong"
                End If
            End If

            'If IsDBNull(row(2)) Then
            '    If TOPSPTransferPayment.ErrorMessage = "" Then
            '        TOPSPTransferPayment.ErrorMessage = "Billing Number tidak boleh kosong"
            '    Else
            '        TOPSPTransferPayment.ErrorMessage = TOPSPTransferPayment.ErrorMessage + "; Billing Number tidak boleh kosong"
            '    End If
            'End If

            If Not IsDBNull(row(2)) Then
                If Not IsNumeric(row(2)) Then
                    If TOPSPTransferPayment.ErrorMessage = "" Then
                        TOPSPTransferPayment.ErrorMessage = "Total Transfer Amount harus numerik dan tidak boleh 0"
                    Else
                        TOPSPTransferPayment.ErrorMessage = TOPSPTransferPayment.ErrorMessage + "; Total Amount harus numerik dan tidak boleh 0"
                    End If
                End If
            Else
                If TOPSPTransferPayment.ErrorMessage = "" Then
                    TOPSPTransferPayment.ErrorMessage = "Total Transfer Amount harus numerik dan tidak boleh 0 "
                Else
                    TOPSPTransferPayment.ErrorMessage = TOPSPTransferPayment.ErrorMessage + "; Total Amount harus numerik dan tidak boleh 0"
                End If
            End If

            'If Not IsDBNull(row(3)) Then
            '    If Not IsNumeric(row(3)) Then
            '        If TOPSPTransferPayment.ErrorMessage = "" Then
            '            TOPSPTransferPayment.ErrorMessage = "Kliring Amount harus numerik dan tidak boleh 0"
            '        Else
            '            TOPSPTransferPayment.ErrorMessage = TOPSPTransferPayment.ErrorMessage + "; Kliring Amount harus numerik dan tidak boleh 0"
            '        End If
            '    End If
            'Else
            '    If TOPSPTransferPayment.ErrorMessage = "" Then
            '        TOPSPTransferPayment.ErrorMessage = "Kliring Amount harus numerik dan tidak boleh 0 "
            '    Else
            '        TOPSPTransferPayment.ErrorMessage = TOPSPTransferPayment.ErrorMessage + "; Kliring Amount harus numerik dan tidak boleh 0"
            '    End If
            'End If

            If Not IsDBNull(row(4)) Then
                If Not IsValidDate(row(4)) Then
                    If TOPSPTransferPayment.ErrorMessage = "" Then
                        TOPSPTransferPayment.ErrorMessage = "Format Kliring Date salah"
                    Else
                        TOPSPTransferPayment.ErrorMessage = TOPSPTransferPayment.ErrorMessage + "; Format Kliring Date salah"
                    End If
                End If
                'Else
                '    If TOPSPTransferPayment.ErrorMessage = "" Then
                '        TOPSPTransferPayment.ErrorMessage = "Kliring Date tidak boleh kosong"
                '    Else
                '        TOPSPTransferPayment.ErrorMessage = TOPSPTransferPayment.ErrorMessage + "; Kliring Date tidak boleh kosong"
                '    End If
            End If

            'If IsDBNull(row(5)) Then
            '    If TOPSPTransferPayment.ErrorMessage = "" Then
            '        TOPSPTransferPayment.ErrorMessage = "No TR tidak boleh kosong"
            '    Else
            '        TOPSPTransferPayment.ErrorMessage = TOPSPTransferPayment.ErrorMessage + "; No TR tidak boleh kosong"
            '    End If
            'End If

            If Not IsDBNull(row(6)) Then
                If Not IsValidDate(row(6)) Then
                    If TOPSPTransferPayment.ErrorMessage = "" Then
                        TOPSPTransferPayment.ErrorMessage = "Format Actual Date salah"
                    Else
                        TOPSPTransferPayment.ErrorMessage = TOPSPTransferPayment.ErrorMessage + "; Format Actual Date salah"
                    End If
                End If
                'Else
                '    If TOPSPTransferPayment.ErrorMessage = "" Then
                '        TOPSPTransferPayment.ErrorMessage = "Actual Date tidak boleh kosong"
                '    Else
                '        TOPSPTransferPayment.ErrorMessage = TOPSPTransferPayment.ErrorMessage + "; Actual Date tidak boleh kosong"
                '    End If
            End If

            'If IsDBNull(row(7)) Then
            '    If TOPSPTransferPayment.ErrorMessage = "" Then
            '        TOPSPTransferPayment.ErrorMessage = "Reff Bank tidak boleh kosong"
            '    Else
            '        TOPSPTransferPayment.ErrorMessage = TOPSPTransferPayment.ErrorMessage + "; Reff Bank tidak boleh kosong"
            '    End If
            'End If

            If ErrorMessage.Length > 0 Then
                'RecallService.ErrorMessage = ErrorMessage.ToString
                If IsDataValid = True Then
                    IsDataValid = False
                End If
            End If

            If IsNothing(TOPSPTransferPayment.ErrorMessage) Then
                TOPSPTransferPayment.ErrorMessage = "OK"
            End If

            Return TOPSPTransferPayment
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

#End Region

    End Class
End Namespace

