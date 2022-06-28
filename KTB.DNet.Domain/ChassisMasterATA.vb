
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ChassisMasterATA Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 08/11/2019 - 9:17:51
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
    <Serializable(), TableInfo("ChassisMasterATA")> _
    Public Class ChassisMasterATA
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
        Private _eTA As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _aTA As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _remarkATA As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _pOHeader As POHeader
        'Private _pOHeaderID As Integer
        Private _chassisMaster As ChassisMaster
        'Private _chassisMasterID As Integer



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


        <ColumnInfo("ETA", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ETA As DateTime
            Get
                Return _eTA
            End Get
            Set(ByVal value As DateTime)
                _eTA = value
            End Set
        End Property


        <ColumnInfo("ATA", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ATA As DateTime
            Get
                Return _aTA
            End Get
            Set(ByVal value As DateTime)
                _aTA = value
            End Set
        End Property


        <ColumnInfo("RemarkATA", "'{0}'")> _
        Public Property RemarkATA As String
            Get
                Return _RemarkATA
            End Get
            Set(ByVal value As String)
                _RemarkATA = value
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

        <ColumnInfo("ChassisMasterID", "{0}"), _
     RelationInfo("ChassisMaster", "ID", "ChassisMasterATA", "ChassisMasterID")> _
        Public Property ChassisMaster As ChassisMaster
            Get
                Try
                    If Not IsNothing(Me._chassisMaster) AndAlso (Not Me._chassisMaster.IsLoaded) Then

                        Me._chassisMaster = CType(DoLoad(GetType(ChassisMaster).ToString(), _chassisMaster.ID), ChassisMaster)
                        Me._chassisMaster.MarkLoaded()

                    End If

                    Return Me._chassisMaster

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As ChassisMaster)

                Me._chassisMaster = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._chassisMaster.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("POHeaderID", "{0}"), _
      RelationInfo("POHeader", "ID", "ChassisMasterATA", "POHeaderID")> _
        Public Property POHeader As POHeader
            Get
                Try
                    If Not IsNothing(Me._pOHeader) AndAlso (Not Me._pOHeader.IsLoaded) Then

                        Me._pOHeader = CType(DoLoad(GetType(POHeader).ToString(), _pOHeader.ID), POHeader)
                        Me._pOHeader.MarkLoaded()

                    End If

                    Return Me._pOHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As POHeader)

                Me._pOHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._pOHeader.MarkLoaded()
                End If
            End Set
        End Property

        '    <ColumnInfo("POHeaderID", "{0}")> _
        '    Public Property POHeaderID As Integer

        '        Get
        'return _pOHeaderID}
        '        End Get
        '        Set(ByVal value As Integer)
        '            _pOHeaderID = value
        '        End Set
        '    End Property
        '    <ColumnInfo("ChassisMasterID", "{0}")> _
        '    Public Property ChassisMasterID As Integer

        '        Get
        'return _chassisMasterID}
        '        End Get
        '        Set(ByVal value As Integer)
        '            _chassisMasterID = value
        '        End Set
        '    End Property


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

