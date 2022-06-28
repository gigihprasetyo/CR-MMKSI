#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : Buletin Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/1/2007 - 10:53:25 AM
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
Imports KTB.DNet.DataMapper.Framework

#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("Buletin")> _
    Public Class Buletin
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
        Private _title As String = String.Empty
        Private _description As String = String.Empty
        Private _validFrom As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _validTo As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _keywords As String = String.Empty
        Private _status As Integer
        Private _uploadBy As String = String.Empty
        Private _uploadDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _periodMonth As Byte
        Private _periodYear As Integer
        Private _fileName As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _buletinCategory As BuletinCategory

        Private _buletinMembers As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _buletinOrganizations As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("Title", "'{0}'")> _
        Public Property Title() As String
            Get
                Return _title
            End Get
            Set(ByVal value As String)
                _title = value
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


        <ColumnInfo("ValidFrom", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ValidFrom() As DateTime
            Get
                Return _validFrom
            End Get
            Set(ByVal value As DateTime)
                _validFrom = value
            End Set
        End Property


        <ColumnInfo("ValidTo", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ValidTo() As DateTime
            Get
                Return _validTo
            End Get
            Set(ByVal value As DateTime)
                _validTo = value
            End Set
        End Property


        <ColumnInfo("Keywords", "'{0}'")> _
        Public Property Keywords() As String
            Get
                Return _keywords
            End Get
            Set(ByVal value As String)
                _keywords = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Integer
            Get
                Return _status
            End Get
            Set(ByVal value As Integer)
                _status = value
            End Set
        End Property


        <ColumnInfo("UploadBy", "'{0}'")> _
        Public Property UploadBy() As String
            Get
                Return _uploadBy
            End Get
            Set(ByVal value As String)
                _uploadBy = value
            End Set
        End Property


        <ColumnInfo("UploadDate", "'{0:yyyy/MM/dd}'")> _
        Public Property UploadDate() As DateTime
            Get
                Return _uploadDate
            End Get
            Set(ByVal value As DateTime)
                _uploadDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("PeriodMonth", "{0}")> _
        Public Property PeriodMonth() As Byte
            Get
                Return _periodMonth
            End Get
            Set(ByVal value As Byte)
                _periodMonth = value
            End Set
        End Property


        <ColumnInfo("PeriodYear", "{0}")> _
        Public Property PeriodYear() As Integer
            Get
                Return _periodYear
            End Get
            Set(ByVal value As Integer)
                _periodYear = value
            End Set
        End Property


        <ColumnInfo("FileName", "'{0}'")> _
        Public Property FileName() As String
            Get
                Return _fileName
            End Get
            Set(ByVal value As String)
                _fileName = value
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


        <ColumnInfo("CategoryID", "{0}"), _
        RelationInfo("BuletinCategory", "ID", "Buletin", "CategoryID")> _
        Public Property BuletinCategory() As BuletinCategory
            Get
                Try
                    If Not IsNothing(Me._buletinCategory) AndAlso (Not Me._buletinCategory.IsLoaded) Then

                        Me._buletinCategory = CType(DoLoad(GetType(BuletinCategory).ToString(), _buletinCategory.ID), BuletinCategory)
                        Me._buletinCategory.MarkLoaded()

                    End If

                    Return Me._buletinCategory

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As BuletinCategory)

                Me._buletinCategory = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._buletinCategory.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("Buletin", "ID", "BuletinMember", "BuletinID")> _
        Public ReadOnly Property BuletinMembers() As System.Collections.ArrayList
            Get
                Try
                    If (Me._buletinMembers.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(BuletinMember), "Buletin", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(BuletinMember), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._buletinMembers = DoLoadArray(GetType(BuletinMember).ToString, criterias)
                    End If

                    Return Me._buletinMembers

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("Buletin", "ID", "BuletinOrganization", "BuletinID")> _
        Public ReadOnly Property BuletinOrganizations() As System.Collections.ArrayList
            Get
                Try
                    If (Me._buletinOrganizations.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(BuletinOrganization), "Buletin", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(BuletinOrganization), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._buletinOrganizations = DoLoadArray(GetType(BuletinOrganization).ToString, criterias)
                    End If

                    Return Me._buletinOrganizations

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

#Region "Custom Properties"

        Private _memberAssigned As Integer = 0
        Private _memberRead As Integer = 0

        Public ReadOnly Property MemberAssigned() As Integer
            Get
                If _memberAssigned = 0 Then
                    Dim m_UserInfoMapper As IMapper = MapperFactory.GetInstance.GetMapper(GetType(KTB.DNet.Domain.UserInfo).ToString)
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserInfo), "ID", MatchType.InSet, "(select UserID from UserGroupMember where UserGroupID in (select UserGroupID from buletingroupmember where buletinid = " & Me.ID.ToString & " ))"))
                    Dim agg As Aggregate = New Aggregate(GetType(KTB.DNet.Domain.UserInfo), "ID", AggregateType.Count)
                    _memberAssigned = CType(m_UserInfoMapper.RetrieveScalar(agg, criterias), Integer)
                End If
                Return _memberAssigned
            End Get
        End Property

        Public ReadOnly Property MemberAssignedOld() As Integer
            Get
                If _memberAssigned = 0 Then
                    Dim m_UserInfoMapper As IMapper = MapperFactory.GetInstance.GetMapper(GetType(KTB.DNet.Domain.UserInfo).ToString)
                    'Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserInfo), "ID", MatchType.InSet, "(select UserID from UserGroupMember where UserGroupID in (Select UserGroupID from BuletinDetail where BuletinCategoryID=" & Me.BuletinCategory.ID.ToString & " ))"))
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(UserInfo), "ID", MatchType.InSet, "(select UserID from buletinmember where buletinid = " & Me.ID.ToString & " )"))
                    'criterias.opAnd(New Criteria(GetType(BuletinMember), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    'criterias.opAnd(New Criteria(GetType(BuletinMember), "ReadStatus", MatchType.Exact, 1))
                    Dim agg As Aggregate = New Aggregate(GetType(KTB.DNet.Domain.UserInfo), "ID", AggregateType.Count)
                    _memberAssigned = CType(m_UserInfoMapper.RetrieveScalar(agg, criterias), Integer)
                End If
                Return _memberAssigned
            End Get
        End Property

        Public ReadOnly Property MemberRead() As Integer
            Get
                Try
                    If _memberRead = 0 Then
                        Dim m_BuletinMemberMapper As IMapper = MapperFactory.GetInstance.GetMapper(GetType(KTB.DNet.Domain.BuletinHistory).ToString)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BuletinHistory), "Buletin", Me.ID))
                        criterias.opAnd(New Criteria(GetType(BuletinHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        Dim agg As Aggregate = New Aggregate(GetType(KTB.DNet.Domain.BuletinHistory), "ReadCount", AggregateType.Sum)
                        _memberRead = CType(m_BuletinMemberMapper.RetrieveScalar(agg, criterias), Integer)

                    End If
                Catch ex As Exception

                End Try
              
                Return _memberRead
            End Get
        End Property

      

        Public ReadOnly Property MemberReadOld() As Integer
            Get
                If _memberRead = 0 Then
                    Dim m_BuletinMemberMapper As IMapper = MapperFactory.GetInstance.GetMapper(GetType(KTB.DNet.Domain.BuletinMember).ToString)
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(BuletinMember), "Buletin", Me.ID))
                    criterias.opAnd(New Criteria(GetType(BuletinMember), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(BuletinMember), "ReadStatus", MatchType.Exact, 1))
                    Dim agg As Aggregate = New Aggregate(GetType(KTB.DNet.Domain.BuletinMember), "ID", AggregateType.Count)
                    _memberRead = CType(m_BuletinMemberMapper.RetrieveScalar(agg, criterias), Integer)

                End If
                Return _memberRead
            End Get
        End Property

        Public ReadOnly Property PercentageMemberRead() As Double
            Get
                Dim intMemberAssigned As Integer = Me.MemberAssigned

                If intMemberAssigned = 0 Then
                    Return 0
                Else
                    Return Math.Round(((Me.MemberRead * 100) / intMemberAssigned))
                End If
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

#End Region

    End Class
End Namespace

