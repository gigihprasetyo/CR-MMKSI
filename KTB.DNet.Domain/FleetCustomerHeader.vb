#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : FleetCustomerHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 7/30/2020 - 12:33:26 PM
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
    <Serializable(), TableInfo("FleetCustomerHeader")> _
    Public Class FleetCustomerHeader
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
        Private _fleetCode As String = String.Empty
        Private _fleetCustomerType As Short
        Private _fleetCompanyCategory As Short
        Private _fleetCustomerName As String = String.Empty
        Private _fleetCustomerGroupCompany As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _businessSectorDetail As BusinessSectorDetail

        Private _fleetCustomerDetails As System.Collections.ArrayList = New System.Collections.ArrayList()
        Private _fleetCustomerDetailMappings As System.Collections.ArrayList = New System.Collections.ArrayList()


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


        <ColumnInfo("FleetCode", "'{0}'")> _
        Public Property FleetCode As String
            Get
                Return _fleetCode
            End Get
            Set(ByVal value As String)
                _fleetCode = value
            End Set
        End Property


        <ColumnInfo("FleetCustomerType", "{0}")> _
        Public Property FleetCustomerType As Short
            Get
                Return _fleetCustomerType
            End Get
            Set(ByVal value As Short)
                _fleetCustomerType = value
            End Set
        End Property


        <ColumnInfo("FleetCompanyCategory", "{0}")> _
        Public Property FleetCompanyCategory As Short
            Get
                Return _fleetCompanyCategory
            End Get
            Set(ByVal value As Short)
                _fleetCompanyCategory = value
            End Set
        End Property


        <ColumnInfo("FleetCustomerName", "'{0}'")> _
        Public Property FleetCustomerName As String
            Get
                Return _fleetCustomerName
            End Get
            Set(ByVal value As String)
                _fleetCustomerName = value
            End Set
        End Property


        <ColumnInfo("FleetCustomerGroupCompany", "'{0}'")> _
        Public Property FleetCustomerGroupCompany As String
            Get
                Return _fleetCustomerGroupCompany
            End Get
            Set(ByVal value As String)
                _fleetCustomerGroupCompany = value
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


        <ColumnInfo("BusinessSectorDetailID", "{0}"), _
        RelationInfo("BusinessSectorDetail", "ID", "FleetCustomerHeader", "BusinessSectorDetailID")> _
        Public Property BusinessSectorDetail As BusinessSectorDetail
            Get
                Try
                    If Not isnothing(Me._businessSectorDetail) AndAlso (Not Me._businessSectorDetail.IsLoaded) Then

                        Me._businessSectorDetail = CType(DoLoad(GetType(BusinessSectorDetail).ToString(), _businessSectorDetail.ID), BusinessSectorDetail)
                        Me._businessSectorDetail.MarkLoaded()

                    End If

                    Return Me._businessSectorDetail

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As BusinessSectorDetail)

                Me._businessSectorDetail = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._businessSectorDetail.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("FleetCustomerHeader", "ID", "FleetCustomerDetail", "FleetCustomerHeaderID")> _
        Public ReadOnly Property FleetCustomerDetails As System.Collections.ArrayList
            Get
                Try
                    If (Me._fleetCustomerDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(FleetCustomerDetail), "FleetCustomerHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(FleetCustomerDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._fleetCustomerDetails = DoLoadArray(GetType(FleetCustomerDetail).ToString, criterias)
                    End If

                    Return Me._fleetCustomerDetails

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("FleetCustomerHeader", "ID", "FleetCustomerDetailMapping", "FleetCustomerHeaderID")> _
        Public ReadOnly Property FleetCustomerDetailMappings As System.Collections.ArrayList
            Get
                Try
                    If (Me._fleetCustomerDetailMappings.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(FleetCustomerDetailMapping), "FleetCustomerHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(FleetCustomerDetailMapping), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._fleetCustomerDetailMappings = DoLoadArray(GetType(FleetCustomerDetailMapping).ToString, criterias)
                    End If

                    Return Me._fleetCustomerDetailMappings

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
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
