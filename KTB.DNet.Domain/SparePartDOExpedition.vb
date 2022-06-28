
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartDOExpedition Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 9/27/2016 - 11:35:55 AM
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
    <Serializable(), TableInfo("SparePartDOExpedition")> _
    Public Class SparePartDOExpedition
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
        Private _expeditionNo As String = String.Empty
        Private _expeditionName As String = String.Empty
        Private _eTA As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _eTD As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _aTD As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _aTA As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _sparePartPackings As System.Collections.ArrayList = New System.Collections.ArrayList()


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


        <ColumnInfo("ExpeditionNo", "'{0}'")> _
        Public Property ExpeditionNo As String
            Get
                Return _expeditionNo
            End Get
            Set(ByVal value As String)
                _expeditionNo = value
            End Set
        End Property


        <ColumnInfo("ExpeditionName", "'{0}'")> _
        Public Property ExpeditionName As String
            Get
                Return _expeditionName
            End Get
            Set(ByVal value As String)
                _expeditionName = value
            End Set
        End Property


        <ColumnInfo("ETA", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ETA As DateTime
            Get
                Return _eTA
            End Get
            Set(ByVal value As DateTime)
                _eTA = value
            End Set
        End Property


        <ColumnInfo("ETD", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ETD As DateTime
            Get
                Return _eTD
            End Get
            Set(ByVal value As DateTime)
                _eTD = value
            End Set
        End Property


        <ColumnInfo("ATD", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ATD As DateTime
            Get
                Return _aTD
            End Get
            Set(ByVal value As DateTime)
                _aTD = value
            End Set
        End Property


        <ColumnInfo("ATA", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ATA As DateTime
            Get
                Return _aTA
            End Get
            Set(ByVal value As DateTime)
                _aTA = value
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


        <RelationInfo("SparePartDO", "ID", "SparePartDODetail", "SparePartDOID")> _
        Public ReadOnly Property SparePartPackings As System.Collections.ArrayList
            Get
                Try
                    If (Me._sparePartPackings.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SparePartPacking), "SparePartDOExpedition.ID", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SparePartPacking), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._sparePartPackings = DoLoadArray(GetType(SparePartPacking).ToString, criterias)
                    End If

                    Return Me._sparePartPackings

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

