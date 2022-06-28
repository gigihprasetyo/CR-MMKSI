#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : UniformDistribution Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/16/2007 - 10:45:52 AM
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
    <Serializable(), TableInfo("UniformDistribution")> _
    Public Class UniformDistribution
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
        Private _distributionCode As String = String.Empty
        Private _uniformCode As String = String.Empty
        Private _description As String = String.Empty
        Private _standardPrice As Decimal
        Private _dealerPrice As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)


        Private _uniformValidations As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _uniformOrders As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _uniformSalesmans As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _uniformGuides As System.Collections.ArrayList = New System.Collections.ArrayList


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


        <ColumnInfo("DistributionCode", "'{0}'")> _
        Public Property DistributionCode() As String
            Get
                Return _distributionCode
            End Get
            Set(ByVal value As String)
                _distributionCode = value
            End Set
        End Property


        <ColumnInfo("UniformCode", "'{0}'")> _
        Public Property UniformCode() As String
            Get
                Return _uniformCode
            End Get
            Set(ByVal value As String)
                _uniformCode = value
            End Set
        End Property


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property


        <ColumnInfo("StandardPrice", "#,##0")> _
        Public Property StandardPrice() As Decimal
            Get
                Return _standardPrice
            End Get
            Set(ByVal value As Decimal)
                _standardPrice = value
            End Set
        End Property


        <ColumnInfo("DealerPrice", "#,##0")> _
        Public Property DealerPrice() As Decimal
            Get
                Return _dealerPrice
            End Get
            Set(ByVal value As Decimal)
                _dealerPrice = value
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



        <RelationInfo("UniformDistribution", "ID", "UniformOrder", "UniformDistributionId")> _
        Public ReadOnly Property UniformOrders() As System.Collections.ArrayList
            Get
                Try
                    If (Me._uniformOrders.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(UniformOrder), "UniformDistribution", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(UniformOrder), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._uniformOrders = DoLoadArray(GetType(UniformOrder).ToString, criterias)
                    End If

                    Return Me._uniformOrders

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("UniformDistribution", "ID", "UniformSalesman", "UniformDistributionId")> _
        Public ReadOnly Property UniformSalesmans() As System.Collections.ArrayList
            Get
                Try
                    If (Me._uniformSalesmans.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(UniformSalesman), "UniformDistribution", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(UniformSalesman), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._uniformSalesmans = DoLoadArray(GetType(UniformSalesman).ToString, criterias)
                    End If

                    Return Me._uniformSalesmans

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("UniformDistribution", "ID", "UniformGuide", "UniformDistributionId")> _
        Public ReadOnly Property UniformGuides() As System.Collections.ArrayList
            Get
                Try
                    If (Me._uniformGuides.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(UniformGuide), "UniformDistribution", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(UniformGuide), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._uniformGuides = DoLoadArray(GetType(UniformGuide).ToString, criterias)
                    End If

                    Return Me._uniformGuides

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

