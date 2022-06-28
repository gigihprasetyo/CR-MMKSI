
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : FactoringMaster Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2010 
'// ---------------------
'// $History      : $
'// Generated on 10/2/2010 - 2:33:05 PM
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
    <Serializable(), TableInfo("FactoringMaster")> _
    Public Class FactoringMaster
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
        Private _creditAccount As String = String.Empty
        Private _totalCeiling As Decimal
        Private _standardCeiling As Decimal
        Private _factoringCeiling As Decimal
        Private _giroTolakan As Decimal
        Private _outstanding As Decimal
        Private _availableCeiling As Decimal
        Private _status As Short
        Private _maxTOPDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUploadedBy As String = String.Empty
        Private _lastUploadedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)


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


        <ColumnInfo("CreditAccount", "'{0}'")> _
        Public Property CreditAccount() As String
            Get
                Return _creditAccount
            End Get
            Set(ByVal value As String)
                _creditAccount = value
            End Set
        End Property

        <ColumnInfo("TotalCeiling", "{0}")> _
        Public Property TotalCeiling() As Decimal
            Get
                Return _totalCeiling
            End Get
            Set(ByVal value As Decimal)
                _totalCeiling = value
            End Set
        End Property

        <ColumnInfo("StandardCeiling", "{0}")> _
        Public Property StandardCeiling() As Decimal
            Get
                Return _standardCeiling
            End Get
            Set(ByVal value As Decimal)
                _standardCeiling = value
            End Set
        End Property

        <ColumnInfo("FactoringCeiling", "{0}")> _
        Public Property FactoringCeiling() As Decimal
            Get
                Return _factoringCeiling
            End Get
            Set(ByVal value As Decimal)
                _factoringCeiling = value
            End Set
        End Property


        <ColumnInfo("GiroTolakan", "{0}")> _
        Public Property GiroTolakan() As Decimal
            Get
                Return _giroTolakan
            End Get
            Set(ByVal value As Decimal)
                _giroTolakan = value
            End Set
        End Property

        <ColumnInfo("Outstanding", "{0}")> _
        Public Property Outstanding() As Decimal
            Get
                Return _outstanding
            End Get
            Set(ByVal value As Decimal)
                _outstanding = value
            End Set
        End Property

        <ColumnInfo("AvailableCeiling", "{0}")> _
        Public Property AvailableCeiling() As Decimal
            Get
                Return _availableCeiling
            End Get
            Set(ByVal value As Decimal)
                _availableCeiling = value
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

        <ColumnInfo("MaxTOPDate", "'{0:yyyy/MM/dd}'")> _
        Public Property MaxTOPDate() As DateTime
            Get
                Return _maxTOPDate
            End Get
            Set(ByVal value As DateTime)
                _maxTOPDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("LastUploadedBy", "'{0}'")> _
        Public Property LastUploadedBy() As String
            Get
                Return _lastUploadedBy
            End Get
            Set(ByVal value As String)
                _lastUploadedBy = value
            End Set
        End Property


        <ColumnInfo("LastUploadedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUploadedTime() As DateTime
            Get
                Return _lastUploadedTime
            End Get
            Set(ByVal value As DateTime)
                _lastUploadedTime = value
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


        <ColumnInfo("ProductCategoryID", "{0}"), _
        RelationInfo("ProductCategory", "ID", "FactoringMaster", "ProductCategoryID")> _
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

