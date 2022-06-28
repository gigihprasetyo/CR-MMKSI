
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : Karoseri Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 20/07/2018 - 13:22:39
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
    <Serializable(), TableInfo("VWI_Karoseri")> _
    Public Class VWI_Karoseri
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal Code As String)
            _Code = Code
        End Sub

#End Region

#Region "Private Variables"

        Private _Code As String = String.Empty
        Private _Name As String = String.Empty
        Private _Province As String = String.Empty
        Private _City As String = String.Empty
        Private _LastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _Status As Integer




#End Region

#Region "Public Properties"

        <ColumnInfo("Code", "'{0}'")> _
        Public Property Code As String
            Get
                Return _Code
            End Get
            Set(ByVal value As String)
                _Code = value
            End Set
        End Property


        <ColumnInfo("Name", "'{0}'")> _
        Public Property Name As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                _Name = value
            End Set
        End Property


        <ColumnInfo("Province", "'{0}'")> _
        Public Property Province As String
            Get
                Return _Province
            End Get
            Set(ByVal value As String)
                _Province = value
            End Set
        End Property


        <ColumnInfo("City", "'{0}'")> _
        Public Property City As String
            Get
                Return _City
            End Get
            Set(ByVal value As String)
                _City = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime As DateTime
            Get
                Return _LastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _LastUpdateTime = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Integer
            Get
                Return _Status
            End Get
            Set(ByVal value As Integer)
                _Status = value
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

