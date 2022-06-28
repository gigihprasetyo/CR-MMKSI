
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : SPKChassis Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 22/02/2018 - 11:16:49
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
    <Serializable(), TableInfo("SPKChassis")> _
    Public Class SPKChassis
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
        Private _matchingType As Short
        Private _matchingDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _matchingNumber As String = String.Empty
        Private _referenceNumber As String = String.Empty
        Private _keyNumber As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _chassisMaster As ChassisMaster
        Private _sPKDetail As SPKDetail

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


        <ColumnInfo("MatchingType", "{0}")> _
        Public Property MatchingType As Short
            Get
                Return _matchingType
            End Get
            Set(ByVal value As Short)
                _matchingType = value
            End Set
        End Property


        <ColumnInfo("MatchingDate", "'{0:yyyy/MM/dd}'")> _
        Public Property MatchingDate As DateTime
            Get
                Return _matchingDate
            End Get
            Set(ByVal value As DateTime)
                _matchingDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("MatchingNumber", "'{0}'")> _
        Public Property MatchingNumber As String
            Get
                Return _matchingNumber
            End Get
            Set(ByVal value As String)
                _matchingNumber = value
            End Set
        End Property


        <ColumnInfo("ReferenceNumber", "'{0}'")> _
        Public Property ReferenceNumber As String
            Get
                Return _referenceNumber
            End Get
            Set(ByVal value As String)
                _referenceNumber = value
            End Set
        End Property


        <ColumnInfo("KeyNumber", "'{0}'")> _
        Public Property KeyNumber As String
            Get
                Return _keyNumber
            End Get
            Set(ByVal value As String)
                _keyNumber = value
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


        <ColumnInfo("ChassisMasterID", "{0}"), _
        RelationInfo("ChassisMaster", "ID", "SPKChassis", "ChassisMasterID")> _
        Public Property ChassisMaster As ChassisMaster
            Get
                Try
                    If Not IsNothing(Me._chassisMaster) AndAlso (Not Me._chassisMaster.IsLoaded) Then

                        Me._chassisMaster = CType(DoLoad(GetType(ChassisMaster).ToString(), _chassisMaster.ID), ChassisMaster)
                        Me._chassisMaster.MarkLoaded()

                    End If

                    Return Me._chassisMaster

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ChassisMaster)

                Me._chassisMaster = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._chassisMaster.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SPKDetailID", "{0}"), _
        RelationInfo("SPKDetail", "ID", "SPKChassis", "SPKDetailID")> _
        Public Property SPKDetail As SPKDetail
            Get
                Try
                    If Not IsNothing(Me._sPKDetail) AndAlso (Not Me._sPKDetail.IsLoaded) Then

                        Me._sPKDetail = CType(DoLoad(GetType(SPKDetail).ToString(), _sPKDetail.ID), SPKDetail)
                        Me._sPKDetail.MarkLoaded()

                    End If

                    Return Me._sPKDetail

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SPKDetail)

                Me._sPKDetail = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sPKDetail.MarkLoaded()
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

