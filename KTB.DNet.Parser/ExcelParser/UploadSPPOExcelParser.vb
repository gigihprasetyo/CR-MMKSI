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
Imports Excel
Imports System.Security.Principal

#End Region

#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.SparePart
Imports System.IO

#End Region

Namespace KTB.DNet.Parser

    Public Class UploadSPPOExcelParser
        Inherits AbstractExcelParser

#Region "Private Variables"
        Private _companyCode As String
        Private _strPQRNo As String = String.Empty
#End Region

#Region "Constructors/Destructors/Finalizers"
        Public Sub New()
        End Sub

        Public Sub New(ByVal companyCode As String)
            Me._companyCode = companyCode
        End Sub
#End Region

#Region "Protected Methods"

        Protected Overrides Function ParsingExcelNoTransaction(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object
            DataCollection = New ArrayList  '-- List of sparepart PO details
            Dim parts() As String = fileName.Split(".".ToCharArray())
            Dim ext As String = parts(parts.Length - 1).Trim().ToLower()
            Dim objReader As IExcelDataReader = Nothing

            Dim PartNumber As String        '-- Part number

            Try

                Using stream As FileStream = File.Open(fileName, FileMode.Open, FileAccess.Read)
                    Dim Row As Integer = 0
                    If (ext = "xls") Then
                        objReader = ExcelReaderFactory.CreateBinaryReader(stream)
                    ElseIf (ext = "xlsx") Then
                        objReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
                    End If
                    objReader.IsFirstRowAsColumnNames = True


                    If (Not IsNothing(objReader)) Then

                        While objReader.Read()

                            '-- In case of failed reading subsequent sparepart
                            '-- simply return already successfully read spareparts
                            If (Row = 0 AndAlso objReader.IsFirstRowAsColumnNames) Then
                                Row = Row + 1
                                Continue While

                            End If
                            Try
                                PartNumber = objReader.GetString(0)  '-- PO detail's part number
                                PartNumber = PartNumber.Trim()  '-- Always trim spaces

                            Catch ex As Exception
                                'If objConn.State = ConnectionState.Open Then
                                '    objConn.Close()  '-- Close connection if already open
                                'End If
                                
                                Return DataCollection  '-- Return list of sparepart PO details
                            End Try
                            Row = Row + 1


                            Dim _sparePartPODetail As New SparePartPODetail  '-- PO detail

                            '-- Retrieve its sparepart master record if any
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(SparePartMaster), "PartNumber", MatchType.Exact, PartNumber))
                            Dim partColl As ArrayList = New SparePartMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)

                            If partColl.Count = 0 Then
                                _sparePartPODetail.NotExistPartNumber = PartNumber
                                _sparePartPODetail.ErrorMessage &= "No.Part tdk ada;"
                            Else
                                Dim objSparepartMaster As SparePartMaster = CType(partColl(0), SparePartMaster)
                                If Me._companyCode.Trim.ToUpper <> objSparepartMaster.ProductCategory.Code.ToUpper Then
                                    _sparePartPODetail.ErrorMessage &= "No.Part tdk ada di " & Me._companyCode.Trim.ToUpper & ";"
                                End If

                                _sparePartPODetail.RetailPrice = CType(partColl(0), SparePartMaster).RetalPrice
                                _sparePartPODetail.SparePartMaster = CType(partColl(0), SparePartMaster)

                                'New LOC 
                                'DATE : 2014 -08 -15
                                'On behalf VAlidasi I,E, & A
                                'By : Ali
                                If (_sparePartPODetail.SparePartMaster.TypeCode.ToUpper = "I" OrElse _sparePartPODetail.SparePartMaster.TypeCode.ToUpper = "E" _
                                    OrElse _sparePartPODetail.SparePartMaster.TypeCode.ToUpper = "X" OrElse _sparePartPODetail.SparePartMaster.TypeCode.ToUpper = "A") Then
                                    _sparePartPODetail.ErrorMessage &= "Untuk Sparepart dengan Type I,E,X dan A harap dipesan lewat menu Indent Part;"
                                End If
                                ' END OF NEW LOC

                                'start add by anh 2017-02-01 req by Maria Anna
                                If _sparePartPODetail.SparePartMaster.TypeCode = "P" Then
                                    _sparePartPODetail.ErrorMessage &= "Untuk Sparepart dengan Type P harap hubungi Customer Support;"
                                End If
                                'end add by anh 2017-02-01 req by Maria Anna

                                If ExistPart(_sparePartPODetail.SparePartMaster.PartNumber, DataCollection) Then
                                    _sparePartPODetail.ErrorMessage &= "No.Part sudah ada;"
                                End If
                                If _sparePartPODetail.SparePartMaster.ActiveStatus <> CType(EnumSparePartActiveStatus.SparePartActiveStatus.Active, Short) Then
                                    _sparePartPODetail.ErrorMessage &= "No.Part tidak aktif"
                                End If
                            End If

                            Try
                                If objReader(1) < 1 Then
                                    _sparePartPODetail.ErrorMessage &= "Quantity harus >= 1;"
                                End If
                                _sparePartPODetail.Quantity = objReader(1)  '-- Assign quantity

                            Catch ex As Exception
                                _sparePartPODetail.ErrorMessage &= "Qty tdk valid;"
                            End Try

                            DataCollection.Add(_sparePartPODetail)  '-- Add object to collection

                        End While




                    End If

                End Using

 

            Catch ex As Exception
                Dim str As String = ex.Message
                Return Nothing

            Finally
                'If objConn.State = ConnectionState.Open Then
                '    objConn.Close()  '-- Close connection if already open
                'End If
            End Try

            Return DataCollection  '-- Return list of sparepart PO details
        End Function

