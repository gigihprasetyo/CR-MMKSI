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

    Public Class UploadChassisCampaignParser
        Inherits AbstractExcelParser

#Region "Private Variables"
        Private FSChassisCampaign As FSChassisCampaign
        Private UserFSKind As FSKind
        Private UserChassisMaster As ChassisMaster
        Private _FSChassisCampaign As ArrayList
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
            _FSChassisCampaign = New ArrayList  '-- List of Material Promotion 
            IsDataValid = True
            Try
                AbstractExcelParser.ContentTypeExcel = Me.ContentFileType
                Dim Ds As DataSet = ParseExcelDataSet(fileName, sheetName, "")
                If IsNothing(Ds) Then
                    Me.ErrorMessage.Append("Pastikan nama worksheetnya adalah Sheet1.")
                Else

                    'Validasi Column
                    Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSChassisCampaign), "RowStatus", MatchType.Exact, 0))
                    Dim sortCol As SortCollection = New SortCollection
                    sortCol.Add(New Sort(GetType(FSChassisCampaign), "ID", Sort.SortDirection.ASC))

                    If Ds.Tables(0).Columns.Count <> 4 Then
                        Me.ErrorMessage.Append("Jumlah Kolom tidak sesuai. ")
                        Throw New Exception("Jumlah Kolom tidak sesuai. ")
                    End If

                    Dim colNames As String() = New String() {}
                    Dim iCol As Integer = 0
                    Dim dt As DataTable = Ds.Tables(0)
                    'ReDim Preserve colNames(dt.Columns.Count - 1)
                    ReDim Preserve colNames(2)

                    For Each dc As DataColumn In dt.Columns
                        If iCol < 3 Then
                            colNames(iCol) = dc.ColumnName
                            iCol = iCol + 1
                        End If
                    Next

                    'Max Row = 150
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
                        Return _FSChassisCampaign
                        Exit Function
                    End If

                    Dim row As DataRow
                    Dim i As Integer = 0
                    For i = 0 To Ds.Tables(0).Rows.Count - 1
                        row = Ds.Tables(0).Rows(i)
                        Try
                            If Not IsDBNull(row(0)) Then
                                FSChassisCampaign = New FSChassisCampaign
                                If IsDBNull(row(2)) Then
                                    row(2) = "-"
                                End If
                                FSChassisCampaign.Remarks = row(2).ToString
                                FSChassisCampaign.FSKind = New FSKindFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(row(0).ToString)
                                FSChassisCampaign.ChassisMaster = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(row(1).ToString)
                                FSChassisCampaign = ParseFSChassisCampaign(row)
                                _FSChassisCampaign.Add(FSChassisCampaign)
                            End If
                        Catch ex As Exception
                            Me.ErrorMessage.Append(ex.Message)
                        End Try
                    Next
                End If
            Catch ex As Exception
                Me.ErrorMessage.Append(ex.Message)
            End Try
            Return _FSChassisCampaign
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

        Private Function ParseFSChassisCampaign(ByVal row As DataRow) As FSChassisCampaign
            Dim objFSKind As FSKind
            Dim objChassisMaster As ChassisMaster
            Me.ErrorMessage = New StringBuilder
            ErrorMessage.Clear()

            'FSKind
            If Not IsDBNull(row(0)) Then
                Try
                    Dim strFSKind As String = CType(row(0), String)
                    objFSKind = New FSKindFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(strFSKind)
                    If objFSKind.ID <> Nothing Then
                        FSChassisCampaign.FSKind = objFSKind
                        If ErrorMessage.ToString = "OK" Then
                            ErrorMessage.Clear()
                        End If
                        ErrorMessage.Append("OK")
                    Else
                        If ErrorMessage.ToString = "OK" Then
                            ErrorMessage.Clear()
                        End If
                        FSChassisCampaign.FSKind = Nothing
                        ErrorMessage.Append("Jenis Service '" & strFSKind & "' tidak terdefinisi." & Chr(13) & Chr(10))
                    End If
                Catch ex As Exception
                    If ErrorMessage.ToString = "OK" Then
                        ErrorMessage.Clear()
                    End If
                    ErrorMessage.Append("Jenis Service tidak terdefinisi." & Chr(13) & Chr(10))
                End Try
            Else
                If ErrorMessage.ToString = "OK" Then
                    ErrorMessage.Clear()
                End If
                ErrorMessage.Append("Jenis Service tidak terdefinisi." & Chr(13) & Chr(10))
            End If
            'Chassis Master
            If Not IsDBNull(row(1)) Then
                Try
                    Dim strChassisMaster As String = CType(row(1), String)
                    objChassisMaster = New ChassisMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).RetrieveByCompany(strChassisMaster, CompanyCode)
                    If objChassisMaster.ID <> Nothing Then
                        FSChassisCampaign.ChassisMaster = objChassisMaster
                        If ErrorMessage.ToString = "OK" Then
                            ErrorMessage.Clear()
                        End If
                        ErrorMessage.Append("OK")
                    Else
                        If ErrorMessage.ToString = "OK" Then
                            ErrorMessage.Clear()
                        End If
                        FSChassisCampaign.ChassisMaster = Nothing
                        ErrorMessage.Append("Chassis Number '" & strChassisMaster & "' tidak terdefinisi")
                    End If
                Catch ex As Exception
                    If ErrorMessage.ToString = "OK" Then
                        ErrorMessage.Clear()
                    End If
                    ErrorMessage.Append("Chassis Number tidak terdefinisi")
                End Try
            Else
                If ErrorMessage.ToString = "OK" Then
                    ErrorMessage.Clear()
                End If
                ErrorMessage.Append("Chassis Number tidak terdefinisi")
            End If

            'Block
            If Not IsDBNull(row(3)) Then
                Try
                    FSChassisCampaign.IsAllow = CBool(row(3))
                Catch ex As Exception
                   
                    ErrorMessage.Append("Block Status tidak terdefinisi")
                End Try
            Else
                ErrorMessage.Append("Block Status tidak terisi")
            End If

            If Not IsDBNull(row(2)) Then
                FSChassisCampaign.Remarks = row(2).ToString
            End If

            'check exist FSKind and Chassis or no
            If Not FSChassisCampaign.ChassisMaster Is Nothing And Not FSChassisCampaign.FSKind Is Nothing Then
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSChassisCampaign), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSChassisCampaign), "ChassisMaster.ID", MatchType.Exact, FSChassisCampaign.ChassisMaster.ID))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSChassisCampaign), "FSKind.ID", MatchType.Exact, FSChassisCampaign.FSKind.ID))

                Dim data As ArrayList = New ArrayList
                data = New FSChassisCampaignFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                If data.Count > 0 Then
                    ErrorMessage.Clear()
                    ErrorMessage.Append("Jenis Service dan Chassis Number sudah terdaftar")
                End If
            End If

            If ErrorMessage.Length > 0 Then
                FSChassisCampaign.ErrorMessage = ErrorMessage.ToString
                If IsDataValid = True Then
                    IsDataValid = False
                End If
            End If

            Return FSChassisCampaign
        End Function

#End Region

    End Class

End Namespace