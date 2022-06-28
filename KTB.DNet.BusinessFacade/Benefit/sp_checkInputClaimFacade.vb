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
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 8/3/2005 - 10:53:00 AM
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

Imports KTB.DNet.BusinessFacade
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

#End Region

Namespace KTB.DNet.BusinessFacade.Benefit
    Public Class sp_checkInputClaimFacade
        Inherits AbstractFacade

#Region "Private Variables"

        Private m_sp_checkInputClaimMapper As IMapper
        'Private m_V_POTotalDetailMapper As IMapper
        Private m_userPrincipal As IPrincipal = Nothing


#End Region

#Region "Constructor"

        Public Sub New(ByVal userPrincipal As IPrincipal)
            Me.m_userPrincipal = userPrincipal
            'Me.m_V_POTotalDetailMapper = MapperFactory.GetInstance().GetMapper(GetType(V_POTotalDetail).ToString)
            Me.m_sp_checkInputClaimMapper = MapperFactory.GetInstance().GetMapper(GetType(sp_checkInputClaim).ToString)
        End Sub

#End Region

#Region "Retrieve"

        Public Function RetrieveFromSP_(ByVal EndCustomerID As Integer, ByVal BenefitClaimDetailID As Integer, ByVal BenefitMasterHeaderID As Integer, ByVal BenefitTypeID As Integer, ByVal LeasingCompanyID As Integer, ByVal IsDebug As Integer) As ArrayList

            '  exec sp_checkInputClaim_Donas @EndCustomerID=1292976
            ', @BenefitClaimDetailID = 0 --0:Input Claim;>0:Edit
            ', @BenefitMasterHeaderID = 29 
            ', @BenefitTypeID=13
            ', @LeasingCompanyID = 8
            ', @IsDebug=0

            Dim SQL As String
            Dim SQLOpt As String = ""
            Dim SQLWhere As String
            Dim SQLOrder As String

            SQL = "exec sp_checkInputClaim " & EndCustomerID & ", " & BenefitClaimDetailID & ", " & BenefitMasterHeaderID & "," & BenefitTypeID & "," & LeasingCompanyID & "," & IsDebug & ""

            Return m_sp_checkInputClaimMapper.RetrieveSP(SQL)
        End Function

        Public Function UploadLeasingValidation(ByVal LeasingCompanyID As Integer, ByVal ChassisNumber As String, ByVal IsDebug As Integer) As ArrayList

            Dim SQL As String
            Dim SQLOpt As String = ""
            Dim SQLWhere As String
            Dim SQLOrder As String

            SQL = "exec sp_checkUploadLeasing " & LeasingCompanyID & ", " & ChassisNumber & "," & IsDebug & ""

            Return m_sp_checkInputClaimMapper.RetrieveSP(SQL)
        End Function

        Public Function RetrieveKTP(ByVal EndCustomerID As Integer, ByVal BenefitEventHeaderID As Integer) As ArrayList

            '  exec sp_checkInputClaim_Donas @EndCustomerID=1292976
            ', @BenefitClaimDetailID = 0 --0:Input Claim;>0:Edit
            ', @BenefitMasterHeaderID = 29 
            ', @BenefitTypeID=13
            ', @LeasingCompanyID = 8
            ', @IsDebug=0

            Dim SQL As String
            Dim SQLOpt As String = ""
            Dim SQLWhere As String
            Dim SQLOrder As String

            SQL = "exec sp_checkKTPBenefitEvent " & EndCustomerID & ", " & BenefitEventHeaderID.ToString()

            Return m_sp_checkInputClaimMapper.RetrieveSP(SQL)
        End Function
#End Region

    End Class

End Namespace
