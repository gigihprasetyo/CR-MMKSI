#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Claim
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security

#End Region

#Region ".NET Base Class Namespace Imports"

Imports System.IO
Imports System.Text
Imports KTB.DNet.BusinessFacade

#End Region

Public Class FrmClaimReason
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents txtTimeLimit As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPrerequisite As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgClaimReason As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtLevel As System.Web.UI.WebControls.TextBox
    Protected WithEvents panelHeader As System.Web.UI.WebControls.Panel
    Protected WithEvents test As System.Web.UI.HtmlControls.HtmlButton
    Protected WithEvents valsumDetail As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents rfHeadCode As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents rfHeadDesc As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents rfHeadLimit As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents rfHeadSyarat As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents chkMandatory As System.Web.UI.WebControls.CheckBox
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents txtdDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfDetDesc As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents txtdLevel As System.Web.UI.WebControls.TextBox
    Protected WithEvents btndSave As System.Web.UI.WebControls.Button
    Protected WithEvents btndBatal As System.Web.UI.WebControls.Button
    Protected WithEvents dtgdCR As System.Web.UI.WebControls.DataGrid
    Protected WithEvents panelDetail As System.Web.UI.WebControls.Panel
    Protected WithEvents xxx As System.Web.UI.WebControls.Panel
    Protected WithEvents lblMandatory As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents txtIncharge As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfTxtIncharge As System.Web.UI.WebControls.RequiredFieldValidator

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
    Private objClaimReason As ClaimReason
    Private arlClaimReason As ArrayList
    Dim sHelper As New SessionHelper
#End Region

