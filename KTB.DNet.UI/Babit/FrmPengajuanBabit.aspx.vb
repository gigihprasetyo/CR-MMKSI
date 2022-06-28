Imports KTB.DNet.Utility
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain.Search
Imports System.Text
Imports System.IO
Imports KTB.DNet.Security

Public Class FrmPengajuanBabit
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealerCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblCity As System.Web.UI.WebControls.Label
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents lblNoPengajuan As System.Web.UI.WebControls.Label
    Protected WithEvents lblProvince As System.Web.UI.WebControls.Label
    Protected WithEvents ddlJenisKegiatan As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblSisaAlokasiBabit As System.Web.UI.WebControls.Label
    Protected WithEvents dgIklan As System.Web.UI.WebControls.DataGrid
    Protected WithEvents dgPameran As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents txtTempatPameran As System.Web.UI.WebControls.TextBox
    Protected WithEvents icDatePameranMulai As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icDatePameranAkhir As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtUkuranTempatPameran As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtJumlahHariPameran As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTargetPenjualanPameran As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtBiayaPameran As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTempatEvent As System.Web.UI.WebControls.TextBox
    Protected WithEvents icDateEventMulai As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents icDateEventAkhir As KTB.DNet.WebCC.IntiCalendar
    Protected WithEvents txtUkuranTempatEvent As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtJumlahHariEvent As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTargetPenjualanEvent As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnSubmit As System.Web.UI.WebControls.Button
    Protected WithEvents lblPeriode As System.Web.UI.WebControls.Label
    Protected WithEvents hdn As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnPameran As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnEvent As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnIklan As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lblNoPerjanjian As System.Web.UI.WebControls.Label
    Protected WithEvents hdnPameranSubmit As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnEventSubmit As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents hdnIklanSubmit As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lblJenisKegiatan As System.Web.UI.WebControls.Label
    Protected WithEvents lblTempatPameran As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglPameranAwal As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglPameranAkhir As System.Web.UI.WebControls.Label
    Protected WithEvents lblUkuranTempatPameran As System.Web.UI.WebControls.Label
    Protected WithEvents lblJumlahHariPameran As System.Web.UI.WebControls.Label
    Protected WithEvents lblTargetPejualanPameran As System.Web.UI.WebControls.Label
    Protected WithEvents lblBiayaPameran As System.Web.UI.WebControls.Label
    Protected WithEvents lblTempatEvent As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglEventAwal As System.Web.UI.WebControls.Label
    Protected WithEvents lblTglEventAkhir As System.Web.UI.WebControls.Label
    Protected WithEvents lblUkuranTempatEvent As System.Web.UI.WebControls.Label
    Protected WithEvents lblJumlahHariEvent As System.Web.UI.WebControls.Label
    Protected WithEvents lblTargetPenjualanEvent As System.Web.UI.WebControls.Label
    Protected WithEvents btnBack As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents divPameran As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents divEvent As System.Web.UI.HtmlControls.HtmlTable
    Protected WithEvents divIklan As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents divPameranGrid As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents pnlScript As System.Web.UI.WebControls.Panel
    Protected WithEvents dgEvent As System.Web.UI.WebControls.DataGrid
    Protected WithEvents divEventGrid As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents btnCancel As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents phJS As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents hdnSelectedVehicleType As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents phBottomScript As System.Web.UI.WebControls.PlaceHolder
    Protected WithEvents hdnSelectedVehicleTypeIndex As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents btnBaru As System.Web.UI.WebControls.Button
    Protected WithEvents lblFileUpload As System.Web.UI.WebControls.Label
    Protected WithEvents UploadFile As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnPrint As System.Web.UI.HtmlControls.HtmlInputButton
    Protected WithEvents hdnValNew As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents lblTotalBiayaIklan As System.Web.UI.WebControls.Label
    Protected WithEvents hdnValSubmit As System.Web.UI.HtmlControls.HtmlInputHidden
    Protected WithEvents txtNoSuratDealer As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPopUpDealer As System.Web.UI.WebControls.Label
    Protected WithEvents txtDealerCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnFindDealer As System.Web.UI.WebControls.Button
    Protected WithEvents lblTotalBiayaPameran As System.Web.UI.WebControls.Label
    Protected WithEvents lblTotalBiayaEvent As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Custom Variables Declaration"

    Dim objDealer As New Dealer
    Dim en As EnumBabit
    Private arlIklan As ArrayList = New ArrayList
    Private arlEvent As ArrayList = New ArrayList
    Private arlPameran As ArrayList = New ArrayList
    Private FU As String = "UploadFile"
    Private FU_NAME As String = "FU_FileName"
    Private MAX_FILE_SIZE As Integer = 5120000

    Private sesHelper As New SessionHelper
    Private Property SesDealer() As Dealer
        Get
            Return CType(sesHelper.GetSession("DEALER"), Dealer)
        End Get
        Set(ByVal Value As Dealer)
            sesHelper.SetSession("DEALER", Value)
        End Set
    End Property
#End Region

