#Region "Code Disclaimer"
'Copyright PT. Puspa Intimedia Internusa (Intimedia) 2005

'Intimedia grants you the right to use and modify the code in this Persistence Framework
'(code under the Framework Namespace) but 
'(i) only for the solutions that are developed by Intimedia for you 
'(ii) or in solutions that are developed in join development between you and KTB.DNet.

'All rights not expressly granted, are reserved.
#End Region

#Region "Summary"
'///////////////////////////////////////////////////////////////////////////////////////
'// Author Name: 
'// PURPOSE       : Enter summary here after generation.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007
'// ---------------------
'// $History      : $
'// Generated on 7/16/2007 - 11:10:52 AM
'//
'// ===========================================================================		
#End Region

#Region ".Net Namespace"
Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography
#End Region

#Region "Custom Namespace"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.Claim
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.General

#End Region

Public Class FrmClaimDealerETA
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents valcode As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents valCondition As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtETA As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgClaimDealerETA As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button

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
    '    Private objClaimDealerETA As ClaimDealerETA
    Private arlClaimDealerETA As ArrayList
    Dim sHelper As New SessionHelper
    Private _createPriv As Boolean = False
#End Region

#Region "Custom Method"

    Private Sub InitiatePage()
        ClearData()
        ViewState("CurrentSortColumn") = "Dealer.ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub ClearData()
        txtDealerCode.Text = String.Empty
        txtETA.Text = String.Empty
        ViewState.Add("vsProcess", "Insert")
        btnSimpan.Enabled = _createPriv
        dtgClaimDealerETA.SelectedIndex = -1
        txtDealerCode.ReadOnly = False
        txtETA.ReadOnly = False
        BindDataGrid(dtgClaimDealerETA.CurrentPageIndex)
        lblSearchDealer.Visible = True
    End Sub
    Private Sub CreateCriteria()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerAdditional), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtDealerCode.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(DealerAdditional), "Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Trim.Replace(";", "','") & "')"))
        End If

        'criterias.opAnd(New Criteria(GetType(DealerAdditional), "ClaimETA", MatchType.No, 0))
        criterias.opAnd(New Criteria(GetType(DealerAdditional), "ClaimETA", MatchType.No, "0"))

        If txtETA.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(DealerAdditional), "ClaimETA", MatchType.Exact, txtETA.Text.Trim))
        End If
        sHelper.SetSession("CRITERIAS", criterias)
    End Sub
    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then

            arlClaimDealerETA = New ClaimDealerAdditionalFacade(User).RetrieveByCriteria(CType(sHelper.GetSession("CRITERIAS"), CriteriaComposite), indexPage + 1, dtgClaimDealerETA.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
                CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
            dtgClaimDealerETA.DataSource = arlClaimDealerETA
            dtgClaimDealerETA.VirtualItemCount = totalRow
            dtgClaimDealerETA.DataBind()
        End If
    End Sub
    Private Sub BindDataGrid(ByVal indexPage As Integer, ByRef RowCount As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerAdditional), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            If txtDealerCode.Text <> "" Then
                criterias.opAnd(New Criteria(GetType(DealerAdditional), "Dealer.DealerCode", MatchType.InSet, "('" & txtDealerCode.Text.Trim.Replace(";", "','") & "')"))
            End If

            'criterias.opAnd(New Criteria(GetType(DealerAdditional), "ClaimETA", MatchType.No, 0))

            If txtETA.Text <> String.Empty Then
                criterias.opAnd(New Criteria(GetType(DealerAdditional), "ClaimETA", MatchType.Exact, txtETA.Text.Trim))
            End If

            arlClaimDealerETA = New ClaimDealerAdditionalFacade(User).RetrieveByCriteria(criterias, indexPage + 1, dtgClaimDealerETA.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), _
                CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

            dtgClaimDealerETA.DataSource = arlClaimDealerETA
            dtgClaimDealerETA.VirtualItemCount = totalRow
            dtgClaimDealerETA.DataBind()
            RowCount = totalRow
        End If
    End Sub

    Private Sub UpdateClaimReason()
        Dim objCDealerETA As DealerAdditional = CType(sHelper.GetSession("ClaimDealerETA"), DealerAdditional)
        Dim nResult As Integer = -1
        Dim dealerfcd As DealerFacade = New DealerFacade(User)
        Try
            objCDealerETA.Dealer = dealerfcd.GetDealer(txtDealerCode.Text)
            objCDealerETA.ClaimETA = txtETA.Text
            nResult = New ClaimDealerAdditionalFacade(User).Update(objCDealerETA)   '-- Update Data To Database
            MessageBox.Show(SR.UpdateSucces)
        Catch ex As Exception
            MessageBox.Show(SR.UpdateFail)
        End Try
    End Sub

    Private Sub ViewDealerETA(ByVal nID As Integer, ByVal EditStatus As Boolean)
        Dim objCDealerETA As DealerAdditional = New ClaimDealerAdditionalFacade(User).Retrieve(nID)
        If Not IsNothing(objCDealerETA) Then
            'Todo session
            'Session.Add("ClaimDealerETA", objCDealerETA)
            sHelper.SetSession("ClaimDealerETA", objCDealerETA)
            txtDealerCode.Text = objCDealerETA.Dealer.DealerCode
            txtETA.Text = objCDealerETA.ClaimETA
        Else
            If EditStatus Then
                MessageBox.Show(SR.UpdateFail)
            Else
                MessageBox.Show(SR.ViewFail)
            End If
            dtgClaimDealerETA.SelectedIndex = -1
            ClearData()
        End If
    End Sub

    Private Sub DeleteDealerETA(ByVal nID As Integer)
        Dim objCDealerETA As DealerAdditional = New ClaimDealerAdditionalFacade(User).Retrieve(nID)
        Try
            Dim facade As ClaimDealerAdditionalFacade = New ClaimDealerAdditionalFacade(User)
            objCDealerETA.ClaimETA = 0
            'Operasi Delete ==> update 0
            facade.Update(objCDealerETA)
            MessageBox.Show(SR.DeleteSucces)
            dtgClaimDealerETA.CurrentPageIndex = 0
            BindDataGrid(dtgClaimDealerETA.CurrentPageIndex)
        Catch ex As Exception
            MessageBox.Show(SR.DeleteFail)
            dtgClaimDealerETA.SelectedIndex = -1
            ClearData()
        End Try
    End Sub

    Protected Function setCategoryName(ByVal oDealerID As Object) As String
        Dim Retval As String = ""
        Dim objDealer As New Dealer
        ' check wether the id is entered in db
        If Not IsNothing(oDealerID) Then
            Dim nID As Integer = CInt(oDealerID)
            Dim _DealerFacade As DealerFacade = New DealerFacade(User)

            ' give separator to separate data
            ' because in display it will looks like ex SP--SP1
            ' then we give separator '--'
            Dim separator As String = "--"

            ' finally get data
            objDealer = _DealerFacade.GetDealer(oDealerID)
            Retval = objDealer.DealerCode
        End If

        Return Retval
    End Function
#End Region

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.PerkiaraanKedatanganView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=CLAIM - Perkiraan Kedatangan")
        End If
    End Sub

    Private Function CekBtnPriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PerkiraanKedatanganCreate_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

#Region "Event Hendlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()
        _createPriv = CekBtnPriv()
        If Not IsPostBack Then
            ClearData()
            InitiatePage()
            dtgClaimDealerETA.CurrentPageIndex = 0
            CreateCriteria()
            BindDataGrid(dtgClaimDealerETA.CurrentPageIndex)
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelectionOne();"
        End If
        If btnSimpan.Enabled = True Then
            btnSimpan.Enabled = _createPriv
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        If Val(txtETA.Text) = 0 Then
            MessageBox.Show("Nilai Estimasi tidak boleh 0")
            Return
        End If
        Dim objCDealerETA As DealerAdditional = New DealerAdditional
        Dim objCDealerETAFacade As ClaimDealerAdditionalFacade = New ClaimDealerAdditionalFacade(User)
        Dim nResult As Integer = -1
        Dim succeed As Integer = 0
        Dim exist As Integer = 0
        Dim failed As Integer = 0
        If CType(ViewState("vsProcess"), String) = "Insert" Then  '-- If Condition is Insert
            Dim dealerfcd As DealerFacade = New DealerFacade(User)
            For Each i As String In CStr(txtDealerCode.Text).Split(";")
                Try
                    If objCDealerETAFacade.ValidateCodeETA(dealerfcd.GetDealer(i).ID.ToString) = 0 Then
                        If objCDealerETAFacade.ValidateCode(dealerfcd.GetDealer(i).ID.ToString) = 0 Then
                            objCDealerETA.Dealer = dealerfcd.GetDealer(i)
                            objCDealerETA.ClaimETA = txtETA.Text
                            nResult = New ClaimDealerAdditionalFacade(User).Insert(objCDealerETA)
                        Else
                            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerAdditional), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias.opAnd(New Criteria(GetType(DealerAdditional), "Dealer.ID", MatchType.Exact, dealerfcd.GetDealer(i).ID.ToString))

                            objCDealerETA = objCDealerETAFacade.Retrieve(criterias)(0)
                            objCDealerETA.ClaimETA = txtETA.Text
                            nResult = New ClaimDealerAdditionalFacade(User).Update(objCDealerETA)

                        End If
                        succeed = succeed + 1
                    Else
                        exist = exist + 1
                    End If
                Catch ex As Exception
                    failed = failed + 1
                End Try
            Next
            alertMessage(succeed, exist, failed)
        Else
            UpdateClaimReason() '-- Update Change
        End If

        ClearData()
        'dtgClaimDealerETA.CurrentPageIndex = 0
        BindDataGrid(dtgClaimDealerETA.CurrentPageIndex)
    End Sub

    Private Sub alertMessage(ByVal succeed1 As Integer, ByVal exist1 As Integer, ByVal failed1 As Integer)
        Dim message As String = ""
        If succeed1 > 0 Then
            message = message + succeed1.ToString + " berhasil disimpan"
        End If

        If exist1 > 0 Then
            If message <> "" Then
                message = message + ", "
            End If
            message = message + exist1.ToString + " data kode sudah ada"
        End If

        If failed1 > 0 Then
            If message <> "" Then
                message = message + ", "
            End If
            message = message + failed1.ToString + " data gagal"
        End If

        MessageBox.Show(message)
    End Sub

    Private Sub btnBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
    End Sub

    Private Sub dtgClaimDealerETA_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgClaimDealerETA.ItemCommand
        If (e.CommandName = "View") Then             '-- View Condition
            ViewState.Add("vsProcess", "View")
            ViewDealerETA(e.Item.Cells(0).Text, False)
            txtDealerCode.ReadOnly = True
            txtDealerCode.Visible = True
            txtETA.ReadOnly = True
            btnSimpan.Enabled = False
        ElseIf e.CommandName = "Edit" Then           '-- Edit/Update Condition
            ViewState.Add("vsProcess", "Edit")
            ViewDealerETA(e.Item.Cells(0).Text, True)
            dtgClaimDealerETA.SelectedIndex = e.Item.ItemIndex
            txtDealerCode.ReadOnly = True
            lblSearchDealer.Visible = False
            txtETA.ReadOnly = False
            btnSimpan.Enabled = True
        ElseIf e.CommandName = "Delete" Then         '-- Delete Permanentely Conditon
            DeleteDealerETA(e.Item.Cells(0).Text)
            ClearData()
        End If
    End Sub

    Private Sub dtgClaimDealerETA_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgClaimDealerETA.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim _lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            _lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
            e.Item.Cells(1).Text = (dtgClaimDealerETA.CurrentPageIndex * dtgClaimDealerETA.PageSize + e.Item.ItemIndex + 1).ToString()  '-- Column No
            Dim _lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            Dim _lbtnView As LinkButton = CType(e.Item.FindControl("lbtnView"), LinkButton)
            If _lbtnDelete.Visible = True Then
                _lbtnDelete.Visible = _createPriv
            End If
            If _lbtnEdit.Visible = True Then
                _lbtnEdit.Visible = _createPriv
            End If
            If _lbtnView.Visible = True Then
                _lbtnView.Visible = _createPriv
            End If


            Dim lblHistoryStatus As Label = e.Item.FindControl("lblHistoryStatus")
            Dim oDA As DealerAdditional = e.Item.DataItem
            Dim sUrl As String = "../Popup/PopupDataHistory.aspx?TableName=DealerAdditional&TableID=" & oDA.ID.ToString
            sUrl = "ShowDataHistory('" & sUrl & "');"
            lblHistoryStatus.Attributes.Add("OnClick", sUrl)
        End If
    End Sub

    Private Sub dtgClaimDealerETA_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgClaimDealerETA.PageIndexChanged
        'Dim CurrentPageIndex As Integer
        'dtgClaimDealerETA.SelectedIndex = -1
        'CurrentPageIndex = e.NewPageIndex
        'dtgClaimDealerETA.CurrentPageIndex = 0

        'Dim RowCount As Integer
        'Dim ResultTotalPage As Integer
        'Dim d_cal As Decimal
        'Dim i_cal As Integer

        'BindDataGrid(dtgClaimDealerETA.CurrentPageIndex, RowCount)
        'd_cal = RowCount / dtgClaimDealerETA.PageSize
        'i_cal = RowCount / dtgClaimDealerETA.PageSize

        'If d_cal > i_cal Then
        '    ResultTotalPage = i_cal + 1
        'Else
        '    ResultTotalPage = i_cal
        'End If

        'If CurrentPageIndex > ResultTotalPage - 1 Then
        '    dtgClaimDealerETA.CurrentPageIndex = ResultTotalPage - 1
        'Else
        '    dtgClaimDealerETA.CurrentPageIndex = CurrentPageIndex
        'End If

        'BindDataGrid(dtgClaimDealerETA.CurrentPageIndex)
        ''ClearData()
        dtgClaimDealerETA.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgClaimDealerETA.CurrentPageIndex)
    End Sub

    Private Sub dtgClaimDealerETA_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgClaimDealerETA.SortCommand
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

        dtgClaimDealerETA.SelectedIndex = -1
        BindDataGrid(dtgClaimDealerETA.CurrentPageIndex)
        ClearData()
    End Sub

#End Region



    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        dtgClaimDealerETA.CurrentPageIndex = 0
        CreateCriteria()
        BindDataGrid(dtgClaimDealerETA.CurrentPageIndex)
    End Sub
End Class
