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
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.Parser

    Public Class SparePartBillingDeleteParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private Grammar As Regex
        Private _fileName As String
        Private errorMessage As StringBuilder

        Private _aBillingHs As ArrayList
        Private _oBilling As SparePartBilling
        Private _aBillingDocHs As ArrayList
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal KeyName As String, ByVal Content As String) As Object
            Try
                Dim lines As String() = MyBase.GetLines(Content)
                Dim ind As String
                Dim line As String

                _aBillingHs = New ArrayList()
                _oBilling = Nothing

                For i As Integer = 0 To lines.Length - 1
                    Try
                        line = lines(i)

                        ind = line.Split(MyBase.ColSeparator)(0)
                        If ind = MyBase.IndicatorHeader Then
                            If Not IsNothing(_oBilling) Then
                                _aBillingHs.Add(_oBilling)
                                _oBilling = Nothing
                            End If

                            errorMessage = New StringBuilder()
                            _oBilling = ParseHeader(line)

                        End If
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "ws-worker", "SparePartBillingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartBillingParser, BlockName)
                        Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        _oBilling = Nothing
                    End Try
                Next

                If Not IsNothing(_oBilling) Then
                    If Not IsNothing(errorMessage) AndAlso errorMessage.ToString() <> String.Empty Then _oBilling.ErrorMessage = errorMessage.ToString()
                    _aBillingHs.Add(_oBilling)
                End If

            Catch ex As Exception
                Throw ex
            Finally

            End Try

            Dim aDatas As New ArrayList
            Dim nError As Integer = 0
            Dim sMsg As String = ""

            For i As Integer = 0 To _aBillingHs.Count - 1
                _oBilling = CType(_aBillingHs(i), SparePartBilling)
                If Not IsNothing(_oBilling.ErrorMessage) AndAlso _oBilling.ErrorMessage.Trim() <> String.Empty Then
                    If nError = 0 Then
                        SysLogParameter.LogErrorToSyslog("Start : Log Error of SparePartBillingParser", "ws-worker", "SparePartBillingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartBillingParser, BlockName)
                    End If
                    sMsg = sMsg & _oBilling.ErrorMessage.Trim() & ";"
                    nError += 1
                    SysLogParameter.LogErrorToSyslog(_oBilling.ErrorMessage, "ws-worker", "SparePartBillingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartBillingParser, BlockName)
                    'Else
                    '    aDatas.Add(_oBilling)
                End If
                aDatas.Add(_oBilling)
            Next
            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Number of Error " & nError.ToString() & " of " & _aBillingHs.Count.ToString() & " Data", "ws-worker", "SparePartBillingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartBillingParser, BlockName)
                SysLogParameter.LogErrorToSyslog("End : Log Error of SparePartBillingParser", "ws-worker", "SparePartBillingParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.SparePartBillingParser, BlockName)

                Dim e As Exception = New Exception("Number of Error " & nError.ToString() & " of " & _aBillingHs.Count.ToString() & " Data. Message : " & sMsg)
                'Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                'Throw e
            End If
            _aBillingHs = New ArrayList()
            _aBillingHs = aDatas

            Return _aBillingHs
        End Function

        Protected Overrides Function DoRead(ByVal fileName As String, ByVal ValueString As String) As Object
            Return Nothing
        End Function

        Protected Overrides Function DoTransaction() As Integer

            Dim doFacade As SparePartBillingFacade
            Dim dcFacade As DocumentCancelFacade
            Dim EdocFacade As KTB.DNet.BusinessFacade.SparePartEDocumentFacade
            Dim nError As Integer = 0
            Dim sMsg As String = ""
            Dim fDocumenCancel As New DocumentCancelFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))

            For Each objSparePartBilling As SparePartBilling In _aBillingHs
                Try
                    If Not IsNothing(objSparePartBilling.ErrorMessage) AndAlso objSparePartBilling.ErrorMessage <> "" AndAlso Not objSparePartBilling.ErrorMessage.ToString().Contains("There is no Billing with number") Then
                        nError += 1
                        sMsg = sMsg & objSparePartBilling.ErrorMessage.ToString() & ";"
                    Else

                        If String.IsNullorEmpty(objSparePartBilling.ErrorMessage) OrElse objSparePartBilling.ErrorMessage.ToString().Contains("There is no Billing with number") Then
                            doFacade = New SparePartBillingFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                            doFacade.DeleteFromWebSevice(objSparePartBilling)
                        End If
                       

                        'Perubahan CR enhancement DSK 27092021
                        EdocFacade = New KTB.DNet.BusinessFacade.SparePartEDocumentFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartEDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(SparePartEDocument), "DocNumber", MatchType.Exact, objSparePartBilling.BillingNumber))
                        criterias.opAnd(New Criteria(GetType(SparePartEDocument), "DocType", MatchType.Exact, CShort(EnumSparepartEdoc.DocumentType.Faktur)))
                        Dim arrList As ArrayList = New ArrayList
                        arrList = New KTB.DNet.BusinessFacade.SparePartEDocumentFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(criterias)
                        For Each objSparePartEDocument As SparePartEDocument In arrList
                            objSparePartEDocument.RowStatus = CType(DBRowStatus.Deleted, Short)
                            EdocFacade.Delete(objSparePartEDocument)


                        Next

                        Dim objDocumentCancel As DocumentCancel = fDocumenCancel.Retrieve(objSparePartBilling.BillingNumber, EnumDocumentCancel.DocumentType.BillingPart)
                        If objDocumentCancel.DocNumber = "" Then

                            Dim objDocumentCancelins As New DocumentCancel
                            objDocumentCancelins.DocNumber = objSparePartBilling.BillingNumber
                            objDocumentCancelins.DocType = EnumDocumentCancel.DocumentType.BillingPart
                            dcFacade = New DocumentCancelFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                            dcFacade.InsertFromWebSevice(objDocumentCancelins)

                        End If
                        'End Perubahan CR enhancement DSK 27092021
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString(), "ws-worker", "SparePartBillingParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & objSparePartBilling.BillingNumber & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next

            If nError > 0 Then
                SysLogParameter.LogErrorToSyslog("Failed " & nError.ToString() & " of " & _aBillingHs.Count.ToString(), "ws-worker", "SparePartBillingParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                SysLogParameter.LogErrorToSyslog("Error Message :" & sMsg, "ws-worker", "SparePartBillingParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, WSMSyslogParameter.ParserType.PurchaseOrderParser, BlockName)
                Dim e As Exception = New Exception(sMsg)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                Throw e
            End If
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object
            Return Nothing
        End Function

