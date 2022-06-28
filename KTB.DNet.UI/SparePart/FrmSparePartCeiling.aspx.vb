Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Security

Public Class FrmSparePartCeiling
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblName As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon2 As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents dgDepositList As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblCreditAcct As System.Web.UI.WebControls.Label
    Protected WithEvents txtCreditAcct As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchCreditAcct As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerGrid As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Declaration"
    Private sessHelper As New SessionHelper
    Private oLoginUser As UserInfo
#End Region

#Region "Events"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'oLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()
        If Not IsNothing(Request.Form("txtDealerName")) Then
            Me.lblDealerName.Text = Request.Form("txtDealerName").ToString
        End If
        If Not IsPostBack Then
            'ClearForm()
            'GetSessionCriteria()
        End If
        lblSearchCreditAcct.Attributes("onclick") = "ShowPPCreditAcctSelection();"
        Dim objDealer As Dealer = sessHelper.GetSession("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            dgDepositList.CurrentPageIndex = 0
            BindToGrid(dgDepositList.CurrentPageIndex)
        End If
        txtCreditAcct.Attributes.Add("readonly", "readonly")
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim objDealer As Dealer = sessHelper.GetSession("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            If txtCreditAcct.Text = "" Then
                MessageBox.Show("Account Group Tidak boleh Kosong")
                Return
            Else
                If Not isCreditAccountValid(txtCreditAcct.Text) Then
                    MessageBox.Show("Ada credit account yg tidak valid.")
                    Return
                End If
            End If
        End If

        dgDepositList.CurrentPageIndex = 0
        BindToGrid(dgDepositList.CurrentPageIndex)
    End Sub

    Private Sub dgDepositList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgDepositList.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim objCreditmAsterSP As CreditMasterSP = CType(e.Item.DataItem, CreditMasterSP)

            If ItemType = ListItemType.Item Or ItemType = ListItemType.AlternatingItem Then
                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = (dgDepositList.CurrentPageIndex * dgDepositList.PageSize + e.Item.ItemIndex + 1).ToString()  '-- Column No

                If objCreditmAsterSP.CeilingBalance <= (objCreditmAsterSP.Ceiling * 0.1) Then
                    e.Item.BackColor = System.Drawing.Color.Red
                End If
            End If

            Dim objDealer As Dealer = New DealerFacade(User).Retrieve(objCreditmAsterSP.CreditAccount)
            e.Item.Cells(3).Text = objDealer.DealerName
        End If
    End Sub

    Private Sub dgDepositList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgDepositList.PageIndexChanged
        dgDepositList.CurrentPageIndex = e.NewPageIndex
        BindToGrid(dgDepositList.CurrentPageIndex)
    End Sub

    Private Sub dgDepositList_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgDepositList.SortCommand
        If CType(ViewState("currSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currSortDirection"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("currSortDirection") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    ViewState("currSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currSortColumn") = e.SortExpression
            ViewState("currSortDirection") = Sort.SortDirection.ASC
        End If
        dgDepositList.SelectedIndex = -1
        dgDepositList.CurrentPageIndex = 0
        BindToGrid(dgDepositList.CurrentPageIndex)
    End Sub

#End Region

#Region "Custom"

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.Sparepart_ceiling_lihat_privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Parts Deposit Spare Part Ceiling – Lihat")
        End If
    End Sub

    Private Sub ClearForm()
        txtCreditAcct.Text = ""
        lblDealerName.Text = ""
    End Sub

    Private Sub GetSessionCriteria()
        'Dim arrLastState As ArrayList = sessHelper.GetSession("POSISIKREDITSESSIONLASTSTATE")
        'If Not arrLastState Is Nothing Then
        '    txtCreditAcct.Text = arrLastState.Item(0)

        '    dgDepositList.CurrentPageIndex = arrLastState.Item(2)
        '    ViewState("currSortColumn") = arrLastState.Item(3)
        '    ViewState("currSortDirection") = arrLastState.Item(4)
        'Else
        ViewState("currSortColumn") = "CreditAccount"
        ViewState("currSortDirection") = Sort.SortDirection.ASC
        dgDepositList.CurrentPageIndex = 0

        'End If
    End Sub

    Private Function isCreditAccountValid(ByVal sListLegalStatus As String) As Boolean

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "Status", MatchType.Exact, CType(EnumDealerStatus.DealerStatus.Aktive, String)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "LegalStatus", MatchType.No, String.Empty))

        Dim DistinctArl As New ArrayList
        Dim arl As ArrayList = New DealerFacade(User).RetrieveActiveList(criterias, viewstate("SortColDealer"), viewstate("SortDirDealer"))

        For Each item As Dealer In arl
            If DistinctArl.Count = 0 Then
                DistinctArl.Add(item)
            Else
                Dim isNew As Boolean = True
                For Each dItem As Dealer In DistinctArl
                    If dItem.LegalStatus = item.LegalStatus Then
                        isNew = False
                        Exit For
                    End If
                Next

                If isNew Then
                    DistinctArl.Add(item)
                End If
            End If
        Next

        Dim isValid As Boolean
        For Each sCreditAcct As String In sListLegalStatus.Split(";")
            isValid = False
            For Each ValidDealer As Dealer In DistinctArl
                If ValidDealer.LegalStatus = sCreditAcct Then
                    isValid = True
                End If
            Next

            If Not isValid Then
                Exit For
            End If
        Next

        Return isValid


    End Function

    Sub BindToGrid(ByVal currentPageIndex As Integer)
        Dim totalRow As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.CreditMasterSP), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim objDealer As Dealer = sessHelper.GetSession("DEALER")
        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If (txtCreditAcct.Text.Trim() <> String.Empty) Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CreditMasterSP), "CreditAccount", MatchType.InSet, "('" & txtCreditAcct.Text.Trim().Replace(";", "','") & "')"))
            End If
        Else
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.CreditMasterSP), "CreditAccount", MatchType.Exact, txtCreditAcct.Text.Trim()))
        End If

        'Dim sortColl As KTB.Dnet.Domain.Search.SortCollection = New KTB.Dnet.Domain.Search.SortCollection
        'If (Not IsNothing(ViewState("currSortColumn"))) And (Not IsNothing(ViewState("currSortDirection"))) Then
        '    sortColl.Add(New KTB.Dnet.Domain.Search.Sort(GetType(CreditMasterSP), ViewState("currSortColumn"), ViewState("currSortDirection")))
        'Else
        '    sortColl = Nothing
        'End If

        Dim arlCreditMasterSP As ArrayList = New KTB.DNet.BusinessFacade.PO.CreditMasterSPFacade(User).RetrieveActiveList(criterias, currentPageIndex + 1, dgDepositList.PageSize, totalRow, ViewState("currSortColumn"), ViewState("currSortDirection"))
        
        If (arlCreditMasterSP.Count > 0) Then
            'dgDepositList.CurrentPageIndex = currentPageIndex
            dgDepositList.DataSource = arlCreditMasterSP
            dgDepositList.VirtualItemCount = totalRow
            dgDepositList.DataBind()

        Else
            MessageBox.Show("Data tidak ditemukan")
        End If
    End Sub
#End Region

End Class
