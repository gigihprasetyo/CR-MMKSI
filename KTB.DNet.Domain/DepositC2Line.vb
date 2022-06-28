#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DepositC2Line Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 25/11/2005 - 16:04:16
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
    <Serializable(), TableInfo("DepositC2Line")> _
    Public Class DepositC2Line
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
        Private _documentNo As String = String.Empty
        Private _documentDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _depositC2Amnt As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _depositC2 As DepositC2

        Private _billingNumber As String = String.Empty


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


        <ColumnInfo("DocumentNo", "'{0}'")> _
        Public Property DocumentNo() As String
            Get
                Return _documentNo
            End Get
            Set(ByVal value As String)
                _documentNo = value
            End Set
        End Property


        <ColumnInfo("DocumentDate", "'{0:yyyy/MM/dd}'")> _
        Public Property DocumentDate() As DateTime
            Get
                Return _documentDate
            End Get
            Set(ByVal value As DateTime)
                _documentDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("DepositC2Amnt", "{0}")> _
        Public Property DepositC2Amnt() As Decimal
            Get
                Return _depositC2Amnt
            End Get
            Set(ByVal value As Decimal)
                _depositC2Amnt = value
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

        <ColumnInfo("DepositC2ID", "{0}"), _
        RelationInfo("DepositC2", "ID", "DepositC2Line", "DepositC2ID")> _
        Public Property DepositC2() As DepositC2
            Get
                Try
                    If Not isnothing(Me._depositC2) AndAlso (Not Me._depositC2.IsLoaded) Then

                        Me._depositC2 = CType(DoLoad(GetType(DepositC2).ToString(), _depositC2.ID), DepositC2)
                        Me._depositC2.MarkLoaded()

                    End If

                    Return Me._depositC2

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DepositC2)

                Me._depositC2 = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._depositC2.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("BillingNumber", "'{0}'")> _
        Public Property BillingNumber As String
            Get
                Return _billingNumber
            End Get
            Set(ByVal value As String)
                _billingNumber = value
            End Set
        End Property

#End Region

#Region "Custom Property"

        Public ReadOnly Property DocDateText() As String
            Get
                Return IIf(Format(DocumentDate, "dd/MM/yyyy") = "01/01/1753", _
                           "", Format(DocumentDate, "dd/MM/yyyy"))
            End Get
        End Property

#End Region

    End Class
End Namespace
