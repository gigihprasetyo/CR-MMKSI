#Region " .NET Base Class Namespace Imports "
Imports System
Imports System.IO
Imports System.Text
Imports System.Collections.Generic
Imports System.Linq
#End Region

#Region " Custom Namespace "
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit
Imports KTB.DNet.BusinessFacade.Service
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.Utility
Imports KTB.DNet.Parser
Imports KTB.DNet.Security
Imports KTB.DNet.BusinessFacade.Profile
Imports KTB.DNet.WebCC


#End Region
Public Class FrmClassConfirmationCS
    Inherits System.Web.UI.Page

    Dim gridColNo As Integer = 0
    Private Const EmptyDate As String = "01/01/0001 0:00:00"
    Private helpers As TrainingHelpers = New TrainingHelpers(Me.Page, "KONFIRMASI PENDAFTARAN KELAS CS")

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        helpers.AddPriv(TrainingHelpers.privilageTraining.PrivillageType.view, SR.TrainingCsViewClassConfirmation_Privilege)
        helpers.AddPriv(TrainingHelpers.privilageTraining.PrivillageType.fullAccess, SR.TrainingCsEditClassConfirmation_Privilege)
        helpers.Privilage()
        If Not Page.IsPostBack Then
            InitForm()
        End If

    End Sub

    Private Sub InitForm()

        ViewState("CurrentSortColumn") = "ID"
        ViewState("CurrentSortDirect") = Sort.SortDirection.ASC

        BindDdlStatus()
        BindDdlFiscalYear()
        BindDataGrid(0)
    End Sub

    Private Sub BindDdlStatus()
        ddlStatus.Items.Clear()
        Dim enumRegis As New EnumTrClassRegistration
        Dim arlStatus As ArrayList = enumRegis.Retrieve()

        For Each data As EnumClassReg In arlStatus
            ddlStatus.Items.Add(New ListItem(data.NameType, data.ValueType))
        Next

        ddlStatus.Items.Insert(0, New ListItem("Silakan Pilih", "-1"))

    End Sub

    Private Sub BindDdlFiscalYear()
        Dim GetTahun As Integer = DateTime.Now.Year
        ddlTahunFiscal.ClearSelection()
        ddlTahunFiscal.Items.Clear()
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
        ddlTahunFiscal.Items.Insert(0, New ListItem("Silakan Pilih", "-1"))
    End Sub

    Private Sub BindDataGrid(ByVal indexPage As Integer)
        Dim totalRow As Integer = 0
        gridColNo = dgList.PageSize * indexPage
        dgList.DataSource = New TrClassRegistrationFacade(User).RetrieveActiveList(CriteriaSearch(), indexPage + 1, dgList.PageSize, totalRow, _
            CType(ViewState("CurrentSortColumn"), String), _
            CType(ViewState("CurrentSortDirect"), Sort.SortDirection))
        dgList.VirtualItemCount = totalRow
        dgList.CurrentPageIndex = indexPage
        dgList.DataBind()
    End Sub

    Private Function CriteriaSearch() As CriteriaComposite
        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "Dealer.ID", MatchType.Exact, CType(Session("Dealer"), Dealer).ID))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.TrCourse.JobPositionCategory.ID", MatchType.Exact, 4))

        If ddlStatus.SelectedValue <> "-1" Then
            criterias.opAnd(New Criteria(GetType(TrClassRegistration), "Status", MatchType.Exact, ddlStatus.SelectedValue))
        End If

        If ddlTahunFiscal.SelectedValue.ToString() <> "-1" Then
            criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.FiscalYear", MatchType.Exact, ddlTahunFiscal.SelectedValue))
        End If

        If txtClassCode.Text <> String.Empty Then
            criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.ClassCode", MatchType.Exact, txtClassCode.Text.Trim()))
        End If

        Try
            If ICTanggalMasukFrom.Value.ToString() <> EmptyDate Or ICTanggalMasukTo.Value.ToString <> EmptyDate Then
                ValidateDateMasuk()
                criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.StartDate", MatchType.GreaterOrEqual, ICTanggalMasukFrom.Value))
                criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.FinishDate", MatchType.LesserOrEqual, ICTanggalMasukTo.Value.AddDays(1)))
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        Return criterias
    End Function

    Private Sub ValidateDateMasuk()
        Try

            If ICTanggalMasukFrom.Value.ToString() = EmptyDate Or ICTanggalMasukTo.Value.ToString = EmptyDate Then
                Throw New Exception("")
            End If

            If Not ICTanggalMasukFrom.Value <= ICTanggalMasukTo.Value Then
                Throw New Exception("")
            End If
        Catch ex As Exception
            Throw New Exception("Tanggal masuk dari harus lebih kecil dari tanggal masuk sampai")
        End Try
    End Sub

    Private Sub dgList_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgList.ItemCommand
        Try
            If (e.CommandName = "Confirm") Then
                Dim id As Integer = CInt(e.Item.Cells(0).Text)
                Dim data As TrClassRegistration = New TrClassRegistrationFacade(User).Retrieve(id)
                ValidateRegistration(data)
                ConfirmRegistration(data)
                BindDataGrid(0)
            ElseIf (e.CommandName = "Reject") Then
                Dim id As Integer = CInt(e.Item.Cells(0).Text)
                Dim data As TrClassRegistration = New TrClassRegistrationFacade(User).Retrieve(id)
                RejectRegistration(data)
                BindDataGrid(0)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            BindDataGrid(dgList.CurrentPageIndex)
        End Try

    End Sub

    Private Sub ConfirmRegistration(ByVal data As TrClassRegistration)
        Try
            data.Status = EnumTrClassRegistration.DataStatusType.Register
            data.RegistrationDate = DateTime.Now
            Dim facade As New TrClassRegistrationFacade(User)
            facade.Update(data)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub RejectRegistration(ByVal data As TrClassRegistration)
        Try
            data.Status = EnumTrClassRegistration.DataStatusType.Reject
            data.Notes = hdnRejectReason.Value
            Dim facade As New TrClassRegistrationFacade(User)
            facade.Update(data)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Private Sub dgList_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgList.ItemDataBound
        Try
            If Not e.Item.DataItem Is Nothing Then
                Dim data As TrClassRegistration = CType(e.Item.DataItem, TrClassRegistration)

                gridColNo += 1

                Dim lblNo As Label = CType(e.Item.FindControl("lblNo"), Label)
                Dim lblTraineeName As Label = CType(e.Item.FindControl("lblTraineeName"), Label)
                Dim lblClassCode As Label = CType(e.Item.FindControl("lblClassCode"), Label)
                Dim lblCourseCategory As Label = CType(e.Item.FindControl("lblCourseCategory"), Label)
                Dim lblLocation As Label = CType(e.Item.FindControl("lblLocation"), Label)
                Dim lblFiscalYear As Label = CType(e.Item.FindControl("lblFiscalYear"), Label)
                Dim lblDate As Label = CType(e.Item.FindControl("lblDate"), Label)
                Dim btnConfirmSingle As LinkButton = CType(e.Item.FindControl("btnConfirmSingle"), LinkButton)
                Dim btnReject As LinkButton = CType(e.Item.FindControl("btnReject"), LinkButton)
                Dim lblStatus As Label = CType(e.Item.FindControl("lblStatus"), Label)
                Dim lblNotes As Label = CType(e.Item.FindControl("lblNotes"), Label)

                lblNo.Text = gridColNo
                lblTraineeName.Text = data.TrTrainee.Name
                lblClassCode.Text = data.TrClass.ClassCode
                lblCourseCategory.Text = data.TrClass.TrCourse.CourseCode
                lblLocation.Text = data.TrClass.Location
                lblFiscalYear.Text = data.TrClass.FiscalYear
                lblDate.Text = data.TrClass.StartDate & " s/d " & data.TrClass.FinishDate
                lblNotes.Text = data.Notes

                Dim enumRegis As New EnumTrClassRegistration

                lblStatus.Text = enumRegis.StatusByIndex(data.Status)

                If data.Status = CInt(EnumTrClassRegistration.DataStatusType.Invite) And data.TrClass.ConfirmDueDate >= CDate(Date.Now.ToShortDateString) Then
                    btnConfirmSingle.Attributes.Add("onclick", "if(!confirm('Konfirmasi pendaftaran " & data.TrTrainee.Name & " di kelas " & data.TrClass.ClassCode & " ?')) return false;")
                    'btnReject.Attributes.Add("onclick", "if(!confirm('Anda yakin ingin menolak pendaftaran " & data.TrTrainee.Name & " di kelas " & data.TrClass.ClassCode & " ?')) return false;")
                    btnReject.Attributes.Add("onclick", "if(!GetRejectReason()) return false;")
                Else
                    btnConfirmSingle.Visible = False
                    btnReject.Visible = False
                End If

                If Not helpers.IsEdit Then
                    btnConfirmSingle.Visible = False
                    btnReject.Visible = False
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub dgList_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgList.PageIndexChanged
        Try
            'dgList.CurrentPageIndex = e.NewPageIndex
            BindDataGrid(e.NewPageIndex)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgList_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles dgList.SortCommand
        Try
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

            dgList.CurrentPageIndex = 0
            BindDataGrid(dgList.CurrentPageIndex)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        Try
            BindDataGrid(0)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Protected Sub chkAll_CheckedChanged(sender As Object, e As EventArgs)
        Try
            Dim chkAll As CheckBox = CType(sender, CheckBox)

            For Each item As DataGridItem In dgList.Items
                Dim chkSingle As CheckBox = CType(item.FindControl("chkSingle"), CheckBox)
                If chkSingle.Visible = True Then
                    chkSingle.Checked = chkAll.Checked
                End If

            Next

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ValidateRegistration(data As TrClassRegistration)
        If Not IsClassStillHaveCapacity(data.TrClass) Then
            RejectRegistration(data)
            Throw New Exception("Maaf, peserta kelas " & data.TrClass.ClassCode & " sudah penuh.")
        End If
    End Sub

    Private Function IsClassStillHaveCapacity(classData As TrClass) As Boolean
        Dim capacity As Integer = classData.Capacity

        Dim criterias As New CriteriaComposite(New Criteria(GetType(TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "Status", MatchType.Exact, CInt(EnumTrClassRegistration.DataStatusType.Register)))
        criterias.opAnd(New Criteria(GetType(TrClassRegistration), "TrClass.ID", MatchType.Exact, classData.ID))

        Dim arlTotalPeserta As ArrayList = New TrClassRegistrationFacade(User).Retrieve(criterias)

        If arlTotalPeserta.Count < capacity Then
            Return True
        Else
            Return False
        End If

    End Function



End Class