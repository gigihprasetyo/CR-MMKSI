#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Noviar Sbastian
'// PURPOSE       : BusinessArea Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 9/29/2005 - 6:52:37 PM
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
    <Serializable(), TableInfo("BusinessArea")> _
    Public Class BusinessArea
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
        Private _kind As String = String.Empty
        Private _title As String = String.Empty
        Private _contactPerson As String = String.Empty
        Private _email As String = String.Empty
        Private _phone As String = String.Empty
        Private _fax As String = String.Empty
        Private _hP As String = String.Empty
        Private _position As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _dealer As Dealer



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


        <ColumnInfo("Kind", "'{0}'")> _
        Public Property Kind() As String
            Get
                Return _kind
            End Get
            Set(ByVal value As String)
                _kind = value
            End Set
        End Property


        <ColumnInfo("Title", "'{0}'")> _
        Public Property Title() As String
            Get
                Return _title
            End Get
            Set(ByVal value As String)
                _title = value
            End Set
        End Property


        <ColumnInfo("ContactPerson", "'{0}'")> _
        Public Property ContactPerson() As String
            Get
                Return _contactPerson
            End Get
            Set(ByVal value As String)
                _contactPerson = value
            End Set
        End Property


        <ColumnInfo("Email", "'{0}'")> _
        Public Property Email() As String
            Get
                Return _email
            End Get
            Set(ByVal value As String)
                _email = value
            End Set
        End Property


        <ColumnInfo("Phone", "'{0}'")> _
        Public Property Phone() As String
            Get
                Return _phone
            End Get
            Set(ByVal value As String)
                _phone = value
            End Set
        End Property


        <ColumnInfo("Fax", "'{0}'")> _
        Public Property Fax() As String
            Get
                Return _fax
            End Get
            Set(ByVal value As String)
                _fax = value
            End Set
        End Property


        <ColumnInfo("HP", "'{0}'")> _
        Public Property HP() As String
            Get
                Return _hP
            End Get
            Set(ByVal value As String)
                _hP = value
            End Set
        End Property

        <ColumnInfo("Position", "{0}")> _
        Public Property Position() As Short
            Get
                Return _position
            End Get
            Set(ByVal value As Short)
                _position = value
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




        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "BusinessArea", "DealerID")> _
        Public Property Dealer() As Dealer
            Get
                Try
                    If Not IsNothing(Me._dealer) And (Not Me._dealer.IsLoaded) Then

                        Me._dealer = CType(DoLoad(GetType(Dealer).ToString(), _dealer.ID), Dealer)
                        Me._dealer.MarkLoaded()

                    End If

                    Return Me._dealer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Dealer)

                Me._dealer = value
                If (Not IsNothing(value)) And (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
                End If
            End Set
        End Property




#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

