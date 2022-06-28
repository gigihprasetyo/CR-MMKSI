#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.Domain.Search
#End Region

Public Class FrmListPartIncidental
    Inherits System.Web.UI.Page

#Region "Custom Variable Declaration"
    Private SessionHelper As New SessionHelper
    Private ArlIncidental As ArrayList
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerNameValue As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerSerch As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatusEmail As System.Web.UI.WebControls.Label
    Protected WithEvents lblTanggalInput As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatusEmail As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents iccalFrom As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents iccalTo As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblsd As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatusKtb As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button

#End Region

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCodeValue As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents dtgListincidental As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " Custom Declaration "
    Private objIncidental As PartIncidentalHeader
    Private objPartHeaderFacade As New PartIncidentalHeaderFacade(User)
    Private objPartDetailFacade As New PartIncidentalDetailFacade(User)
    Private sessHelp As New SessionHelper
#End Region

#Region "Custom Method"

    '--Bind Information Dealer To Form
    Private Sub RetriveMaster()
        Dim objDealer As Dealer = CType(SessionHelper.GetSession("DEALER"), Dealer)
        If Not objDealer Is Nothing Then
            lblDealerCodeValue.Text = objDealer.DealerCode
            lblDealerNameValue.Text = objDealer.DealerName
            lblDealerSerch.Text = objDealer.SearchTerm2
            ViewState("_id") = objDealer.ID
        End If
    End Sub

    Private Sub ActivateUserPrivilege()

        If Not SecurityProvider.Authorize(Context.User, SR.ViewPartIncidentalEmail_Privilege) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Daftar Permintaan Khusus")
        End If

        'btn.Visible = SecurityProvider.Authorize(Context.User, SR.DeletePartIncidentalDetail_Privilege)
        'btnSendEmail.Visible = SecurityProvider.Authorize(Context.User, SR.EmailPartIncidentalDetail_Privilege)
        'ddlAction.Visible = SecurityProvider.Authorize(Context.User, SR.DaftarPembayaranStatusTolakan_Privilege)
        'Label11.Visible = SecurityProvider.Authorize(Context.User, SR.DaftarPembayaranStatusTolakan_Privilege)
    End Sub

#End Region

