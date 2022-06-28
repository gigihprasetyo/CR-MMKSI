#Region "Import Library"
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Sparepart
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

Imports System.Text
Imports System.IO
Imports System.Web.Mail
#End Region

Public Class FrmWSCHeaderBB
#Region "Private Variable"

    Inherits System.Web.UI.Page

    Private _dtKodeKerusakanStartDate As DateTime = New DateTime(2010, 6, 17, 17, 20, 0)

    Private sessHelper As New SessionHelper
    Private oDealer As New Dealer
    Private oLoginUser As New UserInfo
    Private oWSCHeaderBB As WSCHeaderBB
    Private oWSCHeaderBBFacade As New WSCHeaderBBFacade(User)
    Private oWSCDetailBBOngkosKerja As New WSCDetailBB
    Private oWSCDetailBBOngkosKerjaFacade As New WSCDetailBBFacade(User)
    Private oWSCDetailBBParts As New WSCDetailBB
    Private oWSCDetailBBPartsFacade As New WSCDetailBBFacade(User)
    Private oWSCEvidenceBB As New WSCEvidenceBB
    Private oWSCEvidenceBBFacade As New WSCEvidenceBBFacade(User)
    Private oChassis As New ChassisMasterBB
    Private oChassisFacade As New ChassisMasterBBFacade(User)
    Private oPQRAdditionalInfo As New PQRAdditionalInfo
    Private oPQRAdditionalInfoFacade As New PQRAdditionalInfoFacade(User)
    'Private oChassisMasterPKTFacade As New ChassisMasterPKTFacade(User)
    'Private oChassisMasterPKT As New ChassisMasterPKT
    Private oPQRHeaderBB As New PQRHeaderBB
    Dim objKodePosisi As DeskripsiKodePosisi
    Dim objKodeKerja As DeskripsiKodeKerja
    Dim objLaborMaster As LaborMaster
    Dim arrLaborMaster As ArrayList
    Dim screenFrom As String = String.Empty
    Dim special As Boolean = False
    Dim pqrId As Integer = 0
    Dim wscId As Integer = 0
    Private V_Suffix As String = "Static"
    Private _bIsJustDownloadedToSAP As Boolean = False

    Private Mode As enumMode.Mode

    Private AttachmentDirectory As String
    Private TargetDirectory As String
    Private TempDirectory As String
    Private strSaveSuccess As String
    Private resultSave As Integer
    Dim blnIsSoldDealer As Boolean = False
    Dim blnEnableRilis As Boolean = True
    Dim intViewStateMode As Integer = -1

    Const TEMP_EMAIL_NOLABORMASTER = "../DataFile/EmailTemplate/WSCHeaderNoExistedLaborMaster.htm"

#End Region


#Region "Page_Event"
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        'InitializeComponent()

        oDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        oLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)

        AttachmentDirectory = KTB.DNet.Lib.WebConfig.GetValue("WSCAttachmentDir")
        TargetDirectory = KTB.DNet.Lib.WebConfig.GetValue("SAN")
        TempDirectory = Server.MapPath("") + "\..\DataTemp\WSCTemp\" + oDealer.ID.ToString + "-" + oLoginUser.ID.ToString + "\"

    End Sub

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.WSCSaveData_Privilege) Then
            If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                If Not SecurityProvider.Authorize(Context.User, SR.WSCViewData_Privilege) Then
                    Server.Transfer("../FrmAccessDenied.aspx?modulName=WSC Special - Input WSC Special")
                End If
            End If

            'Else
            '    Server.Transfer("../FrmAccessDenied.aspx?modulName=WSC - Input WSC")
        End If
    End Sub

    Dim bCekPriv As Boolean = (SecurityProvider.Authorize(Context.User, SR.WSCSaveData_Privilege))

#End Region

