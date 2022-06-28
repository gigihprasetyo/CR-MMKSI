#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Principal
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
#End Region

Namespace KTB.DNet.Parser
    Public Class MSPClaimDocumentParser
        Inherits AbstractParser

#Region "Private Variables"
        Private stream As StreamReader
        Private grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder

        Private arr As ArrayList
        Private _objMSPClaimDoc As MSPClaimDocument
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            Try
                _fileName = fileName
                Dim val As String
                arr = New ArrayList
                stream = New StreamReader(fileName, True)
                val = MyBase.NextLine(stream).Trim()
                While (Not val = String.Empty)
                    Try
                        _objMSPClaimDoc = ParseDocument(val + Delimited)
                        If Not _objMSPClaimDoc Is Nothing Then
                            arr.Add(_objMSPClaimDoc)
                        End If

                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "MSPClaimDocumentParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.MSPCLAIMDOCUMENT, BlockName)
                        Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _objMSPClaimDoc = Nothing
                    End Try
                    val = MyBase.NextLine(stream)
                End While
            Catch ex As Exception
                Throw ex
            Finally
                stream.Close()
                stream = Nothing
            End Try
            Return arr
        End Function

        Protected Overrides Function DoTransaction() As Integer
            For Each item As MSPClaimDocument In arr
                Try
                    Dim vResult As Integer
                    Dim _facMSPClaimDoc As MSPClaimDocumentFacade = New MSPClaimDocumentFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                    Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPClaimDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crt.opAnd(New Criteria(GetType(MSPClaimDocument), "MSPClaim.ID", MatchType.Exact, item.MSPClaim.ID))

                    Dim arrObjOld As ArrayList = _facMSPClaimDoc.Retrieve(crt)
                    If arrObjOld.Count > 0 Then
                        Dim objNew As MSPClaimDocument = CType(arrObjOld(0), MSPClaimDocument)
                        objNew.LetterNumber = item.LetterNumber
                        objNew.KuitansiNumber = item.KuitansiNumber
                        objNew.TotalAmount = item.TotalAmount
                        objNew.FileNameLetter = item.FileNameLetter
                        objNew.FileNameKuitansi = item.FileNameKuitansi

                        vResult = _facMSPClaimDoc.Update(objNew)
                    Else
                        vResult = _facMSPClaimDoc.Insert(item)
                    End If

                    Dim objMSPClaim As MSPClaim = item.MSPClaim
                    objMSPClaim.Status = EnumStatusMSP.Status.Selesai

                    Dim facMSPClaim As MSPClaimFacade = New MSPClaimFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                    If (facMSPClaim.Update(objMSPClaim) = -1) Then
                        Throw New Exception("Failed to Update MSPClaim with ID " & objMSPClaim.ID.ToString & ".")
                    Else
                        ' update PMHeader to selesai
                        If Not IsNothing(objMSPClaim.PMHeader) Then
                            Dim objPMHeader As PMHeader = objMSPClaim.PMHeader
                            objPMHeader.PMStatus = EnumPMStatus.PMStatus.Selesai

                            Dim facPMHeader As PMHeaderFacade = New PMHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                            If (facPMHeader.UpdatePMStatus(objPMHeader) = -1) Then
                                Throw New Exception("Failed to Update PMHeaer with ID " & objPMHeader.ID.ToString & ".")
                            End If
                        End If
                        
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "MSPClaimDocumentParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.MSPCLAIMDOCUMENT, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & item.KuitansiNumber & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next
            Return 1
        End Function

        Protected Function UpdateMSPRegHistoryStatus() As Boolean
            Dim bool As Boolean = False

            Return bool
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

        Private Function ParseDocument(ByVal ValParser As String) As MSPClaimDocument
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0
            errorMessage = New StringBuilder
            _objMSPClaimDoc = Nothing

            '100001180000000001;NoKuitansi;NoLetter;1250000
            'MSP Claim No;NoKuitansi;NoLetter;Total Amount
            Dim pathLtr As String = KTB.DNet.Lib.WebConfig.GetValue("MSP_CLAIM_LETTER") & "\SVCLTR_"
            Dim pathKui As String = KTB.DNet.Lib.WebConfig.GetValue("MSP_CLAIM_KUITANSI") & "\SVCKUI_"
            For Each m As Match In grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    
                    Case Is = 0 'MSP Claim Number
                        If sTemp.Length > 0 Then
                            Dim crt As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPClaim), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                            crt.opAnd(New Criteria(GetType(MSPClaim), "ClaimNumber", MatchType.Exact, sTemp.Trim))
                            crt.opAnd(New Criteria(GetType(MSPClaim), "Status", MatchType.InSet, String.Format("({0},{1})", CInt(EnumStatusMSP.Status.Proses), CInt(EnumStatusMSP.Status.Selesai))))
                            Dim arr As ArrayList = New MSPClaimFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(crt)

                            If arr.Count > 0 Then
                                _objMSPClaimDoc = New MSPClaimDocument
                                _objMSPClaimDoc.MSPClaim = CType(arr(0), MSPClaim)
                            Else
                                errorMessage.Append("Claim Number with status Proses is not registered in DNet." & Chr(13) & Chr(10))
                            End If
                        Else
                            errorMessage.Append("Claim Number is Empty." & Chr(13) & Chr(10))
                        End If
                    Case Is = 1 'NoKuitansi
                        If sTemp.Length > 0 Then
                            _objMSPClaimDoc.KuitansiNumber = sTemp.Trim
                            _objMSPClaimDoc.FileNameKuitansi = pathKui & sTemp.Trim & ".pdf"
                        Else
                            errorMessage.Append("No Kuitansi is Empty" & Chr(13) & Chr(10))
                        End If

                    Case Is = 2 'NoLetter
                        If sTemp.Length > 0 Then
                            _objMSPClaimDoc.LetterNumber = sTemp.Trim
                            _objMSPClaimDoc.FileNameLetter = pathLtr & sTemp.Trim & ".pdf"
                        Else
                            errorMessage.Append("No Letter is Empty" & Chr(13) & Chr(10))
                        End If

                    Case Is = 3 'totalAmount 
                        If sTemp.Length > 0 Then
                            _objMSPClaimDoc.TotalAmount = sTemp.Trim
                        Else
                            errorMessage.Append("Total Amount is Empty" & Chr(13) & Chr(10))
                        End If

                End Select
                sStart = m.Index + 1
                nCount += 1
            Next

            If errorMessage.Length > 0 Then
                _objMSPClaimDoc = Nothing
                Throw New Exception(errorMessage.ToString)
            End If
            Return _objMSPClaimDoc
        End Function

#End Region
    End Class
End Namespace

