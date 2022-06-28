Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports System.IO
Imports System.Text
Imports System.Configuration
Imports System.Web.UI.WebControls
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit

Public Class FrmInputEvent
    Inherits System.Web.UI.Page

    Private objDealer As Dealer
    Private objUserInfo As UserInfo
    Private sessHelper As New SessionHelper
    Const sessEventDealerDetail As String = "sessEventDealerDetail"
    Const SessEventDealerDoc As String = "sessEventDealerDocument"
    Const SessEventDealerRequiredDoc As String = "sessEventDealerRequiredDocument"
    Private TempDirectory As String
    Private TargetDirectory As String
    Private strMode As String = String.Empty
    Private MAX_FILE_SIZE As Integer = 5120000
    Const SessDeleteEventDealerDoc As String = "sessDeleteEventDealerDocument"
    Const SessDeleteEventDealerRequiredDoc As String = "sessDeleteEventDealerRequiredDocument"
    Const sessDeleteEventDealerDetail As String = "sessDeleteEventDealerDetail"
    Private Property SesDealer() As Dealer
        Get
            Return CType(sessHelper.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)
            sessHelper.SetSession("DEALER", Value)
        End Set
    End Property

    Private Property Mode() As String
        Get
            If Not IsNothing(Request.QueryString("Mode")) Then
                strMode = Request.QueryString("Mode")
            Else
                strMode = "New"
            End If
            hdnMode.Value = strMode
            Return strMode
        End Get
        Set(ByVal Value As String)
            Request.QueryString("Mode") = Value
        End Set
    End Property

    Private Function ValidateData() As String
        Dim sb As StringBuilder = New StringBuilder

        If (ddlEventCategory.SelectedValue = "-1") Then
            sb.Append("Kategori Event Dealer Harus Diisi\n")
        End If

        If (txtDealerCode.Text = String.Empty) Then
            sb.Append("Kode Dealer Harus Diisi\n")
        End If
        'If (txtTOCode.Text.Trim = String.Empty) Then
        '    sb.Append("Kode Temporary Outlet Harus Diisi\n")
        'End If
        If (icDatePeriodeStart.Value > icDatePeriodeAkhir.Value) Then
            sb.Append("Tanggal awal harus lebih kecil atau sama dengan tanggal akhir\n")
        End If

        If (sessHelper.GetSession(SessEventDealerDoc) Is Nothing) Then
            sb.Append("Data Dokumen Event belum ada\n")
        Else
            If CType(sessHelper.GetSession(SessEventDealerDoc), ArrayList).Count = 0 Then
                sb.Append("Data Dokumen Event belum ada\n")
            End If
        End If
        If (sessHelper.GetSession(SessEventDealerRequiredDoc) Is Nothing) Then
            sb.Append("Data Dokumen yang wajib di isi belum ada\n")
        Else
            If CType(sessHelper.GetSession(SessEventDealerRequiredDoc), ArrayList).Count = 0 Then
                sb.Append("Data Dokumen yang wajib di isi belum ada\n")
            End If
        End If

        Return sb.ToString()
    End Function

    Private Sub LoadEventDealer(ByVal intEventDealerHeaderID As Integer)
        Dim objEventDealerHeader As EventDealerHeader
        Dim objEventDealerDetail As EventDealerDetail
        Dim objEventDealerRequiredDocument As EventDealerRequiredDocument
        Dim arrEventDealerDetail As ArrayList
        Dim arrEventDealerRequiredDocument As ArrayList
        Dim ArrDoc As ArrayList

        objEventDealerHeader = New EventDealerHeaderFacade(User).Retrieve(intEventDealerHeaderID)
        If Not IsNothing(objEventDealerHeader) Then
            ddlEventCategory.SelectedValue = objEventDealerHeader.Category.ID
            icDatePeriodeStart.Value = objEventDealerHeader.PeriodStart
            icDatePeriodeAkhir.Value = objEventDealerHeader.PeriodEnd
            txtEventName.Text = objEventDealerHeader.EventName
            txtTargetBudget.Text = Format(CInt(objEventDealerHeader.SubsidyTarget), "###,###")
            txtMaxSubsidy.Text = CInt(objEventDealerHeader.MaxSubsidy)

            txtDealerCode.Text = ""
            txtTemporaryOutlet.Text = ""

            Dim criterias3 As New CriteriaComposite(New Criteria(GetType(EventDealerDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias3.opAnd(New Criteria(GetType(EventDealerDetail), "EventDealerHeader.ID", MatchType.Exact, intEventDealerHeaderID))
            arrEventDealerDetail = New EventDealerDetailFacade(User).Retrieve(criterias3)
            sessHelper.SetSession(sessEventDealerDetail, arrEventDealerDetail)

            CommonFunction.SortListControl(arrEventDealerDetail, "Dealer.ID", Sort.SortDirection.ASC)
            For Each obj As EventDealerDetail In arrEventDealerDetail
                'txtDealerCode.Text = obj.Dealer.DealerCode
                If Not IsNothing(obj.Dealer) Then
                    If Not txtDealerCode.Text.Contains(obj.Dealer.DealerCode) Then
                        If txtDealerCode.Text.Trim = "" Then
                            txtDealerCode.Text = obj.Dealer.DealerCode
                        Else
                            txtDealerCode.Text += ";" & obj.Dealer.DealerCode
                        End If
                    End If
                End If
                If Not IsNothing(obj.DealerBranch) Then
                    If Not txtTemporaryOutlet.Text.Contains(obj.DealerBranch.DealerBranchCode) Then
                        If txtTemporaryOutlet.Text.Trim = "" Then
                            txtTemporaryOutlet.Text = obj.DealerBranch.DealerBranchCode
                        Else
                            txtTemporaryOutlet.Text += ";" & obj.DealerBranch.DealerBranchCode
                        End If
                    End If
                End If
            Next

            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(EventDealerDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(EventDealerDocument), "EventDealerHeader.ID", MatchType.Exact, intEventDealerHeaderID))
            ArrDoc = New EventDealerDocumentFacade(User).Retrieve(criterias2)
            sessHelper.SetSession(SessEventDealerDoc, ArrDoc)
            BindEventDealerDocument()

            Dim criterias As New CriteriaComposite(New Criteria(GetType(EventDealerRequiredDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(EventDealerRequiredDocument), "EventDealerHeader.ID", MatchType.Exact, intEventDealerHeaderID))
            arrEventDealerRequiredDocument = New EventDealerRequiredDocumentFacade(User).Retrieve(criterias)
            sessHelper.SetSession(SessEventDealerRequiredDoc, arrEventDealerRequiredDocument)
            BindEventDealerRequiredDoc()

            Me.btnBack.Visible = True
            Me.btnSimpan.Enabled = False
            Me.lblPopUpDealer.Visible = False
            Me.lblPopUpTO.Visible = False
            Me.ddlEventCategory.Enabled = False
            Me.icDatePeriodeStart.Enabled = False
            Me.icDatePeriodeAkhir.Enabled = False
            Me.txtEventName.Enabled = False
            Me.txtTargetBudget.Enabled = False
            Me.txtMaxSubsidy.Enabled = False

            Me.dgEventDealerDocument.ShowFooter = False
            Me.dgEventDealerDocument.Columns(dgEventDealerDocument.Columns.Count - 1).Visible = False
            Me.dgEventDealerRequiredDoc.ShowFooter = False
            Me.dgEventDealerRequiredDoc.Columns(dgEventDealerRequiredDoc.Columns.Count - 1).Visible = False

            If Mode() = "Edit" Then
                Me.lblPopUpDealer.Visible = True
                Me.lblPopUpTO.Visible = True
                Me.btnSimpan.Enabled = True
                Me.ddlEventCategory.Enabled = True
                Me.icDatePeriodeStart.Enabled = True
                Me.icDatePeriodeAkhir.Enabled = True
                Me.txtEventName.Enabled = True
                Me.txtTargetBudget.Enabled = True
                Me.txtMaxSubsidy.Enabled = True

                Me.dgEventDealerDocument.ShowFooter = True
                Me.dgEventDealerDocument.Columns(dgEventDealerDocument.Columns.Count - 1).Visible = True
                Me.dgEventDealerRequiredDoc.ShowFooter = True
                Me.dgEventDealerRequiredDoc.Columns(dgEventDealerRequiredDoc.Columns.Count - 1).Visible = True
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Privilege_Auth()

        objDealer = CType(Session("DEALER"), Dealer)
        objUserInfo = CType(Session("LOGINUSERINFO"), UserInfo)
        TempDirectory = Server.MapPath("") + "\..\DataTemp\"
        TargetDirectory = KTB.DNet.Lib.WebConfig.GetValue("SAN")

        If Not IsPostBack Then
            Page_Init()
            BindEventDealerRequiredDoc()
            BindEventDealerDocument()

            If Not IsNothing(Request.QueryString("EventDealerHeaderID")) Then
                hdnEventDealerHeaderID.Value = Request.QueryString("EventDealerHeaderID")
                LoadEventDealer(hdnEventDealerHeaderID.Value)
            End If

            If IsLoginAsDealer() Then
                trJmlSubsidi.Visible = False
                trMaxSubsidi.Visible = False
            Else
                trJmlSubsidi.Visible = True
                trMaxSubsidi.Visible = True
            End If
        End If
    End Sub

    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function

    Private Sub UploadAttachment(ByVal ObjAttachment As EventDealerDocument, ByVal TargetPath As String)
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

    Private Sub Page_Init()
        BindDDLCategoryEvent()
        ClearAll()

        lblPopUpDealer.Attributes("onclick") = "ShowPopUpDealer()"
        lblPopUpTO.Attributes("onclick") = "ShowPopUpTO()"
    End Sub

    Private Sub Privilege_Auth()
        If Mode = "New" Then
            If Not SecurityProvider.Authorize(Context.User, SR.EVENT_Input_Event_Dealer_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=EVENT - INPUT EVENT DEALER")
            End If
        Else
            If Not SecurityProvider.Authorize(Context.User, SR.EVENT_Daftar_EventDealer_Detail_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=EVENT - INPUT EVENT DEALER")
            End If
        End If
    End Sub

    Private Sub BindDDLCategoryEvent()
        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(Category), "ProductCategory.ID", MatchType.Exact, 1))
        Dim _arrMedia As ArrayList = New CategoryFacade(User).Retrieve(criteria)

        With ddlEventCategory.Items
            .Clear()
            .Add(New ListItem("Silahkan Pilih", "-1"))
            For Each oPW As Category In _arrMedia
                .Add(New ListItem(oPW.CategoryCode, oPW.ID))
            Next
        End With
    End Sub

    Private Sub ClearAll()
        txtDealerCode.Text = ""
        txtTemporaryOutlet.Text = ""
        icDatePeriodeStart.Value = Date.Now
        icDatePeriodeAkhir.Value = Date.Now
        txtEventName.Text = ""
        txtTargetBudget.Text = "0"
        txtMaxSubsidy.Text = "0"
        sessHelper.SetSession(sessEventDealerDetail, New ArrayList)
        sessHelper.SetSession(sessDeleteEventDealerDetail, New ArrayList)
        sessHelper.SetSession(SessEventDealerDoc, New ArrayList)
        sessHelper.SetSession(SessEventDealerRequiredDoc, New ArrayList)
        sessHelper.SetSession(SessDeleteEventDealerDoc, New ArrayList)
        sessHelper.SetSession(SessDeleteEventDealerRequiredDoc, New ArrayList)
    End Sub

    Protected Sub hdnDealerCode_ValueChanged(sender As Object, e As EventArgs) Handles hdnDealerCode.ValueChanged
        txtDealerCode.Text = hdnDealerCode.Value
    End Sub

    Protected Sub hdnTemporaryOutlet_ValueChanged(sender As Object, e As EventArgs) Handles hdnTemporaryOutlet.ValueChanged
        txtTemporaryOutlet.Text = hdnTemporaryOutlet.Value
    End Sub

    Protected Sub dgEventDealerRequiredDoc_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgEventDealerRequiredDoc.ItemCommand
        Dim _arlEventDealerRequiredDoc As ArrayList = CType(sessHelper.GetSession(SessEventDealerRequiredDoc), ArrayList)
        Select Case e.CommandName
            Case "Add"
                Try
                    Dim txtFileEvent As TextBox = CType(e.Item.FindControl("txtFileEvent"), TextBox)
                    Dim objPostedData As HttpPostedFile
                    Dim sFileName As String
                    Dim oEDRD As New EventDealerRequiredDocument

                    If IsNothing(txtFileEvent) OrElse txtFileEvent.Text = String.Empty Then
                        MessageBox.Show("Nama Dokumen Masih Kosong")
                        Return
                    End If

                    oEDRD.DocumentType = 2
                    oEDRD.DocumentName = txtFileEvent.Text
                    _arlEventDealerRequiredDoc.Add(oEDRD)

                    sessHelper.SetSession(SessEventDealerRequiredDoc, _arlEventDealerRequiredDoc)
                Catch ex As Exception
                End Try

            Case "Delete"
                Dim arl As ArrayList = sessHelper.GetSession(SessEventDealerRequiredDoc)
                Try
                    Dim objEventDealerRequiredDocument As EventDealerRequiredDocument = CType(arl(e.Item.ItemIndex), EventDealerRequiredDocument)
                    If objEventDealerRequiredDocument.ID > 0 Then
                        Dim arrDelete As ArrayList = CType(sessHelper.GetSession(SessDeleteEventDealerRequiredDoc), ArrayList)
                        If IsNothing(arrDelete) Then arrDelete = New ArrayList
                        arrDelete.Add(objEventDealerRequiredDocument)
                        sessHelper.SetSession(SessDeleteEventDealerRequiredDoc, arrDelete)
                    End If
                    arl.RemoveAt(e.Item.ItemIndex)
                Catch
                End Try

        End Select

        BindEventDealerRequiredDoc()
    End Sub

    Private Function TimeStamp() As String
        Return DateTime.Now.Year & DateTime.Now.Month & DateTime.Now.Day & DateTime.Now.Hour & DateTime.Now.Minute & DateTime.Now.Second & DateTime.Now.Millisecond
    End Function

    Private Sub BindEventDealerRequiredDoc()
        dgEventDealerRequiredDoc.DataSource = CType(sessHelper.GetSession(SessEventDealerRequiredDoc), ArrayList)
        dgEventDealerRequiredDoc.DataBind()
    End Sub

    Protected Sub dgEventDealerRequiredDoc_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgEventDealerRequiredDoc.ItemDataBound
        Dim _arlReq As ArrayList = CType(sessHelper.GetSession(SessEventDealerRequiredDoc), ArrayList)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblFileEvent As Label = CType(e.Item.FindControl("lblFileEvent"), Label)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblFileEvent.Text = CType(_arlReq(e.Item.ItemIndex), EventDealerRequiredDocument).DocumentName
            lblNo.Text = e.Item.ItemIndex + 1
        End If
    End Sub

    Private Sub BindDdlFileCat()
        Dim ddlFileCat As DropDownList = LoopDGrid("ddlFileCat", "DropDownList", dgEventDealerRequiredDoc)

        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(Category), "ProductCategory.ID", MatchType.Exact, 1))
        Dim _arrMedia As ArrayList = New CategoryFacade(User).Retrieve(criteria)

        With ddlFileCat.Items
            .Clear()
            .Add(New ListItem("Silahkan Pilih", "-1"))
            For Each oPW As Category In _arrMedia
                .Add(New ListItem(oPW.CategoryCode, oPW.ID))
            Next
        End With
    End Sub

    Private Function LoopDGrid(ControlID As String, ControlType As String, ByVal grid As DataGrid)
        For Each e1 As Control In grid.Controls
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

    Protected Sub ddlEventCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEventCategory.SelectedIndexChanged
        Me.txtDealerCode.Text = ""
        Me.txtTemporaryOutlet.Text = ""
    End Sub

    Private Function getEventDealerHeader(ByVal id As Integer) As EventDealerHeader
        Return New EventDealerHeaderFacade(User).Retrieve(id)
    End Function

    Private Function getEventDealerRequiredDoc(ByVal id As Integer) As EventDealerRequiredDocument
        Return New EventDealerRequiredDocumentFacade(User).Retrieve(id)
    End Function

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Dim arrDoc As New ArrayList
        Dim arrReqDoc As New ArrayList
        Dim arrDet As New ArrayList
        Dim arrDet2 As New ArrayList
        Dim arrUpdateDet As New ArrayList
        Dim arlDeleteDoc As New ArrayList
        Dim arlDeleteRequiredDoc As New ArrayList

        Dim str As String
        str = ValidateData()
        If (str.Length > 0) Then
            MessageBox.Show(str)
            Exit Sub
        End If

        Dim oHeader As EventDealerHeader
        If Mode() = "Edit" Then
            oHeader = New EventDealerHeaderFacade(User).Retrieve(CInt(hdnEventDealerHeaderID.Value))
        Else
            oHeader = New EventDealerHeader
        End If
        oHeader.PeriodStart = icDatePeriodeStart.Value
        oHeader.PeriodEnd = icDatePeriodeAkhir.Value
        oHeader.EventName = txtEventName.Text
        oHeader.SubsidyTarget = txtTargetBudget.Text
        oHeader.MaxSubsidy = txtMaxSubsidy.Text
        oHeader.Category = New CategoryFacade(User).Retrieve(CInt(ddlEventCategory.SelectedValue))

        Dim arrFiles As ArrayList = sessHelper.GetSession(SessEventDealerDoc)
        For Each obj As EventDealerDocument In arrFiles
            Dim oDoc As New EventDealerDocument
            oDoc.EventDealerHeader = oHeader
            oDoc = obj
            arrDoc.Add(oDoc)
        Next
        sessHelper.SetSession(SessEventDealerDoc, arrDoc)

        Dim arrRequiredDoc As ArrayList = sessHelper.GetSession(SessEventDealerRequiredDoc)
        For Each obj As EventDealerRequiredDocument In arrRequiredDoc
            Dim oReqDoc As New EventDealerRequiredDocument
            oReqDoc.EventDealerHeader = oHeader
            oReqDoc = obj
            arrReqDoc.Add(oReqDoc)
        Next
        sessHelper.SetSession(SessEventDealerRequiredDoc, arrReqDoc)

        If Mode() = "Edit" Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(EventDealerDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(EventDealerDetail), "EventDealerHeader.ID", MatchType.Exact, hdnEventDealerHeaderID.Value))
            arrDet = New EventDealerDetailFacade(User).Retrieve(criterias)
        End If

        If txtDealerCode.Text.Trim <> String.Empty Then
            For Each sDealer As String In txtDealerCode.Text.Trim.Split(";")
                Dim dealer As Dealer = New DealerFacade(User).GetDealer(sDealer)
                Dim branch As DealerBranch
                If txtTemporaryOutlet.Text.Trim <> String.Empty Then
                    Dim lenghtArray As Integer = txtTemporaryOutlet.Text.Trim.Split(";").Length
                    For Each sBranch As String In txtTemporaryOutlet.Text.Trim.Split(";")
                        branch = New DealerBranchFacade(User).Retrieve(sBranch, dealer.DealerCode)
                        If Not IsNothing(branch) Then
                            Dim oDetail As New EventDealerDetail
                            Dim criterias As New CriteriaComposite(New Criteria(GetType(EventDealerDetail), "EventDealerHeader.ID", MatchType.Exact, hdnEventDealerHeaderID.Value))
                            criterias.opAnd(New Criteria(GetType(EventDealerDetail), "Dealer.ID", MatchType.Exact, dealer.ID))
                            criterias.opAnd(New Criteria(GetType(EventDealerDetail), "DealerBranch.ID", MatchType.Exact, branch.ID))
                            arrDet2 = New EventDealerDetailFacade(User).Retrieve(criterias)
                            If Not IsNothing(arrDet2) AndAlso arrDet2.Count > 0 Then
                                oDetail = CType(arrDet2(0), EventDealerDetail)
                            Else
                                oDetail = New EventDealerDetail
                            End If
                            oDetail.EventDealerHeader = oHeader
                            oDetail.Dealer = dealer
                            oDetail.DealerBranch = branch
                            arrUpdateDet.Add(oDetail)
                            lenghtArray = lenghtArray - 1
                        End If
                    Next
                    If txtTemporaryOutlet.Text.Trim.Split(";").Length = lenghtArray Then
                        GoTo addEventDealerDetail
                    End If
                Else
