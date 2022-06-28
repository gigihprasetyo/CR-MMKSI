
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : Cessie Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2010 
'// ---------------------
'// $History      : $
'// Generated on 10/8/2010 - 10:50:53 AM
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
    <Serializable(), TableInfo("Cessie")> _
    Public Class Cessie
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
        Private _cessieNumber As String = String.Empty
        Private _cessieDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _amount As Decimal
        Private _paymentDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _purchaseAmount As Decimal
        Private _pDFFile As String = String.Empty
        Private _pDFFile2 As String = String.Empty
        Private _textFile As String = String.Empty
        Private _downloadedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _downloadedBy As String = String.Empty
        Private _adminFee As Decimal
        Private _differenceAmount As Decimal
        Private _numOfTransfered As Integer
        Private _rowStatus As Short
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _createdBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty

        Private _cessieDetails As New ArrayList
        Private _v_CessieDetail As V_CessieDetail
        Private _productCategory As ProductCategory


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


        <ColumnInfo("CessieNumber", "'{0}'")> _
        Public Property CessieNumber() As String
            Get
                Return _cessieNumber
            End Get
            Set(ByVal value As String)
                _cessieNumber = value
            End Set
        End Property


        <ColumnInfo("CessieDate", "'{0:yyyy/MM/dd}'")> _
        Public Property CessieDate() As DateTime
            Get
                Return _cessieDate
            End Get
            Set(ByVal value As DateTime)
                _cessieDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("Amount", "{0}")> _
        Public Property Amount() As Decimal
            Get
                Return _amount
            End Get
            Set(ByVal value As Decimal)
                _amount = value
            End Set
        End Property


        <ColumnInfo("PaymentDate", "'{0:yyyy/MM/dd}'")> _
        Public Property PaymentDate() As DateTime
            Get
                Return _paymentDate
            End Get
            Set(ByVal value As DateTime)
                _paymentDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("PurchaseAmount", "{0}")> _
        Public Property PurchaseAmount() As Decimal
            Get
                Return _purchaseAmount
            End Get
            Set(ByVal value As Decimal)
                _purchaseAmount = value
            End Set
        End Property


        <ColumnInfo("PDFFile", "'{0}'")> _
  Public Property PDFFile() As String
            Get
                Return _pDFFile
            End Get
            Set(ByVal value As String)
                _pDFFile = value
            End Set
        End Property

        <ColumnInfo("PDFFile2", "'{0}'")> _
        Public Property PDFFile2() As String
            Get
                Return _pDFFile2
            End Get
            Set(ByVal value As String)
                _pDFFile2 = value
            End Set
        End Property


        <ColumnInfo("TextFile", "'{0}'")> _
        Public Property TextFile() As String
            Get
                Return _textFile
            End Get
            Set(ByVal value As String)
                _textFile = value
            End Set
        End Property


        <ColumnInfo("DownloadedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property DownloadedTime() As DateTime
            Get
                Return _downloadedTime
            End Get
            Set(ByVal value As DateTime)
                _downloadedTime = value
            End Set
        End Property


        <ColumnInfo("DownloadedBy", "'{0}'")> _
        Public Property DownloadedBy() As String
            Get
                Return _downloadedBy
            End Get
            Set(ByVal value As String)
                _downloadedBy = value
            End Set
        End Property


        <ColumnInfo("AdminFee", "{0}")> _
        Public Property AdminFee() As Decimal
            Get
                Return _adminFee
            End Get
            Set(ByVal value As Decimal)
                _adminFee = value
            End Set
        End Property


        <ColumnInfo("DifferenceAmount", "{0}")> _
        Public Property DifferenceAmount() As Decimal
            Get
                Return _differenceAmount
            End Get
            Set(ByVal value As Decimal)
                _differenceAmount = value
            End Set
        End Property

        <ColumnInfo("NumOfTransfered", "{0}")> _
          Public Property NumOfTransfered() As Integer
            Get
                Return _numOfTransfered
            End Get
            Set(ByVal value As Integer)
                _numOfTransfered = value
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


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime() As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
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


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime() As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
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


        <RelationInfo("Cessie", "ID", "CessieDetail", "CessieID")> _
        Public ReadOnly Property CessieDetails() As System.Collections.ArrayList
            Get
                Try
                    If (Me._cessieDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(CessieDetail), "Cessie", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(CessieDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._cessieDetails = DoLoadArray(GetType(CessieDetail).ToString, criterias)
                    End If

                    Return Me._cessieDetails

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property


        'V_CessieDetail
        <ColumnInfo("ID", "{0}"), _
        RelationInfo("V_CessieDetail", "ID", "Cessie", "ID")> _
        Public Property V_CessieDetail() As V_CessieDetail
            Get
                Try
                    If IsNothing(_v_CessieDetail) OrElse _v_CessieDetail.ID < 1 Then
                        Me._v_CessieDetail = CType(DoLoad(GetType(V_CessieDetail).ToString(), Me.ID), V_CessieDetail)
                    End If

                    Return Me._v_CessieDetail

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As V_CessieDetail)

                Me._v_CessieDetail = value
            End Set
        End Property


        <ColumnInfo("ProductCategoryID", "{0}"), _
        RelationInfo("ProductCategory", "ID", "Cessie", "ProductCategoryID")> _
        Public Property ProductCategory() As ProductCategory
            Get
                Try
                    If Not IsNothing(Me._productCategory) AndAlso (Not Me._productCategory.IsLoaded) Then

                        Me._productCategory = CType(DoLoad(GetType(ProductCategory).ToString(), _productCategory.ID), ProductCategory)
                        Me._productCategory.MarkLoaded()

                    End If

                    Return Me._productCategory

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ProductCategory)

                Me._productCategory = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._productCategory.MarkLoaded()
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

#End Region

    End Class
End Namespace

