
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DepositBInterestDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 3/14/2016 - 11:12:38 AM
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
    <Serializable(), TableInfo("DepositBInterestDetail")> _
    Public Class DepositBInterestDetail
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
        Private _month As String = String.Empty
        Private _year As Short
        Private _interestAmount As Decimal
        Private _nettoAmount As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _depositBInterestHeader As DepositBInterestHeader



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


        <ColumnInfo("Month", "'{0}'")> _
        Public Property Month As String
            Get
                Return _month
            End Get
            Set(ByVal value As String)
                _month = value
            End Set
        End Property


        <ColumnInfo("Year", "{0}")> _
        Public Property Year As Short
            Get
                Return _year
            End Get
            Set(ByVal value As Short)
                _year = value
            End Set
        End Property


        <ColumnInfo("InterestAmount", "{0}")> _
        Public Property InterestAmount As Decimal
            Get
                Return _interestAmount
            End Get
            Set(ByVal value As Decimal)
                _interestAmount = value
            End Set
        End Property


        <ColumnInfo("NettoAmount", "{0}")> _
        Public Property NettoAmount As Decimal
            Get
                Return _nettoAmount
            End Get
            Set(ByVal value As Decimal)
                _nettoAmount = value
            End Set
        End Property


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


        <ColumnInfo("HeaderID", "{0}"), _
        RelationInfo("DepositBInterestHeader", "ID", "DepositBInterestDetail", "HeaderID")> _
        Public Property DepositBInterestHeader As DepositBInterestHeader
            Get
                Try
                    If Not isnothing(Me._depositBInterestHeader) AndAlso (Not Me._depositBInterestHeader.IsLoaded) Then

                        Me._depositBInterestHeader = CType(DoLoad(GetType(DepositBInterestHeader).ToString(), _depositBInterestHeader.ID), DepositBInterestHeader)
                        Me._depositBInterestHeader.MarkLoaded()

                    End If

                    Return Me._depositBInterestHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DepositBInterestHeader)

                Me._depositBInterestHeader = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._depositBInterestHeader.MarkLoaded()
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

