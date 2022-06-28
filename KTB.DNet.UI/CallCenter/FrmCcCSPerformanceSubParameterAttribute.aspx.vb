Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.CallCenter
Imports KTB.DNet.Domain
Imports KTB.DNet.Security

Public Class FrmCcCSPerformanceSubParameterAttribute
    Inherits System.Web.UI.Page


    Private ReadOnly Property CcCSPerformanceSubParameterID As Integer
        Get
            Return CInt(Page.Request.QueryString("SubID"))
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            InitiatePage()

        End If
    End Sub

    Private Sub InitiatePage()
        Dim dat As CcCSPerformanceSubParameter = New CcCSPerformanceSubParameterFacade(User).Retrieve(CcCSPerformanceSubParameterID)
        lblShowAttribute.Attributes("onclick") = "ShowAttribute(" & dat.CcCSPerformanceParameter.CcCustomerCategoryID & ");"
        lblKodeParameter.Text = dat.CcCSPerformanceParameter.Code
        lblKodeSubParameter.Text = dat.Code
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Try
            Dim objAttribute As CcCSPerformanceSubParameterAttribute = MappingDataFromUi()
            Dim facAttribute As CcCSPerformanceSubParameterAttributeFacade = New CcCSPerformanceSubParameterAttributeFacade(User)
            Dim lastId As Integer = facAttribute.Insert(objAttribute)

            If lastId < 0 Then
                Throw New Exception("data gagal disimpan")
            Else
                MessageBox.Show("Data berhasil disimpan")
            End If
        Catch ex As Exception
            MessageBox.Show("Terjadi error saat menyimpan data : " & ex.Message)
        End Try
    End Sub

    Private Function MappingDataFromUi() As CcCSPerformanceSubParameterAttribute
        Dim data As New CcCSPerformanceSubParameterAttribute
        data.CcCSPerformanceSubParameterID = CcCSPerformanceSubParameterID
        data.CcAttributeID = hdnAttributeID.Value

        Return data
    End Function

End Class