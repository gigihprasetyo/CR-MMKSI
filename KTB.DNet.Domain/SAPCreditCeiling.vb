
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Heru Binarto
'// PURPOSE       : Mapping domain from SAP
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 9/27/2005 - 5:14:56 PM
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
Imports KTB.DNet.DataMapper.Framework
#End Region

Namespace KTB.DNet.Domain
    <Serializable()> _
    Public Class SAPCreditCeiling
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub


       
#End Region

#Region "Variable"
        Private _selectionDate As DateTime
        Private _bufferDay As Integer
        Private _CeilingAmount As Decimal
        Private _BlockAmount As Double
        Private _TemporaryKind As String
        Private _BlockStatus As String
        Private _TemporaryCode As String
        Private _TemporaryType As String
        Private _SPLNumber As String
        Private _CreditAccount As String
        Private _validFrom As Date
        Private _validTo As Date
        Private _BlockedDate As Date
        Private _BlockedName As String
        Private _ModifyDate As Date
        Private _ModifyName As String
        Private _DealerCode As String
        Private _PeriodMonth As Integer
        Private _PeriodYear As Integer
#End Region

#Region "Public Properties"

        Public Property DealerCode() As String
            Get
                Return _DealerCode
            End Get
            Set(ByVal Value As String)
                _DealerCode = Value
            End Set
        End Property
        Public Property TemporaryType() As String
            Get
                Return _TemporaryType
            End Get
            Set(ByVal Value As String)
                _TemporaryType = Value
            End Set
        End Property
        Public Property ModifyName() As String
            Get
                Return _ModifyName
            End Get
            Set(ByVal Value As String)
                _ModifyName = Value
            End Set
        End Property

        Public Property ModifyDate() As Date
            Get
                Return _ModifyDate
            End Get
            Set(ByVal Value As Date)
                _ModifyDate = Value
            End Set
        End Property

        Public Property BlokedName() As String
            Get
                Return _BlockedName
            End Get
            Set(ByVal Value As String)
                _BlockedName = Value
            End Set
        End Property

        Public Property BlockedDate() As Date
            Get
                Return _BlockedDate
            End Get
            Set(ByVal Value As Date)
                _BlockedDate = Value
            End Set
        End Property

        Public Property SelectionDate() As Date
            Get
                Return _selectionDate
            End Get
            Set(ByVal Value As Date)
                _selectionDate = Value
            End Set
        End Property

        Public Property ValidTo() As Date
            Get
                Return _validTo
            End Get
            Set(ByVal Value As Date)
                _validTo = Value
            End Set
        End Property

        Public Property ValidFrom() As Date
            Get
                Return _validFrom
            End Get
            Set(ByVal Value As Date)
                _validTo = Value
            End Set
        End Property

        Public Property CreditAccount() As String
            Get
                Return _CreditAccount
            End Get
            Set(ByVal Value As String)
                _CreditAccount = Value
            End Set
        End Property

        Public Property TemporaryCode() As String
            Get
                Return _TemporaryCode
            End Get
            Set(ByVal Value As String)
                _TemporaryCode = Value
            End Set
        End Property

        Public Property SPLNumber() As String
            Get
                Return _SPLNumber
            End Get
            Set(ByVal Value As String)
                _SPLNumber = Value
            End Set
        End Property

        Public Property TemporaryKind() As String
            Get
                Return _TemporaryKind
            End Get
            Set(ByVal Value As String)
                _TemporaryKind = Value
            End Set
        End Property

        Public Property BlokedAmount() As Double
            Get
                Return _BlockAmount
            End Get
            Set(ByVal Value As Double)
                _BlockAmount = Value
            End Set
        End Property

        Public Property BufferDay() As Integer
            Get
                Return _bufferDay
            End Get
            Set(ByVal Value As Integer)
                _bufferDay = Value
            End Set
        End Property

        Public Property CeilingAmount() As Decimal
            Get
                Return _CeilingAmount
            End Get
            Set(ByVal Value As Decimal)
                _CeilingAmount = Value
            End Set
        End Property


        Public Property PeriodMonth() As Integer
            Get
                Return _PeriodMonth
            End Get
            Set(ByVal Value As Integer)
                _PeriodMonth = Value
            End Set
        End Property
        Public Property PeriodYear() As Integer
            Get
                Return _PeriodYear
            End Get
            Set(ByVal Value As Integer)
                _PeriodYear = Value
            End Set
        End Property

        Public ReadOnly Property ProjectName() As String
            Get
                Dim _ProjectName As String = String.Empty
                Dim m_ContractHeader As IMapper = MapperFactory.GetInstance().GetMapper(GetType(ContractHeader).ToString)

                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(ContractHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(ContractHeader), "SPLNumber", MatchType.Exact, SPLNumber))


                Dim sortColl As SortCollection = New SortCollection
                sortColl.Add(New Sort(GetType(ContractHeader), "CreatedTime", Sort.SortDirection.DESC))

                Dim ListContract As ArrayList = m_ContractHeader.RetrieveByCriteria(criterias, sortColl)

                If ListContract.Count > 0 Then
                    Dim objDomain As ContractHeader = ListContract(0)
                    _ProjectName = objDomain.ProjectName
                End If

                Return _ProjectName

            End Get
        End Property

        Public ReadOnly Property TemporaryCreditExposure() As Double
            Get
                Dim m_Mapperder As IMapper = MapperFactory.GetInstance().GetMapper(GetType(POHeader).ToString)
                Dim result As Double = 0
                Dim dueDate As Date
                Dim reqDeliverDate As Date
                Dim dateSelection As Date = _selectionDate
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(POHeader), "TermOfPayment.TermOfPaymentValue", MatchType.Greater, 0))
                criterias.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(" & CType(enumStatusPO.Status.Baru, Integer) & "," & CType(enumStatusPO.Status.Rilis, Integer) & "," & CType(enumStatusPO.Status.Konfirmasi, Integer) & "," & CType(enumStatusPO.Status.Selesai, Integer) & "," & CType(enumStatusPO.Status.Setuju, Integer) & ")"))
                criterias.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Dealer.LegalStatus", MatchType.Exact, _DealerCode))
                criterias.opAnd(New Criteria(GetType(POHeader), "ContractHeader.SPLNumber", MatchType.Exact, _SPLNumber))
                Dim listSelectedPO As ArrayList = m_Mapperder.RetrieveByCriteria(criterias)
                If listSelectedPO.Count > 0 Then
                    For Each item As POHeader In listSelectedPO
                        dueDate = item.ReqAllocationDateTime.AddDays(item.TermOfPayment.TermOfPaymentValue).AddDays(item.ContractHeader.Dealer.DueDate)
                        dueDate = New Date(dueDate.Year, dueDate.Month, dueDate.Day, 0, 0, 0)
                        reqDeliverDate = item.ReqAllocationDateTime
                        reqDeliverDate = New Date(reqDeliverDate.Year, reqDeliverDate.Month, reqDeliverDate.Day, 0, 0, 0)
                        reqDeliverDate = item.ReqAllocationDateTime
                        reqDeliverDate = New Date(reqDeliverDate.Year, reqDeliverDate.Month, reqDeliverDate.Day, 0, 0, 0)
                        If dateSelection >= reqDeliverDate And dateSelection <= dueDate Then
                            result += item.TotalHargaExposure
                        End If
                    Next
                End If
                Return result
            End Get
        End Property

        Private _dealerName As String
        Public ReadOnly Property DealerName() As String
            Get
                Dim m_Mapper As IMapper = MapperFactory.GetInstance().GetMapper(GetType(Dealer).ToString)
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.Exact, _DealerCode))
                Dim List As ArrayList = m_Mapper.RetrieveByCriteria(criterias)
                If List.Count > 0 Then
                    Dim objDomain As Dealer = List(0)
                    Return objDomain.DealerName
                End If
                Return ""
            End Get
        End Property

        Private _AllOutStanding As Double
        Private _AllOutStandingPOList As ArrayList
        Public ReadOnly Property AllOutStanding() As Double
            Get
                Dim m_Mapper As IMapper = MapperFactory.GetInstance().GetMapper(GetType(POHeader).ToString)
                Dim result As Double = 0
                Dim resultList As ArrayList = New ArrayList
                Dim dueDate As Date
                Dim reqDeliverDate As Date
                Dim dateSelection As Date = _selectionDate
                Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(POHeader), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                criterias.opAnd(New Criteria(GetType(POHeader), "TermOfPayment.TermOfPaymentValue", MatchType.Greater, 0))
                criterias.opAnd(New Criteria(GetType(POHeader), "Status", MatchType.InSet, "(" & CType(enumStatusPO.Status.Baru, Integer) & "," & CType(enumStatusPO.Status.Rilis, Integer) & "," & CType(enumStatusPO.Status.Konfirmasi, Integer) & "," & CType(enumStatusPO.Status.Selesai, Integer) & "," & CType(enumStatusPO.Status.Setuju, Integer) & ")"))
                criterias.opAnd(New Criteria(GetType(POHeader), "ContractHeader.Dealer.LegalStatus", MatchType.Exact, _DealerCode))
                Dim listSelectedPO As ArrayList = m_Mapper.RetrieveByCriteria(criterias)
                If listSelectedPO.Count > 0 Then
                    For Each item As POHeader In listSelectedPO

                        If item.SONumber.Trim <> "" Then
                            Dim paymentDueDate As Date = GetDueDateUsingDailyPayment(item)
                            If paymentDueDate > New Date(1901, 1, 1) Then
                                dueDate = paymentDueDate
                            Else
                                dueDate = item.ReqAllocationDateTime.AddDays(item.TermOfPayment.TermOfPaymentValue).AddDays(item.ContractHeader.Dealer.DueDate)
                            End If
                        Else
                            dueDate = item.ReqAllocationDateTime.AddDays(item.TermOfPayment.TermOfPaymentValue).AddDays(item.ContractHeader.Dealer.DueDate)
                        End If
                        dueDate = New Date(dueDate.Year, dueDate.Month, dueDate.Day, 0, 0, 0)
                        reqDeliverDate = item.ReqAllocationDateTime
                        reqDeliverDate = New Date(reqDeliverDate.Year, reqDeliverDate.Month, reqDeliverDate.Day, 0, 0, 0)
                        reqDeliverDate = item.ReqAllocationDateTime
                        reqDeliverDate = New Date(reqDeliverDate.Year, reqDeliverDate.Month, reqDeliverDate.Day, 0, 0, 0)
                        If dateSelection >= reqDeliverDate And dateSelection <= dueDate Then
                            result += item.TotalHargaExposure
                            resultList.Add(item)
                        End If
                    Next
                End If
                _AllOutStandingPOList = resultList
                Return result
            End Get
        End Property

        Public ReadOnly Property AllOutStandingPOList() As ArrayList
            Get
                Return _AllOutStandingPOList
            End Get
        End Property



        Private _AvailableRegTOP As Double
        Public ReadOnly Property AvailableRegTOP() As Double
            Get
                Return _CeilingAmount - _RegOutStanding
            End Get
        End Property
        'Pending
        Private _RejectedOS As Double
        Public ReadOnly Property RejectedOS() As Double
            Get
                Return _RejectedOS
            End Get
        End Property

