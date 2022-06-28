#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VWI_SPKTracking Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 7/20/2020 - 12:26:31 PM
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

    Public Class VWI_SPKTrackingMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_SPKTracking"
        Private m_UpdateStatement As String = "up_UpdateVWI_SPKTracking"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_SPKTracking"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_SPKTrackingList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_SPKTracking"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim VWI_SPKTracking As VWI_SPKTracking = Nothing
            While dr.Read

                VWI_SPKTracking = Me.CreateObject(dr)

            End While

            Return VWI_SPKTracking

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim VWI_SPKTrackingList As ArrayList = New ArrayList

            While dr.Read
                Dim VWI_SPKTracking As VWI_SPKTracking = Me.CreateObject(dr)
                VWI_SPKTrackingList.Add(VWI_SPKTracking)
            End While

            Return VWI_SPKTrackingList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_SPKTracking As VWI_SPKTracking = CType(obj, VWI_SPKTracking)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, VWI_SPKTracking.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_SPKTracking As VWI_SPKTracking = CType(obj, VWI_SPKTracking)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@SPKNumber", DbType.AnsiString, VWI_SPKTracking.SPKNumber)
            DbCommandWrapper.AddInParameter("@DealerSPKNumber", DbType.AnsiString, VWI_SPKTracking.DealerSPKNumber)
            DbCommandWrapper.AddInParameter("@DealerSPKDate", DbType.DateTime, VWI_SPKTracking.DealerSPKDate)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, VWI_SPKTracking.Status)
            DbCommandWrapper.AddInParameter("@StatusDescription", DbType.AnsiString, VWI_SPKTracking.StatusDescription)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, VWI_SPKTracking.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, VWI_SPKTracking.DealerName)
            DbCommandWrapper.AddInParameter("@BranchName", DbType.AnsiString, VWI_SPKTracking.BranchName)
            DbCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, VWI_SPKTracking.SalesmanCode)
            DbCommandWrapper.AddInParameter("@SalesmanName", DbType.AnsiString, VWI_SPKTracking.SalesmanName)
            DbCommandWrapper.AddInParameter("@SPKCustomer", DbType.AnsiString, VWI_SPKTracking.SPKCustomer)
            DbCommandWrapper.AddInParameter("@RejectedReason", DbType.AnsiString, VWI_SPKTracking.RejectedReason)
            DbCommandWrapper.AddInParameter("@DealerCity", DbType.AnsiString, VWI_SPKTracking.DealerCity)
            DbCommandWrapper.AddInParameter("@CustomerType", DbType.AnsiString, VWI_SPKTracking.CustomerType)
            DbCommandWrapper.AddInParameter("@PilotingSPKMatching", DbType.AnsiString, VWI_SPKTracking.PilotingSPKMatching)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, VWI_SPKTracking.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedTime", DbType.DateTime, VWI_SPKTracking.CreatedTime)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, VWI_SPKTracking.CreatedBy)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, VWI_SPKTracking.LastUpdateTime)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, VWI_SPKTracking.LastUpdateBy)

            DbCommandWrapper.AddInParameter("@SPKDetail", DbType.AnsiString, VWI_SPKTracking.SPKDetail)

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

            Dim VWI_SPKTracking As VWI_SPKTracking = CType(obj, VWI_SPKTracking)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@SPKNumber", DbType.AnsiString, VWI_SPKTracking.SPKNumber)
            DbCommandWrapper.AddInParameter("@DealerSPKNumber", DbType.AnsiString, VWI_SPKTracking.DealerSPKNumber)
            DbCommandWrapper.AddInParameter("@DealerSPKDate", DbType.DateTime, VWI_SPKTracking.DealerSPKDate)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, VWI_SPKTracking.Status)
            DbCommandWrapper.AddInParameter("@StatusDescription", DbType.AnsiString, VWI_SPKTracking.StatusDescription)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, VWI_SPKTracking.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, VWI_SPKTracking.DealerName)
            DbCommandWrapper.AddInParameter("@BranchName", DbType.AnsiString, VWI_SPKTracking.BranchName)
            DbCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, VWI_SPKTracking.SalesmanCode)
            DbCommandWrapper.AddInParameter("@SalesmanName", DbType.AnsiString, VWI_SPKTracking.SalesmanName)
            DbCommandWrapper.AddInParameter("@SPKCustomer", DbType.AnsiString, VWI_SPKTracking.SPKCustomer)
            DbCommandWrapper.AddInParameter("@RejectedReason", DbType.AnsiString, VWI_SPKTracking.RejectedReason)
            DbCommandWrapper.AddInParameter("@DealerCity", DbType.AnsiString, VWI_SPKTracking.DealerCity)
            DbCommandWrapper.AddInParameter("@CustomerType", DbType.AnsiString, VWI_SPKTracking.CustomerType)
            DbCommandWrapper.AddInParameter("@PilotingSPKMatching", DbType.AnsiString, VWI_SPKTracking.PilotingSPKMatching)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, VWI_SPKTracking.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedTime", DbType.DateTime, VWI_SPKTracking.CreatedTime)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, VWI_SPKTracking.CreatedBy)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, VWI_SPKTracking.LastUpdateTime)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, VWI_SPKTracking.LastUpdateBy)
            DbCommandWrapper.AddInParameter("@SPKDetail", DbType.AnsiString, VWI_SPKTracking.SPKDetail)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_SPKTracking

            Dim VWI_SPKTracking As VWI_SPKTracking = New VWI_SPKTracking

            VWI_SPKTracking.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SPKNumber")) Then VWI_SPKTracking.SPKNumber = dr("SPKNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerSPKNumber")) Then VWI_SPKTracking.DealerSPKNumber = dr("DealerSPKNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerSPKDate")) Then VWI_SPKTracking.DealerSPKDate = CType(dr("DealerSPKDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then VWI_SPKTracking.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StatusDescription")) Then VWI_SPKTracking.StatusDescription = dr("StatusDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then VWI_SPKTracking.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then VWI_SPKTracking.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BranchName")) Then VWI_SPKTracking.BranchName = dr("BranchName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanCode")) Then VWI_SPKTracking.SalesmanCode = dr("SalesmanCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanName")) Then VWI_SPKTracking.SalesmanName = dr("SalesmanName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SPKCustomer")) Then VWI_SPKTracking.SPKCustomer = dr("SPKCustomer").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RejectedReason")) Then VWI_SPKTracking.RejectedReason = dr("RejectedReason").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCity")) Then VWI_SPKTracking.DealerCity = dr("DealerCity").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerType")) Then VWI_SPKTracking.CustomerType = dr("CustomerType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PilotingSPKMatching")) Then VWI_SPKTracking.PilotingSPKMatching = dr("PilotingSPKMatching").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then VWI_SPKTracking.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then VWI_SPKTracking.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then VWI_SPKTracking.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then VWI_SPKTracking.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then VWI_SPKTracking.LastUpdateBy = dr("LastUpdateBy").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("SPKDetail")) Then VWI_SPKTracking.SPKDetail = dr("SPKDetail").ToString
            Return VWI_SPKTracking

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_SPKTracking) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_SPKTracking), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_SPKTracking).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
