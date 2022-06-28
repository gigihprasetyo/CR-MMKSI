Imports System.Text
Imports System.IO

#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.UserManagement
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security
Imports KTB.DNet.Parser
Imports OfficeOpenXml
Imports System.Collections.Generic
Imports System.Linq
Imports GlobalExtensions
#End Region

Public Class FrmTrClassAss
    Inherits System.Web.UI.Page

    Dim sHCourse As SessionHelper = New SessionHelper
    Dim indexPage As Integer = 0
    Dim totalRow As Integer = 0
    Dim helpers As New TrainingHelpers(Me.Page)


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub


    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Private bPrivilegeChangeClass As Boolean = False
    Private bPrivilegeAlocationClass As Boolean = False


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " subs & function "

    'Private Sub BindDdlCategory()
    '    ddlCategory.Items.Clear()
    '    ddlCategory.DataSource = EnumTrClass.RetrieveEnumTrClass()
    '    ddlCategory.DataValueField = "ID"
    '    ddlCategory.DataTextField = "Name"
    '    ddlCategory.DataBind()
    'End Sub
    Private Sub initiatePage()
        clearData()
        ViewState("CurrentSortColumn") = "ClassCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
        BindDropDown()
    End Sub

    Private Sub BindDropDown()
        BindDdlTipeKelas()
        BindDDLTahunFiskal()
        BindDDLJobPosisi()
    End Sub

    Private Sub BindDDLJobPosisi()
        ddlJobPosition.Items.Clear()

        Dim listItem As New ListItem("Silakan Pilih", "-1")
        ddlJobPosition.Items.Add(listItem)

        listItem = New ListItem("Sparepart", "0")
        ddlJobPosition.Items.Add(listItem)

        listItem = New ListItem("Service", "2")
        ddlJobPosition.Items.Add(listItem)

        listItem = New ListItem("Body Paint", "3")
        ddlJobPosition.Items.Add(listItem)

    End Sub


    Private Sub BindDdlTipeKelas()
        ddlTipeKelas.ClearSelection()
        ddlTipeKelas.Items.Clear()
        ddlTipeKelas.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
        ddlTipeKelas.Items.Add(New ListItem(EnumTrClass.GetStringValueClassType(EnumTrClass.EnumClassType.INCLASS_TRAINING), EnumTrClass.EnumClassType.INCLASS_TRAINING))
        ddlTipeKelas.Items.Add(New ListItem(EnumTrClass.GetStringValueClassType(EnumTrClass.EnumClassType.E_LEARNING), EnumTrClass.EnumClassType.E_LEARNING))
        'ddlTipeKelas.Items.Add(New ListItem(EnumTrClass.GetStringValueClassType(EnumTrClass.EnumClassType.INHOUSE_TRAINING), EnumTrClass.EnumClassType.INHOUSE_TRAINING))
        'ddlTipeKelas.Items.Add(New ListItem(EnumTrClass.GetStringValueClassType(EnumTrClass.EnumClassType.FLEET_TRAINING), EnumTrClass.EnumClassType.FLEET_TRAINING))
    End Sub

    Private Sub BindDDLTahunFiskal()
        Dim GetTahun As Integer = DateTime.Now.Year
        ddlTahunFiscal.ClearSelection()
        ddlTahunFiscal.Items.Clear()
        ddlTahunFiscal.Items.Insert(0, New ListItem("Silahkan Pilih", "-1"))
        'Before
        For x As Integer = 4 To 0 Step -1
            Dim value1 As String = (GetTahun - x).ToString()
            Dim value2 As String = (GetTahun - x - 1).ToString()
            Dim value As String = String.Format("{0}/{1}", value2, value1)
            ddlTahunFiscal.Items.Add(New ListItem(value, value))
        Next
        'After
        For x As Integer = 0 To 4
            Dim value1 As String = (GetTahun + x).ToString()
            Dim value2 As String = (GetTahun + x + 1).ToString()
            Dim value As String = String.Format("{0}/{1}", value1, value2)
            ddlTahunFiscal.Items.Add(New ListItem(value, value))
        Next
        ' ddlTahunFiscal.SelectedValue = String.Format("{0}/{1}", GetTahun.ToString(), (GetTahun + 1).ToString())
    End Sub

    Private Sub bindDataGrid(ByVal indexPage As Integer)
        Dim objTrCourse As TrCourse
        Dim arTrCourse, artrClass As ArrayList
        Dim startDate, finishDate As DateTime
        Dim criterias As CriteriaComposite
        criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If Not String.IsNullOrEmpty(txtKodeKategori.Text) Then
            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourse), "CourseCode", MatchType.Exact, txtKodeKategori.Text))

            arTrCourse = New TrCourseFacade(User).Retrieve(criterias2)

            If arTrCourse.Count > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "TrCourse.ID", MatchType.Exact, CType(arTrCourse(0), TrCourse).ID))
            Else
                dtgTrClass.DataSource() = Nothing
                dtgTrClass.VirtualItemCount = 0
                dtgTrClass.DataBind()
                Return
            End If
        End If

        If Not String.IsNullOrEmpty(txtClassCode.Text) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "ClassCode", MatchType.[Partial], txtClassCode.Text.Trim))
        End If

        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "TrCourse.JobPositionCategory.JobPositionCategoryArea.ID", MatchType.Exact, 2))

        If txtPeriod.Text.Length.Equals(4) Then
            startDate = New DateTime(txtPeriod.Text, 12, Date.DaysInMonth(txtPeriod.Text, 12), 23, 59, 59)
            finishDate = New DateTime(txtPeriod.Text, 1, 1, 0, 0, 0)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "StartDate", MatchType.LesserOrEqual, startDate))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "FinishDate", MatchType.GreaterOrEqual, finishDate))
        ElseIf (txtPeriod.Text.Length > 0 And txtPeriod.Text.Length <> 4) Then
            MessageBox.Show("Format tahun tidak benar")
            Exit Sub
        End If
        If Not String.IsNullOrEmpty(txtLocation.Text) Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "Location", MatchType.[Partial], txtLocation.Text))
        End If
        ' criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "TrCourse.JobPositionCategory.ID", MatchType.Exact, "1"))

        If ddlTahunFiscal.SelectedValue <> "-1" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "FiscalYear", MatchType.Exact, ddlTahunFiscal.SelectedValue))
        End If

        If ddlTipeKelas.SelectedValue <> "-1" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "ClassType", MatchType.Exact, ddlTipeKelas.SelectedValue))
        End If

        If ddlJobPosition.SelectedValue <> "-1" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "TrCourse.JobPositionCategory.ID", MatchType.Exact, ddlJobPosition.SelectedValue))
        End If

        sHCourse.SetSession("searchRes", criterias)
        dtgTrClass.DataSource() = New TrClassFacade(User).RetrieveActiveList(criterias, indexPage + 1, dtgTrClass.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dtgTrClass.VirtualItemCount = totalRow
        dtgTrClass.DataBind()
    End Sub
    Private Sub clearData()

        For Each c As Control In Me.pnl1.Controls
            If TypeOf c Is TextBox Then
                CType(c, TextBox).Text = String.Empty
            End If
        Next

        If dtgTrClass.Items.Count > 0 Then
            dtgTrClass.SelectedIndex = -1
        End If
        lblPopUpCourse.Enabled = True
        ViewState.Add("vsProcess", "Insert")
    End Sub

    Private Sub deleteTrClass(ByVal nID As Integer)
        If New HelperFacade(User, GetType(TrClass)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(TrClass), nID), _
            CreateAggreateForCheckRecord(GetType(TrClass))) Then
            MessageBox.Show(SR.CannotDelete)
        Else
            If Not checkForeignKey(nID) Then
                MessageBox.Show(SR.CannotDelete)
            Else
                Dim objTrClass As TrClass = New TrClassFacade(User).Retrieve(nID)
                Dim facade As TrClassFacade = New TrClassFacade(User)
                'facade.DeleteFromDB(objTrClass)
                objTrClass.RowStatus = CType(DBRowStatus.Deleted, Short)
                facade.Update(objTrClass)
                dtgTrClass.CurrentPageIndex = 0
                bindDataGrid(dtgTrClass.CurrentPageIndex)

            End If
        End If
    End Sub

    Private Sub deleteTrClassUpload(ByVal nID As Integer)
        If New HelperFacade(User, GetType(TrClass)).IsRecordExist(CreateCriteriaForCheckRecord(GetType(TrClass), nID), _
            CreateAggreateForCheckRecord(GetType(TrClass))) Then
            MessageBox.Show(SR.CannotDelete)
        Else
            If Not checkForeignKey(nID) Then
                MessageBox.Show(SR.CannotDelete)
            Else
                Dim objTrClass As TrClass = New TrClassFacade(User).Retrieve(nID)
                Dim facade As TrClassFacade = New TrClassFacade(User)
                facade.DeleteFromDB(objTrClass)
                Dim arlCA As ArrayList = CType(sHCourse.GetSession("arlCA"), ArrayList)
                Dim i As Integer = 0
                For i = 0 To arlCA.Count - 1
                    Dim oClass As TrClass = CType(arlCA(i), TrClass)
                    If oClass.ID = nID Then
                        arlCA.Remove(oClass)
                    End If
                Next
                dtgUpload.DataSource = arlCA
                dtgUpload.DataBind()
            End If
        End If
    End Sub
    Private Function CreateCriteriaForCheckRecord(ByVal DomainType As Type, ByVal classID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(DomainType, "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(DomainType, "ClassCode", MatchType.Exact, classID))
        Return criterias
    End Function
    Private Function CreateAggreateForCheckRecord(ByVal DomainType As Type) As Aggregate
        Dim aggregates As New Aggregate(DomainType, "ID", AggregateType.Count)
        Return aggregates
    End Function
    Private Function checkForeignKey(ByVal id As String) As Boolean
        Dim objTrClassRegistrationAl As ArrayList
        Dim objTrClassAllocationAl As ArrayList
        Dim criterias As CriteriaComposite

        criterias = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "TrClass.ID", MatchType.Exact, id))
        objTrClassRegistrationAl = New TrClassRegistrationFacade(User).Retrieve(criterias)

        If Not objTrClassRegistrationAl.Count = 0 Then
            Return False
        Else
            criterias = New CriteriaComposite(New Criteria(GetType(TrClassAllocation), "TrClass.ID", MatchType.Exact, id))
            objTrClassAllocationAl = New TrClassAllocationFacade(User).Retrieve(criterias)

            If Not objTrClassAllocationAl.Count = 0 Then
                Return False
            Else
                Return True
            End If
        End If

    End Function
    Private Sub boundDtgTrClass(ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        If Not e.Item.FindControl("btnHapus") Is Nothing Then
            CType(e.Item.FindControl("btnHapus"), LinkButton).Attributes.Add("OnClick", "return confirm('Yakin Data ini akan dihapus?');")
        End If
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            CType(e.Item.FindControl("lblNo"), Label).Text = e.Item.ItemIndex + 1 + (dtgTrClass.CurrentPageIndex * dtgTrClass.PageSize)
        End If
    End Sub
    Private Sub assignAttributeControl()
        lblPopUpCourse.Attributes("onclick") = "ShowPPCourseSelection();"
        lblPopUpClass.Attributes("onclick") = "ShowPPClassSelection();"
    End Sub

    Private Function SetFormatEmail(ByVal ObjTrClass As TrClass, ByVal ObjDealer As Dealer) As String
        Dim sb As StringBuilder = New StringBuilder
        Dim strDate As String = Format(DateTime.Now, "dd-MMM-yyyy hh:mm:ss")

        sb.Append("<html><body><Table width=2000px><h1><td colspan = 4><b>")
        sb.Append("Jakarta, ")
        sb.Append(strDate)
        sb.Append("</b></td></h1><tr><td>")
        sb.Append("</td></tr><tr><td><br></td></tr>")
        sb.Append("<tr><td>Kepada Yth,</td></tr>")
        sb.Append("<tr><td>PT MMKSI - TC</td></tr>")
        sb.Append("<tr><td></td></tr>")
        sb.Append("<tr><td></td></tr>")
        sb.Append("<tr><td><br></td></tr>")
        sb.Append("<tr><td>Mohon diproses Alokasi Tambahan untuk kelas berikut ")
        sb.Append(ObjTrClass.ClassCode)
        sb.Append("</td></tr><tr><td><br></td></tr>")
        sb.Append("<tr><td></td></tr>")
        sb.Append("<tr><td>Terima kasih.</td></tr>")
        sb.Append("<tr><td></td></tr>")
        sb.Append("<tr><td>" & ObjDealer.DealerName & "</td></tr>")
        sb.Append("<tr><td>" & ObjDealer.DealerCode & "</td></tr>")
        sb.Append("</table></body></html>")

        Return sb.ToString()
    End Function

    Private Function GetEmailTrTraineeKTB(ByVal tipe As String) As String
        Dim retValue As String
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrUserEmail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrUserEmail), "Tipe", MatchType.Exact, tipe.ToUpper))
        Dim emailList As ArrayList = New TrUserEmailFacade(User).Retrieve(criterias)
        If emailList.Count > 0 Then
            For Each item As TrUserEmail In emailList
                retValue += item.Email + ";"
            Next
        Else
            retValue = String.Empty
        End If
        Return retValue.Trim(";")
    End Function

    Private Function GetEmailKTB() As String
        Dim retValue As String
        Dim criterias As New CriteriaComposite(New Criteria(GetType(BusinessArea), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(BusinessArea), "Dealer.Title", MatchType.Exact, EnumDealerTittle.DealerTittle.KTB))
        criterias.opAnd(New Criteria(GetType(BusinessArea), "Kind", MatchType.Exact, CType(EnumDealerTransKind.DealerTransKind.ServiceUnit, Short)))
        Dim ArrListBusinessArea As ArrayList = New BusinessAreaFacade(User).Retrieve(criterias)
        If ArrListBusinessArea.Count > 0 Then
            retValue = CType(ArrListBusinessArea(0), BusinessArea).Email
        Else
            retValue = ""
        End If
        Return retValue
    End Function

    Private Function GetEmailDealer() As String
        Dim retValue As String
        If Not IsNothing(sHCourse.GetSession("DEALER")) Then
            Dim ObjDealer As Dealer = CType(sHCourse.GetSession("DEALER"), Dealer)
            Dim strUserName As String = User.Identity.Name.Trim().Substring(6)
            Dim criterias As New CriteriaComposite(New Criteria(GetType(UserInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(UserInfo), "Dealer.ID", MatchType.Exact, ObjDealer.ID))
            criterias.opAnd(New Criteria(GetType(UserInfo), "UserName", MatchType.Exact, strUserName))
            Dim ArrListUserInfo As ArrayList = New UserInfoFacade(User).Retrieve(criterias)
            If ArrListUserInfo.Count > 0 Then
                retValue = CType(ArrListUserInfo(0), UserInfo).Email
            Else
                retValue = ""
            End If
        Else
            retValue = ""
        End If
        Return retValue
    End Function

    Private Function SendEmail(ByVal IDClass As Integer) As Integer
        Dim retValue As Integer = 0 '0 = gagal ; 1 = sukses

        Dim ObjDealer As New Dealer
        If Not IsNothing(sHCourse.GetSession("DEALER")) Then
            ObjDealer = CType(sHCourse.GetSession("DEALER"), Dealer)
        End If
        Dim objTrClass As TrClass = New TrClassFacade(User).Retrieve(IDClass)

        Dim strSmtp As String = KTB.DNet.Lib.WebConfig.GetValue("SMTP")
        Dim objEmail As DNetMail = New DNetMail(strSmtp)
        Dim strFrom As String = GetEmailDealer()
        Dim strSubject As String = "[MMKSI-DNet] Service - Proses Alokasi Tambahan Training"
        Dim strTo As String = GetEmailTrTraineeKTB("TO")
        Dim strCC As String = GetEmailTrTraineeKTB("CC")
        If strTo.Trim = String.Empty Then
            MessageBox.Show("Email Penerima tidak ada")
            Exit Function
        End If

        Dim strBody As String = SetFormatEmail(objTrClass, ObjDealer)
        Try
            objEmail.sendMail(strTo, strCC, strFrom, strSubject, Mail.MailFormat.Html, strBody)
            retValue = 1
        Catch ex As Exception
            retValue = 0
        End Try
        Return retValue

    End Function

#End Region

#Region " control event handler "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        helpers.CheckPrivilege("priv5B")
        If Not Page.IsPostBack Then
            initiatePage()
            btnSimpan.Enabled = False
            btnBatal.Enabled = False
            If Not sHCourse.GetSession("backRes") Is Nothing And (Not sHCourse.GetSession("searchRes") Is Nothing) Then
                dtgTrClass.DataSource() = New TrClassFacade(User).RetrieveActiveList(CType(sHCourse.GetSession("searchRes"), CriteriaComposite), indexPage + 1, dtgTrClass.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
                dtgTrClass.VirtualItemCount = totalRow
                dtgTrClass.DataBind()
                btnDownload.Visible = True
                If Not IsNothing(sHCourse.GetSession("DaftarMKS")) Then
                    MessageBox.Show(SR.SaveSuccess)
                End If

            ElseIf Not IsNothing(sHCourse.GetSession("arlCA")) Then
                DataBindUpload()
            End If
            SetActiveControl(helpers.IsEdit)
        End If
        If (CType(Session("DEALER"), Dealer).Title = EnumDealerTittle.DealerTittle.DEALER) Then
            btnBaru.Visible = False
            fileUpload.Visible = False
            btnUpload.Visible = False
            lblUploadFile.Visible = False
            lblSprtUpload.Visible = False
            btnSimpan.Visible = False
            btnBatal.Visible = False
            btnReport.Visible = False
        End If
        assignAttributeControl()

    End Sub

    Private Sub SetActiveControl(ByVal isActive As Boolean)
        btnBaru.Visible = isActive
        btnBatal.Visible = isActive
        trUpload.Visible = isActive
        trTemplate.Visible = isActive
    End Sub

    Private Sub DataBindUpload()
        Dim arlClass As List(Of TrClass) = CType(helpers.GetSession("dataUpload"), List(Of TrClass))
        Dim arlTrClass As ArrayList
        If arlClass.Count > 0 Then
            Dim strID As String = String.Empty
            For Each oClass As TrClass In arlClass
                strID += CType(oClass.ID, String) + ","
            Next
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "ID", MatchType.InSet, "(" & strID.Substring(0, strID.Length - 1) & ")"))

            arlTrClass = New TrClassFacade(User).Retrieve(criterias)

            If arlTrClass.Count > 0 Then
                helpers.RemoveSession("dataUpload")
                helpers.SetSession("dataUpload", arlTrClass)

                dtgUpload.DataSource() = arlTrClass
                dtgUpload.VirtualItemCount = arlTrClass.Count
                dtgUpload.DataBind()
                dtgUpload.Columns(11).Visible = False 'err message
                dtgUpload.Columns(12).Visible = True

                btnDownload.Visible = True
            End If
        End If
    End Sub

   
    Private Function IsUnhack() As Boolean
        If txtKodeKategori.Text.IndexOf("<") >= 0 Or txtKodeKategori.Text.IndexOf(">") >= 0 Or txtKodeKategori.Text.IndexOf("'") >= 0 Then
            Return False
        End If
        Return True
    End Function
    Private Sub btnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSimpan.Click
        If Not Page.IsValid Then
            Return
        End If
        'oon sHCourse.SetSession("arlCA", arlCA)
        Dim arlClass As List(Of TrClass) = CType(helpers.GetSession("dataUpload"), List(Of TrClass))
        Dim arlClassNew As New ArrayList
        If arlClass.Count > 0 Then
            For Each oClass As TrClass In arlClass
                Dim iReturn As Integer
                iReturn = InsertClass(oClass)
                If iReturn > 0 Then
                    oClass.ID = iReturn
                End If
            Next
            helpers.SetSession("dataUpload", arlClass)
            DataBindUpload()
        End If
        setUploadControl(False)
        btnDownload.Enabled = True
    End Sub

    Private Function InsertClass(ByVal objTrClass As TrClass) As Integer
        Return New TrClassFacade(User).InsertTransaction(objTrClass)
    End Function

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        helpers.RemoveSession("dataUpload")
        Response.Redirect("FrmTrClassAss.aspx")
    End Sub
    Private Sub dtgCourse_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgTrClass.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            'viewGroup(e.Item.Cells(0).Text, False)
            sHCourse.SetSession("viewID", CInt(e.Item.Cells(0).Text))
            Response.Redirect("FrmTrClassDetailAss.aspx")

            lblPopUpCourse.Enabled = False
            dtgTrClass.SelectedIndex = -1
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            sHCourse.SetSession("editID", CInt(e.Item.Cells(0).Text))
            dtgTrClass.SelectedIndex = e.Item.ItemIndex
            Response.Redirect("FrmTrClassDetailAss.aspx")
            txtKodeKategori.ReadOnly = True
            lblPopUpCourse.Enabled = False
        ElseIf e.CommandName = "Delete" Then
            deleteTrClass(e.Item.Cells(0).Text)
        ElseIf e.CommandName = "DownloadMateri" Then
            DownloadMateri(CInt(e.Item.Cells(0).Text))
        ElseIf e.CommandName = "AddAlloc" Then
            If SendEmail(CInt(e.Item.Cells(0).Text)) = 1 Then
                CType(e.Item.FindControl("lbtnAddAllocation"), LinkButton).Enabled = False
                MessageBox.Show("Kirim Email Sukses")
            Else
                MessageBox.Show("Kirim Email Gagal")
            End If
        ElseIf e.CommandName = "addsiswa" Then
            Dim classObj As TrClass = New TrClassFacade(User).Retrieve(CInt(e.Item.Cells(0).Text))
            Response.Redirect("FrmDaftarTrainingMKS.aspx?classcode=" + classObj.ClassCode + "&isClass=1")
        End If
    End Sub
    Private Function hitungSisa(ByVal id As Integer) As Integer
        Dim arlist As ArrayList
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrClass.ID", MatchType.Exact, id))
        ' Modified by Ikhsan, 20081212
        ' Requested by Rina, as Part of CR
        ' Only Active Trainee can attend the class
        ' Start -----
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrTrainee.Status", MatchType.Exact, "1"))
        ' End -------
        arlist = New TrClassRegistrationFacade(User).Retrieve(criterias)
        Return arlist.Count - 1
    End Function

    Private Function hitungAllocated(ByVal id As Integer) As Integer
        Dim objCAFac As TrClassAllocationFacade = New TrClassAllocationFacade(User)
        Dim arlCA As ArrayList = New ArrayList
        Dim crtCA As CriteriaComposite
        Dim Tot As Integer = 0

        crtCA = New CriteriaComposite(New Criteria(GetType(TrClassAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtCA.opAnd(New Criteria(GetType(TrClassAllocation), "TrClass.ID", id))
        Dim aggCA As Aggregate = New Aggregate(GetType(TrClassAllocation), "Allocated", AggregateType.Sum)
        Tot = objCAFac.GetAggregateResult(aggCA, crtCA)
        Return Tot
    End Function

    Private Function JumlahTerdaftar(ByVal id As Integer) As Integer
        Dim objCAFac As TrBookingCourseFacade = New TrBookingCourseFacade(User)
        Dim arlCA As ArrayList = New ArrayList
        Dim crtCA As CriteriaComposite
        Dim Tot As Integer = 0

        crtCA = New CriteriaComposite(New Criteria(GetType(TrBookingCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtCA.opAnd(New Criteria(GetType(TrBookingCourse), "TrClassRegistration.TrClass.ID", id))
        Dim aggCA As Aggregate = New Aggregate(GetType(TrBookingCourse), "ID", AggregateType.Count)
        Tot = objCAFac.GetAggregateResult(aggCA, crtCA)
        Return Tot
    End Function

    Private Sub dtgTrClass_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgTrClass.ItemDataBound
        Dim ItemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        Dim Selisih As Integer
        Dim RowValue As TrClass
        If Not e.Item.DataItem Is Nothing Then
            e.Item.Cells(1).Text = e.Item.ItemIndex + 1 + (dtgTrClass.CurrentPageIndex * dtgTrClass.PageSize)
            'e.Item.DataItem.GetType().ToString()
            RowValue = CType(e.Item.DataItem, TrClass)
            '--get rest of class capacity
            'remarks by anh 20111214
            'Dim RetHit As Integer = hitungSisa(RowValue.ID) + 1
            Dim RetHit As Integer = JumlahTerdaftar(RowValue.ID)
            Selisih = CInt(RowValue.Capacity) - RetHit
            Dim lblSelisih As Label = CType(e.Item.FindControl("lblSelisih"), Label)
            lblSelisih.Text = Selisih.ToString().Trim
            '-- status
            Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
            If RowValue.Status = 0 Then
                lblStatus.Text = New EnumTrDataStatus().StatusByIndex(EnumTrDataStatus.DataStatusType.Deactive)
            ElseIf RowValue.Status = 1 Then
                lblStatus.Text = New EnumTrDataStatus().StatusByIndex(EnumTrDataStatus.DataStatusType.Active)
            End If

            Dim lblTipeClass As Label = CType(e.Item.FindControl("lblTipeKelas"), Label)
            If Not String.IsNullOrEmpty(RowValue.ClassType) Then
                lblTipeClass.Text = EnumTrClass.GetStringValueClassType(RowValue.ClassType)
                'If EnumTrClass.EnumClassType.INHOUSE_TRAINING = CType(RowValue.ClassType, Short) Then
                '    lblTipeClass.Text = "Inhouse Training"
                'Else
                '    lblTipeClass.Text = "E-Learning"
                'End If
            End If

            Dim lnkMateri As LinkButton = CType(e.Item.FindControl("lnkMateri"), LinkButton)

            If Not String.IsNullOrEmpty(RowValue.FilePath) Then
                lnkMateri.Visible = True
            End If

            Dim lbtnAdd As LinkButton = CType(e.Item.FindControl("lbtnAdd"), LinkButton)

            If Me.IsDealerTitle(EnumDealerTittle.DealerTittle.KTB) Then
                lbtnAdd.Visible = True
            End If


            boundDtgTrClass(e)
            If Not e.Item.FindControl("btnUbah") Is Nothing Then
                CType(e.Item.FindControl("btnUbah"), LinkButton).Visible = helpers.IsEdit
            End If

            If Not e.Item.FindControl("btnHapus") Is Nothing Then
                CType(e.Item.FindControl("btnHapus"), LinkButton).Visible = helpers.IsEdit
            End If

            If bPrivilegeAlocationClass Then
                If Not IsNothing(sHCourse.GetSession("DEALER")) Then
                    'If CType(sHCourse.GetSession("DEALER"), Dealer).Title = EnumDealerTittle.DealerTittle.DEALER Then
                    If Not e.Item.FindControl("lbtnAddAllocation") Is Nothing Then
                        If Selisih > 0 AndAlso RowValue.FinishDate > DateTime.Now Then
                            CType(e.Item.FindControl("lbtnAddAllocation"), LinkButton).Visible = True
                        Else
                            CType(e.Item.FindControl("lbtnAddAllocation"), LinkButton).Visible = False
                        End If
                    End If
                End If
            Else
                If Not e.Item.FindControl("lbtnAddAllocation") Is Nothing Then
                    CType(e.Item.FindControl("lbtnAddAllocation"), LinkButton).Visible = bPrivilegeAlocationClass
                End If
            End If

            Dim btnUbah As LinkButton = CType(e.Item.FindControl("btnUbah"), LinkButton)
            Dim btnHapus As LinkButton = CType(e.Item.FindControl("btnHapus"), LinkButton)

            If RowValue.CreatedBy.ToLower = "migration" Then
                btnUbah.Visible = False
                btnHapus.Visible = False
            End If

        End If


    End Sub

    Private Sub dtgTrClass_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgTrClass.PageIndexChanged
        dtgTrClass.SelectedIndex = -1
        dtgTrClass.CurrentPageIndex = e.NewPageIndex
        bindDataGrid(dtgTrClass.CurrentPageIndex)
    End Sub

    Private Sub dtgTrClass_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgTrClass.SortCommand
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
        dtgTrClass.SelectedIndex = -1
        dtgTrClass.CurrentPageIndex = 0
        bindDataGrid(dtgTrClass.CurrentPageIndex)
    End Sub

    Private Sub btnBaru_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBaru.Click
        sHCourse.SetSession("clear", 1)
        Response.Redirect("FrmTrClassDetailAss.aspx")

    End Sub

    Private Sub btnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCari.Click
        dtgUpload.DataSource = Nothing
        dtgUpload.Visible = False
        dtgTrClass.Visible = True
        dtgTrClass.CurrentPageIndex = 0
        bindDataGrid(0)
        btnDownload.Visible = True
        If dtgTrClass.Items.Count = 0 Then
            btnDownload.Visible = False
        Else
            btnDownload.Visible = True
        End If
    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        Response.Redirect("./FrmDownoad.aspx")
    End Sub

#End Region


    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        sHCourse.RemoveSession("searchRes") '
        sHCourse.RemoveSession("backRes") '

        dtgTrClass.DataSource = Nothing
        dtgTrClass.Visible = False

        dtgUpload.Visible = True
        setUploadControl(True)
        ReadUploadFile()
        'UploadClass()
        btnDownload.Enabled = False
    End Sub

    Private Sub setUploadControl(ByVal IsUploading As Boolean)

        If IsUploading Then
            sHCourse.SetSession("Upload.IsProcessing", True)
        Else
            sHCourse.SetSession("Upload.IsProcessing", False)
        End If
        btnCari.Enabled = Not IsUploading
        btnBaru.Enabled = Not IsUploading
        btnSimpan.Enabled = IsUploading
        btnBatal.Enabled = IsUploading
    End Sub

    Private Sub UploadClass()

        If (fileUpload.PostedFile Is Nothing) OrElse fileUpload.PostedFile.ContentLength <= 0 Then
            MessageBox.Show("Pilih file yang akan di-upload.")
            Return
        Else
            If fileUpload.PostedFile.ContentType.ToString <> "application/vnd.ms-excel" And _
            fileUpload.PostedFile.ContentType.ToString <> "application/octet-stream" Then

                MessageBox.Show("Bukan file EXCEL. File anda " & fileUpload.PostedFile.ContentType)
                Return

            Else
                'cek maxFileSize First
                Dim maxFileSize As Integer = CInt(KTB.DNet.Lib.WebConfig.GetValue("MaximumFileSize"))

                If fileUpload.PostedFile.ContentLength > maxFileSize Then
                    MessageBox.Show("Ukuran file tidak boleh melebihi " & maxFileSize & " bytes")
                    Exit Sub
                Else
                    Dim filename As String = System.IO.Path.GetFileName(fileUpload.PostedFile.FileName)
                    Dim targetFile As String = Server.MapPath("") & "\..\DataTemp\" & filename
                    Dim arlParseResult As New ArrayList

                    If IsFileExist(targetFile) Then
                        MessageBox.Show("File exist")
                        Return
                    End If


                    Dim _user As String
                    _user = KTB.DNet.Lib.WebConfig.GetValue("User")
                    Dim _password As String
                    _password = KTB.DNet.Lib.WebConfig.GetValue("Password")
                    Dim _webServer As String
                    _webServer = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
                    Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
                    Dim sapImp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

                    sapImp.Start()

                    Try
                        SavingToFolder(targetFile, fileUpload.PostedFile)
                        '-- Declare & instantiate parser
                        Dim parser As IExcelParser = New UploadClassParser

                        '-- Parse data file and store result into arraylist
                        Dim arlCA As ArrayList = CType(parser.ParseExcelNoTransaction(targetFile, "[Sheet1$]", "User"), ArrayList)

                        sHCourse.SetSession("arlCA", arlCA)
                        dtgUpload.DataSource = arlCA
                        dtgUpload.DataBind()
                        dtgUpload.Columns(12).Visible = False
                        btnUpload.Enabled = True
                    Catch ex As Exception
                        Throw
                    Finally
                        sapImp.StopImpersonate()
                    End Try
                End If
            End If
        End If
    End Sub

    Private Sub SavingToFolder(ByVal targetFile As String, ByVal postedFile As HttpPostedFile)
        Try
            postedFile.SaveAs(targetFile)
            Dim trgInfo As System.IO.FileInfo = New System.IO.FileInfo(targetFile)
            If Not trgInfo.Exists Then
                Throw New IO.IOException("File gagal disimpan di Server. Harap hubungi Administrator")
            End If
        Catch ex As IO.IOException
            Throw
        Catch
            Throw New Exception(SR.UploadFail(System.IO.Path.GetFileName(targetFile)))
        End Try
    End Sub

    Private Function IsFileExist(ByVal filename As String) As Boolean
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")

        Dim sapImp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        sapImp.Start()

        Try
            Dim fInfo As System.IO.FileInfo = New System.IO.FileInfo(filename)
            If fInfo.Exists Then
                fInfo.Delete()
                Return False
            End If

        Catch ex As IO.IOException
            Throw
        Catch
            Throw New Exception(SR.UploadFail(System.IO.Path.GetFileName(filename)))
        Finally
            sapImp.StopImpersonate()
        End Try
    End Function

    Private Sub dtgUpload_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgUpload.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.SelectedItem Then
            Dim objCFac As TrClassFacade = New TrClassFacade(User)
            Dim objC As TrClass
            Dim arlCA As ArrayList = New ArrayList
            Dim strError As String = String.Empty

            Dim lblUNo As Label = e.Item.FindControl("lblUNo")
            Dim lblUClassCode As Label = e.Item.FindControl("lblUClassCode")
            Dim lblUClassName As Label = e.Item.FindControl("lblUClassName")
            Dim lblUCategory As Label = e.Item.FindControl("lblUCategory")
            Dim lblULocation As Label = e.Item.FindControl("lblULocation")
            Dim lblUPenginapan As Label = e.Item.FindControl("lblUPenginapan")
            Dim lblUTrainer1 As Label = e.Item.FindControl("lblUTrainer1")
            Dim lblUStartDate As Label = e.Item.FindControl("lblUStartDate")
            Dim lblUEndDate As Label = e.Item.FindControl("lblUEndDate")
            Dim lblUCapacity As Label = e.Item.FindControl("lblUCapacity")
            Dim lblURemain As Label = e.Item.FindControl("lblURemain")
            Dim lblUMessage As Label = e.Item.FindControl("lblUMessage")
            Dim lblUTipeKelas As Label = e.Item.FindControl("lblUTipeKelas")


            Dim i As Integer = 0

            lblUNo.Text = e.Item.ItemIndex + 1
            objC = CType(e.Item.DataItem, TrClass)

            'trClass Data
            If objC Is Nothing Then
                lblUClassCode.Text = "-"
                lblUClassName.Text = "-"
                lblUCategory.Text = "-"
                lblULocation.Text = "-"
                lblUTrainer1.Text = "-"
                lblUStartDate.Text = "-"
                lblUEndDate.Text = "-"
                lblUCapacity.Text = "-"
                lblURemain.Text = "-"
                lblUMessage.Text = "Data kosong"
                If objC.ErrorMessage <> "" Then
                    e.Item.BackColor = System.Drawing.Color.Red
                End If
            Else
                lblUClassCode.Text = objC.ClassCode
                lblUClassName.Text = objC.ClassName
                If Not IsNothing(objC.TrCourse) Then
                    lblUCategory.Text = objC.TrCourse.CourseCode
                Else
                    lblUCategory.Text = String.Empty
                End If
                lblUPenginapan.Text = objC.Lodging
                lblULocation.Text = objC.Location
                lblUTrainer1.Text = objC.Trainer1
                lblUStartDate.Text = objC.StartDate
                lblUEndDate.Text = objC.FinishDate
                lblUCapacity.Text = objC.Capacity
                lblURemain.Text = objC.Capacity
                lblUMessage.Text = objC.ErrorMessage

                If objC.ErrorMessage <> "" Then
                    e.Item.BackColor = System.Drawing.Color.Red
                End If

            

            End If

            If lblUMessage.Text.Trim <> String.Empty Then
                Me.btnSimpan.Enabled = False
            End If
            If Not objC.ClassType.Equals(0) Then
                lblUTipeKelas.Text = EnumTrClass.GetStringValueClassType(objC.ClassType.ToString())
            End If

        End If
    End Sub

    Private Sub dtgUpload_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgUpload.ItemCommand
        If e.CommandName = "Delete" Then
            deleteTrClassUpload(e.Item.Cells(0).Text)
        End If
    End Sub

    Private Sub DownloadMateri(ByVal classID As Integer)

        Try
            Dim classObj As TrClass = New TrClassFacade(User).Retrieve(classID)
            helpers.DownloadFile(classObj.FilePath, "Materi " & classObj.ClassCode)
        Catch ex As Exception
            MessageBox.Show("Data materi tidak ditemukan")
        End Try

    End Sub

    Private Sub btnReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReport.Click
        If Not ddlJobPosition.IsSelected Then
            MessageBox.Show("Pilih Kategori Posisi")
            Return
        End If
        If Not ddlTahunFiscal.IsSelected Then
            MessageBox.Show("Pilih Tahun Fiskal")
            Return
        End If
        GenerateReport()
    End Sub

    Private Sub GenerateReport()
        Dim tahun1 As String = ddlTahunFiscal.SelectedValue.Split("/")(0)
        Dim tahun2 As String = (CInt(tahun1) + 1).ToString()

        Dim listBulan As New Dictionary(Of Integer, String)
        listBulan.Add(4, "April " + tahun1)
        listBulan.Add(5, "Mei " + tahun1)
        listBulan.Add(6, "Juni " + tahun1)
        listBulan.Add(7, "Juli " + tahun1)
        listBulan.Add(8, "Agustus " + tahun1)
        listBulan.Add(9, "September " + tahun1)
        listBulan.Add(10, "Oktober " + tahun1)
        listBulan.Add(11, "November " + tahun1)
        listBulan.Add(12, "Desember " + tahun1)
        listBulan.Add(1, "Januari " + tahun2)
        listBulan.Add(2, "Februari " + tahun2)
        listBulan.Add(3, "Maret " + tahun2)

        Dim dataKelas As List(Of TrClass) = GetDataKelas()
        If Not dataKelas.IsItems Then
            MessageBox.Show(SR.DataNotFound("Kelas"))
            Return
        End If

        Using excelPackage As New ExcelPackage()
            Dim wsData As ExcelWorksheet = excelPackage.Workbook.Worksheets.Add("Laporan Kelas Training")
            Dim rowIdx As Integer = 1
            wsData.Cells("A" & rowIdx.ToString()).ValueBold("Laporan Data Training " + ddlTahunFiscal.SelectedValue)
            rowIdx += 1
            wsData.Cells("A" & rowIdx.ToString()).ValueBold("Kategori " + ddlJobPosition.SelectedItem.Text)
            rowIdx += 1
            rowIdx += 1

            For Each itemBulan As KeyValuePair(Of Integer, String) In listBulan
                Dim dataBulanan As List(Of TrClass) = dataKelas.Where(Function(x) x.StartDate.Month = itemBulan.Key).ToList()
                If Not dataBulanan.IsItems Then
                    Continue For
                End If
                Dim noUrut As Integer = 1
                Dim jumlahSiswaLulus As Integer = 0
                Dim jumlahPeserta As Integer = 0
                wsData.Cells("A" & rowIdx.ToString()).ValueBold(String.Format("Bulan {0}", itemBulan.Value))
                rowIdx += 1
                CreateHeaderColumn(wsData, rowIdx)
                rowIdx += 1
                For Each itemKelas As TrClass In dataBulanan
                    Dim clmidx As Integer = 1
                    wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(noUrut.ToString(), Style.ExcelHorizontalAlignment.Center)
                    clmidx += 1
                    wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(itemKelas.ClassCode.ToUpper, Style.ExcelHorizontalAlignment.Left)
                    clmidx += 1
                    wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(itemKelas.ClassName, Style.ExcelHorizontalAlignment.Left)
                    clmidx += 1
                    wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(itemKelas.StartDate.DateToString, Style.ExcelHorizontalAlignment.Center)
                    clmidx += 1
                    wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(itemKelas.FinishDate.DateToString, Style.ExcelHorizontalAlignment.Center)
                    clmidx += 1
                    wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue((DateDiff(DateInterval.Day, itemKelas.StartDate, itemKelas.FinishDate) + 1 _
                                 ).ToString(), Style.ExcelHorizontalAlignment.Right)
                    clmidx += 1
                    wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(itemKelas.Capacity.ToString(), Style.ExcelHorizontalAlignment.Right)
                    clmidx += 1
                    wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(itemKelas.TrClassRegistrations.Count.ToString(), Style.ExcelHorizontalAlignment.Right)
                    clmidx += 1
                    wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(TotalTraineePassed(itemKelas.ID).ToString(), Style.ExcelHorizontalAlignment.Right)
                    clmidx += 1
                    wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(itemKelas.TrMRTC.Name, Style.ExcelHorizontalAlignment.Left)
                    clmidx += 1
                    rowIdx += 1
                    noUrut += 1
                    jumlahPeserta += itemKelas.TrClassRegistrations.Count
                    jumlahSiswaLulus += TotalTraineePassed(itemKelas.ID)
                Next
                wsData.Cells("C" & rowIdx.ToString).ValueBold("Jumlah Siswa")
                wsData.Cells("H" & rowIdx.ToString).SetValue(jumlahPeserta.ToString, Style.ExcelHorizontalAlignment.Right)
                wsData.Cells("I" & rowIdx.ToString).SetValue(jumlahSiswaLulus.ToString, Style.ExcelHorizontalAlignment.Right)
                rowIdx += 1
                rowIdx += 1
            Next
            For colIdx As Integer = 2 To 10
                wsData.Column(colIdx).AutoFit()
            Next

            Dim fileBytes = excelPackage.GetAsByteArray()
            Dim fileName As String = String.Format("LaporanDataTraining_{0}.xls", ddlTahunFiscal.SelectedValue.Split("/")(0))

            Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
            Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
            Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
            Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

            Dim success As Boolean = False

            Try
                success = imp.Start()
                If success Then
                    File.WriteAllBytes(Server.MapPath("~/DataTemp/" & fileName), fileBytes)
                    imp.StopImpersonate()
                End If

            Catch ex As Exception
                Exit Sub

            End Try

            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & fileName)
        End Using

    End Sub

    Private Sub CreateHeaderColumn(ByVal wsData As ExcelWorksheet, ByVal rowIdx As Integer)
        Dim columnIdx As Integer = 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("No")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Kode Kelas")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Nama Kelas")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Tanggal Mulai")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Tanggal Selesai")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Jumla hari")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Kapasitas")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Peserta")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Lulus")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("MRTC")
        columnIdx += 1
    End Sub

    Private Function GetDataKelas() As List(Of TrClass)
        Dim dataKelas As New List(Of TrClass)
        Dim criterias As CriteriaComposite

        criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If Not txtKodeKategori.IsEmpty() Then
            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourse), "CourseCode", MatchType.Exact, txtKodeKategori.Text))

            Dim arTrCourse As ArrayList = New TrCourseFacade(User).Retrieve(criterias2)
            If arTrCourse.Count > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "TrCourse.ID", MatchType.Exact, CType(arTrCourse(0), TrCourse).ID))
            End If
        End If

        If Not txtClassCode.IsEmpty() Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "ClassCode", MatchType.[Partial], txtClassCode.Text.Trim))
        End If

        If ddlJobPosition.IsSelected Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "TrCourse.JobPositionCategory.ID", MatchType.Exact, ddlJobPosition.SelectedValue))
        End If

        If ddlTahunFiscal.IsSelected Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "FiscalYear", MatchType.Exact, ddlTahunFiscal.SelectedValue))
        End If

        If txtLocation.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "Location", MatchType.[Partial], txtLocation.Text))
        End If

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(KTB.DNet.Domain.TrClass), "StartDate", Sort.SortDirection.ASC))
        sortColl.Add(New Sort(GetType(KTB.DNet.Domain.TrClass), "FinishDate", Sort.SortDirection.ASC))

        Dim arlData As ArrayList = New TrClassFacade(User).Retrieve(criterias, sortColl)
        If arlData.IsItems Then
            dataKelas = arlData.Cast(Of TrClass).ToList()
        End If
        Return dataKelas
    End Function

    Private Function GetData(ByVal strPeriod As String) As ArrayList
        Dim objTrCourse As TrCourse
        Dim arTrCourse, artrClass As ArrayList
        Dim startDate, finishDate As DateTime
        Dim criterias As CriteriaComposite

        criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If Not txtKodeKategori.IsEmpty() Then
            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourse), "CourseCode", MatchType.Exact, txtKodeKategori.Text))

            arTrCourse = New TrCourseFacade(User).Retrieve(criterias2)

            If arTrCourse.Count > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "TrCourse.ID", MatchType.Exact, CType(arTrCourse(0), TrCourse).ID))
            End If
        End If

        If Not txtClassCode.IsEmpty() Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "ClassCode", MatchType.[Partial], txtClassCode.Text.Trim))
        End If

        If txtPeriod.Text.Length = 4 Then
            startDate = New DateTime(txtPeriod.Text, 12, Date.DaysInMonth(txtPeriod.Text, 12), 23, 59, 59)
            finishDate = New DateTime(txtPeriod.Text, 1, 1, 0, 0, 0)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "StartDate", MatchType.LesserOrEqual, startDate))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "StartDate", MatchType.GreaterOrEqual, finishDate))
        End If
        If txtLocation.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "Location", MatchType.[Partial], txtLocation.Text))
        End If

        Dim sortColl As SortCollection = New SortCollection
        sortColl.Add(New Sort(GetType(KTB.DNet.Domain.TrClass), "StartDate", Sort.SortDirection.ASC))
        sortColl.Add(New Sort(GetType(KTB.DNet.Domain.TrClass), "FinishDate", Sort.SortDirection.ASC))

        Dim arlData As ArrayList = New TrClassFacade(User).Retrieve(criterias, sortColl)
        Return arlData
    End Function

    Private Sub DownloadReport(ByVal strPeriod As String, ByVal arlReport As ArrayList)
        Dim sFileName As String
        sFileName = "Rekap Pendaftaran [" & strPeriod & "]"

        '-- Temp file must be a randomly named file!
        Dim SPKData As String = Server.MapPath("") & "\..\DataTemp\" & sFileName & ".xls"

        '-- Impersonation to manipulate file in server
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer")
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Try
            If imp.Start() Then

                Dim finfo As FileInfo = New FileInfo(SPKData)
                If finfo.Exists Then
                    finfo.Delete()  '-- Delete temp file if exists
                End If

                '-- Create file stream
                Dim fs As FileStream = New FileStream(SPKData, FileMode.CreateNew)
                '-- Create stream writer
                Dim sw As StreamWriter = New StreamWriter(fs)

                '-- Write data to temp file
                Write2ExcellReport(sw, arlReport, strPeriod)
                sw.Close()
                fs.Close()

                imp.StopImpersonate()
                imp = Nothing

            End If

            '-- Download invoice data to client!
            Response.Redirect("../downloadlocal.aspx?file=DataTemp\" & sFileName & ".xls", True)

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub Write2ExcellReport(ByVal sw As StreamWriter, ByVal dataReport As ArrayList, ByVal strPeriod As String)

        Dim tab As Char = Chr(9)  '-- Separator character <Tab>
        Dim itemLine As StringBuilder = New StringBuilder
        itemLine.Remove(0, itemLine.Length)  '-- Empty line
        itemLine.Append("DATA JUMLAH SISWA MSTEP & GENERAL COURSE " & strPeriod)
        sw.WriteLine(itemLine)
        itemLine.Remove(0, itemLine.Length)
        itemLine.Append(" " & tab & tab)
        sw.WriteLine(itemLine)
        sw.WriteLine(" ")


        '======SPK DETAIL=======
        If (dataReport.Count > 0) Then
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append(" " & tab)
            sw.WriteLine(itemLine)
            itemLine.Remove(0, itemLine.Length)
            itemLine.Append("NO" & tab)
            itemLine.Append("KODE COURSE" & tab)
            itemLine.Append("NAMA COURSE" & tab)
            itemLine.Append("START" & tab)
            itemLine.Append("FINISH" & tab)
            itemLine.Append("JML HARI" & tab)
            itemLine.Append("KAPASITAS" & tab)
            itemLine.Append("TRAINEE" & tab)
            itemLine.Append("LULUS" & tab)
            itemLine.Append("LOKASI" & tab)
            sw.WriteLine(itemLine.ToString())

            Dim i As Integer = 0
            Try
                Dim iMonth As Integer = 0
                Dim iTrainee As Integer = 0
                Dim iTraineePass As Integer = 0
                For Each item As TrClass In dataReport
                    If item.StartDate.Month > iMonth And iMonth > 0 Then
                        itemLine.Remove(0, itemLine.Length)
                        itemLine.Append("" & tab)
                        itemLine.Append("" & tab)
                        itemLine.Append("JML TRAINEE" & tab)
                        itemLine.Append("" & tab)
                        itemLine.Append("" & tab)
                        itemLine.Append("" & tab)
                        itemLine.Append("" & tab)
                        itemLine.Append(iTrainee & tab)
                        itemLine.Append(iTraineePass & tab)
                        itemLine.Append("" & tab)
                        sw.WriteLine(itemLine)

                        itemLine.Remove(0, itemLine.Length)
                        itemLine.Append(" " & tab)
                        sw.WriteLine(itemLine)

                        itemLine.Remove(0, itemLine.Length)
                        itemLine.Append("NO" & tab)
                        itemLine.Append("KODE COURSE" & tab)
                        itemLine.Append("NAMA COURSE" & tab)
                        itemLine.Append("START" & tab)
                        itemLine.Append("FINISH" & tab)
                        itemLine.Append("JML HARI" & tab)
                        itemLine.Append("KAPASITAS" & tab)
                        itemLine.Append("TRAINEE" & tab)
                        itemLine.Append("LULUS" & tab)
                        itemLine.Append("LOKASI" & tab)
                        sw.WriteLine(itemLine.ToString())

                        i = 0
                        iTrainee = 0
                        iTraineePass = 0
                    End If
                    i = i + 1
                    itemLine.Remove(0, itemLine.Length)
                    itemLine.Append(i.ToString & tab)
                    itemLine.Append(item.ClassCode & tab)
                    itemLine.Append(item.ClassName & tab)
                    itemLine.Append(item.StartDate.ToString("dd-MMM-yyyy") & tab)
                    itemLine.Append(item.FinishDate.ToString("dd-MMM-yyyy") & tab)
                    itemLine.Append(DateDiff(DateInterval.Day, item.StartDate, item.FinishDate) + 1 & tab)
                    itemLine.Append(item.Capacity & tab) 'kapasitas
                    itemLine.Append(item.TrClassRegistrations.Count & tab) 'trainee
                    Dim iPass As Integer = TotalTraineePassed(item.ID)
                    itemLine.Append(iPass & tab) 'lulus
                    itemLine.Append(item.Location & tab)
                    sw.WriteLine(itemLine.ToString())

                    iMonth = item.StartDate.Month
                    iTrainee = iTrainee + item.TrClassRegistrations.Count
                    iTraineePass = iTraineePass + iPass
                Next
                If dataReport.Count > 0 Then
                    itemLine.Remove(0, itemLine.Length)
                    itemLine.Append("" & tab)
                    itemLine.Append("" & tab)
                    itemLine.Append("JML TRAINEE" & tab)
                    itemLine.Append("" & tab)
                    itemLine.Append("" & tab)
                    itemLine.Append("" & tab)
                    itemLine.Append("" & tab)
                    itemLine.Append(iTrainee & tab)
                    itemLine.Append(iTraineePass & tab)
                    itemLine.Append("" & tab)
                    sw.WriteLine(itemLine)

                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try

        End If

        itemLine.Remove(0, itemLine.Length)
        itemLine.Append(" " & tab & tab)
        sw.WriteLine(itemLine)

    End Sub

    Private Function TotalTraineePassed(ByVal ClassID As Integer) As Integer
        Dim objCAFac As TrClassRegistrationFacade = New TrClassRegistrationFacade(User)
        Dim arlCA As ArrayList = New ArrayList
        Dim crtCA As CriteriaComposite

        crtCA = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtCA.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.ID", ClassID))
        crtCA.opAnd(New Criteria(GetType(TrClassRegistration), "Status", 1)) '1 -- passed
        arlCA = objCAFac.Retrieve(crtCA)

        Return arlCA.Count
    End Function

    Private Sub linkTemplate_Click(sender As Object, e As EventArgs) Handles linkTemplate.Click
        Dim template As ExcelTemplate = New ExcelTemplate(Me.Page)
        template.FileName = "TemplateUploadClass.xls"
        template.SheetName = "UploadKelas"
        template.Judul = "Upload Kelas Training"
        template.AddField(1, "No")
        template.AddField(2, "Kode Kategori")
        template.AddField(3, "Nama")
        template.AddField(6, "Kota")
        template.AddField(7, "Nama Lokasi")
        template.AddField(8, "Alamat")
        template.AddField(9, "Penginapan")
        template.AddField(10, "Pengajar 1")
        template.AddField(11, "Pengajar 2")
        template.AddField(12, "Pengajar 3")
        template.AddField(13, "Kapasitas")
        template.AddField(16, "Keterangan")
        template.AddField(17, "Tanggal Mulai", EnumTypeCell.FormatDate)
        template.AddField(18, "Tanggal Selesai", EnumTypeCell.FormatDate)
        template.AddField(5, "Kode MRTC")

        Dim dataTipe As ExcelTemplateColumn = New ExcelTemplateColumn(4, "Tipe kelas", EnumTypeCell.Dropdownlist)
        Dim list As List(Of String) = New List(Of String)
        list.Add("INCLASS TRAINING")
        list.Add("INHOUSE TRAINING")
        list.Add("E-LEARNING")
        list.Add("FLEET_TRAINING")
        dataTipe.DataValidation = list
        template.AddField(dataTipe)

        Dim dataTahunfiskal As ExcelTemplateColumn = New ExcelTemplateColumn(14, "Tahun Fiskal", EnumTypeCell.Dropdownlist)
        Dim listTahunfiskal As List(Of String) = GetTahunFiskal()
        dataTahunfiskal.DataValidation = listTahunfiskal
        template.AddField(dataTahunfiskal)

        Dim dataStatus As ExcelTemplateColumn = New ExcelTemplateColumn(15, "Status", EnumTypeCell.Dropdownlist)
        Dim listStatus As List(Of String) = New List(Of String)
        listStatus.Add("Aktif")
        listStatus.Add("Tidak Aktif")
        dataStatus.DataValidation = listStatus
        template.AddField(dataStatus)

        Dim dataKota As ExcelSheetData = New ExcelSheetData()
        dataKota.SheetName = "Data Kota"
        dataKota.Judul = "Data Kota"
        dataKota.ColumnCode = "Kode Kota"
        dataKota.ColumnName = "Nama Kota"
        Dim dicKota As Dictionary(Of String, String) = New Dictionary(Of String, String)
        Dim dataKotas As ArrayList = New CityFacade(User).RetrieveActiveList()
        For Each item As City In dataKotas
            dicKota.Add(item.CityCode, item.CityName)
        Next
        dataKota.AddData(dicKota)
        template.AddSheet(dataKota)
        template.DownLoad()

    End Sub

    Private Function GetTahunFiskal() As List(Of String)
        Dim GetTahun As Integer = DateTime.Now.Year
        Dim result As List(Of String) = New List(Of String)

        'Before
        For x As Integer = 4 To 0 Step -1
            Dim value1 As String = (GetTahun - x).ToString()
            Dim value2 As String = (GetTahun - x - 1).ToString()
            Dim value As String = String.Format("{0}/{1}", value2, value1)
            result.Add(value)
        Next
        'After
        For x As Integer = 0 To 4
            Dim value1 As String = (GetTahun + x).ToString()
            Dim value2 As String = (GetTahun + x + 1).ToString()
            Dim value As String = String.Format("{0}/{1}", value1, value2)
            result.Add(value)
        Next
        Return result
    End Function

    Private Sub ReadUploadFile()
        Dim fileName As String = String.Empty
        Dim listTrClass As List(Of TrClass) = New List(Of TrClass)
        Dim listError As List(Of ErrorExcelUpload) = New List(Of ErrorExcelUpload)
        Dim resultUpload As String = helpers.UploadFile(fileUpload, KTB.DNet.Lib.WebConfig.GetValue("ClassUpload"), _
                                                    KTB.DNet.Lib.WebConfig.GetValue("MaximumClassUploadSize"))
        Dim errArr() As String = resultUpload.Split("|")
        If errArr(0).Equals("Error") Then
            MessageBox.Show(errArr(1))
            Return
        Else
            fileName = KTB.DNet.Lib.WebConfig.GetValue("SAN") & errArr(1)
        End If
        Dim fileInfo As FileInfo = New FileInfo(fileName)
        Dim limitError As Integer = 0
        Using excelPkg As New ExcelPackage(fileInfo)
            Using ws As ExcelWorksheet = excelPkg.Workbook.Worksheets.Item(1)
                Dim ColumnCount As Integer = ws.Dimension.End.Column
                Dim RowCount As Integer = ws.Dimension.End.Row
                If ColumnCount < 16 Then
                    MessageBox.Show("Format Tidak Sesuai")
                    Exit Sub
                End If

                Dim DataTipeClass As Dictionary(Of String, String) = New Dictionary(Of String, String)
                DataTipeClass.Add("1", "INCLASS TRAINING")
                DataTipeClass.Add("2", "E-LEARNING")
                'DataTipeClass.Add("3", "INHOUSE TRAINING")
                'DataTipeClass.Add("4", "FLEET_TRAINING")

                Dim listCodeKota As List(Of String) = (New CityFacade(User).RetrieveActiveList().Cast(Of City)( _
                                                       )).Select(Function(x) x.CityCode).ToList()
                Dim listTahunFiskal As List(Of String) = GetTahunFiskal()
                Dim listTraining As List(Of String) = New List(Of String)
                Dim listTrainingNum As List(Of Integer) = New List(Of Integer)

                For idx As Integer = 4 To RowCount

                    If limitError = 3 Then
                        Exit For
                    End If

                    Dim validasi As ExcelValidation = New ExcelValidation(ws)
                    Dim kodeTraining As ExcelField = validasi.Create("Kode Kategori", idx, 2, "required,max", 20)
                    Dim Nama As ExcelField = validasi.Create("Nama", idx, 3, "required,max", 50)
                    Dim TipeKelas As ExcelField = validasi.Create("Tipe kelas", idx, 4, "required")
                    Dim KodeMRTC As ExcelField = validasi.Create("Kode MRTC", idx, 5, "required")
                    Dim Penginapan As ExcelField = validasi.Create("Alamat", idx, 9, "max", 200)

                    Dim Kota As New ExcelField
                    Dim NamaLokasi As New ExcelField
                    Dim Alamat As New ExcelField
                    Dim Pengajar1 As New ExcelField
                    Dim Pengajar2 As New ExcelField
                    Dim Pengajar3 As New ExcelField
                    Dim isMRTC As Boolean = False

                    Dim no As String = ws.Cells(idx, 1).Value
                    If no = String.Empty Then
                        limitError = limitError + 1
                    End If

                    If KodeMRTC.Value.IsNullorEmpty Then
                        Kota = validasi.Create("Kota", idx, 6, "required,max", 50)
                        NamaLokasi = validasi.Create("Nama Lokasi", idx, 7, "required,max", 100)
                        Alamat = validasi.Create("Alamat", idx, 8, "required,max", 200)
                        Pengajar1 = validasi.Create("Pengajar 1", idx, 10, "required,max", 50)
                        Pengajar2 = validasi.Create("Pengajar 2", idx, 11, "max", 50)
                        Pengajar3 = validasi.Create("Pengajar 3", idx, 12, "max", 50)
                    Else
                        isMRTC = True
                        Kota = validasi.Create("Kota", idx, 6, "max", 50)
                        NamaLokasi = validasi.Create("Nama Lokasi", idx, 7, "max", 100)
                        Alamat = validasi.Create("Alamat", idx, 8, "max", 200)
                        Pengajar1 = validasi.Create("Pengajar 1", idx, 10, "max", 50)
                        Pengajar2 = validasi.Create("Pengajar 2", idx, 11, "max", 50)
                        Pengajar3 = validasi.Create("Pengajar 3", idx, 12, "max", 50)
                    End If

                    Dim Kapasitas As ExcelField = validasi.Create("Kapasitas", idx, 13, "required,numeric")
                    Dim TahunFiscal As ExcelField = validasi.Create("Tahun Fiskal", idx, 14, "required")
                    Dim Status As ExcelField = validasi.Create("Status", idx, 15, "required")
                    Dim Keterangan As ExcelField = validasi.Create("Keterangan", idx, 16, "max", 200)
                    Dim TanggalMasuk As ExcelField = validasi.Create("Tanggal Masuk", idx, 17, "required,date")
                    Dim TanggalKeluar As ExcelField = validasi.Create("Tanggal Keluar", idx, 18, "required,date")

                    'Validasi Requirment Value
                    Dim listErrorfield As List(Of ErrorExcelUpload) = validasi.Validate()
                    If Not listErrorfield.Count.Equals(0) Then
                        listError.AddRange(listErrorfield)
                        Continue For
                    End If

                    If TanggalMasuk.Value.StringCellToDateTime() > TanggalKeluar.Value.StringCellToDateTime() Then
                        listErrorfield.Add(validasi.CreateCustomError(TanggalMasuk, "lebih besar dari Tanggal Keluar"))
                    End If


                    Dim tahun1 As Integer = CInt(TahunFiscal.Value.Split("/")(0))
                    Dim tahun2 As Integer = CInt(TahunFiscal.Value.Split("/")(1))
                    Dim fiskalYearStart As Date = New Date(tahun1, 4, 1)
                    Dim fiskalYearEnd As Date = New Date(tahun2, 3, 31)
                    If TanggalMasuk.Value.StringCellToDateTime() < fiskalYearStart Or TanggalMasuk.Value.StringCellToDateTime() > fiskalYearEnd Then
                        Dim strMEssage As String = String.Format("Untuk tahun fiskal {0}. Tanggal mulai tidak boleh lebih kecil dari {1} atau lebih besar dari {2}.", _
                                                                 TahunFiscal.Value, _
                                                                 fiskalYearStart.ToString("dd/MM/yyyy"), _
                                                                 fiskalYearEnd.ToString("dd/MM/yyyy"))
                        listErrorfield.Add(validasi.CreateCustomError(TanggalMasuk, strMEssage, False))
                    End If

                    If Not DataTipeClass.ContainsValue(TipeKelas.Value.ToUpper()) Then
                        listErrorfield.Add(validasi.CreateCustomError(TipeKelas, "harus berisi INHOUSE TRAINING, E-LEARNING atau FACE TO FACE"))
                    End If

                    If Not isMRTC Then
                        If Not listCodeKota.Contains(Kota.Value.ToUpper()) Then
                            listErrorfield.Add(validasi.CreateCustomError(Kota, "Kode Kota tidak ditemukan", False))
                        End If
                    End If

                    If Not listTahunFiskal.Contains(TahunFiscal.Value) Then
                        listErrorfield.Add(validasi.CreateCustomError(TahunFiscal, "Tahun fiskal sudah tidak valid atau format tidak benar", False))
                    End If

                    If Not (Status.Value.Contains("Aktif") Or Status.Value.Contains("Tidak Aktif")) Then
                        listErrorfield.Add(validasi.CreateCustomError(Status, "harus berisi Aktif atau Tidak Aktif"))
                    End If

                    Dim objCourse As TrCourse = New TrCourseFacade(User).Retrieve(kodeTraining.Value)
                    If objCourse.ID.Equals(0) Then
                        listErrorfield.Add(validasi.CreateCustomError(kodeTraining, "tidak ditemukan."))
                    Else
                        If objCourse.JobPositionCategory Is Nothing Then
                            listErrorfield.Add(validasi.CreateCustomError(kodeTraining, "tidak ditemukan."))
                        Else
                            If Not objCourse.JobPositionCategory.AreaID.Equals(2) Then
                                listErrorfield.Add(validasi.CreateCustomError(kodeTraining, "tidak ditemukan."))
                            End If
                        End If
                    End If

                    Dim objMRTC As New TrMRTC

                    If Not KodeMRTC.Value = String.Empty Then
                        objMRTC = New TrMRTCFacade(User).Retrieve(KodeMRTC.Value)
                        If objMRTC.ID = 0 Then
                            listErrorfield.Add(validasi.CreateCustomError(KodeMRTC, "tidak ditemukan."))
                        End If
                    End If

                    If Not listErrorfield.Count.Equals(0) Then
                        listError.AddRange(listErrorfield)
                        Continue For
                    End If

                    If listError.Count.Equals(0) Then
                        Dim objClass As TrClass = New TrClass()
                        objClass.TrCourse = objCourse
                        objClass.ClassName = Nama.Value
                        objClass.ClassType = CType(DataTipeClass.FirstOrDefault(Function(x) _
                                             x.Value = TipeKelas.Value.ToUpper()).Key, Short)

                        If objMRTC.ID <> 0 Then
                            objClass.TrMRTC = objMRTC
                            objClass.Location = objMRTC.Address
                            objClass.LocationName = objMRTC.Name
                            objClass.City = objMRTC.City

                            If objClass.TrCourse.PaymentType = CType(EnumTrCourse.PaymentType.CHARGE, Short) Then
                                objClass.PaidDay = 1
                                objClass.PricePerDay = objMRTC.PricePerDay
                                objClass.PriceTotal = CType(1 * objMRTC.PricePerDay, Decimal)
                            End If

                            For i As Integer = 0 To objMRTC.ListOfDetail.Count - 1
                                Dim pic As TrMRTCPIC = CType(objMRTC.ListOfDetail(i), TrMRTCPIC)

                                If i = 0 And String.IsNullorEmpty(Pengajar1.Value) Then
                                    objClass.Trainer1 = pic.TrTrainee.Name
                                ElseIf i = 0 And Not String.IsNullorEmpty(Pengajar1.Value) Then
                                    objClass.Trainer1 = Pengajar1.Value
                                End If

                                If i = 1 And String.IsNullorEmpty(Pengajar2.Value) Then
                                    objClass.Trainer2 = pic.TrTrainee.Name
                                ElseIf i = 1 And Not String.IsNullorEmpty(Pengajar2.Value) Then
                                    objClass.Trainer2 = Pengajar2.Value
                                End If

                                If i = 2 And String.IsNullorEmpty(Pengajar3.Value) Then
                                    objClass.Trainer3 = pic.TrTrainee.Name
                                ElseIf i = 2 And Not String.IsNullorEmpty(Pengajar3.Value) Then
                                    objClass.Trainer3 = Pengajar3.Value
                                End If

                            Next

                        Else
                            objClass.Trainer1 = Pengajar1.Value
                            objClass.Trainer2 = Pengajar2.Value
                            objClass.Trainer3 = Pengajar3.Value
                            objClass.Location = Alamat.Value
                            objClass.LocationName = NamaLokasi.Value
                            objClass.City = New CityFacade(User).Retrieve(Kota.Value)
                        End If
                        objClass.Lodging = Penginapan.Value
                        objClass.Description = Keterangan.Value
                        objClass.FiscalYear = TahunFiscal.Value

                        objClass.FinishDate = TanggalKeluar.Value.StringCellToDateTime()
                        objClass.StartDate = TanggalMasuk.Value.StringCellToDateTime()
                        objClass.Status = IIf(Status.Value.Equals("Aktif"), "1", "0")
                        objClass.Capacity = Integer.Parse(Kapasitas.Value)

                        Dim classCode As String = String.Empty
                        If objCourse.ClassCode.IsNullorEmpty Then
                            classCode = objCourse.CourseCode.ToUpper
                        Else
                            classCode = objCourse.ClassCode.ToUpper
                        End If

                        If listTraining.Count.Equals(0) Then
                            objClass.ClassCode = GenerateCodeClass(kodeTraining.Value, objClass.FiscalYear)
                            Dim number As Integer = 0
                            number = Integer.Parse(objClass.ClassCode.Replace(classCode + _
                                    objClass.FiscalYear.Substring(2, 2), String.Empty))
                            listTraining.Add(kodeTraining.Value)
                            listTrainingNum.Add(number)
                        Else
                            If listTraining.Contains(kodeTraining.Value) Then
                                Dim index As Integer = listTraining.IndexOf(kodeTraining.Value)
                                objClass.ClassCode = classCode + _
                                    objClass.FiscalYear.Substring(2, 2) + (listTrainingNum(index) + 1).GenerateIncrement(2)
                                listTrainingNum(index) = listTrainingNum(index) + 1
                            Else
                                objClass.ClassCode = GenerateCodeClass(kodeTraining.Value, objClass.FiscalYear)
                                Dim number As Integer = Integer.Parse(objClass.ClassCode.Replace(classCode + _
                                    objClass.FiscalYear.Substring(2, 2), String.Empty))
                                listTraining.Add(kodeTraining.Value)
                                listTrainingNum.Add(number)
                            End If

                        End If
                        listTrClass.Add(objClass)
                    End If
                Next
                If Not listError.Count.Equals(0) Then
                    helpers.SetSession("namaFile", fileUpload.PostedFile.FileName)
                    helpers.SetSession("dataError", listError)
                    Me.Page.ClientScript.RegisterStartupScript(Me.GetType(), "window-script", "document.getElementById('btnShowPopup').click();", True)
                Else
                    If helpers.GetSession("namaFile") IsNot Nothing Then
                        helpers.RemoveSession("namaFile")
                    End If
                    If helpers.GetSession("dataError") IsNot Nothing Then
                        helpers.RemoveSession("dataError")
                    End If
                    helpers.SetSession("dataUpload", listTrClass)
                    dtgUpload.DataSource = listTrClass
                    dtgUpload.VirtualItemCount = listTrClass.Count
                    dtgUpload.DataBind()
                    dtgUpload.Columns(12).Visible = False
                    btnUpload.Enabled = True
                End If

            End Using
        End Using
    End Sub

    Private Function GenerateCodeClass(ByVal kode As String, ByVal fiscalyear As String) As String
        Dim code As String = String.Empty
        Dim gelombang As Integer = 1
        Dim corse As TrCourse = New TrCourseFacade(User).Retrieve(kode)
        If String.IsNullorEmpty(corse.ClassCode) Then
            code = corse.CourseCode.ToUpper() + fiscalyear.Substring(2, 2)
        Else
            code = corse.ClassCode.ToUpper() + fiscalyear.Substring(2, 2)
        End If

        Dim srtParam As SortCollection = New SortCollection
        Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "ClassCode", MatchType.StartsWith, code))
        srtParam.Add(New Sort(GetType(TrClass), "CreatedTime", Sort.SortDirection.DESC))
        Dim arrClass As ArrayList = New TrClassFacade(User).Retrieve(criterias, srtParam)

        If arrClass.Count.Equals(0) Then
            Return code & gelombang.GenerateIncrement(2)
        Else
            Dim number As Integer = Integer.Parse(CType(arrClass(0), TrClass).ClassCode.Replace(code, "")) + 1
            Return code & number.GenerateIncrement(2)
        End If
    End Function
End Class
