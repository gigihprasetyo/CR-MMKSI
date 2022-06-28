#Region "Summary"
'// ===========================================================================		
'// Author Name   : Andra AR
'// PURPOSE       : 
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// 
'// ===========================================================================		
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.Data.Odbc
Imports System.Security.Principal
Imports System.Linq

#End Region

#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports Excel

#End Region

Namespace KTB.DNet.Parser

    Public Class DepositBUploadKewajibanParser
        Inherits AbstractExcelParser

        Public Sub New()
        End Sub


#Region "Protected Methods"

        '----------Format Data Excel yang akan diupload-------------'
        '   Part/Equipment Number - Qty     '
        '-----------------------------------------------------------'

        Protected Overrides Function ParsingExcelNoTransaction(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object
            DataCollection = New ArrayList  '-- List of KewajibanDetail
            Dim objConn As OdbcConnection   '-- Connection object
            Dim strData As String        '-- Part / Equipment number 

            Try
                Dim strConn As String = StrConnection & fileName  '-- Connection string
                objConn = New OdbcConnection(strConn)
                objConn.Open()  '-- Open connection

                Dim objCmd = New OdbcCommand("SELECT * FROM " & sheetName, objConn)  '-- Command object
                objCmd.CommandType = CommandType.Text
                objReader = objCmd.ExecuteReader()  '-- Read data

                While objReader.Read()
                    Try
                        strData = objReader(0)
                    Catch ex As Exception

                    End Try

                End While
            Catch ex As Exception

            End Try
        End Function

        Public Function GetSheet(ByVal filename As String) As ArrayList

            Dim arlData As ArrayList = New ArrayList
            Dim parts() As String = filename.Split(".".ToCharArray())
            Dim ext As String = parts(parts.Length - 1).Trim().ToLower()
            Dim objReader As IExcelDataReader = Nothing
            Dim errMsg As String = String.Empty

            Try

                Dim Ds As DataSet = ParseExcelDataSet(filename, "", "")

                If Ds Is Nothing Then
                    Return New ArrayList
                Else
                    Dim arrSheet As New ArrayList
                    For Each row As System.Data.DataTable In Ds.Tables
                        arrSheet.Add(row.TableName.ToString)
                    Next

                    Return arrSheet
                End If
            Catch ex As Exception
                errMsg = ex.Message
            End Try

            Return arlData

        End Function

        Public Function ParsingExcel(ByVal fileName As String, ByVal sheetName As String, ByVal user As String, ByVal iType As Integer) As Object
            DataCollection = New ArrayList  '-- List of Material Promotion 
            Dim objConn As OdbcConnection   '-- Connection object

            Dim Ds As DataSet = ParseExcelDataSet(fileName, sheetName, "")

            Dim i As Integer = 0
            For i = 0 To Ds.Tables(0).Rows.Count - 1
                Dim obj As DepositBKewajibanDetail = New DepositBKewajibanDetail
                Dim o As EquipmentMaster
                Dim ErrMsg As String = ""

                '--Reg		-->EquipmentMaster
                '--Non Reg	-->SparePartMaster
                Try
                    Dim code As String = CStr(Ds.Tables(0).Rows(i)(0)).Trim
                    If code.Length > 0 Then
                        Select Case iType
                            Case 1 'Reguler
                                Dim objEquipment As EquipmentMaster = New EquipmentMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(code)
                                If Not IsNothing(objEquipment) AndAlso objEquipment.ID > 0 Then
                                    obj.EquipmentMaster = objEquipment
                                    obj.Harga = objEquipment.Price
                                Else
                                    obj.EquipmentMaster = Nothing
                                    obj.Harga = 0
                                    obj.ErrorMessage = "Equipment number tidak ada"
                                End If
                                obj.SparePartMaster = Nothing
                            Case 2 'Non Reguler
                                Dim objSparePart As SparePartMaster = New KTB.DNet.BusinessFacade.SparePart.SparePartMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(code)
                                If Not IsNothing(objSparePart) AndAlso objSparePart.ID > 0 Then
                                    obj.SparePartMaster = objSparePart
                                    obj.Harga = objSparePart.RetalPrice
                                Else
                                    obj.SparePartMaster = Nothing
                                    obj.Harga = 0
                                    obj.ErrorMessage = "Part number tidak ada"
                                End If
                                obj.EquipmentMaster = Nothing

                        End Select
                    Else
                        Select Case iType
                            Case 1
                                obj.ErrorMessage = "Kode equipment kosong"
                            Case 2
                                obj.ErrorMessage = "Part Number kosong"
                        End Select
                    End If
                Catch ex As Exception
                    obj.ErrorMessage = ex.Message
                End Try

                If Not IsNothing(Ds.Tables(0).Rows(i)(1)) Then
                    Try
                        Dim qty As Decimal = CDec(Ds.Tables(0).Rows(i)(1))
                        obj.Qty = qty
                    Catch ex As Exception
                        obj.ErrorMessage = "Quantity salah"
                    End Try
                Else
                    obj.ErrorMessage = "Quantity kosong"
                End If

                DataCollection.Add(obj)
            Next

            Return DataCollection

        End Function

#End Region

    End Class

End Namespace


