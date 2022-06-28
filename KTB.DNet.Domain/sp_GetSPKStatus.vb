#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : sp_AllocPO Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 5/29/2012 - 11:17:09 AM
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
    <Serializable(), TableInfo("sp_GetSPKStatus")> _
    Public Class sp_GetSPKStatus
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

#End Region

#Region "Private Variables"

        Private _values As Integer
        Private _status As Short

#End Region

#Region "Public Properties"

        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
            End Set
        End Property

        <ColumnInfo("Values", "{0}")> _
        Public Property Values() As Integer
            Get
                Return _values
            End Get
            Set(ByVal value As Integer)
                _values = value
            End Set
        End Property




#End Region

#Region "Generated Method"

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

