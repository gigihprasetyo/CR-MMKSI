Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.BusinessFacade.Claim
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports System.IO
Imports System.Globalization


Public Class FrmEntryClaim
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ltrDealerCode As System.Web.UI.WebControls.Literal
    Protected WithEvents txtComment As System.Web.UI.WebControls.TextBox
    Protected WithEvents ltrDealerName As System.Web.UI.WebControls.Literal
    Protected WithEvents txtClaimNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents ltrStatus As System.Web.UI.WebControls.Literal
    Protected WithEvents dtgEntryClaim As System.Web.UI.WebControls.DataGrid
    Protected WithEvents drpFCondition As System.Web.UI.WebControls.DropDownList
    Protected WithEvents drpFReasonClaim As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents icClaimDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblTglFaktur As System.Web.UI.WebControls.Label
    Protected WithEvents txtNoFaktur As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSearch As System.Web.UI.WebControls.Label
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents fuEvidence As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents ddlReasonClaimHeader As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblSyarat As System.Web.UI.WebControls.Label
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents lblFilename As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoSO As System.Web.UI.WebControls.Label
    Protected WithEvents HPONumber As System.Web.UI.WebControls.HiddenField
    Protected WithEvents ErrorMessage As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents FailureText As System.Web.UI.WebControls.Literal
    Protected WithEvents dgEvidence As System.Web.UI.WebControls.DataGrid
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

    Dim sesHelper As SessionHelper = New SessionHelper
    Dim oDealer As Dealer
    Dim SPPo As SparePartPOStatus = New SparePartPOStatus
    Dim SPPoDetails As ArrayList = New ArrayList
    Dim count As Integer = 0

    Private objClaimHeader As ClaimHeader
    Private objClaimDetail As ClaimDetail
    Private Mode As enumMode.Mode
    Dim oLoginUser As New UserInfo
    Private TargetDirectory As String
    Private TempDirectory As String
#End Region

