#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DetailBOM Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/08/2005 - 1:33:07 PM
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
    <Serializable(), TableInfo("DetailBOM")> _
    Public Class DetailBOM
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
        Private _quantity As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _headerBOM As HeaderBOM
        Private _equipmentMaster As EquipmentMaster



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


        <ColumnInfo("Quantity", "{0}")> _
        Public Property Quantity() As Integer
            Get
                Return _quantity
            End Get
            Set(ByVal value As Integer)
                _quantity = value
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




        <ColumnInfo("HeaderBOMID", "{0}"), _
        RelationInfo("HeaderBOM", "ID", "DetailBOM", "HeaderBOMID")> _
        Public Property HeaderBOM() As HeaderBOM
            Get
                Try
                    If Not isnothing(Me._headerBOM) AndAlso (Not Me._headerBOM.IsLoaded) Then

                        Me._headerBOM = CType(DoLoad(GetType(HeaderBOM).ToString(), _headerBOM.ID), HeaderBOM)
                        Me._headerBOM.MarkLoaded()

                    End If

                    Return Me._headerBOM

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As HeaderBOM)

                Me._headerBOM = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._headerBOM.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("EquipmentMasterID", "{0}"), _
        RelationInfo("EquipmentMaster", "ID", "DetailBOM", "EquipmentMasterID")> _
        Public Property EquipmentMaster() As EquipmentMaster
            Get
                Try
                    If Not isnothing(Me._equipmentMaster) AndAlso (Not Me._equipmentMaster.IsLoaded) Then

                        Me._equipmentMaster = CType(DoLoad(GetType(EquipmentMaster).ToString(), _equipmentMaster.ID), EquipmentMaster)
                        Me._equipmentMaster.MarkLoaded()

                    End If

                    Return Me._equipmentMaster

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As EquipmentMaster)

                Me._equipmentMaster = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._equipmentMaster.MarkLoaded()
                End If
            End Set
        End Property




#End Region

#Region "Custom Method"
        ReadOnly Property EquipmentNumber() As String
            Get
                If Not Me.EquipmentMaster Is Nothing Then
                    Return Me.EquipmentMaster.EquipmentNumber
                Else
                    Return "Invalid Equipment Number"
                End If
            End Get
        End Property

        ReadOnly Property EquipmentDescription() As String
            Get
                If Not Me.EquipmentMaster Is Nothing Then
                    Return Me.EquipmentMaster.Description
                Else
                    Return ""
                End If
            End Get
        End Property

#End Region

    End Class
End Namespace

