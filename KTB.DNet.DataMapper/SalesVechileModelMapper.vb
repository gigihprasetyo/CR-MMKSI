#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Dnet
'// PURPOSE       : SalesVechileModel Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2022 
'// ---------------------
'// $History      : $
'// Generated on 2/4/2022 - 9:08:40 PM
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

    Public Class SalesVechileModelMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSalesVechileModel"
        Private m_UpdateStatement As String = "up_UpdateSalesVechileModel"
        Private m_RetrieveStatement As String = "up_RetrieveSalesVechileModel"
        Private m_RetrieveListStatement As String = "up_RetrieveSalesVechileModelList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSalesVechileModel"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim salesVechileModel As SalesVechileModel = Nothing
            While dr.Read

                salesVechileModel = Me.CreateObject(dr)

            End While

            Return salesVechileModel

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim salesVechileModelList As ArrayList = New ArrayList

            While dr.Read
                Dim salesVechileModel As SalesVechileModel = Me.CreateObject(dr)
                salesVechileModelList.Add(salesVechileModel)
            End While

            Return salesVechileModelList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesVechileModel As SalesVechileModel = CType(obj, SalesVechileModel)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, salesVechileModel.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesVechileModel As SalesVechileModel = CType(obj, SalesVechileModel)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int16, 2)
            DbCommandWrapper.AddInParameter("@NewVechileModelDesc", DbType.AnsiString, salesVechileModel.NewVechileModelDesc)
            DbCommandWrapper.AddInParameter("@SalesFlag", DbType.Byte, salesVechileModel.SalesFlag)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesVechileModel.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, salesVechileModel.LastUpdateBy)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, salesVechileModel.LastUpdateTime)

            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, Me.GetRefObject(salesVechileModel.Category))
            DbCommandWrapper.AddInParameter("@ModelID", DbType.Byte, Me.GetRefObject(salesVechileModel.VechileModel))

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

            Dim salesVechileModel As SalesVechileModel = CType(obj, SalesVechileModel)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, salesVechileModel.ID)
            DbCommandWrapper.AddInParameter("@NewVechileModelDesc", DbType.AnsiString, salesVechileModel.NewVechileModelDesc)
            DbCommandWrapper.AddInParameter("@SalesFlag", DbType.Byte, salesVechileModel.SalesFlag)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesVechileModel.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, salesVechileModel.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, salesVechileModel.LastUpdateBy)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, salesVechileModel.LastUpdateTime)


            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, Me.GetRefObject(salesVechileModel.Category))
            DbCommandWrapper.AddInParameter("@ModelID", DbType.Byte, Me.GetRefObject(salesVechileModel.VechileModel))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SalesVechileModel

            Dim salesVechileModel As SalesVechileModel = New SalesVechileModel

            salesVechileModel.ID = CType(dr("ID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("NewVechileModelDesc")) Then salesVechileModel.NewVechileModelDesc = dr("NewVechileModelDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesFlag")) Then salesVechileModel.SalesFlag = CType(dr("SalesFlag"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then salesVechileModel.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then salesVechileModel.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then salesVechileModel.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then salesVechileModel.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then salesVechileModel.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryID")) Then
                salesVechileModel.Category = New Category(CType(dr("CategoryID"), Byte))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ModelID")) Then
                salesVechileModel.VechileModel = New VechileModel(CType(dr("ModelID"), Byte))
            End If

            Return salesVechileModel

        End Function

        Private Sub SetTableName()

            If Not (GetType(SalesVechileModel) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SalesVechileModel), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SalesVechileModel).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
