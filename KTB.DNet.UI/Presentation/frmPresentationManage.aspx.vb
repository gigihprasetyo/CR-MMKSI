#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text

#End Region

#Region "Custom Namespace Imports"

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports System.Drawing.Color
Imports KTB.DNet.BusinessFacade


Imports ICSharpCode.SharpZipLib.Core
Imports ICSharpCode.SharpZipLib.Zip

#End Region

Public Class frmPresentationManage
    Inherits System.Web.UI.Page

#Region "var"
    Private ReadOnly varUpload As String = "Presentation\" '  <add key="SAPFolder" value="\\172.17.31.21\ZDNet\Repository\BSI-Net\DNet\SAP\" />
    Private ReadOnly varSession As String = "sessfrmPresentationManage"
    Private sHelper As New SessionHelper
    Dim arlUserGroup As New ArrayList
#End Region

#Region "Private Method"
    Private Sub InitPage()

        If Not Page.IsPostBack Then


            If Not IsNothing(Request.QueryString("id")) AndAlso Request.QueryString("id") <> "" Then
                ViewState.Add("id", Request.QueryString("id"))
                ViewState.Add("backurl", Request.QueryString("backurl"))
                Dim idPresentation As Integer = CInt(Request.QueryString("id"))

                If Request.QueryString("IsSelf") = "0" Then
                    btnBack.Visible = True
                Else
                    btnBack.Visible = False
                End If
                btnTambahGroup.Visible = True
                Me.btnTambahGroup.Attributes.Add("onclick", "showPopUp('../PopUp/PopUpUserGroup.aspx?isPresentation=1" & "&id=" & idPresentation & "','',600,600,GetUserGroupSelection);")

                ViewState("SortCol") = "ID"
                ViewState("SortDirection") = Sort.SortDirection.ASC
                sHelper.SetSession("arlUserGroup", arlUserGroup)
                Dim objPresentation As Presentation
                Dim objPresentationFac As PresentationFacade

                objPresentationFac = New PresentationFacade(User)
                objPresentation = objPresentationFac.Retrieve(CInt(ViewState("id")))

                Me.txtDescription.Text = objPresentation.Description
                Me.txtTitle.Text = objPresentation.Title
                Me.lblFileName.Text = objPresentation.FileName
                If objPresentation.Status Then
                    ddlStatus.SelectedValue = "1"
                Else
                    ddlStatus.SelectedValue = "0"
                End If
                lblCreatedBy.Text = objPresentation.Uploader & "    -   " & objPresentation.CreatedTime.ToString("dd/MM/yyyy")
                lblUpdatedBy.Text = objPresentation.Updater
                trHist.Visible = True
                If lblUpdatedBy.Text <> "" Then

                    lblUpdatedBy.Text = lblUpdatedBy.Text & " -   " & objPresentation.LastUpdateTime.ToString("dd/MM/yyyy")
                    trhistB.Visible = True
                End If
                BindDataGridMember(0)
            Else

                btnTambahGroup.Visible = False
                If Request.QueryString("IsSelf") = "0" Then
                    btnBack.Visible = True
                Else
                    btnBack.Visible = False
                End If
            End If

        Else
            'If Not IsNothing(Request.QueryString("id")) Then
            '    Dim ControlID As String = String.Empty
            '    If (Not String.IsNullOrEmpty(Page.Request.Form("__EVENTTARGET"))) Then
            '        ControlID = Page.Request.Form("__EVENTTARGET")
            '    Else
            '        If Not String.IsNullOrEmpty(Request.Form(CustomHiddenField.UniqueID)) Then
            '            ControlID = Request.Form(CustomHiddenField.UniqueID)
            '        End If
            '    End If
            '    If ControlID <> "btnDeleteUserGroup" Then
            '        BindDataGridMember(0)

            '    End If

            'End If
            BindDataGridMember(0)
            Me.btnUpload.Attributes.Add("onclick", New CommonFunction().PreventDoubleClick(btnUpload))
        End If



    End Sub

    Private Sub ClearControl()
        'Me.txtYearPeriod.Text = String.Empty
        Me.txtTitle.Text = ""
        Me.txtDescription.Text = ""

    End Sub

    Private Function IsInputValid() As String
        Dim sMsg As String = ""
        'If Me.txtYearPeriod.Text.Trim = String.Empty Then
        '    sMsg = sMsg & "Periode (tahun) wajib diisi\n"
        'End If
        If Me.txtTitle.Text.Trim = String.Empty Then
            sMsg = sMsg & "Nama wajib diisi\n"
        End If
        If Me.txtDescription.Text.Trim = String.Empty Then
            sMsg = sMsg & "Deskripsi wajib diisi\n"
        End If

        If ddlStatus.SelectedValue.ToString() = "-1" Then
            sMsg = sMsg & "Status wajib dipilih\n"
        End If


        If IsNothing(ViewState("id")) Then
            If Me.fileUpload.Value = String.Empty Then
                sMsg = sMsg & "File wajib dipilih\n"
            Else
                Dim fileExt As String = Path.GetExtension(fileUpload.PostedFile.FileName)
                If fileExt.ToLower() <> ".zip" Then
                    sMsg = sMsg & "Hanya menerima file format Zip (Ukuran maksimum 10Mb)\n"
                End If

            End If


        End If
        Return sMsg
    End Function

    Private Function ControlToPresentation() As Presentation
        'add by Ery MN
        Dim objUserInfo As UserInfo = CType(sHelper.GetSession("LOGINUSERINFO"), UserInfo)

        '--------------------------
        Dim objPresentation As Presentation
        Dim objPresentationFac As PresentationFacade
        If Not IsNothing(ViewState("id")) Then
            ' update
            objPresentationFac = New PresentationFacade(User)
            objPresentation = objPresentationFac.Retrieve(CInt(ViewState("id")))
            If Not IsNothing(objPresentation) Then

                If (Not Me.fileUpload.PostedFile Is Nothing) And Me.fileUpload.PostedFile.ContentLength > 0 Then
                    If Me.fileUpload.PostedFile.ContentLength > 10240000 Then
                        MessageBox.Show("Hanya menerima file format Zip (Ukuran maksimum 10Mb)")
                        Return Nothing
                    End If
                    Dim ext As String = System.IO.Path.GetExtension(Me.fileUpload.PostedFile.FileName)
                    If Not ext.ToUpper() = ".ZIP" Then
                        MessageBox.Show("Hanya menerima file format Zip (Ukuran maksimum 10Mb)")
                        Return Nothing
                    End If
                    objPresentation.FileName = Path.GetFileName(Me.fileUpload.PostedFile.FileName)
                    objPresentation.UniqueName = Guid.NewGuid().ToString()
                End If
            Else
                MessageBox.Show(SR.DataNotFound("Presentasi"))
                Return Nothing
            End If
        Else
            ' insert
            objPresentation = New Presentation
            objPresentationFac = New PresentationFacade(User)
            objPresentation.FileName = Path.GetFileName(Me.fileUpload.PostedFile.FileName)
            objPresentation.UniqueName = Guid.NewGuid().ToString()
        End If

        objPresentation.Title = Me.txtTitle.Text
        objPresentation.Description = Me.txtDescription.Text
        objPresentation.Status = CBool(CInt(ddlStatus.SelectedValue.ToString()))

        Return objPresentation
    End Function

    Private Sub Proses()
        Dim objPresentation As Presentation
        objPresentation = ControlToPresentation()

        If Not IsNothing(objPresentation) Then
            'Dim fInfo As New FileInfo((Me.fileUpload.PostedFile.FileName))

            'Dim strFileName = fInfo.Name

            'If strFileName.Length > 50 Then
            '    lblMessage.Visible = True
            '    Exit Sub
            'Else
            '    lblMessage.Visible = False
            'End If

            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)



            If Not IsNothing(ViewState("id")) Then
                ' update
                Try
                    If (Not Me.fileUpload.PostedFile Is Nothing) And (Me.fileUpload.PostedFile.ContentLength > 0) Then
                        Try


                            If Me.fileUpload.PostedFile.ContentLength > 10240000 Then
                                MessageBox.Show("Hanya menerima file format Zip (Ukuran maksimum 10Mb)")
                                Return
                            End If
                            Dim ext As String = System.IO.Path.GetExtension(Me.fileUpload.PostedFile.FileName)
                            If Not ext.ToUpper() = ".ZIP" Then
                                MessageBox.Show("Hanya menerima file format Zip (Ukuran maksimum 10Mb)")
                                Return
                            End If

                            If imp.Start() Then
                                Dim NewFileLocation As String = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder") & varUpload ' & objPresentation.UniqueName & ".zip"

                                If Not IO.Directory.Exists(NewFileLocation) Then
                                    IO.Directory.CreateDirectory(Path.GetDirectoryName(NewFileLocation))
                                End If
                                NewFileLocation = NewFileLocation & objPresentation.UniqueName & ".zip"

                                If IO.File.Exists(NewFileLocation) Then
                                    IO.File.Delete(Path.GetDirectoryName(NewFileLocation))
                                End If

                                Dim objUpload As New UploadToWebServer
                                objUpload.Upload(Me.fileUpload.PostedFile.InputStream, NewFileLocation)



                                If IO.File.Exists(NewFileLocation) Then

                                    Dim _pathSlider As String = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder") & varUpload
                                    _pathSlider = _pathSlider & objPresentation.UniqueName

                                    If IO.Directory.Exists(_pathSlider) Then
                                        Directory.Delete(_pathSlider, True)
                                    End If

                                    If Not IO.Directory.Exists(_pathSlider) Then
                                        Directory.CreateDirectory(_pathSlider)
                                    End If

                                    Try
                                        ExtractZipFile(NewFileLocation, "", _pathSlider)
                                    Catch ex As Exception
                                        imp.StopImpersonate()
                                        imp = Nothing
                                        MessageBox.Show(SR.UploadFail("Gagal Extract"))
                                        Return
                                    End Try


                                    Dim CFiles As Integer = New DirectoryInfo(_pathSlider).GetFiles("*.PNG").Length

                                    If CFiles = 0 Then
                                        imp.StopImpersonate()
                                        imp = Nothing
                                        MessageBox.Show(SR.UploadFail("Gambar PNG Tidak Ada"))
                                        Return
                                    End If

                                    '  IO.File.Move(NewFileLocation, _pathSlider & objPresentation.UniqueName & ".zip")
                                End If



                                imp.StopImpersonate()
                                imp = Nothing

                                Dim objPresentationFac As PresentationFacade = New PresentationFacade(User)
                                Dim returnValID As Integer = objPresentationFac.Update(objPresentation)
                                If returnValID = -1 Then
                                    MessageBox.Show(SR.UploadFail(Me.fileUpload.Value.Replace("\", "\\")))
                                Else
                                    returnValID = ViewState("id")

                                    'additional 
                                    If Request.QueryString("isSelf") = "1" Then
                                        Response.Redirect("frmPresentationManage.aspx?id=" & returnValID & "&isSelf=1", False)
                                    Else
                                        Response.Redirect("frmPresentationManage.aspx?id=" & returnValID & "&isSelf=0", False)
                                    End If
                                    MessageBox.Show(SR.SaveSuccess)
                                End If
                            End If
                        Catch ex As Exception
                            MessageBox.Show(SR.UploadFail(Me.fileUpload.Value.Replace("\", "\\")))
                        End Try
                    Else
                        Dim objPresentationFac As PresentationFacade = New PresentationFacade(User)
                        Dim returnValID As Integer = objPresentationFac.Update(objPresentation)
                        If returnValID = -1 Then
                            MessageBox.Show(SR.UpdateFail)
                        Else
                            MessageBox.Show(SR.SaveSuccess)

                           
                        End If

                    End If


                Catch ex As Exception
                    MessageBox.Show(SR.UpdateFail)
                End Try
            Else
                ' insert


                If (Not Me.fileUpload.PostedFile Is Nothing) And (Me.fileUpload.PostedFile.ContentLength > 0) Then
                    Try
                        If Me.fileUpload.PostedFile.ContentLength > 10240000 Then
                            MessageBox.Show("Hanya menerima file format Zip (Ukuran maksimum 10Mb)")
                            Return
                        End If
                        Dim ext As String = System.IO.Path.GetExtension(Me.fileUpload.PostedFile.FileName)
                        If Not ext.ToUpper() = ".ZIP" Then
                            MessageBox.Show("Hanya menerima file format Zip (Ukuran maksimum 10Mb)")
                            Return
                        End If

                        If imp.Start() Then
                            Dim NewFileLocation As String = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder") & varUpload ' & objPresentation.UniqueName & ".zip"

                            If Not IO.Directory.Exists(NewFileLocation) Then
                                IO.Directory.CreateDirectory(Path.GetDirectoryName(NewFileLocation))
                            End If
                            NewFileLocation = NewFileLocation & objPresentation.UniqueName & ".zip"
                            Dim objUpload As New UploadToWebServer
                            objUpload.Upload(Me.fileUpload.PostedFile.InputStream, NewFileLocation)



                            If IO.File.Exists(NewFileLocation) Then

                                Dim _pathSlider As String = KTB.DNet.Lib.WebConfig.GetValue("SAPFolder") & varUpload
                                _pathSlider = _pathSlider & objPresentation.UniqueName
                                If Not IO.Directory.Exists(_pathSlider) Then
                                    Directory.CreateDirectory(_pathSlider)
                                End If

                                Try
                                    ExtractZipFile(NewFileLocation, "", _pathSlider)
                                Catch ex As Exception
                                    imp.StopImpersonate()
                                    imp = Nothing
                                    MessageBox.Show(SR.UploadFail("Gagal Extract"))
                                    Return
                                End Try


                                Dim CFiles As Integer = New DirectoryInfo(_pathSlider).GetFiles("*.PNG").Length

                                If CFiles = 0 Then
                                    imp.StopImpersonate()
                                    imp = Nothing
                                    MessageBox.Show(SR.UploadFail("Gambar PNG Tidak Ada"))
                                    Return
                                End If

                                '  IO.File.Move(NewFileLocation, _pathSlider & objPresentation.UniqueName & ".zip")
                            End If



                            imp.StopImpersonate()
                            imp = Nothing

                            Dim objPresentationFac As PresentationFacade = New PresentationFacade(User)
                            Dim returnValID As Integer = objPresentationFac.Insert(objPresentation)
                            If returnValID = -1 Then
                                MessageBox.Show(SR.UploadFail(Me.fileUpload.Value.Replace("\", "\\")))
                            Else
                                'MessageBox.Show(SR.UploadSucces(Me.fileUpload.Value.Replace("\", "\\")))
                                'additional 
                                MessageBox.Show(SR.UploadSucces(Me.fileUpload.Value.Replace("\", "\\")))
                                Response.Redirect("frmPresentationManage.aspx?id=" & returnValID & "&isSelf=1", False)

                            End If
                        End If
                    Catch ex As Exception
                        MessageBox.Show(SR.UploadFail(Me.fileUpload.Value.Replace("\", "\\")))
                    End Try
                Else
                    MessageBox.Show(SR.FileNotSelected)
                End If
            End If
        Else
            MessageBox.Show(SR.UploadFail(Me.fileUpload.Value.Replace("\", "\\")))
        End If
    End Sub


    Private Sub BindDataGridMember(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) AndAlso Not IsNothing(ViewState("id")) Then

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PresentationGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(PresentationGroup), "Presentation.ID", MatchType.Exact, CInt(ViewState("id"))))
            'New ForumMemberFacade(User).RetrieveByCriteria(criterias, 1, dtgForumMember.PageSize, totalRow, sHelper.GetSession("SortCol"), sHelper.GetSession("SortDirection"))
            arlUserGroup = New PresentationGroupFacade(User).RetrieveActiveList(indexPage, dtgUserGroup.PageSize, totalRow, ViewState("SortCol"), ViewState("SortDirection"), criterias)
            dtgUserGroup.DataSource = arlUserGroup
            dtgUserGroup.VirtualItemCount = totalRow
            dtgUserGroup.DataBind()
            sHelper.SetSession("arlUserGroup", arlUserGroup)
            If arlUserGroup.Count > 0 Then
                dtgUserGroup.Visible = True
                btnDeleteUserGroup.Visible = True
            Else
                dtgUserGroup.Visible = False
                btnDeleteUserGroup.Visible = False
            End If
        End If
    End Sub

    Private Sub DeleteUserGroup()
        Dim arrUserGroup As New ArrayList

        For Each dtg As DataGridItem In Me.dtgUserGroup.Items
            Dim chk As CheckBox = dtg.FindControl("chkSelect")
            If chk.Checked Then
                Dim _orrp As New PresentationGroup(CInt(CType(dtg.FindControl("lblID"), Label).Text))
                arrUserGroup.Add(_orrp)
            End If
        Next



        If arrUserGroup.Count = 0 Then
            MessageBox.Show(SR.DataNotChooseYet("UserGroup"))
        Else

            Dim objLKPPHeaderFacade As New PresentationGroupFacade(User)
            objLKPPHeaderFacade.DeleteFromDB(arrUserGroup)
            MessageBox.Show("data Berhasil di Hapus")
            BindDataGridMember(0)
            dtgUserGroup.CurrentPageIndex = 0


            'RecordStatusChangeHistory(listLKPP, EnumStatusLKPP.Status.Validasi)


        End If
    End Sub

    Public Sub ExtractZipFile(archiveFilenameIn As String, password As String, outFolder As String)
        Dim zf As ZipFile = Nothing
        Try
            Dim fs As FileStream = File.OpenRead(archiveFilenameIn)
            zf = New ZipFile(fs)
            If Not [String].IsNullOrEmpty(password) Then    ' AES encrypted entries are handled automatically
                zf.Password = password
            End If
            For Each zipEntry As ZipEntry In zf
                If Not zipEntry.IsFile Then     ' Ignore directories
                    Continue For
                End If
                Dim entryFileName As [String] = Path.GetFileName(zipEntry.Name)
                ' to remove the folder from the entry:- entryFileName = Path.GetFileName(entryFileName);
                ' Optionally match entrynames against a selection list here to skip as desired.
                ' The unpacked length is available in the zipEntry.Size property.

                Dim buffer As Byte() = New Byte(4095) {}    ' 4K is optimum
                Dim zipStream As Stream = zf.GetInputStream(zipEntry)

                ' Manipulate the output filename here as desired.
                Dim fullZipToPath As [String] = Path.Combine(outFolder, entryFileName)
                Dim directoryName As String = Path.GetDirectoryName(fullZipToPath)
                If directoryName.Length > 0 Then
                    Directory.CreateDirectory(directoryName)
                End If

                ' Unzip file in buffered chunks. This is just as fast as unpacking to a buffer the full size
                ' of the file, but does not waste memory.
                ' The "Using" will close the stream even if an exception occurs.
                Using streamWriter As FileStream = File.Create(fullZipToPath)
                    StreamUtils.Copy(zipStream, streamWriter, buffer)
                End Using
            Next
        Finally
            If zf IsNot Nothing Then
                zf.IsStreamOwner = True     ' Makes close also shut the underlying stream
                ' Ensure we release resources
                zf.Close()
            End If
        End Try
    End Sub

    Private Sub CheckPriv()
        If Not SecurityProvider.Authorize(Context.User, SR.PPT_Input_Edit_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Marketing - Presentation")
        End If
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckPriv()
        InitPage()
    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        If Not Page.IsValid Then
            Exit Sub
        End If

        Dim sMsg As String = ""
        sMsg = IsInputValid()
        If sMsg <> "" Then
            MessageBox.Show(sMsg)
            Return
        End If

        Proses()
    End Sub


    Private Sub dtgUserGroup_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgUserGroup.PageIndexChanged
        dtgUserGroup.CurrentPageIndex = e.NewPageIndex
        BindDataGridMember(e.NewPageIndex + 1)
    End Sub

    Private Sub dtgUserGroup_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgUserGroup.SortCommand
        If e.SortExpression = ViewState("SortCol") Then
            If ViewState("SortDirection") = Sort.SortDirection.ASC Then
                ViewState("SortDirection") = Sort.SortDirection.DESC
            Else
                ViewState("SortDirection") = Sort.SortDirection.ASC
            End If
        End If
        ViewState("SortCol") = e.SortExpression
        dtgUserGroup.SelectedIndex = -1
        'dtgUserGroup.CurrentPageIndex = 0
        BindDataGridMember(dtgUserGroup.CurrentPageIndex)
    End Sub

    Private Sub dtgUserGroup_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgUserGroup.ItemCommand
        If e.CommandName = "Delete" Then
            If ViewState("id") <> String.Empty Then
                Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
                Dim ObjPresentattionGroup As PresentationGroupFacade = New PresentationGroupFacade(User)
                Dim objPresentationGroup As PresentationGroup = New PresentationGroup(CInt(lblID.Text))
                ObjPresentattionGroup.DeleteFromDB(objPresentationGroup)
            End If

            arlUserGroup = CType(sHelper.GetSession("arlUserGroup"), ArrayList)
            arlUserGroup.RemoveAt(e.Item.ItemIndex)

            sHelper.SetSession("arlUserGroup", arlUserGroup)
            BindDataGridMember(0)
            dtgUserGroup.CurrentPageIndex = 0
        End If
    End Sub

    Private Sub dtgUserGroup_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgUserGroup.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNO"), Label)
            Dim lbtnEdit As LinkButton = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            lblNo.Text = CType(e.Item.ItemIndex + 1 + (dtgUserGroup.CurrentPageIndex * dtgUserGroup.PageSize), String)



            Dim _lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            _lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")


        End If
    End Sub

    Protected Sub btnDeleteUserGroup_Click(sender As Object, e As EventArgs) Handles btnDeleteUserGroup.Click
        DeleteUserGroup()
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("frmPresentationList.aspx", False)
    End Sub
End Class