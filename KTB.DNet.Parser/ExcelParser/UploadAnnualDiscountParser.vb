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
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Utility
Imports System.IO

#End Region

Namespace KTB.DNet.Parser

    Public Class UploadAnnualDiscountParser
        Inherits AbstractExcelParser


#Region "Private Variables"
        Private _companyCode As String
#End Region

#Region "Constructors/Destructors/Finalizers"
        Public Sub New()
        End Sub

        Public Sub New(ByVal companyCode As String)
            Me._companyCode = companyCode
        End Sub
#End Region

#Region "Protected Methods"

        ''' <summary>
        ''' NEW ParsingExcelNoTransaction
        ''' </summary>
        ''' <param name="fileName"></param>
        ''' <param name="sheetName"></param>
        ''' <param name="user"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Overrides Function ParsingExcelNoTransaction(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object
            DataCollection = New ArrayList  '-- List of sparepart PO details
            'Dim objConn As OdbcConnection   '-- Connection object
            Dim parts() As String = fileName.Split(".".ToCharArray())
            Dim ext As String = parts(parts.Length - 1).Trim().ToLower()
            Dim objReader As IExcelDataReader = Nothing
            Dim strError As String = String.Empty

            Try

                Using stream As FileStream = File.Open(fileName, FileMode.Open, FileAccess.Read)
                    If (ext = "xls") Then
                        objReader = ExcelReaderFactory.CreateBinaryReader(stream)
                    ElseIf (ext = "xlsx") Then
                        objReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
                    End If

                    If (Not IsNothing(objReader)) Then

                        Dim Idx As Integer = 0

                        While objReader.Read()

                            Idx = Idx + 1
                            If (Idx = 1) Then
                                Continue While
                            End If

                            Dim _annualDiscount As New AnnualDiscount
                            _annualDiscount.PartNo = objReader.GetString(0)
                            Dim objSparepartMaster As SparePartMaster = New SparePartMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(CStr(objReader.GetString(0)))
                            If objSparepartMaster.ID <> 0 Then
                                _annualDiscount.PartName = objSparepartMaster.PartName
                                _annualDiscount.Model = objSparepartMaster.ModelCode
                            Else
                                If strError = String.Empty Then
                                    strError = "SparePart Tidak Ditemukan"
                                Else
                                    strError = strError & ";SparePart Tidak Ditemukan"
                                End If
                            End If
                            _annualDiscount.MinimumQty = objReader.GetInt32(3)
                            If CInt(objReader.GetInt32(3)) <= 0 Then
                                If strError = String.Empty Then
                                    strError = "MinimumQty harus > 0"
                                Else
                                    strError = strError & ";MinimumQty harus > 0"
                                End If
                            End If
                            _annualDiscount.Point = objReader.GetInt32(4)
                            If CInt(objReader.GetInt32(4)) < 0 Then
                                If strError = String.Empty Then
                                    strError = "Point harus > 0"
                                Else
                                    strError = strError & ";Point harus > 0"
                                End If
                            End If
                            _annualDiscount.ErrorMessage = strError
                            strError = String.Empty
                            DataCollection.Add(_annualDiscount)  '-- Add object to collection

                        End While




                    End If

                End Using


               
            Catch ex As Exception
                '    Dim str As String = ex.Message
                '    Return Nothing
                MessageBox.Show("Data Tidak Valid")
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

        '    Try
        '        Dim strConn As String = StrConnection & fileName  '-- Connection string
        '        objConn = New OdbcConnection(strConn)
        '        objConn.Open()  '-- Open connection

        '        Dim objCmd = New OdbcCommand("SELECT * FROM " & sheetName, objConn)  '-- Command object
        '        objCmd.CommandType = CommandType.Text
        '        objReader = objCmd.ExecuteReader()  '-- Read data
        '        Dim strError As String = String.Empty
        '        While objReader.Read()
        '            Dim _annualDiscount As New AnnualDiscount
        '            _annualDiscount.PartNo = objReader(0)
        '            Dim objSparepartMaster As SparePartMaster = New SparePartMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(CStr(objReader(0)))
        '            If objSparepartMaster.ID <> 0 Then
        '                _annualDiscount.PartName = objSparepartMaster.PartName
        '                _annualDiscount.Model = objSparepartMaster.ModelCode
        '            Else
        '                If strError = String.Empty Then
        '                    strError = "SparePart Tidak Ditemukan"
        '                Else
        '                    strError = strError & ";SparePart Tidak Ditemukan"
        '                End If
        '            End If
        '            _annualDiscount.MinimumQty = objReader(3)
        '            If CInt(objReader(3)) <= 0 Then
        '                If strError = String.Empty Then
        '                    strError = "MinimumQty harus > 0"
        '                Else
        '                    strError = strError & ";MinimumQty harus > 0"
        '                End If
        '            End If
        '            _annualDiscount.Point = objReader(4)
        '            If CInt(objReader(4)) < 0 Then
        '                If strError = String.Empty Then
        '                    strError = "Point harus > 0"
        '                Else
        '                    strError = strError & ";Point harus > 0"
        '                End If
        '            End If
        '            _annualDiscount.ErrorMessage = strError
        '            strError = String.Empty
        '            DataCollection.Add(_annualDiscount)  '-- Add object to collection
        '        End While
        '    Catch ex As Exception
        '        '    Dim str As String = ex.Message
        '        '    Return Nothing
        '        MessageBox.Show("Data Tidak Valid")
        '    Finally
        '        If objConn.State = ConnectionState.Open Then
        '            objConn.Close()  '-- Close connection if already open
        '        End If
        '    End Try

        '    Return DataCollection  '-- Return list of sparepart PO details
        'End Function

#End Region

        ''' <summary>
        ''' New ParsingExcelNoTransactionWithValidDate
        ''' </summary>
        ''' <param name="fileName"></param>
        ''' <param name="sheetName"></param>
        ''' <param name="user"></param>
        ''' <param name="ValidFrom"></param>
        ''' <param name="ValidTo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        ''' 

        Protected Overrides Function ParsingExcelNoTransactionWithValidDate(ByVal fileName As String, ByVal sheetName As String, ByVal user As String, ByVal ValidFrom As DateTime, ByVal ValidTo As DateTime) As Object
            DataCollection = New ArrayList  '-- List of sparepart PO details
            'Dim objConn As OdbcConnection   '-- Connection object
            Dim parts() As String = fileName.Split(".".ToCharArray())
            Dim ext As String = parts(parts.Length - 1).Trim().ToLower()
            Dim objReader As IExcelDataReader = Nothing
            Dim strError As String = String.Empty

            Try

                Using stream As FileStream = File.Open(fileName, FileMode.Open, FileAccess.Read)
                    If (ext = "xls") Then
                        objReader = ExcelReaderFactory.CreateBinaryReader(stream)
                    ElseIf (ext = "xlsx") Then
                        objReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
                    End If

                    Dim Idx As Integer = 0

                    If (Not IsNothing(objReader)) Then
 
                        While objReader.Read()

                            strError = String.Empty

                            Idx = Idx + 1
                            If (Idx = 1) Then
                                Continue While
                            End If

                            Dim _annualDiscount As New AnnualDiscount
                            _annualDiscount.PartNo = objReader.GetString(0)
                            Dim objSparepartMaster As SparePartMaster = New SparePartMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(CStr(objReader.GetString(0)))
                            If objSparepartMaster.ID <> 0 Then
                                _annualDiscount.PartName = objSparepartMaster.PartName
                                _annualDiscount.Model = objSparepartMaster.ModelCode

                                If Me._companyCode.Trim.ToUpper <> objSparepartMaster.ProductCategory.Code.ToUpper Then
                                    strError = strError & " No.Part tdk ada di " & Me._companyCode.Trim.ToUpper & ";"
                                End If

                                If objSparepartMaster.ActiveStatus <> CType(EnumSparePartActiveStatus.SparePartActiveStatus.Active, Short) Then
                                    strError = strError & " No.Part tidak aktif"
                                End If
                            Else
                                If strError = String.Empty Then
                                    strError = strError & "SparePart Tidak Ditemukan"
                                Else
                                    strError = strError & ";SparePart Tidak Ditemukan"
                                End If
                            End If
                            _annualDiscount.MinimumQty = objReader.GetInt32(3)
                            If CInt(objReader(3)) <= 0 Then
                                If strError = String.Empty Then
                                    strError = "MinimumQty harus > 0"
                                Else
                                    strError = strError & ";MinimumQty harus > 0"
                                End If
                            End If
                            _annualDiscount.Point = objReader.GetInt32(4)
                            If CInt(objReader.GetInt32(4)) < 0 Then
                                If strError = String.Empty Then
                                    strError = "Point harus > 0"
                                Else
                                    strError = strError & ";Point harus > 0"
                                End If
                            End If
                            _annualDiscount.ValidateDateFrom = ValidFrom
                            _annualDiscount.ValidateDateTo = ValidTo
                            Dim tglawal As New DateTime(ValidFrom.Year, 1, 1, 0, 0, 0)
                            Dim tglakhir As New DateTime(ValidFrom.Year, 12, 31, 23, 59, 59)
                            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AnnualDiscount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDiscount), "ValidateDateFrom", MatchType.GreaterOrEqual, tglawal))
                            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDiscount), "ValidateDateFrom", MatchType.LesserOrEqual, tglakhir))
                            Dim arlAnnualDiscount As ArrayList = New AnnualDiscountFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                            For Each item As AnnualDiscount In arlAnnualDiscount
                                If Not ((ValidFrom < item.ValidateDateFrom And ValidTo < item.ValidateDateFrom) Or (ValidFrom > item.ValidateDateTo And ValidTo > item.ValidateDateTo)) Then
                                    If strError = String.Empty Then
                                        strError = "Periode Tidak Valid"
                                    Else
                                        strError = strError & ";Periode Tidak Valid"
                                    End If
                                    Exit For
                                End If
                            Next
                            _annualDiscount.ErrorMessage = strError
                            strError = String.Empty
                            DataCollection.Add(_annualDiscount)  '-- Add object to collection

                        End While




                    End If

                End Using


            Catch ex As Exception
                '    Dim str As String = ex.Message
                '    Return Nothing
                Throw ex
            Finally
                'If objConn.State = ConnectionState.Open Then
                '    objConn.Close()  '-- Close connection if already open
                'End If
            End Try

            Return DataCollection  '-- Return list of sparepart PO details
        End Function

