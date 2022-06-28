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
'Imports System.Data.Odbc
Imports System.Security.Principal
Imports System.Text
Imports System
Imports System.Data
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.IO
'Imports System.Data.OleDb
Imports Excel
#End Region

#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.General
#End Region

Namespace KTB.DNet.Parser

    Public Class UploadSparePartPemesanan
        Inherits AbstractExcelParser

#Region "Private Variables"
        Private SparePartPOList As ArrayList
        Private SparePartPODetailList As ArrayList
        Private _fileName As String
        Private _sparePartPO As SparePartPO
        Private _sparePartPODetail As SparePartPODetail
        Private ErrorMessage As StringBuilder
#End Region

#Region "Protected Methods"

        ''' <summary>
        ''' New ParsingExcelNoTransaction
        ''' </summary>
        ''' <param name="fileName"></param>
        ''' <param name="sheetName"></param>
        ''' <param name="user"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Overrides Function ParsingExcelNoTransaction(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object
            SparePartPOList = New ArrayList  '-- List of Material Promotion 
            Dim Ds = New System.Data.DataSet

            Ds = ParseExcelDataSet(fileName, sheetName, user)

            'Dim Ds As DataSet = ParseExcelDataSet(fileName, sheetName, "")
            Dim row As DataRow
            Dim i As Integer = 0
            For i = 0 To Ds.Tables(0).Rows.Count - 1
                row = Ds.Tables(0).Rows(i)
                Try
                    If Not IsDBNull(row(0)) Then
                        If CStr(row(0)) = "H" Then
                            _sparePartPO = New SparePartPO
                            _sparePartPO = ParseSparePartPO(row)
                            SparePartPOList.Add(_sparePartPO)
                        ElseIf CStr(row(0)) = "D" Then
                            If Not _sparePartPO Is Nothing Then
                                ParseSparePartPODetail(row, _sparePartPO)
                            End If
                        Else
                            If i > 0 Then
                                _sparePartPO = New SparePartPO
                                _sparePartPO.ErrorMessage = "Kode H / D tidak terdefinisi"
                                SparePartPOList.Add(_sparePartPO)
                            End If
                        End If
                    End If
                Catch ex As Exception

                End Try
            Next

            Return SparePartPOList
        End Function

#Region "Old ParsingExcelNoTransaction"

        'Protected Overrides Function ParsingExcelNoTransaction(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object
        '    SparePartPOList = New ArrayList  '-- List of Material Promotion 
        '    Dim conn As OleDbConnection = New OleDbConnection
        '    Dim connStr As String = String.Format("provider=Microsoft.Jet.OLEDB.4.0; data source='{0}';Extended Properties=""Excel 8.0; IMEX=1; HDR=No;""", fileName)
        '    conn.ConnectionString = connStr ' "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & fileName & ";Extended Properties=""Excel 12.0;IMEX=1"""
        '    conn.Open()
        '    Dim dt As New DataTable
        '    Dim da As OleDbDataAdapter = New OleDbDataAdapter("SELECT * FROM " & sheetName, conn)
        '    da.TableMappings.Add("Table", "TestTable")
        '    Dim Ds = New System.Data.DataSet
        '    da.Fill(Ds)
        '    conn.Close()


        '    'Dim Ds As DataSet = ParseExcelDataSet(fileName, sheetName, "")
        '    Dim row As DataRow
        '    Dim i As Integer = 0
        '    For i = 0 To Ds.Tables(0).Rows.Count - 1
        '        row = Ds.Tables(0).Rows(i)
        '        Try
        '            If Not IsDBNull(row(0)) Then
        '                If CStr(row(0)) = "H" Then
        '                    _sparePartPO = New SparePartPO
        '                    _sparePartPO = ParseSparePartPO(row)
        '                    SparePartPOList.Add(_sparePartPO)
        '                ElseIf CStr(row(0)) = "D" Then
        '                    If Not _sparePartPO Is Nothing Then
        '                        ParseSparePartPODetail(row, _sparePartPO)
        '                    End If
        '                Else
        '                    If i > 0 Then
        '                        _sparePartPO = New SparePartPO
        '                        _sparePartPO.ErrorMessage = "Kode H / D tidak terdefinisi"
        '                        SparePartPOList.Add(_sparePartPO)
        '                    End If
        '                End If
        '            End If
        '        Catch ex As Exception

        '        End Try
        '    Next

        '    Return SparePartPOList
        'End Function

#End Region

        Private Function ParseSparePartPO(ByVal row As DataRow) As SparePartPO

            ErrorMessage = New StringBuilder

            'Dealer
            If Not row(1) Is Nothing Then
                Try
                    Dim strDealer As String = CType(row(1), String)
                    Dim objDealer As Dealer = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(strDealer)
                    If objDealer.ID > 0 Then
                        _sparePartPO.Dealer = objDealer
                    Else
                        ErrorMessage.Append("Kode Dealer " & strDealer & " tidak terdefinisi." & Chr(13) & Chr(10))
                    End If
                Catch ex As Exception
                    ErrorMessage.Append("Kode Dealer tidak terdefinisi." & Chr(13) & Chr(10))
                End Try

            Else
                ErrorMessage.Append("Kode Dealer tidak terdefinisi." & Chr(13) & Chr(10))
            End If

            If Not row(2) Is Nothing Then
                Try
                    Dim strDate As String = CType(row(2), String)
                    _sparePartPO.PODate = Date.Now
                    _sparePartPO.DeliveryDate = New Date(strDate.Substring(0, 4), strDate.Substring(4, 2), strDate.Substring(6, 2))
                Catch ex As Exception
                    ErrorMessage.Append("Tanggal PO tidak valid.(yyyymmdd)" & Chr(13) & Chr(10))
                End Try
            Else
                ErrorMessage.Append("Tanggal PO tidak valid.(yyyymmdd)" & Chr(13) & Chr(10))
            End If

            If Not row(3) Is Nothing Then
                Try
                    If CType(row(3), String).Length > 30 Then
                        ErrorMessage.Append("Picking Ticket lebih dari 30 karakter" & Chr(13) & Chr(10))
                    Else
                        _sparePartPO.PickingTicket = CType(row(3), String)
                    End If
                Catch ex As Exception
                    ErrorMessage.Append("Picking Ticket tidak  terdefinisi" & Chr(13) & Chr(10))
                End Try
            Else
                ErrorMessage.Append("Picking Ticket tidak boleh kosong" & Chr(13) & Chr(10))
            End If

            If row.ItemArray.Length > 4 Then

                If Not row(4) Is Nothing Then
                    Try
                        'Dim listOfPayments As ArrayList
                        Dim strTOPCode As String = CType(row(4), String)
                        Dim objTOP As TermOfPayment = New TermOfPaymentFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(strTOPCode)
                        _sparePartPO.TermOfPayment = objTOP
                    Catch ex As Exception
                        ErrorMessage.Append("Term Of Payment tidak terdefinisi." & Chr(13) & Chr(10))
                    End Try
                Else
                    ErrorMessage.Append("Term Of Payment tidak terdefinisi." & Chr(13) & Chr(10))
                End If
            End If

            If ErrorMessage.Length > 0 Then
                _sparePartPO.ErrorMessage = ErrorMessage.ToString
            End If

            Return _sparePartPO
        End Function

        Private Sub ParseSparePartPODetail(ByVal row As DataRow, ByVal oSparePartPO As SparePartPO)
            ErrorMessage = New StringBuilder
            _sparePartPODetail = New SparePartPODetail
            If Not row(1) Is Nothing Then
                Try
                    Dim criterias As New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(SparePartMaster), "PartNumber", MatchType.Exact, row(1)))

                    Dim arlSparePart As ArrayList = New SparePartMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                    If arlSparePart.Count > 0 Then
                        Dim oSparePartMaster As SparePartMaster = CType(arlSparePart(0), SparePartMaster)
                        If Not oSparePartMaster Is Nothing Then
                            If oSparePartMaster.ActiveStatus = 0 Then
                                _sparePartPODetail.SparePartMaster = oSparePartMaster
                                _sparePartPODetail.RetailPrice = oSparePartMaster.RetalPrice
                            Else
                                ErrorMessage.Append("Part Number " & row(1) & " tidak aktif" & Chr(13) & Chr(10))
                            End If
                        Else
                            ErrorMessage.Append("Part Number " & row(1) & " tidak terdefinisi" & Chr(13) & Chr(10))
                        End If
                    Else
                        ErrorMessage.Append("Part Number " & row(1) & " tidak terdefinisi" & Chr(13) & Chr(10))
                    End If
                Catch ex As Exception
                    ErrorMessage.Append("Part Number tidak terdefinisi" & Chr(13) & Chr(10))
                End Try
            Else
                ErrorMessage.Append("Part Number tidak boleh kosong" & Chr(13) & Chr(10))
            End If

            If Not row(2) Is Nothing Then
                Try
                    If CType(row(2), Integer) > 0 Then
                        _sparePartPODetail.Quantity = CType(row(2), Integer)
                    Else
                        ErrorMessage.Append("Quantity harus >= 1" & Chr(13) & Chr(10))
                    End If
                Catch ex As Exception
                    ErrorMessage.Append("Quantity tidak  terdefinisi" & Chr(13) & Chr(10))
                End Try

            Else
                ErrorMessage.Append("Quantity tidak boleh kosong" & Chr(13) & Chr(10))
            End If
            If ErrorMessage.Length > 0 Then
                _sparePartPODetail.ErrorMessage = ErrorMessage.ToString
            End If
            oSparePartPO.SparePartPODetails.Add(_sparePartPODetail)
        End Sub

        Private Function ExistPart(ByVal partNumber As String, ByVal dataCollection As ArrayList)
            For Each objPoDetail As SparePartPODetail In dataCollection
                If Not IsNothing(objPoDetail.SparePartMaster) Then
                    If objPoDetail.SparePartMaster.PartNumber.Trim.ToUpper = partNumber.Trim.ToUpper Then
                        Return True
                    End If
                End If
            Next
            Return False
        End Function

#End Region

    End Class

End Namespace