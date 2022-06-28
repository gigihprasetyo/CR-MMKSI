#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Text
Imports System.Collections
Imports System.Text.RegularExpressions
Imports System.Security.Principal
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.DataMapper
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.Parser
    Public Class ConditionMasterParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private _ConditionMasters As ArrayList
        Private Grammar As Regex
        Private mapper As IMapper
        Private objTransactionManager As TransactionManager
        Private _fileName As String
        Private sBuilder As StringBuilder
        Private progressDocumentType As Short = -1
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser(";")
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _Stream = New StreamReader(fileName, True)
            _ConditionMasters = New ArrayList
            _fileName = fileName
            Dim val As String = MyBase.NextLine(_Stream).Trim()
            Try
                While (Not val = "")
                    Try
                        ParseConditionMaster(val + ";")
                    Catch ex As Exception
                        SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "ConditionMasterParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.ConditionMasterParser, BlockName)
                        Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                        Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                    End Try
                    val = MyBase.NextLine(_Stream)
                End While
            Finally
                _Stream.Close()
                _Stream = Nothing
            End Try
            Return _ConditionMasters
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"


        Private Sub ParseConditionMaster(ByVal ValParser As String)
            Dim _ConditionMaster As V_LeasingDaftarKondisi = New V_LeasingDaftarKondisi
            Dim objFacadeLeasingFee As New LeasingFeeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0
            sBuilder = New StringBuilder
            For Each m As Match In Grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 0
                        If sTemp.ToUpper = EnumDocumentType.DocumentType.SPAF.ToString.ToUpper Then
                            _ConditionMaster.DocumentType = CType(EnumDocumentType.DocumentType.SPAF, Short)
                        ElseIf sTemp.ToUpper = EnumDocumentType.DocumentType.Subsidi.ToString.ToUpper Then
                            _ConditionMaster.DocumentType = CType(EnumDocumentType.DocumentType.Subsidi, Short)
                        Else
                            _ConditionMaster.DocumentType = -1
                            sBuilder.Append("Tipe Dokumen tidak valid. Tipe dokumen harus SPAF/Subsidi" & Chr(13) & Chr(10))
                        End If
                        If progressDocumentType = -1 Then
                            If _ConditionMaster.DocumentType <> -1 Then
                                progressDocumentType = _ConditionMaster.DocumentType
                            End If
                        End If
                        If progressDocumentType > -1 Then
                            If progressDocumentType <> _ConditionMaster.DocumentType Then
                                sBuilder.Append("Tipe dokumen tidak sama" & Chr(13) & Chr(10))
                            End If
                        End If
                    Case Is = 1
                        If sTemp.Length > 0 Then
                            Dim objVechileType As VechileType = New VechileTypeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)).Retrieve(sTemp.Trim)
                            If Not objVechileType Is Nothing And objVechileType.ID > 0 Then
                                _ConditionMaster.VechileType = objVechileType
                            Else
                                Dim objVechile As VechileType = New VechileType
                                _ConditionMaster.VechileTypeID = 0
                                _ConditionMaster.VechileType = objVechile
                                sBuilder.Append("Vechile Type tidak ditemukan" & Chr(13) & Chr(10))
                            End If
                        Else
                            sBuilder.Append("Vechile Type tidak valid" & Chr(13) & Chr(10))
                        End If
                    Case Is = 2
                        If sTemp.Length = 10 Then
                            Try
                                _ConditionMaster.ValidFrom = New DateTime(CInt(sTemp.Split("/")(2)), CInt(sTemp.Split("/")(1)), CInt(sTemp.Split("/")(0)))
                            Catch ex As Exception
                                sBuilder.Append("Tgl Berlaku tidak valid" & Chr(13) & Chr(10))
                            End Try
                        Else
                            sBuilder.Append("Tgl Berlaku tidak valid" & Chr(13) & Chr(10))
                        End If
                    Case Is = 3
                        If sTemp.Length > 0 Then
                            If IsNumeric(sTemp) Then
                                If CDec(sTemp) > 0 Then
                                    _ConditionMaster.RetailPrice = CDec(sTemp)
                                Else
                                    sBuilder.Append("Retail Price harus lebih besar dari 0" & Chr(13) & Chr(10))
                                End If
                            Else
                                sBuilder.Append("Retail Price harus angka" & Chr(13) & Chr(10))
                            End If
                        Else
                            _ConditionMaster.RetailPrice = 0
                        End If
                        Dim crits As New CriteriaComposite(New Criteria(GetType(LeasingFee), _
                            "RowStatus", CType(DBRowStatus.Active, Short)))
                        crits.opAnd(New Criteria(GetType(LeasingFee), "VechileType.VechileTypeCode", _
                            _ConditionMaster.VechileType.VechileTypeCode))
                        crits.opAnd(New Criteria(GetType(LeasingFee), "DateFrom", _
                            MatchType.LesserOrEqual, _ConditionMaster.ValidFrom))
                        crits.opAnd(New Criteria(GetType(LeasingFee), "DateTo", _
                            MatchType.GreaterOrEqual, _ConditionMaster.ValidFrom))
                        Dim objArr As ArrayList = objFacadeLeasingFee.Retrieve(crits)
                        If objArr.Count > 0 Then
                            _ConditionMaster.SPAF = DirectCast(objArr(0), LeasingFee).Fee
                        Else
                            _ConditionMaster.SPAF = 0
                            sBuilder.Append("Nilai leasing fee belum ada." & Chr(13) & Chr(10))
                        End If
                End Select
                sStart = m.Index + 1
                nCount += 1
            Next
            Dim pphf As PPhFacade = New PPhFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            If _ConditionMaster.RetailPrice > 0 AndAlso _ConditionMaster.SPAF > 0 Then
                _ConditionMaster.Subsidi = FormatCurrency(_ConditionMaster.RetailPrice * (_ConditionMaster.SPAF / 100), 2, , TriState.True)
                _ConditionMaster.PPh = _ConditionMaster.Subsidi * pphf.RetrievePPh() / 100
                _ConditionMaster.AfterPPh = _ConditionMaster.Subsidi - _ConditionMaster.PPh
                _ConditionMaster.PPn = _ConditionMaster.Subsidi * 0.1
            End If
            _ConditionMaster.ErrorMessage = sBuilder.ToString
            _ConditionMasters.Add(_ConditionMaster)
        End Sub
#End Region
    End Class
End Namespace


