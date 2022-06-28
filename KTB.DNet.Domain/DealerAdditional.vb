#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DealerAdditional Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 22/08/2007 - 16:06:27
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
    <Serializable(), TableInfo("DealerAdditional")> _
    Public Class DealerAdditional
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
        Private _claimETA As Integer
        Private _showroomFile As String = String.Empty
        Private _stuctureFile As String = String.Empty
        Private _salesForceFile As String = String.Empty
        Private _classification As String = String.Empty
        Private _heldYear As Integer
        Private _sparepartGrade As String = String.Empty
        Private _equipmentClass As Short
        Private _dealerFacility As Short
        Private _dealerStallEquipment As Short
        Private _serviceGrade As Short
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


        <ColumnInfo("ClaimETA", "{0}")> _
        Public Property ClaimETA() As Integer
            Get
                Return _claimETA
            End Get
            Set(ByVal value As Integer)
                _claimETA = value
            End Set
        End Property


        <ColumnInfo("ShowroomFile", "'{0}'")> _
        Public Property ShowroomFile() As String
            Get
                Return _showroomFile
            End Get
            Set(ByVal value As String)
                _showroomFile = value
            End Set
        End Property


        <ColumnInfo("StuctureFile", "'{0}'")> _
        Public Property StuctureFile() As String
            Get
                Return _stuctureFile
            End Get
            Set(ByVal value As String)
                _stuctureFile = value
            End Set
        End Property


        <ColumnInfo("SalesForceFile", "'{0}'")> _
        Public Property SalesForceFile() As String
            Get
                Return _salesForceFile
            End Get
            Set(ByVal value As String)
                _salesForceFile = value
            End Set
        End Property


        <ColumnInfo("Classification", "'{0}'")> _
        Public Property Classification() As String
            Get
                Return _classification
            End Get
            Set(ByVal value As String)
                _classification = value
            End Set
        End Property


        <ColumnInfo("HeldYear", "{0}")> _
        Public Property HeldYear() As Integer
            Get
                Return _heldYear
            End Get
            Set(ByVal value As Integer)
                _heldYear = value
            End Set
        End Property


        <ColumnInfo("SparePartGrade", "'{0}'")> _
        Public Property SparePartGrade() As String
            Get
                Return _sparepartGrade
            End Get
            Set(ByVal value As String)
                _sparepartGrade = value
            End Set
        End Property

        

        <ColumnInfo("EqupmentClass", "{0}")> _
        Public Property EquipmentClass() As Short
            Get
                Return _equipmentClass
            End Get
            Set(ByVal value As Short)
                _equipmentClass = value
            End Set
        End Property

        <ColumnInfo("DealerFacility", "{0}")> _
        Public Property DealerFacility() As Short
            Get
                Return _dealerFacility
            End Get
            Set(ByVal value As Short)
                _dealerFacility = value
            End Set
        End Property

        <ColumnInfo("DealerStallEquipment", "{0}")> _
        Public Property DealerStallEquipment() As Short
            Get
                Return _dealerStallEquipment
            End Get
            Set(ByVal value As Short)
                _dealerStallEquipment = value
            End Set
        End Property

        <ColumnInfo("ServiceGrade", "{0}")> _
        Public Property ServiceGrade() As Short
            Get
                Return _serviceGrade
            End Get
            Set(ByVal value As Short)
                _serviceGrade = value
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
        RelationInfo("Dealer", "ID", "DealerAdditional", "DealerID")> _
        Public Property Dealer() As Dealer
            Get
                Try
                    If Not IsNothing(Me._dealer) AndAlso (Not Me._dealer.IsLoaded) Then

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
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
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

