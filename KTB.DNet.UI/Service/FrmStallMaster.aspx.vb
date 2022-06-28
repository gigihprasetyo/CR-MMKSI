#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
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

Public Class FrmStallMaster
    Inherits System.Web.UI.Page
#Region " Private fields "
    Dim sessHelp As SessionHelper = New SessionHelper
    Dim InvoiceList As New ArrayList  '-- List of invoice
    Dim _PCAccessAllowed As Boolean = False
    Dim _CVAccessAllowed As Boolean = False
    Dim _LCVAccessAllowed As Boolean = False
    Private objDealer As Dealer
    Dim isDealerDMS As Boolean = False
    'Private isDealerPiloting As Boolean = False

#End Region

#Region "Custom Method"
    Private Sub ActivateUserPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.StallMaster_View_Privilage) Then
            Response.Redirect("../frmAccessDenied.aspx?modulName=Stall Master")
        End If
    End Sub
    Private Sub InitData()
        'If Session("sessCM") Is Nothing Then
        Dim objDealer As Dealer = CType(sessHelp.GetSession("DEALER"), Dealer)
        Dim objUser As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)
        ' Dim mode As String = "add"
        If Not objDealer Is Nothing Then
            'txtKodeDealer.Text = objDealer.DealerCode
            'txtNamaDealer.Text = objDealer.DealerName
            Dim dealerSystems As DealerSystems = New DealerSystemsFacade(User).RetrieveByDealerCode(objDealer.DealerCode)

            If Not IsNothing(dealerSystems) AndAlso dealerSystems.SystemID <> 1 Then
                isDealerDMS = True
            End If

            If (isDealerDMS = True) Then
                btnSave.Visible = False

            End If

            If Not SecurityProvider.Authorize(Context.User, SR.StallMaster_Input_Privilage) Then
                btnSave.Visible = False
            End If

            sessHelp.SetSession("isDealerDMS", isDealerDMS)
            'isDealerPiloting = TCHelper.GetActiveTCResult(objDealer.ID, CInt(EnumDealerTransType.DealerTransKind.PilotingStall))
            'sessHelp.SetSession("isDealerPiloting", isDealerPiloting)
        End If

        'bindBodyPaint()
        'bindKategori()
        'Bindlokasi()
        'bindStatus()
        'BindTipe()
        bindDDLBodiPaint()
        bindDDLKategori()
        bindDDLlokasi()
        bindDDLstatus()
        bindDDLTipe()
    End Sub

    Private Sub ReadData()
        '-- Read all data selected
        'Dim criterias As New CriteriaComposite()
        'Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.StallMaster), "ID", MatchType.Greater, 0))
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.StallMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))


        Dim objDealer As Dealer = CType(sessHelp.GetSession("DEALER"), Dealer)
        Dim objUser As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)
        ' Dim mode As String = "add"
        If Not objDealer Is Nothing Then
            If Not objDealer.DealerGroup Is Nothing Then
                criterias.opAnd(New Criteria(GetType(StallMaster), "Dealer.DealerGroup.DealerGroupCode", MatchType.Exact, objDealer.DealerGroup.DealerGroupCode))
            End If
            'txtKodeDealer.Text = objDealer.DealerCode
            'txtNamaDealer.Text = objDealer.DealerName

        End If

        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.NationalHoliday), "HolidayDate", MatchType.Exact, FakturDate.Day))

        '-- Row status = active
        'Dim criterias As New CriteriaComposite(New Criteria(GetType(StallMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If (txtKodeDealer.Text <> "") Then
            criterias.opAnd(New Criteria(GetType(StallMaster), "Dealer.DealerCode", MatchType.Exact, txtKodeDealer.Text))
        End If

        If txtKodeStall.Text.Trim() <> "" Then
            criterias.opAnd(New Criteria(GetType(StallMaster), "StallCode", MatchType.Exact, txtKodeStall.Text.Trim()))
        End If

        If (txtKodeStallDealer.Text <> "") Then
            criterias.opAnd(New Criteria(GetType(StallMaster), "StallCodeDealer", MatchType.Exact, txtKodeStallDealer.Text.Trim()))
        End If

        If (txtNamaStall.Text <> "") Then
            criterias.opAnd(New Criteria(GetType(StallMaster), "StallName", MatchType.Exact, txtNamaStall.Text.Trim()))
        End If

        If ddlLokasiStall.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(StallMaster), "StallLocation", MatchType.Exact, ddlLokasiStall.SelectedValue))
        End If

        If ddlTipeStall.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(StallMaster), "StallType", MatchType.Exact, ddlTipeStall.SelectedValue))
        End If

        If ddlKategoriStall.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(StallMaster), "StallCategory", MatchType.Exact, ddlKategoriStall.SelectedValue))
        End If

        If ddlBodyPaint.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(StallMaster), "IsBodyPaint", MatchType.Exact, ddlBodyPaint.SelectedValue))
        End If

        '-- Status
        If ddlStatus.SelectedIndex <> 0 Then
            criterias.opAnd(New Criteria(GetType(StallMaster), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If
        ViewState("CurrentSortColumn") = "ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

        'Dim arrStallMaster As ArrayList = New StallMasterFacade(User).RetrieveByCriteria(criterias, 1, 25, 25)
        Dim arrStallMaster As ArrayList = New StallMasterFacade(User).Retrieve(criterias)


        '-- Store InvoiceReqList into session for later use
        sessHelp.SetSession("StallMasterList", arrStallMaster)

    End Sub

    Private Sub BindPage(ByVal pageIndex As Integer)
        Dim arrStallMaster As ArrayList = CType(sessHelp.GetSession("StallMasterList"), ArrayList)
        Dim aStatus As New ArrayList
        If arrStallMaster.Count <> 0 Then
            ' SortListControl(arrStallMaster, CType(ViewState("currSortColumn"), String), CType(ViewState("currSortDirection"), Integer))
            Dim PagedList As ArrayList = ArrayListPager.DoPage(arrStallMaster, pageIndex, dtgStallMaster.PageSize)
            dtgStallMaster.DataSource = PagedList
            dtgStallMaster.VirtualItemCount = arrStallMaster.Count()
            dtgStallMaster.DataBind()
        Else
            dtgStallMaster.DataSource = New ArrayList
            dtgStallMaster.VirtualItemCount = 0
            dtgStallMaster.CurrentPageIndex = 0
            dtgStallMaster.DataBind()
        End If
    End Sub

    Private Sub GetRunningNumber()
        Dim objStallMaster As StallMaster = New StallMaster
        Dim nResult As Integer
        objStallMaster = New StallMasterFacade(User).GetRunningNumber(txtKodeDealer.Text.Trim())
        'Dim arrStallMaster As ArrayList = New StallMasterFacade(User).GetRunningNumber(txtKodeDealer.Text.Trim())
        Dim strRunNum As String = ""
        If Not IsNothing(objStallMaster.StallCode) Then
            strRunNum = objStallMaster.StallCode
            strRunNum = strRunNum.Substring(8, 2)
            Dim intRunNum = Convert.ToInt16(strRunNum) + 1
            strRunNum = Convert.ToString(intRunNum)

            If Len(strRunNum) = 1 Then
                strRunNum = "0" & strRunNum
            End If
        Else
            strRunNum = "01"
        End If
     
        txtKodeStall.Text = txtKodeDealer.Text & "-S" & strRunNum

    End Sub

    Private Sub ClearData()
        lblSearchDealer.Visible = True
        HiddenField1.Value = String.Empty
        HiddenField2.Value = String.Empty
        txtKodeDealer.Text = String.Empty
        txtNamaDealer.Text = String.Empty
        txtKodeStall.Text = String.Empty '"Auto Generate"
        txtKodeStallDealer.Text = String.Empty
        txtNamaStall.Text = String.Empty
        ddlLokasiStall.SelectedIndex = 0
        ddlKategoriStall.SelectedIndex = 0
        ddlTipeStall.SelectedIndex = 0
        ddlBodyPaint.SelectedIndex = 0
        ddlStatus.SelectedIndex = 0
        ReadData()
        dtgStallMaster.CurrentPageIndex = 0
        BindPage(dtgStallMaster.CurrentPageIndex)
        ViewState.Add("vsProcess", "Insert")
        'bindBodyPaint()
        'bindKategori()
        'Bindlokasi()
        'bindStatus()
        'BindTipe()
        bindDDLBodiPaint()
        bindDDLKategori()
        bindDDLlokasi()
        bindDDLstatus()
        bindDDLTipe()
    End Sub

    Private Function InsertModel() As Integer
        Dim objStallMaster As StallMaster = New StallMaster
        Dim intStallRealTime = GetStallRealTime()

        Dim nResult As Integer
        objStallMaster.Dealer = New DealerFacade(User).GetDealer(txtKodeDealer.Text)

        '        objStallMaster.Dealer.DealerCode = txtKodeDealer.Text
        objStallMaster.StallCode = txtKodeStall.Text
        objStallMaster.StallCodeDealer = txtKodeStallDealer.Text
        objStallMaster.StallName = txtNamaStall.Text
        objStallMaster.StallLocation = ddlLokasiStall.SelectedValue.ToString()
        objStallMaster.StallType = ddlTipeStall.SelectedValue.ToString()
        objStallMaster.StallCategory = ddlKategoriStall.SelectedValue.ToString()
        objStallMaster.IsBodyPaint = ddlBodyPaint.SelectedValue.ToString()
        objStallMaster.Status = ddlStatus.SelectedValue.ToString()
        'If ddlStatus.SelectedValue = 0 Then
        '    objStallMaster.RowStatus = 0
        'ElseIf ddlStatus.SelectedValue = 1 Then
        '    objStallMaster.RowStatus = -1
        'End If
        nResult = New StallMasterFacade(User).Insert(objStallMaster)

        'If ddlTipeStall.SelectedValue = intStallRealTime Then
        '    Dim objAllocation As AllocationRealTimeService = New AllocationRealTimeService
        '    Dim objDealer As Dealer = CType(sessHelp.GetSession("DEALER"), Dealer)
        '    Dim intAlokasi As Integer
        '    Dim arrDDL As ArrayList = New ArrayList
        '    Dim criterias As New CriteriaComposite(New Criteria(GetType(AllocationRealTimeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        '    criterias.opAnd(New Criteria(GetType(AllocationRealTimeService), "Dealer.ID", MatchType.Exact, objDealer.ID))
        '    arrDDL = New AllocationRealTimeServiceFacade(User).Retrieve(criterias)
        '    If arrDDL.Count > 0 Then
        '        objAllocation = CType(arrDDL(0), AllocationRealTimeService)
        '        If ddlStatus.SelectedValue = 0 Then
        '            objAllocation.CurrentStall = CInt(objAllocation.CurrentStall) + 1
        '        Else
        '            objAllocation.CurrentStall = CInt(objAllocation.CurrentStall) + 0
        '        End If
        '        nResult = New AllocationRealTimeServiceFacade(User).Update(objAllocation)
        '        'intAlokasi = CInt(objAppConfig.AlokasiStall)
        '    End If
        'End If

        If nResult = -1 Then
            MessageBox.Show(SR.SaveFail)
        Else
            MessageBox.Show(SR.SaveSuccess)
        End If
        Return nResult
    End Function

    Private Function UpdateModel() As Integer

        Dim nRes As Integer = 0

        Dim objStallMaster As StallMaster = New StallMaster
        Dim intStallRealTime = GetStallRealTime()
        'Dim nResult As Integer
        objStallMaster.ID = HiddenField3.Value
        objStallMaster.Dealer = New DealerFacade(User).GetDealer(txtKodeDealer.Text)
        '        objStallMaster.Dealer.DealerCode = txtKodeDealer.Text
        objStallMaster.StallCode = txtKodeStall.Text
        objStallMaster.StallCodeDealer = txtKodeStallDealer.Text
        objStallMaster.StallName = txtNamaStall.Text
        objStallMaster.StallLocation = ddlLokasiStall.SelectedValue.ToString()
        objStallMaster.StallType = ddlTipeStall.SelectedValue.ToString()
        objStallMaster.StallCategory = ddlKategoriStall.SelectedValue.ToString()
        objStallMaster.IsBodyPaint = ddlBodyPaint.SelectedValue.ToString()
        objStallMaster.Status = ddlStatus.SelectedValue.ToString()

        If ddlTipeStall.SelectedValue = intStallRealTime Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.StallWorkingTime), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(StallWorkingTime), "StallMaster.ID", MatchType.Exact, objStallMaster.ID))
            criterias.opAnd(New Criteria(GetType(StallWorkingTime), "VisitType", MatchType.Exact, 1))


            Dim arrStallWorkingTime As ArrayList = New StallWorkingTimeFacade(User).Retrieve(criterias)
            If arrStallWorkingTime.Count > 0 Then
                For Each item As StallWorkingTime In arrStallWorkingTime
                    nRes = New StallWorkingTimeFacade(User).UpdateVisitType(item.ID)
                Next
            End If

        End If


        'update rowstatus
        'If ddlStatus.SelectedValue = 0 Then
        '    objStallMaster.RowStatus = 0
        'ElseIf ddlStatus.SelectedValue = 1 Then
        '    objStallMaster.RowStatus = -1
        'End If
        Dim nResult As Integer = 0
        If nRes = -1 Then
            MessageBox.Show(SR.SaveFail)
            Exit Function
        Else
            nResult = New StallMasterFacade(User).Update(objStallMaster)

            'If ddlTipeStall.SelectedValue = intStallRealTime Then
            '    Dim objAllocation As AllocationRealTimeService = New AllocationRealTimeService
            '    Dim objDealer As Dealer = CType(sessHelp.GetSession("DEALER"), Dealer)
            '    Dim intAlokasi As Integer
            '    Dim arrDDL As ArrayList = New ArrayList
            '    Dim criterias As New CriteriaComposite(New Criteria(GetType(AllocationRealTimeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            '    criterias.opAnd(New Criteria(GetType(AllocationRealTimeService), "Dealer.ID", MatchType.Exact, objDealer.ID))
            '    arrDDL = New AllocationRealTimeServiceFacade(User).Retrieve(criterias)
            '    If arrDDL.Count > 0 Then
            '        objAllocation = CType(arrDDL(0), AllocationRealTimeService)
            '        If ddlStatus.SelectedValue = 0 Then
            '            objAllocation.CurrentStall = CInt(objAllocation.CurrentStall) + 1
            '        Else
            '            objAllocation.CurrentStall = CInt(objAllocation.CurrentStall) - 1
            '        End If
            '        nResult = New AllocationRealTimeServiceFacade(User).Update(objAllocation)
            '        'intAlokasi = CInt(objAppConfig.AlokasiStall)
            '    End If
            'End If
            If nResult = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)
            End If

        End If
        Return nResult
    End Function

    Private Sub DoDownload(ByVal data As ArrayList)
        Dim sFileName As String

        sFileName = "StallMaster" & Now.Year & Now.Month & Now.Day & Now.Hour & Now.Minute & Now.Second

        '-- Temp file must be a randomly named file!
        Dim SvcIncomingData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(SvcIncomingData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(SvcIncomingData, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                WriteStallMaster(sw, data)

                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls")

        Catch ex As Exception
            MessageBox.Show("Download data gagal")
        End Try
    End Sub

    Private Sub WriteStallMaster(ByVal sw As StreamWriter, ByVal data As ArrayList)
        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder
        Dim header As String

        If Not IsNothing(data) Then
            itemLine.Remove(0, itemLine.Length)  '-- Empty line
            itemLine.Append("Stall Master")
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)

            itemLine.Append("No" & tab)
            itemLine.Append("Kode Dealer" & tab)
            itemLine.Append("Kode Stall" & tab)
            itemLine.Append("Kode Stall Dealer" & tab)
            itemLine.Append("Nama Stall" & tab)
            itemLine.Append("Lokasi Stall" & tab)
            itemLine.Append("Tipe Stall" & tab)
            itemLine.Append("Kategori Stall" & tab)
            itemLine.Append("Body Paint" & tab)
            itemLine.Append("Status" & tab)

            sw.WriteLine(itemLine.ToString())
            Dim intNo As Integer = 0
            Dim strBodyPaint As String = ""
            Dim strStatus As String = ""
            Dim strLokasi As String = ""
            Dim strKategori As String = ""
            Dim strTipe As String = ""
            For Each item As StallMaster In data
                itemLine.Remove(0, itemLine.Length)
                itemLine.Append(intNo + 1 & tab)
                itemLine.Append(item.Dealer.DealerCode & tab)
                itemLine.Append(item.StallCode & tab)
                itemLine.Append(item.StallCodeDealer & tab)
                itemLine.Append(item.StallName & tab)

                strBodyPaint = GetStdCodeDesc("StallMaster.IsBodyPaint", item.IsBodyPaint)
                strStatus = GetStdCodeDesc("StallMaster.Status", item.Status)
                strTipe = GetStdCodeDesc("StallMaster.TipeStall", item.StallType)
                strKategori = GetStdCodeDesc("StallMaster.KategoriStall", item.StallCategory)
                strLokasi = GetStdCodeDesc("StallMaster.LokasiStall", item.StallLocation)

                itemLine.Append(strLokasi & tab)
                itemLine.Append(strTipe & tab)
                itemLine.Append(strKategori & tab)
                itemLine.Append(strBodyPaint & tab)
                itemLine.Append(strStatus & tab)

                sw.WriteLine(itemLine.ToString())
                intNo = intNo + 1
            Next
        End If
    End Sub

    Private Sub bindDDLstatus()
        Dim arrDDL As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "StallMaster.Status"))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(StandardCode), "Sequence", CType(Sort.SortDirection.ASC, Sort.SortDirection)))
        arrDDL = New StandardCodeFacade(User).Retrieve(criterias, sortColl)
        With ddlStatus
            .Items.Clear()
            .DataSource = arrDDL
            .DataTextField = "ValueDesc"
            .DataValueField = "ValueId"
            .DataBind()
        End With
        ddlStatus.Items.Insert(0, New ListItem("Silahkan Piih ", -1))
        ddlStatus.SelectedIndex = 0
    End Sub

    Private Sub bindDDLlokasi()
        Dim arrDDL As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "StallMaster.LokasiStall"))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(StandardCode), "Sequence", CType(Sort.SortDirection.ASC, Sort.SortDirection)))
        arrDDL = New StandardCodeFacade(User).Retrieve(criterias, sortColl)
        With ddlLokasiStall
            .Items.Clear()
            .DataSource = arrDDL
            .DataTextField = "ValueDesc"
            .DataValueField = "ValueId"
            .DataBind()
        End With
        ddlLokasiStall.Items.Insert(0, New ListItem("Silahkan Piih ", -1))
        ddlLokasiStall.SelectedIndex = 0
    End Sub

    Private Sub bindDDLKategori()
        Dim arrDDL As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "StallMaster.KategoriStall"))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(StandardCode), "Sequence", CType(Sort.SortDirection.ASC, Sort.SortDirection)))
        arrDDL = New StandardCodeFacade(User).Retrieve(criterias, sortColl)
        With ddlKategoriStall
            .Items.Clear()
            .DataSource = arrDDL
            .DataTextField = "ValueDesc"
            .DataValueField = "ValueId"
            .DataBind()
        End With
        ddlKategoriStall.Items.Insert(0, New ListItem("Silahkan Piih ", -1))
        ddlKategoriStall.SelectedIndex = 0
    End Sub

    Private Sub bindDDLTipe()
        Dim arrDDL As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "StallMaster.TipeStall"))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(StandardCode), "Sequence", CType(Sort.SortDirection.ASC, Sort.SortDirection)))
        arrDDL = New StandardCodeFacade(User).Retrieve(criterias, sortColl)
        With ddlTipeStall
            .Items.Clear()
            .DataSource = arrDDL
            .DataTextField = "ValueDesc"
            .DataValueField = "ValueId"
            .DataBind()
        End With
        ddlTipeStall.Items.Insert(0, New ListItem("Silahkan Piih ", -1))
        ddlTipeStall.SelectedIndex = 0
    End Sub

    Private Function GetStallRealTime() As Integer
        Dim nResult As Integer = 0
        Dim arrDDL As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "StallMaster.TipeStall"))
        criterias.opAnd(New Criteria(GetType(StandardCode), "ValueCode", MatchType.Exact, "RealTimeService"))
        arrDDL = New StandardCodeFacade(User).Retrieve(criterias)
        Dim objStandradCode As New StandardCode
        If arrDDL.Count > 0 Then
            objStandradCode = CType(arrDDL(0), StandardCode)
            nResult = objStandradCode.ValueId
        End If
        Return nResult
    End Function

    Private Function GetMaxStallRealTime() As Integer
        Dim objDealer As New Dealer
        Dim criterias3 As New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias3.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.Exact, txtKodeDealer.Text))
        Dim arrDDLs As ArrayList = New DealerFacade(User).Retrieve(criterias3)
        If arrDDLs.Count > 0 Then
            objDealer = CType(arrDDLs(0), Dealer)
        End If

        Dim nResult As Integer = 0
        Dim arrDDL As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(AllocationRealTimeService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(AllocationRealTimeService), "Dealer.ID", MatchType.Exact, objDealer.ID))
        arrDDL = New AllocationRealTimeServiceFacade(User).Retrieve(criterias)
        Dim objAppConfig As New AllocationRealTimeService
        If arrDDL.Count > 0 Then
            objAppConfig = CType(arrDDL(0), AllocationRealTimeService)
            nResult = CInt(objAppConfig.AlokasiStall)
        End If
        Return nResult
    End Function
    Private Function AvailableRealTimeStall(ByVal intStallRealTime As Integer) As Integer
        Dim nResult As Integer = 0
        Dim objStallMaster2 As StallMaster = New StallMaster
        objStallMaster2.Dealer = New DealerFacade(User).GetDealer(txtKodeDealer.Text)
        Dim criteriasss As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.StallMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriasss.opAnd(New Criteria(GetType(StallMaster), "Dealer.ID", MatchType.Exact, objStallMaster2.Dealer.ID))
        criteriasss.opAnd(New Criteria(GetType(StallMaster), "StallType", MatchType.Exact, intStallRealTime))
        criteriasss.opAnd(New Criteria(GetType(StallMaster), "Status", MatchType.Exact, 0))
        Dim arrStallMaster2 As ArrayList = New StallMasterFacade(User).Retrieve(criteriasss)
        If (arrStallMaster2.Count > 0) Then
            nResult = arrStallMaster2.Count
           
        End If
        Return nResult
    End Function

    Private Sub bindDDLBodiPaint()
        Dim arrDDL As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "StallMaster.IsBodyPaint"))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(StandardCode), "Sequence", CType(Sort.SortDirection.ASC, Sort.SortDirection)))
        arrDDL = New StandardCodeFacade(User).Retrieve(criterias, sortColl)
        With ddlBodyPaint
            .Items.Clear()
            .DataSource = arrDDL
            .DataTextField = "ValueDesc"
            .DataValueField = "ValueId"
            .DataBind()
        End With
        ddlBodyPaint.Items.Insert(0, New ListItem("Silahkan Piih ", -1))
        ddlBodyPaint.SelectedIndex = 0
    End Sub

    Private Function GetStdCodeDesc(ByVal Category As String, ByVal ValueID As Integer) As String
        Dim nResult As String = ""
        Dim criterias3 As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias3.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, Category))
        criterias3.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, ValueID))
        Dim arrDDL As ArrayList = New StandardCodeFacade(User).Retrieve(criterias3)
        Dim objStandardCode As New StandardCode
        If Not IsNothing(arrDDL) AndAlso arrDDL.Count > 0 Then
            objStandardCode = CType(arrDDL(0), StandardCode)
            nResult = objStandardCode.ValueDesc
        End If
        Return nResult
    End Function

    Private Function GetStdCodeID(ByVal Category As String, ByVal ValueDesc As String) As Integer
        Dim nResult As Integer = 0
        Dim criterias3 As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias3.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, Category))
        criterias3.opAnd(New Criteria(GetType(StandardCode), "ValueDesc", MatchType.Exact, ValueDesc))
        Dim arrDDL As ArrayList = New StandardCodeFacade(User).Retrieve(criterias3)
        Dim objStandardCode As New StandardCode
        If Not IsNothing(arrDDL) AndAlso arrDDL.Count > 0 Then
            objStandardCode = CType(arrDDL(0), StandardCode)
            nResult = objStandardCode.ValueId
        End If
        Return nResult
    End Function

    Private Function GetStallType(ByVal id As Integer) As Integer
        Dim nResult As Integer = 0
        Dim criterias3 As New CriteriaComposite(New Criteria(GetType(StallMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias3.opAnd(New Criteria(GetType(StallMaster), "ID", MatchType.Exact, HiddenField3.Value))
        Dim arrDDL As ArrayList = New StallMasterFacade(User).Retrieve(criterias3)
        Dim objStallMaster As New StallMaster
        If Not IsNothing(arrDDL) AndAlso arrDDL.Count > 0 Then
            objStallMaster = CType(arrDDL(0), StallMaster)
            nResult = CInt(objStallMaster.StallType)
        End If
        Return nResult
    End Function

#Region "download excel"

    Private Sub SetDownload()
        Dim arrData As New DataTable
        Dim crits As CriteriaComposite
        If dtgStallMaster.Items.Count < 1 Then
            MessageBox.Show("Tidak ada data yang di download")
            Return
        End If

        ' mengambil data yang dibutuhkan
        Dim arrFlatRate As ArrayList = CType(sessHelp.GetSession("StallMasterList"), ArrayList)


        If arrFlatRate.Count > 0 Then
            CreateExcel("StallMaster", arrFlatRate)
        End If

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
            ws.Cells("C3").Value = "Kode Stall"
            ws.Cells("D3").Value = "Kode Stall Dealer"
            ws.Cells("E3").Value = "Nama Stall"
            ws.Cells("F3").Value = "Lokasi Stall"
            ws.Cells("G3").Value = "Tipe Stall"
            ws.Cells("H3").Value = "Kategori Stall"
            ws.Cells("I3").Value = "Body Paint"
            ws.Cells("J3").Value = "Status"

            Dim strBodyPaint As String = ""
            Dim strStatus As String = ""
            Dim strLokasi As String = ""
            Dim strKategori As String = ""
            Dim strTipe As String = ""
            Dim idx As Integer = 0
            For i As Integer = 0 To Data.Count - 1
                Dim item As StallMaster = Data(i)

                ws.Cells(idx + 4, 1).Value = idx + 1
                ws.Cells(idx + 4, 2).Value = item.Dealer.DealerCode
                ws.Cells(idx + 4, 3).Value = item.StallCode
                ws.Cells(idx + 4, 4).Value = item.StallCodeDealer
                ws.Cells(idx + 4, 5).Value = item.StallName

                Dim criterias3 As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias3.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "StallMaster.IsBodyPaint"))
                criterias3.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, item.IsBodyPaint))
                Dim arrDDL As ArrayList = New StandardCodeFacade(User).Retrieve(criterias3)
                Dim objStandardCode As New StandardCode
                If Not IsNothing(arrDDL) AndAlso arrDDL.Count > 0 Then
                    objStandardCode = CType(arrDDL(0), StandardCode)
                    strBodyPaint = objStandardCode.ValueDesc
                End If

                Dim criterias1 As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias1.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "StallMaster.Status"))
                criterias1.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, item.Status))
                Dim arrDDL1 As ArrayList = New StandardCodeFacade(User).Retrieve(criterias1)
                Dim objStandardCode1 As New StandardCode
                If Not IsNothing(arrDDL1) AndAlso arrDDL1.Count > 0 Then
                    objStandardCode1 = CType(arrDDL1(0), StandardCode)
                    strStatus = objStandardCode1.ValueDesc
                End If

                Dim criterias2 As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias2.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "StallMaster.TipeStall"))
                criterias2.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, item.StallType))
                Dim arrDDL2 As ArrayList = New StandardCodeFacade(User).Retrieve(criterias2)
                Dim objStandardCode2 As New StandardCode
                If Not IsNothing(arrDDL2) AndAlso arrDDL2.Count > 0 Then
                    objStandardCode2 = CType(arrDDL2(0), StandardCode)
                    strTipe = objStandardCode2.ValueDesc
                End If

                Dim criterias0 As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias0.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "StallMaster.KategoriStall"))
                criterias0.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, item.StallCategory))
                Dim arrDDL0 As ArrayList = New StandardCodeFacade(User).Retrieve(criterias0)
                Dim objStandardCode0 As New StandardCode
                If Not IsNothing(arrDDL0) AndAlso arrDDL0.Count > 0 Then
                    objStandardCode0 = CType(arrDDL0(0), StandardCode)
                    strKategori = objStandardCode0.ValueDesc
                End If

                Dim criteriasa As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteriasa.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "StallMaster.LokasiStall"))
                criteriasa.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, item.StallLocation))
                Dim arrDDLa As ArrayList = New StandardCodeFacade(User).Retrieve(criteriasa)
                Dim objStandardCodea As New StandardCode
                If Not IsNothing(arrDDLa) AndAlso arrDDLa.Count > 0 Then
                    objStandardCodea = CType(arrDDLa(0), StandardCode)
                    strLokasi = objStandardCodea.ValueDesc
                End If


                ws.Cells(idx + 4, 6).Value = strLokasi
                ws.Cells(idx + 4, 7).Value = strTipe
                ws.Cells(idx + 4, 8).Value = strKategori
                ws.Cells(idx + 4, 9).Value = strBodyPaint
                ws.Cells(idx + 4, 10).Value = strStatus

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

