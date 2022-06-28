#Region ".NET Base Class Namespace"
Imports System.Collections.Specialized
#End Region

#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
#End Region

Public Class FrmViewTrClassRegistration2
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnSave As System.Web.UI.WebControls.Button
    Protected WithEvents txtRegistrationCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblLastChange As System.Web.UI.WebControls.Label
    Protected WithEvents txtCertificateNo As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtTraineeName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtDealerName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtClassCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtClassName As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtLocation As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtStartDate As System.Web.UI.WebControls.TextBox
    Protected WithEvents txtFinishDate As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents test As System.Web.UI.HtmlControls.HtmlInputFile
    Protected WithEvents txtTglPendaftaran As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblLastStatus As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Private Variables"
    Private objSessionHelper As New SessionHelper
#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not Page.IsPostBack Then
            InitiatePage()
        End If
    End Sub

#Region "Private Method"
    Private Sub InitiatePage()
        'BindDdlStatus()

        'Dim coll As NameValueCollection = Page.Request.QueryString
        'If coll.Count > 0 Then
        '    'get querystring from page registration1
        '    Dim RegID As Integer = 0
        '    Dim Act As Integer = 0
        '    GetQueryString(coll, RegID, Act)
        '    'get object trclassallocation
        '    Dim objRegistration As TrClassRegistration = GetRegistrationData(RegID)
        '    'and fill it 
        '    FillFormData(objRegistration)
        '    'set enability of control
        '    If Act = 0 Then
        '        SetControl(False)
        '    Else
        '        SetControl(True)
        '    End If
        'End If

        If Not IsNothing(Session.Item("arlQueryColl")) _
            And Not IsNothing(Session.Item("objClassRegistration")) Then

            Dim arlQueryColl As ArrayList = _
                CType(Session.Item("arlQueryColl"), ArrayList)
            Dim objClassRegistration As TrClassRegistration = _
                CType(Session.Item("objClassRegistration"), TrClassRegistration)

            If arlQueryColl.Count = 6 And _
                Not IsNothing(objClassRegistration) Then

                FillFormData(objClassRegistration)
                Dim Act As Integer = CInt((CType(arlQueryColl(5), QueryStringCollection)).ParamValue)
                ViewState.Add("act", Act)
                'set enability of control
                If Act = 0 Then
                    SetControl(False)
                Else
                    SetControl(True)
                End If
            End If
        Else
            Response.Redirect("../Training/FrmViewTrClassRegistration1.aspx")
        End If
    End Sub

    'Uncomment it if you want to use this form as an updating form.
    'Private Sub BindDdlStatus()
    '    ddlStatus.Items.Clear()
    '    Dim obj As New EnumTrClassRegistration
    '    Dim arlStatusReg As ArrayList = obj.RetrieveStatus()
    '    For Each en As EnumClassReg In arlStatusReg
    '        Dim lItem As ListItem = New ListItem(en.NameType, en.ValueType.ToString())
    '        ddlStatus.Items.Add(lItem)
    '    Next

    '    'Dim statusColl As Array = System.Enum.GetValues(GetType(TrClassRegistration.EnumClassRegStatus))
    '    'For i As Integer = 0 To statusColl.Length - 1
    '    '    ddlStatus.Items.Add(New ListItem( _
    '    '    System.Enum.GetName(GetType(TrClassRegistration.EnumClassRegStatus), statusColl(i)), _
    '    '    CType(statusColl(i), String)))
    '    'Next
    '    'pilihan kosong dihilangkan untuk menghindari save data kosong dan error saat menampilkannya
    '    'ddlStatus.Items.Insert(0, New ListItem("", ""))
    'End Sub


    Private Sub SetControl(ByVal IsUpdate As Boolean)
        txtRegistrationCode.Enabled = False
        'txtRegistrationDate.Enabled = IsUpdate
        'Sesuai dengan keinginan tester's bug : txtRegistrationDate diganti dengan kalender 
        'calRegDate.Enabled = False 'IsUpdate
        'txtCertificateNo.Enabled = IsUpdate
        'ddlStatus.Enabled = IsUpdate
        btnSave.Enabled = IsUpdate
    End Sub

    Private Function GetRegistrationData(ByVal RegID As Integer) As TrClassRegistration
        Dim objRegistration As TrClassRegistration = New TrClassRegistrationFacade(User).Retrieve(RegID)
        If Not IsNothing(objRegistration) Then
            objSessionHelper.SetSession("objRegistration", objRegistration)
        End If
        Return objRegistration
    End Function

    'Private Sub GetQueryString(ByVal QueryStringColl As NameValueCollection, _
    'ByRef RegID As Integer, ByRef Act As Integer)
    '    'querystring collection consist of 0:classcode 1:status 2:regfrom 3:regto 4:act 5:regid
    '    '0-3 > use it for query again in registration1 if user press Kembali button 
    '    '4 > use it for determine action of form
    '    '5 > use it to get trclassregistration object to fill header data
    '    Dim queryHistory As String = QueryStringColl.GetKey(0) & "=" & QueryStringColl(0) & _
    '        "&" & QueryStringColl.GetKey(1) & "=" & QueryStringColl(1) & _
    '        "&" & QueryStringColl.GetKey(2) & "=" & QueryStringColl(2) & _
    '        "&" & QueryStringColl.GetKey(3) & "=" & QueryStringColl(3)
    '    ViewState.Add("queryHistory", queryHistory)
    '    ViewState.Add("act", QueryStringColl(4))
    '    ViewState.Add("regid", QueryStringColl(5))
    '    Act = CInt(QueryStringColl(4))
    '    RegID = CInt(QueryStringColl(5))
    'End Sub

    Private Sub FillFormData(ByVal Reg As TrClassRegistration)
        If Not IsNothing(Reg) Then
            txtRegistrationCode.Text = Reg.TrTrainee.ID
            'txtRegistrationDate.Text = Reg.RegistrationDate.ToString("dd/MM/yyyy")
            'Sesuai dengan keinginan tester's bug : txtRegistrationDate diganti dengan kalender 
            'calRegDate.Value = Reg.RegistrationDate.ToString("dd/MM/yyyy")
            'bug no 1084 kalender diganti text box
            txtTglPendaftaran.Text = Reg.RegistrationDate.ToString("dd/MM/yyyy")
            'txtCertificateNo.Text = Reg.CertificateNo
            txtTraineeName.Text = Reg.TrTrainee.Name
            'txtDealerName.Text = Reg.TrTrainee.Dealer.DealerName
            txtDealerName.Text = Reg.Dealer.DealerName
            txtClassCode.Text = Reg.TrClass.ClassCode
            txtClassName.Text = Reg.TrClass.ClassName
            txtLocation.Text = Reg.TrClass.Location
            txtStartDate.Text = Reg.TrClass.StartDate.ToString("dd/MM/yyyy")
            txtFinishDate.Text = Reg.TrClass.FinishDate.ToString("dd/MM/yyyy")
            'ddlStatus.SelectedValue = Reg.Status
            lblLastStatus.Text = New EnumTrClassRegistration().StatusByIndex(CInt(Reg.Status))             'ddlStatus.SelectedItem.Text()
            If Reg.LastUpdateTime <> CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                lblLastChange.Text = Reg.LastUpdateTime.ToString("dd/MM/yyyy")
            Else
                lblLastChange.Text = ""
            End If
        End If
    End Sub

    Private Sub GetFormData(ByRef Reg As TrClassRegistration)
        If Not IsNothing(Reg) Then
            Reg.RegistrationCode = txtRegistrationCode.Text
            'Reg.RegistrationDate = txtRegistrationDate.Text
            'Sesuai dengan keinginan tester's bug : txtRegistrationDate diganti dengan kalender 
            'Reg.RegistrationDate = calRegDate.Value
            'Reg.CertificateNo = txtCertificateNo.Text
            'Reg.Status = ddlStatus.SelectedValue
        End If
    End Sub

    'Private Function CheckStatusCondition(ByVal Reg As TrClassRegistration) As Boolean
    '    'daftar:0,lulus:1,tifak lulus :2,batal:3
    '    'daftar -> lulus,tifak lulus,batal
    '    'lulus -> tifak lulus
    '    'tifak lulus -> lulus
    '    'batal -> daftar
    '    If Reg.Status.ToString() = ddlStatus.SelectedValue.Trim() Then
    '        Return True
    '    Else
    '        Select Case Reg.Status.ToString()
    '            Case EnumTrClassRegistration.DataStatusType.Register
    '                'if status before daftar, all status can be granted except the class already expire
    '                'If ddlStatus.SelectedItem.Text.Trim() = TrClassRegistration.EnumClassRegStatus.Batal.ToString() Then
    '                '    If Reg.TrClass.StartDate <= Today Then
    '                '        Return True
    '                '    Else
    '                '        Return False
    '                '    End If
    '                'End If
    '                Return True

    '                'Case EnumTrClassRegistration.DataStatusType.Pass
    '                '    If ddlStatus.SelectedValue = EnumTrClassRegistration.DataStatusType.Fail Then
    '                '        Return True
    '                '    End If
    '                'Case EnumTrClassRegistration.DataStatusType.Fail
    '                '    If ddlStatus.SelectedValue = EnumTrClassRegistration.DataStatusType.Pass Then
    '                '        Return True
    '                '    End If
    '            Case EnumTrClassRegistration.DataStatusType.Reject
    '                If ddlStatus.SelectedValue = EnumTrClassRegistration.DataStatusType.Register Then
    '                    Return True
    '                End If
    '            Case EnumTrClassRegistration.DataStatusType.Cancel
    '                If ddlStatus.SelectedValue = EnumTrClassRegistration.DataStatusType.Register Then
    '                    Return True
    '                End If
    '        End Select
    '    End If

    '    Return False
    'End Function

    'Private Sub AdditionalCase(ByRef objRegistration As TrClassRegistration, _
    '    ByRef intResult As Integer)
    '    'add only if user change status        
    '    If lblLastStatus.Text.Trim() <> ddlStatus.SelectedItem.Text() Then
    '        'if status become batal, set rowstatus to -1 (delete)
    '        If ddlStatus.SelectedValue = EnumTrClassRegistration.DataStatusType.Reject Then
    '            objRegistration.RowStatus = CType(DBRowStatus.Deleted, Short)
    '            'if status become daftar, set rowstatus to 0 (active) and before that check class allocation
    '            'ElseIf ddlStatus.SelectedValue = TrClassRegistration.EnumClassRegStatus.Daftar Then
    '            '    If CheckClassAllocation(objRegistration) Then
    '            '        objRegistration.RowStatus = CType(DBRowStatus.Active, Short)
    '            '    Else
    '            '        intResult = -1
    '            '    End If
    '        End If
    '    End If
    'End Sub

    'Private Function GetClassAllocation(ByVal objRegistration As TrClassRegistration) As Integer
    '    Dim arlAllocation As ArrayList = New TrClassAllocationFacade(User).Retrieve( _
    '        CriteriaForGetClassAllocation(objRegistration.TrClass.ID, objRegistration.TrTrainee.Dealer.ID))
    '    Return CType(arlAllocation(0), TrClassAllocation).Allocated
    'End Function

    Private Function CriteriaForGetClassAllocation(ByVal ClassID As Integer, ByVal DealerID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrClassAllocation), "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClassAllocation), "TrClass.ID", MatchType.Exact, ClassID))
        criterias.opAnd(New Criteria(GetType(TrClassAllocation), "Dealer.ID", MatchType.Exact, DealerID))
        Return criterias
    End Function

    'Private Function CriteriaForCheckRegStatus(ByVal ClassID As Integer) As CriteriaComposite
    '    Dim criterias As New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", _
    '    MatchType.Exact, CType(DBRowStatus.Active, Short)))
    '    criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.ID", MatchType.Exact, ClassID))
    '    Return criterias
    'End Function

    'Private Function AggreateForCheckRecord() As Aggregate
    '    Dim aggregates As New Aggregate(GetType(TrClassRegistration), "ID", AggregateType.Count)
    '    Return aggregates
    'End Function

    'Private Function CheckClassAllocation(ByVal objRegistration As TrClassRegistration) As Boolean
    '    Dim iTotalRegTrainee As Integer = New HelperFacade(User, GetType(TrClassRegistration)).RecordCount( _
    '    CriteriaForCheckRegStatus(objRegistration.TrClass.ID), _
    '    AggreateForCheckRecord())

    '    Dim iClassAllocation As Integer = GetClassAllocation(objRegistration)

    '    If iClassAllocation < iTotalRegTrainee Then
    '        Return True
    '    End If
    '    Return False
    'End Function