#Region "Custom Methods"

    Private Function SetCompPrivilage(ByVal blnAccess As Boolean)
        btnSave.Enabled = blnAccess
        btnPrint.Visible = blnAccess
        UploadFile.Visible = blnAccess
        dgEvent.ShowFooter = blnAccess
        dgIklan.ShowFooter = blnAccess
        dgPameran.ShowFooter = blnAccess
    End Function

    Private Sub GenerateJS()
        Dim strHash As String = String.Empty
        Dim arlCategory As ArrayList = New FinishUnit.CategoryFacade(User).RetrieveActiveList()

        strHash += """-1"":[{""ID"":""-1"", ""Name"":""Silahkan Pilih""}]"
        For Each itemCategory As Category In arlCategory

            If strHash.Length > 0 Then
                strHash += ","
            End If
            strHash += "" + itemCategory.ID.ToString() + ":["

            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(VechileType), "Category", MatchType.Exact, itemCategory.ID))
            criterias.opAnd(New Criteria(GetType(VechileType), "Status", MatchType.Exact, "A"))

            Dim sortColl As SortCollection = New SortCollection
            sortColl.Add(New Sort(GetType(VechileType), "Description", Sort.SortDirection.ASC))

            Dim arlVehicleType As ArrayList = New FinishUnit.VechileTypeFacade(User).Retrieve(criterias, sortColl)

            Dim strObj As String = String.Empty
            For Each item As VechileType In arlVehicleType
                If strObj.Length > 0 Then
                    strObj += ","
                End If
                strObj += "{"
                strObj += String.Format("""ID"":""{0}"", ""Name"":""{1}""", item.ID, item.Description)
                strObj += "}"
            Next

            strHash += strObj + "]"
        Next
        strHash = "<script language=""javascript""> var arrVehicleType = {" + strHash + "};"

        strHash += " function DisplayVehicleType(ddlCategory, ddlVehicle){" + vbNewLine
        strHash += "   var catVal = ddlCategory.options[ddlCategory.selectedIndex].value;" + vbNewLine
        strHash += "   while(ddlVehicle.options.length > 1){" + vbNewLine
        strHash += "	if(navigator.appName == ""Microsoft Internet Explorer"") {" + vbNewLine
        strHash += "        ddlVehicle.options.remove(ddlVehicle.options.length - 1);}" + vbNewLine
        strHash += "    else{" + vbNewLine
        strHash += "        ddlVehicle.options[1]=null;" + vbNewLine
        strHash += "    }" + vbNewLine
        strHash += "   }" + vbNewLine
        strHash += "   if(arrVehicleType[catVal]){" + vbNewLine
        strHash += "    for(var i = 0; i < arrVehicleType[catVal].length; i++){" + vbNewLine
        strHash += "      var newOption = document.createElement(""OPTION""); " + vbNewLine
        strHash += "      ddlVehicle.options.add(newOption); " + vbNewLine
        strHash += "	if(navigator.appName == ""Microsoft Internet Explorer"") {" + vbNewLine
        strHash += "      newOption.innerText = arrVehicleType[catVal][i].Name;}" + vbNewLine
        strHash += "    else{" + vbNewLine
        strHash += "      newOption.innerHTML = arrVehicleType[catVal][i].Name;}" + vbNewLine
        strHash += "      newOption.value = arrVehicleType[catVal][i].ID;" + vbNewLine
        strHash += "    }" + vbNewLine
        strHash += "   }" + vbNewLine
        strHash += "   SaveSelectedVehicle(ddlVehicle);" + vbNewLine
        strHash += "   return;" + vbNewLine
        strHash += "}" + vbNewLine
        strHash += "</script>" + vbNewLine

        phJS.Controls.Add(New LiteralControl(strHash))
    End Sub

    Private Sub ShowNoOfDays()
        txtJumlahHariEvent.Text = (DateDiff(DateInterval.Day, icDateEventMulai.Value, icDateEventAkhir.Value) + 1).ToString()
        txtJumlahHariPameran.Text = (DateDiff(DateInterval.Day, icDatePameranMulai.Value, icDatePameranAkhir.Value) + 1).ToString()
    End Sub

    Private Sub fillDealerInfoFromKTB()
        Dim arlBabitAllocation As New ArrayList
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Status", MatchType.Exact, CType(EnumBabit.BabitAllocationStatus.Rilis, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.AllocationType", MatchType.Exact, CType(EnumBabit.BabitAllocationType.Alokasi_Reguler, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.StartPeriod", MatchType.LesserOrEqual, Month(Date.Now)))
        'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.EndPeriod", MatchType.GreaterOrEqual, Month(Date.Now)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.BabitYear", MatchType.LesserOrEqual, Year(Date.Now)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.BabitYearEnd", MatchType.GreaterOrEqual, Year(Date.Now)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Dealer.DealerCode", MatchType.Exact, txtDealerCode.Text.Trim()))
        arlBabitAllocation = New BabitSalesComm.BabitFacade(User).RetrieveBabitAllocation(criterias, "CreatedTime", Sort.SortDirection.DESC)

        Dim objTmpDealer As Dealer
        If (Request.QueryString("Mode") = "NewFromAlloc") Then
            objTmpDealer = New KTB.DNet.BusinessFacade.General.DealerFacade(User).Retrieve(txtDealerCode.Text.Trim())
            If (IsNothing(objTmpDealer)) Then
                MessageBox.Show("Kode dealer tidak ditemukan")
                Return
            End If
            lblDealerCode.Text = objTmpDealer.DealerCode
            lblDealerName.Text = objTmpDealer.DealerName
            lblCity.Text = objTmpDealer.City.CityName
            lblProvince.Text = objTmpDealer.Province.ProvinceName
        End If

        If (arlBabitAllocation.Count = 0) Then
            btnSave.Enabled = False
        Else
            Dim oBabitAllocation As New BabitAllocation
            If (Request.QueryString("Mode") = "NewFromAlloc") Then
                Dim n As Integer = 0
                For Each obj As BabitAllocation In arlBabitAllocation
                    Dim dtmFrom As DateTime = New DateTime(obj.Babit.BabitYear, obj.Babit.StartPeriod, 1)
                    Dim dtmTo As DateTime = New DateTime(obj.Babit.BabitYearEnd, obj.Babit.EndPeriod, 2)
                    If (DateTime.Now >= dtmFrom) And (DateTime.Now <= dtmTo) Then
                        oBabitAllocation = New BabitAllocation
                        oBabitAllocation = obj
                        Exit For
                    Else
                        n += 1
                    End If
                Next
                If (n >= arlBabitAllocation.Count) Then
                    MessageBox.Show("Alokasi babit khusus untuk periode ini tidak tersedia")
                    Return
                End If
            Else
                oBabitAllocation = CType(arlBabitAllocation(0), BabitAllocation)
            End If
            sesHelper.SetSession("BabitAllocation", oBabitAllocation)
            lblNoPerjanjian.Text = oBabitAllocation.NoPerjanjian
            lblPeriode.Text = String.Format("{0} - {1}", New DateTime(oBabitAllocation.Babit.BabitYear, oBabitAllocation.Babit.StartPeriod, 1).ToString("dd MMM yyyy"), New DateTime(oBabitAllocation.Babit.BabitYearEnd, oBabitAllocation.Babit.EndPeriod, 1).ToString("dd MMM yyyy"))
            lblSisaAlokasiBabit.Text = Convert.ToInt64(oBabitAllocation.SisaBabit).ToString("#,##0")
            lblDealerCode.Text = oBabitAllocation.Dealer.DealerCode
            lblDealerName.Text = oBabitAllocation.Dealer.DealerName
            lblCity.Text = oBabitAllocation.Dealer.City.CityName
            lblProvince.Text = oBabitAllocation.Dealer.Province.ProvinceName
            End If
    End Sub

    Private Function SaveFile(ByVal _filename As String) As Boolean
        Dim nResult As Boolean = False
        Dim DestFile As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") & KTB.DNet.Lib.WebConfig.GetValue("PengajuanBabit") & "\" & _filename      '-- Destination file
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim success As Boolean = False

        Dim finfo As FileInfo
        Try
            success = imp.Start()
            If success Then
                finfo = New FileInfo(DestFile)

                If Not finfo.Directory.Exists Then
                    Directory.CreateDirectory(finfo.DirectoryName)
                End If

                Dim ext As String = System.IO.Path.GetExtension(CType(sesHelper.GetSession(FU_NAME), String))
                If finfo.Exists Then
                    finfo.Delete()
                End If

                Dim fuStream As Stream = (CType(sesHelper.GetSession(FU), Stream))
                Dim ibytes As Long = fuStream.Length
                Dim buffer(ibytes - 1) As Byte
                fuStream.Read(buffer, 0, ibytes)
                fuStream.Close()

                Dim fs As FileStream = New FileStream(DestFile, FileMode.Create)
                fs.Write(buffer, 0, ibytes)
                fs.Close()

                'UploadFile.PostedFile.SaveAs(DestFile)
                imp.StopImpersonate()
                imp = Nothing
                nResult = True
            End If
        Catch ex As Exception
            nResult = False
            Throw ex
        End Try
        Return nResult
    End Function

    Private Function IsLoginAsDealer() As Boolean
        Return (SesDealer.TitleDealer = EnumDealerTittle.DealerTittle.DEALER.ToString())
    End Function

    Sub DealerData(ByVal oDealer As Dealer)
        If IsLoginAsDealer() Then
            Dim arlBabitAllocation As New ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Status", MatchType.Exact, CType(EnumBabit.BabitAllocationStatus.Rilis, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.AllocationType", MatchType.Exact, CType(EnumBabit.BabitAllocationType.Alokasi_Reguler, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.StartPeriod", MatchType.LesserOrEqual, Month(Date.Now)))
            'criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.EndPeriod", MatchType.GreaterOrEqual, Month(Date.Now)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.BabitYear", MatchType.LesserOrEqual, Year(Date.Now)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Babit.BabitYearEnd", MatchType.GreaterOrEqual, Year(Date.Now)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.BabitAllocation), "Dealer.DealerCode", MatchType.Exact, oDealer.DealerCode))
            arlBabitAllocation = New BabitSalesComm.BabitFacade(User).RetrieveBabitAllocation(criterias, "CreatedTime", Sort.SortDirection.DESC)
            If (arlBabitAllocation.Count = 0) Then
                btnSave.Enabled = False
            Else
                Dim n As Integer = 0
                Dim oBabitAllocation As New BabitAllocation
                For Each obj As BabitAllocation In arlBabitAllocation
                    Dim dtmFrom As DateTime = New DateTime(obj.Babit.BabitYear, obj.Babit.StartPeriod, 1)
                    Dim dtmTo As DateTime = New DateTime(obj.Babit.BabitYearEnd, obj.Babit.EndPeriod, 2)
                    If (DateTime.Now >= dtmFrom) And (DateTime.Now <= dtmTo) Then
                        oBabitAllocation = New BabitAllocation
                        oBabitAllocation = obj
                        Exit For
                    Else
                        n += 1
                    End If
                Next
                If (n >= arlBabitAllocation.Count) Then
                    MessageBox.Show("Alokasi babit khusus untuk periode ini tidak tersedia")
                    Return
                End If

                lblNoPerjanjian.Text = oBabitAllocation.NoPerjanjian
                lblPeriode.Text = String.Format("{0} - {1}", New DateTime(oBabitAllocation.Babit.BabitYear, oBabitAllocation.Babit.StartPeriod, 1).ToString("dd MMM yyyy"), New DateTime(oBabitAllocation.Babit.BabitYearEnd, oBabitAllocation.Babit.EndPeriod, 1).ToString("dd MMM yyyy"))
                sesHelper.SetSession("BabitAllocation", oBabitAllocation)
                lblNoPerjanjian.Text = oBabitAllocation.NoPerjanjian
                lblSisaAlokasiBabit.Text = Convert.ToInt64(oBabitAllocation.SisaBabit).ToString("#,##0")
            End If

            txtDealerCode.Visible = False
            lblPopUpDealer.Visible = False
            btnFindDealer.Visible = False
            lblDealerCode.Text = oDealer.DealerCode
            lblDealerName.Text = oDealer.DealerName
            lblCity.Text = oDealer.City.CityName
            lblProvince.Text = oDealer.Province.ProvinceName
        Else
            txtDealerCode.Visible = True
            lblPopUpDealer.Visible = True
            btnFindDealer.Visible = True
            lblPopUpDealer.Attributes("onClick") = "ShowPPDealerSelection()"
        End If
    End Sub

    Sub DealerData(ByVal oDealer As Dealer, ByVal oBabitAllocation As BabitAllocation)
        lblDealerName.Text = oDealer.DealerName
        lblCity.Text = oDealer.City.CityName
        lblProvince.Text = oDealer.Province.ProvinceName
        lblNoPerjanjian.Text = oBabitAllocation.NoPerjanjian
        lblPeriode.Text = MonthName(oBabitAllocation.Babit.StartPeriod, False) & " - " & MonthName(oBabitAllocation.Babit.EndPeriod, False) & " " & oBabitAllocation.Babit.BabitYear
        lblSisaAlokasiBabit.Text = Convert.ToInt64(oBabitAllocation.SisaBabit).ToString("#,##0")
        If IsLoginAsDealer() Then
            lblDealerCode.Text = oDealer.DealerCode
            txtDealerCode.Visible = False
            lblPopUpDealer.Visible = False
            btnFindDealer.Visible = False
        Else
            txtDealerCode.Text = oDealer.DealerCode
            txtDealerCode.Visible = True
            lblPopUpDealer.Visible = True
            btnFindDealer.Visible = True
            lblDealerCode.Visible = False
            lblPopUpDealer.Attributes("onClick") = "ShowPPDealerSelection()"
        End If
    End Sub

    Sub BindJenisKegiatan()
        Dim arlTmp As New ArrayList
        Dim arl As New ArrayList
        en = New EnumBabit
        ddlJenisKegiatan.DataTextField = "BabitValue"
        ddlJenisKegiatan.DataValueField = "BabitCode"
        arlTmp = en.BabitProposalTypeList()

        Dim objDealer As Dealer = CType(sesHelper.GetSession("DEALER"), Dealer)
        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            ddlJenisKegiatan.DataSource = arlTmp
            ddlJenisKegiatan.DataBind()
        ElseIf objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            For Each item As BabitItem In arlTmp
                If (CekPengajuanBabitPameranPrivilege()) And (item.BabitValue = EnumBabit.BabitProposalType.Pameran.ToString) Then
                    arl.Add(item)
                ElseIf (CekPengajuanBabitIklanPrivilege()) And (item.BabitValue = EnumBabit.BabitProposalType.Iklan.ToString) Then
                    arl.Add(item)
                ElseIf (CekPengajuanBabitEventPrivilege()) And (item.BabitValue = EnumBabit.BabitProposalType.Even.ToString) Then
                    arl.Add(item)
                End If
            Next
            ddlJenisKegiatan.DataSource = arl
            ddlJenisKegiatan.DataBind()
        End If
        ddlJenisKegiatan.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
    End Sub

    Sub BindMedia(ByVal ddl As DropDownList)
        en = New EnumBabit
        ddl.DataTextField = "BabitValue"
        ddl.DataValueField = "BabitCode"
        ddl.DataSource = en.MediaTypeList()
        ddl.DataBind()
        ddl.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))

    End Sub

    Sub BindProductCategory(ByVal ddl As DropDownList, ByVal ddlVehicle As DropDownList)
        ddl.DataTextField = "Description"
        ddl.DataValueField = "ID"
        ddl.DataSource = New FinishUnit.CategoryFacade(User).RetrieveActiveList()
        ddl.DataBind()
        ddl.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))

        ddl.Attributes.Add("onchange", String.Format("DisplayVehicleType(this, document.all.{0})", ddlVehicle.ClientID))
    End Sub

    Sub BindCarDisplay(ByVal ddl As DropDownList)
        'ddl.DataTextField = "Description"
        'ddl.DataValueField = "ID"
        'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VechileType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

        'ddl.DataSource = New FinishUnit.VechileTypeFacade(User).Retrieve(criterias)
        'ddl.DataBind()
        ddl.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
        ddl.Attributes.Add("onchange", "SaveSelectedVehicle(this)")
    End Sub

    Sub BindGridPameran()
        arlPameran = CType(sesHelper.GetSession("DataPameran"), ArrayList)
        dgPameran.DataSource = arlPameran
        dgPameran.DataBind()
        Dim total As Decimal = 0
        For Each obj As PameranDisplay In arlPameran
            total += obj.Others
        Next
        If (txtBiayaPameran.Text.Trim <> String.Empty) Then
            If IsNumeric(txtBiayaPameran.Text.Trim) Then
                total += Convert.ToDecimal(txtBiayaPameran.Text)
            End If
        End If
        lblTotalBiayaPameran.Text = String.Format("Total Biaya : Rp.{0}", total.ToString("#,##0"))
    End Sub

    Sub calculateTotalBiayaIklan()
        arlIklan = CType(sesHelper.GetSession("DataIklan"), ArrayList)
        Dim total As Decimal = 0
        For Each obj As BPIklan In arlIklan
            total += obj.Expense
        Next
        lblTotalBiayaIklan.Text = String.Format("Total Biaya : Rp.{0}", total.ToString("#,##0"))
    End Sub

    Sub BindGridIklan()
        arlIklan = CType(sesHelper.GetSession("DataIklan"), ArrayList)
        dgIklan.DataSource = arlIklan
        dgIklan.DataBind()
        calculateTotalBiayaIklan()
    End Sub

    Sub BindGridEvent()
        arlEvent = CType(sesHelper.GetSession("DataEvent"), ArrayList)
        dgEvent.DataSource = arlEvent
        dgEvent.DataBind()
        Dim total As Decimal = 0
        For Each obj As EventActivity In arlEvent
            total += obj.Comsumption + obj.Entertainment + obj.Equipment + obj.Place + obj.Others
        Next
        lblTotalBiayaEvent.Text = String.Format("Total Biaya : Rp.{0}", total.ToString("#,##0"))
    End Sub

    Sub RenderTitle(ByVal writer As HtmlTextWriter, ByVal ctl As Control)
        Try
            Dim grd As DataGrid = ctl.Parent.Parent

            writer.AddAttribute("style", "color:#F7F7F7;background-color:#FFCC00;")
            writer.RenderBeginTag("TR")
            writer.AddAttribute("class", "titleTablePromo")
            writer.AddAttribute("rowspan", "2")
            writer.RenderBeginTag("TD")
            writer.Write("No")
            writer.RenderEndTag()
            writer.AddAttribute("class", "titleTablePromo")
            writer.AddAttribute("rowspan", "2")
            writer.RenderBeginTag("TD")
            writer.Write("Media")
            writer.RenderEndTag()
            writer.AddAttribute("class", "titleTablePromo")
            writer.AddAttribute("rowspan", "2")
            writer.RenderBeginTag("TD")
            writer.Write("Nama Media")
            writer.RenderEndTag()
            writer.AddAttribute("class", "titleTablePromo")
            writer.AddAttribute("colspan", "2")
            writer.RenderBeginTag("TD")
            writer.Write("Tanggal Iklan")
            writer.RenderEndTag()
            writer.AddAttribute("class", "titleTablePromo")
            writer.AddAttribute("rowspan", "2")
            writer.RenderBeginTag("TD")
            writer.Write("Biaya Iklan")
            writer.RenderEndTag()
            writer.AddAttribute("class", "titleTablePromo")
            writer.AddAttribute("rowspan", "2")
            writer.RenderBeginTag("TD")
            writer.Write("")
            writer.RenderEndTag()
            writer.RenderEndTag()

            writer.AddAttribute("style", "color:#F7F7F7;background-color:#FFCC00;")
            writer.RenderBeginTag("TR")
            writer.AddAttribute("class", "titleTablePromo")
            writer.RenderBeginTag("TD")
            writer.Write("Mulai")
            writer.RenderEndTag()
            writer.AddAttribute("class", "titleTablePromo")
            writer.RenderBeginTag("TD")
            writer.Write("Selesai")
            writer.RenderEndTag()
            writer.RenderEndTag()

            grd.HeaderStyle.AddAttributesToRender(writer)
        Catch ex As Exception
        End Try
    End Sub

    Private Function ValidateData() As String
        Dim sb As StringBuilder = New StringBuilder

        If Not IsLoginAsDealer() Then
            fillDealerInfoFromKTB()
            If (txtDealerCode.Text = String.Empty) Then
                sb.Append("Kode Dealer Harus Diisi\n")
            End If
        End If

        If (txtNoSuratDealer.Text = String.Empty) Then
            sb.Append("No Surat Dealer Harus Diisi\n")
        End If

        If (ddlJenisKegiatan.SelectedValue = CType(EnumBabit.BabitProposalType.Pameran, Short)) Then
            If (txtTempatPameran.Text.Trim = String.Empty) Then
                sb.Append("Tempat harus diisi\n")
            End If

            If (txtUkuranTempatPameran.Text.Trim = String.Empty) Then
                sb.Append("Ukuran tempat harus diisi\n")
            Else
                If Not IsNumeric(txtUkuranTempatPameran.Text.Trim) Then
                    sb.Append("Ukuran tempat harus angka\n")
                End If
            End If

            If (txtBiayaPameran.Text.Trim = String.Empty) Then
                sb.Append("Biaya harus diisi\n")
            Else
                If Not IsNumeric(txtBiayaPameran.Text.Trim) Then
                    sb.Append("Biaya harus angka\n")
                End If
            End If
            'If (txtJumlahHariPameran.Text.Trim = String.Empty) Then
            '    sb.Append("Jumlah hari harus diisi\n")
            'End If
            'If (txtTempatPameran.Text.Trim = String.Empty) Then
            '    sb.Append("Tempat harus diisi\n")
            'End If
            If (txtTargetPenjualanPameran.Text.Trim = String.Empty) Then
                sb.Append("Target penjualan harus diisi\n")
            Else
                If Not IsNumeric(txtTargetPenjualanPameran.Text.Trim) Then
                    sb.Append("Target penjualan harus angka\n")
                End If
            End If
            If (icDatePameranMulai.Value > icDatePameranAkhir.Value) Then
                sb.Append("Tanggal awal harus lebih kecil atau sama dengan tanggal akhir\n")
            End If

            If (IsLoginAsDealer()) Then
                If (DateDiff(DateInterval.Day, DateTime.Now, icDatePameranMulai.Value) < 7) Then
                    sb.Append("Pengajuan Pameran Harus Diajukan Minimal 7 Hari Sebelum Tanggal Pameran Dimulai\n")
                End If
            End If

            If (CType(sesHelper.GetSession("DataPameran"), ArrayList).Count = 0) Then
                sb.Append("DataGrid pameran harus diisi\n")
            End If

            If (txtBiayaPameran.Text.Trim <> String.Empty) Then
                Dim strSisaAlokasiBabit As String = lblSisaAlokasiBabit.Text.Trim()
                If strSisaAlokasiBabit.Length <= 0 Then
                    strSisaAlokasiBabit = "0"
                End If
                If Convert.ToDecimal(txtBiayaPameran.Text) < 0 Then
                    sb.Append("Biaya yang diajukan tidak  boleh negatif \n")
                End If
                If (Convert.ToDecimal(strSisaAlokasiBabit) < Convert.ToDecimal(txtBiayaPameran.Text)) Then
                    sb.Append("Biaya yang diajukan melebihi sisa anggaran BABIT anda\n")
                End If
            End If
        ElseIf (ddlJenisKegiatan.SelectedValue = CType(EnumBabit.BabitProposalType.Even, Short)) Then
            If (txtUkuranTempatEvent.Text.Trim = String.Empty) Then
                sb.Append("Ukuran tempat harus diisi\n")
            Else
                If Not IsNumeric(txtUkuranTempatEvent.Text.Trim) Then
                    sb.Append("Ukuran tempat harus angka\n")
                End If
            End If
            If (txtJumlahHariEvent.Text.Trim = String.Empty) Then
                sb.Append("Jumlah hari harus diisi\n")
            Else
                If Not IsNumeric(txtJumlahHariEvent.Text.Trim) Then
                    sb.Append("Jumlah hari harus angka\n")
                End If
            End If
            If (txtTempatEvent.Text.Trim = String.Empty) Then
                sb.Append("Tempat harus diisi\n")
            End If
            If (txtTargetPenjualanEvent.Text.Trim = String.Empty) Then
                sb.Append("Target penjualan harus diisi\n")
            Else
                If Not IsNumeric(txtTargetPenjualanEvent.Text.Trim) Then
                    sb.Append("Target penjualan harus angka\n")
                End If
            End If
            If (icDateEventMulai.Value > icDateEventAkhir.Value) Then
                sb.Append("Tanggal awal harus lebih kecil atau sama dengan tanggal akhir\n")
            End If

            If (IsLoginAsDealer()) Then
                If (DateDiff(DateInterval.Day, DateTime.Now, icDateEventMulai.Value) < 7) Then
                    sb.Append("Pengajuan Even Harus Diajukan Minimal 7 Hari Sebelum Tanggal Even Dimulai\n")
                End If
            End If

            'If (CType(sesHelper.GetSession("DataEvent"), ArrayList).Count = 0) Then
            '    sb.Append("DataGrid event harus diisi\n")
            'End If

            If IsLoginAsDealer() Then
                Dim EventCost As Decimal = 0
                For Each item As DataGridItem In dgEvent.Items
                    Dim lblConsumsion As Label = CType(item.FindControl("lblConsumsion"), Label)
                    Dim lblEntertainment As Label = CType(item.FindControl("lblEntertainment"), Label)
                    Dim lblEquipment As Label = CType(item.FindControl("lblEquipment"), Label)
                    EventCost += Convert.ToDecimal(lblConsumsion.Text) + Convert.ToDecimal(lblEntertainment.Text) + Convert.ToDecimal(lblEquipment.Text)
                Next
                Dim strSisaAlokasiBabit As String = lblSisaAlokasiBabit.Text.Trim()
                If strSisaAlokasiBabit.Length <= 0 Then
                    strSisaAlokasiBabit = "0"
                End If
                If (Convert.ToDecimal(strSisaAlokasiBabit) < EventCost) Then
                    sb.Append("Pengajuan melebihi batas sisa alokasi\n")
                End If
                EventCost = 0
            End If

        ElseIf (ddlJenisKegiatan.SelectedValue = CType(EnumBabit.BabitProposalType.Iklan, Short)) Then
            If (CType(sesHelper.GetSession("DataIklan"), ArrayList).Count = 0) Then
                sb.Append("DataGrid iklan harus diisi\n")
            End If
            Dim IklanCost As Decimal = 0
            For Each item As DataGridItem In dgIklan.Items
                Dim lblCost As Label = CType(item.FindControl("lblCost"), Label)
                IklanCost += Convert.ToDecimal(lblCost.Text)
            Next
            Dim strSisaAlokasiBabit As String = lblSisaAlokasiBabit.Text.Trim()
            If strSisaAlokasiBabit.Length <= 0 Then
                strSisaAlokasiBabit = "0"
            End If
            If (Convert.ToDecimal(strSisaAlokasiBabit) < IklanCost) Then
                sb.Append("Pengajuan melebihi batas sisa alokasi\n")
            End If
            IklanCost = 0
        Else
            sb.Append("Pilih jenis kegiatan\n")
        End If

        ' add security
        'If (CekPengajuanBabitPameranPrivilege()) Or (CekPengajuanBabitEventPrivilege()) Or (CekPengajuanBabitIklanPrivilege()) Then


        'Dim _filename As String = System.IO.Path.GetFileName(UploadFile.PostedFile.FileName)
        'If _filename.Trim().Length <= 0 Then
        '    sb.Append("Upload file belum diisi\n")
        'End If

        'If UploadFile.PostedFile.ContentLength > CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize")) Then
        '    sb.Append("Ukuran file tidak boleh melebihi " & CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize")) & "kb\n")
        'End If
        'End If

        Return sb.ToString()
    End Function

    Private Sub SaveFileToBabit(ByVal oBabitProposal As BabitProposal)
        Dim _filename As String = System.IO.Path.GetFileName(CType(sesHelper.GetSession(FU_NAME), String))
        If Not IsNothing(_filename) Then
            If _filename.Trim().Length > 0 Then
                oBabitProposal.FileName = _filename
                lblFileUpload.Text = _filename
                SaveFile(_filename)
            End If
        End If
    End Sub

    Private Function SaveBabitProposal(ByRef oBabitProposal As BabitProposal) As Integer
        oBabitProposal.ActivityType = ddlJenisKegiatan.SelectedValue
        oBabitProposal.BabitAllocation = CType(sesHelper.GetSession("BabitAllocation"), BabitAllocation)
        If (IsLoginAsDealer()) Then
            oBabitProposal.Dealer = objDealer
        Else
            Dim objDealerTmp As Dealer = New KTB.DNet.BusinessFacade.General.DealerFacade(User).Retrieve(txtDealerCode.Text.Trim())
            oBabitProposal.Dealer = objDealerTmp
        End If

        oBabitProposal.Status = CType(EnumBabit.StatusBabitProposal.Baru, Short)
        oBabitProposal.NoSuratDealer = txtNoSuratDealer.Text
        If (ddlJenisKegiatan.SelectedValue = CType(EnumBabit.BabitProposalType.Pameran, Short)) Then
            Dim oBPPameran As New BPPameran
            oBPPameran.EndExhibitionDate = icDatePameranAkhir.Value
            oBPPameran.ExhibitionSize = txtUkuranTempatPameran.Text
            oBPPameran.Expense = txtBiayaPameran.Text
            oBPPameran.Place = txtTempatPameran.Text
            oBPPameran.SalesTarget = txtTargetPenjualanPameran.Text
            oBPPameran.StartExhibitionDate = icDatePameranMulai.Value
            oBPPameran.NumberOfDay = DateDiff(DateInterval.Day, oBPPameran.StartExhibitionDate, oBPPameran.EndExhibitionDate) + 1
            txtJumlahHariPameran.Text = oBPPameran.NumberOfDay.ToString
            SaveFileToBabit(oBabitProposal)

            If (New BabitSalesComm.BabitProposalFacade(User).InsertBabitPrososalPameran(oBabitProposal, oBPPameran, CType(sesHelper.GetSession("DataPameran"), ArrayList)) = 1) Then
                Return 1
            Else
                Return 0
            End If
        ElseIf (ddlJenisKegiatan.SelectedValue = CType(EnumBabit.BabitProposalType.Even, Short)) Then
            Dim oBPEvent As New BPEvent
            oBPEvent.EndEventDate = icDateEventAkhir.Value
            oBPEvent.EventSize = txtUkuranTempatEvent.Text
            oBPEvent.Place = txtTempatEvent.Text
            oBPEvent.SalesTarget = txtTargetPenjualanEvent.Text
            oBPEvent.StartEventDate = icDateEventMulai.Value
            oBPEvent.NumberOfDay = DateDiff(DateInterval.Day, oBPEvent.StartEventDate, oBPEvent.EndEventDate) + 1
            txtJumlahHariEvent.Text = oBPEvent.NumberOfDay.ToString
            SaveFileToBabit(oBabitProposal)

            If (New BabitSalesComm.BabitProposalFacade(User).InsertBabitPrososalEvent(oBabitProposal, oBPEvent, CType(sesHelper.GetSession("DataEvent"), ArrayList)) = 1) Then
                Return 1
            Else
                Return 0
            End If

        ElseIf (ddlJenisKegiatan.SelectedValue = CType(EnumBabit.BabitProposalType.Iklan, Short)) Then
            SaveFileToBabit(oBabitProposal)
            If (New BabitSalesComm.BabitProposalFacade(User).InsertBabitPrososalIklan(oBabitProposal, CType(sesHelper.GetSession("DataIklan"), ArrayList)) = 1) Then
                Return 1
            Else
                Return 0
            End If
        End If
    End Function

    Sub ViewData(ByVal id As Integer)
        pnlScript.Visible = False
        If Request.QueryString("Src") <> String.Empty And Request.QueryString("Src") = "PopUpPengajuan" Then
            btnBack.Visible = False
            btnCancel.Visible = True
        Else
            btnBack.Visible = True
            btnCancel.Visible = False
        End If
        ddlJenisKegiatan.Enabled = False
        Dim oBabitProposal As New BabitProposal
        oBabitProposal = New BabitSalesComm.BabitProposalFacade(User).Retrieve(id)
        'TODO Session
        'Session("BabitProposal") = oBabitProposal
        sesHelper.SetSession("BabitProposal", oBabitProposal)
        lblNoPengajuan.Text = oBabitProposal.NoPengajuan
        txtNoSuratDealer.Text = oBabitProposal.NoSuratDealer
        If oBabitProposal.ActivityType >= 0 Then
            ddlJenisKegiatan.SelectedValue = oBabitProposal.ActivityType
        End If

        If Not IsLoginAsDealer() Then
            Dim ktbid As Integer = 0
            'cek if the proposal CreatedBy ktb user, so it can be edit by ktb
            Try
                ktbid = CInt(oBabitProposal.CreatedBy.Substring(0, 6))
                If (ktbid = objDealer.ID) Then
                    btnSubmit.Enabled = True
                Else
                    btnSubmit.Enabled = False
                End If
                If (oBabitProposal.Status <> EnumBabit.StatusBabitProposal.Baru) Then
                    btnSubmit.Enabled = False
                End If
            Catch ex As Exception
                btnSubmit.Enabled = False
            End Try
        ElseIf (oBabitProposal.Status = EnumBabit.StatusBabitProposal.Baru) Then
            btnSubmit.Enabled = True
        Else
            btnSubmit.Enabled = False
        End If

        If (oBabitProposal.Status = EnumBabit.StatusBabitProposal.Baru) Then
            btnSave.Enabled = True
        ElseIf (oBabitProposal.Status = EnumBabit.StatusBabitProposal.Validasi) Then
            btnSave.Enabled = True
        Else
            btnSave.Enabled = False
        End If

        DealerData(oBabitProposal.Dealer, oBabitProposal.BabitAllocation)
        lblFileUpload.Text = oBabitProposal.FileName

        If (oBabitProposal.ActivityType = CType(EnumBabit.BabitProposalType.Pameran, Short)) Then
            divPameran.Style.Item("display") = "block"
            divPameranGrid.Style.Item("display") = "block"
            divEvent.Style.Item("display") = "none"
            divIklan.Style.Item("display") = "none"
            lblJenisKegiatan.Text = EnumBabit.BabitProposalType.Pameran.ToString()
            icDatePameranAkhir.Value = oBabitProposal.BPPameran.EndExhibitionDate
            icDatePameranMulai.Value = oBabitProposal.BPPameran.StartExhibitionDate
            txtTempatPameran.Text = oBabitProposal.BPPameran.Place
            txtBiayaPameran.Text = oBabitProposal.BPPameran.Expense.ToString("#,##0")
            txtJumlahHariPameran.Text = oBabitProposal.BPPameran.NumberOfDay.ToString("#,##0")
            txtTargetPenjualanPameran.Text = oBabitProposal.BPPameran.SalesTarget.ToString("#,##0")
            txtUkuranTempatPameran.Text = oBabitProposal.BPPameran.ExhibitionSize

            lblTglPameranAwal.Text = oBabitProposal.BPPameran.StartExhibitionDate.ToString("dd/MM/yyyy")
            lblTglPameranAkhir.Text = oBabitProposal.BPPameran.EndExhibitionDate.ToString("dd/MM/yyyy")
            lblUkuranTempatPameran.Text = oBabitProposal.BPPameran.ExhibitionSize
            lblJumlahHariPameran.Text = oBabitProposal.BPPameran.NumberOfDay.ToString("#,##0")
            lblTargetPejualanPameran.Text = oBabitProposal.BPPameran.SalesTarget.ToString("#,##0")
            lblBiayaPameran.Text = oBabitProposal.BPPameran.Expense.ToString("#,##0")
            lblTempatPameran.Text = oBabitProposal.BPPameran.Place

            'Todo session
            'Session("DataPameran") = oBabitProposal.BPPameran.PameranDisplays
            sesHelper.SetSession("BabitProposalPameran", oBabitProposal)
            sesHelper.SetSession("DataPameran", oBabitProposal.BPPameran.PameranDisplays)
            BindGridPameran()
        ElseIf (oBabitProposal.ActivityType = CType(EnumBabit.BabitProposalType.Even, Short)) Then
            divPameran.Style.Item("display") = "none"
            divPameranGrid.Style.Item("display") = "none"
            divEvent.Style.Item("display") = "block"
            divEventGrid.Style.Item("display") = "block"
            divIklan.Style.Item("display") = "none"
            lblJenisKegiatan.Text = "Event" 'EnumBabit.BabitProposalType.Even.ToString()
            icDateEventAkhir.Value = oBabitProposal.BPEvent.EndEventDate
            txtUkuranTempatEvent.Text = oBabitProposal.BPEvent.EventSize
            txtJumlahHariEvent.Text = oBabitProposal.BPEvent.NumberOfDay.ToString("#,##0")
            txtTempatEvent.Text = oBabitProposal.BPEvent.Place
            txtTargetPenjualanEvent.Text = oBabitProposal.BPEvent.SalesTarget.ToString("#,##0")
            icDateEventMulai.Value = oBabitProposal.BPEvent.StartEventDate

            lblTempatEvent.Text = oBabitProposal.BPEvent.Place
            lblTglEventAwal.Text = oBabitProposal.BPEvent.StartEventDate.ToString("dd/MM/yyyy")
            lblTglEventAkhir.Text = oBabitProposal.BPEvent.EndEventDate.ToString("dd/MM/yyyy")
            lblUkuranTempatEvent.Text = oBabitProposal.BPEvent.EventSize
            lblJumlahHariEvent.Text = oBabitProposal.BPEvent.NumberOfDay.ToString("#,##0")
            lblTargetPenjualanEvent.Text = oBabitProposal.BPEvent.SalesTarget.ToString("#,##0")

            'Todo session
            'Session("DataEvent") = oBabitProposal.BPEvent.EventActivitys
            sesHelper.SetSession("BabitProposalEvent", oBabitProposal)
            sesHelper.SetSession("DataEvent", oBabitProposal.BPEvent.EventActivitys)
            BindGridEvent()
        ElseIf (oBabitProposal.ActivityType = CType(EnumBabit.BabitProposalType.Iklan, Short)) Then
            lblJenisKegiatan.Text = EnumBabit.BabitProposalType.Iklan.ToString()
            divPameran.Style.Item("display") = "none"
            divPameranGrid.Style.Item("display") = "none"
            divEvent.Style.Item("display") = "none"
            divIklan.Style.Item("display") = "block"

            Dim arl As New ArrayList
            arl = New BabitSalesComm.BabitProposalFacade(User).RetrieveIklanByBabitProposalID(oBabitProposal.ID)
            'Todo session
            'Session("DataIklan") = arl
            sesHelper.SetSession("BabitProposalIklan", oBabitProposal)
            sesHelper.SetSession("DataIklan", arl)
            BindGridIklan()
        End If
        btnSave.Text = "Ubah"
    End Sub

    Sub Mode(ByVal isView As Boolean)
        If (isView) Then
            dgPameran.ShowFooter = False
            dgEvent.ShowFooter = False
            dgIklan.ShowFooter = False
            dgPameran.Columns(4).Visible = False
            dgEvent.Columns(5).Visible = False
            dgIklan.Columns(8).Visible = False
            UploadFile.Disabled = True
        End If
        icDatePameranAkhir.Visible = Not isView
        icDatePameranMulai.Visible = Not isView
        txtTempatPameran.Visible = Not isView
        txtBiayaPameran.Visible = Not isView
        txtJumlahHariPameran.Visible = Not isView
        txtUkuranTempatPameran.Visible = Not isView
        txtTargetPenjualanPameran.Visible = Not isView
        icDateEventAkhir.Visible = Not isView
        txtUkuranTempatEvent.Visible = Not isView
        txtJumlahHariEvent.Visible = Not isView
        txtTempatEvent.Visible = Not isView
        txtTargetPenjualanEvent.Visible = Not isView
        icDateEventMulai.Visible = Not isView
        txtNoSuratDealer.Enabled = Not isView

        ddlJenisKegiatan.Visible = Not isView

        lblJenisKegiatan.Visible = isView
        lblTglPameranAwal.Visible = isView
        lblTglPameranAkhir.Visible = isView
        lblUkuranTempatPameran.Visible = isView
        lblJumlahHariPameran.Visible = isView
        lblTargetPejualanPameran.Visible = isView
        lblBiayaPameran.Visible = isView
        lblTempatPameran.Visible = isView
        lblTempatEvent.Visible = isView
        lblTglEventAwal.Visible = isView
        lblTglEventAkhir.Visible = isView
        lblUkuranTempatEvent.Visible = isView
        lblJumlahHariEvent.Visible = isView
        lblTargetPenjualanEvent.Visible = isView
        btnSave.Visible = Not isView
    End Sub

    Private Function IsExistInOldList(ByVal arlList As ArrayList, ByVal ID As Integer)
        For Each oPD As PameranDisplay In arlList
            If (oPD.ID = ID) Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Function IsExistInOldListEvent(ByVal arlList As ArrayList, ByVal ID As Integer)
        For Each oEA As EventActivity In arlList
            If (oEA.ID = ID) Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Function IsExistInOldListIklan(ByVal arlList As ArrayList, ByVal ID As Integer)
        For Each oIklan As BPIklan In arlList
            If (oIklan.ID = ID) Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Function UpdateBabitProposal(ByRef oBabitProposal As BabitProposal) As Integer
        If (ddlJenisKegiatan.SelectedValue = CType(EnumBabit.BabitProposalType.Pameran, Short)) Then
            oBabitProposal.BPPameran.EndExhibitionDate = icDatePameranAkhir.Value
            oBabitProposal.BPPameran.ExhibitionSize = txtTempatPameran.Text
            oBabitProposal.BPPameran.Expense = txtBiayaPameran.Text
            oBabitProposal.BPPameran.Place = txtTempatPameran.Text
            oBabitProposal.BPPameran.SalesTarget = txtTargetPenjualanPameran.Text
            oBabitProposal.BPPameran.StartExhibitionDate = icDatePameranMulai.Value
            oBabitProposal.BPPameran.NumberOfDay = DateDiff(DateInterval.Day, oBabitProposal.BPPameran.StartExhibitionDate, oBabitProposal.BPPameran.EndExhibitionDate) + 1
            txtJumlahHariPameran.Text = oBabitProposal.BPPameran.NumberOfDay.ToString

            SaveFileToBabit(oBabitProposal)

            Dim OldPameranDisplay As New ArrayList
            OldPameranDisplay = New BabitSalesComm.BabitProposalFacade(User).RetrievePameranDisplayByPameranID(oBabitProposal.BPPameran.ID)

            Dim arlNewPameran As ArrayList
            Dim arlUpdatePameran As ArrayList
            Dim arlDeletePameran As ArrayList

            If (OldPameranDisplay.Count > 0) Then
                arlNewPameran = New ArrayList
                arlUpdatePameran = New ArrayList
                arlDeletePameran = New ArrayList

                For Each pd As PameranDisplay In CType(sesHelper.GetSession("DataPameran"), ArrayList)
                    If (pd.ID <> 0) Then
                        If (IsExistInOldList(OldPameranDisplay, pd.ID)) Then
                            arlUpdatePameran.Add(pd)
                        End If
                    Else
                        arlNewPameran.Add(pd)
                    End If
                Next

                For Each pd As PameranDisplay In OldPameranDisplay
                    If (Not IsExistInOldList(CType(sesHelper.GetSession("DataPameran"), ArrayList), pd.ID)) Then
                        arlDeletePameran.Add(pd)
                    End If
                Next
            Else
                For Each pd As PameranDisplay In CType(sesHelper.GetSession("DataPameran"), ArrayList)
                    arlNewPameran.Add(pd)
                Next
            End If

            If (New BabitSalesComm.BabitProposalFacade(User).UpdateBabitPrososalPameran(oBabitProposal, arlNewPameran, arlUpdatePameran, arlDeletePameran) = 1) Then
                Return 1
            Else
                Return 0
            End If
        ElseIf (ddlJenisKegiatan.SelectedValue = CType(EnumBabit.BabitProposalType.Even, Short)) Then
            oBabitProposal.BPEvent.EndEventDate = icDateEventAkhir.Value
            oBabitProposal.BPEvent.EventSize = txtUkuranTempatEvent.Text
            oBabitProposal.BPEvent.Place = txtTempatEvent.Text
            oBabitProposal.BPEvent.SalesTarget = txtTargetPenjualanEvent.Text
            oBabitProposal.BPEvent.StartEventDate = icDateEventMulai.Value
            oBabitProposal.BPEvent.NumberOfDay = DateDiff(DateInterval.Day, oBabitProposal.BPEvent.StartEventDate, oBabitProposal.BPEvent.EndEventDate) + 1
            txtJumlahHariEvent.Text = oBabitProposal.BPEvent.NumberOfDay.ToString()
            SaveFileToBabit(oBabitProposal)

            Dim OldEventActivity As New ArrayList
            OldEventActivity = New BabitSalesComm.BabitProposalFacade(User).RetrieveEventActivityByEventID(oBabitProposal.BPEvent.ID)

            Dim arlNewEventAct As ArrayList
            Dim arlUpdateEventAct As ArrayList
            Dim arlDeleteEventAct As ArrayList

            If (OldEventActivity.Count > 0) Then
                arlNewEventAct = New ArrayList
                arlUpdateEventAct = New ArrayList
                arlDeleteEventAct = New ArrayList
                For Each ea As EventActivity In CType(sesHelper.GetSession("DataEvent"), ArrayList)
                    If (ea.ID <> 0) Then
                        If (IsExistInOldListEvent(OldEventActivity, ea.ID)) Then
                            arlUpdateEventAct.Add(ea)
                        End If
                    Else
                        arlNewEventAct.Add(ea)
                    End If
                Next

                For Each ea As EventActivity In OldEventActivity
                    If (Not IsExistInOldListEvent(CType(sesHelper.GetSession("DataEvent"), ArrayList), ea.ID)) Then
                        arlDeleteEventAct.Add(ea)
                    End If
                Next
            Else
                arlNewEventAct = New ArrayList
                For Each ea As EventActivity In CType(sesHelper.GetSession("DataEvent"), ArrayList)
                    arlNewEventAct.Add(ea)
                Next
            End If

            If (New BabitSalesComm.BabitProposalFacade(User).UpdateBabitPrososalEvent(oBabitProposal, arlNewEventAct, arlUpdateEventAct, arlDeleteEventAct) = 1) Then
                Return 1
            Else
                Return 0
            End If

        ElseIf (ddlJenisKegiatan.SelectedValue = CType(EnumBabit.BabitProposalType.Iklan, Short)) Then
            Dim OldIklan As New ArrayList
            OldIklan = New BabitSalesComm.BabitProposalFacade(User).RetrieveIklanByBabitProposalID(oBabitProposal.ID)

            Dim arlNewIklan As ArrayList
            Dim arlUpdateIklan As ArrayList
            Dim arlDeleteIklan As ArrayList

            If (OldIklan.Count > 0) Then
                arlNewIklan = New ArrayList
                arlUpdateIklan = New ArrayList
                arlDeleteIklan = New ArrayList
                For Each iklan As BPIklan In CType(sesHelper.GetSession("DataIklan"), ArrayList)
                    If (iklan.ID <> 0) Then
                        If (IsExistInOldListIklan(OldIklan, iklan.ID)) Then
                            arlUpdateIklan.Add(iklan)
                        End If
                    Else
                        arlNewIklan.Add(iklan)
                    End If
                Next

                For Each iklan As BPIklan In OldIklan
                    If (Not IsExistInOldListIklan(CType(sesHelper.GetSession("DataIklan"), ArrayList), iklan.ID)) Then
                        arlDeleteIklan.Add(iklan)
                    End If
                Next
            Else
                arlNewIklan = New ArrayList
                For Each iklan As BPIklan In CType(sesHelper.GetSession("DataIklan"), ArrayList)
                    arlNewIklan.Add(iklan)
                Next
            End If
            SaveFileToBabit(oBabitProposal)

            If (New BabitSalesComm.BabitProposalFacade(User).UpdateBabitPrososalIklan(oBabitProposal, arlNewIklan, arlUpdateIklan, arlDeleteIklan) = 1) Then
                Return 1
            Else
                Return 0
            End If

        End If
    End Function

#End Region

#Region "Cek Privilege"
    Private Sub InitiateAuthorization()
        If objDealer.Title = EnumDealerTittle.DealerTittle.KTB Then
            If Not SecurityProvider.Authorize(context.User, SR.BabitApprovalView_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Pengajuan Babit")
            End If
        Else
            ' case from dealer
            If Not SecurityProvider.Authorize(context.User, SR.PengajuanBabitView_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Pengajuan Babit")
            End If
        End If

    End Sub

    Private Function CekPengajuanBabitPameranPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanBabitPameran_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CekPengajuanBabitIklanPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanBabitIklan_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Function CekPengajuanBabitEventPrivilege() As Boolean
        If Not SecurityProvider.Authorize(context.User, SR.PengajuanBabitEvent_Privilege) Then
            Return False
        Else
            Return True
        End If
    End Function

#End Region

#Region "Event Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        objDealer = CType(sesHelper.GetSession("DEALER"), Dealer)
        InitiateAuthorization()
        GenerateJS()

        icDateEventMulai.ScriptOnFocusOut = String.Format("updateJumlahHari(this, document.all.{0}, true)", txtJumlahHariEvent.ClientID)
        icDateEventAkhir.ScriptOnFocusOut = String.Format("updateJumlahHari(this, document.all.{0}, false)", txtJumlahHariEvent.ClientID)

        icDatePameranMulai.ScriptOnFocusOut = String.Format("updateJumlahHari(this, document.all.{0}, true)", txtJumlahHariPameran.ClientID)
        icDatePameranAkhir.ScriptOnFocusOut = String.Format("updateJumlahHari(this, document.all.{0}, false)", txtJumlahHariPameran.ClientID)

        If (Not IsPostBack) Then
            btnBaru.Visible = False
            BindJenisKegiatan()
            Session("BabitAllocation") = Nothing
            Session("DataPameran") = Nothing
            Session("DataIklan") = Nothing
            Session("DataEvent") = Nothing
            Session("BabitProposalPameran") = Nothing
            Session("BabitProposalEvent") = Nothing
            Session("BabitProposalIklan") = Nothing

            If (Request.QueryString("Mode") = "Edit") Then
                ViewData(CType((Request.QueryString("id")), Integer))
                Mode(False)
            ElseIf (Request.QueryString("Mode") = "View") Then
                ViewData(CType((Request.QueryString("id")), Integer))
                Mode(True)
            Else
                If (Request.QueryString("Mode") = "NewFromAlloc") Then
                    ' Tambahan dari Hendra. 
                    ' Start 
                    Dim objAlloc As BabitAllocation = New BabitSalesComm.BabitFacade(User).RetrieveBabitAllocation(CInt(Request.QueryString("idAlloc")))
                    DealerData(objAlloc.Dealer, objAlloc)
                    'Todo session
                    'Session("BabitAllocation") = objAlloc
                    sesHelper.SetSession("BabitAllocation", objAlloc)
                    btnBack.Visible = False
                    btnCancel.Visible = False
                    ' End 
                Else
                    DealerData(objDealer)
                    btnBack.Visible = False
                    btnCancel.Visible = False
                End If
                btnBaru.Visible = True
                If IsNothing(sesHelper.GetSession("DataPameran")) Then
                    'Todo session
                    'Session("DataPameran") = arlPameran
                    sesHelper.SetSession("DataPameran", arlPameran)
                End If
                If IsNothing(sesHelper.GetSession("DataIklan")) Then
                    'Todo session
                    'Session("DataIklan") = arlIklan
                    sesHelper.SetSession("DataIklan", arlIklan)
                End If
                If IsNothing(Session("DataEvent")) Then
                    'Todo session
                    'Session("DataEvent") = arlEvent
                    sesHelper.SetSession("DataEvent", arlEvent)
                End If
                BindGridPameran()
                BindGridIklan()
                BindGridEvent()
                ShowNoOfDays()
            End If

            '' add security
            'Dim blnPengajuanBabitPameran As Boolean
            'Dim blnPengajuanBabitEvent As Boolean
            'Dim blnPengajuanBabitIklan As Boolean

            'blnPengajuanBabitPameran = CekPengajuanBabitPameranPrivilege()
            'blnPengajuanBabitEvent = CekPengajuanBabitEventPrivilege()
            'blnPengajuanBabitIklan = CekPengajuanBabitIklanPrivilege()

            'If (blnPengajuanBabitPameran) Or (blnPengajuanBabitEvent) Or (blnPengajuanBabitIklan) Then
            '    SetCompPrivilage(True)
            'Else
            '    SetCompPrivilage(False)
            'End If
        Else
            If Request.Form("hdnValNew") = "1" Then
                btnSave_Click(Nothing, Nothing)
                hdnValNew.Value = "-1"
                sesHelper.RemoveSession(FU)
                sesHelper.RemoveSession(FU_NAME)
            ElseIf Request.Form("hdnValNew") = "0" Then
                hdnValNew.Value = "-1"
                sesHelper.RemoveSession(FU)
                sesHelper.RemoveSession(FU_NAME)
            End If

            If Request.Form("hdnValSubmit") = "1" Then
                btnSubmit_Click(Nothing, Nothing)
                hdnValSubmit.Value = "-1"
            ElseIf Request.Form("hdnValSubmit") = "0" Then
                hdnValSubmit.Value = "-1"
            End If
        End If
    End Sub

    Private Sub dgPameran_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgPameran.ItemDataBound
        If e.Item.ItemType = ListItemType.Footer Then
            Dim ddlProductCategory As DropDownList = CType(e.Item.FindControl("ddlFProductCategory"), DropDownList)
            Dim ddlCarDisplay As DropDownList = CType(e.Item.FindControl("ddlFCarDisplay"), DropDownList)
            BindProductCategory(ddlProductCategory, ddlCarDisplay)
            BindCarDisplay(ddlCarDisplay)
        End If

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", _
                    New CommonFunction().PreventDoubleClickAtGrid(CType(e.Item.FindControl("lbtnDelete"), LinkButton), "Yakin Data ini akan dihapus?"))
            End If
        End If

        If e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            Dim oPameran As PameranDisplay = CType(e.Item.DataItem, PameranDisplay)
            Dim ddlProductCategory As DropDownList = CType(e.Item.FindControl("ddlEProductCategory"), DropDownList)
            Dim ddlCarDisplay As DropDownList = CType(e.Item.FindControl("ddlECarDisplay"), DropDownList)
            BindProductCategory(ddlProductCategory, ddlCarDisplay)
            BindCarDisplay(ddlCarDisplay)

            Dim strScript As String = String.Empty
            strScript += "<script language=""javascript"">"
            strScript += String.Format("DisplayVehicleType(document.getElementById('{0}'), document.getElementById('{1}'));", ddlProductCategory.ClientID, ddlCarDisplay.ClientID)
            'strScript += String.Format("RestoreSelectedVehicle(document.all.{0})", ddlCarDisplay.ClientID)
            strScript += String.Format("RestoreSelectedVehicleByValue(document.getElementById('{0}'), '{1}')", ddlCarDisplay.ClientID, oPameran.VechileType.ID)

            strScript += "</script>"
            phBottomScript.Controls.Add(New LiteralControl(strScript))

            ddlProductCategory.SelectedValue = oPameran.Category.ID
            'ddlCarDisplay.SelectedValue = oPameran.VechileType.ID
        End If
    End Sub

    Private Sub dgPameran_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgPameran.ItemCommand
        arlPameran = CType(Session("DataPameran"), ArrayList)
        Select Case e.CommandName
            Case "add"
                Dim oCategory As New Category
                Dim oVechileType As New VechileType

                Dim ddlProductCategory As DropDownList = CType(e.Item.FindControl("ddlFProductCategory"), DropDownList)
                Dim ddlCarDisplay As DropDownList = CType(e.Item.FindControl("ddlFCarDisplay"), DropDownList)
                Dim txtOthers As TextBox = CType(e.Item.FindControl("txtFOthers"), TextBox)

                Dim strScript As String = String.Empty
                strScript += "<script language=""javascript"">"
                strScript += String.Format("DisplayVehicleType(document.all.{0}, document.all.{1});", ddlProductCategory.ClientID, ddlCarDisplay.ClientID)
                strScript += "</script>"

                If ddlProductCategory.SelectedValue = "-1" Then
                    MessageBox.Show("Produk Kategori harus diisi.")
                    phBottomScript.Controls.Add(New LiteralControl(strScript))
                    Return
                End If

                If hdnSelectedVehicleType.Value = "-1" Then
                    MessageBox.Show("Kendaraan display harus diisi.")
                    phBottomScript.Controls.Add(New LiteralControl(strScript))
                    Return
                End If

                oCategory = New FinishUnit.CategoryFacade(User).Retrieve(CInt(ddlProductCategory.SelectedValue))
                oVechileType = New FinishUnit.VechileTypeFacade(User).Retrieve(CInt(hdnSelectedVehicleType.Value))

                Dim oPameranDisplay As New PameranDisplay
                oPameranDisplay.Category = oCategory
                oPameranDisplay.VechileType = oVechileType
                If (txtOthers.Text = String.Empty) Then
                    oPameranDisplay.Others = 0
                Else
                    If Not IsNumeric(txtOthers.Text.Trim) Then
                        MessageBox.Show("Biaya lain harus angka")
                        Return
                    Else
                        If (Convert.ToDecimal(txtOthers.Text) < 0) Then
                            MessageBox.Show("Biaya lain tidak boleh minus")
                            Return
                        Else
                            oPameranDisplay.Others = Convert.ToDecimal(txtOthers.Text)
                        End If
                    End If
                End If

                arlPameran.Add(oPameranDisplay)
                ShowNoOfDays()
            Case "save" 'Update this datagrid item   
                Dim oCategory As New Category
                Dim oVechileType As New VechileType

                Dim ddlProductCategory As DropDownList = CType(e.Item.FindControl("ddlEProductCategory"), DropDownList)
                Dim ddlCarDisplay As DropDownList = CType(e.Item.FindControl("ddlECarDisplay"), DropDownList)
                Dim txtOthers As TextBox = CType(e.Item.FindControl("txtEOthers"), TextBox)

                Dim strScript As String = String.Empty
                strScript += "<script language=""javascript"">"
                strScript += String.Format("DisplayVehicleType(document.all.{0}, document.all.{1});", ddlProductCategory.ClientID, ddlCarDisplay.ClientID)
                strScript += "</script>"

                If ddlProductCategory.SelectedValue = "-1" Then
                    MessageBox.Show("Produk Kategori harus diisi.")
                    phBottomScript.Controls.Add(New LiteralControl(strScript))
                    Return
                End If

                If hdnSelectedVehicleType.Value = "-1" Then
                    MessageBox.Show("Kendaraan display harus diisi.")
                    phBottomScript.Controls.Add(New LiteralControl(strScript))
                    Return
                End If

                oCategory = New FinishUnit.CategoryFacade(User).Retrieve(CInt(ddlProductCategory.SelectedValue))
                oVechileType = New FinishUnit.VechileTypeFacade(User).Retrieve(CInt(hdnSelectedVehicleType.Value))

                Dim oPameranDisplay As PameranDisplay = CType(arlPameran(e.Item.ItemIndex), PameranDisplay)
                oPameranDisplay.Category = oCategory
                oPameranDisplay.VechileType = oVechileType
                oPameranDisplay.Others = Convert.ToDecimal(txtOthers.Text)

                dgPameran.EditItemIndex = -1
                dgPameran.ShowFooter = True

                btnBack.Visible = True
                btnSave.Enabled = True
                btnPrint.Disabled = False
            Case "edit" 'Edit mode activated
                dgPameran.ShowFooter = False
                dgPameran.EditItemIndex = e.Item.ItemIndex

                btnBack.Visible = False
                btnSave.Enabled = False
                btnPrint.Disabled = True

            Case "delete" 'Delete this datagrid item 
                Try
                    arlPameran.RemoveAt(e.Item.ItemIndex)
                Catch ex As Exception
                End Try
            Case "cancel" 'Cancel Update this datagrid item 
                dgPameran.EditItemIndex = -1
                dgPameran.ShowFooter = True

                btnBack.Visible = True
                btnSave.Enabled = True
                btnPrint.Disabled = False
        End Select
        'Todo session
        'Session("DataPameran") = arlPameran
        sesHelper.SetSession("DataPameran", arlPameran)
        BindGridPameran()
    End Sub

    Private Sub dgIklan_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgIklan.ItemCommand
        arlIklan = CType(Session("DataIklan"), ArrayList)
        Select Case e.CommandName
            Case "add"
                Dim ddlMedia As DropDownList = CType(e.Item.FindControl("ddlFMedia"), DropDownList)
                Dim txtNamaMedia As TextBox = CType(e.Item.FindControl("txtFNamaMedia"), TextBox)
                Dim icDateStart As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("icFDateStart"), KTB.DNet.WebCC.IntiCalendar)
                Dim icEndDate As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("icFEndDate"), KTB.DNet.WebCC.IntiCalendar)
                Dim txtCost As TextBox = CType(e.Item.FindControl("txtFCost"), TextBox)

                If (arlIklan.Count > 0) Then
                    Dim objIklan As BPIklan = arlIklan(0)
                    If (CInt(ddlMedia.SelectedValue) <> objIklan.MediaType) Then
                        MessageBox.Show("Tipe Media Iklan Yang Diajukan Harus Sama Dengan Yang Pertama")
                        Exit Sub
                    End If
                End If

                If (txtNamaMedia.Text.Trim() = String.Empty) Then
                    MessageBox.Show("Nama Media harus diisi")
                    Exit Sub
                End If

                If (icDateStart.Value > icEndDate.Value) Then
                    MessageBox.Show("Tanggal iklan mulai harus lebih besar atau sama dengan tanggal iklan selesai")
                    Exit Sub
                End If

                If (txtCost.Text.Trim() = String.Empty) Then
                    MessageBox.Show("Biaya iklan harus diisi")
                    Exit Sub
                Else
                    If Not IsNumeric(txtCost.Text.Trim) Then
                        MessageBox.Show("Biaya iklan harus angka")
                        Exit Sub
                    Else
                        If Convert.ToDecimal(txtCost.Text.Trim) < 0 Then
                            MessageBox.Show("Biaya iklan tidak boleh minus")
                            Exit Sub
                        End If
                    End If
                End If

                Dim oIklan As New BPIklan
                oIklan.MediaType = ddlMedia.SelectedValue
                oIklan.MediaName = txtNamaMedia.Text.Trim()
                oIklan.StartDate = icDateStart.Value
                oIklan.EndDate = icEndDate.Value
                oIklan.Expense = txtCost.Text.Trim()
                oIklan.Status = EnumBabit.StatusBabitProposalItem.Baru

                Dim oCategory As New Category
                Dim oVechileType As New VechileType

                Dim ddlProductCategory As DropDownList = CType(e.Item.FindControl("ddlFProductCatIklan"), DropDownList)
                Dim ddlCarDisplay As DropDownList = CType(e.Item.FindControl("ddlFKendDisplay"), DropDownList)

                Dim strScript As String = String.Empty
                strScript += "<script language=""javascript"">"
                strScript += String.Format("DisplayVehicleType(document.all.{0}, document.all.{1});", ddlProductCategory.ClientID, ddlCarDisplay.ClientID)
                strScript += "</script>"

                If ddlProductCategory.SelectedValue = "-1" Then
                    MessageBox.Show("Produk Kategori harus diisi.")
                    phBottomScript.Controls.Add(New LiteralControl(strScript))
                    Return
                End If

                If hdnSelectedVehicleType.Value = "-1" Then
                    MessageBox.Show("Kendaraan display harus diisi.")
                    phBottomScript.Controls.Add(New LiteralControl(strScript))
                    Return
                End If

                oCategory = New FinishUnit.CategoryFacade(User).Retrieve(CInt(ddlProductCategory.SelectedValue))
                oVechileType = New FinishUnit.VechileTypeFacade(User).Retrieve(CInt(hdnSelectedVehicleType.Value))

                oiklan.Category = oCategory
                oiklan.VechileType = oVechileType

                arlIklan.Add(oIklan)
                ShowNoOfDays()
            Case "save" 'Update this datagrid item   
                Dim ddlMedia As DropDownList = CType(e.Item.FindControl("ddlEMedia"), DropDownList)
                Dim txtNamaMedia As TextBox = CType(e.Item.FindControl("txtENamaMedia"), TextBox)
                Dim icDateStart As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("icEDateStart"), KTB.DNet.WebCC.IntiCalendar)
                Dim icEndDate As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("icEEndDate"), KTB.DNet.WebCC.IntiCalendar)
                Dim txtCost As TextBox = CType(e.Item.FindControl("txtECost"), TextBox)

                If (e.Item.ItemIndex > 0) Then
                    If (arlIklan.Count > 0) Then
                        Dim objIklan As BPIklan = arlIklan(0)
                        If (CInt(ddlMedia.SelectedValue) <> objIklan.MediaType) Then
                            MessageBox.Show("Tipe Media Iklan Yang Diajukan Harus Sama Dengan Yang Pertama")
                            Exit Sub
                        End If
                    End If
                Else
                    For Each objIklan As BPIklan In arlIklan
                        objIklan.MediaType = CInt(ddlMedia.SelectedValue)
                    Next
                End If

                If (txtNamaMedia.Text.Trim() = String.Empty) Then
                    MessageBox.Show("Nama Media harus diisi")
                    Exit Sub
                End If

                If (icDateStart.Value > icEndDate.Value) Then
                    MessageBox.Show("Tanggal iklan mulai harus lebih besar atau sama dengan tanggal iklan selesai")
                    Exit Sub
                End If

                If (txtCost.Text.Trim() = String.Empty) Then
                    MessageBox.Show("Biaya iklan harus diisi")
                    Exit Sub
                End If

                Dim oIklan As BPIklan = CType(arlIklan(e.Item.ItemIndex), BPIklan)
                oIklan.MediaType = ddlMedia.SelectedValue
                oIklan.MediaName = txtNamaMedia.Text.Trim()
                oIklan.StartDate = icDateStart.Value
                oIklan.EndDate = icEndDate.Value
                oIklan.Expense = txtCost.Text.Trim()

                Dim oCategory As New Category
                Dim oVechileType As New VechileType

                Dim ddlProductCategory As DropDownList = CType(e.Item.FindControl("ddlEProductCatIklan"), DropDownList)
                Dim ddlCarDisplay As DropDownList = CType(e.Item.FindControl("ddlEKendDisplay"), DropDownList)

                Dim strScript As String = String.Empty
                strScript += "<script language=""javascript"">"
                strScript += String.Format("DisplayVehicleType(document.all.{0}, document.all.{1});", ddlProductCategory.ClientID, ddlCarDisplay.ClientID)
                strScript += "</script>"

                If ddlProductCategory.SelectedValue = "-1" Then
                    MessageBox.Show("Produk Kategori harus diisi.")
                    phBottomScript.Controls.Add(New LiteralControl(strScript))
                    Return
                End If

                If hdnSelectedVehicleType.Value = "-1" Then
                    MessageBox.Show("Kendaraan display harus diisi.")
                    phBottomScript.Controls.Add(New LiteralControl(strScript))
                    Return
                End If

                oCategory = New FinishUnit.CategoryFacade(User).Retrieve(CInt(ddlProductCategory.SelectedValue))
                oVechileType = New FinishUnit.VechileTypeFacade(User).Retrieve(CInt(hdnSelectedVehicleType.Value))

                oiklan.Category = oCategory
                oiklan.VechileType = oVechileType

                dgIklan.EditItemIndex = -1
                dgIklan.ShowFooter = True

                btnBack.Visible = True
                btnSave.Enabled = True
                btnPrint.Disabled = False
            Case "edit" 'Edit mode activated
                dgIklan.ShowFooter = False
                dgIklan.EditItemIndex = e.Item.ItemIndex

                btnBack.Visible = False
                btnSave.Enabled = False
                btnPrint.Disabled = True
            Case "delete" 'Delete this datagrid item 
                Try
                    arlIklan.RemoveAt(e.Item.ItemIndex)
                Catch ex As Exception
                End Try
            Case "cancel" 'Cancel Update this datagrid item 
                dgIklan.EditItemIndex = -1
                dgIklan.ShowFooter = True

                btnBack.Visible = True
                btnSave.Enabled = True
                btnPrint.Disabled = False
        End Select
        'Todo session
        'Session("DataIklan") = arlIklan
        sesHelper.SetSession("DataIklan", arlIklan)
        BindGridIklan()
    End Sub

    Private Sub dgIklan_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dgIklan.ItemDataBound
        If e.Item.ItemType = ListItemType.Footer Then
            Dim ddlMedia As DropDownList = CType(e.Item.FindControl("ddlFMedia"), DropDownList)
            BindMedia(ddlMedia)
            Dim ddlProductCategory As DropDownList = CType(e.Item.FindControl("ddlFProductCatIklan"), DropDownList)
            Dim ddlCarDisplay As DropDownList = CType(e.Item.FindControl("ddlFKendDisplay"), DropDownList)
            BindProductCategory(ddlProductCategory, ddlCarDisplay)
            BindCarDisplay(ddlCarDisplay)
        End If

        If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim oIklan As BPIklan = CType(e.Item.DataItem, BPIklan)
            Dim lblMedia As Label = CType(e.Item.FindControl("lblMedia"), Label)

            lblMedia.Text = IIf(oIklan.MediaType = EnumBabit.MediaType.Cetak, "Cetak", "Elektronik")

            If Not e.Item.FindControl("lbtnDelete") Is Nothing Then
                CType(e.Item.FindControl("lbtnDelete"), LinkButton).Attributes.Add("OnClick", _
                    New CommonFunction().PreventDoubleClickAtGrid(CType(e.Item.FindControl("lbtnDelete"), LinkButton), "Yakin Data ini akan dihapus?"))
            End If
        End If

        If e.Item.ItemType = ListItemType.EditItem OrElse e.Item.ItemType = ListItemType.SelectedItem Then
            Dim oIklan As BPIklan = CType(e.Item.DataItem, BPIklan)
            Dim ddlMedia As DropDownList = CType(e.Item.FindControl("ddlEMedia"), DropDownList)
            Dim icEDateStart As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("icEDateStart"), KTB.DNet.WebCC.IntiCalendar)
            Dim icEEndDate As KTB.DNet.WebCC.IntiCalendar = CType(e.Item.FindControl("icEEndDate"), KTB.DNet.WebCC.IntiCalendar)

            BindMedia(ddlMedia)
            ddlMedia.SelectedValue = IIf(oIklan.MediaType = EnumBabit.MediaType.Cetak, EnumBabit.MediaType.Cetak, EnumBabit.MediaType.Elektronik)
            icEDateStart.Value = oiklan.StartDate
            icEEndDate.Value = oiklan.EndDate

            Dim ddlProductCategory As DropDownList = CType(e.Item.FindControl("ddlEProductCatIklan"), DropDownList)
            Dim ddlCarDisplay As DropDownList = CType(e.Item.FindControl("ddlEKendDisplay"), DropDownList)
            BindProductCategory(ddlProductCategory, ddlCarDisplay)
            BindCarDisplay(ddlCarDisplay)

            Dim strScript As String = String.Empty
            strScript += "<script language=""javascript"">"
            strScript += String.Format("DisplayVehicleType(document.all.{0}, document.all.{1});", ddlProductCategory.ClientID, ddlCarDisplay.ClientID)
            strScript += String.Format("RestoreSelectedVehicleByValue(document.all.{0}, '{1}')", ddlCarDisplay.ClientID, oIklan.VechileType.ID)
            strScript += "</script>"
            phBottomScript.Controls.Add(New LiteralControl(strScript))
            If Not IsNothing(oiklan.Category) Then
                ddlProductCategory.SelectedValue = oiklan.Category.ID
            Else
                ddlProductCategory.SelectedValue = -1
            End If

        End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim str As String
        str = ValidateData()
        If (str.Length > 0) Then
            MessageBox.Show(str)
            Exit Sub
        End If

        If (hdnValNew.Value = "-1") Then
            Dim _filename As String = System.IO.Path.GetFileName(UploadFile.PostedFile.FileName)
            If (Request.QueryString("Mode") = "Edit") Then
                If _filename.Trim().Length > 0 Then
                    If UploadFile.PostedFile.ContentLength > MAX_FILE_SIZE Then
                        MessageBox.Show("Ukuran file tidak boleh melebihi " & (MAX_FILE_SIZE / 1024) & "kb\n")
                        Return
                    End If
                    sesHelper.SetSession(FU, UploadFile.PostedFile.InputStream)
                    sesHelper.SetSession(FU_NAME, UploadFile.PostedFile.FileName)
                End If
            Else
                If IsLoginAsDealer() Then
                    If _filename.Trim().Length <= 0 Then
                        MessageBox.Show("Upload file belum diisi\n")
                        Return
                    End If
                End If
                If UploadFile.PostedFile.ContentLength > MAX_FILE_SIZE Then
                    MessageBox.Show("Ukuran file tidak boleh melebihi " & (MAX_FILE_SIZE / 1024) & "kb\n")
                    Return
                End If
                sesHelper.SetSession(FU, UploadFile.PostedFile.InputStream)
                sesHelper.SetSession(FU_NAME, UploadFile.PostedFile.FileName)
            End If
            MessageBox.Confirm("Anda yakin ingin Simpan Pengajuan ?", "hdnValNew")
            Return
        End If

        Dim oBabitProposal As New BabitProposal
        If (Request.QueryString("Mode") = "Edit") Then
            Dim oBabitProp As BabitProposal = CType(Session("BabitProposal"), BabitProposal)
            If (UpdateBabitProposal(oBabitProp) = 1) Then
                'ddlJenisKegiatan.SelectedIndex = 0
                If Not IsLoginAsDealer() Then
                    Dim ktbid As Integer = 0
                    'cek if the proposal CreatedBy ktb user, so it can be edit by ktb
                    Try
                        ktbid = CInt(oBabitProp.CreatedBy.Substring(0, 6))
                        If (ktbid = objDealer.ID) Then
                            If (oBabitProp.Status = EnumBabit.StatusBabitProposal.Baru) Then
                                btnSubmit.Enabled = True
                            End If
                        Else
                            btnSubmit.Enabled = False
                        End If
                    Catch ex As Exception
                        btnSubmit.Enabled = False
                    End Try
                ElseIf (oBabitProp.Status = EnumBabit.StatusBabitProposal.Baru) Then
                    btnSubmit.Enabled = True
                End If

                lblFileUpload.Text = oBabitProp.FileName
                MessageBox.Show(SR.UpdateSucces)
            Else
                MessageBox.Show(SR.UpdateFail)
            End If
        Else
            If (Session("BabitAllocation") Is Nothing) Then
                MessageBox.Show("Babit Alokasi belum ada")
                Exit Sub
            End If
            If (SaveBabitProposal(oBabitProposal) = 1) Then
                Dim Obj As New BabitProposal
                Obj = New BabitSalesComm.BabitProposalFacade(User).Retrieve(oBabitProposal.ID)
                lblNoPengajuan.Text = Obj.NoPengajuan
                lblNoPerjanjian.Text = Obj.BabitAllocation.NoPerjanjian
                txtNoSuratDealer.Text = Obj.NoSuratDealer
                lblFileUpload.Text = Obj.FileName
                btnSubmit.Enabled = True
                If (ddlJenisKegiatan.SelectedValue = CType(EnumBabit.BabitProposalType.Pameran, Short)) Then
                    'Todo session
                    'Session("BabitProposalPameran") = Obj
                    sesHelper.SetSession("BabitProposalPameran", Obj)
                    hdnPameran.Value = Obj.NoPengajuan
                    MessageBox.Show(SR.SaveSuccess)
                    'MessageBox.Show("Validasi Pengajuan BABIT Pameran/Event Harus Dilakukan Maksimal 7 Hari Sebelum Kegiatan Pameran / Event")
                ElseIf (ddlJenisKegiatan.SelectedValue = CType(EnumBabit.BabitProposalType.Even, Short)) Then
                    'Todo session
                    'Session("BabitProposalEvent") = Obj
                    sesHelper.SetSession("BabitProposalEvent", Obj)
                    hdnEvent.Value = Obj.NoPengajuan
                    MessageBox.Show(SR.SaveSuccess)
                    'MessageBox.Show("Validasi Pengajuan BABIT Pameran/Event Harus Dilakukan Maksimal 7 Hari Sebelum Kegiatan Pameran / Event")
                ElseIf (ddlJenisKegiatan.SelectedValue = CType(EnumBabit.BabitProposalType.Iklan, Short)) Then
                    'Todo session
                    'Session("BabitProposalIklan") = Obj
                    sesHelper.SetSession("BabitProposalIklan", Obj)
                    hdnIklan.Value = Obj.NoPengajuan
                    MessageBox.Show(SR.SaveSuccess)
                End If
                ' ddlJenisKegiatan.SelectedIndex = 0
                ' btnBaru_Click(sender, e)
            Else
                MessageBox.Show(SR.SaveFail)
            End If
        End If

    End Sub

    Private Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        If (hdnValSubmit.Value = "-1") Then
            MessageBox.Confirm("Anda yakin ingin Submit Pengajuan ?", "hdnValSubmit")
            Return
        End If

        If (ddlJenisKegiatan.SelectedValue = CType(EnumBabit.BabitProposalType.Pameran, Short)) Then
            Dim oBabitProposal As BabitProposal = CType(Session("BabitProposalPameran"), BabitProposal)
            oBabitProposal.Status = CType(EnumBabit.StatusBabitProposal.Validasi, Short)
            If (New BabitSalesComm.BabitProposalFacade(User).UpdateBabitProposal(oBabitProposal) <> -1) Then
                MessageBox.Show(SR.SaveSuccess)
                hdnPameranSubmit.Value = oBabitProposal.NoPengajuan
            Else
                MessageBox.Show(SR.SaveFail)
            End If
        ElseIf (ddlJenisKegiatan.SelectedValue = CType(EnumBabit.BabitProposalType.Even, Short)) Then
            Dim oBabitProposal As BabitProposal = CType(Session("BabitProposalEvent"), BabitProposal)
            oBabitProposal.Status = CType(EnumBabit.StatusBabitProposal.Validasi, Short)
            If (New BabitSalesComm.BabitProposalFacade(User).UpdateBabitProposal(oBabitProposal) <> -1) Then
                MessageBox.Show(SR.SaveSuccess)
                hdnEventSubmit.Value = oBabitProposal.NoPengajuan
            Else
                MessageBox.Show(SR.SaveFail)
            End If
        ElseIf (ddlJenisKegiatan.SelectedValue = CType(EnumBabit.BabitProposalType.Iklan, Short)) Then
            Dim oBabitProposal As BabitProposal = CType(Session("BabitProposalIklan"), BabitProposal)
            oBabitProposal.Status = CType(EnumBabit.StatusBabitProposal.Validasi, Short)
            If (New BabitSalesComm.BabitProposalFacade(User).UpdateBabitProposal(oBabitProposal) <> -1) Then
                MessageBox.Show(SR.SaveSuccess)
                hdnIklanSubmit.Value = oBabitProposal.NoPengajuan
                btnSubmit.Enabled = True
            Else
                MessageBox.Show(SR.SaveFail)
            End If
        End If
    End Sub

    Private Sub dgEvent_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgEvent.ItemCommand
        arlEvent = CType(Session("DataEvent"), ArrayList)
        Select Case e.CommandName
            Case "add"
                Dim txtPlace As TextBox = CType(e.Item.FindControl("txtFPlace"), TextBox)
                Dim txtConsumsion As TextBox = CType(e.Item.FindControl("txtFConsumsion"), TextBox)
                Dim txtEntertainment As TextBox = CType(e.Item.FindControl("txtFEntertainment"), TextBox)
                Dim txtEquipment As TextBox = CType(e.Item.FindControl("txtFEquipment"), TextBox)
                Dim txtOthers As TextBox = CType(e.Item.FindControl("txtFOthers"), TextBox)

                If (txtPlace.Text = "" And txtConsumsion.Text = "" And txtEntertainment.Text = "" And txtEquipment.Text = "" And txtOthers.Text = "") Then
                    MessageBox.Show("Item harus diisi minimal 1, atau kosongkan grid jika tidak ada item")
                    Exit Sub
                End If

                If Not IsNumeric(txtPlace.Text.Trim) Then
                    txtPlace.Text = "0"
                Else
                    If Convert.ToDecimal(txtPlace.Text.Trim) < 0 Then
                        MessageBox.Show("Sewa tempat tidak boleh minus")
                        Exit Sub
                    End If
                End If

                If (txtConsumsion.Text.Trim() = String.Empty) Then
                    txtConsumsion.Text = "0"
                Else
                    If Not IsNumeric(txtConsumsion.Text.Trim) Then
                        MessageBox.Show("Konsumsi harus angka")
                        Exit Sub
                    Else
                        If Convert.ToDecimal(txtConsumsion.Text.Trim) < 0 Then
                            MessageBox.Show("Konsumsi tidak boleh minus")
                            Exit Sub
                        End If
                    End If
                End If

                If (txtEntertainment.Text.Trim() = String.Empty) Then
                    txtEntertainment.Text = "0"
                Else
                    If Not IsNumeric(txtEntertainment.Text.Trim) Then
                        MessageBox.Show("Entertainment harus angka")
                        Exit Sub
                    Else
                        If Convert.ToDecimal(txtEntertainment.Text.Trim) < 0 Then
                            MessageBox.Show("Entertainment tidak boleh minus")
                            Exit Sub
                        End If
                    End If
                End If

                If (txtEquipment.Text.Trim() = String.Empty) Then
                    txtEquipment.Text = "0"
                Else
                    If Not IsNumeric(txtEquipment.Text.Trim) Then
                        MessageBox.Show("Perlengkapan harus angka")
                        Exit Sub
                    Else
                        If Convert.ToDecimal(txtEquipment.Text.Trim) < 0 Then
                            MessageBox.Show("Perlengkapan tidak boleh minus")
                            Exit Sub
                        End If
                    End If
                End If

                If (txtOthers.Text.Trim() = String.Empty) Then
                    txtOthers.Text = "0"
                Else
                    If Not IsNumeric(txtOthers.Text.Trim) Then
                        MessageBox.Show("Perlengkapan harus angka")
                        Exit Sub
                    Else
                        If Convert.ToDecimal(txtOthers.Text.Trim) < 0 Then
                            MessageBox.Show("Biaya Lain tidak boleh minus")
                            Exit Sub
                        End If
                    End If
                End If

                Dim oEventActivity As New EventActivity
                oEventActivity.Comsumption = txtConsumsion.Text.Trim
                oEventActivity.Entertainment = txtEntertainment.Text.Trim
                oEventActivity.Equipment = txtEquipment.Text.Trim
                oEventActivity.Place = txtPlace.Text.Trim
                oEventActivity.Others = txtOthers.Text

                arlEvent.Add(oEventActivity)
                ShowNoOfDays()
            Case "save" 'Update this datagrid item   
                Dim txtPlace As TextBox = CType(e.Item.FindControl("txtEPlace"), TextBox)
                Dim txtConsumsion As TextBox = CType(e.Item.FindControl("txtEConsumsion"), TextBox)
                Dim txtEntertainment As TextBox = CType(e.Item.FindControl("txtEEntertainment"), TextBox)
                Dim txtEquipment As TextBox = CType(e.Item.FindControl("txtEEquipment"), TextBox)
                Dim txtOthers As TextBox = CType(e.Item.FindControl("txtEOthers"), TextBox)

                If (txtPlace.Text.Trim() = String.Empty) Then
                    MessageBox.Show("Sewa tempat harus diisi")
                    Exit Sub
                End If

                If (txtConsumsion.Text.Trim() = String.Empty) Then
                    MessageBox.Show("Konsumsi harus diisi")
                    Exit Sub
                End If

                If (txtEntertainment.Text.Trim() = String.Empty) Then
                    MessageBox.Show("Entertainment harus diisi")
                    Exit Sub
                End If

                If (txtEquipment.Text.Trim() = String.Empty) Then
                    MessageBox.Show("Perlengkapan harus diisi")
                    Exit Sub
                End If

                Dim oEventActivity As EventActivity = CType(arlEvent(e.Item.ItemIndex), EventActivity)
                oEventActivity.Comsumption = txtConsumsion.Text.Trim
                oEventActivity.Entertainment = txtEntertainment.Text.Trim
                oEventActivity.Equipment = txtEquipment.Text.Trim
                oEventActivity.Place = txtPlace.Text.Trim
                oEventActivity.Others = txtOthers.Text

                dgEvent.EditItemIndex = -1
                dgEvent.ShowFooter = True

                btnBack.Visible = True
                btnSave.Enabled = True
                btnPrint.Disabled = False

            Case "edit" 'Edit mode activated
                dgEvent.ShowFooter = False
                dgEvent.EditItemIndex = e.Item.ItemIndex

                btnBack.Visible = False
                btnSave.Enabled = False
                btnPrint.Disabled = True
            Case "delete" 'Delete this datagrid item 
                Try
                    arlEvent.RemoveAt(e.Item.ItemIndex)
                Catch ex As Exception
                End Try
            Case "cancel" 'Cancel Update this datagrid item 
                dgEvent.EditItemIndex = -1
                dgEvent.ShowFooter = True

                btnBack.Visible = True
                btnSave.Enabled = True
                btnPrint.Disabled = False
        End Select
        'Todo session
        'Session("DataEvent") = arlEvent
        sesHelper.SetSession("DataEvent", arlEvent)
        BindGridEvent()
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Not IsNothing(Request.QueryString("Src")) Then
            Select Case Request.QueryString("Src")
                Case "ListPengajuan"
                    Response.Redirect("../Babit/FrmListPengajuanBabit.aspx", True)
                Case "ListAlokasi"
                    Response.Redirect("../Babit/FrmAlokasiBabitList.aspx", True)
                Case "Pemotongan"
                    Response.Redirect("../Babit/FrmPemotonganAlokasiBabit.aspx?Src=ListProposalBabit", True)
            End Select
        Else
            Response.Redirect("../Babit/FrmListPengajuanBabit.aspx", True)
        End If
    End Sub

    Private Sub btnBaru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBaru.Click
        lblNoPengajuan.Text = "Auto Generated"
        hdnIklan.Value = String.Empty
        hdnEvent.Value = String.Empty
        hdnPameran.Value = String.Empty
        hdnValNew.Value = "-1"

        txtTempatPameran.Text = String.Empty
        txtUkuranTempatPameran.Text = String.Empty
        txtJumlahHariPameran.Text = String.Empty
        txtTargetPenjualanPameran.Text = String.Empty
        txtBiayaPameran.Text = String.Empty
        txtTempatEvent.Text = String.Empty
        txtUkuranTempatEvent.Text = String.Empty
        txtJumlahHariEvent.Text = String.Empty
        txtTargetPenjualanEvent.Text = String.Empty
        btnSave.Enabled = True
        btnSubmit.Enabled = False
        ddlJenisKegiatan.SelectedIndex = 0
        txtNoSuratDealer.Text = String.Empty

        arlPameran = New ArrayList
        'Todo session
        'Session("DataPameran") = arlPameran
        sesHelper.SetSession("DataPameran", arlPameran)
        BindGridPameran()

        arlEvent = New ArrayList
        'Todo session
        'Session("DataEvent") = arlEvent
        sesHelper.SetSession("DataEvent", arlEvent)
        BindGridEvent()

        arlIklan = New ArrayList
        'Todo session
        'Session("DataIklan") = arlIklan
        sesHelper.SetSession("DataIklan", arlIklan)
        BindGridIklan()

        lblFileUpload.Text = String.Empty
    End Sub

    Private Sub btnFindDealer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindDealer.Click
        fillDealerInfoFromKTB()
        If (btnSave.Enabled = False) Then
            MessageBox.Show("Alokasi babit khusus untuk periode ini tidak tersedia")
        End If
    End Sub

#End Region

End Class
