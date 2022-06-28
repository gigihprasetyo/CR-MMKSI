
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SPKHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 3/7/2011 - 11:45:51 AM
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

    Public Class SPKHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSPKHeader"
        Private m_UpdateStatement As String = "up_UpdateSPKHeader"
        Private m_RetrieveStatement As String = "up_RetrieveSPKHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveSPKHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSPKHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sPKHeader As SPKHeader = Nothing
            While dr.Read

                sPKHeader = Me.CreateObject(dr)

            End While

            Return sPKHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sPKHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim sPKHeader As SPKHeader = Me.CreateObject(dr)
                sPKHeaderList.Add(sPKHeader)
            End While

            Return sPKHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sPKHeader As SPKHeader = CType(obj, SPKHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sPKHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sPKHeader As SPKHeader = CType(obj, SPKHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, sPKHeader.Status)
            DbCommandWrapper.AddInParameter("@SPKNumber", DbType.AnsiString, sPKHeader.SPKNumber)
            DbCommandWrapper.AddInParameter("@SPKReferenceNumber", DbType.AnsiString, sPKHeader.SPKReferenceNumber)
            DbCommandWrapper.AddInParameter("@DealerSPKNumber", DbType.AnsiString, sPKHeader.DealerSPKNumber)
            DbCommandWrapper.AddInParameter("@IndentNumber", DbType.AnsiString, sPKHeader.IndentNumber)
            DbCommandWrapper.AddInParameter("@PlanDeliveryMonth", DbType.Byte, sPKHeader.PlanDeliveryMonth)
            DbCommandWrapper.AddInParameter("@PlanDeliveryYear", DbType.Int16, sPKHeader.PlanDeliveryYear)
            DbCommandWrapper.AddInParameter("@PlanDeliveryDate", DbType.DateTime, sPKHeader.PlanDeliveryDate)
            DbCommandWrapper.AddInParameter("@PlanInvoiceMonth", DbType.Byte, sPKHeader.PlanInvoiceMonth)
            DbCommandWrapper.AddInParameter("@PlanInvoiceYear", DbType.Int16, sPKHeader.PlanInvoiceYear)
            DbCommandWrapper.AddInParameter("@PlanInvoiceDate", DbType.DateTime, sPKHeader.PlanInvoiceDate)
            DbCommandWrapper.AddInParameter("@CustomerRequestID", DbType.Int32, sPKHeader.CustomerRequestID)
            DbCommandWrapper.AddInParameter("@ValidateTime", DbType.DateTime, sPKHeader.ValidateTime)
            DbCommandWrapper.AddInParameter("@ValidateBy", DbType.String, sPKHeader.ValidateBy)
            DbCommandWrapper.AddInParameter("@RejectedReason", DbType.String, sPKHeader.RejectedReason)
            DbCommandWrapper.AddInParameter("@EvidenceFile", DbType.String, sPKHeader.EvidenceFile)
            DbCommandWrapper.AddInParameter("@ValidationKey", DbType.String, sPKHeader.ValidationKey)
            DbCommandWrapper.AddInParameter("@FlagUpdate", DbType.Int16, sPKHeader.FlagUpdate)
            DbCommandWrapper.AddInParameter("@IsSend", DbType.Int16, sPKHeader.IsSend)
            DbCommandWrapper.AddInParameter("@DealerSPKDate", DbType.DateTime, sPKHeader.DealerSPKDate)
            DbCommandWrapper.AddInParameter("@BenefitMasterHeaderID", DbType.Int32, Me.GetRefObject(sPKHeader.BenefitMasterHeader))
            DbCommandWrapper.AddInParameter("@EventType", DbType.Int32, sPKHeader.EventType)
            DbCommandWrapper.AddInParameter("@CampaignName", DbType.String, sPKHeader.CampaignName)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sPKHeader.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.String, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.String, sPKHeader.LastUpdateBy)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(sPKHeader.Dealer))
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(sPKHeader.SalesmanHeader))
            DbCommandWrapper.AddInParameter("@SPKCustomerID", DbType.Int32, Me.GetRefObject(sPKHeader.SPKCustomer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(sPKHeader.DealerBranch))

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

            Dim sPKHeader As SPKHeader = CType(obj, SPKHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sPKHeader.ID)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, sPKHeader.Status)
            DbCommandWrapper.AddInParameter("@SPKNumber", DbType.AnsiString, sPKHeader.SPKNumber)
            DbCommandWrapper.AddInParameter("@SPKReferenceNumber", DbType.AnsiString, sPKHeader.SPKReferenceNumber)
            DbCommandWrapper.AddInParameter("@DealerSPKNumber", DbType.AnsiString, sPKHeader.DealerSPKNumber)
            DbCommandWrapper.AddInParameter("@IndentNumber", DbType.AnsiString, sPKHeader.IndentNumber)
            DbCommandWrapper.AddInParameter("@PlanDeliveryMonth", DbType.Byte, sPKHeader.PlanDeliveryMonth)
            DbCommandWrapper.AddInParameter("@PlanDeliveryYear", DbType.Int16, sPKHeader.PlanDeliveryYear)
            DbCommandWrapper.AddInParameter("@PlanDeliveryDate", DbType.DateTime, sPKHeader.PlanDeliveryDate)
            DbCommandWrapper.AddInParameter("@PlanInvoiceMonth", DbType.Byte, sPKHeader.PlanInvoiceMonth)
            DbCommandWrapper.AddInParameter("@PlanInvoiceYear", DbType.Int16, sPKHeader.PlanInvoiceYear)
            DbCommandWrapper.AddInParameter("@PlanInvoiceDate", DbType.DateTime, sPKHeader.PlanInvoiceDate)
            DbCommandWrapper.AddInParameter("@CustomerRequestID", DbType.Int32, sPKHeader.CustomerRequestID)
            DbCommandWrapper.AddInParameter("@ValidateTime", DbType.DateTime, sPKHeader.ValidateTime)
            DbCommandWrapper.AddInParameter("@ValidateBy", DbType.String, sPKHeader.ValidateBy)
            DbCommandWrapper.AddInParameter("@RejectedReason", DbType.String, sPKHeader.RejectedReason)
            DbCommandWrapper.AddInParameter("@EvidenceFile", DbType.String, sPKHeader.EvidenceFile)
            DbCommandWrapper.AddInParameter("@ValidationKey", DbType.String, sPKHeader.ValidationKey)
            DbCommandWrapper.AddInParameter("@FlagUpdate", DbType.Int16, sPKHeader.FlagUpdate)
            DbCommandWrapper.AddInParameter("@IsSend", DbType.Int16, sPKHeader.IsSend)
            DbCommandWrapper.AddInParameter("@DealerSPKDate", DbType.DateTime, sPKHeader.DealerSPKDate)
            DbCommandWrapper.AddInParameter("@BenefitMasterHeaderID", DbType.Int32, Me.GetRefObject(sPKHeader.BenefitMasterHeader))
            DbCommandWrapper.AddInParameter("@EventType", DbType.Int32, sPKHeader.EventType)
            DbCommandWrapper.AddInParameter("@CampaignName", DbType.String, sPKHeader.CampaignName)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sPKHeader.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.String, sPKHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.String, User)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(sPKHeader.Dealer))
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(sPKHeader.SalesmanHeader))
            DbCommandWrapper.AddInParameter("@SPKCustomerID", DbType.Int32, Me.GetRefObject(sPKHeader.SPKCustomer))
            'DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int16, Me.GetRefObject(sPKHeader.DealerBranch))

            'farid modified 20190115
            If sPKHeader.DealerBranch Is Nothing Then
                DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int16, DBNull.Value)
            Else
                DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int16, Me.GetRefObject(sPKHeader.DealerBranch))
            End If

            Return DbCommandWrapper

        End Function

