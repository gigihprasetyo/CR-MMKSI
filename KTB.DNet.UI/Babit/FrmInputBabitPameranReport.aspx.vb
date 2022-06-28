#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.UI.Helper
Imports KTB.DNet.Security
Imports System.Text
Imports KTB.DNet.SAP
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit

Imports System.Collections.Generic
Imports System.Linq
Imports System.IO

#End Region

Public Class FrmInputBabitPameranReport
    Inherits System.Web.UI.Page

    Dim objBabitReportHeader As BabitReportHeader
    Dim objLoginUser As UserInfo
    Private objDealer As Dealer
    Private sessHelper As New SessionHelper
    Private TargetDirectory As String
    Private TempDirectory As String
    Private _vstICPameranStart As String = "ICPameranStart.Value"
    Private _vstICPameranEnd As String = "ICPameranEnd.Value"
    Private MAX_FILE_SIZE As Integer = 5120000
    Private Mode As String = "New"
    Private intItemIndex As Integer = 0
    Private strVechileTypeKind As String = ""
    Dim arlEvent As ArrayList = New ArrayList
    Dim arrBabitSPKList As New ArrayList

    Private Property SesDealer() As Dealer
        Get
            Return CType(sessHelper.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)
            sessHelper.SetSession("DEALER", Value)
        End Set
    End Property

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        If Not IsNothing(Request.QueryString("Mode")) Then
            Mode = Request.QueryString("Mode")
            btnBack.Visible = True
        End If
        If Mode = "New" Then  ' Login as Dealer
            objDealer = CType(sessHelper.GetSession("DEALER"), Dealer)
        Else
            objDealer = CType(sessHelper.GetSession("FrmInputBabitPameranReport.DEALER"), Dealer)
        End If
        objLoginUser = CType(sessHelper.GetSession("LOGINUSERINFO"), UserInfo)
        TargetDirectory = KTB.DNet.Lib.WebConfig.GetValue("SAN")
        TempDirectory = Server.MapPath("") + "\..\DataTemp\"
    End Sub

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If Mode = "New" Then
            If Not SecurityProvider.Authorize(Context.User, SR.BABIT_Input_Pameran_Laporan_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=BABIT - INPUT LAPORAN BABIT PAMERAN")
            End If
        Else
            If Not SecurityProvider.Authorize(Context.User, SR.BABIT_Daftar_Laporan_Display_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=BABIT - INPUT LAPORAN BABIT PAMERAN")
            End If
        End If
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsNothing(ViewState.Item(Me._vstICPameranStart)) Then
            If CType(ViewState.Item(Me._vstICPameranStart), Date).Day <> Me.ICPameranStart.Value.Day OrElse _
            CType(ViewState.Item(Me._vstICPameranStart), Date).Month <> Me.ICPameranStart.Value.Month OrElse _
            CType(ViewState.Item(Me._vstICPameranStart), Date).Year <> Me.ICPameranStart.Value.Year Then
                CalculatePeriodePameran()
            End If
        End If

        If Not IsNothing(ViewState.Item(Me._vstICPameranEnd)) Then
            If CType(ViewState.Item(Me._vstICPameranEnd), Date).Day <> Me.ICPameranEnd.Value.Day OrElse _
            CType(ViewState.Item(Me._vstICPameranEnd), Date).Month <> Me.ICPameranStart.Value.Month OrElse _
            CType(ViewState.Item(Me._vstICPameranEnd), Date).Year <> Me.ICPameranStart.Value.Year Then
                CalculatePeriodePameran()
            End If
        End If

        Initialization()
        If Not IsPostBack Then
            InitiateAuthorization()
            lblDealerCodeName.Text = SesDealer().DealerCode & " - " & SesDealer().DealerName

            BindddlProvince()
            BindddlLocationType()
            DisableAll()
            CalculatePeriodePameran()

            dgBabitEventSPKProspek.DataSource = New ArrayList()
            dgBabitEventSPKProspek.DataBind()
            dgBabitEventSPK.DataSource = New ArrayList()
            dgBabitEventSPK.DataBind()
            dgDisplayAndTarget.DataSource = New ArrayList()
            dgDisplayAndTarget.DataBind()
            dgUploadFile.DataSource = New ArrayList()
            dgUploadFile.DataBind()
            lblPopUpRegNumber.Attributes("onclick") = "ShowPopUpTO()"
            LoadData()
        End If
    End Sub

    Private Sub LoadData()
        If Not IsNothing(Request.QueryString("Mode")) Then
            Mode = Request.QueryString("Mode")
            If Mode <> "New" Then
                objBabitReportHeader = New BabitReportHeaderFacade(User).Retrieve(CInt(Request.QueryString("BabitReportHeaderID")))
                objDealer = objBabitReportHeader.Dealer
                sessHelper.SetSession("FrmInputBabitPameranReport.DEALER", objDealer)

                hdnNoReg.Value = objBabitReportHeader.BabitHeader.BabitRegNumber
                hdnNoReg_ValueChanged(Nothing, Nothing)

                Dim crit As New CriteriaComposite(New Criteria(GetType(BabitReportDocument), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crit.opAnd(New Criteria(GetType(BabitReportDocument), "BabitReportHeader.ID", MatchType.Exact, objBabitReportHeader.ID))
                Dim arlDoc As ArrayList = New BabitReportDocumentFacade(User).Retrieve(crit)
                sessHelper.SetSession("FrmInputBabitPameranReport.sessDataUploadFile", IIf(IsNothing(arlDoc), New ArrayList, arlDoc))

                BindGridEventUploadFile()
                lblPopUpRegNumber.Visible = False
                txtNoReg.Enabled = False

                If Mode = "Detail" Then
                    dgUploadFile.ShowFooter = False
                    dgUploadFile.Columns(dgUploadFile.Columns.Count - 1).Visible = False

                    btnSave.Enabled = False
                    ICPameranStart.Enabled = False
                    ICPameranEnd.Enabled = False
                    txtNotes.Enabled = False
                Else
                    btnSave.Enabled = True
                    ICPameranStart.Enabled = True
                    ICPameranEnd.Enabled = True
                    txtNotes.Enabled = True
                End If

                ICPameranStart.Value = objBabitReportHeader.PeriodStart
                ICPameranEnd.Value = objBabitReportHeader.PeriodEnd
                txtNotes.Text = objBabitReportHeader.Notes

                btnBack.Visible = True
            End If
        End If
    End Sub

    Private Sub Initialization()
        Me.ViewState.Add(Me._vstICPameranStart, Me.ICPameranStart.Value)
        Me.ViewState.Add(Me._vstICPameranEnd, Me.ICPameranEnd.Value)
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
                BindddlCity(Me.ddlProvinsi.SelectedValue, True)
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

    Private Sub BindddlLocationType()
        ddlLocationType.Items.Clear()
        ddlLocationType.Items.Add(New ListItem("Silahkan Pilih", -1))
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.StandardCode), "Category", MatchType.Exact, "EnumBabit.MasterLocationType"))

        Dim arlLocationType As ArrayList = New StandardCodeFacade(User).Retrieve(criterias)
        For Each sc As StandardCode In arlLocationType
            ddlLocationType.Items.Add(New ListItem(sc.ValueDesc, sc.ValueId))
        Next
    End Sub

    Private Sub DisableAll()
        ICPameranStart.Enabled = True
        ICPameranEnd.Enabled = True
        txtTOCode.ReadOnly = True
        txtNomorSurat.ReadOnly = True
        txtLocation.ReadOnly = True
        txtProspectTarget.ReadOnly = True
        ddlLocationType.Enabled = False
        ddlProvinsi.Enabled = False
        ddlKota.Enabled = False
        dgDisplayAndTarget.ShowFooter = False
        dgBabitEventSPK.ShowFooter = False
        dgBabitEventSPKProspek.ShowFooter = False
    End Sub

    Private Sub CalculatePeriodePameran()
        Dim TotalPeriode As Integer = 0
        If ICPameranEnd.Value < ICPameranStart.Value Then
            MessageBox.Show("Tanggal Pameran selesai tidak boleh kurang dari tanggal pameran dimulai")
            ICPameranStart.Value = ViewState.Item(_vstICPameranStart)
            ICPameranEnd.Value = ViewState.Item(_vstICPameranEnd)
            Exit Sub
        End If
        TotalPeriode = (ICPameranEnd.Value - ICPameranStart.Value).TotalDays + 1
        lblPeriodePameran.Text = TotalPeriode.ToString() & " Hari"
    End Sub

    Protected Sub hdnNoReg_ValueChanged(sender As Object, e As EventArgs) Handles hdnNoReg.ValueChanged
        Dim criteria As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(BabitHeader), "BabitRegNumber", MatchType.Exact, hdnNoReg.Value))
        Dim arrBH As ArrayList = New BabitHeaderFacade(User).Retrieve(criteria)
        If arrBH.Count > 0 Then
            Dim oBH As BabitHeader = arrBH(0)
            txtNoReg.Text = hdnNoReg.Value
            lblDealerCodeName.Text = oBH.Dealer.DealerCode & " - " & oBH.Dealer.DealerName
            If Not IsNothing(oBH.DealerBranch) Then
                txtTOCode.Text = oBH.DealerBranch.DealerBranchCode
                lblTOName.Text = oBH.DealerBranch.Name
            End If
            lblArea.Text = oBH.Dealer.Area2.Description
            txtNomorSurat.Text = oBH.BabitDealerNumber

            If Not IsNothing(oBH.BabitMasterLocation) Then
                ddlLocationType.SelectedIndex = 1
                txtLocation.Text = oBH.BabitMasterLocation.LocationName
                txtLocation.Visible = True
            Else
                ddlLocationType.SelectedIndex = 2

                Dim criterias4 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitSpecialCity), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias4.opAnd(New Criteria(GetType(BabitSpecialCity), "City.ID", MatchType.Exact, oBH.City.ID))
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
                    Me.ddlKota.SelectedValue = oBH.City.ID
                Else
                    Dim criterias5 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Province), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    Dim strSQL As String = "SELECT ProvinceID FROM City WHERE ID = " & oBH.City.ID
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
                        Me.ddlKota.SelectedValue = oBH.City.ID
                    End If
                End If
                txtLocation.Text = oBH.Location
                txtLocation.Visible = True
            End If

            ICPameranStart.Value = oBH.PeriodStart
            ICPameranEnd.Value = oBH.PeriodEnd
            CalculatePeriodePameran()
            txtProspectTarget.Text = oBH.ProspectTarget
            BindGridDisplayAndTarget(oBH)
            BindGridBabitEventSPK()
            BindGridBabitEventSPKProspek()
            sessHelper.SetSession("FrmInputBabitPameranReport.oBHID", oBH.ID)
        End If
    End Sub

    Private Sub BindGridDisplayAndTarget(ByVal oBH As BabitHeader)
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BabitDisplayCar), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crit.opAnd(New Criteria(GetType(BabitDisplayCar), "BabitHeader.ID", MatchType.Exact, oBH.ID))
        Dim arlDisplayAndTarget As ArrayList = New BabitDisplayCarFacade(User).Retrieve(crit)
        dgDisplayAndTarget.DataSource = arlDisplayAndTarget
        dgDisplayAndTarget.DataBind()
    End Sub

    Private Function GetTotalUnitByVechileTypeKind(ByVal strVechileTypeKind As String) As Integer
        Dim intSumTotalUnit As Integer = 0
        intSumTotalUnit = (From item As BabitHeader In arrBabitSPKList
                            Where item.VechileTypeKind = strVechileTypeKind And item.DealerName <> "Total Unit :"
                                Select (item.QtyUnit)).Sum()
        Return intSumTotalUnit
    End Function

    Sub BindGridBabitEventSPK()
        Dim arrBabitSPKListFinal As ArrayList = New ArrayList
        If hdnNoReg.Value.Trim <> "" Then
            Dim dSBabitSPKList As DataSet = New BabitHeaderFacade(User).RetrieveFromSPSPK(hdnNoReg.Value)
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
            Dim strDealerCode As String = ""

            For j As Integer = 0 To dataList.Count - 1
                Dim objBabitEvent As BabitHeader = CType(dataList(j), BabitHeader)
                If j = 0 Then
                    strVechileTypeKind = objBabitEvent.VechileTypeKind
                    strDealerCode = objBabitEvent.DealerCode
                End If
                If strVechileTypeKind <> objBabitEvent.VechileTypeKind Then
                    oBabitHeader = New BabitHeader
                    oBabitHeader.VechileTypeKind = strVechileTypeKind
                    oBabitHeader.DealerCode = strDealerCode
                    oBabitHeader.DealerName = "Total Unit :"
                    oBabitHeader.TotalUnit = GetTotalUnitByVechileTypeKind(strVechileTypeKind)
                    arrBabitSPKListFinal.Add(oBabitHeader)
                    strVechileTypeKind = objBabitEvent.VechileTypeKind
                    strDealerCode = objBabitEvent.DealerCode
                End If

                arrBabitSPKListFinal.Add(objBabitEvent)
                If j = dataList.Count - 1 Then
                    oBabitHeader = New BabitHeader
                    oBabitHeader.BabitRegNumber = objBabitEvent.BabitRegNumber
                    oBabitHeader.DealerCode = objBabitEvent.DealerCode
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

    Protected Sub dgUploadFile_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgUploadFile.ItemCommand
        Dim _arrDataUploadFile As ArrayList = CType(sessHelper.GetSession("FrmInputBabitPameranReport.sessDataUploadFile"), ArrayList)
        If IsNothing(_arrDataUploadFile) Then _arrDataUploadFile = New ArrayList

        Select Case e.CommandName
            Case "add"
                Dim FileUpload As HtmlInputFile = CType(e.Item.FindControl("UploadFile"), HtmlInputFile)
                Dim txtKeterangan As TextBox = CType(e.Item.FindControl("txtKeterangan"), TextBox)
                Dim objPostedData As HttpPostedFile
                Dim objBabitDocument As BabitReportDocument = New BabitReportDocument()
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

                    Dim strDealerCode As String = lblDealerCodeName.Text

                    If Not FileIsExist(sFileName, _arrDataUploadFile) Then
                        Dim SrcFile As String = Path.GetFileName(objPostedData.FileName) '-- Source file name
                        Dim strBabitPathConfig As String = KTB.DNet.Lib.WebConfig.GetValue("BabitFileDirectory")
                        Dim strBabitPathFile As String = "\BABIT\" & objDealer.DealerCode & "\PameranReport\" & TimeStamp() & SrcFile.Substring(SrcFile.Length - 4)
                        Dim strDestFile As String = strBabitPathConfig & strBabitPathFile '--Destination File                       

                        objBabitDocument.BabitReportHeader = New BabitReportHeader()
                        objBabitDocument.AttachmentData = objPostedData
                        objBabitDocument.FileName = sFileName
                        objBabitDocument.Path = strDestFile
                        objBabitDocument.FileDescription = IIf(txtKeterangan.Text.Trim = String.Empty, "Dokumen Laporan Babit Pameran", txtKeterangan.Text.Trim)

                        UploadAttachment(objBabitDocument, TempDirectory)

                        _arrDataUploadFile.Add(objBabitDocument)
                        sessHelper.SetSession("FrmInputBabitPameranReport.sessDataUploadFile", _arrDataUploadFile)
                    Else
                        MessageBox.Show(SR.DataIsExist("Attachment File"))
                    End If
                Else
                    MessageBox.Show(SR.DataNotFound("Attachment File"))
                End If

            Case "Delete" 'Delete this datagrid item 
                Dim oBabitDocument As BabitReportDocument = CType(_arrDataUploadFile(e.Item.ItemIndex), BabitReportDocument)
                If oBabitDocument.ID > 0 Then
                    Dim arrDelete As ArrayList = CType(sessHelper.GetSession("FrmInputBabitPameranReport.sessDelDataUploadFile"), ArrayList)
                    If IsNothing(arrDelete) Then arrDelete = New ArrayList
                    arrDelete.Add(oBabitDocument)
                    sessHelper.SetSession("FrmInputBabitPameranReport.sessDelDataUploadFile", arrDelete)
                End If

                RemoveBabitAttachment(CType(_arrDataUploadFile(e.Item.ItemIndex), BabitReportDocument), TempDirectory)
                _arrDataUploadFile.RemoveAt(e.Item.ItemIndex)

            Case "Download" 'Download File
                Response.Redirect("../Download.aspx?file=" & e.CommandArgument)
        End Select

        BindGridEventUploadFile()
    End Sub

    Private Sub RemoveBabitAttachment(ByVal ObjAttachment As BabitReportDocument, ByVal TargetPath As String)
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

    Sub BindGridEventUploadFile()
        Dim arlDocument As ArrayList = CType(sessHelper.GetSession("FrmInputBabitPameranReport.sessDataUploadFile"), ArrayList)
        If IsNothing(arlDocument) Then arlDocument = New ArrayList()
        dgUploadFile.DataSource = arlDocument
        dgUploadFile.DataBind()
    End Sub

    Private Function TimeStamp() As String
        Return DateTime.Now.Year & DateTime.Now.Month & DateTime.Now.Day & DateTime.Now.Hour & DateTime.Now.Minute & DateTime.Now.Second & DateTime.Now.Millisecond
    End Function

    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function

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

    Private Sub UploadAttachment(ByRef ObjAttachment As BabitReportDocument, ByVal TargetPath As String)
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

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Not Validate() Then
            Exit Sub
        End If
        Dim oBHR As BabitReportHeader = New BabitReportHeader()
        Dim arrBDR As ArrayList = CType(sessHelper.GetSession("FrmInputBabitPameranReport.sessDataUploadFile"), ArrayList)
        Dim _return As Integer = 0
        If Mode = "Edit" Then
            oBHR = New BabitReportHeaderFacade(User).Retrieve(CInt(Request.QueryString("BabitReportHeaderID")))
        End If

        oBHR.Dealer = objDealer
        Dim oBHID As Integer = CType(sessHelper.GetSession("FrmInputBabitPameranReport.oBHID"), Integer)
        Dim oBH As BabitHeader = New BabitHeaderFacade(User).Retrieve(oBHID)
        oBHR.BabitHeader = oBH
        oBHR.PeriodStart = ICPameranStart.Value
        oBHR.PeriodEnd = ICPameranEnd.Value
        oBHR.Notes = txtNotes.Text

        Dim arrDocReportDel As ArrayList = CType(sessHelper.GetSession("FrmInputBabitPameranReport.sessDelDataUploadFile"), ArrayList)
        If IsNothing(arrDocReportDel) Then arrDocReportDel = New ArrayList

        If Mode = "Edit" Then
            _return = New BabitReportDocumentFacade(User).UpdateTransaction(oBHR, arrBDR, arrDocReportDel)
        ElseIf Mode = "New" Then
            _return = New BabitReportDocumentFacade(User).InsertTransaction(oBHR, arrBDR)
        End If

        Dim strJs As String = String.Empty
        If _return > 0 Then
            Dim debug = _return
            CommitAttachment(arrBDR)
            If Request.QueryString("Mode") = "Edit" Then
                RemoveBabitPameranReportAttachment(arrDocReportDel, TargetDirectory)
            End If

            sessHelper.RemoveSession("FrmInputBabitPameranReport.sessDataUploadFile")
            If Mode = "Edit" Then
                strJs = "alert('Update Data Berhasil');"
            ElseIf Mode = "New" Then
                strJs = "alert('Simpan Data Berhasil');"
            End If
            strJs += "window.location = '../Babit/FrmBabitReportEventList.aspx'"
        Else
            If Mode = "Edit" Then
                strJs = "alert('Update Data Gagal');"
            ElseIf Mode = "New" Then
                strJs = "alert('Simpan Data Gagal');"
            End If
        End If
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Script", strJs, True)
    End Sub

    Private Sub RemoveBabitPameranReportAttachment(ByVal AttachmentCollection As ArrayList, ByVal TargetPath As String)
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

    Private Function Validate() As Boolean
        If dgUploadFile.Items.Count < 1 Then
            MessageBox.Show("Lampiran harus di isi")
            Return False
        End If

        If txtNoReg.Text.Trim.Length = 0 Then
            MessageBox.Show("Nomor Registrasi Harus Diisi")
            Return False
        End If
        Return True
    End Function

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Response.Redirect("~/Babit/FrmBabitReportEventList.aspx")
    End Sub

    Private Sub btnNoRegChange_Click(sender As Object, e As EventArgs) Handles btnNoRegChange.Click
        hdnNoReg_ValueChanged(Nothing, Nothing)
    End Sub

    '======================================================
    Sub BindGridBabitEventSPKProspek()
        Dim arrBabitSPKListFinal As ArrayList = New ArrayList
        If hdnNoReg.Value.Trim <> "" Then
            Dim dSBabitSPKList As DataSet = New BabitHeaderFacade(User).RetrieveFromSPSPKProspek(hdnNoReg.Value)
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
            Dim strDealerCode As String = ""

            For j As Integer = 0 To dataList.Count - 1
                Dim objBabitEvent As BabitHeader = CType(dataList(j), BabitHeader)
                If j = 0 Then
                    strVechileTypeKind = objBabitEvent.VechileTypeKind
                    strDealerCode = objBabitEvent.DealerCode
                End If
                If strVechileTypeKind <> objBabitEvent.VechileTypeKind Then
                    oBabitHeader = New BabitHeader
                    oBabitHeader.VechileTypeKind = strVechileTypeKind
                    oBabitHeader.DealerCode = strDealerCode
                    oBabitHeader.DealerName = "Total Unit :"
                    oBabitHeader.TotalUnit = GetTotalUnitByVechileTypeKind(strVechileTypeKind)
                    arrBabitSPKListFinal.Add(oBabitHeader)
                    strVechileTypeKind = objBabitEvent.VechileTypeKind
                    strDealerCode = objBabitEvent.DealerCode
                End If

                arrBabitSPKListFinal.Add(objBabitEvent)
                If j = dataList.Count - 1 Then
                    oBabitHeader = New BabitHeader
                    oBabitHeader.BabitRegNumber = objBabitEvent.BabitRegNumber
                    oBabitHeader.VechileTypeKind = objBabitEvent.VechileTypeKind
                    oBabitHeader.DealerCode = objBabitEvent.DealerCode
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



End Class