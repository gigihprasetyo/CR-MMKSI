
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : RevisionFaktur Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 09/08/2018 - 15:02:05
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
Imports System.Linq
#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("RevisionFaktur")> _
    Public Class RevisionFaktur
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
        Private _regNumber As String = String.Empty
        Private _revisionStatus As Short
        Private _revisionTypeID As Short
        Private _isPay As Short
        Private _newValidationDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _newValidationBy As String = String.Empty
        Private _newConfirmationDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _newConfirmationBy As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _remark As String = String.Empty

        Private _chassisMaster As ChassisMaster
        Private _endCustomer As EndCustomer
        Private _oldEndCustomer As EndCustomer
        Private _revisionSAPDoc As RevisionSAPDoc
        Private _revisionPaymentDetails As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _revisionPrices As System.Collections.ArrayList = New System.Collections.ArrayList

        Private _revisionType As RevisionType
        Private _revisionPrice As RevisionPrice
        Private _revisionPaymentDetail As RevisionPaymentDetail

#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property


        <ColumnInfo("RegNumber", "'{0}'")> _
        Public Property RegNumber As String
            Get
                Return _regNumber
            End Get
            Set(ByVal value As String)
                _regNumber = value
            End Set
        End Property


        <ColumnInfo("RevisionStatus", "{0}")> _
        Public Property RevisionStatus As Short
            Get
                Return _revisionStatus
            End Get
            Set(ByVal value As Short)
                _revisionStatus = value
            End Set
        End Property


        <ColumnInfo("RevisionTypeID", "{0}")> _
        Public Property RevisionTypeID As Short
            Get
                Return _revisionTypeID
            End Get
            Set(ByVal value As Short)
                _revisionTypeID = value
            End Set
        End Property


        <ColumnInfo("IsPay", "{0}")> _
        Public Property IsPay As Short
            Get
                Return _isPay
            End Get
            Set(ByVal value As Short)
                _isPay = value
            End Set
        End Property


        <ColumnInfo("NewValidationDate", "'{0:yyyy/MM/dd}'")> _
        Public Property NewValidationDate As DateTime
            Get
                Return _newValidationDate
            End Get
            Set(ByVal value As DateTime)
                _newValidationDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("NewValidationBy", "'{0}'")> _
        Public Property NewValidationBy As String
            Get
                Return _newValidationBy
            End Get
            Set(ByVal value As String)
                _newValidationBy = value
            End Set
        End Property


        <ColumnInfo("NewConfirmationDate", "'{0:yyyy/MM/dd}'")> _
        Public Property NewConfirmationDate As DateTime
            Get
                Return _newConfirmationDate
            End Get
            Set(ByVal value As DateTime)
                _newConfirmationDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("NewConfirmationBy", "'{0}'")> _
        Public Property NewConfirmationBy As String
            Get
                Return _newConfirmationBy
            End Get
            Set(ByVal value As String)
                _newConfirmationBy = value
            End Set
        End Property

        <ColumnInfo("Remark", "'{0}'")> _
        Public Property Remark() As String
            Get
                Return _remark
            End Get
            Set(ByVal value As String)
                _remark = value
            End Set
        End Property

        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus As Short
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Short)
                _rowStatus = value
            End Set
        End Property


        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
            End Set
        End Property


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
            End Set
        End Property


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
            End Set
        End Property


        <ColumnInfo("RevisionTypeID", "{0}"), _
    RelationInfo("RevisionType", "ID", "RevisionFaktur", "RevisionTypeID")> _
        Public Property RevisionType As RevisionType
            Get
                Try
                    If Not IsNothing(Me._revisionType) AndAlso (Not Me._revisionType.IsLoaded) Then

                        Me._revisionType = CType(DoLoad(GetType(RevisionType).ToString(), _revisionType.ID), RevisionType)
                        Me._revisionType.MarkLoaded()

                    End If

                    Return Me._revisionType

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As RevisionType)

                Me._revisionType = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._revisionType.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ChassisMasterID", "{0}"), _
        RelationInfo("ChassisMaster", "ID", "RevisionFaktur", "ChassisMasterID")> _
        Public Property ChassisMaster As ChassisMaster
            Get
                Try
                    If Not IsNothing(Me._chassisMaster) AndAlso (Not Me._chassisMaster.IsLoaded) Then

                        Me._chassisMaster = CType(DoLoad(GetType(ChassisMaster).ToString(), _chassisMaster.ID), ChassisMaster)
                        Me._chassisMaster.MarkLoaded()

                    End If

                    Return Me._chassisMaster

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ChassisMaster)

                Me._chassisMaster = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._chassisMaster.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("EndCustomerID", "{0}"), _
        RelationInfo("EndCustomer", "ID", "RevisionFaktur", "EndCustomerID")> _
        Public Property EndCustomer As EndCustomer
            Get
                Try
                    If Not IsNothing(Me._endCustomer) AndAlso (Not Me._endCustomer.IsLoaded) Then

                        Me._endCustomer = CType(DoLoad(GetType(EndCustomer).ToString(), _endCustomer.ID), EndCustomer)
                        Me._endCustomer.MarkLoaded()

                    End If

                    Return Me._endCustomer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As EndCustomer)

                Me._endCustomer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._endCustomer.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("OldEndCustomerID", "{0}"), _
        RelationInfo("EndCustomer", "ID", "RevisionFaktur", "OldEndCustomerID")> _
        Public Property OldEndCustomer As EndCustomer
            Get
                Try
                    If Not IsNothing(Me._oldEndCustomer) AndAlso (Not Me._oldEndCustomer.IsLoaded) Then

                        Me._oldEndCustomer = CType(DoLoad(GetType(EndCustomer).ToString(), _oldEndCustomer.ID), EndCustomer)
                        Me._oldEndCustomer.MarkLoaded()

                    End If

                    Return Me._oldEndCustomer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As EndCustomer)

                Me._oldEndCustomer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._oldEndCustomer.MarkLoaded()
                End If
            End Set
        End Property

        <RelationInfo("RevisionFaktur", "ID", "RevisionSAPDoc", "RevisionFakturID")> _
        Public ReadOnly Property RevisionSAPDoc As RevisionSAPDoc
            Get
                Try
                    If IsNothing(_revisionSAPDoc) Then
                        Dim _criteria As Criteria = New Criteria(GetType(RevisionSAPDoc), "RevisionFaktur", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(RevisionSAPDoc), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Dim arryList As ArrayList = DoLoadArray(GetType(RevisionSAPDoc).ToString, criterias)

                        If arryList.Count > 0 Then
                            Me._revisionSAPDoc = arryList(0)
                        End If
                    End If

                    Return Me._revisionSAPDoc

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("RevisionFaktur", "ID", "RevisionPaymentDetail", "RevisionFakturID")> _
        Public ReadOnly Property RevisionPaymentDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._revisionPaymentDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(RevisionPaymentDetail), "RevisionFaktur", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(RevisionPaymentDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._revisionPaymentDetails = DoLoadArray(GetType(RevisionPaymentDetail).ToString, criterias)
                    End If

                    Return Me._revisionPaymentDetails

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("RevisionFaktur", "ID", "RevisionPaymentDetail", "RevisionFakturID")>
        Public ReadOnly Property RevisionPaymentDetail As RevisionPaymentDetail
            Get
                Try
                    If (Me._revisionPaymentDetails.Count < 1) Then
                        Me._revisionPaymentDetail = CType(_revisionPaymentDetails(0), RevisionPaymentDetail)
                    End If

                    Return Me._revisionPaymentDetail
                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        Public Property RevisionPrice As RevisionPrice
            Get
                Try
                    If IsNothing(Me._revisionPrice) Then
                        Dim criterias As New CriteriaComposite(New Criteria(GetType(RevisionPrice), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        criterias.opAnd(New Criteria(GetType(RevisionPrice), "Category.ID", MatchType.Exact, Me.ChassisMaster.Category.ID))
                        criterias.opAnd(New Criteria(GetType(RevisionPrice), "RevisionType.ID", MatchType.Exact, Me.RevisionType.ID))
                        Me._revisionPrices = DoLoadArray(GetType(RevisionPrice).ToString, criterias)
                        If _revisionPrices.Count > 0 Then
                            Me._revisionPrice = CType(_revisionPrices(0), RevisionPrice)
                        Else
                            Me._revisionPrice = New RevisionPrice
                        End If
                    End If

                    Return Me._revisionPrice

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As RevisionPrice)

                Me._revisionPrice = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._revisionPrice.MarkLoaded()
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

#End Region

    End Class
End Namespace

