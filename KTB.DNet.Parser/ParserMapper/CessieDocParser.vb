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
    Public Class CessieDocParser
        Inherits AbstractParser

#Region "Private Variables"
        Private _stream As StreamReader
        Private _grammar As Regex
        Private _Cessie As Cessie
        Private _fileName As String
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            _grammar = MyBase.GetGrammarParser()
        End Sub

#End Region

        Protected Overrides Function DoParse(ByVal fileName As String, ByVal user As String) As Object
            Dim CessieNumber As String = ""
            Dim oCFac As CessieFacade = New CessieFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
            Dim RemainFileName As String = ""
            Dim errMessage As String = String.Empty
            Dim finfo As New FileInfo(fileName)
            Dim fullName As String = finfo.FullName
            fileName = finfo.Name

            _Cessie = Nothing
            If fileName.Length > 4 Then
                If fileName.Substring(fileName.Length - 4).ToUpper = ".TXT" Then
                    If fileName.Split("_").Length >= 2 Then
                        RemainFileName = fileName.Substring(fileName.Split("_")(0).Length + 1) '+'_'
                        CessieNumber = RemainFileName.Substring(0, RemainFileName.Length - 4) ' fileName.Split("_")(1).Substring(0, fileName.Split("_")(1).Length - 4)
                        CessieNumber = CessieNumber.Replace("_", "/")
                        _Cessie = oCFac.Retrieve(CessieNumber)
                        If Not IsNothing(_Cessie) AndAlso _Cessie.ID > 0 Then
                            _Cessie.TextFile = fileName

                            'Add By anh untuk baca row file cessie
                            Dim ObjStreamReader As StreamReader
                            Try
                                ObjStreamReader = New StreamReader(fullName, True)
                                Dim row As String = ObjStreamReader.ReadLine
                                Dim _arlPOHeader As ArrayList = New ArrayList
                                Dim objCriteria As CriteriaComposite

                                While row <> "" Or (Not row Is Nothing)
                                    Dim objPOHeader As POHeader
                                    Dim arrOfSpllitedRow() As String

                                    row = row.Trim

                                    If row.Length <> 0 Then
                                        arrOfSpllitedRow = row.Split(";")

                                        If arrOfSpllitedRow.Length < 18 Then
                                            errMessage = "Struktur tidak sesuai"
                                        Else
                                            Dim arlPOHeader As ArrayList = New ArrayList
                                            Dim objPOHeaderFacade As POHeaderFacade = New POHeaderFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                                            objCriteria = New CriteriaComposite( _
                                                            New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                            objCriteria.opAnd(New Criteria(GetType(POHeader), "SONumber", MatchType.Exact, arrOfSpllitedRow(6)))
                                            arlPOHeader = objPOHeaderFacade.Retrieve(objCriteria)
                                            If arlPOHeader.Count > 0 Then
                                                Dim objPOH As POHeader = arlPOHeader(0)
                                                If objPOH.IsFactoring = 1 Then
                                                    For Each objDailyPayment As DailyPayment In objPOH.DailyPayments
                                                        Dim oDPFac As DailyPaymentFacade = New DailyPaymentFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                                                        If objDailyPayment.CessieID < 1 Then
                                                            objDailyPayment.CessieTime = Now.ToString("yyyy.MM.dd HH:mm:ss")
                                                        End If
                                                        objDailyPayment.CessieID = _Cessie.ID
                                                        objDailyPayment.Status = EnumPaymentStatus.PaymentStatus.Selesai

                                                        oDPFac.Update(objDailyPayment)
                                                    Next
                                                Else
                                                    errMessage = "Fail Non Factoring"
                                                End If
                                            Else
                                                errMessage = "SO Number tidak ditemukan"
                                            End If
                                        End If
                                    End If
                                    row = ObjStreamReader.ReadLine
                                End While
                            Catch ex As Exception
                                SysLogParameter.LogErrorToSyslog(ex.Message.ToString, "wsm-worker", "WSCSStatusParser.vb", "Parsing", KTB.DNet.[Lib].DNetLogFormatStatus.Direct, SourceName, SysLogParameter.ParserType.WSCSStatusParser, BlockName)
                                Dim e As Exception = New Exception(fileName & Chr(13) & Chr(10) & errMessage & Chr(13) & Chr(10) & ex.Message)
                                Dim rethrow As Boolean = ExceptionPolicy.HandleException(e, "Parser Policy")
                            Finally
                                ObjStreamReader.Close()
                                ObjStreamReader = Nothing
                            End Try
                            'end added

                        End If
                    End If
                ElseIf fileName.Substring(fileName.Length - 4).ToUpper = ".PDF" Then
                    If fileName.Split("_").Length >= 2 Then
                        RemainFileName = fileName.Substring(fileName.Split("_")(0).Length + 1) '+'_'
                        CessieNumber = RemainFileName.Substring(0, RemainFileName.Length - 4) 'fileName.Split("_")(1).Substring(0, fileName.Split("_")(1).Length - 4)
                        CessieNumber = CessieNumber.Replace("_", "/")
                        _Cessie = oCFac.Retrieve(CessieNumber)
                        If Not IsNothing(_Cessie) AndAlso _Cessie.ID > 0 Then
                            _Cessie.PDFFile = fileName
                        End If
                    End If
                End If

            End If
            Return _Cessie
        End Function


        Protected Overrides Function DoTransaction() As Integer
            Dim Result As Integer = 0

            If Not IsNothing(_Cessie) AndAlso _Cessie.ID > 0 Then
                Dim oCFac As CessieFacade = New CessieFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing))
                Result = oCFac.Update(_Cessie)
            End If
            Return Result
        End Function

        Protected Overrides Function DoParseFixFormatFile(ByVal fileName As String, ByVal user As String) As Object

        End Function
    End Class
End Namespace

