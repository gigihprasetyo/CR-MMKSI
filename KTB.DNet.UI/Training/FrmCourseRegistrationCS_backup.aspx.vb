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


Public Class FrmCourseRegistrationCS_backup
    Inherits System.Web.UI.Page

    Private helpers As TrainingHelpers = New TrainingHelpers(Me.Page)

    Private Sub TitleDescription()
        lblPageTitle.Text = "Training Customer Satisfaction - Pendaftaran"
        hdnCategory.Value = "cs"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            TitleDescription()
            Dim dealer As Dealer = CType(helpers.GetSession("Dealer"), Dealer)
            BindDDLTahunFiskal()
            BindDDLBulan()

            ReadCritriaSearch()
            GetDataTraining()
        End If

        lblkodeKategori.Attributes("onclick") = "ShowPPCourseSelection2();"
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

    Private Sub BindDDLBulan()
        ddlBulan.ClearSelection()
        ddlBulan.Items.Clear()
        ddlBulan.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        ddlBulan.Items.Add(New ListItem("Januari", "1"))
        ddlBulan.Items.Add(New ListItem("Febuari", "2"))
        ddlBulan.Items.Add(New ListItem("Maret", "3"))
        ddlBulan.Items.Add(New ListItem("April", "4"))
        ddlBulan.Items.Add(New ListItem("Mei", "5"))
        ddlBulan.Items.Add(New ListItem("Juni", "6"))
        ddlBulan.Items.Add(New ListItem("Juli", "7"))
        ddlBulan.Items.Add(New ListItem("Agustus", "8"))
        ddlBulan.Items.Add(New ListItem("September", "9"))
        ddlBulan.Items.Add(New ListItem("Oktober", "10"))
        ddlBulan.Items.Add(New ListItem("November", "11"))
        ddlBulan.Items.Add(New ListItem("Desember", "12"))
        ddlBulan.SelectedValue = "-1"
    End Sub

    Protected Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        SaveCriteriaSearch()
        GetDataTraining()
    End Sub

    Private Sub GetDataTraining()


        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClass), "FiscalYear", MatchType.Exact, ddlTahunFiscal.SelectedValue))
        criterias.opAnd(New Criteria(GetType(TrClass), "TrCourse.JobPositionCategory.ID", MatchType.Exact, 4))

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
            listCourse = New TrCourseFacade(User).Retrieve(criteria).Cast(Of TrCourse).ToList()
        End If
        dtgHeader.DataSource = listCourse
        dtgHeader.DataBind()

    End Sub

    Private Sub dtgHeader_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgHeader.ItemCommand
        If e.CommandName.Equals("RegisterToClass") Then
            Dim lblCourseCode As Label = CType(e.Item.FindControl("lblCourseCode"), Label)
            Response.Redirect(String.Format("FrmCourseRegistrationInputCS.aspx?coursecode={0}&fiscalyear={1}&readonly=0", _
                                            lblCourseCode.Text, ddlTahunFiscal.SelectedValue))
        ElseIf e.CommandName.Equals("detail") Then
            Dim lblCourseCode As Label = CType(e.Item.FindControl("lblCourseCode"), Label)
            Response.Redirect(String.Format("FrmCourseRegistrationInputCS.aspx?coursecode={0}&fiscalyear={1}&readonly=1", _
                                           lblCourseCode.Text, ddlTahunFiscal.SelectedValue))
        End If

    End Sub

    Private Sub dtgHeader_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgHeader.ItemDataBound
        If e.Item.DataItem IsNot Nothing Then
            Dim data As TrCourse = CType(e.Item.DataItem, TrCourse)
            ' Dim dealer As Dealer = CType(helpers.GetSession("Dealer"), Dealer)
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
            ' criterias.opAnd(New Criteria(GetType(TrBookingCourse), "Dealer.ID", MatchType.Exact, dealer.ID))
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

            Dim dataClass As List(Of TrClass) = CType(helpers.GetSession("dataClass"), List(Of TrClass))
            Dim dataClassTraining As List(Of TrClass) = dataClass.Where(Function(x) x.TrCourse.ID.Equals(data.ID)).ToList()
            Dim dataReview As List(Of DetailClass) = New List(Of DetailClass)
            For Each item As TrClass In dataClassTraining
                Dim criteria As New CriteriaComposite(New Criteria(GetType(TrBookingCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criteria.opAnd(New Criteria(GetType(TrBookingCourse), "FiscalYear", MatchType.Exact, ddlTahunFiscal.SelectedValue))
                ' criteria.opAnd(New Criteria(GetType(TrBookingCourse), "Dealer.ID", MatchType.Exact, dealer.ID))
                criteria.opAnd(New Criteria(GetType(TrBookingCourse), "TrCourse.ID", MatchType.Exact, data.ID))
                criteria.opAnd(New Criteria(GetType(TrBookingCourse), "TrClassRegistration.TrClass.ID", MatchType.Exact, item.ID))

                Dim itemData As DetailClass = New DetailClass()
                itemData.ClassCode = item.ClassCode
                itemData.TanggalMulai = item.StartDate.DateFormat()
                itemData.TanggalSelesai = item.FinishDate.DateFormat()
                itemData.Lokasi = item.LocationName
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
            Dim lbtnDaftar As LinkButton = CType(e.Item.FindControl("btnDaftar"), LinkButton)

            Dim actionValue As String = "popUpClassInformation('" + RowValue.ClassCode + "');"
            hlClass.NavigateUrl = "javascript:" + actionValue

            lbtnDaftar.PostBackUrl = String.Format("FrmCourseRegistrationInputCS.aspx?classCode={0}&readonly=0", _
                                           RowValue.ClassCode)

            If RowValue.SiswaTerdaftar.Equals("0") Then
                lbtnDetail.Visible = False
            Else
                Dim dealer As Dealer = CType(helpers.GetSession("Dealer"), Dealer)
                lbtnDetail.PostBackUrl = String.Format("FrmCourseRegistrationInputCS.aspx?classCode={0}&readonly=1", _
                                           RowValue.ClassCode)
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
        helpers.AddCriteria("bulan", ddlBulan.SelectedValue)
        helpers.SaveCriteria()
    End Sub

    Private Sub ReadCritriaSearch()
        If Not helpers.IsNullCriteria Then
            ddlTahunFiscal.SelectedValue = helpers.GetStringCriteria("fiscalyear")
            ddlBulan.SelectedValue = helpers.GetStringCriteria("bulan")
            txtkodeKategori.Text = helpers.GetStringCriteria("courseCode")
        End If
    End Sub

    Protected Sub btnGet_Click(sender As Object, e As EventArgs) Handles btnGet.Click

    End Sub

End Class