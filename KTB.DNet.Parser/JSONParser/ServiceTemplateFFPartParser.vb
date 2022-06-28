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

    Public Class ServiceTemplateFFPartParser
        Inherits AbstractParser

#Region "Private Variables"
        Private grammar As Regex
        'Private ServiceTemplateFFPartHeaders As ArrayList
        Private ServiceTemplateFFPartDetails As ArrayList
        Private _ServiceTemplateFFPartHeader As ServiceTemplateFFPartHeader
        Private _ServiceTemplateFFPartDetail As ServiceTemplateFFPartDetail
        Private jsonData As List(Of ServiceTemplateFFPartHeaderJson)
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
                jsonData = JsonConvert.DeserializeObject(Of List(Of ServiceTemplateFFPartHeaderJson))(Content)
            Catch ex As Exception
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "json-worker", "ServiceTemplateFFPartParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.ServiceTemplateFFPartParser, BlockName)
                Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & Chr(13) & Chr(10) & ex.Message)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                jsonData = Nothing

                Dim err As String = String.Format("Parsing Error : {0}", ex.Message)
                msg = IIf(String.IsNullorEmpty(msg), err, String.Concat(msg, " | ", err))
            End Try

            Return jsonData
        End Function

        Protected Overrides Function DoTransaction(ByRef msg As String) As Integer
            Dim ServiceTemplateFFPartFacade As ServiceTemplateFFPartHeaderFacade
            Dim isSuccess As Integer = 1
            For Each data As ServiceTemplateFFPartHeaderJson In jsonData.Distinct()
                Try
                    ParseServiceTemplateFFPart(data, _ServiceTemplateFFPartHeader, ServiceTemplateFFPartDetails)
                    If Validate(_ServiceTemplateFFPartHeader, ServiceTemplateFFPartDetails, data, msg) Then
                        ServiceTemplateFFPartFacade = New ServiceTemplateFFPartHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing))
                        ServiceTemplateFFPartFacade.Insert(_ServiceTemplateFFPartHeader, ServiceTemplateFFPartDetails)
                    Else
                        isSuccess = 0
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "WSJson-worker", "ServiceTemplateFFPartParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.ServiceTemplateFFPartParser)
                    Dim e As Exception = New Exception("ServiceTemplateFFPartHeader" & Chr(13) & Chr(10) & ex.Message)
                    If ex.Message.Substring(0, 3) = "O/C" Then
                        Throw New System.Exception("O/C status in D-NET is not locked. Update failed.")
                    End If
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                    isSuccess = 0

                    Dim err As String = String.Format("Transaction ({0}, {1}) Error : {2}", data.Varian, data.RegNo, ex.Message)
                    msg = IIf(String.IsNullorEmpty(msg), err, String.Concat(msg, " | ", err))
                End Try
            Next
            Return isSuccess
        End Function

#Region "Private Methods"
        Private Function Validate(ByVal obj As ServiceTemplateFFPartHeader, ByVal objDets As ArrayList, ByVal data As ServiceTemplateFFPartHeaderJson, ByRef msg As String) As Boolean
            Dim err As String = String.Empty
            If IsNothing(obj.RecallCategory) Then
                err = String.Format("RegNo : '{0}' not found on D-Net.", data.RegNo)
            ElseIf String.IsNullorEmpty(obj.Varian) Then
                err = String.Format("Varian : '{0}' can not be empty.", data.Varian)
                'ElseIf IsNothing(obj.VechileType) Then
                '    err = String.Format("MaterialGroup : '{0}' not found on D-Net.", data.MaterialGroup)
            End If

            If String.IsNullorEmpty(err) Then
                Dim index As Integer = 0
                For Each objDet As ServiceTemplateFFPartDetail In objDets
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

        Private Sub ParseServiceTemplateFFPart(ByVal data As ServiceTemplateFFPartHeaderJson, ByRef header As ServiceTemplateFFPartHeader, ByRef details As ArrayList)
            Dim cultureInfo As CultureInfo = New CultureInfo("en-US")
            Dim arr As ArrayList
            Dim crit As CriteriaComposite
            Dim ServiceTemplateFFPartHeaderFacade As New ServiceTemplateFFPartHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing))
            Dim ServiceTemplateFFPartDetailFacade As New ServiceTemplateFFPartDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing))
            Dim RecallCategoryFacade As New RecallCategoryFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing))
            Dim vechileTypeFacade As New VechileTypeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing))
            Dim sparePartMasterFacade As New SparePartMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing))

            details = New ArrayList
            crit = New CriteriaComposite(New Criteria(GetType(ServiceTemplateFFPartHeader), "RowStatus", MatchType.Exact, 0))
            crit.opAnd(New Criteria(GetType(ServiceTemplateFFPartHeader), "RecallCategory.RecallRegNo", MatchType.Exact, data.RegNo))
            crit.opAnd(New Criteria(GetType(ServiceTemplateFFPartHeader), "Varian", MatchType.Exact, data.Varian))

            arr = ServiceTemplateFFPartHeaderFacade.Retrieve(crit)
            If arr.Count = 0 Then
                header = New ServiceTemplateFFPartHeader
            Else
                header = CType(arr(0), ServiceTemplateFFPartHeader)
            End If

            header.RecallCategory = RecallCategoryFacade.Retrieve(data.RegNo)
            header.Varian = data.Varian
            'header.VechileType = vechileTypeFacade.Retrieve(data.MaterialGroup)
            'header.ValidFrom = CDate(data.ValidFrom)

            For Each dataDet As ServiceTemplateFFPartDetailJson In data.Detail
                crit = New CriteriaComposite(New Criteria(GetType(ServiceTemplateFFPartDetail), "ServiceTemplateFFPartHeader.ID", MatchType.Exact, header.ID))
                crit.opAnd(New Criteria(GetType(ServiceTemplateFFPartDetail), "SparePartMaster.PartNumber", MatchType.Exact, dataDet.MaterialDetail))

                arr = ServiceTemplateFFPartDetailFacade.Retrieve(crit)
                If arr.Count = 0 Then
                    _ServiceTemplateFFPartDetail = New ServiceTemplateFFPartDetail
                Else
                    _ServiceTemplateFFPartDetail = CType(arr(0), ServiceTemplateFFPartDetail)
                End If

                _ServiceTemplateFFPartDetail.SparePartMaster = sparePartMasterFacade.Retrieve(dataDet.MaterialDetail)
                'If String.IsNullorEmpty(dataDet.NetAmount) Then
                '    _ServiceTemplateFFPartDetail.PartAmount = 0
                'Else
                '    _ServiceTemplateFFPartDetail.PartAmount = Convert.ToDecimal(dataDet.NetAmount, cultureInfo)
                'End If

                _ServiceTemplateFFPartDetail.PartQuantity = Convert.ToDecimal(dataDet.Quantity, cultureInfo)

                _ServiceTemplateFFPartDetail.RowStatus = CShort(DBRowStatus.Active)
                details.Add(_ServiceTemplateFFPartDetail)
            Next
        End Sub
#End Region

    End Class

End Namespace
