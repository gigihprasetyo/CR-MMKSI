Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security

Public Class FrmTrTrainee
    Inherits System.Web.UI.Page
    Protected WithEvents photoView As System.Web.UI.WebControls.Image
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Requiredfieldvalidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Requiredfieldvalidator4 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ddlShirtSize As System.Web.UI.WebControls.DropDownList
    Protected WithEvents cbDeletePhoto As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtSearchDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSearchNoReg As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtSearchTraineeName As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents ddlSearchStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblSearchJobPos As System.Web.UI.WebControls.Label
    Protected WithEvents ddlRegion As System.Web.UI.WebControls.DropDownList
    Dim sHTrainee As SessionHelper = New SessionHelper


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Requiredfieldvalidator6 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents Requiredfieldvalidator5 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
    Protected WithEvents ICStartWork As Intimedia.WebCC.IntiCalendar
    Protected WithEvents txtJobPosition As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEducationLevel As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents dtgTrainee As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents photoSrc As System.Web.UI.HtmlControls.HtmlInputFile

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Private bPrivilegeChangeTrainee As Boolean = False

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region



    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ActivateUserPrivilege()
        If Not IsPostBack Then
            lblPopUpDealer.Attributes.Add("onClick", "ShowPPDealerSelection();")
            lblSearchJobPos.Attributes("onclick") = "ShowJobPosSelection()"
            InitiatePage()
            BindDdlRegion()
            SetControlPrivilege()
            BindDataGrid(0)
        End If
        AssignControlAttribute()
    End Sub
    Private Sub AssignControlAttribute()
        btnSimpan.Attributes.Add("onclick", New CommonFunction().PreventDoubleClick(btnSimpan))
    End Sub

    Private Sub ActivateUserPrivilege()
        bPrivilegeChangeTrainee = SecurityProvider.Authorize(Context.User, SR.TrainingUpdateSiswa_Privilege)
        Dim objDealer As Dealer = CType(sHTrainee.GetSession("DEALER"), Dealer)
        If Not IsNothing(objDealer) Then
            If Not SecurityProvider.Authorize(Context.User, SR.TrainingViewSiswa_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Training - Form Siswa")
            End If
        End If
    End Sub
    Private Sub SetControlPrivilege()
        btnSimpan.Visible = bPrivilegeChangeTrainee
        btnBatal.Visible = bPrivilegeChangeTrainee
    End Sub
    Private Sub InitiatePage()
        ClearData()
        ViewState("CurrentSortColumn") = "Name"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        Dim objEnumTrTrainee = New EnumTrTrainee
        Dim TraineeStatus As ArrayList = objEnumTrTrainee.RetrieveStatus()
        Dim lItem As ListItem
        For Each ts As EnumTrTraineeData In TraineeStatus
            lItem = New ListItem(ts.NameTitle, ts.ValTitle.ToString)
            ddlStatus.Items.Add(lItem)
            ddlSearchStatus.Items.Add(lItem)
        Next

        ddlStatus.SelectedValue = 1

        lItem = New ListItem("Silahkan pilih", "")
        ddlSearchStatus.Items.Insert(0, lItem)

        Dim ShirtSize As ArrayList = objEnumTrTrainee.RetrieveSize()
        For Each es As EnumShirtData In ShirtSize
            lItem = New ListItem(es.NameTitle, es.ValTitle.ToString)
            ddlShirtSize.Items.Add(lItem)
        Next

        Dim li As ListItem = New ListItem(String.Empty, String.Empty)
        'ddlStatus.Items.Insert(0, li)
        'ddlShirtSize.Items.Insert(0, li)
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then
            Try
                Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTrainee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                If txtSearchDealerCode.Text <> String.Empty Then
                    critCol.opAnd(New Criteria(GetType(TrTrainee), "Dealer.DealerCode", MatchType.Exact, txtSearchDealerCode.Text))
                End If
                If txtSearchNoReg.Text <> String.Empty Then
                    critCol.opAnd(New Criteria(GetType(TrTrainee), "ID", MatchType.Exact, CInt(txtSearchNoReg.Text)))
                End If
                If txtSearchTraineeName.Text <> String.Empty Then
                    critCol.opAnd(New Criteria(GetType(TrTrainee), "Name", MatchType.Partial, txtSearchTraineeName.Text))
                End If
                If ddlSearchStatus.SelectedIndex <> 0 Then
                    critCol.opAnd(New Criteria(GetType(TrTrainee), "Status", MatchType.Exact, ddlSearchStatus.SelectedValue.ToString))
                End If
                If txtJobPosition.Text.Length <> 0 Then
                    critCol.opAnd(New Criteria(GetType(TrTrainee), "JobPosition", MatchType.Partial, txtJobPosition.Text.Trim()))
                End If
                If Not ddlRegion.SelectedValue = "-1" Then
                    critCol.opAnd(New Criteria(GetType(TrTrainee), "Dealer.Area2.ID", MatchType.Exact, CInt(ddlRegion.SelectedValue)))
                End If

                dtgTrainee.DataSource = New TrTraineeFacade(User).RetrieveActiveList(critCol, _
                    indexPage + 1, dtgTrainee.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
                    CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
                dtgTrainee.VirtualItemCount = totalRow
                dtgTrainee.DataBind()
            Catch
                MessageBox.Show("Harap periksa kategori pencarian anda")
            End Try
        End If
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer, ByVal defaultStatus As Integer)
        Dim totalRow As Integer = 0

        If (indexPage >= 0) Then
            Try
                Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTrainee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                If txtSearchDealerCode.Text <> String.Empty Then
                    critCol.opAnd(New Criteria(GetType(TrTrainee), "Dealer.DealerCode", MatchType.Exact, txtSearchDealerCode.Text))
                End If
                If txtSearchNoReg.Text <> String.Empty Then
                    critCol.opAnd(New Criteria(GetType(TrTrainee), "ID", MatchType.Exact, CInt(txtSearchNoReg.Text)))
                End If
                If txtSearchTraineeName.Text <> String.Empty Then
                    critCol.opAnd(New Criteria(GetType(TrTrainee), "Name", MatchType.Exact, txtSearchTraineeName.Text))
                End If

                critCol.opAnd(New Criteria(GetType(TrTrainee), "Status", MatchType.Exact, EnumTrTrainee.TrTraineeStatus.Active))


                dtgTrainee.DataSource = New TrTraineeFacade(User).RetrieveActiveList(critCol, _
                    indexPage + 1, dtgTrainee.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
                    CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
                dtgTrainee.VirtualItemCount = totalRow
                dtgTrainee.DataBind()
            Catch
                MessageBox.Show("Harap periksa kategori pencarian anda")
            End Try
        End If
    End Sub

    Private Sub BindDdlRegion()
        ddlRegion.Items.Clear()
        Dim arlRegion As ArrayList = New Area2Facade(User).RetrieveActiveList()
        Dim objRegion As New Area2
        ddlRegion.Items.Add(New ListItem("Semua Region", "-1"))
        For i As Integer = 0 To arlRegion.Count - 1
            objRegion = CType(arlRegion(i), Area2)
            ddlRegion.Items.Add(New ListItem(objRegion.AreaCode, objRegion.ID))
        Next i
    End Sub

    Private Sub ClearData()
        sHTrainee.RemoveSession("objTrainee")

        Me.txtName.ReadOnly = False
        Me.txtDealerCode.ReadOnly = False
        Me.ICStartWork.Enabled = True
        Me.txtJobPosition.ReadOnly = False
        Me.txtEducationLevel.ReadOnly = False
        Me.photoSrc.Disabled = False
        Me.ddlShirtSize.Enabled = True
        Me.ddlStatus.Enabled = True
        Me.lblPopUpDealer.Enabled = True

        Me.txtName.Text = String.Empty
        Me.txtDealerCode.Text = String.Empty
        Me.ICStartWork.Value = Today
        'Me.txtJobPosition.Text = String.Empty
        Me.txtEducationLevel.Text = String.Empty
        Me.photoSrc.Attributes("value") = String.Empty
        Me.ddlShirtSize.SelectedIndex = 0
        Me.ddlStatus.SelectedIndex = 0
        Me.photoView.ImageUrl = "../WebResources/GetPhoto.aspx"

        btnSimpan.Enabled = True
        dtgTrainee.SelectedIndex = -1
        ViewState.Add("vsProcess", "Insert")
        cbDeletePhoto.Visible = False
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not IsFormValid() Then
            MessageBox.Show("Data masih kosong")
            Return
        End If

        If Not IsUnhack() Then
            MessageBox.Show("< dan > bukan karakter valid")
            Return
        End If

        Try
            If CType(ViewState("vsProcess"), String) = "Insert" Then
                InsertTrainee()
            ElseIf CType(ViewState("vsProcess"), String) = "Edit" Then
                UpdateTrainee()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        dtgTrainee.CurrentPageIndex = 0
        BindDataGrid(dtgTrainee.CurrentPageIndex)
    End Sub

    Private Sub InsertTrainee()
        Dim objTrainee As TrTrainee = New TrTrainee
        Dim objTraineeFacade As TrTraineeFacade = New TrTraineeFacade(User)
        Dim nResult = -1
        Dim imageFile As Byte()

        Try
            imageFile = UploadFile()

            If Not IsDealerExist(Me.txtDealerCode.Text) Then
                Throw New DataException(SR.DataNotFound("Kode Dealer"))
            End If

            FillingObjectFromForm(objTrainee)
            objTrainee.Photo = imageFile

            If IsTraineeExist(objTraineeFacade, objTrainee) Then
                Throw New Exception(SR.DataIsExist("Siswa"))
            End If

            nResult = objTraineeFacade.Insert(objTrainee)
            If nResult = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)
                ClearData()
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function IsTraineeExist(ByVal objFacade As TrTraineeFacade, ByVal objDomain As TrTrainee) As Boolean
        Return objFacade.ValidateTrainee(objDomain) > 0
    End Function

    Private Function IsFieldValidationUpdated(ByVal objDomain As TrTrainee) As Boolean
        Dim objDomainComparer As TrTrainee = New TrTraineeFacade(User).Retrieve(objDomain.ID)
        If IsNothing(objDomainComparer) Or objDomainComparer.ID = 0 Then
            Throw New Exception(SR.DataNotFound("Siswa"))
        End If
        Return objDomain.Name <> objDomainComparer.Name _
        Or objDomain.StartWorkingDate <> objDomainComparer.StartWorkingDate _
        Or objDomain.Dealer.ID <> objDomain.Dealer.ID
    End Function

    Private Function RegOfTraineeCriteria(ByVal TraineeID As Integer) As CriteriaComposite
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrTrainee.ID", MatchType.Exact, TraineeID))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "Status", MatchType.Exact, CType(EnumTrClassRegistration.DataStatusType.Register, String)))
        Return criterias
    End Function

    Private Function GetRegOfTrainee(ByVal objTrainee As TrTrainee) As ArrayList
        Dim objRegFacade As New TrClassRegistrationFacade(User)
        Return objRegFacade.Retrieve(RegOfTraineeCriteria(objTrainee.ID))
    End Function

    Private Function BuildStringOfRegClassColl(ByVal RegClassColl As ArrayList) As String
        Dim strClassColl As String = String.Empty
        For x As Integer = 0 To RegClassColl.Count - 1
            Dim objReg As TrClassRegistration = CType(RegClassColl(x), TrClassRegistration)
            If x <> RegClassColl.Count - 1 Then
                strClassColl = strClassColl & objReg.TrClass.ClassCode & ", "
            Else
                strClassColl = strClassColl & objReg.TrClass.ClassCode
            End If
        Next
        Return strClassColl
    End Function

    Private Sub UpdateTrainee()
        Dim objTrainee As TrTrainee = CType(sHTrainee.GetSession("objTrainee"), TrTrainee)
        Dim objTraineeFacade As TrTraineeFacade = New TrTraineeFacade(User)
        Dim nResult = -1
        Dim imageFile As Byte()
        Dim arlRegClassColl As New ArrayList

        Try
            If objTrainee.Status.Trim().ToUpper() <> ddlStatus.SelectedValue.Trim().ToUpper() Then
                If ddlStatus.SelectedValue = CType(EnumTrTrainee.TrTraineeStatus.Deactive, String) Or _
                    ddlStatus.SelectedValue = CType(EnumTrTrainee.TrTraineeStatus.Unapproved, String) Then
                    arlRegClassColl = GetRegOfTrainee(objTrainee)
                End If
            End If

            If arlRegClassColl.Count = 0 Then
                imageFile = Nothing
                If Not cbDeletePhoto.Checked Then
                    imageFile = UploadFile()
                End If

                If Not IsDealerExist(Me.txtDealerCode.Text) Then
                    Throw New DataException(SR.DataNotFound("Kode Dealer"))
                End If

                FillingObjectFromForm(objTrainee)
                If photoSrc.Value <> String.Empty Or cbDeletePhoto.Checked Then
                    objTrainee.Photo = imageFile
                End If

                If IsFieldValidationUpdated(objTrainee) And IsTraineeExist(objTraineeFacade, objTrainee) Then
                    Throw New Exception(SR.DataIsExist("Siswa"))
                End If

                nResult = objTraineeFacade.Update(objTrainee)
                If nResult = -1 Then
                    MessageBox.Show(SR.SaveFail)
                Else
                    MessageBox.Show(SR.SaveSuccess)
                    ClearData()
                End If
            Else
                MessageBox.Show("Data siswa - Status tidak bisa dirubah, karena siswa sudah terdaftar di kelas : \n" & _
                    BuildStringOfRegClassColl(arlRegClassColl) & "")
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub dtgTrainee_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgTrainee.ItemDataBound
        If Not e.Item.FindControl("btnHapus") Is Nothing Then
            CType(e.Item.FindControl("btnHapus"), LinkButton).Attributes.Add("OnClick", KTB.DNet.Utility.CommonFunction.PreventDoubleEvent("return confirm('Yakin Data ini akan dihapus?');"))
        End If

        If Not e.Item.FindControl("btnLihat") Is Nothing Then
            CType(e.Item.FindControl("btnLihat"), LinkButton).Attributes.Add("OnClick", KTB.DNet.Utility.CommonFunction.PreventDoubleEvent(String.Empty))
        End If

        If Not e.Item.FindControl("btnUbah") Is Nothing Then
            CType(e.Item.FindControl("btnUbah"), LinkButton).Attributes.Add("OnClick", KTB.DNet.Utility.CommonFunction.PreventDoubleEvent(String.Empty))
        End If

        If Not e.Item.FindControl("btnAktif") Is Nothing Then
            CType(e.Item.FindControl("btnAktif"), LinkButton).Attributes.Add("OnClick", KTB.DNet.Utility.CommonFunction.PreventDoubleEvent(String.Empty))
        End If

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgTrainee.CurrentPageIndex * dtgTrainee.PageSize)
        End If

        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        If Not e.Item.DataItem Is Nothing Then
            e.Item.DataItem.GetType().ToString()
            Dim RowValue As TrTrainee = CType(e.Item.DataItem, TrTrainee)

            Dim lblKodeDealer As Label = CType(e.Item.FindControl("lblKodeDealer"), Label)
            Dim btnAktif As LinkButton = CType(e.Item.FindControl("btnAktif"), LinkButton)
            If RowValue.Status = CStr(EnumTrTrainee.TrTraineeStatus.Active) Then
                btnAktif.Text = "<img src='../images/in-aktif.gif' border='0' alt='Non-Aktif'>"
            Else
                btnAktif.Text = "<img src='../images/aktif.gif' border='0' alt='Aktif'>"
            End If


            If Not IsNothing(RowValue.Dealer) Then
                lblKodeDealer.Text = RowValue.Dealer.DealerCode
            End If

            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)

            If Not IsNothing(RowValue.Status) Then
                Try
                    lblStatus.Text = New EnumTrTrainee().StatusByIndex(Integer.Parse(RowValue.Status))
                Catch
                    lblStatus.Text = String.Empty
                End Try
            End If

        End If

        If Not e.Item.FindControl("btnUbah") Is Nothing Then
            CType(e.Item.FindControl("btnUbah"), LinkButton).Visible = bPrivilegeChangeTrainee
        End If

        If Not e.Item.FindControl("btnHapus") Is Nothing Then
            CType(e.Item.FindControl("btnHapus"), LinkButton).Visible = bPrivilegeChangeTrainee
        End If
    End Sub

    Private Sub dtgTrainee_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgTrainee.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            ViewTrainee(e.Item.Cells(1).Text, False)
            dtgTrainee.SelectedIndex = -1
            Me.txtName.ReadOnly = True
            Me.txtDealerCode.ReadOnly = True
            Me.ICStartWork.Enabled = False
            Me.txtJobPosition.ReadOnly = True
            Me.txtEducationLevel.ReadOnly = True
            Me.photoSrc.Disabled = True
            Me.ddlShirtSize.Enabled = False
            Me.ddlStatus.Enabled = False
            Me.lblPopUpDealer.Enabled = False
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewTrainee(e.Item.Cells(1).Text, True)
            dtgTrainee.SelectedIndex = e.Item.ItemIndex
            Me.txtName.ReadOnly = False
            Me.txtDealerCode.ReadOnly = False
            Me.ICStartWork.Enabled = True
            Me.txtJobPosition.ReadOnly = False
            Me.txtEducationLevel.ReadOnly = False
            Me.photoSrc.Disabled = False
            Me.ddlShirtSize.Enabled = True
            Me.ddlStatus.Enabled = True
            Me.lblPopUpDealer.Enabled = True
            cbDeletePhoto.Visible = True
            cbDeletePhoto.Checked = False
        ElseIf e.CommandName = "Delete" Then
            DeleteTrainee(e.Item.Cells(1).Text)
        ElseIf e.CommandName = "Active" Then
            'ActivateTrainee(e.Item.Cells(1).Text, _
            '    CType(e.Item.Cells(8).FindControl("btnAktif"), LinkButton).Text = "<img src='../images/aktif.gif' border='0' alt='Aktif'>")
            ActivatedTrainee(e.Item.Cells(1).Text, _
                CType(e.Item.Cells(8).FindControl("lblStatus"), Label).Text <> "Aktif")

        End If
    End Sub

    Private Sub ActivateTrainee(ByVal nID As Integer, ByVal active As Boolean)
        Dim objTraineeFacade As TrTraineeFacade = New TrTraineeFacade(User)
        Dim objTrainee As TrTrainee = objTraineeFacade.Retrieve(nID)
        Dim arlRegClassColl As New ArrayList
        Dim IsErrorFound As Boolean = False

        If Not IsNothing(objTrainee) And (objTrainee.ID <> 0) Then
            Try
                If active Then
                    objTrainee.Status = CStr(EnumTrTrainee.TrTraineeStatus.Active)
                Else
                    arlRegClassColl = GetRegOfTrainee(objTrainee)
                    If arlRegClassColl.Count > 0 Then
                        MessageBox.Show("Data siswa - Status tidak bisa dirubah, karena siswa sudah terdaftar di kelas : \n" & _
                            BuildStringOfRegClassColl(arlRegClassColl) & "")
                        IsErrorFound = True
                    Else
                        objTrainee.Status = CStr(EnumTrTrainee.TrTraineeStatus.Deactive)
                    End If
                End If
                If Not IsErrorFound Then
                    objTraineeFacade.Update(objTrainee)

                    dtgTrainee.CurrentPageIndex = 0
                    dtgTrainee.SelectedIndex = -1
                    BindDataGrid(0)
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub ActivatedTrainee(ByVal nID As Integer, ByVal active As Boolean)
        Dim objTraineeFacade As TrTraineeFacade = New TrTraineeFacade(User)
        Dim objTrainee As TrTrainee = objTraineeFacade.Retrieve(nID)
        Dim arlRegClassColl As New ArrayList
        Dim IsErrorFound As Boolean = False

        If Not IsNothing(objTrainee) And (objTrainee.ID <> 0) Then
            Try
                If active Then
                    objTrainee.Status = CStr(EnumTrTrainee.TrTraineeStatus.Active)
                Else
                    arlRegClassColl = GetRegOfTrainee(objTrainee)
                    ' Modified by Ikhsan 20081218
                    ' Requested by Rina as a bug report
                    ' Produce message box to warn user to cancel the registered class first before deactivated the user
                    ' Start -----
                    If arlRegClassColl.Count > 0 Then
                        MessageBox.Show("Data siswa - Status tidak bisa dirubah, karena siswa sudah terdaftar di kelas : \n" & _
                            BuildStringOfRegClassColl(arlRegClassColl) & "")
                        IsErrorFound = True
                    Else
                        objTrainee.Status = CStr(EnumTrTrainee.TrTraineeStatus.Deactive)
                    End If
                    ' End -------
                End If
                If Not IsErrorFound Then
                    objTraineeFacade.Update(objTrainee)

                    dtgTrainee.CurrentPageIndex = 0
                    dtgTrainee.SelectedIndex = -1
                    BindDataGrid(0)
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub ViewTrainee(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objTrainee As TrTrainee = New TrTraineeFacade(User).Retrieve(nID)
        sHTrainee.SetSession("objTrainee", objTrainee)
        FillFormFromObject(objTrainee)
        Me.btnSimpan.Enabled = EditStatus
    End Sub

    Private Function IsDealerExist(ByVal DealerCode As String) As Boolean
        Dim objDealerFacade As DealerFacade = New DealerFacade(User)

        Try
            If objDealerFacade.ValidateCode(DealerCode) = 0 Then
                Return False
            End If
        Catch
            Throw
        End Try

        Return True
    End Function

    Private Sub FillingObjectFromForm(ByVal objTrainee As TrTrainee)
        objTrainee.Name = Me.txtName.Text
        objTrainee.Dealer = New DealerFacade(User).Retrieve(Me.txtDealerCode.Text)
        objTrainee.StartWorkingDate = ICStartWork.Value
        objTrainee.JobPosition = txtJobPosition.Text
        objTrainee.EducationLevel = txtEducationLevel.Text
        objTrainee.ShirtSize = ddlShirtSize.SelectedItem.Text
        objTrainee.Status = ddlStatus.SelectedValue.ToString
    End Sub

    Private Sub FillFormFromObject(ByVal objTrainee As TrTrainee)
        Me.txtName.Text = objTrainee.Name
        Me.txtDealerCode.Text = objTrainee.Dealer.DealerCode
        Me.ICStartWork.Value = objTrainee.StartWorkingDate
        Me.txtJobPosition.Text = objTrainee.JobPosition
        Me.txtEducationLevel.Text = objTrainee.EducationLevel
        Me.photoSrc.Attributes("value") = String.Empty
        Dim itemShirt As ListItem = Me.ddlShirtSize.Items.FindByText(objTrainee.ShirtSize)
        Me.ddlShirtSize.SelectedIndex = Me.ddlShirtSize.Items.IndexOf(itemShirt)
        Dim itemStatus As ListItem = Me.ddlStatus.Items.FindByValue(objTrainee.Status)
        Me.ddlStatus.SelectedIndex = Me.ddlStatus.Items.IndexOf(itemStatus)
        Me.photoView.ImageUrl = "../WebResources/GetPhoto.aspx?id=" + objTrainee.ID.ToString
    End Sub

    'Hard Delete, directly affected to physical data
    Private Sub DeleteTrainee(ByVal nID As Integer)
        If New HelperFacade(User, GetType(TrClassRegistration)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(TrClassRegistration), nID), _
            CreateAggreateForCheckRecord(GetType(TrClassRegistration))) Then

            MessageBox.Show(SR.CannotDelete)
        Else
            Dim objTraineeFacade As TrTraineeFacade = New TrTraineeFacade(User)
            Dim objTrainee As TrTrainee = objTraineeFacade.Retrieve(nID)
            'objDealerGroup.RowStatus = DBRowStatus.Deleted
            'Dim nResult = New DealerGroupFacade(User).Update(objDealerGroup)

            Try
                If objTraineeFacade.DeleteFromDB(objTrainee) < 0 Then
                    MessageBox.Show(SR.DeleteFail)
                Else
                    MessageBox.Show(SR.DeleteSucces)
                End If
            Catch
                MessageBox.Show(SR.DeleteFail)
            End Try

            ClearData()
            dtgTrainee.SelectedIndex = -1
            dtgTrainee.CurrentPageIndex = 0
            BindDataGrid(dtgTrainee.CurrentPageIndex)
        End If
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub


    Private Sub dtgTrainee_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgTrainee.SortCommand
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

        dtgTrainee.SelectedIndex = -1
        dtgTrainee.CurrentPageIndex = 0
        BindDataGrid(dtgTrainee.CurrentPageIndex)
        ClearData()
    End Sub


    Private Sub dtgTrainee_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgTrainee.PageIndexChanged
        dtgTrainee.SelectedIndex = -1
        dtgTrainee.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgTrainee.CurrentPageIndex)
        ClearData()
    End Sub

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
    ByVal TraineeID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "TrTrainee", MatchType.Exact, TraineeID))
        Return criterias
    End Function

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

    Private Function UploadFile() As Byte()
        Dim nResult() As Byte

        If photoSrc.Value = "" Then
            Return Nothing
        End If

        If Not (photoSrc.PostedFile Is Nothing) Then
            Try
                If IsValidPhoto(photoSrc.PostedFile) Then
                    Dim inStream As System.IO.Stream = photoSrc.PostedFile.InputStream()
                    Dim ByteRead(TrTrainee.MAX_PHOTO_SIZE) As Byte
                    Dim ReadCount As Integer = New System.IO.BinaryReader(inStream).Read(ByteRead, 0, TrTrainee.MAX_PHOTO_SIZE)
                    If ReadCount = 0 Then
                        Throw New InvalidConstraintException(SR.DataNotFound("Photo"))
                    End If
                    ReDim nResult(ReadCount)
                    Array.Copy(ByteRead, nResult, ReadCount)
                Else
                    Throw New DataException("Foto harus file gambar dan maksimum 20 KB")
                End If
            Catch
                Throw
            End Try
        End If

        Return nResult
    End Function

    Private Function IsValidPhoto(ByVal file As HttpPostedFile) As Boolean
        Dim containImage As Boolean = (file.ContentType.ToUpper.IndexOf(TrTrainee.VALID_IMAGE_TYPE) >= 0)
        Dim sizeValid As Boolean = (file.ContentLength <= TrTrainee.MAX_PHOTO_SIZE)
        Return (containImage AndAlso sizeValid)
    End Function

    Private Function IsFormValid() As Boolean
        If txtName.Text = String.Empty Then
            Return False
        End If

        If txtDealerCode.Text = String.Empty Then
            Return False
        End If

        If txtEducationLevel.Text = String.Empty Then
            Return False
        End If

        If txtJobPosition.Text = String.Empty Then
            Return False
        End If

        'If ddlShirtSize.SelectedIndex = 0 Then
        '    Return False
        'End If
        Dim liShirtSize As ListItem
        liShirtSize = ddlShirtSize.Items.FindByValue(ddlShirtSize.SelectedValue)
        If IsNothing(liShirtSize) Then
            Return False
        End If

        'If ddlShirtSize.SelectedIndex = 0 Then
        '    Return False
        'End If
        Dim liStatus As ListItem
        liStatus = ddlStatus.Items.FindByValue(ddlStatus.SelectedValue)
        If IsNothing(liStatus) Then
            Return False
        End If

        Return True
    End Function

    Private Function IsUnhack() As Boolean
        If txtName.Text.IndexOf("<") >= 0 Or txtName.Text.IndexOf(">") >= 0 Or txtName.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtDealerCode.Text.IndexOf("<") >= 0 Or txtDealerCode.Text.IndexOf(">") >= 0 Or txtDealerCode.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtEducationLevel.Text.IndexOf("<") >= 0 Or txtEducationLevel.Text.IndexOf(">") >= 0 Or txtEducationLevel.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtJobPosition.Text.IndexOf("<") >= 0 Or txtJobPosition.Text.IndexOf(">") >= 0 Or txtJobPosition.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        Return True
    End Function

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        ClearData()
        dtgTrainee.CurrentPageIndex = 0
        BindDataGrid(dtgTrainee.CurrentPageIndex)
    End Sub
End Class
