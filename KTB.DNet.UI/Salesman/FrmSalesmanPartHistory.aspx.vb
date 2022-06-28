Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Salesman

Public Class FrmSalesmanPartHistory
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents dgSalesmanHeader As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblSalesmanID As System.Web.UI.WebControls.Label
    Protected WithEvents txtID As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblShowSalesman As System.Web.UI.WebControls.Label
    Protected WithEvents txtNama As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents lblSalesmanCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblNama As System.Web.UI.WebControls.Label
    Protected WithEvents lblSalesCode As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private sessHelper As New SessionHelper
    Private arlHistory As New ArrayList
    Private objDealer As Dealer
    Private approval As Boolean

#Region "Event"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckPrivilege()
        lblPageTitle.Text = "Historical Employee Part"
        If Not IsPostBack Then
            objDealer = (CType(sessHelper.GetSession("Dealer"), Dealer))
            If Not Request.QueryString("dealer") Is Nothing Then
                lblDealer.Text = Request.QueryString("dealer")
            End If
            If Not Request.QueryString("code") Is Nothing Then
                lblSalesmanCode.Text = Request.QueryString("code")
            End If
            If Not Request.QueryString("nama") Is Nothing Then
                lblNama.Text = Request.QueryString("nama")
            End If
            BindGrid()
        End If
    End Sub

    Private Sub CheckPrivilege()
        approval = SecurityProvider.Authorize(context.User, SR.buat_buatid_salesman_part_privilege)
    End Sub

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("FrmSalesmanPartList.aspx?Mode=part")
    End Sub

    Private Sub dgSalesmanHeader_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSalesmanHeader.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objSalesmanHistory As SalesmanPartHistory = e.Item.DataItem
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgSalesmanHeader.CurrentPageIndex * dgSalesmanHeader.PageSize)
            Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
            lblDealerCode.Text = objSalesmanHistory.Dealer.DealerCode

            'If Not IsNothing(objSalesmanHistory.SalesmanHeader.DealerBranch) Then
            '    Dim lblDealerBranchCode As Label = CType(e.Item.FindControl("lblDealerBranchCode"), Label)
            '    lblDealerBranchCode.Text = objSalesmanHistory.SalesmanHeader.DealerBranch.DealerBranchCode
            'End If

            Dim lblSalesCode As Label = CType(e.Item.FindControl("lblSalesCode"), Label)
            lblSalesCode.Text = objSalesmanHistory.SalesmanCode

            Dim lblKategori As Label = CType(e.Item.FindControl("lblKategori"), Label)
            Dim lblPosisi As Label = CType(e.Item.FindControl("lblPosisi"), Label)
            If Not IsNothing(objSalesmanHistory.SalesmanCategoryLevel) Then
                lblKategori.Text = objSalesmanHistory.SalesmanCategoryLevel.SalesmanCategoryLevel.PositionName
                lblPosisi.Text = objSalesmanHistory.SalesmanCategoryLevel.PositionName
            Else
                lblKategori.Text = ""
                lblPosisi.Text = ""
            End If

            Dim lblLevel As Label = CType(e.Item.FindControl("lblLevel"), Label)
            If objSalesmanHistory.SalesmanLevel <> 99 Then
                Dim EnumLevel As EnumSalesmanPartLevel.Level = objSalesmanHistory.SalesmanLevel
                lblLevel.Text = EnumLevel.ToString
            Else
                lblLevel.Text = String.Empty
            End If

            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            If objSalesmanHistory.Status = 5 Then
                Select Case objSalesmanHistory.SalesmanHeader.ResignType
                    Case 1
                        lblStatus.Text = "Resign Dari Dealer"
                    Case 2
                        lblStatus.Text = "Mutasi Ke Departemen Lain"
                End Select
            Else
                lblStatus.Text = CType(objSalesmanHistory.Status, HistoryStatus).ToString
            End If

            Dim lbtnApprove As LinkButton = CType(e.Item.FindControl("lbtnApprove"), LinkButton)
            Dim lbtnReject As LinkButton = CType(e.Item.FindControl("lbtnReject"), LinkButton)
            lbtnApprove.Visible = False
            lbtnReject.Visible = False
            If objSalesmanHistory.SalesmanHeader.RegisterStatus = "1" And objSalesmanHistory.Status = HistoryStatus.Pengajuan Then
                objDealer = (CType(sessHelper.GetSession("Dealer"), Dealer))
                If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                    lbtnApprove.Visible = approval
                    lbtnReject.Visible = approval
                End If
            End If
        End If

    End Sub

    Private Sub dgSalesmanHeader_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSalesmanHeader.ItemCommand
        Dim salesmanHistoryID As Integer = CType(e.Item.Cells(0).Text, Integer)
        Dim oSalesHistory As SalesmanPartHistory = New SalesmanPartHistoryFacade(User).Retrieve(salesmanHistoryID)
        Dim iReturn As Integer
        If Not oSalesHistory Is Nothing Then
            If (e.CommandName = "Approve") Then
                iReturn = UpdateSalesmanAdditionalInfo(oSalesHistory) '<> -1
                If iReturn <> -1 Then
                    Dim objSalesmanHeader As SalesmanHeader = New SalesmanHeaderFacade(User).Retrieve(oSalesHistory.SalesmanHeader.ID)
                    oSalesHistory.SalesmanCode = objSalesmanHeader.SalesmanCode
                    iReturn = UpdateSalesmanPartHistory(oSalesHistory, CType(HistoryStatus.Disetujui, Integer)) '<> -1
                    If iReturn <> -1 Then
                        MessageBox.Show(SR.UpdateSucces)
                    Else
                        MessageBox.Show(SR.UpdateFail)
                    End If
                End If
            ElseIf e.CommandName = "Reject" Then
                UpdateSalesmanPartHistory(oSalesHistory, CType(HistoryStatus.Ditolak, Integer))
            End If
        End If
        BindDataHistory(Me.dgSalesmanHeader.CurrentPageIndex)
    End Sub
