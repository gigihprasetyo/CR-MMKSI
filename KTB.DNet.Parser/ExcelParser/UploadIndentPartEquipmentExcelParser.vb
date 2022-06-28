#Region ".NET Base Class Namespace Imports"
'Imports System.Data.Odbc
Imports Excel
Imports System.Security.Principal

#End Region

#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.IndentPartEquipment
Imports KTB.DNet.BusinessFacade.SparePart
Imports System.IO

#End Region

Namespace KTB.DNet.Parser

    Public Class UploadIndentPartEquipmentExcelParser
        Inherits AbstractExcelParser

        ''' <summary>
        ''' New ParsingExcelNoTransaction
        ''' </summary>
        ''' <param name="fileName"></param>
        ''' <param name="sheetName"></param>
        ''' <param name="user"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Overrides Function ParsingExcelNoTransaction(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object
            DataCollection = New ArrayList  '-- List of sparepart PO details
            Dim parts() As String = fileName.Split(".".ToCharArray())
            Dim ext As String = parts(parts.Length - 1).Trim().ToLower()
            Dim objReader As IExcelDataReader = Nothing

            Dim PartNumber As String = String.Empty
            Dim OrderQuantity As Integer

            Try


                Using stream As FileStream = File.Open(fileName, FileMode.Open, FileAccess.Read)
                    If (ext = "xls") Then
                        objReader = ExcelReaderFactory.CreateBinaryReader(stream)
                    ElseIf (ext = "xlsx") Then
                        objReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
                    End If

                    If (Not IsNothing(objReader)) Then

                        While objReader.Read()

                            '-- In case of failed reading subsequent sparepart
                            '-- simply return already successfully read spareparts
                            Dim isError As Boolean = False
                            Try
                                PartNumber = objReader.GetString(0)  '-- PO detail's part number
                                PartNumber = PartNumber.Trim()  '-- Always trim spaces

                            Catch ex As Exception
                                'If objConn.State = ConnectionState.Open Then
                                'objConn.Close()  '-- Close connection if already open
                                'End If
                                'continue next loop if error
                                'note:cannot read PartNumber (objReader(0)) which have no string code, ex:1016, excel will convert it to numeric. solution add ' to front of 1016 = '1016 to convert to string.
                                isError = True
                                'Return DataCollection  '-- Return list of sparepart PO details
                            End Try

                            If (Not isError) Then
                                Dim _estimationEquipDetail As New EstimationEquipDetail  '-- PO detail
                                '-- Retrieve its sparepart master record if any
                                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                criterias.opAnd(New Criteria(GetType(SparePartMaster), "PartNumber", MatchType.Exact, PartNumber))
                                Dim partColl As ArrayList = New SparePartMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                                If partColl.Count = 0 Then
                                    _estimationEquipDetail.ErrorMessage &= "Nomor Barang Tidak Terdaftar"
                                ElseIf CType(partColl(0), SparePartMaster).TypeCode <> "E" Then
                                    _estimationEquipDetail.ErrorMessage &= "Nomor Barang Bukan Tipe Equipment"
                                Else
                                    _estimationEquipDetail.Harga = CType(partColl(0), SparePartMaster).RetalPrice
                                    _estimationEquipDetail.SparePartMaster = CType(partColl(0), SparePartMaster)
                                    If ExistPart(_estimationEquipDetail.SparePartMaster.PartNumber, DataCollection) Then
                                        _estimationEquipDetail.ErrorMessage &= "No.Part sudah ada;"
                                    End If
                                    If _estimationEquipDetail.SparePartMaster.ActiveStatus <> EnumSparePartActiveStatus.SparePartActiveStatus.Active Then
                                        _estimationEquipDetail.ErrorMessage = "No.Part tidak aktif"
                                    End If
                                End If

                                Try
                                    If objReader.GetInt32(1) < 1 Then
                                        _estimationEquipDetail.ErrorMessage &= "Quantity harus >= 1;"
                                    End If
                                    _estimationEquipDetail.EstimationUnit = objReader.GetInt32(1)  '-- Assign quantity
                                Catch ex As Exception
                                    _estimationEquipDetail.ErrorMessage &= "Data Tidak Lengkap"
                                End Try

                                DataCollection.Add(_estimationEquipDetail)  '-- Add object to collection
                            End If

                        End While
                         

                    End If

                End Using

             
            Catch ex As Exception
                Dim str As String = ex.Message
                Return Nothing

            Finally
           
            End Try

            Return DataCollection  '-- Return list of estimation equip details

        End Function

#Region "OLD ParsingExcelNoTransaction"

        'Protected Overrides Function ParsingExcelNoTransaction(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object
        '    DataCollection = New ArrayList  '-- List of sparepart PO details
        '    Dim objConn As OdbcConnection   '-- Connection object
        '    Dim PartNumber As String
        '    Dim OrderQuantity As Integer

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
        '            Dim isError As Boolean = False
        '            Try
        '                PartNumber = objReader(0)  '-- PO detail's part number
        '                PartNumber = PartNumber.Trim()  '-- Always trim spaces

        '            Catch ex As Exception
        '                'If objConn.State = ConnectionState.Open Then
        '                'objConn.Close()  '-- Close connection if already open
        '                'End If
        '                'continue next loop if error
        '                'note:cannot read PartNumber (objReader(0)) which have no string code, ex:1016, excel will convert it to numeric. solution add ' to front of 1016 = '1016 to convert to string.
        '                isError = True
        '                'Return DataCollection  '-- Return list of sparepart PO details
        '            End Try

        '            If (Not isError) Then
        '                Dim _estimationEquipDetail As New EstimationEquipDetail  '-- PO detail
        '                '-- Retrieve its sparepart master record if any
        '                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '                criterias.opAnd(New Criteria(GetType(SparePartMaster), "PartNumber", MatchType.Exact, PartNumber))
        '                Dim partColl As ArrayList = New SparePartMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
        '                If partColl.Count = 0 Then
        '                    _estimationEquipDetail.ErrorMessage &= "Nomor Barang Tidak Terdaftar"
        '                ElseIf CType(partColl(0), SparePartMaster).TypeCode <> "E" Then
        '                    _estimationEquipDetail.ErrorMessage &= "Nomor Barang Bukan Tipe Equipment"
        '                Else
        '                    _estimationEquipDetail.Harga = CType(partColl(0), SparePartMaster).RetalPrice
        '                    _estimationEquipDetail.SparePartMaster = CType(partColl(0), SparePartMaster)
        '                    If ExistPart(_estimationEquipDetail.SparePartMaster.PartNumber, DataCollection) Then
        '                        _estimationEquipDetail.ErrorMessage &= "No.Part sudah ada;"
        '                    End If
        '                    If _estimationEquipDetail.SparePartMaster.ActiveStatus <> EnumSparePartActiveStatus.SparePartActiveStatus.Active Then
        '                        _estimationEquipDetail.ErrorMessage = "No.Part tidak aktif"
        '                    End If
        '                End If

        '                Try
        '                    If objReader(1) < 1 Then
        '                        _estimationEquipDetail.ErrorMessage &= "Quantity harus >= 1;"
        '                    End If
        '                    _estimationEquipDetail.EstimationUnit = objReader(1)  '-- Assign quantity
        '                Catch ex As Exception
        '                    _estimationEquipDetail.ErrorMessage &= "Data Tidak Lengkap"
        '                End Try

        '                DataCollection.Add(_estimationEquipDetail)  '-- Add object to collection
        '            End If
        '        End While

        '    Catch ex As Exception
        '        Dim str As String = ex.Message
        '        Return Nothing

        '    Finally
        '        If objConn.State = ConnectionState.Open Then
        '            objConn.Close()  '-- Close connection if already open
        '        End If
        '    End Try

        '    Return DataCollection  '-- Return list of estimation equip details

        'End Function

#End Region
        Private Function ExistPart(ByVal partNumber As String, ByVal dataCollection As ArrayList) As Boolean
            For Each objPoDetail As EstimationEquipDetail In dataCollection
                If Not IsNothing(objPoDetail.SparePartMaster) Then
                    If objPoDetail.SparePartMaster.PartNumber.Trim.ToUpper = partNumber.Trim.ToUpper Then
                        Return True
                    End If
                End If
            Next
            Return False
        End Function

    End Class

End Namespace