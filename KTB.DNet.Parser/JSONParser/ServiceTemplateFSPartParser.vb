#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Principal
Imports Newtonsoft.Json
Imports System.Collections.Generic
Imports System.Linq
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Parser.Domain
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade.SparePart
Imports System.Globalization

#End Region

Namespace KTB.DNet.Parser

    Public Class ServiceTemplateFSPartParser
        Inherits AbstractParser

#Region "Private Variables"
        Private grammar As Regex
        'Private serviceTemplateFSPartHeaders As ArrayList
        Private serviceTemplateFSPartDetails As ArrayList
        Private _serviceTemplateFSPartHeader As ServiceTemplateFSPartHeader
        Private _serviceTemplateFSPartDetail As ServiceTemplateFSPartDetail
        Private jsonData As List(Of ServiceTemplateFSPartHeaderJson)
        Private errorMessage As StringBuilder
#End Region

#Region "Constructors/Destructors/Finalizers"
        Public Sub New()
            grammar = MyBase.GetGrammarParser()
        End Sub
#End Region

        Protected Overrides Function DoParse(ByVal KeyName As String, ByVal Content As String) As Object
            Throw New NotImplementedException()
        End Function

        Protected Overrides Function DoTransaction() As Integer
            Throw New NotImplementedException()
        End Function

        Protected Overrides Function DoParseFixFormatFile(fileName As String, user As String) As Object
            Throw New NotImplementedException()
        End Function

        Protected Overrides Function DoParse(ByVal KeyName As String, ByVal Content As String, ByRef msg As String) As Object
            Try
                msg = String.Empty
                jsonData = JsonConvert.DeserializeObject(Of List(Of ServiceTemplateFSPartHeaderJson))(Content)
            Catch ex As Exception
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "json-worker", "ServiceTemplateFSPartParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.ServiceTemplateFSPartParser, BlockName)
                Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & Chr(13) & Chr(10) & ex.Message)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                jsonData = Nothing

                Dim err As String = String.Format("Parsing Error : {0}", ex.Message)
                msg = IIf(String.IsNullorEmpty(msg), err, String.Concat(msg, " | ", err))
            End Try

            Return jsonData
        End Function

        Protected Overrides Function DoTransaction(ByRef msg As String) As Integer
            Dim serviceTemplateFSPartFacade As ServiceTemplateFSPartHeaderFacade
            Dim isSuccess As Integer = 1
            For Each data As ServiceTemplateFSPartHeaderJson In jsonData.Distinct()
                Try
                    ParseServiceTemplateFSPart(data, _serviceTemplateFSPartHeader, serviceTemplateFSPartDetails)
                    If Validate(_serviceTemplateFSPartHeader, serviceTemplateFSPartDetails, data, msg) Then
                        serviceTemplateFSPartFacade = New ServiceTemplateFSPartHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing))
                        serviceTemplateFSPartFacade.Insert(_serviceTemplateFSPartHeader, serviceTemplateFSPartDetails)
                    Else
                        isSuccess = 0
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "WSJson-worker", "ServiceTemplateFSPartParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.ServiceTemplateFSPartParser)
                    Dim e As Exception = New Exception("ServiceTemplateFSPartHeader" & Chr(13) & Chr(10) & ex.Message)
                    If ex.Message.Substring(0, 3) = "O/C" Then
                        Throw New System.Exception("O/C status in D-NET is not locked. Update failed.")
                    End If
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                    isSuccess = 0

                    Dim err As String = String.Format("Transaction ({0}, {1}) Error : {2}", data.KindofService, data.MaterialGroup, ex.Message)
                    msg = IIf(String.IsNullorEmpty(msg), err, String.Concat(msg, " | ", err))
                End Try
            Next
            Return isSuccess
        End Function