#Region "CustomMethod"

    Private Sub InitiatePage()
        ClearData()
        ViewState("CurrentSortColumn") = "Code"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub bindCRstatus(ByVal ddL As DropDownList)
        Dim arl As ArrayList = New EnumCategoryStatus().RetrieveStatus()

        ddL.Items.Insert(0, New ListItem("Silahkan Pilih", "0"))
        ddL.DataSource = arl
        ddL.DataTextField = "NameStatus"
        ddL.DataValueField = "ValStatus"
        ddL.DataBind()

    End Sub

    Private Sub ClearData()
        txtCode.Text() = String.Empty
        txtDescription.Text() = String.Empty
        txtTimeLimit.Text = String.Empty
        txtPrerequisite.Text = String.Empty
        txtIncharge.Text = String.Empty
        ddlStatus.SelectedIndex = -1
        txtLevel.Text = "Header" 
        Datagrid1.DataSource = New ArrayList
        Datagrid1.DataBind()
        btnSimpan.Enabled = CekBtnPriv()
        txtCode.ReadOnly = False
        txtDescription.ReadOnly = False
        chkMandatory.Checked = False
        SetLabelMandatory()
        'txtdCode.Text() = String.Empty
        'txtdDescription.Text() = String.Empty
        'txtdTimeLimit.Text = String.Empty
        'txtdPrerequisite.Text = String.Empty
        'ddldStatus.SelectedIndex = -1
        'txtdLevel.Text = "Item"
        'btndSave.Enabled = True
        'txtdCode.ReadOnly = False
        'txtdDescription.ReadOnly = False
        ViewState.Add("vsProcess", "Insert")
        Session("TipeBukti") = New ArrayList
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer, ByVal dtg As DataGrid)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            dtg.SelectedIndex = -1
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimReason), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            If dtg.ID = "dtgClaimReason" Then
                criterias.opAnd(New Criteria(GetType(ClaimReason), "IsHeader", MatchType.Exact, "1"))
            Else
                criterias.opAnd(New Criteria(GetType(ClaimReason), "IsHeader", MatchType.Exact, "0"))
            End If
            arlClaimReason = New ClaimReasonFacade(User).RetrieveActiveList(criterias, indexPage + 1, dtgClaimReason.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
                CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

            dtg.DataSource = arlClaimReason
            dtg.VirtualItemCount = totalRow
            dtg.DataBind()
        End If
    End Sub

    Private Sub UpdateClaimReason(ByVal dtg As DataGrid, ByVal level As Boolean)
        Dim objCReason As ClaimReason = CType(sHelper.GetSession("ClaimReason"), ClaimReason)
        Dim objCReasonFacade As ClaimReasonFacade = New ClaimReasonFacade(User)
        If level Then
            objCReason.Code = txtCode.Text
            objCReason.Reason = txtDescription.Text
            objCReason.TimeLimit = CInt(txtTimeLimit.Text)
            objCReason.Prerequisite = txtPrerequisite.Text
            objCReason.incharge = txtIncharge.Text
            objCReason.Status = ddlStatus.SelectedValue
            objCReason.IsHeader = 1
            If chkMandatory.Checked Then
                objCReason.IsMandatoryUpload = 1
            Else
                objCReason.IsMandatoryUpload = 0
            End If
        Else
            'objCReason.Code = txtdCode.Text
            'objCReason.Reason = txtdDescription.Text
            'objCReason.TimeLimit = txtdTimeLimit.Text
            'objCReason.Prerequisite = txtdPrerequisite.Text
            'objCReason.Status = ddldStatus.SelectedValue
            objCReason.IsHeader = 0
        End If
        Dim nResult As Integer = -1
        Try
            nResult = New ClaimReasonFacade(User).Update(objCReason)   '-- Update Data To Database
            MessageBox.Show(SR.UpdateSucces)
            ClearData()
            BindDataGrid(0, dtg)
        Catch ex As Exception
            MessageBox.Show(SR.UpdateFail)
        End Try
    End Sub

    Private Sub ViewClaim(ByVal nID As Integer, ByVal EditStatus As Boolean, ByVal level As Boolean)
        Dim objCReason As ClaimReason = New ClaimReasonFacade(User).Retrieve(nID)
        If Not IsNothing(objCReason) Then
            'Todo session
            'Session.Add("ClaimReason", objCReason)
            sHelper.SetSession("ClaimReason", objCReason)
            If level Then
                txtCode.Text = objCReason.Code
                txtDescription.Text = objCReason.Reason
                txtTimeLimit.Text = objCReason.TimeLimit
                txtPrerequisite.Text = objCReason.Prerequisite
                txtIncharge.Text = objCReason.incharge
                ddlStatus.SelectedValue = objCReason.Status
                If objCReason.IsMandatoryUpload = 1 Then
                    chkMandatory.Checked = True
                Else
                    chkMandatory.Checked = False
                End If
                SetLabelMandatory()
            Else
                'txtdCode.Text = objCReason.Code
                'txtdDescription.Text = objCReason.Reason
                'txtdTimeLimit.Text = objCReason.TimeLimit
                'txtdPrerequisite.Text = objCReason.Prerequisite
                'ddldStatus.SelectedValue = objCReason.Status
            End If
        Else
            If EditStatus Then
                MessageBox.Show(SR.UpdateFail)
            Else
                MessageBox.Show(SR.ViewFail)
            End If
            ClearData()
        End If
    End Sub

    Private Function CekTransaction(ByVal claimReasonId As Integer) As Integer
        Dim nresult As Integer = 1
        Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crits.opAnd(New Criteria(GetType(ClaimHeader), "ClaimReason.ID", MatchType.Exact, claimReasonId))
        Dim arlcek As ArrayList = New ClaimHeaderFacade(User).Retrieve(crits)
        If Not arlcek Is Nothing Then
            If arlcek.Count > 0 Then
                nresult = -1
            End If
        End If
        Return nresult
    End Function

    Private Sub DeleteClaim(ByVal nID As Integer)
        Dim objCReason As ClaimReason = New ClaimReasonFacade(User).Retrieve(nID)
        Try
            If CekTransaction(nID) = -1 Then
                'MessageBox.Show(SR.CannotDelete)
                MessageBox.Show("Kode Alasan Claim no: " & objCReason.Code & " sudah digunakan dalam transaksi, tidak bisa dihapus")
            Else
                Dim facade As ClaimReasonFacade = New ClaimReasonFacade(User)
                facade.DeleteFromDB(objCReason)
                MessageBox.Show(SR.DeleteSucces)
                BindDataGrid(0, dtgClaimReason)
                BindDataGrid(0, dtgdCR)
            End If
            'If objCReason.ClaimDetails.Count > 0 Then
            '    MessageBox.Show(SR.CannotDelete)
            'Else
            '    Dim facade As ClaimReasonFacade = New ClaimReasonFacade(User)
            '    facade.DeleteFromDB(objCReason)
            '    MessageBox.Show(SR.DeleteSucces)
            '    BindDataGrid(0, dtgClaimReason)
            '    BindDataGrid(0, dtgdCR)
            'End If
        Catch ex As Exception
            MessageBox.Show(SR.DeleteFail)
            ClearData()
        End Try
    End Sub

    Private Sub simpandata(ByVal level As Boolean)
        'If Not Page.IsValid Then
        '    Return
        'End If
        Dim objCReason As ClaimReason = New ClaimReason
        Dim objBReasonFacade As ClaimReasonFacade = New ClaimReasonFacade(User)
        Dim nResult As Integer = -1
        If CType(ViewState("vsProcess"), String) = "Insert" Then  '-- If Condition is Insert
            If level Then
                If objBReasonFacade.ValidateCode(txtCode.Text, 0) = 0 Then
                    objCReason.Code = txtCode.Text
                    objCReason.Reason = txtDescription.Text
                    objCReason.TimeLimit = CInt(txtTimeLimit.Text)
                    objCReason.Prerequisite = txtPrerequisite.Text
                    objCReason.incharge = txtIncharge.Text
                    objCReason.Status = ddlStatus.SelectedValue
                    objCReason.IsHeader = 1
                    If chkMandatory.Checked Then
                        objCReason.IsMandatoryUpload = 1
                    Else
                        objCReason.IsMandatoryUpload = 0
                    End If

                    If (New ClaimReasonFacade(User).InsertTransaction(objCReason) <> -1) Then
                        MessageBox.Show(SR.SaveSuccess)
                        ControlLock(True, "ALL")
                    Else
                        MessageBox.Show(SR.SaveFail)
                        Return
                    End If
                Else
                    MessageBox.Show(SR.DataIsExist("Kategori"))
                    Return
                End If
            Else
                If objBReasonFacade.ValidateReason(txtdDescription.Text) = 0 Then
                    'objCReason.Code = txtdCode.Text
                    objCReason.Reason = txtdDescription.Text
                    'objCReason.TimeLimit = txtdTimeLimit.Text
                    'objCReason.Prerequisite = txtdPrerequisite.Text
                    'objCReason.Status = ddldStatus.SelectedValue
                    objCReason.IsHeader = 0
                    Try
                        nResult = New ClaimReasonFacade(User).Insert(objCReason)
                        If nResult <> -1 Then
                            MessageBox.Show(SR.SaveSuccess)
                            ControlLock(True, "ALL")
                            insertTipeBukti(nResult)
                        Else
                            MessageBox.Show(SR.SaveFail)
                            Return
                        End If
                    Catch ex As Exception
                        MessageBox.Show(SR.SaveFail)
                        Return
                    End Try
                Else
                    MessageBox.Show(SR.DataIsExist("Detail - Deskripsi"))
                    Return
                End If
            End If
        Else
            If level Then
                objCReason = CType(sHelper.GetSession("ClaimReason"), ClaimReason)
                'If objBReasonFacade.ValidateCode(txtCode.Text, objCReason.ID) = 0 Then
                For Each item As AttchPramClaimReason In GetTipeBukti(objCReason, True)
                    item.RowStatus = CType(DBRowStatus.Deleted, Short)
                    Dim upd = New AttchPramClaimReasonFacade(User).Update(item)
                Next
                insertTipeBukti(objCReason.ID) 
                UpdateClaimReason(dtgClaimReason, level) '-- Update Change
                'Else
                '    MessageBox.Show(SR.DataIsExist("Kategori"))
                '    Return
                'End If
            Else
                UpdateClaimReason(dtgdCR, level)  '-- Update Change
            End If
        End If
        ClearData()
        BindDataGrid(dtgClaimReason.CurrentPageIndex, dtgClaimReason)
        BindDataGrid(dtgdCR.CurrentPageIndex, dtgdCR)
    End Sub


    Private Sub ControlLock(ByVal bval As Boolean, ByVal level As String)
        If level = "ALL" Then
            txtCode.ReadOnly = Not bval
            txtDescription.ReadOnly = Not bval
            txtTimeLimit.ReadOnly = Not bval
            txtPrerequisite.ReadOnly = Not bval
            txtIncharge.ReadOnly = Not bval
            ddlStatus.Enabled = bval
            chkMandatory.Enabled = bval
            If bval = True Then
                btnSimpan.Enabled = CekBtnPriv()
            Else
                btnSimpan.Enabled = bval
            End If

            btnBatal.Enabled = bval
            dtgClaimReason.Enabled = bval
            dtgClaimReason.Columns(dtgClaimReason.Columns.Count - 1).Visible = bval
            'txtdCode.ReadOnly = Not bval
            txtdDescription.ReadOnly = Not bval
            'txtdTimeLimit.ReadOnly = Not bval
            'txtdPrerequisite.ReadOnly = Not bval
            'ddldStatus.Enabled = bval
            btndSave.Enabled = bval
            btndBatal.Enabled = bval
            dtgdCR.Enabled = bval
            dtgdCR.Columns(dtgdCR.Columns.Count - 1).Visible = bval
        End If
        If level = "D" Then
            txtCode.ReadOnly = Not bval
            txtDescription.ReadOnly = Not bval
            txtTimeLimit.ReadOnly = Not bval
            txtPrerequisite.ReadOnly = Not bval
            txtIncharge.ReadOnly = Not bval
            ddlStatus.Enabled = bval
            If bval = True Then
                btnSimpan.Enabled = CekBtnPriv()
            Else
                btnSimpan.Enabled = bval
            End If

            btnBatal.Enabled = bval
            dtgClaimReason.Enabled = bval
            dtgClaimReason.Columns(dtgClaimReason.Columns.Count - 1).Visible = bval
        ElseIf level = "H" Then
            'txtdCode.ReadOnly = Not bval
            txtdDescription.ReadOnly = Not bval
            'txtdTimeLimit.ReadOnly = Not bval
            'txtdPrerequisite.ReadOnly = Not bval
            'ddldStatus.Enabled = bval
            btndSave.Enabled = bval
            btndBatal.Enabled = bval
            dtgdCR.Enabled = bval
            dtgdCR.Columns(dtgdCR.Columns.Count - 1).Visible = bval
        End If
    End Sub

    Private Sub ResetControl(ByVal bval As Boolean)
        txtCode.ReadOnly = Not bval
        txtDescription.ReadOnly = Not bval
        txtTimeLimit.ReadOnly = Not bval
        txtPrerequisite.ReadOnly = Not bval
        txtIncharge.ReadOnly = Not bval
        ddlStatus.Enabled = bval
        If bval = True Then
            btnSimpan.Enabled = CekBtnPriv()
        Else
            btnSimpan.Enabled = bval
        End If

        btnBatal.Enabled = bval
        dtgClaimReason.Enabled = bval
        dtgClaimReason.Columns(dtgClaimReason.Columns.Count - 1).Visible = bval
    End Sub

#End Region

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.ClaimReasonView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=CLAIM - Alasan Claim")
        End If
    End Sub

    Private Function CekBtnPriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.ClaimReasonCreate_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

#Region "datagrid1"
    Protected WithEvents Datagrid1 As DataGrid
    Protected Sub Datagrid1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Datagrid1.SelectedIndexChanged

    End Sub
    Protected Sub Datagrid1_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles Datagrid1.ItemDataBound
        Select Case e.Item.ItemType
            Case ListItemType.Item, ListItemType.AlternatingItem
                Dim spSupp As SPSupportClaimDoc = CType(e.Item.DataItem, SPSupportClaimDoc)
                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
                lblID.Text = spSupp.ID
                lblNo.Text = spSupp.DocumentName
            Case ListItemType.Footer
                Dim ddlDocumentType As DropDownList = CType(e.Item.FindControl("ddlDocumentType"), DropDownList)
                BindDocumentType(ddlDocumentType)
        End Select
    End Sub
    Private Sub insertTipeBukti(nResult As Integer)
        Dim oClReason As ClaimReason = New ClaimReasonFacade(User).Retrieve(nResult)
        Dim arl As ArrayList = CType(Session("TipeBukti"), ArrayList)
        For Each item As SPSupportClaimDoc In arl
            Dim oAttchPramClaimReason As New AttchPramClaimReason
            oAttchPramClaimReason.ClaimReason = oClReason
            oAttchPramClaimReason.SPSupportClaimDoc = item
            Dim result As Integer = 0
            result = New AttchPramClaimReasonFacade(User).Insert(oAttchPramClaimReason)
            If result <= 0 Then
                MessageBox.Show("Terjadi kesalahan segera hubungi KTB")
            End If
        Next
    End Sub
    Protected Sub Datagrid1_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles Datagrid1.ItemCommand
        Dim arl As ArrayList = CType(Session("TipeBukti"), ArrayList)
        Select Case e.CommandName
            Case "Add"
                Dim ddlDocumentType As DropDownList = CType(e.Item.FindControl("ddlDocumentType"), DropDownList)
                If ddlDocumentType.SelectedIndex = 0 Then
                    MessageBox.Show("Silahkan Pilih Tipe Bukti")
                Else
                    Dim spSU As SPSupportClaimDoc = New SPSupportClaimDocFacade(User).Retrieve(CType(ddlDocumentType.SelectedValue, Integer))
                    If arlAda(arl, spSU) Then
                        MessageBox.Show("Tipe Bukti sudah ada")
                    Else
                        arl.Add(spSU)
                        Session("TipeBukti") = arl
                        BindGridTipeBukti()
                    End If
                End If
            Case "Delete"
                arl.RemoveAt(e.Item.ItemIndex)
                Session("TipeBukti") = arl
                BindGridTipeBukti()
        End Select
    End Sub
    Sub BindGridTipeBukti()
        Datagrid1.DataSource = CType(Session("TipeBukti"), ArrayList)
        Datagrid1.DataBind()
    End Sub
    Private Function arlAda(arl As ArrayList, spSU As SPSupportClaimDoc) As Boolean
        For Each item As SPSupportClaimDoc In arl
            If item.ID = spSU.ID Then
                Return True
            Else
                Return False
            End If
        Next
    End Function
    Private Sub BindDocumentType(ddlDocumentType As DropDownList)
        With ddlDocumentType.Items
            .Clear()
            .Add(New ListItem("Silahkan Pilih", -1))
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPSupportClaimDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(SPSupportClaimDoc), "Status", MatchType.Exact, 1))
            Dim arl As ArrayList = New SPSupportClaimDocFacade(User).Retrieve(crit)
            For Each spSup As SPSupportClaimDoc In arl
                .Add(New ListItem(spSup.DocumentName, spSup.ID))
            Next
        End With
    End Sub
