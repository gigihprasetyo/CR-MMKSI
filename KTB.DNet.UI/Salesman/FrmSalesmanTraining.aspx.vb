#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security
#End Region

Public Class FrmSalesmanTraining
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlTrainingCode As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnTrainingMember As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents dgSalesmanTraining As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnPilih As System.Web.UI.WebControls.Button
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlLevel As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "PrivateVariables"
    Private _SalesmanTrainingParticipantFacade As New SalesmanTrainingParticipantFacade(User)
    Private _SalesmanLevelFacade As New SalesmanLevelFacade(User)
    Private _create As Boolean
    Private sessHelper As New SessionHelper
    Private strSalesId As String = String.Empty

#End Region

#Region "PrivateCustomMethods"

    ' Untuk bind data yg bersangkutan - related
    Private Sub BindDropDownLists()
        CommonFunction.BindSalesmanTrainingCode(ddlTrainingCode, Me.User, True)
        BindDropDownListLevel()
    End Sub

    'Hari -> bind level to dropdownlist
    Private Sub BindDropDownListLevel()
        Dim arrLevel As New ArrayList

        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanLevel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        arrLevel = _SalesmanLevelFacade.Retrieve(criterias)

        ddlLevel.Items.Add(New ListItem("Silahkan pilih", String.Empty))
        For Each objSalesmanlevel As SalesmanLevel In arrLevel
            ddlLevel.Items.Add(New ListItem(objSalesmanlevel.Description, objSalesmanlevel.ID))
        Next

    End Sub

    Private Sub BindControlsAttribute()
        lblPopUpDealer.Attributes("onClick") = "showPopUp('../General/../PopUp/PopUpDealerSelection.aspx','',500,760,DealerSelection);"
        'btnMember.Attributes("onClick") = "ShowPopUpTrainingMember();"
        btnPilih.Attributes("onclick") = "ShowPopUpTrainingMember();"
    End Sub
    Private Sub SaveCriteria()
        Dim crits As Hashtable = New Hashtable
        crits.Add("DealerCode", txtDealerCode.Text)
        crits.Add("TrainingCode", ddlTrainingCode.SelectedValue)
        sessHelper.SetSession("FrmSalesmanTraining", crits)
    End Sub
    ' untuk menampilkan data kriteria sebelumnya
    Private Sub ReadCriteria()
        Dim crits As Hashtable
        crits = CType(sessHelper.GetSession("FrmSalesmanTraining"), Hashtable)

        If Not IsNothing(crits) Then
            txtDealerCode.Text = CStr(crits.Item("DealerCode"))
            ddlTrainingCode.SelectedValue = CStr(crits.Item("TrainingCode"))
        End If

    End Sub
    Private Sub Delete(ByVal nID As Integer)
        Dim iRecordCount As Integer = 0

        Dim objSalesmanTrainingParticipant As SalesmanTrainingParticipant = New SalesmanTrainingParticipantFacade(User).Retrieve(nID)
        Dim facade As SalesmanTrainingParticipantFacade = New SalesmanTrainingParticipantFacade(User)
        Dim iReturn As Integer = -1
        'iReturn = facade.DeleteTransaction(objSalesmanArea)
        iReturn = facade.DeleteFromDB(objSalesmanTrainingParticipant)
        If iReturn <= 0 Then
            MessageBox.Show("Record Gagal Dihapus")
        End If

        dgSalesmanTraining.CurrentPageIndex = 0
        BindDataGrid(dgSalesmanTraining.CurrentPageIndex)
    End Sub
    ' penambahan untuk initialize data
    Private Sub ClearData()
        txtDealerCode.Text = String.Empty
        ddlTrainingCode.SelectedIndex = -1

    End Sub
    Private Sub Initialize()
        ClearData()
    End Sub
    ' penambahan untuk delete data
    Private Sub DeleteArea(ByVal nID As Integer)
        Dim iRecordCount As Integer = 0

        Dim objSalesmanArea As SalesmanArea = New SalesmanAreaFacade(User).Retrieve(nID)
        Dim facade As SalesmanAreaFacade = New SalesmanAreaFacade(User)
        Dim iReturn As Integer = -1
        'iReturn = facade.DeleteTransaction(objSalesmanArea)
        iReturn = facade.DeleteFromDB(objSalesmanArea)
        If iReturn <= 0 Then
            MessageBox.Show("Record Gagal Dihapus")
        End If

        dgSalesmanTraining.CurrentPageIndex = 0
        BindDataGrid(dgSalesmanTraining.CurrentPageIndex)
    End Sub
    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList

        If (IsNothing(sessHelper.GetSession("criteriaTraining"))) Then
            ' membuat format salesmanTrainingParticipant
            Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanTrainingParticipant), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            ' default criteria
            criterias.opAnd(New Criteria(GetType(SalesmanTrainingParticipant), "SalesmanHeader.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanTrainingParticipant), "SalesmanHeader.RegisterStatus", MatchType.Exact, CType(EnumSalesmanRegisterStatus.SalesmanRegisterStatus.Sudah_Register, String)))

            If txtDealerCode.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(SalesmanTrainingParticipant), "SalesmanHeader.Dealer.DealerCode", MatchType.InSet, CommonFunction.GetStrValue(txtDealerCode.Text, ";", ",")))
            End If

            If ddlTrainingCode.SelectedItem.Text <> String.Empty Then
                criterias.opAnd(New Criteria(GetType(SalesmanTrainingParticipant), "SalesmanMasterTraining.TrainingCode", MatchType.Exact, ddlTrainingCode.SelectedItem.Text))
            End If

            If ddlLevel.SelectedIndex <> 0 Then
                criterias.opAnd(New Criteria(GetType(SalesmanTrainingParticipant), "SalesmanHeader.SalesmanLevel.ID", MatchType.Exact, ddlLevel.SelectedItem.Value))
            End If
                   

            sessHelper.SetSession("criteriaTraining", criterias)
        End If

        arrList = _SalesmanTrainingParticipantFacade.RetrieveByCriteria(CType(sessHelper.GetSession("criteriaTraining"), CriteriaComposite), idxPage + 1, dgSalesmanTraining.PageSize, totalRow, _
        CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dgSalesmanTraining.DataSource = arrList

        dgSalesmanTraining.VirtualItemCount = totalRow
        dgSalesmanTraining.DataBind()

    End Sub

#End Region

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckPrivilege()
        If Not IsPostBack Then
            Initialize()
            BindDropDownLists()
            BindControlsAttribute()
            ReadCriteria()
            BindDataGrid(0)
        Else
            'postback from javascript
            If Request("__EVENTARGUMENT") = "AddMember" Then
                btnMember_Click(Me, System.EventArgs.Empty)
            End If
        End If
        btnSimpan.Visible = _create
        btnPilih.Visible = _create

    End Sub
    Private Sub dgSalesmanTraining_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgSalesmanTraining.SortCommand
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
        dgSalesmanTraining.SelectedIndex = -1
        dgSalesmanTraining.CurrentPageIndex = 0
        BindDataGrid(dgSalesmanTraining.CurrentPageIndex)
    End Sub
    Private Sub dgSalesmanTraining_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSalesmanTraining.PageIndexChanged
        dgSalesmanTraining.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgSalesmanTraining.CurrentPageIndex)
    End Sub
    Private Sub dgSalesmanTraining_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSalesmanTraining.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then

            Dim objSalesmanTrainingParticipant As SalesmanTrainingParticipant = e.Item.DataItem
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgSalesmanTraining.CurrentPageIndex * dgSalesmanTraining.PageSize)

            Dim lblDealerCodeNew As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
            lblDealerCodeNew.Text = objSalesmanTrainingParticipant.SalesmanHeader.Dealer.DealerCode

            Dim lblSalesmanCodeNew As Label = CType(e.Item.FindControl("lblSalesmanCode"), Label)
            lblSalesmanCodeNew.Text = objSalesmanTrainingParticipant.SalesmanHeader.SalesmanCode

            Dim lblNameNew As Label = CType(e.Item.FindControl("lblName"), Label)
            lblNameNew.Text = objSalesmanTrainingParticipant.SalesmanHeader.Name

            Dim lblPositionNew As Label = CType(e.Item.FindControl("lblPosition"), Label)
            lblPositionNew.Text = objSalesmanTrainingParticipant.SalesmanHeader.JobPosition.Code

            Dim lblLevelNew As Label = CType(e.Item.FindControl("lblLevel"), Label)
            lblLevelNew.Text = objSalesmanTrainingParticipant.SalesmanHeader.SalesmanLevel.Description

            Dim lblWorkPeriodNew As Label = CType(e.Item.FindControl("lblWorkPeriod"), Label)
            lblWorkPeriodNew.Text = objSalesmanTrainingParticipant.SalesmanHeader.HireDate

            Dim chkValidationNew As CheckBox = CType(e.Item.FindControl("chkValidation"), CheckBox)
            If objSalesmanTrainingParticipant.IsValidated = 1 Then
                chkValidationNew.Checked = True
            Else
                chkValidationNew.Checked = False
            End If

            Dim chkCancelNew As CheckBox = CType(e.Item.FindControl("chkCancel"), CheckBox)
            If objSalesmanTrainingParticipant.IsCancelled = 1 Then
                chkCancelNew.Checked = True
            Else
                chkCancelNew.Checked = False
            End If

            Dim lblSalesmanIdNew As Label = CType(e.Item.FindControl("lblSalesmanId"), Label)
            lblSalesmanIdNew.Text = objSalesmanTrainingParticipant.SalesmanHeader.ID

            Dim lblTrainingParticipantIdNew As Label = CType(e.Item.FindControl("lblTrainingParticipantId"), Label)
            lblTrainingParticipantIdNew.Text = objSalesmanTrainingParticipant.ID

            Dim lblStatusNew As Label = CType(e.Item.FindControl("lblStatus"), Label)
            lblStatusNew.Text = objSalesmanTrainingParticipant.SalesmanHeader.Status.ToString

        End If

        If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
            CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = _create
        End If
    End Sub
    Private Sub dgSalesmanTraining_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSalesmanTraining.ItemCommand
        If e.CommandName = "Delete" Then
            Delete(e.Item.Cells(0).Text)
            ClearData()
        End If
    End Sub
    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        SaveCriteria()
        dgSalesmanTraining.CurrentPageIndex = 0
        sessHelper.RemoveSession("criteriaTraining")
        BindDataGrid(0)
    End Sub
    'Private Sub btnTrainingMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTrainingMember.Click

    '    If ddlTrainingCode.SelectedItem.Value = String.Empty Then
    '        MessageBox.Show("Kode Training harus diisikan terlebih dahulu")
    '    Else
    '        If Not dgSalesmanTraining Is Nothing Then
    '            For Each item As DataGridItem In dgSalesmanTraining.Items
    '                Dim lblSalesmanIdTmp As Label = CType(item.FindControl("lblSalesmanId"), Label)
    '                If strSalesId = "" Then
    '                    strSalesId = lblSalesmanIdTmp.Text
    '                Else
    '                    strSalesId = strSalesId & ";" & lblSalesmanIdTmp.Text
    '                End If

    '            Next
    '        End If

    '        ' melempar data salesman yg sdh pernah ikut, untuk diexclude di data salesman
    '        btnTrainingMember.Attributes("onClick") = "showPopUp('../Salesman/FrmSalesmanTrainingMember.aspx?strSalesId=" & strSalesId + "&strTrainingId=" & ddlTrainingCode.SelectedItem.Value & "','',500,760,TrainingMember);"
    '    End If

    'End Sub

    ' untuk menampung kriteria yg sebelumnya
    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        ' melakukan update pada datagrid yg bersangkutan
        Dim chkValidationNew As CheckBox
        Dim chkCancelNew As CheckBox
        Dim lblTrainingParticipantId As Label
        Dim intResult As Integer
        Dim intTrainingParticipantId As Integer

        If Not dgSalesmanTraining Is Nothing Then
            Dim counter As Integer = 0
            For Each item As DataGridItem In dgSalesmanTraining.Items
                ' mengambil domain yg related
                lblTrainingParticipantId = CType(item.FindControl("lblTrainingParticipantId"), Label)
                intTrainingParticipantId = CType(lblTrainingParticipantId.Text, Integer)

                Dim objSalesmanTrainingParticipantGet As SalesmanTrainingParticipant = New SalesmanTrainingParticipantFacade(Me.User).Retrieve(intTrainingParticipantId)
                If Not objSalesmanTrainingParticipantGet Is Nothing Then
                    chkValidationNew = CType(item.FindControl("chkValidation"), CheckBox)
                    If chkValidationNew.Checked Then
                        objSalesmanTrainingParticipantGet.IsValidated = 1
                    Else
                        objSalesmanTrainingParticipantGet.IsValidated = 0
                    End If

                    chkCancelNew = CType(item.FindControl("chkCancel"), CheckBox)
                    If chkCancelNew.Checked Then
                        objSalesmanTrainingParticipantGet.IsCancelled = 1
                    Else
                        objSalesmanTrainingParticipantGet.IsCancelled = 0
                    End If

                    intResult = New SalesmanTrainingParticipantFacade(User).Update(objSalesmanTrainingParticipantGet)
                    If intResult <> -1 Then
                        counter = counter + 1
                    End If
                End If
            Next

            If counter = dgSalesmanTraining.Items.Count Then
                MessageBox.Show("Data telah berhasil disimpan")
            Else
                MessageBox.Show("Beberapa data gagal disimpan")
            End If
            BindDataGrid(0)
        End If
    End Sub
    Private Sub btnMember_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'sessHelper.SetSession("dealer", txtDealerCode.Text)
        sessHelper.RemoveSession("FrmSalesmanTrainingMember")
        BindDataGrid(0)
    End Sub

#End Region



#Region "Privilege"
    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.PesertaPelatihanSelectView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Pelatihan Tenaga Penjual - Pilih Peserta Pelatihan")
        End If
        _create = SecurityProvider.Authorize(context.User, SR.PesertaPelatihanSelectCreateData_Privilege)
    End Sub
#End Region


End Class
