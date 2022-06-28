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
#End Region

Namespace KTB.DNet.Parser

    Public Class BasicProductParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private _BasicProducts As ArrayList
        Private Grammar As Regex
        Private mapper As IMapper
        Private objTransactionManager As TransactionManager
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _Stream = New StreamReader(fileName, True)
            _BasicProducts = New ArrayList
            Dim val As String = MyBase.NextLine(_Stream).Trim()
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String

            While (Not val = "")
                'sStart = 0
                'nCount = 0
                'For Each m As Match In Grammar.Matches(val)
                '    sTemp = val.Substring(sStart, m.Index - sStart)
                '    sTemp = sTemp.Trim("""")
                '    If (nCount = 0) Then
                '        ParseBasicProduct(val + ";")
                '    End If
                '    nCount += 1
                'Next
                Try
                    sStart = 0
                    nCount = 0
                    For Each m As Match In Grammar.Matches(val)
                        sTemp = val.Substring(sStart, m.Index - sStart)
                        sTemp = sTemp.Trim("""")
                        If (nCount = 0) Then
                            ParseBasicProduct(val + ";")
                        End If
                        nCount += 1
                    Next
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "BasicProductParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.BasicProductParser, BlockName)
                End Try
                val = MyBase.NextLine(_Stream)
            End While
            _Stream.Close()
            _Stream = Nothing
            Return _BasicProducts
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Try
                Me.mapper = MapperFactory.GetInstance().GetMapper(GetType(BasicProduct).ToString)
                Me.objTransactionManager = New TransactionManager
                If _BasicProducts.Count > 0 Then
                    For Each item As BasicProduct In _BasicProducts
                        'mapper.Insert(item, "WSM")
                        Dim ICriterias As ICriteria
                        ICriterias = New CriteriaComposite(New Criteria(GetType(BasicProduct), "BasicProductCode", item.BasicProductCode))
                        Dim al As ArrayList = mapper.RetrieveByCriteria(ICriterias)
                        If al.Count > 0 Then
                            Dim Bp As BasicProduct = CType(al.Item(0), BasicProduct)
                            Bp.BasicProductName = item.BasicProductName
                            objTransactionManager.AddUpdate(Bp, "WSM")
                        Else
                            objTransactionManager.AddInsert(item, "WSM")
                        End If
                    Next
                    objTransactionManager.PerformTransaction()
                End If
            Catch ex As Exception
                'Throw ex
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "BasicProductParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.BasicProductParser, BlockName)
            End Try
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

        Private Sub ParseBasicProduct(ByVal ValParser As String)
            Dim _Bp As BasicProduct = New BasicProduct
            Dim sStart As Integer
            Dim nCount As Integer
            Dim sTemp As String
            sStart = 0
            nCount = 0

            For Each m As Match In Grammar.Matches(ValParser)
                sTemp = ValParser.Substring(sStart, m.Index - sStart)
                sTemp = sTemp.Trim("""")
                sTemp = sTemp.Trim()
                Select Case (nCount)
                    Case Is = 0
                        _Bp.BasicProductCode = sTemp
                    Case Is = 1
                        _Bp.BasicProductName = sTemp
                    Case Is = 2
                        _Bp.LastUpdatedBy = sTemp

                    Case Else
                End Select

                sStart = m.Index + 1
                nCount += 1
            Next

            _BasicProducts.Add(_Bp)

        End Sub

#End Region

    End Class
End Namespace