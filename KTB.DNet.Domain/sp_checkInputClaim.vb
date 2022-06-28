
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Heru Binarto
'// PURPOSE       : Mapping domain from SAP
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 9/27/2005 - 5:14:56 PM
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
Imports KTB.DNet.DataMapper.Framework
#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("sp_checkInputClaim")> _
    Public Class sp_checkInputClaim
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub



#End Region

#Region "Variable"
        Private _isValid As Integer
        Private _msg As String

#End Region

#Region "Public Properties"

        <ColumnInfo("IsValid", "{0}")> _
        Public Property IsValid() As Integer
            Get
                Return _isValid
            End Get
            Set(ByVal Value As Integer)
                _isValid = Value
            End Set
        End Property

        <ColumnInfo("Message", "'{0}'")> _
        Public Property Message() As String
            Get
                Return _msg
            End Get
            Set(ByVal Value As String)
                _msg = Value
            End Set
        End Property

#End Region

    End Class
End Namespace