#Region "Custom Method"
    Private Function TimeStamp() As String
        Return DateTime.Now.Year & DateTime.Now.Month & DateTime.Now.Day & DateTime.Now.Hour & DateTime.Now.Minute & DateTime.Now.Second & DateTime.Now.Millisecond
    End Function

    Private Sub BindClaimType()
        With ddlClaimType.Items
            .Clear()
            .Add(New ListItem("Silahkan Pilih", -1))
            .Add(New ListItem("Z6", "Z6"))
        End With
    End Sub

    Private Sub BindRefDoc()
        With ddlRefDoc.Items
            .Clear()
            .Add(New ListItem("Silahkan Pilih", -1))
            .Add(New ListItem("Service Bulletin", 0))
            .Add(New ListItem("PQR Reference", 1))
        End With
    End Sub

    Private Sub BindKodePosisiWSC()
        BindPosisiWSC(ddlKodeWSCA, "A")
        BindPosisiWSC(ddlKodeWSCB, "B")
        BindPosisiWSC(ddlKodeWSCC, "C")
    End Sub

    Private Sub BindWSCEvidenceBB()
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        If Mode = enumMode.Mode.NewItemMode Then
            dgFileWSCEvidenceBB.DataSource = CType(sessHelper.GetSession("NEW_WSCEVIDENCEBB"), ArrayList)
            dgFileWSCEvidenceBB.DataBind()
        Else
            dgFileWSCEvidenceBB.DataSource = CType(sessHelper.GetSession("WSCEVIDENCEBB"), ArrayList)
            dgFileWSCEvidenceBB.DataBind()
        End If
    End Sub

    Private Sub BindPosisiWSC(ByRef ddl As DropDownList, ByVal PositionCategory As String)
        Dim oPWFac As PositionWSCFacade = New PositionWSCFacade(User)
        Dim cPW As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PositionWSC), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim aPW As New ArrayList
        Dim arlItems As New ArrayList

        cPW.opAnd(New Criteria(GetType(PositionWSC), "PositionCategory", MatchType.Exact, PositionCategory))
        aPW = oPWFac.Retrieve(cPW)
        With ddl.Items
            .Clear()
            .Add(New ListItem("Silahkan Pilih", -1))
            For Each oPW As PositionWSC In aPW
                .Add(New ListItem(oPW.PositionCode & " - " & oPW.Description, oPW.PositionCode))
            Next
        End With
    End Sub

    Private Sub ClearForm()
        lblSearchChassis.Visible = False
        ddlClaimType.SelectedIndex = 0
        ddlRefDoc.SelectedIndex = 0
        txtPQRNo.Text = ""
        lblPQRNoVal.Text = ""
        lblClaimNumber.Text = "[Automatically by System]"
        txtRefClaimNumber.Text = ""
        lblRefClaimNumberVal.Text = ""

        txtNoChasis.Text = ""
        hdntxtNoChasis.Value = ""
        hdnPPtxtNoChasis.Value = ""
        ClearChassisInfo()
        lnkbtnPopUpInfoKendaraan.Attributes.Clear()
        lnkbtnPopUpInfoKendaraan.Visible = False
        lnkbtnPopUpRefClaimNumber.Visible = False
        lnkbtnCheckChassis.Attributes("style") = "display:none"
        lnkbtnPPCheckChassis.Attributes("style") = "display:none"
        lblNoChasisVal.Text = ""
        lblAlreadySaled.Text = ""
        lblNoMesinVal.Text = ""
        lblNamaPemilikVal.Text = ""
        'lblTglPKTVal.Text = ""
        icTglKerusakan.Value = DateTime.Today
        lblTglKerusakanVal.Text = ""
        icTglPerbaikan.Value = DateTime.Today
        lblTglPerbaikanVal.Text = ""
        txtOdometer.Text = ""
        lblOdometerVal.Text = ""
        txtGejala.Text = ""
        txtPemeriksaan.Text = ""
        txtHasil.Text = ""
        ddlKodeWSCA.SelectedIndex = 0
        ddlKodeWSCB.SelectedIndex = 0
        ddlKodeWSCC.SelectedIndex = 0
        txtNotes.Text = ""
    End Sub

    Private Sub fillForm()
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        oWSCHeaderBB = CType(sessHelper.GetSession("WSCHEADERBB"), WSCHeaderBB)
        If Mode = enumMode.Mode.NewItemMode Then
            oWSCHeaderBB = CType(sessHelper.GetSession("NEW_WSCHEADERBB"), WSCHeaderBB)
            ClearForm()
            lblDealerVal.Text = oDealer.DealerCode
            lblDealerSearchTerm1.Text = oDealer.SearchTerm1
            lblDealerName.Text = oDealer.DealerName
            lblTglKirimVal.Text = Today
        ElseIf Mode = enumMode.Mode.EditMode OrElse Mode = enumMode.Mode.ViewMode Then
            lblDealerVal.Text = oWSCHeaderBB.Dealer.DealerCode
            lblDealerSearchTerm1.Text = oWSCHeaderBB.Dealer.SearchTerm1
            lblDealerName.Text = oWSCHeaderBB.Dealer.DealerName
            lblTglKirimVal.Text = oWSCHeaderBB.CreateDateText
        End If
        If IsNothing(oWSCHeaderBB) Then
            oWSCHeaderBB = New WSCHeaderBB()
        End If

        If screenFrom = "PQR" Then
            ViewState("Mode") = enumMode.Mode.NewItemMode
            Mode = ViewState("Mode")
            oPQRHeaderBB = New PQRHeaderBBFacade(User).Retrieve(pqrId)
            If Not IsNothing(oPQRHeaderBB) AndAlso oPQRHeaderBB.ID > 0 Then
                If Not IsNothing(oPQRHeaderBB.ChassisMasterBB) Then
                    If Not IsNothing(oPQRHeaderBB.ChassisMasterBB.ChassisNumber) Then
                        If oDealer.ID = oPQRHeaderBB.ChassisMasterBB.Dealer.ID Then  '---Jika login dealer id = sold dealer id
                            blnIsSoldDealer = True
                            sessHelper.SetSession("blnLoginIsSoldDealer", blnIsSoldDealer)
                        End If
                        'CheckIsExistChassisMasterBBPKT(oPQRHeaderBB, blnIsSoldDealer)


                        ddlClaimType.SelectedIndex = 1
                        ddlRefDoc.SelectedIndex = 2

                        setDisableKodeKerusakan()
                        ddlClaimType.Enabled = False
                        ddlRefDoc.Enabled = False
                        txtPQRNo.Enabled = False
                        If ddlRefDoc.SelectedIndex = 2 Then
                            txtPQRNo.Visible = False
                            ddlRefDoc.Visible = False
                            lblPQRNoVal.Visible = True
                        End If
                        lnkbtnPopUpInfoDokumen.Visible = False
                        lblSearchChassis.Visible = False
                        If Mode = enumMode.Mode.NewItemMode Then
                            btnSimpan.Visible = bCekPriv
                            btnPrintBarcode.Visible = False
                            btnRilis.Visible = False
                            btnPermintaanBukti.Visible = False
                            dgParts.Columns(0).Visible = False
                        End If
                        txtPQRNo.Text = oPQRHeaderBB.PQRNo
                        lblPQRNoVal.Text = txtPQRNo.Text
                        LoadPQRHeaderBBInfo(oPQRHeaderBB)
                        txtNoChasis.Enabled = False
                    End If
                    dgParts.Columns(0).Visible = False
                End If
            Else
                MessageBox.Show("Nomor Rangka tidak valid")
                Server.Transfer("~/PQR/FrmPQRListBB.aspx")
                Return
            End If

        ElseIf screenFrom = "WSC" Then
            ViewState("Mode") = intViewStateMode
            Mode = intViewStateMode
            oWSCHeaderBB = New WSCHeaderBBFacade(User).Retrieve(wscId)
            If Not IsNothing(oWSCHeaderBB) Then
                sessHelper.SetSession("WSCHEADERBB", oWSCHeaderBB)
                RetrieveWSCHeaderBBDomain()
                setFormView()

                If Mode <> enumMode.Mode.ViewMode Then
                    If ddlClaimType.SelectedValue.Trim = "Z2" Then
                        ddlRefDoc.Enabled = False
                    ElseIf ddlClaimType.SelectedValue.Trim = "Z4" Then
                        ddlRefDoc.Enabled = False
                    ElseIf ddlClaimType.SelectedValue.Trim = "Z6" AndAlso Mode = enumMode.Mode.NewItemMode Then
                        ddlRefDoc.Enabled = True
                    Else
                        ddlRefDoc.Enabled = False
                    End If
                    txtNoChasis.ReadOnly = True
                    lnkbtnPopUpInfoKendaraan.Visible = False
                    lnkbtnCheckChassis.Attributes("style") = "display:none"
                    If ddlRefDoc.SelectedValue = "0" Then  'Referensi Buletin Doc
                        dgOngkosKerja.ShowFooter = False
                        dgParts.ShowFooter = False
                        'dgOngkosKerja.Columns(dgOngkosKerja.Columns.Count - 2).Visible = False
                        dgParts.Columns(dgParts.Columns.Count - 2).Visible = False
                    End If
                End If
                lblTglKirimVal.Text = oWSCHeaderBB.CreateDateText
            End If
        Else
            ViewState("Mode") = intViewStateMode
            Mode = intViewStateMode

            setFormView()
            'ddlClaimType.Enabled = True

            oWSCHeaderBB = New WSCHeaderBB
            sessHelper.SetSession("NEW_WSCHEADERBB", oWSCHeaderBB)
            lblTglKirimVal.Text = oWSCHeaderBB.CreateDateText
        End If

        RefreshGrid()
    End Sub

    Private Sub setEnabledReferensiDokumen(enb As Boolean)

        dgOngkosKerja.Enabled = enb
        dgParts.Enabled = enb
        dgFileWSCEvidenceBB.Enabled = enb
        icTglKerusakan.Enabled = enb
        icTglPerbaikan.Enabled = enb
        txtOdometer.Enabled = enb
        txtGejala.Enabled = enb
        txtPemeriksaan.Enabled = enb
        txtHasil.Enabled = enb
        ddlKodeWSCA.Enabled = enb
        ddlKodeWSCB.Enabled = enb
        ddlKodeWSCC.Enabled = enb
        If ddlRefDoc.SelectedValue = "0" Then 'Buletin Ref Doc
            dgOngkosKerja.ShowFooter = False
            'dgOngkosKerja.Columns(dgOngkosKerja.Columns.Count - 2).Visible = False
            'dgParts.ShowFooter = False
            'dgParts.Columns(dgParts.Columns.Count - 2).Visible = False
        ElseIf ddlRefDoc.SelectedValue = "1" Then
            dgOngkosKerja.ShowFooter = True
            dgOngkosKerja.Columns(dgOngkosKerja.Columns.Count - 2).Visible = True
            'dgParts.ShowFooter = True
            'dgParts.Columns(dgParts.Columns.Count - 2).Visible = True
        End If
    End Sub

    Private Sub RefreshGrid()
        BindOngkosKerja()
        BindOngkosParts()
        BindWSCEvidenceBB()
    End Sub

    Private Sub BindOngkosKerja()
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        If Mode = enumMode.Mode.NewItemMode Then
            Dim arlOngker As ArrayList = CType(sessHelper.GetSession("NEW_ONGKOSKERJABB"), ArrayList)
            dgOngkosKerja.DataSource = arlOngker
            dgOngkosKerja.DataBind()
            If ddlRefDoc.SelectedIndex = 1 AndAlso arlOngker.Count > 0 Then
                For Each item As DataGridItem In dgOngkosKerja.Items
                    If item.ItemType = ListItemType.Item OrElse item.ItemType = ListItemType.AlternatingItem Then
                        Dim txtPostionCodeItem As TextBox = CType(item.FindControl("txtPostionCodeItem"), TextBox)
                        Dim lblPositionCode As Label = CType(item.FindControl("lblPositionCode"), Label)
                        lblPositionCode.Text = txtPostionCodeItem.Text
                        lblPositionCode.Visible = True
                        txtPostionCodeItem.Visible = False
                        Dim txtWorkCodeItem As TextBox = CType(item.FindControl("txtWorkCodeItem"), TextBox)
                        Dim lblWorkCode As Label = CType(item.FindControl("lblWorkCode"), Label)
                        lblWorkCode.Text = txtWorkCodeItem.Text
                        lblWorkCode.Visible = True
                        txtWorkCodeItem.Visible = False
                        Dim lblSearchPositionCodeItem As Label = CType(item.FindControl("lblSearchPositionCodeItem"), Label)
                        lblSearchPositionCodeItem.Visible = False
                        Dim lblSearchWorkCodeItem As Label = CType(item.FindControl("lblSearchWorkCodeItem"), Label)
                        lblSearchWorkCodeItem.Visible = False
                    End If
                Next
            End If
        Else
            dgOngkosKerja.DataSource = CType(sessHelper.GetSession("ONGKOSKERJABB"), ArrayList)
            dgOngkosKerja.DataBind()
        End If
    End Sub

    Private Sub BindOngkosParts()
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        If Mode = enumMode.Mode.NewItemMode Then
            dgParts.DataSource = CType(sessHelper.GetSession("NEW_ONGKOSPARTSBB"), ArrayList)
            dgParts.DataBind()
        Else
            dgParts.DataSource = CType(sessHelper.GetSession("ONGKOSPARTSBB"), ArrayList)
            dgParts.DataBind()
        End If
    End Sub

    Private Sub setDisableKodeKerusakan()
        'Dim blnIsExistPosition As Boolean = False
        'Mode = CType(ViewState("Mode"), enumMode.Mode)
        'Dim _arrONGKOSKERJA As ArrayList = CType(sessHelper.GetSession("NEW_ONGKOSKERJABB"), ArrayList)
        'If Mode = enumMode.Mode.EditMode Then
        '    _arrONGKOSKERJA = CType(sessHelper.GetSession("ONGKOSKERJABB"), ArrayList)
        'End If
        If ddlClaimType.SelectedValue = "Z4" Then
            '    ddlRefDoc.SelectedValue = 0
            '    For Each _objWSCDetailBB As WSCDetailBB In _arrONGKOSKERJA
            '        If _objWSCDetailBB.PositionCode = "XEE999" Then
            '            blnIsExistPosition = True
            '            Exit For
            '        End If
            '    Next
            'ElseIf ddlClaimType.SelectedValue = "Z2" Then
            '    ddlRefDoc.SelectedValue = 1
            '    blnIsExistPosition = False
            'Else
            '    If ddlRefDoc.SelectedValue = 0 Then
            '        For Each _objWSCDetailBB As WSCDetailBB In _arrONGKOSKERJA
            '            If _objWSCDetailBB.PositionCode = "XEE999" Then
            '                blnIsExistPosition = True
            '                Exit For
            '            End If
            '        Next
            '    End If
            'End If
            'If blnIsExistPosition Then
            ddlKodeWSCA.SelectedIndex = 0
            'ddlKodeWSCA.Enabled = False
            ddlKodeWSCB.SelectedIndex = 0
            'ddlKodeWSCB.Enabled = False
            ddlKodeWSCC.SelectedIndex = 0
            'ddlKodeWSCC.Enabled = False
            'Else
            '    ddlKodeWSCA.Enabled = True
            '    ddlKodeWSCB.Enabled = True
            '    ddlKodeWSCC.Enabled = True
        End If
    End Sub

    Private Sub setEnbDisableForm(enb As Boolean)
        ddlClaimType.Enabled = enb
        ddlRefDoc.Enabled = enb
        txtPQRNo.Enabled = enb
        lblPQRNoVal.Visible = enb
        lnkbtnPopUpInfoDokumen.Visible = enb
        lnkbtnPopUpRefClaimNumber.Visible = enb
        txtRefClaimNumber.Enabled = enb
        txtNoChasis.Enabled = enb
        lblSearchChassis.Visible = enb

        If enb Then
            lnkbtnCheckChassis.Attributes("style") = "display:table-row"
        Else
            lnkbtnCheckChassis.Attributes("style") = "display:none"
        End If

        icTglKerusakan.Enabled = enb
        icTglPerbaikan.Enabled = enb
        txtOdometer.Enabled = enb
        txtGejala.Enabled = enb
        txtPemeriksaan.Enabled = enb
        txtHasil.Enabled = enb
        ddlKodeWSCA.Enabled = enb
        ddlKodeWSCB.Enabled = enb
        ddlKodeWSCC.Enabled = enb
        txtNotes.Enabled = enb
        btnPrintBarcode.Visible = Not enb
        dgParts.Columns(0).Visible = Not enb

        'If oWSCHeaderBB.Status = CType(enumStatusWSC.Status.Proses, String) Then
        '    btnRilis.Visible = False
        '    btnPrintBarcode.Visible = True
        '    If Mode = enumMode.Mode.NewItemMode Then
        '        btnPrintBarcode.Visible = False
        '    End If
        'Else
        '    btnRilis.Visible = True
        '    If Mode = enumMode.Mode.NewItemMode Then
        '        btnRilis.Visible = False
        '    End If
        'End If

        btnRilis.Visible = False
        btnPrintBarcode.Visible = False
        If oWSCHeaderBB.Status = CType(enumStatusWSC.Status.Baru, String) Then
            btnRilis.Visible = True
        Else
            btnPrintBarcode.Visible = True
        End If

        btnPermintaanBukti.Visible = enb
        btnSimpan.Visible = enb

        dgOngkosKerja.Enabled = enb
        dgParts.Enabled = enb
        dgFileWSCEvidenceBB.Enabled = enb
        dgOngkosKerja.ShowFooter = enb
        dgParts.ShowFooter = enb
        dgFileWSCEvidenceBB.ShowFooter = enb
        dgOngkosKerja.Columns(dgOngkosKerja.Columns.Count - 2).Visible = enb
        dgParts.Columns(dgParts.Columns.Count - 2).Visible = enb
        dgFileWSCEvidenceBB.Columns(dgFileWSCEvidenceBB.Columns.Count - 1).Visible = enb

    End Sub

    Private Sub setFormView()
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        If Mode = enumMode.Mode.NewItemMode Then
            setEnbDisableForm(False)
            btnSimpan.Visible = bCekPriv
            btnPrintBarcode.Visible = False
            btnRilis.Visible = False
            btnBatal.Visible = False
            btnPermintaanBukti.Visible = False
            ddlClaimType.Enabled = True
            dgOngkosKerja.ShowFooter = True
            dgParts.ShowFooter = True
            dgFileWSCEvidenceBB.ShowFooter = True
            dgOngkosKerja.Columns(dgOngkosKerja.Columns.Count - 2).Visible = True
            dgParts.Columns(0).Visible = False
            dgParts.Columns(dgParts.Columns.Count - 2).Visible = True
            dgFileWSCEvidenceBB.Columns(dgFileWSCEvidenceBB.Columns.Count - 1).Visible = True

            screenFrom = Request.QueryString("screenFrom")
            If screenFrom = "PQR" Then
                btnBatal.Visible = True
            Else
                btnBatal.Visible = False
            End If
            If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                CaptionNotes.Visible = True
                txtNotes.Visible = True
                txtNotes.Enabled = False
            Else
                CaptionNotes.Visible = True
                txtNotes.Visible = True
                txtNotes.Enabled = True
            End If
            ddlKodeWSCA.Enabled = True
            ddlKodeWSCB.Enabled = True
            ddlKodeWSCC.Enabled = True
        ElseIf Mode = enumMode.Mode.EditMode Then
            'If ddlRefDoc.SelectedIndex = 1 Then
            '    ddlKodeWSCA.Enabled = False
            '    ddlKodeWSCB.Enabled = False
            '    ddlKodeWSCC.Enabled = False
            'Else
            ddlKodeWSCA.Enabled = True
            ddlKodeWSCB.Enabled = True
            ddlKodeWSCC.Enabled = True
            'End If

            If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                setEnbDisableForm(True)
            Else
                setEnbDisableForm(False)
                ddlKodeWSCA.Enabled = False
                ddlKodeWSCB.Enabled = False
                ddlKodeWSCC.Enabled = False
            End If

            ddlClaimType.Enabled = False
            ddlRefDoc.Enabled = False
            txtPQRNo.Enabled = False
            If ddlRefDoc.SelectedIndex = 2 Then
                txtPQRNo.Visible = False
                ddlRefDoc.Visible = False
                lblPQRNoVal.Visible = True
            End If
            txtRefClaimNumber.Enabled = False
            lnkbtnPopUpInfoDokumen.Visible = False
            lnkbtnPopUpRefClaimNumber.Visible = False
            lblSearchChassis.Visible = False
            lnkbtnCheckChassis.Visible = False
            btnSimpan.Visible = bCekPriv
            btnBatal.Visible = True
            btnPrintBarcode.Visible = False
            If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                txtNotes.Enabled = False
                CaptionNotes.Visible = True
                txtNotes.Visible = True
                btnPermintaanBukti.Visible = False
                btnRilis.Visible = True
            Else
                CaptionNotes.Visible = True
                txtNotes.Enabled = True
                txtNotes.Visible = True
                btnPermintaanBukti.Visible = True
                btnRilis.Visible = False
            End If
            btnPrintBarcode.Visible = False
            dgParts.Columns(0).Visible = False

        ElseIf Mode = enumMode.Mode.ViewMode Then
            setEnbDisableForm(False)
            btnPrintBarcode.Visible = True
            dgParts.Columns(0).Visible = True
            dgParts.Enabled = True
            btnRilis.Visible = False
            If ddlRefDoc.SelectedIndex = 2 Then
                txtPQRNo.Visible = False
                ddlRefDoc.Visible = False
                lblPQRNoVal.Visible = True
            End If
        End If
    End Sub

    Private Sub SetControlKodeKerusakan(ByVal oWSCHdr As WSCHeaderBB)
        Dim IsImplementKodeKerusakan As Boolean = False
        Mode = CType(ViewState("Mode"), enumMode.Mode)

        ddlKodeWSCA.Enabled = False
        ddlKodeWSCB.Enabled = False
        ddlKodeWSCC.Enabled = False
        If Mode = enumMode.Mode.NewItemMode OrElse Mode = enumMode.Mode.EditMode Then
            If CType(sessHelper.GetSession("DEALER"), Dealer).Title = EnumDealerTittle.DealerTittle.DEALER Then
                If SecurityProvider.Authorize(Context.User, SR.WSCSaveData_Privilege) Then
                    ddlKodeWSCA.Enabled = True
                    ddlKodeWSCB.Enabled = True
                    ddlKodeWSCC.Enabled = True
                End If
            Else
            End If
        End If

        If Mode = enumMode.Mode.NewItemMode And Now >= _dtKodeKerusakanStartDate Then
            IsImplementKodeKerusakan = True
        Else
            If Not IsNothing(oWSCHdr) AndAlso oWSCHdr.ID > 0 Then
                If oWSCHdr.CreatedTime >= _dtKodeKerusakanStartDate Then
                    IsImplementKodeKerusakan = True
                End If
            End If
        End If
        If IsImplementKodeKerusakan Then
            tblKodeKerusakan.Visible = True
        Else
            tblKodeKerusakan.Visible = False
        End If
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
                For Each obj As WSCEvidenceBB In AttachmentCollection
                    If Not IsNothing(obj.AttachmentData) Then
                        TargetFInfo = New FileInfo(TargetDirectory + obj.PathFile)
                        TempFInfo = New FileInfo(TempDirectory + obj.PathFile)

                        If TempFInfo.Exists Then
                            If Not TargetFInfo.Directory.Exists Then
                                Directory.CreateDirectory(TargetFInfo.DirectoryName)
                            End If
                            TempFInfo.MoveTo(TargetFInfo.FullName)
                        End If
                        obj.AttachmentData.SaveAs(TargetDirectory + obj.PathFile)
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

    Function GetNextClaimNumberWSC(ByVal DealerID As Integer) As String
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCHeader), "Dealer.ID", MatchType.Exact, DealerID))
        Dim critBB As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCHeaderBB), "Dealer.ID", MatchType.Exact, DealerID))
        Dim claimNumber As Integer = 0
        Dim arrlNmHeader As ArrayList = New WSCHeaderFacade(User).Retrieve(crit)
        Dim arrlBBHeader As ArrayList = New WSCHeaderBBFacade(User).Retrieve(critBB)
        For Each item As WSCHeader In arrlNmHeader
            Try
                Dim dbClaimNumber As Integer = CType(item.ClaimNumber, Integer)
                If dbClaimNumber > claimNumber Then
                    claimNumber = dbClaimNumber
                End If
            Catch
            End Try
        Next
        For Each item As WSCHeaderBB In arrlBBHeader
            Try
                Dim dbClaimNumber As Integer = CType(item.ClaimNumber, Integer)
                If dbClaimNumber > claimNumber Then
                    claimNumber = dbClaimNumber
                End If
            Catch
            End Try
        Next
        Dim _return As String = (claimNumber + 1).ToString("000000.##")
        Dim q = "asdasd"
        Return _return
    End Function

    Private Sub RetrieveWSCHeaderBBDomain()
        oDealer = CType(sessHelper.GetSession("DEALER"), Dealer)

        Mode = CType(ViewState("Mode"), enumMode.Mode)
        oWSCHeaderBB = CType(sessHelper.GetSession("WSCHEADERBB"), WSCHeaderBB)
        If IsNothing(oWSCHeaderBB) Then
            oWSCHeaderBB = New WSCHeaderBB
        End If

        lblDealerVal.Text = oWSCHeaderBB.Dealer.DealerCode
        lblDealerSearchTerm1.Text = oWSCHeaderBB.Dealer.SearchTerm1
        lblDealerName.Text = oWSCHeaderBB.Dealer.DealerName

        ddlClaimType.SelectedValue = oWSCHeaderBB.ClaimType
        txtPQRNo.Text = oWSCHeaderBB.PQR
        'Cek di Dokumen Buletin Reference
        If txtPQRNo.Text.Trim <> "" Then
            Dim oRecallCategory As RecallCategory = New RecallCategoryFacade(User).Retrieve(txtPQRNo.Text.Trim)
            If Not IsNothing(oRecallCategory) Then
                If oRecallCategory.ID <> 0 Then
                    ddlRefDoc.SelectedValue = 0   'Select ke Dokumen Buletin Reference
                Else
                    'Cek di Dokumen PQR Reference
                    Dim oPQRHdr As PQRHeaderBB = New PQRHeaderBBFacade(User).Retrieve(txtPQRNo.Text.Trim)
                    If Not IsNothing(oPQRHdr) AndAlso oPQRHdr.ID <> 0 Then
                        ddlRefDoc.SelectedValue = 1
                    End If
                End If
            Else

                'Cek di Dokumen PQR Reference
                Dim oPQRHdr As PQRHeaderBB = New PQRHeaderBBFacade(User).Retrieve(txtPQRNo.Text.Trim)
                If Not IsNothing(oPQRHdr) AndAlso oPQRHdr.ID <> 0 Then
                    ddlRefDoc.SelectedValue = 1
                End If
                'Else
                '    ddlRefDoc.SelectedValue = 0
            End If
        End If

        lblClaimNumber.Text = oWSCHeaderBB.ClaimNumber
        txtRefClaimNumber.Text = oWSCHeaderBB.RefClaimNumber
        txtNoChasis.Text = oWSCHeaderBB.ChassisMasterBB.ChassisNumber
        hdntxtNoChasis.Value = txtNoChasis.Text
        lnkbtnCheckChassisClick()
        lblNoChasisVal.Text = oWSCHeaderBB.ChassisMasterBB.ChassisNumber
        LoadChassisInfo(oWSCHeaderBB.ChassisMasterBB)
        icTglKerusakan.Value = oWSCHeaderBB.FailureDate
        lblTglKerusakanVal.Text = oWSCHeaderBB.FailureDate
        icTglPerbaikan.Value = oWSCHeaderBB.ServiceDate
        lblTglPerbaikanVal.Text = oWSCHeaderBB.ServiceDate
        txtOdometer.Text = oWSCHeaderBB.Miliage
        txtGejala.Text = oWSCHeaderBB.Description
        txtPemeriksaan.Text = oWSCHeaderBB.Causes
        txtNotes.Text = oWSCHeaderBB.Notes
        txtHasil.Text = oWSCHeaderBB.Results

        If oWSCHeaderBB.CodeA = "" Then
            ddlKodeWSCA.SelectedIndex = 0
        Else
            ddlKodeWSCA.SelectedValue = oWSCHeaderBB.CodeA
        End If
        If oWSCHeaderBB.CodeB = "" Then
            ddlKodeWSCB.SelectedIndex = 0
        Else
            ddlKodeWSCB.SelectedValue = oWSCHeaderBB.CodeB
        End If
        If oWSCHeaderBB.CodeC = "" Then
            ddlKodeWSCC.SelectedIndex = 0
        Else
            ddlKodeWSCC.SelectedValue = oWSCHeaderBB.CodeC
        End If

        Dim arrWSCDetailBBLabor As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(WSCDetailBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(WSCDetailBB), "WSCHeaderBB.ID", MatchType.Exact, oWSCHeaderBB.ID))
        criterias.opAnd(New Criteria(GetType(WSCDetailBB), "WSCType", MatchType.Exact, "L"))
        arrWSCDetailBBLabor = New WSCDetailBBFacade(User).Retrieve(criterias)
        If IsNothing(arrWSCDetailBBLabor) And arrWSCDetailBBLabor.Count = 0 Then
            arrWSCDetailBBLabor = New ArrayList
        End If

        Dim arrWSCDetailBBPart As ArrayList = New ArrayList
        Dim criterias2 As New CriteriaComposite(New Criteria(GetType(WSCDetailBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias2.opAnd(New Criteria(GetType(WSCDetailBB), "WSCHeaderBB.ID", MatchType.Exact, oWSCHeaderBB.ID))
        criterias2.opAnd(New Criteria(GetType(WSCDetailBB), "WSCType", MatchType.Exact, "P"))
        arrWSCDetailBBPart = New WSCDetailBBFacade(User).Retrieve(criterias2)
        If IsNothing(arrWSCDetailBBPart) And arrWSCDetailBBPart.Count = 0 Then
            arrWSCDetailBBPart = New ArrayList
        End If

        sessHelper.SetSession("ONGKOSKERJABB", arrWSCDetailBBLabor)
        sessHelper.SetSession("DELETEDONGKOSKERJABB", New ArrayList)
        sessHelper.SetSession("ONGKOSPARTSBB", arrWSCDetailBBPart)
        sessHelper.SetSession("DELETEDPARTSBB", New ArrayList)
        sessHelper.SetSession("WSCEVIDENCEBB", GetAttachmentList(oWSCHeaderBB.wSCEvidenceBBs))
        sessHelper.SetSession("DELETEDWSEVIDENCEBB", New ArrayList)
        RefreshGrid()

    End Sub

    Private Sub BindWSCHeaderBBDomain()
        oDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        oWSCHeaderBB = CType(sessHelper.GetSession("WSCHEADERBB"), WSCHeaderBB)
        If Mode = enumMode.Mode.NewItemMode Then
            oWSCHeaderBB = CType(sessHelper.GetSession("NEW_WSCHEADERBB"), WSCHeaderBB)
        End If
        If IsNothing(oWSCHeaderBB) Then
            oWSCHeaderBB = New WSCHeaderBB
        End If

        oWSCHeaderBB.ClaimType = ddlClaimType.SelectedValue
        If Mode = enumMode.Mode.NewItemMode Then
            oWSCHeaderBB.Dealer = oDealer
            oWSCHeaderBB.ClaimNumber = GetNextClaimNumberWSC(oDealer.ID)
        ElseIf Mode = enumMode.Mode.EditMode Then
            oWSCHeaderBB.ClaimNumber = lblClaimNumber.Text
        End If

        oWSCHeaderBB.RefClaimNumber = txtRefClaimNumber.Text
        oWSCHeaderBB.ChassisMasterBB = oChassis
        oWSCHeaderBB.Miliage = CInt(txtOdometer.Text)
        oWSCHeaderBB.ServiceDate = icTglPerbaikan.Value
        oWSCHeaderBB.FailureDate = icTglKerusakan.Value
        oWSCHeaderBB.Results = KTB.DNet.Utility.CommonFunction.RemoveWhiteSpace(txtHasil.Text)
        oWSCHeaderBB.Notes = KTB.DNet.Utility.CommonFunction.RemoveWhiteSpace(txtNotes.Text)
        oWSCHeaderBB.Causes = KTB.DNet.Utility.CommonFunction.RemoveWhiteSpace(txtPemeriksaan.Text)
        oWSCHeaderBB.Description = KTB.DNet.Utility.CommonFunction.RemoveWhiteSpace(txtGejala.Text)

        'oWSCHeaderBB.Description = ""

        oWSCHeaderBB.PQR = txtPQRNo.Text
        Dim intPQRStatus As Integer = Nothing
        Dim oPQRHeaderBB As PQRHeaderBB = New PQRHeaderBBFacade(User).Retrieve(txtPQRNo.Text)
        If Not IsNothing(oPQRHeaderBB) AndAlso oPQRHeaderBB.ID > 0 Then
            intPQRStatus = oPQRHeaderBB.RowStatus
        End If
        oWSCHeaderBB.PQRStatus = intPQRStatus
        oWSCHeaderBB.Status = enumStatusWSC.Status.Baru

        If tblKodeKerusakan.Visible And ddlKodeWSCA.Enabled Then
            If Me.ddlKodeWSCA.SelectedValue <> "-1" Then
                oWSCHeaderBB.CodeA = Me.ddlKodeWSCA.SelectedValue
            End If
            If Me.ddlKodeWSCB.SelectedValue <> "-1" Then
                oWSCHeaderBB.CodeB = Me.ddlKodeWSCB.SelectedValue
            End If
            If Me.ddlKodeWSCC.SelectedValue <> "-1" Then
                oWSCHeaderBB.CodeC = Me.ddlKodeWSCC.SelectedValue
            End If
        End If

        Dim _arrWSCEVIDENCE As ArrayList = CType(sessHelper.GetSession("WSCEVIDENCEBB"), ArrayList)
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        If Mode = enumMode.Mode.NewItemMode Then
            _arrWSCEVIDENCE = CType(sessHelper.GetSession("NEW_WSCEVIDENCEBB"), ArrayList)
        End If

        Dim strEvidenceTypePhoto As String = String.Empty
        Dim strEvidenceDmgPart As String = String.Empty
        Dim strEvidenceInvoice As String = String.Empty
        Dim strEvidenceRepair As String = String.Empty
        Dim strEvidenceWSCLetter As String = String.Empty
        Dim strEvidenceWSCTechnical As String = String.Empty

        If _arrWSCEVIDENCE.Count > 0 Then
            For Each objWSCEvidenceBB As WSCEvidenceBB In _arrWSCEVIDENCE
                If objWSCEvidenceBB.EvidenceType = "5" Then   'Type Photo
                    strEvidenceTypePhoto = "X"
                End If
                If objWSCEvidenceBB.EvidenceType = "4" Then   'Type Part Bekas
                    strEvidenceDmgPart = "X"
                End If
                If objWSCEvidenceBB.EvidenceType = "0" Then   'Type Kwitansi WSC
                    strEvidenceInvoice = "X"
                End If
                If objWSCEvidenceBB.EvidenceType = "3" Then   'Type Repair/WO
                    strEvidenceRepair = "X"
                End If
                If objWSCEvidenceBB.EvidenceType = "1" Then   'Type Surat WSC
                    strEvidenceWSCLetter = "X"
                End If
                If objWSCEvidenceBB.EvidenceType = "2" Then   'Type Teknikal WSC
                    strEvidenceWSCTechnical = "X"
                End If
            Next
        End If

        oWSCHeaderBB.EvidencePhoto = strEvidenceTypePhoto
        oWSCHeaderBB.EvidenceDmgPart = strEvidenceDmgPart
        oWSCHeaderBB.EvidenceInvoice = strEvidenceInvoice
        oWSCHeaderBB.EvidenceRepair = strEvidenceRepair
        oWSCHeaderBB.EvidenceWSCLetter = strEvidenceWSCLetter
        oWSCHeaderBB.EvidenceWSCTechnical = strEvidenceWSCTechnical

        Mode = CType(ViewState("Mode"), enumMode.Mode)
        If Mode = enumMode.Mode.NewItemMode Then
            sessHelper.SetSession("NEW_WSCHEADERBB", oWSCHeaderBB)
        Else
            sessHelper.SetSession("WSCHEADERBB", oWSCHeaderBB)
        End If

    End Sub

    Private Function ValidateSaveData() As Boolean
        Dim blnIsExistPositionXEE999 As Boolean = False

        Mode = CType(ViewState("Mode"), enumMode.Mode)

        If ddlClaimType.SelectedIndex = 0 Then
            MessageBox.Show("Silakan masukan Tipe Claim")
            Return False
        End If

        If txtPQRNo.Text = String.Empty Then
            MessageBox.Show("Silakan masukan Nomor Referensi Dokumen")
            Return False
        End If

        If txtNoChasis.Text = String.Empty Then
            MessageBox.Show("Nomor Rangka masih kosong")
            Return False
        End If

        If oChassisFacade.IsExist(txtNoChasis.Text) Then
            txtNoChasis.ForeColor = Color.Black
            LoadChassisInfo(CType(oChassisFacade.Retrieve(txtNoChasis.Text), ChassisMasterBB))
        Else
            txtNoChasis.ForeColor = Color.Red
            ClearChassisInfo()
            MessageBox.Show("Nomor Rangka tidak terdaftar")
            Return False
        End If

        If icTglKerusakan.Value.ToString = "" OrElse icTglKerusakan.Value.ToString = "01/01/0001 0:00:00" Then
            MessageBox.Show("Silahkan masukan Tanggal Kerusakan")
            Return False
        End If
        'If CType(icTglKerusakan.Value.ToString("yyyyMMdd"), Integer) <= CType(DateTime.Now.ToString("yyyyMMdd"), Integer) AndAlso _
        '    CType(icTglKerusakan.Value.ToString("yyyyMMdd"), Integer) >= CType(DateTime.Now.ToString("yyyyMMdd"), Integer) Then
        '    MessageBox.Show("Tanggal Kerusakan tidak boleh lebih besar dari tanggal hari ini")
        '    Return False
        'End If

        If txtOdometer.Text = String.Empty OrElse CType(txtOdometer.Text.Trim, Integer) = 0 Then
            MessageBox.Show("Silahkan masukan Jarak Tempuh")
            Return False
        End If

        '---- Cek Tgl Kerusakan dan Odometer
        Dim criteriaas As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCHeaderBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriaas.opAnd(New Criteria(GetType(WSCHeaderBB), "ChassisMasterBB.ChassisNumber", MatchType.Exact, txtNoChasis.Text))
        Dim objWSCHeaderBBFacade As WSCHeaderBBFacade
        objWSCHeaderBBFacade = New WSCHeaderBBFacade(User)
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(WSCHeaderBB), "CreatedTime", Sort.SortDirection.DESC))
        Dim arrWSC As ArrayList = objWSCHeaderBBFacade.RetrieveByCriteria(criteriaas, sortColl)
        If Not IsNothing(arrWSC) AndAlso arrWSC.Count > 0 Then
            Dim oWSCHeaderBB As WSCHeaderBB = CType(arrWSC(0), WSCHeaderBB)
            If CType(icTglKerusakan.Value.ToString("yyyyMMdd"), Integer) < CType(oWSCHeaderBB.FailureDate.ToString("yyyyMMdd"), Integer) Then
                MessageBox.Show("Tanggal kerusakan tidak boleh kurang dari tanggal kerusakan sebelumnya")
                Return False
            End If
            If Mode <> enumMode.Mode.EditMode Then
                If CType(icTglKerusakan.Value.ToString("yyyyMMdd"), Integer) > CType(DateTime.Now.ToString("yyyyMMdd"), Integer) Then
                    MessageBox.Show("Tanggal kerusakan tidak boleh lebih besar dari tanggal hari ini")
                    Return False
                End If
            End If
            If Mode <> enumMode.Mode.EditMode Then
                If CType(txtOdometer.Text, Integer) <= CType(oWSCHeaderBB.Miliage, Integer) Then
                    MessageBox.Show("Jarak tempuh tidak boleh kurang dari jarak tempuh sebelumnya")
                    Return False
                End If
            Else
                If CType(icTglKerusakan.Value.ToString("yyyyMMdd"), Integer) > CType(oWSCHeaderBB.CreatedTime.ToString("yyyyMMdd"), Integer) Then
                    MessageBox.Show("Tanggal kerusakan tidak boleh kurang dari tanggal kirim")
                    Return False
                End If
                'If CType(icTglKerusakan.Value.ToString("yyyyMMdd"), Integer) > CType(DateTime.Now.ToString("yyyyMMdd"), Integer) Then
                '    MessageBox.Show("Tanggal kerusakan tidak boleh lebih besar dari tanggal hari ini")
                '    Return False
                'End If
                If CType(icTglPerbaikan.Value.ToString("yyyyMMdd"), Integer) > CType(oWSCHeaderBB.CreatedTime.ToString("yyyyMMdd"), Integer) Then
                    MessageBox.Show("Tanggal perbaikan tidak boleh kurang dari tanggal kirim")
                    Return False
                End If
                'If CType(icTglPerbaikan.Value.ToString("yyyyMMdd"), Integer) > CType(DateTime.Now.ToString("yyyyMMdd"), Integer) Then
                '    MessageBox.Show("Tanggal perbaikan tidak boleh lebih besar dari tanggal hari ini")
                '    Return False
                'End If
                If CType(txtOdometer.Text, Integer) < CType(oWSCHeaderBB.Miliage, Integer) Then
                    MessageBox.Show("Jarak tempuh tidak boleh kurang dari jarak tempuh sebelumnya")
                    Return False
                End If
            End If

            Dim tglPerbaikan As Date = icTglPerbaikan.Value
            Dim tglClaim As Date = oWSCHeaderBB.CreatedTime

            'repair - today > 14 hari
            Dim RepMinToday As TimeSpan = oWSCHeaderBB.ServiceDate - DateTime.Now


            If CType(icTglPerbaikan.Value, Date) < CType(icTglKerusakan.Value, Date) Then
                MessageBox.Show("Tanggal Perbaikan tidak boleh kecil dari Tanggal Kerusakan")
                Return False
            End If

            If RepMinToday.TotalDays >= 14 Then
                MessageBox.Show("Tanggal Perbaikan tidak boleh lebih dari 14 hari tanggal input claim")
                Return False
            End If
            'If Mode <> enumMode.Mode.EditMode Then
            '    If CType(icTglPerbaikan.Value.ToString("yyyyMMdd"), Integer) < CType(DateTime.Now.ToString("yyyyMMdd"), Integer) Then
            '        MessageBox.Show("Tanggal Perbaikan tidak boleh kurang dari tanggal input claim")
            '    End If
            'End If

            '============================= PKT Date
            'If Not IsNothing(oChassisMasterBBPKT.PKTDate) Then
            '    Dim repServicePKT As TimeSpan = icTglPerbaikan.Value - oChassisMasterBBPKT.PKTDate
            '    If repServicePKT.Days < 0 Then
            '        Return False
            '    End If
            'ElseIf Not IsNothing(oChassisMasterBBPKT.ChassisMasterBBs.EndCustomer.FakturDate) Then
            '    Dim repServicePKT As TimeSpan = icTglPerbaikan.Value - oChassisMasterBBPKT.ChassisMasterBBs.EndCustomer.FakturDate
            '    If repServicePKT.Days < 0 Then
            '        Return False
            '    End If
            'ElseIf Not IsNothing(oChassisMasterBBPKT.ChassisMasterBBs.EndCustomer.OpenFakturDate) Then
            '    Dim repServicePKT As TimeSpan = icTglPerbaikan.Value - oChassisMasterBBPKT.ChassisMasterBBs.EndCustomer.OpenFakturDate
            '    If repServicePKT.Days < 0 Then
            '        Return False
            '    End If
            'End If
        End If

        If txtGejala.Text = String.Empty AndAlso ddlClaimType.SelectedValue <> "Z4" Then
            MessageBox.Show("Silahkan masukan Gejala Kerusakan")
            Return False
        End If

        '=============================================================================================================

        Dim intVechileTypeID1 As Integer
        Dim intVechileTypeID2 As Integer

        If ddlRefDoc.SelectedValue = "1" Then 'PQR Ref Doc
            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(PQRHeaderBB), "RowStatus", MatchType.No, CType(DBRowStatus.Deleted, Short)))
            criterias2.opAnd(New Criteria(GetType(PQRHeaderBB), "PQRNo", MatchType.Exact, txtPQRNo.Text.Trim))
            Dim _arrPQR As ArrayList = New PQRHeaderBBFacade(User).Retrieve(criterias2)
            If _arrPQR.Count > 0 Then
                Dim objPQRHeaderBB As PQRHeaderBB = CType(_arrPQR(0), PQRHeaderBB)
                intVechileTypeID1 = objPQRHeaderBB.ChassisMasterBB.VechileColor.VechileType.ID
            End If
        End If

        Dim strChassisNumber As String = txtNoChasis.Text.Trim()
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Dim criterias As New CriteriaComposite(New Criteria(GetType(ChassisMasterBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(ChassisMasterBB), "ChassisNumber", MatchType.Exact, strChassisNumber))
        criterias.opAnd(New Criteria(GetType(ChassisMasterBB), "Category.ProductCategory.Code", MatchType.Exact, companyCode))
        Dim ChassisColl As ArrayList = New ChassisMasterBBFacade(User).Retrieve(criterias)
        If ChassisColl.Count > 0 Then
            oChassis = CType(ChassisColl(0), ChassisMasterBB)
            If ddlRefDoc.SelectedValue = "1" Then 'PQR Ref Doc  untuk Tipe Claim Z2 atau Z6
                intVechileTypeID2 = oChassis.VechileColor.VechileType.ID
                If intVechileTypeID1 <> intVechileTypeID2 Then
                    txtNoChasis.ForeColor = Color.Red
                    MessageBox.Show("Tipe kendaraan harus sama dengan tipe kendaraan pada Dokumen PQR.")
                    txtNoChasis.Text = hdntxtNoChasis.Value
                    Return False
                End If
            End If
            LoadChassisInfo(oChassis)
        Else
            txtNoChasis.ForeColor = Color.Red
            MessageBox.Show("No Rangka tidak terdaftar.")
            txtNoChasis.Text = hdntxtNoChasis.Value
            Return False
        End If

        If tblKodeKerusakan.Visible And ddlKodeWSCA.Enabled Then
            If ddlClaimType.SelectedValue = "Z2" OrElse ddlClaimType.SelectedValue = "Z6" Then
                If ddlRefDoc.SelectedValue = "1" Then   ' -- PQR Ref
                    If ddlKodeWSCA.SelectedIndex < 1 And blnIsExistPositionXEE999 = False Then
                        MessageBox.Show("Silahkan isi kode kerusakan A terlebih dahulu ")
                        Return False
                    End If
                    If ddlKodeWSCB.SelectedIndex < 1 And blnIsExistPositionXEE999 = False Then
                        MessageBox.Show("Silahkan isi kode kerusakan B terlebih dahulu ")
                        Return False
                    End If
                    If ddlKodeWSCC.SelectedIndex < 1 And blnIsExistPositionXEE999 = False Then
                        MessageBox.Show("Silahkan isi kode kerusakan C terlebih dahulu ")
                        Return False
                    End If
                End If
            End If

            If dgOngkosKerja.Items.Count < 1 Then
                MessageBox.Show("Ongkos kerja harus di isi")
                Return False
            End If
        End If

        If Not LoopDgrid("ALLGRID", Nothing, Nothing, "Simpan", 0) Then
            Return False
        End If

        If Mode = enumMode.Mode.NewItemMode Then
            Dim arrOngKer As ArrayList = CType(sessHelper.GetSession("NEW_ONGKOSKERJABB"), ArrayList)
            If arrOngKer.Count = 0 AndAlso ddlRefDoc.SelectedIndex <> 1 Then
                MessageBox.Show("Data Ongkos Kerja harus diisi")
                Return False
            End If
            'Dim arrParts As ArrayList = CType(sessHelper.GetSession("NEW_ONGKOSPARTSBB"), ArrayList)
            'If arrParts.Count = 0 AndAlso ddlRefDoc.SelectedIndex <> 1 Then
            '    MessageBox.Show("Data Parts harus diisi")
            '    Return False
            'End If
        Else
            Dim arrOngKer As ArrayList = CType(sessHelper.GetSession("ONGKOSKERJABB"), ArrayList)
            If arrOngKer.Count = 0 AndAlso ddlRefDoc.SelectedIndex <> 1 Then
                MessageBox.Show("Data Ongkos Kerja harus diisi")
                Return False
            End If
            'Dim arrParts As ArrayList = CType(sessHelper.GetSession("ONGKOSPARTSBB"), ArrayList)
            'If arrParts.Count = 0 AndAlso ddlRefDoc.SelectedIndex <> 1 Then
            '    MessageBox.Show("Data Parts harus diisi")
            '    Return False
            'End If
        End If

        Dim _arrONGKER As ArrayList = CType(sessHelper.GetSession("ONGKOSKERJABB"), ArrayList)
        If Mode = enumMode.Mode.NewItemMode Then
            _arrONGKER = CType(sessHelper.GetSession("NEW_ONGKOSKERJABB"), ArrayList)
        End If
        For Each _objWSCDetailBB As WSCDetailBB In _arrONGKER
            If _objWSCDetailBB.PositionCode.ToUpper = "XEE999" AndAlso _objWSCDetailBB.WorkCode = "99" Then
                blnIsExistPositionXEE999 = True
                Exit For
            End If
        Next
        If (ddlClaimType.SelectedValue = "Z4" OrElse ddlClaimType.SelectedValue = "Z6") AndAlso ddlRefDoc.SelectedValue = "0" Then   ' --> Service Bulletin Ref
            If blnIsExistPositionXEE999 = True Then
                MessageBox.Show("Kode Posisi XEE999 dan Kode Kerja 99 tidak boleh di input \nuntuk tipe Claim Z4 atau Z6 tipe Service Buletin Reference")
                Return False
            End If
        End If


        '========= Validasi Attacment tipe Kwitansi =======================================================================
        Dim _arrONGKOSKERJA As ArrayList = CType(sessHelper.GetSession("ONGKOSKERJABB"), ArrayList)
        If Mode = enumMode.Mode.NewItemMode Then
            _arrONGKOSKERJA = CType(sessHelper.GetSession("NEW_ONGKOSKERJABB"), ArrayList)
        End If
        Dim blnIsExistPosition99 As Boolean = False
        Dim blnIsExistPosXEE999 As Boolean = False
        Dim blnIsExistWork90 As Boolean = False
        For Each _objWSCDetailBB As WSCDetailBB In _arrONGKOSKERJA
            If _objWSCDetailBB.WorkCode = "99" Then
                'If _objWSCDetailBB.WorkCode = "99" OrElse _objWSCDetailBB.WorkCode = "90" Then
                blnIsExistPosition99 = True
                Exit For
            End If
            If _objWSCDetailBB.PositionCode = "XEE999" Then
                blnIsExistPosXEE999 = True
                Exit For
            End If
            If _objWSCDetailBB.WorkCode = "90" Then
                blnIsExistWork90 = True
                Exit For
            End If
        Next
        Dim _arrONGKOSPART As ArrayList = CType(sessHelper.GetSession("ONGKOSPARTSBB"), ArrayList)
        If Mode = enumMode.Mode.NewItemMode Then
            _arrONGKOSPART = CType(sessHelper.GetSession("NEW_ONGKOSPARTSBB"), ArrayList)
        End If
        Dim blnIsExistPartNPN7 As Boolean = False
        For Each _objWSCDetailBB As WSCDetailBB In _arrONGKOSPART
            If Not IsNothing(_objWSCDetailBB.SparePartMaster) Then
                If _objWSCDetailBB.SparePartMaster.PartNumber.Trim = "NPN7" Then
                    blnIsExistPartNPN7 = True
                    Exit For
                End If
            End If
        Next

        Dim _arrWSCEVIDENCE As ArrayList = CType(sessHelper.GetSession("WSCEVIDENCEBB"), ArrayList)
        If Mode = enumMode.Mode.NewItemMode Then
            _arrWSCEVIDENCE = CType(sessHelper.GetSession("NEW_WSCEVIDENCEBB"), ArrayList)
        End If
        Dim blnIsNotExistAttachFile As Boolean = False
        If _arrWSCEVIDENCE.Count <= 0 Then blnIsNotExistAttachFile = True
        For Each _objWSCEVIDENCE As WSCEvidenceBB In _arrWSCEVIDENCE
            If _objWSCEVIDENCE.EvidenceType = EnumWSCEvidenceType.WSCEvidenceType.KWITANSI_WSC Then 'Type Kwitansi
                If IsNothing(_objWSCEVIDENCE.AttachmentData) Then
                    If _objWSCEVIDENCE.PathFile.Trim = String.Empty Then
                        blnIsNotExistAttachFile = True
                        Exit For
                    End If
                End If
            End If
        Next
        If (blnIsExistPosition99 = True Or blnIsExistPartNPN7 = True) And blnIsNotExistAttachFile = True Then
            MessageBox.Show("Lampiran Tipe Kwitansi masih kosong")
            Return False
        End If

        If blnIsExistPosXEE999 AndAlso blnIsExistPosition99 AndAlso _arrONGKOSPART.Count > 0 Then
            MessageBox.Show("Kode Posisi XEE999 dengan Kode Kerja 99, tidak boleh ada part di table ongkos part")
            Return False
        End If

        'Validasi Main Part
        Dim intJumlahMainPart As Integer = 0
        For Each _objWSCDetailBB As WSCDetailBB In _arrONGKOSPART
            If _objWSCDetailBB.MainPart = 1 Then
                intJumlahMainPart += 1
            End If
        Next
        'If intJumlahMainPart = 0 Then
        '    If (ddlClaimType.SelectedValue = "Z2" OrElse ddlClaimType.SelectedValue = "Z6") AndAlso ddlRefDoc.SelectedValue = "1" Then   ' -- PQR Ref
        '        If blnIsExistPositionXEE999 = False Then
        '            MessageBox.Show("Silahkan pilih Main Part")
        '            Return False
        '        End If
        '    End If
        'End If
        If intJumlahMainPart > 1 Then
            MessageBox.Show("Silahkan pilih satu Main Part saja dari part yang sudah di input")
            Return False
        End If
        If _arrONGKOSPART.Count > 0 Then
            If intJumlahMainPart = 0 Then
                'If ddlRefDoc.SelectedIndex = 1 AndAlso intJumlahMainPart = 0 Then
                MessageBox.Show("Silahkan pilih Main Part")
                Return False
            End If
        End If

        '================================================================ Chassis No, Kode Posisi, Kode Kerja dan kilometer ga boleh sama
        Dim arlKodePOsisi As ArrayList = New ArrayList
        Dim returnKodeKerusakan As Boolean = True
        Dim reasonKodeKerusakan As String = ""
        For Each item As DataGridItem In dgOngkosKerja.Items
            If item.ItemType = ListItemType.Item OrElse item.ItemType = ListItemType.AlternatingItem Then
                Dim txtPosition As TextBox = CType(item.FindControl("txtPostionCodeItem"), TextBox)
                Dim txtWokCOde As TextBox = CType(item.FindControl("txtWorkCodeItem"), TextBox)
                Dim oWSCDetailBB As WSCDetailBB = New WSCDetailBB
                If Mode = enumMode.Mode.NewItemMode Then
                    If ChassisKodePosWorkCodKiloIsExist(txtNoChasis.Text, txtPosition.Text, txtWokCOde.Text, txtOdometer.Text, oWSCDetailBB) Then
                        MessageBox.Show("Data Claim sudah pernah diinputkan di dealer " & oWSCDetailBB.WSCHeaderBB.Dealer.DealerCode & "\n dengan claim number " & oWSCDetailBB.WSCHeaderBB.ClaimNumber)
                        Return False
                    End If
                End If
                arlKodePOsisi.Add(txtPosition.Text.Trim.Substring(0, 2))

                'If Not validateKodeKerusakan(txtPosition.Text.Trim.ToUpper, ddlKodeWSCA.SelectedValue, "A") Then
                '    returnKodeKerusakan = False
                '    If reasonKodeKerusakan.Length > 0 Then
                '        reasonKodeKerusakan += ";\nKode A " & ddlKodeWSCA.SelectedValue & " tidak valid untuk posisi " & txtPosition.Text.Trim.ToUpper
                '    Else
                '        reasonKodeKerusakan += "Kode A " & ddlKodeWSCA.SelectedValue & " tidak valid untuk posisi " & txtPosition.Text.Trim.ToUpper
                '    End If
                'End If
                'If Not validateKodeKerusakan(txtPosition.Text.Trim.ToUpper, ddlKodeWSCB.SelectedValue, "B") Then
                '    returnKodeKerusakan = False
                '    If reasonKodeKerusakan.Length > 0 Then
                '        reasonKodeKerusakan += ";\nKode B " & ddlKodeWSCB.SelectedValue & " tidak valid untuk posisi " & txtPosition.Text.Trim.ToUpper
                '    Else
                '        reasonKodeKerusakan += "Kode B " & ddlKodeWSCB.SelectedValue & " tidak valid untuk posisi " & txtPosition.Text.Trim.ToUpper
                '    End If
                'End If
            End If
        Next
        If returnKodeKerusakan = False Then
            MessageBox.Show(reasonKodeKerusakan)
            Return False
        End If
        If Not ValidateByParameter() Then
            Return False
        End If
        Return True

    End Function

    Private Sub LoadChassisInfo(ByRef obj As ChassisMasterBB)
        txtNoChasis.Text = obj.ChassisNumber
        hdntxtNoChasis.Value = txtNoChasis.Text
        lblNoChasisVal.Text = obj.ChassisNumber
        lblNoMesinVal.Text = obj.EngineNumber

        sessHelper.SetSession("objChasMas", obj)
        If obj.AlreadySaled = 0 Then
            lblAlreadySaled.Text = "Belum Terjual"
        ElseIf obj.AlreadySaled = 2 Then
            lblAlreadySaled.Text = "Sudah Terjual"
        End If

        If IsNothing(obj.EndCustomer) Then
            lblNamaPemilikVal.Text = ""
            'lblTglPKTVal.Text = ""
        Else
            'If obj.EndCustomer.OpenFakturDate.Year < 1970 Then
            '    lblNamaPemilikVal.Text = ""
            '    lblTglPKTVal.Text = ""
            'Else
            If Not obj.EndCustomer.Customer Is Nothing Then
                If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                    lblNamaPemilikVal.Text = obj.EndCustomer.Customer.Name1 & " - " & obj.EndCustomer.Customer.Alamat
                Else
                    'If obj.Dealer.ID = oDealer.ID Then
                    lblNamaPemilikVal.Text = obj.EndCustomer.Customer.Name1 & " - " & obj.EndCustomer.Customer.Alamat
                    'Else
                    '    lblNamaPemilikVal.Text = ""
                    'End If
                End If
            Else
                lblNamaPemilikVal.Text = ""
            End If
            'lblTglPKTVal.Text = ""
            'Dim arrChassisMasterBBPKT As ArrayList = New ArrayList
            'Dim criterias As New CriteriaComposite(New Criteria(GetType(ChassisMasterBBPKT), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criterias.opAnd(New Criteria(GetType(ChassisMasterBBPKT), "ChassisMasterBBs.ID", MatchType.Exact, obj.ID))
            'arrChassisMasterBBPKT = New ChassisMasterBBPKTFacade(User).Retrieve(criterias)
            'If Not IsNothing(arrChassisMasterBBPKT) And arrChassisMasterBBPKT.Count > 0 Then
            '    oChassisMasterBBPKT = CType(arrChassisMasterBBPKT(0), ChassisMasterBBPKT)
            '    lblTglPKTVal.Text = oChassisMasterBBPKT.PKTDate.ToString("dd/MM/yyyy")
            'Else
            '    If lblTglPKTVal.Text = "" Then
            '        lblTglPKTVal.Text = obj.EndCustomer.FakturDate.ToString("dd/MM/yyyy")
            '    End If
            '    If lblTglPKTVal.Text = "" Then
            '        lblTglPKTVal.Text = obj.EndCustomer.OpenFakturDate.ToString("dd/MM/yyyy")
            '    End If
            'End If
        End If
        'End If
        lnkbtnPopUpInfoKendaraan.Attributes("onclick") = "ShowPPInfoKendaraanSelection()"
        lnkbtnPopUpInfoKendaraan.Visible = True
        lnkbtnCheckChassis.Attributes("style") = "display:table-row"
    End Sub

    Private Sub ClearChassisInfo()
        txtNoChasis.Text = ""
        hdntxtNoChasis.Value = ""
        hdnPPtxtNoChasis.Value = ""
        lblNoMesinVal.Text = ""
        lblAlreadySaled.Text = ""
        lblNamaPemilikVal.Text = ""
        'lblTglPKTVal.Text = "" ' Temporaly not available
    End Sub

    Private Sub ReloadForm(ByVal id As Integer)
        sessHelper.SetSession("WSCHEADERBB", oWSCHeaderBBFacade.Retrieve(id))
        oWSCHeaderBB = CType(sessHelper.GetSession("WSCHEADERBB"), WSCHeaderBB)

        Dim arrWSCDetailBBLabor As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(WSCDetailBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(WSCDetailBB), "WSCHeaderBB.ID", MatchType.Exact, oWSCHeaderBB.ID))
        criterias.opAnd(New Criteria(GetType(WSCDetailBB), "WSCType", MatchType.Exact, "L"))
        arrWSCDetailBBLabor = New WSCDetailBBFacade(User).Retrieve(criterias)
        If IsNothing(arrWSCDetailBBLabor) And arrWSCDetailBBLabor.Count = 0 Then
            arrWSCDetailBBLabor = New ArrayList
        End If

        Dim arrWSCDetailBBPart As ArrayList = New ArrayList
        Dim criterias2 As New CriteriaComposite(New Criteria(GetType(WSCDetailBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias2.opAnd(New Criteria(GetType(WSCDetailBB), "WSCHeaderBB.ID", MatchType.Exact, oWSCHeaderBB.ID))
        criterias2.opAnd(New Criteria(GetType(WSCDetailBB), "WSCType", MatchType.Exact, "P"))
        arrWSCDetailBBPart = New WSCDetailBBFacade(User).Retrieve(criterias2)
        If IsNothing(arrWSCDetailBBPart) And arrWSCDetailBBPart.Count = 0 Then
            arrWSCDetailBBPart = New ArrayList
        End If

        sessHelper.SetSession("ONGKOSKERJABB", arrWSCDetailBBLabor)
        sessHelper.SetSession("DELETEDONGKOSKERJABB", New ArrayList)

        sessHelper.SetSession("ONGKOSPARTSBB", arrWSCDetailBBPart)
        sessHelper.SetSession("DELETEDPARTSBB", New ArrayList)

        sessHelper.SetSession("WSCEVIDENCEBB", GetAttachmentList(oWSCHeaderBB.wSCEvidenceBBs))
        sessHelper.SetSession("DELETEDWSEVIDENCEBB", New ArrayList)


        If CType(ViewState("Mode"), enumMode.Mode) = enumMode.Mode.EditMode Then
            Server.Transfer("~/PQR/FrmWSCHeaderBB.aspx?Mode=Edit")
        ElseIf CType(ViewState("Mode"), enumMode.Mode) = enumMode.Mode.ViewMode Then
            Server.Transfer("~/PQR/FrmWSCHeaderBB.aspx?Mode=View")
        End If
        'fillForm()

    End Sub

    Private Sub RemoveWSCAttachment(ByVal AttachmentCollection As ArrayList, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim finfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                For Each obj As WSCEvidenceBB In AttachmentCollection
                    finfo = New FileInfo(TargetPath + obj.PathFile)
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

    Private Sub RemoveWSCAttachment(ByVal ObjAttachment As WSCEvidenceBB, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim finfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                finfo = New FileInfo(TargetPath + ObjAttachment.PathFile)
                If finfo.Exists Then
                    finfo.Delete()
                End If
            End If
            imp.StopImpersonate()
            imp = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UploadAttachment(ByVal AttachmentCollection As ArrayList, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim finfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                For Each obj As WSCEvidenceBB In AttachmentCollection
                    If Not IsNothing(obj.AttachmentData) Then
                        finfo = New FileInfo(TargetPath + obj.PathFile)

                        If Not finfo.Directory.Exists Then
                            Directory.CreateDirectory(finfo.DirectoryName)
                        End If
                        obj.AttachmentData.SaveAs(TargetPath + obj.PathFile)
                    End If
                Next
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try


    End Sub

    Private Sub UploadAttachment(ByVal ObjAttachment As WSCEvidenceBB, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim finfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                If Not IsNothing(ObjAttachment.PathFile) Then
                    finfo = New FileInfo(TargetPath + ObjAttachment.PathFile)

                    If Not finfo.Directory.Exists Then
                        Directory.CreateDirectory(finfo.DirectoryName)
                    End If
                    ObjAttachment.AttachmentData.SaveAs(TargetPath + ObjAttachment.PathFile)
                End If
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BindWSCEvidenceBBType(ByVal ddlEvidenceType As DropDownList)
        Dim _enumWSCEvidenceType As New EnumWSCEvidenceType
        Dim _arrTmp As New ArrayList
        _arrTmp = _enumWSCEvidenceType.WSCEvidenceTypeList

        ddlEvidenceType.DataSource = _arrTmp
        ddlEvidenceType.DataTextField = "WSCEvidenceTypeId"
        ddlEvidenceType.DataValueField = "WSCEvidenceTypeValue"
        ddlEvidenceType.DataBind()
        ddlEvidenceType.Items.Insert(0, "Silahkan Pilih")
    End Sub

    Private Sub GetObjectLaborMaster(strKodePosisi As String, strWorkCode As String)
        objKodePosisi = New DeskripsiPositionCodeFacade(User).Retrieve(strKodePosisi)
        objKodeKerja = New DeskripsiWorkPositionFacade(User).Retrieve(strWorkCode)

        Dim criterias As New CriteriaComposite(New Criteria(GetType(LaborMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(LaborMaster), "LaborCode", MatchType.Exact, objKodePosisi.KodePosition.Trim))
        criterias.opAnd(New Criteria(GetType(LaborMaster), "WorkCode", MatchType.Exact, objKodeKerja.KodeKerja.Trim))
        arrLaborMaster = New LaborMasterFacade(User).Retrieve(criterias)
        If Not IsNothing(arrLaborMaster) And arrLaborMaster.Count > 0 Then
            objLaborMaster = CType(arrLaborMaster(0), LaborMaster)
        Else
            objLaborMaster = Nothing
        End If
    End Sub

    Private Function GetLaborMaster(strKodePosisi As String, strWorkCode As String) As LaborMaster
        Dim _return As Integer
        objKodePosisi = New DeskripsiPositionCodeFacade(User).Retrieve(strKodePosisi)
        objKodeKerja = New DeskripsiWorkPositionFacade(User).Retrieve(strWorkCode)

        Dim criterias As New CriteriaComposite(New Criteria(GetType(LaborMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(LaborMaster), "LaborCode", MatchType.Exact, objKodePosisi.KodePosition.Trim))
        criterias.opAnd(New Criteria(GetType(LaborMaster), "WorkCode", MatchType.Exact, objKodeKerja.KodeKerja.Trim))
        arrLaborMaster = New LaborMasterFacade(User).Retrieve(criterias)
        If Not IsNothing(arrLaborMaster) And arrLaborMaster.Count > 0 Then
            objLaborMaster = CType(arrLaborMaster(0), LaborMaster)
        Else
            objLaborMaster = Nothing
        End If
    End Function

    Private Function LoopDgrid(ByVal SourceGridName As String, ByVal oLaborMaster As LaborMaster, ByVal objSparePartMaster As SparePartMaster, ByVal fromFunc As String, ByVal index As Integer)
        Dim _arrONGKOSKERJA As ArrayList = New ArrayList
        Dim _arrONGKOSKERJATemp As ArrayList = New ArrayList
        Dim _arrONGKOSPARTS As ArrayList = New ArrayList
        Dim _arrONGKOSPARTSTemp As ArrayList = New ArrayList
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        If Mode = enumMode.Mode.NewItemMode Then
            If SourceGridName.ToUpper() = "DGONGKOSKERJA" OrElse SourceGridName.ToUpper() = "ALLGRID" Then
                _arrONGKOSKERJATemp = CType(sessHelper.GetSession("NEW_ONGKOSKERJABB"), ArrayList)
                sessHelper.RemoveSession("NEW_ONGKOSKERJABB")
                sessHelper.SetSession("NEW_ONGKOSKERJABB", New ArrayList)
                _arrONGKOSKERJA = CType(sessHelper.GetSession("NEW_ONGKOSKERJABB"), ArrayList)
            End If

            If SourceGridName.ToUpper() = "DGPARTS" OrElse SourceGridName.ToUpper() = "ALLGRID" Then
                _arrONGKOSPARTSTemp = CType(sessHelper.GetSession("NEW_ONGKOSPARTSBB"), ArrayList)
                sessHelper.RemoveSession("NEW_ONGKOSPARTSBB")
                sessHelper.SetSession("NEW_ONGKOSPARTSBB", New ArrayList)
                _arrONGKOSPARTS = CType(sessHelper.GetSession("NEW_ONGKOSPARTSBB"), ArrayList)
            End If
        Else
            If SourceGridName.ToUpper() = "DGONGKOSKERJA" OrElse SourceGridName.ToUpper() = "ALLGRID" Then
                _arrONGKOSKERJATemp = CType(sessHelper.GetSession("ONGKOSKERJABB"), ArrayList)
                sessHelper.RemoveSession("ONGKOSKERJABB")
                sessHelper.SetSession("ONGKOSKERJABB", New ArrayList)
                _arrONGKOSKERJA = CType(sessHelper.GetSession("ONGKOSKERJABB"), ArrayList)
            End If

            If SourceGridName.ToUpper() = "DGPARTS" OrElse SourceGridName.ToUpper() = "ALLGRID" Then
                _arrONGKOSPARTSTemp = CType(sessHelper.GetSession("ONGKOSPARTSBB"), ArrayList)
                sessHelper.RemoveSession("ONGKOSPARTSBB")
                sessHelper.SetSession("ONGKOSPARTSBB", New ArrayList)
                _arrONGKOSPARTS = CType(sessHelper.GetSession("ONGKOSPARTSBB"), ArrayList)
            End If
        End If

        Dim i = 0
        Dim Existss As Boolean = False



        Dim blnExistPosCodeXEE999 As Boolean = False
        'DG Ongkos Kerja
        If SourceGridName.ToUpper() = "DGONGKOSKERJA" OrElse SourceGridName.ToUpper() = "ALLGRID" Then
            Dim e1 As Control
            For Each e1 In dgOngkosKerja.Controls
                For Each ct As Control In e1.Controls
                    If TypeOf ct Is System.Web.UI.WebControls.DataGridItem Then
                        Dim di As System.Web.UI.WebControls.DataGridItem = CType(ct, System.Web.UI.WebControls.DataGridItem)
                        If di.ItemType = ListItemType.Item OrElse di.ItemType = ListItemType.AlternatingItem OrElse di.ItemType = ListItemType.Footer Then

                            '    If fromFunc = "Z4" Then
                            '        Dim txtAmount As TextBox
                            '        If di.ItemType = ListItemType.Footer Then
                            '            txtAmount = CType(di.FindControl("txtPartPriceFooter"), TextBox)
                            '        ElseIf di.ItemType = ListItemType.Item OrElse di.ItemType = ListItemType.AlternatingItem Then
                            '            txtAmount = CType(di.FindControl("txtAmountItem"), TextBox)
                            '        End If
                            '
                            '        txtAmount.Enabled = False
                            '    End If
                            'End If

                            If fromFunc = "Z4" Then
                                Dim txtAmount As TextBox
                                If di.ItemType = ListItemType.Footer Then
                                    txtAmount = CType(di.FindControl("txtPartPriceFooter"), TextBox)
                                ElseIf di.ItemType = ListItemType.Item OrElse di.ItemType = ListItemType.AlternatingItem Then
                                    txtAmount = CType(di.FindControl("txtAmountItem"), TextBox)
                                End If

                                txtAmount.Enabled = False
                            End If

                            'If fromFunc = "Add" OrElse fromFunc = "Simpan" Then
                            If fromFunc = "Add" OrElse fromFunc = "Edit" Then


                                'If di.ItemType = ListItemType.Item OrElse di.ItemType = ListItemType.AlternatingItem OrElse di.ItemType = ListItemType.Footer Then
                                Dim txtPositionCode As TextBox
                                Dim txtWorkCode As TextBox
                                Dim txtQuantity As TextBox
                                Dim txtAmount As TextBox
                                Dim lblID As Label

                                If (di.ItemType = ListItemType.Footer) Then
                                    txtPositionCode = CType(di.FindControl("txtPositionCodeFooter"), TextBox)
                                    txtWorkCode = CType(di.FindControl("txtWorkCodeFooter"), TextBox)
                                    txtQuantity = CType(di.FindControl("txtQuantityFooter"), TextBox)
                                    txtAmount = CType(di.FindControl("txtPartPriceFooter"), TextBox)
                                    lblID = CType(di.FindControl("lblIDFooter"), Label)
                                ElseIf di.ItemType = ListItemType.Item OrElse di.ItemType = ListItemType.AlternatingItem Then
                                    txtPositionCode = CType(di.FindControl("txtPostionCodeItem"), TextBox)
                                    txtWorkCode = CType(di.FindControl("txtWorkCodeItem"), TextBox)
                                    txtQuantity = CType(di.FindControl("txtQuantityItem"), TextBox)
                                    txtAmount = CType(di.FindControl("txtAmountItem"), TextBox)
                                    lblID = CType(di.FindControl("lblIDItem"), Label)
                                End If
                                If txtPositionCode.Text.Trim <> "" And txtWorkCode.Text.Trim <> "" Then
                                    If txtQuantity.Text.Trim = "" OrElse CType(txtQuantity.Text.Trim, Integer) = 0 Then
                                        MessageBox.Show("Jumlah ongkos kerja tidak boleh kosong atau nol")
                                        Return False
                                    End If
                                    If txtAmount.Text.Trim = "" OrElse CType(txtAmount.Text.Trim, Integer) = 0 Then
                                        txtAmount.Text = 0
                                    End If
                                    If txtPositionCode.Text.Trim.ToUpper = "XEE999" AndAlso txtWorkCode.Text.Trim.ToUpper = "99" Then
                                        blnExistPosCodeXEE999 = True
                                    End If
                                    'If txtPositionCode.Text.Trim.ToUpper = "XEE999" AndAlso txtWorkCode.Text.ToUpper = "99" Then
                                    If txtWorkCode.Text.ToUpper = "99" Then
                                        If txtAmount.Text.Trim = "" OrElse CType(txtAmount.Text.Trim, Integer) = 0 Then
                                            'txtAmount.Visible = True
                                            'dgOngkosKerja.ShowFooter = False
                                            MessageBox.Show("Silahkan input amount Ongkos Kerja !")
                                            Return False
                                        End If
                                        ''ElseIf txtWorkCode.Text.ToUpper = "90" AndAlso ddlClaimType.SelectedValue = "Z4" Then
                                    ElseIf txtWorkCode.Text.ToUpper = "90" Then
                                        If txtAmount.Text.Trim = "" OrElse CType(txtAmount.Text.Trim, Integer) = 0 Then
                                            'dgOngkosKerja.ShowFooter = False
                                            MessageBox.Show("Silahkan input amount Ongkos Kerja !")
                                            Return False
                                        End If
                                    Else
                                        'txtAmount.Visible = False
                                        txtAmount.Text = 0
                                        dgOngkosKerja.ShowFooter = True
                                    End If
                                    If (ddlClaimType.SelectedValue = "Z2" OrElse ddlClaimType.SelectedValue = "Z6") Then
                                        If ddlRefDoc.SelectedValue = "1" Then   ' -- PQR Ref
                                            If txtPositionCode.Text.Trim <> "" Then
                                                If (Not IsNumeric(Left(txtPositionCode.Text.Trim, 1))) AndAlso txtPositionCode.Text.Trim.ToUpper <> "XEE999" Then
                                                    MessageBox.Show("Silahkan input Kode Posisi diawali dengan angka !")
                                                    Return False
                                                End If
                                            End If
                                        End If
                                    End If

                                    If (ddlClaimType.SelectedValue = "Z4" OrElse ddlClaimType.SelectedValue = "Z6") Then
                                        If ddlRefDoc.SelectedValue = "0" Then   ' -- Buletin Ref
                                            If txtPositionCode.Text.Trim <> "" Then
                                                If (Not Char.IsLetter(Left(txtPositionCode.Text.Trim, 1).Chars(0))) Then
                                                    If txtPositionCode.Text.Trim.ToUpper = "XEE999" Then
                                                        MessageBox.Show("Tipe Claim Z4 atau Z6 untuk Buletin Service Reference \ntidak boleh memakai kode posisi XEE999")
                                                        Return False
                                                    Else
                                                        MessageBox.Show("Silahkan input Kode Posisi diawali dengan huruf !")
                                                        Return False
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                    If i > 0 And blnExistPosCodeXEE999 = True Then
                                        MessageBox.Show("Kode Posisi XEE999 tidak boleh digabung \ndengan kode posisi yang lain")
                                        Return False
                                    End If
                                    'Try
                                    '    GetLaborMaster(txtPositionCode.Text, txtWorkCode.Text)
                                    '    If IsNothing(oLaborMaster) AndAlso Not IsNothing(objLaborMaster) Then
                                    '        oLaborMaster = objLaborMaster
                                    '    End If
                                    '    If IsNothing(objLaborMaster) AndAlso Not IsNothing(oLaborMaster) Then
                                    '        objLaborMaster = oLaborMaster
                                    '    End If
                                    'Catch ex As Exception
                                    'End Try
                                    'GetObjectLaborMaster(txtPositionCode.Text, txtWorkCode.Text)
                                    insertSessionOngKerja(lblID.Text, txtPositionCode.Text, txtWorkCode.Text, oLaborMaster, txtQuantity.Text, txtAmount.Text, _arrONGKOSKERJA)
                                    i += 1
                                End If

                            Else
                                If di.ItemType = ListItemType.Item OrElse di.ItemType = ListItemType.AlternatingItem Then
                                    Dim txtPostionCodeItem As TextBox = CType(di.FindControl("txtPostionCodeItem"), TextBox)
                                    Dim txtWorkCodeItem As TextBox = CType(di.FindControl("txtWorkCodeItem"), TextBox)
                                    Dim txtQuantityItem As TextBox = CType(di.FindControl("txtQuantityItem"), TextBox)
                                    Dim txtAmountItem As TextBox = CType(di.FindControl("txtAmountItem"), TextBox)
                                    Dim lblID As Label = CType(di.FindControl("lblIDItem"), Label)
                                    'GetObjectLaborMaster(txtPostionCodeItem.Text, txtWorkCodeItem.Text)
                                    insertSessionOngKerja(lblID.Text, txtPostionCodeItem.Text, txtWorkCodeItem.Text, oLaborMaster, txtQuantityItem.Text, txtAmountItem.Text, _arrONGKOSKERJA)
                                End If
                            End If
                        End If

                        If fromFunc = "Simpan" Then
                            Dim txtPostionCode As TextBox
                            Dim txtWorkCode As TextBox
                            Dim txtQuantity As TextBox
                            Dim txtAmount As TextBox
                            If di.ItemType = ListItemType.Footer Then
                                txtPostionCode = CType(di.FindControl("txtPositionCodeFooter"), TextBox)
                                txtWorkCode = CType(di.FindControl("txtWorkCodeFooter"), TextBox)
                                txtQuantity = CType(di.FindControl("txtQuantityFooter"), TextBox)
                                txtAmount = CType(di.FindControl("txtPartPriceFooter"), TextBox)
                            ElseIf di.ItemType = ListItemType.Item OrElse di.ItemType = ListItemType.AlternatingItem Then
                                txtPostionCode = CType(di.FindControl("txtPostionCodeItem"), TextBox)
                                txtWorkCode = CType(di.FindControl("txtWorkCodeItem"), TextBox)
                                txtQuantity = CType(di.FindControl("txtQuantityItem"), TextBox)
                                txtAmount = CType(di.FindControl("txtAmountItem"), TextBox)
                                If txtPostionCode.Text.Trim <> "" AndAlso txtWorkCode.Text.Trim <> "" Then

                                    If CType(txtQuantity.Text.Trim, Integer) = 0 OrElse txtQuantity.Text.Trim = "" Then
                                        MessageBox.Show("Silahkan input Jumlah Ongkos Kerja dahulu")
                                        Return False
                                    End If

                                    'If txtPostionCode.Text.Trim.ToUpper = "XEE999" AndAlso txtWorkCode.Text.Trim.ToUpper = "99" _
                                    If txtWorkCode.Text.Trim.ToUpper = "99" _
                                            AndAlso (CType(txtAmount.Text.Trim, Integer) = 1 OrElse txtAmount.Text.Trim = "") Then
                                        MessageBox.Show("Silahkan input Harga Ongkos Kerja dahulu")
                                        Return False
                                    End If
                                End If
                            End If

                            Dim arrLi As ArrayList = CType(sessHelper.GetSession("NEW_ONGKOSKERJABB"), ArrayList)
                            If Mode = enumMode.Mode.EditMode Then
                                arrLi = CType(sessHelper.GetSession("ONGKOSKERJABB"), ArrayList)
                            End If
                            If Not IsNothing(txtPostionCode) AndAlso Not IsNothing(txtWorkCode) _
                                AndAlso txtPostionCode.Text.Trim <> "" AndAlso txtWorkCode.Text.Trim <> "" Then
                                i = 1

                                For Each ArrWSC As WSCDetailBB In arrLi
                                    If i <> index Then
                                        If (ArrWSC.PositionCode + ArrWSC.WorkCode).ToString = (txtPostionCode.Text.Trim + txtWorkCode.Text).ToString Then
                                            MessageBox.Show("Ada kode posisi dan kode kerja yang sama")
                                            Return False
                                        End If
                                        If (ArrWSC.PositionCode + ArrWSC.WorkCode).ToString.ToUpper = "XEE99999" Then
                                            MessageBox.Show("Kode Posisi XEE999 tidak boleh digabung \ndengan kode posisi yang lain")
                                            Return False
                                        End If
                                    End If
                                    i += 1
                                Next
                            End If
                            index += 1
                        End If
                    End If
                Next
            Next


            If fromFunc = "Delete" Then
                If Mode = enumMode.Mode.NewItemMode Then
                    _arrONGKOSKERJA.RemoveAt(index)
                ElseIf Mode = enumMode.Mode.EditMode Then
                    Dim oOngkosKerja As WSCDetailBB = CType(_arrONGKOSKERJA(index), WSCDetailBB)
                    If oOngkosKerja.ID > 0 Then
                        Dim deletedArrLst As ArrayList
                        deletedArrLst = CType(sessHelper.GetSession("DELETEDONGKOSKERJABB"), ArrayList)
                        deletedArrLst.Add(oOngkosKerja)
                        sessHelper.SetSession("DELETEDONGKOSKERJABB", deletedArrLst)
                    End If
                    _arrONGKOSKERJA.RemoveAt(index)
                End If
                dgOngkosKerja.ShowFooter = True
            End If
        End If

        '-------------------------------------------------------------------
        '--- untuk Datagrid Parts
        If SourceGridName.ToUpper() = "DGPARTS" OrElse SourceGridName.ToUpper() = "ALLGRID" Then
            Dim e2 As Control
            For Each e2 In dgParts.Controls
                For Each ct As Control In e2.Controls
                    If TypeOf ct Is System.Web.UI.WebControls.DataGridItem Then
                        Dim di As System.Web.UI.WebControls.DataGridItem = CType(ct, System.Web.UI.WebControls.DataGridItem)

                        Dim KodePart As String
                        Dim intMainPart As Integer = -1
                        Dim lblKodeParts As Label
                        Dim txtKodePartsFooter As TextBox
                        Dim txtQty As TextBox
                        Dim txtPartPrice As TextBox
                        Dim cbMainPart As CheckBox
                        Dim lblID As Label
                        'If fromFunc = "Add" OrElse fromFunc = "Simpan" Then
                        If fromFunc = "Add" OrElse fromFunc = "Edit" Then
                            If di.ItemType = ListItemType.Item OrElse di.ItemType = ListItemType.AlternatingItem OrElse di.ItemType = ListItemType.Footer Then
                                If (di.ItemType = ListItemType.Footer) Then
                                    txtKodePartsFooter = CType(di.FindControl("txtKodePartsFooter"), TextBox)
                                    KodePart = txtKodePartsFooter.Text
                                    txtQty = CType(di.FindControl("txtQtyFooter"), TextBox)
                                    txtPartPrice = CType(di.FindControl("txtPartPriceFooter"), TextBox)
                                    lblID = CType(di.FindControl("lblIDFooter"), Label)
                                    cbMainPart = CType(di.FindControl("cbMainPartFooter"), CheckBox)
                                ElseIf di.ItemType = ListItemType.Item OrElse di.ItemType = ListItemType.AlternatingItem Then
                                    lblKodeParts = CType(di.FindControl("lblKodeParts"), Label)
                                    KodePart = lblKodeParts.Text
                                    txtQty = CType(di.FindControl("txtQtyItem"), TextBox)
                                    txtPartPrice = CType(di.FindControl("txtPartPriceItem"), TextBox)
                                    lblID = CType(di.FindControl("lblIDItem"), Label)
                                    cbMainPart = CType(di.FindControl("cbMainPartItem"), CheckBox)
                                End If

                                If KodePart.Trim <> "" Then
                                    If Not IsNothing(_arrONGKOSPARTS) AndAlso _arrONGKOSPARTS.Count > 0 Then
                                        If Mode = enumMode.Mode.NewItemMode Then
                                            _arrONGKOSPARTSTemp = CType(sessHelper.GetSession("NEW_ONGKOSPARTSBB"), ArrayList)
                                        Else
                                            _arrONGKOSPARTSTemp = CType(sessHelper.GetSession("ONGKOSPARTSBB"), ArrayList)
                                        End If
                                    End If

                                    objSparePartMaster = New SparePartMasterFacade(User).Retrieve(KodePart.Trim())
                                    If Not IsNothing(objSparePartMaster) OrElse objSparePartMaster.ID <> 0 Then
                                        If PartsCodeIsExist(objSparePartMaster.ID, _arrONGKOSPARTS) Then
                                            If Mode = enumMode.Mode.NewItemMode Then
                                                sessHelper.SetSession("NEW_ONGKOSPARTSBB", _arrONGKOSPARTSTemp)
                                            Else
                                                sessHelper.SetSession("ONGKOSPARTSBB", _arrONGKOSPARTSTemp)
                                            End If
                                            MessageBox.Show(SR.DataIsExist("Kode Part"))
                                            Return False
                                        End If
                                    Else
                                        If Mode = enumMode.Mode.NewItemMode Then
                                            sessHelper.SetSession("NEW_ONGKOSPARTSBB", _arrONGKOSPARTSTemp)
                                        Else
                                            sessHelper.SetSession("ONGKOSPARTSBB", _arrONGKOSPARTSTemp)
                                        End If
                                        MessageBox.Show(SR.DataNotFound("Kode Part"))
                                        Return False
                                    End If

                                    'If txtQty.Text.Trim = "" Then
                                    '    txtQty.Text = 0
                                    'End If
                                    If txtQty.Text.Trim = "" OrElse CType(txtQty.Text.Trim, Integer) = 0 Then
                                        MessageBox.Show("Jumlah ongkos part tidak boleh kosong atau nol")
                                        Return False
                                    End If
                                    If txtPartPrice.Text.Trim = "" Then
                                        txtPartPrice.Text = 0
                                    End If

                                    If ddlClaimType.SelectedValue = "Z4" AndAlso KodePart.Trim = "NPN7" AndAlso (txtPartPrice.Text.Trim = "" OrElse CType(txtPartPrice.Text.Trim, Integer) = 0) Then
                                        If Mode = enumMode.Mode.NewItemMode Then
                                            sessHelper.SetSession("NEW_ONGKOSPARTSBB", _arrONGKOSPARTSTemp)
                                        Else
                                            sessHelper.SetSession("ONGKOSPARTSBB", _arrONGKOSPARTSTemp)
                                        End If
                                        MessageBox.Show("Silahkan input Harga Item Part")
                                        Return False
                                    End If

                                    If (txtQty.Text.Trim = "" OrElse CType(txtQty.Text.Trim, Integer) = 0) AndAlso (KodePart.ToString.Trim <> "" OrElse CType(txtQty.Text.Trim, Integer) = 0) Then
                                        If Mode = enumMode.Mode.NewItemMode Then
                                            sessHelper.SetSession("NEW_ONGKOSPARTSBB", _arrONGKOSPARTSTemp)
                                        Else
                                            sessHelper.SetSession("ONGKOSPARTSBB", _arrONGKOSPARTSTemp)
                                        End If

                                        MessageBox.Show("Silahkan input Jumlah Item Part")
                                        Return False
                                    End If

                                    If cbMainPart.Checked = True Then
                                        intMainPart = 1
                                    Else
                                        intMainPart = 0
                                    End If

                                    _arrONGKOSPARTS = insertSessionOngParts(lblID.Text, objSparePartMaster, txtQty.Text, txtPartPrice.Text, intMainPart, _arrONGKOSPARTS)
                                End If
                            End If
                        Else
                            If di.ItemType = ListItemType.Item OrElse di.ItemType = ListItemType.AlternatingItem Then
                                lblKodeParts = CType(di.FindControl("lblKodeParts"), Label)
                                txtQty = CType(di.FindControl("txtQtyItem"), TextBox)
                                txtPartPrice = CType(di.FindControl("txtPartPriceItem"), TextBox)
                                lblID = CType(di.FindControl("lblIDItem"), Label)
                                cbMainPart = CType(di.FindControl("cbMainPartItem"), CheckBox)

                                _arrONGKOSPARTS = insertSessionOngParts(lblID.Text, objSparePartMaster, txtQty.Text, txtPartPrice.Text, intMainPart, _arrONGKOSPARTS)
                            End If
                        End If

                        If fromFunc = "Z4" Then
                            Dim txtAmount As TextBox
                            If di.ItemType = ListItemType.Footer Then
                                txtAmount = CType(di.FindControl("txtPartPriceFooter"), TextBox)
                            ElseIf di.ItemType = ListItemType.Item OrElse di.ItemType = ListItemType.AlternatingItem Then
                                txtAmount = CType(di.FindControl("txtPartPriceItem"), TextBox)
                            End If

                            txtAmount.Enabled = False
                        End If
                    End If
                Next
            Next
            If fromFunc = "Delete" Then
                If Mode = enumMode.Mode.NewItemMode Then
                    _arrONGKOSPARTS.RemoveAt(index)
                ElseIf Mode = enumMode.Mode.EditMode Then
                    Dim oOngkosParts As WSCDetailBB = CType(_arrONGKOSPARTS(index), WSCDetailBB)
                    If oOngkosParts.ID > 0 Then
                        Dim deletedArrLst As ArrayList
                        deletedArrLst = CType(sessHelper.GetSession("DELETEDPARTSBB"), ArrayList)
                        deletedArrLst.Add(oOngkosParts)
                        sessHelper.SetSession("DELETEDPARTSBB", deletedArrLst)
                    End If
                    _arrONGKOSPARTS.RemoveAt(index)
                End If
                dgParts.ShowFooter = True
            End If
        End If

        Return True
    End Function

    Private Function insertSessionOngKerja(ByVal intID As String, ByVal positionCode As String, ByVal workCode As String, ByVal laborMaster As LaborMaster, _
                                    ByVal quantity As String, ByVal amount As String, ByVal _arrONGKOSKERJA As ArrayList) As ArrayList

        intID = IIf(intID.Trim = "", 0, intID)
        quantity = IIf(quantity.Trim = "", 0, quantity)
        amount = IIf(amount.Trim = "", 0, amount)

        Dim objWSCDetailBB As WSCDetailBB = New WSCDetailBB
        objWSCDetailBB.ID = intID
        objWSCDetailBB.WSCType = "L"
        objWSCDetailBB.PositionCode = positionCode
        objWSCDetailBB.WorkCode = workCode
        objWSCDetailBB.LaborMaster = laborMaster
        objWSCDetailBB.SparePartMaster = Nothing
        objWSCDetailBB.Quantity = quantity
        objWSCDetailBB.PartPrice = amount
        If Not IsNothing(objLaborMaster) Then
            objWSCDetailBB.MainPart = 1
        Else
            objWSCDetailBB.MainPart = 0
        End If

        _arrONGKOSKERJA.Add(objWSCDetailBB)

        Mode = CType(ViewState("Mode"), enumMode.Mode)
        If Mode = enumMode.Mode.NewItemMode Then
            sessHelper.SetSession("NEW_ONGKOSKERJABB", _arrONGKOSKERJA)
        Else
            sessHelper.SetSession("ONGKOSKERJABB", _arrONGKOSKERJA)
        End If

        Return _arrONGKOSKERJA
    End Function


    Private Function insertSessionOngParts(ByVal intID As String, ByVal objSparePartMaster As SparePartMaster, _
                                    ByVal quantity As String, ByVal amount As String, ByVal chkMainPart As Integer, ByVal _arrONGKOSPARTS As ArrayList) As ArrayList

        intID = IIf(intID.Trim = "", 0, intID)

        Dim objWSCDetailBB As WSCDetailBB = New WSCDetailBB
        objWSCDetailBB.ID = intID
        objWSCDetailBB.WSCType = "P"
        objWSCDetailBB.LaborMaster = Nothing
        objWSCDetailBB.SparePartMaster = objSparePartMaster
        objWSCDetailBB.Quantity = quantity
        objWSCDetailBB.PartPrice = amount
        objWSCDetailBB.MainPart = chkMainPart

        _arrONGKOSPARTS.Add(objWSCDetailBB)

        Mode = CType(ViewState("Mode"), enumMode.Mode)
        If Mode = enumMode.Mode.NewItemMode Then
            sessHelper.SetSession("NEW_ONGKOSPARTSBB", _arrONGKOSPARTS)
        Else
            sessHelper.SetSession("ONGKOSPARTSBB", _arrONGKOSPARTS)
        End If

        Return _arrONGKOSPARTS
    End Function

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.ClientScript.GetPostBackEventReference(Me, String.Empty)
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        oDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        Try
            intViewStateMode = Request.QueryString("viewStateMode")
            sessHelper.SetSession("VIEWSTATEMODEBB", intViewStateMode)
        Catch
        End Try
        Try
            screenFrom = Request.QueryString("screenFrom")
            sessHelper.SetSession("SCREENFROM", intViewStateMode)
        Catch
        End Try
        Try
            pqrId = CType(Request.QueryString("PQRId").ToString, Integer)
            sessHelper.SetSession("PQRID", intViewStateMode)
        Catch
        End Try
        Try
            wscId = CType(Request.QueryString("WSCId").ToString, Integer)
            sessHelper.SetSession("WSCID", intViewStateMode)
        Catch
        End Try

        InitiateAuthorization()
        lblPopUpDealerBranch.Attributes("onclick") = "ShowPPDealerBranchSelection();"
        lnkbtnPopUpInfoDokumen.Attributes("onClick") = "ShowPPInfoDokumenSelection()"
        lnkbtnPopUpRefClaimNumber.Attributes("onClick") = "ShowPPInfoWSCSelection()"
        lnkbtnPopUpInfoKendaraan.Attributes("onclick") = "ShowPPInfoKendaraanSelection()"
        btnPermintaanBukti.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../Service/FrmWSCSendEmailBB.aspx", "", 600, 600, "DummyFunction")

        If Not IsPostBack Then
            BindClaimType()
            BindRefDoc()
            BindKodePosisiWSC()

            sessHelper.SetSession("NEW_WSCHEADERBB", oWSCHeaderBB)
            sessHelper.SetSession("NEW_ONGKOSKERJABB", New ArrayList)
            sessHelper.SetSession("NEW_ONGKOSPARTSBB", New ArrayList)
            sessHelper.SetSession("NEW_WSCEVIDENCEBB", New ArrayList)

            ViewState("screenFrom") = screenFrom
            fillForm()

        End If
    End Sub

    Private Function GetAttachmentList(ByVal attachmentCollection As ArrayList) As ArrayList
        Dim TempList As New ArrayList
        TempList.Clear()
        For Each obj As WSCEvidenceBB In attachmentCollection
            TempList.Add(obj)
        Next
        Return TempList
    End Function

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        btnBatal_Click()
    End Sub
    Private Sub btnBatal_Click()
        sessHelper.RemoveSession("CriteriaFormWSCHeaderBB")
        If Request.QueryString("Src") = "WSCDetailBB" Then
            Server.Transfer("~/Service/FrmWSCDetailBB.aspx")
        Else
            screenFrom = Request.QueryString("screenFrom")
            If IsNothing(screenFrom) OrElse screenFrom = "" Then
                screenFrom = CType(ViewState("screenFrom"), String)
            End If

            If screenFrom = "PQR" Then
                Server.Transfer("~/PQR/FrmPQRListBB.aspx")
            ElseIf screenFrom = "WSC" Then
                Server.Transfer("~/Service/FrmWSCStatusListBB.aspx")
            End If
        End If
    End Sub

    Private Sub ddlClaimType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClaimType.SelectedIndexChanged
        If screenFrom = "PQR" Then
            ddlRefDoc.Enabled = False
            ddlRefDoc.SelectedValue = "1"
            Return
        End If
        txtPQRNo.Text = ""
        ResetPQRHeaderBBInfo()
        If ddlClaimType.SelectedValue.Trim = "Z2" Then
            ddlRefDoc.Enabled = True
            'ddlRefDoc.Enabled = False
            'ddlRefDoc.SelectedValue = "1"  'Referensi from PQR
        ElseIf ddlClaimType.SelectedValue.Trim = "Z4" Then
            ddlRefDoc.Enabled = False
            ddlRefDoc.SelectedValue = "0"  'Referensi from Service Buletin
        ElseIf ddlClaimType.SelectedValue.Trim = "Z6" AndAlso Mode = enumMode.Mode.NewItemMode Then
            ddlRefDoc.Enabled = True
            'Referensi from PQR & Service Buletin
        Else
            ddlRefDoc.Enabled = False
            If Mode = enumMode.Mode.NewItemMode Then
                ddlRefDoc.SelectedIndex = -1
            End If
        End If

        sessHelper.SetSession("ClaimType", ddlClaimType.SelectedValue.Trim)
        ddlRefDoc_SelectedIndexChanged()
    End Sub

    Private Sub ddlRefDoc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRefDoc.SelectedIndexChanged
        ddlRefDoc_SelectedIndexChanged()
    End Sub

    Private Sub ddlRefDoc_SelectedIndexChanged()
        ResetPQRHeaderBBInfo()
        txtRefClaimNumber.Enabled = False

        lnkbtnPopUpRefClaimNumber.Visible = False
        lnkbtnPopUpInfoKendaraan.Visible = False
        lnkbtnCheckChassis.Attributes("style") = "display:none"
        lblSearchChassis.Visible = False
        lnkbtnPopUpInfoDokumen.Visible = True
        txtPQRNo.Enabled = True
        txtNoChasis.Enabled = False

        If ddlClaimType.SelectedValue.Trim = "Z2" Then
            If ddlRefDoc.SelectedValue = "1" Then 'Referensi from PQR
            End If
        ElseIf ddlClaimType.SelectedValue.Trim = "Z4" Then
            If ddlRefDoc.SelectedValue = "0" Then 'Referensi from Service Buletin
            End If
        ElseIf ddlClaimType.SelectedValue.Trim = "Z6" Then
            lnkbtnPopUpInfoKendaraan.Visible = False
            lnkbtnCheckChassis.Attributes("style") = "display:none"
            lblSearchChassis.Visible = False
            If ddlRefDoc.SelectedValue = "0" Then  'Referensi Buletin Doc
                txtNoChasis.ReadOnly = True
            Else
                txtNoChasis.ReadOnly = False
            End If
        Else
            txtPQRNo.Enabled = False
            If ddlRefDoc.SelectedIndex = 2 Then
                txtPQRNo.Visible = False
                ddlRefDoc.Visible = False
                lblPQRNoVal.Visible = True
            End If
            lnkbtnPopUpInfoDokumen.Visible = False
            ddlKodeWSCA.SelectedIndex = 0
            ddlKodeWSCB.SelectedIndex = 0
            ddlKodeWSCC.SelectedIndex = 0
            ddlKodeWSCA.Enabled = False
            ddlKodeWSCB.Enabled = False
            ddlKodeWSCC.Enabled = False
        End If

        sessHelper.SetSession("RefDoc", ddlRefDoc.SelectedValue.Trim)

        If screenFrom <> "PQR" Then
            txtPQRNo.Text = ""
        End If
    End Sub

    Private Sub dgOngkosKerja_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgOngkosKerja.ItemCommand
        Dim blnBindDataGrid As Boolean = False
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        Dim _arrONGKOSKERJA As ArrayList = CType(sessHelper.GetSession("ONGKOSKERJABB"), ArrayList)
        If Mode = enumMode.Mode.NewItemMode Then
            _arrONGKOSKERJA = CType(sessHelper.GetSession("NEW_ONGKOSKERJABB"), ArrayList)
        End If

        Dim lblPositionCode As Label = CType(e.Item.FindControl("lblPositionCode"), Label)
        Dim txtPositionCode As TextBox = CType(e.Item.FindControl("txtPositionCodeFooter"), TextBox)

        Dim lblWorkCode As Label = CType(e.Item.FindControl("lblWorkCode"), Label)
        Dim txtWorkCode As TextBox = CType(e.Item.FindControl("txtWorkCodeFooter"), TextBox)

        Dim Quantity As Label = CType(e.Item.FindControl("lblQuantity"), Label)
        'Dim txtQuantity As TextBox = CType(e.Item.FindControl("txtQuantityItem"), TextBox)
        Dim txtQuantityFooter As TextBox = CType(e.Item.FindControl("txtQuantityFooter"), TextBox)

        Dim Amon As Label = CType(e.Item.FindControl("lblAmount"), Label)
        'Dim txtAmon As TextBox = CType(e.Item.FindControl("txtAmountItem"), TextBox)
        Dim txtPartPriceFooter As TextBox = CType(e.Item.FindControl("txtPartPriceFooter"), TextBox)

        Dim lnkbtnAddOKerja As LinkButton = CType(e.Item.FindControl("lnkbtnAddOKerja"), LinkButton)
        Dim lnkbtnDeleteOKerja As LinkButton = CType(e.Item.FindControl("lnkbtnDeleteOKerja"), LinkButton)

        Select Case e.CommandName
            Case "Add" 'Insert New item to datagrid
                If IsNothing(txtNoChasis.Text) OrElse txtNoChasis.Text = String.Empty Then
                    MessageBox.Show("Nomor Rangka masih kosong")
                    Return
                End If

                If IsNothing(txtPositionCode) OrElse txtPositionCode.Text = String.Empty Then
                    MessageBox.Show("Kode Posisi masih kosong")
                    Return
                End If
                If IsNothing(txtWorkCode) OrElse txtWorkCode.Text = String.Empty Then
                    MessageBox.Show("Kode Kerja masih kosong")
                    Return
                End If
                If IsNothing(txtQuantityFooter) OrElse txtQuantityFooter.Text = String.Empty OrElse CType(txtQuantityFooter.Text, Integer) = 0 Then
                    MessageBox.Show("Quantity masih kosong atau masih nol")
                    Return
                End If
                If IsNothing(txtPartPriceFooter) OrElse txtPartPriceFooter.Text = String.Empty Then
                    If txtPositionCode.Text.Trim.ToUpper = "XEE999" AndAlso txtWorkCode.Text.ToUpper = "99" Then
                        If CType(txtPartPriceFooter.Text.Trim, Integer) = 0 OrElse txtPartPriceFooter.Text.Trim = "" Then
                            txtPartPriceFooter.Enabled = True
                            MessageBox.Show("Silahkan input amount Ongkos Kerja !")
                            Return
                        End If
                        If txtPartPriceFooter.Text.Trim <> "" Then
                            If CType(txtPartPriceFooter.Text.Trim, Integer) = 0 Then
                                txtPartPriceFooter.Enabled = True
                                MessageBox.Show("Silahkan input amount Ongkos Kerja !")
                                Return
                            End If
                        End If
                    Else
                        txtPartPriceFooter.Visible = False
                        txtPartPriceFooter.Text = "1"
                    End If
                End If

                GetObjectLaborMaster(txtPositionCode.Text.Trim(), txtWorkCode.Text.Trim())

                If Not PositionWorkCodeIsExist(objKodePosisi.ID, objKodeKerja.ID, _arrONGKOSKERJA) Then
                    If LoopDgrid("DGONGKOSKERJA", objLaborMaster, Nothing, "Add", Nothing) Then
                        setDisableKodeKerusakan()
                        blnBindDataGrid = True
                    End If
                Else
                    MessageBox.Show(SR.DataIsExist("Kode Posisi dan Kode Kerja"))
                    Return
                End If

            Case "Delete" 'Delete this datagrid item 
                If LoopDgrid("DGONGKOSKERJA", objLaborMaster, Nothing, "Delete", e.Item.ItemIndex) Then
                    blnBindDataGrid = True
                End If
                If ddlClaimType.SelectedValue = "Z4" AndAlso ddlRefDoc.SelectedIndex = 1 Then
                    dgOngkosKerja.ShowFooter = False
                End If
        End Select

        If blnBindDataGrid Then
            BindOngkosKerja()
        End If
    End Sub

    Private Sub dgOngkosKerja_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgOngkosKerja.ItemDataBound
        If e.Item.ItemType = ListItemType.Footer Then
            SetdgOngkosKerjaItemFooter(e)
        End If
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.EditItem Then
            SetdgOngkosKerjaItem(e)
        End If
        If e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            SetdgOngkosKerjaItemEdit(e)
        End If
    End Sub


    Private Sub SetdgOngkosKerjaItemFooter(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        'Dim lblPopUp As Label = CType(e.Item.Cells(1).FindControl("lblSearchDamageFooter"), Label)
        'lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpPositionCodeSelection.aspx", "", 710, 700, "GetSelectedONGKOSKERJA")

        Dim txtAmon As TextBox = CType(e.Item.FindControl("txtPartPriceFooter"), TextBox)
        Dim txtPositionCode As TextBox = CType(e.Item.FindControl("txtPositionCodeFooter"), TextBox)
        Dim txtWorkCode As TextBox = CType(e.Item.FindControl("txtWorkCodeFooter"), TextBox)
        Dim txtQuantity As TextBox = CType(e.Item.FindControl("txtQuantityFooter"), TextBox)

        'If txtPositionCode.Text.Trim.ToUpper = "XEE999" AndAlso txtWorkCode.Text.Trim.ToUpper = "99" Then
        If (txtWorkCode.Text.Trim.ToUpper = "90" OrElse txtWorkCode.Text.Trim.ToUpper = "99") Then
            txtAmon.Attributes("style") = "display:table-row"
            txtQuantity.ReadOnly = True
        Else
            txtQuantity.ReadOnly = False
            txtAmon.Attributes("style") = "display:none"
            txtAmon.Text = "0"
        End If
        txtPositionCode.Attributes.Add("readonly", "readonly")
        txtWorkCode.Attributes.Add("readonly", "readonly")
    End Sub

    Private Sub SetdgOngkosKerjaItemEdit(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        'Dim lblPopUp As Label = CType(e.Item.Cells(1).FindControl("lblSearchDamageEdit"), Label)
        'lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpPositionCodeSelection.aspx", "", 710, 700, "GetSelectedONGKOSKERJA")
    End Sub

    Private Sub SetdgOngkosKerjaItem(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        'Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
        'e.Item.Cells(0).Controls.Add(lNum)
        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        lblNo.Text = e.Item.ItemIndex + 1 + (dgOngkosKerja.CurrentPageIndex * dgOngkosKerja.PageSize)
        Dim owscDet As WSCDetailBB = CType(e.Item.DataItem, WSCDetailBB)

        Dim lblPositionCode As Label = CType(e.Item.FindControl("lblPositionCode"), Label)
        Dim txtPositionCodeItem As TextBox = CType(e.Item.FindControl("txtPostionCodeItem"), TextBox)
        Dim txtPositionCode As TextBox = CType(e.Item.FindControl("txtPositionCodeFooter"), TextBox)

        Dim lblWorkCode As Label = CType(e.Item.FindControl("lblWorkCode"), Label)
        Dim txtWorkCodeItem As TextBox = CType(e.Item.FindControl("txtWorkCodeItem"), TextBox)
        Dim txtWorkCode As TextBox = CType(e.Item.FindControl("txtWorkCodeFooter"), TextBox)

        Dim Quantity As Label = CType(e.Item.FindControl("lblQuantity"), Label)
        Dim txtQuantity As TextBox = CType(e.Item.FindControl("txtQuantityItem"), TextBox)
        Dim txtQuantityFooter As TextBox = CType(e.Item.FindControl("txtQuantityFooter"), TextBox)

        Dim Amon As Label = CType(e.Item.FindControl("lblAmount"), Label)
        Dim txtAmon As TextBox = CType(e.Item.FindControl("txtAmountItem"), TextBox)
        Dim txtPartPriceFooter As TextBox = CType(e.Item.FindControl("txtPartPriceFooter"), TextBox)

        Dim lnkbtnAddOKerja As LinkButton = CType(e.Item.FindControl("lnkbtnAddOKerja"), LinkButton)
        Dim lnkbtnDeleteOKerja As LinkButton = CType(e.Item.FindControl("lnkbtnDeleteOKerja"), LinkButton)

        Dim cbMasterValidItem As CheckBox = CType(e.Item.FindControl("cbMasterValidItem"), CheckBox)

        txtPositionCodeItem.Text = owscDet.PositionCode
        txtWorkCodeItem.Text = owscDet.WorkCode

        GetObjectLaborMaster(txtPositionCodeItem.Text.Trim(), txtWorkCodeItem.Text.Trim())
        If Not IsNothing(objLaborMaster) Then
            cbMasterValidItem.Checked = True
        Else
            cbMasterValidItem.Checked = False
        End If

        If Mode <> enumMode.Mode.NewItemMode AndAlso IsNothing(owscDet.LaborMaster) Then
            UpdateWscDetailLaborID(owscDet)
        End If

        If (txtWorkCodeItem.Text.Trim.ToUpper = "90" OrElse txtWorkCodeItem.Text.Trim.ToUpper = "99") Then
            txtAmon.Attributes("style") = "display:table-row"
            txtQuantity.Text = "1"
            txtQuantity.ReadOnly = True
        Else
            txtAmon.Attributes("style") = "display:none"
            txtAmon.Text = "0"
            txtQuantity.Text = "1"
            txtQuantity.ReadOnly = False
        End If

        txtPositionCodeItem.Attributes.Add("readonly", "readonly")
        txtWorkCodeItem.Attributes.Add("readonly", "readonly")
    End Sub

    Private Function PositionWorkCodeIsExist(ByVal CodePositionID As Integer, ByVal CodeWorkID As Integer, ByVal ONGKOSKERJACollection As ArrayList) As Boolean
        Dim bResult As Boolean = False
        Try
            If ONGKOSKERJACollection.Count > 0 Then
                For Each _objWSCDetailBB As WSCDetailBB In ONGKOSKERJACollection
                    Dim criterias As New CriteriaComposite(New Criteria(GetType(DeskripsiKodePosisi), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(DeskripsiKodePosisi), "KodePosition", MatchType.Exact, _objWSCDetailBB.PositionCode.Trim))
                    Dim tempArr As ArrayList = New DeskripsiPositionCodeFacade(User).Retrieve(criterias)
                    If tempArr.Count > 0 Then
                        Dim _deskripsiKodePosisi As DeskripsiKodePosisi = CType(tempArr(0), DeskripsiKodePosisi)
                        If _deskripsiKodePosisi.ID = CodePositionID Then
                            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(DeskripsiKodeKerja), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias2.opAnd(New Criteria(GetType(DeskripsiKodeKerja), "KodeKerja", MatchType.Exact, _objWSCDetailBB.WorkCode.Trim))
                            Dim tempArr2 As ArrayList = New DeskripsiWorkPositionFacade(User).Retrieve(criterias2)
                            If tempArr2.Count > 0 Then
                                Dim _deskripsiKodeKerja As DeskripsiKodeKerja = CType(tempArr2(0), DeskripsiKodeKerja)
                                If _deskripsiKodeKerja.ID = CodeWorkID Then
                                    bResult = True
                                    Exit For
                                End If
                            End If
                        End If
                    End If
                Next
            End If
        Catch ex As Exception
        End Try

        Return bResult
    End Function

    Private Function PositionCodeIsExist(ByVal KodePosisi As String) As Boolean
        Try
            Dim criterias As New CriteriaComposite(New Criteria(GetType(DeskripsiKodePosisi), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DeskripsiKodePosisi), "KodePosition", MatchType.Exact, KodePosisi.Trim))

            Dim tempArr As ArrayList = New DeskripsiPositionCodeFacade(User).Retrieve(criterias)

            If tempArr.Count > 0 Then
                Return True
            End If
        Catch ex As Exception
        End Try

        Return False
    End Function

    Private Function WorkCodeIsExist(ByVal KodeKerja As String) As Boolean
        Try
            Dim criterias As New CriteriaComposite(New Criteria(GetType(DeskripsiKodeKerja), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DeskripsiKodeKerja), "KodeKerja", MatchType.Exact, KodeKerja.Trim))

            Dim tempArr As ArrayList = New DeskripsiWorkPositionFacade(User).Retrieve(criterias)

            If tempArr.Count > 0 Then
                Return True
            End If
        Catch ex As Exception
        End Try

        Return False
    End Function

    Private Sub dgParts_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgParts.ItemDataBound
        If e.Item.ItemType = ListItemType.Footer Then
            SetdgPartsItemFooter(e)
        End If
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.EditItem Then
            SetdgPartsItem(e)
        End If
        If e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            SetdgPartsItemEdit(e)
        End If
    End Sub

    Private Sub SetdgPartsItemFooter(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim txtAmon As TextBox = CType(e.Item.FindControl("txtPartPriceFooter"), TextBox)
        Dim txtParts As TextBox = CType(e.Item.FindControl("txtKodePartsFooter"), TextBox)

        'If Not IsNothing(sessHelper.GetSession("ClaimType")) AndAlso sessHelper.GetSession("ClaimType").ToString = "Z4" Then
        '    txtAmon.Enabled = False
        '    txtAmon.Attributes("style") = "display:none"
        '    If txtParts.Text.Trim.ToUpper = "NPN7" Then
        '        txtAmon.Enabled = True
        '        txtAmon.Attributes("style") = "display:table-row"
        '    End If
        'ElseIf Not IsNothing(sessHelper.GetSession("ClaimType")) AndAlso Not IsNothing(sessHelper.GetSession("RefDoc")) Then
        '    txtAmon.Enabled = False
        '    txtAmon.Attributes("style") = "display:none"
        '    If sessHelper.GetSession("ClaimType").ToString = "Z6" AndAlso sessHelper.GetSession("RefDoc").ToString = "1" Then
        If txtParts.Text.Trim.ToUpper = "NPN7" Then
            txtAmon.Enabled = True
            txtAmon.Attributes("style") = "display:table-row"
            '        End If
            '    End If
        Else
            txtAmon.Text = 0
            txtAmon.Enabled = False
            txtAmon.Attributes("style") = "display:none"
        End If

        'If ddlClaimType.SelectedValue = "Z4" Then

        'loopOngker()
        If ddlRefDoc.SelectedIndex = 1 OrElse loopOngker() Then
            Dim cbMainPartFooter As CheckBox = CType(e.Item.Cells(1).FindControl("cbMainPartFooter"), CheckBox)
            cbMainPartFooter.Enabled = False
            cbMainPartFooter.Checked = False
        End If
    End Sub


    Private Function loopOngker()
        For Each item As DataGridItem In dgOngkosKerja.Items
            If item.ItemType = ListItemType.Item OrElse item.ItemType = ListItemType.AlternatingItem Then
                If CType(item.FindControl("txtPostionCodeItem"), TextBox).Text.ToUpper = "XEE999" _
                    AndAlso CType(item.FindControl("txtWorkCodeItem"), TextBox).Text.ToUpper = "99" Then
                    Return True
                End If
            End If
        Next
        Return False
    End Function

    Private Sub SetdgPartsItemEdit(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        'Dim lblPopUp As Label = CType(e.Item.Cells(1).FindControl("lblSearchPartsEdit"), Label)
        'lblPopUp.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../PopUp/PopUpPartsCodeSelection.aspx", "", 710, 700, "GetSelectedPartsCode")
    End Sub

    Private Sub SetdgPartsItem(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        lblNo.Text = e.Item.ItemIndex + 1 + (dgParts.CurrentPageIndex * dgParts.PageSize)

        Dim cbMainPartItem As CheckBox = CType(e.Item.FindControl("cbMainPartItem"), CheckBox)
        Dim hdnMainPartItem As HiddenField = CType(e.Item.FindControl("hdnMainPartItem"), HiddenField)
        Dim txtQty As TextBox = CType(e.Item.FindControl("txtQtyItem"), TextBox)
        Dim txtAmon As TextBox = CType(e.Item.FindControl("txtPartPriceItem"), TextBox)
        Dim txtParts As Label = CType(e.Item.FindControl("lblKodeParts"), Label)

        If hdnMainPartItem.Value = "0" Then
            cbMainPartItem.Checked = False
        ElseIf hdnMainPartItem.Value = "1" Then
            cbMainPartItem.Checked = True
        End If
        'If Not IsNothing(sessHelper.GetSession("ClaimType")) AndAlso sessHelper.GetSession("ClaimType").ToString = "Z4" Then
        '    txtAmon.Enabled = False
        '    txtAmon.Attributes("style") = "display:none"
        '    If txtParts.Text.Trim.ToUpper = "NPN7" Then
        '        txtAmon.Enabled = True
        '        txtAmon.Attributes("style") = "display:table-row"
        '    End If
        'ElseIf Not IsNothing(sessHelper.GetSession("ClaimType")) AndAlso sessHelper.GetSession("ClaimType").ToString = "Z6" AndAlso sessHelper.GetSession("RefDoc").ToString = "1" Then
        '    txtAmon.Enabled = False
        '    txtAmon.Attributes("style") = "display:none"
        If txtParts.Text.Trim.ToUpper = "NPN7" Then
            txtAmon.Enabled = True
            txtAmon.Attributes("style") = "display:table-row"
            'End If
        Else
            txtAmon.Enabled = False
            txtAmon.Attributes("style") = "display:none"
        End If

        If ddlRefDoc.SelectedIndex = 1 OrElse loopOngker() Then
            If e.Item.ItemType = ListItemType.Footer Then
                Dim cbMainPartFooter As CheckBox = CType(e.Item.FindControl("cbMainPartFooter"), CheckBox)
                cbMainPartFooter.Enabled = False
                cbMainPartFooter.Checked = False
            ElseIf e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim cbMainPart As CheckBox = CType(e.Item.FindControl("cbMainPartItem"), CheckBox)
                cbMainPart.Enabled = False
                cbMainPart.Checked = False
            End If
        End If

        If Mode = enumMode.Mode.ViewMode Then
            txtQty.Enabled = False
            txtAmon.Enabled = False
            cbMainPartItem.Enabled = False
        End If
    End Sub

    Private Function PartsCodeIsExist(ByVal SparePartID As Integer, ByVal arrPartsCodeCollection As ArrayList) As Boolean
        Dim bResult As Boolean = False
        If arrPartsCodeCollection.Count > 0 Then
            For Each _PQRPartsCode As WSCDetailBB In arrPartsCodeCollection
                If _PQRPartsCode.SparePartMaster.ID = SparePartID Then
                    bResult = True
                    Exit For
                End If
            Next
        End If
        Return bResult
    End Function

    Private Function PartsCodeIsExist(ByVal SparePartID As Integer, ByVal PartsCodeCollection As ArrayList, ByVal nIndeks As Integer) As Boolean
        Dim i As Integer = 0
        Dim bResult As Boolean = False

        If PartsCodeCollection.Count > 0 Then
            For Each _PQRPartsCode As WSCDetailBB In PartsCodeCollection
                If _PQRPartsCode.SparePartMaster.ID = SparePartID AndAlso nIndeks <> i Then
                    bResult = True
                    Exit For
                End If
                i += 1
            Next
        End If
        Return bResult
    End Function

    Private Function MainPartIsExist(ByVal PartsCodeCollection As ArrayList) As Boolean
        Dim bResult As Boolean = False

        If PartsCodeCollection.Count > 0 Then
            For Each _PQRPartsCode As WSCDetailBB In PartsCodeCollection
                If _PQRPartsCode.MainPart = 1 Then
                    bResult = True
                    Exit For
                End If
            Next
        End If
        Return bResult
    End Function

    Private Function PartsCodeIsExist(ByVal SparePartCode As String) As Boolean
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SparePartMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SparePartMaster), "PartNumber", MatchType.Exact, SparePartCode.Trim))

        Dim tempArr As ArrayList = New SparePartMasterFacade(User).Retrieve(criterias)

        If tempArr.Count > 0 Then
            Return True
        End If

        Return False
    End Function

#End Region

#Region "Datagrid Ongkos Kerja"

#End Region


#Region "Datagrid Parts"

    Private Sub dgParts_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgParts.ItemCommand

        Mode = CType(ViewState("Mode"), enumMode.Mode)
        Dim _arrPartsCode As ArrayList = CType(sessHelper.GetSession("ONGKOSPARTSBB"), ArrayList)
        If Mode = enumMode.Mode.NewItemMode Then
            _arrPartsCode = CType(sessHelper.GetSession("NEW_ONGKOSPARTSBB"), ArrayList)
        End If
        Select Case e.CommandName
            Case "Add" 'Insert New item to datagrid
                Dim txtPartsCode As TextBox = CType(e.Item.FindControl("txtKodePartsFooter"), TextBox)
                Dim txtQtyFooter As TextBox = CType(e.Item.FindControl("txtQtyFooter"), TextBox)
                Dim txtPartPriceFooter As TextBox = CType(e.Item.FindControl("txtPartPriceFooter"), TextBox)
                Dim cbMainPartFooter As CheckBox = CType(e.Item.FindControl("cbMainPartFooter"), CheckBox)
                Dim hdnMainPartItem As HiddenField = CType(e.Item.FindControl("hdnMainPartItem"), HiddenField)

                Dim objSparePartMaster As SparePartMaster

                If IsNothing(txtNoChasis.Text) OrElse txtNoChasis.Text = String.Empty Then
                    MessageBox.Show("Nomor Rangka masih kosong")
                    Return
                End If

                If IsNothing(txtPartsCode) OrElse txtPartsCode.Text = String.Empty Then
                    MessageBox.Show("Nomor Part masih kosong")
                    Return
                End If

                If IsNothing(txtQtyFooter) OrElse txtQtyFooter.Text = String.Empty OrElse CType(txtQtyFooter.Text.Trim, Integer) = 0 Then
                    MessageBox.Show("Quantity masih kosong atau masih nol")
                    Return
                End If
                If IsNothing(txtPartPriceFooter) OrElse txtPartPriceFooter.Text = String.Empty Then
                    If txtPartsCode.Text.Trim.ToUpper = "NPN7" Then
                        'If CType(txtPartPriceFooter.Text.Trim, Integer) = 0 OrElse txtPartPriceFooter.Text.Trim = "" Then
                        If txtPartPriceFooter.Text.Trim = "" Then
                            txtPartPriceFooter.Enabled = True
                            MessageBox.Show("Amount masih kosong atau nol")
                            Return
                        End If
                    Else
                        txtPartPriceFooter.Visible = False
                        txtPartPriceFooter.Text = "1"
                    End If
                End If

                If Not PartsCodeIsExist(txtPartsCode.Text) Then
                    MessageBox.Show("Nomor Part tidak terdaftar")
                    Return
                End If

                If cbMainPartFooter.Checked = True Then
                    If MainPartIsExist(_arrPartsCode) Then
                        MessageBox.Show("Main Part hanya boleh satu")
                        Return
                    End If
                End If

                If LoopDgrid("DGPARTS", Nothing, objSparePartMaster, "Add", Nothing) Then
                    setDisableKodeKerusakan()

                End If

            Case "Delete" 'Delete this datagrid item 
                If Mode = enumMode.Mode.NewItemMode Then
                    _arrPartsCode.RemoveAt(e.Item.ItemIndex)
                ElseIf Mode = enumMode.Mode.EditMode Then
                    Dim deletedOngkosKerja As WSCDetailBB = CType(_arrPartsCode(e.Item.ItemIndex), WSCDetailBB)
                    If deletedOngkosKerja.ID > 0 Then
                        Dim deletedArrLst As ArrayList
                        deletedArrLst = CType(sessHelper.GetSession("DELETEDPARTSBB"), ArrayList)
                        deletedArrLst.Add(deletedOngkosKerja)
                        sessHelper.SetSession("DELETEDPARTSBB", deletedArrLst)
                    End If
                    _arrPartsCode.RemoveAt(e.Item.ItemIndex)
                End If

        End Select


        BindOngkosParts()

    End Sub
#End Region

#Region "Datagrid Attachment Evidence"
    Private Sub dgFileWSCEvidenceBB_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgFileWSCEvidenceBB.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            'Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
            'e.Item.Cells(0).Controls.Add(lNum)

            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dgOngkosKerja.CurrentPageIndex * dgOngkosKerja.PageSize)

            Dim arrAttachment As ArrayList = CType(sessHelper.GetSession("WSCEVIDENCEBB"), ArrayList)
            Mode = CType(ViewState("Mode"), enumMode.Mode)
            If Mode = enumMode.Mode.NewItemMode Then
                arrAttachment = CType(sessHelper.GetSession("NEW_WSCEVIDENCEBB"), ArrayList)
            End If
            If arrAttachment.Count > 0 Then
                Dim objWSCEvidenceBB As WSCEvidenceBB = arrAttachment(e.Item.ItemIndex)

                Dim lblWSCEvidenceBBType As Label = CType(e.Item.FindControl("lblWSCEvidenceType"), Label)
                lblWSCEvidenceBBType.Text = EnumWSCEvidenceType.GetStringWSCEvType(objWSCEvidenceBB.EvidenceType)

                Dim lblFileWSCEVIDENCE As Label = CType(e.Item.FindControl("lblFileWSCEVIDENCE"), Label)
                If Mode = enumMode.Mode.NewItemMode Then
                    lblFileWSCEVIDENCE.Text = Path.GetFileName(objWSCEvidenceBB.AttachmentData.FileName)
                Else
                    lblFileWSCEVIDENCE.Text = Path.GetFileName(objWSCEvidenceBB.PathFile)
                End If
            End If
        End If
        If e.Item.ItemType = ListItemType.Footer Then
            Dim ddlWSCEvidenceBBTypeFooter As DropDownList = CType(e.Item.Cells(1).FindControl("ddlWSCEvidenceTypeFooter"), DropDownList)
            BindWSCEvidenceBBType(ddlWSCEvidenceBBTypeFooter)
        End If

    End Sub

    Private Function FileIsExist(ByVal intEvidenceType As Integer, ByVal FileName As String, ByVal AttachmentCollection As ArrayList) As Boolean
        Dim bResult As Boolean = False
        If AttachmentCollection.Count > 0 Then
            For Each objWSCEvidenceBB As WSCEvidenceBB In AttachmentCollection
                If Not IsNothing(objWSCEvidenceBB.AttachmentData) Then
                    If Path.GetFileName(objWSCEvidenceBB.AttachmentData.FileName) = FileName Then
                        If intEvidenceType = objWSCEvidenceBB.EvidenceType Then
                            bResult = True
                            Exit For
                        End If
                    End If
                Else
                    If intEvidenceType = objWSCEvidenceBB.EvidenceType Then
                        bResult = True
                        Exit For
                    End If
                End If
            Next
        End If
        Return bResult
    End Function
    Private Function FileIsExist(ByVal FileName As String, ByVal AttachmentCollection As ArrayList, ByVal nIndeks As Integer) As Boolean
        Dim i As Integer = 0
        Dim bResult As Boolean = False

        If AttachmentCollection.Count > 0 Then
            For Each objWSCEvidenceBB As WSCEvidenceBB In AttachmentCollection
                If Path.GetFileName(objWSCEvidenceBB.AttachmentData.FileName) = FileName AndAlso nIndeks <> i Then
                    bResult = True
                    Exit For
                End If
                i += 1
            Next
        End If
        Return bResult
    End Function


    Private Sub dgFileWSCEvidenceBB_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgFileWSCEvidenceBB.ItemCommand
        Dim _arrWSCEVIDENCE As ArrayList = CType(sessHelper.GetSession("WSCEVIDENCEBB"), ArrayList)
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        If Mode = enumMode.Mode.NewItemMode Then
            _arrWSCEVIDENCE = CType(sessHelper.GetSession("NEW_WSCEVIDENCEBB"), ArrayList)
        End If

        Select Case e.CommandName
            Case "Add" 'Insert New item to datagrid
                Dim ddlWSCEvidenceBBTypeFooter As DropDownList = CType(e.Item.FindControl("ddlWSCEvidenceTypeFooter"), DropDownList)
                Dim FileUpload As HtmlInputFile = CType(e.Item.FindControl("iFileWSCEVIDENCE"), HtmlInputFile)
                Dim objPostedData As HttpPostedFile
                Dim objWSCEvidenceBB As WSCEvidenceBB = New WSCEvidenceBB
                Dim sFileName As String

                If IsNothing(ddlWSCEvidenceBBTypeFooter) OrElse ddlWSCEvidenceBBTypeFooter.SelectedIndex = 0 Then
                    MessageBox.Show("Tipe Bukti masih kosong")
                    Return
                End If


                '========= Validasi Attacment tipe Kwitansi =======================================================================
                Dim _arrONGKOSKERJA As ArrayList = CType(sessHelper.GetSession("ONGKOSKERJABB"), ArrayList)
                Mode = CType(ViewState("Mode"), enumMode.Mode)
                If Mode = enumMode.Mode.NewItemMode Then
                    _arrONGKOSKERJA = CType(sessHelper.GetSession("NEW_ONGKOSKERJABB"), ArrayList)
                End If
                Dim blnIsExistPosition9990 As Boolean = False
                For Each _objWSCDetailBB As WSCDetailBB In _arrONGKOSKERJA
                    If _objWSCDetailBB.PositionCode = "99" OrElse _objWSCDetailBB.PositionCode = "90" Then
                        blnIsExistPosition9990 = True
                        Exit For
                    End If
                Next
                Dim _arrONGKOSPART As ArrayList = CType(sessHelper.GetSession("ONGKOSPARTSBB"), ArrayList)
                Mode = CType(ViewState("Mode"), enumMode.Mode)
                If Mode = enumMode.Mode.NewItemMode Then
                    _arrONGKOSPART = CType(sessHelper.GetSession("NEW_ONGKOSPARTSBB"), ArrayList)
                End If
                Dim blnIsExistPartNPN7 As Boolean = False
                For Each _objWSCDetailBB As WSCDetailBB In _arrONGKOSPART
                    If _objWSCDetailBB.SparePartMaster.PartCode.Trim = "NPN7" Then
                        blnIsExistPartNPN7 = True
                        Exit For
                    End If
                Next
                If IsNothing(ddlWSCEvidenceBBTypeFooter) OrElse ddlWSCEvidenceBBTypeFooter.SelectedValue = "0" Then
                    If blnIsExistPosition9990 = True And blnIsExistPartNPN7 = True Then
                        If IsNothing(FileUpload) OrElse FileUpload.Value = String.Empty Then
                            MessageBox.Show("Lampiran Kwitansi masih kosong")
                            Return
                        End If
                    End If
                End If
                If IsNothing(FileUpload) OrElse FileUpload.Value = String.Empty Then
                    MessageBox.Show("Lampiran masih kosong")
                    Return
                End If
                '=============================================================================================================


                If Not IsNothing(FileUpload) OrElse FileUpload.Value <> String.Empty Then
                    objPostedData = FileUpload.PostedFile
                Else
                    objPostedData = Nothing
                End If

                If Not (IsNothing(objPostedData)) Then
                    sFileName = objPostedData.FileName.Split("\")(objPostedData.FileName.Split("\").Length - 1)

                    If KTB.DNet.UI.Helper.FileHelper.IsExecutableFile(sFileName) Then
                        MessageBox.Show("Tidak diperkenankan mengupload file dengan ekstensi '.exe'. Pastikan file anda bebas dari virus.")
                        BindWSCEvidenceBB()
                        Return
                    End If

                    If Not FileIsExist(ddlWSCEvidenceBBTypeFooter.SelectedValue, sFileName, _arrWSCEVIDENCE) Then
                        Dim SrcFile As String = Path.GetFileName(objPostedData.FileName) '-- Source file name
                        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("WSCEvidenceFileDirectory") & "\" & lblDealerVal.Text & "\ClaimNumber\" & "SS" & TimeStamp() & SrcFile.Substring(SrcFile.Length - 4)  '-- Destination file
                        Dim FileName As String

                        objWSCEvidenceBB.EvidenceType = ddlWSCEvidenceBBTypeFooter.SelectedValue
                        objWSCEvidenceBB.Description = EnumWSCEvidenceType.GetStringWSCEvType(ddlWSCEvidenceBBTypeFooter.SelectedValue)
                        objWSCEvidenceBB.PathFile = DestFile
                        objWSCEvidenceBB.AttachmentData = objPostedData
                        objWSCEvidenceBB.UploadDate = DateTime.Now.Day
                        objWSCEvidenceBB.UploadMonth = DateTime.Now.Month
                        objWSCEvidenceBB.UploadYear = DateTime.Now.Year
                        objWSCEvidenceBB.WSCHeaderBB = Nothing

                        UploadAttachment(objWSCEvidenceBB, TempDirectory)

                        _arrWSCEVIDENCE.Add(objWSCEvidenceBB)

                        If Mode = enumMode.Mode.NewItemMode Then
                            sessHelper.SetSession("NEW_WSCEVIDENCEBB", _arrWSCEVIDENCE)
                        Else
                            sessHelper.SetSession("WSCEVIDENCEBB", _arrWSCEVIDENCE)
                        End If
                        'Page.RegisterStartupScript("test", "<script language=JavaScript> focusSave(); </script>")
                    Else
                        MessageBox.Show(SR.DataIsExist("Attachment File"))
                    End If
                Else
                    objWSCEvidenceBB.EvidenceType = ddlWSCEvidenceBBTypeFooter.SelectedValue
                    objWSCEvidenceBB.Description = EnumWSCEvidenceType.GetStringWSCEvType(ddlWSCEvidenceBBTypeFooter.SelectedValue)
                    objWSCEvidenceBB.PathFile = vbNull
                    objWSCEvidenceBB.AttachmentData = objPostedData
                    objWSCEvidenceBB.UploadDate = DateTime.Now.Day
                    objWSCEvidenceBB.UploadMonth = DateTime.Now.Month
                    objWSCEvidenceBB.UploadYear = DateTime.Now.Year
                    objWSCEvidenceBB.WSCHeaderBB = Nothing

                    _arrWSCEVIDENCE.Add(objWSCEvidenceBB)

                    If Mode = enumMode.Mode.NewItemMode Then
                        sessHelper.SetSession("NEW_WSCEVIDENCEBB", _arrWSCEVIDENCE)
                    Else
                        sessHelper.SetSession("WSCEVIDENCEBB", _arrWSCEVIDENCE)
                    End If
                End If

            Case "Delete" 'Delete this datagrid item 
                If Mode = enumMode.Mode.NewItemMode Then
                    RemoveWSCAttachment(CType(_arrWSCEVIDENCE(e.Item.ItemIndex), WSCEvidenceBB), TempDirectory)
                    _arrWSCEVIDENCE.RemoveAt(e.Item.ItemIndex)
                ElseIf Mode = enumMode.Mode.EditMode Then
                    Dim deletedWSCEvidenceBB As WSCEvidenceBB = CType(_arrWSCEVIDENCE(e.Item.ItemIndex), WSCEvidenceBB)
                    If deletedWSCEvidenceBB.ID > 0 Then
                        Dim deletedArrLst As ArrayList
                        deletedArrLst = CType(sessHelper.GetSession("DELETEDWSEVIDENCEBB"), ArrayList)
                        deletedArrLst.Add(deletedWSCEvidenceBB)
                        sessHelper.SetSession("DELETEDWSEVIDENCEBB", deletedArrLst)
                    End If
                    _arrWSCEVIDENCE.RemoveAt(e.Item.ItemIndex)
                End If

            Case "Download" 'Download File
                Response.Redirect("../Download.aspx?file=" & e.CommandArgument)

        End Select

        BindWSCEvidenceBB()
    End Sub

#End Region

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Dim result As Integer
        Dim ErrMessage As String = String.Empty
        Mode = CType(ViewState("Mode"), enumMode.Mode)


        If Not Page.IsValid Then
            MessageBox.Show("Lengkapi data terlebih dahulu")
            Return
        End If

        If ValidateSaveData() Then
            ''-- fill value to object WSCHeaderBB
            BindWSCHeaderBBDomain()

            If Mode = enumMode.Mode.NewItemMode Then
                oWSCHeaderBB = sessHelper.GetSession("NEW_WSCHEADERBB")
            Else
                oWSCHeaderBB = sessHelper.GetSession("WSCHEADERBB")
            End If

            If Mode = enumMode.Mode.NewItemMode Then
                result = oWSCHeaderBBFacade.InsertTransaction(oWSCHeaderBB, CType(sessHelper.GetSession("NEW_ONGKOSKERJABB"), ArrayList), CType(sessHelper.GetSession("NEW_ONGKOSPARTSBB"), ArrayList), CType(sessHelper.GetSession("NEW_WSCEVIDENCEBB"), ArrayList))
            ElseIf Mode = enumMode.Mode.EditMode Then
                result = oWSCHeaderBBFacade.UpdateTransaction(oWSCHeaderBB, CType(sessHelper.GetSession("ONGKOSKERJABB"), ArrayList), CType(sessHelper.GetSession("DELETEDONGKOSKERJABB"), ArrayList), CType(sessHelper.GetSession("ONGKOSPARTSBB"), ArrayList), CType(sessHelper.GetSession("DELETEDPARTSBB"), ArrayList), CType(sessHelper.GetSession("WSCEVIDENCEBB"), ArrayList), CType(sessHelper.GetSession("DELETEDWSEVIDENCEBB"), ArrayList), ErrMessage)
            End If

            If result > 0 Then
                Dim _chassisNumber As ChassisMasterBB = oChassisFacade.Retrieve(txtNoChasis.Text)
                Dim _vehicleCode As String = _chassisNumber.VechileColor.VechileType.VechileTypeCode

                Dim _arl As ArrayList = New ArrayList
                If Mode = enumMode.Mode.NewItemMode Then
                    _arl = CType(sessHelper.GetSession("NEW_ONGKOSKERJABB"), ArrayList)
                Else
                    _arl = CType(sessHelper.GetSession("ONGKOSKERJABB"), ArrayList)
                End If
                Dim noLabor As ArrayList = New ArrayList
                For Each objWSCDetailBB As WSCDetailBB In _arl
                    If objWSCDetailBB.PositionCode.Trim <> "" And objWSCDetailBB.WorkCode.Trim <> "" Then
                        Dim oLabMaster As LaborMaster = GetLaborMaster(objWSCDetailBB.PositionCode, objWSCDetailBB.WorkCode)
                        If IsNothing(oLabMaster) Then
                            noLabor.Add(objWSCDetailBB)
                        End If
                    End If
                Next

                MailReport(_vehicleCode, noLabor)

                ' Proses Upload di pisahkan dari simpan 
                'UploadAttachment(CType(sessHelper.GetSession("WSCEVIDENCEBB"), ArrayList), TargetDirectory)

                If Mode = enumMode.Mode.NewItemMode Then
                    CommitAttachment(CType(sessHelper.GetSession("NEW_WSCEVIDENCEBB"), ArrayList))
                Else
                    CommitAttachment(CType(sessHelper.GetSession("WSCEVIDENCEBB"), ArrayList))
                End If
                If Mode = enumMode.Mode.EditMode Then
                    RemoveWSCAttachment(CType(sessHelper.GetSession("DELETEDWSEVIDENCEBB"), ArrayList), TargetDirectory)
                End If
                ClearTempData()

                lblClaimNumber.Text = oWSCHeaderBBFacade.Retrieve(result).ClaimNumber.Trim


                MessageBox.Show("Simpan Data Sukses !")
                Server.Transfer("~/Service/FrmWSCStatusListBB.aspx")
            Else
                If ErrMessage = String.Empty Then
                    MessageBox.Show("Simpan Data Gagal !")
                Else
                    MessageBox.Show(ErrMessage)
                End If

            End If

        Else
            'MessageBox.Show("Data tidak lengkap. Silakan diperiksa lagi ")
        End If

    End Sub

    Private Sub lnkbtnPPCheckChassis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkbtnPPCheckChassis.Click
        hdnPPtxtNoChasis.Value = "1"
        If lnkbtnCheckChassisClick() = False Then Return
    End Sub

    Private Sub lnkbtnCheckChassis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkbtnCheckChassis.Click
        hdnPPtxtNoChasis.Value = "0"
        If lnkbtnCheckChassisClick() = False Then Return
    End Sub

    Private Function lnkbtnCheckChassisClick() As Boolean
        Dim intVechileTypeID1 As Integer = 0
        Dim intVechileTypeID2 As Integer = 0
        If txtNoChasis.Text.Trim = "" Then
            If hdntxtNoChasis.Value.Trim = "" Then
                MessageBox.Show("Silahkan input nomor rangka !")
                Return False
            End If
            txtNoChasis.Text = hdntxtNoChasis.Value.ToString.Trim
        End If
        If hdnPPtxtNoChasis.Value = "1" Then
            If hdntxtNoChasis.Value.Trim = "" Then Return False
            txtNoChasis.Text = hdntxtNoChasis.Value
        End If
        If oChassisFacade.IsExist(txtNoChasis.Text) Then
            txtNoChasis.ForeColor = Color.Black
            Dim strChassisNumber As String = txtNoChasis.Text.Trim()
            setEnabledReferensiDokumen(True)

            ddlKodeWSCA.Enabled = False
            ddlKodeWSCB.Enabled = False
            ddlKodeWSCC.Enabled = False

            If ddlRefDoc.SelectedValue = "1" Then 'PQR Ref Doc
                Dim criterias2 As New CriteriaComposite(New Criteria(GetType(PQRHeaderBB), "RowStatus", MatchType.No, CType(DBRowStatus.Deleted, Short)))
                criterias2.opAnd(New Criteria(GetType(PQRHeaderBB), "PQRNo", MatchType.Exact, txtPQRNo.Text.Trim))
                Dim _arrPQR As ArrayList = New PQRHeaderBBFacade(User).Retrieve(criterias2)
                If _arrPQR.Count > 0 Then
                    Dim objPQRHeaderBB As PQRHeaderBB = CType(_arrPQR(0), PQRHeaderBB)
                    intVechileTypeID1 = objPQRHeaderBB.ChassisMasterBB.VechileColor.VechileType.ID
                End If
            End If

            Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
            Dim criterias As New CriteriaComposite(New Criteria(GetType(ChassisMasterBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ChassisMasterBB), "ChassisNumber", MatchType.Exact, strChassisNumber))
            criterias.opAnd(New Criteria(GetType(ChassisMasterBB), "Category.ProductCategory.Code", MatchType.Exact, companyCode))
            Dim ChassisColl As ArrayList = New ChassisMasterBBFacade(User).Retrieve(criterias)
            If ChassisColl.Count > 0 Then
                Dim objChassisMasterBB As ChassisMasterBB = CType(ChassisColl(0), ChassisMasterBB)
                If ddlRefDoc.SelectedValue = "1" Then 'PQR Ref Doc
                    intVechileTypeID2 = objChassisMasterBB.VechileColor.VechileType.ID
                    If intVechileTypeID1 <> intVechileTypeID2 Then
                        txtNoChasis.ForeColor = Color.Red
                        MessageBox.Show("Tipe kendaraan harus sama dengan tipe kendaraan pada Dokumen PQR.")
                        txtNoChasis.Text = hdntxtNoChasis.Value
                        Return False
                    End If
                End If

                blnIsSoldDealer = False
                If oDealer.ID = objChassisMasterBB.Dealer.ID Then  '---Jika login dealer id = sold dealer id
                    blnIsSoldDealer = True
                End If

                'If IsNothing(objChassisMasterBB.EndCustomer) AndAlso objChassisMasterBB.FakturStatus = 0 Then
                '    MessageBox.Show("Kendaraan Belum Terjual")
                '    txtPQRNo.Text = ""
                '    Dim A As Object
                '    Dim E As EventArgs
                '    txtPQRNo_TextChanged(A, E)
                '    Exit Function
                'End If

                'If LoadChassisMasterBBPKT(objChassisMasterBB, blnIsSoldDealer) Then
                LoadChassisInfo(objChassisMasterBB)
                If ddlRefDoc.SelectedValue = "0" Then 'Buletin Ref Doc
                    Try
                        LoadPositionAndWorkCodes(txtNoChasis.Text.Trim, txtPQRNo.Text.Trim)
                    Catch
                    End Try
                End If
                txtNoChasis.ForeColor = Color.Black
                RefreshGrid()
                'Else
                '    ClearChassisInfo()
                '    Return False
                'End If
                'blnIsSoldDealer = False
                'If oDealer.ID = objChassisMasterBB.Dealer.ID Then  '---Jika login dealer id = sold dealer id
                '    blnIsSoldDealer = True
                'End If
                'If LoadChassisMasterBBPKT(objChassisMasterBB, blnIsSoldDealer) Then
                '        Catch
                '    End Try
                'End If
                'txtNoChasis.ForeColor = Color.Black
                'RefreshGrid()
            Else
                '    ClearChassisInfo()
                '    Return False
                '    txtNoChasis.ForeColor = Color.Black
                '    'RefreshGrid()
                'Else
                txtNoChasis.ForeColor = Color.Red
                MessageBox.Show("No Rangka tidak terdaftar.")
                ClearChassisInfo()
                txtNoChasis.Text = hdntxtNoChasis.Value
                Return False
            End If
        Else
            txtNoChasis.ForeColor = Color.Red
            MessageBox.Show("No Rangka tidak terdaftar.")
            ClearChassisInfo()
            txtNoChasis.Text = hdntxtNoChasis.Value
            Return False
        End If
    End Function

    Private Sub LoadPQRHeaderBBInfo(oPQRHeaderBB As PQRHeaderBB)
        txtPQRNo.Text = oPQRHeaderBB.PQRNo
        txtNoChasis.Text = oPQRHeaderBB.ChassisMasterBB.ChassisNumber
        'If ddlRefDoc.SelectedValue = 1 Then
        '    txtNoChasis.Enabled = False
        'End If
        hdntxtNoChasis.Value = txtNoChasis.Text
        lnkbtnCheckChassisClick()
        txtOdometer.Text = CInt(oPQRHeaderBB.OdoMeter)
        txtGejala.Text = oPQRHeaderBB.Symptomps
        txtPemeriksaan.Text = oPQRHeaderBB.Causes
        txtHasil.Text = oPQRHeaderBB.Results

        If oPQRHeaderBB.CodeA = "" Then
            ddlKodeWSCA.SelectedIndex = 0
        Else
            ddlKodeWSCA.SelectedValue = oPQRHeaderBB.CodeA
        End If
        If oPQRHeaderBB.CodeB = "" Then
            ddlKodeWSCB.SelectedIndex = 0
        Else
            ddlKodeWSCB.SelectedValue = oPQRHeaderBB.CodeB
        End If
        If oPQRHeaderBB.CodeC = "" Then
            ddlKodeWSCC.SelectedIndex = 0
        Else
            ddlKodeWSCC.SelectedValue = oPQRHeaderBB.CodeC
        End If

        Dim _arrONGKOSKERJA As ArrayList = New ArrayList
        For Each _objPQRDamageCode As PQRDamageCodeBB In oPQRHeaderBB.PQRDamageCodeBBs
            Dim objOngkosKeja As WSCDetailBB = New WSCDetailBB
            objOngkosKeja.WSCType = "L"
            objOngkosKeja.PositionCode = _objPQRDamageCode.DeskripsiKodePosisi.KodePosition
            _arrONGKOSKERJA.Add(objOngkosKeja)
        Next
        sessHelper.SetSession("NEW_ONGKOSKERJABB", _arrONGKOSKERJA)

        Dim _arrONGKOSPARTS As ArrayList = New ArrayList
        For Each _objPQRPartsCode As PQRPartsCodeBB In oPQRHeaderBB.PQRPartsCodeBBs
            Dim objOngkosPart As WSCDetailBB = New WSCDetailBB
            objOngkosPart.WSCType = "P"
            objOngkosPart.SparePartMaster = New SparePartMasterFacade(User).Retrieve(_objPQRPartsCode.SparePartMaster.PartNumber)
            _arrONGKOSPARTS.Add(objOngkosPart)
        Next
        sessHelper.SetSession("NEW_ONGKOSPARTSBB", _arrONGKOSPARTS)
        RefreshGrid()
        setEnabledReferensiDokumen(True)
    End Sub

    Private Sub ResetPQRHeaderBBInfo()
        txtRefClaimNumber.Text = ""
        txtNoChasis.Text = ""
        hdntxtNoChasis.Value = ""
        txtOdometer.Text = 0
        txtGejala.Text = ""
        txtPemeriksaan.Text = ""
        txtHasil.Text = ""
        ddlKodeWSCA.SelectedIndex = 0
        ddlKodeWSCB.SelectedIndex = 0
        ddlKodeWSCC.SelectedIndex = 0
        ClearChassisInfo()
        lnkbtnPopUpInfoKendaraan.Attributes.Clear()
        lnkbtnPopUpInfoKendaraan.Visible = False
        lnkbtnCheckChassis.Attributes("style") = "display:none"

        Mode = CType(ViewState("Mode"), enumMode.Mode)
        If Mode = enumMode.Mode.NewItemMode Then
            sessHelper.SetSession("NEW_ONGKOSKERJABB", New ArrayList)
            sessHelper.SetSession("NEW_ONGKOSPARTSBB", New ArrayList)
            sessHelper.SetSession("NEW_WSCEVIDENCEBB", New ArrayList)
        Else
            sessHelper.SetSession("ONGKOSKERJABB", New ArrayList)
            sessHelper.SetSession("ONGKOSPARTSBB", New ArrayList)
            sessHelper.SetSession("WSCEVIDENCEBB", New ArrayList)
        End If

        RefreshGrid()
        setEnabledReferensiDokumen(False)
    End Sub

    'Private Function LoadChassisMasterBBPKT(objChassisMasterBBs As ChassisMasterBB, blnLoginIsSoldDealer As Boolean)
    '    Dim result As Boolean = False
    '    Dim arrCMPKT As New ArrayList
    '    Dim oChassisMasterBBPKT As ChassisMasterBBPKT = New ChassisMasterBBPKT
    '    Dim oChassisMasterBBPKTFacade As ChassisMasterBBPKTFacade = New ChassisMasterBBPKTFacade(User)
    '    If Not IsNothing(objChassisMasterBBs) Then
    '        If blnLoginIsSoldDealer Then
    '            Dim criteriaCMPKT As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterBBPKT), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '            criteriaCMPKT.opAnd(New Criteria(GetType(ChassisMasterBBPKT), "ChassisMasterBBs.ID", MatchType.Exact, objChassisMasterBBs.ID))
    '            arrCMPKT = oChassisMasterBBPKTFacade.Retrieve(criteriaCMPKT)
    '            If Not IsNothing(arrCMPKT) AndAlso arrCMPKT.Count > 0 Then
    '                oChassisMasterBBPKT = CType(arrCMPKT(0), ChassisMasterBBPKT)
    '                ViewState("PKTDate") = oChassisMasterBBPKT.PKTDate
    '                result = True
    '            Else
    '                MessageBox.Show("Nomor rangka ini tidak valid, silahkan isi tanggal PKT")
    '            End If
    '        Else
    '            Dim criteriaCMPKT As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterBBPKT), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '            criteriaCMPKT.opAnd(New Criteria(GetType(ChassisMasterBBPKT), "ChassisMasterBBs.ID", MatchType.Exact, objChassisMasterBBs.ID))
    '            arrCMPKT = oChassisMasterBBPKTFacade.Retrieve(criteriaCMPKT)
    '            If IsNothing(arrCMPKT) OrElse arrCMPKT.Count = 0 Then
    '                If Not IsNothing(objChassisMasterBBs.EndCustomer) Then
    '                    If objChassisMasterBBs.EndCustomer.FakturDate <> CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
    '                        ViewState("PKTDate") = objChassisMasterBBs.EndCustomer.FakturDate
    '                        result = True
    '                    Else
    '                        If Not IsNothing(objChassisMasterBBs.DODate) Then
    '                            If objChassisMasterBBs.DODate <> CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
    '                                ViewState("PKTDate") = objChassisMasterBBs.DODate
    '                                result = True
    '                            Else
    '                                MessageBox.Show("Nomor rangka ini tidak valid karena tanggal PKT, tanggal faktur dan tanggal DO tidak ada")
    '                            End If
    '                        Else
    '                            MessageBox.Show("Nomor rangka ini tidak valid karena tanggal PKT, tanggal faktur dan tanggal DO tidak ada")
    '                        End If
    '                    End If
    '                Else
    '                    If Not IsNothing(objChassisMasterBBs.DODate) Then
    '                        If objChassisMasterBBs.DODate <> CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
    '                            ViewState("PKTDate") = objChassisMasterBBs.DODate
    '                            result = True
    '                        Else
    '                            MessageBox.Show("Nomor rangka ini tidak valid karena tanggal PKT, tanggal faktur dan tanggal DO tidak ada")
    '                        End If
    '                    Else
    '                        MessageBox.Show("Nomor rangka ini tidak valid karena tanggal PKT, tanggal faktur dan tanggal DO tidak ada")
    '                    End If
    '                End If
    '            Else
    '                oChassisMasterBBPKT = CType(arrCMPKT(0), ChassisMasterBBPKT)
    '                ViewState("PKTDate") = oChassisMasterBBPKT.PKTDate
    '                result = True
    '            End If
    '        End If
    '    End If

    '    Return result
    'End Function

    'Private Sub CheckIsExistChassisMasterBBPKT(oPQRHeaderBB As PQRHeaderBB, blnLoginIsSoldDealer As Boolean)
    '    Dim arrCMPKT As New ArrayList
    '    Dim oChassisMasterBBPKT As ChassisMasterBBPKT = New ChassisMasterBBPKT
    '    Dim oChassisMasterBBPKTFacade As ChassisMasterBBPKTFacade = New ChassisMasterBBPKTFacade(User)
    '    If blnLoginIsSoldDealer Then
    '        'Dim arrCMPKT As New ArrayList
    '        'Dim oChassisMasterBBPKTFacade As ChassisMasterBBPKTFacade = New ChassisMasterBBPKTFacade(User)
    '        Dim criteriaCMPKT As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterBBPKT), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '        criteriaCMPKT.opAnd(New Criteria(GetType(ChassisMasterBBPKT), "ChassisMasterBBs.ID", MatchType.Exact, oPQRHeaderBB.ChassisMasterBB.ID))
    '        arrCMPKT = oChassisMasterBBPKTFacade.Retrieve(criteriaCMPKT)
    '        If IsNothing(arrCMPKT) OrElse arrCMPKT.Count = 0 Then
    '            ResetPQRHeaderBBInfo()
    '            MessageBox.Show("Nomor rangka ini tidak valid, silahkan lengkapi data tanggal PKT terlebih dahulu")
    '            btnBatal_Click()
    '        Else
    '            oChassisMasterBBPKT = CType(arrCMPKT(0), ChassisMasterBBPKT)
    '            ViewState("PKTDate") = oChassisMasterBBPKT.PKTDate
    '            LoadPQRHeaderBBInfo(oPQRHeaderBB)
    '        End If
    '    Else
    '        'Dim arrCMPKT As New ArrayList
    '        'Dim oChassisMasterBBPKTFacade As ChassisMasterBBPKTFacade = New ChassisMasterBBPKTFacade(User)
    '        Dim criteriaCMPKT As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterBBPKT), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '        criteriaCMPKT.opAnd(New Criteria(GetType(ChassisMasterBBPKT), "ChassisMasterBBs.ID", MatchType.Exact, oPQRHeaderBB.ChassisMasterBB.ID))
    '        arrCMPKT = oChassisMasterBBPKTFacade.Retrieve(criteriaCMPKT)
    '        If IsNothing(arrCMPKT) OrElse arrCMPKT.Count = 0 Then
    '            If Not IsNothing(oPQRHeaderBB.ChassisMasterBB) Then
    '                If Not IsNothing(oPQRHeaderBB.ChassisMasterBB.EndCustomer) Then
    '                    If oPQRHeaderBB.ChassisMasterBB.EndCustomer.FakturDate <> CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
    '                        ViewState("PKTDate") = oPQRHeaderBB.ChassisMasterBB.EndCustomer.FakturDate
    '                        LoadPQRHeaderBBInfo(oPQRHeaderBB)
    '                    Else
    '                        If Not IsNothing(oPQRHeaderBB.ChassisMasterBB.DODate) Then
    '                            If oPQRHeaderBB.ChassisMasterBB.DODate <> CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
    '                                ViewState("PKTDate") = oPQRHeaderBB.ChassisMasterBB.DODate
    '                                LoadPQRHeaderBBInfo(oPQRHeaderBB)
    '                            Else
    '                                ResetPQRHeaderBBInfo()
    '                                MessageBox.Show("Nomor rangka ini tidak valid, tanggal faktur belum ada")
    '                                btnBatal_Click()
    '                            End If
    '                        Else
    '                            ResetPQRHeaderBBInfo()
    '                            MessageBox.Show("Nomor rangka ini tidak valid, tanggal faktur belum ada")
    '                            btnBatal_Click()
    '                        End If
    '                    End If
    '                Else
    '                    If Not IsNothing(oPQRHeaderBB.ChassisMasterBB.DODate) Then
    '                        If oPQRHeaderBB.ChassisMasterBB.DODate <> CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
    '                            ViewState("PKTDate") = oPQRHeaderBB.ChassisMasterBB.DODate
    '                            LoadPQRHeaderBBInfo(oPQRHeaderBB)
    '                        Else
    '                            ResetPQRHeaderBBInfo()
    '                            MessageBox.Show("Nomor rangka ini tidak valid karena tanggal PKT, tanggal faktur dan tanggal DO tidak ada")
    '                            btnBatal_Click()
    '                        End If
    '                    Else
    '                        ResetPQRHeaderBBInfo()
    '                        MessageBox.Show("Nomor rangka ini tidak valid, tanggal faktur belum ada")
    '                        btnBatal_Click()
    '                    End If
    '                End If
    '            End If
    '        Else
    '            oChassisMasterBBPKT = CType(arrCMPKT(0), ChassisMasterBBPKT)
    '            ViewState("PKTDate") = oChassisMasterBBPKT.PKTDate
    '            LoadPQRHeaderBBInfo(oPQRHeaderBB)
    '        End If
    '    End If
    'End Sub

    Private Sub txtPQRNo_TextChanged(sender As Object, e As EventArgs) Handles txtPQRNo.TextChanged
        oDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        If txtPQRNo.Text.Trim <> "" Then
            txtRefClaimNumber.Enabled = True
            lnkbtnPopUpRefClaimNumber.Visible = True

            If ddlRefDoc.SelectedValue = "1" Then  'PQR Reference
                Dim oPQRHeader As PQRHeaderBB = New PQRHeaderBBFacade(User).Retrieve(txtPQRNo.Text)
                If Not IsNothing(oPQRHeader) AndAlso oPQRHeader.ID > 0 Then
                    LoadPQRHeaderInfo(oPQRHeader)
                Else
                    MessageBox.Show("Dokumen tidak Valid")
                    Return
                End If
                setDisableKodeKerusakan()
                'If ddlClaimType.SelectedValue.Trim = "Z2" Then
                txtNoChasis.Enabled = True
                txtNoChasis.ReadOnly = False
                'End If
            Else 'If ddlRefDoc.SelectedValue = "0" Then   'Buletin reference
                Dim oRecallCategory As RecallCategory = New RecallCategoryFacade(User).Retrieve(txtPQRNo.Text)
                If Not IsNothing(oRecallCategory) Then
                    ResetPQRHeaderBBInfo()
                    txtGejala.Text = oRecallCategory.Description
                    txtNoChasis.Enabled = True
                    'txtNoChasis.ReadOnly = True
                    lblSearchChassis.Visible = True
                    lnkbtnCheckChassis.Attributes("style") = "display:table-row"
                Else
                    lblSearchChassis.Visible = False
                    lnkbtnCheckChassis.Attributes("style") = "display:none"
                End If
                lblSearchChassis.Visible = True
                lnkbtnCheckChassis.Visible = True
                txtNoChasis.Enabled = True
                'txtNoChasis.ReadOnly = False
            End If
        Else
            ResetPQRHeaderBBInfo()
            setEnabledReferensiDokumen(False)
            txtNoChasis.Enabled = False
            txtRefClaimNumber.Enabled = False
            lnkbtnPopUpRefClaimNumber.Visible = False
            lblSearchChassis.Visible = False
            lnkbtnCheckChassis.Attributes("style") = "display:none"
            lnkbtnPopUpInfoKendaraan.Visible = False
        End If
    End Sub

    Private Sub LoadPQRHeaderInfo(oPQRHeader As PQRHeaderBB)
        txtPQRNo.Text = oPQRHeader.PQRNo
        txtNoChasis.Text = oPQRHeader.ChassisMasterBB.ChassisNumber
        hdntxtNoChasis.Value = txtNoChasis.Text
        lnkbtnCheckChassisClick()
        txtOdometer.Text = CInt(oPQRHeader.OdoMeter)
        txtGejala.Text = oPQRHeader.Symptomps
        txtPemeriksaan.Text = oPQRHeader.Causes
        txtHasil.Text = oPQRHeader.Results

        If oPQRHeader.CodeA = "" Then
            ddlKodeWSCA.SelectedIndex = 0
        Else
            ddlKodeWSCA.SelectedValue = oPQRHeader.CodeA
        End If
        If oPQRHeader.CodeB = "" Then
            ddlKodeWSCB.SelectedIndex = 0
        Else
            ddlKodeWSCB.SelectedValue = oPQRHeader.CodeB
        End If
        If oPQRHeader.CodeC = "" Then
            ddlKodeWSCC.SelectedIndex = 0
        Else
            ddlKodeWSCC.SelectedValue = oPQRHeader.CodeC
        End If

        Dim _arrONGKOSKERJA As ArrayList = New ArrayList
        For Each _objPQRDamageCode As PQRDamageCodeBB In oPQRHeader.PQRDamageCodeBBs
            Dim objOngkosKeja As WSCDetailBB = New WSCDetailBB
            objOngkosKeja.WSCType = "L"
            objOngkosKeja.PositionCode = _objPQRDamageCode.DeskripsiKodePosisi.KodePosition
            _arrONGKOSKERJA.Add(objOngkosKeja)
        Next
        sessHelper.SetSession("NEW_ONGKOSKERJABB", _arrONGKOSKERJA)

        Dim _arrONGKOSPARTS As ArrayList = New ArrayList
        For Each _objPQRPartsCode As PQRPartsCodeBB In oPQRHeader.PQRPartsCodeBBs
            Dim objOngkosPart As WSCDetailBB = New WSCDetailBB
            objOngkosPart.WSCType = "P"
            objOngkosPart.SparePartMaster = New SparePartMasterFacade(User).Retrieve(_objPQRPartsCode.SparePartMaster.PartNumber)
            _arrONGKOSPARTS.Add(objOngkosPart)
        Next
        sessHelper.SetSession("NEW_ONGKOSPARTSBB", _arrONGKOSPARTS)
        RefreshGrid()
        setEnabledReferensiDokumen(True)
    End Sub


#Region "Send Email"
    Private Function MailReport(ByVal vCode As String, ByVal _arl As ArrayList) As Boolean
        If _arl.Count <= 0 Then Return False

        Dim oLaborMaster As LaborMaster
        Dim sTO As String = KTB.DNet.Lib.WebConfig.GetString("EmailToWSCRequestNewLaborMaster")
        Dim sCC As String = KTB.DNet.Lib.WebConfig.GetString("EmailCcWSCRequestNewLaborMaster")
        Dim subject As String = "Alert: Request New Labor Master"


        Dim Dir As String = Server.MapPath(TEMP_EMAIL_NOLABORMASTER)
        Try
            Dim objDealer As Dealer = Me.sessHelper.GetSession("DEALER")
            Dim str = New StringBuilder()
            For Each oWSCD As WSCDetailBB In _arl
                str.Append("<tr>" & _
                        "<td><center>" & vCode & "</center></td>" & _
                        "<td><center>" & oWSCD.PositionCode.ToString & "</center></td>" & _
                        "<td><center>" & oWSCD.WorkCode.ToString & "</center></td>" & _
                        "</tr>")
            Next
            Dim sContents() As String = {str.ToString()}

            Me.SendEmail(Dir, sTO, sCC, subject, sContents)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub SendEmail(ByVal EmailFile As String, ByVal sTo As String, ByVal sCC As String, ByVal sSubject As String, ByVal sMessage() As String)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetString("SMTPWSC")
        Dim ObjEmail As DNetMail = New DNetMail(smtp)
        Dim emailAdmin As String = KTB.DNet.Lib.WebConfig.GetString("EmailToWSCRequestNewLaborMaster")
        Dim emailFrom As String = KTB.DNet.Lib.WebConfig.GetString("EmailFromWSCRequestNewLaborMaster")
        Dim sr As System.IO.StreamReader = New System.IO.StreamReader(EmailFile)
        Dim szEmailFormat As String = sr.ReadToEnd()
        sr.Close()
        Dim szEmailContent As String = String.Format(szEmailFormat, sMessage)
        If Not IsNothing(sCC) AndAlso sCC.Trim() <> "" AndAlso sCC.EndsWith(";") = False AndAlso Not IsNothing(emailAdmin) AndAlso emailAdmin <> "" Then
            emailAdmin = ";" & emailAdmin
        End If


        ObjEmail.sendMail(sTo, sCC, emailAdmin, emailFrom, sSubject, MailFormat.Html, szEmailContent)
    End Sub

    Private Function LoadPositionAndWorkCodes(ByVal chassisNumber As String, ByVal recallRegNo As String) As Integer
        Dim strSql As String
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        Dim _arrONGKOSKERJA As ArrayList = New ArrayList
        'Dim _arrONGKOSKERJA As ArrayList = CType(sessHelper.GetSession("NEW_ONGKOSKERJABB"), ArrayList)
        'If Mode = enumMode.Mode.EditMode Then
        '    _arrONGKOSKERJA = CType(sessHelper.GetSession("ONGKOSKERJABB"), ArrayList)
        'End If

        strSql += "select lm.LaborCode as PositionCode, lm.WorkCode as WorkCode from ChassisMasterBB cm"
        strSql += " Join VechileColor cv on cm.VechileColorID = cv.ID"
        strSql += " Join VechileType ct on cv.VechileTypeID = ct.ID"
        strSql += " Join LaborMaster lm on lm.VechileTypeID = ct.ID"
        strSql += " Join RecallCategoryDetail rcd on rcd.LaborMasterID = lm.id"
        strSql += " Join RecallCategory rc on rc.ID = rcd.RecallCategoryID"
        strSql += " where cm.ChassisNumber = '" + chassisNumber + "' and rc.RecallRegNo = '" + recallRegNo + "'"

        Dim CariPosisiAmaWorkCode As DataSet = New RecallCategoryDetailFacade(User).DoRetrieveDataset(strSql)
        Dim objWSCDetailBB As WSCDetailBB

        For Each dr As DataRow In CariPosisiAmaWorkCode.Tables(0).Rows
            objWSCDetailBB = New WSCDetailBB()
            objWSCDetailBB.PositionCode = CType(dr("PositionCode"), String)
            objWSCDetailBB.WorkCode = CType(dr("WorkCode"), String)
            _arrONGKOSKERJA.Add(objWSCDetailBB)
            If Mode = enumMode.Mode.NewItemMode Then
                sessHelper.SetSession("NEW_ONGKOSKERJABB", _arrONGKOSKERJA)
            Else
                sessHelper.SetSession("ONGKOSKERJABB", _arrONGKOSKERJA)
            End If
        Next
        If CariPosisiAmaWorkCode.Tables(0).Rows.Count < 1 Then
            Return 0
        Else
            Return CariPosisiAmaWorkCode.Tables(0).Rows.Count
        End If
    End Function
#End Region

    Private Function GetCheckedWSCItem() As ArrayList
        Dim ListOpenWSC As ArrayList = New ArrayList
        Dim _arrONGKOSKERJA As ArrayList = CType(sessHelper.GetSession("ONGKOSKERJABB"), ArrayList)
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        If Mode = enumMode.Mode.NewItemMode Then
            _arrONGKOSKERJA = CType(sessHelper.GetSession("NEW_ONGKOSKERJABB"), ArrayList)
        End If
        Dim intJmlMasterValid As Short = 0
        For Each _objWSCDetailBB As WSCDetailBB In _arrONGKOSKERJA
            GetObjectLaborMaster(_objWSCDetailBB.PositionCode.Trim(), _objWSCDetailBB.WorkCode.Trim())
            If Not IsNothing(objLaborMaster) Then
                intJmlMasterValid += 1
            End If
        Next
        If intJmlMasterValid <> _arrONGKOSKERJA.Count Then
            MessageBox.Show("Seluruh data master labor harus valid pada item grid Ongkos Kerja")
            Return ListOpenWSC
        End If

        oWSCHeaderBB = CType(sessHelper.GetSession("WSCHEADERBB"), WSCHeaderBB)
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCHeaderBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(WSCHeaderBB), "ID", MatchType.Exact, oWSCHeaderBB.ID))

        Dim objWSCHeaderBBFacade As WSCHeaderBBFacade
        objWSCHeaderBBFacade = New WSCHeaderBBFacade(User)

        Dim sorts As SortCollection = New SortCollection
        sorts.Add(New Sort(GetType(WSCHeaderBB), "Dealer.DealerCode"))

        If LoopDgrid("ALLGRID", Nothing, Nothing, "Simpan", 0) Then
            ListOpenWSC = objWSCHeaderBBFacade.RetrieveByCriteria(criterias, sorts)
        End If

        Return ListOpenWSC
    End Function

    Private Function btnRilis_Click() As Boolean
        Dim bcheck As Boolean = False
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer) 'Just TEST
        'Dim imp As SAPImpersonate = New SAPImpersonate("sap", "7Karakter", "172.17.31.21")
        'Dim imp As SAPImpersonate = New SAPImpersonate("sap", "7Karakter", "172.17.104.68")

        Dim success As Boolean = False
        Dim sTimestamp As String = sSuffix()
        Dim WSCFileNameSAP As String = KTB.DNet.Lib.WebConfig.GetValue("SAPSERVERFOLDER") & "\Service\WSC\MMC\" & "WSCData" & sSuffix() & ".wsd"
        Dim WSCFileNameLocal As String = Server.MapPath("") & "\..\DataTemp\WSCData" & sSuffix() & ".txt"

        If lblClaimNumber.Text.Trim <> "[Automatically by System]" Then
            bcheck = True
        End If

        Try
            success = imp.Start
            'test
            success = True
            If bcheck And success Then
                Dim CheckedWSCItemColl As ArrayList = New ArrayList
                Dim arlTransferedToSAP As New ArrayList
                CheckedWSCItemColl = GetCheckedWSCItem()
                If CheckedWSCItemColl.Count = 0 Then Return False

                Dim nSavedData As Integer = AppendText(CheckedWSCItemColl, WSCFileNameLocal, WSCFileNameSAP, arlTransferedToSAP)
                If nSavedData < 1 Then
                    Dim sIndicator As String = ""
                    sIndicator = IIf(nSavedData = -1, ".", IIf(nSavedData = -1, ",", ""))
                    MessageBox.Show("Rilis data gagal" & sIndicator)
                    Return False
                End If

                'sekarang updatenya
                Dim objWSCHeaderBBColl As ArrayList = New ArrayList
                If arlTransferedToSAP.Count > 0 Then
                    For Each ObjWsCHeader As WSCHeaderBB In CheckedWSCItemColl
                        ObjWsCHeader.Status = CType(enumStatusWSC.Status.Proses, String)
                        ObjWsCHeader.ReleaseDate = DateTime.Now
                        objWSCHeaderBBColl.Add(ObjWsCHeader)
                    Next
                    Dim nResult = New WSCHeaderBBFacade(User).UpdateWSCUploadedToSAP(objWSCHeaderBBColl)
                    If nResult = 0 Then
                        btnRilis.Enabled = False
                        btnSimpan.Enabled = False
                        MessageBox.Show("Update Rilis Sukses")
                        Return True
                    Else
                        MessageBox.Show("Update Rilis gagal")
                        Return False
                    End If
                End If
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            MessageBox.Show("Update Rilis gagal !")
            Return False
        End Try
    End Function

    Private Sub btnRilis_Click(sender As Object, e As EventArgs) Handles btnRilis.Click
        If PQRStatusBeforRelease() Then
            If btnRilis_Click() Then
                btnBatal_Click()
            End If
        End If
    End Sub

    Private Function sSuffix() As String
        If Not IsNothing(ViewState(V_Suffix)) Then
            Return CType(ViewState(V_Suffix), String)
        Else
            Return DateTime.Now.ToString("yyyyMMddHHmmss")
        End If
    End Function

    Private Sub checkFileExistenceToDownload()
        Dim finfo As FileInfo = New FileInfo(Server.MapPath("") & "\..\DataTemp\WSCData" & sSuffix() & ".txt")
        If finfo.Exists Then
            finfo.Delete()
        End If
    End Sub

    Private Function AppendText(ByVal ArrCheckedWSCItem As ArrayList, ByVal FileNameLocal As String, ByVal filename As String, ByRef arlTransferedToSAP As ArrayList) As Integer ' Number of data sent to SAP
        Dim strText As New StringBuilder
        Dim objAl As New ArrayList
        Dim nData As Integer = 0
        Dim delimiter As String = ""","""
        Dim sMessage As String = ""

        Try
            nData = 0
            If ArrCheckedWSCItem.Count > 0 Then
                strText = New StringBuilder
                For Each wscHdr As WSCHeaderBB In ArrCheckedWSCItem
                    For Each wscDetail As WSCDetailBB In wscHdr.wSCDetailBBs
                        strText.Append("""")
                        strText.Append(wscHdr.ClaimType)
                        strText.Append(delimiter)
                        strText.Append(wscHdr.Dealer.DealerCode)
                        strText.Append(delimiter)
                        strText.Append(wscHdr.ClaimNumber)
                        strText.Append(delimiter)
                        strText.Append(wscHdr.RefClaimNumber)
                        strText.Append(delimiter)
                        strText.Append(wscHdr.ChassisMasterBB.ChassisNumber)
                        strText.Append(delimiter)
                        'strText.Append(wscHdr.ServiceDate.Day.ToString.PadLeft(2, "0") & _
                        '                        wscHdr.ServiceDate.Month.ToString.PadLeft(2, "0") & _
                        '                        wscHdr.ServiceDate.Year.ToString.Substring(2, 2))
                        strText.Append(DateTime.Now.Day.ToString.PadLeft(2, "0") & _
                                                DateTime.Now.Month.ToString.PadLeft(2, "0") & _
                                                DateTime.Now.Year.ToString.Substring(2, 2))
                        strText.Append(delimiter)
                        strText.Append(wscHdr.Miliage.ToString)
                        strText.Append(delimiter)
                        strText.Append(wscHdr.PQR)
                        strText.Append(delimiter)
                        strText.Append(wscHdr.PQRStatus)
                        strText.Append(delimiter)
                        strText.Append(wscHdr.CodeA)
                        strText.Append(delimiter)
                        strText.Append(wscHdr.CodeB)
                        strText.Append(delimiter)
                        strText.Append(wscHdr.CodeC)
                        strText.Append(delimiter)
                        strText.Append(wscHdr.Description)
                        strText.Append(delimiter)
                        strText.Append(wscHdr.EvidencePhoto)
                        strText.Append(delimiter)
                        strText.Append(wscHdr.EvidenceInvoice)
                        strText.Append(delimiter)
                        strText.Append(wscHdr.EvidenceDmgPart)
                        strText.Append(delimiter)
                        strText.Append(wscDetail.WSCType)
                        strText.Append(delimiter)
                        If wscDetail.WSCType = "L" Then
                            If Not IsNothing(wscDetail.LaborMaster) Then
                                strText.Append(wscDetail.LaborMaster.LaborCode)
                                strText.Append(delimiter)
                                strText.Append(wscDetail.LaborMaster.WorkCode)
                                strText.Append(delimiter)
                            Else
                                sMessage &= " Data Labor untuk No. Klaim " & wscHdr.ClaimNumber & " tidak ada."
                            End If


                            strText.Append(wscDetail.Quantity.ToString.Replace( _
                                System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator, ","))
                            'masih nunggu sap
                            strText.Append(delimiter)
                            strText.Append(String.Format("{0:#}", wscDetail.PartPrice))
                            strText.Append(delimiter)
                            strText.Append("")

                        ElseIf wscDetail.WSCType = "P" Then
                            If Not IsNothing(wscDetail.SparePartMaster) Then
                                strText.Append(wscDetail.SparePartMaster.PartNumber)
                                strText.Append(delimiter)
                            Else
                                sMessage &= " Data Spare Part untuk No. Klaim " & wscHdr.ClaimNumber & " tidak ada."
                            End If

                            strText.Append(wscDetail.Quantity.ToString.Replace( _
                                System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator, ","))
                            strText.Append(delimiter)
                            strText.Append(String.Format("{0:#}", wscDetail.PartPrice))
                            'strText.Append(delimiter)
                            'strText.Append("")
                            'masih nunggu sap
                            strText.Append(delimiter)
                            If wscDetail.MainPart = 1 Then
                                strText.Append("X")
                            Else
                                strText.Append("")
                            End If
                        End If
                        strText.Append(delimiter)
                        strText.Append(wscDetail.WSCHeaderBB.EvidenceRepair)
                        strText.Append(delimiter)
                        strText.Append(wscDetail.WSCHeaderBB.EvidenceWSCLetter)
                        strText.Append(delimiter)
                        strText.Append(wscDetail.WSCHeaderBB.EvidenceWSCTechnical)
                        strText.Append(delimiter)
                        strText.Append(wscDetail.WSCHeaderBB.FailureDate.ToString("ddMMyyyy"))

                        'Dim strKWITANSI_WSC As String = String.Empty
                        'Dim strSURAT_WSC As String = String.Empty
                        'Dim strTEKNIKAL_WSC As String = String.Empty
                        'Dim strREPAIR_WO As String = String.Empty
                        'Dim strPART_BEKAS As String = String.Empty
                        'Dim strPHOTO As String = String.Empty
                        'Dim criterias As New CriteriaComposite(New Criteria(GetType(WSCEvidenceBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        'criterias.opAnd(New Criteria(GetType(WSCEvidenceBB), "WSCHeaderBB.ID", MatchType.Exact, wscHdr.ID))
                        'Dim WSCEvidenceBBList As ArrayList = New WSCEvidenceBBFacade(User).Retrieve(criterias)
                        'If WSCEvidenceBBList.Count > 0 Then
                        '    Dim _evidence As New WSCEvidenceBB
                        '
                        '    For Each _evidence In WSCEvidenceBBList
                        '        Select Case _evidence.EvidenceType
                        '            Case EnumWSCEvidenceType.WSCEvidenceType.KWITANSI_WSC
                        '                strKWITANSI_WSC = "X"
                        '            Case EnumWSCEvidenceType.WSCEvidenceType.SURAT_WSC
                        '                strSURAT_WSC = "X"
                        '            Case EnumWSCEvidenceType.WSCEvidenceType.TEKNIKAL_WSC
                        '                strTEKNIKAL_WSC = "X"
                        '            Case EnumWSCEvidenceType.WSCEvidenceType.REPAIR_WO
                        '                strREPAIR_WO = "X"
                        '            Case EnumWSCEvidenceType.WSCEvidenceType.PART_BEKAS
                        '                strPART_BEKAS = "X"
                        '            Case EnumWSCEvidenceType.WSCEvidenceType.PHOTO
                        '                strPHOTO = "X"
                        '            Case Else
                        '                strKWITANSI_WSC = ""
                        '        End Select
                        '    Next
                        'End If
                        '
                        'strText.Append(strREPAIR_WO)
                        '
                        'strText.Append(delimiter)
                        'strText.Append(strSURAT_WSC)
                        '
                        'strText.Append(delimiter)
                        'strText.Append(strTEKNIKAL_WSC)
                        '
                        'strText.Append(delimiter)
                        '
                        '
                        'strText.Append(wscHdr.FailureDate.Day.ToString.PadLeft(2, "0") & _
                        '                        wscHdr.FailureDate.Month.ToString.PadLeft(2, "0") & _
                        '                        wscHdr.FailureDate.Year.ToString.Substring(2, 2))
                        'strText.Append(delimiter)

                        strText.Append("""")
                        strText.Append(vbNewLine)

                        arlTransferedToSAP.Add(wscDetail)
                        nData += 1
                    Next
                Next

                If nData > 0 Then
                    If Not Me.SaveToSAP(FileNameLocal, filename, strText) Then
                        nData = -2
                    End If
                End If
            End If
        Catch ex As Exception
            nData = -1 ' -1 means error occured
        End Try

        Return nData
    End Function

    Private Function SaveToSAP(ByVal DestFileLocal As String, ByVal DestFile As String, ByRef sb As StringBuilder) As Boolean
        Dim success As Boolean = False
        Dim sw As StreamWriter
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim fInfoLocal As New FileInfo(DestFile)
        Dim finfo As New FileInfo(DestFile)
        Try
            success = imp.Start()
            If success Then
                'Local
                If Not fInfoLocal.Directory.Exists Then Directory.CreateDirectory(fInfoLocal.DirectoryName)
                If fInfoLocal.Exists() Then fInfoLocal.Delete()
                Dim fs As FileStream = New FileStream(DestFileLocal, FileMode.CreateNew)
                sw = New StreamWriter(fs)
                sw.Write(sb.ToString)
                sw.Close()
                fs.Close()

                'Server
                If Not finfo.Directory.Exists Then
                    Directory.CreateDirectory(finfo.DirectoryName)
                End If
                If finfo.Exists Then
                    finfo.Delete()
                End If
                System.IO.File.Copy(DestFileLocal, DestFile)
                imp.StopImpersonate()
                imp = Nothing
                'System.IO.File.Copy(DestFileLocal, DestFile & ".wts")
            End If
        Catch ex As Exception
            success = False
            sw.Close()
        End Try
        Return success
    End Function

    Private Sub btnPrintBarcode_Click(sender As Object, e As EventArgs) Handles btnPrintBarcode.Click
        Dim strIdTmp As String = String.Empty
        Dim arlPrintBarcode As New ArrayList
        Dim facade As WSCDetailBBFacade = New WSCDetailBBFacade(User)
        Dim countPartItem As Integer = 0
        Dim countChecklist As Integer = 0
        Dim i As Integer = 0
        For Each item As DataGridItem In dgParts.Items
            Dim chk As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            If (chk.Checked) Then
                Dim obj As WSCDetailBB = facade.Retrieve(Convert.ToInt32(dgParts.DataKeys().Item(i)))
                If obj.WSCType.Trim.ToUpper = "P" Then
                    arlPrintBarcode.Add(obj)
                    If strIdTmp.Trim = "" Then
                        strIdTmp += obj.ID.ToString
                    Else
                        strIdTmp += ";" & obj.ID.ToString
                    End If
                    countPartItem += 1
                End If
                countChecklist += 1
            End If
            i += 1
        Next
        If countChecklist <= 0 Then
            MessageBox.Show("Print Barcode gagal, Tidak ada data yang di pilih")
            Return
        End If
        If countPartItem <= 0 Then
            MessageBox.Show("Print Barcode gagal, Tidak ada data dengan tipe part")
            Return
        End If

        Server.Transfer("~/Service/FrmWSCHeaderPrintBarcodeBB.aspx?claimNumber=" & lblClaimNumber.Text & "&id=" & strIdTmp & "&viewStateMode=" & intViewStateMode & "&screenFrom=" & screenFrom & "&PQRId=" & pqrId & "&WSCId=" & wscId)
    End Sub

#Region "Parameter"

    Private Function ValidateByParameter()
        Dim paramCont As ArrayList = New ArrayList
        Dim errorIn As String = String.Empty
        Dim RejectCode As String = String.Empty
        'Dim index As Integer = 0
        Dim RCode As String = String.Empty
        Dim typeCode As String = String.Empty

        '1. Ambil Vtype ID
        Dim criteVType As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteVType.opAnd(New Criteria(GetType(ChassisMasterBB), "ChassisNumber", MatchType.Exact, txtNoChasis.Text))
        Dim oChaMasFas As ChassisMasterBB = New ChassisMasterBBFacade(User).Retrieve(criteVType)(0)
        Dim VechileTypeID As Integer
        If IsNothing(oChaMasFas.VechileColor) Then
            VechileTypeID = -1
        Else
            VechileTypeID = oChaMasFas.VechileColor.VechileType.ID
            typeCode = oChaMasFas.VechileColor.VechileType.VechileTypeCode
        End If
        Dim EndCustomerID As Integer
        If IsNothing(oChaMasFas.EndCustomer) Then
            EndCustomerID = -1
        Else
            EndCustomerID = oChaMasFas.EndCustomer.ID
        End If
        Dim ClaimType As String = ddlClaimType.SelectedValue
        Dim RefDoc As String = txtPQRNo.Text
        'Dim PKTDate As String = lblTglPKTVal.Text
        Dim DmgDate As String = icTglKerusakan.Value
        Dim RepDate As String = icTglPerbaikan.Value
        Dim Odo As Integer = CType(txtOdometer.Text, Integer)
        Dim detailIndex As Integer = 0
        Dim PositionCode As ArrayList
        If Mode = enumMode.Mode.NewItemMode Then
            'PositionCode = sessHelper.GetSession("NEW_ONGKOSKERJABB")
            PositionCode = GetSession("NEW_ONGKOSKERJABB")
        Else
            'PositionCode = sessHelper.GetSession("ONGKOSKERJABB")
            PositionCode = GetSession("ONGKOSKERJABB")
        End If
        Dim WorkCode As ArrayList
        If Mode = enumMode.Mode.NewItemMode Then
            'WorkCode = sessHelper.GetSession("NEW_ONGKOSPARTSBB")
            WorkCode = GetSession("NEW_ONGKOSPARTSBB")
        Else
            'WorkCode = sessHelper.GetSession("ONGKOSPARTSBB")
            WorkCode = GetSession("ONGKOSPARTSBB")
        End If
        Dim DmgCodeA As String = ddlKodeWSCA.SelectedValue
        Dim DmgCodeB As String = ddlKodeWSCB.SelectedValue
        Dim DmgCodeC As String = ddlKodeWSCC.SelectedValue

        Dim countKODEPOSISI As Integer = 0
        Dim countPART As Integer = 0
        Dim countAMOUNT As Integer = 0

        Dim isKODEPOSISI As Boolean = False
        Dim isPART As Boolean = False
        Dim isAMOUNT As Boolean = False

        Dim isPARTBlank As Boolean = False
        Dim isPARTFirstRow As Boolean = False

        Dim countAMOUNTrue As Integer = 0
        Dim isAMOUNTAllRow As Boolean = False


        '2. Ambil Header ID
        Dim criteVehicle As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCParameterVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteVehicle.opAnd(New Criteria(GetType(WSCParameterVehicle), "VechileType.ID", MatchType.Exact, VechileTypeID))
        criteVehicle.opAnd(New Criteria(GetType(WSCParameterVehicle), "WSCParameterHeader.Status", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteVehicle.opAnd(New Criteria(GetType(WSCParameterVehicle), "WSCParameterHeader.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteVehicle.opAnd(New Criteria(GetType(WSCParameterVehicle), "WSCParameterHeader.ClaimType", MatchType.Exact, "Z6"))

        Dim oWSCParamVe As WSCParameterVehicleFacade = New WSCParameterVehicleFacade(User)
        Dim arrParamHead As ArrayList = oWSCParamVe.Retrieve(criteVehicle)
        If arrParamHead.Count <= 0 Then
            MessageBox.Show("Tipe kendaraan " & typeCode & " belum terdaftar pada parameter. silahkan menghubungi MMSKI")
            Return False
        End If
        For Each oParamHead As WSCParameterVehicle In arrParamHead

            '3. ambil Detail
            Dim criteDetail As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCParameterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteDetail.opAnd(New Criteria(GetType(WSCParameterDetail), "WSCParameterHeader.ID", MatchType.Exact, oParamHead.WSCParameterHeader.ID))
            Dim oWSCParamDetail As WSCParameterDetailFacade = New WSCParameterDetailFacade(User)
            Dim arrParamDetail As ArrayList = oWSCParamDetail.Retrieve(criteDetail)
            Dim index As Integer = 0
            For Each oParamDetail As WSCParameterDetail In arrParamDetail
                Dim Result As Boolean = False
                Dim Kinds As String
                Dim vals As Object
                Select Case oParamDetail.Kind
                    Case 0 'Buletin Number
                        Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, RefDoc)
                        Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamDetail.Kind)
                        vals = RefDoc
                    Case 1 'PKT Date
                        'If blnIsSoldDealer Then
                        '    If PKTDate.Trim.Length = 0 Then
                        '        Dim oEndCust As EndCustomer = New EndCustomerFacade(User).Retrieve(EndCustomerID)
                        '        If oEndCust.FakturDate = Date.MinValue Then
                        '            Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, oEndCust.OpenFakturDate)
                        '            Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamDetail.Kind)
                        '            vals = oEndCust.OpenFakturDate
                        '        Else
                        '            Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, oEndCust.FakturDate)
                        '            Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamDetail.Kind)
                        '            vals = oEndCust.FakturDate
                        '        End If
                        '    Else
                        '        Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, PKTDate)
                        '        Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamDetail.Kind)
                        '        vals = PKTDate
                        '    End If
                        'End If
                    Case 2 'Dmg Date
                        Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, DmgDate)
                        Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamDetail.Kind)
                        vals = DmgDate
                    Case 3 'Repair Date
                        Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, RepDate)
                        Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamDetail.Kind)
                        vals = RepDate
                    Case 4 'Odo
                        Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, Odo)
                        Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamDetail.Kind)
                        vals = Odo
                    Case 5 'Position Codes
                        For Each item As WSCDetailBB In PositionCode
                            Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, item.PositionCode)
                            Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamDetail.Kind)
                            vals = item.PositionCode
                        Next


                        If countKODEPOSISI > 0 Then GoTo jump

                        countKODEPOSISI = countKODEPOSISI + 1

                        For Each item As DataGridItem In dgOngkosKerja.Items

                            Dim txtPostionCodeItem As TextBox = item.FindControl("txtPostionCodeItem")

                            Dim params As String() = oParamDetail.Value.Split(";")

                            If oParamDetail.Operators = 9 Then ' Terdiri dari
                                For Each row As String In params

                                    If txtPostionCodeItem.Text.ToUpper() = row.ToUpper() Then
                                        isKODEPOSISI = True
                                    Else
                                        isKODEPOSISI = False
                                    End If

                                Next
                            ElseIf oParamDetail.Operators = 10 Then ' Tidak terdiri dari
                                For Each row As String In params

                                    If txtPostionCodeItem.Text.ToUpper() <> row.ToUpper() Then
                                        isKODEPOSISI = True
                                    Else
                                        isKODEPOSISI = False
                                    End If

                                Next
                            End If

                        Next


                    Case 6 'PKT date to Repair date length
                        'If PKTDate.Trim.Length = 0 Then
                        '    Dim oEndCust As EndCustomer = New EndCustomerFacade(User).Retrieve(EndCustomerID)
                        '    If oEndCust.FakturDate = Date.MinValue Then
                        '        Dim ValDate As Integer = (oEndCust.OpenFakturDate - CType(RepDate, Date)).Days
                        '        If ValDate < 0 Then
                        '            ValDate = ValDate * -1
                        '        End If
                        '        Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, ValDate)
                        '        Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamDetail.Kind)
                        '        vals = ValDate
                        '    Else
                        '        Dim ValDate As Integer = (oEndCust.FakturDate - CType(RepDate, Date)).Days
                        '        If ValDate < 0 Then
                        '            ValDate = ValDate * -1
                        '        End If
                        '        Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, ValDate)
                        '        Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamDetail.Kind)
                        '        vals = ValDate
                        '    End If
                        'Else
                        '    Dim ValDate As Integer = (CType(PKTDate, Date) - CType(RepDate, Date)).Days
                        '    If ValDate < 0 Then
                        '        ValDate = ValDate * -1
                        '    End If
                        '    Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, ValDate)
                        '    Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamDetail.Kind)
                        '    vals = ValDate
                        'End If
                    Case 7 'Repair date to Input Claim date length
                        Dim ValDate As Integer = (CType(RepDate, Date) - DateTime.Now).Days
                        If Mode = enumMode.Mode.EditMode Then
                            ValDate = (CType(RepDate, Date) - CType(lblTglKirimVal.Text, Date)).Days
                        End If
                        If ValDate < 0 Then
                            ValDate = ValDate * -1
                        End If
                        Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, ValDate)
                        Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamDetail.Kind)
                        vals = ValDate
                    Case 8 'Dmg Date to Repair date length
                        Dim ValDate As Integer = (CType(DmgDate, Date) - CType(RepDate, Date)).Days
                        If ValDate < 0 Then
                            ValDate = ValDate * -1
                        End If
                        Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, ValDate)
                        Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamDetail.Kind)
                        vals = ValDate
                    Case 9 'Work Codes
                        For Each item As WSCDetailBB In WorkCode
                            Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, item.WorkCode)
                            Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamDetail.Kind)
                            vals = item.WorkCode
                        Next
                    Case 10 'Dmg Code A
                        Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, DmgCodeA)
                        Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamDetail.Kind)
                        vals = DmgCodeA
                    Case 11 'Dmg Code B
                        Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, DmgCodeB)
                        Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamDetail.Kind)
                        vals = DmgCodeB
                    Case 12 'Dmg Code C
                        Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, DmgCodeC)
                        Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamDetail.Kind)
                        vals = DmgCodeC

                    Case 15 'Part

                        If countPART > 0 Then GoTo jump

                        countPART = countPART + 1

                        For Each item As DataGridItem In dgParts.Items

                            Dim lblKodeParts As Label = item.FindControl("lblKodeParts")

                            Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, lblKodeParts.Text)
                            Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamDetail.Kind)
                            vals = lblKodeParts.Text

                            Dim params As String() = oParamDetail.Value.Split(";")

                            If oParamDetail.Operators = 9 Then ' Terdiri dari
                                For Each row As String In params

                                    If lblKodeParts.Text.ToUpper() = row.ToUpper() Then
                                        isPART = True

                                    Else
                                        isPART = False
                                    End If

                                Next
                            ElseIf oParamDetail.Operators = 10 Then ' Tidak terdiri dari
                                For Each row As String In params

                                    If lblKodeParts.Text.ToUpper() <> row.ToUpper() Then
                                        isPART = True
                                    Else
                                        isPART = False
                                    End If

                                Next
                            End If

                        Next

                        If isKODEPOSISI And dgParts.Items.Count = 0 Then
                            isPARTBlank = True
                        End If

                        If isKODEPOSISI And dgParts.Items.Count = 1 And isPART = False Then
                            isPARTFirstRow = True
                        End If


                    Case 16 'Amount

                        If countAMOUNT > 0 Then GoTo jump

                        countAMOUNT = countAMOUNT + 1

                        For Each item As DataGridItem In dgParts.Items

                            Dim txtPartPriceItem As TextBox = item.FindControl("txtPartPriceItem")

                            Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, txtPartPriceItem.Text)
                            Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamDetail.Kind)
                            vals = txtPartPriceItem.Text

                            If oParamDetail.Operators = 0 Then ' =
                                If Convert.ToDecimal(txtPartPriceItem.Text) = Convert.ToDecimal(oParamDetail.Value) Then
                                    isAMOUNT = True

                                    countAMOUNTrue = countAMOUNTrue + 1
                                Else
                                    isAMOUNT = False
                                End If
                            ElseIf oParamDetail.Operators = 1 Then ' <>
                                If Convert.ToDecimal(txtPartPriceItem.Text) <> Convert.ToDecimal(oParamDetail.Value) Then
                                    isAMOUNT = True

                                    countAMOUNTrue = countAMOUNTrue + 1
                                Else
                                    isAMOUNT = False
                                End If
                            ElseIf oParamDetail.Operators = 5 Then ' >
                                If Convert.ToDecimal(txtPartPriceItem.Text) > Convert.ToDecimal(oParamDetail.Value) Then
                                    isAMOUNT = True

                                    countAMOUNTrue = countAMOUNTrue + 1
                                Else
                                    isAMOUNT = False
                                End If
                            ElseIf oParamDetail.Operators = 6 Then ' <
                                If Convert.ToDecimal(txtPartPriceItem.Text) < Convert.ToDecimal(oParamDetail.Value) Then
                                    isAMOUNT = True

                                    countAMOUNTrue = countAMOUNTrue + 1
                                Else
                                    isAMOUNT = False
                                End If
                            ElseIf oParamDetail.Operators = 7 Then ' >=
                                If Convert.ToDecimal(txtPartPriceItem.Text) >= Convert.ToDecimal(oParamDetail.Value) Then
                                    isAMOUNT = True

                                    countAMOUNTrue = countAMOUNTrue + 1
                                Else
                                    isAMOUNT = False
                                End If
                            ElseIf oParamDetail.Operators = 8 Then ' <=
                                If Convert.ToDecimal(txtPartPriceItem.Text) <= Convert.ToDecimal(oParamDetail.Value) Then
                                    isAMOUNT = True

                                    countAMOUNTrue = countAMOUNTrue + 1
                                Else
                                    isAMOUNT = False
                                End If
                            End If

                        Next


                        If isKODEPOSISI And dgParts.Items.Count > 1 And countAMOUNTrue > 0 Then
                            isAMOUNTAllRow = True
                        End If


