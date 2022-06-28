#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text.RegularExpressions
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
    Public Class SparePartMasterParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _Stream As StreamReader
        Private _SparePartMasters As ArrayList
        Private Grammar As Regex
        Private mapper As IMapper
        Private objTransactionManager As TransactionManager
        Private _fileName As String
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            _Stream = New StreamReader(fileName, True)
            _SparePartMasters = New ArrayList
            _fileName = fileName
            Dim val As String = MyBase.NextLine(_Stream).Trim()
        
            While (Not val = "")
                Try
                    ParseSparePartMaster(val + ";")
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "SparePartMasterParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.SparepartMasterParser, BlockName)
                    Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & val & Chr(13) & Chr(10) & ex.Message)
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                End Try
                val = MyBase.NextLine(_Stream)
            End While
            _Stream.Close()
            _Stream = Nothing
            Return _SparePartMasters
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Try
                Me.mapper = MapperFactory.GetInstance().GetMapper(GetType(SparePartMaster).ToString)
                If _SparePartMasters.Count > 0 Then
                    For Each item As SparePartMaster In _SparePartMasters
                        Try
                            Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
                            Dim _ProductCategoryFacade As ProductCategoryFacade = New ProductCategoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                            Dim ProdCategory As ProductCategory = _ProductCategoryFacade.Retrieve(companyCode)
                            item.ProductCategory = ProdCategory

                            Me.objTransactionManager = New TransactionManager
                            Dim ICriterias As ICriteria
                            ICriterias = New CriteriaComposite(New Criteria(GetType(SparePartMaster), "PartNumber", item.PartNumber))
                            Dim al As ArrayList = mapper.RetrieveByCriteria(ICriterias)
                            If al.Count > 0 Then
                                Dim Sp As SparePartMaster = CType(al.Item(0), SparePartMaster)
                                Sp.PartName = item.PartName
                                Sp.AltPartName = item.AltPartName
                                Sp.AltPartNumber = item.AltPartNumber
                                Sp.ModelCode = item.ModelCode
                                Sp.PartCode = item.PartCode
                                Sp.PartStatus = item.PartStatus
                                Sp.RetalPrice = item.RetalPrice
                                Sp.Stock = item.Stock
                                Sp.TypeCode = item.TypeCode
                                Sp.SupplierCode = item.SupplierCode
                                Sp.IsWarranty = item.IsWarranty
                                objTransactionManager.AddUpdate(Sp, "WSM")
                            Else
                                objTransactionManager.AddInsert(item, "WSM")
                            End If
                            objTransactionManager.PerformTransaction()
                        Catch ex As Exception
                            SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "SparePartMaster", "SparePartMasterParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.SparepartMasterParser, BlockName)
                            Dim e As Exception = New Exception(_fileName & Chr(13) & Chr(10) & item.PartName & Chr(13) & Chr(10) & ex.Message)
                            Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                        End Try
                    Next
                End If
            Catch ex As Exception
                Throw ex
            End Try
            Return 0
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function

#End Region

#Region "Private Methods"

        Private Sub ParseSparePartMaster(ByVal ValParser As String)
            Dim _Sp As SparePartMaster = New SparePartMaster
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
                        _Sp.PartNumber = sTemp
                    Case Is = 1
                        _Sp.PartName = sTemp
                    Case Is = 2
                        _Sp.AltPartNumber = sTemp
                    Case Is = 3
                        _Sp.AltPartName = sTemp
                    Case Is = 4
                        _Sp.PartCode = sTemp
                    Case Is = 5
                        _Sp.ModelCode = sTemp
                    Case Is = 6
                        _Sp.Stock = CType(IIf(sTemp = "", "0", sTemp), Integer)
                    Case Is = 7
                        _Sp.RetalPrice = CType(IIf(sTemp = "", "0", sTemp), Decimal)
                    Case Is = 8
                        _Sp.PartStatus = sTemp
                    Case Is = 9
                        _Sp.TypeCode = sTemp
                    Case Is = 10
                        _Sp.SupplierCode = sTemp
                    Case Else
                End Select

                sStart = m.Index + 1
                nCount += 1
            Next

            _SparePartMasters.Add(_Sp)

        End Sub

#End Region

    End Class
End Namespace