#Region " Summary "
'---------------------------------------------------'
'-- Program Code : UploadSPSpecialItemParser.vb   --'
'-- Program Name : Parser sparepart special item  --'
'-- Description  :                                --'
'---------------------------------------------------'
'-- Programmer   : Agus Pirnadi                   --'
'-- Start Date   : Oct 14 2005                    --'
'-- Update By    :                                --'
'-- Last Update  : Dec 23 2005                    --'
'---------------------------------------------------'
'-- Copyright © 2005 by Intimedia                 --'
'---------------------------------------------------'
#End Region

#Region " .NET Base Class Namespace Imports "
'Imports System.Data.Odbc
Imports System.Security.Principal
#End Region

#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.SparePart
Imports Excel
Imports System.IO

#End Region

Namespace KTB.DNet.Parser

    Public Class UploadSPSpecialItemParser
        Inherits AbstractExcelParser

#Region "Private fields"
        Dim siHeader As SpecialItemHeader   '-- Special item header (Root object)
        Dim curDetail As SpecialItemDetail  '-- Pointer to current Detail
        Private CompanyCode As String
#End Region

        Public Sub New(Optional ByVal companyCode As String = "")
            Me.CompanyCode = companyCode
        End Sub


#Region "Protected Methods"

        Protected Overrides Function ParsingExcelNoTransaction(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object

            'Dim objConn As OdbcConnection  '-- Connection object
            'Dim strConn As String = StrConnection & fileName  '-- Connection string

            Dim iRow As Integer = 1  '-- Row number (Row number-1 is omitted)
            sheetName = sheetName.Replace("[", "").Replace("]", "").Replace("$", "")


            Try
                Dim parts() As String = fileName.Split(".".ToCharArray())
                Dim ext As String = parts(parts.Length - 1).Trim().ToLower()
                Dim objReader As IExcelDataReader = Nothing

                Using stream As FileStream = File.Open(fileName, FileMode.Open, FileAccess.Read)
                    If (ext = "xls") Then
                        objReader = ExcelReaderFactory.CreateBinaryReader(stream)
                    ElseIf (ext = "xlsx") Then
                        objReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
                    End If

                    If (Not IsNothing(objReader)) Then
                        'Do
                        While objReader.Read()
                            '-- This row number
                            If (objReader.Name.Contains(sheetName)) Then

                                iRow += 1
                                If iRow = 2 Then
                                    Continue While
                                End If
                                Select Case iRow
                                    Case 3, 4, 5  '-- Row-2, 3, & 4 are of header info
                                        ProcessHeader(iRow, objReader)  '-- Process header info

                                    Case Else  '-- Other rows are of detail & package info
                                        ProcessDetail(objReader)   '-- Process detail info
                                        ProcessPackage(objReader)  '-- Process package info
                                End Select

                            Else
                                objReader.NextResult()
                            End If

                        End While


                        'Loop While Not objReader.NextResult()

                    End If

                End Using

          

            Catch ex As Exception
                Dim str As String = ex.Message
                Return Nothing  '-- If any error occurs then return Nothing

            Finally
             
            End Try

            Return siHeader  '-- Return Special Item's header object
        End Function

        Private Sub ProcessHeader(ByVal iRow As Integer, ByVal objReader As IExcelDataReader)
            '-- Process info of special item header

            Select Case iRow
                Case 3 '2  '-- Period 
                    siHeader = New SpecialItemHeader   '-- Instantiate header the first time
                    Try
                        siHeader.MonthPeriode = CType(objReader.GetString(0), Integer)  '-- Month of period
                        If Not (1 <= siHeader.MonthPeriode And siHeader.MonthPeriode <= 12) Then
                            siHeader.ErrorMessage &= "Periode invalid;"
                        End If

                        siHeader.YearPeriode = CType(objReader.GetString(1), Integer)   '-- Year of period
                        If siHeader.YearPeriode < 1900 Then
                            siHeader.ErrorMessage &= "Tahun harus >= 1900;"
                        End If

                    Catch ex As Exception
                        siHeader.ErrorMessage &= "Periode invalid;"
                    End Try
                Case 4 '3
                    siHeader.Reference = Trim(objReader.GetString(0))  '-- Reference
                Case 5 '4
                    siHeader.Remark = Trim(objReader.GetString(0))  '-- Remark
            End Select

        End Sub

        Private Sub ProcessDetail(ByVal objReader As IExcelDataReader)
            '-- Process info of special item detail

            Try
                Dim PartNumber As String = Trim(objReader.GetString(0))  '-- Special item detail's part number

                '-- Check if detail already exists
                If Not bDetailExists(PartNumber) Then

                    '-- If not exist then create a new one
                    Dim siDetail As New SpecialItemDetail  '-- Special item detail

                    '-- Retrieve its sparepart master record if any
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(SparePartMaster), "PartNumber", MatchType.Exact, PartNumber))
                    If Me.CompanyCode.Trim <> "" Then
                        criterias.opAnd(New Criteria(GetType(SparePartMaster), "ProductCategory.Code", MatchType.Exact, Me.CompanyCode.Trim.ToUpper))
                    End If

                    Dim partColl As ArrayList = New SparePartMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)

                    If partColl.Count > 0 Then
                        Dim objSparePartMaster As SparePartMaster = CType(partColl(0), SparePartMaster)
                        siDetail.SparePartMaster = objSparePartMaster  '-- Sparepart master
                        If objSparePartMaster.ActiveStatus = EnumSparePartActiveStatus.SparePartActiveStatus.InActive Then
                            siDetail.ErrorMessage &= "Sparepart tidak aktif ;"
                        End If
                    Else
                        siDetail.SparePartMaster = New SparePartMaster
                        siDetail.ErrorMessage &= "Sparepart tidak ada;"
                    End If

                    Dim MatGroup As String = Trim(objReader.GetString(1))  '-- SI detail's material group
                    siDetail.ExtMaterialGroup = MatGroup  '-- Material group

                    Dim PartName As String = Trim(objReader.GetString(2))  '-- SI detail's part name
                    siDetail.PartName = PartName  '-- Part name

                    Dim ModelCode As String = Trim(objReader.GetString(3))  '-- SI detail's model code
                    siDetail.ModelCode = ModelCode  '-- Model code

                    Dim ItemStatus As Byte = CType(Trim(objReader.GetString(4)), Byte)  '-- SI detail's item status
                    siDetail.ItemStatus = ItemStatus  '-- Item status

                    siDetail.SpecialItemHeader = siHeader  '-- Assign its header
                    siHeader.SpecialItemDetails.Add(siDetail)  '-- Register to its header arraylist

                    curDetail = siDetail  '-- Keep current Detail pointer for later use
                Else
                    curDetail.ErrorMessage &= "Duplikasi sparepart;"  '-- Duplicate sparepart exists
                End If

            Catch ex As Exception
                '-- Does nothing
            End Try

        End Sub

        Private Function bDetailExists(ByVal PartNumber As String) As Boolean
            '-- Check if detail already exists

            For Each item As SpecialItemDetail In siHeader.SpecialItemDetails

                If Not IsNothing(item.SparePartMaster) Then
                    If item.SparePartMaster.PartNumber = PartNumber Then
                        curDetail = item  '-- Keep current Detail pointer for later use
                        Return True  '-- Already exists
                    End If
                End If
            Next

            Return False  '-- Doesn't exist
        End Function

        Private Sub ProcessPackage(ByVal objReader As IExcelDataReader)
            '-- Process info of special item packages

            Dim bNoMorePackage As Boolean = False  '-- True if no more package available, otherwise False

            Dim colPackage As Integer = 6  '-- First package column
            Dim colPrice As Integer = 5    '-- First price column
            Dim PackNo As Short = 1        '-- First package number

            Do
                Try
                    Dim siPackage As New SpecialItemPackage  '-- Create special item package

                    siPackage.PackageDescription = Trim(objReader(colPackage))  '-- Assign package description
                    siPackage.PackageNo = PackNo  '-- Package number

                    Try
                        siPackage.PackagePrice = CType(objReader(colPrice), Double)  '-- Assign package price

                        If InStr(objReader.GetValue(colPrice), ".", CompareMethod.Text) <> 0 Or _
                           InStr(objReader.GetValue(colPrice), ",", CompareMethod.Text) <> 0 Then

                            siPackage.ErrorMessage &= "Harga paket tdk boleh pecahan;"
                        End If

                        If siPackage.PackagePrice <= 0 Then
                            '-- Package price may not be less than or equal to zero
                            siPackage.ErrorMessage &= "Harga paket <= 0;"
                        End If

                    Catch ex As Exception
                        siPackage.ErrorMessage &= "Harga paket invalid;"
                    End Try

                    siPackage.SpecialItemDetail = curDetail  '-- Assign its header
                    curDetail.SpecialItemPackages.Add(siPackage)  '-- Register to its header arraylist

                    colPackage += 2  '-- Next package column
                    colPrice += 2    '-- Next price column
                    PackNo += 1      '-- Next package number

                Catch ex As Exception
                    bNoMorePackage = True  '-- No more package available
                End Try

            Loop Until bNoMorePackage  '-- Exit loop if no more package available

        End Sub

