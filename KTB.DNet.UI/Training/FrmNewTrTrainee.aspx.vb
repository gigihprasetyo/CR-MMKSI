Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports System.Collections.Generic
Imports System.Linq

Public Class FrmNewTrTrainee
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtJobPosition As System.Web.UI.WebControls.TextBox
    Protected WithEvents Requiredfieldvalidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtEducationLevel As System.Web.UI.WebControls.TextBox
    Protected WithEvents Requiredfieldvalidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents ddlShirtSize As System.Web.UI.WebControls.DropDownList
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents photoSrc As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents Requiredfieldvalidator6 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents txtName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtEmail As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtKTP As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDealerBranchCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents ICStartWork As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblCity As System.Web.UI.WebControls.Label
    Protected WithEvents lblPopUpDealerBranch As System.Web.UI.WebControls.Label
    Protected WithEvents messageValidationSummary As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents lblSearchJobPos As System.Web.UI.WebControls.Label
    Protected WithEvents icBirthDate As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents ddlGender As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Requiredfieldvalidator7 As System.Web.UI.WebControls.RequiredFieldValidator

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim sHTrainee As SessionHelper = New SessionHelper
    Dim objDealer As Dealer

    Private ReadOnly Property Category As String
        Get
            Try
                Return Request.QueryString("category")
            Catch
                Return String.Empty
            End Try
        End Get
    End Property


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        objDealer = sHTrainee.GetSession("DEALER")
        'If IsNothing(objDealer) Then
        '    'Response.Redirect("../SessionExpired.htm")
        'End If
        lblSearchJobPos.Attributes("onclick") = "ShowJobPosSelection()"
        lblPopUpDealerBranch.Attributes.Add("onClick", "ShowPPDealerBranchSelection();")
        If Not IsPostBack Then
            InitiatePage()
        End If
    End Sub

    Private Sub InitiatePage()
        LoadDealer()
        ClearData()

        ViewState("CurrentSortColumn") = "Name"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        txtJobPosition.Attributes.Add("readonly", "readonly")
        BindEnum()
    End Sub

    Private Sub ClearData()
        Me.txtKTP.Text = String.Empty
        Me.txtEmail.Text = String.Empty
        Me.txtName.Text = String.Empty
        Me.ICStartWork.Value = Today
        Me.txtJobPosition.Text = String.Empty
        Me.txtEducationLevel.Text = String.Empty
        Me.photoSrc.Attributes("value") = String.Empty
        Me.ddlShirtSize.SelectedIndex = 0
    End Sub

    Private Sub LoadDealer()
        lblDealerCode.Text = objDealer.DealerCode & " / " & objDealer.SearchTerm1
        lblDealerName.Text = objDealer.DealerName
        lblCity.Text = objDealer.City.CityName
    End Sub

    Private Sub BindEnum()
        Dim objEnumTrTrainee = New EnumTrTrainee

        Dim ShirtSize As ArrayList = objEnumTrTrainee.RetrieveSize()
        For Each es As EnumShirtData In ShirtSize
            Dim lItem As ListItem = New ListItem(es.NameTitle, es.ValTitle.ToString)
            ddlShirtSize.Items.Add(lItem)
        Next

        CommonFunction.BindFromEnum("SalesmanGender", ddlGender, User, False, "NameStatus", "ValStatus")
        ddlGender.Items.Insert(0, New ListItem("Silakan Pilih", "0"))
        ddlGender.SelectedIndex = 0

        'Dim li As ListItem = New ListItem(String.Empty, String.Empty)
        'ddlShirtSize.Items.Insert(0, li)
    End Sub

    Private Sub InsertTrainee()
        Dim objTrainee As TrTrainee = New TrTrainee
        Dim objTraineeFacade As TrTraineeFacade = New TrTraineeFacade(User)
        Dim objTrSalesmanFacade As TrTraineeSalesmanHeaderFacade = New TrTraineeSalesmanHeaderFacade(User)
        Dim nResult = -1
        Dim imageFile As Byte()

        Try
            imageFile = UploadFile()

            If IsNothing(objDealer) Then
                Throw New DataException(SR.DataNotFound("Dealer"))
            End If

            FillingObjectFromForm(objTrainee)
            objTrainee.Photo = imageFile

            'If IsTraineeExist(objTraineeFacade, objTrainee) Then
            '    Throw New Exception(SR.DataIsExist("Siswa"))
            'End If
            'Pengechekan No KTP
            Dim errMsg As String
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTrainee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(TrTrainee), "NoKTP", MatchType.Exact, objTrainee.NoKTP))

            Dim arrTrtrainee As ArrayList = objTraineeFacade.Retrieve(crit)
            Dim objTr As New TrTrainee
            Dim isUpdate As Boolean = False
            If arrTrtrainee.Count > 0 Then
                objTr = arrTrtrainee(0)
                If objTr.Status = CType(EnumTrTrainee.TrTraineeStatus.Active, String) Or objTr.Status = CType(EnumTrTrainee.TrTraineeStatus.Unapproved, String) Then
                    errMsg = String.Format("KTP dengan no {0} sudah terdaftar di dealer {1}. Harap non aktifkan terlebih dahulu.", objTrainee.NoKTP, _
                             objTrainee.Dealer.DealerCode)
                    MessageBox.Show(errMsg)
                    Return
                ElseIf objTr.Status = CType(EnumTrTrainee.TrTraineeStatus.Deactive, String) Then
                    isUpdate = True
                    objTrainee.ID = objTr.ID
                    nResult = objTraineeFacade.Update(objTrainee)
                End If
            Else
                nResult = objTraineeFacade.Insert(objTrainee)
            End If

            If nResult = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                objTrainee.ID = nResult
                Dim dtTrSlsmn As New TrTraineeSalesmanHeader()
                If isUpdate Then
                    If objTrainee.ListTrTraineeSalesmanHeader.Where(Function(x) x.JobPositionAreaID = 2).Count > 0 Then
                        dtTrSlsmn = objTrainee.ListTrTraineeSalesmanHeader.FirstOrDefault(Function(x) x.JobPositionAreaID = 2)
                        dtTrSlsmn.JobPosition = objTrainee.JobPosition
                        dtTrSlsmn.SalesmanHeader = Nothing
                        dtTrSlsmn.Status = 0
                        objTrSalesmanFacade.Update(dtTrSlsmn)
                    Else
                        isUpdate = False
                    End If
                End If

                If Not isUpdate Then
                    dtTrSlsmn.TrTrainee = objTrainee
                    Select Case Category.ToLower()
                        Case "sales"
                            dtTrSlsmn.JobPositionAreaID = 1
                        Case "ass"
                            dtTrSlsmn.JobPositionAreaID = 2
                        Case "cs"
                            dtTrSlsmn.JobPositionAreaID = 3
                    End Select
                    dtTrSlsmn.JobPosition = objTrainee.JobPosition
                    dtTrSlsmn.SalesmanHeader = Nothing
                    dtTrSlsmn.Status = 0

                    objTrSalesmanFacade.Insert(dtTrSlsmn)
                End If

                MessageBox.Show(SR.SaveSuccess)
                Response.Redirect("FrmNewTrTraineeStatus.aspx?category=" + Me.Category)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Function IsTraineeExist(ByVal objFacade As TrTraineeFacade, ByVal objDomain As TrTrainee) As Boolean
        Return objFacade.ValidateTrainee(objDomain) > 0
    End Function

    Private Sub FillingObjectFromForm(ByVal objTrainee As TrTrainee)
        objTrainee.NoKTP = Me.txtKTP.Text
        objTrainee.Email = Me.txtEmail.Text
        objTrainee.Name = Me.txtName.Text
        If (txtDealerBranchCode.Text.Trim <> String.Empty) Then
            objTrainee.DealerBranch = New DealerBranchFacade(User).Retrieve(txtDealerBranchCode.Text)
        End If
        objTrainee.BirthDate = Me.icBirthDate.Value
        objTrainee.Gender = Me.ddlGender.SelectedValue
        objTrainee.Dealer = objDealer
        objTrainee.StartWorkingDate = ICStartWork.Value
        objTrainee.JobPosition = txtJobPosition.Text
        objTrainee.EducationLevel = txtEducationLevel.Text
        objTrainee.ShirtSize = ddlShirtSize.SelectedItem.Text
        objTrainee.Status = EnumTrTrainee.TrTraineeStatus.Unapproved
    End Sub

    Private Function UploadFile() As Byte()
        Dim nResult() As Byte

        If photoSrc.Value.IsNullorEmpty Then
            Return Nothing
        End If

        If Not (photoSrc.PostedFile Is Nothing) Then
            Try
                If IsValidPhoto(photoSrc.PostedFile) Then
                    Dim inStream As System.IO.Stream = photoSrc.PostedFile.InputStream()
                    Dim ByteRead(TrTrainee.MAX_PHOTO_SIZE) As Byte
                    Dim ReadCount As Integer = New System.IO.BinaryReader(inStream).Read(ByteRead, 0, TrTrainee.MAX_PHOTO_SIZE)
                    If ReadCount = 0 Then
                        Throw New InvalidConstraintException(SR.DataNotFound("Photo"))
                    End If
                    ReDim nResult(ReadCount)
                    Array.Copy(ByteRead, nResult, ReadCount)
                Else
                    Throw New DataException("Foto harus file gambar dan maksimum 20 KB")
                End If
            Catch
                Throw
            End Try
        End If

        Return nResult
    End Function

    Private Function IsValidPhoto(ByVal file As HttpPostedFile) As Boolean
        Dim containImage As Boolean = (file.ContentType.ToUpper.IndexOf(TrTrainee.VALID_IMAGE_TYPE) >= 0)
        Dim sizeValid As Boolean = (file.ContentLength <= TrTrainee.MAX_PHOTO_SIZE)
        Return (containImage AndAlso sizeValid)
    End Function

    Private Function IsFormValid() As Boolean
        If txtName.Text = String.Empty Then
            Return False
        End If

        If txtEmail.Text = String.Empty Then
            Return False
        End If

        If txtKTP.Text = String.Empty Then
            Return False
        End If

        If txtEducationLevel.Text = String.Empty Then
            Return False
        End If

        If txtJobPosition.Text = String.Empty Then
            Return False
        End If

        Dim liShirtSize As ListItem
        liShirtSize = ddlShirtSize.Items.FindByValue(ddlShirtSize.SelectedValue)
        If IsNothing(liShirtSize) Then
            Return False
        End If

        Return True
    End Function

    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not IsFormValid() Then
            MessageBox.Show("Data masih kosong")
            Return
        End If

        If CType(ddlGender.SelectedValue, Integer) < 1 Then
            MessageBox.Show("Pilih Jenis Kelamin")
            Return
        End If

        If (Date.Now.Year - Me.icBirthDate.Value.Year) < 17 Then
            MessageBox.Show("Umur siswa tidak memenuhi syarat/ Tanggal lahir di cek kembali")
            Return
        End If

        If Me.icBirthDate.Value.IsNotValid Then
            MessageBox.Show("Tanggal lahir tolong di cek kembali")
            Return
        End If

        If Not ValidateDealerBranchIfExist(txtDealerBranchCode.Text) Then
            MessageBox.Show(SR.DataNotFound("Kode Cabang Dealer"))
            Return
        End If

        If Not IsUnhack() Then
            MessageBox.Show("< dan > bukan karakter valid")
            Return
        End If

        InsertTrainee()
    End Sub

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        If String.IsNullOrEmpty(Category) Then
            Response.Redirect("FrmTrTrainee1.aspx")
        Else
            Response.Redirect(String.Format("FrmDataStatusSiswa.aspx?category={0}", Category))
        End If

    End Sub

    Private Function ValidateDealerBranchIfExist(ByVal DealerBranchCode As String) As Boolean
        If txtDealerBranchCode.Text.Trim <> String.Empty Then
            Dim dealerBranch As DealerBranch = New DealerBranchFacade(User).Retrieve(DealerBranchCode)
            If IsNothing(dealerBranch) Then
                Return False
            ElseIf (dealerBranch.Dealer.DealerCode <> objDealer.DealerCode) Then
                Return False
            Else
                Return True
            End If
        Else
            Return True
        End If
    End Function

    Private Function IsUnhack() As Boolean
        If txtName.Text.IndexOf("<") >= 0 Or txtName.Text.IndexOf(">") >= 0 Then
            Return False
        End If

        If txtEmail.Text.IndexOf("<") >= 0 Or txtEmail.Text.IndexOf(">") >= 0 Or txtEmail.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtKTP.Text.IndexOf("<") >= 0 Or txtKTP.Text.IndexOf(">") >= 0 Or txtKTP.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtEducationLevel.Text.IndexOf("<") >= 0 Or txtEducationLevel.Text.IndexOf(">") >= 0 Or txtEducationLevel.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        If txtJobPosition.Text.IndexOf("<") >= 0 Or txtJobPosition.Text.IndexOf(">") >= 0 Or txtJobPosition.Text.IndexOf("'") >= 0 Then
            Return False
        End If

        Return True
    End Function
End Class
