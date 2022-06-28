
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PODestination Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 12/5/2016 - 4:03:04 PM
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
    <Serializable(), TableInfo("PODestination")> _
    Public Class PODestination
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
        Private _code As String = String.Empty
        Private _nama As String = String.Empty
        Private _alamat As String = String.Empty
        Private _regionCode = String.Empty
        Private _regionDesc = String.Empty
        Private _leadTime As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _city As City
        'Private _pODestinationRegion As PODestinationRegion
        Private _dealer As Dealer
        Private _dealerDestination As VWI_Dealer

        Private _pOHeaders As System.Collections.ArrayList = New System.Collections.ArrayList()


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


        <ColumnInfo("Code", "'{0}'")> _
        Public Property Code As String
            Get
                Return _code
            End Get
            Set(ByVal value As String)
                _code = value
            End Set
        End Property


        <ColumnInfo("Nama", "'{0}'")> _
        Public Property Nama As String
            Get
                Return _nama
            End Get
            Set(ByVal value As String)
                _nama = value
            End Set
        End Property


        <ColumnInfo("Alamat", "'{0}'")> _
        Public Property Alamat As String
            Get
                Return _alamat
            End Get
            Set(ByVal value As String)
                _alamat = value
            End Set
        End Property

        <ColumnInfo("RegionCode", "'{0}'")> _
        Public Property RegionCode As String
            Get
                Return _regionCode
            End Get
            Set(ByVal value As String)
                _regionCode = value
            End Set
        End Property


        <ColumnInfo("RegionDesc", "'{0}'")> _
        Public Property RegionDesc As String
            Get
                Return _regionDesc
            End Get
            Set(ByVal value As String)
                _regionDesc = value
            End Set
        End Property

        <ColumnInfo("LeadTime", "{0}")> _
        Public Property LeadTime As Short
            Get
                Return _leadTime
            End Get
            Set(ByVal value As Short)
                _leadTime = value
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


        <ColumnInfo("CityID", "{0}"), _
        RelationInfo("City", "ID", "PODestination", "CityID")> _
        Public Property City As City
            Get
                Try
                    If Not IsNothing(Me._city) AndAlso (Not Me._city.IsLoaded) Then

                        Me._city = CType(DoLoad(GetType(City).ToString(), _city.ID), City)
                        Me._city.MarkLoaded()

                    End If

                    Return Me._city

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As City)

                Me._city = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._city.MarkLoaded()
                End If
            End Set
        End Property

        '<ColumnInfo("PODestinationRegionID", "{0}"), _
        'RelationInfo("PODestinationRegion", "ID", "PODestination", "PODestinationRegionID")> _
        'Public Property PODestinationRegion As PODestinationRegion
        '    Get
        '        Try
        '            If Not isnothing(Me._pODestinationRegion) AndAlso (Not Me._pODestinationRegion.IsLoaded) Then

        '                Me._pODestinationRegion = CType(DoLoad(GetType(PODestinationRegion).ToString(), _pODestinationRegion.ID), PODestinationRegion)
        '                Me._pODestinationRegion.MarkLoaded()

        '            End If

        '            Return Me._pODestinationRegion

        '        Catch ex As Exception

        '            Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

        '            If rethrow Then
        '                Throw
        '            End If

        '        End Try

        '        Return Nothing
        '    End Get

        '    Set(ByVal value As PODestinationRegion)

        '        Me._pODestinationRegion = value
        '        If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
        '            Me._pODestinationRegion.MarkLoaded()
        '        End If
        '    End Set
        'End Property


        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "PODestination", "DealerID")> _
        Public Property Dealer As Dealer
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

        <ColumnInfo("DealerDestinationCode", "{0}"), _
        RelationInfo("VWI_Dealer", "ID", "PODestination", "DealerDestinationCode")> _
        Public Property DealerDestinationCode As VWI_Dealer
            Get
                Try
                    If Not IsNothing(Me._dealerDestination) AndAlso (Not Me._dealerDestination.IsLoaded) Then

                        Me._dealerDestination = CType(DoLoad(GetType(VWI_Dealer).ToString(), _dealerDestination.id), VWI_Dealer)
                        Me._dealer.MarkLoaded()

                    End If

                    Return Me._dealerDestination

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As VWI_Dealer)

                Me._dealerDestination = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealerDestination.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("PODestination", "ID", "POHeader", "PODestinationID")> _
        Public ReadOnly Property POHeaders As System.Collections.ArrayList
            Get
                Try
                    If (Me._pOHeaders.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(POHeader), "PODestination", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._pOHeaders = DoLoadArray(GetType(POHeader).ToString, criterias)
                    End If

                    Return Me._pOHeaders

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

