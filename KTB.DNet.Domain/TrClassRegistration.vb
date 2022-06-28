#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrClassRegistration Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 2/1/2006 - 8:50:08 AM
'//
'// ===========================================================================	
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("TrClassRegistration")> _
    Public Class TrClassRegistration
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Integer)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"

        Private _iD As Integer
        Private _registrationCode As String = String.Empty
        Private _registrationDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _certificateNo As String = String.Empty
        Private _status As String = String.Empty
        Private _initialTest As Decimal
        Private _finalTest As Decimal
        Private _avarage As Decimal
        Private _rank As Integer
        Private _notes As String = String.Empty
        Private _isManualCheck As Boolean = False
        Private _isManualBy As String
        Private _entryType As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _trTrainee As TrTrainee
        Private _trClass As TrClass

        Private _trCertificateLines As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _dealer As Dealer

#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID() As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property


        <ColumnInfo("RegistrationCode", "'{0}'")> _
        Public Property RegistrationCode() As String
            Get
                Return _registrationCode
            End Get
            Set(ByVal value As String)
                _registrationCode = value
            End Set
        End Property


        <ColumnInfo("RegistrationDate", "'{0:yyyy/MM/dd}'")> _
        Public Property RegistrationDate() As DateTime
            Get
                Return _registrationDate
            End Get
            Set(ByVal value As DateTime)
                _registrationDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("CertificateNo", "'{0}'")> _
        Public Property CertificateNo() As String
            Get
                Return _certificateNo
            End Get
            Set(ByVal value As String)
                _certificateNo = value
            End Set
        End Property


        <ColumnInfo("Status", "'{0}'")> _
        Public Property Status() As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
            End Set
        End Property


        <ColumnInfo("InitialTest", "#,##0")> _
        Public Property InitialTest() As Decimal
            Get
                Return _initialTest
            End Get
            Set(ByVal value As Decimal)
                _initialTest = value
            End Set
        End Property


        <ColumnInfo("FinalTest", "#,##0")> _
        Public Property FinalTest() As Decimal
            Get
                Return _finalTest
            End Get
            Set(ByVal value As Decimal)
                _finalTest = value
            End Set
        End Property


        <ColumnInfo("Avarage", "#,##0")> _
        Public Property Avarage() As Decimal
            Get
                Return _avarage
            End Get
            Set(ByVal value As Decimal)
                _avarage = value
            End Set
        End Property


        <ColumnInfo("Rank", "{0}")> _
        Public Property Rank() As Integer
            Get
                Return _rank
            End Get
            Set(ByVal value As Integer)
                _rank = value
            End Set
        End Property


        <ColumnInfo("Notes", "'{0}'")> _
        Public Property Notes() As String
            Get
                Return _notes
            End Get
            Set(ByVal value As String)
                _notes = value
            End Set
        End Property


        <ColumnInfo("EntryType", "{0}")> _
        Public Property EntryType() As Short
            Get
                Return _entryType
            End Get
            Set(ByVal value As Short)
                _entryType = value
            End Set
        End Property

        <ColumnInfo("IsManualCheck", "{0}")> _
        Public Property IsManualCheck() As Boolean
            Get
                Return _isManualCheck
            End Get
            Set(ByVal value As Boolean)
                _isManualCheck = value
            End Set
        End Property

        <ColumnInfo("IsManualBy", "{0}")> _
        Public Property IsManualBy() As String
            Get
                Return _isManualBy
            End Get
            Set(ByVal value As String)
                _isManualBy = value
            End Set
        End Property


        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus() As Short
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Short)
                _rowStatus = value
            End Set
        End Property


        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy() As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
            End Set
        End Property


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime() As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
            End Set
        End Property


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy() As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime() As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
            End Set
        End Property


        <ColumnInfo("TraineeID", "{0}"), _
        RelationInfo("TrTrainee", "ID", "TrClassRegistration", "TraineeID")> _
        Public Property TrTrainee() As TrTrainee
            Get
                Try
                    If Not IsNothing(Me._trTrainee) AndAlso (Not Me._trTrainee.IsLoaded) Then

                        Me._trTrainee = CType(DoLoad(GetType(TrTrainee).ToString(), _trTrainee.ID), TrTrainee)
                        Me._trTrainee.MarkLoaded()

                    End If

                    Return Me._trTrainee

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TrTrainee)

                Me._trTrainee = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._trTrainee.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ClassID", "{0}"), _
        RelationInfo("TrClass", "ID", "TrClassRegistration", "ClassID")> _
        Public Property TrClass() As TrClass
            Get
                Try
                    If Not IsNothing(Me._trClass) AndAlso (Not Me._trClass.IsLoaded) Then

                        Me._trClass = CType(DoLoad(GetType(TrClass).ToString(), _trClass.ID), TrClass)
                        Me._trClass.MarkLoaded()

                    End If

                    Return Me._trClass

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TrClass)

                Me._trClass = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._trClass.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("TrClassRegistration", "ID", "TrCertificateLine", "RegistrationID")> _
        Public ReadOnly Property TrCertificateLines() As System.Collections.ArrayList
            Get
                Try
                    If (Me._trCertificateLines.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(TrCertificateLine), "TrClassRegistration", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(TrCertificateLine), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._trCertificateLines = DoLoadArray(GetType(TrCertificateLine).ToString, criterias)
                    End If

                    Return Me._trCertificateLines

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "TrClassRegistration", "DealerID")> _
        Public Property Dealer() As Dealer
            Get
                Try
                    If Not IsNothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

                        Me._dealer = CType(DoLoad(GetType(Dealer).ToString(), _dealer.ID), Dealer)
                        Me._dealer.MarkLoaded()

                    End If

                    Return Me._dealer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Dealer)

                Me._dealer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property

