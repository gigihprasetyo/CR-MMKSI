
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CcReportDealer Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 10/31/2011 - 3:39:29 PM
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
    <Serializable(), TableInfo("CcReportDealer")> _
    Public Class CcReportDealer
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
        Private _periodFrom As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _periodTo As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _pdfFileName As String = String.Empty
        Private _reportStatus As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _ccCustomerCategory As CcCustomerCategory
        Private _ccVehicleCategory As CcVehicleCategory
        Private _dealer As Dealer
        Private _ccReportMaster As CcReportMaster



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


        <ColumnInfo("PeriodFrom", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property PeriodFrom() As DateTime
            Get
                Return _periodFrom
            End Get
            Set(ByVal value As DateTime)
                _periodFrom = value
            End Set
        End Property


        <ColumnInfo("PeriodTo", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property PeriodTo() As DateTime
            Get
                Return _periodTo
            End Get
            Set(ByVal value As DateTime)
                _periodTo = value
            End Set
        End Property


        <ColumnInfo("PdfFileName", "'{0}'")> _
        Public Property PdfFileName() As String
            Get
                Return _pdfFileName
            End Get
            Set(ByVal value As String)
                _pdfFileName = value
            End Set
        End Property


        <ColumnInfo("ReportStatus", "{0}")> _
        Public Property ReportStatus() As Short
            Get
                Return _reportStatus
            End Get
            Set(ByVal value As Short)
                _reportStatus = value
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


        <ColumnInfo("CcCustomerCategoryID", "{0}"), _
        RelationInfo("CcCustomerCategory", "ID", "CcReportDealer", "CcCustomerCategoryID")> _
        Public Property CcCustomerCategory() As CcCustomerCategory
            Get
                Try
                    If Not isnothing(Me._ccCustomerCategory) AndAlso (Not Me._ccCustomerCategory.IsLoaded) Then

                        Me._ccCustomerCategory = CType(DoLoad(GetType(CcCustomerCategory).ToString(), _ccCustomerCategory.ID), CcCustomerCategory)
                        Me._ccCustomerCategory.MarkLoaded()

                    End If

                    Return Me._ccCustomerCategory

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As CcCustomerCategory)

                Me._ccCustomerCategory = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._ccCustomerCategory.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("CcVehicleCategoryID", "{0}"), _
        RelationInfo("CcVehicleCategory", "ID", "CcReportDealer", "CcVehicleCategoryID")> _
        Public Property CcVehicleCategory() As CcVehicleCategory
            Get
                Try
                    If Not isnothing(Me._ccVehicleCategory) AndAlso (Not Me._ccVehicleCategory.IsLoaded) Then

                        Me._ccVehicleCategory = CType(DoLoad(GetType(CcVehicleCategory).ToString(), _ccVehicleCategory.ID), CcVehicleCategory)
                        Me._ccVehicleCategory.MarkLoaded()

                    End If

                    Return Me._ccVehicleCategory

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As CcVehicleCategory)

                Me._ccVehicleCategory = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._ccVehicleCategory.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "CcReportDealer", "DealerID")> _
        Public Property Dealer() As Dealer
            Get
                Try
                    If Not isnothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

                        Me._dealer = CType(DoLoad(GetType(Dealer).ToString(), _dealer.ID), Dealer)
                        Me._dealer.MarkLoaded()

                    End If

                    Return Me._dealer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Dealer)

                Me._dealer = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("CcReportMasterID", "{0}"), _
        RelationInfo("CcReportMaster", "ID", "CcReportDealer", "CcReportMasterID")> _
        Public Property CcReportMaster() As CcReportMaster
            Get
                Try
                    If Not isnothing(Me._ccReportMaster) AndAlso (Not Me._ccReportMaster.IsLoaded) Then

                        Me._ccReportMaster = CType(DoLoad(GetType(CcReportMaster).ToString(), _ccReportMaster.ID), CcReportMaster)
                        Me._ccReportMaster.MarkLoaded()

                    End If

                    Return Me._ccReportMaster

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As CcReportMaster)

                Me._ccReportMaster = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._ccReportMaster.MarkLoaded()
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

