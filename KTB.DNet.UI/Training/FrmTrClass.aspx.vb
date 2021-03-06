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
#End Region

Public Class FrmTrClass
    Inherits System.Web.UI.Page
    Dim sHCourse As SessionHelper = New SessionHelper
    Dim indexPage As Integer = 0
    Protected WithEvents btnDownload As System.Web.UI.WebControls.Button
    Protected WithEvents txtLocation As System.Web.UI.WebControls.TextBox
    Protected WithEvents ddlCategory As System.Web.UI.WebControls.DropDownList
    Protected WithEvents txtClassCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPopUpClass As System.Web.UI.WebControls.Label
    Protected WithEvents btnUpload As System.Web.UI.WebControls.Button
    Protected WithEvents fileUpload As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents btnSimpan As System.Web.UI.WebControls.Button
    Protected WithEvents btnBatal As System.Web.UI.WebControls.Button
    Protected WithEvents dtgUpload As System.Web.UI.WebControls.DataGrid
    Protected WithEvents lblUploadFile As System.Web.UI.WebControls.Label
    Protected WithEvents lblSprtUpload As System.Web.UI.WebControls.Label
    Protected WithEvents txtPeriod As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnReport As System.Web.UI.WebControls.Button
    Dim totalRow As Integer = 0
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents dtgTrClass As System.Web.UI.WebControls.DataGrid
    Protected WithEvents pnl1 As System.Web.UI.WebControls.Panel
    Protected WithEvents txtKodeKategori As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblPopUpCourse As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Protected WithEvents btnBaru As System.Web.UI.WebControls.Button
    Protected WithEvents btnCari As System.Web.UI.WebControls.Button
    Private bPrivilegeChangeClass As Boolean = False
    Private bPrivilegeAlocationClass As Boolean = False


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " subs & function "

    Private Sub BindDdlCategory()
        ddlCategory.Items.Clear()
        ddlCategory.DataSource = EnumTrClass.RetrieveEnumTrClass()
        ddlCategory.DataValueField = "ID"
        ddlCategory.DataTextField = "Name"
        ddlCategory.DataBind()
    End Sub
    Private Sub initiatePage()
        clearData()
        ViewState("CurrentSortColumn") = "ClassCode"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub
    
    Private Sub bindDataGrid(ByVal indexPage As Integer)
        Dim objTrCourse As TrCourse
        Dim arTrCourse, artrClass As ArrayList
        Dim startDate, finishDate As DateTime
        Dim criterias As CriteriaComposite
        criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtKodeKategori.Text <> "" Then
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

        If txtClassCode.Text <> "" Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "ClassCode", MatchType.[Partial], txtClassCode.Text.Trim))
        End If

        If txtPeriod.Text.Length = 4 Then
            startDate = New DateTime(txtPeriod.Text, 12, Date.DaysInMonth(txtPeriod.Text, 12), 23, 59, 59)
            finishDate = New DateTime(txtPeriod.Text, 1, 1, 0, 0, 0)
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "StartDate", MatchType.LesserOrEqual, startDate))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "FinishDate", MatchType.GreaterOrEqual, finishDate))
        ElseIf (txtPeriod.Text.Length > 0 And txtPeriod.Text.Length <> 4) Then
            MessageBox.Show("Format tahun tidak benar")
            Exit Sub
        End If
        If txtLocation.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "Location", MatchType.[Partial], txtLocation.Text))
        End If

        'If CInt(ddlCategory.SelectedValue) = 0 Then
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "Category", MatchType.No, CInt(EnumTrClass.EnumTrClassCategory.GENERAL)))
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "Category", MatchType.No, CInt(EnumTrClass.EnumTrClassCategory.MSTEP)))
        'Else
        '    criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "Category", MatchType.Exact, ddlCategory.SelectedValue))
        'End If

        sHCourse.SetSession("searchRes", criterias)
        dtgTrClass.DataSource() = New TrClassFacade(User).RetrieveActiveList(criterias, indexPage + 1, dtgTrClass.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dtgTrClass.VirtualItemCount = totalRow
        dtgTrClass.DataBind()
    End Sub
    Private Sub clearData()

        For Each c As Control In Me.pnl1.Controls
            If TypeOf c Is TextBox Then
                CType(c, TextBox).Text = ""
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

        'sb.Append("<html><body><Table width=600px><h1><td colspan = 4 align=center><b>")
        'sb.Append("Jakarta,")
        'sb.Append(strDate)
        'sb.Append("</b></td></h1><tr><td><br></td></tr><tr><td>")
        'sb.Append("</td></tr><tr><td><br></td></tr><tr><td>")
        'sb.Append("Kepada Yth,")
        'sb.Append("</td> </tr><tr><td>")
        'sb.Append("PT MMKSI - TC")
        'sb.Append("</td></tr><tr><td>")
        'sb.Append("</td></tr><tr><td><br></td></tr><tr><td>")
        'sb.Append("Mohon diproses Alokasi Tambahan untuk kelas berikut ")
        'sb.Append(ObjTrClass.ClassCode)
        'sb.Append(" - ")
        'sb.Append(ObjTrClass.ClassName)
        'sb.Append("</td></tr><tr><td><br></td></tr><tr><td>")
        'sb.Append("</td></tr><tr><td>")
        'sb.Append("</td> <td align=center>")
        'sb.Append("</td></tr>")
        'sb.Append("<tr><td>")
        'sb.Append("</td></tr><tr><td><br></td></tr><tr><td>")
        'sb.Append("Terima kasih.")
        'sb.Append("</td></tr><tr><td><br></td></tr><tr><td>")
        'sb.Append(ObjDealer.DealerName)
        'sb.Append("</td></tr><tr><td>")
        'sb.Append(ObjDealer.DealerCode)
        'sb.Append("</td></tr></table></body></table></html>")

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
            retValue = ""
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

        'Dim arlListUserEmail As ArrayList = GetActiveListUserEmail()

        'Dim arlChangedAllocation As ArrayList = GetChangedAllocation(AllAllocationDealerColl, _
        '    AllocationToProcessColl)

        'Try
        '    For Each objChangedAllocation As TrClassAllocation In arlChangedAllocation
        '        Dim strTo As String = String.Empty
        '        Dim strCc As String = String.Empty
        '        GenerateToAndCcAddress(strTo, strCc, _
        '            objChangedAllocation.Dealer, arlListUserEmail)
        '        Dim strBody As String = SetFormatEmail(objChangedAllocation)

        '        If (strTo <> String.Empty Or strCc <> String.Empty) _
        '            And strFrom <> String.Empty Then
        '            'added by samuel : dealer will get email if dealer has been allocated
        '            If IsDealerAllocated(objChangedAllocation.TrClass.ID, objChangedAllocation.Dealer.ID) Then
        '                objEmail.sendMail(strTo, strCc, strFrom, strSubject, Mail.MailFormat.Html, strBody)
        '            End If
        '        Else
        '            arlErrorSendEmail.Add(objChangedAllocation.Dealer.DealerCode)
        '        End If
        '    Next
        'Catch ex As Exception
        '    'MessageBox.Show("Proses Kirim Email ke dealer " & & " Tidak Berhasil")
        'End Try

        'If arlErrorSendEmail.Count > 0 Then
        '    Dim strErrorDealer As String = String.Empty
        '    For Each strDealer As String In arlErrorSendEmail
        '        If strErrorDealer = String.Empty Then
        '            strErrorDealer += strDealer
        '        Else
        '            strErrorDealer += "," & strDealer
        '        End If
        '    Next
        '    MessageBox.Show("Proses Kirim Email Tidak Berhasil ke Dealer : " & strErrorDealer)
        'End If
        ''DisplayChangedAllocation(arlChangedAllocation)
    End Function

#End Region

#Region " control event handler "

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        ActivateUserPrivilege()
        If Not Page.IsPostBack Then
            initiatePage()
            'BindDdlCategory()
            btnSimpan.Enabled = False
            btnBatal.Enabled = False
            If Not sHCourse.GetSession("backRes") Is Nothing And (Not sHCourse.GetSession("searchRes") Is Nothing) Then
                dtgTrClass.DataSource() = New TrClassFacade(User).RetrieveActiveList(CType(sHCourse.GetSession("searchRes"), CriteriaComposite), indexPage + 1, dtgTrClass.PageSize, totalRow, CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
                dtgTrClass.VirtualItemCount = totalRow
                dtgTrClass.DataBind()
                btnDownload.Visible = True
            ElseIf Not IsNothing(sHCourse.GetSession("arlCA")) Then
                'Dim arlClass As ArrayList = CType(sHCourse.GetSession("arlCA"), ArrayList)
                'If ViewState.Item("vsProcess") = "Edit" Then
                '    'sHCourse.SetSession("editID", CInt(e.Item.Cells(0).Text))

                'Else
                '    dtgUpload.DataSource() = arlClass
                'End If
                'dtgUpload.DataBind()
                'dtgUpload.Columns(11).Visible = False 'err message
                'dtgUpload.Columns(12).Visible = True
                'btnDownload.Visible = True
                DataBindUpload()
            End If
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

    Private Sub DataBindUpload()
        Dim arlClass As ArrayList = CType(sHCourse.GetSession("arlCA"), ArrayList)
        Dim arlTrClass As ArrayList
        If arlClass.Count > 0 Then
            Dim strID As String = ""
            For Each oClass As TrClass In arlClass
                strID += CType(oClass.ID, String) + ","
            Next
            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "ID", MatchType.InSet, "(" & strID.Substring(0, strID.Length - 1) & ")"))

            arlTrClass = New TrClassFacade(User).Retrieve(criterias)
            
            If arlTrClass.Count > 0 Then
                sHCourse.RemoveSession("arlCA")
                sHCourse.SetSession("arlCA", arlTrClass)

                dtgUpload.DataSource() = arlTrClass
                dtgUpload.VirtualItemCount = arlTrClass.Count
                dtgUpload.DataBind()
                dtgUpload.Columns(11).Visible = False 'err message
                dtgUpload.Columns(12).Visible = True

                btnDownload.Visible = True
            End If
        End If
    End Sub

    Private Sub ActivateUserPrivilege()
        bPrivilegeChangeClass = SecurityProvider.Authorize(Context.User, SR.TrainingUpdateKelas_Privilege)
        bPrivilegeAlocationClass = SecurityProvider.Authorize(Context.User, SR.ENHEmailAlokasiTambahan_Privilege)
        If Not SecurityProvider.Authorize(Context.User, SR.TrainingViewKelas_Privilege) Then
            Server.Transfer("../FrmAccessDenied.aspx?modulName=Training - Form Kelas")
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
        Dim arlClass As ArrayList = CType(sHCourse.GetSession("arlCA"), ArrayList)
        Dim arlClassNew As New ArrayList
        If arlClass.Count > 0 Then
            For Each oClass As TrClass In arlClass
                Dim iReturn As Integer
                iReturn = InsertClass(oClass)
                If iReturn > 0 Then
                    oClass.ID = iReturn
                    arlClassNew.Add(oClass)
                End If
            Next
            sHCourse.SetSession("arlCA", arlClassNew)
            DataBindUpload()
            'dtgUpload.DataSource = arlClassNew
            'dtgUpload.DataBind()
            'dtgUpload.Columns(11).Visible = False 'err message
            'dtgUpload.Columns(12).Visible = True 'action button
        End If
        setUploadControl(False)
        btnDownload.Enabled = True
    End Sub

    Private Function InsertClass(ByVal objTrClass As TrClass) As Integer
        Return New TrClassFacade(User).InsertTransaction(objTrClass)
    End Function

    Private Sub btnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBatal.Click
        sHCourse.RemoveSession("arlCA")
        Response.Redirect("FrmTrClass.aspx")
    End Sub
    Private Sub dtgCourse_ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgTrClass.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            'viewGroup(e.Item.Cells(0).Text, False)
            sHCourse.SetSession("viewID", CInt(e.Item.Cells(0).Text))
            Response.Redirect("FrmTrClassDetail.aspx")

            lblPopUpCourse.Enabled = False
            dtgTrClass.SelectedIndex = -1
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            sHCourse.SetSession("editID", CInt(e.Item.Cells(0).Text))
            dtgTrClass.SelectedIndex = e.Item.ItemIndex
            Response.Redirect("FrmTrClassDetail.aspx")
            txtKodeKategori.ReadOnly = True
            lblPopUpCourse.Enabled = False
        ElseIf e.CommandName = "Delete" Then
            deleteTrClass(e.Item.Cells(0).Text)
        ElseIf e.CommandName = "AddAlloc" Then
            If SendEmail(CInt(e.Item.Cells(0).Text)) = 1 Then
                CType(e.Item.FindControl("lbtnAddAllocation"), LinkButton).Enabled = False
                MessageBox.Show("Kirim Email Sukses")
            Else
                MessageBox.Show("Kirim Email Gagal")
            End If
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
            Dim RetHit As Integer = hitungAllocated(RowValue.ID)
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
        End If
        boundDtgTrClass(e)
        If Not e.Item.FindControl("btnUbah") Is Nothing Then
            CType(e.Item.FindControl("btnUbah"), LinkButton).Visible = bPrivilegeChangeClass
        End If

        If Not e.Item.FindControl("btnHapus") Is Nothing Then
            CType(e.Item.FindControl("btnHapus"), LinkButton).Visible = bPrivilegeChangeClass
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
                    'CType(e.Item.FindControl("lbtnAddAllocation"), LinkButton).Visible = SecurityProvider.Authorize(Context.User, SR.ENHTrainingKelasDaftar_Privilege)
                End If
                'Else
                '    If Not e.Item.FindControl("lbtnAddAllocation") Is Nothing Then
                '        CType(e.Item.FindControl("lbtnAddAllocation"), LinkButton).Visible = False
                '    End If
                'End If
            End If
        Else
            If Not e.Item.FindControl("lbtnAddAllocation") Is Nothing Then
                CType(e.Item.FindControl("lbtnAddAllocation"), LinkButton).Visible = bPrivilegeAlocationClass
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
        Response.Redirect("FrmTrClassDetail.aspx")

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
        'Response.Write(New System.Text.StringBuilder("<script language='javascript'>").Append("{window.open('./FrmDownoad.aspx','_blank','fullscreen=no,menubar=yes,status=yes,titlebar=yes,toolbar=no,height=480,width=640,resizable=yes');}").Append("</script>").ToString)
    End Sub

