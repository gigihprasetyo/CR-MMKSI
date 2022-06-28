#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade

#End Region

Public Class frmChassisMasterDoc
    Inherits System.Web.UI.Page


#Region "variable"
    Private ReadOnly varUpload As String = "\OCRChasiss\" '\\172.17.31.121\MDNET_Repo\Repository\BSI-Net\DNet\SAP\OCRChasiss
    Private ReadOnly varSession As String = "sessfrmChassisMasterDoc"
    Private ReadOnly varCrite As String = "CritfrmChassisMasterDoc"
    Private sesHelper As New SessionHelper
    Dim arlUserGroup As New ArrayList
    Dim IsKTB As Boolean
    Dim IsAllowToEdit As Boolean = False
    Dim IsAllowToRead As Boolean = False
#End Region

#Region "Custom CLasss"
    Private Class FormFilterTemplate
        Sub New()
            Me.icStartConfirm = DateTime.Now
            Me.icEndConfirm = DateTime.Now
        End Sub

        Public SortCol As String
        Public CurrentSortDirect As Int32

        Public Property txtKodeDealer As String


        Public Property icStartConfirm As DateTime
        Public Property icEndConfirm As DateTime


        Public Property txtChassisNo As String
        Public Property txtSPKNumber As String


        Public Property ddlCategory As String
        Public Property chkConfirmPeriod As Boolean
        Public Property icStartValid As DateTime
        Public Property icEndValid As DateTime
        Public Property ddlGesek As String
        Public Property txtInvoiceNo As String

        Public Property chkValidPeriod As Boolean

    End Class
#End Region

