#Region "Code Disclaimer"
'Copyright PT. Puspa Intimedia Internusa (Intimedia) 2005

'Intimedia grants you the right to use and modify the code in this Persistence Framework
'(code under the Framework Namespace) but 
'(i) only for the solutions that are developed by Intimedia for you 
'(ii) or in solutions that are developed in join development between you and Intimedia.

'All rights not expressly granted, are reserved.
#End Region

#Region "Summary"
'///////////////////////////////////////////////////////////////////////////////////////
'// Author Name: 
'// PURPOSE       : Enter summary here after generation.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005
'// ---------------------
'// $History      : $
'// Generated on 9/26/2005 - 1:43:31 PM
'//
'// ===========================================================================		
#End Region

#Region ".Net Namespace"

Imports System
Imports System.Data
Imports System.Collections
Imports System.Security.Principal
Imports System.Security.Cryptography

#End Region

#Region "Custom Namespace"

Imports KTB.DNet.Domain
Imports KTB.Dnet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region
Namespace KTB.DNet.BusinessFacade.AlertManagement
    Public Class AlertModulFacade
        Inherits AbstractFacade
#Region "Private Variables"
        Private m_userPrincipal As IPrincipal = Nothing
        Private m_AlertModuleMapper As IMapper
#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal

            Me.m_AlertModuleMapper = MapperFactory.GetInstance().GetMapper(GetType(KTB.DNet.Domain.AlertModul).ToString)
            Me.DomainTypeCollection.Add(GetType(KTB.DNet.Domain.AlertModul))
        End Sub

#End Region

#Region "Retrieve"
        Public Function RetrieveActiveListByCategoryID(ByVal CategoryID As Long) As ArrayList
            Dim criterias As CriteriaComposite = New CriteriaComposite(New Criteria(GetType(AlertModul), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
            criterias.opAnd(New Criteria(GetType(AlertModul), "AlertCategory.ID", MatchType.Exact, CategoryID))
            Dim result As ArrayList = m_AlertModuleMapper.RetrieveByCriteria(criterias)

            Return result
        End Function

        Public Function Retrieve(ByVal ID As Integer) As AlertModul
            Return CType(m_AlertModuleMapper.Retrieve(ID), AlertModul)
        End Function

        Public Function RetrieveModulStatusesByModulID(ByVal ModulID As Integer) As ArrayList

            Dim arlStatus As ArrayList

            Dim objModul As AlertModul = Retrieve(ModulID)

            Dim obj As Object = Activator.CreateInstance(objModul.EnumAssemblyName, objModul.EnumClassName).Unwrap()

        End Function
#End Region
    End Class
End Namespace