
#region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SpClaimDocument Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 24/03/2020 - 10:16:02
'//
'// ===========================================================================	
#end region

#region ".NET Base Class Namespace Imports"
imports System
imports System.Collections
#end region

#region "Custom Namespace Imports"
imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Domain.Search
#end region

namespace KTB.DNet.Domain
    <Serializable(), TableInfo("SpClaimDocument")> _
    Public Class SpClaimDocument
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
        Private _fileName As String = String.Empty
        Private _filePath As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _AttachmentData As System.Web.HttpPostedFile

        Private _claimHeader As ClaimHeader
        Private _sPSupportClaimDoc As SPSupportClaimDoc


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


        <ColumnInfo("FileName", "'{0}'")> _
        Public Property FileName As String
            Get
                Return _fileName
            End Get
            Set(ByVal value As String)
                _fileName = value
            End Set
        End Property

        <ColumnInfo("FilePath", "'{0}'")> _
        Public Property FilePath As String
            Get
                Return _filePath
            End Get
            Set(ByVal value As String)
                _filePath = value
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


        <ColumnInfo("ClaimHeaderID", "{0}"), _
        RelationInfo("ClaimHeader", "ID", "SpClaimDocument", "ClaimHeaderID")> _
        Public Property ClaimHeader() As ClaimHeader
            Get
                Try
                    If Not IsNothing(Me._claimHeader) AndAlso (Not Me._claimHeader.IsLoaded) Then

                        Me._claimHeader = CType(DoLoad(GetType(ClaimHeader).ToString(), _claimHeader.ID), ClaimHeader)
                        Me._claimHeader.MarkLoaded()

                    End If

                    Return Me._claimHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ClaimHeader)

                Me._claimHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._claimHeader.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("SPSupportClaimDocID", "{0}"), _
        RelationInfo("SPSupportClaimDoc", "ID", "SpClaimDocument", "SPSupportClaimDocID")> _
        Public Property SPSupportClaimDoc() As SPSupportClaimDoc
            Get
                Try
                    If Not IsNothing(Me._sPSupportClaimDoc) AndAlso (Not Me._sPSupportClaimDoc.IsLoaded) Then

                        Me._sPSupportClaimDoc = CType(DoLoad(GetType(SPSupportClaimDoc).ToString(), _sPSupportClaimDoc.ID), SPSupportClaimDoc)
                        Me._sPSupportClaimDoc.MarkLoaded()

                    End If

                    Return Me._sPSupportClaimDoc

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SPSupportClaimDoc)

                Me._sPSupportClaimDoc = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sPSupportClaimDoc.MarkLoaded()
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
        Public Property AttachmentData() As System.Web.HttpPostedFile
            Get
                Return _AttachmentData
            End Get

            Set(ByVal value As System.Web.HttpPostedFile)
                _AttachmentData = value
            End Set
        End Property
#End Region

    End Class
end namespace