addEventDealerDetail:
                    Dim oDetail As New EventDealerDetail
                    Dim criterias As New CriteriaComposite(New Criteria(GetType(EventDealerDetail), "EventDealerHeader.ID", MatchType.Exact, hdnEventDealerHeaderID.Value))
                    criterias.opAnd(New Criteria(GetType(EventDealerDetail), "Dealer.ID", MatchType.Exact, dealer.ID))
                    arrDet2 = New EventDealerDetailFacade(User).Retrieve(criterias)
                    If Not IsNothing(arrDet2) AndAlso arrDet2.Count > 0 Then
                        oDetail = CType(arrDet2(0), EventDealerDetail)
                    Else
                        oDetail = New EventDealerDetail
                    End If
                    oDetail.EventDealerHeader = oHeader
                    oDetail.Dealer = dealer
                    oDetail.DealerBranch = branch
                    arrUpdateDet.Add(oDetail)
                End If
            Next
        End If
        sessHelper.SetSession(sessEventDealerDetail, arrUpdateDet)

        arlDeleteDoc = CType(sessHelper.GetSession(SessDeleteEventDealerDoc), ArrayList)
        arlDeleteRequiredDoc = CType(sessHelper.GetSession(SessDeleteEventDealerRequiredDoc), ArrayList)

        arrUpdateDet = IIf(Not IsNothing(arrUpdateDet), arrUpdateDet, New ArrayList)
        arrDoc = IIf(Not IsNothing(arrDoc), arrDoc, New ArrayList)
        arrReqDoc = IIf(Not IsNothing(arrReqDoc), arrReqDoc, New ArrayList)

        Dim _result As Integer = 0
        If Mode() <> "Edit" Then
            _result = New EventDealerHeaderFacade(User).InsertTransaction(oHeader, arrUpdateDet, arrDoc, arrReqDoc)
        Else
            _result = New EventDealerHeaderFacade(User).UpdateTransaction(oHeader, arrDet, arrUpdateDet, arrDoc, arlDeleteDoc, arrReqDoc, arlDeleteRequiredDoc)
        End If

        Dim strJs As String = String.Empty
        If _result > 0 Then
            Dim arrBabitDoc As ArrayList = sessHelper.GetSession(SessEventDealerDoc)
            CommitAttachment(arrBabitDoc)
            If Mode() = "Edit" Then
                If Not IsNothing(arlDeleteDoc) Then
                    RemoveDocumentAttachment(arlDeleteDoc, TargetDirectory)
                End If
            End If
            strJs = "alert('Simpan Data Berhasil');"
            strJs += "window.location = '../Event/FrmEventDealerList.aspx';"

            ClearAll()
            'MessageBox.Show("Simpan Berhasil")
        Else
            strJs = "alert('Simpan Data Gagal');"
        End If

        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Script", strJs, True)
    End Sub

    Private Sub RemoveDocumentAttachment(ByVal AttachmentCollection As ArrayList, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim finfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                For Each obj As EventDealerDocument In AttachmentCollection
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

    Private Sub CommitAttachment(ByVal AttachmentCollection As ArrayList)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim TargetFInfo As FileInfo
        Dim TempFInfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                For Each obj As EventDealerDocument In AttachmentCollection
                    If Not IsNothing(obj.AttachmentData) Then
                        TargetFInfo = New FileInfo(TargetDirectory + obj.Path)
                        TempFInfo = New FileInfo(TempDirectory + obj.Path)

                        If TempFInfo.Exists Then
                            If Not TargetFInfo.Directory.Exists Then
                                Directory.CreateDirectory(TargetFInfo.DirectoryName)
                            End If
                            TempFInfo.MoveTo(TargetFInfo.FullName)
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


    Private Sub BindEventDealerDocument()
        dgEventDealerDocument.DataSource = CType(sessHelper.GetSession(SessEventDealerDoc), ArrayList)
        dgEventDealerDocument.DataBind()
    End Sub

    Protected Sub dgEventDealerDocument_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgEventDealerDocument.ItemCommand
        Dim arrEventDealerDoc As ArrayList = CType(sessHelper.GetSession(SessEventDealerDoc), ArrayList)
        Dim objPostedData As HttpPostedFile
        Dim sFileName As String
        Select Case e.CommandName
            Case "Add"
                Try
                    Dim FileUploadEvent As HtmlInputFile = CType(e.Item.FindControl("FileUploadEvent"), HtmlInputFile)
                    Dim txtFileName As TextBox = CType(e.Item.FindControl("txtFileName"), TextBox)
                    Dim oEDRD As New EventDealerDocument

                    If txtFileName.Text.Trim = String.Empty Then
                        MessageBox.Show("Nama Dokumen Masih Kosong")
                        Return
                    End If
                    If IsNothing(FileUploadEvent) OrElse FileUploadEvent.Value = String.Empty Then
                        MessageBox.Show("Kolom file masih kosong")
                        Return
                    End If
                    Dim _filename As String = System.IO.Path.GetFileName(FileUploadEvent.PostedFile.FileName)
                    If _filename.Trim().Length <= 0 Then
                        MessageBox.Show("Upload file belum diisi\n")
                        Return
                    End If
                    If _filename.Trim().Length > 0 Then
                        If FileUploadEvent.PostedFile.ContentLength > MAX_FILE_SIZE Then
                            MessageBox.Show("Ukuran file tidak boleh melebihi " & (MAX_FILE_SIZE / 1024) & "kb\n")
                            Return
                        End If
                    End If
                    Dim ext As String = System.IO.Path.GetExtension(FileUploadEvent.PostedFile.FileName)
                    If Not (ext.ToUpper() = ".JPG" OrElse ext.ToUpper() = ".JPEG" OrElse ext.ToUpper() = ".PDF" OrElse ext.ToUpper() = ".DOC" OrElse ext.ToUpper() = ".DOCX" OrElse ext.ToUpper() = ".XLS" OrElse ext.ToUpper() = ".XLSX") Then
                        MessageBox.Show("Hanya menerima file format (PDF/JPG/JPEG/DOC/DOCX/XLS/XLSX)")
                        Return
                    End If
                    If Not IsNothing(FileUploadEvent) OrElse FileUploadEvent.Value <> String.Empty Then
                        objPostedData = FileUploadEvent.PostedFile
                    Else
                        objPostedData = Nothing
                    End If

                    If Not (IsNothing(objPostedData)) Then
                        sFileName = objPostedData.FileName.Split("\")(objPostedData.FileName.Split("\").Length - 1)

                        If KTB.DNet.UI.Helper.FileHelper.IsExecutableFile(sFileName) Then
                            MessageBox.Show("Tidak diperkenankan mengupload file dengan ekstensi '.exe'. Pastikan file anda bebas dari virus.")
                            BindEventDealerRequiredDoc()
                            Return
                        End If

                        Dim SrcFile As String = Path.GetFileName(objPostedData.FileName) '-- Source file name
                        Dim strBabitPathConfig As String = KTB.DNet.Lib.WebConfig.GetValue("BabitFileDirectory")
                        Dim strBabitPathFile As String = "\EVENT\EventDealer\" & TimeStamp() & SrcFile.Substring(SrcFile.Length - 4)
                        Dim strDestFile As String = strBabitPathConfig & strBabitPathFile '--Destination File                       

                        oEDRD.FileName = sFileName
                        oEDRD.Path = strDestFile
                        oEDRD.AttachmentData = objPostedData
                        oEDRD.DocumentName = txtFileName.Text
                        oEDRD.EventDealerHeader = New EventDealerHeader

                        UploadAttachment(oEDRD, TempDirectory)

                        arrEventDealerDoc.Add(oEDRD)
                        sessHelper.SetSession(SessEventDealerDoc, arrEventDealerDoc)
                    Else
                        arrEventDealerDoc.Add(oEDRD)
                        sessHelper.SetSession(SessEventDealerDoc, arrEventDealerDoc)
                    End If

                Catch ex As Exception
                End Try

            Case "Edit"
                dgEventDealerDocument.ShowFooter = False
                dgEventDealerDocument.EditItemIndex = e.Item.ItemIndex

            Case "Delete"
                Dim arl As ArrayList = sessHelper.GetSession(SessEventDealerDoc)
                Try
                    Dim objEventDealerDocument As EventDealerDocument = CType(arl(e.Item.ItemIndex), EventDealerDocument)
                    If objEventDealerDocument.ID > 0 Then
                        Dim arrDelete As ArrayList = CType(sessHelper.GetSession(SessDeleteEventDealerDoc), ArrayList)
                        If IsNothing(arrDelete) Then arrDelete = New ArrayList
                        arrDelete.Add(objEventDealerDocument)
                        sessHelper.SetSession(SessDeleteEventDealerDoc, arrDelete)
                    End If
                    arl.RemoveAt(e.Item.ItemIndex)
                Catch
                End Try

            Case "Save"
                Dim oEDRD As EventDealerDocument = CType(sessHelper.GetSession(SessEventDealerDoc)(e.Item.ItemIndex), EventDealerDocument)
                Dim EditFileUploadEvent As HtmlInputFile = CType(e.Item.FindControl("EditFileUploadEvent"), HtmlInputFile)
                Dim EditlblFileName As TextBox = CType(e.Item.FindControl("EditlblFileName"), TextBox)
                Dim lblEditFileUploadEvent As Label = CType(e.Item.FindControl("lblEditFileUploadEvent"), Label)
                Dim SrcFile As String = ""

                If lblEditFileUploadEvent.Text = String.Empty Then
                    If EditlblFileName.Text.Trim = String.Empty Then
                        MessageBox.Show("Nama Dokumen Masih Kosong")
                        Return
                    End If

                    If IsNothing(EditFileUploadEvent) OrElse EditFileUploadEvent.Value = String.Empty Then
                        MessageBox.Show("Kolom file masih kosong")
                        Return
                    End If
                Else
                    If Not IsNothing(EditFileUploadEvent) OrElse EditFileUploadEvent.Value <> String.Empty Then
                        objPostedData = EditFileUploadEvent.PostedFile
                    Else
                        objPostedData = Nothing
                    End If

                    If Not (IsNothing(objPostedData)) AndAlso objPostedData.FileName <> "" Then
                        If lblEditFileUploadEvent.Text <> String.Empty Then
                        End If

                        sFileName = objPostedData.FileName.Split("\")(objPostedData.FileName.Split("\").Length - 1)

                        If KTB.DNet.UI.Helper.FileHelper.IsExecutableFile(sFileName) Then
                            MessageBox.Show("Tidak diperkenankan mengupload file dengan ekstensi '.exe'. Pastikan file anda bebas dari virus.")
                            BindEventDealerRequiredDoc()
                            Return
                        End If

                        SrcFile = Path.GetFileName(objPostedData.FileName) '-- Source file name
                        'Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory") & "\" & txtDealerCode.Text.Trim & TimeStamp() & SrcFile.Substring(SrcFile.Length - 4)  '-- Destination file
                    Else
                        SrcFile = lblEditFileUploadEvent.Text
                        sFileName = lblEditFileUploadEvent.Text
                    End If
                End If

                Dim strBabitPathConfig As String = KTB.DNet.Lib.WebConfig.GetValue("BabitFileDirectory")
                Dim strBabitPathFile As String = "\EVENT\EventDealer\" & TimeStamp() & SrcFile.Substring(SrcFile.Length - 4)
                Dim strDestFile As String = strBabitPathConfig & strBabitPathFile '--Destination File  

                oEDRD.FileName = sFileName
                oEDRD.Path = strDestFile
                oEDRD.AttachmentData = objPostedData
                oEDRD.DocumentName = EditlblFileName.Text

                arrEventDealerDoc(e.Item.ItemIndex) = oEDRD
                sessHelper.SetSession(SessEventDealerDoc, arrEventDealerDoc)
                dgEventDealerDocument.EditItemIndex = -1
                dgEventDealerDocument.ShowFooter = True

            Case "Download" 'Then Download File
                Response.Redirect("../Download.aspx?file=" & e.CommandArgument)

        End Select

        BindEventDealerDocument()
    End Sub

    Protected Sub dgEventDealerDocument_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgEventDealerDocument.ItemDataBound
        Dim owscDet As EventDealerDocument = CType(e.Item.DataItem, EventDealerDocument)
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblFileName As Label = CType(e.Item.FindControl("lblFileName"), Label)
            Dim lblFile As Label = CType(e.Item.FindControl("lblFile"), Label)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)

            lblFileName.Text = owscDet.DocumentName
            lblFile.Text = owscDet.FileName
            lblNo.Text = e.Item.ItemIndex + 1

        ElseIf e.Item.ItemType = ListItemType.EditItem Then
            Dim EditlblFileName As TextBox = CType(e.Item.FindControl("EditlblFileName"), TextBox)
            Dim lblEditFileUploadEvent As Label = CType(e.Item.FindControl("lblEditFileUploadEvent"), Label)
            lblEditFileUploadEvent.Text = owscDet.FileName
            EditlblFileName.Text = owscDet.DocumentName
        End If
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("~/Event/FrmEventDealerList.aspx")
    End Sub

End Class