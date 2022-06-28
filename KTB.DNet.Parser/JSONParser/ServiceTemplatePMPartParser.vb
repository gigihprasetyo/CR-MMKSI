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

    Public Class ServiceTemplatePMPartParser
        Inherits AbstractParser

#Region "Private Variables"
        Private grammar As Regex
        'Private ServiceTemplatePMPartHeaders As ArrayList
        Private ServiceTemplatePMPartDetails As ArrayList
        Private _ServiceTemplatePMPartHeader As ServiceTemplatePMPartHeader
        Private _ServiceTemplatePMPartDetail As ServiceTemplatePMPartDetail
        Private jsonData As List(Of ServiceTemplatePMPartHeaderJson)
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
                jsonData = JsonConvert.DeserializeObject(Of List(Of ServiceTemplatePMPartHeaderJson))(Content)
            Catch ex As Exception
                SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "json-worker", "ServiceTemplatePMPartParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.ServiceTemplatePMPartParser, BlockName)
                Dim e As Exception = New Exception(KeyName & Chr(13) & Chr(10) & Chr(13) & Chr(10) & ex.Message)
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                jsonData = Nothing

                Dim err As String = String.Format("Parsing Error : {0}", ex.Message)
                msg = IIf(String.IsNullorEmpty(msg), err, String.Concat(msg, " | ", err))
            End Try

            Return jsonData
        End Function

        Protected Overrides Function DoTransaction(ByRef msg As String) As Integer
            Dim ServiceTemplatePMPartFacade As ServiceTemplatePMPartHeaderFacade
            Dim isSuccess As Integer = 1
            For Each data As ServiceTemplatePMPartHeaderJson In jsonData.Distinct()
                Try
                    ParseServiceTemplatePMPart(data, _ServiceTemplatePMPartHeader, ServiceTemplatePMPartDetails)
                    If Validate(_ServiceTemplatePMPartHeader, ServiceTemplatePMPartDetails, data, msg) Then
                        ServiceTemplatePMPartFacade = New ServiceTemplatePMPartHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing))
                        ServiceTemplatePMPartFacade.Insert(_ServiceTemplatePMPartHeader, ServiceTemplatePMPartDetails)
                    Else
                        isSuccess = 0
                    End If
                Catch ex As Exception
                    SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "WSJson-worker", "ServiceTemplatePMPartParser.vb", "Transaction", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.ServiceTemplatePMPartParser)
                    Dim e As Exception = New Exception("ServiceTemplatePMPartHeader" & Chr(13) & Chr(10) & ex.Message)
                    If ex.Message.Substring(0, 3) = "O/C" Then
                        Throw New System.Exception("O/C status in D-NET is not locked. Update failed.")
                    End If
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                    isSuccess = 0

                    Dim err As String = String.Format("Transaction ({0}, {1}) Error : {2}", data.Varian, data.PMCode, ex.Message)
                    msg = IIf(String.IsNullorEmpty(msg), err, String.Concat(msg, " | ", err))
                End Try
            Next
            Return isSuccess
        End Function

#Region "Private Methods"
        Private Function Validate(ByVal obj As ServiceTemplatePMPartHeader, ByVal objDets As ArrayList, ByVal data As ServiceTemplatePMPartHeaderJson, ByRef msg As String) As Boolean
            Dim err As String = String.Empty
            If IsNothing(obj.PMKind) Then
                err = String.Format("PMCode : '{0}' not found on D-Net.", data.PMCode)
            ElseIf String.IsNullorEmpty(obj.Varian) Then
                err = String.Format("Varian : '{0}' can not be empty.", data.Varian)
                'ElseIf IsNothing(obj.VechileType) Then
                '    err = String.Format("MaterialGroup : '{0}' not found on D-Net.", data.MaterialGroup)
            End If

            If String.IsNullorEmpty(err) Then
                Dim index As Integer = 0
                For Each objDet As ServiceTemplatePMPartDetail In objDets
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

        Private Sub ParseServiceTemplatePMPart(ByVal data As ServiceTemplatePMPartHeaderJson, ByRef header As ServiceTemplatePMPartHeader, ByRef details As ArrayList)
            Dim cultureInfo As CultureInfo = New CultureInfo("en-US")
            Dim arr As ArrayList
            Dim crit As CriteriaComposite
            Dim ServiceTemplatePMPartHeaderFacade As New ServiceTemplatePMPartHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing))
            Dim ServiceTemplatePMPartDetailFacade As New ServiceTemplatePMPartDetailFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing))
            Dim PMKindFacade As New PMKindFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing))
            Dim vechileTypeFacade As New VechileTypeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing))
            Dim sparePartMasterFacade As New SparePartMasterFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSJson"), Nothing))

            details = New ArrayList
            crit = New CriteriaComposite(New Criteria(GetType(ServiceTemplatePMPartHeader), "RowStatus", MatchType.Exact, 0))
            crit.opAnd(New Criteria(GetType(ServiceTemplatePMPartHeader), "PMKind.KindCode", MatchType.Exact, data.PMCode))
            crit.opAnd(New Criteria(GetType(ServiceTemplatePMPartHeader), "Varian", MatchType.Exact, data.Varian))

            arr = ServiceTemplatePMPartHeaderFacade.Retrieve(crit)
            If arr.Count = 0 Then
                header = New ServiceTemplatePMPartHeader
            Else
                header = CType(arr(0), ServiceTemplatePMPartHeader)
            End If

            header.PMKind = PMKindFacade.Retrieve(data.PMCode)
            header.Varian = data.Varian
            'header.VechileType = vechileTypeFacade.Retrieve(data.MaterialGroup)
            'header.ValidFrom = CDate(data.ValidFrom)

            For Each dataDet As ServiceTemplatePMPartDetailJson In data.Detail
                crit = New CriteriaComposite(New Criteria(GetType(ServiceTemplatePMPartDetail), "ServiceTemplatePMPartHeader.ID", MatchType.Exact, header.ID))
                crit.opAnd(New Criteria(GetType(ServiceTemplatePMPartDetail), "SparePartMaster.PartNumber", MatchType.Exact, dataDet.MaterialDetail))

                arr = ServiceTemplatePMPartDetailFacade.Retrieve(crit)
                If arr.Count = 0 Then
                    _ServiceTemplatePMPartDetail = New ServiceTemplatePMPartDetail
                Else
                    _ServiceTemplatePMPartDetail = CType(arr(0), ServiceTemplatePMPartDetail)
                End If

                _ServiceTemplatePMPartDetail.SparePartMaster = sparePartMasterFacade.Retrieve(dataDet.MaterialDetail)
                'If String.IsNullorEmpty(dataDet.NetAmount) Then
                '    _ServiceTemplatePMPartDetail.PartAmount = 0
                'Else
                '    _ServiceTemplatePMPartDetail.PartAmount = Convert.ToDecimal(dataDet.NetAmount, cultureInfo)
                'End If
                _ServiceTemplatePMPartDetail.PartQuantity = Convert.ToDecimal(dataDet.Quantity, cultureInfo)

                _ServiceTemplatePMPartDetail.RowStatus = CShort(DBRowStatus.Active)
                details.Add(_ServiceTemplatePMPartDetail)
            Next
        End Sub
#End Region

    End Class

End Namespace