#Region "Private Methods"
        Private Function Validate(ByVal obj As ServiceTemplateFSPartHeader, ByVal objDets As ArrayList, ByVal data As ServiceTemplateFSPartHeaderJson, ByRef msg As String) As Boolean
            Dim err As String = String.Empty
            If IsNothing(obj.FSKind) Then
                err = String.Format("KindOfService : '{0}' not found on D-Net.", data.KindofService)
            ElseIf IsNothing(obj.VechileType) Then
                err = String.Format("MaterialGroup : '{0}' not found on D-Net.", data.MaterialGroup)
            End If

            If String.IsNullorEmpty(err) Then
                Dim index As Integer = 0
                For Each objDet As ServiceTemplateFSPartDetail In objDets
                    If IsNothing(objDet.SparePartMaster) Then
                        err = String.Format("MaterialDetail : '{0}' not found on D-Net.", data.Detail(index).MaterialDetail)
                        Exit For
                        'ElseIf objDet.PartAmount = 0 Then
                        '    err = String.Format("NetAmount : '{0}' must greater than 0.", data.Detail(index).NetAmount)
                        '    Exit For
                    ElseIf objDet.PartQuantity = 0 Then
                        err = String.Format("Quantity : '{0}' must greater than 0.", data.Detail(index).Quantity)
                        Exit For
                    End If
                    index = index + 1
                Next
            End If

            If Not String.IsNullorEmpty(err) Then
                SysLogParameter.LogErrorToSyslog(err, "WSJson-worker", "ServiceTemplateFSLaborParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.ServiceTemplateFSLaborParser)
                msg = IIf(String.IsNullorEmpty(msg), err, String.Concat(msg, " | ", err))
            End If

            Return String.IsNullorEmpty(err)
        End Function

        Private Sub ParseServiceTemplateFSPart(ByVal data As ServiceTemplateFSPartHeaderJson, ByRef header As ServiceTemplateFSPartHeader, ByRef details As ArrayList)
            Dim cultureInfo As CultureInfo = New CultureInfo("en-US")
            Dim arr As ArrayList
            Dim crit As CriteriaComposite
            Dim serviceTemplateFSPartHeaderFacade As New ServiceTemplateFSPartHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing))
            Dim serviceTemplateFSPartDetailFacade As New ServiceTemplateFSPartDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing))
            Dim fsKindFacade As New FSKindFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing))
            Dim vechileTypeFacade As New VechileTypeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing))
            Dim sparePartMasterFacade As New SparePartMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing))

            details = New ArrayList
            crit = New CriteriaComposite(New Criteria(GetType(ServiceTemplateFSPartHeader), "RowStatus", MatchType.Exact, 0))
            crit.opAnd(New Criteria(GetType(ServiceTemplateFSPartHeader), "FSKind.KindCode", MatchType.Exact, data.KindofService))
            crit.opAnd(New Criteria(GetType(ServiceTemplateFSPartHeader), "VechileType.VechileTypeCode", MatchType.Exact, data.MaterialGroup))
            crit.opAnd(New Criteria(GetType(ServiceTemplateFSPartHeader), "ValidFrom", MatchType.Exact, CDate(data.ValidFrom)))

            arr = serviceTemplateFSPartHeaderFacade.Retrieve(crit)
            If arr.Count = 0 Then
                header = New ServiceTemplateFSPartHeader
            Else
                header = CType(arr(0), ServiceTemplateFSPartHeader)
            End If

            header.FSKind = fsKindFacade.Retrieve(data.KindofService)
            header.VechileType = vechileTypeFacade.Retrieve(data.MaterialGroup)
            header.ValidFrom = CDate(data.ValidFrom)

            For Each dataDet As ServiceTemplateFSPartDetailJson In data.Detail
                crit = New CriteriaComposite(New Criteria(GetType(ServiceTemplateFSPartDetail), "ServiceTemplateFSPartHeader.ID", MatchType.Exact, header.ID))
                crit.opAnd(New Criteria(GetType(ServiceTemplateFSPartDetail), "SparePartMaster.PartNumber", MatchType.Exact, dataDet.MaterialDetail))

                arr = serviceTemplateFSPartDetailFacade.Retrieve(crit)
                If arr.Count = 0 Then
                    _serviceTemplateFSPartDetail = New ServiceTemplateFSPartDetail
                Else
                    _serviceTemplateFSPartDetail = CType(arr(0), ServiceTemplateFSPartDetail)
                End If

                _serviceTemplateFSPartDetail.SparePartMaster = sparePartMasterFacade.Retrieve(dataDet.MaterialDetail)
                If String.IsNullorEmpty(dataDet.NetAmount) Then
                    _serviceTemplateFSPartDetail.PartAmount = 0
                Else
                    _serviceTemplateFSPartDetail.PartAmount = Convert.ToDecimal(dataDet.NetAmount, cultureInfo)
                End If
                _serviceTemplateFSPartDetail.PartQuantity = Convert.ToDecimal(dataDet.Quantity, cultureInfo)

                _serviceTemplateFSPartDetail.RowStatus = CShort(DBRowStatus.Active)
                details.Add(_serviceTemplateFSPartDetail)
            Next
        End Sub
#End Region

    End Class

End Namespace
