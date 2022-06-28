
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DRReportRanking Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 10/15/2012 - 10:32:08 AM
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
    <Serializable(), TableInfo("DRReportRanking")> _
    Public Class DRReportRanking
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
        Private _dealerAreaID As Integer
        Private _pdfFileName As String = String.Empty
        Private _reportStatus As Short
        Private _downloadStatus As Short
        Private _periodType As Short
        Private _period As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _dealerType As Short
        Private _ccReportMaster As CcReportMaster
        Private _ccVehicleCategory As CcVehicleCategory
        Private _dealer As Dealer
        Private _dealerGroup As DealerGroup
        Private _ccCustomerCategory As CcCustomerCategory
        Private _xlsFileName As String = String.Empty



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


        <ColumnInfo("DealerAreaID", "{0}")> _
        Public Property DealerAreaID() As Integer
            Get
                Return _dealerAreaID
            End Get
            Set(ByVal value As Integer)
                _dealerAreaID = value
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

        <ColumnInfo("XlsFileName", "'{0}'")> _
        Public Property XlsFileName() As String
            Get
                Return _xlsFileName
            End Get
            Set(ByVal value As String)
                _xlsFileName = value
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


        <ColumnInfo("DownloadStatus", "{0}")> _
        Public Property DownloadStatus() As Short
            Get
                Return _downloadStatus
            End Get
            Set(ByVal value As Short)
                _downloadStatus = value
            End Set
        End Property


        <ColumnInfo("PeriodType", "{0}")> _
        Public Property PeriodType() As Short
            Get
                Return _periodType
            End Get
            Set(ByVal value As Short)
                _periodType = value
            End Set
        End Property


        <ColumnInfo("Period", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property Period() As DateTime
            Get
                Return _period
            End Get
            Set(ByVal value As DateTime)
                _period = value
            End Set
        End Property

        <ColumnInfo("DealerType", "{0}")> _
        Public Property DealerType() As Short
            Get
                Return _dealerType
            End Get
            Set(ByVal value As Short)
                _dealerType = value
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


        <ColumnInfo("CcReportMasterID", "{0}"), _
        RelationInfo("CcReportMaster", "ID", "DRReportRanking", "CcReportMasterID")> _
        Public Property CcReportMaster() As CcReportMaster
            Get
                Try
                    If Not IsNothing(Me._ccReportMaster) AndAlso (Not Me._ccReportMaster.IsLoaded) Then

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
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._ccReportMaster.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("CcVehicleCategoryID", "{0}"), _
        RelationInfo("CcVehicleCategory", "ID", "DRReportRanking", "CcVehicleCategoryID")> _
        Public Property CcVehicleCategory() As CcVehicleCategory
            Get
                Try
                    If Not IsNothing(Me._ccVehicleCategory) AndAlso (Not Me._ccVehicleCategory.IsLoaded) Then

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
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._ccVehicleCategory.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "DRReportRanking", "DealerID")> _
        Public Property Dealer() As Dealer
            Get
                Try
                    If Not IsNothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

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
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("DealerGroupID", "{0}"), _
        RelationInfo("DealerGroup", "ID", "DRReportRanking", "DealerGroupID")> _
        Public Property DealerGroup() As DealerGroup
            Get
                Try
                    If Not IsNothing(Me._dealerGroup) AndAlso (Not Me._dealerGroup.IsLoaded) Then

                        Me._dealerGroup = CType(DoLoad(GetType(DealerGroup).ToString(), _dealerGroup.ID), DealerGroup)
                        Me._dealerGroup.MarkLoaded()

                    End If

                    Return Me._dealerGroup

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As DealerGroup)

                Me._dealerGroup = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealerGroup.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("CcCustomerCategoryID", "{0}"), _
        RelationInfo("CcCustomerCategory", "ID", "DRReportRanking", "CcCustomerCategoryID")> _
        Public Property CcCustomerCategory() As CcCustomerCategory
            Get
                Try
                    If Not IsNothing(Me._ccCustomerCategory) AndAlso (Not Me._ccCustomerCategory.IsLoaded) Then

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
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._ccCustomerCategory.MarkLoaded()
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

