#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DepositC2 Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 25/11/2005 - 15:31:08
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
    <Serializable(), TableInfo("DepositC2")> _
    Public Class DepositC2
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
        Private _period As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer

        Private _depositC2Lines As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("Period", "'{0}'")> _
        Public Property Period() As String
            Get
                Return _period
            End Get
            Set(ByVal value As String)
                _period = value
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




        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "DepositC2", "DealerID")> _
        Public Property Dealer() As Dealer
            Get
                Try
                    If Not isnothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

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
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("DepositC2", "ID", "DepositC2Line", "DepositC2ID")> _
        Public ReadOnly Property DepositC2Lines() As System.Collections.ArrayList
            Get
                Try
                    If (Me._depositC2Lines.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(DepositC2Line), "DepositC2", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DepositC2Line), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._depositC2Lines = DoLoadArray(GetType(DepositC2Line).ToString, criterias)
                    End If

                    Return Me._depositC2Lines

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

#Region "Custom Method"

#End Region

    End Class
End Namespace
