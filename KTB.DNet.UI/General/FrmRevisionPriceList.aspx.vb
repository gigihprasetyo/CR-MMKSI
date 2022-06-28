Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.FinishUnit

Public Class FrmRevisionPriceList
    Inherits System.Web.UI.Page

    Dim sessHelp As SessionHelper = New SessionHelper
#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.Revisis_Faktur_Master_Revision_Price_Lihat_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Revisi Faktur - Master Harga Revisi")
        End If
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        InitiateAuthorization()

        If Not IsPostBack Then
            BindDDLCategory()
            BindRevisionTypeDropDownList()

            ViewState("currentSortColumn") = "ValidFrom"
            ViewState("currentSortDirection") = Sort.SortDirection.DESC
            Dim crit As Hashtable = New Hashtable
            crit = CType(Session("CriteriaFormRevisionPriceList"), Hashtable)
            If Not crit Is Nothing Then
                ddlCategory.SelectedValue = CStr(crit.Item("Category"))
                ddlRevisionType.SelectedValue = CStr(crit.Item("Type"))
                chkValidPeriod.Checked = CType(crit("chkValidPeriod"), Boolean)
                icStartValid.Value = CType(crit("StartValid"), Date)
                icEndValid.Value = CType(crit("EndValid"), Date)
                dgTable.CurrentPageIndex = CInt(crit.Item("PageIndex"))
            End If
            BindDataGrid()
        End If
    End Sub

    Private Sub btnCari_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCari.Click
        storeCriteria()
        BindDataGrid()
    End Sub

    Private Sub BindDataGrid()
        Dim _arrList As New ArrayList
        Dim totalRow As Integer = 0
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RevisionPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If ddlCategory.SelectedIndex > 0 Then criterias.opAnd(New Criteria(GetType(RevisionPrice), "Category.CategoryCode", MatchType.Exact, ddlCategory.SelectedValue))
        If ddlRevisionType.SelectedIndex > 0 Then criterias.opAnd(New Criteria(GetType(RevisionPrice), "RevisionType.ID", MatchType.Exact, ddlRevisionType.SelectedValue))
        If chkValidPeriod.Checked Then
            '-- Periode Validasi
            Dim StartValid As New DateTime(CInt(icStartValid.Value.Year), CInt(icStartValid.Value.Month), CInt(icStartValid.Value.Day), 0, 0, 0)
            Dim EndValid As New DateTime(CInt(icEndValid.Value.Year), CInt(icEndValid.Value.Month), CInt(icEndValid.Value.Day), 23, 59, 59)
            criterias.opAnd(New Criteria(GetType(RevisionPrice), "ValidFrom", MatchType.GreaterOrEqual, Format(StartValid, "yyyy-MM-dd HH:mm:ss")))
            criterias.opAnd(New Criteria(GetType(RevisionPrice), "ValidFrom", MatchType.LesserOrEqual, Format(EndValid, "yyyy-MM-dd HH:mm:ss")))
        End If

        '-- Sorted by
        Dim sortColl As SortCollection = New SortCollection
        'oCM.EndCustomerRev.ValidateBy 
        sortColl.Add(New Sort(GetType(RevisionPrice), CType(ViewState("currentSortColumn"), String), CType(ViewState("currentSortDirection"), Sort.SortDirection)))  '-- Nomor chassis

        _arrList = New RevisionPriceFacade(User).RetrieveByCriteria(criterias, sortColl)
        dgTable.VirtualItemCount = totalRow
        dgTable.DataSource = _arrList
        dgTable.DataBind()
    End Sub

    Private Sub dgTable_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgTable.ItemCommand
        Select Case e.CommandName
            Case "View"
                Response.Redirect("FrmRevisionPriceDetail.aspx?id=" & e.Item.Cells(0).Text)
            Case "Edit"
                btnCari.Enabled = False
                ViewState("typeForm") = "Edit"
                dgTable.SelectedIndex = e.Item.ItemIndex
        End Select
    End Sub

    Private Sub dgTable_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgTable.ItemDataBound
        If Not IsNothing(e.Item.DataItem) Then
            Dim objDomain2 As RevisionPrice = CType(e.Item.DataItem, RevisionPrice)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = (e.Item.ItemIndex + 1 + (dgTable.CurrentPageIndex * dgTable.PageSize)).ToString()
            Dim lblCategory As Label = CType(e.Item.FindControl("lblCategory"), Label)
            lblCategory.Text = objDomain2.Category.CategoryCode
            Dim lblRevision As Label = CType(e.Item.FindControl("lblRevision"), Label)
            lblRevision.Text = objDomain2.RevisionType.Description
            Dim lblAmount As Label = CType(e.Item.FindControl("lblAmount"), Label)
            lblAmount.Text = String.Format("{0:N0}", objDomain2.Amount)
            Dim lblValid As Label = CType(e.Item.FindControl("lblValid"), Label)
            lblValid.Text = objDomain2.ValidFrom.ToString("dd/MM/yyyy")
            Dim lnkbtnView As LinkButton = CType(e.Item.FindControl("lnkbtnView"), LinkButton)
        End If
    End Sub

    Private Sub dgTable_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgTable.PageIndexChanged
        dgTable.CurrentPageIndex = e.NewPageIndex
        BindDataGrid()
    End Sub

    Private Sub dgTable_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgTable.SortCommand
        If CType(ViewState("currentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("currentSortDirection"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("currentSortDirection") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    ViewState("currentSortDirection") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("currentSortColumn") = e.SortExpression
            ViewState("currentSortDirection") = Sort.SortDirection.DESC
        End If
        dgTable.SelectedIndex = -1
        dgTable.CurrentPageIndex = 0
        BindDataGrid()
    End Sub

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
        ddlCategory.Items.Insert(0, New ListItem("Silahkan Pilih", ""))  '-- Dummy blank item
    End Sub

    Private Sub BindRevisionTypeDropDownList()

        Dim objRevisionTypeFacade As RevisionTypeFacade = New RevisionTypeFacade(User)

        ddlRevisionType.DataSource = objRevisionTypeFacade.RetrieveActiveList()
        ddlRevisionType.DataTextField = "Description"
        ddlRevisionType.DataValueField = "ID"
        ddlRevisionType.DataBind()
        ddlRevisionType.Items.Insert(0, "Silahkan Pilih")
    End Sub

    Private Sub storeCriteria()
        Dim crit As Hashtable = New Hashtable
        crit.Add("Category", ddlCategory.SelectedValue)
        crit.Add("Type", ddlRevisionType.SelectedValue)
        crit.Add("chkValidPeriod", chkValidPeriod.Checked)
        crit.Add("StartValid", icStartValid.Value)
        crit.Add("EndValid", icEndValid.Value)
        crit.Add("PageIndex", dgTable.CurrentPageIndex)
        sessHelp.SetSession("CriteriaFormRevisionPriceList", crit)
    End Sub
#End Region
End Class