#End Region


    Private Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        sHCourse.RemoveSession("searchRes") '
        sHCourse.RemoveSession("backRes") '

        dtgTrClass.DataSource = Nothing
        dtgTrClass.Visible = False

        dtgUpload.Visible = True
        setUploadControl(True)
        UploadClass()
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
        'btnUpload.Enabled = Not IsUploading
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
                        btnUpload.Enabled = True  '-- Disable button <Upload> if successfully upload data

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
            Dim strError As String = ""

            Dim lblUNo As Label = e.Item.FindControl("lblUNo")
            Dim lblUClassCode As Label = e.Item.FindControl("lblUClassCode")
            Dim lblUClassName As Label = e.Item.FindControl("lblUClassName")
            Dim lblUCategory As Label = e.Item.FindControl("lblUCategory")
            Dim lblULocation As Label = e.Item.FindControl("lblULocation")
            Dim lblUTrainer1 As Label = e.Item.FindControl("lblUTrainer1")
            Dim lblUStartDate As Label = e.Item.FindControl("lblUStartDate")
            Dim lblUEndDate As Label = e.Item.FindControl("lblUEndDate")
            Dim lblUCapacity As Label = e.Item.FindControl("lblUCapacity")
            Dim lblURemain As Label = e.Item.FindControl("lblURemain")
            Dim lblUMessage As Label = e.Item.FindControl("lblUMessage")

            Dim i As Integer = 0

            lblUNo.Text = e.Item.ItemIndex + 1
            objC = CType(CType(Me.sHCourse.GetSession("arlCA"), ArrayList)(e.Item.ItemIndex), TrClass)

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

            'Check duplicate data 
            'If e.Item.ItemIndex > 0 Then
            '    If Not IsNothing(objC) Then
            '        Dim Idx As Integer = 0
            '        For Each oCA As TrClass In CType(Me.sHCourse.GetSession("arlCA"), ArrayList)
            '            If Idx < e.Item.ItemIndex AndAlso Not IsNothing(oCA) Then
            '                lblUMessage.Text = "Data duplicate"
            '                e.Item.BackColor = System.Drawing.Color.Red
            '            End If
            '            Idx += 1
            '        Next
            '    End If
            'End If



            If lblUMessage.Text.Trim <> String.Empty Then
                Me.btnSimpan.Enabled = False
            End If
        End If
    End Sub

    Private Sub dtgUpload_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dtgUpload.ItemCommand
        If (e.CommandName = "View") Then
            ViewState.Add("vsProcess", "View")
            'viewGroup(e.Item.Cells(0).Text, False)
            sHCourse.SetSession("viewID", CInt(e.Item.Cells(0).Text))
            Response.Redirect("FrmTrClassDetail.aspx")

            lblPopUpCourse.Enabled = False
            dtgUpload.SelectedIndex = -1
        ElseIf e.CommandName = "Edit" Then
            ViewState.Add("vsProcess", "Edit")
            sHCourse.SetSession("editID", CInt(e.Item.Cells(0).Text))
            dtgUpload.SelectedIndex = e.Item.ItemIndex
            Response.Redirect("FrmTrClassDetail.aspx")
        ElseIf e.CommandName = "Delete" Then
            deleteTrClassUpload(e.Item.Cells(0).Text)
        ElseIf e.CommandName = "AddAlloc" Then
            If SendEmail(CInt(e.Item.Cells(0).Text)) = 1 Then
                CType(e.Item.FindControl("lbtnUAllocation"), LinkButton).Enabled = False
                MessageBox.Show("Kirim Email Sukses")
            Else
                MessageBox.Show("Kirim Email Gagal")
            End If
        End If
    End Sub

    Private Sub btnReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReport.Click
        If txtPeriod.Text.Trim <> "" Then
            If txtPeriod.Text.Trim.Length = 4 Then
                Dim arlReport As New ArrayList
                arlReport = GetData(txtPeriod.Text.Trim)
                DownloadReport(txtPeriod.Text.Trim, arlReport)
            Else
                MessageBox.Show("Format tahun periode laporan salah (yyyy)")
                Exit Sub
            End If
        Else
            MessageBox.Show("Tentukan tahun periode laporan")
            Exit Sub
        End If
    End Sub

    Private Function GetData(ByVal strPeriod As String) As ArrayList
        Dim objTrCourse As TrCourse
        Dim arTrCourse, artrClass As ArrayList
        Dim startDate, finishDate As DateTime
        Dim criterias As CriteriaComposite

        criterias = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        If txtKodeKategori.Text <> "" Then
            Dim criterias2 As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias2.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourse), "CourseCode", MatchType.Exact, txtKodeKategori.Text))

            arTrCourse = New TrCourseFacade(User).Retrieve(criterias2)

            If arTrCourse.Count > 0 Then
                criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClass), "TrCourse.ID", MatchType.Exact, CType(arTrCourse(0), TrCourse).ID))
            End If
        End If

        If txtClassCode.Text <> "" Then
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

        'Dim arlData As ArrayList = New TrClassFacade(User).RetrieveActiveList(criterias, "StartDate", Sort.SortDirection.ASC)
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
End Class
