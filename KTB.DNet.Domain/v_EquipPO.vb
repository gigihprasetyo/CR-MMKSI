#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : v_EquipPO Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 8/27/2009 - 12:01:06
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
    <Serializable(), TableInfo("v_EquipPO")> _
    Public Class v_EquipPO
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
        Private _status As String = String.Empty
        Private _statusKTB As String = String.Empty
        Private _statusDesc As String = String.Empty
        Private _dealerCode As String = String.Empty
        Private _dealerName As String = String.Empty
        Private _RequestNo As String = String.Empty
        Private _estimationNumber As String = String.Empty
        Private _paymentType As Byte
        Private _paymentTypeDesc As String = String.Empty
        Private _totalItem As Integer
        Private _totalEstimationUnit As Integer
        Private _sisaQty As Integer
        Private _materialType As Integer
        Private _rowStatus As Short
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _createdBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty

        Private _indentPartDetails As System.Collections.ArrayList = New System.Collections.ArrayList


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

        <ColumnInfo("StatusKTB", "'{0}'")> _
        Public Property StatusKTB() As String
            Get
                Return _statusKTB
            End Get
            Set(ByVal value As String)
                _statusKTB = value
            End Set
        End Property



        <ColumnInfo("Status", "'{0}'")> _
        Public Property Status() As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
            End Set
        End Property


        <ColumnInfo("StatusDesc", "'{0}'")> _
        Public Property StatusDesc() As String
            Get
                Return _statusDesc
            End Get
            Set(ByVal value As String)
                _statusDesc = value
            End Set
        End Property


        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode() As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property


        <ColumnInfo("DealerName", "'{0}'")> _
        Public Property DealerName() As String
            Get
                Return _dealerName
            End Get
            Set(ByVal value As String)
                _dealerName = value
            End Set
        End Property


        <ColumnInfo("RequestNo", "'{0}'")> _
        Public Property RequestNo() As String
            Get
                Return _RequestNo
            End Get
            Set(ByVal value As String)
                _RequestNo = value
            End Set
        End Property


        <ColumnInfo("EstimationNumber", "'{0}'")> _
        Public Property EstimationNumber() As String
            Get
                Return _estimationNumber
            End Get
            Set(ByVal value As String)
                _estimationNumber = value
            End Set
        End Property


        <ColumnInfo("PaymentType", "{0}")> _
        Public Property PaymentType() As Byte
            Get
                Return _paymentType
            End Get
            Set(ByVal value As Byte)
                _paymentType = value
            End Set
        End Property


        <ColumnInfo("PaymentTypeDesc", "'{0}'")> _
        Public Property PaymentTypeDesc() As String
            Get
                Return _paymentTypeDesc
            End Get
            Set(ByVal value As String)
                _paymentTypeDesc = value
            End Set
        End Property


        <ColumnInfo("TotalItem", "{0}")> _
        Public Property TotalItem() As Integer
            Get
                Return _totalItem
            End Get
            Set(ByVal value As Integer)
                _totalItem = value
            End Set
        End Property


        <ColumnInfo("TotalEstimationUnit", "{0}")> _
        Public Property TotalEstimationUnit() As Integer
            Get
                Return _totalEstimationUnit
            End Get
            Set(ByVal value As Integer)
                _totalEstimationUnit = value
            End Set
        End Property


        <ColumnInfo("SisaQty", "{0}")> _
        Public Property SisaQty() As Integer
            Get
                Return _sisaQty
            End Get
            Set(ByVal value As Integer)
                _sisaQty = value
            End Set
        End Property

        <ColumnInfo("MaterialType", "{0}")> _
        Public Property MaterialType() As Integer
            Get
                Return _materialType
            End Get
            Set(ByVal value As Integer)
                _materialType = value
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


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime() As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
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


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime() As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
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


        <RelationInfo("v_EquipPO", "ID", "IndentPartDetail", "IndentPartHeaderID")> _
        Public ReadOnly Property IndentPartDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._indentPartDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(IndentPartDetail), "IndentPartHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(IndentPartDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._indentPartDetails = DoLoadArray(GetType(IndentPartDetail).ToString, criterias)
                    End If

                    Return Me._indentPartDetails

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

