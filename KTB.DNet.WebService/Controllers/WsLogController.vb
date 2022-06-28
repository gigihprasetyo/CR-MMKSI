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

Public Class WsLogController
    Inherits System.Web.Mvc.Controller



    Function Index(ByVal TXTDATEFROM As String, TXTDATETO As String, TXTKEYNAME As String, TXTSTATUS As String, TXTBODY As String, page As Integer?) As ActionResult

        ViewBag.TXTDATEFROM = If(String.IsNullOrEmpty(TXTDATEFROM), DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"), TXTDATEFROM)
        ViewBag.TXTDATETO = If(String.IsNullOrEmpty(TXTDATETO), DateTime.Now.ToString("yyyy-MM-dd"), TXTDATETO)
        ViewBag.TXTKEYNAME = If(String.IsNullOrEmpty(TXTKEYNAME), "", TXTKEYNAME)
        ViewBag.TXTSTATUS = If(String.IsNullOrEmpty(TXTSTATUS), "", TXTSTATUS)
        ViewBag.TXTBODY = If(String.IsNullOrEmpty(TXTBODY), "", TXTBODY)

        Dim DS As List(Of WsLog) = New List(Of WsLog)
        Dim ObWsF As New WsLogFacade(User)
        Dim Objcrit As CriteriaComposite
        Dim sortColl As SortCollection = New SortCollection

        Objcrit = New CriteriaComposite(New Criteria(GetType(WsLog), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        sortColl.Add(New Sort(GetType(WsLog), "ID", Sort.SortDirection.DESC))
        Try
            If IsNothing(page) OrElse page = 0 Then
                page = 1
            End If

            If Not IsNothing(ViewBag.TXTDATEFROM) Then
                Objcrit.opAnd(New Criteria(GetType(WsLog), "CreatedTime", MatchType.GreaterOrEqual, Convert.ToDateTime(ViewBag.TXTDATEFROM)))
            End If

            If Not IsNothing(ViewBag.TXTDATETO) Then
                Objcrit.opAnd(New Criteria(GetType(WsLog), "CreatedTime", MatchType.LesserOrEqual, Convert.ToDateTime(ViewBag.TXTDATETO).AddDays(1)))
            End If

            If Not IsNothing(ViewBag.TXTKEYNAME) AndAlso ViewBag.TXTKEYNAME.ToString() <> "" Then
                Objcrit.opAnd(New Criteria(GetType(WsLog), "Body", MatchType.StartsWith, "K;" & ViewBag.TXTKEYNAME.ToString().Trim()))
            End If

            If Not IsNothing(ViewBag.TXTSTATUS) AndAlso ViewBag.TXTSTATUS.ToString() <> "" Then
                Objcrit.opAnd(New Criteria(GetType(WsLog), "Status", MatchType.Exact, ViewBag.TXTSTATUS))
            End If

            If Not IsNothing(ViewBag.TXTBODY) AndAlso ViewBag.TXTBODY.ToString() <> "" Then
                Objcrit.opAnd(New Criteria(GetType(WsLog), "Body", MatchType.Partial, ViewBag.TXTBODY))
            End If

        Catch ex As Exception

        End Try


        Dim totalRow As Integer = 0


        ' DS = ObWsF.RetrieveByCriteria(Objcrit, page, 10, totalRow).Cast(Of WsLog)().ToList()

        DS = ObWsF.RetrieveCustomPagingBySP(Objcrit, sortColl).Cast(Of WsLog)().ToList()

        ViewBag.totalRow = DS.Count()
        Return View(DS)
    End Function

    Function ViewDetail(ByVal ID As String) As ActionResult

        Dim oWsLog As New WsLog
        Dim ObWsF As New WsLogFacade(User)
        oWsLog = ObWsF.Retrieve(CInt(ID))

        ViewBag.IP = Me.GetAppURL()

        Return View(oWsLog)

    End Function


    Function WsSubmiter() As ActionResult

        ViewBag.IP = Me.GetAppURL()
        Return View()
    End Function

    Function SendFile() As ActionResult

        ViewBag.IP = Me.GetAppURL()
        Return View()
    End Function


    Private Function GetAppURL() As String
        Dim result As String = String.Empty
        result = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd((New Char() {"/"})) + "/"
        Return result
    End Function

End Class