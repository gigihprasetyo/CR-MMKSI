
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DSFLeasingClaim Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 10/12/2019 - 16:43:31
'//
'// ===========================================================================	
#End Region


#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Data
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports Microsoft.Practices.EnterpriseLibrary.Logging
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.DataMapper

    Public Class DSFLeasingClaimMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDSFLeasingClaim"
        Private m_UpdateStatement As String = "up_UpdateDSFLeasingClaim"
        Private m_RetrieveStatement As String = "up_RetrieveDSFLeasingClaim"
        Private m_RetrieveListStatement As String = "up_RetrieveDSFLeasingClaimList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDSFLeasingClaim"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim dSFLeasingClaim As DSFLeasingClaim = Nothing
            While dr.Read

                dSFLeasingClaim = Me.CreateObject(dr)

            End While

            Return dSFLeasingClaim

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim dSFLeasingClaimList As ArrayList = New ArrayList

            While dr.Read
                Dim dSFLeasingClaim As DSFLeasingClaim = Me.CreateObject(dr)
                dSFLeasingClaimList.Add(dSFLeasingClaim)
            End While

            Return dSFLeasingClaimList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dSFLeasingClaim As DSFLeasingClaim = CType(obj, DSFLeasingClaim)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dSFLeasingClaim.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dSFLeasingClaim As DSFLeasingClaim = CType(obj, DSFLeasingClaim)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)

            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@RegNumber", DbType.AnsiString, dSFLeasingClaim.RegNumber)
            DbCommandWrapper.AddInParameter("@ClaimDate", DbType.DateTime, dSFLeasingClaim.ClaimDate)
            DbCommandWrapper.AddInParameter("@AssetSeqNo", DbType.Int32, dSFLeasingClaim.AssetSeqNo)
            DbCommandWrapper.AddInParameter("@AgreementNo", DbType.AnsiString, dSFLeasingClaim.AgreementNo)
            DbCommandWrapper.AddInParameter("@SKDNumber", DbType.AnsiString, dSFLeasingClaim.SKDNumber)
            DbCommandWrapper.AddInParameter("@SKDDate", DbType.DateTime, dSFLeasingClaim.SKDDate)
            DbCommandWrapper.AddInParameter("@SKDApprovalDate", DbType.DateTime, dSFLeasingClaim.SKDApprovalDate)
            DbCommandWrapper.AddInParameter("@GoLiveDate", DbType.DateTime, dSFLeasingClaim.GoLiveDate)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.AnsiString, dSFLeasingClaim.CustomerName)
            DbCommandWrapper.AddInParameter("@Unit", DbType.Int32, dSFLeasingClaim.Unit)
            DbCommandWrapper.AddInParameter("@ObjectLease", DbType.AnsiString, dSFLeasingClaim.ObjectLease)
            DbCommandWrapper.AddInParameter("@ATPMSubsidy", DbType.Currency, dSFLeasingClaim.ATPMSubsidy)
            DbCommandWrapper.AddInParameter("@SupplierName", DbType.AnsiString, dSFLeasingClaim.SupplierName)
            DbCommandWrapper.AddInParameter("@ProgramName", DbType.AnsiString, dSFLeasingClaim.ProgramName)
            DbCommandWrapper.AddInParameter("@CollectionPeriodMonth", DbType.Byte, dSFLeasingClaim.CollectionPeriodMonth)
            DbCommandWrapper.AddInParameter("@CollectionPeriodYear", DbType.Int16, dSFLeasingClaim.CollectionPeriodYear)
            DbCommandWrapper.AddInParameter("@TotalDP", DbType.Currency, dSFLeasingClaim.TotalDP)
            DbCommandWrapper.AddInParameter("@TotalAmountLease", DbType.Currency, dSFLeasingClaim.TotalAmountLease)
            DbCommandWrapper.AddInParameter("@PeriodLease", DbType.Int32, dSFLeasingClaim.PeriodLease)
            DbCommandWrapper.AddInParameter("@InterestLease", DbType.Currency, dSFLeasingClaim.InterestLease)
            DbCommandWrapper.AddInParameter("@Insurance", DbType.AnsiString, dSFLeasingClaim.Insurance)
            DbCommandWrapper.AddInParameter("@TypeInsurance", DbType.AnsiString, dSFLeasingClaim.TypeInsurance)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, dSFLeasingClaim.Status)
            DbCommandWrapper.AddInParameter("@RemarkByDealer", DbType.AnsiString, dSFLeasingClaim.RemarkByDealer)
            DbCommandWrapper.AddInParameter("@RemarkByDSF", DbType.AnsiString, dSFLeasingClaim.RemarkByDSF)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dSFLeasingClaim.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, dSFLeasingClaim.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@BenefitClaimHeaderID", DbType.Int32, Me.GetRefObject(dSFLeasingClaim.BenefitClaimHeader))
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(dSFLeasingClaim.ChassisMaster))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(dSFLeasingClaim.Dealer))

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@ID"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DbCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DbCommandWrapper.AddInParameter("@PK", DbType.String, "ID")

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DinamicQuery)
            DbCommandWrapper.AddInParameter("@sqlQuery", DbType.String, "SELECT " + m_TableName + ".* FROM " + m_TableName)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveListParameter() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveListStatement)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveParameter(ByVal id As Integer) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveStatement)
            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, id)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dSFLeasingClaim As DSFLeasingClaim = CType(obj, DSFLeasingClaim)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dSFLeasingClaim.ID)
            DbCommandWrapper.AddInParameter("@RegNumber", DbType.AnsiString, dSFLeasingClaim.RegNumber)
            DbCommandWrapper.AddInParameter("@ClaimDate", DbType.DateTime, dSFLeasingClaim.ClaimDate)
            DbCommandWrapper.AddInParameter("@AssetSeqNo", DbType.Int32, dSFLeasingClaim.AssetSeqNo)
            DbCommandWrapper.AddInParameter("@AgreementNo", DbType.AnsiString, dSFLeasingClaim.AgreementNo)
            DbCommandWrapper.AddInParameter("@SKDNumber", DbType.AnsiString, dSFLeasingClaim.SKDNumber)
            DbCommandWrapper.AddInParameter("@SKDDate", DbType.DateTime, dSFLeasingClaim.SKDDate)
            DbCommandWrapper.AddInParameter("@SKDApprovalDate", DbType.DateTime, dSFLeasingClaim.SKDApprovalDate)
            DbCommandWrapper.AddInParameter("@GoLiveDate", DbType.DateTime, dSFLeasingClaim.GoLiveDate)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.AnsiString, dSFLeasingClaim.CustomerName)
            DbCommandWrapper.AddInParameter("@Unit", DbType.Int32, dSFLeasingClaim.Unit)
            DbCommandWrapper.AddInParameter("@ObjectLease", DbType.AnsiString, dSFLeasingClaim.ObjectLease)
            DbCommandWrapper.AddInParameter("@ATPMSubsidy", DbType.Currency, dSFLeasingClaim.ATPMSubsidy)
            DbCommandWrapper.AddInParameter("@SupplierName", DbType.AnsiString, dSFLeasingClaim.SupplierName)
            DbCommandWrapper.AddInParameter("@ProgramName", DbType.AnsiString, dSFLeasingClaim.ProgramName)
            DbCommandWrapper.AddInParameter("@CollectionPeriodMonth", DbType.Byte, dSFLeasingClaim.CollectionPeriodMonth)
            DbCommandWrapper.AddInParameter("@CollectionPeriodYear", DbType.Int16, dSFLeasingClaim.CollectionPeriodYear)
            DbCommandWrapper.AddInParameter("@TotalDP", DbType.Currency, dSFLeasingClaim.TotalDP)
            DbCommandWrapper.AddInParameter("@TotalAmountLease", DbType.Currency, dSFLeasingClaim.TotalAmountLease)
            DbCommandWrapper.AddInParameter("@PeriodLease", DbType.Int32, dSFLeasingClaim.PeriodLease)
            DbCommandWrapper.AddInParameter("@InterestLease", DbType.Currency, dSFLeasingClaim.InterestLease)
            DbCommandWrapper.AddInParameter("@Insurance", DbType.AnsiString, dSFLeasingClaim.Insurance)
            DbCommandWrapper.AddInParameter("@TypeInsurance", DbType.AnsiString, dSFLeasingClaim.TypeInsurance)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, dSFLeasingClaim.Status)
            DbCommandWrapper.AddInParameter("@RemarkByDealer", DbType.AnsiString, dSFLeasingClaim.RemarkByDealer)
            DbCommandWrapper.AddInParameter("@RemarkByDSF", DbType.AnsiString, dSFLeasingClaim.RemarkByDSF)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dSFLeasingClaim.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, dSFLeasingClaim.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@BenefitClaimHeaderID", DbType.Int32, Me.GetRefObject(dSFLeasingClaim.BenefitClaimHeader))
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(dSFLeasingClaim.ChassisMaster))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(dSFLeasingClaim.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DSFLeasingClaim

            Dim dSFLeasingClaim As DSFLeasingClaim = New DSFLeasingClaim

            dSFLeasingClaim.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RegNumber")) Then dSFLeasingClaim.RegNumber = dr("RegNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ClaimDate")) Then dSFLeasingClaim.ClaimDate = CType(dr("ClaimDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("AssetSeqNo")) Then dSFLeasingClaim.AssetSeqNo = CType(dr("AssetSeqNo"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("AgreementNo")) Then dSFLeasingClaim.AgreementNo = dr("AgreementNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SKDNumber")) Then dSFLeasingClaim.SKDNumber = dr("SKDNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SKDDate")) Then dSFLeasingClaim.SKDDate = CType(dr("SKDDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SKDApprovalDate")) Then dSFLeasingClaim.SKDApprovalDate = CType(dr("SKDApprovalDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("GoLiveDate")) Then dSFLeasingClaim.GoLiveDate = CType(dr("GoLiveDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerName")) Then dSFLeasingClaim.CustomerName = dr("CustomerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Unit")) Then dSFLeasingClaim.Unit = CType(dr("Unit"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ObjectLease")) Then dSFLeasingClaim.ObjectLease = dr("ObjectLease").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ATPMSubsidy")) Then dSFLeasingClaim.ATPMSubsidy = CType(dr("ATPMSubsidy"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("SupplierName")) Then dSFLeasingClaim.SupplierName = dr("SupplierName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProgramName")) Then dSFLeasingClaim.ProgramName = dr("ProgramName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CollectionPeriodMonth")) Then dSFLeasingClaim.CollectionPeriodMonth = CType(dr("CollectionPeriodMonth"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("CollectionPeriodYear")) Then dSFLeasingClaim.CollectionPeriodYear = CType(dr("CollectionPeriodYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalDP")) Then dSFLeasingClaim.TotalDP = CType(dr("TotalDP"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalAmountLease")) Then dSFLeasingClaim.TotalAmountLease = CType(dr("TotalAmountLease"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodLease")) Then dSFLeasingClaim.PeriodLease = CType(dr("PeriodLease"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("InterestLease")) Then dSFLeasingClaim.InterestLease = CType(dr("InterestLease"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Insurance")) Then dSFLeasingClaim.Insurance = dr("Insurance").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TypeInsurance")) Then dSFLeasingClaim.TypeInsurance = dr("TypeInsurance").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then dSFLeasingClaim.Status = CType(dr("Status"), Short)

            If Not dr.IsDBNull(dr.GetOrdinal("RemarkByDealer")) Then dSFLeasingClaim.RemarkByDealer = dr("RemarkByDealer").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RemarkByDSF")) Then dSFLeasingClaim.RemarkByDSF = dr("RemarkByDSF").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then dSFLeasingClaim.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then dSFLeasingClaim.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then dSFLeasingClaim.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then dSFLeasingClaim.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then dSFLeasingClaim.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("BenefitClaimHeaderID")) Then
                dSFLeasingClaim.BenefitClaimHeader = New BenefitClaimHeader(CType(dr("BenefitClaimHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then
                dSFLeasingClaim.ChassisMaster = New ChassisMaster(CType(dr("ChassisMasterID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                dSFLeasingClaim.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If

            Return dSFLeasingClaim

        End Function

        Private Sub SetTableName()

            If Not (GetType(DSFLeasingClaim) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DSFLeasingClaim), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DSFLeasingClaim).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

