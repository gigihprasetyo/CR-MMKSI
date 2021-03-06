#Region "Summary"
'// ===========================================================================		
'// Author Name   : Agus Pirnadi
'// PURPOSE       : 
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright ? 2005 
'// ---------------------
'// $History      : $
'// Generated on 29/09/2005
'// ===========================================================================		
#End Region

#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
#End Region

Public Class FrmVehicleModel
    Inherits System.Web.UI.Page
    Private m_bFormPrivilege As Boolean = False

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    'Protected WithEvents txtSAPCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvSAPCode As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtIndDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvIndDescription As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtVechileIndModel As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvVechileIndModel As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents dtgModel As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtKode As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvKode As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents rfvDeskripsi As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtDeskripsi As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " private custom variable "
    Private ListVehicleModel As ArrayList
#End Region

#Region "Custom Method"

    Private Sub InitiatePage()
        ClearData()
        SetControlPrivilege()
        ViewState("CurrentSortColumn") = "VechileModelCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        BindCategory()
    End Sub

    Private Sub BindCategory()
        Dim CategoryColl As ArrayList = New CategoryFacade(User).RetrieveList("Description", Sort.SortDirection.ASC)
        Dim item As ListItem
        item = New ListItem("Silahkan pilih", 0)
        ddlCategory.Items.Add(item)
        If CategoryColl.Count > 0 Then
            For Each cat As Category In CategoryColl
                item = New ListItem(cat.Description, cat.ID)
                ddlCategory.Items.Add(item)
            Next
        End If
    End Sub

    Private Sub ClearData()
        txtKode.Text = String.Empty
        txtDeskripsi.Text = String.Empty
        'txtSAPCode.Text = String.Empty
        txtIndDescription.Text = String.Empty
        txtVechileIndModel.Text = String.Empty
        btnSimpan.Enabled = True
        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totRow As Integer = 0
        If (indexPage >= 0) Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If ddlCategory.SelectedValue > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileModel), "Category.ID", MatchType.Exact, ddlCategory.SelectedValue))
            End If
            If txtKode.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileModel), "VechileModelCode", MatchType.Exact, txtKode.Text.Trim))
            End If
            If txtDeskripsi.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileModel), "Description", MatchType.[Partial], txtDeskripsi.Text.Trim))
            End If
            'If txtSAPCode.Text.Length > 0 Then
            '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileModel), "SAPCode", MatchType.Exact, txtSAPCode.Text.Trim))
            'End If
            If txtIndDescription.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileModel), "IndDescription", MatchType.[Partial], txtIndDescription.Text.Trim))
            End If
            If txtVechileIndModel.Text.Length > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileModel), "VechileModelIndCode", MatchType.Exact, txtVechileIndModel.Text.Trim))
            End If

            ListVehicleModel = New VechileModelFacade(User).RetrieveActiveList(criterias, indexPage + 1, _
                    dtgModel.PageSize, totRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgModel.DataSource = ListVehicleModel
            dtgModel.VirtualItemCount = totRow
            dtgModel.DataBind()
        End If
    End Sub



    Private Function InsertModel() As Integer
        Dim objModelFacade As VechileModelFacade = New VechileModelFacade(User)
        Dim objModel As VechileModel = New VechileModel
        Dim nResult As Integer

        If objModelFacade.ValidateCode(txtKode.Text) = 0 Then
            objModel.Category = New Category(CType(Me.ddlCategory.SelectedValue, Integer))
            objModel.VechileModelCode = txtKode.Text
            objModel.Description = txtDeskripsi.Text
            'objModel.SAPCode = txtSAPCode.Text
            objModel.VechileModelIndCode = txtVechileIndModel.Text
            objModel.IndDescription = txtIndDescription.Text

            nResult = New VechileModelFacade(User).Insert(objModel)
            If nResult = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)
            End If
        Else
            MessageBox.Show(SR.DataIsExist("Vehicle Model"))
        End If
        Return nResult
    End Function

    Private Sub UpdateModel()
        Dim objModel As VechileModel = CType(Session.Item("vsModel"), VechileModel)
        objModel.Description = txtDeskripsi.Text
        objModel.Category = New Category(CType(Me.ddlCategory.SelectedValue, Integer))
        'objModel.SAPCode = txtSAPCode.Text
        objModel.VechileModelIndCode = txtVechileIndModel.Text
        objModel.IndDescription = txtIndDescription.Text
        Dim nResult = New VechileModelFacade(User).Update(objModel)
    End Sub

    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, _
        ByVal ModelID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
                MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "VechileModel", MatchType.Exact, ModelID))
        Return criterias
    End Function

    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function

    Private Sub DeleteModel(ByVal nID As Integer)
        'Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.VechileType), "VechileModel.ID", MatchType.Exact, nID))

        'Dim ArrayList1 As ArrayList = New ArrayList
        'ArrayList1 = New VechileTypeFacade(User).Retrieve(criterias)

        If New HelperFacade(User, GetType(VechileType)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(VechileType), nID), _
            CreateAggreateForCheckRecord(GetType(VechileType))) Then

            MessageBox.Show(SR.CannotDelete)
        Else
            Dim objModel As VechileModel = New VechileModelFacade(User).Retrieve(nID)
            'objModel.RowStatus = DBRowStatus.Deleted
            Dim facade As VechileModelFacade = New VechileModelFacade(User)
            facade.DeleteFromDB(objModel)
            'facade.Delete(objModel)
            'Dim nResult = New VechileModelFacade(User).Delete(objModel)
            ClearData()
            dtgModel.CurrentPageIndex = 0
            BindDataGrid(dtgModel.CurrentPageIndex)
        End If
    End Sub

    Private Sub ViewModel(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objModel As VechileModel = New VechileModelFacade(User).Retrieve(nID)
        'Todo session
        Session.Add("vsModel", objModel)
        If IsNothing(objModel) Then
            txtKode.Text = ""
            txtDeskripsi.Text = ""
            'txtSAPCode.Text = ""
            txtIndDescription.Text = ""
            txtVechileIndModel.Text = ""
            Me.ddlCategory.SelectedValue = ""
        Else
            txtKode.Text = objModel.VechileModelCode
            txtDeskripsi.Text = objModel.Description
            'txtSAPCode.Text = objModel.SAPCode
            txtIndDescription.Text = objModel.IndDescription
            txtVechileIndModel.Text = objModel.VechileModelIndCode
            Me.ddlCategory.SelectedValue = CType(objModel.Category.ID, String)
        End If

        Me.btnSimpan.Enabled = EditStatus
    End Sub

    Private Sub SearchVehicleModelByCriteria()
        dtgModel.CurrentPageIndex = 0
        BindDataGrid(dtgModel.CurrentPageIndex)
    End Sub

#End Region

#Region "EventHandler"

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        SearchVehicleModelByCriteria()
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
            'BindDataGrid(0)
        End If
    End Sub

    Private Sub SetControlPrivilege()
        btnSimpan.Visible = m_bFormPrivilege
        btnBatal.Visible = m_bFormPrivilege
    End Sub

    Private Sub ActivateUserPrivilege()
        m_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.ChangeModel_Privilege)

        If Not SecurityProvider.Authorize(Context.User, SR.ViewModel_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=GENERAL - MODEL")
        End If

    End Sub


    Private Sub btnTutup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ViewState.Clear()
        Response.Redirect("../Default.aspx")
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        If ddlCategory.SelectedValue > 0 Then
            Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
            Dim categoryArr As ArrayList = New CategoryFacade(User).RetrieveActiveList(companyCode)
            Dim valid As Boolean = False
            For Each item As Category In categoryArr
                If ddlCategory.SelectedValue = item.ID Then
                    valid = True
                End If
            Next
            If valid Then
                txtKode.ReadOnly = False
                txtDeskripsi.ReadOnly = False
                'txtSAPCode.ReadOnly = False
                txtIndDescription.ReadOnly = False
                txtVechileIndModel.ReadOnly = False
                If CType(ViewState("vsProcess"), String) = "Insert" Then
                    InsertModel()
                Else
                    UpdateModel()
                    MessageBox.Show("Ubah Sukses")
                End If
                ClearData()
                dtgModel.CurrentPageIndex = 0
                BindDataGrid(dtgModel.CurrentPageIndex)
            Else
                MessageBox.Show("Kategori tidak sesuai")
            End If
        Else
            MessageBox.Show("Data Kategori Belum dipilih")
        End If
    End Sub

    Private Sub dtgModel_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgModel.ItemDataBound
        If Not e.Item.FindControl("btnHapus") Is Nothing Then
            CType(e.Item.FindControl("btnHapus"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
        End If

        'tambahan privilege
        If Not e.Item.FindControl("btnUbah") Is Nothing Then
            CType(e.Item.FindControl("btnUbah"), LinkButton).Visible = m_bFormPrivilege
        End If

        If Not e.Item.FindControl("btnHapus") Is Nothing Then
            CType(e.Item.FindControl("btnHapus"), LinkButton).Visible = m_bFormPrivilege
        End If
        'akhir privilege

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgModel.CurrentPageIndex * dtgModel.PageSize)
        End If

        If e.Item.ItemIndex <> -1 Then
            Try
                e.Item.Cells(2).Text = CType(ListVehicleModel.Item(e.Item.ItemIndex), VechileModel).Category.Description
            Catch ex As Exception
                e.Item.Cells(2).Text = ""
            End Try

            Dim ObjVM As VechileModel = CType(e.Item.DataItem, VechileModel)

            If ObjVM.SalesFlag = 0 Then
                CType(e.Item.FindControl("lbtnInactive"), LinkButton).Visible = True
                CType(e.Item.FindControl("lbtnInactive"), LinkButton).Attributes.Add("OnClick", "return confirm('" & "Non Aktifkan Vehicle Model ?" & "');")
                CType(e.Item.FindControl("lbtnActive"), LinkButton).Visible = False
            Else
                CType(e.Item.FindControl("lbtnActive"), LinkButton).Visible = True
                CType(e.Item.FindControl("lbtnActive"), LinkButton).Attributes.Add("OnClick", "return confirm('" & "Aktifkan Vehicle Model ?" & "');")
                CType(e.Item.FindControl("lbtnInactive"), LinkButton).Visible = False
            End If
        End If
    End Sub

    Private Sub dtgModel_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgModel.ItemCommand
        Dim strMsg As String = ""
        If (e.CommandName = "View") Then
            txtKode.ReadOnly = True
            txtDeskripsi.ReadOnly = True
            txtIndDescription.ReadOnly = True
            'txtSAPCode.ReadOnly = True
            txtVechileIndModel.ReadOnly = True
            ViewState.Add("vsProcess", "View")
            ViewModel(e.Item.Cells(0).Text, False)
        ElseIf e.CommandName = "Edit" Then
            txtKode.ReadOnly = True
            txtDeskripsi.ReadOnly = False
            txtIndDescription.ReadOnly = False
            'txtSAPCode.ReadOnly = False
            txtVechileIndModel.ReadOnly = False
            ViewState.Add("vsProcess", "Edit")
            ViewModel(e.Item.Cells(0).Text, True)
            ''dtgModel.SelectedIndex = e.Item.ItemIndex
        ElseIf e.CommandName = "Delete" Then
            Try
                txtKode.ReadOnly = False
                txtDeskripsi.ReadOnly = False
                'txtSAPCode.ReadOnly = False
                txtIndDescription.ReadOnly = False
                txtVechileIndModel.ReadOnly = False

                DeleteModel(e.Item.Cells(0).Text)
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
        ElseIf e.CommandName = "aktif" Then
            ViewState("ID") = CInt(e.Item.Cells(0).Text)
            If Me.UpdateSalesFlag(strMsg, 0) Then
                ClearData()
                dtgModel.CurrentPageIndex = 0
                BindDataGrid(dtgModel.CurrentPageIndex)
            End If
            MessageBox.Show(strMsg)
        ElseIf e.CommandName = "inaktif" Then
            ViewState("ID") = CInt(e.Item.Cells(0).Text)
            If Me.UpdateSalesFlag(strMsg, 1) Then
                ClearData()
                dtgModel.CurrentPageIndex = 0
                BindDataGrid(dtgModel.CurrentPageIndex)
            End If
            MessageBox.Show(strMsg)
        End If
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        txtKode.ReadOnly = False
        txtDeskripsi.ReadOnly = False
        'txtSAPCode.ReadOnly = False
        txtIndDescription.ReadOnly = False
        txtVechileIndModel.ReadOnly = False
        ClearData()
    End Sub

    Private Sub dtgModel_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtgModel.SelectedIndexChanged

    End Sub

    Private Sub dtgModel_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgModel.PageIndexChanged
        dtgModel.SelectedIndex = -1
        dtgModel.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgModel.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgModel_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgModel.SortCommand
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

        dtgModel.SelectedIndex = -1
        dtgModel.CurrentPageIndex = 0
        BindDataGrid(dtgModel.CurrentPageIndex)
        ClearData()
    End Sub

    Private Function UpdateSalesFlag(ByRef strMsg As String, ByVal SalesFlagStatus As Integer) As Boolean
        strMsg = SR.SaveSuccess
        Try
            Dim objVM As VechileModel = New VechileModelFacade(User).Retrieve(CInt(ViewState("ID")))
            objVM.SalesFlag = SalesFlagStatus
            Dim FVechileModel As New VechileModelFacade(User)
            FVechileModel.Update(objVM)
            Return True
        Catch ex As Exception
            strMsg = "Data Gagal di Simpan"
            Return False
        End Try
        Return False
    End Function

#End Region


End Class