#Region "Old ParsingExcelNoTransactionWithValidDate"

        'Protected Overrides Function ParsingExcelNoTransactionWithValidDate(ByVal fileName As String, ByVal sheetName As String, ByVal user As String, ByVal ValidFrom As DateTime, ByVal ValidTo As DateTime) As Object
        '    DataCollection = New ArrayList  '-- List of sparepart PO details
        '    Dim objConn As OdbcConnection   '-- Connection object

        '    Try
        '        Dim strConn As String = StrConnection & fileName  '-- Connection string
        '        objConn = New OdbcConnection(strConn)
        '        objConn.Open()  '-- Open connection

        '        Dim objCmd = New OdbcCommand("SELECT * FROM " & sheetName, objConn)  '-- Command object
        '        objCmd.CommandType = CommandType.Text
        '        objReader = objCmd.ExecuteReader()  '-- Read data
        '        Dim strError As String = String.Empty
        '        While objReader.Read()
        '            Dim _annualDiscount As New AnnualDiscount
        '            _annualDiscount.PartNo = objReader(0)
        '            Dim objSparepartMaster As SparePartMaster = New SparePartMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(CStr(objReader(0)))
        '            If objSparepartMaster.ID <> 0 Then
        '                _annualDiscount.PartName = objSparepartMaster.PartName
        '                _annualDiscount.Model = objSparepartMaster.ModelCode
        '            Else
        '                If strError = String.Empty Then
        '                    strError = "SparePart Tidak Ditemukan"
        '                Else
        '                    strError = strError & ";SparePart Tidak Ditemukan"
        '                End If
        '            End If
        '            _annualDiscount.MinimumQty = objReader(3)
        '            If CInt(objReader(3)) <= 0 Then
        '                If strError = String.Empty Then
        '                    strError = "MinimumQty harus > 0"
        '                Else
        '                    strError = strError & ";MinimumQty harus > 0"
        '                End If
        '            End If
        '            _annualDiscount.Point = objReader(4)
        '            If CInt(objReader(4)) < 0 Then
        '                If strError = String.Empty Then
        '                    strError = "Point harus > 0"
        '                Else
        '                    strError = strError & ";Point harus > 0"
        '                End If
        '            End If
        '            _annualDiscount.ValidateDateFrom = ValidFrom
        '            _annualDiscount.ValidateDateTo = ValidTo
        '            Dim tglawal As New DateTime(ValidFrom.Year, 1, 1, 0, 0, 0)
        '            Dim tglakhir As New DateTime(ValidFrom.Year, 12, 31, 23, 59, 59)
        '            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.AnnualDiscount), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDiscount), "ValidateDateFrom", MatchType.GreaterOrEqual, tglawal))
        '            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.AnnualDiscount), "ValidateDateFrom", MatchType.LesserOrEqual, tglakhir))
        '            Dim arlAnnualDiscount As ArrayList = New AnnualDiscountFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
        '            For Each item As AnnualDiscount In arlAnnualDiscount
        '                If Not ((ValidFrom < item.ValidateDateFrom And ValidTo < item.ValidateDateFrom) Or (ValidFrom > item.ValidateDateTo And ValidTo > item.ValidateDateTo)) Then
        '                    If strError = String.Empty Then
        '                        strError = "Periode Tidak Valid"
        '                    Else
        '                        strError = strError & ";Periode Tidak Valid"
        '                    End If
        '                    Exit For
        '                End If
        '            Next
        '            _annualDiscount.ErrorMessage = strError
        '            strError = String.Empty
        '            DataCollection.Add(_annualDiscount)  '-- Add object to collection
        '        End While
        '    Catch ex As Exception
        '        '    Dim str As String = ex.Message
        '        '    Return Nothing
        '        Throw ex
        '    Finally
        '        If objConn.State = ConnectionState.Open Then
        '            objConn.Close()  '-- Close connection if already open
        '        End If
        '    End Try

        '    Return DataCollection  '-- Return list of sparepart PO details
        'End Function

#End Region

        Private Function ExistPart(ByVal partNumber As String, ByVal dataCollection As ArrayList) As Boolean
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