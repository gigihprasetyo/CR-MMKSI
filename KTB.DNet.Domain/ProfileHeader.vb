#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ProfileHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/18/2007 - 8:35:31 AM
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
    <Serializable(), TableInfo("ProfileHeader")> _
    Public Class ProfileHeader
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
        Private _code As String = String.Empty
        Private _description As String = String.Empty
        Private _dataType As Short
        Private _dataLength As Integer
        Private _controlType As Short
        Private _selectionMode As Short
        Private _mandatory As Short
        Private _status As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)


        Private _profileHeaderToGroups As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _profileDetails As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _chassisMasterProfiles As New ArrayList
        Private _customerProfiles As New ArrayList


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


        <ColumnInfo("Code", "'{0}'")> _
        Public Property Code() As String
            Get
                Return _code
            End Get
            Set(ByVal value As String)
                _code = value
            End Set
        End Property


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property


        <ColumnInfo("DataType", "{0}")> _
        Public Property DataType() As Short
            Get
                Return _dataType
            End Get
            Set(ByVal value As Short)
                _dataType = value
            End Set
        End Property


        <ColumnInfo("DataLength", "{0}")> _
        Public Property DataLength() As Integer
            Get
                Return _dataLength
            End Get
            Set(ByVal value As Integer)
                _dataLength = value
            End Set
        End Property


        <ColumnInfo("ControlType", "{0}")> _
        Public Property ControlType() As Short
            Get
                Return _controlType
            End Get
            Set(ByVal value As Short)
                _controlType = value
            End Set
        End Property


        <ColumnInfo("SelectionMode", "{0}")> _
        Public Property SelectionMode() As Short
            Get
                Return _selectionMode
            End Get
            Set(ByVal value As Short)
                _selectionMode = value
            End Set
        End Property


        <ColumnInfo("Mandatory", "{0}")> _
        Public Property Mandatory() As Short
            Get
                Return _mandatory
            End Get
            Set(ByVal value As Short)
                _mandatory = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
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



        <RelationInfo("ProfileHeader", "ID", "ProfileHeaderToGroup", "ProfileHeaderID")> _
        Public ReadOnly Property ProfileHeaderToGroups() As System.Collections.ArrayList
            Get
                Try
                    If (Me._profileHeaderToGroups.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(ProfileHeaderToGroup), "ProfileHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(ProfileHeaderToGroup), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._profileHeaderToGroups = DoLoadArray(GetType(ProfileHeaderToGroup).ToString, criterias)
                    End If

                    Return Me._profileHeaderToGroups

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("ProfileHeader", "ID", "ProfileDetail", "ProfileHeaderID")> _
        Public ReadOnly Property ProfileDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._profileDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(ProfileDetail), "ProfileHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(ProfileDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._profileDetails = DoLoadArray(GetType(ProfileDetail).ToString, criterias)
                    End If

                    Return Me._profileDetails

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("ProfileHeader", "ID", "ChassisMasterProfile", "ProfileHeaderID")> _
        Public ReadOnly Property ChassisMasterProfiles() As System.Collections.ArrayList
            Get
                Try
                    If (Me._chassisMasterProfiles.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(ChassisMasterProfile), "ProfileHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(ChassisMasterProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._chassisMasterProfiles = DoLoadArray(GetType(ChassisMasterProfile).ToString, criterias)
                    End If

                    Return Me._chassisMasterProfiles

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property
        <RelationInfo("ProfileHeader", "ID", "CustomerProfile", "ProfileHeaderID")> _
        Public ReadOnly Property CustomerProfiles() As ArrayList
            Get
                Try
                    If (Me._customerProfiles.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(CustomerProfile), "ProfileHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(CustomerProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._customerProfiles = DoLoadArray(GetType(CustomerProfile).ToString, criterias)
                    End If

                    Return Me._customerProfiles

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
        Private _profileHeaderValue As String
        Public Property ProfileHeaderValue() As String
            Get
                Return _profileHeaderValue
            End Get
            Set(ByVal Value As String)
                _profileHeaderValue = Value
            End Set
        End Property
#End Region

    End Class
End Namespace

