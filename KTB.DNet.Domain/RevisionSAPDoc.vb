
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : RevisionSAPDoc Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 9/5/2018 - 9:55:12 AM
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
    <Serializable(), TableInfo("RevisionSAPDoc")> _
    Public Class RevisionSAPDoc
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
        Private _debitChargeNo As String = String.Empty
        Private _dCAmount As Decimal
        Private _debitMemoNo As String = String.Empty
        Private _dMAmount As Decimal
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


        <ColumnInfo("DebitChargeNo", "'{0}'")> _
        Public Property DebitChargeNo As String
            Get
                Return _debitChargeNo
            End Get
            Set(ByVal value As String)
                _debitChargeNo = value
            End Set
        End Property


        <ColumnInfo("DCAmount", "{0}")> _
        Public Property DCAmount As Decimal
            Get
                Return _dCAmount
            End Get
            Set(ByVal value As Decimal)
                _dCAmount = value
            End Set
        End Property


        <ColumnInfo("DebitMemoNo", "'{0}'")> _
        Public Property DebitMemoNo As String
            Get
                Return _debitMemoNo
            End Get
            Set(ByVal value As String)
                _debitMemoNo = value
            End Set
        End Property


        <ColumnInfo("DMAmount", "{0}")> _
        Public Property DMAmount As Decimal
            Get
                Return _dMAmount
            End Get
            Set(ByVal value As Decimal)
                _dMAmount = value
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

        <ColumnInfo("RevisionFakturID", "{0}"), _
        RelationInfo("RevisionFaktur", "ID", "RevisionSAPDoc", "RevisionFakturID")> _
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

