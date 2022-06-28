#Region "namespace"
Imports Ktb.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.SparePart
Imports Ktb.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Data.SqlClient
Imports Ktb.DNet.Utility
Imports KTB.DNet.Security
#End Region

Public Class FrmModifySpecialItem
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgSpecialItem As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlPeriodMonth As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlExtMaterialGroup As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlSparePart As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents btnHapus As System.Web.UI.WebControls.Button
    Protected WithEvents ddlPeriodYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblNewPart As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblNewPrice As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Private Packages As ArrayList = New ArrayList

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub
#End Region

#Region "private variable"
    Private _ViewModifySpecialItem As Boolean = False
    Private _DeleteAll As Boolean = False
    Private counter As Integer = 0
#End Region

#Region "private function/sub"

    Private Sub InitiatePrivilage()
        _ViewModifySpecialItem = SecurityProvider.Authorize(Context.User, SR.ViewSpecialItemManagement_Privilege)
        If Not _ViewModifySpecialItem Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Pengelolaan Special Item")
        End If
        Me.btnHapus.Visible = SecurityProvider.Authorize(context.User, SR.DeleteAllSpecialItemManagement_Privilege)
        '--exclude  this privilege from Asra (BA)
        'Me.btnCari.Visible = SecurityProvider.Authorize(Context.User, SR.SearchSpecialItemManagement_Privilege)
    End Sub

    Private Sub BindDtgSpecialItem(ByVal IndexPage As Integer)

        Dim totalrow As Integer = 0
        Dim ListSpecialItemDetail As ArrayList = New ArrayList
        Dim objSpecialItemFacade As SpecialItemFacade = New SpecialItemFacade(User)
        Dim bIsEmpty As Boolean = True
        Dim crit As CriteriaComposite = CreateCriteria()
        Dim listForDG As ArrayList = New ArrayList
        Dim catatanHeader As String = ""
        Dim referensiHeader As String = ""

        ListSpecialItemDetail = objSpecialItemFacade.RetrieveSpecialItemDetailList(crit, IndexPage, dtgSpecialItem.PageSize / 2, totalrow, CType(ViewState("CurrentSortColumn"), String), ViewState("CurrentSortDirect"))
        totalrow = totalrow * 2

        'If Not ListSpecialItemDetail Is Nothing Then
        '    If ListSpecialItemDetail.Count > 0 Then
        '        For Each SpecialItem As SpecialItemDetail In ListSpecialItemDetail
        '            listForDG.Add(SpecialItem)
        '            listForDG.Add(SpecialItem)
        '            If Not SpecialItem.SpecialItemPackages Is Nothing Then
        '                For Each pack As SpecialItemPackage In SpecialItem.SpecialItemPackages
        '                    AddToPackages(Packages, pack.PackageNo)
        '                Next
        '            End If
        '            bIsEmpty = False
        '        Next
        '        Packages.Sort()
        '        AddColumn(Packages)
        '    End If
        'End If

        If Not ListSpecialItemDetail Is Nothing Then
            If ListSpecialItemDetail.Count > 0 Then
                For Each SpecialItem As SpecialItemDetail In ListSpecialItemDetail
                    If catatanHeader = "" Then
                        catatanHeader = SpecialItem.SpecialItemHeader.Remark
                        referensiHeader = SpecialItem.SpecialItemHeader.Reference
                    End If
                    '------
                    listForDG.Add(SpecialItem)
                    listForDG.Add(SpecialItem)
                    '------
                    If Not SpecialItem.SpecialItemPackages Is Nothing Then
                        For Each pack As SpecialItemPackage In SpecialItem.SpecialItemPackages
                            AddToPackages(Packages, pack.PackageNo)
                        Next
                    End If
                    bIsEmpty = False
                Next
                Packages.Sort()
                AddColumn(Packages)
            End If
        End If

        If bIsEmpty Then
            Me.dtgSpecialItem.DataSource = New ArrayList
            btnHapus.Enabled = False
            MessageBox.Show(SR.DataNotFound("Special Item") & " atau tidak aktif")
        Else
            'Me.dtgSpecialItem.DataSource = ListSpecialItemDetail
            Me.dtgSpecialItem.DataSource = listForDG
            'Todo session
            Session.Add("SpecialItemDetailCriteria", crit)
            'If _DeleteAll Then btnHapus.Enabled = True
            'MessageBox.Show(_DeleteAll)
            btnHapus.Enabled = True
        End If
        Me.dtgSpecialItem.VirtualItemCount = totalrow
        Me.dtgSpecialItem.DataBind()

    End Sub

    Private Sub BindDropDownListMonth()
        ' Period - Month
        Me.ddlPeriodMonth.Items.Add(New ListItem("Januari", 0))
        Me.ddlPeriodMonth.Items.Add(New ListItem("Februari", 1))
        Me.ddlPeriodMonth.Items.Add(New ListItem("Maret", 2))
        Me.ddlPeriodMonth.Items.Add(New ListItem("April", 3))
        Me.ddlPeriodMonth.Items.Add(New ListItem("Mei", 4))
        Me.ddlPeriodMonth.Items.Add(New ListItem("Juni", 5))
        Me.ddlPeriodMonth.Items.Add(New ListItem("Juli", 6))
        Me.ddlPeriodMonth.Items.Add(New ListItem("Agustus", 7))
        Me.ddlPeriodMonth.Items.Add(New ListItem("September", 8))
        Me.ddlPeriodMonth.Items.Add(New ListItem("Oktober", 9))
        Me.ddlPeriodMonth.Items.Add(New ListItem("November", 10))
        Me.ddlPeriodMonth.Items.Add(New ListItem("Desember", 11))
        Me.ddlPeriodMonth.SelectedIndex = 0
        ' Ext Group

    End Sub

    Private Sub BindDropDownListYear()
        ' Period - Year
        For Each item As ListItem In LookUp.ArrayYearWithValue(True, 1, 1, DateTime.Now.Year.ToString)
            Me.ddlPeriodYear.Items.Add(item)
        Next
        Me.ddlPeriodYear.SelectedValue = DateTime.Now.Year.ToString

    End Sub

    Private Sub Initial()

        ViewState("CurrentSortColumn") = ""
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC

        ddlPeriodMonth.Enabled = True
        ddlPeriodYear.Enabled = True
        ddlExtMaterialGroup.Enabled = True
        ddlSparePart.Enabled = True

        ddlPeriodMonth.Items.Clear()
        ddlPeriodYear.Items.Clear()
        ddlExtMaterialGroup.Items.Clear()
        ddlSparePart.Items.Clear()

        BindDropDownListMonth()
        BindDropDownListYear()
        Me.ddlPeriodMonth.SelectedValue = Date.Now.Month - 1
        Me.ddlPeriodYear.SelectedValue = Date.Now.Year.ToString
        BindDdlExtMaterialGroup(ddlPeriodMonth.SelectedIndex + 1, CType(ddlPeriodYear.SelectedValue, Integer))
        BindDdlSparePart(CType(ddlPeriodMonth.SelectedValue, Integer) + 1, CType(ddlPeriodYear.SelectedValue, Integer))

        btnHapus.Enabled = False

    End Sub

    Private Sub InitialBack()

        Dim ArrGroup As ArrayList = CType(Session.Item("ListingDdlGroup"), ArrayList)
        Dim ArrSpare As ArrayList = CType(Session.Item("ListingDdlSpare"), ArrayList)
        Dim ct As Integer

        ViewState("CurrentSortColumn") = ""
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC

        ddlPeriodMonth.Enabled = True
        ddlPeriodYear.Enabled = True
        ddlExtMaterialGroup.Enabled = True
        ddlSparePart.Enabled = True
        btnHapus.Enabled = False

        ddlPeriodMonth.Items.Clear()
        ddlPeriodYear.Items.Clear()
        ddlExtMaterialGroup.Items.Clear()
        ddlSparePart.Items.Clear()

        BindDropDownListMonth()
        BindDropDownListYear()

        'ddlExtMaterialGroup.Items = Session.Item("ListingDdlGroup")
        Me.ddlPeriodMonth.SelectedIndex = Session.Item("SelectionDdlMonth")
        ddlPeriodYear.SelectedValue = Session.Item("TextYear")

        For ct = 0 To ArrGroup.Count - 1
            ddlExtMaterialGroup.Items.Add(New ListItem(ArrGroup(ct), ct))
        Next
        ddlExtMaterialGroup.SelectedIndex = Session.Item("SelectionDdlGroup")

        For ct = 0 To ArrSpare.Count - 1
            ddlSparePart.Items.Add(New ListItem(ArrSpare(ct), ct))
        Next
        ddlExtMaterialGroup.SelectedIndex = Session.Item("SelectionDdlSpare")

        ClearSessionBack()

        btnCari_Click(Nothing, Nothing)

    End Sub

    Private Sub ControlsScriptInjection()
        btnHapus.Attributes.Add("OnClick", "return confirm('" & "Yakin Semua Data Mau Dihapus ?" & "');")
        'btnSubmit.Attributes.Add("OnClick", "return confirm('" & SR.SubmitConfirmation & "');")
        'chkRequestForCanceled.Attributes.Add("OnClick", "if ( ! confirm('" & CType(ViewState("messCancelKTB"), String) & "')) return false;")
        'chkRequestForCanceled.Attributes.Add("OnClick", "return  confirm('Yakin Pesanan ini akan dibatalkan ?');")
        'Session.Add("ScreenID", 210)
    End Sub

    Public Sub AddColumn(ByVal PackageList As ArrayList)
        For Each Package As String In PackageList
            Dim datagridcol As New BoundColumn
            datagridcol.HeaderStyle.CssClass = "titleTableParts"
            datagridcol.HeaderText = "Harga / Paket (Rp)"
            datagridcol.HeaderStyle.Width = Unit.Pixel(70)
            Me.dtgSpecialItem.Columns.Add(datagridcol)
        Next
        dtgSpecialItem.Width = Unit.Pixel((PackageList.Count * 70) + 490)
    End Sub
    Private Sub AddToPackages(ByVal packageList As ArrayList, ByVal package As String)
        If Not packageList.Contains(package) Then
            packageList.Add(package)
        End If
    End Sub
    Private Function CreateCriteria() As CriteriaComposite
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SpecialItemDetail), "SpecialItemHeader.MonthPeriode", MatchType.Exact, ddlPeriodMonth.SelectedIndex + 1))
        crit.opAnd(New Criteria(GetType(SpecialItemDetail), "SpecialItemHeader.YearPeriode", MatchType.Exact, CType(ddlPeriodYear.SelectedValue, Integer)))
        crit.opAnd(New Criteria(GetType(SpecialItemDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If ddlExtMaterialGroup.SelectedIndex > 0 Then crit.opAnd(New Criteria(GetType(SpecialItemDetail), "ExtMaterialGroup", MatchType.Exact, ddlExtMaterialGroup.SelectedItem.Text))
        If ddlSparePart.SelectedIndex > 0 Then crit.opAnd(New Criteria(GetType(SpecialItemDetail), "SparePartMaster.PartNumber", MatchType.Exact, ddlSparePart.SelectedItem.Text))

        'Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SpecialItemDetail), "SparePartMaster.PartNumber", MatchType.Exact, ddlSparePart.SelectedItem.Text))
        'If ddlExtMaterialGroup.SelectedIndex > 0 Then crit.opAnd(New Criteria(GetType(SpecialItemDetail), "ExtMaterialGroup", MatchType.Exact, ddlExtMaterialGroup.SelectedItem.Text))

        Return crit
    End Function

    Private Sub BindDdlExtMaterialGroup(ByVal bulan As Integer, ByVal tahun As Integer)

        Dim ct1, ct2 As Integer
        Dim Exist As Boolean
        Dim Group As ArrayList = New ArrayList
        Dim ListSpecialItemDetail As ArrayList = New SpecialItemFacade(User).RetrieveSpecialItemGroupList(bulan, tahun)

        If ListSpecialItemDetail.Count <> 0 Then
            For ct1 = 0 To ListSpecialItemDetail.Count - 1
                Exist = False

                If Group.Count <> 0 Then
                    For ct2 = 0 To Group.Count - 1
                        If Group.Item(ct2) = CType(ListSpecialItemDetail.Item(ct1), VIEW_ExtMaterialGroup).ExtMaterialGroup Then
                            Exist = True
                        End If
                    Next
                End If

                If Not Exist Then
                    Group.Add(CType(ListSpecialItemDetail.Item(ct1), VIEW_ExtMaterialGroup).ExtMaterialGroup)
                End If
            Next
        End If

        dtgSpecialItem.DataSource = New ArrayList
        dtgSpecialItem.VirtualItemCount = 0
        dtgSpecialItem.CurrentPageIndex = 0
        dtgSpecialItem.DataBind()
        ddlExtMaterialGroup.Items.Clear()
        ddlSparePart.Items.Clear()
        btnHapus.Enabled = False

        ddlExtMaterialGroup.Items.Add(New ListItem("Silahkan Pilih", 0))
        ddlSparePart.Items.Add(New ListItem("Silahkan Pilih", 0))
        If Group.Count <> 0 Then
            For ct1 = 0 To Group.Count - 1
                ddlExtMaterialGroup.Items.Add(New ListItem(Group.Item(ct1), ct1 + 1))
            Next
            'ddlExtMaterialGroup.Enabled = True
            'ElseIf Group.Count = 0 Then
            'ddlExtMaterialGroup.Enabled = False
        End If

    End Sub

    Private Sub BindDdlSparePart(ByVal bulan As Integer, ByVal tahun As Integer)

        Dim ct1, ct2 As Integer
        Dim Exist As Boolean
        Dim Group As ArrayList = New ArrayList
        Dim ListSpecialItemDetail As ArrayList = New SpecialItemFacade(User).RetrieveSpecialItemPartList(bulan, tahun)

        If ListSpecialItemDetail.Count <> 0 Then
            For ct1 = 0 To ListSpecialItemDetail.Count - 1
                Exist = False
                If Group.Count <> 0 Then
                    For ct2 = 0 To Group.Count - 1
                        If Group.Item(ct2) = CType(ListSpecialItemDetail.Item(ct1), VIEW_PartNumberByExtMaterialGroup).PartNumber Then
                            Exist = True
                        End If
                    Next
                End If

                If Not Exist Then
                    Group.Add(CType(ListSpecialItemDetail.Item(ct1), VIEW_PartNumberByExtMaterialGroup).PartNumber)
                End If
            Next
        End If

        dtgSpecialItem.DataSource = New ArrayList
        dtgSpecialItem.VirtualItemCount = 0
        dtgSpecialItem.CurrentPageIndex = 0
        dtgSpecialItem.DataBind()
        ddlSparePart.Items.Clear()
        btnHapus.Enabled = False

        ddlSparePart.Items.Add(New ListItem("Silahkan Pilih", 0))
        If Group.Count <> 0 Then
            For ct1 = 0 To Group.Count - 1
                ddlSparePart.Items.Add(New ListItem(Group.Item(ct1), ct1 + 1))
            Next
            'ddlSparePart.Enabled = True
            'ElseIf Group.Count = 0 Then
            'ddlSparePart.Enabled = False
        End If

    End Sub

    Private Sub BindDdlSparePart(ByVal bulan As Integer, ByVal tahun As Integer, ByVal mtrGroup As String)

        Dim ct1, ct2 As Integer
        Dim Exist As Boolean
        Dim Group As ArrayList = New ArrayList
        Dim ListSpecialItemDetail As ArrayList = New SpecialItemFacade(User).RetrieveSpecialItemPartList(bulan, tahun, mtrGroup)

        If ListSpecialItemDetail.Count <> 0 Then
            For ct1 = 0 To ListSpecialItemDetail.Count - 1
                Exist = False
                If Group.Count <> 0 Then
                    For ct2 = 0 To Group.Count - 1
                        If Group.Item(ct2) = CType(ListSpecialItemDetail.Item(ct1), VIEW_PartNumberByExtMaterialGroup).PartNumber Then
                            Exist = True
                        End If
                    Next
                End If

                If Not Exist Then
                    Group.Add(CType(ListSpecialItemDetail.Item(ct1), VIEW_PartNumberByExtMaterialGroup).PartNumber)
                End If
            Next
        End If

        dtgSpecialItem.DataSource = New ArrayList
        dtgSpecialItem.VirtualItemCount = 0
        dtgSpecialItem.CurrentPageIndex = 0
        dtgSpecialItem.DataBind()
        btnHapus.Enabled = False

        ddlSparePart.Items.Clear()

        ddlSparePart.Items.Add(New ListItem("Silahkan Pilih", 0))
        If Group.Count <> 0 Then
            For ct1 = 0 To Group.Count - 1
                ddlSparePart.Items.Add(New ListItem(Group.Item(ct1), ct1 + 1))
            Next
            'ddlSparePart.Enabled = True
            'ElseIf Group.Count = 0 Then
            'ddlSparePart.Enabled = False
        End If

    End Sub

    Private Sub AddSessionBack()

        Dim ArrGroup As ArrayList = New ArrayList
        Dim ArrSpare As ArrayList = New ArrayList
        Dim ct As Integer

        'Todo session
        Session.Add("SelectionDdlMonth", ddlPeriodMonth.SelectedIndex)

        'Todo session
        Session.Add("TextYear", ddlPeriodYear.SelectedValue)

        For ct = 0 To ddlExtMaterialGroup.Items.Count - 1
            ArrGroup.Add(ddlExtMaterialGroup.Items(ct).Text)
        Next

        For ct = 0 To ddlSparePart.Items.Count - 1
            ArrSpare.Add(ddlSparePart.Items(ct).Text)
        Next

        'Todo session
        Session.Add("ListingDdlGroup", ArrGroup)
        'Todo session
        Session.Add("SelectionDdlGroup", ddlExtMaterialGroup.SelectedIndex)

        'Todo session
        Session.Add("ListingDdlSpare", ArrSpare)
        'Todo session
        Session.Add("SelectionDdlSpare", ddlSparePart.SelectedIndex)

    End Sub

    Private Sub ClearSessionBack()

        Session.Remove("SelectionDdlMonth")

        Session.Remove("TextYear")

        Session.Remove("ListingDdlGroup")
        Session.Remove("SelectionDdlGroup")

        Session.Remove("ListingDdlSpare")
        Session.Remove("SelectionDdlSpare")

    End Sub

#End Region

#Region "Event Handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiatePrivilage()
        If Not IsPostBack Then
            If Session.Item("StatusBack") Then
                InitialBack()
            Else
                Initial()
            End If
            'Todo session
            Session.Add("StatusBack", False)
        End If
        ControlsScriptInjection()
    End Sub

    Private Sub btnHapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHapus.Click
        Dim objSpecialItemFacade As SpecialItemFacade = New SpecialItemFacade(User)
        Dim crit As CriteriaComposite = CType(Session.Item("SpecialItemDetailCriteria"), CriteriaComposite)
        Dim ListSpecialItemDetail As ArrayList = objSpecialItemFacade.RetrieveSpecialItemDetailList(crit)
        If ListSpecialItemDetail.Count > 0 Then
            Try
                For Each ItemDetail As SpecialItemDetail In ListSpecialItemDetail
                    objSpecialItemFacade.DeleteSpecialItemDetail(ItemDetail)
                Next
                MessageBox.Show(SR.DeleteSucces)
                Initial()
            Catch ex As Exception
                MessageBox.Show(SR.DeleteFail)
            End Try
        Else
            MessageBox.Show(SR.DeleteFail)
        End If

    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click

        If Not Session.Item("StatusBack") Then
            If Not Page.IsValid Then
                Return
            End If
        End If

        BindDtgSpecialItem(1)

    End Sub

    Private Sub dtgSpecialItem_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSpecialItem.ItemDataBound

        If e.Item.ItemIndex <> -1 Then

            counter = counter + 1
            If counter = 1 Or counter = 2 Then
                e.Item.BackColor = Color.White
            Else
                e.Item.BackColor = Color.FromArgb(241, 246, 251)
                If counter = 4 Then
                    counter = 0
                End If
            End If

            If CType(e.Item.DataItem, SpecialItemDetail).ItemStatus = 1 Then
                e.Item.BackColor = Color.FromArgb(255, 255, 204)
            ElseIf CType(e.Item.DataItem, SpecialItemDetail).ItemStatus = 2 Then
                e.Item.BackColor = Color.FromArgb(204, 255, 204)
            End If

            If (e.Item.ItemIndex Mod 2 = 0) Then
                e.Item.Cells(0).Text = CType(e.Item.DataItem, SpecialItemDetail).ID
                e.Item.Cells(1).Text = CStr(((dtgSpecialItem.PageSize / 2) * dtgSpecialItem.CurrentPageIndex) + e.Item.ItemIndex + 1 - (e.Item.ItemIndex / 2))
                CType(e.Item.FindControl("lnkPartNo"), LinkButton).Text = CType(e.Item.DataItem, SpecialItemDetail).SparePartMaster.PartNumber
                'e.Item.Cells(2).Text = CType(e.Item.DataItem, SpecialItemDetail).SparePartMaster.PartNumber
                e.Item.Cells(3).Text = CType(e.Item.DataItem, SpecialItemDetail).PartName
                e.Item.Cells(4).Text = CType(e.Item.DataItem, SpecialItemDetail).ModelCode

                Dim jmlKolom As Integer = Me.dtgSpecialItem.Columns.Count - 5

                If Not CType(e.Item.DataItem, SpecialItemDetail).SpecialItemPackages Is Nothing Then
                    If CType(e.Item.DataItem, SpecialItemDetail).SpecialItemPackages.Count > 0 Then
                        For counter As Integer = 5 To jmlKolom + 4
                            Dim pack As SpecialItemPackage
                            If CType(e.Item.DataItem, SpecialItemDetail).SpecialItemPackages.Count > counter - 5 Then
                                pack = CType(CType(e.Item.DataItem, SpecialItemDetail).SpecialItemPackages.Item(counter - 5), SpecialItemPackage)
                                e.Item.Cells(counter).Text = String.Format("{0:#,###}", pack.PackagePrice)
                                e.Item.Cells(counter).HorizontalAlign = HorizontalAlign.Right
                            End If
                        Next
                    End If
                End If
            Else
                e.Item.Cells(0).Text = ""
                e.Item.Cells(1).Text = ""
                e.Item.Cells(2).Text = CType(e.Item.DataItem, SpecialItemDetail).Remark
                e.Item.Cells(2).ForeColor = Color.FromArgb(255, 153, 51)
                e.Item.Cells(3).Text = ""
                e.Item.Cells(4).Text = ""

                Dim jmlKolom As Integer = Me.dtgSpecialItem.Columns.Count - 5

                If Not CType(e.Item.DataItem, SpecialItemDetail).SpecialItemPackages Is Nothing Then
                    If CType(e.Item.DataItem, SpecialItemDetail).SpecialItemPackages.Count > 0 Then
                        For counter As Integer = 5 To jmlKolom + 4
                            Dim pack As SpecialItemPackage
                            If CType(e.Item.DataItem, SpecialItemDetail).SpecialItemPackages.Count > counter - 5 Then
                                pack = CType(CType(e.Item.DataItem, SpecialItemDetail).SpecialItemPackages.Item(counter - 5), SpecialItemPackage)
                                e.Item.Cells(counter).Text = pack.PackageDescription
                                e.Item.Cells(counter).ForeColor = Color.FromArgb(255, 153, 51)
                            Else
                                e.Item.Cells(counter).Text = "&nbsp;"
                            End If
                        Next
                    End If
                End If
            End If
        End If


        'If e.Item.ItemIndex <> -1 Then
        '    'e.Item.Cells(0).Text = ""
        '    e.Item.Cells(0).Text = CType(e.Item.DataItem, SpecialItemDetail).ID

        '    e.Item.Cells(1).Text = CStr((dtgSpecialItem.PageSize * dtgSpecialItem.CurrentPageIndex) + e.Item.ItemIndex + 1)
        '    e.Item.Cells(1).Text = e.Item.Cells(1).Text & "<DIV style=""WIDTH: 100%; BACKGROUND-COLOR: #FFFFC0"">&nbsp;</DIV>"
        '    e.Item.Cells(1).VerticalAlign = VerticalAlign.Top

        '    'e.Item.Cells(2).Text = CType(e.Item.DataItem, SpecialItemDetail).SparePartMaster.PartNumber
        '    'e.Item.Cells(2).Text = e.Item.Cells(2).Text & "<DIV style=""WIDTH: 100%; BACKGROUND-COLOR: #FFFFC0""></DIV>"
        '    'e.Item.Cells(2).VerticalAlign = VerticalAlign.Top

        '    CType(e.Item.FindControl("lnkPartNo"), LinkButton).Text = CType(e.Item.DataItem, SpecialItemDetail).SparePartMaster.PartNumber
        '    CType(e.Item.FindControl("lnkPartNo"), LinkButton).Text = CType(e.Item.FindControl("lnkPartNo"), LinkButton).Text & "<DIV style=""WIDTH: 100%; BACKGROUND-COLOR: #FFFFC0"">" & CType(e.Item.DataItem, SpecialItemDetail).Remark & "</DIV>"
        '    e.Item.Cells(2).VerticalAlign = VerticalAlign.Top

        '    e.Item.Cells(3).Text = CType(e.Item.DataItem, SpecialItemDetail).SparePartMaster.PartName
        '    e.Item.Cells(3).Text = e.Item.Cells(3).Text & "<DIV style=""WIDTH: 100%; BACKGROUND-COLOR: #FFFFC0"">&nbsp;</DIV>"
        '    e.Item.Cells(3).VerticalAlign = VerticalAlign.Top

        '    e.Item.Cells(4).Text = CType(e.Item.DataItem, SpecialItemDetail).SparePartMaster.ModelCode
        '    e.Item.Cells(4).Text = e.Item.Cells(4).Text & "<DIV style=""WIDTH: 100%; BACKGROUND-COLOR: #FFFFC0"">&nbsp;</DIV>"
        '    e.Item.Cells(4).VerticalAlign = VerticalAlign.Top

        '    Dim jmlKolom As Integer = Me.dtgSpecialItem.Columns.Count - 5

        '    If Not CType(e.Item.DataItem, SpecialItemDetail).SpecialItemPackages Is Nothing Then
        '        If CType(e.Item.DataItem, SpecialItemDetail).SpecialItemPackages.Count > 0 Then
        '            For counter As Integer = 5 To jmlKolom + 4
        '                Dim pack As SpecialItemPackage
        '                If CType(e.Item.DataItem, SpecialItemDetail).SpecialItemPackages.Count > counter - 5 Then
        '                    pack = CType(CType(e.Item.DataItem, SpecialItemDetail).SpecialItemPackages.Item(counter - 5), SpecialItemPackage)
        '                    e.Item.Cells(counter).Text = String.Format("{0:#,###}", pack.PackagePrice)
        '                    e.Item.Cells(counter).Text = e.Item.Cells(counter).Text & "<DIV style=""WIDTH: 100%; BACKGROUND-COLOR: #FFFFC0"">" & pack.PackageDescription & "</DIV>"
        '                    e.Item.Cells(counter).VerticalAlign = VerticalAlign.Top
        '                Else
        '                    e.Item.Cells(counter).Text = "&nbsp;<br>"
        '                    e.Item.Cells(counter).Text = e.Item.Cells(counter).Text & "<DIV style=""WIDTH: 100%; BACKGROUND-COLOR: #FFFFC0"">&nbsp;</DIV>"
        '                    e.Item.Cells(counter).VerticalAlign = VerticalAlign.Top
        '                End If
        '            Next
        '            'Dim counter As Integer = 4
        '            'For Each pack As SpecialItemPackage In CType(e.Item.DataItem, SpecialItemDetail).SpecialItemPackages
        '            '    counter += 1
        '            '    e.Item.Cells(counter).Text = String.Format("{0:#,###}", pack.PackagePrice)
        '            '    e.Item.Cells(counter).Text = e.Item.Cells(counter).Text & "<DIV style=""WIDTH: 100%; BACKGROUND-COLOR: #FFFFC0"">" & pack.PackageDescription & "</DIV>"
        '            'Next
        '        End If
        '    End If

        '    'If Not CType(e.Item.DataItem, SpecialItemDetail).SpecialItemPackages Is Nothing Then
        '    '    If CType(e.Item.DataItem, SpecialItemDetail).SpecialItemPackages.Count > 0 Then
        '    '        For Each pack As SpecialItemPackage In CType(e.Item.DataItem, SpecialItemDetail).SpecialItemPackages
        '    '            e.Item.Cells(Me.Packages.IndexOf(pack.PackageDescription) + 5).Text = String.Format("{0:#,###}", pack.PackagePrice)
        '    '        Next
        '    '    End If
        '    'End If

        'End If
        ''me.dtgSpecialItem
    End Sub

    Private Sub dtgSpecialItem_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgSpecialItem.ItemCommand
        If e.CommandName = "lnkPartNo" Then
            'Dim arrSpecialItemDetail As ArrayList = New SpecialItemFacade(User).RetrieveSpecialItemDetailListByID(CType(e.Item.Cells(0).Text, Integer))
            'Session.Add("SpecialItemDetailList", arrSpecialItemDetail)
            'Todo session
            Session.Add("IDSpecialItemDetail", CType(e.Item.Cells(0).Text, Integer))
            AddSessionBack()
            Response.Redirect("../SparePart/FrmModifySpecialItemSubmit.aspx")
        End If
    End Sub

    Private Sub dtgSpecialItem_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgSpecialItem.SortCommand
        If dtgSpecialItem.VirtualItemCount <> 0 Then
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

            dtgSpecialItem.SelectedIndex = -1
            dtgSpecialItem.CurrentPageIndex = 0
            BindDtgSpecialItem(dtgSpecialItem.CurrentPageIndex)
        End If
    End Sub

    Private Sub dtgSpecialItem_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgSpecialItem.PageIndexChanged
        dtgSpecialItem.SelectedIndex = -1
        dtgSpecialItem.CurrentPageIndex = e.NewPageIndex
        BindDtgSpecialItem(dtgSpecialItem.CurrentPageIndex + 1)
    End Sub

    Private Sub ddlPeriodMonth_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlPeriodMonth.SelectedIndexChanged

        If ddlPeriodMonth.SelectedValue >= 0 And (ddlPeriodYear.SelectedValue <> String.Empty Or ddlPeriodYear.SelectedValue <> "") Then
            BindDdlExtMaterialGroup(CType(ddlPeriodMonth.SelectedValue, Integer) + 1, CType(ddlPeriodYear.SelectedValue, Integer))
            BindDdlSparePart(CType(ddlPeriodMonth.SelectedValue, Integer) + 1, CType(ddlPeriodYear.SelectedValue, Integer))
        End If

    End Sub

    Private Sub ddlPeriodYear_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlPeriodYear.SelectedIndexChanged
        If ddlPeriodMonth.SelectedValue >= 0 And (ddlPeriodYear.SelectedValue <> String.Empty Or ddlPeriodYear.SelectedValue <> "") Then
            BindDdlExtMaterialGroup(CType(ddlPeriodMonth.SelectedValue, Integer) + 1, CType(ddlPeriodYear.SelectedValue, Integer))
            BindDdlSparePart(CType(ddlPeriodMonth.SelectedValue, Integer) + 1, CType(ddlPeriodYear.SelectedValue, Integer))
        End If
    End Sub


    Private Sub ddlExtMaterialGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlExtMaterialGroup.SelectedIndexChanged

        If ddlExtMaterialGroup.SelectedValue > 0 And ddlPeriodMonth.SelectedValue >= 0 And (ddlPeriodYear.SelectedValue <> String.Empty Or ddlPeriodYear.SelectedValue <> "") Then
            BindDdlSparePart(CType(ddlPeriodMonth.SelectedValue, Integer) + 1, CType(ddlPeriodYear.SelectedValue, Integer), CType(ddlExtMaterialGroup.SelectedItem.Text, String))
        ElseIf ddlExtMaterialGroup.SelectedValue = 0 And ddlPeriodMonth.SelectedValue >= 0 And (ddlPeriodYear.SelectedValue <> String.Empty Or ddlPeriodYear.SelectedValue <> "") Then
            BindDdlSparePart(CType(ddlPeriodMonth.SelectedValue, Integer) + 1, CType(ddlPeriodYear.SelectedValue, Integer))
        End If
        'btnCari_Click(Nothing, Nothing)
    End Sub

#End Region

End Class
