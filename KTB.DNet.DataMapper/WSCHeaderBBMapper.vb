#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : WSCHeaderBB Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/29/2005 - 10:57:46 AM
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

    Public Class WSCHeaderBBMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertWSCHeaderBB"
        Private m_UpdateStatement As String = "up_UpdateWSCHeaderBB"
        Private m_RetrieveStatement As String = "up_RetrieveWSCHeaderBB"
        Private m_RetrieveListStatement As String = "up_RetrieveWSCHeaderBBList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteWSCHeaderBB"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim WSCHeaderBB As WSCHeaderBB = Nothing
            While dr.Read

                WSCHeaderBB = Me.CreateObject(dr)

            End While

            Return WSCHeaderBB

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim WSCHeaderBBList As ArrayList = New ArrayList

            While dr.Read
                Dim WSCHeaderBB As WSCHeaderBB = Me.CreateObject(dr)
                WSCHeaderBBList.Add(WSCHeaderBB)
            End While

            Return WSCHeaderBBList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim WSCHeaderBB As WSCHeaderBB = CType(obj, WSCHeaderBB)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, WSCHeaderBB.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim WSCHeaderBB As WSCHeaderBB = CType(obj, WSCHeaderBB)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)
            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@ClaimType", DbType.AnsiString, WSCHeaderBB.ClaimType)
            DbCommandWrapper.AddInParameter("@ClaimNumber", DbType.AnsiString, WSCHeaderBB.ClaimNumber)
            DbCommandWrapper.AddInParameter("@RefClaimNumber", DbType.AnsiString, WSCHeaderBB.RefClaimNumber)
            DbCommandWrapper.AddInParameter("@FailureDate", DbType.DateTime, wSCHeaderbb.FailureDate)
            DbCommandWrapper.AddInParameter("@ServiceDate", DbType.DateTime, WSCHeaderBB.ServiceDate)
            DbCommandWrapper.AddInParameter("@Miliage", DbType.Int32, WSCHeaderBB.Miliage)
            DbCommandWrapper.AddInParameter("@PQR", DbType.AnsiString, WSCHeaderBB.PQR)
            DbCommandWrapper.AddInParameter("@PQRStatus", DbType.AnsiString, WSCHeaderBB.PQRStatus)
            DbCommandWrapper.AddInParameter("@CodeA", DbType.AnsiString, WSCHeaderBB.CodeA)
            DbCommandWrapper.AddInParameter("@CodeB", DbType.AnsiString, WSCHeaderBB.CodeB)
            DbCommandWrapper.AddInParameter("@CodeC", DbType.AnsiString, WSCHeaderBB.CodeC)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, WSCHeaderBB.Description)
            DbCommandWrapper.AddInParameter("@EvidencePhoto", DbType.AnsiString, WSCHeaderBB.EvidencePhoto)
            DbCommandWrapper.AddInParameter("@EvidenceInvoice", DbType.AnsiString, WSCHeaderBB.EvidenceInvoice)
            DbCommandWrapper.AddInParameter("@EvidenceDmgPart", DbType.AnsiString, WSCHeaderBB.EvidenceDmgPart)
            DbCommandWrapper.AddInParameter("@EvidenceRepair", DbType.AnsiString, wSCHeaderbb.EvidenceRepair)
            DbCommandWrapper.AddInParameter("@EvidenceWSCLetter", DbType.AnsiString, wSCHeaderbb.EvidenceWSCLetter)
            DbCommandWrapper.AddInParameter("@EvidenceWSCTechnical", DbType.AnsiString, wSCHeaderbb.EvidenceWSCTechnical)
            DbCommandWrapper.AddInParameter("@Causes", DbType.AnsiString, wSCHeaderbb.Causes)
            DbCommandWrapper.AddInParameter("@Results", DbType.AnsiString, wSCHeaderbb.Results)
            DbCommandWrapper.AddInParameter("@Notes", DbType.AnsiString, WSCHeaderBB.Notes)
            DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, WSCHeaderBB.WorkOrderNumber)
            DbCommandWrapper.AddInParameter("@ReqDmgPart", DbType.AnsiString, WSCHeaderBB.ReqDmgPart)
            DbCommandWrapper.AddInParameter("@ReqDmgPartBy", DbType.AnsiString, WSCHeaderBB.ReqDmgPartBy)
            DbCommandWrapper.AddInParameter("@ReqDmgPartTime", DbType.DateTime, WSCHeaderBB.ReqDmgPartTime)
            DbCommandWrapper.AddInParameter("@NotificationNumber", DbType.AnsiString, WSCHeaderBB.NotificationNumber)
            DbCommandWrapper.AddInParameter("@DecideDate", DbType.DateTime, WSCHeaderBB.DecideDate)
            DbCommandWrapper.AddInParameter("@ReleaseDate", DbType.DateTime, WSCHeaderBB.ReleaseDate)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, WSCHeaderBB.Status)
            DbCommandWrapper.AddInParameter("@ClaimStatus", DbType.AnsiString, WSCHeaderBB.ClaimStatus)
            DbCommandWrapper.AddInParameter("@LaborAmount", DbType.Currency, WSCHeaderBB.LaborAmount)
            DbCommandWrapper.AddInParameter("@PartAmount", DbType.Currency, WSCHeaderBB.PartAmount)
            DbCommandWrapper.AddInParameter("@PartReceiveBy", DbType.AnsiString, WSCHeaderBB.PartReceiveBy)
            DbCommandWrapper.AddInParameter("@PartReceiveTime", DbType.DateTime, WSCHeaderBB.PartReceiveTime)
            DbCommandWrapper.AddInParameter("@DownLoadBy", DbType.AnsiString, WSCHeaderBB.DownLoadBy)
            DbCommandWrapper.AddInParameter("@DownLoadTime", DbType.DateTime, WSCHeaderBB.DownLoadTime)
            DbCommandWrapper.AddInParameter("@ResponseTime", DbType.DateTime, WSCHeaderBB.ResponseTime)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, WSCHeaderBB.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, WSCHeaderBB.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ReasonID", DbType.Int32, Me.GetRefObject(WSCHeaderBB.Reason))
            DBCommandWrapper.AddInParameter("@ChassisMasterBBID", DbType.Int32, Me.GetRefObject(WSCHeaderBB.ChassisMasterBB))
            DBCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(WSCHeaderBB.Dealer))
	    DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(wSCHeaderBB.DealerBranch))

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@ID"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DBCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DBCommandWrapper.AddInParameter("@PK", DbType.String, "ID")

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DinamicQuery)
            DBCommandWrapper.AddInParameter("@sqlQuery", DbType.String, "SELECT " + m_TableName + ".* FROM " + m_TableName)

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveListParameter() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveListStatement)
            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveParameter(ByVal id As Integer) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveStatement)
            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, id)

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim WSCHeaderBB As WSCHeaderBB = CType(obj, WSCHeaderBB)

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, WSCHeaderBB.ID)
            DBCommandWrapper.AddInParameter("@ClaimType", DbType.AnsiString, WSCHeaderBB.ClaimType)
            DBCommandWrapper.AddInParameter("@ClaimNumber", DbType.AnsiString, WSCHeaderBB.ClaimNumber)
            DBCommandWrapper.AddInParameter("@RefClaimNumber", DbType.AnsiString, WSCHeaderBB.RefClaimNumber)
            DBCommandWrapper.AddInParameter("@ServiceDate", DbType.DateTime, WSCHeaderBB.ServiceDate)
            DbCommandWrapper.AddInParameter("@FailureDate", DbType.DateTime, wSCHeaderbb.FailureDate)
            DBCommandWrapper.AddInParameter("@Miliage", DbType.Int32, WSCHeaderBB.Miliage)
            DBCommandWrapper.AddInParameter("@PQR", DbType.AnsiString, WSCHeaderBB.PQR)
            DBCommandWrapper.AddInParameter("@PQRStatus", DbType.AnsiString, WSCHeaderBB.PQRStatus)
            DBCommandWrapper.AddInParameter("@CodeA", DbType.AnsiString, WSCHeaderBB.CodeA)
            DBCommandWrapper.AddInParameter("@CodeB", DbType.AnsiString, WSCHeaderBB.CodeB)
            DBCommandWrapper.AddInParameter("@CodeC", DbType.AnsiString, WSCHeaderBB.CodeC)
            DBCommandWrapper.AddInParameter("@Description", DbType.AnsiString, WSCHeaderBB.Description)
            DBCommandWrapper.AddInParameter("@EvidencePhoto", DbType.AnsiString, WSCHeaderBB.EvidencePhoto)
            DBCommandWrapper.AddInParameter("@EvidenceInvoice", DbType.AnsiString, WSCHeaderBB.EvidenceInvoice)
            DBCommandWrapper.AddInParameter("@EvidenceDmgPart", DbType.AnsiString, WSCHeaderBB.EvidenceDmgPart)
            DbCommandWrapper.AddInParameter("@EvidenceRepair", DbType.AnsiString, wSCHeaderbb.EvidenceRepair)
            DbCommandWrapper.AddInParameter("@EvidenceWSCLetter", DbType.AnsiString, wSCHeaderbb.EvidenceWSCLetter)
            DbCommandWrapper.AddInParameter("@EvidenceWSCTechnical", DbType.AnsiString, wSCHeaderbb.EvidenceWSCTechnical)
            DbCommandWrapper.AddInParameter("@Causes", DbType.AnsiString, wSCHeaderbb.Causes)
            DbCommandWrapper.AddInParameter("@Results", DbType.AnsiString, wSCHeaderbb.Results)
            DbCommandWrapper.AddInParameter("@Notes", DbType.AnsiString, WSCHeaderBB.Notes)
            DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, WSCHeaderBB.WorkOrderNumber)
            DBCommandWrapper.AddInParameter("@ReqDmgPart", DbType.AnsiString, WSCHeaderBB.ReqDmgPart)
            DBCommandWrapper.AddInParameter("@ReqDmgPartBy", DbType.AnsiString, WSCHeaderBB.ReqDmgPartBy)
            DBCommandWrapper.AddInParameter("@ReqDmgPartTime", DbType.DateTime, WSCHeaderBB.ReqDmgPartTime)
            DBCommandWrapper.AddInParameter("@NotificationNumber", DbType.AnsiString, WSCHeaderBB.NotificationNumber)
            DBCommandWrapper.AddInParameter("@DecideDate", DbType.DateTime, WSCHeaderBB.DecideDate)
            DbCommandWrapper.AddInParameter("@ReleaseDate", DbType.DateTime, WSCHeaderBB.ReleaseDate)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, WSCHeaderBB.Status)
            DBCommandWrapper.AddInParameter("@ClaimStatus", DbType.AnsiString, WSCHeaderBB.ClaimStatus)
            DBCommandWrapper.AddInParameter("@LaborAmount", DbType.Currency, WSCHeaderBB.LaborAmount)
            DBCommandWrapper.AddInParameter("@PartAmount", DbType.Currency, WSCHeaderBB.PartAmount)
            DBCommandWrapper.AddInParameter("@PartReceiveBy", DbType.AnsiString, WSCHeaderBB.PartReceiveBy)
            DBCommandWrapper.AddInParameter("@PartReceiveTime", DbType.DateTime, WSCHeaderBB.PartReceiveTime)
            DBCommandWrapper.AddInParameter("@DownLoadBy", DbType.AnsiString, WSCHeaderBB.DownLoadBy)
            DBCommandWrapper.AddInParameter("@DownLoadTime", DbType.DateTime, WSCHeaderBB.DownLoadTime)
            DBCommandWrapper.AddInParameter("@ResponseTime", DbType.DateTime, WSCHeaderBB.ResponseTime)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, WSCHeaderBB.RowStatus)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, WSCHeaderBB.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DBCommandWrapper.AddInParameter("@ReasonID", DbType.Int32, Me.GetRefObject(WSCHeaderBB.Reason))
            DBCommandWrapper.AddInParameter("@ChassisMasterBBID", DbType.Int32, Me.GetRefObject(WSCHeaderBB.ChassisMasterBB))
            DBCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(WSCHeaderBB.Dealer))

            Return DBCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As WSCHeaderBB

            Dim WSCHeaderBB As WSCHeaderBB = New WSCHeaderBB

            WSCHeaderBB.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ClaimType")) Then WSCHeaderBB.ClaimType = dr("ClaimType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ClaimNumber")) Then WSCHeaderBB.ClaimNumber = dr("ClaimNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RefClaimNumber")) Then WSCHeaderBB.RefClaimNumber = dr("RefClaimNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FailureDate")) Then wSCHeaderbb.FailureDate = CType(dr("FailureDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceDate")) Then WSCHeaderBB.ServiceDate = CType(dr("ServiceDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Miliage")) Then WSCHeaderBB.Miliage = CType(dr("Miliage"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PQR")) Then WSCHeaderBB.PQR = dr("PQR").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PQRStatus")) Then WSCHeaderBB.PQRStatus = dr("PQRStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CodeA")) Then WSCHeaderBB.CodeA = dr("CodeA").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CodeB")) Then WSCHeaderBB.CodeB = dr("CodeB").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CodeC")) Then WSCHeaderBB.CodeC = dr("CodeC").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then WSCHeaderBB.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EvidencePhoto")) Then WSCHeaderBB.EvidencePhoto = dr("EvidencePhoto").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EvidenceInvoice")) Then WSCHeaderBB.EvidenceInvoice = dr("EvidenceInvoice").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EvidenceDmgPart")) Then WSCHeaderBB.EvidenceDmgPart = dr("EvidenceDmgPart").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EvidenceRepair")) Then wSCHeaderbb.EvidenceRepair = dr("EvidenceRepair").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EvidenceWSCLetter")) Then wSCHeaderbb.EvidenceWSCLetter = dr("EvidenceWSCLetter").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EvidenceWSCTechnical")) Then wSCHeaderbb.EvidenceWSCTechnical = dr("EvidenceWSCTechnical").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Causes")) Then wSCHeaderbb.Causes = dr("Causes").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Results")) Then wSCHeaderbb.Results = dr("Results").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Notes")) Then WSCHeaderBB.Notes = dr("Notes").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("WorkOrderNumber")) Then WSCHeaderBB.WorkOrderNumber = dr("WorkOrderNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReqDmgPart")) Then WSCHeaderBB.ReqDmgPart = dr("ReqDmgPart").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReqDmgPartBy")) Then WSCHeaderBB.ReqDmgPartBy = dr("ReqDmgPartBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReqDmgPartTime")) Then WSCHeaderBB.ReqDmgPartTime = CType(dr("ReqDmgPartTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("NotificationNumber")) Then WSCHeaderBB.NotificationNumber = dr("NotificationNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DecideDate")) Then WSCHeaderBB.DecideDate = CType(dr("DecideDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ReleaseDate")) Then WSCHeaderBB.ReleaseDate = CType(dr("ReleaseDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then WSCHeaderBB.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ClaimStatus")) Then WSCHeaderBB.ClaimStatus = dr("ClaimStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LaborAmount")) Then WSCHeaderBB.LaborAmount = CType(dr("LaborAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PartAmount")) Then WSCHeaderBB.PartAmount = CType(dr("PartAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PartReceiveBy")) Then WSCHeaderBB.PartReceiveBy = dr("PartReceiveBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PartReceiveTime")) Then WSCHeaderBB.PartReceiveTime = CType(dr("PartReceiveTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DownLoadBy")) Then WSCHeaderBB.DownLoadBy = dr("DownLoadBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DownLoadTime")) Then WSCHeaderBB.DownLoadTime = CType(dr("DownLoadTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ResponseTime")) Then WSCHeaderBB.ResponseTime = CType(dr("ResponseTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then WSCHeaderBB.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then WSCHeaderBB.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then WSCHeaderBB.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then WSCHeaderBB.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then WSCHeaderBB.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ReasonID")) Then
                WSCHeaderBB.Reason = New Reason(CType(dr("ReasonID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterBBID")) Then
                WSCHeaderBB.ChassisMasterBB = New ChassisMasterBB(CType(dr("ChassisMasterBBID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                WSCHeaderBB.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            Return WSCHeaderBB

        End Function

        Private Sub SetTableName()

            If Not (GetType(WSCHeaderBB) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(WSCHeaderBB), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(WSCHeaderBB).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

