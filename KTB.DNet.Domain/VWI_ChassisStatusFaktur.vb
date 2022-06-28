
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_ChassisStatusFaktur Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 06/03/2018 - 16:23:12
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
    <Serializable(), TableInfo("VWI_ChassisStatusFaktur")>
    Public Class VWI_ChassisStatusFaktur
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
        Private _chassisNumber As String = String.Empty
        Private _dealerCode As String = String.Empty
        Private _dealerName As String = String.Empty
        Private _revisionStatus As String = String.Empty
        Private _revisionDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _revisionType As String = String.Empty
        Private _sPKNumber As String = String.Empty
        Private _dealerSPKNumber As String = String.Empty
        Private _fakturNumber As String = String.Empty
        Private _fakturDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _openFakturDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _validateDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _confirmDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _downloadDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _printedDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _fakturStatus As String = String.Empty
        Private _eTDDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _effectiveDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _releaseDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _aTDDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _eTADate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _aTADate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")>
        Public Property ID As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property

        <ColumnInfo("ChassisNumber", "{0}")>
        Public Property ChassisNumber As String
            Get
                Return _chassisNumber
            End Get
            Set(ByVal value As String)
                _chassisNumber = value
            End Set
        End Property

        <ColumnInfo("DealerCode", "'{0}'")>
        Public Property DealerCode As String
            Get
                Return _dealerCode
            End Get
            Set(ByVal value As String)
                _dealerCode = value
            End Set
        End Property

        <ColumnInfo("DealerName", "'{0}'")>
        Public Property DealerName As String
            Get
                Return _dealerName

            End Get
            Set(ByVal value As String)
                _dealerName = value
            End Set
        End Property

        <ColumnInfo("RevisionStatus", "'{0}'")>
        Public Property RevisionStatus As String
            Get
                Return _revisionStatus
            End Get
            Set(ByVal value As String)
                _revisionStatus = value
            End Set
        End Property

        <ColumnInfo("RevisionDate", "'{0:yyyy/MM/dd}'")>
        Public Property RevisionDate As DateTime
            Get
                Return _revisionDate
            End Get
            Set(ByVal value As DateTime)
                _revisionDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("RevisionType", "'{0}'")>
        Public Property RevisionType As String
            Get
                Return _revisionType
            End Get
            Set(ByVal value As String)
                _revisionType = value
            End Set
        End Property

        <ColumnInfo("SPKNumber", "'{0}'")>
        Public Property SPKNumber As String
            Get
                Return _sPKNumber
            End Get
            Set(ByVal value As String)
                _sPKNumber = value
            End Set
        End Property

        <ColumnInfo("DealerSPKNumber", "'{0}'")>
        Public Property DealerSPKNumber As String
            Get
                Return _dealerSPKNumber
            End Get
            Set(ByVal value As String)
                _dealerSPKNumber = value
            End Set
        End Property

        <ColumnInfo("FakturNumber", "'{0}'")>
        Public Property FakturNumber As String
            Get
                Return _fakturNumber
            End Get
            Set(ByVal value As String)
                _fakturNumber = value
            End Set
        End Property

        <ColumnInfo("FakturDate", "'{0:yyyy/MM/dd HH:mm:ss}'")>
        Public Property FakturDate As DateTime
            Get
                Return _fakturDate
            End Get
            Set(ByVal value As DateTime)
                _fakturDate = value
            End Set
        End Property

        <ColumnInfo("OpenFakturDate", "'{0:yyyy/MM/dd}'")>
        Public Property OpenFakturDate As DateTime
            Get
                Return _openFakturDate
            End Get
            Set(ByVal value As DateTime)
                _openFakturDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("ValidateDate", "'{0:yyyy/MM/dd HH:mm:ss}'")>
        Public Property ValidateDate As DateTime
            Get
                Return _validateDate
            End Get
            Set(ByVal value As DateTime)
                _validateDate = value
            End Set
        End Property

        <ColumnInfo("ConfirmDate", "'{0:yyyy/MM/dd HH:mm:ss}'")>
        Public Property ConfirmDate As DateTime
            Get
                Return _confirmDate
            End Get
            Set(ByVal value As DateTime)
                _confirmDate = value
            End Set
        End Property

        <ColumnInfo("DownloadDate", "'{0:yyyy/MM/dd HH:mm:ss}'")>
        Public Property DownloadDate As DateTime
            Get
                Return _downloadDate
            End Get
            Set(ByVal value As DateTime)
                _downloadDate = value
            End Set
        End Property

        <ColumnInfo("PrintedDate", "'{0:yyyy/MM/dd HH:mm:ss}'")>
        Public Property PrintedDate As DateTime
            Get
                Return _printedDate
            End Get
            Set(ByVal value As DateTime)
                _printedDate = value
            End Set
        End Property

        <ColumnInfo("FakturStatus", "'{0}'")>
        Public Property FakturStatus As String
            Get
                Return _fakturStatus
            End Get
            Set(ByVal value As String)
                _fakturStatus = value
            End Set
        End Property

        <ColumnInfo("ETDDate", "'{0:yyyy/MM/dd HH:mm:ss}'")>
        Public Property ETDDate As DateTime
            Get
                Return _eTDDate
            End Get
            Set(ByVal value As DateTime)
                _eTDDate = value
            End Set
        End Property

        <ColumnInfo("EffectiveDate", "'{0:yyyy/MM/dd HH:mm:ss}'")>
        Public Property EffectiveDate As DateTime
            Get
                Return _effectiveDate
            End Get
            Set(ByVal value As DateTime)
                _effectiveDate = value
            End Set
        End Property

        <ColumnInfo("ReleaseDate", "'{0:yyyy/MM/dd HH:mm:ss}'")>
        Public Property ReleaseDate As DateTime
            Get
                Return _releaseDate
            End Get
            Set(ByVal value As DateTime)
                _releaseDate = value
            End Set
        End Property

        <ColumnInfo("ATDDate", "'{0:yyyy/MM/dd HH:mm:ss}'")>
        Public Property ATDDate As DateTime
            Get
                Return _aTDDate
            End Get
            Set(ByVal value As DateTime)
                _aTDDate = value
            End Set
        End Property

        <ColumnInfo("ETADate", "'{0:yyyy/MM/dd HH:mm:ss}'")>
        Public Property ETADate As DateTime
            Get
                Return _eTADate
            End Get
            Set(ByVal value As DateTime)
                _eTADate = value
            End Set
        End Property

        <ColumnInfo("ATADate", "'{0:yyyy/MM/dd HH:mm:ss}'")>
        Public Property ATADate As DateTime
            Get
                Return _aTADate
            End Get
            Set(ByVal value As DateTime)
                _aTADate = value
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