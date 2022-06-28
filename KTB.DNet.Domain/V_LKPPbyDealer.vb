
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_LKPPbyDealer Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 6/23/2015 - 9:30:54 AM
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
    <Serializable(), TableInfo("V_LKPPbyDealer")> _
    Public Class V_LKPPbyDealer
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
        Private _dealerID As Short
        Private _LKPPHeaderID As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _referenceNumber As String = String.Empty
        Private _letterDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _govInstName As String = String.Empty
        Private _description As String = String.Empty
        Private _attachment As String = String.Empty
        Private _status As Short
        Private _dealerCode As String = String.Empty
        Private _dealerName As String = String.Empty
        Private _title As String = String.Empty
        Private _searchTerm1 As String = String.Empty
        Private _searchTerm2 As String = String.Empty
        Private _dealerGroupID As Integer




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


        <ColumnInfo("DealerID", "{0}")> _
        Public Property DealerID As Short
            Get
                Return _dealerID
            End Get
            Set(ByVal value As Short)
                _dealerID = value
            End Set
        End Property


        <ColumnInfo("LKPPHeaderID", "{0}")> _
        Public Property LKPPHeaderID As Integer
            Get
                Return _LKPPHeaderID
            End Get
            Set(ByVal value As Integer)
                _LKPPHeaderID = value
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


        <ColumnInfo("ReferenceNumber", "'{0}'")> _
        Public Property ReferenceNumber As String
            Get
                Return _referenceNumber
            End Get
            Set(ByVal value As String)
                _referenceNumber = value
            End Set
        End Property


        <ColumnInfo("LetterDate", "'{0:yyyy/MM/dd}'")> _
        Public Property LetterDate As DateTime
            Get
                Return _letterDate
            End Get
            Set(ByVal value As DateTime)
                _letterDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("GovInstName", "'{0}'")> _
        Public Property GovInstName As String
            Get
                Return _govInstName
            End Get
            Set(ByVal value As String)
                _govInstName = value
            End Set
        End Property


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property


        <ColumnInfo("Attachment", "'{0}'")> _
        Public Property Attachment As String
            Get
                Return _attachment
            End Get
            Set(ByVal value As String)
                _attachment = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
            End Set
        End Property


        <ColumnInfo("DealerCode", "'{0}'")> _
        Public Property DealerCode As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property


        <ColumnInfo("DealerName", "'{0}'")> _
        Public Property DealerName As String
            Get
                Return _dealerName
            End Get
            Set(ByVal value As String)
                _dealerName = value
            End Set
        End Property


        <ColumnInfo("Title", "'{0}'")> _
        Public Property Title As String
            Get
                Return _title
            End Get
            Set(ByVal value As String)
                _title = value
            End Set
        End Property


        <ColumnInfo("SearchTerm1", "'{0}'")> _
        Public Property SearchTerm1 As String
            Get
                Return _searchTerm1
            End Get
            Set(ByVal value As String)
                _searchTerm1 = value
            End Set
        End Property


        <ColumnInfo("SearchTerm2", "'{0}'")> _
        Public Property SearchTerm2 As String
            Get
                Return _searchTerm2
            End Get
            Set(ByVal value As String)
                _searchTerm2 = value
            End Set
        End Property


        <ColumnInfo("DealerGroupID", "{0}")> _
        Public Property DealerGroupID As Integer
            Get
                Return _dealerGroupID
            End Get
            Set(ByVal value As Integer)
                _dealerGroupID = value
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