jump:

                End Select

                If index = 0 AndAlso Result = False Then
                    Exit For
                End If

                'If index = 0 Then paramCont = Result
                'If Not RejectCode.Contains(oParamDetail.ReasonCode) And Result = False Then
                '    RejectCode += oParamDetail.ReasonCode & ";"
                'End If

                'If Not paramCont Then
                '    RCode = RejectCode.Replace(";", "\n")
                '    errorIn += Kinds & " Operator " & New EnumWSCParamParameter().RetrieveWSCParamOperator(oParamDetail.Operators).ToLower & "  " & oParamDetail.Value & " dan Page " & vals & ";"
                'Else
                '    paramCont = Result
                'End If

                'index += 1

                If Not RejectCode.Contains(oParamDetail.ReasonCode) And Result = False Then
                    RejectCode += oParamDetail.ReasonCode & ";"
                End If

                If Not Result Then
                    RCode = RejectCode.Replace(";", "\n")
                    errorIn += Kinds & " Operator " & New EnumWSCParamParameter().RetrieveWSCParamOperator(oParamDetail.Operators).ToLower & "  " & oParamDetail.Value & " dan Page " & vals & ";"
                End If

                paramCont.Add(Result)
                index += 1
                detailIndex += 1
            Next
            'If paramCont = False Then
            '    Exit For
            'End If
        Next

        If detailIndex = 0 Then
            MessageBox.Show("Tipe kendaraan " & typeCode & " belum terdaftar pada parameter. \nSilahkan menghubungi MMKSI")
            Return False
        End If

        If paramCont.Contains(False) Then

            'If isPARTBlank = False And
            '   isPARTFirstRow = False And
            '   isAMOUNTAllRow = False And
            '    (isPART = False Or isAMOUNT = False) Then
            SaveToFile(errorIn)
            RejectCode.Replace(";", "\n")
            MessageBox.Show("Pengajuan WSC anda ditolak, \nKode Penolakan : \n" & RejectCode)
            Return False
            'End If

        End If
        'If paramCont Then
        '    Return paramCont
        'End If
        'If arrParamHead.Count = 0 Then
        '    paramCont = True
        'Else
        '    'MessageBox.Show(errorIn)
        '    SaveToFile(errorIn)
        '    RejectCode.Replace(";", "\n")
        '    MessageBox.Show("Pengajuan WSC anda ditolak, \nKode Penolakan : \n" & RejectCode)
        'End If
        'Return paramCont
        Return True
    End Function

    Private Sub SaveToFile(ByVal Errors As String)
        Dim filename = String.Format("{0}", "ParameterError.txt")
        Dim LocalDest As String = Server.MapPath("") & "\..\DataTemp\" & filename

        Dim sw As StreamWriter

        Try
            Dim fs As FileStream = New FileStream(LocalDest, FileMode.Append)
            sw = New StreamWriter(fs)
            Dim erorr As String() = Errors.Split(";")
            For Each item As String In erorr
                sw.Write(DateTime.Now & " : ")
                If item.Trim.Length <> 0 Then
                    sw.Write(item)
                Else
                    sw.Write("================================")
                End If
                sw.Write(vbNewLine)
            Next
            sw.Close()
            fs.Close()
        Catch
            sw.Close()
        End Try
    End Sub

    Private Function paramDetailKind(ByVal Kind As Integer, ByVal Operators As Integer, ByVal DBVal As String, ByVal Value1 As Object)
        Select Case Operators
            Case 0 'Equal
                Return FuncEqual(DBVal, Value1)
            Case 1 'NotEqual
                Return FuncNotEqual(DBVal, Value1)
            Case 2 'PartialEqual
                Return FuncPartialEqual(DBVal, Value1)
            Case 3 'StartWith
                Return FuncStartWith(DBVal, Value1)
            Case 4 'EndWith
                Return FuncEndWith(DBVal, Value1)
            Case 5 'GreaterThan
                Return FuncGreaterThan(DBVal, Value1)
            Case 6 'SmallerThan
                Return FuncSmallerThan(DBVal, Value1)
            Case 7 'GreaterOrEqual
                Return FuncGreaterOrEqual(DBVal, Value1)
            Case 8 'SmallerOrEqual
                Return FuncSmallerOrEqual(DBVal, Value1)
            Case 9 'Inset
                Return FuncInset(DBVal, Value1)
            Case 10 'Offset
                Return FuncOffset(DBVal, Value1)
        End Select
    End Function

    Private Function FuncEqual(ByVal db As String, ByVal value As String)
        Dim _ret As Boolean = False
        If value = db Then
            _ret = True
        End If
        Return _ret
    End Function

    Private Function FuncNotEqual(ByVal db As String, ByVal value As String)
        Dim _ret As Boolean = False
        If value <> db Then
            _ret = True
        End If
        Return _ret
    End Function

    Private Function FuncPartialEqual(ByVal db As String, ByVal value As String)
        Dim _ret As Boolean = False
        If value.Contains(db) Then
            _ret = True
        End If
        Return _ret
    End Function

    Private Function FuncStartWith(ByVal db As String, ByVal value As String)
        Dim _ret As Boolean = False
        If value.StartsWith(db) Then
            _ret = True
        End If
        Return _ret
    End Function

    Private Function FuncEndWith(ByVal db As String, ByVal value As String)
        Dim _ret As Boolean = False
        If value.EndsWith(db) Then
            _ret = True
        End If
        Return _ret
    End Function

    Private Function FuncGreaterThan(ByVal db As Integer, ByVal value As Integer)
        Dim _ret As Boolean = False
        If value > db Then
            _ret = True
        End If
        Return _ret
    End Function

    Private Function FuncSmallerThan(ByVal db As Integer, ByVal value As Integer)
        Dim _ret As Boolean = False
        If value < db Then
            _ret = True
        End If
        Return _ret
    End Function

    Private Function FuncGreaterOrEqual(ByVal db As Integer, ByVal value As Integer)
        Dim _ret As Boolean = False
        If value >= db Then
            _ret = True
        End If
        Return _ret
    End Function

    Private Function FuncSmallerOrEqual(ByVal db As Integer, ByVal value As Integer)
        Dim _ret As Boolean = False
        If value <= db Then
            _ret = True
        End If
        Return _ret
    End Function

    Private Function FuncInset(ByVal db As String, ByVal value As String)
        Dim arrDBVal As String() = db.Split(";")
        For Each Val As String In arrDBVal
            If Val = value.Trim Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Function FuncOffset(ByVal db As String, ByVal value As String)
        Dim arrDBVal As String() = db.Split(";")
        For Each Val As String In arrDBVal
            If Val = value Then
                Return False
            End If
        Next
        Return True
    End Function

