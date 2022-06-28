
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SPKFaktur Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 3/30/2011 - 9:59:17 AM
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
    <Serializable(), TableInfo("SPKFaktur")> _
    Public Class SPKFaktur
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
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _createdBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty

        Private _sPKHeader As SPKHeader
        Private _endCustomer As EndCustomer



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


        <ColumnInfo("lastUpdateBy", "'{0}'")> _
        Public Property lastUpdateBy() As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
            End Set
        End Property


        <ColumnInfo("SPKHeaderID", "{0}"), _
        RelationInfo("SPKHeader", "ID", "SPKFaktur", "SPKHeaderID")> _
        Public Property SPKHeader() As SPKHeader
            Get
                Try
                    If Not isnothing(Me._sPKHeader) AndAlso (Not Me._sPKHeader.IsLoaded) Then

                        Me._sPKHeader = CType(DoLoad(GetType(SPKHeader).ToString(), _sPKHeader.ID), SPKHeader)
                        Me._sPKHeader.MarkLoaded()

                    End If

                    Return Me._sPKHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SPKHeader)

                Me._sPKHeader = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sPKHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("EndCustomerID", "{0}"), _
        RelationInfo("EndCustomer", "ID", "SPKFaktur", "EndCustomerID")> _
        Public Property EndCustomer() As EndCustomer
            Get
                Try
                    If Not isnothing(Me._endCustomer) AndAlso (Not Me._endCustomer.IsLoaded) Then

                        Me._endCustomer = CType(DoLoad(GetType(EndCustomer).ToString(), _endCustomer.ID), EndCustomer)
                        Me._endCustomer.MarkLoaded()

                    End If

                    Return Me._endCustomer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As EndCustomer)

                Me._endCustomer = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._endCustomer.MarkLoaded()
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

