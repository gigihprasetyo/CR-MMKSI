#Region "Summary"
'// ===========================================================================
'// AUTHOR        : 
'// PURPOSE       : AlokasiStok_view Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 13/10/2005 - 3:20:36 PM
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

    <Serializable(), TableInfo("AlokasiStok_view")> _
    Public Class AlokasiStok_view
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

#End Region

#Region "Private Variables"

        Private _iD As Integer
        Private _iDDealer As Integer
        Private _vechileColorID As Integer
        Private _vechileTypeID As Integer
        Private _dealerName As String = String.Empty
        Private _dealerCode As String = String.Empty
        Private _dealerID As Integer
        Private _reqQty As Integer
        Private _pOHeaderID As Integer
        Private _materialNumber As String = String.Empty
        Private _proposeQty As Integer
        Private _materialDescription As String = String.Empty
        Private _reqAllocationDateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _description As String = String.Empty
        Private _rowStatus As Short

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

        <ColumnInfo("IDDealer", "{0}")> _
        Public Property IDDealer() As Integer
            Get
                Return _iDDealer
            End Get
            Set(ByVal value As Integer)
                _iDDealer = value
            End Set
        End Property

        <ColumnInfo("VechileColorID", "{0}")> _
        Public Property VechileColorID() As Integer
            Get
                Return _vechileColorID
            End Get
            Set(ByVal value As Integer)
                _vechileColorID = value
            End Set
        End Property

        <ColumnInfo("VechileTypeID", "{0}")> _
        Public Property VechileTypeID() As Integer
            Get
                Return _vechileTypeID
            End Get
            Set(ByVal value As Integer)
                _vechileTypeID = value
            End Set
        End Property

        <ColumnInfo("DealerName", "'{0}'")> _
        Public Property DealerName() As String
            Get
                Return _dealerName
            End Get
            Set(ByVal value As String)
                _dealerName = value
            End Set
        End Property

        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode() As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}")> _
        Public Property DealerID() As Integer
            Get
                Return _dealerID
            End Get
            Set(ByVal value As Integer)
                _dealerID = value
            End Set
        End Property

        <ColumnInfo("ReqQty", "{0}")> _
        Public Property ReqQty() As Integer
            Get
                Return _reqQty
            End Get
            Set(ByVal value As Integer)
                _reqQty = value
            End Set
        End Property

        <ColumnInfo("POHeaderID", "{0}")> _
        Public Property POHeaderID() As Integer
            Get
                Return _pOHeaderID
            End Get
            Set(ByVal value As Integer)
                _pOHeaderID = value
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

        <ColumnInfo("ProposeQty", "{0}")> _
        Public Property ProposeQty() As Integer
            Get
                Return _proposeQty
            End Get
            Set(ByVal value As Integer)
                _proposeQty = value
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

        <ColumnInfo("ReqAllocationDateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ReqAllocationDateTime() As DateTime
            Get
                Return _reqAllocationDateTime
            End Get
            Set(ByVal value As DateTime)
                _reqAllocationDateTime = value
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

        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus() As Short
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Short)
                _rowStatus = value
            End Set
        End Property

#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace