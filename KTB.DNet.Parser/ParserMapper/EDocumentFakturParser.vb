#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Principal
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNET.Domain
Imports KTB.DNET.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNET.BusinessFacade
Imports KTB.DNET.BusinessFacade.SparePart

#End Region

Namespace KTB.DNet.Parser
    Public Class EDocumentFakturParser
        Inherits AbstractParser

#Region "Private Variables"
        Private stream As StreamReader
        Private grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder
        Private arrSparePartEDocument As ArrayList
        Private oSparePartEDocument As SparePartEDocument
        Private odoc As SparePartEDocument
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

        Protected Overrides Function DoParse(fileName As String, user As String) As Object
            Try
                Dim finfo As New FileInfo(fileName)
                Dim fullName As String = finfo.FullName
                fileName = finfo.Name
                oSparePartEDocument = Nothing
                If fileName.Length > 4 Then
                    If fileName.Substring(fileName.Length - 4).ToUpper = ".PDF" Then
                        Dim debug = ""
                        Dim DocNumber As String = fileName.Split("_")(1)
                        oSparePartEDocument = New SparePartEDocumentFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(DocNumber)
                        If getDocType(fileName.Split("_")(0)) = EnumSparepartEdoc.DocumentType.TTTDepositC2 Then
                            oSparePartEDocument.BillingNumber = fileName.Split("_")(2)
                        End If
                        oSparePartEDocument.DocType = getDocType(fileName.Split("_")(0))
                        oSparePartEDocument.DocNumber = DocNumber
                        oSparePartEDocument.FileName = fileName
                        oSparePartEDocument.Path = KTB.DNET.Lib.WebConfig.GetValue("SPEDocumentPath") & "\" & Date.Now.Year & "\" & fileName
                    End If
                End If
            Catch ex As Exception
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "EDocumentFakturParser.vb", "Parsing", KTB.DNET.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.EDocumentFakturParser, BlockName)
                Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & ex.Message)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                oSparePartEDocument = Nothing
            End Try
            Return oSparePartEDocument
        End Function

        Private Function getDocType(ByVal fileNamePrefix As String) As Short
            Select Case fileNamePrefix
                Case KTB.DNET.Lib.WebConfig.GetValue("EDOC_FAKTUR_PDF")
                    Return EnumSparepartEdoc.DocumentType.Faktur
                Case KTB.DNET.Lib.WebConfig.GetValue("EDOC_CREDITMEMORETUR_PDF")
                    Return EnumSparepartEdoc.DocumentType.CreditMemoRetur
                Case KTB.DNET.Lib.WebConfig.GetValue("EDOC_TTTDEPOSITC2_PDF")
                    Return EnumSparepartEdoc.DocumentType.TTTDepositC2
                Case KTB.DNET.Lib.WebConfig.GetValue("EDOC_PENALTIPENGEMBALIANBARANG_PDF")
                    Return EnumSparepartEdoc.DocumentType.PenaltiPengembalianBarang
                Case KTB.DNET.Lib.WebConfig.GetValue("EDOC_EOPACKLISTCASE_PDF")
                    Return EnumSparepartEdoc.DocumentType.EOPackListCase
                Case KTB.DNET.Lib.WebConfig.GetValue("EDOC_EOPACKLISTSUMMARY_PDF")
                    Return EnumSparepartEdoc.DocumentType.EOPackListSummary
                Case KTB.DNET.Lib.WebConfig.GetValue("EDOC_ROPACKLISTCASE_PDF")
                    Return EnumSparepartEdoc.DocumentType.ROPackListCase
                Case KTB.DNET.Lib.WebConfig.GetValue("EDOC_ROPACKLISTSUMMARY_PDF")
                    Return EnumSparepartEdoc.DocumentType.ROPackListSummary
                Case KTB.DNET.Lib.WebConfig.GetValue("EDOC_CREDITMEMORETURMANUAL_PDF")
                    Return EnumSparepartEdoc.DocumentType.CreditMemoReturManual
            End Select
        End Function

        Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object

        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
            Dim fSparePartEDocument As New SparePartEDocumentFacade(user)
            Dim fDocumenCancel As New DocumentCancelFacade(user)
            Try
                'Perubahan CR enhancement DSK 27092021
                Dim objDocumentCancel As DocumentCancel = fDocumenCancel.Retrieve(oSparePartEDocument.DocNumber, oSparePartEDocument.DocType)
                If objDocumentCancel.DocNumber = "" Then
                    'End Perubahan CR enhancement DSK 27092021
                    If Not IsNothing(oSparePartEDocument) Then
                        If oSparePartEDocument.ErrorMessage = String.Empty Then
                            Dim objSparePartEDocument As SparePartEDocument = fSparePartEDocument.Retrieve(oSparePartEDocument.DocNumber, oSparePartEDocument.DocType)
                            If objSparePartEDocument.ID = 0 Then
                                If fSparePartEDocument.Insert(oSparePartEDocument) < 0 Then
                                    nError += 1
                                End If
                            Else
                                oSparePartEDocument.ID = objSparePartEDocument.ID
                                If fSparePartEDocument.Update(oSparePartEDocument) < 0 Then
                                    nError += 1
                                End If
                            End If

                            If oSparePartEDocument.DocType = EnumSparepartEdoc.DocumentType.TTTDepositC2 Then
                                Dim fDepC2Line As New DepositC2LineFacade(user)
                                Dim oDepC2Line As DepositC2Line = fDepC2Line.RetrieveByBillingNumber(oSparePartEDocument.DocNumber)
                                If oDepC2Line.ID > 0 Then
                                    oDepC2Line.BillingNumber = oSparePartEDocument.BillingNumber
                                    If fDepC2Line.Update(oDepC2Line) < 0 Then
                                        nError += 1
                                    End If
                                End If
                            End If
                        Else
                            Throw New Exception(oSparePartEDocument.ErrorMessage)
                            nError += 1
                        End If
                    End If
                End If
            Catch ex As Exception
                sMsg &= ex.Message.ToString() & ";"
                nError += 1
            End Try


            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & oSparePartEDocument.DocNumber, "ws-worker", "EDocumentFakturParser.vb", "Transaction", KTB.DNET.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.EDocumentFakturParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "EDocumentFakturParser.vb", "Transaction", KTB.DNET.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.EDocumentFakturParser, BlockName)
                Dim e As Exception = New Exception(sMsg)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Throw e
            End If
            Return 0
        End Function
    End Class
End Namespace
