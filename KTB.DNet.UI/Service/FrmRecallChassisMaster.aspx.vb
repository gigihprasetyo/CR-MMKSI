#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.IO
Imports System.Text
Imports Excel
#End Region


#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.Parser
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Service

#End Region
Public Class FrmRecallChassisMaster
    Inherits System.Web.UI.Page

#Region "variable"
    Private ReadOnly varSession As String = "sessfrmPresentationList"
    Private sesHelper As New SessionHelper
    Dim arlUserGroup As New ArrayList
    Dim IsKTB As Boolean
    Dim IsDelete As Boolean
    Dim IsRead As Boolean
    Dim arrChkSelectionID As ArrayList = New ArrayList

#End Region

#Region "Function"

    Private Sub CheckPriv()
        Dim objDealer As Dealer = CType(sesHelper.GetSession("DEALER"), Dealer)
        IsDelete = False
        IsRead = False

        If IsNothing(objDealer) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Service - Field Fix Campaign - Master Data Campaign")
        End If

        If Not SecurityProvider.Authorize(Context.User, SR.Recall_InputChassis_Privilege) Then
            IsDelete = False
            If Not SecurityProvider.Authorize(Context.User, SR.Recall_ListChassis_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Service - Field Fix Campaign - Master Data Campaign")
            Else
                IsRead = True
            End If


        Else
            IsDelete = True
            IsRead = True

        End If

        If IsDelete Then
            Dim lastIdex As Integer = dgRecallMaster.Columns.Count - 1
            dgRecallMaster.Columns(lastIdex).Visible = True
        Else
            btnStore.Enabled = False
            btnUpload.Enabled = False
            'add validasi privilege delete (privilage : input dan lihat chassis recall)
            Dim lastIdex As Integer = dgRecallMaster.Columns.Count - 1
            dgRecallMaster.Columns(lastIdex).Visible = False
        End If

    End Sub

    Private Sub CreateCriterias()
        Dim val As String
        Dim Sql As String = ""
        Dim criterias As New CriteriaComposite(New Criteria(GetType(RecallChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If txtChassisNumber.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(RecallChassisMaster), "ChassisNo", MatchType.[Partial], txtChassisNumber.Text.Trim()))
        End If

        If txtRecallRegNo.Text.Length > 0 Then
            criterias.opAnd(New Criteria(GetType(RecallChassisMaster), "RecallCategory.RecallRegNo", MatchType.InSet, "('" & txtRecallRegNo.Text.Replace(";", "','") & "')"))
        End If

        'If txtRecallRegNo.Text.Length > 0 Then
        '    criterias.opAnd(New Criteria(GetType(RecallChassisMaster), "RecallCategory.RecallRegNo", MatchType.[Partial], txtRecallRegNo.Text.Trim()))
        'End If
        If txtDealerAlokasi.Text.Length > 0 Then
            Dim dlr As Dealer = New KTB.DNet.BusinessFacade.General.DealerFacade(User).Retrieve(txtDealerAlokasi.Text.Trim())
            If Not IsNothing(dlr) Then
                Sql = " select distinct(ChassisNumber) from ChassisMaster cm INNER JOIN Dealer dl on dl.ID=cm.SoldDealerID "
                Sql &= " where cm.RowStatus = 0 and dl.DealerCode in('" & txtDealerAlokasi.Text.Replace(";", "','") & "')"
                Sql &= " union select distinct(ChassisNumber) from ChassisMasterBB cm INNER JOIN Dealer dl on dl.ID=cm.SoldDealerID "
                Sql &= " where cm.RowStatus = 0 and dl.DealerCode in('" & txtDealerAlokasi.Text.Replace(";", "','") & "')"
                criterias.opAnd(New Criteria(GetType(RecallChassisMaster), "ChassisNo", MatchType.InSet, "(" & Sql & ")"))
            End If

        End If

        If txtDealerService.Text.Length > 0 Then
            Dim dlr As Dealer = New KTB.DNet.BusinessFacade.General.DealerFacade(User).Retrieve(txtDealerService.Text.Trim())
            If Not IsNothing(dlr) Then
                Sql = " select distinct(RecallChassisMasterID) from RecallService rc INNER JOIN Dealer dl on dl.ID=rc.ServiceDealerID "
                Sql &= " where rc.RowStatus = 0 and dl.DealerCode in('" & txtDealerService.Text.Replace(";", "','") & "')"
                criterias.opAnd(New Criteria(GetType(RecallChassisMaster), "ID", MatchType.InSet, "(" & Sql & ")"))
            End If

        End If

        'add start rudi

        If ddlIsService.SelectedValue.ToString.Trim = "0" Then 'Sudah Service
            Sql = " select distinct(RecallChassisMasterID) from RecallService "
            Sql &= " where RowStatus = 0"
            criterias.opAnd(New Criteria(GetType(RecallChassisMaster), "ID", MatchType.InSet, "(" & Sql & ")"))
        ElseIf ddlIsService.SelectedValue.ToString.Trim = "1" Then 'Belum Service
            Sql = " select distinct(RecallChassisMasterID) from RecallService "
            Sql &= " where RowStatus = 0"
            criterias.opAnd(New Criteria(GetType(RecallChassisMaster), "ID", MatchType.NotInSet, "(" & Sql & ")"))
        Else
        End If
        'add end rudi

        sesHelper.SetSession("CRITERIASfrRecall", criterias)
    End Sub

    Private Sub BindDataGridMember(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        If (indexPage >= 0) Then

            Dim criterias As CriteriaComposite = sesHelper.GetSession("CRITERIASfrRecall")
            'New ForumMemberFacade(User).RetrieveByCriteria(criterias, 1, dtgForumMember.PageSize, totalRow, sHelper.GetSession("SortCol"), sHelper.GetSession("SortDirection"))
            arlUserGroup = New RecallChassisMasterFacade(User).RetrieveActiveList(indexPage, dgRecallMaster.PageSize, totalRow, ViewState("SortCol"), ViewState("SortDirection"), criterias)
            dgRecallMaster.DataSource = arlUserGroup
            dgRecallMaster.VirtualItemCount = totalRow
            dgRecallMaster.DataBind()

            sesHelper.SetSession("ArlRecallChasissMaster", arlUserGroup)
            If arlUserGroup.Count > 0 Then
                dgRecallMaster.Visible = True
                dgRecallMaster.DataSource = arlUserGroup
                dgRecallMaster.DataBind()

            Else
                dgRecallMaster.DataSource = New ArrayList
                dgRecallMaster.DataBind()
            End If
        End If
    End Sub

    Private Sub Upload()

        If (Not DataFile.PostedFile Is Nothing) AndAlso (DataFile.PostedFile.ContentLength > 0) Then

            Dim fileExt As String = Path.GetExtension(DataFile.PostedFile.FileName)
            If Not (fileExt.ToLower() = ".xls" OrElse fileExt.ToLower() = ".xlsx") Then
                MessageBox.Show("Hanya Menerima File Excell")
                Return
            End If

            dgRecallMaster.DataSource = New ArrayList
            dgRecallMaster.DataBind()
            dgRecallMaster.Visible = False

            Me.btnStore.Enabled = False
            Me.dgRecallUpload.Visible = True
            Me.dgRecallUpload.DataSource = New ArrayList
            Me.dgRecallUpload.DataBind()

            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate

            Try
                Dim SrcFile As String = Path.GetFileName(DataFile.PostedFile.FileName)
                Dim targetFile As String = Server.MapPath("") & "\..\DataTemp\" & DateTime.Now.ToString("dd/MM/yyy HHmmss") & SrcFile

                imp = New SAPImpersonate(_user, _password, _webServer)

                If imp.Start() Then
                    Dim objUpload As New UploadToWebServer
                    objUpload.Upload(DataFile.PostedFile.InputStream, targetFile)

                    Dim parser As IParser = New PriceParser  '-- Declare parser Price

                    '-- Parse data file and store result into list
                    '  Dim arList As ArrayList = CType(parser.ParseNoTransaction(targetFile, "User"), ArrayList)

                    Dim i As Integer = 0
                    Dim objReader As IExcelDataReader = Nothing
                    Dim ArrUpload As New ArrayList
                    Dim ArrUploadOK As New ArrayList


                    Using stream As FileStream = File.Open(targetFile, FileMode.Open, FileAccess.Read)

                        '   objReader = ExcelReaderFactory.CreateBinaryReader(stream)

                        If fileExt.ToLower.Contains("xlsx") Then
                            objReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
                        Else
                            objReader = ExcelReaderFactory.CreateBinaryReader(stream)
                        End If

                        If (Not IsNothing(objReader)) Then
                            While objReader.Read()

                                If i >= 4 Then
                                    Dim ObjRCM As New RecallChassisMaster
                                    Dim ObjFRecCM As RecallChassisMasterFacade = New RecallChassisMasterFacade(User)
                                    Dim ObjFReCa As RecallCategoryFacade = New RecallCategoryFacade(User)

                                    Dim objCM As New ChassisMaster
                                    Dim ObjRC As New RecallCategory

                                    Dim StrCHassis As String = objReader.GetString(1).Trim() ' Get Chassis
                                    Dim StrRecallRegNo As String = objReader.GetString(2).Trim() ' getRegCallNo

                                    ObjRCM = ObjFRecCM.Retrieve(StrCHassis, StrRecallRegNo)
                                    ObjRCM.RecallRegNo = StrRecallRegNo
                                    'RegREcalNo

                                    If StrRecallRegNo.Trim() = "" Then
                                        ObjRCM.ErrorMessage = "Field Fix No tidak terdaftar "
                                    Else
                                        If objCM.ID = 0 Then
                                            ObjRC = ObjFReCa.Retrieve(StrRecallRegNo.Trim())
                                            Try
                                                If Not IsNothing(ObjRC) AndAlso ObjRC.ID > 0 Then
                                                    ObjRCM.RecallCategory = ObjRC
                                                    ObjRCM.RecallCategory.MarkLoaded()
                                                Else
                                                    ObjRC.RecallRegNo = StrRecallRegNo
                                                    ObjRCM.RecallCategory = ObjRC
                                                    ObjRCM.RecallCategory.MarkLoaded()
                                                    ObjRCM.ErrorMessage = "Field Fix No tidak terdaftar"
                                                End If
                                            Catch ex As Exception
                                                ObjRCM.ErrorMessage = "Field Fix No tidak terdaftar"
                                            End Try

                                        End If

                                    End If





                                    'check chassisCategory
                                    Dim ObjChassis As New ChassisMaster
                                    Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")

                                    Dim ObjChassisFac As ChassisMasterFacade = New ChassisMasterFacade(User)

                                    ObjChassis = ObjChassisFac.Retrieve(StrCHassis)

                                    objCM = ObjChassisFac.Retrieve(StrCHassis)
                                    ObjRCM.ChassisNo = StrCHassis
                                    If Not IsNothing(objCM) AndAlso objCM.ID > 0 AndAlso objCM.Category.ProductCategory.Code <> companyCode Then
                                        ObjRCM.ErrorMessage = "Chassis Tidak Terdaftar di " + companyCode

                                        ObjRCM.ErrorMessage = ObjRCM.ErrorMessage & "; Chassis Tidak Terdaftar di " + IIf(companyCode.ToLower().Equals("mmc"), "MMKSI", "MFTBC")

                                        ObjRCM.ChassisNo = StrCHassis
                                        If StrCHassis <> "" Then
                                            ArrUpload.Add(ObjRCM)
                                        End If
                                    ElseIf ObjRCM.ID > 0 Then
                                        ObjRCM.ErrorMessage = "Data Sudah Ada"
                                        If StrCHassis <> "" Then
                                            ArrUpload.Add(ObjRCM)
                                        End If
                                    Else
                                        ObjRCM.ChassisNo = StrCHassis
                                        If StrCHassis <> "" AndAlso ObjRCM.ErrorMessage = "" Then
                                            ArrUpload.Add(ObjRCM)
                                            ArrUploadOK.Add(ObjRCM)
                                        Else
                                            ArrUpload.Add(ObjRCM)
                                        End If
                                    End If

                                End If
                                i = i + 1
                            End While
                        End If
                    End Using

                    dgRecallUpload.DataSource = ArrUpload
                    dgRecallUpload.DataBind()
                    dgRecallUpload.Visible = True

                    If ArrUploadOK.Count > 0 Then
                        sesHelper.SetSession("ArrUploadOK", ArrUploadOK)
                        btnStore.Enabled = True
                    End If
                End If


            Catch ex As Exception
                MessageBox.Show("Fail To Process")
            Finally

                imp.StopImpersonate()
                imp = Nothing
            End Try

        Else
            MessageBox.Show("Berkas Upload tidak boleh Kosong")
        End If

    End Sub


    Private Sub CommandDelete(ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)

        Try

            Dim arr As New ArrayList

            Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RecallService), "RecallChassisMaster.ID", CInt(e.Item.Cells(0).Text)))
            criterias2.opAnd(New Criteria(GetType(RecallService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            arr = New RecallServiceFacade(User).Retrieve(criterias2)

            If Not IsNothing(arr) AndAlso arr.Count > 0 Then
                MessageBox.Show(SR.DeleteFail & " Data sudah terpakai di data service")
                Return
            End If


            Dim ObjPresentattionFa As RecallChassisMasterFacade = New RecallChassisMasterFacade(User)
            Dim ObjPresentation As New RecallChassisMaster
            ObjPresentation = ObjPresentattionFa.Retrieve(CInt(e.Item.Cells(0).Text))

            ObjPresentation.RowStatus = DBRowStatus.Deleted
            ObjPresentattionFa.Update(ObjPresentation)

            MessageBox.Show(SR.DeleteSucces)
            BindDataGridMember(0)
        Catch ex As Exception
            MessageBox.Show(SR.DeleteFail)
        End Try


    End Sub

    'add start rudi
    Private Sub CommandSelectCheck(ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Try
            arrChkSelectionID = CType(sesHelper.GetSession("ArlRecallChasissMasterID"), ArrayList)
            Dim chkSelection As CheckBox = CType(e.Item.FindControl("chkSelection"), CheckBox)
            If chkSelection.Checked = True Then
                arrChkSelectionID.Add(CInt(e.Item.Cells(0).Text))
            Else
                arrChkSelectionID.Remove(CInt(e.Item.Cells(0).Text))
            End If
            sesHelper.SetSession("ArlRecallChasissMasterID", arrChkSelectionID)

        Catch ex As Exception
            MessageBox.Show("Error.")
        End Try
    End Sub
    'add end rudi

    Private Sub Insert()

        If Not IsNothing(sesHelper.GetSession("ArrUploadOK")) Then
            Dim ArrUploadOK As New ArrayList
            ArrUploadOK = CType(sesHelper.GetSession("ArrUploadOK"), ArrayList)

            For Each objRCM As RecallChassisMaster In ArrUploadOK
                Dim ObjF As RecallChassisMasterFacade = New RecallChassisMasterFacade(User)
                ObjF.Insert(objRCM)
            Next
            sesHelper.SetSession("ArrUploadOK", Nothing)
            MessageBox.Show(SR.SaveSuccess)
            Me.btnSearch_Click(Nothing, Nothing)
        End If
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckPriv()
        If Not IsPostBack Then
            ViewState("SortCol") = "ChassisNo"
            ViewState("SortDirection") = Sort.SortDirection.ASC
            sesHelper.SetSession("ArlRecallChasissMasterID", arrChkSelectionID)

            Me.dgRecallMaster.DataSource = New ArrayList
            Me.dgRecallMaster.DataBind()
            Me.dgRecallMaster.Visible = True

            Me.dgRecallUpload.DataSource = New ArrayList
            Me.dgRecallUpload.DataBind()
            Me.dgRecallUpload.Visible = False

            lblRecallRegNo.Attributes("onclick") = "ShowPPRecallCategorySelection();"

            Dim objDealer As Dealer = CType(sesHelper.GetSession("DEALER"), Dealer)
            If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                txtDealerAlokasi.Enabled = True
                txtDealerService.Enabled = True
                lblSearchDealerAlokasi.Attributes("onclick") = "ShowPPDealerSelection();"
                lblSearchDealerService.Attributes("onclick") = "ShowPPDealerSelectionService();"
            Else
                txtDealerAlokasi.Enabled = False
                txtDealerAlokasi.Text = objDealer.DealerCode
                lblSearchDealerAlokasi.Visible = False
                lblSearchDealerService.Attributes("onclick") = "ShowPPDealerSelectionService();"
            End If

        End If

    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        btnStore.Enabled = False
        dgRecallUpload.DataSource = New ArrayList
        dgRecallUpload.DataBind()
        dgRecallUpload.Visible = False

        Me.CreateCriterias()
        dgRecallMaster.CurrentPageIndex = 0
        Me.BindDataGridMember(0)
    End Sub

    Protected Sub btnStore_Click(sender As Object, e As EventArgs) Handles btnStore.Click
        Insert()

    End Sub

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click

        If Not Page.IsValid Then  '-- Postback validation
            Exit Sub
        End If

        Upload()
    End Sub

    Protected Sub LnkTemplate_Click(sender As Object, e As EventArgs) Handles LnkTemplate.Click
        Dim strName As String = "Templates-UploadMasterField FixCampaign.xlsx"
        Response.Redirect("../downloadlocal.aspx?file=DataFile\Recall\" & strName)
    End Sub

    Protected Sub dgRecallMaster_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgRecallMaster.PageIndexChanged

        dgRecallMaster.CurrentPageIndex = e.NewPageIndex
        BindDataGridMember(e.NewPageIndex + 1)

    End Sub

    Protected Sub dgRecallMaster_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgRecallMaster.ItemCommand

        Select Case e.CommandName.ToLower()
            Case "Delete".ToLower()
                CommandDelete(e)
            Case "SelectCheck".ToLower()
                CommandSelectCheck(e)
        End Select
    End Sub

    Protected Sub dgRecallMaster_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgRecallMaster.SortCommand

        If e.SortExpression = ViewState("SortCol") Then
            If ViewState("SortDirection") = Sort.SortDirection.ASC Then
                ViewState("SortDirection") = Sort.SortDirection.DESC
            Else
                ViewState("SortDirection") = Sort.SortDirection.ASC
            End If
        End If
        ViewState("SortCol") = e.SortExpression
        dgRecallMaster.SelectedIndex = -1
        'dtgPresentation.CurrentPageIndex = 0
        BindDataGridMember(dgRecallMaster.CurrentPageIndex)

    End Sub

    Protected Sub dgRecallMaster_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgRecallMaster.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = (e.Item.ItemIndex + 1 + (dgRecallMaster.CurrentPageIndex * dgRecallMaster.PageSize)).ToString

            Dim _lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)

            _lbtnDelete.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")

            If Not IsNothing(e.Item.DataItem) Then
                If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType.AlternatingItem Then
                    Dim cbIsService As CheckBox = CType(e.Item.FindControl("cbIsService"), CheckBox)
                    Dim objDomain As RecallChassisMaster = CType(e.Item.DataItem, RecallChassisMaster)
                    cbIsService.Checked = objDomain.IsService

                    Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
                    Dim chkSelection As CheckBox = CType(e.Item.FindControl("chkSelection"), CheckBox)
                    arrChkSelectionID = CType(sesHelper.GetSession("ArlRecallChasissMasterID"), ArrayList)
                    Try
                        If lblID.Text <> String.Empty Then
                            For Each itemID As String In arrChkSelectionID
                                If itemID.Trim = lblID.Text Then
                                    chkSelection.Checked = True
                                    Exit For
                                End If
                            Next
                        End If
                    Catch ex As Exception
                    End Try

                    Dim lblDealerAlokasi As Label = CType(e.Item.FindControl("lblDealerAlokasi"), Label)
                    Dim lblDealerService As Label = CType(e.Item.FindControl("lblDealerService"), Label)

                    Dim objRecall As RecallService = New RecallServiceFacade(User).RetrieveByRM(objDomain.ID)
                    If objRecall.ID > 0 And Not IsNothing(objRecall) Then
                        lblDealerService.Text = objRecall.Dealer.DealerCode & "-" & objRecall.Dealer.SearchTerm1
                    Else
                        lblDealerService.Text = ""
                    End If

                    Dim objChasis As ChassisMaster = New ChassisMasterFacade(User).Retrieve(objDomain.ChassisNo)
                    If objChasis.ID > 0 And Not IsNothing(objChasis) Then
                        lblDealerAlokasi.Text = objChasis.Dealer.DealerCode & "-" & objChasis.Dealer.SearchTerm1
                    Else
                        Dim objChasisBB As ChassisMasterBB = New ChassisMasterBBFacade(User).Retrieve(objDomain.ChassisNo)
                        If objChasisBB.ID > 0 And Not IsNothing(objChasisBB) Then
                            lblDealerAlokasi.Text = objChasisBB.Dealer.DealerCode & "-" & objChasisBB.Dealer.SearchTerm1
                        Else
                            lblDealerAlokasi.Text = ""
                        End If

                    End If

                End If
            End If
        End If
    End Sub

    Private Sub dgRecallUpload_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgRecallUpload.ItemDataBound
        If e.Item.ItemIndex <> -1 Then
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1


        End If
    End Sub

    Protected Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        ViewState("SortCol") = "ChassisNo"
        ViewState("SortDirection") = Sort.SortDirection.ASC

        Me.dgRecallMaster.DataSource = New ArrayList
        Me.dgRecallMaster.VirtualItemCount = 0
        Me.dgRecallMaster.DataBind()
        Me.dgRecallMaster.Visible = True

        Me.dgRecallUpload.DataSource = New ArrayList
        Me.dgRecallUpload.DataBind()
        Me.dgRecallUpload.Visible = False

        sesHelper.SetSession("ArrUploadOK", Nothing)
        sesHelper.SetSession("ArlRecallChasissMaster", New ArrayList)
        txtChassisNumber.Text = ""
        txtRecallRegNo.Text = ""
    End Sub

    Private Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        lblLoading.Text = "Proses download. Mohon tunggu sebentar..."
        btnDownload.Enabled = False
        SetDownload()
        lblLoading.Text = ""
        btnDownload.Enabled = True
    End Sub

    Private Sub btnDownloadAll_Click(sender As Object, e As EventArgs) Handles btnDownloadAll.Click
        lblLoadingAll.Text = "Proses download. Mohon tunggu sebentar..."
        btnDownloadAll.Enabled = False
        SetDownloadAll()
        lblLoadingAll.Text = ""
        btnDownloadAll.Enabled = True
    End Sub

    Private Sub SetDownloadAll()
        'add start rudi
        Dim arrData As ArrayList
        ' mengambil data sesuai criteria
        'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RecallChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim criterias As CriteriaComposite = sesHelper.GetSession("CRITERIASfrRecall")
        If dgRecallMaster.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If
        arrData = New RecallChassisMasterFacade(User).Retrieve(criterias)
        If arrData.Count > 0 Then
            DoDownloadAll(arrData)
        End If
    End Sub

    Private Sub SetDownload()
        'add start rudi
        Dim arrData As ArrayList
        Dim strCH_id As String = String.Empty
        If dgRecallMaster.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        Else
            arrChkSelectionID = CType(sesHelper.GetSession("ArlRecallChasissMasterID"), ArrayList)
            For Each strIDNew As String In arrChkSelectionID
                If strCH_id = "" Then
                    strCH_id = strIDNew
                Else
                    strCH_id = strCH_id & ";" & strIDNew
                End If
            Next
        End If

        ' mengambil data sesuai criteria
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(RecallChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If strCH_id.Trim = "" Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        Else
            criterias.opAnd(New Criteria(GetType(RecallChassisMaster), "ID", MatchType.InSet, CommonFunction.GetStrValue(strCH_id, ";", ",")))
        End If
        'add end rudi

        arrData = New RecallChassisMasterFacade(User).Retrieve(criterias)
        If arrData.Count > 0 Then
            DoDownload(arrData)
        End If
    End Sub

    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String
        sFileName = "FixCampaign_" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond

        Dim RekapClaimData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(RekapClaimData)
                If finfo.Exists Then
                    finfo.Delete()
                End If

                Dim fs As FileStream = New FileStream(RekapClaimData, FileMode.CreateNew)
                Dim sw As StreamWriter = New StreamWriter(fs)
                WriteCampaignData(sw, data)
                sw.Close()
                fs.Close()
                imp.StopImpersonate()
                imp = Nothing
            End If

            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")
        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub

    Private Sub WriteCampaignData(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder

        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Master Data Campaign")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("No" & tab)
            itemLine.Append("Nomor Rangka" & tab)
            itemLine.Append("Field Fix Campaign" & tab)
            itemLine.Append("Nomor Buletin" & tab)
            itemLine.Append("Delaer Alokasi" & tab)
            itemLine.Append("Dealer Service" & tab)
            'add start rudi
            itemLine.Append("Status Service " & tab)
            'add end rudi
            sw.WriteLine(itemLine.ToString())

            Dim i As Integer = 1
            For Each item As RecallChassisMaster In data
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(i.ToString & tab)
                itemLine.Append(item.ChassisNo & tab)
                itemLine.Append(item.RecallCategory.RecallRegNo & tab)
                itemLine.Append(item.RecallCategory.BuletinDescription & tab)

                Dim strSQLs As String =
                " SELECT DealerCode + '-'+ SearchTerm1 as [DealerCode] From Dealer " +
                " WHERE ID = ( " +
                " Select SoldDealerId from ChassisMaster " +
                " WHERE RowStatus = 0 and ChassisNumber = '" & item.ChassisNo & "' " +
                " UNION Select SoldDealerId from ChassisMasterBB " +
                " WHERE RowStatus = 0 and ChassisNumber = '" & item.ChassisNo & "' ) " +
                " AND Dealer.RowStatus = 0 "

                Dim dlrs As DataTable = New DealerFacade(User).RetrieveUsingSP(strSQLs)
                If dlrs.Rows.Count > 0 Then
                    For Each dlCodes As DataRow In dlrs.Rows

                        If dlCodes("DealerCode").ToString <> "" Then
                            itemLine.Append(dlCodes("DealerCode").ToString & tab)
                        Else
                            itemLine.Append("" & tab)
                        End If
                    Next
                Else
                    itemLine.Append("" & tab)
                End If

                Dim strSQL As String =
                " SELECT DealerCode + '-'+ SearchTerm1 as [DealerCode] From Dealer " +
                " WHERE ID = ( " +
                " Select a.ServiceDealerId from RecallService a INNER JOIN ChassisMaster b on a.ChassisMasterID=b.id" +
                " INNER JOIN RecallChassisMaster c on a.RecallChassisMasterID=c.ID " +
                " WHERE a.RowStatus = 0 and b.ChassisNumber = '" & item.ChassisNo & "' and a.RecallChassisMasterID= '" & item.ID & "' " +
                " UNION Select a.ServiceDealerId from RecallService a INNER JOIN ChassisMasterBB b on a.ChassisMasterID=b.id" +
                " INNER JOIN RecallChassisMaster c on a.RecallChassisMasterID=c.ID " +
                " WHERE a.RowStatus = 0 and b.ChassisNumber = '" & item.ChassisNo & "' and a.RecallChassisMasterID= '" & item.ID & "' ) " +
                " AND Dealer.RowStatus = 0 "

                Dim dlr As DataTable = New DealerFacade(User).RetrieveUsingSP(strSQL)
                If dlr.Rows.Count > 0 Then
                    For Each dlCode As DataRow In dlr.Rows

                        If dlCode("DealerCode").ToString <> "" Then
                            itemLine.Append(dlCode("DealerCode").ToString & tab)
                        Else
                            itemLine.Append("" & tab)
                        End If
                    Next
                Else
                    itemLine.Append("" & tab)
                End If


                'add start rudi
                If CType(item.IsService, Boolean) = False Then
                    itemLine.Append("Belum Service" & tab)
                Else
                    itemLine.Append("Sudah Service" & tab)
                End If
                'add end rudi

                sw.WriteLine(itemLine.ToString())
                i = i + 1
            Next

        End If
    End Sub

    Private Sub DoDownloadAll(ByVal data As ArrayList)
        Dim sFileName As String
        sFileName = "FixCampaign_" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond

        Dim RekapClaimData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(RekapClaimData)
                If finfo.Exists Then
                    finfo.Delete()
                End If

                Dim fs As FileStream = New FileStream(RekapClaimData, FileMode.CreateNew)
                Dim sw As StreamWriter = New StreamWriter(fs)
                WriteCampaignDataAll(sw, data)
                sw.Close()
                fs.Close()
                imp.StopImpersonate()
                imp = Nothing
            End If

            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")
        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub

    Private Sub WriteCampaignDataAll(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)
        Dim itemLine As StringBuilder = New StringBuilder

        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("Master Data Campaign")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("No" & tab)
            itemLine.Append("Nomor Rangka" & tab)
            itemLine.Append("Field Fix Campaign" & tab)
            itemLine.Append("Nomor Buletin" & tab)
            itemLine.Append("Delaer Alokasi" & tab)
            itemLine.Append("Dealer Service" & tab)
            'add start rudi
            itemLine.Append("Status Service " & tab)
            'add end rudi
            sw.WriteLine(itemLine.ToString())
            Dim sts As String = ""
            If ddlIsService.SelectedValue <> -1 Then
                sts = ddlIsService.SelectedValue
            End If
            Dim sql As String = "EXEC sp_getalldownload_mnmasterdatacampaign '" + txtDealerAlokasi.Text + "','" + txtDealerService.Text + "','" + txtRecallRegNo.Text + "','" + txtChassisNumber.Text + "','" + sts + "'"
            'Dim dtSet As DataSet = New RecallChassisMasterFacade(User).RetrieveSp(sql)
            Dim arrDataAll As DataTable = New RecallChassisMasterFacade(User).RetrieveSp(sql)
            If arrDataAll.Rows.Count > 0 Then
                Dim i As Integer = 1
                For Each item As DataRow In arrDataAll.Rows
                    itemLine.Remove(0, itemLine.Length)
                    itemLine.Append(i.ToString & tab)
                    itemLine.Append(item("No_Rangka").ToString & tab)
                    itemLine.Append(item("FieldFixCampaignNo").ToString & tab)
                    itemLine.Append(item("No_Buletin").ToString & tab)
                    itemLine.Append(item("DealerAlokasi").ToString & tab)
                    itemLine.Append(item("DealerService").ToString & tab)
                    'itemLine.Append(item("IsService").ToString & tab)
                    If item("IsService").ToString = 1 Then
                        itemLine.Append("Belum Service" & tab)
                    Else
                        itemLine.Append("Sudah Service" & tab)
                    End If

                    sw.WriteLine(itemLine.ToString())
                    i = i + 1
                Next
            End If

        End If
    End Sub

    Protected Sub chkSelection_CheckedChanged(sender As Object, e As EventArgs)
        Dim chk As CheckBox = CType(sender, CheckBox)
        Dim dgItem As DataGridItem = CType(chk.NamingContainer, DataGridItem)
        Dim lblID As Label = CType(dgItem.FindControl("lblID"), Label)
        Dim strID As String = lblID.Text

        arrChkSelectionID = CType(sesHelper.GetSession("ArlRecallChasissMasterID"), ArrayList)
        If chk.Checked = True Then
            arrChkSelectionID.Add(strID)
        Else
            arrChkSelectionID.Remove(strID)
        End If
        sesHelper.SetSession("ArlRecallChasissMasterID", arrChkSelectionID)
    End Sub

    
End Class