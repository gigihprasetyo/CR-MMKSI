#Region "EntLib Library"

#End Region

#Region "Custom Library"
Imports KTB.DNet.Domain
Imports KTB.DNet.Utility
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.UserManagement
#End Region

#Region ".Net Library"
Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Principal
Imports System.Configuration
#End Region

Public Class FrmDNETSurvey
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents rfvNamaKepalaCabang As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtNamaKepalaCabang As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEMailKepalaCabang As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvEMailKepalaCabang As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents revEMailKepalaCabang As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents txtNamaSalesManager As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvNamaSalesManager As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtEMailSalesManager As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvEMailSalesManager As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents revEMailSalesManager As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents txtNamaSparepartManager As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvNamaSparepartManager As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtEMailSparepartManager As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvEMailSparepartManager As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents revEMailSparepartManager As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents txtNamaServiceManager As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvNamaServiceManager As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtEMailServiceManager As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvEMailServiceManager As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents revEMailServiceManager As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents optOfficer As System.Web.UI.WebControls.RadioButtonList
    Protected WithEvents optSolusi As System.Web.UI.WebControls.RadioButtonList
    Protected WithEvents optJawaban As System.Web.UI.WebControls.RadioButtonList
    Protected WithEvents txtSaran As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvSaran As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents optModul As System.Web.UI.WebControls.RadioButtonList
    Protected WithEvents optAkses As System.Web.UI.WebControls.RadioButtonList
    Protected WithEvents txtFitur As System.Web.UI.WebControls.TextBox
    Protected WithEvents rfvFitur As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents rfvModul As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtModul As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblUserName As System.Web.UI.WebControls.Label
    Protected WithEvents lblID As System.Web.UI.WebControls.Label
    Protected WithEvents lblErrors As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Fields"
    Private ConnectionString As String
    Private UrlToNextPage As String = "default_general.aspx?type="
#End Region

