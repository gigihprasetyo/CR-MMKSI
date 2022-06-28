
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

    Public Class V_SpPO_IndentMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertV_SpPO_Indent"
        Private m_UpdateStatement As String = "up_UpdateV_SpPO_Indent"
        Private m_RetrieveStatement As String = "up_RetrieveV_SpPO_Indent"
        Private m_RetrieveListStatement As String = "up_RetrieveV_SpPO_IndentList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteV_SpPO_Indent"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_SpPO_Indent As V_SpPO_Indent = Nothing
            While dr.Read

                v_SpPO_Indent = Me.CreateObject(dr)

            End While

            Return v_SpPO_Indent

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_SpPO_IndentList As ArrayList = New ArrayList

            While dr.Read
                Dim v_SpPO_Indent As V_SpPO_Indent = Me.CreateObject(dr)
                v_SpPO_IndentList.Add(v_SpPO_Indent)
            End While

            Return v_SpPO_IndentList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_SpPO_Indent As V_SpPO_Indent = CType(obj, V_SpPO_Indent)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_SpPO_Indent.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_SpPO_Indent As V_SpPO_Indent = CType(obj, V_SpPO_Indent)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@RequestNo", DbType.AnsiString, v_SpPO_Indent.RequestNo)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, v_SpPO_Indent.PONumber)
            DbCommandWrapper.AddInParameter("@OrderType", DbType.AnsiString, v_SpPO_Indent.OrderType)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, v_SpPO_Indent.DealerID)
            DbCommandWrapper.AddInParameter("@PODate", DbType.DateTime, v_SpPO_Indent.PODate)
            DbCommandWrapper.AddInParameter("@IndentTransfer", DbType.Int16, v_SpPO_Indent.IndentTransfer)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_SpPO_Indent.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, v_SpPO_Indent.DealerName)
            DbCommandWrapper.AddInParameter("@IndentPartHeaderID", DbType.Int32, v_SpPO_Indent.IndentPartHeaderID)


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

            Dim v_SpPO_Indent As V_SpPO_Indent = CType(obj, V_SpPO_Indent)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_SpPO_Indent.ID)
            DbCommandWrapper.AddInParameter("@RequestNo", DbType.AnsiString, v_SpPO_Indent.RequestNo)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, v_SpPO_Indent.PONumber)
            DbCommandWrapper.AddInParameter("@OrderType", DbType.AnsiString, v_SpPO_Indent.OrderType)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, v_SpPO_Indent.DealerID)
            DbCommandWrapper.AddInParameter("@PODate", DbType.DateTime, v_SpPO_Indent.PODate)
            DbCommandWrapper.AddInParameter("@IndentTransfer", DbType.Int16, v_SpPO_Indent.IndentTransfer)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_SpPO_Indent.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, v_SpPO_Indent.DealerName)
            DbCommandWrapper.AddInParameter("@IndentPartHeaderID", DbType.Int32, v_SpPO_Indent.IndentPartHeaderID)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_SpPO_Indent

            Dim v_SpPO_Indent As V_SpPO_Indent = New V_SpPO_Indent

            v_SpPO_Indent.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RequestNo")) Then v_SpPO_Indent.RequestNo = dr("RequestNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PONumber")) Then v_SpPO_Indent.PONumber = dr("PONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OrderType")) Then v_SpPO_Indent.OrderType = dr("OrderType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then v_SpPO_Indent.DealerID = CType(dr("DealerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PODate")) Then v_SpPO_Indent.PODate = CType(dr("PODate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("IndentTransfer")) Then v_SpPO_Indent.IndentTransfer = CType(dr("IndentTransfer"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then v_SpPO_Indent.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then v_SpPO_Indent.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IndentPartHeaderID")) Then v_SpPO_Indent.IndentPartHeaderID = CType(dr("IndentPartHeaderID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TermOfPaymentID")) Then v_SpPO_Indent.TermOfPaymentID = CType(dr("TermOfPaymentID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TOPDescription")) Then v_SpPO_Indent.TOPDescription = dr("TOPDescription").ToString

            Return v_SpPO_Indent

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_SpPO_Indent) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_SpPO_Indent), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_SpPO_Indent).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

