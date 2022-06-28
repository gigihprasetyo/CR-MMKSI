
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BusinessSectorHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 28/02/2018 - 14:11:13
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
    <Serializable(), TableInfo("BusinessSectorHeader")> _
    Public Class BusinessSectorHeader
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
        Private _businessSectorName As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)


        Private _businessSectorDetails As System.Collections.ArrayList = New System.Collections.ArrayList()
        Private _fleets As System.Collections.ArrayList = New System.Collections.ArrayList()


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


        <ColumnInfo("BusinessSectorName", "'{0}'")> _
        Public Property BusinessSectorName As String
            Get
                Return _businessSectorName
            End Get
            Set(ByVal value As String)
                _businessSectorName = value
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



        <RelationInfo("BusinessSectorHeader", "ID", "BusinessSectorDetail", "BusinessSectorHeaderID")> _
        Public ReadOnly Property BusinessSectorDetails As System.Collections.ArrayList
            Get
                Try
                    If (Me._businessSectorDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(BusinessSectorDetail), "BusinessSectorHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(BusinessSectorDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._businessSectorDetails = DoLoadArray(GetType(BusinessSectorDetail).ToString, criterias)
                    End If

                    Return Me._businessSectorDetails

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("BusinessSectorHeader", "ID", "Fleet", "BusinessSectorHeaderId")> _
        Public ReadOnly Property Fleets As System.Collections.ArrayList
            Get
                Try
                    If (Me._fleets.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(Fleet), "BusinessSectorHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(Fleet), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._fleets = DoLoadArray(GetType(Fleet).ToString, criterias)
                    End If

                    Return Me._fleets

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
