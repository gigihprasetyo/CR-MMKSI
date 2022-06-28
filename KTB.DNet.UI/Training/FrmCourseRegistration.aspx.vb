#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security
Imports System.IO
Imports System.Collections.Generic
Imports System.Linq
Imports GlobalExtensions
#End Region

Public Class FrmCourseRegistration
    Inherits System.Web.UI.Page

    Private helpers As TrainingHelpers = New TrainingHelpers(Me.Page, "Training After Sales - Pendaftaran")

    Private Sub TitleDescription(ByVal areaid As String)
        If areaid.Equals("1") Then
            lblPageTitle.Text = "Training Sales - Pendaftaran"
            hdnCategory.Value = "sales"
        ElseIf areaid.Equals("2") Then
            lblPageTitle.Text = "Training After Sales - Pendaftaran"
            hdnCategory.Value = "ass"
        ElseIf areaid.Equals("3") Then
            lblPageTitle.Text = "Training Customer Satisfaction - Pendaftaran"
            hdnCategory.Value = "cs"
        Else
            lblPageTitle.Text = "Training - Pendaftaran"
            hdnCategory.Value = "ass"
        End If
    End Sub

    Private ReadOnly Property AreaId As String
        Get
            Return Request.QueryString("area")
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        helpers.AddPriv(TrainingHelpers.privilageTraining.PrivillageType.view, SR.TrainingAssViewPendaftaran_Privilege)
        helpers.AddPriv(TrainingHelpers.privilageTraining.PrivillageType.fullAccess, SR.TrainingAssEditPendaftaran_Privilege)
        helpers.Privilage()
        If Not IsPostBack Then
            helpers.CheckDueDateTagihan("2")
            TitleDescription(AreaId)
            BindDDLTahunFiskal()
            ddlBulan.BindingMonth()
            If Me.IsDealer Then
                trDealer.Visible = False
            End If
            ReadCritriaSearch()
            GetDataTraining()
        End If
        lblPopUpDealer.AddOnClick("ShowPPDealerSelection();")
        lblkodeKategori.AddOnClick("ShowPPCourseSelection2();")
    End Sub

    Private Sub BindDDLTahunFiskal()
        Dim GetTahun As Integer = DateTime.Now.Year
        ddlTahunFiscal.ClearSelection()
        ddlTahunFiscal.Items.Clear()
        'Before
        For x As Integer = 4 To 0 Step -1
            Dim value1 As String = (GetTahun - x).ToString()
            Dim value2 As String = (GetTahun - x - 1).ToString()
            Dim value As String = String.Format("{0}/{1}", value2, value1)
            ddlTahunFiscal.Items.Add(New ListItem(value, value))
        Next
        'After
        For x As Integer = 0 To 4
            Dim value1 As String = (GetTahun + x).ToString()
            Dim value2 As String = (GetTahun + x + 1).ToString()
            Dim value As String = String.Format("{0}/{1}", value1, value2)
            ddlTahunFiscal.Items.Add(New ListItem(value, value))
        Next
        ddlTahunFiscal.SelectedValue = String.Format("{0}/{1}", GetTahun.ToString(), (GetTahun + 1).ToString())
    End Sub


    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        SaveCriteriaSearch()
        GetDataTraining()
    End Sub

    Private Sub GetDataTraining()
        Dim dealer As Dealer = CType(helpers.GetSession("Dealer"), Dealer)
        Dim critMRCT As New CriteriaComposite(New Criteria(GetType(TrMRTCDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critMRCT.opAnd(New Criteria(GetType(TrMRTCDealer), "Dealer.ID", MatchType.Exact, dealer.ID))
        Dim dataMRCT As List(Of String) = New TrMRTCDealerFacade(User).Retrieve(critMRCT).Cast(Of  _
            TrMRTCDealer).Select(Function(x) x.TrMRTC.ID.ToString()).ToList()

        Dim critsMRCT As New CriteriaComposite(New Criteria(GetType(TrMRTC), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critsMRCT.opAnd(New Criteria(GetType(TrMRTC), "Dealer.ID", MatchType.Exact, 2))
        Dim arrMRTC As ArrayList = New TrMRTCFacade(User).Retrieve(critsMRCT)
        If arrMRTC.IsItems Then
            dataMRCT.AddRange(arrMRTC.Cast(Of TrMRTC).Select(Function(x) x.ID.ToString()).ToList())
        End If

        If dataMRCT.Count.Equals(0) Then
            dtgHeader.DataSource = New List(Of TrCourse)
            dtgHeader.DataBind()
            Exit Sub
        End If

        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClass), "FiscalYear", MatchType.Exact, ddlTahunFiscal.SelectedValue))

        If dealer.Title.Equals(CType(EnumDealerTittle.DealerTittle.DEALER, String)) Then
            criterias.opAnd(New Criteria(GetType(TrClass), "TrMRTC.ID", MatchType.InSet, dataMRCT.GenerateInSet()))
        End If
        If txtkodeKategori.IsNotEmpty Then
            Dim courseCodeInSet As String = txtkodeKategori.Text.GenerateInSet()
            criterias.opAnd(New Criteria(GetType(TrClass), "TrCourse.CourseCode", MatchType.InSet, String.Format("({0})", courseCodeInSet)))
        End If
        If ddlBulan.IsSelected Then
            Dim nBulan As Integer = CInt(ddlBulan.SelectedValue)
            Dim tglMulai As DateTime = DateTime.MinValue
            Dim tglSelesai As DateTime = DateTime.MinValue
            Dim tahun1 As Integer = CInt(ddlTahunFiscal.SelectedValue.Split("/")(0))
            Dim tahun2 As Integer = CInt(ddlTahunFiscal.SelectedValue.Split("/")(1))

            If nBulan > 0 And nBulan < 4 Then
                tglMulai = New DateTime(tahun2, nBulan, 1)
                tglSelesai = New DateTime(tahun2, nBulan + 1, 1)
            ElseIf nBulan > 3 And nBulan < 12 Then
                tglMulai = New DateTime(tahun1, nBulan, 1)
                tglSelesai = New DateTime(tahun1, nBulan + 1, 1)
            Else
                tglMulai = New DateTime(tahun1, nBulan, 1)
                tglSelesai = New DateTime(tahun2, 1, 1)
            End If
            criterias.opAnd(New Criteria(GetType(TrClass), "StartDate", MatchType.GreaterOrEqual, tglMulai))
            criterias.opAnd(New Criteria(GetType(TrClass), "StartDate", MatchType.Lesser, tglSelesai))
        End If

        Dim dataClass As List(Of TrClass) = New TrClassFacade(Me.User).Retrieve(criterias).Cast(Of  _
            TrClass).ToList()
        Dim dataCourse As List(Of String) = dataClass.Select(Function(x) x.TrCourse.ID.ToString()).Distinct().ToList()
        helpers.SetSession("dataClass", dataClass)

        Dim listCourse As List(Of TrCourse) = New List(Of TrCourse)()
        Dim criteria As New CriteriaComposite(New Criteria(GetType(TrCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If Not dataCourse.Count.Equals(0) Then
            criteria.opAnd(New Criteria(GetType(TrCourse), "ID", MatchType.InSet, dataCourse.GenerateInSet))
            listCourse = New TrCourseFacade(User).Retrieve(criteria).Cast(Of TrCourse).Where(Function(x) x.JobPositionCategory.AreaID = 2).ToList()
        End If
        dtgHeader.DataSource = listCourse
        dtgHeader.DataBind()

    End Sub

    Private Sub dtgHeader_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgHeader.ItemCommand
        If e.CommandName.Equals("RegisterToClass") Then
            Dim lblCourseCode As Label = CType(e.Item.FindControl("lblCourseCode"), Label)
            Dim dealer As Dealer = CType(helpers.GetSession("Dealer"), Dealer)
            Response.Redirect(String.Format("FrmCourseRegistrationInput.aspx?area={0}&dealercode={1}&coursecode={2}&fiscalyear={3}&readonly=0", _
                                            2, dealer.DealerCode, lblCourseCode.Text, ddlTahunFiscal.SelectedValue))
        ElseIf e.CommandName.Equals("detail") Then
            Dim lblCourseCode As Label = CType(e.Item.FindControl("lblCourseCode"), Label)
            Dim dealer As Dealer = CType(helpers.GetSession("Dealer"), Dealer)
            Response.Redirect(String.Format("FrmCourseRegistrationInput.aspx?area={0}&dealercode={1}&coursecode={2}&fiscalyear={3}&readonly=1", _
                                            2, dealer.DealerCode, lblCourseCode.Text, ddlTahunFiscal.SelectedValue))
        End If

    End Sub

    Private Sub dtgHeader_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgHeader.ItemDataBound
        If e.Item.DataItem IsNot Nothing Then
            Dim data As TrCourse = CType(e.Item.DataItem, TrCourse)
            Dim dealer As Dealer = CType(helpers.GetSession("Dealer"), Dealer)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblCourseCode As Label = CType(e.Item.FindControl("lblCourseCode"), Label)
            Dim lblCourseName As Label = CType(e.Item.FindControl("lblCourseName"), Label)
            Dim lblCourseType As Label = CType(e.Item.FindControl("lblCourseType"), Label)
            Dim lblCategory As Label = CType(e.Item.FindControl("lblCategory"), Label)
            Dim lblCourseCtg As Label = CType(e.Item.FindControl("lblCourseCtg"), Label)
            Dim lblJumlahPendaftar As Label = CType(e.Item.FindControl("lblJumlahPendaftar"), Label)
            Dim lblTotTerdaftar As Label = CType(e.Item.FindControl("lblTotPendaftar"), Label)
            Dim lblSisa As Label = CType(e.Item.FindControl("lblSisa"), Label)
            Dim btnDaftar As LinkButton = CType(e.Item.FindControl("btnDaftar"), LinkButton)
            Dim dataDetail As DataGrid = CType(e.Item.FindControl("dtgClass"), DataGrid)

            lblNo.Text = e.Item.ItemIndex + 1 + (dtgHeader.CurrentPageIndex * dtgHeader.PageSize)
            lblCourseCode.Text = data.CourseCode
            lblCourseName.Text = data.CourseName
            lblCategory.Text = data.JobPositionCategory.Description
            lblCourseCtg.Text = data.Category.Code

            If data.PaymentType > 0 Then
                Dim crt As New CriteriaComposite(New Criteria(GetType(StandardCode), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crt.opAnd(New Criteria(GetType(StandardCode), "Category", MatchType.Exact, "EnumTipePembayaran"))
                crt.opAnd(New Criteria(GetType(StandardCode), "ValueId", MatchType.Exact, data.PaymentType.ToString()))

                Dim std As ArrayList = New StandardCodeFacade(User).Retrieve(crt)
                lblCourseType.Text = CType(std(0), StandardCode).ValueDesc
            End If

            Dim criterias As New CriteriaComposite(New Criteria(GetType(TrBookingCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TrBookingCourse), "FiscalYear", MatchType.Exact, ddlTahunFiscal.SelectedValue))
            criterias.opAnd(New Criteria(GetType(TrBookingCourse), "Dealer.ID", MatchType.Exact, dealer.ID))
            criterias.opAnd(New Criteria(GetType(TrBookingCourse), "TrCourse.ID", MatchType.Exact, data.ID))

            lblJumlahPendaftar.Text = New TrBookingCourseFacade(User).Retrieve(criterias).Count.ToString()

            criterias.opAnd(New Criteria(GetType(TrBookingCourse), "TrClassRegistration.ID", MatchType.IsNotNull, Nothing))
            lblTotTerdaftar.Text = New TrBookingCourseFacade(User).Retrieve(criterias).Count.ToString()
            lblSisa.Text = CInt(lblJumlahPendaftar.Text) - CInt(lblTotTerdaftar.Text)

            Dim tahun2 As Integer = CInt(ddlTahunFiscal.SelectedValue.Split("/")(1))
            Dim dateLatest As DateTime = New DateTime(tahun2, 3, 31)

            If DateTime.Now > dateLatest Then
                btnDaftar.Visible = False
            End If

            If Not helpers.IsEdit Then
                btnDaftar.Visible = False
            End If


            Dim dataClass As List(Of TrClass) = CType(helpers.GetSession("dataClass"), List(Of TrClass))
            Dim dataClassTraining As List(Of TrClass) = dataClass.Where(Function(x) x.TrCourse.ID.Equals(data.ID) _
                                                        And x.Status.Equals("1") And x.TrMRTC IsNot Nothing).ToList()
            Dim dataReview As List(Of DetailClass) = New List(Of DetailClass)
            For Each item As TrClass In dataClassTraining
                Dim criteria As New CriteriaComposite(New Criteria(GetType(TrBookingCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteria.opAnd(New Criteria(GetType(TrBookingCourse), "FiscalYear", MatchType.Exact, ddlTahunFiscal.SelectedValue))
                criteria.opAnd(New Criteria(GetType(TrBookingCourse), "Dealer.ID", MatchType.Exact, dealer.ID))
                criteria.opAnd(New Criteria(GetType(TrBookingCourse), "TrCourse.ID", MatchType.Exact, data.ID))
                criteria.opAnd(New Criteria(GetType(TrBookingCourse), "TrClassRegistration.TrClass.ID", MatchType.Exact, item.ID))

                Dim itemData As DetailClass = New DetailClass()
                itemData.ClassCode = item.ClassCode
                itemData.TanggalMulai = item.StartDate.DateFormat()
                itemData.TanggalSelesai = item.FinishDate.DateFormat()
                itemData.Lokasi = item.TrMRTC.Name
                itemData.CourseCode = data.CourseCode
                itemData.SiswaTerdaftar = New TrBookingCourseFacade(User).Retrieve(criteria).Count.ToString()
                If data.PaymentType = 2 Then
                    itemData.PricePerDay = item.PricePerDay.AddThousandDelimiter()
                    itemData.PaidDay = item.PaidDay
                    itemData.TotalPrice = item.PriceTotal.AddThousandDelimiter()
                Else
                    itemData.PricePerDay = "0"
                    itemData.PaidDay = "0"
                    itemData.TotalPrice = "0"
                End If

                dataReview.Add(itemData)
            Next
            AddHandler dataDetail.ItemDataBound, New System.Web.UI.WebControls.DataGridItemEventHandler(AddressOf dtgClass_ItemDataBound)

            dataDetail.DataSource = dataReview
            dataDetail.DataBind()

        End If

    End Sub

    Private Sub dtgClass_ItemDataBound(sender As Object, e As DataGridItemEventArgs)
        If Not e.Item.DataItem Is Nothing Then
            Dim RowValue As DetailClass = CType(e.Item.DataItem, DetailClass)
            Dim hlClass As HyperLink = CType(e.Item.FindControl("hKodeKelas"), HyperLink)
            Dim lbtnDetail As LinkButton = CType(e.Item.FindControl("btnDetail"), LinkButton)

            Dim actionValue As String = "popUpClassInformation('" + RowValue.ClassCode + "');"
            hlClass.NavigateUrl = "javascript:" + actionValue

            If RowValue.SiswaTerdaftar.Equals("0") Then
                lbtnDetail.Visible = False
            Else
                Dim dealer As Dealer = CType(helpers.GetSession("Dealer"), Dealer)
                lbtnDetail.PostBackUrl = String.Format("FrmCourseCancelRegistration.aspx?area={0}&dealercode={1}&coursecode={2}&fiscalyear={3}&classCode={4}", _
                                                       AreaId, dealer.DealerCode, RowValue.CourseCode, ddlTahunFiscal.SelectedValue, RowValue.ClassCode)
            End If

        End If
    End Sub

    Private Sub dtgClass_ItemCommand(sender As Object, e As DataGridCommandEventArgs)
        If Not e.CommandName.Equals("detail") Then
            MessageBox.Show("Masuk detail kaka")
        End If
    End Sub

    Private Sub SaveCriteriaSearch()
        helpers.AddCriteria("fiscalyear", ddlTahunFiscal.SelectedValue)
        helpers.AddCriteria("courseCode", txtkodeKategori.Text)
        helpers.AddCriteria("dealer", txtDealerCode.Text)
        helpers.AddCriteria("bulan", ddlBulan.SelectedValue)
        helpers.SaveCriteria()
    End Sub

    Private Sub ReadCritriaSearch()
        If Not helpers.IsNullCriteria Then
            ddlTahunFiscal.SelectedValue = helpers.GetStringCriteria("fiscalyear")
            ddlBulan.SelectedValue = helpers.GetStringCriteria("bulan")
            txtkodeKategori.Text = helpers.GetStringCriteria("courseCode")
            txtDealerCode.Text = helpers.GetStringCriteria("dealer")
        End If
    End Sub

    Protected Sub btnGet_Click(sender As Object, e As EventArgs) Handles btnGet.Click

    End Sub

End Class