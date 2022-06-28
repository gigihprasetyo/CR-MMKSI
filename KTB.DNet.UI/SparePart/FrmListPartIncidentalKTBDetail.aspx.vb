#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.SparePart
'Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
'Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Utility
'Imports KTB.DNet.UI
Imports KTB.DNet.Security
#End Region

Public Class FrmListPartIncidentalKTBDetail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents Label13 As System.Web.UI.WebControls.Label
    Protected WithEvents Label14 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNomorPermintaan As System.Web.UI.WebControls.Label
    Protected WithEvents lblKodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblIncidentalDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblPoliceNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblWorkOrder As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents lblPIC As System.Web.UI.WebControls.Label
    Protected WithEvents dgPartListDetail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Label15 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNama As System.Web.UI.WebControls.Label
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents btnPenyelesaian As System.Web.UI.WebControls.Button
    Protected WithEvents btnCetak1 As System.Web.UI.WebControls.Button
    Protected WithEvents lblTipeValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblTahunProduksiValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoRangkaValue As System.Web.UI.WebControls.Label
    Protected WithEvents Label16 As System.Web.UI.WebControls.Label
    Protected WithEvents Label17 As System.Web.UI.WebControls.Label
    Protected WithEvents Label18 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoRangka As System.Web.UI.WebControls.Label
    Protected WithEvents lblTipe As System.Web.UI.WebControls.Label
    Protected WithEvents lblTahunProduksi As System.Web.UI.WebControls.Label
    Protected WithEvents lblTelp As System.Web.UI.WebControls.Label
    Protected WithEvents lblTelpValue As System.Web.UI.WebControls.Label
    Protected WithEvents Label19 As System.Web.UI.WebControls.Label
    Protected WithEvents Label20 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNomorSurat As System.Web.UI.WebControls.Label
    Protected WithEvents lblNomorSuratValue As System.Web.UI.WebControls.Label
    Protected WithEvents Label23 As System.Web.UI.WebControls.Label
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Method"
    Private isFinish As Boolean = False
    Private Sub BindHeaderToForm(ByVal id As Integer)
        Dim objPartIncidentalHeader As New PartIncidentalHeader

        objPartIncidentalHeader = New PartIncidentalHeaderFacade(User).Retrieve(id)
        'lblNomorPermintaan.Text = objPartIncidentalHeader.Dealer.SearchTerm2 & "-" & objPartIncidentalHeader.ID.ToString.PadLeft(7, "0")
        lblNomorPermintaan.Text = objPartIncidentalHeader.RequestNumber
        lblKodeDealer.Text = objPartIncidentalHeader.Dealer.DealerCode
        lblNama.Text = objPartIncidentalHeader.Dealer.DealerName & " / " & objPartIncidentalHeader.Dealer.SearchTerm2
        lblIncidentalDate.Text = objPartIncidentalHeader.IncidentalDate
        lblPoliceNumber.Text = objPartIncidentalHeader.PoliceNumber

        lblTelpValue.Text = objPartIncidentalHeader.Phone
        lblNoRangkaValue.Text = objPartIncidentalHeader.ChassisNumber

        Dim oChassisFacade As New KTB.DNet.BusinessFacade.FinishUnit.ChassisMasterFacade(User)
        Dim objChassisMaster As ChassisMaster
        If oChassisFacade.IsExist(lblNoRangkaValue.Text) Then
            lblTahunProduksiValue.Text = objPartIncidentalHeader.AssemblyYear
        Else
            lblTahunProduksiValue.Text = "N/A"
        End If


        lblTipeValue.Text = objPartIncidentalHeader.VehicleType
        lblNomorSuratValue.Text = objPartIncidentalHeader.DealerMailNumber

        lblWorkOrder.Text = objPartIncidentalHeader.WorkOrder
        lblStatus.Text = objPartIncidentalHeader.Status
        lblPIC.Text = objPartIncidentalHeader.PIC
        If objPartIncidentalHeader.KTBStatus = PartIncidentalStatus.PartIncidentalKTBStatusEnum.Selesai Then
            btnPenyelesaian.Enabled = False
            isFinish = True
        Else
            btnPenyelesaian.Enabled = True
            isFinish = False
        End If
        If objPartIncidentalHeader.KTBStatus = CType(PartIncidentalStatus.PartIncidentalKTBStatusEnum.Selesai, Integer) Then
            btnCetak1.Visible = False
            btnPenyelesaian.Visible = False
        End If
        If objPartIncidentalHeader.KTBStatus = CType(PartIncidentalStatus.PartIncidentalKTBStatusEnum.Baru, Integer) Then
            btnPenyelesaian.Enabled = False
        End If

    End Sub

    Private Sub BindDataGrid(ByVal id As Integer)
        Dim _arrDataGrid As ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PartIncidentalDetail), "PartIncidentalHeader.ID", MatchType.Exact, id))

        _arrDataGrid = New PartIncidentalDetailFacade(User).Retrieve(criterias)
        If _arrDataGrid.Count > 0 Then
            dgPartListDetail.DataSource = _arrDataGrid
            dgPartListDetail.DataBind()
        Else
            dgPartListDetail.DataBind()
        End If

    End Sub

     

    Private Function PopulateRemark(ByVal id As Integer) As Boolean
        Dim _result As Boolean = False
        Dim oDataGridItem As DataGrid
        Dim txtRemark As System.Web.UI.WebControls.TextBox
        Dim oExArgs As New System.Collections.ArrayList
        Dim objPartIncidentalDetail As New PartIncidentalDetail
        Dim objPartIncidentalDetailFacade As New PartIncidentalDetailFacade(User)
        Dim objPartIncidentalHeader As New PartIncidentalHeader
        'Dim status As New PartIncidentalStatus.PartIncidentalKTBStatusEnum
        Dim FacadePartListHeader As New PartIncidentalHeaderFacade(User)

        ' For Each oDataGridItem In dgPartListDetail.Items
        'Dim partInDetailID As Integer = CType(oDataGridItem.FindControl("lblID"), Label).Text

        objPartIncidentalDetail = objPartIncidentalDetailFacade.Retrieve(id)
        If CType(oDataGridItem.FindControl("txtRemark"), TextBox).Text = Nothing Then
            objPartIncidentalDetail.Remark = Nothing
        Else
            objPartIncidentalDetail.Remark = CType(oDataGridItem.FindControl("txtRemark"), TextBox).Text
        End If

        objPartIncidentalDetailFacade.Update(objPartIncidentalDetail)
        ' Next
        'objPartIncidentalHeader = FacadePartListHeader.Retrieve(id)
        'objPartIncidentalHeader.KTBStatus = status.Sedang_Proses
        'FacadePartListHeader.Update(objPartIncidentalHeader)

        _result = True
        Return _result
    End Function

    Private Function UpdateStatus() As Boolean
        Dim _result As Boolean = False
        Dim id As Integer = Request.QueryString("ID")
        Dim objPartIncidentalHeader As New PartIncidentalHeader
        Dim status As New PartIncidentalStatus.PartIncidentalKTBStatusEnum
        Dim FacadePartList As New PartIncidentalHeaderFacade(User)

        objPartIncidentalHeader = FacadePartList.Retrieve(id)

        objPartIncidentalHeader.KTBStatus = status.Selesai
        FacadePartList.Update(objPartIncidentalHeader)
        _result = True
        Return _result
    End Function

