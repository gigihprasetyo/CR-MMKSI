#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DiscountProposalDetailApprovaltoSPL Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 7/24/2020 - 11:01:44 AM
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
    <Serializable(), TableInfo("DiscountProposalDetailApprovaltoSPL")> _
    Public Class DiscountProposalDetailApprovaltoSPL
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
        Private _discountProposed As Decimal
        Private _discountApproved As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _numberRow As Integer
        Private _vechileTypeID As String
        Private _modelID As Integer
        Private _totalDiscount As Decimal
        Private _labelTotal As String

        Private _discountMaster As DiscountMaster
        Private _discountProposalDetailApproval As DiscountProposalDetailApproval
        Private _sPLDetail As SPLDetail



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


        <ColumnInfo("DiscountProposed", "{0}")> _
        Public Property DiscountProposed As Decimal
            Get
                Return _discountProposed
            End Get
            Set(ByVal value As Decimal)
                _discountProposed = value
            End Set
        End Property


        <ColumnInfo("DiscountApproved", "{0}")> _
        Public Property DiscountApproved As Decimal
            Get
                Return _discountApproved
            End Get
            Set(ByVal value As Decimal)
                _discountApproved = value
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


        <ColumnInfo("DiscountMasterID", "{0}"), _
        RelationInfo("DiscountMaster", "ID", "DiscountProposalDetailApprovaltoSPL", "DiscountMasterID")> _
        Public Property DiscountMaster As DiscountMaster
            Get
                Try
                    If Not isnothing(Me._discountMaster) AndAlso (Not Me._discountMaster.IsLoaded) Then

                        Me._discountMaster = CType(DoLoad(GetType(DiscountMaster).ToString(), _discountMaster.ID), DiscountMaster)
                        Me._discountMaster.MarkLoaded()

                    End If

                    Return Me._discountMaster

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DiscountMaster)

                Me._discountMaster = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._discountMaster.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DiscountProposalDetailApprovalID", "{0}"), _
        RelationInfo("DiscountProposalDetailApproval", "ID", "DiscountProposalDetailApprovaltoSPL", "DiscountProposalDetailApprovalID")> _
        Public Property DiscountProposalDetailApproval As DiscountProposalDetailApproval
            Get
                Try
                    If Not isnothing(Me._discountProposalDetailApproval) AndAlso (Not Me._discountProposalDetailApproval.IsLoaded) Then
                        Try
                            Me._discountProposalDetailApproval = CType(DoLoad(GetType(DiscountProposalDetailApproval).ToString(), _discountProposalDetailApproval.ID), DiscountProposalDetailApproval)
                            Me._discountProposalDetailApproval.MarkLoaded()
                        Catch
                        End Try
                    End If

                    Return Me._discountProposalDetailApproval

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DiscountProposalDetailApproval)

                Me._discountProposalDetailApproval = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._discountProposalDetailApproval.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SPLDetailID", "{0}"), _
        RelationInfo("SPLDetail", "ID", "DiscountProposalDetailApprovaltoSPL", "SPLDetailID")> _
        Public Property SPLDetail As SPLDetail
            Get
                Try
                    If Not isnothing(Me._sPLDetail) AndAlso (Not Me._sPLDetail.IsLoaded) Then

                        Me._sPLDetail = CType(DoLoad(GetType(SPLDetail).ToString(), _sPLDetail.ID), SPLDetail)
                        Me._sPLDetail.MarkLoaded()

                    End If

                    Return Me._sPLDetail

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SPLDetail)

                Me._sPLDetail = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sPLDetail.MarkLoaded()
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
        Public Property NumberRow As Integer
            Get
                Return _numberRow
            End Get
            Set(ByVal value As Integer)
                _numberRow = value
            End Set
        End Property

        Public Property VechileTypeID As String
            Get
                If _vechileTypeID.ToString() = "" OrElse _vechileTypeID.ToString() = "0" Then
                    If Not IsNothing(Me.DiscountProposalDetailApproval) AndAlso Me.DiscountProposalDetailApproval.ID > 0 Then
                        _vechileTypeID = Me.DiscountProposalDetailApproval.VechileType.ID
                    End If
                End If
                Return _vechileTypeID
            End Get
            Set(ByVal value As String)
                _vechileTypeID = value
            End Set
        End Property

        Public Property ModelID As Integer
            Get
                If Not IsNothing(Me.DiscountProposalDetailApproval) AndAlso Me.DiscountProposalDetailApproval.ID > 0 Then
                    Dim objSubCategoryVehicleToModel As New SubCategoryVehicleToModel
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicleToModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(SubCategoryVehicleToModel), "VechileModel.ID", MatchType.Exact, Me.DiscountProposalDetailApproval.VechileType.VechileModel.ID))
                    Dim strSQL As String = "Select ID From SubCategoryVehicle where CategoryID in (1,2) and Status = ''"
                    criterias.opAnd(New Criteria(GetType(SubCategoryVehicleToModel), "SubCategoryVehicle.ID", MatchType.InSet, "(" & strSQL & ")"))
                    Dim arrSCVToModel As ArrayList = DoLoadArray(GetType(SubCategoryVehicleToModel).ToString, criterias)
                    If Not IsNothing(arrSCVToModel) AndAlso arrSCVToModel.Count > 0 Then
                        objSubCategoryVehicleToModel = CType(arrSCVToModel(0), SubCategoryVehicleToModel)
                        _modelID = objSubCategoryVehicleToModel.SubCategoryVehicle.ID
                    End If
                End If
                Return _modelID
            End Get
            Set(ByVal value As Integer)
                _modelID = value
            End Set
        End Property

        Public Property TotalDiscount As Decimal
            Get
                Return _totalDiscount
            End Get
            Set(ByVal value As Decimal)
                _totalDiscount = value
            End Set
        End Property

        Public Property LabelTotal As String
            Get
                Return _labelTotal
            End Get
            Set(ByVal value As String)
                _labelTotal = value
            End Set
        End Property

#End Region

    End Class
End Namespace
