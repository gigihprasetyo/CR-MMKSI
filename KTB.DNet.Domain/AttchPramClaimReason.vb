
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AttchPramClaimReason Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 26/05/2020 - 9:14:28
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
    <Serializable(), TableInfo("AttchPramClaimReason")> _
    Public Class AttchPramClaimReason
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

        Private _claimReason As ClaimReason
        Private _sPSupportClaimDoc As SPSupportClaimDoc



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

        <ColumnInfo("ClaimReasonID", "{0}"), _
        RelationInfo("ClaimReason", "ID", "AttchPramClaimReason", "ClaimReasonID")> _
        Public Property ClaimReason As ClaimReason
            Get
                Try
                    If Not IsNothing(Me._claimReason) AndAlso (Not Me._claimReason.IsLoaded) Then

                        Me._claimReason = CType(DoLoad(GetType(ClaimReason).ToString(), _claimReason.ID), ClaimReason)
                        Me._claimReason.MarkLoaded()

                    End If

                    Return Me._claimReason

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ClaimReason)

                Me._claimReason = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._claimReason.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SPSupportClaimDocID", "{0}"), _
        RelationInfo("SPSupportClaimDoc", "ID", "AttchPramClaimReason", "SPSupportClaimDocID")> _
        Public Property SPSupportClaimDoc() As SPSupportClaimDoc
            Get
                Try
                    If Not IsNothing(Me._sPSupportClaimDoc) AndAlso (Not Me._sPSupportClaimDoc.IsLoaded) Then

                        Me._sPSupportClaimDoc = CType(DoLoad(GetType(SPSupportClaimDoc).ToString(), _sPSupportClaimDoc.ID), SPSupportClaimDoc)
                        Me._sPSupportClaimDoc.MarkLoaded()

                    End If

                    Return Me._sPSupportClaimDoc

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SPSupportClaimDoc)

                Me._sPSupportClaimDoc = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sPSupportClaimDoc.MarkLoaded()
                End If
            End Set
        End Property

        '    <ColumnInfo("ClaimReasonID", "{0}")> _
        '    Public Property ClaimReasonID As Integer

        '        Get
        'return _claimReasonID}
        '        End Get
        '        Set(ByVal value As Integer)
        '            _claimReasonID = value
        '        End Set
        '    End Property

        '    <ColumnInfo("SPSupportClaimDocID", "{0}")> _
        '    Public Property SPSupportClaimDocID As Integer

        '        Get
        'return _sPSupportClaimDocID}
        '        End Get
        '        Set(ByVal value As Integer)
        '            _sPSupportClaimDocID = value
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

