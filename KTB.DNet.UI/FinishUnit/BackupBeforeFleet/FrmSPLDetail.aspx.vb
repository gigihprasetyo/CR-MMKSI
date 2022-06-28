Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.enumMode
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.PK
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Security
Imports System.Web.UI.WebControls


Public Class FrmSPLDetail
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblSPLNumber As System.Web.UI.WebControls.Label
    Protected WithEvents lblColon3 As System.Web.UI.WebControls.Label
    Protected WithEvents txtSPLNumber As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblName As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerName As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblCustName As System.Web.UI.WebControls.Label
    Protected WithEvents Label3 As System.Web.UI.WebControls.Label
    Protected WithEvents txtCustName As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblDesc As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents lblValidFrom As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents Label13 As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents ddlStatus As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents txtValidFrom As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtValidTo As System.Web.UI.WebControls.TextBox
    Protected WithEvents chkFreeInterest As System.Web.UI.WebControls.CheckBox
    Protected WithEvents txtDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents RequiredFieldValidator1 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator3 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator4 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator5 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents RequiredFieldValidator6 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents inFileLocation As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents Label12 As System.Web.UI.WebControls.Label
    Protected WithEvents Label14 As System.Web.UI.WebControls.Label
    Protected WithEvents lblAttachment As System.Web.UI.WebControls.Label
    Protected WithEvents Label17 As System.Web.UI.WebControls.Label
    Protected WithEvents lblDibuatOleh As System.Web.UI.WebControls.Label
    Protected WithEvents lblDibuatPada As System.Web.UI.WebControls.Label
    Protected WithEvents lblDiubahOleh As System.Web.UI.WebControls.Label
    Protected WithEvents lblDiubahPada As System.Web.UI.WebControls.Label
    Protected WithEvents tr1 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents tr2 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents tr3 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents tr4 As System.Web.UI.HtmlControls.HtmlTableRow
    Protected WithEvents lblSearchDealer As System.Web.UI.WebControls.Label
    Protected WithEvents dgProfileGroup As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dgSPDetail As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents lbtnPrevMonth As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lbtnNextMonth As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lblCurrentPeriode As System.Web.UI.WebControls.Label
    Protected WithEvents PopUpDealer As System.Web.UI.WebControls.Image
    Protected WithEvents txtNumOfInstallment As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtMaxTOPDay As System.Web.UI.WebControls.TextBox

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
    Private _SPLFacade As New SPLFacade(User)
    Private _create As Boolean
    Private _edit As Boolean
    Private sessHelper As New SessionHelper
    Private ObjSPL As New SPL
    Private ObjSPLDetail As New SPLDetail
    Private objSplDetailList As ArrayList
    Private Mode As enumMode
    Private IndexList As Integer

#End Region

