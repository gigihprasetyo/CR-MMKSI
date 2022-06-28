Imports System.Configuration
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.Lib
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search

Public Class frmPCPhisingGuard
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents txtHP As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents txtEmail As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents photoSrc As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents txtDeskripsiGambar As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ImgPC As System.Web.UI.WebControls.Image

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private cooValue As String
    Private clientCookies As String
    Private CookieName = KTB.DNet.Lib.WebConfig.GetValue("DNETPhisingGuard")

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then
            ImgPC.Visible = False
        End If
        btnBack.Attributes.Add("onclick", "return confirm('Anda yakin akan keluar dari halaman ini ?');")
    End Sub

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        Try
            InsertOrUpdatePCPhisingGuard()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString)
        End Try
        btnBack.Attributes.Add("onclick", "return confirm('Anda yakin akan keluar dari halaman ini ?');")
    End Sub

    Private Sub InsertOrUpdatePCPhisingGuard()
        Dim loop1 As Integer
        Dim arr1() As String
        Dim MyCookieColl As HttpCookieCollection
        Dim MyCookie As HttpCookie
     
        MyCookieColl = Request.Cookies
        arr1 = MyCookieColl.AllKeys
        ' Grab individual cookie objects by cookie name     
        For loop1 = 0 To arr1.GetUpperBound(0)
            MyCookie = MyCookieColl(arr1(loop1))
            If MyCookie.Name = CookieName Then
                clientCookies = MyCookie.Value.ToString
            End If
        Next

        Dim objPCPHisingGuard As PCPhisingGuard = RetrievePCPhisingGuard(clientCookies)
        Dim oPC As New PCPhisingGuard
        If Not objPCPHisingGuard Is Nothing Then
            objPCPHisingGuard.Image = UploadFile()
            objPCPHisingGuard.Description = txtDescription.Text
            UpdatePCPhisingGuard(objPCPHisingGuard)
        Else
            oPC = SetValue()
            InsertPCPhisingGuard(oPC)
        End If
        ImgPC.Visible = True
        ImgPC.ImageUrl = "WebResources\GetPCImage.aspx"
    End Sub

    Public Function SetValue() As PCPhisingGuard
        Dim oPCPhisingGuard As New PCPhisingGuard
        Dim _strCookieValue, _sessionID As String
        Dim randomGenerator As New randomGenerator

        '_strCookieValue = getCookiesValue(_sessionID) with Entryption
        _strCookieValue = randomGenerator.GenarateRandom(8)

        'Set Value
        oPCPhisingGuard.CookiesName = CookieName
        oPCPhisingGuard.CookiesValue = _strCookieValue.ToString
        oPCPhisingGuard.Image = UploadFile()
        oPCPhisingGuard.Description = txtDescription.Text
        oPCPhisingGuard.Status = 0
        oPCPhisingGuard.EncKey = _sessionID

        Return oPCPhisingGuard
    End Function

    Private Function getCookiesValue(ByRef _sessionID As String) As String
        Dim randomGenerator As New randomGenerator
        Dim _strCookieValue As String
        _strCookieValue = randomGenerator.GenarateRandom(8)
        _sessionID = HttpContext.Current.Session.SessionID
        Return KTB.DNet.UI.Helper.DNetEncryption.SymmetricEncrypt(_sessionID, _strCookieValue)
    End Function

    Private Sub UpdatePCPhisingGuard(ByVal oPCPhisingGuard As PCPhisingGuard)
        Dim fPCPhisingGuard As PCPhisingGuardFacade = New PCPhisingGuardFacade(Nothing)
        Dim _nresult As Integer
        Try
            fPCPhisingGuard.Update(oPCPhisingGuard)
            txtDescription.Text = ""
            Response.Cookies(CookieName).Value() = oPCPhisingGuard.CookiesValue
            MessageBox.Show("Data Berhasil Disimpan")
        Catch ex As Exception
            Throw New Exception(SR.SaveFail)
        End Try

    End Sub

    Private Sub InsertPCPhisingGuard(ByVal oPCPhisingGuard As PCPhisingGuard)
        Dim fPCPhisingGuard As PCPhisingGuardFacade = New PCPhisingGuardFacade(Nothing)
        Dim _nresult As Integer
        Try
            Dim valCookieExpires As Double = CType(KTB.DNet.Lib.WebConfig.GetValue("PCPhisingCookiesExpires"), Double)
            _nresult = fPCPhisingGuard.Insert(oPCPhisingGuard)
            If _nresult = -1 Then
                Throw New Exception(SR.SaveFail)
            End If
            txtDescription.Text = ""
            Response.Cookies(CookieName).Value() = oPCPhisingGuard.CookiesValue.ToString
            Response.Cookies(CookieName).Expires = Date.Now.AddDays(valCookieExpires) '--set cookies to expires within days that has been defines in web.config
            MessageBox.Show("Data Berhasil Disimpan")
        Catch ex As Exception
            Throw New Exception(SR.SaveFail)
        End Try
    End Sub


    Private Function UploadFile() As Byte()
        Dim nResult() As Byte

        If photoSrc.Value = "" Then
            Return Nothing
        End If

        If Not (photoSrc.PostedFile Is Nothing) Then
            Try
                If IsValidPhoto(photoSrc.PostedFile) Then
                    Dim inStream As System.IO.Stream = photoSrc.PostedFile.InputStream()
                    Dim ByteRead(TrTrainee.MAX_PHOTO_SIZE) As Byte
                    Dim ReadCount As Integer = New System.IO.BinaryReader(inStream).Read(ByteRead, 0, TrTrainee.MAX_PHOTO_SIZE)
                    If ReadCount = 0 Then
                        Throw New InvalidConstraintException(SR.DataNotFound("Image"))
                    End If
                    ReDim nResult(ReadCount)
                    Array.Copy(ByteRead, nResult, ReadCount)
                Else
                    Throw New Exception("Foto harus file gambar dan maksimum 30 KB")
                End If
            Catch e As Exception
                Throw e
            End Try
        End If

        Return nResult
    End Function

    Private Function IsValidPhoto(ByVal file As HttpPostedFile) As Boolean
        Dim containImage As Boolean = (file.ContentType.ToUpper.IndexOf(TrTrainee.VALID_IMAGE_TYPE) >= 0)
        Dim sizeValid As Boolean = (file.ContentLength <= TrTrainee.MAX_PHOTO_SIZE) And (file.ContentLength > 0)

        Return (containImage AndAlso sizeValid)
    End Function

    Private Function RetrievePCPhisingGuard(ByVal CookiesValue As String) As PCPhisingGuard
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.PCPhisingGuard), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.PCPhisingGuard), "CookiesValue", MatchType.Exact, CookiesValue))
        Dim ArlPCPhisingGuard As ArrayList = New PCPhisingGuardFacade(Nothing).Retrieve(criterias)
        If ArlPCPhisingGuard.Count > 0 Then
            Return CType(ArlPCPhisingGuard(0), PCPhisingGuard)
        Else
            Return Nothing
        End If
    End Function

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("login.aspx")
    End Sub
End Class
