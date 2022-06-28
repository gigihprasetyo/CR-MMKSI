
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PhisingGuardImage Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 1/9/2007 - 10:47:51 AM
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
    <Serializable(), TableInfo("PhisingGuardImage")> _
    Public Class PhisingGuardImage
        Inherits DomainObject

#Region "Public Constants"
        Public Const MAX_PHOTO_SIZE As Integer = 30720
        Public Const VALID_IMAGE_TYPE As String = "IMAGE"
#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Integer)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"

        Private _iD As Integer
        Private _imageCode As String = String.Empty
        Private _image As Byte()
        Private _uploadedUserID As Integer
        Private _type As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)



        Private _userProfile As UserProfile = New UserProfile(0)

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


        <ColumnInfo("ImageCode", "'{0}'")> _
        Public Property ImageCode() As String
            Get
                Return _imageCode
            End Get
            Set(ByVal value As String)
                _imageCode = value
            End Set
        End Property


        <ColumnInfo("Image", "{0}")> _
        Public Property Image() As Byte()
            Get
                Return _image
            End Get
            Set(ByVal value As Byte())
                _image = value
            End Set
        End Property


        <ColumnInfo("UploadedUserID", "{0}")> _
        Public Property UploadedUserID() As Integer
            Get
                Return _uploadedUserID
            End Get
            Set(ByVal value As Integer)
                _uploadedUserID = value
            End Set
        End Property


        <ColumnInfo("Type", "{0}")> _
        Public Property Type() As Short
            Get
                Return _type
            End Get
            Set(ByVal value As Short)
                _type = value
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

        <ColumnInfo("ID", "{0}"), _
        RelationInfo("PhisingGuardImage", "ID", "UserProfile", "ImageID")> _
        Public ReadOnly Property UserProfile() As UserProfile
            Get
                Try
                    If Not isnothing(Me._userProfile) AndAlso (Not Me._userProfile.IsLoaded) Then
                        Dim _criteria As Criteria = New Criteria(GetType(UserProfile), "ImageID", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(UserProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Dim tempColl As ArrayList = DoLoadArray(GetType(UserProfile).ToString, criterias)

                        If (tempColl.Count > 0) Then
                            Me._userProfile = CType(tempColl(0), UserProfile)
                        Else
                            Me._userProfile = Nothing
                        End If
                    End If

                    Return Me._userProfile

                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get
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

