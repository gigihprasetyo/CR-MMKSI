#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class FrmGRKind
    Inherits System.Web.UI.Page
    Private _sessHelper As SessionHelper = New SessionHelper
    Private m_bFormPrivilege As Boolean = False

#Region "Custom Method"

    Private Sub InitiatePage()
        ClearData()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = "KindCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub BindDatagrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            dtgGRKind.DataSource = New GRKindFacade(User).RetrieveActiveList(indexPage + 1, dtgGRKind.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
              CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgGRKind.VirtualItemCount = totalRow
            dtgGRKind.DataBind()
        End If
    End Sub

    Private Function UpdateGRKind() As Integer
        Dim objGRKind As GRKind = CType(Session.Item("vsGRKind"), GRKind)
        objGRKind.KindDescription = txtKindDesc.Text
        If cbStatus.Checked Then
            objGRKind.Status = 1
        Else
            objGRKind.Status = 0
        End If
        Dim nResult = New GRKindFacade(User).Update(objGRKind)
        Return nResult
    End Function

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, ByVal id As Integer) As CriteriaComposite
        Dim objStdFacade As StandardCodeFacade = New StandardCodeFacade(User)
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "KindCode", MatchType.Exact, id))
        criterias.opAnd(New Criteria(DomainType, "ServiceTypeID", MatchType.Exact, _
                                     objStdFacade.GetByCategoryValueCode("ServiceBooking.ServiceType", "OTH").ValueId))
        'criterias.opAnd(New Criteria(DomainType, "ServiceBooking.Status", MatchType.Exact, _
        '                             objStdFacade.GetByCategoryValueCode("ServiceBooking.Status", "Booked").ValueId))
        Return criterias
    End Function

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

    Private Function DeleteGRKind(ByVal nID As Integer) As Integer
        Dim iRecordCount As Integer = 0
        If New HelperFacade(User, GetType(ServiceBookingActivity)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(ServiceBookingActivity), nID), _
            CreateAggreateForCheckRecord(GetType(ServiceBookingActivity))) Then
            iRecordCount = iRecordCount + 1
        End If
        If iRecordCount > 0 Then
            Return 2
        Else
            Dim objGRKind As GRKind = New GRKindFacade(User).Retrieve(nID)
            Dim Facade As GRKindFacade = New GRKindFacade(User)
            Return Facade.DeleteFromDB(objGRKind)
            dtgGRKind.CurrentPageIndex = 0
            BindDatagrid(dtgGRKind.CurrentPageIndex)
        End If
    End Function

    Private Sub ViewGRKind(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objGRKind As GRKind = New GRKindFacade(User).Retrieve(nID)
        _sessHelper.SetSession("vsGRKind", objGRKind)
        txtKindCode.Text = objGRKind.KindCode
        txtKindDesc.Text = objGRKind.KindDescription
        If objGRKind.Status = 1 Then
            cbStatus.Checked = True
        Else
            cbStatus.Checked = False
        End If
        Me.btnSimpan.Enabled = EditStatus
    End Sub

    Private Sub ClearData()
        txtKindCode.Text() = String.Empty
        txtKindDesc.Text = String.Empty
        btnSimpan.Enabled = True
        ViewState.Add("vsProcess", "Insert")
        txtKindCode.ReadOnly = False
        txtKindDesc.ReadOnly = False
    End Sub

#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
            BindDatagrid(0)
        End If
        'Put user code to initialize the page here
    End Sub
    Private Sub SetControlPrivilege()
        btnSimpan.Visible = m_bFormPrivilege
        btnBatal.Visible = m_bFormPrivilege
    End Sub

    Private Sub ActivateUserPrivilege()
        m_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.GRKind_Input_Privilage)

        If Not SecurityProvider.Authorize(Context.User, SR.GRKind_View_Privilage) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Jenis General Repair")
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If

        Dim objGRKind As GRKind = New GRKind
        Dim objGRKindFacade As GRKindFacade = New GRKindFacade(User)
        Dim nResult As Integer = -1
        If CType(ViewState("vsProcess"), String) = "Insert" Then
            If Not txtKindCode.Text = String.Empty Then
                If objGRKindFacade.ValidateCode(txtKindCode.Text) <= 0 Then
                    objGRKind.KindCode = txtKindCode.Text
                    objGRKind.KindDescription = txtKindDesc.Text
                    If cbStatus.Checked Then
                        objGRKind.Status = 1
                    Else
                        objGRKind.Status = 0
                    End If
                    nResult = New GRKindFacade(User).Insert(objGRKind)
                    If nResult = -1 Then
                        MessageBox.Show(SR.SaveFail)
                    Else
                        MessageBox.Show(SR.SaveSuccess)
                        ClearData()
                        dtgGRKind.SelectedIndex = -1
                    End If
                Else
                    MessageBox.Show(SR.DataIsExist("Kode General Repair"))
                End If
            Else
                MessageBox.Show(SR.GridIsEmpty("Kode General Repair"))
            End If
        Else
            nResult = UpdateGRKind()
            If nResult = -1 Then
                MessageBox.Show(SR.UpdateFail)
            Else
                MessageBox.Show(SR.UpdateSucces)
                ClearData()
                dtgGRKind.SelectedIndex = -1
            End If
        End If

        'dtgReason.CurrentPageIndex = 0
        BindDatagrid(dtgGRKind.CurrentPageIndex)
    End Sub

    Private Sub dtgGRKind_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgGRKind.ItemDataBound
        'If Not e.Item.DataItem Is Nothing Then
        Dim rowVal As GRKind = CType(e.Item.DataItem, GRKind)

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = CType(e.Item.ItemIndex + 1 + (dtgGRKind.CurrentPageIndex * dtgGRKind.PageSize), String)
            If rowVal.Status = 1 Then
                CType(e.Item.FindControl("lblStatus"), Label).Text = "Aktif"
            Else
                CType(e.Item.FindControl("lblStatus"), Label).Text = "Pasif"
            End If

            If Not e.Item.FindControl("lbtnEdit") Is Nothing Then
                CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = m_bFormPrivilege
            End If

            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = m_bFormPrivilege
            End If

            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")
            End If
        End If

        
    End Sub

    Private Sub dtgGRKind_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgGRKind.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            txtKindCode.ReadOnly = True
            txtKindDesc.ReadOnly = True
            ViewGRKind(e.Item.Cells(0).Text, False)
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            ViewGRKind(e.Item.Cells(0).Text, True)
            dtgGRKind.SelectedIndex = e.Item.ItemIndex
            txtKindCode.ReadOnly = True
            txtKindDesc.ReadOnly = False
        ElseIf e.CommandName = "Delete" Then
            Try
                Dim nResult = DeleteGRKind(e.Item.Cells(0).Text)
                If nResult = 2 Then
                    MessageBox.Show(SR.CannotDelete)
                ElseIf nResult = -1 Then
                    MessageBox.Show(SR.DeleteFail)
                Else
                    MessageBox.Show(SR.DeleteSucces)
                End If
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
            ClearData()
            BindDatagrid(dtgGRKind.CurrentPageIndex)
        End If
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
        dtgGRKind.SelectedIndex = -1
    End Sub

    Private Sub dtgReason_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgGRKind.SortCommand
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
        dtgGRKind.SelectedIndex = -1
        dtgGRKind.CurrentPageIndex = 0
        BindDatagrid(dtgGRKind.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgReason_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgGRKind.PageIndexChanged
        dtgGRKind.SelectedIndex = -1
        dtgGRKind.CurrentPageIndex = e.NewPageIndex
        BindDatagrid(dtgGRKind.CurrentPageIndex)
        ClearData()
    End Sub

#End Region

End Class