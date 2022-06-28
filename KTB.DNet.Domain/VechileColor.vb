#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Noviar Sbastian
'// PURPOSE       : VechileColor Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 9/29/2005 - 2:53:27 PM
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
Imports KTB.DNet.DataMapper.Framework
#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("VechileColor")> _
    Public Class VechileColor
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
        Private _colorCode As String = String.Empty
        Private _colorIndName As String = String.Empty
        Private _colorEngName As String = String.Empty
        Private _materialNumber As String = String.Empty
        Private _materialDescription As String = String.Empty
        Private _headerBOM As String = String.Empty
        Private _marketCode As String = String.Empty
        Private _specialFlag As String = String.Empty
        Private _status As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _vechileType As VechileType
        Private _price As Price


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


        <ColumnInfo("ColorCode", "'{0}'")> _
        Public Property ColorCode() As String
            Get
                Return _colorCode
            End Get
            Set(ByVal value As String)
                _colorCode = value
            End Set
        End Property


        <ColumnInfo("ColorIndName", "'{0}'")> _
        Public Property ColorIndName() As String
            Get
                Return _colorIndName
            End Get
            Set(ByVal value As String)
                _colorIndName = value
            End Set
        End Property


        <ColumnInfo("ColorEngName", "'{0}'")> _
        Public Property ColorEngName() As String
            Get
                Return _colorEngName
            End Get
            Set(ByVal value As String)
                _colorEngName = value
            End Set
        End Property


        <ColumnInfo("MaterialNumber", "'{0}'")> _
        Public Property MaterialNumber() As String
            Get
                Return _materialNumber
            End Get
            Set(ByVal value As String)
                _materialNumber = value
            End Set
        End Property


        <ColumnInfo("MaterialDescription", "'{0}'")> _
        Public Property MaterialDescription() As String
            Get
                Return _materialDescription
            End Get
            Set(ByVal value As String)
                _materialDescription = value
            End Set
        End Property


        <ColumnInfo("HeaderBOM", "'{0}'")> _
        Public Property HeaderBOM() As String
            Get
                Return _headerBOM
            End Get
            Set(ByVal value As String)
                _headerBOM = value
            End Set
        End Property


        <ColumnInfo("MarketCode", "'{0}'")> _
        Public Property MarketCode() As String
            Get
                Return _marketCode
            End Get
            Set(ByVal value As String)
                _marketCode = value
            End Set
        End Property


        <ColumnInfo("SpecialFlag", "'{0}'")> _
        Public Property SpecialFlag() As String
            Get
                Return _specialFlag
            End Get
            Set(ByVal value As String)
                _specialFlag = value
            End Set
        End Property


        <ColumnInfo("Status", "'{0}'")> _
        Public Property Status() As String
            Get
                Return _status
            End Get
            Set(ByVal value As String)
                _status = value
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





        <ColumnInfo("VechileTypeID", "{0}"), _
        RelationInfo("VechileType", "ID", "VechileColor", "VechileTypeID")> _
        Public Property VechileType() As VechileType
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




#End Region

#Region "Custom Properties"

        <ColumnInfo("RowStatusX", "'{0}'")> _
        Public ReadOnly Property RowStatusX() As String
            Get
                Return IIf(_rowStatus = DBRowStatus.Deleted, "X", "")
            End Get
        End Property

        Public ReadOnly Property Price(ByVal CheckingDate As Date) As Price
            Get
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Price), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim srtPrice As SortCollection = New SortCollection
                Dim m_PriceMapper As IMapper
                Dim PriceColl As ArrayList

                m_PriceMapper = MapperFactory.GetInstance.GetMapper(GetType(Price).ToString)
                criterias.opAnd(New Criteria(GetType(Price), "VechileColor.ID", MatchType.Exact, Me.ID))
                criterias.opAnd(New Criteria(GetType(Price), "ValidFrom", MatchType.LesserOrEqual, CheckingDate))
                srtPrice.Add(New Sort(GetType(Price), "ValidFrom", Sort.SortDirection.DESC))

                PriceColl = m_PriceMapper.RetrieveByCriteria(criterias, srtPrice)
                If (PriceColl.Count > 0) Then
                    _price = CType(PriceColl(0), Price)
                Else
                    _price = Nothing
                End If
                Return _price
            End Get
        End Property


        '' CR Sirkular Rewards
        '' by : ali Akbar
        '' 2014-09-24
        Public ReadOnly Property Price(ByVal ObjContractHeader As ContractHeader) As Price
            Get
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Price), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                Dim srtPrice As SortCollection = New SortCollection
                Dim m_PriceMapper As IMapper
                Dim PriceColl As ArrayList
                Dim ValidFrom As Date = New DateTime(ObjContractHeader.PricePeriodYear, ObjContractHeader.PricePeriodMonth, ObjContractHeader.PricePeriodDay)

                m_PriceMapper = MapperFactory.GetInstance.GetMapper(GetType(Price).ToString)
                criterias.opAnd(New Criteria(GetType(Price), "VechileColor.ID", MatchType.Exact, Me.ID))
                criterias.opAnd(New Criteria(GetType(Price), "ValidFrom", MatchType.LesserOrEqual, ValidFrom))
                criterias.opAnd(New Criteria(GetType(Price), "Dealer.ID", MatchType.Exact, ObjContractHeader.Dealer.ID))
                srtPrice.Add(New Sort(GetType(Price), "ValidFrom", Sort.SortDirection.DESC))

                PriceColl = m_PriceMapper.RetrieveByCriteria(criterias, srtPrice)
                If (PriceColl.Count > 0) Then
                    _price = CType(PriceColl(0), Price)
                Else
                    _price = Nothing
                End If
                Return _price
            End Get
        End Property
        '' END OF CR Sirkular Rewards


#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