#End Region

#Region "Event Hendlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()
        If Not IsPostBack Then
            InitiatePage()
            bindCRstatus(ddlStatus)
            'bindCRstatus(ddldStatus)
            BindDataGrid(0, dtgClaimReason)
            BindDataGrid(0, dtgdCR)
            btnSimpan.Attributes.Add("onclick", "enableHeader();")
        End If
        btnSimpan.Enabled = CekBtnPriv()
        SetLabelMandatory()
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        simpandata(True)
    End Sub

    Private Sub btndSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        simpandata(False)
    End Sub

    Private Sub btnBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        'ControlLock(True, "H")
        ResetControl(True)
        ClearData() '-- Refresh UI
        BindDataGrid(0, dtgClaimReason)
    End Sub

    Private Sub btndBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ControlLock(True, "D")
        ClearData() '-- Refresh UI
        BindDataGrid(0, dtgdCR)
    End Sub

    Private Sub dtgClaimReason_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgClaimReason.SortCommand
        If dtgClaimReason.Enabled Then
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
            ClearData()
            BindDataGrid(0, dtgClaimReason)
        End If
    End Sub

    Private Sub dtgClaimReason_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgClaimReason.PageIndexChanged
        dtgClaimReason.SelectedIndex = -1
        dtgClaimReason.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgClaimReason.CurrentPageIndex, dtgClaimReason)
        ClearData()
    End Sub

    Private Sub dtgClaimReason_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgClaimReason.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
                Dim _lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                Dim lblFileLampiran As Label = CType(e.Item.FindControl("lblFileLampiran"), Label)
                _lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")

                Dim _lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                _lblNo.Text = e.Item.ItemIndex + 1 + (dtgClaimReason.CurrentPageIndex * dtgClaimReason.PageSize)

                Dim lblMandatory As Label = e.Item.FindControl("lblMandatory")
                Dim objreason As ClaimReason = e.Item.DataItem

                If objreason.IsMandatoryUpload = 0 Then
                    lblMandatory.Text = "Tidak"
                ElseIf objreason.IsMandatoryUpload = 1 Then
                    lblMandatory.Text = "Ya"
                End If
                Dim _lbtnView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
                Dim _lbtnUbah As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
                Dim att As ArrayList = New AttchPramClaimReasonFacade(User).Retrieve(objreason)
                Dim strDoc As String = ""
                For Each oAttchPramClaimReason As AttchPramClaimReason In att
                    If strDoc.Length = 0 Then
                        strDoc = oAttchPramClaimReason.SPSupportClaimDoc.DocumentName
                    Else
                        strDoc = strDoc & ", " & oAttchPramClaimReason.SPSupportClaimDoc.DocumentName
                    End If
                Next
                lblFileLampiran.Text = strDoc

                If CekBtnPriv() Then
                    _lbtnDelete.Visible = True
                    _lbtnView.Visible = True
                    _lbtnUbah.Visible = True
                Else
                    _lbtnDelete.Visible = False
                    _lbtnView.Visible = False
                    _lbtnUbah.Visible = False
                End If

                Dim lblHistoryStatus As Label = e.Item.FindControl("lblHistoryStatus")
                Dim sUrl As String = "../Popup/PopupDataHistory.aspx?TableName=ClaimReason&TableID=" & objreason.ID.ToString
                sUrl = "ShowDataHistory('" & sUrl & "');"
                lblHistoryStatus.Attributes.Add("OnClick", sUrl)
            End If
        End If
    End Sub
    Private Function GetTipeBukti(ByVal oClaimReason As ClaimReason, Optional ByVal bwtSimpan As Boolean = False) As ArrayList
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AttchPramClaimReason), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(AttchPramClaimReason), "ClaimReason.ID", MatchType.Exact, oClaimReason.ID))
        Dim arlAttch As ArrayList = New AttchPramClaimReasonFacade(User).Retrieve(crit)
        Dim arlSpSupp As ArrayList = New ArrayList
        For Each item As AttchPramClaimReason In arlAttch
            Dim spSupp As SPSupportClaimDoc = New SPSupportClaimDocFacade(User).Retrieve(item.SPSupportClaimDoc.ID)
            arlSpSupp.Add(spSupp)
        Next
        If Not bwtSimpan Then
            Session("TipeBukti") = arlSpSupp
        End If
        Return arlAttch
    End Function
    Private Sub dtgClaimReason_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgClaimReason.ItemCommand
        If (e.CommandName = "View") Then
            dtgClaimReason.SelectedIndex = e.Item.ItemIndex '-- View Condition
            ViewState.Add("vsProcess", "View")
            ViewClaim(e.Item.Cells(0).Text, False, True)
            txtCode.ReadOnly = True
            txtDescription.ReadOnly = True
            txtTimeLimit.ReadOnly = True
            txtPrerequisite.ReadOnly = True
            txtIncharge.ReadOnly = True
            ddlStatus.Enabled = False
            btnSimpan.Enabled = False
            btndSave.Enabled = False 
            Dim claimReason As ClaimReason = New ClaimReasonFacade(User).Retrieve(CType(e.Item.Cells(0).Text, Integer))
            GetTipeBukti(claimReason)
            BindGridTipeBukti()
            Datagrid1.Enabled = False
            ControlLock(False, "H")

        ElseIf e.CommandName = "Edit" Then           '-- Edit/Update Condition
            ViewState.Add("vsProcess", "Edit")
            dtgClaimReason.SelectedIndex = e.Item.ItemIndex
            ViewClaim(e.Item.Cells(0).Text, True, True)
            txtCode.ReadOnly = False
            txtDescription.ReadOnly = False
            txtTimeLimit.ReadOnly = False
            txtPrerequisite.ReadOnly = False
            txtIncharge.ReadOnly = False
            ddlStatus.Enabled = True
            btnSimpan.Enabled = CekBtnPriv()
            ControlLock(False, "H")
            Dim claimReason As ClaimReason = New ClaimReasonFacade(User).Retrieve(CType(e.Item.Cells(0).Text, Integer))
            GetTipeBukti(claimReason)
            BindGridTipeBukti()
            Datagrid1.Enabled = True

        ElseIf e.CommandName = "Delete" Then         '-- Delete Permanentely Conditon
            viewstate.Add("ClaimReasonID", e.Item.Cells(0).Text)
            DeleteClaim(e.Item.Cells(0).Text)
            ClearData()
            btndBatal.Enabled = True
            dtgdCR.Enabled = True
        End If
    End Sub

    Private Sub dtgdCR_SortCommand1(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs)
        If dtgdCR.Enabled Then
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
            ClearData()
            BindDataGrid(0, dtgdCR)
        End If
    End Sub

    Private Sub dtgdCR_ItemCommand1(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        If (e.CommandName = "View") Then             '-- View Condition
            ViewState.Add("vsProcess", "Viewd")
            dtgdCR.SelectedIndex = e.Item.ItemIndex
            ViewClaim(e.Item.Cells(0).Text, False, False)
            'txtdCode.ReadOnly = True
            txtdDescription.ReadOnly = True
            'txtdTimeLimit.ReadOnly = True
            'txtdPrerequisite.ReadOnly = True
            'ddldStatus.Enabled = False
            btndSave.Enabled = False
            ControlLock(False, "D")
        ElseIf e.CommandName = "Edit" Then           '-- Edit/Update Condition
            ViewState.Add("vsProcess", "Editd")
            ViewClaim(e.Item.Cells(0).Text, True, False)
            dtgdCR.SelectedIndex = e.Item.ItemIndex
            'txtdCode.ReadOnly = False
            txtdDescription.ReadOnly = False
            'txtdTimeLimit.ReadOnly = False
            'txtdPrerequisite.ReadOnly = False
            'ddldStatus.Enabled = True
            btndSave.Enabled = True
            ControlLock(False, "D")
        ElseIf e.CommandName = "Delete" Then         '-- Delete Permanentely Conditon
            DeleteClaim(e.Item.Cells(0).Text)
            ClearData()
        End If
    End Sub

    Private Sub dtgdCR_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        If e.Item.ItemIndex <> -1 Then
            Dim _lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDeleted"), LinkButton)
            _lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")

            Dim _lblNo As Label = CType(e.Item.FindControl("lblNoDetail"), Label)
            _lblNo.Text = e.Item.ItemIndex + 1 + (dtgClaimReason.CurrentPageIndex * dtgClaimReason.PageSize)

        End If
    End Sub

    Private Sub dtgdCR_PageIndexChanged1(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs)
        dtgdCR.SelectedIndex = -1
        dtgdCR.CurrentPageIndex = e.NewPageIndex
        ClearData()
    End Sub

    Private Sub SetLabelMandatory()
        If chkMandatory.Checked Then
            lblMandatory.Text = "Ya"
        Else
            lblMandatory.Text = "Tidak"
        End If
    End Sub
#End Region
End Class
