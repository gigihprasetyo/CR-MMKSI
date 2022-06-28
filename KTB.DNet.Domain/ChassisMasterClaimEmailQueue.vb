
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ChassisMasterClaimEmailQueue Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 10/7/2020 - 10:21:03 AM
'//
'// ===========================================================================	
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Collections
Imports System.Collections.Generic
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("ChassisMasterClaimEmailQueue")> _
    Public Class ChassisMasterClaimEmailQueue
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
        Private _claimNumber As String = String.Empty
        Private _statusClaim As Short
        Private _statusReturnProcess As Short
        Private _isSend As Short
        Private _rowStatus As Short
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


        <ColumnInfo("ClaimNumber", "'{0}'")> _
        Public Property ClaimNumber As String
            Get
                Return _claimNumber
            End Get
            Set(ByVal value As String)
                _claimNumber = value
            End Set
        End Property


        <ColumnInfo("StatusClaim", "{0}")> _
        Public Property StatusClaim As Short
            Get
                Return _statusClaim
            End Get
            Set(ByVal value As Short)
                _statusClaim = value
            End Set
        End Property


        <ColumnInfo("StatusReturnProcess", "{0}")> _
        Public Property StatusReturnProcess As Short
            Get
                Return _statusReturnProcess
            End Get
            Set(ByVal value As Short)
                _statusReturnProcess = value
            End Set
        End Property


        <ColumnInfo("IsSend", "{0}")> _
        Public Property IsSend As Short
            Get
                Return _isSend
            End Get
            Set(ByVal value As Short)
                _isSend = value
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

        Public ReadOnly Property RetrieveUnsendToRSD() As List(Of ChassisMasterClaimEmailQueue)
            Get
                Dim lists As New List(Of ChassisMasterClaimEmailQueue)
                Dim _aCMReturns As ArrayList = Nothing
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterClaimEmailQueue), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(ChassisMasterClaimEmailQueue), "StatusReturnProcess", MatchType.InSet, "(1, 2, 6)"))
                criterias.opAnd(New Criteria(GetType(ChassisMasterClaimEmailQueue), "IsSend", MatchType.InSet, "(0, 1)"))
                _aCMReturns = DoLoadArray(GetType(ChassisMasterClaimEmailQueue).ToString, criterias)
                If Not IsNothing(_aCMReturns) Then
                    For Each item As ChassisMasterClaimEmailQueue In DoLoadArray(GetType(ChassisMasterClaimEmailQueue).ToString, criterias)
                        lists.Add(item)
                    Next
                End If
                Return lists
            End Get
        End Property

        Public ReadOnly Property RetrieveUnsendToWSD() As List(Of ChassisMasterClaimEmailQueue)
            Get
                Dim lists As New List(Of ChassisMasterClaimEmailQueue)
                Dim _aCMReturns As ArrayList = Nothing
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterClaimEmailQueue), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(ChassisMasterClaimEmailQueue), "StatusReturnProcess", MatchType.InSet, "(3, 4, 5)"))
                criterias.opAnd(New Criteria(GetType(ChassisMasterClaimEmailQueue), "IsSend", MatchType.InSet, "(0, 1)"))
                _aCMReturns = DoLoadArray(GetType(ChassisMasterClaimEmailQueue).ToString, criterias)
                If Not IsNothing(_aCMReturns) Then
                    For Each item As ChassisMasterClaimEmailQueue In DoLoadArray(GetType(ChassisMasterClaimEmailQueue).ToString, criterias)
                        lists.Add(item)
                    Next
                End If
                Return lists
            End Get
        End Property

        Public ReadOnly Property RetrieveUnsendToVCD() As List(Of ChassisMasterClaimEmailQueue)
            Get
                Dim lists As New List(Of ChassisMasterClaimEmailQueue)
                Dim _aCMReturns As ArrayList = Nothing
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ChassisMasterClaimEmailQueue), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(ChassisMasterClaimEmailQueue), "StatusClaim", MatchType.Exact, "1"))
                criterias.opAnd(New Criteria(GetType(ChassisMasterClaimEmailQueue), "IsSend", MatchType.InSet, "(0, 1)"))
                _aCMReturns = DoLoadArray(GetType(ChassisMasterClaimEmailQueue).ToString, criterias)
                If Not IsNothing(_aCMReturns) Then
                    For Each item As ChassisMasterClaimEmailQueue In DoLoadArray(GetType(ChassisMasterClaimEmailQueue).ToString, criterias)
                        lists.Add(item)
                    Next
                End If
                Return lists
            End Get
        End Property
#End Region

    End Class
End Namespace

