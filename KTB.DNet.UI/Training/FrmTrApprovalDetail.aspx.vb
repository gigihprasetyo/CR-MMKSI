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
#End Region



Public Class FrmTrApprovalDetail
    Inherits System.Web.UI.Page
    Private helpers As TrainingHelpers = New TrainingHelpers(Me.Page)
    Private SessListAllBooking As String = "SessListAllBooking"
    Private SessTrClass As String = "SessTrClass"
    Private SessSuggestion As String = "SessSuggestion"
    Private SessWaiting As String = "SessWaiting"
    Private SessAllocation As String = "SessAllocation"
    Private SessTempDataToMove As String = "SessTempDataToMove"

    Private Const SUGGESTION As String = "SUGGESTION"
    Private Const WAITING As String = "WAITING"

    Private ReadOnly Property ClassCode As String
        Get
            Return Request.QueryString("classCode")
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            InitPage()
            InitSuggestion()
            btnSimpan.Attributes.Add("OnClick", "return confirm('Apakah yakin akan disimpan?');")
            'btnSimpan.Attributes.Add("OnClick", KTB.DNet.Utility.CommonFunction.PreventDoubleEvent("return confirm('Yakin Data ini akan disimpan?');"))
        Else
            If hdnConfirmMove.Value = "1" Then
                DoEffectConfirmation(True)
            ElseIf hdnConfirmMove.Value = "0" Then
                DoEffectConfirmation(False)
            End If
        End If
    End Sub

    Private Sub InitPage()
        lblClassCode.Text = ClassCode
        Dim classData As TrClass = New TrClassFacade(User).Retrieve(ClassCode)
        lblAllocation.Text = classData.Capacity.ToString()
        lblSisaKelas.Text = (classData.Capacity - JumlahTerdaftar(classData.ID)).ToString
        lblCourseCode.Text = classData.TrCourse.CourseCode & " - " & classData.TrCourse.CourseName
        lblLocation.Text = classData.TrMRTC.Code & " - " & classData.TrMRTC.Address
        lblPeriode.Text = classData.StartDate.ToString("dd/MM/yyyy") & " s/d " & classData.FinishDate.ToString("dd/MM/yyyy")
        If lblSisaKelas.Text.Equals("0") Then
            btnSimpan.Enabled = False
        End If
        helpers.SetSession(SessTrClass, classData)
    End Sub

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

    Private Sub DoEffectConfirmation(ByVal bMoveData As Boolean)
        Dim listSuggestion As List(Of TrBookingCourse) = CType(helpers.GetSession(SessSuggestion), List(Of TrBookingCourse))
        Dim listWaiting As List(Of TrBookingCourse) = CType(helpers.GetSession(SessWaiting), List(Of TrBookingCourse))
        Dim listAllocation As List(Of TrCourseCategoryAllocation) = CType(helpers.GetSession(SessAllocation), List(Of TrCourseCategoryAllocation))
        Dim listDataToMove As List(Of TrBookingCourse) = CType(helpers.GetSession(SessTempDataToMove), List(Of TrBookingCourse))

        If bMoveData = True Then
            For Each dataToMove As TrBookingCourse In listDataToMove
                listSuggestion.Add(dataToMove)
                listWaiting.Remove(dataToMove)
            Next
        End If

        helpers.RemoveSession(SessTempDataToMove)
        hdnConfirmMove.Value = ""
        SortingSuggestion(listSuggestion)
        BindDataGrid(listSuggestion, listWaiting)

    End Sub


    Private Sub InitSuggestion()

        Dim classData As TrClass = CType(helpers.GetSession(SessTrClass), TrClass)
        If classData.FinishDate.DateDay < Me.DateNow Then
            btnSimpan.Enabled = False
            btnLeft.Enabled = False
            btnRight.Enabled = False
        End If

        Dim listAllBookingCourse As List(Of TrBookingCourse) = GetListAllBookingCourse(classData)

        Dim listAllocation As List(Of TrCourseCategoryAllocation) = GetListAllocation()

        Dim listSuggestion As List(Of TrBookingCourse) = New List(Of TrBookingCourse)()
        Dim listWaiting As List(Of TrBookingCourse) = New List(Of TrBookingCourse)()

        GenerateSuggestion(classData, listAllBookingCourse, listAllocation, listSuggestion, listWaiting)

        SortingSuggestion(listSuggestion)

        BindDataGrid(listSuggestion, listWaiting)

    End Sub

    Private Sub SortingSuggestion(ByRef listSuggestion As List(Of TrBookingCourse))
        Dim listApprovedBookingCourse As New List(Of TrBookingCourse)
        Dim listSortedSuggestion As New List(Of TrBookingCourse)

        For Each booking As TrBookingCourse In listSuggestion
            If Not IsNothing(booking.TrClassRegistration) Then
                listApprovedBookingCourse.Add(booking)
            Else
                listSortedSuggestion.Add(booking)
            End If
        Next

        If listApprovedBookingCourse.Count > 0 Then
            listSuggestion = New List(Of TrBookingCourse)
            listSuggestion.AddRange(listSortedSuggestion)
            listSuggestion.AddRange(listApprovedBookingCourse)
        End If

    End Sub


    Private Function GetListAllBookingCourse(ByVal classData As TrClass) As List(Of TrBookingCourse)
        Dim result As New List(Of TrBookingCourse)
        Dim criteria As New CriteriaComposite(New Criteria(GetType(TrBookingCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(TrBookingCourse), "FiscalYear", MatchType.Exact, classData.FiscalYear))
        criteria.opAnd(New Criteria(GetType(TrBookingCourse), "TrCourse.ID", MatchType.Exact, classData.TrCourse.ID))
        criteria.opAnd(New Criteria(GetType(TrBookingCourse), "ValidateDate", MatchType.IsNotNull, Nothing))

        If classData.TrMRTC.IsMainDealer = False Then
            Dim strInsetDealer As String = "(SELECT DealerID FROM TrMRTCDealer WHERE status=1 and RowStatus = 0 AND TrMRTCId = " & classData.TrMRTC.ID & ")"
            criteria.opAnd(New Criteria(GetType(TrBookingCourse), "Dealer.ID", MatchType.InSet, strInsetDealer), "(", True)
            criteria.opOr(New Criteria(GetType(TrBookingCourse), "Dealer.ID", MatchType.Exact, 2), ")", False)
        End If

        Dim strNotInsetId As String = "(SELECT tbc.id from TrBookingCourse tbc" & _
                                        " INNER JOIN TrClassRegistration tcg ON tbc.trclassRegistrationID = tcg.id" & _
                                        " WHERE tcg.classid <> " & classData.ID & ")"

        Dim strNotInsetIdInterSectClass As String = "(SELECT DISTINCT tbc.TraineeID from TrBookingCourse tbc" & _
                                                    " INNER JOIN TrClassRegistration tcg ON tbc.TraineeID = tcg.TraineeID" & _
                                                    " INNER JOIN TrClass class on class.id = tcg.classid " & _
                                                    " WHERE tcg.classid <> " & classData.ID & _
                                                    " AND ('" & classData.StartDate.ToString("yyyy-MM-dd HH:mm:ss") & "' BETWEEN class.StartDate AND class.FinishDate OR '" & classData.FinishDate.ToString("yyyy-MM-dd HH:mm:ss") & "' BETWEEN class.StartDate AND class.FinishDate))"

        criteria.opAnd(New Criteria(GetType(TrBookingCourse), "ID", MatchType.NotInSet, strNotInsetId))
        criteria.opAnd(New Criteria(GetType(TrBookingCourse), "TrTrainee.ID", MatchType.NotInSet, strNotInsetIdInterSectClass))

        Dim arlResult As ArrayList = New TrBookingCourseFacade(User).Retrieve(criteria)

        If arlResult.Count > 0 Then
            result = arlResult.Cast(Of TrBookingCourse).ToList()
        End If

        helpers.SetSession(SessListAllBooking, result)

        Return result

    End Function

    Private Function GetListAllocation() As List(Of TrCourseCategoryAllocation)

        Dim result As List(Of TrCourseCategoryAllocation) = New List(Of TrCourseCategoryAllocation)()
        Dim listBookingCourse As List(Of TrBookingCourse) = CType(helpers.GetSession(SessListAllBooking), List(Of TrBookingCourse))
        Dim listDealerInBooking As List(Of String) = listBookingCourse.Select(Function(x) x.Dealer.DealerCode.ToString()).Distinct().ToList()
        Dim stringInsetDealerAlloc As String = GenerateStringInset(listDealerInBooking)
        Dim classData As TrClass = CType(helpers.GetSession(SessTrClass), TrClass)


        'Dim criteria As New CriteriaComposite(New Criteria(GetType(TrClassAllocation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        'criteria.opAnd(New Criteria(GetType(TrClassAllocation), "TrClass.ID", MatchType.Exact, classData.ID))
        'criteria.opAnd(New Criteria(GetType(TrClassAllocation), "Dealer.ID", MatchType.InSet, "(" & stringInsetDealerAlloc & ")"))

        'Dim arlResult As ArrayList = New TrClassAllocationFacade(User).Retrieve(criteria)
        Dim arlResult As ArrayList = New TrCourseCategoryAllocationFacade(User).RetrieveAllocation(2, stringInsetDealerAlloc.GenerateInSet(), classData.TrCourse.Category.Code.GenerateInSet())

        If arlResult.Count > 0 Then
            result = arlResult.Cast(Of TrCourseCategoryAllocation).ToList()
        End If

        helpers.SetSession(SessAllocation, result)

        Return result

    End Function

    Private Function GenerateStringInset(ByVal list As List(Of String)) As String
        Dim result As String = String.Empty
        For Each item As String In list
            result &= item & ";"
        Next

        If result.Length > 0 Then
            result = result.Remove(result.Length - 1)
        End If
        Return result
    End Function

    Private Sub GenerateSuggestion(ByVal classData As TrClass, ByVal listAllBookingCourse As List(Of TrBookingCourse), ByVal listAllocation As List(Of TrCourseCategoryAllocation), ByRef listSuggestion As List(Of TrBookingCourse), ByRef listWaiting As List(Of TrBookingCourse))

        Dim listOrderedDealerId As List(Of Integer) = GenerateListDealerByCreatedTime(listAllBookingCourse)
        Dim listSiswaKelas As List(Of TrBookingCourse) = listAllBookingCourse.Where(Function(x) Not IsNothing(x.TrClassRegistration)).ToList()
        Dim siswaTerdaftar As Integer = listSiswaKelas.Where(Function(x) x.TrClassRegistration.TrClass.ID = classData.ID).Count

        For Each dealerId As Integer In listOrderedDealerId
            Dim allocation As TrCourseCategoryAllocation = listAllocation.FirstOrDefault(Function(x) x.Dealer.ID = dealerId)
            
            Dim dealerBooking As List(Of TrBookingCourse) = listAllBookingCourse.Where(Function(x) x.Dealer.ID = dealerId).OrderBy(Function(x) x.PrioritySequence).ToList()

            For Each booking As TrBookingCourse In dealerBooking

                If Not IsNothing(booking.TrClassRegistration) Then
                    listSuggestion.Add(booking)
                    Continue For
                End If

                If allocation Is Nothing Then
                    Throw New Exception("Alokasi kelas untuk dealer ini tidak ditemukan")
                End If

                Dim existingDealerCount As Integer = listSuggestion.Where(Function(x) x.Dealer.ID = booking.Dealer.ID).Count()
                Dim dataSuggest As Integer = listSuggestion.Where(Function(x) listSiswaKelas.Where(Function(y) y.ID = x.ID).Count = 0).Count

                If existingDealerCount < allocation.Allocated And (dataSuggest + siswaTerdaftar) < classData.Capacity Then
                    If classData.FinishDate.DateDay < Me.DateNow Then
                        listWaiting.Add(booking)
                    Else
                        listSuggestion.Add(booking)
                    End If

                Else
                    listWaiting.Add(booking)
                End If
            Next
        Next

        LimitListWaitingByReference(listWaiting)

    End Sub

    Private Sub MoveData(ByVal listID As List(Of Integer), ByVal direction As String)

        Dim listSuggestion As List(Of TrBookingCourse) = CType(helpers.GetSession(SessSuggestion), List(Of TrBookingCourse))
        Dim listWaiting As List(Of TrBookingCourse) = CType(helpers.GetSession(SessWaiting), List(Of TrBookingCourse))
        Dim listAllocation As List(Of TrCourseCategoryAllocation) = CType(helpers.GetSession(SessAllocation), List(Of TrCourseCategoryAllocation))

        If direction = WAITING Then
            For Each id As Integer In listID
                Dim dataToMove As TrBookingCourse = listSuggestion.FirstOrDefault(Function(x) x.ID = id)
                listSuggestion.Remove(dataToMove)
                listWaiting.Add(dataToMove)
            Next
            SortingSuggestion(listSuggestion)
            BindDataGrid(listSuggestion, listWaiting)

        ElseIf direction = SUGGESTION Then
            Dim classData As TrClass = CType(helpers.GetSession(SessTrClass), TrClass)
            If Not listSuggestion.Count + listID.Count <= classData.Capacity Then
                SortingSuggestion(listSuggestion)
                BindDataGrid(listSuggestion, listWaiting)
                MessageBox.Show("Kapasitas kelas tidak mencukupi")
            Else
                Dim listOverAllocationData As List(Of TrBookingCourse) = New List(Of TrBookingCourse)
                For Each id As Integer In listID
                    Dim dataToMove As TrBookingCourse = listWaiting.FirstOrDefault(Function(x) x.ID = id)
                    Dim allocation As TrCourseCategoryAllocation = listAllocation.FirstOrDefault(Function(x) x.Dealer.ID = dataToMove.Dealer.ID)
                    Dim existingDealerCount As Integer = listSuggestion.Where(Function(x) x.Dealer.ID = dataToMove.Dealer.ID).Count()
                    If existingDealerCount < allocation.Allocated Then
                        listSuggestion.Add(dataToMove)
                        listWaiting.Remove(dataToMove)
                    Else
                        listOverAllocationData.Add(dataToMove)
                    End If
                Next

                helpers.SetSession(SessTempDataToMove, listOverAllocationData)
                Dim listOverAllocationDealerCode As String = GenerateListDealerCode(listOverAllocationData)
                If Not listOverAllocationDealerCode.IsNullorEmpty Then
                    MessageBox.Confirm("Jumlah siswa pada dealer " & listOverAllocationDealerCode & " telah melebihi batas alokasi, apakah anda ingin tetap memasukkan ke list sugesti ?", "hdnConfirmMove")
                Else
                    hdnConfirmMove.Value = "1"
                End If

            End If

        End If


    End Sub

    Private Sub BindDataGrid(ByVal listSuggestion As List(Of TrBookingCourse), ByVal listWaiting As List(Of TrBookingCourse))
        helpers.SetSession(SessSuggestion, listSuggestion)
        helpers.SetSession(SessWaiting, listWaiting)

        gdSuggestion.DataSource = Nothing
        gdSuggestion.DataSource = listSuggestion
        gdSuggestion.DataBind()

        gdWaiting.DataSource = Nothing
        gdWaiting.DataSource = listWaiting
        gdWaiting.DataBind()
    End Sub

    Private Sub btnRight_Click(sender As Object, e As EventArgs) Handles btnRight.Click
        Try
            Dim listId As List(Of Integer) = New List(Of Integer)

            For Each row As System.Web.UI.WebControls.GridViewRow In gdSuggestion.Rows
                Dim chk As CheckBox = CType(row.FindControl("chkMark"), CheckBox)
                If chk.Checked Then
                    Dim hdnIDBooking As HiddenField = CType(row.FindControl("hdnIDBooking"), HiddenField)
                    listId.Add(CInt(hdnIDBooking.Value))
                End If
            Next

            If listId.Count > 0 Then
                MoveData(listId, WAITING)
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Function GenerateListDealerCode(listOverAllocationData As List(Of TrBookingCourse)) As String
        Dim result As String = String.Empty
        Dim listCode As List(Of String) = New List(Of String)

        listCode = listOverAllocationData.Select(Function(x) x.Dealer.DealerCode).Distinct().ToList()

        For Each code As String In listCode
            result &= code & ","
        Next

        If result.Length > 0 Then
            result = result.Remove(result.Length - 1)
        End If

        Return result
    End Function

    Private Sub btnLeft_Click(sender As Object, e As EventArgs) Handles btnLeft.Click
        Try
            Dim listId As List(Of Integer) = New List(Of Integer)

            For Each row As System.Web.UI.WebControls.GridViewRow In gdWaiting.Rows
                Dim chk As CheckBox = CType(row.FindControl("chkMark"), CheckBox)

                If chk.Checked Then
                    Dim hdnIDBooking As HiddenField = CType(row.FindControl("hdnIDBooking"), HiddenField)
                    listId.Add(CInt(hdnIDBooking.Value))
                End If
            Next

            If listId.Count > 0 Then
                MoveData(listId, SUGGESTION)
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnKembali_Click(sender As Object, e As EventArgs) Handles btnKembali.Click
        Response.Redirect("FrmTrApproval.aspx?area=2")
    End Sub

    Private Sub btnSimpan_Click(sender As Object, e As EventArgs) Handles btnSimpan.Click
        Try
            Dim func As New TrBookingCourseFacade(Me.User)
            Dim classData As TrClass = CType(helpers.GetSession(SessTrClass), TrClass)
            Dim listSuggestion As New List(Of TrBookingCourse)
            Dim listApprove As New List(Of TrBookingCourse)
            Dim listWaiting As New List(Of TrBookingCourse)

            For Each iBooking As TrBookingCourse In CType(helpers.GetSession(SessSuggestion), List(Of TrBookingCourse))
                Dim nBooking As TrBookingCourse = func.Retrieve(iBooking.ID)
                If IsNothing(nBooking.TrClassRegistration) Then
                    listSuggestion.Add(iBooking)
                Else
                    listApprove.Add(nBooking)
                End If
            Next
            For Each iBooking As TrBookingCourse In CType(helpers.GetSession(SessWaiting), List(Of TrBookingCourse))
                Dim nBooking As TrBookingCourse = func.Retrieve(iBooking.ID)
                If IsNothing(nBooking.TrClassRegistration) Then
                    listWaiting.Add(iBooking)
                End If
            Next

            Dim iResult = New TrClassRegistrationFacade(User).SaveSuggestion(classData, listSuggestion)

            If iResult <> -1 Then
                listSuggestion.AddRange(listApprove)
                BindDataGrid(listSuggestion, listWaiting)
                MessageBox.Show(SR.SaveSuccess)
            Else
                MessageBox.Show(SR.SaveFail)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgSuggestion_ItemDataBound(sender As Object, e As DataGridItemEventArgs)
        If Not e.Item.DataItem Is Nothing Then
            Dim RowValue As TrBookingCourse = CType(e.Item.DataItem, TrBookingCourse)
            Dim chk As CheckBox = CType(e.Item.FindControl("chkMark"), CheckBox)

            If Not IsNothing(RowValue.TrClassRegistration) Then
                chk.Visible = False
            End If
        End If
    End Sub

    Private Function GenerateListDealerByCreatedTime(listAllBookingCourse As List(Of TrBookingCourse)) As List(Of Integer)

        Dim listOrderedDealer As List(Of Integer) = New List(Of Integer)

        Dim orderedListByCreatedTime As List(Of TrBookingCourse) = listAllBookingCourse.OrderBy(Function(x) x.ValidateDate).ToList()

        If orderedListByCreatedTime.Count > 0 Then
            listOrderedDealer = orderedListByCreatedTime.Select(Function(x) x.Dealer.ID).Distinct().ToList()
        End If

        Return listOrderedDealer

    End Function

    Private Sub gdSuggestion_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gdSuggestion.RowDataBound
        If Not e.Row.DataItem Is Nothing Then
            Dim RowValue As TrBookingCourse = CType(e.Row.DataItem, TrBookingCourse)
            Dim chk As CheckBox = CType(e.Row.FindControl("chkMark"), CheckBox)
            Dim hdnIDBooking As HiddenField = CType(e.Row.FindControl("hdnIDBooking"), HiddenField)
            hdnIDBooking.Value = RowValue.ID
            If Not IsNothing(RowValue.TrClassRegistration) Then
                chk.Enabled = False
            End If
            Dim classData As TrClass = CType(helpers.GetSession(SessTrClass), TrClass)
            If classData.FinishDate.DateDay < Me.DateNow Then
                chk.Enabled = False
            End If
        End If
    End Sub

    Private Sub gdWaiting_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gdWaiting.RowDataBound
        If Not e.Row.DataItem Is Nothing Then
            Dim RowValue As TrBookingCourse = CType(e.Row.DataItem, TrBookingCourse)
            Dim hdnIDBooking As HiddenField = CType(e.Row.FindControl("hdnIDBooking"), HiddenField)
            Dim chk As CheckBox = CType(e.Row.FindControl("chkMark"), CheckBox)
            hdnIDBooking.Value = RowValue.ID
            Dim classData As TrClass = CType(helpers.GetSession(SessTrClass), TrClass)
            If classData.FinishDate.DateDay < Me.DateNow Then
                chk.Enabled = False
            End If
        End If
    End Sub

    Private Sub LimitListWaitingByReference(ByRef listWaiting As List(Of TrBookingCourse))
        Dim newList As New List(Of TrBookingCourse)
        Dim limit As Integer = GetLimitFromReference()

        If limit <> 0 Then
            For Each booking As TrBookingCourse In listWaiting
                Dim totalBookingPerDealer As Integer = newList.Where(Function(x) x.Dealer.ID = booking.Dealer.ID).ToList().Count()
                If totalBookingPerDealer < limit Then
                    newList.Add(booking)
                End If
            Next
        End If

        listWaiting = newList
    End Sub

    Private Function GetLimitFromReference() As Integer
        Try
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Reference), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(Reference), "Type", MatchType.Exact, "TRASS"))
            criterias.opAnd(New Criteria(GetType(Reference), "Code", MatchType.Exact, "MAXHOLD"))

            Dim defaultLimit As Reference = New ReferenceFacade(User).Retrieve(criterias)(0)
            Return CInt(defaultLimit.Description)

        Catch ex As Exception
            Return 0
        End Try

    End Function

End Class