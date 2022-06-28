#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_EventLaporanPenjualanGroupDealer Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 11/20/2009 - 3:32:34 PM
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
    <Serializable(), TableInfo("V_EventLaporanPenjualanGroupDealer")> _
    Public Class V_EventLaporanPenjualanGroupDealer
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
        Private _eventParameterID As Integer
        Private _dealerID As Integer
        Private _vechileTypeID As Short
        Private _description As String = String.Empty
        Private _jumlah As Integer
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


        <ColumnInfo("EventParameterID", "{0}")> _
        Public Property EventParameterID() As Integer
            Get
                Return _eventParameterID
            End Get
            Set(ByVal value As Integer)
                _eventParameterID = value
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


        <ColumnInfo("VechileTypeID", "{0}")> _
        Public Property VechileTypeID() As Short
            Get
                Return _vechileTypeID
            End Get
            Set(ByVal value As Short)
                _vechileTypeID = value
            End Set
        End Property


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property


        <ColumnInfo("Jumlah", "{0}")> _
        Public Property Jumlah() As Integer
            Get
                Return _jumlah
            End Get
            Set(ByVal value As Integer)
                _jumlah = value
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
        Private _vechileType As VechileType
        Private _eventParameter As EventParameter
        Private _dealer As DealerGroup

        <ColumnInfo("VechileTypeID", "{0}"), _
        RelationInfo("VechileType", "ID", "V_EventLaporanPenjualanGroupDealer", "VechileTypeID")> _
        Public Property VechileType() As VechileType
            Get
                Try
                    If IsNothing(Me._vechileType) Then
                        Me._vechileType = New VechileType
                        Me._vechileType.ID = Me._vechileTypeID
                    End If
                    If Not IsNothing(Me._vechileType) AndAlso (Not Me._vechileType.IsLoaded) Then

                        Me._vechileType = CType(DoLoad(GetType(VechileType).ToString(), _vechileType.ID), VechileType)
                        Me._vechileType.MarkLoaded()

                    End If

                    Return Me._vechileType

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As VechileType)

                Me._vechileType = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._vechileType.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("EventParameterID", "{0}"), _
        RelationInfo("EventParameter", "ID", "V_EventLaporanPenjualanGroupDealer", "EventParameterID")> _
        Public Property EventParameter() As EventParameter
            Get
                Try
                    If IsNothing(Me._eventParameter) Then
                        Me._eventParameter = New EventParameter
                        Me._eventParameter.ID = Me._eventParameterID
                    End If
                    If Not IsNothing(Me._eventParameter) AndAlso (Not Me._eventParameter.IsLoaded) Then

                        Me._eventParameter = CType(DoLoad(GetType(EventParameter).ToString(), _eventParameter.ID), EventParameter)
                        Me._eventParameter.MarkLoaded()

                    End If

                    Return Me._eventParameter

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As EventParameter)

                Me._eventParameter = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._eventParameter.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("DealerGroup", "ID", "V_EventLaporanPenjualanGroupDealer", "DealerID")> _
        Public Property Dealer() As DealerGroup
            Get
                Try
                    If IsNothing(Me._dealer) Then
                        Me._dealer = New DealerGroup
                        Me._dealer.ID = Me._dealerID
                    End If
                    If Not IsNothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

                        Me._dealer = CType(DoLoad(GetType(DealerGroup).ToString(), _dealer.ID), DealerGroup)
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

            Set(ByVal value As DealerGroup)

                Me._dealer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property

#End Region

    End Class
End Namespace

