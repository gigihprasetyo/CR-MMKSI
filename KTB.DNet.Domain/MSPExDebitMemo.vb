
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MSPExDebitMemo Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 11/11/2020 - 9:42:40 AM
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
    <Serializable(), TableInfo("MSPExDebitMemo")> _
    Public Class MSPExDebitMemo
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
        Private _debitMemoNo As String = String.Empty
        Private _amount As Decimal
        Private _docType As String = String.Empty
        Private _documentDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _fileName As String = String.Empty
        Private _rowstatus As Short
        Private _createdby As String = String.Empty
        Private _createdtime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdatedby As String = String.Empty
        Private _lastUpdatedtime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _mSPExRegistration As MSPExRegistration



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


        <ColumnInfo("DebitMemoNo", "'{0}'")> _
        Public Property DebitMemoNo As String
            Get
                Return _debitMemoNo
            End Get
            Set(ByVal value As String)
                _debitMemoNo = value
            End Set
        End Property


        <ColumnInfo("Amount", "{0}")> _
        Public Property Amount As Decimal
            Get
                Return _amount
            End Get
            Set(ByVal value As Decimal)
                _amount = value
            End Set
        End Property


        <ColumnInfo("DocType", "'{0}'")> _
        Public Property DocType As String
            Get
                Return _docType
            End Get
            Set(ByVal value As String)
                _docType = value
            End Set
        End Property


        <ColumnInfo("DocumentDate", "'{0:yyyy/MM/dd}'")> _
        Public Property DocumentDate As DateTime
            Get
                Return _documentDate
            End Get
            Set(ByVal value As DateTime)
                _documentDate = New DateTime(value.Year, value.Month, value.Day)
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


        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus As Short
            Get
                Return _rowstatus
            End Get
            Set(ByVal value As Short)
                _rowstatus = value
            End Set
        End Property


        <ColumnInfo("Createdby", "'{0}'")> _
        Public Property Createdby As String
            Get
                Return _createdby
            End Get
            Set(ByVal value As String)
                _createdby = value
            End Set
        End Property


        <ColumnInfo("Createdtime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property Createdtime As DateTime
            Get
                Return _createdtime
            End Get
            Set(ByVal value As DateTime)
                _createdtime = value
            End Set
        End Property


        <ColumnInfo("LastUpdatedby", "'{0}'")> _
        Public Property LastUpdatedby As String
            Get
                Return _lastUpdatedby
            End Get
            Set(ByVal value As String)
                _lastUpdatedby = value
            End Set
        End Property


        <ColumnInfo("LastUpdatedtime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdatedtime As DateTime
            Get
                Return _lastUpdatedtime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdatedtime = value
            End Set
        End Property


        <ColumnInfo("MSPExRegistrationID", "{0}"), _
        RelationInfo("MSPExRegistration", "ID", "MSPExDebitMemo", "MSPExRegistrationID")> _
        Public Property MSPExRegistration() As MSPExRegistration
            Get
                Try
                    If Not IsNothing(Me._mSPExRegistration) AndAlso (Not Me._mSPExRegistration.IsLoaded) Then

                        Me._mSPExRegistration = CType(DoLoad(GetType(MSPExRegistration).ToString(), _mSPExRegistration.ID), MSPExRegistration)
                        Me._mSPExRegistration.MarkLoaded()

                    End If

                    Return Me._mSPExRegistration

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As MSPExRegistration)

                Me._mSPExRegistration = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._mSPExRegistration.MarkLoaded()
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

