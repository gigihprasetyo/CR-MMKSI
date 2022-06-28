#Region "Lib"
Imports System.Net
Imports System.Web.Http
Imports KTB.DNet.Parser
Imports System.Net.Http
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain
Imports System.IO

Imports System.Linq
Imports System.Data
Imports System.Text
Imports System.Security.Principal
Imports KTB.DNet.Domain.Search

#End Region

Public Class BapiLogController
    Inherits Controller

    Function Index(ByVal TXTDATEFROM As String, TXTDATETO As String, TXTKEYNAME As String, TXTSTATUS As String, TXTBODY As String, page As Integer?) As ActionResult

        ViewBag.TXTDATEFROM = If(String.IsNullOrEmpty(TXTDATEFROM), DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), TXTDATEFROM)
        ViewBag.TXTDATETO = If(String.IsNullOrEmpty(TXTDATETO), DateTime.Now.ToString("yyyy-MM-dd"), TXTDATETO)
        ViewBag.TXTKEYNAME = If(String.IsNullOrEmpty(TXTKEYNAME), "", TXTKEYNAME)
        ViewBag.TXTSTATUS = If(String.IsNullOrEmpty(TXTSTATUS), "", TXTSTATUS)
        ViewBag.TXTBODY = If(String.IsNullOrEmpty(TXTBODY), "", TXTBODY)

        Dim DS As List(Of BapiLog) = New List(Of BapiLog)
        Dim ObWsF As New BapiLogFacade(User)
        Dim Objcrit As CriteriaComposite
        Dim sortColl As SortCollection = New SortCollection

        Objcrit = New CriteriaComposite(New Criteria(GetType(BapiLog), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        sortColl.Add(New Sort(GetType(BapiLog), "ID", Sort.SortDirection.DESC))
        Try
            If IsNothing(page) OrElse page = 0 Then
                page = 1
            End If

            If Not IsNothing(ViewBag.TXTDATEFROM) Then
                Objcrit.opAnd(New Criteria(GetType(BapiLog), "SubmitDate", MatchType.GreaterOrEqual, Convert.ToDateTime(ViewBag.TXTDATEFROM)))
            End If

            If Not IsNothing(ViewBag.TXTDATETO) Then
                Objcrit.opAnd(New Criteria(GetType(BapiLog), "SubmitDate", MatchType.LesserOrEqual, Convert.ToDateTime(ViewBag.TXTDATETO).AddDays(1)))
            End If

            If Not IsNothing(ViewBag.TXTKEYNAME) AndAlso ViewBag.TXTKEYNAME.ToString() <> "" Then
                Objcrit.opAnd(New Criteria(GetType(BapiLog), "KindLog", MatchType.Exact, ViewBag.TXTKEYNAME.ToString().Trim()))
            End If

            If Not IsNothing(ViewBag.TXTSTATUS) AndAlso ViewBag.TXTSTATUS.ToString() <> "" Then
                Objcrit.opAnd(New Criteria(GetType(BapiLog), "Status", MatchType.Exact, ViewBag.TXTSTATUS))
            End If

            If Not IsNothing(ViewBag.TXTBODY) AndAlso ViewBag.TXTBODY.ToString() <> "" Then
                Objcrit.opAnd(New Criteria(GetType(BapiLog), "Body", MatchType.Partial, ViewBag.TXTBODY))
            End If

        Catch ex As Exception

        End Try


        Dim totalRow As Integer = 0

        DS = ObWsF.Retrieve(Objcrit, sortColl).Cast(Of BapiLog)().ToList()

        ViewBag.totalRow = DS.Count()
        Return View(DS)
    End Function

    Function ViewDetail(ByVal ID As String) As ActionResult

        Dim oBapiLog As New BapiLog
        Dim ObWsF As New BapiLogFacade(User)
        oBapiLog = ObWsF.Retrieve(CInt(ID))

        Return View(oBapiLog)

    End Function

    Function GetEnumDesc(ByVal val As Integer) As String
        Return [Enum].GetName(GetType(EnumDNET.enumBapiLogKind), val)
    End Function
End Class
