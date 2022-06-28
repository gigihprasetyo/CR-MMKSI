
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : JaminanDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2010 
'// ---------------------
'// $History      : $
'// Generated on 1/8/2010 - 8:54:37 AM
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
    <Serializable(), TableInfo("JaminanDetail")> _
    Public Class JaminanDetail
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
        Private _periodMonth As Integer
        Private _periodYear As Integer
        Private _vehicleTypeCode As String = String.Empty
        Private _amount As Decimal
        Private _purpose As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        'Private _jaminanID As Integer
        Private _jaminan As Jaminan


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


        <ColumnInfo("PeriodMonth", "{0}")> _
        Public Property PeriodMonth() As Integer
            Get
                Return _periodMonth
            End Get
            Set(ByVal value As Integer)
                _periodMonth = value
            End Set
        End Property


        <ColumnInfo("PeriodYear", "{0}")> _
        Public Property PeriodYear() As Integer
            Get
                Return _periodYear
            End Get
            Set(ByVal value As Integer)
                _periodYear = value
            End Set
        End Property


        <ColumnInfo("VehicleTypeCode", "'{0}'")> _
        Public Property VehicleTypeCode() As String
            Get
                Return _vehicleTypeCode
            End Get
            Set(ByVal value As String)
                _vehicleTypeCode = value
            End Set
        End Property


        <ColumnInfo("Amount", "{0}")> _
        Public Property Amount() As Decimal
            Get
                Return _amount
            End Get
            Set(ByVal value As Decimal)
                _amount = value
            End Set
        End Property


        <ColumnInfo("Purpose", "{0}")> _
        Public Property Purpose() As Short
            Get
                Return _purpose
            End Get
            Set(ByVal value As Short)
                _purpose = value
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


        '<ColumnInfo("JaminanID", "{0}")> _
        'Public Property JaminanID() As Integer

        '    Get
        '        Return _jaminanID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _jaminanID = value
        '    End Set
        'End Property

        <ColumnInfo("JaminanID", "{0}"), _
        RelationInfo("Jaminan", "ID", "JaminanDetail", "JaminanID")> _
        Public Property Jaminan() As Jaminan
            Get
                Try
                    If Not IsNothing(Me._jaminan) AndAlso (Not Me._jaminan.IsLoaded) Then

                        Me._jaminan = CType(DoLoad(GetType(Jaminan).ToString(), _jaminan.ID), Jaminan)
                        Me._jaminan.MarkLoaded()

                    End If

                    Return Me._jaminan

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Jaminan)

                Me._jaminan = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._jaminan.MarkLoaded()
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

