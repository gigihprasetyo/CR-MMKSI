
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : WSCDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2014 
'// ---------------------
'// $History      : $
'// Generated on 2/12/2014 - 9:36:45 AM
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
    <Serializable(), TableInfo("WSCDetail")> _
    Public Class WSCDetail
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
        Private _wSCType As String = String.Empty
        Private _quantity As Decimal
        Private _partPrice As Decimal
        Private _mainPart As Short
        Private _quantityReceived As Decimal
        Private _receivedBy As String = String.Empty
        Private _receivedDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _positionCode As String
        Private _workCode As String
        Private _fakturNumber As String

        Private _laborMaster As LaborMaster
        Private _wSCHeader As WSCHeader
        Private _sparePartMaster As SparePartMaster

        Private _status As Short


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


        <ColumnInfo("WSCType", "'{0}'")> _
        Public Property WSCType() As String
            Get
                Return _wSCType
            End Get
            Set(ByVal value As String)
                _wSCType = value
            End Set
        End Property


        <ColumnInfo("Quantity", "{0}")> _
        Public Property Quantity() As Decimal
            Get
                Return _quantity
            End Get
            Set(ByVal value As Decimal)
                _quantity = value
            End Set
        End Property


        <ColumnInfo("PartPrice", "{0}")> _
        Public Property PartPrice() As Decimal
            Get
                Return _partPrice
            End Get
            Set(ByVal value As Decimal)
                _partPrice = value
            End Set
        End Property


        <ColumnInfo("MainPart", "{0}")> _
        Public Property MainPart() As Short
            Get
                Return _mainPart
            End Get
            Set(ByVal value As Short)
                _mainPart = value
            End Set
        End Property


        <ColumnInfo("QuantityReceived", "{0}")> _
        Public Property QuantityReceived() As Decimal
            Get
                Return _quantityReceived
            End Get
            Set(ByVal value As Decimal)
                _quantityReceived = value
            End Set
        End Property


        <ColumnInfo("ReceivedBy", "'{0}'")> _
        Public Property ReceivedBy() As String
            Get
                Return _receivedBy
            End Get
            Set(ByVal value As String)
                _receivedBy = value
            End Set
        End Property


        <ColumnInfo("ReceivedDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ReceivedDate() As DateTime
            Get
                Return _receivedDate
            End Get
            Set(ByVal value As DateTime)
                _receivedDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("PositionCode", "'{0}'")> _
        Public Property PositionCode() As String
            Get
                Return _positionCode
            End Get
            Set(ByVal value As String)
                _positionCode = value
            End Set
        End Property

        <ColumnInfo("WorkCode", "'{0}'")> _
        Public Property WorkCode() As String
            Get
                Return _workCode
            End Get
            Set(ByVal value As String)
                _workCode = value
            End Set
        End Property

        <ColumnInfo("FakturNumber", "'{0}'")> _
        Public Property FakturNumber() As String
            Get
                Return _fakturNumber
            End Get
            Set(ByVal value As String)
                _fakturNumber = value
            End Set
        End Property

        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
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


        <ColumnInfo("LaborMasterID", "{0}"), _
        RelationInfo("LaborMaster", "ID", "WSCDetail", "LaborMasterID")> _
        Public Property LaborMaster() As LaborMaster
            Get
                Try
                    If Not IsNothing(Me._laborMaster) AndAlso (Not Me._laborMaster.IsLoaded) Then

                        Me._laborMaster = CType(DoLoad(GetType(LaborMaster).ToString(), _laborMaster.ID), LaborMaster)
                        Me._laborMaster.MarkLoaded()

                    End If

                    Return Me._laborMaster

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As LaborMaster)

                Me._laborMaster = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._laborMaster.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("WSCHeaderID", "{0}"), _
        RelationInfo("WSCHeader", "ID", "WSCDetail", "WSCHeaderID")> _
        Public Property WSCHeader() As WSCHeader
            Get
                Try
                    If Not IsNothing(Me._wSCHeader) AndAlso (Not Me._wSCHeader.IsLoaded) Then

                        Me._wSCHeader = CType(DoLoad(GetType(WSCHeader).ToString(), _wSCHeader.ID), WSCHeader)
                        Me._wSCHeader.MarkLoaded()

                    End If

                    Return Me._wSCHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As WSCHeader)

                Me._wSCHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._wSCHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("SparePartMasterID", "{0}"), _
        RelationInfo("SparePartMaster", "ID", "WSCDetail", "SparePartMasterID")> _
        Public Property SparePartMaster() As SparePartMaster
            Get
                Try
                    If Not IsNothing(Me._sparePartMaster) AndAlso (Not Me._sparePartMaster.IsLoaded) Then

                        Me._sparePartMaster = CType(DoLoad(GetType(SparePartMaster).ToString(), _sparePartMaster.ID), SparePartMaster)
                        Me._sparePartMaster.MarkLoaded()

                    End If

                    Return Me._sparePartMaster

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SparePartMaster)

                Me._sparePartMaster = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sparePartMaster.MarkLoaded()
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
        <ColumnInfo("Type", "'{0}'")> _
               Public ReadOnly Property Type() As String
            Get
                '-- If WSC Type = Labor then return "Labor" else return "Part"
                Return IIf(WSCType = "L", "Labor", "Part")
            End Get
        End Property

        <ColumnInfo("Code", "'{0}'")> _
        Public ReadOnly Property Code() As String
            Get
                '-- If WSC Type = Labor then return Labor Code else return Part Code
                Dim LaborCode As String = ""
                If Not IsNothing(LaborMaster) Then LaborCode = LaborMaster.LaborCode
                Dim PartNumber As String = ""
                If Not IsNothing(SparePartMaster) Then PartNumber = SparePartMaster.PartNumber
                Return IIf(WSCType = "L", LaborCode, PartNumber)
            End Get
        End Property

        <ColumnInfo("WorkQty", "'{0}'")> _
        Public ReadOnly Property WorkQty() As String
            Get
                '-- If WSC Type = Labor then return Work Code else return Quantity
                Dim WorkCode As String = ""
                If Not IsNothing(LaborMaster) Then WorkCode = LaborMaster.WorkCode
                Return IIf(WSCType = "L", WorkCode, Format(Quantity, "#,##0.0"))
            End Get
        End Property

        <ColumnInfo("UnitPrice", "'{0}'")> _
        Public ReadOnly Property UnitPrice() As String
            Get
                '-- If WSC Type = Labor then return Quantity else return Part Price
                Return IIf(WSCType = "L", Format(Quantity, "#,##0"), Format(PartPrice, "#,##0"))
            End Get
        End Property
#End Region

    End Class
End Namespace

