#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrTraineePaymentDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 8/26/2019 - 1:05:13 PM
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
    <Serializable(), TableInfo("TrTraineePaymentDetail")> _
    Public Class TrTraineePaymentDetail
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
        Private _trBillingHeader As TrBillingHeader
        Private _trTraineePaymentHeader As TrTraineePaymentHeader
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)




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


        <ColumnInfo("TrBillingHeaderID", "{0}")> _
        <RelationInfo("TrBillingHeader", "ID", "TrTraineePaymentDetail", "TrBillingHeaderID")> _
        Public Property TrBillingHeader As TrBillingHeader
            Get
                Try
                    If Not IsNothing(Me._trBillingHeader) AndAlso (Not Me._trBillingHeader.IsLoaded) Then

                        Me._trBillingHeader = CType(DoLoad(GetType(TrBillingHeader).ToString(), _trBillingHeader.ID), TrBillingHeader)
                        Me._trBillingHeader.MarkLoaded()

                    End If

                    Return Me._trBillingHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TrBillingHeader)

                Me._trBillingHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._trBillingHeader.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("TrTraineePaymentHeaderID", "{0}")> _
        <RelationInfo("TrTraineePaymentHeader", "ID", "TrTraineePaymentDetail", "TrTraineePaymentHeaderID")> _
        Public Property TrTraineePaymentHeader As TrTraineePaymentHeader
            Get
                Try
                    If Not IsNothing(Me._trTraineePaymentHeader) AndAlso (Not Me._trTraineePaymentHeader.IsLoaded) Then

                        Me._trTraineePaymentHeader = CType(DoLoad(GetType(TrTraineePaymentHeader).ToString(), _trTraineePaymentHeader.ID), TrTraineePaymentHeader)
                        Me._trTraineePaymentHeader.MarkLoaded()

                    End If

                    Return Me._trTraineePaymentHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TrTraineePaymentHeader)

                Me._trTraineePaymentHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._trTraineePaymentHeader.MarkLoaded()
                End If
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
