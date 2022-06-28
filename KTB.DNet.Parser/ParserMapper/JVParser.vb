#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Security.Principal
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.DataMapper
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.Parser
    Public Class JVParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private _JVs As ArrayList
        Private _DepositAPencairanDs As ArrayList
        Private Grammar As Regex
        Private mapper As IMapper
        Private _fileName As String
        Private ErrorMessage As StringBuilder
        Private _initialStatusPencairanH As Integer = -1
        Private _initialStatusKuitansi As Integer = -1

        Private _DepositAPencairanH As New DepositAPencairanH
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
            _JVs = New ArrayList
            _DepositAPencairanDs = New ArrayList
            Dim val As String = MyBase.NextLine(_Stream).Trim()

            While (Not val = "")
                Try
                    ParseJV(val + ";")
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "JVParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.JVParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
                val = MyBase.NextLine(_Stream)
            End While
            _Stream.Close()
            _Stream = Nothing
            Return _JVs
        End Function

        Protected Overrides Function DoTransaction() As Integer
            'TO DO call facade to insert using transaction
            Dim _DepositKuitansiFacade As New DepositAKuitansiPencairanFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)))
            Dim _DepositAPencairanDFacade As New DepositAPencairanDFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)))
            Dim _FinalStatusKuitansiPencairan As Integer
            For Each item As DepositAKuitansiPencairan In _JVs
                Try
                    item.Status = EnumDepositA.StatusKuitansiDealer.Selesai ''12
                    _DepositKuitansiFacade.Update(item)
                    Dim nResultx As Integer = New JVCancelParser().InsertHistory(item.NoSurat, _initialStatusKuitansi, item.Status, 2)
                    UpdateDepositAPencairanH(item)
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "JVParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.JVParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & item.ReceiptNumber & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next

            'if status= cancel dari jv set status=11
            If _DepositAPencairanH.ID <> 0 Then
                If _DepositAPencairanH.Status = EnumDepositA.StatusPencairanDealer.Blok Then     '14 Then
                    _DepositAPencairanH.Status = EnumDepositA.StatusPencairanDealer.Selesai  '11  '11=selesai
                    Dim _DepositAPencairanHFacade As New DepositAPencairanHFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)))
                    _DepositAPencairanHFacade.Update(_DepositAPencairanH)

                    Dim nResult As Integer = New JVCancelParser().InsertHistory(_DepositAPencairanH.NoSurat, EnumDepositA.StatusPencairanDealer.Blok, _DepositAPencairanH.Status, 1)
                End If
            End If

            For Each item As DepositAPencairanD In _DepositAPencairanDs
                Try
                    _DepositAPencairanDFacade.Update(item)

                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "JVParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.JVParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & item.DepositAPencairanH.NoSurat & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
            Next

        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

        Private Sub ParseJV(ByVal ValParser As String)
            Dim objDepositAKuitansiPencairan As DepositAKuitansiPencairan = New DepositAKuitansiPencairan
            Dim strKodeDealer As String
            Dim strNoReg As String
            Dim strNoJV As String
            Dim decAmount As Decimal
            Dim strDesc As String
            Dim dtTgPencairan As Date
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0
            ErrorMessage = New StringBuilder
            For Each m As Match In Grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 0
                        If sTemp.Trim <> String.Empty Then
                            strKodeDealer = sTemp.Trim
                        Else
                            ErrorMessage.Append("Invalid Kode Dealer" & Chr(13) & Chr(10))
                        End If
                    Case Is = 1
                        If sTemp.Trim <> String.Empty Then
                            strNoReg = sTemp.Trim
                        Else
                            ErrorMessage.Append("Invalid NoReg" & Chr(13) & Chr(10))
                        End If
                    Case Is = 2
                        If sTemp.Trim <> String.Empty Then
                            strNoJV = sTemp.Trim
                        Else
                            ErrorMessage.Append("Invalid NoVJ" & Chr(13) & Chr(10))
                        End If
                    Case Is = 3
                        If sTemp.Trim <> String.Empty And IsNumeric(sTemp.Trim) Then
                            decAmount = sTemp.Trim
                        Else
                            ErrorMessage.Append("Invalid Amount" & Chr(13) & Chr(10))
                        End If
                    Case Is = 4
                        If sTemp.Trim <> String.Empty Then
                            strDesc = sTemp.Trim
                        Else
                            ErrorMessage.Append("Description Empty" & Chr(13) & Chr(10))
                        End If

                    Case Is = 5
                        If sTemp.Trim <> String.Empty Then
                            Try
                                dtTgPencairan = New Date(CInt(Right(sTemp, 4)), CInt(Mid(sTemp, 3, 2)), CInt(Left(sTemp, 2)))
                            Catch ex As Exception
                                ErrorMessage.Append("Tgl Pencairan Invalid" & Chr(13) & Chr(10))
                            End Try
                        Else
                            ErrorMessage.Append("Tgl Pencairan Empty" & Chr(13) & Chr(10))
                        End If

                    Case Else
                End Select

                sStart = m.Index + 1
                nCount += 1
            Next

            If ErrorMessage.Length > 0 Then
                Throw New Exception(ErrorMessage.ToString)
            End If

            If strNoJV <> String.Empty Then               
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanH), "NoReg", MatchType.Exact, strNoReg))
                Dim arrDepositPencairan As ArrayList = New DepositAPencairanHFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))).Retrieve(criterias)
                If arrDepositPencairan.Count > 0 Then
                    
                    _DepositAPencairanH = arrDepositPencairan(0)
                    objDepositAKuitansiPencairan = New DepositAKuitansiPencairan
                    objDepositAKuitansiPencairan = New DepositAKuitansiPencairanFacade((New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))).Retrieve(_DepositAPencairanH.NoReg)

                    _initialStatusKuitansi = objDepositAKuitansiPencairan.Status
                    'objDepositAKuitansiPencairan.Description = strDesc
                    objDepositAKuitansiPencairan.TglPencairan = dtTgPencairan
                    objDepositAKuitansiPencairan.TotalAmount = decAmount
                    If objDepositAKuitansiPencairan.ID > 0 Then
                        objDepositAKuitansiPencairan.NoJV = strNoJV
                        _JVs.Add(objDepositAKuitansiPencairan)
                    End If
                    If _DepositAPencairanH.DepositAPencairanDs.Count > 0 Then
                        For Each item As DepositAPencairanD In _DepositAPencairanH.DepositAPencairanDs
                            'item.Description = strDesc
                            item.Description = objDepositAKuitansiPencairan.Description
                            _DepositAPencairanDs.Add(item)
                        Next
                    End If
                End If
            End If
        End Sub

        Private Sub UpdateDepositAPencairanH(ByVal oDAKP As DepositAKuitansiPencairan)
            'Dim oDAKPFac As DepositAKuitansiPencairanFacade = New DepositAKuitansiPencairanFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim oDAPHFac As DepositAPencairanHFacade = New DepositAPencairanHFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim oDAPH As DepositAPencairanH
            Dim arlDAPH As New ArrayList
            Dim crtDAPH As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DepositAPencairanH), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'crtDAPH.opAnd(New Criteria(GetType(DepositAPencairanH), "NoSurat", MatchType.Exact, oDAKP.NoSurat))
            crtDAPH.opAnd(New Criteria(GetType(DepositAPencairanH), "NoReg", MatchType.Exact, oDAKP.NoReg))

            arlDAPH = oDAPHFac.Retrieve(crtDAPH)
            If arlDAPH.Count > 0 Then
                oDAPH = CType(arlDAPH(0), DepositAPencairanH)
                If oDAPH.Status = 11 Then '11=Setuju
                    oDAPH.Status = 16 '16=Selesai
                    oDAPHFac.Update(oDAPH)

                    Dim nResult As Integer = New JVCancelParser().InsertHistory(oDAPH.NoSurat, 11, oDAPH.Status, 1)
                End If
            End If
        End Sub
#End Region

    End Class
End Namespace