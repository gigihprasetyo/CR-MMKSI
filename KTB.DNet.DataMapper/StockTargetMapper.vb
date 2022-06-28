
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : StockTarget Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 11/12/2015 - 8:28:06 AM
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

    Public Class StockTargetMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertStockTarget"
        Private m_UpdateStatement As String = "up_UpdateStockTarget"
        Private m_RetrieveStatement As String = "up_RetrieveStockTarget"
        Private m_RetrieveListStatement As String = "up_RetrieveStockTargetList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteStockTarget"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim stockTarget As StockTarget = Nothing
            While dr.Read

                stockTarget = Me.CreateObject(dr)

            End While

            Return stockTarget

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim stockTargetList As ArrayList = New ArrayList

            While dr.Read
                Dim stockTarget As StockTarget = Me.CreateObject(dr)
                stockTargetList.Add(stockTarget)
            End While

            Return stockTargetList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim stockTarget As StockTarget = CType(obj, StockTarget)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, stockTarget.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim stockTarget As StockTarget = CType(obj, StockTarget)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, stockTarget.ValidFrom)
            DbCommandWrapper.AddInParameter("@Target", DbType.Int32, stockTarget.Target)
            DbCommandWrapper.AddInParameter("@TargetRatio", DbType.Decimal, stockTarget.TargetRatio)
            DbCommandWrapper.AddInParameter("@IsDealerBlock", DbType.Byte, stockTarget.IsDealerBlock)
            DbCommandWrapper.AddInParameter("@IsKTBBlock", DbType.Byte, stockTarget.IsKTBBlock)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, stockTarget.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, stockTarget.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(stockTarget.Dealer))
            DbCommandWrapper.AddInParameter("@ModelID", DbType.Int16, Me.GetRefObject(stockTarget.VechileModel))

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

            Dim stockTarget As StockTarget = CType(obj, StockTarget)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, stockTarget.ID)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, stockTarget.ValidFrom)
            DbCommandWrapper.AddInParameter("@Target", DbType.Int32, stockTarget.Target)
            DbCommandWrapper.AddInParameter("@TargetRatio", DbType.Decimal, stockTarget.TargetRatio)
            DbCommandWrapper.AddInParameter("@IsDealerBlock", DbType.Byte, stockTarget.IsDealerBlock)
            DbCommandWrapper.AddInParameter("@IsKTBBlock", DbType.Byte, stockTarget.IsKTBBlock)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, stockTarget.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, stockTarget.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(stockTarget.Dealer))
            DbCommandWrapper.AddInParameter("@ModelID", DbType.Int16, Me.GetRefObject(stockTarget.VechileModel))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As StockTarget

            Dim stockTarget As StockTarget = New StockTarget

            stockTarget.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidFrom")) Then stockTarget.ValidFrom = CType(dr("ValidFrom"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Target")) Then stockTarget.Target = CType(dr("Target"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TargetRatio")) Then stockTarget.TargetRatio = CType(dr("TargetRatio"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("IsDealerBlock")) Then stockTarget.IsDealerBlock = CType(dr("IsDealerBlock"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("IsKTBBlock")) Then stockTarget.IsKTBBlock = CType(dr("IsKTBBlock"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then stockTarget.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then stockTarget.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then stockTarget.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then stockTarget.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then stockTarget.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                stockTarget.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ModelID")) Then
                stockTarget.VechileModel = New VechileModel(CType(dr("ModelID"), Short))
            End If

            Return stockTarget

        End Function

        Private Sub SetTableName()

            If Not (GetType(StockTarget) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(StockTarget), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(StockTarget).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

