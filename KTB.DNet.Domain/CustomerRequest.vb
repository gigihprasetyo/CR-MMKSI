#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CustomerRequest Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/26/2007 - 9:17:40 AM
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
    <Serializable(), TableInfo("CustomerRequest")> _
    Public Class CustomerRequest
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
        Private _requestNo As String = String.Empty
        Private _refRequestNo As String = String.Empty
        Private _requestType As String = String.Empty
        Private _requestUserID As Integer
        Private _requestDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _status As Integer
        Private _processUserID As String = String.Empty
        Private _processDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _customerCode As String = String.Empty
        Private _reffCode As String = String.Empty
        Private _name1 As String = String.Empty
        Private _name2 As String = String.Empty
        Private _name3 As String = String.Empty
        Private _alamat As String = String.Empty
        Private _kelurahan As String = String.Empty
        Private _kecamatan As String = String.Empty
        Private _postalCode As String = String.Empty
        Private _preArea As String = String.Empty
        Private _printRegion As String = String.Empty
        Private _phoneNo As String = String.Empty
        Private _email As String = String.Empty
        Private _attachment As String = String.Empty
        Private _status1 As Short

        Private _tipePerusahaan As Short
        'Private _companyGroupId As Short
        Private _mCPStatus As Short
        Private _lKPPStatus As Short

        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _cityID As Integer
        Private _dealer As Dealer
        Private _sPKHeader As SPKHeader

        Private _sPKDetailCustomer As SPKDetailCustomer


        Private _customerRequestProfiles As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _customerStatusHistory As System.Collections.ArrayList = New System.Collections.ArrayList
        Private _sPKHeaders As System.Collections.ArrayList = New System.Collections.ArrayList
        'CR SPK
        Private _typePerorangan As Integer
        Private _typeIdentitas As Integer
        Private _countryCode As String = String.Empty
        '

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


        <ColumnInfo("RequestNo", "'{0}'")> _
        Public Property RequestNo() As String
            Get
                Return _requestNo
            End Get
            Set(ByVal value As String)
                _requestNo = value
            End Set
        End Property


        <ColumnInfo("RefRequestNo", "'{0}'")> _
        Public Property RefRequestNo() As String
            Get
                Return _refRequestNo
            End Get
            Set(ByVal value As String)
                _refRequestNo = value
            End Set
        End Property


        <ColumnInfo("RequestType", "'{0}'")> _
        Public Property RequestType() As String
            Get
                Return _requestType
            End Get
            Set(ByVal value As String)
                _requestType = value
            End Set
        End Property


        <ColumnInfo("RequestUserID", "{0}")> _
        Public Property RequestUserID() As Integer
            Get
                Return _requestUserID
            End Get
            Set(ByVal value As Integer)
                _requestUserID = value
            End Set
        End Property


        <ColumnInfo("RequestDate", "'{0:yyyy/MM/dd}'")> _
        Public Property RequestDate() As DateTime
            Get
                Return _requestDate
            End Get
            Set(ByVal value As DateTime)
                _requestDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status() As Integer
            Get
                Return _status
            End Get
            Set(ByVal value As Integer)
                _status = value
            End Set
        End Property


        <ColumnInfo("ProcessUserID", "'{0}'")> _
        Public Property ProcessUserID() As String
            Get
                Return _processUserID
            End Get
            Set(ByVal value As String)
                _processUserID = value
            End Set
        End Property


        <ColumnInfo("ProcessDate", "'{0:yyyy/MM/dd}'")> _
        Public Property ProcessDate() As DateTime
            Get
                Return _processDate
            End Get
            Set(ByVal value As DateTime)
                _processDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("CustomerCode", "'{0}'")> _
        Public Property CustomerCode() As String
            Get
                Return _customerCode
            End Get
            Set(ByVal value As String)
                _customerCode = value
            End Set
        End Property


        <ColumnInfo("ReffCode", "'{0}'")> _
        Public Property ReffCode() As String
            Get
                Return _reffCode
            End Get
            Set(ByVal value As String)
                _reffCode = value
            End Set
        End Property


        <ColumnInfo("Name1", "'{0}'")> _
        Public Property Name1() As String
            Get
                Return _name1
            End Get
            Set(ByVal value As String)
                _name1 = value
            End Set
        End Property


        <ColumnInfo("Name2", "'{0}'")> _
        Public Property Name2() As String
            Get
                Return _name2
            End Get
            Set(ByVal value As String)
                _name2 = value
            End Set
        End Property


        <ColumnInfo("Name3", "'{0}'")> _
        Public Property Name3() As String
            Get
                Return _name3
            End Get
            Set(ByVal value As String)
                _name3 = value
            End Set
        End Property


        <ColumnInfo("Alamat", "'{0}'")> _
        Public Property Alamat() As String
            Get
                Return _alamat
            End Get
            Set(ByVal value As String)
                _alamat = value
            End Set
        End Property


        <ColumnInfo("Kelurahan", "'{0}'")> _
        Public Property Kelurahan() As String
            Get
                Return _kelurahan
            End Get
            Set(ByVal value As String)
                _kelurahan = value
            End Set
        End Property


        <ColumnInfo("Kecamatan", "'{0}'")> _
        Public Property Kecamatan() As String
            Get
                Return _kecamatan
            End Get
            Set(ByVal value As String)
                _kecamatan = value
            End Set
        End Property


        <ColumnInfo("PostalCode", "'{0}'")> _
        Public Property PostalCode() As String
            Get
                Return _postalCode
            End Get
            Set(ByVal value As String)
                _postalCode = value
            End Set
        End Property


        <ColumnInfo("PreArea", "'{0}'")> _
        Public Property PreArea() As String
            Get
                Return _preArea
            End Get
            Set(ByVal value As String)
                _preArea = value
            End Set
        End Property


        <ColumnInfo("PrintRegion", "'{0}'")> _
        Public Property PrintRegion() As String
            Get
                Return _printRegion
            End Get
            Set(ByVal value As String)
                _printRegion = value
            End Set
        End Property


        <ColumnInfo("PhoneNo", "'{0}'")> _
        Public Property PhoneNo() As String
            Get
                Return _phoneNo
            End Get
            Set(ByVal value As String)
                _phoneNo = value
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


        <ColumnInfo("Attachment", "'{0}'")> _
        Public Property Attachment() As String
            Get
                Return _attachment
            End Get
            Set(ByVal value As String)
                _attachment = value
            End Set
        End Property


        <ColumnInfo("Status1", "{0}")> _
        Public Property Status1() As Short
            Get
                Return _status1
            End Get
            Set(ByVal value As Short)
                _status1 = value
            End Set
        End Property

        <ColumnInfo("TipePerusahaan", "{0}")> _
        Public Property TipePerusahaan() As Short
            Get
                Return _tipePerusahaan
            End Get
            Set(ByVal value As Short)
                _tipePerusahaan = value
            End Set
        End Property

        '<ColumnInfo("CompanyGroupID", "{0}")> _
        'Public Property CompanyGroupID() As Short
        '    Get
        '        Return _companyGroupId
        '    End Get
        '    Set(ByVal value As Short)
        '        _companyGroupId = value
        '    End Set
        'End Property

        <ColumnInfo("MCPStatus", "{0}")> _
        Public Property MCPStatus() As Short
            Get
                Return _mCPStatus
            End Get
            Set(ByVal value As Short)
                _mCPStatus = value
            End Set
        End Property

        <ColumnInfo("LKPPStatus", "{0}")> _
        Public Property LKPPStatus() As Short
            Get
                Return _lKPPStatus
            End Get
            Set(ByVal value As Short)
                _lKPPStatus = value
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


        <ColumnInfo("CityID", "{0}")> _
        Public Property CityID() As Integer

            Get
                Return _cityID
            End Get
            Set(ByVal value As Integer)
                _cityID = value
            End Set
        End Property
        <ColumnInfo("DealerID", "{0}"), _
        RelationInfo("Dealer", "ID", "CustomerRequest", "DealerID")> _
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


        <RelationInfo("CustomerRequest", "ID", "CustomerRequestProfile", "CustomerRequestID")> _
        Public ReadOnly Property CustomerRequestProfiles() As System.Collections.ArrayList
            Get
                Try
                    If (Me._customerRequestProfiles.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(CustomerRequestProfile), "CustomerRequest", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(CustomerRequestProfile), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._customerRequestProfiles = DoLoadArray(GetType(CustomerRequestProfile).ToString, criterias)
                    End If

                    Return Me._customerRequestProfiles

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("CustomerRequest", "ID", "CustomerStatusHistory", "CustomerRequestID")> _
        Public ReadOnly Property CustomerStatusHistory() As System.Collections.ArrayList
            Get
                Try
                    If (Me._customerStatusHistory.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(CustomerStatusHistory), "CustomerRequest", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(CustomerStatusHistory), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._customerStatusHistory = DoLoadArray(GetType(CustomerStatusHistory).ToString, criterias)
                    End If

                    Return Me._customerStatusHistory

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("CustomerRequest", "ID", "SPKHeader", "CustomerRequestID")> _
        Public ReadOnly Property SPKHeaders() As System.Collections.ArrayList
            Get
                Try
                    If (Me._sPKHeaders.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(SPKHeader), "CustomerRequest", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SPKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._sPKHeaders = DoLoadArray(GetType(SPKHeader).ToString, criterias)
                    End If

                    Return Me._sPKHeaders

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <ColumnInfo("ID", "{0}"), _
        RelationInfo("CustomerRequest", "ID", "SPKHeader", "CustomerRequestID")> _
        Public Property SPKHeader() As SPKHeader
            Get
                Try
                    If IsNothing(Me._sPKHeader) Then

                        'Me._sPKHeader = CType(DoLoad(GetType(SPKHeader).ToString(), _sPKHeader.ID), SPKHeader)
                        'Me._sPKHeader.MarkLoaded()


                        Dim _criteria As Criteria = New Criteria(GetType(SPKHeader), "CustomerRequest.ID", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SPKHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Dim tempColl As ArrayList = DoLoadArray(GetType(SPKHeader).ToString, criterias)

                        If (tempColl.Count > 0) Then
                            Me._sPKHeader = CType(tempColl(0), SPKHeader)
                        Else
                            Me._sPKHeader = Nothing
                        End If

                    End If

                    Return Me._sPKHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SPKHeader)

                Me._sPKHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sPKHeader.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("ID", "{0}"), _
      RelationInfo("CustomerRequest", "ID", "SPKDetailCustomer", "CustomerRequestID")> _
        Public Property SPKDetailCustomer As SPKDetailCustomer
            Get
                Try
                    If IsNothing(Me._sPKDetailCustomer) Then

                        'Me._sPKHeader = CType(DoLoad(GetType(SPKHeader).ToString(), _sPKHeader.ID), SPKHeader)
                        'Me._sPKHeader.MarkLoaded()


                        Dim _criteria As Criteria = New Criteria(GetType(SPKDetailCustomer), "CustomerRequest.ID", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SPKDetailCustomer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Dim tempColl As ArrayList = DoLoadArray(GetType(SPKDetailCustomer).ToString, criterias)

                        If (tempColl.Count > 0) Then
                            Me._sPKDetailCustomer = CType(tempColl(0), SPKDetailCustomer)
                        Else
                            Me._sPKDetailCustomer = Nothing
                        End If

                    End If

                    Return Me._sPKDetailCustomer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As SPKDetailCustomer)

                Me._sPKDetailCustomer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._sPKDetailCustomer.MarkLoaded()
                End If
            End Set
        End Property
        'CR SPK
        <ColumnInfo("TypeIdentitas", "'{0}'")> _
        Public Property TypeIdentitas() As Integer
            Get
                Return _typeIdentitas
            End Get
            Set(ByVal value As Integer)
                _typeIdentitas = value
            End Set
        End Property


        <ColumnInfo("TypePerorangan", "{0}")> _
        Public Property TypePerorangan() As Integer
            Get
                Return _typePerorangan
            End Get
            Set(ByVal value As Integer)
                _typePerorangan = value
            End Set
        End Property

        <ColumnInfo("CountryCode", "{0}")> _
        Public Property CountryCode() As String
            Get
                Return _countryCode
            End Get
            Set(ByVal value As String)
                _countryCode = value
            End Set
        End Property
        'end CR SPK


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

        Private _IsChangedWSM As Boolean

        Public Property IsChangedWSM() As Boolean
            Get
                Return _IsChangedWSM
            End Get
            Set(ByVal value As Boolean)
                _IsChangedWSM = value
            End Set
        End Property

        Public Function GetCustomerRequestProfile(ByVal ProfileHeaderCode As String) As CustomerRequestProfile
            For Each oCRP As CustomerRequestProfile In Me.CustomerRequestProfiles
                If oCRP.ProfileHeader.Code.Trim.ToUpper = ProfileHeaderCode.Trim.ToUpper Then
                    Return oCRP
                End If
            Next
            Return Nothing
        End Function

        Public Sub UpdateCustomerRequestProfile(ByVal pCRP As CustomerRequestProfile)
            Dim arlResult As New ArrayList

            For Each oCRP As CustomerRequestProfile In Me._customerRequestProfiles
                If oCRP.ID = pCRP.ID Then
                    oCRP.ProfileValue = pCRP.ProfileValue
                End If
                arlResult.Add(oCRP)
            Next
            Me._customerRequestProfiles = arlResult
        End Sub

        Public Function GetSPKHeader(ByVal CustomerRequestID As Integer) As SPKHeader
            If Not IsNothing(Me.SPKHeaders) AndAlso Me.SPKHeaders.Count > 0 Then

            End If
            For Each oSPKh As SPKHeader In Me.SPKHeaders
                If oSPKh.CustomerRequestID = CustomerRequestID Then
                    Return oSPKh
                End If
            Next
            Return Nothing
        End Function

#End Region

    End Class
End Namespace

