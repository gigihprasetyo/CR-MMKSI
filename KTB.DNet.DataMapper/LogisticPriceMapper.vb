
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : LogisticPrice Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 8/28/2017 - 1:08:30 PM
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

    Public Class LogisticPriceMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertLogisticPrice"
        Private m_UpdateStatement As String = "up_UpdateLogisticPrice"
        Private m_RetrieveStatement As String = "up_RetrieveLogisticPrice"
        Private m_RetrieveListStatement As String = "up_RetrieveLogisticPriceList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteLogisticPrice"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim logisticPrice As LogisticPrice = Nothing
            While dr.Read

                logisticPrice = Me.CreateObject(dr)

            End While

            Return logisticPrice

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim logisticPriceList As ArrayList = New ArrayList

            While dr.Read
                Dim logisticPrice As LogisticPrice = Me.CreateObject(dr)
                logisticPriceList.Add(logisticPrice)
            End While

            Return logisticPriceList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim logisticPrice As LogisticPrice = CType(obj, LogisticPrice)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, logisticPrice.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim logisticPrice As LogisticPrice = CType(obj, LogisticPrice)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddInParameter("@RegionCode", DbType.String, logisticPrice.RegionCode)
            DbCommandWrapper.AddInParameter("@RegionDescription", DbType.String, logisticPrice.RegionDescription)
            DbCommandWrapper.AddInParameter("@SAPModel", DbType.String, logisticPrice.SAPModel)
            DbCommandWrapper.AddInParameter("@EffectiveDate", DbType.DateTime, logisticPrice.EffectiveDate)
            DbCommandWrapper.AddInParameter("@LogisticPrice", DbType.Currency, logisticPrice.LogisticPrice)
            DbCommandWrapper.AddInParameter("@PPn", DbType.Decimal, logisticPrice.PPn)
            DbCommandWrapper.AddInParameter("@Status", DbType.String, logisticPrice.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, logisticPrice.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, logisticPrice.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, logisticPrice.ID)

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

            Dim logisticPrice As LogisticPrice = CType(obj, LogisticPrice)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@RegionCode", DbType.String, logisticPrice.RegionCode)
            DbCommandWrapper.AddInParameter("@RegionDescription", DbType.String, logisticPrice.RegionDescription)
            DbCommandWrapper.AddInParameter("@SAPModel", DbType.String, logisticPrice.SAPModel)
            DbCommandWrapper.AddInParameter("@EffectiveDate", DbType.DateTime, logisticPrice.EffectiveDate)
            DbCommandWrapper.AddInParameter("@LogisticPrice", DbType.Currency, logisticPrice.LogisticPrice)
            DbCommandWrapper.AddInParameter("@PPn", DbType.Decimal, logisticPrice.PPn)
            DbCommandWrapper.AddInParameter("@Status", DbType.String, logisticPrice.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, logisticPrice.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, logisticPrice.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, logisticPrice.ID)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As LogisticPrice

            Dim logisticPrice As LogisticPrice = New LogisticPrice

            If Not dr.IsDBNull(dr.GetOrdinal("RegionCode")) Then logisticPrice.RegionCode = dr("RegionCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RegionDescription")) Then logisticPrice.RegionDescription = dr("RegionDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EffectiveDate")) Then logisticPrice.EffectiveDate = CType(dr("EffectiveDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LogisticPrice")) Then logisticPrice.LogisticPrice = CType(dr("LogisticPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPn")) Then logisticPrice.PPn = CType(dr("PPn"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then logisticPrice.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SAPModel")) Then logisticPrice.SAPModel = dr("SAPModel").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then logisticPrice.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then logisticPrice.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then logisticPrice.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then logisticPrice.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then logisticPrice.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ID")) Then
                logisticPrice.ID = CType(dr("ID"), Integer)
            End If

            Return logisticPrice

        End Function

        Private Sub SetTableName()

            If Not (GetType(LogisticPrice) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(LogisticPrice), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(LogisticPrice).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

