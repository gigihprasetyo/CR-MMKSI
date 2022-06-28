
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_UnmatchSPKChassis Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 11/10/2018 - 22:12:52
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
    <Serializable(), TableInfo("VWI_UnmatchSPKChassis")> _
    Public Class VWI_UnmatchSPKChassis
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
        Private _regNumber As String = String.Empty
        Private _revisionDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _revisionStatusID As Short
        Private _revisionStatus As String = String.Empty
        Private _revisionType As String = String.Empty
        Private _chassisNumber As String = String.Empty
        Private _sPKHeaderID As Integer
        Private _sPKNumber As String = String.Empty
        Private _dealerCode As String = String.Empty
        Private _dealerSPKNumber As String = String.Empty
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


        <ColumnInfo("RegNumber", "'{0}'")> _
        Public Property RegNumber As String
            Get
                Return _regNumber
            End Get
            Set(ByVal value As String)
                _regNumber = value
            End Set
        End Property


        <ColumnInfo("RevisionDate", "'{0:yyyy/MM/dd}'")> _
        Public Property RevisionDate As DateTime
            Get
                Return _revisionDate
            End Get
            Set(ByVal value As DateTime)
                _revisionDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("RevisionStatusID", "{0}")> _
        Public Property RevisionStatusID As Short
            Get
                Return _revisionStatusID
            End Get
            Set(ByVal value As Short)
                _revisionStatusID = value
            End Set
        End Property


        <ColumnInfo("RevisionStatus", "'{0}'")> _
        Public Property RevisionStatus As String
            Get
                Return _revisionStatus
            End Get
            Set(ByVal value As String)
                _revisionStatus = value
            End Set
        End Property


        <ColumnInfo("RevisionType", "'{0}'")> _
        Public Property RevisionType As String
            Get
                Return _revisionType
            End Get
            Set(ByVal value As String)
                _revisionType = value
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


        <ColumnInfo("SPKHeaderID", "{0}")> _
        Public Property SPKHeaderID As Integer
            Get
                Return _sPKHeaderID
            End Get
            Set(ByVal value As Integer)
                _sPKHeaderID = value
            End Set
        End Property


        <ColumnInfo("SPKNumber", "'{0}'")> _
        Public Property SPKNumber As String
            Get
                Return _sPKNumber
            End Get
            Set(ByVal value As String)
                _sPKNumber = value
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


        <ColumnInfo("DealerSPKNumber", "'{0}'")> _
        Public Property DealerSPKNumber As String
            Get
                Return _dealerSPKNumber
            End Get
            Set(ByVal value As String)
                _dealerSPKNumber = value
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

