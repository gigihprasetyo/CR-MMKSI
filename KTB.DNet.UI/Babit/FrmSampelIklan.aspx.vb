Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.BabitSalesComm
Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web.UI.WebControls
Imports KTB.DNet.Security

Public Class FrmSampelIklan
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Protected WithEvents FileUploadIklan As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents fileUploadPenggunaan As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents txtNoIklan As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKeterangan As System.Web.UI.WebControls.TextBox
    Protected WithEvents dtgSampelIklan As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Internal Enum"
    Private Enum TipeFile
        Iklan = 1
        Tutorial = 2
    End Enum
#End Region

#Region "Deklarasi"
    Dim sessHelper As New SessionHelper
    Dim oSampleIklan As New SampleIklan
    Dim oSampleIklanFacade As New SampleIklanFacade(User)
#End Region

#Region "Custom Method"
    Private Sub ClearControl()
        txtNoIklan.Text = ""
        txtKeterangan.Text = ""

        txtNoIklan.ReadOnly = False
        txtKeterangan.ReadOnly = False
        FileUploadIklan.Disabled = False
        fileUploadPenggunaan.Disabled = False

        ViewState.Add("ModeUpload", "New")

        btnSimpan.Visible = True
        btnBatal.Visible = False
        btnCari.Visible = True
    End Sub
    Private Sub InitiatePage()
        ClearControl()
        ViewState("CurrentSortColumn") = "NoIklan"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub
    Private Sub BindToGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        Dim criterias = New CriteriaComposite(New Criteria(GetType(SampleIklan), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If txtNoIklan.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(SampleIklan), "NoIklan", MatchType.[Partial], txtNoIklan.Text.Trim))
        End If

        If txtKeterangan.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(SampleIklan), "Note", MatchType.[Partial], txtKeterangan.Text.Trim))
        End If

        Dim arlSampelIklan As ArrayList = New SampleIklanFacade(User).RetrieveActiveList(criterias, indexPage + 1, dtgSampelIklan.PageSize, totalRow, viewstate("CurrentSortColumn"), viewstate("CurrentSortDirect"))

        If arlSampelIklan.Count > 0 Then
            dtgSampelIklan.DataSource = arlSampelIklan
        Else
            MessageBox.Show("Data tidak ditemukan")
            dtgSampelIklan.DataSource = New ArrayList
        End If
        dtgSampelIklan.VirtualItemCount = totalRow
        dtgSampelIklan.DataBind()
    End Sub
    Private Sub LoadData(ByVal id As Integer)
        oSampleIklan = oSampleIklanFacade.Retrieve(id)
        txtNoIklan.Text = oSampleIklan.NoIklan
        txtKeterangan.Text = oSampleIklan.Note

        txtNoIklan.ReadOnly = True
        txtKeterangan.ReadOnly = False
        FileUploadIklan.Disabled = False
        fileUploadPenggunaan.Disabled = False

        btnSimpan.Visible = True
        btnBatal.Visible = True
        btnCari.Visible = False

        ViewState.Add("ModeUpload", "Edit")
        viewstate.Add("IDEdit", id)

    End Sub

    Private Function UploadFile(ByVal FilenameIklan As String, ByVal FilenameManual As String, ByRef objDomain As SampleIklan) As Integer
        Dim SrcFileIklan As String = Path.GetFileName(FilenameIklan)  '-- Source file name
        Dim SrcFileManual As String = Path.GetFileName(FilenameManual)   '-- Source file name
        Dim DestFileIklan As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("IklanBABIT") & "\" & Date.Now.ToString("ddMMyyhhmmss") & "\" & SrcFileIklan     '-- Destination file
        Dim DestFileManual As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("DirectionBABIT") & "\" & Date.Now.ToString("ddMMyyhhmmss") & "\" & SrcFileManual     '-- Destination file

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim finfoIklan As New FileInfo(DestFileIklan)
        Dim finfoManual As New FileInfo(DestFileManual)
        Try
            success = imp.Start()
            If success Then
                If Not FileUploadIklan.PostedFile Is Nothing And FileUploadIklan.Value <> String.Empty Then
                    If Not finfoIklan.Directory.Exists Then
                        Directory.CreateDirectory(finfoIklan.DirectoryName)
                    End If
                    FileUploadIklan.PostedFile.SaveAs(DestFileIklan)
                    objDomain.UploadedIklan = KTB.DNet.Lib.WebConfig.GetValue("IklanBABIT") & "\" & Date.Now.ToString("ddMMyyhhmmss") & "\" & SrcFileIklan
                End If

                If Not fileUploadPenggunaan.PostedFile Is Nothing And fileUploadPenggunaan.Value <> String.Empty Then
                    If Not finfoManual.Directory.Exists Then
                        Directory.CreateDirectory(finfoManual.DirectoryName)
                    End If
                    fileUploadPenggunaan.PostedFile.SaveAs(DestFileManual)
                    objDomain.UplodedDirection = KTB.DNet.Lib.WebConfig.GetValue("DirectionBABIT") & "\" & Date.Now.ToString("ddMMyyhhmmss") & "\" & SrcFileManual
                End If

                imp.StopImpersonate()
                imp = Nothing
                Return 1
            End If
        Catch ex As Exception
            Throw ex
            Return -1
        End Try
    End Function
    Private Function ValidateSave() As Boolean

        If viewstate("ModeUpload") = "New" Then
            If txtNoIklan.Text = String.Empty Then
                MessageBox.Show("Silakan masukan No Iklan.")
                Return False
            Else
                If oSampleIklanFacade.IsExistNoIklan(txtNoIklan.Text) Then
                    MessageBox.Show("No Iklan sudah pernah di buat.")
                    Return False
                End If
            End If

            If FileUploadIklan.PostedFile Is Nothing Or FileUploadIklan.Value = String.Empty Then
                MessageBox.Show("Silakan pilih contoh iklan yg akan di upload.")
                Return False
            End If
        End If

        If Not FileUploadIklan.PostedFile Is Nothing And FileUploadIklan.Value <> String.Empty Then
            Dim FileExt As String = Path.GetExtension(FileUploadIklan.PostedFile.FileName)
            If FileExt.ToLower <> ".jpg" And FileExt.ToLower <> ".bmp" And FileExt.ToLower <> ".gif" And FileExt.ToLower <> ".png" Then
                MessageBox.Show("Format file contoh iklan yang diijinkan: bmp, jpg, gif, png")
                Return False
            End If

            If FileUploadIklan.PostedFile.ContentLength > 5120000 Then
                MessageBox.Show("File contoh iklan tidak boleh melebihi 5120KB.")
                Return False
            End If
        End If

        Return True
    End Function
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.UploadSampleIklanView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Upload Contoh Iklan")
        End If
    End Sub

    Private Function blnCekUploadSampleIklan() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.UploadSampleIklan_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