#End Region

#Region "Posisi Credit"
        Private _TotalTempOS As Double
        Public ReadOnly Property TotalTempOS() As Double
            Get
                Return _totalTempOS
            End Get
        End Property

        Private _TempProjectOS As Double
        Public ReadOnly Property TempProjectOS() As Double
            Get
                Return _TempProjectOS
            End Get
        End Property

        Private _TempProjectSP As Double
        Public ReadOnly Property TempProjectSP() As Double
            Get
                Return _TempProjectSP
            End Get
        End Property

        Private _RegOutStanding As Double
        Public ReadOnly Property RegularOutStanding() As Double
            Get
                Return 0
            End Get
        End Property



#End Region

        Private Function GetDueDateUsingDailyPayment(ByVal objPO As POHeader) As Date
            Dim m_Mapper As IMapper = MapperFactory.GetInstance().GetMapper(GetType(DailyPayment).ToString)
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(DailyPayment), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(DailyPayment), "POHeader.ID", MatchType.Exact, objPO.ID))
            criterias.opAnd(New Criteria(GetType(DailyPayment), "PaymentPurpose.PaymentPurposeCode", MatchType.[Partial], "VH"))
            criterias.opAnd(New Criteria(GetType(DailyPayment), "RejectStatus", MatchType.Exact, 0))
            Dim sortColl As KTB.DNet.Domain.Search.SortCollection = New KTB.DNet.Domain.Search.SortCollection
            sortColl.Add(New KTB.DNet.Domain.Search.Sort(GetType(DailyPayment), "ID", Sort.SortDirection.DESC))
            Dim list As ArrayList = m_Mapper.RetrieveByCriteria(criterias, sortColl)
            Dim objPayment As DailyPayment
            If list.Count > 0 Then
                objPayment = CType(list(0), DailyPayment)
                Return objPayment.BaselineDate
            Else
                Return New Date(1900, 1, 1)
            End If
        End Function
    End Class
End Namespace
