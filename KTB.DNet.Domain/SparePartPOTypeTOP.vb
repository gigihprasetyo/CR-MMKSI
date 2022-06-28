
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartPOTypeTOP Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 19/09/2018 - 9:38:15
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
    <Serializable(), TableInfo("SparePartPOTypeTOP")> _
    Public Class SparePartPOTypeTOP
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
        Private _sparePartPOType As String = String.Empty
        Private _isTOP As Boolean
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _sparePartMasterTOPs As System.Collections.ArrayList = New System.Collections.ArrayList()
        Private _termOfPaymentNotTOP As TermOfPayment


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


        <ColumnInfo("SparePartPOType", "'{0}'")> _
        Public Property SparePartPOType As String
            Get
                Return _sparePartPOType
            End Get
            Set(ByVal value As String)
                _sparePartPOType = value
            End Set
        End Property


        <ColumnInfo("IsTOP", "{0}")> _
        Public Property IsTOP As Boolean
            Get
                Return _isTOP
            End Get
            Set(ByVal value As Boolean)
                _isTOP = value
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

        <ColumnInfo("TermOfPaymentIDNotTOP", "{0}"), _
        RelationInfo("TermOfPayment", "ID", "SparePartPOTypeTOP", "TermOfPaymentIDNotTOP")> _
        Public Property TermOfPaymentIDNotTOP As TermOfPayment
            Get
                Try
                    If Not IsNothing(Me._termOfPaymentNotTOP) AndAlso (Not Me._termOfPaymentNotTOP.IsLoaded) Then

                        Me._termOfPaymentNotTOP = CType(DoLoad(GetType(TermOfPayment).ToString(), _termOfPaymentNotTOP.ID), TermOfPayment)
                        Me._termOfPaymentNotTOP.MarkLoaded()

                    End If

                    Return Me._termOfPaymentNotTOP

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As TermOfPayment)

                Me._termOfPaymentNotTOP = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._termOfPaymentNotTOP.MarkLoaded()
                End If
            End Set
        End Property

        <RelationInfo("SparePartPOTypeTOP", "ID", "SparePartMasterTOP", "SparePartPOTypeTOPID")> _
        Public ReadOnly Property SparePartMasterTOPs As System.Collections.ArrayList
            Get
                Try
                    If (Me._sparePartMasterTOPs.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SparePartMasterTOP), "SparePartPOTypeTOP", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SparePartMasterTOP), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._sparePartMasterTOPs = DoLoadArray(GetType(SparePartMasterTOP).ToString, criterias)
                    End If

                    Return Me._sparePartMasterTOPs

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

