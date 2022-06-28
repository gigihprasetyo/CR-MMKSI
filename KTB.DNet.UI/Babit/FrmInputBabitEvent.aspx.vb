#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.IO
Imports System.Collections
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Security.Principal
Imports System.Data
Imports System.Linq
Imports System.Collections.Generic

#End Region

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports System.Net
Imports System.Security
Imports KTB.DNet.UI.Helper

#End Region

Public Class FrmInputBabitEvent
    Inherits System.Web.UI.Page

#Region "Custom Variables Declaration"

    Dim objDealer As New Dealer
    Dim objLoginUser As UserInfo
    Dim objBabitHeader As BabitHeader
    Private arlEvent As ArrayList = New ArrayList
    Private arlAlloc As ArrayList = New ArrayList
    Private arlDocument As ArrayList = New ArrayList
    Private arlDisplayAndTarget As ArrayList = New ArrayList

    Private arlDelEvent As ArrayList = New ArrayList
    Private arlDelDocument As ArrayList = New ArrayList
    Private arlDelDisplayAndTarget As ArrayList = New ArrayList

    Private MAX_FILE_SIZE As Integer = 5120000
    Private TempDirectory As String
    Private TargetDirectory As String
    Private sessHelper As New SessionHelper
    Private intBabitParameterHeaderID As Integer = 0
    Private intItemIndex As Integer = 0
    Private intBabitDisplayCarID As Integer = 0
    Private Mode As String = "New"
    Private strBabitType As String = ""

    Const sessBabitHdr As String = "FrmInputBabitEvent.sessDataBabitHdr"
    Const sessBabitDoc As String = "FrmInputBabitEvent.sessDataBabitDoc"
    Const sessBabitAct As String = "FrmInputBabitEvent.sessDataBabitAct"
    Const sessBabitEventDtl As String = "FrmInputBabitEvent.sessDataBabitEventDtl"
    Const sessBabitEventDisplayTarget As String = "FrmInputBabitEvent.sessBabitEventDisplayTrgt"
    Const sessBabitDealerAlloc As String = "FrmInputBabitEvent.sessBabitDealerAlloc"

    Const sessDeleteBabitDoc As String = "FrmInputBabitEvent.sessDeleteDataBabitDoc"
    Const sessDeleteBabitAct As String = "FrmInputBabitEvent.sessDeleteDataBabitAct"
    Const sessDeleteBabitEventDtl As String = "FrmInputBabitEvent.sessDeleteDataBabitEventDtl"
    Const sessDeleteBabitEventDisplayTarget As String = "FrmInputBabitEvent.sessDeleteBabitEventDisplayTrgt"
    Const sessDeleteBabitDealerAlloc As String = "FrmInputBabitEvent.sessDeleteBabitDealerAlloc"

    Private Property SesDealer() As Dealer
        Get
            Return CType(sessHelper.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)
            sessHelper.SetSession("DEALER", Value)
        End Set
    End Property
#End Region