#End Region

#Region "Event Handler"
    'Uncomment it if you want to use it again.
    'Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
    '    Dim objRegistration As TrClassRegistration = CType(Session.Item("objClassRegistration"), TrClassRegistration)
    '    'Dim intResult As Integer = 1
    '    If Not IsNothing(objRegistration) Then
    '        If CheckStatusCondition(objRegistration) Then
    '            GetFormData(objRegistration)
    '            'AdditionalCase(objRegistration, intResult)
    '            '        If intResult > 0 Then
    '            Dim objRegistrationFacade As New TrClassRegistrationFacade(User)
    '            If ddlStatus.SelectedValue = EnumTrClassRegistration.DataStatusType.Cancel Or ddlStatus.SelectedValue = EnumTrClassRegistration.DataStatusType.Reject Then
    '                Try
    '                    objRegistrationFacade.DeleteFromDB(objRegistration)
    '                    If ddlStatus.SelectedValue = EnumTrClassRegistration.DataStatusType.Reject Then
    '                        'TODO : Send Email
    '                    End If
    '                    Response.Redirect("../Training/FrmViewTrClassRegistration1.aspx")
    '                Catch ex As Exception
    '                    MessageBox.Show(SR.SaveFail)
    '                End Try
    '            Else
    '                If objRegistrationFacade.Update(objRegistration) > 0 Then
    '                    'MessageBox.Show(SR.SaveSuccess)
    '                    'Dim querystring As String = CType(ViewState.Item("queryHistory"), String)
    '                    'Response.Redirect("../Training/FrmViewTrClassRegistration1.aspx?" & querystring)
    '                    Response.Redirect("../Training/FrmViewTrClassRegistration1.aspx")
    '                Else
    '                    MessageBox.Show(SR.SaveFail)
    '                End If
    '                'Else
    '                'MessageBox.Show("Kelas sudah penuh")
    '                'End If
    '            End If

    '        Else
    '            MessageBox.Show("Tidak boleh melakukan perubahan status dari " & lblLastStatus.Text & " ke " & ddlStatus.SelectedItem.Text)
    '        End If
    '    End If
    'End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("../Training/FrmViewTrClassRegistration1.aspx")

        'Dim querystring As String = CType(ViewState.Item("queryHistory"), String)
        'Response.Redirect("../Training/FrmViewTrClassRegistration1.aspx?" & querystring)
    End Sub
#End Region

End Class