#End Region

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Dim id As Integer = Request.QueryString("ID")
        If Not IsPostBack Then
            BindHeaderToForm(id)
            BindDataGrid(id)
        End If
        btnPenyelesaian.Visible = SecurityProvider.Authorize(Context.User, SR.ProcessListPengajuanPermintaanKhususDetail_Privilege)
        btnCetak1.Visible = SecurityProvider.Authorize(Context.User, SR.ProcessListPengajuanPermintaanKhususDetail_Privilege)
        btnPenyelesaian.Attributes.Add("OnClick", "return confirm('Yakin mau melakukan proses?');")
    End Sub



    Private Sub dgPartListDetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPartListDetail.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            Dim RowValue As PartIncidentalDetail = CType(e.Item.DataItem, PartIncidentalDetail)
            Dim isAllocated As Boolean = False
            Dim lblPopUp As Label = CType(e.Item.Cells(6).FindControl("lblPopUp"), Label)

            For Each item As PartIncidentalPO In RowValue.PartIncidentalPOs
                If item.Alocation > 0 Then
                    isAllocated = True
                End If
            Next
            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                e.Item.Cells(2).Text = RowValue.SparePartMaster.PartNumber
                e.Item.Cells(3).Text = RowValue.SparePartMaster.PartName
                e.Item.Cells(4).Text = RowValue.SparePartMaster.ModelCode
                Dim txtBox As TextBox = CType(e.Item.FindControl("txtRemark"), TextBox)
                txtBox.Attributes.Add("onkeypress", "return alphaNumericPlusSpaceUniv(event);")
                Dim txtPartSubstitute As TextBox = CType(e.Item.FindControl("txtPartSubstitute"), TextBox)
                txtPartSubstitute.Attributes.Add("readonly", "readonly")
                Dim txtPlanDate As TextBox = CType(e.Item.FindControl("txtPlanDate"), TextBox)
                Dim cbReject As CheckBox = CType(e.Item.FindControl("cbReject"), CheckBox)
                If RowValue.Reject = -1 Then
                    cbReject.Checked = True
                Else
                    cbReject.Checked = False
                End If

                Dim lbtnSave As LinkButton = CType(e.Item.FindControl("lbtnSave"), LinkButton)
                lbtnSave.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan disimpan?');")
                'lbtnSave.Visible = Not isAllocated
                If isFinish Then
                    lbtnSave.Enabled = False
                    cbReject.Enabled = False
                Else
                    lbtnSave.Enabled = True
                    cbReject.Enabled = True
                End If
                cbReject.Enabled = Not isAllocated
                Dim partSub As SparePartMaster = GetSparpartMaster(RowValue.SparePartMasterSubstitutionID)
                If Not partSub Is Nothing Then
                    txtPartSubstitute.Text = partSub.PartNumber
                Else
                    txtPartSubstitute.Text = ""
                End If
                If RowValue.PlanDate < New Date(1900, 1, 1, 0, 0, 0) Then
                    'txtPlanDate.Text = Now.Day & "/" & Now.Month & "/" & Now.Year
                    txtPlanDate.Text = String.Empty
                Else
                    txtPlanDate.Text = RowValue.PlanDate.Day & "/" & RowValue.PlanDate.Month & "/" & RowValue.PlanDate.Year
                End If

                If RowValue.PartIncidentalHeader.KTBStatus = PartIncidentalStatus.PartIncidentalKTBStatusEnum.Selesai Then
                    txtBox.Enabled = False
                    txtPartSubstitute.Enabled = False
                    txtPlanDate.Enabled = False
                    lblPopUp.Visible = False
                    'lbtnSave.Visible = False
                    cbReject.Visible = False
                End If

                If RowValue.RemainQuantity > 0 Then
                    lbtnSave.Visible = True
                Else
                    lbtnSave.Visible = False
                End If

            End If


            lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpSparePart.aspx?index=" & e.Item.ItemIndex, "", 710, 700, "SparePart")
            'lblPopUp.Attributes("onclick") = "Test()"
        End If
    End Sub

    'Private Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
    '    Dim id As Integer = Request.QueryString("ID")
    '    Select Case ddlProses.SelectedValue
    '        Case 0
    '            'If PopulateRemark(id) Then
    '            'MessageBox.Show(SR.SaveSuccess)
    '            'Else
    '            '    MessageBox.Show(SR.SaveFail)
    '            'End If

    '        Case 1
    '            If UpdateStatus() Then
    '                MessageBox.Show(SR.UpdateSucces)
    '            Else
    '                MessageBox.Show(SR.UploadFail("Status"))
    '            End If
    '    End Select


    'End Sub

    Private Sub btnPenyelesaian_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPenyelesaian.Click
        If UpdateStatus() Then
            MessageBox.Show(SR.UpdateSucces)
            btnPenyelesaian.Enabled = False
            Dim id As Integer = Request.QueryString("ID")
            BindDataGrid(id)
        Else
            MessageBox.Show(SR.UploadFail("Status"))
        End If
    End Sub

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Dim id As Integer = Request.QueryString("ID")
        Response.Redirect("../SparePart/FrmListPartIncidentalKTB.aspx?ID=" & id)
    End Sub

    Private Sub btnCetak1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCetak1.Click
        Dim id As Integer = Request.QueryString("ID")
        Response.Redirect("../Sparepart/FrmPartIncidentalDisplayDocument.aspx?Id=" & id)
    End Sub
    Private falseDate As DateTime = New DateTime(1900, 1, 1, 0, 0, 0)
    Private Function PopulateDate(ByVal _date As String) As DateTime
        Dim dt As String() = _date.Split("/")
        If dt.Length <> 3 Then
            Return falseDate
        End If
        Try
            Dim correctDate As DateTime = New DateTime(dt(2), dt(1), dt(0), 0, 0, 0)
            Return correctDate
        Catch ex As Exception
            Return falseDate
        End Try
    End Function

    Private Function GetSparpartMaster(ByVal code As String) As SparePartMaster
        Dim spMasterFacade As SparePartMasterFacade = New SparePartMasterFacade(User)
        Return spMasterFacade.Retrieve(code)
    End Function

    Private Function GetSparpartMaster(ByVal code As Integer) As SparePartMaster
        Dim spMasterFacade As SparePartMasterFacade = New SparePartMasterFacade(User)
        Return spMasterFacade.Retrieve(code)
    End Function

    Private Sub FinishProsessPO(ByVal objHeader As PartIncidentalHeader)
        Dim isFinished As Boolean = True
        If Not objHeader Is Nothing Then
            For Each item As PartIncidentalDetail In objHeader.PartIncidentalDetails
                If item.Reject <> -1 Then
                    isFinished = False
                    Exit For
                End If
            Next

            If isFinished Then
                objHeader.KTBStatus = PartIncidentalStatus.PartIncidentalKTBStatusEnum.Selesai
                Dim objPOSPFacade As PartIncidentalHeaderFacade = New PartIncidentalHeaderFacade(User)
                objPOSPFacade.Update(objHeader)
            End If
        End If
    End Sub

    Private Sub dgPartListDetail_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgPartListDetail.ItemCommand
        If e.CommandName = "Save" Then
            Dim id As Integer = Request.QueryString("ID")
            If id > 0 Then
                Dim objPartIncidentalDetailFacade As New PartIncidentalDetailFacade(User)
                Dim txtRemark As TextBox = CType(e.Item.FindControl("txtRemark"), TextBox)
                Dim _ID As Label = CType(e.Item.FindControl("lblID"), Label)
                Dim txtPartSubstitute As TextBox = CType(e.Item.FindControl("txtPartSubstitute"), TextBox)
                Dim txtPlanDate As TextBox = CType(e.Item.FindControl("txtPlanDate"), TextBox)
                Dim cbReject As CheckBox = CType(e.Item.FindControl("cbReject"), CheckBox)
                Dim objPartDetail As PartIncidentalDetail = New PartIncidentalDetailFacade(User).Retrieve(CInt(_ID.Text))
                objPartDetail.Remark = txtRemark.Text
                If PopulateDate(txtPlanDate.Text.Trim) = falseDate And cbReject.Checked = False Then
                    MessageBox.Show("Plan Date tidak valid.")
                ElseIf PopulateDate(txtPlanDate.Text.Trim) < PopulateDate(DateTime.Now.ToString("dd/MM/yyyy")) Then
                    MessageBox.Show("Plan Date tidak boleh lebih kecil dari tanggal sekarang.")
                Else
                    Dim spSubstitution As SparePartMaster = GetSparpartMaster(txtPartSubstitute.Text.Trim)
                    objPartDetail.PlanDate = PopulateDate(txtPlanDate.Text.Trim)
                    If cbReject.Checked = True Then
                        objPartDetail.Reject = -1
                    Else
                        objPartDetail.Reject = 0
                    End If
                    If spSubstitution.ID > 0 Then
                        objPartDetail.SparePartMasterSubstitutionID = spSubstitution.ID
                    End If
                    objPartIncidentalDetailFacade.Update(objPartDetail)
                    BindDataGrid(id)
                End If
                FinishProsessPO(objPartDetail.PartIncidentalHeader)
            Else
                MessageBox.Show("Proses Simpan Gagal")
            End If
        End If

    End Sub
