
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DealerPajak Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 9/27/2012 - 4:46:02 PM
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
    <Serializable(), TableInfo("DealerPajak")> _
    Public Class DealerPajak
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
        Private _nPWP As String = String.Empty
        Private _kPP As String = String.Empty
        Private _pejabat1 As String = String.Empty
        Private _jabatan1 As String = String.Empty
        Private _pejabat2 As String = String.Empty
        Private _jabatan2 As String = String.Empty
        Private _pejabat3 As String = String.Empty
        Private _jabatan3 As String = String.Empty
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


        <ColumnInfo("NPWP", "'{0}'")> _
        Public Property NPWP() As String
            Get
                Return _nPWP
            End Get
            Set(ByVal value As String)
                _nPWP = value
            End Set
        End Property


        <ColumnInfo("KPP", "'{0}'")> _
        Public Property KPP() As String
            Get
                Return _kPP
            End Get
            Set(ByVal value As String)
                _kPP = value
            End Set
        End Property


        <ColumnInfo("Pejabat1", "'{0}'")> _
        Public Property Pejabat1() As String
            Get
                Return _pejabat1
            End Get
            Set(ByVal value As String)
                _pejabat1 = value
            End Set
        End Property


        <ColumnInfo("Jabatan1", "'{0}'")> _
        Public Property Jabatan1() As String
            Get
                Return _jabatan1
            End Get
            Set(ByVal value As String)
                _jabatan1 = value
            End Set
        End Property


        <ColumnInfo("Pejabat2", "'{0}'")> _
        Public Property Pejabat2() As String
            Get
                Return _pejabat2
            End Get
            Set(ByVal value As String)
                _pejabat2 = value
            End Set
        End Property


        <ColumnInfo("Jabatan2", "'{0}'")> _
        Public Property Jabatan2() As String
            Get
                Return _jabatan2
            End Get
            Set(ByVal value As String)
                _jabatan2 = value
            End Set
        End Property


        <ColumnInfo("Pejabat3", "'{0}'")> _
        Public Property Pejabat3() As String
            Get
                Return _pejabat3
            End Get
            Set(ByVal value As String)
                _pejabat3 = value
            End Set
        End Property


        <ColumnInfo("Jabatan3", "'{0}'")> _
        Public Property Jabatan3() As String
            Get
                Return _jabatan3
            End Get
            Set(ByVal value As String)
                _jabatan3 = value
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
        RelationInfo("Dealer", "ID", "DealerPajak", "DealerID")> _
        Public Property Dealer() As Dealer
            Get
                Try
                    If Not isnothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

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
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._dealer.MarkLoaded()
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

