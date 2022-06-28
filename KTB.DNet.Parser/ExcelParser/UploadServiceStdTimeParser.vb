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
Imports System.Data.SqlClient
#End Region

#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.StandardCodeFacade
Imports KTB.DNet.BusinessFacade.Service


#End Region

Namespace KTB.DNet.Parser

    Public Class UploadServiceStdTimeParser
        Inherits AbstractExcelParser

#Region "Private Variables"
        Private SSTList As ArrayList
        Private _sst As ServiceStandardTime
        Private _fileName As String
        Private ErrorMessage As StringBuilder
        Private periodID As Integer
        Private IsDataValid As Boolean
        Private UserDealer As Dealer
        Private StrPeriod As String
        Private ContentFileType As String
        Private _OCCVOdometer As List(Of CCVehicleOdometer)
        

#End Region


#Region "Protected Methods"

        Sub New(ByVal oUserDealer As Dealer, Optional ByVal contentFileType As String = "")
            'Me.StrPeriod = oStrPeriod
            'Me.periodID = oPeriodID
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
        ''' <returns>Object Data Customer list</returns>
        ''' <remarks>Pake Method Baru</remarks>
        Protected Overrides Function ParsingExcelNoTransaction(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object
            Me.ErrorMessage = New StringBuilder
            SSTList = New ArrayList  '-- List of Material Promotion 
            IsDataValid = True
            Try
                AbstractExcelParser.ContentTypeExcel = Me.ContentFileType
                Dim Ds As DataSet = ParseExcelDataSet(fileName, sheetName, "")
                If IsNothing(Ds) Then
                    Me.ErrorMessage.Append("Pastikan nama worksheetnya adalah Sheet1.")
                Else

                    'Validasi Column
                    Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SstTemplate), "SstTemplateCategoryID", MatchType.Exact, 1)) 'ASS
                    Dim sortCol As SortCollection = New SortCollection
                    sortCol.Add(New Sort(GetType(SstTemplate), "ColNumber", Sort.SortDirection.ASC))

                    Dim objTemplateList As ArrayList = New KTB.DNet.BusinessFacade.Service.SSTTemplateFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(critCol, sortCol)

                    If Not IsNothing(Ds.Tables(0)) Then
                        If Ds.Tables(0).Columns.Count < objTemplateList.Count Then
                            Me.ErrorMessage.Append("Jumlah Kolom tidak sesuai. ")
                        End If

                        Dim colNames As String() = New String() {}
                        Dim iCol As Integer = 0
                        Dim dt As DataTable = Ds.Tables(0)
                        'ReDim Preserve colNames(dt.Columns.Count - 1)
                        ReDim Preserve colNames(objTemplateList.Count - 1)

                        For Each dc As DataColumn In dt.Columns
                            If iCol < objTemplateList.Count Then
                                colNames(iCol) = dc.ColumnName
                                iCol = iCol + 1
                            End If
                        Next

                        If objTemplateList.Count > 0 Then
                            For Each colName As String In colNames
                                If Not IsValidColumn(colName, objTemplateList) Then
                                    Me.ErrorMessage.Append("Kolom '" & colName & "' tidak terdaftar. ")
                                End If
                            Next
                        End If

                        ''Max Row = 150
                        'If Ds.Tables(0).Rows.Count > 150 Then
                        '    Dim dr As DataRow
                        '    Dim iR As Integer = 0
                        '    For iR = 0 To Ds.Tables(0).Rows.Count - 1
                        '        If iR > 150 - 1 Then
                        '            dr = Ds.Tables(0).Rows(iR)
                        '            If Not (IsDBNull(dr(1)) Or IsDBNull(dr(3))) Then
                        '                Me.ErrorMessage.Append("Jumlah baris lebih dari 150. ")
                        '            End If
                        '        End If
                        '    Next
                        'End If

                        If Me.ErrorMessage.Length > 0 Then
                            IsDataValid = False
                            Return SSTList
                            Exit Function
                        End If

                        Dim row As DataRow
                        Dim i As Integer = 0
                        For i = 0 To Ds.Tables(0).Rows.Count - 1
                            row = Ds.Tables(0).Rows(i)
                            Try

                                If Not IsDBNull(row(0)) And Not IsDBNull(row(1)) Then
                                    _sst = New ServiceStandardTime

                                    _sst = ParseCcContact(row)
                                    SSTList.Add(_sst)
                                ElseIf IsDBNull(row(0)) And Not IsDBNull(row(1)) Then
                                    'Me.ErrorMessage.Append("Invalid No Urut. Masukkan No Urut yang valid." & Chr(13) & Chr(10))
                                    _sst = New ServiceStandardTime

                                    _sst = ParseCcContact(row)
                                    SSTList.Add(_sst)
                                ElseIf IsDBNull(row(0)) And IsDBNull(row(1)) Then
                                    Me.ErrorMessage.Append("Tidak Ada Data Yang Di Upload.")
                                Else
                                    _sst = New ServiceStandardTime

                                    _sst = ParseCcContact(row)
                                    SSTList.Add(_sst)
                                End If
                            Catch ex As Exception

                            End Try
                        Next
                    Else
                        Me.ErrorMessage.Append("Tidak Ada Data Yang Di Upload.")
                    End If

                    
                End If

            Catch ex As Exception
                'Me.ErrorMessage.Append(ex.Message)
                Me.ErrorMessage.Append("Tidak Ada Data Yang Di Upload.")
            End Try
            Return SSTList
        End Function

