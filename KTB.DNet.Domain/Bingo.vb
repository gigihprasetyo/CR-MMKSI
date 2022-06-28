#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : Bingo Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 1/16/2007 - 9:47:48 AM
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
    <Serializable(), TableInfo("Bingo")> _
    Public Class Bingo
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
        Private _status As Integer
        Private _expiredCount As Integer
        Private _serialNumber As String = String.Empty
        Private _dimensiX As Integer
        Private _dimensiY As Integer
        Private _handphone As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)


        Private _bingoMatrixs As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _userProfiles As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Integer
            Get
                Return _status
            End Get
            Set(ByVal value As Integer)
                _status = value
            End Set
        End Property


        <ColumnInfo("ExpiredCount", "{0}")> _
        Public Property ExpiredCount() As Integer
            Get
                Return _expiredCount
            End Get
            Set(ByVal value As Integer)
                _expiredCount = value
            End Set
        End Property


        <ColumnInfo("SerialNumber", "'{0}'")> _
        Public Property SerialNumber() As String
            Get
                Return _serialNumber
            End Get
            Set(ByVal value As String)
                _serialNumber = value
            End Set
        End Property


        <ColumnInfo("DimensiX", "{0}")> _
        Public Property DimensiX() As Integer
            Get
                Return _dimensiX
            End Get
            Set(ByVal value As Integer)
                _dimensiX = value
            End Set
        End Property


        <ColumnInfo("DimensiY", "{0}")> _
        Public Property DimensiY() As Integer
            Get
                Return _dimensiY
            End Get
            Set(ByVal value As Integer)
                _dimensiY = value
            End Set
        End Property


        <ColumnInfo("Handphone", "'{0}'")> _
        Public Property Handphone() As String
            Get
                Return _handphone
            End Get
            Set(ByVal value As String)
                _handphone = value
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



        <RelationInfo("Bingo", "ID", "BingoMatrix", "BingoID")> _
        Public ReadOnly Property BingoMatrixs() As System.Collections.ArrayList
            Get
                Try
                    If (Me._bingoMatrixs.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(BingoMatrix), "Bingo", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(BingoMatrix), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._bingoMatrixs = DoLoadArray(GetType(BingoMatrix).ToString, criterias)
                    End If

                    Return Me._bingoMatrixs

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("Bingo", "ID", "UserProfile", "BingoID")> _
        Public ReadOnly Property UserProfiles() As System.Collections.ArrayList
            Get
                Try
                    If (Me._userProfiles.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(UserProfile), "Bingo", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(UserProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._userProfiles = DoLoadArray(GetType(UserProfile).ToString, criterias)
                    End If

                    Return Me._userProfiles

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

#End Region

#Region "Custom Property"

        Public ReadOnly Property IsBingoNoExpiration() As Boolean
            Get
                If Me.ExpiredCount < 0 Then
                    Return True
                Else
                    Return False
                End If
            End Get
        End Property


        Public ReadOnly Property IsValidBingo() As Boolean
            Get
                If Me.ExpiredCount < 0 Then
                    Return True
                End If
                If CreatedTime.AddDays(ExpiredCount) >= System.DateTime.Now Then
                    Return True
                End If
                Return False
            End Get
        End Property

        Public ReadOnly Property BingoValidUntil() As System.DateTime
            Get
                Return CreatedTime.AddDays(ExpiredCount)
            End Get
        End Property



#End Region


    End Class
End Namespace

