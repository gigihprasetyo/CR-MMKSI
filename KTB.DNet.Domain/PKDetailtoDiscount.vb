#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PKDetailtoDiscount Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 8/23/2020 - 10:48:12 AM
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
    <Serializable(), TableInfo("PKDetailtoDiscount")> _
    Public Class PKDetailtoDiscount
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
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _pKDetail As PKDetail
        Private _pKDetailtoSPL As SPLDetailtoSPL
        Private _modelID As Integer
        Private _totalDiscount As Decimal
        Private _discount As Decimal
        Private _labelTotal As String
        Private _vechileTypeID As Integer
        Private _numberRow As Short

        Private _periodMonth As Integer
        Private _periodYear As Integer
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


        <ColumnInfo("Discount", "{0}")> _
        Public Property Discount As Decimal
            Get
                Return _discount
            End Get
            Set(ByVal value As Decimal)
                _discount = value
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


        <ColumnInfo("PKDetailID", "{0}"), _
        RelationInfo("PKDetail", "ID", "PKDetailtoDiscount", "PKDetailID")> _
        Public Property PKDetail As PKDetail
            Get
                Try
                    If Not IsNothing(Me._pKDetail) AndAlso (Not Me._pKDetail.IsLoaded) Then
                        If _pKDetail.ID > 0 Then
                            Me._pKDetail = CType(DoLoad(GetType(PKDetail).ToString(), _pKDetail.ID), PKDetail)
                            Me._pKDetail.MarkLoaded()
                        End If
                    End If

                    Return Me._pKDetail

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As PKDetail)

                Me._pKDetail = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._pKDetail.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SPLDetailtoSPLID", "{0}"), _
        RelationInfo("SPLDetailtoSPL", "ID", "PKDetailtoDiscount", "SPLDetailtoSPLID")> _
        Public Property SPLDetailtoSPL As SPLDetailtoSPL
            Get
                Try
                    If Not IsNothing(Me._pKDetailtoSPL) AndAlso (Not Me._pKDetailtoSPL.IsLoaded) Then

                        Me._pKDetailtoSPL = CType(DoLoad(GetType(SPLDetailtoSPL).ToString(), _pKDetailtoSPL.ID), SPLDetailtoSPL)
                        Me._pKDetailtoSPL.MarkLoaded()

                    End If

                    Return Me._pKDetailtoSPL

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SPLDetailtoSPL)

                Me._pKDetailtoSPL = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._pKDetailtoSPL.MarkLoaded()
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
        Public Property NumberRow As Short
            Get
                Return _numberRow
            End Get
            Set(ByVal value As Short)
                _numberRow = value
            End Set
        End Property

        Public Property VechileTypeID As String
            Get
                If Not IsNothing(Me.PKDetail) AndAlso Me.PKDetail.ID > 0 Then
                    _vechileTypeID = Me.PKDetail.VechileType.ID
                End If
                Return _vechileTypeID
            End Get
            Set(ByVal value As String)
                _vechileTypeID = value
            End Set
        End Property

        Public Property ModelID As Integer
            Get
                If Not IsNothing(Me.PKDetail) AndAlso Me.PKDetail.ID > 0 Then
                    Dim objSubCategoryVehicleToModel As New SubCategoryVehicleToModel
                    Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(SubCategoryVehicleToModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                    criterias.opAnd(New Criteria(GetType(SubCategoryVehicleToModel), "VechileModel.ID", MatchType.Exact, Me.PKDetail.VechileType.VechileModel.ID))
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
