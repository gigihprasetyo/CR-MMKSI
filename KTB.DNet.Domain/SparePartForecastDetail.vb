#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("SparePartForecastDetail")> _
    Public Class SparePartForecastDetail
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
        Private _requestQty As Integer
        Private _requestDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _approvedQty As Integer
        Private _sendDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _noAwb As String = String.Empty
        Private _soNo As String = String.Empty
        Private _doNo As String = String.Empty
        Private _billingNo As String = String.Empty
        Private _status As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdatedBy As String = String.Empty
        Private _lastUpdatedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _sparepartForecastMaster As SparePartForecastMaster
        Private _sparepartForecastHeader As SparePartForecastHeader

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

        <ColumnInfo("RequestQty", "{0}")> _
        Public Property RequestQty() As Integer
            Get
                Return _requestQty
            End Get
            Set(ByVal value As Integer)
                _requestQty = value
            End Set
        End Property

        <ColumnInfo("RequestDate", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property RequestDate As DateTime
            Get
                Return _requestDate
            End Get
            Set(ByVal value As DateTime)
                _requestDate = value
            End Set
        End Property

        <ColumnInfo("ApprovedQty", "{0}")> _
        Public Property ApprovedQty() As Integer
            Get
                Return _approvedQty
            End Get
            Set(ByVal value As Integer)
                _approvedQty = value
            End Set
        End Property

        <ColumnInfo("SendDate", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property SendDate As DateTime
            Get
                Return _sendDate
            End Get
            Set(ByVal value As DateTime)
                _sendDate = value
            End Set
        End Property

        <ColumnInfo("NoAWB", "'{0}'")> _
        Public Property NoAWB As String
            Get
                Return _noAwb
            End Get
            Set(ByVal value As String)
                _noAwb = value
            End Set
        End Property

        <ColumnInfo("SoNumber", "'{0}'")> _
        Public Property SoNumber As String
            Get
                Return _soNo
            End Get
            Set(ByVal value As String)
                _soNo = value
            End Set
        End Property

        <ColumnInfo("DoNumber", "'{0}'")> _
        Public Property DoNumber As String
            Get
                Return _doNo
            End Get
            Set(ByVal value As String)
                _doNo = value
            End Set
        End Property

        <ColumnInfo("BillingNumber", "'{0}'")> _
        Public Property BillingNumber As String
            Get
                Return _billingNo
            End Get
            Set(ByVal value As String)
                _billingNo = value
            End Set
        End Property

        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
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

        <ColumnInfo("LastUpdatedBy", "'{0}'")> _
        Public Property LastUpdatedBy As String
            Get
                Return _lastUpdatedBy
            End Get
            Set(ByVal value As String)
                _lastUpdatedBy = value
            End Set
        End Property

        <ColumnInfo("LastUpdatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdatedTime As DateTime
            Get
                Return _lastUpdatedTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdatedTime = value
            End Set
        End Property

        <ColumnInfo("SparePartForecastHeaderID", "{0}"), _
        RelationInfo("SparePartForecastHeader", "ID", "SparePartForecastDetail", "SparePartForecastHeaderID")> _
        Public Property SparePartForecastHeader() As SparePartForecastHeader
            Get
                Try
                    If Not IsNothing(Me._sparepartForecastHeader) AndAlso (Not Me._sparepartForecastHeader.IsLoaded) Then
                        Me._sparepartForecastHeader = CType(DoLoad(GetType(SparePartForecastHeader).ToString(), _sparepartForecastHeader.ID), SparePartForecastHeader)
                        Me._sparepartForecastHeader.MarkLoaded()
                    End If
                    Return Me._sparepartForecastHeader
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get

            Set(ByVal value As SparePartForecastHeader)
                Me._sparepartForecastHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sparepartForecastHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SparePartForecastMasterID", "{0}"), _
        RelationInfo("SparePartForecastMaster", "ID", "SparePartForecastDetail", "SparePartForecastMasterID")> _
        Public Property SparePartForecastMaster() As SparePartForecastMaster
            Get
                Try
                    If Not IsNothing(Me._sparepartForecastMaster) AndAlso (Not Me._sparepartForecastMaster.IsLoaded) Then
                        Me._sparepartForecastMaster = CType(DoLoad(GetType(SparePartForecastMaster).ToString(), _sparepartForecastMaster.ID), SparePartForecastMaster)
                        Me._sparepartForecastMaster.MarkLoaded()
                    End If
                    Return Me._sparepartForecastMaster
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get

            Set(ByVal value As SparePartForecastMaster)
                Me._sparepartForecastMaster = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sparepartForecastMaster.MarkLoaded()
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


