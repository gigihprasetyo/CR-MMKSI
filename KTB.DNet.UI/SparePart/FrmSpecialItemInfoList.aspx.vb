#Region "Custom Namespace Imports"

Imports Ktb.DNet.BusinessFacade
Imports Ktb.DNet.Domain
Imports System.Data.SqlClient
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports System.IO
Imports KTB.DNet.Security

#End Region


Public Class FrmSpecialItemInfoList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgSpecialItem As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlSparePart As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlExtMaterialGroup As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlPeriodMonth As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents ddlPeriodYear As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblCatatan As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents lbNoReff As System.Web.UI.WebControls.ListBox
    Protected WithEvents txtSparePart As System.Web.UI.WebControls.TextBox

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

    Private Packages As ArrayList = New ArrayList
    Private counter As Integer = 0

#End Region

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.ViewSpecialItemList_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=PEMESANAN - Estimasi Pesanan")
        End If
        '--exclude  this privilege from Asra (BA)
        'Me.btnSearch.Visible = SecurityProvider.Authorize(context.User, SR.SearchSpecialItemList_Privilege)
        Me.btnDownload.Visible = SecurityProvider.Authorize(context.User, SR.DownloadListSpecialItem_Privilege)
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()

        If Not IsPostBack Then
            ViewState("CurrentSortColumn") = ""
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC

            BindDropDownList()
            Me.ddlPeriodMonth.SelectedValue = Date.Now.Month - 1
            Me.ddlPeriodYear.SelectedValue = DateTime.Now.Year.ToString
            BindDdlExtMaterialGroup(CType(ddlPeriodMonth.SelectedValue, Integer) + 1, CType(ddlPeriodYear.SelectedValue, Integer))
            'BindDdlSparePart(CType(ddlPeriodMonth.SelectedValue, Integer) + 1, CType(ddlPeriodYear.SelectedValue, Integer))
            BindListReference(Me.ddlPeriodYear.SelectedValue, Me.ddlPeriodMonth.SelectedValue + 1)

        End If

        'Me.ddlExtMaterialGroup.Enabled = True
        'Me.ddlPeriodMonth.Enabled = True
        'Me.ddlSparePart.Enabled = True
        'Me.btnSearch.Enabled = True

        'Me.ddlExtMaterialGroup.Attributes.Add("onchange", "disableThem();")
        'Me.ddlPeriodMonth.Attributes.Add("onchange", "disableThem();")

    End Sub

    Private Sub BindListReference(ByVal year As Integer, ByVal month As Integer)
        Dim li As ListItem
        Dim spItemFacade As SpecialItemFacade = New SpecialItemFacade(User)
        Dim objSpHeaderList As ArrayList = spItemFacade.GetSpecialItemHeaderByPeriod(year, month)
        lbNoReff.Items.Clear()
        li = New ListItem("Silahkan Pilih", "")
        For Each item As SpecialItemHeader In objSpHeaderList
            li = New ListItem(item.Reference.Split("-")(0), item.Reference)
            lbNoReff.Items.Add(li)
        Next
    End Sub

    Private Sub BindDropDownList()
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
        ' Ext Group

        For Each item As ListItem In LookUp.ArrayYearWithValue(True, 1, 1, DateTime.Now.Year.ToString)
            Me.ddlPeriodYear.Items.Add(item)
        Next

    End Sub

    Private Sub AddColumn(ByVal PackageList As ArrayList)
        For Each Package As String In PackageList
            Dim datagridcol As New BoundColumn
            datagridcol.HeaderStyle.CssClass = "titleTableParts"
            datagridcol.HeaderText = "Harga / Paket (Rp)"
            datagridcol.HeaderStyle.Width = Unit.Pixel(70)
            'datagridcol.HeaderStyle.Width = Unit.Pixel(10)
            Me.dtgSpecialItem.Columns.Add(datagridcol)

        Next
        dtgSpecialItem.Width = Unit.Pixel((PackageList.Count * 70) + 490)
    End Sub

    Private Sub AddToPackages(ByVal packageList As ArrayList, ByVal package As String)
        If Not packageList.Contains(package) Then
            packageList.Add(package)
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If lbNoReff.SelectedValue = "" Then
            MessageBox.Show("Silahkan Pilih Reference.")
        Else
            BindDtgSpecialItem(1)
        End If
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
                e.Item.Cells(0).Text = ""
                e.Item.Cells(1).Text = CStr(((dtgSpecialItem.PageSize / 2) * dtgSpecialItem.CurrentPageIndex) + e.Item.ItemIndex + 1 - (e.Item.ItemIndex / 2))
                e.Item.Cells(2).Text = CType(e.Item.DataItem, SpecialItemDetail).SparePartMaster.PartNumber
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

        '''If e.Item.ItemIndex <> -1 Then
        '''    e.Item.Cells(0).Text = ""
        '''    e.Item.Cells(1).Text = CStr((dtgSpecialItem.PageSize * dtgSpecialItem.CurrentPageIndex) + e.Item.ItemIndex + 1)
        '''    e.Item.Cells(1).Text = e.Item.Cells(1).Text & "<DIV style=""WIDTH: 100%; BACKGROUND-COLOR: #FFFFC0"">&nbsp;</DIV>"
        '''    e.Item.Cells(1).VerticalAlign = VerticalAlign.Top

        '''    e.Item.Cells(2).Text = CType(e.Item.DataItem, SpecialItemDetail).SparePartMaster.PartNumber
        '''    e.Item.Cells(2).Text = e.Item.Cells(2).Text & "<DIV style=""WIDTH: 100%; BACKGROUND-COLOR: #FFFFC0"">&nbsp;</DIV>"
        '''    e.Item.Cells(2).VerticalAlign = VerticalAlign.Top

        '''    e.Item.Cells(3).Text = CType(e.Item.DataItem, SpecialItemDetail).SparePartMaster.PartName
        '''    e.Item.Cells(3).Text = e.Item.Cells(3).Text & "<DIV style=""WIDTH: 100%; BACKGROUND-COLOR: #FFFFC0"">&nbsp;</DIV>"
        '''    e.Item.Cells(3).VerticalAlign = VerticalAlign.Top

        '''    e.Item.Cells(4).Text = CType(e.Item.DataItem, SpecialItemDetail).SparePartMaster.ModelCode
        '''    e.Item.Cells(4).Text = e.Item.Cells(4).Text & "<DIV style=""WIDTH: 100%; BACKGROUND-COLOR: #FFFFC0"">&nbsp;</DIV>"
        '''    e.Item.Cells(4).VerticalAlign = VerticalAlign.Top

        '''    Dim jmlKolom As Integer = Me.dtgSpecialItem.Columns.Count - 5

        '''    If Not CType(e.Item.DataItem, SpecialItemDetail).SpecialItemPackages Is Nothing Then
        '''        If CType(e.Item.DataItem, SpecialItemDetail).SpecialItemPackages.Count > 0 Then
        '''            For counter As Integer = 5 To jmlKolom + 4
        '''                Dim pack As SpecialItemPackage
        '''                If CType(e.Item.DataItem, SpecialItemDetail).SpecialItemPackages.Count > counter - 5 Then
        '''                    pack = CType(CType(e.Item.DataItem, SpecialItemDetail).SpecialItemPackages.Item(counter - 5), SpecialItemPackage)
        '''                    e.Item.Cells(counter).Text = String.Format("{0:#,###}", pack.PackagePrice)
        '''                    e.Item.Cells(counter).Text = e.Item.Cells(counter).Text & "<DIV style=""WIDTH: 100%; BACKGROUND-COLOR: #FFFFC0"">" & pack.PackageDescription & "</DIV>"
        '''                    e.Item.Cells(counter).VerticalAlign = VerticalAlign.Top
        '''                Else
        '''                    e.Item.Cells(counter).Text = "&nbsp;<br>"
        '''                    e.Item.Cells(counter).Text = e.Item.Cells(counter).Text & "<DIV style=""WIDTH: 100%; BACKGROUND-COLOR: #FFFFC0"">&nbsp;</DIV>"
        '''                    e.Item.Cells(counter).VerticalAlign = VerticalAlign.Top
        '''                End If

        '''            Next
        '''            'Dim counter As Integer = 4
        '''            'For Each pack As SpecialItemPackage In CType(e.Item.DataItem, SpecialItemDetail).SpecialItemPackages
        '''            '    counter += 1
        '''            '    e.Item.Cells(counter).Text = String.Format("{0:#,###}", pack.PackagePrice)
        '''            '    e.Item.Cells(counter).Text = e.Item.Cells(counter).Text & "<DIV style=""WIDTH: 100%; BACKGROUND-COLOR: #FFFFC0"">" & pack.PackageDescription & "</DIV>"
        '''            'Next
        '''        End If
        '''    End If

        '''    'If Not CType(e.Item.DataItem, SpecialItemDetail).SpecialItemPackages Is Nothing Then
        '''    '    If CType(e.Item.DataItem, SpecialItemDetail).SpecialItemPackages.Count > 0 Then
        '''    '        For Each pack As SpecialItemPackage In CType(e.Item.DataItem, SpecialItemDetail).SpecialItemPackages
        '''    '            e.Item.Cells(Me.Packages.IndexOf(pack.PackageDescription) + 5).Text = String.Format("{0:#,###}", pack.PackagePrice)
        '''    '        Next
        '''    '    End If
        '''    'End If

        '''End If
    End Sub

    Private Sub BindDdlExtMaterialGroup(ByVal bulan As Integer, ByVal tahun As Integer)

        Dim ct1, ct2 As Integer
        Dim Exist As Boolean
        Dim Group As ArrayList = New ArrayList
        Dim ListSpecialItemDetail As ArrayList = New SpecialItemFacade(User).RetrieveSpecialItemGroupList(bulan, tahun)

        ddlExtMaterialGroup.Items.Clear()

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

        btnDownload.Enabled = False

        ddlExtMaterialGroup.Items.Add(New ListItem("Silahkan Pilih", 0))
        If Group.Count > 0 Then
            For ct1 = 0 To Group.Count - 1
                ddlExtMaterialGroup.Items.Add(New ListItem(Group.Item(ct1), ct1 + 1))
            Next
        End If

        ddlExtMaterialGroup.SelectedIndex = 0
        'BindDdlSparePart(CType(ddlPeriodMonth.SelectedValue, Integer) + 1, CType(ddlPeriodYear.SelectedValue, Integer), CType(ddlExtMaterialGroup.SelectedItem.Text, String))

    End Sub

    Private Function DownLoadFile(ByVal fileName As String) As Integer
        Dim nReturn As Int16 = 0
        Dim fileInfo0 As FileInfo
        'Dim fileInfo1 As New FileInfo(Server.MapPath(""))
        Dim fileInfo1 As New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SAN"))
        Dim destFilePath As String
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim success As Boolean = False
        Dim newFileInfo As FileInfo

        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, KTB.DNet.Lib.WebConfig.GetValue("WebServer"))

        fileInfo0 = New FileInfo(KTB.DNet.Lib.WebConfig.GetValue("SPFileDirectory").ToString & "\" & fileName.ToUpper.Trim)
        destFilePath = fileInfo1.Directory.FullName & "\" & "DataFile\SP\" & fileName.ToUpper.Trim
        newFileInfo = New FileInfo(destFilePath)

        Try
            success = imp.Start()
            If success Then
                If Not newFileInfo.Directory.Exists Then
                    newFileInfo.Directory.Create()
                End If
                success = False
                newFileInfo = New FileInfo(destFilePath)
                If Not newFileInfo.Directory.Exists Then
                    newFileInfo.Directory.Create()
                End If
                If (fileInfo0.Exists) Then
                    fileInfo0.CopyTo(destFilePath, True)
                    imp.StopImpersonate()
                    imp = Nothing
                    Response.Redirect("../Download.aspx?file=DataFile\SP\" & fileName.ToUpper.Trim)
                Else
                    nReturn = -1 'MessageBox.Show("SDGROUP0" & i.ToString & ".DLR:  File tidak ada")
                End If
            Else
                nReturn = -1
            End If
        Catch ex As Exception
            nReturn = -1
        End Try
        Return nReturn
    End Function

    'Private Sub BindDdlSparePart(ByVal bulan As Integer, ByVal tahun As Integer, ByVal mtrGroup As String)
    '    ddlSparePart.Items.Clear()
    '    ddlSparePart.Items.Add(New ListItem("Silahkan Pilih", 0))

    '    If Me.ddlExtMaterialGroup.SelectedValue > 0 Then
    '        Dim ct1, ct2 As Integer
    '        Dim Exist As Boolean
    '        Dim Group As ArrayList = New ArrayList
    '        Dim ListSpecialItemDetail As ArrayList = New SpecialItemFacade(User).RetrieveSpecialItemPartList(bulan, tahun, mtrGroup)

    '        If ListSpecialItemDetail.Count <> 0 Then
    '            For ct1 = 0 To ListSpecialItemDetail.Count - 1
    '                Exist = False
    '                If Group.Count <> 0 Then
    '                    For ct2 = 0 To Group.Count - 1
    '                        If Group.Item(ct2) = CType(ListSpecialItemDetail.Item(ct1), VIEW_PartNumberByExtMaterialGroup).PartNumber Then
    '                            Exist = True
    '                        End If
    '                    Next
    '                End If

    '                If Not Exist Then
    '                    Group.Add(CType(ListSpecialItemDetail.Item(ct1), VIEW_PartNumberByExtMaterialGroup).PartNumber)
    '                End If
    '            Next
    '        End If

    '        btnDownload.Enabled = False

    '        If Group.Count <> 0 Then
    '            For ct1 = 0 To Group.Count - 1
    '                ddlSparePart.Items.Add(New ListItem(Group.Item(ct1), ct1 + 1))
    '            Next
    '        End If

    '    End If

    '    ddlSparePart.SelectedValue = 0

    'End Sub

    Private Sub ddlPeriodMonth_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPeriodMonth.SelectedIndexChanged
        BindDdlExtMaterialGroup(CType(ddlPeriodMonth.SelectedValue, Integer) + 1, CType(ddlPeriodYear.SelectedValue, Integer))
        'BindDdlSparePart(CType(ddlPeriodMonth.SelectedValue, Integer) + 1, CType(ddlPeriodYear.SelectedValue, Integer))
        BindEmptyListToDataGrid()
        BindListReference(Me.ddlPeriodYear.SelectedValue, Me.ddlPeriodMonth.SelectedValue + 1)

    End Sub

    Private Sub BindEmptyListToDataGrid()
        Me.dtgSpecialItem.DataSource = New ArrayList
        Me.dtgSpecialItem.CurrentPageIndex = 0
        Me.dtgSpecialItem.VirtualItemCount = 0
        Me.dtgSpecialItem.DataBind()
        Me.dtgSpecialItem.Visible = False
        Me.lblCatatan.Text = String.Empty
        'Me.lblRefensi.Text = String.Empty
    End Sub

    Private Sub ddlExtMaterialGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlExtMaterialGroup.SelectedIndexChanged
        If ddlExtMaterialGroup.SelectedValue > 0 Then
            'BindDdlSparePart(CType(ddlPeriodMonth.SelectedValue, Integer) + 1, CType(ddlPeriodYear.SelectedValue, Integer), CType(ddlExtMaterialGroup.SelectedItem.Text, String))
        ElseIf ddlExtMaterialGroup.SelectedValue = 0 Then
            'BindDdlSparePart(CType(ddlPeriodMonth.SelectedValue, Integer) + 1, CType(ddlPeriodYear.SelectedValue, Integer))
        End If

        'btnSearch_Click(Nothing, Nothing)
        BindEmptyListToDataGrid()
    End Sub

    Private Function ReturnMonth(ByVal bulan As Integer) As String
        If bulan = 0 Then
            Return "01"
        ElseIf bulan = 1 Then
            Return "02"
        ElseIf bulan = 2 Then
            Return "03"
        ElseIf bulan = 3 Then
            Return "04"
        ElseIf bulan = 4 Then
            Return "05"
        ElseIf bulan = 5 Then
            Return "06"
        ElseIf bulan = 6 Then
            Return "07"
        ElseIf bulan = 7 Then
            Return "08"
        ElseIf bulan = 8 Then
            Return "09"
        ElseIf bulan = 9 Then
            Return "10"
        ElseIf bulan = 10 Then
            Return "11"
        ElseIf bulan = 11 Then
            Return "12"
        End If
    End Function


    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Dim FileName As String = "SpecialItem" & ReturnMonth(ddlPeriodMonth.SelectedIndex) & ddlPeriodYear.SelectedValue.Trim & ".xls"
        If lbNoReff.SelectedValue <> "" Then
            Dim versi() As String = lbNoReff.SelectedValue.Split("-")
            If versi.Length > 1 Then
                FileName = versi(1) & "-" & FileName
            End If

            Try
                If DownLoadFile(FileName) = -1 Then
                    MessageBox.Show(SR.FileNotFound(FileName))
                End If
            Catch ex As Exception
                MessageBox.Show(SR.DownloadFail(FileName))
            End Try
            btnSearch_Click(Nothing, Nothing)
        Else
            MessageBox.Show("Silahkan Pilih Reference.")
        End If

    End Sub

    Private Sub dtgSpecialItem_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgSpecialItem.SortCommand
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
    End Sub

    Private orgReferensi As String = String.Empty

    Private Sub BindDtgSpecialItem(ByVal IndexPage As Integer)
        Dim objSpecialItemFacade As SpecialItemFacade = New SpecialItemFacade(User)
        Dim ListSpecialItemDetail As ArrayList
        Dim listForDG As ArrayList = New ArrayList
        Dim catatanHeader As String = ""
        Dim referensiHeader As String = ""
        Dim bIsEmpty As Boolean = True

        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SpecialItemDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(SpecialItemDetail), "SpecialItemHeader.MonthPeriode", MatchType.Exact, CType(Me.ddlPeriodMonth.SelectedValue.Trim, Integer) + 1))

        crit.opAnd(New Criteria(GetType(SpecialItemDetail), "SpecialItemHeader.YearPeriode", MatchType.Exact, CType(Me.ddlPeriodYear.SelectedValue.Trim, Integer)))

        If ddlExtMaterialGroup.SelectedIndex > 0 Then
            crit.opAnd(New Criteria(GetType(SpecialItemDetail), "ExtMaterialGroup", MatchType.Exact, ddlExtMaterialGroup.SelectedItem.Text))
        End If
        'If ddlSparePart.SelectedIndex > 0 Then
        '    crit.opAnd(New Criteria(GetType(SpecialItemDetail), "SparePartMaster.PartNumber", MatchType.Exact, ddlSparePart.SelectedItem.Text))
        'End If

        If txtSparePart.Text <> String.Empty Then
            crit.opAnd(New Criteria(GetType(SpecialItemDetail), "SparePartMaster.PartNumber", MatchType.Exact, txtSparePart.Text))
        End If

        If lbNoReff.SelectedValue <> "" Then
            crit.opAnd(New Criteria(GetType(SpecialItemDetail), "SpecialItemHeader.Reference", MatchType.Exact, lbNoReff.SelectedValue))
        End If


        Dim totalRow As Integer = 0
        ListSpecialItemDetail = objSpecialItemFacade.RetrieveSpecialItemDetailList(crit, IndexPage, dtgSpecialItem.PageSize / 2, totalRow, CType(ViewState("CurrentSortColumn"), String), ViewState("CurrentSortDirect"))
        totalRow = totalRow * 2
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
            Me.dtgSpecialItem.VirtualItemCount = 0
            btnDownload.Enabled = False
            Me.lblCatatan.Text = String.Empty
            'Me.lblRefensi.Text = String.Empty
        Else
            '''Me.dtgSpecialItem.DataSource = ListSpecialItemDetail
            Me.dtgSpecialItem.DataSource = listForDG
            Me.dtgSpecialItem.VirtualItemCount = totalRow
            btnDownload.Enabled = True
            Me.lblCatatan.Text = catatanHeader
            Dim arrVersi As String() = referensiHeader.Split("-")
            'Me.lblRefensi.Text = arrVersi(0)
            'Me.lblVersi.Text = arrVersi(1)
        End If
        Me.dtgSpecialItem.DataBind()
        Me.dtgSpecialItem.Visible = True
    End Sub

    'Private Sub BindDdlSparePart(ByVal bulan As Integer, ByVal tahun As Integer)

    '    Dim ct1, ct2 As Integer
    '    Dim Exist As Boolean
    '    Dim Group As ArrayList = New ArrayList
    '    Dim ListSpecialItemDetail As ArrayList = New SpecialItemFacade(User).RetrieveSpecialItemPartList(bulan, tahun)

    '    If ListSpecialItemDetail.Count <> 0 Then
    '        For ct1 = 0 To ListSpecialItemDetail.Count - 1
    '            Exist = False
    '            If Group.Count <> 0 Then
    '                For ct2 = 0 To Group.Count - 1
    '                    If Group.Item(ct2) = CType(ListSpecialItemDetail.Item(ct1), VIEW_PartNumberByExtMaterialGroup).PartNumber Then
    '                        Exist = True
    '                    End If
    '                Next
    '            End If

    '            If Not Exist Then
    '                Group.Add(CType(ListSpecialItemDetail.Item(ct1), VIEW_PartNumberByExtMaterialGroup).PartNumber)
    '            End If
    '        Next
    '    End If

    '    dtgSpecialItem.DataSource = New ArrayList
    '    dtgSpecialItem.VirtualItemCount = 0
    '    dtgSpecialItem.CurrentPageIndex = 0
    '    dtgSpecialItem.DataBind()
    '    ddlSparePart.Items.Clear()

    '    ddlSparePart.Items.Add(New ListItem("Silahkan Pilih", 0))
    '    If Group.Count <> 0 Then
    '        For ct1 = 0 To Group.Count - 1
    '            ddlSparePart.Items.Add(New ListItem(Group.Item(ct1), ct1 + 1))
    '        Next
    '        'ddlSparePart.Enabled = True
    '        'ElseIf Group.Count = 0 Then
    '        'ddlSparePart.Enabled = False
    '    End If

    'End Sub

    Private Sub dtgSpecialItem_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgSpecialItem.PageIndexChanged
        dtgSpecialItem.CurrentPageIndex = e.NewPageIndex
        BindDtgSpecialItem(e.NewPageIndex + 1)
    End Sub

    Private Sub ddlPeriodYear_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlPeriodYear.SelectedIndexChanged

        BindDdlExtMaterialGroup(CType(ddlPeriodMonth.SelectedValue, Integer) + 1, CType(ddlPeriodYear.SelectedValue, Integer))
        'BindDdlSparePart(CType(ddlPeriodMonth.SelectedValue, Integer) + 1, CType(ddlPeriodYear.SelectedValue, Integer))

        BindEmptyListToDataGrid()
        BindListReference(Me.ddlPeriodYear.SelectedValue, Me.ddlPeriodMonth.SelectedValue + 1)

    End Sub
End Class