#Region "Custom Method"

    Private Function GetLimitDate(ByVal objPO As SparePartPOStatus) As Date

        Dim estArrival As Integer = 0
        Dim reasonLimit As Integer = 0
        Dim fakturdate As Date = objPO.BillingDate
        Dim objDealerx As Dealer = CType(sesHelper.GetSession("LOGINUSERINFO"), UserInfo).Dealer

        Dim objDealer As Dealer = New KTB.DNet.BusinessFacade.General.DealerFacade(User).Retrieve(CInt(objDealerx.ID))

        If objDealer.DealerAdditionals.Count > 0 Then
            estArrival = CDbl(CType(objDealer.DealerAdditionals(0), DealerAdditional).ClaimETA)
        End If

        Dim objReason As ClaimReason = New ClaimReasonFacade(User).Retrieve(CInt(ddlReasonClaimHeader.SelectedValue))
        reasonLimit = objReason.TimeLimit

        Return objPO.BillingDate.AddDays(estArrival + reasonLimit)

    End Function

    Sub fillDataDealer(ByVal oD As Dealer)
        ltrDealerCode.Text = String.Format("{0} / {1}", oD.DealerCode.ToString(), oD.SearchTerm2)
        ltrDealerName.Text = oD.DealerName
        ltrStatus.Text = "Baru"
    End Sub

    Sub BindToGrid()
        Dim total As Integer = 0
        'SPPo = New SparePartPOStatusFacade(User).Retrieve(txtNoFaktur.Text, lblNoSO.Text)
        SPPo = New SparePartPOStatusFacade(User).RetrieveSO(txtNoFaktur.Text, lblNoSO.Text)
        lblTglFaktur.Text = Format(SPPo.BillingDate, "dd/MM/yyyy")
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartPOStatusDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPOStatusDetail), "SparePartPOStatus.ID", MatchType.Exact, SPPo.ID.ToString()))

        Dim objUser As UserInfo = CType(sesHelper.GetSession("LOGINUSERINFO"), UserInfo)

        'If objUser.Dealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPOStatusDetail), "SparePartPOStatus.SparePartPO.Dealer.ID", MatchType.Exact, objUser.Dealer.ID.ToString()))
        'End If


        'Dim obj As SparePartPOStatusDetail = New SparePartPOStatusDetail
        'obj.SparePartPOStatus.SparePartPO.Dealer.ID

        SPPoDetails = New SparePart.SparePartPOStatusDetailFacade(User).RetrieveActiveList(criterias, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dtgEntryClaim.DataSource = SPPoDetails
        dtgEntryClaim.DataBind()
    End Sub

    Sub BindCondition(ByVal ddl As DropDownList)
        Dim oClaimCondition As ArrayList
        oClaimCondition = New Claim.ClaimGoodConditionFacade(User).RetrieveActiveList()
        ddl.DataTextField = "Condition"
        ddl.DataValueField = "ID"
        ddl.DataSource = oClaimCondition
        ddl.DataBind()
        ddl.Items.Insert(0, New ListItem("Silakan Pilih", "0"))

    End Sub

    Sub BindReasonHeader(ByVal ddl As DropDownList)
        Dim oClaimReason As ArrayList
        oClaimReason = New Claim.ClaimReasonFacade(User).RetrieveActiveListHeader()
        ddl.DataTextField = "Reason"
        ddl.DataValueField = "ID"
        ddl.DataSource = oClaimReason
        ddl.DataBind()
        ddl.Items.Insert(0, New ListItem("Silakan Pilih", "0"))

    End Sub

    Sub BindReasonDetail(ByVal ddl As DropDownList)
        Dim oClaimReasons As ClaimReason
        Dim oClaimReasonAdd As ArrayList = New ArrayList
        Dim oClaimReason As ArrayList
        oClaimReason = New Claim.ClaimReasonFacade(User).RetrieveActiveListDetail()
        For Each CR As ClaimReason In oClaimReason
            oClaimReasons = New ClaimReason
            oClaimReasons.ID = CR.ID
            oClaimReasons.Reason = CR.Reason & " - " & CR.Prerequisite
            oClaimReasonAdd.Add(oClaimReasons)
        Next
        ddl.DataTextField = "Reason"
        ddl.DataValueField = "ID"
        ddl.DataSource = oClaimReasonAdd
        ddl.DataBind()
        ddl.Items.Insert(0, New ListItem("Silakan Pilih", "0"))

    End Sub
    'Private Sub GetFileClaimHeader(ByVal status As Boolean)
    '    Dim ext As String = ""
    '    Dim Rnd As Random = New Random
    '    Dim RndVal As String = Rnd.Next()
    '    Dim fileName As String = String.Empty
    '    fileName = DateTime.Now.ToString("ddMMyyyyHHmmss") & RndVal & "/" & Path.GetFileName(fuEvidence.PostedFile.FileName)
    '    fileName = String.Format("{0}{1}{2}", fileName, "", ext)


    '    If fileName <> "" OrElse fileName <> Nothing Then
    '        'cek filesize first
    '        Dim maxFileSize As Integer = CDbl(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))
    '        If fuEvidence.PostedFile.ContentLength > maxFileSize Then
    '            MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
    '            Exit Sub
    '        Else
    '            'Dim SrcFile As String = Path.GetFileName(fuEvidence.PostedFile.FileName)  '-- Source file name
    '            Dim DestFile As String
    '            If status Then
    '                DestFile = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("ClaimEvidenceDir") & "\" & lblFilename.Text   '-- Destination file
    '            Else
    '                DestFile = KTB.DNet.Lib.WebConfig.GetValue("SAN") & "DataTemp\CE" & "\" & fileName   '-- Destination file
    '            End If
    '            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
    '            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
    '            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
    '            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
    '            Dim success As Boolean = False
    '            Dim finfo As New FileInfo(DestFile)
    '            Try
    '                success = imp.Start()
    '                If success Then
    '                    If Not finfo.Directory.Exists Then
    '                        Directory.CreateDirectory(finfo.DirectoryName)
    '                    End If
    '                    If sesHelper.GetSession("UploadFile") Is Nothing Then
    '                        fuEvidence.PostedFile.SaveAs(DestFile)
    '                        sesHelper.SetSession("UploadFile", DestFile)
    '                        lblFilename.Text = fileName
    '                    Else
    '                        Dim old As String = CType(sesHelper.GetSession("UploadFile"), String)
    '                        If status Then
    '                            File.Copy(old, DestFile)
    '                            File.Delete(CType(sesHelper.GetSession("UploadFile"), String))
    '                            Directory.Delete(Path.GetDirectoryName(old))
    '                        Else
    '                            fuEvidence.PostedFile.SaveAs(DestFile)
    '                            File.Delete(CType(sesHelper.GetSession("UploadFile"), String))
    '                            Directory.Delete(Path.GetDirectoryName(old))
    '                            sesHelper.SetSession("UploadFile", DestFile)
    '                            lblFilename.Text = fileName
    '                        End If
    '                    End If
    '                    If status Then
    '                        sesHelper.RemoveSession("UploadFile")
    '                    End If

    '                    imp.StopImpersonate()
    '                    imp = Nothing
    '                End If
    '            Catch ex As Exception
    '                Throw ex
    '                Exit Sub
    '            End Try
    '        End If
    '    End If
    'End Sub
    Private Sub GetFileClaimHeader(ByVal fuEvidence As System.Web.UI.HtmlControls.HtmlInputFile, ByVal status As Boolean)
        Dim ext As String = ""
        Dim Rnd As Random = New Random
        Dim RndVal As String = Rnd.Next()
        Dim fileName As String = String.Empty
        fileName = DateTime.Now.ToString("ddMMyyyyHHmmss") & RndVal & "/" & Path.GetFileName(fuEvidence.PostedFile.FileName)
        fileName = String.Format("{0}{1}{2}", fileName, "", ext)


        If fileName <> "" OrElse fileName <> Nothing Then
            'cek filesize first
            Dim maxFileSize As Integer = CDbl(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))
            If fuEvidence.PostedFile.ContentLength > maxFileSize Then
                MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                Exit Sub
            Else
                'Dim SrcFile As String = Path.GetFileName(fuEvidence.PostedFile.FileName)  '-- Source file name
                Dim DestFile As String
                If status Then
                    DestFile = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("ClaimEvidenceDir") & "\" & lblFilename.Text   '-- Destination file
                Else
                    DestFile = KTB.DNet.Lib.WebConfig.GetValue("SAN") & "DataTemp\CE" & "\" & fileName   '-- Destination file
                End If
                Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
                Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
                Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
                Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                Dim success As Boolean = False
                Dim finfo As New FileInfo(DestFile)
                Try
                    success = imp.Start()
                    If success Then
                        If Not finfo.Directory.Exists Then
                            Directory.CreateDirectory(finfo.DirectoryName)
                        End If
                        If sesHelper.GetSession("UploadFile") Is Nothing Then
                            fuEvidence.PostedFile.SaveAs(DestFile)
                            sesHelper.SetSession("UploadFile", DestFile)
                            lblFilename.Text = fileName
                        Else
                            Dim old As String = CType(sesHelper.GetSession("UploadFile"), String)
                            If status Then
                                File.Copy(old, DestFile)
                                File.Delete(CType(sesHelper.GetSession("UploadFile"), String))
                                Directory.Delete(Path.GetDirectoryName(old))
                            Else
                                fuEvidence.PostedFile.SaveAs(DestFile)
                                File.Delete(CType(sesHelper.GetSession("UploadFile"), String))
                                Directory.Delete(Path.GetDirectoryName(old))
                                sesHelper.SetSession("UploadFile", DestFile)
                                lblFilename.Text = fileName
                            End If
                        End If

                        If status Then
                            sesHelper.RemoveSession("UploadFile")
                        End If
                    End If
                Catch ex As Exception
                    Throw ex
                    Exit Sub
                End Try
            End If
        End If
    End Sub
    Private Function GetClaimHeader() As ClaimHeader
        Dim objCH As ClaimHeader = New ClaimHeader
        'remark by willy masalah claim date
        'objCH.ClaimDate = icClaimDate.Value
        objCH.SparePartPOStatus = CType(Session("SparePartPOStatus"), SparePartPOStatus)
        objCH.Description = Server.HtmlEncode(txtComment.Text.Trim())
        objCH.Status = EnumClaimStatus.ClaimStatus.Baru
        Dim CR As New ClaimReason
        CR = New Claim.ClaimReasonFacade(User).Retrieve(Convert.ToInt32(ddlReasonClaimHeader.SelectedValue))
        objCH.ClaimReason = CR
        objCH.Dealer = oDealer

        Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimProgress), "Progress", MatchType.Exact, ""))
        objCH.ClaimProgress = New ClaimProgressFacade(User).Retrieve(crits)(0)
        objCH.UploadFileName = lblFilename.Text


        'Dim ext As String = ""
        'Dim Rnd As Random = New Random
        'Dim RndVal As String = Rnd.Next()
        'Dim fileName As String = DateTime.Now.ToString("ddMMyyyyHHmmss") & RndVal & "/" & Path.GetFileName(fuEvidence.PostedFile.FileName)

        'If fuEvidence.Value <> "" OrElse fuEvidence.Value <> Nothing Then
        '    'ext = Path.GetExtension(fuEvidence.PostedFile.FileName)
        '    Dim finfo2 As New FileInfo(fuEvidence.PostedFile.FileName)
        '    objCH.UploadFileName = String.Format("{0}{1}{2}", fileName, "", ext)
        'End If

        'If fuEvidence.Value <> "" OrElse fuEvidence.Value <> Nothing Then
        '    'cek filesize first
        '    Dim maxFileSize As Integer = CDbl(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))
        '    If fuEvidence.PostedFile.ContentLength > maxFileSize Then
        '        MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
        '        Exit Function
        '    Else
        '        'Dim SrcFile As String = Path.GetFileName(fuEvidence.PostedFile.FileName)  '-- Source file name
        '        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("ClaimEvidenceDir") & "\" & String.Format("{0}{1}{2}", fileName, "", ext)   '-- Destination file
        '        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        '        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        '        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        '        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        '        Dim success As Boolean = False
        '        Dim finfo As New FileInfo(DestFile)
        '        Try
        '            success = imp.Start()
        '            If success Then
        '                If Not finfo.Directory.Exists Then
        '                    Directory.CreateDirectory(finfo.DirectoryName)
        '                End If
        '                fuEvidence.PostedFile.SaveAs(DestFile)
        '            End If
        '        Catch ex As Exception
        '            Throw ex
        '            Exit Function
        '        End Try
        '    End If
        'End If
        Session.Remove("SparePartPOStatus")
        Return objCH
    End Function

    Private Sub SetClaimHeaderByInputedData()
        'Dim objCH As ClaimHeader = New ClaimHeader
        ' Dim objSparePartPO As SparePartPOStatus = New SparePart.SparePartPOStatusFacade(User).Retrieve(txtNoFaktur.Text.Trim())
        Dim objSparePartPO As SparePartPOStatus = New SparePart.SparePartPOStatusFacade(User).RetrieveSO(txtNoFaktur.Text, lblNoSO.Text)

        'remark by willy masalah claim
        'remark opened by Ikhsan
        '25 June 2008
        '----------------------------------------------
        objClaimHeader.ClaimDate = icClaimDate.Value
        '----------------------------------------------
        objClaimHeader.SparePartPOStatus = objSparePartPO
        objClaimHeader.Description = Server.HtmlEncode(txtComment.Text.Trim())
        objClaimHeader.Status = EnumClaimStatus.ClaimStatus.Baru
        Dim CR As New ClaimReason
        CR = New Claim.ClaimReasonFacade(User).Retrieve(Convert.ToInt32(ddlReasonClaimHeader.SelectedValue))
        objClaimHeader.ClaimReason = CR
        objClaimHeader.Dealer = oDealer

        Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ClaimProgress), "Progress", MatchType.Exact, ""))
        objClaimHeader.ClaimProgress = New ClaimProgressFacade(User).Retrieve(crits)(0)
        objClaimHeader.UploadFileName = lblFilename.Text

    End Sub


    Private Sub ModeButton(ByVal mode As Boolean)
        btnSave.Visible = mode
        btnCancel.Visible = mode
    End Sub

    Private Function SaveToArrayList() As ArrayList
        Dim _arlClaimDetails As ArrayList = New ArrayList
        Dim i As Integer = 0
        For Each item As DataGridItem In dtgEntryClaim.Items
            '            Dim chkItemChecked As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            Dim txtQtyClaim As TextBox = CType(item.FindControl("txtQtyClaim"), TextBox)
            If txtQtyClaim.Text <> "" Then
                count = count + 1
                If (Val(txtQtyClaim.Text) > 0) Then
                    Dim lblHargaSatuan As Label = CType(item.FindControl("lblHargaSatuan"), Label)
                    Dim lblTotal As Label = CType(item.FindControl("lblTotal"), Label)
                    lblTotal.Text = CStr(Val(txtQtyClaim.Text) * Val(lblHargaSatuan.Text.Replace(".", "")))

                    Dim txtKeterangan As TextBox = CType(item.FindControl("txtKeterangan"), TextBox)

                    Dim myculture As CultureInfo = New CultureInfo("ID-id")
                    lblTotal.Text = CDbl(lblTotal.Text).ToString("#,#", myculture)

                    Dim objClaimDetails As ClaimDetail = New ClaimDetail
                    Dim objSparePartPO As SparePartPOStatus
                    Dim objSpMaster As ArrayList

                    Dim NoBarang As Label = CType(item.FindControl("lblNoBarang"), Label)



                    'objSparePartPO = New SparePart.SparePartPOStatusFacade(User).Retrieve(txtNoFaktur.Text.Trim())
                    objSparePartPO = New SparePart.SparePartPOStatusFacade(User).RetrieveSO(txtNoFaktur.Text.Trim(), lblNoSO.Text.Trim())

                    'Todo session
                    Session("SparePartPOStatus") = objSparePartPO

                    Dim oClaimGood As ClaimGoodCondition = New Claim.ClaimGoodConditionFacade(User).Retrieve("")

                    Dim oSPDetails As SparePartPOStatusDetail = New SparePartPOStatusDetail
                    oSPDetails = New SparePart.SparePartPOStatusDetailFacade(User).Retrieve(Convert.ToInt32(dtgEntryClaim.DataKeys.Item(i)))

                    objClaimDetails.Qty = 0
                    objClaimDetails.SparePartPOStatusDetail = oSPDetails
                    objClaimDetails.ClaimGoodCondition = oClaimGood
                    objClaimDetails.Keterangan = txtKeterangan.Text

                    If (txtQtyClaim.Text.Trim() <> String.Empty Or txtQtyClaim.Text <> "") Then
                        If Convert.ToInt32(ddlReasonClaimHeader.SelectedValue) <> 3 Then
                            'If (Convert.ToInt32(txtQtyClaim.Text) > (objClaimDetails.SparePartPOStatusDetail.BillingQuantity - objClaimDetails.SparePartPOStatusDetail.GetClaimedQty(0))) Then
                            '    'MessageBox.Show("Item " & i + 1 & " : Quantity Claim telah melampaui batas yang diperbolehkan")
                            '    'Return New ArrayList
                            'End If
                        End If
                    Else
                        MessageBox.Show("Qty claim harus diisi")
                        Return New ArrayList
                    End If

                    objClaimDetails.Qty = txtQtyClaim.Text
                    'Bug 1112 ApprovedQty = 0
                    objClaimDetails.ApprovedQty = 0

                    _arlClaimDetails.Add(objClaimDetails)
                End If
            End If
            i = i + 1
        Next
        Return _arlClaimDetails
    End Function

    Private Sub BindDataToPage()
        If IsNothing(sesHelper.GetSession("PartEntryClaim")) Then
            objClaimHeader = New KTB.DNet.Domain.ClaimHeader
            'objClaimHeader.KTBStatus = PartIncidentalStatus.PartIncidentalKTBStatusEnum.Baru
            sesHelper.SetSession("PartEntryClaim", objClaimHeader)
            ClearAllFields()
        Else
            objClaimHeader = sesHelper.GetSession("PartEntryClaim")
            Mode = ViewState("Mode")
            If (Mode = enumMode.Mode.EditMode) Then
                BindHeaderToForm()
            End If
        End If
        BindDetailToGrid()
    End Sub

    Private Sub ClearAllFields()
        txtNoFaktur.Text = String.Empty
        txtComment.Text = String.Empty
        txtClaimNo.Text = String.Empty
        lblNoSO.Text = String.Empty
        dtgEntryClaim.DataSource = Nothing
        dtgEntryClaim.DataBind()
    End Sub

    Private Sub BindHeaderToForm()
        objClaimHeader = sesHelper.GetSession("PartEntryClaim")
        txtClaimNo.Text = objClaimHeader.ClaimNo
        txtComment.Text = objClaimHeader.Description
        txtNoFaktur.Text = sesHelper.GetSession("fakturcari")
        lblNoSO.Text = sesHelper.GetSession("fakturSOcari")
    End Sub

    Private Sub BindDetailToGrid()
        objClaimHeader = sesHelper.GetSession("PartEntryClaim")
        'remark by willy
        'If (objClaimHeader.KTBStatus <> CType(PartIncidentalStatus.PartIncidentalKTBStatusEnum.Baru, Short) OrElse ((Not (objPartIncidentalHeader.Dealer Is Nothing)) AndAlso (objPartIncidentalHeader.Dealer.ID <> objDealer.ID))) Then
        'dtgEntryClaim.Columns(8).Visible = False
        'dtgEntryClaim.Columns(9).Visible = False
        'dtgEntryClaim.Columns(10).Visible = False
        'End If                
        dtgEntryClaim.DataSource = objClaimHeader.ClaimDetails
        dtgEntryClaim.DataBind()
    End Sub

    Private Sub SetButtonNewMode()
        btnSave.Enabled = True
    End Sub

    Private Sub SetButtonEditMode()
        'objClaimHeader = sesHelper.GetSession("PartEntryClaim")

        'If (objClaimHeader.StatusKTB <> PartIncidentalStatus.PartIncidentalKTBStatusEnum.Baru) OrElse ((Not (objPartIncidentalHeader.Dealer Is Nothing)) AndAlso (objPartIncidentalHeader.Dealer.ID <> objDealerSession.ID)) Then
        'btnSave.Enabled = False
        'Else
        'If objPartIncidentalHeader.ID <> 0 Then
        '    btnBaru.Enabled = True
        '    btnValidasi.Enabled = True
        'End If
        'End If
    End Sub

    Private Function ValidateItem(ByVal kodeBarang As String, ByVal quantity As String, ByVal keterangan As String) As Boolean
        'If (kodeBarang = String.Empty Or Unit = String.Empty Or chassisNum = String.Empty Or assemblyYear = String.Empty) Then
        Dim objReason As ClaimReason = New ClaimReasonFacade(User).Retrieve(CInt(ddlReasonClaimHeader.SelectedValue))

        If txtNoFaktur.Text.Trim() = String.Empty Then
            MessageBox.Show("Error : Silahkan isi nomor faktur terlebih dahulu")
            Return False
        ElseIf ddlReasonClaimHeader.SelectedIndex = 0 Then
            MessageBox.Show("Error : Silahkan pilih alasan terlebih dahulu")
            Return False
        ElseIf objReason.Status = EnumCategoryStatus.CategoryStatus.TidakAktif Then
            MessageBox.Show("Alasan " & ddlReasonClaimHeader.SelectedItem.Text & " tidak aktif. Silahkan pilih alasan yang lain.")
            Return False
        ElseIf (kodeBarang = String.Empty Or quantity = String.Empty Or keterangan = String.Empty) Then
            MessageBox.Show("Error : Nomor Barang, jumlah dan keterangan Tidak boleh Kosong")
            Return False
        Else
            Dim ObjSparePartMaster As SparePartMaster = New SparePartMasterFacade(User).Retrieve(kodeBarang.Trim)
            Dim oSPDetails As ArrayList = GetCurrentSparePartPOStatusDetailBySparePartCode(kodeBarang)


            If (ObjSparePartMaster.ID = 0) Then
                MessageBox.Show("Error : SparePart Tidak Ditemukan")
                Return False
            ElseIf oSPDetails.Count = 0 Then
                MessageBox.Show("Error : SparePart Tidak terdapat di Faktur")
                Return False
                'Commented by Ikhsan, 16 Juli 2008
                'Untuk menghilangkan validasi terhadap type code.
                'Berdasarkan permintaan Rina.
                'ElseIf ObjSparePartMaster.TypeCode = "I" Or ObjSparePartMaster.TypeCode = "E" Or ObjSparePartMaster.TypeCode = "A" Then
                '    MessageBox.Show("Error : Sparepart dengan stop mark I, E, A tidak bisa dipesan")
                '    Return False
            ElseIf Convert.ToInt32(ddlReasonClaimHeader.SelectedValue) <> 3 Then
                Dim objSparePartPOStatusDetailTmp As SparePartPOStatusDetail = oSPDetails(0)
                If (Convert.ToInt32(quantity) > (objSparePartPOStatusDetailTmp.BillingQuantity)) Then
                    'MessageBox.Show("Item " & objSparePartPOStatusDetailTmp.SparePartMaster.PartNumber & " : Quantity Claim telah melampaui batas yang diperbolehkan (" & (objSparePartPOStatusDetailTmp.BillingQuantity - objSparePartPOStatusDetailTmp.ClaimedQty) & ")")
                    'Return False
                End If
            End If
        End If

        'End If
        Return True
    End Function

    Private Function ValidateDuplication(ByVal kodeBarang As String, ByVal Mode As String, ByVal Rowindex As Integer) As Boolean
        Try
            If (Mode = "Add") Then
                For Each item As ClaimDetail In objClaimHeader.ClaimDetails
                    If (item.SparePartPOStatusDetail.SparePartMaster.PartNumber = kodeBarang.Trim) Then
                        MessageBox.Show("Error : Duplikasi Kode Barang")
                        Return False
                    End If
                Next
            Else
                Dim i As Integer = 0
                For Each item As ClaimDetail In objClaimHeader.ClaimDetails
                    If (item.SparePartPOStatusDetail.SparePartMaster.PartNumber = kodeBarang.Trim) Then
                        If i <> Rowindex Then
                            MessageBox.Show("Error : Duplikasi Kode Barang")
                            Return False
                        End If
                    End If
                    i = i + 1
                Next
            End If
            Return True
        Catch ex As Exception
            MessageBox.Show("Error : Part Incedental tidak ditemukan.")
            Return False
        End Try
    End Function

    Private Sub SetDtgClaimItemFooter(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lblFooterNomorBarang As Label = CType(e.Item.FindControl("lblFooterNomorBarang"), Label)
        lblFooterNomorBarang.Attributes("onclick") = "ShowPPKodeBarangSelection();"
    End Sub

    Private Sub SetDtgClaimItemEdit(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lblEditNomorBarang As Label = CType(e.Item.FindControl("lblEditNomorBarang"), Label)
        lblEditNomorBarang.Attributes("onclick") = "ShowPPKodeBarangSelection();"
    End Sub

    Private Function CreateClaimDetail(ByVal kodeBarang As String, ByVal quantity As String, ByVal keterangan As String) As ClaimDetail
        Dim objClaimDetailResult As ClaimDetail = New ClaimDetail

        Dim oSPDetails As ArrayList = GetCurrentSparePartPOStatusDetailBySparePartCode(kodeBarang)

        'oSPDetails = New SparePart.SparePartPOStatusDetailFacade(User).Retrieve(Convert.ToInt32(dtgEntryClaim.da.DataKeys.Item(i)))
        Dim oClaimGood As ClaimGoodCondition = New Claim.ClaimGoodConditionFacade(User).Retrieve("")

        objClaimDetailResult.Keterangan = keterangan
        objClaimDetailResult.Qty = quantity
        objClaimDetailResult.SparePartPOStatusDetail = CType(oSPDetails(0), SparePartPOStatusDetail)

        'If Convert.ToInt32(ddlReasonClaimHeader.SelectedValue) <> 3 Then
        '    If (Convert.ToInt32(quantity) > (objClaimDetailResult.SparePartPOStatusDetail.BillingQuantity - objClaimDetailResult.SparePartPOStatusDetail.ClaimedQty)) Then
        '        MessageBox.Show("Item " & objClaimDetailResult.SparePartPOStatusDetail.SparePartMaster.PartNumber & " : Quantity Claim telah melampaui batas yang diperbolehkan")
        '        Return Nothing
        '    End If
        'End If

        objClaimDetailResult.ClaimGoodCondition = oClaimGood
        Return objClaimDetailResult
    End Function

    Private Function isFakturExist(ByVal faktur As String) As Boolean
        '  Dim _SPPo As SparePartPOStatus = New SparePartPOStatusFacade(User).Retrieve(txtNoFaktur.Text)
        Dim _SPPo As SparePartPOStatus = New SparePartPOStatusFacade(User).RetrieveSO(txtNoFaktur.Text, lblNoSO.Text)
        If _SPPo Is Nothing Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function GetCurrentSparePartPOStatusDetailBySparePartCode(ByVal kodeBarang As String) As ArrayList
        Dim oSPDetails As ArrayList
        Dim objUser As UserInfo = CType(sesHelper.GetSession("LOGINUSERINFO"), UserInfo)

        ' SPPo = New SparePartPOStatusFacade(User).Retrieve(txtNoFaktur.Text)
        SPPo = New SparePartPOStatusFacade(User).RetrieveSO(txtNoFaktur.Text, lblNoSO.Text)
        lblTglFaktur.Text = Format(SPPo.BillingDate, "dd/MM/yyyy")
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.SparePartPOStatusDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPOStatusDetail), "SparePartPOStatus.ID", MatchType.Exact, SPPo.ID.ToString()))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPOStatusDetail), "SparePartPOStatus.SparePartPO.Dealer.ID", MatchType.Exact, objUser.Dealer.ID.ToString()))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.SparePartPOStatusDetail), "SparePartMaster.PartNumber", MatchType.Exact, kodeBarang))
        oSPDetails = New SparePart.SparePartPOStatusDetailFacade(User).RetrieveActiveList(criterias, "ID", Sort.SortDirection.ASC)
        Return oSPDetails
    End Function