#Region "Custom Methods"

    Private Sub LoadDataBabit(intBabitHeaderID As Integer)
        Dim objBabitEventDetail As BabitEventDetail
        Dim arrBabitEventDetail As ArrayList
        Dim arrBabitDocument As ArrayList

        objBabitHeader = New BabitHeaderFacade(User).Retrieve(intBabitHeaderID)
        If Not IsNothing(objBabitHeader) Then
            sessHelper.SetSession(sessBabitHdr, objBabitHeader)
            hdnBabitHeaderID.Value = objBabitHeader.ID
            objDealer = objBabitHeader.Dealer
            sessHelper.SetSession("FrmInputBabitEvent.DEALER", objDealer)
            BindMarbox(strBabitType)

            Me.lblBabitRegNumber.Text = objBabitHeader.BabitRegNumber
            Me.icPeriodStart.Value = objBabitHeader.PeriodStart
            'BindDDLAllocationBabit()

            Me.txtDealerCode.Text = objBabitHeader.Dealer.DealerCode
            Me.lblDealerCodeName.Text = objBabitHeader.Dealer.DealerCode & " / " & objBabitHeader.Dealer.DealerName
            If Not IsNothing(objBabitHeader.DealerBranch) Then
                Me.txtTOCode.Text = objBabitHeader.DealerBranch.DealerBranchCode
                Me.hdntxtTOCode.Value = objBabitHeader.DealerBranch.DealerBranchCode
                Me.lblTOCodeName.Text = objBabitHeader.DealerBranch.DealerBranchCode & " / " & objBabitHeader.DealerBranch.Name
                Me.lblTOName.Text = objBabitHeader.DealerBranch.Name
                Me.hdnlblTOName.Value = objBabitHeader.DealerBranch.Name
            End If
            If IsNothing(objBabitHeader.Dealer.Area2) Then
                Me.lblArea2CodeDesc.Text = ""
            Else
                Me.lblArea2CodeDesc.Text = objBabitHeader.Dealer.Area2.AreaCode & " / " & objBabitHeader.Dealer.Area2.Description
            End If
            'Me.lblArea2CodeDesc.Text = IIf(IsNothing(objBabitHeader.Dealer.Area2), "", objBabitHeader.Dealer.Area2.AreaCode & " / " & objBabitHeader.Dealer.Area2.Description)
            Me.txtNoSurat.Text = objBabitHeader.BabitDealerNumber
            Me.txtLocation.Text = objBabitHeader.Location
            Me.txtProspectTarget.Text = objBabitHeader.ProspectTarget
            Me.txtInvitationQty.Text = objBabitHeader.InvitationQty

            Me.icPeriodEnd.Value = objBabitHeader.PeriodEnd
            Me.ddlAlocBabitType.SelectedValue = objBabitHeader.AllocationType
            Me.ddlBabitMasterEventTypeID.SelectedValue = objBabitHeader.BabitMasterEventType.ID
            Me.txtNotes.Text = objBabitHeader.Notes
            Me.ddlMarBox.SelectedValue = objBabitHeader.MarboxID
            ddlMarBox_SelectedIndexChanged(Nothing, Nothing)

            Dim criterias4 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitSpecialCity), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias4.opAnd(New Criteria(GetType(BabitSpecialCity), "City.ID", MatchType.Exact, objBabitHeader.City.ID))
            criterias4.opAnd(New Criteria(GetType(BabitSpecialCity), "Status", MatchType.Exact, 1))
            Dim arlBabitSpecialCity As ArrayList = New BabitSpecialCityFacade(User).Retrieve(criterias4)
            If Not IsNothing(arlBabitSpecialCity) AndAlso arlBabitSpecialCity.Count > 0 Then
                ddlProvinsi.Items.Clear()
                ddlProvinsi.Items.Add(New ListItem("Silahkan Pilih", -1))

                For Each prov As BabitSpecialCity In arlBabitSpecialCity
                    ddlProvinsi.Items.Add(New ListItem(prov.BabitSpecialProvince.Name, prov.BabitSpecialProvince.ID))
                Next
                ddlProvinsi.SelectedValue = arlBabitSpecialCity(0).BabitSpecialProvince.ID
                BindddlCity(ddlProvinsi.SelectedValue, True)
            Else
                Dim criterias5 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Province), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim strSQL As String = "SELECT ProvinceID FROM City WHERE ID = " & objBabitHeader.City.ID
                criterias5.opAnd(New Criteria(GetType(Province), "ID", MatchType.InSet, "(" & strSQL & ")"))
                Dim arlProvince As ArrayList = New ProvinceFacade(User).Retrieve(criterias5)
                If Not IsNothing(arlProvince) AndAlso arlProvince.Count > 0 Then
                    ddlProvinsi.Items.Clear()
                    ddlProvinsi.Items.Add(New ListItem("Silahkan Pilih", -1))

                    For Each prov As Province In arlProvince
                        ddlProvinsi.Items.Add(New ListItem(prov.ProvinceName, prov.ID))
                    Next
                    ddlProvinsi.SelectedValue = arlProvince(0).ID
                    BindddlCity(ddlProvinsi.SelectedValue, False)
                End If
            End If
            Me.ddlKota.SelectedValue = objBabitHeader.City.ID

            'Me.ddlMarBox.SelectedValue = objBabitHeader.MarboxID
            'Me.ddlMarBox_SelectedIndexChanged(Nothing, Nothing)

            If Not IsLoginAsDealer() Then
                Dim dblBiaya As Double = 0
                Dim dblSubsidyAmount As Double = 0
                Dim _babitDealerAllocation As New BabitDealerAllocation
                Dim criterias6 As New CriteriaComposite(New Criteria(GetType(BabitDealerAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias6.opAnd(New Criteria(GetType(BabitDealerAllocation), "BabitHeader.ID", MatchType.Exact, objBabitHeader.ID))
                Dim arr As ArrayList = New BabitDealerAllocationFacade(User).Retrieve(criterias6)
                If Not IsNothing(arr) AndAlso arr.Count > 0 Then
                    _babitDealerAllocation = CType(arr(0), BabitDealerAllocation)
                    If Not IsNothing(_babitDealerAllocation) AndAlso _babitDealerAllocation.ID > 0 Then
                        dblSubsidyAmount = _babitDealerAllocation.SubsidyAmount
                        Dim criterias7 As New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias7.opAnd(New Criteria(GetType(Category), "ProductCategory.Code", MatchType.Exact, KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")))
                        criterias7.opAnd(New Criteria(GetType(Category), "CategoryCode", MatchType.Exact, _babitDealerAllocation.BabitCategory))
                        Dim arr1 As ArrayList = New CategoryFacade(User).Retrieve(criterias7)
                        If Not IsNothing(arr1) AndAlso arr1.Count > 0 Then
                            Me.ddlAllocationBabit.SelectedValue = CType(arr1(0), Category).CategoryCode
                        Else
                            Me.ddlAllocationBabit.SelectedValue = IIf(_babitDealerAllocation.BabitCategory = "", -1, _babitDealerAllocation.BabitCategory)
                        End If
                    End If
                Else
                    Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crits.opAnd(New Criteria(GetType(BabitEventDetail), "BabitHeader.ID", MatchType.Exact, objBabitHeader.ID))
                    Dim eventArr As ArrayList = New BabitEventDetailFacade(User).Retrieve(crits)
                    dblBiaya = 0
                    For Each bed As BabitEventDetail In eventArr
                        dblBiaya += CDbl(bed.Price) * CInt(bed.Qty)
                    Next
                    If dblBiaya > 0 Then
                        dblSubsidyAmount = (dblBiaya * 0.5)
                    End If
                End If
                Me.txtSubsidyAmount.Text = IIf(Format(dblSubsidyAmount, "###,###") = "", 0, Format(dblSubsidyAmount, "###,###"))
            End If

            Dim strDealerCodes As String = String.Empty
            If Not IsNothing(objBabitHeader.BabitDealerGroup) AndAlso objBabitHeader.BabitDealerGroup.Trim() <> "" Then
                Dim arrBabitDealerGroupID As String() = objBabitHeader.BabitDealerGroup.Split(";")
                For Each dealerID As String In arrBabitDealerGroupID
                    Dim oDealer As Dealer = New KTB.DNet.BusinessFacade.General.DealerFacade(User).Retrieve(CInt(dealerID))
                    If Not IsNothing(oDealer) AndAlso oDealer.ID > 0 Then
                        If strDealerCodes = String.Empty Then
                            strDealerCodes = oDealer.DealerCode
                        Else
                            strDealerCodes += ";" & oDealer.DealerCode
                        End If
                    End If
                Next
            End If
            Me.txtBabitDealerGroup.Text = strDealerCodes

            Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitEventDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitEventDetail), "BabitHeader.ID", MatchType.Exact, intBabitHeaderID))
            arrBabitEventDetail = New BabitEventDetailFacade(User).Retrieve(criterias)
            sessHelper.SetSession(sessBabitEventDtl, arrBabitEventDetail)
            BindGridBabitEvent()

            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(BabitDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(BabitDocument), "BabitHeader.ID", MatchType.Exact, intBabitHeaderID))
            arrBabitDocument = New BabitDocumentFacade(User).Retrieve(criterias2)
            sessHelper.SetSession(sessBabitDoc, arrBabitDocument)
            BindGridEventUploadFile()

            Dim criterias3 As New CriteriaComposite(New Criteria(GetType(BabitDisplayCar), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias3.opAnd(New Criteria(GetType(BabitDisplayCar), "BabitHeader.ID", MatchType.Exact, intBabitHeaderID))
            arlDisplayAndTarget = New BabitDisplayCarFacade(User).Retrieve(criterias3)
            sessHelper.SetSession(sessBabitEventDisplayTarget, arlDisplayAndTarget)
            BindGridDisplayAndTarget()

            If Not IsLoginAsDealer() Then
                Dim criterias5 As New CriteriaComposite(New Criteria(GetType(BabitDealerAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias5.opAnd(New Criteria(GetType(BabitDealerAllocation), "BabitHeader.ID", MatchType.Exact, intBabitHeaderID))
                arlAlloc = New BabitDealerAllocationFacade(User).Retrieve(criterias5)
                For Each objAlloc As BabitDealerAllocation In arlAlloc
                    objAlloc.SubsidyAmountBeforeEdit = objAlloc.SubsidyAmount
                    objAlloc.MaxSubsidyAmount = GetSubsidyRemaining(objAlloc.Dealer.ID.ToString())
                Next
                sessHelper.SetSession(sessBabitDealerAlloc, IIf(IsNothing(arlAlloc), New ArrayList, arlAlloc))
                BindGridAlloc()
            End If

            Me.btnSave.Enabled = False
            Me.lblPopUpDealer.Visible = False
            Me.lblPopUpTO.Visible = False
            Me.txtNoSurat.Enabled = False
            Me.icPeriodStart.Enabled = False
            Me.icPeriodEnd.Enabled = False
            Me.ddlAlocBabitType.Enabled = False
            Me.ddlBabitMasterEventTypeID.Enabled = False
            'Me.ddlMarBox.Enabled = False

            Me.txtLocation.Enabled = False
            Me.ddlKota.Enabled = False
            Me.ddlProvinsi.Enabled = False
            Me.txtProspectTarget.Enabled = False
            Me.txtInvitationQty.Enabled = False
            Me.ddlAllocationBabit.Enabled = False
            Me.ddlAllocationType.Enabled = False
            Me.txtSubsidyAmount.Enabled = False
            Me.txtNotes.Enabled = False
            Me.ddlMarBox.Enabled = False

            Me.dgUploadFile.ShowFooter = False
            Me.dgUploadFile.Columns(dgUploadFile.Columns.Count - 1).Visible = False
            Me.dgBabitEvent.ShowFooter = False
            Me.dgBabitEvent.Columns(dgBabitEvent.Columns.Count - 1).Visible = False
            Me.dgDisplayAndTarget.ShowFooter = False
            Me.dgDisplayAndTarget.Columns(dgDisplayAndTarget.Columns.Count - 1).Visible = False

            If Mode = "Edit" Then
                If SesDealer().Title = EnumDealerTittle.DealerTittle.KTB Then
                    Me.lblPopUpDealer.Visible = True
                Else
                    Me.lblPopUpDealer.Visible = False
                End If
                Me.lblPopUpTO.Visible = True
                Me.btnSave.Enabled = True
                Me.txtNoSurat.Enabled = True
                Me.icPeriodStart.Enabled = True
                Me.icPeriodEnd.Enabled = True
                Me.ddlAlocBabitType.Enabled = True
                Me.ddlBabitMasterEventTypeID.Enabled = True

                Me.txtLocation.Enabled = True
                Me.ddlKota.Enabled = True
                Me.ddlProvinsi.Enabled = True
                Me.txtProspectTarget.Enabled = True
                Me.txtInvitationQty.Enabled = True
                Me.ddlAllocationBabit.Enabled = True
                Me.ddlAllocationType.Enabled = True
                Me.txtSubsidyAmount.Enabled = True
                Me.txtNotes.Enabled = True
                Me.ddlMarBox.Enabled = True

                Me.dgUploadFile.ShowFooter = True
                Me.dgUploadFile.Columns(dgUploadFile.Columns.Count - 1).Visible = True
                Me.dgBabitEvent.ShowFooter = True
                Me.dgBabitEvent.Columns(dgBabitEvent.Columns.Count - 1).Visible = True
                Me.dgDisplayAndTarget.ShowFooter = True
                Me.dgDisplayAndTarget.Columns(dgDisplayAndTarget.Columns.Count - 1).Visible = True
            End If
        End If
    End Sub

    Private Function GetSubsidyRemaining(ByVal strDealerID As String) As Double
        Dim dblRemains As Double = 0
        Dim arrView As ArrayList = New V_BabitMasterRetailTargetFacade(User).RetrieveAllocationBudgetByDealer(strDealerID, objBabitHeader.PeriodStart)
        If Not IsNothing(arrView) AndAlso arrView.Count > 0 Then
            For Each objV_BabitMasterRetailTarget As V_BabitMasterRetailTarget In arrView
                dblRemains += (objV_BabitMasterRetailTarget.TotalAllocationBabit - objV_BabitMasterRetailTarget.SumSubsidyAmount)
            Next
        End If
        Return dblRemains
    End Function

    Private Sub BindddlProvince()
        ddlKota.Items.Clear()
        ddlKota.Items.Add(New ListItem("Silahkan Pilih", -1))
        ddlProvinsi.Items.Clear()
        ddlProvinsi.Items.Add(New ListItem("Silahkan Pilih", -1))

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitSpecialCity), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BabitSpecialCity), "City.ID", MatchType.Exact, objLoginUser.Dealer.City.ID))
        criterias.opAnd(New Criteria(GetType(BabitSpecialCity), "Status", MatchType.Exact, 1))
        Dim arlBabitSpecialCity As ArrayList = New BabitSpecialCityFacade(User).Retrieve(criterias)
        If Not IsNothing(arlBabitSpecialCity) AndAlso arlBabitSpecialCity.Count > 0 Then
            For Each prov As BabitSpecialCity In arlBabitSpecialCity
                ddlProvinsi.Items.Add(New ListItem(prov.BabitSpecialProvince.Name, prov.BabitSpecialProvince.ID))
            Next
            If arlBabitSpecialCity.Count = 1 Then
                ddlProvinsi.SelectedValue = arlBabitSpecialCity(0).BabitSpecialProvince.ID
                BindddlCity(ddlProvinsi.SelectedValue, True)
            End If
        Else
            Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Province), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Dim strSQL As String = "SELECT ProvinceID FROM City WHERE ID = " & objLoginUser.Dealer.City.ID
            criterias2.opAnd(New Criteria(GetType(Province), "ID", MatchType.InSet, "(" & strSQL & ")"))
            Dim arlProvince As ArrayList = New ProvinceFacade(User).Retrieve(criterias2)
            If Not IsNothing(arlProvince) AndAlso arlProvince.Count > 0 Then
                For Each prov As Province In arlProvince
                    ddlProvinsi.Items.Add(New ListItem(prov.ProvinceName, prov.ID))
                Next
                If arlProvince.Count = 1 Then
                    ddlProvinsi.SelectedValue = arlProvince(0).ID()
                    BindddlCity(ddlProvinsi.SelectedValue, False)
                End If
            End If
        End If

    End Sub

    Private Sub BindddlCity(ProvinceID As Integer, _isSpecial As Boolean)
        ddlKota.Items.Clear()
        ddlKota.Items.Add(New ListItem("Silahkan Pilih", -1))

        If _isSpecial Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitSpecialCity), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitSpecialCity), "BabitSpecialProvince.ID", MatchType.Exact, ProvinceID))
            criterias.opAnd(New Criteria(GetType(BabitSpecialCity), "Status", MatchType.Exact, 1))
            Dim arlBabitSpecialCity As ArrayList = New BabitSpecialCityFacade(User).Retrieve(criterias)
            If Not IsNothing(arlBabitSpecialCity) AndAlso arlBabitSpecialCity.Count > 0 Then
                For Each c As BabitSpecialCity In arlBabitSpecialCity
                    ddlKota.Items.Add(New ListItem(c.City.CityName, c.City.ID))
                Next
            End If
        Else
            Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(City), "Province.ID", MatchType.Exact, ProvinceID))
            Dim arlCity As ArrayList = New CityFacade(User).Retrieve(criterias2)
            If Not IsNothing(arlCity) AndAlso arlCity.Count > 0 Then
                For Each c As City In arlCity
                    ddlKota.Items.Add(New ListItem(c.CityName, c.ID))
                Next
            End If
        End If
    End Sub

    Private Function ValidateBabitDealerGroup(ByVal _dealers As String) As Boolean
        Dim bcheck As Boolean = True
        Dim i As Integer
        Dim items() As String = _dealers.Split(";")
        For i = 0 To items.Length - 1
            Dim objDealerTmp As Dealer = New DealerFacade(User).Retrieve(items(i))
            If objDealerTmp.ID = 0 Then
                MessageBox.Show("Dealer " + items(i) + " tidak valid")
                bcheck = False
                Exit For
            End If
        Next
        Dim strDealerDuplication As String = ValidateDealerDuplication(_dealers)
        If strDealerDuplication <> String.Empty Then
            MessageBox.Show("Duplikasi Dealer " + strDealerDuplication)
            bcheck = False
        End If
        Return bcheck
    End Function

    Private Function ValidateDealerDuplication(ByVal _dealers As String) As String
        Dim bcheck As Boolean = True
        Dim _dealerDuplicate As String = String.Empty
        Dim i As Integer
        Dim j As Integer
        Dim list() As String = _dealers.Split(";")
        For i = 0 To list.Length - 2
            For j = i + 1 To list.Length - 1
                If list(i) = list(j) Then
                    bcheck = False
                    Exit For
                End If
            Next
            If bcheck = False Then
                _dealerDuplicate = list(i)
                Exit For
            End If
        Next
        Return _dealerDuplicate
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

    Private Sub ClearAll()
        hdnBabitHeaderID.Value = ""
        hdnIndexSelectedGrid.Value = ""

        lblBabitRegNumber.Text = "Auto Generated"
        txtTOCode.Text = ""
        hdntxtTOCode.Value = ""
        lblTOCodeName.Text = ""
        lblTOName.Text = ""
        hdnlblTOName.Value = ""
        lblArea2CodeDesc.Text = ""
        txtNoSurat.Text = ""
        txtLocation.Text = ""
        txtProspectTarget.Text = ""
        txtInvitationQty.Text = ""
        ddlAlocBabitType.SelectedIndex = 0
        ddlBabitMasterEventTypeID.SelectedIndex = 0

        'ddlAllocationBabit.SelectedIndex = 0
        'ddlAllocationType.SelectedIndex = 0
        'txtSubsidyAmount.Text = 0
        txtNotes.Text = ""

        icPeriodStart.Value = Date.Now
        icPeriodEnd.Value = Date.Now
        sessHelper.SetSession(sessBabitEventDtl, New ArrayList)
        sessHelper.SetSession(sessBabitDoc, New ArrayList)
        sessHelper.SetSession(sessBabitEventDisplayTarget, New ArrayList)
        sessHelper.SetSession(sessBabitDealerAlloc, New ArrayList)

        sessHelper.SetSession(sessDeleteBabitEventDtl, New ArrayList)
        sessHelper.SetSession(sessDeleteBabitDoc, New ArrayList)
        sessHelper.SetSession(sessDeleteBabitEventDisplayTarget, New ArrayList)
        sessHelper.SetSession(sessDeleteBabitDealerAlloc, New ArrayList)

        BindGridDisplayAndTarget()
        BindGridBabitEvent()
        BindGridEventUploadFile()
        BindGridAlloc()
    End Sub

    Private Function GetRegNumber() As String
        'Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        Dim YearParameter As Integer = Date.Today.Year
        If Date.Today.Month = 12 Then
            YearParameter = Date.Today.Year + 1
        End If
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitHeader), "BabitRegNumber", MatchType.StartsWith, "E"))
        crit.opAnd(New Criteria(GetType(BabitHeader), "CreatedTime", MatchType.GreaterOrEqual, Date.Today.Year & Date.Today.Month.ToString("d2") & "01"))
        crit.opAnd(New Criteria(GetType(BabitHeader), "CreatedTime", MatchType.Lesser, YearParameter & Date.Today.AddMonths(1).Month.ToString("d2") & "01"))
        Dim arrl As ArrayList = New BabitHeaderFacade(User).Retrieve(crit)
        Dim _return As String
        If arrl.Count > 0 Then
            'Dim objBH As BabitHeader = CommonFunction.SortListControl(arrl, "ID", Sort.SortDirection.DESC)(0)
            Dim objBH As BabitHeader = CommonFunction.SortListControl(arrl, "BabitRegNumber", Sort.SortDirection.DESC)(0)
            Dim noReg As String = objBH.BabitRegNumber
            _return = "E" & Date.Today.Month.ToString("d2") & Date.Today.ToString("yy") & (CInt(noReg.Substring(5, 5)) + 1).ToString("d5")
        Else
            _return = "E" & Date.Today.Month.ToString("d2") & Date.Today.ToString("yy") & CInt(1).ToString("d5")
        End If
        Return _return
    End Function

    Private Function GetTotalPriceByCategory(ByVal intBabitParameterHeaderID As Integer) As Double
        Dim dblSumTotalPrice As Double = 0
        dblSumTotalPrice = (From item As BabitEventDetail In arlEvent
                            Where item.BabitParameterDetail.BabitParameterHeader.ID = intBabitParameterHeaderID And item.Item <> "Total Biaya :"
                                Select (item.Qty * item.Price)).Sum()
        Return dblSumTotalPrice
    End Function

    Private Function TimeStamp() As String
        Return DateTime.Now.Year & DateTime.Now.Month & DateTime.Now.Day & DateTime.Now.Hour & DateTime.Now.Minute & DateTime.Now.Second & DateTime.Now.Millisecond
    End Function

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
                For Each obj As BabitDocument In AttachmentCollection
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

    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function

    Private Function GetFromSession(ByVal sObject As String) As Object
        If Session("DEALER") Is Nothing Then
            Response.Redirect("../SessionExpired.htm")
        Else
            Return Session(sObject)
        End If
    End Function

    Sub GetDealerData(ByVal oDealer As Dealer)
        txtDealerCode.Visible = False
        lblPopUpDealer.Visible = False
        lblDealerCodeName.Visible = True
        lblArea2CodeDesc.Visible = True
        lblDealerCodeName.Text = oDealer.DealerCode & " / " & oDealer.DealerName
        lblArea2CodeDesc.Text = IIf(IsNothing(oDealer.Area2), "", oDealer.Area2.AreaCode & " / " & oDealer.Area2.Description)
        lblPopUpDealer.Attributes("onClick") = "ShowPPDealerSelection()"

        If Not IsLoginAsDealer() Then
            If Mode = "Detail" Then
                'Me.ddlMarBox.Enabled = False
                Me.lnkReload.Visible = False
            ElseIf Mode = "Edit" Then
                'Me.ddlMarBox.Enabled = True
                Me.lnkReload.Visible = True
            End If
        End If
    End Sub

    Sub BindAllocBabitType()
        ddlAlocBabitType.Items.Clear()
        ddlAlocBabitType.DataSource = New StandardCodeFacade(User).RetrieveByCategory("Babit.EnumAllocationType")
        ddlAlocBabitType.DataTextField = "ValueDesc"
        ddlAlocBabitType.DataValueField = "ValueId"
        ddlAlocBabitType.DataBind()
        ddlAlocBabitType.Items.Insert(0, New ListItem("Silakan Piih ", -1))
        ddlAlocBabitType.SelectedIndex = 0
    End Sub

    Sub BindBabitMasterEventType()
        With ddlBabitMasterEventTypeID
            .Items.Clear()
            Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitMasterEventType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitMasterEventType), "Status", MatchType.Exact, 1))
            criterias.opAnd(New Criteria(GetType(BabitMasterEventType), "FormType", MatchType.Exact, 1))
            Dim arrayDDl As ArrayList = New BabitMasterEventTypeFacade(User).Retrieve(criterias)

            .DataSource = arrayDDl
            .DataTextField = "TypeName"
            .DataValueField = "ID"
            .DataBind()
            .Items.Insert(0, New ListItem("Silakan Piih ", -1))
            .SelectedIndex = 0
        End With
    End Sub

    Sub BindGridEventUploadFile()
        arlDocument = CType(sessHelper.GetSession(sessBabitDoc), ArrayList)
        If IsNothing(arlDocument) Then arlDocument = New ArrayList()
        dgUploadFile.DataSource = arlDocument
        dgUploadFile.DataBind()
    End Sub

    Private Sub BindGridDisplayAndTarget()
        arlDisplayAndTarget = CType(sessHelper.GetSession(sessBabitEventDisplayTarget), ArrayList)
        If IsNothing(arlDisplayAndTarget) Then arlDisplayAndTarget = New ArrayList()
        dgDisplayAndTarget.DataSource = arlDisplayAndTarget
        dgDisplayAndTarget.DataBind()
        sessHelper.SetSession(sessBabitEventDisplayTarget, arlDisplayAndTarget)
    End Sub

    Private Sub BindGridAlloc()
        If Not IsNothing(CType(sessHelper.GetSession(sessBabitDealerAlloc), ArrayList)) Then
            arlAlloc = CType(sessHelper.GetSession(sessBabitDealerAlloc), ArrayList)
        Else
            arlAlloc = New ArrayList
        End If
        dgAlloc.DataSource = arlAlloc
        dgAlloc.DataBind()
    End Sub

    Sub BindGridBabitEvent()
        arlEvent = CType(sessHelper.GetSession(sessBabitEventDtl), ArrayList)
        If IsNothing(arlEvent) Then
            arlEvent = GetArrayGridEvent(hdnBabitHeaderID.Value)
        End If

        Dim dataList As ArrayList = New ArrayList
        dataList = New System.Collections.ArrayList(
                        (From obj As BabitEventDetail In arlEvent.OfType(Of BabitEventDetail)()
                            Where obj.Item <> "Total Biaya :"
                            Select obj).ToList())

        CommonFunction.SortListControl(dataList, "BabitParameterDetail.BabitParameterHeader.ID", Sort.SortDirection.ASC)

        Dim objBabitParamDtl As New BabitParameterDetail
        Dim oBabitEventDetail As New BabitEventDetail
        Dim intBabitParameterHeaderID As Integer = 0

        Dim arlEvent2 As ArrayList = New ArrayList
        For i As Integer = 0 To dataList.Count - 1
            Dim objBabitEvent As BabitEventDetail = CType(dataList(i), BabitEventDetail)
            If i = 0 Then
                intBabitParameterHeaderID = objBabitEvent.BabitParameterDetail.BabitParameterHeader.ID
            End If
            If intBabitParameterHeaderID <> objBabitEvent.BabitParameterDetail.BabitParameterHeader.ID Then
                oBabitEventDetail = New BabitEventDetail
                oBabitEventDetail.BabitHeader = New BabitHeader

                objBabitParamDtl = New BabitParameterDetail
                Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitParameterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(BabitParameterDetail), "BabitParameterHeader.ID", MatchType.Exact, intBabitParameterHeaderID))
                Dim array As ArrayList = New BabitParameterDetailFacade(User).Retrieve(criterias)
                If Not IsNothing(array) AndAlso array.Count > 0 Then
                    objBabitParamDtl = CType(array(0), BabitParameterDetail)
                End If
                oBabitEventDetail.BabitParameterDetail = objBabitParamDtl
                oBabitEventDetail.Item = "Total Biaya :"
                oBabitEventDetail.TotalPrice = GetTotalPriceByCategory(intBabitParameterHeaderID)
                arlEvent2.Add(oBabitEventDetail)
                intBabitParameterHeaderID = objBabitEvent.BabitParameterDetail.BabitParameterHeader.ID
            End If

            arlEvent2.Add(objBabitEvent)
            If i = dataList.Count - 1 Then
                oBabitEventDetail = New BabitEventDetail
                oBabitEventDetail.BabitHeader = New BabitHeader

                objBabitParamDtl = New BabitParameterDetail
                Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitParameterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(BabitParameterDetail), "BabitParameterHeader.ID", MatchType.Exact, intBabitParameterHeaderID))
                Dim array As ArrayList = New BabitParameterDetailFacade(User).Retrieve(criterias)
                If Not IsNothing(array) AndAlso array.Count > 0 Then
                    objBabitParamDtl = CType(array(0), BabitParameterDetail)
                End If
                oBabitEventDetail.BabitParameterDetail = objBabitParamDtl
                oBabitEventDetail.Item = "Total Biaya :"
                oBabitEventDetail.TotalPrice = GetTotalPriceByCategory(intBabitParameterHeaderID)
                arlEvent2.Add(oBabitEventDetail)
            End If
        Next
        CommonFunction.SortListControl(arlEvent2, "BabitParameterDetail.BabitParameterHeader.ID", Sort.SortDirection.ASC)

        dgBabitEvent.DataSource = arlEvent2
        dgBabitEvent.DataBind()
        sessHelper.SetSession(sessBabitEventDtl, arlEvent2)
    End Sub

    Private Function GetArrayGridEvent(ByVal _babitHeaderID As Integer) As ArrayList
        Dim arr As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitEventDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BabitEventDetail), "BabitHeader.ID", MatchType.Exact, _babitHeaderID))
        arr = New BabitEventDetailFacade(User).Retrieve(criterias)
        If IsNothing(arr) Then arr = New ArrayList
        Return arr
    End Function

    Private Function ValidateIsMandatoryParamBabitEvent(ByRef strParamName As String) As String
        strParamName = String.Empty
        Dim dataList As ArrayList = New ArrayList
        arlEvent = CType(sessHelper.GetSession(sessBabitEventDtl), ArrayList)

        Dim isMandatory As Boolean = False
        Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitParameterHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "BabitMasterEventType.FormType", MatchType.Exact, 1))
        criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "IsMandatory", MatchType.Exact, 1))
        criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "Status", MatchType.Exact, 1))
        criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "BabitMasterEventType.ID", MatchType.Exact, ddlBabitMasterEventTypeID.SelectedValue))

        Dim arrBabitParamHdr As ArrayList = New BabitParameterHeaderFacade(User).Retrieve(criterias)
        If Not IsNothing(arrBabitParamHdr) AndAlso arrBabitParamHdr.Count > 0 Then
            For Each objParam As BabitParameterHeader In arrBabitParamHdr
                dataList = New ArrayList(
                                (From obj As BabitEventDetail In arlEvent.OfType(Of BabitEventDetail)()
                                    Where obj.BabitParameterDetail.BabitParameterHeader.ID = objParam.ID And obj.Item.Trim <> "Total Biaya :"
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

    Private Function ValidateData() As String
        Dim sb As StringBuilder = New StringBuilder

        If Not IsLoginAsDealer() Then
            If (txtDealerCode.Text = String.Empty) Then
                sb.Append("- Kode Dealer Harus Diisi\n")
            End If
        End If

        'If (txtTOCode.Text.Trim = String.Empty) Then
        '    sb.Append("- Kode Temporary Outlet Harus Diisi\n")
        'End If

        If (txtNoSurat.Text.Trim = String.Empty) Then
            sb.Append("- No Surat Harus Diisi\n")
        End If
        If (txtLocation.Text.Trim = String.Empty) Then
            sb.Append("- Lokasi Event Harus Diisi\n")
        End If
        If (ddlProvinsi.SelectedValue.Trim = String.Empty) Then
            sb.Append("- Provinsi Harus Diisi\n")
        End If
        If (ddlKota.SelectedValue.Trim = String.Empty) Then
            sb.Append("- Kota Harus Diisi\n")
        End If
        If (txtProspectTarget.Text.Trim = String.Empty OrElse txtProspectTarget.Text.Trim = "0") Then
            sb.Append("- Target Prospek Harus Diisi\n")
        End If
        If (txtInvitationQty.Text.Trim = String.Empty OrElse txtInvitationQty.Text.Trim = "0") Then
            sb.Append("- Jumlah Undangan Harus Diisi\n")
        End If
        If (icPeriodStart.Value > icPeriodEnd.Value) Then
            sb.Append("- Tanggal awal harus lebih kecil atau sama dengan tanggal akhir\n")
        End If
        If ddlBabitMasterEventTypeID.SelectedValue = -1 Then
            sb.Append("- Kategori Kegiatan Harus Diisi\n")
        End If
        If (sessHelper.GetSession(sessBabitEventDtl) Is Nothing) Then
            sb.Append("- Data Babit Event Detail belum ada\n")
        Else
            If CType(sessHelper.GetSession(sessBabitEventDtl), ArrayList).Count = 0 Then
                sb.Append("- Data Babit Event Detail belum ada\n")
            End If
        End If

        If (sessHelper.GetSession(sessBabitEventDisplayTarget) Is Nothing) Then
            sb.Append("- Data Display dan Target Penjualan belum ada\n")
        Else
            If CType(sessHelper.GetSession(sessBabitEventDisplayTarget), ArrayList).Count = 0 Then
                sb.Append("- Data Display dan Target Penjualan belum ada\n")
            End If
        End If
        If (sessHelper.GetSession(sessBabitDoc) Is Nothing) Then
            sb.Append("- Dokumen Pendukung belum diupload\n")
        Else
            If CType(sessHelper.GetSession(sessBabitDoc), ArrayList).Count = 0 Then
                sb.Append("- Dokumen Pendukung belum diupload\n")
            End If
        End If

        Dim strParamName As String = String.Empty
        If ValidateIsMandatoryParamBabitEvent(strParamName) <> String.Empty Then
            sb.Append("- Biaya: " & strParamName & " belum diinputkan! \n")
        End If

        If ddlAlocBabitType.SelectedIndex = 0 Then
            ddlAlocBabitType.SelectedValue = 0
        End If
        If Me.txtSubsidyAmount.Text.Trim = "" Then Me.txtSubsidyAmount.Text = 0

        '--Sementara di buang mandatorynya
        'If ddlMarBox.SelectedIndex = 0 Then
        '    sb.Append("- Marbox harus dipilih! \n")
        'End If

        If ddlMarBox.SelectedValue = "-1" Then
            sb.Append("- Marbox harus dipilih! \n")
        End If

        If ddlMarBox.SelectedValue = "" Then
            sb.Append("- Data Marbox harus dibuat! \n")
        End If

        If IsLoginAsDealer() Then
            Dim dteGetDate As Date = Now.ToShortDateString()
            Dim dtePeriodStart As Date = icPeriodStart.Value
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
                    sb.Append("- Pengajuan proposal hanya boleh dilakukan selambat-lambatnya " & limitWorkDays.ToString & " hari kerja sebelum kegiatan.\n")
                End If
            End If
        End If

        If Mode = "Edit" Then
            If Not IsLoginAsDealer() Then
                If div_Alokasi_Babit.Visible = True Then
                    Dim arrAlloc As ArrayList = CType(sessHelper.GetSession(sessBabitDealerAlloc), ArrayList)
                    If Not IsNothing(arrAlloc) Then
                        If arrAlloc.Count = 0 Then
                            sb.Append("- Alokasi Babit harus Diisi.\n")
                        Else
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

                    'If ddlAllocationBabit.SelectedIndex = 0 Then
                    '    sb.Append("- Alokasi Babit harus Diisi.\n")
                    'End If
                End If
                'If TR_Jml_Subsidi.Visible = True Then
                '    If txtSubsidyAmount.Text.Trim = 0 OrElse txtSubsidyAmount.Text.Trim = "" Then
                '        sb.Append("- Jumlah Subsidi harus Diisi.\n")
                '    End If
                'End If

                'Dim strSQL As String = String.Empty
                'strSQL = "select distinct a.ID "
                'strSQL += "from BabitBudgetHeader a "
                'strSQL += "where a.RowStatus = 0 "
                'strSQL += "and a.DealerID Is null "
                'strSQL += "and a.CategoryID is null "
                'strSQL += "and a.YearPeriod = year('" & icPeriodStart.Value.ToString("yyyy/MM/dd") & "')  "
                'strSQL += "and month('" & icPeriodStart.Value.ToString("yyyy/MM/dd") & "') IN ("
                'strSQL += "Select value "
                'strSQL += "from funcListToTableInt("
                'strSQL += "case when a.QuarterPeriod = 1 then '04,05,06' "
                'strSQL += "when a.QuarterPeriod = 2 then '07,08,09' "
                'strSQL += "when a.QuarterPeriod = 3 then '10,11,12' "
                'strSQL += "when a.QuarterPeriod = 4 then '01,02,03' "
                'strSQL += "else '' end,','))  "
                'Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitBudgetHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'criterias.opAnd(New Criteria(GetType(BabitBudgetHeader), "ID", MatchType.InSet, "(" & strSQL & ")"))
                'criterias.opAnd(New Criteria(GetType(BabitBudgetHeader), "Description", MatchType.Partial, ddlAllocationBabit.SelectedValue.Trim))
                'Dim arrBBH As ArrayList = New BabitBudgetHeaderFacade(User).Retrieve(criterias)
                'If Not IsNothing(arrBBH) AndAlso arrBBH.Count <= 0 Then
                '    Dim arrView As ArrayList = New V_BabitMasterRetailTargetFacade(User).RetrieveAllocationBudget(ddlAllocationBabit.SelectedValue, lblBabitRegNumber.Text.Trim)
                '    If Not IsNothing(arrView) AndAlso arrView.Count > 0 Then
                '        Dim objV_BabitMasterRetailTarget As V_BabitMasterRetailTarget = CType(arrView(0), V_BabitMasterRetailTarget)
                '        Dim dblRemains As Double = objV_BabitMasterRetailTarget.TotalAllocationBabit - objV_BabitMasterRetailTarget.SumSubsidyAmount
                '        Dim dblSubsidyAmount As Double = txtSubsidyAmount.Text
                '        If dblRemains < dblSubsidyAmount Then
                '            sb.Append("- Jumlah Subsidi Tidak Boleh Melebihi Sisa Alokasi Babit\n")
                '        End If
                '    Else
                '        sb.Append("- Data Alokasi Babit: " & ddlAllocationBabit.SelectedItem.Text & " tidak tersedia.\n")
                '    End If
                'End If
            End If
        End If

        Return sb.ToString()
    End Function

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

    Private Function FileIsExist(ByVal FileName As String, ByVal AttachmentCollection As ArrayList) As Boolean
        Dim bResult As Boolean = False
        If AttachmentCollection.Count > 0 Then
            For Each obj As BabitDocument In AttachmentCollection
                If Not IsNothing(obj.AttachmentData) Then
                    If Path.GetFileName(obj.AttachmentData.FileName) = FileName Then
                        bResult = True
                        Exit For
                    End If
                End If
            Next
        End If
        Return bResult
    End Function

    Private Sub BindDDLAllocationType()
        With ddlAllocationType
            .Items.Clear()
            .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            .Items.Insert(1, New ListItem("Reguler", 0))
            .Items.Insert(2, New ListItem("Tambahan", 1))
        End With
        ddlAllocationType.SelectedIndex = 0
    End Sub

    Private Sub BindDDLAllocationBabit_Old2()
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

            Dim arrDDL As ArrayList = New BabitBudgetHeaderFacade(User).GetDataAllocationByDealer(_dealerID, icPeriodStart.Value)
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

    Private Sub BindDDLAllocationBabit_old()
        Dim strAllocationBabitValue As String = String.Empty
        Dim strAllocationBabitDesc As String = String.Empty
        With ddlAllocationBabit
            .Items.Clear()
            .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            Dim arrDDL As ArrayList = New BabitBudgetHeaderFacade(User).GetDataAllocation(lblBabitRegNumber.Text, icPeriodStart.Value)
            Dim i% = 1
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
                .Items.Insert(i, New ListItem("BABIT " & strAllocationBabitDesc, strAllocationBabitValue))
                i += 1
            Next
        End With
        ddlAllocationBabit.SelectedIndex = 0
    End Sub

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If SesDealer().Title = EnumDealerTittle.DealerTittle.KTB Then
            'If Not SecurityProvider.Authorize(Context.User, SR.Babit_Input_Event_Privilege) Then
            '    Server.Transfer("../FrmAccessDenied.aspx?modulName=BABIT - INPUT BABIT EVENT")
            'End If
        Else
            ' case from dealer
            If Not SecurityProvider.Authorize(Context.User, SR.Babit_Input_Event_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=BABIT - INPUT BABIT EVENT")
            End If
        End If

    End Sub
#End Region

#Region "Event Handlers"

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Not IsNothing(Request.QueryString("Mode")) Then
            Mode = Request.QueryString("Mode")
            btnBack.Visible = True
        End If
        If Mode = "New" Then  ' Login as Dealer
            objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        Else
            objDealer = CType(sessHelper.GetSession("FrmInputBabitEvent.DEALER"), Dealer)
            If IsNothing(objDealer) Then
                objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
            End If
            objBabitHeader = CType(sessHelper.GetSession(sessBabitHdr), BabitHeader)
            If IsNothing(objBabitHeader) Then
                objBabitHeader = CType(sessHelper.GetSession(sessBabitHdr), BabitHeader)
            End If
        End If
        objLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        TargetDirectory = KTB.DNet.Lib.WebConfig.GetValue("SAN")
        TempDirectory = Server.MapPath("") + "\..\DataTemp\"
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If (Not IsPostBack) Then
            'LoadMarBox()
            strBabitType = New AppConfigFacade(User).Retrieve("BabitCognitoSharePoint.BabitType.Event").Value
            BindMarbox(strBabitType)
            InitiateAuthorization()

            BindBabitMasterEventType()
            BindAllocBabitType()
            BindGridEventUploadFile()
            BindGridBabitEvent()
            BindGridDisplayAndTarget()

            BindddlProvince()
            BindDDLAllocationType()
            'BindDDLAllocationBabit()

            lblPopUpDealer.Attributes("onclick") = "ShowPPDealerSelection();"
            lblPopUpTO.Attributes("onclick") = "ShowPPDealerBranchSelection();"

            objBabitHeader = New BabitHeader
            If Not IsNothing(Request.QueryString("BabitHeaderID")) Then
                hdnBabitHeaderID.Value = Request.QueryString("BabitHeaderID")
                LoadDataBabit(hdnBabitHeaderID.Value)
            End If
            Dim strDealerGroupID As String = ""
            If IsDealer() Then
                strDealerGroupID = CStr(objLoginUser.Dealer.DealerGroup.ID)
            Else
                If Not IsNothing(objBabitHeader) AndAlso objBabitHeader.ID > 0 Then
                    strDealerGroupID = objBabitHeader.Dealer.DealerGroup.ID
                End If
            End If
            lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelectionGab('" & strDealerGroupID & "','" & CType(GetFromSession("DEALER"), Dealer).DealerCode & "');"

            GetDealerData(objDealer)
            BindGridAlloc()

            If SesDealer().Title = EnumDealerTittle.DealerTittle.KTB Then
                If Mode = "Edit" Then
                    If objBabitHeader.BabitStatus >= 2 Then  '--> status Konfirmasi
                        dgAlloc.ShowFooter = True
                        dgAlloc.Columns(dgAlloc.Columns.Count - 1).Visible = True
                        'ddlAllocationBabit.Enabled = True
                        'txtSubsidyAmount.Enabled = True
                    Else
                        dgAlloc.ShowFooter = False
                        dgAlloc.Columns(dgAlloc.Columns.Count - 1).Visible = False
                        'ddlAllocationBabit.Enabled = False
                        'txtSubsidyAmount.Enabled = False
                    End If
                    txtNotes.Enabled = True
                End If
            Else
                dgAlloc.ShowFooter = False
                dgAlloc.Columns(dgAlloc.Columns.Count - 1).Visible = False
                'ddlAllocationBabit.Enabled = False
                'txtSubsidyAmount.Enabled = False
                txtNotes.Enabled = False
                If Mode = "New" Then
                    div_Alokasi_Babit.Visible = False
                    'TR_Jml_Subsidi.Visible = False
                    TR_CatatanMKS.Visible = False
                Else
                    div_Alokasi_Babit.Visible = True
                    'TR_Jml_Subsidi.Visible = True
                    TR_CatatanMKS.Visible = True
                End If
            End If

            'UsedMarbox()
        End If
    End Sub

    Protected Sub ddlProvinsi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlProvinsi.SelectedIndexChanged
        Dim _isSpecial As Boolean = False
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitSpecialCity), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BabitSpecialCity), "City.ID", MatchType.Exact, objLoginUser.Dealer.City.ID))
        criterias.opAnd(New Criteria(GetType(BabitSpecialCity), "Status", MatchType.Exact, 1))
        Dim arlBabitSpecialCity As ArrayList = New BabitSpecialCityFacade(User).Retrieve(criterias)
        If Not IsNothing(arlBabitSpecialCity) AndAlso arlBabitSpecialCity.Count > 0 Then
            _isSpecial = True
        End If

        BindddlCity(ddlProvinsi.SelectedValue, _isSpecial)
        BindGridEventUploadFile()
        BindGridBabitEvent()
        BindGridDisplayAndTarget()
    End Sub

    Public Sub ddlFCategoryBabitEvent_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim arrDDL As ArrayList = New ArrayList
        Dim ddlFCategoryBabitEvent As DropDownList = sender
        Dim gridItem As DataGridItem = ddlFCategoryBabitEvent.Parent.Parent()
        Dim ddlFJenisBabitEvent As DropDownList
        If gridItem.DataSetIndex > -1 Then
            ddlFJenisBabitEvent = gridItem.FindControl("ddlEJenisBabitEvent")
        Else
            ddlFJenisBabitEvent = gridItem.FindControl("ddlFJenisBabitEvent")
        End If
        ddlFJenisBabitEvent.Items.Clear()
        If ddlFCategoryBabitEvent.SelectedIndex > 0 Then
            arrDDL = New ArrayList
            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(BabitParameterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(BabitParameterDetail), "BabitParameterHeader.ID", MatchType.Exact, ddlFCategoryBabitEvent.SelectedValue))
            arrDDL = New BabitParameterDetailFacade(User).Retrieve(criterias2)

            ddlFJenisBabitEvent.DataSource = arrDDL
            ddlFJenisBabitEvent.DataTextField = "ParameterDetailName"
            ddlFJenisBabitEvent.DataValueField = "ID"
            ddlFJenisBabitEvent.DataBind()
            ddlFJenisBabitEvent.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        Else
            ddlFJenisBabitEvent.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        End If
    End Sub

    Public Sub ddlFKategoriKendaraan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim arrDDL As ArrayList = New ArrayList
        Dim ddlFKategoriKendaraan As DropDownList = sender
        Dim gridItem As DataGridItem = ddlFKategoriKendaraan.Parent.Parent
        Dim ddlFModelKendaraan As DropDownList
        If gridItem.DataSetIndex > -1 Then
            ddlFModelKendaraan = gridItem.FindControl("ddlEModelKendaraan")
        Else
            ddlFModelKendaraan = gridItem.FindControl("ddlFModelKendaraan")
        End If

        ddlFModelKendaraan.Items.Clear()
        If ddlFKategoriKendaraan.SelectedIndex > 0 Then
            arrDDL = New ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "Category.ID", MatchType.Exact, ddlFKategoriKendaraan.SelectedValue))

            arrDDL = New SubCategoryVehicleFacade(User).Retrieve(criterias)

            ddlFModelKendaraan.DataSource = arrDDL
            ddlFModelKendaraan.DataTextField = "Name"
            ddlFModelKendaraan.DataValueField = "ID"
            ddlFModelKendaraan.DataBind()
            ddlFModelKendaraan.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        Else
            ddlFModelKendaraan.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        End If
    End Sub

    Private Sub dgBabitEvent_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgBabitEvent.ItemCommand
        Dim oBabitParameterDetail As New BabitParameterDetail
        Dim ddlECategoryBabitEvent As DropDownList
        Dim ddlFCategoryBabitEvent As DropDownList
        Dim ddlEJenisBabitEvent As DropDownList
        Dim ddlFJenisBabitEvent As DropDownList
        Dim lblCategoryBabitEvent As Label
        Dim lblJenisBabitEvent As Label
        Dim txtEItem As TextBox
        Dim txtFItem As TextBox
        Dim txtEQty As TextBox
        Dim txtFQty As TextBox
        Dim txtEPrice As TextBox
        Dim txtFPrice As TextBox
        Dim txtETotalPrice As TextBox
        Dim txtFTotalPrice As TextBox
        Dim txtEDesc As TextBox
        Dim txtFDesc As TextBox

        objBabitHeader = CType(sessHelper.GetSession(sessBabitHdr), BabitHeader)
        arlEvent = CType(sessHelper.GetSession(sessBabitEventDtl), ArrayList)

        Select Case e.CommandName
            Case "add"
                ddlFCategoryBabitEvent = CType(e.Item.FindControl("ddlFCategoryBabitEvent"), DropDownList)
                ddlFJenisBabitEvent = CType(e.Item.FindControl("ddlFJenisBabitEvent"), DropDownList)
                txtFItem = CType(e.Item.FindControl("txtFItem"), TextBox)
                txtFQty = CType(e.Item.FindControl("txtFQty"), TextBox)
                txtFPrice = CType(e.Item.FindControl("txtFPrice"), TextBox)
                txtFTotalPrice = CType(e.Item.FindControl("txtFTotalPrice"), TextBox)
                txtFDesc = CType(e.Item.FindControl("txtFDesc"), TextBox)

                If ddlFCategoryBabitEvent.SelectedValue = "-1" Then
                    MessageBox.Show("Event Kategori harus diisi.")
                    Return
                End If
                If ddlFJenisBabitEvent.SelectedValue = "-1" Then
                    MessageBox.Show("Jenis Event harus diisi.")
                    Return
                End If
                If txtFItem.Text.Trim = "" Then
                    MessageBox.Show("Item Event harus diisi.")
                    Return
                End If
                If txtFQty.Text.Trim = "" OrElse txtFQty.Text.Trim = "0" Then
                    MessageBox.Show("Qty Event harus diisi.")
                    Return
                End If
                If txtFPrice.Text.Trim = "" OrElse txtFPrice.Text.Trim = "0" Then
                    MessageBox.Show("Price Event harus diisi.")
                    Return
                End If
                txtFTotalPrice.Text = txtFQty.Text * txtFPrice.Text()

                oBabitParameterDetail = New BabitParameterDetailFacade(User).Retrieve(CInt(ddlFJenisBabitEvent.SelectedValue))

                Dim oBabitEventDetail As New BabitEventDetail
                oBabitEventDetail.BabitHeader = New BabitHeader
                oBabitEventDetail.BabitParameterDetail = oBabitParameterDetail
                oBabitEventDetail.Item = txtFItem.Text.Trim
                oBabitEventDetail.Qty = txtFQty.Text.Trim
                oBabitEventDetail.Price = txtFPrice.Text.Trim
                oBabitEventDetail.TotalPrice = (oBabitEventDetail.Qty * oBabitEventDetail.Price)
                oBabitEventDetail.Description = txtFDesc.Text.Trim
                arlEvent.Add(oBabitEventDetail)

            Case "save" 'Update this datagrid item   
                ddlECategoryBabitEvent = CType(e.Item.FindControl("ddlECategoryBabitEvent"), DropDownList)
                ddlEJenisBabitEvent = CType(e.Item.FindControl("ddlEJenisBabitEvent"), DropDownList)
                txtEItem = CType(e.Item.FindControl("txtEItem"), TextBox)
                txtEQty = CType(e.Item.FindControl("txtEQty"), TextBox)
                txtEPrice = CType(e.Item.FindControl("txtEPrice"), TextBox)
                txtETotalPrice = CType(e.Item.FindControl("txtETotalPrice"), TextBox)
                txtEDesc = CType(e.Item.FindControl("txtEDesc"), TextBox)

                If ddlECategoryBabitEvent.SelectedValue = "-1" Then
                    MessageBox.Show("Event Kategori harus diisi.")
                    Return
                End If
                If ddlEJenisBabitEvent.SelectedValue = "-1" Then
                    MessageBox.Show("Jenis Event harus diisi.")
                    Return
                End If
                If txtEItem.Text.Trim = "" Then
                    MessageBox.Show("Item Event harus diisi.")
                    Return
                End If
                If txtEQty.Text.Trim = "" OrElse txtEQty.Text.Trim = "0" Then
                    MessageBox.Show("Qty Event harus diisi.")
                    Return
                End If
                If txtEPrice.Text.Trim = "" OrElse txtEPrice.Text.Trim = "0" Then
                    MessageBox.Show("Price Event harus diisi.")
                    Return
                End If
                txtETotalPrice.Text = txtEQty.Text * txtEPrice.Text()

                oBabitParameterDetail = New BabitParameterDetailFacade(User).Retrieve(CInt(ddlEJenisBabitEvent.SelectedValue))
                Dim oBabitEventDetail As BabitEventDetail = CType(arlEvent(e.Item.ItemIndex), BabitEventDetail)
                oBabitEventDetail.BabitHeader = objBabitHeader
                oBabitEventDetail.BabitParameterDetail = oBabitParameterDetail
                oBabitEventDetail.Item = txtEItem.Text.Trim
                oBabitEventDetail.Qty = txtEQty.Text.Trim
                oBabitEventDetail.Price = txtEPrice.Text.Trim
                oBabitEventDetail.Description = txtEDesc.Text.Trim()

                dgBabitEvent.EditItemIndex = -1
                dgBabitEvent.ShowFooter = True

            Case "edit" 'Edit mode activated
                dgBabitEvent.ShowFooter = False
                dgBabitEvent.EditItemIndex = e.Item.ItemIndex

            Case "delete" 'Delete this datagrid item 
                Try
                    Dim oBabitEventDetail As BabitEventDetail = CType(arlEvent(e.Item.ItemIndex), BabitEventDetail)
                    If oBabitEventDetail.ID > 0 Then
                        Dim arrDelete As ArrayList = CType(sessHelper.GetSession(sessDeleteBabitEventDtl), ArrayList)
                        If IsNothing(arrDelete) Then arrDelete = New ArrayList
                        arrDelete.Add(oBabitEventDetail)
                        sessHelper.SetSession(sessDeleteBabitEventDtl, arrDelete)
                    End If
                    arlEvent.RemoveAt(e.Item.ItemIndex)
                Catch
                End Try
            Case "cancel" 'Cancel Update this datagrid item 
                dgBabitEvent.EditItemIndex = -1
                dgBabitEvent.ShowFooter = True

        End Select

        sessHelper.SetSession(sessBabitEventDtl, arlEvent)
        BindGridBabitEvent()
    End Sub

    Private Sub dgBabitEvent_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgBabitEvent.ItemDataBound
        Dim ddlECategoryBabitEvent As DropDownList
        Dim ddlFCategoryBabitEvent As DropDownList
        Dim ddlEJenisBabitEvent As DropDownList
        Dim ddlFJenisBabitEvent As DropDownList
        Dim lblCategoryBabitEvent As Label
        Dim lblJenisBabitEvent As Label
        Dim lblItem As Label
        Dim lblQty As Label
        Dim lblPrice As Label
        Dim lblTotalPrice As Label
        Dim lbtnEdit As LinkButton
        Dim lbtnDelete As LinkButton

        If e.Item.ItemType = ListItemType.Footer Then
            ddlFCategoryBabitEvent = CType(e.Item.FindControl("ddlFCategoryBabitEvent"), DropDownList)
            ddlFJenisBabitEvent = CType(e.Item.FindControl("ddlFJenisBabitEvent"), DropDownList)

            Dim arrDDL As ArrayList = New ArrayList
            Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitParameterHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "BabitMasterEventType.FormType", MatchType.Exact, 1))
            criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "BabitMasterEventType.ID", MatchType.Exact, ddlBabitMasterEventTypeID.SelectedValue))
            criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "Status", MatchType.Exact, 1))
            arrDDL = New BabitParameterHeaderFacade(User).Retrieve(criterias)

            With ddlFCategoryBabitEvent
                .Items.Clear()
                .DataSource = arrDDL
                .DataTextField = "ParameterName"
                .DataValueField = "ID"
                .DataBind()
                .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            End With

            ddlFJenisBabitEvent.Items.Clear()
        End If

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oBED As BabitEventDetail = CType(e.Item.DataItem, BabitEventDetail)

            If e.Item.ItemIndex = 0 Then
                ViewState("ItemIndex") = e.Item.ItemIndex
                intItemIndex = CType(ViewState("ItemIndex"), Integer)
                intBabitParameterHeaderID = oBED.BabitParameterDetail.BabitParameterHeader.ID
            Else
                If intBabitParameterHeaderID <> oBED.BabitParameterDetail.BabitParameterHeader.ID Then
                    ViewState("ItemIndex") = 0
                    intItemIndex = CType(ViewState("ItemIndex"), Integer)
                    intBabitParameterHeaderID = oBED.BabitParameterDetail.BabitParameterHeader.ID
                Else
                    ViewState("ItemIndex") = CType(ViewState("ItemIndex"), Integer) + 1
                    intItemIndex = CType(ViewState("ItemIndex"), Integer)
                End If
            End If
            e.Item.Cells(0).Text = intItemIndex + 1 + (dgBabitEvent.CurrentPageIndex * dgBabitEvent.PageSize)

            lblCategoryBabitEvent = CType(e.Item.FindControl("lblCategoryBabitEvent"), Label)
            lblJenisBabitEvent = CType(e.Item.FindControl("lblJenisBabitEvent"), Label)
            lblItem = CType(e.Item.FindControl("lblItem"), Label)
            lblQty = CType(e.Item.FindControl("lblQty"), Label)
            lblPrice = CType(e.Item.FindControl("lblPrice"), Label)
            lbtnEdit = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            lbtnDelete = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            lblTotalPrice = CType(e.Item.FindControl("lblTotalPrice"), Label)

            lblCategoryBabitEvent.Text = oBED.BabitParameterDetail.BabitParameterHeader.ParameterName()
            lblJenisBabitEvent.Text = oBED.BabitParameterDetail.ParameterDetailName()
            lblQty.Text = oBED.Qty
            lblPrice.Text = oBED.Price

            If lblItem.Text.Trim.ToLower = "total biaya :" Then
                e.Item.Cells(3).BackColor = Color.SkyBlue
                e.Item.Cells(4).BackColor = Color.SkyBlue
                e.Item.Cells(5).BackColor = Color.SkyBlue
                e.Item.Cells(6).BackColor = Color.SkyBlue
                lblItem.Font.Bold = True
                lblTotalPrice.Font.Bold = True
                lblJenisBabitEvent.Text = ""
                lblQty.Text = ""
                lblPrice.Text = ""
                lbtnEdit.Attributes("style") = "display:none"
                lbtnDelete.Attributes("style") = "display:none"
                lblCategoryBabitEvent.Attributes("style") = "display:none"
                e.Item.Cells(0).Text = ""
            Else
                lblTotalPrice.Text = Format(oBED.Qty * oBED.Price, "###,###,##0")
                lblItem.Font.Bold = False
                lblTotalPrice.Font.Bold = False
                lbtnEdit.Attributes("style") = "display:table-row"
                lbtnDelete.Attributes("style") = "display:table-row"
                lblCategoryBabitEvent.Attributes("style") = "display:table-row"
            End If
        End If

        If e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            Dim oBED As BabitEventDetail = CType(e.Item.DataItem, BabitEventDetail)
            If e.Item.ItemIndex = 0 Then
                ViewState("ItemIndex") = e.Item.ItemIndex
                intItemIndex = CType(ViewState("ItemIndex"), Integer)
                intBabitParameterHeaderID = oBED.BabitParameterDetail.BabitParameterHeader.ID
            Else
                If intBabitParameterHeaderID <> oBED.BabitParameterDetail.BabitParameterHeader.ID Then
                    ViewState("ItemIndex") = 0
                    intItemIndex = CType(ViewState("ItemIndex"), Integer)
                    intBabitParameterHeaderID = oBED.BabitParameterDetail.BabitParameterHeader.ID
                Else
                    ViewState("ItemIndex") = CType(ViewState("ItemIndex"), Integer) + 1
                    intItemIndex = CType(ViewState("ItemIndex"), Integer)
                End If
            End If
            e.Item.Cells(0).Text = intItemIndex + 1 + (dgBabitEvent.CurrentPageIndex * dgBabitEvent.PageSize)

            ddlECategoryBabitEvent = CType(e.Item.FindControl("ddlECategoryBabitEvent"), DropDownList)
            Dim arrDDL As ArrayList = New ArrayList
            Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitParameterHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "BabitMasterEventType.FormType", MatchType.Exact, 1))
            criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "BabitMasterEventType.ID", MatchType.Exact, ddlBabitMasterEventTypeID.SelectedValue))

            criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "Status", MatchType.Exact, 1))
            arrDDL = New BabitParameterHeaderFacade(User).Retrieve(criterias)
            With ddlECategoryBabitEvent
                .Items.Clear()
                .DataSource = arrDDL
                .DataTextField = "ParameterName"
                .DataValueField = "ID"
                .DataBind()
                .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
                .SelectedValue = oBED.BabitParameterDetail.BabitParameterHeader.ID
            End With

            ddlEJenisBabitEvent = CType(e.Item.FindControl("ddlEJenisBabitEvent"), DropDownList)
            arrDDL = New ArrayList
            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(BabitParameterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(BabitParameterDetail), "BabitParameterHeader.ID", MatchType.Exact, oBED.BabitParameterDetail.BabitParameterHeader.ID))
            arrDDL = New BabitParameterDetailFacade(User).Retrieve(criterias2)
            With ddlEJenisBabitEvent
                .Items.Clear()
                .DataSource = arrDDL
                .DataTextField = "ParameterDetailName"
                .DataValueField = "ID"
                .DataBind()
                .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
                .SelectedValue = oBED.BabitParameterDetail.ID
            End With
        End If
    End Sub

    Private Sub dgUploadFile_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgUploadFile.ItemCommand
        Dim _arrDataUploadFile As ArrayList = CType(sessHelper.GetSession(sessBabitDoc), ArrayList)
        If IsNothing(_arrDataUploadFile) Then _arrDataUploadFile = New ArrayList

        Select Case e.CommandName
            Case "add" 'Insert New item to datagrid
                Dim FileUpload As HtmlInputFile = CType(e.Item.FindControl("UploadFile"), HtmlInputFile)
                Dim txtKeterangan As TextBox = CType(e.Item.FindControl("txtKeterangan"), TextBox)
                Dim objPostedData As HttpPostedFile
                Dim objBabitDocument As BabitDocument = New BabitDocument()
                Dim sFileName As String

                '========= Validasi  =======================================================================
                If IsNothing(FileUpload) OrElse FileUpload.Value = String.Empty Then
                    MessageBox.Show("Lampiran masih kosong")
                    Return
                End If
                Dim _filename As String = System.IO.Path.GetFileName(FileUpload.PostedFile.FileName)
                If _filename.Trim().Length <= 0 Then
                    MessageBox.Show("Upload file belum diisi\n")
                    Return
                End If
                If _filename.Trim().Length > 0 Then
                    If FileUpload.PostedFile.ContentLength > MAX_FILE_SIZE Then
                        MessageBox.Show("Ukuran file tidak boleh melebihi " & (MAX_FILE_SIZE / 1024) & "kb\n")
                        Return
                    End If
                End If
                Dim ext As String = System.IO.Path.GetExtension(FileUpload.PostedFile.FileName)
                If Not (ext.ToUpper() = ".JPG" OrElse ext.ToUpper() = ".JPEG" OrElse ext.ToUpper() = ".PDF") Then
                    MessageBox.Show("Hanya menerima file format (PDF/JPG/JPEG)")
                    Return
                End If

                If Not IsNothing(FileUpload) OrElse FileUpload.Value <> String.Empty Then
                    objPostedData = FileUpload.PostedFile
                Else
                    objPostedData = Nothing
                End If

                If Not (IsNothing(objPostedData)) Then
                    sFileName = objPostedData.FileName.Split("\")(objPostedData.FileName.Split("\").Length - 1)

                    If KTB.DNet.UI.Helper.FileHelper.IsExecutableFile(sFileName) Then
                        MessageBox.Show("Tidak diperkenankan mengupload file dengan ekstensi '.exe'. Pastikan file anda bebas dari virus.")
                        BindGridEventUploadFile()
                        Return
                    End If

                    Dim strDealerCode As String = String.Empty
                    If IsLoginAsDealer() Then
                        strDealerCode = lblDealerCodeName.Text
                    Else
                        strDealerCode = txtDealerCode.Text
                    End If

                    If Not FileIsExist(sFileName, _arrDataUploadFile) Then
                        Dim SrcFile As String = Path.GetFileName(objPostedData.FileName) '-- Source file name
                        Dim strBabitPathConfig As String = KTB.DNet.Lib.WebConfig.GetValue("BabitFileDirectory")
                        Dim strBabitPathFile As String = "\BABIT\" & objDealer.DealerCode & "\Event\" & TimeStamp() & SrcFile.Substring(SrcFile.Length - 4)
                        Dim strDestFile As String = strBabitPathConfig & strBabitPathFile '--Destination File                       

                        objBabitDocument.BabitHeader = New BabitHeader()
                        objBabitDocument.AttachmentData = objPostedData
                        objBabitDocument.FileName = sFileName
                        objBabitDocument.Path = strDestFile
                        objBabitDocument.FileDescription = IIf(txtKeterangan.Text.Trim = String.Empty, "Babit Event Dokumen", txtKeterangan.Text.Trim)

                        UploadAttachment(objBabitDocument, TempDirectory)

                        _arrDataUploadFile.Add(objBabitDocument)
                        sessHelper.SetSession(sessBabitDoc, _arrDataUploadFile)
                    Else
                        MessageBox.Show(SR.DataIsExist("Attachment File"))
                    End If
                Else
                    MessageBox.Show(SR.DataNotFound("Attachment File"))
                End If

            Case "Delete" 'Delete this datagrid item 
                Dim oBabitDocument As BabitDocument = CType(_arrDataUploadFile(e.Item.ItemIndex), BabitDocument)
                If oBabitDocument.ID > 0 Then
                    Dim arrDelete As ArrayList = CType(sessHelper.GetSession(sessDeleteBabitDoc), ArrayList)
                    If IsNothing(arrDelete) Then arrDelete = New ArrayList
                    arrDelete.Add(oBabitDocument)
                    sessHelper.SetSession(sessDeleteBabitDoc, arrDelete)
                End If

                RemoveBabitAttachment(CType(_arrDataUploadFile(e.Item.ItemIndex), BabitDocument), TempDirectory)
                _arrDataUploadFile.RemoveAt(e.Item.ItemIndex)

            Case "Download" 'Download File
                Response.Redirect("../Download.aspx?file=" & e.CommandArgument)

        End Select

        BindGridEventUploadFile()
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

    Private Sub dgUploadFile_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgUploadFile.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dgUploadFile.CurrentPageIndex * dgUploadFile.PageSize)

            Dim arrUpload As ArrayList = CType(sessHelper.GetSession(sessBabitDoc), ArrayList)
            If Not IsNothing(arrUpload) AndAlso arrUpload.Count > 0 Then
                Dim objBabitDocument As BabitDocument = arrUpload(e.Item.ItemIndex)
                Dim lblFileName As Label = CType(e.Item.FindControl("lblFileName"), Label)
                lblFileName.Text = Path.GetFileName(objBabitDocument.FileName)
            End If
        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim str As String
        str = ValidateData()
        If (str.Length > 0) Then
            MessageBox.Show(str)
            Exit Sub
        End If
        If Me.txtBabitDealerGroup.Text.Trim <> "" Then
            If Not ValidateBabitDealerGroup(Me.txtBabitDealerGroup.Text.Trim) Then Exit Sub
        End If

        arlEvent = CType(sessHelper.GetSession(sessBabitEventDtl), ArrayList)

        'Remove row SubTotal
        For i As Integer = 0 To arlEvent.Count - 1
            Dim objEvent As BabitEventDetail = CType(arlEvent(i), BabitEventDetail)
            If objEvent.Item.ToLower = "total biaya :" Then
                arlEvent.RemoveAt(i)
                i -= 1
            End If
            If i = arlEvent.Count - 1 Then
                Exit For
            End If
        Next

        Dim _arrBabitDealerAlloc As ArrayList = CType(sessHelper.GetSession(sessBabitDealerAlloc), ArrayList)
        If IsNothing(_arrBabitDealerAlloc) Then _arrBabitDealerAlloc = New ArrayList

        Dim _arrBabitDocs As ArrayList = CType(sessHelper.GetSession(sessBabitDoc), ArrayList)
        Dim _babitHeader As New BabitHeader
        If Mode = "Edit" Then
            hdnBabitHeaderID.Value = Request.QueryString("BabitHeaderID")
            _babitHeader = New BabitHeaderFacade(User).Retrieve(CInt(hdnBabitHeaderID.Value))
            _babitHeader.Dealer = New DealerFacade(User).Retrieve(txtDealerCode.Text)
        ElseIf Mode = "New" Then
            _babitHeader.BabitRegNumber = GetRegNumber()
            _babitHeader.Dealer = objDealer
            _babitHeader.BabitStatus = 0  '--- Status Baru
        End If
        _babitHeader.DealerBranch = New DealerBranchFacade(User).Retrieve(txtTOCode.Text)
        _babitHeader.BabitMasterEventType = New BabitMasterEventTypeFacade(User).Retrieve(CType(ddlBabitMasterEventTypeID.SelectedValue, Integer))
        _babitHeader.BabitDealerNumber = txtNoSurat.Text
        _babitHeader.Location = txtLocation.Text
        _babitHeader.ProspectTarget = txtProspectTarget.Text
        _babitHeader.InvitationQty = txtInvitationQty.Text
        _babitHeader.AllocationType = ddlAlocBabitType.SelectedValue
        _babitHeader.PeriodStart = icPeriodStart.Value
        _babitHeader.PeriodEnd = icPeriodEnd.Value
        _babitHeader.MarboxID = IIf(ddlMarBox.SelectedIndex = 0, "NULL", ddlMarBox.SelectedValue)
        _babitHeader.Notes = txtNotes.Text

        Dim strDealerID As String = String.Empty
        If txtBabitDealerGroup.Text.Trim() <> "" Then
            Dim BabitDealerGroupCode As String() = txtBabitDealerGroup.Text.Split(";")
            For Each dealerCode As String In BabitDealerGroupCode
                Dim oDealer As Dealer = New KTB.DNet.BusinessFacade.General.DealerFacade(User).Retrieve(dealerCode)
                If Not IsNothing(oDealer) AndAlso oDealer.ID > 0 Then
                    If strDealerID = String.Empty Then
                        strDealerID = CStr(oDealer.ID)
                    Else
                        strDealerID += ";" & CStr(oDealer.ID)
                    End If
                End If
            Next
        End If
        _babitHeader.BabitDealerGroup = strDealerID

        Dim _babitDealerAllocation As BabitDealerAllocation
        If Mode = "Edit" Then
            If Not IsLoginAsDealer() Then
                'Dim dblBiaya As Double = 0
                'Dim dblSubsidyAmount As Double = 0
                'If txtSubsidyAmount.Text.Trim = "" Then txtSubsidyAmount.Text = 0
                'dblSubsidyAmount = txtSubsidyAmount.Text
                'If dblSubsidyAmount = 0 Then
                '    For Each bed As BabitEventDetail In arlEvent
                '        dblBiaya += CDbl(bed.Price) * CInt(bed.Qty)
                '    Next
                '    If dblBiaya > 0 Then
                '        dblSubsidyAmount = (dblBiaya * 0.5)
                '    End If
                'End If

                'Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitDealerAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                'criterias.opAnd(New Criteria(GetType(BabitDealerAllocation), "BabitHeader.ID", MatchType.Exact, _babitHeader.ID))
                'Dim arr As ArrayList = New BabitDealerAllocationFacade(User).Retrieve(criterias)
                'If Not IsNothing(arr) AndAlso arr.Count > 0 Then
                '    _babitDealerAllocation = CType(arr(0), BabitDealerAllocation)
                'Else
                '    _babitDealerAllocation = New BabitDealerAllocation
                'End If

                'txtSubsidyAmount.Text = dblSubsidyAmount
                '_babitDealerAllocation.BabitHeader = _babitHeader
                '_babitDealerAllocation.BabitCategory = IIf(ddlAllocationBabit.SelectedIndex = 0, "", ddlAllocationBabit.SelectedValue)
                '_babitDealerAllocation.SubsidyAmount = dblSubsidyAmount
            End If
        End If

        Dim intCityID As Integer = 0
        If ddlKota.SelectedIndex <> 0 Then
            intCityID = CType(ddlKota.SelectedValue, Integer)
        End If
        _babitHeader.City = New CityFacade(User).Retrieve(intCityID)

        Dim _arlDisplayAndTarget As ArrayList = CType(sessHelper.GetSession(sessBabitEventDisplayTarget), ArrayList)
        Dim _result As Integer = 0

        If IsNothing(arlEvent) Then arlEvent = New ArrayList
        If IsNothing(_arrBabitDocs) Then _arrBabitDocs = New ArrayList
        If IsNothing(_arlDisplayAndTarget) Then _arlDisplayAndTarget = New ArrayList

        If Mode = "Edit" Then
            arlDelEvent = CType(sessHelper.GetSession(sessDeleteBabitEventDtl), ArrayList)
            arlDelDocument = CType(sessHelper.GetSession(sessDeleteBabitDoc), ArrayList)
            arlDelDisplayAndTarget = CType(sessHelper.GetSession(sessDeleteBabitEventDisplayTarget), ArrayList)
            Dim _arrDelBabitDealerAlloc As ArrayList = CType(sessHelper.GetSession(sessDeleteBabitDealerAlloc), ArrayList)
            If IsNothing(_arrDelBabitDealerAlloc) Then _arrDelBabitDealerAlloc = New ArrayList

            _result = New BabitEventDetailFacade(User).UpdateTransaction(_babitHeader, arlEvent, arlDelEvent, _arrBabitDocs, arlDelDocument, _arlDisplayAndTarget, arlDelDisplayAndTarget, _arrBabitDealerAlloc, _arrDelBabitDealerAlloc)
        Else
            _result = New BabitEventDetailFacade(User).InsertTransaction(_babitHeader, arlEvent, _arrBabitDocs, _arlDisplayAndTarget, _arrBabitDealerAlloc)
        End If

        Dim strJs As String = String.Empty
        If _result > 0 Then
            CommitAttachment(_arrBabitDocs)
            If Request.QueryString("Mode") = "Edit" Then
                If Not IsNothing(arlDelDocument) Then
                    RemoveBabitDocumentAttachment(arlDelDocument, TargetDirectory)
                End If
            End If
            strJs = "alert('Simpan Data Berhasil');"
            strJs += "window.location = '../Babit/FrmBabitList.aspx?Back=OK';"
            ClearTempData()
            ClearAll()
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

    Private Sub btnGetInfoDealer_Click(sender As Object, e As EventArgs) Handles btnGetInfoDealer.Click
        Dim oDealer As Dealer = New KTB.DNet.BusinessFacade.General.DealerFacade(User).Retrieve(txtDealerCode.Text.Trim())
        lblArea2CodeDesc.Text = IIf(IsNothing(oDealer.Area2), "", oDealer.Area2.AreaCode & " / " & oDealer.Area2.Description)
        lblDealerCodeName.Text = oDealer.DealerCode & " / " & oDealer.DealerName
    End Sub

    Private Sub btnGetInfoDealerBranch_Click(sender As Object, e As EventArgs) Handles btnGetInfoDealerBranch.Click
        Dim objDealerBranch As DealerBranch = New DealerBranchFacade(User).Retrieve(hdntxtTOCode.Value.Trim())
        lblArea2CodeDesc.Text = IIf(IsNothing(objDealerBranch.Area2), "", objDealerBranch.Area2.AreaCode & " / " & objDealerBranch.Area2.Description)
        lblTOName.Text = objDealerBranch.Name
        txtTOCode.Text = objDealerBranch.DealerBranchCode
    End Sub

    Private Sub btnBaru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBaru.Click
        Me.ClearAll()
        Me.btnSave.Enabled = True
    End Sub

    Protected Sub dgDisplayAndTarget_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgDisplayAndTarget.ItemDataBound
        Dim ddlFKategoriKendaraan As DropDownList
        Dim ddlFModelKendaraan As DropDownList
        Dim ddlEKategoriKendaraan As DropDownList
        Dim ddlEModelKendaraan As DropDownList
        Dim lblKategoriKendaraan As Label
        Dim lblModelKendaraan As Label
        Dim lblQtyDisplay As Label
        Dim lblTargetPenjualan As Label
        Dim lblTestDrive As Label
        Dim lbtnEdit As LinkButton
        Dim lbtnDelete As LinkButton
        Dim CBETestDrive As CheckBox

        If e.Item.ItemType = ListItemType.Footer Then
            ddlFKategoriKendaraan = CType(e.Item.FindControl("ddlFKategoriKendaraan"), DropDownList)
            ddlFModelKendaraan = CType(e.Item.FindControl("ddlFModelKendaraan"), DropDownList)

            Dim arrDDL As ArrayList = New ArrayList
            Dim criterias As New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Category), "ProductCategory.Code", MatchType.Exact, KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")))
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(Category), "CategoryCode", Sort.SortDirection.ASC))  '-- Sort by Category code
            arrDDL = New CategoryFacade(User).Retrieve(criterias)

            With ddlFKategoriKendaraan
                .Items.Clear()
                .DataSource = arrDDL
                .DataTextField = "CategoryCode"
                .DataValueField = "ID"
                .DataBind()
                .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            End With

            ddlFModelKendaraan.Items.Clear()
            ddlFModelKendaraan.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        End If

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oBDC As BabitDisplayCar = CType(e.Item.DataItem, BabitDisplayCar)

            If e.Item.ItemIndex = 0 Then
                ViewState("ItemIndex") = e.Item.ItemIndex
                intItemIndex = CType(ViewState("ItemIndex"), Integer)
                intBabitDisplayCarID = oBDC.ID
            Else
                If intBabitParameterHeaderID <> oBDC.ID Then
                    ViewState("ItemIndex") = 0
                    intItemIndex = CType(ViewState("ItemIndex"), Integer)
                    intBabitDisplayCarID = oBDC.ID
                Else
                    ViewState("ItemIndex") = CType(ViewState("ItemIndex"), Integer) + 1
                    intItemIndex = CType(ViewState("ItemIndex"), Integer)
                End If
            End If
            e.Item.Cells(0).Text = intItemIndex + 1 + (dgDisplayAndTarget.CurrentPageIndex * dgDisplayAndTarget.PageSize)

            lblKategoriKendaraan = CType(e.Item.FindControl("lblKategoriKendaraan"), Label)
            lblModelKendaraan = CType(e.Item.FindControl("lblModelKendaraan"), Label)
            lblQtyDisplay = CType(e.Item.FindControl("lblQtyDisplay"), Label)
            lblTargetPenjualan = CType(e.Item.FindControl("lblTargetPenjualan"), Label)
            lblTestDrive = CType(e.Item.FindControl("lblTestDrive"), Label)
            lbtnEdit = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            lbtnDelete = CType(e.Item.FindControl("lbtnDelete"), LinkButton)

            lblKategoriKendaraan.Text = oBDC.SubCategoryVehicle.Category.CategoryCode
            lblModelKendaraan.Text = oBDC.SubCategoryVehicle.Name
            lblQtyDisplay.Text = oBDC.Qty.ToString
            lblTargetPenjualan.Text = oBDC.SalesTarget.ToString
            If oBDC.IsTestDrive Then
                lblTestDrive.Text = "Ya"
            Else
                lblTestDrive.Text = "Tidak"
            End If
            lbtnEdit.Attributes("style") = "display:table-row"
            lbtnDelete.Attributes("style") = "display:table-row"
        End If

        If e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            Dim oBDC As BabitDisplayCar = CType(e.Item.DataItem, BabitDisplayCar)
            If e.Item.ItemIndex = 0 Then
                ViewState("ItemIndex") = e.Item.ItemIndex
                intItemIndex = CType(ViewState("ItemIndex"), Integer)
                intBabitDisplayCarID = oBDC.ID
            Else
                If intBabitParameterHeaderID <> oBDC.ID Then
                    ViewState("ItemIndex") = 0
                    intItemIndex = CType(ViewState("ItemIndex"), Integer)
                    intBabitDisplayCarID = oBDC.ID
                Else
                    ViewState("ItemIndex") = CType(ViewState("ItemIndex"), Integer) + 1
                    intItemIndex = CType(ViewState("ItemIndex"), Integer)
                End If
            End If
            e.Item.Cells(0).Text = intItemIndex + 1 + (dgBabitEvent.CurrentPageIndex * dgBabitEvent.PageSize)

            ddlEKategoriKendaraan = CType(e.Item.FindControl("ddlEKategoriKendaraan"), DropDownList)
            Dim arrDDL As ArrayList = New ArrayList
            Dim criterias As New CriteriaComposite(New Criteria(GetType(Category), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Category), "ProductCategory.Code", MatchType.Exact, KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")))
            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(Category), "CategoryCode", Sort.SortDirection.ASC))  '-- Sort by Category code
            arrDDL = New CategoryFacade(User).Retrieve(criterias)

            With ddlEKategoriKendaraan
                .Items.Clear()
                .DataSource = arrDDL
                .DataTextField = "CategoryCode"
                .DataValueField = "ID"
                .DataBind()
                .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
                .SelectedValue = oBDC.SubCategoryVehicle.Category.ID
            End With

            ddlEModelKendaraan = CType(e.Item.FindControl("ddlEModelKendaraan"), DropDownList)
            arrDDL = New ArrayList
            Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(SubCategoryVehicle), "Category.ID", MatchType.Exact, ddlEKategoriKendaraan.SelectedValue))

            arrDDL = New SubCategoryVehicleFacade(User).Retrieve(criterias2)
            With ddlEModelKendaraan
                .Items.Clear()
                .DataSource = arrDDL
                .DataTextField = "Name"
                .DataValueField = "ID"
                .DataBind()
                .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
                .SelectedValue = oBDC.SubCategoryVehicle.ID
            End With

            CBETestDrive = CType(e.Item.FindControl("CBETestDrive"), CheckBox)
            If oBDC.IsTestDrive Then
                CBETestDrive.Checked = True
            End If
        End If
    End Sub

    Protected Sub dgDisplayAndTarget_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgDisplayAndTarget.ItemCommand
        Dim ddlFKategoriKendaraan As DropDownList
        Dim ddlEKategoriKendaraan As DropDownList
        Dim lblKategoriKendaraan As Label

        Dim ddlFModelKendaraan As DropDownList
        Dim ddlEModelKendaraan As DropDownList
        Dim lblModelKendaraan As Label

        Dim txtFQtyDisplay As TextBox
        Dim txtEQtyDisplay As TextBox

        Dim txtFTargetPenjualan As TextBox
        Dim txtETargetPenjualan As TextBox

        Dim CBFTestDrive As CheckBox
        Dim CBETestDrive As CheckBox

        Dim oSubCategoryVehicle As New SubCategoryVehicle

        arlDisplayAndTarget = CType(sessHelper.GetSession(sessBabitEventDisplayTarget), ArrayList)

        Select Case e.CommandName
            Case "add"
                ddlFKategoriKendaraan = CType(e.Item.FindControl("ddlFKategoriKendaraan"), DropDownList)
                ddlFModelKendaraan = CType(e.Item.FindControl("ddlFModelKendaraan"), DropDownList)
                txtFQtyDisplay = CType(e.Item.FindControl("txtFQtyDisplay"), TextBox)
                txtFTargetPenjualan = CType(e.Item.FindControl("txtFTargetPenjualan"), TextBox)
                CBFTestDrive = CType(e.Item.FindControl("CBFTestDrive"), CheckBox)

                If ddlFKategoriKendaraan.SelectedValue = -1 Then
                    MessageBox.Show("Kategori Kendaraan harus dipilih.")
                    Return
                End If
                If ddlFModelKendaraan.SelectedValue = -1 Then
                    MessageBox.Show("Model Kendaraan harus dipilih.")
                    Return
                End If
                If txtFQtyDisplay.Text = String.Empty OrElse txtFQtyDisplay.Text.Trim = "0" Then
                    MessageBox.Show("Qty Display harus diisi dan harus lebih dari 0")
                    Return
                End If
                If txtFTargetPenjualan.Text = String.Empty OrElse txtFTargetPenjualan.Text.Trim = "0" Then
                    MessageBox.Show("Target Penjualan harus diisi dan harus lebih dari 0.")
                    Return
                End If

                oSubCategoryVehicle = New SubCategoryVehicleFacade(User).Retrieve(CShort(ddlFModelKendaraan.SelectedValue))

                Dim oBabitDisplayCar As New BabitDisplayCar
                oBabitDisplayCar.BabitHeader = New BabitHeader
                oBabitDisplayCar.SubCategoryVehicle = oSubCategoryVehicle
                oBabitDisplayCar.Qty = CInt(txtFQtyDisplay.Text.Trim)
                oBabitDisplayCar.SalesTarget = CInt(txtFTargetPenjualan.Text.Trim)
                If CBFTestDrive.Checked Then
                    oBabitDisplayCar.IsTestDrive = True
                Else
                    oBabitDisplayCar.IsTestDrive = False
                End If
                arlDisplayAndTarget.Add(oBabitDisplayCar)
            Case "save"
                ddlEKategoriKendaraan = CType(e.Item.FindControl("ddlEKategoriKendaraan"), DropDownList)
                ddlEModelKendaraan = CType(e.Item.FindControl("ddlEModelKendaraan"), DropDownList)
                txtEQtyDisplay = CType(e.Item.FindControl("txtEQtyDisplay"), TextBox)
                txtETargetPenjualan = CType(e.Item.FindControl("txtETargetPenjualan"), TextBox)
                CBETestDrive = CType(e.Item.FindControl("CBETestDrive"), CheckBox)

                If ddlEKategoriKendaraan.SelectedValue = -1 Then
                    MessageBox.Show("Kategori Kendaraan harus dipilih.")
                    Return
                End If
                If ddlEModelKendaraan.SelectedValue = -1 Then
                    MessageBox.Show("Model Kendaraan harus dipilih.")
                    Return
                End If
                If txtEQtyDisplay.Text = String.Empty OrElse txtEQtyDisplay.Text.Trim = "0" Then
                    MessageBox.Show("Qty Display harus diisi dan harus lebih dari 0")
                    Return
                End If
                If txtETargetPenjualan.Text = String.Empty OrElse txtETargetPenjualan.Text.Trim = "0" Then
                    MessageBox.Show("Target Penjualan harus diisi dan harus lebih dari 0.")
                    Return
                End If

                oSubCategoryVehicle = New SubCategoryVehicleFacade(User).Retrieve(CShort(ddlEModelKendaraan.SelectedValue))

                Dim oBabitDisplayCar As BabitDisplayCar = CType(arlDisplayAndTarget(e.Item.ItemIndex), BabitDisplayCar)
                oBabitDisplayCar.BabitHeader = New BabitHeader
                oBabitDisplayCar.SubCategoryVehicle = oSubCategoryVehicle
                oBabitDisplayCar.Qty = CInt(txtEQtyDisplay.Text.Trim)
                oBabitDisplayCar.SalesTarget = CInt(txtETargetPenjualan.Text.Trim)
                If CBETestDrive.Checked Then
                    oBabitDisplayCar.IsTestDrive = True
                Else
                    oBabitDisplayCar.IsTestDrive = False
                End If
                dgDisplayAndTarget.EditItemIndex = -1
                dgDisplayAndTarget.ShowFooter = True
            Case "edit"
                dgDisplayAndTarget.ShowFooter = False
                dgDisplayAndTarget.EditItemIndex = e.Item.ItemIndex
            Case "delete"
                Try
                    Dim oBabitDisplayCar As BabitDisplayCar = CType(arlDisplayAndTarget(e.Item.ItemIndex), BabitDisplayCar)
                    If oBabitDisplayCar.ID > 0 Then
                        Dim arrDelete As ArrayList = CType(sessHelper.GetSession(sessDeleteBabitEventDisplayTarget), ArrayList)
                        If IsNothing(arrDelete) Then arrDelete = New ArrayList
                        arrDelete.Add(oBabitDisplayCar)
                        sessHelper.SetSession(sessDeleteBabitEventDisplayTarget, arrDelete)
                    End If
                    arlDisplayAndTarget.RemoveAt(e.Item.ItemIndex)
                Catch
                End Try
            Case "cancel"
                dgDisplayAndTarget.EditItemIndex = -1
                dgDisplayAndTarget.ShowFooter = True
        End Select

        sessHelper.SetSession(sessBabitEventDisplayTarget, arlDisplayAndTarget)
        BindGridDisplayAndTarget()
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("FrmBabitList.aspx?Back=OK")
    End Sub

    Private Sub LoadMarBox()

        Try
            Dim objDealer As Dealer = CType(sessHelper.GetSession("DEALER"), Dealer)
            Dim objBabit As BabitInterfaceHelper = New BabitInterfaceHelper()
            Dim strBabitType As String = New AppConfigFacade(User).Retrieve("BabitCognitoSharePoint.BabitType.Event").Value
            'If objBabit.RetrieveBabitMarbox(objDealer.DealerCode, strBabitType) Then
            '    BindMarbox(strBabitType)
            'End If
            If KTB.DNet.SFIntegration.SchedulingNonSF.BabitCognitoLogic.Read(objDealer.DealerCode, strBabitType) Then
                BindMarbox(strBabitType)
            End If

        Catch ex As Exception
            MessageBox.Show("Error : " + ex.Message.ToString())
        End Try
        'Dim client As System.Net.WebClient = New System.Net.WebClient()
        'client.Headers("Authorization") = "Bearer Db7i7pVn8QbQbvVSniUDRqMHGtgY9DygYo25VPVEfGoX"
        'client.Headers("Content-type") = "application/json"
        'Dim mylist As New BabitMarBox.RootObject

        'Try
        '    client.BaseAddress = "https://api.typeform.com/forms/Zq1Mc6/responses"
        '    Dim _jsonResponse = client.DownloadString("https://api.typeform.com/forms/Zq1Mc6/responses")
        '    mylist = Newtonsoft.Json.JsonConvert.DeserializeObject(Of BabitMarBox.RootObject)(_jsonResponse)

        'Catch ex As Net.WebException
        '    If ex.Status = Net.WebExceptionStatus.ProtocolError Then
        '        Dim wrsp As Net.HttpWebResponse = CType(ex.Response, Net.HttpWebResponse)
        '        Dim statusCode As Integer = CType(wrsp.StatusCode, Integer)
        '        Dim msg = wrsp.StatusDescription
        '        Throw New HttpException(statusCode, msg)
        '    Else
        '        Throw New HttpException(500, ex.Message)
        '    End If
        'End Try
        'sesHelper.SetSession("Marbox", mylist)
        'BindMarbox()
    End Sub

    Private Sub DownloadFile(ByVal webUri As String, ByVal userName As String, ByVal password As String, ByVal file2 As String)

        Dim funcConfig As AppConfigFacade = New AppConfigFacade(User)
        Dim wp As System.Net.WebProxy = New System.Net.WebProxy(funcConfig.Retrieve("MSLeadApiProxyAddress").Value, Convert.ToInt32(funcConfig.Retrieve("MSLeadApiProxyPort").Value))
        'Dim wp As System.Net.WebProxy = New System.Net.WebProxy("172.17.240.84", 9090)
        Try
            Using client As New WebClient()

                client.Proxy = wp
                Dim securePassword As SecureString = New SecureString()
                For Each c As String In password
                    securePassword.AppendChar(c)
                Next
                'Dim credentials = New Microsoft.SharePoint.Client.SharePointOnlineCredentials(userName, securePassword)
                'credentials.GetAuthenticationCookie(New Uri(webUri))

                'client.Credentials = credentials
                'client.Headers.Add("X-FORMS_BASED_AUTH_ACCEPTED", "f")
                'client.Headers.Add("User-Agent: Other")
                Dim result = Guid.NewGuid().ToString()
                'Dim strjson = client.DownloadString(file2)
                client.DownloadFile(file2, result.ToString() + ".json")
                Console.WriteLine(result.ToString() + ".json")
                'Dim bowerjson As String = File.ReadAllText(strjson.ToString)
                'MessageBox.Show(strjson)
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Function GetMarboxFromBabitTransaction() As List(Of BabitHeader)
        Dim arr As List(Of BabitHeader)
        Dim objfac As New BabitHeaderFacade(User)
        Dim cri As New CriteriaComposite(New Criteria(GetType(BabitHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'Request Erwin
        'cri.opAnd(New Criteria(GetType(BabitHeader), "PeriodEnd", MatchType.GreaterOrEqual, New DateTime(Now.Year, Now.Month, 1).ToString("yyyyMMdd")))
        cri.opAnd(New Criteria(GetType(BabitHeader), "Dealer.ID", MatchType.Exact, objDealer.ID))
        arr = objfac.Retrieve(cri, "tambahan")
        Return arr
    End Function

    Private Sub BindMarbox(ByVal strBabitType As String)

        Dim listBabitHeader As List(Of BabitHeader) = GetMarboxFromBabitTransaction()

        'Dim objDealer As Dealer = CType(sesHelper.GetSession("DEALER"), Dealer)
        Dim cri As New CriteriaComposite(New Criteria(GetType(BabitMarketingBox), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        cri.opAnd(New Criteria(GetType(BabitMarketingBox), "DealerID", MatchType.Exact, objDealer.ID))
        cri.opAnd(New Criteria(GetType(BabitMarketingBox), "BabitType", MatchType.Partial, strBabitType))
        'Request Erwin
        'cri.opAnd(New Criteria(GetType(BabitMarketingBox), "EndDate", MatchType.GreaterOrEqual, New DateTime(Now.Year, Now.Month, 1).ToString("yyyyMMdd")))
        'cri.opAnd(New Criteria(GetType(BabitMarketingBox), "StartDate", MatchType.GreaterOrEqual, Now.Date.ToString("yyyyMMdd")))
        'cri.opAnd(New Criteria(GetType(BabitMarketingBox), "EndDate", MatchType.LesserOrEqual, Now.Date.ToString("yyyyMMdd")))

        Dim arrMarbox As ArrayList = New BabitMarketingBoxFacade(User).Retrieve(cri)
        If arrMarbox.Count > 0 Then
            ddlMarBox.Enabled = True
            sessHelper.SetSession("Marbox", arrMarbox)
            ddlMarBox.Items.Clear()
            With ddlMarBox.Items
                .Add(New ListItem("Silahkan Pilih", "-1", True))
                For Each MB As BabitMarketingBox In arrMarbox
                    Try
                        Dim obj = (From obj1 As BabitHeader In listBabitHeader Where obj1.MarboxID = MB.SubMissionID And obj1.ID <> hdnBabitHeaderID.Value).Count
                        If obj = 0 Then
                            .Add(New ListItem(MB.EventName, MB.SubMissionID))
                        End If
                    Catch
                    End Try
                Next
            End With
            ddlMarBox.SelectedIndex = 0
        End If
    End Sub

    Private Sub UsedMarbox()
        For i As Integer = 0 To ddlMarBox.Items.Count - 1
            Dim crits As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits.opAnd(New Criteria(GetType(BabitHeader), "MarboxID", MatchType.Exact, ddlMarBox.Items(i).Value))
            Dim arlCheck As ArrayList = New BabitHeaderFacade(User).Retrieve(crits)

            If arlCheck.Count > 0 Then
                If Mode <> "New" Then
                    If objBabitHeader.MarboxID <> ddlMarBox.Items(i).Value Then
                        If i <> 0 Then
                            ddlMarBox.Items.RemoveAt(i)
                        End If
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
            Dim Marbox As ArrayList = sessHelper.GetSession("Marbox")
            If Not IsNothing(Marbox) Then
                For Each MB As BabitMarketingBox In Marbox
                    Try
                        If ddlMarBox.SelectedValue = MB.SubMissionID Then
                            lblPeriodeMarbox.Text = MB.StartDate.ToString("dd/MM/yyyy") & " - " & MB.EndDate.ToString("dd/MM/yyyy")
                            lblLokasiMarbox.Text = MB.EventLocation
                            Exit For
                        End If
                    Catch
                    End Try
                Next
            End If
        End If
    End Sub

    'Protected Sub ddlMarBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMarBox.SelectedIndexChanged
    'If ddlMarBox.SelectedIndex = 0 Then
    '    lblLokasiMarbox.Text = ""
    '    lblPeriodeMarbox.Text = ""
    'Else
    '    Dim Marbox As BabitMarBox.RootObject = CType(sesHelper.GetSession("Marbox"), BabitMarBox.RootObject)
    '    For Each MB As BabitMarBox.Item In Marbox.items
    '        Try
    '            If MB.landing_id = ddlMarBox.SelectedValue Then
    '                lblPeriodeMarbox.Text = MB.answers.Item(3).date & " - " & MB.answers.Item(4).date
    '                lblLokasiMarbox.Text = MB.answers.Item(2).text
    '            End If
    '        Catch
    '        End Try
    '    Next
    'End If
    'End Sub

    Private Sub ddlBabitMasterEventTypeID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBabitMasterEventTypeID.SelectedIndexChanged
        BindGridBabitEvent()
    End Sub

    Protected Sub lnkReload_Click(sender As Object, e As EventArgs) Handles lnkReload.Click
        LoadMarBox()
    End Sub

    Private Sub MarboxLoad2()

    End Sub

    Protected Sub dgAlloc_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgAlloc.ItemDataBound
        Select Case e.Item.ItemType
            Case ListItemType.Item, ListItemType.AlternatingItem
                Dim arrAlloc As ArrayList = CType(sessHelper.GetSession(sessBabitDealerAlloc), ArrayList)
                Dim objBabitDealerAllocation As BabitDealerAllocation = arrAlloc(e.Item.ItemIndex)

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

                Dim dblRemains As Double = GetSubsidyRemaining(dealerID.ToString)
                Dim lblJmlMaxSubsidy As Label = CType(e.Item.FindControl("lblJmlMaxSubsidy"), Label)
                lblJmlMaxSubsidy.Text = Format(dblRemains, "###,###,##0")

                Dim lblAllocationBabit As Label = CType(e.Item.FindControl("lblAllocationBabit"), Label)
                Dim lblJmlSubsidy As Label = CType(e.Item.FindControl("lblJmlSubsidy"), Label)
                lblAllocationBabit.Text = "BABIT " & objBabitDealerAllocation.BabitCategory.ToString()
                lblJmlSubsidy.Text = objBabitDealerAllocation.SubsidyAmount.ToString("#,##0")

            Case ListItemType.Footer
                Dim ddlAllocationBabit As DropDownList = CType(e.Item.FindControl("ddlAllocationBabit"), DropDownList)
                Dim txtJmlSubsidy As TextBox = CType(e.Item.FindControl("txtJmlSubsidy"), TextBox)
                Dim txtDealerCodeAlokasi As TextBox = CType(e.Item.FindControl("txtDealerCodeAlokasi"), TextBox)

                Dim lblFJmlMaxSubsidy As Label = CType(e.Item.FindControl("lblFJmlMaxSubsidy"), Label)
                lblFJmlMaxSubsidy.Text = 0

                Dim _oDealer As Dealer
                If Not IsNothing(sessHelper.GetSession("FrmInputBabitEvent.DEALER")) Then
                    _oDealer = CType(sessHelper.GetSession("FrmInputBabitEvent.DEALER"), Dealer)
                Else
                    _oDealer = New Dealer
                End If
                Dim lblSearchDealerGrid As Label = CType(e.Item.FindControl("lblSearchDealerGrid"), Label)
                lblSearchDealerGrid.Attributes("onclick") = "ShowPPDealerSelectionAlokasi(this,'" & _oDealer.ID.ToString & "');"

                txtDealerCodeAlokasi.Text = ""
                With ddlAllocationBabit
                    .Items.Clear()
                    .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
                End With
                txtJmlSubsidy.Text = Format(CalculateBiaya(), "###,###")

            Case ListItemType.EditItem
                Dim arrAlloc As ArrayList = CType(sessHelper.GetSession(sessBabitDealerAlloc), ArrayList)
                Dim objBabitDealerAllocation As BabitDealerAllocation = arrAlloc(e.Item.ItemIndex)
                Dim txtEDealerCodeAlokasi As TextBox = CType(e.Item.FindControl("txtEDealerCodeAlokasi"), TextBox)
                Dim ddlEAllocationBabit As DropDownList = CType(e.Item.FindControl("ddlEAllocationBabit"), DropDownList)

                Dim _oDealer As New Dealer
                If Not IsNothing(objBabitDealerAllocation.Dealer) Then
                    txtEDealerCodeAlokasi.Text = objBabitDealerAllocation.Dealer.DealerCode
                End If
                Dim dealerID As Integer = 0
                If txtEDealerCodeAlokasi.Text.Trim <> "" Then
                    _oDealer = New DealerFacade(User).Retrieve(txtEDealerCodeAlokasi.Text)
                    If Not IsNothing(_oDealer) AndAlso _oDealer.ID > 0 Then
                        dealerID = _oDealer.ID
                        BindDDLAllocationBabit(_oDealer.ID, ddlEAllocationBabit)
                        ddlEAllocationBabit.SelectedValue = objBabitDealerAllocation.BabitCategory
                    End If
                End If
                Dim lblESearchDealerGrid As Label = CType(e.Item.FindControl("lblESearchDealerGrid"), Label)
                lblESearchDealerGrid.Attributes("onclick") = "ShowPPDealerSelectionAlokasi(this," & dealerID & ");"

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

                Dim dblRemains As Double = GetSubsidyRemaining(_oDealer.ID.ToString())
                Dim lblEJmlMaxSubsidy As Label = CType(e.Item.FindControl("lblEJmlMaxSubsidy"), Label)
                lblEJmlMaxSubsidy.Text = Format(dblRemains, "###,###,##0")
                objBabitDealerAllocation.MaxSubsidyAmount = dblRemains

                Dim txtEJmlSubsidy As TextBox = CType(e.Item.FindControl("txtEJmlSubsidy"), TextBox)
                txtEJmlSubsidy.Text = objBabitDealerAllocation.SubsidyAmount.ToString("###,###,##0")
        End Select
    End Sub

    Private Function ValidateAddAndEditGridAlloc(ByVal e As DataGridCommandEventArgs, ByVal arrAlloc As ArrayList) As String
        Dim sb As StringBuilder = New StringBuilder
        Dim txtDealerCodeAlokasi As TextBox
        Dim ddlAllocationBabit As DropDownList
        Dim txtJmlSubsidy As TextBox
        Dim lblJmlMaxSubsidy As Label
        Dim oAlloc As BabitDealerAllocation

        Select Case e.CommandName
            Case "Add"
                txtDealerCodeAlokasi = CType(e.Item.FindControl("txtDealerCodeAlokasi"), TextBox)
                ddlAllocationBabit = CType(e.Item.FindControl("ddlAllocationBabit"), DropDownList)
                txtJmlSubsidy = CType(e.Item.FindControl("txtJmlSubsidy"), TextBox)
                lblJmlMaxSubsidy = CType(e.Item.FindControl("lblFJmlMaxSubsidy"), Label)

                For Each obj As BabitDealerAllocation In arrAlloc
                    If IsNothing(obj.Dealer) Then
                        obj.Dealer = New Dealer
                    End If
                    If txtDealerCodeAlokasi.Text.Trim = obj.Dealer.DealerCode Then
                        If ddlAllocationBabit.SelectedValue = obj.BabitCategory Then
                            sb.Append("- Dealer Alokasi dan Tipe Alokasi Babit tidak boleh sama dalam satu pengajuan Babit\n")
                            Exit For
                        End If
                    End If
                Next

            Case "Save"
                Dim lblID As Label = CType(e.Item.FindControl("lblEID"), Label)
                txtDealerCodeAlokasi = CType(e.Item.FindControl("txtEDealerCodeAlokasi"), TextBox)
                ddlAllocationBabit = CType(e.Item.FindControl("ddlEAllocationBabit"), DropDownList)
                txtJmlSubsidy = CType(e.Item.FindControl("txtEJmlSubsidy"), TextBox)
                lblJmlMaxSubsidy = CType(e.Item.FindControl("lblEJmlMaxSubsidy"), Label)

                For Each obj As BabitDealerAllocation In arrAlloc
                    If IsNothing(obj.Dealer) Then
                        obj.Dealer = New Dealer
                    End If
                    If txtDealerCodeAlokasi.Text.Trim = obj.Dealer.DealerCode Then
                        If ddlAllocationBabit.SelectedValue = obj.BabitCategory And obj.ID <> lblID.Text Then
                            sb.Append("- Tipe Alokasi tidak boleh sama dalam satu pengajuan Babit\n")
                            Exit For
                        End If
                    End If
                Next
                oAlloc = CType(arrAlloc(e.Item.ItemIndex), BabitDealerAllocation)

            Case Else
        End Select

        If txtDealerCodeAlokasi.Text.Trim = "" Then
            sb.Append("- Dealer Alokasi harus diisi\n")
        End If
        If ddlAllocationBabit.SelectedIndex = 0 Then
            sb.Append("- Alokasi BABIT harus diisi\n")
        End If
        If txtJmlSubsidy.Text.Trim = "" OrElse txtJmlSubsidy.Text.Trim = "0" Then
            txtJmlSubsidy.Text = 0
            sb.Append("- Jumlah Subsidi harus diisi\n")
        End If
        If lblJmlMaxSubsidy.Text.Trim = "" OrElse lblJmlMaxSubsidy.Text.Trim = "0" Then
            lblJmlMaxSubsidy.Text = 0
        End If
        Dim oDealer As Dealer = New DealerFacade(User).Retrieve(txtDealerCodeAlokasi.Text)
        If (IsNothing(oDealer)) OrElse (Not IsNothing(oDealer) AndAlso oDealer.ID = 0) Then
            sb.Append("- Dealer Alokasi tidak valid\n")
        End If

        If ddlAllocationBabit.SelectedValue <> "SPESIAL" Then
            Dim dblRemains As Double = lblJmlMaxSubsidy.Text
            Dim dblSubsidyAmount As Double = txtJmlSubsidy.Text
            If Not IsNothing(oAlloc) AndAlso oAlloc.ID > 0 Then
                dblRemains = dblRemains + oAlloc.SubsidyAmount
            End If
            If dblRemains < dblSubsidyAmount Then
                sb.Append("- Jumlah Subsidi Tidak Boleh Melebihi Maksimal Subsidi Babit\n")
            End If
        End If

        Return sb.ToString()
    End Function

    Protected Sub dgAlloc_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgAlloc.ItemCommand
        Dim arrAlloc As ArrayList = CType(sessHelper.GetSession(sessBabitDealerAlloc), ArrayList)
        If IsNothing(arrAlloc) Then arrAlloc = New ArrayList

        Dim strMSG As String = String.Empty
        Select Case e.CommandName
            Case "Add"
                strMSG = ValidateAddAndEditGridAlloc(e, arrAlloc)
                If (strMSG.Length > 0) Then
                    MessageBox.Show(strMSG)
                    Exit Sub
                End If

                Dim txtDealerCodeAlokasi As TextBox = CType(e.Item.FindControl("txtDealerCodeAlokasi"), TextBox)
                Dim ddlAllocationBabit As DropDownList = CType(e.Item.FindControl("ddlAllocationBabit"), DropDownList)
                Dim txtJmlSubsidy As TextBox = CType(e.Item.FindControl("txtJmlSubsidy"), TextBox)
                Dim lblJmlMaxSubsidy As Label = CType(e.Item.FindControl("lblFJmlMaxSubsidy"), Label)

                Dim dblRemains As Double = lblJmlMaxSubsidy.Text
                Dim oAlloc As New BabitDealerAllocation
                oAlloc.Dealer = New DealerFacade(User).Retrieve(txtDealerCodeAlokasi.Text)
                oAlloc.BabitCategory = IIf(ddlAllocationBabit.SelectedIndex = 0, "", ddlAllocationBabit.SelectedValue)
                Try
                    oAlloc.SubsidyAmount = txtJmlSubsidy.Text
                Catch
                    oAlloc.SubsidyAmount = CalculateBiaya()
                End Try
                oAlloc.MaxSubsidyAmount = dblRemains
                arrAlloc.Add(oAlloc)

            Case "Save"
                strMSG = ValidateAddAndEditGridAlloc(e, arrAlloc)
                If (strMSG.Length > 0) Then
                    MessageBox.Show(strMSG)
                    Exit Sub
                End If

                Dim lblEID As Label = CType(e.Item.FindControl("lblEID"), Label)
                Dim txtEDealerCodeAlokasi As TextBox = CType(e.Item.FindControl("txtEDealerCodeAlokasi"), TextBox)
                Dim ddlEAllocationBabit As DropDownList = CType(e.Item.FindControl("ddlEAllocationBabit"), DropDownList)
                Dim txtEJmlSubsidy As TextBox = CType(e.Item.FindControl("txtEJmlSubsidy"), TextBox)
                Dim lblEJmlMaxSubsidy As Label = CType(e.Item.FindControl("lblEJmlMaxSubsidy"), Label)

                Dim oAlloc As BabitDealerAllocation = CType(arrAlloc(e.Item.ItemIndex), BabitDealerAllocation)
                oAlloc.Dealer = New DealerFacade(User).Retrieve(txtEDealerCodeAlokasi.Text)
                oAlloc.BabitCategory = IIf(ddlEAllocationBabit.SelectedIndex = 0, "", ddlEAllocationBabit.SelectedValue)
                Try
                    oAlloc.SubsidyAmount = txtEJmlSubsidy.Text
                Catch
                    oAlloc.SubsidyAmount = CalculateBiaya()
                End Try
                dgAlloc.EditItemIndex = -1
                dgAlloc.ShowFooter = True

            Case "edit" 'Edit mode activated
                dgAlloc.ShowFooter = False
                dgAlloc.EditItemIndex = e.Item.ItemIndex

            Case "cancel" 'Cancel Update this datagrid item 
                dgAlloc.EditItemIndex = -1
                dgAlloc.ShowFooter = True

            Case "Delete"
                Try
                    Dim oAlloc As BabitDealerAllocation = CType(arrAlloc(e.Item.ItemIndex), BabitDealerAllocation)
                    If oAlloc.ID > 0 Then
                        Dim arrDelete As ArrayList = CType(sessHelper.GetSession(sessDeleteBabitDealerAlloc), ArrayList)
                        If IsNothing(arrDelete) Then arrDelete = New ArrayList
                        arrDelete.Add(oAlloc)
                        sessHelper.SetSession(sessDeleteBabitDealerAlloc, arrDelete)
                    End If
                    arrAlloc.RemoveAt(e.Item.ItemIndex)
                Catch ex As Exception
                End Try
        End Select

        sessHelper.SetSession(sessBabitDealerAlloc, arrAlloc)
        BindGridAlloc()
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

        '    Dim arrView As ArrayList = New V_BabitMasterRetailTargetFacade(User).RetrieveAllocationBudgetByDealer(ddlAllocationBabit.SelectedValue, oDealer.ID.ToString(), intCategoryID, intSubCategoryVehicleID, objBabitHeader.PeriodStart)
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

    Public Sub txtDealerCodeAlokasi_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim arrDDL As ArrayList = New ArrayList
        Dim txtDealerCodeAlokasi As TextBox = sender
        Dim gridItem As DataGridItem = txtDealerCodeAlokasi.Parent.Parent
        Dim ddlAllocationBabit As DropDownList
        Dim lblFJmlMaxSubsidy As Label
        If gridItem.DataSetIndex > -1 Then
            ddlAllocationBabit = gridItem.FindControl("ddlEAllocationBabit")
            lblFJmlMaxSubsidy = gridItem.FindControl("lblEJmlMaxSubsidy")
        Else
            ddlAllocationBabit = gridItem.FindControl("ddlAllocationBabit")
            lblFJmlMaxSubsidy = gridItem.FindControl("lblFJmlMaxSubsidy")
        End If

        If txtDealerCodeAlokasi.Text.Trim <> "" Then
            Dim _oDealer As Dealer = sessHelper.GetSession("FrmInputBabitEvent.DEALER")
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerCode", MatchType.Exact, txtDealerCodeAlokasi.Text))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Dealer), "DealerGroup.ID", MatchType.Exact, _oDealer.DealerGroup.ID))
            Dim arrDealer As ArrayList = New DealerFacade(User).Retrieve(criterias)
            If Not IsNothing(arrDealer) AndAlso arrDealer.Count > 0 Then
            Else
                MessageBox.Show("Dealer : " & txtDealerCodeAlokasi.Text & " tidak termasuk Dealer Group yang mengajukan Proposal")
                txtDealerCodeAlokasi.Text = ""
                txtDealerCodeAlokasi.Focus()
                Exit Sub
            End If
        End If

        lblFJmlMaxSubsidy.Text = 0
        ddlAllocationBabit.Items.Clear()
        ddlAllocationBabit.Items.Insert(0, New ListItem("Silahkan Pilih", -1))
        If txtDealerCodeAlokasi.Text.Trim <> "" Then
            Dim oDealer As Dealer = New DealerFacade(User).Retrieve(txtDealerCodeAlokasi.Text.Trim)
            If Not IsNothing(oDealer) AndAlso oDealer.ID > 0 Then
                BindDDLAllocationBabit(oDealer.ID, ddlAllocationBabit)
                Dim dblRemains As Double = GetSubsidyRemaining(oDealer.ID.ToString())
                lblFJmlMaxSubsidy.Text = Format(dblRemains, "###,###,##0")
            End If
        End If
    End Sub

    Private Function CalculateBiaya() As Integer
        Dim _return As Integer = 0

        If SesDealer().Title = EnumDealerTittle.DealerTittle.KTB Then
            Try
                Dim headerID As Integer = CInt(Request.QueryString("BabitHeaderID"))

                Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitEventDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(BabitEventDetail), "BabitHeader.ID", MatchType.Exact, headerID))
                Dim ArrBabitEventDtl As ArrayList = New BabitEventDetailFacade(User).Retrieve(crit)
                For Each objExp As BabitEventDetail In ArrBabitEventDtl
                    _return += (objExp.Qty * objExp.Price)
                Next
                _return = _return / 2
            Catch
            End Try
        End If

        Return _return
    End Function


#End Region

End Class
