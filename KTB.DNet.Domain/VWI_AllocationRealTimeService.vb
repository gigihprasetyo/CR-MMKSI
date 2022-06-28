#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VWI_ServiceAdvisor Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 7/20/2020 - 12:23:06 PM
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
    <Serializable(), TableInfo("VWI_AllocationRealTimeService")> _
    Public Class VWI_AllocationRealTimeService
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
        Private _alokasiStall As Integer
        Private _currentStall As Integer
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


        <ColumnInfo("AlokasiStall", "{0}")> _
        Public Property AlokasiStall As Integer
            Get
                Return _alokasiStall
            End Get
            Set(ByVal value As Integer)
                _alokasiStall = value
            End Set
        End Property

        <ColumnInfo("CurrentStall", "{0}")> _
        Public Property CurrentStall As Integer
            Get
                Return _currentStall
            End Get
            Set(ByVal value As Integer)
                _currentStall = value
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