#End Region

    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanClaimView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=ORDER CLAIM - Pengajuan")
        End If
    End Sub

    Private Function CekBtnPriv() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanClaimCreateData_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        InitiateAuthorization()
        oDealer = CType(sesHelper.GetSession("DEALER"), Dealer)
        oLoginUser = CType(sesHelper.GetSession("LOGINUSERINFO"), UserInfo)
        lblSearch.Attributes("onclick") = "ShowPPNoFaktur();"
        txtNoFaktur.Attributes.Add("readonly", "readonly")
        TargetDirectory = KTB.DNet.Lib.WebConfig.GetValue("SAN")
        TempDirectory = Server.MapPath("") + "\..\DataTemp\ClaimTemp\" + oDealer.ID.ToString + "-" + oLoginUser.ID.ToString + "\"

     
        lblNoSO.Text = HPONumber.Value
        'If Not IsPostBack Then
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "change label", "<script type='text/javascript'>change_label()</script>")
        '    lblNoSO.Text = HPONumber.Value
        '    ViewState("currSortColumn") = "SparePartMaster.PartNumber"
        '    ViewState("currSortDirection") = Sort.SortDirection.ASC
        '    fillDataDealer(oDealer)
        '    'ModeButton(False)
        '    BindReasonHeader(ddlReasonClaimHeader)
        '    BindDataToPage()

        'End If
        If CekBtnPriv() Then
            lblSearch.Visible = True
            btnSave.Enabled = True
            'fuEvidence.Disabled = False
            dgEvidence.Enabled = True
            dgEvidence.Visible = True

        Else
            lblSearch.Visible = False
            btnSave.Enabled = False
            'fuEvidence.Disabled = True
            dgEvidence.Enabled = False
            dgEvidence.Visible = False
        End If

        If Not IsPostBack Then
            Session(sesEvidence) = New ArrayList
            BindEvidence()
            dgEvidence.Enabled = False

            Page.ClientScript.RegisterStartupScript(Me.GetType(), "change label", "<script type='text/javascript'>change_label()</script>")
            lblNoSO.Text = HPONumber.Value
            ViewState("currSortColumn") = "SparePartMaster.PartNumber"
            ViewState("currSortDirection") = Sort.SortDirection.ASC
            fillDataDealer(oDealer)
            'ModeButton(False)
            BindReasonHeader(ddlReasonClaimHeader)
            BindDataToPage()
        End If


    End Sub

    Private Sub dtgEntryClaim_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgEntryClaim.ItemDataBound
        'If (e.Item.ItemIndex >= 0) Then
        objClaimHeader = sesHelper.GetSession("PartEntryClaim")
        If e.Item.ItemType = ListItemType.Footer Then
            SetDtgClaimItemFooter(e)
        ElseIf e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            SetDtgClaimItemEdit(e)
        ElseIf (e.Item.ItemIndex >= 0) Then
            Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
            e.Item.Cells(0).Controls.Add(lNum)
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dtgEntryClaim.CurrentPageIndex * dtgEntryClaim.PageSize)
            'Dim txtQtyClaim As TextBox = CType(e.Item.FindControl("txtQtyClaim"), TextBox)
            Dim lblQtyClaim As Label = CType(e.Item.FindControl("lblQtyClaim"), Label)
            Dim lblHargaSatuan As Label = CType(e.Item.FindControl("lblHargaSatuan"), Label)
            Dim lblTotal As Label = CType(e.Item.FindControl("lblTotal"), Label)

            lblTotal.Text = (Convert.ToDecimal(lblHargaSatuan.Text) * Convert.ToInt32(lblQtyClaim.Text)).ToString("#,##0")

            'txtQtyClaim.Attributes.Add("onchange", "CalculateAmount('" & txtQtyClaim.ClientID & "','" & lblHargaSatuan.ClientID & "','" & lblTotal.ClientID & "')")
            'If Val(txtQtyClaim.Text) > 0 Then
            '    lblTotal.Text = CStr(Val(txtQtyClaim.Text) * Val(lblHargaSatuan.Text.Replace(".", "")))

            '    Dim myculture As CultureInfo = New CultureInfo("ID-id")
            '    lblTotal.Text = CDbl(lblTotal.Text).ToString("#,#", myculture)
            'End If
        End If

        If Not (objClaimHeader.ClaimDetails.Count = 0 Or e.Item.ItemIndex = -1) Then
            objClaimDetail = objClaimHeader.ClaimDetails(e.Item.ItemIndex)
            'e.Item.Cells(2).Text = objClaimHeader.SparePartMaster.PartName
            'e.Item.Cells(3).Text = objClaimHeader.SparePartMaster.ModelCode
            Dim lbtnHapus As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            lbtnHapus.Attributes("onclick") = "return confirm('Yakin akan hapus record ini?');"
        End If

        'End If
    End Sub

    Private Sub dtgEntryClaim_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgEntryClaim.SortCommand
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
        BindToGrid()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        If (txtNoFaktur.Text <> String.Empty) Then
            'Dim objPO As SparePartPOStatus = New SparePartPOStatusFacade(User).Retrieve(txtNoFaktur.Text)
            Dim objPO As SparePartPOStatus = New SparePartPOStatusFacade(User).RetrievePO(txtNoFaktur.Text, lblNoSO.Text)
            If objPO Is Nothing Then
                MessageBox.Show("Data Nomor PO yang anda masukkan tidak ada")
                Exit Sub
            End If

            sesHelper.SetSession("fakturcari", txtNoFaktur.Text.Trim)
            sesHelper.SetSession("fakturSOcari", lblNoSO.Text.Trim)
            ModeButton(True)
            dtgEntryClaim.CurrentPageIndex = 0
            BindToGrid()
            'txtNoFaktur.Enabled = False
            'lblSearch.Enabled = False
        Else
            MessageBox.Show("No faktur harus diisi")
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        'Refresh Total First
        'For Each item As DataGridItem In dtgEntryClaim.Items
        '    Dim txtQtyClaim As TextBox = CType(item.FindControl("txtQtyClaim"), TextBox)
        '    If (Val(txtQtyClaim.Text) > 0) Then
        '        Dim lblHargaSatuan As Label = CType(item.FindControl("lblHargaSatuan"), Label)
        '        Dim lblTotal As Label = CType(item.FindControl("lblTotal"), Label)
        '        lblTotal.Text = CStr(Val(txtQtyClaim.Text) * Val(lblHargaSatuan.Text.Replace(".", "")))

        '        Dim myculture As CultureInfo = New CultureInfo("ID-id")
        '        lblTotal.Text = CDbl(lblTotal.Text).ToString("#,#", myculture)

        '    End If

        'Next

        If ddlReasonClaimHeader.SelectedIndex = 0 Then
            MessageBox.Show("Isi Alasan Claim Lebih Dulu")
            Exit Sub
        End If

        'If sesHelper.GetSession("fakturcari") <> txtNoFaktur.Text.Trim Then
        '    MessageBox.Show("Item barang tidak match dengan no faktur " & txtNoFaktur.Text.Trim)
        '    Exit Sub
        'End If

        If (icClaimDate.Value < Now.Date) Then
            MessageBox.Show("Tanggal Claim tidak valid")
            Exit Sub
        End If
        If Not UploadFileMatchEvidence() Then
            MessageBox.Show("Upload File tidak lengkap")
            Exit Sub
        End If

        Dim objReason As ClaimReason = New ClaimReasonFacade(User).Retrieve(CInt(ddlReasonClaimHeader.SelectedValue))

        If objReason.IsMandatoryUpload = 1 And sesHelper.GetSession("UploadFile") Is Nothing Then
            MessageBox.Show("Alasan " & ddlReasonClaimHeader.SelectedItem.Text & " harus menyertakan upload file evidence")
            Return
        End If

        If objReason.Status = EnumCategoryStatus.CategoryStatus.TidakAktif Then
            MessageBox.Show("Alasan " & ddlReasonClaimHeader.SelectedItem.Text & " tidak aktif. Silahkan pilih alasan yang lain.")
            Return
        End If
        'Dim cDetails As ArrayList = SaveToArrayList()
        ObjClaimHeader = sesHelper.GetSession("PartEntryClaim")
        SetClaimHeaderByInputedData()
        Dim cDetails As ArrayList = ObjClaimHeader.ClaimDetails
        'If count > 0 Then
        If cDetails.Count > 0 Then
            If (cDetails.Count > 0) Then
                Dim nResult As Integer
                Dim ClaimNo As String = ""

                Try
                    'If sesHelper.GetSession("UploadFile") <> Nothing Then
                    '    btnUpload_Click(True)
                    'End If

                    'Dim ObjClaimHeader As ClaimHeader = GetClaimHeader()
                    If objClaimHeader Is Nothing Then
                        MessageBox.Show("Gagal simpan claim")
                        Return
                    Else

                        Dim limitDate As Date = GetLimitDate(objClaimHeader.SparePartPOStatus)
                        If Date.Today > limitDate Then
                            MessageBox.Show("Batas waktu pengajuan claim untuk faktur ini sudah lewat ( " & Format(limitDate, "dd-MM-yyyy") & " )")
                            Exit Sub
                        End If

                        Dim strHtml As String = String.Empty
                        If Not isDetailOk(cDetails, strHtml) Then
                            ErrorMessage.Visible = True
                            FailureText.Text = strHtml
                            Throw New Exception(" ")
                            Return
                        Else
                            ErrorMessage.Visible = False
                        End If
                        nResult = New Claim.ClaimDetailFacade(User).InsertClaimHeaderDetail(objClaimHeader, cDetails)
                        Dim objInserted As ClaimHeader = New Claim.ClaimHeaderFacade(User).Retrieve(nResult)
                        ClaimNo = objInserted.ClaimNo
                    End If
                Catch ex As Exception
                    MessageBox.Show("Gagal simpan claim " & ex.Message)
                    Return
                End Try
                If nResult <> -1 Then
                    MessageBox.Show("Claim " & ClaimNo & " berhasil disimpan. ")
                    txtClaimNo.Text = ClaimNo
                    btnSave.Enabled = False
                    sesHelper.SetSession("UploadFile", Nothing)
                    lblFilename.Text = ""
                Else
                    MessageBox.Show(SR.SaveFail)
                End If
            End If
        Else
            MessageBox.Show("Qty claim harus diisi")
        End If
    End Sub

    Private Function UploadFileMatchEvidence() As Boolean
        'SpClaimDocument
        Dim arrEvidence As ArrayList = CType(Session(sesEvidence), ArrayList)
        'AttchPramClaimReason
        Dim arrEvidenceType As ArrayList = CType(Session(EvidenceType), ArrayList)
        Dim typeCount As Integer = 0
        For Each attch As AttchPramClaimReason In arrEvidenceType
            For Each item As SpClaimDocument In arrEvidence
                If attch.SPSupportClaimDoc.ID = item.SPSupportClaimDoc.ID Then
                    typeCount = typeCount + 1
                    Exit For
                End If
            Next
        Next
        If typeCount = arrEvidenceType.Count Then
            Return True
        End If
        Return False
    End Function


    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        sesHelper.RemoveSession("PartEntryClaim")
        Response.Redirect("../Claim/FrmEntryClaim.aspx", True)
    End Sub

    Private Sub ddlReasonClaimHeader_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlReasonClaimHeader.SelectedIndexChanged
        If ddlReasonClaimHeader.SelectedIndex = 0 Then
            lblSyarat.Text = ""
            'lblIncharge.Text = ""
        Else
            Dim objReason As ClaimReason = New KTB.DNet.BusinessFacade.Claim.ClaimReasonFacade(User).Retrieve(CInt(ddlReasonClaimHeader.SelectedValue))
            lblSyarat.Text = objReason.Prerequisite
            'lblIncharge.Text = objReason.incharge
        End If

        For Each item As DataGridItem In dtgEntryClaim.Items
            Dim txtQtyClaim As TextBox = CType(item.FindControl("txtQtyClaim"), TextBox)
            If (Val(txtQtyClaim.Text) > 0) Then
                Dim lblHargaSatuan As Label = CType(item.FindControl("lblHargaSatuan"), Label)
                Dim lblTotal As Label = CType(item.FindControl("lblTotal"), Label)
                lblTotal.Text = CStr(Val(txtQtyClaim.Text) * Val(lblHargaSatuan.Text.Replace(".", "")))

                Dim myculture As CultureInfo = New CultureInfo("ID-id")
                lblTotal.Text = CDbl(lblTotal.Text).ToString("#,#", myculture)

            End If

        Next
        BindEvidence()
    End Sub

    Private Sub btnUpload_Click(ByVal status As Boolean)
        'If fuEvidence.Value <> "" OrElse fuEvidence.Value <> Nothing Then
        '    GetFileClaimHeader(False)
        'Else
        '    MessageBox.Show("File masih kosong")
        'End If
        'For Each item As DataGridItem In dgEvidence.Items
        'If item.ItemType = ListItemType.Item OrElse item.ItemType = ListItemType.AlternatingItem Then
        'Dim fuEvidence As System.Web.UI.HtmlControls.HtmlInputFile = CType(item.FindControl("iFileClaimEVIDENCE"), System.Web.UI.HtmlControls.HtmlInputFile)
        If fuEvidence.Value <> "" OrElse fuEvidence.Value <> Nothing Then
            GetFileClaimHeader(fuEvidence, status)
        Else
            MessageBox.Show("File masih kosong")
        End If
        '    End If
        'Next

    End Sub

    Private Sub dtgEntryClaim_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgEntryClaim.ItemCommand
        objClaimHeader = sesHelper.GetSession("PartEntryClaim")
        Dim objClaimDetailFacade As ClaimDetailFacade
        Select Case (e.CommandName)
            Case "Delete"
                Dim lbl1 As Label = e.Item.Cells(0).FindControl("lblNo")
                Mode = ViewState("Mode")
                If (Mode = enumMode.Mode.EditMode) Then
                    'If (CType(objClaimHeader.StatusKTB, Short) = CType(PartIncidentalStatus.PartIncidentalKTBStatusEnum.Baru, Short)) Then
                    If (objClaimHeader.ClaimDetails.Count <> 1) Then
                        objClaimDetailFacade = New ClaimDetailFacade(User)
                        objClaimDetailFacade.DeleteFromDB(objClaimHeader.ClaimDetails.Item(CType(lbl1.Text, Integer) - 1))
                        'Else
                        '    MessageBox.Show("Permintaan Khusus Harus memiliki minimal 1 Detail")
                        '    Exit Sub
                        'End If
                    Else
                        MessageBox.Show("Status Permintaan Bukan Baru")
                        Exit Sub
                    End If
                End If
                objClaimHeader.ClaimDetails.Remove(objClaimHeader.ClaimDetails.Item(CType(lbl1.Text, Integer) - 1))
                sesHelper.SetSession("PartEntryClaim", objClaimHeader)
                BindDataToPage()
                Mode = ViewState("Mode")
                If objClaimHeader.ClaimDetails.Count = 0 And Mode = enumMode.Mode.NewItemMode Then
                    SetButtonNewMode()
                End If
                dtgEntryClaim.ShowFooter = True
            Case "Add"
                If Not Page.IsValid Then
                    Return
                End If
                If isFakturExist(txtNoFaktur.Text.Trim) Then
                    Dim txt1 As TextBox = e.Item.FindControl("txtFooterNomorBarang")
                    Dim txt2 As TextBox = e.Item.FindControl("txtQtyClaimEntry")
                    Dim txt3 As TextBox = e.Item.FindControl("txtKeteranganEntry")
                    If (ValidateDuplication(txt1.Text.ToUpper, "Add", -1) AndAlso ValidateItem(txt1.Text, txt2.Text, txt3.Text)) Then
                        objClaimDetail = CreateClaimDetail(txt1.Text, txt2.Text, txt3.Text)
                        Mode = ViewState("Mode")
                        If (Mode = enumMode.Mode.EditMode) Then
                            objClaimDetailFacade = New ClaimDetailFacade(User)
                            objClaimDetail.ClaimHeader = objClaimHeader
                            objClaimDetailFacade.Insert(objClaimDetail)
                        End If
                    Else
                        Exit Sub
                    End If
                    'add by willy, ga tau enaknya taro dmana
                    txtNoFaktur.Enabled = False
                    lblSearch.Visible = False
                    ddlReasonClaimHeader.Enabled = False

                    objClaimHeader.ClaimDetails.Add(objClaimDetail)
                    BindDataToPage()
                    SetButtonEditMode()
                Else
                    MessageBox.Show("No Faktur tidak ditemukan.")
                End If

        End Select
    End Sub

    Private Sub dtgEntryClaim_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgEntryClaim.EditCommand
        dtgEntryClaim.EditItemIndex = CInt(e.Item.ItemIndex)
        dtgEntryClaim.ShowFooter = False
        BindDetailToGrid()
    End Sub

    Private Sub dtgEntryClaim_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgEntryClaim.CancelCommand
        dtgEntryClaim.EditItemIndex = -1
        BindDetailToGrid()
        dtgEntryClaim.ShowFooter = True
    End Sub

    Private Sub dtgEntryClaim_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgEntryClaim.UpdateCommand
        objClaimHeader = sesHelper.GetSession("PartEntryClaim")
        Dim txt1 As TextBox = e.Item.FindControl("txtNomorBarangEdit")
        Dim txt2 As TextBox = e.Item.FindControl("txtQtyClaimEntryEdit")
        Dim txt3 As TextBox = e.Item.FindControl("txtKeteranganEntryEdit")
        If (ValidateDuplication(txt1.Text.ToUpper, "Edit", e.Item.ItemIndex) AndAlso ValidateItem(txt1.Text, txt2.Text, txt3.Text)) Then
            objClaimDetail = objClaimHeader.ClaimDetails(e.Item.ItemIndex)

            Dim oSPDetails As ArrayList = GetCurrentSparePartPOStatusDetailBySparePartCode(txt1.Text)
            Dim oClaimGood As ClaimGoodCondition = New Claim.ClaimGoodConditionFacade(User).Retrieve("")
            objClaimDetail.Keterangan = txt3.Text
            objClaimDetail.Qty = Convert.ToInt32(txt2.Text)
            objClaimDetail.SparePartPOStatusDetail = CType(oSPDetails(0), SparePartPOStatusDetail)

            sesHelper.SetSession("Part", objClaimHeader)
            dtgEntryClaim.EditItemIndex = -1
            Mode = ViewState("Mode")
            If (Mode = enumMode.Mode.EditMode) Then
                Dim objClaimDetailFacade As New ClaimDetailFacade(User)
                objClaimDetail.ClaimHeader = objClaimHeader
                objClaimDetailFacade.Update(objClaimDetail)
            End If
            dtgEntryClaim.EditItemIndex = -1
            dtgEntryClaim.ShowFooter = True
            BindDetailToGrid()
        End If
    End Sub
