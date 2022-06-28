#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AlertSound Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/26/2007 - 4:56:27 PM
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
    <Serializable(), TableInfo("SAPCustomerMapping")> _
    Public Class SAPCustomerMapping
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
        Private _SourceLead As String
        Private _SourceInformation As String
        Private _SourceLeadDesc As String
        Private _SourceInformationDesc As String
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _SAPCustomer As SAPCustomer



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



        <ColumnInfo("SourceLead", "'{0}'")> _
        Public Property SourceLead() As String
            Get
                Return _SourceLead
            End Get
            Set(ByVal value As String)
                _SourceLead = value
            End Set
        End Property

        Public ReadOnly Property SourceLeadDesc() As String
            Get
                If String.IsNullOrEmpty(_SourceLeadDesc) And _SourceLead > 0 Then

                End If

                Return _SourceLeadDesc
            End Get
        End Property

        Public ReadOnly Property SourceInformationDesc() As String
            Get
                Return _SourceInformationDesc
            End Get
        End Property

        <ColumnInfo("SourceInformation", "'{0}'")> _
        Public Property SourceInformation() As String
            Get
                Return _SourceInformation
            End Get
            Set(ByVal value As String)
                _SourceInformation = value
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


        <ColumnInfo("SAPCustomerID", "{0}"), _
        RelationInfo("SAPCustomer", "ID", "SAPCustomerMapping", "SAPCustomerID")> _
        Public Property SAPCustomer() As SAPCustomer
            Get
                Try
                    If Not IsNothing(Me._SAPCustomer) AndAlso (Not Me._SAPCustomer.IsLoaded) Then

                        Me._SAPCustomer = CType(DoLoad(GetType(SAPCustomer).ToString(), _SAPCustomer.ID), SAPCustomer)
                        Me._SAPCustomer.MarkLoaded()

                    End If

                    Return Me._SAPCustomer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SAPCustomer)

                Me._SAPCustomer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._SAPCustomer.MarkLoaded()
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

