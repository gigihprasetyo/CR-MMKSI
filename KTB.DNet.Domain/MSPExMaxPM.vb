#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MSPExMaxPM Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 6/4/2021 - 9:05:19 AM
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
    <Serializable(), TableInfo("MSPExMaxPM")> _
    Public Class MSPExMaxPM
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
        Private _mSPExTypeID As Integer
        Private _vechileModelID As Integer
        Private _templateFileName As String
        Private _maxPM As Integer
        Private _rowstatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _mSPExType As MSPExType
        Private _vechileModel As VechileModel


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


        <ColumnInfo("MSPExTypeID", "{0}")> _
        Public Property MSPExTypeID As Integer
            Get
                Return _mSPExTypeID
            End Get
            Set(ByVal value As Integer)
                _mSPExTypeID = value
            End Set
        End Property


        <ColumnInfo("VechileModelID", "{0}")> _
        Public Property VechileModelID As Integer
            Get
                Return _vechileModelID
            End Get
            Set(ByVal value As Integer)
                _vechileModelID = value
            End Set
        End Property


        <ColumnInfo("MaxPM", "{0}")> _
        Public Property MaxPM As Integer
            Get
                Return _maxPM
            End Get
            Set(ByVal value As Integer)
                _maxPM = value
            End Set
        End Property


        <ColumnInfo("TemplateFileName", "'{0}'")> _
        Public Property TemplateFileName As String
            Get
                Return _templateFileName
            End Get
            Set(ByVal value As String)
                _templateFileName = value
            End Set
        End Property


        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus As Short
            Get
                Return _rowstatus
            End Get
            Set(ByVal value As Short)
                _rowstatus = value
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


        <ColumnInfo("MSPExTypeID", "{0}"), _
        RelationInfo("MSPExType", "ID", "MSPExMaxPM", "MSPExTypeID")> _
        Public Property MSPExType As MSPExType
            Get
                Try
                    If Not IsNothing(Me._mSPExType) AndAlso (Not Me._mSPExType.IsLoaded) Then

                        Me._mSPExType = CType(DoLoad(GetType(MSPExType).ToString(), _mSPExType.ID), MSPExType)
                        Me._mSPExType.MarkLoaded()

                    End If

                    Return Me._mSPExType

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As MSPExType)

                Me._mSPExType = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._mSPExType.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("VechileModelID", "{0}"), _
        RelationInfo("VechileModel", "ID", "MSPExMaxPM", "VechileModelID")> _
        Public Property VechileModel As VechileModel
            Get
                Try
                    If Not IsNothing(Me._vechileModel) AndAlso (Not Me._vechileModel.IsLoaded) Then

                        Me._vechileModel = CType(DoLoad(GetType(VechileModel).ToString(), _vechileModel.ID), VechileModel)
                        Me._vechileModel.MarkLoaded()

                    End If

                    Return Me._vechileModel

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As VechileModel)

                Me._vechileModel = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._vechileModel.MarkLoaded()
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
