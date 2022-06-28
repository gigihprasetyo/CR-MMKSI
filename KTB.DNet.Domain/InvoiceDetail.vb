
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : InvoiceDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2008 
'// ---------------------
'// $History      : $
'// Generated on 12/9/2008 - 8:45:26 AM
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
    <Serializable(), TableInfo("InvoiceDetail")> _
    Public Class InvoiceDetail
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
        Private _invoiceItem As Long
        Private _billedQty As Long
        Private _itemAmount As Decimal
        Private _pPH22 As Decimal
        Private _interest As Decimal
        Private _category As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _invoiceHeader As InvoiceHeader
        Private _vechileColor As VechileColor


        Private _chassisMaster As ChassisMaster = New ChassisMaster(0)

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


        <ColumnInfo("InvoiceItem", "{0}")> _
        Public Property InvoiceItem() As Long
            Get
                Return _invoiceItem
            End Get
            Set(ByVal value As Long)
                _invoiceItem = value
            End Set
        End Property


        <ColumnInfo("BilledQty", "{0}")> _
        Public Property BilledQty() As Long
            Get
                Return _billedQty
            End Get
            Set(ByVal value As Long)
                _billedQty = value
            End Set
        End Property


        <ColumnInfo("ItemAmount", "{0}")> _
        Public Property ItemAmount() As Decimal
            Get
                Return _itemAmount
            End Get
            Set(ByVal value As Decimal)
                _itemAmount = value
            End Set
        End Property


        <ColumnInfo("PPH22", "{0}")> _
        Public Property PPH22() As Decimal
            Get
                Return _pPH22
            End Get
            Set(ByVal value As Decimal)
                _pPH22 = value
            End Set
        End Property


        <ColumnInfo("Interest", "{0}")> _
        Public Property Interest() As Decimal
            Get
                Return _interest
            End Get
            Set(ByVal value As Decimal)
                _interest = value
            End Set
        End Property


        <ColumnInfo("Category", "'{0}'")> _
        Public Property Category() As String
            Get
                Return _category
            End Get
            Set(ByVal value As String)
                _category = value
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


        <ColumnInfo("InvoiceHeaderID", "{0}"), _
        RelationInfo("InvoiceHeader", "ID", "InvoiceDetail", "InvoiceHeaderID")> _
        Public Property InvoiceHeader() As InvoiceHeader
            Get
                Try
                    If Not isnothing(Me._invoiceHeader) AndAlso (Not Me._invoiceHeader.IsLoaded) Then

                        Me._invoiceHeader = CType(DoLoad(GetType(InvoiceHeader).ToString(), _invoiceHeader.ID), InvoiceHeader)
                        Me._invoiceHeader.MarkLoaded()

                    End If

                    Return Me._invoiceHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As InvoiceHeader)

                Me._invoiceHeader = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._invoiceHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("VechileColorID", "{0}"), _
        RelationInfo("VechileColor", "ID", "InvoiceDetail", "VechileColorID")> _
        Public Property VechileColor() As VechileColor
            Get
                Try
                    If Not isnothing(Me._vechileColor) AndAlso (Not Me._vechileColor.IsLoaded) Then

                        Me._vechileColor = CType(DoLoad(GetType(VechileColor).ToString(), _vechileColor.ID), VechileColor)
                        Me._vechileColor.MarkLoaded()

                    End If

                    Return Me._vechileColor

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As VechileColor)

                Me._vechileColor = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._vechileColor.MarkLoaded()
                End If
            End Set
        End Property



        <ColumnInfo("ID", "{0}"), _
        RelationInfo("InvoiceDetail", "ID", "ChassisMaster", "InvoiceDetailID")> _
        Public ReadOnly Property ChassisMaster() As ChassisMaster
            Get
                Try
                    If Not isnothing(Me._chassisMaster) AndAlso (Not Me._chassisMaster.IsLoaded) Then
                        Dim _criteria As Criteria = New Criteria(GetType(ChassisMaster), "InvoiceDetail", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Dim tempColl As ArrayList = DoLoadArray(GetType(ChassisMaster).ToString, criterias)

                        If (tempColl.Count > 0) Then
                            Me._chassisMaster = CType(tempColl(0), ChassisMaster)
                        Else
                            Me._chassisMaster = Nothing
                        End If
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

