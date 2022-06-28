#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EndCustomer Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/24/2005 - 4:04:03 PM
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
    <Serializable(), TableInfo("EndCustomer")> _
    Public Class EndCustomer
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
        Private _projectIndicator As String = String.Empty
        Private _refChassisNumberID As Integer
        Private _name1 As String = String.Empty
        Private _fakturDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _openFakturDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _fakturNumber As String = String.Empty
        Private _areaViolationFlag As String = String.Empty
        Private _areaViolationyAmount As Decimal
        Private _areaViolationBankName As String = String.Empty
        Private _areaViolationGyroNumber As String = String.Empty
        Private _penaltyFlag As String = String.Empty
        Private _penaltyAmount As Decimal
        Private _penaltyBankName As String = String.Empty
        Private _penaltyGyroNumber As String = String.Empty
        Private _referenceLetterFlag As String = String.Empty
        Private _referenceLetter As String = String.Empty
        Private _saveBy As String = String.Empty
        Private _saveTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _validateBy As String = String.Empty
        Private _validateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _confirmBy As String = String.Empty
        Private _confirmTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _downloadBy As String = String.Empty
        Private _downloadTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _printedBy As String = String.Empty
        Private _printedTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _cleansingCustomerID As Integer
        Private _mcpStatus As Short
        Private _remark1 As String = String.Empty
        Private _remark2 As String = String.Empty
        Private _handoverDate As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _isTemporary As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _customer As Customer
        Private _ownerAge As OwnerAge
        Private _mainUsage As MainUsage
        Private _vehicleBodyShape As VehicleBodyShape
        Private _vehiclePurpose As VehiclePurpose
        Private _mainOperationArea As MainOperationArea
        Private _vehicleOwnership As VehicleOwnership
        Private _customerBusiness As CustomerBusiness
        Private _paymentType As PaymentType
        Private _areaViolationPaymentMethodID As Integer
        Private _penaltyPaymentMethodID As Integer
        Private _city As City


        Private _chassisMaster As ChassisMaster = New ChassisMaster(0)
        Private _spkFaktur As SPKFaktur
        Private _mcpHeader As MCPHeader
        Private _lkppHeader As LKPPHeader
        Private _revisionFaktur As RevisionFaktur
        Private _revisionSpkFaktur As RevisionSPKFaktur

        Private _lKPPStatus As Short
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


        <ColumnInfo("ProjectIndicator", "'{0}'")> _
        Public Property ProjectIndicator() As String
            Get
                Return _projectIndicator
            End Get
            Set(ByVal value As String)
                _projectIndicator = value
            End Set
        End Property


        <ColumnInfo("RefChassisNumberID", "{0}")> _
        Public Property RefChassisNumberID() As Integer
            Get
                Return _refChassisNumberID
            End Get
            Set(ByVal value As Integer)
                _refChassisNumberID = value
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

        <ColumnInfo("FakturDate", "'{0:yyyy/MM/dd}'")> _
        Public Property FakturDate() As DateTime
            Get
                Return _fakturDate
            End Get
            Set(ByVal value As DateTime)
                _fakturDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("OpenFakturDate", "'{0:yyyy/MM/dd}'")> _
        Public Property OpenFakturDate() As DateTime
            Get
                Return _openFakturDate
            End Get
            Set(ByVal value As DateTime)
                _openFakturDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property


        <ColumnInfo("FakturNumber", "'{0}'")> _
        Public Property FakturNumber() As String
            Get
                Return _fakturNumber
            End Get
            Set(ByVal value As String)
                _fakturNumber = value
            End Set
        End Property

        <ColumnInfo("AreaViolationFlag", "'{0}'")> _
        Public Property AreaViolationFlag() As String
            Get
                Return _areaViolationFlag
            End Get
            Set(ByVal value As String)
                _areaViolationFlag = value
            End Set
        End Property


        <ColumnInfo("AreaViolationyAmount", "{0}")> _
        Public Property AreaViolationyAmount() As Decimal
            Get
                Return _areaViolationyAmount
            End Get
            Set(ByVal value As Decimal)
                _areaViolationyAmount = value
            End Set
        End Property


        <ColumnInfo("AreaViolationBankName", "'{0}'")> _
        Public Property AreaViolationBankName() As String
            Get
                Return _areaViolationBankName
            End Get
            Set(ByVal value As String)
                _areaViolationBankName = value
            End Set
        End Property


        <ColumnInfo("AreaViolationGyroNumber", "'{0}'")> _
        Public Property AreaViolationGyroNumber() As String
            Get
                Return _areaViolationGyroNumber
            End Get
            Set(ByVal value As String)
                _areaViolationGyroNumber = value
            End Set
        End Property


        <ColumnInfo("PenaltyFlag", "'{0}'")> _
        Public Property PenaltyFlag() As String
            Get
                Return _penaltyFlag
            End Get
            Set(ByVal value As String)
                _penaltyFlag = value
            End Set
        End Property


        <ColumnInfo("PenaltyAmount", "{0}")> _
        Public Property PenaltyAmount() As Decimal
            Get
                Return _penaltyAmount
            End Get
            Set(ByVal value As Decimal)
                _penaltyAmount = value
            End Set
        End Property


        <ColumnInfo("PenaltyBankName", "'{0}'")> _
        Public Property PenaltyBankName() As String
            Get
                Return _penaltyBankName
            End Get
            Set(ByVal value As String)
                _penaltyBankName = value
            End Set
        End Property


        <ColumnInfo("PenaltyGyroNumber", "'{0}'")> _
        Public Property PenaltyGyroNumber() As String
            Get
                Return _penaltyGyroNumber
            End Get
            Set(ByVal value As String)
                _penaltyGyroNumber = value
            End Set
        End Property


        <ColumnInfo("ReferenceLetterFlag", "'{0}'")> _
        Public Property ReferenceLetterFlag() As String
            Get
                Return _referenceLetterFlag
            End Get
            Set(ByVal value As String)
                _referenceLetterFlag = value
            End Set
        End Property


        <ColumnInfo("ReferenceLetter", "'{0}'")> _
        Public Property ReferenceLetter() As String
            Get
                Return _referenceLetter
            End Get
            Set(ByVal value As String)
                _referenceLetter = value
            End Set
        End Property


        <ColumnInfo("SaveBy", "'{0}'")> _
        Public Property SaveBy() As String
            Get
                Return _saveBy
            End Get
            Set(ByVal value As String)
                _saveBy = value
            End Set
        End Property


        <ColumnInfo("SaveTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property SaveTime() As DateTime
            Get
                Return _saveTime
            End Get
            Set(ByVal value As DateTime)
                _saveTime = value
            End Set
        End Property


        <ColumnInfo("ValidateBy", "'{0}'")> _
        Public Property ValidateBy() As String
            Get
                Return _validateBy
            End Get
            Set(ByVal value As String)
                _validateBy = value
            End Set
        End Property


        <ColumnInfo("ValidateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ValidateTime() As DateTime
            Get
                Return _validateTime
            End Get
            Set(ByVal value As DateTime)
                _validateTime = value
            End Set
        End Property


        <ColumnInfo("ConfirmBy", "'{0}'")> _
        Public Property ConfirmBy() As String
            Get
                Return _confirmBy
            End Get
            Set(ByVal value As String)
                _confirmBy = value
            End Set
        End Property


        <ColumnInfo("ConfirmTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property ConfirmTime() As DateTime
            Get
                Return _confirmTime
            End Get
            Set(ByVal value As DateTime)
                _confirmTime = value
            End Set
        End Property


        <ColumnInfo("DownloadBy", "'{0}'")> _
        Public Property DownloadBy() As String
            Get
                Return _downloadBy
            End Get
            Set(ByVal value As String)
                _downloadBy = value
            End Set
        End Property


        <ColumnInfo("DownloadTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property DownloadTime() As DateTime
            Get
                Return _downloadTime
            End Get
            Set(ByVal value As DateTime)
                _downloadTime = value
            End Set
        End Property


        <ColumnInfo("PrintedBy", "'{0}'")> _
        Public Property PrintedBy() As String
            Get
                Return _printedBy
            End Get
            Set(ByVal value As String)
                _printedBy = value
            End Set
        End Property


        <ColumnInfo("PrintedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property PrintedTime() As DateTime
            Get
                Return _printedTime
            End Get
            Set(ByVal value As DateTime)
                _printedTime = value
            End Set
        End Property


        <ColumnInfo("CleansingCustomerID", "{0}")> _
        Public Property CleansingCustomerID() As Integer
            Get
                Return _cleansingCustomerID
            End Get
            Set(ByVal value As Integer)
                _cleansingCustomerID = value
            End Set
        End Property

        <ColumnInfo("MCPStatus", "{0}")> _
        Public Property MCPStatus() As Short
            Get
                Return _mcpStatus
            End Get
            Set(ByVal value As Short)
                _mcpStatus = value
            End Set
        End Property

        <ColumnInfo("Remark1", "'{0}'")> _
        Public Property Remark1() As String
            Get
                Return _remark1
            End Get
            Set(ByVal value As String)
                _remark1 = value
            End Set
        End Property


        <ColumnInfo("Remark2", "'{0}'")> _
        Public Property Remark2() As String
            Get
                Return _remark2
            End Get
            Set(ByVal value As String)
                _remark2 = value
            End Set
        End Property


        <ColumnInfo("HandoverDate", "'{0:yyyy/MM/dd}'")> _
        Public Property HandoverDate() As DateTime
            Get
                Return _handoverDate
            End Get
            Set(ByVal value As DateTime)
                _handoverDate = New DateTime(value.Year, value.Month, value.Day)
            End Set
        End Property

        <ColumnInfo("IsTemporary", "{0}")> _
        Public Property IsTemporary() As Short
            Get
                Return _isTemporary
            End Get
            Set(ByVal value As Short)
                _isTemporary = value
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

        <ColumnInfo("AreaViolationPaymentMethodID", "{0}")> _
        Public Property AreaViolationPaymentMethodID() As Integer

            Get
                Return _areaViolationPaymentMethodID
            End Get
            Set(ByVal value As Integer)
                _areaViolationPaymentMethodID = value
            End Set
        End Property
        <ColumnInfo("PenaltyPaymentMethodID", "{0}")> _
        Public Property PenaltyPaymentMethodID() As Integer

            Get
                Return _penaltyPaymentMethodID
            End Get
            Set(ByVal value As Integer)
                _penaltyPaymentMethodID = value
            End Set
        End Property
        '<ColumnInfo("CityID", "{0}"), _
        'RelationInfo("City", "ID", "EndCustomer", "CityID")> _
        'Public Property City() As City
        '    Get
        '        Try
        '            If Not IsNothing(Me._city) AndAlso (Not Me._city.IsLoaded) Then

        '                Me._city = CType(DoLoad(GetType(City).ToString(), _city.ID), City)
        '                Me._city.MarkLoaded()

        '            End If

        '            Return Me._city

        '        Catch ex As Exception

        '            Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

        '            If rethrow Then
        '                Throw
        '            End If

        '        End Try

        '        Return Nothing
        '    End Get

        '    Set(ByVal value As City)

        '        Me._city = value
        '        If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
        '            Me._city.MarkLoaded()
        '        End If
        '    End Set
        'End Property

        <ColumnInfo("CustomerID", "{0}"), _
          RelationInfo("Customer", "ID", "EndCustomer", "CustomerID")> _
        Public Property Customer() As Customer
            Get
                Try
                    If Not IsNothing(Me._customer) AndAlso (Not Me._customer.IsLoaded) Then

                        Me._customer = CType(DoLoad(GetType(Customer).ToString(), _customer.ID), Customer)
                        Me._customer.MarkLoaded()

                    End If

                    Return Me._customer

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As Customer)

                Me._customer = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._customer.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("ID", "{0}"), _
        RelationInfo("EndCustomer", "ID", "ChassisMaster", "EndCustomerID")> _
        Public Property ChassisMaster() As ChassisMaster
            Get
                Try
                    If Not IsNothing(Me._chassisMaster) AndAlso (Not Me._chassisMaster.IsLoaded) Then
                        Dim _criteria As Criteria = New Criteria(GetType(ChassisMaster), "EndCustomer", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(ChassisMaster), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Dim tempColl As ArrayList = DoLoadArray(GetType(ChassisMaster).ToString, criterias)

                        If (tempColl.Count > 0) Then
                            Me._chassisMaster = CType(tempColl(0), ChassisMaster)
                        Else
                            Me._chassisMaster = Nothing
                        End If
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

        <ColumnInfo("ID", "{0}"), _
        RelationInfo("EndCustomer", "ID", "RevisionFaktur", "OldEndCustomerID")> _
        Public Property RevisionFaktur() As RevisionFaktur
            Get
                Try
                    If IsNothing(Me._revisionFaktur) Then
                        Dim _criteria As Criteria = New Criteria(GetType(RevisionFaktur), "OldEndCustomer", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(RevisionFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Dim tempColl As ArrayList = DoLoadArray(GetType(RevisionFaktur).ToString, criterias)

                        If (tempColl.Count > 0) Then
                            Me._revisionFaktur = CType(tempColl(0), RevisionFaktur)
                        Else
                            Me._revisionFaktur = Nothing
                        End If
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
                'If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                '    Me._revisionFaktur.MarkLoaded()
                'End If
            End Set
        End Property

        <ColumnInfo("ID", "{0}"), _
        RelationInfo("SPKFaktur", "EndCustomerID", "EndCustomer", "ID")> _
        Public ReadOnly Property SPKFaktur() As SPKFaktur
            Get
                Try
                    If IsNothing(Me._spkFaktur) Then
                        'Me._spkFaktur = CType(DoLoad(GetType(SPKFaktur).ToString(), _spkFaktur.ID), SPKFaktur)
                        'Me._spkFaktur.MarkLoaded()
                        Dim _criteria As Criteria = New Criteria(GetType(SPKFaktur), "EndCustomer", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(SPKFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        Dim tempColl As ArrayList = DoLoadArray(GetType(SPKFaktur).ToString, criterias)

                        If (tempColl.Count > 0) Then
                            Me._spkFaktur = CType(tempColl(0), SPKFaktur)
                        Else
                            Me._spkFaktur = Nothing
                        End If
                    End If

                    Return Me._spkFaktur

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            'Set(ByVal value As SPKFaktur)

            '    Me._spkFaktur = value
            '    If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
            '        Me._spkFaktur.MarkLoaded()
            '    End If
            'End Set

        End Property

        <ColumnInfo("MCPHeaderID", "{0}"), _
          RelationInfo("MCPHeader", "ID", "EndCustomer", "MCPHeaderID")> _
        Public Property MCPHeader() As MCPHeader
            Get
                Try
                    If Not IsNothing(Me._mcpHeader) AndAlso (Not Me._mcpHeader.IsLoaded) Then

                        Me._mcpHeader = CType(DoLoad(GetType(MCPHeader).ToString(), _mcpHeader.ID), MCPHeader)
                        Me._mcpHeader.MarkLoaded()

                    End If

                    Return Me._mcpHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As MCPHeader)

                Me._mcpHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._mcpHeader.MarkLoaded()
                End If
            End Set
        End Property


        <ColumnInfo("LKPPHeaderID", "{0}"), _
                    RelationInfo("LKPPHeader", "ID", "EndCustomer", "LKPPHeaderID")> _
        Public Property LKPPHeader() As LKPPHeader
            Get
                Try
                    If Not IsNothing(Me._lkppHeader) AndAlso (Not Me._lkppHeader.IsLoaded) Then

                        Me._lkppHeader = CType(DoLoad(GetType(LKPPHeader).ToString(), _lkppHeader.ID), LKPPHeader)
                        Me._lkppHeader.MarkLoaded()

                    End If

                    Return Me._lkppHeader

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As LKPPHeader)

                Me._lkppHeader = value
                If (Not IsNothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._lkppHeader.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("LKPPStatus", "{0}")> _
        Public Property LKPPStatus As Short
            Get
                Return _lKPPStatus
            End Get
            Set(ByVal value As Short)
                _lKPPStatus = value
            End Set
        End Property

        <ColumnInfo("ID", "{0}"), _
        RelationInfo("RevisionSPKFaktur", "EndCustomerID", "EndCustomer", "ID")> _
        Public ReadOnly Property RevisionSPKFaktur() As RevisionSPKFaktur
            Get
                Try
                    If IsNothing(Me._revisionSpkFaktur) Then
                        Dim _criteria As Criteria = New Criteria(GetType(RevisionSPKFaktur), "EndCustomer", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(RevisionSPKFaktur), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                        Dim tempColl As ArrayList = DoLoadArray(GetType(RevisionSPKFaktur).ToString, criterias)

                        If (tempColl.Count > 0) Then
                            Me._revisionSpkFaktur = CType(tempColl(0), RevisionSPKFaktur)
                        Else
                            Me._revisionSpkFaktur = Nothing
                        End If
                    End If

                    Return Me._revisionSpkFaktur

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get
        End Property
#End Region

#Region "Custom Method"
        Private _CleansingCustomerCode As String
        Private _OriginalCustomer As Customer
        Public Property CleansingCustomerCode() As String
            Get
                Return _CleansingCustomerCode
            End Get
            Set(ByVal Value As String)
                _CleansingCustomerCode = Value
            End Set
        End Property

        Public Property OriginalCustomer() As Customer
            Get
                Return _OriginalCustomer
            End Get
            Set(ByVal Value As Customer)
                _OriginalCustomer = Value
            End Set
        End Property

#End Region

    End Class
End Namespace