#End Region


    Private Function isDetailOk(ByVal arrDetail As ArrayList, ByRef strHTML As String) As Boolean


        If Convert.ToInt32(ddlReasonClaimHeader.SelectedValue) = 3 Then
            Return True
        End If
        Dim isValid As Boolean = True
        Dim strTempalte As String = "<ul>{0}</ul>"
        strHTML = ""
        For Each Cd As ClaimDetail In arrDetail
            Dim qtyBill As Integer = Cd.SparePartPOStatusDetail.BillingQuantity
            If Cd.Qty > qtyBill Then

                strHTML = strHTML & "<li> " & Cd.SparePartPOStatusDetail.SparePartMaster.PartNumber & " melebihi SO  </li>"

                isValid = False
                Continue For
            End If
            Dim intClaim As Integer = Cd.SparePartPOStatusDetail.GetClaimedQty(0)
            If (qtyBill - intClaim) < Cd.Qty Then

                Dim arr As New ArrayList
                arr = Cd.SparePartPOStatusDetail.GetClaimedDetail(0)
                strHTML = strHTML & "<li> Sisa Qty : " & (qtyBill - intClaim).ToString & " & " & Cd.SparePartPOStatusDetail.SparePartMaster.PartNumber & " sudah diajukan di {0}  </li>"
                Dim strClaim As String = "<ul>"

                For Each cdd As ClaimDetail In arr
                    strClaim = strClaim & "<li>" & cdd.ClaimHeader.ClaimNo & " </li> "
                Next
                strClaim = strClaim & "</ul>"
                strHTML = String.Format(strHTML, strClaim)
                isValid = False
                Continue For
            End If
        Next

        If Not isValid Then
            strHTML = String.Format(strTempalte, strHTML)
        End If

        Return isValid
    End Function

