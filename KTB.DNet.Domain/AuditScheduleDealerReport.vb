#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AuditScheduleDealerReport Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 29/09/2007 - 8:37:09
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
    <Serializable(), TableInfo("AuditScheduleDealerReport")> _
    Public Class AuditScheduleDealerReport
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
        Private _itemDesc As String = String.Empty
        Private _itemImage As Byte()
        Private _itemImageReparation As Byte()
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _auditParameterPhotoID As Integer
        Private _auditScheduleDealerID As Integer
        Private _auditParameterPhoto As AuditParameterPhoto


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


        <ColumnInfo("ItemDesc", "'{0}'")> _
        Public Property ItemDesc() As String
            Get
                Return _itemDesc
            End Get
            Set(ByVal value As String)
                _itemDesc = value
            End Set
        End Property


        <ColumnInfo("ItemImage", "{0}")> _
        Public Property ItemImage() As Byte()
            Get
                Return _itemImage
            End Get
            Set(ByVal value As Byte())
                _itemImage = value
            End Set
        End Property


        <ColumnInfo("ItemImageReparation", "{0}")> _
        Public Property ItemImageReparation() As Byte()
            Get
                Return _itemImageReparation
            End Get
            Set(ByVal value As Byte())
                _itemImageReparation = value
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


        <ColumnInfo("AuditParameterPhotoID", "{0}")> _
        Public Property AuditParameterPhotoID() As Integer

            Get
                Return _auditParameterPhotoID
            End Get
            Set(ByVal value As Integer)
                _auditParameterPhotoID = value
            End Set
        End Property
        <ColumnInfo("AuditScheduleDealerID", "{0}")> _
        Public Property AuditScheduleDealerID() As Integer

            Get
                Return _auditScheduleDealerID
            End Get
            Set(ByVal value As Integer)
                _auditScheduleDealerID = value
            End Set
        End Property

        <ColumnInfo("AuditParameterPhotoID", "{0}"), RelationInfo("AuditParameterPhoto", "ID", "AuditScheduleDealerReport", "AuditParameterPhotoID")> _
        Public Property AuditParameterPhoto() As AuditParameterPhoto
            Get
                Try
                    
                    If Not IsNothing(Me._auditParameterPhoto) AndAlso (Not Me._auditParameterPhoto.IsLoaded) Then

                        Me._auditParameterPhoto = CType(DoLoad(GetType(AuditParameterPhoto).ToString(), _auditParameterPhoto.ID), AuditParameterPhoto)
                        Me._auditParameterPhoto.MarkLoaded()

                    End If

                    Return Me._auditParameterPhoto

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As AuditParameterPhoto)

                Me._auditParameterPhoto = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._auditParameterPhoto.MarkLoaded()
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

