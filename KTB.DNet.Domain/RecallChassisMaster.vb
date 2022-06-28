
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : RecallChassisMaster Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 19/04/2016 - 11:30:16
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
    <Serializable(), TableInfo("RecallChassisMaster")> _
    Public Class RecallChassisMaster
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
        Private _chassisNo As String = String.Empty
        Private _recallCategory As RecallCategory
        Private _recallServices As System.Collections.ArrayList = New System.Collections.ArrayList

        Private _rowStatus As Short
        Private _isService As Boolean

        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)




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


        <ColumnInfo("ChassisNo", "'{0}'")> _
        Public Property ChassisNo As String
            Get
                Return _chassisNo
            End Get
            Set(ByVal value As String)
                _chassisNo = value
            End Set
        End Property

        <ColumnInfo("RecallCategoryID", "{0}"), _
     RelationInfo("RecallChassisMaster", "RecallCategoryID", "RecallCategory", "ID")> _
        Public Property RecallCategory As RecallCategory
            Get
                Try
                    If Not IsNothing(Me._recallCategory) AndAlso (Not Me._recallCategory.IsLoaded) Then
                        Me._recallCategory = CType(DoLoad(GetType(RecallCategory).ToString(), _recallCategory.ID), RecallCategory)
                        Me._recallCategory.MarkLoaded()
                    End If

                    Return Me._recallCategory

                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get
            Set(value As RecallCategory)
                _recallCategory = value


                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._recallCategory.MarkLoaded()
                End If
            End Set
        End Property

        <RelationInfo("RecallChassisMaster", "ID", "RecallService", "RecallChassisMasterID")> _
        Public ReadOnly Property RecallServices() As System.Collections.ArrayList
            Get
                Try
                    If (Me._recallServices.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(RecallService), "RecallChassisMaster.ID", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(RecallService), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._recallServices = DoLoadArray(GetType(RecallService).ToString, criterias)
                    End If

                    Return Me._recallServices

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get
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

        Private _RecallRegNo As String = ""
        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property RecallRegNo As String
            Get
                Return _RecallRegNo
            End Get
            Set(ByVal value As String)
                _RecallRegNo = value
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
        Public Property IsService As Boolean
            Get
                If Not IsNothing(RecallServices) AndAlso RecallServices.Count > 0 Then
                    _isService = True
                Else
                    _isService = False
                End If

                Return _isService
            End Get
            Set(ByVal value As Boolean)
                _isService = value
            End Set
        End Property
#End Region

    End Class

End Namespace

