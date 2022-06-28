
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DMSWorkOrderWSCStatus Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 20/07/2018 - 5:52:33
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

    Public Class VWI_DMSWorkOrderWSCStatusMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDMSWorkOrderWSCStatus"
        Private m_UpdateStatement As String = "up_UpdateDMSWorkOrderWSCStatus"
        Private m_RetrieveStatement As String = "up_RetrieveDMSWorkOrderWSCStatus"
        Private m_RetrieveListStatement As String = "up_RetrieveDMSWorkOrderWSCStatusList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDMSWorkOrderWSCStatus"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim dMSWorkOrderWSCStatus As VWI_DMSWorkOrderWSCStatus = Nothing
            While dr.Read

                dMSWorkOrderWSCStatus = Me.CreateObject(dr)

            End While

            Return dMSWorkOrderWSCStatus

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim dMSWorkOrderWSCStatusList As ArrayList = New ArrayList

            While dr.Read
                Dim dMSWorkOrderWSCStatus As VWI_DMSWorkOrderWSCStatus = Me.CreateObject(dr)
                dMSWorkOrderWSCStatusList.Add(dMSWorkOrderWSCStatus)
            End While

            Return dMSWorkOrderWSCStatusList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dMSWorkOrderWSCStatus As VWI_DMSWorkOrderWSCStatus = CType(obj, VWI_DMSWorkOrderWSCStatus)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dMSWorkOrderWSCStatus.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dMSWorkOrderWSCStatus As VWI_DMSWorkOrderWSCStatus = CType(obj, VWI_DMSWorkOrderWSCStatus)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, dMSWorkOrderWSCStatus.DealerCode)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, dMSWorkOrderWSCStatus.ChassisNumber)
            DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, dMSWorkOrderWSCStatus.WorkOrderNumber)
            DbCommandWrapper.AddInParameter("@PQRNo", DbType.AnsiString, dMSWorkOrderWSCStatus.PQRNo)
            DbCommandWrapper.AddInParameter("@PQRType", DbType.Int32, dMSWorkOrderWSCStatus.PQRType)
            DbCommandWrapper.AddInParameter("@PQRTypeText", DbType.AnsiString, dMSWorkOrderWSCStatus.PQRTypeText)
            DbCommandWrapper.AddInParameter("@PQRDate", DbType.DateTime, dMSWorkOrderWSCStatus.PQRDate)
            DbCommandWrapper.AddInParameter("@PQRStatus", DbType.Int16, dMSWorkOrderWSCStatus.PQRStatus)
            DbCommandWrapper.AddInParameter("@PQRStatusText", DbType.AnsiString, dMSWorkOrderWSCStatus.PQRStatusText)
            DbCommandWrapper.AddInParameter("@ClaimType", DbType.AnsiString, dMSWorkOrderWSCStatus.ClaimType)
            DbCommandWrapper.AddInParameter("@ClaimNumber", DbType.AnsiString, dMSWorkOrderWSCStatus.ClaimNumber)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, dMSWorkOrderWSCStatus.Description)
            DbCommandWrapper.AddInParameter("@ClaimStatus", DbType.AnsiString, dMSWorkOrderWSCStatus.ClaimStatus)
            DbCommandWrapper.AddInParameter("@WSCStatus", DbType.AnsiString, dMSWorkOrderWSCStatus.WSCStatus)
            DbCommandWrapper.AddInParameter("@WSCStatusText", DbType.AnsiString, dMSWorkOrderWSCStatus.WSCStatusText)
            DbCommandWrapper.AddInParameter("@LaborAmount", DbType.Decimal, dMSWorkOrderWSCStatus.LaborAmount)
            DbCommandWrapper.AddInParameter("@PartAmount", DbType.Decimal, dMSWorkOrderWSCStatus.PartAmount)
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

            Dim dMSWorkOrderWSCStatus As VWI_DMSWorkOrderWSCStatus = CType(obj, VWI_DMSWorkOrderWSCStatus)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dMSWorkOrderWSCStatus.ID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, dMSWorkOrderWSCStatus.DealerCode)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, dMSWorkOrderWSCStatus.ChassisNumber)
            DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, dMSWorkOrderWSCStatus.WorkOrderNumber)
            DbCommandWrapper.AddInParameter("@PQRNo", DbType.AnsiString, dMSWorkOrderWSCStatus.PQRNo)
            DbCommandWrapper.AddInParameter("@PQRType", DbType.Int32, dMSWorkOrderWSCStatus.PQRType)
            DbCommandWrapper.AddInParameter("@PQRTypeText", DbType.AnsiString, dMSWorkOrderWSCStatus.PQRTypeText)
            DbCommandWrapper.AddInParameter("@PQRDate", DbType.DateTime, dMSWorkOrderWSCStatus.PQRDate)
            DbCommandWrapper.AddInParameter("@PQRStatus", DbType.Int16, dMSWorkOrderWSCStatus.PQRStatus)
            DbCommandWrapper.AddInParameter("@PQRStatusText", DbType.AnsiString, dMSWorkOrderWSCStatus.PQRStatusText)
            DbCommandWrapper.AddInParameter("@ClaimType", DbType.AnsiString, dMSWorkOrderWSCStatus.ClaimType)
            DbCommandWrapper.AddInParameter("@ClaimNumber", DbType.AnsiString, dMSWorkOrderWSCStatus.ClaimNumber)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, dMSWorkOrderWSCStatus.Description)
            DbCommandWrapper.AddInParameter("@ClaimStatus", DbType.AnsiString, dMSWorkOrderWSCStatus.ClaimStatus)
            DbCommandWrapper.AddInParameter("@WSCStatus", DbType.AnsiString, dMSWorkOrderWSCStatus.WSCStatus)
            DbCommandWrapper.AddInParameter("@WSCStatusText", DbType.AnsiString, dMSWorkOrderWSCStatus.WSCStatusText)
            DbCommandWrapper.AddInParameter("@LaborAmount", DbType.Decimal, dMSWorkOrderWSCStatus.LaborAmount)
            DbCommandWrapper.AddInParameter("@PartAmount", DbType.Decimal, dMSWorkOrderWSCStatus.PartAmount)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_DMSWorkOrderWSCStatus

            Dim dMSWorkOrderWSCStatus As VWI_DMSWorkOrderWSCStatus = New VWI_DMSWorkOrderWSCStatus

            'dMSWorkOrderWSCStatus.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then dMSWorkOrderWSCStatus.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then dMSWorkOrderWSCStatus.ChassisNumber = dr("ChassisNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("WorkOrderNumber")) Then dMSWorkOrderWSCStatus.WorkOrderNumber = dr("WorkOrderNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PQRNo")) Then dMSWorkOrderWSCStatus.PQRNo = dr("PQRNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PQRType")) Then dMSWorkOrderWSCStatus.PQRType = CType(dr("PQRType"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PQRTypeText")) Then dMSWorkOrderWSCStatus.PQRTypeText = dr("PQRTypeText").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PQRDate")) Then dMSWorkOrderWSCStatus.PQRDate = CType(dr("PQRDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PQRStatus")) Then dMSWorkOrderWSCStatus.PQRStatus = CType(dr("PQRStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PQRStatusText")) Then dMSWorkOrderWSCStatus.PQRStatusText = dr("PQRStatusText").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ClaimType")) Then dMSWorkOrderWSCStatus.ClaimType = dr("ClaimType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ClaimNumber")) Then dMSWorkOrderWSCStatus.ClaimNumber = dr("ClaimNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then dMSWorkOrderWSCStatus.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ClaimStatus")) Then dMSWorkOrderWSCStatus.ClaimStatus = dr("ClaimStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("WSCStatus")) Then dMSWorkOrderWSCStatus.WSCStatus = dr("WSCStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("WSCStatusText")) Then dMSWorkOrderWSCStatus.WSCStatusText = dr("WSCStatusText").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LaborAmount")) Then dMSWorkOrderWSCStatus.LaborAmount = CType(dr("LaborAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PartAmount")) Then dMSWorkOrderWSCStatus.PartAmount = CType(dr("PartAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then dMSWorkOrderWSCStatus.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return dMSWorkOrderWSCStatus

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_DMSWorkOrderWSCStatus) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_DMSWorkOrderWSCStatus), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_DMSWorkOrderWSCStatus).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

