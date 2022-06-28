#Region "Default Region"
Imports System.IO
Imports System.Text
Imports OfficeOpenXml
Imports System.Collections.Generic
Imports System.Linq
Imports KTB.DNet.Security
Imports System.Security.Principal
Imports System.Web.UI.Page
Imports KTB.DNet.Utility
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.Domain
Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.BusinessFacade.Training
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.Helper
Imports System.Runtime.CompilerServices
Imports GlobalExtensions
#End Region


Public Class TrainingHelpers
    Class privilageTraining
        Public Sub New(ByVal mKey As String, ByVal mMenuDesc As String)
            Me.nKey = mKey
            Me.nMenuDesc = mMenuDesc
        End Sub

        Private nKey As String
        Public Property Key() As String
            Get
                Return nKey
            End Get
            Set(ByVal value As String)
                nKey = value
            End Set
        End Property

        Private nMenuDesc As String
        Public Property MenuDesc() As String
            Get
                Return nMenuDesc
            End Get
            Set(ByVal value As String)
                nMenuDesc = value
            End Set
        End Property

        Private nDictionaryPriv As New Dictionary(Of PrivillageType, String)
        Public ReadOnly Property DictionaryPrivillage() As Dictionary(Of PrivillageType, String)
            Get
                Return nDictionaryPriv
            End Get
        End Property

        Public Sub AddPrivillage(ByVal privType As PrivillageType, ByVal privName As String)
            nDictionaryPriv.Add(privType, privName)
        End Sub

        Enum PrivillageType As Short
            view = 0
            fullAccess = 1
        End Enum
    End Class

    Private pView As Boolean
    Private pEdit As Boolean
    Private _Crits As Hashtable
    Private _SessionHelper As New SessionHelper
    Private _Page As System.Web.UI.Page
    Private _NameCriteria As String
    Private _ListValidation As List(Of Validataion)
    Private listMessage As List(Of String) = New List(Of String)

    Public ReadOnly Property GetCriterias() As Boolean
        Get
            Try
                If _Crits IsNot Nothing Then
                    Return True
                End If
            Catch
            End Try
            Return False
        End Get
    End Property

    Public ReadOnly Property NotSelected() As String
        Get
            Return "-1"
        End Get
    End Property

    Public Sub New(ByVal page As System.Web.UI.Page, ByVal titlePage As String)
        Me.New(page)
        Me.PrivilegePage = New privilageTraining(page.ClientID, titlePage)
    End Sub

    Public Sub New(ByVal page As System.Web.UI.Page)
        MyBase.New()
        Me._Page = page
        Me._Crits = New Hashtable()
        Me._ListValidation = New List(Of Validataion)
        _NameCriteria = _Page.ToString() + "_Crits"
        pView = True
        pEdit = True
    End Sub

    Public Sub CheckPrivilege(ByVal menuKey As String)
        If Not menuKey.IsNullorEmpty Then
            Dim datas As List(Of privilageTraining) = DataPrivillage()
            If datas.Where(Function(x) x.Key.ToLower() = menuKey.ToLower).IsData Then
                Me.pView = False
                Me.pEdit = False
                Dim priv As privilageTraining = datas.FirstOrDefault(Function(x) x.Key.ToLower = menuKey.ToLower)
                Dim privView As KeyValuePair(Of privilageTraining.PrivillageType, String) = _
                    priv.DictionaryPrivillage.FirstOrDefault(Function(x) x.Key = privilageTraining.PrivillageType.view)
                If Not IsNothing(privView) Then
                    If SecurityProvider.Authorize(Me._Page.User, privView.Value) Then
                        pView = True
                    End If
                End If

                Dim privAdmin As KeyValuePair(Of privilageTraining.PrivillageType, String) = _
                    priv.DictionaryPrivillage.FirstOrDefault(Function(x) x.Key = privilageTraining.PrivillageType.fullAccess)
                If Not IsNothing(privAdmin) Then
                    If SecurityProvider.Authorize(Me._Page.User, privAdmin.Value) Then
                        pEdit = True
                    End If
                End If

                If pEdit = False And pView = False Then
                    Me._Page.Server.Transfer("../FrmAccessDenied.aspx?modulName=" + priv.MenuDesc)
                End If
            End If
        End If
    End Sub


    Private PrivilegePage As privilageTraining

    Public Sub AddPriv(ByVal privType As privilageTraining.PrivillageType, ByVal privName As String)
        Me.PrivilegePage.AddPrivillage(privType, privName)
    End Sub

    Public Sub Privilage()
        Me.pView = False
        Me.pEdit = False
        Dim privView As KeyValuePair(Of privilageTraining.PrivillageType, String) = _
            PrivilegePage.DictionaryPrivillage.FirstOrDefault(Function(x) x.Key = privilageTraining.PrivillageType.view)
        If Not IsNothing(privView) Then
            If SecurityProvider.Authorize(Me._Page.User, privView.Value) Then
                pView = True
            End If
        End If

        Dim privAdmin As KeyValuePair(Of privilageTraining.PrivillageType, String) = _
            PrivilegePage.DictionaryPrivillage.FirstOrDefault(Function(x) x.Key = privilageTraining.PrivillageType.fullAccess)
        If Not IsNothing(privAdmin) Then
            If SecurityProvider.Authorize(Me._Page.User, privAdmin.Value) Then
                pEdit = True
            End If
        End If

        If pEdit = False And pView = False Then
            Me._Page.Server.Transfer("../FrmAccessDenied.aspx?modulName=" + PrivilegePage.MenuDesc)
        End If
    End Sub

    Public Sub CheckPrivilegeTransaction(ByVal menuKey As String)
        If Not menuKey.IsNullorEmpty Then
            Dim datas As List(Of privilageTraining) = DataPrivillageTransaction()
            If datas.Where(Function(x) x.Key.ToLower() = menuKey.ToLower).IsData Then
                Me.pView = False
                Me.pEdit = False
                Dim priv As privilageTraining = datas.FirstOrDefault(Function(x) x.Key.ToLower = menuKey.ToLower)
                Dim privView As KeyValuePair(Of privilageTraining.PrivillageType, String) = _
                    priv.DictionaryPrivillage.FirstOrDefault(Function(x) x.Key = privilageTraining.PrivillageType.view)
                If Not IsNothing(privView) Then
                    If SecurityProvider.Authorize(Me._Page.User, privView.Value) Then
                        pView = True
                    End If
                End If

                Dim privAdmin As KeyValuePair(Of privilageTraining.PrivillageType, String) = _
                    priv.DictionaryPrivillage.FirstOrDefault(Function(x) x.Key = privilageTraining.PrivillageType.fullAccess)
                If Not IsNothing(privAdmin) Then
                    If SecurityProvider.Authorize(Me._Page.User, privAdmin.Value) Then
                        pEdit = True
                    End If
                End If

                If pEdit = False And pView = False Then
                    Me._Page.Server.Transfer("../FrmAccessDenied.aspx?modulName=" + priv.MenuDesc)
                End If

            End If
        End If
    End Sub

    Public Sub SetEdit(Optional ByVal isSet As Boolean = True)
        pEdit = isSet
    End Sub

    Public Sub CheckDueDateTagihan(ByVal area As String)
        If IsDueDateTagihan(area) Then
            Me._Page.Server.Transfer("../FrmAccessDenied.aspx?modulName= Training (Harap selesaikan tagihan)")
        End If
    End Sub

    Public Function isDueDateClass(ByVal registerID As Integer) As Boolean
        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(TrBillingDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(TrBillingDetail), "TrBookingCourse.TrClassRegistration.ID", MatchType.Exact, registerID))

        Dim arrDataTagihan As ArrayList = New TrBillingDetailFacade(Me._Page.User).Retrieve(criterias)
        If arrDataTagihan.IsItems Then
            Dim billHeader As TrBillingHeader = CType(arrDataTagihan(0), TrBillingDetail).TrBillingHeader
            If billHeader.Status = 2 Or billHeader.Status = 3 Or billHeader.Status = 4 Then
                Dim crits As New CriteriaComposite(New Criteria(GetType(Reference), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crits.opAnd(New Criteria(GetType(Reference), "Type", MatchType.Exact, "TRASS"))
                crits.opAnd(New Criteria(GetType(Reference), "Code", MatchType.Exact, "DUEDATE"))

                Dim spaceDueDate As Integer = CType(CType(New ReferenceFacade(Me._Page.User).Retrieve(crits)(0), Reference).Description.Trim, Integer)
                Dim dueDate As DateTime = billHeader.PostedDate.AddDays(spaceDueDate).DateDay
                If Me._Page.DateNow > dueDate Then
                    Return True
                End If
            End If
        Else
            Return True
        End If


        Return False
    End Function

    Public Function IsDueDateTagihan(ByVal area As String, Optional ByVal dealerCode As String = "") As Boolean
        Dim nDealer As Dealer
        If dealerCode.IsNullorEmpty Then
            nDealer = Me._Page.GetDealer()
        Else
            nDealer = New DealerFacade(Me._Page.User).Retrieve(dealerCode)
        End If
        If nDealer.IsDealer And area.Equals("2") Then
            Dim criteria As New CriteriaComposite(New Criteria(GetType(TrBillingHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criteria.opAnd(New Criteria(GetType(TrBillingHeader), "Dealer.ID", MatchType.Exact, nDealer.ID))
            criteria.opAnd(New Criteria(GetType(TrBillingHeader), "Status", MatchType.No, CType(EnumTagihanTraining.TagihanStatus.Selesai, Short)))

            Dim arrTagihan As ArrayList = New TrBillingHeaderFacade(Me._Page.User).Retrieve(criteria)
            If arrTagihan.IsItems Then
                Dim crits As New CriteriaComposite(New Criteria(GetType(Reference), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                crits.opAnd(New Criteria(GetType(Reference), "Type", MatchType.Exact, "TRASS"))
                crits.opAnd(New Criteria(GetType(Reference), "Code", MatchType.Exact, "DUEDATE"))

                Dim spaceDueDate As Integer = CType(CType(New ReferenceFacade(Me._Page.User).Retrieve(crits)(0), Reference).Description.Trim, Integer)
                For Each iBill As TrBillingHeader In arrTagihan
                    If iBill.Status = 2 Or iBill.Status = 3 Or iBill.Status = 4 Then
                        Dim dueDate As DateTime = iBill.PostedDate.AddDays(spaceDueDate).DateDay
                        If Me._Page.DateNow > dueDate Then
                            Return True
                        End If
                    End If
                Next
            End If

            'Dim crit As New CriteriaComposite(New Criteria(GetType(TrBookingCourse), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'crit.opAnd(New Criteria(GetType(TrBookingCourse), "Dealer.ID", MatchType.Exact, nDealer.ID))
            'crit.opAnd(New Criteria(GetType(TrBookingCourse), "TrClassRegistration.TrClass.TrCourse.PaymentType", MatchType.Exact, _
            '                        CType(EnumTrCourse.PaymentType.CHARGE, Short)))
            'crit.opAnd(New Criteria(GetType(TrBookingCourse), "TrClassRegistration.TrClass.TrCourse.JobPositionCategory.AreaID", MatchType.Exact, 2))
            'crit.opAnd(New Criteria(GetType(TrBookingCourse), "TrClassRegistration.TrClass.StartDate", MatchType.LesserOrEqual, Me._Page.DateNow))
            'crit.opAnd(New Criteria(GetType(TrBookingCourse), "TrClassRegistration.Status", MatchType.Exact, 0))

            'Dim aggregates As New Aggregate(GetType(TrBookingCourse), "ID", AggregateType.Count)
            'Dim countDaftar As Integer = New TrBookingCourseFacade(Me._Page.User).RetrieveScalar(crit, aggregates)

            'Dim dueDateReference As Integer = GetDueDateFromReference()

            'Dim crit2 As New CriteriaComposite(New Criteria(GetType(TrBillingDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            'crit2.opAnd(New Criteria(GetType(TrBillingDetail), "TrBookingCourse.Dealer.ID", MatchType.Exact, nDealer.ID))
            'crit2.opAnd(New Criteria(GetType(TrBillingDetail), "TrBookingCourse.TrClassRegistration.Status", MatchType.Exact, CType(EnumTrClassRegistration.DataStatusType.Register, String)))
            'crit2.opAnd(New Criteria(GetType(TrBillingDetail), "TrBookingCourse.TrClassRegistration.TrClass.TrCourse.JobPositionCategory.AreaID", MatchType.Exact, 2))
            'crit2.opAnd(New Criteria(GetType(TrBillingDetail), "TrBookingCourse.TrClassRegistration.TrClass.StartDate", MatchType.LesserOrEqual, Me._Page.DateNow.AddDays((dueDateReference * -1))))
            'crit2.opAnd(New Criteria(GetType(TrBillingDetail), "TrBookingCourse.TrClassRegistration.TrClass.TrCourse.PaymentType", MatchType.Exact, _
            '                        CType(EnumTrCourse.PaymentType.CHARGE, Short)))


            'Dim aggregates2 As New Aggregate(GetType(TrBillingDetail), "ID", AggregateType.Count)
            'Dim countTagihan As Integer = .RetrieveScalar(crit2, aggregates)

            If Not (New TrBillingDetailFacade(Me._Page.User).IsValidTagihan(nDealer.ID)) Then
                Return True
            End If
        End If
        Return False
    End Function

    Private Function GetDueDateFromReference() As Integer
        Dim crits As New CriteriaComposite(New Criteria(GetType(Reference), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        crits.opAnd(New Criteria(GetType(Reference), "Type", MatchType.Exact, "TRASS"))
        crits.opAnd(New Criteria(GetType(Reference), "Code", MatchType.Exact, "DUEDATECFR"))

        Return CType(CType(New ReferenceFacade(Me._Page.User).Retrieve(crits)(0), Reference).Description.Trim, Integer)
    End Function

    Private Function DataPrivillage() As List(Of privilageTraining)
        Dim listData As New List(Of privilageTraining)

        '*** Kategori kursus ***
        Dim priv1A As New privilageTraining("priv1A", "Training Sales - Kategori Kursus")
        priv1A.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingSalesViewKategoriKursus_Privilege)
        priv1A.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingSalesEditKategoriKursus_Privilege)

        Dim priv1B As New privilageTraining("priv1B", "Training After Sales - Kategori Kursus")
        priv1B.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingAssViewKategoriKursus_Privilege)
        priv1B.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingAssEditKategoriKursus_Privilege)

        Dim priv1C As New privilageTraining("priv1C", "Training Customer Satisfaction - Kategori Kursus")
        priv1C.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingCsViewKategoriKursus_Privilege)
        priv1C.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingCsEditKategoriKursus_Privilege)

        '*** Kategori Training ***
        Dim priv2A As New privilageTraining("priv2A", "Training Sales - Kategori Training")
        priv2A.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingSalesViewKategoriTraining_Privilege)
        priv2A.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingSalesEditKategoriTraining_Privilege)

        Dim priv2B As New privilageTraining("priv2B", "Training After Sales - Kategori Training")
        priv2B.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingAssViewKategoriTraining_Privilege)
        priv2B.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingAssEditKategoriTraining_Privilege)

        Dim priv2C As New privilageTraining("priv2C", "Training Customer Satisfaction - Kategori Training")
        priv2C.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingCsViewKategoriTraining_Privilege)
        priv2C.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingCsEditKategoriTraining_Privilege)


        '*** Jenis Evaluasi ***
        Dim priv3A As New privilageTraining("priv3A", "Training Sales - Jenis Evaluasi")
        priv3A.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingSalesViewJenisEvaluasi_Privilege)
        priv3A.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingSalesEditJenisEvaluasi_Privilege)

        Dim priv3B As New privilageTraining("priv3B", "Training After Sales - Jenis Evaluasi")
        priv3B.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingAssViewJenisEvaluasi_Privilege)
        priv3B.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingAssEditJenisEvaluasi_Privilege)

        Dim priv3C As New privilageTraining("priv3C", "Training Customer Satisfaction - Jenis Evaluasi")
        priv3C.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingCsViewJenisEvaluasi_Privilege)
        priv3C.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingCsEditJenisEvaluasi_Privilege)

        '*** Prasyarat ***
        Dim priv4A As New privilageTraining("priv4A", "Training Sales - Prasyarat")
        priv4A.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingSalesViewPrasyarat_Privilege)
        priv4A.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingSalesEditPrasyarat_Privilege)

        Dim priv4B As New privilageTraining("priv4B", "Training After Sales - Prasyarat")
        priv4B.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingAssViewPrasyarat_Privilege)
        priv4B.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingAssEditPrasyarat_Privilege)

        Dim priv4C As New privilageTraining("priv4C", "Training Customer Satisfaction - Prasyarat")
        priv4C.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingCsViewPrasyarat_Privilege)
        priv4C.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingCsEditPrasyarat_Privilege)

        '*** Kelas ***
        Dim priv5A As New privilageTraining("priv5A", "Training Sales - Kelas")
        priv5A.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingSalesViewKelas_Privilege)
        priv5A.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingSalesEditKelas_Privilege)

        Dim priv5B As New privilageTraining("priv5B", "Training After Sales - Kelas")
        priv5B.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingAssViewKelas_Privilege)
        priv5B.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingAssEditKelas_Privilege)

        Dim priv5C As New privilageTraining("priv5C", "Training Customer Satisfaction - Kelas")
        priv5C.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingCsViewKelas_Privilege)
        priv5C.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingCsEditKelas_Privilege)

        '*** Training Pengganti ***
        Dim priv6A As New privilageTraining("priv6A", "Training Sales - Training Pengganti")
        priv6A.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingSalesViewTrPengganti_Privilege)
        priv6A.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingSalesEditTrPengganti_Privilege)

        Dim priv6B As New privilageTraining("priv6B", "Training After Sales - Training Pengganti")
        priv6B.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingAssViewTrPengganti_Privilege)
        priv6B.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingAssEditTrPengganti_Privilege)

        Dim priv6C As New privilageTraining("priv6C", "Training Customer Satisfaction - Training Pengganti")
        priv6C.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingCsViewTrPengganti_Privilege)
        priv6C.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingCsEditTrPengganti_Privilege)

        '*** Alokasi ***
        Dim priv7A As New privilageTraining("priv7A", "Training Sales - Alokasi")
        priv7A.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingSalesViewAlokasi_Privilege)
        priv7A.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingSalesEditAlokasi_Privilege)

        Dim priv7B As New privilageTraining("priv7B", "Training After Sales - Alokasi")
        priv7B.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingAssViewAlokasi_Privilege)
        priv7B.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingAssEditAlokasi_Privilege)

        Dim priv7C As New privilageTraining("priv7C", "Training Customer Satisfaction - Alokasi")
        priv7C.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingCsViewAlokasi_Privilege)
        priv7C.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingCsEditAlokasi_Privilege)

        '*** MRTC ***
        Dim priv8B As New privilageTraining("priv8B", "Training After Sales - MRTC")
        priv8B.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingAssViewMRTC_Privilege)
        priv8B.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingAssEditMRTC_Privilege)

        '*** Free Pass Training ***
        Dim priv9B As New privilageTraining("priv9B", "Training After Sales - Free Pass Training")
        priv9B.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingAssViewFreePass_Privilege)
        priv9B.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingAssEditFreePass_Privilege)

        '*** Konfigusi Sertifikat ***
        Dim priv10B As New privilageTraining("priv10B", "Training After Sales - Konfigusi Sertifikat")
        priv10B.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingAssViewConfCertificate_Privilege)
        priv10B.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingAssEditConfCertificate_Privilege)

        '*** Training Pengganti ***
        Dim descUploadJadwal As String = "Upload"
        If Me._Page.IsDealer Then
            descUploadJadwal = "DownLoad"
        End If

        Dim priv11A As New privilageTraining("priv11A", String.Format("Training Sales - {0} Jadwal", descUploadJadwal))
        priv11A.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingSalesViewUploadJadwal_Privilege)
        priv11A.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingSalesEditUploadJadwal_Privilege)

        Dim priv11B As New privilageTraining("priv11B", String.Format("Training After Sales - {0} Jadwal", descUploadJadwal))
        priv11B.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingAssViewUploadJadwal_Privilege)
        priv11B.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingAssEditUploadJadwal_Privilege)

        Dim priv11C As New privilageTraining("priv11C", String.Format("Training Customer Satisfaction - {0} Jadwal", descUploadJadwal))
        priv11C.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingCsViewUploadJadwal_Privilege)
        priv11C.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingCsEditUploadJadwal_Privilege)

        listData.Add(priv1A)
        listData.Add(priv2A)
        listData.Add(priv3A)
        listData.Add(priv4A)
        listData.Add(priv5A)
        listData.Add(priv6A)
        listData.Add(priv7A)
        listData.Add(priv11A)

        listData.Add(priv1B)
        listData.Add(priv2B)
        listData.Add(priv3B)
        listData.Add(priv4B)
        listData.Add(priv5B)
        listData.Add(priv6B)
        listData.Add(priv7B)
        listData.Add(priv8B)
        listData.Add(priv9B)
        listData.Add(priv10B)
        listData.Add(priv11B)

        listData.Add(priv1C)
        listData.Add(priv2C)
        listData.Add(priv3C)
        listData.Add(priv4C)
        listData.Add(priv5C)
        listData.Add(priv6C)
        listData.Add(priv7C)
        listData.Add(priv11C)

        Return listData
    End Function

    Private Function DataPrivillageTransaction() As List(Of privilageTraining)
        Dim listData As New List(Of privilageTraining)

        '*** Reminder ***
        Dim tr1A As New privilageTraining("tr1A", "Training Sales - Reminder")
        tr1A.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingSalesViewReminder_Privilege)
        listData.Add(tr1A)

        Dim tr1B As New privilageTraining("tr1B", "Training After Sales - Reminder")
        tr1B.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingAssViewReminder_Privilege)
        listData.Add(tr1B)

        Dim tr1C As New privilageTraining("tr1C", "Training Customer Satisfaction - Reminder")
        tr1C.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingCsViewReminder_Privilege)
        listData.Add(tr1C)

        '*** Referensi ***
        Dim tr2A As New privilageTraining("tr2A", "Training Sales - Referensi")
        tr2A.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingSalesViewReferensi_Privilege)
        tr2A.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingSalesEditReferensi_Privilege)
        listData.Add(tr2A)

        Dim tr2B As New privilageTraining("tr2B", "Training After Sales - Referensi")
        tr2B.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingAssViewReferensi_Privilege)
        tr2B.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingAssEditReferensi_Privilege)
        listData.Add(tr2B)

        Dim tr2C As New privilageTraining("tr2C", "Training Customer Satisfaction - Referensi")
        tr2C.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingCsViewReferensi_Privilege)
        tr2C.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingCsEditReferensi_Privilege)
        listData.Add(tr2C)

        '*** Siswa ***
        Dim tr3A As New privilageTraining("tr3A", "Training Sales - Siswa")
        tr3A.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingSalesViewSiswa_Privilege)
        listData.Add(tr3A)

        Dim tr3B As New privilageTraining("tr3B", "Training After Sales - Siswa")
        tr3B.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingAssViewSiswa_Privilege)
        tr3B.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingAssEditSiswa_Privilege)
        listData.Add(tr3B)

        Dim tr3C As New privilageTraining("tr3C", "Training Customer Satisfaction - Siswa")
        tr3C.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingCsViewSiswa_Privilege)
        listData.Add(tr3C)

        '*** Data Status Siswa ***
        Dim tr4A As New privilageTraining("tr4A", "Training Sales - Data Status Siswa")
        tr4A.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingSalesViewDataStatusSiswa_Privilege)
        listData.Add(tr4A)

        Dim tr4B As New privilageTraining("tr4B", "Training After Sales - Data Status Siswa")
        tr4B.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingAssViewDataStatusSiswa_Privilege)
        tr4B.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingAssEditDataStatusSiswa_Privilege)
        listData.Add(tr4B)

        Dim tr4C As New privilageTraining("tr4C", "Training Customer Satisfaction - Data Status Siswa")
        tr4C.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingCsViewDataStatusSiswa_Privilege)
        listData.Add(tr4C)

        '*** Input Data Nilai ***
        Dim tr5A As New privilageTraining("tr5A", "Training Sales - Input Data Nilai")
        tr5A.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingSalesViewInputNilai_Privilege)
        tr5A.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingSalesEditInputNilai_Privilege)
        listData.Add(tr5A)

        Dim tr5B As New privilageTraining("tr5B", "Training After Sales - Input Data Nilai")
        tr5B.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingAssViewInputNilai_Privilege)
        tr5B.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingAssEditInputNilai_Privilege)
        listData.Add(tr5B)

        Dim tr5C As New privilageTraining("tr5C", "Training Customer Satisfaction - Input Data Nilai")
        tr5C.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingCsViewInputNilai_Privilege)
        tr5C.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingCsEditInputNilai_Privilege)
        listData.Add(tr5C)

        '*** Evaluasi Hasil Training ***
        Dim tr6A As New privilageTraining("tr6A", "Training Sales - Evaluasi Hasil Training")
        tr6A.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingSalesViewEvaluasiHasil_Privilege)
        tr6A.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingSalesEditEvaluasiHasil_Privilege)
        listData.Add(tr6A)

        Dim tr6B As New privilageTraining("tr6B", "Training After Sales - Evaluasi Hasil Training")
        tr6B.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingAssViewEvaluasiHasil_Privilege)
        tr6B.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingAssEditEvaluasiHasil_Privilege)
        listData.Add(tr6B)

        Dim tr6C As New privilageTraining("tr6C", "Training Customer Satisfaction - Evaluasi Hasil Training")
        tr6C.AddPrivillage(privilageTraining.PrivillageType.view, SR.TrainingCsViewEvaluasiHasil_Privilege)
        tr6C.AddPrivillage(privilageTraining.PrivillageType.fullAccess, SR.TrainingCsEditEvaluasiHasil_Privilege)
        listData.Add(tr6C)

        Return listData
    End Function

    Public ReadOnly Property IsView() As Boolean
        Get
            Return pView
        End Get
    End Property

    Public ReadOnly Property IsEdit() As Boolean
        Get
            Return pEdit
        End Get
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="keyName"></param>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    Public Sub SetSession(ByVal keyName As String, ByVal value As Object)
        _SessionHelper.SetSession(keyName, value)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="keyName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSession(ByVal keyName As String) As Object
        Return _SessionHelper.GetSession(keyName)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="keyName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Sub RemoveSession(ByVal keyName As String)
        _SessionHelper.RemoveSession(keyName)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="key"></param>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    Public Sub AddCriteria(ByVal key As Object, ByVal value As Object)
        _Crits.Add(key, value)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SaveCriteria()
        _SessionHelper.SetSession(_NameCriteria, _Crits)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ClearCriteria()
        _SessionHelper.RemoveSession(_NameCriteria)
        _Crits.Clear()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIntCriteria(ByVal obj As Object) As Integer
        Try
            Return Integer.Parse(GetValueCriteria(obj).ToString)
        Catch ex As Exception
            Return -1
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetStringCriteria(ByVal obj As Object) As String
        Try
            Return GetValueCriteria(obj).ToString
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDateCriteria(ByVal obj As Object) As Date
        Try
            Return CDate(GetValueCriteria(obj))
        Catch ex As Exception
            Return New Date()
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBoolCriteria(ByVal obj As Object) As Boolean
        Try
            Return CBool(GetValueCriteria(obj))
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function IsNullCriteria() As Boolean
        If IsNothing(CType(_SessionHelper.GetSession(_NameCriteria), Hashtable)) Then
            Return True
        End If

        Return False
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetValueCriteria(ByVal obj As Object) As Object
        If Me._Crits.Count.Equals(0) Then
            Me._Crits = CType(_SessionHelper.GetSession(_NameCriteria), Hashtable)
        End If
        Return Me._Crits.Item(obj)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ctrl"></param>
    ''' <remarks></remarks>
    Public Sub ClearData(ByVal ctrl As Control, Optional ByVal batal As Boolean = False)
        If ctrl.HasControls Then
            For Each childCtrl As Control In ctrl.Controls
                ClearData(childCtrl, batal)
            Next
        Else
            If TypeOf ctrl Is TextBox Then
                Dim txt As TextBox = DirectCast(ctrl, TextBox)
                If (txt.ReadOnly = False And txt.Enabled = True) Or batal Then
                    txt.Text = String.Empty
                    txt.BackColor = Color.Empty
                End If
                If batal Then
                    txt.ReadOnly = False
                    txt.Enabled = True
                End If

            ElseIf TypeOf ctrl Is DropDownList Then
                Dim ddl As DropDownList = CType(ctrl, DropDownList)
                If ddl.Enabled = True Or batal Then
                    ddl.ClearSelection()
                    If Not ddl.Items.Count.Equals(0) Then
                        ddl.Items.FindByValue("-1").Selected = True
                    End If
                    If batal Then
                        ddl.Enabled = True
                    End If
                End If
            ElseIf TypeOf ctrl Is CheckBox Then
                Dim cbx As CheckBox = CType(ctrl, CheckBox)
                If cbx.Enabled = True Or batal Then
                    cbx.Checked = False
                    If batal Then
                        cbx.Checked = False
                        cbx.Enabled = True
                    End If
                End If
            ElseIf TypeOf ctrl Is CheckBoxList Then
                Dim cbl As CheckBoxList = CType(ctrl, CheckBoxList)
                If cbl.Enabled = True Or batal Then
                    cbl.ClearSelection()
                    If batal Then
                        cbl.Enabled = True
                    End If
                End If
            ElseIf TypeOf ctrl Is System.Web.UI.HtmlControls.HtmlInputFile Then
                DirectCast(ctrl, System.Web.UI.HtmlControls.HtmlInputFile).Value = String.Empty
            ElseIf TypeOf ctrl Is KTB.DNet.WebCC.IntiCalendar Then
                DirectCast(ctrl, KTB.DNet.WebCC.IntiCalendar).Value = Date.MinValue
            End If
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ClearData()
        MyClass.ClearData(_Page)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ModeReadOnly()
        MyClass.ModeReadOnly(Me._Page)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="listControl"></param>
    ''' <param name="modeExclude"></param>
    ''' <remarks></remarks>
    Public Sub ModeReadOnly(ByVal listControl As List(Of String), ByVal modeExclude As Boolean)
        MyClass.ModeReadOnly(Me._Page, listControl, modeExclude)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ctrl"></param>
    ''' <remarks></remarks>
    Public Sub ModeReadOnly(ctrl As Control, Optional ByRef coll As ControlCollection = Nothing)
        If ctrl.HasControls Then
            Dim listCtr As List(Of Control) = New List(Of Control) '= ctrl.Controls
            listCtr.AddRange(ctrl.Controls.Cast(Of Control).ToList())
            For Each childCtrl As Control In listCtr
                ModeReadOnly(childCtrl, ctrl.Controls)
            Next
        Else
            If TypeOf ctrl Is TextBox Then
                DirectCast(ctrl, TextBox).Disabled()
            ElseIf TypeOf ctrl Is DropDownList Then
                Dim ddl As DropDownList = DirectCast(ctrl, DropDownList)
                If ddl.Visible Then
                    If coll IsNot Nothing Then
                        If TypeOf (ddl.Parent) Is System.Web.UI.HtmlControls.HtmlForm Then
                            ddl.Enabled = False
                        Else
                            Dim lbl As Label = New Label()
                            lbl.Text = ddl.SelectedItem.Text
                            coll.Add(CType(lbl, Control))
                            ddl.NonVisible()
                        End If
                    Else
                        ddl.Enabled = False
                    End If
                End If
            ElseIf TypeOf ctrl Is CheckBox Then
                DirectCast(ctrl, CheckBox).Enabled = False
            ElseIf TypeOf ctrl Is Label Then
                Dim lblform As Label = DirectCast(ctrl, Label)
                If lblform.HasAttributes Then
                    lblform.Visible = False
                End If
            ElseIf TypeOf ctrl Is System.Web.UI.HtmlControls.HtmlImage Then
                Dim imgform As System.Web.UI.HtmlControls.HtmlImage = DirectCast(ctrl, System.Web.UI.HtmlControls.HtmlImage)
                If imgform.Attributes.Count > 0 Then
                    imgform.Visible = False
                End If
            ElseIf TypeOf ctrl Is System.Web.UI.WebControls.FileUpload Then
                DirectCast(ctrl, FileUpload).Visible = False
            ElseIf TypeOf ctrl Is System.Web.UI.HtmlControls.HtmlInputFile Then
                DirectCast(ctrl, System.Web.UI.HtmlControls.HtmlInputFile).Disabled = True
            ElseIf TypeOf ctrl Is KTB.DNet.WebCC.IntiCalendar Then
                Dim ccKal As KTB.DNet.WebCC.IntiCalendar = DirectCast(ctrl, KTB.DNet.WebCC.IntiCalendar)
                If ccKal.Visible Then
                    If coll IsNot Nothing Then
                        If TypeOf (ccKal.Parent) Is System.Web.UI.HtmlControls.HtmlForm Then
                            ccKal.Enabled = False
                        Else
                            Dim lbl As Label = New Label()
                            lbl.Text = ccKal.Value.DateToString
                            coll.Add(CType(lbl, Control))
                            ccKal.NonVisible()
                        End If
                    Else
                        ccKal.Enabled = False
                    End If
                End If

            ElseIf TypeOf ctrl Is System.Web.UI.WebControls.LinkButton Then
                Dim lbtn As LinkButton = DirectCast(ctrl, LinkButton)
                If lbtn.Parent IsNot Nothing Then
                    If Not (TypeOf (lbtn.Parent) Is DataGridItem Or TypeOf (lbtn.Parent) Is GridViewRow Or TypeOf (lbtn.Parent) Is TableCell) Then
                        DirectCast(ctrl, LinkButton).Visible = False
                    End If
                Else
                    DirectCast(ctrl, LinkButton).Visible = False
                End If

            ElseIf TypeOf ctrl Is System.Web.UI.WebControls.ImageButton Then
                DirectCast(ctrl, System.Web.UI.WebControls.ImageButton).Visible = False
            ElseIf TypeOf ctrl Is System.Web.UI.WebControls.CustomValidator Then
                DirectCast(ctrl, System.Web.UI.WebControls.CustomValidator).Visible = False
            ElseIf TypeOf ctrl Is Label Then
                Dim lblform As Label = DirectCast(ctrl, Label)
                If lblform.HasAttributes Then
                    lblform.Visible = False
                End If
            ElseIf TypeOf ctrl Is Button Then
                Dim btn As Button = CType(ctrl, Button)
                If Not btn.Text.ToLower.Contains("kembali") Then
                    btn.Visible = False
                End If

            End If
        End If
    End Sub

    Public Function StringInFromListItem(ByVal listItemColl As ListItemCollection) As String
        Dim strIn As String = String.Empty
        For Each item As ListItem In listItemColl
            If item.Selected And Not item.Value.Equals("all") Then
                strIn = strIn + item.Value + ", "
            End If
        Next
        If Not String.IsNullorEmpty(strIn) Then
            strIn = strIn.Remove(strIn.Length - 2)
            Return String.Format("({0})", strIn)
        End If
        Return String.Empty
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ctrl"></param>
    ''' <param name="listControl"></param>
    ''' <param name="modeExclude"></param>
    ''' <remarks></remarks>
    Public Sub ModeReadOnly(ctrl As Control, ByVal listControl As List(Of String), ByVal modeExclude As Boolean)
        If modeExclude Then
            If ctrl.HasControls Then
                For Each childCtrl As Control In ctrl.Controls
                    ModeReadOnly(childCtrl, listControl, modeExclude)
                Next
            Else
                If Not listControl.Contains(ctrl.ID) Then
                    ModeReadOnly(ctrl)
                End If
            End If
        Else
            If ctrl.HasControls Then
                For Each childCtrl As Control In ctrl.Controls
                    ModeReadOnly(childCtrl, listControl, modeExclude)
                Next
            Else
                If listControl.Contains(ctrl.ID) Then
                    ModeReadOnly(ctrl)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Sub BindDDLCategory(ByRef ddl As DropDownList, Optional ByVal area As String = "")
        Dim arrCtgByArea As List(Of JobPositionCategoryToArea) = New List(Of JobPositionCategoryToArea)

        Dim dataCtg As List(Of JobPositionCategory) = _
                New JobPositionCategoryFacade(Me._Page.User).RetrieveActiveList().Cast(Of JobPositionCategory).ToList()
        ddl.ClearSelection()
        ddl.Items.Clear()
        ddl.Items.Add(New ListItem("Silahkan pilih", "-1"))

        Dim dataCtgArea As New List(Of JobPositionCategory)
        If Not String.IsNullorEmpty(area) Then
            dataCtgArea = dataCtg.Where(Function(x) x.AreaID = CInt(area)).ToList()
        End If

        For Each data As JobPositionCategory In dataCtg
            If dataCtgArea.Count.Equals(0) Then
                ddl.Items.Add(New ListItem(data.Description, data.ID))
            Else
                If dataCtgArea.Where(Function(x) x.ID.Equals(data.ID)).Count > 0 Then
                    ddl.Items.Add(New ListItem(data.Description, data.ID))
                End If
            End If

        Next
        ddl.Items.FindByValue("-1").Selected = True
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="catgory"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Sub BindJobPosition(ByRef cbl As CheckBoxList, ByVal catgory As String)
        cbl.ClearSelection()
        cbl.Items.Clear()

        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(JobPositionToCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.JobPositionToCategory), "CategoryID", MatchType.Exact, CType(catgory, Short)))

        Dim arlJobPositionToCategory As ArrayList = New JobPositionToCategoryFacade(Me._Page.User).Retrieve(criterias)
        For Each JobPosition As JobPositionToCategory In arlJobPositionToCategory
            cbl.Items.Add(New ListItem(JobPosition.JobPosition.Description, JobPosition.JobPositionID))
        Next
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="catgory"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Sub BindDDLStatus(ByRef ddl As DropDownList)
        ddl.ClearSelection()
        ddl.Items.Clear()
        ddl.Items.Add(New ListItem("Silahkan pilih", "-1"))
        ddl.Items.Add(New ListItem("Aktif", "1"))
        ddl.Items.Add(New ListItem("Tidak Aktif", "0"))

        ddl.Items.FindByValue("1").Selected = True
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="catgory"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetJobPosition(ByVal catgory As EnumCategoryArea) As ArrayList
        Dim listCategory As String = String.Empty
        Dim criteriaData As New CriteriaComposite(New Criteria(GetType(JobPositionCategoryToArea), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
        criteriaData.opAnd(New Criteria(GetType(JobPositionCategoryToArea), "JobPositionCategoryAreaID", MatchType.Exact, CType(catgory, Short)))
        Dim datasCategory As List(Of JobPositionCategoryToArea) = New JobPositionCategoryToAreaFacade(Me._Page.User).Retrieve(criteriaData).Cast(Of JobPositionCategoryToArea).ToList
        Dim dataCtg As List(Of JobPositionCategory) = New JobPositionCategoryFacade(Me._Page.User).RetrieveActiveList().Cast(Of JobPositionCategory).ToList()

        For Each data As JobPositionCategory In dataCtg
            If datasCategory.Where(Function(x) x.JobPositionCategoryID.Equals(data.ID)).Count > 0 Then
                If String.IsNullorEmpty(listCategory) Then
                    listCategory = data.ID.ToString()
                Else
                    listCategory = listCategory + ", " + data.ID.ToString()
                End If
            End If
        Next
        If Not String.IsNullorEmpty(listCategory) Then
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(JobPositionToCategory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(KTB.DNet.Domain.JobPositionToCategory), "CategoryID", MatchType.InSet, String.Format("({0})", listCategory)))

            Dim arlJobPositionToCategory As ArrayList = New JobPositionToCategoryFacade(Me._Page.User).Retrieve(criterias)
            Return arlJobPositionToCategory
        End If

        Return New ArrayList()
    End Function

    Public Sub AddValidatation(ByVal ctrl As Control, ByVal name As String, ByVal tipeValidation As String, Optional ByVal length As Integer = 0)
        Me._ListValidation.Add(New Validataion(ctrl, name, tipeValidation, length))
    End Sub

    Public Function CheckValidatiaon() As ActionResult
        Dim rest As ActionResult = New ActionResult()
        rest.Status = EnumStatusActive.Succes
        rest.Message = String.Empty
        listMessage.Clear()

        For Each dataVal As Validataion In _ListValidation
            Dim nValue As String = String.Empty
            If TypeOf dataVal.Controls Is TextBox Then
                Dim txt As TextBox = DirectCast(dataVal.Controls, TextBox)
                nValue = txt.Text
                If Not checkValueValidation(nValue, dataVal) Then
                    'txt.BorderColor = Color.OrangeRed
                Else
                    'txt.BorderColor = Color.Empty
                End If
            ElseIf TypeOf dataVal.Controls Is DropDownList Then
                Dim ddl As DropDownList = DirectCast(dataVal.Controls, DropDownList)
                If Not ddl.SelectedValue.Equals("-1") Then
                    nValue = ddl.SelectedValue
                End If
                If Not checkValueValidation(nValue, dataVal) Then
                    'ddl.BorderColor = Color.OrangeRed
                Else
                    'ddl.BorderColor = Color.Empty
                End If
            ElseIf TypeOf dataVal.Controls Is System.Web.UI.WebControls.FileUpload Then
                Dim fileUpload As FileUpload = DirectCast(dataVal.Controls, FileUpload)
                nValue = fileUpload.FileName
                If Not checkValueValidation(nValue, dataVal) Then
                    'fileUpload.BorderColor = Color.OrangeRed
                Else
                    'fileUpload.BorderColor = Color.Empty
                End If
            ElseIf TypeOf dataVal.Controls Is System.Web.UI.HtmlControls.HtmlInputFile Then
                Dim htmlInputFile As HtmlInputFile = DirectCast(dataVal.Controls, System.Web.UI.HtmlControls.HtmlInputFile)
                nValue = htmlInputFile.Value
                If Not checkValueValidation(nValue, dataVal) Then
                    'htmlInputFile.Style.Add("Border-Color", "OrangeRed")
                End If
            ElseIf TypeOf dataVal.Controls Is KTB.DNet.WebCC.IntiCalendar Then
                nValue = DirectCast(dataVal.Controls, KTB.DNet.WebCC.IntiCalendar).Value.ToString()
            End If

        Next

        If listMessage.Count > 0 Then
            rest.Status = EnumStatusActive.Fail
            rest.Message = String.Join(", ", listMessage.ToArray())
        End If

        Return rest
    End Function

    Private Function checkValueValidation(ByVal nvalue As String, ByVal dataVals As Validataion) As Boolean
        Dim arrTipeVal() As String = dataVals.TipeValidate.Split(New Char() {","}, System.StringSplitOptions.RemoveEmptyEntries)
        Dim countMessage As Integer = listMessage.Count
        For Each tipeVal As String In arrTipeVal
            Select Case tipeVal.ToLower()
                Case "required"
                    If String.IsNullorEmpty(nvalue) Then
                        listMessage.Add(String.Format("Field {0} tidak boleh kosong atau harus dipilih", dataVals.Name))
                    End If
                Case "numeric"
                    If Not String.IsNullorEmpty(nvalue) Then
                        Try
                            Dim xValue As Double = Double.Parse(nvalue)
                        Catch ex As Exception
                            listMessage.Add(String.Format("Field {0} harus berisi angka", dataVals.Name))
                        End Try
                    End If
                Case "email"
                    If Not String.IsNullorEmpty(nvalue) Then
                        Try
                            Dim mailAdd As System.Net.Mail.MailAddress = New Net.Mail.MailAddress(nvalue)
                        Catch
                            listMessage.Add(String.Format("Field {0} format email tidak benar", dataVals.Name))
                        End Try
                    End If
                Case "max"
                    If Not String.IsNullorEmpty(nvalue) Then
                        If nvalue.Trim.Length > dataVals.Length Then
                            listMessage.Add(String.Format("Field {0} maksimal {1} karakter", dataVals.Name, dataVals.Length))
                        End If

                    End If
            End Select
        Next
        If countMessage.Equals(listMessage.Count) Then
            Return True
        End If
        Return False
    End Function


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ctrInput"></param>
    ''' <param name="subPath"></param>
    ''' <param name="maxSize"></param>
    ''' <param name="extension"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UploadFile(ByVal ctrInput As HtmlInputFile, ByVal subPath As String, ByVal maxSize As Long, Optional ByVal extension() As String = Nothing, Optional ByVal errMessage As String = "") As String
        Dim strComplaintNumber As String
        Dim success As Boolean = False
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim ServerTarget As String = KTB.DNet.Lib.WebConfig.GetValue("SAN")
        Dim maxFinfo As Long = maxSize

        Dim SrcFile As String = Path.GetFileName(ctrInput.PostedFile.FileName)  '-- Source file name
        If String.IsNullOrEmpty(SrcFile) Then
            Return "Error|Silahkan Mengupload Terlebih Dahulu"
        End If

        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)
        Dim fileExt As String = System.IO.Path.GetExtension(ctrInput.PostedFile.FileName)
        Dim newNameFile As String = String.Format("\{0}\{1}\{2}{3}", _
                                                  CType(GetSession("DEALER"), Dealer).DealerCode, _
                                                 subPath, _
                                                 Guid.NewGuid().ToString().Substring(0, 5), _
                                                 fileExt)
        Try
            success = imp.Start()
            If extension IsNot Nothing Then
                If Not (extension.Contains(fileExt)) Then
                    Return "Error|Format File Hanya : " & String.Join(", ", extension)
                End If
            End If

            Dim warnSize As String = "0"
            If maxFinfo > 5120000 Then
                warnSize = "10"
            Else
                warnSize = "5"
            End If

            If ctrInput.PostedFile.ContentLength > maxFinfo Then
                If errMessage = "" Then
                    Return "Error|Ukuran File Maximal " & warnSize & " Mb"
                Else
                    Return errMessage
                End If

            End If

            If imp.Start() Then
                Dim NewFileLocation As String = ServerTarget & newNameFile
                Dim strFileName As String = Path.GetFileName(ctrInput.PostedFile.FileName)

                If Not IO.Directory.Exists(NewFileLocation) Then
                    IO.Directory.CreateDirectory(Path.GetDirectoryName(NewFileLocation))
                End If

                If IO.File.Exists(NewFileLocation) Then
                    IO.File.Delete(Path.GetDirectoryName(NewFileLocation))
                End If

                Dim objUpload As New UploadToWebServer
                objUpload.Upload(ctrInput.PostedFile.InputStream, NewFileLocation)

                imp.StopImpersonate()
                imp = Nothing
                Return "Success|" & newNameFile
            End If

        Catch ex As Exception
            Throw ex
        End Try

        Return ServerTarget
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pathFile"></param>
    ''' <remarks></remarks>
    Public Sub DownloadFile(ByVal pathFile As String, Optional ByVal fileName As String = "")
        Dim _user As String = KTB.DNet.Lib.WebConfig.GetValue("User")
        Dim _password As String = KTB.DNet.Lib.WebConfig.GetValue("Password")
        Dim _webServer As String = KTB.DNet.Lib.WebConfig.GetValue("WebServer") '172.17.104.204
        Dim imp As SAPImpersonate = New SAPImpersonate(_user, _password, _webServer)

        Dim filePath As String = KTB.DNet.Lib.WebConfig.GetValue("SAN") + pathFile

        Dim fileInfo As New FileInfo(filePath)
        Dim destFilePath As String = fileInfo.FullName
        Dim success As Boolean = False


        Try
            success = imp.Start()
            If success Then
                If (fileInfo.Exists) Then
                    Me._Page.Response.Redirect("../Download.aspx?file=" & destFilePath & "&name=" & fileName)
                Else
                    MessageBox.Show(SR.FileNotFound(fileInfo.Name))
                End If
                imp.StopImpersonate()
            End If

        Catch ex As Exception
            MessageBox.Show(SR.DownloadFail(fileInfo.Name))
            Me._Page.Response.End()
        End Try
        'Return finfo
    End Sub



    Class Validataion
        Private _Control As Control
        Private _TipeValidate As String
        Private _Name As String
        Private _length As Integer

        Public Sub New(ByVal ctrl As Control, ByVal name As String, ByVal tipeValidate As String, Optional ByVal length As Integer = 0)
            Me.Controls = ctrl
            Me.Name = name
            Me.TipeValidate = tipeValidate
            Me.Length = length
        End Sub

        Public Property Controls As Control
            Get
                Return _Control
            End Get
            Set(ByVal value As Control)
                _Control = value
            End Set
        End Property

        Public Property Name As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                _Name = value
            End Set
        End Property

        Public Property TipeValidate As String
            Get
                Return _TipeValidate
            End Get
            Set(ByVal value As String)
                _TipeValidate = value
            End Set
        End Property

        Public Property Length As Integer
            Get
                Return _length
            End Get
            Set(ByVal value As Integer)
                _length = value
            End Set
        End Property
    End Class

End Class

Public Class ActionResult
    Private _Status As EnumStatusActive
    Private _Message As String
    Private _Data As String

    Public Property Status As EnumStatusActive
        Get
            Return _Status
        End Get
        Set(ByVal value As EnumStatusActive)
            _Status = value
        End Set
    End Property

    Public Property Message As String
        Get
            Return _Message
        End Get
        Set(ByVal value As String)
            _Message = value
        End Set
    End Property

    Public Property Data As String
        Get
            Return _Data
        End Get
        Set(ByVal value As String)
            _Data = value
        End Set
    End Property
End Class

Public Enum EnumCategoryArea As Short
    Sales = 1
    ASS = 2
    CS = 3
End Enum

Public Enum EnumStatusActive As Short
    Succes = 1
    Fail = -1
End Enum

Public Class CriteriasComposite
    Implements ICriteria

    Private crits As CriteriaComposite
    Private nType As Type

    Public Sub New()
    End Sub

    Public Sub New(ByVal type As Type)
        Me.nType = type
        crits = New CriteriaComposite(New Criteria(Me.nType, "RowStatus", MatchType.Exact, CType(KTB.DNet.Domain.DBRowStatus.Active, Short)))
    End Sub

    Public Sub opAnd(ByVal Criteria As ICriteria)
        crits.opAnd(Criteria)
    End Sub

    Public Sub opAnd(ByVal PropertyName As String, ByVal Value As Object)
        opAnd(New Criteria(Me.nType, PropertyName, Value))
    End Sub

    Public Sub opAnd(ByVal PropertyName As String, ByVal Match As MatchType, ByVal Value As Object)
        opAnd(New Criteria(Me.nType, PropertyName, Match, Value))
    End Sub

    Public Sub opAnd(ByVal Criteria As ICriteria, ByVal parenthesis As String, ByVal isOpenParenthesis As Boolean)
        crits.opAnd(Criteria, parenthesis, isOpenParenthesis)
    End Sub

    Public Sub opOr(ByVal Criteria As ICriteria)
        crits.opOr(Criteria)
    End Sub

    Public Sub opOr(ByVal PropertyName As String, ByVal Value As Object)
        opOr(New Criteria(Me.nType, PropertyName, Value))
    End Sub

    Public Sub opOr(ByVal PropertyName As String, ByVal Match As MatchType, ByVal Value As Object)
        opOr(New Criteria(Me.nType, PropertyName, Match, Value))
    End Sub

    Public Sub opOr(ByVal Criteria As ICriteria, ByVal parenthesis As String, ByVal isOpenParenthesis As Boolean)
        crits.opOr(Criteria, parenthesis, isOpenParenthesis)
    End Sub

    Public Function ToString() As String Implements ICriteria.ToString
        crits.ToString()
    End Function
End Class
