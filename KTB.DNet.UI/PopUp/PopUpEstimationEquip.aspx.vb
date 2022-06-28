Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.IndentPartEquipment
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

Public Class PopUpEstimationEquip
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dtgEstimationEquip As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnChoose As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents icPODateFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icPODateUntil As KTB.DNet.WebCC.IntiCalendar

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Deklarasi"
    Dim criterias As CriteriaComposite
    Dim sHelper As New SessionHelper
#End Region

#Region "event handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Dim oDealer As Dealer = sHelper.GetSession("DEALER")
        If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            lblSearchDealer.Visible = False
            txtDealerCode.Enabled = False
            txtDealerCode.BorderStyle = BorderStyle.None
            txtDealerCode.Text = oDealer.DealerCode
        End If
        If Not IsPostBack Then
            ViewState.Add("SortColumn", "CreatedTime")
            ViewState.Add("SortDirection", Sort.SortDirection.ASC)
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection()"
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If icPODateUntil.Value < icPODateFrom.Value Then
            MessageBox.Show("Tanggal Pengajuan 'Dari' tidak boleh lebih besar dari Tanggal Pengajuan 'Sampai'")
            Return
        End If

        dtgEstimationEquip.CurrentPageIndex = 0
        CreateCriteria()
        BindToGrid(dtgEstimationEquip.CurrentPageIndex)
        If dtgEstimationEquip.Items.Count > 0 Then
            btnChoose.Disabled = False
        Else
            btnChoose.Disabled = True
        End If
    End Sub

    Private Sub dtgEstimationEquip_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgEstimationEquip.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = (dtgEstimationEquip.PageSize * dtgEstimationEquip.CurrentPageIndex) + e.Item.ItemIndex + 1

            If Not IsNothing(Request.QueryString("IsMultiple")) Then
                If (Not CType(Request.QueryString("IsMultiple"), Boolean)) Then
                    'CType(e.Item.FindControl("chkItemChecked"), CheckBox).Visible = False
                    Dim rdbChoice As LiteralControl = New LiteralControl("<INPUT type=radio name=""radio"">")
                    e.Item.Cells(0).Controls.Add(rdbChoice)
                End If
            Else
                Dim chkItem As CheckBox = New CheckBox
                chkItem.ID = "chkItemChecked"
                e.Item.Cells(0).Controls.Add(chkItem)
            End If

            Dim objEstimationEquipHeader As EstimationEquipHeader
            Dim arrTmp As ArrayList
            Dim strTmp As String = ""
            Dim strTmp2 As String = ""
            Dim lblTglKonfirmasi As Label

            arrTmp = CType(dtgEstimationEquip.DataSource, ArrayList)
            objEstimationEquipHeader = arrTmp(e.Item.ItemIndex)


            For Each objTmp As EstimationEquipDetail In objEstimationEquipHeader.EstimationEquipDetails
                If strTmp2 <> Format(objTmp.ConfirmedDate, "dd/MM/yyyy") Then
                    strTmp = strTmp & Format(objTmp.ConfirmedDate, "dd/MM/yyyy") & vbCrLf
                    strTmp2 = Format(objTmp.ConfirmedDate, "dd/MM/yyyy")
                End If
            Next

            If strTmp <> "" Then strTmp = Left(strTmp, Len(strTmp) - 1)
            lblTglKonfirmasi = CType(e.Item.FindControl("lblTglKonfirmasi"), Label)
            lblTglKonfirmasi.Text = strTmp
        End If
    End Sub

    Private Sub dtgEstimationEquip_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgEstimationEquip.SortCommand
        If e.SortExpression = ViewState("SortColumn") Then
            If ViewState("SortDirection") = Sort.SortDirection.ASC Then
                ViewState.Add("SortDirection", Sort.SortDirection.DESC)
            Else
                ViewState.Add("SortDirection", Sort.SortDirection.ASC)
            End If
        End If
        ViewState.Add("SortColumn", e.SortExpression)
        BindToGrid(0)
    End Sub

    Private Sub dtgEstimationEquip_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgEstimationEquip.PageIndexChanged
        dtgEstimationEquip.CurrentPageIndex = e.NewPageIndex
        BindToGrid(dtgEstimationEquip.CurrentPageIndex)
    End Sub

#End Region

#Region "Custom Method"

    Private Sub BindToGrid(ByVal indexpage As Integer)
        Dim totalRow As Integer = 0
        If indexpage >= 0 Then
            Dim arlHeader As ArrayList = New EstimationEquipHeaderFacade(User).RetrieveActiveList(indexpage + 1, dtgEstimationEquip.PageSize, totalRow, ViewState("SortColumn"), ViewState("SortDirection"), sHelper.GetSession("crits"))
            dtgEstimationEquip.DataSource = arlHeader
            dtgEstimationEquip.VirtualItemCount = totalRow
            dtgEstimationEquip.DataBind()
        End If
    End Sub

    Private Sub CreateCriteria()
        Dim kdDealer As String = txtDealerCode.Text.Replace(";", "','")
        Dim tglPOStart As Date = icPODateFrom.Value
        Dim tglPOUntil As Date = icPODateUntil.Value

        criterias = New CriteriaComposite(New Criteria(GetType(EstimationEquipHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtDealerCode.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(EstimationEquipHeader), "Dealer.DealerCode", MatchType.InSet, "('" & kdDealer & "')"))
        End If

        If Not IsNothing(Request.QueryString("IsMultiple")) Then
            If (Not CType(Request.QueryString("IsMultiple"), Boolean)) Then
                criterias.opAnd(New Criteria(GetType(EstimationEquipHeader), "Status", MatchType.Exact, CInt(EnumEstimationEquipStatus.EstimationEquipStatusHeader.Selesai)), "(", True)
                criterias.opOr(New Criteria(GetType(EstimationEquipHeader), "Status", MatchType.Exact, CInt(EnumEstimationEquipStatus.EstimationEquipStatusHeader.Konfirmasi_Sebagian)), ")", False)
            End If
        Else
            If Request.QueryString("isAlokasi") <> String.Empty Then
                If Request.QueryString("isAlokasi") = "1" Then
                    criterias.opAnd(New Criteria(GetType(EstimationEquipHeader), "Status", MatchType.GreaterOrEqual, CInt(EnumEstimationEquipStatus.EstimationEquipStatusHeader.Baru)))
                    criterias.opAnd(New Criteria(GetType(EstimationEquipHeader), "Status", MatchType.No, CInt(EnumEstimationEquipStatus.EstimationEquipStatusHeader.Batal)))
                End If
            End If
        End If

        criterias.opAnd(New Criteria(GetType(EstimationEquipHeader), "CreatedTime", MatchType.GreaterOrEqual, tglPOStart))
        criterias.opAnd(New Criteria(GetType(EstimationEquipHeader), "CreatedTime", MatchType.LesserOrEqual, tglPOUntil.AddDays(1)))

        sHelper.SetSession("crits", criterias)
    End Sub

#End Region

End Class
