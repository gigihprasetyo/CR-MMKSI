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
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.PO
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain.Search
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
#End Region

Namespace KTB.DNet.Parser
    Public Class ParkingFeeMemoDocParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _stream As StreamReader
        Private _grammar As Regex
        Private _ParkingFee As ParkingFee
        Private _fileName As String
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            _grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            Dim oCFac As ParkingFeeFacade = New ParkingFeeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim RemainFileName As String = ""
            Dim errMessage As String = String.Empty
            Dim finfo As New FileInfo(fileName)
            Dim fullName As String = finfo.FullName
            Dim DealerCode As String, DebitMemoNumber As String, DebitChargeNumber As String
            Dim cPF As CriteriaComposite
            Dim aPFs As ArrayList

            fileName = finfo.Name

            _ParkingFee = Nothing
            If fileName.Length > 4 Then
                If fileName.Substring(fileName.Length - 4).ToUpper = ".PDF" Then
                    If fileName.Substring(0, 6).ToLower = "debitm" Then
                        If fileName.Split("_").Length >= 2 Then
                            DebitMemoNumber = fileName.Split("_")(1)
                            DealerCode = fileName.Split("_")(2)
                            cPF = New CriteriaComposite(New Criteria(GetType(ParkingFee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            cPF.opAnd(New Criteria(GetType(ParkingFee), "Dealer.DealerCode", MatchType.Exact, DealerCode))
                            cPF.opAnd(New Criteria(GetType(ParkingFee), "DebitMemoNumber", MatchType.Exact, DebitMemoNumber))
                            aPFs = oCFac.Retrieve(cPF)

                            If aPFs.Count > 0 Then
                                _ParkingFee = CType(aPFs(0), ParkingFee)
                            Else
                                _ParkingFee = New ParkingFee
                            End If

                            If Not IsNothing(_ParkingFee) AndAlso _ParkingFee.ID > 0 Then
                                _ParkingFee.FileNameDebitMemo = fileName
                            End If
                        End If

                    ElseIf fileName.Substring(0, 6).ToLower = "debitc" Then
                        If fileName.Split("_").Length >= 2 Then
                            DebitChargeNumber = fileName.Split("_")(1)
                            DealerCode = fileName.Split("_")(2)
                            cPF = New CriteriaComposite(New Criteria(GetType(ParkingFee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            cPF.opAnd(New Criteria(GetType(ParkingFee), "Dealer.DealerCode", MatchType.Exact, DealerCode))
                            cPF.opAnd(New Criteria(GetType(ParkingFee), "DebitChargeNumber", MatchType.Exact, DebitChargeNumber))
                            aPFs = oCFac.Retrieve(cPF)

                            If aPFs.Count > 0 Then
                                _ParkingFee = CType(aPFs(0), ParkingFee)
                            Else
                                _ParkingFee = New ParkingFee
                            End If

                            If Not IsNothing(_ParkingFee) AndAlso _ParkingFee.ID > 0 Then
                                _ParkingFee.FileNameParkingFee = fileName
                            End If
                        End If
                    End If
                End If

            End If
            Return _ParkingFee
        End Function


        Protected Overrides Function DoTransaction() As Integer
            Dim Result As Integer = 0

            If Not IsNothing(_ParkingFee) AndAlso _ParkingFee.ID > 0 Then
                Dim oCFac As ParkingFeeFacade = New ParkingFeeFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                Result = oCFac.Update(_ParkingFee)
            End If
            Return Result
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function
    End Class
End Namespace

