
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : FSCampaignVehicle Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2010 
'// ---------------------
'// $History      : $
'// Generated on 9/3/2010 - 2:17:44 PM
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
    <Serializable(), TableInfo("FSCampaignVehicle")> _
    Public Class FSCampaignVehicle
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
        Private _rowStatus As Byte
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _createdBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty

        Private _fSCampaign As FSCampaign
        Private _vechileType As VechileType



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


        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus() As Byte
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Byte)
                _rowStatus = value
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


        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy() As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
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


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy() As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
            End Set
        End Property


        <ColumnInfo("CampaignID", "{0}"), _
        RelationInfo("FSCampaign", "ID", "FSCampaignVehicle", "CampaignID")> _
        Public Property FSCampaign() As FSCampaign
            Get
                Try
                    If Not isnothing(Me._fSCampaign) AndAlso (Not Me._fSCampaign.IsLoaded) Then

                        Me._fSCampaign = CType(DoLoad(GetType(FSCampaign).ToString(), _fSCampaign.ID), FSCampaign)
                        Me._fSCampaign.MarkLoaded()

                    End If

                    Return Me._fSCampaign

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As FSCampaign)

                Me._fSCampaign = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._fSCampaign.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("VehicleTypeID", "{0}"), _
        RelationInfo("VechileType", "ID", "FSCampaignVehicle", "VehicleTypeID")> _
        Public Property VechileType() As VechileType
            Get
                Try
                    If Not isnothing(Me._vechileType) AndAlso (Not Me._vechileType.IsLoaded) Then

                        Me._vechileType = CType(DoLoad(GetType(VechileType).ToString(), _vechileType.ID), VechileType)
                        Me._vechileType.MarkLoaded()

                    End If

                    Return Me._vechileType

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As VechileType)

                Me._vechileType = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._vechileType.MarkLoaded()
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
