
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CcComplaint Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 16/03/2020 - 13:40:04
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
    <Serializable(), TableInfo("CcComplaint")> _
    Public Class CcComplaint
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
        Private _ccSurveyID As Integer
        Private _ccSurveyQuetionnareID As Integer
        Private _ccScenarioID As Integer
        Private _ccFactorID As Integer
        Private _ccAttributeID As Integer
        Private _description As String = String.Empty
        Private _category As String = String.Empty
        Private _subCategory1 As String = String.Empty
        Private _subCategory2 As String = String.Empty
        Private _subCategory3 As String = String.Empty
        Private _subCategory4 As String = String.Empty
        Private _salesforceid As String = String.Empty
        Private _status As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)




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


        <ColumnInfo("CcSurveyID", "{0}")> _
        Public Property CcSurveyID As Integer
            Get
                Return _ccSurveyID
            End Get
            Set(ByVal value As Integer)
                _ccSurveyID = value
            End Set
        End Property


        <ColumnInfo("CcSurveyQuetionnareID", "{0}")> _
        Public Property CcSurveyQuetionnareID As Integer
            Get
                Return _ccSurveyQuetionnareID
            End Get
            Set(ByVal value As Integer)
                _ccSurveyQuetionnareID = value
            End Set
        End Property


        <ColumnInfo("CcScenarioID", "{0}")> _
        Public Property CcScenarioID As Integer
            Get
                Return _ccScenarioID
            End Get
            Set(ByVal value As Integer)
                _ccScenarioID = value
            End Set
        End Property


        <ColumnInfo("CcFactorID", "{0}")> _
        Public Property CcFactorID As Integer
            Get
                Return _ccFactorID
            End Get
            Set(ByVal value As Integer)
                _ccFactorID = value
            End Set
        End Property


        <ColumnInfo("CcAttributeID", "{0}")> _
        Public Property CcAttributeID As Integer
            Get
                Return _ccAttributeID
            End Get
            Set(ByVal value As Integer)
                _ccAttributeID = value
            End Set
        End Property


        <ColumnInfo("Description", "'{0}'")> _
        Public Property Description As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property


        <ColumnInfo("Category", "'{0}'")> _
        Public Property Category As String
            Get
                Return _category
            End Get
            Set(ByVal value As String)
                _category = value
            End Set
        End Property


        <ColumnInfo("SubCategory1", "'{0}'")> _
        Public Property SubCategory1 As String
            Get
                Return _subCategory1
            End Get
            Set(ByVal value As String)
                _subCategory1 = value
            End Set
        End Property


        <ColumnInfo("SubCategory2", "'{0}'")> _
        Public Property SubCategory2 As String
            Get
                Return _subCategory2
            End Get
            Set(ByVal value As String)
                _subCategory2 = value
            End Set
        End Property


        <ColumnInfo("SubCategory3", "'{0}'")> _
        Public Property SubCategory3 As String
            Get
                Return _subCategory3
            End Get
            Set(ByVal value As String)
                _subCategory3 = value
            End Set
        End Property


        <ColumnInfo("SubCategory4", "'{0}'")> _
        Public Property SubCategory4 As String
            Get
                Return _subCategory4
            End Get
            Set(ByVal value As String)
                _subCategory4 = value
            End Set
        End Property


        <ColumnInfo("Salesforceid", "'{0}'")> _
        Public Property Salesforceid As String
            Get
                Return _salesforceid
            End Get
            Set(ByVal value As String)
                _salesforceid = value
            End Set
        End Property


        <ColumnInfo("Status", "{0}")> _
        Public Property Status As Short
            Get
                Return _status
            End Get
            Set(ByVal value As Short)
                _status = value
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
        Public Enum EnumStatus
            NOT_SET = 0
            COMPLAINT = 1
            NOT_COMPLAINT = 2
            SEND_SALESFORCE = 3
            CANCEL_SALESFORCE = 4
            SENT_SALESFORCE = 5
        End Enum

        Public Shared Function GetStatusDescription(ByVal iStatus As Integer) As String
            Dim str As String = ""

            Select Case iStatus
                Case 0
                    str = "Belum di set"
                Case 1
                    str = "Keluhan"
                Case 2
                    str = "Bukan Keluhan"
                Case 3
                    str = "Kirim ke Sales Force"
                Case 4
                    str = "Batal Sales Force"
                Case 5
                    str = "Terkirim ke Sales Force"

            End Select

            Return str
        End Function

        Public Shared Function GetStatusValue(ByVal sStatus As String) As Integer
            Dim Rsl As Integer = 0
            Select Case sStatus.ToLower()
                Case "belum di set"
                    Rsl = 0
                Case "keluhan"
                    Rsl = 1
                Case "bukan keluhan"
                    Rsl = 2
                Case "kirim ke sales force"
                    Rsl = 3
                Case "batal sales force"
                    Rsl = 4
                Case "terkirim ke sales force"
                    Rsl = 5

            End Select

            Return Rsl
        End Function
#End Region

    End Class
End Namespace

