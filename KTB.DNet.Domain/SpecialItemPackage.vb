
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SpecialItemPackage Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 28/11/2005 - 10:34:08
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
    <Serializable(), TableInfo("SpecialItemPackage")> _
    Public Class SpecialItemPackage
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
        Private _packageNo As Short
        Private _packagePrice As Decimal
        Private _packageDescription As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _specialItemDetail As SpecialItemDetail



#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID() As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property


        <ColumnInfo("PackageNo", "{0}")> _
        Public Property PackageNo() As Short
            Get
                Return _packageNo
            End Get
            Set(ByVal value As Short)
                _packageNo = value
            End Set
        End Property


        <ColumnInfo("PackagePrice", "{0}")> _
        Public Property PackagePrice() As Decimal
            Get
                Return _packagePrice
            End Get
            Set(ByVal value As Decimal)
                _packagePrice = value
            End Set
        End Property


        <ColumnInfo("PackageDescription", "'{0}'")> _
        Public Property PackageDescription() As String
            Get
                Return _packageDescription
            End Get
            Set(ByVal value As String)
                _packageDescription = value
            End Set
        End Property


        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus() As Short
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Short)
                _rowStatus = value
            End Set
        End Property


        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy() As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
            End Set
        End Property


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime() As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
            End Set
        End Property


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy() As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime() As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
            End Set
        End Property


        <ColumnInfo("SpecialItemDetailID", "{0}"), _
        RelationInfo("SpecialItemDetail", "ID", "SpecialItemPackage", "SpecialItemDetailID")> _
        Public Property SpecialItemDetail() As SpecialItemDetail
            Get
                Try
                    If Not isnothing(Me._specialItemDetail) AndAlso (Not Me._specialItemDetail.IsLoaded) Then

                        Me._specialItemDetail = CType(DoLoad(GetType(SpecialItemDetail).ToString(), _specialItemDetail.ID), SpecialItemDetail)
                        Me._specialItemDetail.MarkLoaded()

                    End If

                    Return Me._specialItemDetail

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SpecialItemDetail)

                Me._specialItemDetail = value
                If (Not IsNothing(value)) Then ' AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._specialItemDetail.MarkLoaded()
                End If
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


