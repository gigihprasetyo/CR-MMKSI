#Region "custom namespace"
Imports Ktb.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.SparePart
Imports Ktb.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports System.Data.SqlClient
Imports Ktb.DNet.Utility
Imports KTB.DNet.Security
#End Region

Public Class FrmModifySpecialItemSubmit
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgSpecialItem As System.Web.UI.WebControls.DataGrid
    Protected WithEvents txtTahun As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtBulan As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtGroup As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtPartNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnDelete As System.Web.UI.WebControls.Button
    Protected WithEvents btnSubmit As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents txtPartName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtModel As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtReference As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtRemark As System.Web.UI.WebControls.TextBox
    Private Packages As ArrayList = New ArrayList
    Protected WithEvents btnKembali As System.Web.UI.WebControls.Button
    Private ListSpecialItemDetail As ArrayList = New ArrayList

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "private variable"
    Private _HapusDetail As Boolean = False
    Private _SimpanDetail As Boolean = False
    Private _KembaliDetail As Boolean = False
    Private counter As Integer = 0
#End Region

#Region "Event Handler"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitPrivilage()
        If Not IsPostBack Then
            BindDtgSpecialItem()
        End If
        ControlsScriptInjection()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        Dim objSpecialItemDetail As SpecialItemDetail = New SpecialItemFacade(User).RetrieveSpecialItemDetail(Session.Item("IDSpecialItemDetail"))
        'Todo session
        Session.Add("IDSpecialItemHeader", CType(objSpecialItemDetail.SpecialItemHeader.ID, Integer))

        If DeleteSpecialItemDetail(objSpecialItemDetail) <> -1 Then
            MessageBox.Show(SR.DeleteSucces)
            'Todo session
            Session.Add("StatusBack", True)
            Response.Redirect("../SparePart/FrmModifySpecialItem.aspx")
        Else
            MessageBox.Show(SR.DeleteFail)
        End If
    End Sub

    Private Sub btnKembali_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKembali.Click
        'Todo session
        Session.Add("StatusBack", True)
        Response.Redirect("../SparePart/FrmModifySpecialItem.aspx")
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click

        'sampe di sini mo update
        Dim objSpecialItemDetail As SpecialItemDetail = New SpecialItemFacade(User).RetrieveSpecialItemDetail(Session.Item("IDSpecialItemDetail"))
        Dim objSpecialItemHeader As SpecialItemHeader = New SpecialItemFacade(User).RetrieveSpecialItemHeader(objSpecialItemDetail.SpecialItemHeader.ID)

        objSpecialItemDetail.Remark = txtRemark.Text.Trim
        If UpdateSpecialItem(objSpecialItemHeader, objSpecialItemDetail) <> -1 Then
            MessageBox.Show(SR.UpdateSucces)
        Else
            MessageBox.Show(SR.UpdateFail)
        End If


        BindDtgSpecialItem()
    End Sub
    Private Sub dtgSpecialItem_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSpecialItem.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            initTextBox(CType(e.Item.DataItem, SpecialItemDetail).SpecialItemHeader.MonthPeriode, _
                    CType((CType(e.Item.DataItem, SpecialItemDetail).SpecialItemHeader.YearPeriode), String), _
                    CType(e.Item.DataItem, SpecialItemDetail).ExtMaterialGroup, _
                    CType(e.Item.DataItem, SpecialItemDetail).ModelCode, _
                    CType(e.Item.DataItem, SpecialItemDetail).SparePartMaster.PartNumber, _
                    CType(e.Item.DataItem, SpecialItemDetail).PartName, _
                    CType(e.Item.DataItem, SpecialItemDetail).SpecialItemHeader.Reference, _
                    CType(e.Item.DataItem, SpecialItemDetail).Remark)

            'counter = counter + 1
            'If counter = 1 Or counter = 2 Then
            '    e.Item.BackColor = Color.White
            'Else
            '    e.Item.BackColor = Color.FromArgb(241, 246, 251)
            '    If counter = 4 Then
            '        counter = 0
            '    End If
            'End If

            If (e.Item.ItemIndex Mod 2 = 0) Then

                If CType(e.Item.DataItem, SpecialItemDetail).ItemStatus = 1 Then
                    e.Item.BackColor = Color.FromArgb(255, 255, 204)
                ElseIf CType(e.Item.DataItem, SpecialItemDetail).ItemStatus = 2 Then
                    e.Item.BackColor = Color.FromArgb(204, 255, 204)
                End If

                e.Item.Cells(0).Text = CType(e.Item.DataItem, SpecialItemDetail).ID
                e.Item.Cells(1).Text = CStr(((dtgSpecialItem.PageSize / 2) * dtgSpecialItem.CurrentPageIndex) + e.Item.ItemIndex + 1 - (e.Item.ItemIndex / 2))
                CType(e.Item.FindControl("lblNo"), Label).Text = CType(e.Item.DataItem, SpecialItemDetail).SparePartMaster.PartNumber
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

        '    initTextBox(CType(e.Item.DataItem, SpecialItemDetail).SpecialItemHeader.MonthPeriode, _
        '                CType((CType(e.Item.DataItem, SpecialItemDetail).SpecialItemHeader.YearPeriode), String), _
        '                CType(e.Item.DataItem, SpecialItemDetail).ExtMaterialGroup, _
        '                CType(e.Item.DataItem, SpecialItemDetail).SparePartMaster.ModelCode, _
        '                CType(e.Item.DataItem, SpecialItemDetail).SparePartMaster.PartNumber, _
        '                CType(e.Item.DataItem, SpecialItemDetail).SparePartMaster.PartName, _
        '                CType(e.Item.DataItem, SpecialItemDetail).SpecialItemHeader.Reference, _
        '                CType(e.Item.DataItem, SpecialItemDetail).Remark)

        '    'e.Item.Cells(0).Text = ""
        '    e.Item.Cells(0).Text = CType(e.Item.DataItem, SpecialItemDetail).ID
        '    e.Item.Cells(1).Text = CStr(e.Item.ItemIndex + 1)
        '    e.Item.Cells(1).Text = e.Item.Cells(1).Text & "<DIV style=""WIDTH: 100%; BACKGROUND-COLOR: #FFFFC0"">&nbsp;</DIV>"
        '    e.Item.Cells(1).VerticalAlign = VerticalAlign.Top

        '    e.Item.Cells(2).Text = CType(e.Item.DataItem, SpecialItemDetail).SparePartMaster.PartNumber
        '    e.Item.Cells(2).Text = e.Item.Cells(2).Text & "<DIV style=""WIDTH: 100%; BACKGROUND-COLOR: #FFFFC0"">&nbsp;</DIV>"
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
        '            '    e.Item.Cells(counter).Text = e.Item.Cells(counter).Text & "<SPAN style=""WIDTH: 100%; BACKGROUND-COLOR: #ffcc66"">" & pack.PackageDescription & "</SPAN>"
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


