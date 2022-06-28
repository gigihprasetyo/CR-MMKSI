#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_LeasingDaftarKondisi Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 8/14/2009 - 10:11:53 AM
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
    <Serializable(), TableInfo("V_LeasingDaftarKondisi")> _
    Public Class V_LeasingDaftarKondisi
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
        Private _documentType As Short
        Private _vechileTypeID As Short
        Private _validFrom As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _validTo As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MaxValue.Value, DateTime)
        Private _basePrice As Decimal
        Private _retailPrice As Decimal
        Private _subsidi As Decimal
        Private _pPh As Decimal
        Private _afterPPh As Decimal
        Private _pPn As Decimal
        Private _sPAF As Decimal
        Private _assistFee As Decimal
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _pPhPercent As Decimal




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


        <ColumnInfo("DocumentType", "{0}")> _
        Public Property DocumentType() As Short
            Get
                Return _documentType
            End Get
            Set(ByVal value As Short)
                _documentType = value
            End Set
        End Property


        <ColumnInfo("VechileTypeID", "{0}")> _
        Public Property VechileTypeID() As Short
            Get
                Return _vechileTypeID
            End Get
            Set(ByVal value As Short)
                _vechileTypeID = value
            End Set
        End Property


        <ColumnInfo("ValidFrom", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ValidFrom() As DateTime
            Get
                Return _validFrom
            End Get
            Set(ByVal value As DateTime)
                _validFrom = value
            End Set
        End Property

        <ColumnInfo("ValidTo", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
       Public Property ValidTo() As DateTime
            Get
                Return _validTo
            End Get
            Set(ByVal value As DateTime)
                _validTo = value
            End Set
        End Property


        <ColumnInfo("BasePrice", "{0}")> _
        Public Property BasePrice() As Decimal
            Get
                Return _basePrice
            End Get
            Set(ByVal value As Decimal)
                _basePrice = value
            End Set
        End Property


        <ColumnInfo("RetailPrice", "{0}")> _
        Public Property RetailPrice() As Decimal
            Get
                Return _retailPrice
            End Get
            Set(ByVal value As Decimal)
                _retailPrice = value
            End Set
        End Property


        <ColumnInfo("Subsidi", "{0}")> _
        Public Property Subsidi() As Decimal
            Get
                Return _subsidi
            End Get
            Set(ByVal value As Decimal)
                _subsidi = value
            End Set
        End Property


        <ColumnInfo("PPh", "#,##0")> _
        Public Property PPh() As Decimal
            Get
                Return _pPh
            End Get
            Set(ByVal value As Decimal)
                _pPh = value
            End Set
        End Property


        <ColumnInfo("AfterPPh", "#,##0")> _
        Public Property AfterPPh() As Decimal
            Get
                Return _afterPPh
            End Get
            Set(ByVal value As Decimal)
                _afterPPh = value
            End Set
        End Property


        <ColumnInfo("PPn", "#,##0")> _
        Public Property PPn() As Decimal
            Get
                Return _pPn
            End Get
            Set(ByVal value As Decimal)
                _pPn = value
            End Set
        End Property


        <ColumnInfo("SPAF", "{0}")> _
        Public Property SPAF() As Decimal
            Get
                Return _sPAF
            End Get
            Set(ByVal value As Decimal)
                _sPAF = value
            End Set
        End Property


        <ColumnInfo("AssistFee", "{0}")> _
        Public Property AssistFee() As Decimal
            Get
                Return _assistFee
            End Get
            Set(ByVal value As Decimal)
                _assistFee = value
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


        <ColumnInfo("PPhPercent", "#,##0")> _
        Public Property PPhPercent() As Decimal
            Get
                Return _pPhPercent
            End Get
            Set(ByVal Value As Decimal)
                _pPhPercent = Value
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
        Private _vechileType As VechileType
        <ColumnInfo("VechileTypeID", "{0}"), _
        RelationInfo("VechileType", "ID", "V_LeasingDaftarKondisi", "VechileTypeID")> _
        Public Property VechileType() As VechileType
            Get
                Try
                    If IsNothing(Me._vechileType) Then

                        Me._vechileType = CType(DoLoad(GetType(VechileType).ToString(), Me._vechileTypeID), VechileType)
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

    End Class
End Namespace

