Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports System.Linq
Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web.UI.WebControls
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.WebCC
Imports System.Collections.Generic

Public Class FrmInputBabitIklan
    Inherits System.Web.UI.Page

    Private objDealer As Dealer
    Private objUserInfo As UserInfo
    Private sessHelper As New SessionHelper
    Private TempDirectory As String
    Dim sessionFiles As String = "BABITFILES"
    Dim sessionIklan As String = "BABITIKLAN"
    Dim sessionAlloc As String = "BABITIKLANAlloc"
    Dim DelSessionFiles As String = "DeletedBABITFILES"
    Dim DelSessionIklan As String = "DeletedBABITIKLAN"
    Dim DelSessionAlloc As String = "DeletedBABITAlloc"
    Dim sessionPH As String = "BABITParamHeader"
    Dim sessionPD As String = "BABITParamDetail"
    Dim sessionBabitHdr As String = "FrmInputBabitIklan.sessionBabitHdr"
    Dim Mode As String = "New"
    Dim oHeader As New BabitHeader
    Private Const strTypeCode As String = "I"
    Private TargetDirectory As String

    Private Property SesDealer() As Dealer
        Get
            Return CType(sessHelper.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)
            sessHelper.SetSession("DEALER", Value)
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        InitializeAuthorization()

        If Not IsNothing(Request.QueryString("Mode")) Then
            Mode = Request.QueryString("Mode")
        End If
        If Mode = "New" Then  ' Login as Dealer
            objDealer = CType(Session("DEALER"), Dealer)
        Else
            objDealer = CType(sessHelper.GetSession("FrmInputBabitIklan.DEALER"), Dealer)

        End If
        objUserInfo = CType(Session("LOGINUSERINFO"), UserInfo)

        TargetDirectory = KTB.DNet.Lib.WebConfig.GetValue("SAN")
        TempDirectory = Server.MapPath("") + "\..\DataTemp\"

        If Not IsPostBack Then
            Page_Init()
            'LoadMarBox()
            FromList()
            BindDgFiles()
            BindDgMedia()
            BindDgAlloc()
        End If
    End Sub

    Protected Sub dgIklan_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgIklan.ItemCommand
        Dim _arrBabitMediaIklan As ArrayList = CType(sessHelper.GetSession(sessionIklan), ArrayList)
        Select Case e.CommandName
            Case "Add"
                'Try
                Dim ddlMedia As DropDownList = CType(e.Item.FindControl("ddlMedia"), DropDownList)
                Dim txtNamaMedia As TextBox = CType(e.Item.FindControl("txtNamaMedia"), TextBox)
                Dim ddlTipeMedia As DropDownList = CType(e.Item.FindControl("ddlTipeMedia"), DropDownList)
                Dim txtUkuran As TextBox = CType(e.Item.FindControl("txtUkuran"), TextBox)
                Dim txtJmlTayang As TextBox = CType(e.Item.FindControl("txtJmlTayang"), TextBox)
                Dim txtNilaiPengajuan As TextBox = CType(e.Item.FindControl("txtNilaiPengajuan"), TextBox)
                Dim ddlCategory As DropDownList = CType(e.Item.FindControl("ddlCategory"), DropDownList)
                Dim icFooterPeriodeIklanStart As IntiCalendar = CType(e.Item.FindControl("icFooterPeriodeIklanStart"), IntiCalendar)
                Dim icFooterPeriodeIklanEnd As IntiCalendar = CType(e.Item.FindControl("icFooterPeriodeIklanEnd"), IntiCalendar)

                Dim babitIklanDetail As New BabitIklanDetail
                Dim babitParameterDetail As New BabitParameterDetail
                Dim babitParameterHeader As New BabitParameterHeader

                If ddlMedia.SelectedValue > -1 Then
                    babitParameterHeader = New BabitParameterHeaderFacade(User).Retrieve(CInt(ddlMedia.SelectedValue))
                    babitIklanDetail.BabitParameterHeader = babitParameterHeader
                Else
                    MessageBox.Show("Pilih Media terlebih dahulu")
                    Exit Sub
                End If

                If ddlTipeMedia.SelectedValue > -1 Then
                    babitParameterDetail = New BabitParameterDetailFacade(User).Retrieve(CInt(ddlTipeMedia.SelectedValue))
                    babitIklanDetail.BabitParameterDetail = babitParameterDetail
                Else
                    MessageBox.Show("Pilih tipe terlebih dahulu")
                    Exit Sub
                End If

                babitIklanDetail.MediaName = txtNamaMedia.Text
                babitIklanDetail.Size = txtUkuran.Text
                If txtJmlTayang.Text = "" Then
                    txtJmlTayang.Text = 0
                End If
                If txtNilaiPengajuan.Text = "" Then
                    txtNilaiPengajuan.Text = 0
                End If
                babitIklanDetail.ViewNumber = txtJmlTayang.Text
                babitIklanDetail.SubmissionAmount = txtNilaiPengajuan.Text

                If ddlCategory.SelectedValue > -1 Then
                    Dim cats As Category = New CategoryFacade(User).Retrieve(CInt(ddlCategory.SelectedValue))
                    babitIklanDetail.Category = cats
                Else
                    MessageBox.Show("Pilih Kategori terlebih dahulu")
                    Exit Sub
                End If
                If icFooterPeriodeIklanStart.Value <= icFooterPeriodeIklanEnd.Value Then
                    babitIklanDetail.PeriodIklanStart = icFooterPeriodeIklanStart.Value
                    babitIklanDetail.PeriodIklanEnd = icFooterPeriodeIklanEnd.Value
                Else
                    MessageBox.Show("Periode Mulai tidak boleh lebih dari Periode Selesai")
                    Exit Sub
                End If

                _arrBabitMediaIklan.Add(babitIklanDetail)
                sessHelper.SetSession(sessionIklan, _arrBabitMediaIklan)
                sessHelper.SetSession(sessionPH, babitParameterHeader)
                sessHelper.SetSession(sessionPD, babitParameterDetail)

                ViewState("AddMedia") = True
                'Catch ex As Exception
                'End Try
            Case "Edit"
                dgIklan.ShowFooter = False
                dgIklan.EditItemIndex = e.Item.ItemIndex
            Case "Delete"
                Try
                    Dim oBabitIklanDetail As BabitIklanDetail = CType(_arrBabitMediaIklan(e.Item.ItemIndex), BabitIklanDetail)
                    If oBabitIklanDetail.ID > 0 Then
                        Dim arrDelete As ArrayList = CType(sessHelper.GetSession(DelSessionIklan), ArrayList)
                        If IsNothing(arrDelete) Then arrDelete = New ArrayList
                        arrDelete.Add(oBabitIklanDetail)
                        sessHelper.SetSession(DelSessionIklan, arrDelete)
                    End If
                    _arrBabitMediaIklan.RemoveAt(e.Item.ItemIndex)
                Catch ex As Exception
                End Try
            Case "Save"
                Dim babitIklanDetail As New BabitIklanDetail
                Dim babitParameterDetail As New BabitParameterDetail
                Dim babitParameterHeader As New BabitParameterHeader

                Dim txtNamaMedia As TextBox = CType(e.Item.FindControl("txtNamaMediaEdit"), TextBox)
                Dim txtUkuran As TextBox = CType(e.Item.FindControl("txtUkuranEdit"), TextBox)
                Dim txtJmlTayang As TextBox = CType(e.Item.FindControl("txtJmlTayangEdit"), TextBox)
                Dim txtNilaiPengajuan As TextBox = CType(e.Item.FindControl("txtNilaiPengajuanEdit"), TextBox)
                Dim ddTipeMedia As DropDownList = CType(e.Item.FindControl("ddlTipeMediaEdit"), DropDownList)
                Dim ddMedia As DropDownList = CType(e.Item.FindControl("ddlMediaEdit"), DropDownList)
                Dim ddCategory As DropDownList = CType(e.Item.FindControl("ddlCategoryEdit"), DropDownList)
                Dim icEditPeriodeIklanStart As IntiCalendar = CType(e.Item.FindControl("icEditPeriodeIklanStart"), IntiCalendar)
                Dim icEditPeriodeIklanEnd As IntiCalendar = CType(e.Item.FindControl("icEditPeriodeIklanEnd"), IntiCalendar)

                If e.Item.Cells(0).Text <> "0" Then
                    babitIklanDetail.ID = CInt(e.Item.Cells(0).Text)
                    If Not IsNothing(CType(_arrBabitMediaIklan(e.Item.ItemIndex), BabitIklanDetail).BabitHeader) Then
                        babitIklanDetail.BabitHeader = CType(_arrBabitMediaIklan(e.Item.ItemIndex), BabitIklanDetail).BabitHeader
                    End If

                End If

                If ddMedia.SelectedValue > -1 Then
                    babitParameterHeader = New BabitParameterHeaderFacade(User).Retrieve(CInt(ddMedia.SelectedValue))
                    babitIklanDetail.BabitParameterHeader = babitParameterHeader
                Else
                    MessageBox.Show("Pilih Media terlebih dahulu")
                    Exit Sub
                End If

                If ddTipeMedia.SelectedValue > -1 Then
                    babitParameterDetail = New BabitParameterDetailFacade(User).Retrieve(CInt(ddTipeMedia.SelectedValue))
                    babitIklanDetail.BabitParameterDetail = babitParameterDetail
                End If

                babitIklanDetail.MediaName = txtNamaMedia.Text
                babitIklanDetail.Size = txtUkuran.Text
                If txtJmlTayang.Text = "" Then
                    txtJmlTayang.Text = 0
                End If
                If txtNilaiPengajuan.Text = "" Then
                    txtNilaiPengajuan.Text = 0
                End If
                babitIklanDetail.ViewNumber = txtJmlTayang.Text
                babitIklanDetail.SubmissionAmount = txtNilaiPengajuan.Text
                babitIklanDetail.Category = New CategoryFacade(User).Retrieve(CInt(ddCategory.SelectedValue))

                If icEditPeriodeIklanStart.Value <= icEditPeriodeIklanEnd.Value Then
                    babitIklanDetail.PeriodIklanStart = icEditPeriodeIklanStart.Value
                    babitIklanDetail.PeriodIklanEnd = icEditPeriodeIklanEnd.Value
                Else
                    MessageBox.Show("Periode Mulai tidak boleh lebih dari Periode Selesai")
                    Exit Sub
                End If

                _arrBabitMediaIklan(e.Item.ItemIndex) = babitIklanDetail
                sessHelper.SetSession(sessionIklan, _arrBabitMediaIklan)
                dgIklan.EditItemIndex = -1
                dgIklan.ShowFooter = True

            Case "cancel" 'Cancel Update this datagrid item 
                dgIklan.EditItemIndex = -1
                dgIklan.ShowFooter = True
        End Select
        BindDgMedia()
        'ViewState("POSTBACK") = False
    End Sub

    Protected Sub dgFiles_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgFiles.ItemCommand
        Dim _arrBabitDocs As ArrayList = CType(sessHelper.GetSession(sessionFiles), ArrayList)
        If e.CommandName = "Add" Then
            Try
                Dim FileUploadIklan As HtmlInputFile = CType(e.Item.FindControl("FileUploadIklan"), HtmlInputFile)
                Dim objPostedData As HttpPostedFile
                Dim sFileName As String
                Dim objBabitDoc As BabitDocument = New BabitDocument


                If IsNothing(FileUploadIklan) OrElse FileUploadIklan.Value = String.Empty Then
                    MessageBox.Show("Lampiran masih kosong")
                    Return
                End If

                If Not IsNothing(FileUploadIklan) OrElse FileUploadIklan.Value <> String.Empty Then
                    objPostedData = FileUploadIklan.PostedFile
                Else
                    objPostedData = Nothing
                End If

                If Not (IsNothing(objPostedData)) Then
                    sFileName = objPostedData.FileName.Split("\")(objPostedData.FileName.Split("\").Length - 1)

                    If KTB.DNet.UI.Helper.FileHelper.IsExecutableFile(sFileName) Then
                        MessageBox.Show("Tidak diperkenankan mengupload file dengan ekstensi '.exe'. Pastikan file anda bebas dari virus.")
                        BindDgFiles()
                        Return
                    End If

                    'cek filesize
                    Dim fileSize As Integer = 5120000
                    If FileUploadIklan.PostedFile.ContentLength > fileSize Then
                        MessageBox.Show("Ukuran file tidak boleh melebihi 5 MB")
                        Return
                    End If
                    If ((Not Path.GetExtension(objPostedData.FileName).ToLower() = ".jpg") And
                        (Not Path.GetExtension(objPostedData.FileName).ToLower() = ".jpeg") And
                        (Not Path.GetExtension(objPostedData.FileName).ToLower() = ".pdf")) Then
                        MessageBox.Show("file harus berekstensi: jpg, jpeg, atau pdf")
                        Return
                    End If

                    'If Not FileIsExist(sFileName, _arrBabitDocs) Then
                    Dim SrcFile As String = Path.GetFileName(objPostedData.FileName) '-- Source file name
                    Dim strBabitPathConfig As String = KTB.DNet.Lib.WebConfig.GetValue("BabitFileDirectory")
                    Dim strBabitPathFile As String = "\BABIT\" & objDealer.DealerCode & "\Iklan\" & TimeStamp() & SrcFile.Substring(SrcFile.Length - 4)
                    Dim strDestFile As String = strBabitPathConfig & strBabitPathFile '--Destination File                       

                    objBabitDoc.BabitHeader = New BabitHeader()
                    objBabitDoc.AttachmentData = objPostedData
                    objBabitDoc.FileName = sFileName
                    objBabitDoc.Path = strDestFile
                    objBabitDoc.FileDescription = "Babit Iklan Document"

                    UploadAttachment(objBabitDoc, TempDirectory)

                    _arrBabitDocs.Add(objBabitDoc)
                    sessHelper.SetSession(sessionFiles, _arrBabitDocs)

                    'Else
                    '    MessageBox.Show(SR.DataIsExist("Attachment File"))
                    'End If
                Else
                    _arrBabitDocs.Add(objBabitDoc)
                    sessHelper.SetSession(sessionFiles, _arrBabitDocs)
                End If

                BindDgFiles()

            Catch ex As Exception
            End Try
        ElseIf e.CommandName = "Delete" Then
            Try
                Dim oDelete As BabitDocument = CType(_arrBabitDocs(e.Item.ItemIndex), BabitDocument)
                If oDelete.ID > 0 Then
                    Dim arrDelete As ArrayList = CType(sessHelper.GetSession(DelSessionFiles), ArrayList)
                    If IsNothing(arrDelete) Then arrDelete = New ArrayList
                    arrDelete.Add(oDelete)
                    sessHelper.SetSession(DelSessionFiles, arrDelete)
                End If

                RemoveBabitAttachment(CType(_arrBabitDocs(e.Item.ItemIndex), BabitDocument), TempDirectory)
                _arrBabitDocs.RemoveAt(e.Item.ItemIndex)
            Catch ex As Exception
            Finally
                BindDgFiles()
            End Try

        ElseIf e.CommandName = "Download" Then 'Download File
            Response.Redirect("../Download.aspx?file=" & e.CommandArgument)

        End If
    End Sub

    Private Sub RemoveBabitAttachment(ByVal ObjAttachment As BabitDocument, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Dim finfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                finfo = New FileInfo(TargetPath + ObjAttachment.Path)
                If finfo.Exists Then
                    finfo.Delete()
                End If

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UploadAttachment(ByVal ObjAttachment As BabitDocument, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Dim finfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                If Not IsNothing(ObjAttachment.Path) Then
                    finfo = New FileInfo(TargetPath + ObjAttachment.Path)

                    If Not finfo.Directory.Exists Then
                        Directory.CreateDirectory(finfo.DirectoryName)
                    End If
                    ObjAttachment.AttachmentData.SaveAs(TargetPath + ObjAttachment.Path)
                End If

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CommitAttachment(ByVal ArrBabitDoc As ArrayList)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False
        Dim finfo As FileInfo
        Dim TempFInfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                For Each obj As BabitDocument In ArrBabitDoc
                    If Not IsNothing(obj.AttachmentData) Then
                        finfo = New FileInfo(TargetDirectory + obj.Path)
                        TempFInfo = New FileInfo(TempDirectory + obj.Path)

                        If TempFInfo.Exists Then
                            If Not finfo.Directory.Exists Then
                                Directory.CreateDirectory(finfo.DirectoryName)
                            End If
                            TempFInfo.MoveTo(finfo.FullName)
                        End If
                        obj.AttachmentData.SaveAs(TargetDirectory + obj.Path)
                    End If
                Next

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function FileIsExist(ByVal FileName As String, ByVal AttachmentCollection As ArrayList) As Boolean
        Dim bResult As Boolean = False
        If AttachmentCollection.Count > 0 Then
            For Each objWSCEvidence As BabitDocument In AttachmentCollection
                If Not IsNothing(objWSCEvidence.AttachmentData) Then
                    If Path.GetFileName(objWSCEvidence.AttachmentData.FileName) = FileName Then
                        bResult = True
                        Exit For
                    End If
                End If
            Next
        End If
        Return bResult
    End Function

    Private Sub BindDgMedia()
        dgIklan.DataSource = CType(sessHelper.GetSession(sessionIklan), ArrayList)
        dgIklan.DataBind()
    End Sub

    Private Sub BindDgAlloc()
        dgAlloc.DataSource = CType(sessHelper.GetSession(sessionAlloc), ArrayList)
        dgAlloc.DataBind()
    End Sub

    Private Sub BindDgFiles()
        dgFiles.DataSource = CType(sessHelper.GetSession(sessionFiles), ArrayList)
        dgFiles.DataBind()
    End Sub

    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function

    Private Sub Page_Init()
        If Not IsNothing(Request.QueryString("Mode")) Then
            Mode = Request.QueryString("Mode")
        End If
        If SesDealer().Title = EnumDealerTittle.DealerTittle.KTB Then
            sessHelper.SetSession(sessionAlloc, New ArrayList)
            allocBlock.Visible = True
            txtNotes.Enabled = True
        Else
            allocBlock.Visible = False
            txtNotes.Enabled = False
            If Mode = "New" Then
                TR_CatatanMKS.Visible = False
            Else
                TR_CatatanMKS.Visible = True
            End If
        End If

        If Not IsNothing(Request.QueryString("BabitHeaderID")) Then
            oHeader = New BabitHeaderFacade(User).Retrieve(CInt(Request.QueryString("BabitHeaderID")))
            objDealer = oHeader.Dealer
        End If
        If SesDealer().Title <> EnumDealerTittle.DealerTittle.KTB Then
            lblDealerCode.Text = objDealer.DealerCode
            lblDealerName.Text = " / " & objDealer.DealerName
            LoadArea()
        End If
        lblPopUpTO.Attributes("onclick") = "ShowPopUpTO()"


        If Mode = "New" Then
            sessHelper.SetSession(sessionFiles, New ArrayList)
            sessHelper.SetSession(sessionIklan, New ArrayList)
            sessHelper.SetSession(DelSessionFiles, New ArrayList)
            sessHelper.SetSession(DelSessionIklan, New ArrayList)
        End If
        BindMonth()
        BindYear()
        'ViewState("POSTBACK") = True
    End Sub

    Protected Sub txtTOChanges(source As Object, e As EventArgs) Handles hdnTemporaryOutlet.ValueChanged
        Dim data As String() = hdnTemporaryOutlet.Value.Trim.Split(";")
        Dim DBF As DealerBranch = New DealerBranchFacade(User).Retrieve(data(0))

        Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Area2), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crits.opAnd(New Criteria(GetType(Area2), "ID", MatchType.Exact, DBF.Area2.ID))

        Dim oArea2 As ArrayList = New Area2Facade(User).Retrieve(crits)

        If oArea2.Count > 0 AndAlso data.Length >= 1 Then
            txtTemporaryOutlet.Text = data(0)
            lblArea.Text = CType(oArea2(0), Area2).Description
        End If

        lblNamaCabang.Text = DBF.Name
    End Sub

    Private Function TimeStamp() As String
        Return DateTime.Now.Year & DateTime.Now.Month & DateTime.Now.Day & DateTime.Now.Hour & DateTime.Now.Minute & DateTime.Now.Second & DateTime.Now.Millisecond
    End Function

    Protected Sub dgFiles_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgFiles.ItemDataBound
        Dim arrAttachment As ArrayList = CType(sessHelper.GetSession(sessionFiles), ArrayList)
        Try
            If arrAttachment.Count > 0 AndAlso e.Item.ItemIndex > -1 Then
                Dim objBabitDocument As BabitDocument = arrAttachment(e.Item.ItemIndex)

                Dim lblFileIklan As Label = CType(e.Item.FindControl("lblFileIklan"), Label)
                If Mode = "New" Then
                    lblFileIklan.Text = Path.GetFileName(objBabitDocument.AttachmentData.FileName)
                Else
                    lblFileIklan.Text = Path.GetFileName(objBabitDocument.FileName)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BindDDLMedia(ByVal ddlMedia As DropDownList)
        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitParameterHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(BabitParameterHeader), "BabitMasterEventType.TypeCode", MatchType.Exact, strTypeCode))
        criteria.opAnd(New Criteria(GetType(BabitParameterHeader), "ParameterCategory", MatchType.Exact, 4))
        criteria.opAnd(New Criteria(GetType(BabitParameterHeader), "Status", MatchType.Exact, 1))
        Dim _arrMedia As ArrayList = New BabitParameterHeaderFacade(User).Retrieve(criteria)

        With ddlMedia.Items
            .Clear()
            .Add(New ListItem("Silahkan Pilih", "-1"))
            For Each oPW As BabitParameterHeader In _arrMedia
                .Add(New ListItem(oPW.ParameterName, oPW.ID))
            Next
        End With
    End Sub

    Protected Sub dgIklan_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgIklan.ItemDataBound
        'On Error GoTo CheckError

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim _arrBabitMediaIklan As ArrayList = CType(sessHelper.GetSession(sessionIklan), ArrayList)

            Dim objBabitIklanDetail As BabitIklanDetail = _arrBabitMediaIklan(e.Item.ItemIndex)

            Dim lblMedia As Label = CType(e.Item.FindControl("lblMedia"), Label)
            Dim lblNamaMedia As Label = CType(e.Item.FindControl("lblNamaMedia"), Label)
            Dim lblTipe As Label = CType(e.Item.FindControl("lblTipe"), Label)
            Dim lblUkuran As Label = CType(e.Item.FindControl("lblUkuran"), Label)
            Dim lblJmlTayang As Label = CType(e.Item.FindControl("lblJmlTayang"), Label)
            Dim lblNilaiPengajuan As Label = CType(e.Item.FindControl("lblNilaiPengajuan"), Label)
            Dim lblCategory As Label = CType(e.Item.FindControl("lblCategory"), Label)
            Dim lblPeriodIklanStart As Label = CType(e.Item.FindControl("lblPeriodIklanStart"), Label)
            Dim lblPeriodIklanEnd As Label = CType(e.Item.FindControl("lblPeriodIklanEnd"), Label)


            Dim ParameterDetailName As String = ""
            If Not IsNothing(objBabitIklanDetail.BabitParameterDetail) Then
                ParameterDetailName = objBabitIklanDetail.BabitParameterDetail.ParameterDetailName
            End If
            lblTipe.Text = ParameterDetailName

            Dim ParameterHeaderName As String = ""
            If Not IsNothing(objBabitIklanDetail.BabitParameterHeader) Then
                ParameterHeaderName = objBabitIklanDetail.BabitParameterHeader.ParameterName
            End If
            lblMedia.Text = ParameterHeaderName


            lblNamaMedia.Text = objBabitIklanDetail.MediaName
            lblUkuran.Text = objBabitIklanDetail.Size
            lblJmlTayang.Text = objBabitIklanDetail.ViewNumber
            lblNilaiPengajuan.Text = Format(CInt(objBabitIklanDetail.SubmissionAmount), "###,###")
            lblCategory.Text = objBabitIklanDetail.Category.CategoryCode
            lblPeriodIklanStart.Text = objBabitIklanDetail.PeriodIklanStart
            lblPeriodIklanEnd.Text = objBabitIklanDetail.PeriodIklanEnd

        ElseIf e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            Dim objBabitIklanDetail As BabitIklanDetail = CType(e.Item.DataItem, BabitIklanDetail)

            Dim txtNamaMedia As TextBox = CType(e.Item.FindControl("txtNamaMediaEdit"), TextBox)
            Dim txtUkuran As TextBox = CType(e.Item.FindControl("txtUkuranEdit"), TextBox)
            Dim txtJmlTayang As TextBox = CType(e.Item.FindControl("txtJmlTayangEdit"), TextBox)
            Dim txtNilaiPengajuan As TextBox = CType(e.Item.FindControl("txtNilaiPengajuanEdit"), TextBox)
            Dim ddTipeMedia As DropDownList = CType(e.Item.FindControl("ddlTipeMediaEdit"), DropDownList)
            Dim ddMedia As DropDownList = CType(e.Item.FindControl("ddlMediaEdit"), DropDownList)
            Dim ddlCategory As DropDownList = CType(e.Item.FindControl("ddlCategoryEdit"), DropDownList)
            Dim icEditPeriodeIklanStart As IntiCalendar = CType(e.Item.FindControl("icEditPeriodeIklanStart"), IntiCalendar)
            Dim icEditPeriodeIklanEnd As IntiCalendar = CType(e.Item.FindControl("icEditPeriodeIklanEnd"), IntiCalendar)

            BindDDLCategory(ddlCategory)
            txtNamaMedia.Text = objBabitIklanDetail.MediaName
            txtUkuran.Text = objBabitIklanDetail.Size
            txtJmlTayang.Text = objBabitIklanDetail.ViewNumber
            txtNilaiPengajuan.Text = Format(CInt(objBabitIklanDetail.SubmissionAmount), "###,###")
            If txtNilaiPengajuan.Text.Trim = "" Then txtNilaiPengajuan.Text = 0

            ddlCategory.SelectedValue = objBabitIklanDetail.Category.ID
            icEditPeriodeIklanStart.Value = objBabitIklanDetail.PeriodIklanStart
            icEditPeriodeIklanEnd.Value = objBabitIklanDetail.PeriodIklanEnd

            Dim arrDDL As ArrayList = New ArrayList
            Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitParameterHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "BabitMasterEventType.TypeCode", MatchType.Exact, strTypeCode))
            criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "ParameterCategory", MatchType.Exact, 4))
            criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "Status", MatchType.Exact, 1))
            arrDDL = New BabitParameterHeaderFacade(User).Retrieve(criterias)
            With ddMedia
                .Items.Clear()
                .DataSource = arrDDL
                .DataTextField = "ParameterName"
                .DataValueField = "ID"
                .DataBind()
                .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
                .SelectedValue = objBabitIklanDetail.BabitParameterHeader.ID
            End With

            If Not IsNothing(objBabitIklanDetail.BabitParameterDetail) Then
                arrDDL = New ArrayList
                Dim criterias2 As New CriteriaComposite(New Criteria(GetType(BabitParameterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias2.opAnd(New Criteria(GetType(BabitParameterDetail), "BabitParameterHeader.ID", MatchType.Exact, objBabitIklanDetail.BabitParameterHeader.ID))
                arrDDL = New BabitParameterDetailFacade(User).Retrieve(criterias2)
                With ddTipeMedia
                    .Items.Clear()
                    .DataSource = arrDDL
                    .DataTextField = "ParameterDetailName"
                    .DataValueField = "ID"
                    .DataBind()
                    .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
                    .SelectedValue = objBabitIklanDetail.BabitParameterDetail.ID
                End With
            End If

        ElseIf e.Item.ItemType = ListItemType.Footer Then
            If Not IsNothing(ViewState("AddMedia")) Then
                Dim ddlMedia As DropDownList = CType(e.Item.FindControl("ddlMedia"), DropDownList)
                Dim txtNamaMedia As TextBox = CType(e.Item.FindControl("txtNamaMedia"), TextBox)
                Dim ddlTipeMedia As DropDownList = CType(e.Item.FindControl("ddlTipeMedia"), DropDownList)
                Dim txtUkuran As TextBox = CType(e.Item.FindControl("txtUkuran"), TextBox)
                Dim txtJmlTayang As TextBox = CType(e.Item.FindControl("txtJmlTayang"), TextBox)
                Dim txtNilaiPengajuan As TextBox = CType(e.Item.FindControl("txtNilaiPengajuan"), TextBox)
                Dim ddlCategory As DropDownList = CType(e.Item.FindControl("ddlCategory"), DropDownList)
                Dim icFooterPeriodeIklanStart As IntiCalendar = CType(e.Item.FindControl("icFooterPeriodeIklanStart"), IntiCalendar)
                Dim icFooterPeriodeIklanEnd As IntiCalendar = CType(e.Item.FindControl("icFooterPeriodeIklanEnd"), IntiCalendar)

                ddlMedia.SelectedIndex = 0
                ddlTipeMedia.SelectedIndex = 0
                ddlCategory.SelectedIndex = 0
                txtNamaMedia.Text = ""
                txtUkuran.Text = ""
                txtJmlTayang.Text = ""
                txtNilaiPengajuan.Text = 0
                icFooterPeriodeIklanStart.Value = Date.Now
                icFooterPeriodeIklanEnd.Value = Date.Now

                BindDDLMedia(ddlMedia)
                BindDDLCategory(ddlCategory)
                ViewState("AddMedia") = Nothing
            Else
                Dim ddlMedia As DropDownList = CType(e.Item.FindControl("ddlMedia"), DropDownList)
                Dim ddlCategory As DropDownList = CType(e.Item.FindControl("ddlCategory"), DropDownList)
                Dim ddlTipeMedia As DropDownList = CType(e.Item.FindControl("ddlTipeMedia"), DropDownList)
                BindDDLCategory(ddlCategory)
                BindDDLMedia(ddlMedia)
            End If
        End If
    End Sub

    Private Sub BindDDLTipe(ddlTipe As DropDownList, HeaderID As String)
        Dim criteria2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitParameterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria2.opAnd(New Criteria(GetType(BabitParameterDetail), "BabitParameterHeader.BabitMasterEventType.TypeCode", MatchType.Exact, strTypeCode))
        criteria2.opAnd(New Criteria(GetType(BabitParameterHeader), "ParameterCategory", MatchType.Exact, 4))
        criteria2.opAnd(New Criteria(GetType(BabitParameterDetail), "BabitParameterHeader.ID", MatchType.Exact, HeaderID))
        Dim _arrTipe As ArrayList = New BabitParameterDetailFacade(User).Retrieve(criteria2)

        With ddlTipe.Items
            .Clear()
            .Add(New ListItem("Silahkan Pilih", "-1"))
            For Each oPW As BabitParameterDetail In _arrTipe
                .Add(New ListItem(oPW.ParameterDetailName, oPW.ID))
            Next
        End With
    End Sub

    Private Sub BindDDLCategory(ddlCategory As DropDownList)
        Dim criteria2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria2.opAnd(New Criteria(GetType(Category), "ProductCategory.Code", MatchType.Exact, "MMC"))
        Dim _arrTipe As ArrayList = New CategoryFacade(User).Retrieve(criteria2)

        With ddlCategory.Items
            .Clear()
            .Add(New ListItem("Silahkan Pilih", "-1"))
            For Each oPW As Category In _arrTipe
                .Add(New ListItem(oPW.CategoryCode, oPW.ID))
            Next
        End With
    End Sub

    Protected Sub ddlMediaEdit_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim selectedValue As String = CType(sender, DropDownList).SelectedValue
        Dim ddlTipeEdit As DropDownList = LoopDGrid("ddlTipeMediaEdit", "DropDownList")
        BindDDLTipe(ddlTipeEdit, selectedValue)
    End Sub

    Protected Sub ddlMedia_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim ddlTipe As DropDownList = LoopDGrid("ddlTipeMedia", "DropDownList")
        Dim selectedValue As String = CType(sender, DropDownList).SelectedValue
        BindDDLTipe(ddlTipe, selectedValue)
    End Sub

    Private Function LoopDGrid(ControlID As String, ControlType As String)
        For Each e1 As Control In dgIklan.Controls
            For Each ct As Control In e1.Controls
                If TypeOf ct Is System.Web.UI.WebControls.DataGridItem Then
                    Dim di As System.Web.UI.WebControls.DataGridItem = CType(ct, System.Web.UI.WebControls.DataGridItem)
                    If di.ItemType = ListItemType.Item OrElse di.ItemType = ListItemType.AlternatingItem OrElse di.ItemType = ListItemType.Footer OrElse di.ItemType = ListItemType.EditItem OrElse di.ItemType = ListItemType.SelectedItem Then
                        If Not IsNothing(di.FindControl(ControlID)) Then
                            Select Case ControlType
                                Case "DropDownList"
                                    Return CType(di.FindControl(ControlID), DropDownList)
                                Case "TextBox"
                                    Return CType(di.FindControl(ControlID), TextBox)
                                Case "CheckBox"
                                    Return CType(di.FindControl(ControlID), CheckBox)
                                Case "Label"
                                    Return CType(di.FindControl(ControlID), Label)
                            End Select
                        End If
                    End If
                End If
            Next
        Next
    End Function

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Dim str As String
        str = Validasi()
        If (str.Length > 0) Then
            MessageBox.Show(str)
            Exit Sub
        End If

        Dim _arrBabitMediaIklan As ArrayList = CType(sessHelper.GetSession(sessionIklan), ArrayList)
        Dim _arrBabitDocs As ArrayList = CType(sessHelper.GetSession(sessionFiles), ArrayList)
        Dim _arrBabitDealerAlloc As ArrayList = CType(sessHelper.GetSession(sessionAlloc), ArrayList)

        If Not IsNothing(_arrBabitDealerAlloc) Then
            For Each objAlloc As BabitDealerAllocation In _arrBabitDealerAlloc
                objAlloc.Dealer = objDealer
            Next
        End If

        Dim _babitHeader As New BabitHeader
        If Mode = "Edit" Then
            _babitHeader = New BabitHeaderFacade(User).Retrieve(CInt(Request.QueryString("BabitHeaderID")))
        ElseIf Mode = "New" Then
            _babitHeader.BabitRegNumber = getRegNumber()
        End If
        _babitHeader.Dealer = objDealer
        _babitHeader.DealerBranch = New DealerBranchFacade(User).Retrieve(txtTemporaryOutlet.Text)
        '_babitHeader.BabitType = "I"
        _babitHeader.BabitMasterEventType = New BabitMasterEventTypeFacade(User).Retrieve(strTypeCode)
        _babitHeader.BabitDealerNumber = txtNomorSurat.Text
        '_babitHeader.AllocationType = ddlTipeAlokasi.SelectedValue
        _babitHeader.PeriodStart = CDate(1 & "/" & ddlMonth.SelectedValue & "/" & ddlYear.SelectedValue).ToString("yyyy/MM/dd 00:00:00")
        _babitHeader.PeriodEnd = CDate(Date.DaysInMonth(ddlYear.SelectedValue, ddlMonth.SelectedValue) & "/" & ddlMonth.SelectedValue & "/" & ddlYear.SelectedValue).ToString("yyyy/MM/dd 00:00:00")
        _babitHeader.MarboxID = IIf(ddlMarBox.SelectedIndex = 0, "NULL", ddlMarBox.SelectedValue)
        _babitHeader.Notes = txtNotes.Text

        Dim _result As Integer = 0
        Dim _arrDelBabitMediaIklan As ArrayList
        Dim _arrDelBabitDocs As New ArrayList
        Dim _arrDelBabitDealerAlloc As New ArrayList
        If Not IsNothing(Request.QueryString("Mode")) Then
            If Request.QueryString("Mode") = "Edit" Then
                _arrDelBabitMediaIklan = CType(sessHelper.GetSession(DelSessionIklan), ArrayList)
                _arrDelBabitDocs = CType(sessHelper.GetSession(DelSessionFiles), ArrayList)
                _arrDelBabitDealerAlloc = CType(sessHelper.GetSession(DelSessionAlloc), ArrayList)

                _result = New BabitIklanDetailFacade(User).UpdateTransaction(_babitHeader, _arrBabitMediaIklan, _arrDelBabitMediaIklan, _arrBabitDocs, _arrDelBabitDocs, _arrBabitDealerAlloc, _arrDelBabitDealerAlloc)
            End If
        Else
            '_babitHeader.ID = New BabitHeaderFacade(User).Insert(_babitHeader)
            _result = New BabitIklanDetailFacade(User).InsertTransaction(_babitHeader, _arrBabitMediaIklan, _arrBabitDocs, _arrBabitDealerAlloc)
        End If

        Dim strJs As String = String.Empty
        If _result > 0 Then
            Dim arrBabitDoc As ArrayList = sessHelper.GetSession(sessionFiles)
            CommitAttachment(arrBabitDoc)
            If Request.QueryString("Mode") = "Edit" Then
                If Not IsNothing(_arrDelBabitDocs) Then
                    RemoveBabitDocumentAttachment(_arrDelBabitDocs, TargetDirectory)
                End If
            End If
            ClearTempData()
            ClearAll()

            'MessageBox.Show("Simpan Berhasil")
            'If Mode = "Edit" Then
            '    Response.Redirect("FrmBabitList.aspx?Done=OK")
            'End If

            strJs = "alert('Simpan Data Berhasil');"
            strJs += "window.location = '../Babit/FrmBabitList.aspx?Back=OK';"
        Else
            strJs = "alert('Simpan Data Gagal');"
        End If
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Script", strJs, True)

    End Sub

    Private Sub RemoveBabitDocumentAttachment(ByVal AttachmentCollection As ArrayList, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim finfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                For Each obj As BabitDocument In AttachmentCollection
                    finfo = New FileInfo(TargetPath + obj.Path)
                    If finfo.Exists Then
                        finfo.Delete()
                    End If
                Next

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub ClearTempData()
        'Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        'Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        'Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        'Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        'Dim success As Boolean = False

        'Try
        '    success = imp.Start()
        '    If success Then
        '        Dim dir As New DirectoryInfo(TempDirectory)
        '        dir.Delete(True)
        '    End If
        'Catch ex As Exception
        '    'Throw ex
        'End Try
    End Sub

    Private Function getRegNumber() As String
        'Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim YearParameter As Integer = Date.Today.Year
        If Date.Today.Month = 12 Then
            YearParameter = Date.Today.Year + 1
        End If
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitHeader), "BabitRegNumber", MatchType.StartsWith, "I"))
        crit.opAnd(New Criteria(GetType(BabitHeader), "CreatedTime", MatchType.GreaterOrEqual, Date.Today.Year & Date.Today.Month.ToString("d2") & "01"))
        crit.opAnd(New Criteria(GetType(BabitHeader), "CreatedTime", MatchType.Lesser, YearParameter & Date.Today.AddMonths(1).Month.ToString("d2") & "01"))
        Dim arrl As ArrayList = New BabitHeaderFacade(User).Retrieve(crit)
        Dim _return As String
        If arrl.Count > 0 Then
            'Dim objBH As BabitHeader = CommonFunction.SortListControl(arrl, "ID", Sort.SortDirection.DESC)(0)
            Dim objBH As BabitHeader = CommonFunction.SortListControl(arrl, "BabitRegNumber", Sort.SortDirection.DESC)(0)
            Dim noReg As String = objBH.BabitRegNumber
            _return = "I" & Date.Today.Month.ToString("d2") & Date.Today.ToString("yy") & (CInt(noReg.Substring(5, 5)) + 1).ToString("d5")
        Else
            _return = "I" & Date.Today.Month.ToString("d2") & Date.Today.ToString("yy") & CInt(1).ToString("d5")
        End If
        Return _return
    End Function

    Private Sub InitializeAuthorization()
        'BabitIklanDetail_Input_Privilege

        If Not SecurityProvider.Authorize(Context.User, SR.Babit_Input_Iklan_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=BABIT - INPUT BABIT IKLAN")
        End If
    End Sub

    Private Sub ClearAll()
        txtTemporaryOutlet.Text = ""
        hdnTemporaryOutlet.Value = ""
        lblNamaCabang.Text = ""
        lblArea.Text = ""
        txtNomorSurat.Text = ""
        'ddlTipeAlokasi.SelectedIndex = 0
        'icDatePeriodeStart.Value = Date.Now
        'icDatePeriodeAkhir.Value = Date.Now
        BindMonth()
        BindYear()
        sessHelper.SetSession(sessionIklan, New ArrayList)
        sessHelper.SetSession(sessionFiles, New ArrayList)
        sessHelper.RemoveSession(sessionPH)
        sessHelper.RemoveSession(sessionPD)
        BindDgMedia()
        BindDgFiles()
    End Sub

    Private Sub BindMonth()
        '
        Dim al As ArrayList = enumMonthGet.RetriveMonth()
        ddlMonth.DataSource = enumMonthGet.RetriveMonth()
        ddlMonth.DataValueField = "ValStatus"
        ddlMonth.DataTextField = "NameStatus"
        ddlMonth.DataBind()

        'ddlMonth.SelectedValue = DateTime.Now.Month - 1
        ddlMonth.SelectedValue = DateTime.Now.Month
    End Sub

    Private Sub BindYear()

        Dim a As Integer
        Dim now As DateTime = DateTime.Now
        For a = -1 To 1
            ddlYear.Items.Insert(0, New ListItem(now.AddYears(a).Year, now.AddYears(a).Year))
        Next
        ddlYear.SelectedValue = now.Year
    End Sub

    Private Sub LoadArea()
        If Mode = "New" Then
            Dim objDealer As Dealer = New KTB.DNet.BusinessFacade.General.DealerFacade(User).Retrieve(lblDealerCode.Text.Trim.Split("/")(0))
            If Not IsNothing(objDealer.Area2) Then
                lblArea.Text = objDealer.Area2.AreaCode & " / " & objDealer.Area2.Description
            End If
        Else
            If Not IsNothing(objDealer.Area2) Then
                lblArea.Text = objDealer.Area2.AreaCode & " / " & objDealer.Area2.Description
            End If
        End If
    End Sub

    Private Function Validasi() As String
        Dim sb As StringBuilder = New StringBuilder

        'If ddlTipeAlokasi.SelectedIndex = 0 Then
        '    sb.Append("- Tipe alokasi Harus diisi! \n")
        'End If

        If txtNomorSurat.Text.Trim = String.Empty Then
            sb.Append("- Nomor Surat harus diisi! \n")
        End If

        Dim strParamName As String = String.Empty
        If ValidateIsMandatoryParamBabitEvent(strParamName) <> String.Empty Then
            sb.Append("- Media: " & strParamName & " belum diinputkan! \n")
        End If

        'If ddlMarBox.SelectedIndex = 0 Then
        '    sb.Append("- Marbox harus dipilih! \n")
        'End If

        If dgIklan.Items.Count < 1 Then
            sb.Append("- Daftar Media belum diinputkan! \n")
        End If

        If dgFiles.Items.Count < 1 Then
            sb.Append("- Lampiran belum diinputkan! \n")
        End If

        If Not IsLoginAsDealer() Then
            Dim intAllocBabit As Integer = 0
            Dim arrAlloc As ArrayList = CType(sessHelper.GetSession(sessionAlloc), ArrayList)
            If Not IsNothing(arrAlloc) Then
                If arrAlloc.Count = 0 Then
                    sb.Append("- Alokasi Babit harus Diisi.\n")
                Else
                    For Each obj As BabitDealerAllocation In arrAlloc
                        intAllocBabit = 0
                        For Each item As DataGridItem In dgAlloc.Items
                            Dim lblAllocationBabit As Label = CType(item.FindControl("lblAllocationBabit"), Label)
                            If lblAllocationBabit.Text = obj.BabitCategory Then
                                intAllocBabit += 1
                                If intAllocBabit >= 2 Then
                                    sb.Append("- Tipe Alokasi tidak boleh sama dalam satu pengajuan Babit \n")
                                    Exit For
                                End If
                            End If
                        Next
                        If intAllocBabit >= 2 Then
                            Exit For
                        End If
                    Next

                    Dim dblSumSubsidyAmountBeforeEdit As Double = 0
                    Dim dblMaxJumlahSubsidy As Double = 0
                    Dim dblSumJumlahSubsidy As Double = 0
                    Dim strDealerCode As String = ""
                    Dim arrAlloc2 As ArrayList = New ArrayList
                    arrAlloc2 = New System.Collections.ArrayList((From item As BabitDealerAllocation In arrAlloc.OfType(Of BabitDealerAllocation)()
                                Order By item.Dealer.DealerCode Ascending
                                Select item).ToList())
                    If arrAlloc2.Count > 0 Then
                        dblSumSubsidyAmountBeforeEdit = 0
                        dblMaxJumlahSubsidy = 0
                        dblSumJumlahSubsidy = 0
                        strDealerCode = CType(arrAlloc2(0), BabitDealerAllocation).Dealer.DealerCode
                        For i As Integer = 0 To arrAlloc2.Count - 1
                            Dim objAlloc As BabitDealerAllocation = CType(arrAlloc2(i), BabitDealerAllocation)
                            If objAlloc.Dealer.DealerCode <> strDealerCode Then
                                dblMaxJumlahSubsidy += dblSumSubsidyAmountBeforeEdit
                                If objAlloc.BabitCategory <> "SPESIAL" AndAlso dblSumJumlahSubsidy > dblMaxJumlahSubsidy Then
                                    sb.Append("- Jumlah Subsidi untuk Kode Dealer " & strDealerCode & " sudah melebihi maksimal subsidinya.\n")
                                End If
                                dblMaxJumlahSubsidy = objAlloc.MaxSubsidyAmount
                                dblSumJumlahSubsidy = objAlloc.SubsidyAmount
                                dblSumSubsidyAmountBeforeEdit = objAlloc.SubsidyAmountBeforeEdit
                                strDealerCode = objAlloc.Dealer.DealerCode
                            Else
                                dblMaxJumlahSubsidy = objAlloc.MaxSubsidyAmount
                                dblSumJumlahSubsidy += objAlloc.SubsidyAmount
                                dblSumSubsidyAmountBeforeEdit += objAlloc.SubsidyAmountBeforeEdit
                            End If
                            If i = arrAlloc2.Count - 1 Then
                                dblMaxJumlahSubsidy += dblSumSubsidyAmountBeforeEdit
                                If objAlloc.BabitCategory <> "SPESIAL" AndAlso dblSumJumlahSubsidy > dblMaxJumlahSubsidy Then
                                    sb.Append("- Jumlah Subsidi untuk Kode Dealer " & strDealerCode & " sudah melebihi maksimal subsidinya.\n")
                                End If
                            End If
                        Next
                    End If
                End If
            Else
                sb.Append("- Alokasi Babit harus Diisi.\n")
            End If
        End If

        If IsLoginAsDealer() Then
            Dim intNo As Integer = 0
            Dim dteGetDate As Date = Now.ToShortDateString()
            Dim _arrBabitMediaIklan As ArrayList = CType(sessHelper.GetSession(sessionIklan), ArrayList)
            For Each objBID As BabitIklanDetail In _arrBabitMediaIklan
                Dim dtePeriodStart As Date = objBID.PeriodIklanStart
                Dim dtePeriodStartCalculate As Date
                Dim countWorkDays As Integer = 0
                Dim limitWorkDays As Integer = 0

                Dim objAppConfig As New AppConfig
                Dim criterias As New CriteriaComposite(New Criteria(GetType(AppConfig), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(AppConfig), "Name", MatchType.Exact, "BabitSubmissionLimit"))
                Dim arrConfig As ArrayList = New AppConfigFacade(User).Retrieve(criterias)
                If Not IsNothing(arrConfig) AndAlso arrConfig.Count > 0 Then
                    objAppConfig = CType(arrConfig(0), AppConfig)
                    limitWorkDays = objAppConfig.Value
                End If

                intNo += 1
                dtePeriodStartCalculate = dtePeriodStart
                If limitWorkDays > 0 Then
                    For i As Integer = 1 To 30
                        dtePeriodStartCalculate = dtePeriodStart.AddDays(-i)

                        Dim criterias2 As New CriteriaComposite(New Criteria(GetType(NationalHoliday), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias2.opAnd(New Criteria(GetType(NationalHoliday), "HolidayYear", MatchType.Exact, dtePeriodStartCalculate.Year))
                        criterias2.opAnd(New Criteria(GetType(NationalHoliday), "HolidayMonth", MatchType.Exact, dtePeriodStartCalculate.Month))
                        criterias2.opAnd(New Criteria(GetType(NationalHoliday), "HolidayDate", MatchType.Exact, dtePeriodStartCalculate.Day))
                        Dim arrDDL2 As ArrayList = New NationalHolidayFacade(User).Retrieve(criterias2)
                        If IsNothing(arrDDL2) OrElse (Not IsNothing(arrDDL2) AndAlso arrDDL2.Count = 0) Then
                            countWorkDays += 1
                            If countWorkDays = limitWorkDays Then
                                Exit For
                            End If
                        End If
                    Next
                    Dim intdteGetDate As Double = CDbl(Format(dteGetDate, "yyyyMMdd"))
                    Dim intPeriodMaxInput As Double = CDbl(Format(dtePeriodStartCalculate, "yyyyMMdd"))
                    If intdteGetDate > intPeriodMaxInput Then
                        sb.Append("- Pengajuan proposal nomor: " & intNo & " dengan media: " & objBID.BabitParameterHeader.ParameterName & "/" & objBID.MediaName & " hanya boleh dilakukan selambat-lambatnya " & limitWorkDays.ToString & " hari kerja sebelum kegiatan.\n")
                    End If
                End If
            Next
        End If

        Dim dtePeriod As Double = CDbl(CDate(1 & "/" & ddlMonth.SelectedValue & "/" & ddlYear.SelectedValue).ToString("yyyyMM"))
        Dim dteCurrent As Double = CDbl(CDate(Now.Date).ToString("yyyyMM"))
        If dtePeriod < dteCurrent Then
            sb.Append("- Periode bulan dan tahun tidak boleh kurang dari bulan ini \n")
        End If

        Return sb.ToString
    End Function

    Private Function ValidateIsMandatoryParamBabitEvent(ByRef strParamName As String) As String
        strParamName = String.Empty
        Dim dataList As ArrayList = New ArrayList
        Dim arlEvent As ArrayList = CType(sessHelper.GetSession(sessionIklan), ArrayList)

        Dim isMandatory As Boolean = False
        Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitParameterHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "BabitMasterEventType.TypeCode", MatchType.Exact, strTypeCode))
        criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "ParameterCategory", MatchType.Exact, 4))
        criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "Status", MatchType.Exact, 1))
        criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "IsMandatory", MatchType.Exact, 1))
        Dim arrBabitParamHdr As ArrayList = New BabitParameterHeaderFacade(User).Retrieve(criterias)
        If Not IsNothing(arrBabitParamHdr) AndAlso arrBabitParamHdr.Count > 0 Then
            For Each objParam As BabitParameterHeader In arrBabitParamHdr
                dataList = New ArrayList(
                                (From obj As BabitIklanDetail In arlEvent.OfType(Of BabitIklanDetail)()
                                    Where obj.BabitParameterHeader.ID = objParam.ID
                                    Select obj).ToList())
                If dataList.Count = 0 Then
                    If strParamName = String.Empty Then
                        strParamName = objParam.ParameterName
                    Else
                        strParamName += ", " & objParam.ParameterName
                    End If
                End If
            Next
        End If

        Return strParamName
    End Function

    Private Sub FromList()
        If Mode <> "New" Then
            oHeader = New BabitHeaderFacade(User).Retrieve(CInt(Request.QueryString("BabitHeaderID")))
            sessHelper.SetSession(sessionBabitHdr, oHeader)

            objDealer = oHeader.Dealer
            sessHelper.SetSession("FrmInputBabitIklan.DEALER", objDealer)

            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(BabitDocument), "BabitHeader.ID", MatchType.Exact, oHeader.ID))
            Dim arlDoc As ArrayList = New BabitDocumentFacade(User).Retrieve(crit)

            crit = New CriteriaComposite(New Criteria(GetType(BabitIklanDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(BabitIklanDetail), "BabitHeader.ID", MatchType.Exact, oHeader.ID))
            Dim arlMed As ArrayList = New BabitIklanDetailFacade(User).Retrieve(crit)

            Dim arlDAlloc As New ArrayList
            If Not IsLoginAsDealer() Then
                crit = New CriteriaComposite(New Criteria(GetType(BabitDealerAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(BabitDealerAllocation), "BabitHeader.ID", MatchType.Exact, oHeader.ID))
                arlDAlloc = New BabitDealerAllocationFacade(User).Retrieve(crit)
                Dim dblRemains As Double = 0
                For Each objAlloc As BabitDealerAllocation In arlDAlloc
                    objAlloc.SubsidyAmountBeforeEdit = objAlloc.SubsidyAmount
                    dblRemains = 0
                    Dim arrView As ArrayList = New V_BabitMasterRetailTargetFacade(User).RetrieveAllocationBudgetByDealer(objAlloc.Dealer.ID.ToString(), oHeader.PeriodStart)
                    If Not IsNothing(arrView) AndAlso arrView.Count > 0 Then
                        For Each objV_BabitMasterRetailTarget As V_BabitMasterRetailTarget In arrView
                            dblRemains += (objV_BabitMasterRetailTarget.TotalAllocationBabit - objV_BabitMasterRetailTarget.SumSubsidyAmount)
                        Next
                    End If
                    objAlloc.MaxSubsidyAmount = dblRemains
                Next
            End If

            'If ViewState("POSTBACK") Then
            sessHelper.SetSession(sessionIklan, IIf(IsNothing(arlMed), New ArrayList, arlMed))
            sessHelper.SetSession(sessionFiles, IIf(IsNothing(arlDoc), New ArrayList, arlDoc))
            sessHelper.SetSession(sessionAlloc, IIf(IsNothing(arlDAlloc), New ArrayList, arlDAlloc))
            'End If
            If Mode = "Detail" Then
                dgFiles.ShowFooter = False
                dgIklan.ShowFooter = False
                dgAlloc.ShowFooter = False
                txtTemporaryOutlet.Enabled = False
                ddlMarBox.Enabled = False
                'icDatePeriodeStart.Enabled = False
                'icDatePeriodeAkhir.Enabled = False
                ddlMonth.Enabled = False
                ddlYear.Enabled = False
                txtNomorSurat.Enabled = False
                txtNotes.Enabled = False
                dgFiles.Columns(dgFiles.Columns.Count - 1).Visible = False
                dgIklan.Columns(dgIklan.Columns.Count - 1).Visible = False
                dgAlloc.Columns(dgAlloc.Columns.Count - 1).Visible = False
                lblPopUpTO.Visible = False
                btnSimpan.Enabled = False
            End If
            btnKembali.Visible = True
            lblJenisAlokasi.Text = oHeader.BabitRegNumber
            lblDealerCode.Text = oHeader.Dealer.DealerCode
            lblDealerName.Text = " \ " & oHeader.Dealer.DealerName
            If Not IsNothing(oHeader.DealerBranch) Then
                txtTemporaryOutlet.Text = oHeader.DealerBranch.DealerBranchCode
                lblNamaCabang.Text = oHeader.DealerBranch.Name
            End If
            txtNomorSurat.Text = oHeader.BabitDealerNumber

            'ddlMarBox.SelectedValue = oHeader.MarboxID
            'ddlMarBox_SelectedIndexChanged(Nothing, Nothing)

            'icDatePeriodeStart.Value = oHeader.PeriodStart
            'icDatePeriodeAkhir.Value = oHeader.PeriodEnd
            ddlMonth.SelectedValue = oHeader.PeriodStart.Month
            ddlYear.SelectedValue = oHeader.PeriodEnd.Year
            txtNotes.Text = oHeader.Notes
        End If
    End Sub

    Protected Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Response.Redirect("FrmBabitList.aspx?Back=OK")
    End Sub

    Private Function ValidateIsMandatoryParamBabit(ByRef strParamName As String) As String
        strParamName = String.Empty
        Dim dataList As ArrayList = New ArrayList
        Dim arlIklan As ArrayList = CType(sessHelper.GetSession(sessionIklan), ArrayList)

        Dim isMandatory As Boolean = False
        Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitParameterHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "BabitMasterEventType.TypeCode", MatchType.Exact, strTypeCode))
        criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "ParameterCategory", MatchType.Exact, 4))
        criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "Status", MatchType.Exact, 1))
        criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "IsMandatory", MatchType.Exact, 1))
        Dim arrBabitParamHdr As ArrayList = New BabitParameterHeaderFacade(User).Retrieve(criterias)
        If Not IsNothing(arrBabitParamHdr) AndAlso arrBabitParamHdr.Count > 0 Then
            For Each objParam As BabitParameterHeader In arrBabitParamHdr
                dataList = New ArrayList(
                                (From obj As BabitIklanDetail In arlIklan.OfType(Of BabitIklanDetail)()
                                    Where obj.BabitParameterDetail.BabitParameterHeader.ID = objParam.ID And obj.MediaName.Trim <> "Total Biaya :"
                                    Select obj).ToList())
                If dataList.Count = 0 Then
                    If strParamName = String.Empty Then
                        strParamName = objParam.ParameterName
                    Else
                        strParamName += ", " & objParam.ParameterName
                    End If
                End If
            Next
        End If

        Return strParamName
    End Function

    Private Sub LoadMarBox()
        Dim client As System.Net.WebClient = New System.Net.WebClient()
        client.Headers("Authorization") = "Bearer Db7i7pVn8QbQbvVSniUDRqMHGtgY9DygYo25VPVEfGoX"
        client.Headers("Content-type") = "application/json"
        Dim mylist As New BabitMarBox.RootObject

        Try
            client.BaseAddress = "https://api.typeform.com/forms/Zq1Mc6/responses"
            Dim _jsonResponse = client.DownloadString("https://api.typeform.com/forms/Zq1Mc6/responses")
            mylist = Newtonsoft.Json.JsonConvert.DeserializeObject(Of BabitMarBox.RootObject)(_jsonResponse)

        Catch ex As Net.WebException
            If ex.Status = Net.WebExceptionStatus.ProtocolError Then
                Dim wrsp As Net.HttpWebResponse = CType(ex.Response, Net.HttpWebResponse)
                Dim statusCode As Integer = CType(wrsp.StatusCode, Integer)
                Dim msg = wrsp.StatusDescription
                Throw New HttpException(statusCode, msg)
            Else
                Throw New HttpException(500, ex.Message)
            End If
        End Try
        sessHelper.SetSession("Marbox", mylist)
        BindMarbox()
    End Sub

    Private Sub BindMarbox()
        Dim Marbox As BabitMarBox.RootObject = CType(sessHelper.GetSession("Marbox"), BabitMarBox.RootObject)
        ddlMarBox.Items.Clear()
        With ddlMarBox.Items
            .Add(New ListItem("Silahkan Pilih", "-1", True))
            For Each MB As BabitMarBox.Item In Marbox.items
                Try
                    If MB.answers.Item(6).number = objDealer.DealerCode Then
                        .Add(New ListItem(MB.answers.Item(1).text, MB.landing_id))
                    End If
                Catch
                End Try
            Next
        End With
        ddlMarBox.SelectedIndex = 0

        For i As Integer = 0 To ddlMarBox.Items.Count - 1
            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits.opAnd(New Criteria(GetType(BabitHeader), "MarboxID", MatchType.Exact, ddlMarBox.Items(i).Value))
            Dim arlCheck As ArrayList = New BabitHeaderFacade(User).Retrieve(crits)

            If arlCheck.Count > 0 Then
                If oHeader.MarboxID <> ddlMarBox.Items(i).Value Then
                    If i <> 0 Then
                        ddlMarBox.Items.RemoveAt(i)
                    End If
                End If
            End If
        Next
    End Sub

    Protected Sub ddlMarBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMarBox.SelectedIndexChanged
        If ddlMarBox.SelectedIndex = 0 Then
            lblLokasiMarbox.Text = ""
            lblPeriodeMarbox.Text = ""
        Else
            Dim Marbox As BabitMarBox.RootObject = CType(sessHelper.GetSession("Marbox"), BabitMarBox.RootObject)
            For Each MB As BabitMarBox.Item In Marbox.items
                Try
                    If MB.landing_id = ddlMarBox.SelectedValue Then
                        lblPeriodeMarbox.Text = MB.answers.Item(3).date & " - " & MB.answers.Item(4).date
                        lblLokasiMarbox.Text = MB.answers.Item(2).text
                    End If
                Catch
                End Try
            Next
        End If
    End Sub

    Protected Sub lnkReload_Click(sender As Object, e As EventArgs) Handles lnkReload.Click
        LoadMarBox()
    End Sub

    Private Sub BindDDLAllocationBabit_old(ByVal ddlAllocationBabit As DropDownList)
        With ddlAllocationBabit
            .Items.Clear()
            .Items.Insert(0, New ListItem("Silahkan Pilih", -1))

            Dim criterias As New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Category), "ProductCategory.Code", MatchType.Exact, KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")))
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(Category), "CategoryCode", Sort.SortDirection.ASC))  '-- Sort by Category code
            Dim arrDDL As ArrayList = New CategoryFacade(User).Retrieve(criterias, sortColl)
            Dim i% = 1
            For Each objCategory As Category In arrDDL
                .Items.Insert(i, New ListItem("BABIT " & objCategory.CategoryCode, objCategory.CategoryCode))
                i += 1
            Next
            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(BabitMasterPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(BabitMasterPrice), "SpecialCategoryFlag", MatchType.Exact, 1))
            Dim arrDDL2 As ArrayList = New BabitMasterPriceFacade(User).Retrieve(criterias2)
            Dim newArrDDL2 = From obj As BabitMasterPrice In arrDDL2
                                         Group By obj.SubCategoryVehicle.ID Into Group
                                    Select ID
            For Each id As Integer In newArrDDL2
                Dim obj As SubCategoryVehicle = New SubCategoryVehicleFacade(User).Retrieve(CType(id, Short))
                .Items.Insert(i, New ListItem("BABIT " & obj.Name, obj.Name.Replace(" ", "_")))
                i += 1
            Next
        End With
        ddlAllocationBabit.SelectedIndex = 0
    End Sub

    Private Sub BindDDLAllocationBabit(ByVal _dealerID As Integer, ByVal ddlAllocationBabit As DropDownList)
        Dim strAllocationBabitValue As String = String.Empty
        Dim strAllocationBabitDesc As String = String.Empty
        With ddlAllocationBabit
            .Items.Clear()
            .Items.Insert(0, New ListItem("Silahkan Pilih", -1))

            Dim periodStart As DateTime = CDate(1 & "/" & ddlMonth.SelectedValue & "/" & ddlYear.SelectedValue).ToString("yyyy/MM/dd 00:00:00")
            Dim arrDDL As ArrayList = New BabitBudgetHeaderFacade(User).GetDataAllocationByDealer(_dealerID, periodStart)
            Dim allocationBabitList As Dictionary(Of String, String) = New Dictionary(Of String, String)

            For Each obj As BabitBudgetHeader In arrDDL
                If Not IsNothing(obj.Dealer) Then
                    If obj.SubCategoryVehicleID = 0 Then
                        strAllocationBabitValue = obj.Category.CategoryCode
                        strAllocationBabitDesc = obj.Category.CategoryCode
                    Else
                        strAllocationBabitValue = obj.SubCategoryVehicle.Name.Replace(" ", "_")
                        strAllocationBabitDesc = obj.SubCategoryVehicle.Name
                    End If
                Else
                    strAllocationBabitValue = obj.Description.Replace(" ", "_").ToUpper
                    strAllocationBabitDesc = obj.Description.ToUpper
                End If

                If Not allocationBabitList.ContainsKey(strAllocationBabitValue) Then
                    allocationBabitList.Add(strAllocationBabitValue, "BABIT " & strAllocationBabitDesc.ToUpper())
                End If
            Next

            Dim i% = 1
            For Each iKey As String In allocationBabitList.Keys
                Dim value As String = allocationBabitList(iKey)
                .Items.Insert(i, New ListItem(value, iKey))
                i += 1
            Next
        End With
        ddlAllocationBabit.SelectedIndex = 0
    End Sub

    Protected Sub dgAlloc_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgAlloc.ItemDataBound
        Select Case e.Item.ItemType
            Case ListItemType.Item, ListItemType.AlternatingItem
                Dim arrAlloc As ArrayList = CType(sessHelper.GetSession(sessionAlloc), ArrayList)
                Dim objBabitDealerAllocation As BabitDealerAllocation = arrAlloc(e.Item.ItemIndex)

                Dim lblAllocationBabit As Label = CType(e.Item.FindControl("lblAllocationBabit"), Label)
                Dim lblJmlSubsidy As Label = CType(e.Item.FindControl("lblJmlSubsidy"), Label)
                Dim lblJmlMaxSubsidy As Label = CType(e.Item.FindControl("lblJmlMaxSubsidy"), Label)

                Dim lblDealerID As Label = CType(e.Item.FindControl("lblDealerID"), Label)
                Dim lblDealerCodeName As Label = CType(e.Item.FindControl("lblDealerCodeName"), Label)
                If Not IsNothing(objBabitDealerAllocation.Dealer) Then
                    lblDealerID.Text = objBabitDealerAllocation.Dealer.ID
                    lblDealerCodeName.Text = objBabitDealerAllocation.Dealer.DealerCode & " / " & objBabitDealerAllocation.Dealer.DealerName
                End If

                Dim dealerID As Integer = 0
                If Not IsNothing(objBabitDealerAllocation.Dealer) Then
                    dealerID = objBabitDealerAllocation.Dealer.ID
                End If
                'Dim intSubCategoryVehicleID As Integer = 0
                'Dim intCategoryID As Integer = 0
                'Dim oCategory As Category
                'Dim criterias As New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'criterias.opAnd(New Criteria(GetType(Category), "CategoryCode", MatchType.Exact, objBabitDealerAllocation.BabitCategory))
                'Dim arrCat As ArrayList = New CategoryFacade(User).Retrieve(criterias)
                'If Not IsNothing(arrCat) AndAlso arrCat.Count > 0 Then
                '    oCategory = CType(arrCat(0), Category)
                '    intCategoryID = oCategory.ID
                '    intSubCategoryVehicleID = 0
                'Else
                '    Dim strSQL As String = String.Empty
                '    strSQL = "select distinct a.ID "
                '    strSQL += "from SubCategoryVehicle a "
                '    strSQL += "where a.RowStatus = 0 "
                '    strSQL += "and replace(a.name,' ','') = '" & objBabitDealerAllocation.BabitCategory & "'"

                '    Dim crit As New CriteriaComposite(New Criteria(GetType(SubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                '    crit.opAnd(New Criteria(GetType(SubCategoryVehicle), "ID", MatchType.InSet, "(" & strSQL & ")"))
                '    Dim arrSCV As ArrayList = New SubCategoryVehicleFacade(User).Retrieve(crit)
                '    If Not IsNothing(arrSCV) AndAlso arrSCV.Count > 0 Then
                '        Dim oSubCategoryVehicle As SubCategoryVehicle = CType(arrSCV(0), SubCategoryVehicle)
                '        intCategoryID = oSubCategoryVehicle.Category.ID
                '        intSubCategoryVehicleID = oSubCategoryVehicle.ID
                '    End If
                'End If

                Dim dblRemains As Double = 0
                Dim _periodStart As DateTime = CDate(1 & "/" & ddlMonth.SelectedValue & "/" & ddlYear.SelectedValue).ToString("yyyy/MM/dd 00:00:00")
                'Dim arrView As ArrayList = New V_BabitMasterRetailTargetFacade(User).RetrieveAllocationBudgetByDealer(objBabitDealerAllocation.BabitCategory, dealerID.ToString, intCategoryID, intSubCategoryVehicleID, _periodStart)
                Dim arrView As ArrayList = New V_BabitMasterRetailTargetFacade(User).RetrieveAllocationBudgetByDealer(dealerID.ToString, _periodStart)
                If Not IsNothing(arrView) AndAlso arrView.Count > 0 Then
                    For Each objV_BabitMasterRetailTarget As V_BabitMasterRetailTarget In arrView
                        dblRemains += (objV_BabitMasterRetailTarget.TotalAllocationBabit - objV_BabitMasterRetailTarget.SumSubsidyAmount)
                    Next
                End If
                lblJmlMaxSubsidy.Text = Format(dblRemains, "###,###,##0")

                lblAllocationBabit.Text = "BABIT " & objBabitDealerAllocation.BabitCategory.ToString()
                lblJmlSubsidy.Text = objBabitDealerAllocation.SubsidyAmount.ToString("#,##0")
            Case ListItemType.Footer
                Dim ddlAllocationBabit As DropDownList = CType(e.Item.FindControl("ddlAllocationBabit"), DropDownList)
                Dim txtJmlSubsidy As TextBox = CType(e.Item.FindControl("txtJmlSubsidy"), TextBox)
                Dim txtDealerCodeAlokasi As TextBox = CType(e.Item.FindControl("txtDealerCodeAlokasi"), TextBox)
                Dim lblFJmlMaxSubsidy As Label = CType(e.Item.FindControl("lblFJmlMaxSubsidy"), Label)
                txtJmlSubsidy.Text = Format(Biaya(), "###,###")

                If Mode <> "New" Then
                    If Not IsNothing(Request.QueryString("BabitHeaderID")) Then
                        oHeader = CType(sessHelper.GetSession(sessionBabitHdr), BabitHeader)
                        If Not IsNothing(oHeader) AndAlso oHeader.ID > 0 Then
                            txtDealerCodeAlokasi.Text = oHeader.Dealer.DealerCode
                            If txtDealerCodeAlokasi.Text.Trim <> "" Then
                                BindDDLAllocationBabit(oHeader.Dealer.ID, ddlAllocationBabit)
                                Dim dblRemains As Double = 0
                                Dim _periodStart As DateTime = CDate(1 & "/" & ddlMonth.SelectedValue & "/" & ddlYear.SelectedValue).ToString("yyyy/MM/dd 00:00:00")
                                'Dim arrView As ArrayList = New V_BabitMasterRetailTargetFacade(User).RetrieveAllocationBudgetByDealer(objBabitDealerAllocation.BabitCategory, dealerID.ToString, intCategoryID, intSubCategoryVehicleID, _periodStart)
                                Dim arrView As ArrayList = New V_BabitMasterRetailTargetFacade(User).RetrieveAllocationBudgetByDealer(oHeader.Dealer.ID.ToString, _periodStart)
                                If Not IsNothing(arrView) AndAlso arrView.Count > 0 Then
                                    For Each objV_BabitMasterRetailTarget As V_BabitMasterRetailTarget In arrView
                                        dblRemains += (objV_BabitMasterRetailTarget.TotalAllocationBabit - objV_BabitMasterRetailTarget.SumSubsidyAmount)
                                    Next
                                End If
                                lblFJmlMaxSubsidy.Text = Format(dblRemains, "###,###,##0")
                            End If
                        End If
                    End If
                End If
        End Select
    End Sub

    Private Function Biaya() As Integer
        Dim _return As Integer = 0

        If SesDealer().Title = EnumDealerTittle.DealerTittle.KTB Then
            Try
                Dim headerID As Integer = CInt(Request.QueryString("BabitHeaderID"))
                Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitIklanDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crits.opAnd(New Criteria(GetType(BabitIklanDetail), "BabitHeader.ID", MatchType.Exact, headerID))
                Dim iklanArr As ArrayList = New BabitIklanDetailFacade(User).Retrieve(crits)
                For Each bid As BabitIklanDetail In iklanArr
                    _return += CInt(bid.SubmissionAmount)
                Next
                _return = _return / 2
            Catch
            End Try
        End If

        Return _return
    End Function

    Protected Sub dgAlloc_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgAlloc.ItemCommand
        Dim arrAlloc As ArrayList = CType(sessHelper.GetSession(sessionAlloc), ArrayList)
        Select Case e.CommandName
            Case "Add"
                Dim txtDealerCodeAlokasi As TextBox = CType(e.Item.FindControl("txtDealerCodeAlokasi"), TextBox)
                Dim txtJmlSubsidy As TextBox = CType(e.Item.FindControl("txtJmlSubsidy"), TextBox)
                Dim ddlAllocationBabit As DropDownList = CType(e.Item.FindControl("ddlAllocationBabit"), DropDownList)
                Dim lblFJmlMaxSubsidy As Label = CType(e.Item.FindControl("lblFJmlMaxSubsidy"), Label)

                If ddlAllocationBabit.SelectedIndex = 0 Then
                    MessageBox.Show("Alokasi BABIT harus diisi")
                    Exit Sub
                End If
                If txtJmlSubsidy.Text.Trim = "" OrElse txtJmlSubsidy.Text.Trim = "0" Then
                    MessageBox.Show("Jumlah Subsidi harus diisi")
                    Exit Sub
                End If

                For Each obj As BabitDealerAllocation In arrAlloc
                    If ddlAllocationBabit.SelectedValue = obj.BabitCategory Then
                        MessageBox.Show("Tipe Alokasi tidak boleh sama dalam satu pengajuan Babit")
                        Exit Sub
                    End If
                Next

                Dim dblRemains As Double = lblFJmlMaxSubsidy.Text
                Dim dblSubsidyAmount As Double = txtJmlSubsidy.Text
                If ddlAllocationBabit.SelectedValue <> "SPESIAL" Then
                    If dblRemains < dblSubsidyAmount Then
                        MessageBox.Show("Jumlah Subsidi Tidak Boleh Melebihi Maksimal Subsidi Babit")
                        Exit Sub
                    End If
                End If

                Dim oAlloc As New BabitDealerAllocation
                oAlloc.Dealer = New DealerFacade(User).Retrieve(txtDealerCodeAlokasi.Text)
                oAlloc.BabitCategory = IIf(ddlAllocationBabit.SelectedIndex = 0, "", ddlAllocationBabit.SelectedValue)
                Try
                    oAlloc.SubsidyAmount = txtJmlSubsidy.Text
                Catch
                    oAlloc.SubsidyAmount = Biaya()
                End Try
                oAlloc.MaxSubsidyAmount = dblRemains
                arrAlloc.Add(oAlloc)
                sessHelper.SetSession(sessionAlloc, arrAlloc)
            Case "Delete"
                Try
                    Dim oAlloc As BabitDealerAllocation = CType(arrAlloc(e.Item.ItemIndex), BabitDealerAllocation)
                    If oAlloc.ID > 0 Then
                        Dim arrDelete As ArrayList = CType(sessHelper.GetSession(DelSessionAlloc), ArrayList)
                        If IsNothing(arrDelete) Then arrDelete = New ArrayList
                        arrDelete.Add(oAlloc)
                        sessHelper.SetSession(DelSessionAlloc, arrDelete)
                    End If
                    arrAlloc.RemoveAt(e.Item.ItemIndex)
                Catch ex As Exception
                End Try
        End Select
        BindDgAlloc()
    End Sub

    Public Sub ddlAllocationBabit_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim arrDDL As ArrayList = New ArrayList
        Dim ddlAllocationBabit As DropDownList = sender
        Dim gridItem As DataGridItem = ddlAllocationBabit.Parent.Parent
        Dim txtDealerCodeAlokasi As TextBox
        Dim lblFJmlMaxSubsidy As Label
        If gridItem.DataSetIndex > -1 Then
            txtDealerCodeAlokasi = gridItem.FindControl("txtEDealerCode")
            lblFJmlMaxSubsidy = gridItem.FindControl("lblEJmlMaxSubsidy")
        Else
            txtDealerCodeAlokasi = gridItem.FindControl("txtDealerCodeAlokasi")
            lblFJmlMaxSubsidy = gridItem.FindControl("lblFJmlMaxSubsidy")
        End If
        'If ddlAllocationBabit.SelectedIndex > 0 AndAlso txtDealerCodeAlokasi.Text.Trim <> "" Then
        '    Dim dblRemains As Double = 0
        '    Dim oDealer As Dealer = New DealerFacade(User).Retrieve(txtDealerCodeAlokasi.Text)

        '    Dim intSubCategoryVehicleID As Integer = 0
        '    Dim intCategoryID As Integer = 0
        '    Dim oCategory As Category
        '    Dim criterias As New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    criterias.opAnd(New Criteria(GetType(Category), "CategoryCode", MatchType.Exact, ddlAllocationBabit.SelectedValue))
        '    Dim arrCat As ArrayList = New CategoryFacade(User).Retrieve(criterias)
        '    If Not IsNothing(arrCat) AndAlso arrCat.Count > 0 Then
        '        oCategory = CType(arrCat(0), Category)
        '        intCategoryID = oCategory.ID
        '        intSubCategoryVehicleID = 0
        '    Else
        '        Dim strSQL As String = String.Empty
        '        strSQL = "select distinct a.ID "
        '        strSQL += "from SubCategoryVehicle a "
        '        strSQL += "where a.RowStatus = 0 "
        '        strSQL += "and replace(a.name,' ','') = '" & ddlAllocationBabit.SelectedValue & "'"

        '        Dim crit As New CriteriaComposite(New Criteria(GetType(SubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '        crit.opAnd(New Criteria(GetType(SubCategoryVehicle), "ID", MatchType.InSet, "(" & strSQL & ")"))
        '        Dim arrSCV As ArrayList = New SubCategoryVehicleFacade(User).Retrieve(crit)
        '        If Not IsNothing(arrSCV) AndAlso arrSCV.Count > 0 Then
        '            Dim oSubCategoryVehicle As SubCategoryVehicle = CType(arrSCV(0), SubCategoryVehicle)
        '            intCategoryID = oSubCategoryVehicle.Category.ID
        '            intSubCategoryVehicleID = oSubCategoryVehicle.ID
        '        End If
        '    End If

        '    Dim _periodStart As DateTime = CDate(1 & "/" & ddlMonth.SelectedValue & "/" & ddlYear.SelectedValue).ToString("yyyy/MM/dd 00:00:00")
        '    Dim arrView As ArrayList = New V_BabitMasterRetailTargetFacade(User).RetrieveAllocationBudgetByDealer(ddlAllocationBabit.SelectedValue, oDealer.ID.ToString(), intCategoryID, intSubCategoryVehicleID, _periodStart)
        '    If Not IsNothing(arrView) AndAlso arrView.Count > 0 Then
        '        For Each objV_BabitMasterRetailTarget As V_BabitMasterRetailTarget In arrView
        '            dblRemains += (objV_BabitMasterRetailTarget.TotalAllocationBabit - objV_BabitMasterRetailTarget.SumSubsidyAmount)
        '        Next
        '    End If
        '    lblFJmlMaxSubsidy.Text = Format(dblRemains, "###,###,##0")
        'Else
        '    lblFJmlMaxSubsidy.Text = 0
        'End If
    End Sub


End Class