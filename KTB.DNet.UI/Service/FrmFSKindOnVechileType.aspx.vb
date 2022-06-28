Imports System.IO
Imports OfficeOpenXml
#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
#End Region

Public Class FrmFSKindOnVechileType
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents dtgFSKind As System.Web.UI.WebControls.DataGrid
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlVehicleType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlFSKind As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents ddlFSType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtDurasi As System.Web.UI.WebControls.TextBox
    Protected WithEvents iFSUpload As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents btnDownloadTemplate As System.Web.UI.WebControls.Button
    Protected WithEvents dgUpload As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnSimpanUpload As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    '+++++++++++++++++++++++++++++++++++++++++++++++++++++

#Region "Private Variable"
    Private objDealer As Dealer
    Private sessHelper As SessionHelper = New SessionHelper
    Private m_bFormPrivilege As Boolean = False
    Private SESSIONFSUPLOAD As String = "SESSIONFSUPLOAD"
#End Region

#Region "Form's Event"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not IsPostBack Then
            InitiatePage()
            ViewState("vsSearchVehicleType") = "Load"
            BindDataGrid(0)
            BindDdlFSType()
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If ddlCategory.SelectedIndex = 0 Then
            MessageBox.Show("Silahkan pilih Kategori")
            Return
        End If

        If ddlVehicleType.SelectedIndex = 0 Then
            MessageBox.Show("Silahkan pilih Tipe")
            Return
        End If

        If ddlFSKind.SelectedIndex = 0 Then
            MessageBox.Show("Silahkan pilih Jenis Free Service")
            Return
        End If

        If ddlFSType.SelectedIndex = 0 Then
            MessageBox.Show("Silahkan pilih Tipe FS")
            Return
        End If

        If txtDurasi.Text.Trim = "" Then
            MessageBox.Show("Silahkan Tentukan Durasi")
            Return
        End If

        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Dim categoryArr As ArrayList = New CategoryFacade(User).RetrieveActiveList(companyCode)
        Dim valid As Boolean = False
        For Each item As Category In categoryArr
            If ddlCategory.SelectedValue = item.ID Then
                valid = True
            End If
        Next


        If valid Then
            Simpan()
        Else
            MessageBox.Show("Kategori tidak sesuai")
        End If

    End Sub

    Private Sub btnBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearData()
        dtgFSKind.CurrentPageIndex = 0
        dtgFSKind.SelectedIndex = -1
        BindDataGrid(dtgFSKind.CurrentPageIndex)
    End Sub
    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        ViewState("vsSearchVehicleType") = "Search"
        dtgFSKind.CurrentPageIndex = 0
        BindDataGrid(dtgFSKind.CurrentPageIndex)
    End Sub
    Private Sub dtgFSKind_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgFSKind.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then

            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('" & SR.DeleteConfirmation & "');")
            End If

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
                CType(e.Item.FindControl("lblNo"), Label).Text = CType(e.Item.ItemIndex + 1 + (dtgFSKind.CurrentPageIndex * dtgFSKind.PageSize), String)
            End If

            Dim lblFSType As Label = CType(e.Item.FindControl("lblFSType"), Label)
            Dim lblDurasi As Label = CType(e.Item.FindControl("lblDurasi"), Label)
            Dim rowVal As FSKindOnVechileType = CType(e.Item.DataItem, FSKindOnVechileType)
            If Not IsNothing(rowVal) Then
                Try
                    lblFSType.Text = New EnumFSKind().TypeByIndex(Integer.Parse(rowVal.FSType))
                    lblDurasi.Text = rowVal.Duration
                Catch
                    lblFSType.Text = String.Empty
                    lblDurasi.Text = String.Empty
                End Try
            End If

            If Not e.Item.FindControl("lbtnEdit") Is Nothing Then
                'CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = m_bFormPrivilege
                CType(e.Item.FindControl("lbtnEdit"), LinkButton).Visible = False
            End If

            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                'CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = m_bFormPrivilege
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Visible = False
            End If

            If rowVal.Status = 0 Then
                CType(e.Item.FindControl("lbtnActive"), LinkButton).Visible = False
            ElseIf rowVal.Status = 1 Then
                CType(e.Item.FindControl("lbtnInactive"), LinkButton).Visible = False
            End If

        End If
    End Sub

    Private Sub dtgFSKind_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgFSKind.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            Try
                LoadObjectToForm(e.Item.Cells(0).Text, False)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            Try
                LoadObjectToForm(e.Item.Cells(0).Text, True)
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            dtgFSKind.SelectedIndex = e.Item.ItemIndex
        ElseIf e.CommandName = "Delete" Then
            Delete(e.Item.Cells(0).Text)


        ElseIf e.CommandName = "Active" Then
            ActivateParameter(e.Item.Cells(0).Text)
            BindDataGrid(dtgFSKind.CurrentPageIndex)  '-- Bind page-1

        ElseIf e.CommandName = "Inactive" Then
            InActivateParameter(e.Item.Cells(0).Text)
            BindDataGrid(dtgFSKind.CurrentPageIndex)  '-- Bind page-1

        End If
    End Sub

    Private Sub ActivateParameter(ByVal nID As Integer)
        '-- Activate Parameter
        Dim oFSKindOnVechileType As FSKindOnVechileType = New FSKindOnVechileTypeFacade(User).Retrieve(nID)
        oFSKindOnVechileType.Status = 0  '-- Parameter Aktif
        Dim nResult = New FSKindOnVechileTypeFacade(User).Update(oFSKindOnVechileType)
    End Sub

    Private Sub InActivateParameter(ByVal nID As Integer)
        '-- Inactivate Parameter
        Dim oFSKindOnVechileType As FSKindOnVechileType = New FSKindOnVechileTypeFacade(User).Retrieve(nID)
        oFSKindOnVechileType.Status = 1  '-- Parameter Tidak Aktif
        Dim nResult = New FSKindOnVechileTypeFacade(User).Update(oFSKindOnVechileType)
    End Sub

    Private Sub dtgFSKind_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgFSKind.PageIndexChanged
        dtgFSKind.SelectedIndex = -1
        dtgFSKind.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgFSKind.CurrentPageIndex)
    End Sub

    Private Sub dtgFSKind_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgFSKind.SortCommand
        If CType(ViewState("vsSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("vsSortDirect"), Sort.SortDirection)
                Case Sort.SortDirection.ASC
                    ViewState("vsSortDirect") = Sort.SortDirection.DESC
                Case Sort.SortDirection.DESC
                    ViewState("vsSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("vsSortColumn") = e.SortExpression
            ViewState("vsSortDirect") = Sort.SortDirection.ASC
        End If
        dtgFSKind.SelectedIndex = -1
        dtgFSKind.CurrentPageIndex = 0
        BindDataGrid(dtgFSKind.CurrentPageIndex)
        ClearData()
    End Sub

    Private Sub ddlCategory_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCategory.SelectedIndexChanged
        btnSimpan.Enabled = False
        If ddlCategory.SelectedIndex > 0 Then
            btnSimpan.Enabled = True
            BindDdlType()
        End If
    End Sub

    Private Sub BindDdlFSType()
        ddlFSType.Items.Clear()
        Dim listTitle As New EnumFSKind
        Dim al2 As ArrayList = listTitle.RetrieveFSType
        ddlFSType.Items.Add(New ListItem("Pilih Tipe FS", -1))
        For Each item As EnumFSType In al2
            ddlFSType.Items.Add(New ListItem(item.NameTitle, item.ValTitle))
        Next
        ddlFSType.SelectedIndex = 0
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        If iFSUpload.PostedFile.ContentLength = 0 Then
            MessageBox.Show("Pilih berkasterlebih dahulu")
            Return
        End If

        Dim allowedExt As String = ".XLS .XLSX .CSV"
        If Not allowedExt.Contains(Path.GetExtension(iFSUpload.PostedFile.FileName).ToUpper()) Then
            MessageBox.Show("Format file tidak didukung")
            Return
        End If

        If validateItemUpload(iFSUpload.PostedFile) Then
            btnSimpanUpload.Visible = True
        Else
            btnSimpanUpload.Visible = False
        End If
    End Sub

    Protected Sub dgUpload_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgUpload.ItemDataBound
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
                CType(e.Item.FindControl("lblNo"), Label).Text = CType(e.Item.ItemIndex + 1 + (dtgFSKind.CurrentPageIndex * dtgFSKind.PageSize), String)
            End If

            Dim lblKategori As Label = CType(e.Item.FindControl("lblKategori"), Label)
            Dim lblTipe As Label = CType(e.Item.FindControl("lblTipe"), Label)
            Dim lblFSKind As Label = CType(e.Item.FindControl("lblFSKind"), Label)
            Dim lblFSType As Label = CType(e.Item.FindControl("lblFSType"), Label)
            Dim lblDurasi As Label = CType(e.Item.FindControl("lblDurasi"), Label)
            Dim lblMessage As Label = CType(e.Item.FindControl("lblMessage"), Label)
            Dim rowVal As FSKindOnVechileType = CType(e.Item.DataItem, FSKindOnVechileType)
            If Not IsNothing(rowVal) Then
                If Not IsNothing(rowVal.VechileType) Then
                    lblKategori.Text = rowVal.VechileType.Category.CategoryCode
                End If

                If rowVal.ErrorMessage.Contains("Vehicle Type Code salah") Then
                    lblKategori.Text = rowVal.VehicleTypeCode_u
                    lblKategori.ForeColor = Color.Red
                End If

                If rowVal.ErrorMessage.Contains("bukan PC / LCV") Then
                    lblKategori.Text = rowVal.VechileType.Category.CategoryCode
                    lblKategori.ForeColor = Color.Red
                End If

                If Not rowVal.ErrorMessage.Contains("Vehicle Type Code") Then
                    lblTipe.Text = rowVal.VechileType.VechileTypeCode
                Else
                    lblTipe.Text = rowVal.VehicleTypeCode_u
                    lblTipe.ForeColor = Color.Red
                End If

                If Not rowVal.ErrorMessage.Contains("FS Kind") Then
                    lblFSKind.Text = rowVal.FSKind.KindDescription
                Else
                    lblFSKind.Text = rowVal.FSKindCode_u
                    lblFSKind.ForeColor = Color.Red
                End If

                lblFSType.Text = New EnumFSKind().TypeByIndex(Integer.Parse(rowVal.FSType))
                If rowVal.ErrorMessage.Contains("FS Type") Then
                    lblFSType.ForeColor = Color.Red
                End If

                lblDurasi.Text = rowVal.Duration
                If rowVal.ErrorMessage.Contains("FS Type") Then
                    lblDurasi.ForeColor = Color.Red
                End If
                If Not rowVal.ErrorMessage = String.Empty Then
                    lblMessage.Text = rowVal.ErrorMessage
                    lblMessage.ForeColor = Color.Red
                Else
                    If rowVal.IsUpdate Then
                        lblMessage.Text = "Update"
                    Else
                        lblMessage.Text = "Insert"
                    End If
                End If
            End If
        End If
    End Sub

    Protected Sub btnSimpanUpload_Click(sender As Object, e As EventArgs) Handles btnSimpanUpload.Click
        Dim arrUploadFS As New ArrayList
        arrUploadFS = Session(SESSIONFSUPLOAD)
        If IsNothing(arrUploadFS) OrElse arrUploadFS.Count = 0 Then
            MessageBox.Show("Data kosong, harap periksa kembali file upload")
        Else
            Dim oFSKFacade As New FSKindOnVechileTypeFacade(User)
            Dim res As Integer = 0
            For Each item As FSKindOnVechileType In arrUploadFS
                If item.IsUpdate Then
                    'update
                    res = oFSKFacade.Update(item)
                Else
                    'insert
                    res = oFSKFacade.Insert(item)
                End If
            Next
            MessageBox.Show("Simpan data berhasil")
            dgUpload.Visible = False
            btnSimpanUpload.Visible = False
        End If
    End Sub
#End Region

#Region "Private Method"
    Private Sub ActivateUserPrivilege()
        m_bFormPrivilege = SecurityProvider.Authorize(Context.User, SR.ServiceJenisFSUpdate_Privilege)

        If Not SecurityProvider.Authorize(Context.User, SR.ServiceJenisFSView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=FREE SERVICE - Form Jenis Free Service")
        End If
    End Sub

    Private Sub SetControlPrivilege()
        'btnSimpan.Visible = m_bFormPrivilege
        btnSimpan.Visible = False

        btnBatal.Visible = m_bFormPrivilege
        'btnSimpanUpload.Enabled = m_bFormPrivilege
        btnSimpanUpload.Visible = False
    End Sub

    Private Sub InitiatePage()
        ClearData()
        SetControlPrivilege()
        ViewState("vsSortColumn") = "VechileType.Category.CategoryCode"
        ViewState("vsSortDirect") = Sort.SortDirection.ASC
        btnSimpanUpload.Visible = False
        Session(SESSIONFSUPLOAD) = Nothing
    End Sub

    Private Sub ClearData()
        BindDdlCategory()
        BindDdlType()
        BindDdlFSType()
        BindFSType()
        txtDurasi.Text = String.Empty
        ViewState("vsProcess") = "Insert"
        btnSimpan.Enabled = False
        ddlCategory.Enabled = True
        ddlVehicleType.Enabled = True
        ddlFSKind.Enabled = True
        ddlFSType.Enabled = True
        txtDurasi.Enabled = True
    End Sub

    Private Sub BindDataGrid(ByVal index As Integer)
        Dim data As ArrayList
        Try
            Dim totalRow As Integer = 0
            If ViewState("vsSearchVehicleType") = "Load" Then
                data = New FSKindOnVechileTypeFacade(User).RetrieveActiveList(index + 1, dtgFSKind.PageSize, totalRow, ViewState("vsSortColumn"), ViewState("vsSortDirect"))
            Else

                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.FSKindOnVechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                If ddlCategory.SelectedIndex <> 0 Then
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKindOnVechileType), "VechileType.Category.ID", MatchType.Exact, CType(ddlCategory.SelectedValue, Integer)))
                End If
                If ddlVehicleType.SelectedIndex <> 0 Then
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKindOnVechileType), "VechileType.ID", MatchType.Exact, CType(ddlVehicleType.SelectedValue, Integer)))
                End If
                If ddlFSKind.SelectedIndex <> 0 Then
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKindOnVechileType), "FSKind.ID", MatchType.Exact, CType(ddlFSKind.SelectedValue, Integer)))
                End If
                If ddlFSType.SelectedIndex <> 0 Then
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKindOnVechileType), "FSType", MatchType.Exact, CType(ddlFSType.SelectedValue, Integer)))
                End If
                If txtDurasi.Text.Trim <> "" Then
                    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.FSKindOnVechileType), "Duration", MatchType.Exact, CType(txtDurasi.Text.Trim, Integer)))
                End If

                data = New FSKindOnVechileTypeFacade(User).RetrieveActiveList(criterias, index + 1, dtgFSKind.PageSize, _
                      totalRow, CType(ViewState("vsSortColumn"), String), _
                CType(ViewState("vsSortDirect"), Sort.SortDirection))

            End If
            dtgFSKind.DataSource = data
            dtgFSKind.VirtualItemCount = totalRow
            dtgFSKind.DataBind()
        Catch ex As Exception
            dtgFSKind.DataSource = Nothing
            dtgFSKind.VirtualItemCount = 0
            dtgFSKind.DataBind()
            'MessageBox.Show(ex.Message.ToString)
        End Try

    End Sub

    Private Sub BindDdlCategory()
        ddlCategory.Items.Clear()

        Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sortCol As SortCollection = New SortCollection
        sortCol.Add(New Sort(GetType(Category), "CategoryCode", Sort.SortDirection.ASC))
        Dim objCategory As ArrayList = New CategoryFacade(User).Retrieve(critCol, sortCol)

        Dim li As ListItem
        For Each oneCategory As Category In objCategory
            li = New ListItem(oneCategory.CategoryCode, oneCategory.ID.ToString)
            ddlCategory.Items.Add(li)
        Next

        li = New ListItem("Silahkan pilih", "0")
        ddlCategory.Items.Insert(0, li)
    End Sub

    Private Sub BindDdlType()
        ddlVehicleType.Items.Clear()
        Dim li As ListItem

        li = New ListItem("Silahkan pilih", "0")
        ddlVehicleType.Items.Add(li)

        If ddlCategory.SelectedIndex = 0 Then
            Return
        End If

        Dim CategoryID As Integer = Integer.Parse(ddlCategory.SelectedValue)

        Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critCol.opAnd(New Criteria(GetType(VechileType), "Category.ID", MatchType.Exact, CategoryID))
        Dim sortCol As SortCollection = New SortCollection
        sortCol.Add(New Sort(GetType(VechileType), "VechileTypeCode", Sort.SortDirection.ASC))
        Dim objVehicleType As ArrayList = New VechileTypeFacade(User).Retrieve(critCol, sortCol)


        For Each oneVehicleType As VechileType In objVehicleType
            li = New ListItem(oneVehicleType.VechileTypeCode, oneVehicleType.ID.ToString)
            ddlVehicleType.Items.Add(li)
        Next

        btnSimpan.Enabled = True
    End Sub

    Private Sub BindFSType()
        ddlFSKind.Items.Clear()
        Dim critCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim sortCol As SortCollection = New SortCollection
        sortCol.Add(New Sort(GetType(FSKind), "ID", Sort.SortDirection.ASC))
        Dim objFSKind As ArrayList = New FSKindFacade(User).Retrieve(critCol, sortCol)

        Dim li As ListItem
        For Each oneFSKind As FSKind In objFSKind
            li = New ListItem(oneFSKind.KindDescription, oneFSKind.ID.ToString)
            ddlFSKind.Items.Add(li)
        Next

        li = New ListItem("Silahkan pilih", "0")
        ddlFSKind.Items.Insert(0, li)
    End Sub

    Private Sub Simpan()
        Try
            If ViewState("vsProcess") = "Insert" Then
                Insert()
            ElseIf ViewState("vsProcess") = "Edit" Then
                Edit()
            End If
        Catch
            MessageBox.Show(SR.SaveFail)
            ClearData()
            dtgFSKind.CurrentPageIndex = 0
            dtgFSKind.SelectedIndex = -1
            BindDataGrid(dtgFSKind.CurrentPageIndex)
        End Try
    End Sub

    Private Sub Insert()
        Dim objData As FSKindOnVechileType = New FSKindOnVechileType
        LoadFormToObject(objData)
        If IsExistInsert(objData) Then
            MessageBox.Show(SR.DataIsExist("Jenis Free Service"))
        Else
            Dim objFacade As FSKindOnVechileTypeFacade = New FSKindOnVechileTypeFacade(User)
            Dim result As Integer = objFacade.Insert(objData)
            If result > 0 Then
                MessageBox.Show(SR.SaveSuccess)
                ClearData()
                dtgFSKind.CurrentPageIndex = 0
                dtgFSKind.SelectedIndex = -1
                BindDataGrid(dtgFSKind.CurrentPageIndex)
            Else
                MessageBox.Show(SR.SaveFail)
            End If
        End If
    End Sub

    Private Sub Edit()
        Dim objData As FSKindOnVechileType = sessHelper.GetSession("objFSKindOnVehicleType")
        If IsNothing(objData) Then
            MessageBox.Show(SR.DataNotFound("Jenis Free Service"))
            Return
        End If

        Dim newData As FSKindOnVechileType = New FSKindOnVechileType
        LoadFormToObject(newData)

        If objData.FSKind.ID = newData.FSKind.ID _
            And objData.VechileType.ID = newData.VechileType.ID _
            And objData.FSType = newData.FSType _
            And objData.Duration = newData.Duration Then
            MessageBox.Show(SR.SaveSuccess)
            ClearData()
            dtgFSKind.CurrentPageIndex = 0
            dtgFSKind.SelectedIndex = -1
            BindDataGrid(dtgFSKind.CurrentPageIndex)
            Return
        End If

        objData.FSKind = newData.FSKind
        objData.VechileType = newData.VechileType
        objData.FSType = newData.FSType
        objData.Duration = newData.Duration

        'If IsExist(objData) Then
        '    MessageBox.Show(SR.DataNotFound("Jenis Free Service"))
        '    Return
        'End If

        Dim objFacade As FSKindOnVechileTypeFacade = New FSKindOnVechileTypeFacade(User)
        Dim result As Integer = objFacade.Update(objData)
        If result > 0 Then
            MessageBox.Show(SR.SaveSuccess)
            ClearData()
            dtgFSKind.CurrentPageIndex = 0
            dtgFSKind.SelectedIndex = -1
            BindDataGrid(dtgFSKind.CurrentPageIndex)
        Else
            MessageBox.Show(SR.SaveFail)
        End If
    End Sub

    Private Sub LoadFormToObject(ByVal obj As FSKindOnVechileType)
        Dim objFSKind As FSKind = New FSKindFacade(User).Retrieve(CInt(ddlFSKind.SelectedValue))
        Dim objVehicleType As VechileType = New VechileTypeFacade(User).Retrieve(CInt(ddlVehicleType.SelectedValue))
        If IsNothing(objFSKind) Or objFSKind.ID = 0 Then
            Throw New Exception(SR.DataNotFound("Jenis Free Service"))
        End If
        If IsNothing(objVehicleType) Or objVehicleType.ID = 0 Then
            Throw New Exception(SR.DataNotFound("Tipe Service"))
        End If
        obj.FSKind = objFSKind
        obj.VechileType = objVehicleType
        obj.FSType = ddlFSType.SelectedValue
        obj.Duration = txtDurasi.Text.Trim
    End Sub

    Private Sub LoadObjectToForm(ByVal nID As Integer, ByVal Editable As Boolean)
        Try
            Dim obj As FSKindOnVechileType = New FSKindOnVechileTypeFacade(User).Retrieve(nID)
            If IsNothing(obj) Or obj.ID = 0 Then
                Throw New Exception(SR.DataNotFound("Jenis Free Service"))
            End If

            SetDdlCategory(obj)
            SetDdlFSKind(obj)
            If obj.FSType <> "" Then
                SetDdlFSType(obj)
            End If
            txtDurasi.Text = obj.Duration
            sessHelper.SetSession("objFSKindOnVehicleType", obj)
        Catch ex As Exception
            Throw ex
        Finally
            ddlCategory.Enabled = Editable
            ddlVehicleType.Enabled = Editable
            ddlFSKind.Enabled = Editable
            ddlFSType.Enabled = Editable
            btnSimpan.Enabled = Editable
            txtDurasi.Enabled = Editable
        End Try
    End Sub

    Private Sub SetDdlCategory(ByVal obj As FSKindOnVechileType)
        Dim li As ListItem = ddlCategory.Items.FindByValue(obj.VechileType.Category.ID.ToString)
        If IsNothing(li) Then
            Throw New Exception(SR.DataNotFound("Kategori"))
        End If

        ddlCategory.SelectedIndex = ddlCategory.Items.IndexOf(li)

        BindDdlType()
        SetDdlVehicleType(obj)
    End Sub

    Private Sub SetDdlVehicleType(ByVal obj As FSKindOnVechileType)
        Dim li As ListItem = ddlVehicleType.Items.FindByValue(obj.VechileType.ID.ToString)
        If IsNothing(li) Then
            Throw New Exception(SR.DataNotFound("Tipe"))
        End If

        ddlVehicleType.SelectedIndex = ddlVehicleType.Items.IndexOf(li)
    End Sub

    Private Sub SetDdlFSKind(ByVal obj As FSKindOnVechileType)
        Dim li As ListItem = ddlFSKind.Items.FindByValue(obj.FSKind.ID.ToString)
        If IsNothing(li) Then
            Throw New Exception(SR.DataNotFound("Tipe"))
        End If

        ddlFSKind.SelectedIndex = ddlFSKind.Items.IndexOf(li)
    End Sub

    Private Sub SetDdlFSType(ByVal obj As FSKindOnVechileType)
        Dim li As ListItem = ddlFSType.Items.FindByValue(obj.FSType.ToString)
        If IsNothing(li) Then
            Throw New Exception(SR.DataNotFound("Tipe FS"))
        End If

        ddlFSType.SelectedIndex = ddlFSType.Items.IndexOf(li)
    End Sub

    Private Function IsExist(ByVal obj As FSKindOnVechileType) As Boolean
        Dim critComp As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSKindOnVechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critComp.opAnd(New Criteria(GetType(FSKindOnVechileType), "FSKind.ID", MatchType.Exact, obj.FSKind.ID))
        critComp.opAnd(New Criteria(GetType(FSKindOnVechileType), "VechileType.ID", MatchType.Exact, obj.VechileType.ID))
        'If obj.FSType <> "" Then
        '    critComp.opAnd(New Criteria(GetType(FSKindOnVechileType), "FSType", MatchType.Exact, obj.FSType))
        'End If

        Return New FSKindOnVechileTypeFacade(User).Retrieve(critComp).Count > 0
    End Function

    Private Function IsExistInsert(ByVal obj As FSKindOnVechileType) As Boolean
        Dim critComp As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSKindOnVechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critComp.opAnd(New Criteria(GetType(FSKindOnVechileType), "FSKind.ID", MatchType.Exact, obj.FSKind.ID))
        critComp.opAnd(New Criteria(GetType(FSKindOnVechileType), "VechileType.ID", MatchType.Exact, obj.VechileType.ID))

        Return New FSKindOnVechileTypeFacade(User).Retrieve(critComp).Count > 0
    End Function

    Private Function IsExist(ByVal obj As FSKindOnVechileType, ByVal newData As FSKindOnVechileType) As Boolean
        Dim critComp As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FSKindOnVechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critComp.opAnd(New Criteria(GetType(FSKindOnVechileType), "FSKind.ID", MatchType.Exact, obj.FSKind.ID))
        critComp.opAnd(New Criteria(GetType(FSKindOnVechileType), "VechileType.ID", MatchType.Exact, obj.VechileType.ID))
        critComp.opAnd(New Criteria(GetType(FSKindOnVechileType), "FSKind.ID", MatchType.No, newData.FSKind.ID))
        critComp.opAnd(New Criteria(GetType(FSKindOnVechileType), "VechileType.ID", MatchType.No, newData.VechileType.ID))

        Return New FSKindOnVechileTypeFacade(User).Retrieve(critComp).Count > 0
    End Function

    Private Sub Delete(ByVal nID As Integer)
        Dim objFacade As FSKindOnVechileTypeFacade = New FSKindOnVechileTypeFacade(User)
        Dim deletedData As FSKindOnVechileType = objFacade.Retrieve(nID)
        If IsNothing(deletedData) Or deletedData.ID = 0 Then
            MessageBox.Show(SR.DataNotFound("Jenis Free Service"))
        Else
            Try
                Dim nResult = objFacade.DeleteFromDB(deletedData)
                If nResult <= 0 Then
                    MessageBox.Show(SR.DeleteFail)
                Else
                    MessageBox.Show(SR.DeleteSucces)
                End If
            Catch
                MessageBox.Show(SR.DeleteSucces)
            End Try
        End If
        ClearData()
        dtgFSKind.CurrentPageIndex = 0
        dtgFSKind.SelectedIndex = -1
        BindDataGrid(dtgFSKind.CurrentPageIndex)
    End Sub

    Private Function validateItemUpload(ByVal fupload As HttpPostedFile) As Boolean
        Try
            Dim arrUploadFS As New ArrayList()
            Dim result As Boolean = True
            Using pck As New ExcelPackage(fupload.InputStream)
                Dim ws As ExcelWorksheet = pck.Workbook.Worksheets(1)
                Dim row As Integer = 2
                While Not IsNothing(ws.Cells(row, 2).Value)
                    Dim oFSKindOnVch As New FSKindOnVechileType()
                    oFSKindOnVch.ErrorMessage = ""
                    For col As Integer = 2 To 5
                        Select Case col
                            Case 2
                                If IsNothing(ws.Cells(row, col).Value) Then
                                    oFSKindOnVch.ErrorMessage += "Harap isi kolom Vehicle Type code \n"
                                Else
                                    Dim vechileTypeCode As String = ws.Cells(row, col).Value.ToString()
                                    Dim vt As VechileType = New VechileTypeFacade(User).Retrieve(vechileTypeCode)
                                    If vt.ID < 1 Then
                                        oFSKindOnVch.ErrorMessage += "Vehicle Type Code salah \n"
                                        oFSKindOnVch.VehicleTypeCode_u = vechileTypeCode
                                    Else
                                        Dim cat As String = vt.VechileModel.Category.CategoryCode
                                        If Not cat = "PC" And Not cat = "LCV" Then
                                            oFSKindOnVch.ErrorMessage += "Vehicle Type bukan PC / LCV \n"
                                            oFSKindOnVch.VehicleTypeCode_u = vechileTypeCode
                                        Else
                                            oFSKindOnVch.VechileType = vt
                                        End If
                                    End If
                                End If

                            Case 3
                                If IsNothing(ws.Cells(row, col).Value) Then
                                    oFSKindOnVch.ErrorMessage += "Harap isi kolom FS Kind Code \n"
                                Else
                                    Dim fSKindCode As String = ws.Cells(row, col).Value.ToString()
                                    Dim fsk As FSKind = New FSKindFacade(User).Retrieve(fSKindCode)
                                    If fsk.ID < 1 Then
                                        oFSKindOnVch.ErrorMessage += "FS Kind Code salah \n"
                                        oFSKindOnVch.FSKindCode_u = fSKindCode
                                    Else
                                        oFSKindOnVch.FSKind = fsk
                                    End If
                                End If

                            Case 4
                                If IsNothing(ws.Cells(row, col).Value) Then
                                    oFSKindOnVch.ErrorMessage += "Harap isi kolom FS Type \n"
                                Else
                                    Dim fSType As String() = ws.Cells(row, col).Value.ToString().Split("-")
                                    oFSKindOnVch.FSType = fSType(0)
                                    If oFSKindOnVch.FSType = String.Empty Then
                                        oFSKindOnVch.ErrorMessage += "FS Type tidak terdaftar \n"
                                        oFSKindOnVch.FSType = fSType(1)
                                    End If
                                End If

                            Case 5
                                If IsNothing(ws.Cells(row, col).Value) Then
                                    oFSKindOnVch.ErrorMessage += "Harap isi durasi\n"
                                Else
                                    Dim duratioin As Integer = CInt(ws.Cells(row, col).Value)
                                    If duratioin < 1 Then
                                        oFSKindOnVch.ErrorMessage += "Durasi tidak boleh 0 atau minus \n"
                                    End If
                                    oFSKindOnVch.Duration = CInt(duratioin)
                                End If
                        End Select
                    Next

                    If oFSKindOnVch.ErrorMessage = String.Empty Then
                        Dim crit As New CriteriaComposite(New Criteria(GetType(FSKindOnVechileType), "RowStatus", MatchType.Exact, 0))
                        crit.opAnd(New Criteria(GetType(FSKindOnVechileType), "FSKind.ID", MatchType.Exact, oFSKindOnVch.FSKind.ID))
                        crit.opAnd(New Criteria(GetType(FSKindOnVechileType), "VechileType.ID", MatchType.Exact, oFSKindOnVch.VechileType.ID))
                        Dim ret As ArrayList = New FSKindOnVechileTypeFacade(User).Retrieve(crit)
                        If ret.Count > 0 Then
                            oFSKindOnVch.IsUpdate = True
                            oFSKindOnVch.ID = CType(ret(0), FSKindOnVechileType).ID
                        Else
                            oFSKindOnVch.IsUpdate = False
                        End If
                    Else
                        result = False
                    End If

                    arrUploadFS.Add(oFSKindOnVch)
                    row = row + 1
                End While
                dgUpload.DataSource = arrUploadFS
                dgUpload.VirtualItemCount = arrUploadFS.Count
                dgUpload.DataBind()
                Session(SESSIONFSUPLOAD) = arrUploadFS
                Return result
            End Using
        Catch ex As Exception
            MessageBox.Show("File Excel gagal diproses")
            Return False
        End Try

    End Function

    Private Sub createExcelFile(pck As ExcelPackage, fileName As String)
        Dim fileBytes = pck.GetAsByteArray()

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Dim success As Boolean = False

        Try
            success = imp.Start()
            If success Then
                File.WriteAllBytes(Server.MapPath("~/DataTemp/" & fileName), fileBytes)
                imp.StopImpersonate()
            End If

        Catch ex As Exception
            Exit Sub

        End Try

        Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & fileName)
    End Sub