#End Region

#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SPKHeader

            Dim sPKHeader As SPKHeader = New SPKHeader

            sPKHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then sPKHeader.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SPKNumber")) Then sPKHeader.SPKNumber = dr("SPKNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SPKReferenceNumber")) Then sPKHeader.SPKReferenceNumber = dr("SPKReferenceNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerSPKNumber")) Then sPKHeader.DealerSPKNumber = dr("DealerSPKNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IndentNumber")) Then sPKHeader.IndentNumber = dr("IndentNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PlanDeliveryMonth")) Then sPKHeader.PlanDeliveryMonth = CType(dr("PlanDeliveryMonth"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("PlanDeliveryYear")) Then sPKHeader.PlanDeliveryYear = CType(dr("PlanDeliveryYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PlanDeliveryDate")) Then sPKHeader.PlanDeliveryDate = CType(dr("PlanDeliveryDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PlanInvoiceMonth")) Then sPKHeader.PlanInvoiceMonth = CType(dr("PlanInvoiceMonth"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("PlanInvoiceYear")) Then sPKHeader.PlanInvoiceYear = CType(dr("PlanInvoiceYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PlanInvoiceDate")) Then sPKHeader.PlanInvoiceDate = CType(dr("PlanInvoiceDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerRequestID")) Then sPKHeader.CustomerRequestID = CType(dr("CustomerRequestID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidateTime")) Then sPKHeader.ValidateTime = CType(dr("ValidateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidateBy")) Then sPKHeader.ValidateBy = dr("ValidateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RejectedReason")) Then sPKHeader.RejectedReason = dr("RejectedReason").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EvidenceFile")) Then sPKHeader.EvidenceFile = dr("EvidenceFile").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ValidationKey")) Then sPKHeader.ValidationKey = dr("ValidationKey").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FlagUpdate")) Then sPKHeader.FlagUpdate = CType(dr("FlagUpdate"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("IsSend")) Then sPKHeader.IsSend = CType(dr("IsSend"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerSPKDate")) Then sPKHeader.DealerSPKDate = CType(dr("DealerSPKDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sPKHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sPKHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sPKHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sPKHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sPKHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                sPKHeader.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanHeaderID")) Then
                sPKHeader.SalesmanHeader = New SalesmanHeader(CType(dr("SalesmanHeaderID"), Int32))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SPKCustomerID")) Then
                sPKHeader.SPKCustomer = New SPKCustomer(CType(dr("SPKCustomerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchID")) Then
                sPKHeader.DealerBranch = New DealerBranch(CType(dr("DealerBranchID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("BenefitMasterHeaderID")) Then
                sPKHeader.BenefitMasterHeader = New BenefitMasterHeader(CType(dr("BenefitMasterHeaderID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("EventType")) Then sPKHeader.EventType = CType(dr("EventType"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CampaignName")) Then sPKHeader.CampaignName = dr("CampaignName").ToString

            Return sPKHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(SPKHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SPKHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SPKHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

