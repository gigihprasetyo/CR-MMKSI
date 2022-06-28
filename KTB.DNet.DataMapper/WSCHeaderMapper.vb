#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : WSCHeader Objects Mapper.
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

    Public Class WSCHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertWSCHeader"
        Private m_UpdateStatement As String = "up_UpdateWSCHeader"
        Private m_RetrieveStatement As String = "up_RetrieveWSCHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveWSCHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteWSCHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim wSCHeader As WSCHeader = Nothing
            While dr.Read

                wSCHeader = Me.CreateObject(dr)

            End While

            Return wSCHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim wSCHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim wSCHeader As WSCHeader = Me.CreateObject(dr)
                wSCHeaderList.Add(wSCHeader)
            End While

            Return wSCHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim wSCHeader As WSCHeader = CType(obj, WSCHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, wSCHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim wSCHeader As WSCHeader = CType(obj, WSCHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@ClaimType", DbType.AnsiString, wSCHeader.ClaimType)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(wSCHeader.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(wSCHeader.DealerBranch))
            DbCommandWrapper.AddInParameter("@ClaimNumber", DbType.AnsiString, wSCHeader.ClaimNumber)
            DbCommandWrapper.AddInParameter("@RefClaimNumber", DbType.AnsiString, wSCHeader.RefClaimNumber)
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(wSCHeader.ChassisMaster))
            DbCommandWrapper.AddInParameter("@FailureDate", DbType.DateTime, wSCHeader.FailureDate)
            DbCommandWrapper.AddInParameter("@ServiceDate", DbType.DateTime, wSCHeader.ServiceDate)
            DbCommandWrapper.AddInParameter("@Miliage", DbType.Int32, wSCHeader.Miliage)
            DbCommandWrapper.AddInParameter("@PQR", DbType.AnsiString, wSCHeader.PQR)
            DbCommandWrapper.AddInParameter("@PQRStatus", DbType.AnsiString, wSCHeader.PQRStatus)
            DbCommandWrapper.AddInParameter("@CodeA", DbType.AnsiString, wSCHeader.CodeA)
            DbCommandWrapper.AddInParameter("@CodeB", DbType.AnsiString, wSCHeader.CodeB)
            DbCommandWrapper.AddInParameter("@CodeC", DbType.AnsiString, wSCHeader.CodeC)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, wSCHeader.Description)
            DbCommandWrapper.AddInParameter("@EvidencePhoto", DbType.AnsiString, wSCHeader.EvidencePhoto)
            DbCommandWrapper.AddInParameter("@EvidenceInvoice", DbType.AnsiString, wSCHeader.EvidenceInvoice)
            DbCommandWrapper.AddInParameter("@EvidenceDmgPart", DbType.AnsiString, wSCHeader.EvidenceDmgPart)

            DbCommandWrapper.AddInParameter("@EvidenceRepair", DbType.AnsiString, wSCHeader.EvidenceRepair)
            DbCommandWrapper.AddInParameter("@EvidenceWSCLetter", DbType.AnsiString, wSCHeader.EvidenceWSCLetter)
            DbCommandWrapper.AddInParameter("@EvidenceWSCTechnical", DbType.AnsiString, wSCHeader.EvidenceWSCTechnical)
            DbCommandWrapper.AddInParameter("@Causes", DbType.AnsiString, wSCHeader.Causes)
            DbCommandWrapper.AddInParameter("@Results", DbType.AnsiString, wSCHeader.Results)
            DbCommandWrapper.AddInParameter("@Notes", DbType.AnsiString, wSCHeader.Notes)
            DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, wSCHeader.WorkOrderNumber)

            DbCommandWrapper.AddInParameter("@ReqDmgPart", DbType.AnsiString, wSCHeader.ReqDmgPart)
            DbCommandWrapper.AddInParameter("@ReqDmgPartBy", DbType.AnsiString, wSCHeader.ReqDmgPartBy)
            DbCommandWrapper.AddInParameter("@ReqDmgPartTime", DbType.DateTime, wSCHeader.ReqDmgPartTime)
            DbCommandWrapper.AddInParameter("@NotificationNumber", DbType.AnsiString, wSCHeader.NotificationNumber)
            DbCommandWrapper.AddInParameter("@DecideDate", DbType.DateTime, wSCHeader.DecideDate)
            DbCommandWrapper.AddInParameter("@ReleaseDate", DbType.DateTime, wSCHeader.ReleaseDate)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, wSCHeader.Status)
            DbCommandWrapper.AddInParameter("@ClaimStatus", DbType.AnsiString, wSCHeader.ClaimStatus)
            DbCommandWrapper.AddInParameter("@ReasonID", DbType.Int32, Me.GetRefObject(wSCHeader.Reason))
            DbCommandWrapper.AddInParameter("@LaborAmount", DbType.Currency, wSCHeader.LaborAmount)
            DbCommandWrapper.AddInParameter("@PartAmount", DbType.Currency, wSCHeader.PartAmount)
            DbCommandWrapper.AddInParameter("@PartReceiveBy", DbType.AnsiString, wSCHeader.PartReceiveBy)
            DbCommandWrapper.AddInParameter("@PartReceiveTime", DbType.DateTime, wSCHeader.PartReceiveTime)
            DbCommandWrapper.AddInParameter("@DownLoadBy", DbType.AnsiString, wSCHeader.DownLoadBy)
            DbCommandWrapper.AddInParameter("@DownLoadTime", DbType.DateTime, wSCHeader.DownLoadTime)
            DbCommandWrapper.AddInParameter("@ResponseTime", DbType.DateTime, wSCHeader.ResponseTime)
            DbCommandWrapper.AddInParameter("@InstallDate", DbType.DateTime, wSCHeader.InstallDate)
            DbCommandWrapper.AddInParameter("@InstallMiliage", DbType.Int32, wSCHeader.InstallMiliage)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, wSCHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, wSCHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

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

            Dim wSCHeader As WSCHeader = CType(obj, WSCHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, wSCHeader.ID)
            DbCommandWrapper.AddInParameter("@ClaimType", DbType.AnsiString, wSCHeader.ClaimType)
            DbCommandWrapper.AddInParameter("@ClaimNumber", DbType.AnsiString, wSCHeader.ClaimNumber)
            DbCommandWrapper.AddInParameter("@RefClaimNumber", DbType.AnsiString, wSCHeader.RefClaimNumber)

            DbCommandWrapper.AddInParameter("@FailureDate", DbType.DateTime, wSCHeader.FailureDate)

            DbCommandWrapper.AddInParameter("@ServiceDate", DbType.DateTime, wSCHeader.ServiceDate)
            DbCommandWrapper.AddInParameter("@Miliage", DbType.Int32, wSCHeader.Miliage)
            DbCommandWrapper.AddInParameter("@PQR", DbType.AnsiString, wSCHeader.PQR)
            DbCommandWrapper.AddInParameter("@PQRStatus", DbType.AnsiString, wSCHeader.PQRStatus)
            DbCommandWrapper.AddInParameter("@CodeA", DbType.AnsiString, wSCHeader.CodeA)
            DbCommandWrapper.AddInParameter("@CodeB", DbType.AnsiString, wSCHeader.CodeB)
            DbCommandWrapper.AddInParameter("@CodeC", DbType.AnsiString, wSCHeader.CodeC)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, wSCHeader.Description)
            DbCommandWrapper.AddInParameter("@EvidencePhoto", DbType.AnsiString, wSCHeader.EvidencePhoto)
            DbCommandWrapper.AddInParameter("@EvidenceInvoice", DbType.AnsiString, wSCHeader.EvidenceInvoice)
            DbCommandWrapper.AddInParameter("@EvidenceDmgPart", DbType.AnsiString, wSCHeader.EvidenceDmgPart)

            DbCommandWrapper.AddInParameter("@EvidenceRepair", DbType.AnsiString, wSCHeader.EvidenceRepair)
            DbCommandWrapper.AddInParameter("@EvidenceWSCLetter", DbType.AnsiString, wSCHeader.EvidenceWSCLetter)
            DbCommandWrapper.AddInParameter("@EvidenceWSCTechnical", DbType.AnsiString, wSCHeader.EvidenceWSCTechnical)
            DbCommandWrapper.AddInParameter("@Causes", DbType.AnsiString, wSCHeader.Causes)
            DbCommandWrapper.AddInParameter("@Results", DbType.AnsiString, wSCHeader.Results)
            DbCommandWrapper.AddInParameter("@Notes", DbType.AnsiString, wSCHeader.Notes)
            DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, wSCHeader.WorkOrderNumber)

            DbCommandWrapper.AddInParameter("@ReqDmgPart", DbType.AnsiString, wSCHeader.ReqDmgPart)
            DbCommandWrapper.AddInParameter("@ReqDmgPartBy", DbType.AnsiString, wSCHeader.ReqDmgPartBy)
            DbCommandWrapper.AddInParameter("@ReqDmgPartTime", DbType.DateTime, wSCHeader.ReqDmgPartTime)
            DbCommandWrapper.AddInParameter("@NotificationNumber", DbType.AnsiString, wSCHeader.NotificationNumber)
            DbCommandWrapper.AddInParameter("@DecideDate", DbType.DateTime, wSCHeader.DecideDate)
            DbCommandWrapper.AddInParameter("@ReleaseDate", DbType.DateTime, wSCHeader.ReleaseDate)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, wSCHeader.Status)
            DbCommandWrapper.AddInParameter("@ClaimStatus", DbType.AnsiString, wSCHeader.ClaimStatus)
            DbCommandWrapper.AddInParameter("@LaborAmount", DbType.Currency, wSCHeader.LaborAmount)
            DbCommandWrapper.AddInParameter("@PartAmount", DbType.Currency, wSCHeader.PartAmount)
            DbCommandWrapper.AddInParameter("@PartReceiveBy", DbType.AnsiString, wSCHeader.PartReceiveBy)
            DbCommandWrapper.AddInParameter("@PartReceiveTime", DbType.DateTime, wSCHeader.PartReceiveTime)
            DbCommandWrapper.AddInParameter("@DownLoadBy", DbType.AnsiString, wSCHeader.DownLoadBy)
            DbCommandWrapper.AddInParameter("@DownLoadTime", DbType.DateTime, wSCHeader.DownLoadTime)
            DbCommandWrapper.AddInParameter("@ResponseTime", DbType.DateTime, wSCHeader.ResponseTime)
            DbCommandWrapper.AddInParameter("@InstallDate", DbType.DateTime, wSCHeader.InstallDate)
            DbCommandWrapper.AddInParameter("@InstallMiliage", DbType.Int32, wSCHeader.InstallMiliage)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, wSCHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, wSCHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@ReasonID", DbType.Int32, Me.GetRefObject(wSCHeader.Reason))
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(wSCHeader.ChassisMaster))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(wSCHeader.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(wSCHeader.DealerBranch))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As WSCHeader

            Dim wSCHeader As WSCHeader = New WSCHeader

            wSCHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ClaimType")) Then wSCHeader.ClaimType = dr("ClaimType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ClaimNumber")) Then wSCHeader.ClaimNumber = dr("ClaimNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RefClaimNumber")) Then wSCHeader.RefClaimNumber = dr("RefClaimNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FailureDate")) Then wSCHeader.FailureDate = CType(dr("FailureDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceDate")) Then wSCHeader.ServiceDate = CType(dr("ServiceDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Miliage")) Then wSCHeader.Miliage = CType(dr("Miliage"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PQR")) Then wSCHeader.PQR = dr("PQR").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PQRStatus")) Then wSCHeader.PQRStatus = dr("PQRStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CodeA")) Then wSCHeader.CodeA = dr("CodeA").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CodeB")) Then wSCHeader.CodeB = dr("CodeB").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CodeC")) Then wSCHeader.CodeC = dr("CodeC").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then wSCHeader.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EvidencePhoto")) Then wSCHeader.EvidencePhoto = dr("EvidencePhoto").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EvidenceInvoice")) Then wSCHeader.EvidenceInvoice = dr("EvidenceInvoice").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EvidenceDmgPart")) Then wSCHeader.EvidenceDmgPart = dr("EvidenceDmgPart").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("EvidenceRepair")) Then wSCHeader.EvidenceRepair = dr("EvidenceRepair").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EvidenceWSCLetter")) Then wSCHeader.EvidenceWSCLetter = dr("EvidenceWSCLetter").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EvidenceWSCTechnical")) Then wSCHeader.EvidenceWSCTechnical = dr("EvidenceWSCTechnical").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Causes")) Then wSCHeader.Causes = dr("Causes").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Results")) Then wSCHeader.Results = dr("Results").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Notes")) Then wSCHeader.Notes = dr("Notes").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("WorkOrderNumber")) Then wSCHeader.WorkOrderNumber = dr("WorkOrderNumber").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("ReqDmgPart")) Then wSCHeader.ReqDmgPart = dr("ReqDmgPart").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReqDmgPartBy")) Then wSCHeader.ReqDmgPartBy = dr("ReqDmgPartBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReqDmgPartTime")) Then wSCHeader.ReqDmgPartTime = CType(dr("ReqDmgPartTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("NotificationNumber")) Then wSCHeader.NotificationNumber = dr("NotificationNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DecideDate")) Then wSCHeader.DecideDate = CType(dr("DecideDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ReleaseDate")) Then wSCHeader.ReleaseDate = CType(dr("ReleaseDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then wSCHeader.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ClaimStatus")) Then wSCHeader.ClaimStatus = dr("ClaimStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LaborAmount")) Then wSCHeader.LaborAmount = CType(dr("LaborAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PartAmount")) Then wSCHeader.PartAmount = CType(dr("PartAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PartReceiveBy")) Then wSCHeader.PartReceiveBy = dr("PartReceiveBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PartReceiveTime")) Then wSCHeader.PartReceiveTime = CType(dr("PartReceiveTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DownLoadBy")) Then wSCHeader.DownLoadBy = dr("DownLoadBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DownLoadTime")) Then wSCHeader.DownLoadTime = CType(dr("DownLoadTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ResponseTime")) Then wSCHeader.ResponseTime = CType(dr("ResponseTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("InstallDate")) Then wSCHeader.InstallDate = CType(dr("InstallDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("InstallMiliage")) Then wSCHeader.InstallMiliage = CType(dr("InstallMiliage"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then wSCHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then wSCHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then wSCHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then wSCHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then wSCHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ReasonID")) Then
                wSCHeader.Reason = New Reason(CType(dr("ReasonID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then
                wSCHeader.ChassisMaster = New ChassisMaster(CType(dr("ChassisMasterID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                wSCHeader.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchID")) Then
                wSCHeader.DealerBranch = New DealerBranch(CType(dr("DealerBranchID"), Integer))
            End If

            Return wSCHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(WSCHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(WSCHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(WSCHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

