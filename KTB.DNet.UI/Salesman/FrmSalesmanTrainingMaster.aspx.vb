#Region "Custom Namespace Import"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
#End Region


Public Class FrmSalesmanTrainingMaster
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents txtTrainingCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlTrainingType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents icTglCreate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icTglCreate2 As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtTrainer1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTrainer2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTrainer3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtAttendanceTarget As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTrainingPlace As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPrerequisite As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTrainingTitle As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgTraining As System.Web.UI.WebControls.DataGrid
    Protected WithEvents icTglDaftar As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icTglDaftar2 As KTB.DNet.WebCC.IntiCalendar

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
    Dim objSalesmanTrainingType As SalesmanTrainingType = New SalesmanTrainingType
    Dim objSalesmanMasterTraining As SalesmanMasterTraining = New SalesmanMasterTraining
    Dim objSalesmanMasterTrainingFacade As SalesmanMasterTrainingFacade = New SalesmanMasterTrainingFacade(User)
    Dim objSalesmanTrainingTypeFacade As New SalesmanTrainingTypeFacade(User)
    Dim SalesmanMasterTrainingList As ArrayList
    Dim sessHelp As SessionHelper = New SessionHelper
    Dim criterias As CriteriaComposite
#End Region

#Region "Privilege"
    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.KodePelatihanViewCreate_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Pelatihan Tenaga Penjual - Buat Kode Pelatihan")
        End If
    End Sub

    Dim bCheckCreatePrivilege As Boolean = SecurityProvider.Authorize(context.User, SR.KodePelatihanCreateData_Privilege)

    'Private Function CheckCreatePrivilege() As Boolean
    '    If Not SecurityProvider.Authorize(context.User, SR.KodePelatihanCreateData_Privilege) Then
    '        Return False
    '    Else
    '        Return True
    '    End If
    'End Function
    Private Function CheckViewDetailPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.KodePelatihanCreateViewDetail_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
    Private Function CheckEditDetailPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.KodePelatihanEditCreate_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
    Private Function CheckDeleteDetailPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.KodePelatihanCreateDelete_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