#Region "EventHendlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckUserPrivilege()
        If Not IsPostBack Then

            RetriveMaster()
            BindToDropDownList()
            BindToddlStatusKTB()
            InitiatePage()

            If Not IsNothing(sessHelp.GetSession("CriteriaFromPartHeader")) Then
                Dim criteria As Hashtable
                criteria = CType(sessHelp.GetSession("CriteriaFromPartHeader"), Hashtable)
                ViewState("_id") = CType(criteria("DealerID"), String)
                ddlStatusEmail.SelectedValue = CType(criteria("EmailStatus"), String)
                iccalFrom.Value = CType(criteria("tglFrom"), Date)
                iccalTo.Value = CType(criteria("tglTo"), Date)
                btnCari_Click(Nothing, Nothing)
            End If
        End If
    End Sub

    Private Sub CheckUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.ViewPartIncidentalList_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Daftar Permintaan Khusus")
        End If
        '--060204 in active by request fo BA 
        'btnCari.Visible = SecurityProvider.Authorize(Context.User, SR.SearchPartIncidentalList_Privilege)
        dtgListincidental.Columns(14).Visible = SecurityProvider.Authorize(Context.User, SR.DeletePartIncidentalList_Privilege)
    End Sub

    Private Sub InitiatePage()
        ViewState("CurrentSortColumn") = "Dealer.SearchTerm2"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC
    End Sub

    Private Sub dtgListincidental_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgListincidental.SortCommand
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

        dtgListincidental.SelectedIndex = -1
        dtgListincidental.CurrentPageIndex = 0
        BindToDataGrid(dtgListincidental.CurrentPageIndex)
    End Sub

    Private Sub dtgListincidental_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgListincidental.PageIndexChanged
        dtgListincidental.CurrentPageIndex = e.NewPageIndex
        BindToDataGrid(dtgListincidental.CurrentPageIndex)
    End Sub

    Private Sub BindToDropDownList()
        ddlStatusEmail.DataSource = PartIncidentalStatus.RetrievePartIncidentalEmailStatus
        ddlStatusEmail.DataTextField = "NameStatus"
        ddlStatusEmail.DataValueField = "ValStatus"
        ddlStatusEmail.DataBind()
        ddlStatusEmail.Items.Add(New ListItem("Silahkan Pilih", -1))
        ddlStatusEmail.SelectedValue = -1
    End Sub

    Private Sub BindToddlStatusKTB()
        ddlStatusKtb.DataSource = PartIncidentalStatus.RetrievePartIncidentalKTBStatus
        ddlStatusKtb.DataTextField = "NameStatus"
        ddlStatusKtb.DataValueField = "ValStatus"
        ddlStatusKtb.DataBind()
    End Sub

    Private Sub BindToDataGrid(ByVal currentPageIndex As Integer)
        Dim total As Integer = 0

        Dim criterias As CriteriaComposite = New CriteriaComposite(New criteria(GetType(KTB.DNet.Domain.PartIncidentalHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New criteria(GetType(KTB.DNet.Domain.PartIncidentalHeader), "Dealer.ID", MatchType.Exact, ViewState("_id")))
        If ddlStatusKtb.SelectedValue <> -1 Then criterias.opAnd(New criteria(GetType(KTB.DNet.Domain.PartIncidentalHeader), "KTBStatus", MatchType.Exact, ddlStatusKtb.SelectedValue))

        '--DropdownList Status Email
        If ddlStatusEmail.SelectedValue <> -1 Then
            criterias.opAnd(New criteria(GetType(KTB.DNet.Domain.PartIncidentalHeader), "EmailStatus", MatchType.Exact, ddlStatusEmail.SelectedValue))
        End If

        If CType(iccalFrom.Value, Date) <= CType(iccalTo.Value, Date) Then '--Create New Calendar
            Dim tglFrom As New Date(iccalFrom.Value.Year, iccalFrom.Value.Month, iccalFrom.Value.Day, 0, 0, 0)
            Dim tglTo As New Date(iccalTo.Value.Year, iccalTo.Value.Month, iccalTo.Value.Day, 23, 59, 59)
            '--Get Criterias From Selected Calendar
            criterias.opAnd(New criteria(GetType(KTB.DNet.Domain.PartIncidentalHeader), "CreatedTime", MatchType.GreaterOrEqual, Format(tglFrom, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New criteria(GetType(KTB.DNet.Domain.PartIncidentalHeader), "CreatedTime", MatchType.LesserOrEqual, Format(tglTo, "yyyy-MM-dd HH:mm:ss")))

            Dim criteria As Hashtable = New Hashtable(2)
            criteria.Add("DealerID", ViewState("_id"))
            criteria.Add("EmailStatus", ddlStatusEmail.SelectedValue)
            criteria.Add("tglFrom", iccalFrom.Value)
            criteria.Add("tglTo", iccalTo.Value)
            sessHelp.SetSession("CriteriaFromPartHeader", criteria)
        Else
            dtgListincidental.DataBind()
            MessageBox.Show("Tanggal Awal Lebih Besar Dari Tanggal Akhir")
            Return
        End If

        ArlIncidental = New PartIncidentalHeaderFacade(User).RetrieveActiveList(criterias, currentPageIndex + 1, dtgListincidental.PageSize, _
             total, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dtgListincidental.DataSource = ArlIncidental
        dtgListincidental.VirtualItemCount = total
        dtgListincidental.DataBind()
    End Sub

    Private Sub dtgListincidental_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles dtgListincidental.ItemDataBound
        If Not IsNothing(ArlIncidental) Then
            If Not (ArlIncidental.Count = 0 Or e.Item.ItemIndex = -1) Then
                objIncidental = ArlIncidental(e.Item.ItemIndex)
                e.Item.Cells(2).Text = e.Item.ItemIndex + 1 + (dtgListincidental.CurrentPageIndex * dtgListincidental.PageSize)
                'e.Item.Cells(3).Text = objIncidental.Dealer.SearchTerm2 & "-" & objIncidental.ID.ToString.PadLeft(7, "0")
                e.Item.Cells(12).Text = CType(objIncidental.KTBStatus, PartIncidentalStatus.PartIncidentalKTBStatusEnum).ToString

                '--Check Status Email and Status KTB
                Dim lblStatus As Label = e.Item.FindControl("lblStatus")
                lblStatus.Text = CType(objIncidental.KTBStatus, PartIncidentalStatus.PartIncidentalKTBStatusEnum).ToString
                Dim linkDelete As LinkButton = e.Item.FindControl("lbtnDelete")
                Dim emailStatus As Label = e.Item.FindControl("lblStatusEmail")

                If objIncidental.EmailStatus = PartIncidentalStatus.PartIncidentalEmailStatusEnum.Dikirim Then
                    emailStatus.Text = "<img src=""../images/red.gif"" border=""0"">"
                    emailStatus.ToolTip = "Sudah Dikirim"
                    If lblStatus.Text = PartIncidentalStatus.PartIncidentalKTBStatusEnum.Selesai.ToString Then
                        linkDelete.Visible = True
                    End If
                Else
                    emailStatus.Text = "<img src=""../images/green.gif"" border=""0"">"
                    emailStatus.ToolTip = "Belum Dikirim"
                    If lblStatus.Text = PartIncidentalStatus.PartIncidentalKTBStatusEnum.Baru.ToString Then
                        linkDelete.Visible = True
                    End If
                End If

                If objIncidental.KTBStatus = PartIncidentalStatus.PartIncidentalKTBStatusEnum.Selesai Then
                    linkDelete.Visible = False
                End If

                '--Get Confirm Message From Button Delete
                If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                    CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
                End If

            End If
        End If
    End Sub

    Private Sub dtgListincidental_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgListincidental.ItemCommand
        If e.CommandName = "View" Then
            Dim lblID As Label = e.Item.FindControl("lblID")
            sessHelp.GetSession("CriteriaFromPartHeader")
            Response.Redirect("../Sparepart/FrmListPartIncidentalDetail.aspx?Id=" & lblID.Text)

        ElseIf e.CommandName = "Delete" Then
            Dim lblID As Label = e.Item.FindControl("lblID")
            objIncidental = New PartIncidentalHeaderFacade(User).Retrieve(CInt(lblID.Text))
            If objIncidental.PartIncidentalDetails.Count > 0 Then
                For Each item As PartIncidentalDetail In objIncidental.PartIncidentalDetails
                    objPartDetailFacade.Delete(item)
                Next
                objPartHeaderFacade.DeleteFromDB(objIncidental)
            Else
                objPartHeaderFacade.DeleteFromDB(objIncidental)
            End If

            btnCari_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        dtgListincidental.CurrentPageIndex = 0
        BindToDataGrid(dtgListincidental.CurrentPageIndex)
    End Sub

#End Region

End Class
