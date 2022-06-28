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

Public Class FrmPendaftaranSalesDetail
    Inherits System.Web.UI.Page

    Private ReadOnly Property DealerCode As String
        Get
            Return Request.QueryString("dealercode")
        End Get
    End Property

    Private ReadOnly Property ClassCode As String
        Get
            Return Request.QueryString("classcode")
        End Get
    End Property

    Private ReadOnly Property Year As String
        Get
            Return Request.QueryString("year")
        End Get
    End Property

    Private ReadOnly Property Month As String
        Get
            Return Request.QueryString("month")
        End Get
    End Property

    Private ReadOnly Property Category As String
        Get
            Return Request.QueryString("cat")
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.PageNoCache()
        If Not IsPostBack Then
            TitleDescription()
            InitialForm()
        End If
    End Sub

    Private Sub TitleDescription()
        lblPageTitle.Text = "Training Sales - Detail Pendaftaran kelas"
    End Sub

    Private Sub InitialForm()
        Dim funcC As New TrClassFacade(User)
        Dim funcCA As New TrClassAllocationFacade(User)
        Dim funcCR As New TrClassRegistrationFacade(User)
        Dim dataKelas As TrClass = New TrClassFacade(User).Retrieve(Me.ClassCode)
        Dim dataAllo As New TrClassAllocation

        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrClassAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClassAllocation), "Dealer.DealerCode", MatchType.Exact, Me.DealerCode))
        criterias.opAnd(New Criteria(GetType(TrClassAllocation), "TrClass.ID", MatchType.Exact, dataKelas.ID))

        Dim arrAllo As ArrayList = funcCA.Retrieve(criterias)
        If arrAllo.IsItems Then
            dataAllo = CType(arrAllo(0), TrClassAllocation)
        End If

        lblClassCode.Text = dataKelas.ClassCode
        lblClassName.Text = dataKelas.ClassName
        lblStartDate.Text = dataKelas.StartDate.DateToString
        lblFinishDate.Text = dataKelas.FinishDate.DateToString
        lblLocation.Text = dataKelas.Location
        lblAllocatedTot.Text = dataAllo.Allocated.ToString

        Dim criteria As New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(TrClassRegistration), "Dealer.DealerCode", MatchType.Exact, Me.DealerCode))
        criteria.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.ID", MatchType.Exact, dataKelas.ID))

        dtgBooking.DataSource = funcCR.Retrieve(criteria)
        dtgBooking.DataBind()

    End Sub

    Protected Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Dim url As String = "FrmPendaftaranSales.aspx?year={0}&month={1}&cat={2}"
        Response.Redirect(String.Format(url, Me.Year, Me.Month, Me.Category))
    End Sub

    Private Sub dtgBooking_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dtgBooking.ItemDataBound
        If e.IsRowItems Then
            Dim data As TrClassRegistration = e.DataItem(Of TrClassRegistration)()
            Dim lblNo As Label = e.FindLabel("lblNo")
            Dim lblNoReg As Label = e.FindLabel("lblNoReg")
            Dim lblNamaSiswa As Label = e.FindLabel("lblNamaSiswa")
            Dim lblMulaiKerja As Label = e.FindLabel("lblMulaiKerja")
            Dim lblPosisi As Label = e.FindLabel("lblPosisi")
            Dim lblStatus As Label = e.FindLabel("lblStatus")

            lblNo.Text = e.CreateNumberPage()
            lblNoReg.Text = data.TrTrainee.ListTrTraineeSalesmanHeader.FirstOrDefault(Function(x) _
                            x.JobPositionAreaID = 1).SalesmanHeader.SalesmanCode
            lblNamaSiswa.Text = data.TrTrainee.Name
            lblMulaiKerja.Text = data.TrTrainee.StartWorkingDate.DateToString
            lblPosisi.Text = data.TrTrainee.ListTrTraineeSalesmanHeader.FirstOrDefault(Function(x) _
                            x.JobPositionAreaID = 1).RefJobPosition.Description
            lblStatus.Text = New EnumTrClassRegistration().StatusByIndex(CInt(data.Status))
        End If
    End Sub
End Class