#Region "Custom Method"
    Private Sub bindArrayListToDropDownList(ByRef objDropDownList As DropDownList, ByVal objArrayList As ArrayList, ByVal DataTextField As String)
        objDropDownList.DataSource = objArrayList
        objDropDownList.DataTextField = DataTextField
        objDropDownList.DataValueField = "ID"
        objDropDownList.DataBind()
    End Sub
    Private Function CheckValidation() As Boolean
        Dim blnValid As Boolean = True
        If (txtTrainingCode.Text = "") Then
            blnValid = False
            MessageBox.Show("Kode Training harus diinput terlebih dahulu")
            Return (blnValid)
        End If
        If (txtTrainingTitle.Text = "") Then
            blnValid = False
            MessageBox.Show("Materi Training harus diinput terlebih dahulu")
            Return (blnValid)
        End If
        If (icTglCreate.Value > icTglCreate2.Value) Then
            blnValid = False
            MessageBox.Show("Tanggal penyelenggaraan dari harus lebih kecil dari Tanggal penyelenggaraan sampai")
            Return (blnValid)
        End If
        Return blnValid
    End Function
    Private Sub BindDatagrid(ByVal indexPage As Integer)

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanMasterTraining), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            SalesmanMasterTrainingList = New SalesmanMasterTrainingFacade(User).RetrieveActiveList(indexPage + 1, dtgTraining.PageSize, totalRow, sessHelp.GetSession("SortCol"), sessHelp.GetSession("SortDirection"), criterias)
            dtgTraining.DataSource = SalesmanMasterTrainingList
            dtgTraining.VirtualItemCount = totalRow
            dtgTraining.DataBind()
        End If

        If SalesmanMasterTrainingList.Count = 0 And IsPostBack Then
            MessageBox.Show(SR.DataNotFound("Data"))
        End If
    End Sub
    Private Sub InsertTrainingMaster()
        Dim nResult As Integer
        If objSalesmanMasterTrainingFacade.ValidateCode(txtTrainingCode.Text) = 0 Then
            objSalesmanMasterTraining.TrainingCode = txtTrainingCode.Text
            objSalesmanMasterTraining.TrainingTitle = txtTrainingTitle.Text
            objSalesmanMasterTraining.SalesmanTrainingType = objSalesmanTrainingTypeFacade.Retrieve(CType(ddlTrainingType.SelectedValue, Integer))
            objSalesmanMasterTraining.StartingDate = icTglCreate.Value
            objSalesmanMasterTraining.EndDate = icTglCreate2.Value
            objSalesmanMasterTraining.Trainer1 = txtTrainer1.Text
            objSalesmanMasterTraining.Trainer2 = txtTrainer2.Text
            objSalesmanMasterTraining.Trainer3 = txtTrainer3.Text
            objSalesmanMasterTraining.AttendanceTarget = IIf(txtAttendanceTarget.Text = "", 0, txtAttendanceTarget.Text)
            objSalesmanMasterTraining.TrainingPlace = txtTrainingPlace.Text
            objSalesmanMasterTraining.Prerequisite = txtPrerequisite.Text
            objSalesmanMasterTraining.RegisterStartingDate = icTglDaftar.Value
            objSalesmanMasterTraining.RegisterEndDate = icTglDaftar2.Value

            nResult = New SalesmanMasterTrainingFacade(User).Insert(objSalesmanMasterTraining)
            If nResult = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)
            End If
        Else
            MessageBox.Show(SR.DataIsExist("Jenis Training"))
        End If
        ClearData()
        BindDatagrid(0)
    End Sub
    Private Sub DisableControl()
        txtTrainingCode.ReadOnly = True
        txtTrainingTitle.ReadOnly = True
        ddlTrainingType.Enabled = False
        icTglCreate.Enabled = False
        icTglCreate2.Enabled = False
        txtTrainer1.ReadOnly = True
        txtTrainer2.ReadOnly = True
        txtTrainer3.ReadOnly = True
        txtAttendanceTarget.ReadOnly = True
        txtTrainingPlace.ReadOnly = True
        txtPrerequisite.ReadOnly = True
        btnSimpan.Enabled = False
    End Sub
    Private Sub EnableControl()
        txtTrainingCode.ReadOnly = False
        txtTrainingTitle.ReadOnly = False
        ddlTrainingType.Enabled = True
        icTglCreate.Enabled = True
        icTglCreate2.Enabled = True
        txtTrainer1.ReadOnly = False
        txtTrainer2.ReadOnly = False
        txtTrainer3.ReadOnly = False
        txtAttendanceTarget.ReadOnly = False
        txtTrainingPlace.ReadOnly = False
        txtPrerequisite.ReadOnly = False
        btnSimpan.Enabled = True
    End Sub
    Private Sub UpdateTrainingMaster()
        Dim objSalesmanMasterTraining As SalesmanMasterTraining = CType(sessHelp.GetSession("objedit"), SalesmanMasterTraining)
        Dim nResult As Integer

        ' Training code must be unique
        Dim objSalesmanMasterTrainingFacade As SalesmanMasterTrainingFacade = New SalesmanMasterTrainingFacade(User)
        If objSalesmanMasterTrainingFacade.ValidateCode(txtTrainingCode.Text, objSalesmanMasterTraining.ID) > 0 Then
            MessageBox.Show("Kode Training sudah ada sebelumnya, silakan input Kode Training yang lainnya")
            Return
        End If

        objSalesmanMasterTraining.TrainingCode = txtTrainingCode.Text.Trim
        objSalesmanMasterTraining.TrainingTitle = txtTrainingTitle.Text
        objSalesmanMasterTraining.SalesmanTrainingType = objSalesmanTrainingTypeFacade.Retrieve(CType(ddlTrainingType.SelectedValue, Integer))
        objSalesmanMasterTraining.StartingDate = icTglCreate.Value
        objSalesmanMasterTraining.EndDate = icTglCreate2.Value
        objSalesmanMasterTraining.Trainer1 = txtTrainer1.Text
        objSalesmanMasterTraining.Trainer2 = txtTrainer2.Text
        objSalesmanMasterTraining.Trainer3 = txtTrainer3.Text
        objSalesmanMasterTraining.AttendanceTarget = IIf(txtAttendanceTarget.Text = "", 0, txtAttendanceTarget.Text)
        objSalesmanMasterTraining.TrainingPlace = txtTrainingPlace.Text
        objSalesmanMasterTraining.Prerequisite = txtPrerequisite.Text
        objSalesmanMasterTraining.RegisterStartingDate = icTglDaftar.Value
        objSalesmanMasterTraining.RegisterEndDate = icTglDaftar2.Value

        nResult = New SalesmanMasterTrainingFacade(User).Update(objSalesmanMasterTraining)

        If nResult = -1 Then
            MessageBox.Show(SR.UpdateFail)
        Else
            MessageBox.Show(SR.UpdateSucces)
        End If

        ClearData()
        BindDatagrid(0)
    End Sub
    Private Sub ClearData()
        txtTrainingCode.Text = String.Empty
        txtTrainingTitle.Text = String.Empty
        txtTrainer1.Text = String.Empty
        txtTrainer2.Text = String.Empty
        txtTrainer3.Text = String.Empty
        txtAttendanceTarget.Text = String.Empty
        txtTrainingPlace.Text = String.Empty
        txtPrerequisite.Text = String.Empty
        icTglCreate.Value = DateTime.Now
        icTglCreate2.Value = DateTime.Now
        icTglDaftar.Value = DateTime.Now
        icTglDaftar2.Value = DateTime.Now
        ViewState.Add("vsProcess", "Insert")
        If Not IsNothing(dtgTraining) Then
            dtgTraining.SelectedIndex = -1
        End If
    End Sub
    Private Sub GetValue(ByVal intID As Integer, ByVal intItemIndex As Integer)
        dtgTraining.SelectedIndex = intItemIndex
        Dim objSalesmanMasterTraining As SalesmanMasterTraining = New SalesmanMasterTrainingFacade(User).Retrieve(intID)
        If Not objSalesmanMasterTraining Is Nothing Then
            If CType(ViewState("vsProcess"), String) = "Edit" Then
                sessHelp.SetSession("objedit", objSalesmanMasterTraining)
            End If
            txtTrainingCode.Text = objSalesmanMasterTraining.TrainingCode
            txtTrainingTitle.Text = objSalesmanMasterTraining.TrainingTitle
            txtTrainer1.Text = objSalesmanMasterTraining.Trainer1
            txtTrainer2.Text = objSalesmanMasterTraining.Trainer2
            txtTrainer3.Text = objSalesmanMasterTraining.Trainer3
            txtAttendanceTarget.Text = objSalesmanMasterTraining.AttendanceTarget
            txtTrainingPlace.Text = objSalesmanMasterTraining.TrainingPlace
            txtPrerequisite.Text = objSalesmanMasterTraining.Prerequisite
            ddlTrainingType.SelectedValue = objSalesmanMasterTraining.SalesmanTrainingType.ID
            icTglCreate.Value = objSalesmanMasterTraining.StartingDate
            icTglCreate2.Value = objSalesmanMasterTraining.EndDate
            icTglDaftar.Value = objSalesmanMasterTraining.RegisterStartingDate
            icTglDaftar2.Value = objSalesmanMasterTraining.RegisterEndDate


        End If
    End Sub
    Private Sub DeleteForumBannedWord(ByVal nID As Integer)
        Dim crit As CriteriaComposite
        Dim objSalesmanMasterTraining As SalesmanMasterTraining = New SalesmanMasterTrainingFacade(User).Retrieve(nID)
        Dim objSalesmanMasterTrainingFacade As SalesmanMasterTrainingFacade = New SalesmanMasterTrainingFacade(User)


        crit = New CriteriaComposite(New Criteria(GetType(SalesmanTrainingParticipant), "SalesmanMasterTraining.ID", MatchType.Exact, objSalesmanMasterTraining.ID))
        Dim arlSalesmanTrainingParticipant As ArrayList = New SalesmanTrainingParticipantFacade(User).Retrieve(crit)
        If (arlSalesmanTrainingParticipant.Count > 0) Then
            MessageBox.Show("Data tidak dapat dihapus karena digunakan pada Salesman Training Participant")
        Else
            objSalesmanMasterTrainingFacade.DeleteFromDB(objSalesmanMasterTraining)
        End If
        BindDatagrid(dtgTraining.CurrentPageIndex)
    End Sub
