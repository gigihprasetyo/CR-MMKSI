#Region ".Net Namespace"

Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography

#End Region

#Region "Custom Namespace"

Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.FinishUnit
    Public Class CustomerProfileFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_MainOperationAreaMapper As IMapper
        Private m_VehicleOwnershipMapper As IMapper
        Private m_CustomerBusinessMapper As IMapper
        Private m_PaymentTypeMapper As IMapper
        Private m_VehicleBodyShapeMapper As IMapper
        Private m_MainUsageMapper As IMapper
        Private m_VehiclePurposeMapper As IMapper
        Private m_OwnerAgeMapper As IMapper
        Private m_CustomerProfileMapper As IMapper

        Private m_userPrincipal As IPrincipal = Nothing

#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            Me.m_MainOperationAreaMapper = MapperFactory.GetInstance().GetMapper(GetType(MainOperationArea).ToString)
            Me.m_VehicleOwnershipMapper = MapperFactory.GetInstance().GetMapper(GetType(VehicleOwnership).ToString)
            Me.m_CustomerBusinessMapper = MapperFactory.GetInstance().GetMapper(GetType(CustomerBusiness).ToString)
            Me.m_PaymentTypeMapper = MapperFactory.GetInstance().GetMapper(GetType(PaymentType).ToString)
            Me.m_VehicleBodyShapeMapper = MapperFactory.GetInstance().GetMapper(GetType(VehicleBodyShape).ToString)
            Me.m_MainUsageMapper = MapperFactory.GetInstance().GetMapper(GetType(MainUsage).ToString)
            Me.m_VehiclePurposeMapper = MapperFactory.GetInstance().GetMapper(GetType(VehiclePurpose).ToString)
            Me.m_OwnerAgeMapper = MapperFactory.GetInstance().GetMapper(GetType(OwnerAge).ToString)
            Me.m_CustomerProfileMapper = MapperFactory.GetInstance().GetMapper(GetType(CustomerProfile).ToString)
        End Sub

#End Region

#Region "Retrieve"
        Public Function RetrieveMainOperationAreaList() As ArrayList
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MainOperationArea), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(MainOperationArea), "Status", MatchType.Exact, "A"))
            Return Me.m_MainOperationAreaMapper.RetrieveByCriteria(crit)
        End Function

        Public Function RetrieveMainOperationArea(ByVal ID As Integer) As MainOperationArea
            Return Me.m_MainOperationAreaMapper.Retrieve(ID)
        End Function

        Public Function RetrieveVehicleOwnershipList() As ArrayList
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleOwnership), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(VehicleOwnership), "Status", MatchType.Exact, "A"))
            Return Me.m_VehicleOwnershipMapper.RetrieveByCriteria(crit)
        End Function

        Public Function RetrieveVehicleOwnership(ByVal ID As Integer) As VehicleOwnership
            Return Me.m_VehicleOwnershipMapper.Retrieve(ID)
        End Function

        Public Function RetrieveCustomerBusinessList() As ArrayList
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(CustomerBusiness), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(CustomerBusiness), "Status", MatchType.Exact, "A"))
            Return Me.m_CustomerBusinessMapper.RetrieveByCriteria(crit)
        End Function

        Public Function RetrieveCustomerBusiness(ByVal ID As Integer) As CustomerBusiness
            Return Me.m_CustomerBusinessMapper.Retrieve(ID)
        End Function

        Public Function RetrievePaymentTypeList() As ArrayList
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(PaymentType), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(PaymentType), "Status", MatchType.Exact, "A"))
            Return Me.m_PaymentTypeMapper.RetrieveByCriteria(crit)
        End Function

        Public Function RetrievePaymentType(ByVal ID As Integer) As PaymentType
            Return Me.m_PaymentTypeMapper.Retrieve(ID)
        End Function

        Public Function RetrieveVehicleBodyShapeList() As ArrayList
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehicleBodyShape), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(VehicleBodyShape), "Status", MatchType.Exact, "A"))
            Return Me.m_VehicleBodyShapeMapper.RetrieveByCriteria(crit)
        End Function

        Public Function RetrieveVehicleBodyShape(ByVal ID As Integer) As VehicleBodyShape
            Return Me.m_VehicleBodyShapeMapper.Retrieve(ID)
        End Function

        Public Function RetrieveVehicleBodyShapeList(ByVal Criteria As CriteriaComposite) As ArrayList
            Criteria.opAnd(New Criteria(GetType(VehicleBodyShape), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            Criteria.opAnd(New Criteria(GetType(VehicleBodyShape), "Status", MatchType.Exact, "A"))
            Return Me.m_VehicleBodyShapeMapper.RetrieveByCriteria(Criteria)
        End Function

        Public Function RetrieveMainUsageList() As ArrayList
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(MainUsage), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(MainUsage), "Status", MatchType.Exact, "A"))
            Return Me.m_MainUsageMapper.RetrieveByCriteria(crit)
        End Function

        Public Function RetrieveMainUsage(ByVal ID As Integer) As MainUsage
            Return Me.m_MainUsageMapper.Retrieve(ID)
        End Function

        Public Function RetrieveVehiclePurposeList() As ArrayList
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(VehiclePurpose), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(VehiclePurpose), "Status", MatchType.Exact, "A"))
            Return Me.m_VehiclePurposeMapper.RetrieveByCriteria(crit)
        End Function

        Public Function RetrieveVehiclePurpose(ByVal ID As Integer) As VehiclePurpose
            Return Me.m_VehiclePurposeMapper.Retrieve(ID)
        End Function

        Public Function RetrieveOwnerAgeList() As ArrayList
            Dim crit As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(OwnerAge), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            crit.opAnd(New Criteria(GetType(OwnerAge), "Status", MatchType.Exact, "A"))
            Return Me.m_OwnerAgeMapper.RetrieveByCriteria(crit)
        End Function

        Public Function RetrieveOwnerAge(ByVal ID As Integer) As OwnerAge
            Return Me.m_OwnerAgeMapper.Retrieve(ID)
        End Function

        Public Function RetrieveActiveList(ByVal Criterias As CriteriaComposite) As ArrayList
            Dim arl As ArrayList = m_CustomerProfileMapper.RetrieveByCriteria(Criterias)
            Return arl
        End Function
#End Region

    End Class

End Namespace
