Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports System.Text
Imports System.Collections.Generic
Imports System.Linq
Imports GlobalExtensions

Public Class FrmDataStatusSiswa
    Inherits System.Web.UI.Page

    Dim rowNumber As Integer = 0
    Dim SessDataStatusSiswa As String = "SessDataStatusSiswa"
    Private helpers As TrainingHelpers = New TrainingHelpers(Me.Page)
    Private Const URL_DETAIL As String = "FrmDetailSiswa.aspx?category={0}"
    Private Const URL_REGISTER As String = "FrmRegistrasiSiswa.aspx?category={0}"

    Public ReadOnly Property AreaPriv() As String
        Get
            Select Case Request.QueryString("category").ToString()
                Case "sales"
                    Return "A"
                Case "ass"
                    Return "B"
                Case "cs"
                    Return "C"
            End Select
            Return "B"
        End Get
    End Property


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        helpers.CheckPrivilegeTransaction("tr4" + AreaPriv)
        If Not Page.IsPostBack Then
            InitForm()
            BindDataGrid(0)
        End If
    End Sub

    Private Sub InitForm()
        LoadByQueryString()
        SetViewState()
        BindDropDownList()

        If Not IsNothing(helpers.GetSession(SessDataStatusSiswa)) Then
            RestoreInputFieldFromSession()
        Else
            ClearInputField()
        End If
        Dim dealer As Dealer = CType(helpers.GetSession("Dealer"), Dealer)
        If dealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
            txtDealerCode.Text = dealer.DealerCode & " - " & dealer.DealerName
            trRegion.Visible = False
            txtDealerCode.Disabled()
            lblPopUpDealer.NonActiveControl()
        End If
    End Sub

    Private Sub LoadByQueryString()
        If String.IsNullOrEmpty(Request.QueryString("category")) Then
            Throw New Exception("Error no category")
        End If

        ddlJobPositionCategory.Items.Clear()
        hdnCategory.Value = Request.QueryString("category").ToString()

        Select Case Request.QueryString("category").ToString()
            Case "sales"
                hdnAreaID.Value = "1"
                lblSearchJobPos.AddOnClick("ShowJobPosSelectionMany(1);")
                btnBaru.NonVisible()
                Dim listItem As New ListItem("Sales", "1")
                ddlJobPositionCategory.Items.Add(listItem)
                lblPageTitle.Text = "Training Sales - Data Status Siswa"
            Case "ass"
                lblSearchJobPos.AddOnClick("ShowJobPosSelectionMany(2);")
                hdnAreaID.Value = "2"

                Dim listItem As New ListItem("Silakan Pilih", "-1")
                ddlJobPositionCategory.Items.Add(listItem)

                listItem = New ListItem("Sparepart", "0")
                ddlJobPositionCategory.Items.Add(listItem)

                listItem = New ListItem("Service", "2")
                ddlJobPositionCategory.Items.Add(listItem)

                listItem = New ListItem("Body Paint", "3")
                ddlJobPositionCategory.Items.Add(listItem)
                lblPageTitle.Text = "Training After Sales - Data Status Siswa"
                trAction.Visible = helpers.IsEdit
            Case "cs"
                hdnAreaID.Value = "3"
                btnBaru.NonVisible()
                lblSearchJobPos.AddOnClick("ShowJobPosSelectionMany(3);")
                Dim listItem As New ListItem("Customer Satisfaction", "4")
                ddlJobPositionCategory.Items.Add(listItem)
                lblPageTitle.Text = "Training Customer Satisfaction - Data Status Siswa"
        End Select

    End Sub

    Private Sub BindDropDownList()
        Dim enumTrainee As EnumTrTrainee = New EnumTrTrainee
        ddlStatus.DataSource = enumTrainee.RetrieveStatus
        ddlStatus.DataTextField = "NameTitle"
        ddlStatus.DataValueField = "ValTitle"
        ddlStatus.DataBind()
        Dim listItem As New ListItem("Silakan Pilih", "-1")
        ddlStatus.Items.Insert(0, listItem)
        ddlStatus.SelectedIndex = 0
    End Sub

    Private Sub SetViewState()
        ViewState("CurrentSortColumn") = "ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub RestoreInputFieldFromSession()
        Dim lastSession As Dictionary(Of String, String) = CType(helpers.GetSession(SessDataStatusSiswa), Dictionary(Of String, String))

        txtArea.Text = lastSession("txtArea")
        txtDealerCode.Text = lastSession("txtDealerCode")
        txtCourseCodePass.Text = lastSession("txtCourseCodePass")
        txtCourseCodeNotPass.Text = lastSession("txtCourseCodeNotPass")
        txtNomorReg.Text = lastSession("txtNomorReg")
        txtNama.Text = lastSession("txtNama")
        ddlStatus.ClearSelection()
        ddlStatus.Items.FindByValue(lastSession("ddlStatus")).Selected = True
        ddlJobPositionCategory.ClearSelection()
        ddlJobPositionCategory.Items.FindByValue(lastSession("ddlJobPositionCategory")).Selected = True
        txtPosisi.Text = lastSession("txtPosisi")

    End Sub

    Private Sub ClearInputField()
        txtDealerCode.Clear()
        txtArea.Clear()
        txtNama.Clear()
        txtCourseCodePass.Clear()
        txtCourseCodeNotPass.Clear()
        txtNomorReg.Clear()
        txtPosisi.Clear()
    End Sub

    Private Function GetDataTraining(ByVal traineeID As Integer) As List(Of DetailTrainingWajib)
        Dim rest As List(Of DetailTrainingWajib) = New List(Of DetailTrainingWajib)
        Try
            Dim dataTraining As IEnumerable(Of DataTrainingWajib) = _
                New TrTraineeFacade(User).GetDataTrainingWajib(traineeID, CInt(hdnAreaID.Value)).Cast(Of DataTrainingWajib)()

            Dim dataKursus As IEnumerable(Of String) = dataTraining.Select(Function(x) x.CourseCategoryCode).Distinct()
            For Each itemCtg As String In dataKursus
                Dim restData As DetailTrainingWajib = New DetailTrainingWajib()
                restData.CourseCategoryCode = itemCtg

                Dim dataLulus As List(Of DataTrainingWajib) = dataTraining.Where(Function(x) x.CourseCategoryCode = itemCtg And x.IsPass = 1).ToList()
                Dim strRest As String = String.Empty
                For Each restLulus As DataTrainingWajib In dataLulus
                    If String.IsNullOrEmpty(strRest) Then
                        strRest = strRest + restLulus.CourseCode
                    Else
                        strRest = strRest + ", " + restLulus.CourseCode
                    End If
                Next
                restData.CourseCategoryIsPass = strRest

                Dim dataBelumLulus As List(Of DataTrainingWajib) = dataTraining.Where(Function(x) x.CourseCategoryCode = itemCtg And x.IsPass = 0).ToList()
                Dim strTLRest As String = String.Empty
                For Each restTLulus As DataTrainingWajib In dataBelumLulus
                    If String.IsNullOrEmpty(strTLRest) Then
                        strTLRest = strTLRest + restTLulus.CourseCode
                    Else
                        strTLRest = strTLRest + ", " + restTLulus.CourseCode
                    End If
                Next
                restData.CourseCategoryIsNotPass = strTLRest
                rest.Add(restData)
            Next
        Catch ex As Exception
        End Try

        Return rest
    End Function

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        rowNumber = 0
        Dim totalRow As Integer = 0

        Dim criteria As CriteriaComposite = CreateCriteria()

        dtgHeader.DataSource = New TrTraineeFacade(User).RetrieveActiveList(criteria, indexPage + 1, dtgHeader.PageSize, totalRow, _
            CType(ViewState("CurrentSortColumn"), String), _
            CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dtgHeader.VirtualItemCount = totalRow
        dtgHeader.DataBind()
    End Sub

    Private Function CreateCriteria() As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrTrainee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))



        Select Case Request.QueryString("category").ToString()
            Case "sales"
                If txtDealerCode.IsNotEmpty Then
                    Dim dealer As Dealer = CType(helpers.GetSession("Dealer"), Dealer)
                    If dealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
                        criterias.opAnd(New Criteria(GetType(TrTrainee), "TrTraineeSalesmanHeader.SalesmanHeader.Dealer.ID", MatchType.Exact, dealer.ID))
                    Else
                        Dim dealers As String = New System.Text.StringBuilder("('").Append(txtDealerCode.Text).Append("')").ToString().Replace(";", "','")
                        criterias.opAnd(New Criteria(GetType(TrTrainee), "TrTraineeSalesmanHeader.SalesmanHeader.Dealer.DealerCode", MatchType.InSet, dealers))
                    End If
                End If
                If txtPosisi.IsNotEmpty Then
                    If txtPosisi.Text.IndexOf(";") > -1 Then
                        criterias.opAnd(New Criteria(GetType(TrTrainee), "TrTraineeSalesmanHeader.JobPosition", MatchType.InSet, txtPosisi.Text.GenerateInSet(True)))
                    Else
                        criterias.opAnd(New Criteria(GetType(TrTrainee), "TrTraineeSalesmanHeader.JobPosition", MatchType.Exact, txtPosisi.Text.Trim))
                    End If
                End If
                If txtNomorReg.IsNotEmpty Then
                    criterias.opAnd(New Criteria(GetType(TrTrainee), "TrTraineeSalesmanHeader.SalesmanHeader.SalesmanCode", MatchType.Exact, txtNomorReg.Text.Trim))
                End If
                If ddlStatus.IsSelected Then
                    criterias.opAnd(New Criteria(GetType(TrTrainee), "TrTraineeSalesmanHeader.Status", MatchType.Exact, ddlStatus.SelectedValue))
                End If
                criterias.opAnd(New Criteria(GetType(TrTrainee), "TrTraineeSalesmanHeader.JobPositionAreaID", MatchType.Exact, 1))
            Case "ass"
                If txtDealerCode.IsNotEmpty Then
                    Dim dealer As Dealer = CType(helpers.GetSession("Dealer"), Dealer)
                    If dealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
                        criterias.opAnd(New Criteria(GetType(TrTrainee), "Dealer.ID", MatchType.Exact, dealer.ID))
                    Else
                        Dim dealers As String = New System.Text.StringBuilder("('").Append(txtDealerCode.Text).Append("')").ToString().Replace(";", "','")
                        criterias.opAnd(New Criteria(GetType(TrTrainee), "Dealer.DealerCode", MatchType.InSet, dealers))
                    End If
                End If
                Dim critJobPositionCategory As Criteria = CreateCriteriaJobPositionCategory(hdnCategory.Value)
                criterias.opAnd(critJobPositionCategory)
                criterias.opAnd(New Criteria(GetType(JobPosition), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                If txtPosisi.IsNotEmpty Then
                    If txtPosisi.Text.IndexOf(";") > -1 Then
                        criterias.opAnd(New Criteria(GetType(TrTrainee), "JobPosition", MatchType.InSet, txtPosisi.Text.GenerateInSet(True)))
                    Else
                        criterias.opAnd(New Criteria(GetType(TrTrainee), "JobPosition", MatchType.Exact, txtPosisi.Text.Trim))
                    End If
                End If
                If txtNomorReg.IsNotEmpty Then
                    criterias.opAnd(New Criteria(GetType(TrTrainee), "ID", MatchType.Exact, txtNomorReg.Text.Trim))
                End If
                If ddlStatus.IsSelected Then
                    criterias.opAnd(New Criteria(GetType(TrTrainee), "Status", MatchType.Exact, ddlStatus.SelectedValue))
                End If
                criterias.opAnd(New Criteria(GetType(TrTrainee), "TrTraineeSalesmanHeader.JobPositionAreaID", MatchType.Exact, 2))
            Case "cs"
                If txtDealerCode.IsNotEmpty Then
                    Dim dealer As Dealer = CType(helpers.GetSession("Dealer"), Dealer)
                    If dealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
                        criterias.opAnd(New Criteria(GetType(TrTrainee), "TrTraineeSalesmanHeader.SalesmanHeader.Dealer.ID", MatchType.Exact, dealer.ID))
                    Else
                        Dim dealers As String = New System.Text.StringBuilder("('").Append(txtDealerCode.Text).Append("')").ToString().Replace(";", "','")
                        criterias.opAnd(New Criteria(GetType(TrTrainee), "TrTraineeSalesmanHeader.SalesmanHeader.Dealer.DealerCode", MatchType.InSet, dealers))
                    End If
                End If
                If txtPosisi.IsNotEmpty Then
                    If txtPosisi.Text.IndexOf(";") > -1 Then
                        criterias.opAnd(New Criteria(GetType(TrTrainee), "TrTraineeSalesmanHeader.JobPosition", MatchType.InSet, txtPosisi.Text.GenerateInSet(True)))
                    Else
                        criterias.opAnd(New Criteria(GetType(TrTrainee), "TrTraineeSalesmanHeader.JobPosition", MatchType.Exact, txtPosisi.Text.Trim))
                    End If
                End If
                If txtNomorReg.IsNotEmpty Then
                    criterias.opAnd(New Criteria(GetType(TrTrainee), "TrTraineeSalesmanHeader.SalesmanHeader.SalesmanCode", MatchType.Exact, txtNomorReg.Text.Trim))
                End If
                If ddlStatus.IsSelected Then
                    criterias.opAnd(New Criteria(GetType(TrTrainee), "TrTraineeSalesmanHeader.Status", MatchType.Exact, ddlStatus.SelectedValue))
                End If
                criterias.opAnd(New Criteria(GetType(TrTrainee), "TrTraineeSalesmanHeader.JobPositionAreaID", MatchType.Exact, 3))
        End Select


        If txtNama.IsNotEmpty Then
            criterias.opAnd(New Criteria(GetType(TrTrainee), "Name", MatchType.Partial, txtNama.Text.Trim))
        End If

        If txtArea.IsNotEmpty Then
            Dim area As String = New System.Text.StringBuilder("('").Append(txtArea.Text).Append("')").ToString().Replace(";", "','")
            criterias.opAnd(New Criteria(GetType(TrTrainee), "Dealer.Area1.AreaCode", MatchType.InSet, area))
        End If




        If txtCourseCodePass.IsNotEmpty Then
            Dim criteriaPass As Criteria = CreateCriteriaCourse(txtCourseCodePass.Text, True)

            If Not criteriaPass Is Nothing Then
                criterias.opAnd(criteriaPass)
            End If

        End If

        If txtCourseCodeNotPass.IsNotEmpty Then
            Dim criteriaPass As Criteria = CreateCriteriaCourse(txtCourseCodeNotPass.Text, True)

            If Not criteriaPass Is Nothing Then
                criterias.opAnd(criteriaPass)
            End If

        End If
        CreateSessionCriteria()

        Return criterias
    End Function

    Private Sub CreateSessionCriteria()
        Dim lastSession As Dictionary(Of String, String) = New Dictionary(Of String, String)

        lastSession("txtArea") = txtArea.Text
        lastSession("txtDealerCode") = txtDealerCode.Text
        lastSession("txtCourseCodePass") = txtCourseCodePass.Text
        lastSession("txtCourseCodeNotPass") = txtCourseCodeNotPass.Text
        lastSession("txtNomorReg") = txtNomorReg.Text
        lastSession("txtNama") = txtNama.Text
        lastSession("ddlStatus") = ddlStatus.SelectedValue
        lastSession("ddlJobPositionCategory") = ddlJobPositionCategory.SelectedValue
        lastSession("txtPosisi") = txtPosisi.Text

        helpers.SetSession(SessDataStatusSiswa, lastSession)

    End Sub

    Private Function CreateCriteriaJobPositionCategory(ByVal category As String) As Criteria
        Dim func As New JobPositionToCategoryFacade(Me.User)
        Select Case category
            Case "sales"
                Return New Criteria(GetType(TrTrainee), "RefJobPosition.ID", MatchType.InSet, func.RetrieveByCategoryID(1))
            Case "ass"
                If ddlJobPositionCategory.NotSelected() Then
                    Return New Criteria(GetType(TrTrainee), "RefJobPosition.ID", MatchType.InSet, func.RetrieveByArrCategoryID("(0,2,3)"))
                Else
                    Return New Criteria(GetType(TrTrainee), "RefJobPosition.ID", MatchType.InSet, func.RetrieveByCategoryID(CInt(ddlJobPositionCategory.SelectedValue)))
                End If
            Case "cs"
                Return New Criteria(GetType(TrTrainee), "RefJobPosition.ID", MatchType.InSet, func.RetrieveByCategoryID(4))
        End Select
    End Function

    Private Function CriteriaJobPositionCategory(ByVal type As Type, ByVal parameter As String) As Criteria
        Select Case hdnCategory.Value.ToLower()
            Case "sales"
                Return New Criteria(type, parameter, MatchType.Exact, 1)
            Case "ass"
                Return New Criteria(type, parameter, MatchType.InSet, "(0,2,3)")
            Case "cs"
                Return New Criteria(type, parameter, MatchType.Exact, 4)
        End Select
    End Function

    Private Function CreateCriteriaCourse(ByVal strFilter As String, ByVal isLulus As Boolean) As Criteria

        Dim stringCourseCode As List(Of String) = New List(Of String)

        Dim listCourse() As String = strFilter.Split(New Char() {";"}, StringSplitOptions.RemoveEmptyEntries)

        For Each courseCode As String In listCourse
            courseCode = courseCode.Trim()
            stringCourseCode.Add(courseCode)

            'cari replacement 
            Dim replacementHeader As TrReplacementHeader = New TrReplacementHeaderFacade(User).Retrieve(courseCode)

            If replacementHeader.ID <> 0 Then
                For Each detail As TrReplacementDetail In replacementHeader.ListOfDetail
                    stringCourseCode.Add(detail.TrCourse.CourseCode)
                Next
            End If
        Next

        If stringCourseCode.Count > 0 Then
            Dim stringInset As String = stringCourseCode.Distinct().GenerateInSet()

            Dim stringResult As String = "(select regis.TraineeID from TrClassRegistration regis"
            stringResult &= " LEFT JOIN TrClass class on regis.ClassID = class.ID "
            stringResult &= " LEFT JOIN TrCourse course on course.ID = class.CourseID"
            stringResult &= " where regis.RowStatus = 0 AND course.CourseCode IN " & stringInset

            If isLulus = True Then
                stringResult &= " AND regis.Status = 1)"
            Else
                stringResult &= " AND regis.Status <> 1)"
            End If


            Dim crit As Criteria = New Criteria(GetType(TrTrainee), "ID", MatchType.InSet, stringResult)
            Return crit
        Else

            Return Nothing
        End If

    End Function

    Private Function GenerateStringInset(ByVal listString As List(Of String)) As String
        Dim result As String = "("

        For i As Integer = 0 To listString.Count - 1

            result = result & "'" & listString(i) & "'"

            If i <> listString.Count - 1 Then
                result = result & ","
            End If

        Next

        result = result & ")"
        Return result
    End Function

    Private Sub dtgHeader_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgHeader.ItemCommand
        If (e.CommandName = "RegisterToClass") Then
            Try
                'SaveLastState()
                Dim objID As String = CType(e.Item.FindControl("lblNoReg"), Label).Text
                TraineeRedirect(String.Format(URL_REGISTER, hdnCategory.Value), CInt(objID))
            Catch
                MessageBox.Show("Terjadi kendala saat mendaftarkan siswa")
            End Try
        ElseIf e.CommandName = "Detail" Then
            Try
                'SaveLastState()
                Dim objID As String = CType(e.Item.FindControl("lblNoReg"), Label).Text
                TraineeRedirect(String.Format(URL_DETAIL, hdnCategory.Value), CInt(objID))
            Catch
                MessageBox.Show("Terjadi kendala saat menampilkan data siswa")
            End Try

        End If
    End Sub

    Private Sub TraineeRedirect(ByVal url As String, ByVal nIdTrainee As Integer)
        Dim selectedTrainee As TrTrainee = New TrTraineeFacade(User).Retrieve(nIdTrainee)
        helpers.SetSession("veTrainee", selectedTrainee)
        Response.Redirect(url)
    End Sub

    Private Sub dtgHeader_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgHeader.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            rowNumber = rowNumber + 1

            Dim RowValue As TrTrainee = CType(e.Item.DataItem, TrTrainee)

            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblNoReg As Label = CType(e.Item.FindControl("lblNoReg"), Label)
            Dim lblSalesmanCode As Label = CType(e.Item.FindControl("lblSalesmanCode"), Label)
            Dim lblNamaSiswa As Label = CType(e.Item.FindControl("lblNamaSiswa"), Label)
            Dim lblTanggalLahir As Label = CType(e.Item.FindControl("lblTanggalLahir"), Label)
            Dim lblGender As Label = CType(e.Item.FindControl("lblGender"), Label)
            Dim lblKodeDealerRegion As Label = CType(e.Item.FindControl("lblKodeDealerRegion"), Label)
            Dim lblMulaiBekerja As Label = CType(e.Item.FindControl("lblMulaiBekerja"), Label)
            Dim lblPosisi As Label = CType(e.Item.FindControl("lblPosisi"), Label)
            Dim lblPendidikan As Label = CType(e.Item.FindControl("lblPendidikan"), Label)
            Dim lblLevel As Label = CType(e.Item.FindControl("lblLevel"), Label)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim dtgDetail As DataGrid = CType(e.Item.FindControl("dtgDetail"), DataGrid)

            lblLevel.Text = ""
            lblNo.Text = e.CreateNumberPage
            lblNoReg.Text = RowValue.ID
            lblNamaSiswa.Text = RowValue.Name

            If Not RowValue.BirthDate.Year < 1900 Then
                lblTanggalLahir.Text = RowValue.BirthDate.DateToString
            End If

            If Not String.IsNullOrEmpty(RowValue.Gender.ToString()) Then
                If RowValue.Gender.ToString() = "1" Then
                    lblGender.Text = "Pria"
                ElseIf RowValue.Gender.ToString() = "2" Then
                    lblGender.Text = "Wanita"
                End If
            End If
            Dim enumTrainee As EnumTrTrainee = New EnumTrTrainee

            Select Case Request.QueryString("category").ToLower()
                Case "ass"
                    Dim funcH As TrTraineeLevelHeaderFacade = New TrTraineeLevelHeaderFacade(User)
                    Dim crits As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrTraineeLevelHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    crits.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrTraineeLevelHeader), "TrTrainee.ID", MatchType.Exact, RowValue.ID))

                    Dim arrDataLevel As ArrayList = funcH.Retrieve(crits)
                    If arrDataLevel.Count > 0 Then
                        Dim dataLevel As TrTraineeLevelHeader = CType(arrDataLevel(0), TrTraineeLevelHeader)
                        lblLevel.Text = dataLevel.TrTraineeLevel.Description
                    End If
                    If Not IsNothing(RowValue.Status) Then
                        lblStatus.Text = enumTrainee.StatusByIndex(CInt(RowValue.Status))
                    End If
                    lblKodeDealerRegion.Text = RowValue.Dealer.DealerCode
                    lblSalesmanCode.Visible = False
                    lblPosisi.Text = RowValue.JobPosition.ToString()
                Case "sales"
                    lblNoReg.Visible = False
                    If RowValue.ListTrTraineeSalesmanHeader.IsItems Then
                        Dim datas As List(Of TrTraineeSalesmanHeader) = RowValue.ListTrTraineeSalesmanHeader
                        Dim data As TrTraineeSalesmanHeader = datas.FirstOrDefault(Function(x) x.JobPositionAreaID = 1)
                        lblSalesmanCode.Text = data.SalesmanHeader.SalesmanCode
                        lblKodeDealerRegion.Text = data.SalesmanHeader.Dealer.DealerCode
                        lblPosisi.Text = data.RefJobPosition.Description
                        If data.SalesmanHeader.SalesmanLevel IsNot Nothing Then
                            lblLevel.Text = data.SalesmanHeader.SalesmanLevel.Description
                        End If
                        If Not IsNothing(data.Status) Then
                            lblStatus.Text = enumTrainee.StatusByIndex(data.Status)
                        End If
                    End If
                Case "cs"
                    lblNoReg.Visible = False
                    If RowValue.ListTrTraineeSalesmanHeader.IsItems Then
                        Dim datas As List(Of TrTraineeSalesmanHeader) = RowValue.ListTrTraineeSalesmanHeader
                        Dim data As TrTraineeSalesmanHeader = datas.FirstOrDefault(Function(x) x.JobPositionAreaID = 3)
                        lblSalesmanCode.Text = data.SalesmanHeader.SalesmanCode
                        lblKodeDealerRegion.Text = data.SalesmanHeader.Dealer.DealerCode
                        lblPosisi.Text = data.RefJobPosition.Description
                        If Not IsNothing(data.Status) Then
                            lblStatus.Text = enumTrainee.StatusByIndex(data.Status)
                        End If
                    End If
            End Select

            lblMulaiBekerja.Text = RowValue.StartWorkingDate.DateToString
            lblPendidikan.Text = RowValue.EducationLevel.ToString()

            dtgDetail.DataSource = GetDataTraining(RowValue.ID)
            dtgDetail.DataBind()

        End If

    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        Try
            BindDataGrid(0)
            btnPrint.Enabled = True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub SendConfirmationEmail()
        Dim appConfigFacade As New AppConfigFacade(User)
        Dim smtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim ObjEmail As DNetMail = New DNetMail(smtp)
        Dim objUserInfo As UserInfo = CType(Session("LOGINUSERINFO"), UserInfo)
        Dim contentEmail As String = GetEmailNotification()

        Dim emailTo As String = String.Empty

        Select Case Request.QueryString("category").ToLower()
            Case "sales"
                emailTo = appConfigFacade.Retrieve(TrainingCenterEmail.EMAIL_TO_TC_SALES).Value
            Case "ass"
                emailTo = appConfigFacade.Retrieve(TrainingCenterEmail.EMAIL_TO_TC_ASS).Value
            Case "cs"
                emailTo = appConfigFacade.Retrieve(TrainingCenterEmail.EMAIL_TO_TC_CS).Value
        End Select

        Dim emailFrom As String = objUserInfo.Email
        If emailFrom = "" Then
            emailFrom = appConfigFacade.Retrieve(TrainingCenterEmail.EMAIL_FROM_GENERAL).Value
        End If
        ObjEmail.sendMail(emailTo, "", emailFrom, "[MMKSI-DNet] Service - (Validitas data siswa Training)", Mail.MailFormat.Html, contentEmail)
    End Sub

    Private Function UpdateConfirmDate(ByVal strUpdate As String) As Boolean
        Dim objDealer As Dealer = CType(Session("DEALER"), Dealer)
        Dim objAlertConfirmationFacade As New KTB.DNet.BusinessFacade.AlertManagement.AlertConfirmationFacade(User)
        Dim arlAlertConfirmation As New ArrayList
        Dim bReturn As Boolean = False
        Dim idAlert As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("IdUpdateSiswaTraining"))

        arlAlertConfirmation = objAlertConfirmationFacade.RetrieveByAlertDealer(idAlert, objDealer.ID)
        If arlAlertConfirmation.Count > 0 Then
            For Each objAlertConfirmation As AlertConfirmation In arlAlertConfirmation
                Try
                    If strUpdate = "Yes" Then
                        objAlertConfirmation.ConfirmDate = Date.Now.AddMonths(3)
                    Else
                        objAlertConfirmation.ConfirmDate = Date.Now
                    End If
                    objAlertConfirmationFacade.Update(objAlertConfirmation)
                    bReturn = True
                Catch ex As Exception
                    bReturn = False
                End Try
            Next
        Else
            Dim objAC As New AlertConfirmation
            Dim objAlertMaster As New AlertMaster
            Dim objAlertMasterFacade As New KTB.DNet.BusinessFacade.AlertManagement.AlertMasterFacade(User)
            Try
                objAlertMaster = objAlertMasterFacade.Retrieve(idAlert)

                objAC.AlertMaster = objAlertMaster
                objAC.DealerID = objDealer.ID
                objAC.Status = 0
                objAC.RowStatus = 0
                If strUpdate = "Yes" Then
                    objAC.ConfirmDate = Date.Now.AddMonths(3)
                Else
                    objAC.ConfirmDate = Date.Now
                End If

                objAlertConfirmationFacade.Insert(objAC)
                bReturn = True
            Catch
                bReturn = False
            End Try
        End If
        Return bReturn
    End Function

    Private Function GetEmailNotification() As String
        Dim objDealer As Dealer = CType(Session("DEALER"), Dealer)
        Dim objUserInfo As UserInfo = CType(Session("LOGINUSERINFO"), UserInfo)
        Dim contentEmail As String

        contentEmail = "<P>Kepada Yth,</P>"
        contentEmail += "<P>PT.MMKSI-TC</P>"
        contentEmail += "<P>Ditempat</P>"
        contentEmail += "<P>&nbsp;</P>"
        contentEmail += "<P>Data status siswa " & objDealer.DealerName & " - " & objDealer.City.CityName & ",</P>"
        contentEmail += "<P>adalah valid per tanggal " & Date.Now.ToString("dd-MM-yyyy") & ".</P>"
        contentEmail += "<P>&nbsp;</P>"
        contentEmail += "<P>Regards,</P>"
        contentEmail += "<P>" & objUserInfo.UserName & "</P>"
        contentEmail += "<P>" & objDealer.DealerCode & " - " & objDealer.DealerName & "</P>"
        contentEmail += "<P>" & objDealer.City.CityName & "</P>"

        Return contentEmail
    End Function

    Protected Sub btnConfirmation_Click(sender As Object, e As EventArgs) Handles btnConfirmation.Click
        If UpdateConfirmDate("Yes") = True Then 'update data dealer for ConfirmTraineeDate
            SendConfirmationEmail() 'send confirmation email
            btnConfirmation.Enabled = False
        End If
    End Sub

    Protected Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim criteria As CriteriaComposite = CreateCriteria()
        Dim dataexcel As ArrayList = New TrTraineeFacade(User).Retrieve(criteria)
        Dim dicCtg As Dictionary(Of Integer, String) = New Dictionary(Of Integer, String)

        Dim criCtgCourse As New CriteriaComposite(New Criteria(GetType(TrCourseCategory), _
                      "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criCtgCourse.opAnd(GetType(TrCourseCategory), "IsMandatory", MatchType.Exact, "1")
        criCtgCourse.opAnd(CriteriaJobPositionCategory(GetType(TrCourseCategory), "JobPositionCategory.ID"))

        Dim arrCtgCourse As ArrayList = New TrCourseCategoryFacade(User).Retrieve(criCtgCourse)

        Dim excel As ExcelTemplate = New ExcelTemplate(Me.Page)
        excel.TimeOut = 300
        excel.FileName = "DataStatusSiswa.xls"
        excel.SheetName = "DataStatusSiswa"
        excel.Judul = "Data Status Siswa Training"
        excel.AddField(1, "No. Reg")
        excel.AddField(2, "Nama Siswa")
        excel.AddField(3, "Jenis Kelamin")
        excel.AddField(4, "Tanggal Lahir")
        excel.AddField(5, "Kode Dealer")
        excel.AddField(6, "Mulai Bekerja")
        excel.AddField(7, "Posisi")
        excel.AddField(8, "Pendidikan")
        excel.AddField(9, "Daftar Course")
        Dim strIdx As Integer = 10
        For Each itemCol As TrCourseCategory In arrCtgCourse
            excel.AddField(strIdx, String.Format("{0} belum lulus", itemCol.Code))
            dicCtg.Add(strIdx, itemCol.Code)
            strIdx = strIdx + 1
        Next
        excel.AddField(strIdx, "Tgl Lulus Junior")
        excel.AddField(strIdx + 1, "Tgl Lulus Senior")
        excel.AddField(strIdx + 2, "Tgl Lulus Master")
        excel.AddField(strIdx + 3, "Tgl Lulus Specialist E/G & F/S")
        excel.AddField(strIdx + 4, "Tgl Lulus Specialist D/T & E/C")
        excel.AddField(strIdx + 5, "Status")


        Dim index As Integer = 1
        For Each dataItem As TrTrainee In dataexcel
            Dim dataWajib As List(Of DetailTrainingWajib) = GetDataTraining(dataItem.ID)
            Dim dataTrainee As List(Of ExcelTemplateColumn) = New List(Of ExcelTemplateColumn)
            dataTrainee.Add(New ExcelTemplateColumn(1, dataItem.ID))
            dataTrainee.Add(New ExcelTemplateColumn(2, dataItem.Name))
            If dataItem.Gender.ToString() = "1" Then
                dataTrainee.Add(New ExcelTemplateColumn(3, "Pria"))
            ElseIf dataItem.Gender.ToString() = "2" Then
                dataTrainee.Add(New ExcelTemplateColumn(3, "Wanita"))
            End If

            'dataTrainee.Add(New ExcelTemplateColumn(3, IIf(dataItem.Gender.ToString() = "1", "Pria", "Wanita")))
            If dataItem.BirthDate.IsValid() Then
                dataTrainee.Add(New ExcelTemplateColumn(4, dataItem.BirthDate.ToString("dd/MM/yyyy")))
            End If


            dataTrainee.Add(New ExcelTemplateColumn(5, dataItem.Dealer.DealerCode))
            dataTrainee.Add(New ExcelTemplateColumn(6, dataItem.StartWorkingDate))
            dataTrainee.Add(New ExcelTemplateColumn(7, dataItem.JobPosition))
            dataTrainee.Add(New ExcelTemplateColumn(8, dataItem.EducationLevel))
            Dim listTraining As New List(Of String)
            Dim arrTraining As ArrayList = dataItem.TrClassRegistrations
            If arrTraining.Count > 0 Then
                listTraining = arrTraining.Cast(Of  _
                TrClassRegistration).Where(Function(x) x.Status = CType(EnumTrClassRegistration.DataStatusType.Pass, String)).Select(Function(y) _
                y.TrClass.TrCourse.CourseCode).ToList()

                If listTraining.Count > 0 Then
                    Dim dataTraining As String = String.Join(", ", listTraining.ToArray())
                    dataTrainee.Add(New ExcelTemplateColumn(9, dataTraining))
                End If
            End If

            For Each temCtg As DetailTrainingWajib In dataWajib
                Dim dataCtg As KeyValuePair(Of Integer, String) = dicCtg.FirstOrDefault(Function(x) x.Value.Equals(temCtg.CourseCategoryCode))
                If Not dataCtg.Equals(Nothing) Then
                    dataTrainee.Add(New ExcelTemplateColumn(dataCtg.Key, temCtg.CourseCategoryIsNotPass))
                End If
            Next
            Dim strIdxLulus As Integer = 10 + dicCtg.Count

            Dim funcD As TrTraineeLevelDetailFacade = New TrTraineeLevelDetailFacade(User)

            Dim crits As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrTraineeLevelDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crits.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrTraineeLevelDetail), "TrTrainee.ID", MatchType.Exact, dataItem.ID))

            Dim arrDataLevel As ArrayList = funcD.Retrieve(crits)
            For Each datalevel As TrTraineeLevelDetail In arrDataLevel
                Select Case datalevel.TrTraineeLevel.Sequence
                    Case 1
                        dataTrainee.Add(New ExcelTemplateColumn(strIdxLulus, datalevel.TanggalLulus.ToString("dd/MM/yyyy")))
                    Case 2
                        dataTrainee.Add(New ExcelTemplateColumn(strIdxLulus + 1, datalevel.TanggalLulus.ToString("dd/MM/yyyy")))
                    Case 3
                        dataTrainee.Add(New ExcelTemplateColumn(strIdxLulus + 2, datalevel.TanggalLulus.ToString("dd/MM/yyyy")))
                    Case 4
                        dataTrainee.Add(New ExcelTemplateColumn(strIdxLulus + 3, datalevel.TanggalLulus.ToString("dd/MM/yyyy")))
                    Case 5
                        dataTrainee.Add(New ExcelTemplateColumn(strIdxLulus + 4, datalevel.TanggalLulus.ToString("dd/MM/yyyy")))
                End Select
            Next
            dataTrainee.Add(New ExcelTemplateColumn(strIdxLulus + 5, New EnumTrTrainee().StatusByIndex(dataItem.Status)))

            excel.AddValue(index, dataTrainee)
            index = index + 1
        Next
        excel.DownLoad()
    End Sub

    Protected Sub btnBaru_Click(sender As Object, e As EventArgs) Handles btnBaru.Click
        Response.Redirect(String.Format("FrmNewTrTrainee.aspx?category={0}", Request.QueryString("category")))
    End Sub

    Private Sub dtgHeader_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgHeader.PageIndexChanged
        dtgHeader.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgHeader.CurrentPageIndex)
    End Sub

End Class