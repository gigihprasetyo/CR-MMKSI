#Region ".NET Base Class Namespace Imports"
Imports Excel
Imports System.Security.Principal
Imports System.IO
#End Region

#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports System.Text
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.Utility
Imports System.Collections.Generic
Imports System.Linq

#End Region

Namespace KTB.DNet.Parser
    Public Class UploadCBUReturnExcelParser
        Inherits AbstractExcelParser


        Protected Overrides Function ParsingExcelNoTransaction(fileName As String, sheetName As String, DealerID As String) As Object
            DataCollection = New ArrayList
            Dim parts() As String = fileName.Split(".".ToCharArray())
            Dim ext As String = parts(parts.Length - 1).Trim().ToLower()
            Dim objReader As IExcelDataReader = Nothing
            Dim user As IPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
            Try

                Using stream As FileStream = File.Open(fileName, FileMode.Open, FileAccess.Read)
                    Dim Row As Integer = 1
                    If (ext = "xls") Then
                        objReader = ExcelReaderFactory.CreateBinaryReader(stream)
                    ElseIf (ext = "xlsx") Then
                        objReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
                    End If
                    Dim tab As Char = Chr(9)

                    If (Not IsNothing(objReader)) Then
                        While objReader.Read()
                            If Row >= 4 Then
                                Dim hashColl As New Hashtable
                                Try
                                    If Not IsNothing(objReader.GetString(4)) Then
                                        hashColl.Add("ReporterIssue", objReader.GetString(0))
                                        hashColl.Add("ChassisMasterLogisticCompany", objReader.GetString(1))
                                        hashColl.Add("DateOccur", objReader.GetString(2))
                                        hashColl.Add("PlaceOccur", objReader.GetString(3))
                                        hashColl.Add("ChassisNumber", objReader.GetString(4))
                                        hashColl.Add("DONumber", objReader.GetString(5))
                                        hashColl.Add("ClaimType", objReader.GetString(6))
                                        hashColl.Add("ClaimPoint", objReader.GetString(7))

                                        DataCollection.Add(hashColl)
                                    End If
                                Catch ex As Exception
                                    Return DataCollection
                                End Try
                            End If
                            Row = Row + 1
                        End While
                    End If
                End Using

            Catch ex As Exception
                Dim str As String = ex.Message
                Return Nothing
            End Try

            Return DataCollection
        End Function
    End Class
End Namespace
