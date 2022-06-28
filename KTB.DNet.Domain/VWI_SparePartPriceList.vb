
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : SparePartPriceList Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 28/03/2018 - 11:47:09
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
    <Serializable(), TableInfo("VWI_SparePartPriceList")> _
    Public Class VWI_SparePartPriceList
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
        Private _partNumber As String = String.Empty
        Private _partName As String = String.Empty
        Private _uoM As String = String.Empty
        Private _retailPrice As Decimal
        Private _activeStatus As Short
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)




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


        <ColumnInfo("PartNumber", "'{0}'")> _
        Public Property PartNumber As String
            Get
                Return _partNumber
            End Get
            Set(ByVal value As String)
                _partNumber = value
            End Set
        End Property


        <ColumnInfo("PartName", "'{0}'")> _
        Public Property PartName As String
            Get
                Return _partName
            End Get
            Set(ByVal value As String)
                _partName = value
            End Set
        End Property


        <ColumnInfo("UoM", "'{0}'")> _
        Public Property UoM As String
            Get
                Return _uoM
            End Get
            Set(ByVal value As String)
                _uoM = value
            End Set
        End Property


        <ColumnInfo("RetailPrice", "{0}")> _
        Public Property RetailPrice As Decimal
            Get
                Return _retailPrice
            End Get
            Set(ByVal value As Decimal)
                _retailPrice = value
            End Set
        End Property


        <ColumnInfo("ActiveStatus", "{0}")> _
        Public Property ActiveStatus As Short
            Get
                Return _activeStatus
            End Get
            Set(ByVal value As Short)
                _activeStatus = value
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

