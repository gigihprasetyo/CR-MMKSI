#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EquipmentMaster Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/08/2005 - 1:30:57 PM
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
    <Serializable(), TableInfo("EquipmentMaster")> _
    Public Class EquipmentMaster
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
        Private _kind As Integer
        Private _equipmentNumber As String = String.Empty
        Private _description As String = String.Empty
        Private _specification As String = String.Empty
        Private _status As Integer
        Private _price As Decimal
        Private _photoFileName As String = String.Empty
        Private _photoPath As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

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


        <ColumnInfo("Kind", "{0}")> _
        Public Property Kind() As Integer
            Get
                Return _kind
            End Get
            Set(ByVal value As Integer)
                _kind = value
            End Set
        End Property


        <ColumnInfo("EquipmentNumber", "'{0}'")> _
        Public Property EquipmentNumber() As String
            Get
                Return _equipmentNumber
            End Get
            Set(ByVal value As String)
                _equipmentNumber = value
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


        <ColumnInfo("Specification", "'{0}'")> _
        Public Property Specification() As String
            Get
                Return _specification
            End Get
            Set(ByVal value As String)
                _specification = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Integer
            Get
                Return _status
            End Get
            Set(ByVal value As Integer)
                _status = value
            End Set
        End Property


        <ColumnInfo("Price", "{0}")> _
        Public Property Price() As Decimal
            Get
                Return _price
            End Get
            Set(ByVal value As Decimal)
                _price = value
            End Set
        End Property


        <ColumnInfo("PhotoFileName", "'{0}'")> _
        Public Property PhotoFileName() As String
            Get
                Return _photoFileName
            End Get
            Set(ByVal value As String)
                _photoFileName = value
            End Set
        End Property


        <ColumnInfo("PhotoPath", "'{0}'")> _
        Public Property PhotoPath() As String
            Get
                Return _photoPath
            End Get
            Set(ByVal value As String)
                _photoPath = value
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


#End Region

#Region "Custom Method"

        Public ReadOnly Property TotalQty() As Integer
            Get
                Dim _total As Integer
                Dim m_HeaderBOMMapper As IMapper

                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(HeaderBOM), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(HeaderBOM), "EquipmentMaster.ID", MatchType.Exact, Me.ID))

                m_HeaderBOMMapper = MapperFactory.GetInstance.GetMapper(GetType(HeaderBOM).ToString)
                Dim HeaderBOMColl As ArrayList = m_HeaderBOMMapper.RetrieveByCriteria(criterias)

                If (HeaderBOMColl.Count > 0) Then
                    For Each objHeaderBOM As HeaderBOM In HeaderBOMColl
                        For Each item As DetailBOM In objHeaderBOM.DetailBOMs
                            _total += item.Quantity
                        Next
                    Next
                End If

                Return _total
            End Get
        End Property

#End Region

    End Class
End Namespace

