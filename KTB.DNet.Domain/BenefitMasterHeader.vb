
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BenefitMasterHeader Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 12/11/2015 - 8:48:14 AM
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
    <Serializable(), TableInfo("BenefitMasterHeader")> _
    Public Class BenefitMasterHeader
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
        Private _nomorSurat As String = String.Empty
        Private _status As Short
        Private _benefitRegNo As String = String.Empty
        Private _remarks As String = String.Empty
        Private _formula As String = String.Empty
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)


        Private _benefitMasterDealers As System.Collections.ArrayList = New System.Collections.ArrayList()
        Private _benefitEventHeaders As System.Collections.ArrayList = New System.Collections.ArrayList()
        Private _benefitMasterDetails As System.Collections.ArrayList = New System.Collections.ArrayList()


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


        <ColumnInfo("NomorSurat", "'{0}'")> _
        Public Property NomorSurat As String
            Get
                Return _nomorSurat
            End Get
            Set(ByVal value As String)
                _nomorSurat = value
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


        <ColumnInfo("BenefitRegNo", "'{0}'")> _
        Public Property BenefitRegNo As String
            Get
                Return _benefitRegNo
            End Get
            Set(ByVal value As String)
                _benefitRegNo = value
            End Set
        End Property


        <ColumnInfo("Remarks", "'{0}'")> _
        Public Property Remarks As String
            Get
                Return _remarks
            End Get
            Set(ByVal value As String)
                _remarks = value
            End Set
        End Property


        <ColumnInfo("Formula", "'{0}'")> _
        Public Property Formula As String
            Get
                Return _formula
            End Get
            Set(ByVal value As String)
                _formula = value
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



        <RelationInfo("BenefitMasterHeader", "ID", "BenefitMasterDealer", "BenefitMasterHeaderID")> _
        Public ReadOnly Property BenefitMasterDealers As System.Collections.ArrayList
            Get
                Try
                    If (Me._benefitMasterDealers.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(BenefitMasterDealer), "BenefitMasterHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(BenefitMasterDealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._benefitMasterDealers = DoLoadArray(GetType(BenefitMasterDealer).ToString, criterias)
                    End If

                    Return Me._benefitMasterDealers

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("BenefitMasterHeader", "ID", "BenefitEventHeader", "BenefitMasterHeaderID")> _
        Public ReadOnly Property BenefitEventHeaders As System.Collections.ArrayList
            Get
                Try
                    If (Me._benefitEventHeaders.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(BenefitEventHeader), "BenefitMasterHeader", Me.ID)
                        Dim criterias As CriteriaComposite = New CriteriaComposite(_criteria)
                        criterias.opAnd(New Criteria(GetType(BenefitEventHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))

                        Me._benefitEventHeaders = DoLoadArray(GetType(BenefitEventHeader).ToString, criterias)
                    End If

                    Return Me._benefitEventHeaders

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing

            End Get
        End Property

        <RelationInfo("BenefitMasterHeader", "ID", "BenefitMasterDetail", "BenefitMasterHeaderID")> _
        Public ReadOnly Property BenefitMasterDetails As System.Collections.ArrayList
            Get
                Try
                    If (Me._benefitMasterDetails.Count < 1) Then
                        Dim _criteria As Criteria = New Criteria(GetType(BenefitMasterDetail), "BenefitMasterHeader", Me.ID)
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

