
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CcAttribute Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 12/03/2020 - 10:58:34
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
    <Serializable(), TableInfo("CcPerformanceReport")> _
    Public Class CcPerformanceReport
        Inherits DomainObject



        Public Sub New()
        End Sub

        Private _dealerCode As String
        Public Property DealerCode() As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property

        Private _dealerName As String
        Public Property DealerName() As String
            Get
                Return _dealerName
            End Get
            Set(ByVal value As String)
                _dealerName = value
            End Set
        End Property

        Private _searchTerm1 As String
        Public Property SearchTerm1() As String
            Get
                Return _searchTerm1
            End Get
            Set(ByVal value As String)
                _searchTerm1 = value
            End Set
        End Property

        Private _groupName As String
        Public Property GroupName() As String
            Get
                Return _groupName
            End Get
            Set(ByVal value As String)
                _groupName = value
            End Set
        End Property

        Private _cityName As String
        Public Property CityName() As String
            Get
                Return _cityName
            End Get
            Set(ByVal value As String)
                _cityName = value
            End Set
        End Property

        Private _description As String
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property

        Private _yearMonth As String
        Public Property YearMonth() As String
            Get
                Return _yearMonth
            End Get
            Set(ByVal value As String)
                _yearMonth = value
            End Set
        End Property

        Private _code As String
        Public Property Code() As String
            Get
                Return _code
            End Get
            Set(ByVal value As String)
                _code = value
            End Set
        End Property

        Private _clusterName As String
        Public Property ClusterName() As String
            Get
                Return _clusterName
            End Get
            Set(ByVal value As String)
                _clusterName = value
            End Set
        End Property

        Private _parameterName As String
        Public Property ParameterName() As String
            Get
                Return _parameterName
            End Get
            Set(ByVal value As String)
                _parameterName = value
            End Set
        End Property

        Private _subParameterName As String
        Public Property SubParameterName() As String
            Get
                Return _subParameterName
            End Get
            Set(ByVal value As String)
                _subParameterName = value
            End Set
        End Property

        Private _totalScore As Decimal
        Public Property TotalScore() As Decimal
            Get
                Return _totalScore
            End Get
            Set(ByVal value As Decimal)
                _totalScore = value
            End Set
        End Property

        Private _summaryScore As Decimal
        Public Property SummaryScore() As Decimal
            Get
                Return _summaryScore
            End Get
            Set(ByVal value As Decimal)
                _summaryScore = value
            End Set
        End Property

        Private _csp_ranking As Short
        Public Property CSP_Ranking() As Short
            Get
                Return _csp_ranking
            End Get
            Set(ByVal value As Short)
                _csp_ranking = value
            End Set
        End Property








    End Class
End Namespace

