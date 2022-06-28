Imports System.IO
Imports System.Text
Imports System.Linq
Imports System.Collections.Generic

Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.PK


Public Class FrmProgramDiskonReguler
    Inherits System.Web.UI.Page

    Protected WithEvents dgDetail As System.Web.UI.WebControls.DataGrid

#Region "Variable Declaration"
    Private isVisibilitySave As Boolean = False
    Private isVisibilityEdit As Boolean = False
    Private isVisibilityDelete As Boolean = False
    Private sessionHelper As New SessionHelper
    Private Mode As String = "New"
#End Region

#Region "Function"
    Private Property SesDealer() As Dealer
        Get
            Return CType(sessionHelper.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)
            sessionHelper.SetSession("DEALER", Value)
        End Set
    End Property

    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function

    Private Sub BindDDL()
        BindToddlDiscountCategory()
        BindToddlProgramBased()
        BindToddlModel()
    End Sub

    Private Sub BindToddlModel()
        ddlModel.Items.Clear()
        '-- SubCategoryVehicle criteria & sort
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicle), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "Status", MatchType.Exact, ""))  '-- Type still active
        criterias.opAnd(New Criteria(GetType(SubCategoryVehicle), "Category.ID", MatchType.InSet, "(1,2)"))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(SubCategoryVehicle), "ID", Sort.SortDirection.DESC))  '-- Sort by Vechile type code

        '-- Bind ddlSubCategory dropdownlist
        ddlModel.DataSource = New SubCategoryVehicleFacade(User).Retrieve(criterias, sortColl)
        ddlModel.DataTextField = "Name"
        ddlModel.DataValueField = "ID"
        ddlModel.DataBind()
        ddlModel.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))  '-- Dummy blank item
    End Sub

    Private Sub BindToddlProgramBased()
        With ddlProgramBased
            .Items.Clear()
            .DataSource = New StandardCodeFacade(User).RetrieveByCategory("EnumProgramReguler.ProgramBased")
            .DataTextField = "ValueDesc"
            .DataValueField = "ValueId"
            .DataBind()
            .Items.Insert(0, New ListItem("Silakan Pilih ", -1))
            .SelectedIndex = 0
        End With
    End Sub

    Private Sub BindToddlDiscountCategory()
        Dim arrayListCategory As List(Of DiscountProposalParameter) = New DiscountProposalParameterFacade(User).RetrieveActiveList().Cast(Of DiscountProposalParameter).ToList().Where(Function(i) i.ParameterType = 1).ToList()

        ddlDiscountCategory.Items.Clear()
        Dim listitemBlank As New ListItem("Silakan Pilih", -1)
        ddlDiscountCategory.Items.Add(listitemBlank)

        For Each item As DiscountProposalParameter In arrayListCategory
            Dim listItem As New ListItem(item.ParameterName, item.ID)
            ddlDiscountCategory.Items.Add(listItem)
        Next
    End Sub

    Private Sub DoCari()
        dgMain.CurrentPageIndex = 0
        BindDataGrid(dgMain.CurrentPageIndex)
    End Sub

    Private Function getCriteria() As CriteriaComposite
        Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalProgramReguler), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If txtVechileTypeGeneralName.Text.Trim <> hdnVechileTypeGeneralName.Value.Trim Then txtVechileTypeGeneralName.Text = hdnVechileTypeGeneralName.Value
        If txtModelYear.Text.Trim <> hdnModelYear.Value.Trim Then txtModelYear.Text = hdnModelYear.Value
        If txtAssyYear.Text.Trim <> hdnAssyYear.Value.Trim Then txtAssyYear.Text = hdnAssyYear.Value

        If chkPeriode.Checked = True Then
            Dim TanggalAwal As New DateTime(CInt(icValidFrom.Value.Year), CInt(icValidFrom.Value.Month), CInt(icValidFrom.Value.Day), 0, 0, 0)
            Dim TanggalAkhir As New DateTime(CInt(icValidTo.Value.Year), CInt(icValidTo.Value.Month), CInt(icValidTo.Value.Day), 0, 0, 0)
            crit.opAnd(New Criteria(GetType(DiscountProposalProgramReguler), "ValidFrom", MatchType.GreaterOrEqual, Format(TanggalAwal, "yyyy-MM-dd HH:mm:ss")))
            crit.opAnd(New Criteria(GetType(DiscountProposalProgramReguler), "ValidTo", MatchType.Lesser, Format(TanggalAkhir.AddDays(1), "yyyy-MM-dd HH:mm:ss")))
        End If

        If ddlProgramBased.SelectedIndex > 0 Then
            crit.opAnd(New Criteria(GetType(DiscountProposalProgramReguler), "ProgramBased", MatchType.Exact, ddlProgramBased.SelectedValue))
        End If
        If ddlDiscountCategory.SelectedIndex > 0 Then
            crit.opAnd(New Criteria(GetType(DiscountProposalProgramReguler), "DiscountProposalParameter.ID", MatchType.Exact, ddlDiscountCategory.SelectedValue))
        End If
        If ddlModel.SelectedIndex > 0 Then
            crit.opAnd(New Criteria(GetType(DiscountProposalProgramReguler), "VechileTypeGeneral.SubCategoryVehicle.ID", MatchType.Exact, ddlModel.SelectedValue))
        End If
        If hdnVechileTypeGeneralID.Value <> "" Then
            crit.opAnd(New Criteria(GetType(DiscountProposalProgramReguler), "VechileTypeGeneral.ID", MatchType.InSet, "(" & hdnVechileTypeGeneralID.Value.Trim.Replace(";", ",") & ")"))
        End If
        If hdnModelYear.Value.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(DiscountProposalProgramReguler), "ModelYear", MatchType.InSet, "(" & hdnModelYear.Value.Trim.Replace(";", ",") & ")"))
        End If
        If hdnAssyYear.Value.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(DiscountProposalProgramReguler), "AssyYear", MatchType.InSet, "(" & hdnAssyYear.Value.Trim.Replace(";", ",") & ")"))
        End If
        If txtDiscountAmount.Text.Trim <> "" Then
            crit.opAnd(New Criteria(GetType(DiscountProposalProgramReguler), "DiscountAmount", MatchType.Exact, txtDiscountAmount.Text.Replace(",", "").Replace(".", "")))
        End If

        Return crit
    End Function

    Private Sub BindDataGrid(ByVal index As Integer, Optional ByVal isSearch As Boolean = True)
        Dim crit As CriteriaComposite = getCriteria()
        Dim totalrow As Integer = 0
        Dim arlDiscountProposalProgramReguler As ArrayList = New DiscountProposalProgramRegulerFacade(User).RetrieveActiveList(crit, index + 1,
                                                               dgMain.PageSize,
                                                               totalrow,
                                                               CType(ViewState("SortColFrmProgramDiskonReguler"), String),
                                                               CType(ViewState("SortDirFrmProgramDiskonReguler"), Sort.SortDirection))

        dgMain.DataSource = arlDiscountProposalProgramReguler
        dgMain.VirtualItemCount = totalrow
        dgMain.DataBind()
    End Sub

    Private Sub DoSimpan()
        If icValidFrom.Value > icValidTo.Value Then
            MessageBox.Show("Periode Awal tidak boleh lebih dari Periode Akhir")
            Exit Sub
        End If
        If ddlProgramBased.SelectedIndex = 0 Then
            MessageBox.Show("Program Based harap diisi")
            Exit Sub
        End If
        If ddlDiscountCategory.SelectedIndex = 0 Then
            MessageBox.Show("Program harap diisi")
            Exit Sub
        End If
        If txtDiscountAmount.Text.Trim = "" OrElse txtDiscountAmount.Text.Trim = "0" Then
            MessageBox.Show("Jumlah Diskon harap diisi")
            Exit Sub
        End If
        If hdnVechileTypeGeneralID.Value = "" Then
            MessageBox.Show("Tipe harap diisi")
            Exit Sub
        End If
        If hdnModelYear.Value = "" Then
            MessageBox.Show("Tahun Model harap diisi")
            Exit Sub
        End If
        If hdnAssyYear.Value.Trim = "" Then
            MessageBox.Show("Tahun Perakitan harap diisi")
            Exit Sub
        End If

        If txtVechileTypeGeneralName.Text.Trim <> hdnVechileTypeGeneralName.Value.Trim Then txtVechileTypeGeneralName.Text = hdnVechileTypeGeneralName.Value
        If txtModelYear.Text.Trim <> hdnModelYear.Value.Trim Then txtModelYear.Text = hdnModelYear.Value
        If txtAssyYear.Text.Trim <> hdnAssyYear.Value.Trim Then txtAssyYear.Text = hdnAssyYear.Value

        If txtModelYear.Text.Trim = "" Then txtModelYear.Text = 0
        If txtAssyYear.Text.Trim = "" Then txtAssyYear.Text = 0

        Dim objDPProgramReguler As DiscountProposalProgramReguler = CType(sessionHelper.GetSession("sessionDiscountProposalProgramReguler"), DiscountProposalProgramReguler)
        If IsNothing(objDPProgramReguler) Then
            objDPProgramReguler = New DiscountProposalProgramReguler With {.ID = 0}
        End If

        Dim objDiscountProposalParameter As DiscountProposalParameter = New DiscountProposalParameterFacade(User).Retrieve(CType(ddlDiscountCategory.SelectedValue, Integer))
        If IsNothing(objDiscountProposalParameter) Then objDiscountProposalParameter = New DiscountProposalParameter

        Dim intLoopFirst As Integer = 1
        Dim arrDiscountProposalProgramReguler As New ArrayList
        If hdnVechileTypeGeneralID.Value.Trim <> "" Then
            For Each strTipe As String In hdnVechileTypeGeneralID.Value.Split(";")
                Dim objVechileTypeGeneral As VechileTypeGeneral = New VechileTypeGeneralFacade(User).Retrieve(CShort(strTipe.Trim))
                If IsNothing(objVechileTypeGeneral) Then objVechileTypeGeneral = New VechileTypeGeneral

                If hdnModelYear.Value.Trim <> "" Then
                    For Each strModelYear As String In hdnModelYear.Value.Split(";")
                        Dim _strAssyYear As String = String.Empty
                        If hdnAssyYear.Value.Trim <> "" Then
                            For Each strAssyYear As String In hdnAssyYear.Value.Split(";")
                                If Mode = "Edit" Then
                                    If intLoopFirst = 1 Then
                                        intLoopFirst += 1
                                    Else
                                        objDPProgramReguler = New DiscountProposalProgramReguler
                                    End If
                                Else
                                    objDPProgramReguler = New DiscountProposalProgramReguler
                                End If
                                If CekDuplikasiRecord(objDPProgramReguler.ID, objDiscountProposalParameter.ID, objVechileTypeGeneral.ID, strAssyYear, strModelYear, ddlProgramBased.SelectedValue, icValidFrom.Value, icValidTo.Value) Then
                                    MessageBox.Show("Data ini sudah pernah diinput atau tidak boleh duplikat")
                                    Exit Sub
                                End If

                                objDPProgramReguler.DiscountProposalParameter = objDiscountProposalParameter
                                If ddlProgramBased.SelectedIndex > 0 Then
                                    objDPProgramReguler.ProgramBased = ddlProgramBased.SelectedValue
                                End If
                                objDPProgramReguler.ValidFrom = icValidFrom.Value
                                objDPProgramReguler.ValidTo = icValidTo.Value
                                objDPProgramReguler.DiscountAmount = CType(txtDiscountAmount.Text, Decimal)
                                objDPProgramReguler.VechileTypeGeneral = objVechileTypeGeneral
                                objDPProgramReguler.ModelYear = strModelYear.Trim
                                objDPProgramReguler.AssyYear = strAssyYear
                                arrDiscountProposalProgramReguler.Add(objDPProgramReguler)
                            Next
                        Else
                            _strAssyYear = "0"
                            If Mode = "Edit" Then
                                If intLoopFirst = 1 Then
                                    intLoopFirst += 1
                                Else
                                    objDPProgramReguler = New DiscountProposalProgramReguler
                                End If
                            Else
                                objDPProgramReguler = New DiscountProposalProgramReguler
                            End If
                            If CekDuplikasiRecord(objDPProgramReguler.ID, objDiscountProposalParameter.ID, objVechileTypeGeneral.ID, _strAssyYear, strModelYear.Trim, ddlProgramBased.SelectedValue, icValidFrom.Value, icValidTo.Value) Then
                                MessageBox.Show("Data ini sudah pernah diinput atau tidak boleh duplikat")
                                Exit Sub
                            End If

                            objDPProgramReguler.DiscountProposalParameter = objDiscountProposalParameter
                            If ddlProgramBased.SelectedIndex > 0 Then
                                objDPProgramReguler.ProgramBased = ddlProgramBased.SelectedValue
                            End If
                            objDPProgramReguler.ValidFrom = icValidFrom.Value
                            objDPProgramReguler.ValidTo = icValidTo.Value
                            objDPProgramReguler.DiscountAmount = CType(txtDiscountAmount.Text, Decimal)
                            objDPProgramReguler.VechileTypeGeneral = objVechileTypeGeneral
                            objDPProgramReguler.ModelYear = strModelYear.Trim
                            objDPProgramReguler.AssyYear = _strAssyYear
                            arrDiscountProposalProgramReguler.Add(objDPProgramReguler)
                        End If
                    Next
                Else
                    If Mode = "Edit" Then
                        If intLoopFirst = 1 Then
                            intLoopFirst += 1
                        Else
                            objDPProgramReguler = New DiscountProposalProgramReguler
                        End If
                    Else
                        objDPProgramReguler = New DiscountProposalProgramReguler
                    End If
                    If CekDuplikasiRecord(objDPProgramReguler.ID, objDiscountProposalParameter.ID, objVechileTypeGeneral.ID, 0, 0, ddlProgramBased.SelectedValue, icValidFrom.Value, icValidTo.Value) Then
                        MessageBox.Show("Data ini sudah pernah diinput atau tidak boleh duplikat")
                        Exit Sub
                    End If

                    objDPProgramReguler.DiscountProposalParameter = objDiscountProposalParameter
                    If ddlProgramBased.SelectedIndex > 0 Then
                        objDPProgramReguler.ProgramBased = ddlProgramBased.SelectedValue
                    End If
                    objDPProgramReguler.ValidFrom = icValidFrom.Value
                    objDPProgramReguler.ValidTo = icValidTo.Value
                    objDPProgramReguler.DiscountAmount = CType(txtDiscountAmount.Text, Decimal)
                    objDPProgramReguler.VechileTypeGeneral = objVechileTypeGeneral
                    objDPProgramReguler.ModelYear = 0
                    objDPProgramReguler.AssyYear = 0
                    arrDiscountProposalProgramReguler.Add(objDPProgramReguler)
                End If
            Next
        End If

        Dim fn As DiscountProposalProgramRegulerFacade = New DiscountProposalProgramRegulerFacade(User)
        Try
            If Mode = "New" Then
                fn.InsertTransaction(arrDiscountProposalProgramReguler)
            ElseIf Mode = "Edit" Then
                fn.UpdateTransaction(arrDiscountProposalProgramReguler)
            Else
                MessageBox.Show("Simpan data Gagal")
                Exit Sub
            End If
            MessageBox.Show("Simpan data Sukses")
            DoBatal()
            dgMain.CurrentPageIndex = 0
            BindDataGrid(dgMain.CurrentPageIndex, False)
        Catch ex As Exception
            MessageBox.Show("Simpan data gagal")
        End Try
    End Sub

    Private Function CekDuplikasiRecord(ByVal intDPProgramRegulerID As Integer, ByVal intDPParameterID As Integer, ByVal intVechileTypeGeneralID As Integer, ByVal intAssyYear As Integer, _
                                        ByVal intModelYear As Integer, ByVal intProgramBased As Integer, ByVal icValidFrom As Date, ByVal icValidTo As Date) As Boolean
        Try
            Dim _facade As DiscountProposalProgramRegulerFacade = New DiscountProposalProgramRegulerFacade(User)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DiscountProposalProgramReguler), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            If intDPProgramRegulerID > 0 Then
                criterias.opAnd(New Criteria(GetType(DiscountProposalProgramReguler), "ID", MatchType.No, intDPProgramRegulerID))
            End If
            criterias.opAnd(New Criteria(GetType(DiscountProposalProgramReguler), "DiscountProposalParameter.ID", MatchType.Exact, intDPParameterID))
            criterias.opAnd(New Criteria(GetType(DiscountProposalProgramReguler), "VechileTypeGeneral.ID", MatchType.Exact, intVechileTypeGeneralID))
            criterias.opAnd(New Criteria(GetType(DiscountProposalProgramReguler), "AssyYear", MatchType.Exact, intAssyYear))
            criterias.opAnd(New Criteria(GetType(DiscountProposalProgramReguler), "ModelYear", MatchType.Exact, intModelYear))
            criterias.opAnd(New Criteria(GetType(DiscountProposalProgramReguler), "ProgramBased", MatchType.Exact, intProgramBased))
            criterias.opAnd(New Criteria(GetType(DiscountProposalProgramReguler), "ValidFrom", MatchType.Exact, icValidFrom))
            criterias.opAnd(New Criteria(GetType(DiscountProposalProgramReguler), "ValidTo", MatchType.Exact, icValidTo))
            Dim arrDPProgramReguler As ArrayList = _facade.Retrieve(criterias)
            If Not IsNothing(arrDPProgramReguler) AndAlso arrDPProgramReguler.Count > 0 Then
                Return True
            End If
        Catch
        End Try

        Return False
    End Function

    Private Sub DoBatal()
        sessionHelper.SetSession("sessionDiscountProposalProgramReguler", Nothing)
        BindDDL()

        chkPeriode.Checked = False
        icValidFrom.Value = Date.Now
        icValidTo.Value = Date.Now
        ddlProgramBased.SelectedIndex = 0
        ddlDiscountCategory.SelectedIndex = 0
        txtDiscountAmount.Text = ""
        ddlModel.SelectedIndex = 0
        ddlModel_SelectedIndexChanged(Nothing, Nothing)
        hdnVechileTypeGeneralID.Value = ""
        hdnVechileTypeGeneralName.Value = ""
        hdnModelYear.Value = ""
        hdnAssyYear.Value = ""
        txtVechileTypeGeneralName.Text = ""
        txtAssyYear.Text = ""
        txtModelYear.Text = ""
        btnCari.Enabled = True
        dgMain.Enabled = True
        DoCari()
        ViewState("Mode") = "New"
    End Sub

    Private Sub EditRow(ByVal id As Integer)
        Dim obj As DiscountProposalProgramReguler = New DiscountProposalProgramRegulerFacade(User).Retrieve(id)
        sessionHelper.SetSession("sessionDiscountProposalProgramReguler", obj)

        icValidFrom.Value = obj.ValidFrom
        icValidTo.Value = obj.ValidTo
        If obj.ProgramBased <> "" Then
            ddlProgramBased.SelectedValue = obj.ProgramBased
        Else
            ddlProgramBased.SelectedIndex = 0
        End If
        ddlDiscountCategory.SelectedValue = obj.DiscountProposalParameter.ID
        txtDiscountAmount.Text = obj.DiscountAmount.ToString("#,##0")

        If Not IsNothing(obj.VechileTypeGeneral) Then
            If Not IsNothing(obj.VechileTypeGeneral.SubCategoryVehicle) Then
                ddlModel.SelectedValue = obj.VechileTypeGeneral.SubCategoryVehicle.ID
            End If
            hdnVechileTypeGeneralID.Value = obj.VechileTypeGeneral.ID
            hdnVechileTypeGeneralName.Value = obj.VechileTypeGeneral.Name
            txtVechileTypeGeneralName.Text = obj.VechileTypeGeneral.Name
        End If
        txtModelYear.Text = If(obj.ModelYear.ToString = "0", "", obj.ModelYear)
        hdnModelYear.Value = If(obj.ModelYear.ToString = "0", "", obj.ModelYear)
        txtAssyYear.Text = If(obj.AssyYear.ToString = "0", "", obj.AssyYear)
        hdnAssyYear.Value = If(obj.AssyYear.ToString = "0", "", obj.AssyYear)
        lblPopUpModelYear.Visible = True
        lblPopUpAssyYear.Visible = True

        dgMain.Enabled = False
        btnCari.Enabled = False
        ViewState("Mode") = "Edit"
    End Sub
    Private Sub DeleteRow(ByVal id As Integer)
        Dim obj As DiscountProposalProgramReguler = New DiscountProposalProgramRegulerFacade(User).Retrieve(id)
        Dim fn As DiscountProposalProgramRegulerFacade = New DiscountProposalProgramRegulerFacade(User)
        obj.RowStatus = 1
        Try
            fn.Delete(obj)
            DoBatal()
            dgMain.CurrentPageIndex = 0
            BindDataGrid(dgMain.CurrentPageIndex, False)
            MessageBox.Show(SR.DeleteSucces)
        Catch ex As Exception
            MessageBox.Show(SR.DeleteFail)
        End Try
    End Sub
