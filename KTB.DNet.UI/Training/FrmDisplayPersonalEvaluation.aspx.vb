Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.Helper
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports System.Drawing.Color
Imports System.Web.UI.WebControls.BorderStyle
Imports System.Text
Imports System.Collections.Generic
Imports System.Linq

Public Class FrmDisplayPersonalEvaluation
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private _sessHelper As SessionHelper = New SessionHelper
    Protected WithEvents lblBlank As System.Web.UI.WebControls.Label
    Protected WithEvents lblCatatan As System.Web.UI.WebControls.Label
    Protected WithEvents lblRekomendasi As System.Web.UI.WebControls.Label
    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Const EHT1_REF_CODE As String = "EHT1"
    Protected WithEvents TextBox1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents TextBox2 As System.Web.UI.WebControls.TextBox
    Protected WithEvents TextBox3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents TextBox4 As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblSkala As System.Web.UI.WebControls.Label
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button
    Protected WithEvents lblCatat As System.Web.UI.WebControls.Label
    Protected WithEvents Repeater1 As System.Web.UI.WebControls.Repeater
    Protected WithEvents rptPenilaianSikap As System.Web.UI.WebControls.Repeater
    Protected WithEvents lblInitial As System.Web.UI.WebControls.Label
    Protected WithEvents lblAverage As System.Web.UI.WebControls.Label
    Protected WithEvents lblRangking As System.Web.UI.WebControls.Label
    Protected WithEvents lblStatus As System.Web.UI.WebControls.Label
    Protected WithEvents rptTest As System.Web.UI.WebControls.Repeater
    Protected WithEvents lblFinal As System.Web.UI.WebControls.Label
    Protected WithEvents lblTraineeName As System.Web.UI.WebControls.Label
    Protected WithEvents lblRegNo As System.Web.UI.WebControls.Label
    Protected WithEvents lblDealerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblCityName As System.Web.UI.WebControls.Label
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents lblClassName As System.Web.UI.WebControls.Label
    Protected WithEvents lblClass As System.Web.UI.WebControls.Label
    Protected WithEvents lblClassCode As System.Web.UI.WebControls.Label
    Protected WithEvents lblLokasi As System.Web.UI.WebControls.Label
    Protected WithEvents lblTanggal As System.Web.UI.WebControls.Label
    Protected WithEvents lblManagerName As System.Web.UI.WebControls.Label
    Protected WithEvents lblReporter As System.Web.UI.WebControls.Label
    Protected WithEvents lblStartPeriod As System.Web.UI.WebControls.Label
    Protected WithEvents lblEndPeriod As System.Web.UI.WebControls.Label
    Private Const EHT_REF_CODE As String = "EHT"
    Private Const MNGR_REF_CODE As String = "MNGR"
    Private Const RPTR_REF_CODE As String = "RPTR"
    Private Const TMPTR_REF_CODE As String = "TMPTR"

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    
    Private ReadOnly Property AreaId As String
        Get
            Return Request.QueryString("area")
        End Get
    End Property

    Private ReadOnly Property TYPE_REF_CODE As String
        Get
            If AreaId.IsNullorEmpty Then
                Return "TR"
            Else
                Select Case AreaId
                    Case "1"
                        Return "TRSLS"
                    Case "2"
                        Return "TRASS"
                    Case "3"
                        Return "TRCS"
                End Select
            End If
        End Get
    End Property


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''Put user code to initialize the page here
        If Not (Session("SessObjClassReg")) Is Nothing Then
            Dim strArrList As ArrayList = New ArrayList
            Dim ObjTrClassReg As TrClassRegistration = CType(_sessHelper.GetSession("SessObjClassReg"), TrClassRegistration)


            If Not (ObjTrClassReg) Is Nothing Then
                If AreaId.IsNullorEmpty Then
                    lblRegNo.Text = CType(ObjTrClassReg.TrTrainee.ID, String)
                Else
                    Select Case AreaId
                        Case "1"
                            lblRegNo.Text = ObjTrClassReg.TrTrainee.ListTrTraineeSalesmanHeader.FirstOrDefault(Function(x) _
                                        x.JobPositionAreaID = CInt(AreaId)).SalesmanHeader.SalesmanCode
                        Case "2"
                            lblRegNo.Text = CType(ObjTrClassReg.TrTrainee.ID, String)
                        Case "3"
                            lblRegNo.Text = ObjTrClassReg.TrTrainee.ListTrTraineeSalesmanHeader.FirstOrDefault(Function(x) _
                                        x.JobPositionAreaID = CInt(AreaId)).SalesmanHeader.SalesmanCode
                    End Select
                End If

                lblTraineeName.Text = ObjTrClassReg.TrTrainee.Name
                lblDealerName.Text = ObjTrClassReg.Dealer.DealerName
                lblCityName.Text = ObjTrClassReg.Dealer.City.CityName
                lblClassName.Text = ObjTrClassReg.TrClass.ClassName
                lblClassCode.Text = ObjTrClassReg.TrClass.ClassCode
                lblStartPeriod.Text = ObjTrClassReg.TrClass.StartDate.ToString("dd MMM yyyy")
                lblEndPeriod.Text = ObjTrClassReg.TrClass.FinishDate.ToString("dd MMM yyyy")

                BindDataTest(ObjTrClassReg)
                BindDataSikap(ObjTrClassReg)

                Dim strInitialTest As String = ObjTrClassReg.InitialTest.ToString("#.##")
                Dim strFinalTest As String = ObjTrClassReg.FinalTest.ToString("#.##")
                Dim strAverageTest As String = ObjTrClassReg.Avarage.ToString("#.##")
                Dim strRank As String = ObjTrClassReg.Rank.ToString("###")

                lblInitial.Text = strInitialTest
                lblAverage.Text = strAverageTest
                lblFinal.Text = strFinalTest
                lblRangking.Text = strRank + " dari " + CStr(TotalStudent(ObjTrClassReg))
                Try
                    lblStatus.Text = New EnumTrClassRegistration().StatusByIndex(ObjTrClassReg.Status)
                Catch
                End Try

                lblTanggal.Text = ObjTrClassReg.TrClass.FinishDate.ToString("dd-MMM-yyyy")
                lblLokasi.Text = GetLocation()
                lblManagerName.Text = GetManager()
                lblReporter.Text = GetReporter(ObjTrClassReg.TrClass)
            End If
            lblCatatan.Text = ObjTrClassReg.Notes
            lblRekomendasi.Text = GetRecommended()
        Else
            Response.Redirect("../login.aspx#expired")
        End If

    End Sub

    'Private Function GetNote() As String
    '    Return CType(New ReferenceFacade(User).RetrieveActiveList(TYPE_REF_CODE, EHT1_REF_CODE), Reference).Description
    'End Function
    Private Function GetRecommended() As String
        Return CType(New ReferenceFacade(User).RetrieveActiveList(TYPE_REF_CODE, EHT_REF_CODE), Reference).Description
    End Function
    Private Function GetManager() As String
        Return CType(New ReferenceFacade(User).RetrieveActiveList(TYPE_REF_CODE, MNGR_REF_CODE), Reference).Description
    End Function
    Private Function GetReporter(ByVal objClass As TrClass) As String
        'Return CType(New ReferenceFacade(User).RetrieveActiveList(TYPE_REF_CODE, RPTR_REF_CODE), Reference).Description
        Dim sbReporter As StringBuilder = New StringBuilder

        If objClass.Trainer1 <> String.Empty Then
            sbReporter.Append(objClass.Trainer1)
        End If

        If objClass.Trainer2 <> String.Empty Then
            sbReporter.Append(" / " & objClass.Trainer2)
        End If
        sbReporter.Append("<BR>")
        sbReporter.Append("Instruktur")
        Return sbReporter.ToString()
    End Function
    Private Function GetLocation() As String
        Return CType(New ReferenceFacade(User).RetrieveActiveList(TYPE_REF_CODE, TMPTR_REF_CODE), Reference).Description
    End Function
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        If Not (Session("SessObjClassReg")) Is Nothing Then
            Dim areas As String = String.Empty
            If AreaId.NotNullorEmpty Then
                areas = "&area=" + AreaId
            End If

            If Not Request.QueryString("Rank") Is Nothing Then
                Response.Redirect("../Training/FrmCourseEvaluationList.aspx?form=" + "FrmDisplayPersonalEvaluation.aspx?RankBack=True" + areas)
            Else
                Response.Redirect("../Training/FrmCourseEvaluationList.aspx?form=" + "FrmDisplayPersonalEvaluation.aspx" + areas)
            End If
        End If

    End Sub

    Private Sub BindDataSikap(ByVal objTrClassReg As TrClassRegistration)
        Dim data As ArrayList = New ArrayList

        Dim critCourseEval2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critCourseEval2.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "TrCourse.ID", MatchType.Exact, objTrClassReg.TrClass.TrCourse.ID))
        critCourseEval2.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "Type", MatchType.Exact, CType(EnumTrEvaluationType.TrEvaluationType.Sikap, String)))
        Dim TrCourseEvalColl2 As ArrayList = New TrCourseEvaluationFacade(User).Retrieve(critCourseEval2)

        For Each objTrCourseEval As TrCourseEvaluation In TrCourseEvalColl2
            Dim _strCourseEvalName As String = KTB.DNet.Utility.CommonFunction.GetNamaKhususEvalTraining(objTrClassReg.TrClass.ID.ToString(), objTrCourseEval.ID)
            If (_strCourseEvalName = String.Empty) Then
                _strCourseEvalName = objTrCourseEval.Name
            End If
            Dim item As ListItem = New ListItem(_strCourseEvalName, GetCharTestResult(objTrClassReg.TrCertificateLines, objTrCourseEval.ID, CType(EnumTrEvaluationType.TrEvaluationType.Sikap, String)))

            'Dim item As ListItem = New ListItem(objTrCourseEval.Name, GetCharTestResult(objTrClassReg.TrCertificateLines, objTrCourseEval.ID, CType(EnumTrEvaluationType.TrEvaluationType.Sikap, String)))

            data.Add(item)
        Next

        critCourseEval2 = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critCourseEval2.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "TrCourse.ID", MatchType.Exact, objTrClassReg.TrClass.TrCourse.ID))
        critCourseEval2.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "Type", MatchType.Exact, CType(EnumTrEvaluationType.TrEvaluationType.Prestasi, String)))
        TrCourseEvalColl2 = New TrCourseEvaluationFacade(User).Retrieve(critCourseEval2)

        For Each objTrCourseEval As TrCourseEvaluation In TrCourseEvalColl2
            'Check If Class Id Mapping on TrClassNumEvaluation
            Dim _trClassNumEvaluationFacade As New TrClassNumEvaluationFacade(User)
            Dim critParam As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrClassNumEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critParam.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassNumEvaluation), "TrClass.ID", MatchType.Exact, objTrClassReg.TrClass.ID))
            critParam.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassNumEvaluation), "TrCourseEvaluation.ID", MatchType.Exact, objTrCourseEval.ID))

            Dim _lstClassNumEval As New ArrayList
            _lstClassNumEval = _trClassNumEvaluationFacade.Retrieve(critParam)
            If (_lstClassNumEval.Count > 0) Then


            Dim _strCourseEvalName As String = KTB.DNet.Utility.CommonFunction.GetNamaKhususEvalTraining(objTrClassReg.TrClass.ID.ToString(), objTrCourseEval.ID)
            If (_strCourseEvalName = String.Empty) Then
                _strCourseEvalName = objTrCourseEval.Name
            End If
            Dim item As ListItem = New ListItem(_strCourseEvalName, GetCharTestResult(objTrClassReg.TrCertificateLines, objTrCourseEval.ID, CType(EnumTrEvaluationType.TrEvaluationType.Sikap, String)))

            'Dim item As ListItem = New ListItem(objTrCourseEval.Name, GetCharTestResult(objTrClassReg.TrCertificateLines, objTrCourseEval.ID, CType(EnumTrEvaluationType.TrEvaluationType.Prestasi, String)))
                data.Add(item)
            End If
        Next
        'KTB.DNet.Utility.CommonFunction.GetNamaKhususEvalTraining(

        rptPenilaianSikap.DataSource = data
        rptPenilaianSikap.DataBind()
    End Sub

    Private Function GetCharTestResult(ByVal objTrCertificateLines As ArrayList, ByVal courseEvaluationID As Integer, ByVal courseEvalType As String) As String
        For Each objTrCertificateline As TrCertificateLine In objTrCertificateLines
            If objTrCertificateline.TrCourseEvaluation.Type = courseEvalType _
            And objTrCertificateline.TrCourseEvaluation.ID = courseEvaluationID Then
                If objTrCertificateline.TrCourseEvaluation.Type = CType(EnumTrEvaluationType.TrEvaluationType.Angka, String) Then
                    Return CStr(objTrCertificateline.NumTestResult)
                ElseIf objTrCertificateline.TrCourseEvaluation.Type <> CType(EnumTrEvaluationType.TrEvaluationType.Angka, String) _
                And objTrCertificateline.CharTestResult <> "" Then
                    Return objTrCertificateline.CharTestResult
                Else
                    Return "-"
                End If
            End If
        Next
        Return "-"
    End Function

    Private Sub rptPenilaianSikap_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptPenilaianSikap.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblKeterangan As Label = e.Item.FindControl("lblKeterangan")
            Dim lblNilai As Label = e.Item.FindControl("lblNilai")
            Dim item As ListItem = CType(e.Item.DataItem, ListItem)
            lblKeterangan.Text = item.Text
            lblNilai.Text = item.Value
        End If
    End Sub

    Private Sub BindDataTest(ByVal objTrClassReg As TrClassRegistration)
        Dim data As ArrayList = New ArrayList

        Dim critCourseEval2 As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        critCourseEval2.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "TrCourse.ID", MatchType.Exact, objTrClassReg.TrClass.TrCourse.ID))
        critCourseEval2.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrCourseEvaluation), "Type", MatchType.Exact, CType(EnumTrEvaluationType.TrEvaluationType.Angka, String)))

        Dim TrCourseEvalColl2 As ArrayList = New TrCourseEvaluationFacade(User).Retrieve(critCourseEval2)

        For Each objTrCourseEval As TrCourseEvaluation In TrCourseEvalColl2
            'Check If Class Id Mapping on TrClassNumEvaluation
            Dim _trClassNumEvaluationFacade As New TrClassNumEvaluationFacade(User)
            Dim critParam As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.TrClassNumEvaluation), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            critParam.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassNumEvaluation), "TrClass.ID", MatchType.Exact, objTrClassReg.TrClass.ID))
            critParam.opAnd(New Criteria(GetType(KTB.DNet.Domain.TrClassNumEvaluation), "TrCourseEvaluation.ID", MatchType.Exact, objTrCourseEval.ID))

            Dim _lstClassNumEval As New ArrayList
            _lstClassNumEval = _trClassNumEvaluationFacade.Retrieve(critParam)

            If (_lstClassNumEval.Count > 0) Then
                Dim _strCourseEvalName As String = KTB.DNet.Utility.CommonFunction.GetNamaKhususEvalTraining(objTrClassReg.TrClass.ID.ToString(), objTrCourseEval.ID)
                If (_strCourseEvalName = String.Empty) Then
                    _strCourseEvalName = objTrCourseEval.Name
                End If
                If Not objTrCourseEval.EvaluationCode.EndsWith("00") And Not objTrCourseEval.EvaluationCode.EndsWith("99") Then

                    Dim item As ListItem = New ListItem(_strCourseEvalName, GetCharTestResult(objTrClassReg.TrCertificateLines, objTrCourseEval.ID, CType(EnumTrEvaluationType.TrEvaluationType.Angka, String)))
                    'Dim item As ListItem = New ListItem(objTrCourseEval.Name, GetCharTestResult(objTrClassReg.TrCertificateLines, objTrCourseEval.ID, CType(EnumTrEvaluationType.TrEvaluationType.Angka, String)))
                    data.Add(item)
                End If
            End If

           
        Next

        rptTest.DataSource = data
        rptTest.DataBind()
    End Sub

    Private Sub rptTest_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptTest.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim lblKeteranganTest As Label = e.Item.FindControl("lblKeteranganTest")
            Dim lblNilaiTest As Label = e.Item.FindControl("lblNilaiTest")
            Dim item As ListItem = CType(e.Item.DataItem, ListItem)
            lblKeteranganTest.Text = item.Text
            lblNilaiTest.Text = item.Value
        End If
    End Sub

    Private Function TotalStudent(ByVal objTrClassReg As TrClassRegistration) As Integer
        Return objTrClassReg.TrClass.TrClassRegistrations.Count
    End Function

End Class
