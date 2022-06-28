#Region "Summary"
'---------------------------------------------------'
'-- Program Code : frmPhisingGuardImageList.aspx  --'
'-- Program Name : Phising Guard Image            --'
'-- Description  :                                --'
'---------------------------------------------------'
'-- Programmer   : Agus Pirnadi                   --'
'-- Start Date   : Jan 16 2007                    --'
'-- Update By    :                                --'
'-- Last Update  : Jan 23 2007                    --'
'---------------------------------------------------'
'-- Copyright © 2005 by Intimedia                 --'
'---------------------------------------------------'
#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.Domain.Search
#End Region

Public Class frmPhisingGuardImagaList
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents dtgPhising As System.Web.UI.WebControls.DataGrid
    Protected WithEvents photoSrc As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents photoView As System.Web.UI.WebControls.Image

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Fields"
    Dim sessHelp As SessionHelper = New SessionHelper

    Private oPhisingGuardImage As PhisingGuardImage = New PhisingGuardImage
    Private oPhisingGuardImageFacade As PhisingGuardImageFacade = New PhisingGuardImageFacade(User)
#End Region

#Region "EventHandler"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '-- Page Load event handler

        If Not IsPostBack Then
            CheckPrivilege()
            Initiate()
            dtgPhising.CurrentPageIndex = 0  '-- Page index
            BindDataGrid(dtgPhising.CurrentPageIndex)  '-- Bind datagrid
        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        '-- [Simpan] click event handler

        If Not Page.IsValid Then Return '-- If page not valid then exit

        Select Case CType(ViewState("sGridMode"), String)

            Case "Insert"  '-- Insert mode
                InsertImage()

                dtgPhising.CurrentPageIndex = 0  '-- Re-bind
                BindDataGrid(dtgPhising.CurrentPageIndex)

            Case "Edit"  '-- Edit mode

                oPhisingGuardImage = CType(sessHelp.GetSession("PhisingGuardImage"), PhisingGuardImage)

                '-- Check to see if this record is being referenced
                If Not oPhisingGuardImage.UserProfile Is Nothing Then
                    MessageBox.Show("Tidak bisa di-edit. Image ini sedang dipakai!")
                    Return
                End If

                UpdateImage()

                ViewState.Add("sGridMode", "Insert")  '-- Set to Insert mode
                dtgPhising.SelectedIndex = -1  '-- Set row unselected

                dtgPhising.CurrentPageIndex = 0  '-- Re-bind
                BindDataGrid(dtgPhising.CurrentPageIndex)
        End Select

    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        '-- [Batal] click event handler

        ViewState.Add("sGridMode", "Insert")  '-- Set to Insert mode
        dtgPhising.SelectedIndex = -1  '-- Set row unselected
        photoView.Visible = False  '-- Hide image

    End Sub

    Private Sub dtgPhising_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgPhising.ItemDataBound
        '-- Item data bound event handler

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or _
           e.Item.ItemType = ListItemType.SelectedItem Then

            Dim RowValue As PhisingGuardImage = CType(e.Item.DataItem, PhisingGuardImage)

            '-- Add confirmation to Delete linkbutton
            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Hapus data ini?');")
            End If

            '-- Column number
            CType(e.Item.FindControl("lblNo"), Label).Text = _
                (dtgPhising.CurrentPageIndex * dtgPhising.PageSize) + e.Item.ItemIndex + 1

            '-- Image type
            CType(e.Item.FindControl("lblType"), Label).Text = CType(RowValue.Type, EnumSE.ImageType).ToString()
        End If

    End Sub

    Private Sub dtgPhising_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgPhising.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("sGridMode", "View")
            dtgPhising.SelectedIndex = e.Item.ItemIndex

            photoView.Visible = True  '-- Show image
            photoView.ImageUrl = "../WebResources/frmGetImage.aspx?id=" + e.Item.Cells(0).Text

        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("sGridMode", "Edit")  '-- Set to Edit mode
            dtgPhising.SelectedIndex = e.Item.ItemIndex
            photoView.Visible = False  '-- Hide image

            oPhisingGuardImage = oPhisingGuardImageFacade.Retrieve(CType(e.Item.Cells(0).Text, Int32))
            sessHelp.SetSession("PhisingGuardImage", oPhisingGuardImage)

        ElseIf e.CommandName = "Delete" Then
            '-- Read this record
            oPhisingGuardImage = oPhisingGuardImageFacade.Retrieve(CType(e.Item.Cells(0).Text, Int32))

            '-- Check to see if this record is being referenced
            If Not oPhisingGuardImage.UserProfile Is Nothing Then
                MessageBox.Show("Tidak bisa dihapus. Image ini sedang dipakai!")
                Return
            End If

            oPhisingGuardImage.RowStatus = CType(DBRowStatus.Deleted, Short)  '-- Change status to Deleted

            Try
                oPhisingGuardImageFacade.Update(oPhisingGuardImage)  '-- Delete record

                dtgPhising.CurrentPageIndex = 0  '-- Reset to first page
                BindDataGrid(dtgPhising.CurrentPageIndex)  '-- Re-bind datagrid

                ViewState.Add("sGridMode", "Insert")  '-- Set to Insert mode
                dtgPhising.SelectedIndex = -1  '-- Set row unselected
                photoView.Visible = False  '-- Hide image

                MessageBox.Show(SR.DeleteSucces)

            Catch
                MessageBox.Show(SR.DeleteFail)
            End Try

        End If
    End Sub

    Private Sub dtgPhising_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgPhising.SortCommand
        '-- Sort command event handler

        If CType(ViewState("SortColumn"), String) = e.SortExpression Then

            '-- If column name does not change then reverse its sort direction
            If CType(ViewState("SortDirect"), Sort.SortDirection) = Sort.SortDirection.ASC Then
                ViewState("SortDirect") = Sort.SortDirection.DESC
            Else
                ViewState("SortDirect") = Sort.SortDirection.ASC
            End If

        Else
            '-- New column gets clicked
            ViewState("SortColumn") = e.SortExpression
            ViewState("SortDirect") = Sort.SortDirection.ASC
        End If

        ViewState.Add("sGridMode", "Insert")  '-- Set to Insert mode
        dtgPhising.SelectedIndex = -1  '-- Set row unselected

        dtgPhising.CurrentPageIndex = 0  '-- Set page index
        BindDataGrid(dtgPhising.CurrentPageIndex)
    End Sub

    Private Sub dtgPhising_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgPhising.PageIndexChanged
        '-- Page index changed event handler

        ViewState.Add("sGridMode", "Insert")  '-- Set to Insert mode
        dtgPhising.SelectedIndex = -1  '-- Set row unselected

        dtgPhising.CurrentPageIndex = e.NewPageIndex  '-- Current page index
        BindDataGrid(dtgPhising.CurrentPageIndex)  '-- Bind datagrid
    End Sub

