#Region "Custom Namespace Import"
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Salesman
Imports KTB.DNet.BusinessFacade.General

Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports Ktb.DNet.Security

#End Region



Public Class FrmSalesmanUniformGuide
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents lblKeterangan As System.Web.UI.WebControls.Label
    Protected WithEvents ddlUnifDistributionCode As System.Web.UI.WebControls.DropDownList
    Protected WithEvents dgSalesmanUnifGuide As System.Web.UI.WebControls.DataGrid
    Protected WithEvents photoView As System.Web.UI.WebControls.Image
    Protected WithEvents photoSrc As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents lblBrowse As System.Web.UI.WebControls.Label
    Protected WithEvents lblPopImage As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "PrivateVariables"
    Private _SalesmanUnifGuideFacade As New SalesmanUnifGuideFacade(User)
    Private _uploadPriv As Boolean = False
    Private sessHelper As New SessionHelper
    Dim arrSalesmanUnifGuide As New ArrayList

#End Region

#Region "PrivateCustomMethods"
    ' penambahan untuk initialize data
    Private Sub ClearData()
        ddlUnifDistributionCode.SelectedIndex = -1
        dgSalesmanUnifGuide.DataSource = Nothing
        dgSalesmanUnifGuide.DataBind()
        lblKeterangan.Text = String.Empty
    End Sub

    Private Sub Initialize()
        Dim objDealer As Dealer = sessHelper.GetSession("DEALER")
        If objDealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
            ' bila dealer maka browse capability disable, dealer hanya bs view saja
            lblBrowse.Visible = False
            photoSrc.Visible = False
            btnUpload.Visible = False
            dgSalesmanUnifGuide.ShowFooter = False
            dgSalesmanUnifGuide.Columns(8).Visible = False
            'ElseIf objDealer.Title = CType(EnumDealerTittle.DealerTittle.KTB, String) Then 'KTB login
            '    btnUpload.Visible = _uploadPriv
        End If
        ClearData()
    End Sub
    Private Sub CheckGridFooter()
        ' yang hanya bisa menambah hanya KTB
        Dim objDealer As Dealer = sessHelper.GetSession("DEALER")
        If objDealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
            dgSalesmanUnifGuide.ShowFooter = False
        Else
            dgSalesmanUnifGuide.ShowFooter = True
        End If
    End Sub
    ' untuk keperluan penyimpanan photo
    Private Sub InsertImage()
        Dim oPhisingGuardImage As SalesmanHeader = New SalesmanHeader
        If photoSrc.PostedFile.FileName = String.Empty Then
            MessageBox.Show("Tidak ada file gambar")
            Return  '-- No photo defined
        End If

        '-- Split into array of strings. The last element is the file name
        Dim sFileNames() As String = photoSrc.PostedFile.FileName.Split("\")
        Dim sFileName As String = sFileNames(sFileNames.Length - 1)

        Try
            Dim imageFile As Byte()
            imageFile = UploadFile()

            oPhisingGuardImage.Image = imageFile
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Function IsValidPhoto(ByVal file As HttpPostedFile) As Boolean
        Dim containImage As Boolean = (file.ContentType.ToUpper.IndexOf(SalesmanUniform.VALID_IMAGE_TYPE) >= 0)
        Dim sizeValid As Boolean = (file.ContentLength <= SalesmanUniform.MAX_PHOTO_SIZE)
        Return (containImage AndAlso sizeValid)
    End Function
    Private Function UploadFile() As Byte()
        Dim nResult() As Byte

        Try
            If IsValidPhoto(photoSrc.PostedFile) Then
                Dim inStream As System.IO.Stream = photoSrc.PostedFile.InputStream()
                Dim ByteRead(SalesmanUniform.MAX_PHOTO_SIZE) As Byte
                Dim ReadCount As Integer = New System.IO.BinaryReader(inStream).Read(ByteRead, 0, SalesmanUniform.MAX_PHOTO_SIZE)
                If ReadCount = 0 Then
                    Throw New InvalidConstraintException(SR.DataNotFound("Photo"))
                End If
                ReDim nResult(ReadCount)
                Array.Copy(ByteRead, nResult, ReadCount)
            Else
                'Throw New DataException("Bukan file gambar atau Ukuran file > " & _
                '                        CType(SalesmanUniform.MAX_PHOTO_SIZE / 1024, String) & "KB")
                MessageBox.Show("Bukan file gambar atau Ukuran file > " & _
                                        CType(SalesmanUniform.MAX_PHOTO_SIZE / 1024, String) & "KB")
            End If
        Catch
            'Throw
        End Try

        Return nResult
    End Function
    Private Sub BindDataGrid(ByVal idxPage As Integer)
        Dim totalRow As Integer = 0
        Dim arrList As New ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanUnifGuide), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        If ddlUnifDistributionCode.SelectedValue <> "-1" Then
            criterias.opAnd(New Criteria(GetType(SalesmanUnifGuide), "SalesmanUniform.SalesmanUnifDistribution.ID", MatchType.Exact, CType(ddlUnifDistributionCode.SelectedValue, Integer)))
        End If

        arrList = _SalesmanUnifGuideFacade.RetrieveByCriteria(criterias, idxPage + 1, dgSalesmanUnifGuide.PageSize, totalRow, _
        CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))

        dgSalesmanUnifGuide.DataSource = arrList
        dgSalesmanUnifGuide.VirtualItemCount = totalRow
        dgSalesmanUnifGuide.DataBind()
    End Sub
    ' Untuk bind data yg bersangkutan - related
    Private Sub BindDropDownLists()
        CommonFunction.BindSalesmanUnifDistributionCode(ddlUnifDistributionCode, Me.User, True)
    End Sub
    Private Sub BindAttributes()
        lblPopImage.Attributes("onclick") = "ShowPPSalesmanUniformImage();"
    End Sub
