#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DiscountProposalDetailApproval Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 7/24/2020 - 1:20:51 PM
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
    <Serializable(), TableInfo("DiscountProposalDetailApproval")> _
    Public Class DiscountProposalDetailApproval
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
        Private _responseQty As Integer
        Private _priceReff As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _maxTOPDay As Integer
        Private _freeIntIndicator As Short
        Private _deliveryDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _numberRow As Integer
        Private _vechileTypeID As Integer
        Private _modelID As Integer
        Private _discountProposalHeader As DiscountProposalHeader
        Private _vechileType As VechileType

        Private _discountProposalDetailApprovaltoSPLs As System.Collections.ArrayList = New System.Collections.ArrayList()


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


        <ColumnInfo("ResponseQty", "{0}")> _
        Public Property ResponseQty As Integer
            Get
                Return _responseQty
            End Get
            Set(ByVal value As Integer)
                _responseQty = value
            End Set
        End Property


        <ColumnInfo("PriceReff", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property PriceReff As DateTime
            Get
                Return _priceReff
            End Get
            Set(ByVal value As DateTime)
                _priceReff = value
            End Set
        End Property


        <ColumnInfo("MaxTOPDay", "{0}")> _
        Public Property MaxTOPDay As Integer
            Get
                Return _maxTOPDay
            End Get
            Set(ByVal value As Integer)
                _maxTOPDay = value
            End Set
        End Property


        <ColumnInfo("FreeIntIndicator", "{0}")> _
        Public Property FreeIntIndicator As Short
            Get
                Return _freeIntIndicator
            End Get
            Set(ByVal value As Short)
                _freeIntIndicator = value
            End Set
        End Property


        <ColumnInfo("DeliveryDate", "'{0:yyyy/MM/dd}'")> _
        Public Property DeliveryDate As DateTime
            Get
                Return _deliveryDate
            End Get
            Set(ByVal value As DateTime)
                _deliveryDate = New DateTime(value.Year, value.Month, value.Day)
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


        <ColumnInfo("VechileTypeID", "{0}"), _
        RelationInfo("VechileType", "ID", "DiscountProposalDetailApproval", "VechileTypeID")> _
        Public Property VechileType As VechileType
            Get
                Try
                    If Not IsNothing(Me._vechileType) AndAlso (Not Me._vechileType.IsLoaded) Then

                        Me._vechileType = CType(DoLoad(GetType(VechileType).ToString(), _vechileType.ID), VechileType)
                        Me._vechileType.MarkLoaded()

                    End If

                    Return Me._vechileType

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As VechileType)

                Me._vechileType = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._vechileType.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("DiscountProposalHeaderID", "{0}"), _
        RelationInfo("DiscountProposalHeader", "ID", "DiscountProposalDetailApproval", "DiscountProposalHeaderID")> _
        Public Property DiscountProposalHeader As DiscountProposalHeader
            Get
                Try
                    If Not IsNothing(Me._discountProposalHeader) AndAlso (Not Me._discountProposalHeader.IsLoaded) Then

                        Me._discountProposalHeader = CType(DoLoad(GetType(DiscountProposalHeader).ToString(), _discountProposalHeader.ID), DiscountProposalHeader)
                        Me._discountProposalHeader.MarkLoaded()

                    End If

                    Return Me._discountProposalHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DiscountProposalHeader)

                Me._discountProposalHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._discountProposalHeader.MarkLoaded()
                End If
            End Set
        End Property


        <RelationInfo("DiscountProposalDetailApproval", "ID", "DiscountProposalDetailApprovaltoSPL", "DiscountProposalDetailApprovalID")> _
        Public ReadOnly Property DiscountProposalDetailApprovaltoSPLs As System.Collections.ArrayList
            Get
                Try
                    If (Me._discountProposalDetailApprovaltoSPLs.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(DiscountProposalDetailApprovaltoSPL), "DiscountProposalDetailApproval", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(DiscountProposalDetailApprovaltoSPL), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._discountProposalDetailApprovaltoSPLs = DoLoadArray(GetType(DiscountProposalDetailApprovaltoSPL).ToString, criterias)
                    End If

                    Return Me._discountProposalDetailApprovaltoSPLs

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
        Public Property NumberRow As Integer
            Get
                Return _numberRow
            End Get
            Set(ByVal value As Integer)
                _numberRow = value
            End Set
        End Property

        Public Property VechileTypeID As Integer
            Get
                If Not IsNothing(Me.VechileType) Then
                    _vechileTypeID = Me.VechileType.ID
                End If
                Return _vechileTypeID
            End Get
            Set(ByVal value As Integer)
                _vechileTypeID = value
            End Set
        End Property

        Public Property ModelID As Integer
            Get
                If Not IsNothing(Me.VechileType) Then
                    Dim objSubCategoryVehicleToModel As New SubCategoryVehicleToModel
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicleToModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(SubCategoryVehicleToModel), "VechileModel.ID", MatchType.Exact, Me.VechileType.VechileModel.ID))
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
#End Region

    End Class
End Namespace
