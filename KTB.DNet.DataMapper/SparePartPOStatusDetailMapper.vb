
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartPOStatusDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 24/11/2005 - 10:33:08
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

    Public Class SparePartPOStatusDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSparePartPOStatusDetail"
        Private m_UpdateStatement As String = "up_UpdateSparePartPOStatusDetail"
        Private m_RetrieveStatement As String = "up_RetrieveSparePartPOStatusDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveSparePartPOStatusDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSparePartPOStatusDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sparePartPOStatusDetail As SparePartPOStatusDetail = Nothing
            While dr.Read

                sparePartPOStatusDetail = Me.CreateObject(dr)

            End While

            Return sparePartPOStatusDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sparePartPOStatusDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim sparePartPOStatusDetail As SparePartPOStatusDetail = Me.CreateObject(dr)
                sparePartPOStatusDetailList.Add(sparePartPOStatusDetail)
            End While

            Return sparePartPOStatusDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartPOStatusDetail As SparePartPOStatusDetail = CType(obj, SparePartPOStatusDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartPOStatusDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartPOStatusDetail As SparePartPOStatusDetail = CType(obj, SparePartPOStatusDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@SOQuantity", DbType.Int32, sparePartPOStatusDetail.SOQuantity)
            DbCommandWrapper.AddInParameter("@BillingQuantity", DbType.Int32, sparePartPOStatusDetail.BillingQuantity)
            DbCommandWrapper.AddInParameter("@NetPrice", DbType.Currency, sparePartPOStatusDetail.NetPrice)
            DbCommandWrapper.AddInParameter("@SOPrice", DbType.Currency, sparePartPOStatusDetail.SOPrice)
            DbCommandWrapper.AddInParameter("@BillingPrice", DbType.Currency, sparePartPOStatusDetail.BillingPrice)
            DBCommandWrapper.AddInParameter("@DONumber", DbType.AnsiString, sparePartPOStatusDetail.DONumber)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartPOStatusDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sparePartPOStatusDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SparePartPOStatusID", DbType.Int32, Me.GetRefObject(sparePartPOStatusDetail.SparePartPOStatus))
            DbCommandWrapper.AddInParameter("@SparePartMasterID", DbType.Int32, Me.GetRefObject(sparePartPOStatusDetail.SparePartMaster))

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

            Dim sparePartPOStatusDetail As SparePartPOStatusDetail = CType(obj, SparePartPOStatusDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartPOStatusDetail.ID)
            DbCommandWrapper.AddInParameter("@SOQuantity", DbType.Int32, sparePartPOStatusDetail.SOQuantity)
            DbCommandWrapper.AddInParameter("@BillingQuantity", DbType.Int32, sparePartPOStatusDetail.BillingQuantity)
            DbCommandWrapper.AddInParameter("@NetPrice", DbType.Currency, sparePartPOStatusDetail.NetPrice)
            DbCommandWrapper.AddInParameter("@SOPrice", DbType.Currency, sparePartPOStatusDetail.SOPrice)
            DbCommandWrapper.AddInParameter("@BillingPrice", DbType.Currency, sparePartPOStatusDetail.BillingPrice)
            DBCommandWrapper.AddInParameter("@DONumber", DbType.AnsiString, sparePartPOStatusDetail.DONumber)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartPOStatusDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sparePartPOStatusDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SparePartPOStatusID", DbType.Int32, Me.GetRefObject(sparePartPOStatusDetail.SparePartPOStatus))
            DbCommandWrapper.AddInParameter("@SparePartMasterID", DbType.Int32, Me.GetRefObject(sparePartPOStatusDetail.SparePartMaster))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SparePartPOStatusDetail

            Dim sparePartPOStatusDetail As SparePartPOStatusDetail = New SparePartPOStatusDetail

            sparePartPOStatusDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SOQuantity")) Then sparePartPOStatusDetail.SOQuantity = CType(dr("SOQuantity"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("BillingQuantity")) Then sparePartPOStatusDetail.BillingQuantity = CType(dr("BillingQuantity"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("NetPrice")) Then sparePartPOStatusDetail.NetPrice = CType(dr("NetPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("SOPrice")) Then sparePartPOStatusDetail.SOPrice = CType(dr("SOPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("BillingPrice")) Then sparePartPOStatusDetail.BillingPrice = CType(dr("BillingPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("DONumber")) Then sparePartPOStatusDetail.DONumber = dr("DONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sparePartPOStatusDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sparePartPOStatusDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sparePartPOStatusDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sparePartPOStatusDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sparePartPOStatusDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartPOStatusID")) Then
                sparePartPOStatusDetail.SparePartPOStatus = New SparePartPOStatus(CType(dr("SparePartPOStatusID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartMasterID")) Then
                sparePartPOStatusDetail.SparePartMaster = New SparePartMaster(CType(dr("SparePartMasterID"), Integer))
            End If

            Return sparePartPOStatusDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(SparePartPOStatusDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SparePartPOStatusDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SparePartPOStatusDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

