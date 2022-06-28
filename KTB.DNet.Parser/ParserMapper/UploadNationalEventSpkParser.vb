#Region "Summary"
'// ===========================================================================		
'// Author Name   : Agus Pirnadi
'// PURPOSE       : 
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 14/10/2005
'// ===========================================================================		
#End Region

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
Imports System.Collections.Generic
Imports System.Linq
Imports System.Data.Linq.SqlClient
#End Region

#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.CallCenter
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade

#End Region

Namespace KTB.DNet.Parser

    Public Class UploadNationalEventSpkParser
        Inherits AbstractExcelParser

#Region "Private Variables"
        Private SpkNationalEventList As ArrayList
        Private _SpkNationalEvent As SPKNationalEvent
        Private _fileName As String
        Private ErrorMessage As StringBuilder
        Private IsDataValid As Boolean
        Private UserDealer As Dealer
        Private ContentFileType As String


#End Region


#Region "Protected Methods"

        Sub New(ByVal oUserDealer As Dealer, Optional ByVal contentFileType As String = "")
            Me.UserDealer = oUserDealer
            Me.ContentFileType = contentFileType
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
        ''' <returns>Object Data Customer Awal list</returns>
        Protected Overrides Function ParsingExcelNoTransaction(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object
            Me.ErrorMessage = New StringBuilder
            SpkNationalEventList = New ArrayList  '-- List of Customer 
            IsDataValid = True
            Try
                AbstractExcelParser.ContentTypeExcel = Me.ContentFileType
                Dim Ds As DataSet = ParseExcelDataSet(fileName, sheetName, "")

                If IsNothing(Ds) Then
                    Me.ErrorMessage.Append("Pastikan nama worksheetnya adalah Sheet1.")
                Else

                    'Validasi Column
                    'Dim critCol As New CriteriaComposite(New Criteria(GetType(CcTemplate), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    'critCol.opAnd(New Criteria(GetType(CcTemplate), "CcCustomerCategoryID", MatchType.Exact, 3))

                    'Dim sortCol As SortCollection = New SortCollection
                    'sortCol.Add(New Sort(GetType(CcTemplate), "ColNumber", Sort.SortDirection.ASC))

                    'Dim objTemplateList As ArrayList = New KTB.DNet.BusinessFacade.CallCenter.CcTemplateFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(critCol, sortCol)

                    'Dim objTemplateListCount As Integer = 22

                    'If Ds.Tables(0).Columns.Count < objTemplateListCount Then
                    '    Me.ErrorMessage.Append("Jumlah Kolom tidak sesuai. ")
                    'End If

                    'Dim colNames As String() = New String() {}
                    'Dim iCol As Integer = 0
                    'Dim dt As DataTable = Ds.Tables(0)
                    ''ReDim Preserve colNames(dt.Columns.Count - 1)
                    'ReDim Preserve colNames(objTemplateListCount - 1)

                    '--------------------------------------------
                    ''if using header title
                    'For colNum = 0 To dt.Columns.Count - 1
                    '    If iCol < objTemplateListCount Then
                    '        colNames(iCol) = dt.Rows(2)(colNum)
                    '        Dim data As String = colNames(iCol)
                    '        iCol = iCol + 1
                    '    End If
                    'Next
                    '--------------------------------------------

                    '--------------------------------------------
                    ''if not using header title
                    'For Each dc As DataColumn In dt.Columns
                    '    If iCol < objTemplateListCount Then
                    '        colNames(iCol) = dc.ColumnName
                    '        iCol = iCol + 1
                    '    End If
                    'Next
                    '--------------------------------------------

                    'If objTemplateListCount > 0 Then
                    '    For Each colName As String In colNames
                    '        If Not IsValidColumn(colName, objTemplateList) Then
                    '            Me.ErrorMessage.Append("Kolom '" & colName & "' tidak terdaftar. ")
                    '        End If
                    '    Next
                    'End If

                    'Max Row = 150

                    Dim rowAwal As Integer
                    ''if using header title
                    'rowAwal = 3

                    ''if not using header title
                    rowAwal = 0

                    'If Ds.Tables(0).Rows.Count > 101 Then
                    '    Dim dr As DataRow
                    '    Dim iR As Integer = 0
                    '    For iR = rowAwal To Ds.Tables(0).Rows.Count - 1
                    '        If iR > 101 - 1 Then
                    '            dr = Ds.Tables(0).Rows(iR)
                    '            If Not (IsDBNull(dr(1)) Or IsDBNull(dr(3))) Then
                    '                Me.ErrorMessage.Append("Jumlah baris lebih dari 100. ")
                    '                Exit For
                    '            End If
                    '        End If
                    '    Next
                    'End If

                    Dim row As DataRow

                    Dim i As Integer = 0
                    For i = rowAwal To Ds.Tables(0).Rows.Count - 1
                        row = Ds.Tables(0).Rows(i)
                        Try
                            If Not IsDBNull(row(0)) Then
                                _SpkNationalEvent = New SPKNationalEvent
                                _SpkNationalEvent = ParseCustomer(row)
                                SpkNationalEventList.Add(_SpkNationalEvent)
                            End If
                        Catch ex As Exception
                            Me.ErrorMessage.Append(ex.Message)
                        End Try
                    Next

                    If SpkNationalEventList.Count < 1 Then
                        Me.ErrorMessage.Append("Tidak ada data yang diinput.")
                        SpkNationalEventList.Clear()
                    End If

                    If SpkNationalEventList.Count > 100 Then
                        Me.ErrorMessage.Append("Jumlah baris tidak boleh lebih dari 100")
                        SpkNationalEventList.Clear()
                    End If

                    If Me.ErrorMessage.Length > 0 Then
                        IsDataValid = False
                        Return SpkNationalEventList
                        Exit Function
                    End If
                End If

            Catch ex As Exception
                Me.ErrorMessage.Append(ex.Message)
            End Try
            Return SpkNationalEventList
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

        Private Function ParseCustomer(ByVal row As DataRow) As SPKNationalEvent
            Me.ErrorMessage = New StringBuilder

            Dim col As Integer = -1
            Dim objNationalEventDetail As NationalEventDetail

            If Not IsDBNull(row(col + 19)) Then 'NationalEventID
                Try
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEventDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(NationalEventDetail), "NationalEvent.ID", MatchType.Exact, CInt(row(col + 19).ToString())))
                    criterias.opAnd(New Criteria(GetType(NationalEventDetail), "Dealer.ID", MatchType.Exact, row(col + 8).ToString()))
                    Dim arrNationalEventDetail As ArrayList = New NationalEventDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                    'objVehicleType = New VechileTypeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(row(col + 13).ToString())
                    If Not IsNothing(arrNationalEventDetail) AndAlso arrNationalEventDetail.Count > 0 Then
                        objNationalEventDetail = CType(arrNationalEventDetail(0), NationalEventDetail)
                        _SpkNationalEvent.NationalEvent = objNationalEventDetail.NationalEvent
                    Else
                        ErrorMessage.Append("Dealer " & row(col + 9).ToString & " belum melakukan registrasi." & Chr(13) & Chr(10))
                    End If
                Catch ex As Exception
                    ErrorMessage.Append("National Event ID atau Dealer ID tidak valid." & Chr(13) & Chr(10))
                End Try
            Else
                ErrorMessage.Append("National Event ID harus diisi." & Chr(13) & Chr(10))
            End If

            Dim strNoSPK As String = CType(row(col + 2), String)
            If Not IsDBNull(row(col + 2)) Then
                _SpkNationalEvent.SPKNumber = strNoSPK.Trim
            Else
                ErrorMessage.Append("No SPK harus diisi." & Chr(13) & Chr(10))
            End If

            If Not IsDBNull(row(col + 3)) Then
                Try
                    Dim _date As String = row(col + 3)
                    Dim _year As String = _date.Substring(0, 4)
                    Dim _month As String = _date.Substring(4, 2)
                    Dim _day As String = _date.Substring(6, 2)
                    Dim _spkDate As DateTime = New Date(_year, _month, _day)

                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(NationalEvent), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(NationalEvent), "ID", MatchType.Exact, CInt(row(col + 19).ToString())))
                    criterias.opAnd(New Criteria(GetType(NationalEvent), "PeriodStart", MatchType.LesserOrEqual, _spkDate))
                    criterias.opAnd(New Criteria(GetType(NationalEvent), "PeriodEnd", MatchType.GreaterOrEqual, _spkDate))
                    Dim arrNationalEvent As ArrayList = New NationalEventFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                    If Not IsNothing(arrNationalEvent) AndAlso arrNationalEvent.Count > 0 Then
                        _SpkNationalEvent.DealerSPKDate = _spkDate
                    Else
                        _SpkNationalEvent.DealerSPKDate = _spkDate

                        ErrorMessage.Append("Tanggal diluar periode National Event." & Chr(13) & Chr(10))
                    End If
                Catch ex As Exception
                    ErrorMessage.Append("Tanggal tidak valid." & Chr(13) & Chr(10))
                End Try               
            Else
                ErrorMessage.Append("Tanggal SPK harus diisi." & Chr(13) & Chr(10))
            End If

            Dim strNama As String = CType(row(col + 4), String)
            If Not IsDBNull(row(col + 4)) Then
                _SpkNationalEvent.CustomerName = strNama

            Else
                ErrorMessage.Append("Nama Konsumen harus diisi." & Chr(13) & Chr(10))
            End If

            'If Not IsDBNull(row(col + 5)) Then
            '    Dim objDealerGroup As DealerGroup = New DealerGroupFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(CInt(row(col + 54).ToString))
            '    If Not IsNothing(objDealerGroup) AndAlso (objDealerGroup.ID > 0) Then
            '        _SpkNationalEventUpload.GroupDealer = objDealerGroup.GroupName
            '    Else
            '        _SpkNationalEventUpload.GroupDealer = row(col + 5).ToString
            '        ErrorMessage.Append("Group Dealer " & row(col + 5).ToString & " tidak terdefinisi." & Chr(13) & Chr(10))
            '    End If
            'Else
            '    ErrorMessage.Append("Group Dealer harus diisi." & Chr(13) & Chr(10))
            'End If

            If Not IsDBNull(row(col + 9)) Then
                Try
                    Dim objDealer As Dealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(CInt(row(col + 8).ToString()))
                    If Not IsNothing(objDealer) AndAlso (objDealer.ID > 0) Then
                        _SpkNationalEvent.Dealer = objDealer

                    Else
                        _SpkNationalEvent.DealerName = row(col + 9).ToString
                        ErrorMessage.Append("Dealer " & row(col + 9).ToString & " tidak terdefinisi." & Chr(13) & Chr(10))
                    End If
                Catch ex As Exception
                    ErrorMessage.Append("Dealer ID tidak valid." & Chr(13) & Chr(10))
                End Try
            Else
                ErrorMessage.Append("Nama Dealer harus diisi." & Chr(13) & Chr(10))
            End If

            If Not IsDBNull(row(col + 8)) Then
                Try
                    Dim dealerID As Integer = CInt(row(col + 8).ToString)
                Catch ex As Exception
                    ErrorMessage.Append("Dealer ID tidak valid." & Chr(13) & Chr(10))
                End Try
            Else
                ErrorMessage.Append("Dealer ID harus diisi." & Chr(13) & Chr(10))
            End If

            'If Not IsDBNull(row(col + 8)) Then
            '    Dim strKota As String = IIf(DBNull.Value.Equals(row(col + 8)), String.Empty, row(col + 8))
            '    _SpkNationalEventUpload.DealerCity = strKota

            'Else
            '    ErrorMessage.Append("Kota harus diisi." & Chr(13) & Chr(10))
            'End If

            If Not IsDBNull(row(col + 5)) Then
                Try
                    Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(CInt(row(col + 7).ToString()))
                    If Not IsNothing(objSalesmanHeader) AndAlso (objSalesmanHeader.ID > 0) Then
                        Dim findSalesID As Integer = Array.Find(objNationalEventDetail.SalesmanID.Split(";"), Function(s) s = objSalesmanHeader.ID)
                        If findSalesID > 0 Then
                            _SpkNationalEvent.SalesmanHeader = objSalesmanHeader

                        Else
                            _SpkNationalEvent.SalesmanHeader = objSalesmanHeader

                            ErrorMessage.Append("Kode Salesman tidak terdaftar dalam event." & Chr(13) & Chr(10))
                        End If
                    Else
                        _SpkNationalEvent.SalesmanName = row(col + 5).ToString

                        ErrorMessage.Append("Kode Salesman " & row(col + 6).ToString & " tidak terdefinisi." & Chr(13) & Chr(10))
                    End If
                Catch ex As Exception
                    _SpkNationalEvent.SalesmanName = row(col + 5).ToString

                    ErrorMessage.Append("Kode Salesman " & row(col + 6).ToString & " tidak terdefinisi." & Chr(13) & Chr(10))
                End Try
            Else
                ErrorMessage.Append("Nama Salesman harus diisi." & Chr(13) & Chr(10))
            End If

            If Not IsDBNull(row(col + 6)) Then
                Dim strKodeSales As String = IIf(DBNull.Value.Equals(row(col + 6)), String.Empty, row(col + 6))
                _SpkNationalEvent.SalesmanCode = strKodeSales
            Else
                ErrorMessage.Append("Kode Salesman harus diisi." & Chr(13) & Chr(10))
            End If

            If Not IsDBNull(row(col + 7)) Then
                Try
                    Dim salesID As Integer = CInt(row(col + 7).ToString)
                Catch ex As Exception
                    ErrorMessage.Append("Salesman ID tidak valid." & Chr(13) & Chr(10))
                End Try
            Else
                ErrorMessage.Append("Salesman ID harus diisi." & Chr(13) & Chr(10))
            End If

            'To be Lead Source
            Dim objVehicleColor As VechileColor

            If Not IsDBNull(row(col + 10)) Then
                Try
                    Dim vehWarnaID As String = row(col + 11).ToString()
                    Dim arrVechileColor As ArrayList
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColor), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(VechileColor), "ID", MatchType.Exact, vehWarnaID))
                    criterias.opAnd(New Criteria(GetType(VechileColor), "Status", MatchType.Exact, ""))
                    arrVechileColor = New VechileColorFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                    If Not IsNothing(arrVechileColor) AndAlso arrVechileColor.Count > 0 Then
                        objVehicleColor = CType(arrVechileColor(0), VechileColor)
                        If Not IsNothing(objVehicleColor) AndAlso objVehicleColor.ID > 0 Then
                            _SpkNationalEvent.VechileColor = objVehicleColor

                        Else
                            _SpkNationalEvent.VehicleName = row(col + 10).ToString
                            _SpkNationalEvent.VehicleType = row(col + 10).ToString
                            _SpkNationalEvent.VehicleColor = row(col + 10).ToString

                            ErrorMessage.Append("Warna Kendaraan '" & row(col + 10) & "' tidak valid." & Chr(13) & Chr(10))
                        End If
                    Else
                        _SpkNationalEvent.VehicleName = row(col + 10).ToString
                        _SpkNationalEvent.VehicleType = row(col + 10).ToString
                        _SpkNationalEvent.VehicleColor = row(col + 10).ToString

                        ErrorMessage.Append("Warna Kendaraan '" & row(col + 10) & "' tidak valid." & Chr(13) & Chr(10))
                    End If
                Catch ex As Exception
                    _SpkNationalEvent.VehicleName = row(col + 10).ToString
                    _SpkNationalEvent.VehicleType = row(col + 10).ToString
                    _SpkNationalEvent.VehicleColor = row(col + 10).ToString
                    ErrorMessage.Append("Warna Kendaraan '" & row(col + 10) & "' tidak valid." & Chr(13) & Chr(10))
                End Try
            Else
                ErrorMessage.Append("Warna Kendaraan harus diisi." & Chr(13) & Chr(10))
            End If

            If Not IsDBNull(row(col + 11)) Then
                Try
                    Dim vehColorID = CInt(row(col + 11).ToString)
                Catch ex As Exception
                    ErrorMessage.Append("Kendaraan ID tidak valid." & Chr(13) & Chr(10))
                End Try
            Else
                ErrorMessage.Append("Kendaraan ID harus diisi." & Chr(13) & Chr(10))
            End If

            If Not IsDBNull(row(col + 12)) Then
                Try
                    _SpkNationalEvent.AssyYear = CInt(row(col + 12).ToString)
                Catch ex As Exception
                    ErrorMessage.Append("Tahun Produksi tidak valid." & Chr(13) & Chr(10))
                End Try
            Else
                ErrorMessage.Append("Tahun Produksi harus diisi." & Chr(13) & Chr(10))
            End If

            'If Not IsDBNull(row(col + 16)) Then
            '    Dim _date As String = row(col + 16)
            '    Dim _year As String = _date.Substring(0, 4)
            '    Dim _month As String = _date.Substring(4, 2)
            '    Dim _day As String = _date.Substring(6, 2)
            '    Dim _fakturDate As DateTime = New Date(_year, _month, _day)
            '    _SpkNationalEvent.EndCustomerPrintedTime = _fakturDate
            'Else
            '    ErrorMessage.Append("Tanggal Faktur harus diisi." & Chr(13) & Chr(10))
            'End If

            'If Not IsDBNull(row(col + 17)) Then
            '    '_SpkNationalEvent.CurrVehicleBrand = row(col + 17)

            'Else
            '    ErrorMessage.Append("Nomor Faktur harus diisi." & Chr(13) & Chr(10))
            'End If

            If Not IsDBNull(row(col + 13)) Then
                Try
                    _SpkNationalEvent.Quantity = CInt(row(col + 13).ToString)
                Catch ex As Exception
                    ErrorMessage.Append("Qty tidak valid." & Chr(13) & Chr(10))
                End Try
            Else
                ErrorMessage.Append("Qty harus diisi." & Chr(13) & Chr(10))
            End If

            If Not IsDBNull(row(col + 14)) Then
                Try
                    _SpkNationalEvent.DownPayment = CLng(row(col + 14).ToString)
                Catch ex As Exception
                    ErrorMessage.Append("Tanda Jadi tidak valid." & Chr(13) & Chr(10))
                End Try
            Else
                ErrorMessage.Append("Tanda Jadi harus diisi." & Chr(13) & Chr(10))
            End If

            If Not IsDBNull(row(col + 15)) Then
                Try
                    Dim paymentType As String = IIf(DBNull.Value.Equals(row(col + 15)), String.Empty, row(col + 15))
                    Select Case paymentType.Trim.ToLower
                        Case "kredit"
                            _SpkNationalEvent.PaymentType = New PaymentTypeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(2)
                        Case "tunai"
                            _SpkNationalEvent.PaymentType = New PaymentTypeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(1)
                            row(col + 15).ToString()
                        Case Else
                            _SpkNationalEvent.PaymentTypeTemp = row(col + 15).ToString
                            ErrorMessage.Append("Tipe Pembayaran tidak valid." & Chr(13) & Chr(10))
                    End Select
                Catch ex As Exception
                    _SpkNationalEvent.PaymentTypeTemp = row(col + 15).ToString
                    ErrorMessage.Append("Tipe Pembayaran tidak valid." & Chr(13) & Chr(10))
                End Try               
            Else
                ErrorMessage.Append("Type Pembayaran harus diisi." & Chr(13) & Chr(10))
            End If

            If Not IsDBNull(row(col + 16)) Then
                Try
                    Dim paymentType As String = IIf(DBNull.Value.Equals(row(col + 15)), String.Empty, row(col + 15))
                    If paymentType.ToLower = "kredit" Then
                        Dim leasingID As String = row(col + 17).ToString()
                        Dim arrLeasing As ArrayList
                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Leasing), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(Leasing), "ID", MatchType.Exact, leasingID))
                        arrLeasing = New LeasingFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                        If Not IsNothing(arrLeasing) AndAlso arrLeasing.Count > 0 Then
                            Dim objLeasing As Leasing = CType(arrLeasing(0), Leasing)
                            If Not IsNothing(objLeasing) AndAlso objLeasing.ID > 0 Then
                                _SpkNationalEvent.Leasing = objLeasing

                            Else
                                _SpkNationalEvent.LeasingTemp = row(col + 16).ToString

                                ErrorMessage.Append("Leasing '" & row(col + 16) & "' tidak valid." & Chr(13) & Chr(10))
                            End If
                        Else
                            _SpkNationalEvent.LeasingTemp = row(col + 16).ToString

                            ErrorMessage.Append("Leasing '" & row(col + 16) & "' tidak valid." & Chr(13) & Chr(10))
                        End If
                    End If
                Catch ex As Exception
                    _SpkNationalEvent.LeasingTemp = row(col + 16).ToString

                    ErrorMessage.Append("Leasing '" & row(col + 16) & "' tidak valid." & Chr(13) & Chr(10))
                End Try
            Else
                ErrorMessage.Append("Leasing harus diisi." & Chr(13) & Chr(10))

            End If

            If Not IsDBNull(row(col + 18)) Then
                _SpkNationalEvent.Remarks = row(col + 18).ToString
            Else
                ErrorMessage.Append("Remark harus diisi." & Chr(13) & Chr(10))
            End If

            If Not IsDBNull(row(col + 20)) Then
                Try
                    _SpkNationalEvent.Shift = row(col + 20).ToString
                Catch ex As Exception
                    ErrorMessage.Append("Shift tidak valid." & Chr(13) & Chr(10))
                End Try
            Else
                ErrorMessage.Append("Shift harus diisi." & Chr(13) & Chr(10))
            End If

            Try
                Dim criteriasSPK As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKNationalEvent), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriasSPK.opAnd(New Criteria(GetType(SPKNationalEvent), "SPKNumber", MatchType.Exact, strNoSPK))
                criteriasSPK.opAnd(New Criteria(GetType(SPKNationalEvent), "CustomerName", MatchType.Exact, strNama))
                criteriasSPK.opAnd(New Criteria(GetType(SPKNationalEvent), "SalesmanHeader.ID", MatchType.Exact, CInt(row(col + 7).ToString())))
                criteriasSPK.opAnd(New Criteria(GetType(SPKNationalEvent), "VechileColor.ID", MatchType.Exact, CInt(row(col + 11).ToString())))
                Dim arrSPK As ArrayList = New SPKNationalEventFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criteriasSPK)
                If Not IsNothing(arrSPK) AndAlso arrSPK.Count > 0 Then
                    ErrorMessage.Append("SPK ini sudah ada di database." & Chr(13) & Chr(10))
                End If
            Catch ex As Exception

            End Try

            If ErrorMessage.Length > 0 Then
                _SpkNationalEvent.ErrorMessage = ErrorMessage.ToString

                If IsDataValid = True Then
                    IsDataValid = False
                End If
            End If
            Return _SpkNationalEvent
        End Function

        Private Function IsCustomerExist(ByVal dealer As Dealer, ByVal phone As String, ByVal vehType As String) As Boolean
            Dim vReturn As Boolean = False
            Try
                Dim arrSPKNationalEvent As ArrayList
                Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPKNationalEvent), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteria.opAnd(New Criteria(GetType(SPKNationalEvent), "Phone", MatchType.Exact, phone))
                'criteria.opAnd(New Criteria(GetType(SPKNationalEvent), "Status", MatchType.No, CInt(EnumSPKNationalEventStatus.SPKNationalEventStatus.Deal_SPK)))
                criteria.opAnd(New Criteria(GetType(SPKNationalEvent), "Dealer.ID", MatchType.Exact, dealer.ID))
                If vehType <> String.Empty Then
                    criteria.opAnd(New Criteria(GetType(SPKNationalEvent), "VechileType.VechileTypeCode", MatchType.Exact, vehType))
                End If

                'arrSPKNationalEvent = New KTB.DNet.BusinessFacade.SAP.SPKNationalEventFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criteria)

                'If arrSPKNationalEvent.Count > 0 Then
                '    vReturn = True
                'End If
            Catch ex As Exception
                vReturn = True
            End Try
            Return vReturn
        End Function

        Private Function EmailAddressCheck(ByVal emailAddress As String) As Boolean
            Dim pattern As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
            Dim emailAddressMatch As Match = Regex.Match(emailAddress, pattern)
            If emailAddressMatch.Success Then
                EmailAddressCheck = True
            Else
                EmailAddressCheck = False
            End If
        End Function

        Private Function IsPhoneValid(ByVal phoneNo As String, Optional ByVal phoneType As String = "") As String
            Dim strMessage As String = String.Empty

            If phoneType = "Handphone" Then
                If Len(phoneNo) < 5 Then 'AndAlso Left(phoneNo, 2) <> "08" Then
                    strMessage += "No. Phone tidak valid"
                End If
            End If
            'If phoneNo.Length > 20 Then
            '    If phoneType <> "" Then
            '        strMessage += "No " & phoneType & " cukup satu nomor."
            '    Else
            '        strMessage += "Cukup masukkan satu nomor."
            '    End If
            'End If
            Return strMessage
        End Function


#End Region
    End Class

End Namespace

