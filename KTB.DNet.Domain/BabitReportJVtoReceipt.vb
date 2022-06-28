
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitReportJVtoReceipt Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 02/10/2019 - 14:02:37
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
    <Serializable(), TableInfo("BabitReportJVtoReceipt")> _
    Public Class BabitReportJVtoReceipt
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
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        'Private _babitReportJVID As Integer
        'Private _babitReportReceiptID As Integer
        Private _babitReportJV As BabitReportJV
        Private _babitReportReceipt As BabitReportReceipt



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

        <ColumnInfo("BabitReportJVID", "{0}"), _
        RelationInfo("BabitReportJV", "ID", "BabitReportJVtoReceipt", "BabitReportJVID")> _
        Public Property BabitReportJV As BabitReportJV
            Get
                Try
                    If Not IsNothing(Me._babitReportJV) AndAlso (Not Me._babitReportJV.IsLoaded) Then

                        Me._babitReportJV = CType(DoLoad(GetType(BabitReportJV).ToString(), _babitReportJV.ID), BabitReportJV)
                        Me._babitReportJV.MarkLoaded()
                    End If
                    Return Me._babitReportJV
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get

            Set(ByVal value As BabitReportJV)
                Me._babitReportJV = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._babitReportJV.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("BabitReportReceiptID", "{0}"), _
        RelationInfo("BabitReportReceipt", "ID", "BabitReportJVtoReceipt", "BabitReportReceiptID")> _
        Public Property BabitReportReceipt As BabitReportReceipt
            Get
                Try
                    If Not IsNothing(Me._babitReportReceipt) AndAlso (Not Me._babitReportReceipt.IsLoaded) Then

                        Me._babitReportReceipt = CType(DoLoad(GetType(BabitReportReceipt).ToString(), _babitReportReceipt.ID), BabitReportReceipt)
                        Me._babitReportReceipt.MarkLoaded()
                    End If
                    Return Me._babitReportReceipt
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get

            Set(ByVal value As BabitReportReceipt)
                Me._babitReportReceipt = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._babitReportReceipt.MarkLoaded()
                End If
            End Set
        End Property

        '    <ColumnInfo("BabitReportJVID", "{0}")> _
        '    Public Property BabitReportJVID As Integer

        '        Get
        'return _babitReportJVID}
        '        End Get
        '        Set(ByVal value As Integer)
        '            _babitReportJVID = value
        '        End Set
        '    End Property
        '    <ColumnInfo("BabitReportReceiptID", "{0}")> _
        '    Public Property BabitReportReceiptID As Integer

        '        Get
        'return _babitReportReceiptID}
        '        End Get
        '        Set(ByVal value As Integer)
        '            _babitReportReceiptID = value
        '        End Set
        '    End Property


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