#End Region

#Region "Event Handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        CheckPrivilege()
        btnSimpan.Visible = bCheckCreatePrivilege
        If Not IsPostBack Then
            ClearData()
            sessHelp.SetSession("SortCol", "TrainingCode")
            sessHelp.SetSession("SortDirection", Sort.SortDirection.ASC)
            bindArrayListToDropDownList(ddlTrainingType, objSalesmanTrainingTypeFacade.RetrieveList(), "TrainingType")
            BindDatagrid(0)
        End If
    End Sub
    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        ' 26-Nov-2007   Deddy H     Fix bug 1615, database type smallInt
        If Val(txtAttendanceTarget.Text) > 9999 Then
            MessageBox.Show("Target Peserta tidak boleh melebihi jumlah 9999")
            Return
        End If

        If CheckValidation() Then
            Dim nResult As Integer = -1
            If CType(ViewState("vsProcess"), String) = "Insert" Then
                'nResult = InsertTrainingMaster()
                InsertTrainingMaster()
            ElseIf CType(ViewState("vsProcess"), String) = "Edit" Then
                UpdateTrainingMaster()
            ElseIf CType(ViewState("vsProcess"), String) = "View" Then
                MessageBox.Show("Hanya view saja, tidak bisa disimpan")
            End If
        End If

    End Sub
    Private Sub ddlTrainingType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTrainingType.SelectedIndexChanged
    End Sub
    Private Sub btnBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
        EnableControl()
    End Sub
    Private Sub dtgTraining_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgTraining.SortCommand
        If e.SortExpression = sessHelp.GetSession("SortCol") Then
            If sessHelp.GetSession("SortDirection") = Sort.SortDirection.ASC Then
                sessHelp.SetSession("SortDirection", Sort.SortDirection.DESC)
            Else
                sessHelp.SetSession("SortDirection", Sort.SortDirection.ASC)
            End If
        End If
        sessHelp.SetSession("SortCol", e.SortExpression)
        dtgTraining.SelectedIndex = -1
        'dtgTraining.CurrentPageIndex = 0
        BindDatagrid(dtgTraining.CurrentPageIndex)
    End Sub
    Private Sub dtgTraining_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgTraining.PageIndexChanged
        dtgTraining.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgTraining.CurrentPageIndex)
    End Sub
    Private Sub dtgTraining_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgTraining.ItemCommand
        If e.CommandName = "Delete" Then
            DeleteForumBannedWord(e.CommandArgument)
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            GetValue(CInt(e.CommandArgument), e.Item.ItemIndex)
            EnableControl()
        ElseIf e.CommandName = "View" Then
            ViewState.Add("vsProcess", "View")
            GetValue(CInt(e.CommandArgument), e.Item.ItemIndex)
            DisableControl()
        End If
    End Sub
    Private Sub dtgTraining_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgTraining.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            If Not (SalesmanMasterTrainingList Is Nothing) Then
                objSalesmanMasterTraining = SalesmanMasterTrainingList(e.Item.ItemIndex)
                Dim _lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                _lblNo.Text = e.Item.ItemIndex + 1 + (dtgTraining.CurrentPageIndex * dtgTraining.PageSize)

                Dim _lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                _lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
                _lbtnDelete.CommandArgument = objSalesmanMasterTraining.ID

                Dim _lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
                _lbtnEdit.CommandArgument = objSalesmanMasterTraining.ID

                Dim _lbtnView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
                _lbtnView.CommandArgument = objSalesmanMasterTraining.ID

                _lbtnDelete.Visible = bCheckCreatePrivilege
                _lbtnEdit.Visible = bCheckCreatePrivilege
                _lbtnView.Visible = bCheckCreatePrivilege

            End If
        End If
    End Sub

#End Region


End Class