#Region "Old ParsingExcelNoTransaction"
        'Protected Overrides Function ParsingExcelNoTransaction(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object
        '    Me.ErrorMessage = New StringBuilder
        '    CcContactList = New ArrayList  '-- List of Material Promotion 
        '    IsDataValid = True
        '    Try
        '        AbstractExcelParser.ContentTypeExcel = Me.ContentFileType
        '        Dim Ds As DataSet = ParseExcelDataSet(fileName, sheetName, "")
        '        If IsNothing(Ds) Then
        '            Me.ErrorMessage.Append("Pastikan nama worksheetnya adalah Sheet1.")
        '        Else

        '            'Validasi Column
        '            Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CcTemplate), "CcCustomerCategoryID", MatchType.Exact, 2)) 'ASS
        '            Dim sortCol As SortCollection = New SortCollection
        '            sortCol.Add(New Sort(GetType(CcTemplate), "ColNumber", Sort.SortDirection.ASC))

        '            Dim objTemplateList As ArrayList = New KTB.DNet.BusinessFacade.CallCenter.CcTemplateFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(critCol, sortCol)

        '            If Ds.Tables(0).Columns.Count < objTemplateList.Count Then
        '                Me.ErrorMessage.Append("Jumlah Kolom tidak sesuai. ")
        '            End If

        '            Dim colNames As String() = New String() {}
        '            Dim iCol As Integer = 0
        '            Dim dt As DataTable = Ds.Tables(0)
        '            'ReDim Preserve colNames(dt.Columns.Count - 1)
        '            ReDim Preserve colNames(objTemplateList.Count - 1)

        '            For Each dc As DataColumn In dt.Columns
        '                If iCol < objTemplateList.Count Then
        '                    colNames(iCol) = dc.ColumnName
        '                    iCol = iCol + 1
        '                End If
        '            Next

        '            If objTemplateList.Count > 0 Then
        '                For Each colName As String In colNames
        '                    If Not IsValidColumn(colName, objTemplateList) Then
        '                        Me.ErrorMessage.Append("Kolom '" & colName & "' tidak terdaftar. ")
        '                    End If
        '                Next
        '            End If

        '            'Max Row = 150
        '            If Ds.Tables(0).Rows.Count > 150 Then
        '                Dim dr As DataRow
        '                Dim iR As Integer = 0
        '                For iR = 0 To Ds.Tables(0).Rows.Count - 1
        '                    If iR > 150 - 1 Then
        '                        dr = Ds.Tables(0).Rows(iR)
        '                        If Not (IsDBNull(dr(1)) Or IsDBNull(dr(3))) Then
        '                            Me.ErrorMessage.Append("Jumlah baris lebih dari 150. ")
        '                        End If
        '                    End If
        '                Next
        '            End If

        '            If Me.ErrorMessage.Length > 0 Then
        '                IsDataValid = False
        '                Return CcContactList
        '                Exit Function
        '            End If

        '            Dim row As DataRow
        '            Dim i As Integer = 0
        '            For i = 0 To Ds.Tables(0).Rows.Count - 1
        '                row = Ds.Tables(0).Rows(i)
        '                Try
        '                    If Not IsDBNull(row(0)) Then
        '                        _ccContact = New CcContact
        '                        _ccContact.CcPeriodID = Me.periodID
        '                        _ccContact.CcCustomerCategoryID = 2 'ASS
        '                        _ccContact.CcContactStatusID = 1 'Initial
        '                        _ccContact = ParseCcContact(row)
        '                        CcContactList.Add(_ccContact)
        '                    End If
        '                Catch ex As Exception

        '                End Try
        '            Next
        '        End If

        '    Catch ex As Exception
        '        Me.ErrorMessage.Append(ex.Message)
        '    End Try
        '    Return CcContactList
        'End Function
