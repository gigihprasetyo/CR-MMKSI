#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrCertificateConfig Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 10/24/2019 - 11:13:29 AM
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
    <Serializable(), TableInfo("TrCertificateConfig")> _
    Public Class TrCertificateConfig
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
        Private _description As String = String.Empty
        Private _namaTTD As String = String.Empty
        Private _jabatanTTD As String = String.Empty
        Private _pathTTD As String = String.Empty
        Private _rowStatus As Short
        Private _status As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
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


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property


        <ColumnInfo("NamaTTD", "'{0}'")> _
        Public Property NamaTTD As String
            Get
                Return _namaTTD
            End Get
            Set(ByVal value As String)
                _namaTTD = value
            End Set
        End Property


        <ColumnInfo("JabatanTTD", "'{0}'")> _
        Public Property JabatanTTD As String
            Get
                Return _jabatanTTD
            End Get
            Set(ByVal value As String)
                _jabatanTTD = value
            End Set
        End Property


        <ColumnInfo("PathTTD", "'{0}'")> _
        Public Property PathTTD As String
            Get
                Return _pathTTD
            End Get
            Set(ByVal value As String)
                _pathTTD = value
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


        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
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
