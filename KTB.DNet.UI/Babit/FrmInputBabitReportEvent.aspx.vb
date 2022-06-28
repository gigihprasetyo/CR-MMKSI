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
#End Region

Public Class FrmInputBabitReportEvent
    Inherits System.Web.UI.Page

#Region "Custom Variables Declaration"

    Dim objDealer As New Dealer
    Dim objLoginUser As UserInfo
    Dim objBabitReportHeader As BabitReportHeader

    Private arlBabitReportDisplayAndTarget As New ArrayList
    Private arrBabitReportEventDtl As New ArrayList
    Private arrBabitReportEventDoc As New ArrayList
    Private arrBabitReportEventAct As New ArrayList

    Private arlDeleteBabitReportEventDtl As New ArrayList
    Private arlDeleteBabitReportDoc As New ArrayList
    Private arlDeleteBabitReportAct As New ArrayList

    Const sessBabitReportHdr As String = "FrmInputBabitReportEvent.sessDataBabitReportHdr"
    Const sessBabitReportDoc As String = "FrmInputBabitReportEvent.sessDataBabitReportDoc"
    Const sessBabitReportAct As String = "FrmInputBabitReportEvent.sessDataBabitReportAct"
    Const sessBabitReportEventDtl As String = "FrmInputBabitReportEvent.sessDataBabitReportEventDtl"
    Const sessBabitEventDisplayTarget As String = "FrmInputBabitReportEvent.sessBabitEventDisplayTarget"

    Const sessDeleteBabitReportDoc As String = "FrmInputBabitReportEvent.sessDeleteDataBabitReportDoc"
    Const sessDeleteBabitReportAct As String = "FrmInputBabitReportEvent.sessDeleteDataBabitReportAct"
    Const sessDeleteBabitReportEventDtl As String = "FrmInputBabitReportEvent.sessDeleteDataBabitReportEventDtl"

    Private MAX_FILE_SIZE As Integer = 5120000
    Private TempDirectory As String
    Private TargetDirectory As String
    Private sesHelper As New SessionHelper
    Private intBabitParameterHeaderID As Integer = 0
    Private intActivityType As Integer = 0
    Private intItemIndex As Integer = 0
    Private Const strTypeCode As String = "'B', 'E'"
    Private Const strCategory As String = "EnumBabit.BabitParameterCategory"
    Private Const strValueCodeActivity As String = "Aktivitas"
    Private strVechileTypeKind As String = ""
    Dim arrBabitSPKList As New ArrayList
    Dim Mode As String = "New"

    Private Property SesDealer() As Dealer
        Get
            Return CType(sesHelper.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)
            sesHelper.SetSession("DEALER", Value)
        End Set
    End Property
#End Region

