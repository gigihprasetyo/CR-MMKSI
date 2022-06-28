Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Public Class FrmPengajuanPencairanDepositAViewEdit
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgEntryPencairanDepositA As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblTotal As System.Web.UI.WebControls.Label
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
    Private arlDepositAPencairanD As ArrayList = New ArrayList
    Private arlDepositAPencairanDFilter As ArrayList = New ArrayList

    Dim sHelper As New SessionHelper
#End Region

#Region "Custom Method"

    Private Sub BindDetailToGrid(ByVal ID As Integer)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanD), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DepositAPencairanD), "DepositAPencairanH.ID", MatchType.Exact, ID))

        Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
        If (Not IsNothing(ViewState("currSortColumn"))) And (Not IsNothing(ViewState("currSortDirection"))) Then
            sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(DepositAPencairanD), ViewState("currSortColumn"), ViewState("currSortDirection")))
        Else
            sortColl = Nothing
        End If

        arlDepositAPencairanD = New FinishUnit.DepositAPencairanDFacade(User).Retrieve(criterias, sortColl)
        Dim DealerCode As String = String.Empty
        For Each item As DepositAPencairanD In arlDepositAPencairanD
            'If (Not IsExist(item.Dealer.DealerCode, arlDepositAPencairan Filter)) Then
            arlDepositAPencairanDFilter.Add(item)
            'End If
        Next

        If (arlDepositAPencairanDFilter.Count > 0) Then
            dgEntryPencairanDepositA.Visible = True
            dgEntryPencairanDepositA.DataSource = arlDepositAPencairanDFilter
            dgEntryPencairanDepositA.DataBind()
        Else
            dgEntryPencairanDepositA.Visible = False

            MessageBox.Show("Data tidak ditemukan")
        End If

        '    'If pTipePengajuan = TipePengajuan.Offset Then
        '    '    If pTipePengajuan = TipePengajuan.Offset And pTipeDokumen = "SO" Then
        '    '        If ddlSO.SelectedIndex > 0 Then
        '    '            Dim RowStatus As Integer = -1
        '    '            objPencairan = sesHelper.GetSession("SalesEntryPencairan")
        '    '            If Not (objPencairan Is Nothing) Then
        '    '                If objPencairan.Rows.Count > 0 Then
        '    '                    If objPencairan.Rows(0).Item("AssignmentNumber") <> ddlSO.SelectedItem.Text Then
        '    '                        RowStatus = 0
        '    '                        objPencairan.Rows.Clear()
        '    '                    Else
        '    '                        'dtPengajuan.Rows.Clear()


        '    '                        dtPengajuan = objPencairan
        '    '                    End If
        '    '                Else
        '    '                    RowStatus = 0
        '    '                    objPencairan.Rows.Clear()
        '    '                End If
        '    '            Else
        '    '                RowStatus = 0
        '    '            End If

        '    '            If RowStatus = 0 Then
        '    '                Dim objDebitNote As DebitNote = New FinishUnit.DebitNoteFacade(User).Retrieve(ddlSO.SelectedValue)
        '    '                Dim dr As DataRow
        '    '                dr = dtPengajuan.NewRow
        '    '                dr("AssignmentNumber") = objDebitNote.Assignment.ToString
        '    '                dr("DealerAmount") = objDebitNote.Amount
        '    '                dr("HeaderAmount") = objDebitNote.Amount
        '    '                dr("PPn") = 0.1 * objDebitNote.Amount
        '    '                dr("Penjelasan") = objDebitNote.Description
        '    '                dtPengajuan.Rows.Add(dr)
        '    '            End If
        '    '            dgEntryPencairanDepositA.Columns(1).Visible = True
        '    '        Else
        '    '            dtPengajuan = Nothing
        '    '        End If
        '    '        dgEntryPencairanDepositA.ShowFooter = False

        '    '        objPencairan = dtPengajuan
        '    '        sesHelper.SetSession("SalesEntryPencairan", objPencairan)
        '    '    ElseIf pTipePengajuan = TipePengajuan.Offset And pTipeDokumen = "DN" Then
        '    '        dtPengajuan.Rows.Clear()
        '    '        If ddlDN.SelectedIndex > 0 Then
        '    '            Dim objDebitNote As DebitNote = New FinishUnit.DebitNoteFacade(User).Retrieve(ddlDN.SelectedValue)

        '    '            Dim dr As DataRow
        '    '            dr = dtPengajuan.NewRow
        '    '            dr("DNNumber") = objDebitNote.DNNumber.ToString
        '    '            dr("DealerAmount") = objDebitNote.Amount
        '    '            dr("HeaderAmount") = objDebitNote.Amount
        '    '            dr("PPn") = 0.1 * objDebitNote.Amount
        '    '            dr("Penjelasan") = objDebitNote.Description
        '    '            dtPengajuan.Rows.Add(dr)
        '    '        Else
        '    '            dgEntryPencairanDepositA.Columns(1).Visible = False
        '    '            dtPengajuan = Nothing
        '    '        End If
        '    '        dgEntryPencairanDepositA.ShowFooter = False

        '    '        objPencairan = dtPengajuan
        '    '        sesHelper.SetSession("SalesEntryPencairan", objPencairan)
        '    '    End If
        '    'Else
        '    '    dgEntryPencairanDepositA.Columns(1).Visible = False
        '    '    dgEntryPencairanDepositA.ShowFooter = True

        '    '    objPencairan = sesHelper.GetSession("SalesEntryPencairan")
        '    '    'If dtPengajuan Is Nothing Or dtPengajuan.Columns.Count = 0 Then
        '    '    If dtPengajuan.Columns.Count = 0 Then
        '    '        CreateColumn()
        '    '    End If
        '    '    'If objPencairan.Rows(0).Item("AssignmentNumber") <> ddlSO.SelectedItem.Text Then
        '    '    '    'dtPengajuan.Rows.Clear()
        '    '    'End If
        '    '    If (objPencairan Is Nothing) Then
        '    '        objPencairan = dtPengajuan
        '    '        sesHelper.SetSession("SalesEntryPencairan", objPencairan)
        '    '    Else
        '    '        If Not gridEdited Then
        '    '            dtPengajuan.Rows.Clear()
        '    '            objPencairan = dtPengajuan
        '    '        End If
        '    '        sesHelper.SetSession("SalesEntryPencairan", objPencairan)

        '    '    End If
        '    'End If

        '    'dgEntryPencairanDepositA.DataSource = objPencairan
        '    'dgEntryPencairanDepositA.DataBind()
    End Sub

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()

        If Not SecurityProvider.Authorize(context.User, SR.DepositAView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=FINISH UNIT - Pengajuan Pencairan Deposit A Detail")
        End If
    End Sub

    Private Function CekBtnPriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanClaimCreateData_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

#Region "Event Handlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'InitiateAuthorization()
        If Not IsPostBack Then
            'If Request.Params("Mode").ToString() = "Edit" Then
            '    ViewState("Mode") = enumMode.Mode.EditMode
            '    'objDepositAPencairanD = New FinishUnit.DepositAPencairanDFacade(User).Retrieve(CInt(Request.Params("id")))
            '    'sHelper.SetSession("DepositAPencairanD", objDepositAPencairanD)
            '    'Dim al As New ArrayList
            '    'al.Add(objDepositAPencairanD)
            '    'sHelper.SetSession("BabitAlocationList", al)
            '    'dgAlokasiBabit.DataSource = CType(sHelper.GetSession("BabitAlocationList"), ArrayList)
            '    'dgAlokasiBabit.DataBind()
            'End If
            BindDetailToGrid(CInt(Request.Params("id").ToString()))
            'BindDetailToGrid(CInt(Request.QueryString("id")))
        End If
    End Sub

    Private TotalAmount As Double = 0
    Private Sub dgEntryPencairanDepositA_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgEntryPencairanDepositA.ItemDataBound
        If ((e.Item.ItemType = ListItemType.Item) Or (e.Item.ItemType = ListItemType.AlternatingItem)) Then
            Dim objPencairanDepositAD As DepositAPencairanD = CType(e.Item.DataItem, DepositAPencairanD)

            Dim selectedTipe As Integer = objPencairanDepositAD.DepositAPencairanH.Type
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dgEntryPencairanDepositA.CurrentPageIndex * dgEntryPencairanDepositA.PageSize)

            TotalAmount = TotalAmount + Convert.ToDouble(DataBinder.Eval(e.Item.DataItem, "DealerAmount"))
            lblTotal.Text = IIf(TotalAmount = 0, 0, TotalAmount.ToString("#,###"))

            Dim lblPPn As Label = CType(e.Item.FindControl("lblPPn"), Label)
            Dim PPn As Double = 0.1 * objPencairanDepositAD.DealerAmount
            lblPPn.Text = PPn.ToString("#,###")
            Dim lblHeaderAmount As Label = CType(e.Item.FindControl("lblHeaderAmount"), Label)

            If selectedTipe = TipePengajuan.Offset Then
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.DebitNote), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim agg As Aggregate = New Aggregate(GetType(KTB.DNet.Domain.DebitNote), "Amount", AggregateType.Sum)
                If objPencairanDepositAD.DepositAPencairanH.DNNumber.ToString() <> "" Then
                    e.Item.Cells(5).Visible = False
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DebitNote), "DNNumber", MatchType.Exact, objPencairanDepositAD.DepositAPencairanH.DNNumber))
                    Dim DNAmount As Double = New FinishUnit.DebitNoteFacade(User).RetrieveScalar(criterias, agg)
                    lblHeaderAmount.Text = DNAmount.ToString("#,####")
                Else
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.DebitNote), "Assignment", MatchType.Exact, objPencairanDepositAD.DepositAPencairanH.AssignmentNumber))

                    Dim SOAmount As Double = New FinishUnit.DebitNoteFacade(User).RetrieveScalar(criterias, agg)

                    lblHeaderAmount.Text = SOAmount.ToString("#,####")
                End If
                'lblHeaderAmount.Visible = True
            Else
                e.Item.Cells(1).Visible = False
                'lblHeaderAmount.Visible = False
            End If
        ElseIf (e.Item.ItemType = ListItemType.EditItem) Then
            Dim txtJumlahPencairanEdit As TextBox = CType(e.Item.FindControl("txtJumlahPencairanEdit"), TextBox)
            Dim temp As String = "CalculatePPn('dgEntryPencairanDepositA__ctl_txtJumlahPencairanEdit_', 'dgEntryPencairanDepositA__ctl_txtPPnEdit_' )"
            txtJumlahPencairanEdit.Attributes.Add("OnBlur", temp)
        End If
    End Sub

    Private Sub dgEntryPencairanDepositA_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgEntryPencairanDepositA.CancelCommand
        'gridEdited = False

        MessageBox.Show("Masuk Cancel Command Grid")
        'dgEntryPencairanDepositA.EditItemIndex = -1
        'Dim selectedTipe As TipePengajuan = CType([Enum].Parse(GetType(TipePengajuan), ddlTipePengajuan.SelectedValue), TipePengajuan)
        'Dim pTipeDokumen As String
        'If rbDN.Checked Then
        '    pTipeDokumen = "DN"
        'Else
        '    pTipeDokumen = "SO"
        'End If
        'BindDetailToGrid(selectedTipe, pTipeDokumen)
    End Sub

    Private Sub dgEntryPencairanDepositA_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgEntryPencairanDepositA.UpdateCommand
        'TotalAmount = 0
        'objPencairan = sesHelper.GetSession("SalesEntryPencairan")

        'If rbSO.Checked = True Then
        '    Dim myRow As DataRow = objPencairan.Rows(e.Item.ItemIndex)
        '    Dim txtJumlahPencairanEdit As TextBox = e.Item.FindControl("txtJumlahPencairanEdit")
        '    If txtJumlahPencairanEdit.Text < myRow("HeaderAmount") Then
        '        myRow("DealerAmount") = txtJumlahPencairanEdit.Text
        '    Else
        '        'myRow("DealerAmount") = DealerAmount
        '    End If
        '    Dim txtPenjelasanEntryEdit As TextBox = e.Item.FindControl("txtPenjelasanEntryEdit")
        '    myRow("Penjelasan") = txtPenjelasanEntryEdit.Text
        '    myRow("PPn") = 0.1 * CDbl(txtJumlahPencairanEdit.Text)
        '    gridEdited = True
        '    objPencairan.AcceptChanges()
        '    sesHelper.SetSession("SalesEntryPencairan", objPencairan)
        'Else
        '    gridEdited = True
        '    objPencairan.AcceptChanges()
        '    sesHelper.SetSession("SalesEntryPencairan", objPencairan)
        'End If
        'dgEntryPencairanDepositA.EditItemIndex = -1
        'Dim selectedTipe As TipePengajuan = CType([Enum].Parse(GetType(TipePengajuan), ddlTipePengajuan.SelectedValue), TipePengajuan)
        'Dim pTipeDokumen As String
        'If rbDN.Checked Then
        '    pTipeDokumen = "DN"
        'Else
        '    pTipeDokumen = "SO"
        'End If
        'BindDetailToGrid(selectedTipe, pTipeDokumen)
    End Sub
#End Region

#Region "Internal Enum"
    Private Enum TipePengajuan
        Offset = 1
        CashAnnual = 2
        CashIncidental = 3
    End Enum
#End Region

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Server.Transfer("~/FinishUnit/FrmDaftarPengajuanPencairanDepositA.aspx")
    End Sub
End Class
