#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ChassisMasterClaimDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 9/7/2020 - 9:16:15 AM
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
    <Serializable(), TableInfo("ChassisMasterClaimDetail")> _
    Public Class ChassisMasterClaimDetail
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
        Private _claimPoint As String = String.Empty
        Private _claimType As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)


        Private _chassisMasterClaimHeader As ChassisMasterClaimHeader


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


        <ColumnInfo("ClaimPoint", "'{0}'")> _
        Public Property ClaimPoint As String
            Get
                Return _claimPoint
            End Get
            Set(ByVal value As String)
                _claimPoint = value
            End Set
        End Property


        <ColumnInfo("ClaimType", "{0}")> _
        Public Property ClaimType As Integer
            Get
                Return _claimType
            End Get
            Set(ByVal value As Integer)
                _claimType = value
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


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime As DateTime
            Get
                Return _LastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _LastUpdateTime = value
            End Set
        End Property


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy As String
            Get
                Return _LastUpdateBy
            End Get
            Set(ByVal value As String)
                _LastUpdateBy = value
            End Set
        End Property


        '<ColumnInfo("CBUReturnID", "{0}")> _
        'Public Property CBUReturnID As Integer
        '    Get
        '        Return _cBUReturnID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _cBUReturnID = value
        '    End Set
        'End Property


        <ColumnInfo("ChassisMasterClaimHeaderID", "{0}"), _
        RelationInfo("ChassisMasterClaimHeader", "ID", "ChassisMasterClaimDetail", "ChassisMasterClaimHeaderID")> _
        Public Property ChassisMasterClaimHeader() As ChassisMasterClaimHeader
            Get
                Try
                    If Not IsNothing(Me._chassisMasterClaimHeader) AndAlso (Not Me._chassisMasterClaimHeader.IsLoaded) Then

                        Me._chassisMasterClaimHeader = CType(DoLoad(GetType(ChassisMasterClaimHeader).ToString(), _chassisMasterClaimHeader.ID), ChassisMasterClaimHeader)
                        Me._chassisMasterClaimHeader.MarkLoaded()

                    End If

                    Return Me._chassisMasterClaimHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ChassisMasterClaimHeader)

                Me._chassisMasterClaimHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._chassisMasterClaimHeader.MarkLoaded()
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