#Region "dgEvidence"
    Dim sesEvidence = "EVIDENCE"
    Dim EvidenceType = "EVIDENCETYPE"
    'Private TempDirectory As String
    Protected Sub dgEvidence_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgEvidence.ItemDataBound
        Select Case e.Item.ItemType
            Case ListItemType.Item, ListItemType.AlternatingItem

                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                lblNo.Text = e.Item.ItemIndex + 1 + (dgEvidence.CurrentPageIndex * dgEvidence.PageSize)

                Dim arrAttachment As ArrayList = Session(sesEvidence)
                If arrAttachment.Count > 0 Then
                    Dim objSpClaimBandingDocument As SpClaimDocument = arrAttachment(e.Item.ItemIndex)

                    Dim lblWSCEvidenceType As Label = CType(e.Item.FindControl("lblClaimEvidenceType"), Label)
                    lblWSCEvidenceType.Text = objSpClaimBandingDocument.SPSupportClaimDoc.DocumentName

                    Dim lblFileWSCEVIDENCE As Label = CType(e.Item.FindControl("lblFileClaimEVIDENCE"), Label)
                    lblFileWSCEVIDENCE.Text = Path.GetFileName(objSpClaimBandingDocument.AttachmentData.FileName)
                End If
            Case ListItemType.Footer
                Dim ddlClaimEvidenceTypeFooter As DropDownList = CType(e.Item.FindControl("ddlClaimEvidenceTypeFooter"), DropDownList)
                BindDDLClaimEvidence(ddlClaimEvidenceTypeFooter)
        End Select
    End Sub
    Private Sub BindDDLClaimEvidence(dropDownList As DropDownList)
        With dropDownList.Items
            .Clear()
            .Add(New ListItem("Silahkan Pilih", -1))
            If ddlReasonClaimHeader.SelectedValue <> "" Then
                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AttchPramClaimReason), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(AttchPramClaimReason), "ClaimReason.ID", MatchType.Exact, ddlReasonClaimHeader.SelectedValue))
                Dim arl As ArrayList = New AttchPramClaimReasonFacade(User).Retrieve(crit)
                'Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SPSupportClaimDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'crit.opAnd(New Criteria(GetType(SPSupportClaimDoc), "Status", MatchType.Exact, 1))
                'Dim arl As ArrayList = New SPSupportClaimDocFacade(User).Retrieve(crit)
                For Each spSup As AttchPramClaimReason In arl
                    .Add(New ListItem(spSup.SPSupportClaimDoc.DocumentName, spSup.SPSupportClaimDoc.ID))
                Next
                Session(EvidenceType) = arl
            End If
        End With
    End Sub
    Protected Sub dgEvidence_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgEvidence.SelectedIndexChanged

    End Sub
    Private Function FileIsExist(ByVal intEvidenceType As Integer, ByVal FileName As String, ByVal AttachmentCollection As ArrayList) As Boolean
        Dim bResult As Boolean = False
        If AttachmentCollection.Count > 0 Then
            For Each objEvidence As SpClaimDocument In AttachmentCollection
                If Not IsNothing(objEvidence.AttachmentData) Then
                    If Path.GetFileName(objEvidence.AttachmentData.FileName) = FileName Then
                        If intEvidenceType = objEvidence.SPSupportClaimDoc.ID Then
                            bResult = True
                            Exit For
                        End If
                    End If
                End If
            Next
        End If
        Return bResult
    End Function
    Private Function TimeStamp() As String
        Return DateTime.Now.Year & DateTime.Now.Month & DateTime.Now.Day & DateTime.Now.Hour & DateTime.Now.Minute & DateTime.Now.Second & DateTime.Now.Millisecond
    End Function
    Protected Sub dgEvidence_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgEvidence.ItemCommand
        Dim _arrEVIDENCE As ArrayList = Session(sesEvidence)
        Select Case e.CommandName
            Case "Add"
                Dim ddlClaimEvidenceTypeFooter As DropDownList = CType(e.Item.FindControl("ddlClaimEvidenceTypeFooter"), DropDownList)
                Dim FileUpload As HtmlInputFile = CType(e.Item.FindControl("iFileClaimEVIDENCE"), HtmlInputFile)

                fuEvidence = FileUpload

                Dim objPostedData As HttpPostedFile
                Dim objSpClaimDocument As SpClaimDocument = New SpClaimDocument
                Dim sFileName As String

                If ddlClaimEvidenceTypeFooter.SelectedIndex = 0 Then
                    MessageBox.Show("Silahkan pilih Tipe bukti")
                    Exit Sub
                End If

                If FileUpload.PostedFile.ContentLength = 0 Then
                    MessageBox.Show("Silahkan pilih File yang akan di upload")
                    Exit Sub
                End If

                If Not IsNothing(FileUpload) OrElse FileUpload.Value <> String.Empty Then
                    objPostedData = FileUpload.PostedFile
                Else
                    objPostedData = Nothing
                End If

                'If sesHelper.GetSession("UploadFile") <> Nothing Then
                '    btnUpload_Click(True)
                'End If

                If FileUpload.PostedFile.ContentLength > 0 Then
                    btnUpload_Click(False)
                End If

                If Not (IsNothing(objPostedData)) Then
                    sFileName = objPostedData.FileName.Split("\")(objPostedData.FileName.Split("\").Length - 1)

                    If KTB.DNet.UI.Helper.FileHelper.IsExecutableFile(sFileName) Then
                        MessageBox.Show("Tidak diperkenankan mengupload file dengan ekstensi '.exe'. Pastikan file anda bebas dari virus.")
                        BindEvidence()
                        Return
                    End If

                    If Not FileIsExist(ddlClaimEvidenceTypeFooter.SelectedValue, sFileName, _arrEVIDENCE) Then
                        Dim SrcFile As String = Path.GetFileName(objPostedData.FileName) '-- Source file name
                        'Dim DestFile = KTB.DNet.Lib.WebConfig.GetValue("ClaimEvidenceDir") & "\ClaimNumber\" & TimeStamp() & SrcFile.Substring(SrcFile.Length - 4)   '-- Destination file
                        Dim DestFile = KTB.DNet.Lib.WebConfig.GetValue("ClaimEvidenceDir") & "\ClaimNumber\" & TimeStamp() & sFileName   '-- Destination file

                        objSpClaimDocument.FileName = sFileName
                        objSpClaimDocument.FilePath = DestFile
                        objSpClaimDocument.AttachmentData = objPostedData
                        objSpClaimDocument.SPSupportClaimDoc = New SPSupportClaimDocFacade(User).Retrieve(CType(ddlClaimEvidenceTypeFooter.SelectedValue, Integer))
                        objSpClaimDocument.ClaimHeader = Nothing

                        UploadAttachment(objSpClaimDocument, TempDirectory)

                        _arrEVIDENCE.Add(objSpClaimDocument)
                        Session(sesEvidence) = _arrEVIDENCE
                    Else
                        MessageBox.Show(SR.DataIsExist("Attachment File"))
                    End If
                Else
                    objSpClaimDocument.FilePath = vbNull
                    objSpClaimDocument.AttachmentData = objPostedData
                    objSpClaimDocument.SPSupportClaimDoc = New SPSupportClaimDocFacade(User).Retrieve(CType(ddlClaimEvidenceTypeFooter.SelectedValue, Integer))
                    objSpClaimDocument.ClaimHeader = Nothing

                    _arrEVIDENCE.Add(objSpClaimDocument)
                    Session(sesEvidence) = _arrEVIDENCE
                End If
            Case "Delete"
                RemoveWSCAttachment(CType(_arrEVIDENCE(e.Item.ItemIndex), SpClaimDocument), TempDirectory)
                _arrEVIDENCE.RemoveAt(e.Item.ItemIndex)
        End Select
        BindEvidence()
    End Sub
    Private Sub RemoveWSCAttachment(ByVal ObjAttachment As SpClaimDocument, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim finfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                finfo = New FileInfo(TargetPath + ObjAttachment.FilePath)
                If finfo.Exists Then
                    finfo.Delete()
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub UploadAttachment(ByVal ObjAttachment As SpClaimDocument, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim finfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                If Not IsNothing(ObjAttachment.FilePath) Then
                    finfo = New FileInfo(TargetPath + ObjAttachment.FilePath)

                    If Not finfo.Directory.Exists Then
                        Directory.CreateDirectory(finfo.DirectoryName)
                    End If
                    ObjAttachment.AttachmentData.SaveAs(TargetPath + ObjAttachment.FilePath)
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub BindEvidence()
        dgEvidence.DataSource = Session(sesEvidence)
        dgEvidence.DataBind()
    End Sub
#End Region
    
End Class
