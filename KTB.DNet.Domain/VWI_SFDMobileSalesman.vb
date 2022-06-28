
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : vwi_SFDMobileSalesman Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 06/29/2019 - 7:48:45 PM
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
    <Serializable(), TableInfo("VWI_SFDMobileSalesman")> _
    Public Class VWI_SFDMobileSalesman
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
        Private _dealerCode As String = String.Empty
        Private _dealerName As String = String.Empty
        Private _dealerCity As String = String.Empty
        Private _dealerGroup As String = String.Empty
        Private _dealerArea As String = String.Empty
        Private _dealerBranchCode As String = String.Empty
        Private _dealerBranchName As String = String.Empty
        Private _salesmanCode As String = String.Empty
        Private _salesmanName As String = String.Empty
        Private _salesmanHireDate As Date = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, Date)
        Private _jobDescription As String = String.Empty
        Private _levelDescription As String = String.Empty
        Private _superiorName As String = String.Empty
        Private _superiorCode As String = String.Empty
        Private _salesmanEmail As String = String.Empty
        Private _salesmanHandphone As String = String.Empty
        Private _salesmanTeamCategory As String = String.Empty
        Private _salesmanStatus As String = String.Empty
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


        <ColumnInfo("DealerCity", "'{0}'")> _
        Public Property DealerCity As String
            Get
                Return _dealerCity
            End Get
            Set(ByVal value As String)
                _dealerCity = value
            End Set
        End Property


        <ColumnInfo("DealerGroup", "'{0}'")> _
        Public Property DealerGroup As String
            Get
                Return _dealerGroup
            End Get
            Set(ByVal value As String)
                _dealerGroup = value
            End Set
        End Property


        <ColumnInfo("DealerArea", "'{0}'")> _
        Public Property DealerArea As String
            Get
                Return _dealerArea
            End Get
            Set(ByVal value As String)
                _dealerArea = value
            End Set
        End Property


        <ColumnInfo("DealerBranchCode", "'{0}'")> _
        Public Property DealerBranchCode As String
            Get
                Return _dealerBranchCode
            End Get
            Set(ByVal value As String)
                _dealerBranchCode = value
            End Set
        End Property


        <ColumnInfo("DealerBranchName", "'{0}'")> _
        Public Property DealerBranchName As String
            Get
                Return _dealerBranchName
            End Get
            Set(ByVal value As String)
                _dealerBranchName = value
            End Set
        End Property


        <ColumnInfo("SalesmanCode", "'{0}'")> _
        Public Property SalesmanCode As String
            Get
                Return _salesmanCode
            End Get
            Set(ByVal value As String)
                _salesmanCode = value
            End Set
        End Property


        <ColumnInfo("SalesmanName", "'{0}'")> _
        Public Property SalesmanName As String
            Get
                Return _salesmanName
            End Get
            Set(ByVal value As String)
                _salesmanName = value
            End Set
        End Property


        <ColumnInfo("SalesmanHireDate", "'{0:yyyy/MM/dd}'")> _
        Public Property SalesmanHireDate As Date
            Get
                Return _salesmanHireDate
            End Get
            Set(ByVal value As Date)
                _salesmanHireDate = value
            End Set
        End Property


        <ColumnInfo("JobDescription", "'{0}'")> _
        Public Property JobDescription As String
            Get
                Return _jobDescription
            End Get
            Set(ByVal value As String)
                _jobDescription = value
            End Set
        End Property


        <ColumnInfo("LevelDescription", "'{0}'")> _
        Public Property LevelDescription As String
            Get
                Return _levelDescription
            End Get
            Set(ByVal value As String)
                _levelDescription = value
            End Set
        End Property


        <ColumnInfo("SuperiorName", "'{0}'")> _
        Public Property SuperiorName As String
            Get
                Return _superiorName
            End Get
            Set(ByVal value As String)
                _superiorName = value
            End Set
        End Property


        <ColumnInfo("SuperiorCode", "'{0}'")> _
        Public Property SuperiorCode As String
            Get
                Return _superiorCode
            End Get
            Set(ByVal value As String)
                _superiorCode = value
            End Set
        End Property


        <ColumnInfo("SalesmanEmail", "'{0}'")> _
        Public Property SalesmanEmail As String
            Get
                Return _salesmanEmail
            End Get
            Set(ByVal value As String)
                _salesmanEmail = value
            End Set
        End Property


        <ColumnInfo("SalesmanHandphone", "'{0}'")> _
        Public Property SalesmanHandphone As String
            Get
                Return _salesmanHandphone
            End Get
            Set(ByVal value As String)
                _salesmanHandphone = value
            End Set
        End Property


        <ColumnInfo("SalesmanTeamCategory", "'{0}'")> _
        Public Property SalesmanTeamCategory As String
            Get
                Return _salesmanTeamCategory
            End Get
            Set(ByVal value As String)
                _salesmanTeamCategory = value
            End Set
        End Property


        <ColumnInfo("SalesmanStatus", "'{0}'")> _
        Public Property SalesmanStatus As String
            Get
                Return _salesmanStatus
            End Get
            Set(ByVal value As String)
                _salesmanStatus = value
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

