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
Imports System.Web.HttpUtility


#End Region


Public Class FrmCourseRegistrationInputCS
    Inherits System.Web.UI.Page

    Private helpers As TrainingHelpers = New TrainingHelpers(Me.Page)
    Dim gridHeaderRowNumber As Integer = 0

    Private Sub TitleDescription()
        lblPageTitle.Text = "Training Customer Satisfaction - Input Pendaftaran"
        hdnCategory.Value = "cs"
    End Sub

    Private ReadOnly Property ClassId As Integer
        Get
            Return Request.QueryString("classId")
        End Get
    End Property

    Private ReadOnly Property FormReadOnly As String
        Get
            Return Request.QueryString("readonly")
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            TitleDescription()
            SetDescription()
            ViewState("CurrentSortColumn") = "ID"
            ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

            If FormReadOnly.Equals("0") Then
                DataTerdaftarBinding(True)
                If helpers.GetSession("idxpage") Is Nothing Then
                    DataGridBinding()
                Else
                    DataGridBinding(helpers.GetSession("idxpage"))
                End If

            Else
                DataTerdaftarBinding()
                dvBooking.Style.Add("height", "300px")
                trGridHeader.Visible = False
                trBtnTambah.Visible = False
                btnSimpan.Visible = False
            End If

        End If
    End Sub

    Private Sub SetDescription()
        Dim classData As TrClass = New TrClassFacade(User).Retrieve(ClassId)
        Dim course As TrCourse = classData.TrCourse
        lblKodeTraining.Text = course.CourseCode & " - " & course.CourseName
        lbltahunFiscal.Text = classData.FiscalYear
        lblKategori.Text = course.JobPositionCategory.Description
        lblClassCode.Text = classData.ClassCode

    End Sub

    Private Sub DataGridBinding(Optional ByVal index As Integer = 0)
        gridHeaderRowNumber = dtgHeader.PageSize * index
        Dim totalRow As Integer = 0
        Dim critJobPositionCategory As New CriteriaComposite(New Criteria(GetType(JobPositionToCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critJobPositionCategory.opAnd(New Criteria(GetType(JobPositionToCategory), "JobPositionCategory.JobPositionCategoryArea.ID", MatchType.Exact, 3))

        Dim jobPositions As List(Of String) = New List(Of String)()

        Dim critTrTrainee As New CriteriaComposite(New Criteria(GetType(TrTraineeSalesmanHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critTrTrainee.opAnd(New Criteria(GetType(TrTraineeSalesmanHeader), "Status", MatchType.Exact, CType(EnumTrTrainee.TrTraineeStatus.Active, Short)))
        critTrTrainee.opAnd(New Criteria(GetType(TrTraineeSalesmanHeader), "JobPositionAreaID", MatchType.Exact, 3))
        'critTrTrainee.opAnd(New Criteria(GetType(TrTrainee), "RefJobPosition.Category", MatchType.Exact, 4))
        'critTrTrainee.opAnd(New Criteria(GetType(TrTraineeSalesmanHeader), "ID", MatchType.InSet, "(SELECT TrTraineeID FROM TrTraineesalesmanheader WHERE JobPositionAreaID = 3 AND RowStatus = 0 AND Status = 1)"))



        Dim isMandatory As Boolean = False
        Dim dataCourse As TrCourse = New TrCourse()
        'If Not String.IsNullorEmpty(ClassId) Then
        Dim classData As TrClass = New TrClassFacade(User).Retrieve(ClassId)
        dataCourse = classData.TrCourse
        If dataCourse.Category.IsMandatory Then
            isMandatory = True
        End If

        critTrTrainee.opAnd(New Criteria(GetType(TrTraineeSalesmanHeader), "ID", MatchType.NotInSet, GetTraineeIDInSameCourse(classData)))

        'End If
        If isMandatory Then
            jobPositions = dataCourse.Category.ListOfJobPosition.Cast(Of  _
                    TrCourseCategoryToJobPosition).Select(Function(x) x.JobPosition.ID.ToString()).ToList()
        Else
            jobPositions = New JobPositionToCategoryFacade(User).Retrieve(critJobPositionCategory).Cast(Of  _
            JobPositionToCategory).Select(Function(x) x.JobPosition.ID.ToString()).ToList()
        End If
        If jobPositions.Count > 0 Then
            critTrTrainee.opAnd(New Criteria(GetType(TrTraineeSalesmanHeader), "RefJobPosition.ID", MatchType.InSet, jobPositions.GenerateInSet()))
        End If

        Dim dataSiswa As ArrayList = New TrTraineeSalesmanHeaderFacade(User).Retrieve(critTrTrainee)

        Dim dataValid As New ArrayList
        For Each itemdata As TrTraineeSalesmanHeader In dataSiswa
            Dim registrationstate As String = New TrBookingCourseFacade(User).GetCourseRegistrationState(itemdata.ID, dataCourse.ID, _
                                            classData.FiscalYear, classData.StartDate)

            If registrationstate.ToLower.Equals("belum terdaftar") Then
                dataValid.Add(itemdata.TrTrainee)
            End If
        Next


        dtgHeader.DataSource = dataValid
        dtgHeader.DataBind()
        helpers.SetSession("idxpage", dtgHeader.CurrentPageIndex)
    End Sub

    Private Function GetTraineeIDInSameCourse(classData As TrClass) As String
        Dim sqlQuery As String = String.Empty

        sqlQuery = "(SELECT regis.TraineeID FROM TrClassRegistration regis " & _
                    " LEFT JOIN TrClass class on regis.ClassID = class.ID " & _
                    " LEFT JOIN TrCourse course on class.CourseID = course.ID " & _
                    "         WHERE regis.RowStatus = 0 And Course.id = " & classData.TrCourse.ID & _
                    "         AND class.FiscalYear = '" & classData.FiscalYear & "'" & _
                    "         AND regis.status <> 3)" 'reject

        Return sqlQuery
    End Function


    Private Sub DataTerdaftarBinding(Optional ByVal isValidate As Boolean = False)
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.ID", MatchType.Exact, Me.ClassId))
        'criterias.opAnd(New Criteria(GetType(TrClassRegistration), "Status", MatchType.Exact, CInt(EnumTrClassRegistration.DataStatusType.Register)), "(", True)
        'criterias.opOr(New Criteria(GetType(TrClassRegistration), "Status", MatchType.Exact, CInt(EnumTrClassRegistration.DataStatusType.Reject)))
        'criterias.opOr(New Criteria(GetType(TrClassRegistration), "Status", MatchType.Exact, CInt(EnumTrClassRegistration.DataStatusType.Invite)), ")", False)

        Dim arlRegistration As ArrayList = New TrClassRegistrationFacade(User).Retrieve(criterias)

        helpers.SetSession("CountTerdaftar", arlRegistration.Count)
        helpers.SetSession("sessListCourseRegistrationDetailCS", arlRegistration)

        dtgBooking.DataSource = arlRegistration
        dtgBooking.DataBind()
    End Sub

    Private Sub dtgHeader_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgHeader.ItemDataBound
        If e.Item.DataItem IsNot Nothing Then
            Dim data As TrTrainee = CType(e.Item.DataItem, TrTrainee)
            Dim chkItemChecked As CheckBox = CType(e.Item.FindControl("chkItemChecked"), CheckBox)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblNoReg As Label = CType(e.Item.FindControl("lblNoReg"), Label)
            Dim lblNamaSiswa As Label = CType(e.Item.FindControl("lblNamaSiswa"), Label)
            Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
            Dim lblMulaiKerja As Label = CType(e.Item.FindControl("lblMulaiKerja"), Label)
            Dim lblPosisi As Label = CType(e.Item.FindControl("lblPosisi"), Label)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim hdnTrID As HiddenField = CType(e.Item.FindControl("hdnTrID"), HiddenField)


            Dim datas As List(Of TrTraineeSalesmanHeader) = data.ListTrTraineeSalesmanHeader
            Dim tSH As TrTraineeSalesmanHeader = datas.FirstOrDefault(Function(x) x.JobPositionAreaID = 3)

            hdnTrID.Value = data.ID.ToString
            lblNoReg.Text = tSH.SalesmanHeader.SalesmanCode
            lblNamaSiswa.Text = tSH.SalesmanHeader.Name
            lblDealerCode.Text = tSH.SalesmanHeader.Dealer.DealerCode
            lblMulaiKerja.Text = tSH.SalesmanHeader.HireDate.DateToString()
            Try
                lblPosisi.Text = tSH.SalesmanHeader.JobPosition.Description
            Catch ex As Exception
                lblPosisi.Text = String.Empty
            End Try

            Dim classData As TrClass = New TrClassFacade(User).Retrieve(ClassId)
            Dim dataCourse As TrCourse = classData.TrCourse
            Dim registrationState As String = New TrBookingCourseFacade(User).GetCourseRegistrationState(data.ID, dataCourse.ID, _
                                            classData.FiscalYear, classData.StartDate)
            lblStatus.Text = registrationState

            If Not registrationState.Equals("Belum Terdaftar") Then

                e.Item.Visible = False
            Else
                gridHeaderRowNumber += 1
            End If

            lblNo.Text = gridHeaderRowNumber

            If IsAlreadyInBooking(data.ID.ToString()) Then
                chkItemChecked.Visible = False
            End If
            'Dim listBooking As List(Of TrBookingCourse) = GetDataRegistration()
            'If listBooking.Where(Function(x) x.TrTrainee.ID = data.ID).Count > 0 Then
            '    If registrationState.Equals("Belum Terdaftar") Then
            '        lblStatus.Text = "Draft"
            '    End If
            '    chkItemChecked.Visible = False
            'End If

        End If

    End Sub

    Private Sub dtgHeader_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dtgHeader.PageIndexChanged
        dtgHeader.SelectedIndex = -1
        dtgHeader.CurrentPageIndex = e.NewPageIndex
        DataGridBinding(dtgHeader.CurrentPageIndex)
    End Sub

    Private Sub dtgHeader_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dtgHeader.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        End If
        dtgHeader.SelectedIndex = -1
        dtgHeader.CurrentPageIndex = 0
        DataGridBinding(dtgHeader.CurrentPageIndex)
    End Sub

    Protected Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Response.Redirect(String.Format("FrmCourseRegistrationCS.aspx"))
    End Sub

    Private Function Simpan() As List(Of TrClassRegistration)
        Dim listRegistration As List(Of TrClassRegistration) = GetDataRegistration()
        If listRegistration.Count.Equals(0) Then
            MessageBox.Show("Tambahkan data terlebih dahulu")
            Exit Function
        End If

        'Dim criterias As New CriteriaComposite(New Criteria(GetType(TrBookingCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criterias.opAnd(New Criteria(GetType(TrBookingCourse), "TrCourse.CourseCode", MatchType.Exact, Me.CourseCode))
        'criterias.opAnd(New Criteria(GetType(TrBookingCourse), "FiscalYear", MatchType.Exact, Me.FiscalYear))

        Dim func As TrClassRegistrationFacade = New TrClassRegistrationFacade(User)
        'Dim dataBooking As ArrayList = New TrBookingCourseFacade(User).Retrieve(criterias)
        'For Each dataExist As TrBookingCourse In dataBooking
        '    If listRegistration.Where(Function(x) x.ID = dataExist.ID).Count = 0 Then
        '        dataExist.RowStatus = CType(DBRowStatus.Deleted, Short)
        '        func.Update(dataExist)
        '    End If
        'Next

        For Each item As TrClassRegistration In listRegistration
            If item.ID.Equals(0) Then
                item.ID = func.Insert(item)
            Else
                func.Update(item)
            End If
        Next
        Return listRegistration
    End Function

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Dim listRegistration As List(Of TrClassRegistration) = Simpan()
        helpers.SetSession("CountTerdaftar", listRegistration.Count)
        helpers.SetSession("sessListCourseRegistrationDetailCS", listRegistration)
        dtgBooking.DataSource = listRegistration
        dtgBooking.DataBind()

        DataGridBinding(helpers.GetSession("idxpage"))

        MessageBox.Show(SR.SaveSuccess)
    End Sub

    Private Sub dtgBooking_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgBooking.ItemCommand
        Select e.CommandName.ToLower()
            Case "delete"
                Dim listData As List(Of TrClassRegistration) = GetDataRegistration()
                Dim hdnTrID As HiddenField = CType(e.Item.FindControl("hdnTrID"), HiddenField)
                'Dim lblNoReg As Label = CType(e.Item.FindControl("lblNoReg"), Label)
                'Dim dataCourse As TrCourse = New TrCourseFacade(User).Retrieve(Me.CourseCode)
                Dim dataRemove As TrClassRegistration = listData.FirstOrDefault(Function(x) x.TrTrainee.ID = CInt(hdnTrID.Value))

                If dataRemove.ID <> 0 Then
                    Dim facade As New TrClassRegistrationFacade(User)
                    facade.Delete(dataRemove)
                End If

                listData.Remove(dataRemove)
                helpers.SetSession("CountTerdaftar", listData.Count)
                dtgBooking.DataSource = listData
                dtgBooking.DataBind()
                DataGridBinding(dtgHeader.CurrentPageIndex)
            Case "confirm"
                Dim id As Integer = CInt(e.CommandArgument)
                Dim data As TrClassRegistration = New TrClassRegistrationFacade(User).Retrieve(id)
                ValidateRegistration(data)
                ConfirmRegistration(data)
                Dim listData As List(Of TrClassRegistration) = GetDataRegistration()
                dtgBooking.DataSource = listData
                dtgBooking.DataBind()
            Case "reject"
                Dim id As Integer = CInt(e.CommandArgument)
                Dim data As TrClassRegistration = New TrClassRegistrationFacade(User).Retrieve(id)
                RejectRegistration(data)
                Dim listData As List(Of TrClassRegistration) = GetDataRegistration()
                dtgBooking.DataSource = listData
                dtgBooking.DataBind()
        End Select

    End Sub

    Private Sub ValidateRegistration(data As TrClassRegistration)
        If Not IsClassStillHaveCapacity(data.TrClass) Then
            RejectRegistration(data)
            Throw New Exception("Maaf, peserta kelas " & data.TrClass.ClassCode & " sudah penuh.")
        End If
    End Sub

    Private Sub ConfirmRegistration(ByVal data As TrClassRegistration)
        Try
            data.Status = EnumTrClassRegistration.DataStatusType.Register
            data.RegistrationDate = DateTime.Now
            Dim facade As New TrClassRegistrationFacade(User)
            facade.Update(data)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub RejectRegistration(ByVal data As TrClassRegistration)
        Try
            data.Status = EnumTrClassRegistration.DataStatusType.Reject
            data.Notes = hdnRejectReason.Value
            Dim facade As New TrClassRegistrationFacade(User)
            facade.Update(data)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Function IsClassStillHaveCapacity(classData As TrClass) As Boolean
        Dim capacity As Integer = classData.Capacity

        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "Status", MatchType.Exact, CInt(EnumTrClassRegistration.DataStatusType.Register)))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.ID", MatchType.Exact, classData.ID))

        Dim arlTotalPeserta As ArrayList = New TrClassRegistrationFacade(User).Retrieve(criterias)

        If arlTotalPeserta.Count < capacity Then
            Return True
        Else
            Return False
        End If

    End Function

    Private Function IsAlreadyInBooking(ByVal regNumber As String) As Boolean
        Dim result As Boolean = False
        For Each item As DataGridItem In dtgBooking.Items
            Dim lblNoReg As Label = CType(item.FindControl("lblNoReg"), Label)
            If lblNoReg.Text = regNumber Then
                Return True
            End If
        Next
        Return result
    End Function

    Private Function GetDataRegistration() As List(Of TrClassRegistration)
        Dim result As List(Of TrClassRegistration) = New List(Of TrClassRegistration)
        For Each item As DataGridItem In dtgBooking.Items
            Dim lblNoReg As Label = CType(item.FindControl("lblNoReg"), Label)
            Dim hdnIDBooking As HiddenField = CType(item.FindControl("hdnIDBooking"), HiddenField)
            Dim data As TrClassRegistration = New TrClassRegistration()
            data.ID = CInt(hdnIDBooking.Value)
           
            If data.ID = 0 Then
                Dim trTraineeData As TrTrainee = New TrTraineeFacade(User).Retrieve(CInt(lblNoReg.Text))
                data.Dealer = trTraineeData.Dealer
                data.TrTrainee = trTraineeData
                data.TrClass = New TrClassFacade(User).Retrieve(Me.ClassId)
                data.Status = EnumTrClassRegistration.DataStatusType.Invite
                data.RegistrationDate = DateTime.Now.ToShortDateString()
            Else
                data = New TrClassRegistrationFacade(User).Retrieve(data.ID)
            End If

            result.Add(data)
        Next
        Return result
    End Function

    Private Sub dtgBooking_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgBooking.ItemDataBound
        If FormReadOnly.Equals("1") Then
            e.Item.Cells(8).Visible = False
        End If

        If e.Item.DataItem IsNot Nothing Then
            Dim data As TrClassRegistration = CType(e.Item.DataItem, TrClassRegistration)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblNoReg As Label = CType(e.Item.FindControl("lblNoReg"), Label)
            Dim lblNamaSiswa As Label = CType(e.Item.FindControl("lblNamaSiswa"), Label)
            Dim lblMulaiKerja As Label = CType(e.Item.FindControl("lblMulaiKerja"), Label)
            Dim lblPosisi As Label = CType(e.Item.FindControl("lblPosisi"), Label)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim lblDealerCode As Label = CType(e.Item.FindControl("lblDealerCode"), Label)
            Dim lblValidasi As Label = CType(e.Item.FindControl("lblValidasi"), Label)
            Dim lblNotes As Label = CType(e.Item.FindControl("lblNotes"), Label)
            Dim hdnTrID As HiddenField = CType(e.Item.FindControl("hdnTrID"), HiddenField)
           
            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            Dim btnConfirmSingle As LinkButton = CType(e.Item.FindControl("btnConfirmSingle"), LinkButton)
            Dim btnReject As LinkButton = CType(e.Item.FindControl("btnReject"), LinkButton)
            Dim hdnClass As HiddenField = CType(e.Item.FindControl("hdnClass"), HiddenField)
            Dim hdnIDBooking As HiddenField = CType(e.Item.FindControl("hdnIDBooking"), HiddenField)
            Dim hClassCode As HyperLink = CType(e.Item.FindControl("hKodeKelas"), HyperLink)

            btnReject.Attributes.Add("onclick", "if(!GetRejectReason()) return false;")

            Dim datas As List(Of TrTraineeSalesmanHeader) = data.TrTrainee.ListTrTraineeSalesmanHeader
            Dim tSH As TrTraineeSalesmanHeader = datas.FirstOrDefault(Function(x) x.JobPositionAreaID = 3)

            lblNo.Text = e.CreateNumberPage()
            hdnTrID.Value = data.TrTrainee.ID
            lblNoReg.Text = tSH.TrTrainee.ID
            lblNamaSiswa.Text = tSH.TrTrainee.Name
            lblDealerCode.Text = data.Dealer.DealerCode
            lblMulaiKerja.Text = tSH.SalesmanHeader.HireDate.DateToString()
            lblNotes.Text = data.Notes

            Try
                lblPosisi.Text = tSH.SalesmanHeader.JobPosition.Description
            Catch ex As Exception
                lblPosisi.Text = String.Empty
            End Try

            hdnIDBooking.Value = data.ID

            If Not e.Item.ItemIndex.Equals(0) Then
                Dim dataItem As DataGridItem = dtgBooking.Items(e.Item.ItemIndex - 1)
                Dim hdnClassID As HiddenField = CType(dataItem.FindControl("hdnClass"), HiddenField)
            End If

            If data.ID.Equals(0) Then
                lblStatus.Text = "Draft"
            Else

                Dim trTraineeSalesmanHeader As TrTraineeSalesmanHeader = data.TrTrainee.ListTrTraineeSalesmanHeader.FirstOrDefault(Function(x) x.JobPositionAreaID = 3)

                If trTraineeSalesmanHeader.Status = EnumTrTrainee.TrTraineeStatus.Deactive Then
                    e.Item.ForeColor = Color.White
                    e.Item.BackColor = Color.Gray
                    lblStatus.Text = "Resign"
                    lbtnDelete.Visible = False
                Else
                    If data.Status = EnumTrClassRegistration.DataStatusType.Invite Then
                        e.Item.BackColor = Color.Yellow
                        btnConfirmSingle.Visible = True
                        btnReject.Visible = True
                    ElseIf data.Status = EnumTrClassRegistration.DataStatusType.Register Then
                        e.Item.BackColor = Color.Cyan
                        lblValidasi.Text = data.RegistrationDate

                    ElseIf data.Status = EnumTrClassRegistration.DataStatusType.Reject Then
                        e.Item.BackColor = Color.OrangeRed
                    End If
                    If data.Status = EnumTrClassRegistration.DataStatusType.Invite Or data.Status = EnumTrClassRegistration.DataStatusType.Register Then
                        Dim ctgCourse As TrCourseCategory = data.TrClass.TrCourse.Category
                        If ctgCourse.IsMandatory Then
                            Dim listJobPosition As List(Of JobPosition) = ctgCourse.ListOfJobPosition.Cast(Of TrCourseCategoryToJobPosition).Select(Function(x) x.JobPosition).ToList()
                            If Not listJobPosition.Where(Function(x) x.ID = trTraineeSalesmanHeader.SalesmanHeader.JobPosition.ID).IsData Then
                                e.Item.BackColor = Color.MediumPurple
                            End If
                        End If
                    End If

                    hdnClass.Value = data.ID
                    Dim enumRegis As New EnumTrClassRegistration
                    lblStatus.Text = enumRegis.StatusByIndex(data.Status) & " - "
                    Dim classCode As String = data.TrClass.ClassCode
                    Dim actionValue As String = "popUpClassInformation('" + classCode + "');"
                    hClassCode.Text = classCode
                    hClassCode.NavigateUrl = "javascript:" + actionValue
                End If
            End If
        End If
    End Sub

    Protected Sub BtnTambah_Click(sender As Object, e As EventArgs) Handles BtnTambah.Click
        If Not isValidAdd.Value.IsNullorEmpty() Then
            isValidAdd.Value = String.Empty
            Exit Sub
        End If
        Dim listRegistration As List(Of TrClassRegistration) = New List(Of TrClassRegistration)
        Dim listData As List(Of TrClassRegistration) = GetDataRegistration()
        Dim classData As TrClass = New TrClassFacade(User).Retrieve(Me.ClassId)
        Dim isReady As Boolean = False
        For Each itemData As DataGridItem In dtgHeader.Items
            Dim chkItemChecked As CheckBox = CType(itemData.FindControl("chkItemChecked"), CheckBox)
            Dim lblNoReg As Label = CType(itemData.FindControl("lblNoReg"), Label)
            Dim hdnTrID As HiddenField = CType(itemData.FindControl("hdnTrID"), HiddenField)

            If chkItemChecked.Checked Then
                Dim dataItem As TrClassRegistration = New TrClassRegistration()
                dataItem.TrTrainee = New TrTraineeFacade(User).Retrieve(CInt(hdnTrID.Value))
                dataItem.TrClass = classData
                dataItem.Dealer = dataItem.TrTrainee.Dealer
                dataItem.RegistrationDate = DateTime.Now
                chkItemChecked.Checked = False

                If listData.Where(Function(x) x.TrTrainee.ID = dataItem.TrTrainee.ID).Count = 0 Then
                    If String.IsNullorEmpty(dataItem.TrTrainee.NoKTP) Then
                        MessageBox.Show(String.Format("Peserta {0} dengan No Registrasi {1} tidak memiliki data KTP", dataItem.TrTrainee.Name, dataItem.TrTrainee.ID))
                    Else
                        listRegistration.Add(dataItem)
                    End If
                End If
                isReady = True
            End If
        Next

        If Not isReady Then
            MessageBox.Show("Silahkan Pilih Siswa")
            Exit Sub
        End If

        If listRegistration.IsItems Then
            listData.AddRange(listRegistration)
        End If


        helpers.SetSession("CountTerdaftar", listData.Count)
        dtgBooking.DataSource = listData
        dtgBooking.DataBind()
        DataGridBinding(dtgHeader.CurrentPageIndex)
        MessageBox.Show("Berhasil ditambahkan")
    End Sub

    Private Function lbtnUp() As Object
        Throw New NotImplementedException
    End Function

    Private Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Response.Redirect("FrmDownloadCourseRegistrationDetailCS.aspx")
    End Sub
End Class