#Region "Event Handlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ConnectionString = KTB.DNet.Lib.WebConfig.GetValue("DS")
        UrlToNextPage = UrlToNextPage & Request.QueryString("type")

        If ConnectionString Is Nothing Then
            Response.Redirect(UrlToNextPage)
        Else
            If ConnectionString.ToString.Trim = "" Then
                Response.Redirect(UrlToNextPage)
            Else
                If Not Page.IsPostBack Then
                    LoadList()
                    InitiatePage()
                    If isAlreadyEntry() Then
                        Response.Redirect(UrlToNextPage)
                    End If
                    MessageBox.Show("Mohon maaf kenyamanan anda terganggu, kami meminta feedback dari anda untuk pengembangan D-NET.\nTerima Kasih")
                End If
            End If

        End If
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not rfvNamaKepalaCabang.IsValid Then
            MessageBox.Show(rfvNamaKepalaCabang.ErrorMessage)
            Return
        End If
        If Not rfvEMailKepalaCabang.IsValid Then
            MessageBox.Show(rfvEMailKepalaCabang.ErrorMessage)
            Return
        End If
        If Not revEMailKepalaCabang.IsValid Then
            MessageBox.Show(revEMailKepalaCabang.ErrorMessage)
            Return
        End If

        If Not rfvNamaSalesManager.IsValid Then
            MessageBox.Show(rfvNamaSalesManager.ErrorMessage)
            Return
        End If
        If Not rfvEMailSalesManager.IsValid Then
            MessageBox.Show(rfvEMailSalesManager.ErrorMessage)
            Return
        End If
        If Not revEMailSalesManager.IsValid Then
            MessageBox.Show(revEMailSalesManager.ErrorMessage)
            Return
        End If

        If Not rfvNamaServiceManager.IsValid Then
            MessageBox.Show(rfvNamaServiceManager.ErrorMessage)
            Return
        End If
        If Not rfvEMailServiceManager.IsValid Then
            MessageBox.Show(rfvEMailServiceManager.ErrorMessage)
            Return
        End If
        If Not revEMailServiceManager.IsValid Then
            MessageBox.Show(revEMailServiceManager.ErrorMessage)
            Return
        End If

        If Not rfvNamaSparepartManager.IsValid Then
            MessageBox.Show(rfvNamaSparepartManager.ErrorMessage)
            Return
        End If
        If Not rfvEMailSparepartManager.IsValid Then
            MessageBox.Show(rfvEMailSparepartManager.ErrorMessage)
            Return
        End If
        If Not revEMailSparepartManager.IsValid Then
            MessageBox.Show(revEMailSparepartManager.ErrorMessage)
            Return
        End If

        If Not rfvSaran.IsValid Then
            MessageBox.Show(rfvSaran.ErrorMessage)
            Return
        End If
        If Not rfvModul.IsValid Then
            MessageBox.Show(rfvModul.ErrorMessage)
            Return
        End If
        If Not rfvFitur.IsValid Then
            MessageBox.Show(rfvFitur.ErrorMessage)
            Return
        End If

        Dim userid As Integer = CType(ViewState("UserID"), Integer)
        Dim dealercode As String = lblDealerCode.Text
        Dim sendip As String = Request.UserHostAddress
        Dim kcname As String = txtNamaKepalaCabang.Text.Trim()
        Dim kcemail As String = txtEMailKepalaCabang.Text.Trim()
        Dim slsname As String = txtNamaSalesManager.Text.Trim()
        Dim slsemail As String = txtEMailSalesManager.Text.Trim()
        Dim svcname As String = txtNamaServiceManager.Text.Trim()
        Dim svcemail As String = txtEMailServiceManager.Text.Trim()
        Dim sptname As String = txtNamaSparepartManager.Text.Trim()
        Dim sptemail As String = txtEMailSparepartManager.Text.Trim()
        Dim saran As String = txtSaran.Text
        Dim modul As String = txtModul.Text
        Dim fitur As String = txtFitur.Text
        Dim nofficer As Short = CType(optOfficer.SelectedValue, Short)
        Dim nsolusi As Short = CType(optSolusi.SelectedValue, Short)
        Dim njawaban As Short = CType(optJawaban.SelectedValue, Short)
        Dim nmodul As Short = CType(optModul.SelectedValue, Short)
        Dim nakses As Short = CType(optAkses.SelectedValue, Short)

        Dim retval As Integer = 0
        Dim oCn As SqlConnection = New SqlConnection(ConnectionString)
        Dim oCmd As SqlCommand = New SqlCommand("SaveSurvey", oCn)
        oCmd.CommandType = CommandType.StoredProcedure

        oCmd.Parameters.Add(New SqlParameter("@RETURN_VALUE", SqlDbType.Int, 0))
        oCmd.Parameters.Add(New SqlParameter("@NewID", SqlDbType.Int, 0))
        oCmd.Parameters.Add(New SqlParameter("@ID", SqlDbType.Int, 0))
        oCmd.Parameters.Add(New SqlParameter("@UserID", SqlDbType.Int))
        'oCmd.Parameters.Add(New SqlParameter("@DealerCode", SqlDbType.VarChar, 10))
        oCmd.Parameters.Add(New SqlParameter("@NamaKepalaCabang", SqlDbType.VarChar, 50))
        oCmd.Parameters.Add(New SqlParameter("@EMailKepalaCabang", SqlDbType.VarChar, 50))
        oCmd.Parameters.Add(New SqlParameter("@NamaSalesManager", SqlDbType.VarChar, 50))
        oCmd.Parameters.Add(New SqlParameter("@EMailSalesManager", SqlDbType.VarChar, 50))
        oCmd.Parameters.Add(New SqlParameter("@NamaServiceManager", SqlDbType.VarChar, 50))
        oCmd.Parameters.Add(New SqlParameter("@EMailServiceManager", SqlDbType.VarChar, 50))
        oCmd.Parameters.Add(New SqlParameter("@NamaSparepartManager", SqlDbType.VarChar, 50))
        oCmd.Parameters.Add(New SqlParameter("@EMailSparepartManager", SqlDbType.VarChar, 50))
        oCmd.Parameters.Add(New SqlParameter("@HelpDeskSaran", SqlDbType.Text))
        oCmd.Parameters.Add(New SqlParameter("@HelpDeskOfficer", SqlDbType.SmallInt))
        oCmd.Parameters.Add(New SqlParameter("@HelpDeskSolusi", SqlDbType.SmallInt))
        oCmd.Parameters.Add(New SqlParameter("@HelpDeskJawaban", SqlDbType.SmallInt))
        oCmd.Parameters.Add(New SqlParameter("@FiturSuggestion", SqlDbType.Text))
        oCmd.Parameters.Add(New SqlParameter("@FiturModul", SqlDbType.SmallInt))
        oCmd.Parameters.Add(New SqlParameter("@FiturAkses", SqlDbType.SmallInt))
        oCmd.Parameters.Add(New SqlParameter("@FiturModulLambat", SqlDbType.VarChar, 255))
        oCmd.Parameters.Add(New SqlParameter("@SendBy", SqlDbType.VarChar))
        oCmd.Parameters.Add(New SqlParameter("@SendDate", SqlDbType.DateTime))
        oCmd.Parameters.Add(New SqlParameter("@SendIP", SqlDbType.VarChar))

        oCmd.Parameters("@RETURN_VALUE").Direction = ParameterDirection.ReturnValue
        oCmd.Parameters("@NewID").Direction = ParameterDirection.Output
        oCmd.Parameters("@ID").Value = retval
        oCmd.Parameters("@UserID").Value = userid
        'oCmd.Parameters("@DealerCode").Value = dealercode
        oCmd.Parameters("@NamaKepalaCabang").Value = kcname
        oCmd.Parameters("@EMailKepalaCabang").Value = kcemail
        oCmd.Parameters("@NamaSalesManager").Value = slsname
        oCmd.Parameters("@EMailSalesManager").Value = slsemail
        oCmd.Parameters("@NamaServiceManager").Value = svcname
        oCmd.Parameters("@EMailServiceManager").Value = svcemail
        oCmd.Parameters("@NamaSparepartManager").Value = sptname
        oCmd.Parameters("@EMailSparepartManager").Value = sptemail
        oCmd.Parameters("@HelpDeskSaran").Value = saran
        oCmd.Parameters("@HelpDeskOfficer").Value = nofficer
        oCmd.Parameters("@HelpDeskSolusi").Value = nsolusi
        oCmd.Parameters("@HelpDeskJawaban").Value = njawaban
        oCmd.Parameters("@FiturSuggestion").Value = fitur
        oCmd.Parameters("@FiturModulLambat").Value = modul
        oCmd.Parameters("@FiturModul").Value = nmodul
        oCmd.Parameters("@FiturAkses").Value = nakses
        oCmd.Parameters("@SendBy").Value = User.Identity.Name
        oCmd.Parameters("@SendDate").Value = DateTime.Now
        oCmd.Parameters("@SendIP").Value = sendip

        Dim success As Integer = 0
        Try
            Dim sRet As String = ""
            Dim nRet As Integer = 0

            oCn.Open()
            success = oCmd.ExecuteNonQuery()
            sRet = oCmd.Parameters("@RETURN_VALUE").Value.ToString()
            nRet = Integer.Parse(oCmd.Parameters("@NewID").Value.ToString())

            If ((nRet > 0) And (sRet = "0")) Then
                retval = nRet
            Else
                'retval = Key
            End If
        Catch 'ex As Exception
            'lblErrors.Text = ex.ToString()
        Finally
            If (oCn.State = ConnectionState.Open) Then oCn.Close()
        End Try

        oCmd.Dispose() : oCmd = Nothing
        oCn.Dispose() : oCn = Nothing

        If retval > 0 Then
            MessageBox.Show("Terima kasih atas kesediaan anda dalam mengisi form survey ini.\nTerima Kasih")
            'Response.Redirect("default_general.aspx?type=" & Request.QueryString("type").ToString())
            Response.Redirect(UrlToNextPage)
        End If
    End Sub
