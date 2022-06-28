
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DN
'// PURPOSE       : v_DealerStockID Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 8/19/2009 - 2:36:37 PM
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
    <Serializable(), TableInfo("v_DealerStockID")> _
    Public Class v_DealerStockID
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
        Private _dID As Short
        Private _vtID As Short
        Private _vcID As Short
        Private _pkhID As Integer
        Private _custID As Integer
        Private _ecID As Integer
        Private _catID As Byte
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


        <ColumnInfo("dID", "{0}")> _
        Public Property dID() As Short
            Get
                Return _dID
            End Get
            Set(ByVal value As Short)
                _dID = value
            End Set
        End Property


        <ColumnInfo("vtID", "{0}")> _
        Public Property vtID() As Short
            Get
                Return _vtID
            End Get
            Set(ByVal value As Short)
                _vtID = value
            End Set
        End Property


        <ColumnInfo("vcID", "{0}")> _
        Public Property vcID() As Short
            Get
                Return _vcID
            End Get
            Set(ByVal value As Short)
                _vcID = value
            End Set
        End Property


        <ColumnInfo("pkhID", "{0}")> _
        Public Property pkhID() As Integer
            Get
                Return _pkhID
            End Get
            Set(ByVal value As Integer)
                _pkhID = value
            End Set
        End Property


        <ColumnInfo("custID", "{0}")> _
        Public Property custID() As Integer
            Get
                Return _custID
            End Get
            Set(ByVal value As Integer)
                _custID = value
            End Set
        End Property


        <ColumnInfo("ecID", "{0}")> _
        Public Property ecID() As Integer
            Get
                Return _ecID
            End Get
            Set(ByVal value As Integer)
                _ecID = value
            End Set
        End Property


        <ColumnInfo("catID", "{0}")> _
        Public Property catID() As Byte
            Get
                Return _catID
            End Get
            Set(ByVal value As Byte)
                _catID = value
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

