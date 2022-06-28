#Region "Summary"
'// ===========================================================================		
'// Author Name   : Heru
'// PURPOSE       : 
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 11/10/2005
'// ===========================================================================		
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Security.Principal
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.DataMapper
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.Parser
    Public Class AnnualDiscountAchievementParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private _AnnualDiscountAchievement As AnnualDiscountAchievement
        Private mapper As IMapper
        Private objTransactionManager As TransactionManager
        Private _fileName As String
        Private _AnnualDiscountAchievementHeader As AnnualDiscountAchievementHeader
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _fileName = fileName
            DoParseFixFormatFile(fileName, user)
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim nResult As Integer = 0
            If Not _AnnualDiscountAchievementHeader Is Nothing Then
                Dim _AnnualDiscountAchievementHeaderFacade As AnnualDiscountAchievementHeaderFacade = New AnnualDiscountAchievementHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                Try
                    _AnnualDiscountAchievementHeaderFacade.ShyncroniseAnnualDiscount(_AnnualDiscountAchievementHeader)
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "AnnualDiscountAchievementParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.AnnualDiscountAchievementParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & "Insert SP PO Status :" & _AnnualDiscountAchievementHeader.DealerCode & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                    nResult = -1
                End Try
            End If
            Return nResult
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
            Try
                _Stream = New StreamReader(fileName, True)
                Dim val As String = MyBase.NextLine(_Stream).Trim()
                Dim dealerCode As String
                Dim ValidFrom As DateTime
                Dim ValidTo As DateTime
                Dim valid As Boolean = True
                Dim IsHeader As Boolean = True
                Dim _dealer As Dealer
                Dim sStart As Integer
                Dim nCount As Integer
                Dim sTemp As String
                Dim _annualDiscount As AnnualDiscountAchievement
                While (Not val = "")
                    sStart = 0
                    nCount = 0
                    Try
                        If IsHeader Then
                            IsHeader = False
                            _AnnualDiscountAchievementHeader = New AnnualDiscountAchievementHeader
                            _dealer = isDealerExist(val.Substring(0, 6).Trim)
                            _AnnualDiscountAchievementHeader.DealerCode = _dealer.DealerCode
                            Dim _validFrom As String = val.Substring(7, 8).Trim
                            Dim _validTo As String = val.Substring(16, 8).Trim
                            ValidFrom = New Date(CInt(_validFrom.Substring(4, 4)), CInt(_validFrom.Substring(2, 2)), CInt(_validFrom.Substring(0, 2)), 0, 0, 0, 0)
                            ValidTo = New Date(CInt(_validTo.Substring(4, 4)), CInt(_validTo.Substring(2, 2)), CInt(_validTo.Substring(0, 2)), 0, 0, 0, 0)
                            _AnnualDiscountAchievementHeader.ValidateDateFrom = ValidFrom
                            _AnnualDiscountAchievementHeader.ValidateDateTo = ValidTo
                        Else
                            If (Not _AnnualDiscountAchievementHeader Is Nothing) And (valid) Then
                                _annualDiscount = ParseAnnualDiscountAchievement(val)
                            End If
                            If Not _annualDiscount Is Nothing Then
                                _AnnualDiscountAchievementHeader.AnnualDiscountAchievements.Add(_annualDiscount)
                            End If
                        End If
                    Catch ex As Exception
                        valid = False
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "AnnualDiscountAchievementParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.AnnualDiscountAchievementParser, BlockName)
                        Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                    End Try
                    val = MyBase.NextLine(_Stream).trim()
                End While
            Finally
                If Not _Stream Is Nothing Then
                    _Stream.Close()
                    _Stream = Nothing
                End If
            End Try
            Return _AnnualDiscountAchievementHeader
        End Function

#End Region

#Region "Private Methods"

        Private Function ParseAnnualDiscountAchievement(ByVal val As String) As AnnualDiscountAchievement
            Dim objADC As AnnualDiscountAchievement = New AnnualDiscountAchievement
            Try
                objADC.MaterialCode = val.Substring(0, 15).Trim
                objADC.MaterialDescription = val.Substring(15, 30).Trim
                objADC.Point = val.Substring(52, 8).Trim
                objADC.MinimumQty = val.Substring(60, 8).Trim
                objADC.BillQtyThisMonth = val.Substring(68, 8).Trim
                objADC.BillQtyThisPeriod = val.Substring(76, 8).Trim
                objADC.RebateQtyThisPeriod = val.Substring(84, 8).Trim
                objADC.RebateAmountThisPeriod = val.Substring(92, 15).Trim
                objADC.RemainQty = val.Substring(107, 8).Trim
                Return objADC
            Catch ex As Exception
                Throw ex
            End Try
            Return Nothing
        End Function

       
        Private Function isDealerExist(ByVal code As String) As Dealer
            Dim dealerFacade As dealerFacade = New dealerFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim _dealer As Dealer = dealerFacade.Retrieve(code)
            If _dealer.ID > 0 Then
                Return _dealer
            Else
                Throw New Exception("Dealer Tidak Ketemu.")
            End If
            Return Nothing
        End Function


#End Region


    End Class

End Namespace