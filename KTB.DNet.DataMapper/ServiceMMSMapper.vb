#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ServiceMMS Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 10/13/2005 - 11:16:18 AM
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

    Public Class ServiceMMSMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertServiceMMS"
        Private m_UpdateStatement As String = "up_UpdateServiceMMS"
        Private m_RetrieveStatement As String = "up_RetrieveServiceMMS"
        Private m_RetrieveListStatement As String = "up_RetrieveServiceMMSList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteServiceMMS"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim ServiceMMS As ServiceMMS = Nothing
            While dr.Read

                ServiceMMS = Me.CreateObject(dr)

            End While

            Return ServiceMMS

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim ServiceMMSList As ArrayList = New ArrayList

            While dr.Read
                Dim ServiceMMS As ServiceMMS = Me.CreateObject(dr)
                ServiceMMSList.Add(ServiceMMS)
            End While

            Return ServiceMMSList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ServiceMMS As ServiceMMS = CType(obj, ServiceMMS)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ServiceMMS.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ServiceMMS As ServiceMMS = CType(obj, ServiceMMS)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@WONumber", DbType.AnsiString, ServiceMMS.WONumber)
            DbCommandWrapper.AddInParameter("@ServiceDate", DbType.DateTime, ServiceMMS.ServiceDate)
            DbCommandWrapper.AddInParameter("@PlateNo", DbType.AnsiString, ServiceMMS.PlateNo)
            DbCommandWrapper.AddInParameter("@NextEstimatedServiceDate", DbType.DateTime, ServiceMMS.NextEstimatedServiceDate)
            DbCommandWrapper.AddInParameter("@Notes", DbType.AnsiString, ServiceMMS.Notes)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, ServiceMMS.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ServiceMMS.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, ServiceMMS.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(ServiceMMS.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int16, Me.GetRefObject(ServiceMMS.DealerBranch))
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(ServiceMMS.ChassisMaster))

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

            Dim ServiceMMS As ServiceMMS = CType(obj, ServiceMMS)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ServiceMMS.ID)
            DbCommandWrapper.AddInParameter("@WONumber", DbType.AnsiString, ServiceMMS.WONumber)
            DbCommandWrapper.AddInParameter("@ServiceDate", DbType.DateTime, ServiceMMS.ServiceDate)
            DbCommandWrapper.AddInParameter("@PlateNo", DbType.AnsiString, ServiceMMS.PlateNo)
            DbCommandWrapper.AddInParameter("@NextEstimatedServiceDate", DbType.DateTime, ServiceMMS.NextEstimatedServiceDate)
            DbCommandWrapper.AddInParameter("@Notes", DbType.AnsiString, ServiceMMS.Notes)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, ServiceMMS.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ServiceMMS.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, ServiceMMS.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(ServiceMMS.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int16, Me.GetRefObject(ServiceMMS.DealerBranch))
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int16, Me.GetRefObject(ServiceMMS.ChassisMaster))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ServiceMMS

            Dim ServiceMMS As ServiceMMS = New ServiceMMS

            ServiceMMS.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then ServiceMMS.Dealer = New Dealer(CType(dr("DealerID"), Short))
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchID")) Then ServiceMMS.DealerBranch = New DealerBranch(CType(dr("DealerBranchID"), Short))
            If Not dr.IsDBNull(dr.GetOrdinal("WONumber")) Then ServiceMMS.WONumber = dr("WONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceDate")) Then ServiceMMS.ServiceDate = CType(dr("ServiceDate").ToString, DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then ServiceMMS.ChassisMaster = New ChassisMaster(CType(dr("ChassisMasterID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("PlateNo")) Then ServiceMMS.PlateNo = dr("PlateNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NextEstimatedServiceDate")) Then ServiceMMS.NextEstimatedServiceDate = CType(dr("NextEstimatedServiceDate").ToString, DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Notes")) Then ServiceMMS.Notes = dr("Notes").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then ServiceMMS.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then ServiceMMS.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then ServiceMMS.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then ServiceMMS.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then ServiceMMS.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then ServiceMMS.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return ServiceMMS

        End Function

        Private Sub SetTableName()

            If Not (GetType(ServiceMMS) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ServiceMMS), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ServiceMMS).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace



