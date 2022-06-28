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

Public Class FrmCourseCancelRegistration
    Inherits System.Web.UI.Page

    Private helpers As TrainingHelpers = New TrainingHelpers(Me.Page)

    Private Sub TitleDescription(ByVal areaid As String)
        If areaid.Equals("1") Then
            lblPageTitle.Text = "Training Sales - Detail Pendaftaran kelas"
            hdnCategory.Value = "sales"
        ElseIf areaid.Equals("2") Then
            lblPageTitle.Text = "Training After Sales - Detail Pendaftaran kelas"
            hdnCategory.Value = "ass"
        ElseIf areaid.Equals("3") Then
            lblPageTitle.Text = "Training Customer Satisfaction - Detail Pendaftaran kelas"
            hdnCategory.Value = "cs"
        Else
            lblPageTitle.Text = "Training - Input Pendaftaran"
            hdnCategory.Value = "ass"
        End If
    End Sub

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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            TitleDescription(AreaId)
            ViewState("CurrentSortColumn") = "ID"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC

            DataTerdaftarBinding()
        End If
    End Sub

    Private Sub DataTerdaftarBinding()
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrBookingCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrBookingCourse), "Dealer.DealerCode", MatchType.Exact, Me.DealerCode))
        criterias.opAnd(New Criteria(GetType(TrBookingCourse), "TrCourse.CourseCode", MatchType.Exact, Me.CourseCode))
        criterias.opAnd(New Criteria(GetType(TrBookingCourse), "FiscalYear", MatchType.Exact, Me.FiscalYear))
        criterias.opAnd(New Criteria(GetType(TrBookingCourse), "TrClassRegistration.TrClass.ClassCode", MatchType.Exact, Me.ClassCode))

        Dim dataBooking As List(Of TrBookingCourse) = New TrBookingCourseFacade(User).Retrieve(criterias).Cast(Of  _
            TrBookingCourse).OrderBy(Function(x) x.PrioritySequence).ToList()
        dtgBooking.DataSource = dataBooking
        dtgBooking.DataBind()

    End Sub

    Private Sub dtgBooking_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dtgBooking.ItemCommand
        Select Case e.CommandName.ToLower()
            Case "cancel"
                Dim hdnIDBooking As HiddenField = CType(e.Item.FindControl("hdnIDBooking"), HiddenField)
                Dim url As String = String.Format("FrmClassCancelRegistration.aspx?" & _
                                          "area={0}&dealercode={1}&coursecode={2}" & _
                                          "&fiscalyear={3}&classCode={4}&bookingid={5}", _
                                          Me.AreaId, Me.DealerCode, Me.CourseCode, Me.FiscalYear, _
                                          Me.ClassCode, hdnIDBooking.Value)
                Response.Redirect(url)
        End Select
    End Sub

    Private Sub dtgBooking_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgBooking.ItemDataBound
        If e.Item.DataItem IsNot Nothing Then
            Dim data As TrBookingCourse = CType(e.Item.DataItem, TrBookingCourse)
            Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
            Dim lblNoReg As Label = CType(e.Item.FindControl("lblNoReg"), Label)
            Dim lblNamaSiswa As Label = CType(e.Item.FindControl("lblNamaSiswa"), Label)
            Dim lblMulaiKerja As Label = CType(e.Item.FindControl("lblMulaiKerja"), Label)
            Dim lblPosisi As Label = CType(e.Item.FindControl("lblPosisi"), Label)
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            Dim lbtnDelete As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
            Dim hdnClass As HiddenField = CType(e.Item.FindControl("hdnClass"), HiddenField)
            Dim hdnIDBooking As HiddenField = CType(e.Item.FindControl("hdnIDBooking"), HiddenField)
            Dim hClassCode As HyperLink = CType(e.Item.FindControl("hKodeKelas"), HyperLink)

            lblNo.Text = e.CreateNumberPage
            lblNoReg.Text = data.TrTrainee.ID
            lblNamaSiswa.Text = data.TrTrainee.Name
            lblMulaiKerja.Text = data.TrTrainee.StartWorkingDate.ToString("dd/MM/yyyy")
            lblPosisi.Text = data.TrTrainee.RefJobPosition.Description
            hdnIDBooking.Value = data.ID

            If data.TrClassRegistration.TrClass.TrCourse.PaymentType = CType(EnumTrCourse.PaymentType.CHARGE, Short) Then
                If Not New TrBillingDetailFacade(User).RetrieveByBookingID(data.ID).ID.Equals(0) Then
                    Dim objBill As TrBillingDetail = New TrBillingDetailFacade(User).RetrieveByBookingID(data.ID)
                    If Not objBill.TrBillingHeader.Status = EnumTagihanTraining.TagihanStatus.Selesai Then
                        lbtnDelete.Visible = False
                    End If
                End If
            End If

            If data.TrClassRegistration.TrClass.StartDate.AddDays(1) <= DateTime.Now.DateToString() Then
                lbtnDelete.Visible = False
            End If

        End If
    End Sub

    Protected Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Response.Redirect("FrmCourseRegistration.aspx?area=" + AreaId)
    End Sub

End Class