#End Region


    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'Todo : Mandatory Date
        Dim id As Integer = Request.QueryString("ID")
        If Not id > 0 Then
            MessageBox.Show("Proses Simpan Gagal")
            Return
        End If

        Dim strMsg As String = ""
        Dim objHeader As PartIncidentalHeader
        Dim Counter As Integer = 0
        Dim IsAnyRowUpdated As Boolean = False

        Dim objPartIncidentalDetailFacade As New PartIncidentalDetailFacade(User)

        For Each itemRow As DataGridItem In dgPartListDetail.Items
            If itemRow.ItemType = ListItemType.AlternatingItem Or itemRow.ItemType = ListItemType.Item Then
                Counter += 1
                Dim txtRemark As TextBox = CType(itemRow.FindControl("txtRemark"), TextBox)
                Dim _ID As Label = CType(itemRow.FindControl("lblID"), Label)
                Dim txtPartSubstitute As TextBox = CType(itemRow.FindControl("txtPartSubstitute"), TextBox)
                Dim txtPlanDate As TextBox = CType(itemRow.FindControl("txtPlanDate"), TextBox)
                Dim cbReject As CheckBox = CType(itemRow.FindControl("cbReject"), CheckBox)
                Dim objPartDetail As PartIncidentalDetail = objPartIncidentalDetailFacade.Retrieve(CInt(_ID.Text))
                objPartDetail.Remark = txtRemark.Text

                If Counter = 1 Then
                    objHeader = objPartDetail.PartIncidentalHeader
                End If

                If txtPlanDate.Text.Trim = "" Then

                ElseIf PopulateDate(txtPlanDate.Text.Trim) = falseDate AndAlso cbReject.Checked = False Then
                    'MessageBox.Show("Plan Date tidak valid.")
                    strMsg &= "Row " & Counter.ToString & " : Plan Date tidak valid.\n"
                ElseIf PopulateDate(txtPlanDate.Text.Trim) <> falseDate And PopulateDate(txtPlanDate.Text.Trim) < PopulateDate(DateTime.Now.ToString("dd/MM/yyyy")) AndAlso PopulateDate(txtPlanDate.Text.Trim) <> objPartDetail.PlanDate Then
                    'MessageBox.Show("Plan Date tidak boleh lebih kecil dari tanggal sekarang.")
                    strMsg &= "Row " & Counter.ToString & " : Plan Date tidak boleh lebih kecil dari tanggal sekarang.\n"
                Else
                    Dim spSubstitution As SparePartMaster = GetSparpartMaster(txtPartSubstitute.Text.Trim)
                    objPartDetail.PlanDate = PopulateDate(txtPlanDate.Text.Trim)
                    If cbReject.Checked = True Then
                        objPartDetail.Reject = -1
                    Else
                        objPartDetail.Reject = 0
                    End If
                    If spSubstitution.ID > 0 Then
                        objPartDetail.SparePartMasterSubstitutionID = spSubstitution.ID
                    End If
                    objPartIncidentalDetailFacade.Update(objPartDetail)
                    IsAnyRowUpdated = True
                End If

            End If
        Next

        If strMsg <> "" Then
            MessageBox.Show(strMsg)
        ElseIf strMsg = "" And IsAnyRowUpdated Then
            MessageBox.Show("Data Sudah Disimpan")
        End If

        'Gantinya Button Cetak
        If IsAnyRowUpdated Then
            Dim FacadePartListHeader As New PartIncidentalHeaderFacade(User)
            Dim status As New PartIncidentalStatus.PartIncidentalKTBStatusEnum
            objHeader.KTBStatus = status.Sedang_Proses
            FacadePartListHeader.Update(objHeader)
            btnPenyelesaian.Enabled = True

        End If
        'End Gantinya Button Cetak

        FinishProsessPO(objHeader)
        BindDataGrid(id)
    End Sub
End Class