#End Region

    End Class

#Region "OLD UploadSPSpecialItemParser"
    '    Public Class UploadSPSpecialItemParser
    '        Inherits AbstractExcelParser

    '#Region "Private fields"
    '        Dim siHeader As SpecialItemHeader   '-- Special item header (Root object)
    '        Dim curDetail As SpecialItemDetail  '-- Pointer to current Detail
    '#End Region

    '#Region "Protected Methods"

    '        Protected Overrides Function ParsingExcelNoTransaction(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object

    '            Dim objConn As OdbcConnection  '-- Connection object
    '            Dim strConn As String = StrConnection & fileName  '-- Connection string

    '            Dim iRow As Integer = 1  '-- Row number (Row number-1 is omitted)

    '            Try
    '                objConn = New OdbcConnection(strConn)
    '                objConn.Open()  '-- Open connection

    '                Dim objCmd = New OdbcCommand("SELECT * FROM " & sheetName, objConn)  '-- Command object
    '                objCmd.CommandType = CommandType.Text
    '                objReader = objCmd.ExecuteReader()  '-- Read data

    '                While objReader.Read()  '-- Read row-by-row

    '                    iRow += 1  '-- This row number

    '                    Select Case iRow
    '                        Case 2, 3, 4  '-- Row-2, 3, & 4 are of header info
    '                            ProcessHeader(iRow, objReader)  '-- Process header info

    '                        Case Else  '-- Other rows are of detail & package info
    '                            ProcessDetail(objReader)   '-- Process detail info
    '                            ProcessPackage(objReader)  '-- Process package info
    '                    End Select

    '                End While

    '            Catch ex As Exception
    '                Dim str As String = ex.Message
    '                Return Nothing  '-- If any error occurs then return Nothing

    '            Finally
    '                If objConn.State = ConnectionState.Open Then
    '                    objConn.Close()  '-- Close connection if already open
    '                End If
    '            End Try

    '            Return siHeader  '-- Return Special Item's header object
    '        End Function

    '        Private Sub ProcessHeader(ByVal iRow As Integer, ByVal objReader As OdbcDataReader)
    '            '-- Process info of special item header

    '            Select Case iRow
    '                Case 2  '-- Period
    '                    siHeader = New SpecialItemHeader   '-- Instantiate header the first time
    '                    Try
    '                        siHeader.MonthPeriode = CType(objReader(0), Integer)  '-- Month of period
    '                        If Not (1 <= siHeader.MonthPeriode And siHeader.MonthPeriode <= 12) Then
    '                            siHeader.ErrorMessage &= "Periode invalid;"
    '                        End If

    '                        siHeader.YearPeriode = CType(objReader(1), Integer)   '-- Year of period
    '                        If siHeader.YearPeriode < 1900 Then
    '                            siHeader.ErrorMessage &= "Tahun harus >= 1900;"
    '                        End If

    '                    Catch ex As Exception
    '                        siHeader.ErrorMessage &= "Periode invalid;"
    '                    End Try
    '                Case 3
    '                    siHeader.Reference = Trim(objReader(0))  '-- Reference
    '                Case 4
    '                    siHeader.Remark = Trim(objReader(0))  '-- Remark
    '            End Select

    '        End Sub

    '        Private Sub ProcessDetail(ByVal objReader As OdbcDataReader)
    '            '-- Process info of special item detail

    '            Try
    '                Dim PartNumber As String = Trim(objReader(0))  '-- Special item detail's part number

    '                '-- Check if detail already exists
    '                If Not bDetailExists(PartNumber) Then

    '                    '-- If not exist then create a new one
    '                    Dim siDetail As New SpecialItemDetail  '-- Special item detail

    '                    '-- Retrieve its sparepart master record if any
    '                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '                    criterias.opAnd(New Criteria(GetType(SparePartMaster), "PartNumber", MatchType.Exact, PartNumber))
    '                    Dim partColl As ArrayList = New SparePartMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)

    '                    If partColl.Count > 0 Then
    '                        siDetail.SparePartMaster = CType(partColl(0), SparePartMaster)  '-- Sparepart master
    '                    Else
    '                        siDetail.SparePartMaster = New SparePartMaster
    '                        siDetail.ErrorMessage &= "Sparepart tidak ada;"
    '                    End If

    '                    Dim MatGroup As String = Trim(objReader(1))  '-- SI detail's material group
    '                    siDetail.ExtMaterialGroup = MatGroup  '-- Material group

    '                    Dim PartName As String = Trim(objReader(2))  '-- SI detail's part name
    '                    siDetail.PartName = PartName  '-- Part name

    '                    Dim ModelCode As String = Trim(objReader(3))  '-- SI detail's model code
    '                    siDetail.ModelCode = ModelCode  '-- Model code

    '                    Dim ItemStatus As Byte = CType(Trim(objReader(4)), Byte)  '-- SI detail's item status
    '                    siDetail.ItemStatus = ItemStatus  '-- Item status

    '                    siDetail.SpecialItemHeader = siHeader  '-- Assign its header
    '                    siHeader.SpecialItemDetails.Add(siDetail)  '-- Register to its header arraylist

    '                    curDetail = siDetail  '-- Keep current Detail pointer for later use
    '                Else
    '                    curDetail.ErrorMessage &= "Duplikasi sparepart;"  '-- Duplicate sparepart exists
    '                End If

    '            Catch ex As Exception
    '                '-- Does nothing
    '            End Try

    '        End Sub

    '        Private Function bDetailExists(ByVal PartNumber As String) As Boolean
    '            '-- Check if detail already exists

    '            For Each item As SpecialItemDetail In siHeader.SpecialItemDetails

    '                If Not IsNothing(item.SparePartMaster) Then
    '                    If item.SparePartMaster.PartNumber = PartNumber Then
    '                        curDetail = item  '-- Keep current Detail pointer for later use
    '                        Return True  '-- Already exists
    '                    End If
    '                End If
    '            Next

    '            Return False  '-- Doesn't exist
    '        End Function

    '        Private Sub ProcessPackage(ByVal objReader As OdbcDataReader)
    '            '-- Process info of special item packages

    '            Dim bNoMorePackage As Boolean = False  '-- True if no more package available, otherwise False

    '            Dim colPackage As Integer = 6  '-- First package column
    '            Dim colPrice As Integer = 5    '-- First price column
    '            Dim PackNo As Short = 1        '-- First package number

    '            Do
    '                Try
    '                    Dim siPackage As New SpecialItemPackage  '-- Create special item package

    '                    siPackage.PackageDescription = Trim(objReader(colPackage))  '-- Assign package description
    '                    siPackage.PackageNo = PackNo  '-- Package number

    '                    Try
    '                        siPackage.PackagePrice = CType(objReader(colPrice), Double)  '-- Assign package price

    '                        If InStr(objReader(colPrice), ".", CompareMethod.Text) <> 0 Or _
    '                           InStr(objReader(colPrice), ",", CompareMethod.Text) <> 0 Then

    '                            siPackage.ErrorMessage &= "Harga paket tdk boleh pecahan;"
    '                        End If

    '                        If siPackage.PackagePrice <= 0 Then
    '                            '-- Package price may not be less than or equal to zero
    '                            siPackage.ErrorMessage &= "Harga paket <= 0;"
    '                        End If

    '                    Catch ex As Exception
    '                        siPackage.ErrorMessage &= "Harga paket invalid;"
    '                    End Try

    '                    siPackage.SpecialItemDetail = curDetail  '-- Assign its header
    '                    curDetail.SpecialItemPackages.Add(siPackage)  '-- Register to its header arraylist

    '                    colPackage += 2  '-- Next package column
    '                    colPrice += 2    '-- Next price column
    '                    PackNo += 1      '-- Next package number

    '                Catch ex As Exception
    '                    bNoMorePackage = True  '-- No more package available
    '                End Try

    '            Loop Until bNoMorePackage  '-- Exit loop if no more package available

    '        End Sub

    '#End Region

    '    End Class
#End Region

End Namespace
