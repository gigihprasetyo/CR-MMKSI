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


Public Class FrmDaftarTrainingMKS
    Inherits System.Web.UI.Page

    Private ReadOnly Property ClassCode As String
        Get
            Return Request.QueryString("classCode")
        End Get
    End Property

    Private helpers As New TrainingHelpers(Me.Page)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ViewState("CurrentSortColumn") = "ID"
            ViewState("CurrentSortDirect") = Sort.SortDirection.ASC
            InitInformationClass()
            BindDataGrid()
        End If
    End Sub

    Public Sub InitInformationClass()
        Dim func As New TrClassRegistrationFacade(User)
        Dim data As TrClass = New TrClassFacade(User).Retrieve(Me.ClassCode)
        Dim siswaTerdaftar As New List(Of TrClassRegistration)

        If Not IsNothing(data) Then
            lblClassCode.Text = data.ClassCode
            lblClassName.Text = data.ClassName
            lblStartDate.Text = data.StartDate.DateToString
            lblFinishDate.Text = data.FinishDate.DateToString
            lblLocation.Text = data.TrMRTC.Name
            lblAllocatedTot.Text = (data.Capacity - JumlahTerdaftar(data.ID)).ToString()
            

        End If
    End Sub

    Private Function JumlahTerdaftar(ByVal id As Integer) As Integer
        Dim objCAFac As TrBookingCourseFacade = New TrBookingCourseFacade(User)
        Dim arlCA As ArrayList = New ArrayList
        Dim crtCA As CriteriaComposite
        Dim Tot As Integer = 0

        crtCA = New CriteriaComposite(New Criteria(GetType(TrBookingCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crtCA.opAnd(New Criteria(GetType(TrBookingCourse), "TrClassRegistration.TrClass.ID", id))
        crtCA.opAnd(New Criteria(GetType(TrBookingCourse), "Dealer.ID", MatchType.No, Me.GetDealer().ID))
        Dim aggCA As Aggregate = New Aggregate(GetType(TrBookingCourse), "ID", AggregateType.Count)
        Tot = objCAFac.GetAggregateResult(aggCA, crtCA)
        Return Tot
    End Function

    Private Function Criterias()
        Dim criteria As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrTrainee), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrTrainee), "Dealer.ID", MatchType.Exact, Me.GetDealer().ID))
        criteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrTrainee), "Status", MatchType.Exact, CType(EnumTrTrainee.TrTraineeStatus.Active, String)))

        Return criteria
    End Function

    Private Sub BindDataGrid()
        Dim func As New TrTraineeFacade(User)
        Dim funcReg As New TrClassRegistrationFacade(User)
        Dim data As TrClass = New TrClassFacade(User).Retrieve(Me.ClassCode)
        Dim siswaTerdaftar As New List(Of TrClassRegistration)
        Dim sortColl As SortCollection = New SortCollection
        If (Not IsNothing(CType(ViewState("CurrentSortColumn"), String))) And (Not IsNothing(CType(ViewState("CurrentSortColumn"), String))) Then
            sortColl.Add(New Sort(GetType(TrTrainee), CType(ViewState("CurrentSortColumn"), String), CType(ViewState("CurrentSortDirect"), Sort.SortDirection)))
        Else
            sortColl = Nothing
        End If
        Dim dataTrainee As ArrayList = func.Retrieve(Criterias(), sortColl)
        Dim listTrainee As New List(Of TrTrainee)

        Dim criteria As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "TrClass.ID", MatchType.Exact, Data.ID))
        criteria.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassRegistration), "Dealer.ID", MatchType.Exact, Me.GetDealer().ID))

        Dim dataSiswa As ArrayList = funcReg.Retrieve(criteria)
        If dataSiswa.IsItems Then
            siswaTerdaftar = dataSiswa.Cast(Of TrClassRegistration).ToList()
        End If
        lblAllocatedReg.Text = siswaTerdaftar.Count.ToString()

        For Each item As TrTrainee In dataTrainee
            If siswaTerdaftar.IsItems Then
                If Not siswaTerdaftar.Where(Function(x) x.TrTrainee.ID = item.ID).IsData Then
                    item.IsTraineeRegistered = False
                End If
            Else
                item.IsTraineeRegistered = False
            End If

            If Not item.IsTraineeRegistered Then
                Dim registrationstate As String = New TrBookingCourseFacade(User).GetCourseRegistrationState(item.ID, data.TrCourse.ID, _
                                                  data.FiscalYear, Me.DateNow)
                If registrationstate.ToLower.Equals("belum terdaftar") Then
                    listTrainee.Add(item)
                End If
            Else
                listTrainee.Add(item)
            End If
        Next
        dtgTrainee2.DataSource = listTrainee
        dtgTrainee2.PageSize = dataTrainee.Count
        dtgTrainee2.DataBind()
    End Sub

    Private Sub dtgTrainee2_RowDataBound(ByVal sender As Object, e As GridViewRowEventArgs) Handles dtgTrainee2.RowDataBound
        If Not IsNothing(e.Row.DataItem) Then
            Dim rowValue As TrTrainee = CType(e.Row.DataItem, TrTrainee)
            Dim lblNo As Label = e.Row.FindControl("lblNo")
            Dim lblID As Label = e.Row.FindControl("lblID")
            Dim lblStartWork As Label = e.Row.FindControl("lblStartWork")
            Dim lblJobposition As Label = e.Row.FindControl("lblJobposition")
            Dim chk As CheckBox = e.Row.FindControl("chkItemChecked")
            Dim hdnIsregister As HiddenField = e.Row.FindControl("hdnIsregister")


            lblNo.Text = e.CreateNumberPage()
            lblID.Text = rowValue.ID
            lblStartWork.Text = rowValue.StartWorkingDate.DateToString()
            lblJobposition.Text = rowValue.RefJobPosition.Description
            chk.Attributes.Add("onclick", "CheckEnability();")
            chk.Checked = rowValue.IsTraineeRegistered
            If chk.Checked Then
                hdnIsregister.Value = "1"
                e.Row.BackColor = Color.LightSalmon
            End If

        End If
    End Sub

    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        If IsNothing(Request.QueryString("isClass")) Then
            Response.Redirect("FrmListPendaftaranASS.aspx")
        Else
            Dim sHClass As New SessionHelper
            sHClass.SetSession("backRes", 1)
            'sHClass.SetSession("DaftarMKS", 1)
            Response.Redirect("FrmTrClassAss.aspx")
        End If

    End Sub

    Protected Sub btnDaftar_Click(sender As Object, e As EventArgs) Handles btnDaftar.Click
        Dim dataDic As Dictionary(Of String, List(Of TrTrainee)) = GetDataTrainee()
        If dataDic.Count.Equals(0) Then
            MessageBox.Show("Tidak ada perubahan data!")
        Else
            Dim funcB As New TrBookingCourseFacade(User)
            Dim funcBC As New TrBookingClassFacade(User)
            Dim funcC As New TrClassRegistrationFacade(User)
            Dim dataClass As TrClass = New TrClassFacade(User).Retrieve(lblClassCode.Text)
            For Each itemDic As KeyValuePair(Of String, List(Of TrTrainee)) In dataDic
                For Each dataTr As TrTrainee In itemDic.Value
                    Dim dataBooking As New TrBookingCourse
                    Dim dataReg As New TrClassRegistration
                    Dim dataBClass As New TrBookingClass
                    Select Case itemDic.Key
                        Case "new"
                            dataReg.TrTrainee = dataTr
                            dataReg.TrClass = dataClass
                            dataReg.Dealer = dataTr.Dealer
                            dataReg.Status = CType(EnumTrClassRegistration.DataStatusType.Register, String)
                            dataReg.RegistrationDate = Me.DateNow
                            dataReg.ID = funcC.Insert(dataReg)

                            dataBooking.TrTrainee = dataTr
                            dataBooking.Dealer = dataTr.Dealer
                            dataBooking.TrClassRegistration = dataReg
                            dataBooking.TrCourse = dataClass.TrCourse
                            dataBooking.FiscalYear = dataClass.FiscalYear
                            dataBooking.ValidateDate = Me.DateNow
                            dataBooking.ID = funcB.Insert(dataBooking)

                            dataBClass.TrBookingCourse = dataBooking
                            dataBClass.TrClass = dataClass
                            dataBClass.TrClassRegistration = dataReg
                            funcBC.Insert(dataBClass)
                        Case "remove"
                            Dim criteria As New CriteriaComposite(New Criteria(GetType(TrBookingClass), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            criteria.opAnd(New Criteria(GetType(TrBookingClass), "TrClass.ID", MatchType.Exact, dataClass.ID))
                            criteria.opAnd(New Criteria(GetType(TrBookingClass), "TrClassRegistration.TrTrainee.ID", MatchType.Exact, dataTr.ID))

                            dataBClass = CType(funcBC.Retrieve(criteria)(0), TrBookingClass)
                            dataBooking = dataBClass.TrBookingCourse
                            dataReg = dataBClass.TrClassRegistration

                            funcBC.Delete(dataBClass)
                            funcB.Delete(dataBooking)
                            funcC.Delete(dataReg)
                    End Select
                Next
            Next
            Dim sHClass As New SessionHelper
            sHClass.SetSession("DaftarMKS", 1)
            MessageBox.Show(SR.SaveSuccess)
            btnBack_Click(sender, e)
            'Response.Redirect("FrmListPendaftaranASS.aspx")
        End If
    End Sub

    Private Function GetDataTrainee() As Dictionary(Of String, List(Of TrTrainee))
        Dim dataDic As New Dictionary(Of String, List(Of TrTrainee))
        Dim dataNew As New List(Of TrTrainee)
        Dim dataRemove As New List(Of TrTrainee)
        For Each itemData As GridViewRow In dtgTrainee2.Rows
            Dim lblID As Label = itemData.FindControl("lblID")
            Dim chk As CheckBox = itemData.FindControl("chkItemChecked")
            Dim hdnIsregister As HiddenField = itemData.FindControl("hdnIsregister")

            If chk.Checked Then
                If hdnIsregister.Value.IsNullorEmpty Then
                    dataNew.Add(New TrTraineeFacade(User).Retrieve(CInt(lblID.Text)))
                End If
            End If

            If hdnIsregister.Value.Equals("1") Then
                If Not chk.Checked Then
                    dataRemove.Add(New TrTraineeFacade(User).Retrieve(CInt(lblID.Text)))
                End If
            End If
        Next
        If dataNew.IsItems Then
            dataDic.Add("new", dataNew)
        End If
        If dataRemove.IsItems Then
            dataDic.Add("remove", dataRemove)
        End If
        Return dataDic
    End Function

End Class