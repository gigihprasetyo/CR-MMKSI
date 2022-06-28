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
Imports System.Reflection

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
    Public Class UploadAllocationRealtimeService
        Inherits AbstractExcelParser
#Region "Private Variables"
        Private ARSList As ArrayList
        Private _ars As AllocationRealTimeService
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
            ARSList = New ArrayList  '-- List of Allocation 
            IsDataValid = True
            Try
                AbstractExcelParser.ContentTypeExcel = Me.ContentFileType
                Dim Ds As DataSet = ParseExcelDataSet(fileName, sheetName, "")
                If IsNothing(Ds) Then
                    Me.ErrorMessage.Append("Pastikan nama worksheetnya adalah Sheet1.")
                Else

                    'Validasi Column
                    Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AllocationRealtimeServiceTemplate), "ARSTemplateCategoryID", MatchType.Exact, 1)) 'ASS
                    Dim sortCol As SortCollection = New SortCollection
                    sortCol.Add(New Sort(GetType(AllocationRealtimeServiceTemplate), "ColNumber", Sort.SortDirection.ASC))

                    Dim objTemplateList As ArrayList = New KTB.DNet.BusinessFacade.Service.AllocationRealtimeServiceTemplateFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(critCol, sortCol)

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
                            Return ARSList
                            Exit Function
                        End If

                        Dim row As DataRow
                        Dim i As Integer = 0
                        For i = 0 To Ds.Tables(0).Rows.Count - 1
                            row = Ds.Tables(0).Rows(i)
                            Try

                                If Not IsDBNull(row(0)) And Not IsDBNull(row(1)) Then
                                    _ars = New AllocationRealTimeService

                                    _ars = ParseCcContact(row)
                                    ARSList.Add(_ars)
                                ElseIf IsDBNull(row(0)) And Not IsDBNull(row(1)) Then
                                    'Me.ErrorMessage.Append("Invalid No Urut. Masukkan No Urut yang valid." & Chr(13) & Chr(10))
                                    _ars = New AllocationRealTimeService

                                    _ars = ParseCcContact(row)
                                    ARSList.Add(_ars)
                                ElseIf IsDBNull(row(0)) And IsDBNull(row(1)) Then
                                    Me.ErrorMessage.Append("Tidak Ada Data Yang Di Upload.")
                                Else
                                    _ars = New AllocationRealTimeService

                                    _ars = ParseCcContact(row)
                                    ARSList.Add(_ars)
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
            Return ARSList
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

        Private Function ParseCcContact(ByVal row As DataRow) As AllocationRealTimeService

            Dim objDealer As Dealer
            Dim objARS As VWI_AllocationRealTimeService

            Me.ErrorMessage = New StringBuilder

            If Not IsDBNull(row(1)) Then
                Try
                    Dim strKodeDealer As String = CType(row(1), String)

                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "Status", MatchType.Exact, 1))
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.Exact, strKodeDealer))
                    'Dim results As ArrayList = vechileTypeFacade.Retrieve(crit)
                    Dim arrVM As ArrayList = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                    If arrVM.Count > 0 Then
                        If Not IsNothing(arrVM) AndAlso arrVM.Count > 0 Then
                            objDealer = CType(arrVM(0), Dealer)
                            _ars.Dealer = objDealer
                        End If
                    Else
                        ErrorMessage.Append("Kode Dealer tidak Terdefinisi." & Chr(13) & Chr(10))
                    End If
                Catch ex As Exception
                    'ErrorMessage.Append("Standard Waktu Dealer Harus diisi Decimal." & Chr(13) & Chr(10))
                End Try
            Else
                ErrorMessage.Append("Kode Dealer Harus diisi." & Chr(13) & Chr(10))
            End If


            If Not IsDBNull(row(2)) Then
                Try
                    'Dim decimalSeparator As String = Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator
                    Dim STD As Integer
                    Dim strStd As String = CType(row(2), String) 'row(6).Replace(".", decimalSeparator).Replace(",", decimalSeparator) 'row(6) 'CType(row(6), String)
                    If Integer.TryParse(strStd, STD) Then

                        If Not IsNothing(_ars.Dealer) Then
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VWI_AllocationRealTimeService), "DealerCode", MatchType.Exact, _ars.Dealer.DealerCode))
                            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VWI_AllocationRealTimeService), "ID", MatchType.Greater, 0))
                            ''Dim results As ArrayList = vechileTypeFacade.Retrieve(crit)
                            Dim AllocationRealTimeService As List(Of VWI_AllocationRealTimeService) = New KTB.DNet.BusinessFacade.Service.VWI_AllocationRealTimeServiceFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).RetrieveByCriteria(criterias).Cast(Of VWI_AllocationRealTimeService).ToList
                            Dim arrVM = New ArrayList
                            For Each objs As VWI_AllocationRealTimeService In AllocationRealTimeService
                                
                                arrVM.add(objs)
                            Next

                            If arrVM.Count > 0 Then
                                If Not IsNothing(arrVM) AndAlso arrVM.Count > 0 Then
                                    objARS = CType(arrVM(0), VWI_AllocationRealTimeService)
                                    If objARS.ID > 0 Then
                                        If STD <> 0 Then
                                            If STD < objARS.CurrentStall Then
                                                _ars.AlokasiStall = STD
                                                '_ars.
                                                ErrorMessage.Append("Alokasi Stall tidak boleh kurang dari Current Stall.." & Chr(13) & Chr(10))
                                            Else
                                                _ars.AlokasiStall = STD
                                            End If
                                        Else
                                            _ars.AlokasiStall = STD
                                        End If
                                    Else
                                        If STD <> 0 Then
                                            If STD < objARS.CurrentStall Then
                                                _ars.AlokasiStall = STD
                                                '_ars.
                                                ErrorMessage.Append("Alokasi Stall tidak boleh kurang dari Current Stall.." & Chr(13) & Chr(10))
                                            Else
                                                _ars.AlokasiStall = STD
                                            End If
                                        Else
                                            _ars.AlokasiStall = STD
                                        End If
                                    End If
                                End If
                            Else
                                _ars.AlokasiStall = STD
                            End If
                        Else
                            _ars.AlokasiStall = STD
                        End If
                    Else
                        ErrorMessage.Append("Alokasi Stall harus diisi integer.." & Chr(13) & Chr(10))
                    End If
                Catch ex As Exception
                    ErrorMessage.Append("Alokasi Stall Harus diisi integer." & Chr(13) & Chr(10))
                End Try
            Else
                ErrorMessage.Append("Alokasi Stall Harus diisi." & Chr(13) & Chr(10))
            End If

            'If Not IsDBNull(row(3)) Then
            '    Try
            '        'Dim decimalSeparator As String = Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator
            '        Dim STD As Integer
            '        Dim strStd As String = CType(row(3), String) 'row(6).Replace(".", decimalSeparator).Replace(",", decimalSeparator) 'row(6) 'CType(row(6), String)
            '        If Integer.TryParse(strStd, STD) Then
            '            If Not IsNothing(_ars.Dealer) Then
            '                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VWI_AllocationRealTimeService), "DealerCode", MatchType.Exact, _ars.Dealer.DealerCode))
            '                'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VWI_AllocationRealTimeService), "DealerCode", MatchType.Exact, _ars.Dealer.DealerCode))
            '                'Dim results As ArrayList = vechileTypeFacade.Retrieve(crit)
            '                Dim AllocationRealTimeService As List(Of VWI_AllocationRealTimeService) = New KTB.DNet.BusinessFacade.Service.VWI_AllocationRealTimeServiceFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).RetrieveByCriteria(criterias).Cast(Of VWI_AllocationRealTimeService).ToList
            '                Dim arrVM = New ArrayList
            '                For Each objs As VWI_AllocationRealTimeService In AllocationRealTimeService

            '                    arrVM.add(objs)
            '                Next

            '                If arrVM.Count > 0 Then
            '                    If Not IsNothing(arrVM) AndAlso arrVM.Count > 0 Then
            '                        objARS = CType(arrVM(0), VWI_AllocationRealTimeService)
            '                        '_ars.CurrentStall = objARS.CurrentStall
            '                    End If
            '                Else
            '                    If Not IsNothing(_ars.AlokasiStall) Then
            '                        If _ars.AlokasiStall < STD Then
            '                            ErrorMessage.Append("Alokasi Stall tidak boleh kurang dari Current Stall.." & Chr(13) & Chr(10))
            '                        Else
            '                            '_ars.CurrentStall = STD
            '                        End If
            '                    End If
            '                End If
            '            Else
            '                '_ars.CurrentStall = STD
            '            End If
            '        Else
            '            ErrorMessage.Append("Current Stall harus diisi integer.." & Chr(13) & Chr(10))
            '        End If
            '    Catch ex As Exception
            '        'ErrorMessage.Append("Current Stall Harus diisi integer." & Chr(13) & Chr(10))
            '    End Try
            '    'Else
            '    '    ErrorMessage.Append("Current Stall Harus diisi." & Chr(13) & Chr(10))
            'End If

            If ErrorMessage.Length > 0 Then
                _ars.ErrorMessage = ErrorMessage.ToString
                If IsDataValid = True Then
                    IsDataValid = False
                End If
            End If
            'Dim arl As New ArrayList = CType(_ars(0), ArrayList)
            '_ars = SortArraylist(_ars, GetType(AllocationRealTimeService), "ErrorMessage", Sort.SortDirection.ASC)

            Return _ars
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

