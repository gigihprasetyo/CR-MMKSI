
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitPameranExpense Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 05/16/2019 - 2:19:03 PM
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
    <Serializable(), TableInfo("BabitPameranExpense")> _
    Public Class BabitPameranExpense
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
        'private _babitHeaderID as integer 		
        'private _babitParameterDetailID as integer 		
        Private _item As String = String.Empty
        Private _qty As Integer
        Private _price As Decimal
        Private _description As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _totalprice As Decimal

        Private _babitHeader As BabitHeader
        Private _babitParameterDetail As BabitParameterDetail

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


        '<ColumnInfo("BabitHeaderID","{0}")> _
        'public property BabitHeaderID as integer
        '	get
        '		return _babitHeaderID
        '	end get
        '	set(byval value as integer)
        '		_babitHeaderID= value
        '	end set
        'end property


        '<ColumnInfo("BabitParameterDetailID","{0}")> _
        'public property BabitParameterDetailID as integer
        '	get
        '		return _babitParameterDetailID
        '	end get
        '	set(byval value as integer)
        '		_babitParameterDetailID= value
        '	end set
        'end property


        <ColumnInfo("Item", "'{0}'")> _
        Public Property Item As String
            Get
                Return _item
            End Get
            Set(ByVal value As String)
                _item = value
            End Set
        End Property


        <ColumnInfo("Qty", "{0}")> _
        Public Property Qty As Integer
            Get
                Return _qty
            End Get
            Set(ByVal value As Integer)
                _qty = value
            End Set
        End Property


        <ColumnInfo("Price", "{0}")> _
        Public Property Price As Decimal
            Get
                Return _price
            End Get
            Set(ByVal value As Decimal)
                _price = value
            End Set
        End Property


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
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


        <ColumnInfo("BabitHeaderID", "{0}"), _
        RelationInfo("BabitHeader", "ID", "BabitPameranExpense", "BabitHeaderID")> _
        Public Property BabitHeader As BabitHeader
            Get
                Try
                    If Not IsNothing(Me._babitHeader) AndAlso (Not Me._babitHeader.IsLoaded) Then

                        Me._babitHeader = CType(DoLoad(GetType(BabitHeader).ToString(), _babitHeader.ID), BabitHeader)
                        Me._babitHeader.MarkLoaded()
                    End If
                    Return Me._babitHeader
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get

            Set(ByVal value As BabitHeader)
                Me._babitHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._babitHeader.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("BabitParameterDetailID", "{0}"), _
        RelationInfo("BabitParameterDetail", "ID", "BabitPameranExpense", "BabitParameterDetailID")> _
        Public Property BabitParameterDetail As BabitParameterDetail
            Get
                Try
                    If Not IsNothing(Me._babitParameterDetail) AndAlso (Not Me._babitParameterDetail.IsLoaded) Then

                        Me._babitParameterDetail = CType(DoLoad(GetType(BabitParameterDetail).ToString(), _babitParameterDetail.ID), BabitParameterDetail)
                        Me._babitParameterDetail.MarkLoaded()
                    End If
                    Return Me._babitParameterDetail
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get

            Set(ByVal value As BabitParameterDetail)
                Me._babitParameterDetail = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._babitParameterDetail.MarkLoaded()
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
        Public Property TotalPrice As Decimal
            Get
                Return _totalprice
            End Get
            Set(ByVal value As Decimal)
                _totalprice = value
            End Set
        End Property
#End Region

    End Class
End Namespace

