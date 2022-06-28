#Region "Import Library"
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.SparePart
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security

Imports ICSharpCode.SharpZipLib.Core
Imports ICSharpCode.SharpZipLib.Zip

Imports System.Text
Imports System.IO
Imports System.Collections.Generic
Imports System.Collections
Imports System.Web.Mail
Imports System.Linq
Imports System.Guid
#End Region

Public Class FrmWSCHeader

#Region "Private Variable"

    Inherits System.Web.UI.Page

    Private _dtKodeKerusakanStartDate As DateTime = New DateTime(2010, 6, 17, 17, 20, 0)

    'Private sessHelper As New SessionHelper
    Private oDealer As New Dealer
    Private oLoginUser As New UserInfo
    Private oWSCHeader As WSCHeader
    Private oWSCHeaderFacade As New WSCHeaderFacade(User)
    Private oWSCDetailOngkosKerja As New WSCDetail
    Private oWSCDetailOngkosKerjaFacade As New WSCDetailFacade(User)
    Private oWSCDetailParts As New WSCDetail
    Private oWSCDetailPartsFacade As New WSCDetailFacade(User)
    Private oWSCEvidence As New WSCEvidence
    Private oWSCEvidenceFacade As New WSCEvidenceFacade(User)
    Private oChassis As New ChassisMaster
    Private oChassisFacade As New ChassisMasterFacade(User)
    Private oPQRAdditionalInfo As New PQRAdditionalInfo
    Private oPQRAdditionalInfoFacade As New PQRAdditionalInfoFacade(User)
    Private oChassisMasterPKTFacade As New ChassisMasterPKTFacade(User)
    Private oChassisMasterPKT As New ChassisMasterPKT
    Private oPQRHeader As New PQRHeader
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
    Protected WithEvents tblKodeKerusakan As System.Web.UI.HtmlControls.HtmlTable
    Private TempDirectory As String
    Private strSaveSuccess As String
    Private resultSave As Integer
    Dim blnIsSoldDealer As Boolean = False
    Dim blnEnableRilis As Boolean = True
    Dim intViewStateMode As Integer = -1

    Const TEMP_EMAIL_NOLABORMASTER = "../DataFile/EmailTemplate/WSCHeaderNoExistedLaborMaster.htm"

#End Region

#Region "Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents lblDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerSearchTerm1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerNm As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents ddlClaimType As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlRefDoc As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtPQRNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPQRNoVal As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lblClaimNumber As System.Web.UI.WebControls.Label
    Protected WithEvents txtRefClaimNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblRefClaimNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblRefClaimNumberVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoChasis As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoChasisVal As System.Web.UI.WebControls.Label
    Protected WithEvents txtNoChasis As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvChassisNumber As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lnkbtnCheckChassis As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lnkbtnPPCheckChassis As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lnkbtnPopUpInfoKendaraan As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lblAlreadySaled As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoMesin As System.Web.UI.WebControls.Label
    Protected WithEvents lblNoMesinVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaPemilik As System.Web.UI.WebControls.Label
    Protected WithEvents lblNamaPemilikVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglPKT As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglPKTVal As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglKerusakan As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglKerusakanVal As System.Web.UI.WebControls.Label
    Protected WithEvents icTglKerusakan As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblTglPerbaikan As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglPerbaikanVal As System.Web.UI.WebControls.Label
    Protected WithEvents icTglPerbaikan As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblOdometer As System.Web.UI.WebControls.Label
    Protected WithEvents lblOdometerVal As System.Web.UI.WebControls.Label
    Protected WithEvents txtOdometer As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtWONumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblGejala As System.Web.UI.WebControls.Label
    Protected WithEvents txtGejala As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvGejala As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblPemeriksaan As System.Web.UI.WebControls.Label
    Protected WithEvents txtPemeriksaan As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvPemeriksaan As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblHasil As System.Web.UI.WebControls.Label
    Protected WithEvents txtHasil As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlKodeWSCA As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlKodeWSCB As System.Web.UI.WebControls.DropDownList
    Protected WithEvents ddlKodeWSCC As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtNotes As System.Web.UI.WebControls.TextBox
    'Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnSimpan2 As System.Web.UI.WebControls.Button
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents ValidationSummary1 As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents rfvOdometer As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lnkbtnPopUpInfoDokumen As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lnkbtnPopUpRefClaimNumber As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lnkBtnCheckWONumber As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lblSearchChassis As System.Web.UI.WebControls.Label
    Protected WithEvents hdntxtNoChasis As System.Web.UI.WebControls.HiddenField
    Protected WithEvents hdnVechileTypeId As System.Web.UI.WebControls.HiddenField
    Protected WithEvents hdnPPtxtNoChasis As System.Web.UI.WebControls.HiddenField
    Protected WithEvents btnRilis As System.Web.UI.WebControls.Button
    Protected WithEvents btnPermintaanBukti As System.Web.UI.WebControls.Button
    Protected WithEvents btnPrintBarcode As System.Web.UI.WebControls.Button
    Protected WithEvents CaptionNotes As Label
    Protected WithEvents dgFileWSCEvidence As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dgOngkosKerja As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dgParts As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblTglKirim As Label
    Protected WithEvents lblTglKirimVal As Label
    Protected WithEvents trTglKerusakan As HtmlTableRow
    Protected WithEvents trTglPemasangan As HtmlTableRow
    Protected WithEvents trJarakTempuhPemasangan As HtmlTableRow
    Protected WithEvents lblPopUpDealerBranch As Label
    Protected WithEvents txtDealerBranchCode As TextBox
    Protected WithEvents txtBranchName As TextBox
    Protected WithEvents icTglPemasangan As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblTglPemasanganVal As Label
    Protected WithEvents txtOdometerPemasangan As TextBox
    Protected WithEvents downloadAllEvidence As Button
#End Region

#Region "Page_Event"
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()

        'oDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        'oLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        oDealer = CType(GetSession("DEALER"), Dealer)
        oLoginUser = CType(GetSession("LOGINUSERINFO"), UserInfo)

        AttachmentDirectory = KTB.DNet.Lib.WebConfig.GetValue("WSCEvidenceFileDirectory")
        TargetDirectory = KTB.DNet.Lib.WebConfig.GetValue("SAN")
        TempDirectory = Server.MapPath("") + "\..\DataTemp\WSCTemp\" + oDealer.ID.ToString + "-" + oLoginUser.ID.ToString + "\"

    End Sub

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Not SecurityProvider.Authorize(Context.User, SR.WSCSaveData_Privilege) Then
            If oDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                If Not SecurityProvider.Authorize(Context.User, SR.WSCViewData_Privilege) Then
                    Server.Transfer("../FrmAccessDenied.aspx?modulName=WSC - Input WSC")
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
            .Add(New ListItem("Z2", "Z2"))
            .Add(New ListItem("Z4", "Z4"))
            .Add(New ListItem("ZA", "ZA"))
            .Add(New ListItem("ZB", "ZB"))
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

    Private Sub BindWSCEvidence()
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        If Mode = enumMode.Mode.NewItemMode Then
            'dgFileWSCEvidence.DataSource = CType(sessHelper.GetSession("NEW_WSCEVIDENCE"), ArrayList)
            dgFileWSCEvidence.DataSource = CType(GetSession("NEW_WSCEVIDENCE"), ArrayList)
            dgFileWSCEvidence.DataBind()
        Else
            'dgFileWSCEvidence.DataSource = CType(sessHelper.GetSession("WSCEVIDENCE"), ArrayList)
            dgFileWSCEvidence.DataSource = CType(GetSession("WSCEVIDENCE"), ArrayList)
            dgFileWSCEvidence.DataBind()
            If Not IsNothing(GetSession("WSCEVIDENCE")) Then
                If CType(GetSession("WSCEVIDENCE"), ArrayList).Count > 0 Then
                    downloadAllEvidence.Visible = True
                End If
            End If
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
            .Add(New ListItem("Silahkan Pilih", ""))
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
        hdnVechileTypeId.Value = ""
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
        lblTglPKTVal.Text = ""
        icTglKerusakan.Value = DateTime.Today
        lblTglKerusakanVal.Text = ""
        icTglPerbaikan.Value = DateTime.Today
        lblTglPerbaikanVal.Text = ""
        icTglPemasangan.Value = DateTime.Today
        lblTglPemasanganVal.Text = ""
        txtOdometer.Text = ""
        lblOdometerVal.Text = ""
        txtGejala.Text = ""
        txtPemeriksaan.Text = ""
        txtHasil.Text = ""
        ddlKodeWSCA.SelectedIndex = 0
        ddlKodeWSCB.SelectedIndex = 0
        ddlKodeWSCC.SelectedIndex = 0
        txtNotes.Text = ""
        'txtDealerBranchCode.Text = ""
        'txtBranchName.Text = ""
    End Sub

    Private Sub fillForm()
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        'oWSCHeader = CType(sessHelper.GetSession("WSCHEADER"), WSCHeader)
        oWSCHeader = CType(GetSession("WSCHEADER"), WSCHeader)
        If Mode = enumMode.Mode.NewItemMode Then
            'oWSCHeader = CType(sessHelper.GetSession("NEW_WSCHEADER"), WSCHeader)
            oWSCHeader = CType(GetSession("NEW_WSCHEADER"), WSCHeader)
            ClearForm()
            lblDealerVal.Text = oDealer.DealerCode
            lblDealerSearchTerm1.Text = oDealer.SearchTerm1
            lblDealerName.Text = oDealer.DealerName
            lblTglKirimVal.Text = Today
        ElseIf Mode = enumMode.Mode.EditMode OrElse Mode = enumMode.Mode.ViewMode Then
            lblDealerVal.Text = oWSCHeader.Dealer.DealerCode
            lblDealerSearchTerm1.Text = oWSCHeader.Dealer.SearchTerm1
            lblDealerName.Text = oWSCHeader.Dealer.DealerName
            lblTglKirimVal.Text = oWSCHeader.CreateDateText
            If Not IsNothing(oWSCHeader.DealerBranch) Then
                txtDealerBranchCode.Text = oWSCHeader.DealerBranch.DealerBranchCode
                txtBranchName.Text = oWSCHeader.DealerBranch.Name
            End If
        End If
        If IsNothing(oWSCHeader) Then
            oWSCHeader = New WSCHeader()
        End If

        If screenFrom = "PQR" Then
            ViewState("Mode") = enumMode.Mode.NewItemMode
            Mode = ViewState("Mode")
            oPQRHeader = New PQRHeaderFacade(User).Retrieve(pqrId)
            If Not IsNothing(oPQRHeader) AndAlso oPQRHeader.ID > 0 Then
                If Not IsNothing(oPQRHeader.ChassisMaster) Then
                    If Not IsNothing(oPQRHeader.ChassisMaster.ChassisNumber) Then
                        If oDealer.ID = oPQRHeader.ChassisMaster.Dealer.ID Then  '---Jika login dealer id = sold dealer id
                            blnIsSoldDealer = True
                            'sessHelper.SetSession("blnLoginIsSoldDealer", blnIsSoldDealer)
                            SetSession("blnLoginIsSoldDealer", blnIsSoldDealer)
                        End If
                        CheckIsExistChassisMasterPKT(oPQRHeader, blnIsSoldDealer)

                        ddlClaimType.SelectedIndex = 1
                        If oPQRHeader.PQRType = 2 OrElse oPQRHeader.PQRType = 3 Then
                            ddlClaimType.SelectedValue = "ZA"
                        ElseIf oPQRHeader.PQRType = 4 Then
                            ddlClaimType.SelectedValue = "ZB"
                        End If
                        ddlRefDoc.SelectedIndex = 2
                        ddlRefDoc.Enabled = False
                        'ddlRefDoc.Attributes("style") = "display:none"

                        setDisableKodeKerusakan()
                        ddlClaimType.Enabled = False
                        ddlClaimType.Items(2).Enabled = False

                        lnkbtnPopUpInfoDokumen.Visible = False
                        lblSearchChassis.Visible = False
                        If Mode = enumMode.Mode.NewItemMode Then
                            btnSave.Visible = bCekPriv
                            btnPrintBarcode.Visible = False
                            btnRilis.Visible = False
                            btnPermintaanBukti.Visible = False
                            dgParts.Columns(0).Visible = False
                        End If
                        txtPQRNo.Text = oPQRHeader.PQRNo
                        lblPQRNoVal.Text = oPQRHeader.PQRNo
                        LoadPQRHeaderInfo(oPQRHeader)
                        txtNoChasis.Enabled = False
                        txtPQRNo.Visible = True
                        txtPQRNo.Enabled = False
                        'If ddlClaimType.SelectedValue = "Z2" AndAlso ddlRefDoc.SelectedIndex = 2 Then
                        '    'lblPQRNoVal.Visible = True
                        '    'txtPQRNo.Visible = False
                        'End If\
                        txtPQRNo_TextChanged(Nothing, Nothing)
                    End If
                    dgParts.Columns(0).Visible = False
                End If
            Else
                MessageBox.Show("Nomor Rangka tidak valid")
                Server.Transfer("~/PQR/FrmPQRList.aspx")
                Return
            End If

        ElseIf screenFrom = "WSC" Then
            ViewState("Mode") = intViewStateMode
            Mode = intViewStateMode
            oWSCHeader = New WSCHeaderFacade(User).Retrieve(wscId)
            If Not IsNothing(oWSCHeader) Then
                'sessHelper.SetSession("WSCHEADER", oWSCHeader)
                SetSession("WSCHEADER", oWSCHeader)
                RetrieveWSCHeaderDomain()
                setFormView()

                If Mode <> enumMode.Mode.ViewMode Then
                    If ddlClaimType.SelectedValue.Trim = "ZA" OrElse ddlClaimType.SelectedValue.Trim = "Z2" OrElse ddlClaimType.SelectedValue.Trim = "ZB" Then
                        ddlRefDoc.Enabled = False
                    ElseIf ddlClaimType.SelectedValue.Trim = "Z4" Then
                        ddlRefDoc.Enabled = False
                    ElseIf ddlClaimType.SelectedValue.Trim = "Z6" AndAlso Mode = enumMode.Mode.NewItemMode Then
                        ddlRefDoc.Enabled = True
                    Else
                        ddlRefDoc.Enabled = False
                    End If
                    txtNoChasis.ReadOnly = True
                    lnkbtnCheckChassis.Attributes("style") = "display:none"
                    If ddlRefDoc.SelectedValue = "0" Then  'Referensi Buletin Doc
                        dgOngkosKerja.ShowFooter = False
                        dgParts.ShowFooter = False
                        'dgOngkosKerja.Columns(dgOngkosKerja.Columns.Count - 2).Visible = False
                        dgParts.Columns(dgParts.Columns.Count - 2).Visible = False
                    End If
                End If
                lblTglKirimVal.Text = oWSCHeader.CreateDateText
            End If
        Else
            ViewState("Mode") = intViewStateMode
            Mode = intViewStateMode

            setFormView()
            'ddlClaimType.Enabled = True

            oWSCHeader = New WSCHeader
            'sessHelper.SetSession("NEW_WSCHEADER", oWSCHeader)
            SetSession("NEW_WSCHEADER", oWSCHeader)
            lblTglKirimVal.Text = oWSCHeader.CreateDateText
        End If

        RefreshGrid()
    End Sub

    Private Sub setEnabledReferensiDokumen(enb As Boolean)

        dgOngkosKerja.Enabled = enb
        dgParts.Enabled = enb
        'dgFileWSCEvidence.Enabled = enb
        If ddlClaimType.SelectedValue.Trim = "ZA" Then
            icTglPemasangan.Enabled = True
            txtOdometerPemasangan.Enabled = True
        ElseIf ddlClaimType.SelectedValue.Trim = "ZB" Then
            trTglPemasangan.Visible = False
            trJarakTempuhPemasangan.Visible = False
        Else
            icTglPemasangan.Enabled = False
            txtOdometerPemasangan.Enabled = False
        End If
        icTglKerusakan.Enabled = enb
        icTglPerbaikan.Enabled = enb
        'icTglPemasangan.Enabled = enb
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
        BindWSCEvidence()
    End Sub

    Private Sub BindOngkosKerja()
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        If Mode = enumMode.Mode.NewItemMode Then
            'Dim arlOngker As ArrayList = CType(sessHelper.GetSession("NEW_ONGKOSKERJA"), ArrayList)
            Dim arlOngker As ArrayList = CType(GetSession("NEW_ONGKOSKERJA"), ArrayList)
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
            'dgOngkosKerja.DataSource = CType(sessHelper.GetSession("ONGKOSKERJA"), ArrayList)
            dgOngkosKerja.DataSource = CType(GetSession("ONGKOSKERJA"), ArrayList)
            dgOngkosKerja.DataBind()
        End If
    End Sub

    Private Sub BindOngkosParts()
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        If Mode = enumMode.Mode.NewItemMode Then
            'dgParts.DataSource = CType(sessHelper.GetSession("NEW_ONGKOSPARTS"), ArrayList)
            dgParts.DataSource = CType(GetSession("NEW_ONGKOSPARTS"), ArrayList)
            dgParts.DataBind()
        Else
            'dgParts.DataSource = CType(sessHelper.GetSession("ONGKOSPARTS"), ArrayList)
            dgParts.DataSource = CType(GetSession("ONGKOSPARTS"), ArrayList)
            dgParts.DataBind()
        End If

        If ddlClaimType.SelectedValue.Trim = "ZA" Then
            dgParts.Columns(5).Visible = True
            dgParts.Columns(6).Visible = True
        Else
            dgParts.Columns(5).Visible = False
            dgParts.Columns(6).Visible = False
        End If
    End Sub

    Private Sub setDisableKodeKerusakan()
        'Dim blnIsExistPosition As Boolean = False
        'Mode = CType(ViewState("Mode"), enumMode.Mode)
        'Dim _arrONGKOSKERJA As ArrayList = CType(sessHelper.GetSession("NEW_ONGKOSKERJA"), ArrayList)
        'If Mode = enumMode.Mode.EditMode Then
        '    _arrONGKOSKERJA = CType(sessHelper.GetSession("ONGKOSKERJA"), ArrayList)
        'End If
        If ddlClaimType.SelectedValue = "Z4" Then
            '    ddlRefDoc.SelectedValue = 0
            '    For Each _objWSCDetail As WSCDetail In _arrONGKOSKERJA
            '        If _objWSCDetail.PositionCode = "XEE999" Then
            '            blnIsExistPosition = True
            '            Exit For
            '        End If
            '    Next
            'ElseIf ddlClaimType.SelectedValue = "Z2" Then
            '    ddlRefDoc.SelectedValue = 1
            '    blnIsExistPosition = False
            'Else
            '    If ddlRefDoc.SelectedValue = 0 Then
            '        For Each _objWSCDetail As WSCDetail In _arrONGKOSKERJA
            '            If _objWSCDetail.PositionCode = "XEE999" Then
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
        If ddlRefDoc.SelectedIndex = 1 Then
            lblPQRNoVal.Visible = False
            txtPQRNo.Visible = True
        End If
        lnkbtnPopUpInfoDokumen.Visible = enb
        lnkbtnPopUpRefClaimNumber.Visible = enb
        txtRefClaimNumber.Enabled = enb
        txtNoChasis.Enabled = enb
        lblSearchChassis.Visible = enb
        txtDealerBranchCode.Enabled = enb

        'req by Doni 12022020
        'txtWONumber.Enabled = enb

        If enb Then
            lnkbtnCheckChassis.Attributes("style") = "display:table-row"
        Else
            lnkbtnCheckChassis.Attributes("style") = "display:none"
        End If

        If ddlClaimType.SelectedValue.Trim = "ZA" Then
            icTglPemasangan.Enabled = True
        Else
            icTglPemasangan.Enabled = False
        End If
        icTglKerusakan.Enabled = enb
        If ddlRefDoc.SelectedValue = 1 Then
            icTglKerusakan.Enabled = False
        End If
        icTglPerbaikan.Enabled = enb
        icTglPemasangan.Enabled = enb
        txtOdometer.Enabled = enb
        txtOdometerPemasangan.Enabled = enb
        txtGejala.Enabled = enb
        txtPemeriksaan.Enabled = enb
        txtHasil.Enabled = enb
        ddlKodeWSCA.Enabled = enb
        ddlKodeWSCB.Enabled = enb
        ddlKodeWSCC.Enabled = enb
        txtNotes.Enabled = enb
        btnPrintBarcode.Visible = Not enb
        dgParts.Columns(0).Visible = Not enb

        'If oWSCHeader.Status = CType(enumStatusWSC.Status.Proses, String) Then
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
        If oWSCHeader.Status = CType(enumStatusWSC.Status.Baru, String) Then
            btnRilis.Visible = True
        Else
            btnPrintBarcode.Visible = True
        End If

        'btnPermintaanBukti.Visible = enb
        btnSave.Visible = enb

        dgOngkosKerja.Enabled = enb
        dgParts.Enabled = enb
        'dgFileWSCEvidence.Enabled = enb
        dgOngkosKerja.ShowFooter = enb
        dgParts.ShowFooter = enb
        dgFileWSCEvidence.ShowFooter = enb
        dgOngkosKerja.Columns(dgOngkosKerja.Columns.Count - 2).Visible = enb
        dgParts.Columns(dgParts.Columns.Count - 2).Visible = enb
        dgFileWSCEvidence.Columns(dgFileWSCEvidence.Columns.Count - 1).Visible = enb

    End Sub

    Private Sub setFormView()
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        If Mode = enumMode.Mode.NewItemMode Then
            setEnbDisableForm(False)
            btnSave.Visible = bCekPriv
            btnPrintBarcode.Visible = False
            btnRilis.Visible = False
            btnBatal.Visible = False
            btnPermintaanBukti.Visible = False
            ddlClaimType.Enabled = True
            dgOngkosKerja.ShowFooter = True
            dgParts.ShowFooter = True
            dgFileWSCEvidence.ShowFooter = True
            dgOngkosKerja.Columns(dgOngkosKerja.Columns.Count - 2).Visible = True
            dgParts.Columns(0).Visible = False
            dgParts.Columns(dgParts.Columns.Count - 2).Visible = True
            dgFileWSCEvidence.Columns(dgFileWSCEvidence.Columns.Count - 1).Visible = True

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
            'ddlKodeWSCA.Enabled = True
            'ddlKodeWSCB.Enabled = True
            'ddlKodeWSCC.Enabled = True
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
            If (ddlClaimType.SelectedValue = "ZA" OrElse ddlClaimType.SelectedValue = "Z2" OrElse ddlClaimType.SelectedValue = "ZB") AndAlso ddlRefDoc.SelectedIndex = 2 Then
                txtPQRNo.Visible = False
                'ddlRefDoc.Attributes("style") = "display:none"
                lblPQRNoVal.Visible = True
            End If
            txtRefClaimNumber.Enabled = False
            lnkbtnPopUpInfoDokumen.Visible = False
            lnkbtnPopUpRefClaimNumber.Visible = False
            lblSearchChassis.Visible = False
            lnkbtnCheckChassis.Visible = False
            btnSave.Visible = bCekPriv
            btnBatal.Visible = True
            btnPrintBarcode.Visible = False
            CaptionNotes.Visible = True
            txtNotes.Visible = True
            If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                txtNotes.Enabled = False
                btnPermintaanBukti.Visible = False
                If oWSCHeader.Status = CType(enumStatusWSC.Status.Baru, String) Then
                    btnRilis.Visible = True
                End If
            Else
                txtNotes.Enabled = True
                btnPermintaanBukti.Visible = True
                btnRilis.Visible = False
            End If
            btnPrintBarcode.Visible = False
            dgParts.Columns(0).Visible = False
            If CType(icTglKerusakan.Value.ToString("yyyy"), Integer) < 2000 Then
                trTglKerusakan.Visible = False
            Else
                trTglKerusakan.Visible = True
            End If
        ElseIf Mode = enumMode.Mode.ViewMode Then
            setEnbDisableForm(False)
            btnPrintBarcode.Visible = True
            dgParts.Columns(0).Visible = True
            dgParts.Enabled = True
            btnRilis.Visible = False
            If (ddlClaimType.SelectedValue = "ZA" OrElse ddlClaimType.SelectedValue = "Z2" OrElse ddlClaimType.SelectedValue = "ZB") AndAlso ddlRefDoc.SelectedIndex = 2 Then
                txtPQRNo.Visible = False
                'ddlRefDoc.Attributes("style") = "display:none"
                lblPQRNoVal.Visible = True
            End If
            CaptionNotes.Visible = True
            txtNotes.Visible = True
            txtNotes.Enabled = False
            If oDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
                btnPermintaanBukti.Visible = False
                If oWSCHeader.Status = CType(enumStatusWSC.Status.Baru, String) Then
                    btnRilis.Visible = True
                End If
            Else
                btnPermintaanBukti.Visible = True
                btnRilis.Visible = False
                txtNotes.Enabled = True
                btnSave.Visible = True
            End If
            If CType(icTglKerusakan.Value.ToString("yyyy"), Integer) < 2000 Then
                trTglKerusakan.Visible = False
            Else
                trTglKerusakan.Visible = True
            End If
        End If
    End Sub

    Private Sub SetControlKodeKerusakan(ByVal oWSCHdr As WSCHeader)
        Dim IsImplementKodeKerusakan As Boolean = False
        Mode = CType(ViewState("Mode"), enumMode.Mode)

        ddlKodeWSCA.Enabled = False
        ddlKodeWSCB.Enabled = False
        ddlKodeWSCC.Enabled = False
        If Mode = enumMode.Mode.NewItemMode OrElse Mode = enumMode.Mode.EditMode Then
            'If CType(sessHelper.GetSession("DEALER"), Dealer).Title = EnumDealerTittle.DealerTittle.DEALER Then
            If CType(GetSession("DEALER"), Dealer).Title = EnumDealerTittle.DealerTittle.DEALER Then
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
                For Each obj As WSCEvidence In AttachmentCollection
                    If Not IsNothing(obj.AttachmentData) Or obj.IsFromPQR Then
                        TargetFInfo = New FileInfo(TargetDirectory + obj.PathFile)
                        TempFInfo = New FileInfo(TempDirectory + obj.PathFile)

                        If TempFInfo.Exists Then
                            If Not TargetFInfo.Directory.Exists Then
                                Directory.CreateDirectory(TargetFInfo.DirectoryName)
                            End If
                            TempFInfo.MoveTo(TargetFInfo.FullName)
                        End If

                        If obj.IsFromPQR Then
                            TempFInfo = New FileInfo(TempDirectory + obj.TempFilePath)
                            TempFInfo.CopyTo(TargetDirectory + obj.PathFile)
                            Continue For
                        End If

                        obj.AttachmentData.SaveAs(TargetDirectory + obj.PathFile)
                    ElseIf obj.ID > 0 Then
                        Dim fileNameFormat As String = obj.WSCHeader.ChassisMaster.ChassisNumber + "-" + obj.WSCHeader.Dealer.DealerCode + "-" + obj.WSCHeader.ClaimNumber
                        If Not obj.PathFile.Contains(fileNameFormat) Then
                            TargetFInfo = New FileInfo(TargetDirectory + obj.PathFile)
                            If TargetFInfo.Exists Then
                                Dim newFileNameFormat As String = fileNameFormat & "-" & DateTime.Now.ToString("ffff") & Path.GetExtension(obj.PathFile)
                                Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("WSCEvidenceFileDirectory") & "\" & lblDealerVal.Text & "\" & newFileNameFormat
                                TargetFInfo.MoveTo(TargetDirectory + DestFile)
                                obj.PathFile = DestFile
                                Dim objUpdate As WSCEvidenceFacade = New WSCEvidenceFacade(User)
                                objUpdate.Update(obj)
                            End If
                        End If

                    End If
                Next
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function copyAndRenameReferencedFile(ByVal prevPath, ByVal newNameFormat) As String
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim targetFile As FileInfo = New FileInfo(prevPath)
        Dim TempFInfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                TempFInfo = New FileInfo(TempDirectory + newNameFormat)
                If Not TempFInfo.Directory.Exists Then
                    Directory.CreateDirectory(TempFInfo.DirectoryName)
                End If
                If targetFile.Exists Then
                    If TempFInfo.Exists Then
                        TempFInfo.Delete()
                    End If
                    targetFile.CopyTo(TempDirectory + newNameFormat)
                End If
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            imp.StopImpersonate()
            imp = Nothing
            Throw ex
        End Try
    End Function

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

    Private Function CopyBeforeZip(ByVal AttachmentCollection As ArrayList, ByRef nameGuid As String) As String
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim TargetFInfo As FileInfo
        Dim TempFInfo As FileInfo
        nameGuid = Guid.NewGuid().ToString().Substring(0, 5)
        Dim _ret As String = TempDirectory & "\" & nameGuid & "\"

        Try
            success = imp.Start()
            If success Then
                For Each obj As WSCEvidence In AttachmentCollection
                    TargetFInfo = New FileInfo(TargetDirectory + obj.PathFile)
                    Dim fileName As FileInfo = New FileInfo(obj.PathFile)
                    TempFInfo = New FileInfo(_ret + fileName.Name)

                    If TargetFInfo.Exists Then
                        If Not TempFInfo.Directory.Exists Then
                            Directory.CreateDirectory(TempFInfo.DirectoryName)
                        End If
                        TargetFInfo.CopyTo(TempFInfo.FullName)
                    End If
                Next
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            imp.StopImpersonate()
            imp = Nothing
            Throw ex
        End Try

        Return _ret
    End Function

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

    Private Sub RetrieveWSCHeaderDomain()
        'oDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        oDealer = CType(GetSession("DEALER"), Dealer)

        Mode = CType(ViewState("Mode"), enumMode.Mode)
        'oWSCHeader = CType(sessHelper.GetSession("WSCHEADER"), WSCHeader)
        oWSCHeader = CType(GetSession("WSCHEADER"), WSCHeader)
        If IsNothing(oWSCHeader) Then
            oWSCHeader = New WSCHeader
        End If

        lblDealerVal.Text = oWSCHeader.Dealer.DealerCode
        lblDealerSearchTerm1.Text = oWSCHeader.Dealer.SearchTerm1
        lblDealerName.Text = oWSCHeader.Dealer.DealerName

        ddlClaimType.SelectedValue = oWSCHeader.ClaimType
        txtPQRNo.Text = oWSCHeader.PQR
        lblPQRNoVal.Text = oWSCHeader.PQR
        'Cek di Dokumen Buletin Reference
        If txtPQRNo.Text.Trim <> "" Then
            Dim oRecallCategory As RecallCategory = New RecallCategoryFacade(User).Retrieve(txtPQRNo.Text.Trim)
            If Not IsNothing(oRecallCategory) Then
                If oRecallCategory.ID <> 0 Then
                    ddlRefDoc.SelectedValue = 0   'Select ke Dokumen Buletin Reference
                Else
                    'Cek di Dokumen PQR Reference
                    Dim oPQRHdr As PQRHeader = New PQRHeaderFacade(User).Retrieve(txtPQRNo.Text.Trim)
                    If Not IsNothing(oPQRHdr) AndAlso oPQRHdr.ID <> 0 Then
                        ddlRefDoc.SelectedValue = 1
                    End If
                End If
            Else

                'Cek di Dokumen PQR Reference
                Dim oPQRHdr As PQRHeader = New PQRHeaderFacade(User).Retrieve(txtPQRNo.Text.Trim)
                If Not IsNothing(oPQRHdr) AndAlso oPQRHdr.ID <> 0 Then
                    ddlRefDoc.SelectedValue = 1
                End If
                'Else
                '    ddlRefDoc.SelectedValue = 0
            End If
        End If

        lblClaimNumber.Text = oWSCHeader.ClaimNumber
        txtRefClaimNumber.Text = oWSCHeader.RefClaimNumber
        txtNoChasis.Text = oWSCHeader.ChassisMaster.ChassisNumber
        hdntxtNoChasis.Value = txtNoChasis.Text
        hdnVechileTypeId.Value = oWSCHeader.ChassisMaster.VechileColor.VechileType.ID
        lnkbtnCheckChassisClick()
        lblNoChasisVal.Text = oWSCHeader.ChassisMaster.ChassisNumber
        LoadChassisInfo(oWSCHeader.ChassisMaster)
        icTglKerusakan.Value = oWSCHeader.FailureDate
        lblTglKerusakanVal.Text = oWSCHeader.FailureDate
        icTglPerbaikan.Value = oWSCHeader.ServiceDate
        lblTglPerbaikanVal.Text = oWSCHeader.ServiceDate
        icTglPemasangan.Value = oWSCHeader.InstallDate
        lblTglPemasanganVal.Text = oWSCHeader.InstallDate
        txtOdometer.Text = oWSCHeader.Miliage
        txtOdometerPemasangan.Text = oWSCHeader.InstallMiliage

        txtGejala.Text = oWSCHeader.Description
        txtPemeriksaan.Text = oWSCHeader.Causes
        txtNotes.Text = oWSCHeader.Notes
        txtHasil.Text = oWSCHeader.Results
        txtWONumber.Text = oWSCHeader.WorkOrderNumber
        If Not IsNothing(oWSCHeader.DealerBranch) Then
            txtDealerBranchCode.Text = oWSCHeader.DealerBranch.DealerBranchCode
            txtBranchName.Text = oWSCHeader.DealerBranch.Name
        End If
        If oWSCHeader.CodeA = "" Then
            ddlKodeWSCA.SelectedIndex = 0
        Else
            ddlKodeWSCA.SelectedValue = oWSCHeader.CodeA
        End If
        If oWSCHeader.CodeB = "" Then
            ddlKodeWSCB.SelectedIndex = 0
        Else
            ddlKodeWSCB.SelectedValue = oWSCHeader.CodeB
        End If
        If oWSCHeader.CodeC = "" Then
            ddlKodeWSCC.SelectedIndex = 0
        Else
            ddlKodeWSCC.SelectedValue = oWSCHeader.CodeC
        End If

        Dim arrWSCDetailLabor As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(WSCDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(WSCDetail), "WSCHeader.ID", MatchType.Exact, oWSCHeader.ID))
        criterias.opAnd(New Criteria(GetType(WSCDetail), "WSCType", MatchType.Exact, "L"))
        arrWSCDetailLabor = New WSCDetailFacade(User).Retrieve(criterias)
        If IsNothing(arrWSCDetailLabor) And arrWSCDetailLabor.Count = 0 Then
            arrWSCDetailLabor = New ArrayList
        End If

        Dim arrWSCDetailPart As ArrayList = New ArrayList
        Dim criterias2 As New CriteriaComposite(New Criteria(GetType(WSCDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias2.opAnd(New Criteria(GetType(WSCDetail), "WSCHeader.ID", MatchType.Exact, oWSCHeader.ID))
        criterias2.opAnd(New Criteria(GetType(WSCDetail), "WSCType", MatchType.Exact, "P"))
        arrWSCDetailPart = New WSCDetailFacade(User).Retrieve(criterias2)
        If IsNothing(arrWSCDetailPart) And arrWSCDetailPart.Count = 0 Then
            arrWSCDetailPart = New ArrayList
        End If

        'sessHelper.SetSession("ONGKOSKERJA", arrWSCDetailLabor)
        'sessHelper.SetSession("DELETEDONGKOSKERJA", New ArrayList)
        'sessHelper.SetSession("ONGKOSPARTS", arrWSCDetailPart)
        'sessHelper.SetSession("DELETEDPARTS", New ArrayList)
        'sessHelper.SetSession("WSCEVIDENCE", GetAttachmentList(oWSCHeader.WSCEvidences))
        'sessHelper.SetSession("DELETEDWSEVIDENCE", New ArrayList)
        SetSession("ONGKOSKERJA", arrWSCDetailLabor)
        SetSession("DELETEDONGKOSKERJA", New ArrayList)
        SetSession("ONGKOSPARTS", arrWSCDetailPart)
        SetSession("DELETEDPARTS", New ArrayList)
        SetSession("WSCEVIDENCE", GetAttachmentList(oWSCHeader.WSCEvidences))
        SetSession("DELETEDWSEVIDENCE", New ArrayList)
        RefreshGrid()

    End Sub

    Private Sub BindWSCHeaderDomain()
        'oDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        oDealer = CType(GetSession("DEALER"), Dealer)
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        'oWSCHeader = CType(sessHelper.GetSession("WSCHEADER"), WSCHeader)
        oWSCHeader = CType(GetSession("WSCHEADER"), WSCHeader)
        If Mode = enumMode.Mode.NewItemMode Then
            'oWSCHeader = CType(sessHelper.GetSession("NEW_WSCHEADER"), WSCHeader)
            oWSCHeader = CType(GetSession("NEW_WSCHEADER"), WSCHeader)
        End If
        If IsNothing(oWSCHeader) Then
            oWSCHeader = New WSCHeader
        End If

        oWSCHeader.ClaimType = ddlClaimType.SelectedValue
        If Mode = enumMode.Mode.NewItemMode Then
            oWSCHeader.Dealer = oDealer
            oWSCHeader.ClaimNumber = GetNextClaimNumberWSC(oDealer.ID)
        ElseIf Mode = enumMode.Mode.EditMode Then
            oWSCHeader.ClaimNumber = lblClaimNumber.Text
        End If

        oWSCHeader.RefClaimNumber = txtRefClaimNumber.Text
        oWSCHeader.ChassisMaster = oChassis
        oWSCHeader.Miliage = CInt(txtOdometer.Text)
        If txtOdometerPemasangan.Text.Trim <> "" OrElse txtOdometerPemasangan.Text.Trim <> String.Empty Then
            oWSCHeader.InstallMiliage = CInt(txtOdometerPemasangan.Text)
        End If
        oWSCHeader.ServiceDate = icTglPerbaikan.Value
        oWSCHeader.FailureDate = icTglKerusakan.Value
        oWSCHeader.InstallDate = icTglPemasangan.Value
        oWSCHeader.Results = KTB.DNet.Utility.CommonFunction.RemoveWhiteSpace(txtHasil.Text)
        oWSCHeader.Notes = KTB.DNet.Utility.CommonFunction.RemoveWhiteSpace(txtNotes.Text)
        oWSCHeader.Causes = KTB.DNet.Utility.CommonFunction.RemoveWhiteSpace(txtPemeriksaan.Text)
        oWSCHeader.Description = KTB.DNet.Utility.CommonFunction.RemoveWhiteSpace(txtGejala.Text)


        ' Add Branch
        If Not String.IsNullorEmpty(txtDealerBranchCode.Text) Then
            Dim branchCriteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerBranch), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            branchCriteria.opAnd(New Criteria(GetType(DealerBranch), "DealerBranchCode", MatchType.Exact, txtDealerBranchCode.Text))
            Dim branchList As ArrayList = New DealerBranchFacade(HttpContext.Current.User).Retrieve(branchCriteria)
            If branchList.Count = 1 Then
                ' Dealer Branch Found
                oWSCHeader.DealerBranch = branchList(0)
            End If
        End If

        'oWSCHeader.Description = ""

        oWSCHeader.PQR = txtPQRNo.Text
        Dim intPQRStatus As Integer = Nothing
        Dim oPQRHeader As PQRHeader = New PQRHeaderFacade(User).Retrieve(txtPQRNo.Text)
        If Not IsNothing(oPQRHeader) AndAlso oPQRHeader.ID > 0 Then
            intPQRStatus = oPQRHeader.RowStatus
        End If
        oWSCHeader.PQRStatus = intPQRStatus
        oWSCHeader.Status = enumStatusWSC.Status.Baru

        'If tblKodeKerusakan.Visible And ddlKodeWSCA.Enabled Then
        'If Me.ddlKodeWSCA.SelectedValue <> "-1" Then
        oWSCHeader.CodeA = Me.ddlKodeWSCA.SelectedValue
        'End If
        'If Me.ddlKodeWSCB.SelectedValue <> "-1" Then
        oWSCHeader.CodeB = Me.ddlKodeWSCB.SelectedValue
        'End If
        'If Me.ddlKodeWSCC.SelectedValue <> "-1" Then
        oWSCHeader.CodeC = Me.ddlKodeWSCC.SelectedValue
        'End If
        'End If

        'Dim _arrWSCEVIDENCE As ArrayList = CType(sessHelper.GetSession("WSCEVIDENCE"), ArrayList)
        Dim _arrWSCEVIDENCE As ArrayList = CType(GetSession("WSCEVIDENCE"), ArrayList)
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        If Mode = enumMode.Mode.NewItemMode Then
            '_arrWSCEVIDENCE = CType(sessHelper.GetSession("NEW_WSCEVIDENCE"), ArrayList)
            _arrWSCEVIDENCE = CType(GetSession("NEW_WSCEVIDENCE"), ArrayList)
        End If

        Dim strEvidenceTypePhoto As String = String.Empty
        Dim strEvidenceDmgPart As String = String.Empty
        Dim strEvidenceInvoice As String = String.Empty
        Dim strEvidenceRepair As String = String.Empty
        Dim strEvidenceWSCLetter As String = String.Empty
        Dim strEvidenceWSCTechnical As String = String.Empty

        If _arrWSCEVIDENCE.Count > 0 Then
            For Each objWSCEvidence As WSCEvidence In _arrWSCEVIDENCE
                If objWSCEvidence.EvidenceType = "5" Then   'Type Photo
                    strEvidenceTypePhoto = "X"
                End If
                If objWSCEvidence.EvidenceType = "4" Then   'Type Part Bekas
                    strEvidenceDmgPart = "X"
                End If
                If objWSCEvidence.EvidenceType = "0" Then   'Type Kwitansi WSC
                    strEvidenceInvoice = "X"
                End If
                If objWSCEvidence.EvidenceType = "3" Then   'Type Repair/WO
                    strEvidenceRepair = "X"
                End If
                If objWSCEvidence.EvidenceType = "1" Then   'Type Surat WSC
                    strEvidenceWSCLetter = "X"
                End If
                If objWSCEvidence.EvidenceType = "2" Then   'Type Teknikal WSC
                    strEvidenceWSCTechnical = "X"
                End If
            Next
        End If

        oWSCHeader.EvidencePhoto = strEvidenceTypePhoto
        oWSCHeader.EvidenceDmgPart = strEvidenceDmgPart
        oWSCHeader.EvidenceInvoice = strEvidenceInvoice
        oWSCHeader.EvidenceRepair = strEvidenceRepair
        oWSCHeader.EvidenceWSCLetter = strEvidenceWSCLetter
        oWSCHeader.EvidenceWSCTechnical = strEvidenceWSCTechnical

        Mode = CType(ViewState("Mode"), enumMode.Mode)
        If Mode = enumMode.Mode.NewItemMode Then
            'sessHelper.SetSession("NEW_WSCHEADER", oWSCHeader)
            SetSession("NEW_WSCHEADER", oWSCHeader)
        Else
            'sessHelper.SetSession("WSCHEADER", oWSCHeader)
            SetSession("WSCHEADER", oWSCHeader)
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

        If Not IsNothing(dgParts.Items) AndAlso dgParts.Items.Count > 0 Then
            For Each dtgitem As DataGridItem In dgParts.Items
                Dim txtPartPriceItem As TextBox = dtgitem.FindControl("txtPartPriceItem")
                Dim txtKodePartsItem As TextBox = dtgitem.FindControl("txtKodePartsItem")
                If Not IsNothing(txtPartPriceItem) Then
                    If txtKodePartsItem.Text.Trim.ToUpper() = "NPN7" AndAlso ((txtPartPriceItem.Text = String.Empty) OrElse (txtPartPriceItem.Text = "0")) Then
                        MessageBox.Show("Silakan Input Harga item Part")
                        Return False
                    End If
                End If


            Next
        End If

        If txtNoChasis.Text = String.Empty Then
            MessageBox.Show("Nomor Rangka masih kosong")
            Return False
        End If

        '20220125
        If oChassisFacade.IsExistByChassisAndEngineNumber(txtNoChasis.Text, lblNoMesinVal.Text) Then
            txtNoChasis.ForeColor = Color.Black
        Else
            txtNoChasis.ForeColor = Color.Red
            MessageBox.Show("No Rangka dan No Mesin Tidak Sama, silahkan dicek kembali")
            Return False
        End If
        'end 20220125

        If oChassisFacade.IsExist(txtNoChasis.Text) Then
            txtNoChasis.ForeColor = Color.Black
            LoadChassisInfo(CType(oChassisFacade.Retrieve(txtNoChasis.Text), ChassisMaster))
        Else
            txtNoChasis.ForeColor = Color.Red
            'ClearChassisInfo()
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

        If Mode = enumMode.Mode.EditMode Then
            If Not LoopDgrid("ALLGRID", Nothing, Nothing, "Edit", 0) Then
                Return False
            End If
        Else
            If Not LoopDgrid("ALLGRID", Nothing, Nothing, "Simpan", 0) Then
                Return False
            End If
        End If

        'Dim _arrONGKER As ArrayList = CType(sessHelper.GetSession("ONGKOSKERJA"), ArrayList)
        Dim _arrONGKER As ArrayList = CType(GetSession("ONGKOSKERJA"), ArrayList)
        If Mode = enumMode.Mode.NewItemMode Then
            '_arrONGKER = CType(sessHelper.GetSession("NEW_ONGKOSKERJA"), ArrayList)
            _arrONGKER = CType(GetSession("NEW_ONGKOSKERJA"), ArrayList)
        End If
        For Each _objWSCDetail As WSCDetail In _arrONGKER
            If _objWSCDetail.PositionCode.ToUpper = "XEE999" AndAlso _objWSCDetail.WorkCode = "99" Then
                blnIsExistPositionXEE999 = True
                Exit For
            End If
        Next

        If ddlClaimType.SelectedValue = "Z4" OrElse ddlClaimType.SelectedValue = "Z6" Then
            Dim searchOngKer = From c As WSCDetail In _arrONGKER Order By c.WorkCode Ascending Select c
            For Each _objWSCDetail As WSCDetail In searchOngKer
                'If _objWSCDetail.PositionCode = "10" OrElse _objWSCDetail.PositionCode = "20" Then
                If _objWSCDetail.WorkCode = "10" OrElse _objWSCDetail.WorkCode = "20" Then
                    Dim refinedSearch As List(Of WSCDetail) = (From c As WSCDetail In _arrONGKER Where c.PositionCode = _objWSCDetail.PositionCode Select c).ToList()

                    'If refinedSearch.Count > 1 Then
                    If refinedSearch.Count > 2 Then
                        'MessageBox.Show("WSC Z4 and Z6 tidak dapat memilih 2 Kode Kerja dalam 1 Kode Posisi Terkecuali Kode Kerja 90")
                        MessageBox.Show("WSC Z4 dan Z6 tidak dapat memilih lebih dari 1 kode kerja dalam 1 kode posisi terkecuali dengan kode kerja 90")
                        Return False
                    ElseIf refinedSearch.Count = 2 Then
                        'refinedSearch = (From c As WSCDetail In _arrONGKER Where c.PositionCode = _objWSCDetail.PositionCode AndAlso c.WorkCode = 90 Select c).ToList()
                        refinedSearch = (From c As WSCDetail In _arrONGKER Where c.PositionCode = _objWSCDetail.PositionCode AndAlso c.WorkCode = 90 Select c).ToList()

                        If refinedSearch.Count = 0 Then
                            'MessageBox.Show("WSC Z4 and Z6 tidak dapat memilih 2 Kode Kerja dalam 1 Kode Posisi Terkecuali Kode Kerja 90")
                            MessageBox.Show("WSC Z4 dan Z6 tidak dapat memilih lebih dari 1 kode kerja dalam 1 kode posisi terkecuali dengan kode kerja 90")
                            Return False
                        End If
                    End If
                End If
            Next
        End If

        If (ddlClaimType.SelectedValue = "Z4" OrElse ddlClaimType.SelectedValue = "Z6") AndAlso ddlRefDoc.SelectedValue = "0" Then   ' --> Service Bulletin Ref
            If blnIsExistPositionXEE999 = True Then
                MessageBox.Show("Kode Posisi XEE999 dan Kode Kerja 99 tidak boleh di input \nuntuk tipe Claim Z4 atau Z6 tipe Service Buletin Reference")
                Return False
            End If
        End If
        If (ddlClaimType.SelectedValue = "ZA" OrElse ddlClaimType.SelectedValue = "Z2" OrElse ddlClaimType.SelectedValue = "ZB") AndAlso blnIsExistPositionXEE999 Then
            If dgParts.Items.Count > 0 Then
                MessageBox.Show("Kode Posisi XEE999 dengan Kode Kerja 99, tidak boleh ada part di table ongkos part")
                Return False
            End If
        End If

        '---- Cek Tgl Kerusakan dan Odometer
        Dim criteriaas As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriaas.opAnd(New Criteria(GetType(WSCHeader), "ChassisMaster.ChassisNumber", MatchType.Exact, txtNoChasis.Text))
        Dim objWSCHeaderFacade As WSCHeaderFacade
        objWSCHeaderFacade = New WSCHeaderFacade(User)
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(WSCHeader), "CreatedTime", Sort.SortDirection.DESC))
        Dim arrWSC As ArrayList = objWSCHeaderFacade.RetrieveByCriteria(criteriaas, sortColl)
        If Not IsNothing(arrWSC) AndAlso arrWSC.Count > 0 Then
            Dim oWSCHeader As WSCHeader = CType(arrWSC(0), WSCHeader)
            If Mode <> enumMode.Mode.EditMode Then
                If txtRefClaimNumber.Text.Trim = "" AndAlso Not blnIsExistPositionXEE999 Then
                    If CType(icTglKerusakan.Value.ToString("yyyyMMdd"), Integer) < CType(oWSCHeader.FailureDate.ToString("yyyyMMdd"), Integer) Then
                        MessageBox.Show("Tanggal kerusakan tidak boleh kurang dari tanggal kerusakan sebelumnya")
                        Return False
                    End If
                End If
                If CType(icTglKerusakan.Value.ToString("yyyyMMdd"), Integer) > CType(DateTime.Now.ToString("yyyyMMdd"), Integer) Then
                    MessageBox.Show("Tanggal kerusakan tidak boleh lebih besar dari tanggal hari ini")
                    Return False
                End If
                If CType(txtOdometer.Text, Integer) <= CType(oWSCHeader.Miliage, Integer) Then
                    If Not oWSCHeader.ClaimStatus.Contains("DAPP") Then
                        MessageBox.Show("Jarak tempuh tidak boleh kurang dari jarak tempuh sebelumnya")
                        Return False
                    End If
                End If
            Else
                If CType(icTglKerusakan.Value.ToString("yyyyMMdd"), Integer) > CType(oWSCHeader.CreatedTime.ToString("yyyyMMdd"), Integer) Then
                    MessageBox.Show("Tanggal kerusakan tidak boleh lebih besar dari tanggal kirim")
                    Return False
                End If
                'If CType(icTglKerusakan.Value.ToString("yyyyMMdd"), Integer) > CType(DateTime.Now.ToString("yyyyMMdd"), Integer) Then
                '    MessageBox.Show("Tanggal kerusakan tidak boleh lebih besar dari tanggal hari ini")
                '    Return False
                'End If
                If CType(icTglPerbaikan.Value.ToString("yyyyMMdd"), Integer) > CType(oWSCHeader.CreatedTime.ToString("yyyyMMdd"), Integer) Then
                    MessageBox.Show("Tanggal Service tidak boleh lebih besar dari tanggal kirim")
                    Return False
                End If
                'If CType(icTglPerbaikan.Value.ToString("yyyyMMdd"), Integer) > CType(DateTime.Now.ToString("yyyyMMdd"), Integer) Then
                '    MessageBox.Show("Tanggal perbaikan tidak boleh lebih besar dari tanggal hari ini")
                '    Return False
                'End If
                If CType(txtOdometer.Text, Integer) < CType(oWSCHeader.Miliage, Integer) Then
                    MessageBox.Show("Jarak tempuh tidak boleh kurang dari jarak tempuh sebelumnya")
                    Return False
                End If
            End If

            Dim tglPerbaikan As Date = icTglPerbaikan.Value
            Dim tglClaim As Date = oWSCHeader.CreatedTime

            'repair - today > 14 hari
            Dim RepMinToday As TimeSpan = oWSCHeader.ServiceDate - DateTime.Now


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
            If Not IsNothing(oChassisMasterPKT.PKTDate) Then
                Dim repServicePKT As TimeSpan = icTglPerbaikan.Value - oChassisMasterPKT.PKTDate
                If repServicePKT.Days < 0 Then
                    MessageBox.Show("Tanggal Perbaikan tidak boleh kurang dari tanggal PKT")
                    Return False
                End If
            ElseIf Not IsNothing(oChassisMasterPKT.ChassisMaster.EndCustomer.FakturDate) Then
                Dim repServicePKT As TimeSpan = icTglPerbaikan.Value - oChassisMasterPKT.ChassisMaster.EndCustomer.FakturDate
                If repServicePKT.Days < 0 Then
                    MessageBox.Show("Tanggal Perbaikan tidak boleh kurang dari tanggal PKT")
                    Return False
                End If
            ElseIf Not IsNothing(oChassisMasterPKT.ChassisMaster.EndCustomer.OpenFakturDate) Then
                Dim repServicePKT As TimeSpan = icTglPerbaikan.Value - oChassisMasterPKT.ChassisMaster.EndCustomer.OpenFakturDate
                If repServicePKT.Days < 0 Then
                    MessageBox.Show("Tanggal Perbaikan tidak boleh kurang dari tanggal PKT")
                    Return False
                End If
            End If
        End If

        If txtGejala.Text = String.Empty AndAlso ddlClaimType.SelectedValue <> "Z4" Then
            MessageBox.Show("Silahkan masukan Gejala Kerusakan")
            Return False
        End If
        If txtPemeriksaan.Text = String.Empty AndAlso ddlClaimType.SelectedValue <> "Z4" Then
            MessageBox.Show("Silahkan masukan Catatan Pemeriksaan")
            Return False
        End If
        If txtHasil.Text = String.Empty AndAlso ddlClaimType.SelectedValue <> "Z4" Then
            MessageBox.Show("Silahkan masukan Catatan Perbaikan")
            Return False
        End If

        '=============================================================================================================

        Dim intVechileTypeID1 As Integer
        Dim intVechileTypeID2 As Integer

        If ddlRefDoc.SelectedValue = "1" Then 'PQR Ref Doc
            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(PQRHeader), "RowStatus", MatchType.No, CType(DBRowStatus.Deleted, Short)))
            criterias2.opAnd(New Criteria(GetType(PQRHeader), "PQRNo", MatchType.Exact, txtPQRNo.Text.Trim))
            Dim _arrPQR As ArrayList = New PQRHeaderFacade(User).Retrieve(criterias2)
            If _arrPQR.Count > 0 Then
                Dim objPQRHeader As PQRHeader = CType(_arrPQR(0), PQRHeader)
                intVechileTypeID1 = objPQRHeader.ChassisMaster.VechileColor.VechileType.ID
            End If
        End If

        Dim strChassisNumber As String = txtNoChasis.Text.Trim()
        Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
        Dim criterias As New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, strChassisNumber))
        criterias.opAnd(New Criteria(GetType(ChassisMaster), "Category.ProductCategory.Code", MatchType.Exact, companyCode))
        Dim ChassisColl As ArrayList = New ChassisMasterFacade(User).Retrieve(criterias)
        If ChassisColl.Count > 0 Then
            oChassis = CType(ChassisColl(0), ChassisMaster)
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

        If Not String.IsNullorEmpty(txtDealerBranchCode.Text) Then
            Dim branchCriteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DealerBranch), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            branchCriteria.opAnd(New Criteria(GetType(DealerBranch), "DealerBranchCode", MatchType.Exact, txtDealerBranchCode.Text))
            Dim branchList As ArrayList = New DealerBranchFacade(HttpContext.Current.User).Retrieve(branchCriteria)
            If branchList.Count = 0 Then
                ' Dealer Branch Found
                MessageBox.Show("Cabang tidak ditemukan")
                Return False
            End If
        End If

        'If dgOngkosKerja.Items.Count < 1 AndAlso ddlRefDoc.SelectedIndex = 1 Then
        If dgOngkosKerja.Items.Count < 1 Then
            MessageBox.Show("Ongkos kerja harus di isi")
            Return False
        End If

        If Mode = enumMode.Mode.NewItemMode Then
            'Dim arrOngKer As ArrayList = CType(sessHelper.GetSession("NEW_ONGKOSKERJA"), ArrayList)
            Dim arrOngKer As ArrayList = CType(GetSession("NEW_ONGKOSKERJA"), ArrayList)
            If arrOngKer.Count = 0 AndAlso ddlRefDoc.SelectedIndex <> 1 Then
                MessageBox.Show("Data Ongkos Kerja harus diisi")
                Return False
            End If
            'Dim arrParts As ArrayList = CType(sessHelper.GetSession("NEW_ONGKOSPARTS"), ArrayList)
            'If arrParts.Count = 0 AndAlso ddlRefDoc.SelectedIndex <> 1 Then
            '    MessageBox.Show("Data Parts harus diisi")
            '    Return False
            'End If
        Else
            'Dim arrOngKer As ArrayList = CType(sessHelper.GetSession("ONGKOSKERJA"), ArrayList)
            Dim arrOngKer As ArrayList = CType(GetSession("ONGKOSKERJA"), ArrayList)
            If arrOngKer.Count = 0 AndAlso ddlRefDoc.SelectedIndex <> 1 Then
                MessageBox.Show("Data Ongkos Kerja harus diisi")
                Return False
            End If
            'Dim arrParts As ArrayList = CType(sessHelper.GetSession("ONGKOSPARTS"), ArrayList)
            'If arrParts.Count = 0 AndAlso ddlRefDoc.SelectedIndex <> 1 Then
            '    MessageBox.Show("Data Parts harus diisi")
            '    Return False
            'End If
        End If

        '========= Validasi Attacment tipe Kwitansi =======================================================================
        'Dim _arrONGKOSKERJA As ArrayList = CType(sessHelper.GetSession("ONGKOSKERJA"), ArrayList)
        Dim _arrONGKOSKERJA As ArrayList = CType(GetSession("ONGKOSKERJA"), ArrayList)
        If Mode = enumMode.Mode.NewItemMode Then
            '_arrONGKOSKERJA = CType(sessHelper.GetSession("NEW_ONGKOSKERJA"), ArrayList)
            _arrONGKOSKERJA = CType(GetSession("NEW_ONGKOSKERJA"), ArrayList)
        End If
        Dim blnIsExistPosition99 As Boolean = False
        Dim blnIsExistPosXEE999 As Boolean = False
        Dim blnIsExistWork90 As Boolean = False
        For Each _objWSCDetail As WSCDetail In _arrONGKOSKERJA
            If _objWSCDetail.WorkCode = "99" Then
                'If _objWSCDetail.WorkCode = "99" OrElse _objWSCDetail.WorkCode = "90" Then
                blnIsExistPosition99 = True
                Exit For
            End If
            If _objWSCDetail.PositionCode = "XEE999" Then
                blnIsExistPosXEE999 = True
                Exit For
            End If
            If _objWSCDetail.WorkCode = "90" Then
                blnIsExistWork90 = True
                Exit For
            End If
        Next
        'Dim _arrONGKOSPART As ArrayList = CType(sessHelper.GetSession("ONGKOSPARTS"), ArrayList)
        Dim _arrONGKOSPART As ArrayList = CType(GetSession("ONGKOSPARTS"), ArrayList)
        If Mode = enumMode.Mode.NewItemMode Then
            '_arrONGKOSPART = CType(sessHelper.GetSession("NEW_ONGKOSPARTS"), ArrayList)
            _arrONGKOSPART = CType(GetSession("NEW_ONGKOSPARTS"), ArrayList)
        End If
        Dim blnIsExistPartNPN7 As Boolean = False
        For Each _objWSCDetail As WSCDetail In _arrONGKOSPART
            If Not IsNothing(_objWSCDetail.SparePartMaster) Then
                If _objWSCDetail.SparePartMaster.PartNumber.Trim = "NPN7" Then
                    blnIsExistPartNPN7 = True
                    Exit For
                End If
            End If
        Next

        'Dim _arrWSCEVIDENCE As ArrayList = CType(sessHelper.GetSession("WSCEVIDENCE"), ArrayList)
        Dim _arrWSCEVIDENCE As ArrayList = CType(GetSession("WSCEVIDENCE"), ArrayList)
        If Mode = enumMode.Mode.NewItemMode Then
            '_arrWSCEVIDENCE = CType(sessHelper.GetSession("NEW_WSCEVIDENCE"), ArrayList)
            _arrWSCEVIDENCE = CType(GetSession("NEW_WSCEVIDENCE"), ArrayList)
        End If
        Dim blnIsNotExistAttachFile As Boolean = False
        If _arrWSCEVIDENCE.Count <= 0 Then blnIsNotExistAttachFile = True
        For Each _objWSCEVIDENCE As WSCEvidence In _arrWSCEVIDENCE
            If _objWSCEVIDENCE.EvidenceType = EnumWSCEvidenceType.WSCEvidenceType.KWITANSI_WSC Then 'Type Kwitansi
                If IsNothing(_objWSCEVIDENCE.AttachmentData) Then
                    If _objWSCEVIDENCE.PathFile.Trim = String.Empty Then
                        blnIsNotExistAttachFile = True
                        Exit For
                    End If
                End If
            End If
        Next
        If (blnIsExistPosition99 Or blnIsExistPartNPN7 Or (blnIsExistWork90 And ddlClaimType.SelectedValue = "Z4")) And blnIsNotExistAttachFile = True Then
            MessageBox.Show("Lampiran Tipe Kwitansi masih kosong")
            Return False
        End If

        'Validasi Main Part
        Dim intJumlahMainPart As Integer = 0
        For Each _objWSCDetail As WSCDetail In _arrONGKOSPART
            If _objWSCDetail.MainPart = 1 Then
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

        If ddlClaimType.SelectedValue = "ZA" OrElse ddlClaimType.SelectedValue = "Z2" OrElse ddlClaimType.SelectedValue = "ZB" Then
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

        If ddlClaimType.SelectedValue = "ZB" Then
            'txtNoChasis.Text
            'txtOdometer.Text
            Dim mspExReg As MSPExRegistration = New MSPExRegistrationFacade(User).RetrieveByChassisNumber(txtNoChasis.Text.Trim)
            If mspExReg.ID = 0 Then
                MessageBox.Show("Nomor Rangka Tidak Terdaftar MSP Extended")
                Return False
            Else
                If mspExReg.WarrantyValidDateTo < Date.Now.Date Then
                    MessageBox.Show("Masa warranty untuk Chassis ini dengan Claim Tipe ZB sudah lewat ")
                    Return False
                End If

                If mspExReg.WarrantyValidKMTo < CInt(txtOdometer.Text.Trim) Then
                    MessageBox.Show("Jarak warranty untuk Chassis ini dengan Claim Tipe ZB sudah habis ")
                    Return False
                End If
            End If

            Dim WSCValidationMsg As String = String.Empty
            If intViewStateMode = enumMode.Mode.NewItemMode Then
                Dim isMSPExtFinish As Boolean = ValidationMSPExt(WSCValidationMsg, txtNoChasis.Text.Trim)
                If Not isMSPExtFinish Then
                    MessageBox.Show(WSCValidationMsg)
                    Return False
                End If
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
                Dim oWSCDetail As WSCDetail = New WSCDetail
                If (Mode = enumMode.Mode.NewItemMode OrElse Mode = enumMode.Mode.EditMode) AndAlso txtPosition.Text.Trim.ToUpper <> "XEE999" Then
                    If ChassisKodePosWorkCodKiloIsExist(txtNoChasis.Text, txtPosition.Text, txtWokCOde.Text, txtOdometer.Text, oWSCDetail) Then
                        MessageBox.Show("Data Claim sudah pernah diinputkan di dealer " & oWSCDetail.WSCHeader.Dealer.DealerCode & "\n dengan claim number " & oWSCDetail.WSCHeader.ClaimNumber)
                        Return False
                    End If
                End If
                arlKodePOsisi.Add(txtPosition.Text.Trim.Substring(0, 2))

                If Not validateKodeKerusakan(txtPosition.Text.Trim.ToUpper, ddlKodeWSCA.SelectedValue, "A") Then
                    returnKodeKerusakan = False
                    If reasonKodeKerusakan.Length > 0 Then
                        reasonKodeKerusakan += ";\nKode A " & ddlKodeWSCA.SelectedValue & " tidak valid untuk posisi " & txtPosition.Text.Trim.ToUpper
                    Else
                        reasonKodeKerusakan += "Kode A " & ddlKodeWSCA.SelectedValue & " tidak valid untuk posisi " & txtPosition.Text.Trim.ToUpper
                    End If
                End If
                If Not validateKodeKerusakan(txtPosition.Text.Trim.ToUpper, ddlKodeWSCB.SelectedValue, "B") Then
                    returnKodeKerusakan = False
                    If reasonKodeKerusakan.Length > 0 Then
                        reasonKodeKerusakan += ";\nKode B " & ddlKodeWSCB.SelectedValue & " tidak valid untuk posisi " & txtPosition.Text.Trim.ToUpper
                    Else
                        reasonKodeKerusakan += "Kode B " & ddlKodeWSCB.SelectedValue & " tidak valid untuk posisi " & txtPosition.Text.Trim.ToUpper
                    End If
                End If
            End If
        Next
        If returnKodeKerusakan = False Then
            MessageBox.Show(reasonKodeKerusakan)
            Return False
        End If

        If ddlClaimType.SelectedValue = "Z4" Then
            GoTo disableValidation
        End If

        If Not ValidateByParameter() Then
            Return False
        End If

disableValidation:

        'Validate PQRNo With or Without XEE999
        If Mode = enumMode.Mode.NewItemMode AndAlso ddlClaimType.SelectedIndex = 1 AndAlso ddlRefDoc.SelectedIndex = 2 Then
            If Not validatePQRAndXEE999() Then
                MessageBox.Show("Penggunaan refensi Nomor PQR yang sama hanya valid untuk pengajuan Claim Ongkos Kirim")
                Return False
            End If
        End If

        Return True

    End Function

    Private Sub LoadChassisInfo(ByRef obj As ChassisMaster)
        txtNoChasis.Text = obj.ChassisNumber
        hdntxtNoChasis.Value = txtNoChasis.Text
        hdnVechileTypeId.Value = obj.VechileColor.VechileType.ID
        lblNoChasisVal.Text = obj.ChassisNumber
        lblNoMesinVal.Text = obj.EngineNumber

        'sessHelper.SetSession("objChasMas", obj)
        SetSession("objChasMas", obj)
        If obj.AlreadySaled = 0 Then
            lblAlreadySaled.Text = "Belum Terjual"
        ElseIf obj.AlreadySaled = 2 Then
            lblAlreadySaled.Text = "Sudah Terjual"
        End If

        If IsNothing(obj.EndCustomer) Then
            lblNamaPemilikVal.Text = ""
            lblTglPKTVal.Text = ""
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
            lblTglPKTVal.Text = ""
            Dim arrChassisMasterPKT As ArrayList = New ArrayList
            Dim criterias As New CriteriaComposite(New Criteria(GetType(ChassisMasterPKT), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ChassisMasterPKT), "ChassisMaster.ID", MatchType.Exact, obj.ID))
            arrChassisMasterPKT = New ChassisMasterPKTFacade(User).Retrieve(criterias)
            If Not IsNothing(arrChassisMasterPKT) And arrChassisMasterPKT.Count > 0 Then
                oChassisMasterPKT = CType(arrChassisMasterPKT(0), ChassisMasterPKT)
                lblTglPKTVal.Text = IIf(oChassisMasterPKT.PKTDate.Year <= 1900, "", oChassisMasterPKT.PKTDate.ToString("dd/MM/yyyy"))
            Else
                If lblTglPKTVal.Text = "" Then
                    lblTglPKTVal.Text = IIf(obj.EndCustomer.FakturDate.Year <= 1900, "", obj.EndCustomer.FakturDate.ToString("dd/MM/yyyy"))
                End If
                If lblTglPKTVal.Text = "" Then
                    lblTglPKTVal.Text = IIf(obj.EndCustomer.OpenFakturDate.Year <= 1900, "", obj.EndCustomer.OpenFakturDate.ToString("dd/MM/yyyy"))
                End If
            End If
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
        hdnVechileTypeId.Value = ""
        lblNoMesinVal.Text = ""
        lblAlreadySaled.Text = ""
        lblNamaPemilikVal.Text = ""
        lblTglPKTVal.Text = "" ' Temporaly not available
    End Sub

    Private Sub ReloadForm(ByVal id As Integer)
        'sessHelper.SetSession("WSCHEADER", oWSCHeaderFacade.Retrieve(id))
        SetSession("WSCHEADER", oWSCHeaderFacade.Retrieve(id))
        'oWSCHeader = CType(sessHelper.GetSession("WSCHEADER"), WSCHeader)
        oWSCHeader = CType(GetSession("WSCHEADER"), WSCHeader)

        Dim arrWSCDetailLabor As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(WSCDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(WSCDetail), "WSCHeader.ID", MatchType.Exact, oWSCHeader.ID))
        criterias.opAnd(New Criteria(GetType(WSCDetail), "WSCType", MatchType.Exact, "L"))
        arrWSCDetailLabor = New WSCDetailFacade(User).Retrieve(criterias)
        If IsNothing(arrWSCDetailLabor) And arrWSCDetailLabor.Count = 0 Then
            arrWSCDetailLabor = New ArrayList
        End If

        Dim arrWSCDetailPart As ArrayList = New ArrayList
        Dim criterias2 As New CriteriaComposite(New Criteria(GetType(WSCDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias2.opAnd(New Criteria(GetType(WSCDetail), "WSCHeader.ID", MatchType.Exact, oWSCHeader.ID))
        criterias2.opAnd(New Criteria(GetType(WSCDetail), "WSCType", MatchType.Exact, "P"))
        arrWSCDetailPart = New WSCDetailFacade(User).Retrieve(criterias2)
        If IsNothing(arrWSCDetailPart) And arrWSCDetailPart.Count = 0 Then
            arrWSCDetailPart = New ArrayList
        End If

        'sessHelper.SetSession("ONGKOSKERJA", arrWSCDetailLabor)
        'sessHelper.SetSession("DELETEDONGKOSKERJA", New ArrayList)

        'sessHelper.SetSession("ONGKOSPARTS", arrWSCDetailPart)
        'sessHelper.SetSession("DELETEDPARTS", New ArrayList)

        'sessHelper.SetSession("WSCEVIDENCE", GetAttachmentList(oWSCHeader.WSCEvidences))
        'sessHelper.SetSession("DELETEDWSEVIDENCE", New ArrayList)

        SetSession("ONGKOSKERJA", arrWSCDetailLabor)
        SetSession("DELETEDONGKOSKERJA", New ArrayList)

        SetSession("ONGKOSPARTS", arrWSCDetailPart)
        SetSession("DELETEDPARTS", New ArrayList)

        SetSession("WSCEVIDENCE", GetAttachmentList(oWSCHeader.WSCEvidences))
        SetSession("DELETEDWSEVIDENCE", New ArrayList)

        If CType(ViewState("Mode"), enumMode.Mode) = enumMode.Mode.EditMode Then
            Server.Transfer("~/PQR/FrmWSCHeader.aspx?Mode=Edit")
        ElseIf CType(ViewState("Mode"), enumMode.Mode) = enumMode.Mode.ViewMode Then
            Server.Transfer("~/PQR/FrmWSCHeader.aspx?Mode=View")
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
                For Each obj As WSCEvidence In AttachmentCollection
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

    Private Sub RemoveWSCAttachment(ByVal ObjAttachment As WSCEvidence, ByVal TargetPath As String)
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
                imp.StopImpersonate()
                imp = Nothing
            End If
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
                For Each obj As WSCEvidence In AttachmentCollection
                    If Not IsNothing(obj.AttachmentData) Then
                        finfo = New FileInfo(TargetPath + obj.PathFile)

                        If Not finfo.Directory.Exists Then
                            Directory.CreateDirectory(finfo.DirectoryName)
                        End If
                        obj.AttachmentData.SaveAs(TargetPath + obj.PathFile)
                    End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try


    End Sub

    Private Sub UploadAttachment(ByVal ObjAttachment As WSCEvidence, ByVal TargetPath As String)
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
                    finfo = New FileInfo(TargetPath + ObjAttachment.PathFile) 'targetpath = tempdirectory

                    If Not finfo.Directory.Exists Then
                        Directory.CreateDirectory(finfo.DirectoryName)
                    End If
                    ObjAttachment.AttachmentData.SaveAs(TargetPath + ObjAttachment.PathFile)
                End If
                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            imp.StopImpersonate()
            imp = Nothing
            Throw ex
        End Try
    End Sub

    Private Sub BindWSCEvidenceType(ByVal ddlEvidenceType As DropDownList)
        Dim _enumWSCEvidenceType As New EnumWSCEvidenceType
        Dim _arrTmp As New ArrayList
        _arrTmp = _enumWSCEvidenceType.WSCEvidenceTypeList

        ddlEvidenceType.DataSource = _arrTmp
        ddlEvidenceType.DataTextField = "WSCEvidenceTypeId"
        ddlEvidenceType.DataValueField = "WSCEvidenceTypeValue"
        ddlEvidenceType.DataBind()
        ddlEvidenceType.Items.Insert(0, "Silahkan Pilih")
    End Sub

    Private Sub GetObjectLaborMaster(ByVal strKodePosisi As String, ByVal strWorkCode As String, Optional ByVal vTypeID As Integer = 0)
        objKodePosisi = New DeskripsiPositionCodeFacade(User).Retrieve(strKodePosisi)
        objKodeKerja = New DeskripsiWorkPositionFacade(User).Retrieve(strWorkCode)

        Dim criterias As New CriteriaComposite(New Criteria(GetType(LaborMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(LaborMaster), "LaborCode", MatchType.Exact, objKodePosisi.KodePosition.Trim))
        criterias.opAnd(New Criteria(GetType(LaborMaster), "LaborCode", MatchType.Exact, strKodePosisi))
        'criterias.opAnd(New Criteria(GetType(LaborMaster), "WorkCode", MatchType.Exact, objKodeKerja.KodeKerja.Trim))
        criterias.opAnd(New Criteria(GetType(LaborMaster), "WorkCode", MatchType.Exact, strWorkCode))
        If vTypeID <> 0 Then
            criterias.opAnd(New Criteria(GetType(LaborMaster), "VechileType.ID", MatchType.Exact, vTypeID))
        End If
        arrLaborMaster = New LaborMasterFacade(User).Retrieve(criterias)
        If Not IsNothing(arrLaborMaster) And arrLaborMaster.Count > 0 Then
            objLaborMaster = CType(arrLaborMaster(0), LaborMaster)
        Else
            objLaborMaster = Nothing
        End If
    End Sub

    Private Function GetLaborMaster(strKodePosisi As String, strWorkCode As String, veTypeID As Integer) As LaborMaster
        Dim _return As Integer
        'objKodePosisi = New DeskripsiPositionCodeFacade(User).Retrieve(strKodePosisi)
        'objKodeKerja = New DeskripsiWorkPositionFacade(User).Retrieve(strWorkCode)

        Dim criterias As New CriteriaComposite(New Criteria(GetType(LaborMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(LaborMaster), "LaborCode", MatchType.Exact, strKodePosisi.Trim))
        criterias.opAnd(New Criteria(GetType(LaborMaster), "WorkCode", MatchType.Exact, strWorkCode.Trim))
        criterias.opAnd(New Criteria(GetType(LaborMaster), "VechileType.ID", MatchType.Exact, veTypeID))
        arrLaborMaster = New LaborMasterFacade(User).Retrieve(criterias)
        If Not IsNothing(arrLaborMaster) And arrLaborMaster.Count > 0 Then
            Return CType(arrLaborMaster(0), LaborMaster)
        Else
            objLaborMaster = Nothing
        End If
    End Function

    Private Function LoopDgrid(ByVal SourceGridName As String, ByVal objLaborMaster As LaborMaster, ByVal objSparePartMaster As SparePartMaster, ByVal fromFunc As String, ByVal index As Integer)
        Dim _arrONGKOSKERJA As ArrayList = New ArrayList
        Dim _arrONGKOSKERJATemp As ArrayList = New ArrayList
        Dim _arrONGKOSPARTS As ArrayList = New ArrayList
        Dim _arrONGKOSPARTSTemp As ArrayList = New ArrayList
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        If Mode = enumMode.Mode.NewItemMode Then
            If SourceGridName.ToUpper() = "DGONGKOSKERJA" OrElse SourceGridName.ToUpper() = "ALLGRID" Then
                '_arrONGKOSKERJATemp = CType(sessHelper.GetSession("NEW_ONGKOSKERJA"), ArrayList)
                'sessHelper.RemoveSession("NEW_ONGKOSKERJA")
                'sessHelper.SetSession("NEW_ONGKOSKERJA", New ArrayList)
                '_arrONGKOSKERJA = CType(sessHelper.GetSession("NEW_ONGKOSKERJA"), ArrayList)
                _arrONGKOSKERJATemp = CType(GetSession("NEW_ONGKOSKERJA"), ArrayList)
                RemoveSession("NEW_ONGKOSKERJA")
                SetSession("NEW_ONGKOSKERJA", New ArrayList)
                _arrONGKOSKERJA = CType(GetSession("NEW_ONGKOSKERJA"), ArrayList)
            End If

            If SourceGridName.ToUpper() = "DGPARTS" OrElse SourceGridName.ToUpper() = "ALLGRID" Then
                '_arrONGKOSPARTSTemp = CType(sessHelper.GetSession("NEW_ONGKOSPARTS"), ArrayList)
                'sessHelper.RemoveSession("NEW_ONGKOSPARTS")
                'sessHelper.SetSession("NEW_ONGKOSPARTS", New ArrayList)
                '_arrONGKOSPARTS = CType(sessHelper.GetSession("NEW_ONGKOSPARTS"), ArrayList)
                _arrONGKOSPARTSTemp = CType(GetSession("NEW_ONGKOSPARTS"), ArrayList)
                RemoveSession("NEW_ONGKOSPARTS")
                SetSession("NEW_ONGKOSPARTS", New ArrayList)
                _arrONGKOSPARTS = CType(GetSession("NEW_ONGKOSPARTS"), ArrayList)
            End If
        Else
            If SourceGridName.ToUpper() = "DGONGKOSKERJA" OrElse SourceGridName.ToUpper() = "ALLGRID" Then
                '_arrONGKOSKERJATemp = CType(sessHelper.GetSession("ONGKOSKERJA"), ArrayList)
                'sessHelper.RemoveSession("ONGKOSKERJA")
                'sessHelper.SetSession("ONGKOSKERJA", New ArrayList)
                '_arrONGKOSKERJA = CType(sessHelper.GetSession("ONGKOSKERJA"), ArrayList)
                _arrONGKOSKERJATemp = CType(GetSession("ONGKOSKERJA"), ArrayList)
                RemoveSession("ONGKOSKERJA")
                SetSession("ONGKOSKERJA", New ArrayList)
                _arrONGKOSKERJA = CType(GetSession("ONGKOSKERJA"), ArrayList)
            End If

            If SourceGridName.ToUpper() = "DGPARTS" OrElse SourceGridName.ToUpper() = "ALLGRID" Then
                '_arrONGKOSPARTSTemp = CType(sessHelper.GetSession("ONGKOSPARTS"), ArrayList)
                'sessHelper.RemoveSession("ONGKOSPARTS")
                'sessHelper.SetSession("ONGKOSPARTS", New ArrayList)
                '_arrONGKOSPARTS = CType(sessHelper.GetSession("ONGKOSPARTS"), ArrayList)
                _arrONGKOSPARTSTemp = CType(GetSession("ONGKOSPARTS"), ArrayList)
                RemoveSession("ONGKOSPARTS")
                SetSession("ONGKOSPARTS", New ArrayList)
                _arrONGKOSPARTS = CType(GetSession("ONGKOSPARTS"), ArrayList)
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
                            'If fromFunc = "Add" Then
                            If fromFunc = "Add" OrElse fromFunc = "Edit" Then


                                'If di.ItemType = ListItemType.Item OrElse di.ItemType = ListItemType.AlternatingItem OrElse di.ItemType = ListItemType.Footer Then
                                Dim txtPositionCode As TextBox
                                Dim txtWorkCode As TextBox
                                Dim txtQuantity As TextBox
                                Dim txtAmount As TextBox
                                Dim lblWorkCode As Label
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
                                    lblWorkCode = CType(di.FindControl("lblWorkCode"), Label)
                                End If

                                If txtPositionCode.Text.Trim <> "" Or txtWorkCode.Text.Trim <> "" Then
                                    If txtQuantity.Text.Trim = "" OrElse CType(txtQuantity.Text.Trim, Decimal) = 0 Then
                                        MessageBox.Show("Jumlah ongkos kerja tidak boleh kosong atau nol")
                                        Return False
                                    End If
                                    If txtAmount.Text.Trim = "" Then ' OrElse CType(txtAmount.Text.Trim, Integer) = 0 Then
                                        'txtAmount.Text = 1
                                        txtAmount.Text = 0
                                    End If
                                    If txtPositionCode.Text.Trim.ToUpper = "XEE999" AndAlso txtWorkCode.Text.Trim.ToUpper = "99" Then
                                        blnExistPosCodeXEE999 = True
                                    End If
                                    'If txtPositionCode.Text.Trim.ToUpper = "XEE999" AndAlso txtWorkCode.Text.ToUpper = "99" Then
                                    If di.ItemType = ListItemType.Item OrElse di.ItemType = ListItemType.AlternatingItem Then
                                        If txtAmount.Enabled AndAlso txtAmount.Style.Item("display") = "table-row" Then
                                            If CType(txtAmount.Text.Trim, Integer) = 0 OrElse txtAmount.Text.Trim = "" Then
                                                'txtAmount.Visible = True
                                                dgOngkosKerja.ShowFooter = False
                                                MessageBox.Show("Silahkan input amount Ongkos Kerja !")
                                                Return False
                                            End If
                                        End If

                                        If txtWorkCode.Text.ToUpper = "99" OrElse lblWorkCode.Text.ToUpper = "99" Then
                                            If txtWorkCode.Text.ToUpper = "99" Then
                                                If CType(txtAmount.Text.Trim, Integer) = 0 OrElse txtAmount.Text.Trim = "" Then
                                                    'txtAmount.Visible = True
                                                    dgOngkosKerja.ShowFooter = False
                                                    MessageBox.Show("Silahkan input amount Ongkos Kerja !")
                                                    Return False
                                                End If
                                                lblWorkCode.Text = txtWorkCode.Text
                                            End If
                                        ElseIf (txtWorkCode.Text.Trim.ToUpper = "90" OrElse lblWorkCode.Text.Trim.ToUpper = "90") AndAlso ddlClaimType.SelectedValue = "Z4" Then
                                            If txtWorkCode.Text.Trim.ToUpper = "90" Then
                                                If CType(txtAmount.Text.Trim, Integer) = 0 OrElse txtAmount.Text.Trim = "" Then
                                                    'txtAmount.Visible = True
                                                    dgOngkosKerja.ShowFooter = False
                                                    MessageBox.Show("Silahkan input amount Ongkos Kerja !")
                                                    Return False
                                                End If
                                                lblWorkCode.Text = txtWorkCode.Text.Trim
                                            End If
                                        Else
                                            'txtAmount.Visible = False
                                            'txtAmount.Text = 1
                                            txtAmount.Text = 0
                                            dgOngkosKerja.ShowFooter = True
                                        End If
                                    End If

                                    If di.ItemType = ListItemType.Footer Then
                                        If txtWorkCode.Text.ToUpper = "99" Then
                                            If CType(txtAmount.Text.Trim, Integer) = 0 OrElse txtAmount.Text.Trim = "" Then
                                                'txtAmount.Visible = True
                                                dgOngkosKerja.ShowFooter = False
                                                MessageBox.Show("Silahkan input amount Ongkos Kerja !")
                                                Return False
                                            End If
                                        ElseIf txtWorkCode.Text.Trim.ToUpper = "90" AndAlso ddlClaimType.SelectedValue = "Z4" Then
                                            If CType(txtAmount.Text.Trim, Integer) = 0 OrElse txtAmount.Text.Trim = "" Then
                                                'txtAmount.Visible = True
                                                dgOngkosKerja.ShowFooter = False
                                                MessageBox.Show("Silahkan input amount Ongkos Kerja !")
                                                Return False
                                            End If
                                        Else
                                            'txtAmount.Visible = False
                                            'txtAmount.Text = 1
                                            txtAmount.Text = 0
                                            dgOngkosKerja.ShowFooter = True
                                        End If
                                    End If

                                    If (ddlClaimType.SelectedValue = "ZA" OrElse ddlClaimType.SelectedValue = "Z2" OrElse ddlClaimType.SelectedValue = "ZB" OrElse ddlClaimType.SelectedValue = "Z6") Then
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
                                    insertSessionOngKerja(lblID.Text, txtPositionCode.Text, txtWorkCode.Text, objLaborMaster, txtQuantity.Text, txtAmount.Text, _arrONGKOSKERJA)
                                    i += 1
                                End If

                            Else
                                If di.ItemType = ListItemType.Item OrElse di.ItemType = ListItemType.AlternatingItem Then
                                    Dim txtPostionCodeItem As TextBox = CType(di.FindControl("txtPostionCodeItem"), TextBox)
                                    Dim txtWorkCodeItem As TextBox = CType(di.FindControl("txtWorkCodeItem"), TextBox)
                                    Dim txtQuantityItem As TextBox = CType(di.FindControl("txtQuantityItem"), TextBox)
                                    Dim txtAmountItem As TextBox = CType(di.FindControl("txtAmountItem"), TextBox)
                                    Dim lblID As Label = CType(di.FindControl("lblIDItem"), Label)
                                    insertSessionOngKerja(lblID.Text, txtPostionCodeItem.Text, txtWorkCodeItem.Text, objLaborMaster, txtQuantityItem.Text, txtAmountItem.Text, _arrONGKOSKERJA)
                                End If
                            End If
                        End If

                        If fromFunc = "Simpan" Then
                            Dim txtPostionCode As TextBox
                            Dim txtWorkCode As TextBox
                            Dim txtQuantity As TextBox
                            Dim txtAmount As TextBox
                            Dim lblWorkCode As Label
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
                                lblWorkCode = CType(di.FindControl("lblWorkCode"), Label)
                                If txtPostionCode.Text.Trim <> "" AndAlso txtWorkCode.Text.Trim <> "" Then

                                    If CType(txtQuantity.Text.Trim, Integer) = 0 OrElse txtQuantity.Text.Trim = "" Then
                                        MessageBox.Show("Silahkan input Jumlah Ongkos Kerja dahulu")
                                        Return False
                                    End If

                                    'If txtPostionCode.Text.Trim.ToUpper = "XEE999" AndAlso txtWorkCode.Text.Trim.ToUpper = "99" _
                                    If (txtWorkCode.Text.Trim.ToUpper = "99" OrElse txtWorkCode.Text.Trim.ToUpper = "90" _
                                        OrElse lblWorkCode.Text.Trim.ToUpper = "99" OrElse lblWorkCode.Text.Trim.ToUpper = "90") _
                                            AndAlso (CType(txtAmount.Text.Trim, Integer) = 1 OrElse txtAmount.Text.Trim = "") _
                                            AndAlso ddlClaimType.SelectedValue <> "Z2" Then
                                        txtAmount.Attributes("style") = "display:table-row"
                                        MessageBox.Show("Silahkan input Harga Ongkos Kerja dahulu")
                                        Return False
                                    End If

                                    If txtWorkCode.Text.Trim.ToUpper = "99" AndAlso (ddlClaimType.SelectedValue = "ZA" OrElse ddlClaimType.SelectedValue = "Z2" OrElse ddlClaimType.SelectedValue = "ZB") Then
                                        If txtAmount.Text.Trim = "" OrElse CType(txtAmount.Text.Trim, Integer) = 0 Then
                                            txtAmount.Attributes("style") = "display:table-row"
                                            MessageBox.Show("Silahkan input Harga Ongkos Kerja dahulu")
                                            Return False
                                        End If
                                    End If
                                End If
                            End If

                            'Dim arrLi As ArrayList = CType(sessHelper.GetSession("NEW_ONGKOSKERJA"), ArrayList)
                            Dim arrLi As ArrayList = CType(GetSession("NEW_ONGKOSKERJA"), ArrayList)
                            If Mode = enumMode.Mode.EditMode Then
                                'arrLi = CType(sessHelper.GetSession("ONGKOSKERJA"), ArrayList)
                                arrLi = CType(GetSession("ONGKOSKERJA"), ArrayList)
                            End If

                            If Not IsNothing(txtPostionCode) AndAlso Not IsNothing(txtWorkCode) Then
                                If (txtPostionCode.Text.Trim <> "" Or txtWorkCode.Text.Trim <> "") AndAlso di.ItemType <> ListItemType.Footer Then
                                    If (txtPostionCode.Text.Trim + txtWorkCode.Text.Trim).Length < 8 Then
                                        MessageBox.Show("Data pada ongkos kerja kurang lengkap")
                                        Return False
                                    End If
                                    i = 1
                                    For Each ArrWSC As WSCDetail In arrLi
                                        If i <> index Then
                                            If (ArrWSC.PositionCode + ArrWSC.WorkCode).ToString = (txtPostionCode.Text + txtWorkCode.Text).ToString Then
                                                MessageBox.Show("Ada kode posisi dan kode kerja yang sama")
                                                Return False
                                            End If
                                            If (ArrWSC.PositionCode + ArrWSC.WorkCode).ToString.ToUpper = "XEE999" Then
                                                MessageBox.Show("Kode Posisi XEE999 tidak boleh digabung \ndengan kode posisi yang lain")
                                                Return False
                                            End If
                                        End If
                                        i += 1
                                    Next
                                End If
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
                    Dim oOngkosKerja As WSCDetail = CType(_arrONGKOSKERJA(index), WSCDetail)
                    If oOngkosKerja.ID > 0 Then
                        Dim deletedArrLst As ArrayList
                        'deletedArrLst = CType(sessHelper.GetSession("DELETEDONGKOSKERJA"), ArrayList)
                        deletedArrLst = CType(GetSession("DELETEDONGKOSKERJA"), ArrayList)
                        deletedArrLst.Add(oOngkosKerja)
                        'sessHelper.SetSession("DELETEDONGKOSKERJA", deletedArrLst)
                        SetSession("DELETEDONGKOSKERJA", deletedArrLst)
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
                        Dim txtKodePartsItem As TextBox
                        Dim txtKodePartsFooter As TextBox
                        Dim txtQty As TextBox
                        Dim txtPartPrice As TextBox
                        Dim txtFakturNumberFooter As TextBox
                        Dim lblFakturNumber As Label
                        Dim FakturNumber As String
                        Dim cbMainPart As CheckBox
                        Dim lblID As Label
                        'If fromFunc = "Add" OrElse fromFunc = "Simpan" Then
                        If fromFunc = "Add" Then
                            If di.ItemType = ListItemType.Item OrElse di.ItemType = ListItemType.AlternatingItem OrElse di.ItemType = ListItemType.Footer Then
                                If (di.ItemType = ListItemType.Footer) Then
                                    txtKodePartsFooter = CType(di.FindControl("txtKodePartsFooter"), TextBox)
                                    KodePart = txtKodePartsFooter.Text
                                    txtQty = CType(di.FindControl("txtQtyFooter"), TextBox)
                                    txtPartPrice = CType(di.FindControl("txtPartPriceFooter"), TextBox)
                                    lblID = CType(di.FindControl("lblIDFooter"), Label)
                                    cbMainPart = CType(di.FindControl("cbMainPartFooter"), CheckBox)
                                    txtFakturNumberFooter = CType(di.FindControl("txtFakturNumberFooter"), TextBox)
                                    FakturNumber = txtFakturNumberFooter.Text
                                ElseIf di.ItemType = ListItemType.Item OrElse di.ItemType = ListItemType.AlternatingItem Then
                                    lblKodeParts = CType(di.FindControl("lblKodeParts"), Label)
                                    KodePart = lblKodeParts.Text
                                    txtQty = CType(di.FindControl("txtQtyItem"), TextBox)
                                    txtPartPrice = CType(di.FindControl("txtPartPriceItem"), TextBox)
                                    lblID = CType(di.FindControl("lblIDItem"), Label)
                                    cbMainPart = CType(di.FindControl("cbMainPartItem"), CheckBox)
                                    lblFakturNumber = CType(di.FindControl("lblFakturNumber"), Label)
                                    FakturNumber = lblFakturNumber.Text
                                End If

                                If KodePart.Trim <> "" Then
                                    If Not IsNothing(_arrONGKOSPARTS) AndAlso _arrONGKOSPARTS.Count > 0 Then
                                        If Mode = enumMode.Mode.NewItemMode Then
                                            '_arrONGKOSPARTSTemp = CType(sessHelper.GetSession("NEW_ONGKOSPARTS"), ArrayList)
                                            _arrONGKOSPARTSTemp = CType(GetSession("NEW_ONGKOSPARTS"), ArrayList)
                                        Else
                                            '_arrONGKOSPARTSTemp = CType(sessHelper.GetSession("ONGKOSPARTS"), ArrayList)
                                            _arrONGKOSPARTSTemp = CType(GetSession("ONGKOSPARTS"), ArrayList)
                                        End If
                                    End If

                                    objSparePartMaster = New SparePartMasterFacade(User).Retrieve(KodePart.Trim())
                                    If Not IsNothing(objSparePartMaster) OrElse objSparePartMaster.ID <> 0 Then
                                        If PartsCodeIsExist(objSparePartMaster.ID, _arrONGKOSPARTS) Then
                                            If Mode = enumMode.Mode.NewItemMode Then
                                                'sessHelper.SetSession("NEW_ONGKOSPARTS", _arrONGKOSPARTSTemp)
                                                SetSession("NEW_ONGKOSPARTS", _arrONGKOSPARTSTemp)
                                            Else
                                                'sessHelper.SetSession("ONGKOSPARTS", _arrONGKOSPARTSTemp)
                                                SetSession("ONGKOSPARTS", _arrONGKOSPARTSTemp)
                                            End If
                                            MessageBox.Show(SR.DataIsExist("Kode Part"))
                                            Return False
                                        End If
                                    Else
                                        If Mode = enumMode.Mode.NewItemMode Then
                                            'sessHelper.SetSession("NEW_ONGKOSPARTS", _arrONGKOSPARTSTemp)
                                            SetSession("NEW_ONGKOSPARTS", _arrONGKOSPARTSTemp)
                                        Else
                                            'sessHelper.SetSession("ONGKOSPARTS", _arrONGKOSPARTSTemp)
                                            SetSession("ONGKOSPARTS", _arrONGKOSPARTSTemp)
                                        End If
                                        MessageBox.Show(SR.DataNotFound("Kode Part"))
                                        Return False
                                    End If

                                    'If txtQty.Text.Trim = "" Then
                                    '    txtQty.Text = 0
                                    'End If
                                    'If txtQty.Text.Trim = "" OrElse CType(txtQty.Text.Trim, Decimal) = 0 Then
                                    '    MessageBox.Show("Jumlah ongkos part tidak boleh kosong atau nol")
                                    '    Return False
                                    'End If

                                    If txtPartPrice.Text.Trim = "" Then
                                        txtPartPrice.Text = 0
                                    End If

                                    If KodePart.ToUpper.Trim = "NPN7" AndAlso (txtPartPrice.Text.Trim = "" OrElse CType(txtPartPrice.Text.Trim, Integer) = 0) Then
                                        If Mode = enumMode.Mode.NewItemMode Then
                                            'sessHelper.SetSession("NEW_ONGKOSPARTS", _arrONGKOSPARTSTemp)
                                            SetSession("NEW_ONGKOSPARTS", _arrONGKOSPARTSTemp)
                                        Else
                                            'sessHelper.SetSession("ONGKOSPARTS", _arrONGKOSPARTSTemp)
                                            SetSession("ONGKOSPARTS", _arrONGKOSPARTSTemp)
                                        End If
                                        MessageBox.Show("Silahkan input Harga Item Part")
                                        Return False
                                    End If

                                    If (txtQty.Text.Trim = "" OrElse CType(txtQty.Text.Trim, Integer) = 0) AndAlso (KodePart.ToString.Trim <> "" OrElse CType(txtQty.Text.Trim, Integer) = 0) Then
                                        'If KodePart = "NPN7" Then
                                        If Mode = enumMode.Mode.NewItemMode Then
                                            'sessHelper.SetSession("NEW_ONGKOSPARTS", _arrONGKOSPARTSTemp)
                                            SetSession("NEW_ONGKOSPARTS", _arrONGKOSPARTSTemp)
                                        Else
                                            'sessHelper.SetSession("ONGKOSPARTS", _arrONGKOSPARTSTemp)
                                            SetSession("ONGKOSPARTS", _arrONGKOSPARTSTemp)
                                        End If

                                        MessageBox.Show("Silahkan input Jumlah Item Part")
                                        Return False
                                        'End If
                                    End If

                                    If cbMainPart.Checked = True Then
                                        intMainPart = 1
                                    Else
                                        intMainPart = 0
                                    End If

                                    'If _arrONGKOSPARTS.Count = 0 Then
                                    '    _arrONGKOSPARTS = _arrONGKOSPARTSTemp
                                    'End If

                                    If FakturNumber.Trim = "" Then
                                        _arrONGKOSPARTS = insertSessionOngParts(lblID.Text, objSparePartMaster, txtQty.Text, txtPartPrice.Text, intMainPart, _arrONGKOSPARTS)
                                    Else
                                        _arrONGKOSPARTS = insertSessionOngParts(lblID.Text, objSparePartMaster, txtQty.Text, txtPartPrice.Text, intMainPart, _arrONGKOSPARTS, FakturNumber)
                                    End If
                                End If
                            End If
                        Else
                            If di.ItemType = ListItemType.Item OrElse di.ItemType = ListItemType.AlternatingItem Then
                                lblKodeParts = CType(di.FindControl("lblKodeParts"), Label)
                                txtKodePartsItem = CType(di.FindControl("txtKodePartsItem"), TextBox)
                                txtQty = CType(di.FindControl("txtQtyItem"), TextBox)
                                txtPartPrice = CType(di.FindControl("txtPartPriceItem"), TextBox)
                                lblID = CType(di.FindControl("lblIDItem"), Label)
                                cbMainPart = CType(di.FindControl("cbMainPartItem"), CheckBox)
                                lblFakturNumber = CType(di.FindControl("lblFakturNumber"), Label)


                                'If _arrONGKOSPARTS.Count = 0 Then
                                '    _arrONGKOSPARTS = _arrONGKOSPARTSTemp
                                'End If

                                objSparePartMaster = New SparePartMasterFacade(User).Retrieve(txtKodePartsItem.Text.Trim())
                                If Not IsNothing(objSparePartMaster) OrElse objSparePartMaster.ID <> 0 Then
                                    If PartsCodeIsExist(objSparePartMaster.ID, _arrONGKOSPARTS) Then
                                        If Mode = enumMode.Mode.NewItemMode Then
                                            'sessHelper.SetSession("NEW_ONGKOSPARTS", _arrONGKOSPARTSTemp)
                                            SetSession("NEW_ONGKOSPARTS", _arrONGKOSPARTSTemp)
                                        Else
                                            'sessHelper.SetSession("ONGKOSPARTS", _arrONGKOSPARTSTemp)
                                            SetSession("ONGKOSPARTS", _arrONGKOSPARTSTemp)
                                        End If
                                        MessageBox.Show(SR.DataIsExist("Kode Part"))
                                        Return False
                                    End If
                                Else
                                    If Mode = enumMode.Mode.NewItemMode Then
                                        'sessHelper.SetSession("NEW_ONGKOSPARTS", _arrONGKOSPARTSTemp)
                                        SetSession("NEW_ONGKOSPARTS", _arrONGKOSPARTSTemp)
                                    Else
                                        'sessHelper.SetSession("ONGKOSPARTS", _arrONGKOSPARTSTemp)
                                        SetSession("ONGKOSPARTS", _arrONGKOSPARTSTemp)
                                    End If
                                    MessageBox.Show(SR.DataNotFound("Kode Part"))
                                    Return False
                                End If


                                If (txtQty.Text.Trim = "" OrElse CType(txtQty.Text.Trim, Integer) = 0) Then
                                    'If KodePart = "NPN7" Then
                                    If Mode = enumMode.Mode.NewItemMode Then
                                        'sessHelper.SetSession("NEW_ONGKOSPARTS", _arrONGKOSPARTSTemp)
                                        SetSession("NEW_ONGKOSPARTS", _arrONGKOSPARTSTemp)
                                    Else
                                        'sessHelper.SetSession("ONGKOSPARTS", _arrONGKOSPARTSTemp)
                                        SetSession("ONGKOSPARTS", _arrONGKOSPARTSTemp)
                                    End If

                                    MessageBox.Show("Silahkan input Jumlah Item Part")
                                    Return False
                                    'End If
                                End If

                                If cbMainPart.Checked = True Then
                                    intMainPart = 1
                                Else
                                    intMainPart = 0
                                End If

                                If IsNothing(lblFakturNumber.Text.Trim = "") Then
                                    _arrONGKOSPARTS = insertSessionOngParts(lblID.Text, objSparePartMaster, txtQty.Text, txtPartPrice.Text, intMainPart, _arrONGKOSPARTS)
                                Else
                                    _arrONGKOSPARTS = insertSessionOngParts(lblID.Text, objSparePartMaster, txtQty.Text, txtPartPrice.Text, intMainPart, _arrONGKOSPARTS, lblFakturNumber.Text)
                                End If
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
                    Dim oOngkosParts As WSCDetail = CType(_arrONGKOSPARTS(index), WSCDetail)
                    If oOngkosParts.ID > 0 Then
                        Dim deletedArrLst As ArrayList
                        'deletedArrLst = CType(sessHelper.GetSession("DELETEDPARTS"), ArrayList)
                        deletedArrLst = CType(GetSession("DELETEDPARTS"), ArrayList)
                        deletedArrLst.Add(oOngkosParts)
                        'sessHelper.SetSession("DELETEDPARTS", deletedArrLst)
                        SetSession("DELETEDPARTS", deletedArrLst)
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

        Dim objWSCDetail As WSCDetail = New WSCDetail
        objWSCDetail.ID = intID
        objWSCDetail.WSCType = "L"
        objWSCDetail.PositionCode = positionCode
        objWSCDetail.WorkCode = workCode
        objWSCDetail.LaborMaster = laborMaster
        objWSCDetail.SparePartMaster = Nothing
        objWSCDetail.Quantity = quantity
        objWSCDetail.PartPrice = amount
        If Not IsNothing(objLaborMaster) Then
            objWSCDetail.MainPart = 1
        Else
            objWSCDetail.MainPart = 0
        End If

        _arrONGKOSKERJA.Add(objWSCDetail)

        Mode = CType(ViewState("Mode"), enumMode.Mode)
        If Mode = enumMode.Mode.NewItemMode Then
            'sessHelper.SetSession("NEW_ONGKOSKERJA", _arrONGKOSKERJA)
            SetSession("NEW_ONGKOSKERJA", _arrONGKOSKERJA)
        Else
            'sessHelper.SetSession("ONGKOSKERJA", _arrONGKOSKERJA)
            SetSession("ONGKOSKERJA", _arrONGKOSKERJA)
        End If

        Return _arrONGKOSKERJA
    End Function

    Private Function insertSessionOngParts(ByVal intID As String, ByVal objSparePartMaster As SparePartMaster, _
                                    ByVal quantity As String, ByVal amount As String, ByVal chkMainPart As Integer, ByVal _arrONGKOSPARTS As ArrayList, Optional ByVal fakturNumber As String = "0") As ArrayList

        intID = IIf(intID.Trim = "", 0, intID)

        Dim objWSCDetail As WSCDetail = New WSCDetail
        objWSCDetail.ID = intID
        objWSCDetail.WSCType = "P"
        objWSCDetail.LaborMaster = Nothing
        objWSCDetail.SparePartMaster = objSparePartMaster
        objWSCDetail.Quantity = quantity
        objWSCDetail.PartPrice = amount
        objWSCDetail.MainPart = chkMainPart
        objWSCDetail.FakturNumber = fakturNumber

        _arrONGKOSPARTS.Add(objWSCDetail)

        Mode = CType(ViewState("Mode"), enumMode.Mode)
        If Mode = enumMode.Mode.NewItemMode Then
            'sessHelper.SetSession("NEW_ONGKOSPARTS", _arrONGKOSPARTS)
            SetSession("NEW_ONGKOSPARTS", _arrONGKOSPARTS)
        Else
            'sessHelper.SetSession("ONGKOSPARTS", _arrONGKOSPARTS)
            SetSession("ONGKOSPARTS", _arrONGKOSPARTS)
        End If

        Return _arrONGKOSPARTS
    End Function

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.ClientScript.GetPostBackEventReference(Me, String.Empty)
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        'oDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        oDealer = CType(GetSession("DEALER"), Dealer)
        Try
            intViewStateMode = Request.QueryString("viewStateMode")
            'sessHelper.SetSession("VIEWSTATEMODE", intViewStateMode)
            SetSession("VIEWSTATEMODE", intViewStateMode)
        Catch
        End Try
        Try
            screenFrom = Request.QueryString("screenFrom")
            'sessHelper.SetSession("SCREENFROM", intViewStateMode)
            SetSession("SCREENFROM", intViewStateMode)
        Catch
        End Try
        Try
            pqrId = CType(Request.QueryString("PQRId").ToString, Integer)
            'sessHelper.SetSession("PQRID", intViewStateMode)
            SetSession("PQRID", intViewStateMode)
        Catch
        End Try
        Try
            wscId = CType(Request.QueryString("WSCId").ToString, Integer)
            'sessHelper.SetSession("WSCID", intViewStateMode)
            SetSession("WSCID", intViewStateMode)
        Catch
        End Try

        InitiateAuthorization()
        lblPopUpDealerBranch.Attributes("onclick") = "ShowPPDealerBranchSelection();"
        lnkbtnPopUpInfoDokumen.Attributes("onClick") = "ShowPPInfoDokumenSelection()"
        lnkbtnPopUpRefClaimNumber.Attributes("onClick") = "ShowPPInfoWSCSelection()"
        lnkbtnPopUpInfoKendaraan.Attributes("onclick") = "ShowPPInfoKendaraanSelection()"
        btnPermintaanBukti.Attributes("onclick") = GeneralScript.GetPopUpEventReference("../Service/FrmWSCSendEmail.aspx", "", 600, 600, "DummyFunction")

        If Not IsPostBack Then
            If intViewStateMode = enumMode.Mode.NewItemMode Then
                CheckOutstandingWSC()
            End If

            If Not IsDownloaded() Then
                Dim strMessage As String = String.Empty
                strMessage = GetMonthlyFaultDescription()
                Dim strMessageHeader As String = "ANDA BELUM MELAKUKAN DOWNLOAD ATAU KIRIM DOKUMEN WARRANTY LETTER/WARRANTY STATUS LIST/KWITANSI WARRANTY/KWITANSI WARRANTY SPARE PART ACCESSCORIES (MENU DAFTAR DOKUMEN SERVICE)"
                Server.Transfer("../FrmAccessDenied.aspx?isEncode=1&mess=" & Server.UrlEncode(strMessageHeader) & "&messDescription=" & Server.UrlEncode(strMessage) & "")
            End If
            BindClaimType()
            BindRefDoc()
            BindKodePosisiWSC()

            'sessHelper.SetSession("NEW_WSCHEADER", oWSCHeader)
            'sessHelper.SetSession("NEW_ONGKOSKERJA", New ArrayList)
            'sessHelper.SetSession("NEW_ONGKOSPARTS", New ArrayList)
            'sessHelper.SetSession("NEW_WSCEVIDENCE", New ArrayList)

            SetSession("NEW_WSCHEADER", oWSCHeader)
            SetSession("NEW_ONGKOSKERJA", New ArrayList)
            SetSession("NEW_ONGKOSPARTS", New ArrayList)
            SetSession("NEW_WSCEVIDENCE", New ArrayList)

            ViewState("screenFrom") = screenFrom
            txtBranchName.Attributes.Add("readonly", "readonly")
            fillForm()

        End If
        If dgOngkosKerja.Items.Count > 0 Then
            loopOngker("Rilis")
        End If
    End Sub

    Private Function GetAttachmentList(ByVal attachmentCollection As ArrayList) As ArrayList
        Dim TempList As New ArrayList
        TempList.Clear()
        For Each obj As WSCEvidence In attachmentCollection
            TempList.Add(obj)
        Next
        Return TempList
    End Function

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        btnBatal_Click()
    End Sub
    Private Sub btnBatal_Click()
        'sessHelper.RemoveSession("CriteriaFormWSCHeader")
        RemoveSession("CriteriaFormWSCHeader")
        If Request.QueryString("Src") = "WSCDetail" Then
            Server.Transfer("~/Service/FrmWSCDetail.aspx")
        Else
            screenFrom = Request.QueryString("screenFrom")
            If IsNothing(screenFrom) OrElse screenFrom = "" Then
                screenFrom = CType(ViewState("screenFrom"), String)
            End If

            If screenFrom = "PQR" Then
                Server.Transfer("~/PQR/FrmPQRList.aspx")
            ElseIf screenFrom = "WSC" Then
                Server.Transfer("~/Service/FrmWSCStatusList.aspx")
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
        ResetPQRHeaderInfo()
        If ddlClaimType.SelectedValue.Trim = "ZA" OrElse ddlClaimType.SelectedValue.Trim = "Z2" Then
            ddlRefDoc.Enabled = True
            'ddlRefDoc.Enabled = False
            'ddlRefDoc.SelectedValue = "1"  'Referensi from PQR
        ElseIf ddlClaimType.SelectedValue.Trim = "Z4" Then
            ddlRefDoc.Enabled = False
            ddlRefDoc.SelectedValue = "0"  'Referensi from Service Buletin
        ElseIf ddlClaimType.SelectedValue.Trim = "ZB" Then
            ddlRefDoc.Enabled = False
            ddlRefDoc.SelectedValue = "1"  'Referensi from PQR
        ElseIf ddlClaimType.SelectedValue.Trim = "Z6" AndAlso Mode = enumMode.Mode.NewItemMode Then
            ddlRefDoc.Enabled = True
            'Referensi from PQR & Service Buletin
        Else
            ddlRefDoc.Enabled = False
            If Mode = enumMode.Mode.NewItemMode Then
                ddlRefDoc.SelectedIndex = -1
            End If
        End If

        If ddlClaimType.SelectedValue.Trim = "ZA" Then
            dgParts.Columns(5).Visible = True
            dgParts.Columns(6).Visible = True
        Else
            dgParts.Columns(5).Visible = False
            dgParts.Columns(6).Visible = False
        End If

        'sessHelper.SetSession("ClaimType", ddlClaimType.SelectedValue.Trim)
        SetSession("ClaimType", ddlClaimType.SelectedValue.Trim)
        ddlRefDoc_SelectedIndexChanged()
    End Sub

    Private Sub ddlRefDoc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRefDoc.SelectedIndexChanged
        ddlRefDoc_SelectedIndexChanged()
    End Sub

    Private Sub ddlRefDoc_SelectedIndexChanged()
        ResetPQRHeaderInfo()
        txtRefClaimNumber.Enabled = False

        lnkbtnPopUpRefClaimNumber.Visible = False
        lnkbtnPopUpInfoKendaraan.Visible = False
        lnkbtnCheckChassis.Attributes("style") = "display:none"
        lblSearchChassis.Visible = False
        lnkbtnPopUpInfoDokumen.Visible = True
        txtPQRNo.Enabled = True
        txtNoChasis.Enabled = False

        If ddlClaimType.SelectedValue.Trim = "ZA" OrElse ddlClaimType.SelectedValue.Trim = "Z2" OrElse ddlClaimType.SelectedValue.Trim = "ZB" Then
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
            If (ddlClaimType.SelectedValue = "ZA" OrElse ddlClaimType.SelectedValue = "Z2" OrElse ddlClaimType.SelectedValue = "ZB") AndAlso ddlRefDoc.SelectedIndex = 2 Then
                txtPQRNo.Visible = False
                'ddlRefDoc.Attributes("style") = "display:none"
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

        'sessHelper.SetSession("RefDoc", ddlRefDoc.SelectedValue.Trim)
        SetSession("RefDoc", ddlRefDoc.SelectedValue.Trim)

        If screenFrom <> "PQR" Then
            txtPQRNo.Text = ""
        End If
    End Sub

    Private Sub dgOngkosKerja_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgOngkosKerja.ItemCommand
        Dim blnBindDataGrid As Boolean = False
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        'Dim _arrONGKOSKERJA As ArrayList = CType(sessHelper.GetSession("ONGKOSKERJA"), ArrayList)
        Dim _arrONGKOSKERJA As ArrayList = CType(GetSession("ONGKOSKERJA"), ArrayList)
        If Mode = enumMode.Mode.NewItemMode Then
            '_arrONGKOSKERJA = CType(sessHelper.GetSession("NEW_ONGKOSKERJA"), ArrayList)
            _arrONGKOSKERJA = CType(GetSession("NEW_ONGKOSKERJA"), ArrayList)
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
                If IsNothing(txtPartPriceFooter) OrElse txtPartPriceFooter.Text = "0" OrElse txtPartPriceFooter.Text = String.Empty Then
                    If txtPositionCode.Text.Trim.ToUpper = "XEE999" AndAlso txtWorkCode.Text.Trim.ToUpper = "99" Then
                        If txtPartPriceFooter.Text.Trim <> "" Then
                            If CType(txtPartPriceFooter.Text.Trim, Integer) = 0 Then
                                txtPartPriceFooter.Enabled = True
                                MessageBox.Show("Silahkan input amount Ongkos Kerja !")
                                txtPartPriceFooter.Attributes("style") = "display:table-row"
                                Return
                            End If
                        End If
                    ElseIf (ddlClaimType.SelectedValue = "ZA" OrElse ddlClaimType.SelectedValue = "Z2" OrElse ddlClaimType.SelectedValue = "ZB") AndAlso txtWorkCode.Text.ToUpper = "99" Then
                        If txtPartPriceFooter.Text.Trim <> "" Then
                            If txtPartPriceFooter.Text.Trim = "0" OrElse txtPartPriceFooter.Text.Trim = "" Then
                                txtPartPriceFooter.Enabled = True
                                MessageBox.Show("Silahkan input amount Ongkos Kerja !")
                                txtPartPriceFooter.Attributes("style") = "display:table-row"
                                Return
                            End If
                        End If
                    ElseIf ddlClaimType.SelectedValue = "Z4" AndAlso txtWorkCode.Text.ToUpper = "90" Then
                        If txtPartPriceFooter.Text.Trim <> "" Then
                            If txtPartPriceFooter.Text.Trim = "0" OrElse txtPartPriceFooter.Text.Trim = "" Then
                                txtPartPriceFooter.Enabled = True
                                MessageBox.Show("Silahkan input amount Ongkos Kerja !")
                                txtPartPriceFooter.Attributes("style") = "display:table-row"
                                Return
                            End If
                        End If
                    Else
                        txtPartPriceFooter.Attributes("style") = "display:none"
                        txtPartPriceFooter.Text = "0"
                    End If
                End If

                GetObjectLaborMaster(txtPositionCode.Text.Trim(), txtWorkCode.Text.Trim(), hdnVechileTypeId.Value)

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
                If ddlClaimType.SelectedValue = "Z4" Then
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

        If txtPositionCode.Text.Trim.ToUpper = "XEE999" AndAlso txtWorkCode.Text.Trim.ToUpper = "99" Then
            txtAmon.Attributes("style") = "display:table-row"
            txtQuantity.ReadOnly = True
        Else
            txtQuantity.ReadOnly = False
            txtAmon.Attributes("style") = "display:none"
            txtAmon.Text = "0"
        End If
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
        Dim owscDet As WSCDetail = CType(e.Item.DataItem, WSCDetail)

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

        If (owscDet.PositionCode <> "" AndAlso owscDet.WorkCode <> "") OrElse IsNothing(owscDet.LaborMaster) Then
            txtPositionCodeItem.Text = owscDet.PositionCode
            txtWorkCodeItem.Text = owscDet.WorkCode
        Else
            txtPositionCodeItem.Text = owscDet.LaborMaster.LaborCode
            txtWorkCodeItem.Text = owscDet.LaborMaster.WorkCode
        End If

        GetObjectLaborMaster(txtPositionCodeItem.Text.Trim(), txtWorkCodeItem.Text.Trim(), hdnVechileTypeId.Value)
        If Not IsNothing(objLaborMaster) Then
            cbMasterValidItem.Checked = True
        Else
            cbMasterValidItem.Checked = False
        End If

        If Mode <> enumMode.Mode.NewItemMode AndAlso IsNothing(owscDet.LaborMaster) Then
            UpdateWscDetailLaborID(owscDet)
        End If

        If txtWorkCodeItem.Text.Trim.ToUpper = "99" OrElse txtWorkCodeItem.Text.Trim.ToUpper = "90" OrElse lblWorkCode.Text.Trim.ToUpper = "99" OrElse lblWorkCode.Text.Trim.ToUpper = "90" Then
            txtAmon.Attributes("style") = "display:table-row"
            txtQuantity.ReadOnly = True
        Else
            txtQuantity.ReadOnly = False
            txtAmon.Attributes("style") = "display:none"
            txtAmon.Text = "0"
        End If

        If ddlClaimType.SelectedValue = "Z2" OrElse ddlClaimType.SelectedValue = "ZB" Then
            If (ddlRefDoc.SelectedValue = "0" And txtWorkCodeItem.Text.Trim.ToUpper = "99") OrElse (ddlRefDoc.SelectedValue = "1" And txtWorkCodeItem.Text.Trim.ToUpper = "90") Then
                txtAmon.Attributes("style") = "display:none"
            ElseIf (ddlRefDoc.SelectedValue = "0" And txtWorkCodeItem.Text.Trim.ToUpper = "90") OrElse (ddlRefDoc.SelectedValue = "1" And txtWorkCodeItem.Text.Trim.ToUpper = "99") Then
                txtAmon.Attributes("style") = "display:table-row"
            End If
        End If

        If ddlClaimType.SelectedValue = "Z4" Then
            If txtWorkCodeItem.Text.Trim.ToUpper = "90" Then
                txtAmon.Attributes("style") = "display:table-row"
            ElseIf txtWorkCodeItem.Text.Trim.ToUpper = "90" Then
                txtAmon.Attributes("style") = "display:none"
            End If
        End If

        If txtQuantity.Text.Trim = "0" OrElse txtQuantity.Text.Trim = "" Then
            txtQuantity.Text = "1"
        End If
        If txtPositionCodeItem.Text.Trim.ToUpper.Contains("XEE999") AndAlso txtWorkCodeItem.Text.Trim.ToUpper = "99" Then
            txtPositionCodeItem.Enabled = False
            txtWorkCodeItem.Enabled = False
        End If
        If ddlClaimType.SelectedValue = "Z4" Then
            txtQuantity.Enabled = False
            txtQuantity.Text = "1"
            txtPositionCodeItem.Attributes.Add("readonly", "readonly")
            txtWorkCodeItem.Attributes.Add("readonly", "readonly")
        End If
    End Sub

    Private Function PositionWorkCodeIsExist(ByVal CodePositionID As Integer, ByVal CodeWorkID As Integer, ByVal ONGKOSKERJACollection As ArrayList) As Boolean
        Dim bResult As Boolean = False
        Try
            If ONGKOSKERJACollection.Count > 0 Then
                For Each _objWSCDetail As WSCDetail In ONGKOSKERJACollection
                    Dim criterias As New CriteriaComposite(New Criteria(GetType(DeskripsiKodePosisi), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(DeskripsiKodePosisi), "KodePosition", MatchType.Exact, _objWSCDetail.PositionCode.Trim))
                    Dim tempArr As ArrayList = New DeskripsiPositionCodeFacade(User).Retrieve(criterias)
                    If tempArr.Count > 0 Then
                        Dim _deskripsiKodePosisi As DeskripsiKodePosisi = CType(tempArr(0), DeskripsiKodePosisi)
                        If _deskripsiKodePosisi.ID = CodePositionID Then
                            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(DeskripsiKodeKerja), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criterias2.opAnd(New Criteria(GetType(DeskripsiKodeKerja), "KodeKerja", MatchType.Exact, _objWSCDetail.WorkCode.Trim))
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
        Dim txtQtyFooter As TextBox = CType(e.Item.FindControl("txtQtyFooter"), TextBox)
        Dim lblFakturNumberFooter As Label = CType(e.Item.FindControl("lblFakturNumberFooter"), Label)
        Dim lblFakturDateFooter As Label = CType(e.Item.FindControl("lblFakturDateFooter"), Label)

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
            txtQtyFooter.Enabled = False
            txtAmon.Attributes("style") = "display:table-row"
            '        End If
            '    End If
        Else
            txtAmon.Text = 0
            txtQtyFooter.Enabled = True
            'txtAmon.Enabled = False
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


    Private Function loopOngker(Optional ByVal func As String = "")
        For Each item As DataGridItem In dgOngkosKerja.Items
            If item.ItemType = ListItemType.Item OrElse item.ItemType = ListItemType.AlternatingItem Then
                Dim txtWorkCode As TextBox = CType(item.FindControl("txtWorkCodeItem"), TextBox)
                Dim lblWorkCode As Label = CType(item.FindControl("lblWorkCode"), Label)
                Dim txtAmountItem As TextBox = CType(item.FindControl("txtAmountItem"), TextBox)
                If func = "Rilis" Then
                    If (txtWorkCode.Text.ToUpper = "99" OrElse txtWorkCode.Text.ToUpper = "90" _
                        OrElse lblWorkCode.Text.ToUpper = "99" OrElse lblWorkCode.Text.ToUpper = "90") _
                        AndAlso ddlClaimType.SelectedValue <> "Z2" Then
                        txtAmountItem.Attributes("style") = "display:table-row"
                    ElseIf txtWorkCode.Text.Trim.ToUpper = "99" AndAlso (ddlClaimType.SelectedValue = "ZA" OrElse ddlClaimType.SelectedValue = "Z2" OrElse ddlClaimType.SelectedValue = "ZB") Then
                        txtAmountItem.Attributes("style") = "display:table-row"
                    Else
                        txtAmountItem.Attributes("style") = "display:none"
                    End If
                    Return 0
                End If
                If CType(item.FindControl("txtPostionCodeItem"), TextBox).Text.ToUpper = "XEE999" _
                    AndAlso txtWorkCode.Text.ToUpper = "99" Then
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
        Dim ROwData As WSCDetail = CType(e.Item.DataItem, WSCDetail)

        Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
        lblNo.Text = e.Item.ItemIndex + 1 + (dgParts.CurrentPageIndex * dgParts.PageSize)
        Dim cbMainPartItem As CheckBox = CType(e.Item.FindControl("cbMainPartItem"), CheckBox)
        Dim hdnMainPartItem As HiddenField = CType(e.Item.FindControl("hdnMainPartItem"), HiddenField)
        Dim txtQty As TextBox = CType(e.Item.FindControl("txtQtyItem"), TextBox)
        Dim txtAmon As TextBox = CType(e.Item.FindControl("txtPartPriceItem"), TextBox)
        Dim txtParts As Label = CType(e.Item.FindControl("lblKodeParts"), Label)
        Dim txtKodePartsItem As TextBox = CType(e.Item.FindControl("txtKodePartsItem"), TextBox)
        Dim lblFakturNumber As Label = CType(e.Item.FindControl("lblFakturNumber"), Label)
        Dim lblFakturDate As Label = CType(e.Item.FindControl("lblFakturDate"), Label)

        Dim spBilling As SparePartBilling = New SparePartBillingFacade(User).Retrieve(lblFakturNumber.Text.Trim)
        If IsNothing(spBilling) Then
            lblFakturDate.Text = ""
        Else
            lblFakturDate.Text = spBilling.BillingDate
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
            txtQty.Enabled = False
            txtAmon.Attributes("style") = "display:table-row"
            If txtQty.Text.Trim = "0" OrElse txtQty.Text.Trim = "" Then
                txtQty.Text = "1"
            End If
            'End If
        Else
            'txtAmon.Enabled = False
            txtQty.Enabled = True
            txtAmon.Attributes("style") = "display:none"
            If txtQty.Text.Trim = "0" OrElse txtQty.Text.Trim = "" Then
                txtQty.Text = "0"
                txtQty.Enabled = True
            End If
        End If

        'If loopOngker() Then
        '    disableEnableMainPart(e, False, False)
        'Else
        '    disableEnableMainPart(e, True, True)
        'End If

        'If loopPart("SetdgPartsItem") Then
        '    disableEnableMainPart(e, False, False)
        'Else
        '    disableEnableMainPart(e, True, True)
        'End If



        If hdnMainPartItem.Value = "0" Then
            cbMainPartItem.Checked = False
        ElseIf hdnMainPartItem.Value = "1" Then
            cbMainPartItem.Checked = True
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
            For Each _PQRPartsCode As WSCDetail In arrPartsCodeCollection
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
            For Each _PQRPartsCode As WSCDetail In PartsCodeCollection
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
            For Each _PQRPartsCode As WSCDetail In PartsCodeCollection
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
        'Dim _arrPartsCode As ArrayList = CType(sessHelper.GetSession("ONGKOSPARTS"), ArrayList)
        Dim _arrPartsCode As ArrayList = CType(GetSession("ONGKOSPARTS"), ArrayList)
        If Mode = enumMode.Mode.NewItemMode Then
            '_arrPartsCode = CType(sessHelper.GetSession("NEW_ONGKOSPARTS"), ArrayList)
            _arrPartsCode = CType(GetSession("NEW_ONGKOSPARTS"), ArrayList)
        End If
        Select Case e.CommandName
            Case "Add" 'Insert New item to datagrid
                Dim txtPartsCode As TextBox = CType(e.Item.FindControl("txtKodePartsFooter"), TextBox)
                Dim txtQtyFooter As TextBox = CType(e.Item.FindControl("txtQtyFooter"), TextBox)
                Dim txtPartPriceFooter As TextBox = CType(e.Item.FindControl("txtPartPriceFooter"), TextBox)
                Dim cbMainPartFooter As CheckBox = CType(e.Item.FindControl("cbMainPartFooter"), CheckBox)
                Dim hdnMainPartItem As HiddenField = CType(e.Item.FindControl("hdnMainPartItem"), HiddenField)
                Dim txtFakturNumberFooter As TextBox = CType(e.Item.FindControl("txtFakturNumberFooter"), TextBox)
                Dim lblFakturDateFooter As Label = CType(e.Item.FindControl("lblFakturDateFooter"), Label)

                Dim objSparePartMaster As SparePartMaster

                If ddlClaimType.SelectedValue = "ZA" Then
                    If IsNothing(txtFakturNumberFooter) OrElse txtFakturNumberFooter.Text = String.Empty Then
                        MessageBox.Show("Nomor Faktur masih kosong")
                        Return
                    End If
                End If

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
                ElseIf txtQtyFooter.Text = "NPN7" Then
                    txtQtyFooter.Text = "1"
                End If
                If Not IsNothing(txtPartPriceFooter) Then
                    If txtPartPriceFooter.Text.Trim = "0" OrElse txtPartPriceFooter.Text.Trim = String.Empty Then
                        If txtPartPriceFooter.Text.Trim = String.Empty Then txtPartPriceFooter.Text = 0
                        If txtPartsCode.Text.Trim.ToUpper = "NPN7" Then
                            If CType(txtPartPriceFooter.Text.Trim, Integer) = 0 OrElse txtPartPriceFooter.Text.Trim = "" Then
                                txtPartPriceFooter.Enabled = True
                                txtPartPriceFooter.Attributes("style") = "display:table-row"
                                txtQtyFooter.Text = 1
                                txtQtyFooter.Attributes("readOnly") = "true"
                                MessageBox.Show("Amount masih kosong atau nol")
                                Return
                            End If
                        Else
                            'txtPartPriceFooter.Visible = False
                            txtPartPriceFooter.Attributes("style") = "display:none"
                            txtPartPriceFooter.Text = "0"
                        End If
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
                    Dim deletedOngkosKerja As WSCDetail = CType(_arrPartsCode(e.Item.ItemIndex), WSCDetail)
                    If deletedOngkosKerja.ID > 0 Then
                        Dim deletedArrLst As ArrayList
                        'deletedArrLst = CType(sessHelper.GetSession("DELETEDPARTS"), ArrayList)
                        deletedArrLst = CType(GetSession("DELETEDPARTS"), ArrayList)
                        deletedArrLst.Add(deletedOngkosKerja)
                        'sessHelper.SetSession("DELETEDPARTS", deletedArrLst)
                        SetSession("DELETEDPARTS", deletedArrLst)
                    End If
                    _arrPartsCode.RemoveAt(e.Item.ItemIndex)
                End If

        End Select


        BindOngkosParts()

    End Sub
#End Region

#Region "Datagrid Attachment Evidence"
    Private Sub dgFileWSCEvidence_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgFileWSCEvidence.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            'Dim lNum As LiteralControl = New LiteralControl((e.Item.ItemIndex + 1).ToString())
            'e.Item.Cells(0).Controls.Add(lNum)

            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            lblNo.Text = e.Item.ItemIndex + 1 + (dgOngkosKerja.CurrentPageIndex * dgOngkosKerja.PageSize)

            'Dim arrAttachment As ArrayList = CType(sessHelper.GetSession("WSCEVIDENCE"), ArrayList)
            Dim arrAttachment As ArrayList = CType(GetSession("WSCEVIDENCE"), ArrayList)
            Mode = CType(ViewState("Mode"), enumMode.Mode)
            If Mode = enumMode.Mode.NewItemMode Then
                'arrAttachment = CType(sessHelper.GetSession("NEW_WSCEVIDENCE"), ArrayList)
                arrAttachment = CType(GetSession("NEW_WSCEVIDENCE"), ArrayList)
            End If
            If arrAttachment.Count > 0 Then
                Dim objWSCEvidence As WSCEvidence = arrAttachment(e.Item.ItemIndex)

                Dim lblWSCEvidenceType As Label = CType(e.Item.FindControl("lblWSCEvidenceType"), Label)
                lblWSCEvidenceType.Text = EnumWSCEvidenceType.GetStringWSCEvType(objWSCEvidence.EvidenceType)
                Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lnkbtnFileAttachmentTopDelete"), LinkButton)
                Dim lblFileWSCEVIDENCE As Label = CType(e.Item.FindControl("lblFileWSCEVIDENCE"), Label)
                Try
                    If Mode = enumMode.Mode.NewItemMode Then
                        If objWSCEvidence.IsFromPQR Then
                            lblFileWSCEVIDENCE.Text = Path.GetFileName(objWSCEvidence.PathFile)
                            lbtnDelete.Visible = False
                        Else
                            lblFileWSCEVIDENCE.Text = Path.GetFileName(objWSCEvidence.AttachmentData.FileName)
                        End If
                    Else
                        lblFileWSCEVIDENCE.Text = Path.GetFileName(objWSCEvidence.PathFile)
                        If IsNothing(objWSCEvidence.AttachmentData) Then
                            lbtnDelete.Visible = False
                        End If
                    End If
                Catch ex As Exception
                    lblFileWSCEVIDENCE.Text = Path.GetFileName(objWSCEvidence.PathFile)
                End Try
            End If
        End If
        If e.Item.ItemType = ListItemType.Footer Then
            Dim ddlWSCEvidenceTypeFooter As DropDownList = CType(e.Item.Cells(1).FindControl("ddlWSCEvidenceTypeFooter"), DropDownList)
            BindWSCEvidenceType(ddlWSCEvidenceTypeFooter)
        End If

    End Sub

    Private Function FileIsExist(ByVal intEvidenceType As Integer, ByVal FileName As String, ByVal AttachmentCollection As ArrayList) As Boolean
        Dim bResult As Boolean = False
        If AttachmentCollection.Count > 0 Then
            For Each objWSCEvidence As WSCEvidence In AttachmentCollection
                If Not IsNothing(objWSCEvidence.AttachmentData) Then
                    If Path.GetFileName(objWSCEvidence.AttachmentData.FileName) = FileName Then
                        If intEvidenceType = objWSCEvidence.EvidenceType Then
                            bResult = True
                            Exit For
                        End If
                    End If
                Else
                    If intEvidenceType = objWSCEvidence.EvidenceType Then
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
            For Each objWSCEvidence As WSCEvidence In AttachmentCollection
                If Path.GetFileName(objWSCEvidence.AttachmentData.FileName) = FileName AndAlso nIndeks <> i Then
                    bResult = True
                    Exit For
                End If
                i += 1
            Next
        End If
        Return bResult
    End Function


    Private Sub dgFileWSCEvidence_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgFileWSCEvidence.ItemCommand
        'Dim _arrWSCEVIDENCE As ArrayList = CType(sessHelper.GetSession("WSCEVIDENCE"), ArrayList)
        Dim _arrWSCEVIDENCE As ArrayList = CType(GetSession("WSCEVIDENCE"), ArrayList)
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        If Mode = enumMode.Mode.NewItemMode Then
            '_arrWSCEVIDENCE = CType(sessHelper.GetSession("NEW_WSCEVIDENCE"), ArrayList)
            _arrWSCEVIDENCE = CType(GetSession("NEW_WSCEVIDENCE"), ArrayList)
        End If

        Select Case e.CommandName
            Case "Add" 'Insert New item to datagrid
                oWSCHeader = GetSession("WSCHEADER")
                Dim ddlWSCEvidenceTypeFooter As DropDownList = CType(e.Item.FindControl("ddlWSCEvidenceTypeFooter"), DropDownList)
                Dim FileUpload As HtmlInputFile = CType(e.Item.FindControl("iFileWSCEVIDENCE"), HtmlInputFile)
                Dim objPostedData As HttpPostedFile
                Dim objWSCEvidence As WSCEvidence = New WSCEvidence
                Dim sFileName As String

                If IsNothing(ddlWSCEvidenceTypeFooter) OrElse ddlWSCEvidenceTypeFooter.SelectedIndex = 0 Then
                    MessageBox.Show("Tipe Bukti masih kosong")
                    Return
                End If

                '========= Validasi Attacment tipe Kwitansi =======================================================================
                'Dim _arrONGKOSKERJA As ArrayList = CType(sessHelper.GetSession("ONGKOSKERJA"), ArrayList)
                Dim _arrONGKOSKERJA As ArrayList = CType(GetSession("ONGKOSKERJA"), ArrayList)
                Mode = CType(ViewState("Mode"), enumMode.Mode)
                If Mode = enumMode.Mode.NewItemMode Then
                    '_arrONGKOSKERJA = CType(sessHelper.GetSession("NEW_ONGKOSKERJA"), ArrayList)
                    _arrONGKOSKERJA = CType(GetSession("NEW_ONGKOSKERJA"), ArrayList)
                End If
                Dim blnIsExistPosition9990 As Boolean = False
                For Each _objWSCDetail As WSCDetail In _arrONGKOSKERJA
                    If _objWSCDetail.PositionCode = "99" OrElse _objWSCDetail.PositionCode = "90" Then
                        blnIsExistPosition9990 = True
                        Exit For
                    End If
                Next
                'Dim _arrONGKOSPART As ArrayList = CType(sessHelper.GetSession("ONGKOSPARTS"), ArrayList)
                Dim _arrONGKOSPART As ArrayList = CType(GetSession("ONGKOSPARTS"), ArrayList)
                Mode = CType(ViewState("Mode"), enumMode.Mode)
                If Mode = enumMode.Mode.NewItemMode Then
                    '_arrONGKOSPART = CType(sessHelper.GetSession("NEW_ONGKOSPARTS"), ArrayList)
                    _arrONGKOSPART = CType(GetSession("NEW_ONGKOSPARTS"), ArrayList)
                End If
                Dim blnIsExistPartNPN7 As Boolean = False
                For Each _objWSCDetail As WSCDetail In _arrONGKOSPART
                    If _objWSCDetail.SparePartMaster.PartCode.Trim = "NPN7" Then
                        blnIsExistPartNPN7 = True
                        Exit For
                    End If
                Next
                If IsNothing(ddlWSCEvidenceTypeFooter) OrElse ddlWSCEvidenceTypeFooter.SelectedValue = "0" Then
                    If blnIsExistPosition9990 = True OrElse blnIsExistPartNPN7 = True Then
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


                    Dim ext As String = System.IO.Path.GetExtension(FileUpload.PostedFile.FileName)
                    If Not (ext.ToUpper() = ".JPG" OrElse ext.ToUpper() = ".JPEG" OrElse ext.ToUpper() = ".PDF" OrElse ext.ToUpper() = ".DOC" OrElse ext.ToUpper() = ".DOCX" OrElse ext.ToUpper() = ".XLS" OrElse ext.ToUpper() = ".XLSX" _
                            OrElse ext.ToUpper() = ".MP3" OrElse ext.ToUpper() = ".MP4" OrElse ext.ToUpper() = ".PNG" OrElse ext.ToUpper() = ".PPT") Then
                        MessageBox.Show("Hanya menerima file format (PDF/JPG/JPEG/DOC/DOCX/XLS/XLSX/MP3/MP4/PNG/PPT)")
                        Return
                    End If

                    If KTB.DNet.UI.Helper.FileHelper.IsExecutableFile(sFileName) Then
                        MessageBox.Show("Tidak diperkenankan mengupload file dengan ekstensi '.exe'. Pastikan file anda bebas dari virus.")
                        BindWSCEvidence()
                        Return
                    End If

                    If Not FileIsExist(ddlWSCEvidenceTypeFooter.SelectedValue, sFileName, _arrWSCEVIDENCE) Then
                        Dim SrcFile As String = Path.GetFileName(objPostedData.FileName) '-- Source file name
                        'Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("WSCEvidenceFileDirectory") & "\" & lblDealerVal.Text &
                        '    "\ClaimNumber\" & "SS" & TimeStamp() & SrcFile.Substring(SrcFile.Length - 4)  '-- Destination file
                        Dim newFileNameFormat As String = txtNoChasis.Text.Trim & "-" & oDealer.DealerCode & "-" & "{0}-" & DateTime.Now.ToString("ffff") & Path.GetExtension(objPostedData.FileName)
                        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("WSCEvidenceFileDirectory") & "\" & lblDealerVal.Text & "\" & newFileNameFormat
                        Dim FileName As String

                        objWSCEvidence.EvidenceType = ddlWSCEvidenceTypeFooter.SelectedValue
                        objWSCEvidence.Description = EnumWSCEvidenceType.GetStringWSCEvType(ddlWSCEvidenceTypeFooter.SelectedValue)
                        objWSCEvidence.PathFile = DestFile
                        objWSCEvidence.AttachmentData = objPostedData
                        objWSCEvidence.UploadDate = DateTime.Now.Day
                        objWSCEvidence.UploadMonth = DateTime.Now.Month
                        objWSCEvidence.UploadYear = DateTime.Now.Year
                        objWSCEvidence.WSCHeader = Nothing

                        UploadAttachment(objWSCEvidence, TempDirectory)

                        _arrWSCEVIDENCE.Add(objWSCEvidence)

                        If Mode = enumMode.Mode.NewItemMode Then
                            'sessHelper.SetSession("NEW_WSCEVIDENCE", _arrWSCEVIDENCE)
                            SetSession("NEW_WSCEVIDENCE", _arrWSCEVIDENCE)
                        Else
                            'sessHelper.SetSession("WSCEVIDENCE", _arrWSCEVIDENCE)
                            SetSession("WSCEVIDENCE", _arrWSCEVIDENCE)
                        End If
                        'Page.RegisterStartupScript("test", "<script language=JavaScript> focusSave(); </script>")
                    Else
                        MessageBox.Show(SR.DataIsExist("Attachment File"))
                    End If
                Else
                    objWSCEvidence.EvidenceType = ddlWSCEvidenceTypeFooter.SelectedValue
                    objWSCEvidence.Description = EnumWSCEvidenceType.GetStringWSCEvType(ddlWSCEvidenceTypeFooter.SelectedValue)
                    objWSCEvidence.PathFile = vbNull
                    objWSCEvidence.AttachmentData = objPostedData
                    objWSCEvidence.UploadDate = DateTime.Now.Day
                    objWSCEvidence.UploadMonth = DateTime.Now.Month
                    objWSCEvidence.UploadYear = DateTime.Now.Year
                    objWSCEvidence.WSCHeader = Nothing

                    _arrWSCEVIDENCE.Add(objWSCEvidence)

                    If Mode = enumMode.Mode.NewItemMode Then
                        'sessHelper.SetSession("NEW_WSCEVIDENCE", _arrWSCEVIDENCE)
                        SetSession("NEW_WSCEVIDENCE", _arrWSCEVIDENCE)
                    Else
                        'sessHelper.SetSession("WSCEVIDENCE", _arrWSCEVIDENCE)
                        SetSession("WSCEVIDENCE", _arrWSCEVIDENCE)
                    End If
                End If

            Case "Delete" 'Delete this datagrid item 
                If Mode = enumMode.Mode.NewItemMode Then
                    RemoveWSCAttachment(CType(_arrWSCEVIDENCE(e.Item.ItemIndex), WSCEvidence), TempDirectory)
                    _arrWSCEVIDENCE.RemoveAt(e.Item.ItemIndex)
                ElseIf Mode = enumMode.Mode.EditMode Then
                    Dim deletedWSCEvidence As WSCEvidence = CType(_arrWSCEVIDENCE(e.Item.ItemIndex), WSCEvidence)
                    If deletedWSCEvidence.ID > 0 Then
                        Dim deletedArrLst As ArrayList
                        'deletedArrLst = CType(sessHelper.GetSession("DELETEDWSEVIDENCE"), ArrayList)
                        deletedArrLst = CType(GetSession("DELETEDWSEVIDENCE"), ArrayList)
                        deletedArrLst.Add(deletedWSCEvidence)
                        'sessHelper.SetSession("DELETEDWSEVIDENCE", deletedArrLst)
                        SetSession("DELETEDWSEVIDENCE", deletedArrLst)
                    End If
                    _arrWSCEVIDENCE.RemoveAt(e.Item.ItemIndex)
                End If

            Case "Download" 'Download File
                Dim owscEv As WSCEvidence = _arrWSCEVIDENCE(e.Item.ItemIndex)
                If owscEv.IsFromPQR Then
                    Response.Redirect("../Download.aspx?file=" & owscEv.PQRFilePath)
                Else
                    Response.Redirect("../Download.aspx?file=" & e.CommandArgument)
                End If

        End Select

        BindWSCEvidence()
    End Sub

    Private Function getFileNameCounter() As String
        Dim arrEV As ArrayList = New ArrayList
        If Mode = enumMode.Mode.NewItemMode Then
            arrEV = CType(Session("NEW_WSCEVIDENCE"), ArrayList)
        Else
            arrEV = CType(Session("WSCEVIDENCE"), ArrayList)
        End If

        If arrEV.Count = 0 Then
            Return String.Empty
        Else
            If Mode = enumMode.Mode.NewItemMode Then
                Return "-" & arrEV.Count
            Else
                Dim c As Integer = 0
                For Each ev As WSCEvidence In arrEV
                    If ev.PathFile.Contains(oWSCHeader.ClaimNumber) Then
                        c = c + 1
                    End If
                Next

                Return "-" & c
            End If
        End If
    End Function

#End Region

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan2.Click, Button1.Click, btnSave.Click
        loopOngker("Rilis")
        Dim result As Integer
        Dim ErrMessage As String = String.Empty
        Mode = CType(ViewState("Mode"), enumMode.Mode)

        'Zaldy 19/12/2019 Req Doni
        If oDealer.Title = EnumDealerTittle.DealerTittle.KTB AndAlso Mode = enumMode.Mode.ViewMode AndAlso txtNotes.Enabled = True Then
            'oWSCHeader = sessHelper.GetSession("WSCHEADER")
            oWSCHeader = GetSession("WSCHEADER")
            oWSCHeader.Notes = KTB.DNet.Utility.CommonFunction.RemoveWhiteSpace(txtNotes.Text)
            Dim ok As Integer = oWSCHeaderFacade.Update(oWSCHeader)
            If ok > 0 Then
                MessageBox.Show("Simpan Data Sukses !")
                Server.Transfer("~/Service/FrmWSCStatusList.aspx")
            Else
                If ErrMessage = String.Empty Then
                    MessageBox.Show("Simpan Data Gagal !")
                Else
                    MessageBox.Show(ErrMessage)
                End If
            End If
        End If
        '---

        If Not Page.IsValid Then
            MessageBox.Show("Lengkapi data terlebih dahulu")
            Return
        End If

        Dim _chassisMaster As ChassisMaster
        _chassisMaster = oChassisFacade.Retrieve(txtNoChasis.Text)
        Dim WSCValidationMsg As String = String.Empty
        If ddlClaimType.SelectedValue.Trim = "ZA" OrElse ddlClaimType.SelectedValue.Trim = "Z2" Then
            If intViewStateMode = enumMode.Mode.NewItemMode Then
                Dim isPDIFSPMExist As Boolean = ValidationPDIFSPM(WSCValidationMsg, _chassisMaster)
                If Not isPDIFSPMExist Then
                    MessageBox.Show(WSCValidationMsg)
                    Return
                End If
            End If
        End If

        If ValidateSaveData() Then
            ''-- fill value to object WSCHeader
            BindWSCHeaderDomain()

            If Mode = enumMode.Mode.NewItemMode Then
                'oWSCHeader = sessHelper.GetSession("NEW_WSCHEADER")
                oWSCHeader = GetSession("NEW_WSCHEADER")
            Else
                'oWSCHeader = sessHelper.GetSession("WSCHEADER")
                oWSCHeader = GetSession("WSCHEADER")
            End If

            If Not IsNothing(txtWONumber.Text) AndAlso txtWONumber.Text <> "" Then
                oWSCHeader.WorkOrderNumber = txtWONumber.Text
            End If
            If Not IsNothing(txtDealerBranchCode.Text) AndAlso txtDealerBranchCode.Text <> "" Then
                Dim objDealerBranch As DealerBranch = New DealerBranchFacade(User).Retrieve(txtDealerBranchCode.Text)
                oWSCHeader.DealerBranch = objDealerBranch
            End If

            If Mode = enumMode.Mode.NewItemMode Then
                'result = oWSCHeaderFacade.InsertTransaction(oWSCHeader, CType(sessHelper.GetSession("NEW_ONGKOSKERJA"), ArrayList), CType(sessHelper.GetSession("NEW_ONGKOSPARTS"), ArrayList), CType(sessHelper.GetSession("NEW_WSCEVIDENCE"), ArrayList))
                result = oWSCHeaderFacade.InsertTransaction(oWSCHeader, CType(GetSession("NEW_ONGKOSKERJA"), ArrayList), CType(GetSession("NEW_ONGKOSPARTS"), ArrayList), CType(GetSession("NEW_WSCEVIDENCE"), ArrayList))
            ElseIf Mode = enumMode.Mode.EditMode Then
                'result = oWSCHeaderFacade.UpdateTransaction(oWSCHeader, CType(sessHelper.GetSession("ONGKOSKERJA"), ArrayList), CType(sessHelper.GetSession("DELETEDONGKOSKERJA"), ArrayList), CType(sessHelper.GetSession("ONGKOSPARTS"), ArrayList), CType(sessHelper.GetSession("DELETEDPARTS"), ArrayList), CType(sessHelper.GetSession("WSCEVIDENCE"), ArrayList), CType(sessHelper.GetSession("DELETEDWSEVIDENCE"), ArrayList), ErrMessage)
                result = oWSCHeaderFacade.UpdateTransaction(oWSCHeader, CType(GetSession("ONGKOSKERJA"), ArrayList), CType(GetSession("DELETEDONGKOSKERJA"), ArrayList), CType(GetSession("ONGKOSPARTS"), ArrayList), CType(GetSession("DELETEDPARTS"), ArrayList), CType(GetSession("WSCEVIDENCE"), ArrayList), CType(GetSession("DELETEDWSEVIDENCE"), ArrayList), ErrMessage)
            End If

            If result > 0 Then

                Dim _vehicleCode As String = _chassisMaster.VechileColor.VechileType.VechileTypeCode

                Dim _arl As ArrayList = New ArrayList
                If Mode = enumMode.Mode.NewItemMode Then
                    '_arl = CType(sessHelper.GetSession("NEW_ONGKOSKERJA"), ArrayList)
                    _arl = CType(GetSession("NEW_ONGKOSKERJA"), ArrayList)
                Else
                    '_arl = CType(sessHelper.GetSession("ONGKOSKERJA"), ArrayList)
                    _arl = CType(GetSession("ONGKOSKERJA"), ArrayList)
                End If
                Dim noLabor As ArrayList = New ArrayList
                For Each objWSCDetail As WSCDetail In _arl
                    If objWSCDetail.PositionCode.Trim <> "" And objWSCDetail.WorkCode.Trim <> "" Then
                        Dim oLabMaster As LaborMaster = GetLaborMaster(objWSCDetail.PositionCode, objWSCDetail.WorkCode, _chassisMaster.VechileColor.VechileType.ID)
                        If IsNothing(oLabMaster) Then
                            noLabor.Add(objWSCDetail)
                        End If
                    End If
                Next

                MailReport(_vehicleCode, noLabor)

                ' Proses Upload di pisahkan dari simpan 
                'UploadAttachment(CType(sessHelper.GetSession("WSCEVIDENCE"), ArrayList), TargetDirectory)

                If Mode = enumMode.Mode.NewItemMode Then
                    'CommitAttachment(CType(sessHelper.GetSession("NEW_WSCEVIDENCE"), ArrayList))
                    CommitAttachment(CType(GetSession("NEW_WSCEVIDENCE"), ArrayList))
                Else
                    'CommitAttachment(CType(sessHelper.GetSession("WSCEVIDENCE"), ArrayList))
                    CommitAttachment(CType(GetSession("WSCEVIDENCE"), ArrayList))
                End If
                If Mode = enumMode.Mode.EditMode Then
                    'RemoveWSCAttachment(CType(sessHelper.GetSession("DELETEDWSEVIDENCE"), ArrayList), TargetDirectory)
                    RemoveWSCAttachment(CType(GetSession("DELETEDWSEVIDENCE"), ArrayList), TargetDirectory)
                End If
                ClearTempData()

                lblClaimNumber.Text = oWSCHeaderFacade.Retrieve(result).ClaimNumber.Trim


                MessageBox.Show("Simpan Data Sukses !")
                Server.Transfer("~/Service/FrmWSCStatusList.aspx")
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
        loopOngker("Rilis")
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

            'ddlKodeWSCA.Enabled = False
            'ddlKodeWSCB.Enabled = False
            'ddlKodeWSCC.Enabled = False

            If ddlRefDoc.SelectedValue = "1" Then 'PQR Ref Doc
                Dim criterias2 As New CriteriaComposite(New Criteria(GetType(PQRHeader), "RowStatus", MatchType.No, CType(DBRowStatus.Deleted, Short)))
                criterias2.opAnd(New Criteria(GetType(PQRHeader), "PQRNo", MatchType.Exact, txtPQRNo.Text.Trim))
                Dim _arrPQR As ArrayList = New PQRHeaderFacade(User).Retrieve(criterias2)
                If _arrPQR.Count > 0 Then
                    Dim objPQRHeader As PQRHeader = CType(_arrPQR(0), PQRHeader)
                    intVechileTypeID1 = objPQRHeader.ChassisMaster.VechileColor.VechileType.ID
                End If
            End If

            Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")
            Dim criterias As New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, strChassisNumber))
            criterias.opAnd(New Criteria(GetType(ChassisMaster), "Category.ProductCategory.Code", MatchType.Exact, companyCode))
            Dim ChassisColl As ArrayList = New ChassisMasterFacade(User).Retrieve(criterias)
            If ChassisColl.Count > 0 Then
                Dim objChassisMaster As ChassisMaster = CType(ChassisColl(0), ChassisMaster)
                If ddlRefDoc.SelectedValue = "1" Then 'PQR Ref Doc
                    intVechileTypeID2 = objChassisMaster.VechileColor.VechileType.ID
                    If intVechileTypeID1 <> intVechileTypeID2 Then
                        txtNoChasis.ForeColor = Color.Red
                        MessageBox.Show("Tipe kendaraan harus sama dengan tipe kendaraan pada Dokumen PQR.")
                        txtNoChasis.Text = hdntxtNoChasis.Value
                        Return False
                    End If
                End If

                blnIsSoldDealer = False
                If oDealer.ID = objChassisMaster.Dealer.ID Then  '---Jika login dealer id = sold dealer id
                    blnIsSoldDealer = True
                End If

                If IsNothing(objChassisMaster.EndCustomer) AndAlso objChassisMaster.FakturStatus = 0 Then
                    MessageBox.Show("Kendaraan Belum Terjual")
                    ResetPQRHeaderInfo()
                    Exit Function
                End If

                If LoadChassisMasterPKT(objChassisMaster, blnIsSoldDealer) Then
                    LoadChassisInfo(objChassisMaster)
                    If ddlRefDoc.SelectedValue = "0" Then 'Buletin Ref Doc
                        Try
                            Dim _result As Integer = LoadPositionAndWorkCodes(txtNoChasis.Text.Trim, txtPQRNo.Text.Trim)
                            If _result < 1 Then
                                Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", CType(DBRowStatus.Active, Short)))
                                criteria.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", txtNoChasis.Text))
                                Dim ArrChas As ArrayList = New ChassisMasterFacade(User).Retrieve(criteria)
                                If ArrChas.Count > 0 Then
                                    Dim objChas As ChassisMaster = ArrChas(0)
                                    MessageBox.Show("Ongkos kerja untuk service buletin " & txtPQRNo.Text & " \ndengan Tipe Kendaraan " & objChas.VechileColor.VechileType.VechileTypeCode & " masih kosong \nSilahkan hubungi MMKSI")
                                Else
                                    MessageBox.Show("Ongkos kerja untuk service buletin " & txtPQRNo.Text & " \ndengan Nomor rangka " & txtNoChasis.Text & " masih kosong \nSilahkan hubungi MMKSI")
                                End If
                                Return False
                            End If
                        Catch
                        End Try
                    End If
                    txtNoChasis.ForeColor = Color.Black
                    RefreshGrid()
                Else
                    ClearChassisInfo()
                    Return False
                End If
                'blnIsSoldDealer = False
                'If oDealer.ID = objChassisMaster.Dealer.ID Then  '---Jika login dealer id = sold dealer id
                '    blnIsSoldDealer = True
                'End If
                'If LoadChassisMasterPKT(objChassisMaster, blnIsSoldDealer) Then
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

    Private Sub LoadPQRHeaderInfo(oPQRHeader As PQRHeader)
        txtPQRNo.Text = oPQRHeader.PQRNo
        lblPQRNoVal.Text = oPQRHeader.PQRNo
        txtNoChasis.Text = oPQRHeader.ChassisMaster.ChassisNumber
        'If ddlRefDoc.SelectedValue = 1 Then
        '    txtNoChasis.Enabled = False
        'End If
        hdntxtNoChasis.Value = txtNoChasis.Text
        hdnVechileTypeId.Value = oPQRHeader.ChassisMaster.VechileColor.VechileType.ID
        lnkbtnCheckChassisClick()
        txtOdometerPemasangan.Text = CInt(oPQRHeader.InstallOdometer)
        txtOdometer.Text = (CInt(oPQRHeader.OdoMeter))
        txtGejala.Text = oPQRHeader.Symptomps
        txtPemeriksaan.Text = oPQRHeader.Causes
        txtHasil.Text = oPQRHeader.Results
        icTglKerusakan.Value = oPQRHeader.PQRDate.Date
        If ddlClaimType.SelectedValue.Trim = "Z2" And ddlRefDoc.SelectedValue = "1" Then
            icTglPemasangan.Value = oPQRHeader.InstallDate.Date
        End If

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
        For Each _objPQRDamageCode As PQRDamageCode In oPQRHeader.PQRDamageCodes
            Dim objOngkosKeja As WSCDetail = New WSCDetail
            objOngkosKeja.WSCType = "L"
            objOngkosKeja.PositionCode = _objPQRDamageCode.DeskripsiKodePosisi.KodePosition
            _arrONGKOSKERJA.Add(objOngkosKeja)
        Next
        'sessHelper.SetSession("NEW_ONGKOSKERJA", _arrONGKOSKERJA)
        SetSession("NEW_ONGKOSKERJA", _arrONGKOSKERJA)

        Dim _arrONGKOSPARTS As ArrayList = New ArrayList
        For Each _objPQRPartsCode As PQRPartsCode In oPQRHeader.PQRPartsCodes
            Dim objOngkosPart As WSCDetail = New WSCDetail
            objOngkosPart.WSCType = "P"
            objOngkosPart.SparePartMaster = New SparePartMasterFacade(User).Retrieve(_objPQRPartsCode.SparePartMaster.PartNumber)
            _arrONGKOSPARTS.Add(objOngkosPart)
        Next
        'sessHelper.SetSession("NEW_ONGKOSPARTS", _arrONGKOSPARTS)
        SetSession("NEW_ONGKOSPARTS", _arrONGKOSPARTS)
        RefreshGrid()
        setEnabledReferensiDokumen(True)
    End Sub

    Private Sub ResetPQRHeaderInfo()
        txtRefClaimNumber.Text = ""
        txtNoChasis.Text = ""
        hdntxtNoChasis.Value = ""
        hdnVechileTypeId.Value = ""
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
            'sessHelper.SetSession("NEW_ONGKOSKERJA", New ArrayList)
            'sessHelper.SetSession("NEW_ONGKOSPARTS", New ArrayList)
            'sessHelper.SetSession("NEW_WSCEVIDENCE", New ArrayList)
            SetSession("NEW_ONGKOSKERJA", New ArrayList)
            SetSession("NEW_ONGKOSPARTS", New ArrayList)
            SetSession("NEW_WSCEVIDENCE", New ArrayList)
        Else
            'sessHelper.SetSession("ONGKOSKERJA", New ArrayList)
            'sessHelper.SetSession("ONGKOSPARTS", New ArrayList)
            'sessHelper.SetSession("WSCEVIDENCE", New ArrayList)
            SetSession("ONGKOSKERJA", New ArrayList)
            SetSession("ONGKOSPARTS", New ArrayList)
            SetSession("WSCEVIDENCE", New ArrayList)
        End If

        RefreshGrid()
        setEnabledReferensiDokumen(False)
    End Sub

    Private Function LoadChassisMasterPKT(objChassisMasters As ChassisMaster, blnLoginIsSoldDealer As Boolean)
        Dim result As Boolean = False
        Dim arrCMPKT As New ArrayList
        Dim oChassisMasterPKT As ChassisMasterPKT = New ChassisMasterPKT
        Dim oChassisMasterPKTFacade As ChassisMasterPKTFacade = New ChassisMasterPKTFacade(User)
        If Not IsNothing(objChassisMasters) Then
            If blnLoginIsSoldDealer Then
                Dim criteriaCMPKT As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterPKT), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriaCMPKT.opAnd(New Criteria(GetType(ChassisMasterPKT), "ChassisMaster.ID", MatchType.Exact, objChassisMasters.ID))
                arrCMPKT = oChassisMasterPKTFacade.Retrieve(criteriaCMPKT)
                If Not IsNothing(arrCMPKT) AndAlso arrCMPKT.Count > 0 Then
                    oChassisMasterPKT = CType(arrCMPKT(0), ChassisMasterPKT)
                    ViewState("PKTDate") = oChassisMasterPKT.PKTDate
                    result = True
                Else
                    MessageBox.Show("Nomor rangka ini tidak valid, silahkan isi tanggal PKT")
                End If
            Else
                Dim criteriaCMPKT As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterPKT), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriaCMPKT.opAnd(New Criteria(GetType(ChassisMasterPKT), "ChassisMaster.ID", MatchType.Exact, objChassisMasters.ID))
                arrCMPKT = oChassisMasterPKTFacade.Retrieve(criteriaCMPKT)
                If IsNothing(arrCMPKT) OrElse arrCMPKT.Count = 0 Then
                    If Not IsNothing(objChassisMasters.EndCustomer) Then
                        If objChassisMasters.EndCustomer.FakturDate <> CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                            ViewState("PKTDate") = objChassisMasters.EndCustomer.FakturDate
                            result = True
                        Else
                            If Not IsNothing(objChassisMasters.DODate) Then
                                If objChassisMasters.DODate <> CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                                    ViewState("PKTDate") = objChassisMasters.DODate
                                    result = True
                                Else
                                    MessageBox.Show("Nomor rangka ini tidak valid karena tanggal PKT, tanggal faktur dan tanggal DO tidak ada")
                                End If
                            Else
                                MessageBox.Show("Nomor rangka ini tidak valid karena tanggal PKT, tanggal faktur dan tanggal DO tidak ada")
                            End If
                        End If
                    Else
                        If Not IsNothing(objChassisMasters.DODate) Then
                            If objChassisMasters.DODate <> CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                                ViewState("PKTDate") = objChassisMasters.DODate
                                result = True
                            Else
                                MessageBox.Show("Nomor rangka ini tidak valid karena tanggal PKT, tanggal faktur dan tanggal DO tidak ada")
                            End If
                        Else
                            MessageBox.Show("Nomor rangka ini tidak valid karena tanggal PKT, tanggal faktur dan tanggal DO tidak ada")
                        End If
                    End If
                Else
                    oChassisMasterPKT = CType(arrCMPKT(0), ChassisMasterPKT)
                    ViewState("PKTDate") = oChassisMasterPKT.PKTDate
                    result = True
                End If
            End If
        End If

        Return result
    End Function

    Private Sub CheckIsExistChassisMasterPKT(oPQRHeader As PQRHeader, blnLoginIsSoldDealer As Boolean)
        Dim arrCMPKT As New ArrayList
        Dim oChassisMasterPKT As ChassisMasterPKT = New ChassisMasterPKT
        Dim oChassisMasterPKTFacade As ChassisMasterPKTFacade = New ChassisMasterPKTFacade(User)
        If blnLoginIsSoldDealer Then
            'Dim arrCMPKT As New ArrayList
            'Dim oChassisMasterPKTFacade As ChassisMasterPKTFacade = New ChassisMasterPKTFacade(User)
            Dim criteriaCMPKT As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterPKT), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteriaCMPKT.opAnd(New Criteria(GetType(ChassisMasterPKT), "ChassisMaster.ID", MatchType.Exact, oPQRHeader.ChassisMaster.ID))
            arrCMPKT = oChassisMasterPKTFacade.Retrieve(criteriaCMPKT)
            If IsNothing(arrCMPKT) OrElse arrCMPKT.Count = 0 Then
                ResetPQRHeaderInfo()
                MessageBox.Show("Nomor rangka ini tidak valid, silahkan lengkapi data tanggal PKT terlebih dahulu")
                btnBatal_Click()
            Else
                oChassisMasterPKT = CType(arrCMPKT(0), ChassisMasterPKT)
                ViewState("PKTDate") = oChassisMasterPKT.PKTDate
                LoadPQRHeaderInfo(oPQRHeader)
            End If
        Else
            'Dim arrCMPKT As New ArrayList
            'Dim oChassisMasterPKTFacade As ChassisMasterPKTFacade = New ChassisMasterPKTFacade(User)
            Dim criteriaCMPKT As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterPKT), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteriaCMPKT.opAnd(New Criteria(GetType(ChassisMasterPKT), "ChassisMaster.ID", MatchType.Exact, oPQRHeader.ChassisMaster.ID))
            arrCMPKT = oChassisMasterPKTFacade.Retrieve(criteriaCMPKT)
            If IsNothing(arrCMPKT) OrElse arrCMPKT.Count = 0 Then
                If Not IsNothing(oPQRHeader.ChassisMaster) Then
                    If Not IsNothing(oPQRHeader.ChassisMaster.EndCustomer) Then
                        If oPQRHeader.ChassisMaster.EndCustomer.FakturDate <> CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                            ViewState("PKTDate") = oPQRHeader.ChassisMaster.EndCustomer.FakturDate
                            LoadPQRHeaderInfo(oPQRHeader)
                        Else
                            If Not IsNothing(oPQRHeader.ChassisMaster.DODate) Then
                                If oPQRHeader.ChassisMaster.DODate <> CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                                    ViewState("PKTDate") = oPQRHeader.ChassisMaster.DODate
                                    LoadPQRHeaderInfo(oPQRHeader)
                                Else
                                    ResetPQRHeaderInfo()
                                    MessageBox.Show("Nomor rangka ini tidak valid, tanggal faktur belum ada")
                                    btnBatal_Click()
                                End If
                            Else
                                ResetPQRHeaderInfo()
                                MessageBox.Show("Nomor rangka ini tidak valid, tanggal faktur belum ada")
                                btnBatal_Click()
                            End If
                        End If
                    Else
                        If Not IsNothing(oPQRHeader.ChassisMaster.DODate) Then
                            If oPQRHeader.ChassisMaster.DODate <> CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                                ViewState("PKTDate") = oPQRHeader.ChassisMaster.DODate
                                LoadPQRHeaderInfo(oPQRHeader)
                            Else
                                ResetPQRHeaderInfo()
                                MessageBox.Show("Nomor rangka ini tidak valid karena tanggal PKT, tanggal faktur dan tanggal DO tidak ada")
                                btnBatal_Click()
                            End If
                        Else
                            ResetPQRHeaderInfo()
                            MessageBox.Show("Nomor rangka ini tidak valid, tanggal faktur belum ada")
                            btnBatal_Click()
                        End If
                    End If
                End If
            Else
                oChassisMasterPKT = CType(arrCMPKT(0), ChassisMasterPKT)
                ViewState("PKTDate") = oChassisMasterPKT.PKTDate
                LoadPQRHeaderInfo(oPQRHeader)
            End If
        End If
    End Sub

    Private Sub txtRefClaimNumber_TextChanged(sender As Object, e As EventArgs) Handles txtRefClaimNumber.TextChanged
        oDealer = CType(GetSession("DEALER"), Dealer)
        If txtRefClaimNumber.Text.Trim <> "" AndAlso txtNoChasis.Text.Trim <> "" Then
            Dim oWSCHeader As WSCHeader = New WSCHeaderFacade(User).Retrieve(oDealer, txtRefClaimNumber.Text.Trim, txtNoChasis.Text.Trim)
            If oWSCHeader.ID > 0 Then
                icTglPemasangan.Value = oWSCHeader.InstallDate.Date
                icTglKerusakan.Value = oWSCHeader.FailureDate.Date
            End If
        End If
    End Sub

    Private Sub txtPQRNo_TextChanged(sender As Object, e As EventArgs) Handles txtPQRNo.TextChanged
        'oDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        oDealer = CType(GetSession("DEALER"), Dealer)
        If txtPQRNo.Text.Trim <> "" Then
            If (ddlClaimType.SelectedValue.Trim = "ZA" _
                OrElse ddlClaimType.SelectedValue.Trim = "Z2" _
                OrElse ddlClaimType.SelectedValue.Trim = "ZB" _
                OrElse ddlClaimType.SelectedValue.Trim = "Z6") Then
                If ddlRefDoc.SelectedValue = "1" Then  'PQR Reference
                    Dim oPQRHeader As PQRHeader = RetrievePQR(txtPQRNo.Text)
                    If oPQRHeader.ID > 0 Then
                        Try
                            If oPQRHeader.PQRType = 0 OrElse oPQRHeader.PQRType = 2 OrElse oPQRHeader.PQRType = 3 OrElse oPQRHeader.PQRType = 4 Then
                                txtRefClaimNumber.Enabled = True
                                lnkbtnPopUpRefClaimNumber.Visible = True
                                If oDealer.ID = oPQRHeader.ChassisMaster.Dealer.ID Then  '---Jika login dealer id = sold dealer id
                                    CheckIsExistChassisMasterPKT(oPQRHeader, True)
                                Else
                                    CheckIsExistChassisMasterPKT(oPQRHeader, False)
                                End If
                                setDisableKodeKerusakan()
                                'If ddlClaimType.SelectedValue.Trim = "Z2" Then
                                If ddlRefDoc.SelectedValue = "1" Then
                                    txtNoChasis.Enabled = False
                                    txtNoChasis.ReadOnly = True
                                End If
                                If Mode = enumMode.Mode.NewItemMode AndAlso ddlRefDoc.SelectedIndex = 2 Then
                                    secondTimeInput(oPQRHeader)
                                End If

                                Dim arrAttach As ArrayList = New PQRAttachmentFacade(User).Retrieve(oPQRHeader)
                                If arrAttach.Count > 0 Then
                                    Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("WSCEvidenceFileDirectory") & "\" & lblDealerVal.Text & "\"
                                    Dim _arrWSCEVIDENCE As New ArrayList
                                    For Each pqrAtt As PQRAttachment In arrAttach
                                        'copy
                                        Dim newFileNameFormat As String = txtNoChasis.Text.Trim & "-" & oDealer.DealerCode & "-" & "{0}-" & DateTime.Now.ToString("ffff") & Path.GetExtension(pqrAtt.Attachment)
                                        Dim objWSCEvidence As New WSCEvidence
                                        objWSCEvidence.EvidenceType = 5
                                        objWSCEvidence.Description = EnumWSCEvidenceType.GetStringWSCEvType(5)
                                        objWSCEvidence.PathFile = DestFile & newFileNameFormat
                                        objWSCEvidence.UploadDate = DateTime.Now.Day
                                        objWSCEvidence.UploadMonth = DateTime.Now.Month
                                        objWSCEvidence.UploadYear = DateTime.Now.Year
                                        objWSCEvidence.WSCHeader = Nothing
                                        'objWSCEvidence.AttachmentData = Request.Files(0)

                                        Dim filePath = KTB.DNet.Lib.WebConfig.GetValue("SAN") & pqrAtt.Attachment
                                        objWSCEvidence.IsFromPQR = True
                                        objWSCEvidence.PQRFilePath = filePath
                                        objWSCEvidence.TempFilePath = DestFile & newFileNameFormat
                                        copyAndRenameReferencedFile(filePath, DestFile + newFileNameFormat)
                                        _arrWSCEVIDENCE.Add(objWSCEvidence)

                                    Next

                                    If Mode = enumMode.Mode.NewItemMode Then
                                        'sessHelper.SetSession("NEW_WSCEVIDENCE", _arrWSCEVIDENCE)
                                        SetSession("NEW_WSCEVIDENCE", _arrWSCEVIDENCE)
                                    Else
                                        'sessHelper.SetSession("WSCEVIDENCE", _arrWSCEVIDENCE)
                                        SetSession("WSCEVIDENCE", _arrWSCEVIDENCE)
                                    End If

                                    dgFileWSCEvidence.DataSource = _arrWSCEVIDENCE
                                    dgFileWSCEvidence.DataBind()
                                    If _arrWSCEVIDENCE.Count > 0 Then
                                        downloadAllEvidence.Visible = True
                                    End If
                                End If
                            ElseIf oPQRHeader.PQRType = 1 Then
                                MessageBox.Show("Dokumen PQR Tidak dapat digunakan")
                                ddlRefDoc_SelectedIndexChanged()
                                Return
                            End If
                        Catch
                            MessageBox.Show("Dokumen tidak valid")
                            ddlRefDoc_SelectedIndexChanged()
                        End Try
                    Else
                        MessageBox.Show("Dokumen tidak valid")
                        ddlRefDoc_SelectedIndexChanged()
                    End If
                    icTglKerusakan.Enabled = False
                ElseIf ddlRefDoc.SelectedValue = "0" Then   'Buletin reference
                    txtRefClaimNumber.Enabled = True
                    lnkbtnPopUpRefClaimNumber.Visible = True
                    lblSearchChassis.Visible = True
                    lnkbtnCheckChassis.Visible = True
                    txtNoChasis.Enabled = True
                    'txtNoChasis.ReadOnly = False
                End If
            Else
                'type Z4
                Dim oRecallCategory As RecallCategory = New RecallCategoryFacade(User).Retrieve(txtPQRNo.Text)
                If Not IsNothing(oRecallCategory) Then
                    lnkbtnPopUpRefClaimNumber.Visible = True
                    ResetPQRHeaderInfo()
                    txtGejala.Text = oRecallCategory.Description
                    txtNoChasis.Enabled = True
                    'txtNoChasis.ReadOnly = True
                    lblSearchChassis.Visible = True
                    lnkbtnCheckChassis.Attributes("style") = "display:table-row"
                Else
                    lblSearchChassis.Visible = False
                    lnkbtnCheckChassis.Attributes("style") = "display:none"
                End If
            End If
        Else
            ResetPQRHeaderInfo()
            setEnabledReferensiDokumen(False)
            txtNoChasis.Enabled = False
            txtRefClaimNumber.Enabled = False
            lnkbtnPopUpRefClaimNumber.Visible = False
            lblSearchChassis.Visible = False
            lnkbtnCheckChassis.Attributes("style") = "display:none"
            lnkbtnPopUpInfoKendaraan.Visible = False
        End If
    End Sub


#Region "Send Email"
    Private Function MailReport(ByVal vCode As String, ByVal _arl As ArrayList) As Boolean
        If _arl.Count <= 0 Then Return False

        Dim oLaborMaster As LaborMaster
        Dim sTO As String = KTB.DNet.Lib.WebConfig.GetString("EmailToWSCRequestNewLaborMaster")
        Dim sCC As String = KTB.DNet.Lib.WebConfig.GetString("EmailCcWSCRequestNewLaborMaster")
        Dim subject As String = "Alert: Request New Labor Master"
        Dim strClaimNumber As String = String.Empty
        Dim strDealerCode As String = String.Empty

        Dim Dir As String = Server.MapPath(TEMP_EMAIL_NOLABORMASTER)
        Try
            'Dim objDealer As Dealer = Me.sessHelper.GetSession("DEALER")
            Dim objDealer As Dealer = Me.GetSession("DEALER")
            Dim str = New StringBuilder()
            For Each oWSCD As WSCDetail In _arl
                If strClaimNumber.Trim = "" Then
                    strClaimNumber = oWSCD.WSCHeader.ClaimNumber
                End If
                str.Append("<tr>" & _
                        "<td><center>" & vCode & "</center></td>" & _
                        "<td><center>" & oWSCD.PositionCode.ToString & "</center></td>" & _
                        "<td><center>" & oWSCD.WorkCode.ToString & "</center></td>" & _
                        "</tr>")
            Next
            Dim sContents() As String = {str.ToString()}
            strDealerCode = objDealer.DealerCode & "&nbsp;/&nbsp;" & objDealer.SearchTerm1

            Me.SendEmail(Dir, sTO, sCC, subject, sContents, strDealerCode, strClaimNumber)

            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub SendEmail(ByVal EmailFile As String, ByVal sTo As String, ByVal sCC As String, ByVal sSubject As String, ByVal sMessage() As String, ByVal strDealerCode As String, ByVal strClaimNumber As String)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetString("SMTPWSC")
        Dim ObjEmail As DNetMail = New DNetMail(smtp)
        Dim emailAdmin As String = KTB.DNet.Lib.WebConfig.GetString("EmailFromAdminWSCRequestNewLaborMaster")
        Dim emailFrom As String = KTB.DNet.Lib.WebConfig.GetString("EmailFromWSCRequestNewLaborMaster")
        Dim sr As System.IO.StreamReader = New System.IO.StreamReader(EmailFile)
        Dim szEmailFormat As String = sr.ReadToEnd()
        sr.Close()
        Dim szEmailContent As String = String.Format(szEmailFormat, sMessage(0), strDealerCode, strClaimNumber)
        If Not IsNothing(sCC) AndAlso sCC.Trim() <> "" AndAlso sCC.EndsWith(";") = False AndAlso Not IsNothing(emailAdmin) AndAlso emailAdmin <> "" Then
            emailAdmin = ";" & emailAdmin
        End If

        ObjEmail.sendMail(sTo, sCC, emailAdmin, emailFrom, sSubject, MailFormat.Html, szEmailContent)
    End Sub

    Private Function LoadPositionAndWorkCodes(ByVal chassisNumber As String, ByVal recallRegNo As String) As Integer
        Dim strSql As String
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        Dim _arrONGKOSKERJA As ArrayList = New ArrayList
        'Dim _arrONGKOSKERJA As ArrayList = CType(sessHelper.GetSession("NEW_ONGKOSKERJA"), ArrayList)
        'If Mode = enumMode.Mode.EditMode Then
        '    _arrONGKOSKERJA = CType(sessHelper.GetSession("ONGKOSKERJA"), ArrayList)
        'End If

        strSql += "select lm.LaborCode as PositionCode, lm.WorkCode as WorkCode from ChassisMaster cm"
        strSql += " Join VechileColor cv on cm.VechileColorID = cv.ID"
        strSql += " Join VechileType ct on cv.VechileTypeID = ct.ID"
        strSql += " Join LaborMaster lm on lm.VechileTypeID = ct.ID"
        strSql += " Join RecallCategoryDetail rcd on rcd.LaborMasterID = lm.id"
        strSql += " Join RecallCategory rc on rc.ID = rcd.RecallCategoryID"
        strSql += " where cm.RowStatus = 0 AND cv.RowStatus = 0 AND ct.RowStatus = 0 AND lm.RowStatus = 0 AND rcd.RowStatus = 0 AND rc.RowStatus = 0 "
        strSql += " AND cm.ChassisNumber = '" + chassisNumber + "' and rc.RecallRegNo = '" + recallRegNo + "'"


        Dim CariPosisiAmaWorkCode As DataSet = New RecallCategoryDetailFacade(User).DoRetrieveDataset(strSql)
        Dim objWSCDetail As WSCDetail

        For Each dr As DataRow In CariPosisiAmaWorkCode.Tables(0).Rows
            objWSCDetail = New WSCDetail()
            objWSCDetail.PositionCode = CType(dr("PositionCode"), String)
            objWSCDetail.WorkCode = CType(dr("WorkCode"), String)
            _arrONGKOSKERJA.Add(objWSCDetail)
            If Mode = enumMode.Mode.NewItemMode Then
                'sessHelper.SetSession("NEW_ONGKOSKERJA", _arrONGKOSKERJA)
                SetSession("NEW_ONGKOSKERJA", _arrONGKOSKERJA)
            Else
                'sessHelper.SetSession("ONGKOSKERJA", _arrONGKOSKERJA)
                SetSession("ONGKOSKERJA", _arrONGKOSKERJA)
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
        'Dim _arrONGKOSKERJA As ArrayList = CType(sessHelper.GetSession("ONGKOSKERJA"), ArrayList)
        Dim _arrONGKOSKERJA As ArrayList = CType(GetSession("ONGKOSKERJA"), ArrayList)
        'oWSCHeader = CType(sessHelper.GetSession("WSCHEADER"), WSCHeader)
        oWSCHeader = CType(GetSession("WSCHEADER"), WSCHeader)
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        If Mode = enumMode.Mode.NewItemMode Then
            '_arrONGKOSKERJA = CType(sessHelper.GetSession("NEW_ONGKOSKERJA"), ArrayList)
            _arrONGKOSKERJA = CType(GetSession("NEW_ONGKOSKERJA"), ArrayList)
            'oWSCHeader = CType(sessHelper.GetSession("NEW_WSCHEADER"), WSCHeader)
            oWSCHeader = CType(GetSession("NEW_WSCHEADER"), WSCHeader)
        End If
        Dim intJmlMasterValid As Short = 0
        For Each _objWSCDetail As WSCDetail In _arrONGKOSKERJA
            GetObjectLaborMaster(_objWSCDetail.PositionCode.Trim(), _objWSCDetail.WorkCode.Trim(), oWSCHeader.ChassisMaster.VechileColor.VechileType.ID)
            If Not IsNothing(objLaborMaster) Then
                intJmlMasterValid += 1
            End If
        Next
        If Not IsNothing(_arrONGKOSKERJA) AndAlso _arrONGKOSKERJA.Count <= 0 Then
            MessageBox.Show("Data grid Ongkos Kerja masih kosong")
            Return ListOpenWSC
        End If
        If intJmlMasterValid <> _arrONGKOSKERJA.Count Then
            MessageBox.Show("Seluruh data master labor harus valid pada item grid Ongkos Kerja")
            Return ListOpenWSC
        End If
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(WSCHeader), "ID", MatchType.Exact, oWSCHeader.ID))

        Dim objWSCHeaderFacade As WSCHeaderFacade
        objWSCHeaderFacade = New WSCHeaderFacade(User)

        Dim sorts As SortCollection = New SortCollection
        sorts.Add(New Sort(GetType(WSCHeader), "Dealer.DealerCode"))

        If LoopDgrid("ALLGRID", Nothing, Nothing, "Simpan", 0) Then
            ListOpenWSC = objWSCHeaderFacade.RetrieveByCriteria(criterias, sorts)
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

        If isLaborNull2() Then
            Return False
        End If

        Dim success As Boolean = False
        Dim sTimestamp As String = sSuffix()
        Dim WSCFileNameSAP As String = KTB.DNet.Lib.WebConfig.GetValue("SAPSERVERFOLDER") & "\Service\WSC\MMC\" & "WSCData" & sTimestamp & ".wsd"
        Dim WSCFileNameLocal As String = Server.MapPath("") & "\..\DataTemp\WSCData" & sTimestamp & ".txt"

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
                Dim objWSCHeaderColl As ArrayList = New ArrayList
                If arlTransferedToSAP.Count > 0 Then
                    For Each ObjWsCHeader As WSCHeader In CheckedWSCItemColl
                        ObjWsCHeader.Status = CType(enumStatusWSC.Status.Proses, String)
                        ObjWsCHeader.ReleaseDate = DateTime.Now
                        objWSCHeaderColl.Add(ObjWsCHeader)
                    Next
                    Dim nResult = New WSCHeaderFacade(User).UpdateWSCUploadedToSAP(objWSCHeaderColl)
                    If nResult = 0 Then
                        btnRilis.Enabled = False
                        btnSave.Enabled = False
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
        loopOngker("Rilis")
    End Function

    Private Function IsDownloaded() As Boolean
        'debug
        'Return True
        Dim _return As Boolean = False
        Dim objDealer As Dealer = CType(Session.Item("DEALER"), Dealer)
        Dim ArlMonthly As ArrayList = New ArrayList
        Try
            Dim paramDate As DateTime = New DateTime(1900, 1, 1)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "ProductCategoryID", MatchType.Exact, "1"))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "DealerCode", MatchType.Exact, objDealer.DealerCode))
            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "Kind", MatchType.InSet, "(1,6,7)"))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "Kind", MatchType.InSet, "(1,6,7,22)"))

            'criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.V_MonthlyReport), "PeriodeYear", MatchType.Exact, "2017"), "((", True)
            'criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.V_MonthlyReport), "PeriodeMonth", MatchType.GreaterOrEqual, "4"), ")", False)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "PeriodeMonth", MatchType.GreaterOrEqual, "4"))

            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "PeriodeYear", MatchType.GreaterOrEqual, "2017"))
            'criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "PeriodeYear", MatchType.Greater, "2017"), "(", True)
            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "PeriodeMonth", MatchType.GreaterOrEqual, "1"), ")", False)

            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "CreatedTime", MatchType.GreaterOrEqual, Date.Now.AddMonths(-1)))
            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "Period", MatchType.GreaterOrEqual, New DateTime(2017, 4, 1)))


            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "LastDownloadDate", MatchType.IsNull, True), "(", True)
            'criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "LastDownloadDate", MatchType.Exact, New DateTime(1900, 1, 1)), "", False)
            'criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "LastDownloadDate", MatchType.Exact, CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)), "", False)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "LastDownloadDate", MatchType.Exact, CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)), "(", True)
            criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "TransferDate", MatchType.Exact, CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)), ")", False)
            'criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "TransferDate", MatchType.Exact, CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)), "", False)
            'criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "TransferDate", MatchType.Exact, New DateTime(1900, 1, 1)), "", False)
            'criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "TransferDate", MatchType.IsNull, True), ")", False)

            Dim minMonth = 0
            If Date.Now.Day >= 20 Then
                minMonth = -1
            Else
                minMonth = -2
            End If
            Dim dtn As DateTime = New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(minMonth)


            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "Period", MatchType.LesserOrEqual, dtn))


            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(V_MonthlyReport), "Period", Sort.SortDirection.DESC))


            ArlMonthly = New V_MonthlyReportFacade(User).Retrieve(criterias, sortColl)
            If Not IsNothing(ArlMonthly) AndAlso ArlMonthly.Count > 0 Then
                Dim vM As New V_MonthlyReport
                vM = CType(ArlMonthly(0), V_MonthlyReport)

                If 1 = 1 OrElse (vM.Period.Year = dtn.Year AndAlso dtn.Month = vM.Period.Month) Then
                    _return = False
                Else
                    Return True
                End If

                _return = False
            Else
                _return = True
            End If
        Catch ex As Exception
            _return = False
        End Try
        Return _return
    End Function

    Private Sub btnRilis_Click(sender As Object, e As EventArgs) Handles btnRilis.Click
        If PQRStatusBeforRelease() Then
            If btnRilis_Click() Then
                btnBatal_Click()
            End If
        End If
    End Sub

    Private Function sSuffix() As String
        'If Not IsNothing(ViewState(V_Suffix)) Then
        '    Return CType(ViewState(V_Suffix), String)
        'Else
        Return DateTime.Now.ToString("yyyyMMddHHmmss")
        'End If
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
                For Each wscHdr As WSCHeader In ArrCheckedWSCItem
                    For Each wscDetail As WSCDetail In wscHdr.WSCDetails
                        strText.Append("""")
                        strText.Append(wscHdr.ClaimType)
                        strText.Append(delimiter)
                        strText.Append(wscHdr.Dealer.DealerCode)
                        strText.Append(delimiter)
                        strText.Append(wscHdr.ClaimNumber)
                        strText.Append(delimiter)
                        strText.Append(wscHdr.RefClaimNumber)
                        strText.Append(delimiter)
                        strText.Append(wscHdr.ChassisMaster.ChassisNumber)
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
                        If wscHdr.Description.Length > 50 Then
                            strText.Append(wscHdr.Description.Substring(0, 50))
                        Else
                            strText.Append(wscHdr.Description)
                        End If
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
                            strText.Append(delimiter)
                            strText.Append("")
                            'masih nunggu sap
                            strText.Append(delimiter)
                            If wscDetail.MainPart = 1 Then
                                strText.Append("X")
                            Else
                                strText.Append("")
                            End If
                        End If
                        strText.Append(delimiter)
                        strText.Append(wscDetail.WSCHeader.EvidenceRepair)
                        strText.Append(delimiter)
                        strText.Append(wscDetail.WSCHeader.EvidenceWSCLetter)
                        strText.Append(delimiter)
                        strText.Append(wscDetail.WSCHeader.EvidenceWSCTechnical)
                        strText.Append(delimiter)
                        strText.Append(wscDetail.WSCHeader.FailureDate.ToString("ddMMyyyy"))

                        'Dim strKWITANSI_WSC As String = String.Empty
                        'Dim strSURAT_WSC As String = String.Empty
                        'Dim strTEKNIKAL_WSC As String = String.Empty
                        'Dim strREPAIR_WO As String = String.Empty
                        'Dim strPART_BEKAS As String = String.Empty
                        'Dim strPHOTO As String = String.Empty
                        'Dim criterias As New CriteriaComposite(New Criteria(GetType(WSCEvidence), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        'criterias.opAnd(New Criteria(GetType(WSCEvidence), "WSCHeader.ID", MatchType.Exact, wscHdr.ID))
                        'Dim WSCEvidenceList As ArrayList = New WSCEvidenceFacade(User).Retrieve(criterias)
                        'If WSCEvidenceList.Count > 0 Then
                        '    Dim _evidence As New WSCEvidence

                        '    For Each _evidence In WSCEvidenceList
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

                        'strText.Append(strREPAIR_WO)
                        'strText.Append(delimiter)
                        'strText.Append(strSURAT_WSC)
                        'strText.Append(delimiter)
                        'strText.Append(strTEKNIKAL_WSC)
                        'strText.Append(delimiter)

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
                'System.IO.File.Copy(DestFileLocal, DestFile & ".wts")
                imp.StopImpersonate()
                imp = Nothing
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
        Dim facade As WSCDetailFacade = New WSCDetailFacade(User)
        Dim countPartItem As Integer = 0
        Dim countChecklist As Integer = 0
        Dim i As Integer = 0
        For Each item As DataGridItem In dgParts.Items
            Dim chk As CheckBox = CType(item.FindControl("chkItemChecked"), CheckBox)
            Dim qty As TextBox = CType(item.FindControl("txtQtyItem"), TextBox)
            If (chk.Checked) Then
                Dim obj As WSCDetail = facade.Retrieve(Convert.ToInt32(dgParts.DataKeys().Item(i)))
                If obj.WSCType.Trim.ToUpper = "P" Then
                    For q As Integer = 1 To CType(qty.Text, Integer)
                        arlPrintBarcode.Add(obj)
                        If strIdTmp.Trim = "" Then
                            strIdTmp += obj.ID.ToString
                        Else
                            strIdTmp += ";" & obj.ID.ToString
                        End If
                    Next
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

        Server.Transfer("~/Service/FrmWSCHeaderPrintBarcode.aspx?claimNumber=" & lblClaimNumber.Text & "&id=" & strIdTmp & "&viewStateMode=" & intViewStateMode & "&screenFrom=" & screenFrom & "&PQRId=" & pqrId & "&WSCId=" & wscId)
    End Sub

#Region "Parameter"

    Private Function ValidateByParameter()
        Dim paramCont As ArrayList = New ArrayList
        Dim errorIn As String = String.Empty
        Dim RejectCode As String = String.Empty
        Dim RCode As String = String.Empty
        Dim typeCode As String = String.Empty
        Dim paramHeaderDesc As String = String.Empty
        Dim rejectionsResult As ArrayList = New ArrayList
        ViewState("UncoverPart") = Nothing

        '1. Ambil Vtype ID
        Dim criteVType As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteVType.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, txtNoChasis.Text))
        Dim oChaMasFas As ChassisMaster = New ChassisMasterFacade(User).Retrieve(criteVType)(0)
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
        Dim PKTDate As String = lblTglPKTVal.Text
        Dim DmgDate As String = icTglKerusakan.Value
        Dim RepDate As String = icTglPerbaikan.Value
        Dim Odo As Integer = CType(txtOdometer.Text, Integer)
        Dim detailIndex As Integer = 0
        Dim PositionCode As ArrayList
        If Mode = enumMode.Mode.NewItemMode Then
            'PositionCode = sessHelper.GetSession("NEW_ONGKOSKERJA")
            PositionCode = GetSession("NEW_ONGKOSKERJA")
        Else
            'PositionCode = sessHelper.GetSession("ONGKOSKERJA")
            PositionCode = GetSession("ONGKOSKERJA")
        End If
        Dim WorkCode As ArrayList
        If Mode = enumMode.Mode.NewItemMode Then
            'WorkCode = sessHelper.GetSession("NEW_ONGKOSPARTS")
            WorkCode = GetSession("NEW_ONGKOSPARTS")
        Else
            'WorkCode = sessHelper.GetSession("ONGKOSPARTS")
            WorkCode = GetSession("ONGKOSPARTS")
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

        Dim validParameters As ArrayList = New ArrayList

        '2. Ambil Header ID
        Dim criteVehicle As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCParameterVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteVehicle.opAnd(New Criteria(GetType(WSCParameterVehicle), "VechileType.ID", MatchType.Exact, VechileTypeID))
        criteVehicle.opAnd(New Criteria(GetType(WSCParameterVehicle), "WSCParameterHeader.Status", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteVehicle.opAnd(New Criteria(GetType(WSCParameterVehicle), "WSCParameterHeader.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteVehicle.opAnd(New Criteria(GetType(WSCParameterVehicle), "WSCParameterHeader.ClaimType", MatchType.Exact, ClaimType)) 'Changes 26/06/2018

        Dim oWSCParamVe As WSCParameterVehicleFacade = New WSCParameterVehicleFacade(User)
        Dim arrParamHead As ArrayList = oWSCParamVe.Retrieve(criteVehicle)
        If arrParamHead.Count <= 0 Then
            MessageBox.Show("Tipe kendaraan " & typeCode & " belum terdaftar pada parameter. \nSilahkan menghubungi MMKSI")
            Return False
        End If
        For Each oParamHead As WSCParameterVehicle In arrParamHead
            Dim tempRejectionsResult As ArrayList = New ArrayList

            '3. ambil Detail
            Dim criteDetail As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCParameterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteDetail.opAnd(New Criteria(GetType(WSCParameterDetail), "WSCParameterHeader.ID", MatchType.Exact, oParamHead.WSCParameterHeader.ID))
            Dim oWSCParamDetail As WSCParameterDetailFacade = New WSCParameterDetailFacade(User)
            Dim arrParamDetail As ArrayList = oWSCParamDetail.Retrieve(criteDetail)

            'Ambil condition
            Dim arrCondition As ArrayList = New ArrayList
            Dim criteria1 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCParameterCondition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria1.opAnd(New Criteria(GetType(WSCParameterCondition), "WSCParameterHeader.ID", MatchType.Exact, oParamHead.WSCParameterHeader.ID))
            arrCondition = New WSCParameterConditionFacade(User).Retrieve(criteria1)

            'validaate condition
            Dim conditionIsValid As Boolean = validateCondition1(arrCondition)
            If Not conditionIsValid Then
                Continue For
            End If

            Dim conditionValidation As ArrayList = GetSession("conditionValidation")
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
                        If blnIsSoldDealer OrElse PKTDate <> String.Empty Then
                            If PKTDate.Trim.Length = 0 Then
                                Dim oEndCust As EndCustomer = New EndCustomerFacade(User).Retrieve(EndCustomerID)
                                If oEndCust.FakturDate = Date.MinValue Then
                                    Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, oEndCust.OpenFakturDate)
                                    Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamDetail.Kind)
                                    vals = oEndCust.OpenFakturDate
                                Else
                                    Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, oEndCust.FakturDate)
                                    Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamDetail.Kind)
                                    vals = oEndCust.FakturDate
                                End If
                            Else
                                Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, PKTDate)
                                Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamDetail.Kind)
                                vals = PKTDate
                            End If
                        End If
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
                        Dim tempResult As ArrayList = New ArrayList

                        If arrCondition.Count > 0 Then
                            If arrCondition.Contains(conditionValidation(0)) Then
                                MessageBox.Show("validasinya benar dan nyata adanya")
                                For Each item As WSCDetail In PositionCode
                                    Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, item.PositionCode)
                                    tempResult.Add(Result)
                                    Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamDetail.Kind)
                                    vals = item.PositionCode
                                Next

                                For Each r As Boolean In tempResult
                                    Result = Result And r
                                Next
                            Else
                                Result = True
                            End If
                        Else
                            For Each item As WSCDetail In PositionCode
                                Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, item.PositionCode)
                                tempResult.Add(Result)
                                Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamDetail.Kind)
                                vals = item.PositionCode
                            Next

                            For Each r As Boolean In tempResult
                                Result = Result And r
                            Next
                        End If


                        'If countKODEPOSISI > 0 Then GoTo jump

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
                        If PKTDate.Trim.Length = 0 Then
                            Dim oEndCust As EndCustomer = New EndCustomerFacade(User).Retrieve(EndCustomerID)
                            If oEndCust.FakturDate = Date.MinValue Then
                                Dim ValDate As Integer = (oEndCust.OpenFakturDate - CType(RepDate, Date)).Days
                                If ValDate < 0 Then
                                    ValDate = ValDate * -1
                                End If
                                Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, ValDate)
                                Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamDetail.Kind)
                                vals = ValDate
                            Else
                                Dim ValDate As Integer = (oEndCust.FakturDate - CType(RepDate, Date)).Days
                                If ValDate < 0 Then
                                    ValDate = ValDate * -1
                                End If
                                Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, ValDate)
                                Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamDetail.Kind)
                                vals = ValDate
                            End If
                        Else
                            Dim ValDate As Integer = (CType(PKTDate, Date) - CType(RepDate, Date)).Days
                            If ValDate < 0 Then
                                ValDate = ValDate * -1
                            End If
                            Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, ValDate)
                            Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamDetail.Kind)
                            vals = ValDate
                        End If
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
                        Dim tempResult As ArrayList = New ArrayList

                        For Each item As WSCDetail In PositionCode
                            If Not IsNothing(oParamDetail.WSCParameterCondition) Then
                                If paramDetailKind(oParamDetail.WSCParameterCondition.Kind, oParamDetail.WSCParameterCondition.Operators,
                                                                oParamDetail.WSCParameterCondition.Value, item.PositionCode) Then
                                    Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators,
                                                                oParamDetail.Value, item.WorkCode)
                                    tempResult.Add(Result)
                                End If
                            Else
                                Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, item.WorkCode)
                                tempResult.Add(Result)
                            End If

                            'Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, item.WorkCode)
                            Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamDetail.Kind)
                            vals = item.WorkCode
                        Next

                        For Each r As Boolean In tempResult
                            Result = Result And r
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

                        'If countPART > 0 Then GoTo jump

                        countPART = countPART + 1

                        If dgParts.Items.Count = 0 Then
                            Result = True
                        Else
                            For Each item As DataGridItem In dgParts.Items

                                Dim txtKodePartsItem As TextBox = item.FindControl("txtKodePartsItem")

                                If Not IsNothing(oParamDetail.WSCParameterCondition) Then
                                    Dim condition As WSCParameterCondition = (From c As WSCParameterCondition In arrCondition Where c.ID = oParamDetail.WSCParameterCondition.ID Select c)(0)
                                    For Each condValid As ArrayList In conditionValidation
                                        Dim cond As WSCParameterCondition = condValid(0)
                                        If cond.ID = condition.ID Then
                                            If condValid(2) = True Then
                                                Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, txtKodePartsItem.Text)
                                            Else
                                                Result = True
                                            End If
                                        End If
                                    Next
                                Else
                                    Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, txtKodePartsItem.Text)
                                End If

                                Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamDetail.Kind)
                                vals = txtKodePartsItem.Text

                                Dim params As String() = oParamDetail.Value.Split(";")

                                If oParamDetail.Operators = 9 Then ' Terdiri dari
                                    For Each row As String In params

                                        If txtKodePartsItem.Text.ToUpper() = row.ToUpper() Then
                                            isPART = True
                                            Exit For
                                        Else
                                            isPART = False
                                        End If

                                    Next
                                ElseIf oParamDetail.Operators = 10 Then ' Tidak terdiri dari
                                    For Each row As String In params

                                        If txtKodePartsItem.Text.ToUpper() <> row.ToUpper() Then
                                            isPART = True
                                        Else
                                            isPART = False
                                            Exit For
                                        End If

                                    Next
                                End If

                                If Not isPART Then
                                    If IsNothing(ViewState("UncoverPart")) Then
                                        ViewState("UncoverPart") = txtKodePartsItem.Text.ToUpper
                                    Else
                                        If Not CType(ViewState("UncoverPart"), String).Contains(txtKodePartsItem.Text.ToUpper) Then
                                            ViewState("UncoverPart") = ViewState("UncoverPart") & "," & txtKodePartsItem.Text.ToUpper
                                        End If
                                    End If
                                End If

                                If Not Result AndAlso Not IsNothing(ViewState("UncoverPart")) Then
                                    Dim findResult As ArrayList = rejectionsResult
                                    If findResult.Count > 0 Then
                                        Dim res = (From rejects As ArrayList In findResult
                                                  Where rejects(0).ToString = oParamHead.WSCParameterHeader.Description AndAlso rejects(1).ToString = oParamDetail.ReasonCode
                                                  Select rejects).Count

                                        RCode = RejectCode.Replace(";", "\n")
                                        errorIn += Kinds & " Operator " & New EnumWSCParamParameter().RetrieveWSCParamOperator(oParamDetail.Operators).ToLower & "  " & oParamDetail.Value & " dan Page " & vals & ";"

                                        Dim tempRejection As ArrayList = New ArrayList
                                        tempRejection.Add(oParamHead.WSCParameterHeader.Description)
                                        tempRejection.Add(oParamDetail.ReasonCode)

                                        If res = 0 Then
                                            rejectionsResult.Add(tempRejection)
                                        End If
                                    Else
                                        RCode = RejectCode.Replace(";", "\n")
                                        errorIn += Kinds & " Operator " & New EnumWSCParamParameter().RetrieveWSCParamOperator(oParamDetail.Operators).ToLower & "  " & oParamDetail.Value & " dan Page " & vals & ";"

                                        Dim tempRejection As ArrayList = New ArrayList
                                        tempRejection.Add(oParamHead.WSCParameterHeader.Description)
                                        tempRejection.Add(oParamDetail.ReasonCode)

                                        rejectionsResult.Add(tempRejection)
                                    End If
                                End If
                            Next
                        End If

                        If isKODEPOSISI And dgParts.Items.Count = 0 Then
                            isPARTBlank = True
                        End If

                        If isKODEPOSISI And dgParts.Items.Count = 1 And isPART = False Then
                            isPARTFirstRow = True
                        End If

                        If Not IsNothing(ViewState("UncoverPart")) Then
                            Result = False
                        End If

                    Case 16 'Amount

                        'If countAMOUNT > 0 Then GoTo jump

                        countAMOUNT = countAMOUNT + 1

                        For Each item As DataGridItem In dgParts.Items
                            Dim txtPartPriceItem As TextBox = item.FindControl("txtPartPriceItem")
                            Dim txtKodePartsItem As TextBox = item.FindControl("txtKodePartsItem")

                            If Not IsNothing(oParamDetail.WSCParameterCondition) Then
                                Dim condition As WSCParameterCondition = (From c As WSCParameterCondition In arrCondition Where c.ID = oParamDetail.WSCParameterCondition.ID Select c)(0)
                                'If condition.Kind = 15 Then
                                For Each condValid As ArrayList In conditionValidation
                                    Dim cond As WSCParameterCondition = condValid(0)
                                    If cond.ID = condition.ID Then
                                        If condValid(2) = True Then
                                            Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, txtPartPriceItem.Text)
                                        Else
                                            Result = True
                                        End If
                                    End If
                                Next
                                'ElseIf condition.Kind = 5 Then
                                '    Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, txtPartPriceItem.Text)
                                'ElseIf condition.Kind = 9 Then
                                '    Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, txtPartPriceItem.Text)
                                'End If
                            Else
                                Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, txtPartPriceItem.Text)
                            End If

                            'Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, txtPartPriceItem.Text)
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

                    Case 17 'Jumlah
                        If Not IsNothing(oParamDetail.WSCParameterCondition) Then
                            Dim condition As WSCParameterCondition = (From c As WSCParameterCondition In arrCondition Where c.ID = oParamDetail.WSCParameterCondition.ID Select c)(0)
                            For Each condValid As ArrayList In conditionValidation
                                Dim cond As WSCParameterCondition = condValid(0)
                                If cond.ID = condition.ID Then
                                    If condValid(2) = True Then
                                        If condition.Kind = 5 OrElse condition.Kind = 9 Then    'jumlah pada kode posisi dan kode kerja
                                            For Each item As WSCDetail In PositionCode
                                                Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, item.Quantity.ToString())
                                            Next
                                        ElseIf condition.Kind = 15 Then 'jumlah pada part
                                            For Each item As DataGridItem In dgParts.Items
                                                Dim val As TextBox = item.FindControl("txtQtyItem")
                                                Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, val.Text)
                                            Next
                                        End If
                                    Else
                                        Result = True
                                    End If
                                End If
                            Next
                        End If

                    Case 18
                    Case 19

                        'Case 8 'Dmg Date to Repair date length
                    Case 20 'PKT Date to Failure Date
                        Dim ValDate As Integer = (CType(PKTDate, Date) - CType(DmgDate, Date)).Days
                        If ValDate < 0 Then
                            ValDate = ValDate * -1
                        End If
                        Result = paramDetailKind(oParamDetail.Kind, oParamDetail.Operators, oParamDetail.Value, ValDate)
                        Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamDetail.Kind)
                        vals = ValDate
jump:

                End Select

                'If index = 0 AndAlso Result = False Then
                '    Exit For
                'End If

                'jika result false, tambahkan reject code
                If Not RejectCode.Contains(oParamDetail.ReasonCode) And Result = False Then
                    RejectCode += oParamDetail.ReasonCode & ";"
                End If

                'jika result false
                If Not Result AndAlso rejectionsResult.Count = 0 Then
                    RCode = RejectCode.Replace(";", "\n")
                    errorIn += Kinds & " Operator " & New EnumWSCParamParameter().RetrieveWSCParamOperator(oParamDetail.Operators).ToLower & "  " & oParamDetail.Value & " dan Page " & vals & ";"

                    Dim tempRejection As ArrayList = New ArrayList
                    tempRejection.Add(oParamHead.WSCParameterHeader.Description)
                    tempRejection.Add(oParamDetail.ReasonCode)
                    rejectionsResult.Add(tempRejection)
                End If

                'jika true
                If Result Then
                    tempRejectionsResult.Add(True)
                End If

                paramCont.Add(Result)
                index += 1
                detailIndex += 1
            Next

            Dim tempRes As Boolean = True
            For Each r As Boolean In tempRejectionsResult
                tempRes = tempRes And r
            Next

            If tempRes Then
                validParameters.Add(oParamHead.WSCParameterHeader.Description)
            End If

        Next

        If detailIndex = 0 Then
            MessageBox.Show("Tipe kendaraan " & typeCode & " belum terdaftar pada parameter. \nSilahkan menghubungi MMKSI")
            Return False
        End If

        If rejectionsResult.Count > 0 Then
            Dim strMsg As String = "Pengajuan WSC Anda ditolak karena tidak sesuai dengan :"

            For Each r As ArrayList In rejectionsResult
                strMsg = strMsg & "\nParameter : " & r(0) & "\nKode penolakan : " & r(1)
            Next

            If Not IsNothing(ViewState("UncoverPart")) Then
                If ViewState("UncoverPart").ToString.Length > 0 Then
                    strMsg = strMsg & "\nParts tidak valid: "
                    If ViewState("UncoverPart").ToString.Contains(",") Then
                        For Each _varStr As String In ViewState("UncoverPart").ToString.Split(",")
                            strMsg = strMsg & "\n" & _varStr
                        Next
                    Else
                        strMsg = strMsg & "\n" & ViewState("UncoverPart")
                    End If
                End If
            End If

            MessageBox.Show(strMsg)
            Return False
        End If

        'If paramCont.Contains(False) Then

        '    'If isPARTBlank = False And
        '    '   isPARTFirstRow = False And
        '    '   isAMOUNTAllRow = False And
        '    '    (isPART = False Or isAMOUNT = False) Then
        '    SaveToFile(errorIn)
        '    RejectCode.Replace(";", "\n")
        '    MessageBox.Show("Pengajuan WSC anda ditolak, \nKode Penolakan : \n" & RejectCode)
        '    Return False
        '    'End If
        'End If

        'If arrParamHead.Count = 0 Then
        '    Return True
        'Else
        '    'MessageBox.Show(errorIn)
        '    SaveToFile(errorIn)
        '    RejectCode.Replace(";", "\n")
        '    MessageBox.Show("Pengajuan WSC anda ditolak, \nKode Penolakan : \n" & RejectCode)
        'End If

        If validParameters.Count > 0 Then
            Dim strMsg As String = "Valid pada parameter : "
            For Each v As String In validParameters
                strMsg = strMsg & "\n - " & v
            Next

            MessageBox.Show(strMsg)
        End If

        Return True
    End Function

    Private Sub setValidCondValue(ByVal val As String, ByVal status As WSCParameterCondition)
        Dim validCondValue As ArrayList = GetSession("validCondValue")
        If IsNothing(validCondValue) Then
            validCondValue = New ArrayList
        End If

        Dim temp As ArrayList = New ArrayList
        temp.Add(val)
        temp.Add(status)
        validCondValue.Add(temp)

        SetSession("validCondValue", validCondValue)
    End Sub

    Private Function getValidCondValue(ByVal val As String)
        Dim validCondValue As ArrayList = GetSession("validCondValue")

        For Each v As ArrayList In validCondValue
            If v(0) = val Then
                Return True
            End If
        Next

        Return False
    End Function

    Private Function validateCondition1(ByVal arrCondition As ArrayList) As Boolean
        Dim paramCont As ArrayList = New ArrayList
        Dim errorIn As String = String.Empty
        Dim RejectCode As String = String.Empty
        Dim RCode As String = String.Empty
        Dim typeCode As String = String.Empty

        Dim condStatus As ArrayList = New ArrayList

        '1. Ambil Vtype ID
        Dim criteVType As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteVType.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, txtNoChasis.Text))
        Dim oChaMasFas As ChassisMaster = New ChassisMasterFacade(User).Retrieve(criteVType)(0)
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
        Dim PKTDate As String = lblTglPKTVal.Text
        Dim DmgDate As String = icTglKerusakan.Value
        Dim RepDate As String = icTglPerbaikan.Value
        Dim Odo As Integer = CType(txtOdometer.Text, Integer)
        Dim detailIndex As Integer = 0
        Dim PositionCode As ArrayList
        If Mode = enumMode.Mode.NewItemMode Then
            'PositionCode = sessHelper.GetSession("NEW_ONGKOSKERJA")
            PositionCode = GetSession("NEW_ONGKOSKERJA")
        Else
            'PositionCode = sessHelper.GetSession("ONGKOSKERJA")
            PositionCode = GetSession("ONGKOSKERJA")
        End If
        Dim WorkCode As ArrayList
        If Mode = enumMode.Mode.NewItemMode Then
            'WorkCode = sessHelper.GetSession("NEW_ONGKOSPARTS")
            WorkCode = GetSession("NEW_ONGKOSPARTS")
        Else
            'WorkCode = sessHelper.GetSession("ONGKOSPARTS")
            WorkCode = GetSession("ONGKOSPARTS")
        End If
        Dim DmgCodeA As String = ddlKodeWSCA.SelectedValue
        Dim DmgCodeB As String = ddlKodeWSCB.SelectedValue
        Dim DmgCodeC As String = ddlKodeWSCC.SelectedValue

        Dim arrValidationConditionResult As ArrayList = New ArrayList
        Dim Result As Boolean = False
        Dim condValidationCheckFlag As Boolean = False

        'cek kode posisi(5) dan kode kerja(9) dulu
        For Each item As WSCDetail In PositionCode
            Dim tempResult As ArrayList = New ArrayList
            'cek kode posisi
            For Each condition As WSCParameterCondition In arrCondition
                If condition.Kind = 5 Then
                    Result = paramDetailKind(condition.Kind, condition.Operators, condition.Value, item.PositionCode)
                    tempResult.Add(Result)
                    condValidationCheckFlag = True
                End If
            Next

            For r As Integer = 0 To tempResult.Count - 1
                If tempResult(r) = True Then
                    Dim validationObj As validationObj = New validationObj
                    validationObj.Condition = (From c As WSCParameterCondition In arrCondition Where c.Kind = 5 Select c)(r)
                    validationObj.InputValue = item.PositionCode
                    validationObj.Status = True
                    arrValidationConditionResult.Add(validationObj)
                End If
                Result = Result Or tempResult(r)
            Next

            'cek kode kerja
            Dim arrPosValid = (From v As validationObj In arrValidationConditionResult Where v.InputValue = item.PositionCode Select v).ToList()
            If Not IsNothing(arrPosValid) Then
                For Each posValid As validationObj In arrPosValid
                    Dim _workCode As WSCParameterCondition = (From c As WSCParameterCondition In arrCondition Where _
                        c.WSCParameterConditionID = posValid.Condition.ID And c.Kind = 9 Select c).FirstOrDefault()
                    If Not IsNothing(_workCode) Then
                        Result = paramDetailKind(_workCode.Kind, _workCode.Operators, _workCode.Value, item.WorkCode)
                        condValidationCheckFlag = True
                        If Result = True Then
                            Dim validationObj As validationObj = New validationObj
                            validationObj.Condition = _workCode
                            validationObj.InputValue = item.WorkCode
                            validationObj.Status = True
                            arrValidationConditionResult.Add(validationObj)
                        End If
                    Else
                        Result = True
                    End If
                Next
            End If

            'jumlah di koed posisi & kode kerja
            arrPosValid = (From v As validationObj In arrValidationConditionResult Where v.InputValue = item.PositionCode Select v).ToList()
            Dim arrWorkValid = (From v As validationObj In arrValidationConditionResult Where v.InputValue = item.WorkCode Select v).ToList()
            'cek terhadap kode posisi
            If Not IsNothing(arrPosValid) Then
                For Each posValid As validationObj In arrPosValid
                    Dim _qty As WSCParameterCondition = (From c As WSCParameterCondition In arrCondition Where _
                        c.WSCParameterConditionID = posValid.Condition.ID And c.Kind = 17 Select c)(0)
                    If Not IsNothing(_qty) Then
                        Result = paramDetailKind(_qty.Kind, _qty.Operators, _qty.Value, item.Quantity.ToString())
                        condValidationCheckFlag = True
                        If Result = True Then
                            Dim validationObj As validationObj = New validationObj
                            validationObj.Condition = _qty
                            validationObj.InputValue = item.Quantity
                            validationObj.Status = True
                            arrValidationConditionResult.Add(validationObj)
                        End If
                    End If
                Next
            End If

            'cek terhadap kode kerja
            If Not IsNothing(arrWorkValid) Then
                For Each workValid As validationObj In arrWorkValid
                    Dim _qty As WSCParameterCondition = (From c As WSCParameterCondition In arrCondition Where _
                        c.WSCParameterConditionID = workValid.Condition.ID And c.Kind = 17 Select c)(0)
                    If Not IsNothing(_qty) Then
                        Result = paramDetailKind(_qty.Kind, _qty.Operators, _qty.Value, item.Quantity.ToString())
                        condValidationCheckFlag = True
                        If Result = True Then
                            Dim validationObj As validationObj = New validationObj
                            validationObj.Condition = _qty
                            validationObj.InputValue = item.Quantity
                            validationObj.Status = True
                            arrValidationConditionResult.Add(validationObj)
                        End If
                    End If
                Next
            End If
        Next


        'cek part dan jumlahnya
        For Each item As DataGridItem In dgParts.Items
            Dim txtKodePartsItem As TextBox = item.FindControl("txtKodePartsItem")
            Dim lblPartQty As TextBox = item.FindControl("txtQtyItem")

            For Each cond As WSCParameterCondition In arrCondition
                If cond.Kind = 15 Then  'cek part
                    Result = paramDetailKind(cond.Kind, cond.Operators, cond.Value, txtKodePartsItem.Text)
                    condValidationCheckFlag = True
                    If Result = True Then
                        Dim validationObj As validationObj = New validationObj
                        validationObj.Condition = cond
                        validationObj.InputValue = txtKodePartsItem.Text
                        validationObj.Status = True
                        arrValidationConditionResult.Add(validationObj)
                    End If
                ElseIf cond.Kind = 17 Then  'cek jumlah part
                    Dim validPart As validationObj = (From vp As validationObj In arrValidationConditionResult Where vp.InputValue = txtKodePartsItem.Text _
                        And vp.Condition.ID = cond.WSCParameterConditionID Select vp)(0)
                    If Not IsNothing(validPart) Then
                        Result = paramDetailKind(cond.Kind, cond.Operators, cond.Value, lblPartQty.Text)
                        condValidationCheckFlag = True
                        If Result = True Then
                            Dim validationObj As validationObj = New validationObj
                            validationObj.Condition = cond
                            validationObj.InputValue = lblPartQty.Text
                            validationObj.Status = True
                            arrValidationConditionResult.Add(validationObj)
                        End If
                    End If
                End If
            Next
        Next

        'cek nomor buletin
        For Each cond As WSCParameterCondition In arrCondition
            If cond.Kind = 0 Then
                Result = paramDetailKind(cond.Kind, cond.Operators, cond.Value, RefDoc)
                condValidationCheckFlag = True
                Dim validationObj As validationObj = New validationObj
                validationObj.Condition = cond
                validationObj.InputValue = RefDoc
                validationObj.Status = Result
                arrValidationConditionResult.Add(validationObj)
            End If
        Next

        'cek tanggal PKT
        For Each cond As WSCParameterCondition In arrCondition
            If cond.Kind = 1 Then
                If blnIsSoldDealer OrElse PKTDate <> String.Empty Then
                    If PKTDate.Trim.Length = 0 Then
                        Dim oEndCust As EndCustomer = New EndCustomerFacade(User).Retrieve(EndCustomerID)
                        If oEndCust.FakturDate = Date.MinValue Then
                            Result = paramDetailKind(cond.Kind, cond.Operators, cond.Value, oEndCust.OpenFakturDate)

                        Else
                            Result = paramDetailKind(cond.Kind, cond.Operators, cond.Value, oEndCust.FakturDate)
                            condValidationCheckFlag = True
                        End If
                    Else
                        Result = paramDetailKind(cond.Kind, cond.Operators, cond.Value, PKTDate)
                        condValidationCheckFlag = True
                    End If
                End If

                Dim validationObj As validationObj = New validationObj
                validationObj.Condition = cond
                validationObj.InputValue = RefDoc
                validationObj.Status = Result
                arrValidationConditionResult.Add(validationObj)
            End If
        Next

        'cek dmg date
        For Each cond As WSCParameterCondition In arrCondition
            If cond.Kind = 2 Then
                Result = paramDetailKind(cond.Kind, cond.Operators, cond.Value, DmgDate)
                condValidationCheckFlag = True
                Dim validationObj As validationObj = New validationObj
                validationObj.Condition = cond
                validationObj.InputValue = RefDoc
                validationObj.Status = Result
                arrValidationConditionResult.Add(validationObj)
            End If
        Next

        'cek repair date
        For Each cond As WSCParameterCondition In arrCondition
            If cond.Kind = 3 Then
                Result = paramDetailKind(cond.Kind, cond.Operators, cond.Value, RepDate)
                condValidationCheckFlag = True
                Dim validationObj As validationObj = New validationObj
                validationObj.Condition = cond
                validationObj.InputValue = RefDoc
                validationObj.Status = Result
                arrValidationConditionResult.Add(validationObj)
            End If
        Next

        'cek odo
        For Each cond As WSCParameterCondition In arrCondition
            If cond.Kind = 4 Then
                Result = paramDetailKind(cond.Kind, cond.Operators, cond.Value, Odo)
                condValidationCheckFlag = True
                Dim validationObj As validationObj = New validationObj
                validationObj.Condition = cond
                validationObj.InputValue = RefDoc
                validationObj.Status = Result
                arrValidationConditionResult.Add(validationObj)
            End If
        Next

        'pkt date to repair date length
        For Each cond As WSCParameterCondition In arrCondition
            If cond.Kind = 6 Then
                Dim ValDate As Integer
                If PKTDate.Trim.Length = 0 Then
                    Dim oEndCust As EndCustomer = New EndCustomerFacade(User).Retrieve(EndCustomerID)
                    If oEndCust.FakturDate = Date.MinValue Then
                        ValDate = (oEndCust.OpenFakturDate - CType(RepDate, Date)).Days
                        If ValDate < 0 Then
                            ValDate = ValDate * -1
                        End If
                        Result = paramDetailKind(cond.Kind, cond.Operators, cond.Value, ValDate)
                        condValidationCheckFlag = True
                    Else
                        ValDate = (oEndCust.FakturDate - CType(RepDate, Date)).Days
                        If ValDate < 0 Then
                            ValDate = ValDate * -1
                        End If
                        Result = paramDetailKind(cond.Kind, cond.Operators, cond.Value, ValDate)
                        condValidationCheckFlag = True
                    End If
                Else
                    ValDate = (CType(PKTDate, Date) - CType(RepDate, Date)).Days
                    If ValDate < 0 Then
                        ValDate = ValDate * -1
                    End If
                    Result = paramDetailKind(cond.Kind, cond.Operators, cond.Value, ValDate)
                    condValidationCheckFlag = True
                End If

                Dim validationObj As validationObj = New validationObj
                validationObj.Condition = cond
                validationObj.InputValue = ValDate
                validationObj.Status = Result
                arrValidationConditionResult.Add(validationObj)
            End If
        Next

        'repair date to input claim length
        For Each cond As WSCParameterCondition In arrCondition
            If cond.Kind = 7 Then
                Dim ValDate As Integer = (CType(RepDate, Date) - DateTime.Now).Days
                If Mode = enumMode.Mode.EditMode Then
                    ValDate = (CType(RepDate, Date) - CType(lblTglKirimVal.Text, Date)).Days
                End If
                If ValDate < 0 Then
                    ValDate = ValDate * -1
                End If
                Result = paramDetailKind(cond.Kind, cond.Operators, cond.Value, ValDate)
                condValidationCheckFlag = True
                Dim validationObj As validationObj = New validationObj
                validationObj.Condition = cond
                validationObj.InputValue = ValDate
                validationObj.Status = Result
                arrValidationConditionResult.Add(validationObj)
            End If
        Next

        'dmg date to repair date length
        For Each cond As WSCParameterCondition In arrCondition
            If cond.Kind = 8 Then
                Dim ValDate As Integer = (CType(DmgDate, Date) - CType(RepDate, Date)).Days
                If ValDate < 0 Then
                    ValDate = ValDate * -1
                End If
                Result = paramDetailKind(cond.Kind, cond.Operators, cond.Value, ValDate)
                condValidationCheckFlag = True
                Dim validationObj As validationObj = New validationObj
                validationObj.Condition = cond
                validationObj.InputValue = ValDate
                validationObj.Status = Result
                arrValidationConditionResult.Add(validationObj)
            End If
        Next

        'dmg code A
        For Each cond As WSCParameterCondition In arrCondition
            If cond.Kind = 10 Then
                Result = paramDetailKind(cond.Kind, cond.Operators, cond.Value, DmgCodeA)
                condValidationCheckFlag = True
                Dim validationObj As validationObj = New validationObj
                validationObj.Condition = cond
                validationObj.InputValue = DmgCodeA
                validationObj.Status = Result
                arrValidationConditionResult.Add(validationObj)
            End If
        Next

        'dmg code B
        For Each cond As WSCParameterCondition In arrCondition
            If cond.Kind = 11 Then
                Result = paramDetailKind(cond.Kind, cond.Operators, cond.Value, DmgCodeB)
                condValidationCheckFlag = True
                Dim validationObj As validationObj = New validationObj
                validationObj.Condition = cond
                validationObj.InputValue = DmgCodeB
                validationObj.Status = Result
                arrValidationConditionResult.Add(validationObj)
            End If
        Next

        'dmg code C
        For Each cond As WSCParameterCondition In arrCondition
            If cond.Kind = 12 Then
                Result = paramDetailKind(cond.Kind, cond.Operators, cond.Value, DmgCodeC)
                condValidationCheckFlag = True
                Dim validationObj As validationObj = New validationObj
                validationObj.Condition = cond
                validationObj.InputValue = DmgCodeC
                validationObj.Status = Result
                arrValidationConditionResult.Add(validationObj)
            End If
        Next

        For Each cond As WSCParameterCondition In arrCondition
            Dim oMSPExReg As MSPExRegistration = New MSPExRegistrationFacade(User).RetrieveByChassisNumber(txtNoChasis.Text.Trim)
            If cond.Kind = 18 Then
                Result = paramDetailKind(cond.Kind, cond.Operators, cond.Value, oMSPExReg.CreatedTime.Date.ToString)
                condValidationCheckFlag = True
                Dim validationObj As validationObj = New validationObj
                validationObj.Condition = cond
                validationObj.InputValue = oMSPExReg.CreatedTime.Date.ToString
                validationObj.Status = Result
                arrValidationConditionResult.Add(validationObj)
            End If

            If cond.Kind = 19 Then
                Result = paramDetailKind(cond.Kind, cond.Operators, cond.Value, oMSPExReg.MSPExMaster.MSPExType.Code)
                condValidationCheckFlag = True
                Dim validationObj As validationObj = New validationObj
                validationObj.Condition = cond
                validationObj.InputValue = oMSPExReg.MSPExMaster.MSPExType.Code
                validationObj.Status = Result
                arrValidationConditionResult.Add(validationObj)
            End If
        Next

        If condValidationCheckFlag = False Then
            Return True
        End If

        Dim ret As Boolean = validateHeader(arrValidationConditionResult, arrCondition)
        'saveConditionResultForJumlah(arrCondition)

        Return ret
    End Function

    Private Function validateHeader(ByVal condResult As ArrayList, ByVal arrCondition As ArrayList) As Boolean
        Dim tempResult As ArrayList = New ArrayList
        Dim resultToSave As ArrayList = New ArrayList

        For Each cond As WSCParameterCondition In arrCondition
            Dim result As WSCParameterCondition = (From r As validationObj In condResult Where r.Condition.ID = cond.ID Select r.Condition)(0)

            Dim temp As ArrayList = New ArrayList
            temp.Add(cond)
            temp.Add(cond.Functions)
            If Not IsNothing(result) Then
                temp.Add(True)
            Else
                temp.Add(False)
            End If

            tempResult.Add(temp)
            resultToSave.Add(temp)
        Next

        Dim ret As Boolean = validateCondStatus(tempResult)
        If ret Then
            SetSession("conditionValidation", resultToSave)
        End If

        Return ret
    End Function

    Private Sub saveConditionResultForJumlah(ByVal arrCondition As ArrayList)
        Dim realCondResult As ArrayList = GetSession("conditionValidation")
        Dim arrResult As ArrayList = New ArrayList
        Dim arrCondResult As ArrayList = New ArrayList
        Dim tempArrResult As Boolean
        Dim orFlag As Boolean = True
        Dim paramCnt As Integer = 0

        If Not IsNothing(realCondResult) Then
            For i As Integer = 0 To arrCondition.Count - 1
                Dim cond As WSCParameterCondition = arrCondition(i)
                Dim realResult As ArrayList = realCondResult(i)

                If orFlag Then
                    tempArrResult = realResult(2)
                    orFlag = False
                Else
                    tempArrResult = tempArrResult And realResult(2)
                End If


                If cond.Functions = "1" Or i = arrCondition.Count - 1 Then
                    For j As Integer = paramCnt To i
                        arrResult.Add(tempArrResult)
                    Next

                    paramCnt = i + 1
                    orFlag = True
                End If
            Next

            For i As Integer = 0 To arrCondition.Count - 1
                Dim temp As ArrayList = New ArrayList
                temp.Add(arrCondition(i))
                temp.Add(True)
                temp.Add(arrResult(i))
                arrCondResult.Add(temp)
            Next

            SetSession("conditionValidationJumlah", arrCondResult)
        End If
    End Sub

    Private Function validateCondition() As Boolean
        Dim paramCont As ArrayList = New ArrayList
        Dim errorIn As String = String.Empty
        Dim RejectCode As String = String.Empty
        Dim RCode As String = String.Empty
        Dim typeCode As String = String.Empty

        Dim condStatus As ArrayList = New ArrayList

        '1. Ambil Vtype ID
        Dim criteVType As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteVType.opAnd(New Criteria(GetType(ChassisMaster), "ChassisNumber", MatchType.Exact, txtNoChasis.Text))
        Dim oChaMasFas As ChassisMaster = New ChassisMasterFacade(User).Retrieve(criteVType)(0)
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
        Dim PKTDate As String = lblTglPKTVal.Text
        Dim DmgDate As String = icTglKerusakan.Value
        Dim RepDate As String = icTglPerbaikan.Value
        Dim Odo As Integer = CType(txtOdometer.Text, Integer)
        Dim detailIndex As Integer = 0
        Dim PositionCode As ArrayList
        If Mode = enumMode.Mode.NewItemMode Then
            'PositionCode = sessHelper.GetSession("NEW_ONGKOSKERJA")
            PositionCode = GetSession("NEW_ONGKOSKERJA")
        Else
            'PositionCode = sessHelper.GetSession("ONGKOSKERJA")
            PositionCode = GetSession("ONGKOSKERJA")
        End If
        Dim WorkCode As ArrayList
        If Mode = enumMode.Mode.NewItemMode Then
            'WorkCode = sessHelper.GetSession("NEW_ONGKOSPARTS")
            WorkCode = GetSession("NEW_ONGKOSPARTS")
        Else
            'WorkCode = sessHelper.GetSession("ONGKOSPARTS")
            WorkCode = GetSession("ONGKOSPARTS")
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
        criteVehicle.opAnd(New Criteria(GetType(WSCParameterVehicle), "WSCParameterHeader.ClaimType", MatchType.Exact, ClaimType)) 'Changes 26/06/2018

        Dim oWSCParamVe As WSCParameterVehicleFacade = New WSCParameterVehicleFacade(User)
        Dim arrParamHead As ArrayList = oWSCParamVe.Retrieve(criteVehicle)
        If arrParamHead.Count <= 0 Then
            MessageBox.Show("Tipe kendaraan " & typeCode & " belum terdaftar pada parameter. \nSilahkan menghubungi MMKSI")
            Return False
        End If


        For Each oParamHead As WSCParameterVehicle In arrParamHead

            'Ambil condition
            Dim arrCondition As ArrayList = New ArrayList
            Dim criteria1 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCParameterCondition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria1.opAnd(New Criteria(GetType(WSCParameterCondition), "WSCParameterHeader.ID", MatchType.Exact, oParamHead.WSCParameterHeader.ID))
            arrCondition = New WSCParameterConditionFacade(User).Retrieve(criteria1)

            SetSession("Condition", arrCondition)

            Dim index As Integer = 0
            For Each oParamCondition As WSCParameterCondition In arrCondition
                Dim Result As Boolean = False
                Dim Kinds As String
                Dim vals As Object
                Select Case oParamCondition.Kind
                    Case 0 'Buletin Number
                        Result = paramDetailKind(oParamCondition.Kind, oParamCondition.Operators, oParamCondition.Value, RefDoc)
                        Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamCondition.Kind)
                        vals = RefDoc

                    Case 1 'PKT Date
                        If blnIsSoldDealer OrElse PKTDate <> String.Empty Then
                            If PKTDate.Trim.Length = 0 Then
                                Dim oEndCust As EndCustomer = New EndCustomerFacade(User).Retrieve(EndCustomerID)
                                If oEndCust.FakturDate = Date.MinValue Then
                                    Result = paramDetailKind(oParamCondition.Kind, oParamCondition.Operators, oParamCondition.Value, oEndCust.OpenFakturDate)
                                    Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamCondition.Kind)
                                    vals = oEndCust.OpenFakturDate
                                Else
                                    Result = paramDetailKind(oParamCondition.Kind, oParamCondition.Operators, oParamCondition.Value, oEndCust.FakturDate)
                                    Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamCondition.Kind)
                                    vals = oEndCust.FakturDate
                                End If
                            Else
                                Result = paramDetailKind(oParamCondition.Kind, oParamCondition.Operators, oParamCondition.Value, PKTDate)
                                Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamCondition.Kind)
                                vals = PKTDate
                            End If
                        End If

                    Case 2 'Dmg Date
                        Result = paramDetailKind(oParamCondition.Kind, oParamCondition.Operators, oParamCondition.Value, DmgDate)
                        Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamCondition.Kind)
                        vals = DmgDate

                    Case 3 'Repair Date
                        Result = paramDetailKind(oParamCondition.Kind, oParamCondition.Operators, oParamCondition.Value, RepDate)
                        Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamCondition.Kind)
                        vals = RepDate

                    Case 4 'Odo
                        Result = paramDetailKind(oParamCondition.Kind, oParamCondition.Operators, oParamCondition.Value, Odo)
                        Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamCondition.Kind)
                        vals = Odo

                    Case 5 'Position Codes
                        Dim tempResult As ArrayList = New ArrayList

                        For Each item As WSCDetail In PositionCode
                            Result = paramDetailKind(oParamCondition.Kind, oParamCondition.Operators, oParamCondition.Value, item.PositionCode)
                            If Result = False Then
                                For Each cond As WSCParameterCondition In arrCondition
                                    If cond.Kind = 5 Then
                                        Result = paramDetailKind(cond.Kind, cond.Operators, cond.Value, item.PositionCode)
                                        If Result = True Then
                                            setValidCondValue(item.PositionCode, cond)
                                        End If
                                        Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamCondition.Kind)
                                        vals = item.PositionCode
                                        tempResult.Add(Result)
                                    End If
                                Next
                            Else
                                Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamCondition.Kind)
                                vals = item.PositionCode
                            End If

                            tempResult.Add(Result)
                        Next

                        For Each r As Boolean In tempResult
                            Result = Result Or r
                        Next

                        For Each item As DataGridItem In dgOngkosKerja.Items

                            Dim txtPostionCodeItem As TextBox = item.FindControl("txtPostionCodeItem")

                            Dim params As String() = oParamCondition.Value.Split(";")

                            If oParamCondition.Operators = 9 Then ' Terdiri dari
                                For Each row As String In params
                                    If txtPostionCodeItem.Text.ToUpper() = row.ToUpper() Then
                                        isKODEPOSISI = True
                                    Else
                                        isKODEPOSISI = False
                                    End If
                                Next

                            ElseIf oParamCondition.Operators = 10 Then ' Tidak terdiri dari
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
                        If PKTDate.Trim.Length = 0 Then
                            Dim oEndCust As EndCustomer = New EndCustomerFacade(User).Retrieve(EndCustomerID)
                            If oEndCust.FakturDate = Date.MinValue Then
                                Dim ValDate As Integer = (oEndCust.OpenFakturDate - CType(RepDate, Date)).Days
                                If ValDate < 0 Then
                                    ValDate = ValDate * -1
                                End If
                                Result = paramDetailKind(oParamCondition.Kind, oParamCondition.Operators, oParamCondition.Value, ValDate)
                                Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamCondition.Kind)
                                vals = ValDate
                            Else
                                Dim ValDate As Integer = (oEndCust.FakturDate - CType(RepDate, Date)).Days
                                If ValDate < 0 Then
                                    ValDate = ValDate * -1
                                End If
                                Result = paramDetailKind(oParamCondition.Kind, oParamCondition.Operators, oParamCondition.Value, ValDate)
                                Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamCondition.Kind)
                                vals = ValDate
                            End If
                        Else
                            Dim ValDate As Integer = (CType(PKTDate, Date) - CType(RepDate, Date)).Days
                            If ValDate < 0 Then
                                ValDate = ValDate * -1
                            End If
                            Result = paramDetailKind(oParamCondition.Kind, oParamCondition.Operators, oParamCondition.Value, ValDate)
                            Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamCondition.Kind)
                            vals = ValDate
                        End If

                    Case 7 'Repair date to Input Claim date length
                        Dim ValDate As Integer = (CType(RepDate, Date) - DateTime.Now).Days
                        If Mode = enumMode.Mode.EditMode Then
                            ValDate = (CType(RepDate, Date) - CType(lblTglKirimVal.Text, Date)).Days
                        End If
                        If ValDate < 0 Then
                            ValDate = ValDate * -1
                        End If
                        Result = paramDetailKind(oParamCondition.Kind, oParamCondition.Operators, oParamCondition.Value, ValDate)
                        Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamCondition.Kind)
                        vals = ValDate

                    Case 8 'Dmg Date to Repair date length
                        Dim ValDate As Integer = (CType(DmgDate, Date) - CType(RepDate, Date)).Days
                        If ValDate < 0 Then
                            ValDate = ValDate * -1
                        End If
                        Result = paramDetailKind(oParamCondition.Kind, oParamCondition.Operators, oParamCondition.Value, ValDate)
                        Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamCondition.Kind)
                        vals = ValDate

                    Case 9 'Work Codes
                        Dim tempResult As ArrayList = New ArrayList

                        For Each item As WSCDetail In PositionCode
                            Result = paramDetailKind(oParamCondition.Kind, oParamCondition.Operators, oParamCondition.Value, item.WorkCode)
                            If Result = False Then
                                Result = getValidCondValue(item.PositionCode)
                            End If
                            Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamCondition.Kind)
                            vals = item.WorkCode
                        Next

                        For Each r As Boolean In tempResult
                            Result = Result Or r
                        Next

                        If Result = False Then

                        End If
                    Case 10 'Dmg Code A
                        Result = paramDetailKind(oParamCondition.Kind, oParamCondition.Operators, oParamCondition.Value, DmgCodeA)
                        Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamCondition.Kind)
                        vals = DmgCodeA

                    Case 11 'Dmg Code B
                        Result = paramDetailKind(oParamCondition.Kind, oParamCondition.Operators, oParamCondition.Value, DmgCodeB)
                        Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamCondition.Kind)
                        vals = DmgCodeB

                    Case 12 'Dmg Code C
                        Result = paramDetailKind(oParamCondition.Kind, oParamCondition.Operators, oParamCondition.Value, DmgCodeC)
                        Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamCondition.Kind)
                        vals = DmgCodeC

                    Case 15 'Part

                        countPART = countPART + 1

                        If dgParts.Items.Count = 0 Then
                            Result = True
                        Else
                            For Each item As DataGridItem In dgParts.Items

                                Dim txtKodePartsItem As TextBox = item.FindControl("txtKodePartsItem")

                                Result = paramDetailKind(oParamCondition.Kind, oParamCondition.Operators, oParamCondition.Value, txtKodePartsItem.Text)
                                Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamCondition.Kind)
                                vals = txtKodePartsItem.Text

                                Dim params As String() = oParamCondition.Value.Split(";")

                                If oParamCondition.Operators = 9 Then ' Terdiri dari
                                    For Each row As String In params

                                        If txtKodePartsItem.Text.ToUpper() = row.ToUpper() Then
                                            isPART = True

                                        Else
                                            isPART = False
                                        End If

                                    Next
                                ElseIf oParamCondition.Operators = 10 Then ' Tidak terdiri dari
                                    For Each row As String In params

                                        If txtKodePartsItem.Text.ToUpper() <> row.ToUpper() Then
                                            isPART = True
                                        Else
                                            isPART = False
                                        End If

                                    Next
                                End If

                            Next
                        End If

                        If isKODEPOSISI And dgParts.Items.Count = 0 Then
                            isPARTBlank = True
                        End If

                        If isKODEPOSISI And dgParts.Items.Count = 1 And isPART = False Then
                            isPARTFirstRow = True
                        End If

                    Case 16 'Amount

                        countAMOUNT = countAMOUNT + 1

                        For Each item As DataGridItem In dgParts.Items

                            Dim txtPartPriceItem As TextBox = item.FindControl("txtPartPriceItem")

                            Result = paramDetailKind(oParamCondition.Kind, oParamCondition.Operators, oParamCondition.Value, txtPartPriceItem.Text)
                            Kinds = New EnumWSCParamParameter().RetrieveWSCParameter(oParamCondition.Kind)
                            vals = txtPartPriceItem.Text

                            If oParamCondition.Operators = 0 Then ' =
                                If Convert.ToDecimal(txtPartPriceItem.Text) = Convert.ToDecimal(oParamCondition.Value) Then
                                    isAMOUNT = True

                                    countAMOUNTrue = countAMOUNTrue + 1
                                Else
                                    isAMOUNT = False
                                End If
                            ElseIf oParamCondition.Operators = 1 Then ' <>
                                If Convert.ToDecimal(txtPartPriceItem.Text) <> Convert.ToDecimal(oParamCondition.Value) Then
                                    isAMOUNT = True

                                    countAMOUNTrue = countAMOUNTrue + 1
                                Else
                                    isAMOUNT = False
                                End If
                            ElseIf oParamCondition.Operators = 5 Then ' >
                                If Convert.ToDecimal(txtPartPriceItem.Text) > Convert.ToDecimal(oParamCondition.Value) Then
                                    isAMOUNT = True

                                    countAMOUNTrue = countAMOUNTrue + 1
                                Else
                                    isAMOUNT = False
                                End If
                            ElseIf oParamCondition.Operators = 6 Then ' <
                                If Convert.ToDecimal(txtPartPriceItem.Text) < Convert.ToDecimal(oParamCondition.Value) Then
                                    isAMOUNT = True

                                    countAMOUNTrue = countAMOUNTrue + 1
                                Else
                                    isAMOUNT = False
                                End If
                            ElseIf oParamCondition.Operators = 7 Then ' >=
                                If Convert.ToDecimal(txtPartPriceItem.Text) >= Convert.ToDecimal(oParamCondition.Value) Then
                                    isAMOUNT = True

                                    countAMOUNTrue = countAMOUNTrue + 1
                                Else
                                    isAMOUNT = False
                                End If
                            ElseIf oParamCondition.Operators = 8 Then ' <=
                                If Convert.ToDecimal(txtPartPriceItem.Text) <= Convert.ToDecimal(oParamCondition.Value) Then
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

                End Select

                If index = 0 AndAlso Result = False Then
                    Exit For
                End If

                Dim temp As ArrayList = New ArrayList
                temp.Add(oParamCondition)
                temp.Add(oParamCondition.Functions)
                temp.Add(Result)
                condStatus.Add(temp)

                'jika result false
                If Not Result Then
                    RCode = RejectCode.Replace(";", "\n")
                    errorIn += Kinds & " Operator " & New EnumWSCParamParameter().RetrieveWSCParamOperator(oParamCondition.Operators).ToLower & "  " & oParamCondition.Value & " dan Page " & vals & ";"
                End If

                paramCont.Add(Result)
                index += 1
                detailIndex += 1
            Next
        Next

        If detailIndex = 0 Then
            MessageBox.Show("Tipe kendaraan " & typeCode & " belum terdaftar pada parameter. \nSilahkan menghubungi MMKSI")
            Return False
        End If

        Dim ret As Boolean = validateCondStatus(condStatus)
        Return ret
    End Function

    Private Function validateCondStatus(ByVal status As ArrayList) As Boolean
        Dim ret As Boolean = False

        If status.Count = 1 Then
            Dim _status As ArrayList = status(0)
            Return _status(2)
        End If

        For i As Integer = 0 To status.Count - 2
            Dim _status As ArrayList = status(i)
            Dim nextStatus As ArrayList = status(i + 1)

            If i = 0 Then
                If _status(1) = 0 Then
                    ret = _status(2) And nextStatus(2)
                ElseIf _status(1) = 1 Then
                    ret = _status(2) Or nextStatus(2)
                End If
            Else
                Dim nextCon As WSCParameterCondition = nextStatus(0)
                If nextCon.Kind = 5 Then
                    status.RemoveRange(0, i + 1)
                    Dim tempRet As Boolean = validateCondStatus(status)
                    If _status(1) = 0 Then
                        ret = ret And tempRet
                    ElseIf _status(1) = 1 Then
                        ret = ret Or tempRet
                    End If
                    Return ret
                ElseIf _status(1) = 0 Then
                    ret = ret And nextStatus(2)
                ElseIf _status(1) = 1 Then
                    ret = ret Or nextStatus(2)
                End If
            End If
        Next

        Return ret
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
                Return FuncEqual(DBVal.ToUpper, Value1.ToUpper)
            Case 1 'NotEqual
                Return FuncNotEqual(DBVal.ToUpper, Value1.ToUpper)
            Case 2 'PartialEqual
                Return FuncPartialEqual(DBVal, Value1)
            Case 3 'StartWith
                Return FuncStartWith(DBVal.ToUpper, Value1.ToUpper)
            Case 4 'EndWith
                Return FuncEndWith(DBVal.ToUpper, Value1.ToUpper)
            Case 5 'GreaterThan

                If FuncIsDate(DBVal, Value1) Then
                    Return FuncDtGreaterThan(DBVal, Value1)
                Else
                    Return FuncGreaterThan(DBVal, Value1)
                End If

            Case 6 'SmallerThan
                If FuncIsDate(DBVal, Value1) Then
                    Return FuncDtSmallerThan(DBVal, Value1)
                Else
                    Return FuncSmallerThan(DBVal, Value1)
                End If

            Case 7 'GreaterOrEqual
                Return FuncGreaterOrEqual(DBVal, Value1)
            Case 8 'SmallerOrEqual
                Return FuncSmallerOrEqual(DBVal, Value1)


            Case 9 'Inset
                Return FuncInset(DBVal.ToUpper, Value1.ToUpper)
            Case 10 'Offset
                Return FuncOffset(DBVal.ToUpper, Value1.ToUpper)
        End Select
    End Function

    Private Function FuncEqual(ByVal db As String, ByVal value As String)
        Dim _ret As Boolean = False
        If value.Trim = db.Trim Then
            _ret = True
        End If
        Return _ret
    End Function

    Private Function FuncNotEqual(ByVal db As String, ByVal value As String)
        Dim _ret As Boolean = False
        If value.Trim <> db.Trim Then
            _ret = True
        End If
        Return _ret
    End Function

    Private Function FuncPartialEqual(ByVal db As String, ByVal value As String)
        Dim _ret As Boolean = False
        If value.Trim.Contains(db.Trim) Then
            _ret = True
        End If
        Return _ret
    End Function

    Private Function FuncStartWith(ByVal db As String, ByVal value As String)
        Dim _ret As Boolean = False
        If value.Trim.StartsWith(db.Trim) Then
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
            If Val.Trim = value.Trim Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Function FuncOffset(ByVal db As String, ByVal value As String)
        Dim arrDBVal As String() = db.Split(";")
        For Each Val As String In arrDBVal
            If Val.Trim = value.Trim Then
                Return False
            End If
        Next
        Return True
    End Function


    Private Function FuncIsDate(ByVal val1 As String, ByVal val2 As String) As Boolean
        Dim ret1 As Boolean = False
        Dim ret2 As Boolean = False
        If (val1.Contains("/") OrElse val1.Contains("-")) AndAlso (val1.Split("/").Length = 3 OrElse val1.Split("-").Length = 3) Then
            ret1 = True
        End If

        If (val2.Contains("/") OrElse val2.Contains("-")) AndAlso (val2.Split("/").Length = 3 OrElse val2.Split("-").Length = 3) Then
            ret2 = True
        End If
        Return (ret1 OrElse ret2)

    End Function

    Private Function FuncGetDate(ByVal dt1 As String) As DateTime

        Dim dt2 As New DateTime(1900, 1, 1)
        Try
            If (dt1.Contains("/") OrElse dt1.Contains("-")) AndAlso (dt1.Split("/").Length = 3 OrElse dt1.Split("-").Length = 3) Then
                If (dt1.Contains("/")) Then
                    dt2 = New DateTime(CInt(dt1.Split("/")(2)), CInt(dt1.Split("/")(1)), CInt(dt1.Split("/")(0)))
                End If

                If (dt1.Contains("-")) Then
                    dt2 = New DateTime(CInt(dt1.Split("-")(2)), CInt(dt1.Split("-")(1)), CInt(dt1.Split("-")(0)))
                End If
            End If
        Catch ex As Exception

        End Try

        Return dt2
    End Function


    Private Function FuncDtSmallerThan(ByVal db As String, ByVal value As String)
        Dim _ret As Boolean = False
        If FuncGetDate(value) < FuncGetDate(db) Then
            _ret = True
        End If
        Return _ret
    End Function

    Private Function FuncDtGreaterThan(ByVal db As String, ByVal value As String)
        Dim _ret As Boolean = False
        If FuncGetDate(value) < FuncGetDate(db) Then
            _ret = True
        End If
        Return _ret
    End Function


#End Region

    Private Function ChassisKodePosWorkCodKiloIsExist(ByVal Chassis As String, ByVal PositionCode As String, ByVal WorkCode As String, ByVal Kilo As Integer, ByRef oDetail As WSCDetail) As Boolean
        Dim arl As ArrayList
        For i As Integer = 0 To 1
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(WSCDetail), "WSCHeader.ClaimType", MatchType.Exact, ddlClaimType.SelectedValue)) 'Doni Add same Chassis diff ClaimType 05042022
            crit.opAnd(New Criteria(GetType(WSCDetail), "WSCType", MatchType.Exact, "L"))
            crit.opAnd(New Criteria(GetType(WSCDetail), "WSCHeader.ChassisMaster.ChassisNumber", MatchType.Exact, Chassis))
            If i = 0 Then
                crit.opAnd(New Criteria(GetType(WSCDetail), "PositionCode", MatchType.Exact, PositionCode))
            Else
                crit.opAnd(New Criteria(GetType(WSCDetail), "WSCHeader.Miliage", MatchType.Exact, Kilo))
            End If
            crit.opAnd(New Criteria(GetType(WSCDetail), "WorkCode", MatchType.Exact, WorkCode))
            crit.opAnd(New Criteria(GetType(WSCDetail), "WSCHeader.ClaimStatus", MatchType.No, "DAPP"))
            If Mode = enumMode.Mode.EditMode Then
                crit.opAnd(New Criteria(GetType(WSCDetail), "WSCHeader.ClaimNumber", MatchType.No, lblClaimNumber.Text.Trim))
            End If

            crit.opAnd(New Criteria(GetType(WSCDetail), "WSCHeader.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            arl = New WSCDetailFacade(User).Retrieve(crit)
            If arl.Count > 0 Then
                oDetail = CType(arl(0), WSCDetail)
                Return True
            End If
            i += 1
        Next
        Return False
    End Function

    Private Function PQRStatusBeforRelease() As Boolean
        If ddlRefDoc.SelectedIndex = 2 Then
            Dim arl As ArrayList
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRHeader), "RowStatus", MatchType.Greater, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(PQRHeader), "PQRNo", MatchType.Exact, txtPQRNo.Text))
            arl = New PQRHeaderFacade(User).Retrieve(crit)
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

    Private Sub UpdateWscDetailLaborID(ByVal ___objWSCDetail As WSCDetail)
        Try
            If Not IsNothing(___objWSCDetail.WSCHeader) Then
                Dim vTypeID As Integer = ___objWSCDetail.WSCHeader.ChassisMaster.VechileColor.VechileType.ID
                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LaborMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(LaborMaster), "VechileType.ID", MatchType.Exact, vTypeID))
                crit.opAnd(New Criteria(GetType(LaborMaster), "LaborCode", MatchType.Exact, ___objWSCDetail.PositionCode))
                crit.opAnd(New Criteria(GetType(LaborMaster), "WorkCode", MatchType.Exact, ___objWSCDetail.WorkCode))
                Dim arlLabMaster As ArrayList = New LaborMasterFacade(User).Retrieve(crit)
                If arlLabMaster.Count > 0 Then
                    Dim oLaborMaster As LaborMaster = arlLabMaster(0)
                    ___objWSCDetail.LaborMaster = oLaborMaster
                    Dim _res As Integer = New WSCDetailFacade(User).Update(___objWSCDetail)
                End If
            End If
        Catch
            Dim a As String = "debug"
        End Try
    End Sub

    Private Function isLaborNull() As Boolean
        'Dim objWscHeaderr As WSCHeader = CType(sessHelper.GetSession("WSCHEADER"), WSCHeader)
        Dim objWscHeaderr As WSCHeader = CType(GetSession("WSCHEADER"), WSCHeader)
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

                GetObjectLaborMaster(Position, Work, objWscHeaderr.ChassisMaster.VechileColor.VechileType.ID)
                If IsNothing(objLaborMaster) Then
                    MessageBox.Show("Kode Posisi " & Position & ", dengan Kode Kerja " & Work & ",\nuntuk Kode Kendaraan " & objWscHeaderr.ChassisMaster.VechileColor.VechileType.VechileTypeCode & ", belum Terdaftar.\nSilahkan Hubungi MMKSI")
                    Return True
                End If

                'Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(LaborMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'crit.opAnd(New Criteria(GetType(LaborMaster), "VechileType.ID", MatchType.Exact, objWscHeaderr.ChassisMaster.VechileColor.VechileType.ID))
                'crit.opAnd(New Criteria(GetType(LaborMaster), "LaborCode", MatchType.Exact, Position))
                'crit.opAnd(New Criteria(GetType(LaborMaster), "WorkCode", MatchType.Exact, Work))
                'Dim arl As ArrayList = New LaborMasterFacade(User).Retrieve(crit)
                'If arl.Count < 1 Then
                '    MessageBox.Show("Kode Posisi " & Position & ", dengan Kode Kerja " & Work & ",\nuntuk Kode Kendaraan " & objWscHeaderr.ChassisMaster.VechileColor.VechileType.VechileTypeCode & ", belum Terdaftar.\nSilahkan Hubungi MMKSI")
                '    Return True
                'End If
            End If
        Next
        Return False
    End Function

    Private Function isLaborNull2() As Boolean
        Dim result As Integer
        Dim Position As String = String.Empty
        Dim Work As String = String.Empty
        'Dim objWSCHeader As WSCHeader = CType(sessHelper.GetSession("WSCHEADER"), WSCHeader)
        Dim objWSCHeader As WSCHeader = CType(GetSession("WSCHEADER"), WSCHeader)
        Dim objWSCDetailLabor As WSCDetail
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(WSCDetail), "WSCHeader.ID", MatchType.Exact, objWSCHeader.ID))
        crit.opAnd(New Criteria(GetType(WSCDetail), "WSCType", MatchType.Exact, "L"))
        Dim arrWSCDetailLabor As ArrayList = New WSCDetailFacade(User).Retrieve(crit)
        If Not IsNothing(arrWSCDetailLabor) AndAlso arrWSCDetailLabor.Count > 0 Then
            For Each objWSDDetailLabor As WSCDetail In arrWSCDetailLabor
                Position = objWSDDetailLabor.PositionCode
                Work = objWSDDetailLabor.WorkCode

                GetObjectLaborMaster(Position, Work, objWSCHeader.ChassisMaster.VechileColor.VechileType.ID)
                If IsNothing(objLaborMaster) Then
                    MessageBox.Show("Kode Posisi " & Position & ", dengan Kode Kerja " & Work & ",\nuntuk Kode Kendaraan " & objWSCHeader.ChassisMaster.VechileColor.VechileType.VechileTypeCode & ", belum Terdaftar.\nSilahkan Hubungi MMKSI")
                    Return True
                Else
                    If IsNothing(objWSDDetailLabor.LaborMaster) Then
                        objWSDDetailLabor.LaborMaster = objLaborMaster
                        result = New WSCDetailFacade(User).Update(objWSDDetailLabor)
                    End If

                End If
            Next
        End If

        Return False
    End Function

    Protected Sub lblPQRNoVal_Click(sender As Object, e As EventArgs) Handles lblPQRNoVal.Click
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRHeader), "RowStatus", MatchType.No, CType(DBRowStatus.Deleted, Short)))
        crit.opAnd(New Criteria(GetType(PQRHeader), "PQRNo", MatchType.Exact, lblPQRNoVal.Text))
        Dim arl As ArrayList = New PQRHeaderFacade(User).Retrieve(crit)
        If arl.Count > 0 Then
            Dim oPQRHeader As PQRHeader = arl(0)
            'sessHelper.SetSession("PQR", oPQRHeader)
            SetSession("PQR", oPQRHeader)
            Server.Transfer("~/PQR/FrmPQRHeader.aspx?Mode=View&Src=WSCList&WSCId=" & Request.QueryString("WSCId") & "&State=" & Request.QueryString("VIEWSTATEMODE"))
        Else
            MessageBox.Show("Dokumen tidak valid")
        End If
    End Sub

    Private Function RetrievePQR(p1 As String) As PQRHeader
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(WSCHeader), "PQR", MatchType.Exact, p1))
        Dim _Exist As ArrayList = New WSCHeaderFacade(User).Retrieve(crit)
        'If _Exist.Count > 0 Then
        '    Return Nothing
        'Else
        Return New PQRHeaderFacade(User).Retrieve(txtPQRNo.Text)
        'End If
    End Function

    Private Function loopPart(Optional ByVal func As Object = 0) As Object
        For Each item As DataGridItem In dgParts.Items
            If item.ItemType = ListItemType.Item OrElse item.ItemType = ListItemType.AlternatingItem Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub disableEnableMainPart(ByVal e As DataGridItemEventArgs, ByVal footerEnable As Boolean, ByVal itemEnable As Boolean)
        If e.Item.ItemType = ListItemType.Footer Then
            Dim cbMainPartFooter As CheckBox = CType(e.Item.FindControl("cbMainPartFooter"), CheckBox)
            cbMainPartFooter.Enabled = footerEnable
            cbMainPartFooter.Checked = False
        ElseIf e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim cbMainPart As CheckBox = CType(e.Item.FindControl("cbMainPartItem"), CheckBox)
            cbMainPart.Enabled = itemEnable
            cbMainPart.Checked = False
        End If
    End Sub

    Private Function GetMonthlyFaultDescription() As String
        Dim _return As Boolean = False
        Dim objDealer As Dealer = CType(Session.Item("DEALER"), Dealer)
        Dim ArlMonthly As ArrayList = New ArrayList
        Dim strMessage As String = String.Empty
        Try
            Dim paramDate As DateTime = New DateTime(1900, 1, 1)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "ProductCategoryID", MatchType.Exact, "1"))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "DealerCode", MatchType.Exact, objDealer.DealerCode))
            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "Kind", MatchType.InSet, "(1,6,7)"))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "Kind", MatchType.InSet, "(1,6,7,22)"))

            'criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.V_MonthlyReport), "PeriodeYear", MatchType.Exact, "2017"), "((", True)
            'criterias.opAnd(New Criteria(GetType(KTB.DNET.Domain.V_MonthlyReport), "PeriodeMonth", MatchType.GreaterOrEqual, "4"), ")", False)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "PeriodeMonth", MatchType.GreaterOrEqual, "4"))

            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "PeriodeYear", MatchType.GreaterOrEqual, "2017"))
            'criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "PeriodeYear", MatchType.Greater, "2017"), "(", True)
            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "PeriodeMonth", MatchType.GreaterOrEqual, "1"), ")", False)

            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "CreatedTime", MatchType.GreaterOrEqual, Date.Now.AddMonths(-1)))
            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "Period", MatchType.GreaterOrEqual, New DateTime(2017, 4, 1)))


            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "LastDownloadDate", MatchType.IsNull, True), "(", True)
            'criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "LastDownloadDate", MatchType.Exact, New DateTime(1900, 1, 1)), "", False)
            'criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "LastDownloadDate", MatchType.Exact, CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)), "", False)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "LastDownloadDate", MatchType.Exact, CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)), "(", True)
            criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "TransferDate", MatchType.Exact, CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)), ")", False)
            'criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "TransferDate", MatchType.Exact, CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)), "", False)
            'criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "TransferDate", MatchType.Exact, New DateTime(1900, 1, 1)), "", False)
            'criterias.opOr(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "TransferDate", MatchType.IsNull, True), ")", False)

            Dim minMonth = 0
            If Date.Now.Day >= 20 Then
                minMonth = -1
            Else
                minMonth = -2
            End If
            Dim dtn As DateTime = New DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(minMonth)


            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.V_MonthlyReport), "Period", MatchType.LesserOrEqual, dtn))


            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(V_MonthlyReport), "Period", Sort.SortDirection.DESC))

            ArlMonthly = New V_MonthlyReportFacade(User).Retrieve(criterias, sortColl)

            If Not IsNothing(ArlMonthly) AndAlso ArlMonthly.Count > 0 Then
                Dim itr As Integer = 0
                Dim currentYear As Integer = 0
                Dim currentMonth As Integer = 0
                strMessage = "||"
                For Each item As V_MonthlyReport In ArlMonthly
                    If (itr = 0) Then
                        currentYear = item.PeriodeYear
                        strMessage = "Year Periode : " & currentYear & "|Month : "
                    End If

                    If (item.PeriodeYear = currentYear) Then
                        If (currentMonth <> item.PeriodeMonth) Then
                            strMessage = strMessage & item.PeriodeMonth & ", "
                            currentMonth = item.PeriodeMonth
                        End If
                    Else
                        currentYear = item.PeriodeYear
                        strMessage = strMessage.Substring(0, strMessage.Length - 2) & "||Year Periode : " & currentYear & "|Month : " & item.PeriodeMonth & ", "
                        currentMonth = item.PeriodeMonth
                    End If

                    itr = itr + 1
                Next

                strMessage = strMessage.Substring(0, strMessage.Length - 2)

                'Dim lengthOfMessage As Integer = strMessage.Length
                'Dim lengthOfLastWord As Integer = 5
                'Dim lastWord1 As String = strMessage.Substring(lengthOfMessage - lengthOfLastWord, lengthOfLastWord)
                'Dim lastWord2 As String = strMessage.Substring(lengthOfMessage - (lengthOfLastWord + 1), (lengthOfLastWord + 1))
                'If (lastWord1 = "Month" Or lastWord2 = "Month ") Then

                'End If
                Return strMessage
            End If
        Catch ex As Exception
            strMessage = ex.Message
        End Try
        Return strMessage
    End Function

    Private Function validateKodeKerusakan(ByVal KodePos As String, ByVal value As String, ByVal category As String) As Boolean
        If (ddlClaimType.SelectedValue = "ZA" OrElse ddlClaimType.SelectedValue = "Z2" OrElse ddlClaimType.SelectedValue = "ZB") AndAlso KodePos.Substring(0, 2).ToUpper <> "XE" Then
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

    Private Function validatePQRAndXEE999() As Boolean
        'Check pqrno in WscHeader
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(WSCHeader), "PQR", MatchType.Exact, txtPQRNo.Text))
        Dim arl As ArrayList = New WSCHeaderFacade(User).Retrieve(crit)
        If arl.Count > 0 Then
            'Check laborCode in WSCDetail
            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits.opAnd(New Criteria(GetType(WSCDetail), "PositionCode", MatchType.NotLike, "XEE999"))
            crits.opAnd(New Criteria(GetType(WSCDetail), "WorkCode", MatchType.NotLike, "99"))
            crits.opAnd(New Criteria(GetType(WSCDetail), "WSCHeader.PQR", MatchType.Exact, txtPQRNo.Text))
            crits.opAnd(New Criteria(GetType(WSCDetail), "WSCHeader.ClaimStatus", MatchType.No, "DAPP"))
            Dim arlL As ArrayList = New WSCDetailFacade(User).Retrieve(crits)
            If arlL.Count > 0 Then
                For Each item As DataGridItem In dgOngkosKerja.Items
                    If item.ItemType = ListItemType.Item OrElse item.ItemType = ListItemType.AlternatingItem Then
                        Dim txtPostionCodeItem As TextBox = CType(item.FindControl("txtPostionCodeItem"), TextBox)
                        Dim txtWorkCodeItem As TextBox = CType(item.FindControl("txtWorkCodeItem"), TextBox)
                        If Not txtPostionCodeItem.Text.ToUpper.Contains("XEE999") Then
                            Return False
                        Else
                            If txtWorkCodeItem.Text.ToUpper = ("99") Then
                                Return True
                            Else
                                Return False
                            End If
                        End If
                    End If
                Next
            Else
                Return True
            End If
        Else
            Return True
        End If
        Return False
    End Function

    Private Sub secondTimeInput(oPQRHeader As PQRHeader)
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(WSCHeader), "PQR", MatchType.Exact, txtPQRNo.Text))
        Dim arl As ArrayList = New WSCHeaderFacade(User).Retrieve(crit)
        If arl.Count > 0 Then
            'Check laborCode in WSCDetail
            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits.opAnd(New Criteria(GetType(WSCDetail), "PositionCode", MatchType.NotLike, "XEE999"))
            crits.opAnd(New Criteria(GetType(WSCDetail), "WorkCode", MatchType.NotLike, "99"))
            crits.opAnd(New Criteria(GetType(WSCDetail), "WSCHeader.PQR", MatchType.Exact, txtPQRNo.Text))
            crits.opAnd(New Criteria(GetType(WSCDetail), "WSCHeader.ClaimStatus", MatchType.No, "DAPP"))
            Dim arlL As ArrayList = New WSCDetailFacade(User).Retrieve(crits)
            If arlL.Count > 0 Then
                'Dim arlOngker As ArrayList = CType(sessHelper.GetSession("NEW_ONGKOSKERJA"), ArrayList)
                Dim arlOngker As ArrayList = CType(GetSession("NEW_ONGKOSKERJA"), ArrayList)
                If arlOngker.Count > 1 Then
                    arlOngker.RemoveRange(1, arlOngker.Count - 1)
                    dgOngkosKerja.DataSource = arlOngker
                    dgOngkosKerja.DataBind()
                End If

                For Each item As DataGridItem In dgOngkosKerja.Items
                    If item.ItemType = ListItemType.Item OrElse item.ItemType = ListItemType.AlternatingItem Then
                        Dim txtPostionCodeItem As TextBox = CType(item.FindControl("txtPostionCodeItem"), TextBox)
                        Dim txtWorkCodeItem As TextBox = CType(item.FindControl("txtWorkCodeItem"), TextBox)
                        Dim txtAmountItem As TextBox = CType(item.FindControl("txtAmountItem"), TextBox)
                        Dim lnkbtnDeleteOKerja As LinkButton = CType(item.FindControl("lnkbtnDeleteOKerja"), LinkButton)
                        Dim lblSearchWorkCode As Label = CType(item.FindControl("lblSearchWorkCodeItem"), Label)
                        Dim lblSearchPositionCode As Label = CType(item.FindControl("lblSearchPositionCodeItem"), Label)
                        txtPostionCodeItem.Text = "XEE999"
                        txtWorkCodeItem.Text = "99"
                        txtPostionCodeItem.Enabled = False
                        txtWorkCodeItem.Enabled = False
                        lblSearchPositionCode.Visible = False
                        lblSearchWorkCode.Visible = False
                        lnkbtnDeleteOKerja.Visible = False
                        'txtAmountItem.Visible = True
                        txtAmountItem.Attributes("style") = "display:table-row"
                        dgOngkosKerja.ShowFooter = False
                    End If
                Next
            End If
        End If
    End Sub

    Protected Sub lnkBtnCheckWONumber_Click(sender As Object, e As EventArgs) Handles lnkBtnCheckWONumber.Click
        Try
            If Not String.IsNullorEmpty(txtWONumber.Text.Trim) Then
                ' Get DMSWOWarrantyClaim based on WO Number            
                Dim warrantyCriteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DMSWOWarrantyClaim), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                warrantyCriteria.opAnd(New Criteria(GetType(DMSWOWarrantyClaim), "WorkOrderNumber", MatchType.Exact, txtWONumber.Text))
                warrantyCriteria.opAnd(New Criteria(GetType(DMSWOWarrantyClaim), "isBB", MatchType.Exact, 0))
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

                    If String.IsNullorEmpty(dmsWOWarrantyClaim.ServiceBuletin) Then
                        Dim pqrCriteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PQRHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        pqrCriteria.opAnd(New Criteria(GetType(PQRHeader), "WorkOrderNumber", MatchType.Exact, txtWONumber.Text))
                        Dim pqrList As ArrayList = New PQRHeaderFacade(User).Retrieve(pqrCriteria)
                        If pqrList.Count = 0 Then
                            MessageBox.Show("PQR harus dibuat")
                        Else
                            Dim pqrHeader As PQRHeader = pqrList(0)
                            LoadPQRHeaderInfo(pqrHeader)
                            ddlClaimType.SelectedValue = "Z2"
                            ddlClaimType.Enabled = False
                            ddlRefDoc.SelectedValue = "1"
                            ddlRefDoc.Enabled = False
                            txtPQRNo_TextChanged(sender, e)
                        End If
                    Else
                        ResetPQRHeaderInfo()
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
                ResetPQRHeaderInfo()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function CheckOutstandingWSC() As Boolean
        'Dim objDealer As Dealer = Me.sessHelper.GetSession("DEALER")
        Dim objDealer As Dealer = Me.GetSession("DEALER")

        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(WSCHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(WSCHeader), "Status", MatchType.Exact, CType(enumStatusWSC.Status.Baru, Integer)))
        crit.opAnd(New Criteria(GetType(WSCHeader), "CreatedTime", MatchType.Lesser, DateTime.Now.Date.AddDays(-3)))
        crit.opAnd(New Criteria(GetType(WSCHeader), "Dealer.ID", MatchType.Exact, objDealer.ID))

        Dim arl As ArrayList = New WSCHeaderFacade(User).Retrieve(crit)
        If arl.Count > 0 Then
            Server.Transfer("../FrmAccessDenied.aspx?isEncode=1&mess=" & Server.UrlEncode("Anda tidak dapat mengakses halaman ini.") & "&messDescription=" & Server.UrlEncode("<center>Segera Rilis WSC dengan status Baru</center>") & "")
        End If
    End Function

    Private Function ValidationPDIFSPM(ByRef ValidationMsg As String, ByRef oChassisMaster As ChassisMaster) As Boolean
        'Dim objDealer As Dealer = Me.sessHelper.GetSession("DEALER")
        Dim objDealer As Dealer = Me.GetSession("DEALER")
        Dim isHaveFS As Boolean = False
        Dim isHavePDI As Boolean = False

        Dim critPDI As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PDI), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'critPDI.opAnd(New Criteria(GetType(PDI), "PDIStatus", MatchType.Exact, CType(EnumFSStatus.FSStatus.Selesai, Integer)))
        critPDI.opAnd(New Criteria(GetType(PDI), "ChassisMaster.ID", MatchType.Exact, oChassisMaster.ID))
        Dim arlPDI As ArrayList = New PDIFacade(User).Retrieve(critPDI)
        If arlPDI.Count <= 0 Then
            ValidationMsg = "WSC belum memiliki PDI"
            Return False
        Else
            isHavePDI = True
        End If

        Dim critFS As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(FreeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'critFS.opAnd(New Criteria(GetType(FreeService), "Status", MatchType.Exact, CType(EnumFSStatus.FSStatus.Selesai, Integer)))
        critFS.opAnd(New Criteria(GetType(FreeService), "ChassisMaster.ID", MatchType.Exact, oChassisMaster.ID))
        Dim arlFS As ArrayList = New FreeServiceFacade(User).Retrieve(critFS)
        If arlFS.Count <= 0 Then
            If Not isHavePDI Then
                ValidationMsg = "WSC belum memiliki Free Service"
                Return False
            End If
        Else
            isHaveFS = True
        End If

        Dim critPM As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PMHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'critPM.opAnd(New Criteria(GetType(PMHeader), "PMStatus", MatchType.Exact, CType(EnumPMStatus.PMStatus.Selesai, Integer)))
        critPM.opAnd(New Criteria(GetType(PMHeader), "ChassisMaster.ID", MatchType.Exact, oChassisMaster.ID))
        Dim arlPM As ArrayList = New PMHeaderFacade(User).Retrieve(critPM)
        If IsNothing(arlPM) OrElse arlPM.Count <= 0 Then
            If Not isHaveFS AndAlso Not isHavePDI Then
                ValidationMsg = "WSC belum memiliki PM"
                Return False
            End If
        End If

        Return True
    End Function

    Private Sub CopyAttachment(objWSCEvidence As String, TempDirectory As String)
        Throw New NotImplementedException
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

    Private Function ValidationMSPExt(ByRef WSCValidationMsg As String, ByVal oChassisNumber As String) As Boolean
        Dim objDealer As Dealer = Me.GetSession("DEALER")
        Dim critMSPExRegistration As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MSPExRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critMSPExRegistration.opAnd(New Criteria(GetType(MSPExRegistration), "ChassisMaster.ChassisNumber", MatchType.Exact, oChassisNumber))
        critMSPExRegistration.opAnd(New Criteria(GetType(MSPExRegistration), "Status", MatchType.Exact, CType(EnumMSPEx.MSPExStatus.Selesai, Short)))
        Dim arlPDI As ArrayList = New MSPExRegistrationFacade(User).Retrieve(critMSPExRegistration)
        If arlPDI.Count = 0 Then
            WSCValidationMsg = "Status MSP Extended untuk chassis ini belum selesai"
            Return False
        End If
        Return True
    End Function

    Protected Sub downloadAllEvidence_Click(sender As Object, e As EventArgs) Handles downloadAllEvidence.Click
        Dim _arrWSCEVIDENCE As ArrayList = CType(GetSession("WSCEVIDENCE"), ArrayList)
        Mode = CType(ViewState("Mode"), enumMode.Mode)
        If Mode = enumMode.Mode.NewItemMode Then
            '_arrWSCEVIDENCE = CType(sessHelper.GetSession("NEW_WSCEVIDENCE"), ArrayList)
            _arrWSCEVIDENCE = CType(GetSession("NEW_WSCEVIDENCE"), ArrayList)
        End If
        Dim nameGuid As String = Guid.NewGuid().ToString().Substring(0, 5)
        If hdntxtNoChasis.Value.Trim.Length > 0 Then
            nameGuid = hdntxtNoChasis.Value & "_" & lblClaimNumber.Text
        End If
        Dim zipName = String.Empty
        If Not checkFilesOK(_arrWSCEVIDENCE) Then
            MessageBox.Show("File belum tersimpan!")
            Exit Sub
        End If
        ZipIt(_arrWSCEVIDENCE, nameGuid, zipName)
        Dim fInfo As FileInfo = New FileInfo(zipName)
        Try
            Response.Redirect("../Download.aspx?file=" & fInfo.FullName)
        Catch ex As Exception
            MessageBox.Show(SR.DownloadFail(fInfo.Name))
        End Try
    End Sub

    Private Sub ZipIt(ByVal arrLampiran As ArrayList, ByVal targetName As String, ByRef zipName As String)

        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Try
            success = imp.Start()
            If success Then

                If arrLampiran.Count = 0 Then
                    MessageBox.Show("Tidak ada file yang akan didownload")
                    Return
                End If

                If targetName.Length = 0 Then
                    MessageBox.Show("Zip file name error")
                    Return
                End If

                zipName = TargetDirectory & KTB.DNet.Lib.WebConfig.GetValue("WSCEvidenceFileDirectory") & "\" & lblDealerVal.Text & "\" & targetName & ".zip"
                Dim zipInfo As FileInfo = New FileInfo(zipName)

                If Not zipInfo.Directory.Exists Then
                    zipInfo.Directory.Create()
                End If

                If zipInfo.Exists Then
                    zipInfo.Delete()
                End If

                Using strmZipOutputStream As New ZipOutputStream(File.Create(zipName))
                    strmZipOutputStream.SetLevel(9) ' Highest Compression
                    strmZipOutputStream.Finish()
                    strmZipOutputStream.Close()
                End Using


                Using zipFile As New ZipFile(zipName)
                    zipFile.BeginUpdate()

                    For Each _wscEvidence As WSCEvidence In arrLampiran
                        If _wscEvidence.IsFromPQR Then
                            Dim fInfo As FileInfo = New FileInfo(TargetDirectory & _wscEvidence.PQRFilePath)
                            If fInfo.Exists Then
                                zipFile.Add(fInfo.FullName)
                            End If
                        Else
                            Dim fInfo As FileInfo = New FileInfo(TargetDirectory & _wscEvidence.PathFile)
                            If fInfo.Exists Then
                                zipFile.Add(fInfo.FullName)
                            End If
                        End If
                    Next

                    zipFile.CommitUpdate()
                    zipFile.Close()
                End Using

                imp.StopImpersonate()
                imp = Nothing
            End If
        Catch ex As Exception
            imp.StopImpersonate()
            imp = Nothing
            Throw ex
        End Try
    End Sub

    Private Function checkFilesOK(arrWSCEvidence As ArrayList) As Boolean
        Dim crit As New CriteriaComposite(New Criteria(GetType(WSCEvidence), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(WSCEvidence), "WSCHeader.ID", MatchType.Exact, Request.QueryString("WSCID")))
        Dim arlWSCAtt As ArrayList = New WSCEvidenceFacade(User).Retrieve(crit)
        If arlWSCAtt.Count = arrWSCEvidence.Count Then
            Return True
        End If
        Return False
    End Function

End Class

Public Class validationObj
    Public Condition As WSCParameterCondition
    Public InputValue As String
    Public Status As Boolean
End Class

Public Class FakeHttpPostedFile
    Inherits HttpPostedFileBase

    Public Overrides ReadOnly Property ContentLength As Integer
        Get
            Return InputStream.Length
        End Get
    End Property

    Public Overrides ReadOnly Property ContentType As String
        Get
            Return _ContentType
        End Get
    End Property

    Private _ContentType As String

    Public Overrides ReadOnly Property FileName As String
        Get
            Return MyBase.FileName
        End Get
    End Property

    Private _FileName As String

    Public Overrides ReadOnly Property InputStream As Stream
        Get

            If _Stream Is Nothing Then
                _Stream = New FileStream(_FileName, FileMode.Open, FileAccess.Read, FileShare.Read)
            End If

            Return _Stream
        End Get
    End Property

    Private _Stream As Stream

    Public Sub New(ByVal contentData As Byte(), ByVal contentType As String, ByVal fileName As String)
        _ContentType = contentType
        _FileName = fileName
        _Stream = New MemoryStream(contentData)
    End Sub

    Public Overrides Sub SaveAs(ByVal filename As String)
        File.WriteAllBytes(filename, File.ReadAllBytes(_FileName))
    End Sub ' SaveAs
End Class