#End Region

    Protected Sub btnDownloadTemplate_Click(sender As Object, e As EventArgs) Handles btnDownloadTemplate.Click
        Dim pck As New ExcelPackage()
        Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add("sheet1")

        Dim ddl As New ArrayList()
        Dim crit As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, 0))
        crit.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "EnumFSKind"))
        ddl = New StandardCodeFacade(User).Retrieve(crit)

        Dim list As DataValidation.Contracts.IExcelDataValidationList = ws.DataValidations.AddListValidation("D2:D99")
        If ddl.Count > 0 Then
            For Each d As StandardCode In ddl
                list.Formula.Values.Add(d.ValueId & "-" & d.ValueDesc)
            Next
        End If

        ws.Cells("B1").Value = "Vechile Type Code"
        ws.Cells("C1").Value = "FS Kind Code"
        ws.Cells("D1").Value = "FS Tipe"
        ws.Cells("E1").Value = "Durasi"
        ws.Cells("B1").Style.Font.Bold = True
        ws.Cells("C1").Style.Font.Bold = True
        ws.Cells("D1").Style.Font.Bold = True
        ws.Cells("E1").Style.Font.Bold = True
        ws.Column(1).Width = 1
        ws.Column(2).Width = 15
        ws.Column(3).Width = 10
        ws.Column(4).Width = 10
        ws.Column(5).Width = 10

        createExcelFile(pck, "FSTemplate.xlsx")
    End Sub
End Class

Public Class FSKindUpload

End Class
