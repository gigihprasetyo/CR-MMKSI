#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PDI Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/30/2005 - 1:01:07 PM
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
    <Serializable(), TableInfo("PDI")> _
    Public Class PDI
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
        Private _kind As String = String.Empty
        Private _pDIStatus As String = String.Empty
        Private _pDIDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _releaseBy As String = String.Empty
        Private _releaseDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _workOrderNumber As String = String.Empty
        Private _rowStatus As Short
        Private _fileName As String
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealerBranch As DealerBranch
        Private _chassisMaster As ChassisMaster
        Private _dealer As Dealer



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


        <ColumnInfo("Kind", "'{0}'")> _
        Public Property Kind() As String
            Get
                Return _kind
            End Get
            Set(ByVal value As String)
                _kind = value
            End Set
        End Property


        <ColumnInfo("PDIStatus", "'{0}'")> _
        Public Property PDIStatus() As String
            Get
                Return _pDIStatus
            End Get
            Set(ByVal value As String)
                _pDIStatus = value
            End Set
        End Property


        <ColumnInfo("PDIDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PDIDate() As DateTime
            Get
                Return _pDIDate
            End Get
            Set(ByVal value As DateTime)
                _pDIDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("ReleaseBy", "'{0}'")> _
        Public Property ReleaseBy() As String
            Get
                Return _releaseBy
            End Get
            Set(ByVal value As String)
                _releaseBy = value
            End Set
        End Property

        <ColumnInfo("WorkOrderNumber", "'{0}'")> _
        Public Property WorkOrderNumber() As String
            Get
                Return _workOrderNumber
            End Get
            Set(ByVal value As String)
                _workOrderNumber = value
            End Set
        End Property


        <ColumnInfo("ReleaseDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ReleaseDate() As DateTime
            Get
                Return _releaseDate
            End Get
            Set(ByVal value As DateTime)
                _releaseDate = New DateTime(value.Year, value.Month, value.Day)
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


        <ColumnInfo("FileName", "{0}")> _
        Public Property FileName() As String
            Get
                Return _fileName
            End Get
            Set(ByVal value As String)
                _fileName = value
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



        <ColumnInfo("DealerBranchID", "{0}"), _
        RelationInfo("DealerBranch", "ID", "PDI", "DealerBranchID")> _
        Public Property DealerBranch() As DealerBranch
            Get
                Try
                    If Not IsNothing(Me._dealerBranch) AndAlso (Not Me._dealerBranch.IsLoaded) Then

                        Me._dealerBranch = CType(DoLoad(GetType(DealerBranch).ToString(), _dealerBranch.ID), DealerBranch)
                        Me._dealerBranch.MarkLoaded()

                    End If

                    Return Me._dealerBranch

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DealerBranch)

                Me._dealerBranch = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealerBranch.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ChassisMasterID", "{0}"), _
        RelationInfo("ChassisMaster", "ID", "PDI", "ChassisMasterID")> _
        Public Property ChassisMaster() As ChassisMaster
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

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "PDI", "DealerID")> _
        Public Property Dealer() As Dealer
            Get
                Try
                    If Not IsNothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

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
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
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

#Region "Non_generated Properties"
        Private _ChassisNumberMsg As String = String.Empty
        Private _DealerBranchCodeMsg As String = String.Empty
        Private _PDIKindMsg As String = String.Empty
        Private _PDIDateMsg As String = String.Empty

        Public Property ChassisNumberMsg() As String
            Get
                Return _ChassisNumberMsg
            End Get
            Set(ByVal value As String)
                _ChassisNumberMsg = value
            End Set
        End Property

        Public Property DealerBranchCodeMsg() As String
            Get
                Return _DealerBranchCodeMsg
            End Get
            Set(ByVal value As String)
                _DealerBranchCodeMsg = value
            End Set
        End Property

        Public Property PDIKindMsg() As String
            Get
                Return _PDIKindMsg
            End Get
            Set(ByVal value As String)
                _PDIKindMsg = value
            End Set
        End Property

        Public Property PDIDateMsg() As String
            Get
                Return _PDIDateMsg
            End Get
            Set(ByVal value As String)
                _PDIDateMsg = value
            End Set
        End Property
#End Region

#Region "Custom Method"
        Public ReadOnly Property PilotingPDI() As String
            Get
                Dim criterias As New CriteriaComposite(New Criteria(GetType(TransactionControl), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(TransactionControl), "Dealer.ID", MatchType.Exact, Me.Dealer.ID))
                criterias.opAnd(New Criteria(GetType(TransactionControl), "Status", MatchType.Exact, "1"))
                criterias.opAnd(New Criteria(GetType(TransactionControl), "Kind", MatchType.Exact, CInt(EnumDealerTransType.DealerTransKind.PilotingPDI).ToString))
                Dim datas As ArrayList = DoLoadArray(GetType(TransactionControl).ToString, criterias)
                If datas.Count > 0 Then
                    Return "1"
                Else
                    Return "0"
                End If
            End Get
        End Property
#End Region

    End Class
End Namespace

