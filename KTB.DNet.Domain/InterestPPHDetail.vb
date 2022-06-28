#Region "Summary"
'// ===========================================================================
'// AUTHOR        : dnet
'// PURPOSE       : InterestPPHDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 8/9/2021 - 10:57:19 AM
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
    <Serializable(), TableInfo("InterestPPHDetail")> _
    Public Class InterestPPHDetail
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
        'Private _salesOrderInterestID As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _salesOrderInterest As SalesOrderInterest
        Private _interestPPHHeader As InterestPPHHeader



#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property


        '<ColumnInfo("SalesOrderInterestID", "{0}")> _
        'Public Property SalesOrderInterestID As Integer
        '    Get
        '        Return _salesOrderInterestID
        '    End Get
        '    Set(ByVal value As Integer)
        '        _salesOrderInterestID = value
        '    End Set
        'End Property


        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus As Short
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Short)
                _rowStatus = value
            End Set
        End Property


        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
            End Set
        End Property


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
            End Set
        End Property


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
            End Set
        End Property


        <ColumnInfo("SalesOrderInterestID", "{0}"), _
       RelationInfo("SalesOrderInterest", "ID", "InterestPPHDetail", "SalesOrderInterestID")> _
        Public Property SalesOrderInterest() As SalesOrderInterest
            Get
                Try
                    If Not IsNothing(Me._salesOrderInterest) AndAlso (Not Me._salesOrderInterest.IsLoaded) Then

                        Me._salesOrderInterest = CType(DoLoad(GetType(SalesOrderInterest).ToString(), _salesOrderInterest.ID), SalesOrderInterest)
                        Me._salesOrderInterest.MarkLoaded()

                    End If

                    Return Me._salesOrderInterest

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SalesOrderInterest)

                Me._salesOrderInterest = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._salesOrderInterest.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("InterestPPHHeaderID", "{0}"), _
       RelationInfo("InterestPPHHeader", "ID", "InterestPPHDetail", "InterestPPHHeaderID")> _
        Public Property InterestPPHHeader() As InterestPPHHeader
            Get
                Try
                    If Not IsNothing(Me._interestPPHHeader) AndAlso (Not Me._interestPPHHeader.IsLoaded) Then

                        Me._interestPPHHeader = CType(DoLoad(GetType(InterestPPHHeader).ToString(), _interestPPHHeader.ID), InterestPPHHeader)
                        Me._interestPPHHeader.MarkLoaded()

                    End If

                    Return Me._interestPPHHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As InterestPPHHeader)

                Me._interestPPHHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._interestPPHHeader.MarkLoaded()
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
