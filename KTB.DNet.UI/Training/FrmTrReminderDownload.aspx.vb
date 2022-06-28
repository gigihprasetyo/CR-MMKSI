Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Security
Imports KTB.DNet.Utility
Imports KTB.DNet.UI

Public Class FrmTrReminderDownload
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents lblDealerID As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblCity As System.Web.UI.WebControls.Label
    Protected WithEvents lblNotes As System.Web.UI.WebControls.Label
    Protected WithEvents lblPeriode As System.Web.UI.WebControls.Label
    Protected WithEvents dtgReminder As System.Web.UI.WebControls.DataGrid

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim sHStatus As SessionHelper = New SessionHelper
    Dim objDealer As Dealer
    Private Const REF_TYPE As String = "TR"
    Private Const REF_CODE As String = "RMDR"
    Private objSessionHelper As New SessionHelper

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.EnableViewState = False
        objDealer = CType(sHStatus.GetSession("DEALER"), Dealer)
        If Not IsNothing(objDealer) Then
            ActivateUserPrivilege()
            InitiatePage()
            SetDownload()
            BindDataGrid(0)
        End If
    End Sub
    Private Function SetDownload()
        Response.ContentType = "application/x-download"
        Dim uploadFilename As String = New System.Text.StringBuilder("""").Append("Training Reminder.xls").Append("""").ToString

        Response.AddHeader("Content-Disposition", _
            New System.Text.StringBuilder("attachment;filename=").Append(uploadFilename).ToString)

    End Function

    Private Sub ActivateUserPrivilege()
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            If Not SecurityProvider.Authorize(Context.User, SR.TrainingRemainder_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Training - Reminder")
            End If
        Else
            'todo buat ktb privilege
        End If

    End Sub

    Private Sub InitiatePage()
        'objDealer = CType(sHStatus.GetSession("DEALER"), Dealer)
        lblDealerID.Text = objDealer.DealerCode & " / " & objDealer.SearchTerm1
        lblDealerName.Text = objDealer.DealerName
        lblCity.Text = objDealer.City.CityName
        Dim periode As DateTime = Today.AddMonths(1)
        lblPeriode.Text = CType(periode.Month, LookUp.EnumBulan).ToString() & " " & periode.Year

        ViewState("CurrentSortColumn") = "ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Try
            If (indexPage >= 0) Then
                Try
                    Dim objRef As Reference = New ReferenceFacade(User).RetrieveActiveList(REF_TYPE, REF_CODE)
                    If Not IsNothing(objRef) Then
                        lblNotes.Text = "Note :" & objRef.Description
                    End If
                Catch
                    lblNotes.Text = "Note :"
                End Try
                Dim arrReference As ArrayList = GetClassRegistrationByTraineeList(indexPage)
                dtgReminder.DataSource = arrReference
                dtgReminder.DataBind()
                
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function GetTraineeByDealerList(ByVal nDealer As Dealer) As ArrayList
        Dim objCritCol As CriteriaComposite = CreateCriteriaOfTrainees(nDealer.ID)
        Return New TrTraineeFacade(User).Retrieve(objCritCol)
    End Function

    Private Function GetClassRegistrationByTraineeList(ByVal indexPage As Integer) As ArrayList
        Dim objTrainees As ArrayList = GetTraineeByDealerList(objDealer)

        If Not IsNothing(objTrainees) And objTrainees.Count > 0 Then
            Dim objCritCol As CriteriaComposite = CreateCriteriaOfClassRegistration(objTrainees)

            Dim objSortCol As SortCollection = New SortCollection
            objSortCol.Add(New Sort(GetType(TrClassRegistration), ViewState("CurrentSortColumn"), ViewState("CurrentSortDirect")))

            Dim totalRow As Integer = 0
            Dim nResult As ArrayList = New TrClassRegistrationFacade(User).RetrieveByCriteria(objCritCol, _
                objSortCol, _
                indexPage + 1, _
                dtgReminder.PageSize, _
                totalRow)
            dtgReminder.VirtualItemCount = totalRow
            Return nResult
        End If
        Return New ArrayList
    End Function

    Private Function CreateCriteriaOfTrainees(ByVal nId As Integer) As CriteriaComposite
        Dim objCritCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrTrainee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        objCritCol.opAnd(New Criteria(GetType(TrTrainee), "Dealer.ID", MatchType.Exact, nId))
        Return objCritCol
    End Function

    Private Function CreateCriteriaOfClassRegistration(ByVal trainees As ArrayList) As CriteriaComposite
        Dim objCritCol As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        objCritCol.opAnd(New Criteria(GetType(TrClassRegistration), "TrTrainee.ID", MatchType.InSet, GenerateStrTraineeList(trainees)))

        Dim periodStart As DateTime = Today.AddMonths(1)
        'Dim periodEnd As DateTime = periodStart.AddMonths(1)

        Dim startDate As DateTime = New DateTime(periodStart.Year, periodStart.Month, 1, 0, 0, 0)
        Dim endDate As DateTime = New DateTime(periodStart.Year, periodStart.Month, DateTime.DaysInMonth(periodStart.Year, periodStart.Month), 23, 59, 59)

        objCritCol.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.StartDate", MatchType.GreaterOrEqual, startDate))
        objCritCol.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.StartDate", MatchType.LesserOrEqual, endDate))

        Return objCritCol
    End Function

    Private Function GenerateStrTraineeList(ByVal trainees As ArrayList)
        If IsNothing(trainees) Then
            Return "()"
        End If
        If trainees.Count = 0 Then
            Return "()"
        End If
        Dim sb As System.Text.StringBuilder = New System.Text.StringBuilder
        sb.Append("(")
        For idx As Integer = 0 To trainees.Count - 1
            Dim obj As TrTrainee = trainees(idx)
            sb.Append(obj.ID)
            If Not (obj Is trainees.Item(trainees.Count - 1)) Then
                sb.Append(",")
            End If
        Next
        sb.Append(")")
        Return sb.ToString
    End Function

    Private Sub dtgReminder_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles dtgReminder.ItemDataBound
        If Not e.Item.DataItem Is Nothing Then
            Dim RowValue As TrClassRegistration = CType(e.Item.DataItem, TrClassRegistration)

            Dim lblTraineeID As Label = CType(e.Item.FindControl("lblTraineeID"), Label)
            Dim lblName As Label = CType(e.Item.FindControl("lblName"), Label)
            Dim lblClass As Label = CType(e.Item.FindControl("lblClass"), Label)
            Dim lblStartDate As Label = CType(e.Item.FindControl("lblStartDate"), Label)
            Dim lblFinishDate As Label = CType(e.Item.FindControl("lblFinishDate"), Label)
            Dim lblLocation As Label = CType(e.Item.FindControl("lblLocation"), Label)

            If Not IsNothing(RowValue.TrTrainee) Then
                lblTraineeID.Text = RowValue.TrTrainee.ID
                lblName.Text = RowValue.TrTrainee.Name
            End If

            If Not IsNothing(RowValue.TrClass) Then
                Dim actionValue As String = "popUpClassInformation('" + RowValue.TrClass.ClassCode + "');"
                lblClass.Text = RowValue.TrClass.ClassCode
                lblStartDate.Text = RowValue.TrClass.StartDate.Date.ToShortDateString
                lblFinishDate.Text = RowValue.TrClass.FinishDate.Date.ToShortDateString
                lblLocation.Text = RowValue.TrClass.Location
            End If

        End If
    End Sub

    Private Sub dtgReminder_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dtgReminder.PageIndexChanged
        dtgReminder.SelectedIndex = -1
        dtgReminder.CurrentPageIndex = e.NewPageIndex
        BindDataGrid(dtgReminder.CurrentPageIndex)
    End Sub

    Private Sub dtgReminder_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles dtgReminder.SortCommand
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

        dtgReminder.SelectedIndex = -1
        dtgReminder.CurrentPageIndex = 0
        BindDataGrid(dtgReminder.CurrentPageIndex)
    End Sub

    Private Sub btnCetak_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Server.Transfer("../Training/FrmPrintTrReminder.aspx")
    End Sub
End Class
