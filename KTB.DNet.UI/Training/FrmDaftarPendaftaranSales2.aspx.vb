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
Imports System.Collections.Generic
Imports System.Linq
#End Region

Public Class FrmDaftarPendaftaranSales2
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    
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
        ' Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not Page.IsPostBack Then
            InitiatePage()
            Me.Page.DisabledTextBoxWithPrefix("txt")
        End If
    End Sub

#Region "Private Method"
    Private Sub InitiatePage()
        
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
            Response.Redirect("../Training/FrmDaftarPendaftaranSales.aspx")
        End If
    End Sub

    Private Sub SetControl(ByVal IsUpdate As Boolean)
        btnSave.Enabled = IsUpdate
    End Sub

    Private Function GetRegistrationData(ByVal RegID As Integer) As TrClassRegistration
        Dim objRegistration As TrClassRegistration = New TrClassRegistrationFacade(User).Retrieve(RegID)
        If Not IsNothing(objRegistration) Then
            objSessionHelper.SetSession("objRegistration", objRegistration)
        End If
        Return objRegistration
    End Function

    Private Sub FillFormData(ByVal Reg As TrClassRegistration)
        If Not IsNothing(Reg) Then
            txtRegistrationCode.Text = Reg.TrTrainee.ListTrTraineeSalesmanHeader.FirstOrDefault(Function(x) _
                                    x.JobPositionAreaID = 1).SalesmanHeader.SalesmanCode
            txtTglPendaftaran.Text = Reg.RegistrationDate.DateToString()
            txtTraineeName.Text = Reg.TrTrainee.Name
            txtDealerName.Text = Reg.Dealer.DealerName
            txtClassCode.Text = Reg.TrClass.ClassCode
            txtClassName.Text = Reg.TrClass.ClassName
            txtLocation.Text = Reg.TrClass.Location
            txtStartDate.Text = Reg.TrClass.StartDate.DateToString()
            txtFinishDate.Text = Reg.TrClass.FinishDate.DateToString()
            lblLastStatus.Text = New EnumTrClassRegistration().StatusByIndex(CInt(Reg.Status))             'ddlStatus.SelectedItem.Text()
            lblLastChange.Text = Reg.LastUpdateTime.DateToString()
        End If
    End Sub

    Private Sub GetFormData(ByRef Reg As TrClassRegistration)
        If Not IsNothing(Reg) Then
            Reg.RegistrationCode = txtRegistrationCode.Text
            
        End If
    End Sub

    Private Function CriteriaForGetClassAllocation(ByVal ClassID As Integer, ByVal DealerID As Integer) As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrClassAllocation), "RowStatus", _
        MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClassAllocation), "TrClass.ID", MatchType.Exact, ClassID))
        criterias.opAnd(New Criteria(GetType(TrClassAllocation), "Dealer.ID", MatchType.Exact, DealerID))
        Return criterias
    End Function

#End Region

#Region "Event Handler"
    
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Response.Redirect("../Training/FrmDaftarPendaftaranSales.aspx")

    End Sub
#End Region

End Class