#End Region


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

        Private Function ParseCcContact(ByVal row As DataRow) As ServiceStandardTime

            Dim objDealer As Dealer

            Dim objVechileModel As VechileModel

            

            Me.ErrorMessage = New StringBuilder
            'Dealer
            'If Not IsDBNull(row(1)) Then
            '    Try
            '        Dim strDealer As String = CType(row(1), String)
            '        objDealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(strDealer)
            '        If objDealer.ID > 0 Then
            '            _sst.Dealer = objDealer
            '            If objDealer.ID <> UserDealer.ID Then
            '                ErrorMessage.Append("Kode Dealer tidak sesuai." & Chr(13) & Chr(10))
            '            End If
            '        Else
            '            ErrorMessage.Append("Kode Dealer " & strDealer & " tidak terdefinisi." & Chr(13) & Chr(10))
            '        End If
            '    Catch ex As Exception
            '        ErrorMessage.Append("Kode Dealer tidak terdefinisi." & Chr(13) & Chr(10))
            '    End Try
            'Else
            '    ErrorMessage.Append("Kode Dealer tidak terdefinisi." & Chr(13) & Chr(10))
            'End If

            'If Not IsDBNull(row(0)) Then
            '    Try
            '        If CStr(row(0)).Trim.Length > 0 Then
            '            If IsNumeric(CType(row(0), String)) Then
            '                '_sst.AssistServiceTypeCode = CType(row(1), String)
            '            Else
            '                ErrorMessage.Append("Invalid No. Masukkan No Urut yang valid." & Chr(13) & Chr(10))
            '            End If

            '        End If
            '    Catch ex As Exception
            '        'ErrorMessage.Append("Keterangan Bpk/Ibu harus diisi." & Chr(13) & Chr(10))
            '    End Try
            'Else
            '    ErrorMessage.Append("Harap Mengisi No Urut." & Chr(13) & Chr(10))
            'End If

            If Not IsDBNull(row(1)) Then
                Try
                    If CStr(row(1)).Trim.Length > 0 Then
                        If CType(row(1), String) = "Regular" Or CType(row(1), String) = "MQP" Then
                            _sst.AssistServiceTypeCode = CType(row(1), String)
                        Else
                            ErrorMessage.Append("Assist Service Type Tidak terdefinisi." & Chr(13) & Chr(10))
                        End If

                    End If
                Catch ex As Exception
                    'ErrorMessage.Append("Keterangan Bpk/Ibu harus diisi." & Chr(13) & Chr(10))
                End Try
            Else
                ErrorMessage.Append("Assist Service Type Harus Diisi." & Chr(13) & Chr(10))
            End If

            If Not IsDBNull(row(2)) Then
                Try
                    Dim objVechileType As VechileType
                    Dim strVechileType As String = CType(row(2), String)

                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileType), "Status", MatchType.Exact, "A"))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileType), "VechileTypeCode", MatchType.Exact, strVechileType))
                    'Dim results As ArrayList = vechileTypeFacade.Retrieve(crit)
                    Dim arrVM As ArrayList = New VechileTypeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                    If arrVM.Count > 0 Then
                        If Not IsNothing(arrVM) AndAlso arrVM.Count > 0 Then
                            objVechileType = CType(arrVM(0), VechileType)
                            _sst.VechileType = objVechileType
                        End If
                    Else
                        ErrorMessage.Append("Kode Tipe Kendaraan tidak Terdefinisi." & Chr(13) & Chr(10))
                    End If
                Catch ex As Exception
                    ErrorMessage.Append("Kode Tipe Kendaraan tidak terdefinisi." & Chr(13) & Chr(10))
                End Try
            Else
                ErrorMessage.Append("Kode Tipe Kendaraan Harus Diisi." & Chr(13) & Chr(10))
            End If
            If Not IsDBNull(row(3)) Then
                Try
                    If IsNothing(_sst.VechileType) Then
                        ErrorMessage.Append("Deskripsi Kendaraan tidak Terdefinisi." & Chr(13) & Chr(10))
                    Else
                        Dim objVechileType As VechileType
                        Dim strVechileType2 As String = CType(row(2), String)
                        Dim strVechileType As String = CType(row(3), String)

                        Dim criteriass As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criteriass.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileType), "Status", MatchType.Exact, "A"))
                        criteriass.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileType), "VechileTypeCode", MatchType.Exact, strVechileType2))
                        criteriass.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileType), "Description", MatchType.Exact, strVechileType))
                        'Dim results As ArrayList = vechileTypeFacade.Retrieve(crit)
                        Dim arrVt As ArrayList = New VechileTypeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criteriass)
                        If arrVt.Count > 0 Then
                            If Not IsNothing(arrVt) AndAlso arrVt.Count > 0 Then
                                objVechileType = CType(arrVt(0), VechileType)
                                _sst.VechileType = objVechileType
                            End If
                        Else
                            _sst.VechileType.Description = ""
                            ErrorMessage.Append("Deskripsi Kendaraan tidak Terdefinisi." & Chr(13) & Chr(10))

                        End If
                    End If
                    
                Catch ex As Exception
                    _sst.VechileType.Description = ""
                    ErrorMessage.Append("Deskripsi Kendaraan tidak terdefinisi." & Chr(13) & Chr(10))
                End Try
            Else
                ErrorMessage.Append("Deskripsi Kendaraan Harus Diisi." & Chr(13) & Chr(10))
            End If
            If Not IsDBNull(row(4)) Then
                Try
                    Dim objStandardCode As StandardCode
                    Dim strStandardCode As String = CType(row(4), String)

                    Dim criteriasss As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criteriasss.opAnd(New Criteria(GetType(KTB.DNet.Domain.StandardCode), "Category", MatchType.Exact, "ServiceBooking.ServiceType"))
                    criteriasss.opAnd(New Criteria(GetType(KTB.DNet.Domain.StandardCode), "ValueDesc", MatchType.Exact, strStandardCode))

                    'Dim results As ArrayList = vechileTypeFacade.Retrieve(crit)
                    Dim arrSc As ArrayList = New KTB.DNet.BusinessFacade.StandardCodeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criteriasss)
                    If arrSc.Count > 0 Then
                        If Not IsNothing(arrSc) AndAlso arrSc.Count > 0 Then
                            objStandardCode = CType(arrSc(0), StandardCode)
                            _sst.ServiceTypeID = objStandardCode.ValueId
                        End If
                    Else
                        ErrorMessage.Append("Jenis Kegiatan tidak Terdefinisi." & Chr(13) & Chr(10))
                    End If
                Catch ex As Exception
                    ErrorMessage.Append("Jenis Kegiatan tidak terdefinisi." & Chr(13) & Chr(10))
                End Try
            Else
                ErrorMessage.Append("Jenis Kegiatan Harus Diisi." & Chr(13) & Chr(10))
            End If
            If Not IsDBNull(row(5)) Then
                Try
                    Dim strKindCode As String = CType(row(5), String)
                    Dim objFSKind As FSKind
                    Dim objPMKind As PMKind
                    Dim objRecallCategory As RecallCategory
                    Dim objGRKind As GRKind

                    Select Case _sst.ServiceTypeID
                        Case 1
                            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            'criterias2.opAnd(New Criteria(GetType(FSKind), "KindDescription", MatchType.Exact, strKindCode))
                            criterias2.opAnd(New Criteria(GetType(FSKind), "KindCode", MatchType.Exact, strKindCode))
                            Dim arrFSK2 As ArrayList = New FSKindFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias2)
                            If Not IsNothing(arrFSK2) AndAlso arrFSK2.Count > 0 Then
                                objFSKind = CType(arrFSK2(0), FSKind)
                                _sst.KindCode = objFSKind.KindCode
                            Else
                                ErrorMessage.Append("Jenis Service tidak terdefinisi." & Chr(13) & Chr(10))
                            End If

                        Case 2
                            Dim criterias0 As New CriteriaComposite(New Criteria(GetType(PMKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            'criterias0.opAnd(New Criteria(GetType(PMKind), "KindDescription", MatchType.Exact, strKindCode))
                            criterias0.opAnd(New Criteria(GetType(PMKind), "KindCode", MatchType.Exact, strKindCode))
                            Dim arrFSK As ArrayList = New PMKindFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias0)
                            If Not IsNothing(arrFSK) AndAlso arrFSK.Count > 0 Then
                                objPMKind = CType(arrFSK(0), PMKind)
                                _sst.KindCode = objPMKind.KindCode
                            Else
                                ErrorMessage.Append("Jenis Service tidak terdefinisi." & Chr(13) & Chr(10))
                            End If
                        Case 3
                            Dim criteriasa As New CriteriaComposite(New Criteria(GetType(RecallCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            'criteriasa.opAnd(New Criteria(GetType(RecallCategory), "Description", MatchType.Exact, strKindCode))
                            criteriasa.opAnd(New Criteria(GetType(RecallCategory), "RecallRegNo", MatchType.Exact, strKindCode))
                            Dim arrRC As ArrayList = New RecallCategoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criteriasa)
                            If Not IsNothing(arrRC) AndAlso arrRC.Count > 0 Then
                                objRecallCategory = CType(arrRC(0), RecallCategory)
                                _sst.KindCode = objRecallCategory.RecallRegNo
                            Else
                                ErrorMessage.Append("Jenis Service tidak terdefinisi." & Chr(13) & Chr(10))
                            End If
                        Case 4
                            Dim criteriasa As New CriteriaComposite(New Criteria(GetType(GRKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            'criteriasa.opAnd(New Criteria(GetType(RecallCategory), "Description", MatchType.Exact, strKindCode))
                            criteriasa.opAnd(New Criteria(GetType(GRKind), "KindCode", MatchType.Exact, strKindCode))
                            Dim arrGR As ArrayList = New GRKindFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criteriasa)
                            If Not IsNothing(arrGR) AndAlso arrGR.Count > 0 Then
                                objGRKind = CType(arrGR(0), GRKind)
                                _sst.KindCode = objGRKind.KindCode
                            Else
                                ErrorMessage.Append("Jenis Service tidak terdefinisi." & Chr(13) & Chr(10))
                            End If
                    End Select

                   

                Catch ex As Exception
                    ErrorMessage.Append("Jenis Service tidak terdefinisi." & Chr(13) & Chr(10))
                End Try
            Else
                ErrorMessage.Append("Jenis Service Harus Diisi." & Chr(13) & Chr(10))
            End If
            If Not IsDBNull(row(6)) Then
                Try
                    'Dim decimalSeparator As String = Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator
                    Dim STD As Decimal
                    Dim strStd As String = CType(row(6), String) 'row(6).Replace(".", decimalSeparator).Replace(",", decimalSeparator) 'row(6) 'CType(row(6), String)
                    If Decimal.TryParse(strStd, STD) Then
                        _sst.DealerStandardTime = STD
                    Else
                        ErrorMessage.Append("Standard Waktu Dealer Harus diisi Decimal." & Chr(13) & Chr(10))
                    End If
                Catch ex As Exception
                    ErrorMessage.Append("Standard Waktu Dealer Harus diisi Decimal." & Chr(13) & Chr(10))
                End Try
            Else
                ErrorMessage.Append("Standard Waktu Dealer Harus diisi." & Chr(13) & Chr(10))
            End If

            'If Not IsDBNull(row(7)) Then
            '    Try
            '        Dim STD As Decimal
            '        Dim strStd As String = CType(row(7), String)
            '        If Decimal.TryParse(strStd, STD) Then
            '            _sst.DealerStandardTime = STD
            '        Else
            '            ErrorMessage.Append("Standard Waktu Dealer Harus diisi Decimal." & Chr(13) & Chr(10))
            '        End If
            '    Catch ex As Exception
            '        ErrorMessage.Append("Standard Waktu Dealer Harus diisi Decimal." & Chr(13) & Chr(10))
            '    End Try
            'Else
            '    ErrorMessage.Append("Standard Waktu Dealer Harus diisi." & Chr(13) & Chr(10))
            'End If

            '_sst.SystemStandardTime = 0
            If Not IsDBNull(row(7)) Then
                Try
                    Dim STD As Decimal
                    Dim strStd As String = CType(row(7), String)
                    If Decimal.TryParse(strStd, STD) Then
                        _sst.SystemStandardTime = STD
                    Else
                        ErrorMessage.Append("Standard Waktu Sistem Harus diisi Decimal." & Chr(13) & Chr(10))
                    End If
                Catch ex As Exception
                    ErrorMessage.Append("Standard Waktu Sistem Harus diisi Decimal." & Chr(13) & Chr(10))
                End Try
            Else
                ErrorMessage.Append("Standard Waktu Sistem Harus diisi." & Chr(13) & Chr(10))
            End If
            'If Not IsDBNull(row(7)) Then
            '    Try
            '        _ccContact.OfficePhoneAreaCode = row(7)
            '    Catch ex As Exception
            '        ErrorMessage.Append("" & Chr(13) & Chr(10))
            '    End Try
            'End If
            'If Not IsDBNull(row(8)) Then
            '    Try
            '        _ccContact.OfficePhoneNo = row(8)
            '        Dim strMessage As String = IsPhoneValid(row(8))
            '        If strMessage <> String.Empty Then
            '            ErrorMessage.Append(strMessage & Chr(13) & Chr(10))
            '        End If
            '    Catch ex As Exception
            '        'ErrorMessage.Append("" & Chr(13) & Chr(10))
            '    End Try
            'Else
            '    'ErrorMessage.Append("" & Chr(13) & Chr(10)
            'End If
            'If Not IsDBNull(row(9)) Then
            '    Try
            '        _ccContact.OfficePhoneNoExt = row(9)
            '    Catch ex As Exception
            '        'ErrorMessage.Append("" & Chr(13) & Chr(10))
            '    End Try
            'Else
            '    'ErrorMessage.Append("" & Chr(13) & Chr(10)
            'End If

            'If _ccContact.HandphoneNo = String.Empty And _ccContact.HomePhoneNo = String.Empty And _ccContact.OfficePhoneNo = String.Empty Then
            '    ErrorMessage.Append("Tidak ada No Telp yang bisa dihubungi. Salah satu nomor telp harus diisi.")
            'End If

            'If CheckContactByPhoneNoOnExcel(_ccContact.HandphoneNo, _ccContact.HomePhoneNo, _ccContact.OfficePhoneNo) Then
            '    ErrorMessage.Append("Nomor Telepon sudah terdefinisi di File Excel yang sama" & Chr(13) & Chr(10))
            'End If

            'If Not IsDBNull(row(10)) Then
            '    Try
            '        Dim iVhcl As Short
            '        Dim sVhcl As String = IIf(DBNull.Value.Equals(row(10)), String.Empty, row(10))
            '        'added by anh 20130923
            '        Select Case sVhcl
            '            Case "PC"
            '                iVhcl = 1
            '            Case "LCV"
            '                iVhcl = 2
            '            Case "CV"
            '                iVhcl = 3
            '            Case Else
            '                iVhcl = 4
            '        End Select
            '        'end added
            '        'If sVhcl = "PC" Then iVhcl = 1 Else iVhcl = 2 'remarks by anh 20130923
            '        _ccContact.CcVehicleCategoryID = iVhcl
            '        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
            '        If companyCode.ToUpper.Trim().Equals("MMC") Then
            '            If iVhcl = 3 OrElse iVhcl = 4 Then
            '                ErrorMessage.Append("Hanya Tipe Kendaraan hanya PC/LCV yang dapat diinput" & Chr(13) & Chr(10))
            '            End If
            '        ElseIf companyCode.ToUpper.Trim().Equals("MFTBC") Then
            '            If iVhcl <> 3 Then
            '                ErrorMessage.Append("Hanya Tipe Kendaraan hanya CV yang dapat diinput" & Chr(13) & Chr(10))
            '            End If
            '        End If
            '    Catch ex As Exception
            '        ErrorMessage.Append("" & Chr(13) & Chr(10))
            '    End Try
            'Else
            '    ErrorMessage.Append("" & Chr(13) & Chr(10))
            'End If
            'If Not IsDBNull(row(11)) Then
            '    Try
            '        _ccContact.VehicleType = row(11)
            '    Catch ex As Exception
            '        'ErrorMessage.Append("Tipe Kendaraan harus diisi." & Chr(13) & Chr(10))
            '    End Try
            'Else
            '    ErrorMessage.Append("Tipe Kendaraan harus diisi." & Chr(13) & Chr(10))
            'End If
            'If Not IsDBNull(row(12)) Then
            '    Try
            '        _ccContact.NameSTNK = row(12)
            '    Catch ex As Exception
            '        'ErrorMessage.Append("Nama di STNK harus diisi." & Chr(13) & Chr(10))
            '    End Try
            'Else
            '    ErrorMessage.Append("Nama di STNK harus diisi." & Chr(13) & Chr(10))
            'End If
            'If Not IsDBNull(row(13)) Then
            '    Try
            '        _ccContact.AddressSTNK = row(13)
            '    Catch ex As Exception
            '        'ErrorMessage.Append("Alamat di STNK harus diisi." & Chr(13) & Chr(10))
            '    End Try
            'Else
            '    ErrorMessage.Append("Alamat di STNK harus diisi." & Chr(13) & Chr(10))
            'End If
            'If Not IsDBNull(row(14)) Then
            '    Try
            '        _ccContact.City = row(14)
            '    Catch ex As Exception
            '        'ErrorMessage.Append("Kota harus diisi." & Chr(13) & Chr(10))
            '    End Try
            'Else
            '    ErrorMessage.Append("Kota harus diisi." & Chr(13) & Chr(10))
            'End If
            'If Not IsDBNull(row(15)) Then
            '    Try
            '        Dim funcASSI As New AssistServiceIncomingFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            '        Dim _transactionDate As Date = DateTime.ParseExact(row(16), "dd.MM.yyyy", New CultureInfo("ID-id"), DateTimeStyles.None)

            '        Dim enddate As Integer = DateTime.DaysInMonth(_transactionDate.Year, _transactionDate.Month)
            '        Dim strStart As String = String.Format("{0}-{1}-1 00:00", _transactionDate.Year.ToString, _transactionDate.Month)
            '        Dim strEnd As String = String.Format("{0}-{1}-{2} 23:59", _transactionDate.Year.ToString, _transactionDate.Month, enddate.ToString())

            '        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncoming), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncoming), "KodeChassis", MatchType.Exact, row(15)))
            '        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncoming), "WorkOrderCategoryCode", MatchType.No, "PDI"))
            '        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncoming), "TglBukaTransaksi", MatchType.GreaterOrEqual, strStart))
            '        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncoming), "TglBukaTransaksi", MatchType.Lesser, strEnd))
            '        'criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.AssistServiceIncoming), "AssistUploadLog.Year", MatchType.Exact, _transactionDate.Year))
            '        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AssistServiceIncoming), "AssistUploadLog.Month", MatchType.Exact, _transactionDate.Month))

            '        Dim arrSIU As ArrayList = funcASSI.Retrieve(criterias)
            '        If arrSIU.Count > 0 Then
            '            _ccContact.ChassisNo = row(15)
            '        Else
            '            _ccContact.ChassisNo = row(15)
            '            ErrorMessage.Append("Data konsumen belum diupload ke SIU. ")
            '        End If


            '    Catch ex As Exception
            '        'ErrorMessage.Append("No Rangka Kendaraan harus diisi." & Chr(13) & Chr(10))
            '    End Try
            'Else
            '    ErrorMessage.Append("No Rangka Kendaraan harus diisi. " & Chr(13) & Chr(10))
            'End If

            'If Not row.IsNull(16) Then 'If Not IsDBNull(row(16)) Then
            '    Try
            '        Dim strServiceDate As String = row(16)
            '        Dim arrStr() As String = strServiceDate.Split(".")
            '        If arrStr.Length = 3 Then
            '            Dim _periodDate As Date = New Date(CInt(Left(Me.StrPeriod, 4)), CInt(Right(Me.StrPeriod, 2)), 1)
            '            Dim _transactionDate As Date = DateTime.ParseExact(row(16), "dd.MM.yyyy", New CultureInfo("ID-id"), DateTimeStyles.None)
            '            _ccContact.TransactionDate = _transactionDate

            '            If _transactionDate.Year = _periodDate.Year Then
            '                If _transactionDate.Month <> _periodDate.AddMonths(-1).Month Then
            '                    ErrorMessage.Append("Tgl. Servis tidak sesuai periode survei." & Chr(13) & Chr(10))
            '                ElseIf _transactionDate > New Date(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day) Then
            '                    ErrorMessage.Append("Tgl. Servis lebih besar dari pada hari ini." & Chr(13) & Chr(10))
            '                End If
            '            ElseIf _transactionDate.Year < _periodDate.Year Then
            '                If _periodDate.Month <> _transactionDate.Month - 11 Then
            '                    ErrorMessage.Append("Tgl. Servis tidak sesuai periode survei." & Chr(13) & Chr(10))
            '                End If

            '            Else
            '                ErrorMessage.Append("Tgl. Servis salah." & Chr(13) & Chr(10))
            '            End If

            '        Else
            '            ErrorMessage.Append("Format Tgl. Servis harus DD.MM.YYYY." & Chr(13) & Chr(10))
            '        End If
            '    Catch ex As Exception
            '        ErrorMessage.Append("Tgl. Servis salah." & Chr(13) & Chr(10))
            '    End Try
            'Else
            '    ErrorMessage.Append("Tgl. Servis harus diisi." & Chr(13) & Chr(10))
            'End If
            'If Not IsDBNull(row(17)) Then
            '    Try
            '        _ccContact.Odometer = row(17)
            '        If _ccContact.VehicleType <> "" Then

            '            'Dim objOdometer = (From ob As CCVehicleOdometer In Me._OCCVOdometer
            '            '                  Where SqlMethods.Like(_ccContact.VehicleType, ob.VehicleModel)
            '            '                  Select ob.Odometer).SingleOrDefault()

            '            Dim objOdometer = (From ob As CCVehicleOdometer In Me._OCCVOdometer
            '                             Where _ccContact.VehicleType.ToUpper().Trim().Contains(ob.VehicleModel.Trim().ToUpper())
            '                             Select ob.Odometer).SingleOrDefault()
            '            Dim oDefaultOdometer = (From ob As CCVehicleOdometer In Me._OCCVOdometer
            '                             Where ob.VehicleModel.ToUpper().Trim() = "OTHERSMODEL"
            '                             Select ob.Odometer).SingleOrDefault()

            '            Dim ODOdometer As Integer = IIf(IsNothing(oDefaultOdometer) OrElse CInt(oDefaultOdometer) <= 0, 100000, CInt(oDefaultOdometer))

            '            If Not IsNothing(objOdometer) AndAlso CInt(objOdometer) > 0 Then
            '                If CInt(_ccContact.Odometer) > CInt(objOdometer) Then
            '                    ErrorMessage.Append("Odometer lebih dari  " & objOdometer.ToString() & " km." & Chr(13) & Chr(10))
            '                End If
            '            Else
            '                If CInt(row(17)) > ODOdometer Then
            '                    ErrorMessage.Append("Odometer lebih dari " & ODOdometer.ToString() & "  km." & Chr(13) & Chr(10))
            '                End If

            '            End If


            '        End If


            '    Catch ex As Exception
            '        'ErrorMessage.Append("Odometer harus diisi." & Chr(13) & Chr(10))
            '    End Try
            'Else
            '    ErrorMessage.Append("Odometer harus diisi." & Chr(13) & Chr(10))
            'End If
            'If Not IsDBNull(row(18)) Then
            '    Try
            '        _ccContact.ServiceType = row(18)
            '    Catch ex As Exception
            '        ErrorMessage.Append("Kesalahan data service" & Chr(13) & Chr(10))
            '    End Try
            'Else
            '    ErrorMessage.Append("Data service harus diisi." & Chr(13) & Chr(10))
            'End If

            'Dim msgContact As String = CheckContactByPhoneNo(_ccContact.CcVehicleCategoryID, 2, _ccContact.HandphoneNo, _ccContact.HomePhoneNo, _ccContact.OfficePhoneNo, periodID)
            'If msgContact <> String.Empty Then
            '    If msgContact = objDealer.DealerCode Then
            '        ErrorMessage.Append("Data sudah ada. No telp sama." & Chr(13) & Chr(10))
            '    Else
            '        ErrorMessage.Append("Data sudah ada,  no telp sama dengan dealer lain." & Chr(13) & Chr(10))
            '    End If
            'Else
            '    If CheckContactByChassisNo(periodID, _ccContact.ChassisNo, _ccContact.CcVehicleCategoryID, 2) Then
            '        ErrorMessage.Append("Data sudah ada. Nomor rangkanya sama." & Chr(13) & Chr(10))
            '    Else
            '        Dim IsCalled As Boolean = New CcContactFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).RetrieveCountByCriteria(_ccContact.CcVehicleCategoryID, 2, _ccContact.HandphoneNo, _ccContact.HomePhoneNo, _ccContact.OfficePhoneNo, periodID)
            '        If IsCalled Then
            '            ErrorMessage.Append("Data ini sudah ditelepon." & Chr(13) & Chr(10))
            '        End If
            '    End If
            'End If

            'If Not (ErrorMessage.Length > 0) Then
            '    Dim isExist As Boolean = False
            '    Dim oFacade As New CcContactFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            '    If _ccContact.HandphoneNo <> "" Then
            '        Try
            '            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcContact), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcContact), "CcPeriodID", MatchType.Exact, _ccContact.CcPeriodID))
            '            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcContact), "CcCustomerCategoryID", MatchType.Exact, _ccContact.CcCustomerCategoryID))
            '            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcContact), "HandphoneNo", MatchType.Exact, _ccContact.HandphoneNo))
            '            Dim arlContacts As ArrayList = oFacade.RetrieveByCriteria(criterias)
            '            If arlContacts.Count > 0 Then
            '                isExist = True
            '                ErrorMessage.Append("no. handphone : " & _ccContact.HandphoneNo & ", sudah diupload.")
            '            End If
            '        Catch ex As Exception

            '        End Try

            '    End If
            '    If _ccContact.HomePhoneNo <> "" And isExist = False Then
            '        Try
            '            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcContact), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcContact), "CcPeriodID", MatchType.Exact, _ccContact.CcPeriodID))
            '            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcContact), "CcCustomerCategoryID", MatchType.Exact, _ccContact.CcCustomerCategoryID))
            '            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcContact), "HomePhoneNo", MatchType.Exact, _ccContact.HomePhoneNo))
            '            Dim arlContacts As ArrayList = oFacade.RetrieveByCriteria(criterias)
            '            If arlContacts.Count > 0 Then
            '                isExist = True
            '                ErrorMessage.Append("no. rumah : " & _ccContact.HomePhoneAreaCode & _ccContact.HomePhoneNo & ", sudah diupload.")
            '            End If
            '        Catch ex As Exception

            '        End Try
            '    End If
            '    If _ccContact.OfficePhoneNo <> "" And isExist = False Then
            '        Try
            '            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CcContact), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcContact), "CcPeriodID", MatchType.Exact, _ccContact.CcPeriodID))
            '            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcContact), "CcCustomerCategoryID", MatchType.Exact, _ccContact.CcCustomerCategoryID))
            '            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CcContact), "OfficePhoneNo", MatchType.Exact, _ccContact.OfficePhoneNo))
            '            Dim arlContacts As ArrayList = oFacade.RetrieveByCriteria(criterias)
            '            If arlContacts.Count > 0 Then
            '                isExist = True
            '                ErrorMessage.Append("no. kantor : " & _ccContact.OfficePhoneAreaCode & _ccContact.OfficePhoneNo & ", sudah diupload.")
            '            End If
            '        Catch ex As Exception

            '        End Try
            '    End If
            'End If


            If ErrorMessage.Length > 0 Then
                _sst.ErrorMessage = ErrorMessage.ToString
                If IsDataValid = True Then
                    IsDataValid = False
                End If
            End If

            Return _sst
        End Function

        Private Function IsPhoneValid(ByVal phoneNo As String, Optional ByVal phoneType As String = "") As String
            Dim strMessage As String = String.Empty
            'If Left(phoneNo, 2) = 62 Then
            '    If phoneType <> "" Then 'Handphone
            '        strMessage += "Kode negara tambahkan '+' pada " & phoneType & "."
            '    Else
            '        strMessage += "Kode negara tambahkan '+'."
            '    End If
            'End If

            If phoneType = "Handphone" Then
                If Len(phoneNo) > 5 AndAlso Left(phoneNo, 1) <> "0" Then
                    strMessage += "No. Handphone harus diawali dengan '0' (nol)"
                End If
            End If
            If phoneNo.Length > 20 Then
                If phoneType <> "" Then
                    strMessage += "No " & phoneType & " cukup satu nomor."
                Else
                    strMessage += "Cukup masukkan satu nomor."
                End If
            End If
            Return strMessage
        End Function

        'Private Function CheckContactByChassisNo(ByVal periodID As Integer, ByVal strChasisNo As String, ByVal strVehicleCategoryID As String, ByVal strCustomerCategoryID As String) As Boolean

        '    Dim criterias As New CriteriaComposite(New Criteria(GetType(CcContact), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    criterias.opAnd(New Criteria(GetType(CcContact), "CcPeriodID", MatchType.Exact, periodID))
        '    criterias.opAnd(New Criteria(GetType(CcContact), "ChassisNo", MatchType.Exact, strChasisNo))
        '    criterias.opAnd(New Criteria(GetType(CcContact), "CcCustomerCategoryID", MatchType.Exact, strCustomerCategoryID))
        '    criterias.opAnd(New Criteria(GetType(CcContact), "CcVehicleCategoryID", MatchType.Exact, strVehicleCategoryID))

        '    Dim objColl As ArrayList = New CcContactFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)

        '    Return objColl.Count > 0

        'End Function
        'Private Sub InitCCVehicleOdometer()
        '    Me._OCCVOdometer = New List(Of CCVehicleOdometer)
        '    Dim ObjCOF As CCVehicleOdometerFacade
        '    ObjCOF = New CCVehicleOdometerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))

        '    Dim criterias As New CriteriaComposite(New Criteria(GetType(CCVehicleOdometer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    Dim objColl As ArrayList = ObjCOF.Retrieve(criterias)

        '    Me._OCCVOdometer = objColl.Cast(Of CCVehicleOdometer)().ToList()
        'End Sub

        'Private Function CheckContactByPhoneNo(ByVal strVehicleCategoryID As String, ByVal strCustomerCategoryID As String, _
        '                                       ByVal handphoneNo As String, ByVal homePhoneNo As String, ByVal officePhoneNo As String, ByVal strPeriodId As String) As String

        '    Dim criterias As New CriteriaComposite(New Criteria(GetType(CcContact), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    criterias.opAnd(New Criteria(GetType(CcContact), "CcCustomerCategoryID", MatchType.Exact, strCustomerCategoryID))
        '    criterias.opAnd(New Criteria(GetType(CcContact), "CcVehicleCategoryID", MatchType.Exact, strVehicleCategoryID))
        '    criterias.opAnd(New Criteria(GetType(CcContact), "CcPeriodID", MatchType.Exact, strPeriodId))

        '    If handphoneNo <> String.Empty Then
        '        criterias.opAnd(New Criteria(GetType(CcContact), "HandphoneNo", MatchType.Exact, handphoneNo))
        '    ElseIf handphoneNo = String.Empty AndAlso homePhoneNo <> String.Empty Then
        '        criterias.opAnd(New Criteria(GetType(CcContact), "HomePhoneNo", MatchType.Exact, homePhoneNo))
        '    ElseIf handphoneNo = String.Empty AndAlso homePhoneNo = String.Empty And officePhoneNo <> String.Empty Then
        '        criterias.opAnd(New Criteria(GetType(CcContact), "OfficePhoneNo", MatchType.Exact, officePhoneNo))
        '    End If

        '    Dim objColl As ArrayList = New CcContactFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)

        '    If objColl.Count = 0 Then
        '        Return String.Empty
        '    Else
        '        Dim objCcContact As CcContact = CType(objColl(0), CcContact)
        '        Return objCcContact.Dealer.DealerCode
        '    End If

        'End Function

        'Private Function CheckContactByPhoneNoOnExcel(ByVal handphoneNo As String, ByVal homePhoneNo As String, ByVal officePhoneNo As String) As Boolean
        '    Dim IsExist As Boolean = False

        '    If CcContactList.Count > 0 Then
        '        For Each obj As CcContact In CcContactList
        '            If obj.ErrorMessage = String.Empty Then
        '                If handphoneNo <> String.Empty Then
        '                    If obj.HandphoneNo = handphoneNo Then
        '                        IsExist = True
        '                        Exit For
        '                    End If
        '                ElseIf handphoneNo = String.Empty AndAlso homePhoneNo <> String.Empty Then
        '                    If obj.HomePhoneNo = homePhoneNo Then
        '                        IsExist = True
        '                        Exit For
        '                    End If
        '                ElseIf handphoneNo = String.Empty AndAlso homePhoneNo = String.Empty And officePhoneNo <> String.Empty Then
        '                    If obj.OfficePhoneNo = officePhoneNo Then
        '                        IsExist = True
        '                        Exit For
        '                    End If
        '                End If
        '            End If
        '        Next
        '    End If

        '    Return IsExist

        'End Function


#End Region

    End Class

End Namespace