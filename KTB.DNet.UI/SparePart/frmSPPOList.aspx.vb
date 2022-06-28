#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
#End Region

Public Class frmSPPOList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ddlOrderType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents icPODateStart As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icPODateEnd As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlProcessCode As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dtgSPPO As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnFind As System.Web.UI.WebControls.Button
    Protected WithEvents ddlTermOfPayment As System.Web.UI.WebControls.DropDownList

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Private bViewPrivilege As Boolean = False

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variable Declaration"
    Private _sessHelper As SessionHelper = New SessionHelper
    Private _sparePartPOTypeTOP As SparePartPOTypeTOP
#End Region

#Region "Custom Method"

    Private Sub BindDdlPaymentType()
        If ddlOrderType.SelectedValue <> "-1" Then
            _sparePartPOTypeTOP = New SparePartPOTypeTOPFacade(User).Retrieve(ddlOrderType.SelectedValue)
        End If
        If ddlOrderType.SelectedValue = "R" OrElse ddlOrderType.SelectedValue = "I" OrElse ddlOrderType.SelectedValue = "Z" Then
            ddlTermOfPayment.Enabled = True
            Dim dlr As Dealer = CType(Session("DEALER"), Dealer)
            Dim spCa As VWI_DealerSettingCreditAccount = New VWI_DealerSettingCreditAccountFacade(User).RetrieveByDealerCode(dlr.DealerCode)
            Dim oTopCA As TermOfPayment = New TermOfPaymentFacade(User).Retrieve(spCa.TermOfPaymentID)
            If Not IsNothing(oTopCA) Then
                Dim listOfPayments As ArrayList = New TermOfPaymentFacade(User).RetrieveFromSP(oTopCA.PaymentType, spCa.KelipatanPembayaran, oTopCA.TermOfPaymentValue)
                ddlTermOfPayment.DataSource = listOfPayments
                ddlTermOfPayment.DataValueField = "ID"
                ddlTermOfPayment.DataTextField = "Description"
                ddlTermOfPayment.DataBind()

                If _sparePartPOTypeTOP.IsTOP Then
                    ddlTermOfPayment.Items.Insert(0, New ListItem("Pilih Cara Pembayaran", ""))
                Else
                    ddlTermOfPayment.Items.Insert(0, New ListItem(_sparePartPOTypeTOP.TermOfPaymentIDNotTOP.Description, _sparePartPOTypeTOP.TermOfPaymentIDNotTOP.ID)) 'hardcoded temp
                End If
            End If
        Else
            ddlTermOfPayment.ClearSelection()
            If Not IsNothing(_sparePartPOTypeTOP) Then
                If Not IsNothing(_sparePartPOTypeTOP.TermOfPaymentIDNotTOP) Then
                    ddlTermOfPayment.Items.Insert(0, New ListItem(_sparePartPOTypeTOP.TermOfPaymentIDNotTOP.Description, _sparePartPOTypeTOP.TermOfPaymentIDNotTOP.ID))
                End If
            Else
                ddlTermOfPayment.Items.Insert(0, New ListItem("Pilih Cara Pembayaran", ""))
                ddlTermOfPayment.SelectedIndex = 0
            End If
            ddlTermOfPayment.Enabled = False
        End If

    End Sub

    Private Sub checkDealer()
        'If Session("DEALER") Is Nothing Then
        '    Response.Redirect("../SessionExpired.htm")
        'End If
    End Sub

    Private Sub BindOrderType()
        ddlOrderType.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        For Each liOrderType As ListItem In LookUp.ArraySPOrderTypeKTBDealer 'LookUp.ArraySPOrderType
            ddlOrderType.Items.Add(New ListItem(liOrderType.Text, liOrderType.Value))
        Next
    End Sub

    Private Sub BindProccessCode()
        ddlProcessCode.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        For Each liOrderType As ListItem In LookUp.ArraySPPOProccessCode
            ddlProcessCode.Items.Add(New ListItem(liOrderType.Text, liOrderType.Value))
        Next
    End Sub

    Private Sub BindHeader()
        BindOrderType()
        BindProccessCode()
    End Sub

    Private Sub BindTodtgSPPO(ByVal pageIndex As Integer)
        checkDealer()
        'dtgSPPO.Columns(6).Visible = (SecurityProvider.Authorize(Context.User, SR.ViewSPPO_Privilege) _
        '                        AndAlso SecurityProvider.Authorize(Context.User, SR.ViewSPPO_ListDetail_Privilege))
        Dim objDealer As Dealer = CType(Session("DEALER"), Dealer)
        Dim ListSPPO As New ArrayList
        Dim totalRow As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SparePartPO), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SparePartPO), "Dealer.ID", MatchType.Exact, CType(Session("DEALER"), Dealer).ID))
        If icPODateStart.Value <= icPODateEnd.Value Then
            criterias.opAnd(New Criteria(GetType(SparePartPO), "PODate", MatchType.GreaterOrEqual, Format(icPODateStart.Value, "yyyy/MM/dd")))
            criterias.opAnd(New Criteria(GetType(SparePartPO), "PODate", MatchType.LesserOrEqual, Format(icPODateEnd.Value, "yyyy/MM/dd")))
        Else
            criterias.opAnd(New Criteria(GetType(SparePartPO), "PODate", MatchType.GreaterOrEqual, Format(Date.Now, "yyyy/MM/dd")))
            criterias.opAnd(New Criteria(GetType(SparePartPO), "PODate", MatchType.LesserOrEqual, Format(Date.Now, "yyyy/MM/dd")))
            icPODateStart.Value = Date.Now
            icPODateEnd.Value = Date.Now
        End If

        If ddlProcessCode.SelectedValue = "" Then

        End If

        'Special order tidak ditampilkan untuk status baru
        If ddlOrderType.SelectedValue <> "-1" Then
            If ddlOrderType.SelectedValue = "Z" OrElse ddlOrderType.SelectedValue = "Y" Then
                criterias.opAnd(New Criteria(GetType(SparePartPO), "ProcessCode", MatchType.No, String.Empty))
                criterias.opAnd(New Criteria(GetType(SparePartPO), "OrderType", MatchType.Exact, ddlOrderType.SelectedValue))
            Else
                criterias.opAnd(New Criteria(GetType(SparePartPO), "OrderType", MatchType.Exact, ddlOrderType.SelectedValue))
            End If
            'Else
            '    Dim strSql1 As String
            '    If ddlProcessCode.SelectedValue = "-1" Then
            '        strSql1 = "(Select ID from SparePartPO where ProcessCode in ('','C') and OrderType in ('Z','Y'))"
            '        criterias.opAnd(New Criteria(GetType(SparePartPO), "ID", MatchType.NotInSet, strSql1))
            '    ElseIf ddlProcessCode.SelectedValue = "" Then
            '        criterias.opAnd(New Criteria(GetType(SparePartPO), "OrderType", MatchType.NotInSet, "'Z','Y'"))
            '        criterias.opAnd(New Criteria(GetType(SparePartPO), "ProcessCode", MatchType.Exact, ddlProcessCode.SelectedValue))
            '    Else
            '        criterias.opAnd(New Criteria(GetType(SparePartPO), "ProcessCode", MatchType.Exact, ddlProcessCode.SelectedValue))
            '    End If
        End If
        If ddlTermOfPayment.SelectedValue <> "" Then
            criterias.opAnd(New Criteria(GetType(SparePartPO), "TermOfPayment.ID", MatchType.Exact, ddlTermOfPayment.SelectedValue))
        End If
        Dim strSql1 As String
        If ddlProcessCode.SelectedValue = "-1" Then
            strSql1 = "(Select ID from SparePartPO where ProcessCode in ('','C') and OrderType in ('Z','Y'))"
            criterias.opAnd(New Criteria(GetType(SparePartPO), "ID", MatchType.NotInSet, strSql1))
        ElseIf ddlProcessCode.SelectedValue = "" Then
            criterias.opAnd(New Criteria(GetType(SparePartPO), "OrderType", MatchType.NotInSet, "'Z','Y'"))
            criterias.opAnd(New Criteria(GetType(SparePartPO), "ProcessCode", MatchType.Exact, ddlProcessCode.SelectedValue))
        Else
            criterias.opAnd(New Criteria(GetType(SparePartPO), "ProcessCode", MatchType.Exact, ddlProcessCode.SelectedValue))
        End If

        ListSPPO = New SparePartPOFacade(User).RetrieveActiveListByCriteria(criterias, pageIndex, dtgSPPO.PageSize, totalRow, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Sort.SortDirection))
        If ListSPPO.Count > 0 Then
            dtgSPPO.DataSource = ListSPPO
            dtgSPPO.VirtualItemCount = totalRow

        Else
            dtgSPPO.DataSource = New ArrayList
            If IsPostBack Then
                MessageBox.Show(SR.DataNotFound("Pesanan Spare Part"))
            End If
        End If
        dtgSPPO.DataBind()
    End Sub

    Private Sub ActivateUserPrivilege()
        btnFind.Visible = SecurityProvider.Authorize(Context.User, SR.SearchSPPO_List_Privilege)
    End Sub