#End Region

#Region "Event Handlers"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        If Not IsPostBack Then

            hdnMCPConfirmation.Value = -1
            ActivateUserPrivilege()
            InitData()
            'ViewState.Add("vsProcess", "Insert")
            ReadData()
            dtgStallMaster.CurrentPageIndex = 0
            BindPage(dtgStallMaster.CurrentPageIndex)
        End If
        txtKodeDealer.Text = HiddenField1.Value
        txtNamaDealer.Text = HiddenField2.Value
    End Sub

    Private Sub dtgStallMaster_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgStallMaster.ItemCommand
        If e.CommandName = "lnkDetail" Then
            lblSearchDealer.Visible = False
            ViewState.Add("vsProcess", "Edit")
            Dim lbNo As Label = e.Item.FindControl("lbNo")
            HiddenField3.Value = e.Item.Cells(0).Text
            Dim lbKodeDealer As Label = e.Item.FindControl("lbKodeDealer")
            Dim lbKodeStall As Label = e.Item.FindControl("lbKodeStall")
            Dim lbKodeStallDealer As Label = e.Item.FindControl("lbKodeStallDealer")
            Dim lbNamaStall As Label = e.Item.FindControl("lbNamaStall")
            Dim lbLokasiStall As Label = e.Item.FindControl("lbLokasiStall")
            Dim lbTipeStall As Label = e.Item.FindControl("lbTipeStall")
            Dim lbKategoriStall As Label = e.Item.FindControl("lbKategoriStall")
            Dim lbBodyPaint As Label = e.Item.FindControl("lbBodyPaint")
            Dim lbStatus As Label = e.Item.FindControl("lbStatus")
            Dim lnkDetail As LinkButton = e.Item.FindControl("lnkDetail")

            txtKodeDealer.Text = lbKodeDealer.Text
            HiddenField1.Value = lbKodeDealer.Text
            txtKodeStall.Text = lbKodeStall.Text
            txtKodeStallDealer.Text = lbKodeStallDealer.Text
            txtNamaStall.Text = lbNamaStall.Text

            ddlStatus.SelectedValue = GetStdCodeID("StallMaster.Status", lbStatus.Text)
            ddlBodyPaint.SelectedValue = GetStdCodeID("StallMaster.IsBodyPaint", lbBodyPaint.Text)
            ddlTipeStall.SelectedValue = GetStdCodeID("StallMaster.TipeStall", lbTipeStall.Text)
            ddlKategoriStall.SelectedValue = GetStdCodeID("StallMaster.KategoriStall", lbKategoriStall.Text)
            ddlLokasiStall.SelectedValue = GetStdCodeID("StallMaster.LokasiStall", lbLokasiStall.Text)

            Dim objDealer As Dealer = New Dealer
            objDealer = New DealerFacade(User).GetDealer(txtKodeDealer.Text)

            txtNamaDealer.Text = objDealer.DealerName

            If (isDealerDMS = True) Then
                lnkDetail.Visible = False
            End If

        End If
    End Sub

    Private Sub dtgStallMaster_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgStallMaster.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblNo As Label = CType(e.Item.FindControl("lbNo"), Label)
            Dim lnkDetail As LinkButton = CType(e.Item.FindControl("lnkDetail"), LinkButton)
            Dim lbKodeDealer As Label = CType(e.Item.FindControl("lbKodeDealer"), Label)
            Dim lbLokasiStall As Label = CType(e.Item.FindControl("lbLokasiStall"), Label)
            Dim lbTipeStall As Label = CType(e.Item.FindControl("lbTipeStall"), Label)
            Dim lbKategoriStall As Label = CType(e.Item.FindControl("lbKategoriStall"), Label)
            Dim lbBodyPaint As Label = CType(e.Item.FindControl("lbBodyPaint"), Label)
            Dim lbStatus As Label = CType(e.Item.FindControl("lbStatus"), Label)

            lblNo.Text = (dtgStallMaster.CurrentPageIndex * dtgStallMaster.PageSize + e.Item.ItemIndex + 1).ToString()  '-- Column No
            lbBodyPaint.Text = GetStdCodeDesc("StallMaster.IsBodyPaint", lbBodyPaint.Text)
            lbStatus.Text = GetStdCodeDesc("StallMaster.Status", lbStatus.Text)
            lbTipeStall.Text = GetStdCodeDesc("StallMaster.TipeStall", lbTipeStall.Text)
            lbKategoriStall.Text = GetStdCodeDesc("StallMaster.KategoriStall", lbKategoriStall.Text)
            lbLokasiStall.Text = GetStdCodeDesc("StallMaster.LokasiStall", lbLokasiStall.Text)

            Dim objDealer As Dealer = CType(sessHelp.GetSession("DEALER"), Dealer)
            Dim objUser As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)

            Dim dealerSystems As DealerSystems = New DealerSystemsFacade(User).RetrieveByDealerCode(objDealer.DealerCode)

            If Not IsNothing(dealerSystems) AndAlso dealerSystems.SystemID <> 1 Then
                lnkDetail.Visible = False
            Else
                If Not objDealer Is Nothing Then
                    If (objDealer.DealerCode = lbKodeDealer.Text) Then
                        lnkDetail.Visible = True
                    Else
                        Dim criterias3 As New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias3.opAnd(New Criteria(GetType(Dealer), "ParentDealerID", MatchType.Exact, objDealer.ID))
                        criterias3.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.Exact, lbKodeDealer.Text))
                        Dim arrDDL As ArrayList = New DealerFacade(User).Retrieve(criterias3)
                        If arrDDL.Count = 0 Then
                            lnkDetail.Visible = False
                        Else
                            lnkDetail.Visible = True
                        End If

                    End If
                End If
            End If

        End If
    End Sub

    Protected Sub txtKodeDealer_TextChanged(sender As Object, e As EventArgs)
        If (txtKodeDealer.Text <> "") Then
            Dim objStallMaster As StallMaster = New StallMaster
            objStallMaster.Dealer = New DealerFacade(User).GetDealer(txtKodeDealer.Text)
            txtNamaDealer.Text = objStallMaster.Dealer.DealerName
        End If
    End Sub

    Protected Sub HiddenField1_ValueChanged(sender As Object, e As EventArgs)

    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs)
        ReadData()
        dtgStallMaster.CurrentPageIndex = 0
        BindPage(dtgStallMaster.CurrentPageIndex)
    End Sub

    Private Sub dtgStallMaster_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgStallMaster.PageIndexChanged
        '-- Change datagrid page

        dtgStallMaster.CurrentPageIndex = e.NewPageIndex
        BindPage(e.NewPageIndex)

    End Sub

    Protected Sub btnBatal_Click(sender As Object, e As EventArgs)
        ClearData()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)
        If Not Page.IsValid Then
            Return
        End If
        'Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.StallMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        Dim intStallRealTime = GetStallRealTime()
        Dim intMaxStallRealTime = GetMaxStallRealTime()

        Dim objDealer As Dealer = CType(sessHelp.GetSession("DEALER"), Dealer)
        Dim objUser As UserInfo = CType(sessHelp.GetSession("LOGINUSERINFO"), UserInfo)
        ' Dim mode As String = "add"
        If Not objDealer Is Nothing Then
            If Not objDealer.DealerGroup Is Nothing Then
                If objDealer.DealerCode <> txtKodeDealer.Text Then
                    Dim criterias3 As New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias3.opAnd(New Criteria(GetType(Dealer), "ParentDealerID", MatchType.Exact, objDealer.ID))
                    criterias3.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.Exact, txtKodeDealer.Text))
                    Dim arrDDL As ArrayList = New DealerFacade(User).Retrieve(criterias3)
                    If arrDDL.Count = 0 Then
                        MessageBox.Show("Tidak dapat menyimpan Stall yang tidak sesuai dengan dealer anda.")
                        Return
                    End If
                End If
            End If
            'txtKodeDealer.Text = objDealer.DealerCode
            'txtNamaDealer.Text = objDealer.DealerName

        End If
        txtKodeDealer.Text = HiddenField1.Value
        If (txtKodeDealer.Text = "" Or txtKodeStallDealer.Text = "" Or txtNamaStall.Text = "" Or ddlBodyPaint.SelectedIndex = 0 Or ddlKategoriStall.SelectedIndex = 0 Or ddlLokasiStall.SelectedIndex = 0 Or ddlStatus.SelectedIndex = 0 Or ddlTipeStall.SelectedIndex = 0) Then
            MessageBox.Show("Semua Data Wajib Diisi.")
            Return
        End If

        If (CType(ViewState("vsProcess"), String) = "Edit") Then
            Dim intStallType As Integer = GetStallType(HiddenField3.Value)
            If (ddlStatus.SelectedValue = 0) Then
                If intStallType <> intStallRealTime Then
                    If ddlTipeStall.SelectedValue = intStallRealTime Then
                        'If sessHelp.GetSession("isDealerPiloting") = True Then  
                        If intMaxStallRealTime > 0 Then
                            If (AvailableRealTimeStall(intStallRealTime) >= intMaxStallRealTime) Then
                                MessageBox.Show("Tidak dapat menambahkan Stall Real Time Service lagi. Batas maksimal stall Real Time Service yang dapat dibuat tidak boleh lebih dari " & intMaxStallRealTime & ".")
                                Exit Sub
                            End If
                            'Else
                            '    MessageBox.Show("Tidak dapat menyimpan sebagai Stall Real Time Service. Dealer anda belum melakukan aktivasi Piloting Stall Master.")
                            '    Exit Sub
                            'End If
                        Else
                            MessageBox.Show("Tidak dapat menambahkan Stall Real Time Service lagi. Batas maksimal stall Real Time Service yang dapat dibuat tidak boleh lebih dari " & intMaxStallRealTime & ".")
                            Exit Sub
                        End If
                    End If
                Else
                    If ddlTipeStall.SelectedValue = intStallRealTime Then
                        'If sessHelp.GetSession("isDealerPiloting") = True Then
                        If intMaxStallRealTime > 0 Then
                            If (AvailableRealTimeStall(intStallRealTime) >= intMaxStallRealTime) Then
                                MessageBox.Show("Tidak dapat menambahkan Stall Real Time Service lagi. Batas maksimal stall Real Time Service yang dapat dibuat tidak boleh lebih dari " & intMaxStallRealTime & ".")
                                Exit Sub
                            End If
                            'Else
                            '    MessageBox.Show("Tidak dapat menyimpan sebagai Stall Real Time Service. Dealer anda belum melakukan aktivasi Piloting Stall Master.")
                            '    Exit Sub
                            'End If
                        Else
                            MessageBox.Show("Tidak dapat menambahkan Stall Real Time Service lagi. Batas maksimal stall Real Time Service yang dapat dibuat tidak boleh lebih dari " & intMaxStallRealTime & ".")
                            Exit Sub
                        End If
                    End If
                End If
            End If
            
            Dim objStallMasters As StallMaster = New StallMaster
            Dim criteriass As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.StallMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteriass.opAnd(New Criteria(GetType(StallMaster), "StallCode", MatchType.Exact, txtKodeStall.Text))
            Dim arrStallMasters As ArrayList = New StallMasterFacade(User).Retrieve(criteriass)
            If (arrStallMasters.Count > 0) Then
                For Each item As StallMaster In arrStallMasters
                    If (txtKodeStallDealer.Text <> item.StallCodeDealer) Then
                        Dim objStallMaster As StallMaster = New StallMaster
                        objStallMaster.Dealer = New DealerFacade(User).GetDealer(txtKodeDealer.Text)
                        Dim criteriasa As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.StallMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criteriasa.opAnd(New Criteria(GetType(StallMaster), "Dealer.ID", MatchType.Exact, objStallMaster.Dealer.ID))
                        criteriasa.opAnd(New Criteria(GetType(StallMaster), "StallCodeDealer", MatchType.Exact, txtKodeStallDealer.Text))
                        Dim arrStallMaster As ArrayList = New StallMasterFacade(User).Retrieve(criteriasa)
                        If (arrStallMaster.Count > 0) Then
                            MessageBox.Show("Kode Stall Dealer sudah ada. Harap input dengan Kode Stall Dealer lainnya.")
                            Exit Sub
                        End If
                    End If
                Next
            End If
            'If (hdnValid.Value = 1) Then
            '    If (hdnMCPConfirmation.Value = -1) Then
            '        MessageBox.Confirm("Ubah Status jadi Tidak Aktif?", "hdnMCPConfirmation")
            '        Return
            '        'Exit Sub
            '        'Else
            '        '    UpdateModel()
            '        '    hdnValid.Value = 0
            '        '    hdnMCPConfirmation.Value = -1
            '    End If
            'Else
            '    UpdateModel()
            '    hdnValid.Value = 0
            '    hdnMCPConfirmation.Value = -1
            'End If

            'If (hdnMCPConfirmation.Value = 1) Then
            '    UpdateModel()
            '    hdnValid.Value = 0
            '    hdnMCPConfirmation.Value = -1
            'End If
            If (hdnValid.Value = 1) Then
                Dim confMsg As String = String.Empty
                If hdConfirm.Value = "-1" Then
                    confMsg = "Ubah Status jadi Tidak Aktif?"
                    RegisterStartupScript("Confirm", String.Format("<script>ShowConfirm('{0}', 'btnSave');</script>", confMsg))
                    Return
                Else
                    hdConfirm.Value = "-1"
                End If
            End If
            UpdateModel()
            hdnValid.Value = 0
        Else
            If ddlStatus.SelectedValue = 0 Then
                If ddlTipeStall.SelectedValue = intStallRealTime Then
                    'If sessHelp.GetSession("isDealerPiloting") = True Then
                    If ddlStatus.SelectedValue = 0 Then
                        If intMaxStallRealTime > 0 Then
                            If (AvailableRealTimeStall(intStallRealTime) >= intMaxStallRealTime) Then
                                MessageBox.Show("Tidak dapat menambahkan Stall Real Time Service lagi. Batas maksimal stall Real Time Service yang dapat dibuat tidak boleh lebih dari " & intMaxStallRealTime & ".")
                                Exit Sub
                            End If
                        Else
                            MessageBox.Show("Tidak dapat menambahkan Stall Real Time Service lagi. Batas maksimal stall Real Time Service yang dapat dibuat tidak boleh lebih dari " & intMaxStallRealTime & ".")
                            Exit Sub
                        End If
                    End If
                    'Else
                    '    MessageBox.Show("Tidak dapat menyimpan sebagai Stall Real Time Service. Dealer anda belum melakukan aktivasi Piloting Stall Master.")
                    '    Exit Sub
                    'End If
                End If
            End If
            Dim objStallMaster As StallMaster = New StallMaster
            objStallMaster.Dealer = New DealerFacade(User).GetDealer(txtKodeDealer.Text)
            Dim criteriasa As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.StallMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteriasa.opAnd(New Criteria(GetType(StallMaster), "Dealer.ID", MatchType.Exact, objStallMaster.Dealer.ID))
            criteriasa.opAnd(New Criteria(GetType(StallMaster), "StallCodeDealer", MatchType.Exact, txtKodeStallDealer.Text))
            Dim arrStallMaster As ArrayList = New StallMasterFacade(User).Retrieve(criteriasa)
            If (arrStallMaster.Count > 0) Then
                MessageBox.Show("Kode Stall Dealer sudah ada. Harap input dengan Kode Stall Dealer lainnya.")
                Exit Sub
            End If
            InsertModel()
            hdnValid.Value = 0
        End If
        ClearData()
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs)
        Dim arrStallMaster As ArrayList = CType(sessHelp.GetSession("StallMasterList"), ArrayList)
        'Dim aStatus As New ArrayList
        If arrStallMaster.Count <> 0 Then
            '   DoDownload(arrStallMaster)
            SetDownload()
        End If
    End Sub

    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs)
        If (ddlStatus.SelectedValue = 1) Then
            hdnValid.Value = 1
        Else
            hdnValid.Value = 0
        End If
    End Sub

#End Region
End Class