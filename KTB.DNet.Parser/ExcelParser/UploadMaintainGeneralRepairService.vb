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
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade


#End Region

Namespace KTB.DNet.Parser
    Public Class UploadMaintainGeneralRepairService
        Inherits AbstractExcelParser
#Region "Private Variables"
        Private ARSList As ArrayList
        Private _ars As ServiceTemplateGRLabor
        Private _arsDetail As ServiceTemplateGRPartDetail
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

        End Function

        Public Function ParsingExcelMultiSheet(ByVal fileName As String, ByVal sheetName() As String, ByVal user As String) As Object
            Me.ErrorMessage = New StringBuilder
            ARSList = New ArrayList  '-- List of Allocation 
            IsDataValid = True
            Try
                AbstractExcelParser.ContentTypeExcel = Me.ContentFileType

                Dim Ds As DataSet = ParseExcelMultiSheetDataSet(fileName, sheetName, "")
                If IsNothing(Ds) Then
                    Me.ErrorMessage.Append("Pastikan nama worksheetnya adalah Sheet1.")
                Else
                    ValidasiKolomTable1(Ds)

                End If

            Catch ex As Exception
                'Me.ErrorMessage.Append(ex.Message)
                Me.ErrorMessage.Append("Tidak Ada Data Yang Di Upload.")
            End Try
            Return ARSList
        End Function

        Private Function ValidasiKolomTable1(Data As DataSet) As ArrayList
            If Not IsNothing(Data.Tables(0)) Then
                If Not Data.Tables(0).Columns.Count >= 6 Then
                    Me.ErrorMessage.Append("Jumlah Kolom tidak sesuai. ")
                End If

                If Me.ErrorMessage.Length > 0 Then
                    IsDataValid = False
                    Return ARSList
                    Exit Function
                End If

                Dim row As DataRow
                Dim i As Integer = 0
                For i = 0 To Data.Tables(0).Rows.Count - 1
                    row = Data.Tables(0).Rows(i)
                    If IsDBNull(row(4)) And IsDBNull(row(5)) And IsDBNull(row(7)) Then
                        Exit For
                    Else
                        If String.IsNullorEmpty(row(4)) And String.IsNullorEmpty(row(5)) And String.IsNullorEmpty(row(7)) Then
                            Continue For
                        End If
                    End If

                    Try
                        _ars = New ServiceTemplateGRLabor

                        _ars.Variants = row(1)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.GRKind), "KindCode", MatchType.Exact, row(3)))
                        Dim arrList As ArrayList = New GRKindFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)

                        If arrList.Count > 0 Then
                            Dim dt As GRKind = arrList.Item(0)
                            _ars.GRKindID = dt.ID

                            _ars = ParseCcContact(row)
                            criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ServiceTemplateGRLabor), "Variants", MatchType.Exact, row(1)))
                            criterias.opAnd(New Criteria(GetType(ServiceTemplateGRLabor), "GRKindID", MatchType.Exact, dt.ID))
                            Dim arrGRLabor As ArrayList = New ServiceTemplateGRLaborFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                            If (arrGRLabor.Count > 0) Then
                                Dim dtGR As ServiceTemplateGRLabor = arrGRLabor.Item(0)
                                If String.IsNullorEmpty(_ars.ErrorMessage) Then
                                    If String.Equals(dtGR.LaborCost, CType(row(4), Decimal)) And String.Equals(dtGR.LaborDuration, CType(row(5), Decimal)) And String.Equals(dtGR.ValidFrom, CType(row(7), DateTime)) Then
                                        Continue For
                                    End If
                                End If
                            End If
                        End If
                        If String.IsNullorEmpty(_ars.ErrorMessage) Then
                            '_ars.GRKindID = IIf(arrList.Count > 0, arrList.Item(0)("ID"), 0)
                            _ars.LaborCost = CType(IIf(String.IsNullorEmpty(row(4)), 0, row(4)), Decimal)
                            _ars.LaborDuration = CType(IIf(String.IsNullorEmpty(row(5)), 0, row(5)), Decimal)
                            _ars.ValidFrom = CType(IIf(String.IsNullorEmpty(row(7)), System.Data.SqlTypes.SqlDateTime.MinValue.Value, row(7)), Date)
                        End If
                        
                        ARSList.Add(_ars)

                    Catch ex As Exception

                    End Try
                Next

                'Detail
                Dim rowDetail As DataRow
                'Dim i As Integer = 0
                For i = 0 To Data.Tables(1).Rows.Count - 1
                    rowDetail = Data.Tables(1).Rows(i)
                    If IsDBNull(rowDetail(4)) And IsDBNull(rowDetail(5)) And IsDBNull(rowDetail(6)) And IsDBNull(rowDetail(7)) Then
                        'Exit For
                        Continue For
                    Else
                        If String.IsNullorEmpty(rowDetail(4)) And String.IsNullorEmpty(rowDetail(5)) And String.IsNullorEmpty(rowDetail(6)) And String.IsNullorEmpty(rowDetail(7)) Then
                            Continue For
                        End If
                    End If

                    Try
                        _arsDetail = New ServiceTemplateGRPartDetail
                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.GRKind), "KindCode", MatchType.Exact, rowDetail(3)))
                        Dim arrList As ArrayList = New GRKindFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                        If arrList.Count > 0 Then
                            Dim dt As GRKind = arrList.Item(0)

                            Dim criteriasDetail As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ServiceTemplateGRLabor), "Variants", MatchType.Exact, rowDetail(1).ToString()))
                            criteriasDetail.opAnd(New Criteria(GetType(KTB.DNet.Domain.ServiceTemplateGRLabor), "GRKindID", MatchType.Exact, dt.ID))
                            Dim arrListDetail As ArrayList = New ServiceTemplateGRLaborFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criteriasDetail)

                            If arrListDetail.Count > 0 Then
                                Dim dtDetail As ServiceTemplateGRLabor = arrListDetail.Item(0)
                                _arsDetail.ServiceTemplateGRLaborID = dtDetail.ID
                            End If
                            _arsDetail = ParseCcContactDetail(rowDetail)

                            Dim criteriasSparePart As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartMaster), "PartNumber", MatchType.Exact, rowDetail(5)))
                            Dim arrSparepart As ArrayList = New SparePartMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criteriasSparePart)
                            If arrSparepart.Count > 0 Then
                                Dim sparePartItem As SparePartMaster = arrSparepart.Item(0)
                                _arsDetail.SparePartMasterID = sparePartItem.ID
                                criteriasDetail = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ServiceTemplateGRPartDetail), "ServiceTemplateGRLaborID", MatchType.Exact, _arsDetail.ServiceTemplateGRLaborID))
                                criteriasDetail.opAnd(New Criteria(GetType(KTB.DNet.Domain.ServiceTemplateGRPartDetail), "SparePartMasterID", MatchType.Exact, sparePartItem.ID))
                                Dim arrGRPartDetail As ArrayList = New ServiceTemplateGRPartDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criteriasDetail)
                                If Not String.IsNullorEmpty(rowDetail(7)) Then
                                    If CType(rowDetail(7), Decimal) <= 0 Then
                                        _arsDetail.ErrorMessage = "Jumlah Unit Harus Lebih dari 0 " & Chr(13) & Chr(10)
                                        _arsDetail.PartQuantity = CType(rowDetail(7), Decimal)
                                        IsDataValid = False
                                    End If
                                Else
                                    _arsDetail.ErrorMessage = "Jumlah Unit Tidak Boleh Kosong " & Chr(13) & Chr(10)
                                    IsDataValid = False
                                End If

                                If arrGRPartDetail.Count > 0 Then
                                    Dim dtGRPartDetail As ServiceTemplateGRPartDetail = arrGRPartDetail.Item(0)
                                    If String.IsNullorEmpty(_arsDetail.ErrorMessage) Then
                                        If sparePartItem.RetalPrice = CType(rowDetail(6), Decimal) And dtGRPartDetail.PartQuantity = CType(rowDetail(7), Decimal) Then
                                            Continue For
                                        End If
                                        If Not String.IsNullorEmpty(rowDetail(7)) Then
                                            If CType(rowDetail(7), Decimal) <= 0 Then
                                                _arsDetail.ErrorMessage = "Jumlah Unit Harus Lebih dari 0 " & Chr(13) & Chr(10)
                                                IsDataValid = False
                                            End If
                                        Else
                                            _arsDetail.ErrorMessage = "Jumlah Unit Tidak Boleh Kosong " & Chr(13) & Chr(10)
                                            IsDataValid = False
                                        End If

                                        'If Not String.Equals(sparePartItem.PartName, rowDetail(4)) Then
                                        '    _arsDetail.ErrorMessage = "Nama Sparepart Tidak bisa di Edit " & Chr(13) & Chr(10)
                                        'End If
                                    End If
                                Else
                                    _arsDetail.PartQuantity = CType(IIf(String.IsNullorEmpty(rowDetail(7)), 0, rowDetail(7)), Decimal)
                                    If arrGRPartDetail.Count = 0 And arrListDetail.Count = 0 Then
                                        _arsDetail.ErrorMessage = "Variant, Jenis Service, dan Kind Code atas Sparepart tersebut belum terdaftar pada GR Labor"
                                        IsDataValid = False
                                    End If
                                End If

                            Else
                                Dim tempErrMessage = _arsDetail.ErrorMessage
                                _arsDetail.ErrorMessage = tempErrMessage & " Sparepart Tidak Ditemukan" & Chr(13) & Chr(10)
                                IsDataValid = False
                            End If
                            If String.IsNullorEmpty(_arsDetail.ErrorMessage) Then
                                _arsDetail.PartQuantity = CType(IIf(String.IsNullorEmpty(rowDetail(7)), 0, rowDetail(7)), Decimal)
                            End If

                            ARSList.Add(_arsDetail)
                        End If

                    Catch ex As Exception

                    End Try
                Next

            Else
                Me.ErrorMessage.Append("Tidak Ada Data Yang Di Upload.")
            End If


        End Function

