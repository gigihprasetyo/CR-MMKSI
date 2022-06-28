#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EventMaster Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/21/2007 - 9:24:01 AM
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
    <Serializable(), TableInfo("EventMaster")> _
    Public Class EventMaster
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
        Private _eventNo As String = String.Empty
        Private _period As Short
        Private _startMonth As Short
        Private _endMonth As Short
        Private _fileMaterialName As String = String.Empty
        Private _fileDirectionName As String = String.Empty
        Private _fileProposalName As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)


        Private _eventInfos As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("EventNo", "'{0}'")> _
        Public Property EventNo() As String
            Get
                Return _eventNo
            End Get
            Set(ByVal value As String)
                _eventNo = value
            End Set
        End Property


        <ColumnInfo("Period", "{0}")> _
        Public Property Period() As Short
            Get
                Return _period
            End Get
            Set(ByVal value As Short)
                _period = value
            End Set
        End Property


        <ColumnInfo("StartMonth", "{0}")> _
        Public Property StartMonth() As Short
            Get
                Return _startMonth
            End Get
            Set(ByVal value As Short)
                _startMonth = value
            End Set
        End Property


        <ColumnInfo("EndMonth", "{0}")> _
        Public Property EndMonth() As Short
            Get
                Return _endMonth
            End Get
            Set(ByVal value As Short)
                _endMonth = value
            End Set
        End Property


        <ColumnInfo("FileMaterialName", "'{0}'")> _
        Public Property FileMaterialName() As String
            Get
                Return _fileMaterialName
            End Get
            Set(ByVal value As String)
                _fileMaterialName = value
            End Set
        End Property


        <ColumnInfo("FileDirectionName", "'{0}'")> _
        Public Property FileDirectionName() As String
            Get
                Return _fileDirectionName
            End Get
            Set(ByVal value As String)
                _fileDirectionName = value
            End Set
        End Property


        <ColumnInfo("FileProposalName", "'{0}'")> _
        Public Property FileProposalName() As String
            Get
                Return _fileProposalName
            End Get
            Set(ByVal value As String)
                _fileProposalName = value
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



        <RelationInfo("EventMaster", "ID", "EventInfo", "EventMasterID")> _
        Public ReadOnly Property EventInfos() As System.Collections.ArrayList
            Get
                Try
                    If (Me._eventInfos.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(EventInfo), "EventMaster", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(EventInfo), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._eventInfos = DoLoadArray(GetType(EventInfo).ToString, criterias)
                    End If

                    Return Me._eventInfos

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

    End Class
End Namespace