#End Region

#Region "Custom Method"

    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.SEMaintainPhisingGuardImage_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=ADMIN SISTEM - Maintain Gambar Pilihan")
        End If
    End Sub


    Private Sub Initiate()
        '-- Sort by ImageCode ascending
        ViewState("SortColumn") = "ImageCode"
        ViewState("SortDirect") = Sort.SortDirection.ASC

        ViewState.Add("sGridMode", "Insert")  '-- Set to Insert mode
        dtgPhising.SelectedIndex = -1  '-- Set row unselected

        photoView.Visible = False  '-- Hide image
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        '-- Bind datagrid

        Dim criterias As New CriteriaComposite(New Criteria(GetType(PhisingGuardImage), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(PhisingGuardImage), "Type", MatchType.Exact, CType(EnumSE.ImageType.General, Short)))

        Dim totalRow As Integer = 0
        dtgPhising.DataSource = _
            New PhisingGuardImageFacade(User).RetrieveActiveList(criterias, indexPage + 1, dtgPhising.PageSize, totalRow, _
                                                                 CType(ViewState("SortColumn"), String), _
                                                                 CType(ViewState("SortDirect"), Sort.SortDirection))
        dtgPhising.VirtualItemCount = totalRow
        dtgPhising.DataBind()
    End Sub

    Private Sub InsertImage()
        '-- Insert image

        If photoSrc.PostedFile.FileName = String.Empty Then
            MessageBox.Show("Tidak ada file gambar")
            Return  '-- No photo defined
        End If

        '-- Split into array of strings. The last element is the file name
        Dim sFileNames() As String = photoSrc.PostedFile.FileName.Split("\")
        Dim sFileName As String = sFileNames(sFileNames.Length - 1)

        Try
            Dim imageFile As Byte()
            imageFile = UploadFile()

            oPhisingGuardImage.ImageCode = sFileName
            oPhisingGuardImage.Image = imageFile
            oPhisingGuardImage.UploadedUserID = 0  ''CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo).ID
            oPhisingGuardImage.Type = CType(EnumSE.ImageType.General, Short)  '-- Always set with Type "General"

            If oPhisingGuardImageFacade.Insert(oPhisingGuardImage) = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub UpdateImage()
        '-- Update image

        If photoSrc.PostedFile.FileName = String.Empty Then
            MessageBox.Show("Tidak ada file gambar")
            Return  '-- No photo defined
        End If

        '-- Split into array of strings. The last element is the file name
        Dim sFileNames() As String = photoSrc.PostedFile.FileName.Split("\")
        Dim sFileName As String = sFileNames(sFileNames.Length - 1)

        Try
            Dim imageFile As Byte()
            imageFile = UploadFile()

            '-- Read from session
            oPhisingGuardImage = CType(sessHelp.GetSession("PhisingGuardImage"), PhisingGuardImage)

            oPhisingGuardImage.ImageCode = sFileName
            oPhisingGuardImage.Image = imageFile
            oPhisingGuardImage.UploadedUserID = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo).ID
            oPhisingGuardImage.Type = CType(EnumSE.ImageType.General, Short)  '-- Always set with Type "General"

            Try
                oPhisingGuardImageFacade.Update(oPhisingGuardImage)  '-- Update record
                MessageBox.Show(SR.UpdateSucces)
            Catch
                MessageBox.Show(SR.UpdateFail)
            End Try

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Function UploadFile() As Byte()
        Dim nResult() As Byte

        Try
            If IsValidPhoto(photoSrc.PostedFile) Then
                Dim inStream As System.IO.Stream = photoSrc.PostedFile.InputStream()
                Dim ByteRead(PhisingGuardImage.MAX_PHOTO_SIZE) As Byte
                Dim ReadCount As Integer = New System.IO.BinaryReader(inStream).Read(ByteRead, 0, PhisingGuardImage.MAX_PHOTO_SIZE)
                If ReadCount = 0 Then
                    Throw New InvalidConstraintException(SR.DataNotFound("Photo"))
                End If
                ReDim nResult(ReadCount)
                Array.Copy(ByteRead, nResult, ReadCount)
            Else
                Throw New DataException("Bukan file gambar atau Ukuran file > " & _
                                        CType(PhisingGuardImage.MAX_PHOTO_SIZE / 1024, String) & "KB")
            End If
        Catch
            Throw
        End Try

        Return nResult
    End Function

    Private Function IsValidPhoto(ByVal file As HttpPostedFile) As Boolean
        Dim containImage As Boolean = (file.ContentType.ToUpper.IndexOf(PhisingGuardImage.VALID_IMAGE_TYPE) >= 0)
        Dim sizeValid As Boolean = (file.ContentLength <= PhisingGuardImage.MAX_PHOTO_SIZE)
        Return (containImage AndAlso sizeValid)
    End Function

#End Region

End Class