#End Region

#Region "Private Methods"
        Private Sub writeError(str As String)
            errorMessage.Append(str & Chr(13) & Chr(10))
        End Sub

        Private Function ParseHeader(ByVal line As String) As SparePartBilling
            Dim cols As String() = line.Split(MyBase.ColSeparator)
            Dim user As GenericPrincipal = New System.Security.Principal.GenericPrincipal(New GenericIdentity("ws"), Nothing)
            Dim objSparePartBilling As New SparePartBilling
            Dim objSparePartBillingFac As New SparePartBillingFacade(user)


            errorMessage = New StringBuilder()
            If cols.Length <> 2 Then
                writeError("Invalid Header Format")
            Else
                '1 Billing Number
                If cols(1).Trim = String.Empty Then
                    writeError("Billing Number can't be empty")
                Else
                    Try
                        Dim BillingNumber As String = cols(1).Trim
                        objSparePartBilling = objSparePartBillingFac.Retrieve(BillingNumber)
                        If Not IsNothing(objSparePartBilling) AndAlso objSparePartBilling.ID > 0 Then
                            objSparePartBilling.RowStatus = CType(DBRowStatus.Deleted, Short)
                            objSparePartBilling.LastUpdateBy = "SAP"
                        Else
                            writeError("There is no Billing with number : " & BillingNumber)
                        End If
                    Catch ex As Exception
                        writeError("Delete Billing error: " & ex.Message)
                    End Try
                End If

                If Not IsNothing(errorMessage) AndAlso errorMessage.ToString().Trim() <> "" Then
                    If IsNothing(objSparePartBilling) Then objSparePartBilling = New SparePartBilling()
                    objSparePartBilling.ErrorMessage = errorMessage.ToString()
                    objSparePartBilling.BillingNumber = cols(1).Trim
                Else
                    objSparePartBilling.LastUpdateBy = "SAP"
                End If
            End If

            Return objSparePartBilling 
        End Function

#End Region

    End Class
End Namespace
