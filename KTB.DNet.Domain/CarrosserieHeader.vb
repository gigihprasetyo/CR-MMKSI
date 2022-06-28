
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : CarrosserieHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 21/03/2018 - 10:46:42
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
    <Serializable(), TableInfo("CarrosserieHeader")> _
    Public Class CarrosserieHeader
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
        Private _pDIStateCode As Short
        Private _pDIStatusCode As Short
        Private _bUCode As String = String.Empty
        Private _bUName As String = String.Empty
        Private _pDIName As String = String.Empty
        Private _pDIReceiptNo As String = String.Empty
        Private _pDIReceiptRefName As String = String.Empty
        Private _pDIReceiptStatus As Short
        Private _transactionDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _transactionType As Short
        Private _vendorName As String = String.Empty
        Private _chassisNumber As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)


        Private _carrosserieDetails As System.Collections.ArrayList = New System.Collections.ArrayList()


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


        <ColumnInfo("PDIStateCode", "{0}")> _
        Public Property PDIStateCode As Short
            Get
                Return _pDIStateCode
            End Get
            Set(ByVal value As Short)
                _pDIStateCode = value
            End Set
        End Property


        <ColumnInfo("PDIStatusCode", "{0}")> _
        Public Property PDIStatusCode As Short
            Get
                Return _pDIStatusCode
            End Get
            Set(ByVal value As Short)
                _pDIStatusCode = value
            End Set
        End Property


        <ColumnInfo("BUCode", "'{0}'")> _
        Public Property BUCode As String
            Get
                Return _bUCode
            End Get
            Set(ByVal value As String)
                _bUCode = value
            End Set
        End Property


        <ColumnInfo("BUName", "'{0}'")> _
        Public Property BUName As String
            Get
                Return _bUName
            End Get
            Set(ByVal value As String)
                _bUName = value
            End Set
        End Property


        <ColumnInfo("PDIName", "'{0}'")> _
        Public Property PDIName As String
            Get
                Return _pDIName
            End Get
            Set(ByVal value As String)
                _pDIName = value
            End Set
        End Property


        <ColumnInfo("PDIReceiptNo", "'{0}'")> _
        Public Property PDIReceiptNo As String
            Get
                Return _pDIReceiptNo
            End Get
            Set(ByVal value As String)
                _pDIReceiptNo = value
            End Set
        End Property


        <ColumnInfo("PDIReceiptRefName", "'{0}'")> _
        Public Property PDIReceiptRefName As String
            Get
                Return _pDIReceiptRefName
            End Get
            Set(ByVal value As String)
                _pDIReceiptRefName = value
            End Set
        End Property


        <ColumnInfo("PDIReceiptStatus", "{0}")> _
        Public Property PDIReceiptStatus As Short
            Get
                Return _pDIReceiptStatus
            End Get
            Set(ByVal value As Short)
                _pDIReceiptStatus = value
            End Set
        End Property


        <ColumnInfo("TransactionDate", "'{0:yyyy/MM/dd}'")> _
        Public Property TransactionDate As DateTime
            Get
                Return _transactionDate
            End Get
            Set(ByVal value As DateTime)
                _transactionDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("TransactionType", "{0}")> _
        Public Property TransactionType As Short
            Get
                Return _transactionType
            End Get
            Set(ByVal value As Short)
                _transactionType = value
            End Set
        End Property


        <ColumnInfo("VendorName", "'{0}'")> _
        Public Property VendorName As String
            Get
                Return _vendorName
            End Get
            Set(ByVal value As String)
                _vendorName = value
            End Set
        End Property


        <ColumnInfo("ChassisNumber", "'{0}'")> _
        Public Property ChassisNumber As String
            Get
                Return _chassisNumber
            End Get
            Set(ByVal value As String)
                _chassisNumber = value
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



        <RelationInfo("CarrosserieHeader", "ID", "CarrosserieDetail", "CarrosserieHeaderID")> _
        Public ReadOnly Property CarrosserieDetails As System.Collections.ArrayList
            Get
                Try
                    If (Me._carrosserieDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(CarrosserieDetail), "CarrosserieHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(CarrosserieDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._carrosserieDetails = DoLoadArray(GetType(CarrosserieDetail).ToString, criterias)
                    End If

                    Return Me._carrosserieDetails

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

#End Region

    End Class
End Namespace

