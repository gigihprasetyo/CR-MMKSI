#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Event
Imports KTB.DNet.BusinessFacade.FinishUnit
#End Region

#Region ".NET Base Class Namespace Imports"
'Imports System.IO
'Imports System.Text
'Imports System.Configuration
#End Region


Public Class FrmListRealizationEvent
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents icStartDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icEndDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtRealizationNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRequestNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents dtgRealEvent As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents ddlEventType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Private arlRealEvent As ArrayList
    Dim oDealer As Dealer
    'Private arlDetail As ArrayList = New ArrayList
    'Private objRealEvent As EventInfo
    Private sHelper As SessionHelper = New SessionHelper
    'Private objMatPromo As String
    'Private objVideo As String
    'Private objBiaya As String
#End Region


#Region "Event Handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        oDealer = CType(sHelper.GetSession("DEALER"), Dealer)
        If (oDealer.Title = EnumDealerTittle.DealerTittle.DEALER) Then
            txtDealerCode.Text = oDealer.DealerCode
            txtDealerCode.Enabled = False
            lblSearchDealer.Visible = False
        ElseIf (oDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            lblSearchDealer.Visible = True
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            txtDealerCode.Enabled = True
        End If

        If Not IsPostBack Then
            ViewState("CurrentSortColumn") = "Dealer.DealerCode"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            bindDdlEventType()
            cleardata()
            If Not IsNothing(sHelper.GetSession("SessRealization")) Then
                Dim arlSes As ArrayList = CType(sHelper.GetSession("SessRealization"), ArrayList)
                bindFromSession(arlSes(arlSes.Count - 1), CType(arlSes(0), CriteriaComposite))
            End If
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        bindToGrid(0)
    End Sub

    Private Sub dtgRealEvent_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgRealEvent.ItemCreated
        If (e.Item.ItemType = ListItemType.Header) Then
            e.Item.SetRenderMethodDelegate(New RenderMethod(AddressOf NewRenderHeader))
        End If
    End Sub

    Private Sub dtgRealEvent_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgRealEvent.ItemDataBound
        If e.Item.ItemIndex >= 0 Then
            Dim oInfo As EventInfo = CType(e.Item.DataItem, EventInfo)
            e.Item.Cells(0).Text = (dtgRealEvent.CurrentPageIndex * dtgRealEvent.PageSize + e.Item.ItemIndex + 1).ToString()

            Dim btnShowComment As Button = CType(e.Item.FindControl("btnShowComment"), Button)
            If oInfo.RealComment <> String.Empty Then
                btnShowComment.Attributes.Add("onclick", "showPopUp('../Event/FrmViewComment.aspx?id=" & oInfo.ID & "&page=real','',300,500,null);return false;")
            Else
                btnShowComment.Enabled = False
            End If
            Dim lbtnVideo As LinkButton = CType(e.Item.FindControl("lbtnVideo"), LinkButton)
            Dim lbtnMatPromo As LinkButton = CType(e.Item.FindControl("lbtnMatpromo"), LinkButton)
            Dim lbtnCost As LinkButton = CType(e.Item.FindControl("lbtnCost"), LinkButton)
            If (oInfo.RealCostDetailFile <> String.Empty) Then
                lbtnCost.Visible = True
            Else
                lbtnCost.Visible = False
            End If

            If (oInfo.RealVideoFile <> String.Empty) Then
                lbtnVideo.Visible = True
            Else
                lbtnVideo.Visible = False
            End If

            If (oInfo.RealMatPromoFile <> String.Empty) Then
                lbtnMatPromo.Visible = True
            Else
                lbtnMatPromo.Visible = False
            End If
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If txtDealerCode.Enabled Then
            txtDealerCode.Text = String.Empty
        End If
        cleardata()

    End Sub

    Private Sub dtgRealEvent_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgRealEvent.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        End If

        If Not IsNothing(sHelper.GetSession("SessRealization")) Then
            Dim arlSes As ArrayList = CType(sHelper.GetSession("SessRealization"), ArrayList)
            bindFromSession(arlSes(arlSes.Count - 1), CType(arlSes(0), CriteriaComposite))
        End If

        sortGrid(dtgRealEvent.CurrentPageIndex)
    End Sub

    Private Sub dtgRealEvent_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgRealEvent.PageIndexChanged
        dtgRealEvent.CurrentPageIndex = e.NewPageIndex
        bindToGrid(e.NewPageIndex)
    End Sub

    Private Sub dtgRealEvent_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgRealEvent.ItemCommand
        Dim objFile As EventInfo
        Dim filename As String

        If (e.CommandName <> "Sort") Then
            objFile = New EventInfoFacade(User).Retrieve(CInt(e.CommandArgument))
            filename = KTB.DNet.Lib.WebConfig.GetValue("EventDir") & "\"

            Select Case e.CommandName
                Case "Video"
                    filename = filename & objFile.RealVideoFile
                Case "MatPromo"
                    filename = filename & objFile.RealMatPromoFile
                Case "DetailBiaya"
                    filename = filename & objFile.RealCostDetailFile
                Case "detail"
                    Response.Redirect("../Event/FrmRealizationEvent.aspx?id=" & e.CommandArgument & "&Mode=View", True)
                Case "Edit"
                    Response.Redirect("../Event/FrmRealizationEvent.aspx?id=" & e.CommandArgument & "&Mode=Edit", True)
            End Select
            Response.Redirect("../download.aspx?file=" & filename)
        End If
    End Sub

#End Region

#Region "Custom Method"

    Private Sub NewRenderHeader(ByVal writer As HtmlTextWriter, ByVal ctl As Control)
        Dim cell As TableCell

        Dim idx As Integer = 0

        '---row pertama
        For idx = 0 To 5
            cell = CType(ctl.Controls(idx), TableCell)
            cell.Attributes.Add("rowspan", "2")
            cell.RenderControl(writer)
        Next
        writer.Write("<TD colspan='2' align='center' Class='titleTablePromo'>Jadwal Event</TD>")

        For idx = 8 To 9
            cell = CType(ctl.Controls(idx), TableCell)
            cell.Attributes.Add("rowspan", "2")
            cell.RenderControl(writer)
        Next

        writer.Write("<TD colspan='3' align='center' Class='titleTablePromo'>Hasil Penjualan</TD>")

        For idx = 13 To ctl.Controls.Count - 1
            cell = CType(ctl.Controls(idx), TableCell)
            cell.Attributes.Add("rowspan", "2")
            cell.RenderControl(writer)
        Next

        writer.Write("</TR>")

        dtgRealEvent.HeaderStyle.AddAttributesToRender(writer)

        '--- row kedua
        writer.RenderBeginTag("TR")

        For idx = 6 To 7
            ctl.Controls(idx).RenderControl(writer)
        Next

        For idx = 10 To 12
            ctl.Controls(idx).RenderControl(writer)
        Next

    End Sub

    Private Sub cleardata()
        txtRealizationNo.Text = String.Empty
        txtRequestNo.Text = String.Empty
        ddlEventType.SelectedIndex = -1
        icEndDate.Value = Now()
        icStartDate.Value = Now()
        arlRealEvent = New ArrayList
        arlRealEvent = New ArrayList
        dtgRealEvent.DataSource = arlRealEvent
        dtgRealEvent.DataBind()

    End Sub

    Private Sub bindDdlEventType()
        Dim arlEventType As ArrayList
        arlEventType = New EventTypeFacade(User).RetrieveList
        If arlEventType.Count > 0 Then
            ddlEventType.DataTextField = "Description"
            ddlEventType.DataValueField = "ID"
            ddlEventType.DataSource = arlEventType
            ddlEventType.DataBind()
            ddlEventType.Items.Insert(0, New ListItem("Silahkan Pilih", "0"))
        End If
    End Sub

    Private Sub bindToGrid(ByVal currentPageIndex As Integer)
        Dim totalRow As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.EventInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EventInfo), "IsRealization", MatchType.Exact, 1))

        
        oDealer = Session("DEALER")
        If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then  '---User is login as Dealer
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EventInfo), "Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text))

        Else '----User Login As KTB
            If txtDealerCode.Text <> String.Empty Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EventInfo), "Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Replace(";", "','") & "')"))
            End If
        End If

        If txtRealizationNo.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EventInfo), "EventRealizationNo", MatchType.[Partial], txtRealizationNo.Text.Trim))
        End If

        If txtRequestNo.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EventInfo), "EventRequestNo", MatchType.[Partial], txtRequestNo.Text.Trim))
        End If

        If ddlEventType.SelectedIndex > 0 Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EventInfo), "EventType.ID", MatchType.Exact, ddlEventType.SelectedValue))
        End If

        If icStartDate.Value > icEndDate.Value Then
            MessageBox.Show("Tanggal Mulai Tidak Boleh Melebihi Tanggal Akhir")
            Return
        Else
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EventInfo), "RealDateStart", MatchType.GreaterOrEqual, icStartDate.Value))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.EventInfo), "RealDateEnd", MatchType.LesserOrEqual, icEndDate.Value))
        End If

        Try
            arlRealEvent = New EventInfoFacade(User).RetrieveByCriteria(criterias, currentPageIndex + 1, dtgRealEvent.PageSize, totalRow)
            If arlRealEvent.Count = 0 Then
                sHelper.SetSession("SessarlRealEvent", Nothing)
                sHelper.SetSession("SessRealization", Nothing)
                MessageBox.Show("Data Tidak Ditemukan")
            Else
                '---create search session 
                Dim arlForSession As ArrayList = New ArrayList
                arlForSession.Add(criterias)
                arlForSession.Add(currentPageIndex)
                sHelper.SetSession("SessarlRealEvent", arlRealEvent)
                sHelper.SetSession("SessRealization", arlForSession)
                '--------------------
            End If
        Catch ex As Exception
            MessageBox.Show("Pencarian Gagal")
        Finally
            dtgRealEvent.DataSource = arlRealEvent
            dtgRealEvent.DataBind()
        End Try
    End Sub

    Private Sub bindFromSession(ByVal _idx As Integer, ByVal _kriteria As CriteriaComposite)
        Dim _rowTotal As Integer = 0
        Try
            arlRealEvent = New EventInfoFacade(User).RetrieveByCriteria(_kriteria, _idx + 1, dtgRealEvent.PageSize, _rowTotal)
            If arlRealEvent.Count = 0 Then
                sHelper.SetSession("SessarlRealEvent", Nothing)
                sHelper.SetSession("SessRealization", Nothing)
                MessageBox.Show("Data Tidak Ditemukan")
            Else
                '---create search session 
                Dim arlForSession As ArrayList = New ArrayList
                arlForSession.Add(_kriteria)
                arlForSession.Add(_idx)
                sHelper.SetSession("SessRealization", arlForSession)
                sHelper.SetSession("SessarlRealEvent", arlRealEvent)
                '--------------------
            End If
        Catch ex As Exception
            MessageBox.Show("Pencarian Gagal")
        Finally
            dtgRealEvent.DataSource = arlRealEvent
            dtgRealEvent.DataBind()
        End Try
    End Sub

    Private Sub sortGrid(ByVal _idx As Integer)
        If Not IsNothing(sHelper.GetSession("SessarlRealEvent")) Then
            arlRealEvent = CType(sHelper.GetSession("SessarlRealEvent"), ArrayList)
            arlRealEvent = CommonFunction.PageAndSortArraylist(arlRealEvent, _idx, dtgRealEvent.PageSize, GetType(EventInfo), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect"))
            dtgRealEvent.DataSource = arlRealEvent
            dtgRealEvent.DataBind()
        End If

    End Sub
#End Region

   

    
    
End Class