#Region "Old ParsingExcelNoTransaction"

        'Protected Overrides Function ParsingExcelNoTransaction(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object
        '    DataCollection = New ArrayList  '-- List of sparepart PO details
        '    Dim objConn As OdbcConnection   '-- Connection object
        '    Dim PartNumber As String        '-- Part number

        '    Try
        '        Dim strConn As String = StrConnection & fileName  '-- Connection string
        '        objConn = New OdbcConnection(strConn)
        '        objConn.Open()  '-- Open connection

        '        Dim objCmd = New OdbcCommand("SELECT * FROM " & sheetName, objConn)  '-- Command object
        '        objCmd.CommandType = CommandType.Text
        '        objReader = objCmd.ExecuteReader()  '-- Read data

        '        While objReader.Read()

        '            '-- In case of failed reading subsequent sparepart
        '            '-- simply return already successfully read spareparts
        '            Try
        '                PartNumber = objReader(0)  '-- PO detail's part number
        '                PartNumber = PartNumber.Trim()  '-- Always trim spaces

        '            Catch ex As Exception
        '                If objConn.State = ConnectionState.Open Then
        '                    objConn.Close()  '-- Close connection if already open
        '                End If

        '                Return DataCollection  '-- Return list of sparepart PO details
        '            End Try

        '            Dim _sparePartPODetail As New SparePartPODetail  '-- PO detail

        '            '-- Retrieve its sparepart master record if any
        '            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '            criterias.opAnd(New Criteria(GetType(SparePartMaster), "PartNumber", MatchType.Exact, PartNumber))
        '            Dim partColl As ArrayList = New SparePartMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)

        '            If partColl.Count = 0 Then
        '                _sparePartPODetail.NotExistPartNumber = PartNumber
        '                _sparePartPODetail.ErrorMessage &= "No.Part tdk ada;"
        '            Else
        '                _sparePartPODetail.RetailPrice = CType(partColl(0), SparePartMaster).RetalPrice
        '                _sparePartPODetail.SparePartMaster = CType(partColl(0), SparePartMaster)

        '                'New LOC 
        '                'DATE : 2014 -08 -15
        '                'On behalf VAlidasi I,E, & A
        '                'By : Ali
        '                If (_sparePartPODetail.SparePartMaster.TypeCode.ToUpper = "I" OrElse _sparePartPODetail.SparePartMaster.TypeCode.ToUpper = "E" OrElse _sparePartPODetail.SparePartMaster.TypeCode.ToUpper = "A") Then
        '                    _sparePartPODetail.ErrorMessage &= "Untuk Sparepart dengan Type I,E dan A harap dipesan lewat menu Indent Part;"
        '                End If
        '                ' END OF NEW LOC

        '                If ExistPart(_sparePartPODetail.SparePartMaster.PartNumber, DataCollection) Then
        '                    _sparePartPODetail.ErrorMessage &= "No.Part sudah ada;"
        '                End If
        '                If _sparePartPODetail.SparePartMaster.ActiveStatus <> EnumSparePartActiveStatus.SparePartActiveStatus.Active Then
        '                    _sparePartPODetail.ErrorMessage &= "No.Part tidak aktif"
        '                End If
        '            End If

        '            Try
        '                If objReader(1) < 1 Then
        '                    _sparePartPODetail.ErrorMessage &= "Quantity harus >= 1;"
        '                End If
        '                _sparePartPODetail.Quantity = objReader(1)  '-- Assign quantity

        '            Catch ex As Exception
        '                _sparePartPODetail.ErrorMessage &= "Qty tdk valid;"
        '            End Try

        '            DataCollection.Add(_sparePartPODetail)  '-- Add object to collection
        '        End While

        '    Catch ex As Exception
        '        Dim str As String = ex.Message
        '        Return Nothing

        '    Finally
        '        If objConn.State = ConnectionState.Open Then
        '            objConn.Close()  '-- Close connection if already open
        '        End If
        '    End Try

        '    Return DataCollection  '-- Return list of sparepart PO details
        'End Function

#End Region
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