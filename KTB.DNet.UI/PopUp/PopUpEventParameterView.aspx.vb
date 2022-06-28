#Region "Custom Namespace Imports"
Imports KTB.DNet.BusinessFacade.Event
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System.IO
#End Region

Public Class PopUpEventParameterView
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblKodeDealer As System.Web.UI.WebControls.Label
    Protected WithEvents lblEventPeriod As System.Web.UI.WebControls.Label
    Protected WithEvents lblActivityType As System.Web.UI.WebControls.Label
    Protected WithEvents lblEventName As System.Web.UI.WebControls.Label
    Protected WithEvents lblSalesmanArea As System.Web.UI.WebControls.Label
    Protected WithEvents lblFileMaterial As System.Web.UI.WebControls.Label
    Protected WithEvents lblJuklak As System.Web.UI.WebControls.Label
    Protected WithEvents hplFilePendukung2 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hplFilePendukung3 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hplFilePendukung4 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hplFilePendukung5 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hplFilePendukung6 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hplFilePendukung7 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hplFilePendukung8 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hplFilePendukung9 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hplFilePendukung10 As System.Web.UI.WebControls.HyperLink
    Protected WithEvents hplFilePendukung1 As System.Web.UI.WebControls.HyperLink

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private ReadOnly Property GetID() As Int32
        Get
            Return Request.QueryString("id")
        End Get
    End Property

    Private Sub GetData()
        Dim objFacade As New EventParameterFacade(User)
        Dim objEvent As EventParameter = objFacade.Retrieve(GetID)
        lblKodeDealer.Text = objEvent.Dealer.DealerName
        If Not IsNothing(objEvent.SalesmanArea) Then
            lblSalesmanArea.Text = objEvent.SalesmanArea.AreaDesc
        End If
        lblEventPeriod.Text = String.Format("{0:dd/MM/yyyy} - {1:dd/MM/yyyy}", _
            objEvent.EventDateStart, objEvent.EventDateEnd)
        lblActivityType.Text = objEvent.ActivityType.ActivityName
        lblEventName.Text = objEvent.EventName
        lblFileMaterial.Text = objEvent.FileNameMaterial
        hplFilePendukung1.Text = objEvent.FileNamePendukung1
        hplFilePendukung1.NavigateUrl = String.Format("..\download.aspx?file={0}\{1}\{2}", _
            KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory"), objEvent.DirTarget, objEvent.FileNamePendukung1)
        hplFilePendukung2.Text = objEvent.FileNamePendukung2
        hplFilePendukung2.NavigateUrl = String.Format("..\download.aspx?file={0}\{1}\{2}", _
            KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory"), objEvent.DirTarget, objEvent.FileNamePendukung2)
        hplFilePendukung3.Text = objEvent.FileNamePendukung3
        hplFilePendukung3.NavigateUrl = String.Format("..\download.aspx?file={0}\{1}\{2}", _
            KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory"), objEvent.DirTarget, objEvent.FileNamePendukung3)
        hplFilePendukung4.Text = objEvent.FileNamePendukung4
        hplFilePendukung4.NavigateUrl = String.Format("..\download.aspx?file={0}\{1}\{2}", _
            KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory"), objEvent.DirTarget, objEvent.FileNamePendukung4)
        hplFilePendukung5.Text = objEvent.FileNamePendukung5
        hplFilePendukung5.NavigateUrl = String.Format("..\download.aspx?file={0}\{1}\{2}", _
            KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory"), objEvent.DirTarget, objEvent.FileNamePendukung5)
        hplFilePendukung6.Text = objEvent.FileNamePendukung6
        hplFilePendukung6.NavigateUrl = String.Format("..\download.aspx?file={0}\{1}\{2}", _
            KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory"), objEvent.DirTarget, objEvent.FileNamePendukung6)
        hplFilePendukung7.Text = objEvent.FileNamePendukung7
        hplFilePendukung7.NavigateUrl = String.Format("..\download.aspx?file={0}\{1}\{2}", _
            KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory"), objEvent.DirTarget, objEvent.FileNamePendukung7)
        hplFilePendukung8.Text = objEvent.FileNamePendukung8
        hplFilePendukung8.NavigateUrl = String.Format("..\download.aspx?file={0}\{1}\{2}", _
            KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory"), objEvent.DirTarget, objEvent.FileNamePendukung8)
        hplFilePendukung9.Text = objEvent.FileNamePendukung9
        hplFilePendukung9.NavigateUrl = String.Format("..\download.aspx?file={0}\{1}\{2}", _
            KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory"), objEvent.DirTarget, objEvent.FileNamePendukung9)
        hplFilePendukung10.Text = objEvent.FileNamePendukung10
        hplFilePendukung10.NavigateUrl = String.Format("..\download.aspx?file={0}\{1}\{2}", _
            KTB.DNet.Lib.WebConfig.GetValue("EventDestFileDirectory"), objEvent.DirTarget, objEvent.FileNamePendukung10)
        lblJuklak.Text = objEvent.FileNameJuklak
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then
            GetData()
        End If
    End Sub
End Class