#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Principal
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNET.BusinessFacade.Service
#End Region

Namespace KTB.DNet.Parser
    Public Class DepositBKewajibanHeaderParser
        Inherits AbstractParser

#Region "Private Variables"
        Private DepositBKewajibanHeaders As ArrayList
        Private DepositBKewajibanDetails As ArrayList
        Private _DepositBKewajibanHeader As DepositBKewajibanHeader
        Private _DepositBKewajibanDetail As DepositBKewajibanDetail

        Private stream As StreamReader
        Private grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder
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
                DepositBKewajibanHeaders = New ArrayList
                stream = New StreamReader(fileName, True)
                val = MyBase.NextLine(stream).Trim()
                While (Not val = "")
                    Try
                        If val.Substring(0, 1).ToUpper.Equals("H") Then
                            If Not _DepositBKewajibanHeader Is Nothing Then
                                DepositBKewajibanHeaders.Add(_DepositBKewajibanHeader)  'customer header input text
                            End If
                            _DepositBKewajibanHeader = ParseDepositBKewajibanHeader(val + Delimited)
                        Else
                            If Not _DepositBKewajibanHeader Is Nothing Then
                                ParseDepositBKewajibanDetail(val + Delimited, _DepositBKewajibanHeader)  'Order detail input
                            End If
                        End If

                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DepositBKewajibanParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.ContractParser, BlockName)
                        Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _DepositBKewajibanHeader = Nothing
                    End Try

                    val = MyBase.NextLine(stream)

                End While

                If Not _DepositBKewajibanHeader Is Nothing Then
                    DepositBKewajibanHeaders.Add(_DepositBKewajibanHeader)
                End If
            Catch ex As Exception
                Throw ex
            Finally
                stream.Close()
                stream = Nothing
            End Try
            Return DepositBKewajibanHeaders

        End Function

        Protected Overrides Function DoTransaction() As Integer
            Dim iReturn As Integer = 0
            Dim _objDepositBKewajibanHeader As New DepositBKewajibanHeader
            Try

                For Each item As DepositBKewajibanHeader In DepositBKewajibanHeaders
                    _objDepositBKewajibanHeader = IsHeaderExist(item)
                    If Not IsNothing(_objDepositBKewajibanHeader) AndAlso (_objDepositBKewajibanHeader.ID > 0) Then
                        _objDepositBKewajibanHeader.ProductCategory = item.ProductCategory
                        _objDepositBKewajibanHeader.NoSalesorder = item.NoSalesorder
                        For Each _detail As DepositBKewajibanDetail In item.DepositBKewajibanDetails
                            _detail.DepositBKewajibanHeader = _objDepositBKewajibanHeader
                            Dim oldDetail As DepositBKewajibanDetail = IsDetailExist(_detail)
                            If Not IsNothing(oldDetail) Then
                                oldDetail.Qty = _detail.Qty
                                oldDetail.Harga = _detail.Harga
                                oldDetail.Tax = _detail.Tax
                                oldDetail.DepositBKewajibanHeader = _objDepositBKewajibanHeader
                                _objDepositBKewajibanHeader.DepositBKewajibanDetails.Add(oldDetail)
                            End If
                        Next

                        iReturn = New DepositBKewajibanHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).UpdateTransaction(_objDepositBKewajibanHeader, _objDepositBKewajibanHeader.DepositBKewajibanDetails)
                        'Else
                        'iReturn = New DepositBKewajibanHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).InsertTransaction(item, item.DepositBKewajibanDetails)
                    End If
                Next
            Catch ex As Exception
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "DepositBKewajibanParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.DepositBKewajibanPHeaderParser)
                Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & " NoRegKewajiban :" & _objDepositBKewajibanHeader.NoRegKewajiban & Chr(13) & Chr(10) & ex.Message)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
            End Try
            Return iReturn
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
            Return -1
        End Function


        Private Function IsHeaderExist(ByVal objDepositBKewajibanHeader As DepositBKewajibanHeader) As DepositBKewajibanHeader

            Dim criterias As New CriteriaComposite(New Criteria(GetType(DepositBKewajibanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criterias.opAnd(New Criteria(GetType(DepositBKewajibanHeader), "Dealer.ID", MatchType.Exact, objDepositBKewajibanHeader.Dealer.ID))
            criterias.opAnd(New Criteria(GetType(DepositBKewajibanHeader), "NoRegKewajiban", MatchType.Exact, objDepositBKewajibanHeader.NoRegKewajiban))
            'criterias.opAnd(New Criteria(GetType(DepositBKewajibanHeader), "TipeKewajiban", MatchType.Exact, objDepositBKewajibanHeader.TipeKewajiban))
            'criterias.opAnd(New Criteria(GetType(DepositBKewajibanHeader), "PeriodYear", MatchType.Exact, objDepositBKewajibanHeader.PeriodYear))
            'criterias.opAnd(New Criteria(GetType(DepositBKewajibanHeader), "NoSalesorder", MatchType.Exact, objDepositBKewajibanHeader.NoSalesorder))


            Dim objDepositBKewajibanHeaderList As ArrayList = New DepositBKewajibanHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
            If objDepositBKewajibanHeaderList.Count > 0 Then
                Return objDepositBKewajibanHeaderList.Item(0)
            Else
                Return Nothing
            End If
        End Function

        Private Function IsDetailExist(ByVal objDepositBKewajibanDetail As DepositBKewajibanDetail) As DepositBKewajibanDetail
            Dim criterias As New CriteriaComposite(New Criteria(GetType(DepositBKewajibanDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DepositBKewajibanDetail), "DepositBKewajibanHeader.ID", MatchType.Exact, objDepositBKewajibanDetail.DepositBKewajibanHeader.ID))
            criterias.opAnd(New Criteria(GetType(DepositBKewajibanDetail), "EquipmentMaster.ID", MatchType.Exact, objDepositBKewajibanDetail.EquipmentMaster.ID))
            'criterias.opAnd(New Criteria(GetType(DepositBKewajibanDetail), "SparePartMaster.ID", MatchType.Exact, objDepositBKewajibanDetail.SparePartMaster.ID))


            Dim objDepositBKewajibanDetailList As ArrayList = New DepositBKewajibanDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
            If objDepositBKewajibanDetailList.Count > 0 Then
                Return objDepositBKewajibanDetailList(0)
            Else
                Return Nothing
            End If
        End Function

#End Region

#Region "Private Methods"

        Private Function ParseDepositBKewajibanHeader(ByVal ValParser As String) As DepositBKewajibanHeader
            _DepositBKewajibanHeader = New DepositBKewajibanHeader
            errorMessage = New StringBuilder
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String

            sStart = 0
            nCount = 0

            For Each m As Match In grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    ' H - No SO - Sales Org - No Reg

                    Case Is = 1
                        Try
                            _DepositBKewajibanHeader.NoSalesorder = sTemp.Trim
                        Catch ex As Exception
                            errorMessage.Append("Invalid SO Number" & Chr(13) & Chr(10))
                        End Try
                    Case Is = 2 'MMC / MFTBC
                        Try

                            If sTemp.Trim <> "" Then
                                Dim strCode As String
                                Select Case sTemp.Trim
                                    Case "S122"
                                        strCode = "MMC"
                                    Case "S121"
                                        strCode = "MFTBC"
                                End Select
                                Dim objProductCategory As ProductCategory = New ProductCategoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(strCode.Trim)
                                'Dim objProductCategory As ProductCategory = New ProductCategoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).RetrieveByName(sTemp.Trim)
                                If Not IsNothing(objProductCategory) Then
                                    _DepositBKewajibanHeader.ProductCategory = objProductCategory
                                Else
                                    errorMessage.Append("Invalid product category code" & Chr(13) & Chr(10))
                                End If
                            Else
                                errorMessage.Append("Product category is null" & Chr(13) & Chr(10))
                            End If

                        Catch ex As Exception
                            errorMessage.Append("Invalid Product Category" & Chr(13) & Chr(10))
                        End Try
                    Case Is = 3
                        Try
                            _DepositBKewajibanHeader.NoRegKewajiban = sTemp.Trim
                        Catch ex As Exception
                            errorMessage.Append("Invalid NoRegKewajiban" & Chr(13) & Chr(10))
                        End Try
                    Case Else
                        'do nothing
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next
            If errorMessage.Length > 0 Then
                Throw New Exception(errorMessage.ToString)
            End If

            Return _DepositBKewajibanHeader
        End Function

        Private Sub ParseDepositBKewajibanDetail(ByVal ValParser As String, ByVal _objDepositBKewajibanHeader As DepositBKewajibanHeader)
            _DepositBKewajibanDetail = New DepositBKewajibanDetail
            errorMessage = New StringBuilder

            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String

            'D;KodeDealer;Year;Month;KewajibanAmount;Netto
            sStart = 0
            nCount = 0

            For Each m As Match In grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)

                    'D - Material - Qty - Harga - Pajak
                    Case Is = 1
                        Try
                            'If _objDepositBKewajibanHeader.TipeKewajiban = DepositBEnum.TipeKewajiban.Regular Then 'EquipmentMaster
                            Dim objEquipmentMaster As EquipmentMaster = New KTB.DNet.BusinessFacade.EquipmentMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp.Trim)
                            If Not IsNothing(objEquipmentMaster) Then
                                _DepositBKewajibanDetail.EquipmentMaster = objEquipmentMaster
                            Else
                                errorMessage.Append("Invalid Equipment master code" & Chr(13) & Chr(10))
                            End If
                            'End If
                            'If _objDepositBKewajibanHeader.TipeKewajiban = DepositBEnum.TipeKewajiban.NonReguler Then 'SparepartMaster
                            '    Dim objSparePartMaster As SparePartMaster = New KTB.DNet.BusinessFacade.SparePart.SparePartMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp.Trim)
                            '    If Not IsNothing(objSparePartMaster) Then
                            '        _DepositBKewajibanDetail.SparePartMaster = objSparePartMaster
                            '    Else
                            '        errorMessage.Append("Invalid Sparepart master code" & Chr(13) & Chr(10))
                            '    End If

                            'End If
                        Catch ex As Exception
                            errorMessage.Append("Invalid Material" & Chr(13) & Chr(10))
                        End Try
                    Case Is = 2
                        Try
                            _DepositBKewajibanDetail.Qty = Short.Parse(sTemp.Trim)
                        Catch ex As Exception
                            errorMessage.Append("Invalid Qty" & Chr(13) & Chr(10))
                        End Try
                    Case Is = 3
                        Try
                            _DepositBKewajibanDetail.Harga = Decimal.Parse(IIf(sTemp.Trim = "", "0", sTemp.Trim))
                        Catch ex As Exception
                            errorMessage.Append("Invalid Harga." & Chr(13) & Chr(10))
                        End Try
                    Case Is = 4
                        Try
                            _DepositBKewajibanDetail.Tax = Decimal.Parse(IIf(sTemp.Trim = "", "0", sTemp.Trim))
                        Catch ex As Exception
                            errorMessage.Append("Invalid Tax." & Chr(13) & Chr(10))
                        End Try
                    Case Else
                        'Do Nothing else
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next
            If errorMessage.Length > 0 Then
                Throw New Exception(errorMessage.ToString)
            End If

            _objDepositBKewajibanHeader.DepositBKewajibanDetails.Add(_DepositBKewajibanDetail)

        End Sub


#End Region

    End Class

End Namespace