#Region "Event Handler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()
        If Not IsPostBack Then
            'CheckUserPrivilege()
            InitiatePage()
            dtgSampelIklan.CurrentPageIndex = 0
            'BindToGrid(dtgSampelIklan.CurrentPageIndex)
            If Not blnCekUploadSampleIklan() Then
                FileUploadIklan.Visible = False
                fileUploadPenggunaan.Visible = False
                dtgSampelIklan.Columns(4).Visible = False
            End If
        End If
    End Sub
    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        ClearControl()
    End Sub
    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        dtgSampelIklan.CurrentPageIndex = 0
        BindToGrid(dtgSampelIklan.CurrentPageIndex)
    End Sub
    Private Sub dtgSampelIklan_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgSampelIklan.ItemDataBound
        'Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        'Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        'Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        'Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        'Dim success As Boolean = False

        If e.Item.ItemIndex <> -1 Then
            Dim RowData As SampleIklan = CType(e.Item.DataItem, SampleIklan)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dtgSampelIklan.CurrentPageIndex * dtgSampelIklan.PageSize)

            Dim lbtnDirection As LinkButton = CType(e.Item.FindControl("lbtnDirection"), LinkButton)

            'Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            'Dim objSampelIklan As SampleIklan = New SampleIklanFacade(User).Retrieve(CInt(lblID.Text))

            Dim imgShow As Image = CType(e.Item.FindControl("imgIklan"), Image)
            'success = imp.Start()
            'If success Then
            imgShow.ImageUrl = String.Format("~/Event/EventImageHandler.aspx?file={0}", RowData.UploadedIklan)
            'End If

        If RowData.UplodedDirection <> "" Then
            lbtnDirection.CommandArgument = RowData.UplodedDirection
        Else
            lbtnDirection.Visible = False
        End If
        End If
    End Sub
    Private Sub dtgSampelIklan_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgSampelIklan.PageIndexChanged
        dtgSampelIklan.CurrentPageIndex = e.NewPageIndex
        BindToGrid(dtgSampelIklan.CurrentPageIndex)
    End Sub
    Private Sub dtgSampelIklan_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgSampelIklan.SortCommand
        If e.SortExpression = viewstate("CurrentSortColumn") Then
            If viewstate("CurrentSortDirect") = Sort.SortDirection.ASC Then
                viewstate.Add("CurrentSortDirect", Sort.SortDirection.DESC)
            Else
                viewstate.Add("CurrentSortDirect", Sort.SortDirection.ASC)
            End If
        End If
        viewstate.Add("CurrentSortColumn", e.SortExpression)
        BindToGrid(dtgSampelIklan.CurrentPageIndex)
    End Sub
    Private Sub dtgSampelIklan_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgSampelIklan.ItemCommand
        Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
        Select Case e.CommandName
            Case "LihatPenggunaan"
                Response.Redirect("../Download.aspx?file=" & e.CommandArgument)
            Case "Edit"
                LoadData(CInt(lblID.Text))
            Case "Delete"
                ClearControl()
                oSampleIklan = oSampleIklanFacade.Retrieve(CInt(lblID.Text))
                oSampleIklanFacade.Delete(oSampleIklan)
                BindToGrid(dtgSampelIklan.CurrentPageIndex)
        End Select
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If ValidateSave() Then
            If viewstate("ModeUpload") = "New" Then
                'insert iklan baru
                oSampleIklan = New SampleIklan
                oSampleIklan.NoIklan = txtNoIklan.Text.Trim
                oSampleIklan.Note = txtKeterangan.Text

                Dim uResult As Integer = UploadFile(FileUploadIklan.Value, fileUploadPenggunaan.Value, oSampleIklan)
                If uResult <> -1 Then
                    If oSampleIklanFacade.Insert(oSampleIklan) <> -1 Then
                        MessageBox.Show("Iklan berhasil disimpan")
                    Else
                        MessageBox.Show("Iklan gagal disimpan")
                    End If
                End If
            ElseIf viewstate("ModeUpload") = "Edit" Then
                Dim idEdit As Integer = CInt(viewstate("IDEdit"))
                oSampleIklan = oSampleIklanFacade.Retrieve(idEdit)
                oSampleIklan.Note = txtKeterangan.Text

                Dim uResult As Integer = UploadFile(FileUploadIklan.Value, fileUploadPenggunaan.Value, oSampleIklan)
                If uResult <> -1 Then
                    If oSampleIklanFacade.Update(oSampleIklan) <> -1 Then
                        MessageBox.Show("Iklan berhasil disimpan")
                    Else
                        MessageBox.Show("Iklan gagal disimpan")
                    End If
                End If
            End If

            ClearControl()
            BindToGrid(dtgSampelIklan.CurrentPageIndex)
        End If
    End Sub
#End Region
End Class