#End Region

#Region "Custom"

    Private Sub BindGrid()
        ViewState("CurrentSortColumn") = "CreatedTime"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        Me.dgSalesmanHeader.CurrentPageIndex = 0
        BindDataHistory(Me.dgSalesmanHeader.CurrentPageIndex)
    End Sub

    Private Sub BindDataHistory(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0

        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanPartHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If Not Request.QueryString("ID") Is Nothing Then
            criterias.opAnd(New Criteria(GetType(SalesmanPartHistory), "SalesmanHeader.ID", MatchType.Exact, Request.QueryString("ID")))
        End If

        arlHistory = New SalesmanPartHistoryFacade(User).RetrieveByCriteria(criterias, idxPage + 1, dgSalesmanHeader.PageSize, totalRow, _
            sessHelper.GetSession("CurrentSortColumn"), sessHelper.GetSession("CurrentSortDirect"))

        dgSalesmanHeader.CurrentPageIndex = idxPage
        dgSalesmanHeader.DataSource = arlHistory
        dgSalesmanHeader.VirtualItemCount = totalRow

        dgSalesmanHeader.DataBind()
    End Sub

    Private Function UpdateSalesmanAdditionalInfo(ByVal objSalesmanHistory As SalesmanPartHistory) As Integer
        Dim iReturn As Integer

        Dim oSalesInfoFacade As SalesmanAdditionalInfoFacade = New SalesmanAdditionalInfoFacade(User)
        Dim oSalesmanCategoryLevelFacade As SalesmanCategoryLevelFacade = New SalesmanCategoryLevelFacade(User)
        Dim oSalemenLevelFacade As SalesmanLevelFacade = New SalesmanLevelFacade(User)

        Dim oSalesAddInfo As SalesmanAdditionalInfo

        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanAdditionalInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SalesmanAdditionalInfo), "SalesmanHeader.ID", MatchType.Exact, objSalesmanHistory.SalesmanHeader.ID))
        Dim arlSalesInfo As ArrayList = oSalesInfoFacade.Retrieve(criterias)
        If arlSalesInfo.Count < 1 Then
            Return -1
        Else
            oSalesAddInfo = CType(arlSalesInfo(0), SalesmanAdditionalInfo)
            If oSalesAddInfo.SalesmanCategoryLevel.SalesmanCategoryLevel.ID = objSalesmanHistory.SalesmanCategoryLevel.SalesmanCategoryLevel.ID Then
                UpdateSalesmanHeader(objSalesmanHistory, oSalesAddInfo.SalesmanHeader.SalesmanCode)
            Else
                If oSalesAddInfo.SalesmanHeader.SalesmanCode = String.Empty OrElse oSalesAddInfo.SalesmanHeader.SalesmanCode = "" Then
                    UpdateSalesmanHeader(objSalesmanHistory, "request_part")
                Else
                    UpdateSalesmanHeader(objSalesmanHistory, "update_code")
                End If
            End If
        End If
        oSalesAddInfo.SalesmanCategoryLevel = objSalesmanHistory.SalesmanCategoryLevel
        oSalesAddInfo.SalesmanLevel = objSalesmanHistory.SalesmanLevel

        iReturn = oSalesInfoFacade.Update(oSalesAddInfo)

        Return iReturn
    End Function

    Private Function UpdateSalesmanPartHistory(ByVal objSalesmanHistory As SalesmanPartHistory, ByVal iStatus As Integer) As Integer
        Dim iReturn As Integer
        Dim oSalesHistory As SalesmanPartHistory = New SalesmanPartHistory

        oSalesHistory = objSalesmanHistory
        'oSalesHistory.Dealer = objSalesmanHistory.Dealer
        'oSalesHistory.SalesmanCode = objSalesmanHistory.SalesmanHeader.SalesmanCode
        oSalesHistory.Status = iStatus
        oSalesHistory.ChangedDate = Date.Now
        iReturn = New SalesmanPartHistoryFacade(User).Update(oSalesHistory)
        Return iReturn
    End Function

    Private Function UpdateSalesmanHeader(ByVal objSalesmanHistory As SalesmanPartHistory, ByVal salesmanCode As String)
        Dim objSalesmanHeader As SalesmanHeader = objSalesmanHistory.SalesmanHeader
        objSalesmanHeader.SalesmanCode = salesmanCode '"request_part"
        objSalesmanHeader.RegisterStatus = EnumSalesmanRegisterStatus.SalesmanRegisterStatus.Sudah_Register ' set supaya diregister
        objSalesmanHeader.Status = EnumSalesmanStatus.SalesmanStatus.Aktif
        Dim nResult = New SalesmanHeaderFacade(User).Update(objSalesmanHeader)
        Return nResult
    End Function

#End Region

#Region "Internal Enum"
    Private Enum HistoryStatus
        Baru = 0
        Pengajuan = 1
        Disetujui = 2
        Ditolak = 3
        Dibatalkan = 4
        Resign = 5
        Mutasi = 6
    End Enum

#End Region

End Class