#End Region

#Region "Generated Method"
        Public Function GetStrDate(ByVal dateInput As DateTime, ByVal dateFormat As String) As String
            If dateInput = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                Return ""
            Else
                Return Format(dateInput, dateFormat)
            End If
        End Function
#End Region

#Region "Custom Method"
        Public ReadOnly Property IsBilliing As Boolean
            Get
                Try
                    If TrClass.TrCourse.PaymentType = 2 And TrClass.TrCourse.JobPositionCategory.AreaID = 2 And Dealer.Title = CType(EnumDealerTittle.DealerTittle.DEALER, String) Then
                        Dim _criteria As Criteria = New Criteria(GetType(TrBillingDetail), "TrBookingCourse.TrClassRegistration.ID", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(TrBillingDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Dim arrBilling As ArrayList = DoLoadArray(GetType(TrBillingDetail).ToString, criterias)
                        If arrBilling.Count = 0 Then
                            Return False
                        Else
                            Dim iBilling As TrBillingHeader = CType(arrBilling(0), TrBillingDetail).TrBillingHeader
                            Dim crits As New CriteriaComposite(New Criteria(GetType(Reference), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            crits.opAnd(New Criteria(GetType(Reference), "Type", MatchType.Exact, "TRASS"))
                            crits.opAnd(New Criteria(GetType(Reference), "Code", MatchType.Exact, "DUEDATE"))

                            Dim spaceDueDate As Integer = CType(CType(DoLoadArray(GetType(Reference).ToString, crits)(0), Reference).Description.Trim, Integer)

                            If iBilling.Status <> 7 Then
                                Dim dueDate As DateTime = iBilling.PostedDate.AddDays(spaceDueDate).Date
                                If Date.Today > dueDate Then
                                    Return False
                                End If
                            End If
                        End If

                    End If
                Catch
                End Try

                Return True
            End Get
        End Property

#End Region

#Region "Non_generated Properties"
        Private _Test1 As Decimal = 0
        Private _Test2 As Decimal = 0
        Private _Test3 As Decimal = 0
        Private _Test4 As Decimal = 0
        Private _Test5 As Decimal = 0
        Private _Test6 As Decimal = 0
        Private _Test7 As Decimal = 0
        Private _Test8 As Decimal = 0
        Private _Test9 As Decimal = 0
        Private _Test10 As Decimal = 0
        Private _TempRank As Decimal = 0

        Public Property Test1() As Decimal
            Get
                Return _Test1
            End Get
            Set(ByVal value As Decimal)
                _Test1 = value
            End Set
        End Property
        Public Property Test2() As Decimal
            Get
                Return _Test2
            End Get
            Set(ByVal value As Decimal)
                _Test2 = value
            End Set
        End Property

        Public Property Test3() As Decimal
            Get
                Return _Test3
            End Get
            Set(ByVal value As Decimal)
                _Test3 = value
            End Set
        End Property
        Public Property Test4() As Decimal
            Get
                Return _Test4
            End Get
            Set(ByVal value As Decimal)
                _Test4 = value
            End Set
        End Property
        Public Property Test5() As Decimal
            Get
                Return _Test5
            End Get
            Set(ByVal value As Decimal)
                _Test5 = value
            End Set
        End Property
        Public Property Test6() As Decimal
            Get
                Return _Test6
            End Get
            Set(ByVal value As Decimal)
                _Test6 = value
            End Set
        End Property
        Public Property Test7() As Decimal
            Get
                Return _Test7
            End Get
            Set(ByVal value As Decimal)
                _Test7 = value
            End Set
        End Property

        Public Property Test8() As Decimal
            Get
                Return _Test8
            End Get
            Set(ByVal value As Decimal)
                _Test8 = value
            End Set
        End Property
        Public Property Test9() As Decimal
            Get
                Return _Test9
            End Get
            Set(ByVal value As Decimal)
                _Test9 = value
            End Set
        End Property
        Public Property Test10() As Decimal
            Get
                Return _Test10
            End Get
            Set(ByVal value As Decimal)
                _Test10 = value
            End Set
        End Property
        Public Property TempRank() As Decimal
            Get
                Return _TempRank
            End Get
            Set(ByVal value As Decimal)
                _TempRank = value
            End Set
        End Property



#End Region

    End Class

#Region "Non_generated Class"
Public Class QueryStringCollection
    Private _ParamName As String = ""
    Private _ParamValue As String = ""

    Public Property ParamName() As String
        Get
            Return _ParamName
        End Get
        Set(ByVal value As String)
            _ParamName = value
        End Set
    End Property
    Public Property ParamValue() As String
        Get
            Return _ParamValue
        End Get
        Set(ByVal value As String)
            _ParamValue = value
        End Set
    End Property
End Class
#End Region

End Namespace
















