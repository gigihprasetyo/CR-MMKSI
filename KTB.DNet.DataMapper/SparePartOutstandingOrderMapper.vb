
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartOutstandingOrder Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 10/26/2015 - 4:30:26 PM
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

    Public Class SparePartOutstandingOrderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSparePartOutstandingOrder"
        Private m_UpdateStatement As String = "up_UpdateSparePartOutstandingOrder"
        Private m_RetrieveStatement As String = "up_RetrieveSparePartOutstandingOrder"
        Private m_RetrieveListStatement As String = "up_RetrieveSparePartOutstandingOrderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSparePartOutstandingOrder"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sparePartOutstandingOrder As SparePartOutstandingOrder = Nothing
            While dr.Read

                sparePartOutstandingOrder = Me.CreateObject(dr)

            End While

            Return sparePartOutstandingOrder

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sparePartOutstandingOrderList As ArrayList = New ArrayList

            While dr.Read
                Dim sparePartOutstandingOrder As SparePartOutstandingOrder = Me.CreateObject(dr)
                sparePartOutstandingOrderList.Add(sparePartOutstandingOrder)
            End While

            Return sparePartOutstandingOrderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartOutstandingOrder As SparePartOutstandingOrder = CType(obj, SparePartOutstandingOrder)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartOutstandingOrder.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartOutstandingOrder As SparePartOutstandingOrder = CType(obj, SparePartOutstandingOrder)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@PODate", DbType.DateTime, sparePartOutstandingOrder.PODate)
            DbCommandWrapper.AddInParameter("@ValidTo", DbType.DateTime, sparePartOutstandingOrder.ValidTo)
            DbCommandWrapper.AddInParameter("@OrderType", DbType.AnsiString, sparePartOutstandingOrder.OrderType)
            DbCommandWrapper.AddInParameter("@DocumentType", DbType.AnsiString, sparePartOutstandingOrder.DocumentType)
            DbCommandWrapper.AddInParameter("@LastEmailDate", DbType.DateTime, sparePartOutstandingOrder.LastEmailDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartOutstandingOrder.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sparePartOutstandingOrder.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SparePartPOID", DbType.Int32, Me.GetRefObject(sparePartOutstandingOrder.SparePartPO))

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

            Dim sparePartOutstandingOrder As SparePartOutstandingOrder = CType(obj, SparePartOutstandingOrder)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartOutstandingOrder.ID)
            DbCommandWrapper.AddInParameter("@PODate", DbType.DateTime, sparePartOutstandingOrder.PODate)
            DbCommandWrapper.AddInParameter("@ValidTo", DbType.DateTime, sparePartOutstandingOrder.ValidTo)
            DbCommandWrapper.AddInParameter("@OrderType", DbType.AnsiString, sparePartOutstandingOrder.OrderType)
            DbCommandWrapper.AddInParameter("@DocumentType", DbType.AnsiString, sparePartOutstandingOrder.DocumentType)
            DbCommandWrapper.AddInParameter("@LastEmailDate", DbType.DateTime, sparePartOutstandingOrder.LastEmailDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartOutstandingOrder.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sparePartOutstandingOrder.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SparePartPOID", DbType.Int32, Me.GetRefObject(sparePartOutstandingOrder.SparePartPO))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SparePartOutstandingOrder

            Dim sparePartOutstandingOrder As SparePartOutstandingOrder = New SparePartOutstandingOrder

            sparePartOutstandingOrder.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PODate")) Then sparePartOutstandingOrder.PODate = CType(dr("PODate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidTo")) Then sparePartOutstandingOrder.ValidTo = CType(dr("ValidTo"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("OrderType")) Then sparePartOutstandingOrder.OrderType = dr("OrderType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DocumentType")) Then sparePartOutstandingOrder.DocumentType = dr("DocumentType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastEmailDate")) Then sparePartOutstandingOrder.LastEmailDate = CType(dr("LastEmailDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sparePartOutstandingOrder.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sparePartOutstandingOrder.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sparePartOutstandingOrder.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sparePartOutstandingOrder.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sparePartOutstandingOrder.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartPOID")) Then
                sparePartOutstandingOrder.SparePartPO = New SparePartPO(CType(dr("SparePartPOID"), Integer))
            End If

            Return sparePartOutstandingOrder

        End Function

        Private Sub SetTableName()

            If Not (GetType(SparePartOutstandingOrder) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SparePartOutstandingOrder), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SparePartOutstandingOrder).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

