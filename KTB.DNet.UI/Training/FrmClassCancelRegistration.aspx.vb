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

Public Class FrmClassCancelRegistration
    Inherits System.Web.UI.Page

    Private ReadOnly Property AreaId As String
        Get
            Return Request.QueryString("area")
        End Get
    End Property

    Private ReadOnly Property DealerCode As String
        Get
            Return Request.QueryString("dealercode")
        End Get
    End Property

    Private ReadOnly Property CourseCode As String
        Get
            Return Request.QueryString("coursecode")
        End Get
    End Property

    Private ReadOnly Property ClassCode As String
        Get
            Return Request.QueryString("classCode")
        End Get
    End Property

    Private ReadOnly Property FiscalYear As String
        Get
            Return Request.QueryString("fiscalyear")
        End Get
    End Property

    Private ReadOnly Property TrBookingID As String
        Get
            Return Request.QueryString("bookingid")
        End Get
    End Property

    Private Sub TitleDescription(ByVal areaid As String)
        If areaid.Equals("1") Then
            lblPageTitle.Text = "Training Sales - Pertukaran Siswa"
            hdnCategory.Value = "sales"
        ElseIf areaid.Equals("2") Then
            lblPageTitle.Text = "Training After Sales - Pertukaran Siswa"
            hdnCategory.Value = "ass"
        ElseIf areaid.Equals("3") Then
            lblPageTitle.Text = "Training Customer Satisfaction - Pertukaran Siswa"
            hdnCategory.Value = "cs"
        Else
            lblPageTitle.Text = "Training - Pertukaran Siswa"
            hdnCategory.Value = "ass"
        End If
        
    End Sub

    Private Sub SetDetail()
        Dim dataBooking As TrBookingCourse = New TrBookingCourseFacade(User).Retrieve(CInt(TrBookingID))
        Dim dataClass As TrClass = New TrClassFacade(User).Retrieve(Me.ClassCode)
        If dataBooking IsNot Nothing And dataBooking.ID <> 0 Then
            txNoReg.Text = dataBooking.TrTrainee.ID
            txName.Text = dataBooking.TrTrainee.Name
            txtPosition.Text = dataBooking.TrTrainee.RefJobPosition.Description
            txtKodeKelas.Text = dataClass.ClassCode
            txtNamaKelas.Text = dataClass.ClassName
            If dataClass.TrMRTC IsNot Nothing Then
                txtLokasi.Text = dataClass.TrMRTC.Name
            Else
                txtLokasi.Text = dataClass.Location
            End If
            ICTanggalMulai.Value = dataClass.StartDate
            ICTanggalSelesai.Value = dataClass.FinishDate
        End If
        BindDDLTipe()

        txName.ReadOnly = True
        txNoReg.ReadOnly = True
        txtPosition.ReadOnly = True
        txtKodeKelas.ReadOnly = True
        txtNamaKelas.ReadOnly = True
        txtLokasi.ReadOnly = True
        ICTanggalMulai.Enabled = False
        ICTanggalSelesai.Enabled = False
        trPosisi.Visible = False
    End Sub

    Private Sub BindDDLTipe()
        ddlTipePertukaran.ClearSelection()
        ddlTipePertukaran.Items.Clear()

        ddlTipePertukaran.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        ddlTipePertukaran.Items.Add(New ListItem("Siswa kelas lain", "1"))
        ddlTipePertukaran.Items.Add(New ListItem("Siswa Hold", "2"))
        ddlTipePertukaran.SelectedValue = "-1"
    End Sub

    Private Sub BackPage()
        Dim url As String = String.Format("FrmCourseCancelRegistration.aspx?" & _
                                          "area={0}&dealercode={1}&coursecode={2}" & _
                                          "&fiscalyear={3}&classCode={4}", _
                                          Me.AreaId, Me.DealerCode, Me.CourseCode, Me.FiscalYear, _
                                          Me.ClassCode)
        Response.Redirect(url)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            TitleDescription(AreaId)
            SetDetail()

        End If
    End Sub

    Protected Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        BackPage()
    End Sub

    Protected Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click

    End Sub

    Protected Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        If Not ddlTipePertukaran.IsSelected Then
            MessageBox.Show("Tipe Pertukaran belum dipilih")
            Exit Sub
        End If

        If Not ddlSiswa.IsSelected Then
            MessageBox.Show("No.Reg Siswa belum dipilih")
            Exit Sub
        End If

        Dim funcClass As TrBookingClassFacade = New TrBookingClassFacade(User)
        Dim funcCourse As TrBookingCourseFacade = New TrBookingCourseFacade(User)
        Dim funcReg As TrClassRegistrationFacade = New TrClassRegistrationFacade(User)
        Dim funcBills As New TrBillingDetailFacade(User)

        Dim dataBookingCourse1 As TrBookingCourse = funcCourse.Retrieve(CInt(Me.TrBookingID))
        Dim dataBookingCourse2 As TrBookingCourse = funcCourse.Retrieve(CInt(ddlSiswa.SelectedValue))

        If ddlTipePertukaran.SelectedValue.Equals("1") Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(TrBookingClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TrBookingClass), "TrBookingCourse.ID", MatchType.Exact, Me.TrBookingID))
            criterias.opAnd(New Criteria(GetType(TrBookingClass), "TrClassRegistration.ID", MatchType.Exact, dataBookingCourse1.TrClassRegistration.ID))

            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(TrBookingClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(TrBookingClass), "TrBookingCourse.ID", MatchType.Exact, dataBookingCourse2.ID))
            criterias2.opAnd(New Criteria(GetType(TrBookingClass), "TrClassRegistration.ID", MatchType.Exact, dataBookingCourse2.TrClassRegistration.ID))

            Dim dataBookingClass As TrBookingClass = CType(funcClass.Retrieve(criterias)(0), TrBookingClass)
            Dim dataBookingClass2 As TrBookingClass = CType(funcClass.Retrieve(criterias2)(0), TrBookingClass)

            dataBookingClass.TrClass = dataBookingClass2.TrClass
            funcClass.Update(dataBookingClass)

            dataBookingClass2.TrClass = New TrClassFacade(User).Retrieve(Me.ClassCode)
            funcClass.Update(dataBookingClass2)

            Dim dataReg As TrClassRegistration = dataBookingCourse1.TrClassRegistration
            dataReg.TrClass = dataBookingClass.TrClass
            funcReg.Update(dataReg)

            Dim dataReg2 As TrClassRegistration = dataBookingCourse2.TrClassRegistration
            dataReg2.TrClass = dataBookingClass2.TrClass
            funcReg.Update(dataReg2)

        Else
            Dim criterias As New CriteriaComposite(New Criteria(GetType(TrBookingClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(TrBookingClass), "TrBookingCourse.ID", MatchType.Exact, Me.TrBookingID))
            criterias.opAnd(New Criteria(GetType(TrBookingClass), "TrClassRegistration.ID", MatchType.Exact, dataBookingCourse1.TrClassRegistration.ID))

            Dim dataBookingClass As TrBookingClass = CType(funcClass.Retrieve(criterias)(0), TrBookingClass)
            dataBookingClass.TrBookingCourse = dataBookingCourse2
            funcClass.Update(dataBookingClass)

            Dim dataReg As TrClassRegistration = dataBookingClass.TrClassRegistration
            dataReg.TrTrainee = dataBookingCourse2.TrTrainee
            funcReg.Update(dataReg)

            dataBookingCourse1.TrClassRegistration = Nothing
            funcCourse.Update(dataBookingCourse1)

            dataBookingCourse2.TrClassRegistration = dataReg
            funcCourse.Update(dataBookingCourse2)

        End If

        Dim crits As New CriteriaComposite(New Criteria(GetType(TrBillingDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crits.opAnd(New Criteria(GetType(TrBillingDetail), "TrBookingCourse.ID", MatchType.Exact, Me.TrBookingID))

        Dim billDetail As ArrayList = funcBills.Retrieve(crits)
        If billDetail.Count > 0 Then
            Dim objBills As TrBillingDetail = CType(billDetail(0), TrBillingDetail)
            objBills.TrBookingCourse = dataBookingCourse2
            funcBills.Update(objBills)
        End If


        Dim csType As Type = Me.GetType()
        Me.Page.ClientScript.RegisterStartupScript(csType, "windows-script", "FuncSuccess();", True)
       
    End Sub

    Private Sub ddlTipePertukaran_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTipePertukaran.SelectedIndexChanged
        Select Case ddlTipePertukaran.SelectedValue
            Case "-1"
                BindNothingSiswa()
            Case "1"
                Dim listSiswa As ListItemCollection = New ListItemCollection()
                Dim criterias As New CriteriaComposite(New Criteria(GetType(TrBookingCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(TrBookingCourse), "TrCourse.CourseCode", MatchType.Exact, Me.CourseCode))
                criterias.opAnd(New Criteria(GetType(TrBookingCourse), "Dealer.DealerCode", MatchType.Exact, Me.DealerCode))
                criterias.opAnd(New Criteria(GetType(TrBookingCourse), "TrClassRegistration.TrClass.ClassCode", MatchType.No, Me.ClassCode))
                criterias.opAnd(New Criteria(GetType(TrBookingCourse), "TrClassRegistration.TrClass.StartDate", MatchType.GreaterOrEqual, Me.DateNow))
                criterias.opAnd(New Criteria(GetType(TrBookingCourse), "FiscalYear", MatchType.Exact, Me.FiscalYear))

                Dim dataBooking As ArrayList = New TrBookingCourseFacade(User).Retrieve(criterias)
                For Each itemBooking As TrBookingCourse In dataBooking
                    listSiswa.Add(New ListItem(itemBooking.TrTrainee.ID.ToString() + " - " + itemBooking.TrTrainee.Name, itemBooking.ID))
                Next
                BindDDLSiswa(listSiswa)
            Case "2"
                Dim listSiswa As ListItemCollection = New ListItemCollection()
                Dim criterias As New CriteriaComposite(New Criteria(GetType(TrBookingCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(TrBookingCourse), "TrCourse.CourseCode", MatchType.Exact, Me.CourseCode))
                criterias.opAnd(New Criteria(GetType(TrBookingCourse), "Dealer.DealerCode", MatchType.Exact, Me.DealerCode))
                criterias.opAnd(New Criteria(GetType(TrBookingCourse), "TrClassRegistrationID", MatchType.IsNull, Nothing))
                criterias.opAnd(New Criteria(GetType(TrBookingCourse), "FiscalYear", MatchType.Exact, Me.FiscalYear))

                Dim dataBooking As ArrayList = New TrBookingCourseFacade(User).Retrieve(criterias)
                For Each itemBooking As TrBookingCourse In dataBooking
                    listSiswa.Add(New ListItem(itemBooking.TrTrainee.ID.ToString() + " - " + itemBooking.TrTrainee.Name, itemBooking.ID))
                Next
                BindDDLSiswa(listSiswa)
        End Select
    End Sub

    Private Sub BindNothingSiswa()
        ddlSiswa.ClearSelection()
        ddlSiswa.Items.Clear()
        ddlSiswa.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        txtPosisi2.Clear()
        trPosisi.NonVisible()
    End Sub

    Private Sub BindDDLSiswa(ByVal listSiswa As ListItemCollection)
        ddlSiswa.ClearSelection()
        ddlSiswa.Items.Clear()
        ddlSiswa.Items.Add(New ListItem("Silahkan Pilih", "-1"))
        For Each item As ListItem In listSiswa
            ddlSiswa.Items.Add(item)
        Next
        txtPosisi2.Clear()
        trPosisi.ActiveControl()
    End Sub

    Private Sub ddlSiswa_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSiswa.SelectedIndexChanged
        If ddlSiswa.IsSelected Then
            Dim dataBooking As TrBookingCourse = New TrBookingCourseFacade(User).Retrieve(CInt(ddlSiswa.SelectedValue))
            trPosisi.ActiveControl()
            txtPosisi2.Text = dataBooking.TrTrainee.RefJobPosition.Description
        Else
            trPosisi.NonVisible()
            txtPosisi2.Clear()
        End If
    End Sub
End Class