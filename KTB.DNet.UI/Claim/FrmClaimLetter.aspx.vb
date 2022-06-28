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

#End Region

Public Class FrmClaimLetter
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents dtgClaimCCofLetter As System.Web.UI.WebControls.DataGrid
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents dtgClaimSignOfLetter As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtCCList As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPosition As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSimpanCCofLetter As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpanSignofLetter As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatalCCofLetter As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatalSignofLetter As System.Web.UI.WebControls.Button
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents txtheader As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents txtfooter As System.Web.UI.WebControls.TextBox
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button

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
    Private objClaimCCofLetter As ClaimCCofLetter
    Private arlClaimCCofLetter As ArrayList
    Private objClaimSignofLetter As ClaimSignofLetter
    Private arlClaimSignofLetter As ArrayList
    Dim sHelper As New SessionHelper
#End Region

#Region "CustomMethod"
    Private Sub InitiatePage()
        ClearData()
        ViewState("CurrentSortColumnCCofLetter") = "CCList"
        ViewState("CurrentSortColumnSignofLetter") = "Name"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub ClearData()
        'Clear Data CCofLetter
        txtCCList.Text() = String.Empty
        'btnSimpanCCofLetter.Enabled = True
        dtgClaimCCofLetter.SelectedIndex = -1
        BindDGCCofLetter(dtgClaimCCofLetter.CurrentPageIndex)

        'Clear Data SignofLetter
        txtName.Text() = String.Empty
        txtPosition.Text = String.Empty
        'btnSimpanSignofLetter.Enabled = True
        dtgClaimSignOfLetter.SelectedIndex = -1
        BindDGSignofLetter(dtgClaimSignOfLetter.CurrentPageIndex)

        txtCCList.ReadOnly = False
        txtName.ReadOnly = False
        txtPosition.ReadOnly = False

        ViewState.Add("vsProcess", "InsertCCofLetter")
        ViewState.Add("vsProcess1", "InsertSignofLetter")

        If CekBtnPrivCC() Then
            btnSimpanCCofLetter.Enabled = True
        Else
            btnSimpanCCofLetter.Enabled = False
        End If

        If CekBtnPrivPenandaTgn() Then
            btnSimpanSignofLetter.Enabled = True
        Else
            btnSimpanSignofLetter.Enabled = False
        End If

    End Sub

    Private Sub bindHF()
        Dim oLookup As LookUp = New LookUp
    End Sub
    Private Sub BindDGCCofLetter(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            arlClaimCCofLetter = New ClaimCCofLetterFacade(User).RetrieveActiveList(indexPage + 1, dtgClaimCCofLetter.PageSize, totalRow, CType(ViewState("CurrentSortColumnCCofLetter"), String), _
                CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgClaimCCofLetter.DataSource = arlClaimCCofLetter
            dtgClaimCCofLetter.VirtualItemCount = totalRow
            dtgClaimCCofLetter.DataBind()
        End If
    End Sub

    Private Sub BindDGSignofLetter(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            arlClaimSignofLetter = New ClaimSignofLetterFacade(User).RetrieveActiveList(indexPage + 1, dtgClaimSignOfLetter.PageSize, totalRow, CType(ViewState("CurrentSortColumnSignofLetter"), String), _
                CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgClaimSignOfLetter.DataSource = arlClaimSignofLetter
            dtgClaimSignOfLetter.VirtualItemCount = totalRow
            If totalRow > 0 Then
                btnSimpanSignofLetter.Enabled = False
            Else
                btnSimpanSignofLetter.Enabled = True
            End If
            dtgClaimSignOfLetter.DataBind()
        End If
    End Sub

    Private Sub ViewCCofLetter(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objCCCofLetter As ClaimCCofLetter = New ClaimCCofLetterFacade(User).Retrieve(nID)
        If Not IsNothing(objCCCofLetter) Then
            'Todo session
            'Session.Add("ClaimCCofLetter", objCCCofLetter)
            sHelper.SetSession("ClaimCCofLetter", objCCCofLetter)
            txtCCList.Text = objCCCofLetter.CCList
        Else
            If EditStatus Then
                MessageBox.Show(SR.UpdateFail)
            Else
                MessageBox.Show(SR.ViewFail)
            End If
            dtgClaimCCofLetter.SelectedIndex = -1
            ClearData()
        End If
    End Sub

    Private Sub DeleteCCofLetter(ByVal nID As Integer)
        Dim objCCCofLetter As ClaimCCofLetter = New ClaimCCofLetterFacade(User).Retrieve(nID)
        Try
            Dim facade As ClaimCCofLetterFacade = New ClaimCCofLetterFacade(User)
            facade.DeleteFromDB(objCCCofLetter)
            MessageBox.Show(SR.DeleteSucces)
            dtgClaimCCofLetter.CurrentPageIndex = 0
            BindDGCCofLetter(dtgClaimCCofLetter.CurrentPageIndex)
        Catch ex As Exception
            MessageBox.Show(SR.DeleteFail)
            dtgClaimCCofLetter.SelectedIndex = -1
            ClearData()
        End Try
    End Sub


    Private Sub ViewSignofLetter(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objCSignofLetter As ClaimSignofLetter = New ClaimSignofLetterFacade(User).Retrieve(nID)
        If Not IsNothing(objCSignofLetter) Then
            'Todo session
            'Session.Add("ClaimSignofLetter", objCSignofLetter)
            sHelper.SetSession("ClaimSignofLetter", objCSignofLetter)
            txtName.Text = objCSignofLetter.Name
            txtPosition.Text = objCSignofLetter.Position
        Else
            If EditStatus Then
                MessageBox.Show(SR.UpdateFail)
            Else
                MessageBox.Show(SR.ViewFail)
            End If
            dtgClaimSignOfLetter.SelectedIndex = -1
            ClearData()
        End If
    End Sub

    Private Sub DeleteSignofLetter(ByVal nID As Integer)
        Dim objCSignofLetter As ClaimSignofLetter = New ClaimSignofLetterFacade(User).Retrieve(nID)
        Try
            Dim facade As ClaimSignofLetterFacade = New ClaimSignofLetterFacade(User)
            facade.DeleteFromDB(objCSignofLetter)
            MessageBox.Show(SR.DeleteSucces)
            dtgClaimSignOfLetter.CurrentPageIndex = 0
            BindDGSignofLetter(dtgClaimSignOfLetter.CurrentPageIndex)
        Catch ex As Exception
            MessageBox.Show(SR.DeleteFail)
            dtgClaimSignOfLetter.SelectedIndex = -1
            ClearData()
        End Try
    End Sub
#End Region

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.ParameterSuratView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=CLAIM - Parameter Surat")
        End If
    End Sub

    Private Function CekBtnPrivCC() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.ParameterSuratCCCreatelist_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
    Private Function CekBtnPrivPenandaTgn() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PenandaTanganParameterSuratCreate_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

#Region "Event Handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()
        If Not IsPostBack Then
            'If Not SecurityProvider.Authorize(Context.User, SR) Then '-- Check User Privilege
            '    Response.Redirect("../frmAccessDenied.aspx?modulName=Parameter Surat")
            'End If
            InitiatePage()
            BindDGCCofLetter(0)
            BindDGSignofLetter(0)
            btnSimpanCCofLetter.Attributes.Add("onclick", "return checkVal();")
        End If
        If CekBtnPrivCC() Then
            btnSimpanCCofLetter.Enabled = True
        Else
            btnSimpanCCofLetter.Enabled = False
        End If

        If CekBtnPrivPenandaTgn() Then
            btnSimpanSignofLetter.Enabled = True
        Else
            btnSimpanSignofLetter.Enabled = False
        End If

    End Sub

    Private Sub btnSimpanCCofLetter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpanCCofLetter.Click
        If Not Page.IsValid Then
            Return
        End If
        Dim nResult As Integer = -1
        If CType(ViewState("vsProcess"), String) = "InsertCCofLetter" Then  '-- If Condition is Insert
            Dim objCCCofLeter As ClaimCCofLetter = New ClaimCCofLetter
            'Dim objCCCofLetterFacade As ClaimCCofLetterFacade = New ClaimCCofLetterFacade(User)
            objCCCofLeter.CCList = txtCCList.Text
            If New ClaimCCofLetterFacade(User).ValidateCC(objCCCofLeter.CCList.Trim) > 0 Then
                MessageBox.Show("Nama '" & objCCCofLeter.CCList & "' sudah pernah dimasukkan")
            Else
                nResult = New ClaimCCofLetterFacade(User).Insert(objCCCofLeter)
                If nResult = -1 Then
                    MessageBox.Show(SR.SaveFail)
                Else
                    MessageBox.Show(SR.SaveSuccess)
                End If
            End If
        Else
            Dim objCCCofLeter As ClaimCCofLetter = CType(sHelper.GetSession("ClaimCCofLetter"), ClaimCCofLetter)
            'Dim objCCCofLetterFacade As ClaimCCofLetterFacade = New ClaimCCofLetterFacade(User)
            objCCCofLeter.CCList = txtCCList.Text
            Try
                nResult = New ClaimCCofLetterFacade(User).Update(objCCCofLeter)   '-- Update Data To Database
                MessageBox.Show(SR.UpdateSucces)
                '     BindDGCCofLetter(0)
            Catch ex As Exception
                MessageBox.Show(SR.UpdateFail)
            End Try
        End If

        ClearData()
        dtgClaimCCofLetter.CurrentPageIndex = 0
        BindDGCCofLetter(dtgClaimCCofLetter.CurrentPageIndex)
    End Sub

    Private Sub btnSimpanSignofLetter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpanSignofLetter.Click
        If Not Page.IsValid Then
            Return
        End If
        Dim nResult As Integer = -1
        If CType(ViewState("vsProcess1"), String) = "InsertSignofLetter" Then  '-- If Condition is Insert
            Dim objCSignofLetter As ClaimSignofLetter = New ClaimSignofLetter
            'Dim objCsofLetterFacade As ClaimCCofLetterFacade = New ClaimCCofLetterFacade(User)

            Dim arl As ArrayList = New ClaimSignofLetterFacade(User).RetrieveActiveList()

            If arl.Count > 0 Then
                MessageBox.Show("Data Sudah Ada")
                Exit Sub
            End If

            objCSignofLetter.Name = txtName.Text
            objCSignofLetter.Position = txtPosition.Text
            nResult = New ClaimSignofLetterFacade(User).Insert(objCSignofLetter)
            If nResult = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)
            End If
        Else
            Dim objCSignofLetter As ClaimSignofLetter = CType(sHelper.GetSession("ClaimSignofLetter"), ClaimSignofLetter)
            'Dim objCCCofLetterFacade As ClaimCCofLetterFacade = New ClaimCCofLetterFacade(User)
            objCSignofLetter.Name = txtName.Text
            objCSignofLetter.Position = txtPosition.Text
            Try
                nResult = New ClaimSignofLetterFacade(User).Update(objCSignofLetter)   '-- Update Data To Database
                MessageBox.Show(SR.UpdateSucces)
                '          BindDGSignofLetter(0)
            Catch ex As Exception
                MessageBox.Show(SR.UpdateFail)
            End Try
        End If

        ClearData()
        dtgClaimSignOfLetter.CurrentPageIndex = 0
        BindDGSignofLetter(dtgClaimSignOfLetter.CurrentPageIndex)
    End Sub

    Private Sub dtgClaimCCofLetter_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgClaimCCofLetter.ItemCommand
        If (e.CommandName = "View") Then             '-- View Condition
            ViewState.Add("vsProcess", "View")
            ViewCCofLetter(e.Item.Cells(0).Text, False)
            txtCCList.ReadOnly = True
            btnSimpanCCofLetter.Enabled = False
        ElseIf e.CommandName = "Edit" Then           '-- Edit/Update Condition
            ViewState.Add("vsProcess", "Edit")
            ViewCCofLetter(e.Item.Cells(0).Text, True)
            dtgClaimCCofLetter.SelectedIndex = e.Item.ItemIndex
            txtCCList.ReadOnly = False
            btnSimpanCCofLetter.Enabled = True
        ElseIf e.CommandName = "Delete" Then         '-- Delete Permanentely Conditon
            DeleteCCofLetter(e.Item.Cells(0).Text)
            ClearData()
        End If
    End Sub

    Private Sub dtgClaimSignOfLetter_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgClaimSignOfLetter.ItemCommand
        If (e.CommandName = "View") Then             '-- View Condition
            ViewState.Add("vsProcess1", "View")
            ViewSignofLetter(e.Item.Cells(0).Text, False)
            txtName.ReadOnly = True
            txtPosition.ReadOnly = True
            btnSimpanSignofLetter.Enabled = False
        ElseIf e.CommandName = "Edit" Then           '-- Edit/Update Condition
            ViewState.Add("vsProcess1", "Edit")
            ViewSignofLetter(e.Item.Cells(0).Text, True)
            dtgClaimSignOfLetter.SelectedIndex = e.Item.ItemIndex
            txtName.ReadOnly = False
            txtPosition.ReadOnly = False
            btnSimpanSignofLetter.Enabled = True
        ElseIf e.CommandName = "Delete" Then         '-- Delete Permanentely Conditon
            DeleteSignofLetter(e.Item.Cells(0).Text)
            ClearData()
        End If
    End Sub

    Private Sub dtgClaimSignOfLetter_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgClaimSignOfLetter.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim _lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDeleteSignofLetter"), LinkButton)
            _lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
            e.Item.Cells(1).Text = (dtgClaimSignOfLetter.CurrentPageIndex * dtgClaimSignOfLetter.PageSize + e.Item.ItemIndex + 1).ToString()  '-- Column No
            Dim _lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEditSignofLetter"), LinkButton)
            Dim _lbtnView As LinkButton = CType(e.Item.FindControl("lbtnViewSignofLetter"), LinkButton)
            If CekBtnPrivPenandaTgn() Then
                _lbtnEdit.Visible = True
                _lbtnDelete.Visible = True
                _lbtnView.Visible = True
            Else
                _lbtnEdit.Visible = False
                _lbtnDelete.Visible = False
                _lbtnView.Visible = False
            End If
        End If
    End Sub

    Private Sub dtgClaimCCofLetter_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgClaimCCofLetter.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim _lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDeleteCCofLetter"), LinkButton)
            _lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
            e.Item.Cells(1).Text = (dtgClaimCCofLetter.CurrentPageIndex * dtgClaimCCofLetter.PageSize + e.Item.ItemIndex + 1).ToString()  '-- Column No
            Dim _lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEditCCofLetter"), LinkButton)
            Dim _lbtnView As LinkButton = CType(e.Item.FindControl("lbtnViewCCofLetter"), LinkButton)
            If CekBtnPrivCC() Then
                _lbtnEdit.Visible = True
                _lbtnDelete.Visible = True
                _lbtnView.Visible = True
            Else
                _lbtnEdit.Visible = False
                _lbtnDelete.Visible = False
                _lbtnView.Visible = False
            End If
        End If
    End Sub

    Private Sub dtgClaimCCofLetter_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgClaimCCofLetter.PageIndexChanged
        dtgClaimCCofLetter.SelectedIndex = -1
        dtgClaimCCofLetter.CurrentPageIndex = e.NewPageIndex
        BindDGCCofLetter(dtgClaimCCofLetter.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgClaimCCofLetter_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgClaimCCofLetter.SortCommand
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

        dtgClaimCCofLetter.SelectedIndex = -1
        dtgClaimCCofLetter.CurrentPageIndex = 0
        BindDGCCofLetter(dtgClaimCCofLetter.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgClaimSignOfLetter_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgClaimSignOfLetter.PageIndexChanged
        dtgClaimSignOfLetter.SelectedIndex = -1
        dtgClaimSignOfLetter.CurrentPageIndex = e.NewPageIndex
        BindDGSignofLetter(dtgClaimSignOfLetter.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub dtgClaimSignOfLetter_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgClaimSignOfLetter.SortCommand
        'If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
        '    Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)

        '        Case Sort.SortDirection.ASC
        '            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

        '        Case Sort.SortDirection.DESC
        '            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        '    End Select
        'Else
        '    ViewState("CurrentSortColumn") = e.SortExpression
        '    ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        'End If

        'dtgClaimSignOfLetter.SelectedIndex = -1
        'dtgClaimSignOfLetter.CurrentPageIndex = 0
        'BindDGSignofLetter(dtgClaimSignOfLetter.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub btnBatalCCofLetter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatalCCofLetter.Click
        ClearData()
    End Sub

    Private Sub btnBatalSignofLetter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatalSignofLetter.Click
        ClearData()
    End Sub
#End Region

End Class