#End Region

#Region "EventHandlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckPrivilege()
        If Not IsPostBack Then
            Initialize()
            BindDropDownLists()
            BindAttributes()
            ' add security
            If Not CheckUploadPrivilege() Then
                photoSrc.Visible = False
                btnUpload.Enabled = False
                dgSalesmanUnifGuide.ShowFooter = False
                dgSalesmanUnifGuide.Columns(8).Visible = False  ' kolom aksi
            End If
        End If
    End Sub
    Private Sub ddlUnifDistributionCode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlUnifDistributionCode.SelectedIndexChanged
        ' mengambil data SalesmanUniform dr SalesmanUnifDistributionID nya
        Dim strSalesmanUniformId As String
        If ddlUnifDistributionCode.SelectedValue <> "-1" Then
            Dim objSalesmanUnifDistribution As SalesmanUnifDistribution = New SalesmanUnifDistributionFacade(User).Retrieve(CType(ddlUnifDistributionCode.SelectedValue, Integer))
            lblKeterangan.Text = objSalesmanUnifDistribution.Description
            Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanUniform), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanUniform), "SalesmanUnifDistribution.ID", MatchType.Exact, CType(ddlUnifDistributionCode.SelectedValue, Integer)))
            Dim arrSalesmanUniform As ArrayList = New SalesmanUniformFacade(User).Retrieve(criterias)

            If arrSalesmanUniform.Count = 0 Then
                dgSalesmanUnifGuide.DataSource = Nothing
                dgSalesmanUnifGuide.DataBind()
                lblKeterangan.Text = String.Empty
                photoView.Visible = False
                lblPopImage.Visible = False
            Else
                strSalesmanUniformId = ""
                For Each item As SalesmanUniform In arrSalesmanUniform
                    If strSalesmanUniformId = "" Then
                        strSalesmanUniformId = CType(item.ID, String)
                        Exit For
                    End If
                Next

                Dim objSalesmanUniform As SalesmanUniform = New SalesmanUniformFacade(User).Retrieve(CType(strSalesmanUniformId, Integer))
                If Not objSalesmanUniform Is Nothing Then

                    ' Show image
                    photoView.Visible = True
                    photoView.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=" & CType(strSalesmanUniformId, Integer) & "&type=" & "SalesmanUniform"
                    BindDataGrid(0)
                    lblPopImage.Visible = True
                End If
            End If
        Else
            dgSalesmanUnifGuide.DataSource = Nothing
            dgSalesmanUnifGuide.DataBind()
            lblKeterangan.Text = String.Empty
            photoView.Visible = False
            lblPopImage.Visible = False
        End If

    End Sub
    Private Sub dgSalesmanUnifGuide_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgSalesmanUnifGuide.ItemCommand
        ' take data from related component
        Dim facade As SalesmanUnifGuideFacade = New SalesmanUnifGuideFacade(User)

        If e.CommandName = "Delete" Then
            Dim objSalesmanUnifGuide As SalesmanUnifGuide = facade.Retrieve(CInt(e.CommandArgument))
            Dim result As Integer = facade.DeleteFromDB(objSalesmanUnifGuide)
            BindDataGrid(0)
        End If

        If e.CommandName = "Add" Then
            Dim txtAddDescriptionNew As TextBox = CType(e.Item.FindControl("txtAddDescription"), TextBox)
            ' check if already exist on grid or not, if already cann't add
            For Each items As DataGridItem In dgSalesmanUnifGuide.Items
                Dim lblDescriptionNew As Label = CType(items.FindControl("lblDescription"), Label)
                If txtAddDescriptionNew.Text = lblDescriptionNew.Text Then
                    MessageBox.Show("Data uraian tidak boleh duplikat")
                    Return
                End If
            Next

            Dim txtAddSSizeNew As TextBox = CType(e.Item.FindControl("txtAddSSize"), TextBox)
            Dim txtAddMSizeNew As TextBox = CType(e.Item.FindControl("txtAddMSize"), TextBox)
            Dim txtAddLSizeNew As TextBox = CType(e.Item.FindControl("txtAddLSize"), TextBox)
            Dim txtAddXLSizeNew As TextBox = CType(e.Item.FindControl("txtAddXLSize"), TextBox)
            Dim txtAddXXLSizeNew As TextBox = CType(e.Item.FindControl("txtAddXXLSize"), TextBox)

            ' check validation
            If txtAddDescriptionNew.Text = "" Then
                MessageBox.Show("Lengkapi Data Description dahulu !")
                Return
            End If

            If txtAddSSizeNew.Text <> "" Then
                If Not IsNumeric(txtAddSSizeNew.Text) Then
                    MessageBox.Show("S Size harus dalam format numeric !")
                    Return
                End If
            End If

            If txtAddMSizeNew.Text <> "" Then
                If Not IsNumeric(txtAddMSizeNew.Text) Then
                    MessageBox.Show("M Size harus dalam format numeric !")
                    Return
                End If
            End If

            If txtAddLSizeNew.Text <> "" Then
                If Not IsNumeric(txtAddLSizeNew.Text) Then
                    MessageBox.Show("L Size harus dalam format numeric !")
                    Return
                End If
            End If

            If txtAddXLSizeNew.Text <> "" Then
                If Not IsNumeric(txtAddXLSizeNew.Text) Then
                    MessageBox.Show("XL Size harus dalam format numeric !")
                    Return
                End If
            End If

            If txtAddXXLSizeNew.Text <> "" Then
                If Not IsNumeric(txtAddXXLSizeNew.Text) Then
                    MessageBox.Show("XXL Size harus dalam format numeric !")
                    Return
                End If
            End If

            Dim arrSalesmanUniform As ArrayList
            Dim strSalesmanUniformId As String
            Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanUniform), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanUniform), "SalesmanUnifDistribution.ID", MatchType.Exact, CType(ddlUnifDistributionCode.SelectedValue, Integer)))
            arrSalesmanUniform = New SalesmanUniformFacade(User).Retrieve(criterias)
            strSalesmanUniformId = ""
            If arrSalesmanUniform.Count > 0 Then
                For Each item As SalesmanUniform In arrSalesmanUniform
                    If strSalesmanUniformId = "" Then
                        strSalesmanUniformId = CType(item.ID, String)
                        Exit For
                    End If
                Next
            End If
            If strSalesmanUniformId <> "" Then
                Dim objSalesmanUniform As SalesmanUniform = New SalesmanUniformFacade(User).Retrieve(CType(strSalesmanUniformId, Integer))
                Dim objSalesmanUnifGuide As SalesmanUnifGuide = New SalesmanUnifGuide

                objSalesmanUnifGuide.SalesmanUniform = objSalesmanUniform
                objSalesmanUnifGuide.Description = txtAddDescriptionNew.Text
                objSalesmanUnifGuide.SSize = CType(IIf(txtAddSSizeNew.Text = "", 0, txtAddSSizeNew.Text), Integer)
                objSalesmanUnifGuide.MSize = CType(IIf(txtAddMSizeNew.Text = "", 0, txtAddMSizeNew.Text), Integer)
                objSalesmanUnifGuide.LSize = CType(IIf(txtAddLSizeNew.Text = "", 0, txtAddLSizeNew.Text), Integer)
                objSalesmanUnifGuide.XLSize = CType(IIf(txtAddXLSizeNew.Text = "", 0, txtAddXLSizeNew.Text), Integer)
                objSalesmanUnifGuide.XXLSize = CType(IIf(txtAddXXLSizeNew.Text = "", 0, txtAddXXLSizeNew.Text), Integer)

                Dim result As Integer = facade.Insert(objSalesmanUnifGuide)

                If result = -1 Then
                    MessageBox.Show(SR.SaveFail)
                Else
                    MessageBox.Show(SR.SaveSuccess)
                End If
                BindDataGrid(0)
            End If

        End If

        If e.CommandName = "Edit" Then
            dgSalesmanUnifGuide.ShowFooter = False
            dgSalesmanUnifGuide.EditItemIndex = e.Item.ItemIndex
            BindDataGrid(0)
        End If

        If e.CommandName = "Cancel" Then
            Dim objDealer As Dealer = sessHelper.GetSession("DEALER")
            CheckGridFooter()

            dgSalesmanUnifGuide.EditItemIndex = -1
            BindDataGrid(0)
        End If

        ' update data yg sdh ada
        If e.CommandName = "Save" Then
            Dim objSalesmanUnifGuide As SalesmanUnifGuide
            objSalesmanUnifGuide = New SalesmanUnifGuideFacade(User).Retrieve(CInt(e.CommandArgument))

            Dim txtEditDescriptionNew As TextBox = CType(e.Item.FindControl("txtEditDescription"), TextBox)
            Dim txtEditSSizeNew As TextBox = CType(e.Item.FindControl("txtEditSSize"), TextBox)
            Dim txtEditMSizeNew As TextBox = CType(e.Item.FindControl("txtEditMSize"), TextBox)
            Dim txtEditLSizeNew As TextBox = CType(e.Item.FindControl("txtEditLSize"), TextBox)
            Dim txtEditXLSizeNew As TextBox = CType(e.Item.FindControl("txtEditXLSize"), TextBox)
            Dim txtEditXXLSizeNew As TextBox = CType(e.Item.FindControl("txtEditXXLSize"), TextBox)

            ' check validation
            If txtEditDescriptionNew.Text = "" Then
                MessageBox.Show("Lengkapi Data Description dahulu !")
                Return
            End If

            If txtEditSSizeNew.Text <> "" Then
                If Not IsNumeric(txtEditSSizeNew.Text) Then
                    MessageBox.Show("S Size harus dalam format numeric !")
                    Return
                End If
            End If

            If txtEditMSizeNew.Text <> "" Then
                If Not IsNumeric(txtEditMSizeNew.Text) Then
                    MessageBox.Show("M Size harus dalam format numeric !")
                    Return
                End If
            End If

            If txtEditLSizeNew.Text <> "" Then
                If Not IsNumeric(txtEditLSizeNew.Text) Then
                    MessageBox.Show("L Size harus dalam format numeric !")
                    Return
                End If
            End If

            If txtEditXLSizeNew.Text <> "" Then
                If Not IsNumeric(txtEditXLSizeNew.Text) Then
                    MessageBox.Show("XL Size harus dalam format numeric !")
                    Return
                End If
            End If

            If txtEditXXLSizeNew.Text <> "" Then
                If Not IsNumeric(txtEditXXLSizeNew.Text) Then
                    MessageBox.Show("XXL Size harus dalam format numeric !")
                    Return
                End If
            End If

            objSalesmanUnifGuide.Description = txtEditDescriptionNew.Text
            objSalesmanUnifGuide.SSize = CType(IIf(txtEditSSizeNew.Text = "", 0, txtEditSSizeNew.Text), Integer)
            objSalesmanUnifGuide.MSize = CType(IIf(txtEditMSizeNew.Text = "", 0, txtEditMSizeNew.Text), Integer)
            objSalesmanUnifGuide.LSize = CType(IIf(txtEditLSizeNew.Text = "", 0, txtEditLSizeNew.Text), Integer)
            objSalesmanUnifGuide.XLSize = CType(IIf(txtEditXLSizeNew.Text = "", 0, txtEditXLSizeNew.Text), Integer)
            objSalesmanUnifGuide.XXLSize = CType(IIf(txtEditXXLSizeNew.Text = "", 0, txtEditXXLSizeNew.Text), Integer)

            Dim result As Integer = facade.Update(objSalesmanUnifGuide)

            If result = -1 Then
                MessageBox.Show(SR.SaveFail)
            Else
                MessageBox.Show(SR.SaveSuccess)
            End If
            CheckGridFooter()
            dgSalesmanUnifGuide.EditItemIndex = -1
            BindDataGrid(0)
        End If
    End Sub
    Private Sub dgSalesmanUnifGuide_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dgSalesmanUnifGuide.SortCommand
        If CType(ViewState("CurrentSortColumn"), String) = e.SortExpression Then
            Select Case CType(ViewState("CurrentSortDirect"), Sort.SortDirection)

                Case Sort.SortDirection.ASC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.DESC

                Case Sort.SortDirection.DESC
                    ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            End Select
        Else
            ViewState("CurrentSortColumn") = e.SortExpression
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        End If
        dgSalesmanUnifGuide.SelectedIndex = -1
        dgSalesmanUnifGuide.CurrentPageIndex = 0
        BindDataGrid(dgSalesmanUnifGuide.CurrentPageIndex)
    End Sub
    Private Sub dgSalesmanUnifGuide_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgSalesmanUnifGuide.PageIndexChanged
        dgSalesmanUnifGuide.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dgSalesmanUnifGuide.CurrentPageIndex)
    End Sub
    Private Sub dgSalesmanUnifGuide_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgSalesmanUnifGuide.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim objSalesmanUnifGuide As SalesmanUnifGuide = e.Item.DataItem

            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dgSalesmanUnifGuide.CurrentPageIndex * dgSalesmanUnifGuide.PageSize)

            ' untuk bagian item / alternate item
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                ' mengisi value
                Dim lbtnDeleteNew As LinkButton = CType(e.Item.FindControl("lbtnDelete"), LinkButton)
                lbtnDeleteNew.Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
                lbtnDeleteNew.CommandArgument = objSalesmanUnifGuide.ID

                Dim lblDescriptionNew As Label = CType(e.Item.FindControl("lblDescription"), Label)
                lblDescriptionNew.Text = objSalesmanUnifGuide.Description

                Dim lblSSizeNew As Label = CType(e.Item.FindControl("lblSSize"), Label)
                lblSSizeNew.Text = CType(objSalesmanUnifGuide.SSize, String)

                Dim lblMSizeNew As Label = CType(e.Item.FindControl("lblMSize"), Label)
                lblMSizeNew.Text = CType(objSalesmanUnifGuide.MSize, String)

                Dim lblLSizeNew As Label = CType(e.Item.FindControl("lblLSize"), Label)
                lblLSizeNew.Text = CType(objSalesmanUnifGuide.LSize, String)

                Dim lblXLSizeNew As Label = CType(e.Item.FindControl("lblXLSize"), Label)
                lblXLSizeNew.Text = CType(objSalesmanUnifGuide.XLSize, String)

                Dim lblXXLSizeNew As Label = CType(e.Item.FindControl("lblXXLSize"), Label)
                lblXXLSizeNew.Text = CType(objSalesmanUnifGuide.XXLSize, String)

            End If

            ' untuk bagian edit item
            If e.Item.ItemType = ListItemType.EditItem Then
                Dim lbtnSaveNew As LinkButton = CType(e.Item.FindControl("lbtnSave"), LinkButton)
                lbtnSaveNew.CommandArgument = objSalesmanUnifGuide.ID

                Dim txtEditDescriptionNew As TextBox = CType(e.Item.FindControl("txtEditDescription"), TextBox)
                txtEditDescriptionNew.Text = objSalesmanUnifGuide.Description

                Dim txtEditSSizeNew As TextBox = CType(e.Item.FindControl("txtEditSSize"), TextBox)
                txtEditSSizeNew.Text = CType(objSalesmanUnifGuide.SSize, String)

                Dim txtEditMSizeNew As TextBox = CType(e.Item.FindControl("txtEditMSize"), TextBox)
                txtEditMSizeNew.Text = CType(objSalesmanUnifGuide.MSize, String)

                Dim txtEditLSizeNew As TextBox = CType(e.Item.FindControl("txtEditLSize"), TextBox)
                txtEditLSizeNew.Text = CType(objSalesmanUnifGuide.LSize, String)

                Dim txtEditXLSizeNew As TextBox = CType(e.Item.FindControl("txtEditXLSize"), TextBox)
                txtEditXLSizeNew.Text = CType(objSalesmanUnifGuide.XLSize, String)

                Dim txtEditXXLSizeNew As TextBox = CType(e.Item.FindControl("txtEditXXLSize"), TextBox)
                txtEditXXLSizeNew.Text = CType(objSalesmanUnifGuide.XXLSize, String)

                ''---Privilege upload
                'txtEditDescriptionNew.Visible = _uploadPriv
                'txtEditSSizeNew.Visible = _uploadPriv
                'txtEditMSizeNew.Visible = _uploadPriv
                'txtEditLSizeNew.Visible = _uploadPriv
                'txtEditXLSizeNew.Visible = _uploadPriv
                'txtEditXXLSizeNew.Visible = _uploadPriv
                ''---End Privilege upload
            End If

            ' untuk bagian footer
            If e.Item.ItemType = ListItemType.Footer Then
                Dim txtAddDescriptionNew As TextBox = CType(e.Item.FindControl("txtAddDescription"), TextBox)
                txtAddDescriptionNew.Text = ""

                Dim txtAddSSizeNew As TextBox = CType(e.Item.FindControl("txtAddSSize"), TextBox)
                txtAddSSizeNew.Text = ""

                Dim txtAddMSizeNew As TextBox = CType(e.Item.FindControl("txtAddMSize"), TextBox)
                txtAddMSizeNew.Text = ""

                Dim txtAddLSizeNew As TextBox = CType(e.Item.FindControl("txtAddLSize"), TextBox)
                txtAddLSizeNew.Text = ""

                Dim txtAddXLSizeNew As TextBox = CType(e.Item.FindControl("txtAddXLSize"), TextBox)
                txtAddXLSizeNew.Text = ""

                Dim txtAddXXLSizeNew As TextBox = CType(e.Item.FindControl("txtAddXXLSize"), TextBox)
                txtAddXXLSizeNew.Text = ""

                ''---Privilege upload
                'Dim lbtnAdd As LinkButton = CType(e.Item.FindControl("lbtnAdd"), LinkButton)
                'lbtnAdd.Visible = _uploadPriv
                'txtAddDescriptionNew.Visible = _uploadPriv
                'txtAddSSizeNew.Visible = _uploadPriv
                'txtAddMSizeNew.Visible = _uploadPriv
                'txtAddLSizeNew.Visible = _uploadPriv
                'txtAddXLSizeNew.Visible = _uploadPriv
                'txtAddXXLSizeNew.Visible = _uploadPriv
                ''---End Privilege upload
            End If

        End If

    End Sub
    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Dim arrSalesmanUniform As ArrayList
        Dim strSalesmanUniformId As String
        If ddlUnifDistributionCode.SelectedItem.Text <> "" Then
            Dim criterias As New CriteriaComposite(New Criteria(GetType(SalesmanUniform), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(SalesmanUniform), "SalesmanUnifDistribution.ID", MatchType.Exact, CType(ddlUnifDistributionCode.SelectedValue, Integer)))
            arrSalesmanUniform = New SalesmanUniformFacade(User).Retrieve(criterias)
        End If

        If arrSalesmanUniform.Count < 1 Then
            MessageBox.Show("Harus ada data Price Uniform terlebih dahulu")
            Return
        End If

        strSalesmanUniformId = ""
        For Each item As SalesmanUniform In arrSalesmanUniform
            If strSalesmanUniformId = "" Then
                strSalesmanUniformId = CType(item.ID, String)
                Exit For
            End If
        Next

        If strSalesmanUniformId <> "" Then
            Dim objSalesmanUniform As SalesmanUniform
            objSalesmanUniform = New SalesmanUniformFacade(User).Retrieve(CType(strSalesmanUniformId, Integer))
            If Not objSalesmanUniform Is Nothing Then
                Dim nResult As Integer = -1
                ' penambahan image menggunakan method conversi
                If (photoSrc.PostedFile.FileName <> String.Empty) Then
                    Dim imageFile As Byte()
                    imageFile = UploadFile()
                    objSalesmanUniform.Image = imageFile
                End If

                nResult = New SalesmanUniformFacade(User).Update(objSalesmanUniform)
                If nResult = -1 Then
                    MessageBox.Show(SR.UpdateFail)
                Else
                    MessageBox.Show(SR.UpdateSucces)
                    photoView.ImageUrl = "../WebResources/GetImageGlobal.aspx?id=" & CType(strSalesmanUniformId, Integer) & "&type=" & "SalesmanUniform"
                End If
            End If
        End If

    End Sub

#End Region

#Region "Privilege"

    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(context.User, SR.PASView_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Seragam Tenaga Penjual - Panduan Ukuran Seragam")
        End If
    End Sub

    Private Function CheckUploadPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PASUploadPicture_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

#End Region





End Class
