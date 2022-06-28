#Region "Custom Namespace Imports"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.UI
Imports KTB.DNet.Security
Imports System.IO
Imports System.Collections.Generic
Imports System.Linq
Imports GlobalExtensions
Imports OfficeOpenXml
#End Region

Public Class FrmTrReminderNew
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

    Private ReadOnly Property AreaId As String
        Get
            Return Request.QueryString("area")
        End Get
    End Property
    
#End Region

    Dim sHStatus As SessionHelper = New SessionHelper
    Dim objDealer As Dealer
    Private Const REF_TYPE As String = "TR"
    Private Const REF_CODE As String = "RMDR"
    Private objSessionHelper As New SessionHelper
    Private helpers As New TrainingHelpers(Me.Page)

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        objDealer = CType(sHStatus.GetSession("DEALER"), Dealer)
        helpers.CheckPrivilegeTransaction("tr1" + AreaId.PrivilegeTrainingType)
        If Not IsNothing(objDealer) Then
            ActivateUserPrivilege()
            If Not IsPostBack Then
                helpers.CheckDueDateTagihan(AreaId)
                InitiatePage()
                BindDDLBulan()
                BindDDLKelas()
                TitleDescription(AreaId)
                ReadCritriaSearch()
                ' BindDataGrid(0)
            End If
        End If

        If hdnDealerCode.Value <> String.Empty Then
            Dim selectedDealer As Dealer = New DealerFacade(User).Retrieve(hdnDealerCode.Value.ToString())
            txtKodeDealer.Text = selectedDealer.DealerCode
            lblDealerName.Text = selectedDealer.DealerName
            lblCity.Text = selectedDealer.City.CityName
        End If

    End Sub

    Private Sub TitleDescription(ByVal areaid As String)
        If areaid.Equals("1") Then
            lblTitle.Text = "Training Sales - Reminder"
        ElseIf areaid.Equals("2") Then
            lblTitle.Text = "Training After Sales - Reminder"
        Else
            lblTitle.Text = "Training Customer Satisfaction - Reminder"
        End If
    End Sub


    Private Sub ActivateUserPrivilege()
        If objDealer.Title = EnumDealerTittle.DealerTittle.DEALER Then
            lblPopUpKodeDealer.Visible = False
            txtKodeDealer.Text = objDealer.DealerCode
            hdnDealerCode.Value = objDealer.DealerCode
            lblDealerName.Text = objDealer.DealerName
            lblCity.Text = objDealer.City.CityName
            txtKodeDealer.Enabled = False
            If Not SecurityProvider.Authorize(Context.User, SR.TrainingRemainder_Privilege) Then
                Server.Transfer("../FrmAccessDenied.aspx?modulName=Training - Reminder")
            End If
        Else
            lblPopUpKodeDealer.Visible = True
            txtKodeDealer.Text = String.Empty
            lblDealerName.Text = String.Empty
            objDealer.DealerCode = String.Empty
            lblCity.Text = String.Empty
            'todo buat ktb privilege
        End If

    End Sub

    Private Sub InitiatePage()
        btnDownload.Enabled = False
        btnCetak.Enabled = False
        ' objDealer = CType(sHStatus.GetSession("DEALER"), Dealer)

        Dim periode As DateTime = Today.AddMonths(1)
        lblPeriode.Text = CType(periode.Month, Lookup.EnumBulan).ToString() & " " & periode.Year

        ViewState("CurrentSortColumn") = "ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC

        txtTahun.Text = DateTime.Now.AddMonths(1).Year.ToString()
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Try
            Dim dealerSelected As Dealer = New DealerFacade(User).Retrieve(hdnDealerCode.Value)
            If (indexPage >= 0) Then
                Try
                    Dim refType As String = String.Empty

                    Select Case AreaId
                        Case "1"
                            refType = "TRSLS"
                        Case "2"
                            refType = "TRASS"
                        Case "3"
                            refType = "TRCS"
                    End Select

                    Dim objRef As Reference = New ReferenceFacade(User).RetrieveActiveList(refType, REF_CODE)
                    If Not IsNothing(objRef) Then
                        txtNotes.Text = objRef.Description
                    End If
                Catch
                    txtNotes.Text = String.Empty
                End Try

                Dim totalrow As Integer = 0
                Dim arrReference As ArrayList = New TrClassRegistrationFacade(User).RetrieveCustomPagingSP(CInt(txtTahun.Text), _
                                               CInt(ddlBulan.SelectedValue), CInt(ddlKelas.SelectedValue), dealerSelected.ID, Integer.Parse(AreaId), _
                                               indexPage, dtgReminder.PageSize, totalrow)
                lblPeriode.Text = ddlBulan.SelectedItem.Text + " " + txtTahun.Text
                dtgReminder.DataSource = arrReference
                dtgReminder.VirtualItemCount = totalrow
                dtgReminder.DataBind()
                If arrReference.Count > 0 Then
                    btnCetak.Enabled = True
                    btnDownload.Enabled = True
                    objSessionHelper.SetSession("PrintTr", arrReference)
                Else
                    btnDownload.Enabled = False
                    btnCetak.Enabled = False
                End If
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
        If e.IsRowItems Then
            e.Item.Cells(0).Text = e.CreateNumberPage
            Dim RowValue As TrClassRegistration = CType(e.Item.DataItem, TrClassRegistration)

            Dim lblTraineeID As Label = CType(e.Item.FindControl("lblTraineeID"), Label)
            Dim lblName As Label = CType(e.Item.FindControl("lblName"), Label)
            Dim lblClass As Label = CType(e.Item.FindControl("lblClass"), Label)
            Dim lblStartDate As Label = CType(e.Item.FindControl("lblStartDate"), Label)
            Dim lblFinishDate As Label = CType(e.Item.FindControl("lblFinishDate"), Label)
            Dim lblLocation As Label = CType(e.Item.FindControl("lblLocation"), Label)

            If RowValue.TrTrainee.IsNotNull Then
                Select Case AreaId.Trim
                    Case "1"
                        lblTraineeID.Text = RowValue.TrTrainee.ListTrTraineeSalesmanHeader.FirstOrDefault(Function(x) _
                                            x.JobPositionAreaID = CInt(AreaId)).SalesmanHeader.SalesmanCode
                        lblLocation.Text = RowValue.TrClass.Location

                    Case "3"
                        lblTraineeID.Text = RowValue.TrTrainee.ListTrTraineeSalesmanHeader.FirstOrDefault(Function(x) _
                                            x.JobPositionAreaID = CInt(AreaId)).SalesmanHeader.SalesmanCode
                        lblLocation.Text = RowValue.TrClass.Location
                    Case "2"
                        lblTraineeID.Text = RowValue.TrTrainee.ID
                        lblLocation.Text = RowValue.TrClass.TrMRTC.Name
                End Select
                lblName.Text = RowValue.TrTrainee.Name
            End If

            If Not IsNothing(RowValue.TrClass) Then
                Dim actionValue As String = "popUpClassInformation('" + RowValue.TrClass.ClassCode + "');"
                lblClass.Text = RowValue.TrClass.ClassCode
                lblStartDate.Text = RowValue.TrClass.StartDate.Date.ToShortDateString
                lblFinishDate.Text = RowValue.TrClass.FinishDate.Date.ToShortDateString
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

    Private Sub btnCetak_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCetak.Click
        If helpers.IsNullCriteria Then
            Server.Transfer("../Training/FrmPrintTrReminder.aspx?areaid=" + AreaId + "&bulan=" + ddlBulan.SelectedValue + "&tahun=" + txtTahun.Text + "&kelas=" + ddlKelas.SelectedValue + "&dealerCode=" + hdnDealerCode.Value)
        Else
            Server.Transfer("../Training/FrmPrintTrReminder.aspx?areaid=" + AreaId + "&bulan=" + helpers.GetStringCriteria("Bulan") + "&tahun=" + helpers.GetStringCriteria("Tahun") + "&kelas=" + ddlKelas.SelectedValue + "&dealerCode=" + hdnDealerCode.Value)
        End If
    End Sub

    Private Sub btnDownload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownload.Click
        'Response.Redirect("../Training/FrmTrReminderDownload.aspx")
        Using excelPackage As New ExcelPackage()
            Dim wsData As ExcelWorksheet = excelPackage.Workbook.Worksheets.Add("Data Siswa Kelas")
            Dim rowIdx As Integer = 1
            wsData.Cells("A" & rowIdx.ToString()).ValueBold("Kode Dealer ")
            wsData.Cells("B" & rowIdx.ToString()).ValueBold(": " + txtKodeDealer.Text)
            rowIdx += 1
            wsData.Cells("A" & rowIdx.ToString()).ValueBold("Nama Dealer")
            wsData.Cells("B" & rowIdx.ToString()).ValueBold(": " + lblDealerName.Text)
            rowIdx += 1
            wsData.Cells("A" & rowIdx.ToString()).ValueBold("Kota ")
            wsData.Cells("B" & rowIdx.ToString()).ValueBold(": " + lblCity.Text)
            rowIdx += 1
            wsData.Cells("A" & rowIdx.ToString()).ValueBold("Periode ")
            wsData.Cells("B" & rowIdx.ToString()).ValueBold(": " + lblCity.Text)
            rowIdx += 1
            rowIdx += 1

            Dim noUrut As Integer = 1

            CreateHeaderColumn(wsData, rowIdx)
            rowIdx += 1
            For Each dataSiswa As DataGridItem In dtgReminder.Items
                Dim clmidx As Integer = 1
                Dim idReg As Integer = CInt(dataSiswa.Cells(1).Text)
                Dim dataReg As TrClassRegistration = New TrClassRegistrationFacade(User).Retrieve(idReg)
                Dim rowValue As TrTrainee = dataReg.TrTrainee

                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(noUrut.ToString(), Style.ExcelHorizontalAlignment.Center)
                clmidx += 1
                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(dataReg.ID.ToString, Style.ExcelHorizontalAlignment.Center)
                clmidx += 1
                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(rowValue.ID.ToString, Style.ExcelHorizontalAlignment.Center)
                clmidx += 1
                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(rowValue.Name, Style.ExcelHorizontalAlignment.Center)
                clmidx += 1
                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(dataReg.TrClass.ClassCode, Style.ExcelHorizontalAlignment.Center)
                clmidx += 1
                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(dataReg.TrClass.StartDate.DateToString, Style.ExcelHorizontalAlignment.Center)
                clmidx += 1
                wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(dataReg.TrClass.FinishDate.DateToString, Style.ExcelHorizontalAlignment.Center)
                clmidx += 1
                If AreaId.Equals("2") Then
                    wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(dataReg.TrClass.TrMRTC.Name, Style.ExcelHorizontalAlignment.Center)
                    clmidx += 1
                Else
                    wsData.Cells(clmidx.ExcelColumnName & rowIdx.ToString).SetValue(dataReg.TrClass.Location, Style.ExcelHorizontalAlignment.Center)
                    clmidx += 1
                End If
                rowIdx += 1
                noUrut += 1

            Next
            rowIdx += 1
            For colIdx As Integer = 1 To 8
                wsData.Column(colIdx).AutoFit()
            Next
            wsData.Cells("A" & rowIdx.ToString()).ValueBold("Notes")
            rowIdx += 1
            For Each strLine As String In txtNotes.Text.Split(vbNewLine)
                wsData.Cells("A" & rowIdx.ToString()).ValueBold(strLine)
                rowIdx += 1
            Next

            Dim fileBytes = excelPackage.GetAsByteArray()
            Dim fileName As String = String.Format("Training-Reminder{0}.xlsx", Replace(lblPeriode.Text, " ", ""))

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
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("No. daftar")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("No. Reg")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Nama")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Kelas")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Mulai")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Selesai")
        columnIdx += 1
        wsData.Cells(columnIdx.ExcelColumnName & rowIdx.ToString()).SetHeaderValue("Lokasi")
        columnIdx += 1
        
    End Sub

    Private Sub BindDDLBulan()
        ddlBulan.ClearSelection()
        ddlBulan.Items.Clear()
        ddlBulan.Items.Add(New ListItem("Januari", "1"))
        ddlBulan.Items.Add(New ListItem("Februari", "2"))
        ddlBulan.Items.Add(New ListItem("Maret", "3"))
        ddlBulan.Items.Add(New ListItem("April", "4"))
        ddlBulan.Items.Add(New ListItem("Mei", "5"))
        ddlBulan.Items.Add(New ListItem("Juni", "6"))
        ddlBulan.Items.Add(New ListItem("Juli", "7"))
        ddlBulan.Items.Add(New ListItem("Agustus", "8"))
        ddlBulan.Items.Add(New ListItem("September", "9"))
        ddlBulan.Items.Add(New ListItem("Oktober", "10"))
        ddlBulan.Items.Add(New ListItem("November", "11"))
        ddlBulan.Items.Add(New ListItem("Desember", "12"))

        Dim bulanNow As Integer = (DateTime.Now.AddMonths(1)).Month
        ddlBulan.Items.FindByValue(bulanNow).Selected = True
    End Sub

    Private Sub BindDDLKelas()
        Dim totalRow As Integer = 0
        ddlKelas.ClearSelection()
        ddlKelas.Items.Clear()
        ddlKelas.Items.Add(New ListItem("Semua", "0"))
        Dim arrReference As ArrayList = New TrClassRegistrationFacade(User).RetrieveCustomPagingSP(CInt(txtTahun.Text), _
                                               CInt(ddlBulan.SelectedValue), 0, Me.GetDealer.ID, Integer.Parse(AreaId), _
                                               0, 1000, totalRow)
        Dim dicKelas As New Dictionary(Of String, String)
        For Each item As TrClassRegistration In arrReference
            If dicKelas.Where(Function(x) x.Key = item.TrClass.ID.ToString()).Count = 0 Then
                dicKelas.Add(item.TrClass.ID, item.TrClass.ClassCode)
            End If
        Next
        For Each i As KeyValuePair(Of String, String) In dicKelas
            ddlKelas.Items.Add(New ListItem(i.Value, i.Key))
        Next
        ddlKelas.SelectedIndex = 0
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try

            Dim dealerSelected As Dealer = New DealerFacade(User).Retrieve(hdnDealerCode.Value)

            Dim totalRow As Integer = 0
            SaveCriteriaSearch()
            Dim arrReference As ArrayList = New TrClassRegistrationFacade(User).RetrieveCustomPagingSP(CInt(txtTahun.Text), _
                                              CInt(ddlBulan.SelectedValue), CInt(ddlKelas.SelectedValue), dealerSelected.ID, Integer.Parse(AreaId), _
                                              0, dtgReminder.PageSize, totalRow)
            If arrReference.IsItems Then
                BindDataGrid(0)
            Else
                btnCetak.Enabled = False
                dtgReminder.DataSource = New List(Of TrClassRegistration)
                dtgReminder.DataBind()
                MessageBox.Show(SR.DataNotFound("Reminder"))
            End If
            lblPeriode.Text = ddlBulan.SelectedItem.Text + " " + txtTahun.Text
        Catch ex As Exception
            MessageBox.Show(SR.DataNotFound("Reminder"))
        End Try
    End Sub

    Private Sub ddlBulan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBulan.SelectedIndexChanged
        BindDDLKelas()
    End Sub

    Private Sub SaveCriteriaSearch()
        helpers.ClearCriteria()
        helpers.AddCriteria("Tahun", txtTahun.Text)
        helpers.AddCriteria("Bulan", ddlBulan.SelectedValue)
        helpers.AddCriteria("Kelas", ddlKelas.SelectedValue)
        helpers.SaveCriteria()
    End Sub

    Private Sub ReadCritriaSearch()
        If Not helpers.IsNullCriteria Then
            txtTahun.Text = helpers.GetStringCriteria("Tahun")
            ddlBulan.ClearSelection()
            ddlBulan.SelectedValue = helpers.GetStringCriteria("Bulan")
            BindDDLKelas()
            ddlKelas.ClearSelection()
            ddlKelas.SelectedValue = helpers.GetStringCriteria("Kelas")
        End If
    End Sub
End Class