#End Region

#Region "EventHandler"

    Protected Sub ddlOrderType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim ddlOrderTypeT As DropDownList = CType(sender, DropDownList)
        BindDdlPaymentType()

    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            If Not SecurityProvider.Authorize(Context.User, SR.ViewSPPO_List_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Daftar Pemesanan")
            End If
            If Me._sessHelper.GetSession("isRefresh") = "1" Then
                MessageBox.Show("Proses Gagal. Data telah diupdate oleh user/windows lain")
            End If
            Me._sessHelper.SetSession("isRefresh", "0")
            ViewState("currSortColumn") = ""
            ViewState("currSortDirection") = Sort.SortDirection.ASC
            BindHeader()
            Dim criteria As Hashtable
            criteria = Session("CriteriaFormSPPOList")
            If Not criteria Is Nothing Then
                criteria = CType(criteria, Hashtable)
                Me.ddlOrderType.SelectedValue = CType(criteria.Item("OrderType"), String)
                If ddlOrderType.SelectedValue <> "-1" Then
                    _sparePartPOTypeTOP = New SparePartPOTypeTOPFacade(User).Retrieve(ddlOrderType.SelectedValue)
                End If
                Me.ddlProcessCode.SelectedValue = CType(criteria.Item("ProcessCode"), String)
                Me.icPODateStart.Value = CType(criteria.Item("PODateStart"), Date)
                Me.icPODateEnd.Value = CType(criteria.Item("PODateEnd"), Date)
            End If
            BindDdlPaymentType()
            BindTodtgSPPO(1)
        End If
        '--Req update from Asra (BA)
        'ActivateUserPrivilege()
    End Sub

    Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
        Dim criteria As Hashtable = New Hashtable(4)
        criteria.Add("OrderType", Me.ddlOrderType.SelectedValue)
        criteria.Add("ProcessCode", Me.ddlProcessCode.SelectedValue)
        criteria.Add("TermOfPaymentID", Me.ddlTermOfPayment.SelectedValue)
        criteria.Add("PODateStart", Me.icPODateStart.Value)
        criteria.Add("PODateEnd", Me.icPODateEnd.Value)
        '_sessHelper.SetSession("CriteriaFormSPPOList", criteria)
        'Todo session
        Session.Add("CriteriaFormSPPOList", criteria)
        dtgSPPO.CurrentPageIndex = 0
        BindTodtgSPPO(dtgSPPO.CurrentPageIndex)
    End Sub

    Private Sub dtgSPPO_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgSPPO.PageIndexChanged
        dtgSPPO.CurrentPageIndex = e.NewPageIndex
        BindTodtgSPPO(e.NewPageIndex + 1)
    End Sub

    Private Sub checkCurrentStatus(ByVal spPO As SparePartPO)
        Dim oldStatus As String = spPO.ProcessCode
        spPO.SyncProcessCode()
        If spPO.ProcessCode.Trim <> oldStatus.Trim Then
            Session.Item("SPartPO") = spPO
            'MessageBox.Show("Proses Gagal. Data telah diupdate oleh user lain") '. Silahkan Refresh halaman terlebih dahulu")
            Me._sessHelper.SetSession("isRefresh", "1")
            Response.Redirect("frmSPPOList.aspx")
        End If
    End Sub

    Private Sub SetSPPODetailButton(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lblDetail As Label = CType(e.Item.FindControl("lblDetail"), Label)
        Dim objPO As SparePartPO = CType(CType(dtgSPPO.DataSource, ArrayList).Item(e.Item.ItemIndex), SparePartPO)
        Me.checkCurrentStatus(objPO)
        Dim isDetail As Boolean = False
        If objPO.ProcessCode = "" Then
            If objPO.OrderType = "Z" OrElse objPO.OrderType = "Y" Then
                lblDetail.Text = "<img style=""cursor:hand"" alt=""Rincian"" src=""../images/detail.gif"" border=""0"" height=""18px"" width=""18px"">"
                isDetail = True
            Else
                lblDetail.Text = "<img style=""cursor:hand"" alt=""Ubah"" src=""../images/edit.gif"" border=""0"" height=""18px"" width=""18px"">"
            End If

        Else
            isDetail = True
            lblDetail.Text = "<img style=""cursor:hand"" alt=""Rincian"" src=""../images/detail.gif"" border=""0"" height=""18px"" width=""18px"">"
            'End If
        End If
        lblDetail.Attributes("onclick") = "newLocation('../SparePart/FrmEntrySparePartPO.aspx?poid=" & e.Item.Cells(0).Text & "&isDetail=" & isDetail & "');document.cookie='prevPage=frmSPPOList.aspx'"

        If objPO.ProcessCode = "S" AndAlso objPO.CancelRequestBy <> String.Empty AndAlso Left(objPO.CancelRequestBy, 1) <> "-" Then
            e.Item.BackColor = System.Drawing.Color.FromName("#FFFF99")
        Else
            e.Item.ForeColor = System.Drawing.Color.Black
        End If


    End Sub

    'Private Sub dtgSPPO_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgSPPO.ItemCommand
    '    If e.CommandName = "edit" Then
    '        Response.Redirect("../SparePart/FrmEntrySparePartPO.aspx?poid=" & e.Item.Cells(0).Text)
    '    End If
    'End Sub

    Private Sub dtgSPPO_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSPPO.ItemDataBound
        If (e.Item.ItemIndex <> -1) Then
            e.Item.Cells(1).Text = (e.Item.ItemIndex + 1 + (dtgSPPO.PageSize * dtgSPPO.CurrentPageIndex)).ToString
        End If
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            SetSPPODetailButton(e)
        End If
    End Sub

    Private Sub dtgSPPO_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgSPPO.SortCommand
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
        dtgSPPO.CurrentPageIndex = 0
        BindTodtgSPPO(dtgSPPO.CurrentPageIndex + 1)
    End Sub

#End Region


End Class