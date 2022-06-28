
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : RevisionPaymentDetail Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 9/4/2018 - 9:09:52 AM
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
    <Serializable(), TableInfo("RevisionPaymentDetail")> _
    Public Class RevisionPaymentDetail
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
        Private _revisionFaktur As RevisionFaktur
        Private _revisionPaymentHeader As RevisionPaymentHeader
        Private _revisionSAPDoc As RevisionSAPDoc
        Private _isCancel As Short
        Private _cancelReason As String = String.Empty
        Private _chassisNumber As String = String.Empty
        Private _rowStatus As Short
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

        <ColumnInfo("IsCancel", "{0}")> _
        Public Property IsCancel As Short
            Get
                Return _isCancel
            End Get
            Set(ByVal value As Short)
                _isCancel = value
            End Set
        End Property


        <ColumnInfo("CancelReason", "'{0}'")> _
        Public Property CancelReason As String
            Get
                Return _cancelReason
            End Get
            Set(ByVal value As String)
                _cancelReason = value
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

        Public Property ChassisNumber() As String
            Get
                _chassisNumber = Me.RevisionFaktur.ChassisMaster.ChassisNumber
                Return _chassisNumber
            End Get
            Set(ByVal value As String)
                _chassisNumber = value
            End Set
        End Property

        <ColumnInfo("RevisionPaymentHeaderID", "{0}"), _
        RelationInfo("RevisionPaymentHeader", "ID", "RevisionPaymentDetail", "RevisionPaymentHeaderID")> _
        Public Property RevisionPaymentHeader() As RevisionPaymentHeader
            Get
                Try
                    If Not IsNothing(Me._revisionPaymentHeader) AndAlso (Not Me._revisionPaymentHeader.IsLoaded) Then
                        Me._revisionPaymentHeader = CType(DoLoad(GetType(RevisionPaymentHeader).ToString(), _revisionPaymentHeader.ID), RevisionPaymentHeader)
                        Me._revisionPaymentHeader.MarkLoaded()
                    End If
                    Return Me._revisionPaymentHeader

                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get

            Set(ByVal value As RevisionPaymentHeader)
                Me._revisionPaymentHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._revisionPaymentHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("RevisionFakturID", "{0}"), _
        RelationInfo("RevisionFaktur", "ID", "RevisionPaymentDetail", "RevisionFakturID")> _
        Public Property RevisionFaktur() As RevisionFaktur
            Get
                Try
                    If Not IsNothing(Me._revisionFaktur) AndAlso (Not Me._revisionFaktur.IsLoaded) Then
                        Me._revisionFaktur = CType(DoLoad(GetType(RevisionFaktur).ToString(), _revisionFaktur.ID), RevisionFaktur)
                        Me._revisionFaktur.MarkLoaded()
                    End If
                    Return Me._revisionFaktur

                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get

            Set(ByVal value As RevisionFaktur)
                Me._revisionFaktur = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._revisionFaktur.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("RevisionSAPDocID", "{0}"), _
        RelationInfo("RevisionSAPDoc", "ID", "RevisionPaymentDetail", "RevisionSAPDocID")> _
        Public Property RevisionSAPDoc() As RevisionSAPDoc
            Get
                Try
                    If Not IsNothing(Me._revisionSAPDoc) AndAlso (Not Me._revisionSAPDoc.IsLoaded) Then
                        Me._revisionSAPDoc = CType(DoLoad(GetType(RevisionSAPDoc).ToString(), _revisionSAPDoc.ID), RevisionSAPDoc)
                        Me._revisionSAPDoc.MarkLoaded()
                    End If
                    Return Me._revisionSAPDoc

                Catch ex As Exception
                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")
                    If rethrow Then
                        Throw
                    End If
                End Try
                Return Nothing
            End Get

            Set(ByVal value As RevisionSAPDoc)
                Me._revisionSAPDoc = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._revisionSAPDoc.MarkLoaded()
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

