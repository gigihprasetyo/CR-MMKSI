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

    Public Class UploadCustomerEntryParser
        Inherits AbstractExcelParser

#Region "Private Variables"
        Private CustomerList As ArrayList
        Private _Customer As SAPCustomer
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
            CustomerList = New ArrayList  '-- List of Customer 
            IsDataValid = True
            Try
                AbstractExcelParser.ContentTypeExcel = Me.ContentFileType
                Dim Ds As DataSet = ParseExcelDataSet(fileName, sheetName, "")

                If IsNothing(Ds) Then
                    Me.ErrorMessage.Append("Pastikan nama worksheetnya adalah Sheet1.")
                Else

                    'Validasi Column
                    Dim critCol As New CriteriaComposite(New Criteria(GetType(CcTemplate), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    critCol.opAnd(New Criteria(GetType(CcTemplate), "CcCustomerCategoryID", MatchType.Exact, 3))

                    Dim sortCol As SortCollection = New SortCollection
                    sortCol.Add(New Sort(GetType(CcTemplate), "ColNumber", Sort.SortDirection.ASC))

                    Dim objTemplateList As ArrayList = New KTB.DNet.BusinessFacade.CallCenter.CcTemplateFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(critCol, sortCol)

                    If Ds.Tables(0).Columns.Count < objTemplateList.Count Then
                        Me.ErrorMessage.Append("Jumlah Kolom tidak sesuai. ")
                    End If

                    Dim colNames As String() = New String() {}
                    Dim iCol As Integer = 0
                    Dim dt As DataTable = Ds.Tables(0)
                    'ReDim Preserve colNames(dt.Columns.Count - 1)
                    ReDim Preserve colNames(objTemplateList.Count - 1)

                    '--------------------------------------------
                    ''if using header title
                    'For colNum = 0 To dt.Columns.Count - 1
                    '    If iCol < objTemplateList.Count Then
                    '        colNames(iCol) = dt.Rows(2)(colNum)
                    '        Dim data As String = colNames(iCol)
                    '        iCol = iCol + 1
                    '    End If
                    'Next
                    '--------------------------------------------

                    '--------------------------------------------
                    ''if not using header title
                    For Each dc As DataColumn In dt.Columns
                        If iCol < objTemplateList.Count Then
                            colNames(iCol) = dc.ColumnName
                            iCol = iCol + 1
                        End If
                    Next
                    '--------------------------------------------

                    If objTemplateList.Count > 0 Then
                        For Each colName As String In colNames
                            If Not IsValidColumn(colName, objTemplateList) Then
                                Me.ErrorMessage.Append("Kolom '" & colName & "' tidak terdaftar. ")
                            End If
                        Next
                    End If

                    'Max Row = 150

                    Dim rowAwal As Integer
                    ''if using header title
                    'rowAwal = 3

                    ''if not using header title
                    rowAwal = 0

                    If Ds.Tables(0).Rows.Count > 150 Then
                        Dim dr As DataRow
                        Dim iR As Integer = 0
                        For iR = rowAwal To Ds.Tables(0).Rows.Count - 1
                            If iR > 150 - 1 Then
                                dr = Ds.Tables(0).Rows(iR)
                                If Not (IsDBNull(dr(1)) Or IsDBNull(dr(3))) Then
                                    Me.ErrorMessage.Append("Jumlah baris lebih dari 150. ")
                                End If
                            End If
                        Next
                    End If

                    If Me.ErrorMessage.Length > 0 Then
                        IsDataValid = False
                        Return CustomerList
                        Exit Function
                    End If

                    Dim row As DataRow
                    Dim i As Integer = 0
                    For i = rowAwal To Ds.Tables(0).Rows.Count - 1
                        row = Ds.Tables(0).Rows(i)
                        Try
                            If Not IsDBNull(row(0)) Then
                                _Customer = New SAPCustomer
                                _Customer = ParseCustomer(row)
                                CustomerList.Add(_Customer)
                            End If
                        Catch ex As Exception
                            Me.ErrorMessage.Append(ex.Message)
                        End Try
                    Next
                End If

            Catch ex As Exception
                Me.ErrorMessage.Append(ex.Message)
            End Try
            Return CustomerList
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

        Private Function ParseCustomer(ByVal row As DataRow) As SAPCustomer
            Me.ErrorMessage = New StringBuilder
            'Dealer
            _Customer.Dealer = Me.UserDealer

            Dim col As Integer = -1


            If Not IsDBNull(row(col + 1)) Then
                Dim CustomerType As String = IIf(DBNull.Value.Equals(row(col + 1)), String.Empty, row(col + 1))
                Select Case CustomerType.Trim.ToLower
                    Case "perorangan"
                        _Customer.CustomerType = 0
                    Case "perusahaan"
                        _Customer.CustomerType = 1
                    Case "bumn/pemerintah"
                        _Customer.CustomerType = 2
                    Case Else
                        _Customer.CustomerType = 3
                End Select
            Else
                ErrorMessage.Append("Tipe Konsumen harus diisi." & Chr(13) & Chr(10))
            End If

            If Not IsDBNull(row(col + 2)) Then
                Try
                    Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(row(col + 2).ToString())
                    If Not IsNothing(objSalesmanHeader) AndAlso (objSalesmanHeader.ID > 0) Then
                        _Customer.SalesmanHeader = objSalesmanHeader
                    Else
                        '_Customer.SalesmanHeader = Nothing
                        ErrorMessage.Append("Kode Salesman tidak terdefinisi." & Chr(13) & Chr(10))
                    End If
                Catch ex As Exception
                    ErrorMessage.Append("Kode Salesman tidak terdefinisi." & Chr(13) & Chr(10))
                End Try
            Else
                ErrorMessage.Append("Kode Salesman tidak terdefinisi." & Chr(13) & Chr(10))
            End If

            If Not IsDBNull(row(col + 3)) Then
                Dim strName As String = CType(row(col + 3), String)
                If Len(strName) > 40 Then
                    _Customer.CustomerName = strName.Trim
                    ErrorMessage.Append("Alamat melebihi 40 karakter." & Chr(13) & Chr(10))
                Else
                    _Customer.CustomerName = strName.Trim
                End If
            Else
                ErrorMessage.Append("Nama Konsumen harus diisi." & Chr(13) & Chr(10))
            End If

            If Not IsDBNull(row(col + 4)) Then
                Dim strAdress As String = CType(row(col + 4), String)
                If Len(strAdress) > 60 Then
                    _Customer.CustomerAddress = strAdress.Trim
                    ErrorMessage.Append("Alamat melebihi 60 karakter." & Chr(13) & Chr(10))
                Else
                    _Customer.CustomerAddress = strAdress.Trim
                End If

            Else
                ErrorMessage.Append("Alamat Konsumen harus diisi." & Chr(13) & Chr(10))
            End If

            If Not IsDBNull(row(col + 5)) Then
                'If EmailAddressCheck(row(col + 5)) Then
                _Customer.Email = row(col + 5)
                'Else
                '    ErrorMessage.Append("Email tidak valid." & Chr(13) & Chr(10))
                'End If
                'Else
                '    ErrorMessage.Append("Email Konsumen harus diisi." & Chr(13) & Chr(10))
            End If

            If Not IsDBNull(row(col + 6)) Then
                Dim strMsg As String = IsPhoneValid(row(col + 6), "Handphone")
                If strMsg = String.Empty Then
                    _Customer.Phone = row(col + 6)
                Else
                    ErrorMessage.Append(strMsg & Chr(13) & Chr(10))
                End If
            Else
                ErrorMessage.Append("Telp Konsumen harus diisi." & Chr(13) & Chr(10))
            End If

            If Not IsDBNull(row(col + 7)) Then
                Dim gender As String = IIf(DBNull.Value.Equals(row(col + 7)), String.Empty, row(col + 7))
                Select Case gender.Trim.ToLower
                    Case "pria"
                        _Customer.Sex = 1
                    Case "wanita"
                        _Customer.Sex = 2
                    Case Else
                        ErrorMessage.Append("Jenis Kelamin Salah." & Chr(13) & Chr(10))
                End Select
            Else
                ErrorMessage.Append("Jenis Kelamin harus diisi." & Chr(13) & Chr(10))
            End If

            If Not IsDBNull(row(col + 8)) Then
                Dim usia As String = IIf(DBNull.Value.Equals(row(col + 8)), String.Empty, row(col + 8))
                Select Case usia
                    Case "s/d 29 tahun"
                        _Customer.AgeSegment = 1
                    Case "30-39 tahun"
                        _Customer.AgeSegment = 2
                    Case "40-49 tahun"
                        _Customer.AgeSegment = 3
                    Case "50 tahun keatas"
                        _Customer.AgeSegment = 4
                    Case Else
                        ErrorMessage.Append("Usia Salah." & Chr(13) & Chr(10))
                End Select
            Else
                ErrorMessage.Append("Usia harus diisi." & Chr(13) & Chr(10))
            End If

            If Not IsDBNull(row(col + 9)) Then
                Dim _date As String = row(col + 9)
                Dim _year As String = _date.Substring(0, 4)
                Dim _month As String = _date.Substring(4, 2)
                Dim _day As String = _date.Substring(6, 2)
                Dim _birthDate As DateTime = New Date(_year, _month, _day)
                _Customer.BirthDate = _birthDate
            Else
                ErrorMessage.Append("Tanggal Lahir harus diisi." & Chr(13) & Chr(10))
            End If

            If Not IsDBNull(row(col + 10)) Then
                Dim status As String = IIf(DBNull.Value.Equals(row(col + 10)), String.Empty, row(col + 10))
                Select Case status.Trim.ToLower
                    Case "hot prospect"
                        _Customer.Status = 1
                    Case "prospect"
                        _Customer.Status = 2
                    Case "suspect"
                        _Customer.Status = 3
                    Case "Deal/spk"
                        _Customer.Status = 4
                    Case Else
                        ErrorMessage.Append("Status Salah." & Chr(13) & Chr(10))
                End Select
            Else
                ErrorMessage.Append("Status harus diisi." & Chr(13) & Chr(10))
            End If

            'To Be Information Source
            If Not IsDBNull(row(col + 11)) Then
                Dim informationType As String = IIf(DBNull.Value.Equals(row(col + 11)), String.Empty, row(col + 11))
                Select Case informationType.Trim.ToLower
                    Case "surat kabar"
                        _Customer.InformationType = 3
                    Case "televisi"
                        _Customer.InformationType = 4
                    Case "majalah"
                        _Customer.InformationType = 5
                    Case "radio"
                        _Customer.InformationType = 6
                    Case "papan reklame"
                        _Customer.InformationType = 7
                    Case "internet"
                        _Customer.InformationType = 8
                    Case "mobile apps"
                        _Customer.InformationType = 9
                    Case "social media"
                        _Customer.InformationType = 10
                    Case "kebetulan melintas"
                        _Customer.InformationType = 11
                    Case "database"
                        _Customer.InformationType = 12
                    Case Else
                        ErrorMessage.Append("Sumber Informasi Salah." & Chr(13) & Chr(10))
                End Select
            Else
                ErrorMessage.Append("Sumber Informasi harus diisi." & Chr(13) & Chr(10))
            End If

            'To be Lead Source
            If Not IsDBNull(row(col + 12)) Then
                Dim informationSource As String = IIf(DBNull.Value.Equals(row(col + 12)), String.Empty, row(col + 12))
                Select Case informationSource.Trim.ToLower
                    Case "rekomendasi"
                        _Customer.InformationSource = 5
                    Case "kunjungan sales"
                        _Customer.InformationSource = 6
                    Case "pameran/event/exhibition"
                        _Customer.InformationSource = 7
                    Case "walk in"
                        _Customer.InformationSource = 16
                    Case "database"
                        _Customer.InformationSource = 15
                    Case Else
                        ErrorMessage.Append("Sumber Lead Salah." & Chr(13) & Chr(10))
                End Select
            Else
                ErrorMessage.Append("Sumber Lead harus diisi." & Chr(13) & Chr(10))
            End If

            If Not IsDBNull(row(col + 13)) Then
                Dim customerPurpose As String = IIf(DBNull.Value.Equals(row(col + 13)), String.Empty, row(col + 13))
                Select Case customerPurpose.Trim.ToLower
                    Case "tanya kendaraan"
                        _Customer.CustomerPurpose = 1
                    Case "test drive"
                        _Customer.CustomerPurpose = 2
                    Case "memesan kendaraan"
                        _Customer.CustomerPurpose = 3
                    Case "tanya promosi"
                        _Customer.CustomerPurpose = 4
                    Case "tanya fasilitas dealer"
                        _Customer.CustomerPurpose = 5
                    Case "komplain"
                        _Customer.CustomerPurpose = 6
                    Case "mengantar saudara / teman"
                        _Customer.CustomerPurpose = 7
                    Case "lain lain"
                        _Customer.CustomerPurpose = 8
                    Case Else
                        ErrorMessage.Append("Tujuan Konsumen Salah." & Chr(13) & Chr(10))
                End Select
            End If

            Dim objVehicleType As VechileType
            If Not IsDBNull(row(col + 14)) Then
                Dim typeCode As String = row(col + 14).ToString()
                Dim arrVechileType As ArrayList
                Try
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(VechileType), "VechileTypeCode", MatchType.Exact, typeCode))
                    criterias.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.Exact, "A"))
                    arrVechileType = New VechileTypeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                    'objVehicleType = New VechileTypeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(row(col + 13).ToString())
                    If Not IsNothing(arrVechileType) AndAlso arrVechileType.Count > 0 Then
                        objVehicleType = CType(arrVechileType(0), VechileType)
                        If Not IsNothing(objVehicleType) AndAlso objVehicleType.ID > 0 Then
                            _Customer.VechileType = objVehicleType
                        Else
                            ErrorMessage.Append("Tipe Kendaraan '" & row(col + 14) & "' tidak valid." & Chr(13) & Chr(10))
                        End If
                    Else
                        ErrorMessage.Append("Tipe Kendaraan '" & row(col + 14) & "' tidak valid." & Chr(13) & Chr(10))
                    End If
                Catch ex As Exception
                    ErrorMessage.Append("Tipe Kendaraan '" & row(col + 14) & "' tidak valid." & Chr(13) & Chr(10))
                End Try
            Else
                ErrorMessage.Append("Tipe Kendaraan harus diisi." & Chr(13) & Chr(10))
            End If

            If Not IsDBNull(row(col + 15)) Then
                _Customer.Qty = row(col + 15)
            Else
                ErrorMessage.Append("Qty harus diisi." & Chr(13) & Chr(10))
            End If

            If Not IsDBNull(row(col + 16)) Then
                Dim _date As String = row(col + 16)
                Dim _year As String = _date.Substring(0, 4)
                Dim _month As String = _date.Substring(4, 2)
                Dim _day As String = _date.Substring(6, 2)
                Dim _prospectDate As DateTime = New Date(_year, _month, _day)
                _Customer.ProspectDate = _prospectDate
            Else
                ErrorMessage.Append("Tanggal Prospek harus diisi." & Chr(13) & Chr(10))
            End If

            If Not IsDBNull(row(col + 17)) Then
                _Customer.CurrVehicleBrand = row(col + 17)
            Else
                _Customer.CurrVehicleBrand = ""
            End If

            If Not IsDBNull(row(col + 18)) Then
                _Customer.CurrVehicleType = row(col + 18)
            Else
                _Customer.CurrVehicleType = ""
            End If

            If Not IsDBNull(row(col + 19)) Then
                Dim _date As String = row(col + 19)
                Dim _year As String = _date.Substring(0, 4)
                Dim _month As String = _date.Substring(4, 2)
                Dim _day As String = _date.Substring(6, 2)
                Dim _tglRencanaBeli As DateTime = New Date(_year, _month, _day)
                _Customer.EstimatedCloseDate = _tglRencanaBeli
            End If

            If Not IsDBNull(row(col + 20)) Then
                Dim statusFollowUp As String = IIf(DBNull.Value.Equals(row(col + 20)), String.Empty, row(col + 20))
                Select Case statusFollowUp.Trim.ToLower
                    Case "new"
                        _Customer.LeadStatus = 1
                    Case "contacted"
                        _Customer.LeadStatus = 2
                    Case "qualified"
                        _Customer.LeadStatus = 3
                    Case "lost"
                        _Customer.LeadStatus = 4
                    Case "can not contact"
                        _Customer.LeadStatus = 5
                    Case "no longer interested"
                        _Customer.LeadStatus = 6
                    Case "canceled"
                        _Customer.LeadStatus = 7
                    Case Else
                        ErrorMessage.Append("Status Follow Up Salah." & Chr(13) & Chr(10))
                End Select
            End If

            If Not IsDBNull(row(col + 21)) Then
                Dim statusAkhir As String = IIf(DBNull.Value.Equals(row(col + 21)), String.Empty, row(col + 21))
                Select Case statusAkhir.Trim.ToLower
                    Case "open"
                        _Customer.StateCode = 0
                    Case "won"
                        _Customer.StateCode = 1
                    Case "lost"
                        _Customer.StateCode = 2
                    Case Else
                        ErrorMessage.Append("Status Akhir Salah." & Chr(13) & Chr(10))
                End Select
            End If

            If Not IsDBNull(row(col + 22)) Then
                Dim alasanBatal As String = IIf(DBNull.Value.Equals(row(col + 22)), String.Empty, row(col + 22))
                Select Case alasanBatal.Trim.ToLower
                    Case "on progress"
                        _Customer.StatusCode = 1
                    Case "on hold"
                        _Customer.StatusCode = 2
                    Case "won"
                        _Customer.StatusCode = 3
                    Case "canceled"
                        _Customer.StatusCode = 4
                    Case "on sold"
                        _Customer.StatusCode = 5
                    Case Else
                        ErrorMessage.Append("Alasan Batal Salah." & Chr(13) & Chr(10))
                End Select
            End If

            ' dealer event
            If Not IsDBNull(row(col + 23)) Then
                _Customer.CampaignName = row(col + 23)
            Else
                _Customer.CampaignName = ""
            End If

            If Not IsDBNull(row(col + 24)) Then
                Dim sectorView As VWI_BusinessSector = New VWI_BusinessSectorFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).GetVWI_BusinessSectorByName(row(col + 24))
                If Not IsNothing(sectorView) Then
                    Dim sectorDetail As BusinessSectorDetail = New BusinessSectorDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sectorView.ID)
                    _Customer.BusinessSectorDetail = sectorDetail
                Else
                    ErrorMessage.Append("Data Industri '" & row(col + 24) & "' tidak terdaftar")
                End If
            End If

            If Not IsDBNull(row(col + 25)) Then
                _Customer.Description = row(col + 25)
            Else
                _Customer.Description = ""
            End If

            If Not IsDBNull(row(col + 26)) Then
                _Customer.Note = row(col + 26)
            Else
                _Customer.Note = ""
            End If

            If Not IsNothing(objVehicleType) Then
                If IsCustomerExist(_Customer.Dealer, _Customer.Phone, objVehicleType.VechileTypeCode) Then
                    ErrorMessage.Append("Data konsumen sudah ada (Kode Dealer, No HP, tipe kendaraan)" & Chr(13) & Chr(10))
                Else
                    If IsCustomerExist(_Customer.Dealer, _Customer.Phone, String.Empty) Then
                        ErrorMessage.Append("Data konsumen sudah ada (Kode Dealer, No HP)" & Chr(13) & Chr(10))
                    End If
                End If
            End If

            If ErrorMessage.Length > 0 Then
                _Customer.ErrorMessage = ErrorMessage.ToString
                If IsDataValid = True Then
                    IsDataValid = False
                End If
            End If

            Return _Customer
        End Function

        Private Function IsCustomerExist(ByVal dealer As Dealer, ByVal phone As String, ByVal vehType As String) As Boolean
            Dim vReturn As Boolean = False
            Try
                Dim arrSAPCustomer As ArrayList
                Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SAPCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteria.opAnd(New Criteria(GetType(SAPCustomer), "Phone", MatchType.Exact, phone))
                criteria.opAnd(New Criteria(GetType(SAPCustomer), "Status", MatchType.No, CInt(EnumSAPCustomerStatus.SAPCustomerStatus.Deal_SPK)))
                criteria.opAnd(New Criteria(GetType(SAPCustomer), "Dealer.ID", MatchType.Exact, dealer.ID))
                If vehType <> String.Empty Then
                    criteria.opAnd(New Criteria(GetType(SAPCustomer), "VechileType.VechileTypeCode", MatchType.Exact, vehType))
                End If

                arrSAPCustomer = New KTB.DNet.BusinessFacade.SAP.SAPCustomerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criteria)

                If arrSAPCustomer.Count > 0 Then
                    vReturn = True
                End If
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