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

    Public Class UploadVechileColorIsActiveOnPK
        Inherits AbstractExcelParser

#Region "Private Variables"
        Private VechileColorIsActiveOnPKList As ArrayList
        Private _VechileColorIsActiveOnPK As VechileColorIsActiveOnPK
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
            VechileColorIsActiveOnPKList = New ArrayList  '-- List of Customer 
            IsDataValid = True
            Try
                AbstractExcelParser.ContentTypeExcel = Me.ContentFileType
                Dim Ds As DataSet = ParseExcelDataSet(fileName, sheetName, "")

                If IsNothing(Ds) Then
                    Me.ErrorMessage.Append("Pastikan nama worksheetnya adalah Sheet1.")
                Else

                    Dim rowAwal As Integer                  
                    rowAwal = 0

                    Dim row As DataRow

                    Dim i As Integer = 0
                    For i = rowAwal To Ds.Tables(0).Rows.Count - 1
                        row = Ds.Tables(0).Rows(i)
                        Try
                            If Not IsDBNull(row(0)) Then
                                _VechileColorIsActiveOnPK = New VechileColorIsActiveOnPK
                                _VechileColorIsActiveOnPK = ParseCustomer(row)
                                VechileColorIsActiveOnPKList.Add(_VechileColorIsActiveOnPK)
                            End If
                        Catch ex As Exception
                            Me.ErrorMessage.Append(ex.Message)
                        End Try
                    Next

                    If VechileColorIsActiveOnPKList.Count < 1 Then
                        Me.ErrorMessage.Append("Tidak ada data yang diinput.")
                        VechileColorIsActiveOnPKList.Clear()
                    End If

                    'If VechileColorIsActiveOnPKList.Count > 100 Then
                    '    Me.ErrorMessage.Append("Jumlah baris tidak boleh lebih dari 100")
                    '    VechileColorIsActiveOnPKList.Clear()
                    'End If

                    If Me.ErrorMessage.Length > 0 Then
                        IsDataValid = False
                        Return VechileColorIsActiveOnPKList
                        Exit Function
                    End If
                End If

            Catch ex As Exception
                Me.ErrorMessage.Append(ex.Message)
            End Try
            Return VechileColorIsActiveOnPKList
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

        Private Function ParseCustomer(ByVal row As DataRow) As VechileColorIsActiveOnPK
            Me.ErrorMessage = New StringBuilder

            Dim col As Integer = -1
            Dim objVechileColorIsActiveOnPK As VechileColorIsActiveOnPK

            If Not IsDBNull(row(col + 2)) Then 'VechileColorIsActiveOnPKID
                Try
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColorIsActiveOnPK), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(VechileColorIsActiveOnPK), "ID", MatchType.Exact, CInt(row(col + 2).ToString())))
                    Dim arrVechileColorIsActiveOnPK As ArrayList = New VechileColorIsActiveOnPKFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                    'objVehicleType = New VechileTypeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(row(col + 13).ToString())
                    If Not IsNothing(arrVechileColorIsActiveOnPK) AndAlso arrVechileColorIsActiveOnPK.Count > 0 Then
                        objVechileColorIsActiveOnPK = CType(arrVechileColorIsActiveOnPK(0), VechileColorIsActiveOnPK)
                        _VechileColorIsActiveOnPK = objVechileColorIsActiveOnPK
                    Else
                        ErrorMessage.Append("ID tidak ditemukan." & Chr(13) & Chr(10))
                    End If
                Catch ex As Exception
                    ErrorMessage.Append("ID tidak valid." & Chr(13) & Chr(10))
                End Try
            Else
                ErrorMessage.Append("ID harus diisi." & Chr(13) & Chr(10))
            End If

            If Not IsDBNull(row(col + 6)) Then
                Try
                    _VechileColorIsActiveOnPK.DescriptionDealer = row(col + 6).ToString
                Catch ex As Exception
                    ErrorMessage.Append("Deskripsi Kendaraan Dealer tidak valid." & Chr(13) & Chr(10))
                End Try
            Else
                ErrorMessage.Append("Deskripsi Kendaraan Dealer harus diisi." & Chr(13) & Chr(10))
            End If

            If Not IsDBNull(row(col + 10)) Then
                Try
                    _VechileColorIsActiveOnPK.ModelYear = row(col + 10).ToString
                Catch ex As Exception
                    ErrorMessage.Append("Tahun Model tidak valid." & Chr(13) & Chr(10))
                End Try
            Else
                ErrorMessage.Append("Tahun Model harus diisi." & Chr(13) & Chr(10))
            End If

            If ErrorMessage.Length > 0 Then
                _VechileColorIsActiveOnPK.ErrorMessage = ErrorMessage.ToString

                If IsDataValid = True Then
                    IsDataValid = False
                End If
            End If
            Return _VechileColorIsActiveOnPK
        End Function

        Private Function IsCustomerExist(ByVal dealer As Dealer, ByVal phone As String, ByVal vehType As String) As Boolean
            Dim vReturn As Boolean = False
            Try
                Dim arrVechileColorIsActiveOnPK As ArrayList
                Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileColorIsActiveOnPK), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteria.opAnd(New Criteria(GetType(VechileColorIsActiveOnPK), "Phone", MatchType.Exact, phone))
                'criteria.opAnd(New Criteria(GetType(VechileColorIsActiveOnPK), "Status", MatchType.No, CInt(EnumVechileColorIsActiveOnPKStatus.VechileColorIsActiveOnPKStatus.Deal_SPK)))
                criteria.opAnd(New Criteria(GetType(VechileColorIsActiveOnPK), "Dealer.ID", MatchType.Exact, dealer.ID))
                If vehType <> String.Empty Then
                    criteria.opAnd(New Criteria(GetType(VechileColorIsActiveOnPK), "VechileType.VechileTypeCode", MatchType.Exact, vehType))
                End If

                'arrVechileColorIsActiveOnPK = New KTB.DNet.BusinessFacade.SAP.VechileColorIsActiveOnPKFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criteria)

                'If arrVechileColorIsActiveOnPK.Count > 0 Then
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