#Region "Private Function"

    '-- Bind Category dropdownlist
    Private Sub BindDDLCategory()
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim cat As String = ""
        cat = cat & "'PC',"
        cat = cat & "'LCV',"

        If cat <> "" Then
            cat = "(" & cat.Substring(0, cat.Length - 1) & ")"
            criterias.opAnd(New Criteria(GetType(Category), "CategoryCode", MatchType.InSet, cat))
        End If

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(Category), "CategoryCode", Sort.SortDirection.ASC))  '-
        ddlCategory.DataSource = New CategoryFacade(User).RetrieveByCriteria(criterias, sortColl)
        ddlCategory.DataTextField = "CategoryCode"
        ddlCategory.DataValueField = "CategoryCode"
        ddlCategory.DataBind()
        ddlCategory.Items.Insert(0, New ListItem("Pilih", ""))  '-- Dummy blank item
    End Sub

    Private Sub CheckPrivilege()
        Dim objDealer As Dealer = CType(sesHelper.GetSession("DEALER"), Dealer)
        Dim lihat
        IsAllowToRead = SecurityProvider.Authorize(Context.User, SR.UploadGesek_Lihat_Privilege)
        IsAllowToEdit = SecurityProvider.Authorize(Context.User, SR.UploadGesek_Input_Privilege)

        If Not IsAllowToEdit AndAlso Not IsAllowToRead Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Sales-FakturKendaraan-UploadRangka")
        End If

        If (objDealer.Title = EnumDealerTittle.DealerTittle.KTB) Then
            IsKTB = True

        Else

        End If
    End Sub

    Private Sub initPage()
        ViewState("SortCol") = "ChassisNumber"
        ViewState("SortDirection") = Sort.SortDirection.DESC
    End Sub

    Private Sub LoadCrite()
        Dim ObjArr As New ArrayList



        If Not IsNothing(sesHelper.GetSession(varSession)) Then
            Dim abg As New FormFilterTemplate()
            abg = CType(sesHelper.GetSession(varSession), FormFilterTemplate)

            ViewState("SortCol") = abg.SortCol
            ViewState("CurrentSortDirect") = abg.CurrentSortDirect
            chkConfirmPeriod.Checked = abg.chkConfirmPeriod
            txtKodeDealer.Text = abg.txtKodeDealer
            txtChassisNo.Text = abg.txtChassisNo
            chkValidPeriod.Checked = abg.chkValidPeriod
            ddlCategory.SelectedValue = abg.ddlCategory
            ddlGesek.SelectedValue = abg.ddlGesek
            txtSPKNumber.Text = abg.txtSPKNumber
            icStartConfirm.Value = abg.icStartConfirm
            icEndConfirm.Value = abg.icEndConfirm
            icStartValid.Value = abg.icStartValid
            icEndValid.Value = abg.icEndValid
            txtInvoiceNo.Text = abg.txtInvoiceNo

        End If



    End Sub

    Private Sub SaveCrite()
        Dim ObjArr As New ArrayList

        Dim abg As New FormFilterTemplate()
        abg.SortCol = ViewState("SortCol").ToString()
        abg.CurrentSortDirect = CInt(ViewState("CurrentSortDirect"))
        abg.chkConfirmPeriod = chkConfirmPeriod.Checked
        abg.txtKodeDealer = txtKodeDealer.Text
        abg.txtChassisNo = txtChassisNo.Text
        abg.chkValidPeriod = chkValidPeriod.Checked
        abg.ddlCategory = ddlCategory.SelectedValue
        abg.ddlGesek = ddlGesek.SelectedValue
        abg.txtSPKNumber = txtSPKNumber.Text
        abg.icStartConfirm = icStartConfirm.Value
        abg.icEndConfirm = icEndConfirm.Value
        abg.icStartValid = icStartValid.Value
        abg.icEndValid = icEndValid.Value
        abg.txtInvoiceNo = txtInvoiceNo.Text


        sesHelper.SetSession(varSession, abg)
    End Sub

    Private Sub CreateCriterias()
        Dim val As String
        Dim criterias As New CriteriaComposite(New Criteria(GetType(Vw_ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim objDealer As Dealer = CType(sesHelper.GetSession("DEALER"), Dealer)

        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Vw_ChassisMaster), "Dealer.DealerCode", MatchType.InSet, DealerFacade.GenerateDealerCodeSelection(objDealer, User)))

        Else

        End If

        '-- Nomor faktur
        If txtInvoiceNo.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(Vw_ChassisMaster), "EndCustomer.FakturNumber", MatchType.[Partial], txtInvoiceNo.Text.Trim()))
        End If

        If txtKodeDealer.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(Vw_ChassisMaster), "Dealer.DealerCode", MatchType.InSet, "('" & txtKodeDealer.Text.Replace(";", "','") & "')"))
            'criterias.opAnd(New Criteria(GetType(Vw_ChassisMaster), "Dealer.DealerCode", MatchType.InSet, "('" & objDealer.DealerCode & "')"))

        End If

        If txtSPKNumber.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(Vw_ChassisMaster), "EndCustomer.SPKFaktur.SPKHeader.SPKNumber", MatchType.[Partial], txtSPKNumber.Text.Trim()))
        End If
        If chkConfirmPeriod.Checked Then
            Dim StartConfirm As New DateTime(CInt(icStartConfirm.Value.Year), CInt(icStartConfirm.Value.Month), CInt(icStartConfirm.Value.Day), 0, 0, 0)
            Dim EndConfirm As New DateTime(CInt(icEndConfirm.Value.Year), CInt(icEndConfirm.Value.Month), CInt(icEndConfirm.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(Vw_ChassisMaster), "EndCustomer.ConfirmTime", MatchType.GreaterOrEqual, Format(StartConfirm, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(Vw_ChassisMaster), "EndCustomer.ConfirmTime", MatchType.LesserOrEqual, Format(EndConfirm, "yyyy-MM-dd HH:mm:ss")))
        End If


        If chkValidPeriod.Checked Then
            '-- Periode Validasi
            Dim StartValid As New DateTime(CInt(icStartValid.Value.Year), CInt(icStartValid.Value.Month), CInt(icStartValid.Value.Day), 0, 0, 0)
            Dim EndValid As New DateTime(CInt(icEndValid.Value.Year), CInt(icEndValid.Value.Month), CInt(icEndValid.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(Vw_ChassisMaster), "EndCustomer.ValidateTime", MatchType.GreaterOrEqual, Format(StartValid, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(Vw_ChassisMaster), "EndCustomer.ValidateTime", MatchType.LesserOrEqual, Format(EndValid, "yyyy-MM-dd HH:mm:ss")))
        End If

        If ddlGesek.SelectedValue <> "" Then
            If ddlGesek.SelectedValue = "1" Then

                criterias.opAnd(New Criteria(GetType(Vw_ChassisMaster), "ChassisMasterDocumentID", MatchType.Greater, 0))
            Else
                criterias.opAnd(New Criteria(GetType(Vw_ChassisMaster), "ChassisMasterDocumentID", MatchType.Exact, 0))
            End If

        End If
        If txtChassisNo.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(Vw_ChassisMaster), "ChassisNumber", MatchType.Partial, txtChassisNo.Text))
        End If


        criterias.opAnd(New Criteria(GetType(Vw_ChassisMaster), "FakturStatus", MatchType.InSet, "('1','2','3','4')"))
        sesHelper.SetSession(Me.varCrite, criterias)
    End Sub

    Private Sub BindDataGridMember(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then

            Dim criterias As CriteriaComposite = sesHelper.GetSession(Me.varCrite)
            'New ForumMemberFacade(User).RetrieveByCriteria(criterias, 1, dtgForumMember.PageSize, totalRow, sHelper.GetSession("SortCol"), sHelper.GetSession("SortDirection"))
            arlUserGroup = New Vw_ChassisMasterFacade(User).RetrieveActiveList(indexPage, Me.dgInvoiceList.PageSize, totalRow, ViewState("SortCol"), ViewState("SortDirection"), criterias)
            dgInvoiceList.DataSource = arlUserGroup
            dgInvoiceList.VirtualItemCount = totalRow
            dgInvoiceList.DataBind()


        End If
    End Sub


    Private Sub CommandView(ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Try
            Dim ObjV_DiscountProposalHeader As New Vw_ChassisMaster(CInt(e.Item.Cells(0).Text))
            SaveCrite()

            If IsKTB Then
                Response.Redirect("frmChassisMasterDocInput.aspx?ID=" & ObjV_DiscountProposalHeader.ID.ToString() & "&isSelf=0", False)
            Else
                Response.Redirect("frmChassisMasterDocInput.aspx?ID=" & ObjV_DiscountProposalHeader.ID.ToString() & "&isSelf=0", False)
            End If

        Catch ex As Exception

        End Try
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        CheckPrivilege()
        If Not IsPostBack Then
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            BindDDLCategory()
            ViewState("SortCol") = "ChassisNumber"
            ViewState("SortDirection") = Sort.SortDirection.DESC
            If Request.QueryString("Mode") = "1" AndAlso Not IsNothing(sesHelper.GetSession(varSession)) Then
                LoadCrite()
                CreateCriterias()

                sesHelper.RemoveSession(varSession)
                BindDataGridMember(0)
            Else
                sesHelper.RemoveSession(varSession)
            End If
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        CreateCriterias()
        BindDataGridMember(0)
    End Sub

    Private Sub dgInvoiceList_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgInvoiceList.ItemCommand

        Select Case e.CommandName.ToLower()
            Case "Delete".ToLower()
                'CommandDelete(e)
            Case "Edit".ToLower()
                'CommandEdit(e)
            Case "Download".ToLower()
                'CommandDownload(e)
            Case "View".ToLower()
                CommandView(e)

        End Select
    End Sub

    Private Sub dgInvoiceList_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgInvoiceList.ItemDataBound
        If e.Item.ItemIndex <> -1 AndAlso (e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem) Then
            Dim objV_Chassis As Vw_ChassisMaster = e.Item.DataItem
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)

            lblNo.Text = CType(e.Item.ItemIndex + 1 + (dgInvoiceList.CurrentPageIndex * dgInvoiceList.PageSize), String)

            If IsNothing(objV_Chassis.ChassisMasterDocument) OrElse objV_Chassis.ChassisMasterDocumentID = 0 Then
                e.Item.BackColor = Color.Aqua
            End If


        End If
    End Sub

    Private Sub dgInvoiceList_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgInvoiceList.PageIndexChanged
        dgInvoiceList.CurrentPageIndex = e.NewPageIndex
        BindDataGridMember(e.NewPageIndex + 1)
    End Sub

    Private Sub dgInvoiceList_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgInvoiceList.SortCommand
        If e.SortExpression = ViewState("SortCol") Then
            If ViewState("SortDirection") = Sort.SortDirection.ASC Then
                ViewState("SortDirection") = Sort.SortDirection.DESC
            Else
                ViewState("SortDirection") = Sort.SortDirection.ASC
            End If
        End If
        ViewState("SortCol") = e.SortExpression
        dgInvoiceList.SelectedIndex = -1
        'dtgV_DiscountProposalHeader.CurrentPageIndex = 0
        BindDataGridMember(dgInvoiceList.CurrentPageIndex)
    End Sub
End Class