#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If SesDealer().Title = EnumDealerTittle.DealerTittle.KTB Then
            If Not SecurityProvider.Authorize(Context.User, SR.DP_ProgramDiskonReguler_Lihat_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=DISCOUNT PROPOSAL - Program Diskon Reguler")
            End If
        Else
            ' case from dealer
            If Not SecurityProvider.Authorize(Context.User, SR.DP_ProgramDiskonReguler_Lihat_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=DISCOUNT PROPOSAL - Program Diskon Reguler")
            End If
        End If

        isVisibilitySave = SecurityProvider.Authorize(Context.User, SR.DP_Program_Diskon_Reguler_Simpan_Privilege)
        isVisibilityEdit = SecurityProvider.Authorize(Context.User, SR.DP_Program_Diskon_Reguler_Edit_Privilege)
        isVisibilityDelete = SecurityProvider.Authorize(Context.User, SR.DP_Program_Diskon_Reguler_Delete_Privilege)
    End Sub
#End Region

#Region "EventHandler"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        InitiateAuthorization()

        If Not IsPostBack Then
            ViewState.Add("SortColFrmProgramDiskonReguler", "ID")
            ViewState.Add("SortDirFrmProgramDiskonReguler", Sort.SortDirection.DESC)

            lblPopUpVechileTypeGeneral.Attributes("onclick") = "ShowPPTipeGeneralSelection();"
            lblPopUpModelYear.Attributes("onclick") = "ShowPPModelYearSelection();"
            lblPopUpAssyYear.Attributes("onclick") = "ShowPPProductionYearSelection();"

            ViewState("Mode") = "New"

            BindDDL()
            dgMain.CurrentPageIndex = 0
            BindDataGrid(dgMain.CurrentPageIndex, False)
            btnSimpan.Visible = isVisibilitySave
            If isVisibilityDelete = False AndAlso isVisibilityEdit = False Then
                dgMain.Columns(dgMain.Columns.Count - 1).Visible = isVisibilityDelete
            End If
        Else
            Try
                Mode = CType(ViewState("Mode"), String)
            Catch
            End Try
        End If
    End Sub

    Private Sub ddlModel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlModel.SelectedIndexChanged
        If ddlModel.SelectedIndex = 0 Then
            hdnVechileTypeGeneralID.Value = ""
            hdnVechileTypeGeneralName.Value = ""
            txtVechileTypeGeneralName.Text = ""
            hdnModelYear.Value = ""
            txtModelYear.Text = ""
            hdnAssyYear.Value = ""
            txtAssyYear.Text = ""
        End If
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        DoCari()
    End Sub

    Protected Sub dgMain_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgMain.ItemDataBound
        If Not IsNothing(e.Item.DataItem) Then

            Dim objDomain As DiscountProposalProgramReguler = CType(e.Item.DataItem, DiscountProposalProgramReguler)
            Dim lblID As Label = CType(e.Item.FindControl("lblID"), Label)
            Dim lblRowNum As Label = CType(e.Item.FindControl("lblRowNum"), Label)

            Dim lblPeriode As Label = CType(e.Item.FindControl("lblPeriode"), Label)
            Dim lblProgramBased As Label = CType(e.Item.FindControl("lblProgramBased"), Label)
            Dim lblKategoriProgram As Label = CType(e.Item.FindControl("lblKategoriProgram"), Label)
            Dim lblJumlahDiskon As Label = CType(e.Item.FindControl("lblJumlahDiskon"), Label)
            Dim lblModel As Label = CType(e.Item.FindControl("lblModel"), Label)
            Dim lblTipe As Label = CType(e.Item.FindControl("lblTipe"), Label)
            Dim lblModelYear As Label = CType(e.Item.FindControl("lblModelYear"), Label)
            Dim lblAssyYear As Label = CType(e.Item.FindControl("lblAssyYear"), Label)
            Dim lnkbtnEdit As LinkButton = CType(e.Item.FindControl("lnkbtnEdit"), LinkButton)
            Dim lnkbtnDelete As LinkButton = CType(e.Item.FindControl("lnkbtnDelete"), LinkButton)

            lblID.Text = objDomain.ID
            lblRowNum.Text = (e.Item.ItemIndex + 1 + (dgMain.CurrentPageIndex * dgMain.PageSize)).ToString

            lblPeriode.Text = objDomain.ValidFrom.ToString("dd/MM/yyyy") & " s.d " & objDomain.ValidTo.ToString("dd/MM/yyyy")
            Dim strProgramBasedName As String = String.Empty
            If objDomain.ProgramBased.ToString.Trim <> "" Then
                strProgramBasedName = CType(New StandardCodeFacade(User).RetrieveByValueId(objDomain.ProgramBased.ToString(), "EnumProgramReguler.ProgramBased")(0), StandardCode).ValueDesc
            End If
            lblProgramBased.Text = strProgramBasedName
            lblKategoriProgram.Text = objDomain.DiscountProposalParameter.ParameterName
            lblJumlahDiskon.Text = Format(objDomain.DiscountAmount, "#,##0")
            If Not IsNothing(objDomain.VechileTypeGeneral) Then
                If Not IsNothing(objDomain.VechileTypeGeneral.SubCategoryVehicle) Then
                    lblModel.Text = objDomain.VechileTypeGeneral.SubCategoryVehicle.Name
                End If
                lblTipe.Text = objDomain.VechileTypeGeneral.Name
            End If

            lblModelYear.Text = If(objDomain.ModelYear = 0, "", objDomain.ModelYear)
            lblAssyYear.Text = If(objDomain.AssyYear = 0, "", objDomain.AssyYear)
            lnkbtnEdit.Visible = isVisibilityEdit
            lnkbtnDelete.Visible = isVisibilityDelete
        End If
    End Sub

    Protected Sub dgMain_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgMain.ItemCommand
        Select Case e.CommandName
            Case "View"
            Case "Edit"
                EditRow(CInt(e.CommandArgument))
            Case "Delete"
                DeleteRow(CInt(e.CommandArgument))
        End Select
    End Sub

    Protected Sub dgMain_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgMain.PageIndexChanged
        dgMain.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgMain.CurrentPageIndex, False)
    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        DoSimpan()
    End Sub

    Protected Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        DoBatal()
    End Sub
#End Region

End Class