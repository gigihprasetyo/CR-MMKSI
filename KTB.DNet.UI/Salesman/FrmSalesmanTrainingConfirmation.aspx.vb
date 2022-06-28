#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

#End Region



Public Class FrmSalesmanTrainingConfirmation
    Inherits System.Web.UI.Page

#Region "PrivateVariables"
    Private sessHelper As New SessionHelper
    Protected WithEvents lblTrainingPlace As System.Web.UI.WebControls.Label
    Private isChanged As Boolean
    Private objUser As UserInfo = New UserInfo
    Protected WithEvents lblPeriodePendaftaran As System.Web.UI.WebControls.Label
    Private _confirmPriv As Boolean = False
#End Region

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dgParticipant As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtKodeDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlKodeTraining As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblNamaTraining As System.Web.UI.WebControls.Label
    Protected WithEvents lblPeriodeTraining As System.Web.UI.WebControls.Label
    Protected WithEvents lblJenisTraining As System.Web.UI.WebControls.Label
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnFind As System.Web.UI.WebControls.Button

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

    Private Sub GetDealer()
        Dim objUserInfo As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        If Not objUserInfo Is Nothing Then
            txtKodeDealer.Text = objUserInfo.Dealer.DealerCode
        End If
    End Sub

    Private Sub BindDDl()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SalesmanMasterTraining), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        ddlKodeTraining.DataSource = New SalesmanMasterTrainingFacade(User).RetrieveByCriteria(criterias)
        ddlKodeTraining.DataTextField = "TrainingCode"
        ddlKodeTraining.DataValueField = "ID"
        ddlKodeTraining.DataBind()

        ddlKodeTraining.Items.Insert(0, New ListItem("Pilih Semua", 0))

        If ddlKodeTraining.Items.Count > 0 Then
            ddlKodeTraining.SelectedIndex = 0
        End If

    End Sub

    Private Sub bindgrid(ByVal idxPage As Integer)

        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanTrainingParticipant), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If isChanged = False Then
            ddlKodeTraining.SelectedValue = CInt(viewstate("DDLVal"))
        End If

        If (txtKodeDealer.Text.Length > 0) Then
            criterias.opAnd(New Criteria(GetType(SalesmanTrainingParticipant), "SalesmanHeader.Dealer.DealerCode", MatchType.Exact, txtKodeDealer.Text))
        End If


        If (CInt(ddlKodeTraining.SelectedValue) > 0) Then
            criterias.opAnd(New Criteria(GetType(SalesmanTrainingParticipant), "SalesmanMasterTraining.ID", MatchType.Exact, CInt(ddlKodeTraining.SelectedValue)))
        End If


        arrList = New SalesmanTrainingParticipantFacade(User).RetrieveByCriteria(criterias, idxPage + 1, dgParticipant.PageSize, totalRow, _
        CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dgParticipant.CurrentPageIndex = idxPage
        dgParticipant.DataSource = arrList
        dgParticipant.VirtualItemCount = totalRow
        dgParticipant.DataBind()

        If arrList.Count = "0" Then
            MessageBox.Show("Data Tidak Ada")
        End If
        isChanged = False
    End Sub

#End Region

#Region "Event Handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsNothing(sessHelper.GetSession("LOGINUSERINFO")) Then
            objUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
            If objUser.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                CheckPrivilege()
                _confirmPriv = CheckConfirmPrivilege()
            Else
                _confirmPriv = False
            End If
        End If
        btnSimpan.Enabled = _confirmPriv
        If Not IsPostBack Then
            GetDealer()
            BindDDl()
        End If
    End Sub

    Private Sub dgParticipant_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgParticipant.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objSalesmanTrainingParticipant As SalesmanTrainingParticipant = e.Item.DataItem

            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgParticipant.CurrentPageIndex * dgParticipant.PageSize)

            Dim chkKonfirmasi As CheckBox = e.Item.FindControl("chkKonfirmasi")
            If objSalesmanTrainingParticipant.IsConfirm = 1 Then
                chkKonfirmasi.Checked = True
                chkKonfirmasi.Enabled = False
            Else
                chkKonfirmasi.Checked = False
                chkKonfirmasi.Enabled = True
            End If
            chkKonfirmasi.Visible = _confirmPriv

        End If

    End Sub

    Private Sub dgParticipant_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgParticipant.PageIndexChanged
        dgParticipant.CurrentPageIndex = e.NewPageIndex
        bindgrid(dgParticipant.CurrentPageIndex)
    End Sub

    Private Sub dgParticipant_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgParticipant.SortCommand
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
        dgParticipant.SelectedIndex = -1
        'dgParticipant.CurrentPageIndex = 0
        bindgrid(dgParticipant.CurrentPageIndex)

    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click

        Dim objfacade As SalesmanTrainingParticipantFacade = New SalesmanTrainingParticipantFacade(User)
        Dim arlUpdate As ArrayList = New ArrayList
        For Each item As DataGridItem In dgParticipant.Items

            Dim lblSalesmanID As Label = item.FindControl("lblSalesmanID")
            Dim chkKonfirmasi As CheckBox = item.FindControl("chkKonfirmasi")
            Dim isConfirm As Integer

            If chkKonfirmasi.Checked Then
                isConfirm = 1
            Else
                isConfirm = 0
            End If

            Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanTrainingParticipant), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanTrainingParticipant), "SalesmanHeader.SalesmanCode", MatchType.Exact, lblSalesmanID.Text))
            If (ddlKodeTraining.SelectedIndex <> 0) Then
                criterias.opAnd(New Criteria(GetType(SalesmanTrainingParticipant), "SalesmanMasterTraining.ID", MatchType.Exact, CInt(ddlKodeTraining.SelectedValue)))
            End If

            Dim obj As SalesmanTrainingParticipant = objfacade.Retrieve(criterias)(0)

            If obj.IsConfirm <> isConfirm Then
                obj.IsConfirm = isConfirm
                arlUpdate.Add(obj)
            End If

        Next

        Dim result As Integer = objfacade.UpdateTransaction(arlUpdate)

        If result > 0 Then
            MessageBox.Show(result.ToString & " data berhasil diupdate")
            bindgrid(dgParticipant.CurrentPageIndex)
        Else
            MessageBox.Show("Tidak ada data baru")
        End If

    End Sub

    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        dgParticipant.CurrentPageIndex = 0

        viewstate("DDLVal") = ddlKodeTraining.SelectedValue
        isChanged = True

        ViewState("CurrentSortColumn") = "SalesmanHeader.SalesmanCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC

        Dim objSalesmanMasterTraining As SalesmanMasterTraining = New SalesmanMasterTrainingFacade(User).Retrieve(CInt(ddlKodeTraining.SelectedValue))

        If Not objSalesmanMasterTraining Is Nothing Then
            If objSalesmanMasterTraining.RegisterStartingDate > DateTime.Now Then
                MessageBox.Show("Pendaftaran belum dibuka")
                Exit Sub
            ElseIf objSalesmanMasterTraining.RegisterEndDate < DateTime.Now Then
                MessageBox.Show("Pendaftaran sudah ditutup")
                Exit Sub
            End If

        End If
        
        If (ddlKodeTraining.SelectedIndex <> 0) Then
            lblJenisTraining.Text = objSalesmanMasterTraining.SalesmanTrainingType.TrainingType
            lblNamaTraining.Text = objSalesmanMasterTraining.TrainingTitle
            lblPeriodeTraining.Text = objSalesmanMasterTraining.StartingDate.ToString("dd/MM/yyyy") & " s/d " & objSalesmanMasterTraining.EndDate.ToString("dd/MM/yyyy")
            lblPeriodePendaftaran.Text = objSalesmanMasterTraining.RegisterStartingDate.ToString("dd/MM/yyyy") & " s/d " & objSalesmanMasterTraining.RegisterEndDate.ToString("dd/MM/yyyy")
            lblTrainingPlace.Text = objSalesmanMasterTraining.TrainingPlace
        Else
            lblJenisTraining.Text = String.Empty
            lblNamaTraining.Text = String.Empty
            lblPeriodeTraining.Text = String.Empty
            lblPeriodePendaftaran.Text = String.Empty
            lblTrainingPlace.Text = String.Empty
        End If

        bindgrid(dgParticipant.CurrentPageIndex)

    End Sub

#End Region

#Region "Privilege"

    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.PesertaPelatihanAccessView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Pelatihan Tenaga Penjual - Daftar Peserta Pelatihan")
        End If
    End Sub

    Private Function CheckConfirmPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PesertaPelatihanConfirmation_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

#End Region

End Class