#Region "Old ParsingExcelNoTransaction"

#End Region


        Private Function IsValidColumn(ByVal colName As String, ByVal mapCols As ArrayList) As Boolean
            Dim isValid As Boolean = False

            Return isValid

        End Function

        Private Function ParseCcContact(ByVal row As DataRow) As ServiceTemplateGRLabor

            Me.ErrorMessage = New StringBuilder
            If Not Regex.IsMatch(row(1), "^[A-Za-z]") Then
                ErrorMessage.Append("Kolom Variant harus huruf" & Chr(13) & Chr(10))
            End If

            If Not Regex.IsMatch(row(2), "^[A-Za-z]") Then
                ErrorMessage.Append("Kolom Jenis Service harus huruf" & Chr(13) & Chr(10))
            End If
            If Not Regex.IsMatch(row(3), "^[0-9 ]") Then
                ErrorMessage.Append("Kolom Kind Code harus number" & Chr(13) & Chr(10))
            End If
            If Not Regex.IsMatch(row(4), "^[0-9 ]") Then
                ErrorMessage.Append("Kolom Biaya Jasa harus number" & Chr(13) & Chr(10))
            End If
            If Not Regex.IsMatch(row(5), "^[0-9 ]") Then
                ErrorMessage.Append("Kolom Durasi Service harus number" & Chr(13) & Chr(10))
            End If
            If Not IsDate(row(7)) Then
                ErrorMessage.Append("Kolom Mulai Berlaku harus Date" & Chr(13) & Chr(10))
            End If


            If ErrorMessage.Length > 0 Then
                _ars.ErrorMessage = ErrorMessage.ToString
                If IsDataValid = True Then
                    IsDataValid = False
                End If
            End If

            Return _ars
        End Function

        Private Function ParseCcContactDetail(ByVal row As DataRow) As ServiceTemplateGRPartDetail

            'Dim objDealer As Dealer
            'Dim objARS As VWI_AllocationRealTimeService

            Me.ErrorMessage = New StringBuilder
            If Not Regex.IsMatch(row(1), "^[A-Za-z]") Then
                ErrorMessage.Append("Kolom Variant harus huruf" & Chr(13) & Chr(10))
            End If

            If Not Regex.IsMatch(row(2), "^[A-Za-z]") Then
                ErrorMessage.Append("Kolom Jenis Service harus huruf" & Chr(13) & Chr(10))
            End If
            If Not Regex.IsMatch(row(3), "^[0-9 ]") Then
                ErrorMessage.Append("Kolom Kind Code harus number" & Chr(13) & Chr(10))
            End If
            If Not Regex.IsMatch(row(4), "^[A-Za-z]") Then
                ErrorMessage.Append("Kolom Nama Sparepart harus huruf" & Chr(13) & Chr(10))
            End If
            If Not Regex.IsMatch(row(5), "^[a-zA-Z0-9]") Then
                ErrorMessage.Append("Kolom Kode Sparepart harus kombinasi number dan huruf" & Chr(13) & Chr(10))
            End If
            If Not Regex.IsMatch(row(6), "^[0-9 ]") Then
                ErrorMessage.Append("Kolom Harga Satuan harus number" & Chr(13) & Chr(10))
            End If
            If Not Regex.IsMatch(row(7), "^[0-9 ]") Then
                ErrorMessage.Append("Kolom Jumlah Unit harus number dan harus lebih dari 0" & Chr(13) & Chr(10))
            End If


            If ErrorMessage.Length > 0 Then
                _arsDetail.ErrorMessage = ErrorMessage.ToString
                If IsDataValid = True Then
                    IsDataValid = False
                End If
            End If

            Return _arsDetail
        End Function

        Private Function IsPhoneValid(ByVal phoneNo As String, Optional ByVal phoneType As String = "") As String
            Dim strMessage As String = String.Empty

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

#End Region
    End Class

End Namespace

