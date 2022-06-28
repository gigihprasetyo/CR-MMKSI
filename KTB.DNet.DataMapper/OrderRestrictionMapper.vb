
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : OrderRestriction Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 10/3/2006 - 3:17:24 PM
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

    Public Class OrderRestrictionMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertOrderRestriction"
        Private m_UpdateStatement As String = "up_UpdateOrderRestriction"
        Private m_RetrieveStatement As String = "up_RetrieveOrderRestriction"
        Private m_RetrieveListStatement As String = "up_RetrieveOrderRestrictionList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteOrderRestriction"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim orderRestriction As OrderRestriction = Nothing
            While dr.Read

                orderRestriction = Me.CreateObject(dr)

            End While

            Return orderRestriction

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim orderRestrictionList As ArrayList = New ArrayList

            While dr.Read
                Dim orderRestriction As OrderRestriction = Me.CreateObject(dr)
                orderRestrictionList.Add(orderRestriction)
            End While

            Return orderRestrictionList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim orderRestriction As OrderRestriction = CType(obj, OrderRestriction)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, orderRestriction.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim orderRestriction As OrderRestriction = CType(obj, OrderRestriction)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@OrderType", DbType.AnsiString, orderRestriction.OrderType)
            DbCommandWrapper.AddInParameter("@RestrictedType", DbType.AnsiString, orderRestriction.RestrictedType)
            DbCommandWrapper.AddInParameter("@DateFrom", DbType.DateTime, orderRestriction.DateFrom)
            DbCommandWrapper.AddInParameter("@DateTo", DbType.DateTime, orderRestriction.DateTo)
            DbCommandWrapper.AddInParameter("@TimeFrom", DbType.AnsiString, orderRestriction.TimeFrom)
            DbCommandWrapper.AddInParameter("@TimeTO", DbType.AnsiString, orderRestriction.TimeTO)
            DbCommandWrapper.AddInParameter("@Days", DbType.Int32, orderRestriction.Days)
            DbCommandWrapper.AddInParameter("@Note", DbType.AnsiString, orderRestriction.Note)
            DbCommandWrapper.AddInParameter("@IsActive", DbType.Int32, orderRestriction.IsActive)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, orderRestriction.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, orderRestriction.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(orderRestriction.Dealer))

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

            Dim orderRestriction As OrderRestriction = CType(obj, OrderRestriction)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, orderRestriction.ID)
            DbCommandWrapper.AddInParameter("@OrderType", DbType.AnsiString, orderRestriction.OrderType)
            DbCommandWrapper.AddInParameter("@RestrictedType", DbType.AnsiString, orderRestriction.RestrictedType)
            DbCommandWrapper.AddInParameter("@DateFrom", DbType.DateTime, orderRestriction.DateFrom)
            DbCommandWrapper.AddInParameter("@DateTo", DbType.DateTime, orderRestriction.DateTo)
            DbCommandWrapper.AddInParameter("@TimeFrom", DbType.AnsiString, orderRestriction.TimeFrom)
            DbCommandWrapper.AddInParameter("@TimeTO", DbType.AnsiString, orderRestriction.TimeTO)
            DbCommandWrapper.AddInParameter("@Days", DbType.Int32, orderRestriction.Days)
            DbCommandWrapper.AddInParameter("@Note", DbType.AnsiString, orderRestriction.Note)
            DbCommandWrapper.AddInParameter("@IsActive", DbType.Int32, orderRestriction.IsActive)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, orderRestriction.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, orderRestriction.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(orderRestriction.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As OrderRestriction

            Dim orderRestriction As OrderRestriction = New OrderRestriction

            orderRestriction.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("OrderType")) Then orderRestriction.OrderType = dr("OrderType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RestrictedType")) Then orderRestriction.RestrictedType = dr("RestrictedType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DateFrom")) Then orderRestriction.DateFrom = CType(dr("DateFrom"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DateTo")) Then orderRestriction.DateTo = CType(dr("DateTo"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TimeFrom")) Then orderRestriction.TimeFrom = dr("TimeFrom").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TimeTO")) Then orderRestriction.TimeTO = dr("TimeTO").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Days")) Then orderRestriction.Days = CType(dr("Days"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Note")) Then orderRestriction.Note = dr("Note").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IsActive")) Then orderRestriction.IsActive = CType(dr("IsActive"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then orderRestriction.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then orderRestriction.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then orderRestriction.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then orderRestriction.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then orderRestriction.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                orderRestriction.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            Return orderRestriction

        End Function

        Private Sub SetTableName()

            If Not (GetType(OrderRestriction) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(OrderRestriction), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(OrderRestriction).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

