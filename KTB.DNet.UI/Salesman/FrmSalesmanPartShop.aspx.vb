Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Salesman

Public Class FrmSalesmanPartShop
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblPageTitle As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblSalesmanCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblNama As System.Web.UI.WebControls.Label
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents dgPartshop As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button

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
    Private arlPartshop As New ArrayList
    Private objDealer As Dealer
    'Private objSalesmanHeader As SalesmanHeader

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblPageTitle.Text = "Employee Part Shop"
        objDealer = (CType(sessHelper.GetSession("Dealer"), Dealer))
        If Not IsPostBack Then

            If Not Request.QueryString("dealer") Is Nothing Then
                lblDealer.Text = Request.QueryString("dealer")
            End If
            If Not Request.QueryString("code") Is Nothing Then
                lblSalesmanCode.Text = Request.QueryString("code")
                Dim objSHFacade As SalesmanHeaderFacade = New SalesmanHeaderFacade(User)
                Dim objSalesmanHeader As SalesmanHeader = objSHFacade.Retrieve(lblSalesmanCode.Text)
                If Not objSalesmanHeader Is Nothing Then
                    sessHelper.SetSession("SalesmanPart", objSalesmanHeader)
                    If objSalesmanHeader.Status = CType(EnumSalesmanStatus.SalesmanStatus.Aktif, Short) Then
                        btnSimpan.Visible = True
                        dgPartshop.ShowFooter = True
                    Else
                        dgPartshop.ShowFooter = False
                        btnSimpan.Visible = False
                    End If
                End If
                sessHelper.SetSession("EmpPartshop", Nothing)
                sessHelper.SetSession("EmpPartshopDel", Nothing)

            End If
            If Not Request.QueryString("nama") Is Nothing Then
                lblNama.Text = Request.QueryString("nama")
            End If
            BindGrid()
        End If
        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            btnSimpan.Visible = False
        End If
    End Sub

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("FrmSalesmanPartList.aspx?Mode=part")
    End Sub


    Private Sub BindGrid()
        ViewState("CurrentSortColumn") = "CreatedTime"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        Me.dgPartshop.CurrentPageIndex = 0
        BindDataPartShop(Me.dgPartshop.CurrentPageIndex)
    End Sub

    Private Sub BindDataPartShop(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        If sessHelper.GetSession("EmpPartshop") Is Nothing Then
            'sessHelper.SetSession("EmpPartshop", Nothing)
            Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanPartShop), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If Not Request.QueryString("ID") Is Nothing Then
                criterias.opAnd(New Criteria(GetType(SalesmanPartShop), "SalesmanHeader.ID", MatchType.Exact, Request.QueryString("ID")))
            End If

            arlPartshop = New SalesmanPartShopFacade(User).RetrieveByCriteria(criterias, idxPage + 1, dgPartshop.PageSize, totalRow, _
                sessHelper.GetSession("CurrentSortColumn"), sessHelper.GetSession("CurrentSortDirect"))

        Else
            arlPartshop = sessHelper.GetSession("EmpPartshop")
        End If

        dgPartshop.CurrentPageIndex = idxPage
        dgPartshop.DataSource = arlPartshop
        dgPartshop.VirtualItemCount = totalRow

        dgPartshop.DataBind()

        sessHelper.SetSession("EmpPartshop", arlPartshop)
    End Sub

    Private Sub dgPartshop_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgPartshop.ItemCommand
        Select Case (e.CommandName)
            Case "add"
                AddCommand(e)
            Case "update"
                UpdateCommand(e)
            Case "delete"
                DeleteCommand(e)
        End Select
    End Sub

    Private Sub AddCommand(ByVal e As DataGridCommandEventArgs)
        'If IsValidParameter() Then
        Dim arl As ArrayList = sessHelper.GetSession("EmpPartshop")
        Dim objSalesmanHeader = sessHelper.GetSession("SalesmanPart")
        Dim txtPartShopCode As TextBox = e.Item.FindControl("txtPartShopCodeF")
        If txtPartShopCode.Text.Trim = "" Then
            MessageBox.Show("Kode Partshop tidak boleh kosong")
            Exit Sub
        End If

        Dim objPartshop As PartShop = New PartShopFacade(User).Retrieve(txtPartShopCode.Text)
        'If Not objPartshop Is Nothing Then
        If Not objPartshop.ID = 0 Then
            Dim objSalesmanPartshop As New SalesmanPartShop
            '
            objSalesmanPartshop.SalesmanHeader = objSalesmanHeader
            objSalesmanPartshop.PartShop = objPartshop

            'Insert the third person, but first check if they already exists.
            Dim isExist As Boolean = False
            For Each item As SalesmanPartShop In arl
                If item.PartShop.PartShopCode = objSalesmanPartshop.PartShop.PartShopCode Then
                    isExist = True
                End If
            Next
            If Not isExist Then
                arl.Add(objSalesmanPartshop)
                BindDataPartShop(dgPartshop.CurrentPageIndex())
            Else
                MessageBox.Show("Kode Partshop sudah ada di daftar")
            End If

        Else
            MessageBox.Show("Tidak ada Partshop dengan kode tersebut")
        End If

        'End If

    End Sub

    Private Sub UpdateCommand(ByVal e As DataGridCommandEventArgs)
        'If IsValidParameter() Then
        Dim arl As ArrayList = sessHelper.GetSession("EmpPartshop")
        Dim txtPartShopCode As TextBox = e.Item.FindControl("txtPartShopCodeE")

        Dim objSalesmanHeader As SalesmanHeader
        If Not sessHelper.GetSession("SalesmanPart") Is Nothing Then
            objSalesmanHeader = CType(sessHelper.GetSession("SalesmanPart"), SalesmanHeader)
        End If

        If txtPartShopCode.Text.Trim = "" Then
            MessageBox.Show("Kode Partshop tidak boleh kosong")
            Exit Sub
        End If

        Dim objPartshop As PartShop = New PartShopFacade(User).Retrieve(txtPartShopCode.Text)
        If Not objPartshop Is Nothing Then
            Dim objSalesmanPartshop As New SalesmanPartShop
            '
            objSalesmanPartshop.SalesmanHeader = objSalesmanHeader
            objSalesmanPartshop.PartShop = objPartshop

            'Insert the third person, but first check if they already exists.
            Dim isExist As Boolean = False
            For Each item As SalesmanPartShop In arl
                If item.PartShop.PartShopCode = objSalesmanPartshop.PartShop.PartShopCode Then
                    isExist = True
                End If
            Next

            arl.Item(e.Item.ItemIndex) = objSalesmanPartshop

            dgPartshop.ShowFooter = True
            dgPartshop.EditItemIndex = -1
            BindDataPartShop(dgPartshop.CurrentPageIndex)
        Else
            MessageBox.Show("Tidak ada Partshop dengan kode tersebut")
        End If

        'End If

    End Sub

    Private Sub DeleteCommand(ByVal e As DataGridCommandEventArgs)
        Try
            'Dim oSalesmanPartShopFacade As SalesmanPartShopFacade = New SalesmanPartShopFacade(User)
            'Dim lblPartshopCode As Label = e.Item.FindControl("lblPartshopCode")
            'Dim objSalesmanHeader As SalesmanHeader
            'If Not sessHelper.GetSession("SalesmanPart") Is Nothing Then
            '    objSalesmanHeader = CType(sessHelper.GetSession("SalesmanPart"), SalesmanHeader)
            'End If

            'Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanPartShop), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criterias.opAnd(New Criteria(GetType(SalesmanPartShop), "SalesmanHeader.ID", MatchType.Exact, objSalesmanHeader.ID))
            'criterias.opAnd(New Criteria(GetType(SalesmanPartShop), "PartShop.PartShopCode", MatchType.Exact, lblPartshopCode.Text.Trim))

            'Dim arlPartshop As ArrayList = New SalesmanPartShopFacade(User).RetrieveByCriteria(criterias)
            'If arlPartshop.Count > 0 Then
            '    Dim objSalesmanPartShop As SalesmanPartShop = arlPartshop(0)
            '    oSalesmanPartShopFacade.Delete(objSalesmanPartShop)
            'End If
            Dim arl As ArrayList = sessHelper.GetSession("EmpPartshop")
            Dim arlDel As ArrayList
            If Not sessHelper.GetSession("EmpPartshopDel") Is Nothing Then
                arlDel = sessHelper.GetSession("EmpPartshopDel")
            Else
                arlDel = New ArrayList
            End If
            Dim objSalesmanPartShop As SalesmanPartShop = e.Item.DataItem
            If Not objSalesmanPartShop Is Nothing Then
                arlDel.Add(objSalesmanPartShop)
            End If
            arl.RemoveAt(e.Item.ItemIndex)

            sessHelper.SetSession("EmpPartshop", arl)
            sessHelper.SetSession("EmpPartshopDel", arlDel)

            BindDataPartShop(dgPartshop.CurrentPageIndex)
        Catch ex As Exception
            MessageBox.Show("Hapus data partshop gagal")
        End Try

    End Sub

    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim nResult As Integer = 0
        Dim oFacade As SalesmanPartShopFacade = New SalesmanPartShopFacade(User)
        Dim objSalesmanHeader As SalesmanHeader
        If Not sessHelper.GetSession("SalesmanPart") Is Nothing Then
            objSalesmanHeader = CType(sessHelper.GetSession("SalesmanPart"), SalesmanHeader)
        End If

        Dim arl As ArrayList = sessHelper.GetSession("EmpPartshop")
        Dim arlDel As ArrayList = sessHelper.GetSession("EmpPartshopDel")

        Try
            If Not arl Is Nothing Then
                If arl.Count > 0 Then
                    For Each item As SalesmanPartShop In arl
                        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanPartShop), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(SalesmanPartShop), "SalesmanHeader.ID", MatchType.Exact, objSalesmanHeader.ID))
                        criterias.opAnd(New Criteria(GetType(SalesmanPartShop), "PartShop.ID", MatchType.Exact, item.PartShop.ID))

                        Dim arlSalesPartshop As ArrayList = New SalesmanPartShopFacade(User).RetrieveByCriteria(criterias)
                        If arlSalesPartshop.Count > 0 Then
                            nResult = oFacade.Update(item)
                        Else
                            nResult = oFacade.Insert(item)
                            item.ID = nResult
                        End If
                    Next
                End If
            End If
            
            If Not arlDel Is Nothing Then
                If arlDel.Count > 0 Then
                    For Each item As SalesmanPartShop In arlDel
                        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanPartShop), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(SalesmanPartShop), "SalesmanHeader.ID", MatchType.Exact, item.SalesmanHeader.ID))
                        criterias.opAnd(New Criteria(GetType(SalesmanPartShop), "PartShop.ID", MatchType.Exact, item.PartShop.ID))

                        Dim arlPartshop As ArrayList = New SalesmanPartShopFacade(User).RetrieveByCriteria(criterias)
                        If arlPartshop.Count > 0 Then
                            Dim objSalesmanPartShop As SalesmanPartShop = arlPartshop(0)
                            oFacade.Delete(objSalesmanPartShop)
                        End If
                    Next
                End If
            End If

            MessageBox.Show(SR.SaveSuccess)

        Catch ex As Exception
            MessageBox.Show(SR.SaveFail)
        End Try

    End Sub

    Private Sub dgPartshop_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPartshop.ItemDataBound
        Dim lblPartshopCodeE As Label
        Dim lblPartshopCodeF As Label
        Dim txtPartShopCodeF As TextBox
        Dim txtPartShopCodeE As TextBox
        Dim lbDeleteParthop As LinkButton
        If e.Item.ItemType = ListItemType.Footer Then
            lblPartshopCodeF = CType(e.Item.FindControl("lblPartshopCodeF"), Label)
            lblPartshopCodeF.Attributes("onclick") = "ShowPopUpPartShop(this);"
            txtPartShopCodeF = CType(e.Item.FindControl("txtPartShopCodeF"), TextBox)
        ElseIf e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            lblPartshopCodeE = CType(e.Item.FindControl("lblPartshopCodeE"), Label)
            lblPartshopCodeE.Attributes("onclick") = "ShowPopUpPartShop(this);"
            txtPartShopCodeE = CType(e.Item.FindControl("txtPartShopCodeE"), TextBox)
            lbDeleteParthop = CType(e.Item.FindControl("lbDeleteParthop"), LinkButton)
        End If
        If Not e.Item.DataItem Is Nothing Then
            Dim objSalesmanPartShop As SalesmanPartShop = e.Item.DataItem
        End If
        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
                Dim txtSalesmanCodeE As TextBox = CType(e.Item.FindControl("txtSalesmanCodeE"), TextBox)
                txtPartShopCodeE.Enabled = False
                lblPartshopCodeE.Visible = False
                lbDeleteParthop.Visible = False
            End If
            If e.Item.ItemType = ListItemType.Footer Then
                e.Item.Visible = False
            End If
        End If
    End Sub
End Class