#End Region

#Region "Methods"
    Private Function isAlreadyEntry() As Boolean
        'Dim retval As Boolean = False
        Dim userid As Integer = CType(ViewState("UserID"), Integer)
        Dim oCn As SqlConnection = New SqlConnection(ConnectionString)
        Dim oCmd As SqlCommand = New SqlCommand("CheckSurveyEntrier", oCn)

        oCmd.CommandType = CommandType.StoredProcedure

        oCmd.Parameters.Add(New SqlParameter("@result", SqlDbType.Int, 0))
        oCmd.Parameters.Add(New SqlParameter("@UserID", SqlDbType.Int))

        oCmd.Parameters("@result").Direction = ParameterDirection.Output
        oCmd.Parameters("@UserID").Value = userid

        Dim result As Integer = 0
        Try
            oCn.Open()
            If oCmd.ExecuteNonQuery() <> 0 Then
                result = Integer.Parse(oCmd.Parameters("@result").Value.ToString())
            End If
        Catch ex As Exception
            lblErrors.Text = ex.ToString()
        Finally
            If (oCn.State = ConnectionState.Open) Then oCn.Close()
        End Try

        oCmd.Dispose() : oCmd = Nothing
        oCn.Dispose() : oCn = Nothing

        If result > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub InitiatePage()
        Dim objUser As UserInfo = CType(Session("LOGINUSERINFO"), UserInfo)
        ViewState("UserID") = 0
        If Not IsNothing(objUser) Then
            ViewState("UserID") = objUser.ID
            'lblID.Text = objUser.ID.ToString()

            ' for D-NET
            lblUserName.Text = User.Identity.Name.Substring(6)
            If Not IsNothing(objUser.Dealer) Then
            	
            	lblDealerCode.Text = objUser.Dealer.DealerCode
            	lblDealerName.Text = objUser.Dealer.DealerName + "/" + objUser.Dealer.SearchTerm1
            End If
        End If
    End Sub

    Private Sub LoadList()
        optOfficer.DataSource = DataAnswerList()
        optOfficer.DataBind()
        optOfficer.SelectedIndex = 0

        optSolusi.DataSource = DataAnswerList()
        optSolusi.DataBind()
        optSolusi.SelectedIndex = 0

        optJawaban.DataSource = DataAnswerList()
        optJawaban.DataBind()
        optJawaban.SelectedIndex = 0

        optModul.DataSource = DataAnswerList("Sebagian")
        optModul.DataBind()
        optModul.SelectedIndex = 0

        optAkses.DataSource = DataAnswerList()
        optAkses.DataBind()
        optAkses.SelectedIndex = 0
    End Sub

    Private Function DataAnswerList() As ListItemCollection
        Return DataAnswerList("Kadang-kadang")
    End Function

    Private Function DataAnswerList(ByVal sMiddle As String) As ListItemCollection
        Dim Retval As New ListItemCollection

        Retval.Add(New ListItem("Ya", "1"))
        Retval.Add(New ListItem(sMiddle, "2"))
        Retval.Add(New ListItem("Tidak", "0"))

        Return Retval
    End Function
#End Region
End Class
