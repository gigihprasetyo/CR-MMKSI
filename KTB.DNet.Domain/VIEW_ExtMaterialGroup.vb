
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet team
'// PURPOSE       : VIEW_ExtMaterialGroup Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 28/11/2005 - 12:44:36
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

    <Serializable(), TableInfo("VIEW_ExtMaterialGroup")> _
    Public Class VIEW_ExtMaterialGroup
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

#End Region

#Region "Private Variables"

        Private _monthPeriode As Short
        Private _yearPeriode As Short
        Private _extMaterialGroup As String = String.Empty

#End Region

#Region "Public Properties"

        <ColumnInfo("MonthPeriode", "{0}")> _
        Public Property MonthPeriode() As Short
            Get
                Return _monthPeriode
            End Get
            Set(ByVal value As Short)
                _monthPeriode = value
            End Set
        End Property


        <ColumnInfo("YearPeriode", "{0}")> _
        Public Property YearPeriode() As Short
            Get
                Return _yearPeriode
            End Get
            Set(ByVal value As Short)
                _yearPeriode = value
            End Set
        End Property


        <ColumnInfo("ExtMaterialGroup", "'{0}'")> _
        Public Property ExtMaterialGroup() As String
            Get
                Return _extMaterialGroup
            End Get
            Set(ByVal value As String)
                _extMaterialGroup = value
            End Set
        End Property


#End Region

#Region "Custom Method"

#End Region

    End Class

End Namespace

