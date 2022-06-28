
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_SparePartPOSummary Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 1/14/2009 - 1:02:13 PM
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

    Public Class V_SparePartPOSummaryMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertV_SparePartPOSummary"
        Private m_UpdateStatement As String = "up_UpdateV_SparePartPOSummary"
        Private m_RetrieveStatement As String = "up_RetrieveV_SparePartPOSummary"
        Private m_RetrieveListStatement As String = "up_RetrieveV_SparePartPOSummaryList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteV_SparePartPOSummary"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_SparePartPOSummary As V_SparePartPOSummary = Nothing
            While dr.Read

                v_SparePartPOSummary = Me.CreateObject(dr)

            End While

            Return v_SparePartPOSummary

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_SparePartPOSummaryList As ArrayList = New ArrayList

            While dr.Read
                Dim v_SparePartPOSummary As V_SparePartPOSummary = Me.CreateObject(dr)
                v_SparePartPOSummaryList.Add(v_SparePartPOSummary)
            End While

            Return v_SparePartPOSummaryList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_SparePartPOSummary As V_SparePartPOSummary = CType(obj, V_SparePartPOSummary)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_SparePartPOSummary.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_SparePartPOSummary As V_SparePartPOSummary = CType(obj, V_SparePartPOSummary)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, v_SparePartPOSummary.PONumber)
            DbCommandWrapper.AddInParameter("@OrderType", DbType.AnsiString, v_SparePartPOSummary.OrderType)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, v_SparePartPOSummary.DealerID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_SparePartPOSummary.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, v_SparePartPOSummary.DealerName)
            DbCommandWrapper.AddInParameter("@PODate", DbType.DateTime, v_SparePartPOSummary.PODate)
            DbCommandWrapper.AddInParameter("@ProcessCode", DbType.AnsiString, v_SparePartPOSummary.ProcessCode)
            DbCommandWrapper.AddInParameter("@CancelRequestBy", DbType.AnsiString, v_SparePartPOSummary.CancelRequestBy)
            DbCommandWrapper.AddInParameter("@IndentTransfer", DbType.Int16, v_SparePartPOSummary.IndentTransfer)
            DbCommandWrapper.AddInParameter("@ItemCount", DbType.Int32, v_SparePartPOSummary.ItemCount)
            DbCommandWrapper.AddInParameter("@ItemAmount", DbType.Currency, v_SparePartPOSummary.ItemAmount)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_SparePartPOSummary.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, v_SparePartPOSummary.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@SentPoDate", DbType.DateTime, v_SparePartPOSummary.SentPODate)

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

            Dim v_SparePartPOSummary As V_SparePartPOSummary = CType(obj, V_SparePartPOSummary)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_SparePartPOSummary.ID)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, v_SparePartPOSummary.PONumber)
            DbCommandWrapper.AddInParameter("@OrderType", DbType.AnsiString, v_SparePartPOSummary.OrderType)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, v_SparePartPOSummary.DealerID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_SparePartPOSummary.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, v_SparePartPOSummary.DealerName)
            DbCommandWrapper.AddInParameter("@PODate", DbType.DateTime, v_SparePartPOSummary.PODate)
            DbCommandWrapper.AddInParameter("@ProcessCode", DbType.AnsiString, v_SparePartPOSummary.ProcessCode)
            DbCommandWrapper.AddInParameter("@CancelRequestBy", DbType.AnsiString, v_SparePartPOSummary.CancelRequestBy)
            DbCommandWrapper.AddInParameter("@IndentTransfer", DbType.Int16, v_SparePartPOSummary.IndentTransfer)
            DbCommandWrapper.AddInParameter("@ItemCount", DbType.Int32, v_SparePartPOSummary.ItemCount)
            DbCommandWrapper.AddInParameter("@ItemAmount", DbType.Currency, v_SparePartPOSummary.ItemAmount)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_SparePartPOSummary.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, v_SparePartPOSummary.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@SentPoDate", DbType.DateTime, v_SparePartPOSummary.SentPODate)


            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_SparePartPOSummary

            Dim v_SparePartPOSummary As V_SparePartPOSummary = New V_SparePartPOSummary

            v_SparePartPOSummary.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PONumber")) Then v_SparePartPOSummary.PONumber = dr("PONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OrderType")) Then v_SparePartPOSummary.OrderType = dr("OrderType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then v_SparePartPOSummary.DealerID = CType(dr("DealerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then v_SparePartPOSummary.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then v_SparePartPOSummary.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PODate")) Then v_SparePartPOSummary.PODate = CType(dr("PODate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ProcessCode")) Then v_SparePartPOSummary.ProcessCode = dr("ProcessCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CancelRequestBy")) Then v_SparePartPOSummary.CancelRequestBy = dr("CancelRequestBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IndentTransfer")) Then v_SparePartPOSummary.IndentTransfer = CType(dr("IndentTransfer"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ItemCount")) Then v_SparePartPOSummary.ItemCount = CType(dr("ItemCount"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ItemAmount")) Then v_SparePartPOSummary.ItemAmount = CType(dr("ItemAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then v_SparePartPOSummary.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then v_SparePartPOSummary.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then v_SparePartPOSummary.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then v_SparePartPOSummary.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then v_SparePartPOSummary.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SentPoDate")) Then v_SparePartPOSummary.SentPODate = CType(dr("SentPoDate"), DateTime)
            Return v_SparePartPOSummary

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_SparePartPOSummary) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_SparePartPOSummary), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_SparePartPOSummary).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