#End Region

    Private Function ChassisKodePosWorkCodKiloIsExist(ByVal Chassis As String, ByVal PositionCode As String, ByVal WorkCode As String, ByVal Kilo As Integer, ByRef oDetail As WSCDetailBB) As Boolean
        Dim arl As ArrayList
        For i As Integer = 0 To 1
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCDetailBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(WSCDetailBB), "WSCHeaderBB.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

            crit.opAnd(New Criteria(GetType(WSCDetailBB), "WSCType", MatchType.Exact, "L"))
            crit.opAnd(New Criteria(GetType(WSCDetailBB), "WSCHeaderBB.ChassisMasterBB.ChassisNumber", MatchType.Exact, Chassis))
            If i = 0 Then
                crit.opAnd(New Criteria(GetType(WSCDetailBB), "PositionCode", MatchType.Exact, PositionCode))
            Else
                crit.opAnd(New Criteria(GetType(WSCDetailBB), "WSCHeaderBB.Miliage", MatchType.Exact, Kilo))
            End If
            crit.opAnd(New Criteria(GetType(WSCDetailBB), "WorkCode", MatchType.Exact, WorkCode))
            crit.opAnd(New Criteria(GetType(WSCDetailBB), "WSCHeaderBB.ClaimStatus", MatchType.No, "DAPP"))
            'crit.opAnd(New Criteria(GetType(WSCDetailBB), "WSCHeaderBB.ClaimNumber", MatchType.No, lblClaimNumber.Text.Trim))
            arl = New WSCDetailBBFacade(User).Retrieve(crit)
            If arl.Count > 0 Then
                oDetail = CType(arl(0), WSCDetailBB)
                Return True
            End If
            i += 1
        Next
        Return False
    End Function

    Private Function PQRStatusBeforRelease() As Boolean
        If ddlRefDoc.SelectedIndex = 2 Then
            Dim arl As ArrayList
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRHeaderBB), "RowStatus", MatchType.Greater, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(PQRHeaderBB), "PQRNo", MatchType.Exact, txtPQRNo.Text))
            arl = New PQRHeaderBBFacade(User).Retrieve(crit)
            If arl.Count > 0 Then
                Return True
            Else
                MessageBox.Show("Proses Rilis gagal, PQR status belum divalidasi")
                Return False
            End If
        Else
            Return True
        End If
    End Function

    Private Sub UpdateWscDetailLaborID(ByVal ___objWSCDetailBB As WSCDetailBB)
        If Not IsNothing(___objWSCDetailBB.WSCHeaderBB) Then
            Dim vTypeID As Integer = ___objWSCDetailBB.WSCHeaderBB.ChassisMasterBB.VechileColor.VechileType.ID
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LaborMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(LaborMaster), "VechileType.ID", MatchType.Exact, vTypeID))
            crit.opAnd(New Criteria(GetType(LaborMaster), "LaborCode", MatchType.Exact, ___objWSCDetailBB.PositionCode))
            crit.opAnd(New Criteria(GetType(LaborMaster), "WorkCode", MatchType.Exact, ___objWSCDetailBB.WorkCode))
            Dim arlLabMaster As ArrayList = New LaborMasterFacade(User).Retrieve(crit)
            If arlLabMaster.Count > 0 Then
                Dim oLaborMaster As LaborMaster = arlLabMaster(0)
                ___objWSCDetailBB.LaborMaster = oLaborMaster
                Dim _res As Integer = New WSCDetailBBFacade(User).Update(___objWSCDetailBB)
            End If
        End If
    End Sub

    Private Function isLaborNull() As Boolean
        Dim objWscHeaderr As WSCHeader = CType(sessHelper.GetSession("WSCHEADERBB"), WSCHeader)
        For Each item As DataGridItem In dgOngkosKerja.Items
            If item.ItemType = ListItemType.Item OrElse item.ItemType = ListItemType.AlternatingItem Then
                Dim Position As String = String.Empty
                Dim Work As String = String.Empty
                Dim lblPositionCode As Label = CType(item.FindControl("lblPositionCode"), Label)
                Dim txtPositionCodeItem As TextBox = CType(item.FindControl("txtPostionCodeItem"), TextBox)
                Dim lblWorkCode As Label = CType(item.FindControl("lblWorkCode"), Label)
                Dim txtWorkCodeItem As TextBox = CType(item.FindControl("txtWorkCodeItem"), TextBox)

                If lblPositionCode.Text.Trim = "" OrElse lblPositionCode.Text.Trim = String.Empty Then
                    Position = txtPositionCodeItem.Text.Trim
                Else
                    Position = txtPositionCodeItem.Text.Trim
                End If

                If lblWorkCode.Text.Trim = "" OrElse lblWorkCode.Text.Trim = String.Empty Then
                    Work = txtWorkCodeItem.Text.Trim
                Else
                    Work = txtWorkCodeItem.Text.Trim
                End If

                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LaborMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(LaborMaster), "VechileType.ID", MatchType.Exact, objWscHeaderr.ChassisMaster.VechileColor.VechileType.ID))
                crit.opAnd(New Criteria(GetType(LaborMaster), "LaborCode", MatchType.Exact, Position))
                crit.opAnd(New Criteria(GetType(LaborMaster), "WorkCode", MatchType.Exact, Work))
                Dim arl As ArrayList = New LaborMasterFacade(User).Retrieve(crit)
                If arl.Count < 1 Then
                    MessageBox.Show("Kode Posisi " & Position & ", dengan Kode Kerja " & Work & ",\nuntuk Kode Kendaraan " & objWscHeaderr.ChassisMaster.VechileColor.VechileType.VechileTypeCode & ", belum Terdaftar.\nSilahkan Hubungi MMKSI")
                    Return True
                End If
            End If
        Next
        Return False
    End Function

    Protected Sub lblPQRNoVal_Click(sender As Object, e As EventArgs) Handles lblPQRNoVal.Click
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRHeader), "RowStatus", MatchType.No, CType(DBRowStatus.Deleted, Short)))
        crit.opAnd(New Criteria(GetType(PQRHeader), "PQRNo", MatchType.Exact, lblPQRNoVal.Text))
        Dim arl As ArrayList = New PQRHeaderFacade(User).Retrieve(crit)
        If arl.Count > 0 Then
            Dim oPQRHeader As PQRHeader = arl(0)
            sessHelper.SetSession("PQR", oPQRHeader)
            Server.Transfer("~/PQR/FrmPQRHeader.aspx?Mode=View&Src=WSCList&WSCId=" & Request.QueryString("WSCId") & "&State=" & Request.QueryString("VIEWSTATEMODE"))
        Else
            MessageBox.Show("Dokumen tidak valid")
        End If
    End Sub

    Private Function validateKodeKerusakan(ByVal KodePos As String, ByVal value As String, ByVal category As String) As Boolean
        If KodePos.Substring(0, 2).ToUpper <> "XE" Then
            Dim KodePostionWSCFacade As New KodePostionWSCFacade(User)
            Dim objCriteriaA As New CriteriaComposite(New Criteria(GetType(KodePostionWSC), "Code", MatchType.Exact, KodePos.Substring(0, 2)))
            objCriteriaA.opAnd(New Criteria(GetType(KodePostionWSC), "PostionCode", MatchType.Exact, value))
            objCriteriaA.opAnd(New Criteria(GetType(KodePostionWSC), "CategoryCode", MatchType.Exact, category))
            objCriteriaA.opAnd(New Criteria(GetType(KodePostionWSC), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim arlA As ArrayList = KodePostionWSCFacade.Retrieve(objCriteriaA)
            If arlA.Count = 0 Then
                Return False
            End If
        End If
        Return True
    End Function

    Protected Sub lnkBtnCheckWONumber_Click(sender As Object, e As EventArgs) Handles lnkBtnCheckWONumber.Click
        Try
            If Not String.IsNullOrEmpty(txtWONumber.Text.Trim) Then
                ' Get DMSWOWarrantyClaim based on WO Number            
                Dim warrantyCriteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DMSWOWarrantyClaim), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                warrantyCriteria.opAnd(New Criteria(GetType(DMSWOWarrantyClaim), "WorkOrderNumber", MatchType.Exact, txtWONumber.Text))
                warrantyCriteria.opAnd(New Criteria(GetType(DMSWOWarrantyClaim), "isBB", MatchType.Exact, 1))
                Dim warrantyList As ArrayList = New DMSWOWarrantyClaimFacade(User).Retrieve(warrantyCriteria)
                If warrantyList.Count = 0 Then
                    ' DMSWOWarrantyClaim not found
                    MessageBox.Show("Nomor WO Tidak Valid")
                Else

                    ' Check Warranty Claim with DealerCode
                    Dim dmsWOWarrantyClaim As DMSWOWarrantyClaim = warrantyList(0)
                    If dmsWOWarrantyClaim.Dealer.DealerCode <> lblDealerVal.Text.Split("-")(0).Trim Then
                        ' WO Number not valid
                        MessageBox.Show("Nomor WO tidak sesuai dengan Dealer Code")
                        Return
                    End If

                    If String.IsNullOrEmpty(dmsWOWarrantyClaim.ServiceBuletin) Then
                        Dim pqrCriteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRHeaderBB), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        pqrCriteria.opAnd(New Criteria(GetType(PQRHeaderBB), "WorkOrderNumber", MatchType.Exact, txtWONumber.Text))
                        Dim pqrList As ArrayList = New PQRHeaderBBFacade(User).Retrieve(pqrCriteria)
                        If pqrList.Count = 0 Then
                            MessageBox.Show("PQR Special harus dibuat")
                        Else
                            Dim pqrHeader As PQRHeaderBB = pqrList(0)
                            LoadPQRHeaderInfo(pqrHeader)
                            'ddlClaimType.SelectedValue = "Z2"
                            'ddlClaimType.Enabled = False
                            ddlRefDoc.SelectedValue = "1"
                            ddlRefDoc.Enabled = False
                            txtPQRNo_TextChanged(sender, e)
                        End If
                    Else
                        ResetPQRHeaderBBInfo()
                        ddlRefDoc.SelectedValue = "0"
                        ddlRefDoc.Enabled = False
                        txtPQRNo.Text = dmsWOWarrantyClaim.ServiceBuletin
                        txtNoChasis.Text = dmsWOWarrantyClaim.ChassisNumber
                        icTglKerusakan.Value = dmsWOWarrantyClaim.FailureDate
                        icTglPerbaikan.Value = dmsWOWarrantyClaim.ServiceDate
                        txtOdometer.Text = dmsWOWarrantyClaim.Mileage
                        txtGejala.Text = dmsWOWarrantyClaim.Symptoms
                        txtPemeriksaan.Text = dmsWOWarrantyClaim.Causes
                        txtHasil.Text = dmsWOWarrantyClaim.Results
                    End If
                End If
            Else
                ddlClaimType.Enabled = True
                ddlRefDoc.Enabled = True
                ClearForm()
                ResetPQRHeaderBBInfo()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub SetSession(ByVal Name As String, ByVal obj As Object)
        Session(Name) = obj
    End Sub

    Private Function GetSession(ByVal Name As String) As Object
        Return Session(Name)
    End Function

    Private Sub RemoveSession(ByVal Name As String)
        Session.Remove(Name)
    End Sub
End Class