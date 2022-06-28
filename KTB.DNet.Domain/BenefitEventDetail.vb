
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BenefitEventDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 12/2/2015 - 10:57:45 AM
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
    <Serializable(), TableInfo("BenefitEventDetail")> _
    Public Class BenefitEventDetail
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
        Private _status As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _benefitEventHeader As BenefitEventHeader
        Private _benefitParticipant As BenefitParticipant



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


        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
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


        <ColumnInfo("BenefitEventHeaderID", "{0}"), _
        RelationInfo("BenefitEventHeader", "ID", "BenefitEventDetail", "BenefitEventHeaderID")> _
        Public Property BenefitEventHeader As BenefitEventHeader
            Get
                Try
                    'If Not IsNothing(Me._benefitEventHeader) AndAlso (Not Me._benefitEventHeader.IsLoaded) AndAlso (_benefitEventHeader.ID > 0) Then
                    If Not IsNothing(Me._benefitEventHeader) AndAlso (Not Me._benefitEventHeader.IsLoaded) Then

                        If _benefitEventHeader.ID > 0 Then
                            Me._benefitEventHeader = CType(DoLoad(GetType(BenefitEventHeader).ToString(), _benefitEventHeader.ID), BenefitEventHeader)
                        Else
                            Me._benefitEventHeader = _benefitEventHeader
                        End If
                        'Me._benefitEventHeader = CType(DoLoad(GetType(BenefitEventHeader).ToString(), _benefitEventHeader.ID), BenefitEventHeader)
                        'Me._benefitEventHeader = _benefitEventHeader
                        Me._benefitEventHeader.MarkLoaded()

                    End If

                    Return Me._benefitEventHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As BenefitEventHeader)

                Me._benefitEventHeader = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._benefitEventHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("BenefitParticipantID", "{0}"), _
        RelationInfo("BenefitParticipant", "ID", "BenefitEventDetail", "BenefitParticipantID")> _
        Public Property BenefitParticipant As BenefitParticipant
            Get
                Try
                    ' If Not IsNothing(Me._benefitParticipant) AndAlso (Not Me._benefitParticipant.IsLoaded) AndAlso (_benefitParticipant.ID > 0) Then
                    If Not IsNothing(Me._benefitParticipant) AndAlso (Not Me._benefitParticipant.IsLoaded) Then

                        If _benefitParticipant.ID > 0 Then
                            Me._benefitParticipant = CType(DoLoad(GetType(BenefitParticipant).ToString(), _benefitParticipant.ID), BenefitParticipant)
                        Else
                            Me._benefitParticipant = _benefitParticipant
                        End If

                        'Me._benefitParticipant = CType(DoLoad(GetType(BenefitParticipant).ToString(), _benefitParticipant.ID), BenefitParticipant)
                        'Me._benefitParticipant = _benefitParticipant
                        Me._benefitParticipant.MarkLoaded()

                    End If

                    Return Me._benefitParticipant

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If


                End Try

                Return Nothing
            End Get

            Set(ByVal value As BenefitParticipant)

                Me._benefitParticipant = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._benefitParticipant.MarkLoaded()
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

