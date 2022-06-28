#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EstimationEquipPO Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 8/25/2009 - 09:58:22
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
    <Serializable(), TableInfo("EstimationEquipPO")> _
    Public Class EstimationEquipPO
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
        Private _lastUpdatedBy As String = String.Empty
        Private _lastUpdatedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _Note As String = ""
        Private _estimationEquipDetailID As Integer
        Private _sparePartPODetailID As Integer



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

        <ColumnInfo("Note", "'{0}'")> _
        Public Property Note() As String
            Get
                Return _note
            End Get
            Set(ByVal value As String)
                _Note = value
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


        <ColumnInfo("LastUpdatedBy", "'{0}'")> _
        Public Property LastUpdatedBy() As String
            Get
                Return _lastUpdatedBy
            End Get
            Set(ByVal value As String)
                _lastUpdatedBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdatedTime() As DateTime
            Get
                Return _lastUpdatedTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdatedTime = value
            End Set
        End Property


        Dim _estimationEquipDetail As EstimationEquipDetail
        <ColumnInfo("EstimationEquipDetailID", "{0}"), _
        RelationInfo("EstimationEquipDetail", "ID", "EstimationEquipPO", "EstimationEquipDetailID")> _
        Public Property EstimationEquipDetail() As EstimationEquipDetail
            Get
                Try
                    If Not IsNothing(Me._estimationEquipDetail) AndAlso (Not Me._estimationEquipDetail.IsLoaded) Then
                        Me._estimationEquipDetail = CType(DoLoad(GetType(EstimationEquipDetail).ToString(), _estimationEquipDetail.ID), EstimationEquipDetail)
                        Me._estimationEquipDetail.MarkLoaded()
                    End If
                    Return Me._estimationEquipDetail
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get
            Set(ByVal Value As EstimationEquipDetail)
                _estimationEquipDetail = Value
            End Set
        End Property


        Dim _indentPartDetail As IndentPartDetail
        <ColumnInfo("IndentPartDetailID", "{0}"), _
        RelationInfo("IndentPartDetail", "ID", "EstimationEquipPO", "IndentPartDetailID")> _
        Public Property IndentPartDetail() As IndentPartDetail
            Get
                Try
                    If Not IsNothing(Me._indentPartDetail) AndAlso (Not Me._indentPartDetail.IsLoaded) Then
                        Me._indentPartDetail = CType(DoLoad(GetType(IndentPartDetail).ToString(), _indentPartDetail.ID), IndentPartDetail)
                        Me._indentPartDetail.MarkLoaded()
                    End If
                    Return Me._indentPartDetail
                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get
            Set(ByVal Value As IndentPartDetail)
                _indentPartDetail = Value
            End Set
        End Property

        <ColumnInfo("EstimationEquipDetailID", "{0}")> _
        Public Property EstimationEquipDetailID() As Integer
            Get
                Return _estimationEquipDetailID
            End Get
            Set(ByVal value As Integer)
                _estimationEquipDetailID = value
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

