
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_SpPO_Indent Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2008 
'// ---------------------
'// $History      : $
'// Generated on 12/22/2008 - 11:04:41 AM
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

    Public Class v_EquipSPPOIndentMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_Insertv_EquipSPPOIndent"
        Private m_UpdateStatement As String = "up_Updatev_EquipSPPOIndent"
        Private m_RetrieveStatement As String = "up_Retrievev_EquipSPPOIndent"
        Private m_RetrieveListStatement As String = "up_Retrievev_EquipSPPOIndentList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_Deletev_EquipSPPOIndent"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim objv_EquipSPPOIndent As v_EquipSPPOIndent = Nothing
            While dr.Read

                objv_EquipSPPOIndent = Me.CreateObject(dr)

            End While

            Return objv_EquipSPPOIndent

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_EquipSPPOIndentList As ArrayList = New ArrayList

            While dr.Read
                Dim v_EquipSPPOIndent As v_EquipSPPOIndent = Me.CreateObject(dr)
                v_EquipSPPOIndentList.Add(v_EquipSPPOIndent)
            End While

            Return v_EquipSPPOIndentList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_EquipSPPOIndent As v_EquipSPPOIndent = CType(obj, v_EquipSPPOIndent)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_EquipSPPOIndent.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim objv_EquipSPPOIndent As v_EquipSPPOIndent = CType(obj, v_EquipSPPOIndent)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)

            DBCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DBCommandWrapper.AddInParameter("@RequestNo", DbType.AnsiString, objv_EquipSPPOIndent.RequestNo)
            DBCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, objv_EquipSPPOIndent.PONumber)
            DBCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, objv_EquipSPPOIndent.SONumber)
            DBCommandWrapper.AddInParameter("@OrderType", DbType.AnsiString, objv_EquipSPPOIndent.OrderType)
            DBCommandWrapper.AddInParameter("@DealerID", DbType.Int32, objv_EquipSPPOIndent.DealerID)
            DBCommandWrapper.AddInParameter("@PODate", DbType.DateTime, objv_EquipSPPOIndent.PODate)
            DBCommandWrapper.AddInParameter("@IndentTransfer", DbType.Int16, objv_EquipSPPOIndent.IndentTransfer)
            DBCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, objv_EquipSPPOIndent.DealerCode)
            DBCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, objv_EquipSPPOIndent.DealerName)
            DBCommandWrapper.AddInParameter("@IndentPartHeaderID", DbType.Int32, objv_EquipSPPOIndent.IndentPartHeaderID)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, objv_EquipSPPOIndent.RowStatus)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, objv_EquipSPPOIndent.LastUpdateBy)

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

            Dim objv_EquipSPPOIndent As v_EquipSPPOIndent = CType(obj, v_EquipSPPOIndent)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, objv_EquipSPPOIndent.ID)
            DBCommandWrapper.AddInParameter("@RequestNo", DbType.AnsiString, objv_EquipSPPOIndent.RequestNo)
            DBCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, objv_EquipSPPOIndent.PONumber)
            DBCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, objv_EquipSPPOIndent.SONumber)
            DBCommandWrapper.AddInParameter("@OrderType", DbType.AnsiString, objv_EquipSPPOIndent.OrderType)
            DBCommandWrapper.AddInParameter("@DealerID", DbType.Int32, objv_EquipSPPOIndent.DealerID)
            DBCommandWrapper.AddInParameter("@PODate", DbType.DateTime, objv_EquipSPPOIndent.PODate)
            DBCommandWrapper.AddInParameter("@IndentTransfer", DbType.Int16, objv_EquipSPPOIndent.IndentTransfer)
            DBCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, objv_EquipSPPOIndent.DealerCode)
            DBCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, objv_EquipSPPOIndent.DealerName)
            DBCommandWrapper.AddInParameter("@IndentPartHeaderID", DbType.Int32, objv_EquipSPPOIndent.IndentPartHeaderID)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, objv_EquipSPPOIndent.RowStatus)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As v_EquipSPPOIndent

            Dim objv_EquipSPPOIndent As v_EquipSPPOIndent = New v_EquipSPPOIndent

            objv_EquipSPPOIndent.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RequestNo")) Then objv_EquipSPPOIndent.RequestNo = dr("RequestNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PONumber")) Then objv_EquipSPPOIndent.PONumber = dr("PONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SONumber")) Then objv_EquipSPPOIndent.SONumber = dr("SONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OrderType")) Then objv_EquipSPPOIndent.OrderType = dr("OrderType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then objv_EquipSPPOIndent.DealerID = CType(dr("DealerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PODate")) Then objv_EquipSPPOIndent.PODate = CType(dr("PODate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("IndentTransfer")) Then objv_EquipSPPOIndent.IndentTransfer = CType(dr("IndentTransfer"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then objv_EquipSPPOIndent.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then objv_EquipSPPOIndent.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IndentPartHeaderID")) Then objv_EquipSPPOIndent.IndentPartHeaderID = CType(dr("IndentPartHeaderID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then objv_EquipSPPOIndent.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then objv_EquipSPPOIndent.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then objv_EquipSPPOIndent.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then objv_EquipSPPOIndent.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then objv_EquipSPPOIndent.LastUpdateBy = dr("LastUpdateBy").ToString

            Return objv_EquipSPPOIndent

        End Function

        Private Sub SetTableName()

            If Not (GetType(v_EquipSPPOIndent) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(v_EquipSPPOIndent), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(v_EquipSPPOIndent).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace