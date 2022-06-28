#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.AfterSales
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.WebCC
Imports MMKSI.DNetUpload.Utility
Imports System.Collections.Generic
Imports OfficeOpenXml
Imports GlobalExtensions
Imports KTB.DNet.BusinessValidation

#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
Imports System.Text
Imports System.Reflection

#End Region

Public Class FrmServiceStandardTime
    Inherits System.Web.UI.Page
#Region " Private fields "
    Private sessHelper As SessionHelper = New SessionHelper
    Private crit As CriteriaComposite
    Private objDealer As New Dealer
    Private oLoginUser As New UserInfo
    Private m_bInputPrivilege As Boolean = False
    Private stdFacade As StandardCodeFacade = New StandardCodeFacade(User)
    Private vechileModelFacade As VechileModelFacade = New VechileModelFacade(User)
    Private fSKindFacade As FSKindFacade = New FSKindFacade(User)
    Private pMKindFacade As PMKindFacade = New PMKindFacade(User)
    Private gRKindFacade As GRKindFacade = New GRKindFacade(User)

    Private recallCategoryFacade As RecallCategoryFacade = New RecallCategoryFacade(User)
    Private serviceBookingFacade As ServiceBookingFacade = New ServiceBookingFacade(User)
    Private freeServiceFacade As FreeServiceFacade = New FreeServiceFacade(User)
    Private recallServiceFacade As RecallServiceFacade = New RecallServiceFacade(User)
    Private pmHeaderFacade As PMHeaderFacade = New PMHeaderFacade(User)
    Private vechileTypeFacade As VechileTypeFacade = New VechileTypeFacade(User)
    Private chassisMasterFacade As ChassisMasterFacade = New ChassisMasterFacade(User)
    Private dealerFacade As DealerFacade = New DealerFacade(User)
    Private stallMasterFacade As StallMasterFacade = New StallMasterFacade(User)
    Private assistServiceTypeFacade As AssistServiceTypeFacade = New AssistServiceTypeFacade(User)
    Dim isDealerDMS As Boolean = False
    Private isDealerPiloting As Boolean = False

#End Region

#Region "Event Handler"
    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        oLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Server.ScriptTimeout = 300
        CheckPrivilege()
        If Not IsPostBack Then
            'btnCalculateAll.Attributes("onclick") = "ShowPPServiceStdTime();"
            ResetControl()
            txtKodeDealer.Attributes.Add("readonly", True)
            If Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
                txtKodeDealer.Text = objDealer.DealerCode
                lblPopupDealer.Visible = False
                btnSave.Visible = True
                btnCalculate1.Visible = True
            Else
                txtKodeDealer.Text = ""
                lblPopupDealer.Visible = True
                btnSave.Visible = False
                btnCalculateAll.Visible = False
            End If
            'ViewState("Mode") = "New"
            'lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            ReadData()
            dtgServiceStandardTime.CurrentPageIndex = 0
            BindPage(dtgServiceStandardTime.CurrentPageIndex)
            'ResetHistory()
        End If
    End Sub

    Protected Sub ddlAssistServiceType_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Protected Sub ddlModelKendaraan_SelectedIndexChanged(sender As Object, e As EventArgs)
        ddlVehicleType.Items.Clear()

        If Not IsNothing(sender) Then
            Dim ddl As DropDownList = sender
            If ddl.SelectedIndex <> 0 Then
                crit = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.Exact, "A"))
                crit.opAnd(New Criteria(GetType(VechileType), "VechileModel.ID", MatchType.Exact, ddl.SelectedValue))
                Dim results As ArrayList = VechileTypeFacade.Retrieve(crit)

                With ddlVehicleType.Items
                    For Each obj As VechileType In results
                        .Add(New ListItem(obj.Description, obj.ID))
                    Next
                End With
            End If
        End If
        ddlVehicleType.Items.Insert(0, "Silahkan Pilih")
    End Sub

    Protected Sub ddlJenisKegiatan_SelectedIndexChanged(sender As Object, e As EventArgs)
        ddlJenisService.Items.Clear()

        If Not IsNothing(sender) Then
            Dim ddl As DropDownList = sender
            Select Case ddl.SelectedValue
                Case 1
                    crit = New CriteriaComposite(New Criteria(GetType(FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim results As ArrayList = fSKindFacade.Retrieve(crit)

                    With ddlJenisService.Items
                        For Each obj As FSKind In results
                            .Add(New ListItem(String.Format("{0} - {1}", obj.KindCode, obj.KindDescription), obj.KindCode))
                        Next
                    End With
                Case 2
                    crit = New CriteriaComposite(New Criteria(GetType(PMKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim results As ArrayList = pMKindFacade.Retrieve(crit)

                    With ddlJenisService.Items
                        For Each obj As PMKind In results
                            .Add(New ListItem(String.Format("{0} - {1}", obj.KindCode, obj.KindDescription), obj.KindCode))
                        Next
                    End With
                Case 3
                    crit = New CriteriaComposite(New Criteria(GetType(RecallCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim results As ArrayList = recallCategoryFacade.Retrieve(crit)

                    With ddlJenisService.Items
                        For Each obj As RecallCategory In results
                            .Add(New ListItem(String.Format("{0} - {1}", obj.RecallRegNo, obj.Description), obj.RecallRegNo))
                        Next
                    End With
                Case 4
                    crit = New CriteriaComposite(New Criteria(GetType(GRKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim results As ArrayList = GRKindFacade.Retrieve(crit)

                    With ddlJenisService.Items
                        For Each obj As GRKind In results
                            .Add(New ListItem(String.Format("{0} - {1}", obj.KindCode, obj.KindDescription), obj.KindCode))
                        Next
                    End With
            End Select
        End If

        ddlJenisService.Items.Insert(0, "Silahkan Pilih")

    End Sub

    Protected Sub ddlJenisService_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        If Not Page.IsValid Then
            Return
        End If
       
        Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        Dim objUser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        ' Dim mode As String = "add"
        If Not objDealer Is Nothing Then
            If Not objDealer.DealerGroup Is Nothing Then
                If objDealer.DealerCode <> txtKodeDealer.Text Then
                    MessageBox.Show("Tidak dapat menyimpan Service Standard Time yang tidak sesuai dengan dealer anda.")
                    Return
                End If
            End If
            'txtKodeDealer.Text = objDealer.DealerCode
            'txtNamaDealer.Text = objDealer.DealerName

        End If
        'txtKodeDealer.Text = HiddenField1.Value
        If (txtKodeDealer.Text = "" Or ddlAssistServiceType.SelectedIndex = 0 Or ddlModelKendaraan.SelectedIndex = 0 Or ddlJenisKegiatan.SelectedIndex = 0 Or ddlJenisService.SelectedIndex = 0 Or txtStandardDealer.Text = "") Then
            MessageBox.Show("Semua Data Wajib Diisi.")
            Return
        End If

        If (CType(ViewState("vsProcess"), String) = "Edit") Then
            UpdateModel()
        Else
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ServiceStandardTime), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If Not objDealer Is Nothing Then
                If Not objDealer.DealerGroup Is Nothing Then
                    criterias.opAnd(New Criteria(GetType(ServiceStandardTime), "Dealer.DealerGroup.DealerGroupCode", MatchType.Exact, objDealer.DealerGroup.DealerGroupCode))
                End If

            End If

            If ddlJenisService.SelectedIndex <> 0 Then
                criterias.opAnd(New Criteria(GetType(ServiceStandardTime), "KindCode", MatchType.Exact, ddlJenisService.SelectedValue))
            End If

            If ddlJenisKegiatan.SelectedIndex <> 0 Then
                criterias.opAnd(New Criteria(GetType(ServiceStandardTime), "ServiceTypeID", MatchType.Exact, ddlJenisKegiatan.SelectedValue))
            End If

            If ddlAssistServiceType.SelectedIndex <> 0 Then
                criterias.opAnd(New Criteria(GetType(ServiceStandardTime), "AssistServiceTypeCode", MatchType.Exact, ddlAssistServiceType.SelectedValue))
            End If

            If ddlModelKendaraan.SelectedIndex <> 0 Then
                criterias.opAnd(New Criteria(GetType(ServiceStandardTime), "VechileModel.ID", MatchType.Exact, ddlModelKendaraan.SelectedValue))
            End If

            If ddlVehicleType.SelectedIndex <> 0 Then
                criterias.opAnd(New Criteria(GetType(ServiceStandardTime), "VechileType.ID", MatchType.Exact, ddlVehicleType.SelectedValue))
            End If

            ViewState("CurrentSortColumn") = "ID"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

            Dim arrServiceStandard As ArrayList = New ServiceStandardTimeFacade(User).Retrieve(criterias)
            

            If (arrServiceStandard.Count <> 0) Then
                Dim objSTT1 As New ServiceStandardTime
                objSTT1 = CType(arrServiceStandard(0), ServiceStandardTime)
                HiddenField3.Value = objSTT1.ID
                UpdateModel()
            Else
                InsertModel()
            End If
        End If
        btnBatal_Click(Nothing, Nothing)
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs)
        ReadData()
        
        dtgServiceStandardTime.CurrentPageIndex = 0
        BindPage(dtgServiceStandardTime.CurrentPageIndex)
    End Sub

    Protected Sub btnBatal_Click(sender As Object, e As EventArgs)
        ResetControl()
        ReadData()
        dtgServiceStandardTime.CurrentPageIndex = 0
        BindPage(dtgServiceStandardTime.CurrentPageIndex)
        'btnDownload.Enabled = False
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs)
        Dim arrServiceStandard As ArrayList = CType(sessHelper.GetSession("ServiceStandardTimeList"), ArrayList)
        'Dim aStatus As New ArrayList
        If arrServiceStandard.Count <> 0 Then
            '   DoDownload(arrStallMaster)
            SetDownload()
        End If
    End Sub

    Private Sub dtgServiceStandardTime_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgServiceStandardTime.PageIndexChanged
        '-- Change datagrid page

        dtgServiceStandardTime.CurrentPageIndex = e.NewPageIndex
        BindPage(e.NewPageIndex)

    End Sub

    Private Sub dtgServiceStandardTime_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgServiceStandardTime.ItemCommand
        If e.CommandName = "lnkDetail" Then
            ViewState.Add("vsProcess", "Edit")
            HiddenField3.Value = e.Item.Cells(0).Text
            Dim lbNo As Label = e.Item.FindControl("lbNo")
            Dim lbKodeDealer As Label = e.Item.FindControl("lbKodeDealer")
            Dim lbAssistServiceType As Label = e.Item.FindControl("lbAssistServiceType")
            Dim lbModelKendaraan As Label = e.Item.FindControl("lbModelKendaraan")
            Dim lbModelKendaraan2 As Label = e.Item.FindControl("lbModelKendaraan2")
            Dim lbTipeKendaraan As Label = e.Item.FindControl("lbTipeKendaraan")
            Dim lbJenisKegiatan As Label = e.Item.FindControl("lbJenisKegiatan")
            Dim lbJenisKegiatan2 As Label = e.Item.FindControl("lbJenisKegiatan2")
            Dim lbJenisService As Label = e.Item.FindControl("lbJenisService")
            Dim lbJenisService2 As Label = e.Item.FindControl("lbJenisService2")
            Dim lbStandardDealer As Label = e.Item.FindControl("lbStandardDealer")
            Dim lnkDetail As LinkButton = e.Item.FindControl("lnkDetail")

            txtKodeDealer.Text = lbKodeDealer.Text
            txtStandardDealer.Text = lbStandardDealer.Text

            Dim criteriasu As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteriasu.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "ServiceBooking.StallServiceType"))
            criteriasu.opAnd(New Criteria(GetType(StandardCode), "ValueDesc", MatchType.Exact, lbAssistServiceType.Text))
            'If lbAssistServiceType.Text = "Regular" Then
            '    criteriasu.opAnd(New Criteria(GetType(StandardCode), "ValueDesc", MatchType.Exact, "lbAssistServiceType"))
            'Else
            '    criteriasu.opAnd(New Criteria(GetType(AssistServiceType), "ServiceTypeCode", MatchType.Exact, lbAssistServiceType.Text))
            'End If
            Dim arrDDLsu As ArrayList = New StandardCodeFacade(User).Retrieve(criteriasu)
            Dim objStandardCodesu As New StandardCode
            If Not IsNothing(arrDDLsu) AndAlso arrDDLsu.Count > 0 Then
                objStandardCodesu = CType(arrDDLsu(0), StandardCode)
                ddlAssistServiceType.SelectedValue = objStandardCodesu.ValueCode
            End If

            Dim criterias1 As New CriteriaComposite(New Criteria(GetType(VechileModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias1.opAnd(New Criteria(GetType(VechileModel), "ID", MatchType.Exact, lbModelKendaraan2.Text))
            Dim arrDDL1 As ArrayList = New VechileModelFacade(User).Retrieve(criterias1)
            Dim objStandardCode1 As New VechileModel
            If Not IsNothing(arrDDL1) AndAlso arrDDL1.Count > 0 Then
                objStandardCode1 = CType(arrDDL1(0), VechileModel)
                ddlModelKendaraan.SelectedValue = objStandardCode1.ID
                ddlModelKendaraan_SelectedIndexChanged(ddlModelKendaraan, Nothing)
            End If

            Dim criteriass As New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteriass.opAnd(New Criteria(GetType(VechileType), "VechileModel.ID", MatchType.Exact, ddlModelKendaraan.SelectedValue.ToString()))
            criteriass.opAnd(New Criteria(GetType(VechileType), "Description", MatchType.Exact, lbTipeKendaraan.Text))
            Dim arrDDLl As ArrayList = New VechileTypeFacade(User).Retrieve(criteriass)
            Dim objStandardCodee As New VechileType
            If Not IsNothing(arrDDLl) AndAlso arrDDLl.Count > 0 Then
                objStandardCodee = CType(arrDDLl(0), VechileType)
                ddlVehicleType.SelectedValue = objStandardCodee.ID
                'ddlModelKendaraan_SelectedIndexChanged(ddlModelKendaraan, Nothing)
            End If

            Dim criterias3 As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias3.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "ServiceBooking.ServiceType"))
            criterias3.opAnd(New Criteria(GetType(StandardCode), "ValueDesc", MatchType.Exact, lbJenisKegiatan.Text))
            Dim arrDDL As ArrayList = New StandardCodeFacade(User).Retrieve(criterias3)
            Dim objStandardCode As New StandardCode
            If Not IsNothing(arrDDL) AndAlso arrDDL.Count > 0 Then
                objStandardCode = CType(arrDDL(0), StandardCode)
                ddlJenisKegiatan.SelectedValue = objStandardCode.ValueId
                ddlJenisKegiatan_SelectedIndexChanged(ddlJenisKegiatan, Nothing)
            End If

            Select Case lbJenisKegiatan2.Text
                Case 1
                    crit = New CriteriaComposite(New Criteria(GetType(FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crit.opAnd(New Criteria(GetType(FSKind), "KindCode", MatchType.Exact, lbJenisService2.Text))
                    Dim results As ArrayList = fSKindFacade.Retrieve(crit)

                    Dim objFSKind As New FSKind
                    If Not IsNothing(results) AndAlso results.Count > 0 Then
                        objFSKind = CType(results(0), FSKind)
                        ddlJenisService.SelectedValue = objFSKind.KindCode
                        '                        ddlJenisKegiatan_SelectedIndexChanged(ddlJenisKegiatan, Nothing)
                    End If
                Case 2
                    crit = New CriteriaComposite(New Criteria(GetType(PMKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crit.opAnd(New Criteria(GetType(PMKind), "KindCode", MatchType.Exact, lbJenisService2.Text))
                    Dim results As ArrayList = pMKindFacade.Retrieve(crit)

                    Dim objPMKind As New PMKind
                    If Not IsNothing(results) AndAlso results.Count > 0 Then
                        objPMKind = CType(results(0), PMKind)
                        ddlJenisService.SelectedValue = objPMKind.KindCode
                        '                        ddlJenisKegiatan_SelectedIndexChanged(ddlJenisKegiatan, Nothing)
                    End If
                Case 3
                    crit = New CriteriaComposite(New Criteria(GetType(RecallCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crit.opAnd(New Criteria(GetType(RecallCategory), "RecallRegNo", MatchType.Exact, lbJenisService2.Text))
                    Dim results As ArrayList = recallCategoryFacade.Retrieve(crit)

                    Dim objRecallCategory As New RecallCategory
                    If Not IsNothing(results) AndAlso results.Count > 0 Then
                        objRecallCategory = CType(results(0), RecallCategory)
                        ddlJenisService.SelectedValue = objRecallCategory.RecallRegNo
                        '                        ddlJenisKegiatan_SelectedIndexChanged(ddlJenisKegiatan, Nothing)
                    End If
                Case 4
                    crit = New CriteriaComposite(New Criteria(GetType(GRKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crit.opAnd(New Criteria(GetType(GRKind), "KindCode", MatchType.Exact, lbJenisService2.Text))
                    Dim results As ArrayList = gRKindFacade.Retrieve(crit)

                    Dim objGRKind As New GRKind
                    If Not IsNothing(results) AndAlso results.Count > 0 Then
                        objGRKind = CType(results(0), GRKind)
                        ddlJenisService.SelectedValue = objGRKind.KindCode
                        '                        ddlJenisKegiatan_SelectedIndexChanged(ddlJenisKegiatan, Nothing)
                    End If
            End Select

            SetControl(False)
            txtStandardDealer.Enabled = True
            'Dim criterias3a As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'criterias3a.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "ServiceBooking.ServiceType"))
            'criterias3a.opAnd(New Criteria(GetType(StandardCode), "ValueDesc", MatchType.Exact, lbJenisKegiatan.Text))
            'Dim arrDDL As ArrayList = New StandardCodeFacade(User).Retrieve(criterias3a)
            'Dim objStandardCode As New StandardCode
            'If Not IsNothing(arrDDL) AndAlso arrDDL.Count > 0 Then
            '    objStandardCode = CType(arrDDL(0), StandardCode)
            '    ddlJenisKegiatan.SelectedValue = objStandardCode.ValueId
            '    ddlJenisKegiatan_SelectedIndexChanged(ddlJenisKegiatan, Nothing)
            'End If

            'ddlJenisService.SelectedValue = lbJenisService2.Text

        End If
    End Sub

    Private Sub dtgServiceStandardTime_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgServiceStandardTime.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblNo As Label = CType(e.Item.FindControl("lbNo"), Label)
            Dim lnkDetail As LinkButton = CType(e.Item.FindControl("lnkDetail"), LinkButton)
            Dim lbJenisKegiatan As Label = CType(e.Item.FindControl("lbJenisKegiatan"), Label)
            Dim lbJenisService As Label = CType(e.Item.FindControl("lbJenisService"), Label)
            Dim lbStatus As Label = CType(e.Item.FindControl("lbStatus"), Label)
            Dim lbKodeDealer As Label = CType(e.Item.FindControl("lbKodeDealer"), Label)
            Dim lbAssistServiceType As Label = e.Item.FindControl("lbAssistServiceType")


            lblNo.Text = (dtgServiceStandardTime.CurrentPageIndex * dtgServiceStandardTime.PageSize + e.Item.ItemIndex + 1).ToString()  '-- Column No

            If lbAssistServiceType.Text = "SB" Then
                lbAssistServiceType.Text = "Regular"
            End If

            Select Case lbJenisKegiatan.Text
                Case 1
                    Dim criterias2 As New CriteriaComposite(New Criteria(GetType(FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias2.opAnd(New Criteria(GetType(FSKind), "KindCode", MatchType.Exact, lbJenisService.Text))
                    Dim arrFSK2 As ArrayList = New FSKindFacade(User).Retrieve(criterias2)
                    Dim objFSKind As New FSKind
                    If Not IsNothing(arrFSK2) AndAlso arrFSK2.Count > 0 Then
                        objFSKind = CType(arrFSK2(0), FSKind)
                        lbJenisService.Text = objFSKind.KindCode & " - " & objFSKind.KindDescription
                    End If

                Case 2
                    Dim criterias0 As New CriteriaComposite(New Criteria(GetType(PMKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias0.opAnd(New Criteria(GetType(PMKind), "KindCode", MatchType.Exact, lbJenisService.Text))
                    Dim arrFSK As ArrayList = New PMKindFacade(User).Retrieve(criterias0)
                    Dim objPMKind As New PMKind
                    If Not IsNothing(arrFSK) AndAlso arrFSK.Count > 0 Then
                        objPMKind = CType(arrFSK(0), PMKind)
                        lbJenisService.Text = objPMKind.KindCode & " - " & objPMKind.KindDescription
                    End If
                Case 3
                    Dim criteriasa As New CriteriaComposite(New Criteria(GetType(RecallCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criteriasa.opAnd(New Criteria(GetType(RecallCategory), "RecallRegNo", MatchType.Exact, lbJenisService.Text))
                    Dim arrRC As ArrayList = New RecallCategoryFacade(User).Retrieve(criteriasa)
                    Dim objRecallCategory As New RecallCategory
                    If Not IsNothing(arrRC) AndAlso arrRC.Count > 0 Then
                        objRecallCategory = CType(arrRC(0), RecallCategory)
                        lbJenisService.Text = objRecallCategory.RecallRegNo & " - " & objRecallCategory.Description
                    End If
                Case 4
                    Dim criterias01 As New CriteriaComposite(New Criteria(GetType(GRKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias01.opAnd(New Criteria(GetType(GRKind), "KindCode", MatchType.Exact, lbJenisService.Text))
                    Dim arrFSK As ArrayList = New GRKindFacade(User).Retrieve(criterias01)
                    Dim objPMKind As New GRKind
                    If Not IsNothing(arrFSK) AndAlso arrFSK.Count > 0 Then
                        objPMKind = CType(arrFSK(0), GRKind)
                        lbJenisService.Text = objPMKind.KindCode & " - " & objPMKind.KindDescription
                    End If
            End Select


            Dim criterias3 As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias3.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "ServiceBooking.ServiceType"))
            criterias3.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, lbJenisKegiatan.Text))
            Dim arrDDL As ArrayList = New StandardCodeFacade(User).Retrieve(criterias3)
            Dim objStandardCode As New StandardCode
            If Not IsNothing(arrDDL) AndAlso arrDDL.Count > 0 Then
                objStandardCode = CType(arrDDL(0), StandardCode)
                lbJenisKegiatan.Text = objStandardCode.ValueDesc
            End If

            Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
            Dim objUser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)

            Dim dealerSystems As DealerSystems = New DealerSystemsFacade(User).RetrieveByDealerCode(objDealer.DealerCode)

            If Not objDealer Is Nothing Then
                If (objDealer.DealerCode = lbKodeDealer.Text) Then
                    lnkDetail.Visible = True
                Else
                    lnkDetail.Visible = False
                End If
            Else
                lnkDetail.Visible = False
            End If

                'If lbStatus.Text = "1" Then
                '    If Not IsNothing(dealerSystems) AndAlso dealerSystems.SystemID <> 1 Then
                '        lnkDetail.Visible = False
                '    Else
                '        If Not objDealer Is Nothing Then
                '            If (objDealer.DealerCode = lbNoReservasi.Text.Substring(0, 6)) Then
                '                lnkDetail.Visible = True
                '            Else
                '                lnkDetail.Visible = False
                '            End If
                '        End If
                '    End If
                'Else
                '    lnkDetail.Visible = False
                'End If


                'Dim criterias1 As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'criterias1.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "ServiceBooking.Status"))
                'criterias1.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, lbStatus.Text))
                'Dim arrDDL1 As ArrayList = New StandardCodeFacade(User).Retrieve(criterias1)
                'Dim objStandardCode1 As New StandardCode
                'If Not IsNothing(arrDDL1) AndAlso arrDDL1.Count > 0 Then
                '    objStandardCode1 = CType(arrDDL1(0), StandardCode)
                '    lbStatus.Text = objStandardCode1.ValueDesc
                'End If


            End If
    End Sub
#End Region

#Region "Custom Method"

    Private Sub CheckPrivilege()
        m_bInputPrivilege = SecurityProvider.Authorize(Context.User, SR.ServiceSTD_View_Privilage)
        isDealerPiloting = TCHelper.GetActiveTCResult(objDealer.ID, CInt(EnumDealerTransType.DealerTransKind.PilotingServiceStandardTime))
        

        If Not objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If (Not m_bInputPrivilege Or Not isDealerPiloting) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Stall - Service Standard Time")
            End If
        Else
            If (Not m_bInputPrivilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Stall - Service Standard Time")
            End If
        End If
    End Sub

    Private Sub ResetControl()
        InitDdl()
        txtStandardSystem.Attributes.Add("readonly", "true")
        'txtKodeDealer.Text = ""
        txtStandardDealer.Text = ""
        txtStandardSystem.Text = "[Auto Generate]"
        Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        Dim objUser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)

        Dim dealerSystems As DealerSystems = New DealerSystemsFacade(User).RetrieveByDealerCode(objDealer.DealerCode)
        If objDealer.DealerGroup Is Nothing Then
            txtKodeDealer.Text = ""
        End If

        SetControl(True)
    End Sub

    Private Sub SetControl(ByVal opt As Boolean)
        txtStandardDealer.Enabled = opt
        ddlAssistServiceType.Enabled = opt
        ddlJenisKegiatan.Enabled = opt
        ddlModelKendaraan.Enabled = opt
        ddlJenisService.Enabled = opt
        ddlVehicleType.Enabled = opt
    End Sub

    Private Sub InitDdl()
        Dim results As ArrayList

        ddlModelKendaraan.Items.Clear()

        crit = New CriteriaComposite(New Criteria(GetType(VechileModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        results = vechileModelFacade.Retrieve(crit)

        With ddlModelKendaraan.Items
            For Each obj As VechileModel In results
                .Add(New ListItem(String.Format("{0} - {1}", obj.VechileModelCode, obj.IndDescription), obj.ID))
            Next
        End With

        ddlModelKendaraan.Items.Insert(0, "Silahkan Pilih")

        ddlJenisKegiatan.Items.Clear()

        crit = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "ServiceBooking.ServiceType"))

        results = stdFacade.Retrieve(crit)

        With ddlJenisKegiatan.Items
            For Each obj As StandardCode In results
                .Add(New ListItem(obj.ValueDesc, obj.ValueId))
            Next
        End With

        ddlJenisKegiatan.Items.Insert(0, "Silahkan Pilih")

        ddlAssistServiceType.Items.Clear()

        'crit = New CriteriaComposite(New Criteria(GetType(AssistServiceType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'crit.opAnd(New Criteria(GetType(AssistServiceType), "ID", MatchType.InSet, "(1,9)"))

        'results = assistServiceTypeFacade.Retrieve(crit)
        'Dim strServiceTypeCode As String = ""
        'With ddlAssistServiceType.Items
        '    For Each obj As AssistServiceType In results
        '        If (obj.ServiceTypeCode.ToString() = "SB") Then
        '            strServiceTypeCode = "Regular"
        '            .Add(New ListItem(strServiceTypeCode, obj.ID))
        '        Else
        '            .Add(New ListItem(obj.ServiceTypeCode, obj.ID))
        '        End If

        '    Next
        'End With

        crit = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "ServiceBooking.StallServiceType"))

        results = New StandardCodeFacade(User).Retrieve(crit)
        With ddlAssistServiceType.Items
            For Each obj As StandardCode In results
                .Add(New ListItem(obj.ValueDesc, obj.ValueCode))
                'If (obj.ServiceTypeCode.ToString() = "SB") Then
                '    strServiceTypeCode = "Regular"
                '    .Add(New ListItem(strServiceTypeCode, obj.ID))
                'Else
                '    .Add(New ListItem(obj.ServiceTypeCode, obj.ID))
                'End If

            Next
        End With

        ddlAssistServiceType.Items.Insert(0, "Silahkan Pilih")

        ddlJenisKegiatan_SelectedIndexChanged(Nothing, Nothing)
        ddlModelKendaraan_SelectedIndexChanged(Nothing, Nothing)

        ddlJenisKegiatan2.Items.Clear()

        crit = New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "ServiceBooking.ServiceType"))

        results = stdFacade.Retrieve(crit)

        With ddlJenisKegiatan2.Items
            For Each obj As StandardCode In results
                .Add(New ListItem(obj.ValueDesc, obj.ValueId))
            Next
        End With

        ddlJenisKegiatan2.Items.Insert(0, "Silahkan Pilih")

    End Sub

    Private Sub ReadData()
        '-- Read all data selected
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.ServiceStandardTime), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        Dim objUser As UserInfo = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        If Not objDealer Is Nothing Then
            If Not objDealer.DealerGroup Is Nothing Then
                criterias.opAnd(New Criteria(GetType(ServiceStandardTime), "Dealer.DealerGroup.DealerGroupCode", MatchType.Exact, objDealer.DealerGroup.DealerGroupCode))
            End If

        End If

        If (txtKodeDealer.Text <> "") Then
            criterias.opAnd(New Criteria(GetType(ServiceStandardTime), "Dealer.DealerCode", MatchType.Exact, txtKodeDealer.Text))
        End If
        
        If (txtStandardDealer.Text <> "") Then
            criterias.opAnd(New Criteria(GetType(ServiceStandardTime), "DealerStandardTime", MatchType.Exact, txtStandardDealer.Text.Replace(",", ".")))
        End If

        If ddlJenisService.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(ServiceStandardTime), "KindCode", MatchType.Exact, ddlJenisService.SelectedValue))
        End If

        If ddlJenisKegiatan.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(ServiceStandardTime), "ServiceTypeID", MatchType.Exact, ddlJenisKegiatan.SelectedValue))
        End If

        If ddlAssistServiceType.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(ServiceStandardTime), "AssistServiceTypeCode", MatchType.Exact, ddlAssistServiceType.SelectedValue))
        End If

        If ddlModelKendaraan.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(ServiceStandardTime), "VechileModel.ID", MatchType.Exact, ddlModelKendaraan.SelectedValue))
        End If

        If ddlVehicleType.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(ServiceStandardTime), "VechileType.ID", MatchType.Exact, ddlVehicleType.SelectedValue))
        End If

        'criterias.opAnd(New Criteria(GetType(ServiceStandardTime), "ID", MatchType.Exact, "59382"))

        ViewState("CurrentSortColumn") = "ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

        Dim arrServiceStandard As ArrayList = New ServiceStandardTimeFacade(User).Retrieve(criterias)


        '-- Store InvoiceReqList into session for later use
        sessHelper.SetSession("ServiceStandardTimeList", arrServiceStandard)
        If txtKodeDealer.Text <> "" And ddlAssistServiceType.SelectedIndex <> 0 And ddlJenisKegiatan.SelectedIndex <> 0 Then
            btnDownload.Enabled = True
        Else
            btnDownload.Enabled = False
        End If
    End Sub

    Private Sub BindPage(ByVal pageIndex As Integer)
        Dim arrServiceStandard As ArrayList = CType(sessHelper.GetSession("ServiceStandardTimeList"), ArrayList)
        Dim aStatus As New ArrayList
        If arrServiceStandard.Count <> 0 Then
            ' SortListControl(arrStallMaster, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(arrServiceStandard, pageIndex, dtgServiceStandardTime.PageSize)
            dtgServiceStandardTime.DataSource = PagedList
            dtgServiceStandardTime.VirtualItemCount = arrServiceStandard.Count()
            dtgServiceStandardTime.DataBind()
        Else
            dtgServiceStandardTime.DataSource = New ArrayList
            dtgServiceStandardTime.VirtualItemCount = 0
            dtgServiceStandardTime.CurrentPageIndex = 0
            dtgServiceStandardTime.DataBind()
        End If
    End Sub


    Private Function InsertModel() As Integer
        Dim objServiceStandardTime As ServiceStandardTime = New ServiceStandardTime

        Dim nResult As Integer
        objServiceStandardTime.Dealer = New DealerFacade(User).GetDealer(txtKodeDealer.Text)

        'objStallMaster.Dealer.DealerCode = txtKodeDealer.Text
        objServiceStandardTime.AssistServiceTypeCode = ddlAssistServiceType.SelectedValue 'assistServiceTypeFacade.Retrieve(CInt(ddlAssistServiceType.SelectedValue))
        objServiceStandardTime.VechileModel = vechileModelFacade.Retrieve(CInt(ddlModelKendaraan.SelectedValue))
        objServiceStandardTime.VechileType = vechileTypeFacade.Retrieve(CInt(ddlVehicleType.SelectedValue))
        objServiceStandardTime.ServiceTypeID = ddlJenisKegiatan.SelectedValue
        objServiceStandardTime.KindCode = ddlJenisService.SelectedValue
        objServiceStandardTime.DealerStandardTime = txtStandardDealer.Text
        objServiceStandardTime.SystemStandardTime = 0
        objServiceStandardTime.Notes = ""
        objServiceStandardTime.ProcessCode = ""
        'objServiceStandardTime.CreatedBy = 

        nResult = New ServiceStandardTimeFacade(User).Insert(objServiceStandardTime)
        If nResult = -1 Then
            MessageBox.Show(SR.SaveFail)
        Else
            MessageBox.Show(SR.SaveSuccess)
        End If
        Return nResult
    End Function

    Private Function UpdateModel() As Integer


        Dim objServiceStandardTime As ServiceStandardTime = New ServiceStandardTime

        objServiceStandardTime.ID = HiddenField3.Value
        objServiceStandardTime.Dealer = New DealerFacade(User).GetDealer(txtKodeDealer.Text)
        objServiceStandardTime.AssistServiceTypeCode = ddlAssistServiceType.SelectedValue 'assistServiceTypeFacade.Retrieve(CInt(ddlAssistServiceType.SelectedValue))
        objServiceStandardTime.VechileModel = vechileModelFacade.Retrieve(CInt(ddlModelKendaraan.SelectedValue))
        objServiceStandardTime.VechileType = vechileTypeFacade.Retrieve(CInt(ddlVehicleType.SelectedValue))
        objServiceStandardTime.ServiceTypeID = ddlJenisKegiatan.SelectedValue
        objServiceStandardTime.KindCode = ddlJenisService.SelectedValue
        objServiceStandardTime.DealerStandardTime = txtStandardDealer.Text 'Format(Double.Parse(txtStandardDealer.Text), "0.00") 'Convert.ToDecimal(txtStandardDealer.Text)
        objServiceStandardTime.SystemStandardTime = 0
        objServiceStandardTime.Notes = ""
        objServiceStandardTime.ProcessCode = ""
        Dim nResult = New ServiceStandardTimeFacade(User).Update(objServiceStandardTime)
        If nResult = -1 Then
            MessageBox.Show(SR.SaveFail)
        Else
            MessageBox.Show(SR.SaveSuccess)
            ViewState.Add("vsProcess", "Add")
        End If
        Return nResult
    End Function

#End Region

#Region "download excel"

    Private Sub SetDownload()
        Dim arrData As New DataTable
        Dim crits As CriteriaComposite
        If dtgServiceStandardTime.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        'If Not IsNothing(sessHelp.GetSession("criteriadownload")) Then
        '    crits = CType(sessHelp.GetSession("criteriadownload"), CriteriaComposite)
        'End If
        ' mengambil data yang dibutuhkan
        Dim arrServiceBooking As ArrayList = CType(sessHelper.GetSession("ServiceStandardTimeList"), ArrayList)
        Dim propertiesinfo As PropertyInfo() = arrServiceBooking(0).GetType().GetProperties()

        'For Each pf As PropertyInfo In propertiesinfo
        '    Dim dc As DataColumn = New DataColumn(pf.Name)
        '    dc.DataType = pf.PropertyType
        '    arrData.Columns.Add(dc)
        'Next

        'For Each ar As Object In arrFlatRate
        '    Dim dr As DataRow = arrData.NewRow
        '    Dim pf As PropertyInfo() = ar.GetType().GetProperties()

        '    For Each prop As PropertyInfo In pf
        '        dr(prop.Name) = prop.GetValue(ar, Nothing)
        '    Next
        '    arrData.Rows.Add(dr)
        'Next

        'arrData = New VW_FlatRateMasterFacade(User).GetDownLoadExcel(crits.ToString())

        If arrServiceBooking.Count > 0 Then
            CreateExcel("ServiceStandardTime", arrServiceBooking)
        End If

    End Sub

    Private Sub CreateExcel(ByVal FileName As String, ByVal Data As DataTable)
        Using pck As New ExcelPackage()
            Dim ws As ExcelWorksheet = CreateSheet(pck, FileName)

            'Create Header Column
            ws.Cells("A1").ValueBold(FileName)
            Dim rowIndex As Integer = 3
            Dim ColumnIndex As Integer = 1
            Dim lastColumn As Integer = 0
            ws.Cells(rowIndex, ColumnIndex).ValueBold("No")
            ws.Cells(rowIndex, ColumnIndex).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid
            ws.Cells(rowIndex, ColumnIndex).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray)

            For Each dColumn As DataColumn In Data.Columns
                ColumnIndex += 1
                ws.Cells(rowIndex, ColumnIndex).ValueBold(dColumn.ColumnName)
                ws.Cells(rowIndex, ColumnIndex).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid
                ws.Cells(rowIndex, ColumnIndex).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray)
            Next
            lastColumn = ColumnIndex

            'Create Data
            Dim noUrutan As Integer = 1
            For Each dRow As DataRow In Data.Rows
                rowIndex += 1
                ColumnIndex = 1
                ws.Cells(rowIndex, ColumnIndex).SetValue(noUrutan.ToString())
                For Each dColumn As DataColumn In Data.Columns
                    ColumnIndex += 1
                    ws.Cells(rowIndex, ColumnIndex).SetValue(dRow(dColumn.ColumnName).ToString())
                Next
                noUrutan += 1
            Next
            ws.Cells(3, 2, rowIndex, lastColumn).AutoFilter = True
            For colIdx As Integer = 1 To Data.Columns.Count + 1
                ws.Column(colIdx).AutoFit()
            Next
            CreateExcelFile(pck, FileName & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & ".xls")
        End Using
    End Sub

    Private Sub CreateExcel(ByVal FileName As String, ByVal Data As ArrayList)
        Dim oD As Dealer
        Dim LF As Char = Chr(10)
        Dim CR As Char = Chr(13)
        Using pck As New ExcelPackage()

            Dim ws As ExcelWorksheet = CreateSheet(pck, FileName)

            ws.Cells("A1").Value = FileName
            ws.Cells("A3").Value = "No"
            ws.Cells("B3").Value = "Kode Dealer"
            ws.Cells("C3").Value = "Assist Service Type"
            ws.Cells("D3").Value = "Model Kendaraan"
            ws.Cells("E3").Value = "Tipe Kendaraan"
            ws.Cells("F3").Value = "Jenis Kegiatan"
            ws.Cells("G3").Value = "Jenis Service"
            ws.Cells("H3").Value = "Standard Waktu Dealer (Jam)"
            ws.Cells("I3").Value = "Standard Waktu System (Jam)"
          
            'Dim standardCodeStatusClaimList As List(Of StandardCode) = New StandardCodeFacade(Me.User).RetrieveByCategory("ChassisMasterClaim.StatusClaim").Cast(Of  _
            '                                    StandardCode).ToList()
            'Dim standardCodeStatusProsesReturList As List(Of StandardCode) = New StandardCodeFacade(Me.User).RetrieveByCategory("ChassisMasterClaim.StatusProsesRetur").Cast(Of  _
            'StandardCode).ToList()
            Dim strJenisServis As String = ""
            Dim strAssist As String = ""
            Dim strJenisKegiatan As String = ""
            Dim idx As Integer = 0
            For i As Integer = 0 To Data.Count - 1
                Dim item As ServiceStandardTime = Data(i)

                ws.Cells(idx + 4, 1).Value = idx + 1
                ws.Cells(idx + 4, 2).Value = item.Dealer.DealerCode

                Dim criterias33 As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias33.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "ServiceBooking.StallServiceType"))
                criterias33.opAnd(New Criteria(GetType(StandardCode), "ValueCode", MatchType.Exact, item.AssistServiceTypeCode))
                Dim arrDDLs As ArrayList = New StandardCodeFacade(User).Retrieve(criterias33)
                Dim objStandardCodes As New StandardCode
                If Not IsNothing(arrDDLs) AndAlso arrDDLs.Count > 0 Then
                    objStandardCodes = CType(arrDDLs(0), StandardCode)
                    strAssist = objStandardCodes.ValueDesc
                End If

                ws.Cells(idx + 4, 3).Value = strAssist

                ws.Cells(idx + 4, 4).Value = item.VechileModel.IndDescription
                ws.Cells(idx + 4, 5).Value = item.VechileType.Description
                Dim criterias3 As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias3.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "ServiceBooking.ServiceType"))
                criterias3.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, item.ServiceTypeID))
                Dim arrDDL As ArrayList = New StandardCodeFacade(User).Retrieve(criterias3)
                Dim objStandardCode As New StandardCode
                If Not IsNothing(arrDDL) AndAlso arrDDL.Count > 0 Then
                    objStandardCode = CType(arrDDL(0), StandardCode)
                    strJenisKegiatan = objStandardCode.ValueDesc
                End If

                ws.Cells(idx + 4, 6).Value = strJenisKegiatan

                Select Case item.ServiceTypeID
                    Case 1
                        Dim criterias2 As New CriteriaComposite(New Criteria(GetType(FSKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias2.opAnd(New Criteria(GetType(FSKind), "KindCode", MatchType.Exact, item.KindCode))
                        Dim arrFSK2 As ArrayList = New FSKindFacade(User).Retrieve(criterias2)
                        Dim objFSKind As New FSKind
                        If Not IsNothing(arrFSK2) AndAlso arrFSK2.Count > 0 Then
                            objFSKind = CType(arrFSK2(0), FSKind)
                            strJenisServis = objFSKind.KindCode & " - " & objFSKind.KindDescription
                        End If

                    Case 2
                        Dim criterias0 As New CriteriaComposite(New Criteria(GetType(PMKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias0.opAnd(New Criteria(GetType(PMKind), "KindCode", MatchType.Exact, item.KindCode))
                        Dim arrFSK As ArrayList = New PMKindFacade(User).Retrieve(criterias0)
                        Dim objPMKind As New PMKind
                        If Not IsNothing(arrFSK) AndAlso arrFSK.Count > 0 Then
                            objPMKind = CType(arrFSK(0), PMKind)
                            strJenisServis = objPMKind.KindCode & " - " & objPMKind.KindDescription
                        End If
                    Case 3
                        Dim criteriasa As New CriteriaComposite(New Criteria(GetType(RecallCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criteriasa.opAnd(New Criteria(GetType(RecallCategory), "RecallRegNo", MatchType.Exact, item.KindCode))
                        Dim arrRC As ArrayList = New RecallCategoryFacade(User).Retrieve(criteriasa)
                        Dim objRecallCategory As New RecallCategory
                        If Not IsNothing(arrRC) AndAlso arrRC.Count > 0 Then
                            objRecallCategory = CType(arrRC(0), RecallCategory)
                            strJenisServis = objRecallCategory.RecallRegNo & " - " & objRecallCategory.Description
                        End If
                    Case 4
                        Dim criterias01 As New CriteriaComposite(New Criteria(GetType(GRKind), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias01.opAnd(New Criteria(GetType(GRKind), "KindCode", MatchType.Exact, item.KindCode))
                        Dim arrFSK As ArrayList = New GRKindFacade(User).Retrieve(criterias01)
                        Dim objPMKind As New GRKind
                        If Not IsNothing(arrFSK) AndAlso arrFSK.Count > 0 Then
                            objPMKind = CType(arrFSK(0), GRKind)
                            strJenisServis = objPMKind.KindCode & " - " & objPMKind.KindDescription
                        End If
                End Select

               
                ws.Cells(idx + 4, 7).Value = strJenisServis
                ws.Cells(idx + 4, 8).Value = item.DealerStandardTime 'Format(Double.Parse(item.DealerStandardTime), "0.00")
                ws.Cells(idx + 4, 9).Value = item.SystemStandardTime 'Format(Double.Parse(item.SystemStandardTime), "0.00")

                idx = idx + 1
            Next

            CreateExcelFile(pck, FileName & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second & Now.Millisecond & ".xls")
        End Using

    End Sub

    Private Function CreateSheet(pck As ExcelPackage, sheetName As String) As ExcelWorksheet
        Dim ws As ExcelWorksheet = pck.Workbook.Worksheets.Add(sheetName)
        ws.View.ShowGridLines = False
        Return ws
    End Function

    Private Sub CreateExcelFile(pck As ExcelPackage, fileName As String)
        Dim fileBytes = pck.GetAsByteArray()
        Response.Clear()

        Response.AppendHeader("Content-Length", fileBytes.Length.ToString())
        Response.AppendHeader("Content-Disposition", String.Format("attachment; filename=""{0}"";", fileName))
        'Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"  'xlsx
        Response.ContentType = "application/vnd.ms-excel" 'xls
        Response.BinaryWrite(fileBytes)
        Response.Flush()
        Response.[End]()

    End Sub
#End Region

    Protected Sub btnCalculateAll_Click(sender As Object, e As EventArgs)

    End Sub

    Protected Sub HiddenField1_ValueChanged(sender As Object, e As EventArgs)
        TextBox1.Text = HiddenField1.Value
    End Sub

    Protected Sub btnCalculate1_Click(sender As Object, e As EventArgs)
        Dim TotalBulan As Integer
        Dim M As Integer = Math.Abs((Now.Year - ICPeriodFrom.Value.Year))
        Dim months As Integer = ((M * 12) + Math.Abs((Now.Month - ICPeriodFrom.Value.Month)))
        If (ddlJenisKegiatan2.SelectedIndex <> 0) Then
            If (CInt(ICPeriodFrom.Value.Year <> 1)) Then
                TotalBulan = Math.Abs((Now.Month - ICPeriodFrom.Value.Month))
                If (months <= 18) Then
                    Calculate()
                    MessageBox.Show("Berhasil Calculate.")
                    ReadData()
                    dtgServiceStandardTime.CurrentPageIndex = 0
                    BindPage(dtgServiceStandardTime.CurrentPageIndex)
                Else
                    MessageBox.Show("Period From tidak boleh lebih dari 18 Bulan")
                End If
            Else
                MessageBox.Show("Pilih Period From yang akan dicalculate.")
            End If
        Else
            MessageBox.Show("Pilih Jenis Kegiatan yang akan dicalculate.")
        End If
        
    End Sub
    Private Sub Calculate()
        Dim objService As ServiceStandardTime = New ServiceStandardTime
        'objService = New ServiceStandardTimeFacade(User).Calculate(txtKodeDealer.Text.Trim(), "", ddlJenisKegiatan2.SelectedValue, ICPeriodFrom.Value)
        Dim RESULT As Integer = 0
        RESULT = New ServiceStandardTimeFacade(User).calculate2(txtKodeDealer.Text.Trim(), "", ddlJenisKegiatan2.SelectedValue, ICPeriodFrom.Value)
        ReadData()
        dtgServiceStandardTime.CurrentPageIndex = 0
        BindPage(dtgServiceStandardTime.CurrentPageIndex)
        pnlCalculate.Visible = False
        'objService = New ServiceStandardTimeFacade(User)
        'int _result = new int();

        'If (e.Cancelled) Then
        '    {
        '        _result = DPHFac.InsertDataLogForSendMail("Send canceled", token, 0);
        '    }

    End Sub

    Protected Sub btnBatal_Click1(sender As Object, e As EventArgs)

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        pnlCalculate.Visible = False
    End Sub

    Protected Sub btnCalculateAll_Click1(sender As Object, e As EventArgs)
        pnlCalculate.Visible = True
        ICPeriodFrom.Value = DateTime.Now.AddYears(-1)
    End Sub

    Protected Sub btnCalculate1_Click1(sender As Object, e As EventArgs)
        Dim TotalBulan As Integer
        Dim M As Integer = Math.Abs((Now.Year - ICPeriodFrom.Value.Year))
        Dim months As Integer = ((M * 12) + Math.Abs((Now.Month - ICPeriodFrom.Value.Month)))
        If (ddlJenisKegiatan2.SelectedIndex <> 0) Then
            If (CInt(ICPeriodFrom.Value.Year <> 1)) Then
                TotalBulan = Math.Abs((Now.Month - ICPeriodFrom.Value.Month))
                If (months <= 18) Then
                    Calculate()
                    MessageBox.Show("Berhasil Calculate.")
                    ReadData()
                    dtgServiceStandardTime.CurrentPageIndex = 0
                    BindPage(dtgServiceStandardTime.CurrentPageIndex)
                Else
                    MessageBox.Show("Period From tidak boleh lebih dari 18 Bulan")
                End If
            Else
                MessageBox.Show("Pilih Period From yang akan dicalculate.")
            End If
        Else
            MessageBox.Show("Pilih Jenis Kegiatan yang akan dicalculate.")
        End If
    End Sub
End Class