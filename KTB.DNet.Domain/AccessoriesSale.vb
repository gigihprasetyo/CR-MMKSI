
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AccessoriesSale Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 7/16/2012 - 3:32:13 PM
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
    <Serializable(), TableInfo("AccessoriesSale")> _
    Public Class AccessoriesSale
        Inherits DomainObject

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Integer)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"

        Private _iD As Integer
        Private _reportNumber As String = String.Empty
        Private _refNumber As String = String.Empty
        Private _dealerID As Integer
        Private _accessoriesCategoryID As Integer
        Private _chassisMasterID As Integer
        Private _soldDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _customerName As String = String.Empty
        Private _customerPhone As String = String.Empty
        Private _comment As String = String.Empty
        Private _status As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)




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


        <ColumnInfo("ReportNumber", "'{0}'")> _
        Public Property ReportNumber() As String
            Get
                Return _reportNumber
            End Get
            Set(ByVal value As String)
                _reportNumber = value
            End Set
        End Property


        <ColumnInfo("RefNumber", "'{0}'")> _
        Public Property RefNumber() As String
            Get
                Return _refNumber
            End Get
            Set(ByVal value As String)
                _refNumber = value
            End Set
        End Property


        <ColumnInfo("DealerID", "{0}")> _
        Public Property DealerID() As Integer
            Get
                Return _dealerID
            End Get
            Set(ByVal value As Integer)
                _dealerID = value
            End Set
        End Property


        <ColumnInfo("AccessoriesCategoryID", "{0}")> _
        Public Property AccessoriesCategoryID() As Integer
            Get
                Return _accessoriesCategoryID
            End Get
            Set(ByVal value As Integer)
                _accessoriesCategoryID = value
            End Set
        End Property


        <ColumnInfo("ChassisMasterID", "{0}")> _
        Public Property ChassisMasterID() As Integer
            Get
                Return _chassisMasterID
            End Get
            Set(ByVal value As Integer)
                _chassisMasterID = value
            End Set
        End Property


        <ColumnInfo("SoldDate", "'{0:yyyy/MM/dd}'")> _
        Public Property SoldDate() As DateTime
            Get
                Return _soldDate
            End Get
            Set(ByVal value As DateTime)
                _soldDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("CustomerName", "'{0}'")> _
        Public Property CustomerName() As String
            Get
                Return _customerName
            End Get
            Set(ByVal value As String)
                _customerName = value
            End Set
        End Property


        <ColumnInfo("CustomerPhone", "'{0}'")> _
        Public Property CustomerPhone() As String
            Get
                Return _customerPhone
            End Get
            Set(ByVal value As String)
                _customerPhone = value
            End Set
        End Property


        <ColumnInfo("Comment", "'{0}'")> _
        Public Property Comment() As String
            Get
                Return _comment
            End Get
            Set(ByVal value As String)
                _comment = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
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



        Private _dealer As Dealer
        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "AccessoriesSale", "DealerID")> _
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

        Private _AccessoriesSaleDetails As New ArrayList

        <RelationInfo("AccessoriesSale", "ID", "AccessoriesSaleDetail", "AccessoriesSaleID")> _
        Public ReadOnly Property AccessoriesSaleDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._AccessoriesSaleDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(AccessoriesSaleDetail), "AccessoriesSale", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(AccessoriesSaleDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._AccessoriesSaleDetails = DoLoadArray(GetType(AccessoriesSaleDetail).ToString, criterias)
                    End If

                    Return Me._AccessoriesSaleDetails

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        Private _AccessoriesCategory As AccessoriesCategory
        <ColumnInfo("AccessoriesCategoryID", "{0}"), _
        RelationInfo("AccessoriesCategory", "ID", "AccessoriesSale", "AccessoriesCategoryID")> _
        Public Property AccessoriesCategory() As AccessoriesCategory
            Get
                Try
                    If Not IsNothing(Me._AccessoriesCategory) AndAlso (Not Me._AccessoriesCategory.IsLoaded) Then

                        Me._AccessoriesCategory = CType(DoLoad(GetType(AccessoriesCategory).ToString(), _AccessoriesCategory.ID), AccessoriesCategory)
                        Me._AccessoriesCategory.MarkLoaded()

                    End If

                    Return Me._AccessoriesCategory

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As AccessoriesCategory)

                Me._AccessoriesCategory = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._AccessoriesCategory.MarkLoaded()
                End If
            End Set
        End Property

        Private _ChassisMaster As ChassisMaster
        <ColumnInfo("ChassisMasterID", "{0}"), _
        RelationInfo("ChassisMaster", "ID", "AccessoriesSale", "ChassisMasterID")> _
        Public Property ChassisMaster() As ChassisMaster
            Get
                Try
                    If Not IsNothing(Me._ChassisMaster) AndAlso (Not Me._ChassisMaster.IsLoaded) Then

                        Me._ChassisMaster = CType(DoLoad(GetType(ChassisMaster).ToString(), _ChassisMaster.ID), ChassisMaster)
                        Me._ChassisMaster.MarkLoaded()

                    End If

                    Return Me._ChassisMaster

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ChassisMaster)

                Me._ChassisMaster = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._ChassisMaster.MarkLoaded()
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

