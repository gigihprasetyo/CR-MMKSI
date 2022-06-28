#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.DataMapper
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.PO
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports System.Security.Principal
#End Region

Namespace KTB.DNet.Parser

    Public Class CreditMasterSPParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private _creditMasterSPs As ArrayList
        Private Grammar As Regex
        Private _fileName As String
        Private strDealerlist As ArrayList
        Private _ErrorList As String = ""
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"
        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _fileName = fileName
            _Stream = New StreamReader(fileName, True)
            _creditMasterSPs = New ArrayList
            strDealerlist = New ArrayList
            Dim val As String = MyBase.NextLine(_Stream).Trim()
            While (Not val = "")
                Try
                    ParseCreditMasterSP(val + ";")
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "CreditMasterSPParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.CreditMasterSPParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
                val = MyBase.NextLine(_Stream)
            End While
            _Stream.Close()
            _Stream = Nothing
            Return _creditMasterSPs
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

        Private Function DeleteExistingCreditMasterSPs() As Integer
            Dim objCreditMasterSPFacade As KTB.DNet.BusinessFacade.PO.CreditMasterSPFacade = New CreditMasterSPFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim list As ArrayList = objCreditMasterSPFacade.RetrieveActiveList
            If list.Count > 0 Then
                Return objCreditMasterSPFacade.Delete(list)
            Else
                Return 1
            End If
        End Function

        Protected Overrides Function DoTransaction() As Integer
            If _creditMasterSPs.Count > 0 Then
                Dim objCMFac As CreditMasterSPFacade = New CreditMasterSPFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                Dim objCMExisting As CreditMasterSP
                Dim IsExisting As Boolean
                Dim IsSameWithOld As Boolean = True

                For Each objCM As CreditMasterSP In _creditMasterSPs
                    IsExisting = False

                    objCMExisting = objCMFac.Retrieve(objCM.CreditAccount.ToString)
                    If Not objCMExisting Is Nothing Then
                        If objCMExisting.ID > 0 Then
                            IsExisting = True
                        End If
                    End If
                    Try
                        If Not IsExisting Then
                            objCMFac.Insert(objCM)
                        Else
                            If objCMExisting.Ceiling <> objCM.Ceiling Then
                                objCMExisting.Ceiling = objCM.Ceiling
                                IsSameWithOld = False
                            End If
                            If objCMExisting.CeilingBalance <> objCM.CeilingBalance Then
                                objCMExisting.CeilingBalance = objCM.CeilingBalance
                                IsSameWithOld = False
                            End If
                            If Not IsSameWithOld Then
                                objCMFac.Update(objCMExisting)
                            End If
                        End If

                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "CreditMasterSP", "CreditMasterSPParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.CreditMasterSPParser, BlockName)
                        Dim e1 As Exception = New Exception(_fileName & Chr(13) & Chr(10) & objCM.CreditAccount & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e1, "Parser Policy")
                    End Try
                Next
            End If
            
        End Function

#End Region

#Region "Private Methods"


        Private Sub ParseCreditMasterSP(ByVal ValParser As String)
            Dim _creditMasterSP As CreditMasterSP = New CreditMasterSP
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0
            _creditMasterSP.ErrorMessage = ""
            For Each m As Match In Grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 0
                        If sTemp.Trim.Length > 0 Then
                            Dim objDealerFacade As DealerFacade = New DealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                            Dim objDealer As Dealer = objDealerFacade.Retrieve(sTemp)
                            If objDealer.ID > 0 Then
                                _creditMasterSP.CreditAccount = objDealer.CreditAccount
                            Else
                                _creditMasterSP.ErrorMessage = _creditMasterSP.ErrorMessage & " Credit Account tidak ditemukan."
                            End If
                        Else
                            _creditMasterSP.ErrorMessage = _creditMasterSP.ErrorMessage & " Credit Account tidak ditemukan."
                        End If
                    Case Is = 1
                        If sTemp.Length > 0 Then
                            If IsNumeric(sTemp) Then
                                _creditMasterSP.Ceiling = CLng(sTemp)
                            Else
                                _creditMasterSP.ErrorMessage = _creditMasterSP.ErrorMessage & "Ceiling tidak valid"
                            End If
                        End If
                    Case Is = 2
                        If sTemp.Length > 0 Then
                            If IsNumeric(sTemp) Then
                                _creditMasterSP.CeilingBalance = CLng(sTemp)
                            Else
                                _creditMasterSP.ErrorMessage = _creditMasterSP.ErrorMessage & "Ceiling Balance tidak valid"
                            End If
                        End If
                End Select
                sStart = m.Index + 1
                nCount += 1

            Next

            If _creditMasterSP.ErrorMessage.Length > 0 Then
                _ErrorList &= Chr(13) & Chr(10) & _creditMasterSP.ErrorMessage.ToString
            Else
                _creditMasterSPs.Add(_creditMasterSP)
            End If

        End Sub
#End Region

    End Class

End Namespace