#End Region

#Region "Private Function/Sub"

    Private Sub InitPrivilage()

        If Not SecurityProvider.Authorize(context.User, SR.ViewDetailPengelelolaanSpecialItem_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=SPECIAL ITEM - Modifikasi Special Item")
        End If

        If Not SecurityProvider.Authorize(context.User, SR.DeleteDetailPengelelolaanSpecialItem_Privilege) Then
            btnDelete.Visible = False
        End If

        If Not SecurityProvider.Authorize(context.User, SR.SaveDetailPengelelolaanSpecialItem_Privilege) Then
            btnSubmit.Visible = False
        End If

        If Not SecurityProvider.Authorize(context.User, SR.BackDetailPengelelolaanSpecialItem_Privilege) Then
            btnKembali.Visible = False
        End If

    End Sub

    Private Sub AddColumn(ByVal PackageList As ArrayList)
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

    Private Sub initTextBox(ByVal nBulan As Integer, ByVal nTahun As String, ByVal nGroup As String, ByVal nModel As String, ByVal nPartNumber As String, ByVal nPartName As String, ByVal nReference As String, ByVal nRemark As String)

        If nBulan = 1 Then
            txtBulan.Text = "Januari"
        ElseIf nBulan = 2 Then
            txtBulan.Text = "Februari"
        ElseIf nBulan = 3 Then
            txtBulan.Text = "Maret"
        ElseIf nBulan = 4 Then
            txtBulan.Text = "April"
        ElseIf nBulan = 5 Then
            txtBulan.Text = "Mei"
        ElseIf nBulan = 6 Then
            txtBulan.Text = "Juni"
        ElseIf nBulan = 7 Then
            txtBulan.Text = "Juli"
        ElseIf nBulan = 8 Then
            txtBulan.Text = "Agustus"
        ElseIf nBulan = 9 Then
            txtBulan.Text = "September"
        ElseIf nBulan = 10 Then
            txtBulan.Text = "Oktober"
        ElseIf nBulan = 11 Then
            txtBulan.Text = "November"
        ElseIf nBulan = 12 Then
            txtBulan.Text = "Desember"
        End If

        txtTahun.Text = nTahun
        txtGroup.Text = nGroup
        txtModel.Text = nModel
        txtPartNumber.Text = nPartNumber
        txtPartName.Text = nPartName
        txtReference.Text = nReference
        txtRemark.Text = nRemark
    End Sub

    Private Sub BindDtgSpecialItem()

        Dim objSpecialItemFacade As SpecialItemFacade = New SpecialItemFacade(User)
        Dim bIsEmpty As Boolean = True
        Dim listForDG As ArrayList = New ArrayList
        ListSpecialItemDetail = New SpecialItemFacade(User).RetrieveSpecialItemDetailListByID(Session.Item("IDSpecialItemDetail"))

        If Not ListSpecialItemDetail Is Nothing Then
            If ListSpecialItemDetail.Count > 0 Then
                For Each SpecialItem As SpecialItemDetail In ListSpecialItemDetail
                    listForDG.Add(SpecialItem)
                    listForDG.Add(SpecialItem)
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
        Else
            btnSubmit.Enabled = False
            btnDelete.Enabled = False
            MessageBox.Show(SR.ViewFail)
        End If

        If bIsEmpty Then
            Me.dtgSpecialItem.DataSource = New ArrayList
        Else
            'Me.dtgSpecialItem.DataSource = ListSpecialItemDetail
            Me.dtgSpecialItem.DataSource = listForDG
        End If
        Me.dtgSpecialItem.DataBind()
    End Sub

    Private Function DeleteSpecialItemDetail(ByVal objDetail As SpecialItemDetail) As Integer

        Dim nDelDet As Integer = -1

        Try
            nDelDet = New SpecialItemFacade(User).DeleteSpecialItemDetail(objDetail)
        Catch ex As Exception
            nDelDet = -1
        End Try

        Return nDelDet

    End Function

    Private Function UpdateSpecialItem(ByVal objHeader As SpecialItemHeader, ByVal objDetail As SpecialItemDetail) As Integer
        Dim nRetHea As Integer = -1
        Dim nRetDet As Integer = -1

        Try
            nRetHea = New SpecialItemFacade(User).UpdateSpecialItemHeader(objHeader)
            nRetDet = New SpecialItemFacade(User).UpdateSpecialItemDetail(objDetail)
        Catch ex As Exception
            nRetHea = -1
            nRetDet = -1
        End Try

        Return nRetHea

    End Function

    Private Sub ControlsScriptInjection()
        btnDelete.Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")
        'btnSubmit.Attributes.Add("OnClick", "return confirm('" & SR.SubmitConfirmation & "');")
        'chkRequestForCanceled.Attributes.Add("OnClick", "if ( ! confirm('" & CType(ViewState("messCancelKTB"), String) & "')) return false;")
        'chkRequestForCanceled.Attributes.Add("OnClick", "return  confirm('Yakin Pesanan ini akan dibatalkan ?');")
        'Session.Add("ScreenID", 210)
    End Sub

#End Region



End Class
