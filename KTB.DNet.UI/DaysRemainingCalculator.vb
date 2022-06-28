Imports System.Web.SessionState

Imports KTB.TAF.Domain
Imports KTB.TAF.BusinessFacade.General
Imports KTB.TAF.Domain.Search

Imports KTB.DNet.Utility
Imports Ktb.DNet.Security
Imports System.Security.Principal

Namespace KTB.DNet.UI

  Public Class DaysRemainingCalculator
    Implements System.Web.IHttpHandler

    Implements IRequiresSessionState

    Public ReadOnly Property IsReusable() As Boolean Implements System.Web.IHttpHandler.IsReusable
      Get
        Return True
      End Get
    End Property

    Public Sub ProcessRequest(ByVal context As System.Web.HttpContext) Implements System.Web.IHttpHandler.ProcessRequest
      Dim tempDate As String
      tempDate = context.Request.QueryString("TerminationDate")
      Dim delimStr = "/"
      Dim delimiter = delimStr.ToCharArray()
      Dim splitResult = tempDate.Split(delimiter, 3)
      Dim InstallmentDate As New DateTime(splitResult(2), splitResult(1), splitResult(0))

      Dim objContract As Contract = New ContractFacade(New System.Security.Principal.GenericPrincipal(New GenericIdentity("WEB"), Nothing)).Retrieve(CInt(context.Request.QueryString("ID")))
      Dim objLastSchedule As SceduleDetail = objContract.LastScheduleDetailByDate(InstallmentDate)
      Dim SisaHari As Integer
      Dim AngsuranTerakhir As String
      If Not objLastSchedule Is Nothing Then
        SisaHari = System.Math.Abs(DateDiff(DateInterval.Day, objLastSchedule.InstallmentDate, InstallmentDate))
        AngsuranTerakhir = objLastSchedule.InstallmentDate.ToString("dd/MM/yyyy")
      Else
        SisaHari = 0
        AngsuranTerakhir = ""
      End If

      Dim result As String
      result = SisaHari & "~" & AngsuranTerakhir
      context.Response.Write(result)
      context.Response.End()
    End Sub
  End Class
End Namespace