#Region "Custom Methods"

    Private Sub enabledProperty(ByVal enb As Boolean)
        Me.icPeriodStart.Enabled = enb
        Me.icPeriodEnd.Enabled = enb
        Me.txtAttendeeQty.Enabled = enb
        Me.txtBabitRegNumber.Enabled = enb
        Me.txtInvitationQty.Enabled = enb
        Me.txtLocationName.Enabled = enb
        Me.txtNotes.Enabled = enb
        Me.ddlKota.Enabled = enb
        Me.btnSave.Visible = enb

        Me.dgBabitReportDocument.ShowFooter = enb
        Me.dgBabitReportEvent.ShowFooter = enb
        Me.dgBabitReportEventActivity.ShowFooter = enb
        Me.dgBabitReportDocument.Columns(dgBabitReportDocument.Columns.Count - 1).Visible = enb
        Me.dgBabitReportEvent.Columns(dgBabitReportEvent.Columns.Count - 1).Visible = enb
        Me.dgBabitReportEventActivity.Columns(dgBabitReportEventActivity.Columns.Count - 1).Visible = enb
    End Sub

    Private Sub BindBabitMasterEventType()
        With ddlBabitMasterEventType
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

    Private Sub BindddlProvince()
        ddlKota.Items.Clear()
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
            Dim criterias2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.City), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.City), "Province.ID", MatchType.Exact, ProvinceID))
            Dim arlCity As ArrayList = New CityFacade(User).Retrieve(criterias2)
            If Not IsNothing(arlCity) AndAlso arlCity.Count > 0 Then
                For Each c As City In arlCity
                    ddlKota.Items.Add(New ListItem(c.CityName, c.ID))
                Next
            End If
        End If
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

        BindGridBabitReportEventDtl()
        BindGridBabitReportEventAct()
    End Sub

    Private Sub ClearAll()
        hdnBabitReportHeaderID.Value = ""
        hdnBabitHeaderID.Value = ""
        hdnDealerID.Value = ""
        lblDealerBranchCodeName.Text = ""
        lblDealerCodeName.Text = ""
        lblAreaName.Text = ""
        hdnAreaName.Value = ""
        txtBabitRegNumber.Text = ""
        txtNoSurat.Text = ""
        icPeriodStart.Value = Date.Now
        icPeriodEnd.Value = Date.Now
        ddlBabitMasterEventType.SelectedIndex = 0
        txtLocationName.Text = ""
        ddlProvinsi.SelectedIndex = 0
        ddlKota.SelectedIndex = 0

        txtInvitationQty.Text = "0"
        txtAttendeeQty.Text = "0"
        txtNotes.Text = ""

        sesHelper.SetSession(sessBabitReportEventDtl, New ArrayList)
        sesHelper.SetSession(sessBabitReportDoc, New ArrayList)
        sesHelper.SetSession(sessBabitReportAct, New ArrayList)

        BindGridBabitReportEventDtl()
        BindGridBabitReportEventDoc()
        BindGridBabitReportEventAct()
        BindGridBabitEventSPK(txtBabitRegNumber.Text)
        BindGridBabitEventSPKProspek(txtBabitRegNumber.Text)

    End Sub

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
                For Each obj As BabitReportDocument In AttachmentCollection
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

    Sub GetDealerData(ByVal oDealer As Dealer)
        lblDealerCodeName.Visible = True
        lblDealerCodeName.Text = oDealer.DealerCode & " / " & oDealer.DealerName
        hdnDealerID.Value = oDealer.ID
    End Sub

    Sub BindGridBabitEventSPK(strEventRegNumber As String)
        Dim arrBabitSPKListFinal As ArrayList = New ArrayList
        If strEventRegNumber.Trim <> "" Then
            Dim dSBabitSPKList As DataSet = New BabitHeaderFacade(User).RetrieveFromSPSPK(strEventRegNumber)
            Dim _babitHeader As New BabitHeader
            Dim row As DataRow
            Dim i As Integer = 0
            arrBabitSPKList = New ArrayList
            For i = 0 To dSBabitSPKList.Tables(0).Rows.Count - 1
                row = dSBabitSPKList.Tables(0).Rows(i)
                Try
                    _babitHeader = New BabitHeader
                    _babitHeader.ID = row("ID")
                    _babitHeader.BabitRegNumber = row("BabitRegNumber")
                    _babitHeader.VechileTypeKind = row("VechileTypeKind")
                    _babitHeader.VechileTypeName = row("VechileTypeName")
                    _babitHeader.DealerCode = row("DealerCode")
                    _babitHeader.DealerName = row("DealerName")
                    _babitHeader.QtyUnit = row("QtyUnit")
                    arrBabitSPKList.Add(_babitHeader)

                Catch ex As Exception
                End Try
            Next

            Dim dataList As ArrayList = New ArrayList
            dataList = New System.Collections.ArrayList(
                            (From obj As BabitHeader In arrBabitSPKList.OfType(Of BabitHeader)()
                                Where obj.DealerName <> "Total Unit :"
                                Order By obj.VechileTypeKind, obj.DealerCode
                                Select obj).ToList())

            Dim oBabitHeader As New BabitHeader
            Dim strVechileTypeKind As String = ""

            For j As Integer = 0 To dataList.Count - 1
                Dim objBabitEvent As BabitHeader = CType(dataList(j), BabitHeader)
                If j = 0 Then
                    strVechileTypeKind = objBabitEvent.VechileTypeKind
                End If
                If strVechileTypeKind <> objBabitEvent.VechileTypeKind Then
                    oBabitHeader = New BabitHeader
                    oBabitHeader.VechileTypeKind = strVechileTypeKind
                    oBabitHeader.DealerName = "Total Unit :"
                    oBabitHeader.TotalUnit = GetTotalUnitByVechileTypeKind(strVechileTypeKind)
                    arrBabitSPKListFinal.Add(oBabitHeader)
                    strVechileTypeKind = objBabitEvent.VechileTypeKind
                End If

                arrBabitSPKListFinal.Add(objBabitEvent)
                If j = dataList.Count - 1 Then
                    oBabitHeader = New BabitHeader
                    oBabitHeader.BabitRegNumber = objBabitEvent.BabitRegNumber
                    oBabitHeader.VechileTypeKind = objBabitEvent.VechileTypeKind
                    oBabitHeader.DealerName = "Total Unit :"
                    oBabitHeader.TotalUnit = GetTotalUnitByVechileTypeKind(strVechileTypeKind)
                    arrBabitSPKListFinal.Add(oBabitHeader)
                End If
            Next
        End If

        If arrBabitSPKListFinal.Count > 0 Then
            dgBabitEventSPK.DataSource = arrBabitSPKListFinal
        Else
            dgBabitEventSPK.DataSource = New ArrayList
        End If
        dgBabitEventSPK.DataBind()
    End Sub

    'Sub BindGridBabitEventSPK(strEventRegNumber As String)
    '    Dim arrEventSPKList As ArrayList = New BabitReportHeaderFacade(User).DoRetrieveDataSetBySPK(strEventRegNumber)
    '    If arrEventSPKList.Count > 0 Then
    '        dgBabitEventSPK.DataSource = arrEventSPKList
    '    Else
    '        dgBabitEventSPK.DataSource = New ArrayList
    '    End If
    '    dgBabitEventSPK.DataBind()
    'End Sub

    Sub BindGridBabitReportEventAct()
        arrBabitReportEventAct = CType(sesHelper.GetSession(sessBabitReportAct), ArrayList)
        If IsNothing(arrBabitReportEventAct) Then arrBabitReportEventAct = New ArrayList()

        CommonFunction.SortListControl(arrBabitReportEventAct, "BabitParameterDetail.ID", Sort.SortDirection.ASC)

        dgBabitReportEventActivity.DataSource = arrBabitReportEventAct
        dgBabitReportEventActivity.DataBind()
        sesHelper.SetSession(sessBabitReportAct, arrBabitReportEventAct)
    End Sub

    Private Function GetTotalUnitByVechileTypeKind(ByVal strVechileTypeKind As String) As Integer
        Dim intSumTotalUnit As Integer = 0
        intSumTotalUnit = (From item As BabitHeader In arrBabitSPKList
                            Where item.VechileTypeKind = strVechileTypeKind And item.DealerName <> "Total Unit :"
                                Select (item.QtyUnit)).Sum()
        Return intSumTotalUnit
    End Function

    Sub BindGridBabitReportEventDoc()
        arrBabitReportEventDoc = CType(sesHelper.GetSession(sessBabitReportDoc), ArrayList)
        If IsNothing(arrBabitReportEventDoc) Then arrBabitReportEventDoc = New ArrayList()
        dgBabitReportDocument.DataSource = arrBabitReportEventDoc
        dgBabitReportDocument.DataBind()
    End Sub

    Sub BindGridBabitReportEventDtl()
        arrBabitReportEventDtl = CType(sesHelper.GetSession(sessBabitReportEventDtl), ArrayList)
        If IsNothing(arrBabitReportEventDtl) Then
            arrBabitReportEventDtl = GetArrayGridBabitReport(hdnBabitReportHeaderID.Value)
        End If

        Dim dataList As ArrayList = New ArrayList
        dataList = New System.Collections.ArrayList(
                        (From obj As BabitReportEventDetail In arrBabitReportEventDtl.OfType(Of BabitReportEventDetail)()
                            Where obj.Description <> "#"
                            Select obj).ToList())

        CommonFunction.SortListControl(dataList, "BabitParameterDetail.BabitParameterHeader.ID", Sort.SortDirection.ASC)

        Dim objBabitParamDtl As New BabitParameterDetail
        Dim oBabitReportEventDetail As New BabitReportEventDetail
        Dim intBabitParameterHeaderID As Integer = 0
        Dim arlEvent2 As ArrayList = New ArrayList

        For i As Integer = 0 To dataList.Count - 1
            Dim objBabitEvent As BabitReportEventDetail = CType(dataList(i), BabitReportEventDetail)
            If i = 0 Then
                intBabitParameterHeaderID = objBabitEvent.BabitParameterDetail.BabitParameterHeader.ID
            End If
            If intBabitParameterHeaderID <> objBabitEvent.BabitParameterDetail.BabitParameterHeader.ID Then
                oBabitReportEventDetail = New BabitReportEventDetail
                oBabitReportEventDetail.BabitReportHeader = New BabitReportHeader

                objBabitParamDtl = New BabitParameterDetail
                Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitParameterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(BabitParameterDetail), "BabitParameterHeader.ID", MatchType.Exact, intBabitParameterHeaderID))
                Dim array As ArrayList = New BabitParameterDetailFacade(User).Retrieve(criterias)
                If Not IsNothing(array) AndAlso array.Count > 0 Then
                    objBabitParamDtl = CType(array(0), BabitParameterDetail)
                End If
                oBabitReportEventDetail.BabitParameterDetail = objBabitParamDtl
                oBabitReportEventDetail.Description = "#"
                arlEvent2.Add(oBabitReportEventDetail)
                intBabitParameterHeaderID = objBabitEvent.BabitParameterDetail.BabitParameterHeader.ID
            End If

            arlEvent2.Add(objBabitEvent)
        Next
        CommonFunction.SortListControl(arlEvent2, "BabitParameterDetail.BabitParameterHeader.ID", Sort.SortDirection.ASC)

        dgBabitReportEvent.DataSource = arlEvent2
        dgBabitReportEvent.DataBind()
        sesHelper.SetSession(sessBabitReportEventDtl, arlEvent2)
    End Sub

    Private Function GetArrayGridBabitReport(ByVal _intBabitReportHeaderID As Integer) As ArrayList
        Dim arr As ArrayList = New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitReportEventDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BabitReportEventDetail), "BabitReportHeader.ID", MatchType.Exact, _intBabitReportHeaderID))
        arr = New BabitReportEventDetailFacade(User).Retrieve(criterias)
        If IsNothing(arr) Then arr = New ArrayList
        Return arr
    End Function

    Private Function ValidateData() As String
        Dim sb As StringBuilder = New StringBuilder

        If txtBabitRegNumber.Text.Trim = "" Then
            sb.Append("Nomor Reg Babit harus Diisi\n")
        End If
        If txtLocationName.Text.Trim = "" Then
            sb.Append("Nama Lokasi harus Diisi\n")
        End If
        If (ddlProvinsi.SelectedValue.Trim = String.Empty) Then
            sb.Append("Provinsi Harus Diisi\n")
        End If
        If (ddlKota.SelectedValue.Trim = String.Empty) Then
            sb.Append("Kota Harus Diisi\n")
        End If
        If txtInvitationQty.Text.Trim = "" OrElse txtInvitationQty.Text.Trim = "0" Then
            sb.Append("Jumlah Undangan Event harus Diisi\n")
        End If
        If txtAttendeeQty.Text.Trim = "" OrElse txtAttendeeQty.Text.Trim = "0" Then
            sb.Append("Jumlah Kehadiran harus Diisi\n")
        End If

        If (icPeriodStart.Value > icPeriodEnd.Value) Then
            sb.Append("Tanggal awal harus lebih kecil atau sama dengan tanggal akhir\n")
        End If

        If (sesHelper.GetSession(sessBabitReportAct) Is Nothing) Then
            sb.Append("Data Aktivitas Laporan belum ada\n")
        Else
            If CType(sesHelper.GetSession(sessBabitReportAct), ArrayList).Count = 0 Then
                sb.Append("Data Aktivitas Laporan belum ada\n")
            End If
        End If
        If (sesHelper.GetSession(sessBabitReportDoc) Is Nothing) Then
            sb.Append("Data Lampiran Dokumen belum ada\n")
        Else
            If CType(sesHelper.GetSession(sessBabitReportDoc), ArrayList).Count = 0 Then
                sb.Append("Data Lampiran Dokumen belum ada\n")
            End If
        End If
        If (sesHelper.GetSession(sessBabitReportEventDtl) Is Nothing) Then
            sb.Append("Data Rincian Kegiatan belum ada\n")
        Else
            If CType(sesHelper.GetSession(sessBabitReportEventDtl), ArrayList).Count = 0 Then
                sb.Append("Data Rincian Kegiatan belum ada\n")
            End If
        End If

        If txtInvitationQty.Text.Trim = "" Then
            txtInvitationQty.Text = "0"
        End If
        If txtAttendeeQty.Text.Trim = "" Then
            txtAttendeeQty.Text = "0"
        End If

        Return sb.ToString()
    End Function

    Private Sub UploadAttachment(ByVal ObjAttachment As BabitReportDocument, ByVal TargetPath As String)
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
            For Each obj As BabitReportDocument In AttachmentCollection
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

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Mode = "New" Then
            If Not SecurityProvider.Authorize(Context.User, SR.BABIT_Input_Event_Laporan_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=BABIT - INPUT LAPORAN BABIT EVENT")
            End If
        ElseIf Mode = "Edit" Then
            If Not SecurityProvider.Authorize(Context.User, SR.BABIT_Daftar_Laporan_Edit_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=BABIT - EDIT LAPORAN BABIT EVENT")
            End If
        Else
            If Not SecurityProvider.Authorize(Context.User, SR.BABIT_Daftar_Laporan_Display_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=BABIT - DISPLAY LAPORAN BABIT EVENT")
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
            objDealer = CType(sesHelper.GetSession("DEALER"), Dealer)
        Else
            objDealer = CType(sesHelper.GetSession("FrmInputBabitReportEvent.DEALER"), Dealer)
        End If
        objLoginUser = CType(sesHelper.GetSession("LOGINUSERINFO"), UserInfo)
        TargetDirectory = KTB.DNet.Lib.WebConfig.GetValue("SAN")
        TempDirectory = Server.MapPath("") + "\..\DataTemp\"
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)

        If (Not IsPostBack) Then
            InitiateAuthorization()

            BindGridBabitReportEventAct()
            BindGridBabitReportEventDtl()
            BindGridBabitReportEventDoc()
            BindGridDisplayAndTarget()

            BindBabitMasterEventType()
            BindddlProvince()
            ClearAll()

            objBabitReportHeader = New BabitReportHeader
            If Not IsNothing(Request.QueryString("BabitReportHeaderID")) Then
                hdnBabitReportHeaderID.Value = Request.QueryString("BabitReportHeaderID")
                LoadDataBabitReport(hdnBabitReportHeaderID.Value)
            End If
            BindGridBabitEventSPK(txtBabitRegNumber.Text)
            BindGridBabitEventSPKProspek(txtBabitRegNumber.Text)
            GetDealerData(objDealer)
            lblPopUpBabitHeader.Attributes("onclick") = "ShowPPBabitHeaderSelection();"
            If Mode = "New" Then
                btnCetak.Visible = False
            End If
        End If
    End Sub

    Private Sub LoadDataBabitReport(intBabitReportHeaderID As Integer)
        Dim sSQL As String = String.Empty
        Dim objBabitReportEventDetail As BabitReportEventDetail
        Dim arrBabitReportEventDetail As ArrayList
        Dim arrBabitReportDocument As ArrayList

        objBabitReportHeader = New BabitReportHeaderFacade(User).Retrieve(intBabitReportHeaderID)
        If Not IsNothing(objBabitReportHeader) Then
            objDealer = objBabitReportHeader.Dealer
            sesHelper.SetSession("FrmInputBabitReportEvent.DEALER", objDealer)
            sesHelper.SetSession(sessBabitReportHdr, objBabitReportHeader)

            Dim objBabitHeader As BabitHeader = New BabitHeaderFacade(User).Retrieve(objBabitReportHeader.BabitHeader.ID)
            Me.hdnBabitHeaderID.Value = objBabitHeader.ID
            Me.txtBabitRegNumber.Text = objBabitHeader.BabitRegNumber
            btnGetInfoBabitHeader_Click(Nothing, Nothing)

            Me.icPeriodStart.Value = objBabitReportHeader.PeriodStart
            Me.icPeriodEnd.Value = objBabitReportHeader.PeriodEnd
            Me.txtAttendeeQty.Text = objBabitReportHeader.AttendeeQty
            Me.txtInvitationQty.Text = objBabitReportHeader.InvitationQty
            Me.txtNotes.Text = objBabitReportHeader.Notes

            sSQL = String.Empty
            sSQL += "select ID "
            sSQL += "from BabitParameterDetail "
            sSQL += "where BabitParameterHeaderID in ("
            sSQL += "select ID from BabitParameterHeader "
            sSQL += "where Status = 1 and ParameterCategory in ("
            sSQL += "select ValueID from StandardCode where Category = '" & strCategory & "' and ValueCode <> '" & strValueCodeActivity & "') "
            sSQL += "and BabitMasterEventTypeID in ("
            sSQL += "select ID from BabitMasterEventType where TypeCode in ('B','E')))"

            Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitReportEventDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitReportEventDetail), "BabitReportHeader.ID", MatchType.Exact, intBabitReportHeaderID))
            criterias.opAnd(New Criteria(GetType(BabitReportEventDetail), "BabitParameterDetail.ID", MatchType.InSet, "(" & sSQL & ")"))
            arrBabitReportEventDetail = New BabitReportEventDetailFacade(User).Retrieve(criterias)
            sesHelper.SetSession(sessBabitReportEventDtl, arrBabitReportEventDetail)
            BindGridBabitReportEventDtl()

            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(BabitReportDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(BabitReportDocument), "BabitReportHeader.ID", MatchType.Exact, intBabitReportHeaderID))
            arrBabitReportDocument = New BabitReportDocumentFacade(User).Retrieve(criterias2)
            sesHelper.SetSession(sessBabitReportDoc, arrBabitReportDocument)
            BindGridBabitReportEventDoc()

            sSQL = String.Empty
            sSQL += "select ID "
            sSQL += "from BabitParameterDetail "
            sSQL += "where BabitParameterHeaderID in ("
            sSQL += "select ID from BabitParameterHeader "
            sSQL += "where Status = 1 and ParameterCategory in ("
            sSQL += "select ValueID from StandardCode where Category = '" & strCategory & "' and ValueCode = '" & strValueCodeActivity & "') "
            sSQL += "and BabitMasterEventTypeID in ("
            sSQL += "select ID from BabitMasterEventType where TypeCode in ('B','E')))"

            Dim criterias3 As New CriteriaComposite(New Criteria(GetType(BabitReportEventDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias3.opAnd(New Criteria(GetType(BabitReportEventDetail), "BabitReportHeader.ID", MatchType.Exact, intBabitReportHeaderID))
            criterias3.opAnd(New Criteria(GetType(BabitReportEventDetail), "BabitParameterDetail.ID", MatchType.InSet, "(" & sSQL & ")"))
            arrBabitReportEventAct = New BabitReportEventDetailFacade(User).Retrieve(criterias3)
            sesHelper.SetSession(sessBabitReportAct, arrBabitReportEventAct)
            BindGridBabitReportEventAct()

            lblPopUpBabitHeader.Visible = False
            txtBabitRegNumber.Enabled = False

            Me.btnSave.Enabled = False
            Me.txtNoSurat.Enabled = False
            Me.icPeriodStart.Enabled = False
            Me.icPeriodEnd.Enabled = False

            Me.ddlKota.Enabled = False
            Me.ddlProvinsi.Enabled = False
            Me.txtInvitationQty.Enabled = False
            Me.txtNotes.Enabled = False
            Me.txtAttendeeQty.Enabled = False

            Me.dgBabitReportDocument.ShowFooter = False
            Me.dgBabitReportDocument.Columns(dgBabitReportDocument.Columns.Count - 1).Visible = False
            Me.dgBabitReportEvent.ShowFooter = False
            Me.dgBabitReportEvent.Columns(dgBabitReportEvent.Columns.Count - 1).Visible = False
            Me.dgBabitReportEventActivity.ShowFooter = False
            Me.dgBabitReportEventActivity.Columns(dgBabitReportEventActivity.Columns.Count - 1).Visible = False

            If Mode = "Edit" Then
                Me.btnSave.Enabled = True
                Me.icPeriodStart.Enabled = True
                Me.icPeriodEnd.Enabled = True

                Me.txtAttendeeQty.Enabled = True
                Me.txtInvitationQty.Enabled = True
                Me.txtNotes.Enabled = True

                Me.dgBabitReportDocument.ShowFooter = True
                Me.dgBabitReportDocument.Columns(dgBabitReportDocument.Columns.Count - 1).Visible = True
                Me.dgBabitReportEvent.ShowFooter = True
                Me.dgBabitReportEvent.Columns(dgBabitReportEvent.Columns.Count - 1).Visible = True
                Me.dgBabitReportEventActivity.ShowFooter = True
                Me.dgBabitReportEventActivity.Columns(dgBabitReportEventActivity.Columns.Count - 1).Visible = True
            End If
        End If
    End Sub

    Private Function GetFromSession(ByVal sObject As String) As Object
        If Session("DEALER") Is Nothing Then
            Response.Redirect("../SessionExpired.htm")
        Else
            Return Session(sObject)
        End If
    End Function

    Private Sub dgBabitReportEventActivity_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgBabitReportEventActivity.ItemCommand
        Dim lblActivityType As Label
        Dim ddlActivityType As DropDownList
        Dim lblDescription As Label
        Dim txtFDescription As TextBox
        Dim txtEDescription As TextBox

        objBabitReportHeader = CType(sesHelper.GetSession(sessBabitReportHdr), BabitReportHeader)
        arrBabitReportEventAct = CType(sesHelper.GetSession(sessBabitReportAct), ArrayList)
        If IsNothing(arrBabitReportEventAct) Then arrBabitReportEventAct = New ArrayList

        Select Case e.CommandName
            Case "add"
                ddlActivityType = CType(e.Item.FindControl("ddlFActivityType"), DropDownList)
                txtFDescription = CType(e.Item.FindControl("txtFDescription"), TextBox)
                If ddlActivityType.SelectedValue = "-1" Then
                    MessageBox.Show("Tipe Aktivitas harus diisi.")
                    Return
                End If
                If txtFDescription.Text.Trim = "" Then
                    MessageBox.Show("Deskripsi harus diisi.")
                    Return
                End If
                Dim objBabitReportEventDetail As BabitReportEventDetail = New BabitReportEventDetail
                objBabitReportEventDetail.BabitReportHeader = New BabitReportHeader
                objBabitReportEventDetail.BabitParameterDetail = New BabitParameterDetailFacade(User).Retrieve(CInt(ddlActivityType.SelectedValue))
                objBabitReportEventDetail.Description = txtFDescription.Text.Trim
                arrBabitReportEventAct.Add(objBabitReportEventDetail)

            Case "save" 'Update this datagrid item   
                ddlActivityType = CType(e.Item.FindControl("ddlEActivityType"), DropDownList)
                txtEDescription = CType(e.Item.FindControl("txtEDescription"), TextBox)
                If ddlActivityType.SelectedValue = "-1" Then
                    MessageBox.Show("Tipe Aktivitas harus diisi.")
                    Return
                End If
                If txtEDescription.Text.Trim = "" Then
                    MessageBox.Show("Deskripsi harus diisi.")
                    Return
                End If
                Dim objBabitReportEventDetail As BabitReportEventDetail = CType(arrBabitReportEventAct(e.Item.ItemIndex), BabitReportEventDetail)
                objBabitReportEventDetail.BabitReportHeader = objBabitReportHeader
                objBabitReportEventDetail.BabitParameterDetail = New BabitParameterDetailFacade(User).Retrieve(CInt(ddlActivityType.SelectedValue))
                objBabitReportEventDetail.Description = txtEDescription.Text.Trim()

                dgBabitReportEventActivity.EditItemIndex = -1
                dgBabitReportEventActivity.ShowFooter = True

            Case "edit" 'Edit mode activated
                dgBabitReportEventActivity.ShowFooter = False
                dgBabitReportEventActivity.EditItemIndex = e.Item.ItemIndex

            Case "delete" 'Delete this datagrid item 
                Try
                    Dim oBabitReportEventDetail As BabitReportEventDetail = CType(arrBabitReportEventAct(e.Item.ItemIndex), BabitReportEventDetail)
                    If oBabitReportEventDetail.ID > 0 Then
                        Dim arrDelete As ArrayList = CType(sesHelper.GetSession(sessDeleteBabitReportAct), ArrayList)
                        If IsNothing(arrDelete) Then arrDelete = New ArrayList
                        arrDelete.Add(oBabitReportEventDetail)
                        sesHelper.SetSession(sessDeleteBabitReportAct, arrDelete)
                    End If
                    arrBabitReportEventAct.RemoveAt(e.Item.ItemIndex)
                Catch
                End Try
            Case "cancel" 'Cancel Update this datagrid item 
                dgBabitReportEventActivity.EditItemIndex = -1
                dgBabitReportEventActivity.ShowFooter = True
        End Select

        sesHelper.SetSession(sessBabitReportAct, arrBabitReportEventAct)
        BindGridBabitReportEventAct()
        BindGridBabitReportEventDtl()
    End Sub

    Private Function GetDataAcivitas() As ArrayList
        Dim sSQL As String = String.Empty
        sSQL += "select ID,BabitParameterHeaderID,ParameterDetailName,RowStatus,CreatedBy,CreatedTime,LastUpdateBy,LastUpdateTime "
        sSQL += "from BabitParameterDetail "
        sSQL += "where BabitParameterHeaderID in ("
        sSQL += "select ID from BabitParameterHeader "
        sSQL += "where Status = 1 and ParameterCategory in ("
        sSQL += "select ValueID from StandardCode where Category = '" & strCategory & "' and ValueCode= '" & strValueCodeActivity & "') "
        sSQL += "and BabitMasterEventTypeID in ("
        sSQL += "select ID from BabitMasterEventType where TypeCode in ('B', 'E')))"

        Dim result As ArrayList = New BabitParameterDetailFacade(User).RetrieveSP(sSQL)
        If IsNothing(result) Then result = New ArrayList

        Return result
    End Function

    Private Sub dgBabitReportEventActivity_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgBabitReportEventActivity.ItemDataBound

        Dim lblActivityType As Label
        Dim ddlActivityType As DropDownList
        Dim lblDescription As Label
        Dim txtFDescription As TextBox
        Dim txtEDescription As TextBox

        Dim lbtnEdit As LinkButton
        Dim lbtnDelete As LinkButton

        If e.Item.ItemType = ListItemType.Footer Then
            ddlActivityType = CType(e.Item.FindControl("ddlFActivityType"), DropDownList)
            Dim arrDDL As ArrayList = GetDataAcivitas()
            With ddlActivityType
                .Items.Clear()
                .DataSource = arrDDL
                .DataTextField = "ParameterDetailName"
                .DataValueField = "ID"
                .DataBind()
                .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            End With
        End If

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim objBabitReportEventDetail As BabitReportEventDetail = CType(e.Item.DataItem, BabitReportEventDetail)
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dgBabitReportEventActivity.CurrentPageIndex * dgBabitReportEventActivity.PageSize)

            lblActivityType = CType(e.Item.FindControl("lblActivityType"), Label)
            lblDescription = CType(e.Item.FindControl("lblDescription"), Label)
            lblActivityType.Text = objBabitReportEventDetail.BabitParameterDetail.ParameterDetailName
            lblDescription.Text = objBabitReportEventDetail.Description
        End If

        If e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            Dim objBabitReportEventDetail As BabitReportEventDetail = CType(e.Item.DataItem, BabitReportEventDetail)
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dgBabitReportEventActivity.CurrentPageIndex * dgBabitReportEventActivity.PageSize)

            ddlActivityType = CType(e.Item.FindControl("ddlEActivityType"), DropDownList)
            Dim arrDDL As ArrayList = GetDataAcivitas()
            With ddlActivityType
                .Items.Clear()
                .DataSource = arrDDL
                .DataTextField = "ParameterDetailName"
                .DataValueField = "ID"
                .DataBind()
                .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
            End With
            ddlActivityType.SelectedValue = objBabitReportEventDetail.BabitParameterDetail.ID
        End If
    End Sub

    Private Sub dgBabitReportEvent_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgBabitReportEvent.ItemCommand
        Dim oBabitParameterDetail As New BabitParameterDetail
        Dim ddlECategoryBabitEvent As DropDownList
        Dim ddlFCategoryBabitEvent As DropDownList
        Dim ddlEJenisBabitEvent As DropDownList
        Dim ddlFJenisBabitEvent As DropDownList
        Dim lblCategoryBabitEvent As Label
        Dim lblJenisBabitEvent As Label
        Dim txtEDesc As TextBox
        Dim txtFDesc As TextBox
        Dim txtEQty As TextBox
        Dim txtFQty As TextBox

        objBabitReportHeader = CType(sesHelper.GetSession(sessBabitReportHdr), BabitReportHeader)
        arrBabitReportEventDtl = CType(sesHelper.GetSession(sessBabitReportEventDtl), ArrayList)

        Select Case e.CommandName
            Case "add"
                ddlFCategoryBabitEvent = CType(e.Item.FindControl("ddlFCategoryBabitEvent"), DropDownList)
                ddlFJenisBabitEvent = CType(e.Item.FindControl("ddlFJenisBabitEvent"), DropDownList)
                txtFDesc = CType(e.Item.FindControl("txtFDesc"), TextBox)
                txtFQty = CType(e.Item.FindControl("txtFQty"), TextBox)
                txtFDesc = CType(e.Item.FindControl("txtFDesc"), TextBox)

                If ddlFCategoryBabitEvent.SelectedValue = "-1" Then
                    MessageBox.Show("Kategori Kegiatan harus diisi.")
                    Return
                End If
                If ddlFJenisBabitEvent.SelectedValue = "-1" Then
                    MessageBox.Show("Jenis Kegiatan harus diisi.")
                    Return
                End If
                If txtFDesc.Text.Trim = "" Then
                    MessageBox.Show("Deskripsi Kegiatan harus diisi.")
                    Return
                End If
                oBabitParameterDetail = New BabitParameterDetailFacade(User).Retrieve(CInt(ddlFJenisBabitEvent.SelectedValue))

                Dim oBabitReportEventDetail As New BabitReportEventDetail
                oBabitReportEventDetail.BabitReportHeader = New BabitReportHeader
                oBabitReportEventDetail.BabitParameterDetail = oBabitParameterDetail
                oBabitReportEventDetail.Description = txtFDesc.Text.Trim
                arrBabitReportEventDtl.Add(oBabitReportEventDetail)

            Case "save" 'Update this datagrid item   
                ddlECategoryBabitEvent = CType(e.Item.FindControl("ddlECategoryBabitEvent"), DropDownList)
                ddlEJenisBabitEvent = CType(e.Item.FindControl("ddlEJenisBabitEvent"), DropDownList)
                txtEDesc = CType(e.Item.FindControl("txtEDesc"), TextBox)
                txtEQty = CType(e.Item.FindControl("txtEQty"), TextBox)

                If ddlECategoryBabitEvent.SelectedValue = "-1" Then
                    MessageBox.Show("Kategori Kegiatan harus diisi.")
                    Return
                End If
                If ddlEJenisBabitEvent.SelectedValue = "-1" Then
                    MessageBox.Show("Jenis Kegiatan harus diisi.")
                    Return
                End If
                If txtEDesc.Text.Trim = "" Then
                    MessageBox.Show("Deskripsi Kegiatan harus diisi.")
                    Return
                End If

                oBabitParameterDetail = New BabitParameterDetailFacade(User).Retrieve(CInt(ddlEJenisBabitEvent.SelectedValue))
                Dim oBabitReportEventDetail As BabitReportEventDetail = CType(arrBabitReportEventDtl(e.Item.ItemIndex), BabitReportEventDetail)
                oBabitReportEventDetail.BabitReportHeader = objBabitReportHeader
                oBabitReportEventDetail.BabitParameterDetail = oBabitParameterDetail
                oBabitReportEventDetail.Description = txtEDesc.Text.Trim()
                dgBabitReportEvent.EditItemIndex = -1
                dgBabitReportEvent.ShowFooter = True

            Case "edit" 'Edit mode activated
                dgBabitReportEvent.ShowFooter = False
                dgBabitReportEvent.EditItemIndex = e.Item.ItemIndex

            Case "delete" 'Delete this datagrid item 
                Try
                    Dim oDeleteBabitReportEventDetail As BabitReportEventDetail = CType(arrBabitReportEventDtl(e.Item.ItemIndex), BabitReportEventDetail)
                    If oDeleteBabitReportEventDetail.ID > 0 Then
                        Dim arrDelete As ArrayList = CType(sesHelper.GetSession(sessDeleteBabitReportEventDtl), ArrayList)
                        If IsNothing(arrDelete) Then arrDelete = New ArrayList
                        arrDelete.Add(oDeleteBabitReportEventDetail)
                        sesHelper.SetSession(sessDeleteBabitReportEventDtl, arrDelete)
                    End If
                    arrBabitReportEventDtl.RemoveAt(e.Item.ItemIndex)
                Catch
                End Try

            Case "cancel" 'Cancel Update this datagrid item 
                dgBabitReportEvent.EditItemIndex = -1
                dgBabitReportEvent.ShowFooter = True
        End Select

        sesHelper.SetSession(sessBabitReportEventDtl, arrBabitReportEventDtl)
        BindGridBabitReportEventDtl()
        BindGridBabitReportEventAct()
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

    Private Sub dgBabitReportEvent_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgBabitReportEvent.ItemDataBound
        Dim ddlECategoryBabitEvent As DropDownList
        Dim ddlFCategoryBabitEvent As DropDownList
        Dim ddlEJenisBabitEvent As DropDownList
        Dim ddlFJenisBabitEvent As DropDownList
        Dim lblCategoryBabitEvent As Label
        Dim lblJenisBabitEvent As Label
        Dim lblDescription As Label
        Dim lbtnEdit As LinkButton
        Dim lbtnDelete As LinkButton

        If e.Item.ItemType = ListItemType.Footer Then
            ddlFCategoryBabitEvent = CType(e.Item.FindControl("ddlFCategoryBabitEvent"), DropDownList)
            ddlFJenisBabitEvent = CType(e.Item.FindControl("ddlFJenisBabitEvent"), DropDownList)

            Dim strSql As String = String.Empty
            strSql = "select ValueID from StandardCode where Category = '" & strCategory & "' and ValueCode = 'RincianKegiatan'"
            Dim arrDDL As ArrayList = New ArrayList
            Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitParameterHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "BabitMasterEventType.TypeCode", MatchType.InSet, "('B', 'E')"))
            criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "ParameterCategory", MatchType.InSet, "(" & strSql & ")"))
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
            Dim oBERD As BabitReportEventDetail = CType(e.Item.DataItem, BabitReportEventDetail)

            If e.Item.ItemIndex = 0 Then
                ViewState("ItemIndex") = e.Item.ItemIndex
                intItemIndex = CType(ViewState("ItemIndex"), Integer)
                intBabitParameterHeaderID = oBERD.BabitParameterDetail.BabitParameterHeader.ID
            Else
                If intBabitParameterHeaderID <> oBERD.BabitParameterDetail.BabitParameterHeader.ID Then
                    ViewState("ItemIndex") = 0
                    intItemIndex = CType(ViewState("ItemIndex"), Integer)
                    intBabitParameterHeaderID = oBERD.BabitParameterDetail.BabitParameterHeader.ID
                Else
                    ViewState("ItemIndex") = CType(ViewState("ItemIndex"), Integer) + 1
                    intItemIndex = CType(ViewState("ItemIndex"), Integer)
                End If
            End If
            e.Item.Cells(0).Text = intItemIndex + 1 + (dgBabitReportEvent.CurrentPageIndex * dgBabitReportEvent.PageSize)

            lblCategoryBabitEvent = CType(e.Item.FindControl("lblCategoryBabitEvent"), Label)
            lblJenisBabitEvent = CType(e.Item.FindControl("lblJenisBabitEvent"), Label)
            lblDescription = CType(e.Item.FindControl("lblDescription"), Label)
            lbtnEdit = CType(e.Item.FindControl("lbtnEdit"), LinkButton)
            lbtnDelete = CType(e.Item.FindControl("lbtnDelete"), LinkButton)

            lblCategoryBabitEvent.Text = oBERD.BabitParameterDetail.BabitParameterHeader.ParameterName()
            lblJenisBabitEvent.Text = oBERD.BabitParameterDetail.ParameterDetailName()

            If lblDescription.Text.Trim.ToLower = "#" Then
                e.Item.Cells(0).Text = "No."
                e.Item.Cells(1).Text = "Kategori"
                e.Item.Cells(2).Text = "Jenis"
                e.Item.Cells(3).Text = "Deskripsi"
                e.Item.Cells(0).Style.Add("text-align", "center")
                e.Item.Cells(1).Style.Add("text-align", "center")
                e.Item.Cells(2).Style.Add("text-align", "center")
                e.Item.Cells(3).Style.Add("text-align", "center")
                e.Item.Cells(4).Style.Add("text-align", "center")
                e.Item.Cells(0).Style.Add("font-weight", "bold")
                e.Item.Cells(1).Style.Add("font-weight", "bold")
                e.Item.Cells(2).Style.Add("font-weight", "bold")
                e.Item.Cells(3).Style.Add("font-weight", "bold")
                e.Item.Cells(4).Style.Add("font-weight", "bold")
                e.Item.Cells(0).ForeColor = Color.White
                e.Item.Cells(1).ForeColor = Color.White
                e.Item.Cells(2).ForeColor = Color.White
                e.Item.Cells(3).ForeColor = Color.White
                e.Item.Cells(4).ForeColor = Color.White
                e.Item.Cells(0).BackColor = Color.FromName("C9A557")
                e.Item.Cells(1).BackColor = Color.FromName("C9A557")
                e.Item.Cells(2).BackColor = Color.FromName("C9A557")
                e.Item.Cells(3).BackColor = Color.FromName("C9A557")
                e.Item.Cells(4).BackColor = Color.FromName("C9A557")

                lblDescription.Attributes("style") = "display:none"
                lblJenisBabitEvent.Text = ""
                lbtnEdit.Attributes("style") = "display:none"
                lbtnDelete.Attributes("style") = "display:none"
                lblCategoryBabitEvent.Attributes("style") = "display:none"
                e.Item.Cells(0).Text = ""
            Else
                lblDescription.Attributes("style") = "display:table-row"
                lbtnEdit.Attributes("style") = "display:table-row"
                lbtnDelete.Attributes("style") = "display:table-row"
                lblCategoryBabitEvent.Attributes("style") = "display:table-row"
            End If
        End If

        If e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            Dim oBERD As BabitReportEventDetail = CType(e.Item.DataItem, BabitReportEventDetail)
            If e.Item.ItemIndex = 0 Then
                ViewState("ItemIndex") = e.Item.ItemIndex
                intItemIndex = CType(ViewState("ItemIndex"), Integer)
                intBabitParameterHeaderID = oBERD.BabitParameterDetail.BabitParameterHeader.ID
            Else
                If intBabitParameterHeaderID <> oBERD.BabitParameterDetail.BabitParameterHeader.ID Then
                    ViewState("ItemIndex") = 0
                    intItemIndex = CType(ViewState("ItemIndex"), Integer)
                    intBabitParameterHeaderID = oBERD.BabitParameterDetail.BabitParameterHeader.ID
                Else
                    ViewState("ItemIndex") = CType(ViewState("ItemIndex"), Integer) + 1
                    intItemIndex = CType(ViewState("ItemIndex"), Integer)
                End If
            End If
            e.Item.Cells(0).Text = intItemIndex + 1 + (dgBabitReportEvent.CurrentPageIndex * dgBabitReportEvent.PageSize)

            ddlECategoryBabitEvent = CType(e.Item.FindControl("ddlECategoryBabitEvent"), DropDownList)

            Dim strSql As String = String.Empty
            strSql = "select ValueID from StandardCode where Category = '" & strCategory & "' and ValueCode <> '" & strValueCodeActivity & "'"
            Dim arrDDL As ArrayList = New ArrayList
            Dim criterias As New CriteriaComposite(New Criteria(GetType(BabitParameterHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "BabitMasterEventType.TypeCode", MatchType.InSet, "('B', 'E')"))
            criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "ParameterCategory", MatchType.InSet, "(" & strSql & ")"))
            criterias.opAnd(New Criteria(GetType(BabitParameterHeader), "Status", MatchType.Exact, 1))
            arrDDL = New BabitParameterHeaderFacade(User).Retrieve(criterias)
            With ddlECategoryBabitEvent
                .Items.Clear()
                .DataSource = arrDDL
                .DataTextField = "ParameterName"
                .DataValueField = "ID"
                .DataBind()
                .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
                .SelectedValue = oBERD.BabitParameterDetail.BabitParameterHeader.ID
            End With

            ddlEJenisBabitEvent = CType(e.Item.FindControl("ddlEJenisBabitEvent"), DropDownList)
            arrDDL = New ArrayList
            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(BabitParameterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(BabitParameterDetail), "BabitParameterHeader.ID", MatchType.Exact, oBERD.BabitParameterDetail.BabitParameterHeader.ID))
            arrDDL = New BabitParameterDetailFacade(User).Retrieve(criterias2)
            With ddlEJenisBabitEvent
                .Items.Clear()
                .DataSource = arrDDL
                .DataTextField = "ParameterDetailName"
                .DataValueField = "ID"
                .DataBind()
                .Items.Insert(0, New ListItem("Silahkan Pilih", -1))
                .SelectedValue = oBERD.BabitParameterDetail.ID
            End With
        End If
    End Sub

    Private Sub dgBabitReportDocument_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgBabitReportDocument.ItemCommand
        Dim _arrDataUploadFile As ArrayList = CType(sesHelper.GetSession(sessBabitReportDoc), ArrayList)
        If IsNothing(_arrDataUploadFile) Then _arrDataUploadFile = New ArrayList

        Select Case e.CommandName
            Case "add" 'Insert New item to datagrid
                Dim FileUpload As HtmlInputFile = CType(e.Item.FindControl("UploadFile"), HtmlInputFile)
                Dim txtKeterangan As TextBox = CType(e.Item.FindControl("txtKeterangan"), TextBox)
                Dim ddlFCategoryEvent As DropDownList = CType(e.Item.FindControl("ddlFCategoryEvent"), DropDownList)
                Dim objPostedData As HttpPostedFile
                Dim objBabitReportDocument As BabitReportDocument = New BabitReportDocument()
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
                        BindGridBabitReportEventDoc()
                        Return
                    End If

                    Dim strDealerCode As String = String.Empty
                    If IsLoginAsDealer() Then
                        strDealerCode = lblDealerCodeName.Text.Split("/")(0).Trim
                    End If

                    If Not FileIsExist(sFileName, _arrDataUploadFile) Then
                        Dim SrcFile As String = Path.GetFileName(objPostedData.FileName) '-- Source file name
                        Dim strBabitPathConfig As String = KTB.DNet.Lib.WebConfig.GetValue("BabitFileDirectory")
                        Dim strBabitPathFile As String = "\BABIT\" & objDealer.DealerCode & "\BabitReportEvent\" & TimeStamp() & SrcFile.Substring(SrcFile.Length - 4)
                        Dim strDestFile As String = strBabitPathConfig & strBabitPathFile '--Destination File                       

                        objBabitReportDocument.BabitReportHeader = New BabitReportHeader()
                        objBabitReportDocument.AttachmentData = objPostedData
                        objBabitReportDocument.FileName = sFileName
                        objBabitReportDocument.Path = strDestFile
                        objBabitReportDocument.FileDescription = IIf(txtKeterangan.Text.Trim = "", "Laporan Babit Document", txtKeterangan.Text)

                        UploadAttachment(objBabitReportDocument, TempDirectory)

                        _arrDataUploadFile.Add(objBabitReportDocument)
                        sesHelper.SetSession(sessBabitReportDoc, _arrDataUploadFile)
                    Else
                        MessageBox.Show(SR.DataIsExist("Attachment File"))
                    End If
                Else
                    MessageBox.Show(SR.DataNotFound("Attachment File"))
                End If

            Case "Delete" 'Delete this datagrid item 
                Dim oBabitReportDocument As BabitReportDocument = CType(_arrDataUploadFile(e.Item.ItemIndex), BabitReportDocument)
                If oBabitReportDocument.ID > 0 Then
                    Dim arrDelete As ArrayList = CType(sesHelper.GetSession(sessDeleteBabitReportDoc), ArrayList)
                    If IsNothing(arrDelete) Then arrDelete = New ArrayList
                    arrDelete.Add(oBabitReportDocument)
                    sesHelper.SetSession(sessDeleteBabitReportDoc, arrDelete)
                End If
                RemoveBabitReportEventAttachment(CType(_arrDataUploadFile(e.Item.ItemIndex), BabitReportDocument), TempDirectory)
                _arrDataUploadFile.RemoveAt(e.Item.ItemIndex)

            Case "Download" 'Download File
                Response.Redirect("../Download.aspx?file=" & e.CommandArgument)
        End Select

        BindGridBabitReportEventDoc()
    End Sub

    Private Sub RemoveBabitReportEventAttachment(ByVal ObjAttachment As BabitReportDocument, ByVal TargetPath As String)
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

    Private Sub dgBabitReportDocument_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgBabitReportDocument.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            e.Item.Cells(0).Text = e.Item.ItemIndex + 1 + (dgBabitReportDocument.CurrentPageIndex * dgBabitReportDocument.PageSize)

            Dim arrUpload As ArrayList = CType(sesHelper.GetSession(sessBabitReportDoc), ArrayList)
            If Not IsNothing(arrUpload) AndAlso arrUpload.Count > 0 Then
                Dim objBabitReportDocument As BabitReportDocument = arrUpload(e.Item.ItemIndex)
                Dim lblFileName As Label = CType(e.Item.FindControl("lblFileName"), Label)
                lblFileName.Text = Path.GetFileName(objBabitReportDocument.FileName)
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

        arrBabitReportEventDtl = CType(sesHelper.GetSession(sessBabitReportEventDtl), ArrayList)
        arrBabitReportEventDtl = New System.Collections.ArrayList(
                (From obj As BabitReportEventDetail In arrBabitReportEventDtl.OfType(Of BabitReportEventDetail)()
                    Where obj.Description <> "#"
                    Select obj).ToList())

        arrBabitReportEventAct = CType(sesHelper.GetSession(sessBabitReportAct), ArrayList)
        arrBabitReportEventDoc = CType(sesHelper.GetSession(sessBabitReportDoc), ArrayList)

        arlDeleteBabitReportEventDtl = CType(sesHelper.GetSession(sessDeleteBabitReportEventDtl), ArrayList)
        arlDeleteBabitReportDoc = CType(sesHelper.GetSession(sessDeleteBabitReportDoc), ArrayList)
        arlDeleteBabitReportAct = CType(sesHelper.GetSession(sessDeleteBabitReportAct), ArrayList)

        arlDeleteBabitReportEventDtl = IIf(Not IsNothing(arlDeleteBabitReportEventDtl), arlDeleteBabitReportEventDtl, New ArrayList)
        arlDeleteBabitReportDoc = IIf(Not IsNothing(arlDeleteBabitReportDoc), arlDeleteBabitReportDoc, New ArrayList)
        arlDeleteBabitReportAct = IIf(Not IsNothing(arlDeleteBabitReportAct), arlDeleteBabitReportAct, New ArrayList)

        Dim _oDealer As Dealer = New Dealer
        If IsLoginAsDealer() Then
            _oDealer = SesDealer()
        Else
            _oDealer = New DealerFacade(User).Retrieve(CInt(hdnDealerID.Value))
        End If
        If Mode <> "Edit" Then
            objBabitReportHeader = New BabitReportHeader
        Else
            objBabitReportHeader = CType(sesHelper.GetSession(sessBabitReportHdr), BabitReportHeader)
        End If
        objBabitReportHeader.Dealer = _oDealer
        objBabitReportHeader.BabitHeader = New BabitHeaderFacade(User).Retrieve(CType(hdnBabitHeaderID.Value, Integer))
        objBabitReportHeader.PeriodStart = icPeriodStart.Value
        objBabitReportHeader.PeriodEnd = icPeriodEnd.Value
        If Mode = "New" Then
            objBabitReportHeader.BabitReportStatus = 0  '--- Status Baru
        End If
        objBabitReportHeader.InvitationQty = txtInvitationQty.Text
        objBabitReportHeader.AttendeeQty = txtAttendeeQty.Text
        objBabitReportHeader.Notes = txtNotes.Text

        Dim _result As Integer = 0
        If Mode <> "Edit" Then
            _result = New BabitReportEventDetailFacade(User).InsertTransaction(objBabitReportHeader, arrBabitReportEventDtl, arrBabitReportEventDoc, arrBabitReportEventAct)
        Else
            _result = New BabitReportEventDetailFacade(User).UpdateTransaction(objBabitReportHeader, arrBabitReportEventDtl, arlDeleteBabitReportEventDtl, arrBabitReportEventDoc, arlDeleteBabitReportDoc, arrBabitReportEventAct, arlDeleteBabitReportAct)
        End If

        Dim strJs As String = String.Empty
        If _result > 0 Then
            CommitAttachment(arrBabitReportEventDoc)
            If Mode = "Edit" Then
                RemoveBabitProposalAttachment(arlDeleteBabitReportDoc, TargetDirectory)
            End If

            If Mode = "Edit" Then
                strJs = "alert('Update Data Berhasil');"

            ElseIf Mode = "New" Then
                strJs = "alert('Simpan Data Berhasil');"
            End If

            strJs += "window.location = '../Babit/FrmBabitReportEventList.aspx'"
            ClearTempData()
            ClearAll()
        Else
            If Mode = "Edit" Then
                strJs = "alert('Update Data Gagal');"

            ElseIf Mode = "New" Then
                strJs = "alert('Simpan Data Gagal');"
            End If
        End If
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Script", strJs, True)
    End Sub

    Private Sub RemoveBabitProposalAttachment(ByVal AttachmentCollection As ArrayList, ByVal TargetPath As String)
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim finfo As FileInfo

        Try
            success = imp.Start()
            If success Then
                For Each obj As BabitReportDocument In AttachmentCollection
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

    Private Sub btnBaru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBaru.Click
        Me.ClearAll()
        Me.btnSave.Visible = True
    End Sub

    Private Sub btnGetInfoBabitHeader_Click(sender As Object, e As EventArgs) Handles btnGetInfoBabitHeader.Click
        If Me.hdnBabitHeaderID.Value.Trim = "" Then Exit Sub

        arrBabitReportEventDoc = CType(sesHelper.GetSession(sessBabitReportDoc), ArrayList)
        If IsNothing(arrBabitReportEventDoc) Then arrBabitReportEventDoc = New ArrayList

        Dim objBabitHeader As BabitHeader = New BabitHeaderFacade(User).Retrieve(CInt(Me.hdnBabitHeaderID.Value))
        If Not IsNothing(objBabitHeader) AndAlso objBabitHeader.ID > 0 Then
            txtBabitRegNumber.Text = objBabitHeader.BabitRegNumber
            hdnDealerID.Value = objBabitHeader.Dealer.ID
            lblDealerCodeName.Text = objBabitHeader.Dealer.DealerCode & " / " & objBabitHeader.Dealer.DealerName
            lblAreaName.Text = objBabitHeader.Dealer.Area2.Description
            If Not IsNothing(objBabitHeader.DealerBranch) Then
                hdnDealerBranchID.Value = objBabitHeader.DealerBranch.ID
                lblDealerBranchCodeName.Text = objBabitHeader.DealerBranch.DealerBranchCode & " / " & objBabitHeader.DealerBranch.Name
            Else
                hdnDealerBranchID.Value = ""
                lblDealerBranchCodeName.Text = ""
            End If
            txtNoSurat.Text = objBabitHeader.BabitDealerNumber
            icPeriodStart.Value = objBabitHeader.PeriodStart
            icPeriodEnd.Value = objBabitHeader.PeriodEnd
            If Not IsNothing(objBabitHeader.BabitMasterEventType) Then
                ddlBabitMasterEventType.SelectedValue = objBabitHeader.BabitMasterEventType.ID
            Else
                ddlBabitMasterEventType.SelectedIndex = 0
            End If
            txtLocationName.Text = objBabitHeader.Location
            'ddlKota.SelectedValue = objBabitHeader.City.ID
            'ddlProvinsi.SelectedValue = objBabitHeader.City.Province.ID

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
                Me.ddlKota.SelectedValue = objBabitHeader.City.ID
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
                    Me.ddlKota.SelectedValue = objBabitHeader.City.ID
                End If
            End If

            Dim arlDisplayAndTarget As New ArrayList
            Dim criterias3 As New CriteriaComposite(New Criteria(GetType(BabitDisplayCar), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias3.opAnd(New Criteria(GetType(BabitDisplayCar), "BabitHeader.ID", MatchType.Exact, objBabitHeader.ID))
            arlDisplayAndTarget = New BabitDisplayCarFacade(User).Retrieve(criterias3)
            sesHelper.SetSession(sessBabitEventDisplayTarget, arlDisplayAndTarget)
            BindGridDisplayAndTarget()

            BindGridBabitEventSPK(txtBabitRegNumber.Text)
            BindGridBabitEventSPKProspek(txtBabitRegNumber.Text)
        End If
    End Sub

    Protected Sub dgDisplayAndTarget_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgDisplayAndTarget.ItemDataBound
        Dim lblKategoriKendaraan As Label
        Dim lblModelKendaraan As Label
        Dim lblQtyDisplay As Label
        Dim lblTargetPenjualan As Label
        Dim lblTestDrive As Label
        Dim intBabitDisplayCarID%

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

            lblKategoriKendaraan.Text = oBDC.SubCategoryVehicle.Category.CategoryCode
            lblModelKendaraan.Text = oBDC.SubCategoryVehicle.Name
            lblQtyDisplay.Text = oBDC.Qty.ToString
            lblTargetPenjualan.Text = Format(oBDC.SalesTarget, "###,###")
            If oBDC.IsTestDrive Then
                lblTestDrive.Text = "Ya"
            Else
                lblTestDrive.Text = "Tidak"
            End If
        End If
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("~/Babit/FrmBabitReportEventList.aspx")
    End Sub

    Private Sub BindGridDisplayAndTarget()
        arlBabitReportDisplayAndTarget = CType(sesHelper.GetSession(sessBabitEventDisplayTarget), ArrayList)
        If IsNothing(arlBabitReportDisplayAndTarget) Then arlBabitReportDisplayAndTarget = New ArrayList()

        dgDisplayAndTarget.DataSource = arlBabitReportDisplayAndTarget
        dgDisplayAndTarget.DataBind()
        sesHelper.SetSession(sessBabitEventDisplayTarget, arlBabitReportDisplayAndTarget)
    End Sub

    Private Sub dgBabitEventSPK_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgBabitEventSPK.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oBH As BabitHeader = CType(e.Item.DataItem, BabitHeader)
            If e.Item.ItemIndex = 0 Then
                ViewState("ItemIndex") = e.Item.ItemIndex
                intItemIndex = CType(ViewState("ItemIndex"), Integer)
                strVechileTypeKind = oBH.VechileTypeKind
            Else
                If strVechileTypeKind <> oBH.VechileTypeKind AndAlso oBH.DealerName.Trim.ToLower <> "Total Unit:" Then
                    ViewState("ItemIndex") = 0
                    intItemIndex = CType(ViewState("ItemIndex"), Integer)
                    strVechileTypeKind = oBH.VechileTypeKind
                Else
                    ViewState("ItemIndex") = CType(ViewState("ItemIndex"), Integer) + 1
                    intItemIndex = CType(ViewState("ItemIndex"), Integer)
                End If
            End If
            e.Item.Cells(0).Text = intItemIndex + 1 + (dgBabitEventSPK.CurrentPageIndex * dgBabitEventSPK.PageSize)

            Dim lblVechileTypeKind As Label = CType(e.Item.FindControl("lblVechileTypeKind"), Label)
            Dim lblVechileTypeName As Label = CType(e.Item.FindControl("lblVechileTypeName"), Label)
            Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
            Dim lblDealerName As Label = CType(e.Item.FindControl("lblDealerName"), Label)
            Dim lblQtyUnit As Label = CType(e.Item.FindControl("lblQtyUnit"), Label)

            If lblDealerName.Text.Trim.ToLower = "total unit :" Then
                e.Item.Cells(0).BackColor = Color.SkyBlue
                e.Item.Cells(1).BackColor = Color.SkyBlue
                e.Item.Cells(2).BackColor = Color.SkyBlue
                e.Item.Cells(3).BackColor = Color.SkyBlue
                e.Item.Cells(4).BackColor = Color.SkyBlue
                e.Item.Cells(5).BackColor = Color.SkyBlue
                e.Item.Cells(4).HorizontalAlign = HorizontalAlign.Right
                lblDealerName.Font.Bold = True
                lblQtyUnit.Font.Bold = True
                lblVechileTypeKind.Text = ""
                lblVechileTypeName.Text = ""
                lblDealerCode.Text = ""
                e.Item.Cells(0).Text = ""
                lblQtyUnit.Text = oBH.TotalUnit
            Else
                lblQtyUnit.Text = oBH.QtyUnit
                lblDealerName.Font.Bold = False
                lblQtyUnit.Font.Bold = False
                e.Item.Cells(4).HorizontalAlign = HorizontalAlign.Left
            End If
        End If
    End Sub

    '======================================================
    Sub BindGridBabitEventSPKProspek(ByVal strBabitRegNumber As String)
        Dim arrBabitSPKListFinal As ArrayList = New ArrayList
        If strBabitRegNumber.Trim <> "" Then
            Dim dSBabitSPKList As DataSet = New BabitHeaderFacade(User).RetrieveFromSPSPKProspek(strBabitRegNumber)
            Dim _babitHeader As New BabitHeader
            Dim row As DataRow
            Dim i As Integer = 0
            arrBabitSPKList = New ArrayList
            For i = 0 To dSBabitSPKList.Tables(0).Rows.Count - 1
                row = dSBabitSPKList.Tables(0).Rows(i)
                Try
                    _babitHeader = New BabitHeader
                    _babitHeader.ID = row("ID")
                    _babitHeader.BabitRegNumber = row("BabitRegNumber")
                    _babitHeader.VechileTypeKind = row("VechileTypeKind")
                    _babitHeader.VechileTypeName = row("VechileTypeName")
                    _babitHeader.DealerCode = row("DealerCode")
                    _babitHeader.DealerName = row("DealerName")
                    _babitHeader.QtyUnit = row("QtyUnit")
                    arrBabitSPKList.Add(_babitHeader)

                Catch ex As Exception
                End Try
            Next

            Dim dataList As ArrayList = New ArrayList
            dataList = New System.Collections.ArrayList(
                            (From obj As BabitHeader In arrBabitSPKList.OfType(Of BabitHeader)()
                                Where obj.DealerName <> "Total Unit :"
                                Order By obj.VechileTypeKind, obj.DealerCode
                                Select obj).ToList())

            Dim oBabitHeader As New BabitHeader
            Dim strVechileTypeKind As String = ""

            For j As Integer = 0 To dataList.Count - 1
                Dim objBabitEvent As BabitHeader = CType(dataList(j), BabitHeader)
                If j = 0 Then
                    strVechileTypeKind = objBabitEvent.VechileTypeKind
                End If
                If strVechileTypeKind <> objBabitEvent.VechileTypeKind Then
                    oBabitHeader = New BabitHeader
                    oBabitHeader.VechileTypeKind = strVechileTypeKind
                    oBabitHeader.DealerName = "Total Unit :"
                    oBabitHeader.TotalUnit = GetTotalUnitByVechileTypeKind(strVechileTypeKind)
                    arrBabitSPKListFinal.Add(oBabitHeader)
                    strVechileTypeKind = objBabitEvent.VechileTypeKind
                End If

                arrBabitSPKListFinal.Add(objBabitEvent)
                If j = dataList.Count - 1 Then
                    oBabitHeader = New BabitHeader
                    oBabitHeader.BabitRegNumber = objBabitEvent.BabitRegNumber
                    oBabitHeader.VechileTypeKind = objBabitEvent.VechileTypeKind
                    oBabitHeader.DealerName = "Total Unit :"
                    oBabitHeader.TotalUnit = GetTotalUnitByVechileTypeKind(strVechileTypeKind)
                    arrBabitSPKListFinal.Add(oBabitHeader)
                End If
            Next
        End If

        If arrBabitSPKListFinal.Count > 0 Then
            dgBabitEventSPKProspek.DataSource = arrBabitSPKListFinal
        Else
            dgBabitEventSPKProspek.DataSource = New ArrayList
        End If
        dgBabitEventSPKProspek.DataBind()
    End Sub

    Private Sub dgBabitEventSPKProspek_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgBabitEventSPKProspek.ItemDataBound
        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oBH As BabitHeader = CType(e.Item.DataItem, BabitHeader)
            If e.Item.ItemIndex = 0 Then
                ViewState("ItemIndex") = e.Item.ItemIndex
                intItemIndex = CType(ViewState("ItemIndex"), Integer)
                strVechileTypeKind = oBH.VechileTypeKind
            Else
                If strVechileTypeKind <> oBH.VechileTypeKind AndAlso oBH.DealerName.Trim.ToLower <> "Total Unit:" Then
                    ViewState("ItemIndex") = 0
                    intItemIndex = CType(ViewState("ItemIndex"), Integer)
                    strVechileTypeKind = oBH.VechileTypeKind
                Else
                    ViewState("ItemIndex") = CType(ViewState("ItemIndex"), Integer) + 1
                    intItemIndex = CType(ViewState("ItemIndex"), Integer)
                End If
            End If
            e.Item.Cells(0).Text = intItemIndex + 1 + (dgBabitEventSPKProspek.CurrentPageIndex * dgBabitEventSPKProspek.PageSize)

            Dim lblVechileTypeKind As Label = CType(e.Item.FindControl("lblVechileTypeKind"), Label)
            Dim lblVechileTypeName As Label = CType(e.Item.FindControl("lblVechileTypeName"), Label)
            Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
            Dim lblDealerName As Label = CType(e.Item.FindControl("lblDealerName"), Label)
            Dim lblQtyUnit As Label = CType(e.Item.FindControl("lblQtyUnit"), Label)

            If lblDealerName.Text.Trim.ToLower = "total unit :" Then
                e.Item.Cells(0).BackColor = Color.SkyBlue
                e.Item.Cells(1).BackColor = Color.SkyBlue
                e.Item.Cells(2).BackColor = Color.SkyBlue
                e.Item.Cells(3).BackColor = Color.SkyBlue
                e.Item.Cells(4).BackColor = Color.SkyBlue
                e.Item.Cells(5).BackColor = Color.SkyBlue
                e.Item.Cells(4).HorizontalAlign = HorizontalAlign.Right
                lblDealerName.Font.Bold = True
                lblQtyUnit.Font.Bold = True
                lblVechileTypeKind.Text = ""
                lblVechileTypeName.Text = ""
                lblDealerCode.Text = ""
                e.Item.Cells(0).Text = ""
                lblQtyUnit.Text = oBH.TotalUnit
            Else
                lblQtyUnit.Text = oBH.QtyUnit
                lblDealerName.Font.Bold = False
                lblQtyUnit.Font.Bold = False
                e.Item.Cells(4).HorizontalAlign = HorizontalAlign.Left
            End If
        End If
    End Sub

#End Region

End Class
