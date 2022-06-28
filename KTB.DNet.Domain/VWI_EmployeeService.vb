
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_EmployeeService Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 23/04/2018 - 15:42:56
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
    <Serializable(), TableInfo("VWI_EmployeeService")> _
    Public Class VWI_EmployeeService
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
        Private _name As String = String.Empty
        Private _dealerCode As String = String.Empty
        Private _dealerBranchCode As String = String.Empty
        Private _birthDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _gender As Short
        Private _noKTP As String = String.Empty
        Private _email As String = String.Empty
        Private _startWorkingDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _jobPosition As String = String.Empty
        Private _educationLevel As String = String.Empty
        Private _photo As Byte()
        Private _shirtSize As String = String.Empty
        Private _status As Integer
        Private _salesmanStatusDNET As Integer
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty




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


        <ColumnInfo("Name", "'{0}'")> _
        Public Property Name As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
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

        <ColumnInfo("DealerBranchCode", "'{0}'")> _
        Public Property DealerBranchCode As String
            Get
                Return _dealerBranchCode
            End Get
            Set(ByVal value As String)
                _dealerBranchCode = value
            End Set
        End Property


        <ColumnInfo("BirthDate", "'{0:yyyy/MM/dd}'")> _
        Public Property BirthDate As DateTime
            Get
                Return _birthDate
            End Get
            Set(ByVal value As DateTime)
                _birthDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("Gender", "{0}")> _
        Public Property Gender As Short
            Get
                Return _gender
            End Get
            Set(ByVal value As Short)
                _gender = value
            End Set
        End Property

        <ColumnInfo("NoKTP", "'{0}'")> _
        Public Property NoKTP As String
            Get
                Return _noKTP
            End Get
            Set(ByVal value As String)
                _noKTP = value
            End Set
        End Property

        <ColumnInfo("Email", "'{0}'")> _
        Public Property Email As String
            Get
                Return _email
            End Get
            Set(ByVal value As String)
                _email = value
            End Set
        End Property

        <ColumnInfo("StartWorkingDate", "'{0:yyyy/MM/dd}'")> _
        Public Property StartWorkingDate As DateTime
            Get
                Return _startWorkingDate
            End Get
            Set(ByVal value As DateTime)
                _startWorkingDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("JobPosition", "'{0}'")> _
        Public Property JobPosition As String
            Get
                Return _jobPosition
            End Get
            Set(ByVal value As String)
                _jobPosition = value
            End Set
        End Property


        <ColumnInfo("EducationLevel", "'{0}'")> _
        Public Property EducationLevel As String
            Get
                Return _educationLevel
            End Get
            Set(ByVal value As String)
                _educationLevel = value
            End Set
        End Property


        <ColumnInfo("Photo", "{0}")> _
        Public Property Photo As Byte()
            Get
                Return _photo
            End Get
            Set(ByVal value As Byte())
                _photo = value
            End Set
        End Property


        <ColumnInfo("ShirtSize", "'{0}'")> _
        Public Property ShirtSize As String
            Get
                Return _shirtSize
            End Get
            Set(ByVal value As String)
                _shirtSize = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")>
        Public Property Status As Integer
            Get
                Return _status
            End Get
            Set(ByVal value As Integer)
                _status = value
            End Set
        End Property


        <ColumnInfo("SalesmanStatusDNET", "{0}")>
        Public Property SalesmanStatusDNET As Integer
            Get
                Return _salesmanStatusDNET
            End Get
            Set(ByVal value As Integer)
                _salesmanStatusDNET = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")>
        Public Property LastUpdateTime As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
            End Set
        End Property

        <ColumnInfo("LastUpdateBy", "'{0}'")>
        Public Property LastUpdateBy As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
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