
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BenefitType Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 12/2/2015 - 10:48:45 AM
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
    <Serializable(), TableInfo("BenefitType")> _
    Public Class BenefitType
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Short)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"

        Private _iD As Short
        Private _name As String = String.Empty
        Private _leasingBox As Short
        Private _assyYearBox As Short
        Private _receiptBox As Short
        Private _eventValidation As Short
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _wsDiscount As Short
        Private _status As Short

        Private _benefitMasterDetails As System.Collections.ArrayList = New System.Collections.ArrayList()


#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID As Short
            Get
                Return _iD
            End Get
            Set(ByVal value As Short)
                _iD = value
            End Set
        End Property


        <ColumnInfo("Name", "'{0}'")> _
        Public Property Name As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property


        <ColumnInfo("LeasingBox", "{0}")> _
        Public Property LeasingBox As Short
            Get
                Return _leasingBox
            End Get
            Set(ByVal value As Short)
                _leasingBox = value
            End Set
        End Property


        <ColumnInfo("AssyYearBox", "{0}")> _
        Public Property AssyYearBox As Short
            Get
                Return _assyYearBox
            End Get
            Set(ByVal value As Short)
                _assyYearBox = value
            End Set
        End Property


        <ColumnInfo("ReceiptBox", "{0}")> _
        Public Property ReceiptBox As Short
            Get
                Return _receiptBox
            End Get
            Set(ByVal value As Short)
                _receiptBox = value
            End Set
        End Property

        <ColumnInfo("EventValidation", "{0}")> _
        Public Property EventValidation As Short
            Get
                Return _eventValidation
            End Get
            Set(ByVal value As Short)
                _eventValidation = value
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


        <ColumnInfo("WSDiscount", "{0}")> _
        Public Property WSDiscount As Short
            Get
                Return _wsDiscount
            End Get
            Set(ByVal value As Short)
                _wsDiscount = value
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


        <RelationInfo("BenefitType", "ID", "BenefitMasterDetail", "BenefitTypeID")> _
        Public ReadOnly Property BenefitMasterDetails As System.Collections.ArrayList
            Get
                Try
                    If (Me._benefitMasterDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(BenefitMasterDetail), "BenefitType", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(BenefitMasterDetail), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._benefitMasterDetails = DoLoadArray(GetType(BenefitMasterDetail).ToString, criterias)
                    End If

                    Return Me._benefitMasterDetails

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

