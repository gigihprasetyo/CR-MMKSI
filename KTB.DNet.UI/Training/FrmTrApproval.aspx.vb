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
Imports OfficeOpenXml
#End Region


Public Class FrmTrApproval
    Inherits System.Web.UI.Page

    Private helpers As TrainingHelpers = New TrainingHelpers(Me.Page, "Training After Sales - Approval")

    Private Sub TitleDescription(ByVal areaid As String)
        If areaid.Equals("1") Then
            lblPageTitle.Text = "Training Sales - Approval"
            hdnCategory.Value = "sales"
        ElseIf areaid.Equals("2") Then
            lblPageTitle.Text = "Training After Sales - Approval"
            hdnCategory.Value = "ass"
        ElseIf areaid.Equals("3") Then
            lblPageTitle.Text = "Training Customer Satisfaction - Approval"
            hdnCategory.Value = "cs"
        Else
            lblPageTitle.Text = "Training - Approval"
            hdnCategory.Value = "ass"
        End If
    End Sub

    Private ReadOnly Property AreaId As String
        Get
            Return Request.QueryString("area")
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        helpers.AddPriv(TrainingHelpers.privilageTraining.PrivillageType.fullAccess, SR.TrainingAssEditApproval_Privilege)
        helpers.Privilage()
        If Not IsPostBack Then
            TitleDescription(AreaId)
            Dim dealer As Dealer = Me.GetDealer()
            BindDDLTahunFiskal()
            BindDDLBulan()
            ReadCritriaSearch()
            GetDataTraining()
        End If
        lblkodeKategori.AddOnClick("ShowPPCourseSelection2();")

    End Sub

    Private Sub BindDDLTahunFiskal()
        Dim GetTahun As Integer = DateTime.Now.Year
        ddlTahunFiscal.ClearSelection()
        ddlTahunFiscal.Items.Clear()
       
        Dim iloop As Integer = 3
        If DateTime.Now.Month < 4 Then
            Dim value1 As String = (GetTahun).ToString()
            Dim value2 As String = (GetTahun - 1).ToString()
            Dim value As String = String.Format("{0}/{1}", value2, value1)
            ddlTahunFiscal.Items.Add(New ListItem(value, value))
        Else
            iloop = 4
        End If

        For x As Integer = 0 To iloop
            Dim value1 As String = (GetTahun + x).ToString()
            Dim value2 As String = (GetTahun + x + 1).ToString()
            Dim value As String = String.Format("{0}/{1}", value1, value2)
            ddlTahunFiscal.Items.Add(New ListItem(value, value))
        Next

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

    Private Function DataTraining() As List(Of TrCourse)
        Dim restData As List(Of TrCourse) = New List(Of TrCourse)
        Dim dealer As Dealer = CType(helpers.GetSession("Dealer"), Dealer)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        criterias.opAnd(New Criteria(GetType(TrClass), "FiscalYear", MatchType.Exact, ddlTahunFiscal.SelectedValue))

        If dealer.Title.Equals(CType(EnumDealerTittle.DealerTittle.DEALER, String)) Then
            criterias.opAnd(New Criteria(GetType(TrClass), "TrMRTC.MainArea.ID", MatchType.Exact, dealer.MainArea.ID))
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

        criterias.opAnd(New Criteria(GetType(TrClass), "TrMRTC.ID", MatchType.IsNotNull, Nothing))

        If txtKodeMrtc.IsNotEmpty Then
            criterias.opAnd(New Criteria(GetType(TrClass), "TrMRTC.Code", MatchType.Exact, txtKodeMrtc.Text))
        End If

        Dim dataClass As List(Of TrClass) = New TrClassFacade(Me.User).Retrieve(criterias).Cast(Of  _
            TrClass).ToList()
        Dim dataCourse As List(Of String) = dataClass.Select(Function(x) x.TrCourse.ID.ToString()).Distinct().ToList()
        helpers.SetSession("dataClass", dataClass)

        Dim criteria As New CriteriaComposite(New Criteria(GetType(TrCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If Not dataCourse.Count.Equals(0) Then
            criteria.opAnd(New Criteria(GetType(TrCourse), "ID", MatchType.InSet, dataCourse.GenerateInSet))
            criteria.opAnd(New Criteria(GetType(TrCourse), "Category.RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            restData = New TrCourseFacade(User).Retrieve(criteria).Cast(Of TrCourse).ToList()
        End If

        Return restData
    End Function

    Private Sub GetDataTraining()
        Dim data As List(Of TrCourse) = DataTraining().Where(Function(x) x.JobPositionCategory.AreaID = 2).ToList()
        If data.Count > 25 Then
            dtgHeader.PageSize = data.Count
        End If

        dtgHeader.DataSource = data
        dtgHeader.DataBind()
    End Sub

    Private Sub dtgHeader_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgHeader.ItemCommand
        Select Case e.CommandName.ToLower
            Case "download"
                Dim lblCourseCode As Label = CType(e.Item.FindControl("lblCourseCode"), Label)
                Dim course As TrCourse = New TrCourseFacade(User).Retrieve(lblCourseCode.Text)
                Download(course)
        End Select
    End Sub

    Private Sub dtgHeader_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgHeader.ItemDataBound
        If e.Item.DataItem IsNot Nothing Then
            Dim data As TrCourse = CType(e.Item.DataItem, TrCourse)
            ' Dim dealer As Dealer = CType(helpers.GetSession("Dealer"), Dealer)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblCourseCode As Label = CType(e.Item.FindControl("lblCourseCode"), Label)
            Dim lblCourseName As Label = CType(e.Item.FindControl("lblCourseName"), Label)
            Dim lblCategory As Label = CType(e.Item.FindControl("lblCategory"), Label)
            Dim lblCourseCtg As Label = CType(e.Item.FindControl("lblCourseCtg"), Label)
            Dim lblJumlahPendaftar As Label = CType(e.Item.FindControl("lblJumlahPendaftar"), Label)
            Dim lblJumlahKelas As Label = CType(e.Item.FindControl("lblJumlahKelas"), Label)
            Dim lblholdpendaftar As Label = CType(e.Item.FindControl("lblholdpendaftar"), Label)
            Dim lblTotTerdaftar As Label = CType(e.Item.FindControl("lblTotPendaftar"), Label)
            Dim dataDetail As DataGrid = CType(e.Item.FindControl("dtgClass"), DataGrid)

            lblNo.Text = e.Item.ItemIndex + 1 + (dtgHeader.CurrentPageIndex * dtgHeader.PageSize)
            lblCourseCode.Text = data.CourseCode
            lblCourseName.Text = data.CourseName
            lblCategory.Text = data.JobPositionCategory.Description
            lblCourseCtg.Text = data.Category.Code

            Dim criterias As New CriteriaComposite(New Criteria(GetType(TrBookingCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TrBookingCourse), "FiscalYear", MatchType.Exact, ddlTahunFiscal.SelectedValue))
            'criterias.opAnd(New Criteria(GetType(TrBookingCourse), "Dealer.ID", MatchType.Exact, dealer.ID))
            criterias.opAnd(New Criteria(GetType(TrBookingCourse), "TrCourse.ID", MatchType.Exact, data.ID))

            lblJumlahPendaftar.Text = New TrBookingCourseFacade(User).Retrieve(criterias).Count.ToString()

            criterias.opAnd(New Criteria(GetType(TrBookingCourse), "TrClassRegistration.ID", MatchType.IsNotNull, Nothing))
            lblTotTerdaftar.Text = New TrBookingCourseFacade(User).Retrieve(criterias).Count.ToString()
            lblholdpendaftar.Text = (CInt(lblJumlahPendaftar.Text) - CInt(lblTotTerdaftar.Text)).ToString()

            Dim dataReview As List(Of DetailClass) = DataDetailClass(data)

            lblJumlahKelas.Text = dataReview.Count.ToString()
            AddHandler dataDetail.ItemDataBound, New System.Web.UI.WebControls.DataGridItemEventHandler(AddressOf dtgClass_ItemDataBound)

            dataDetail.DataSource = dataReview
            dataDetail.DataBind()

        End If

    End Sub

    Private Function DataDetailClass(ByVal Data As TrCourse) As List(Of DetailClass)
        Dim dataClass As List(Of TrClass) = CType(helpers.GetSession("dataClass"), List(Of TrClass))
        Dim dataClassTraining As List(Of TrClass) = dataClass.Where(Function(x) x.TrCourse.ID.Equals(Data.ID) And x.Status = 1).ToList()
        Dim dataReview As List(Of DetailClass) = New List(Of DetailClass)
        For Each item As TrClass In dataClassTraining
            Dim criteria As New CriteriaComposite(New Criteria(GetType(TrBookingCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New Criteria(GetType(TrBookingCourse), "FiscalYear", MatchType.Exact, item.FiscalYear))
            criteria.opAnd(New Criteria(GetType(TrBookingCourse), "TrCourse.ID", MatchType.Exact, Data.ID))
            criteria.opAnd(New Criteria(GetType(TrBookingCourse), "TrClassRegistration.TrClass.ID", MatchType.Exact, item.ID))

            Dim itemData As DetailClass = New DetailClass()
            itemData.ClassCode = item.ClassCode
            itemData.TanggalMulai = item.StartDate.DateToString()
            itemData.TanggalSelesai = item.FinishDate.ToString("dd/MM/yyyy")
            itemData.Kapasitas = item.Capacity.ToString()
            itemData.Lokasi = item.TrMRTC.Name
            Dim arlBooking As ArrayList = New TrBookingCourseFacade(User).Retrieve(criteria)
            itemData.SiswaTerdaftar = arlBooking.Count.ToString()
            itemData.SisaKapasitas = (CInt(itemData.Kapasitas) - CInt(itemData.SiswaTerdaftar)).ToString()
            dataReview.Add(itemData)
        Next
        Return dataReview
    End Function

    Private Sub dtgClass_ItemDataBound(sender As Object, e As DataGridItemEventArgs)
        If Not e.Item.DataItem Is Nothing Then
            Dim RowValue As DetailClass = CType(e.Item.DataItem, DetailClass)
            Dim hlClass As HyperLink = CType(e.Item.FindControl("hKodeKelas"), HyperLink)
            Dim lbtnApproval As LinkButton = CType(e.Item.FindControl("lbtnApproval"), LinkButton)
            Dim classData As TrClass = New TrClassFacade(User).Retrieve(RowValue.ClassCode)

            Dim actionValue As String = "popUpClassInformation('" + RowValue.ClassCode + "');"
            hlClass.NavigateUrl = "javascript:" + actionValue

            If RowValue.SiswaTerdaftar = classData.Capacity Then
                lbtnApproval.PostBackUrl = "FrmTrApprovalDetail.aspx?classCode=" & RowValue.ClassCode
            Else
                Dim dataClass As TrClass = New TrClassFacade(User).Retrieve(RowValue.ClassCode)
                Dim criterias As New CriteriaComposite(New Criteria(GetType(TrClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(TrClass), "FiscalYear", MatchType.Exact, ddlTahunFiscal.SelectedValue))
                criterias.opAnd(New Criteria(GetType(TrClass), "TrMRTC.ID", MatchType.Exact, dataClass.TrMRTC.ID))
                criterias.opAnd(New Criteria(GetType(TrClass), "TrCourse.ID", MatchType.Exact, dataClass.TrCourse.ID))
                criterias.opAnd(New Criteria(GetType(TrClass), "StartDate", MatchType.Lesser, dataClass.StartDate))

                Dim sortColl As SortCollection = New SortCollection
                sortColl.Add(New Sort(GetType(TrClass), "StartDate", Sort.SortDirection.DESC))

                Dim arrClass As ArrayList = New TrClassFacade(User).Retrieve(criterias, sortColl)
                If arrClass.Count > 0 Then
                    Dim classBefore As TrClass = CType(arrClass(0), TrClass)
                    If classBefore.FinishDate.DateDay > Me.DateNow Then
                        Dim crits As New CriteriaComposite(New Criteria(GetType(TrBookingClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        crits.opAnd(New Criteria(GetType(TrBookingClass), "TrClass.ID", MatchType.Exact, classBefore.ID))
                        crits.opAnd(New Criteria(GetType(TrBookingClass), "Status", MatchType.Exact, 1))
                        crits.opAnd(New Criteria(GetType(TrBookingClass), "TrClassRegistration.ID", MatchType.IsNotNull, Nothing))
                        crits.opAnd(New Criteria(GetType(TrBookingClass), "TrBookingCourse.ValidateDate", MatchType.IsNotNull, Nothing))

                        Dim aggCA As Aggregate = New Aggregate(GetType(TrBookingClass), "TrBookingCourse", AggregateType.Count)
                        Dim Total As Integer = New TrBookingClassFacade(User).GetAggregateResult(aggCA, crits)

                        If Total.Equals(classBefore.Capacity) Then
                            lbtnApproval.PostBackUrl = "FrmTrApprovalDetail.aspx?classCode=" & RowValue.ClassCode
                        Else
                            lbtnApproval.Attributes.Add("onclick", "refreshGrid();")
                        End If
                    Else
                        lbtnApproval.PostBackUrl = "FrmTrApprovalDetail.aspx?classCode=" & RowValue.ClassCode
                    End If
                Else
                    lbtnApproval.PostBackUrl = "FrmTrApprovalDetail.aspx?classCode=" & RowValue.ClassCode
                End If

            End If

        End If
    End Sub

    Private Sub SaveCriteriaSearch()
        helpers.AddCriteria("fiscalyear", ddlTahunFiscal.SelectedValue)
        helpers.AddCriteria("courseCode", txtkodeKategori.Text)
        ' helpers.AddCriteria("dealer", txtDealerCode.Text)
        helpers.AddCriteria("bulan", ddlBulan.SelectedValue)
        helpers.AddCriteria("mrtcCode", txtKodeMrtc.Text)
        helpers.SaveCriteria()
    End Sub

    Private Sub ReadCritriaSearch()
        If Not helpers.IsNullCriteria Then
            ddlTahunFiscal.SelectedValue = helpers.GetStringCriteria("fiscalyear")
            ddlBulan.SelectedValue = helpers.GetStringCriteria("bulan")
            txtkodeKategori.Text = helpers.GetStringCriteria("courseCode")
            '   txtDealerCode.Text = helpers.GetStringCriteria("dealer")
            txtKodeMrtc.Text = helpers.GetStringCriteria("mrtcCode")
        End If
    End Sub

    Protected Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Try
            ReadCritriaSearch()
            Dim dataDic As Dictionary(Of TrCourse, List(Of DetailClass)) = New Dictionary(Of TrCourse, List(Of DetailClass))
            Dim dataCourse As List(Of TrCourse) = DataTraining()
            For Each itemData As TrCourse In dataCourse
                dataDic.Add(itemData, DataDetailClass(itemData))
            Next
            Dim x As String = String.Empty
        Catch ex As Exception
            MessageBox.Show("Download data tidak berhasil")
        End Try
    End Sub

    Private Sub Download(ByVal course As TrCourse)
        ReadCritriaSearch()
        Dim dataClass As List(Of TrClass) = CType(helpers.GetSession("dataClass"),  _
                            List(Of TrClass)).Where(Function(x) x.TrCourse.ID.Equals(course.ID) And x.Status = "1").ToList()
        Dim dataMRTC As List(Of TrMRTC) = DistictList(dataClass.Select(Function(x) x.TrMRTC))

        Using excelPackage As New ExcelPackage()
            Dim wsData As ExcelWorksheet = excelPackage.Workbook.Worksheets.Add("Approval Training Course")
            Dim rowIdx As Integer = 1

            For Each mrtc As TrMRTC In dataMRTC
                wsData.Cells("A" & rowIdx.ToString()).ValueBold("MRTC")
                wsData.Cells("B" & rowIdx.ToString()).ValueBold(mrtc.Name)
                rowIdx += 1
                wsData.Cells("A" & rowIdx.ToString()).ValueBold("Alamat")
                wsData.Cells("B" & rowIdx.ToString()).ValueBold(mrtc.Address)
                rowIdx += 1
                wsData.Cells("B" & rowIdx.ToString()).ValueBold(mrtc.City.CityName)
                rowIdx += 1

                Dim dataPendaftar As List(Of TrBookingCourse) = DataPendaftaran(course, mrtc, ddlTahunFiscal.SelectedValue)
                For Each kelas As TrClass In dataClass.Where(Function(x) x.TrMRTC.ID = mrtc.ID)
                    Dim clsIndesx As Integer = 1
                    rowIdx += 1
                    wsData.Cells("A" & rowIdx.ToString).ValueBold("Kelas")
                    wsData.Cells("B" & rowIdx.ToString).ValueBold(kelas.ClassCode + " - " + kelas.ClassName)
                    rowIdx += 1
                    wsData.Cells("A" & rowIdx.ToString).ValueBold("Tanggal Mulai")
                    wsData.Cells("B" & rowIdx.ToString).ValueBold(kelas.StartDate.DateToString())
                    rowIdx += 1
                    wsData.Cells("A" & rowIdx.ToString).ValueBold("Tanggal Selesai")
                    wsData.Cells("B" & rowIdx.ToString).ValueBold(kelas.FinishDate.DateToString())
                    rowIdx += 2
                    CreateHeaderExcel(wsData, rowIdx)
                    rowIdx += 1

                    'Yang Sudah di Approve
                    Dim dataKelas As List(Of TrBookingCourse) = New List(Of TrBookingCourse)
                    If dataPendaftar.IsItems Then
                        dataKelas = dataPendaftar.Where(Function(x) x.TrClassRegistration IsNot Nothing).ToList()
                        For Each dataSiswa As TrBookingCourse In dataKelas.Where(Function(x) x.TrClassRegistration.TrClass.ID = kelas.ID)
                            InsertDataRow(wsData, rowIdx, clsIndesx, dataSiswa, "Terdaftar")
                            dataPendaftar.Remove(dataSiswa)
                            clsIndesx += 1
                            rowIdx += 1
                        Next
                    End If
                    'Suggestion berdasarkan Priority
                    Dim listDealer As List(Of String) = dataPendaftar.OrderBy(Function(x) _
                                                         x.ValidateDate).Select(Function(x) x.Dealer.DealerCode).Distinct().ToList()
                    For Each dealerCode As String In listDealer
                        Dim allo As Integer = GetDealerAllocation(dealerCode, course.Category.Code)
                        Dim jumlahTerdaftar As Integer = dataKelas.Where(Function(x) x.TrClassRegistration.TrClass.ID = kelas.ID And x.Dealer.DealerCode = dealerCode).Count
                        If jumlahTerdaftar >= allo Then
                            Continue For
                        End If
                        Dim dataBooking As List(Of TrBookingCourse) = dataPendaftar.Where(Function(x) x.Dealer.DealerCode = dealerCode).ToList()
                        If dataBooking.IsItems Then
                            dataBooking = dataBooking.OrderBy(Function(x) x.PrioritySequence).ToList()
                            For idx As Integer = 1 To allo - jumlahTerdaftar
                                If dataBooking.Count >= idx Then
                                    InsertDataRow(wsData, rowIdx, clsIndesx, dataBooking(idx - 1), "Suggestion")
                                    dataPendaftar.Remove(dataBooking(idx - 1))
                                    clsIndesx += 1
                                    rowIdx += 1
                                Else
                                    Exit For
                                End If
                            Next
                        End If

                    Next

                    rowIdx += 1
                Next
                rowIdx += 1
                wsData.Cells("A" & rowIdx.ToString).ValueBold("Daftar Antrian")
                rowIdx += 1
               
                CreateHeaderExcel(wsData, rowIdx)
                rowIdx += 1
                If dataPendaftar.IsItems Then
                    Dim idxHold As Integer = 1
                    For Each dataSiswa As TrBookingCourse In dataPendaftar
                        InsertDataRow(wsData, rowIdx, idxHold, dataSiswa, "Hold")
                        idxHold += 1
                        rowIdx += 1
                    Next
                End If
                rowIdx += 1
            Next
            For colIdx As Integer = 1 To 8
                wsData.Column(colIdx).AutoFit()
            Next

            Dim fileBytes = excelPackage.GetAsByteArray()
            Dim fiscalYear As String = ddlTahunFiscal.SelectedValue.Split("/")(0)
            Dim fileName As String = "ApprovalTraining_" + course.CourseCode + "_" + fiscalYear + ".xls"

            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

            Dim success As Boolean = False

            Try
                success = imp.Start()
                If success Then
                    File.WriteAllBytes(Server.MapPath("~/DataTemp/" & fileName), fileBytes)
                    imp.StopImpersonate()
                End If

            Catch ex As Exception
                Exit Sub

            End Try

            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & fileName)
        End Using
    End Sub

    Public Sub IndexPlus(ByVal index As Integer, Optional ByVal plus As Integer = 1)
        index = index + plus
    End Sub

    Public Function DistictList(ByVal listT As IEnumerable(Of TrMRTC)) As List(Of TrMRTC)
        Dim listResult As New List(Of TrMRTC)
        For Each item As TrMRTC In listT
            If listResult.Where(Function(x) x.ID = item.ID).Count = 0 Then
                listResult.Add(item)
            End If
        Next
        Return listResult
    End Function

    Private Function DataPendaftaran(ByVal course As TrCourse, ByVal mrtc As TrMRTC, ByVal fiscalyear As String) As List(Of TrBookingCourse)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrBookingCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrBookingCourse), "TrCourse.ID", MatchType.Exact, course.ID))
        criterias.opAnd(New Criteria(GetType(TrBookingCourse), "FiscalYear", MatchType.Exact, fiscalyear))

        If mrtc.Dealer.IsDealer Then
            Dim listDealer As List(Of String) = GetDealerByMRTC(mrtc).Select(Function(x) x.DealerCode).ToList()
            If listDealer.IsItems Then
                criterias.opAnd(New Criteria(GetType(TrBookingCourse), "Dealer.DealerCode", MatchType.InSet, listDealer.GenerateInSet()))
            End If
        End If

        Dim dataBooking As List(Of TrBookingCourse) = New TrBookingCourseFacade(User).Retrieve(criterias).Cast(Of  _
            TrBookingCourse).OrderBy(Function(x) x.Dealer.ID).ToList()

        Return dataBooking
    End Function

    Private Function GetDealerByMRTC(ByVal mrtc As TrMRTC) As List(Of Dealer)
        Dim listDealer As New List(Of Dealer)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrMRTCDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrMRTCDealer), "TrMRTC.ID", MatchType.Exact, mrtc.ID))

        Dim arrDealer As ArrayList = New TrMRTCDealerFacade(User).Retrieve(criterias)
        If arrDealer.IsItems Then
            listDealer = arrDealer.Cast(Of TrMRTCDealer).Select(Function(x) x.Dealer).ToList()
        End If

        Return listDealer
    End Function

    Private Sub CreateHeaderExcel(ByVal wsData As ExcelWorksheet, ByVal rowIndex As Integer)
        wsData.Cells("A" & rowIndex.ToString).SetHeaderValue("No")
        wsData.Cells("B" & rowIndex.ToString).SetHeaderValue("Kode Dealer")
        wsData.Cells("C" & rowIndex.ToString).SetHeaderValue("No. Reg")
        wsData.Cells("D" & rowIndex.ToString).SetHeaderValue("Nama Siswa")
        wsData.Cells("E" & rowIndex.ToString).SetHeaderValue("Posisi")
        wsData.Cells("F" & rowIndex.ToString).SetHeaderValue("Mulai Bekerja")
        wsData.Cells("G" & rowIndex.ToString).SetHeaderValue("Prioritas")
        wsData.Cells("H" & rowIndex.ToString).SetHeaderValue("Status")
    End Sub

    Private Sub InsertDataRow(ByVal wsData As ExcelWorksheet, ByVal rowIndex As Integer, ByVal noTabel As Integer, ByVal dataBooking As TrBookingCourse, ByVal status As String)
        wsData.Cells("A" & rowIndex.ToString).SetValue(noTabel.ToString, Style.ExcelHorizontalAlignment.Center)
        wsData.Cells("B" & rowIndex.ToString).SetValue(dataBooking.Dealer.DealerCode, Style.ExcelHorizontalAlignment.Center)
        wsData.Cells("C" & rowIndex.ToString).SetValue(dataBooking.TrTrainee.ID, Style.ExcelHorizontalAlignment.Center)
        wsData.Cells("D" & rowIndex.ToString).SetValue(dataBooking.TrTrainee.Name)
        wsData.Cells("E" & rowIndex.ToString).SetValue(dataBooking.TrTrainee.RefJobPosition.Description, Style.ExcelHorizontalAlignment.Center)
        wsData.Cells("F" & rowIndex.ToString).SetValue(dataBooking.TrTrainee.StartWorkingDate.DateToString, Style.ExcelHorizontalAlignment.Center)
        wsData.Cells("G" & rowIndex.ToString).SetValue(dataBooking.PrioritySequence, Style.ExcelHorizontalAlignment.Center)
        wsData.Cells("H" & rowIndex.ToString).SetValue(status, Style.ExcelHorizontalAlignment.Center)
    End Sub

    Private Function GetDealerAllocation(ByVal dealerCode As String, kodeKursus As String) As Integer
        Dim dealerSetIn As String = dealerCode.GenerateInSet()
        Dim categorySetIn As String = kodeKursus.GenerateInSet()

        Dim alloctg As TrCourseCategoryAllocation = CType(New TrCourseCategoryAllocationFacade(User).RetrieveAllocation(CInt(AreaId), _
                                               dealerSetIn, categorySetIn)(0), TrCourseCategoryAllocation)
        Return alloctg.Allocated
    End Function

End Class