#Region "PrivateCustomMethods"
    Private Function IsMonthYearValid() As String
        Dim retValue As String = ""
        Try
            Dim dd1 As Date = GetDateFromMonthYear(txtValidFrom.Text.Trim(), 1)
            Dim dd2 As Date = GetDateFromMonthYear(txtValidTo.Text.Trim(), 2)
            If dd1 > dd2 Then
                retValue = "Valid sampai tidak boleh lebih besar dari valid dari"
            Else
                retValue = ""
            End If

        Catch ex As Exception
            retValue = "Format bulan dan tahun di 'Valid Dari/Valid Sampai' salah"
        End Try
        Return retValue
    End Function
    Private Function IsExistSPLNumber(ByVal SPLNumber As String) As Boolean
        Dim isExist As Boolean = True
        Dim criterias As New CriteriaComposite(New Criteria(GetType(SPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(SPL), "SPLNumber", MatchType.Exact, SPLNumber))

        Dim arrList As ArrayList = _SPLFacade.Retrieve(criterias)
        If arrList.Count > 0 Then
            isExist = True
        Else
            isExist = False
        End If

        Return isExist
    End Function
    Private Function CheckExt(ByVal ext As String) As Boolean
        Dim retValue As Boolean = False
        If ext.ToUpper() = "PDF" Or ext.ToUpper() = "XLS" Or ext.ToUpper() = "DOC" Or ext.ToUpper() = "ZIP" Or ext.ToUpper() = "RAR" Then
            retValue = True
        Else
            retValue = False
        End If
        Return retValue
    End Function
    Private Function UploadFile(ByRef ObjSPL As SPL) As Integer
        Dim retValue As Integer = 0
        If inFileLocation.PostedFile.FileName.Length > 0 Then
            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")

            Dim sapImp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
            sapImp.Start()
            Try
                If inFileLocation.PostedFile.ContentLength <> inFileLocation.PostedFile.InputStream.Length Then
                    'MessageBox.Show(SR.InvalidData(inFileLocation.PostedFile.FileName))
                    retValue = 0
                    Throw New Exception("File Tidak Sama")
                End If

                Dim directory As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("SPLAttachment")
                Dim directoryInfo As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(directory)
                If Not directoryInfo.Exists Then
                    directoryInfo.Create()
                End If

                Dim ext As String = System.IO.Path.GetExtension(inFileLocation.PostedFile.FileName)
                If Not CheckExt(ext.Substring(1)) Then
                    retValue = 0
                    Throw New Exception("Salah Extention")
                End If

                Dim filename As String = txtSPLNumber.Text.Replace("/", "_") & ext
                Dim targetFile As String = New System.Text.StringBuilder(directory). _
                    Append("\").Append(filename).ToString

                Dim fInfo As System.IO.FileInfo = New System.IO.FileInfo(targetFile)
                If fInfo.Exists Then
                    fInfo.Delete()
                End If

                inFileLocation.PostedFile.SaveAs(targetFile)
                Dim trgInfo As System.IO.FileInfo = New System.IO.FileInfo(targetFile)
                If Not trgInfo.Exists Then
                    retValue = 0
                End If
                Dim strFileSave As String = KTB.DNet.Lib.WebConfig.GetValue("SPLAttachment") & "\" & filename
                ObjSPL.Attachment = strFileSave
                retValue = 1
            Catch ex As Exception
                retValue = 0
            Finally
                sapImp.StopImpersonate()
            End Try
            Return retValue
        Else
            retValue = 1
            Return retValue
        End If
    End Function
    Private Function GetDateFromMonthYear(ByVal MonthYear As String, ByVal type As Integer) As Date
        If MonthYear.Length = 5 Then
            MonthYear = "0" + MonthYear
        End If
        Dim month As Integer = CInt(MonthYear.Substring(0, 2))
        Dim year As Integer = CInt(MonthYear.Substring(2, 4))
        Dim retDate As DateTime
        If type = 1 Then
            retDate = New Date(year, month, 1)
        Else
            retDate = New Date(year, month, DateTime.DaysInMonth(year, month))
        End If
        Return retDate
    End Function
    Private Sub GetEnableControl(ByVal isEnabled As Boolean)
        If Convert.ToString(sessHelper.GetSession("Status")) = "Update" Then
            txtSPLNumber.Attributes.Add("readonly", "readonly")
            'txtSPLNumber.ReadOnly = True
        Else
            ' txtSPLNumber.ReadOnly = Not isEnabled
            txtSPLNumber.Attributes.Remove("readonly")
        End If
        If isEnabled Then
            txtDealerName.Attributes.Remove("readonly")
            txtCustName.Attributes.Remove("readonly")
            txtDescription.Attributes.Remove("readonly")
            txtValidFrom.Attributes.Remove("readonly")
            txtValidTo.Attributes.Remove("readonly")
        Else
            txtDealerName.Attributes.Add("readonly", "readonly")
            txtCustName.Attributes.Add("readonly", "readonly")
            txtDescription.Attributes.Add("readonly", "readonly")
            txtValidFrom.Attributes.Add("readonly", "readonly")
            txtValidTo.Attributes.Add("readonly", "readonly")
        End If
        'txtDealerName.ReadOnly = Not isEnabled
        'txtCustName.ReadOnly = Not isEnabled
        'txtDescription.ReadOnly = Not isEnabled
        'txtValidFrom.ReadOnly = Not isEnabled
        'txtValidTo.ReadOnly = Not isEnabled
        ddlStatus.Enabled = isEnabled
        btnSave.Enabled = isEnabled

    End Sub
    Private Sub GetValueToDatabase(ByRef ObjSPL As SPL)
        ObjSPL.SPLNumber = txtSPLNumber.Text
        ObjSPL.DealerName = txtDealerName.Text
        ObjSPL.CustomerName = txtCustName.Text
        ObjSPL.Description = txtDescription.Text
        ObjSPL.ValidFrom = GetDateFromMonthYear(txtValidFrom.Text, 1)
        ObjSPL.ValidTo = GetDateFromMonthYear(txtValidTo.Text, 2)
        ObjSPL.Status = CType(EnumStatusSPL.StatusSPL.Aktif, Short)
        ObjSPL.NumOfInstallment = CType(Me.txtNumOfInstallment.Text, Integer)
        ObjSPL.MaxTOPDay = CType(Me.txtMaxTOPDay.Text, Integer)

    End Sub
    Private Sub GetValueFromDataBase(ByVal idSPL As Integer)
        Dim ObjSPL As SPL = _SPLFacade.Retrieve(idSPL)
        txtSPLNumber.Text = ObjSPL.SPLNumber
        Dim _splDealer As ArrayList = _SPLFacade.RetrieveDealerID(idSPL)
        sessHelper.SetSession("OLDSPLDealer", _splDealer)
        Dim _tempDealer As String = ""
        If Not _splDealer Is Nothing Then
            For Each item As SPLDealer In _splDealer
                Dim objDealer As Dealer = New DealerFacade(User).Retrieve(item.Dealer.ID)
                _tempDealer += objDealer.DealerCode + ";"
            Next
            'txtDealerName.Text = _tempDealer.Remove(_tempDealer.LastIndexOf(";"), 1)
            txtDealerName.Text = _tempDealer
        End If
        txtCustName.Text = ObjSPL.CustomerName
        txtDescription.Text = ObjSPL.Description
        txtValidFrom.Text = ReturnMonth2Digit(Convert.ToString(ObjSPL.ValidFrom.Month)) & Convert.ToString(ObjSPL.ValidFrom.Year)
        txtValidTo.Text = ReturnMonth2Digit(Convert.ToString(ObjSPL.ValidTo.Month)) & Convert.ToString(ObjSPL.ValidTo.Year)

        ddlStatus.SelectedValue = ObjSPL.Status
        lblAttachment.Text = ObjSPL.Attachment
        lblDibuatOleh.Text = UserInfo.Convert(ObjSPL.CreatedBy)
        lblDibuatPada.Text = ObjSPL.CreatedTime
        lblDiubahOleh.Text = UserInfo.Convert(ObjSPL.LastUpdateBy)
        lblDiubahPada.Text = ObjSPL.LastUpdateTime

        Me.txtNumOfInstallment.Text = ObjSPL.NumOfInstallment.ToString()
        Me.txtMaxTOPDay.Text = ObjSPL.MaxTOPDay.ToString()

    End Sub
    Private Function ReturnMonth2Digit(ByVal mm As String) As String
        If mm.Length < 2 Then
            mm = "0" & mm
        End If
        Return mm
    End Function
    Private Sub BindDdlStatus()
        ddlStatus.Items.Clear()
        ddlStatus.DataSource = EnumStatusSPL.RetrieveStatus
        ddlStatus.DataValueField = "ValStatus"
        ddlStatus.DataTextField = "NameStatus"
        ddlStatus.DataBind()
    End Sub
    Private Sub CheckPrivilege()
        If Not SecurityProvider.Authorize(Context.User, SR.ENHPKDaftarAplikasiLihat_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=UMUM - Detail SPL")
        End If

        _create = SecurityProvider.Authorize(Context.User, SR.ENHPKDaftarAplikasiBuat_Privilege)
        _edit = SecurityProvider.Authorize(Context.User, SR.ENHPKDaftarAplikasiUbah_Privilege)

        btnSave.Visible = _create And _edit
    End Sub
    Private Sub RemoveALLSession()
        sessHelper.RemoveSession("OLDSPLDealer")
        sessHelper.RemoveSession("OLDSPLDETAILLIST")
        sessHelper.RemoveSession("SPLDETAILLIST")
        sessHelper.RemoveSession("STATUSMONTH")
    End Sub
    Private Sub Initialize()
        BindDdlStatus()
        lblSearchDealer.Attributes("onclick") = "ShowPPDealerSelection();"
        btnBack.Visible = True
        'If Convert.ToString(sessHelper.GetSession("Status")) = "Insert" Then
        '    txtSPLNumber.Text = ""
        '    txtDealerName.Text = ""
        '    txtCustName.Text = ""
        '    txtDescription.Text = ""
        '    txtValidFrom.Text = ""
        '    txtValidTo.Text = ""
        '    lblAttachment.Text = ""
        '    GetEnableControl(True)
        '    tr1.Visible = False
        '    ' tr2.Visible = False
        '    tr3.Visible = False
        '    'tr4.Visible = False
        '    ddlStatus.SelectedValue = 1
        '    ddlStatus.Enabled = False
        '    lbtnNextMonth.Visible = False
        '    lbtnPrevMonth.Visible = False
        '    dgSPDetail.ShowFooter = False
        '    btnBack.Visible = False
        'End If

        If Convert.ToString(sessHelper.GetSession("Status")) = "Update" Then
            Dim idSPL As Integer = CInt(sessHelper.GetSession("IDSPLHeader"))
            GetValueFromDataBase(idSPL)
            ddlStatus.Enabled = True
            GetEnableControl(True)
            BindDetail(idSPL)
            If ddlStatus.SelectedValue = 1 Then
                dgSPDetail.ShowFooter = False
                lbtnNextMonth.Visible = False
                lbtnPrevMonth.Visible = False
            Else
                dgSPDetail.ShowFooter = True
                lbtnNextMonth.Visible = True
                lbtnPrevMonth.Visible = True
            End If
        ElseIf Convert.ToString(sessHelper.GetSession("Status")) = "View" Then
            Dim idSPL As Integer = CInt(sessHelper.GetSession("IDSPLHeader"))
            GetValueFromDataBase(idSPL)
            BindDetail(idSPL)
            GetEnableControl(False)
            dgSPDetail.ShowFooter = False
            inFileLocation.Disabled = True
        Else
            txtSPLNumber.Text = ""
            txtDealerName.Text = ""
            txtCustName.Text = ""
            txtDescription.Text = ""
            txtValidFrom.Text = ""
            txtValidTo.Text = ""
            lblAttachment.Text = ""
            GetEnableControl(True)
            tr1.Visible = False
            ' tr2.Visible = False
            tr3.Visible = False
            'tr4.Visible = False
            ddlStatus.SelectedValue = 1
            ddlStatus.Enabled = False
            lbtnNextMonth.Visible = False
            lbtnPrevMonth.Visible = False
            dgSPDetail.ShowFooter = False
            btnBack.Visible = False
            sessHelper.SetSession("Status", "Insert")
        End If
    End Sub

    Private Function GetTOPInstallment(ByVal DealerID As Integer, ByVal SPLD As SPLDetail, ByVal NumInstallMent As Integer) As Integer
        Dim Val As Integer = 0

        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Price), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Price), "VechileColor.VechileType.ID", MatchType.Exact, SPLD.VechileType.ID))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Price), "Dealer.ID", MatchType.Exact, DealerID))
        Dim DTPrice As DateTime = New DateTime(SPLD.PeriodYear, SPLD.PeriodMonth, 1)
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.Price), "ValidFrom", MatchType.LesserOrEqual, DTPrice))
        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(Price), "ValidFrom", Sort.SortDirection.DESC))
        Dim objPriceArrayList As ArrayList = New ArrayList
        objPriceArrayList = New PriceFacade(User).Retrieve(criterias, sortColl)

        If Not IsNothing(objPriceArrayList) AndAlso objPriceArrayList.Count > 0 Then
            Dim ObjPrice As Price = CType(objPriceArrayList(0), Price)
            Dim MainInstallment As Decimal = ObjPrice.BasePrice
            MainInstallment = Me.RoundUp(MainInstallment / CDec(NumInstallMent), -3)
            Dim Dt As DateTime = DateTime.Now.Date
            Dim vBalance As Decimal = ObjPrice.BasePrice
            Dim TINterest As Decimal = 0

            'Jika tak punya interest harfcode to 1.5
            ' requested bby jeng ike 20161114
            'confirmed by sugeng via phone
            If ObjPrice.Interest = 0 Then
                ObjPrice.Interest = 1.5
            End If
            Dim Tdate As Integer = 0
            Dim TDay As Date = Dt

            For idM As Integer = 1 To NumInstallMent
                Dim VINterest As Decimal = 0
                VINterest = vBalance * ObjPrice.Interest * 1.0 / 100.0
                vBalance = vBalance - MainInstallment
                If idM > 1 Then
                    Dim DifDate As TimeSpan
                    Dim IDif As Integer = 0
                    TINterest = TINterest + VINterest
                    TDay = TDay.AddMonths(1)

                    DifDate = TDay.Subtract(Dt)
                    IDif = DifDate.Days
                    Tdate = Tdate + IDif

                    Dt = Dt.AddMonths(1)
                End If
            Next

            Dim DayT As Integer = Math.Round(((365 * TINterest) / (ObjPrice.BasePrice * ObjPrice.Interest * (12.0 / 100.0))), 0)
            Val = DayT
        Else
            Throw New Exception("No Data Price")
        End If
        Return Val
    End Function

    Private Function RoundDown(ByVal number As Double, ByVal decimalPlaces As Integer) As Double

        Return Math.Floor(number * Math.Pow(10, decimalPlaces)) / Math.Pow(10, decimalPlaces)
    End Function

    Private Function RoundUp(ByVal number As Double, ByVal decimalPlaces As Integer) As Double

        Return Math.Ceiling(number * Math.Pow(10, decimalPlaces)) / Math.Pow(10, decimalPlaces)
    End Function
