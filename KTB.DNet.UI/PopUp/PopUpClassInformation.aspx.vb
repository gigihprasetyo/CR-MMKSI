Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Utility

Public Class PopUpClassInformation
    Inherits System.Web.UI.Page

    Dim sHClass As SessionHelper = New SessionHelper

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblClassCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblClassName As System.Web.UI.WebControls.Label
    Protected WithEvents lblCourseID As System.Web.UI.WebControls.Label
    Protected WithEvents lblLocation As System.Web.UI.WebControls.Label
    Protected WithEvents lblTrainer1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTrainer2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblTrainer3 As System.Web.UI.WebControls.Label
    Protected WithEvents lblStartDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblFinishDate As System.Web.UI.WebControls.Label
    Protected WithEvents lblCapasity As System.Web.UI.WebControls.Label
    Protected WithEvents lblDescription As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents lblFiscalYear As System.Web.UI.WebControls.Label
    Protected WithEvents lblKota As System.Web.UI.WebControls.Label
    Protected WithEvents lblLocationName As System.Web.UI.WebControls.Label
    Protected WithEvents lblMRTC As System.Web.UI.WebControls.Label
    Protected WithEvents lblClassType As System.Web.UI.WebControls.Label
    Protected WithEvents lblPenginapan As System.Web.UI.WebControls.Label
    Protected WithEvents trMRTC As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents trPenginapan As System.Web.UI.HtmlControls.HtmlTableRow

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not IsPostBack Then
            InitiatePage()
        End If
    End Sub

    Private Sub InitiatePage()
        Dim parm As String = Request("kode")
        Dim objClass As TrClass = New TrClassFacade(User).Retrieve(parm)

        If Not objClass Is Nothing Then
            LoadClassInformation(objClass)
        Else
            MessageBox.Show(SR.DataNotFound("Kelas"))
        End If
    End Sub

    Private Sub LoadClassInformation(ByVal objClass As TrClass)
        lblClassCode.Text = objClass.ClassCode
        lblClassName.Text = objClass.ClassName
        lblCourseID.Text = objClass.TrCourse.CourseCode
        lblLocation.Text = objClass.Location
        lblTrainer1.Text = objClass.Trainer1
        lblTrainer2.Text = objClass.Trainer2
        lblTrainer3.Text = objClass.Trainer3
        lblStartDate.Text = objClass.StartDate.ToShortDateString
        lblFinishDate.Text = objClass.FinishDate.ToShortDateString
        lblCapasity.Text = objClass.Capacity.ToString
        lblDescription.Text = objClass.Description
        lblFiscalYear.Text = objClass.FiscalYear
        lblLocationName.Text = objClass.LocationName
        lblPenginapan.Text = objClass.Lodging
        lblClassType.Text = EnumTrClass.GetStringValueClassType(objClass.ClassType)

        If objClass.City IsNot Nothing Then
            lblKota.Text = objClass.City.CityName
        End If

        If objClass.Status.Equals("1") Then
            lblStatus.Text = "Aktif"
        Else
            lblStatus.Text = "Tidak Aktif"
        End If

        If objClass.TrMRTC IsNot Nothing And objClass.TrCourse.JobPositionCategory.AreaID = 2 Then
            lblMRTC.Text = objClass.TrMRTC.Dealer.DealerName
        Else
            trMRTC.Visible = False
            trPenginapan.Visible = False
        End If

    End Sub

End Class
