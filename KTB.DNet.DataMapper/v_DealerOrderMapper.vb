
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : v_DealerOrder Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 5/31/2012 - 2:30:06 PM
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

    Public Class v_DealerOrderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_Insertv_DealerOrder"
        Private m_UpdateStatement As String = "up_Updatev_DealerOrder"
        Private m_RetrieveStatement As String = "up_Retrievev_DealerOrder"
        Private m_RetrieveListStatement As String = "up_Retrievev_DealerOrderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_Deletev_DealerOrder"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_DealerOrder As v_DealerOrder = Nothing
            While dr.Read

                v_DealerOrder = Me.CreateObject(dr)

            End While

            Return v_DealerOrder

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_DealerOrderList As ArrayList = New ArrayList

            While dr.Read
                Dim v_DealerOrder As v_DealerOrder = Me.CreateObject(dr)
                v_DealerOrderList.Add(v_DealerOrder)
            End While

            Return v_DealerOrderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_DealerOrder As v_DealerOrder = CType(obj, v_DealerOrder)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_DealerOrder.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_DealerOrder As v_DealerOrder = CType(obj, v_DealerOrder)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_DealerOrder.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, v_DealerOrder.DealerName)
            DbCommandWrapper.AddInParameter("@ReqAllocationDateTime", DbType.DateTime, v_DealerOrder.ReqAllocationDateTime)
            DbCommandWrapper.AddInParameter("@MaterialNumber", DbType.AnsiString, v_DealerOrder.MaterialNumber)
            DbCommandWrapper.AddInParameter("@PODetailID", DbType.Int32, v_DealerOrder.PODetailID)
            DbCommandWrapper.AddInParameter("@AllocationDateTime", DbType.DateTime, v_DealerOrder.AllocationDateTime)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, v_DealerOrder.PONumber)
            DbCommandWrapper.AddInParameter("@DealerPONumber", DbType.AnsiString, v_DealerOrder.DealerPONumber)
            DbCommandWrapper.AddInParameter("@StokATP", DbType.Int32, v_DealerOrder.StokATP)
            DbCommandWrapper.AddInParameter("@StokSebelum", DbType.Int32, v_DealerOrder.StokSebelum)
            DbCommandWrapper.AddInParameter("@ReqQty", DbType.Int32, v_DealerOrder.ReqQty)
            DbCommandWrapper.AddInParameter("@AllocQty", DbType.Int32, v_DealerOrder.AllocQty)
            DbCommandWrapper.AddInParameter("@UnAllocated", DbType.Int32, v_DealerOrder.UnAllocated)
            DbCommandWrapper.AddInParameter("@StokSesudah", DbType.Int32, v_DealerOrder.StokSesudah)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_DealerOrder.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, v_DealerOrder.LastUpdateBy)
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

            Dim v_DealerOrder As v_DealerOrder = CType(obj, v_DealerOrder)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_DealerOrder.ID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_DealerOrder.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, v_DealerOrder.DealerName)
            DbCommandWrapper.AddInParameter("@ReqAllocationDateTime", DbType.DateTime, v_DealerOrder.ReqAllocationDateTime)
            DbCommandWrapper.AddInParameter("@MaterialNumber", DbType.AnsiString, v_DealerOrder.MaterialNumber)
            DbCommandWrapper.AddInParameter("@PODetailID", DbType.Int32, v_DealerOrder.PODetailID)
            DbCommandWrapper.AddInParameter("@AllocationDateTime", DbType.DateTime, v_DealerOrder.AllocationDateTime)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, v_DealerOrder.PONumber)
            DbCommandWrapper.AddInParameter("@DealerPONumber", DbType.AnsiString, v_DealerOrder.DealerPONumber)
            DbCommandWrapper.AddInParameter("@StokATP", DbType.Int32, v_DealerOrder.StokATP)
            DbCommandWrapper.AddInParameter("@StokSebelum", DbType.Int32, v_DealerOrder.StokSebelum)
            DbCommandWrapper.AddInParameter("@ReqQty", DbType.Int32, v_DealerOrder.ReqQty)
            DbCommandWrapper.AddInParameter("@AllocQty", DbType.Int32, v_DealerOrder.AllocQty)
            DbCommandWrapper.AddInParameter("@UnAllocated", DbType.Int32, v_DealerOrder.UnAllocated)
            DbCommandWrapper.AddInParameter("@StokSesudah", DbType.Int32, v_DealerOrder.StokSesudah)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_DealerOrder.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, v_DealerOrder.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As v_DealerOrder

            Dim v_DealerOrder As v_DealerOrder = New v_DealerOrder

            v_DealerOrder.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then v_DealerOrder.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then v_DealerOrder.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReqAllocationDateTime")) Then v_DealerOrder.ReqAllocationDateTime = CType(dr("ReqAllocationDateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("MaterialNumber")) Then v_DealerOrder.MaterialNumber = dr("MaterialNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PODetailID")) Then v_DealerOrder.PODetailID = CType(dr("PODetailID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("AllocationDateTime")) Then v_DealerOrder.AllocationDateTime = CType(dr("AllocationDateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PONumber")) Then v_DealerOrder.PONumber = dr("PONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerPONumber")) Then v_DealerOrder.DealerPONumber = dr("DealerPONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StokATP")) Then v_DealerOrder.StokATP = CType(dr("StokATP"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("StokSebelum")) Then v_DealerOrder.StokSebelum = CType(dr("StokSebelum"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ReqQty")) Then v_DealerOrder.ReqQty = CType(dr("ReqQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("AllocQty")) Then v_DealerOrder.AllocQty = CType(dr("AllocQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("UnAllocated")) Then v_DealerOrder.UnAllocated = CType(dr("UnAllocated"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("StokSesudah")) Then v_DealerOrder.StokSesudah = CType(dr("StokSesudah"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then v_DealerOrder.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then v_DealerOrder.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then v_DealerOrder.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then v_DealerOrder.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then v_DealerOrder.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return v_DealerOrder

        End Function

        Private Sub SetTableName()

            If Not (GetType(v_DealerOrder) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(v_DealerOrder), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(v_DealerOrder).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