#End Region

#Region "EventHandlers"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckPrivilege()
        If Not IsPostBack Then
            txtMaxTOPDay.Attributes.Add("readonly", "readonly")
            Initialize()
        End If
    End Sub
    Private Sub BindDetail(ByVal IDSPL As Integer)
        Dim objSPL As SPL = New SPLFacade(User).Retrieve(IDSPL)
        Dim list As ArrayList = CType(sessHelper.GetSession("SPLDETAILLIST"), ArrayList)
        If list Is Nothing Then
            If Convert.ToString(sessHelper.GetSession("Status")) = "Update" Or Convert.ToString(sessHelper.GetSession("Status")) = "View" Then
                list = _SPLFacade.RetrieveSPLDetail(IDSPL)
                sessHelper.SetSession("OLDSPLDETAILLIST", _SPLFacade.RetrieveSPLDetail(IDSPL))
                If list Is Nothing Then
                    list = New ArrayList
                End If
            ElseIf Convert.ToString(sessHelper.GetSession("Status")) = "Insert" Then
                list = New ArrayList
            End If
        End If

        'Tampilkan berdasarkan periodmonth

        Dim _objSPL As New SPL
        Dim _listTmp As New ArrayList

        For Each item As SPLDetail In list
            _objSPL.SPLDetails.Add(item)
        Next

        If sessHelper.GetSession("STATUSMONTH") Is Nothing Then
            sessHelper.SetSession("STATUSMONTH", objSPL.ValidFrom)
            sessHelper.SetSession("STATUSMAXMONTH", objSPL.ValidTo)
            sessHelper.SetSession("STATUSMINMONTH", objSPL.ValidFrom)
            lblCurrentPeriode.Text = CType(objSPL.ValidFrom.Month - 1, enumMonth.Month).ToString.Replace("_", " ") + " " + objSPL.ValidFrom.Year.ToString()
        Else
            lblCurrentPeriode.Text = CType(CType(sessHelper.GetSession("STATUSMONTH"), Date).Month - 1, enumMonth.Month).ToString.Replace("_", " ") + " " + CType(sessHelper.GetSession("STATUSMONTH"), Date).Year.ToString()
        End If


        For Each _itemTmp As SPLDetail In _objSPL.SPLDetails
            If _itemTmp.PeriodMonth = CType(sessHelper.GetSession("STATUSMONTH"), Date).Month And _itemTmp.PeriodYear = CType(sessHelper.GetSession("STATUSMONTH"), Date).Year Then
                _listTmp.Add(_itemTmp)
            End If
        Next

        sessHelper.SetSession("SPLDETAILLIST", list)
        sessHelper.SetSession("SPLVIEW", _listTmp)
        dgSPDetail.DataSource = _listTmp
        dgSPDetail.DataBind()
    End Sub

    Private Function IsValidInstallment() As Boolean
        Dim nNum As Integer
        Dim IsValid As Boolean

        IsValid = False
        Try
            nNum = CType(Me.txtNumOfInstallment.Text, Integer)
            If nNum >= 1 Then
                IsValid = True
            End If
        Catch ex As Exception

        End Try
        If IsValid = False Then
            MessageBox.Show("Jumlah Installment Tidak Valid!")
            Return IsValid
        End If

        IsValid = False
        Try
            nNum = CType(Me.txtMaxTOPDay.Text, Integer)
            If nNum >= 1 Then
                IsValid = True
            End If
        Catch ex As Exception

        End Try
        If IsValid = False Then
            MessageBox.Show("Jumlah Hari TOP  Tidak Valid!")
        End If




    End Function

    Private Function UpdatePKTOPDay(ByVal SPLNumber As String, ByVal NumOfInstalment As Integer, ByVal MaxTOPDay As Integer) As Boolean
        Dim cPK As CriteriaComposite
        Dim oPKFac As New PKHeaderFacade(User)
        Dim aPKs As ArrayList
        Dim aPKChecks As ArrayList
        Dim Sql As String = ""
        Dim sPKIDs As String
        Dim oSPLinDB As SPL
        Dim oSPLFac As New SPLFacade(User)

        oSPLinDB = oSPLFac.Retrieve(SPLNumber)
        If Not IsNothing(oSPLinDB) AndAlso oSPLinDB.ID > 0 Then 'UPDATE
            If oSPLinDB.NumOfInstallment <> NumOfInstalment Then

                cPK = New CriteriaComposite(New Criteria(GetType(PKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                cPK.opAnd(New Criteria(GetType(PKHeader), "SPLNumber", MatchType.Exact, SPLNumber))
                aPKs = oPKFac.Retrieve(cPK)
                If aPKs.Count > 0 Then
                    sPKIDs = ""
                    For i As Integer = 0 To aPKs.Count - 1
                        sPKIDs &= CType(aPKs(i), PKHeader).ID.ToString()
                        If i <> aPKs.Count - 1 Then
                            sPKIDs &= ","
                        End If
                    Next

                    Sql = " select count(1) n " & _
                        "   from POHeader poh with (nolock) " & _
                        "       join ContractHeader ch with (nolock) on ch.ID=poh.ContractHeaderID " & _
                        "       join PKHeader pkh with (nolock) on pkh.PKNumber=ch.PKNumber " & _
                        "   where 1=1 " & _
                        "       and poh.RowStatus=0 and ch.RowStatus=0 and pkh.RowStatus=0 " & _
                        "       and poh.Status in (0,2,4,6,8) " & _
                        "       and pkh.ID in (" & sPKIDs & ")"
                    Sql = "(" & Sql & ")"
                    cPK = New CriteriaComposite(New Criteria(GetType(PKHeader), "RowStatus", MatchType.Exact, Sql))
                    cPK.opAnd(New Criteria(GetType(PKHeader), "ID", MatchType.Exact, CType(aPKs(0), PKHeader).ID))
                    aPKChecks = oPKFac.Retrieve(cPK)
                    If aPKChecks.Count = 0 Then
                        MessageBox.Show("Jumlah Cicilan Tidak Bisa Dirubah. Sudah Ada PO Untuk Nomor SPL " & SPLNumber)
                        Return False
                    Else
                        'Update PK
                        For Each oPKH As PKHeader In aPKs
                            oPKH.MaxTopDay = MaxTOPDay
                            oPKH.MaxTopIndicator = 1
                            oPKH.ID = oPKFac.Update(oPKH)
                        Next
                    End If

                End If

            End If


            Return True
        Else
            MessageBox.Show("Data SPL " & SPLNumber & " Tidak Ada!")
            Return False
        End If



    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim sValid As String = IsMonthYearValid()
        Dim list As New ArrayList
        Dim i As Integer
        If sValid.Trim().Length > 0 Then
            MessageBox.Show(sValid)
            Return
        End If
        If ValidateDealers(txtDealerName.Text.Trim) Then
            If Convert.ToString(sessHelper.GetSession("Status")) = "Insert" Then
                If IsExistSPLNumber(txtSPLNumber.Text.Trim()) Then
                    MessageBox.Show(SR.DataIsExist("SPLNumber"))
                Else
                    If Not sessHelper.GetSession("SPLDETAILLIST") Is Nothing Then
                        list = CType(sessHelper.GetSession("SPLDETAILLIST"), ArrayList)
                    End If
                    Dim _splDealers As New ArrayList
                    GetValueToDatabase(ObjSPL)
                    i = 0
                    For Each item As String In txtDealerName.Text.Split(";")
                        If i < txtDealerName.Text.Split(";").Length - 1 Or i = 0 Then
                            Dim objDealerTmp As Dealer = New DealerFacade(User).Retrieve(item)
                            Dim objSPLDealer As SPLDealer = New SPLDealer
                            objSPLDealer.Dealer = objDealerTmp
                            _splDealers.Add(objSPLDealer)
                        End If
                        i = i + 1
                    Next

                    If UploadFile(ObjSPL) = 1 Then
                        Dim n As Integer = _SPLFacade.InsertSPL(ObjSPL, list, _splDealers)
                        If n = -1 Then
                            MessageBox.Show(SR.SaveFail)
                        Else
                            RemoveALLSession()
                            sessHelper.SetSession("Status", "Update")
                            sessHelper.SetSession("IDSPLHeader", ObjSPL.ID)
                            Response.Redirect("FrmSPLDetail.aspx")
                            MessageBox.Show("Simpan Berhasil")
                        End If
                    Else
                        MessageBox.Show("File gagal disimpan di Server. Harap hubungi Administrator")
                    End If
                End If
            ElseIf Convert.ToString(sessHelper.GetSession("Status")) = "Update" Then
                Dim idSPL As Integer = CInt(sessHelper.GetSession("IDSPLHeader"))
                Dim ObjSPL As SPL = _SPLFacade.Retrieve(idSPL)
                If ddlStatus.SelectedValue = EnumStatusSPL.StatusSPL.Tidak_Aktif Then
                    Dim criterias As New CriteriaComposite(New Criteria(GetType(PKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(PKHeader), "SPLNumber", MatchType.Exact, ObjSPL.SPLNumber))
                    Dim x As Integer = New PKHeaderFacade(User).Retrieve(criterias).Count
                    If x > 0 Then
                        MessageBox.Show("Aplikasi sudah di gunakan di Pesanan Kendaraan")
                        Return
                    End If
                End If

                Dim _oldSPLDetail As ArrayList = CType(sessHelper.GetSession("OLDSPLDETAILLIST"), ArrayList)
                Dim _oldSPLDealer As ArrayList = CType(sessHelper.GetSession("OLDSPLDealer"), ArrayList)

                ''remark by ali
                'If Not _oldSPLDetail Is Nothing Then
                '    Dim _delDetail As Integer = _SPLFacade.DeleteOLDSPLDetail(_oldSPLDetail)
                'End If

                'If Not _oldSPLDealer Is Nothing Then
                '    Dim _delDealer As Integer = _SPLFacade.DeleteOLDDealer(_oldSPLDealer)
                'End If
                ''remark by ali



                Dim _splDetail As ArrayList = CType(sessHelper.GetSession("SPLDETAILLIST"), ArrayList)
                Dim _splDealers As New ArrayList
                i = 0
                If CInt(txtNumOfInstallment.Text) > 1 AndAlso _splDetail.Count = 0 Then
                    MessageBox.Show("Minimum Installment  1 jenis kendaraan")
                    Return


                End If

                If CInt(txtNumOfInstallment.Text) = 1 Then
                    txtMaxTOPDay.Text = 0


                End If
                Dim TempDealer As Dealer
                For Each item As String In txtDealerName.Text.Split(";")
                    If i < txtDealerName.Text.Split(";").Length - 1 Then
                        Dim objDealerTmp As Dealer = New DealerFacade(User).Retrieve(item)
                        Dim objSPLDealer As SPLDealer = New SPLDealer
                        objSPLDealer.Dealer = objDealerTmp
                        _splDealers.Add(objSPLDealer)
                        If IsNothing(TempDealer) OrElse TempDealer.ID <= 0 Then
                            TempDealer = objDealerTmp
                        End If

                    End If
                    i = i + 1
                Next
                Try
                    If CInt(txtNumOfInstallment.Text) > 1 Then
                        txtMaxTOPDay.Text = GetTOPInstallment(TempDealer.ID, _splDetail(0), CInt(txtNumOfInstallment.Text))
                    End If
                Catch ex As Exception
                    MessageBox.Show("Harga Tidak memiliki Interest")
                    Return
                End Try
             



                GetValueToDatabase(ObjSPL)
                If ddlStatus.SelectedValue = EnumStatusSPL.StatusSPL.Tidak_Aktif Then
                    ObjSPL.Status = EnumStatusSPL.StatusSPL.Tidak_Aktif
                End If

                If UploadFile(ObjSPL) = 1 Then
                    'Dim n As Integer = _SPLFacade.UpdateSPL(ObjSPL, _splDetail, _splDealers)
                    Dim n As Integer = _SPLFacade.UpdateSPL(ObjSPL, _splDetail, _splDealers, _oldSPLDetail, _oldSPLDealer)

                    If n = -1 Then
                        MessageBox.Show(SR.UpdateFail)
                    Else
                        RemoveALLSession()
                        sessHelper.SetSession("Status", "Update")
                        sessHelper.SetSession("STATUSMONTH", ObjSPL.ValidFrom)
                        sessHelper.SetSession("IDSPLHeader", ObjSPL.ID)
                        Response.Redirect("FrmSPLDetail.aspx")
                        MessageBox.Show("Simpan Berhasil")
                    End If
                Else
                    MessageBox.Show("File gagal disimpan di Server. Harap hubungi Administrator")
                End If

            End If
        End If
    End Sub
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        RemoveALLSession()
        Response.Redirect("FrmSPLHeader.aspx")
    End Sub
    Private Sub SetDgSPLDetailItemFooter(ByVal e As DataGridItemEventArgs)

        Dim lblFooterKodeModel As Label = CType(e.Item.FindControl("lblFooterKodeModel"), Label)
        lblFooterKodeModel.Attributes("onclick") = "ShowPPKodeModelSelection();"

        Dim lblFooterTop As Label = CType(e.Item.FindControl("lblFooterTop"), Label)
        lblFooterTop.Attributes("onclick") = "ShowPPFasilitasTOPSelection();"

        Dim cboFooterCreditCeiling As DropDownList = CType(e.Item.FindControl("cboFooterCreditCeiling"), DropDownList)
        Dim li As ListItem

        For Each item As SPLCreditCeiling In SPLEnum.RetrieveEnumCreditCeiling
            li = New ListItem(item.Description, item.Code)
            cboFooterCreditCeiling.Items.Add(li)
        Next

        Dim cboFooterInteres As DropDownList = CType(e.Item.FindControl("cboFooterInterest"), DropDownList)
        For Each items As SPLInterest In SPLEnum.RetrieveEnumInterest
            li = New ListItem(items.Desc, items.Code)
            cboFooterInteres.Items.Add(li)
        Next
        Dim txtFooterKodeModel As TextBox = CType(e.Item.FindControl("txtFooterKodeModel"), TextBox)
        'txtFooterKodeModel.Attributes.Add("readonly", "readonly")
        Dim txtFooterTop As TextBox = CType(e.Item.FindControl("txtFooterTop"), TextBox)
        'txtFooterTop.Attributes.Add("readonly", "readonly")
    End Sub
    Private Sub SetDgSPLDetailItemEdit(ByVal e As DataGridItemEventArgs, ByVal list As ArrayList)

        'Dim list As ArrayList = CType(sessHelper.GetSession("SPLDETAILLIST"), ArrayList)
        'IndexList = (list.Count - 1) - (listis.Count - 1) + e.Item.ItemIndex
        If list Is Nothing Then
            list = New ArrayList
        End If
        Dim _objSPLDetail As SPLDetail = list(e.Item.ItemIndex)


        Dim txtEditKodeModel As TextBox = CType(e.Item.FindControl("txtEditKodeModel"), TextBox)
        Dim txtEditTop As TextBox = CType(e.Item.FindControl("txtEditTOP"), TextBox)

        txtEditTop.Text = _objSPLDetail.MaxTopIndicator.ToString() + "," + _objSPLDetail.MaxTopDate.ToString() + "," + _objSPLDetail.MaxTopDay.ToString()
        'txtEditTop.ReadOnly = True
        txtEditTop.Attributes.Add("readonly", "readonly")

        'txtEditKodeModel.ReadOnly = True
        txtEditKodeModel.Attributes.Add("readonly", "readonly")

        Dim li As ListItem
        Dim cboEditInterest As DropDownList = CType(e.Item.FindControl("cboEditInteres"), DropDownList)
        For Each items As SPLInterest In SPLEnum.RetrieveEnumInterest
            li = New ListItem(items.Desc, items.Code)
            cboEditInterest.Items.Add(li)
        Next
        cboEditInterest.SelectedValue = _objSPLDetail.FreeIntIndicator

        Dim cboEditCreditCeiling As DropDownList = CType(e.Item.FindControl("cboEditCreditCeiling"), DropDownList)
        For Each item As SPLCreditCeiling In SPLEnum.RetrieveEnumCreditCeiling
            li = New ListItem(item.Description, item.Code)
            cboEditCreditCeiling.Items.Add(li)
        Next
        cboEditCreditCeiling.SelectedValue = _objSPLDetail.CreditCeiling

        Dim txtEditPriceRef As TextBox = CType(e.Item.FindControl("txtEditPriceRef"), TextBox)
        txtEditPriceRef.Text = ReturnMonth2Digit(Convert.ToString(_objSPLDetail.PriceRefDate.Month)) & Convert.ToString(_objSPLDetail.PriceRefDate.Year)

        Dim txtEditDeliveryDate As TextBox = CType(e.Item.FindControl("txtEditDeliveryDate"), TextBox)
        txtEditDeliveryDate.Text = ReturnMonth2Digit(Convert.ToString(_objSPLDetail.DeliveryDate.Month)) & Convert.ToString(_objSPLDetail.DeliveryDate.Year)

    End Sub
    Sub dgSPDetail_ItemCommand(ByVal sender As System.Object, ByVal e As DataGridCommandEventArgs) Handles dgSPDetail.ItemCommand
        Select Case (e.CommandName)
            Case "Delete"
                Dim lShouldReturn As Boolean
                DeleteCommand(e, lShouldReturn)
                If lShouldReturn Then
                    Return
                End If
            Case "Add"
                AddCommand(e)
            Case "Edit"
                dgSPDetail_EditCommand(e)
            Case "Update"
                dgSPDetail_Update(e)
            Case "Cancel"
                dgSPDetail_CancelCommand(e)
        End Select
    End Sub
    Private Sub DeleteCommand(ByVal e As DataGridCommandEventArgs, ByRef shouldReturn As Boolean)
        shouldReturn = False
        Dim lblNamaType As Label = CType(e.Item.FindControl("lblNamaType"), Label)

        'Delete item yang index nya itu sesuai dengan index item yg di filter
        Dim list As ArrayList = CType(sessHelper.GetSession("SPLDETAILLIST"), ArrayList)
        Dim _splDetailDelete As New SPLDetail
        For Each item As SPLDetail In list
            If item.PeriodMonth = CType(sessHelper.GetSession("STATUSMONTH"), Date).Month And item.PeriodYear = CType(sessHelper.GetSession("STATUSMONTH"), Date).Year And item.VechileType.Description = CType(e.Item.FindControl("lblNamaType"), Label).Text Then
                _splDetailDelete = item
            End If
        Next
        list.Remove(_splDetailDelete)
        BindDetail(CInt(sessHelper.GetSession("IDSPLHeader")))
    End Sub
    Private Sub AddCommand(ByVal e As DataGridCommandEventArgs)
        If Not Page.IsValid Then
            Return
        End If

        Dim txtKodeModel As TextBox = e.Item.FindControl("txtFooterKodeModel")
        Dim txtTop As TextBox = e.Item.FindControl("txtFooterTOP")
        Dim txtSurcharge As TextBox = e.Item.FindControl("txtFooterSurcharge")
        Dim txtUnit As TextBox = e.Item.FindControl("txtFooterUnit")
        Dim txtDiscount As TextBox = e.Item.FindControl("txtFooterDiscount")
        Dim txtPriceRefDate As TextBox = e.Item.FindControl("txtFooterPriceRef")
        Dim cboInterest As DropDownList = CType(e.Item.FindControl("cboFooterInterest"), DropDownList)
        Dim cboCreditCeiling As DropDownList = CType(e.Item.FindControl("cboFooterCreditCeiling"), DropDownList)
        'Dim txtFooterDeliveryDate As TextBox = e.Item.FindControl("txtFooterDeliveryDate")
        If txtSurcharge.Text.Trim = "" Then txtSurcharge.Text = "0"
        If txtTop.Text.Trim <> "" And txtUnit.Text.Trim <> "" And txtPriceRefDate.Text.Trim <> "" Then
            If ValidateData(txtTop.Text.Trim, txtKodeModel.Text.Trim, txtPriceRefDate.Text.Trim, txtUnit.Text) Then

                ObjSPLDetail = New SPLDetail

                Dim objVehicleFacade As VechileTypeFacade = New VechileTypeFacade(User)
                Dim objVecType As VechileType = objVehicleFacade.Retrieve(txtKodeModel.Text.Trim)
                ObjSPLDetail.VechileType = objVecType

                ' Masalah TOP
                Dim a() As String = txtTop.Text.Split(",")
                ObjSPLDetail.MaxTopIndicator = Integer.Parse(a(0))
                If Not (a(1) = "") Then
                    ObjSPLDetail.MaxTopDate = Date.Parse(a(1))
                End If
                If Not a(2) = "" Then
                    ObjSPLDetail.MaxTopDay = Integer.Parse(a(2))
                    'tambahan validasi
                    Dim objFTOP As TermOfPaymentFacade = New TermOfPaymentFacade(User)
                    Dim criterias As New CriteriaComposite(New Criteria(GetType(TermOfPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(TermOfPayment), "TermOfPaymentValue", MatchType.Exact, ObjSPLDetail.MaxTopDay))

                    Dim agg As Aggregate = New Aggregate(GetType(TermOfPayment), "TermOfPaymentValue", AggregateType.Max)
                    Dim JJ = objFTOP.RetrieveScalar(criterias, agg)
                    Dim maxDay As Integer = 0
                    If Not IsNothing(JJ) Then
                        maxDay = CInt(JJ)
                    End If

                    If ObjSPLDetail.MaxTopDay > maxDay Then
                        MessageBox.Show("Pilihan TOP " + ObjSPLDetail.MaxTopDay.ToString() + " hari tidak terdaftar   ")
                        Return
                    End If

                    'end of validasi
                End If
                ObjSPLDetail.Surcharge = Integer.Parse(txtSurcharge.Text.Replace(".", ""))
                ObjSPLDetail.PriceRefDate = GetDateFromMonthYear(txtPriceRefDate.Text, 1)
                ObjSPLDetail.Quantity = Integer.Parse(txtUnit.Text.Trim.Replace(".", ""))
                ObjSPLDetail.FreeIntIndicator = cboInterest.SelectedItem.Value
                ObjSPLDetail.CreditCeiling = cboCreditCeiling.SelectedItem.Value
                'ObjSPLDetail.DeliveryDate = GetDateFromMonthYear(txtFooterDeliveryDate.Text, 1)
                ObjSPLDetail.PeriodMonth = CType(sessHelper.GetSession("STATUSMONTH"), Date).Month
                ObjSPLDetail.PeriodYear = CType(sessHelper.GetSession("STATUSMONTH"), Date).Year

                If txtDiscount.Text.Trim = "" Then
                    ObjSPLDetail.Discount = 0
                Else
                    ObjSPLDetail.Discount = Integer.Parse(txtDiscount.Text.Trim.Replace(".", ""))
                End If

                Dim list As ArrayList = CType(sessHelper.GetSession("SPLDETAILLIST"), ArrayList)
                If Not ValidateDuplication(txtKodeModel.Text.Trim, CType(sessHelper.GetSession("STATUSMONTH"), Date), "add", e.Item.ItemIndex) Then
                    Return
                End If

                If Not list Is Nothing Then
                    list.Add(ObjSPLDetail)
                End If
                BindDetail(CInt(sessHelper.GetSession("IDSPLHeader")))
            End If
        Else
            MessageBox.Show("Isi detail dengan lengkap")
        End If

    End Sub
    Private Function ValidateData(ByVal _top As String, ByVal _kodeModel As String, ByVal _refDate As String, ByVal _unit As String) As Boolean
        Dim bcheck As Boolean = True
        ' Masalah TOP
        Dim a() As String = _top.Split(",")
        If a.Length <> 3 Then
            MessageBox.Show("Top invalid")
            bcheck = False
        End If
        If Not (a(1) = "") Then
            Try
                Dim _temp = Date.Parse(a(1))
            Catch ex As Exception
                MessageBox.Show("Top invalid")
                bcheck = False
            End Try
        End If
        Dim objVecType As VechileType = New VechileTypeFacade(User).Retrieve(_kodeModel.Trim)
        If objVecType.ID = 0 Then
            MessageBox.Show("Kode Model Invalid")
            bcheck = False
        End If
        If ValidateDate(_refDate) = False Then
            MessageBox.Show("Tanggal Ref Harga Invalid")
            bcheck = False
        End If
        'If ValidateDate(_period) = False Then
        '    MessageBox.Show("Periode Kirim Invalid")
        '    bcheck = False
        'End If
        If _unit.Trim = "0" Then
            MessageBox.Show("Unit harus lebih besar dari 0")
            bcheck = False
        End If
        Return bcheck
    End Function
    Private Function ValidateDate(ByVal periods As String) As Boolean
        Dim bcheck As Boolean = True
        Try
            Dim dd1 As Date = GetDateFromMonthYear(periods.Trim(), 1)
        Catch ex As Exception
            bcheck = False
        End Try
        Return bcheck
    End Function
    Private Function ValidateDealers(ByVal _dealers As String) As Boolean
        Dim bcheck As Boolean = True
        Dim i As Integer
        Dim items() As String = _dealers.Split(";")
        For i = 0 To items.Length - 2
            Dim objDealerTmp As Dealer = New DealerFacade(User).Retrieve(items(i))
            If objDealerTmp.ID = 0 Then
                MessageBox.Show("Dealer " + items(i) + "tidak valid")
                bcheck = False
                Exit For
            End If

        Next
        If ValidateDealerDuplication(_dealers) <> String.Empty Then
            MessageBox.Show("Duplikasi Dealer " + ValidateDealerDuplication(_dealers))
            bcheck = False
        End If
        Return bcheck
    End Function
    Private Function ValidateDealerDuplication(ByVal _dealers As String) As String
        Dim bcheck As Boolean = True
        Dim _dealerDuplicate As String = String.Empty
        Dim i As Integer
        Dim j As Integer
        Dim list() As String = _dealers.Split(";")
        For i = 0 To list.Length - 2
            For j = i + 1 To list.Length - 1
                If list(i) = list(j) Then
                    bcheck = False
                    Exit For
                End If
            Next
            If bcheck = False Then
                _dealerDuplicate = list(i)
                Exit For
            End If
        Next
        Return _dealerDuplicate
    End Function
    Private Function ValidateDuplication(ByVal kodeModel As String, ByVal _period As Date, ByVal Mode As String, ByVal Rowindex As Integer) As Boolean
        Dim bcheck As Boolean = True
        Dim list As ArrayList = CType(sessHelper.GetSession("SPLVIEW"), ArrayList)
        Dim _objSPL As New SPL
        If Not list Is Nothing Then

            For Each item As SPLDetail In list
                _objSPL.SPLDetails.Add(item)
            Next
        Else
            list = New ArrayList
        End If

        If (Mode = "Add") Then
            If Not list Is Nothing Then
                For Each item As SPLDetail In _objSPL.SPLDetails
                    If (item.VechileType.VechileTypeCode.ToString = kodeModel And item.PeriodMonth = _period.Month And item.PeriodYear = _period.Year) Then
                        MessageBox.Show("Error : Duplikasi Kode Tipe")
                        bcheck = False
                    End If

                Next
            End If
        Else
            Dim i As Integer = 0
            For Each item As SPLDetail In _objSPL.SPLDetails
                If (item.VechileType.VechileTypeCode.ToString = kodeModel) Then
                    If i <> Rowindex Then
                        MessageBox.Show("Error : Duplikasi Kode Tipe")
                        bcheck = False
                    End If
                End If

                i = i + 1
            Next
        End If
        Return bcheck
    End Function
    Sub dgSPDetail_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles dgSPDetail.ItemDataBound
        Dim list As ArrayList = CType(sessHelper.GetSession("SPLVIEW"), ArrayList)

        If Convert.ToString(sessHelper.GetSession("Status")) = "View" Then
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                SetDgSPLDetailItemView(e, list)
                Dim lbtnDelete As LinkButton = e.Item.FindControl("lbtnDelete")
                lbtnDelete.Visible = False
                Dim lbtnEdit As LinkButton = e.Item.FindControl("lbtnEdit")
                lbtnEdit.Visible = False
            End If
        Else
            If e.Item.ItemType = ListItemType.Footer Then
                SetDgSPLDetailItemFooter(e)
            ElseIf e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
                SetDgSPLDetailItemEdit(e, list)
            ElseIf e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                SetDgSPLDetailItemView(e, list)
            End If
            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", "return confirm('Anda yakin data ini ingin dihapus?');")
            End If
        End If

    End Sub
    Private Sub SetDgSPLDetailItemView(ByVal e As DataGridItemEventArgs, ByVal list As ArrayList)
        If Not list Is Nothing Then
            Dim _objSPLTmp As New SPL
            Dim _objSPLDetail As New SPLDetail
            _objSPLTmp = New SPLFacade(User).Retrieve(CInt(sessHelper.GetSession("IDSPLHeader")))

            Dim listAll As ArrayList = CType(sessHelper.GetSession("SPLDETAILLIST"), ArrayList)
            _objSPLDetail = CType(list(e.Item.ItemIndex), SPLDetail)
            IndexList = GetIndexSPLDETAILLIST(listAll, _objSPLDetail)

            _objSPLDetail.SPL = _objSPLTmp
            Dim _lblNamaTipe As Label = CType(e.Item.FindControl("lblNamaType"), Label)
            _lblNamaTipe.Text = New VechileTypeFacade(User).Retrieve(_objSPLDetail.VechileType.ID).Description

            Dim _lblInterest As Label = CType(e.Item.FindControl("lblInterest"), Label)
            _lblInterest.Text = CType(_objSPLDetail.FreeIntIndicator, SPLEnum.Interest).ToString.Replace("_", " ")

            Dim _lblCreditCeiling As Label = CType(e.Item.FindControl("lblCreditCeiling"), Label)
            _lblCreditCeiling.Text = CType(Integer.Parse(_objSPLDetail.CreditCeiling.ToString()), SPLEnum.CreditCeiling).ToString.Replace("_", " ")

            Dim _lblPeriodePrice As Label = CType(e.Item.FindControl("lblViewPriceRef"), Label)
            _lblPeriodePrice.Text = CType(_objSPLDetail.PriceRefDate.Month - 1, enumMonth.Month).ToString.Replace("_", " ") + " " + _objSPLDetail.PriceRefDate.Year.ToString()

            Dim _lblDeliveryTime As Label = CType(e.Item.FindControl("lblDeliveryTime"), Label)
            _lblDeliveryTime.Text = CType(_objSPLDetail.DeliveryDate.Month - 1, enumMonth.Month).ToString.Replace("_", " ") + " " + _objSPLDetail.DeliveryDate.Year.ToString()

            Dim _lblViewTop As Label = CType(e.Item.FindControl("lblViewTop"), Label)
            If _objSPLDetail.MaxTopIndicator = 1 Then
                _lblViewTop.Text = _objSPLDetail.MaxTopDay.ToString + " hari"
            ElseIf _objSPLDetail.MaxTopIndicator = 0 Then
                _lblViewTop.Text = "s.d " + _objSPLDetail.MaxTopDate.Day.ToString() + " " + CType(_objSPLDetail.MaxTopDate.Month - 1, enumMonth.Month).ToString.Replace("-", " ") + " " + _objSPLDetail.MaxTopDate.Year.ToString()
            Else
                _lblViewTop.Text = "COD"
            End If

            Dim _lblPeriodMonth As Label = CType(e.Item.FindControl("lblPeriodMonth"), Label)
            _lblPeriodMonth.Text = CType(sessHelper.GetSession("STATUSMONTH"), Date).Month.ToString

            Dim _lblPeriodYear As Label = CType(e.Item.FindControl("lblPeriodYear"), Label)
            _lblPeriodYear.Text = CType(sessHelper.GetSession("STATUSMONTH"), Date).Year.ToString

            Dim lblViewPK As Label = CType(e.Item.FindControl("lblViewPK"), Label)

            lblViewPK.Attributes("onclick") = "showPopUp('../FinishUnit/FrmPKHeaderSPL.aspx?_splnumber=" & _objSPLTmp.SPLNumber & "&_kodetipe=" & _objSPLDetail.VechileType.VechileTypeCode & "&_periodemonth=" & _objSPLDetail.PeriodMonth & "&_periodeyear=" & _objSPLDetail.PeriodYear & "','',400,500,'');"
            Dim lblSisaUnit As Label = CType(e.Item.FindControl("lblSisaUnit"), Label)
            lblSisaUnit.Text = (_objSPLDetail.Quantity - GetResponseQtyPKDetail(_objSPLDetail)).ToString
        End If
    End Sub
    Private Sub dgSPDetail_EditCommand(ByVal e As DataGridCommandEventArgs)
        dgSPDetail.ShowFooter = False
        dgSPDetail.EditItemIndex = CInt(e.Item.ItemIndex)
        BindDetail(CInt(sessHelper.GetSession("IDSPLHeader")))
    End Sub
    Private Sub dgSPDetail_CancelCommand(ByVal E As DataGridCommandEventArgs)
        dgSPDetail.EditItemIndex = -1
        BindDetail(CInt(sessHelper.GetSession("IDSPLHeader")))
        dgSPDetail.ShowFooter = True
    End Sub
    Private Sub dgSPDetail_Update(ByVal e As DataGridCommandEventArgs)
        UpdateCommand(e)
    End Sub
    Private Sub UpdateCommand(ByVal e As DataGridCommandEventArgs)
        If Not Page.IsValid Then
            Return
        End If
        If Convert.ToString(sessHelper.GetSession("Status")) = "View" Then
            dgSPDetail.EditItemIndex = -1
            BindDetail(CInt(sessHelper.GetSession("IDSPLHeader")))
        Else
            Dim txtKodeModel As TextBox = e.Item.FindControl("txtEditKodeModel")
            Dim txtTop As TextBox = e.Item.FindControl("txtEditTOP")
            Dim txtSurcharge As TextBox = e.Item.FindControl("txtEditSurcharge")
            Dim txtUnit As TextBox = e.Item.FindControl("txtEditUnit")
            Dim txtDiscount As TextBox = e.Item.FindControl("txtEditDiscount")
            Dim txtPriceRefDate As TextBox = e.Item.FindControl("txtEditPriceRef")
            Dim cboInterest As DropDownList = CType(e.Item.FindControl("cboEditInteres"), DropDownList)
            Dim cboCreditCeiling As DropDownList = CType(e.Item.FindControl("cboEditCreditCeiling"), DropDownList)
            'Dim txtFooterDeliveryDate As TextBox = e.Item.FindControl("txtEditDeliveryDate")
            If txtTop.Text.Trim <> "" And txtUnit.Text.Trim <> "" And txtPriceRefDate.Text.Trim <> "" Then
                If ValidateData(txtTop.Text.Trim, txtKodeModel.Text.Trim, txtPriceRefDate.Text.Trim, txtUnit.Text) Then

                    Dim listAll As ArrayList = CType(sessHelper.GetSession("SPLDETAILLIST"), ArrayList)
                    Dim list As ArrayList = CType(sessHelper.GetSession("SPLVIEW"), ArrayList)

                    ObjSPLDetail = New SPLDetail
                    ObjSPLDetail = CType(list(e.Item.ItemIndex), SPLDetail)
                    IndexList = GetIndexSPLDETAILLIST(listAll, ObjSPLDetail)

                    Dim objVehicleFacade As VechileTypeFacade = New VechileTypeFacade(User)
                    Dim objVecType As VechileType = objVehicleFacade.Retrieve(txtKodeModel.Text.Trim)
                    ObjSPLDetail.VechileType = objVecType
                    ObjSPLDetail.Surcharge = Val(txtSurcharge.Text.Replace(".", ""))
                    ObjSPLDetail.Quantity = Integer.Parse(txtUnit.Text.Trim.Replace(".", ""))
                    ObjSPLDetail.PriceRefDate = GetDateFromMonthYear(txtPriceRefDate.Text, 1)
                    ObjSPLDetail.FreeIntIndicator = cboInterest.SelectedItem.Value
                    ObjSPLDetail.CreditCeiling = cboCreditCeiling.SelectedItem.Value
                    'ObjSPLDetail.DeliveryDate = GetDateFromMonthYear(txtFooterDeliveryDate.Text, 1)
                    ObjSPLDetail.PeriodMonth = CType(sessHelper.GetSession("STATUSMONTH"), Date).Month
                    ObjSPLDetail.PeriodYear = CType(sessHelper.GetSession("STATUSMONTH"), Date).Year

                    If txtDiscount.Text.Trim = "" Then
                        ObjSPLDetail.Discount = 0
                    Else
                        ObjSPLDetail.Discount = Integer.Parse(txtDiscount.Text.Trim.Replace(".", ""))
                    End If

                    If Not list Is Nothing Then
                        listAll.RemoveAt(IndexList)
                        listAll.Insert(IndexList, ObjSPLDetail)
                    End If

                    dgSPDetail.EditItemIndex = -1
                    BindDetail(CInt(sessHelper.GetSession("IDSPLHeader")))
                    dgSPDetail.ShowFooter = True
                End If

            Else
                MessageBox.Show("Isi detail dengan lengkap")
            End If
        End If

    End Sub
    Private Function GetIndexSPLDETAILLIST(ByVal _list As ArrayList, ByVal _obj As SPLDetail) As Integer
        Dim i As Integer = 0
        For Each item As SPLDetail In _list
            If item.VechileType.VechileTypeCode = _obj.VechileType.VechileTypeCode And item.PeriodMonth = _obj.PeriodMonth And item.PeriodYear = _obj.PeriodYear Then
                Exit For
            End If
            i = i + 1
        Next
        Return i
    End Function
    Sub lbtnPrevMonth_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles lbtnPrevMonth.Click
        Dim _prevDate As Date
        If Not sessHelper.GetSession("STATUSMONTH") Is Nothing Then
            Dim _sessDate As Date = CType(sessHelper.GetSession("STATUSMONTH"), Date).Date
            Dim _sessMinDate As Date = CType(sessHelper.GetSession("STATUSMINMONTH"), Date).Date
            If _sessDate.Month = 1 Then
                _prevDate = GetDateFromMonthYear((12).ToString + (_sessDate.Year - 1).ToString, 1)
            Else
                _prevDate = GetDateFromMonthYear((_sessDate.Month - 1).ToString + _sessDate.Year.ToString, 1)
            End If

            If DateDiff(DateInterval.Day, _sessMinDate, _prevDate, Microsoft.VisualBasic.FirstDayOfWeek.Monday, FirstWeekOfYear.Jan1) >= 0 Then
                sessHelper.SetSession("STATUSMONTH", _prevDate)
                lblCurrentPeriode.Text = CType(_prevDate.Month - 1, enumMonth.Month).ToString.Replace("_", " ") + " " + _prevDate.Year.ToString
                BindDetail(CInt(sessHelper.GetSession("IDSPLHeader")))
            Else
                MessageBox.Show("Periode Detil Melampaui Periode Aplikasi")
            End If
        End If
    End Sub
    Sub lbtnNextMonth_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles lbtnNextMonth.Click
        Dim _nextDate As Date
        If Not sessHelper.GetSession("STATUSMONTH") Is Nothing Then
            Dim _sessDate As Date = CType(sessHelper.GetSession("STATUSMONTH"), Date).Date
            Dim _sessMaxDate As Date = CType(sessHelper.GetSession("STATUSMAXMONTH"), Date).Date
            If _sessDate.Month = 12 Then
                _nextDate = GetDateFromMonthYear((1).ToString + (_sessDate.Year + 1).ToString, 1)
            Else
                _nextDate = GetDateFromMonthYear((_sessDate.Month + 1).ToString + _sessDate.Year.ToString, 1)
            End If
            If DateDiff(DateInterval.Day, _nextDate, _sessMaxDate, Microsoft.VisualBasic.FirstDayOfWeek.Monday, FirstWeekOfYear.Jan1) >= 0 Then
                sessHelper.SetSession("STATUSMONTH", _nextDate)
                lblCurrentPeriode.Text = CType(_nextDate.Month - 1, enumMonth.Month).ToString.Replace("_", " ") + " " + _nextDate.Year.ToString
                BindDetail(CInt(sessHelper.GetSession("IDSPLHeader")))
            Else
                MessageBox.Show("Periode Detil Melampaui Periode Aplikasi")
            End If
        End If
    End Sub
    Private Sub ddlStatus_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlStatus.SelectedIndexChanged
        If ddlStatus.SelectedValue = 1 Then
            lbtnNextMonth.Visible = False
            lbtnPrevMonth.Visible = False
            dgSPDetail.ShowFooter = False
        Else
            lbtnNextMonth.Visible = True
            lbtnPrevMonth.Visible = True
            dgSPDetail.ShowFooter = True
            BindDetail(CInt(sessHelper.GetSession("IDSPLHeader")))
        End If
    End Sub
    Private Function GetResponseQtyPKDetail(ByVal obj As SPLDetail) As Integer
        Dim _total As Integer = 0
        Dim criterias As New CriteriaComposite(New Criteria(GetType(PKDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.SPLNumber", MatchType.Exact, obj.SPL.SPLNumber))
        criterias.opAnd(New Criteria(GetType(PKDetail), "VehicleTypeCode", MatchType.Exact, obj.VechileType.VechileTypeCode))
        criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.RequestPeriodeMonth", MatchType.Exact, obj.PeriodMonth))
        criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.RequestPeriodeYear", MatchType.Exact, obj.PeriodYear))
        criterias.opAnd(New Criteria(GetType(PKDetail), "PKHeader.PKStatus", MatchType.InSet, "(" & CType(enumStatusPK.Status.Rilis, Integer) _
                & "," & CType(enumStatusPK.Status.Selesai, Integer) & "," & CType(enumStatusPK.Status.Konfirmasi, Integer) & "," & CType(enumStatusPK.Status.Setuju, Integer) & ")"))
        Dim _result As ArrayList = New PKDetailFacade(User).RetrieveByCriteria(criterias)

        For Each items As PKDetail In _result
            _total = _total + items.ResponseQty
        Next
        Return _total
    End Function
#End Region

End Class
